using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    public static partial class Context
    {
        public static void SetUniform(int location, float v0)
        {
            Gl.Uniform1f(location, v0);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, float v0, float v1)
        {
            Gl.Uniform2f(location, v0, v1);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, float v0, float v1, float v2)
        {
            Gl.Uniform3f(location, v0, v1, v2);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, float v0, float v1, float v2, float v3)
        {
            Gl.Uniform4f(location, v0, v1, v2, v3);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, Ibasa.Numerics.Vector2f value)
        {
            Gl.Uniform2f(location, value.X, value.Y);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, Ibasa.Numerics.Vector3f value)
        {
            Gl.Uniform3f(location, value.X, value.Y, value.Z);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, Ibasa.Numerics.Vector4f value)
        {
            Gl.Uniform4f(location, value.X, value.Y, value.Z, value.W);
            GlHelper.GetError();
        }

        public static void SetUniform(int location, int v0)
        {
            Gl.Uniform1i(location, v0);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, int v0, int v1)
        {
            Gl.Uniform2i(location, v0, v1);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, int v0, int v1, int v2)
        {
            Gl.Uniform3i(location, v0, v1, v2);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, int v0, int v1, int v2, int v3)
        {
            Gl.Uniform4i(location, v0, v1, v2, v3);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, Ibasa.Numerics.Vector2i value)
        {
            Gl.Uniform2i(location, value.X, value.Y);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, Ibasa.Numerics.Vector3i value)
        {
            Gl.Uniform3i(location, value.X, value.Y, value.Z);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, Ibasa.Numerics.Vector4i value)
        {
            Gl.Uniform4i(location, value.X, value.Y, value.Z, value.W);
            GlHelper.GetError();
        }

        public static void SetUniform(int location, uint v0)
        {
            Gl.Uniform1ui(location, v0);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, uint v0, uint v1)
        {
            Gl.Uniform2ui(location, v0, v1);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, uint v0, uint v1, uint v2)
        {
            Gl.Uniform3ui(location, v0, v1, v2);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, uint v0, uint v1, uint v2, uint v3)
        {
            Gl.Uniform4ui(location, v0, v1, v2, v3);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, Ibasa.Numerics.Vector2ui value)
        {
            Gl.Uniform2ui(location, value.X, value.Y);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, Ibasa.Numerics.Vector3ui value)
        {
            Gl.Uniform3ui(location, value.X, value.Y, value.Z);
            GlHelper.GetError();
        }
        public static void SetUniform(int location, Ibasa.Numerics.Vector4ui value)
        {
            Gl.Uniform4ui(location, value.X, value.Y, value.Z, value.W);
            GlHelper.GetError();
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<float> value)
        {
            unsafe
            {
                Gl.Uniform1fv(location, value.Count, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Vector2f> value)
        {
            unsafe
            {
                Gl.Uniform2fv(location, value.Count, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Vector3f> value)
        {
            unsafe
            {
                Gl.Uniform3fv(location, value.Count, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Vector4f> value)
        {
            unsafe
            {
                Gl.Uniform4fv(location, value.Count, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<int> value)
        {
            unsafe
            {
                Gl.Uniform1iv(location, value.Count, (int*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Vector2i> value)
        {
            unsafe
            {
                Gl.Uniform2iv(location, value.Count, (int*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Vector3i> value)
        {
            unsafe
            {
                Gl.Uniform3iv(location, value.Count, (int*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Vector4i> value)
        {
            unsafe
            {
                Gl.Uniform4iv(location, value.Count, (int*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }
        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<uint> value)
        {
            unsafe
            {
                Gl.Uniform1uiv(location, value.Count, (uint*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Vector2ui> value)
        {
            unsafe
            {
                Gl.Uniform2uiv(location, value.Count, (uint*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Vector3ui> value)
        {
            unsafe
            {
                Gl.Uniform3uiv(location, value.Count, (uint*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Vector4ui> value)
        {
            unsafe
            {
                Gl.Uniform4uiv(location, value.Count, (uint*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Matrix2x2f> value)
        {
            unsafe
            {
                Gl.UniformMatrix2fv(location, value.Count, 0, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }
        public static void SetUniform(int location, Ibasa.Numerics.Matrix2x2f value)
        {
            unsafe
            {
                Gl.UniformMatrix2fv(location, 1, 0, (float*)(&value));
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Matrix3x3f> value)
        {
            unsafe
            {
                Gl.UniformMatrix3fv(location, value.Count, 0, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }
        public static void SetUniform(int location, Ibasa.Numerics.Matrix3x3f value)
        {
            unsafe
            {
                Gl.UniformMatrix3fv(location, 1, 0, (float*)(&value));
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Matrix4x4f> value)
        {
            unsafe
            {
                Gl.UniformMatrix4fv(location, value.Count, 0, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }
        public static void SetUniform(int location, Ibasa.Numerics.Matrix4x4f value)
        {
            unsafe
            {
                Gl.UniformMatrix4fv(location, 1, 0, (float*)(&value));
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Matrix2x3f> value)
        {
            unsafe
            {
                Gl.UniformMatrix2x3fv(location, value.Count, 0, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }
        public static void SetUniform(int location, Ibasa.Numerics.Matrix2x3f value)
        {
            unsafe
            {
                Gl.UniformMatrix2x3fv(location, 1, 0, (float*)(&value));
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Matrix2x4f> value)
        {
            unsafe
            {
                Gl.UniformMatrix2x4fv(location, value.Count, 0, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }
        public static void SetUniform(int location, Ibasa.Numerics.Matrix2x4f value)
        {
            unsafe
            {
                Gl.UniformMatrix2x4fv(location, 1, 0, (float*)(&value));
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Matrix3x2f> value)
        {
            unsafe
            {
                Gl.UniformMatrix3x2fv(location, value.Count, 0, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }
        public static void SetUniform(int location, Ibasa.Numerics.Matrix3x2f value)
        {
            unsafe
            {
                Gl.UniformMatrix3x2fv(location, 1, 0, (float*)(&value));
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Matrix3x4f> value)
        {
            unsafe
            {
                Gl.UniformMatrix3x4fv(location, value.Count, 0, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }
        public static void SetUniform(int location, Ibasa.Numerics.Matrix3x4f value)
        {
            unsafe
            {
                Gl.UniformMatrix3x4fv(location, 1, 0, (float*)(&value));
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Matrix4x2f> value)
        {
            unsafe
            {
                Gl.UniformMatrix4x2fv(location, value.Count, 0, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }
        public static void SetUniform(int location, Ibasa.Numerics.Matrix4x2f value)
        {
            unsafe
            {
                Gl.UniformMatrix4x2fv(location, 1, 0, (float*)(&value));
                GlHelper.GetError();
            }
        }

        public static void SetUniform(int location, Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Matrix4x3f> value)
        {
            unsafe
            {
                Gl.UniformMatrix4x3fv(location, value.Count, 0, (float*)value.Pointer.ToPointer());
                GlHelper.GetError();
            }
        }
        public static void SetUniform(int location, Ibasa.Numerics.Matrix4x3f value)
        {
            unsafe
            {
                Gl.UniformMatrix4x3fv(location, 1, 0, (float*)(&value));
                GlHelper.GetError();
            }
        }


    }
}
