using System;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Formats
{
    public sealed class BC2 : Format
    {
        readonly Options Options;
        const int BlockSize = 16;

        public BC2(Options options)
            : base("BC2_UNorm", new Colord(0.0, 0.0, 0.0, 0.0), new Colord(1.0, 1.0, 1.0, 1.0), true, true)
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

        public override void GetBytes(Colord[] source, int index, int width, int height, System.IO.Stream destination, int rowPitch, int slicePitch, Boxi sourceBoxi, Point3i destinationPoint)
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

            Colord[] codes = new Colord[4];
            byte[] block = new byte[BlockSize];

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

                        // decompress the block
                        ulong alphas = BitConverter.ToUInt64(block, 0);
                        int color0 = BitConverter.ToUInt16(block, 8);
                        int color1 = BitConverter.ToUInt16(block, 10);
                        uint indices = BitConverter.ToUInt32(block, 12);

                        // unpack the endpoints
                        codes[0] = Colord.UnpackBGR(5, 6, 5, color0);
                        codes[1] = Colord.UnpackBGR(5, 6, 5, color1);

                        // generate the midpoints
                        codes[2] = Numerics.Color.Lerp(codes[0], codes[1], 1.0 / 3.0);
                        codes[3] = Numerics.Color.Lerp(codes[0], codes[1], 2.0 / 3.0);

                        int bwidth = Functions.Min(4, width - (destinationPoint.X + x));
                        int bheight = Functions.Min(4, height - (destinationPoint.Y + y));

                        for (int y2 = 0; y2 < bheight; ++y2)
                        {
                            int xyindex = (destinationPoint.X + x) + (destinationPoint.Y + y + y2) * width + zindex;

                            for (int x2 = 0; x2 < bwidth; ++x2)
                            {
                                uint codeIndex = (indices >> ((x2 + (y2 * 4)) << 1) & 3);
                                ulong alpha = (alphas >> ((x2 + (y2 * 4)) << 2)) & 15;

                                destination[xyindex++] = new Numerics.Colord(
                                    codes[codeIndex].R,
                                    codes[codeIndex].G,
                                    codes[codeIndex].B,
                                    (alpha | (alpha << 4)) / 255.0);
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
    }
}
