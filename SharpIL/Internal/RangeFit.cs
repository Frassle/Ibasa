/* -----------------------------------------------------------------------------
 
	Copyright (c) 2009 Fraser Waters						  frassle@gmail.com

	Permission is hereby granted, free of charge, to any person obtaining
	a copy of this software and associated documentation files (the 
	"Software"), to deal in the Software without restriction, including
	without limitation the rights to use, copy, modify, merge, publish,
	distribute, sublicense, and/or sell copies of the Software, and to 
	permit persons to whom the Software is furnished to do so, subject to 
	the following conditions:

	The above copyright notice and this permission notice shall be included
	in all copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
	OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
	MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
	IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY 
	CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
	TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
	SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

	-------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Maths;

namespace Ibasa.Squash.Internal
{
    static class RangeFit 
    {
        public static ColorBlock Fit(ColorSet colors, Options options, bool isBC1)
        {
            int count = colors.Count;
            Vector3[] points = colors.Points;
            double[] weights = colors.Weights;
	
	        // compute the principle component
	        Vector3 principle = ComputePrincipleComponent(count, points, weights);

            // get the min and max range as the codebook endpoints
            Vector3 start = new Vector3();
            Vector3 end = new Vector3();
            if( count > 0 )
            {
	            double min, max;
		
	            // compute the range
	            start = end = points[0];
	            min = max = Vector3.Dot(points[0], principle);
	            for( int i = 1; i < count; ++i )
	            {
		            double val = Vector3.Dot(points[i], principle);
		            if( val < min )
		            {
			            start = points[i];
			            min = val;
		            }
		            else if( val > max )
		            {
			            end = points[i];
			            max = val;
		            }
	            }
            }
            
	        // clamp the output to [0, 1]
            start = Vector3.Clamp(start, 0.0, 1.0);
            end = Vector3.Clamp(end, 0.0, 1.0);


	        // clamp to the grid and save
            Vector3 grid = new Vector3(31.0, 63.0, 31.0);
            Vector3 gridrcp = Vector3.Reciprocal(grid);
            Vector3 half = new Vector3(0.5);

            start = Truncate(grid * start + half) * gridrcp;
            end = Truncate(grid * end + half) * gridrcp;
	
	        // create a codebook
	        Vector3[] codes = new Vector3[4];
	        codes[0] = start;
	        codes[1] = end;
            codes[2] = (2.0 / 3.0) * start + (1.0 / 3.0) * end;
            codes[3] = (1.0 / 3.0) * start + (2.0 / 3.0) * end;

	        // match each point to the closest code
	        int[] closest = new int[16];
	        double error = 0.0f;
	        for( int i = 0; i < count; ++i )
	        {
		        // find the closest code
		        double dist = double.MaxValue;
		        int idx = 0;
		        for( int j = 0; j < 4; ++j )
		        {
                    double d = (options.Metric * (points[i] - codes[j])).MagnitudeSquared;
			        if( d < dist )
			        {
				        dist = d;
				        idx = j;
			        }
		        }
		
		        // save the index
		        closest[i] = idx;
		
		        // accumulate the error
		        error += dist;
	        }
	
	        
		    // remap the indices
	        int[] indices = new int[16];
            colors.RemapIndices(closest, indices);

            return new ColorBlock(new Color(start.X, start.Y, start.Z), new Color(end.X, end.Y, end.Z), indices);
        }

        public static ChannelBlock Fit(ChannelSet points, Options options)
        {
            throw new NotImplementedException();
        }

        static Vector3 Truncate(Vector3 value)
        {
            return new Vector3(
                value.X > 0.0 ? Math.Floor(value.X) : Math.Ceiling(value.X),
                value.Y > 0.0 ? Math.Floor(value.Y) : Math.Ceiling(value.Y),
                value.Z > 0.0 ? Math.Floor(value.Z) : Math.Ceiling(value.Z));
        }

        #region CPC
        static Vector3 ComputePrincipleComponent(int count, Vector3[] points, double[] weights)
        {
            // get the covariance covariance
            // compute the centroid
            double total = 0.0;
            Vector3 centroid = new Vector3();
            for (int i = 0; i < count; ++i)
            {
                total += weights[i];
                centroid += weights[i] * points[i];
            }
            centroid /= total;

            // accumulate the covariance covariance
            double[] covariance = new double[6];
            for (int i = 0; i < count; ++i)
            {
                Vector3 va = points[i] - centroid;
                Vector3 vb = weights[i] * va;

                covariance[0] += va.X * vb.X;
                covariance[1] += va.X * vb.Y;
                covariance[2] += va.X * vb.Z;
                covariance[3] += va.Y * vb.Y;
                covariance[4] += va.Y * vb.Z;
                covariance[5] += va.Z * vb.Z;
            }

            // compute the cubic coefficients
            double c0 =
                covariance[0] * covariance[3] * covariance[5]
                + 2.0 * covariance[1] * covariance[2] * covariance[4]
                - covariance[0] * covariance[4] * covariance[4]
                - covariance[3] * covariance[2] * covariance[2]
                - covariance[5] * covariance[1] * covariance[1];
            double c1 =
                covariance[0] * covariance[3] + covariance[0] * covariance[5] + covariance[3] * covariance[5]
                - covariance[1] * covariance[1] - covariance[2] * covariance[2] - covariance[4] * covariance[4];
            double c2 =
                covariance[0] + covariance[3] + covariance[5];

            // compute the quadratic coefficients
            double a = c1 - (1.0 / 3.0) * c2 * c2;
            double b = (-2.0 / 27.0) * c2 * c2 * c2 + (1.0 / 3.0) * c1 * c2 - c0;

            // compute the root count check
            double Q = 0.25 * b * b + (1.0 / 27.0) * a * a * a;

            // test the multiplicity
            if (double.Epsilon < Q)
            {
                // only one root, which implies we have a multiple of the identity
                return new Vector3(1.0);
            }
            else if (Q < -double.Epsilon)
            {
                // three distinct roots
                double theta = Math.Atan2(Math.Sqrt(-Q), -0.5 * b);
                double rho = Math.Sqrt(0.25 * b * b - Q);

                double rt = Math.Pow(rho, 1.0 / 3.0);
                double ct = Math.Cos(theta / 3.0);
                double st = Math.Sin(theta / 3.0);

                double l1 = (1.0 / 3.0) * c2 + 2.0 * rt * ct;
                double l2 = (1.0 / 3.0) * c2 - rt * (ct + Math.Sqrt(3.0) * st);
                double l3 = (1.0 / 3.0) * c2 - rt * (ct - Math.Sqrt(3.0) * st);

                // pick the larger
                if (Math.Abs(l2) > Math.Abs(l1))
                    l1 = l2;
                if (Math.Abs(l3) > Math.Abs(l1))
                    l1 = l3;

                // get the eigenvector
                return GetMultiplicity1Evector(covariance, l1);
            }
            else // if( -FLT_EPSILON <= Q && Q <= FLT_EPSILON )
            {
                // two roots
                double rt;
                if (b < 0.0f)
                    rt = -Math.Pow(-0.5 * b, 1.0 / 3.0);
                else
                    rt = Math.Pow(0.5 * b, 1.0 / 3.0);

                double l1 = (1.0 / 3.0) * c2 + rt;		// repeated
                double l2 = (1.0 / 3.0) * c2 - 2.0 * rt;

                // get the eigenvector
                if (Math.Abs(l1) > Math.Abs(l2))
                    return GetMultiplicity2Evector(covariance, l1);
                else
                    return GetMultiplicity1Evector(covariance, l2);
            }
        }


        static Vector3 GetMultiplicity1Evector(double[] covariance, double evalue)
        {
            // compute M
            double[] m = new double[6];
            m[0] = covariance[0] - evalue;
            m[1] = covariance[1];
            m[2] = covariance[2];
            m[3] = covariance[3] - evalue;
            m[4] = covariance[4];
            m[5] = covariance[5] - evalue;

            // compute U
            double[] u = new double[6];
            u[0] = m[3] * m[5] - m[4] * m[4];
            u[1] = m[2] * m[4] - m[1] * m[5];
            u[2] = m[1] * m[4] - m[2] * m[3];
            u[3] = m[0] * m[5] - m[2] * m[2];
            u[4] = m[1] * m[2] - m[4] * m[0];
            u[5] = m[0] * m[3] - m[1] * m[1];

            // find the largest component
            double mc = Math.Abs(u[0]);
            int mi = 0;
            for (int i = 1; i < 6; ++i)
            {
                double c = Math.Abs(u[i]);
                if (c > mc)
                {
                    mc = c;
                    mi = i;
                }
            }

            // pick the column with this component
            switch (mi)
            {
                case 0:
                    return new Vector3(u[0], u[1], u[2]);

                case 1:
                case 3:
                    return new Vector3(u[1], u[3], u[4]);

                default:
                    return new Vector3(u[2], u[4], u[5]);
            }
        }

        static Vector3 GetMultiplicity2Evector(double[] covariance, double evalue)
        {
            // compute M
            double[] m = new double[6];
            m[0] = covariance[0] - evalue;
            m[1] = covariance[1];
            m[2] = covariance[2];
            m[3] = covariance[3] - evalue;
            m[4] = covariance[4];
            m[5] = covariance[5] - evalue;

            // find the largest component
            double mc = Math.Abs(m[0]);
            int mi = 0;
            for (int i = 1; i < 6; ++i)
            {
                double c = Math.Abs(m[i]);
                if (c > mc)
                {
                    mc = c;
                    mi = i;
                }
            }

            // pick the first eigenvector based on this index
            switch (mi)
            {
                case 0:
                case 1:
                    return new Vector3(-m[1], m[0], 0.0);

                case 2:
                    return new Vector3(m[2], 0.0, -m[0]);

                case 3:
                case 4:
                    return new Vector3(0.0, -m[4], m[3]);

                default:
                    return new Vector3(0.0, -m[5], m[4]);
            }
        }
        #endregion
    }
}