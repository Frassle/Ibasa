using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    public struct Texture : IEquatable<Texture>
    {
        public struct TextureBinding : IEquatable<TextureBinding>
        {
            public uint Target { get; private set; }

            public TextureBinding(uint target)
                : this()
            {
                Target = target;
            }

            public void Bind(Texture texture)
            {
                GlHelper.ThrowNullException(Target);
                Gl.BindTexture(Target, texture.Id);
                GlHelper.GetError();
            }

            public TextureFilter MinFilter
            {
                get
                {
                    GlHelper.ThrowNullException(Target);
                    unsafe
                    {
                        int value;
                        Gl.GetTexParameteriv(Target, Gl.TEXTURE_MIN_FILTER, &value);
                        GlHelper.GetError();
                        return (TextureFilter)value;
                    }
                }
                set
                {
                    GlHelper.ThrowNullException(Target);
                    Gl.TexParameteri(Target, Gl.TEXTURE_MIN_FILTER, (int)value);
                    GlHelper.GetError();
                }
            }

            public TextureFilter MagFilter
            {
                get
                {
                    GlHelper.ThrowNullException(Target);
                    unsafe
                    {
                        int value;
                        Gl.GetTexParameteriv(Target, Gl.TEXTURE_MAG_FILTER, &value);
                        GlHelper.GetError();
                        return (TextureFilter)value;
                    }
                }
                set
                {
                    GlHelper.ThrowNullException(Target);
                    Gl.TexParameteri(Target, Gl.TEXTURE_MAG_FILTER, (int)value);
                    GlHelper.GetError();
                }
            }

            public TextureWrapMode WrapS
            {
                get
                {
                    GlHelper.ThrowNullException(Target);
                    unsafe
                    {
                        int value;
                        Gl.GetTexParameteriv(Target, Gl.TEXTURE_WRAP_S, &value);
                        GlHelper.GetError();
                        return (TextureWrapMode)value;
                    }
                }
                set
                {
                    GlHelper.ThrowNullException(Target);
                    Gl.TexParameteri(Target, Gl.TEXTURE_WRAP_S, (int)value);
                    GlHelper.GetError();
                }
            }

            public TextureWrapMode WrapT
            {
                get
                {
                    GlHelper.ThrowNullException(Target);
                    unsafe
                    {
                        int value;
                        Gl.GetTexParameteriv(Target, Gl.TEXTURE_WRAP_T, &value);
                        GlHelper.GetError();
                        return (TextureWrapMode)value;
                    }
                }
                set
                {
                    GlHelper.ThrowNullException(Target);
                    Gl.TexParameteri(Target, Gl.TEXTURE_WRAP_T, (int)value);
                    GlHelper.GetError();
                }
            }

            public TextureWrapMode WrapR
            {
                get
                {
                    GlHelper.ThrowNullException(Target);
                    unsafe
                    {
                        int value;
                        Gl.GetTexParameteriv(Target, Gl.TEXTURE_WRAP_R, &value);
                        GlHelper.GetError();
                        return (TextureWrapMode)value;
                    }
                }
                set
                {
                    GlHelper.ThrowNullException(Target);
                    Gl.TexParameteri(Target, Gl.TEXTURE_WRAP_R, (int)value);
                    GlHelper.GetError();
                }
            }

            public override int GetHashCode()
            {
                GlHelper.ThrowNullException(Target);
                return Target.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                GlHelper.ThrowNullException(Target);
                if (obj is TextureBinding)
                {
                    return Equals((TextureBinding)obj);
                }
                return false;
            }

            public bool Equals(TextureBinding other)
            {
                GlHelper.ThrowNullException(Target);
                return Target == other.Target;
            }

            public static bool operator ==(TextureBinding left, TextureBinding right)
            {
                return left.Target == right.Target;
            }

            public static bool operator !=(TextureBinding left, TextureBinding right)
            {
                return left.Target != right.Target;
            }

            public override string ToString()
            {
                GlHelper.ThrowNullException(Target);
                return string.Format("TextureBinding: {0}", Target.ToString());
            }
        }

        public static TextureBinding Texture1D
        {
            get
            {
                return new TextureBinding(Gl.TEXTURE_1D);
            }
        }

        public static TextureBinding Texture2D
        {
            get
            {
                return new TextureBinding(Gl.TEXTURE_2D);
            }
        }

        public static TextureBinding Texture3D
        {
            get
            {
                return new TextureBinding(Gl.TEXTURE_3D);
            }
        }

        public static TextureBinding Texture1DArray
        {
            get
            {
                return new TextureBinding(Gl.TEXTURE_1D_ARRAY);
            }
        }

        public static TextureBinding Texture2DArray
        {
            get
            {
                return new TextureBinding(Gl.TEXTURE_2D_ARRAY);
            }
        }

        public static TextureBinding TextureRectangle
        {
            get
            {
                return new TextureBinding(Gl.TEXTURE_RECTANGLE);
            }
        }

        public static TextureBinding TextureCubemap
        {
            get
            {
                return new TextureBinding(Gl.TEXTURE_CUBE_MAP);
            }
        }

        public static TextureBinding TextureCubemapArray
        {
            get
            {
                return new TextureBinding(Gl.TEXTURE_CUBE_MAP_ARRAY);
            }
        }

        public static TextureBinding Texture2DMultisample
        {
            get
            {
                return new TextureBinding(Gl.TEXTURE_2D_MULTISAMPLE);
            }
        }

        public static TextureBinding Texture2DMultisampleArray
        {
            get
            {
                return new TextureBinding(Gl.TEXTURE_2D_MULTISAMPLE_ARRAY);
            }
        }
        
        public static readonly Texture Null = new Texture();

        public uint Id { get; internal set; }

        public Texture(uint id)
            : this()
        {
            if (Gl.IsTexture(id) == 0)
                throw new ArgumentException("id is not an OpenGL texture object.", "id");
            Id = id;
        }

        public static Texture Create()
        {
            unsafe
            {
                uint id;
                Gl.GenTextures(1, &id);
                GlHelper.GetError();
                var texture = new Texture();
                texture.Id = id;
                return texture;
            }
        }

        public static void Create(Texture[] textures, int index, int count)
        {
            if (textures == null)
                throw new ArgumentNullException("textures");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", index, "index is less than 0.");
            if (index + count > textures.Length)
                throw new ArgumentOutOfRangeException("count", count, "index + count is greater than textures.Length.");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", count, "count is less than 0.");

            unsafe
            {
                uint* ids = stackalloc uint[count];
                Gl.GenTextures(count, ids);
                GlHelper.GetError();
                for (int i = 0; i < count; ++i)
                {
                    var texture = new Texture();
                    texture.Id = ids[i];
                    textures[index + i] = texture;
                }
            }
        }

        public void Delete()
        {
            GlHelper.ThrowNullException(Id);
            unsafe
            {
                uint id = Id;
                Gl.DeleteTextures(1, &id);
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
                    Gl.GetObjectLabel(Gl.TEXTURE, Id, 0, &length, null);
                    byte* str = stackalloc byte[length];
                    Gl.GetObjectLabel(Gl.TEXTURE, Id, length, null, str);

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

                    Gl.ObjectLabel(Gl.TEXTURE, Id, length, str);
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
            if (obj is Texture)
            {
                return Equals((Texture)obj);
            }
            return false;
        }

        public bool Equals(Texture other)
        {
            GlHelper.ThrowNullException(Id);
            return Id == other.Id;
        }

        public static bool operator ==(Texture left, Texture right)
        {
            return left.Id == right.Id;
        }

        public static bool operator !=(Texture left, Texture right)
        {
            return left.Id != right.Id;
        }

        public override string ToString()
        {
            GlHelper.ThrowNullException(Id);
            return string.Format("Texture: {0}", Id.ToString());
        }
    }
}