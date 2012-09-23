using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;
using System.Diagnostics.Contracts;

namespace Ibasa.Spatial
{
    public class DenseVoxelVolume<T> : IVoxelVolume<T>
    {
        public Boxl Bounds { get; private set; }
        T[] Volume;

        public DenseVoxelVolume(Boxl bounds)
        {
            Bounds = bounds;
            Volume = new T[Bounds.Width * Bounds.Height * Bounds.Depth];
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

        // Volume is stored in columns (y is up/down), this makes traversals down columns fast.
        // After that x and z are in morton order.

        public T Get(Point3l point)
        {
            Contract.Requires(Box.Contains(Bounds, point));

            long xz = Morton.Encode(point.X - Bounds.X, point.Z - Bounds.Z);
            return Volume[(point.Y - Bounds.Y) + xz * Bounds.Height];
        }

        public void Set(Point3l point, T value)
        {
            Contract.Requires(Box.Contains(Bounds, point));

            long xz = Morton.Encode(point.X - Bounds.X, point.Z - Bounds.Z);
            Volume[(point.Y - Bounds.Y) + xz * Bounds.Height] = value;
        }
    }
}
