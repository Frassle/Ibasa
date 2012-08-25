using System;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Formats
{
    public sealed class BC7 : Format
    {
        readonly Options Options;
        const int BlockSize = 16;

        public BC7(Options options)
            : base("BC7_UNorm", new Colord(0.0, 0.0, 0.0, 0.0), new Colord(1.0, 1.0, 1.0, 1.0), true, true)
        {
            Options = options;
        }

        public override Size3i GetPhysicalSize(Size3i size)
        {
            return new Size3i(
                Functions.Max((size.Width + 3) & ~3, 4), //& ~3 = mask of 2 low bits, makes it multiple of 4, rounded up
                Functions.Max((size.Height + 3) & ~3, 4), //5 -> 8, 3 -> 4, 7 -> 8
                Functions.Max(size.Depth, 1));
        }

        public override int GetByteCount(Size3i size, out int rowPitch, out int slicePitch)
        {
            rowPitch = BlockSize * ((size.Width + 3) / 4);
            slicePitch = rowPitch * ((size.Height + 3) / 4);
            return slicePitch * size.Depth;
        }

        public override void GetBytes(
            Colord[] source, int index, int width, int height, 
            Stream destination, int rowPitch, int slicePitch, 
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }
        
        public override Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch)
        {
            int blocksPerRow = rowPitch / BlockSize;
            int blocksPerSlice = slicePitch / rowPitch;
            int slices = byteCount / slicePitch;

            return new Size3i(
                blocksPerRow * 4,
                blocksPerSlice * 4,
                slices);
        }

        public override void GetColords(
           System.IO.Stream source, int rowPitch, int slicePitch,
           Colord[] destination, int index, int width, int height,
           Boxi sourceBoxi, Point3i destinationPoint)
        {
            if ((sourceBoxi.X & 0x3) != 0 || (sourceBoxi.Y & 0x3) != 0)
                throw new ArgumentException("sourceBoxi X and Y must be a multiple of 4.", "sourceBoxi");

            //seek to start
            source.Seek(
                (sourceBoxi.X / 4) * BlockSize + (sourceBoxi.Y / 4) * rowPitch + sourceBoxi.Z * slicePitch,
                System.IO.SeekOrigin.Current);

            byte[] block = new byte[BlockSize];
            Colord[] pixels = new Colord[16];

            // loop over blocks
            for (int z = 0; z < sourceBoxi.Depth; ++z)
            {
                int zindex = index + (destinationPoint.Z + z) * (height * width);

                for (int y = 0; y < sourceBoxi.Height; y += 4)
                {
                    //read scan line
                    for (int x = 0; x < sourceBoxi.Width; x += 4)
                    {
                        //read block
                        int read = 0;
                        while (read < BlockSize)
                        {
                            int bytes = source.Read(block, read, BlockSize - read);
                            if (bytes == 0)
                                throw new System.IO.EndOfStreamException();
                            read += bytes;
                        }

                        int mode = (block[0] & (-block[0]));

                        switch (mode)
                        {
                            case 1:
                                DecodeMode0(block, pixels); break;
                            case 2:
                                DecodeMode1(block, pixels); break;
                            case 4:
                                DecodeMode2(block, pixels); break;
                            case 8:
                                DecodeMode3(block, pixels); break;
                            case 16:
                                DecodeMode4(block, pixels); break;
                            case 32:
                                DecodeMode5(block, pixels); break;
                            case 64:
                                DecodeMode6(block, pixels); break;
                            case 128:
                                DecodeMode7(block, pixels); break;
                        }

                        int bwidth = Functions.Min(4, width - (destinationPoint.X + x));
                        int bheight = Functions.Min(4, height - (destinationPoint.Y + y));

                        for (int y2 = 0; y2 < bheight; ++y2)
                        {
                            int xyindex = (destinationPoint.X + x) + (destinationPoint.Y + y + y2) * width + zindex;
                            int xy2index = y2 * 4;

                            for (int x2 = 0; x2 < bwidth; ++x2)
                            {
                                destination[xyindex++] = pixels[xy2index++];
                            }
                        }
                    }
                    //seek to next scan line
                    source.Seek(rowPitch - ((sourceBoxi.Width / 4) * BlockSize), System.IO.SeekOrigin.Current);
                }
                //seek to next scan slice
                source.Seek(slicePitch - ((sourceBoxi.Height / 4) * (sourceBoxi.Width / 4) * BlockSize), System.IO.SeekOrigin.Current);
            }
        }

        #region Tables
        private static readonly ushort[] Table2 = new ushort[] 
        {
            0xCCCC,
            0x8888,
            0xEEEE,
            0xECC8,
            0xC880,
            0xFEEC,
            0xFEC8,
            0xEC80,
            0xC800,
            0xFFEC,
            0xFE80,
            0xE800,
            0xFFE8,
            0xFF00,
            0xFFF0,
            0xF000,
            0xF710,
            0x808E,
            0xF100,
            0x88CE,
            0x808C,
            0xF310,
            0xB100,
            0x8CCE,
            0x888C,
            0xB110,
            0xE666,
            0xB66C,
            0x97E8,
            0x8FF0,
            0xF18E,
            0xB99C,
            0xAAAA,
            0xF0F0,
            0xDA5A,
            0xB3CC,
            0xBC3C,
            0xD5AA,
            0x9696,
            0xA55A,
            0xF3CE,
            0x93C8,
            0xB24C,
            0xBBDC,
            0xE996,
            0xC33C,
            0x9966,
            0x8660,
            0x8272,
            0x84E4,
            0xCE40,
            0xA720,
            0xC936,
            0x936C,
            0xB9C6,
            0xE39C,
            0x9336,
            0x9CC6,
            0x817E,
            0xE718,
            0xCCF0,
            0x8FCC,
            0xF744,
            0xEE22,
        };

        private static readonly uint[] Table3 = new uint[]
        {
            0xAA685050,
            0xAA5A5040,
            0x9A5A4200,
            0x9450A0A8,
            0xA5A50000,
            0xA0A05050,
            0x9555A0A0,
            0x9A5A5050,
            0xAA550000,
            0xAA555500,
            0xAAAA5500,
            0x90909090,
            0x94949494,
            0xA4A4A4A4,
            0xA9A59450,
            0xAA0A4250,
            0xA5945040,
            0x8A425054,
            0xA5A5A500,
            0x95A0A0A0,
            0xA8A85454,
            0xAA6A4040,
            0xA4A45000,
            0x9A1A0500,
            0x8050A4A4,
            0xAAA59090,
            0x94696914,
            0xA9691400,
            0xA08585A0,
            0xAA821414,
            0x90A4A450,
            0xAA5A0200,
            0xA9A58000,
            0x9090A0A8,
            0xA8A09050,
            0xA4242424,
            0x80AA5500,
            0xA4924924,
            0xA4499224,
            0x90A50A50,
            0x900AA550,
            0xAAAA4444,
            0xA6660000,
            0xA5A0A5A0,
            0x90A050A0,
            0xA9286928,
            0x84AAAA44,
            0xA6666600,
            0xAA444444,
            0x94A854A8,
            0x95809580,
            0x96969600,
            0xA85454A8,
            0x80959580,
            0xAA141414,
            0x96960000,
            0xAAAA1414,
            0xA05050A0,
            0xA0A5A5A0,
            0x96000000,
            0x80804080,
            0xA9A8A9A8,
            0xAAAAAA44,
            0x2A4A5254,
        };
        #endregion

        #region Interpolate
        static readonly int[] Weights2 = new int[] { 0, 21, 43, 64 };
        static readonly int[] Weights3 = new int[] { 0, 9, 18, 27, 37, 46, 55, 64 };
        static readonly int[] Weights4 = new int[] { 0, 4, 9, 13, 17, 21, 26, 30, 34, 38, 43, 47, 51, 55, 60, 64 };

        static unsafe Colord InterpolateColord(int weight, int* codes, int subsetIndex)
        {
            int r0 = codes[(subsetIndex * 6) + 0];
            int g0 = codes[(subsetIndex * 6) + 1];
            int b0 = codes[(subsetIndex * 6) + 2];
            int r1 = codes[(subsetIndex * 6) + 3];
            int g1 = codes[(subsetIndex * 6) + 4];
            int b1 = codes[(subsetIndex * 6) + 5];

            int r = ((64 - weight) * r0 + weight * r1 + 32) >> 6;
            int g = ((64 - weight) * g0 + weight * g1 + 32) >> 6;
            int b = ((64 - weight) * b0 + weight * b1 + 32) >> 6;

            return new Colord(r / 255.0, g / 255.0, b / 255.0);
        }

        static unsafe double InterpolateAlpha(int weight, int* endpoints)
        {
            int a0 = endpoints[0];
            int a1 = endpoints[1];

            int a = ((64 - weight) * a0 + weight * a1 + 32) >> 6;

            return a / 255.0;
        }
        #endregion
        
        private unsafe void DecodeMode0(byte[] block, Colord[] pixels)
        {
            //Number of subsets in each partition: 3
            //Partition bits: 4
            //Rotation bits: 0
            //Index selection bits: 0
            //Colord bits: 4
            //Alpha bits: 0
            //Endpoint P-bits: 1
            //Shared P-bits: 0
            //Index bits per element: 3
            //Secondary index bits per element: 0

            // [76543210] counter
            // [rrrppppm] 0
            // [rrrrrrrr] 1
            // [rrrrrrrr] 2
            // [gggrrrrr] 3
            // [gggggggg] 4
            // [gggggggg] 5
            // [bbbggggg] 6
            // [bbbbbbbb] 7
            // [bbbbbbbb] 8
            // [pppbbbbb] 9
            // [xxxxxppp] A
            // [xxxxxxxx] B
            // [xxxxxxxx] C
            // [xxxxxxxx] D
            // [xxxxxxxx] E
            // [xxxxxxxx] F

            int partition = (block[0] >> 1) & 0x0F;

            int* codes = stackalloc int[18];

            codes[0] = ((block[0] >> 1) | (block[1] << 7)) & 0xF0 | ((block[9] >> 2) & 0x08);
            codes[1] = ((block[3] >> 1) | (block[4] << 7)) & 0xF0 | ((block[9] >> 2) & 0x08);
            codes[2] = ((block[6] >> 1) | (block[7] << 7)) & 0xF0 | ((block[9] >> 2) & 0x08);
            codes[3] = (block[1] << 3) & 0xF0 | ((block[9] >> 3) & 0x08);
            codes[4] = (block[4] << 3) & 0xF0 | ((block[9] >> 3) & 0x08);
            codes[5] = (block[7] << 3) & 0xF0 | ((block[9] >> 3) & 0x08);

            codes[6] = ((block[1] >> 1) | (block[2] << 7)) & 0xF0 | ((block[9] >> 4) & 0x08);
            codes[7] = ((block[4] >> 1) | (block[5] << 7)) & 0xF0 | ((block[9] >> 4) & 0x08);
            codes[8] = ((block[7] >> 1) | (block[8] << 7)) & 0xF0 | ((block[9] >> 4) & 0x08);
            codes[9] = (block[2] << 3) & 0xF0 | ((block[10] << 3) & 0x08);
            codes[10] = (block[5] << 3) & 0xF0 | ((block[10] << 3) & 0x08);
            codes[11] = (block[8] << 3) & 0xF0 | ((block[10] << 3) & 0x08);

            codes[12] = ((block[2] >> 1) | (block[3] << 7)) & 0xF0 | ((block[10] << 2) & 0x08);
            codes[13] = ((block[5] >> 1) | (block[6] << 7)) & 0xF0 | ((block[10] << 2) & 0x08);
            codes[14] = ((block[8] >> 1) | (block[9] << 7)) & 0xF0 | ((block[10] << 2) & 0x08);
            codes[15] = (block[3] << 3) & 0xF0 | ((block[10] << 1) & 0x08);
            codes[16] = (block[6] << 3) & 0xF0 | ((block[10] << 1) & 0x08);
            codes[17] = (block[9] << 3) & 0xF0 | ((block[10] << 1) & 0x08);

            codes[0] |= (codes[0] >> 5);
            codes[1] |= (codes[1] >> 5);
            codes[2] |= (codes[2] >> 5);
            codes[3] |= (codes[3] >> 5);
            codes[4] |= (codes[4] >> 5);
            codes[5] |= (codes[5] >> 5);
            codes[6] |= (codes[6] >> 5);
            codes[7] |= (codes[7] >> 5);
            codes[8] |= (codes[8] >> 5);
            codes[9] |= (codes[9] >> 5);
            codes[10] |= (codes[10] >> 5);
            codes[11] |= (codes[11] >> 5);
            codes[12] |= (codes[12] >> 5);
            codes[13] |= (codes[13] >> 5);
            codes[14] |= (codes[14] >> 5);
            codes[15] |= (codes[15] >> 5);
            codes[16] |= (codes[16] >> 5);
            codes[17] |= (codes[17] >> 5);

            for (int i = 0; i < 16; ++i)
            {
                int colorIndex = 0;

                //64 <= partition < 64 + 16
                //if (i == 0)
                //    colorIndex = (block.z >> 19) & 0x03;
                //else if (i < candidateFixUpIndex1DOrdered[partition][0])
                //{
                //    if (i < 4)
                //        colorIndex = (block.z >> (i * 3 + 18)) & 0x07;
                //    else if (i == 4)
                //        colorIndex = ((block.z >> (i * 3 + 18)) & 0x03) | ((block.w << 2) & 0x04);
                //    else
                //        colorIndex = (block.w >> (i * 3 - 14)) & 0x07;
                //}
                //else if (i == candidateFixUpIndex1DOrdered[partition][0])
                //{
                //    if (i <= 4)
                //        colorIndex = (block.z >> (i * 3 + 18)) & 0x03;
                //    else
                //        colorIndex = (block.w >> (i * 3 - 14)) & 0x03;
                //}
                //else if (i < candidateFixUpIndex1DOrdered[partition][1])
                //{
                //    if (i <= 4)
                //        colorIndex = (block.z >> (i * 3 + 17)) & 0x07;
                //    else
                //        colorIndex = (block.w >> (i * 3 - 15)) & 0x07;
                //}
                //else if (i == candidateFixUpIndex1DOrdered[partition][1]) //i >= 8
                //    colorIndex = (block.w >> (i * 3 - 15)) & 0x03;
                //else //i >= 9
                //    colorIndex = (block.w >> (i * 3 - 16)) & 0x07;

                //int subsetIndex = (candidateSectionCompressed[partition] >> (30 - i * 2)) & 0x03;
                int subsetIndex = 0;
                pixels[i] = InterpolateColord(Weights3[colorIndex], codes, subsetIndex);
            }
        }
        private void DecodeMode1(byte[] block, Colord[] pixels)
        {
        }
        private void DecodeMode2(byte[] block, Colord[] pixels)
        {
        }
        private void DecodeMode3(byte[] block, Colord[] pixels)
        {
        }
        private void DecodeMode4(byte[] block, Colord[] pixels)
        {
        }
        private void DecodeMode5(byte[] block, Colord[] pixels)
        {
        }
        private void DecodeMode6(byte[] block, Colord[] pixels)
        {
        }
        private void DecodeMode7(byte[] block, Colord[] pixels)
        {
        }
    }
}