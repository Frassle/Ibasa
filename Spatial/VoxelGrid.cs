using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;
using System.Diagnostics.Contracts;

namespace Ibasa.Spatial
{
    public class VoxelGrid<T> : IVoxel<T> where T : IEquatable<T>
    {
        public Boxl Bounds { get; private set; }
        T[] Volume;

        public VoxelGrid(Boxl bounds)
        {
            Bounds = bounds;
            Volume = new T[Bounds.Width * Bounds.Height * Bounds.Depth];
        }

        // Volume is stored in columns (y is up/down), this makes traversals down columns fast.
        // After that x and z are in morton order.
        public T this[Point3l point]
        {
            get
            {
                int xz = Morton.Encode((int)point.X, (int)point.Z);
                return Volume[point.Y + xz * Bounds.Height];
            }
            set
            {
                int xz = Morton.Encode((int)point.X, (int)point.Z);
                Volume[point.Y + xz * Bounds.Height] = value;
            }
        }
    }
}
