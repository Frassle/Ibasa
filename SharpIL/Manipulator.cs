using System;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL
{
    public static class Manipulator
    {
        #region Mipmaps
        public static Image[] GenerateMipmaps(Image source, Filter filter)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            int count = (int)Functions.Log(Functions.Max(source.Width, Functions.Max(source.Height, source.Depth)), 2) + 1;
            Image[] mips = new Image[count];

            mips[0] = new Image(source);

            int width = Functions.Max(source.Width >> 1, 1);
            int height = Functions.Max(source.Height >> 1, 1);
            int depth = Functions.Max(source.Depth >> 1, 1);
            for (int i = 1; i < count; ++i)
            {
                mips[i] = Scale(source, width, height, depth, filter);

                width = Functions.Max(width >> 1, 1);
                height = Functions.Max(height >> 1, 1);
                depth = Functions.Max(depth >> 1, 1);
            }

            return mips;
        }
        #endregion

        #region Scaling
        public enum Filter
        {
            Point,
            Linear,
			Cubic,
			Spline,
        }

        public static Image Scale(Image source, int width, int height, int depth, Filter filter)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (source.Width == width && source.Height == height && source.Depth == depth)
                return new Image(source);

            Image scaled = new Image(new Size3i(width, height, depth));

            if (scaled.Height == 1 && source.Height == 1 && scaled.Depth == 1 && source.Depth == 1)
                Scale1D(source, scaled, filter);
            else if (scaled.Depth == 1 && source.Depth == 1)
                Scale2D(source, scaled, filter);
            else
                Scale3D(source, scaled, filter);

            return scaled;
        }

        static Colord Lerp(Colord p0, Colord p1, double f)
		{
			return p0 + f * (p1 - p0);
		}
        static Colord Cerp(Colord p0, Colord p1, Colord p2, Colord p3, double f, double f2, double f3)
		{
            Colord p = (p3 - p2) - (p0 - p1);
            Colord q = (p0 - p1) - p;
            Colord r = p2 - p0;
            Colord s = p1;

			return p*f3 + q*f2 + r*f + s;
		}
        static Colord Serp(Colord p0, Colord p1, Colord p2, Colord p3, double cint0, double cint1, double cint2, double cint3)
		{
			return p0 * cint0 + p1 * cint1 + p2 * cint2 + p3 * cint3;
		}

        #region 1D
	
        static void Scale1D(Image source, Image scaled, Filter filter)
        {
            double scaleX = (double)source.Width / scaled.Width;

            switch (filter)
            {
                case Filter.Point:
                    for (int x = 0; x < scaled.Width; ++x)
                    {
                        int srcX = (int)(x * scaleX);
                        scaled[x] = source[srcX];
                    }
                    return;

                case Filter.Linear:
                    for (int x = 0; x < scaled.Width; ++x)
                    {
						int x0 = (int)(x * scaleX);
                        int x1 = Functions.Min(x0+1, source.Width-1);

                        double fX = (x * scaleX) - x0;

                        scaled[x] = Lerp(
							source[x0], 
							source[x1], 
							fX);
                    }
                    return;
					
				case Filter.Cubic:
					for (int x = 0; x < scaled.Width; ++x)
                    {
                        int x1 = (int)(x * scaleX);
                        int x2 = Functions.Min(x1+1, source.Width-1);
						int x0 = Functions.Max(x1-1,0);
						int x3 = Functions.Min(x2+1, source.Width-1);

                        double fX = (x * scaleX) - x1;
                        double fX2 = fX * fX;
                        double fX3 = fX2 * fX;
						
                        scaled[x] = Cerp(
							source[x0], 
							source[x1], 
							source[x2], 
							source[x3],
							fX, fX2, fX3);
                    }
					return;
				
				case Filter.Spline:
					for (int x = 0; x < scaled.Width; ++x)
                    {
                        int x1 = (int)(x * scaleX);
                        int x2 = Functions.Min(x1+1, source.Width-1);
						int x0 = Functions.Max(x1-1,0);
						int x3 = Functions.Min(x2+1, source.Width-1);

                        double fX = (x * scaleX) - x1;

                        double cintx0 = (-fX * (fX - 1) * (fX - 2)) / 6;
                        double cintx1 = (3 * (fX + 1) * (fX - 1) * (fX - 2)) / 6;
                        double cintx2 = (-3 * (fX + 1) * fX * (fX - 2)) / 6;
                        double cintx3 = ((fX + 1) * fX * (fX - 1)) / 6;
						
                        scaled[x] = Serp(
							source[x0], 
							source[x1], 
							source[x2], 
							source[x3],
							cintx0, cintx1, cintx2, cintx3);
                    }
					return;
            }
        }
        #endregion

        #region 2D
        static void Scale2D(Image source, Image scaled, Filter filter)
        {
            switch (filter)
            {
                case Filter.Point:
                    Scale2DPoint(source, scaled);
                    break;
                case Filter.Linear:
                    Scale2DLinear(source, scaled);
                    break;
				case Filter.Cubic:
					Scale2DCubic(source, scaled);
					break;
				case Filter.Spline:
					Scale2DSpline(source, scaled);
					break;
            }
        }
        static void Scale2DPoint(Image source, Image scaled)
        {
            double scaleX = (double)source.Width / scaled.Width;
            double scaleY = (double)source.Height / scaled.Height;

            for (int y = 0; y < scaled.Height; ++y)
            {
                int srcY = (int)(y * scaleY);
				
                for (int x = 0; x < scaled.Width; ++x)
				{
					int srcX = (int)(x * scaleX);
					scaled[x, y] = source[srcX, srcY];
				}
            }
        }
        static void Scale2DLinear(Image source, Image scaled)
        {
            double scaleX = (double)source.Width / scaled.Width;
            double scaleY = (double)source.Height / scaled.Height;

            for (int y = 0; y < scaled.Height; ++y)
            {
                int y0 = (int)(y * scaleY);
				int y1 = Functions.Min(y0+1, source.Height-1);
					
				double fY = (y * scaleY) - y0;

                for (int x = 0; x < scaled.Width; ++x)
				{
					int x0 = (int)(x * scaleX);
					int x1 = Functions.Min(x0+1, source.Width-1);
					
					double fX = (x * scaleX) - x0;
					
					scaled[x, y] = Lerp(
						Lerp(source[x0, y0], source[x1, y0], fX), 
						Lerp(source[x0, y1], source[x1, y1], fX), 
						fY);
				}
            }
        }
		static void Scale2DCubic(Image source, Image scaled)
		{
			double scaleX = (double)source.Width / scaled.Width;
            double scaleY = (double)source.Height / scaled.Height;

            for (int y = 0; y < scaled.Height; ++y)
            {
                int y1 = (int)(y * scaleY);
				int y2 = Functions.Min(y1+1, source.Height-1);
				int y0 = Functions.Max(y1-1,0);
				int y3 = Functions.Min(y2+1, source.Height-1);

				double fY = (y * scaleY) - y1;
				double fY2 = fY*fY;
				double fY3 = fY2*fY;
				
				for (int x = 0; x < scaled.Width; ++x)
				{
					int x1 = (int)(x * scaleX);
					int x2 = Functions.Min(x1+1, source.Width-1);
					int x0 = Functions.Max(x1-1,0);
					int x3 = Functions.Min(x2+1, source.Width-1);

					double fX = (x * scaleX) - x1;
					double fX2 = fX*fX;
					double fX3 = fX2*fX;

                    Colord p = Cerp(
						Cerp(
							source.GetPixel(x0, y0, 0), 
							source.GetPixel(x1, y0, 0), 
							source.GetPixel(x2, y0, 0), 
							source.GetPixel(x3, y0, 0),
							fX, fX2, fX3),
						Cerp(
							source.GetPixel(x0, y1, 0), 
							source.GetPixel(x1, y1, 0), 
							source.GetPixel(x2, y1, 0), 
							source.GetPixel(x3, y1, 0),
							fX, fX2, fX3),
                        Cerp(
                            source.GetPixel(x0, y2, 0),
                            source.GetPixel(x1, y2, 0),
                            source.GetPixel(x2, y2, 0),
                            source.GetPixel(x3, y2, 0),
                            fX, fX2, fX3),
                        Cerp(
                            source.GetPixel(x0, y3, 0),
                            source.GetPixel(x1, y3, 0),
                            source.GetPixel(x2, y3, 0),
                            source.GetPixel(x3, y3, 0),
                            fX, fX2, fX3),
						fY, fY2, fY3);
							
					scaled.SetPixel(x, 0, 0, p);
				}
			}
		}
		static void Scale2DSpline(Image source, Image scaled)
        {
            double scaleX = (double)source.Width / scaled.Width;
            double scaleY = (double)source.Height / scaled.Height;

            for (int y = 0; y < scaled.Height; ++y)
            {
                int y1 = (int)(y * scaleY);
				int y2 = Functions.Min(y1+1, source.Height-1);
				int y0 = Functions.Max(y1-1,0);
				int y3 = Functions.Min(y2+1, source.Height-1);

				double fY = (y * scaleY) - y1;
				
				double cinty0 = (-fY * (fY-1)*(fY-2))/6;
				double cinty1 = (3*(fY+1)*(fY-1)*(fY-2))/6;
				double cinty2 = (-3*(fY+1)*fY*(fY-2))/6;
				double cinty3 = ((fY+1)*fY*(fY-1))/6;

                for (int x = 0; x < scaled.Width; ++x)
				{
					int x1 = (int)(x * scaleX);
					int x2 = Functions.Min(x1+1, source.Width-1);
					int x0 = Functions.Max(x1-1,0);
					int x3 = Functions.Min(x2+1, source.Width-1);

					double fX = (x * scaleX) - x1;
					
					double cintx0 = (-fX * (fX-1)*(fX-2))/6;
					double cintx1 = (3*(fX+1)*(fX-1)*(fX-2))/6;
					double cintx2 = (-3*(fX+1)*fX*(fX-2))/6;
					double cintx3 = ((fX+1)*fX*(fX-1))/6;

                    Colord p = Serp(
						Serp(
							source.GetPixel(x0, y0, 0), 
							source.GetPixel(x1, y0, 0), 
							source.GetPixel(x2, y0, 0), 
							source.GetPixel(x3, y0, 0), 
							cintx0, cintx1, cintx2, cintx3), 
						Serp(
							source.GetPixel(x0, y1, 0), 
							source.GetPixel(x1, y1, 0), 
							source.GetPixel(x2, y1, 0), 
							source.GetPixel(x3, y1, 0), 
							cintx0, cintx1, cintx2, cintx3), 
						Serp(
							source.GetPixel(x0, y2, 0), 
							source.GetPixel(x1, y2, 0), 
							source.GetPixel(x2, y2, 0), 
							source.GetPixel(x3, y2, 0), 
							cintx0, cintx1, cintx2, cintx3), 
						Serp(
							source.GetPixel(x0, y3, 0), 
							source.GetPixel(x1, y3, 0), 
							source.GetPixel(x2, y3, 0), 
							source.GetPixel(x3, y3, 0), 
							cintx0, cintx1, cintx2, cintx3), 
						cinty0, cinty1, cinty2, cinty3);
					
					scaled.SetPixel(x, y, 0, p);
				}
            }
        }
        #endregion

        #region 3D
        static void Scale3D(Image source, Image scaled, Filter filter)
        {
            switch (filter)
            {
                case Filter.Point:
                    Scale3DPoint(source, scaled);
                    break;
                case Filter.Linear:
                    Scale3DLinear(source, scaled);
                    break;
            }
        }
        static void Scale3DPoint(Image source, Image scaled)
        {
            double scaleX = (double)source.Width / scaled.Width;
            double scaleY = (double)source.Height / scaled.Height;
            double scaleZ = (double)source.Depth / scaled.Depth;

            for (int z = 0; z < scaled.Depth; ++z)
            {
                int srcZ = (int)(z * scaleZ);

                for (int y = 0; y < scaled.Height; ++y)
                {
                    int srcY = (int)(y * scaleY);

                    for (int x = 0; x < scaled.Width; ++x)
                    {
                        int srcX = (int)(x * scaleX);

                        scaled[x, y, z] = source[srcX, srcY, srcZ];
                    }
                }
            }
        }
        static void Scale3DLinear(Image source, Image scaled)
        {
            double scaleX = (double)source.Width / scaled.Width;
            double scaleY = (double)source.Height / scaled.Height;
            double scaleZ = (double)source.Depth / scaled.Depth;

            for (int z = 0; z < scaled.Depth; ++z)
            {
                int z0 = (int)(z * scaleZ);
				int z1 = Functions.Min(z0+1, source.Depth-1);
				
				double fZ = (z * scaleZ) - z0;

                for (int y = 0; y < scaled.Height; ++y)
                {
                    int y0 = (int)(y * scaleY);
					int y1 = Functions.Min(y0+1, source.Height-1);
					
					double fY = (y * scaleY) - y0;

                    for (int x = 0; x < scaled.Width; ++x)
                    {
						int x0 = (int)(x * scaleX);
                        int x1 = Functions.Min(x0+1, source.Width-1);
						
                        double fX = (x * scaleX) - x0;

                        scaled[x, y, z] = Lerp(
							Lerp(
								Lerp(
									source.GetPixel(x0, y0, z0), 
									source.GetPixel(x1, y0, z0), 
									fX), 
								Lerp(
									source.GetPixel(x0, y1, z0), 
									source.GetPixel(x1, y1, z0), 
									fX), 
								fY), 
							Lerp(
								Lerp(
									source.GetPixel(x0, y0, z1), 
									source.GetPixel(x1, y0, z1), 
									fX), 
								Lerp(
									source.GetPixel(x0, y1, z1), 
									source.GetPixel(x1, y1, z1), 
									fX), 
								fY), 
							fZ);
                    }
                }
            }
        }
        #endregion
        #endregion

        #region Convolution
        #region Shader
        public static void Shader(Image image, Func<Colord, Colord> shader)
        {
            for (int z = 0; z < image.Depth; ++z)
            {
                for (int y = 0; y < image.Height; ++y)
                {
                    for (int x = 0; x < image.Width; ++x)
                    {
                        image[x, y, z] = shader(image[x, y, z]);
                    }
                }
            }
        }
        #endregion

        //public static Image Convolution(Image image, Matrix kernel)
        //{
        //    if (image == null)
        //        throw new ArgumentNullException("image");
        //    if (kernel == null)
        //        throw new ArgumentNullException("kernel");

        //    Image result = new Image(image.Size);

        //    for (int y = 0; y < image.Height; ++y)
        //    {
        //        for (int x = 0; x < image.Width; ++x)
        //        {
        //            Colord p = new Colord();
        //            for (int j = 0; j < kernel.Rows.Count; ++j)
        //            {
        //                for (int i = 0; i < kernel.Columns.Count; ++i)
        //                {
        //                    p += image[x - i, y - j] * kernel[i, j];
        //                }
        //            }
        //            result[x, y] = p;
        //        }
        //    }

        //    return result;
        //}
        #endregion

        #region Dither
        public static void Dither(Image image, Func<Colord, Colord> quantize)
        {
            for (int z = 0; z < image.Depth; ++z)
            {
                for (int y = 0; y < image.Height; ++y)
                {
                    for (int x = 0; x < image.Width; ++x)
                    {
                        Colord oldc = image[x, y, z];
                        Colord newc = quantize(oldc);
                        image[x, y, z] = newc;
                        Colord error = oldc - newc;

                        image[x + 1, y, z] += (7.0 / 16.0 * error);
                        image[x - 1, y + 1, z] += (3.0 / 16.0 * error);
                        image[x, y + 1, z] += (5.0 / 16.0 * error);
                        image[x + 1, y + 1, z] += (1.0 / 16.0 * error);
                    }
                }
            }
        }
        #endregion

        #region Filter

        public delegate double Metric(int x1, int y1, int z1, int x2, int y2, int z2);

        public static bool ThresholdBoundary(Colord value)
		{
            return (value.R >= 0.5);
		}
		
		public static double EuclideanMetric(int x1, int y1, int z1, int x2, int y2, int z2)
		{
			double x = x1-x2;
			double y = y1-y2;
			double z = z1-z2;
			return Functions.Sqrt(x*x+y*y+z*z);
		}
		public static double ManhattanMetric(int x1, int y1, int z1, int x2, int y2, int z2)
		{
			double x = Functions.Abs(x1-x2);
			double y = Functions.Abs(y1-y2);
			double z = Functions.Abs(z1-z2);
			return (x+y+z);
		}
		public static double ChessboardMetric(int x1, int y1, int z1, int x2, int y2, int z2)
		{
			double x = Functions.Abs(x1-x2);
			double y = Functions.Abs(y1-y2);
			double z = Functions.Abs(z1-z2);
			return Functions.Max(x, Functions.Max(y, z));
		}
		
		public static Image DistanceTransform(
			Image source, int width, int height, int depth,
            Func<Colord, bool> boundary, Metric metric)
		{
			Image dt = new Image(new Size3i(width, height, depth));
			
			double zScale = depth == 1 ? 0 : (double)(source.Depth-1)/(depth-1);
			double yScale = height == 1 ? 0 : (double)(source.Height-1)/(height-1);
			double xScale = width == 1 ? 0 : (double)(source.Width-1)/(width-1);

            int spread = (int)Functions.Max(source.Width, Functions.Max(source.Height, Functions.Max(source.Depth, 3)));
            if (spread % 2 == 0) //0 % 2 == 0
                spread += 1; //spread must be odd and at least 3
			
			for (int z = 0; z < dt.Depth; ++z)
            {
				int srcZ = (int)(z * zScale);
                for (int y = 0; y < dt.Height; ++y)
                {
					int srcY = (int)(y * yScale);
                    for (int x = 0; x < dt.Width; ++x)
                    {
						int srcX = (int)(x * xScale);
						int boxSize = 3;		
						
						double minDistance = double.MaxValue;
                        bool srcBoundary = boundary(source[srcX, srcY, srcZ]);

                        while (boxSize <= spread)
						{		
							int minZ = srcZ - (boxSize/2);
							int maxZ = srcZ + (boxSize/2);
							int minY = srcY - (boxSize/2);
							int maxY = srcY + (boxSize/2);
							int minX = srcX - (boxSize/2);
							int maxX = srcX + (boxSize/2);
							
							int zStart = Functions.Max(minZ, 0);
							int zEnd = Functions.Min(maxZ+1, source.Depth);
							int yStart = Functions.Max(minY, 0);
							int yEnd = Functions.Min(maxY+1, source.Height);
							int xStart = Functions.Max(minX, 0);
							int xEnd = Functions.Min(maxX+1, source.Width);

							for (int boxZ = zStart; boxZ < zEnd; ++boxZ)
							{
                                bool notOnZBorder = (boxZ != minZ && boxZ != maxZ);
								
								for (int boxY = yStart; boxY < yEnd; ++boxY)
								{
                                    bool notOnYBorder = (boxY != minY && boxY != maxY);
									
									for (int boxX = xStart; boxX < xEnd; ++boxX)
									{
										bool notOnXBorder = (boxX != minX && boxX != maxX);

                                        if (notOnZBorder && notOnYBorder && notOnXBorder)
											continue;

                                        bool boxBoundary = boundary(source[boxX, boxY, boxZ]);
                                        
                                        if (srcBoundary != boxBoundary)
											minDistance = Functions.Min(minDistance, metric(srcX,srcY,srcZ,boxX,boxY,boxZ));
									}
								}
							}
							
							//found closest opposite pixel
							if(minDistance != double.MaxValue)
								break;
							
							//not found any opposites yet, expand our search range
							boxSize += 2;
						}

                        if(srcBoundary)
                            dt[x, y, z] = new Colord(minDistance, 0, 0, 0);
                        else
                            dt[x, y, z] = new Colord(-minDistance, 0, 0, 0);
					}
				}
			}

            return dt;
		}

        public static Image Normalmap(Image depthmap, float depth)
        {
            //Create normalmap, only use the first slice if a 3D texture
            Image normalmap = new Image(new Size3i(depthmap.Width, depthmap.Height, 1));

            for (int y = 0; y < normalmap.Height; ++y)
            {
                for (int x = 0; x < normalmap.Width; ++x)
                {
                    double dx = 0.0;
                    double dy = 0.0;
                    double p;

                    p = depthmap[x - 1, y - 1].R * depth;
                    dx -= p;
                    dy -= p;

                    p = depthmap[x, y - 1].R * depth;
                    dy -= 2 * p;

                    p = depthmap[x + 1, y - 1].R * depth;
                    dx += p;
                    dy -= p;

                    p = depthmap[x - 1, y].R * depth;
                    dx -= 2 * p;

                    p = depthmap[x + 1, y].R * depth;
                    dx += 2 * p;

                    p = depthmap[x - 1, y + 1].R * depth;
                    dx -= p;
                    dy += p;

                    p = depthmap[x, y + 1].R * depth;
                    dy += 2 * p;

                    p = depthmap[x + 1, y + 1].R * depth;
                    dx += p;
                    dy += p;

                    Vector3d n = Vector.Normalize(new Vector3d(dx, dy, 0.0));

                    //[-1,1] -> [0,1]
                    n = (n * 0.5) + Vector3d.One;

                    normalmap[x, y] = new Colord(n.X, n.Y, n.Z);
                }
            }

            return normalmap;
        }
        #endregion
    }
}
