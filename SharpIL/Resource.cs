using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.SharpIL
{
    public sealed class Resource : IEnumerable<byte[]>
    {
        public static Resource Convert(Resource resource, Format newFormat)
        {
            Resource newResource = new Resource(
                    resource.Size, resource.MipLevels, resource.ArraySize,
                    newFormat);

            for (int mipSlice = 0; mipSlice < resource.MipLevels; ++mipSlice)
            {
                for (int arraySlice = 0; arraySlice < resource.ArraySize; ++arraySlice)
                {
                    Size3i size = resource.ComputeMipSliceSize(mipSlice);

                    System.IO.MemoryStream source = new System.IO.MemoryStream(resource[mipSlice,arraySlice]);
                    int sourceRowPitch, sourceSlicePitch;
                    resource.Format.GetByteCount(size, out sourceRowPitch, out sourceSlicePitch);

                    System.IO.MemoryStream destination = new System.IO.MemoryStream(newResource[mipSlice, arraySlice]);
                    int destinationRowPitch, destinationSlicePitch;
                    newResource.Format.GetByteCount(size, out destinationRowPitch, out destinationSlicePitch);

                    Format.Convert(resource.Format, newResource.Format,
                        source, sourceRowPitch, sourceSlicePitch,
                        destination, destinationRowPitch, destinationSlicePitch,
                        new Boxi(Point3i.Zero, size), Point3i.Zero);
                }
            }

            return newResource;
        }

        public Size3i Size { get; private set; }
        public int Width { get { return Size.Width; } }
        public int Height { get { return Size.Height; } }
        public int Depth { get { return Size.Depth; } }
        public int MipLevels { get; private set; }
        public int ArraySize { get; private set; }
        public Format Format { get; private set; }

        public int Count { get { return Subresources.Length; } }

        private byte[][] Subresources;
        public byte[] this[int mipSlice, int arraySlice]
        {
            get
            {
                Contract.Requires(mipSlice >= 0, "mipSlice must be zero or greater.");
                Contract.Requires(mipSlice < MipLevels, "mipSlice must be less than MipLevels.");
                Contract.Requires(arraySlice >= 0, "arraySlice must be zero or greater.");
                Contract.Requires(arraySlice < ArraySize, "arraySlice must be less than ArraySize.");
                Contract.Ensures(Contract.Result<byte[]>() != null);

                return Subresources[mipSlice + MipLevels * arraySlice];
            }
            private set
            {
                Subresources[mipSlice + MipLevels * arraySlice] = value;
            }
        }

        public byte[] this[int index]
        {
            get
            {
                Contract.Requires(index >= 0, "index must be zero or greater.");
                Contract.Requires(index < ArraySize * MipLevels, "index must be less than ArraySize * MipLevels.");
                Contract.Ensures(Contract.Result<byte[]>() != null);

                return Subresources[index];
            }
        }

        public Resource(Size3i size, int mipLevels, int arraySize, Format format)
        {
            Contract.Requires(mipLevels >= 0, "mipLevels must be zero or greater.");
            Contract.Requires(arraySize > 0, "arraySize must be greater than zero.");
            Contract.Requires(format != null, "format cannot be null.");

            Size = new Size3i(Functions.Max(size.Width, 1), Functions.Max(size.Height, 1), Functions.Max(size.Depth, 1));
            MipLevels = mipLevels == 0 ? ComputeMiplevels(Size) : mipLevels;
            ArraySize = arraySize;
            Format = format;
            Subresources = new byte[MipLevels * ArraySize][];

            
            for (int mipSlice = 0; mipSlice < MipLevels; ++mipSlice)
            {
                int byteCount = Format.GetByteCount(ComputeMipSliceSize(mipSlice));
                for (int arraySlice = 0; arraySlice < ArraySize; ++arraySlice)
                {
                    this[mipSlice, arraySlice] = new byte[byteCount];
                }
            }
        }

        public bool SetSubresource(int mipSlice, int arraySlice, byte[] data)
        {
            Contract.Requires(mipSlice >= 0, "mipSlice must be zero or greater.");
            Contract.Requires(mipSlice < MipLevels, "mipSlice must be less than MipLevels.");
            Contract.Requires(arraySlice >= 0, "arraySlice must be zero or greater.");
            Contract.Requires(arraySlice < ArraySize, "arraySlice must be less than ArraySize.");
            Contract.Requires(data != null, "data must not be null.");

            if (data.Length == this[mipSlice, arraySlice].Length)
            {
                this[mipSlice, arraySlice] = data;
                return true;
            }
            return false;
        }

        public static int ComputeMiplevels(Size3i size)
        {
            Contract.Requires(size.Width > 0, "size must be greater than zero.");
            Contract.Requires(size.Height > 0, "size must be greater than zero.");
            Contract.Requires(size.Depth > 0, "size must be greater than zero.");
            Contract.Ensures(Contract.Result<int>() > 0, "Result must be greater than zero.");

            return Binary.CeilingLog2(Functions.Max(Functions.Max(size.Width, size.Height), size.Depth)) + 1;
        }

        public Size3i ComputeMipSliceSize(int mipSlice)
        {
            Contract.Requires(mipSlice >= 0, "mipSlice must be zero or greater.");
            Contract.Requires(mipSlice < MipLevels, "mipSlice must be less than MipLevels.");

            return new Size3i(
                Functions.Max(Width >> mipSlice, 1),
                Functions.Max(Height >> mipSlice, 1),
                Functions.Max(Depth >> mipSlice, 1));
        }

        public IEnumerator<byte[]> GetEnumerator()
        {
            for (int mipSlice = 0; mipSlice < MipLevels; ++mipSlice)
            {
                for (int arraySlice = 0; arraySlice < ArraySize; ++arraySlice)
                {
                    yield return this[mipSlice, arraySlice];
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void SetBytes(
            int mipSlice, int arraySlice,
            Colord[] source, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            System.IO.Stream destinationStream = new System.IO.MemoryStream(this[mipSlice, arraySlice]);

            int rowPitch, slicePitch;
            Format.GetByteCount(ComputeMipSliceSize(mipSlice), out rowPitch, out slicePitch);

            Format.GetBytes(
                source, index, width, height,
                destinationStream, rowPitch, slicePitch,
                sourceBoxi, destinationPoint);
        }

        public void GetColords(
            int mipSlice, int arraySlice,
            Colord[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            System.IO.Stream sourceStream = new System.IO.MemoryStream(this[mipSlice, arraySlice]);

            int rowPitch, slicePitch;
            Format.GetByteCount(ComputeMipSliceSize(mipSlice), out rowPitch, out slicePitch);

            Format.GetColords(
                sourceStream, rowPitch, slicePitch,
                destination, index, width, height,
                sourceBoxi, destinationPoint);
        }
    }
}
