using System;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Formats
{
    public sealed class BC6U : Format
    {
        readonly Options Options;
        const int BlockSize = 16;

        public BC6U(Options options)
            : base(
                "BC6H_UF16",
                new Colord(MiniFloat.MaxValue16, MiniFloat.MaxValue16, MiniFloat.MaxValue16, 0.0),
                new Colord(0.0, 0.0, 0.0, 0.0),
                true, true)
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
            throw new NotImplementedException();
        }
    }

    public sealed class BC6S : Format
    {
        readonly Options Options;
        const int BlockSize = 16;

        public BC6S(Options options)
            : base(
                "BC6H_SF16",
                new Colord(MiniFloat.MaxValue16, MiniFloat.MaxValue16, MiniFloat.MaxValue16, 0.0),
                new Colord(MiniFloat.MinValue16, MiniFloat.MinValue16, MiniFloat.MinValue16, 0.0),
                true, true)
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
            throw new NotImplementedException();
        }
    }
}
