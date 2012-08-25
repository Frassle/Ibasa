using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Internal
{
	static class BoxiFit
	{
        public static void Fit(ColordBlock block, ColordSet colors, Options options, bool isBC1)
		{
            Vector3d maxColor = Vector3d.Zero;
            Vector3d minColor = Vector3d.One;

            foreach(Vector3d color in colors.Points)
            {
                minColor = Vector.Min(minColor, color);
                maxColor = Vector.Max(maxColor, color);
			}

            Vector3d inset = (maxColor - minColor) / 8.0;

            minColor = Vector.Clamp(minColor + inset, Vector3d.Zero, Vector3d.One);
            maxColor = Vector.Clamp(maxColor - inset, Vector3d.Zero, Vector3d.One);

            Vector3d color0 = minColor;
            Vector3d color1 = maxColor;
            Vector3d color2;
            Vector3d color3;

            if (isBC1 & colors.IsTransparent)
			{
				//need alpha coding, co <= c1
                var max = Vector.Pack(5, 6, 5, Color.Quantize(5, 6, 5, (Colord)maxColor));
                var min = Vector.Pack(5, 6, 5, Color.Quantize(5, 6, 5, (Colord)minColor));

                if (max <= min)
				{
					color0 = maxColor;
					color1 = minColor;
				}
			}

            if (!isBC1 || (Vector.Pack(5, 6, 5, Color.Quantize(5, 6, 5, (Colord)color0)) > Vector.Pack(5, 6, 5, Color.Quantize(5, 6, 5, (Colord)color1))))
            {
                color2 = Vector.Lerp(color0, color1, 1.0 / 3.0);
                color3 = Vector.Lerp(color0, color1, 2.0 / 3.0);
            }
            else
            {
                color2 = Vector.Lerp(color0, color1, 1.0 / 2.0);
                color3 = new Vector3d(0.0);
            }

            for (int y = 0; y < colors.Height; ++y)
            {
                for (int x = 0; x < colors.Width; ++x)
                {
                    Vector3d color = colors.Points[x, y];

                    double d0 = Vector.AbsoluteSquared(Vector.Modulate(options.Metric, (color0 - color)));
                    double d1 = Vector.AbsoluteSquared(Vector.Modulate(options.Metric, (color1 - color)));
                    double d2 = Vector.AbsoluteSquared(Vector.Modulate(options.Metric, (color2 - color)));
                    double d3 = Vector.AbsoluteSquared(Vector.Modulate(options.Metric, (color3 - color)));

                    int b0 = d0 > d3 ? 1 : 0;
                    int b1 = d1 > d2 ? 1 : 0;
                    int b2 = d0 > d2 ? 1 : 0;
                    int b3 = d1 > d3 ? 1 : 0;
                    int b4 = d2 > d3 ? 1 : 0;

                    int x0 = b1 & b2;
                    int x1 = b0 & b3;
                    int x2 = b0 & b4;

                    block.Indices[x + (y * 4)] = (x2 | ((x0 | x1) << 1));
                }
            }

            block.Colord0 = (int)Vector.Pack(5, 6, 5, Color.Quantize(5, 6, 5, (Colord)color0));
            block.Colord0 = (int)Vector.Pack(5, 6, 5, Color.Quantize(5, 6, 5, (Colord)color0));
        }

    //    static void FixRange(ref double min, ref double max, double zeroMinus)
    //    {
    //        double inset = (max - min) / 8;

    //        min = Functions.Clamp(min + inset, zeroMinus, 1);
    //        max = Functions.Clamp(max - inset, zeroMinus, 1);
    //    }
    //    static double FitCodes(ChannelSet colors, double[] codes, int[] indices )
    //    {
    //        // fit each alpha value to the codebook
    //        double err = 0;
    //        for(int i = 0; i < 16; ++i)
    //        {
    //            // find the least error and corresponding index
    //            double value = colors.Points[i];
    //            double least = double.MaxValue;
    //            int index = 0;
    //            for(int j = 0; j < 8; ++j)
    //            {
    //                // get the squared error from this code
    //                double dist = value - codes[j];
    //                dist *= dist;

    //                // compare with the best so far
    //                if( dist < least )
    //                {
    //                    least = dist;
    //                    index = j;
    //                }
    //            }

    //            // save this index and accumulate the error
    //            indices[i] = index;
    //            err += least;
    //        }

    //        // return the total error
    //        return err;
    //    }
    //    public static ChannelBlock Fit(ChannelSet colors, Options options, bool signed)
    //    {
    //        double zeroMinus = 0.0;
    //        if (signed)
    //            zeroMinus = -1.0;

    //        double max5 = zeroMinus;
    //        double min5 = 1.0;
    //        double max7 = zeroMinus;
    //        double min7 = 1.0;

    //        for (int i = 0; i < colors.Count; ++i)
    //        {
    //            double color = colors.Points[i];

    //            // incorporate into the min/max
    //             if( color < min7 )
    //                    min7 = color;
    //             if( color > max7 )
    //                    max7 = color;
    //             if( color != zeroMinus && color < min5 )
    //                    min5 = color;
    //             if( color != 1 && color > max5 )
    //                    max5 = color;
    //        }
			
    //        if( min5 > max5 )
    //            min5 = max5;
    //        if( min7 > max7 )
    //            min7 = max7;
 
    //        // fix the range to be the minimum in each case
    //        FixRange(ref min5, ref max5, zeroMinus);
    //        FixRange(ref min7, ref max7, zeroMinus);
			
    //        // set up the 5-alpha code book
    //        double[] codes5 = new double[8];
    //        codes5[0] = min5;
    //        codes5[1] = max5;
    //        for( int i = 1; i < 5; ++i )
    //            codes5[1 + i] = ( ( ( 5 - i )*min5 + i*max5 )/5.0 );
    //        codes5[6] = zeroMinus;
    //        codes5[7] = 1;

    //        // set up the 7-alpha code book
    //        double[] codes7 = new double[8];
    //        codes7[0] = min7;
    //        codes7[1] = max7;
    //        for( int i = 1; i < 7; ++i )
    //            codes7[1 + i] = ( ( ( 7 - i )*min7 + i*max7 )/7.0 );

    //        int[] indices5 = new int[16];
    //        int[] indices7 = new int[16];
    //        double err5 = FitCodes(colors, codes5, indices5 );
    //        double err7 = FitCodes(colors, codes7, indices7 );

    //        int[] remapped = new int[16];
    //        colors.RemapIndices(indices5, remapped);
				
    //        // save the block with least error
    //        if( err5 <= err7 )
    //        {
    //            // check the relative values of the endpoints
    //            if( min5 > max5 )
    //            {
    //                // swap the indices
    //                int[] swapped = new int[16];
    //                for( int i = 0; i < 16; ++i )
    //                {
    //                    int index = remapped[i];
    //                    if( index == 0 )
    //                        swapped[i] = 1;
    //                    else if( index == 1 )
    //                        swapped[i] = 0;
    //                    else if( index <= 5 )
    //                        swapped[i] = 7 - index;
    //                    else 
    //                        swapped[i] = index;
    //                }

    //                // write the block
    //                return new ChannelBlock(max5, min5, swapped);
    //            }
    //            else
    //            {
    //                // write the block
    //                return new ChannelBlock(min5, max5, remapped);
    //            }       
 
    //        }
    //        else
    //        {
    //            // check the relative values of the endpoints
    //            if( min7 < max7 )
    //            {
    //                // swap the indices
    //                int[] swapped = new int[16];
    //                for( int i = 0; i < 16; ++i )
    //                {
    //                    int index = remapped[i];
    //                    if( index == 0 )
    //                        swapped[i] = 1;
    //                    else if( index == 1 )
    //                        swapped[i] = 0;
    //                    else
    //                        swapped[i] = 9 - index;
    //                }

    //                // write the block
    //                return new ChannelBlock(max7, min7, swapped);
    //            }
    //            else
    //            {
    //                // write the block
    //                return new ChannelBlock(min7, max7, remapped);
    //            }       

    //        }
    //    }
    }
}