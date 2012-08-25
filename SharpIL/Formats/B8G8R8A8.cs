using System;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Formats
{
    public sealed class B8G8R8A8 : Format<Vector4b>
    {
        public B8G8R8A8()
            : base("B8G8R8A8_UNorm", new Colord(0.0, 0.0, 0.0, 0.0), new Colord(1.0, 1.0, 1.0, 1.0), true)
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
            rowPitch = size.Width * 4; //4 bytes per color
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
                destinationPoint.X * 4 + destinationPoint.Y * rowPitch + destinationPoint.Z * slicePitch,
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

                        destination.WriteByte((byte)(color.B * byte.MaxValue));
                        destination.WriteByte((byte)(color.G * byte.MaxValue));
                        destination.WriteByte((byte)(color.R * byte.MaxValue));
                        destination.WriteByte((byte)(color.A * byte.MaxValue));
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - (sourceBoxi.Width * 4), System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - (sourceBoxi.Height * rowPitch), System.IO.SeekOrigin.Current);
            }
        }

        public override void GetBytes(
            Vector4b[] source, int index, int width, int height, 
            System.IO.Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            //seek to start
            destination.Seek(
                destinationPoint.X * 4 + destinationPoint.Y * rowPitch + destinationPoint.Z * slicePitch,
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
                        Vector4b color = source[xyindex++];

                        destination.WriteByte(color.Z);
                        destination.WriteByte(color.Y);
                        destination.WriteByte(color.X);
                        destination.WriteByte(color.W);
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - (sourceBoxi.Width * 4), System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - (sourceBoxi.Height * rowPitch), System.IO.SeekOrigin.Current);
            }
        }

        public override Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch)
        {
            return new Size3i(
                rowPitch / 4,
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
                sourceBoxi.X * 4 + sourceBoxi.Y * rowPitch + sourceBoxi.Z * slicePitch,
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
                        int b = source.ReadByte();
                        int g = source.ReadByte();
                        int r = source.ReadByte();
                        int a = source.ReadByte();

                        if ((b | g | r | a) < 0)
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Numerics.Colord(
                            r / (double)byte.MaxValue, 
                            g / (double)byte.MaxValue, 
                            b / (double)byte.MaxValue, 
                            a / (double)byte.MaxValue);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - (sourceBoxi.Width * 4), System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - (sourceBoxi.Height * rowPitch), System.IO.SeekOrigin.Current);
            }
        }

        public override void GetData(
            System.IO.Stream source, int rowPitch, int slicePitch, 
            Vector4b[] destination, int index, int width, int height, 
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            //seek to start
            source.Seek(
                sourceBoxi.X * 4 + sourceBoxi.Y * rowPitch + sourceBoxi.Z * slicePitch,
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
                        int b = source.ReadByte();
                        int g = source.ReadByte();
                        int r = source.ReadByte();
                        int a = source.ReadByte();

                        if ((b | g | r | a) < 0)
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Vector4b((byte)r, (byte)g, (byte)b, (byte)a);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - (sourceBoxi.Width * 4), System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - (sourceBoxi.Height * rowPitch), System.IO.SeekOrigin.Current);
            }
        }
    }
}
