using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    public struct Shader : IEquatable<Shader>
    {
        public static readonly Shader Null = new Shader();

        public uint Id { get; internal set; }

        public Shader(uint id)
            : this()
        {
            if (Gl.IsShader(id) == 0)
                throw new ArgumentException("id is not an OpenGL shader object.", "id");
            Id = id;
        }

        public static Shader Create(ShaderType type)
        {
            unsafe
            {
                uint id = Gl.CreateShader((uint)type);
                GlHelper.GetError();
                var shader = new Shader();
                shader.Id = id;
                return shader;
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
                    Gl.GetObjectLabel(Gl.SHADER, Id, 0, &length, null);
                    byte* str = stackalloc byte[length];
                    Gl.GetObjectLabel(Gl.SHADER, Id, length, null, str);

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

                    Gl.ObjectLabel(Gl.SHADER, Id, length, str);
                    GlHelper.GetError();
                }
            }
        }

        public ShaderType Type
        {
            get
            {
                GlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Gl.GetShaderiv(Id, Gl.SHADER_TYPE, &value);
                    return (ShaderType)value;
                }
            }
        }

        public bool Status
        {
            get
            {
                GlHelper.ThrowNullException(Id);
                unsafe
                {
                    int status;
                    Gl.GetShaderiv(Id, Gl.COMPILE_STATUS, &status);
                    return status != 0;
                }
            }
        }

        public string Source
        {
            get
            {
                GlHelper.ThrowNullException(Id);
                unsafe
                {
                    int bytes;
                    Gl.GetShaderiv(Id, Gl.SHADER_SOURCE_LENGTH, &bytes);
                    byte* str = stackalloc byte[bytes];
                    Gl.GetShaderSource(Id, bytes, null, str);

                    return new string((sbyte*)str, 0, bytes, Encoding.ASCII);
                }
            }
            set
            {
                GlHelper.ThrowNullException(Id);
                unsafe
                {
                    int bytes = Encoding.ASCII.GetByteCount(value);
                    byte* strings = stackalloc byte[bytes];

                    fixed (char* source_ptr = value)
                    {
                        Encoding.ASCII.GetBytes(source_ptr, value.Length, strings, bytes);
                    }

                    Gl.ShaderSource(Id, 1, &strings, &bytes);
                    GlHelper.GetError();
                }
            }
        }

        public string InfoLog
        {
            get
            {
                GlHelper.ThrowNullException(Id);
                unsafe
                {
                    int bytes;
                    Gl.GetShaderiv(Id, Gl.INFO_LOG_LENGTH, &bytes);
                    byte* str = stackalloc byte[bytes];
                    Gl.GetShaderInfoLog(Id, bytes, null, str);

                    return new string((sbyte*)str, 0, bytes, Encoding.ASCII);
                }
            }
        }

        public bool Compile()
        {
            GlHelper.ThrowNullException(Id);
            Gl.CompileShader(Id);
            GlHelper.GetError();
            return Status;
        }

        public void Delete()
        {
            GlHelper.ThrowNullException(Id);
            Gl.DeleteShader(Id);
        }

        public override int GetHashCode()
        {
            GlHelper.ThrowNullException(Id);
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            GlHelper.ThrowNullException(Id);
            if (obj is Shader)
            {
                return Equals((Shader)obj);
            }
            return false;
        }

        public bool Equals(Shader other)
        {
            GlHelper.ThrowNullException(Id);
            return Id == other.Id;
        }

        public static bool operator ==(Shader left, Shader right)
        {
            return left.Id == right.Id;
        }

        public static bool operator !=(Shader left, Shader right)
        {
            return left.Id != right.Id;
        }

        public override string ToString()
        {
            GlHelper.ThrowNullException(Id);
            return string.Format("Shader: {0}", Id.ToString());
        }
    }
}
