using System;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL.Formats
{
    public sealed class R8G8UInt : Format<Vector2b>
    {
        public R8G8UInt()
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
            Vector2b[] source, int index, int width, int height, 
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
            Vector2b[] destination, int index, int width, int height, 
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }

        //public override string Name
        //{
        //    get { return "R8G8_UInt"; }
        //}

        //public override Numerics.Colord MaxValue
        //{
        //    get { return new Numerics.Colord(byte.MaxValue, byte.MaxValue, 0, 0); }
        //}

        //public override Numerics.Colord MinValue
        //{
        //    get { return new Numerics.Colord(byte.MinValue, byte.MinValue, 0, 0); }
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

        //    return (width * height * depth) * 2; //2 bytes per color
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

        //                bytes[byteIndex + 0] = (byte)(color.R);
        //                bytes[byteIndex + 1] = (byte)(color.G);

        //                byteIndex += 2;
        //            }
        //        }
        //    }

        //    return GetByteCount(width, height, depth);
        //}

        //public override int GetColordCount(int byteCount)
        //{
        //    return byteCount / 2;
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
        //                    bytes[byteIndex + 0],
        //                    bytes[byteIndex + 1],
        //                    0, 0);

        //                byteIndex += 2;
        //            }
        //        }
        //    }

        //    return (width * height * depth);
        //}
    }

    public sealed class R8G8Int : Format<Vector2sb>
    {
        public R8G8Int()
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
            Vector2sb[] source, int index, int width, int height,
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
            Vector2sb[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }
        
        //public override string Name
        //{
        //    get { return "R8G8_SInt"; }
        //}

        //public override Numerics.Colord MaxValue
        //{
        //    get { return new Numerics.Colord(sbyte.MaxValue, sbyte.MaxValue, 0, 0); }
        //}

        //public override Numerics.Colord MinValue
        //{
        //    get { return new Numerics.Colord(sbyte.MinValue, sbyte.MinValue, 0, 0); }
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

        //    return (width * height * depth) * 2; //2 bytes per color
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

        //                bytes[byteIndex + 0] = (byte)(sbyte)(color.R);
        //                bytes[byteIndex + 1] = (byte)(sbyte)(color.G);

        //                byteIndex += 2;
        //            }
        //        }
        //    }

        //    return GetByteCount(width, height, depth);
        //}

        //public override int GetColordCount(int byteCount)
        //{
        //    return byteCount / 2;
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
        //                    (sbyte)bytes[byteIndex + 0],
        //                    (sbyte)bytes[byteIndex + 1],
        //                    0, 0);

        //                byteIndex += 2;
        //            }
        //        }
        //    }

        //    return (width * height * depth);
        //}
    }

    public sealed class R8G8UNorm : Format<Vector2b>
    {
        public R8G8UNorm()
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
            Vector2b[] source, int index, int width, int height,
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
            Vector2b[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }
        
        //public override string Name
        //{
        //    get { return "R8G8_UNorm"; }
        //}

        //public override Numerics.Colord MaxValue
        //{
        //    get { return new Numerics.Colord(1.0, 1.0, 0.0, 0.0); }
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

        //    return (width * height * depth) * 2; //2 bytes per color
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

        //                bytes[byteIndex + 0] = (byte)(color.R * byte.MaxValue);
        //                bytes[byteIndex + 1] = (byte)(color.G * byte.MaxValue);

        //                byteIndex += 2;
        //            }
        //        }
        //    }

        //    return GetByteCount(width, height, depth);
        //}

        //public override int GetColordCount(int byteCount)
        //{
        //    return byteCount / 2;
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
        //                    bytes[byteIndex + 0] / (double)byte.MaxValue,
        //                    bytes[byteIndex + 1] / (double)byte.MaxValue,
        //                    0.0, 0.0);

        //                byteIndex += 2;
        //            }
        //        }
        //    }

        //    return (width * height * depth);
        //}
    }

    public sealed class R8G8Norm : Format<Vector2sb>
    {
        public R8G8Norm()
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
            Vector2sb[] source, int index, int width, int height,
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
            Vector2sb[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            throw new NotImplementedException();
        }
        
        //public override string Name
        //{
        //    get { return "R8G8_SNorm"; }
        //}

        //public override Numerics.Colord MaxValue
        //{
        //    get { return new Numerics.Colord(1.0, 1.0, 0.0, 0.0); }
        //}

        //public override Numerics.Colord MinValue
        //{
        //    get { return new Numerics.Colord(-1.0, -1.0, 0.0, 0.0); }
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

        //    return (width * height * depth) * 2; //2 bytes per color
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

        //                bytes[byteIndex + 0] = (byte)(sbyte)(color.R * sbyte.MaxValue);
        //                bytes[byteIndex + 1] = (byte)(sbyte)(color.G * sbyte.MaxValue);

        //                byteIndex += 2;
        //            }
        //        }
        //    }

        //    return GetByteCount(width, height, depth);
        //}

        //public override int GetColordCount(int byteCount)
        //{
        //    return byteCount / 2;
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
        //                    Functions.Max(-1.0, (sbyte)bytes[byteIndex + 0] / (double)sbyte.MaxValue),
        //                    Functions.Max(-1.0, (sbyte)bytes[byteIndex + 1] / (double)sbyte.MaxValue),
        //                    0.0, 0.0);

        //                byteIndex += 2;
        //            }
        //        }
        //    }

        //    return (width * height * depth);
        //}
    }
}
