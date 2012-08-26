using System;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Formats
{
    public sealed class B5G6R5UNorm : Format<ushort>
    {
        public B5G6R5UNorm()
            : base("B5G6R5_UNorm", new Colord(0.0, 0.0, 0.0), new Colord(1.0, 1.0, 1.0), true)
        {
        }

        public override Size3i GetPhysicalSize(Size3i size)
        {
            return new Size3i(
                  Functions.Max(size.Width, 1),
                  Functions.Max(size.Height, 1),
                  Functions.Max(size.Depth, 1));
        }

        public override int GetByteCount(Size3i size, out int rowPitch, out int slicePitch)
        {
            rowPitch = size.Width * 2; //2 bytes per color
            slicePitch = rowPitch * size.Height;
            return slicePitch * size.Depth;
        }

        public override void GetBytes(
            Colord[] source, int index, int width, int height, 
            System.IO.Stream destination, int rowPitch, int slicePitch, 
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            //seek to start
            destination.Seek(
                destinationPoint.X + destinationPoint.Y * rowPitch + destinationPoint.Z * slicePitch,
                System.IO.SeekOrigin.Current);

            for (int z = 0; z < sourceBoxi.Depth; ++z)
            {
                int zindex = index + (sourceBoxi.Z + z) * (height * width);

                for (int y = 0; y < sourceBoxi.Height; ++y)
                {
                    int xyindex = sourceBoxi.X + (sourceBoxi.Y + y) * width + zindex;

                    //write scan line
                    for (int x = 0; x < sourceBoxi.Width; ++x)
                    {
                        Numerics.Colord color = source[xyindex++];

                        int b = (int)(color.B * 31.0);
                        int g = (int)(color.G * 63.0);
                        int r = (int)(color.R * 31.0);

                        int bgr = (r << 11) | ((g & 63) << 5) | (b & 31);

                        destination.WriteByte((byte)bgr);
                        destination.WriteByte((byte)(bgr >> 8));
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - (sourceBoxi.Width * 2), System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - (sourceBoxi.Height * rowPitch), System.IO.SeekOrigin.Current);
            }
        }

        public override void GetBytes(ushort[] source, int index, int width, int height, System.IO.Stream destination, int rowPitch, int slicePitch, Boxi sourceBoxi, Point3i destinationPoint)
        {
            //seek to start
            destination.Seek(
                destinationPoint.X + destinationPoint.Y * rowPitch + destinationPoint.Z * slicePitch,
                System.IO.SeekOrigin.Current);

            for (int z = 0; z < sourceBoxi.Depth; ++z)
            {
                int zindex = index + (sourceBoxi.Z + z) * (height * width);

                for (int y = 0; y < sourceBoxi.Height; ++y)
                {
                    int xyindex = sourceBoxi.X + (sourceBoxi.Y + y) * width + zindex;

                    //write scan line
                    for (int x = 0; x < sourceBoxi.Width; ++x)
                    {
                        ushort color = source[xyindex++];

                        destination.WriteByte((byte)color);
                        destination.WriteByte((byte)(color >> 8));
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - (sourceBoxi.Width * 2), System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - (sourceBoxi.Height * rowPitch), System.IO.SeekOrigin.Current);
            }
        }

        public override Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch)
        {
            return new Size3i(
                rowPitch / 2,
                slicePitch / rowPitch,
                byteCount / slicePitch);
        }

        public override void GetColords(
            System.IO.Stream source, int rowPitch, int slicePitch, 
            Colord[] destination, int index, int width, int height, 
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            //seek to start
            source.Seek(
                sourceBoxi.X + sourceBoxi.Y * rowPitch + sourceBoxi.Z * slicePitch,
                System.IO.SeekOrigin.Current);

            for (int z = 0; z < sourceBoxi.Depth; ++z)
            {
                int zindex = index + (destinationPoint.Z + z) * (height * width);

                for (int y = 0; y < sourceBoxi.Height; ++y)
                {
                    int xyindex = destinationPoint.X + (destinationPoint.Y + y) * width + zindex;

                    //write scan line
                    for (int x = 0; x < sourceBoxi.Width; ++x)
                    {
                        int bgr = source.ReadByte() | (source.ReadByte() << 8);

                        double b = (bgr & 31) / 31.0;
                        double g = ((bgr >> 5) & 63) / 63.0;
                        double r = (bgr >> 11) / 31.0;

                        destination[xyindex++] = new Numerics.Colord(r, g, b);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - (sourceBoxi.Width * 2), System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - (sourceBoxi.Height * rowPitch), System.IO.SeekOrigin.Current);
            }
        }

        public override void GetData(
            System.IO.Stream source, int rowPitch, int slicePitch, 
            ushort[] destination, int index, int width, int height, 
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            //seek to start
            source.Seek(
                sourceBoxi.X + sourceBoxi.Y * rowPitch + sourceBoxi.Z * slicePitch,
                System.IO.SeekOrigin.Current);

            for (int z = 0; z < sourceBoxi.Depth; ++z)
            {
                int zindex = index + (destinationPoint.Z + z) * (height * width);

                for (int y = 0; y < sourceBoxi.Height; ++y)
                {
                    int xyindex = destinationPoint.X + (destinationPoint.Y + y) * width + zindex;

                    //write scan line
                    for (int x = 0; x < sourceBoxi.Width; ++x)
                    {
                        destination[xyindex++] = (ushort)(source.ReadByte() | (source.ReadByte() << 8));
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - (sourceBoxi.Width * 2), System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - (sourceBoxi.Height * rowPitch), System.IO.SeekOrigin.Current);
            }
        }
    }

    public sealed class B5G5R5A1UNorm : Format<ushort>
    {
        public B5G5R5A1UNorm()
            : base("B5G5R5A1_UNorm", new Colord(0.0, 0.0, 0.0, 0.0), new Colord(1.0, 1.0, 1.0, 1.0), true)
        {
        }

        public override Size3i GetPhysicalSize(Size3i size)
        {
            return new Size3i(
                  Functions.Max(size.Width, 1),
                  Functions.Max(size.Height, 1),
                  Functions.Max(size.Depth, 1));
        }

        public override int GetByteCount(Size3i size, out int rowPitch, out int slicePitch)
        {
            rowPitch = size.Width * 2; //2 bytes per color
            slicePitch = rowPitch * size.Height;
            return slicePitch * size.Depth;
        }

        public override void GetBytes(
            Colord[] source, int index, int width, int height,
            System.IO.Stream destination, int rowPitch, int slicePitch, 
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            //seek to start
            destination.Seek(
                destinationPoint.X + destinationPoint.Y * rowPitch + destinationPoint.Z * slicePitch,
                System.IO.SeekOrigin.Current);

            for (int z = 0; z < sourceBoxi.Depth; ++z)
            {
                int zindex = index + (sourceBoxi.Z + z) * (height * width);

                for (int y = 0; y < sourceBoxi.Height; ++y)
                {
                    int xyindex = sourceBoxi.X + (sourceBoxi.Y + y) * width + zindex;

                    //write scan line
                    for (int x = 0; x < sourceBoxi.Width; ++x)
                    {
                        Numerics.Colord color = source[xyindex++];

                        int b = (int)(color.B * 31.0);
                        int g = (int)(color.G * 31.0);
                        int r = (int)(color.R * 31.0);
                        int a = (int)(color.A * 1.0);
                        //B5G5R5A1UNorm
                        int bgra = (b & 31) | ((g & 31) << 5) | ((r & 31) << 10) | (a << 15);

                        destination.WriteByte((byte)bgra);
                        destination.WriteByte((byte)(bgra >> 8));
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - (sourceBoxi.Width * 2), System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - (sourceBoxi.Height * rowPitch), System.IO.SeekOrigin.Current);
            }
        }

        public override void GetBytes(
            ushort[] source, int index, int width, int height, 
            System.IO.Stream destination, int rowPitch, int slicePitch, 
            Boxi sourceBoxi, Point3i destinationPoint)
        {//seek to start
            destination.Seek(
                destinationPoint.X + destinationPoint.Y * rowPitch + destinationPoint.Z * slicePitch,
                System.IO.SeekOrigin.Current);

            for (int z = 0; z < sourceBoxi.Depth; ++z)
            {
                int zindex = index + (sourceBoxi.Z + z) * (height * width);

                for (int y = 0; y < sourceBoxi.Height; ++y)
                {
                    int xyindex = sourceBoxi.X + (sourceBoxi.Y + y) * width + zindex;

                    //write scan line
                    for (int x = 0; x < sourceBoxi.Width; ++x)
                    {
                        int bgra = source[xyindex++];

                        destination.WriteByte((byte)bgra);
                        destination.WriteByte((byte)(bgra >> 8));
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - (sourceBoxi.Width * 2), System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - (sourceBoxi.Height * rowPitch), System.IO.SeekOrigin.Current);
            }
        }

        public override Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch)
        {
            return new Size3i(
                rowPitch / 2,
                slicePitch / rowPitch,
                byteCount / slicePitch);
        }

        public override void GetColords(
            System.IO.Stream source, int rowPitch, int slicePitch, 
            Colord[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            //seek to start
            source.Seek(
                sourceBoxi.X + sourceBoxi.Y * rowPitch + sourceBoxi.Z * slicePitch,
                System.IO.SeekOrigin.Current);

            for (int z = 0; z < sourceBoxi.Depth; ++z)
            {
                int zindex = index + (destinationPoint.Z + z) * (height * width);

                for (int y = 0; y < sourceBoxi.Height; ++y)
                {
                    int xyindex = destinationPoint.X + (destinationPoint.Y + y) * width + zindex;

                    //write scan line
                    for (int x = 0; x < sourceBoxi.Width; ++x)
                    {
                        int bgra = source.ReadByte() | (source.ReadByte() << 8);

                        double b = (bgra & 31) / 31.0;
                        double g = ((bgra >> 5) & 31) / 31.0;
                        double r = ((bgra >> 10) & 31) / 31.0;
                        double a = (bgra >> 15) / 1.0;

                        destination[xyindex++] = new Numerics.Colord(r, g, b, a);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - (sourceBoxi.Width * 2), System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - (sourceBoxi.Height * rowPitch), System.IO.SeekOrigin.Current);
            }
        }

        public override void GetData(
            System.IO.Stream source, int rowPitch, int slicePitch, 
            ushort[] destination, int index, int width, int height, 
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            //seek to start
            source.Seek(
                sourceBoxi.X + sourceBoxi.Y * rowPitch + sourceBoxi.Z * slicePitch,
                System.IO.SeekOrigin.Current);

            for (int z = 0; z < sourceBoxi.Depth; ++z)
            {
                int zindex = index + (destinationPoint.Z + z) * (height * width);

                for (int y = 0; y < sourceBoxi.Height; ++y)
                {
                    int xyindex = destinationPoint.X + (destinationPoint.Y + y) * width + zindex;

                    //write scan line
                    for (int x = 0; x < sourceBoxi.Width; ++x)
                    {
                        destination[xyindex++] = (ushort)(source.ReadByte() | (source.ReadByte() << 8));
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - (sourceBoxi.Width * 2), System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - (sourceBoxi.Height * rowPitch), System.IO.SeekOrigin.Current);
            }
        }
    }
}
