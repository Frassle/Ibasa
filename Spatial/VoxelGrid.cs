using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;
using System.Diagnostics.Contracts;

namespace Ibasa.Spatial
{
    public class VoxelGrid<T>
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
                Contract.Requires(Box.Contains(Bounds, point));

                int xz = Morton.Encode((int)(point.X - Bounds.X), (int)(point.Z - Bounds.Z));
                return Volume[(point.Y - Bounds.Y) + xz * Bounds.Height];
            }
            set
            {
                Contract.Requires(Box.Contains(Bounds, point));

                int xz = Morton.Encode((int)(point.X - Bounds.X), (int)(point.Z - Bounds.Z));
                Volume[(point.Y - Bounds.Y) + xz * Bounds.Height] = value;
            }
        }
    }
}
