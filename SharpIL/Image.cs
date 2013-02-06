using System;
using System.Diagnostics.Contracts;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace Ibasa.SharpIL
{
    public enum AddressMode
    {
        Clamp,
        Wrap,
        Mirror,
        Border,
    }

    public sealed class Image
    {
        #region Contracts
        [ContractInvariantMethod()]
        void ObjectInvariant()
        {
            Contract.Invariant(Pixels != null, "Pixels is null.");
            Contract.Invariant(Width > 0, "Width is less than or zero.");
            Contract.Invariant(Height > 0, "Height is less than or zero.");
            Contract.Invariant(Depth > 0, "Depth is less than or zero.");
            Contract.Invariant(Pixels.Length != Width * Height * Depth, "Pixels does not match image bounds.");
        }
        #endregion

        #region Pixels
        public AddressMode AddressX { get; set; }
        public AddressMode AddressY { get; set; }
        public AddressMode AddressZ { get; set; }
        public Colord[] Pixels { get; private set; }
        public Colord BorderColor { get; set; }

        private static int Address(int axis, int axisLength, AddressMode mode)
        {
            switch (mode)
            {
                case AddressMode.Clamp:
                    return Functions.Max(0, Functions.Min(axisLength - 1, axis));
                case AddressMode.Wrap:
                    return Functions.Modulus(axis, axisLength);
                case AddressMode.Mirror:
                    {
                        int cycle = axis / axisLength;
                        if (cycle % 2 == 0)
                            return axis - (cycle * axisLength);
                        else
                            return -axis + (cycle * axisLength) + (axisLength - 1);
                    }
            }
            return axis;
        }
        private bool Border(int x)
        {
            if ((x < 0 || x >= Width) && AddressX == AddressMode.Border)
                return true;
            else
                return false;
        }
        private bool Border(int x, int y)
        {
            if ((x < 0 || x >= Width) && AddressX == AddressMode.Border)
                return true;
            else if ((y < 0 || y >= Height) && AddressY == AddressMode.Border)
                return true;
            else
                return false;
        }
        private bool Border(int x, int y, int z)
        {
            if ((x < 0 || x >= Width) && AddressX == AddressMode.Border)
                return true;
            else if ((y < 0 || y >= Height) && AddressY == AddressMode.Border)
                return true;
            else if ((z < 0 || z >= Depth) && AddressZ == AddressMode.Border)
                return true;
            else
                return false;
        }

        public Colord this[int x]
        {
            get
            {
                if (Border(x))
                    return BorderColor;

                x = Address(x, Width, AddressX);

                return Pixels[x];
            }
            set
            {
                if (Border(x))
                    return;

                x = Address(x, Width, AddressX);

                Pixels[x] = value;
            }
        }
        public Colord this[int x, int y]
        {
            get
            {
                if (Border(x, y))
                    return BorderColor;

                x = Address(x, Width, AddressX);
                y = Address(y, Height, AddressY);

                return Pixels[x + y * Width];
            }
            set
            {
                if (Border(x, y))
                    return;

                x = Address(x, Width, AddressX);
                y = Address(y, Height, AddressY);

                Pixels[x + y * Width] = value;
            }
        }
        public Colord this[int x, int y, int z]
        {
            get
            {
                if (Border(x, y, z))
                    return BorderColor;

                x = Address(x, Width, AddressX);
                y = Address(y, Height, AddressY);
                z = Address(z, Depth, AddressZ);

                return Pixels[x + y * Width + z * (Width * Height)];
            }
            set
            {
                if (Border(x, y, z))
                    return;

                x = Address(x, Width, AddressX);
                y = Address(y, Height, AddressY);
                z = Address(z, Depth, AddressZ);

                Pixels[x + y * Width + z * (Width * Height)] = value;
            }
        }
        public Colord this[Point3i point]
        {
            get
            {
                if (Border(point.X, point.Y, point.Z))
                    return BorderColor;

                int x = Address(point.X, Width, AddressX);
                int y = Address(point.Y, Height, AddressY);
                int z = Address(point.Z, Depth, AddressZ);

                return Pixels[x + y * Width + z * (Width * Height)];
            }
            set
            {
                if (Border(point.X, point.Y, point.Z))
                    return;

                int x = Address(point.X, Width, AddressX);
                int y = Address(point.Y, Height, AddressY);
                int z = Address(point.Z, Depth, AddressZ);

                Pixels[x + y * Width + z * (Width * Height)] = value;
            }
        }
        #endregion

        #region Properties
        public Size3i Size { get; private set; }
        public int Width { get { return Size.Width; } }
        public int Height { get { return Size.Height; } }
        public int Depth { get { return Size.Depth; } }
        #endregion

        #region Constructors
        public Image(Image source)
        {
            Contract.Requires(source != null);

            Size = source.Size;

            Pixels = new Colord[source.Pixels.Length];
            Array.Copy(source.Pixels, Pixels, Pixels.Length);

            AddressX = source.AddressX;
            AddressY = source.AddressY;
            AddressZ = source.AddressZ;
        }
        public Image(Resource source, int mipSlice, int arraySlice)
        {
            Contract.Requires(source != null);
            Contract.Requires(0 < mipSlice);
            Contract.Requires(mipSlice < source.MipLevels);
            Contract.Requires(0 < arraySlice);
            Contract.Requires(arraySlice < source.ArraySize);

            Size = source.ComputeMipSliceSize(mipSlice);

            Pixels = new Colord[Width * Height * Depth];
            source.GetColords(
                mipSlice, arraySlice,
                Pixels, 0, Size.Width, Size.Height,
                new Boxi(Point3i.Zero, Size), Point3i.Zero);

            AddressX = AddressMode.Clamp;
            AddressY = AddressMode.Clamp;
            AddressZ = AddressMode.Clamp;
        }
        public Image(Size3i size)
        {
            Contract.Requires(size.Width > 0);
            Contract.Requires(size.Height > 0);
            Contract.Requires(size.Depth > 0);

            Size = size;
            Pixels = new Colord[Width * Height * Depth];

            AddressX = AddressMode.Clamp;
            AddressY = AddressMode.Clamp;
            AddressZ = AddressMode.Clamp;
        }
        public Image(Colord[] pixels)
        {
            Contract.Requires(pixels != null);

            Size = new Size3i(pixels.GetLength(0), 1, 1);
            Pixels = new Colord[Width];
            for (int x = 0; x < Width; ++x)
            {
                this[x] = pixels[x];
            }

            AddressX = AddressMode.Clamp;
            AddressY = AddressMode.Clamp;
            AddressZ = AddressMode.Clamp;
        }
        public Image(Colord[,] pixels)
        {
            Contract.Requires(pixels != null);

            Size = new Size3i(pixels.GetLength(0), pixels.GetLength(1), 1);
            Pixels = new Colord[Width * Height];
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    this[x, y] = pixels[x, y];
                }
            }

            AddressX = AddressMode.Clamp;
            AddressY = AddressMode.Clamp;
            AddressZ = AddressMode.Clamp;
        }
        public Image(Colord[,,] pixels)
        {
            Contract.Requires(pixels != null);

            Size = new Size3i(pixels.GetLength(0), pixels.GetLength(1), pixels.GetLength(2));
            Pixels = new Colord[Width * Height * Depth];
            for (int z = 0; z < Depth; ++z)
            {
                for (int y = 0; y < Height; ++y)
                {
                    for (int x = 0; x < Width; ++x)
                    {
                        this[x, y, z] = pixels[x, y, z];
                    }
                }
            }

            AddressX = AddressMode.Clamp;
            AddressY = AddressMode.Clamp;
            AddressZ = AddressMode.Clamp;
        }

        #endregion

        #region Get/Set Pixel
        public Colord GetPixel(int index)
        {
            Contract.Requires(index >= 0, "index is less than zero.");
            Contract.Requires(index < Pixels.Length, "index is out of image bounds.");

            return Pixels[index];
        }
        public Colord GetPixel(int x, int y, int z)
        {
            Contract.Requires(x < Width && x >= 0, "x must be zero or greater, and less than the image width.");
            Contract.Requires(y < Height && y >= 0, "y must be zero or greater, and less than the image height.");
            Contract.Requires(z < Depth && z >= 0, "z must be zero or greater, and less than the image depth.");

            return Pixels[x + y * Width + z * (Width * Height)];
        }
        public Colord[] GetPixels(int x, int y, int z, int width, int height, int depth)
        {
            Contract.Requires(x < Width && x >= 0, "x must be zero or greater, and less than the image width.");
            Contract.Requires(y < Height && y >= 0, "y must be zero or greater, and less than the image height.");
            Contract.Requires(z < Depth && z >= 0, "z must be zero or greater, and less than the image depth.");
            Contract.Requires(width > 0, "width must be greater than zero.");
            Contract.Requires(height > 0, "height must be greater than zero.");
            Contract.Requires(depth > 0, "depth must be greater than zero.");
            Contract.Requires(x + width < Width, "x plus width is greater than image width.");
            Contract.Requires(y + height < Height, "y plus height is greater than image height.");
            Contract.Requires(z + depth < Depth, "z plus depth is greater than image depth.");

            Colord[] pixels = new Colord[width * height * depth];

            for (int destZ = 0, srcZ = z; destZ < depth; ++destZ, ++srcZ)
            {        
                int destZOffset = destZ * height;
                for (int destY = 0, srcY = y; destY < height; ++destY, ++srcY)
                {
                    int destYOffset = destY * width + destZOffset;
                    for (int destX = 0, srcX = x; destX < width; ++destX, ++srcX)
                    {
                        int destOffset = destX + destYOffset;
                        pixels[destOffset] = GetPixel(srcX,srcY,srcZ);
                    }
                }
            }

            return pixels;
        }
        public void SetPixel(int index, Colord pixel)
        {
            Contract.Requires(index >= 0, "index is less than zero.");
            Contract.Requires(index < Pixels.Length, "index is out of image bounds.");

            Pixels[index] = pixel;
        }
        public void SetPixel(int x, int y, int z, Colord pixel)
        {
            Contract.Requires(x < Width && x >= 0, "x must be zero or greater, and less than the image width.");
            Contract.Requires(y < Height && y >= 0, "y must be zero or greater, and less than the image height.");
            Contract.Requires(z < Depth && z >= 0, "z must be zero or greater, and less than the image depth.");

            Pixels[x + y * Width + z * (Width * Height)] = pixel;
        }
        public void SetPixels(int x, int y, int z, int width, int height, int depth, Colord[] pixels)
        {
            Contract.Requires(width * height * depth == pixels.Length, 
                "pixels length does not match length given by width, height and depth.");
            Contract.Requires(x < Width && x >= 0, "x must be zero or greater, and less than the image width.");
            Contract.Requires(y < Height && y >= 0, "y must be zero or greater, and less than the image height.");
            Contract.Requires(z < Depth && z >= 0, "z must be zero or greater, and less than the image depth.");
            Contract.Requires(width > 0, "width must be greater than zero.");
            Contract.Requires(height > 0, "height must be greater than zero.");
            Contract.Requires(depth > 0, "depth must be greater than zero.");
            Contract.Requires(x + width < Width, "x plus width is greater than image width.");
            Contract.Requires(y + height < Height, "y plus height is greater than image height.");
            Contract.Requires(z + depth < Depth, "z plus depth is greater than image depth.");

            for (int destZ = z, srcZ = 0; srcZ < depth; ++destZ, ++srcZ)
            {
                int srcZOffset = srcZ * height;
                for (int destY = y, srcY = 0; srcY < height; ++destY, ++srcY)
                {
                    int srcYOffset = srcY * width + srcZOffset;
                    for (int destX = x, srcX = 0; srcX < width; ++destX, ++srcX)
                    {
                        int srcOffset = srcX + srcYOffset;
                        SetPixel(destX, destY, destZ, pixels[srcOffset]);
                    }
                }
            }
        }
        #endregion

        #region Blit
        public void Blit(Image source, int srcX, int srcY, int srcZ, int destX, int destY, int destZ, int width, int height, int depth)
		{
            Blit(source, new Point3i(srcX, srcY, srcZ), new Point3i(destX, destY, destZ), new Size3i(width, height, depth));
        }
        public void Blit(Image source, Point3i sourcePoint, Point3i destinationPoint, Size3i size)
        {
            for (int z = 0; z < size.Depth; ++z)
            {
                int zsrc = (sourcePoint.Z + z) * (source.Width * source.Height);
                int zdst = (destinationPoint.Z + z) * (Width * Height);
                for (int y = 0; y < size.Height; ++y)
                {
                    int xysrc = sourcePoint.X + (sourcePoint.Y + y) * source.Width + zsrc;
                    int xydst = destinationPoint.X + (destinationPoint.Y + y) * Width + zdst;

                    for (int x = 0; x < size.Width; ++x)
                    {
                        Pixels[xydst++] = source.Pixels[xysrc++];
                    }
                }
            }
        }
        #endregion

        #region Clear
        public void Clear(Colord color)
        {
            for (int i = 0; i < Pixels.Length; ++i)
            {
                Pixels[i] = color;
            }
        }
        #endregion

        #region Manipulation

        public static Image Initialize(Size3i size, Func<Point3i, Colord> initalizer)
        {
            Image image = new Image(size);

            for (int z = 0; z < image.Depth; ++z)
            {
                for (int y = 0; y < image.Height; ++y)
                {
                    for (int x = 0; x < image.Width; ++x)
                    {
                        image[x, y, z] = initalizer(new Point3i(x, y, z));
                    }
                }
            }

            return image;
        }

        public static void Iterate(Action<Colord> action, Image source)
        {
            for (int z = 0; z < source.Depth; ++z)
            {
                for (int y = 0; y < source.Height; ++y)
                {
                    for (int x = 0; x < source.Width; ++x)
                    {
                        action(source[x, y, z]);
                    }
                }
            }
        }

        public static void Iterate2(Action<Colord, Colord> action, Image source1, Image source2)
        {
            Contract.Requires(source1.Size == source2.Size);

            for (int z = 0; z < source1.Depth; ++z)
            {
                for (int y = 0; y < source1.Height; ++y)
                {
                    for (int x = 0; x < source1.Width; ++x)
                    {
                        action(source1[x, y, z], source2[x, y, z]);
                    }
                }
            }
        }

        public static void IterateIndexed(Action<Point3i, Colord> action, Image source)
        {
            for (int z = 0; z < source.Depth; ++z)
            {
                for (int y = 0; y < source.Height; ++y)
                {
                    for (int x = 0; x < source.Width; ++x)
                    {
                        action(new Point3i(x, y, z), source[x, y, z]);
                    }
                }
            }
        }

        public static void IterateIndexed2(Action<Point3i, Colord, Colord> action, Image source1, Image source2)
        {
            Contract.Requires(source1.Size == source2.Size);

            for (int z = 0; z < source1.Depth; ++z)
            {
                for (int y = 0; y < source1.Height; ++y)
                {
                    for (int x = 0; x < source1.Width; ++x)
                    {
                        action(new Point3i(x, y, z), source1[x, y, z], source2[x, y, z]);
                    }
                }
            }
        }

        public static Image Map(Func<Colord, Colord> function, Image source)
        {
            Image result = new Image(source.Size);

            for (int z = 0; z < result.Depth; ++z)
            {
                for (int y = 0; y < result.Height; ++y)
                {
                    for (int x = 0; x < result.Width; ++x)
                    {
                        result[x, y, z] = function(source[x, y, z]);
                    }
                }
            }

            return result;
        }

        public static Image Map2(Func<Colord, Colord, Colord> function, Image source1, Image source2)
        {
            Image result = new Image(Ibasa.Numerics.Geometry.Size.Min(source1.Size, source2.Size));

            for (int z = 0; z < result.Depth; ++z)
            {
                for (int y = 0; y < result.Height; ++y)
                {
                    for (int x = 0; x < result.Width; ++x)
                    {
                        result[x, y, z] = function(source1[x, y, z], source2[x, y, z]);
                    }
                }
            }

            return result;
        }

        public static Image Map3(Func<Colord, Colord, Colord, Colord> function, Image source1, Image source2, Image source3)
        {
            Image result = new Image(
                Ibasa.Numerics.Geometry.Size.Min(Ibasa.Numerics.Geometry.Size.Min(source1.Size, source2.Size), source3.Size));

            for (int z = 0; z < result.Depth; ++z)
            {
                for (int y = 0; y < result.Height; ++y)
                {
                    for (int x = 0; x < result.Width; ++x)
                    {
                        result[x, y, z] = function(source1[x, y, z], source2[x, y, z], source3[x, y, z]);
                    }
                }
            }

            return result;
        }

        public static Image MapIndexed(Func<Point3i, Colord, Colord> function, Image source)
        {
            Image result = new Image(source.Size);

            for (int z = 0; z < source.Depth; ++z)
            {
                for (int y = 0; y < source.Height; ++y)
                {
                    for (int x = 0; x < source.Width; ++x)
                    {
                        result[x, y, z] = function(new Point3i(x, y, z), source[x, y, z]);
                    }
                }
            }

            return result;
        }

        public static Image MapIndexed2(Func<Point3i, Colord, Colord, Colord> function, Image source1, Image source2)
        {
            Image result = new Image(Ibasa.Numerics.Geometry.Size.Min(source1.Size, source2.Size));

            for (int z = 0; z < result.Depth; ++z)
            {
                for (int y = 0; y < result.Height; ++y)
                {
                    for (int x = 0; x < result.Width; ++x)
                    {
                        result[x, y, z] = function(new Point3i(x, y, z), source1[x, y, z], source2[x, y, z]);
                    }
                }
            }

            return result;
        }

        public static Image Fold(Func<Colord, Colord, Colord> function, Colord color, IEnumerable<Image> images)
        {
            var size = images.Aggregate(
                new Size3i(int.MaxValue, int.MaxValue, int.MaxValue),
                (s, i) =>
                    Ibasa.Numerics.Geometry.Size.Min(s, i.Size));

            var result = new Image(size);

            for (int z = 0; z < result.Depth; ++z)
            {
                for (int y = 0; y < result.Height; ++y)
                {
                    for (int x = 0; x < result.Width; ++x)
                    {
                        var fold = color;
                        foreach (var image in images)
                        {
                            fold = function(fold, image[x,y,z]);
                        }
                        result[x, y, z] = fold;
                    }
                }
            }

            return result;
        }

        #endregion

        public void Copy(Resource destination, int mipSlice, int arraySlice)
        {
            Contract.Requires(destination != null);
            Contract.Requires(0 < mipSlice);
            Contract.Requires(mipSlice < destination.MipLevels);
            Contract.Requires(0 < arraySlice);
            Contract.Requires(arraySlice < destination.ArraySize);

            destination.SetBytes(
                mipSlice, arraySlice,
                Pixels, 0, Size.Width, Size.Height,
                new Boxi(Point3i.Zero, Size), Point3i.Zero);
        }
    }
}
