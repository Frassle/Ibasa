using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    [Flags]
    public enum ClearFlags : uint
    {
        Color = Gl.COLOR_BUFFER_BIT,
        Depth = Gl.DEPTH_BUFFER_BIT,
        Stencil = Gl.STENCIL_BUFFER_BIT,

        All = Color | Depth | Stencil,
    }

    public enum Face : uint
    {
        Front = Gl.FRONT,
        Back = Gl.BACK,
        FrontAndBack = Gl.FRONT_AND_BACK,
    }

    public enum Comparison : uint
    {
        Never = Gl.NEVER,
        Less = Gl.LESS,
        Equal = Gl.EQUAL,
        LessEqual = Gl.LEQUAL,
        Greater = Gl.GREATER,
        NotEqual = Gl.NOTEQUAL,
        GreaterEqual = Gl.GEQUAL,
        Always = Gl.ALWAYS,
    }

    public enum PolygonMode : uint
    {
        Point = Gl.POINT,
        Line = Gl.LINE,
        Fill = Gl.FILL,
    }

    public enum PrimitiveTopology : uint
    {
        PointList = Gl.POINTS,
        LineStrip = Gl.LINE_STRIP,
        LineLoop = Gl.LINE_LOOP,
        LineList = Gl.LINES,
        LineStripAdjacency = Gl.LINE_STRIP_ADJACENCY,
        LineListAdjacency = Gl.LINES_ADJACENCY,
        TriangleStrip = Gl.TRIANGLE_STRIP,
        TriangleFan = Gl.TRIANGLE_FAN,
        TriangleList = Gl.TRIANGLES,
        TirangleStripAdjacency = Gl.TRIANGLE_STRIP_ADJACENCY,
        TriangleListAdjacency = Gl.TRIANGLES_ADJACENCY,
        Patches = Gl.PATCHES,
    }

    public enum ShaderType : uint
    {
        Vertex = Gl.VERTEX_SHADER,
        Fragment = Gl.FRAGMENT_SHADER,
    }

    public enum DataType : uint
    {
        UnsignedByte = Gl.UNSIGNED_BYTE,
        Byte = Gl.BYTE,
        UnsignedShort = Gl.UNSIGNED_SHORT,
        Short = Gl.SHORT,
        UnsignedInt = Gl.UNSIGNED_INT,
        Int = Gl.INT,
        Float = Gl.FLOAT,
        Double = Gl.DOUBLE,
    }

    public enum SyncStatus : uint
    {
        AlreadySignaled = Gl.ALREADY_SIGNALED,
        TimeoutExpired = Gl.TIMEOUT_EXPIRED,
        ConditionSatisfied = Gl.CONDITION_SATISFIED,
    }

    public enum MapAccess : uint
    {
        Read = Gl.READ_ONLY,
        Write = Gl.WRITE_ONLY,
        WriteInvalidateRegion = Gl.READ_WRITE,
    }

    public enum Usage : uint
    {
        StreamDraw = Gl.STREAM_DRAW,
        StreamRead = Gl.STREAM_READ,
        StreamCopy = Gl.STREAM_COPY,
        StaticDraw = Gl.STATIC_DRAW,
        StaticRead = Gl.STATIC_READ,
        StaticCopy = Gl.STATIC_COPY,
        DynamicDraw = Gl.DYNAMIC_DRAW,
        DynamicRead = Gl.DYNAMIC_READ,
        DynamicCopy = Gl.DYNAMIC_COPY,
    }

    public enum TextureFilter : uint
    {
        Nearest = Gl.NEAREST,
        Linear = Gl.LINEAR,

        NearestMipmapNearest = Gl.NEAREST_MIPMAP_NEAREST,
        LinearMipmapNearest = Gl.LINEAR_MIPMAP_NEAREST,
        NearestMipmapLinear = Gl.NEAREST_MIPMAP_LINEAR,
        LinearMipmapLinear = Gl.LINEAR_MIPMAP_LINEAR,
    }

    public enum TextureWrapMode : uint
    {
        ClampToEdge = Gl.CLAMP_TO_EDGE,
        ClampToBorder = Gl.CLAMP_TO_BORDER,
        MirroredRepeat = Gl.MIRRORED_REPEAT,
        Repeat = Gl.REPEAT,
    }
}
