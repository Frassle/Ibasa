using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;
using System.Diagnostics.Contracts;

namespace Ibasa.Spatial
{
    public sealed class CompressedVoxelVolume<T> where T : IEquatable<T>
    {
        public Boxl Bounds { get; private set; }
        public CompressedList<T> Compressed { get; private set; }
        public T[] Uncompressed { get; private set; }
        public bool IsUncompressed { get { return Uncompressed != null; } }
        public bool IsCompressed { get { return Compressed.Count != 0; } }

        public CompressedVoxelVolume(Boxl bounds)
        {
            Bounds = bounds;
            Compressed = new CompressedList<T>();
            Uncompressed = null;
            Fill(default(T));
        }

        public void Fill(T voxel)
        {
            if (IsUncompressed)
            {
                Compressed.Clear();
                for (int i = 0; i < Uncompressed.Length; ++i)
                {
                    Uncompressed[i] = voxel;
                }
            }
            else
            {
                Compressed.Clear();
                long size = Bounds.Width * Bounds.Height * Bounds.Depth;
                for (int i = 0; i < size; ++i)
                {
                    Compressed.Add(voxel);
                }
            }
        }

        public void Compress()
        {
            Contract.Requires(IsUncompressed);

            if (!IsCompressed)
            {
                Compressed.AddRange(Uncompressed);
            }
        }

        public void Uncompress(T[] data)
        {
            Contract.Requires(!IsUncompressed);
            Contract.Requires(data != null);
            Contract.Requires(data.Length == Bounds.Width * Bounds.Height * Bounds.Depth);

            Uncompressed = data;
            Compressed.CopyTo(Uncompressed);
        }

        public T[] Flush()
        {
            Contract.Requires(IsUncompressed);

            Compress();

            var data = Uncompressed;
            Uncompressed = null;
            return data;
        }

        public T Get(Point3l point)
        {
            Contract.Requires(IsUncompressed);

            point = new Point3l(point.X - Bounds.X, point.Y - Bounds.Y, point.Z - Bounds.Z);
            long index = point.Y + point.X * Bounds.Height + point.Z * Bounds.Height * Bounds.Width;
            return Uncompressed[index];
        }

        public void Set(Point3l point, T value)
        {
            Contract.Requires(IsUncompressed);

            point = new Point3l(point.X - Bounds.X, point.Y - Bounds.Y, point.Z - Bounds.Z);
            long index = point.Y + point.X * Bounds.Height + point.Z * Bounds.Height * Bounds.Width;
            Uncompressed[index] = value;
            Compressed.Clear();
        }

        public T this[Point3l point]
        {
            get
            {
                return Get(point);
            }
            set
            {
                Set(point, value);
            }
        }
    }
}
