using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Spatial
{
    public interface IVoxelVolume<T> 
    {
        Boxl Bounds { get; }

        T this[Point3l point] { get; set; }

        T Get(Point3l point);
        void Set(Point3l point, T value);
    }
}
