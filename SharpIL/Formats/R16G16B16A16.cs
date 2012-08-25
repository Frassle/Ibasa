using System;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;
namespace Ibasa.SharpIL.Formats
{
    public sealed class R16G16B16A16Float : Format<Vector4h>
    {
        public R16G16B16A16Float()
            : base("NotImplemented", Colord.Black, Colord.White, false)
        {
        }

        public override Size3i GetPhysicalSize(Size3i size)
        {
            throw new NotImplementedException();
        }

        public override int GetByteCount(Size3i size, out int rowPitch, out int slicePitch)
        {
            throw new NotImplementedException();
        }

        public override void GetBytes(
            Colord[] source, int index, int width, int height,
            Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override void GetBytes(
            Vector4h[] source, int index, int width, int height,
            Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch)
        {
            throw new NotImplementedException();
        }

        public override void GetColords(
            Stream source, int rowPitch, int slicePitch,
            Colord[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override void GetData(
            Stream source, int rowPitch, int slicePitch,
            Vector4h[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        //public override string Name
        //{
        //    get { return "R16G16B16A16_Float"; }
        //}

        //public override Numerics.Colord MaxValue
        //{
        //    get { return new Numerics.Colord(Numerics.MiniFloat.MaxValue16, Numerics.MiniFloat.MaxValue16, Numerics.MiniFloat.MaxValue16, Numerics.MiniFloat.MaxValue16); }
        //}
        //public override Numerics.Colord MinValue
        //{
        //    get { return new Numerics.Colord(Numerics.MiniFloat.MinValue16, Numerics.MiniFloat.MinValue16, Numerics.MiniFloat.MinValue16, Numerics.MiniFloat.MinValue16); }
        //}
        //public override bool IsNormalized
        //{
        //    get { return false; }
        //}
        //public override int MinWidth
        //{
        //    get { return 1; }
        //}

        //public override int MinHeight
        //{
        //    get { return 1; }
        //}

        //public override int MinDepth
        //{
        //    get { return 1; }
        //}

        //public override void GetValidDimensions(ref int width, ref int height, ref int depth)
        //{
        //    width = Functions.Max(width, 1);
        //    height = Functions.Max(height, 1);
        //    depth = Functions.Max(depth, 1);
        //}

        //public override int GetByteCount(int width, int height, int depth)
        //{
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");

        //    return (width * height * depth) * 8; //8 bytes per color
        //}

        //public override int GetBytes(Numerics.Colord[] colors, int colorIndex, int width, int height, int depth, byte[] bytes, int byteIndex)
        //{
        //    Contract.Requires(colors != null, "colors cannot be null.");
        //    Contract.Requires(colorIndex >= 0, "colorIndex must be zero or greater.");
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");
        //    Contract.Requires(colorIndex + (width * height * depth) <= colors.Length, "colorIndex, width, height and depth must specify a valid range in colors.");
        //    Contract.Requires(bytes != null, "bytes cannot be null.");
        //    Contract.Requires(byteIndex >= 0, "byteIndex must be zero or greater.");
        //    Contract.Requires(byteIndex + GetByteCount(width, height, depth) <= bytes.Length, "byteIndex, width, height and depth must specify a valid range in bytes.");

        //    for (int z = 0; z < depth; ++z)
        //    {
        //        for (int y = 0; y < height; ++y)
        //        {
        //            for (int x = 0; x < width; ++x)
        //            {
        //                Numerics.Colord color = colors[colorIndex + (x + y * width + z * (height*width))];

        //                BitConverter.GetBytes(Numerics.MiniFloat.To16Bit(color.R)).CopyTo(bytes, byteIndex + 0);
        //                BitConverter.GetBytes(Numerics.MiniFloat.To16Bit(color.G)).CopyTo(bytes, byteIndex + 2);
        //                BitConverter.GetBytes(Numerics.MiniFloat.To16Bit(color.B)).CopyTo(bytes, byteIndex + 4);
        //                BitConverter.GetBytes(Numerics.MiniFloat.To16Bit(color.A)).CopyTo(bytes, byteIndex + 6);

        //                byteIndex += 8;
        //            }
        //        }
        //    }

        //    return GetByteCount(width, height, depth);
        //}

        //public override int GetColordCount(int byteCount)
        //{
        //    return byteCount / 8;
        //}

        //public override int GetColords(byte[] bytes, int byteIndex, int width, int height, int depth, Numerics.Colord[] colors, int colorIndex)
        //{
        //    Contract.Requires(bytes != null, "bytes cannot be null.");
        //    Contract.Requires(byteIndex >= 0, "byteIndex must be zero or greater.");
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");
        //    Contract.Requires(byteIndex + GetByteCount(width, height, depth) <= bytes.Length, "byteIndex, width, height and depth must specify a valid range in bytes.");
        //    Contract.Requires(colors != null, "colors cannot be null.");
        //    Contract.Requires(colorIndex >= 0, "colorIndex must be zero or greater.");
        //    Contract.Requires(colorIndex + (width * height * depth) <= colors.Length, "colorIndex, width, height and depth must specify a valid range in colors.");

        //    for (int z = 0; z < depth; ++z)
        //    {
        //        for (int y = 0; y < height; ++y)
        //        {
        //            for (int x = 0; x < width; ++x)
        //            {
        //                colors[colorIndex + (x + y * width + z * (height * width))] = new Numerics.Colord(
        //                    Numerics.MiniFloat.From16Bit(BitConverter.ToInt16(bytes, byteIndex + 0)),
        //                    Numerics.MiniFloat.From16Bit(BitConverter.ToInt16(bytes, byteIndex + 2)),
        //                    Numerics.MiniFloat.From16Bit(BitConverter.ToInt16(bytes, byteIndex + 4)),
        //                    Numerics.MiniFloat.From16Bit(BitConverter.ToInt16(bytes, byteIndex + 6)));

        //                byteIndex += 16;
        //            }
        //        }
        //    }

        //    return (width * height * depth);
        //}
    }

    public sealed class R16G16B16A16UInt : Format<Vector4us>
    {
        public R16G16B16A16UInt()
            : base("NotImplemented", Colord.Black, Colord.White, false)
        {
        }

        public override Size3i GetPhysicalSize(Size3i size)
        {
            throw new NotImplementedException();
        }

        public override int GetByteCount(Size3i size, out int rowPitch, out int slicePitch)
        {
            throw new NotImplementedException();
        }

        public override void GetBytes(
            Colord[] source, int index, int width, int height,
            Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override void GetBytes(
            Vector4us[] source, int index, int width, int height,
            Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch)
        {
            throw new NotImplementedException();
        }

        public override void GetColords(
            Stream source, int rowPitch, int slicePitch,
            Colord[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override void GetData(
            Stream source, int rowPitch, int slicePitch,
            Vector4us[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        //public override string Name
        //{
        //    get { return "R16G16B16A16_UInt"; }
        //}

        //public override Numerics.Colord MaxValue
        //{
        //    get { return new Numerics.Colord(ushort.MaxValue, ushort.MaxValue, ushort.MaxValue, ushort.MaxValue); }
        //}

        //public override Numerics.Colord MinValue
        //{
        //    get { return new Numerics.Colord(ushort.MinValue, ushort.MinValue, ushort.MinValue, ushort.MinValue); }
        //}

        //public override bool IsNormalized
        //{
        //    get { return false; }
        //}

        //public override int MinWidth
        //{
        //    get { return 1; }
        //}

        //public override int MinHeight
        //{
        //    get { return 1; }
        //}

        //public override int MinDepth
        //{
        //    get { return 1; }
        //}

        //public override void GetValidDimensions(ref int width, ref int height, ref int depth)
        //{
        //    width = Functions.Max(width, 1);
        //    height = Functions.Max(height, 1);
        //    depth = Functions.Max(depth, 1);
        //}

        //public override int GetByteCount(int width, int height, int depth)
        //{
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");

        //    return (width * height * depth) * 8; //8 bytes per color
        //}

        //public override int GetBytes(Numerics.Colord[] colors, int colorIndex, int width, int height, int depth, byte[] bytes, int byteIndex)
        //{
        //    Contract.Requires(colors != null, "colors cannot be null.");
        //    Contract.Requires(colorIndex >= 0, "colorIndex must be zero or greater.");
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");
        //    Contract.Requires(colorIndex + (width * height * depth) <= colors.Length, "colorIndex, width, height and depth must specify a valid range in colors.");
        //    Contract.Requires(bytes != null, "bytes cannot be null.");
        //    Contract.Requires(byteIndex >= 0, "byteIndex must be zero or greater.");
        //    Contract.Requires(byteIndex + GetByteCount(width, height, depth) <= bytes.Length, "byteIndex, width, height and depth must specify a valid range in bytes.");

        //    for (int z = 0; z < depth; ++z)
        //    {
        //        for (int y = 0; y < height; ++y)
        //        {
        //            for (int x = 0; x < width; ++x)
        //            {
        //                Numerics.Colord color = colors[colorIndex + (x + y * width + z * (height * width))];

        //                BitConverter.GetBytes((ushort)color.R).CopyTo(bytes, byteIndex + 0);
        //                BitConverter.GetBytes((ushort)color.G).CopyTo(bytes, byteIndex + 2);
        //                BitConverter.GetBytes((ushort)color.B).CopyTo(bytes, byteIndex + 4);
        //                BitConverter.GetBytes((ushort)color.A).CopyTo(bytes, byteIndex + 6);

        //                byteIndex += 8;
        //            }
        //        }
        //    }

        //    return GetByteCount(width, height, depth);
        //}

        //public override int GetColordCount(int byteCount)
        //{
        //    return byteCount / 8;
        //}

        //public override int GetColords(byte[] bytes, int byteIndex, int width, int height, int depth, Numerics.Colord[] colors, int colorIndex)
        //{
        //    Contract.Requires(bytes != null, "bytes cannot be null.");
        //    Contract.Requires(byteIndex >= 0, "byteIndex must be zero or greater.");
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");
        //    Contract.Requires(byteIndex + GetByteCount(width, height, depth) <= bytes.Length, "byteIndex, width, height and depth must specify a valid range in bytes.");
        //    Contract.Requires(colors != null, "colors cannot be null.");
        //    Contract.Requires(colorIndex >= 0, "colorIndex must be zero or greater.");
        //    Contract.Requires(colorIndex + (width * height * depth) <= colors.Length, "colorIndex, width, height and depth must specify a valid range in colors.");

        //    for (int z = 0; z < depth; ++z)
        //    {
        //        for (int y = 0; y < height; ++y)
        //        {
        //            for (int x = 0; x < width; ++x)
        //            {
        //                colors[colorIndex + (x + y * width + z * (height * width))] = new Numerics.Colord(
        //                    BitConverter.ToUInt16(bytes, byteIndex + 0),
        //                    BitConverter.ToUInt16(bytes, byteIndex + 2),
        //                    BitConverter.ToUInt16(bytes, byteIndex + 4),
        //                    BitConverter.ToUInt16(bytes, byteIndex + 6));

        //                byteIndex += 16;
        //            }
        //        }
        //    }

        //    return (width * height * depth);
        //}
    }

    public sealed class R16G16B16A16Int : Format<Vector4s>
    {
        public R16G16B16A16Int()
            : base("NotImplemented", Colord.Black, Colord.White, false)
        {
        }

        public override Size3i GetPhysicalSize(Size3i size)
        {
            throw new NotImplementedException();
        }

        public override int GetByteCount(Size3i size, out int rowPitch, out int slicePitch)
        {
            throw new NotImplementedException();
        }

        public override void GetBytes(
            Colord[] source, int index, int width, int height,
            Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override void GetBytes(
            Vector4s[] source, int index, int width, int height,
            Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch)
        {
            throw new NotImplementedException();
        }

        public override void GetColords(
            Stream source, int rowPitch, int slicePitch,
            Colord[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override void GetData(
            Stream source, int rowPitch, int slicePitch,
            Vector4s[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }
        
        //public override string Name
        //{
        //    get { return "R16G16B16A16_SInt"; }
        //}

        //public override Numerics.Colord MaxValue
        //{
        //    get { return new Numerics.Colord(short.MaxValue, short.MaxValue, short.MaxValue, short.MaxValue); }
        //}

        //public override Numerics.Colord MinValue
        //{
        //    get { return new Numerics.Colord(short.MinValue, short.MinValue, short.MinValue, short.MinValue); }
        //}

        //public override bool IsNormalized
        //{
        //    get { return false; }
        //}

        //public override int MinWidth
        //{
        //    get { return 1; }
        //}

        //public override int MinHeight
        //{
        //    get { return 1; }
        //}

        //public override int MinDepth
        //{
        //    get { return 1; }
        //}

        //public override void GetValidDimensions(ref int width, ref int height, ref int depth)
        //{
        //    width = Functions.Max(width, 1);
        //    height = Functions.Max(height, 1);
        //    depth = Functions.Max(depth, 1);
        //}

        //public override int GetByteCount(int width, int height, int depth)
        //{
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");

        //    return (width * height * depth) * 8; //8 bytes per color
        //}

        //public override int GetBytes(Numerics.Colord[] colors, int colorIndex, int width, int height, int depth, byte[] bytes, int byteIndex)
        //{
        //    Contract.Requires(colors != null, "colors cannot be null.");
        //    Contract.Requires(colorIndex >= 0, "colorIndex must be zero or greater.");
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");
        //    Contract.Requires(colorIndex + (width * height * depth) <= colors.Length, "colorIndex, width, height and depth must specify a valid range in colors.");
        //    Contract.Requires(bytes != null, "bytes cannot be null.");
        //    Contract.Requires(byteIndex >= 0, "byteIndex must be zero or greater.");
        //    Contract.Requires(byteIndex + GetByteCount(width, height, depth) <= bytes.Length, "byteIndex, width, height and depth must specify a valid range in bytes.");

        //    for (int z = 0; z < depth; ++z)
        //    {
        //        for (int y = 0; y < height; ++y)
        //        {
        //            for (int x = 0; x < width; ++x)
        //            {
        //                Numerics.Colord color = colors[colorIndex + (x + y * width + z * (height * width))];

        //                BitConverter.GetBytes((short)color.R).CopyTo(bytes, byteIndex + 0);
        //                BitConverter.GetBytes((short)color.G).CopyTo(bytes, byteIndex + 2);
        //                BitConverter.GetBytes((short)color.B).CopyTo(bytes, byteIndex + 4);
        //                BitConverter.GetBytes((short)color.A).CopyTo(bytes, byteIndex + 6);

        //                byteIndex += 8;
        //            }
        //        }
        //    }

        //    return GetByteCount(width, height, depth);
        //}

        //public override int GetColordCount(int byteCount)
        //{
        //    return byteCount / 8;
        //}

        //public override int GetColords(byte[] bytes, int byteIndex, int width, int height, int depth, Numerics.Colord[] colors, int colorIndex)
        //{
        //    Contract.Requires(bytes != null, "bytes cannot be null.");
        //    Contract.Requires(byteIndex >= 0, "byteIndex must be zero or greater.");
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");
        //    Contract.Requires(byteIndex + GetByteCount(width, height, depth) <= bytes.Length, "byteIndex, width, height and depth must specify a valid range in bytes.");
        //    Contract.Requires(colors != null, "colors cannot be null.");
        //    Contract.Requires(colorIndex >= 0, "colorIndex must be zero or greater.");
        //    Contract.Requires(colorIndex + (width * height * depth) <= colors.Length, "colorIndex, width, height and depth must specify a valid range in colors.");

        //    for (int z = 0; z < depth; ++z)
        //    {
        //        for (int y = 0; y < height; ++y)
        //        {
        //            for (int x = 0; x < width; ++x)
        //            {
        //                colors[colorIndex + (x + y * width + z * (height * width))] = new Numerics.Colord(
        //                    BitConverter.ToInt16(bytes, byteIndex + 0),
        //                    BitConverter.ToInt16(bytes, byteIndex + 2),
        //                    BitConverter.ToInt16(bytes, byteIndex + 4),
        //                    BitConverter.ToInt16(bytes, byteIndex + 6));

        //                byteIndex += 8;
        //            }
        //        }
        //    }

        //    return (width * height * depth);
        //}
    }

    public sealed class R16G16B16A16 : Format<Vector4us>
    {
        public R16G16B16A16()
            : base("NotImplemented", Colord.Black, Colord.White, false)
        {
        }


        public override Size3i GetPhysicalSize(Size3i size)
        {
            throw new NotImplementedException();
        }

        public override int GetByteCount(Size3i size, out int rowPitch, out int slicePitch)
        {
            throw new NotImplementedException();
        }

        public override void GetBytes(
            Colord[] source, int index, int width, int height,
            Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override void GetBytes(
            Vector4us[] source, int index, int width, int height,
            Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch)
        {
            throw new NotImplementedException();
        }

        public override void GetColords(
            Stream source, int rowPitch, int slicePitch,
            Colord[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override void GetData(
            Stream source, int rowPitch, int slicePitch,
            Vector4us[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        //public override string Name
        //{
        //    get { return "R16G16B16A16_UNorm"; }
        //}

        //public override Numerics.Colord MaxValue
        //{
        //    get { return new Numerics.Colord(1.0, 1.0, 1.0, 1.0); }
        //}

        //public override Numerics.Colord MinValue
        //{
        //    get { return new Numerics.Colord(0.0, 0.0, 0.0, 0.0); }
        //}

        //public override bool IsNormalized
        //{
        //    get { return true; }
        //}

        //public override int MinWidth
        //{
        //    get { return 1; }
        //}

        //public override int MinHeight
        //{
        //    get { return 1; }
        //}

        //public override int MinDepth
        //{
        //    get { return 1; }
        //}

        //public override void GetValidDimensions(ref int width, ref int height, ref int depth)
        //{
        //    width = Functions.Max(width, 1);
        //    height = Functions.Max(height, 1);
        //    depth = Functions.Max(depth, 1);
        //}

        //public override int GetByteCount(int width, int height, int depth)
        //{
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");

        //    return (width * height * depth) * 8; //8 bytes per color
        //}

        //public override int GetBytes(Numerics.Colord[] colors, int colorIndex, int width, int height, int depth, byte[] bytes, int byteIndex)
        //{
        //    Contract.Requires(colors != null, "colors cannot be null.");
        //    Contract.Requires(colorIndex >= 0, "colorIndex must be zero or greater.");
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");
        //    Contract.Requires(colorIndex + (width * height * depth) <= colors.Length, "colorIndex, width, height and depth must specify a valid range in colors.");
        //    Contract.Requires(bytes != null, "bytes cannot be null.");
        //    Contract.Requires(byteIndex >= 0, "byteIndex must be zero or greater.");
        //    Contract.Requires(byteIndex + GetByteCount(width, height, depth) <= bytes.Length, "byteIndex, width, height and depth must specify a valid range in bytes.");

        //    for (int z = 0; z < depth; ++z)
        //    {
        //        for (int y = 0; y < height; ++y)
        //        {
        //            for (int x = 0; x < width; ++x)
        //            {
        //                Numerics.Colord color = colors[colorIndex + (x + y * width + z * (height * width))];

        //                BitConverter.GetBytes((ushort)(color.R * ushort.MaxValue)).CopyTo(bytes, byteIndex + 0);
        //                BitConverter.GetBytes((ushort)(color.G * ushort.MaxValue)).CopyTo(bytes, byteIndex + 2);
        //                BitConverter.GetBytes((ushort)(color.B * ushort.MaxValue)).CopyTo(bytes, byteIndex + 4);
        //                BitConverter.GetBytes((ushort)(color.A * ushort.MaxValue)).CopyTo(bytes, byteIndex + 6);

        //                byteIndex += 8;
        //            }
        //        }
        //    }

        //    return GetByteCount(width, height, depth);
        //}

        //public override int GetColordCount(int byteCount)
        //{
        //    return byteCount / 8;
        //}

        //public override int GetColords(byte[] bytes, int byteIndex, int width, int height, int depth, Numerics.Colord[] colors, int colorIndex)
        //{
        //    Contract.Requires(bytes != null, "bytes cannot be null.");
        //    Contract.Requires(byteIndex >= 0, "byteIndex must be zero or greater.");
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");
        //    Contract.Requires(byteIndex + GetByteCount(width, height, depth) <= bytes.Length, "byteIndex, width, height and depth must specify a valid range in bytes.");
        //    Contract.Requires(colors != null, "colors cannot be null.");
        //    Contract.Requires(colorIndex >= 0, "colorIndex must be zero or greater.");
        //    Contract.Requires(colorIndex + (width * height * depth) <= colors.Length, "colorIndex, width, height and depth must specify a valid range in colors.");

        //    for (int z = 0; z < depth; ++z)
        //    {
        //        for (int y = 0; y < height; ++y)
        //        {
        //            for (int x = 0; x < width; ++x)
        //            {
        //                colors[colorIndex + (x + y * width + z * (height * width))] = new Numerics.Colord(
        //                    BitConverter.ToUInt16(bytes, byteIndex + 0) / (double)ushort.MaxValue,
        //                    BitConverter.ToUInt16(bytes, byteIndex + 2) / (double)ushort.MaxValue,
        //                    BitConverter.ToUInt16(bytes, byteIndex + 4) / (double)ushort.MaxValue,
        //                    BitConverter.ToUInt16(bytes, byteIndex + 6) / (double)ushort.MaxValue);

        //                byteIndex += 16;
        //            }
        //        }
        //    }

        //    return (width * height * depth);
        //}
    }

    public sealed class R16G16B16A16Norm : Format<Vector4s>
    {
        public R16G16B16A16Norm()
            : base("NotImplemented", Colord.Black, Colord.White, false)
        {
        }

        public override Size3i GetPhysicalSize(Size3i size)
        {
            throw new NotImplementedException();
        }

        public override int GetByteCount(Size3i size, out int rowPitch, out int slicePitch)
        {
            throw new NotImplementedException();
        }

        public override void GetBytes(
            Colord[] source, int index, int width, int height,
            Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override void GetBytes(
            Vector4s[] source, int index, int width, int height,
            Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch)
        {
            throw new NotImplementedException();
        }

        public override void GetColords(
            Stream source, int rowPitch, int slicePitch,
            Colord[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        public override void GetData(
            Stream source, int rowPitch, int slicePitch,
            Vector4s[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        //public override string Name
        //{
        //    get { return "R16G16B16A16_SNorm"; }
        //}

        //public override Numerics.Colord MaxValue
        //{
        //    get { return new Numerics.Colord(1.0, 1.0, 1.0, 1.0); }
        //}

        //public override Numerics.Colord MinValue
        //{
        //    get { return new Numerics.Colord(-1.0, -1.0, -1.0, -1.0); }
        //}

        //public override bool IsNormalized
        //{
        //    get { return true; }
        //}

        //public override int MinWidth
        //{
        //    get { return 1; }
        //}

        //public override int MinHeight
        //{
        //    get { return 1; }
        //}

        //public override int MinDepth
        //{
        //    get { return 1; }
        //}

        //public override void GetValidDimensions(ref int width, ref int height, ref int depth)
        //{
        //    width = Functions.Max(width, 1);
        //    height = Functions.Max(height, 1);
        //    depth = Functions.Max(depth, 1);
        //}

        //public override int GetByteCount(int width, int height, int depth)
        //{
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");

        //    return (width * height * depth) * 8; //8 bytes per color
        //}

        //public override int GetBytes(Numerics.Colord[] colors, int colorIndex, int width, int height, int depth, byte[] bytes, int byteIndex)
        //{
        //    Contract.Requires(colors != null, "colors cannot be null.");
        //    Contract.Requires(colorIndex >= 0, "colorIndex must be zero or greater.");
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");
        //    Contract.Requires(colorIndex + (width * height * depth) <= colors.Length, "colorIndex, width, height and depth must specify a valid range in colors.");
        //    Contract.Requires(bytes != null, "bytes cannot be null.");
        //    Contract.Requires(byteIndex >= 0, "byteIndex must be zero or greater.");
        //    Contract.Requires(byteIndex + GetByteCount(width, height, depth) <= bytes.Length, "byteIndex, width, height and depth must specify a valid range in bytes.");

        //    for (int z = 0; z < depth; ++z)
        //    {
        //        for (int y = 0; y < height; ++y)
        //        {
        //            for (int x = 0; x < width; ++x)
        //            {
        //                Numerics.Colord color = colors[colorIndex + (x + y * width + z * (height * width))];

        //                BitConverter.GetBytes((short)(color.R * short.MaxValue)).CopyTo(bytes, byteIndex + 0);
        //                BitConverter.GetBytes((short)(color.G * short.MaxValue)).CopyTo(bytes, byteIndex + 2);
        //                BitConverter.GetBytes((short)(color.B * short.MaxValue)).CopyTo(bytes, byteIndex + 4);
        //                BitConverter.GetBytes((short)(color.A * short.MaxValue)).CopyTo(bytes, byteIndex + 6);

        //                byteIndex += 8;
        //            }
        //        }
        //    }

        //    return GetByteCount(width, height, depth);
        //}

        //public override int GetColordCount(int byteCount)
        //{
        //    return byteCount / 8;
        //}

        //public override int GetColords(byte[] bytes, int byteIndex, int width, int height, int depth, Numerics.Colord[] colors, int colorIndex)
        //{
        //    Contract.Requires(bytes != null, "bytes cannot be null.");
        //    Contract.Requires(byteIndex >= 0, "byteIndex must be zero or greater.");
        //    Contract.Requires(width > 0, "width must be greater than 0");
        //    Contract.Requires(height > 0, "height must be greater than 0");
        //    Contract.Requires(depth > 0, "depth must be greater than 0");
        //    Contract.Requires(byteIndex + GetByteCount(width, height, depth) <= bytes.Length, "byteIndex, width, height and depth must specify a valid range in bytes.");
        //    Contract.Requires(colors != null, "colors cannot be null.");
        //    Contract.Requires(colorIndex >= 0, "colorIndex must be zero or greater.");
        //    Contract.Requires(colorIndex + (width * height * depth) <= colors.Length, "colorIndex, width, height and depth must specify a valid range in colors.");

        //    for (int z = 0; z < depth; ++z)
        //    {
        //        for (int y = 0; y < height; ++y)
        //        {
        //            for (int x = 0; x < width; ++x)
        //            {
        //                colors[colorIndex + (x + y * width + z * (height * width))] = new Numerics.Colord(
        //                    Functions.Max(-1.0, BitConverter.ToInt16(bytes, byteIndex + 0) / (double)short.MaxValue),
        //                    Functions.Max(-1.0, BitConverter.ToInt16(bytes, byteIndex + 2) / (double)short.MaxValue),
        //                    Functions.Max(-1.0, BitConverter.ToInt16(bytes, byteIndex + 4) / (double)short.MaxValue),
        //                    Functions.Max(-1.0, BitConverter.ToInt16(bytes, byteIndex + 6) / (double)short.MaxValue));

        //                byteIndex += 8;
        //            }
        //        }
        //    }

        //    return (width * height * depth);
        //}
    }
}
