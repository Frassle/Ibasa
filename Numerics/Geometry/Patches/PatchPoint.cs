using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Numerics.Geometry.Patches
{
    public struct PatchPoint
    {
        public PatchPoint(Vector3d position, Vector3d normal, Vector3d tangent)
        {
            Position = position;
            Normal = normal;
            Tangent = tangent;
            Bitangent = Vector.Cross(normal, tangent);
        }

        public PatchPoint(Vector3d position, Vector3d normal, Vector3d tangent, Vector3d bitangent)
        {
            Position = position;
            Normal = normal;
            Tangent = tangent;
            Bitangent = bitangent;
        }

        public Vector3d Position;
        public Vector3d Normal;
        public Vector3d Tangent;
        public Vector3d Bitangent;
    }
}
