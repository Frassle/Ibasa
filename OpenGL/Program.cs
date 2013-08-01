using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    public struct Program : IEquatable<Program>
    {
        public static readonly Program Null = new Program();

        public uint Id { get; internal set; }

        public Program(uint id)
            : this()
        {
            if (Gl.IsProgram(id) == 0)
                throw new ArgumentException("id is not an OpenGL program object.", "id");
            Id = id;
        }

        public static Program Create()
        {
            unsafe
            {
                uint id = Gl.CreateProgram();
                GlHelper.GetError();
                var program = new Program();
                program.Id = id;
                return program;
            }
        }

        public void AttachShader(Shader shader)
        {
            GlHelper.ThrowNullException(Id);
            Gl.AttachShader(Id, shader.Id);
            GlHelper.GetError();
        }

        public void DetachShader(Shader shader)
        {
            GlHelper.ThrowNullException(Id);
            Gl.DetachShader(Id, shader.Id);
            GlHelper.GetError();
        }

        public string Label
        {
            get
            {
                GlHelper.ThrowNullException(Id);
                unsafe
                {
                    int length;
                    Gl.GetObjectLabel(Gl.PROGRAM, Id, 0, &length, null);
                    byte* str = stackalloc byte[length];
                    Gl.GetObjectLabel(Gl.PROGRAM, Id, length, null, str);

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

                    Gl.ObjectLabel(Gl.PROGRAM, Id, length, str);
                    GlHelper.GetError();
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
                    Gl.GetProgramiv(Id, Gl.LINK_STATUS, &status);
                    return status != 0;
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
                    Gl.GetProgramiv(Id, Gl.INFO_LOG_LENGTH, &bytes);
                    byte* str = stackalloc byte[bytes];
                    Gl.GetProgramInfoLog(Id, bytes, null, str);

                    return new string((sbyte*)str, 0, bytes, Encoding.ASCII);
                }
            }
        }

        public bool Link()
        {
            GlHelper.ThrowNullException(Id);
            Gl.LinkProgram(Id);
            GlHelper.GetError();
            return Status;
        }

        public int GetUniformLocation(string name)
        {
            GlHelper.ThrowNullException(Id);
            unsafe
            {
                int bytes = Encoding.ASCII.GetByteCount(name);
                byte* str = stackalloc byte[bytes + 1];
                str[bytes] = 0; //null terminated string

                fixed (char* name_ptr = name)
                {
                    Encoding.ASCII.GetBytes(name_ptr, name.Length, str, bytes);
                }

                var location = Gl.GetUniformLocation(Id, str);
                GlHelper.GetError();
                return location;
            }
        }

        public void Delete()
        {
            GlHelper.ThrowNullException(Id);
            Gl.DeleteProgram(Id);
        }

        public override int GetHashCode()
        {
            GlHelper.ThrowNullException(Id);
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            GlHelper.ThrowNullException(Id);
            if (obj is Program)
            {
                return Equals((Program)obj);
            }
            return false;
        }

        public bool Equals(Program other)
        {
            GlHelper.ThrowNullException(Id);
            return Id == other.Id;
        }

        public static bool operator ==(Program left, Program right)
        {
            return left.Id == right.Id;
        }

        public static bool operator !=(Program left, Program right)
        {
            return left.Id != right.Id;
        }

        public override string ToString()
        {
            GlHelper.ThrowNullException(Id);
            return string.Format("Program: {0}", Id.ToString());
        }
    }

    public static partial class Context
    {
        public static Program CurrentProgram
        {
            get
            {
                unsafe
                {
                    int value;
                    Gl.GetIntegerv(Gl.CURRENT_PROGRAM, &value);
                    var program = new Program();
                    program.Id = (uint)value;
                    return program;
                }
            }
            set
            {
                Gl.UseProgram(value.Id);
                GlHelper.GetError();
            }
        }
    }
}
