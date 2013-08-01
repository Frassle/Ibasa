using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    public sealed class BindingPoint : IEquatable<BindingPoint>
    {
        readonly uint Target;
        readonly uint BufferBinding;

        internal BindingPoint(uint target, uint binding)
        {
            Target = target;
            BufferBinding = binding;
        }

        public void BufferData(long size, IntPtr data, Usage usage)
        {
            unsafe
            {
                Gl.BufferData(Target, (void*)size, data.ToPointer(), (uint)usage);
                GlHelper.GetError();
            }
        }

        public void BufferSubData(long offset, long size, IntPtr data)
        {
            unsafe
            {
                Gl.BufferSubData(Target, (void*)offset, (void*)size, data.ToPointer());
                GlHelper.GetError();
            }
        }

        public void GetBufferSubData(long offset, long size, IntPtr data)
        {
            unsafe
            {
                Gl.GetBufferSubData(Target, (void*)offset, (void*)size, data.ToPointer());
                GlHelper.GetError();
            }
        }

        public IntPtr Map(MapAccess access)
        {
            unsafe
            {
                var result = Gl.MapBuffer(Target, (uint)access);
                GlHelper.GetError();
                return new IntPtr(result);
            }
        }

        public void Unmap()
        {
            bool result = Gl.UnmapBuffer(Target) != 0;
            GlHelper.GetError();
            if (!result)
            {
                throw new OpenGLException("glUnmapBuffer returned false.");
            }
        }

        public Buffer Buffer
        {
            get
            {
                unsafe
                {
                    int value;
                    Gl.GetIntegerv(BufferBinding, &value);
                    Buffer buffer = new Buffer();
                    buffer.Id = (uint)value;
                    return buffer;
                }
            }
            set
            {
                Gl.BindBuffer(Target, value.Id);
                GlHelper.GetError();
            }
        }

        public long Size
        {
            get
            {
                unsafe
                {
                    long value;
                    Gl.GetBufferParameteri64v(Target, Gl.BUFFER_SIZE, &value);
                    GlHelper.GetError();
                    return value;
                }
            }
        }

        public MapAccess Access
        {
            get
            {
                unsafe
                {
                    int value;
                    Gl.GetBufferParameteriv(Target, Gl.BUFFER_ACCESS, &value);
                    GlHelper.GetError();
                    return (MapAccess)value;
                }
            }
        }

        public Usage Usage
        {
            get
            {
                unsafe
                {
                    int value;
                    Gl.GetBufferParameteriv(Target, Gl.BUFFER_USAGE, &value);
                    GlHelper.GetError();
                    return (Usage)value;
                }
            }
        }

        public int AccessFlags
        {
            get
            {
                unsafe
                {
                    int value;
                    Gl.GetBufferParameteriv(Target, Gl.BUFFER_ACCESS_FLAGS, &value);
                    GlHelper.GetError();
                    return value;
                }
            }
        }

        public bool Mapped
        {
            get
            {
                unsafe
                {
                    int value;
                    Gl.GetBufferParameteriv(Target, Gl.BUFFER_MAPPED, &value);
                    GlHelper.GetError();
                    return value != 0;
                }
            }
        }

        public IntPtr MapPointer
        {
            get
            {
                unsafe
                {
                    void* value;
                    Gl.GetBufferPointerv(Target, Gl.BUFFER_MAP_POINTER, &value);
                    GlHelper.GetError();
                    return new IntPtr(value);
                }
            }
        }

        public long MapOffset
        {
            get
            {
                unsafe
                {
                    long value;
                    Gl.GetBufferParameteri64v(Target, Gl.BUFFER_MAP_OFFSET, &value);
                    GlHelper.GetError();
                    return value;
                }
            }
        }

        public long MapLength
        {
            get
            {
                unsafe
                {
                    long value;
                    Gl.GetBufferParameteri64v(Target, Gl.BUFFER_MAP_LENGTH, &value);
                    GlHelper.GetError();
                    return value;
                }
            }
        }

        public override int GetHashCode()
        {
            return Target.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is BindingPoint)
            {
                return Equals((BindingPoint)obj);
            }
            return false;
        }

        public bool Equals(BindingPoint other)
        {
            return !Object.ReferenceEquals(other, null) && Target == other.Target;
        }

        public static bool operator ==(BindingPoint left, BindingPoint right)
        {
            if (Object.ReferenceEquals(left, right)) return true;
            if (Object.ReferenceEquals(left, null)) return false;
            if (Object.ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(BindingPoint left, BindingPoint right)
        {
            if (Object.ReferenceEquals(left, right)) return false;
            if (Object.ReferenceEquals(left, null)) return true;
            if (Object.ReferenceEquals(right, null)) return true;

            return !left.Equals(right);
        }

        public override string ToString()
        {
            GlHelper.ThrowNullException(Target);
            return string.Format("BufferBinding: {0}", Target.ToString());
        }
    }

    public static partial class Context
    {
        static BindingPoint _ArrayBuffer = new BindingPoint(Gl.ARRAY_BUFFER, Gl.ARRAY_BUFFER_BINDING);
        public static BindingPoint ArrayBuffer
        {
            get
            {
                return _ArrayBuffer;
            }
        }

        static BindingPoint _ElementArrayBuffer = new BindingPoint(Gl.ELEMENT_ARRAY_BUFFER, Gl.ELEMENT_ARRAY_BUFFER_BINDING);
        public static BindingPoint ElementArrayBuffer
        {
            get
            {
                return _ElementArrayBuffer;
            }
        }

        static BindingPoint _UniformBuffer = new BindingPoint(Gl.UNIFORM_BUFFER, Gl.UNIFORM_BUFFER_BINDING);
        public static BindingPoint UniformBuffer
        {
            get
            {
                return _UniformBuffer;
            }
        }
    }
}
