using System;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Formats
{
    public sealed class R8G8B8 : Format<Vector3b>
    {
        public R8G8B8()
            : base("R8G8B8_UNorm", new Colord(0.0, 0.0, 0.0, 1.0), new Colord(1.0, 1.0, 1.0, 1.0), true)
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
            rowPitch = size.Width * 3; //3 bytes per color
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
                destinationPoint.X * 3 + destinationPoint.Y * rowPitch + destinationPoint.Z * slicePitch,
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

                        destination.WriteByte((byte)(color.R * 255.0));
                        destination.WriteByte((byte)(color.G * 255.0));
                        destination.WriteByte((byte)(color.B * 255.0));
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - sourceBoxi.Width * 3, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }

        public override void GetBytes(
            Vector3b[] source, int index, int width, int height, 
            System.IO.Stream destination, int rowPitch, int slicePitch, 
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            //seek to start
            destination.Seek(
                destinationPoint.X * 3 + destinationPoint.Y * rowPitch + destinationPoint.Z * slicePitch,
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
                        Vector3b color = source[xyindex++];

                        destination.WriteByte(color.X);
                        destination.WriteByte(color.Y);
                        destination.WriteByte(color.Z);
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - sourceBoxi.Width * 3, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }

        public override Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch)
        {
            return new Size3i(
                rowPitch / 3,
                slicePitch / 3,
                byteCount / slicePitch);
        }

        public override void GetColords(
            System.IO.Stream source, int rowPitch, int slicePitch, 
            Colord[] destination, int index, int width, int height, 
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            //seek to start
            source.Seek(
                sourceBoxi.X * 3 + sourceBoxi.Y * rowPitch + sourceBoxi.Z * slicePitch,
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
                        int r = source.ReadByte();
                        int g = source.ReadByte();
                        int b = source.ReadByte();

                        if ((r | g | b) < 0)
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Colord(
                            r / 255.0,
                            g / 255.0,
                            b / 255.0);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - sourceBoxi.Width * 3, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }

        public override void GetData(
            System.IO.Stream source, int rowPitch, int slicePitch,
            Vector3b[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            //seek to start
            source.Seek(
                sourceBoxi.X * 3 + sourceBoxi.Y * rowPitch + sourceBoxi.Z * slicePitch,
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
                        int r = source.ReadByte();
                        int g = source.ReadByte();
                        int b = source.ReadByte();

                        if ((r | g | b) < 0)
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Vector3b((byte)r, (byte)g, (byte)b);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - sourceBoxi.Width * 3, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }
    }
}