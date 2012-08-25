using System;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Formats
{
    public sealed class BC5 : Format
    {
        readonly Options Options;
        const int BlockSize = 16;

        public BC5(Options options)
            : base("BC5_UNorm", new Colord(0.0, 0.0, 0.0, 0.0), new Colord(1.0, 1.0, 0.0, 0.0), true, true)
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
            System.IO.Stream destination, int rowPitch, int slicePitch,
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

            double[] rcodes = new double[16];
            double[] gcodes = new double[16];
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
                        int red0 = block[0];
                        int red1 = block[1];
                        ulong rindices =
                            BitConverter.ToUInt16(block, 2) |
                            ((ulong)BitConverter.ToUInt32(block, 4) << 16); //Only 6 bytes
                        int green0 = block[8];
                        int green1 = block[9];
                        ulong gindices =
                            BitConverter.ToUInt16(block, 10) |
                            ((ulong)BitConverter.ToUInt32(block, 12) << 16); //Only 6 bytes

                        // unpack the endpoints
                        rcodes[0] = red0 / 255.0;
                        rcodes[1] = red1 / 255.0;

                        gcodes[0] = green0 / 255.0;
                        gcodes[1] = green1 / 255.0;

                        //generate midpoints
                        if (red0 > red1)
                        {
                            rcodes[2] = (6 * rcodes[0] + 1 * rcodes[1]) / 7.0; // bit code 010
                            rcodes[3] = (5 * rcodes[0] + 2 * rcodes[1]) / 7.0; // bit code 011
                            rcodes[4] = (4 * rcodes[0] + 3 * rcodes[1]) / 7.0; // bit code 100
                            rcodes[5] = (3 * rcodes[0] + 4 * rcodes[1]) / 7.0; // bit code 101
                            rcodes[6] = (2 * rcodes[0] + 5 * rcodes[1]) / 7.0; // bit code 110
                            rcodes[7] = (1 * rcodes[0] + 6 * rcodes[1]) / 7.0; // bit code 111
                        }
                        else
                        {
                            rcodes[2] = (4 * rcodes[0] + 1 * rcodes[1]) / 5.0; // bit code 010
                            rcodes[3] = (3 * rcodes[0] + 2 * rcodes[1]) / 5.0; // bit code 011
                            rcodes[4] = (2 * rcodes[0] + 3 * rcodes[1]) / 5.0; // bit code 100
                            rcodes[5] = (1 * rcodes[0] + 4 * rcodes[1]) / 5.0; // bit code 101
                            rcodes[6] = 0.0;					 // bit code 110
                            rcodes[7] = 1.0;					 // bit code 111
                        }

                        if (green0 > green1)
                        {
                            gcodes[2] = (6 * gcodes[0] + 1 * gcodes[1]) / 7.0; // bit code 010
                            gcodes[3] = (5 * gcodes[0] + 2 * gcodes[1]) / 7.0; // bit code 011
                            gcodes[4] = (4 * gcodes[0] + 3 * gcodes[1]) / 7.0; // bit code 100
                            gcodes[5] = (3 * gcodes[0] + 4 * gcodes[1]) / 7.0; // bit code 101
                            gcodes[6] = (2 * gcodes[0] + 5 * gcodes[1]) / 7.0; // bit code 110
                            gcodes[7] = (1 * gcodes[0] + 6 * gcodes[1]) / 7.0; // bit code 111
                        }
                        else
                        {
                            gcodes[2] = (4 * gcodes[0] + 1 * gcodes[1]) / 5.0; // bit code 010
                            gcodes[3] = (3 * gcodes[0] + 2 * gcodes[1]) / 5.0; // bit code 011
                            gcodes[4] = (2 * gcodes[0] + 3 * gcodes[1]) / 5.0; // bit code 100
                            gcodes[5] = (1 * gcodes[0] + 4 * gcodes[1]) / 5.0; // bit code 101
                            gcodes[6] = 0.0;					 // bit code 110
                            gcodes[7] = 1.0;					 // bit code 111
                        }

                        int bwidth = Functions.Min(4, width - (destinationPoint.X + x));
                        int bheight = Functions.Min(4, height - (destinationPoint.Y + y));

                        for (int y2 = 0; y2 < bheight; ++y2)
                        {
                            int xyindex = (destinationPoint.X + x) + (destinationPoint.Y + y + y2) * width + zindex;

                            for (int x2 = 0; x2 < bwidth; ++x2)
                            {
                                ulong rindex = (rindices >> ((x2 + (y2 * 4)) << 2)) & 7;
                                ulong gindex = (gindices >> ((x2 + (y2 * 4)) << 2)) & 7;
                                destination[xyindex++] = new Colord(
                                    rcodes[rindex],
                                    gcodes[gindex],
                                    0.0);
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

    public sealed class BC5Norm : Format
    {
        readonly Options Options;
        const int BlockSize = 16;

        public BC5Norm(Options options)
            : base("BC5_SNorm", new Colord(-1.0, -1.0, 0.0, 0.0), new Colord(1.0, 1.0, 0.0, 0.0), true, true)
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
            if ((size.Width & 0x3) != 0 || (size.Height & 0x3) != 0)
                throw new ArgumentException("size Width and Height must be a multiple of 4.", "size");
            rowPitch = BlockSize * (size.Width / 4);
            slicePitch = rowPitch * (size.Height / 4);
            return slicePitch * size.Depth;
        }

        public override void GetBytes(
            Colord[] source, int index, int width, int height,
            System.IO.Stream destination, int rowPitch, int slicePitch,
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

            double[] rcodes = new double[16];
            double[] gcodes = new double[16];
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
                        int red0 = (sbyte)block[0];
                        int red1 = (sbyte)block[1];
                        ulong rindices =
                            BitConverter.ToUInt16(block, 2) |
                            ((ulong)BitConverter.ToUInt32(block, 4) << 16); //Only 6 bytes
                        int green0 = (sbyte)block[8];
                        int green1 = (sbyte)block[9];
                        ulong gindices =
                            BitConverter.ToUInt16(block, 10) |
                            ((ulong)BitConverter.ToUInt32(block, 12) << 16); //Only 6 bytes

                        // unpack the endpoints
                        rcodes[0] = red0 / 255.0;
                        rcodes[1] = red1 / 255.0; 
                        
                        gcodes[0] = green0 / 255.0;
                        gcodes[1] = green1 / 255.0;

                        //generate midpoints
                        if (red0 > red1)
                        {
                            rcodes[2] = (6 * rcodes[0] + 1 * rcodes[1]) / 7.0; // bit code 010
                            rcodes[3] = (5 * rcodes[0] + 2 * rcodes[1]) / 7.0; // bit code 011
                            rcodes[4] = (4 * rcodes[0] + 3 * rcodes[1]) / 7.0; // bit code 100
                            rcodes[5] = (3 * rcodes[0] + 4 * rcodes[1]) / 7.0; // bit code 101
                            rcodes[6] = (2 * rcodes[0] + 5 * rcodes[1]) / 7.0; // bit code 110
                            rcodes[7] = (1 * rcodes[0] + 6 * rcodes[1]) / 7.0; // bit code 111
                        }
                        else
                        {
                            rcodes[2] = (4 * rcodes[0] + 1 * rcodes[1]) / 5.0; // bit code 010
                            rcodes[3] = (3 * rcodes[0] + 2 * rcodes[1]) / 5.0; // bit code 011
                            rcodes[4] = (2 * rcodes[0] + 3 * rcodes[1]) / 5.0; // bit code 100
                            rcodes[5] = (1 * rcodes[0] + 4 * rcodes[1]) / 5.0; // bit code 101
                            rcodes[6] = -1.0;					 // bit code 110
                            rcodes[7] = 1.0;					 // bit code 111
                        }

                        if (green0 > green1)
                        {
                            gcodes[2] = (6 * gcodes[0] + 1 * gcodes[1]) / 7.0; // bit code 010
                            gcodes[3] = (5 * gcodes[0] + 2 * gcodes[1]) / 7.0; // bit code 011
                            gcodes[4] = (4 * gcodes[0] + 3 * gcodes[1]) / 7.0; // bit code 100
                            gcodes[5] = (3 * gcodes[0] + 4 * gcodes[1]) / 7.0; // bit code 101
                            gcodes[6] = (2 * gcodes[0] + 5 * gcodes[1]) / 7.0; // bit code 110
                            gcodes[7] = (1 * gcodes[0] + 6 * gcodes[1]) / 7.0; // bit code 111
                        }
                        else
                        {
                            gcodes[2] = (4 * gcodes[0] + 1 * gcodes[1]) / 5.0; // bit code 010
                            gcodes[3] = (3 * gcodes[0] + 2 * gcodes[1]) / 5.0; // bit code 011
                            gcodes[4] = (2 * gcodes[0] + 3 * gcodes[1]) / 5.0; // bit code 100
                            gcodes[5] = (1 * gcodes[0] + 4 * gcodes[1]) / 5.0; // bit code 101
                            gcodes[6] = -1.0;					 // bit code 110
                            gcodes[7] = 1.0;					 // bit code 111
                        }

                        int bwidth = Functions.Min(4, width - (destinationPoint.X + x));
                        int bheight = Functions.Min(4, height - (destinationPoint.Y + y));

                        for (int y2 = 0; y2 < bheight; ++y2)
                        {
                            int xyindex = (destinationPoint.X + x) + (destinationPoint.Y + y + y2) * width + zindex;

                            for (int x2 = 0; x2 < bwidth; ++x2)
                            {
                                ulong rindex = (rindices >> ((x2 + (y2 * 4)) << 2)) & 7;
                                ulong gindex = (gindices >> ((x2 + (y2 * 4)) << 2)) & 7;
                                destination[xyindex++] = new Colord(
                                    rcodes[rindex],
                                    gcodes[gindex],
                                    0.0);
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
