using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    public struct Buffer : IEquatable<Buffer>
    {
        public static readonly Buffer Null = new Buffer();

        public uint Id { get; internal set; }

        public Buffer(uint id)
            : this()
        {
            if (Gl.IsBuffer(id) == 0)
                throw new ArgumentException("id is not an OpenGL buffer object.", "id");
            Id = id;
        }

        public static Buffer Create()
        {
            unsafe
            {
                uint id;
                Gl.GenBuffers(1, &id);
                GlHelper.GetError();
                var buffer = new Buffer();
                buffer.Id = id;
                return buffer;
            }
        }

        public static void Create(Buffer[] buffers, int index, int count)
        {
            if (buffers == null)
                throw new ArgumentNullException("buffers");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", index, "index is less than 0.");
            if (index + count > buffers.Length)
                throw new ArgumentOutOfRangeException("count", count, "index + count is greater than buffers.Length.");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", count, "count is less than 0.");

            unsafe
            {
                uint* ids = stackalloc uint[count];
                Gl.GenBuffers(count, ids);
                GlHelper.GetError();
                for (int i = 0; i < count; ++i)
                {
                    var buffer = new Buffer();
                    buffer.Id = ids[i];
                    buffers[index + i] = buffer;
                }
            }
        }

        public void Delete()
        {
            GlHelper.ThrowNullException(Id);
            unsafe
            {
                uint id = Id;
                Gl.DeleteBuffers(1, &id);
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
                    Gl.GetObjectLabel(Gl.BUFFER, Id, 0, &length, null);
                    byte* str = stackalloc byte[length];
                    Gl.GetObjectLabel(Gl.BUFFER, Id, length, null, str);

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

                    Gl.ObjectLabel(Gl.BUFFER, Id, length, str);
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
            if (obj is Buffer)
            {
                return Equals((Buffer)obj);
            }
            return false;
        }

        public bool Equals(Buffer other)
        {
            GlHelper.ThrowNullException(Id);
            return Id == other.Id;
        }

        public static bool operator ==(Buffer left, Buffer right)
        {
            return left.Id == right.Id;
        }

        public static bool operator !=(Buffer left, Buffer right)
        {
            return left.Id != right.Id;
        }

        public override string ToString()
        {
            GlHelper.ThrowNullException(Id);
            return string.Format("Buffer: {0}", Id.ToString());
        }
    }
}
