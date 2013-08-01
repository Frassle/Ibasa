using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    public struct VertexArray : IEquatable<VertexArray>
    {
        public static readonly VertexArray Null = new VertexArray();

        public uint Id { get; internal set; }

        public VertexArray(uint id)
            : this()
        {
            if (Gl.IsVertexArray(id) == 0)
                throw new ArgumentException("id is not an OpenGL vertex array object.", "id");
            Id = id;
        }

        public static VertexArray Create()
        {
            unsafe
            {
                uint id;
                Gl.GenVertexArrays(1, &id);
                GlHelper.GetError();
                var array = new VertexArray();
                array.Id = id;
                return array;
            }
        }

        public static void Create(VertexArray[] arrays, int index, int count)
        {
            if (arrays == null)
                throw new ArgumentNullException("arrays");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", index, "index is less than 0.");
            if (index + count > arrays.Length)
                throw new ArgumentOutOfRangeException("count", count, "index + count is greater than arrays.Length.");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", count, "count is less than 0.");

            unsafe
            {
                uint* ids = stackalloc uint[count];
                Gl.GenVertexArrays(count, ids);
                GlHelper.GetError();
                for (int i = 0; i < count; ++i)
                {
                    var array = new VertexArray();
                    array.Id = ids[i];
                    arrays[index + i] = array;
                }
            }
        }

        public void Delete()
        {
            GlHelper.ThrowNullException(Id);
            unsafe
            {
                uint id = Id;
                Gl.DeleteVertexArrays(1, &id);
            }
        }

        public string Label
        {
            get
            {
                GlHelper.ThrowNullException(Id);
                unsafe
                {
                    int length;
                    Gl.GetObjectLabel(Gl.VERTEX_ARRAY, Id, 0, &length, null);
                    byte* str = stackalloc byte[length];
                    Gl.GetObjectLabel(Gl.VERTEX_ARRAY, Id, length, null, str);

                    int charCount = Encoding.ASCII.GetCharCount(str, length);
                    char[] chars = new char[length];

                    return length == 0
                        ? string.Empty
                        : new string((sbyte*)str, 0, length, Encoding.ASCII);
                }
            }
            set
            {
                GlHelper.ThrowNullException(Id);
                unsafe
                {
                    int length = Encoding.ASCII.GetByteCount(value);
                    byte* str = stackalloc byte[length];

                    fixed (char* source_ptr = value)
                    {
                        Encoding.ASCII.GetBytes(source_ptr, value.Length, str, length);
                    }

                    Gl.ObjectLabel(Gl.VERTEX_ARRAY, Id, length, str);
                    GlHelper.GetError();
                }
            }
        }

        public override int GetHashCode()
        {
            GlHelper.ThrowNullException(Id);
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            GlHelper.ThrowNullException(Id);
            if (obj is VertexArray)
            {
                return Equals((VertexArray)obj);
            }
            return false;
        }

        public bool Equals(VertexArray other)
        {
            GlHelper.ThrowNullException(Id);
            return Id == other.Id;
        }

        public static bool operator ==(VertexArray left, VertexArray right)
        {
            return left.Id == right.Id;
        }

        public static bool operator !=(VertexArray left, VertexArray right)
        {
            return left.Id != right.Id;
        }

        public override string ToString()
        {
            GlHelper.ThrowNullException(Id);
            return string.Format("VertexArray: {0}", Id.ToString());
        }
    }
}
