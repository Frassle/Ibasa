using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Spatial
{
    public interface IVoxel<T> where T : IEquatable<T>
    {
        T this[Point3l point] { get; set; }
    }
}
