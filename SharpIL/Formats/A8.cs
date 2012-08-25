using System;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Formats
{
    public sealed class A8 : Format<Byte>
    {
        public A8()
            : base("A8_UNorm", new Colord(0.0, 0.0, 0.0, 0.0), new Colord(0.0, 0.0, 0.0, 1.0), true)
        { }

        public override Size3i GetPhysicalSize(Size3i size)
        {
            return new Size3i(
                Functions.Max(size.Width, 1),
                Functions.Max(size.Height, 1),
                Functions.Max(size.Depth, 1));
        }

        public override int GetByteCount(Size3i size, out int rowPitch, out int slicePitch)
        {
            rowPitch = size.Width; //1 byte per color
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

                        destination.WriteByte((byte)(color.A * 255.0));
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - sourceBoxi.Width, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }

        public override void GetBytes(
            byte[] source, int index, int width, int height, 
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
                        destination.WriteByte(source[xyindex++]);
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - sourceBoxi.Width, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }

        public override Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch)
        {
            return new Size3i(
                rowPitch,
                slicePitch,
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
                        int a = source.ReadByte();

                        if (a < 0)
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Colord(
                            0.0, 0.0, 0.0,
                            a / 255.0);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - sourceBoxi.Width, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }

        public override void GetData(
            System.IO.Stream source, int rowPitch, int slicePitch, 
            byte[] destination, int index, int width, int height, 
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
                        int a = source.ReadByte();

                        if (a < 0)
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = (byte)a;
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - sourceBoxi.Width, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }
    }
}
