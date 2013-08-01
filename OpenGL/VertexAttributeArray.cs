using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    public sealed class VertexAttributeArray : IEquatable<VertexAttributeArray>
    {
        readonly uint Index;

        internal VertexAttributeArray(uint index)
        {
            Index = index;
        }

        public bool Enabled
        {
            get
            {
                unsafe
                {
                    int value;
                    Gl.GetVertexAttribiv(Index, Gl.VERTEX_ATTRIB_ARRAY_ENABLED, &value);
                    return value != 0;
                }
            }
            set
            {
                if (value)
                {
                    Gl.EnableVertexAttribArray(Index);
                }
                else
                {
                    Gl.DisableVertexAttribArray(Index);
                }
            }
        }

        public void VertexAttributePointer(int size, DataType type, bool normalized, int stride, int offset)
        {
            unsafe
            {
                Gl.VertexAttribPointer(Index, size, (uint)type, (byte)(normalized ? 1 : 0), stride, (void*)offset);
                GlHelper.GetError();
            }
        }

        public override int GetHashCode()
        {
            return Index.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is VertexAttributeArray)
            {
                return Equals((VertexAttributeArray)obj);
            }
            return false;
        }

        public bool Equals(VertexAttributeArray other)
        {
            return !Object.ReferenceEquals(other, null) && Index == other.Index;
        }

        public static bool operator ==(VertexAttributeArray left, VertexAttributeArray right)
        {
            if (Object.ReferenceEquals(left, right)) return true;
            if (Object.ReferenceEquals(left, null)) return false;
            if (Object.ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(VertexAttributeArray left, VertexAttributeArray right)
        {
            if (Object.ReferenceEquals(left, right)) return false;
            if (Object.ReferenceEquals(left, null)) return true;
            if (Object.ReferenceEquals(right, null)) return true;

            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format("VertexAttributeArray: {0}", Index.ToString());
        }
    }
}
