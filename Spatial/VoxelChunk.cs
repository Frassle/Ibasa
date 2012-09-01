using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Spatial
{
    public sealed class VoxelChunk<T> where T : IEquatable<T>
    {
        public struct RLE
        {
            public RLE(byte length, T voxel)
            {
                Length = length;
                Voxel = voxel;
            }

            public byte Length;
            public T Voxel;
        }

        public Boxl Bounds { get; private set; }
        public List<RLE> Compressed { get; private set; }
        public T[] Uncompressed { get; private set; }
        public bool IsUncompressed { get { return Uncompressed != null; } }

        public VoxelChunk(Boxl bounds)
        {
            Bounds = bounds;
            Compressed = new List<RLE>();
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
                while(size > 0)
                {
                    long length = Math.Min(size, byte.MaxValue + 1);
                    size -= length;
                    Compressed.Add(new RLE((byte)(length - 1), voxel));
                }
            }
        }

        public void Compress()
        {
            if (Compressed.Count == 0)
            {
                RLE rle = new RLE(0, Uncompressed[0]);

                for (int i = 1; i < Uncompressed.Length; ++i)
                {
                    if (Uncompressed[i].Equals(rle.Voxel) && rle.Length != byte.MaxValue)
                    {
                        rle.Length++;
                    }
                    else
                    {
                        Compressed.Add(rle);
                        rle.Voxel = Uncompressed[i];
                        rle.Length = 0;
                    }
                }

                Compressed.Add(rle);
            }
        }

        public void Uncompress()
        {
            if (!IsUncompressed)
            {
                Uncompressed = new T[Bounds.Width * Bounds.Height * Bounds.Depth];

                int offset = 0;

                for (int i = 0; i < Compressed.Count; ++i)
                {
                    int length = Compressed[i].Length + 1;
                    T voxel = Compressed[i].Voxel;

                    for (int j = 0; j < length; ++j)
                    {
                        Uncompressed[offset++] = voxel;
                    }
                }

                System.Diagnostics.Debug.Assert(offset == Uncompressed.Length);
            }
        }

        public T Get(Point3l point)
        {
            point = new Point3l(point.X - Bounds.X, point.Y - Bounds.Y, point.Z - Bounds.Z);
            long index = point.Y + point.X * Bounds.Height + point.Z * Bounds.Height * Bounds.Width;
            return Uncompressed[index];
        }

        public void Set(Point3l point, T value)
        {
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
