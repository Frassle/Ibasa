using System;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Formats
{
    public sealed class R32G32B32Float : Format<Vector3f>
    {public R32G32B32Float()
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
        Vector3f[] source, int index, int width, int height,
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
        Vector3f[] destination, int index, int width, int height,
        Boxi sourceBoxi, Point3i destinationPoint)
    {
        throw new NotImplementedException();
    }
        
        //public override string Name
        //{
        //    get { return "R32G32B32_Float"; }
        //}

        //public override Numerics.Colord MaxValue
        //{
        //    get { return new Numerics.Colord(float.MaxValue, float.MaxValue, float.MaxValue, 0.0); }
        //}

        //public override Numerics.Colord MinValue
        //{
        //    get { return new Numerics.Colord(float.MinValue, float.MinValue, float.MinValue, 0.0); }
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

        //    return (width * height * depth) * 12; //12 bytes per color
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

        //                BitConverter.GetBytes((float)color.R).CopyTo(bytes, byteIndex + 0);
        //                BitConverter.GetBytes((float)color.G).CopyTo(bytes, byteIndex + 4);
        //                BitConverter.GetBytes((float)color.B).CopyTo(bytes, byteIndex + 8);

        //                byteIndex += 12;
        //            }
        //        }
        //    }

        //    return GetByteCount(width, height, depth);
        //}

        //public override int GetColordCount(int byteCount)
        //{
        //    return byteCount / 12;
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
        //                    BitConverter.ToSingle(bytes, byteIndex + 0),
        //                    BitConverter.ToSingle(bytes, byteIndex + 4),
        //                    BitConverter.ToSingle(bytes, byteIndex + 8),
        //                    0.0);

        //                byteIndex += 12;
        //            }
        //        }
        //    }

        //    return (width * height * depth);
        //}
    }

    public sealed class R32G32B32UInt : Format<Vector3ui>
    {public R32G32B32UInt()
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
        Vector3ui[] source, int index, int width, int height,
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
        Vector3ui[] destination, int index, int width, int height,
        Boxi sourceBoxi, Point3i destinationPoint)
    {
        throw new NotImplementedException();
    }
        
        //public override string Name
        //{
        //    get { return "R32G32B32_UInt"; }
        //}

        //public override Numerics.Colord MaxValue
        //{
        //    get { return new Numerics.Colord(uint.MaxValue, uint.MaxValue, uint.MaxValue, 0); }
        //}

        //public override Numerics.Colord MinValue
        //{
        //    get { return new Numerics.Colord(uint.MinValue, uint.MinValue, uint.MinValue, 0); }
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

        //    return (width * height * depth) * 12; //12 bytes per color
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

        //                BitConverter.GetBytes((uint)color.R).CopyTo(bytes, byteIndex + 0);
        //                BitConverter.GetBytes((uint)color.G).CopyTo(bytes, byteIndex + 4);
        //                BitConverter.GetBytes((uint)color.B).CopyTo(bytes, byteIndex + 8);

        //                byteIndex += 12;
        //            }
        //        }
        //    }

        //    return GetByteCount(width, height, depth);
        //}

        //public override int GetColordCount(int byteCount)
        //{
        //    return byteCount / 12;
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
        //                    BitConverter.ToUInt32(bytes, byteIndex + 0),
        //                    BitConverter.ToUInt32(bytes, byteIndex + 4),
        //                    BitConverter.ToUInt32(bytes, byteIndex + 8),
        //                    0);

        //                byteIndex += 12;
        //            }
        //        }
        //    }

        //    return (width * height * depth);
        //}
    }

    public sealed class R32G32B32Int : Format<Vector3i>
    {
        public R32G32B32Int()
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
            Vector3i[] source, int index, int width, int height,
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
            Vector3i[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }
        
        //public override string Name
        //{
        //    get { return "R32G32B32_SInt"; }
        //}

        //public override Numerics.Colord MaxValue
        //{
        //    get { return new Numerics.Colord(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue); }
        //}

        //public override Numerics.Colord MinValue
        //{
        //    get { return new Numerics.Colord(int.MinValue, int.MinValue, int.MinValue, int.MinValue); }
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

        //    return (width * height * depth) * 12; //12 bytes per color
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

        //                BitConverter.GetBytes((int)color.R).CopyTo(bytes, byteIndex + 0);
        //                BitConverter.GetBytes((int)color.G).CopyTo(bytes, byteIndex + 4);
        //                BitConverter.GetBytes((int)color.B).CopyTo(bytes, byteIndex + 8);

        //                byteIndex += 12;
        //            }
        //        }
        //    }

        //    return GetByteCount(width, height, depth);
        //}

        //public override int GetColordCount(int byteCount)
        //{
        //    return byteCount / 12;
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
        //                    BitConverter.ToVector3Vector2i(bytes, byteIndex + 0),
        //                    BitConverter.ToVector3Vector2i(bytes, byteIndex + 4),
        //                    BitConverter.ToVector3Vector2i(bytes, byteIndex + 8),
        //                    0);

        //                byteIndex += 12;
        //            }
        //        }
        //    }

        //    return (width * height * depth);
        //}
    }
}
