using System;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Internal
{
	sealed class ChannelSet
	{
        public ChannelSet(Colord[] pixels, int mask, int channel)
		{
			Count = 0;
            Points = new double[16];
			
			// create the minimal set
			for( int i = 0; i < 16; ++i )
			{
				// check this pixel is enabled
				int bit = 1 << i;
				if( ( mask & (1 << i) ) == 0 )
				{
					remap[i] = -1;
					continue;
				}
				
				// loop over previous points for a match
				for( int j = 0;; ++j )
				{
					// allocate a new point
					if( j == i )
					{
						// add the point
						Points[Count] = pixels[i][channel];
						remap[i] = Count;

						// advance
						++Count;
						break;
					}

					// check for a match
					int oldbit = 1 << j;
					bool match = ( ( mask & oldbit ) != 0 )
						&& ( pixels[i][channel] == pixels[j][channel]);
					if( match )
					{
						// get the index of the match
						int index = remap[j];
						
						// map to this point
						remap[i] = index;
						break;
					}
				}
			}
		}
		
		public int Count {get; private set; }
        public double[] Points { get; private set; }
		
		public void RemapIndices(int[] source, int[] target)
		{
			for( int i = 0; i < 16; ++i )
			{
				int j = remap[i];
				if( j == -1 )
					target[i] = 0;
				else
					target[i] = source[j];
			}
		}

		int[] remap = new int[16];
	}
	
	sealed class ColordSet
	{
		public ColordSet(bool weightColourByAlpha)
		{
            Points = new Vector3d[4,4];
            Weights = new double[4,4];

            WeightColourByAlpha = weightColourByAlpha;
        }

        public void Map(Colord[] colors, int colorIndex, int x, int y, int z, int width, int height)
        {
            //find width and height
            Width = Functions.Min(4, width - x);
            Height = Functions.Min(4, height - y);

            //set points and weights to zero
            Array.Clear(Points, 0, Points.Length);
            Array.Clear(Weights, 0, Weights.Length);

            IsTransparent = false;

            int zindex = colorIndex + z * (height * width);
            for (int iy = 0; iy < Height; ++iy)
            {
                int xyindex = (x) + (y + iy) * width + zindex;

                for (int ix = 0; ix < Width; ++ix)
                {
                    Colord pixel = colors[xyindex++];

                    Points[ix,iy] = new Vector3d(pixel.R, pixel.G, pixel.B);
                    Weights[ix,iy] = WeightColourByAlpha ? pixel.A : 1.0;

                    IsTransparent = pixel.A < 0.5 | IsTransparent;
                }
            }
		}

        public bool IsTransparent { get; private set; }

        public bool WeightColourByAlpha { get; private set; }
        public Vector3d[,] Points { get; private set; }
        public double[,] Weights { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }
	};
}