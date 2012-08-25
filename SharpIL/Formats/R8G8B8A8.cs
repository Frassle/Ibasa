using System;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Formats
{
    public sealed class R8G8B8A8UInt : Format<Vector4b>
    {
        public R8G8B8A8UInt()
            : base("R8G8B8A8_UInt", new Colord(byte.MinValue, byte.MinValue, byte.MinValue, byte.MinValue), new Colord(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), false)
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

                        destination.WriteByte((byte)color.R);
                        destination.WriteByte((byte)color.G);
                        destination.WriteByte((byte)color.B);
                        destination.WriteByte((byte)color.A);
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
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

                        destination.WriteByte(color.X);
                        destination.WriteByte(color.Y);
                        destination.WriteByte(color.Z);
                        destination.WriteByte(color.W);
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
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
                        int r = source.ReadByte();
                        int g = source.ReadByte();
                        int b = source.ReadByte();
                        int a = source.ReadByte();

                        if ((r | g | b | a) < 0) // if any are negative
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Colord(r, g, b, a);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
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
                        int r = source.ReadByte();
                        int g = source.ReadByte();
                        int b = source.ReadByte();
                        int a = source.ReadByte();

                        if ((r | g | b | a) < 0) // if any are negative
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Vector4b((byte)r, (byte)g, (byte)b, (byte)a);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }
    }

    public sealed class R8G8B8A8Int : Format<Vector4sb>
    {
        public R8G8B8A8Int()
            : base("R8G8B8A8_SInt", new Colord(sbyte.MinValue, sbyte.MinValue, sbyte.MinValue, sbyte.MinValue), new Colord(sbyte.MaxValue, sbyte.MaxValue, sbyte.MaxValue, sbyte.MaxValue), false)
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

        public override void GetBytes(Colord[] source, int index, int width, int height, System.IO.Stream destination, int rowPitch, int slicePitch, Boxi sourceBoxi, Point3i destinationPoint)
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

                        destination.WriteByte((byte)(sbyte)color.R);
                        destination.WriteByte((byte)(sbyte)color.G);
                        destination.WriteByte((byte)(sbyte)color.B);
                        destination.WriteByte((byte)(sbyte)color.A);
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }

        public override void GetBytes(Vector4sb[] source, int index, int width, int height, System.IO.Stream destination, int rowPitch, int slicePitch, Boxi sourceBoxi, Point3i destinationPoint)
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
                        Vector4sb color = source[xyindex++];

                        destination.WriteByte((byte)color.X);
                        destination.WriteByte((byte)color.Y);
                        destination.WriteByte((byte)color.Z);
                        destination.WriteByte((byte)color.W);
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }

        public override Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch)
        {
            return new Size3i(
                rowPitch / 4,
                slicePitch / rowPitch,
                byteCount / slicePitch);
        }

        public override void GetColords(System.IO.Stream source, int rowPitch, int slicePitch, Colord[] destination, int index, int width, int height, Boxi sourceBoxi, Point3i destinationPoint)
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
                        int r = source.ReadByte();
                        int g = source.ReadByte();
                        int b = source.ReadByte();
                        int a = source.ReadByte();

                        if ((r | g | b | a) < 0)
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Colord((sbyte)r, (sbyte)g, (sbyte)b, (sbyte)a);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }

        public override void GetData(
            System.IO.Stream source, int rowPitch, int slicePitch, 
            Vector4sb[] destination, int index, int width, int height, 
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
                        int r = source.ReadByte();
                        int g = source.ReadByte();
                        int b = source.ReadByte();
                        int a = source.ReadByte();

                        if ((r | g | b | a) < 0)
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Vector4sb((sbyte)r, (sbyte)g, (sbyte)b, (sbyte)a);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }
    }

    public sealed class R8G8B8A8 : Format<Vector4b>
    {
        public R8G8B8A8()
            : base("R8G8B8A8_UNorm", new Colord(0.0, 0.0, 0.0, 0.0), new Colord(1.0, 1.0, 1.0, 1.0), true)
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

        public override void GetBytes(Colord[] source, int index, int width, int height, System.IO.Stream destination, int rowPitch, int slicePitch, Boxi sourceBoxi, Point3i destinationPoint)
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

                        destination.WriteByte((byte)(color.R * byte.MaxValue));
                        destination.WriteByte((byte)(color.G * byte.MaxValue));
                        destination.WriteByte((byte)(color.B * byte.MaxValue));
                        destination.WriteByte((byte)(color.A * byte.MaxValue));
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }

        public override void GetBytes(Vector4b[] source, int index, int width, int height, System.IO.Stream destination, int rowPitch, int slicePitch, Boxi sourceBoxi, Point3i destinationPoint)
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

                        destination.WriteByte(color.X);
                        destination.WriteByte(color.Y);
                        destination.WriteByte(color.Z);
                        destination.WriteByte(color.W);
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
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
                        int r = source.ReadByte();
                        int g = source.ReadByte();
                        int b = source.ReadByte();
                        int a = source.ReadByte();

                        if ((r | g | b | a) < 0)
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Colord(r / byte.MaxValue, g / byte.MaxValue, b / byte.MaxValue, a / byte.MaxValue);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
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
                        int r = source.ReadByte();
                        int g = source.ReadByte();
                        int b = source.ReadByte();
                        int a = source.ReadByte();

                        if ((r | g | b | a) < 0)
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Vector4b((byte)r, (byte)g, (byte)b, (byte)a);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }
    }

    public sealed class R8G8B8A8SNorm : Format<Vector4sb>
    {
        public R8G8B8A8SNorm()
            : base("R8G8B8A8_SNorm", new Colord(-1.0, -1.0, -1.0, -1.0), new Colord(1.0, 1.0, 1.0, 1.0), true)
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

                        destination.WriteByte((byte)(sbyte)(color.R * sbyte.MaxValue));
                        destination.WriteByte((byte)(sbyte)(color.G * sbyte.MaxValue));
                        destination.WriteByte((byte)(sbyte)(color.B * sbyte.MaxValue));
                        destination.WriteByte((byte)(sbyte)(color.A * sbyte.MaxValue));
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }

        public override void GetBytes(Vector4sb[] source, int index, int width, int height, System.IO.Stream destination, int rowPitch, int slicePitch, Boxi sourceBoxi, Point3i destinationPoint)
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
                        Vector4sb color = source[xyindex++];

                        destination.WriteByte((byte)color.X);
                        destination.WriteByte((byte)color.Y);
                        destination.WriteByte((byte)color.Z);
                        destination.WriteByte((byte)color.W);
                    }

                    //seek to next scan line
                    destination.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                destination.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
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
                        int r = source.ReadByte();
                        int g = source.ReadByte();
                        int b = source.ReadByte();
                        int a = source.ReadByte();

                        if ((r | g | b | a) < 0)
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Colord(
                            (sbyte)(byte)r / sbyte.MaxValue,
                            (sbyte)(byte)g / sbyte.MaxValue,
                            (sbyte)(byte)b / sbyte.MaxValue,
                            (sbyte)(byte)a / sbyte.MaxValue);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }

        public override void GetData(
            System.IO.Stream source, int rowPitch, int slicePitch, 
            Vector4sb[] destination, int index, int width, int height,
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
                        int r = source.ReadByte();
                        int g = source.ReadByte();
                        int b = source.ReadByte();
                        int a = source.ReadByte();

                        if ((r | g | b | a) < 0)
                            throw new System.IO.EndOfStreamException();

                        destination[xyindex++] = new Vector4sb(
                            (sbyte)(byte)r,
                            (sbyte)(byte)g,
                            (sbyte)(byte)b,
                            (sbyte)(byte)a);
                    }

                    //seek to next scan line
                    source.Seek(rowPitch - sourceBoxi.Width * 4, System.IO.SeekOrigin.Current);
                }

                //seek to next scan slice
                source.Seek(slicePitch - sourceBoxi.Height * rowPitch, System.IO.SeekOrigin.Current);
            }
        }
    }
}
