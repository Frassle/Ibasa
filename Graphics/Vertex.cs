using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Graphics
{
    public interface IVertex
    {

    }

    public struct VertexPositionColor
    {
        public readonly Vector3f Position;
        public readonly Colorf Color;
    }
}
