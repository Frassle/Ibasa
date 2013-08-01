using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    public static partial class Context
    {
        public static void GetError()
        {
            GlHelper.GetError();
        }

        public static string Vendor
        {
            get
            {
                unsafe
                {
                    byte* str = Gl.GetString(Gl.VENDOR);
                    return new string((sbyte*)str);
                }
            }
        }

        public static string Renderer
        {
            get
            {
                unsafe
                {
                    byte* str = Gl.GetString(Gl.RENDERER);
                    return new string((sbyte*)str);
                }
            }
        }

        public static string Version
        {
            get
            {
                unsafe
                {
                    byte* str = Gl.GetString(Gl.VERSION);
                    return new string((sbyte*)str);
                }
            }
        }

        public static string Extensions
        {
            get
            {
                unsafe
                {
                    byte* str = Gl.GetString(Gl.EXTENSIONS);
                    return new string((sbyte*)str);
                }
            }
        }

        public static Ibasa.Numerics.Colorf ClearColor
        {
            get
            {
                unsafe
                {
                    float* value = stackalloc float[4];
                    Gl.GetFloatv(Gl.COLOR_CLEAR_VALUE, value);
                    return new Numerics.Colorf(value[0], value[1], value[2], value[3]);
                }
            }
            set
            {
                Gl.ClearColor(value.R, value.G, value.B, value.A);
                GlHelper.GetError();
            }
        }

        public static int ClearStencil
        {
            get
            {
                unsafe
                {
                    int value;
                    Gl.GetIntegerv(Gl.STENCIL_CLEAR_VALUE, &value);
                    return value;
                }
            }
            set
            {
                Gl.ClearStencil(value);
                GlHelper.GetError();
            }
        }

        public static double ClearDepth
        {
            get
            {
                unsafe
                {
                    double value;
                    Gl.GetDoublev(Gl.DEPTH_CLEAR_VALUE, &value);
                    return value;
                }
            }
            set
            {
                Gl.ClearDepth(value);
                GlHelper.GetError();
            }
        }

        public static void Clear(ClearFlags clearFlags)
        {
            Gl.Clear((uint)clearFlags);
            GlHelper.GetError();
        }

        public static class Rasterization
        {
            public static Structure<PolygonMode, PolygonMode> PolygonMode
            {
                get
                {
                    unsafe
                    {
                        int* modes = stackalloc int[2];
                        Gl.GetIntegerv(Gl.POLYGON_MODE, modes);
                        return Structure.Create(
                            (PolygonMode)modes[0], 
                            (PolygonMode)modes[1]);
                    }
                }
                set
                {
                    Gl.PolygonMode(Gl.FRONT, (uint)value.Item1);
                    Gl.PolygonMode(Gl.BACK, (uint)value.Item2);
                    Gl.GetError();
                }
            }

            public static void SetPolygonMode(Face face, PolygonMode mode)
            {
                Gl.PolygonMode((uint)face, (uint)mode);
                Gl.GetError();
            }

            public static Structure<double, double> DepthRange
            {
                get
                {
                    unsafe
                    {
                        double* values = stackalloc double[2];
                        Gl.GetDoublev(Gl.DEPTH_RANGE, values);
                        return Structure.Create(
                            values[0],
                            values[1]);
                    }
                }
                set
                {
                    Gl.DepthRange(value.Item1, value.Item2);
                    Gl.GetError();
                }
            }

            public static bool DepthTest
            {
                get
                {
                    return Gl.IsEnabled(Gl.DEPTH_TEST) != 0;
                }
                set
                {
                    if (value)
                    {
                        Gl.Enable(Gl.DEPTH_TEST);
                    }
                    else
                    {
                        Gl.Disable(Gl.DEPTH_TEST);
                    }
                }
            }

            public static Comparison DepthFunction
            {
                get
                {
                    unsafe
                    {
                        int value;
                        Gl.GetIntegerv(Gl.DEPTH_FUNC, &value);
                        return (Comparison)value;
                    }
                }
                set
                {
                    Gl.DepthFunc((uint)value);
                    GlHelper.GetError();
                }
            }

            public static bool DepthMask
            {
                get
                {
                    unsafe
                    {
                        int value;
                        Gl.GetIntegerv(Gl.DEPTH_WRITEMASK, &value);
                        return value != 0;
                    }
                }
                set
                {
                    Gl.DepthMask((byte)(value ? 1 : 0));
                }
            }

            public static bool DepthClamp
            {
                get
                {
                    unsafe
                    {
                        int value;
                        Gl.GetIntegerv(Gl.DEPTH_CLAMP, &value);
                        return value != 0;
                    }
                }
                set
                {
                    if (value)
                    {
                        Gl.Enable(Gl.DEPTH_CLAMP);
                    }
                    else
                    {
                        Gl.Disable(Gl.DEPTH_CLAMP);
                    }
                }
            }

            /// <summary>
            /// Determines if a triangle is front- or back-facing. If this 
            /// parameter is TRUE, a triangle will be considered front-facing if 
            /// its vertices are counter-clockwise on the render target and 
            /// considered back-facing if they are clockwise. If this parameter is 
            /// FALSE, the opposite is true.
            /// </summary>
            public static bool FrontCounterClockwise
            {
                get
                {
                    unsafe
                    {
                        int value;
                        Gl.GetIntegerv(Gl.FRONT_FACE, &value);
                        return value == Gl.CCW;
                    }
                }
                set
                {
                    Gl.FrontFace(value ? Gl.CCW : Gl.CW);
                }
            }

            public static Face CullFaceMode
            {
                get
                {
                    unsafe
                    {
                        int value;
                        Gl.GetIntegerv(Gl.CULL_FACE_MODE, &value);
                        return (Face)value;
                    }
                }
                set
                {
                    //Gl.CullFace((uint)value);
                    GlHelper.GetError();
                }
            }

            public static bool CullFace
            {
                get
                {
                    return Gl.IsEnabled(Gl.CULL_FACE) != 0;
                }
                set
                {
                    if (value)
                    {
                        Gl.Enable(Gl.CULL_FACE);
                    }
                    else
                    {
                        Gl.Disable(Gl.CULL_FACE);
                    }
                }
            }

            public static Ibasa.Numerics.Geometry.Rectanglei ScissorBox
            {
                get
                {
                    unsafe
                    {
                        int* values = stackalloc int[4];
                        Gl.GetIntegerv(Gl.SCISSOR_BOX, values);
                        return new Numerics.Geometry.Rectanglei(values[0], values[1], values[2], values[3]);
                    }
                }
                set
                {
                    Gl.Scissor(value.X, value.Y, value.Width, value.Height);
                    GlHelper.GetError();
                }
            }

            public static bool ScissorTest
            {
                get
                {
                    return Gl.IsEnabled(Gl.SCISSOR_TEST) != 0;
                }
                set
                {
                    if (value)
                    {
                        Gl.Enable(Gl.SCISSOR_TEST);
                    }
                    else
                    {
                        Gl.Disable(Gl.SCISSOR_TEST);
                    }
                }
            }

            public static float LineWidth
            {
                get
                {
                    unsafe
                    {
                        float value;
                        Gl.GetFloatv(Gl.LINE_WIDTH, &value);
                        return value;
                    }
                }
                set
                {
                    Gl.LineWidth(value);
                    GlHelper.GetError();
                }
            }

            public static float PointSize
            {
                get
                {
                    unsafe
                    {
                        float value;
                        Gl.GetFloatv(Gl.POINT_SIZE, &value);
                        return value;
                    }
                }
                set
                {
                    Gl.PointSize(value);
                    GlHelper.GetError();
                }
            }

            public static Ibasa.Numerics.Geometry.Size2i MaxViewportDimensions
            {
                get
                {
                    unsafe
                    {
                        int* value = stackalloc int[2];
                        Gl.GetIntegerv(Gl.MAX_VIEWPORT_DIMS, value);
                        return new Numerics.Geometry.Size2i(value[0], value[1]);
                    }
                }
            }

            public static Ibasa.Numerics.Geometry.Rectanglei Viewport
            {
                get
                {
                    unsafe
                    {
                        int* value = stackalloc int[4];
                        Gl.GetIntegerv(Gl.VIEWPORT, value);
                        return new Numerics.Geometry.Rectanglei(value[0], value[1], value[2], value[3]);
                    }
                }
                set
                {
                    Gl.Viewport(value.X, value.Y, value.Width, value.Height);
                    GlHelper.GetError();
                }
            }
        }

        public static void DrawArrays(PrimitiveTopology topology, int first, int count)
        {
            Gl.DrawArrays((uint)topology, first, count);
            GlHelper.GetError();
        }

        public static void DrawElements(PrimitiveTopology topology, int count, DataType type, int offset)
        {
            unsafe
            {                
                Gl.DrawElements((uint)topology, count, (uint)type, (void*)offset);
                GlHelper.GetError();
            }
        }

        public static void DrawElements(PrimitiveTopology topology, int count, DataType type, IntPtr indices)
        {
            unsafe
            {
                Gl.DrawElements((uint)topology, count, (uint)type, indices.ToPointer());
                GlHelper.GetError();
            }
        }

        public static void DrawElements(PrimitiveTopology topology, int count, DataType type, int offset, int basevertex)
        {
            unsafe
            {
                Gl.DrawElementsBaseVertex((uint)topology, count, (uint)type, (void*)offset, basevertex);
                GlHelper.GetError();
            }
        }

        public static void DrawElements(PrimitiveTopology topology, int count, DataType type, IntPtr indices, int basevertex)
        {
            unsafe
            {
                Gl.DrawElementsBaseVertex((uint)topology, count, (uint)type, indices.ToPointer(), basevertex);
                GlHelper.GetError();
            }
        }

        public static VertexArray VertexArray
        {
            get
            {
                unsafe
                {
                    int value;
                    Gl.GetIntegerv(Gl.VERTEX_ARRAY_BINDING, &value);
                    VertexArray vao = new VertexArray();
                    vao.Id = (uint)value;
                    return vao;
                }                
            }
            set
            {
                Gl.BindVertexArray(value.Id);
                GlHelper.GetError();
            }
        }

        static Ibasa.Collections.Immutable.ImmutableArray<VertexAttributeArray> CreateVertexAttributeArrays()
        {
            unsafe
            {
                int count;
                Gl.GetIntegerv(Gl.MAX_VERTEX_ATTRIBS, &count);
                var array =
                    Ibasa.Collections.Immutable.ImmutableArray<VertexAttributeArray>.Create(
                    i =>
                    {
                        return new VertexAttributeArray((uint)i);
                    }, count);
                return array;
            }
        }

        static Ibasa.Collections.Immutable.ImmutableArray<VertexAttributeArray> _VertexAttributeArrays =
            CreateVertexAttributeArrays();
        public static Ibasa.Collections.Immutable.ImmutableArray<VertexAttributeArray> VertexAttributeArrays
        {
            get
            {
                return _VertexAttributeArrays;
            }
        }
    }
}
