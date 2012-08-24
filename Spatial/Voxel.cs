using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;
using System.Diagnostics.Contracts;

namespace Ibasa.Spatial
{
    public class Voxel
    {
        public Boxl Bounds { get; private set; }
        byte[] Volume;

        public Voxel(Boxl bounds)
        {
            Bounds = bounds;
            Volume = new byte[Bounds.Width * Bounds.Height * Bounds.Depth];
        }

        // Volume is stored in columns (y is up/down), this makes traversals down columns fast.
        // After that x and z are in morton order.
        public byte this[int x, int y, int z]
        {
            get
            {
                int xz = Morton.Encode(x, z);
                return Volume[y + xz * Bounds.Height];
            }
            set
            {
                int xz = Morton.Encode(x, z);
                Volume[y + xz * Bounds.Height] = value;
            }
        }
    }
}
