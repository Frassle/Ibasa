using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    public unsafe class Gl
    {
        public Gl(Func<string, IntPtr> getProcAddress)
        {
            var fields = typeof(Gl).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var addr = getProcAddress(field.Name);

                if (addr == IntPtr.Zero)
                {
                    throw new OpenGLException(string.Format("Could not load function: {0}", field.Name));
                }
                else
                {
                    field.SetValue(this, addr);
                }
            }
        }

        private static Gl context;
        public static Gl Current { get { return context; } set { context = value; } }

        #region Base GL types

        //typedef unsigned int GLenum;
        //typedef unsigned char GLboolean;
        //typedef unsigned int GLbitfield;
        //typedef signed char GLbyte;
        //typedef short GLshort;
        //typedef int GLint;
        //typedef int GLsizei;
        //typedef unsigned char GLubyte;
        //typedef unsigned short GLushort;
        //typedef unsigned int GLuint;
        //typedef unsigned short GLhalf;
        //typedef float GLfloat;
        //typedef float GLclampf;
        //typedef double GLdouble;
        //typedef double GLclampd;
        //typedef void GLvoid;

        #endregion


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 1.0/1.1 enums (there is no VERSION_1_0 token)
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_1_1

        /* AttribMask */
        public const uint DEPTH_BUFFER_BIT = 0x00000100;	// AttribMask
        public const uint STENCIL_BUFFER_BIT = 0x00000400;	// AttribMask
        public const uint COLOR_BUFFER_BIT = 0x00004000;	// AttribMask
        /* Boolean */
        public const uint FALSE = 0;		// Boolean
        public const uint TRUE = 1;		// Boolean
        /* BeginMode */

        public const uint POINTS = 0x0000;	// BeginMode
        public const uint LINES = 0x0001;	// BeginMode
        public const uint LINE_LOOP = 0x0002;	// BeginMode
        public const uint LINE_STRIP = 0x0003;	// BeginMode
        public const uint TRIANGLES = 0x0004;	// BeginMode
        public const uint TRIANGLE_STRIP = 0x0005;	// BeginMode
        public const uint TRIANGLE_FAN = 0x0006;	// BeginMode
        public const uint QUADS = 0x0007;	// BeginMode
        /* AlphaFunction */

        public const uint NEVER = 0x0200;	// AlphaFunction
        public const uint LESS = 0x0201;	// AlphaFunction
        public const uint EQUAL = 0x0202;	// AlphaFunction
        public const uint LEQUAL = 0x0203;	// AlphaFunction
        public const uint GREATER = 0x0204;	// AlphaFunction
        public const uint NOTEQUAL = 0x0205;	// AlphaFunction
        public const uint GEQUAL = 0x0206;	// AlphaFunction
        public const uint ALWAYS = 0x0207;	// AlphaFunction
        /* BlendingFactorDest */

        public const uint ZERO = 0;		// BlendingFactorDest
        public const uint ONE = 1;		// BlendingFactorDest
        public const uint SRC_COLOR = 0x0300;	// BlendingFactorDest
        public const uint ONE_MINUS_SRC_COLOR = 0x0301;	// BlendingFactorDest
        public const uint SRC_ALPHA = 0x0302;	// BlendingFactorDest
        public const uint ONE_MINUS_SRC_ALPHA = 0x0303;	// BlendingFactorDest
        public const uint DST_ALPHA = 0x0304;	// BlendingFactorDest
        public const uint ONE_MINUS_DST_ALPHA = 0x0305;	// BlendingFactorDest
        /* BlendingFactorSrc */

        public const uint DST_COLOR = 0x0306;	// BlendingFactorSrc
        public const uint ONE_MINUS_DST_COLOR = 0x0307;	// BlendingFactorSrc
        public const uint SRC_ALPHA_SATURATE = 0x0308;	// BlendingFactorSrc
        /* DrawBufferMode */

        public const uint NONE = 0;		// DrawBufferMode
        public const uint FRONT_LEFT = 0x0400;	// DrawBufferMode
        public const uint FRONT_RIGHT = 0x0401;	// DrawBufferMode
        public const uint BACK_LEFT = 0x0402;	// DrawBufferMode
        public const uint BACK_RIGHT = 0x0403;	// DrawBufferMode
        public const uint FRONT = 0x0404;	// DrawBufferMode
        public const uint BACK = 0x0405;	// DrawBufferMode
        public const uint LEFT = 0x0406;	// DrawBufferMode
        public const uint RIGHT = 0x0407;	// DrawBufferMode
        public const uint FRONT_AND_BACK = 0x0408;	// DrawBufferMode
        /* ErrorCode */

        public const uint NO_ERROR = 0;		// ErrorCode
        public const uint INVALID_ENUM = 0x0500;	// ErrorCode
        public const uint INVALID_VALUE = 0x0501;	// ErrorCode
        public const uint INVALID_OPERATION = 0x0502;	// ErrorCode
        public const uint OUT_OF_MEMORY = 0x0505;	// ErrorCode
        /* FrontFaceDirection */

        public const uint CW = 0x0900;	// FrontFaceDirection
        public const uint CCW = 0x0901;	// FrontFaceDirection
        /* GetPName */

        public const uint POINT_SIZE = 0x0B11; // 1 F	// GetPName
        public const uint POINT_SIZE_RANGE = 0x0B12; // 2 F	// GetPName
        public const uint POINT_SIZE_GRANULARITY = 0x0B13; // 1 F	// GetPName
        public const uint LINE_SMOOTH = 0x0B20; // 1 I	// GetPName
        public const uint LINE_WIDTH = 0x0B21; // 1 F	// GetPName
        public const uint LINE_WIDTH_RANGE = 0x0B22; // 2 F	// GetPName
        public const uint LINE_WIDTH_GRANULARITY = 0x0B23; // 1 F	// GetPName
        public const uint POLYGON_MODE = 0x0B40; // 2 I	// GetPName
        public const uint POLYGON_SMOOTH = 0x0B41; // 1 I	// GetPName
        public const uint CULL_FACE = 0x0B44; // 1 I	// GetPName
        public const uint CULL_FACE_MODE = 0x0B45; // 1 I	// GetPName
        public const uint FRONT_FACE = 0x0B46; // 1 I	// GetPName
        public const uint DEPTH_RANGE = 0x0B70; // 2 F	// GetPName
        public const uint DEPTH_TEST = 0x0B71; // 1 I	// GetPName
        public const uint DEPTH_WRITEMASK = 0x0B72; // 1 I	// GetPName
        public const uint DEPTH_CLEAR_VALUE = 0x0B73; // 1 F	// GetPName
        public const uint DEPTH_FUNC = 0x0B74; // 1 I	// GetPName
        public const uint STENCIL_TEST = 0x0B90; // 1 I	// GetPName
        public const uint STENCIL_CLEAR_VALUE = 0x0B91; // 1 I	// GetPName
        public const uint STENCIL_FUNC = 0x0B92; // 1 I	// GetPName
        public const uint STENCIL_VALUE_MASK = 0x0B93; // 1 I	// GetPName
        public const uint STENCIL_FAIL = 0x0B94; // 1 I	// GetPName
        public const uint STENCIL_PASS_DEPTH_FAIL = 0x0B95; // 1 I	// GetPName
        public const uint STENCIL_PASS_DEPTH_PASS = 0x0B96; // 1 I	// GetPName
        public const uint STENCIL_REF = 0x0B97; // 1 I	// GetPName
        public const uint STENCIL_WRITEMASK = 0x0B98; // 1 I	// GetPName
        public const uint VIEWPORT = 0x0BA2; // 4 I	// GetPName
        public const uint DITHER = 0x0BD0; // 1 I	// GetPName
        public const uint BLEND_DST = 0x0BE0; // 1 I	// GetPName
        public const uint BLEND_SRC = 0x0BE1; // 1 I	// GetPName
        public const uint BLEND = 0x0BE2; // 1 I	// GetPName
        public const uint LOGIC_OP_MODE = 0x0BF0; // 1 I	// GetPName
        public const uint COLOR_LOGIC_OP = 0x0BF2; // 1 I	// GetPName
        public const uint DRAW_BUFFER = 0x0C01; // 1 I	// GetPName
        public const uint READ_BUFFER = 0x0C02; // 1 I	// GetPName
        public const uint SCISSOR_BOX = 0x0C10; // 4 I	// GetPName
        public const uint SCISSOR_TEST = 0x0C11; // 1 I	// GetPName
        public const uint COLOR_CLEAR_VALUE = 0x0C22; // 4 F	// GetPName
        public const uint COLOR_WRITEMASK = 0x0C23; // 4 I	// GetPName
        public const uint DOUBLEBUFFER = 0x0C32; // 1 I	// GetPName
        public const uint STEREO = 0x0C33; // 1 I	// GetPName
        public const uint LINE_SMOOTH_HINT = 0x0C52; // 1 I	// GetPName
        public const uint POLYGON_SMOOTH_HINT = 0x0C53; // 1 I	// GetPName
        public const uint UNPACK_SWAP_BYTES = 0x0CF0; // 1 I	// GetPName
        public const uint UNPACK_LSB_FIRST = 0x0CF1; // 1 I	// GetPName
        public const uint UNPACK_ROW_LENGTH = 0x0CF2; // 1 I	// GetPName
        public const uint UNPACK_SKIP_ROWS = 0x0CF3; // 1 I	// GetPName
        public const uint UNPACK_SKIP_PIXELS = 0x0CF4; // 1 I	// GetPName
        public const uint UNPACK_ALIGNMENT = 0x0CF5; // 1 I	// GetPName
        public const uint PACK_SWAP_BYTES = 0x0D00; // 1 I	// GetPName
        public const uint PACK_LSB_FIRST = 0x0D01; // 1 I	// GetPName
        public const uint PACK_ROW_LENGTH = 0x0D02; // 1 I	// GetPName
        public const uint PACK_SKIP_ROWS = 0x0D03; // 1 I	// GetPName
        public const uint PACK_SKIP_PIXELS = 0x0D04; // 1 I	// GetPName
        public const uint PACK_ALIGNMENT = 0x0D05; // 1 I	// GetPName
        public const uint MAX_TEXTURE_SIZE = 0x0D33; // 1 I	// GetPName
        public const uint MAX_VIEWPORT_DIMS = 0x0D3A; // 2 F	// GetPName
        public const uint SUBPIXEL_BITS = 0x0D50; // 1 I	// GetPName
        public const uint TEXTURE_1D = 0x0DE0; // 1 I	// GetPName
        public const uint TEXTURE_2D = 0x0DE1; // 1 I	// GetPName
        public const uint POLYGON_OFFSET_UNITS = 0x2A00; // 1 F	// GetPName
        public const uint POLYGON_OFFSET_POINT = 0x2A01; // 1 I	// GetPName
        public const uint POLYGON_OFFSET_LINE = 0x2A02; // 1 I	// GetPName
        public const uint POLYGON_OFFSET_FILL = 0x8037; // 1 I	// GetPName
        public const uint POLYGON_OFFSET_FACTOR = 0x8038; // 1 F	// GetPName
        public const uint TEXTURE_BINDING_1D = 0x8068; // 1 I	// GetPName
        public const uint TEXTURE_BINDING_2D = 0x8069; // 1 I	// GetPName
        /* GetTextureParameter */

        public const uint TEXTURE_WIDTH = 0x1000;	// GetTextureParameter
        public const uint TEXTURE_HEIGHT = 0x1001;	// GetTextureParameter
        public const uint TEXTURE_INTERNAL_FORMAT = 0x1003;	// GetTextureParameter
        public const uint TEXTURE_BORDER_COLOR = 0x1004;	// GetTextureParameter
        public const uint TEXTURE_RED_SIZE = 0x805C;	// GetTextureParameter
        public const uint TEXTURE_GREEN_SIZE = 0x805D;	// GetTextureParameter
        public const uint TEXTURE_BLUE_SIZE = 0x805E;	// GetTextureParameter
        public const uint TEXTURE_ALPHA_SIZE = 0x805F;	// GetTextureParameter
        /* HintMode */

        public const uint DONT_CARE = 0x1100;	// HintMode
        public const uint FASTEST = 0x1101;	// HintMode
        public const uint NICEST = 0x1102;	// HintMode
        /* DataType */

        public const uint BYTE = 0x1400;	// DataType
        public const uint UNSIGNED_BYTE = 0x1401;	// DataType
        public const uint SHORT = 0x1402;	// DataType
        public const uint UNSIGNED_SHORT = 0x1403;	// DataType
        public const uint INT = 0x1404;	// DataType
        public const uint UNSIGNED_INT = 0x1405;	// DataType
        public const uint FLOAT = 0x1406;	// DataType
        public const uint DOUBLE = 0x140A;	// DataType
        //// Deprecated in GL 3.0; undeprecated in GL 4.3 / KHR_debug
        /* ErrorCode */

        public const uint STACK_OVERFLOW = 0x0503;	// ErrorCode
        public const uint STACK_UNDERFLOW = 0x0504;	// ErrorCode
        /* LogicOp */

        public const uint CLEAR = 0x1500;	// LogicOp
        public const uint AND = 0x1501;	// LogicOp
        public const uint AND_REVERSE = 0x1502;	// LogicOp
        public const uint COPY = 0x1503;	// LogicOp
        public const uint AND_INVERTED = 0x1504;	// LogicOp
        public const uint NOOP = 0x1505;	// LogicOp
        public const uint XOR = 0x1506;	// LogicOp
        public const uint OR = 0x1507;	// LogicOp
        public const uint NOR = 0x1508;	// LogicOp
        public const uint EQUIV = 0x1509;	// LogicOp
        public const uint INVERT = 0x150A;	// LogicOp
        public const uint OR_REVERSE = 0x150B;	// LogicOp
        public const uint COPY_INVERTED = 0x150C;	// LogicOp
        public const uint OR_INVERTED = 0x150D;	// LogicOp
        public const uint NAND = 0x150E;	// LogicOp
        public const uint SET = 0x150F;	// LogicOp
        /* MatrixMode (for gl3.h, FBO attachment type) */

        public const uint TEXTURE = 0x1702;	// MatrixMode
        /* PixelCopyType */

        public const uint COLOR = 0x1800;	// PixelCopyType
        public const uint DEPTH = 0x1801;	// PixelCopyType
        public const uint STENCIL = 0x1802;	// PixelCopyType
        /* PixelFormat */

        public const uint STENCIL_INDEX = 0x1901;	// PixelFormat
        public const uint DEPTH_COMPONENT = 0x1902;	// PixelFormat
        public const uint RED = 0x1903;	// PixelFormat
        public const uint GREEN = 0x1904;	// PixelFormat
        public const uint BLUE = 0x1905;	// PixelFormat
        public const uint ALPHA = 0x1906;	// PixelFormat
        public const uint RGB = 0x1907;	// PixelFormat
        public const uint RGBA = 0x1908;	// PixelFormat
        /* PolygonMode */

        public const uint POINT = 0x1B00;	// PolygonMode
        public const uint LINE = 0x1B01;	// PolygonMode
        public const uint FILL = 0x1B02;	// PolygonMode
        /* StencilOp */

        public const uint KEEP = 0x1E00;	// StencilOp
        public const uint REPLACE = 0x1E01;	// StencilOp
        public const uint INCR = 0x1E02;	// StencilOp
        public const uint DECR = 0x1E03;	// StencilOp
        /* StringName */

        public const uint VENDOR = 0x1F00;	// StringName
        public const uint RENDERER = 0x1F01;	// StringName
        public const uint VERSION = 0x1F02;	// StringName
        public const uint EXTENSIONS = 0x1F03;	// StringName
        /* TextureMagFilter */

        public const uint NEAREST = 0x2600;	// TextureMagFilter
        public const uint LINEAR = 0x2601;	// TextureMagFilter
        /* TextureMinFilter */

        public const uint NEAREST_MIPMAP_NEAREST = 0x2700;	// TextureMinFilter
        public const uint LINEAR_MIPMAP_NEAREST = 0x2701;	// TextureMinFilter
        public const uint NEAREST_MIPMAP_LINEAR = 0x2702;	// TextureMinFilter
        public const uint LINEAR_MIPMAP_LINEAR = 0x2703;	// TextureMinFilter
        /* TextureParameterName */

        public const uint TEXTURE_MAG_FILTER = 0x2800;	// TextureParameterName
        public const uint TEXTURE_MIN_FILTER = 0x2801;	// TextureParameterName
        public const uint TEXTURE_WRAP_S = 0x2802;	// TextureParameterName
        public const uint TEXTURE_WRAP_T = 0x2803;	// TextureParameterName
        /* TextureTarget */

        public const uint PROXY_TEXTURE_1D = 0x8063;	// TextureTarget
        public const uint PROXY_TEXTURE_2D = 0x8064;	// TextureTarget
        /* TextureWrapMode */

        public const uint REPEAT = 0x2901;	// TextureWrapMode
        /* PixelInternalFormat */

        public const uint R3_G3_B2 = 0x2A10;	// PixelInternalFormat
        public const uint RGB4 = 0x804F;	// PixelInternalFormat
        public const uint RGB5 = 0x8050;	// PixelInternalFormat
        public const uint RGB8 = 0x8051;	// PixelInternalFormat
        public const uint RGB10 = 0x8052;	// PixelInternalFormat
        public const uint RGB12 = 0x8053;	// PixelInternalFormat
        public const uint RGB16 = 0x8054;	// PixelInternalFormat
        public const uint RGBA2 = 0x8055;	// PixelInternalFormat
        public const uint RGBA4 = 0x8056;	// PixelInternalFormat
        public const uint RGB5_A1 = 0x8057;	// PixelInternalFormat
        public const uint RGBA8 = 0x8058;	// PixelInternalFormat
        public const uint RGB10_A2 = 0x8059;	// PixelInternalFormat
        public const uint RGBA12 = 0x805A;	// PixelInternalFormat
        public const uint RGBA16 = 0x805B;	// PixelInternalFormat
        //profile: compatibility
        /* AttribMask */

        public const uint CURRENT_BIT = 0x00000001;	// AttribMask
        public const uint POINT_BIT = 0x00000002;	// AttribMask
        public const uint LINE_BIT = 0x00000004;	// AttribMask
        public const uint POLYGON_BIT = 0x00000008;	// AttribMask
        public const uint POLYGON_STIPPLE_BIT = 0x00000010;	// AttribMask
        public const uint PIXEL_MODE_BIT = 0x00000020;	// AttribMask
        public const uint LIGHTING_BIT = 0x00000040;	// AttribMask
        public const uint FOG_BIT = 0x00000080;	// AttribMask
        public const uint ACCUM_BUFFER_BIT = 0x00000200;	// AttribMask
        public const uint VIEWPORT_BIT = 0x00000800;	// AttribMask
        public const uint TRANSFORM_BIT = 0x00001000;	// AttribMask
        public const uint ENABLE_BIT = 0x00002000;	// AttribMask
        public const uint HINT_BIT = 0x00008000;	// AttribMask
        public const uint EVAL_BIT = 0x00010000;	// AttribMask
        public const uint LIST_BIT = 0x00020000;	// AttribMask
        public const uint TEXTURE_BIT = 0x00040000;	// AttribMask
        public const uint SCISSOR_BIT = 0x00080000;	// AttribMask
        public const uint ALL_ATTRIB_BITS = 0xFFFFFFFF;	// AttribMask
        /* ClientAttribMask */

        public const uint CLIENT_PIXEL_STORE_BIT = 0x00000001;	// ClientAttribMask
        public const uint CLIENT_VERTEX_ARRAY_BIT = 0x00000002;	// ClientAttribMask
        public const uint CLIENT_ALL_ATTRIB_BITS = 0xFFFFFFFF;	// ClientAttribMask
        /* BeginMode */

        public const uint QUAD_STRIP = 0x0008;	// BeginMode
        public const uint POLYGON = 0x0009;	// BeginMode
        /* AccumOp */

        public const uint ACCUM = 0x0100;	// AccumOp
        public const uint LOAD = 0x0101;	// AccumOp
        public const uint RETURN = 0x0102;	// AccumOp
        public const uint MULT = 0x0103;	// AccumOp
        public const uint ADD = 0x0104;	// AccumOp
        /* DrawBufferMode */

        public const uint AUX0 = 0x0409;	// DrawBufferMode
        public const uint AUX1 = 0x040A;	// DrawBufferMode
        public const uint AUX2 = 0x040B;	// DrawBufferMode
        public const uint AUX3 = 0x040C;	// DrawBufferMode
        /* FeedbackType */

        //public const uint 2D = 0x0600;	// FeedbackType
        //public const uint 3D = 0x0601;	// FeedbackType
        //public const uint 3D_COLOR = 0x0602;	// FeedbackType
        //public const uint 3D_COLOR_TEXTURE = 0x0603;	// FeedbackType
        //public const uint 4D_COLOR_TEXTURE = 0x0604;	// FeedbackType
        /* FeedBackToken */

        public const uint PASS_THROUGH_TOKEN = 0x0700;	// FeedBackToken
        public const uint POINT_TOKEN = 0x0701;	// FeedBackToken
        public const uint LINE_TOKEN = 0x0702;	// FeedBackToken
        public const uint POLYGON_TOKEN = 0x0703;	// FeedBackToken
        public const uint BITMAP_TOKEN = 0x0704;	// FeedBackToken
        public const uint DRAW_PIXEL_TOKEN = 0x0705;	// FeedBackToken
        public const uint COPY_PIXEL_TOKEN = 0x0706;	// FeedBackToken
        public const uint LINE_RESET_TOKEN = 0x0707;	// FeedBackToken
        /* FogMode */

        public const uint EXP = 0x0800;	// FogMode
        public const uint EXP2 = 0x0801;	// FogMode
        /* GetMapQuery */

        public const uint COEFF = 0x0A00;	// GetMapQuery
        public const uint ORDER = 0x0A01;	// GetMapQuery
        public const uint DOMAIN = 0x0A02;	// GetMapQuery
        /* GetPixelMap */

        public const uint PIXEL_MAP_I_TO_I = 0x0C70;	// GetPixelMap
        public const uint PIXEL_MAP_S_TO_S = 0x0C71;	// GetPixelMap
        public const uint PIXEL_MAP_I_TO_R = 0x0C72;	// GetPixelMap
        public const uint PIXEL_MAP_I_TO_G = 0x0C73;	// GetPixelMap
        public const uint PIXEL_MAP_I_TO_B = 0x0C74;	// GetPixelMap
        public const uint PIXEL_MAP_I_TO_A = 0x0C75;	// GetPixelMap
        public const uint PIXEL_MAP_R_TO_R = 0x0C76;	// GetPixelMap
        public const uint PIXEL_MAP_G_TO_G = 0x0C77;	// GetPixelMap
        public const uint PIXEL_MAP_B_TO_B = 0x0C78;	// GetPixelMap
        public const uint PIXEL_MAP_A_TO_A = 0x0C79;	// GetPixelMap
        /* GetPointervPName */

        public const uint VERTEX_ARRAY_POINTER = 0x808E;	// GetPointervPName
        public const uint NORMAL_ARRAY_POINTER = 0x808F;	// GetPointervPName
        public const uint COLOR_ARRAY_POINTER = 0x8090;	// GetPointervPName
        public const uint INDEX_ARRAY_POINTER = 0x8091;	// GetPointervPName
        public const uint TEXTURE_COORD_ARRAY_POINTER = 0x8092;	// GetPointervPName
        public const uint EDGE_FLAG_ARRAY_POINTER = 0x8093;	// GetPointervPName
        public const uint FEEDBACK_BUFFER_POINTER = 0x0DF0;	// GetPointervPName
        public const uint SELECTION_BUFFER_POINTER = 0x0DF3;	// GetPointervPName
        /* GetPName */

        public const uint CURRENT_COLOR = 0x0B00; // 4 F	// GetPName
        public const uint CURRENT_INDEX = 0x0B01; // 1 F	// GetPName
        public const uint CURRENT_NORMAL = 0x0B02; // 3 F	// GetPName
        public const uint CURRENT_TEXTURE_COORDS = 0x0B03; // 4 F	// GetPName
        public const uint CURRENT_RASTER_COLOR = 0x0B04; // 4 F	// GetPName
        public const uint CURRENT_RASTER_INDEX = 0x0B05; // 1 F	// GetPName
        public const uint CURRENT_RASTER_TEXTURE_COORDS = 0x0B06; // 4 F	// GetPName
        public const uint CURRENT_RASTER_POSITION = 0x0B07; // 4 F	// GetPName
        public const uint CURRENT_RASTER_POSITION_VALID = 0x0B08; // 1 I	// GetPName
        public const uint CURRENT_RASTER_DISTANCE = 0x0B09; // 1 F	// GetPName
        public const uint POINT_SMOOTH = 0x0B10; // 1 I	// GetPName
        public const uint LINE_STIPPLE = 0x0B24; // 1 I	// GetPName
        public const uint LINE_STIPPLE_PATTERN = 0x0B25; // 1 I	// GetPName
        public const uint LINE_STIPPLE_REPEAT = 0x0B26; // 1 I	// GetPName
        public const uint LIST_MODE = 0x0B30; // 1 I	// GetPName
        public const uint MAX_LIST_NESTING = 0x0B31; // 1 I	// GetPName
        public const uint LIST_BASE = 0x0B32; // 1 I	// GetPName
        public const uint LIST_INDEX = 0x0B33; // 1 I	// GetPName
        public const uint POLYGON_STIPPLE = 0x0B42; // 1 I	// GetPName
        public const uint EDGE_FLAG = 0x0B43; // 1 I	// GetPName
        public const uint LIGHTING = 0x0B50; // 1 I	// GetPName
        public const uint LIGHT_MODEL_LOCAL_VIEWER = 0x0B51; // 1 I	// GetPName
        public const uint LIGHT_MODEL_TWO_SIDE = 0x0B52; // 1 I	// GetPName
        public const uint LIGHT_MODEL_AMBIENT = 0x0B53; // 4 F	// GetPName
        public const uint SHADE_MODEL = 0x0B54; // 1 I	// GetPName
        public const uint COLOR_MATERIAL_FACE = 0x0B55; // 1 I	// GetPName
        public const uint COLOR_MATERIAL_PARAMETER = 0x0B56; // 1 I	// GetPName
        public const uint COLOR_MATERIAL = 0x0B57; // 1 I	// GetPName
        public const uint FOG = 0x0B60; // 1 I	// GetPName
        public const uint FOG_INDEX = 0x0B61; // 1 I	// GetPName
        public const uint FOG_DENSITY = 0x0B62; // 1 F	// GetPName
        public const uint FOG_START = 0x0B63; // 1 F	// GetPName
        public const uint FOG_END = 0x0B64; // 1 F	// GetPName
        public const uint FOG_MODE = 0x0B65; // 1 I	// GetPName
        public const uint FOG_COLOR = 0x0B66; // 4 F	// GetPName
        public const uint ACCUM_CLEAR_VALUE = 0x0B80; // 4 F	// GetPName
        public const uint MATRIX_MODE = 0x0BA0; // 1 I	// GetPName
        public const uint NORMALIZE = 0x0BA1; // 1 I	// GetPName
        public const uint MODELVIEW_STACK_DEPTH = 0x0BA3; // 1 I	// GetPName
        public const uint PROJECTION_STACK_DEPTH = 0x0BA4; // 1 I	// GetPName
        public const uint TEXTURE_STACK_DEPTH = 0x0BA5; // 1 I	// GetPName
        public const uint MODELVIEW_MATRIX = 0x0BA6; // 16 F // GetPName
        public const uint PROJECTION_MATRIX = 0x0BA7; // 16 F // GetPName
        public const uint TEXTURE_MATRIX = 0x0BA8; // 16 F // GetPName
        public const uint ATTRIB_STACK_DEPTH = 0x0BB0; // 1 I	// GetPName
        public const uint CLIENT_ATTRIB_STACK_DEPTH = 0x0BB1; // 1 I	// GetPName
        public const uint ALPHA_TEST = 0x0BC0; // 1 I	// GetPName
        public const uint ALPHA_TEST_FUNC = 0x0BC1; // 1 I	// GetPName
        public const uint ALPHA_TEST_REF = 0x0BC2; // 1 F	// GetPName
        public const uint INDEX_LOGIC_OP = 0x0BF1; // 1 I	// GetPName
        public const uint LOGIC_OP = 0x0BF1; // 1 I	// GetPName
        public const uint AUX_BUFFERS = 0x0C00; // 1 I	// GetPName
        public const uint INDEX_CLEAR_VALUE = 0x0C20; // 1 I	// GetPName
        public const uint INDEX_WRITEMASK = 0x0C21; // 1 I	// GetPName
        public const uint INDEX_MODE = 0x0C30; // 1 I	// GetPName
        public const uint RGBA_MODE = 0x0C31; // 1 I	// GetPName
        public const uint RENDER_MODE = 0x0C40; // 1 I	// GetPName
        public const uint PERSPECTIVE_CORRECTION_HINT = 0x0C50; // 1 I	// GetPName
        public const uint POINT_SMOOTH_HINT = 0x0C51; // 1 I	// GetPName
        public const uint FOG_HINT = 0x0C54; // 1 I	// GetPName
        public const uint TEXTURE_GEN_S = 0x0C60; // 1 I	// GetPName
        public const uint TEXTURE_GEN_T = 0x0C61; // 1 I	// GetPName
        public const uint TEXTURE_GEN_R = 0x0C62; // 1 I	// GetPName
        public const uint TEXTURE_GEN_Q = 0x0C63; // 1 I	// GetPName
        public const uint PIXEL_MAP_I_TO_I_SIZE = 0x0CB0; // 1 I	// GetPName
        public const uint PIXEL_MAP_S_TO_S_SIZE = 0x0CB1; // 1 I	// GetPName
        public const uint PIXEL_MAP_I_TO_R_SIZE = 0x0CB2; // 1 I	// GetPName
        public const uint PIXEL_MAP_I_TO_G_SIZE = 0x0CB3; // 1 I	// GetPName
        public const uint PIXEL_MAP_I_TO_B_SIZE = 0x0CB4; // 1 I	// GetPName
        public const uint PIXEL_MAP_I_TO_A_SIZE = 0x0CB5; // 1 I	// GetPName
        public const uint PIXEL_MAP_R_TO_R_SIZE = 0x0CB6; // 1 I	// GetPName
        public const uint PIXEL_MAP_G_TO_G_SIZE = 0x0CB7; // 1 I	// GetPName
        public const uint PIXEL_MAP_B_TO_B_SIZE = 0x0CB8; // 1 I	// GetPName
        public const uint PIXEL_MAP_A_TO_A_SIZE = 0x0CB9; // 1 I	// GetPName
        public const uint MAP_COLOR = 0x0D10; // 1 I	// GetPName
        public const uint MAP_STENCIL = 0x0D11; // 1 I	// GetPName
        public const uint INDEX_SHIFT = 0x0D12; // 1 I	// GetPName
        public const uint INDEX_OFFSET = 0x0D13; // 1 I	// GetPName
        public const uint RED_SCALE = 0x0D14; // 1 F	// GetPName
        public const uint RED_BIAS = 0x0D15; // 1 F	// GetPName
        public const uint ZOOM_X = 0x0D16; // 1 F	// GetPName
        public const uint ZOOM_Y = 0x0D17; // 1 F	// GetPName
        public const uint GREEN_SCALE = 0x0D18; // 1 F	// GetPName
        public const uint GREEN_BIAS = 0x0D19; // 1 F	// GetPName
        public const uint BLUE_SCALE = 0x0D1A; // 1 F	// GetPName
        public const uint BLUE_BIAS = 0x0D1B; // 1 F	// GetPName
        public const uint ALPHA_SCALE = 0x0D1C; // 1 F	// GetPName
        public const uint ALPHA_BIAS = 0x0D1D; // 1 F	// GetPName
        public const uint DEPTH_SCALE = 0x0D1E; // 1 F	// GetPName
        public const uint DEPTH_BIAS = 0x0D1F; // 1 F	// GetPName
        public const uint MAX_EVAL_ORDER = 0x0D30; // 1 I	// GetPName
        public const uint MAX_LIGHTS = 0x0D31; // 1 I	// GetPName
        public const uint MAX_CLIP_PLANES = 0x0D32; // 1 I	// GetPName
        public const uint MAX_PIXEL_MAP_TABLE = 0x0D34; // 1 I	// GetPName
        public const uint MAX_ATTRIB_STACK_DEPTH = 0x0D35; // 1 I	// GetPName
        public const uint MAX_MODELVIEW_STACK_DEPTH = 0x0D36; // 1 I	// GetPName
        public const uint MAX_NAME_STACK_DEPTH = 0x0D37; // 1 I	// GetPName
        public const uint MAX_PROJECTION_STACK_DEPTH = 0x0D38; // 1 I	// GetPName
        public const uint MAX_TEXTURE_STACK_DEPTH = 0x0D39; // 1 I	// GetPName
        public const uint MAX_CLIENT_ATTRIB_STACK_DEPTH = 0x0D3B; // 1 I	// GetPName
        public const uint INDEX_BITS = 0x0D51; // 1 I	// GetPName
        public const uint RED_BITS = 0x0D52; // 1 I	// GetPName
        public const uint GREEN_BITS = 0x0D53; // 1 I	// GetPName
        public const uint BLUE_BITS = 0x0D54; // 1 I	// GetPName
        public const uint ALPHA_BITS = 0x0D55; // 1 I	// GetPName
        public const uint DEPTH_BITS = 0x0D56; // 1 I	// GetPName
        public const uint STENCIL_BITS = 0x0D57; // 1 I	// GetPName
        public const uint ACCUM_RED_BITS = 0x0D58; // 1 I	// GetPName
        public const uint ACCUM_GREEN_BITS = 0x0D59; // 1 I	// GetPName
        public const uint ACCUM_BLUE_BITS = 0x0D5A; // 1 I	// GetPName
        public const uint ACCUM_ALPHA_BITS = 0x0D5B; // 1 I	// GetPName
        public const uint NAME_STACK_DEPTH = 0x0D70; // 1 I	// GetPName
        public const uint AUTO_NORMAL = 0x0D80; // 1 I	// GetPName
        public const uint MAP1_COLOR_4 = 0x0D90; // 1 I	// GetPName
        public const uint MAP1_INDEX = 0x0D91; // 1 I	// GetPName
        public const uint MAP1_NORMAL = 0x0D92; // 1 I	// GetPName
        public const uint MAP1_TEXTURE_COORD_1 = 0x0D93; // 1 I	// GetPName
        public const uint MAP1_TEXTURE_COORD_2 = 0x0D94; // 1 I	// GetPName
        public const uint MAP1_TEXTURE_COORD_3 = 0x0D95; // 1 I	// GetPName
        public const uint MAP1_TEXTURE_COORD_4 = 0x0D96; // 1 I	// GetPName
        public const uint MAP1_VERTEX_3 = 0x0D97; // 1 I	// GetPName
        public const uint MAP1_VERTEX_4 = 0x0D98; // 1 I	// GetPName
        public const uint MAP2_COLOR_4 = 0x0DB0; // 1 I	// GetPName
        public const uint MAP2_INDEX = 0x0DB1; // 1 I	// GetPName
        public const uint MAP2_NORMAL = 0x0DB2; // 1 I	// GetPName
        public const uint MAP2_TEXTURE_COORD_1 = 0x0DB3; // 1 I	// GetPName
        public const uint MAP2_TEXTURE_COORD_2 = 0x0DB4; // 1 I	// GetPName
        public const uint MAP2_TEXTURE_COORD_3 = 0x0DB5; // 1 I	// GetPName
        public const uint MAP2_TEXTURE_COORD_4 = 0x0DB6; // 1 I	// GetPName
        public const uint MAP2_VERTEX_3 = 0x0DB7; // 1 I	// GetPName
        public const uint MAP2_VERTEX_4 = 0x0DB8; // 1 I	// GetPName
        public const uint MAP1_GRID_DOMAIN = 0x0DD0; // 2 F	// GetPName
        public const uint MAP1_GRID_SEGMENTS = 0x0DD1; // 1 I	// GetPName
        public const uint MAP2_GRID_DOMAIN = 0x0DD2; // 4 F	// GetPName
        public const uint MAP2_GRID_SEGMENTS = 0x0DD3; // 2 I	// GetPName
        public const uint FEEDBACK_BUFFER_SIZE = 0x0DF1; // 1 I	// GetPName
        public const uint FEEDBACK_BUFFER_TYPE = 0x0DF2; // 1 I	// GetPName
        public const uint SELECTION_BUFFER_SIZE = 0x0DF4; // 1 I	// GetPName
        public const uint VERTEX_ARRAY = 0x8074; // 1 I	// GetPName
        public const uint NORMAL_ARRAY = 0x8075; // 1 I	// GetPName
        public const uint COLOR_ARRAY = 0x8076; // 1 I	// GetPName
        public const uint INDEX_ARRAY = 0x8077; // 1 I	// GetPName
        public const uint TEXTURE_COORD_ARRAY = 0x8078; // 1 I	// GetPName
        public const uint EDGE_FLAG_ARRAY = 0x8079; // 1 I	// GetPName
        public const uint VERTEX_ARRAY_SIZE = 0x807A; // 1 I	// GetPName
        public const uint VERTEX_ARRAY_TYPE = 0x807B; // 1 I	// GetPName
        public const uint VERTEX_ARRAY_STRIDE = 0x807C; // 1 I	// GetPName
        public const uint NORMAL_ARRAY_TYPE = 0x807E; // 1 I	// GetPName
        public const uint NORMAL_ARRAY_STRIDE = 0x807F; // 1 I	// GetPName
        public const uint COLOR_ARRAY_SIZE = 0x8081; // 1 I	// GetPName
        public const uint COLOR_ARRAY_TYPE = 0x8082; // 1 I	// GetPName
        public const uint COLOR_ARRAY_STRIDE = 0x8083; // 1 I	// GetPName
        public const uint INDEX_ARRAY_TYPE = 0x8085; // 1 I	// GetPName
        public const uint INDEX_ARRAY_STRIDE = 0x8086; // 1 I	// GetPName
        public const uint TEXTURE_COORD_ARRAY_SIZE = 0x8088; // 1 I	// GetPName
        public const uint TEXTURE_COORD_ARRAY_TYPE = 0x8089; // 1 I	// GetPName
        public const uint TEXTURE_COORD_ARRAY_STRIDE = 0x808A; // 1 I	// GetPName
        public const uint EDGE_FLAG_ARRAY_STRIDE = 0x808C; // 1 I	// GetPName
        /* GetTextureParameter */

        public const uint TEXTURE_COMPONENTS = 0x1003;	// GetTextureParameter
        public const uint TEXTURE_BORDER = 0x1005;	// GetTextureParameter
        public const uint TEXTURE_LUMINANCE_SIZE = 0x8060;	// GetTextureParameter
        public const uint TEXTURE_INTENSITY_SIZE = 0x8061;	// GetTextureParameter
        public const uint TEXTURE_PRIORITY = 0x8066;	// GetTextureParameter
        public const uint TEXTURE_RESIDENT = 0x8067;	// GetTextureParameter
        /* LightParameter */

        public const uint AMBIENT = 0x1200;	// LightParameter
        public const uint DIFFUSE = 0x1201;	// LightParameter
        public const uint SPECULAR = 0x1202;	// LightParameter
        public const uint POSITION = 0x1203;	// LightParameter
        public const uint SPOT_DIRECTION = 0x1204;	// LightParameter
        public const uint SPOT_EXPONENT = 0x1205;	// LightParameter
        public const uint SPOT_CUTOFF = 0x1206;	// LightParameter
        public const uint CONSTANT_ATTENUATION = 0x1207;	// LightParameter
        public const uint LINEAR_ATTENUATION = 0x1208;	// LightParameter
        public const uint QUADRATIC_ATTENUATION = 0x1209;	// LightParameter
        /* ListMode */

        public const uint COMPILE = 0x1300;	// ListMode
        public const uint COMPILE_AND_EXECUTE = 0x1301;	// ListMode
        /* DataType */

        //public const uint 2_BYTES = 0x1407;	// DataType
        //public const uint 3_BYTES = 0x1408;	// DataType
        //public const uint 4_BYTES = 0x1409;	// DataType
        /* MaterialParameter */

        public const uint EMISSION = 0x1600;	// MaterialParameter
        public const uint SHININESS = 0x1601;	// MaterialParameter
        public const uint AMBIENT_AND_DIFFUSE = 0x1602;	// MaterialParameter
        public const uint COLOR_INDEXES = 0x1603;	// MaterialParameter
        /* MatrixMode */

        public const uint MODELVIEW = 0x1700;	// MatrixMode
        public const uint PROJECTION = 0x1701;	// MatrixMode
        /* PixelFormat */

        public const uint COLOR_INDEX = 0x1900;	// PixelFormat
        public const uint LUMINANCE = 0x1909;	// PixelFormat
        public const uint LUMINANCE_ALPHA = 0x190A;	// PixelFormat
        /* PixelType */

        public const uint BITMAP = 0x1A00;	// PixelType
        /* RenderingMode */

        public const uint RENDER = 0x1C00;	// RenderingMode
        public const uint FEEDBACK = 0x1C01;	// RenderingMode
        public const uint SELECT = 0x1C02;	// RenderingMode
        /* ShadingModel */

        public const uint FLAT = 0x1D00;	// ShadingModel
        public const uint SMOOTH = 0x1D01;	// ShadingModel
        /* TextureCoordName */

        public const uint S = 0x2000;	// TextureCoordName
        public const uint T = 0x2001;	// TextureCoordName
        public const uint R = 0x2002;	// TextureCoordName
        public const uint Q = 0x2003;	// TextureCoordName
        /* TextureEnvMode */

        public const uint MODULATE = 0x2100;	// TextureEnvMode
        public const uint DECAL = 0x2101;	// TextureEnvMode
        /* TextureEnvParameter */

        public const uint TEXTURE_ENV_MODE = 0x2200;	// TextureEnvParameter
        public const uint TEXTURE_ENV_COLOR = 0x2201;	// TextureEnvParameter
        /* TextureEnvTarget */

        public const uint TEXTURE_ENV = 0x2300;	// TextureEnvTarget
        /* TextureGenMode */

        public const uint EYE_LINEAR = 0x2400;	// TextureGenMode
        public const uint OBJECT_LINEAR = 0x2401;	// TextureGenMode
        public const uint SPHERE_MAP = 0x2402;	// TextureGenMode
        /* TextureGenParameter */

        public const uint TEXTURE_GEN_MODE = 0x2500;	// TextureGenParameter
        public const uint OBJECT_PLANE = 0x2501;	// TextureGenParameter
        public const uint EYE_PLANE = 0x2502;	// TextureGenParameter
        /* TextureWrapMode */

        public const uint CLAMP = 0x2900;	// TextureWrapMode
        /* PixelInternalFormat */

        public const uint ALPHA4 = 0x803B;	// PixelInternalFormat
        public const uint ALPHA8 = 0x803C;	// PixelInternalFormat
        public const uint ALPHA12 = 0x803D;	// PixelInternalFormat
        public const uint ALPHA16 = 0x803E;	// PixelInternalFormat
        public const uint LUMINANCE4 = 0x803F;	// PixelInternalFormat
        public const uint LUMINANCE8 = 0x8040;	// PixelInternalFormat
        public const uint LUMINANCE12 = 0x8041;	// PixelInternalFormat
        public const uint LUMINANCE16 = 0x8042;	// PixelInternalFormat
        public const uint LUMINANCE4_ALPHA4 = 0x8043;	// PixelInternalFormat
        public const uint LUMINANCE6_ALPHA2 = 0x8044;	// PixelInternalFormat
        public const uint LUMINANCE8_ALPHA8 = 0x8045;	// PixelInternalFormat
        public const uint LUMINANCE12_ALPHA4 = 0x8046;	// PixelInternalFormat
        public const uint LUMINANCE12_ALPHA12 = 0x8047;	// PixelInternalFormat
        public const uint LUMINANCE16_ALPHA16 = 0x8048;	// PixelInternalFormat
        public const uint INTENSITY = 0x8049;	// PixelInternalFormat
        public const uint INTENSITY4 = 0x804A;	// PixelInternalFormat
        public const uint INTENSITY8 = 0x804B;	// PixelInternalFormat
        public const uint INTENSITY12 = 0x804C;	// PixelInternalFormat
        public const uint INTENSITY16 = 0x804D;	// PixelInternalFormat
        /* InterleavedArrayFormat */

        public const uint V2F = 0x2A20;	// InterleavedArrayFormat
        public const uint V3F = 0x2A21;	// InterleavedArrayFormat
        public const uint C4UB_V2F = 0x2A22;	// InterleavedArrayFormat
        public const uint C4UB_V3F = 0x2A23;	// InterleavedArrayFormat
        public const uint C3F_V3F = 0x2A24;	// InterleavedArrayFormat
        public const uint N3F_V3F = 0x2A25;	// InterleavedArrayFormat
        public const uint C4F_N3F_V3F = 0x2A26;	// InterleavedArrayFormat
        public const uint T2F_V3F = 0x2A27;	// InterleavedArrayFormat
        public const uint T4F_V4F = 0x2A28;	// InterleavedArrayFormat
        public const uint T2F_C4UB_V3F = 0x2A29;	// InterleavedArrayFormat
        public const uint T2F_C3F_V3F = 0x2A2A;	// InterleavedArrayFormat
        public const uint T2F_N3F_V3F = 0x2A2B;	// InterleavedArrayFormat
        public const uint T2F_C4F_N3F_V3F = 0x2A2C;	// InterleavedArrayFormat
        public const uint T4F_C4F_N3F_V4F = 0x2A2D;	// InterleavedArrayFormat
        /* ClipPlaneName */

        public const uint CLIP_PLANE0 = 0x3000; // 1 I	// ClipPlaneName
        public const uint CLIP_PLANE1 = 0x3001; // 1 I	// ClipPlaneName
        public const uint CLIP_PLANE2 = 0x3002; // 1 I	// ClipPlaneName
        public const uint CLIP_PLANE3 = 0x3003; // 1 I	// ClipPlaneName
        public const uint CLIP_PLANE4 = 0x3004; // 1 I	// ClipPlaneName
        public const uint CLIP_PLANE5 = 0x3005; // 1 I	// ClipPlaneName
        /* LightName */

        public const uint LIGHT0 = 0x4000; // 1 I	// LightName
        public const uint LIGHT1 = 0x4001; // 1 I	// LightName
        public const uint LIGHT2 = 0x4002; // 1 I	// LightName
        public const uint LIGHT3 = 0x4003; // 1 I	// LightName
        public const uint LIGHT4 = 0x4004; // 1 I	// LightName
        public const uint LIGHT5 = 0x4005; // 1 I	// LightName
        public const uint LIGHT6 = 0x4006; // 1 I	// LightName
        public const uint LIGHT7 = 0x4007; // 1 I	// LightName


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 1.2 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_1_2
        public const uint UNSIGNED_BYTE_3_3_2 = 0x8032; // Equivalent to EXT_packed_pixels
        public const uint UNSIGNED_SHORT_4_4_4_4 = 0x8033;
        public const uint UNSIGNED_SHORT_5_5_5_1 = 0x8034;
        public const uint UNSIGNED_INT_8_8_8_8 = 0x8035;
        public const uint UNSIGNED_INT_10_10_10_2 = 0x8036;
        public const uint TEXTURE_BINDING_3D = 0x806A; // 1 I
        public const uint PACK_SKIP_IMAGES = 0x806B; // 1 I
        public const uint PACK_IMAGE_HEIGHT = 0x806C; // 1 F
        public const uint UNPACK_SKIP_IMAGES = 0x806D; // 1 I
        public const uint UNPACK_IMAGE_HEIGHT = 0x806E; // 1 F
        public const uint TEXTURE_3D = 0x806F; // 1 I
        public const uint PROXY_TEXTURE_3D = 0x8070;
        public const uint TEXTURE_DEPTH = 0x8071;
        public const uint TEXTURE_WRAP_R = 0x8072;
        public const uint MAX_3D_TEXTURE_SIZE = 0x8073; // 1 I
        public const uint UNSIGNED_BYTE_2_3_3_REV = 0x8362; // New for OpenGL 1.2
        public const uint UNSIGNED_SHORT_5_6_5 = 0x8363;
        public const uint UNSIGNED_SHORT_5_6_5_REV = 0x8364;
        public const uint UNSIGNED_SHORT_4_4_4_4_REV = 0x8365;
        public const uint UNSIGNED_SHORT_1_5_5_5_REV = 0x8366;
        public const uint UNSIGNED_INT_8_8_8_8_REV = 0x8367;
        public const uint UNSIGNED_INT_2_10_10_10_REV = 0x8368;
        public const uint BGR = 0x80E0;
        public const uint BGRA = 0x80E1;
        public const uint MAX_ELEMENTS_VERTICES = 0x80E8;
        public const uint MAX_ELEMENTS_INDICES = 0x80E9;
        public const uint CLAMP_TO_EDGE = 0x812F; // Equivalent to SGIS_texture_edge_clamp
        public const uint TEXTURE_MIN_LOD = 0x813A; // Equivalent to SGIS_texture_lod
        public const uint TEXTURE_MAX_LOD = 0x813B;
        public const uint TEXTURE_BASE_LEVEL = 0x813C;
        public const uint TEXTURE_MAX_LEVEL = 0x813D;
        public const uint SMOOTH_POINT_SIZE_RANGE = 0x0B12; // 2 F
        public const uint SMOOTH_POINT_SIZE_GRANULARITY = 0x0B13; // 1 F
        public const uint SMOOTH_LINE_WIDTH_RANGE = 0x0B22; // 2 F
        public const uint SMOOTH_LINE_WIDTH_GRANULARITY = 0x0B23; // 1 F
        public const uint ALIASED_LINE_WIDTH_RANGE = 0x846E; // 2 F
        //profile: compatibility
        public const uint RESCALE_NORMAL = 0x803A; // 1 I // Equivalent to EXT_rescale_normal
        public const uint LIGHT_MODEL_COLOR_CONTROL = 0x81F8; // 1 I
        public const uint SINGLE_COLOR = 0x81F9;
        public const uint SEPARATE_SPECULAR_COLOR = 0x81FA;
        public const uint ALIASED_POINT_SIZE_RANGE = 0x846D; // 2 F

        //ARB_imaging
        public const uint CONSTANT_COLOR = 0x8001; // Equivalent to EXT_blend_color
        public const uint ONE_MINUS_CONSTANT_COLOR = 0x8002;
        public const uint CONSTANT_ALPHA = 0x8003;
        public const uint ONE_MINUS_CONSTANT_ALPHA = 0x8004;
        public const uint BLEND_COLOR = 0x8005; // 4 F
        public const uint FUNC_ADD = 0x8006; // Equivalent to EXT_blend_minmax
        public const uint MIN = 0x8007;
        public const uint MAX = 0x8008;
        public const uint BLEND_EQUATION = 0x8009; // 1 I
        public const uint FUNC_SUBTRACT = 0x800A; // Equivalent to EXT_blend_subtract
        public const uint FUNC_REVERSE_SUBTRACT = 0x800B;
        //profile: compatibility
        public const uint CONVOLUTION_1D = 0x8010; // 1 I // Equivalent to EXT_convolution
        public const uint CONVOLUTION_2D = 0x8011; // 1 I
        public const uint SEPARABLE_2D = 0x8012; // 1 I
        public const uint CONVOLUTION_BORDER_MODE = 0x8013;
        public const uint CONVOLUTION_FILTER_SCALE = 0x8014;
        public const uint CONVOLUTION_FILTER_BIAS = 0x8015;
        public const uint REDUCE = 0x8016;
        public const uint CONVOLUTION_FORMAT = 0x8017;
        public const uint CONVOLUTION_WIDTH = 0x8018;
        public const uint CONVOLUTION_HEIGHT = 0x8019;
        public const uint MAX_CONVOLUTION_WIDTH = 0x801A;
        public const uint MAX_CONVOLUTION_HEIGHT = 0x801B;
        public const uint POST_CONVOLUTION_RED_SCALE = 0x801C; // 1 F
        public const uint POST_CONVOLUTION_GREEN_SCALE = 0x801D; // 1 F
        public const uint POST_CONVOLUTION_BLUE_SCALE = 0x801E; // 1 F
        public const uint POST_CONVOLUTION_ALPHA_SCALE = 0x801F; // 1 F
        public const uint POST_CONVOLUTION_RED_BIAS = 0x8020; // 1 F
        public const uint POST_CONVOLUTION_GREEN_BIAS = 0x8021; // 1 F
        public const uint POST_CONVOLUTION_BLUE_BIAS = 0x8022; // 1 F
        public const uint POST_CONVOLUTION_ALPHA_BIAS = 0x8023; // 1 F
        public const uint HISTOGRAM = 0x8024; // 1 I // Equivalent to EXT_histogram
        public const uint PROXY_HISTOGRAM = 0x8025;
        public const uint HISTOGRAM_WIDTH = 0x8026;
        public const uint HISTOGRAM_FORMAT = 0x8027;
        public const uint HISTOGRAM_RED_SIZE = 0x8028;
        public const uint HISTOGRAM_GREEN_SIZE = 0x8029;
        public const uint HISTOGRAM_BLUE_SIZE = 0x802A;
        public const uint HISTOGRAM_ALPHA_SIZE = 0x802B;
        public const uint HISTOGRAM_LUMINANCE_SIZE = 0x802C;
        public const uint HISTOGRAM_SINK = 0x802D;
        public const uint MINMAX = 0x802E; // 1 I
        public const uint MINMAX_FORMAT = 0x802F;
        public const uint MINMAX_SINK = 0x8030;
        public const uint TABLE_TOO_LARGE = 0x8031;
        public const uint COLOR_MATRIX = 0x80B1; // 16 F // Equivalent to SGI_color_matrix
        public const uint COLOR_MATRIX_STACK_DEPTH = 0x80B2; // 1 I
        public const uint MAX_COLOR_MATRIX_STACK_DEPTH = 0x80B3; // 1 I
        public const uint POST_COLOR_MATRIX_RED_SCALE = 0x80B4; // 1 F
        public const uint POST_COLOR_MATRIX_GREEN_SCALE = 0x80B5; // 1 F
        public const uint POST_COLOR_MATRIX_BLUE_SCALE = 0x80B6; // 1 F
        public const uint POST_COLOR_MATRIX_ALPHA_SCALE = 0x80B7; // 1 F
        public const uint POST_COLOR_MATRIX_RED_BIAS = 0x80B8; // 1 F
        public const uint POST_COLOR_MATRIX_GREEN_BIAS = 0x80B9; // 1 F
        public const uint POST_COLOR_MATRIX_BLUE_BIAS = 0x80BA; // 1 F
        public const uint POST_COLOR_MATRIX_ALPHA_BIAS = 0x80BB; // 1 F
        public const uint COLOR_TABLE = 0x80D0; // 1 I // Equivalent to SGI_color_table
        public const uint POST_CONVOLUTION_COLOR_TABLE = 0x80D1; // 1 I
        public const uint POST_COLOR_MATRIX_COLOR_TABLE = 0x80D2; // 1 I
        public const uint PROXY_COLOR_TABLE = 0x80D3;
        public const uint PROXY_POST_CONVOLUTION_COLOR_TABLE = 0x80D4;
        public const uint PROXY_POST_COLOR_MATRIX_COLOR_TABLE = 0x80D5;
        public const uint COLOR_TABLE_SCALE = 0x80D6;
        public const uint COLOR_TABLE_BIAS = 0x80D7;
        public const uint COLOR_TABLE_FORMAT = 0x80D8;
        public const uint COLOR_TABLE_WIDTH = 0x80D9;
        public const uint COLOR_TABLE_RED_SIZE = 0x80DA;
        public const uint COLOR_TABLE_GREEN_SIZE = 0x80DB;
        public const uint COLOR_TABLE_BLUE_SIZE = 0x80DC;
        public const uint COLOR_TABLE_ALPHA_SIZE = 0x80DD;
        public const uint COLOR_TABLE_LUMINANCE_SIZE = 0x80DE;
        public const uint COLOR_TABLE_INTENSITY_SIZE = 0x80DF;
        public const uint CONSTANT_BORDER = 0x8151;
        public const uint REPLICATE_BORDER = 0x8153;
        public const uint CONVOLUTION_BORDER_COLOR = 0x8154;


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 1.3 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_1_3
        public const uint TEXTURE0 = 0x84C0;	// Promoted from ARB_multitexture
        public const uint TEXTURE1 = 0x84C1;
        public const uint TEXTURE2 = 0x84C2;
        public const uint TEXTURE3 = 0x84C3;
        public const uint TEXTURE4 = 0x84C4;
        public const uint TEXTURE5 = 0x84C5;
        public const uint TEXTURE6 = 0x84C6;
        public const uint TEXTURE7 = 0x84C7;
        public const uint TEXTURE8 = 0x84C8;
        public const uint TEXTURE9 = 0x84C9;
        public const uint TEXTURE10 = 0x84CA;
        public const uint TEXTURE11 = 0x84CB;
        public const uint TEXTURE12 = 0x84CC;
        public const uint TEXTURE13 = 0x84CD;
        public const uint TEXTURE14 = 0x84CE;
        public const uint TEXTURE15 = 0x84CF;
        public const uint TEXTURE16 = 0x84D0;
        public const uint TEXTURE17 = 0x84D1;
        public const uint TEXTURE18 = 0x84D2;
        public const uint TEXTURE19 = 0x84D3;
        public const uint TEXTURE20 = 0x84D4;
        public const uint TEXTURE21 = 0x84D5;
        public const uint TEXTURE22 = 0x84D6;
        public const uint TEXTURE23 = 0x84D7;
        public const uint TEXTURE24 = 0x84D8;
        public const uint TEXTURE25 = 0x84D9;
        public const uint TEXTURE26 = 0x84DA;
        public const uint TEXTURE27 = 0x84DB;
        public const uint TEXTURE28 = 0x84DC;
        public const uint TEXTURE29 = 0x84DD;
        public const uint TEXTURE30 = 0x84DE;
        public const uint TEXTURE31 = 0x84DF;
        public const uint ACTIVE_TEXTURE = 0x84E0; // 1 I
        public const uint MULTISAMPLE = 0x809D;	// Promoted from ARB_multisample
        public const uint SAMPLE_ALPHA_TO_COVERAGE = 0x809E;
        public const uint SAMPLE_ALPHA_TO_ONE = 0x809F;
        public const uint SAMPLE_COVERAGE = 0x80A0;
        public const uint SAMPLE_BUFFERS = 0x80A8;
        public const uint SAMPLES = 0x80A9;
        public const uint SAMPLE_COVERAGE_VALUE = 0x80AA;
        public const uint SAMPLE_COVERAGE_INVERT = 0x80AB;
        public const uint TEXTURE_CUBE_MAP = 0x8513;
        public const uint TEXTURE_BINDING_CUBE_MAP = 0x8514;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A;
        public const uint PROXY_TEXTURE_CUBE_MAP = 0x851B;
        public const uint MAX_CUBE_MAP_TEXTURE_SIZE = 0x851C;
        public const uint COMPRESSED_RGB = 0x84ED;
        public const uint COMPRESSED_RGBA = 0x84EE;
        public const uint TEXTURE_COMPRESSION_HINT = 0x84EF;
        public const uint TEXTURE_COMPRESSED_IMAGE_SIZE = 0x86A0;
        public const uint TEXTURE_COMPRESSED = 0x86A1;
        public const uint NUM_COMPRESSED_TEXTURE_FORMATS = 0x86A2;
        public const uint COMPRESSED_TEXTURE_FORMATS = 0x86A3;
        public const uint CLAMP_TO_BORDER = 0x812D;	// Promoted from ARB_texture_border_clamp
        //profile: compatibility
        public const uint CLIENT_ACTIVE_TEXTURE = 0x84E1; // 1 I
        public const uint MAX_TEXTURE_UNITS = 0x84E2; // 1 I
        public const uint TRANSPOSE_MODELVIEW_MATRIX = 0x84E3; // 16 F // Promoted from ARB_transpose_matrix
        public const uint TRANSPOSE_PROJECTION_MATRIX = 0x84E4; // 16 F
        public const uint TRANSPOSE_TEXTURE_MATRIX = 0x84E5; // 16 F
        public const uint TRANSPOSE_COLOR_MATRIX = 0x84E6; // 16 F
        public const uint MULTISAMPLE_BIT = 0x20000000;
        public const uint NORMAL_MAP = 0x8511;	// Promoted from ARB_texture_cube_map
        public const uint REFLECTION_MAP = 0x8512;
        public const uint COMPRESSED_ALPHA = 0x84E9;	// Promoted from ARB_texture_compression
        public const uint COMPRESSED_LUMINANCE = 0x84EA;
        public const uint COMPRESSED_LUMINANCE_ALPHA = 0x84EB;
        public const uint COMPRESSED_INTENSITY = 0x84EC;
        public const uint COMBINE = 0x8570;	// Promoted from ARB_texture_env_combine
        public const uint COMBINE_RGB = 0x8571;
        public const uint COMBINE_ALPHA = 0x8572;
        public const uint SOURCE0_RGB = 0x8580;
        public const uint SOURCE1_RGB = 0x8581;
        public const uint SOURCE2_RGB = 0x8582;
        public const uint SOURCE0_ALPHA = 0x8588;
        public const uint SOURCE1_ALPHA = 0x8589;
        public const uint SOURCE2_ALPHA = 0x858A;
        public const uint OPERAND0_RGB = 0x8590;
        public const uint OPERAND1_RGB = 0x8591;
        public const uint OPERAND2_RGB = 0x8592;
        public const uint OPERAND0_ALPHA = 0x8598;
        public const uint OPERAND1_ALPHA = 0x8599;
        public const uint OPERAND2_ALPHA = 0x859A;
        public const uint RGB_SCALE = 0x8573;
        public const uint ADD_SIGNED = 0x8574;
        public const uint INTERPOLATE = 0x8575;
        public const uint SUBTRACT = 0x84E7;
        public const uint CONSTANT = 0x8576;
        public const uint PRIMARY_COLOR = 0x8577;
        public const uint PREVIOUS = 0x8578;
        public const uint DOT3_RGB = 0x86AE;	// Promoted from ARB_texture_env_dot3
        public const uint DOT3_RGBA = 0x86AF;


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 1.4 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_1_4
        public const uint BLEND_DST_RGB = 0x80C8;
        public const uint BLEND_SRC_RGB = 0x80C9;
        public const uint BLEND_DST_ALPHA = 0x80CA;
        public const uint BLEND_SRC_ALPHA = 0x80CB;
        public const uint POINT_FADE_THRESHOLD_SIZE = 0x8128; // 1 F
        public const uint DEPTH_COMPONENT16 = 0x81A5;
        public const uint DEPTH_COMPONENT24 = 0x81A6;
        public const uint DEPTH_COMPONENT32 = 0x81A7;
        public const uint MIRRORED_REPEAT = 0x8370;
        public const uint MAX_TEXTURE_LOD_BIAS = 0x84FD;
        public const uint TEXTURE_LOD_BIAS = 0x8501;
        public const uint INCR_WRAP = 0x8507;
        public const uint DECR_WRAP = 0x8508;
        public const uint TEXTURE_DEPTH_SIZE = 0x884A;
        public const uint TEXTURE_COMPARE_MODE = 0x884C;
        public const uint TEXTURE_COMPARE_FUNC = 0x884D;
        //profile: compatibility
        public const uint POINT_SIZE_MIN = 0x8126; // 1 F
        public const uint POINT_SIZE_MAX = 0x8127; // 1 F
        public const uint POINT_DISTANCE_ATTENUATION = 0x8129; // 3 F
        public const uint GENERATE_MIPMAP = 0x8191;
        public const uint GENERATE_MIPMAP_HINT = 0x8192; // 1 I
        public const uint FOG_COORDINATE_SOURCE = 0x8450; // 1 I
        public const uint FOG_COORDINATE = 0x8451;
        public const uint FRAGMENT_DEPTH = 0x8452;
        public const uint CURRENT_FOG_COORDINATE = 0x8453; // 1 F
        public const uint FOG_COORDINATE_ARRAY_TYPE = 0x8454; // 1 I
        public const uint FOG_COORDINATE_ARRAY_STRIDE = 0x8455; // 1 I
        public const uint FOG_COORDINATE_ARRAY_POINTER = 0x8456;
        public const uint FOG_COORDINATE_ARRAY = 0x8457; // 1 I
        public const uint COLOR_SUM = 0x8458; // 1 I
        public const uint CURRENT_SECONDARY_COLOR = 0x8459; // 3 F
        public const uint SECONDARY_COLOR_ARRAY_SIZE = 0x845A; // 1 I
        public const uint SECONDARY_COLOR_ARRAY_TYPE = 0x845B; // 1 I
        public const uint SECONDARY_COLOR_ARRAY_STRIDE = 0x845C; // 1 I
        public const uint SECONDARY_COLOR_ARRAY_POINTER = 0x845D;
        public const uint SECONDARY_COLOR_ARRAY = 0x845E; // 1 I
        public const uint TEXTURE_FILTER_CONTROL = 0x8500;
        public const uint DEPTH_TEXTURE_MODE = 0x884B;
        public const uint COMPARE_R_TO_TEXTURE = 0x884E;


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 1.5 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_1_5
        public const uint BUFFER_SIZE = 0x8764; // ARB_vertex_buffer_object
        public const uint BUFFER_USAGE = 0x8765; // ARB_vertex_buffer_object
        public const uint QUERY_COUNTER_BITS = 0x8864; // ARB_occlusion_query
        public const uint CURRENT_QUERY = 0x8865; // ARB_occlusion_query
        public const uint QUERY_RESULT = 0x8866; // ARB_occlusion_query
        public const uint QUERY_RESULT_AVAILABLE = 0x8867; // ARB_occlusion_query
        public const uint ARRAY_BUFFER = 0x8892; // ARB_vertex_buffer_object
        public const uint ELEMENT_ARRAY_BUFFER = 0x8893; // ARB_vertex_buffer_object
        public const uint ARRAY_BUFFER_BINDING = 0x8894; // ARB_vertex_buffer_object
        public const uint ELEMENT_ARRAY_BUFFER_BINDING = 0x8895; // ARB_vertex_buffer_object
        public const uint VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 0x889F; // ARB_vertex_buffer_object
        public const uint READ_ONLY = 0x88B8; // ARB_vertex_buffer_object
        public const uint WRITE_ONLY = 0x88B9; // ARB_vertex_buffer_object
        public const uint READ_WRITE = 0x88BA; // ARB_vertex_buffer_object
        public const uint BUFFER_ACCESS = 0x88BB; // ARB_vertex_buffer_object
        public const uint BUFFER_MAPPED = 0x88BC; // ARB_vertex_buffer_object
        public const uint BUFFER_MAP_POINTER = 0x88BD; // ARB_vertex_buffer_object
        public const uint STREAM_DRAW = 0x88E0; // ARB_vertex_buffer_object
        public const uint STREAM_READ = 0x88E1; // ARB_vertex_buffer_object
        public const uint STREAM_COPY = 0x88E2; // ARB_vertex_buffer_object
        public const uint STATIC_DRAW = 0x88E4; // ARB_vertex_buffer_object
        public const uint STATIC_READ = 0x88E5; // ARB_vertex_buffer_object
        public const uint STATIC_COPY = 0x88E6; // ARB_vertex_buffer_object
        public const uint DYNAMIC_DRAW = 0x88E8; // ARB_vertex_buffer_object
        public const uint DYNAMIC_READ = 0x88E9; // ARB_vertex_buffer_object
        public const uint DYNAMIC_COPY = 0x88EA; // ARB_vertex_buffer_object
        public const uint SAMPLES_PASSED = 0x8914; // ARB_occlusion_query
        //// New naming scheme (reintroduced in GL 3.3)
        public const uint SRC1_ALPHA = 0x8589;    // alias GL_SOURCE1_ALPHA
        //profile: compatibility
        public const uint VERTEX_ARRAY_BUFFER_BINDING = 0x8896; // ARB_vertex_buffer_object
        public const uint NORMAL_ARRAY_BUFFER_BINDING = 0x8897; // ARB_vertex_buffer_object
        public const uint COLOR_ARRAY_BUFFER_BINDING = 0x8898; // ARB_vertex_buffer_object
        public const uint INDEX_ARRAY_BUFFER_BINDING = 0x8899; // ARB_vertex_buffer_object
        public const uint TEXTURE_COORD_ARRAY_BUFFER_BINDING = 0x889A; // ARB_vertex_buffer_object
        public const uint EDGE_FLAG_ARRAY_BUFFER_BINDING = 0x889B; // ARB_vertex_buffer_object
        public const uint SECONDARY_COLOR_ARRAY_BUFFER_BINDING = 0x889C; // ARB_vertex_buffer_object
        public const uint FOG_COORDINATE_ARRAY_BUFFER_BINDING = 0x889D; // ARB_vertex_buffer_object
        public const uint WEIGHT_ARRAY_BUFFER_BINDING = 0x889E; // ARB_vertex_buffer_object
        public const uint FOG_COORD_SRC = 0x8450;    // alias GL_FOG_COORDINATE_SOURCE
        public const uint FOG_COORD = 0x8451;    // alias GL_FOG_COORDINATE
        public const uint CURRENT_FOG_COORD = 0x8453;    // alias GL_CURRENT_FOG_COORDINATE
        public const uint FOG_COORD_ARRAY_TYPE = 0x8454;    // alias GL_FOG_COORDINATE_ARRAY_TYPE
        public const uint FOG_COORD_ARRAY_STRIDE = 0x8455;    // alias GL_FOG_COORDINATE_ARRAY_STRIDE
        public const uint FOG_COORD_ARRAY_POINTER = 0x8456;    // alias GL_FOG_COORDINATE_ARRAY_POINTER
        public const uint FOG_COORD_ARRAY = 0x8457;    // alias GL_FOG_COORDINATE_ARRAY
        public const uint FOG_COORD_ARRAY_BUFFER_BINDING = 0x889D;    // alias GL_FOG_COORDINATE_ARRAY_BUFFER_BINDING
        //// New naming scheme
        public const uint SRC0_RGB = 0x8580;    // alias GL_SOURCE0_RGB
        public const uint SRC1_RGB = 0x8581;    // alias GL_SOURCE1_RGB
        public const uint SRC2_RGB = 0x8582;    // alias GL_SOURCE2_RGB
        public const uint SRC0_ALPHA = 0x8588;    // alias GL_SOURCE0_ALPHA
        public const uint SRC2_ALPHA = 0x858A;    // alias GL_SOURCE2_ALPHA

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 2.0 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_2_0
        public const uint BLEND_EQUATION_RGB = 0x8009;    // EXT_blend_equation_separate   // alias GL_BLEND_EQUATION
        public const uint VERTEX_ATTRIB_ARRAY_ENABLED = 0x8622;    // ARB_vertex_shader
        public const uint VERTEX_ATTRIB_ARRAY_SIZE = 0x8623;    // ARB_vertex_shader
        public const uint VERTEX_ATTRIB_ARRAY_STRIDE = 0x8624;    // ARB_vertex_shader
        public const uint VERTEX_ATTRIB_ARRAY_TYPE = 0x8625;    // ARB_vertex_shader
        public const uint CURRENT_VERTEX_ATTRIB = 0x8626;    // ARB_vertex_shader
        public const uint VERTEX_PROGRAM_POINT_SIZE = 0x8642;    // ARB_vertex_shader
        public const uint VERTEX_ATTRIB_ARRAY_POINTER = 0x8645;    // ARB_vertex_shader
        public const uint STENCIL_BACK_FUNC = 0x8800;    // ARB_stencil_two_side
        public const uint STENCIL_BACK_FAIL = 0x8801;    // ARB_stencil_two_side
        public const uint STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802;    // ARB_stencil_two_side
        public const uint STENCIL_BACK_PASS_DEPTH_PASS = 0x8803;    // ARB_stencil_two_side
        public const uint MAX_DRAW_BUFFERS = 0x8824;    // ARB_draw_buffers
        public const uint DRAW_BUFFER0 = 0x8825;    // ARB_draw_buffers
        public const uint DRAW_BUFFER1 = 0x8826;    // ARB_draw_buffers
        public const uint DRAW_BUFFER2 = 0x8827;    // ARB_draw_buffers
        public const uint DRAW_BUFFER3 = 0x8828;    // ARB_draw_buffers
        public const uint DRAW_BUFFER4 = 0x8829;    // ARB_draw_buffers
        public const uint DRAW_BUFFER5 = 0x882A;    // ARB_draw_buffers
        public const uint DRAW_BUFFER6 = 0x882B;    // ARB_draw_buffers
        public const uint DRAW_BUFFER7 = 0x882C;    // ARB_draw_buffers
        public const uint DRAW_BUFFER8 = 0x882D;    // ARB_draw_buffers
        public const uint DRAW_BUFFER9 = 0x882E;    // ARB_draw_buffers
        public const uint DRAW_BUFFER10 = 0x882F;    // ARB_draw_buffers
        public const uint DRAW_BUFFER11 = 0x8830;    // ARB_draw_buffers
        public const uint DRAW_BUFFER12 = 0x8831;    // ARB_draw_buffers
        public const uint DRAW_BUFFER13 = 0x8832;    // ARB_draw_buffers
        public const uint DRAW_BUFFER14 = 0x8833;    // ARB_draw_buffers
        public const uint DRAW_BUFFER15 = 0x8834;    // ARB_draw_buffers
        public const uint BLEND_EQUATION_ALPHA = 0x883D;    // EXT_blend_equation_separate
        public const uint MAX_VERTEX_ATTRIBS = 0x8869;    // ARB_vertex_shader
        public const uint VERTEX_ATTRIB_ARRAY_NORMALIZED = 0x886A;    // ARB_vertex_shader
        public const uint MAX_TEXTURE_IMAGE_UNITS = 0x8872;    // ARB_vertex_shader, ARB_fragment_shader
        public const uint FRAGMENT_SHADER = 0x8B30;    // ARB_fragment_shader
        public const uint VERTEX_SHADER = 0x8B31;    // ARB_vertex_shader
        public const uint MAX_FRAGMENT_UNIFORM_COMPONENTS = 0x8B49;    // ARB_fragment_shader
        public const uint MAX_VERTEX_UNIFORM_COMPONENTS = 0x8B4A;    // ARB_vertex_shader
        public const uint MAX_VARYING_FLOATS = 0x8B4B;    // ARB_vertex_shader
        public const uint MAX_VERTEX_TEXTURE_IMAGE_UNITS = 0x8B4C;    // ARB_vertex_shader
        public const uint MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D;    // ARB_vertex_shader
        public const uint SHADER_TYPE = 0x8B4F;    // ARB_shader_objects
        public const uint FLOAT_VEC2 = 0x8B50;    // ARB_shader_objects
        public const uint FLOAT_VEC3 = 0x8B51;    // ARB_shader_objects
        public const uint FLOAT_VEC4 = 0x8B52;    // ARB_shader_objects
        public const uint INT_VEC2 = 0x8B53;    // ARB_shader_objects
        public const uint INT_VEC3 = 0x8B54;    // ARB_shader_objects
        public const uint INT_VEC4 = 0x8B55;    // ARB_shader_objects
        public const uint BOOL = 0x8B56;    // ARB_shader_objects
        public const uint BOOL_VEC2 = 0x8B57;    // ARB_shader_objects
        public const uint BOOL_VEC3 = 0x8B58;    // ARB_shader_objects
        public const uint BOOL_VEC4 = 0x8B59;    // ARB_shader_objects
        public const uint FLOAT_MAT2 = 0x8B5A;    // ARB_shader_objects
        public const uint FLOAT_MAT3 = 0x8B5B;    // ARB_shader_objects
        public const uint FLOAT_MAT4 = 0x8B5C;    // ARB_shader_objects
        public const uint SAMPLER_1D = 0x8B5D;    // ARB_shader_objects
        public const uint SAMPLER_2D = 0x8B5E;    // ARB_shader_objects
        public const uint SAMPLER_3D = 0x8B5F;    // ARB_shader_objects
        public const uint SAMPLER_CUBE = 0x8B60;    // ARB_shader_objects
        public const uint SAMPLER_1D_SHADOW = 0x8B61;    // ARB_shader_objects
        public const uint SAMPLER_2D_SHADOW = 0x8B62;    // ARB_shader_objects
        public const uint DELETE_STATUS = 0x8B80;    // ARB_shader_objects
        public const uint COMPILE_STATUS = 0x8B81;    // ARB_shader_objects
        public const uint LINK_STATUS = 0x8B82;    // ARB_shader_objects
        public const uint VALIDATE_STATUS = 0x8B83;    // ARB_shader_objects
        public const uint INFO_LOG_LENGTH = 0x8B84;    // ARB_shader_objects
        public const uint ATTACHED_SHADERS = 0x8B85;    // ARB_shader_objects
        public const uint ACTIVE_UNIFORMS = 0x8B86;    // ARB_shader_objects
        public const uint ACTIVE_UNIFORM_MAX_LENGTH = 0x8B87;    // ARB_shader_objects
        public const uint SHADER_SOURCE_LENGTH = 0x8B88;    // ARB_shader_objects
        public const uint ACTIVE_ATTRIBUTES = 0x8B89;    // ARB_vertex_shader
        public const uint ACTIVE_ATTRIBUTE_MAX_LENGTH = 0x8B8A;    // ARB_vertex_shader
        public const uint FRAGMENT_SHADER_DERIVATIVE_HINT = 0x8B8B;    // ARB_fragment_shader
        public const uint SHADING_LANGUAGE_VERSION = 0x8B8C;    // ARB_shading_language_100
        public const uint CURRENT_PROGRAM = 0x8B8D;    // ARB_shader_objects (added for 2.0)
        public const uint POINT_SPRITE_COORD_ORIGIN = 0x8CA0;    // ARB_point_sprite (added for 2.0)
        public const uint LOWER_LEFT = 0x8CA1;    // ARB_point_sprite (added for 2.0)
        public const uint UPPER_LEFT = 0x8CA2;    // ARB_point_sprite (added for 2.0)
        public const uint STENCIL_BACK_REF = 0x8CA3;    // ARB_stencil_two_side
        public const uint STENCIL_BACK_VALUE_MASK = 0x8CA4;    // ARB_stencil_two_side
        public const uint STENCIL_BACK_WRITEMASK = 0x8CA5;    // ARB_stencil_two_side
        //profile: compatibility
        public const uint VERTEX_PROGRAM_TWO_SIDE = 0x8643;    // ARB_vertex_shader
        public const uint POINT_SPRITE = 0x8861;    // ARB_point_sprite
        public const uint COORD_REPLACE = 0x8862;    // ARB_point_sprite
        public const uint MAX_TEXTURE_COORDS = 0x8871;    // ARB_vertex_shader, ARB_fragment_shader


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 2.1 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_2_1
        public const uint PIXEL_PACK_BUFFER = 0x88EB;    // ARB_pixel_buffer_object
        public const uint PIXEL_UNPACK_BUFFER = 0x88EC;    // ARB_pixel_buffer_object
        public const uint PIXEL_PACK_BUFFER_BINDING = 0x88ED;    // ARB_pixel_buffer_object
        public const uint PIXEL_UNPACK_BUFFER_BINDING = 0x88EF;    // ARB_pixel_buffer_object
        public const uint FLOAT_MAT2x3 = 0x8B65;    // New for 2.1
        public const uint FLOAT_MAT2x4 = 0x8B66;    // New for 2.1
        public const uint FLOAT_MAT3x2 = 0x8B67;    // New for 2.1
        public const uint FLOAT_MAT3x4 = 0x8B68;    // New for 2.1
        public const uint FLOAT_MAT4x2 = 0x8B69;    // New for 2.1
        public const uint FLOAT_MAT4x3 = 0x8B6A;    // New for 2.1
        public const uint SRGB = 0x8C40;    // EXT_texture_sRGB
        public const uint SRGB8 = 0x8C41;    // EXT_texture_sRGB
        public const uint SRGB_ALPHA = 0x8C42;    // EXT_texture_sRGB
        public const uint SRGB8_ALPHA8 = 0x8C43;    // EXT_texture_sRGB
        public const uint COMPRESSED_SRGB = 0x8C48;    // EXT_texture_sRGB
        public const uint COMPRESSED_SRGB_ALPHA = 0x8C49;    // EXT_texture_sRGB
        //profile: compatibility
        public const uint CURRENT_RASTER_SECONDARY_COLOR = 0x845F;    // New for 2.1
        public const uint SLUMINANCE_ALPHA = 0x8C44;    // EXT_texture_sRGB
        public const uint SLUMINANCE8_ALPHA8 = 0x8C45;    // EXT_texture_sRGB
        public const uint SLUMINANCE = 0x8C46;    // EXT_texture_sRGB
        public const uint SLUMINANCE8 = 0x8C47;    // EXT_texture_sRGB
        public const uint COMPRESSED_SLUMINANCE = 0x8C4A;    // EXT_texture_sRGB
        public const uint COMPRESSED_SLUMINANCE_ALPHA = 0x8C4B;    // EXT_texture_sRGB


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 3.0 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_3_0
        public const uint COMPARE_REF_TO_TEXTURE = 0x884E;    // alias GL_COMPARE_R_TO_TEXTURE_ARB
        public const uint CLIP_DISTANCE0 = 0x3000;    // alias GL_CLIP_PLANE0
        public const uint CLIP_DISTANCE1 = 0x3001;    // alias GL_CLIP_PLANE1
        public const uint CLIP_DISTANCE2 = 0x3002;    // alias GL_CLIP_PLANE2
        public const uint CLIP_DISTANCE3 = 0x3003;    // alias GL_CLIP_PLANE3
        public const uint CLIP_DISTANCE4 = 0x3004;    // alias GL_CLIP_PLANE4
        public const uint CLIP_DISTANCE5 = 0x3005;    // alias GL_CLIP_PLANE5
        public const uint CLIP_DISTANCE6 = 0x3006;
        public const uint CLIP_DISTANCE7 = 0x3007;
        public const uint MAX_CLIP_DISTANCES = 0x0D32;    // alias GL_MAX_CLIP_PLANES
        public const uint MAJOR_VERSION = 0x821B;
        public const uint MINOR_VERSION = 0x821C;
        public const uint NUM_EXTENSIONS = 0x821D;
        public const uint CONTEXT_FLAGS = 0x821E;
        public const uint COMPRESSED_RED = 0x8225;
        public const uint COMPRESSED_RG = 0x8226;
        public const uint CONTEXT_FLAG_FORWARD_COMPATIBLE_BIT = 0x00000001;
        public const uint RGBA32F = 0x8814;
        public const uint RGB32F = 0x8815;
        public const uint RGBA16F = 0x881A;
        public const uint RGB16F = 0x881B;
        public const uint VERTEX_ATTRIB_ARRAY_INTEGER = 0x88FD;
        public const uint MAX_ARRAY_TEXTURE_LAYERS = 0x88FF;
        public const uint MIN_PROGRAM_TEXEL_OFFSET = 0x8904;
        public const uint MAX_PROGRAM_TEXEL_OFFSET = 0x8905;
        public const uint CLAMP_READ_COLOR = 0x891C;
        public const uint FIXED_ONLY = 0x891D;
        public const uint MAX_VARYING_COMPONENTS = 0x8B4B;    // alias GL_MAX_VARYING_FLOATS
        public const uint TEXTURE_1D_ARRAY = 0x8C18;
        public const uint PROXY_TEXTURE_1D_ARRAY = 0x8C19;
        public const uint TEXTURE_2D_ARRAY = 0x8C1A;
        public const uint PROXY_TEXTURE_2D_ARRAY = 0x8C1B;
        public const uint TEXTURE_BINDING_1D_ARRAY = 0x8C1C;
        public const uint TEXTURE_BINDING_2D_ARRAY = 0x8C1D;
        public const uint R11F_G11F_B10F = 0x8C3A;
        public const uint UNSIGNED_INT_10F_11F_11F_REV = 0x8C3B;
        public const uint RGB9_E5 = 0x8C3D;
        public const uint UNSIGNED_INT_5_9_9_9_REV = 0x8C3E;
        public const uint TEXTURE_SHARED_SIZE = 0x8C3F;
        public const uint TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH = 0x8C76;
        public const uint TRANSFORM_FEEDBACK_BUFFER_MODE = 0x8C7F;
        public const uint MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS = 0x8C80;
        public const uint TRANSFORM_FEEDBACK_VARYINGS = 0x8C83;
        public const uint TRANSFORM_FEEDBACK_BUFFER_START = 0x8C84;
        public const uint TRANSFORM_FEEDBACK_BUFFER_SIZE = 0x8C85;
        public const uint PRIMITIVES_GENERATED = 0x8C87;
        public const uint TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN = 0x8C88;
        public const uint RASTERIZER_DISCARD = 0x8C89;
        public const uint MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS = 0x8C8A;
        public const uint MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS = 0x8C8B;
        public const uint INTERLEAVED_ATTRIBS = 0x8C8C;
        public const uint SEPARATE_ATTRIBS = 0x8C8D;
        public const uint TRANSFORM_FEEDBACK_BUFFER = 0x8C8E;
        public const uint TRANSFORM_FEEDBACK_BUFFER_BINDING = 0x8C8F;
        public const uint RGBA32UI = 0x8D70;
        public const uint RGB32UI = 0x8D71;
        public const uint RGBA16UI = 0x8D76;
        public const uint RGB16UI = 0x8D77;
        public const uint RGBA8UI = 0x8D7C;
        public const uint RGB8UI = 0x8D7D;
        public const uint RGBA32I = 0x8D82;
        public const uint RGB32I = 0x8D83;
        public const uint RGBA16I = 0x8D88;
        public const uint RGB16I = 0x8D89;
        public const uint RGBA8I = 0x8D8E;
        public const uint RGB8I = 0x8D8F;
        public const uint RED_INTEGER = 0x8D94;
        public const uint GREEN_INTEGER = 0x8D95;
        public const uint BLUE_INTEGER = 0x8D96;
        public const uint RGB_INTEGER = 0x8D98;
        public const uint RGBA_INTEGER = 0x8D99;
        public const uint BGR_INTEGER = 0x8D9A;
        public const uint BGRA_INTEGER = 0x8D9B;
        public const uint SAMPLER_1D_ARRAY = 0x8DC0;
        public const uint SAMPLER_2D_ARRAY = 0x8DC1;
        public const uint SAMPLER_1D_ARRAY_SHADOW = 0x8DC3;
        public const uint SAMPLER_2D_ARRAY_SHADOW = 0x8DC4;
        public const uint SAMPLER_CUBE_SHADOW = 0x8DC5;
        public const uint UNSIGNED_INT_VEC2 = 0x8DC6;
        public const uint UNSIGNED_INT_VEC3 = 0x8DC7;
        public const uint UNSIGNED_INT_VEC4 = 0x8DC8;
        public const uint INT_SAMPLER_1D = 0x8DC9;
        public const uint INT_SAMPLER_2D = 0x8DCA;
        public const uint INT_SAMPLER_3D = 0x8DCB;
        public const uint INT_SAMPLER_CUBE = 0x8DCC;
        public const uint INT_SAMPLER_1D_ARRAY = 0x8DCE;
        public const uint INT_SAMPLER_2D_ARRAY = 0x8DCF;
        public const uint UNSIGNED_INT_SAMPLER_1D = 0x8DD1;
        public const uint UNSIGNED_INT_SAMPLER_2D = 0x8DD2;
        public const uint UNSIGNED_INT_SAMPLER_3D = 0x8DD3;
        public const uint UNSIGNED_INT_SAMPLER_CUBE = 0x8DD4;
        public const uint UNSIGNED_INT_SAMPLER_1D_ARRAY = 0x8DD6;
        public const uint UNSIGNED_INT_SAMPLER_2D_ARRAY = 0x8DD7;
        public const uint QUERY_WAIT = 0x8E13;
        public const uint QUERY_NO_WAIT = 0x8E14;
        public const uint QUERY_BY_REGION_WAIT = 0x8E15;
        public const uint QUERY_BY_REGION_NO_WAIT = 0x8E16;
        public const uint BUFFER_ACCESS_FLAGS = 0x911F;
        public const uint BUFFER_MAP_LENGTH = 0x9120;
        public const uint BUFFER_MAP_OFFSET = 0x9121;
        /* Reuse tokens from ARB_depth_buffer_float */

        //    use ARB_depth_buffer_float	    DEPTH_COMPONENT32F
        //    use ARB_depth_buffer_float	    DEPTH32F_STENCIL8
        //    use ARB_depth_buffer_float	    FLOAT_32_UNSIGNED_INT_24_8_REV
        /* Reuse tokens from ARB_framebuffer_object */

        //    use ARB_framebuffer_object	    INVALID_FRAMEBUFFER_OPERATION
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_COLOR_ENCODING
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_COMPONENT_TYPE
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_RED_SIZE
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_GREEN_SIZE
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_BLUE_SIZE
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_ALPHA_SIZE
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_DEPTH_SIZE
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_STENCIL_SIZE
        //    use ARB_framebuffer_object	    FRAMEBUFFER_DEFAULT
        //    use ARB_framebuffer_object	    FRAMEBUFFER_UNDEFINED
        //    use ARB_framebuffer_object	    DEPTH_STENCIL_ATTACHMENT
        //    use ARB_framebuffer_object	    INDEX
        //    use ARB_framebuffer_object	    MAX_RENDERBUFFER_SIZE
        //    use ARB_framebuffer_object	    DEPTH_STENCIL
        //    use ARB_framebuffer_object	    UNSIGNED_INT_24_8
        //    use ARB_framebuffer_object	    DEPTH24_STENCIL8
        //    use ARB_framebuffer_object	    TEXTURE_STENCIL_SIZE
        //    use ARB_framebuffer_object	    TEXTURE_RED_TYPE
        //    use ARB_framebuffer_object	    TEXTURE_GREEN_TYPE
        //    use ARB_framebuffer_object	    TEXTURE_BLUE_TYPE
        //    use ARB_framebuffer_object	    TEXTURE_ALPHA_TYPE
        //    use ARB_framebuffer_object	    TEXTURE_DEPTH_TYPE
        //    use ARB_framebuffer_object	    UNSIGNED_NORMALIZED
        //    use ARB_framebuffer_object	    FRAMEBUFFER_BINDING
        //    use ARB_framebuffer_object	    DRAW_FRAMEBUFFER_BINDING
        //    use ARB_framebuffer_object	    RENDERBUFFER_BINDING
        //    use ARB_framebuffer_object	    READ_FRAMEBUFFER
        //    use ARB_framebuffer_object	    DRAW_FRAMEBUFFER
        //    use ARB_framebuffer_object	    READ_FRAMEBUFFER_BINDING
        //    use ARB_framebuffer_object	    RENDERBUFFER_SAMPLES
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_OBJECT_NAME
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER
        //    use ARB_framebuffer_object	    FRAMEBUFFER_COMPLETE
        //    use ARB_framebuffer_object	    FRAMEBUFFER_INCOMPLETE_ATTACHMENT
        //    use ARB_framebuffer_object	    FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT
        //    use ARB_framebuffer_object	    FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER
        //    use ARB_framebuffer_object	    FRAMEBUFFER_INCOMPLETE_READ_BUFFER
        //    use ARB_framebuffer_object	    FRAMEBUFFER_UNSUPPORTED
        //    use ARB_framebuffer_object	    MAX_COLOR_ATTACHMENTS
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT0
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT1
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT2
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT3
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT4
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT5
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT6
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT7
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT8
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT9
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT10
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT11
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT12
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT13
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT14
        //    use ARB_framebuffer_object	    COLOR_ATTACHMENT15
        //    use ARB_framebuffer_object	    DEPTH_ATTACHMENT
        //    use ARB_framebuffer_object	    STENCIL_ATTACHMENT
        //    use ARB_framebuffer_object	    FRAMEBUFFER
        //    use ARB_framebuffer_object	    RENDERBUFFER
        //    use ARB_framebuffer_object	    RENDERBUFFER_WIDTH
        //    use ARB_framebuffer_object	    RENDERBUFFER_HEIGHT
        //    use ARB_framebuffer_object	    RENDERBUFFER_INTERNAL_FORMAT
        //    use ARB_framebuffer_object	    STENCIL_INDEX1
        //    use ARB_framebuffer_object	    STENCIL_INDEX4
        //    use ARB_framebuffer_object	    STENCIL_INDEX8
        //    use ARB_framebuffer_object	    STENCIL_INDEX16
        //    use ARB_framebuffer_object	    RENDERBUFFER_RED_SIZE
        //    use ARB_framebuffer_object	    RENDERBUFFER_GREEN_SIZE
        //    use ARB_framebuffer_object	    RENDERBUFFER_BLUE_SIZE
        //    use ARB_framebuffer_object	    RENDERBUFFER_ALPHA_SIZE
        //    use ARB_framebuffer_object	    RENDERBUFFER_DEPTH_SIZE
        //    use ARB_framebuffer_object	    RENDERBUFFER_STENCIL_SIZE
        //    use ARB_framebuffer_object	    FRAMEBUFFER_INCOMPLETE_MULTISAMPLE
        //    use ARB_framebuffer_object	    MAX_SAMPLES
        /* Reuse tokens from ARB_framebuffer_sRGB */

        //    use ARB_framebuffer_sRGB	    FRAMEBUFFER_SRGB
        /* Reuse tokens from ARB_half_float_vertex */

        //    use ARB_half_float_vertex	    HALF_FLOAT
        /* Reuse tokens from ARB_map_buffer_range */

        //    use ARB_map_buffer_range	    MAP_READ_BIT
        //    use ARB_map_buffer_range	    MAP_WRITE_BIT
        //    use ARB_map_buffer_range	    MAP_INVALIDATE_RANGE_BIT
        //    use ARB_map_buffer_range	    MAP_INVALIDATE_BUFFER_BIT
        //    use ARB_map_buffer_range	    MAP_FLUSH_EXPLICIT_BIT
        //    use ARB_map_buffer_range	    MAP_UNSYNCHRONIZED_BIT
        /* Reuse tokens from ARB_texture_compression_rgtc */

        //    use ARB_texture_compression_rgtc    COMPRESSED_RED_RGTC1
        //    use ARB_texture_compression_rgtc    COMPRESSED_SIGNED_RED_RGTC1
        //    use ARB_texture_compression_rgtc    COMPRESSED_RG_RGTC2
        //    use ARB_texture_compression_rgtc    COMPRESSED_SIGNED_RG_RGTC2
        /* Reuse tokens from ARB_texture_rg */

        //    use ARB_texture_rg		    RG
        //    use ARB_texture_rg		    RG_INTEGER
        //    use ARB_texture_rg		    R8
        //    use ARB_texture_rg		    R16
        //    use ARB_texture_rg		    RG8
        //    use ARB_texture_rg		    RG16
        //    use ARB_texture_rg		    R16F
        //    use ARB_texture_rg		    R32F
        //    use ARB_texture_rg		    RG16F
        //    use ARB_texture_rg		    RG32F
        //    use ARB_texture_rg		    R8I
        //    use ARB_texture_rg		    R8UI
        //    use ARB_texture_rg		    R16I
        //    use ARB_texture_rg		    R16UI
        //    use ARB_texture_rg		    R32I
        //    use ARB_texture_rg		    R32UI
        //    use ARB_texture_rg		    RG8I
        //    use ARB_texture_rg		    RG8UI
        //    use ARB_texture_rg		    RG16I
        //    use ARB_texture_rg		    RG16UI
        //    use ARB_texture_rg		    RG32I
        //    use ARB_texture_rg		    RG32UI
        /* Reuse tokens from ARB_vertex_array_object */

        //    use ARB_vertex_array_object	    VERTEX_ARRAY_BINDING
        //profile: compatibility
        public const uint CLAMP_VERTEX_COLOR = 0x891A;
        public const uint CLAMP_FRAGMENT_COLOR = 0x891B;
        public const uint ALPHA_INTEGER = 0x8D97;
        /* Reuse tokens from ARB_framebuffer_object */

        //    use ARB_framebuffer_object	    TEXTURE_LUMINANCE_TYPE
        //    use ARB_framebuffer_object	    TEXTURE_INTENSITY_TYPE


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 3.1 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_3_1
        public const uint SAMPLER_2D_RECT = 0x8B63;    // ARB_shader_objects + ARB_texture_rectangle
        public const uint SAMPLER_2D_RECT_SHADOW = 0x8B64;    // ARB_shader_objects + ARB_texture_rectangle
        public const uint SAMPLER_BUFFER = 0x8DC2;    // EXT_gpu_shader4 + ARB_texture_buffer_object
        public const uint INT_SAMPLER_2D_RECT = 0x8DCD;    // EXT_gpu_shader4 + ARB_texture_rectangle
        public const uint INT_SAMPLER_BUFFER = 0x8DD0;    // EXT_gpu_shader4 + ARB_texture_buffer_object
        public const uint UNSIGNED_INT_SAMPLER_2D_RECT = 0x8DD5;    // EXT_gpu_shader4 + ARB_texture_rectangle
        public const uint UNSIGNED_INT_SAMPLER_BUFFER = 0x8DD8;    // EXT_gpu_shader4 + ARB_texture_buffer_object
        public const uint TEXTURE_BUFFER = 0x8C2A;    // ARB_texture_buffer_object
        public const uint MAX_TEXTURE_BUFFER_SIZE = 0x8C2B;    // ARB_texture_buffer_object
        public const uint TEXTURE_BINDING_BUFFER = 0x8C2C;    // ARB_texture_buffer_object
        public const uint TEXTURE_BUFFER_DATA_STORE_BINDING = 0x8C2D;    // ARB_texture_buffer_object
        public const uint TEXTURE_RECTANGLE = 0x84F5;    // ARB_texture_rectangle
        public const uint TEXTURE_BINDING_RECTANGLE = 0x84F6;    // ARB_texture_rectangle
        public const uint PROXY_TEXTURE_RECTANGLE = 0x84F7;    // ARB_texture_rectangle
        public const uint MAX_RECTANGLE_TEXTURE_SIZE = 0x84F8;    // ARB_texture_rectangle
        public const uint RED_SNORM = 0x8F90;    // 3.1
        public const uint RG_SNORM = 0x8F91;    // 3.1
        public const uint RGB_SNORM = 0x8F92;    // 3.1
        public const uint RGBA_SNORM = 0x8F93;    // 3.1
        public const uint R8_SNORM = 0x8F94;    // 3.1
        public const uint RG8_SNORM = 0x8F95;    // 3.1
        public const uint RGB8_SNORM = 0x8F96;    // 3.1
        public const uint RGBA8_SNORM = 0x8F97;    // 3.1
        public const uint R16_SNORM = 0x8F98;    // 3.1
        public const uint RG16_SNORM = 0x8F99;    // 3.1
        public const uint RGB16_SNORM = 0x8F9A;    // 3.1
        public const uint RGBA16_SNORM = 0x8F9B;    // 3.1
        public const uint SIGNED_NORMALIZED = 0x8F9C;    // 3.1
        public const uint PRIMITIVE_RESTART = 0x8F9D;    // 3.1 (different from NV_primitive_restart)
        public const uint PRIMITIVE_RESTART_INDEX = 0x8F9E;    // 3.1 (different from NV_primitive_restart)
        /* Reuse tokens from ARB_copy_buffer */

        //    use ARB_copy_buffer		    COPY_READ_BUFFER
        //    use ARB_copy_buffer		    COPY_WRITE_BUFFER
        /* Reuse tokens from ARB_draw_instanced (none) */

        /* Reuse tokens from ARB_uniform_buffer_object */

        //    use ARB_uniform_buffer_object	    UNIFORM_BUFFER
        //    use ARB_uniform_buffer_object	    UNIFORM_BUFFER_BINDING
        //    use ARB_uniform_buffer_object	    UNIFORM_BUFFER_START
        //    use ARB_uniform_buffer_object	    UNIFORM_BUFFER_SIZE
        //    use ARB_uniform_buffer_object	    MAX_VERTEX_UNIFORM_BLOCKS
        //    use ARB_uniform_buffer_object	    MAX_FRAGMENT_UNIFORM_BLOCKS
        //    use ARB_uniform_buffer_object	    MAX_COMBINED_UNIFORM_BLOCKS
        //    use ARB_uniform_buffer_object	    MAX_UNIFORM_BUFFER_BINDINGS
        //    use ARB_uniform_buffer_object	    MAX_UNIFORM_BLOCK_SIZE
        //    use ARB_uniform_buffer_object	    MAX_COMBINED_VERTEX_UNIFORM_COMPONENTS
        //    use ARB_uniform_buffer_object	    MAX_COMBINED_FRAGMENT_UNIFORM_COMPONENTS
        //    use ARB_uniform_buffer_object	    UNIFORM_BUFFER_OFFSET_ALIGNMENT
        //    use ARB_uniform_buffer_object	    ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH
        //    use ARB_uniform_buffer_object	    ACTIVE_UNIFORM_BLOCKS
        //    use ARB_uniform_buffer_object	    UNIFORM_TYPE
        //    use ARB_uniform_buffer_object	    UNIFORM_SIZE
        //    use ARB_uniform_buffer_object	    UNIFORM_NAME_LENGTH
        //    use ARB_uniform_buffer_object	    UNIFORM_BLOCK_INDEX
        //    use ARB_uniform_buffer_object	    UNIFORM_OFFSET
        //    use ARB_uniform_buffer_object	    UNIFORM_ARRAY_STRIDE
        //    use ARB_uniform_buffer_object	    UNIFORM_MATRIX_STRIDE
        //    use ARB_uniform_buffer_object	    UNIFORM_IS_ROW_MAJOR
        //    use ARB_uniform_buffer_object	    UNIFORM_BLOCK_BINDING
        //    use ARB_uniform_buffer_object	    UNIFORM_BLOCK_DATA_SIZE
        //    use ARB_uniform_buffer_object	    UNIFORM_BLOCK_NAME_LENGTH
        //    use ARB_uniform_buffer_object	    UNIFORM_BLOCK_ACTIVE_UNIFORMS
        //    use ARB_uniform_buffer_object	    UNIFORM_BLOCK_ACTIVE_UNIFORM_INDICES
        //    use ARB_uniform_buffer_object	    UNIFORM_BLOCK_REFERENCED_BY_VERTEX_SHADER
        //    use ARB_uniform_buffer_object	    UNIFORM_BLOCK_REFERENCED_BY_FRAGMENT_SHADER
        //    use ARB_uniform_buffer_object	    INVALID_INDEX


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 3.2 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_3_2
        public const uint CONTEXT_CORE_PROFILE_BIT = 0x00000001;
        public const uint CONTEXT_COMPATIBILITY_PROFILE_BIT = 0x00000002;
        public const uint LINES_ADJACENCY = 0x000A;
        public const uint LINE_STRIP_ADJACENCY = 0x000B;
        public const uint TRIANGLES_ADJACENCY = 0x000C;
        public const uint TRIANGLE_STRIP_ADJACENCY = 0x000D;
        public const uint PROGRAM_POINT_SIZE = 0x8642;
        public const uint MAX_GEOMETRY_TEXTURE_IMAGE_UNITS = 0x8C29;
        public const uint FRAMEBUFFER_ATTACHMENT_LAYERED = 0x8DA7;
        public const uint FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS = 0x8DA8;
        public const uint GEOMETRY_SHADER = 0x8DD9;
        public const uint GEOMETRY_VERTICES_OUT = 0x8916;
        public const uint GEOMETRY_INPUT_TYPE = 0x8917;
        public const uint GEOMETRY_OUTPUT_TYPE = 0x8918;
        public const uint MAX_GEOMETRY_UNIFORM_COMPONENTS = 0x8DDF;
        public const uint MAX_GEOMETRY_OUTPUT_VERTICES = 0x8DE0;
        public const uint MAX_GEOMETRY_TOTAL_OUTPUT_COMPONENTS = 0x8DE1;
        public const uint MAX_VERTEX_OUTPUT_COMPONENTS = 0x9122;
        public const uint MAX_GEOMETRY_INPUT_COMPONENTS = 0x9123;
        public const uint MAX_GEOMETRY_OUTPUT_COMPONENTS = 0x9124;
        public const uint MAX_FRAGMENT_INPUT_COMPONENTS = 0x9125;
        public const uint CONTEXT_PROFILE_MASK = 0x9126;
        //    use VERSION_3_0			    MAX_VARYING_COMPONENTS
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER
        /* Reuse tokens from ARB_depth_clamp */

        //    use ARB_depth_clamp		    DEPTH_CLAMP
        /* Reuse tokens from ARB_draw_elements_base_vertex (none) */

        /* Reuse tokens from ARB_fragment_coord_conventions (none) */

        /* Reuse tokens from ARB_provoking_vertex */

        //    use ARB_provoking_vertex	    QUADS_FOLLOW_PROVOKING_VERTEX_CONVENTION
        //    use ARB_provoking_vertex	    FIRST_VERTEX_CONVENTION
        //    use ARB_provoking_vertex	    LAST_VERTEX_CONVENTION
        //    use ARB_provoking_vertex	    PROVOKING_VERTEX
        /* Reuse tokens from ARB_seamless_cube_map */

        //    use ARB_seamless_cube_map	    TEXTURE_CUBE_MAP_SEAMLESS
        /* Reuse tokens from ARB_sync */

        //    use ARB_sync			    MAX_SERVER_WAIT_TIMEOUT
        //    use ARB_sync			    OBJECT_TYPE
        //    use ARB_sync			    SYNC_CONDITION
        //    use ARB_sync			    SYNC_STATUS
        //    use ARB_sync			    SYNC_FLAGS
        //    use ARB_sync			    SYNC_FENCE
        //    use ARB_sync			    SYNC_GPU_COMMANDS_COMPLETE
        //    use ARB_sync			    UNSIGNALED
        //    use ARB_sync			    SIGNALED
        //    use ARB_sync			    ALREADY_SIGNALED
        //    use ARB_sync			    TIMEOUT_EXPIRED
        //    use ARB_sync			    CONDITION_SATISFIED
        //    use ARB_sync			    WAIT_FAILED
        //    use ARB_sync			    TIMEOUT_IGNORED
        //    use ARB_sync			    SYNC_FLUSH_COMMANDS_BIT
        //    use ARB_sync			    TIMEOUT_IGNORED
        /* Reuse tokens from ARB_texture_multisample */

        //    use ARB_texture_multisample	    SAMPLE_POSITION
        //    use ARB_texture_multisample	    SAMPLE_MASK
        //    use ARB_texture_multisample	    SAMPLE_MASK_VALUE
        //    use ARB_texture_multisample	    MAX_SAMPLE_MASK_WORDS
        //    use ARB_texture_multisample	    TEXTURE_2D_MULTISAMPLE
        //    use ARB_texture_multisample	    PROXY_TEXTURE_2D_MULTISAMPLE
        //    use ARB_texture_multisample	    TEXTURE_2D_MULTISAMPLE_ARRAY
        //    use ARB_texture_multisample	    PROXY_TEXTURE_2D_MULTISAMPLE_ARRAY
        //    use ARB_texture_multisample	    TEXTURE_BINDING_2D_MULTISAMPLE
        //    use ARB_texture_multisample	    TEXTURE_BINDING_2D_MULTISAMPLE_ARRAY
        //    use ARB_texture_multisample	    TEXTURE_SAMPLES
        //    use ARB_texture_multisample	    TEXTURE_FIXED_SAMPLE_LOCATIONS
        //    use ARB_texture_multisample	    SAMPLER_2D_MULTISAMPLE
        //    use ARB_texture_multisample	    INT_SAMPLER_2D_MULTISAMPLE
        //    use ARB_texture_multisample	    UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE
        //    use ARB_texture_multisample	    SAMPLER_2D_MULTISAMPLE_ARRAY
        //    use ARB_texture_multisample	    INT_SAMPLER_2D_MULTISAMPLE_ARRAY
        //    use ARB_texture_multisample	    UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE_ARRAY
        //    use ARB_texture_multisample	    MAX_COLOR_TEXTURE_SAMPLES
        //    use ARB_texture_multisample	    MAX_DEPTH_TEXTURE_SAMPLES
        //    use ARB_texture_multisample	    MAX_INTEGER_SAMPLES
        /* Don't need to reuse tokens from ARB_vertex_array_bgra since they're already in 1.2 core */


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 3.3 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_3_3
        public const uint VERTEX_ATTRIB_ARRAY_DIVISOR = 0x88FE;    // ARB_instanced_arrays
        /* Reuse tokens from ARB_blend_func_extended */

        //    use ARB_blend_func_extended	    SRC1_COLOR
        //    use ARB_blend_func_extended	    ONE_MINUS_SRC1_COLOR
        //    use ARB_blend_func_extended	    ONE_MINUS_SRC1_ALPHA
        //    use ARB_blend_func_extended	    MAX_DUAL_SOURCE_DRAW_BUFFERS
        /* Reuse tokens from ARB_explicit_attrib_location (none) */

        /* Reuse tokens from ARB_occlusion_query2 */

        //    use ARB_occlusion_query2	    ANY_SAMPLES_PASSED
        /* Reuse tokens from ARB_sampler_objects */

        //    use ARB_sampler_objects		    SAMPLER_BINDING
        /* Reuse tokens from ARB_shader_bit_encoding (none) */

        /* Reuse tokens from ARB_texture_rgb10_a2ui */

        //    use ARB_texture_rgb10_a2ui	    RGB10_A2UI
        /* Reuse tokens from ARB_texture_swizzle */

        //    use ARB_texture_swizzle		    TEXTURE_SWIZZLE_R
        //    use ARB_texture_swizzle		    TEXTURE_SWIZZLE_G
        //    use ARB_texture_swizzle		    TEXTURE_SWIZZLE_B
        //    use ARB_texture_swizzle		    TEXTURE_SWIZZLE_A
        //    use ARB_texture_swizzle		    TEXTURE_SWIZZLE_RGBA
        /* Reuse tokens from ARB_timer_query */

        //    use ARB_timer_query		    TIME_ELAPSED
        //    use ARB_timer_query		    TIMESTAMP
        /* Reuse tokens from ARB_vertex_type_2_10_10_10_rev */

        //    use ARB_vertex_type_2_10_10_10_rev  INT_2_10_10_10_REV

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 4.0 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_4_0
        public const uint SAMPLE_SHADING = 0x8C36;    // ARB_sample_shading
        public const uint MIN_SAMPLE_SHADING_VALUE = 0x8C37;    // ARB_sample_shading
        public const uint MIN_PROGRAM_TEXTURE_GATHER_OFFSET = 0x8E5E;    // ARB_texture_gather
        public const uint MAX_PROGRAM_TEXTURE_GATHER_OFFSET = 0x8E5F;    // ARB_texture_gather
        public const uint TEXTURE_CUBE_MAP_ARRAY = 0x9009;    // ARB_texture_cube_map_array
        public const uint TEXTURE_BINDING_CUBE_MAP_ARRAY = 0x900A;    // ARB_texture_cube_map_array
        public const uint PROXY_TEXTURE_CUBE_MAP_ARRAY = 0x900B;    // ARB_texture_cube_map_array
        public const uint SAMPLER_CUBE_MAP_ARRAY = 0x900C;    // ARB_texture_cube_map_array
        public const uint SAMPLER_CUBE_MAP_ARRAY_SHADOW = 0x900D;    // ARB_texture_cube_map_array
        public const uint INT_SAMPLER_CUBE_MAP_ARRAY = 0x900E;    // ARB_texture_cube_map_array
        public const uint UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY = 0x900F;    // ARB_texture_cube_map_array
        /* Reuse tokens from ARB_texture_query_lod (none) */

        /* Reuse tokens from ARB_draw_buffers_blend (none) */

        /* Reuse tokens from ARB_draw_indirect */

        //    use ARB_draw_indirect		    DRAW_INDIRECT_BUFFER
        //    use ARB_draw_indirect		    DRAW_INDIRECT_BUFFER_BINDING
        /* Reuse tokens from ARB_gpu_shader5 */

        //    use ARB_gpu_shader5		    GEOMETRY_SHADER_INVOCATIONS
        //    use ARB_gpu_shader5		    MAX_GEOMETRY_SHADER_INVOCATIONS
        //    use ARB_gpu_shader5		    MIN_FRAGMENT_INTERPOLATION_OFFSET
        //    use ARB_gpu_shader5		    MAX_FRAGMENT_INTERPOLATION_OFFSET
        //    use ARB_gpu_shader5		    FRAGMENT_INTERPOLATION_OFFSET_BITS
        /* Reuse tokens from ARB_gpu_shader_fp64 */

        //    use ARB_gpu_shader_fp64		    DOUBLE_VEC2
        //    use ARB_gpu_shader_fp64		    DOUBLE_VEC3
        //    use ARB_gpu_shader_fp64		    DOUBLE_VEC4
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT2
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT3
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT4
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT2x3
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT2x4
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT3x2
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT3x4
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT4x2
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT4x3
        /* Reuse tokens from ARB_shader_subroutine */

        //    use ARB_shader_subroutine	    ACTIVE_SUBROUTINES
        //    use ARB_shader_subroutine	    ACTIVE_SUBROUTINE_UNIFORMS
        //    use ARB_shader_subroutine	    ACTIVE_SUBROUTINE_UNIFORM_LOCATIONS
        //    use ARB_shader_subroutine	    ACTIVE_SUBROUTINE_MAX_LENGTH
        //    use ARB_shader_subroutine	    ACTIVE_SUBROUTINE_UNIFORM_MAX_LENGTH
        //    use ARB_shader_subroutine	    MAX_SUBROUTINES
        //    use ARB_shader_subroutine	    MAX_SUBROUTINE_UNIFORM_LOCATIONS
        //    use ARB_shader_subroutine	    NUM_COMPATIBLE_SUBROUTINES
        //    use ARB_shader_subroutine	    COMPATIBLE_SUBROUTINES
        /* Reuse tokens from ARB_tessellation_shader */

        //    use ARB_tessellation_shader	    PATCHES
        //    use ARB_tessellation_shader	    PATCH_VERTICES
        //    use ARB_tessellation_shader	    PATCH_DEFAULT_INNER_LEVEL
        //    use ARB_tessellation_shader	    PATCH_DEFAULT_OUTER_LEVEL
        //    use ARB_tessellation_shader	    TESS_CONTROL_OUTPUT_VERTICES
        //    use ARB_tessellation_shader	    TESS_GEN_MODE
        //    use ARB_tessellation_shader	    TESS_GEN_SPACING
        //    use ARB_tessellation_shader	    TESS_GEN_VERTEX_ORDER
        //    use ARB_tessellation_shader	    TESS_GEN_POINT_MODE
        //    use ARB_tessellation_shader	    ISOLINES
        //    use ARB_tessellation_shader	    FRACTIONAL_ODD
        //    use ARB_tessellation_shader	    FRACTIONAL_EVEN
        //    use ARB_tessellation_shader	    MAX_PATCH_VERTICES
        //    use ARB_tessellation_shader	    MAX_TESS_GEN_LEVEL
        //    use ARB_tessellation_shader	    MAX_TESS_CONTROL_UNIFORM_COMPONENTS
        //    use ARB_tessellation_shader	    MAX_TESS_EVALUATION_UNIFORM_COMPONENTS
        //    use ARB_tessellation_shader	    MAX_TESS_CONTROL_TEXTURE_IMAGE_UNITS
        //    use ARB_tessellation_shader	    MAX_TESS_EVALUATION_TEXTURE_IMAGE_UNITS
        //    use ARB_tessellation_shader	    MAX_TESS_CONTROL_OUTPUT_COMPONENTS
        //    use ARB_tessellation_shader	    MAX_TESS_PATCH_COMPONENTS
        //    use ARB_tessellation_shader	    MAX_TESS_CONTROL_TOTAL_OUTPUT_COMPONENTS
        //    use ARB_tessellation_shader	    MAX_TESS_EVALUATION_OUTPUT_COMPONENTS
        //    use ARB_tessellation_shader	    MAX_TESS_CONTROL_UNIFORM_BLOCKS
        //    use ARB_tessellation_shader	    MAX_TESS_EVALUATION_UNIFORM_BLOCKS
        //    use ARB_tessellation_shader	    MAX_TESS_CONTROL_INPUT_COMPONENTS
        //    use ARB_tessellation_shader	    MAX_TESS_EVALUATION_INPUT_COMPONENTS
        //    use ARB_tessellation_shader	    MAX_COMBINED_TESS_CONTROL_UNIFORM_COMPONENTS
        //    use ARB_tessellation_shader	    MAX_COMBINED_TESS_EVALUATION_UNIFORM_COMPONENTS
        //    use ARB_tessellation_shader	    UNIFORM_BLOCK_REFERENCED_BY_TESS_CONTROL_SHADER
        //    use ARB_tessellation_shader	    UNIFORM_BLOCK_REFERENCED_BY_TESS_EVALUATION_SHADER
        //    use ARB_tessellation_shader	    TESS_EVALUATION_SHADER
        //    use ARB_tessellation_shader	    TESS_CONTROL_SHADER
        /* Reuse tokens from ARB_texture_buffer_object_rgb32 (none) */

        /* Reuse tokens from ARB_transform_feedback2 */

        //    use ARB_tessellation_shader	    TRANSFORM_FEEDBACK
        //    use ARB_tessellation_shader	    TRANSFORM_FEEDBACK_BUFFER_PAUSED
        //    use ARB_tessellation_shader	    TRANSFORM_FEEDBACK_BUFFER_ACTIVE
        //    use ARB_tessellation_shader	    TRANSFORM_FEEDBACK_BINDING
        /* Reuse tokens from ARB_transform_feedback3 */

        //    use ARB_tessellation_shader	    MAX_TRANSFORM_FEEDBACK_BUFFERS
        //    use ARB_tessellation_shader	    MAX_VERTEX_STREAMS

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 4.1 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_4_1
        /* Reuse tokens from ARB_ES2_compatibility */

        //    use ARB_ES2_compatibility	    FIXED
        //    use ARB_ES2_compatibility	    IMPLEMENTATION_COLOR_READ_TYPE
        //    use ARB_ES2_compatibility	    IMPLEMENTATION_COLOR_READ_FORMAT
        //    use ARB_ES2_compatibility	    LOW_FLOAT
        //    use ARB_ES2_compatibility	    MEDIUM_FLOAT
        //    use ARB_ES2_compatibility	    HIGH_FLOAT
        //    use ARB_ES2_compatibility	    LOW_INT
        //    use ARB_ES2_compatibility	    MEDIUM_INT
        //    use ARB_ES2_compatibility	    HIGH_INT
        //    use ARB_ES2_compatibility	    SHADER_COMPILER
        //    use ARB_ES2_compatibility	    SHADER_BINARY_FORMATS
        //    use ARB_ES2_compatibility	    NUM_SHADER_BINARY_FORMATS
        //    use ARB_ES2_compatibility	    MAX_VERTEX_UNIFORM_VECTORS
        //    use ARB_ES2_compatibility	    MAX_VARYING_VECTORS
        //    use ARB_ES2_compatibility	    MAX_FRAGMENT_UNIFORM_VECTORS
        //    use ARB_ES2_compatibility	    RGB565
        /* Reuse tokens from ARB_get_program_binary */

        //    use ARB_get_program_binary	    PROGRAM_BINARY_RETRIEVABLE_HINT
        //    use ARB_get_program_binary	    PROGRAM_BINARY_LENGTH
        //    use ARB_get_program_binary	    NUM_PROGRAM_BINARY_FORMATS
        //    use ARB_get_program_binary	    PROGRAM_BINARY_FORMATS
        /* Reuse tokens from ARB_separate_shader_objects */

        //    use ARB_separate_shader_objects     VERTEX_SHADER_BIT
        //    use ARB_separate_shader_objects     FRAGMENT_SHADER_BIT
        //    use ARB_separate_shader_objects     GEOMETRY_SHADER_BIT
        //    use ARB_separate_shader_objects     TESS_CONTROL_SHADER_BIT
        //    use ARB_separate_shader_objects     TESS_EVALUATION_SHADER_BIT
        //    use ARB_separate_shader_objects     ALL_SHADER_BITS
        //    use ARB_separate_shader_objects     PROGRAM_SEPARABLE
        //    use ARB_separate_shader_objects     ACTIVE_PROGRAM
        //    use ARB_separate_shader_objects     PROGRAM_PIPELINE_BINDING
        /* Reuse tokens from ARB_shader_precision (none) */

        /* Reuse tokens from ARB_vertex_attrib_64bit - all are in GL 3.0 and 4.0 already */

        /* Reuse tokens from ARB_viewport_array - some are in GL 1.1 and ARB_provoking_vertex already */

        //    use ARB_viewport_array		    MAX_VIEWPORTS
        //    use ARB_viewport_array		    VIEWPORT_SUBPIXEL_BITS
        //    use ARB_viewport_array		    VIEWPORT_BOUNDS_RANGE
        //    use ARB_viewport_array		    LAYER_PROVOKING_VERTEX
        //    use ARB_viewport_array		    VIEWPORT_INDEX_PROVOKING_VERTEX
        //    use ARB_viewport_array		    UNDEFINED_VERTEX

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 4.2 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_4_2
        /* Reuse tokens from ARB_base_instance (none) */

        /* Reuse tokens from ARB_shading_language_420pack (none) */

        /* Reuse tokens from ARB_transform_feedback_instanced (none) */

        /* Reuse tokens from ARB_compressed_texture_pixel_storage */

        //    use ARB_compressed_texture_pixel_storage    UNPACK_COMPRESSED_BLOCK_WIDTH
        //    use ARB_compressed_texture_pixel_storage    UNPACK_COMPRESSED_BLOCK_HEIGHT
        //    use ARB_compressed_texture_pixel_storage    UNPACK_COMPRESSED_BLOCK_DEPTH
        //    use ARB_compressed_texture_pixel_storage    UNPACK_COMPRESSED_BLOCK_SIZE
        //    use ARB_compressed_texture_pixel_storage    PACK_COMPRESSED_BLOCK_WIDTH
        //    use ARB_compressed_texture_pixel_storage    PACK_COMPRESSED_BLOCK_HEIGHT
        //    use ARB_compressed_texture_pixel_storage    PACK_COMPRESSED_BLOCK_DEPTH
        //    use ARB_compressed_texture_pixel_storage    PACK_COMPRESSED_BLOCK_SIZE
        /* Reuse tokens from ARB_conservative_depth (none) */

        /* Reuse tokens from ARB_internalformat_query */

        //    use ARB_internalformat_query	    NUM_SAMPLE_COUNTS
        /* Reuse tokens from ARB_map_buffer_alignment */

        //    use ARB_map_buffer_alignment	    MIN_MAP_BUFFER_ALIGNMENT
        /* Reuse tokens from ARB_shader_atomic_counters */

        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER
        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER_BINDING
        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER_START
        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER_SIZE
        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER_DATA_SIZE
        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER_ACTIVE_ATOMIC_COUNTERS
        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER_ACTIVE_ATOMIC_COUNTER_INDICES
        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER_REFERENCED_BY_VERTEX_SHADER
        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER_REFERENCED_BY_TESS_CONTROL_SHADER
        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER_REFERENCED_BY_TESS_EVALUATION_SHADER
        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER_REFERENCED_BY_GEOMETRY_SHADER
        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER_REFERENCED_BY_FRAGMENT_SHADER
        //    use ARB_shader_atomic_counters	    MAX_VERTEX_ATOMIC_COUNTER_BUFFERS
        //    use ARB_shader_atomic_counters	    MAX_TESS_CONTROL_ATOMIC_COUNTER_BUFFERS
        //    use ARB_shader_atomic_counters	    MAX_TESS_EVALUATION_ATOMIC_COUNTER_BUFFERS
        //    use ARB_shader_atomic_counters	    MAX_GEOMETRY_ATOMIC_COUNTER_BUFFERS
        //    use ARB_shader_atomic_counters	    MAX_FRAGMENT_ATOMIC_COUNTER_BUFFERS
        //    use ARB_shader_atomic_counters	    MAX_COMBINED_ATOMIC_COUNTER_BUFFERS
        //    use ARB_shader_atomic_counters	    MAX_VERTEX_ATOMIC_COUNTERS
        //    use ARB_shader_atomic_counters	    MAX_TESS_CONTROL_ATOMIC_COUNTERS
        //    use ARB_shader_atomic_counters	    MAX_TESS_EVALUATION_ATOMIC_COUNTERS
        //    use ARB_shader_atomic_counters	    MAX_GEOMETRY_ATOMIC_COUNTERS
        //    use ARB_shader_atomic_counters	    MAX_FRAGMENT_ATOMIC_COUNTERS
        //    use ARB_shader_atomic_counters	    MAX_COMBINED_ATOMIC_COUNTERS
        //    use ARB_shader_atomic_counters	    MAX_ATOMIC_COUNTER_BUFFER_SIZE
        //    use ARB_shader_atomic_counters	    MAX_ATOMIC_COUNTER_BUFFER_BINDINGS
        //    use ARB_shader_atomic_counters	    ACTIVE_ATOMIC_COUNTER_BUFFERS
        //    use ARB_shader_atomic_counters	    UNIFORM_ATOMIC_COUNTER_BUFFER_INDEX
        //    use ARB_shader_atomic_counters	    UNSIGNED_INT_ATOMIC_COUNTER
        /* Reuse tokens from ARB_shader_image_load_store */

        //    use ARB_shader_image_load_store     VERTEX_ATTRIB_ARRAY_BARRIER_BIT
        //    use ARB_shader_image_load_store     ELEMENT_ARRAY_BARRIER_BIT
        //    use ARB_shader_image_load_store     UNIFORM_BARRIER_BIT
        //    use ARB_shader_image_load_store     TEXTURE_FETCH_BARRIER_BIT
        //    use ARB_shader_image_load_store     SHADER_IMAGE_ACCESS_BARRIER_BIT
        //    use ARB_shader_image_load_store     COMMAND_BARRIER_BIT
        //    use ARB_shader_image_load_store     PIXEL_BUFFER_BARRIER_BIT
        //    use ARB_shader_image_load_store     TEXTURE_UPDATE_BARRIER_BIT
        //    use ARB_shader_image_load_store     BUFFER_UPDATE_BARRIER_BIT
        //    use ARB_shader_image_load_store     FRAMEBUFFER_BARRIER_BIT
        //    use ARB_shader_image_load_store     TRANSFORM_FEEDBACK_BARRIER_BIT
        //    use ARB_shader_image_load_store     ATOMIC_COUNTER_BARRIER_BIT
        //    use ARB_shader_image_load_store     ALL_BARRIER_BITS
        //    use ARB_shader_image_load_store     MAX_IMAGE_UNITS
        //    use ARB_shader_image_load_store     MAX_COMBINED_IMAGE_UNITS_AND_FRAGMENT_OUTPUTS
        //    use ARB_shader_image_load_store     IMAGE_BINDING_NAME
        //    use ARB_shader_image_load_store     IMAGE_BINDING_LEVEL
        //    use ARB_shader_image_load_store     IMAGE_BINDING_LAYERED
        //    use ARB_shader_image_load_store     IMAGE_BINDING_LAYER
        //    use ARB_shader_image_load_store     IMAGE_BINDING_ACCESS
        //    use ARB_shader_image_load_store     IMAGE_1D
        //    use ARB_shader_image_load_store     IMAGE_2D
        //    use ARB_shader_image_load_store     IMAGE_3D
        //    use ARB_shader_image_load_store     IMAGE_2D_RECT
        //    use ARB_shader_image_load_store     IMAGE_CUBE
        //    use ARB_shader_image_load_store     IMAGE_BUFFER
        //    use ARB_shader_image_load_store     IMAGE_1D_ARRAY
        //    use ARB_shader_image_load_store     IMAGE_2D_ARRAY
        //    use ARB_shader_image_load_store     IMAGE_CUBE_MAP_ARRAY
        //    use ARB_shader_image_load_store     IMAGE_2D_MULTISAMPLE
        //    use ARB_shader_image_load_store     IMAGE_2D_MULTISAMPLE_ARRAY
        //    use ARB_shader_image_load_store     INT_IMAGE_1D
        //    use ARB_shader_image_load_store     INT_IMAGE_2D
        //    use ARB_shader_image_load_store     INT_IMAGE_3D
        //    use ARB_shader_image_load_store     INT_IMAGE_2D_RECT
        //    use ARB_shader_image_load_store     INT_IMAGE_CUBE
        //    use ARB_shader_image_load_store     INT_IMAGE_BUFFER
        //    use ARB_shader_image_load_store     INT_IMAGE_1D_ARRAY
        //    use ARB_shader_image_load_store     INT_IMAGE_2D_ARRAY
        //    use ARB_shader_image_load_store     INT_IMAGE_CUBE_MAP_ARRAY
        //    use ARB_shader_image_load_store     INT_IMAGE_2D_MULTISAMPLE
        //    use ARB_shader_image_load_store     INT_IMAGE_2D_MULTISAMPLE_ARRAY
        //    use ARB_shader_image_load_store     UNSIGNED_INT_IMAGE_1D
        //    use ARB_shader_image_load_store     UNSIGNED_INT_IMAGE_2D
        //    use ARB_shader_image_load_store     UNSIGNED_INT_IMAGE_3D
        //    use ARB_shader_image_load_store     UNSIGNED_INT_IMAGE_2D_RECT
        //    use ARB_shader_image_load_store     UNSIGNED_INT_IMAGE_CUBE
        //    use ARB_shader_image_load_store     UNSIGNED_INT_IMAGE_BUFFER
        //    use ARB_shader_image_load_store     UNSIGNED_INT_IMAGE_1D_ARRAY
        //    use ARB_shader_image_load_store     UNSIGNED_INT_IMAGE_2D_ARRAY
        //    use ARB_shader_image_load_store     UNSIGNED_INT_IMAGE_CUBE_MAP_ARRAY
        //    use ARB_shader_image_load_store     UNSIGNED_INT_IMAGE_2D_MULTISAMPLE
        //    use ARB_shader_image_load_store     UNSIGNED_INT_IMAGE_2D_MULTISAMPLE_ARRAY
        //    use ARB_shader_image_load_store     MAX_IMAGE_SAMPLES
        //    use ARB_shader_image_load_store     IMAGE_BINDING_FORMAT
        //    use ARB_shader_image_load_store     IMAGE_FORMAT_COMPATIBILITY_TYPE
        //    use ARB_shader_image_load_store     IMAGE_FORMAT_COMPATIBILITY_BY_SIZE
        //    use ARB_shader_image_load_store     IMAGE_FORMAT_COMPATIBILITY_BY_CLASS
        //    use ARB_shader_image_load_store     MAX_VERTEX_IMAGE_UNIFORMS
        //    use ARB_shader_image_load_store     MAX_TESS_CONTROL_IMAGE_UNIFORMS
        //    use ARB_shader_image_load_store     MAX_TESS_EVALUATION_IMAGE_UNIFORMS
        //    use ARB_shader_image_load_store     MAX_GEOMETRY_IMAGE_UNIFORMS
        //    use ARB_shader_image_load_store     MAX_FRAGMENT_IMAGE_UNIFORMS
        //    use ARB_shader_image_load_store     MAX_COMBINED_IMAGE_UNIFORMS
        /* Reuse tokens from ARB_shading_language_packing (none) */

        /* Reuse tokens from ARB_texture_storage */

        //    use ARB_texture_storage		    TEXTURE_IMMUTABLE_FORMAT

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// OpenGL 4.3 enums
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //VERSION_4_3
        public const uint NUM_SHADING_LANGUAGE_VERSIONS = 0x82E9;
        public const uint VERTEX_ATTRIB_ARRAY_LONG = 0x874E;
        /* Reuse tokens from ARB_arrays_of_arrays (none, GLSL only) */

        /* Reuse tokens from ARB_fragment_layer_viewport (none, GLSL only) */

        /* Reuse tokens from ARB_shader_image_size (none, GLSL only) */

        /* Reuse tokens from ARB_ES3_compatibility */

        //    use ARB_ES3_compatibility		    COMPRESSED_RGB8_ETC2
        //    use ARB_ES3_compatibility		    COMPRESSED_SRGB8_ETC2
        //    use ARB_ES3_compatibility		    COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2
        //    use ARB_ES3_compatibility		    COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2
        //    use ARB_ES3_compatibility		    COMPRESSED_RGBA8_ETC2_EAC
        //    use ARB_ES3_compatibility		    COMPRESSED_SRGB8_ALPHA8_ETC2_EAC
        //    use ARB_ES3_compatibility		    COMPRESSED_R11_EAC
        //    use ARB_ES3_compatibility		    COMPRESSED_SIGNED_R11_EAC
        //    use ARB_ES3_compatibility		    COMPRESSED_RG11_EAC
        //    use ARB_ES3_compatibility		    COMPRESSED_SIGNED_RG11_EAC
        //    use ARB_ES3_compatibility		    PRIMITIVE_RESTART_FIXED_INDEX
        //    use ARB_ES3_compatibility		    ANY_SAMPLES_PASSED_CONSERVATIVE
        //    use ARB_ES3_compatibility		    MAX_ELEMENT_INDEX
        /* Reuse tokens from ARB_clear_buffer_object (none) */

        /* Reuse tokens from ARB_compute_shader */

        //    use ARB_compute_shader			    COMPUTE_SHADER
        //    use ARB_compute_shader			    MAX_COMPUTE_UNIFORM_BLOCKS
        //    use ARB_compute_shader			    MAX_COMPUTE_TEXTURE_IMAGE_UNITS
        //    use ARB_compute_shader			    MAX_COMPUTE_IMAGE_UNIFORMS
        //    use ARB_compute_shader			    MAX_COMPUTE_SHARED_MEMORY_SIZE
        //    use ARB_compute_shader			    MAX_COMPUTE_UNIFORM_COMPONENTS
        //    use ARB_compute_shader			    MAX_COMPUTE_ATOMIC_COUNTER_BUFFERS
        //    use ARB_compute_shader			    MAX_COMPUTE_ATOMIC_COUNTERS
        //    use ARB_compute_shader			    MAX_COMBINED_COMPUTE_UNIFORM_COMPONENTS
        //    use ARB_compute_shader			    MAX_COMPUTE_LOCAL_INVOCATIONS
        //    use ARB_compute_shader			    MAX_COMPUTE_WORK_GROUP_COUNT
        //    use ARB_compute_shader			    MAX_COMPUTE_WORK_GROUP_SIZE
        //    use ARB_compute_shader			    COMPUTE_LOCAL_WORK_SIZE
        //    use ARB_compute_shader			    UNIFORM_BLOCK_REFERENCED_BY_COMPUTE_SHADER
        //    use ARB_compute_shader			    ATOMIC_COUNTER_BUFFER_REFERENCED_BY_COMPUTE_SHADER
        //    use ARB_compute_shader			    DISPATCH_INDIRECT_BUFFER
        //    use ARB_compute_shader			    DISPATCH_INDIRECT_BUFFER_BINDING
        /* Reuse tokens from ARB_copy_image (none) */

        /* Reuse tokens from KHR_debug */

        //    use KHR_debug				    DEBUG_OUTPUT_SYNCHRONOUS
        //    use KHR_debug				    DEBUG_NEXT_LOGGED_MESSAGE_LENGTH
        //    use KHR_debug				    DEBUG_CALLBACK_FUNCTION
        //    use KHR_debug				    DEBUG_CALLBACK_USER_PARAM
        //    use KHR_debug				    DEBUG_SOURCE_API
        //    use KHR_debug				    DEBUG_SOURCE_WINDOW_SYSTEM
        //    use KHR_debug				    DEBUG_SOURCE_SHADER_COMPILER
        //    use KHR_debug				    DEBUG_SOURCE_THIRD_PARTY
        //    use KHR_debug				    DEBUG_SOURCE_APPLICATION
        //    use KHR_debug				    DEBUG_SOURCE_OTHER
        //    use KHR_debug				    DEBUG_TYPE_ERROR
        //    use KHR_debug				    DEBUG_TYPE_DEPRECATED_BEHAVIOR
        //    use KHR_debug				    DEBUG_TYPE_UNDEFINED_BEHAVIOR
        //    use KHR_debug				    DEBUG_TYPE_PORTABILITY
        //    use KHR_debug				    DEBUG_TYPE_PERFORMANCE
        //    use KHR_debug				    DEBUG_TYPE_OTHER
        //    use KHR_debug				    MAX_DEBUG_MESSAGE_LENGTH
        //    use KHR_debug				    MAX_DEBUG_LOGGED_MESSAGES
        //    use KHR_debug				    DEBUG_LOGGED_MESSAGES
        //    use KHR_debug				    DEBUG_SEVERITY_HIGH
        //    use KHR_debug				    DEBUG_SEVERITY_MEDIUM
        //    use KHR_debug				    DEBUG_SEVERITY_LOW
        //    use KHR_debug				    DEBUG_TYPE_MARKER
        //    use KHR_debug				    DEBUG_TYPE_PUSH_GROUP
        //    use KHR_debug				    DEBUG_TYPE_POP_GROUP
        //    use KHR_debug				    DEBUG_SEVERITY_NOTIFICATION
        //    use KHR_debug				    MAX_DEBUG_GROUP_STACK_DEPTH
        //    use KHR_debug				    DEBUG_GROUP_STACK_DEPTH
        //    use KHR_debug				    BUFFER
        //    use KHR_debug				    SHADER
        //    use KHR_debug				    PROGRAM
        //    use KHR_debug				    QUERY
        //    use KHR_debug				    PROGRAM_PIPELINE
        //    use KHR_debug				    SAMPLER
        //    use KHR_debug				    DISPLAY_LIST
        //    use KHR_debug				    MAX_LABEL_LENGTH
        //    use KHR_debug				    DEBUG_OUTPUT
        //    use KHR_debug				    CONTEXT_FLAG_DEBUG_BIT
        //    use ErrorCode				    STACK_UNDERFLOW
        //    use ErrorCode				    STACK_OVERFLOW
        /* Reuse tokens from ARB_explicit_uniform_location */

        //    use ARB_explicit_uniform_location	    MAX_UNIFORM_LOCATIONS
        /* Reuse tokens from ARB_framebuffer_no_attachments */

        //    use ARB_framebuffer_no_attachments	    FRAMEBUFFER_DEFAULT_WIDTH
        //    use ARB_framebuffer_no_attachments	    FRAMEBUFFER_DEFAULT_HEIGHT
        //    use ARB_framebuffer_no_attachments	    FRAMEBUFFER_DEFAULT_LAYERS
        //    use ARB_framebuffer_no_attachments	    FRAMEBUFFER_DEFAULT_SAMPLES
        //    use ARB_framebuffer_no_attachments	    FRAMEBUFFER_DEFAULT_FIXED_SAMPLE_LOCATIONS
        //    use ARB_framebuffer_no_attachments	    MAX_FRAMEBUFFER_WIDTH
        //    use ARB_framebuffer_no_attachments	    MAX_FRAMEBUFFER_HEIGHT
        //    use ARB_framebuffer_no_attachments	    MAX_FRAMEBUFFER_LAYERS
        //    use ARB_framebuffer_no_attachments	    MAX_FRAMEBUFFER_SAMPLES
        /* Reuse tokens from ARB_internalformat_query2 */

        //    use ARB_internalformat_query2		    INTERNALFORMAT_SUPPORTED
        //    use ARB_internalformat_query2		    INTERNALFORMAT_PREFERRED
        //    use ARB_internalformat_query2		    INTERNALFORMAT_RED_SIZE
        //    use ARB_internalformat_query2		    INTERNALFORMAT_GREEN_SIZE
        //    use ARB_internalformat_query2		    INTERNALFORMAT_BLUE_SIZE
        //    use ARB_internalformat_query2		    INTERNALFORMAT_ALPHA_SIZE
        //    use ARB_internalformat_query2		    INTERNALFORMAT_DEPTH_SIZE
        //    use ARB_internalformat_query2		    INTERNALFORMAT_STENCIL_SIZE
        //    use ARB_internalformat_query2		    INTERNALFORMAT_SHARED_SIZE
        //    use ARB_internalformat_query2		    INTERNALFORMAT_RED_TYPE
        //    use ARB_internalformat_query2		    INTERNALFORMAT_GREEN_TYPE
        //    use ARB_internalformat_query2		    INTERNALFORMAT_BLUE_TYPE
        //    use ARB_internalformat_query2		    INTERNALFORMAT_ALPHA_TYPE
        //    use ARB_internalformat_query2		    INTERNALFORMAT_DEPTH_TYPE
        //    use ARB_internalformat_query2		    INTERNALFORMAT_STENCIL_TYPE
        //    use ARB_internalformat_query2		    MAX_WIDTH
        //    use ARB_internalformat_query2		    MAX_HEIGHT
        //    use ARB_internalformat_query2		    MAX_DEPTH
        //    use ARB_internalformat_query2		    MAX_LAYERS
        //    use ARB_internalformat_query2		    MAX_COMBINED_DIMENSIONS
        //    use ARB_internalformat_query2		    COLOR_COMPONENTS
        //    use ARB_internalformat_query2		    DEPTH_COMPONENTS
        //    use ARB_internalformat_query2		    STENCIL_COMPONENTS
        //    use ARB_internalformat_query2		    COLOR_RENDERABLE
        //    use ARB_internalformat_query2		    DEPTH_RENDERABLE
        //    use ARB_internalformat_query2		    STENCIL_RENDERABLE
        //    use ARB_internalformat_query2		    FRAMEBUFFER_RENDERABLE
        //    use ARB_internalformat_query2		    FRAMEBUFFER_RENDERABLE_LAYERED
        //    use ARB_internalformat_query2		    FRAMEBUFFER_BLEND
        //    use ARB_internalformat_query2		    READ_PIXELS
        //    use ARB_internalformat_query2		    READ_PIXELS_FORMAT
        //    use ARB_internalformat_query2		    READ_PIXELS_TYPE
        //    use ARB_internalformat_query2		    TEXTURE_IMAGE_FORMAT
        //    use ARB_internalformat_query2		    TEXTURE_IMAGE_TYPE
        //    use ARB_internalformat_query2		    GET_TEXTURE_IMAGE_FORMAT
        //    use ARB_internalformat_query2		    GET_TEXTURE_IMAGE_TYPE
        //    use ARB_internalformat_query2		    MIPMAP
        //    use ARB_internalformat_query2		    MANUAL_GENERATE_MIPMAP
        //    use ARB_internalformat_query2		    AUTO_GENERATE_MIPMAP
        //    use ARB_internalformat_query2		    COLOR_ENCODING
        //    use ARB_internalformat_query2		    SRGB_READ
        //    use ARB_internalformat_query2		    SRGB_WRITE
        //    use ARB_internalformat_query2		    FILTER
        //    use ARB_internalformat_query2		    VERTEX_TEXTURE
        //    use ARB_internalformat_query2		    TESS_CONTROL_TEXTURE
        //    use ARB_internalformat_query2		    TESS_EVALUATION_TEXTURE
        //    use ARB_internalformat_query2		    GEOMETRY_TEXTURE
        //    use ARB_internalformat_query2		    FRAGMENT_TEXTURE
        //    use ARB_internalformat_query2		    COMPUTE_TEXTURE
        //    use ARB_internalformat_query2		    TEXTURE_SHADOW
        //    use ARB_internalformat_query2		    TEXTURE_GATHER
        //    use ARB_internalformat_query2		    TEXTURE_GATHER_SHADOW
        //    use ARB_internalformat_query2		    SHADER_IMAGE_LOAD
        //    use ARB_internalformat_query2		    SHADER_IMAGE_STORE
        //    use ARB_internalformat_query2		    SHADER_IMAGE_ATOMIC
        //    use ARB_internalformat_query2		    IMAGE_TEXEL_SIZE
        //    use ARB_internalformat_query2		    IMAGE_COMPATIBILITY_CLASS
        //    use ARB_internalformat_query2		    IMAGE_PIXEL_FORMAT
        //    use ARB_internalformat_query2		    IMAGE_PIXEL_TYPE
        //    use ARB_internalformat_query2		    SIMULTANEOUS_TEXTURE_AND_DEPTH_TEST
        //    use ARB_internalformat_query2		    SIMULTANEOUS_TEXTURE_AND_STENCIL_TEST
        //    use ARB_internalformat_query2		    SIMULTANEOUS_TEXTURE_AND_DEPTH_WRITE
        //    use ARB_internalformat_query2		    SIMULTANEOUS_TEXTURE_AND_STENCIL_WRITE
        //    use ARB_internalformat_query2		    TEXTURE_COMPRESSED_BLOCK_WIDTH
        //    use ARB_internalformat_query2		    TEXTURE_COMPRESSED_BLOCK_HEIGHT
        //    use ARB_internalformat_query2		    TEXTURE_COMPRESSED_BLOCK_SIZE
        //    use ARB_internalformat_query2		    CLEAR_BUFFER
        //    use ARB_internalformat_query2		    TEXTURE_VIEW
        //    use ARB_internalformat_query2		    VIEW_COMPATIBILITY_CLASS
        //    use ARB_internalformat_query2		    FULL_SUPPORT
        //    use ARB_internalformat_query2		    CAVEAT_SUPPORT
        //    use ARB_internalformat_query2		    IMAGE_CLASS_4_X_32
        //    use ARB_internalformat_query2		    IMAGE_CLASS_2_X_32
        //    use ARB_internalformat_query2		    IMAGE_CLASS_1_X_32
        //    use ARB_internalformat_query2		    IMAGE_CLASS_4_X_16
        //    use ARB_internalformat_query2		    IMAGE_CLASS_2_X_16
        //    use ARB_internalformat_query2		    IMAGE_CLASS_1_X_16
        //    use ARB_internalformat_query2		    IMAGE_CLASS_4_X_8
        //    use ARB_internalformat_query2		    IMAGE_CLASS_2_X_8
        //    use ARB_internalformat_query2		    IMAGE_CLASS_1_X_8
        //    use ARB_internalformat_query2		    IMAGE_CLASS_11_11_10
        //    use ARB_internalformat_query2		    IMAGE_CLASS_10_10_10_2
        //    use ARB_internalformat_query2		    VIEW_CLASS_128_BITS
        //    use ARB_internalformat_query2		    VIEW_CLASS_96_BITS
        //    use ARB_internalformat_query2		    VIEW_CLASS_64_BITS
        //    use ARB_internalformat_query2		    VIEW_CLASS_48_BITS
        //    use ARB_internalformat_query2		    VIEW_CLASS_32_BITS
        //    use ARB_internalformat_query2		    VIEW_CLASS_24_BITS
        //    use ARB_internalformat_query2		    VIEW_CLASS_16_BITS
        //    use ARB_internalformat_query2		    VIEW_CLASS_8_BITS
        //    use ARB_internalformat_query2		    VIEW_CLASS_S3TC_DXT1_RGB
        //    use ARB_internalformat_query2		    VIEW_CLASS_S3TC_DXT1_RGBA
        //    use ARB_internalformat_query2		    VIEW_CLASS_S3TC_DXT3_RGBA
        //    use ARB_internalformat_query2		    VIEW_CLASS_S3TC_DXT5_RGBA
        //    use ARB_internalformat_query2		    VIEW_CLASS_RGTC1_RED
        //    use ARB_internalformat_query2		    VIEW_CLASS_RGTC2_RG
        //    use ARB_internalformat_query2		    VIEW_CLASS_BPTC_UNORM
        //    use ARB_internalformat_query2		    VIEW_CLASS_BPTC_FLOAT
        /* Reuse tokens from ARB_invalidate_subdata (none) */

        /* Reuse tokens from ARB_multi_draw_indirect (none) */

        /* Reuse tokens from ARB_program_interface_query */

        //    use ARB_program_interface_query		    UNIFORM
        //    use ARB_program_interface_query		    UNIFORM_BLOCK
        //    use ARB_program_interface_query		    PROGRAM_INPUT
        //    use ARB_program_interface_query		    PROGRAM_OUTPUT
        //    use ARB_program_interface_query		    BUFFER_VARIABLE
        //    use ARB_program_interface_query		    SHADER_STORAGE_BLOCK
        //    use ARB_program_interface_query		    VERTEX_SUBROUTINE
        //    use ARB_program_interface_query		    TESS_CONTROL_SUBROUTINE
        //    use ARB_program_interface_query		    TESS_EVALUATION_SUBROUTINE
        //    use ARB_program_interface_query		    GEOMETRY_SUBROUTINE
        //    use ARB_program_interface_query		    FRAGMENT_SUBROUTINE
        //    use ARB_program_interface_query		    COMPUTE_SUBROUTINE
        //    use ARB_program_interface_query		    VERTEX_SUBROUTINE_UNIFORM
        //    use ARB_program_interface_query		    TESS_CONTROL_SUBROUTINE_UNIFORM
        //    use ARB_program_interface_query		    TESS_EVALUATION_SUBROUTINE_UNIFORM
        //    use ARB_program_interface_query		    GEOMETRY_SUBROUTINE_UNIFORM
        //    use ARB_program_interface_query		    FRAGMENT_SUBROUTINE_UNIFORM
        //    use ARB_program_interface_query		    COMPUTE_SUBROUTINE_UNIFORM
        //    use ARB_program_interface_query		    TRANSFORM_FEEDBACK_VARYING
        //    use ARB_program_interface_query		    ACTIVE_RESOURCES
        //    use ARB_program_interface_query		    MAX_NAME_LENGTH
        //    use ARB_program_interface_query		    MAX_NUM_ACTIVE_VARIABLES
        //    use ARB_program_interface_query		    MAX_NUM_COMPATIBLE_SUBROUTINES
        //    use ARB_program_interface_query		    NAME_LENGTH
        //    use ARB_program_interface_query		    TYPE
        //    use ARB_program_interface_query		    ARRAY_SIZE
        //    use ARB_program_interface_query		    OFFSET
        //    use ARB_program_interface_query		    BLOCK_INDEX
        //    use ARB_program_interface_query		    ARRAY_STRIDE
        //    use ARB_program_interface_query		    MATRIX_STRIDE
        //    use ARB_program_interface_query		    IS_ROW_MAJOR
        //    use ARB_program_interface_query		    ATOMIC_COUNTER_BUFFER_INDEX
        //    use ARB_program_interface_query		    BUFFER_BINDING
        //    use ARB_program_interface_query		    BUFFER_DATA_SIZE
        //    use ARB_program_interface_query		    NUM_ACTIVE_VARIABLES
        //    use ARB_program_interface_query		    ACTIVE_VARIABLES
        //    use ARB_program_interface_query		    REFERENCED_BY_VERTEX_SHADER
        //    use ARB_program_interface_query		    REFERENCED_BY_TESS_CONTROL_SHADER
        //    use ARB_program_interface_query		    REFERENCED_BY_TESS_EVALUATION_SHADER
        //    use ARB_program_interface_query		    REFERENCED_BY_GEOMETRY_SHADER
        //    use ARB_program_interface_query		    REFERENCED_BY_FRAGMENT_SHADER
        //    use ARB_program_interface_query		    REFERENCED_BY_COMPUTE_SHADER
        //    use ARB_program_interface_query		    TOP_LEVEL_ARRAY_SIZE
        //    use ARB_program_interface_query		    TOP_LEVEL_ARRAY_STRIDE
        //    use ARB_program_interface_query		    LOCATION
        //    use ARB_program_interface_query		    LOCATION_INDEX
        //    use ARB_program_interface_query		    IS_PER_PATCH
        /* Reuse tokens from ARB_robust_buffer_access_behavior (none) */

        /* Reuse tokens from ARB_shader_storage_buffer_object */

        //    use ARB_shader_storage_buffer_object	    SHADER_STORAGE_BUFFER
        //    use ARB_shader_storage_buffer_object	    SHADER_STORAGE_BUFFER_BINDING
        //    use ARB_shader_storage_buffer_object	    SHADER_STORAGE_BUFFER_START
        //    use ARB_shader_storage_buffer_object	    SHADER_STORAGE_BUFFER_SIZE
        //    use ARB_shader_storage_buffer_object	    MAX_VERTEX_SHADER_STORAGE_BLOCKS
        //    use ARB_shader_storage_buffer_object	    MAX_GEOMETRY_SHADER_STORAGE_BLOCKS
        //    use ARB_shader_storage_buffer_object	    MAX_TESS_CONTROL_SHADER_STORAGE_BLOCKS
        //    use ARB_shader_storage_buffer_object	    MAX_TESS_EVALUATION_SHADER_STORAGE_BLOCKS
        //    use ARB_shader_storage_buffer_object	    MAX_FRAGMENT_SHADER_STORAGE_BLOCKS
        //    use ARB_shader_storage_buffer_object	    MAX_COMPUTE_SHADER_STORAGE_BLOCKS
        //    use ARB_shader_storage_buffer_object	    MAX_COMBINED_SHADER_STORAGE_BLOCKS
        //    use ARB_shader_storage_buffer_object	    MAX_SHADER_STORAGE_BUFFER_BINDINGS
        //    use ARB_shader_storage_buffer_object	    MAX_SHADER_STORAGE_BLOCK_SIZE
        //    use ARB_shader_storage_buffer_object	    SHADER_STORAGE_BUFFER_OFFSET_ALIGNMENT
        //    use ARB_shader_storage_buffer_object	    SHADER_STORAGE_BARRIER_BIT
        //    use ARB_shader_storage_buffer_object	    MAX_COMBINED_SHADER_OUTPUT_RESOURCES
        /* Reuse tokens from ARB_stencil_texturing */

        //    use ARB_stencil_texturing		    DEPTH_STENCIL_TEXTURE_MODE
        /* Reuse tokens from ARB_texture_buffer_range */

        //    use ARB_texture_buffer_range		    TEXTURE_BUFFER_OFFSET
        //    use ARB_texture_buffer_range		    TEXTURE_BUFFER_SIZE
        //    use ARB_texture_buffer_range		    TEXTURE_BUFFER_OFFSET_ALIGNMENT
        /* Reuse tokens from ARB_texture_query_levels (none) */

        /* Reuse tokens from ARB_texture_storage_multisample (none) */

        /* Reuse tokens from ARB_texture_view */

        //    use ARB_texture_view			    TEXTURE_VIEW_MIN_LEVEL
        //    use ARB_texture_view			    TEXTURE_VIEW_NUM_LEVELS
        //    use ARB_texture_view			    TEXTURE_VIEW_MIN_LAYER
        //    use ARB_texture_view			    TEXTURE_VIEW_NUM_LAYERS
        //    use ARB_texture_view			    TEXTURE_IMMUTABLE_LEVELS
        /* Reuse tokens from ARB_vertex_attrib_binding */

        //    use ARB_vertex_attrib_binding		    VERTEX_ATTRIB_BINDING
        //    use ARB_vertex_attrib_binding		    VERTEX_ATTRIB_RELATIVE_OFFSET
        //    use ARB_vertex_attrib_binding		    VERTEX_BINDING_DIVISOR
        //    use ARB_vertex_attrib_binding		    VERTEX_BINDING_OFFSET
        //    use ARB_vertex_attrib_binding		    VERTEX_BINDING_STRIDE
        //    use ARB_vertex_attrib_binding		    MAX_VERTEX_ATTRIB_RELATIVE_OFFSET
        //    use ARB_vertex_attrib_binding		    MAX_VERTEX_ATTRIB_BINDINGS


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// ARB extensions, in ARB extension order
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //1
        //ARB_multitexture
        public const uint TEXTURE0_ARB = 0x84C0;
        public const uint TEXTURE1_ARB = 0x84C1;
        public const uint TEXTURE2_ARB = 0x84C2;
        public const uint TEXTURE3_ARB = 0x84C3;
        public const uint TEXTURE4_ARB = 0x84C4;
        public const uint TEXTURE5_ARB = 0x84C5;
        public const uint TEXTURE6_ARB = 0x84C6;
        public const uint TEXTURE7_ARB = 0x84C7;
        public const uint TEXTURE8_ARB = 0x84C8;
        public const uint TEXTURE9_ARB = 0x84C9;
        public const uint TEXTURE10_ARB = 0x84CA;
        public const uint TEXTURE11_ARB = 0x84CB;
        public const uint TEXTURE12_ARB = 0x84CC;
        public const uint TEXTURE13_ARB = 0x84CD;
        public const uint TEXTURE14_ARB = 0x84CE;
        public const uint TEXTURE15_ARB = 0x84CF;
        public const uint TEXTURE16_ARB = 0x84D0;
        public const uint TEXTURE17_ARB = 0x84D1;
        public const uint TEXTURE18_ARB = 0x84D2;
        public const uint TEXTURE19_ARB = 0x84D3;
        public const uint TEXTURE20_ARB = 0x84D4;
        public const uint TEXTURE21_ARB = 0x84D5;
        public const uint TEXTURE22_ARB = 0x84D6;
        public const uint TEXTURE23_ARB = 0x84D7;
        public const uint TEXTURE24_ARB = 0x84D8;
        public const uint TEXTURE25_ARB = 0x84D9;
        public const uint TEXTURE26_ARB = 0x84DA;
        public const uint TEXTURE27_ARB = 0x84DB;
        public const uint TEXTURE28_ARB = 0x84DC;
        public const uint TEXTURE29_ARB = 0x84DD;
        public const uint TEXTURE30_ARB = 0x84DE;
        public const uint TEXTURE31_ARB = 0x84DF;
        public const uint ACTIVE_TEXTURE_ARB = 0x84E0; // 1 I
        public const uint CLIENT_ACTIVE_TEXTURE_ARB = 0x84E1; // 1 I
        public const uint MAX_TEXTURE_UNITS_ARB = 0x84E2; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //2 - GLX_ARB_get_proc_address

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //3
        //ARB_transpose_matrix
        public const uint TRANSPOSE_MODELVIEW_MATRIX_ARB = 0x84E3; // 16 F
        public const uint TRANSPOSE_PROJECTION_MATRIX_ARB = 0x84E4; // 16 F
        public const uint TRANSPOSE_TEXTURE_MATRIX_ARB = 0x84E5; // 16 F
        public const uint TRANSPOSE_COLOR_MATRIX_ARB = 0x84E6; // 16 F

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //4 - WGL_ARB_buffer_region

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //5
        //ARB_multisample
        public const uint MULTISAMPLE_ARB = 0x809D;
        public const uint SAMPLE_ALPHA_TO_COVERAGE_ARB = 0x809E;
        public const uint SAMPLE_ALPHA_TO_ONE_ARB = 0x809F;
        public const uint SAMPLE_COVERAGE_ARB = 0x80A0;
        public const uint SAMPLE_BUFFERS_ARB = 0x80A8;
        public const uint SAMPLES_ARB = 0x80A9;
        public const uint SAMPLE_COVERAGE_VALUE_ARB = 0x80AA;
        public const uint SAMPLE_COVERAGE_INVERT_ARB = 0x80AB;
        public const uint MULTISAMPLE_BIT_ARB = 0x20000000;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //6
        //ARB_texture_env_add

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //7
        //ARB_texture_cube_map
        public const uint NORMAL_MAP_ARB = 0x8511;
        public const uint REFLECTION_MAP_ARB = 0x8512;
        public const uint TEXTURE_CUBE_MAP_ARB = 0x8513;
        public const uint TEXTURE_BINDING_CUBE_MAP_ARB = 0x8514;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_X_ARB = 0x8515;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_X_ARB = 0x8516;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_Y_ARB = 0x8517;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_Y_ARB = 0x8518;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_Z_ARB = 0x8519;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_Z_ARB = 0x851A;
        public const uint PROXY_TEXTURE_CUBE_MAP_ARB = 0x851B;
        public const uint MAX_CUBE_MAP_TEXTURE_SIZE_ARB = 0x851C;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //8 - WGL_ARB_extensions_string
        //// ARB Extension //9 - WGL_ARB_pixel_format
        //// ARB Extension //10 - WGL_ARB_make_current_read
        //// ARB Extension //11 - WGL_ARB_pbuffer

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //12
        //ARB_texture_compression
        public const uint COMPRESSED_ALPHA_ARB = 0x84E9;
        public const uint COMPRESSED_LUMINANCE_ARB = 0x84EA;
        public const uint COMPRESSED_LUMINANCE_ALPHA_ARB = 0x84EB;
        public const uint COMPRESSED_INTENSITY_ARB = 0x84EC;
        public const uint COMPRESSED_RGB_ARB = 0x84ED;
        public const uint COMPRESSED_RGBA_ARB = 0x84EE;
        public const uint TEXTURE_COMPRESSION_HINT_ARB = 0x84EF;
        public const uint TEXTURE_COMPRESSED_IMAGE_SIZE_ARB = 0x86A0;
        public const uint TEXTURE_COMPRESSED_ARB = 0x86A1;
        public const uint NUM_COMPRESSED_TEXTURE_FORMATS_ARB = 0x86A2;
        public const uint COMPRESSED_TEXTURE_FORMATS_ARB = 0x86A3;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //13
        //// Promoted from //36 SGIS_texture_border_clamp
        //ARB_texture_border_clamp
        public const uint CLAMP_TO_BORDER_ARB = 0x812D;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //14 - promoted from //54 EXT_point_parameters
        //// Promoted from //54 {SGIS,EXT}_point_parameters
        //ARB_point_parameters
        public const uint POINT_SIZE_MIN_ARB = 0x8126; // 1 F
        public const uint POINT_SIZE_MAX_ARB = 0x8127; // 1 F
        public const uint POINT_FADE_THRESHOLD_SIZE_ARB = 0x8128; // 1 F
        public const uint POINT_DISTANCE_ATTENUATION_ARB = 0x8129; // 3 F

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //15
        //ARB_vertex_blend
        public const uint MAX_VERTEX_UNITS_ARB = 0x86A4;
        public const uint ACTIVE_VERTEX_UNITS_ARB = 0x86A5;
        public const uint WEIGHT_SUM_UNITY_ARB = 0x86A6;
        public const uint VERTEX_BLEND_ARB = 0x86A7;
        public const uint CURRENT_WEIGHT_ARB = 0x86A8;
        public const uint WEIGHT_ARRAY_TYPE_ARB = 0x86A9;
        public const uint WEIGHT_ARRAY_STRIDE_ARB = 0x86AA;
        public const uint WEIGHT_ARRAY_SIZE_ARB = 0x86AB;
        public const uint WEIGHT_ARRAY_POINTER_ARB = 0x86AC;
        public const uint WEIGHT_ARRAY_ARB = 0x86AD;
        public const uint MODELVIEW0_ARB = 0x1700;
        public const uint MODELVIEW1_ARB = 0x850A;
        public const uint MODELVIEW2_ARB = 0x8722;
        public const uint MODELVIEW3_ARB = 0x8723;
        public const uint MODELVIEW4_ARB = 0x8724;
        public const uint MODELVIEW5_ARB = 0x8725;
        public const uint MODELVIEW6_ARB = 0x8726;
        public const uint MODELVIEW7_ARB = 0x8727;
        public const uint MODELVIEW8_ARB = 0x8728;
        public const uint MODELVIEW9_ARB = 0x8729;
        public const uint MODELVIEW10_ARB = 0x872A;
        public const uint MODELVIEW11_ARB = 0x872B;
        public const uint MODELVIEW12_ARB = 0x872C;
        public const uint MODELVIEW13_ARB = 0x872D;
        public const uint MODELVIEW14_ARB = 0x872E;
        public const uint MODELVIEW15_ARB = 0x872F;
        public const uint MODELVIEW16_ARB = 0x8730;
        public const uint MODELVIEW17_ARB = 0x8731;
        public const uint MODELVIEW18_ARB = 0x8732;
        public const uint MODELVIEW19_ARB = 0x8733;
        public const uint MODELVIEW20_ARB = 0x8734;
        public const uint MODELVIEW21_ARB = 0x8735;
        public const uint MODELVIEW22_ARB = 0x8736;
        public const uint MODELVIEW23_ARB = 0x8737;
        public const uint MODELVIEW24_ARB = 0x8738;
        public const uint MODELVIEW25_ARB = 0x8739;
        public const uint MODELVIEW26_ARB = 0x873A;
        public const uint MODELVIEW27_ARB = 0x873B;
        public const uint MODELVIEW28_ARB = 0x873C;
        public const uint MODELVIEW29_ARB = 0x873D;
        public const uint MODELVIEW30_ARB = 0x873E;
        public const uint MODELVIEW31_ARB = 0x873F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //16
        //ARB_matrix_palette
        public const uint MATRIX_PALETTE_ARB = 0x8840;
        public const uint MAX_MATRIX_PALETTE_STACK_DEPTH_ARB = 0x8841;
        public const uint MAX_PALETTE_MATRICES_ARB = 0x8842;
        public const uint CURRENT_PALETTE_MATRIX_ARB = 0x8843;
        public const uint MATRIX_INDEX_ARRAY_ARB = 0x8844;
        public const uint CURRENT_MATRIX_INDEX_ARB = 0x8845;
        public const uint MATRIX_INDEX_ARRAY_SIZE_ARB = 0x8846;
        public const uint MATRIX_INDEX_ARRAY_TYPE_ARB = 0x8847;
        public const uint MATRIX_INDEX_ARRAY_STRIDE_ARB = 0x8848;
        public const uint MATRIX_INDEX_ARRAY_POINTER_ARB = 0x8849;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //17
        //// Shares enum values with EXT_texture_env_combine
        //ARB_texture_env_combine
        public const uint COMBINE_ARB = 0x8570;
        public const uint COMBINE_RGB_ARB = 0x8571;
        public const uint COMBINE_ALPHA_ARB = 0x8572;
        public const uint SOURCE0_RGB_ARB = 0x8580;
        public const uint SOURCE1_RGB_ARB = 0x8581;
        public const uint SOURCE2_RGB_ARB = 0x8582;
        public const uint SOURCE0_ALPHA_ARB = 0x8588;
        public const uint SOURCE1_ALPHA_ARB = 0x8589;
        public const uint SOURCE2_ALPHA_ARB = 0x858A;
        public const uint OPERAND0_RGB_ARB = 0x8590;
        public const uint OPERAND1_RGB_ARB = 0x8591;
        public const uint OPERAND2_RGB_ARB = 0x8592;
        public const uint OPERAND0_ALPHA_ARB = 0x8598;
        public const uint OPERAND1_ALPHA_ARB = 0x8599;
        public const uint OPERAND2_ALPHA_ARB = 0x859A;
        public const uint RGB_SCALE_ARB = 0x8573;
        public const uint ADD_SIGNED_ARB = 0x8574;
        public const uint INTERPOLATE_ARB = 0x8575;
        public const uint SUBTRACT_ARB = 0x84E7;
        public const uint CONSTANT_ARB = 0x8576;
        public const uint PRIMARY_COLOR_ARB = 0x8577;
        public const uint PREVIOUS_ARB = 0x8578;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //18
        //ARB_texture_env_crossbar

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //19
        //// Promoted from //220 EXT_texture_env_dot3; enum values changed
        //ARB_texture_env_dot3
        public const uint DOT3_RGB_ARB = 0x86AE;
        public const uint DOT3_RGBA_ARB = 0x86AF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //20 - WGL_ARB_render_texture

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //21
        //ARB_texture_mirrored_repeat
        public const uint MIRRORED_REPEAT_ARB = 0x8370;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //22
        //ARB_depth_texture
        public const uint DEPTH_COMPONENT16_ARB = 0x81A5;
        public const uint DEPTH_COMPONENT24_ARB = 0x81A6;
        public const uint DEPTH_COMPONENT32_ARB = 0x81A7;
        public const uint TEXTURE_DEPTH_SIZE_ARB = 0x884A;
        public const uint DEPTH_TEXTURE_MODE_ARB = 0x884B;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //23
        //ARB_shadow
        public const uint TEXTURE_COMPARE_MODE_ARB = 0x884C;
        public const uint TEXTURE_COMPARE_FUNC_ARB = 0x884D;
        public const uint COMPARE_R_TO_TEXTURE_ARB = 0x884E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //24
        //ARB_shadow_ambient
        public const uint TEXTURE_COMPARE_FAIL_VALUE_ARB = 0x80BF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //25
        //ARB_window_pos

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //26
        //// ARB_vertex_program enums are shared by ARB_fragment_program are so marked.
        //// Unfortunately, PROGRAM_BINDING_ARB does accidentally reuse 0x8677; -
        ////   this was a spec editing typo that's now uncorrectable.
        //ARB_vertex_program
        public const uint COLOR_SUM_ARB = 0x8458;
        public const uint VERTEX_PROGRAM_ARB = 0x8620;
        public const uint VERTEX_ATTRIB_ARRAY_ENABLED_ARB = 0x8622;
        public const uint VERTEX_ATTRIB_ARRAY_SIZE_ARB = 0x8623;
        public const uint VERTEX_ATTRIB_ARRAY_STRIDE_ARB = 0x8624;
        public const uint VERTEX_ATTRIB_ARRAY_TYPE_ARB = 0x8625;
        public const uint CURRENT_VERTEX_ATTRIB_ARB = 0x8626;
        public const uint PROGRAM_LENGTH_ARB = 0x8627;    // shared
        public const uint PROGRAM_STRING_ARB = 0x8628;    // shared
        public const uint MAX_PROGRAM_MATRIX_STACK_DEPTH_ARB = 0x862E;    // shared
        public const uint MAX_PROGRAM_MATRICES_ARB = 0x862F;    // shared
        public const uint CURRENT_MATRIX_STACK_DEPTH_ARB = 0x8640;    // shared
        public const uint CURRENT_MATRIX_ARB = 0x8641;    // shared
        public const uint VERTEX_PROGRAM_POINT_SIZE_ARB = 0x8642;
        public const uint VERTEX_PROGRAM_TWO_SIDE_ARB = 0x8643;
        public const uint VERTEX_ATTRIB_ARRAY_POINTER_ARB = 0x8645;
        public const uint PROGRAM_ERROR_POSITION_ARB = 0x864B;    // shared
        public const uint PROGRAM_BINDING_ARB = 0x8677;    // shared
        public const uint MAX_VERTEX_ATTRIBS_ARB = 0x8869;
        public const uint VERTEX_ATTRIB_ARRAY_NORMALIZED_ARB = 0x886A;
        public const uint PROGRAM_ERROR_STRING_ARB = 0x8874;    // shared
        public const uint PROGRAM_FORMAT_ASCII_ARB = 0x8875;    // shared
        public const uint PROGRAM_FORMAT_ARB = 0x8876;    // shared
        public const uint PROGRAM_INSTRUCTIONS_ARB = 0x88A0;    // shared
        public const uint MAX_PROGRAM_INSTRUCTIONS_ARB = 0x88A1;    // shared
        public const uint PROGRAM_NATIVE_INSTRUCTIONS_ARB = 0x88A2;    // shared
        public const uint MAX_PROGRAM_NATIVE_INSTRUCTIONS_ARB = 0x88A3;    // shared
        public const uint PROGRAM_TEMPORARIES_ARB = 0x88A4;    // shared
        public const uint MAX_PROGRAM_TEMPORARIES_ARB = 0x88A5;    // shared
        public const uint PROGRAM_NATIVE_TEMPORARIES_ARB = 0x88A6;    // shared
        public const uint MAX_PROGRAM_NATIVE_TEMPORARIES_ARB = 0x88A7;    // shared
        public const uint PROGRAM_PARAMETERS_ARB = 0x88A8;    // shared
        public const uint MAX_PROGRAM_PARAMETERS_ARB = 0x88A9;    // shared
        public const uint PROGRAM_NATIVE_PARAMETERS_ARB = 0x88AA;    // shared
        public const uint MAX_PROGRAM_NATIVE_PARAMETERS_ARB = 0x88AB;    // shared
        public const uint PROGRAM_ATTRIBS_ARB = 0x88AC;    // shared
        public const uint MAX_PROGRAM_ATTRIBS_ARB = 0x88AD;    // shared
        public const uint PROGRAM_NATIVE_ATTRIBS_ARB = 0x88AE;    // shared
        public const uint MAX_PROGRAM_NATIVE_ATTRIBS_ARB = 0x88AF;    // shared
        public const uint PROGRAM_ADDRESS_REGISTERS_ARB = 0x88B0;    // shared
        public const uint MAX_PROGRAM_ADDRESS_REGISTERS_ARB = 0x88B1;    // shared
        public const uint PROGRAM_NATIVE_ADDRESS_REGISTERS_ARB = 0x88B2;    // shared
        public const uint MAX_PROGRAM_NATIVE_ADDRESS_REGISTERS_ARB = 0x88B3;    // shared
        public const uint MAX_PROGRAM_LOCAL_PARAMETERS_ARB = 0x88B4;    // shared
        public const uint MAX_PROGRAM_ENV_PARAMETERS_ARB = 0x88B5;    // shared
        public const uint PROGRAM_UNDER_NATIVE_LIMITS_ARB = 0x88B6;    // shared
        public const uint TRANSPOSE_CURRENT_MATRIX_ARB = 0x88B7;    // shared
        public const uint MATRIX0_ARB = 0x88C0;    // shared
        public const uint MATRIX1_ARB = 0x88C1;    // shared
        public const uint MATRIX2_ARB = 0x88C2;    // shared
        public const uint MATRIX3_ARB = 0x88C3;    // shared
        public const uint MATRIX4_ARB = 0x88C4;    // shared
        public const uint MATRIX5_ARB = 0x88C5;    // shared
        public const uint MATRIX6_ARB = 0x88C6;    // shared
        public const uint MATRIX7_ARB = 0x88C7;    // shared
        public const uint MATRIX8_ARB = 0x88C8;    // shared
        public const uint MATRIX9_ARB = 0x88C9;    // shared
        public const uint MATRIX10_ARB = 0x88CA;    // shared
        public const uint MATRIX11_ARB = 0x88CB;    // shared
        public const uint MATRIX12_ARB = 0x88CC;    // shared
        public const uint MATRIX13_ARB = 0x88CD;    // shared
        public const uint MATRIX14_ARB = 0x88CE;    // shared
        public const uint MATRIX15_ARB = 0x88CF;    // shared
        public const uint MATRIX16_ARB = 0x88D0;    // shared
        public const uint MATRIX17_ARB = 0x88D1;    // shared
        public const uint MATRIX18_ARB = 0x88D2;    // shared
        public const uint MATRIX19_ARB = 0x88D3;    // shared
        public const uint MATRIX20_ARB = 0x88D4;    // shared
        public const uint MATRIX21_ARB = 0x88D5;    // shared
        public const uint MATRIX22_ARB = 0x88D6;    // shared
        public const uint MATRIX23_ARB = 0x88D7;    // shared
        public const uint MATRIX24_ARB = 0x88D8;    // shared
        public const uint MATRIX25_ARB = 0x88D9;    // shared
        public const uint MATRIX26_ARB = 0x88DA;    // shared
        public const uint MATRIX27_ARB = 0x88DB;    // shared
        public const uint MATRIX28_ARB = 0x88DC;    // shared
        public const uint MATRIX29_ARB = 0x88DD;    // shared
        public const uint MATRIX30_ARB = 0x88DE;    // shared
        public const uint MATRIX31_ARB = 0x88DF;    // shared

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //27
        //// Some ARB_fragment_program enums are shared with ARB_vertex_program,
        ////   and are only included in that //define block, for now.
        //ARB_fragment_program
        //public const uint PROGRAM_LENGTH_ARB = 0x8627;    // shared
        //public const uint PROGRAM_STRING_ARB = 0x8628;    // shared
        //public const uint MAX_PROGRAM_MATRIX_STACK_DEPTH_ARB = 0x862E;    // shared
        //public const uint MAX_PROGRAM_MATRICES_ARB = 0x862F;    // shared
        //public const uint CURRENT_MATRIX_STACK_DEPTH_ARB = 0x8640;    // shared
        //public const uint CURRENT_MATRIX_ARB = 0x8641;    // shared
        //public const uint PROGRAM_ERROR_POSITION_ARB = 0x864B;    // shared
        //public const uint PROGRAM_BINDING_ARB = 0x8677;    // shared
        public const uint FRAGMENT_PROGRAM_ARB = 0x8804;
        public const uint PROGRAM_ALU_INSTRUCTIONS_ARB = 0x8805;
        public const uint PROGRAM_TEX_INSTRUCTIONS_ARB = 0x8806;
        public const uint PROGRAM_TEX_INDIRECTIONS_ARB = 0x8807;
        public const uint PROGRAM_NATIVE_ALU_INSTRUCTIONS_ARB = 0x8808;
        public const uint PROGRAM_NATIVE_TEX_INSTRUCTIONS_ARB = 0x8809;
        public const uint PROGRAM_NATIVE_TEX_INDIRECTIONS_ARB = 0x880A;
        public const uint MAX_PROGRAM_ALU_INSTRUCTIONS_ARB = 0x880B;
        public const uint MAX_PROGRAM_TEX_INSTRUCTIONS_ARB = 0x880C;
        public const uint MAX_PROGRAM_TEX_INDIRECTIONS_ARB = 0x880D;
        public const uint MAX_PROGRAM_NATIVE_ALU_INSTRUCTIONS_ARB = 0x880E;
        public const uint MAX_PROGRAM_NATIVE_TEX_INSTRUCTIONS_ARB = 0x880F;
        public const uint MAX_PROGRAM_NATIVE_TEX_INDIRECTIONS_ARB = 0x8810;
        public const uint MAX_TEXTURE_COORDS_ARB = 0x8871;
        public const uint MAX_TEXTURE_IMAGE_UNITS_ARB = 0x8872;
        //public const uint PROGRAM_ERROR_STRING_ARB = 0x8874;    // shared
        //public const uint PROGRAM_FORMAT_ASCII_ARB = 0x8875;    // shared
        //public const uint PROGRAM_FORMAT_ARB = 0x8876;    // shared
        //public const uint PROGRAM_INSTRUCTIONS_ARB = 0x88A0;    // shared
        //public const uint MAX_PROGRAM_INSTRUCTIONS_ARB = 0x88A1;    // shared
        //public const uint PROGRAM_NATIVE_INSTRUCTIONS_ARB = 0x88A2;    // shared
        //public const uint MAX_PROGRAM_NATIVE_INSTRUCTIONS_ARB = 0x88A3;    // shared
        //public const uint PROGRAM_TEMPORARIES_ARB = 0x88A4;    // shared
        //public const uint MAX_PROGRAM_TEMPORARIES_ARB = 0x88A5;    // shared
        //public const uint PROGRAM_NATIVE_TEMPORARIES_ARB = 0x88A6;    // shared
        //public const uint MAX_PROGRAM_NATIVE_TEMPORARIES_ARB = 0x88A7;    // shared
        //public const uint PROGRAM_PARAMETERS_ARB = 0x88A8;    // shared
        //public const uint MAX_PROGRAM_PARAMETERS_ARB = 0x88A9;    // shared
        //public const uint PROGRAM_NATIVE_PARAMETERS_ARB = 0x88AA;    // shared
        //public const uint MAX_PROGRAM_NATIVE_PARAMETERS_ARB = 0x88AB;    // shared
        //public const uint PROGRAM_ATTRIBS_ARB = 0x88AC;    // shared
        //public const uint MAX_PROGRAM_ATTRIBS_ARB = 0x88AD;    // shared
        //public const uint PROGRAM_NATIVE_ATTRIBS_ARB = 0x88AE;    // shared
        //public const uint MAX_PROGRAM_NATIVE_ATTRIBS_ARB = 0x88AF;    // shared
        //public const uint PROGRAM_ADDRESS_REGISTERS_ARB = 0x88B0;    // shared
        //public const uint MAX_PROGRAM_ADDRESS_REGISTERS_ARB = 0x88B1;    // shared
        //public const uint PROGRAM_NATIVE_ADDRESS_REGISTERS_ARB = 0x88B2;    // shared
        //public const uint MAX_PROGRAM_NATIVE_ADDRESS_REGISTERS_ARB = 0x88B3;    // shared
        //public const uint MAX_PROGRAM_LOCAL_PARAMETERS_ARB = 0x88B4;    // shared
        //public const uint MAX_PROGRAM_ENV_PARAMETERS_ARB = 0x88B5;    // shared
        //public const uint PROGRAM_UNDER_NATIVE_LIMITS_ARB = 0x88B6;    // shared
        //public const uint TRANSPOSE_CURRENT_MATRIX_ARB = 0x88B7;    // shared
        //public const uint MATRIX0_ARB = 0x88C0;    // shared
        //public const uint MATRIX1_ARB = 0x88C1;    // shared
        //public const uint MATRIX2_ARB = 0x88C2;    // shared
        //public const uint MATRIX3_ARB = 0x88C3;    // shared
        //public const uint MATRIX4_ARB = 0x88C4;    // shared
        //public const uint MATRIX5_ARB = 0x88C5;    // shared
        //public const uint MATRIX6_ARB = 0x88C6;    // shared
        //public const uint MATRIX7_ARB = 0x88C7;    // shared
        //public const uint MATRIX8_ARB = 0x88C8;    // shared
        //public const uint MATRIX9_ARB = 0x88C9;    // shared
        //public const uint MATRIX10_ARB = 0x88CA;    // shared
        //public const uint MATRIX11_ARB = 0x88CB;    // shared
        //public const uint MATRIX12_ARB = 0x88CC;    // shared
        //public const uint MATRIX13_ARB = 0x88CD;    // shared
        //public const uint MATRIX14_ARB = 0x88CE;    // shared
        //public const uint MATRIX15_ARB = 0x88CF;    // shared
        //public const uint MATRIX16_ARB = 0x88D0;    // shared
        //public const uint MATRIX17_ARB = 0x88D1;    // shared
        //public const uint MATRIX18_ARB = 0x88D2;    // shared
        //public const uint MATRIX19_ARB = 0x88D3;    // shared
        //public const uint MATRIX20_ARB = 0x88D4;    // shared
        //public const uint MATRIX21_ARB = 0x88D5;    // shared
        //public const uint MATRIX22_ARB = 0x88D6;    // shared
        //public const uint MATRIX23_ARB = 0x88D7;    // shared
        //public const uint MATRIX24_ARB = 0x88D8;    // shared
        //public const uint MATRIX25_ARB = 0x88D9;    // shared
        //public const uint MATRIX26_ARB = 0x88DA;    // shared
        //public const uint MATRIX27_ARB = 0x88DB;    // shared
        //public const uint MATRIX28_ARB = 0x88DC;    // shared
        //public const uint MATRIX29_ARB = 0x88DD;    // shared
        //public const uint MATRIX30_ARB = 0x88DE;    // shared
        //public const uint MATRIX31_ARB = 0x88DF;    // shared


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //28
        //ARB_vertex_buffer_object
        public const uint BUFFER_SIZE_ARB = 0x8764;
        public const uint BUFFER_USAGE_ARB = 0x8765;
        public const uint ARRAY_BUFFER_ARB = 0x8892;
        public const uint ELEMENT_ARRAY_BUFFER_ARB = 0x8893;
        public const uint ARRAY_BUFFER_BINDING_ARB = 0x8894;
        public const uint ELEMENT_ARRAY_BUFFER_BINDING_ARB = 0x8895;
        public const uint VERTEX_ARRAY_BUFFER_BINDING_ARB = 0x8896;
        public const uint NORMAL_ARRAY_BUFFER_BINDING_ARB = 0x8897;
        public const uint COLOR_ARRAY_BUFFER_BINDING_ARB = 0x8898;
        public const uint INDEX_ARRAY_BUFFER_BINDING_ARB = 0x8899;
        public const uint TEXTURE_COORD_ARRAY_BUFFER_BINDING_ARB = 0x889A;
        public const uint EDGE_FLAG_ARRAY_BUFFER_BINDING_ARB = 0x889B;
        public const uint SECONDARY_COLOR_ARRAY_BUFFER_BINDING_ARB = 0x889C;
        public const uint FOG_COORDINATE_ARRAY_BUFFER_BINDING_ARB = 0x889D;
        public const uint WEIGHT_ARRAY_BUFFER_BINDING_ARB = 0x889E;
        public const uint VERTEX_ATTRIB_ARRAY_BUFFER_BINDING_ARB = 0x889F;
        public const uint READ_ONLY_ARB = 0x88B8;
        public const uint WRITE_ONLY_ARB = 0x88B9;
        public const uint READ_WRITE_ARB = 0x88BA;
        public const uint BUFFER_ACCESS_ARB = 0x88BB;
        public const uint BUFFER_MAPPED_ARB = 0x88BC;
        public const uint BUFFER_MAP_POINTER_ARB = 0x88BD;
        public const uint STREAM_DRAW_ARB = 0x88E0;
        public const uint STREAM_READ_ARB = 0x88E1;
        public const uint STREAM_COPY_ARB = 0x88E2;
        public const uint STATIC_DRAW_ARB = 0x88E4;
        public const uint STATIC_READ_ARB = 0x88E5;
        public const uint STATIC_COPY_ARB = 0x88E6;
        public const uint DYNAMIC_DRAW_ARB = 0x88E8;
        public const uint DYNAMIC_READ_ARB = 0x88E9;
        public const uint DYNAMIC_COPY_ARB = 0x88EA;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //29
        //ARB_occlusion_query
        public const uint QUERY_COUNTER_BITS_ARB = 0x8864;
        public const uint CURRENT_QUERY_ARB = 0x8865;
        public const uint QUERY_RESULT_ARB = 0x8866;
        public const uint QUERY_RESULT_AVAILABLE_ARB = 0x8867;
        public const uint SAMPLES_PASSED_ARB = 0x8914;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //30
        //ARB_shader_objects
        public const uint PROGRAM_OBJECT_ARB = 0x8B40;
        public const uint SHADER_OBJECT_ARB = 0x8B48;
        public const uint OBJECT_TYPE_ARB = 0x8B4E;
        public const uint OBJECT_SUBTYPE_ARB = 0x8B4F;
        public const uint FLOAT_VEC2_ARB = 0x8B50;
        public const uint FLOAT_VEC3_ARB = 0x8B51;
        public const uint FLOAT_VEC4_ARB = 0x8B52;
        public const uint INT_VEC2_ARB = 0x8B53;
        public const uint INT_VEC3_ARB = 0x8B54;
        public const uint INT_VEC4_ARB = 0x8B55;
        public const uint BOOL_ARB = 0x8B56;
        public const uint BOOL_VEC2_ARB = 0x8B57;
        public const uint BOOL_VEC3_ARB = 0x8B58;
        public const uint BOOL_VEC4_ARB = 0x8B59;
        public const uint FLOAT_MAT2_ARB = 0x8B5A;
        public const uint FLOAT_MAT3_ARB = 0x8B5B;
        public const uint FLOAT_MAT4_ARB = 0x8B5C;
        public const uint SAMPLER_1D_ARB = 0x8B5D;
        public const uint SAMPLER_2D_ARB = 0x8B5E;
        public const uint SAMPLER_3D_ARB = 0x8B5F;
        public const uint SAMPLER_CUBE_ARB = 0x8B60;
        public const uint SAMPLER_1D_SHADOW_ARB = 0x8B61;
        public const uint SAMPLER_2D_SHADOW_ARB = 0x8B62;
        public const uint SAMPLER_2D_RECT_ARB = 0x8B63;
        public const uint SAMPLER_2D_RECT_SHADOW_ARB = 0x8B64;
        public const uint OBJECT_DELETE_STATUS_ARB = 0x8B80;
        public const uint OBJECT_COMPILE_STATUS_ARB = 0x8B81;
        public const uint OBJECT_LINK_STATUS_ARB = 0x8B82;
        public const uint OBJECT_VALIDATE_STATUS_ARB = 0x8B83;
        public const uint OBJECT_INFO_LOG_LENGTH_ARB = 0x8B84;
        public const uint OBJECT_ATTACHED_OBJECTS_ARB = 0x8B85;
        public const uint OBJECT_ACTIVE_UNIFORMS_ARB = 0x8B86;
        public const uint OBJECT_ACTIVE_UNIFORM_MAX_LENGTH_ARB = 0x8B87;
        public const uint OBJECT_SHADER_SOURCE_LENGTH_ARB = 0x8B88;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //31
        //// Additional enums are reused from ARB_vertex/fragment_program and ARB_shader_objects
        //ARB_vertex_shader
        public const uint VERTEX_SHADER_ARB = 0x8B31;
        public const uint MAX_VERTEX_UNIFORM_COMPONENTS_ARB = 0x8B4A;
        public const uint MAX_VARYING_FLOATS_ARB = 0x8B4B;
        public const uint MAX_VERTEX_TEXTURE_IMAGE_UNITS_ARB = 0x8B4C;
        public const uint MAX_COMBINED_TEXTURE_IMAGE_UNITS_ARB = 0x8B4D;
        public const uint OBJECT_ACTIVE_ATTRIBUTES_ARB = 0x8B89;
        public const uint OBJECT_ACTIVE_ATTRIBUTE_MAX_LENGTH_ARB = 0x8B8A;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //32
        //// Additional enums are reused from ARB_fragment_program and ARB_shader_objects
        //ARB_fragment_shader
        public const uint FRAGMENT_SHADER_ARB = 0x8B30;
        public const uint MAX_FRAGMENT_UNIFORM_COMPONENTS_ARB = 0x8B49;
        public const uint FRAGMENT_SHADER_DERIVATIVE_HINT_ARB = 0x8B8B;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //33
        //ARB_shading_language_100
        public const uint SHADING_LANGUAGE_VERSION_ARB = 0x8B8C;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //34
        //// No new tokens
        //ARB_texture_non_power_of_two

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //35
        //ARB_point_sprite
        public const uint POINT_SPRITE_ARB = 0x8861;
        public const uint COORD_REPLACE_ARB = 0x8862;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //36
        //// No new tokens
        //ARB_fragment_program_shadow

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //37
        //ARB_draw_buffers
        public const uint MAX_DRAW_BUFFERS_ARB = 0x8824;
        public const uint DRAW_BUFFER0_ARB = 0x8825;
        public const uint DRAW_BUFFER1_ARB = 0x8826;
        public const uint DRAW_BUFFER2_ARB = 0x8827;
        public const uint DRAW_BUFFER3_ARB = 0x8828;
        public const uint DRAW_BUFFER4_ARB = 0x8829;
        public const uint DRAW_BUFFER5_ARB = 0x882A;
        public const uint DRAW_BUFFER6_ARB = 0x882B;
        public const uint DRAW_BUFFER7_ARB = 0x882C;
        public const uint DRAW_BUFFER8_ARB = 0x882D;
        public const uint DRAW_BUFFER9_ARB = 0x882E;
        public const uint DRAW_BUFFER10_ARB = 0x882F;
        public const uint DRAW_BUFFER11_ARB = 0x8830;
        public const uint DRAW_BUFFER12_ARB = 0x8831;
        public const uint DRAW_BUFFER13_ARB = 0x8832;
        public const uint DRAW_BUFFER14_ARB = 0x8833;
        public const uint DRAW_BUFFER15_ARB = 0x8834;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //38
        //ARB_texture_rectangle
        public const uint TEXTURE_RECTANGLE_ARB = 0x84F5;
        public const uint TEXTURE_BINDING_RECTANGLE_ARB = 0x84F6;
        public const uint PROXY_TEXTURE_RECTANGLE_ARB = 0x84F7;
        public const uint MAX_RECTANGLE_TEXTURE_SIZE_ARB = 0x84F8;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //39
        //ARB_color_buffer_float
        public const uint RGBA_FLOAT_MODE_ARB = 0x8820;
        public const uint CLAMP_VERTEX_COLOR_ARB = 0x891A;
        public const uint CLAMP_FRAGMENT_COLOR_ARB = 0x891B;
        public const uint CLAMP_READ_COLOR_ARB = 0x891C;
        public const uint FIXED_ONLY_ARB = 0x891D;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //40
        //ARB_half_float_pixel
        public const uint HALF_FLOAT_ARB = 0x140B;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //41
        //ARB_texture_float
        public const uint TEXTURE_RED_TYPE_ARB = 0x8C10;
        public const uint TEXTURE_GREEN_TYPE_ARB = 0x8C11;
        public const uint TEXTURE_BLUE_TYPE_ARB = 0x8C12;
        public const uint TEXTURE_ALPHA_TYPE_ARB = 0x8C13;
        public const uint TEXTURE_LUMINANCE_TYPE_ARB = 0x8C14;
        public const uint TEXTURE_INTENSITY_TYPE_ARB = 0x8C15;
        public const uint TEXTURE_DEPTH_TYPE_ARB = 0x8C16;
        public const uint UNSIGNED_NORMALIZED_ARB = 0x8C17;
        public const uint RGBA32F_ARB = 0x8814;
        public const uint RGB32F_ARB = 0x8815;
        public const uint ALPHA32F_ARB = 0x8816;
        public const uint INTENSITY32F_ARB = 0x8817;
        public const uint LUMINANCE32F_ARB = 0x8818;
        public const uint LUMINANCE_ALPHA32F_ARB = 0x8819;
        public const uint RGBA16F_ARB = 0x881A;
        public const uint RGB16F_ARB = 0x881B;
        public const uint ALPHA16F_ARB = 0x881C;
        public const uint INTENSITY16F_ARB = 0x881D;
        public const uint LUMINANCE16F_ARB = 0x881E;
        public const uint LUMINANCE_ALPHA16F_ARB = 0x881F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //42
        //ARB_pixel_buffer_object
        public const uint PIXEL_PACK_BUFFER_ARB = 0x88EB;
        public const uint PIXEL_UNPACK_BUFFER_ARB = 0x88EC;
        public const uint PIXEL_PACK_BUFFER_BINDING_ARB = 0x88ED;
        public const uint PIXEL_UNPACK_BUFFER_BINDING_ARB = 0x88EF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //43
        //ARB_depth_buffer_float
        public const uint DEPTH_COMPONENT32F = 0x8CAC;
        public const uint DEPTH32F_STENCIL8 = 0x8CAD;
        public const uint FLOAT_32_UNSIGNED_INT_24_8_REV = 0x8DAD;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //44
        //// No new tokens
        //ARB_draw_instanced

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //45
        //ARB_framebuffer_object
        public const uint INVALID_FRAMEBUFFER_OPERATION = 0x0506;
        public const uint FRAMEBUFFER_ATTACHMENT_COLOR_ENCODING = 0x8210;
        public const uint FRAMEBUFFER_ATTACHMENT_COMPONENT_TYPE = 0x8211;
        public const uint FRAMEBUFFER_ATTACHMENT_RED_SIZE = 0x8212;
        public const uint FRAMEBUFFER_ATTACHMENT_GREEN_SIZE = 0x8213;
        public const uint FRAMEBUFFER_ATTACHMENT_BLUE_SIZE = 0x8214;
        public const uint FRAMEBUFFER_ATTACHMENT_ALPHA_SIZE = 0x8215;
        public const uint FRAMEBUFFER_ATTACHMENT_DEPTH_SIZE = 0x8216;
        public const uint FRAMEBUFFER_ATTACHMENT_STENCIL_SIZE = 0x8217;
        public const uint FRAMEBUFFER_DEFAULT = 0x8218;
        public const uint FRAMEBUFFER_UNDEFINED = 0x8219;
        public const uint DEPTH_STENCIL_ATTACHMENT = 0x821A;
        public const uint MAX_RENDERBUFFER_SIZE = 0x84E8;
        public const uint DEPTH_STENCIL = 0x84F9;
        public const uint UNSIGNED_INT_24_8 = 0x84FA;
        public const uint DEPTH24_STENCIL8 = 0x88F0;
        public const uint TEXTURE_STENCIL_SIZE = 0x88F1;
        public const uint TEXTURE_RED_TYPE = 0x8C10;
        public const uint TEXTURE_GREEN_TYPE = 0x8C11;
        public const uint TEXTURE_BLUE_TYPE = 0x8C12;
        public const uint TEXTURE_ALPHA_TYPE = 0x8C13;
        public const uint TEXTURE_DEPTH_TYPE = 0x8C16;
        public const uint UNSIGNED_NORMALIZED = 0x8C17;
        public const uint FRAMEBUFFER_BINDING = 0x8CA6;
        public const uint DRAW_FRAMEBUFFER_BINDING = 0x8CA6;    // alias FRAMEBUFFER_BINDING
        public const uint RENDERBUFFER_BINDING = 0x8CA7;
        public const uint READ_FRAMEBUFFER = 0x8CA8;
        public const uint DRAW_FRAMEBUFFER = 0x8CA9;
        public const uint READ_FRAMEBUFFER_BINDING = 0x8CAA;
        public const uint RENDERBUFFER_SAMPLES = 0x8CAB;
        public const uint FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = 0x8CD0;
        public const uint FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = 0x8CD1;
        public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = 0x8CD2;
        public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = 0x8CD3;
        public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER = 0x8CD4;
        public const uint FRAMEBUFFER_COMPLETE = 0x8CD5;
        public const uint FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;
        public const uint FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;
        public const uint FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER = 0x8CDB;
        public const uint FRAMEBUFFER_INCOMPLETE_READ_BUFFER = 0x8CDC;
        public const uint FRAMEBUFFER_UNSUPPORTED = 0x8CDD;
        public const uint MAX_COLOR_ATTACHMENTS = 0x8CDF;
        public const uint COLOR_ATTACHMENT0 = 0x8CE0;
        public const uint COLOR_ATTACHMENT1 = 0x8CE1;
        public const uint COLOR_ATTACHMENT2 = 0x8CE2;
        public const uint COLOR_ATTACHMENT3 = 0x8CE3;
        public const uint COLOR_ATTACHMENT4 = 0x8CE4;
        public const uint COLOR_ATTACHMENT5 = 0x8CE5;
        public const uint COLOR_ATTACHMENT6 = 0x8CE6;
        public const uint COLOR_ATTACHMENT7 = 0x8CE7;
        public const uint COLOR_ATTACHMENT8 = 0x8CE8;
        public const uint COLOR_ATTACHMENT9 = 0x8CE9;
        public const uint COLOR_ATTACHMENT10 = 0x8CEA;
        public const uint COLOR_ATTACHMENT11 = 0x8CEB;
        public const uint COLOR_ATTACHMENT12 = 0x8CEC;
        public const uint COLOR_ATTACHMENT13 = 0x8CED;
        public const uint COLOR_ATTACHMENT14 = 0x8CEE;
        public const uint COLOR_ATTACHMENT15 = 0x8CEF;
        public const uint DEPTH_ATTACHMENT = 0x8D00;
        public const uint STENCIL_ATTACHMENT = 0x8D20;
        public const uint FRAMEBUFFER = 0x8D40;
        public const uint RENDERBUFFER = 0x8D41;
        public const uint RENDERBUFFER_WIDTH = 0x8D42;
        public const uint RENDERBUFFER_HEIGHT = 0x8D43;
        public const uint RENDERBUFFER_INTERNAL_FORMAT = 0x8D44;
        public const uint STENCIL_INDEX1 = 0x8D46;
        public const uint STENCIL_INDEX4 = 0x8D47;
        public const uint STENCIL_INDEX8 = 0x8D48;
        public const uint STENCIL_INDEX16 = 0x8D49;
        public const uint RENDERBUFFER_RED_SIZE = 0x8D50;
        public const uint RENDERBUFFER_GREEN_SIZE = 0x8D51;
        public const uint RENDERBUFFER_BLUE_SIZE = 0x8D52;
        public const uint RENDERBUFFER_ALPHA_SIZE = 0x8D53;
        public const uint RENDERBUFFER_DEPTH_SIZE = 0x8D54;
        public const uint RENDERBUFFER_STENCIL_SIZE = 0x8D55;
        public const uint FRAMEBUFFER_INCOMPLETE_MULTISAMPLE = 0x8D56;
        public const uint MAX_SAMPLES = 0x8D57;
        //profile: compatibility
        public const uint INDEX = 0x8222;
        public const uint TEXTURE_LUMINANCE_TYPE = 0x8C14;
        public const uint TEXTURE_INTENSITY_TYPE = 0x8C15;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //46
        //ARB_framebuffer_sRGB
        public const uint FRAMEBUFFER_SRGB = 0x8DB9;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //47
        //ARB_geometry_shader4
        public const uint LINES_ADJACENCY_ARB = 0x000A;
        public const uint LINE_STRIP_ADJACENCY_ARB = 0x000B;
        public const uint TRIANGLES_ADJACENCY_ARB = 0x000C;
        public const uint TRIANGLE_STRIP_ADJACENCY_ARB = 0x000D;
        public const uint PROGRAM_POINT_SIZE_ARB = 0x8642;
        public const uint MAX_GEOMETRY_TEXTURE_IMAGE_UNITS_ARB = 0x8C29;
        public const uint FRAMEBUFFER_ATTACHMENT_LAYERED_ARB = 0x8DA7;
        public const uint FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS_ARB = 0x8DA8;
        public const uint FRAMEBUFFER_INCOMPLETE_LAYER_COUNT_ARB = 0x8DA9;
        public const uint GEOMETRY_SHADER_ARB = 0x8DD9;
        public const uint GEOMETRY_VERTICES_OUT_ARB = 0x8DDA;
        public const uint GEOMETRY_INPUT_TYPE_ARB = 0x8DDB;
        public const uint GEOMETRY_OUTPUT_TYPE_ARB = 0x8DDC;
        public const uint MAX_GEOMETRY_VARYING_COMPONENTS_ARB = 0x8DDD;
        public const uint MAX_VERTEX_VARYING_COMPONENTS_ARB = 0x8DDE;
        public const uint MAX_GEOMETRY_UNIFORM_COMPONENTS_ARB = 0x8DDF;
        public const uint MAX_GEOMETRY_OUTPUT_VERTICES_ARB = 0x8DE0;
        public const uint MAX_GEOMETRY_TOTAL_OUTPUT_COMPONENTS_ARB = 0x8DE1;
        //    use VERSION_3_0			    MAX_VARYING_COMPONENTS
        //    use ARB_framebuffer_object	    FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //48
        //ARB_half_float_vertex
        public const uint HALF_FLOAT = 0x140B;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //49
        //ARB_instanced_arrays
        public const uint VERTEX_ATTRIB_ARRAY_DIVISOR_ARB = 0x88FE;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //50
        //ARB_map_buffer_range
        public const uint MAP_READ_BIT = 0x0001;
        public const uint MAP_WRITE_BIT = 0x0002;
        public const uint MAP_INVALIDATE_RANGE_BIT = 0x0004;
        public const uint MAP_INVALIDATE_BUFFER_BIT = 0x0008;
        public const uint MAP_FLUSH_EXPLICIT_BIT = 0x0010;
        public const uint MAP_UNSYNCHRONIZED_BIT = 0x0020;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //51
        //ARB_texture_buffer_object
        public const uint TEXTURE_BUFFER_ARB = 0x8C2A;
        public const uint MAX_TEXTURE_BUFFER_SIZE_ARB = 0x8C2B;
        public const uint TEXTURE_BINDING_BUFFER_ARB = 0x8C2C;
        public const uint TEXTURE_BUFFER_DATA_STORE_BINDING_ARB = 0x8C2D;
        public const uint TEXTURE_BUFFER_FORMAT_ARB = 0x8C2E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //52
        //ARB_texture_compression_rgtc
        public const uint COMPRESSED_RED_RGTC1 = 0x8DBB;
        public const uint COMPRESSED_SIGNED_RED_RGTC1 = 0x8DBC;
        public const uint COMPRESSED_RG_RGTC2 = 0x8DBD;
        public const uint COMPRESSED_SIGNED_RG_RGTC2 = 0x8DBE;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //53
        //ARB_texture_rg
        public const uint RG = 0x8227;
        public const uint RG_INTEGER = 0x8228;
        public const uint R8 = 0x8229;
        public const uint R16 = 0x822A;
        public const uint RG8 = 0x822B;
        public const uint RG16 = 0x822C;
        public const uint R16F = 0x822D;
        public const uint R32F = 0x822E;
        public const uint RG16F = 0x822F;
        public const uint RG32F = 0x8230;
        public const uint R8I = 0x8231;
        public const uint R8UI = 0x8232;
        public const uint R16I = 0x8233;
        public const uint R16UI = 0x8234;
        public const uint R32I = 0x8235;
        public const uint R32UI = 0x8236;
        public const uint RG8I = 0x8237;
        public const uint RG8UI = 0x8238;
        public const uint RG16I = 0x8239;
        public const uint RG16UI = 0x823A;
        public const uint RG32I = 0x823B;
        public const uint RG32UI = 0x823C;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //54
        //ARB_vertex_array_object
        public const uint VERTEX_ARRAY_BINDING = 0x85B5;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //55 - WGL_ARB_create_context
        //// ARB Extension //56 - GLX_ARB_create_context

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //57
        //ARB_uniform_buffer_object
        public const uint UNIFORM_BUFFER = 0x8A11;
        public const uint UNIFORM_BUFFER_BINDING = 0x8A28;
        public const uint UNIFORM_BUFFER_START = 0x8A29;
        public const uint UNIFORM_BUFFER_SIZE = 0x8A2A;
        public const uint MAX_VERTEX_UNIFORM_BLOCKS = 0x8A2B;
        public const uint MAX_GEOMETRY_UNIFORM_BLOCKS = 0x8A2C;
        public const uint MAX_FRAGMENT_UNIFORM_BLOCKS = 0x8A2D;
        public const uint MAX_COMBINED_UNIFORM_BLOCKS = 0x8A2E;
        public const uint MAX_UNIFORM_BUFFER_BINDINGS = 0x8A2F;
        public const uint MAX_UNIFORM_BLOCK_SIZE = 0x8A30;
        public const uint MAX_COMBINED_VERTEX_UNIFORM_COMPONENTS = 0x8A31;
        public const uint MAX_COMBINED_GEOMETRY_UNIFORM_COMPONENTS = 0x8A32;
        public const uint MAX_COMBINED_FRAGMENT_UNIFORM_COMPONENTS = 0x8A33;
        public const uint UNIFORM_BUFFER_OFFSET_ALIGNMENT = 0x8A34;
        public const uint ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH = 0x8A35;
        public const uint ACTIVE_UNIFORM_BLOCKS = 0x8A36;
        public const uint UNIFORM_TYPE = 0x8A37;
        public const uint UNIFORM_SIZE = 0x8A38;
        public const uint UNIFORM_NAME_LENGTH = 0x8A39;
        public const uint UNIFORM_BLOCK_INDEX = 0x8A3A;
        public const uint UNIFORM_OFFSET = 0x8A3B;
        public const uint UNIFORM_ARRAY_STRIDE = 0x8A3C;
        public const uint UNIFORM_MATRIX_STRIDE = 0x8A3D;
        public const uint UNIFORM_IS_ROW_MAJOR = 0x8A3E;
        public const uint UNIFORM_BLOCK_BINDING = 0x8A3F;
        public const uint UNIFORM_BLOCK_DATA_SIZE = 0x8A40;
        public const uint UNIFORM_BLOCK_NAME_LENGTH = 0x8A41;
        public const uint UNIFORM_BLOCK_ACTIVE_UNIFORMS = 0x8A42;
        public const uint UNIFORM_BLOCK_ACTIVE_UNIFORM_INDICES = 0x8A43;
        public const uint UNIFORM_BLOCK_REFERENCED_BY_VERTEX_SHADER = 0x8A44;
        public const uint UNIFORM_BLOCK_REFERENCED_BY_GEOMETRY_SHADER = 0x8A45;
        public const uint UNIFORM_BLOCK_REFERENCED_BY_FRAGMENT_SHADER = 0x8A46;
        public const uint INVALID_INDEX = 0xFFFFFFFF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //58
        //// No new tokens
        //ARB_compatibility
        /* ARB_compatibility just defines tokens from core 3.0 */


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //59
        //ARB_copy_buffer
        public const uint COPY_READ_BUFFER_BINDING = 0x8F36;
        public const uint COPY_READ_BUFFER = 0x8F36;    // alias COPY_READ_BUFFER_BINDING
        public const uint COPY_WRITE_BUFFER_BINDING = 0x8F37;
        public const uint COPY_WRITE_BUFFER = 0x8F37;    // alias COPY_WRITE_BUFFER_BINDING

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //60
        //// No new tokens
        //ARB_shader_texture_lod

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //61
        //ARB_depth_clamp
        public const uint DEPTH_CLAMP = 0x864F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //62
        //ARB_draw_elements_base_vertex

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //63
        //ARB_fragment_coord_conventions

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //64
        //ARB_provoking_vertex
        public const uint QUADS_FOLLOW_PROVOKING_VERTEX_CONVENTION = 0x8E4C;
        public const uint FIRST_VERTEX_CONVENTION = 0x8E4D;
        public const uint LAST_VERTEX_CONVENTION = 0x8E4E;
        public const uint PROVOKING_VERTEX = 0x8E4F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //65
        //ARB_seamless_cube_map
        public const uint TEXTURE_CUBE_MAP_SEAMLESS = 0x884F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //66
        //ARB_sync
        public const uint MAX_SERVER_WAIT_TIMEOUT = 0x9111;
        public const uint OBJECT_TYPE = 0x9112;
        public const uint SYNC_CONDITION = 0x9113;
        public const uint SYNC_STATUS = 0x9114;
        public const uint SYNC_FLAGS = 0x9115;
        public const uint SYNC_FENCE = 0x9116;
        public const uint SYNC_GPU_COMMANDS_COMPLETE = 0x9117;
        public const uint UNSIGNALED = 0x9118;
        public const uint SIGNALED = 0x9119;
        public const uint ALREADY_SIGNALED = 0x911A;
        public const uint TIMEOUT_EXPIRED = 0x911B;
        public const uint CONDITION_SATISFIED = 0x911C;
        public const uint WAIT_FAILED = 0x911D;
        public const uint SYNC_FLUSH_COMMANDS_BIT = 0x00000001;
        public const ulong TIMEOUT_IGNORED = 0xFFFFFFFFFFFFFFFF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //67
        //ARB_texture_multisample
        public const uint SAMPLE_POSITION = 0x8E50;
        public const uint SAMPLE_MASK = 0x8E51;
        public const uint SAMPLE_MASK_VALUE = 0x8E52;
        public const uint MAX_SAMPLE_MASK_WORDS = 0x8E59;
        public const uint TEXTURE_2D_MULTISAMPLE = 0x9100;
        public const uint PROXY_TEXTURE_2D_MULTISAMPLE = 0x9101;
        public const uint TEXTURE_2D_MULTISAMPLE_ARRAY = 0x9102;
        public const uint PROXY_TEXTURE_2D_MULTISAMPLE_ARRAY = 0x9103;
        public const uint TEXTURE_BINDING_2D_MULTISAMPLE = 0x9104;
        public const uint TEXTURE_BINDING_2D_MULTISAMPLE_ARRAY = 0x9105;
        public const uint TEXTURE_SAMPLES = 0x9106;
        public const uint TEXTURE_FIXED_SAMPLE_LOCATIONS = 0x9107;
        public const uint SAMPLER_2D_MULTISAMPLE = 0x9108;
        public const uint INT_SAMPLER_2D_MULTISAMPLE = 0x9109;
        public const uint UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE = 0x910A;
        public const uint SAMPLER_2D_MULTISAMPLE_ARRAY = 0x910B;
        public const uint INT_SAMPLER_2D_MULTISAMPLE_ARRAY = 0x910C;
        public const uint UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE_ARRAY = 0x910D;
        public const uint MAX_COLOR_TEXTURE_SAMPLES = 0x910E;
        public const uint MAX_DEPTH_TEXTURE_SAMPLES = 0x910F;
        public const uint MAX_INTEGER_SAMPLES = 0x9110;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //68
        //ARB_vertex_array_bgra
        //    use VERSION_1_2			    BGRA

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //69
        //ARB_draw_buffers_blend

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //70
        //ARB_sample_shading
        public const uint SAMPLE_SHADING_ARB = 0x8C36;
        public const uint MIN_SAMPLE_SHADING_VALUE_ARB = 0x8C37;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //71
        //ARB_texture_cube_map_array
        public const uint TEXTURE_CUBE_MAP_ARRAY_ARB = 0x9009;
        public const uint TEXTURE_BINDING_CUBE_MAP_ARRAY_ARB = 0x900A;
        public const uint PROXY_TEXTURE_CUBE_MAP_ARRAY_ARB = 0x900B;
        public const uint SAMPLER_CUBE_MAP_ARRAY_ARB = 0x900C;
        public const uint SAMPLER_CUBE_MAP_ARRAY_SHADOW_ARB = 0x900D;
        public const uint INT_SAMPLER_CUBE_MAP_ARRAY_ARB = 0x900E;
        public const uint UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY_ARB = 0x900F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //72
        //ARB_texture_gather
        public const uint MIN_PROGRAM_TEXTURE_GATHER_OFFSET_ARB = 0x8E5E;
        public const uint MAX_PROGRAM_TEXTURE_GATHER_OFFSET_ARB = 0x8E5F;
        public const uint MAX_PROGRAM_TEXTURE_GATHER_COMPONENTS_ARB = 0x8F9F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //73
        //ARB_texture_query_lod

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //74 - WGL_ARB_create_context_profile
        //// ARB Extension //75 - GLX_ARB_create_context_profile

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //76
        //ARB_shading_language_include
        public const uint SHADER_INCLUDE_ARB = 0x8DAE;
        public const uint NAMED_STRING_LENGTH_ARB = 0x8DE9;
        public const uint NAMED_STRING_TYPE_ARB = 0x8DEA;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //77
        //ARB_texture_compression_bptc
        public const uint COMPRESSED_RGBA_BPTC_UNORM_ARB = 0x8E8C;
        public const uint COMPRESSED_SRGB_ALPHA_BPTC_UNORM_ARB = 0x8E8D;
        public const uint COMPRESSED_RGB_BPTC_SIGNED_FLOAT_ARB = 0x8E8E;
        public const uint COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT_ARB = 0x8E8F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //78
        //ARB_blend_func_extended
        public const uint SRC1_COLOR = 0x88F9;
        //    use VERSION_1_5			    SRC1_ALPHA
        public const uint ONE_MINUS_SRC1_COLOR = 0x88FA;
        public const uint ONE_MINUS_SRC1_ALPHA = 0x88FB;
        public const uint MAX_DUAL_SOURCE_DRAW_BUFFERS = 0x88FC;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //79
        //ARB_explicit_attrib_location

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //80
        //ARB_occlusion_query2
        public const uint ANY_SAMPLES_PASSED = 0x8C2F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //81
        //ARB_sampler_objects
        public const uint SAMPLER_BINDING = 0x8919;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //82
        //ARB_shader_bit_encoding

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //83
        //ARB_texture_rgb10_a2ui
        public const uint RGB10_A2UI = 0x906F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //84
        //ARB_texture_swizzle
        public const uint TEXTURE_SWIZZLE_R = 0x8E42;
        public const uint TEXTURE_SWIZZLE_G = 0x8E43;
        public const uint TEXTURE_SWIZZLE_B = 0x8E44;
        public const uint TEXTURE_SWIZZLE_A = 0x8E45;
        public const uint TEXTURE_SWIZZLE_RGBA = 0x8E46;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //85
        //ARB_timer_query
        public const uint TIME_ELAPSED = 0x88BF;
        public const uint TIMESTAMP = 0x8E28;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //86
        //ARB_vertex_type_2_10_10_10_rev
        //    use VERSION_1_2			    UNSIGNED_INT_2_10_10_10_REV
        public const uint INT_2_10_10_10_REV = 0x8D9F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //87
        //ARB_draw_indirect
        public const uint DRAW_INDIRECT_BUFFER = 0x8F3F;
        public const uint DRAW_INDIRECT_BUFFER_BINDING = 0x8F43;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //88
        //ARB_gpu_shader5
        public const uint GEOMETRY_SHADER_INVOCATIONS = 0x887F;
        public const uint MAX_GEOMETRY_SHADER_INVOCATIONS = 0x8E5A;
        public const uint MIN_FRAGMENT_INTERPOLATION_OFFSET = 0x8E5B;
        public const uint MAX_FRAGMENT_INTERPOLATION_OFFSET = 0x8E5C;
        public const uint FRAGMENT_INTERPOLATION_OFFSET_BITS = 0x8E5D;
        //    use ARB_transform_feedback3	    MAX_VERTEX_STREAMS

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //89
        //ARB_gpu_shader_fp64
        //    use VERSION_1_1			    DOUBLE
        public const uint DOUBLE_VEC2 = 0x8FFC;
        public const uint DOUBLE_VEC3 = 0x8FFD;
        public const uint DOUBLE_VEC4 = 0x8FFE;
        public const uint DOUBLE_MAT2 = 0x8F46;
        public const uint DOUBLE_MAT3 = 0x8F47;
        public const uint DOUBLE_MAT4 = 0x8F48;
        public const uint DOUBLE_MAT2x3 = 0x8F49;
        public const uint DOUBLE_MAT2x4 = 0x8F4A;
        public const uint DOUBLE_MAT3x2 = 0x8F4B;
        public const uint DOUBLE_MAT3x4 = 0x8F4C;
        public const uint DOUBLE_MAT4x2 = 0x8F4D;
        public const uint DOUBLE_MAT4x3 = 0x8F4E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //90
        //ARB_shader_subroutine
        public const uint ACTIVE_SUBROUTINES = 0x8DE5;
        public const uint ACTIVE_SUBROUTINE_UNIFORMS = 0x8DE6;
        public const uint ACTIVE_SUBROUTINE_UNIFORM_LOCATIONS = 0x8E47;
        public const uint ACTIVE_SUBROUTINE_MAX_LENGTH = 0x8E48;
        public const uint ACTIVE_SUBROUTINE_UNIFORM_MAX_LENGTH = 0x8E49;
        public const uint MAX_SUBROUTINES = 0x8DE7;
        public const uint MAX_SUBROUTINE_UNIFORM_LOCATIONS = 0x8DE8;
        public const uint NUM_COMPATIBLE_SUBROUTINES = 0x8E4A;
        public const uint COMPATIBLE_SUBROUTINES = 0x8E4B;
        //    use ARB_uniform_buffer_object	    UNIFORM_SIZE
        //    use ARB_uniform_buffer_object	    UNIFORM_NAME_LENGTH

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //91
        //ARB_tessellation_shader
        public const uint PATCHES = 0x000E;
        public const uint PATCH_VERTICES = 0x8E72;
        public const uint PATCH_DEFAULT_INNER_LEVEL = 0x8E73;
        public const uint PATCH_DEFAULT_OUTER_LEVEL = 0x8E74;
        public const uint TESS_CONTROL_OUTPUT_VERTICES = 0x8E75;
        public const uint TESS_GEN_MODE = 0x8E76;
        public const uint TESS_GEN_SPACING = 0x8E77;
        public const uint TESS_GEN_VERTEX_ORDER = 0x8E78;
        public const uint TESS_GEN_POINT_MODE = 0x8E79;
        //    use VERSION_1_1			    TRIANGLES
        //    use VERSION_1_1			    QUADS
        public const uint ISOLINES = 0x8E7A;
        //    use VERSION_1_1			    EQUAL
        public const uint FRACTIONAL_ODD = 0x8E7B;
        public const uint FRACTIONAL_EVEN = 0x8E7C;
        //    use VERSION_1_1			    CCW
        //    use VERSION_1_1			    CW
        public const uint MAX_PATCH_VERTICES = 0x8E7D;
        public const uint MAX_TESS_GEN_LEVEL = 0x8E7E;
        public const uint MAX_TESS_CONTROL_UNIFORM_COMPONENTS = 0x8E7F;
        public const uint MAX_TESS_EVALUATION_UNIFORM_COMPONENTS = 0x8E80;
        public const uint MAX_TESS_CONTROL_TEXTURE_IMAGE_UNITS = 0x8E81;
        public const uint MAX_TESS_EVALUATION_TEXTURE_IMAGE_UNITS = 0x8E82;
        public const uint MAX_TESS_CONTROL_OUTPUT_COMPONENTS = 0x8E83;
        public const uint MAX_TESS_PATCH_COMPONENTS = 0x8E84;
        public const uint MAX_TESS_CONTROL_TOTAL_OUTPUT_COMPONENTS = 0x8E85;
        public const uint MAX_TESS_EVALUATION_OUTPUT_COMPONENTS = 0x8E86;
        public const uint MAX_TESS_CONTROL_UNIFORM_BLOCKS = 0x8E89;
        public const uint MAX_TESS_EVALUATION_UNIFORM_BLOCKS = 0x8E8A;
        public const uint MAX_TESS_CONTROL_INPUT_COMPONENTS = 0x886C;
        public const uint MAX_TESS_EVALUATION_INPUT_COMPONENTS = 0x886D;
        public const uint MAX_COMBINED_TESS_CONTROL_UNIFORM_COMPONENTS = 0x8E1E;
        public const uint MAX_COMBINED_TESS_EVALUATION_UNIFORM_COMPONENTS = 0x8E1F;
        public const uint UNIFORM_BLOCK_REFERENCED_BY_TESS_CONTROL_SHADER = 0x84F0;
        public const uint UNIFORM_BLOCK_REFERENCED_BY_TESS_EVALUATION_SHADER = 0x84F1;
        public const uint TESS_EVALUATION_SHADER = 0x8E87;
        public const uint TESS_CONTROL_SHADER = 0x8E88;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //92
        //ARB_texture_buffer_object_rgb32
        //    use VERSION_3_0			    RGB32F
        //    use VERSION_3_0			    RGB32UI
        //    use VERSION_3_0			    RGB32I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //93
        //ARB_transform_feedback2
        public const uint TRANSFORM_FEEDBACK = 0x8E22;
        public const uint TRANSFORM_FEEDBACK_PAUSED = 0x8E23;
        public const uint TRANSFORM_FEEDBACK_BUFFER_PAUSED = 0x8E23;    // alias TRANSFORM_FEEDBACK_PAUSED
        public const uint TRANSFORM_FEEDBACK_ACTIVE = 0x8E24;
        public const uint TRANSFORM_FEEDBACK_BUFFER_ACTIVE = 0x8E24;    // alias TRANSFORM_FEEDBACK_ACTIVE
        public const uint TRANSFORM_FEEDBACK_BINDING = 0x8E25;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //94
        //ARB_transform_feedback3
        public const uint MAX_TRANSFORM_FEEDBACK_BUFFERS = 0x8E70;
        public const uint MAX_VERTEX_STREAMS = 0x8E71;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //95
        //ARB_ES2_compatibility
        public const uint FIXED = 0x140C;
        public const uint IMPLEMENTATION_COLOR_READ_TYPE = 0x8B9A;
        public const uint IMPLEMENTATION_COLOR_READ_FORMAT = 0x8B9B;
        public const uint LOW_FLOAT = 0x8DF0;
        public const uint MEDIUM_FLOAT = 0x8DF1;
        public const uint HIGH_FLOAT = 0x8DF2;
        public const uint LOW_INT = 0x8DF3;
        public const uint MEDIUM_INT = 0x8DF4;
        public const uint HIGH_INT = 0x8DF5;
        public const uint SHADER_COMPILER = 0x8DFA;
        public const uint SHADER_BINARY_FORMATS = 0x8DF8;
        public const uint NUM_SHADER_BINARY_FORMATS = 0x8DF9;
        public const uint MAX_VERTEX_UNIFORM_VECTORS = 0x8DFB;
        public const uint MAX_VARYING_VECTORS = 0x8DFC;
        public const uint MAX_FRAGMENT_UNIFORM_VECTORS = 0x8DFD;
        public const uint RGB565 = 0x8D62;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //96
        //ARB_get_program_binary
        public const uint PROGRAM_BINARY_RETRIEVABLE_HINT = 0x8257;
        public const uint PROGRAM_BINARY_LENGTH = 0x8741;
        public const uint NUM_PROGRAM_BINARY_FORMATS = 0x87FE;
        public const uint PROGRAM_BINARY_FORMATS = 0x87FF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //97
        //ARB_separate_shader_objects
        public const uint VERTEX_SHADER_BIT = 0x00000001;
        public const uint FRAGMENT_SHADER_BIT = 0x00000002;
        public const uint GEOMETRY_SHADER_BIT = 0x00000004;
        public const uint TESS_CONTROL_SHADER_BIT = 0x00000008;
        public const uint TESS_EVALUATION_SHADER_BIT = 0x00000010;
        public const uint ALL_SHADER_BITS = 0xFFFFFFFF;
        public const uint PROGRAM_SEPARABLE = 0x8258;
        public const uint ACTIVE_PROGRAM = 0x8259;
        public const uint PROGRAM_PIPELINE_BINDING = 0x825A;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //98
        //ARB_shader_precision

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //99
        //ARB_vertex_attrib_64bit
        //    use VERSION_3_0			    RGB32I
        //    use ARB_gpu_shader_fp64		    DOUBLE_VEC2
        //    use ARB_gpu_shader_fp64		    DOUBLE_VEC3
        //    use ARB_gpu_shader_fp64		    DOUBLE_VEC4
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT2
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT3
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT4
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT2x3
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT2x4
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT3x2
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT3x4
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT4x2
        //    use ARB_gpu_shader_fp64		    DOUBLE_MAT4x3

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //100
        //ARB_viewport_array
        //    use VERSION_1_1			    SCISSOR_BOX
        //    use VERSION_1_1			    VIEWPORT
        //    use VERSION_1_1			    DEPTH_RANGE
        //    use VERSION_1_1			    SCISSOR_TEST
        public const uint MAX_VIEWPORTS = 0x825B;
        public const uint VIEWPORT_SUBPIXEL_BITS = 0x825C;
        public const uint VIEWPORT_BOUNDS_RANGE = 0x825D;
        public const uint LAYER_PROVOKING_VERTEX = 0x825E;
        public const uint VIEWPORT_INDEX_PROVOKING_VERTEX = 0x825F;
        public const uint UNDEFINED_VERTEX = 0x8260;
        //    use ARB_provoking_vertex	    FIRST_VERTEX_CONVENTION
        //    use ARB_provoking_vertex	    LAST_VERTEX_CONVENTION
        //    use ARB_provoking_vertex	    PROVOKING_VERTEX

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //101 - GLX_ARB_create_context_robustness
        //// ARB Extension //102 - WGL_ARB_create_context_robustness

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //103
        //ARB_cl_event
        public const uint SYNC_CL_EVENT_ARB = 0x8240;
        public const uint SYNC_CL_EVENT_COMPLETE_ARB = 0x8241;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //104
        //ARB_debug_output
        public const uint DEBUG_OUTPUT_SYNCHRONOUS_ARB = 0x8242;
        public const uint DEBUG_NEXT_LOGGED_MESSAGE_LENGTH_ARB = 0x8243;
        public const uint DEBUG_CALLBACK_FUNCTION_ARB = 0x8244;
        public const uint DEBUG_CALLBACK_USER_PARAM_ARB = 0x8245;
        public const uint DEBUG_SOURCE_API_ARB = 0x8246;
        public const uint DEBUG_SOURCE_WINDOW_SYSTEM_ARB = 0x8247;
        public const uint DEBUG_SOURCE_SHADER_COMPILER_ARB = 0x8248;
        public const uint DEBUG_SOURCE_THIRD_PARTY_ARB = 0x8249;
        public const uint DEBUG_SOURCE_APPLICATION_ARB = 0x824A;
        public const uint DEBUG_SOURCE_OTHER_ARB = 0x824B;
        public const uint DEBUG_TYPE_ERROR_ARB = 0x824C;
        public const uint DEBUG_TYPE_DEPRECATED_BEHAVIOR_ARB = 0x824D;
        public const uint DEBUG_TYPE_UNDEFINED_BEHAVIOR_ARB = 0x824E;
        public const uint DEBUG_TYPE_PORTABILITY_ARB = 0x824F;
        public const uint DEBUG_TYPE_PERFORMANCE_ARB = 0x8250;
        public const uint DEBUG_TYPE_OTHER_ARB = 0x8251;
        public const uint MAX_DEBUG_MESSAGE_LENGTH_ARB = 0x9143;
        public const uint MAX_DEBUG_LOGGED_MESSAGES_ARB = 0x9144;
        public const uint DEBUG_LOGGED_MESSAGES_ARB = 0x9145;
        public const uint DEBUG_SEVERITY_HIGH_ARB = 0x9146;
        public const uint DEBUG_SEVERITY_MEDIUM_ARB = 0x9147;
        public const uint DEBUG_SEVERITY_LOW_ARB = 0x9148;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //105
        //ARB_robustness
        //    use VERSION_1_1			    NO_ERROR
        public const uint CONTEXT_FLAG_ROBUST_ACCESS_BIT_ARB = 0x00000004;
        public const uint LOSE_CONTEXT_ON_RESET_ARB = 0x8252;
        public const uint GUILTY_CONTEXT_RESET_ARB = 0x8253;
        public const uint INNOCENT_CONTEXT_RESET_ARB = 0x8254;
        public const uint UNKNOWN_CONTEXT_RESET_ARB = 0x8255;
        public const uint RESET_NOTIFICATION_STRATEGY_ARB = 0x8256;
        public const uint NO_RESET_NOTIFICATION_ARB = 0x8261;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //106
        //ARB_shader_stencil_export

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //107
        //ARB_base_instance

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //108
        //ARB_shading_language_420pack

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //109
        //ARB_transform_feedback_instanced

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //110
        //ARB_compressed_texture_pixel_storage
        public const uint UNPACK_COMPRESSED_BLOCK_WIDTH = 0x9127;
        public const uint UNPACK_COMPRESSED_BLOCK_HEIGHT = 0x9128;
        public const uint UNPACK_COMPRESSED_BLOCK_DEPTH = 0x9129;
        public const uint UNPACK_COMPRESSED_BLOCK_SIZE = 0x912A;
        public const uint PACK_COMPRESSED_BLOCK_WIDTH = 0x912B;
        public const uint PACK_COMPRESSED_BLOCK_HEIGHT = 0x912C;
        public const uint PACK_COMPRESSED_BLOCK_DEPTH = 0x912D;
        public const uint PACK_COMPRESSED_BLOCK_SIZE = 0x912E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //111
        //ARB_conservative_depth

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //112
        //ARB_internalformat_query
        public const uint NUM_SAMPLE_COUNTS = 0x9380;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //113
        //ARB_map_buffer_alignment
        public const uint MIN_MAP_BUFFER_ALIGNMENT = 0x90BC;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //114
        //ARB_shader_atomic_counters
        public const uint ATOMIC_COUNTER_BUFFER = 0x92C0;
        public const uint ATOMIC_COUNTER_BUFFER_BINDING = 0x92C1;
        public const uint ATOMIC_COUNTER_BUFFER_START = 0x92C2;
        public const uint ATOMIC_COUNTER_BUFFER_SIZE = 0x92C3;
        public const uint ATOMIC_COUNTER_BUFFER_DATA_SIZE = 0x92C4;
        public const uint ATOMIC_COUNTER_BUFFER_ACTIVE_ATOMIC_COUNTERS = 0x92C5;
        public const uint ATOMIC_COUNTER_BUFFER_ACTIVE_ATOMIC_COUNTER_INDICES = 0x92C6;
        public const uint ATOMIC_COUNTER_BUFFER_REFERENCED_BY_VERTEX_SHADER = 0x92C7;
        public const uint ATOMIC_COUNTER_BUFFER_REFERENCED_BY_TESS_CONTROL_SHADER = 0x92C8;
        public const uint ATOMIC_COUNTER_BUFFER_REFERENCED_BY_TESS_EVALUATION_SHADER = 0x92C9;
        public const uint ATOMIC_COUNTER_BUFFER_REFERENCED_BY_GEOMETRY_SHADER = 0x92CA;
        public const uint ATOMIC_COUNTER_BUFFER_REFERENCED_BY_FRAGMENT_SHADER = 0x92CB;
        public const uint MAX_VERTEX_ATOMIC_COUNTER_BUFFERS = 0x92CC;
        public const uint MAX_TESS_CONTROL_ATOMIC_COUNTER_BUFFERS = 0x92CD;
        public const uint MAX_TESS_EVALUATION_ATOMIC_COUNTER_BUFFERS = 0x92CE;
        public const uint MAX_GEOMETRY_ATOMIC_COUNTER_BUFFERS = 0x92CF;
        public const uint MAX_FRAGMENT_ATOMIC_COUNTER_BUFFERS = 0x92D0;
        public const uint MAX_COMBINED_ATOMIC_COUNTER_BUFFERS = 0x92D1;
        public const uint MAX_VERTEX_ATOMIC_COUNTERS = 0x92D2;
        public const uint MAX_TESS_CONTROL_ATOMIC_COUNTERS = 0x92D3;
        public const uint MAX_TESS_EVALUATION_ATOMIC_COUNTERS = 0x92D4;
        public const uint MAX_GEOMETRY_ATOMIC_COUNTERS = 0x92D5;
        public const uint MAX_FRAGMENT_ATOMIC_COUNTERS = 0x92D6;
        public const uint MAX_COMBINED_ATOMIC_COUNTERS = 0x92D7;
        public const uint MAX_ATOMIC_COUNTER_BUFFER_SIZE = 0x92D8;
        public const uint MAX_ATOMIC_COUNTER_BUFFER_BINDINGS = 0x92DC;
        public const uint ACTIVE_ATOMIC_COUNTER_BUFFERS = 0x92D9;
        public const uint UNIFORM_ATOMIC_COUNTER_BUFFER_INDEX = 0x92DA;
        public const uint UNSIGNED_INT_ATOMIC_COUNTER = 0x92DB;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //115
        //ARB_shader_image_load_store
        public const uint VERTEX_ATTRIB_ARRAY_BARRIER_BIT = 0x00000001;
        public const uint ELEMENT_ARRAY_BARRIER_BIT = 0x00000002;
        public const uint UNIFORM_BARRIER_BIT = 0x00000004;
        public const uint TEXTURE_FETCH_BARRIER_BIT = 0x00000008;
        public const uint SHADER_IMAGE_ACCESS_BARRIER_BIT = 0x00000020;
        public const uint COMMAND_BARRIER_BIT = 0x00000040;
        public const uint PIXEL_BUFFER_BARRIER_BIT = 0x00000080;
        public const uint TEXTURE_UPDATE_BARRIER_BIT = 0x00000100;
        public const uint BUFFER_UPDATE_BARRIER_BIT = 0x00000200;
        public const uint FRAMEBUFFER_BARRIER_BIT = 0x00000400;
        public const uint TRANSFORM_FEEDBACK_BARRIER_BIT = 0x00000800;
        public const uint ATOMIC_COUNTER_BARRIER_BIT = 0x00001000;
        public const uint ALL_BARRIER_BITS = 0xFFFFFFFF;
        public const uint MAX_IMAGE_UNITS = 0x8F38;
        public const uint MAX_COMBINED_IMAGE_UNITS_AND_FRAGMENT_OUTPUTS = 0x8F39;
        public const uint IMAGE_BINDING_NAME = 0x8F3A;
        public const uint IMAGE_BINDING_LEVEL = 0x8F3B;
        public const uint IMAGE_BINDING_LAYERED = 0x8F3C;
        public const uint IMAGE_BINDING_LAYER = 0x8F3D;
        public const uint IMAGE_BINDING_ACCESS = 0x8F3E;
        public const uint IMAGE_1D = 0x904C;
        public const uint IMAGE_2D = 0x904D;
        public const uint IMAGE_3D = 0x904E;
        public const uint IMAGE_2D_RECT = 0x904F;
        public const uint IMAGE_CUBE = 0x9050;
        public const uint IMAGE_BUFFER = 0x9051;
        public const uint IMAGE_1D_ARRAY = 0x9052;
        public const uint IMAGE_2D_ARRAY = 0x9053;
        public const uint IMAGE_CUBE_MAP_ARRAY = 0x9054;
        public const uint IMAGE_2D_MULTISAMPLE = 0x9055;
        public const uint IMAGE_2D_MULTISAMPLE_ARRAY = 0x9056;
        public const uint INT_IMAGE_1D = 0x9057;
        public const uint INT_IMAGE_2D = 0x9058;
        public const uint INT_IMAGE_3D = 0x9059;
        public const uint INT_IMAGE_2D_RECT = 0x905A;
        public const uint INT_IMAGE_CUBE = 0x905B;
        public const uint INT_IMAGE_BUFFER = 0x905C;
        public const uint INT_IMAGE_1D_ARRAY = 0x905D;
        public const uint INT_IMAGE_2D_ARRAY = 0x905E;
        public const uint INT_IMAGE_CUBE_MAP_ARRAY = 0x905F;
        public const uint INT_IMAGE_2D_MULTISAMPLE = 0x9060;
        public const uint INT_IMAGE_2D_MULTISAMPLE_ARRAY = 0x9061;
        public const uint UNSIGNED_INT_IMAGE_1D = 0x9062;
        public const uint UNSIGNED_INT_IMAGE_2D = 0x9063;
        public const uint UNSIGNED_INT_IMAGE_3D = 0x9064;
        public const uint UNSIGNED_INT_IMAGE_2D_RECT = 0x9065;
        public const uint UNSIGNED_INT_IMAGE_CUBE = 0x9066;
        public const uint UNSIGNED_INT_IMAGE_BUFFER = 0x9067;
        public const uint UNSIGNED_INT_IMAGE_1D_ARRAY = 0x9068;
        public const uint UNSIGNED_INT_IMAGE_2D_ARRAY = 0x9069;
        public const uint UNSIGNED_INT_IMAGE_CUBE_MAP_ARRAY = 0x906A;
        public const uint UNSIGNED_INT_IMAGE_2D_MULTISAMPLE = 0x906B;
        public const uint UNSIGNED_INT_IMAGE_2D_MULTISAMPLE_ARRAY = 0x906C;
        public const uint MAX_IMAGE_SAMPLES = 0x906D;
        public const uint IMAGE_BINDING_FORMAT = 0x906E;
        public const uint IMAGE_FORMAT_COMPATIBILITY_TYPE = 0x90C7;
        public const uint IMAGE_FORMAT_COMPATIBILITY_BY_SIZE = 0x90C8;
        public const uint IMAGE_FORMAT_COMPATIBILITY_BY_CLASS = 0x90C9;
        public const uint MAX_VERTEX_IMAGE_UNIFORMS = 0x90CA;
        public const uint MAX_TESS_CONTROL_IMAGE_UNIFORMS = 0x90CB;
        public const uint MAX_TESS_EVALUATION_IMAGE_UNIFORMS = 0x90CC;
        public const uint MAX_GEOMETRY_IMAGE_UNIFORMS = 0x90CD;
        public const uint MAX_FRAGMENT_IMAGE_UNIFORMS = 0x90CE;
        public const uint MAX_COMBINED_IMAGE_UNIFORMS = 0x90CF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //116
        //ARB_shading_language_packing

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //117
        //ARB_texture_storage
        public const uint TEXTURE_IMMUTABLE_FORMAT = 0x912F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //118
        //KHR_texture_compression_astc_ldr
        public const uint COMPRESSED_RGBA_ASTC_4x4_KHR = 0x93B0;
        public const uint COMPRESSED_RGBA_ASTC_5x4_KHR = 0x93B1;
        public const uint COMPRESSED_RGBA_ASTC_5x5_KHR = 0x93B2;
        public const uint COMPRESSED_RGBA_ASTC_6x5_KHR = 0x93B3;
        public const uint COMPRESSED_RGBA_ASTC_6x6_KHR = 0x93B4;
        public const uint COMPRESSED_RGBA_ASTC_8x5_KHR = 0x93B5;
        public const uint COMPRESSED_RGBA_ASTC_8x6_KHR = 0x93B6;
        public const uint COMPRESSED_RGBA_ASTC_8x8_KHR = 0x93B7;
        public const uint COMPRESSED_RGBA_ASTC_10x5_KHR = 0x93B8;
        public const uint COMPRESSED_RGBA_ASTC_10x6_KHR = 0x93B9;
        public const uint COMPRESSED_RGBA_ASTC_10x8_KHR = 0x93BA;
        public const uint COMPRESSED_RGBA_ASTC_10x10_KHR = 0x93BB;
        public const uint COMPRESSED_RGBA_ASTC_12x10_KHR = 0x93BC;
        public const uint COMPRESSED_RGBA_ASTC_12x12_KHR = 0x93BD;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_4x4_KHR = 0x93D0;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_5x4_KHR = 0x93D1;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_5x5_KHR = 0x93D2;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_6x5_KHR = 0x93D3;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_6x6_KHR = 0x93D4;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_8x5_KHR = 0x93D5;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_8x6_KHR = 0x93D6;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_8x8_KHR = 0x93D7;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_10x5_KHR = 0x93D8;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_10x6_KHR = 0x93D9;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_10x8_KHR = 0x93DA;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_10x10_KHR = 0x93DB;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_12x10_KHR = 0x93DC;
        public const uint COMPRESSED_SRGB8_ALPHA8_ASTC_12x12_KHR = 0x93DD;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //119
        //KHR_debug
        public const uint DEBUG_OUTPUT_SYNCHRONOUS = 0x8242;
        public const uint DEBUG_NEXT_LOGGED_MESSAGE_LENGTH = 0x8243;
        public const uint DEBUG_CALLBACK_FUNCTION = 0x8244;
        public const uint DEBUG_CALLBACK_USER_PARAM = 0x8245;
        public const uint DEBUG_SOURCE_API = 0x8246;
        public const uint DEBUG_SOURCE_WINDOW_SYSTEM = 0x8247;
        public const uint DEBUG_SOURCE_SHADER_COMPILER = 0x8248;
        public const uint DEBUG_SOURCE_THIRD_PARTY = 0x8249;
        public const uint DEBUG_SOURCE_APPLICATION = 0x824A;
        public const uint DEBUG_SOURCE_OTHER = 0x824B;
        public const uint DEBUG_TYPE_ERROR = 0x824C;
        public const uint DEBUG_TYPE_DEPRECATED_BEHAVIOR = 0x824D;
        public const uint DEBUG_TYPE_UNDEFINED_BEHAVIOR = 0x824E;
        public const uint DEBUG_TYPE_PORTABILITY = 0x824F;
        public const uint DEBUG_TYPE_PERFORMANCE = 0x8250;
        public const uint DEBUG_TYPE_OTHER = 0x8251;
        public const uint DEBUG_TYPE_MARKER = 0x8268;
        public const uint DEBUG_TYPE_PUSH_GROUP = 0x8269;
        public const uint DEBUG_TYPE_POP_GROUP = 0x826A;
        public const uint DEBUG_SEVERITY_NOTIFICATION = 0x826B;
        public const uint MAX_DEBUG_GROUP_STACK_DEPTH = 0x826C;
        public const uint DEBUG_GROUP_STACK_DEPTH = 0x826D;
        public const uint BUFFER = 0x82E0;
        public const uint SHADER = 0x82E1;
        public const uint PROGRAM = 0x82E2;
        public const uint QUERY = 0x82E3;
        public const uint PROGRAM_PIPELINE = 0x82E4;
        //public const uint SYNC = 0x82E5; no longer used in extension
        public const uint SAMPLER = 0x82E6;
        public const uint DISPLAY_LIST = 0x82E7;
        /* DISPLAY_LIST used in compatibility profile only */

        public const uint MAX_LABEL_LENGTH = 0x82E8;
        public const uint MAX_DEBUG_MESSAGE_LENGTH = 0x9143;
        public const uint MAX_DEBUG_LOGGED_MESSAGES = 0x9144;
        public const uint DEBUG_LOGGED_MESSAGES = 0x9145;
        public const uint DEBUG_SEVERITY_HIGH = 0x9146;
        public const uint DEBUG_SEVERITY_MEDIUM = 0x9147;
        public const uint DEBUG_SEVERITY_LOW = 0x9148;
        public const uint DEBUG_OUTPUT = 0x92E0;
        public const uint CONTEXT_FLAG_DEBUG_BIT = 0x00000002;
        //    use ErrorCode			    STACK_UNDERFLOW
        //    use ErrorCode			    STACK_OVERFLOW

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //120
        //ARB_arrays_of_arrays

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //121
        //ARB_clear_buffer_object

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //122
        //ARB_compute_shader
        public const uint COMPUTE_SHADER = 0x91B9;
        public const uint MAX_COMPUTE_UNIFORM_BLOCKS = 0x91BB;
        public const uint MAX_COMPUTE_TEXTURE_IMAGE_UNITS = 0x91BC;
        public const uint MAX_COMPUTE_IMAGE_UNIFORMS = 0x91BD;
        public const uint MAX_COMPUTE_SHARED_MEMORY_SIZE = 0x8262;
        public const uint MAX_COMPUTE_UNIFORM_COMPONENTS = 0x8263;
        public const uint MAX_COMPUTE_ATOMIC_COUNTER_BUFFERS = 0x8264;
        public const uint MAX_COMPUTE_ATOMIC_COUNTERS = 0x8265;
        public const uint MAX_COMBINED_COMPUTE_UNIFORM_COMPONENTS = 0x8266;
        public const uint MAX_COMPUTE_LOCAL_INVOCATIONS = 0x90EB;
        public const uint MAX_COMPUTE_WORK_GROUP_COUNT = 0x91BE;
        public const uint MAX_COMPUTE_WORK_GROUP_SIZE = 0x91BF;
        public const uint COMPUTE_LOCAL_WORK_SIZE = 0x8267;
        public const uint UNIFORM_BLOCK_REFERENCED_BY_COMPUTE_SHADER = 0x90EC;
        public const uint ATOMIC_COUNTER_BUFFER_REFERENCED_BY_COMPUTE_SHADER = 0x90ED;
        public const uint DISPATCH_INDIRECT_BUFFER = 0x90EE;
        public const uint DISPATCH_INDIRECT_BUFFER_BINDING = 0x90EF;
        public const uint COMPUTE_SHADER_BIT = 0x00000020;	// UseProgramStages <stage> bitfield

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //123
        //ARB_copy_image

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //124 (renumbered from 142)
        //ARB_texture_view
        public const uint TEXTURE_VIEW_MIN_LEVEL = 0x82DB;
        public const uint TEXTURE_VIEW_NUM_LEVELS = 0x82DC;
        public const uint TEXTURE_VIEW_MIN_LAYER = 0x82DD;
        public const uint TEXTURE_VIEW_NUM_LAYERS = 0x82DE;
        public const uint TEXTURE_IMMUTABLE_LEVELS = 0x82DF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //125 (renumbered from 143)
        //ARB_vertex_attrib_binding
        public const uint VERTEX_ATTRIB_BINDING = 0x82D4;
        public const uint VERTEX_ATTRIB_RELATIVE_OFFSET = 0x82D5;
        public const uint VERTEX_BINDING_DIVISOR = 0x82D6;
        public const uint VERTEX_BINDING_OFFSET = 0x82D7;
        public const uint VERTEX_BINDING_STRIDE = 0x82D8;
        public const uint MAX_VERTEX_ATTRIB_RELATIVE_OFFSET = 0x82D9;
        public const uint MAX_VERTEX_ATTRIB_BINDINGS = 0x82DA;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //126 (renumbered from 144)
        //ARB_robustness_isolation

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //127
        //ARB_ES3_compatibility
        public const uint COMPRESSED_RGB8_ETC2 = 0x9274;
        public const uint COMPRESSED_SRGB8_ETC2 = 0x9275;
        public const uint COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2 = 0x9276;
        public const uint COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2 = 0x9277;
        public const uint COMPRESSED_RGBA8_ETC2_EAC = 0x9278;
        public const uint COMPRESSED_SRGB8_ALPHA8_ETC2_EAC = 0x9279;
        public const uint COMPRESSED_R11_EAC = 0x9270;
        public const uint COMPRESSED_SIGNED_R11_EAC = 0x9271;
        public const uint COMPRESSED_RG11_EAC = 0x9272;
        public const uint COMPRESSED_SIGNED_RG11_EAC = 0x9273;
        public const uint PRIMITIVE_RESTART_FIXED_INDEX = 0x8D69;
        public const uint ANY_SAMPLES_PASSED_CONSERVATIVE = 0x8D6A;
        public const uint MAX_ELEMENT_INDEX = 0x8D6B;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //128
        //ARB_explicit_uniform_location
        public const uint MAX_UNIFORM_LOCATIONS = 0x826E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //129
        //ARB_fragment_layer_viewport

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //130
        //ARB_framebuffer_no_attachments
        public const uint FRAMEBUFFER_DEFAULT_WIDTH = 0x9310;
        public const uint FRAMEBUFFER_DEFAULT_HEIGHT = 0x9311;
        public const uint FRAMEBUFFER_DEFAULT_LAYERS = 0x9312;
        public const uint FRAMEBUFFER_DEFAULT_SAMPLES = 0x9313;
        public const uint FRAMEBUFFER_DEFAULT_FIXED_SAMPLE_LOCATIONS = 0x9314;
        public const uint MAX_FRAMEBUFFER_WIDTH = 0x9315;
        public const uint MAX_FRAMEBUFFER_HEIGHT = 0x9316;
        public const uint MAX_FRAMEBUFFER_LAYERS = 0x9317;
        public const uint MAX_FRAMEBUFFER_SAMPLES = 0x9318;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //131
        //ARB_internalformat_query2
        //    use ARB_shader_image_load_store     IMAGE_FORMAT_COMPATIBILITY_TYPE
        //    use ARB_internalformat_query	    NUM_SAMPLE_COUNTS
        //    use VERSION_3_0			    RENDERBUFFER
        //    use VERSION_1_3			    SAMPLES
        //    use GetPName			    TEXTURE_1D
        //    use VERSION_3_0			    TEXTURE_1D_ARRAY
        //    use GetPName			    TEXTURE_2D
        //    use VERSION_3_0			    TEXTURE_2D_ARRAY
        //    use VERSION_1_2			    TEXTURE_3D
        //    use VERSION_1_3			    TEXTURE_CUBE_MAP
        //    use ARB_texture_cube_map	    TEXTURE_CUBE_MAP_ARRAY
        //    use VERSION_3_1			    TEXTURE_RECTANGLE
        //    use VERSION_3_1			    TEXTURE_BUFFER
        //    use ARB_texture_multisample	    TEXTURE_2D_MULTISAMPLE
        //    use ARB_texture_multisample	    TEXTURE_2D_MULTISAMPLE_ARRAY
        //    use VERSION_1_3			    TEXTURE_COMPRESSED
        public const uint INTERNALFORMAT_SUPPORTED = 0x826F;
        public const uint INTERNALFORMAT_PREFERRED = 0x8270;
        public const uint INTERNALFORMAT_RED_SIZE = 0x8271;
        public const uint INTERNALFORMAT_GREEN_SIZE = 0x8272;
        public const uint INTERNALFORMAT_BLUE_SIZE = 0x8273;
        public const uint INTERNALFORMAT_ALPHA_SIZE = 0x8274;
        public const uint INTERNALFORMAT_DEPTH_SIZE = 0x8275;
        public const uint INTERNALFORMAT_STENCIL_SIZE = 0x8276;
        public const uint INTERNALFORMAT_SHARED_SIZE = 0x8277;
        public const uint INTERNALFORMAT_RED_TYPE = 0x8278;
        public const uint INTERNALFORMAT_GREEN_TYPE = 0x8279;
        public const uint INTERNALFORMAT_BLUE_TYPE = 0x827A;
        public const uint INTERNALFORMAT_ALPHA_TYPE = 0x827B;
        public const uint INTERNALFORMAT_DEPTH_TYPE = 0x827C;
        public const uint INTERNALFORMAT_STENCIL_TYPE = 0x827D;
        public const uint MAX_WIDTH = 0x827E;
        public const uint MAX_HEIGHT = 0x827F;
        public const uint MAX_DEPTH = 0x8280;
        public const uint MAX_LAYERS = 0x8281;
        public const uint MAX_COMBINED_DIMENSIONS = 0x8282;
        public const uint COLOR_COMPONENTS = 0x8283;
        public const uint DEPTH_COMPONENTS = 0x8284;
        public const uint STENCIL_COMPONENTS = 0x8285;
        public const uint COLOR_RENDERABLE = 0x8286;
        public const uint DEPTH_RENDERABLE = 0x8287;
        public const uint STENCIL_RENDERABLE = 0x8288;
        public const uint FRAMEBUFFER_RENDERABLE = 0x8289;
        public const uint FRAMEBUFFER_RENDERABLE_LAYERED = 0x828A;
        public const uint FRAMEBUFFER_BLEND = 0x828B;
        public const uint READ_PIXELS = 0x828C;
        public const uint READ_PIXELS_FORMAT = 0x828D;
        public const uint READ_PIXELS_TYPE = 0x828E;
        public const uint TEXTURE_IMAGE_FORMAT = 0x828F;
        public const uint TEXTURE_IMAGE_TYPE = 0x8290;
        public const uint GET_TEXTURE_IMAGE_FORMAT = 0x8291;
        public const uint GET_TEXTURE_IMAGE_TYPE = 0x8292;
        public const uint MIPMAP = 0x8293;
        public const uint MANUAL_GENERATE_MIPMAP = 0x8294;
        //// Should be deprecated
        public const uint AUTO_GENERATE_MIPMAP = 0x8295;
        public const uint COLOR_ENCODING = 0x8296;
        public const uint SRGB_READ = 0x8297;
        public const uint SRGB_WRITE = 0x8298;
        public const uint SRGB_DECODE_ARB = 0x8299;
        public const uint FILTER = 0x829A;
        public const uint VERTEX_TEXTURE = 0x829B;
        public const uint TESS_CONTROL_TEXTURE = 0x829C;
        public const uint TESS_EVALUATION_TEXTURE = 0x829D;
        public const uint GEOMETRY_TEXTURE = 0x829E;
        public const uint FRAGMENT_TEXTURE = 0x829F;
        public const uint COMPUTE_TEXTURE = 0x82A0;
        public const uint TEXTURE_SHADOW = 0x82A1;
        public const uint TEXTURE_GATHER = 0x82A2;
        public const uint TEXTURE_GATHER_SHADOW = 0x82A3;
        public const uint SHADER_IMAGE_LOAD = 0x82A4;
        public const uint SHADER_IMAGE_STORE = 0x82A5;
        public const uint SHADER_IMAGE_ATOMIC = 0x82A6;
        public const uint IMAGE_TEXEL_SIZE = 0x82A7;
        public const uint IMAGE_COMPATIBILITY_CLASS = 0x82A8;
        public const uint IMAGE_PIXEL_FORMAT = 0x82A9;
        public const uint IMAGE_PIXEL_TYPE = 0x82AA;
        public const uint SIMULTANEOUS_TEXTURE_AND_DEPTH_TEST = 0x82AC;
        public const uint SIMULTANEOUS_TEXTURE_AND_STENCIL_TEST = 0x82AD;
        public const uint SIMULTANEOUS_TEXTURE_AND_DEPTH_WRITE = 0x82AE;
        public const uint SIMULTANEOUS_TEXTURE_AND_STENCIL_WRITE = 0x82AF;
        public const uint TEXTURE_COMPRESSED_BLOCK_WIDTH = 0x82B1;
        public const uint TEXTURE_COMPRESSED_BLOCK_HEIGHT = 0x82B2;
        public const uint TEXTURE_COMPRESSED_BLOCK_SIZE = 0x82B3;
        public const uint CLEAR_BUFFER = 0x82B4;
        public const uint TEXTURE_VIEW = 0x82B5;
        public const uint VIEW_COMPATIBILITY_CLASS = 0x82B6;
        public const uint FULL_SUPPORT = 0x82B7;
        public const uint CAVEAT_SUPPORT = 0x82B8;
        public const uint IMAGE_CLASS_4_X_32 = 0x82B9;
        public const uint IMAGE_CLASS_2_X_32 = 0x82BA;
        public const uint IMAGE_CLASS_1_X_32 = 0x82BB;
        public const uint IMAGE_CLASS_4_X_16 = 0x82BC;
        public const uint IMAGE_CLASS_2_X_16 = 0x82BD;
        public const uint IMAGE_CLASS_1_X_16 = 0x82BE;
        public const uint IMAGE_CLASS_4_X_8 = 0x82BF;
        public const uint IMAGE_CLASS_2_X_8 = 0x82C0;
        public const uint IMAGE_CLASS_1_X_8 = 0x82C1;
        public const uint IMAGE_CLASS_11_11_10 = 0x82C2;
        public const uint IMAGE_CLASS_10_10_10_2 = 0x82C3;
        public const uint VIEW_CLASS_128_BITS = 0x82C4;
        public const uint VIEW_CLASS_96_BITS = 0x82C5;
        public const uint VIEW_CLASS_64_BITS = 0x82C6;
        public const uint VIEW_CLASS_48_BITS = 0x82C7;
        public const uint VIEW_CLASS_32_BITS = 0x82C8;
        public const uint VIEW_CLASS_24_BITS = 0x82C9;
        public const uint VIEW_CLASS_16_BITS = 0x82CA;
        public const uint VIEW_CLASS_8_BITS = 0x82CB;
        public const uint VIEW_CLASS_S3TC_DXT1_RGB = 0x82CC;
        public const uint VIEW_CLASS_S3TC_DXT1_RGBA = 0x82CD;
        public const uint VIEW_CLASS_S3TC_DXT3_RGBA = 0x82CE;
        public const uint VIEW_CLASS_S3TC_DXT5_RGBA = 0x82CF;
        public const uint VIEW_CLASS_RGTC1_RED = 0x82D0;
        public const uint VIEW_CLASS_RGTC2_RG = 0x82D1;
        public const uint VIEW_CLASS_BPTC_UNORM = 0x82D2;
        public const uint VIEW_CLASS_BPTC_FLOAT = 0x82D3;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //132
        //ARB_invalidate_subdata

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens ; might not exist as an ARB extension
        //// ARB Extension //133
        //ARB_multi_draw_indirect

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //134
        //ARB_program_interface_query
        public const uint UNIFORM = 0x92E1;
        public const uint UNIFORM_BLOCK = 0x92E2;
        public const uint PROGRAM_INPUT = 0x92E3;
        public const uint PROGRAM_OUTPUT = 0x92E4;
        public const uint BUFFER_VARIABLE = 0x92E5;
        public const uint SHADER_STORAGE_BLOCK = 0x92E6;
        //    use ARB_shader_atomic_counters	    ATOMIC_COUNTER_BUFFER
        public const uint VERTEX_SUBROUTINE = 0x92E8;
        public const uint TESS_CONTROL_SUBROUTINE = 0x92E9;
        public const uint TESS_EVALUATION_SUBROUTINE = 0x92EA;
        public const uint GEOMETRY_SUBROUTINE = 0x92EB;
        public const uint FRAGMENT_SUBROUTINE = 0x92EC;
        public const uint COMPUTE_SUBROUTINE = 0x92ED;
        public const uint VERTEX_SUBROUTINE_UNIFORM = 0x92EE;
        public const uint TESS_CONTROL_SUBROUTINE_UNIFORM = 0x92EF;
        public const uint TESS_EVALUATION_SUBROUTINE_UNIFORM = 0x92F0;
        public const uint GEOMETRY_SUBROUTINE_UNIFORM = 0x92F1;
        public const uint FRAGMENT_SUBROUTINE_UNIFORM = 0x92F2;
        public const uint COMPUTE_SUBROUTINE_UNIFORM = 0x92F3;
        public const uint TRANSFORM_FEEDBACK_VARYING = 0x92F4;
        public const uint ACTIVE_RESOURCES = 0x92F5;
        public const uint MAX_NAME_LENGTH = 0x92F6;
        public const uint MAX_NUM_ACTIVE_VARIABLES = 0x92F7;
        public const uint MAX_NUM_COMPATIBLE_SUBROUTINES = 0x92F8;
        public const uint NAME_LENGTH = 0x92F9;
        public const uint TYPE = 0x92FA;
        public const uint ARRAY_SIZE = 0x92FB;
        public const uint OFFSET = 0x92FC;
        public const uint BLOCK_INDEX = 0x92FD;
        public const uint ARRAY_STRIDE = 0x92FE;
        public const uint MATRIX_STRIDE = 0x92FF;
        public const uint IS_ROW_MAJOR = 0x9300;
        public const uint ATOMIC_COUNTER_BUFFER_INDEX = 0x9301;
        public const uint BUFFER_BINDING = 0x9302;
        public const uint BUFFER_DATA_SIZE = 0x9303;
        public const uint NUM_ACTIVE_VARIABLES = 0x9304;
        public const uint ACTIVE_VARIABLES = 0x9305;
        public const uint REFERENCED_BY_VERTEX_SHADER = 0x9306;
        public const uint REFERENCED_BY_TESS_CONTROL_SHADER = 0x9307;
        public const uint REFERENCED_BY_TESS_EVALUATION_SHADER = 0x9308;
        public const uint REFERENCED_BY_GEOMETRY_SHADER = 0x9309;
        public const uint REFERENCED_BY_FRAGMENT_SHADER = 0x930A;
        public const uint REFERENCED_BY_COMPUTE_SHADER = 0x930B;
        public const uint TOP_LEVEL_ARRAY_SIZE = 0x930C;
        public const uint TOP_LEVEL_ARRAY_STRIDE = 0x930D;
        public const uint LOCATION = 0x930E;
        public const uint LOCATION_INDEX = 0x930F;
        public const uint IS_PER_PATCH = 0x92E7;
        //    use ARB_shader_subroutine	    NUM_COMPATIBLE_SUBROUTINES
        //    use ARB_shader_subroutine	    COMPATIBLE_SUBROUTINES

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //135
        //ARB_robust_buffer_access_behavior

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //136
        //ARB_shader_image_size

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //137
        //ARB_shader_storage_buffer_object
        public const uint SHADER_STORAGE_BUFFER = 0x90D2;
        public const uint SHADER_STORAGE_BUFFER_BINDING = 0x90D3;
        public const uint SHADER_STORAGE_BUFFER_START = 0x90D4;
        public const uint SHADER_STORAGE_BUFFER_SIZE = 0x90D5;
        public const uint MAX_VERTEX_SHADER_STORAGE_BLOCKS = 0x90D6;
        public const uint MAX_GEOMETRY_SHADER_STORAGE_BLOCKS = 0x90D7;
        public const uint MAX_TESS_CONTROL_SHADER_STORAGE_BLOCKS = 0x90D8;
        public const uint MAX_TESS_EVALUATION_SHADER_STORAGE_BLOCKS = 0x90D9;
        public const uint MAX_FRAGMENT_SHADER_STORAGE_BLOCKS = 0x90DA;
        public const uint MAX_COMPUTE_SHADER_STORAGE_BLOCKS = 0x90DB;
        public const uint MAX_COMBINED_SHADER_STORAGE_BLOCKS = 0x90DC;
        public const uint MAX_SHADER_STORAGE_BUFFER_BINDINGS = 0x90DD;
        public const uint MAX_SHADER_STORAGE_BLOCK_SIZE = 0x90DE;
        public const uint SHADER_STORAGE_BUFFER_OFFSET_ALIGNMENT = 0x90DF;
        public const uint SHADER_STORAGE_BARRIER_BIT = 0x00002000;
        public const uint MAX_COMBINED_SHADER_OUTPUT_RESOURCES = 0x8F39;    // alias MAX_COMBINED_IMAGE_UNITS_AND_FRAGMENT_OUTPUTS
        //    use ARB_shader_image_load_store     MAX_COMBINED_IMAGE_UNITS_AND_FRAGMENT_OUTPUTS

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //138
        //ARB_stencil_texturing
        public const uint DEPTH_STENCIL_TEXTURE_MODE = 0x90EA;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// ARB Extension //139
        //ARB_texture_buffer_range
        public const uint TEXTURE_BUFFER_OFFSET = 0x919D;
        public const uint TEXTURE_BUFFER_SIZE = 0x919E;
        public const uint TEXTURE_BUFFER_OFFSET_ALIGNMENT = 0x919F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //140
        //ARB_texture_query_levels

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// ARB Extension //141
        //ARB_texture_storage_multisample

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //142 - GLX_ARB_robustness_application_isolation
        //// Extension //143 - WGL_ARB_robustness_application_isolation

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////
        //// Non-ARB extensions follow, in registry order
        ////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //1
        //EXT_abgr
        public const uint ABGR_EXT = 0x8000;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //2
        //EXT_blend_color
        public const uint CONSTANT_COLOR_EXT = 0x8001;
        public const uint ONE_MINUS_CONSTANT_COLOR_EXT = 0x8002;
        public const uint CONSTANT_ALPHA_EXT = 0x8003;
        public const uint ONE_MINUS_CONSTANT_ALPHA_EXT = 0x8004;
        public const uint BLEND_COLOR_EXT = 0x8005; // 4 F

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //3
        //EXT_polygon_offset
        public const uint POLYGON_OFFSET_EXT = 0x8037;
        public const uint POLYGON_OFFSET_FACTOR_EXT = 0x8038;
        public const uint POLYGON_OFFSET_BIAS_EXT = 0x8039; // 1 F

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //4
        //EXT_texture
        public const uint ALPHA4_EXT = 0x803B;
        public const uint ALPHA8_EXT = 0x803C;
        public const uint ALPHA12_EXT = 0x803D;
        public const uint ALPHA16_EXT = 0x803E;
        public const uint LUMINANCE4_EXT = 0x803F;
        public const uint LUMINANCE8_EXT = 0x8040;
        public const uint LUMINANCE12_EXT = 0x8041;
        public const uint LUMINANCE16_EXT = 0x8042;
        public const uint LUMINANCE4_ALPHA4_EXT = 0x8043;
        public const uint LUMINANCE6_ALPHA2_EXT = 0x8044;
        public const uint LUMINANCE8_ALPHA8_EXT = 0x8045;
        public const uint LUMINANCE12_ALPHA4_EXT = 0x8046;
        public const uint LUMINANCE12_ALPHA12_EXT = 0x8047;
        public const uint LUMINANCE16_ALPHA16_EXT = 0x8048;
        public const uint INTENSITY_EXT = 0x8049;
        public const uint INTENSITY4_EXT = 0x804A;
        public const uint INTENSITY8_EXT = 0x804B;
        public const uint INTENSITY12_EXT = 0x804C;
        public const uint INTENSITY16_EXT = 0x804D;
        public const uint RGB2_EXT = 0x804E;
        public const uint RGB4_EXT = 0x804F;
        public const uint RGB5_EXT = 0x8050;
        public const uint RGB8_EXT = 0x8051;
        public const uint RGB10_EXT = 0x8052;
        public const uint RGB12_EXT = 0x8053;
        public const uint RGB16_EXT = 0x8054;
        public const uint RGBA2_EXT = 0x8055;
        public const uint RGBA4_EXT = 0x8056;
        public const uint RGB5_A1_EXT = 0x8057;
        public const uint RGBA8_EXT = 0x8058;
        public const uint RGB10_A2_EXT = 0x8059;
        public const uint RGBA12_EXT = 0x805A;
        public const uint RGBA16_EXT = 0x805B;
        public const uint TEXTURE_RED_SIZE_EXT = 0x805C;
        public const uint TEXTURE_GREEN_SIZE_EXT = 0x805D;
        public const uint TEXTURE_BLUE_SIZE_EXT = 0x805E;
        public const uint TEXTURE_ALPHA_SIZE_EXT = 0x805F;
        public const uint TEXTURE_LUMINANCE_SIZE_EXT = 0x8060;
        public const uint TEXTURE_INTENSITY_SIZE_EXT = 0x8061;
        public const uint REPLACE_EXT = 0x8062;
        public const uint PROXY_TEXTURE_1D_EXT = 0x8063;
        public const uint PROXY_TEXTURE_2D_EXT = 0x8064;
        public const uint TEXTURE_TOO_LARGE_EXT = 0x8065;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //5 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //6
        //EXT_texture3D
        public const uint PACK_SKIP_IMAGES_EXT = 0x806B; // 1 I
        public const uint PACK_IMAGE_HEIGHT_EXT = 0x806C; // 1 F
        public const uint UNPACK_SKIP_IMAGES_EXT = 0x806D; // 1 I
        public const uint UNPACK_IMAGE_HEIGHT_EXT = 0x806E; // 1 F
        public const uint TEXTURE_3D_EXT = 0x806F; // 1 I
        public const uint PROXY_TEXTURE_3D_EXT = 0x8070;
        public const uint TEXTURE_DEPTH_EXT = 0x8071;
        public const uint TEXTURE_WRAP_R_EXT = 0x8072;
        public const uint MAX_3D_TEXTURE_SIZE_EXT = 0x8073; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //7
        //SGIS_texture_filter4
        public const uint FILTER4_SGIS = 0x8146;
        public const uint TEXTURE_FILTER4_SIZE_SGIS = 0x8147;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //8 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //9
        //EXT_subtexture

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //10
        //EXT_copy_texture

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //11
        //EXT_histogram
        public const uint HISTOGRAM_EXT = 0x8024; // 1 I
        public const uint PROXY_HISTOGRAM_EXT = 0x8025;
        public const uint HISTOGRAM_WIDTH_EXT = 0x8026;
        public const uint HISTOGRAM_FORMAT_EXT = 0x8027;
        public const uint HISTOGRAM_RED_SIZE_EXT = 0x8028;
        public const uint HISTOGRAM_GREEN_SIZE_EXT = 0x8029;
        public const uint HISTOGRAM_BLUE_SIZE_EXT = 0x802A;
        public const uint HISTOGRAM_ALPHA_SIZE_EXT = 0x802B;
        public const uint HISTOGRAM_LUMINANCE_SIZE_EXT = 0x802C;
        public const uint HISTOGRAM_SINK_EXT = 0x802D;
        public const uint MINMAX_EXT = 0x802E; // 1 I
        public const uint MINMAX_FORMAT_EXT = 0x802F;
        public const uint MINMAX_SINK_EXT = 0x8030;
        public const uint TABLE_TOO_LARGE_EXT = 0x8031;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //12
        //EXT_convolution
        public const uint CONVOLUTION_1D_EXT = 0x8010; // 1 I
        public const uint CONVOLUTION_2D_EXT = 0x8011; // 1 I
        public const uint SEPARABLE_2D_EXT = 0x8012; // 1 I
        public const uint CONVOLUTION_BORDER_MODE_EXT = 0x8013;
        public const uint CONVOLUTION_FILTER_SCALE_EXT = 0x8014;
        public const uint CONVOLUTION_FILTER_BIAS_EXT = 0x8015;
        public const uint REDUCE_EXT = 0x8016;
        public const uint CONVOLUTION_FORMAT_EXT = 0x8017;
        public const uint CONVOLUTION_WIDTH_EXT = 0x8018;
        public const uint CONVOLUTION_HEIGHT_EXT = 0x8019;
        public const uint MAX_CONVOLUTION_WIDTH_EXT = 0x801A;
        public const uint MAX_CONVOLUTION_HEIGHT_EXT = 0x801B;
        public const uint POST_CONVOLUTION_RED_SCALE_EXT = 0x801C; // 1 F
        public const uint POST_CONVOLUTION_GREEN_SCALE_EXT = 0x801D; // 1 F
        public const uint POST_CONVOLUTION_BLUE_SCALE_EXT = 0x801E; // 1 F
        public const uint POST_CONVOLUTION_ALPHA_SCALE_EXT = 0x801F; // 1 F
        public const uint POST_CONVOLUTION_RED_BIAS_EXT = 0x8020; // 1 F
        public const uint POST_CONVOLUTION_GREEN_BIAS_EXT = 0x8021; // 1 F
        public const uint POST_CONVOLUTION_BLUE_BIAS_EXT = 0x8022; // 1 F
        public const uint POST_CONVOLUTION_ALPHA_BIAS_EXT = 0x8023; // 1 F

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //13
        //SGI_color_matrix
        public const uint COLOR_MATRIX_SGI = 0x80B1; // 16 F
        public const uint COLOR_MATRIX_STACK_DEPTH_SGI = 0x80B2; // 1 I
        public const uint MAX_COLOR_MATRIX_STACK_DEPTH_SGI = 0x80B3; // 1 I
        public const uint POST_COLOR_MATRIX_RED_SCALE_SGI = 0x80B4; // 1 F
        public const uint POST_COLOR_MATRIX_GREEN_SCALE_SGI = 0x80B5; // 1 F
        public const uint POST_COLOR_MATRIX_BLUE_SCALE_SGI = 0x80B6; // 1 F
        public const uint POST_COLOR_MATRIX_ALPHA_SCALE_SGI = 0x80B7; // 1 F
        public const uint POST_COLOR_MATRIX_RED_BIAS_SGI = 0x80B8; // 1 F
        public const uint POST_COLOR_MATRIX_GREEN_BIAS_SGI = 0x80B9; // 1 F
        public const uint POST_COLOR_MATRIX_BLUE_BIAS_SGI = 0x80BA; // 1 F
        public const uint POST_COLOR_MATRIX_ALPHA_BIAS_SGI = 0x80BB; // 1 F

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //14
        //SGI_color_table
        public const uint COLOR_TABLE_SGI = 0x80D0; // 1 I
        public const uint POST_CONVOLUTION_COLOR_TABLE_SGI = 0x80D1; // 1 I
        public const uint POST_COLOR_MATRIX_COLOR_TABLE_SGI = 0x80D2; // 1 I
        public const uint PROXY_COLOR_TABLE_SGI = 0x80D3;
        public const uint PROXY_POST_CONVOLUTION_COLOR_TABLE_SGI = 0x80D4;
        public const uint PROXY_POST_COLOR_MATRIX_COLOR_TABLE_SGI = 0x80D5;
        public const uint COLOR_TABLE_SCALE_SGI = 0x80D6;
        public const uint COLOR_TABLE_BIAS_SGI = 0x80D7;
        public const uint COLOR_TABLE_FORMAT_SGI = 0x80D8;
        public const uint COLOR_TABLE_WIDTH_SGI = 0x80D9;
        public const uint COLOR_TABLE_RED_SIZE_SGI = 0x80DA;
        public const uint COLOR_TABLE_GREEN_SIZE_SGI = 0x80DB;
        public const uint COLOR_TABLE_BLUE_SIZE_SGI = 0x80DC;
        public const uint COLOR_TABLE_ALPHA_SIZE_SGI = 0x80DD;
        public const uint COLOR_TABLE_LUMINANCE_SIZE_SGI = 0x80DE;
        public const uint COLOR_TABLE_INTENSITY_SIZE_SGI = 0x80DF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //15
        //SGIS_pixel_texture
        public const uint PIXEL_TEXTURE_SGIS = 0x8353; // 1 I
        public const uint PIXEL_FRAGMENT_RGB_SOURCE_SGIS = 0x8354; // 1 I
        public const uint PIXEL_FRAGMENT_ALPHA_SOURCE_SGIS = 0x8355; // 1 I
        public const uint PIXEL_GROUP_COLOR_SGIS = 0x8356; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //15a
        //SGIX_pixel_texture
        public const uint PIXEL_TEX_GEN_SGIX = 0x8139; // 1 I
        public const uint PIXEL_TEX_GEN_MODE_SGIX = 0x832B; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //16
        //SGIS_texture4D
        public const uint PACK_SKIP_VOLUMES_SGIS = 0x8130; // 1 I
        public const uint PACK_IMAGE_DEPTH_SGIS = 0x8131; // 1 I
        public const uint UNPACK_SKIP_VOLUMES_SGIS = 0x8132; // 1 I
        public const uint UNPACK_IMAGE_DEPTH_SGIS = 0x8133; // 1 I
        public const uint TEXTURE_4D_SGIS = 0x8134; // 1 I
        public const uint PROXY_TEXTURE_4D_SGIS = 0x8135;
        public const uint TEXTURE_4DSIZE_SGIS = 0x8136;
        public const uint TEXTURE_WRAP_Q_SGIS = 0x8137;
        public const uint MAX_4D_TEXTURE_SIZE_SGIS = 0x8138; // 1 I
        public const uint TEXTURE_4D_BINDING_SGIS = 0x814F; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //17
        //SGI_texture_color_table
        public const uint TEXTURE_COLOR_TABLE_SGI = 0x80BC; // 1 I
        public const uint PROXY_TEXTURE_COLOR_TABLE_SGI = 0x80BD;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //18
        //EXT_cmyka
        public const uint CMYK_EXT = 0x800C;
        public const uint CMYKA_EXT = 0x800D;
        public const uint PACK_CMYK_HINT_EXT = 0x800E; // 1 I
        public const uint UNPACK_CMYK_HINT_EXT = 0x800F; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //19 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //20
        //EXT_texture_object
        public const uint TEXTURE_PRIORITY_EXT = 0x8066;
        public const uint TEXTURE_RESIDENT_EXT = 0x8067;
        public const uint TEXTURE_1D_BINDING_EXT = 0x8068;
        public const uint TEXTURE_2D_BINDING_EXT = 0x8069;
        public const uint TEXTURE_3D_BINDING_EXT = 0x806A; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //21
        //SGIS_detail_texture
        public const uint DETAIL_TEXTURE_2D_SGIS = 0x8095;
        public const uint DETAIL_TEXTURE_2D_BINDING_SGIS = 0x8096; // 1 I
        public const uint LINEAR_DETAIL_SGIS = 0x8097;
        public const uint LINEAR_DETAIL_ALPHA_SGIS = 0x8098;
        public const uint LINEAR_DETAIL_COLOR_SGIS = 0x8099;
        public const uint DETAIL_TEXTURE_LEVEL_SGIS = 0x809A;
        public const uint DETAIL_TEXTURE_MODE_SGIS = 0x809B;
        public const uint DETAIL_TEXTURE_FUNC_POINTS_SGIS = 0x809C;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //22
        //SGIS_sharpen_texture
        public const uint LINEAR_SHARPEN_SGIS = 0x80AD;
        public const uint LINEAR_SHARPEN_ALPHA_SGIS = 0x80AE;
        public const uint LINEAR_SHARPEN_COLOR_SGIS = 0x80AF;
        public const uint SHARPEN_TEXTURE_FUNC_POINTS_SGIS = 0x80B0;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //23
        //EXT_packed_pixels
        public const uint UNSIGNED_BYTE_3_3_2_EXT = 0x8032;
        public const uint UNSIGNED_SHORT_4_4_4_4_EXT = 0x8033;
        public const uint UNSIGNED_SHORT_5_5_5_1_EXT = 0x8034;
        public const uint UNSIGNED_INT_8_8_8_8_EXT = 0x8035;
        public const uint UNSIGNED_INT_10_10_10_2_EXT = 0x8036;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //24
        //SGIS_texture_lod
        public const uint TEXTURE_MIN_LOD_SGIS = 0x813A;
        public const uint TEXTURE_MAX_LOD_SGIS = 0x813B;
        public const uint TEXTURE_BASE_LEVEL_SGIS = 0x813C;
        public const uint TEXTURE_MAX_LEVEL_SGIS = 0x813D;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //25
        //SGIS_multisample
        public const uint MULTISAMPLE_SGIS = 0x809D; // 1 I
        public const uint SAMPLE_ALPHA_TO_MASK_SGIS = 0x809E; // 1 I
        public const uint SAMPLE_ALPHA_TO_ONE_SGIS = 0x809F; // 1 I
        public const uint SAMPLE_MASK_SGIS = 0x80A0; // 1 I
        //public const uint 1PASS_SGIS = 0x80A1;
        //public const uint 2PASS_0_SGIS = 0x80A2;
        //public const uint 2PASS_1_SGIS = 0x80A3;
        //public const uint 4PASS_0_SGIS = 0x80A4;
        //public const uint 4PASS_1_SGIS = 0x80A5;
        //public const uint 4PASS_2_SGIS = 0x80A6;
        //public const uint 4PASS_3_SGIS = 0x80A7;
        public const uint SAMPLE_BUFFERS_SGIS = 0x80A8; // 1 I
        public const uint SAMPLES_SGIS = 0x80A9; // 1 I
        public const uint SAMPLE_MASK_VALUE_SGIS = 0x80AA; // 1 F
        public const uint SAMPLE_MASK_INVERT_SGIS = 0x80AB; // 1 I
        public const uint SAMPLE_PATTERN_SGIS = 0x80AC; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //26 - no specification?
        //// SGIS_premultiply_blend

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //27
        //// Diamond ships an otherwise identical IBM_rescale_normal extension;
        ////  Dan Brokenshire says this is deprecated and should not be advertised.
        //EXT_rescale_normal
        public const uint RESCALE_NORMAL_EXT = 0x803A; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //28 - GLX_EXT_visual_info

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //29 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //30
        //EXT_vertex_array
        public const uint VERTEX_ARRAY_EXT = 0x8074;
        public const uint NORMAL_ARRAY_EXT = 0x8075;
        public const uint COLOR_ARRAY_EXT = 0x8076;
        public const uint INDEX_ARRAY_EXT = 0x8077;
        public const uint TEXTURE_COORD_ARRAY_EXT = 0x8078;
        public const uint EDGE_FLAG_ARRAY_EXT = 0x8079;
        public const uint VERTEX_ARRAY_SIZE_EXT = 0x807A;
        public const uint VERTEX_ARRAY_TYPE_EXT = 0x807B;
        public const uint VERTEX_ARRAY_STRIDE_EXT = 0x807C;
        public const uint VERTEX_ARRAY_COUNT_EXT = 0x807D; // 1 I
        public const uint NORMAL_ARRAY_TYPE_EXT = 0x807E;
        public const uint NORMAL_ARRAY_STRIDE_EXT = 0x807F;
        public const uint NORMAL_ARRAY_COUNT_EXT = 0x8080; // 1 I
        public const uint COLOR_ARRAY_SIZE_EXT = 0x8081;
        public const uint COLOR_ARRAY_TYPE_EXT = 0x8082;
        public const uint COLOR_ARRAY_STRIDE_EXT = 0x8083;
        public const uint COLOR_ARRAY_COUNT_EXT = 0x8084; // 1 I
        public const uint INDEX_ARRAY_TYPE_EXT = 0x8085;
        public const uint INDEX_ARRAY_STRIDE_EXT = 0x8086;
        public const uint INDEX_ARRAY_COUNT_EXT = 0x8087; // 1 I
        public const uint TEXTURE_COORD_ARRAY_SIZE_EXT = 0x8088;
        public const uint TEXTURE_COORD_ARRAY_TYPE_EXT = 0x8089;
        public const uint TEXTURE_COORD_ARRAY_STRIDE_EXT = 0x808A;
        public const uint TEXTURE_COORD_ARRAY_COUNT_EXT = 0x808B; // 1 I
        public const uint EDGE_FLAG_ARRAY_STRIDE_EXT = 0x808C;
        public const uint EDGE_FLAG_ARRAY_COUNT_EXT = 0x808D; // 1 I
        public const uint VERTEX_ARRAY_POINTER_EXT = 0x808E;
        public const uint NORMAL_ARRAY_POINTER_EXT = 0x808F;
        public const uint COLOR_ARRAY_POINTER_EXT = 0x8090;
        public const uint INDEX_ARRAY_POINTER_EXT = 0x8091;
        public const uint TEXTURE_COORD_ARRAY_POINTER_EXT = 0x8092;
        public const uint EDGE_FLAG_ARRAY_POINTER_EXT = 0x8093;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //31
        //EXT_misc_attribute
        //public const uint MISC_BIT = 0x;????

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //32
        //SGIS_generate_mipmap
        public const uint GENERATE_MIPMAP_SGIS = 0x8191;
        public const uint GENERATE_MIPMAP_HINT_SGIS = 0x8192; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //33
        //SGIX_clipmap
        public const uint LINEAR_CLIPMAP_LINEAR_SGIX = 0x8170;
        public const uint TEXTURE_CLIPMAP_CENTER_SGIX = 0x8171;
        public const uint TEXTURE_CLIPMAP_FRAME_SGIX = 0x8172;
        public const uint TEXTURE_CLIPMAP_OFFSET_SGIX = 0x8173;
        public const uint TEXTURE_CLIPMAP_VIRTUAL_DEPTH_SGIX = 0x8174;
        public const uint TEXTURE_CLIPMAP_LOD_OFFSET_SGIX = 0x8175;
        public const uint TEXTURE_CLIPMAP_DEPTH_SGIX = 0x8176;
        public const uint MAX_CLIPMAP_DEPTH_SGIX = 0x8177; // 1 I
        public const uint MAX_CLIPMAP_VIRTUAL_DEPTH_SGIX = 0x8178; // 1 I
        public const uint NEAREST_CLIPMAP_NEAREST_SGIX = 0x844D;
        public const uint NEAREST_CLIPMAP_LINEAR_SGIX = 0x844E;
        public const uint LINEAR_CLIPMAP_NEAREST_SGIX = 0x844F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //34
        //SGIX_shadow
        public const uint TEXTURE_COMPARE_SGIX = 0x819A;
        public const uint TEXTURE_COMPARE_OPERATOR_SGIX = 0x819B;
        public const uint TEXTURE_LEQUAL_R_SGIX = 0x819C;
        public const uint TEXTURE_GEQUAL_R_SGIX = 0x819D;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //35
        //SGIS_texture_edge_clamp
        public const uint CLAMP_TO_EDGE_SGIS = 0x812F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //36
        //// Promoted to ARB_texture_border_clamp
        //SGIS_texture_border_clamp
        public const uint CLAMP_TO_BORDER_SGIS = 0x812D;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //37
        //EXT_blend_minmax
        public const uint FUNC_ADD_EXT = 0x8006;
        public const uint MIN_EXT = 0x8007;
        public const uint MAX_EXT = 0x8008;
        public const uint BLEND_EQUATION_EXT = 0x8009; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //38
        //EXT_blend_subtract
        public const uint FUNC_SUBTRACT_EXT = 0x800A;
        public const uint FUNC_REVERSE_SUBTRACT_EXT = 0x800B;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //39
        //EXT_blend_logic_op

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //40 - GLX_SGI_swap_control
        //// Extension //41 - GLX_SGI_video_sync
        //// Extension //42 - GLX_SGI_make_current_read
        //// Extension //43 - GLX_SGIX_video_source
        //// Extension //44 - GLX_EXT_visual_rating

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //45
        //SGIX_interlace
        public const uint INTERLACE_SGIX = 0x8094; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //46
        //SGIX_pixel_tiles
        public const uint PIXEL_TILE_BEST_ALIGNMENT_SGIX = 0x813E; // 1 I
        public const uint PIXEL_TILE_CACHE_INCREMENT_SGIX = 0x813F; // 1 I
        public const uint PIXEL_TILE_WIDTH_SGIX = 0x8140; // 1 I
        public const uint PIXEL_TILE_HEIGHT_SGIX = 0x8141; // 1 I
        public const uint PIXEL_TILE_GRID_WIDTH_SGIX = 0x8142; // 1 I
        public const uint PIXEL_TILE_GRID_HEIGHT_SGIX = 0x8143; // 1 I
        public const uint PIXEL_TILE_GRID_DEPTH_SGIX = 0x8144; // 1 I
        public const uint PIXEL_TILE_CACHE_SIZE_SGIX = 0x8145; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //47 - GLX_EXT_import_context

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //48 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //49 - GLX_SGIX_fbconfig
        //// Extension //50 - GLX_SGIX_pbuffer

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //51
        //SGIS_texture_select
        public const uint DUAL_ALPHA4_SGIS = 0x8110;
        public const uint DUAL_ALPHA8_SGIS = 0x8111;
        public const uint DUAL_ALPHA12_SGIS = 0x8112;
        public const uint DUAL_ALPHA16_SGIS = 0x8113;
        public const uint DUAL_LUMINANCE4_SGIS = 0x8114;
        public const uint DUAL_LUMINANCE8_SGIS = 0x8115;
        public const uint DUAL_LUMINANCE12_SGIS = 0x8116;
        public const uint DUAL_LUMINANCE16_SGIS = 0x8117;
        public const uint DUAL_INTENSITY4_SGIS = 0x8118;
        public const uint DUAL_INTENSITY8_SGIS = 0x8119;
        public const uint DUAL_INTENSITY12_SGIS = 0x811A;
        public const uint DUAL_INTENSITY16_SGIS = 0x811B;
        public const uint DUAL_LUMINANCE_ALPHA4_SGIS = 0x811C;
        public const uint DUAL_LUMINANCE_ALPHA8_SGIS = 0x811D;
        public const uint QUAD_ALPHA4_SGIS = 0x811E;
        public const uint QUAD_ALPHA8_SGIS = 0x811F;
        public const uint QUAD_LUMINANCE4_SGIS = 0x8120;
        public const uint QUAD_LUMINANCE8_SGIS = 0x8121;
        public const uint QUAD_INTENSITY4_SGIS = 0x8122;
        public const uint QUAD_INTENSITY8_SGIS = 0x8123;
        public const uint DUAL_TEXTURE_SELECT_SGIS = 0x8124;
        public const uint QUAD_TEXTURE_SELECT_SGIS = 0x8125;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //52
        //SGIX_sprite
        public const uint SPRITE_SGIX = 0x8148; // 1 I
        public const uint SPRITE_MODE_SGIX = 0x8149; // 1 I
        public const uint SPRITE_AXIS_SGIX = 0x814A; // 3 F
        public const uint SPRITE_TRANSLATION_SGIX = 0x814B; // 3 F
        public const uint SPRITE_AXIAL_SGIX = 0x814C;
        public const uint SPRITE_OBJECT_ALIGNED_SGIX = 0x814D;
        public const uint SPRITE_EYE_ALIGNED_SGIX = 0x814E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //53
        //SGIX_texture_multi_buffer
        public const uint TEXTURE_MULTI_BUFFER_HINT_SGIX = 0x812E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //54
        //// EXT form promoted from SGIS form; both are included
        //EXT_point_parameters
        public const uint POINT_SIZE_MIN_EXT = 0x8126; // 1 F
        public const uint POINT_SIZE_MAX_EXT = 0x8127; // 1 F
        public const uint POINT_FADE_THRESHOLD_SIZE_EXT = 0x8128; // 1 F
        public const uint DISTANCE_ATTENUATION_EXT = 0x8129; // 3 F

        //SGIS_point_parameters
        public const uint POINT_SIZE_MIN_SGIS = 0x8126; // 1 F
        public const uint POINT_SIZE_MAX_SGIS = 0x8127; // 1 F
        public const uint POINT_FADE_THRESHOLD_SIZE_SGIS = 0x8128; // 1 F
        public const uint DISTANCE_ATTENUATION_SGIS = 0x8129; // 3 F

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //55
        //SGIX_instruments
        public const uint INSTRUMENT_BUFFER_POINTER_SGIX = 0x8180;
        public const uint INSTRUMENT_MEASUREMENTS_SGIX = 0x8181; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //56
        //SGIX_texture_scale_bias
        public const uint POST_TEXTURE_FILTER_BIAS_SGIX = 0x8179;
        public const uint POST_TEXTURE_FILTER_SCALE_SGIX = 0x817A;
        public const uint POST_TEXTURE_FILTER_BIAS_RANGE_SGIX = 0x817B; // 2 F
        public const uint POST_TEXTURE_FILTER_SCALE_RANGE_SGIX = 0x817C; // 2 F

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //57
        //SGIX_framezoom
        public const uint FRAMEZOOM_SGIX = 0x818B; // 1 I
        public const uint FRAMEZOOM_FACTOR_SGIX = 0x818C; // 1 I
        public const uint MAX_FRAMEZOOM_FACTOR_SGIX = 0x818D; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //58
        //SGIX_tag_sample_buffer

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //59
        //SGIX_polynomial_ffd
        public const uint TEXTURE_DEFORMATION_BIT_SGIX = 0x00000001;
        public const uint GEOMETRY_DEFORMATION_BIT_SGIX = 0x00000002;
        public const uint GEOMETRY_DEFORMATION_SGIX = 0x8194;
        public const uint TEXTURE_DEFORMATION_SGIX = 0x8195;
        public const uint DEFORMATIONS_MASK_SGIX = 0x8196; // 1 I
        public const uint MAX_DEFORMATION_ORDER_SGIX = 0x8197;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //60
        //SGIX_reference_plane
        public const uint REFERENCE_PLANE_SGIX = 0x817D; // 1 I
        public const uint REFERENCE_PLANE_EQUATION_SGIX = 0x817E; // 4 F

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //61
        //SGIX_flush_raster

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //62 - GLX_SGIX_cushion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //63
        //SGIX_depth_texture
        public const uint DEPTH_COMPONENT16_SGIX = 0x81A5;
        public const uint DEPTH_COMPONENT24_SGIX = 0x81A6;
        public const uint DEPTH_COMPONENT32_SGIX = 0x81A7;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //64
        //SGIS_fog_function
        public const uint FOG_FUNC_SGIS = 0x812A;
        public const uint FOG_FUNC_POINTS_SGIS = 0x812B; // 1 I
        public const uint MAX_FOG_FUNC_POINTS_SGIS = 0x812C; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //65
        //SGIX_fog_offset
        public const uint FOG_OFFSET_SGIX = 0x8198; // 1 I
        public const uint FOG_OFFSET_VALUE_SGIX = 0x8199; // 4 F

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //66
        //HP_image_transform
        public const uint IMAGE_SCALE_X_HP = 0x8155;
        public const uint IMAGE_SCALE_Y_HP = 0x8156;
        public const uint IMAGE_TRANSLATE_X_HP = 0x8157;
        public const uint IMAGE_TRANSLATE_Y_HP = 0x8158;
        public const uint IMAGE_ROTATE_ANGLE_HP = 0x8159;
        public const uint IMAGE_ROTATE_ORIGIN_X_HP = 0x815A;
        public const uint IMAGE_ROTATE_ORIGIN_Y_HP = 0x815B;
        public const uint IMAGE_MAG_FILTER_HP = 0x815C;
        public const uint IMAGE_MIN_FILTER_HP = 0x815D;
        public const uint IMAGE_CUBIC_WEIGHT_HP = 0x815E;
        public const uint CUBIC_HP = 0x815F;
        public const uint AVERAGE_HP = 0x8160;
        public const uint IMAGE_TRANSFORM_2D_HP = 0x8161;
        public const uint POST_IMAGE_TRANSFORM_COLOR_TABLE_HP = 0x8162;
        public const uint PROXY_POST_IMAGE_TRANSFORM_COLOR_TABLE_HP = 0x8163;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //67
        //HP_convolution_border_modes
        public const uint IGNORE_BORDER_HP = 0x8150;
        public const uint CONSTANT_BORDER_HP = 0x8151;
        public const uint REPLICATE_BORDER_HP = 0x8153;
        public const uint CONVOLUTION_BORDER_COLOR_HP = 0x8154;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //68
        //// (Unknown token values???)
        //INGR_palette_buffer

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //69
        //SGIX_texture_add_env
        public const uint TEXTURE_ENV_BIAS_SGIX = 0x80BE;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //70 - skipped
        //// Extension //71 - skipped
        //// Extension //72 - skipped
        //// Extension //73 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //74
        //EXT_color_subtable

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //75 - GLU_EXT_object_space_tess

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //76
        //PGI_vertex_hints
        public const uint VERTEX_DATA_HINT_PGI = 0x1A22A;
        public const uint VERTEX_CONSISTENT_HINT_PGI = 0x1A22B;
        public const uint MATERIAL_SIDE_HINT_PGI = 0x1A22C;
        public const uint MAX_VERTEX_HINT_PGI = 0x1A22D;
        public const uint VERTEX23_BIT_PGI = 0x00000004;
        public const uint VERTEX4_BIT_PGI = 0x00000008;
        public const uint COLOR3_BIT_PGI = 0x00010000;
        public const uint COLOR4_BIT_PGI = 0x00020000;
        public const uint EDGEFLAG_BIT_PGI = 0x00040000;
        public const uint INDEX_BIT_PGI = 0x00080000;
        public const uint MAT_AMBIENT_BIT_PGI = 0x00100000;
        public const uint MAT_AMBIENT_AND_DIFFUSE_BIT_PGI = 0x00200000;
        public const uint MAT_DIFFUSE_BIT_PGI = 0x00400000;
        public const uint MAT_EMISSION_BIT_PGI = 0x00800000;
        public const uint MAT_COLOR_INDEXES_BIT_PGI = 0x01000000;
        public const uint MAT_SHININESS_BIT_PGI = 0x02000000;
        public const uint MAT_SPECULAR_BIT_PGI = 0x04000000;
        public const uint NORMAL_BIT_PGI = 0x08000000;
        public const uint TEXCOORD1_BIT_PGI = 0x10000000;
        public const uint TEXCOORD2_BIT_PGI = 0x20000000;
        public const uint TEXCOORD3_BIT_PGI = 0x40000000;
        public const uint TEXCOORD4_BIT_PGI = 0x80000000;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //77
        //PGI_misc_hints
        public const uint PREFER_DOUBLEBUFFER_HINT_PGI = 0x1A1F8;
        public const uint CONSERVE_MEMORY_HINT_PGI = 0x1A1FD;
        public const uint RECLAIM_MEMORY_HINT_PGI = 0x1A1FE;
        public const uint NATIVE_GRAPHICS_HANDLE_PGI = 0x1A202;
        public const uint NATIVE_GRAPHICS_BEGIN_HINT_PGI = 0x1A203;
        public const uint NATIVE_GRAPHICS_END_HINT_PGI = 0x1A204;
        public const uint ALWAYS_FAST_HINT_PGI = 0x1A20C;
        public const uint ALWAYS_SOFT_HINT_PGI = 0x1A20D;
        public const uint ALLOW_DRAW_OBJ_HINT_PGI = 0x1A20E;
        public const uint ALLOW_DRAW_WIN_HINT_PGI = 0x1A20F;
        public const uint ALLOW_DRAW_FRG_HINT_PGI = 0x1A210;
        public const uint ALLOW_DRAW_MEM_HINT_PGI = 0x1A211;
        public const uint STRICT_DEPTHFUNC_HINT_PGI = 0x1A216;
        public const uint STRICT_LIGHTING_HINT_PGI = 0x1A217;
        public const uint STRICT_SCISSOR_HINT_PGI = 0x1A218;
        public const uint FULL_STIPPLE_HINT_PGI = 0x1A219;
        public const uint CLIP_NEAR_HINT_PGI = 0x1A220;
        public const uint CLIP_FAR_HINT_PGI = 0x1A221;
        public const uint WIDE_LINE_HINT_PGI = 0x1A222;
        public const uint BACK_NORMALS_HINT_PGI = 0x1A223;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //78
        //EXT_paletted_texture
        public const uint COLOR_INDEX1_EXT = 0x80E2;
        public const uint COLOR_INDEX2_EXT = 0x80E3;
        public const uint COLOR_INDEX4_EXT = 0x80E4;
        public const uint COLOR_INDEX8_EXT = 0x80E5;
        public const uint COLOR_INDEX12_EXT = 0x80E6;
        public const uint COLOR_INDEX16_EXT = 0x80E7;
        public const uint TEXTURE_INDEX_SIZE_EXT = 0x80ED;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //79
        //EXT_clip_volume_hint
        public const uint CLIP_VOLUME_CLIPPING_HINT_EXT = 0x80F0;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //80
        //SGIX_list_priority
        public const uint LIST_PRIORITY_SGIX = 0x8182;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //81
        //SGIX_ir_instrument1
        public const uint IR_INSTRUMENT1_SGIX = 0x817F; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //82
        //SGIX_calligraphic_fragment
        public const uint CALLIGRAPHIC_FRAGMENT_SGIX = 0x8183; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //83 - GLX_SGIX_video_resize

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //84
        //SGIX_texture_lod_bias
        public const uint TEXTURE_LOD_BIAS_S_SGIX = 0x818E;
        public const uint TEXTURE_LOD_BIAS_T_SGIX = 0x818F;
        public const uint TEXTURE_LOD_BIAS_R_SGIX = 0x8190;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //85 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //86 - GLX_SGIX_dmbuffer

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //87 - skipped
        //// Extension //88 - skipped
        //// Extension //89 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //90
        //SGIX_shadow_ambient
        public const uint SHADOW_AMBIENT_SGIX = 0x80BF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //91 - GLX_SGIX_swap_group
        //// Extension //92 - GLX_SGIX_swap_barrier

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //93
        //EXT_index_texture

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //94
        //// Promoted from SGI?
        //EXT_index_material
        public const uint INDEX_MATERIAL_EXT = 0x81B8;
        public const uint INDEX_MATERIAL_PARAMETER_EXT = 0x81B9;
        public const uint INDEX_MATERIAL_FACE_EXT = 0x81BA;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //95
        //// Promoted from SGI?
        //EXT_index_func
        public const uint INDEX_TEST_EXT = 0x81B5;
        public const uint INDEX_TEST_FUNC_EXT = 0x81B6;
        public const uint INDEX_TEST_REF_EXT = 0x81B7;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //96
        //// Promoted from SGI?
        //EXT_index_array_formats
        public const uint IUI_V2F_EXT = 0x81AD;
        public const uint IUI_V3F_EXT = 0x81AE;
        public const uint IUI_N3F_V2F_EXT = 0x81AF;
        public const uint IUI_N3F_V3F_EXT = 0x81B0;
        public const uint T2F_IUI_V2F_EXT = 0x81B1;
        public const uint T2F_IUI_V3F_EXT = 0x81B2;
        public const uint T2F_IUI_N3F_V2F_EXT = 0x81B3;
        public const uint T2F_IUI_N3F_V3F_EXT = 0x81B4;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //97
        //// Promoted from SGI?
        //EXT_compiled_vertex_array
        public const uint ARRAY_ELEMENT_LOCK_FIRST_EXT = 0x81A8;
        public const uint ARRAY_ELEMENT_LOCK_COUNT_EXT = 0x81A9;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //98
        //// Promoted from SGI?
        //EXT_cull_vertex
        public const uint CULL_VERTEX_EXT = 0x81AA;
        public const uint CULL_VERTEX_EYE_POSITION_EXT = 0x81AB;
        public const uint CULL_VERTEX_OBJECT_POSITION_EXT = 0x81AC;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //99 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //100 - GLU_EXT_nurbs_tessellator

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //101
        //SGIX_ycrcb
        public const uint YCRCB_422_SGIX = 0x81BB;
        public const uint YCRCB_444_SGIX = 0x81BC;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //102
        //SGIX_fragment_lighting
        public const uint FRAGMENT_LIGHTING_SGIX = 0x8400; // 1 I
        public const uint FRAGMENT_COLOR_MATERIAL_SGIX = 0x8401; // 1 I
        public const uint FRAGMENT_COLOR_MATERIAL_FACE_SGIX = 0x8402; // 1 I
        public const uint FRAGMENT_COLOR_MATERIAL_PARAMETER_SGIX = 0x8403; // 1 I
        public const uint MAX_FRAGMENT_LIGHTS_SGIX = 0x8404; // 1 I
        public const uint MAX_ACTIVE_LIGHTS_SGIX = 0x8405; // 1 I
        public const uint CURRENT_RASTER_NORMAL_SGIX = 0x8406; // 1 I
        public const uint LIGHT_ENV_MODE_SGIX = 0x8407; // 1 I
        public const uint FRAGMENT_LIGHT_MODEL_LOCAL_VIEWER_SGIX = 0x8408; // 1 I
        public const uint FRAGMENT_LIGHT_MODEL_TWO_SIDE_SGIX = 0x8409; // 1 I
        public const uint FRAGMENT_LIGHT_MODEL_AMBIENT_SGIX = 0x840A; // 4 F
        public const uint FRAGMENT_LIGHT_MODEL_NORMAL_INTERPOLATION_SGIX = 0x840B; // 1 I
        public const uint FRAGMENT_LIGHT0_SGIX = 0x840C; // 1 I
        public const uint FRAGMENT_LIGHT1_SGIX = 0x840D;
        public const uint FRAGMENT_LIGHT2_SGIX = 0x840E;
        public const uint FRAGMENT_LIGHT3_SGIX = 0x840F;
        public const uint FRAGMENT_LIGHT4_SGIX = 0x8410;
        public const uint FRAGMENT_LIGHT5_SGIX = 0x8411;
        public const uint FRAGMENT_LIGHT6_SGIX = 0x8412;
        public const uint FRAGMENT_LIGHT7_SGIX = 0x8413;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //103 - skipped
        //// Extension //104 - skipped
        //// Extension //105 - skipped
        //// Extension //106 - skipped
        //// Extension //107 - skipped
        //// Extension //108 - skipped
        //// Extension //109 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //110
        //IBM_rasterpos_clip
        public const uint RASTER_POSITION_UNCLIPPED_IBM = 0x19262;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //111
        //HP_texture_lighting
        public const uint TEXTURE_LIGHTING_MODE_HP = 0x8167;
        public const uint TEXTURE_POST_SPECULAR_HP = 0x8168;
        public const uint TEXTURE_PRE_SPECULAR_HP = 0x8169;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //112
        //EXT_draw_range_elements
        public const uint MAX_ELEMENTS_VERTICES_EXT = 0x80E8;
        public const uint MAX_ELEMENTS_INDICES_EXT = 0x80E9;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //113
        //WIN_phong_shading
        public const uint PHONG_WIN = 0x80EA;
        public const uint PHONG_HINT_WIN = 0x80EB;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //114
        //WIN_specular_fog
        public const uint FOG_SPECULAR_TEXTURE_WIN = 0x80EC;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //115 - skipped
        //// Extension //116 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //117
        //EXT_light_texture
        public const uint FRAGMENT_MATERIAL_EXT = 0x8349;
        public const uint FRAGMENT_NORMAL_EXT = 0x834A;
        public const uint FRAGMENT_COLOR_EXT = 0x834C;
        public const uint ATTENUATION_EXT = 0x834D;
        public const uint SHADOW_ATTENUATION_EXT = 0x834E;
        public const uint TEXTURE_APPLICATION_MODE_EXT = 0x834F; // 1 I
        public const uint TEXTURE_LIGHT_EXT = 0x8350; // 1 I
        public const uint TEXTURE_MATERIAL_FACE_EXT = 0x8351; // 1 I
        public const uint TEXTURE_MATERIAL_PARAMETER_EXT = 0x8352; // 1 I
        //    use EXT_fog_coord		    FRAGMENT_DEPTH_EXT

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //118 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //119
        //SGIX_blend_alpha_minmax
        public const uint ALPHA_MIN_SGIX = 0x8320;
        public const uint ALPHA_MAX_SGIX = 0x8321;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //120 - skipped
        //// Extension //121 - skipped
        //// Extension //122 - skipped
        //// Extension //123 - skipped
        //// Extension //124 - skipped
        //// Extension //125 - skipped
        //// Extension //126 - skipped (some enums used to be in glext.h, but this
        ////   was an incomplete SGI extension that never actually shipped).
        //// Extension //127 - skipped
        //// Extension //128 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //129
        //EXT_bgra
        public const uint BGR_EXT = 0x80E0;
        public const uint BGRA_EXT = 0x80E1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //130 - skipped
        //// Extension //131 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //132
        //SGIX_async
        public const uint ASYNC_MARKER_SGIX = 0x8329;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //133
        //SGIX_async_pixel
        public const uint ASYNC_TEX_IMAGE_SGIX = 0x835C;
        public const uint ASYNC_DRAW_PIXELS_SGIX = 0x835D;
        public const uint ASYNC_READ_PIXELS_SGIX = 0x835E;
        public const uint MAX_ASYNC_TEX_IMAGE_SGIX = 0x835F;
        public const uint MAX_ASYNC_DRAW_PIXELS_SGIX = 0x8360;
        public const uint MAX_ASYNC_READ_PIXELS_SGIX = 0x8361;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //134
        //SGIX_async_histogram
        public const uint ASYNC_HISTOGRAM_SGIX = 0x832C;
        public const uint MAX_ASYNC_HISTOGRAM_SGIX = 0x832D;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Intel has not implemented this; enums never assigned
        //// Extension //135
        //INTEL_texture_scissor
        //public const uint TEXTURE_SCISSOR_INTEL = 0x;????
        //public const uint TEXTURE_SCISSOR_INTEL = 0x;????
        //public const uint TEXTURE_SCISSOR_FUNC_INTEL = 0x;????
        //public const uint TEXTURE_SCISSOR_S_INTEL = 0x;????
        //public const uint TEXTURE_SCISSOR_T_INTEL = 0x;????
        //public const uint TEXTURE_SCISSOR_R_INTEL = 0x;????

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //136
        //INTEL_parallel_arrays
        public const uint PARALLEL_ARRAYS_INTEL = 0x83F4;
        public const uint VERTEX_ARRAY_PARALLEL_POINTERS_INTEL = 0x83F5;
        public const uint NORMAL_ARRAY_PARALLEL_POINTERS_INTEL = 0x83F6;
        public const uint COLOR_ARRAY_PARALLEL_POINTERS_INTEL = 0x83F7;
        public const uint TEXTURE_COORD_ARRAY_PARALLEL_POINTERS_INTEL = 0x83F8;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //137
        //HP_occlusion_test
        public const uint OCCLUSION_TEST_HP = 0x8165;
        public const uint OCCLUSION_TEST_RESULT_HP = 0x8166;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //138
        //EXT_pixel_transform
        public const uint PIXEL_TRANSFORM_2D_EXT = 0x8330;
        public const uint PIXEL_MAG_FILTER_EXT = 0x8331;
        public const uint PIXEL_MIN_FILTER_EXT = 0x8332;
        public const uint PIXEL_CUBIC_WEIGHT_EXT = 0x8333;
        public const uint CUBIC_EXT = 0x8334;
        public const uint AVERAGE_EXT = 0x8335;
        public const uint PIXEL_TRANSFORM_2D_STACK_DEPTH_EXT = 0x8336;
        public const uint MAX_PIXEL_TRANSFORM_2D_STACK_DEPTH_EXT = 0x8337;
        public const uint PIXEL_TRANSFORM_2D_MATRIX_EXT = 0x8338;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Unknown enum values
        //// Extension //139
        //EXT_pixel_transform_color_table

        ////	 PIXEL_TRANSFORM_COLOR_TABLE_EXT
        ////	 PROXY_PIXEL_TRANSFORM_COLOR_TABLE_EXT

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //140 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //141
        //EXT_shared_texture_palette
        public const uint SHARED_TEXTURE_PALETTE_EXT = 0x81FB;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //142 - GLX_SGIS_blended_overlay

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //143 - SGIS_shared_multisample
        //public const uint MULTISAMPLE_SUB_RECT_POSITION_SGIS = <TBD>
        //public const uint MULTISAMPLE_SUB_RECT_DIMS_SGIS = <TBD>

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //144
        //EXT_separate_specular_color
        public const uint LIGHT_MODEL_COLOR_CONTROL_EXT = 0x81F8;
        public const uint SINGLE_COLOR_EXT = 0x81F9;
        public const uint SEPARATE_SPECULAR_COLOR_EXT = 0x81FA;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //145
        //EXT_secondary_color
        public const uint COLOR_SUM_EXT = 0x8458; // 1 I
        public const uint CURRENT_SECONDARY_COLOR_EXT = 0x8459; // 3 F
        public const uint SECONDARY_COLOR_ARRAY_SIZE_EXT = 0x845A; // 1 I
        public const uint SECONDARY_COLOR_ARRAY_TYPE_EXT = 0x845B; // 1 I
        public const uint SECONDARY_COLOR_ARRAY_STRIDE_EXT = 0x845C; // 1 I
        public const uint SECONDARY_COLOR_ARRAY_POINTER_EXT = 0x845D;
        public const uint SECONDARY_COLOR_ARRAY_EXT = 0x845E; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Dead extension - EXT_texture_env_combine was finished instead
        //// Extension //146
        ////EXT_texture_env

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //147
        //EXT_texture_perturb_normal
        public const uint PERTURB_EXT = 0x85AE;
        public const uint TEXTURE_NORMAL_EXT = 0x85AF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //148
        //// Diamond ships an otherwise identical IBM_multi_draw_arrays extension;
        ////  Dan Brokenshire says this is deprecated and should not be advertised.
        //EXT_multi_draw_arrays

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //149
        //EXT_fog_coord
        public const uint FOG_COORDINATE_SOURCE_EXT = 0x8450; // 1 I
        public const uint FOG_COORDINATE_EXT = 0x8451;
        public const uint FRAGMENT_DEPTH_EXT = 0x8452;
        public const uint CURRENT_FOG_COORDINATE_EXT = 0x8453; // 1 F
        public const uint FOG_COORDINATE_ARRAY_TYPE_EXT = 0x8454; // 1 I
        public const uint FOG_COORDINATE_ARRAY_STRIDE_EXT = 0x8455; // 1 I
        public const uint FOG_COORDINATE_ARRAY_POINTER_EXT = 0x8456;
        public const uint FOG_COORDINATE_ARRAY_EXT = 0x8457; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //150 - skipped
        //// Extension //151 - skipped
        //// Extension //152 - skipped
        //// Extension //153 - skipped
        //// Extension //154 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //155
        //REND_screen_coordinates
        public const uint SCREEN_COORDINATES_REND = 0x8490;
        public const uint INVERTED_SCREEN_W_REND = 0x8491;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //156
        //EXT_coordinate_frame
        public const uint TANGENT_ARRAY_EXT = 0x8439;
        public const uint BINORMAL_ARRAY_EXT = 0x843A;
        public const uint CURRENT_TANGENT_EXT = 0x843B;
        public const uint CURRENT_BINORMAL_EXT = 0x843C;
        public const uint TANGENT_ARRAY_TYPE_EXT = 0x843E;
        public const uint TANGENT_ARRAY_STRIDE_EXT = 0x843F;
        public const uint BINORMAL_ARRAY_TYPE_EXT = 0x8440;
        public const uint BINORMAL_ARRAY_STRIDE_EXT = 0x8441;
        public const uint TANGENT_ARRAY_POINTER_EXT = 0x8442;
        public const uint BINORMAL_ARRAY_POINTER_EXT = 0x8443;
        public const uint MAP1_TANGENT_EXT = 0x8444;
        public const uint MAP2_TANGENT_EXT = 0x8445;
        public const uint MAP1_BINORMAL_EXT = 0x8446;
        public const uint MAP2_BINORMAL_EXT = 0x8447;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //157 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //158
        //EXT_texture_env_combine
        public const uint COMBINE_EXT = 0x8570;
        public const uint COMBINE_RGB_EXT = 0x8571;
        public const uint COMBINE_ALPHA_EXT = 0x8572;
        public const uint RGB_SCALE_EXT = 0x8573;
        public const uint ADD_SIGNED_EXT = 0x8574;
        public const uint INTERPOLATE_EXT = 0x8575;
        public const uint CONSTANT_EXT = 0x8576;
        public const uint PRIMARY_COLOR_EXT = 0x8577;
        public const uint PREVIOUS_EXT = 0x8578;
        public const uint SOURCE0_RGB_EXT = 0x8580;
        public const uint SOURCE1_RGB_EXT = 0x8581;
        public const uint SOURCE2_RGB_EXT = 0x8582;
        public const uint SOURCE0_ALPHA_EXT = 0x8588;
        public const uint SOURCE1_ALPHA_EXT = 0x8589;
        public const uint SOURCE2_ALPHA_EXT = 0x858A;
        public const uint OPERAND0_RGB_EXT = 0x8590;
        public const uint OPERAND1_RGB_EXT = 0x8591;
        public const uint OPERAND2_RGB_EXT = 0x8592;
        public const uint OPERAND0_ALPHA_EXT = 0x8598;
        public const uint OPERAND1_ALPHA_EXT = 0x8599;
        public const uint OPERAND2_ALPHA_EXT = 0x859A;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //159
        //APPLE_specular_vector
        public const uint LIGHT_MODEL_SPECULAR_VECTOR_APPLE = 0x85B0;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //160
        //APPLE_transform_hint
        public const uint TRANSFORM_HINT_APPLE = 0x85B1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //161 - skipped
        //// Extension //162 - skipped
        //// (some enums used to be in glext.h, but these were incomplete SGI
        ////  extensions that never actually shipped).

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //163
        //SUNX_constant_data
        public const uint UNPACK_CONSTANT_DATA_SUNX = 0x81D5;
        public const uint TEXTURE_CONSTANT_DATA_SUNX = 0x81D6;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //164
        //SUN_global_alpha
        public const uint GLOBAL_ALPHA_SUN = 0x81D9;
        public const uint GLOBAL_ALPHA_FACTOR_SUN = 0x81DA;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //165
        //SUN_triangle_list
        public const uint RESTART_SUN = 0x0001;
        public const uint REPLACE_MIDDLE_SUN = 0x0002;
        public const uint REPLACE_OLDEST_SUN = 0x0003;
        public const uint TRIANGLE_LIST_SUN = 0x81D7;
        public const uint REPLACEMENT_CODE_SUN = 0x81D8;
        public const uint REPLACEMENT_CODE_ARRAY_SUN = 0x85C0;
        public const uint REPLACEMENT_CODE_ARRAY_TYPE_SUN = 0x85C1;
        public const uint REPLACEMENT_CODE_ARRAY_STRIDE_SUN = 0x85C2;
        public const uint REPLACEMENT_CODE_ARRAY_POINTER_SUN = 0x85C3;
        public const uint R1UI_V3F_SUN = 0x85C4;
        public const uint R1UI_C4UB_V3F_SUN = 0x85C5;
        public const uint R1UI_C3F_V3F_SUN = 0x85C6;
        public const uint R1UI_N3F_V3F_SUN = 0x85C7;
        public const uint R1UI_C4F_N3F_V3F_SUN = 0x85C8;
        public const uint R1UI_T2F_V3F_SUN = 0x85C9;
        public const uint R1UI_T2F_N3F_V3F_SUN = 0x85CA;
        public const uint R1UI_T2F_C4F_N3F_V3F_SUN = 0x85CB;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //166
        //SUN_vertex

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //167 - WGL_EXT_display_color_table
        //// Extension //168 - WGL_EXT_extensions_string
        //// Extension //169 - WGL_EXT_make_current_read
        //// Extension //170 - WGL_EXT_pixel_format
        //// Extension //171 - WGL_EXT_pbuffer
        //// Extension //172 - WGL_EXT_swap_control

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //173
        //EXT_blend_func_separate
        public const uint BLEND_DST_RGB_EXT = 0x80C8;
        public const uint BLEND_SRC_RGB_EXT = 0x80C9;
        public const uint BLEND_DST_ALPHA_EXT = 0x80CA;
        public const uint BLEND_SRC_ALPHA_EXT = 0x80CB;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //174
        //INGR_color_clamp
        public const uint RED_MIN_CLAMP_INGR = 0x8560;
        public const uint GREEN_MIN_CLAMP_INGR = 0x8561;
        public const uint BLUE_MIN_CLAMP_INGR = 0x8562;
        public const uint ALPHA_MIN_CLAMP_INGR = 0x8563;
        public const uint RED_MAX_CLAMP_INGR = 0x8564;
        public const uint GREEN_MAX_CLAMP_INGR = 0x8565;
        public const uint BLUE_MAX_CLAMP_INGR = 0x8566;
        public const uint ALPHA_MAX_CLAMP_INGR = 0x8567;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //175
        //INGR_interlace_read
        public const uint INTERLACE_READ_INGR = 0x8568;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //176
        //EXT_stencil_wrap
        public const uint INCR_WRAP_EXT = 0x8507;
        public const uint DECR_WRAP_EXT = 0x8508;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //177 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //178
        //EXT_422_pixels
        //public const uint 422_EXT = 0x80CC;
        //public const uint 422_REV_EXT = 0x80CD;
        //public const uint 422_AVERAGE_EXT = 0x80CE;
        //public const uint 422_REV_AVERAGE_EXT = 0x80CF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //179
        //NV_texgen_reflection
        public const uint NORMAL_MAP_NV = 0x8511;
        public const uint REFLECTION_MAP_NV = 0x8512;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //180 - skipped
        //// Extension //181 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Is this shipping? No extension number assigned.
        //// Extension //?
        //EXT_texture_cube_map
        public const uint NORMAL_MAP_EXT = 0x8511;
        public const uint REFLECTION_MAP_EXT = 0x8512;
        public const uint TEXTURE_CUBE_MAP_EXT = 0x8513;
        public const uint TEXTURE_BINDING_CUBE_MAP_EXT = 0x8514;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_X_EXT = 0x8515;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_X_EXT = 0x8516;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_Y_EXT = 0x8517;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_Y_EXT = 0x8518;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_Z_EXT = 0x8519;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_Z_EXT = 0x851A;
        public const uint PROXY_TEXTURE_CUBE_MAP_EXT = 0x851B;
        public const uint MAX_CUBE_MAP_TEXTURE_SIZE_EXT = 0x851C;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //182
        //SUN_convolution_border_modes
        public const uint WRAP_BORDER_SUN = 0x81D4;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //183 - GLX_SUN_transparent_index

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //184 - skipped

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //185
        //EXT_texture_env_add

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //186
        //EXT_texture_lod_bias
        public const uint MAX_TEXTURE_LOD_BIAS_EXT = 0x84FD;
        public const uint TEXTURE_FILTER_CONTROL_EXT = 0x8500;
        public const uint TEXTURE_LOD_BIAS_EXT = 0x8501;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //187
        //EXT_texture_filter_anisotropic
        public const uint TEXTURE_MAX_ANISOTROPY_EXT = 0x84FE;
        public const uint MAX_TEXTURE_MAX_ANISOTROPY_EXT = 0x84FF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //188
        //EXT_vertex_weighting
        public const uint MODELVIEW0_STACK_DEPTH_EXT = 0x0BA3; // GL_MODELVIEW_STACK_DEPTH
        public const uint MODELVIEW1_STACK_DEPTH_EXT = 0x8502;
        public const uint MODELVIEW0_MATRIX_EXT = 0x0BA6; // GL_MODELVIEW_MATRIX
        public const uint MODELVIEW1_MATRIX_EXT = 0x8506;
        public const uint VERTEX_WEIGHTING_EXT = 0x8509;
        public const uint MODELVIEW0_EXT = 0x1700; // GL_MODELVIEW
        public const uint MODELVIEW1_EXT = 0x850A;
        public const uint CURRENT_VERTEX_WEIGHT_EXT = 0x850B;
        public const uint VERTEX_WEIGHT_ARRAY_EXT = 0x850C;
        public const uint VERTEX_WEIGHT_ARRAY_SIZE_EXT = 0x850D;
        public const uint VERTEX_WEIGHT_ARRAY_TYPE_EXT = 0x850E;
        public const uint VERTEX_WEIGHT_ARRAY_STRIDE_EXT = 0x850F;
        public const uint VERTEX_WEIGHT_ARRAY_POINTER_EXT = 0x8510;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //189
        //NV_light_max_exponent
        public const uint MAX_SHININESS_NV = 0x8504;
        public const uint MAX_SPOT_EXPONENT_NV = 0x8505;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //190
        //NV_vertex_array_range
        public const uint VERTEX_ARRAY_RANGE_NV = 0x851D;
        public const uint VERTEX_ARRAY_RANGE_LENGTH_NV = 0x851E;
        public const uint VERTEX_ARRAY_RANGE_VALID_NV = 0x851F;
        public const uint MAX_VERTEX_ARRAY_RANGE_ELEMENT_NV = 0x8520;
        public const uint VERTEX_ARRAY_RANGE_POINTER_NV = 0x8521;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //191
        //NV_register_combiners
        public const uint REGISTER_COMBINERS_NV = 0x8522;
        public const uint VARIABLE_A_NV = 0x8523;
        public const uint VARIABLE_B_NV = 0x8524;
        public const uint VARIABLE_C_NV = 0x8525;
        public const uint VARIABLE_D_NV = 0x8526;
        public const uint VARIABLE_E_NV = 0x8527;
        public const uint VARIABLE_F_NV = 0x8528;
        public const uint VARIABLE_G_NV = 0x8529;
        public const uint CONSTANT_COLOR0_NV = 0x852A;
        public const uint CONSTANT_COLOR1_NV = 0x852B;
        public const uint PRIMARY_COLOR_NV = 0x852C;
        public const uint SECONDARY_COLOR_NV = 0x852D;
        public const uint SPARE0_NV = 0x852E;
        public const uint SPARE1_NV = 0x852F;
        public const uint DISCARD_NV = 0x8530;
        public const uint E_TIMES_F_NV = 0x8531;
        public const uint SPARE0_PLUS_SECONDARY_COLOR_NV = 0x8532;
        public const uint UNSIGNED_IDENTITY_NV = 0x8536;
        public const uint UNSIGNED_INVERT_NV = 0x8537;
        public const uint EXPAND_NORMAL_NV = 0x8538;
        public const uint EXPAND_NEGATE_NV = 0x8539;
        public const uint HALF_BIAS_NORMAL_NV = 0x853A;
        public const uint HALF_BIAS_NEGATE_NV = 0x853B;
        public const uint SIGNED_IDENTITY_NV = 0x853C;
        public const uint SIGNED_NEGATE_NV = 0x853D;
        public const uint SCALE_BY_TWO_NV = 0x853E;
        public const uint SCALE_BY_FOUR_NV = 0x853F;
        public const uint SCALE_BY_ONE_HALF_NV = 0x8540;
        public const uint BIAS_BY_NEGATIVE_ONE_HALF_NV = 0x8541;
        public const uint COMBINER_INPUT_NV = 0x8542;
        public const uint COMBINER_MAPPING_NV = 0x8543;
        public const uint COMBINER_COMPONENT_USAGE_NV = 0x8544;
        public const uint COMBINER_AB_DOT_PRODUCT_NV = 0x8545;
        public const uint COMBINER_CD_DOT_PRODUCT_NV = 0x8546;
        public const uint COMBINER_MUX_SUM_NV = 0x8547;
        public const uint COMBINER_SCALE_NV = 0x8548;
        public const uint COMBINER_BIAS_NV = 0x8549;
        public const uint COMBINER_AB_OUTPUT_NV = 0x854A;
        public const uint COMBINER_CD_OUTPUT_NV = 0x854B;
        public const uint COMBINER_SUM_OUTPUT_NV = 0x854C;
        public const uint MAX_GENERAL_COMBINERS_NV = 0x854D;
        public const uint NUM_GENERAL_COMBINERS_NV = 0x854E;
        public const uint COLOR_SUM_CLAMP_NV = 0x854F;
        public const uint COMBINER0_NV = 0x8550;
        public const uint COMBINER1_NV = 0x8551;
        public const uint COMBINER2_NV = 0x8552;
        public const uint COMBINER3_NV = 0x8553;
        public const uint COMBINER4_NV = 0x8554;
        public const uint COMBINER5_NV = 0x8555;
        public const uint COMBINER6_NV = 0x8556;
        public const uint COMBINER7_NV = 0x8557;
        //    use ARB_multitexture		    TEXTURE0_ARB
        //    use ARB_multitexture		    TEXTURE1_ARB
        //    use BlendingFactorDest		    ZERO
        //    use DrawBufferMode		    NONE
        //    use GetPName			    FOG

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //192
        //NV_fog_distance
        public const uint FOG_DISTANCE_MODE_NV = 0x855A;
        public const uint EYE_RADIAL_NV = 0x855B;
        public const uint EYE_PLANE_ABSOLUTE_NV = 0x855C;
        //    use TextureGenParameter		    EYE_PLANE

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //193
        //NV_texgen_emboss
        public const uint EMBOSS_LIGHT_NV = 0x855D;
        public const uint EMBOSS_CONSTANT_NV = 0x855E;
        public const uint EMBOSS_MAP_NV = 0x855F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //194
        //NV_blend_square

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //195
        //NV_texture_env_combine4
        public const uint COMBINE4_NV = 0x8503;
        public const uint SOURCE3_RGB_NV = 0x8583;
        public const uint SOURCE3_ALPHA_NV = 0x858B;
        public const uint OPERAND3_RGB_NV = 0x8593;
        public const uint OPERAND3_ALPHA_NV = 0x859B;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //196
        //MESA_resize_buffers

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //197
        //MESA_window_pos

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //198
        //EXT_texture_compression_s3tc
        public const uint COMPRESSED_RGB_S3TC_DXT1_EXT = 0x83F0;
        public const uint COMPRESSED_RGBA_S3TC_DXT1_EXT = 0x83F1;
        public const uint COMPRESSED_RGBA_S3TC_DXT3_EXT = 0x83F2;
        public const uint COMPRESSED_RGBA_S3TC_DXT5_EXT = 0x83F3;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //199
        //IBM_cull_vertex
        public const uint CULL_VERTEX_IBM = 103050;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //200
        //IBM_multimode_draw_arrays

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //201
        //IBM_vertex_array_lists
        public const uint VERTEX_ARRAY_LIST_IBM = 103070;
        public const uint NORMAL_ARRAY_LIST_IBM = 103071;
        public const uint COLOR_ARRAY_LIST_IBM = 103072;
        public const uint INDEX_ARRAY_LIST_IBM = 103073;
        public const uint TEXTURE_COORD_ARRAY_LIST_IBM = 103074;
        public const uint EDGE_FLAG_ARRAY_LIST_IBM = 103075;
        public const uint FOG_COORDINATE_ARRAY_LIST_IBM = 103076;
        public const uint SECONDARY_COLOR_ARRAY_LIST_IBM = 103077;
        public const uint VERTEX_ARRAY_LIST_STRIDE_IBM = 103080;
        public const uint NORMAL_ARRAY_LIST_STRIDE_IBM = 103081;
        public const uint COLOR_ARRAY_LIST_STRIDE_IBM = 103082;
        public const uint INDEX_ARRAY_LIST_STRIDE_IBM = 103083;
        public const uint TEXTURE_COORD_ARRAY_LIST_STRIDE_IBM = 103084;
        public const uint EDGE_FLAG_ARRAY_LIST_STRIDE_IBM = 103085;
        public const uint FOG_COORDINATE_ARRAY_LIST_STRIDE_IBM = 103086;
        public const uint SECONDARY_COLOR_ARRAY_LIST_STRIDE_IBM = 103087;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //202
        //SGIX_subsample
        public const uint PACK_SUBSAMPLE_RATE_SGIX = 0x85A0;
        public const uint UNPACK_SUBSAMPLE_RATE_SGIX = 0x85A1;
        public const uint PIXEL_SUBSAMPLE_4444_SGIX = 0x85A2;
        public const uint PIXEL_SUBSAMPLE_2424_SGIX = 0x85A3;
        public const uint PIXEL_SUBSAMPLE_4242_SGIX = 0x85A4;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //203
        //SGIX_ycrcb_subsample
        //public const uint PACK_SUBSAMPLE_RATE_SGIX = 0x85A0;
        //public const uint UNPACK_SUBSAMPLE_RATE_SGIX = 0x85A1;
        //public const uint PIXEL_SUBSAMPLE_4444_SGIX = 0x85A2;
        //public const uint PIXEL_SUBSAMPLE_2424_SGIX = 0x85A3;
        //public const uint PIXEL_SUBSAMPLE_4242_SGIX = 0x85A4;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //204
        //SGIX_ycrcba
        public const uint YCRCB_SGIX = 0x8318;
        public const uint YCRCBA_SGIX = 0x8319;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //205 - skipped (some enums used to be in glext.h, but this
        ////   was an incomplete SGI extension that never actually shipped).

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //206
        //3DFX_texture_compression_FXT1
        public const uint COMPRESSED_RGB_FXT1_3DFX = 0x86B0;
        public const uint COMPRESSED_RGBA_FXT1_3DFX = 0x86B1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //207
        //3DFX_multisample
        public const uint MULTISAMPLE_3DFX = 0x86B2;
        public const uint SAMPLE_BUFFERS_3DFX = 0x86B3;
        public const uint SAMPLES_3DFX = 0x86B4;
        public const uint MULTISAMPLE_BIT_3DFX = 0x20000000;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //208
        //3DFX_tbuffer

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //209
        //EXT_multisample
        public const uint MULTISAMPLE_EXT = 0x809D;
        public const uint SAMPLE_ALPHA_TO_MASK_EXT = 0x809E;
        public const uint SAMPLE_ALPHA_TO_ONE_EXT = 0x809F;
        public const uint SAMPLE_MASK_EXT = 0x80A0;
        //public const uint 1PASS_EXT = 0x80A1;
        //public const uint 2PASS_0_EXT = 0x80A2;
        //public const uint 2PASS_1_EXT = 0x80A3;
        //public const uint 4PASS_0_EXT = 0x80A4;
        //public const uint 4PASS_1_EXT = 0x80A5;
        //public const uint 4PASS_2_EXT = 0x80A6;
        //public const uint 4PASS_3_EXT = 0x80A7;
        public const uint SAMPLE_BUFFERS_EXT = 0x80A8; // 1 I
        public const uint SAMPLES_EXT = 0x80A9; // 1 I
        public const uint SAMPLE_MASK_VALUE_EXT = 0x80AA; // 1 F
        public const uint SAMPLE_MASK_INVERT_EXT = 0x80AB; // 1 I
        public const uint SAMPLE_PATTERN_EXT = 0x80AC; // 1 I
        public const uint MULTISAMPLE_BIT_EXT = 0x20000000;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //210
        //SGIX_vertex_preclip
        public const uint VERTEX_PRECLIP_SGIX = 0x83EE;
        public const uint VERTEX_PRECLIP_HINT_SGIX = 0x83EF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //211
        //SGIX_convolution_accuracy
        public const uint CONVOLUTION_HINT_SGIX = 0x8316; // 1 I

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //212
        //SGIX_resample
        public const uint PACK_RESAMPLE_SGIX = 0x842C;
        public const uint UNPACK_RESAMPLE_SGIX = 0x842D;
        public const uint RESAMPLE_REPLICATE_SGIX = 0x842E;
        public const uint RESAMPLE_ZERO_FILL_SGIX = 0x842F;
        public const uint RESAMPLE_DECIMATE_SGIX = 0x8430;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //213
        //SGIS_point_line_texgen
        public const uint EYE_DISTANCE_TO_POINT_SGIS = 0x81F0;
        public const uint OBJECT_DISTANCE_TO_POINT_SGIS = 0x81F1;
        public const uint EYE_DISTANCE_TO_LINE_SGIS = 0x81F2;
        public const uint OBJECT_DISTANCE_TO_LINE_SGIS = 0x81F3;
        public const uint EYE_POINT_SGIS = 0x81F4;
        public const uint OBJECT_POINT_SGIS = 0x81F5;
        public const uint EYE_LINE_SGIS = 0x81F6;
        public const uint OBJECT_LINE_SGIS = 0x81F7;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //214
        //SGIS_texture_color_mask
        public const uint TEXTURE_COLOR_WRITEMASK_SGIS = 0x81EF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //220
        //// Promoted to ARB_texture_env_dot3, enum values changed
        //EXT_texture_env_dot3
        public const uint DOT3_RGB_EXT = 0x8740;
        public const uint DOT3_RGBA_EXT = 0x8741;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //221
        //ATI_texture_mirror_once
        public const uint MIRROR_CLAMP_ATI = 0x8742;
        public const uint MIRROR_CLAMP_TO_EDGE_ATI = 0x8743;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //222
        //NV_fence
        public const uint ALL_COMPLETED_NV = 0x84F2;
        public const uint FENCE_STATUS_NV = 0x84F3;
        public const uint FENCE_CONDITION_NV = 0x84F4;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //223
        //IBM_static_data
        public const uint ALL_STATIC_DATA_IBM = 103060;
        public const uint STATIC_VERTEX_ARRAY_IBM = 103061;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //224
        //IBM_texture_mirrored_repeat
        public const uint MIRRORED_REPEAT_IBM = 0x8370;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //225
        //NV_evaluators
        public const uint EVAL_2D_NV = 0x86C0;
        public const uint EVAL_TRIANGULAR_2D_NV = 0x86C1;
        public const uint MAP_TESSELLATION_NV = 0x86C2;
        public const uint MAP_ATTRIB_U_ORDER_NV = 0x86C3;
        public const uint MAP_ATTRIB_V_ORDER_NV = 0x86C4;
        public const uint EVAL_FRACTIONAL_TESSELLATION_NV = 0x86C5;
        public const uint EVAL_VERTEX_ATTRIB0_NV = 0x86C6;
        public const uint EVAL_VERTEX_ATTRIB1_NV = 0x86C7;
        public const uint EVAL_VERTEX_ATTRIB2_NV = 0x86C8;
        public const uint EVAL_VERTEX_ATTRIB3_NV = 0x86C9;
        public const uint EVAL_VERTEX_ATTRIB4_NV = 0x86CA;
        public const uint EVAL_VERTEX_ATTRIB5_NV = 0x86CB;
        public const uint EVAL_VERTEX_ATTRIB6_NV = 0x86CC;
        public const uint EVAL_VERTEX_ATTRIB7_NV = 0x86CD;
        public const uint EVAL_VERTEX_ATTRIB8_NV = 0x86CE;
        public const uint EVAL_VERTEX_ATTRIB9_NV = 0x86CF;
        public const uint EVAL_VERTEX_ATTRIB10_NV = 0x86D0;
        public const uint EVAL_VERTEX_ATTRIB11_NV = 0x86D1;
        public const uint EVAL_VERTEX_ATTRIB12_NV = 0x86D2;
        public const uint EVAL_VERTEX_ATTRIB13_NV = 0x86D3;
        public const uint EVAL_VERTEX_ATTRIB14_NV = 0x86D4;
        public const uint EVAL_VERTEX_ATTRIB15_NV = 0x86D5;
        public const uint MAX_MAP_TESSELLATION_NV = 0x86D6;
        public const uint MAX_RATIONAL_EVAL_ORDER_NV = 0x86D7;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //226
        //NV_packed_depth_stencil
        public const uint DEPTH_STENCIL_NV = 0x84F9;
        public const uint UNSIGNED_INT_24_8_NV = 0x84FA;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //227
        //NV_register_combiners2
        public const uint PER_STAGE_CONSTANTS_NV = 0x8535;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //228
        //NV_texture_compression_vtc

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //229
        //NV_texture_rectangle
        public const uint TEXTURE_RECTANGLE_NV = 0x84F5;
        public const uint TEXTURE_BINDING_RECTANGLE_NV = 0x84F6;
        public const uint PROXY_TEXTURE_RECTANGLE_NV = 0x84F7;
        public const uint MAX_RECTANGLE_TEXTURE_SIZE_NV = 0x84F8;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //230
        //NV_texture_shader
        public const uint OFFSET_TEXTURE_RECTANGLE_NV = 0x864C;
        public const uint OFFSET_TEXTURE_RECTANGLE_SCALE_NV = 0x864D;
        public const uint DOT_PRODUCT_TEXTURE_RECTANGLE_NV = 0x864E;
        public const uint RGBA_UNSIGNED_DOT_PRODUCT_MAPPING_NV = 0x86D9;
        public const uint UNSIGNED_INT_S8_S8_8_8_NV = 0x86DA;
        public const uint UNSIGNED_INT_8_8_S8_S8_REV_NV = 0x86DB;
        public const uint DSDT_MAG_INTENSITY_NV = 0x86DC;
        public const uint SHADER_CONSISTENT_NV = 0x86DD;
        public const uint TEXTURE_SHADER_NV = 0x86DE;
        public const uint SHADER_OPERATION_NV = 0x86DF;
        public const uint CULL_MODES_NV = 0x86E0;
        public const uint OFFSET_TEXTURE_MATRIX_NV = 0x86E1;
        public const uint OFFSET_TEXTURE_2D_MATRIX_NV = 0x86E1;    // alias OFFSET_TEXTURE_MATRIX_NV
        public const uint OFFSET_TEXTURE_SCALE_NV = 0x86E2;
        public const uint OFFSET_TEXTURE_2D_SCALE_NV = 0x86E2;    // alias OFFSET_TEXTURE_SCALE_NV
        public const uint OFFSET_TEXTURE_BIAS_NV = 0x86E3;
        public const uint OFFSET_TEXTURE_2D_BIAS_NV = 0x86E3;    // alias OFFSET_TEXTURE_BIAS_NV
        public const uint PREVIOUS_TEXTURE_INPUT_NV = 0x86E4;
        public const uint CONST_EYE_NV = 0x86E5;
        public const uint PASS_THROUGH_NV = 0x86E6;
        public const uint CULL_FRAGMENT_NV = 0x86E7;
        public const uint OFFSET_TEXTURE_2D_NV = 0x86E8;
        public const uint DEPENDENT_AR_TEXTURE_2D_NV = 0x86E9;
        public const uint DEPENDENT_GB_TEXTURE_2D_NV = 0x86EA;
        public const uint DOT_PRODUCT_NV = 0x86EC;
        public const uint DOT_PRODUCT_DEPTH_REPLACE_NV = 0x86ED;
        public const uint DOT_PRODUCT_TEXTURE_2D_NV = 0x86EE;
        public const uint DOT_PRODUCT_TEXTURE_CUBE_MAP_NV = 0x86F0;
        public const uint DOT_PRODUCT_DIFFUSE_CUBE_MAP_NV = 0x86F1;
        public const uint DOT_PRODUCT_REFLECT_CUBE_MAP_NV = 0x86F2;
        public const uint DOT_PRODUCT_CONST_EYE_REFLECT_CUBE_MAP_NV = 0x86F3;
        public const uint HILO_NV = 0x86F4;
        public const uint DSDT_NV = 0x86F5;
        public const uint DSDT_MAG_NV = 0x86F6;
        public const uint DSDT_MAG_VIB_NV = 0x86F7;
        public const uint HILO16_NV = 0x86F8;
        public const uint SIGNED_HILO_NV = 0x86F9;
        public const uint SIGNED_HILO16_NV = 0x86FA;
        public const uint SIGNED_RGBA_NV = 0x86FB;
        public const uint SIGNED_RGBA8_NV = 0x86FC;
        public const uint SIGNED_RGB_NV = 0x86FE;
        public const uint SIGNED_RGB8_NV = 0x86FF;
        public const uint SIGNED_LUMINANCE_NV = 0x8701;
        public const uint SIGNED_LUMINANCE8_NV = 0x8702;
        public const uint SIGNED_LUMINANCE_ALPHA_NV = 0x8703;
        public const uint SIGNED_LUMINANCE8_ALPHA8_NV = 0x8704;
        public const uint SIGNED_ALPHA_NV = 0x8705;
        public const uint SIGNED_ALPHA8_NV = 0x8706;
        public const uint SIGNED_INTENSITY_NV = 0x8707;
        public const uint SIGNED_INTENSITY8_NV = 0x8708;
        public const uint DSDT8_NV = 0x8709;
        public const uint DSDT8_MAG8_NV = 0x870A;
        public const uint DSDT8_MAG8_INTENSITY8_NV = 0x870B;
        public const uint SIGNED_RGB_UNSIGNED_ALPHA_NV = 0x870C;
        public const uint SIGNED_RGB8_UNSIGNED_ALPHA8_NV = 0x870D;
        public const uint HI_SCALE_NV = 0x870E;
        public const uint LO_SCALE_NV = 0x870F;
        public const uint DS_SCALE_NV = 0x8710;
        public const uint DT_SCALE_NV = 0x8711;
        public const uint MAGNITUDE_SCALE_NV = 0x8712;
        public const uint VIBRANCE_SCALE_NV = 0x8713;
        public const uint HI_BIAS_NV = 0x8714;
        public const uint LO_BIAS_NV = 0x8715;
        public const uint DS_BIAS_NV = 0x8716;
        public const uint DT_BIAS_NV = 0x8717;
        public const uint MAGNITUDE_BIAS_NV = 0x8718;
        public const uint VIBRANCE_BIAS_NV = 0x8719;
        public const uint TEXTURE_BORDER_VALUES_NV = 0x871A;
        public const uint TEXTURE_HI_SIZE_NV = 0x871B;
        public const uint TEXTURE_LO_SIZE_NV = 0x871C;
        public const uint TEXTURE_DS_SIZE_NV = 0x871D;
        public const uint TEXTURE_DT_SIZE_NV = 0x871E;
        public const uint TEXTURE_MAG_SIZE_NV = 0x871F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //231
        //NV_texture_shader2
        public const uint DOT_PRODUCT_TEXTURE_3D_NV = 0x86EF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //232
        //NV_vertex_array_range2
        public const uint VERTEX_ARRAY_RANGE_WITHOUT_FLUSH_NV = 0x8533;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //233
        //NV_vertex_program
        public const uint VERTEX_PROGRAM_NV = 0x8620;
        public const uint VERTEX_STATE_PROGRAM_NV = 0x8621;
        public const uint ATTRIB_ARRAY_SIZE_NV = 0x8623;
        public const uint ATTRIB_ARRAY_STRIDE_NV = 0x8624;
        public const uint ATTRIB_ARRAY_TYPE_NV = 0x8625;
        public const uint CURRENT_ATTRIB_NV = 0x8626;
        public const uint PROGRAM_LENGTH_NV = 0x8627;
        public const uint PROGRAM_STRING_NV = 0x8628;
        public const uint MODELVIEW_PROJECTION_NV = 0x8629;
        public const uint IDENTITY_NV = 0x862A;
        public const uint INVERSE_NV = 0x862B;
        public const uint TRANSPOSE_NV = 0x862C;
        public const uint INVERSE_TRANSPOSE_NV = 0x862D;
        public const uint MAX_TRACK_MATRIX_STACK_DEPTH_NV = 0x862E;
        public const uint MAX_TRACK_MATRICES_NV = 0x862F;
        public const uint MATRIX0_NV = 0x8630;
        public const uint MATRIX1_NV = 0x8631;
        public const uint MATRIX2_NV = 0x8632;
        public const uint MATRIX3_NV = 0x8633;
        public const uint MATRIX4_NV = 0x8634;
        public const uint MATRIX5_NV = 0x8635;
        public const uint MATRIX6_NV = 0x8636;
        public const uint MATRIX7_NV = 0x8637;
        //////////////////////////////////////
        ////
        ////	Reserved:
        //public const uint ////	MATRIX8_NV = 0x8638;
        //public const uint MATRIX9_NV = 0x8639;
        //public const uint MATRIX10_NV = 0x863A;
        //public const uint MATRIX11_NV = 0x863B;
        //public const uint MATRIX12_NV = 0x863C;
        //public const uint MATRIX13_NV = 0x863D;
        //public const uint MATRIX14_NV = 0x863E;
        //public const uint MATRIX15_NV = 0x863F;
        ////
        //////////////////////////////////////public const uint //    CURRENT_MATRIX_STACK_DEPTH_NV = 0x8640;
        public const uint CURRENT_MATRIX_NV = 0x8641;
        public const uint VERTEX_PROGRAM_POINT_SIZE_NV = 0x8642;
        public const uint VERTEX_PROGRAM_TWO_SIDE_NV = 0x8643;
        public const uint PROGRAM_PARAMETER_NV = 0x8644;
        public const uint ATTRIB_ARRAY_POINTER_NV = 0x8645;
        public const uint PROGRAM_TARGET_NV = 0x8646;
        public const uint PROGRAM_RESIDENT_NV = 0x8647;
        public const uint TRACK_MATRIX_NV = 0x8648;
        public const uint TRACK_MATRIX_TRANSFORM_NV = 0x8649;
        public const uint VERTEX_PROGRAM_BINDING_NV = 0x864A;
        public const uint PROGRAM_ERROR_POSITION_NV = 0x864B;
        public const uint VERTEX_ATTRIB_ARRAY0_NV = 0x8650;
        public const uint VERTEX_ATTRIB_ARRAY1_NV = 0x8651;
        public const uint VERTEX_ATTRIB_ARRAY2_NV = 0x8652;
        public const uint VERTEX_ATTRIB_ARRAY3_NV = 0x8653;
        public const uint VERTEX_ATTRIB_ARRAY4_NV = 0x8654;
        public const uint VERTEX_ATTRIB_ARRAY5_NV = 0x8655;
        public const uint VERTEX_ATTRIB_ARRAY6_NV = 0x8656;
        public const uint VERTEX_ATTRIB_ARRAY7_NV = 0x8657;
        public const uint VERTEX_ATTRIB_ARRAY8_NV = 0x8658;
        public const uint VERTEX_ATTRIB_ARRAY9_NV = 0x8659;
        public const uint VERTEX_ATTRIB_ARRAY10_NV = 0x865A;
        public const uint VERTEX_ATTRIB_ARRAY11_NV = 0x865B;
        public const uint VERTEX_ATTRIB_ARRAY12_NV = 0x865C;
        public const uint VERTEX_ATTRIB_ARRAY13_NV = 0x865D;
        public const uint VERTEX_ATTRIB_ARRAY14_NV = 0x865E;
        public const uint VERTEX_ATTRIB_ARRAY15_NV = 0x865F;
        public const uint MAP1_VERTEX_ATTRIB0_4_NV = 0x8660;
        public const uint MAP1_VERTEX_ATTRIB1_4_NV = 0x8661;
        public const uint MAP1_VERTEX_ATTRIB2_4_NV = 0x8662;
        public const uint MAP1_VERTEX_ATTRIB3_4_NV = 0x8663;
        public const uint MAP1_VERTEX_ATTRIB4_4_NV = 0x8664;
        public const uint MAP1_VERTEX_ATTRIB5_4_NV = 0x8665;
        public const uint MAP1_VERTEX_ATTRIB6_4_NV = 0x8666;
        public const uint MAP1_VERTEX_ATTRIB7_4_NV = 0x8667;
        public const uint MAP1_VERTEX_ATTRIB8_4_NV = 0x8668;
        public const uint MAP1_VERTEX_ATTRIB9_4_NV = 0x8669;
        public const uint MAP1_VERTEX_ATTRIB10_4_NV = 0x866A;
        public const uint MAP1_VERTEX_ATTRIB11_4_NV = 0x866B;
        public const uint MAP1_VERTEX_ATTRIB12_4_NV = 0x866C;
        public const uint MAP1_VERTEX_ATTRIB13_4_NV = 0x866D;
        public const uint MAP1_VERTEX_ATTRIB14_4_NV = 0x866E;
        public const uint MAP1_VERTEX_ATTRIB15_4_NV = 0x866F;
        public const uint MAP2_VERTEX_ATTRIB0_4_NV = 0x8670;
        public const uint MAP2_VERTEX_ATTRIB1_4_NV = 0x8671;
        public const uint MAP2_VERTEX_ATTRIB2_4_NV = 0x8672;
        public const uint MAP2_VERTEX_ATTRIB3_4_NV = 0x8673;
        public const uint MAP2_VERTEX_ATTRIB4_4_NV = 0x8674;
        public const uint MAP2_VERTEX_ATTRIB5_4_NV = 0x8675;
        public const uint MAP2_VERTEX_ATTRIB6_4_NV = 0x8676;
        public const uint MAP2_VERTEX_ATTRIB7_4_NV = 0x8677;
        public const uint MAP2_VERTEX_ATTRIB8_4_NV = 0x8678;
        public const uint MAP2_VERTEX_ATTRIB9_4_NV = 0x8679;
        public const uint MAP2_VERTEX_ATTRIB10_4_NV = 0x867A;
        public const uint MAP2_VERTEX_ATTRIB11_4_NV = 0x867B;
        public const uint MAP2_VERTEX_ATTRIB12_4_NV = 0x867C;
        public const uint MAP2_VERTEX_ATTRIB13_4_NV = 0x867D;
        public const uint MAP2_VERTEX_ATTRIB14_4_NV = 0x867E;
        public const uint MAP2_VERTEX_ATTRIB15_4_NV = 0x867F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //235
        //SGIX_texture_coordinate_clamp
        public const uint TEXTURE_MAX_CLAMP_S_SGIX = 0x8369;
        public const uint TEXTURE_MAX_CLAMP_T_SGIX = 0x836A;
        public const uint TEXTURE_MAX_CLAMP_R_SGIX = 0x836B;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //236
        //SGIX_scalebias_hint
        public const uint SCALEBIAS_HINT_SGIX = 0x8322;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //237 - GLX_OML_swap_method
        //// Extension //238 - GLX_OML_sync_control

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //239
        //OML_interlace
        public const uint INTERLACE_OML = 0x8980;
        public const uint INTERLACE_READ_OML = 0x8981;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //240
        //OML_subsample
        public const uint FORMAT_SUBSAMPLE_24_24_OML = 0x8982;
        public const uint FORMAT_SUBSAMPLE_244_244_OML = 0x8983;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //241
        //OML_resample
        public const uint PACK_RESAMPLE_OML = 0x8984;
        public const uint UNPACK_RESAMPLE_OML = 0x8985;
        public const uint RESAMPLE_REPLICATE_OML = 0x8986;
        public const uint RESAMPLE_ZERO_FILL_OML = 0x8987;
        public const uint RESAMPLE_AVERAGE_OML = 0x8988;
        public const uint RESAMPLE_DECIMATE_OML = 0x8989;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //242 - WGL_OML_sync_control

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //243
        //NV_copy_depth_to_color
        public const uint DEPTH_STENCIL_TO_RGBA_NV = 0x886E;
        public const uint DEPTH_STENCIL_TO_BGRA_NV = 0x886F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //244
        //ATI_envmap_bumpmap
        public const uint BUMP_ROT_MATRIX_ATI = 0x8775;
        public const uint BUMP_ROT_MATRIX_SIZE_ATI = 0x8776;
        public const uint BUMP_NUM_TEX_UNITS_ATI = 0x8777;
        public const uint BUMP_TEX_UNITS_ATI = 0x8778;
        public const uint DUDV_ATI = 0x8779;
        public const uint DU8DV8_ATI = 0x877A;
        public const uint BUMP_ENVMAP_ATI = 0x877B;
        public const uint BUMP_TARGET_ATI = 0x877C;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //245
        //ATI_fragment_shader
        public const uint FRAGMENT_SHADER_ATI = 0x8920;
        public const uint REG_0_ATI = 0x8921;
        public const uint REG_1_ATI = 0x8922;
        public const uint REG_2_ATI = 0x8923;
        public const uint REG_3_ATI = 0x8924;
        public const uint REG_4_ATI = 0x8925;
        public const uint REG_5_ATI = 0x8926;
        public const uint REG_6_ATI = 0x8927;
        public const uint REG_7_ATI = 0x8928;
        public const uint REG_8_ATI = 0x8929;
        public const uint REG_9_ATI = 0x892A;
        public const uint REG_10_ATI = 0x892B;
        public const uint REG_11_ATI = 0x892C;
        public const uint REG_12_ATI = 0x892D;
        public const uint REG_13_ATI = 0x892E;
        public const uint REG_14_ATI = 0x892F;
        public const uint REG_15_ATI = 0x8930;
        public const uint REG_16_ATI = 0x8931;
        public const uint REG_17_ATI = 0x8932;
        public const uint REG_18_ATI = 0x8933;
        public const uint REG_19_ATI = 0x8934;
        public const uint REG_20_ATI = 0x8935;
        public const uint REG_21_ATI = 0x8936;
        public const uint REG_22_ATI = 0x8937;
        public const uint REG_23_ATI = 0x8938;
        public const uint REG_24_ATI = 0x8939;
        public const uint REG_25_ATI = 0x893A;
        public const uint REG_26_ATI = 0x893B;
        public const uint REG_27_ATI = 0x893C;
        public const uint REG_28_ATI = 0x893D;
        public const uint REG_29_ATI = 0x893E;
        public const uint REG_30_ATI = 0x893F;
        public const uint REG_31_ATI = 0x8940;
        public const uint CON_0_ATI = 0x8941;
        public const uint CON_1_ATI = 0x8942;
        public const uint CON_2_ATI = 0x8943;
        public const uint CON_3_ATI = 0x8944;
        public const uint CON_4_ATI = 0x8945;
        public const uint CON_5_ATI = 0x8946;
        public const uint CON_6_ATI = 0x8947;
        public const uint CON_7_ATI = 0x8948;
        public const uint CON_8_ATI = 0x8949;
        public const uint CON_9_ATI = 0x894A;
        public const uint CON_10_ATI = 0x894B;
        public const uint CON_11_ATI = 0x894C;
        public const uint CON_12_ATI = 0x894D;
        public const uint CON_13_ATI = 0x894E;
        public const uint CON_14_ATI = 0x894F;
        public const uint CON_15_ATI = 0x8950;
        public const uint CON_16_ATI = 0x8951;
        public const uint CON_17_ATI = 0x8952;
        public const uint CON_18_ATI = 0x8953;
        public const uint CON_19_ATI = 0x8954;
        public const uint CON_20_ATI = 0x8955;
        public const uint CON_21_ATI = 0x8956;
        public const uint CON_22_ATI = 0x8957;
        public const uint CON_23_ATI = 0x8958;
        public const uint CON_24_ATI = 0x8959;
        public const uint CON_25_ATI = 0x895A;
        public const uint CON_26_ATI = 0x895B;
        public const uint CON_27_ATI = 0x895C;
        public const uint CON_28_ATI = 0x895D;
        public const uint CON_29_ATI = 0x895E;
        public const uint CON_30_ATI = 0x895F;
        public const uint CON_31_ATI = 0x8960;
        public const uint MOV_ATI = 0x8961;
        public const uint ADD_ATI = 0x8963;
        public const uint MUL_ATI = 0x8964;
        public const uint SUB_ATI = 0x8965;
        public const uint DOT3_ATI = 0x8966;
        public const uint DOT4_ATI = 0x8967;
        public const uint MAD_ATI = 0x8968;
        public const uint LERP_ATI = 0x8969;
        public const uint CND_ATI = 0x896A;
        public const uint CND0_ATI = 0x896B;
        public const uint DOT2_ADD_ATI = 0x896C;
        public const uint SECONDARY_INTERPOLATOR_ATI = 0x896D;
        public const uint NUM_FRAGMENT_REGISTERS_ATI = 0x896E;
        public const uint NUM_FRAGMENT_CONSTANTS_ATI = 0x896F;
        public const uint NUM_PASSES_ATI = 0x8970;
        public const uint NUM_INSTRUCTIONS_PER_PASS_ATI = 0x8971;
        public const uint NUM_INSTRUCTIONS_TOTAL_ATI = 0x8972;
        public const uint NUM_INPUT_INTERPOLATOR_COMPONENTS_ATI = 0x8973;
        public const uint NUM_LOOPBACK_COMPONENTS_ATI = 0x8974;
        public const uint COLOR_ALPHA_PAIRING_ATI = 0x8975;
        public const uint SWIZZLE_STR_ATI = 0x8976;
        public const uint SWIZZLE_STQ_ATI = 0x8977;
        public const uint SWIZZLE_STR_DR_ATI = 0x8978;
        public const uint SWIZZLE_STQ_DQ_ATI = 0x8979;
        public const uint SWIZZLE_STRQ_ATI = 0x897A;
        public const uint SWIZZLE_STRQ_DQ_ATI = 0x897B;
        public const uint RED_BIT_ATI = 0x00000001;
        public const uint GREEN_BIT_ATI = 0x00000002;
        public const uint BLUE_BIT_ATI = 0x00000004;
        //public const uint 2X_BIT_ATI = 0x00000001;
        //public const uint 4X_BIT_ATI = 0x00000002;
        //public const uint 8X_BIT_ATI = 0x00000004;
        public const uint HALF_BIT_ATI = 0x00000008;
        public const uint QUARTER_BIT_ATI = 0x00000010;
        public const uint EIGHTH_BIT_ATI = 0x00000020;
        public const uint SATURATE_BIT_ATI = 0x00000040;
        //public const uint 2X_BIT_ATI = 0x00000001;
        public const uint COMP_BIT_ATI = 0x00000002;
        public const uint NEGATE_BIT_ATI = 0x00000004;
        public const uint BIAS_BIT_ATI = 0x00000008;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //246
        //ATI_pn_triangles
        public const uint PN_TRIANGLES_ATI = 0x87F0;
        public const uint MAX_PN_TRIANGLES_TESSELATION_LEVEL_ATI = 0x87F1;
        public const uint PN_TRIANGLES_POINT_MODE_ATI = 0x87F2;
        public const uint PN_TRIANGLES_NORMAL_MODE_ATI = 0x87F3;
        public const uint PN_TRIANGLES_TESSELATION_LEVEL_ATI = 0x87F4;
        public const uint PN_TRIANGLES_POINT_MODE_LINEAR_ATI = 0x87F5;
        public const uint PN_TRIANGLES_POINT_MODE_CUBIC_ATI = 0x87F6;
        public const uint PN_TRIANGLES_NORMAL_MODE_LINEAR_ATI = 0x87F7;
        public const uint PN_TRIANGLES_NORMAL_MODE_QUADRATIC_ATI = 0x87F8;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //247
        //ATI_vertex_array_object
        public const uint STATIC_ATI = 0x8760;
        public const uint DYNAMIC_ATI = 0x8761;
        public const uint PRESERVE_ATI = 0x8762;
        public const uint DISCARD_ATI = 0x8763;
        public const uint OBJECT_BUFFER_SIZE_ATI = 0x8764;
        public const uint OBJECT_BUFFER_USAGE_ATI = 0x8765;
        public const uint ARRAY_OBJECT_BUFFER_ATI = 0x8766;
        public const uint ARRAY_OBJECT_OFFSET_ATI = 0x8767;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //248
        //EXT_vertex_shader
        public const uint VERTEX_SHADER_EXT = 0x8780;
        public const uint VERTEX_SHADER_BINDING_EXT = 0x8781;
        public const uint OP_INDEX_EXT = 0x8782;
        public const uint OP_NEGATE_EXT = 0x8783;
        public const uint OP_DOT3_EXT = 0x8784;
        public const uint OP_DOT4_EXT = 0x8785;
        public const uint OP_MUL_EXT = 0x8786;
        public const uint OP_ADD_EXT = 0x8787;
        public const uint OP_MADD_EXT = 0x8788;
        public const uint OP_FRAC_EXT = 0x8789;
        public const uint OP_MAX_EXT = 0x878A;
        public const uint OP_MIN_EXT = 0x878B;
        public const uint OP_SET_GE_EXT = 0x878C;
        public const uint OP_SET_LT_EXT = 0x878D;
        public const uint OP_CLAMP_EXT = 0x878E;
        public const uint OP_FLOOR_EXT = 0x878F;
        public const uint OP_ROUND_EXT = 0x8790;
        public const uint OP_EXP_BASE_2_EXT = 0x8791;
        public const uint OP_LOG_BASE_2_EXT = 0x8792;
        public const uint OP_POWER_EXT = 0x8793;
        public const uint OP_RECIP_EXT = 0x8794;
        public const uint OP_RECIP_SQRT_EXT = 0x8795;
        public const uint OP_SUB_EXT = 0x8796;
        public const uint OP_CROSS_PRODUCT_EXT = 0x8797;
        public const uint OP_MULTIPLY_MATRIX_EXT = 0x8798;
        public const uint OP_MOV_EXT = 0x8799;
        public const uint OUTPUT_VERTEX_EXT = 0x879A;
        public const uint OUTPUT_COLOR0_EXT = 0x879B;
        public const uint OUTPUT_COLOR1_EXT = 0x879C;
        public const uint OUTPUT_TEXTURE_COORD0_EXT = 0x879D;
        public const uint OUTPUT_TEXTURE_COORD1_EXT = 0x879E;
        public const uint OUTPUT_TEXTURE_COORD2_EXT = 0x879F;
        public const uint OUTPUT_TEXTURE_COORD3_EXT = 0x87A0;
        public const uint OUTPUT_TEXTURE_COORD4_EXT = 0x87A1;
        public const uint OUTPUT_TEXTURE_COORD5_EXT = 0x87A2;
        public const uint OUTPUT_TEXTURE_COORD6_EXT = 0x87A3;
        public const uint OUTPUT_TEXTURE_COORD7_EXT = 0x87A4;
        public const uint OUTPUT_TEXTURE_COORD8_EXT = 0x87A5;
        public const uint OUTPUT_TEXTURE_COORD9_EXT = 0x87A6;
        public const uint OUTPUT_TEXTURE_COORD10_EXT = 0x87A7;
        public const uint OUTPUT_TEXTURE_COORD11_EXT = 0x87A8;
        public const uint OUTPUT_TEXTURE_COORD12_EXT = 0x87A9;
        public const uint OUTPUT_TEXTURE_COORD13_EXT = 0x87AA;
        public const uint OUTPUT_TEXTURE_COORD14_EXT = 0x87AB;
        public const uint OUTPUT_TEXTURE_COORD15_EXT = 0x87AC;
        public const uint OUTPUT_TEXTURE_COORD16_EXT = 0x87AD;
        public const uint OUTPUT_TEXTURE_COORD17_EXT = 0x87AE;
        public const uint OUTPUT_TEXTURE_COORD18_EXT = 0x87AF;
        public const uint OUTPUT_TEXTURE_COORD19_EXT = 0x87B0;
        public const uint OUTPUT_TEXTURE_COORD20_EXT = 0x87B1;
        public const uint OUTPUT_TEXTURE_COORD21_EXT = 0x87B2;
        public const uint OUTPUT_TEXTURE_COORD22_EXT = 0x87B3;
        public const uint OUTPUT_TEXTURE_COORD23_EXT = 0x87B4;
        public const uint OUTPUT_TEXTURE_COORD24_EXT = 0x87B5;
        public const uint OUTPUT_TEXTURE_COORD25_EXT = 0x87B6;
        public const uint OUTPUT_TEXTURE_COORD26_EXT = 0x87B7;
        public const uint OUTPUT_TEXTURE_COORD27_EXT = 0x87B8;
        public const uint OUTPUT_TEXTURE_COORD28_EXT = 0x87B9;
        public const uint OUTPUT_TEXTURE_COORD29_EXT = 0x87BA;
        public const uint OUTPUT_TEXTURE_COORD30_EXT = 0x87BB;
        public const uint OUTPUT_TEXTURE_COORD31_EXT = 0x87BC;
        public const uint OUTPUT_FOG_EXT = 0x87BD;
        public const uint SCALAR_EXT = 0x87BE;
        public const uint VECTOR_EXT = 0x87BF;
        public const uint MATRIX_EXT = 0x87C0;
        public const uint VARIANT_EXT = 0x87C1;
        public const uint INVARIANT_EXT = 0x87C2;
        public const uint LOCAL_CONSTANT_EXT = 0x87C3;
        public const uint LOCAL_EXT = 0x87C4;
        public const uint MAX_VERTEX_SHADER_INSTRUCTIONS_EXT = 0x87C5;
        public const uint MAX_VERTEX_SHADER_VARIANTS_EXT = 0x87C6;
        public const uint MAX_VERTEX_SHADER_INVARIANTS_EXT = 0x87C7;
        public const uint MAX_VERTEX_SHADER_LOCAL_CONSTANTS_EXT = 0x87C8;
        public const uint MAX_VERTEX_SHADER_LOCALS_EXT = 0x87C9;
        public const uint MAX_OPTIMIZED_VERTEX_SHADER_INSTRUCTIONS_EXT = 0x87CA;
        public const uint MAX_OPTIMIZED_VERTEX_SHADER_VARIANTS_EXT = 0x87CB;
        public const uint MAX_OPTIMIZED_VERTEX_SHADER_LOCAL_CONSTANTS_EXT = 0x87CC;
        public const uint MAX_OPTIMIZED_VERTEX_SHADER_INVARIANTS_EXT = 0x87CD;
        public const uint MAX_OPTIMIZED_VERTEX_SHADER_LOCALS_EXT = 0x87CE;
        public const uint VERTEX_SHADER_INSTRUCTIONS_EXT = 0x87CF;
        public const uint VERTEX_SHADER_VARIANTS_EXT = 0x87D0;
        public const uint VERTEX_SHADER_INVARIANTS_EXT = 0x87D1;
        public const uint VERTEX_SHADER_LOCAL_CONSTANTS_EXT = 0x87D2;
        public const uint VERTEX_SHADER_LOCALS_EXT = 0x87D3;
        public const uint VERTEX_SHADER_OPTIMIZED_EXT = 0x87D4;
        public const uint X_EXT = 0x87D5;
        public const uint Y_EXT = 0x87D6;
        public const uint Z_EXT = 0x87D7;
        public const uint W_EXT = 0x87D8;
        public const uint NEGATIVE_X_EXT = 0x87D9;
        public const uint NEGATIVE_Y_EXT = 0x87DA;
        public const uint NEGATIVE_Z_EXT = 0x87DB;
        public const uint NEGATIVE_W_EXT = 0x87DC;
        public const uint ZERO_EXT = 0x87DD;
        public const uint ONE_EXT = 0x87DE;
        public const uint NEGATIVE_ONE_EXT = 0x87DF;
        public const uint NORMALIZED_RANGE_EXT = 0x87E0;
        public const uint FULL_RANGE_EXT = 0x87E1;
        public const uint CURRENT_VERTEX_EXT = 0x87E2;
        public const uint MVP_MATRIX_EXT = 0x87E3;
        public const uint VARIANT_VALUE_EXT = 0x87E4;
        public const uint VARIANT_DATATYPE_EXT = 0x87E5;
        public const uint VARIANT_ARRAY_STRIDE_EXT = 0x87E6;
        public const uint VARIANT_ARRAY_TYPE_EXT = 0x87E7;
        public const uint VARIANT_ARRAY_EXT = 0x87E8;
        public const uint VARIANT_ARRAY_POINTER_EXT = 0x87E9;
        public const uint INVARIANT_VALUE_EXT = 0x87EA;
        public const uint INVARIANT_DATATYPE_EXT = 0x87EB;
        public const uint LOCAL_CONSTANT_VALUE_EXT = 0x87EC;
        public const uint LOCAL_CONSTANT_DATATYPE_EXT = 0x87ED;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //249
        //ATI_vertex_streams
        public const uint MAX_VERTEX_STREAMS_ATI = 0x876B;
        public const uint VERTEX_STREAM0_ATI = 0x876C;
        public const uint VERTEX_STREAM1_ATI = 0x876D;
        public const uint VERTEX_STREAM2_ATI = 0x876E;
        public const uint VERTEX_STREAM3_ATI = 0x876F;
        public const uint VERTEX_STREAM4_ATI = 0x8770;
        public const uint VERTEX_STREAM5_ATI = 0x8771;
        public const uint VERTEX_STREAM6_ATI = 0x8772;
        public const uint VERTEX_STREAM7_ATI = 0x8773;
        public const uint VERTEX_SOURCE_ATI = 0x8774;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //250 - WGL_I3D_digital_video_control
        //// Extension //251 - WGL_I3D_gamma
        //// Extension //252 - WGL_I3D_genlock
        //// Extension //253 - WGL_I3D_image_buffer
        //// Extension //254 - WGL_I3D_swap_frame_lock
        //// Extension //255 - WGL_I3D_swap_frame_usage

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //256
        //ATI_element_array
        public const uint ELEMENT_ARRAY_ATI = 0x8768;
        public const uint ELEMENT_ARRAY_TYPE_ATI = 0x8769;
        public const uint ELEMENT_ARRAY_POINTER_ATI = 0x876A;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //257
        //SUN_mesh_array
        public const uint QUAD_MESH_SUN = 0x8614;
        public const uint TRIANGLE_MESH_SUN = 0x8615;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //258
        //SUN_slice_accum
        public const uint SLICE_ACCUM_SUN = 0x85CC;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //259
        //NV_multisample_filter_hint
        public const uint MULTISAMPLE_FILTER_HINT_NV = 0x8534;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //260
        //NV_depth_clamp
        public const uint DEPTH_CLAMP_NV = 0x864F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //261
        //NV_occlusion_query
        public const uint PIXEL_COUNTER_BITS_NV = 0x8864;
        public const uint CURRENT_OCCLUSION_QUERY_ID_NV = 0x8865;
        public const uint PIXEL_COUNT_NV = 0x8866;
        public const uint PIXEL_COUNT_AVAILABLE_NV = 0x8867;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //262
        //NV_point_sprite
        public const uint POINT_SPRITE_NV = 0x8861;
        public const uint COORD_REPLACE_NV = 0x8862;
        public const uint POINT_SPRITE_R_MODE_NV = 0x8863;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //263 - WGL_NV_render_depth_texture
        //// Extension //264 - WGL_NV_render_texture_rectangle

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //265
        //NV_texture_shader3
        public const uint OFFSET_PROJECTIVE_TEXTURE_2D_NV = 0x8850;
        public const uint OFFSET_PROJECTIVE_TEXTURE_2D_SCALE_NV = 0x8851;
        public const uint OFFSET_PROJECTIVE_TEXTURE_RECTANGLE_NV = 0x8852;
        public const uint OFFSET_PROJECTIVE_TEXTURE_RECTANGLE_SCALE_NV = 0x8853;
        public const uint OFFSET_HILO_TEXTURE_2D_NV = 0x8854;
        public const uint OFFSET_HILO_TEXTURE_RECTANGLE_NV = 0x8855;
        public const uint OFFSET_HILO_PROJECTIVE_TEXTURE_2D_NV = 0x8856;
        public const uint OFFSET_HILO_PROJECTIVE_TEXTURE_RECTANGLE_NV = 0x8857;
        public const uint DEPENDENT_HILO_TEXTURE_2D_NV = 0x8858;
        public const uint DEPENDENT_RGB_TEXTURE_3D_NV = 0x8859;
        public const uint DEPENDENT_RGB_TEXTURE_CUBE_MAP_NV = 0x885A;
        public const uint DOT_PRODUCT_PASS_THROUGH_NV = 0x885B;
        public const uint DOT_PRODUCT_TEXTURE_1D_NV = 0x885C;
        public const uint DOT_PRODUCT_AFFINE_DEPTH_REPLACE_NV = 0x885D;
        public const uint HILO8_NV = 0x885E;
        public const uint SIGNED_HILO8_NV = 0x885F;
        public const uint FORCE_BLUE_TO_ONE_NV = 0x8860;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //266
        //NV_vertex_program1_1

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //267
        //EXT_shadow_funcs

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //268
        //EXT_stencil_two_side
        public const uint STENCIL_TEST_TWO_SIDE_EXT = 0x8910;
        public const uint ACTIVE_STENCIL_FACE_EXT = 0x8911;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //269
        //ATI_text_fragment_shader
        public const uint TEXT_FRAGMENT_SHADER_ATI = 0x8200;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //270
        //APPLE_client_storage
        public const uint UNPACK_CLIENT_STORAGE_APPLE = 0x85B2;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //271
        //APPLE_element_array
        public const uint ELEMENT_ARRAY_APPLE = 0x8A0C;
        public const uint ELEMENT_ARRAY_TYPE_APPLE = 0x8A0D;
        public const uint ELEMENT_ARRAY_POINTER_APPLE = 0x8A0E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //272
        //// ??? BUFFER_OBJECT_APPLE appears to be part of the shipping extension,
        //// but is not in the spec in the registry. Also appears in
        //// APPLE_object_purgeable below.
        //APPLE_fence
        public const uint DRAW_PIXELS_APPLE = 0x8A0A;
        public const uint FENCE_APPLE = 0x8A0B;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //273
        //APPLE_vertex_array_object
        public const uint VERTEX_ARRAY_BINDING_APPLE = 0x85B5;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //274
        //APPLE_vertex_array_range
        public const uint VERTEX_ARRAY_RANGE_APPLE = 0x851D;
        public const uint VERTEX_ARRAY_RANGE_LENGTH_APPLE = 0x851E;
        public const uint VERTEX_ARRAY_STORAGE_HINT_APPLE = 0x851F;
        public const uint VERTEX_ARRAY_RANGE_POINTER_APPLE = 0x8521;
        public const uint STORAGE_CLIENT_APPLE = 0x85B4;
        public const uint STORAGE_CACHED_APPLE = 0x85BE;
        public const uint STORAGE_SHARED_APPLE = 0x85BF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //275
        //APPLE_ycbcr_422
        public const uint YCBCR_422_APPLE = 0x85B9;
        public const uint UNSIGNED_SHORT_8_8_APPLE = 0x85BA;
        public const uint UNSIGNED_SHORT_8_8_REV_APPLE = 0x85BB;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //276
        //S3_s3tc
        public const uint RGB_S3TC = 0x83A0;
        public const uint RGB4_S3TC = 0x83A1;
        public const uint RGBA_S3TC = 0x83A2;
        public const uint RGBA4_S3TC = 0x83A3;
        public const uint RGBA_DXT5_S3TC = 0x83A4;
        public const uint RGBA4_DXT5_S3TC = 0x83A5;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //277
        //ATI_draw_buffers
        public const uint MAX_DRAW_BUFFERS_ATI = 0x8824;
        public const uint DRAW_BUFFER0_ATI = 0x8825;
        public const uint DRAW_BUFFER1_ATI = 0x8826;
        public const uint DRAW_BUFFER2_ATI = 0x8827;
        public const uint DRAW_BUFFER3_ATI = 0x8828;
        public const uint DRAW_BUFFER4_ATI = 0x8829;
        public const uint DRAW_BUFFER5_ATI = 0x882A;
        public const uint DRAW_BUFFER6_ATI = 0x882B;
        public const uint DRAW_BUFFER7_ATI = 0x882C;
        public const uint DRAW_BUFFER8_ATI = 0x882D;
        public const uint DRAW_BUFFER9_ATI = 0x882E;
        public const uint DRAW_BUFFER10_ATI = 0x882F;
        public const uint DRAW_BUFFER11_ATI = 0x8830;
        public const uint DRAW_BUFFER12_ATI = 0x8831;
        public const uint DRAW_BUFFER13_ATI = 0x8832;
        public const uint DRAW_BUFFER14_ATI = 0x8833;
        public const uint DRAW_BUFFER15_ATI = 0x8834;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //278
        //// This is really a WGL extension, but if defined there are
        //// some associated GL enumerants.
        //ATI_pixel_format_float
        public const uint RGBA_FLOAT_MODE_ATI = 0x8820;
        public const uint COLOR_CLEAR_UNCLAMPED_VALUE_ATI = 0x8835;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //279
        //ATI_texture_env_combine3
        public const uint MODULATE_ADD_ATI = 0x8744;
        public const uint MODULATE_SIGNED_ADD_ATI = 0x8745;
        public const uint MODULATE_SUBTRACT_ATI = 0x8746;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //280
        //ATI_texture_float
        public const uint RGBA_FLOAT32_ATI = 0x8814;
        public const uint RGB_FLOAT32_ATI = 0x8815;
        public const uint ALPHA_FLOAT32_ATI = 0x8816;
        public const uint INTENSITY_FLOAT32_ATI = 0x8817;
        public const uint LUMINANCE_FLOAT32_ATI = 0x8818;
        public const uint LUMINANCE_ALPHA_FLOAT32_ATI = 0x8819;
        public const uint RGBA_FLOAT16_ATI = 0x881A;
        public const uint RGB_FLOAT16_ATI = 0x881B;
        public const uint ALPHA_FLOAT16_ATI = 0x881C;
        public const uint INTENSITY_FLOAT16_ATI = 0x881D;
        public const uint LUMINANCE_FLOAT16_ATI = 0x881E;
        public const uint LUMINANCE_ALPHA_FLOAT16_ATI = 0x881F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //281 (also WGL_NV_float_buffer)
        //NV_float_buffer
        public const uint FLOAT_R_NV = 0x8880;
        public const uint FLOAT_RG_NV = 0x8881;
        public const uint FLOAT_RGB_NV = 0x8882;
        public const uint FLOAT_RGBA_NV = 0x8883;
        public const uint FLOAT_R16_NV = 0x8884;
        public const uint FLOAT_R32_NV = 0x8885;
        public const uint FLOAT_RG16_NV = 0x8886;
        public const uint FLOAT_RG32_NV = 0x8887;
        public const uint FLOAT_RGB16_NV = 0x8888;
        public const uint FLOAT_RGB32_NV = 0x8889;
        public const uint FLOAT_RGBA16_NV = 0x888A;
        public const uint FLOAT_RGBA32_NV = 0x888B;
        public const uint TEXTURE_FLOAT_COMPONENTS_NV = 0x888C;
        public const uint FLOAT_CLEAR_COLOR_VALUE_NV = 0x888D;
        public const uint FLOAT_RGBA_MODE_NV = 0x888E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //282
        //NV_fragment_program
        public const uint MAX_FRAGMENT_PROGRAM_LOCAL_PARAMETERS_NV = 0x8868;
        public const uint FRAGMENT_PROGRAM_NV = 0x8870;
        public const uint MAX_TEXTURE_COORDS_NV = 0x8871;
        public const uint MAX_TEXTURE_IMAGE_UNITS_NV = 0x8872;
        public const uint FRAGMENT_PROGRAM_BINDING_NV = 0x8873;
        public const uint PROGRAM_ERROR_STRING_NV = 0x8874;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //283
        //NV_half_float
        public const uint HALF_FLOAT_NV = 0x140B;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //284
        //NV_pixel_data_range
        public const uint WRITE_PIXEL_DATA_RANGE_NV = 0x8878;
        public const uint READ_PIXEL_DATA_RANGE_NV = 0x8879;
        public const uint WRITE_PIXEL_DATA_RANGE_LENGTH_NV = 0x887A;
        public const uint READ_PIXEL_DATA_RANGE_LENGTH_NV = 0x887B;
        public const uint WRITE_PIXEL_DATA_RANGE_POINTER_NV = 0x887C;
        public const uint READ_PIXEL_DATA_RANGE_POINTER_NV = 0x887D;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //285
        //NV_primitive_restart
        public const uint PRIMITIVE_RESTART_NV = 0x8558;
        public const uint PRIMITIVE_RESTART_INDEX_NV = 0x8559;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //286
        //NV_texture_expand_normal
        public const uint TEXTURE_UNSIGNED_REMAP_MODE_NV = 0x888F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //287
        //NV_vertex_program2

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //288
        //ATI_map_object_buffer

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //289
        //ATI_separate_stencil
        public const uint STENCIL_BACK_FUNC_ATI = 0x8800;
        public const uint STENCIL_BACK_FAIL_ATI = 0x8801;
        public const uint STENCIL_BACK_PASS_DEPTH_FAIL_ATI = 0x8802;
        public const uint STENCIL_BACK_PASS_DEPTH_PASS_ATI = 0x8803;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //290
        //ATI_vertex_attrib_array_object

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //291 - OpenGL ES only
        //OES_byte_coordinates

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //292 - OpenGL ES only
        //OES_fixed_point
        public const uint FIXED_OES = 0x140C;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //293 - OpenGL ES only
        //OES_single_precision

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //294 - OpenGL ES only
        //OES_compressed_paletted_texture
        public const uint PALETTE4_RGB8_OES = 0x8B90;
        public const uint PALETTE4_RGBA8_OES = 0x8B91;
        public const uint PALETTE4_R5_G6_B5_OES = 0x8B92;
        public const uint PALETTE4_RGBA4_OES = 0x8B93;
        public const uint PALETTE4_RGB5_A1_OES = 0x8B94;
        public const uint PALETTE8_RGB8_OES = 0x8B95;
        public const uint PALETTE8_RGBA8_OES = 0x8B96;
        public const uint PALETTE8_R5_G6_B5_OES = 0x8B97;
        public const uint PALETTE8_RGBA4_OES = 0x8B98;
        public const uint PALETTE8_RGB5_A1_OES = 0x8B99;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //295 - This is an OpenGL ES extension, but also implemented in Mesa
        //OES_read_format
        public const uint IMPLEMENTATION_COLOR_READ_TYPE_OES = 0x8B9A;
        public const uint IMPLEMENTATION_COLOR_READ_FORMAT_OES = 0x8B9B;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //296 - OpenGL ES only
        //OES_query_matrix

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //297
        //EXT_depth_bounds_test
        public const uint DEPTH_BOUNDS_TEST_EXT = 0x8890;
        public const uint DEPTH_BOUNDS_EXT = 0x8891;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //298
        //EXT_texture_mirror_clamp
        public const uint MIRROR_CLAMP_EXT = 0x8742;
        public const uint MIRROR_CLAMP_TO_EDGE_EXT = 0x8743;
        public const uint MIRROR_CLAMP_TO_BORDER_EXT = 0x8912;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //299
        //EXT_blend_equation_separate
        public const uint BLEND_EQUATION_RGB_EXT = 0x8009;    // alias GL_BLEND_EQUATION_EXT
        public const uint BLEND_EQUATION_ALPHA_EXT = 0x883D;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //300
        //MESA_pack_invert
        public const uint PACK_INVERT_MESA = 0x8758;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //301
        //MESA_ycbcr_texture
        public const uint UNSIGNED_SHORT_8_8_MESA = 0x85BA;
        public const uint UNSIGNED_SHORT_8_8_REV_MESA = 0x85BB;
        public const uint YCBCR_MESA = 0x8757;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //302
        //EXT_pixel_buffer_object
        public const uint PIXEL_PACK_BUFFER_EXT = 0x88EB;
        public const uint PIXEL_UNPACK_BUFFER_EXT = 0x88EC;
        public const uint PIXEL_PACK_BUFFER_BINDING_EXT = 0x88ED;
        public const uint PIXEL_UNPACK_BUFFER_BINDING_EXT = 0x88EF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //303
        //NV_fragment_program_option

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //304
        //NV_fragment_program2
        public const uint MAX_PROGRAM_EXEC_INSTRUCTIONS_NV = 0x88F4;
        public const uint MAX_PROGRAM_CALL_DEPTH_NV = 0x88F5;
        public const uint MAX_PROGRAM_IF_DEPTH_NV = 0x88F6;
        public const uint MAX_PROGRAM_LOOP_DEPTH_NV = 0x88F7;
        public const uint MAX_PROGRAM_LOOP_COUNT_NV = 0x88F8;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //305
        //NV_vertex_program2_option
        //    use NV_fragment_program2	    MAX_PROGRAM_EXEC_INSTRUCTIONS_NV
        //    use NV_fragment_program2	    MAX_PROGRAM_CALL_DEPTH_NV

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //306
        //NV_vertex_program3
        //    use ARB_vertex_shader		    MAX_VERTEX_TEXTURE_IMAGE_UNITS_ARB

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //307 - GLX_SGIX_hyperpipe
        //// Extension //308 - GLX_MESA_agp_offset

        //// Extension //309 - GL_EXT_texture_compression_dxt1 (OpenGL ES only, subset of _s3tc version)
        ////	 use EXT_texture_compression_s3tc    COMPRESSED_RGB_S3TC_DXT1_EXT
        ////	 use EXT_texture_compression_s3tc    COMPRESSED_RGBA_S3TC_DXT1_EXT

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //310
        //EXT_framebuffer_object
        public const uint INVALID_FRAMEBUFFER_OPERATION_EXT = 0x0506;
        public const uint MAX_RENDERBUFFER_SIZE_EXT = 0x84E8;
        public const uint FRAMEBUFFER_BINDING_EXT = 0x8CA6;
        public const uint RENDERBUFFER_BINDING_EXT = 0x8CA7;
        public const uint FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE_EXT = 0x8CD0;
        public const uint FRAMEBUFFER_ATTACHMENT_OBJECT_NAME_EXT = 0x8CD1;
        public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL_EXT = 0x8CD2;
        public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE_EXT = 0x8CD3;
        public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_3D_ZOFFSET_EXT = 0x8CD4;
        public const uint FRAMEBUFFER_COMPLETE_EXT = 0x8CD5;
        public const uint FRAMEBUFFER_INCOMPLETE_ATTACHMENT_EXT = 0x8CD6;
        public const uint FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT_EXT = 0x8CD7;
        ////// Removed 2005/09/26 in revision //117 of the extension:
        ////public const uint FRAMEBUFFER_INCOMPLETE_DUPLICATE_ATTACHMENT_EXT = 0x8CD8;
        public const uint FRAMEBUFFER_INCOMPLETE_DIMENSIONS_EXT = 0x8CD9;
        public const uint FRAMEBUFFER_INCOMPLETE_FORMATS_EXT = 0x8CDA;
        public const uint FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER_EXT = 0x8CDB;
        public const uint FRAMEBUFFER_INCOMPLETE_READ_BUFFER_EXT = 0x8CDC;
        public const uint FRAMEBUFFER_UNSUPPORTED_EXT = 0x8CDD;
        ////// Removed 2005/05/31 in revision //113 of the extension:
        ////public const uint FRAMEBUFFER_STATUS_ERROR_EXT = 0x8CDE;
        public const uint MAX_COLOR_ATTACHMENTS_EXT = 0x8CDF;
        public const uint COLOR_ATTACHMENT0_EXT = 0x8CE0;
        public const uint COLOR_ATTACHMENT1_EXT = 0x8CE1;
        public const uint COLOR_ATTACHMENT2_EXT = 0x8CE2;
        public const uint COLOR_ATTACHMENT3_EXT = 0x8CE3;
        public const uint COLOR_ATTACHMENT4_EXT = 0x8CE4;
        public const uint COLOR_ATTACHMENT5_EXT = 0x8CE5;
        public const uint COLOR_ATTACHMENT6_EXT = 0x8CE6;
        public const uint COLOR_ATTACHMENT7_EXT = 0x8CE7;
        public const uint COLOR_ATTACHMENT8_EXT = 0x8CE8;
        public const uint COLOR_ATTACHMENT9_EXT = 0x8CE9;
        public const uint COLOR_ATTACHMENT10_EXT = 0x8CEA;
        public const uint COLOR_ATTACHMENT11_EXT = 0x8CEB;
        public const uint COLOR_ATTACHMENT12_EXT = 0x8CEC;
        public const uint COLOR_ATTACHMENT13_EXT = 0x8CED;
        public const uint COLOR_ATTACHMENT14_EXT = 0x8CEE;
        public const uint COLOR_ATTACHMENT15_EXT = 0x8CEF;
        public const uint DEPTH_ATTACHMENT_EXT = 0x8D00;
        public const uint STENCIL_ATTACHMENT_EXT = 0x8D20;
        public const uint FRAMEBUFFER_EXT = 0x8D40;
        public const uint RENDERBUFFER_EXT = 0x8D41;
        public const uint RENDERBUFFER_WIDTH_EXT = 0x8D42;
        public const uint RENDERBUFFER_HEIGHT_EXT = 0x8D43;
        public const uint RENDERBUFFER_INTERNAL_FORMAT_EXT = 0x8D44;
        //public const uint removed STENCIL_INDEX_EXT = 0x8D45; in rev. //114 of the spec
        public const uint STENCIL_INDEX1_EXT = 0x8D46;
        public const uint STENCIL_INDEX4_EXT = 0x8D47;
        public const uint STENCIL_INDEX8_EXT = 0x8D48;
        public const uint STENCIL_INDEX16_EXT = 0x8D49;
        public const uint RENDERBUFFER_RED_SIZE_EXT = 0x8D50;
        public const uint RENDERBUFFER_GREEN_SIZE_EXT = 0x8D51;
        public const uint RENDERBUFFER_BLUE_SIZE_EXT = 0x8D52;
        public const uint RENDERBUFFER_ALPHA_SIZE_EXT = 0x8D53;
        public const uint RENDERBUFFER_DEPTH_SIZE_EXT = 0x8D54;
        public const uint RENDERBUFFER_STENCIL_SIZE_EXT = 0x8D55;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //311
        //GREMEDY_string_marker

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //312
        //EXT_packed_depth_stencil
        public const uint DEPTH_STENCIL_EXT = 0x84F9;
        public const uint UNSIGNED_INT_24_8_EXT = 0x84FA;
        public const uint DEPTH24_STENCIL8_EXT = 0x88F0;
        public const uint TEXTURE_STENCIL_SIZE_EXT = 0x88F1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //313 - WGL_3DL_stereo_control

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //314
        //EXT_stencil_clear_tag
        public const uint STENCIL_TAG_BITS_EXT = 0x88F2;
        public const uint STENCIL_CLEAR_TAG_VALUE_EXT = 0x88F3;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //315
        //EXT_texture_sRGB
        public const uint SRGB_EXT = 0x8C40;
        public const uint SRGB8_EXT = 0x8C41;
        public const uint SRGB_ALPHA_EXT = 0x8C42;
        public const uint SRGB8_ALPHA8_EXT = 0x8C43;
        public const uint SLUMINANCE_ALPHA_EXT = 0x8C44;
        public const uint SLUMINANCE8_ALPHA8_EXT = 0x8C45;
        public const uint SLUMINANCE_EXT = 0x8C46;
        public const uint SLUMINANCE8_EXT = 0x8C47;
        public const uint COMPRESSED_SRGB_EXT = 0x8C48;
        public const uint COMPRESSED_SRGB_ALPHA_EXT = 0x8C49;
        public const uint COMPRESSED_SLUMINANCE_EXT = 0x8C4A;
        public const uint COMPRESSED_SLUMINANCE_ALPHA_EXT = 0x8C4B;
        public const uint COMPRESSED_SRGB_S3TC_DXT1_EXT = 0x8C4C;
        public const uint COMPRESSED_SRGB_ALPHA_S3TC_DXT1_EXT = 0x8C4D;
        public const uint COMPRESSED_SRGB_ALPHA_S3TC_DXT3_EXT = 0x8C4E;
        public const uint COMPRESSED_SRGB_ALPHA_S3TC_DXT5_EXT = 0x8C4F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //316
        //EXT_framebuffer_blit
        public const uint READ_FRAMEBUFFER_EXT = 0x8CA8;
        public const uint DRAW_FRAMEBUFFER_EXT = 0x8CA9;
        public const uint DRAW_FRAMEBUFFER_BINDING_EXT = 0x8CA6;    // alias FRAMEBUFFER_BINDING_EXT
        public const uint READ_FRAMEBUFFER_BINDING_EXT = 0x8CAA;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //317
        //EXT_framebuffer_multisample
        public const uint RENDERBUFFER_SAMPLES_EXT = 0x8CAB;
        public const uint FRAMEBUFFER_INCOMPLETE_MULTISAMPLE_EXT = 0x8D56;
        public const uint MAX_SAMPLES_EXT = 0x8D57;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //318
        //MESAX_texture_stack
        public const uint TEXTURE_1D_STACK_MESAX = 0x8759;
        public const uint TEXTURE_2D_STACK_MESAX = 0x875A;
        public const uint PROXY_TEXTURE_1D_STACK_MESAX = 0x875B;
        public const uint PROXY_TEXTURE_2D_STACK_MESAX = 0x875C;
        public const uint TEXTURE_1D_STACK_BINDING_MESAX = 0x875D;
        public const uint TEXTURE_2D_STACK_BINDING_MESAX = 0x875E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //319
        //EXT_timer_query
        public const uint TIME_ELAPSED_EXT = 0x88BF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //320
        //EXT_gpu_program_parameters

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //321
        //APPLE_flush_buffer_range
        public const uint BUFFER_SERIALIZED_MODIFY_APPLE = 0x8A12;
        public const uint BUFFER_FLUSHING_UNMAP_APPLE = 0x8A13;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //322
        //NV_gpu_program4
        public const uint MIN_PROGRAM_TEXEL_OFFSET_NV = 0x8904;
        public const uint MAX_PROGRAM_TEXEL_OFFSET_NV = 0x8905;
        public const uint PROGRAM_ATTRIB_COMPONENTS_NV = 0x8906;
        public const uint PROGRAM_RESULT_COMPONENTS_NV = 0x8907;
        public const uint MAX_PROGRAM_ATTRIB_COMPONENTS_NV = 0x8908;
        public const uint MAX_PROGRAM_RESULT_COMPONENTS_NV = 0x8909;
        public const uint MAX_PROGRAM_GENERIC_ATTRIBS_NV = 0x8DA5;
        public const uint MAX_PROGRAM_GENERIC_RESULTS_NV = 0x8DA6;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //323
        //NV_geometry_program4
        public const uint LINES_ADJACENCY_EXT = 0x000A;
        public const uint LINE_STRIP_ADJACENCY_EXT = 0x000B;
        public const uint TRIANGLES_ADJACENCY_EXT = 0x000C;
        public const uint TRIANGLE_STRIP_ADJACENCY_EXT = 0x000D;
        public const uint GEOMETRY_PROGRAM_NV = 0x8C26;
        public const uint MAX_PROGRAM_OUTPUT_VERTICES_NV = 0x8C27;
        public const uint MAX_PROGRAM_TOTAL_OUTPUT_COMPONENTS_NV = 0x8C28;
        public const uint GEOMETRY_VERTICES_OUT_EXT = 0x8DDA;
        public const uint GEOMETRY_INPUT_TYPE_EXT = 0x8DDB;
        public const uint GEOMETRY_OUTPUT_TYPE_EXT = 0x8DDC;
        public const uint MAX_GEOMETRY_TEXTURE_IMAGE_UNITS_EXT = 0x8C29;
        public const uint FRAMEBUFFER_ATTACHMENT_LAYERED_EXT = 0x8DA7;
        public const uint FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS_EXT = 0x8DA8;
        public const uint FRAMEBUFFER_INCOMPLETE_LAYER_COUNT_EXT = 0x8DA9;
        public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER_EXT = 0x8CD4;
        public const uint PROGRAM_POINT_SIZE_EXT = 0x8642;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //324
        //EXT_geometry_shader4
        public const uint GEOMETRY_SHADER_EXT = 0x8DD9;
        //    use NV_geometry_program4	    GEOMETRY_VERTICES_OUT_EXT
        //    use NV_geometry_program4	    GEOMETRY_INPUT_TYPE_EXT
        //    use NV_geometry_program4	    GEOMETRY_OUTPUT_TYPE_EXT
        //    use NV_geometry_program4	    MAX_GEOMETRY_TEXTURE_IMAGE_UNITS_EXT
        public const uint MAX_GEOMETRY_VARYING_COMPONENTS_EXT = 0x8DDD;
        public const uint MAX_VERTEX_VARYING_COMPONENTS_EXT = 0x8DDE;
        public const uint MAX_VARYING_COMPONENTS_EXT = 0x8B4B;
        public const uint MAX_GEOMETRY_UNIFORM_COMPONENTS_EXT = 0x8DDF;
        public const uint MAX_GEOMETRY_OUTPUT_VERTICES_EXT = 0x8DE0;
        public const uint MAX_GEOMETRY_TOTAL_OUTPUT_COMPONENTS_EXT = 0x8DE1;
        //    use NV_geometry_program4	    LINES_ADJACENCY_EXT
        //    use NV_geometry_program4	    LINE_STRIP_ADJACENCY_EXT
        //    use NV_geometry_program4	    TRIANGLES_ADJACENCY_EXT
        //    use NV_geometry_program4	    TRIANGLE_STRIP_ADJACENCY_EXT
        //    use NV_geometry_program4	    FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS_EXT
        //    use NV_geometry_program4	    FRAMEBUFFER_INCOMPLETE_LAYER_COUNT_EXT
        //    use NV_geometry_program4	    FRAMEBUFFER_ATTACHMENT_LAYERED_EXT
        //    use NV_geometry_program4	    FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER_EXT
        //    use NV_geometry_program4	    PROGRAM_POINT_SIZE_EXT

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //325
        //NV_vertex_program4
        public const uint VERTEX_ATTRIB_ARRAY_INTEGER_NV = 0x88FD;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //326
        //EXT_gpu_shader4
        public const uint SAMPLER_1D_ARRAY_EXT = 0x8DC0;
        public const uint SAMPLER_2D_ARRAY_EXT = 0x8DC1;
        public const uint SAMPLER_BUFFER_EXT = 0x8DC2;
        public const uint SAMPLER_1D_ARRAY_SHADOW_EXT = 0x8DC3;
        public const uint SAMPLER_2D_ARRAY_SHADOW_EXT = 0x8DC4;
        public const uint SAMPLER_CUBE_SHADOW_EXT = 0x8DC5;
        public const uint UNSIGNED_INT_VEC2_EXT = 0x8DC6;
        public const uint UNSIGNED_INT_VEC3_EXT = 0x8DC7;
        public const uint UNSIGNED_INT_VEC4_EXT = 0x8DC8;
        public const uint INT_SAMPLER_1D_EXT = 0x8DC9;
        public const uint INT_SAMPLER_2D_EXT = 0x8DCA;
        public const uint INT_SAMPLER_3D_EXT = 0x8DCB;
        public const uint INT_SAMPLER_CUBE_EXT = 0x8DCC;
        public const uint INT_SAMPLER_2D_RECT_EXT = 0x8DCD;
        public const uint INT_SAMPLER_1D_ARRAY_EXT = 0x8DCE;
        public const uint INT_SAMPLER_2D_ARRAY_EXT = 0x8DCF;
        public const uint INT_SAMPLER_BUFFER_EXT = 0x8DD0;
        public const uint UNSIGNED_INT_SAMPLER_1D_EXT = 0x8DD1;
        public const uint UNSIGNED_INT_SAMPLER_2D_EXT = 0x8DD2;
        public const uint UNSIGNED_INT_SAMPLER_3D_EXT = 0x8DD3;
        public const uint UNSIGNED_INT_SAMPLER_CUBE_EXT = 0x8DD4;
        public const uint UNSIGNED_INT_SAMPLER_2D_RECT_EXT = 0x8DD5;
        public const uint UNSIGNED_INT_SAMPLER_1D_ARRAY_EXT = 0x8DD6;
        public const uint UNSIGNED_INT_SAMPLER_2D_ARRAY_EXT = 0x8DD7;
        public const uint UNSIGNED_INT_SAMPLER_BUFFER_EXT = 0x8DD8;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //327
        //EXT_draw_instanced

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //328
        //EXT_packed_float
        public const uint R11F_G11F_B10F_EXT = 0x8C3A;
        public const uint UNSIGNED_INT_10F_11F_11F_REV_EXT = 0x8C3B;
        public const uint RGBA_SIGNED_COMPONENTS_EXT = 0x8C3C;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //329
        //EXT_texture_array
        public const uint TEXTURE_1D_ARRAY_EXT = 0x8C18;
        public const uint PROXY_TEXTURE_1D_ARRAY_EXT = 0x8C19;
        public const uint TEXTURE_2D_ARRAY_EXT = 0x8C1A;
        public const uint PROXY_TEXTURE_2D_ARRAY_EXT = 0x8C1B;
        public const uint TEXTURE_BINDING_1D_ARRAY_EXT = 0x8C1C;
        public const uint TEXTURE_BINDING_2D_ARRAY_EXT = 0x8C1D;
        public const uint MAX_ARRAY_TEXTURE_LAYERS_EXT = 0x88FF;
        public const uint COMPARE_REF_DEPTH_TO_TEXTURE_EXT = 0x884E;
        //    use NV_geometry_program4	    FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER_EXT

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //330
        //EXT_texture_buffer_object
        public const uint TEXTURE_BUFFER_EXT = 0x8C2A;
        public const uint MAX_TEXTURE_BUFFER_SIZE_EXT = 0x8C2B;
        public const uint TEXTURE_BINDING_BUFFER_EXT = 0x8C2C;
        public const uint TEXTURE_BUFFER_DATA_STORE_BINDING_EXT = 0x8C2D;
        public const uint TEXTURE_BUFFER_FORMAT_EXT = 0x8C2E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //331
        //EXT_texture_compression_latc
        public const uint COMPRESSED_LUMINANCE_LATC1_EXT = 0x8C70;
        public const uint COMPRESSED_SIGNED_LUMINANCE_LATC1_EXT = 0x8C71;
        public const uint COMPRESSED_LUMINANCE_ALPHA_LATC2_EXT = 0x8C72;
        public const uint COMPRESSED_SIGNED_LUMINANCE_ALPHA_LATC2_EXT = 0x8C73;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //332
        //EXT_texture_compression_rgtc
        public const uint COMPRESSED_RED_RGTC1_EXT = 0x8DBB;
        public const uint COMPRESSED_SIGNED_RED_RGTC1_EXT = 0x8DBC;
        public const uint COMPRESSED_RED_GREEN_RGTC2_EXT = 0x8DBD;
        public const uint COMPRESSED_SIGNED_RED_GREEN_RGTC2_EXT = 0x8DBE;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //333
        //EXT_texture_shared_exponent
        public const uint RGB9_E5_EXT = 0x8C3D;
        public const uint UNSIGNED_INT_5_9_9_9_REV_EXT = 0x8C3E;
        public const uint TEXTURE_SHARED_SIZE_EXT = 0x8C3F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //334
        //NV_depth_buffer_float
        public const uint DEPTH_COMPONENT32F_NV = 0x8DAB;
        public const uint DEPTH32F_STENCIL8_NV = 0x8DAC;
        public const uint FLOAT_32_UNSIGNED_INT_24_8_REV_NV = 0x8DAD;
        public const uint DEPTH_BUFFER_FLOAT_MODE_NV = 0x8DAF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //335
        //NV_fragment_program4

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //336
        //NV_framebuffer_multisample_coverage
        public const uint RENDERBUFFER_COVERAGE_SAMPLES_NV = 0x8CAB;
        public const uint RENDERBUFFER_COLOR_SAMPLES_NV = 0x8E10;
        public const uint MAX_MULTISAMPLE_COVERAGE_MODES_NV = 0x8E11;
        public const uint MULTISAMPLE_COVERAGE_MODES_NV = 0x8E12;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //337
        //// ??? Also WGL/GLX extensions ???
        //EXT_framebuffer_sRGB
        public const uint FRAMEBUFFER_SRGB_EXT = 0x8DB9;
        public const uint FRAMEBUFFER_SRGB_CAPABLE_EXT = 0x8DBA;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //338
        //NV_geometry_shader4

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //339
        //NV_parameter_buffer_object
        public const uint MAX_PROGRAM_PARAMETER_BUFFER_BINDINGS_NV = 0x8DA0;
        public const uint MAX_PROGRAM_PARAMETER_BUFFER_SIZE_NV = 0x8DA1;
        public const uint VERTEX_PROGRAM_PARAMETER_BUFFER_NV = 0x8DA2;
        public const uint GEOMETRY_PROGRAM_PARAMETER_BUFFER_NV = 0x8DA3;
        public const uint FRAGMENT_PROGRAM_PARAMETER_BUFFER_NV = 0x8DA4;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //340
        //EXT_draw_buffers2

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //341
        //NV_transform_feedback
        public const uint BACK_PRIMARY_COLOR_NV = 0x8C77;
        public const uint BACK_SECONDARY_COLOR_NV = 0x8C78;
        public const uint TEXTURE_COORD_NV = 0x8C79;
        public const uint CLIP_DISTANCE_NV = 0x8C7A;
        public const uint VERTEX_ID_NV = 0x8C7B;
        public const uint PRIMITIVE_ID_NV = 0x8C7C;
        public const uint GENERIC_ATTRIB_NV = 0x8C7D;
        public const uint TRANSFORM_FEEDBACK_ATTRIBS_NV = 0x8C7E;
        public const uint TRANSFORM_FEEDBACK_BUFFER_MODE_NV = 0x8C7F;
        public const uint MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS_NV = 0x8C80;
        public const uint ACTIVE_VARYINGS_NV = 0x8C81;
        public const uint ACTIVE_VARYING_MAX_LENGTH_NV = 0x8C82;
        public const uint TRANSFORM_FEEDBACK_VARYINGS_NV = 0x8C83;
        public const uint TRANSFORM_FEEDBACK_BUFFER_START_NV = 0x8C84;
        public const uint TRANSFORM_FEEDBACK_BUFFER_SIZE_NV = 0x8C85;
        public const uint TRANSFORM_FEEDBACK_RECORD_NV = 0x8C86;
        public const uint PRIMITIVES_GENERATED_NV = 0x8C87;
        public const uint TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN_NV = 0x8C88;
        public const uint RASTERIZER_DISCARD_NV = 0x8C89;
        public const uint MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS_NV = 0x8C8A;
        public const uint MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS_NV = 0x8C8B;
        public const uint INTERLEAVED_ATTRIBS_NV = 0x8C8C;
        public const uint SEPARATE_ATTRIBS_NV = 0x8C8D;
        public const uint TRANSFORM_FEEDBACK_BUFFER_NV = 0x8C8E;
        public const uint TRANSFORM_FEEDBACK_BUFFER_BINDING_NV = 0x8C8F;
        public const uint LAYER_NV = 0x8DAA;
        public const int NEXT_BUFFER_NV = -2;	    // Requires ARB_transform_feedback3
        public const int SKIP_COMPONENTS4_NV = -3;	    // Requires ARB_transform_feedback3
        public const int SKIP_COMPONENTS3_NV = -4;	    // Requires ARB_transform_feedback3
        public const int SKIP_COMPONENTS2_NV = -5;	    // Requires ARB_transform_feedback3
        public const int SKIP_COMPONENTS1_NV = -6;	    // Requires ARB_transform_feedback3

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //342
        //EXT_bindable_uniform
        public const uint MAX_VERTEX_BINDABLE_UNIFORMS_EXT = 0x8DE2;
        public const uint MAX_FRAGMENT_BINDABLE_UNIFORMS_EXT = 0x8DE3;
        public const uint MAX_GEOMETRY_BINDABLE_UNIFORMS_EXT = 0x8DE4;
        public const uint MAX_BINDABLE_UNIFORM_SIZE_EXT = 0x8DED;
        public const uint UNIFORM_BUFFER_EXT = 0x8DEE;
        public const uint UNIFORM_BUFFER_BINDING_EXT = 0x8DEF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //343
        //EXT_texture_integer
        public const uint RGBA32UI_EXT = 0x8D70;
        public const uint RGB32UI_EXT = 0x8D71;
        public const uint ALPHA32UI_EXT = 0x8D72;
        public const uint INTENSITY32UI_EXT = 0x8D73;
        public const uint LUMINANCE32UI_EXT = 0x8D74;
        public const uint LUMINANCE_ALPHA32UI_EXT = 0x8D75;
        public const uint RGBA16UI_EXT = 0x8D76;
        public const uint RGB16UI_EXT = 0x8D77;
        public const uint ALPHA16UI_EXT = 0x8D78;
        public const uint INTENSITY16UI_EXT = 0x8D79;
        public const uint LUMINANCE16UI_EXT = 0x8D7A;
        public const uint LUMINANCE_ALPHA16UI_EXT = 0x8D7B;
        public const uint RGBA8UI_EXT = 0x8D7C;
        public const uint RGB8UI_EXT = 0x8D7D;
        public const uint ALPHA8UI_EXT = 0x8D7E;
        public const uint INTENSITY8UI_EXT = 0x8D7F;
        public const uint LUMINANCE8UI_EXT = 0x8D80;
        public const uint LUMINANCE_ALPHA8UI_EXT = 0x8D81;
        public const uint RGBA32I_EXT = 0x8D82;
        public const uint RGB32I_EXT = 0x8D83;
        public const uint ALPHA32I_EXT = 0x8D84;
        public const uint INTENSITY32I_EXT = 0x8D85;
        public const uint LUMINANCE32I_EXT = 0x8D86;
        public const uint LUMINANCE_ALPHA32I_EXT = 0x8D87;
        public const uint RGBA16I_EXT = 0x8D88;
        public const uint RGB16I_EXT = 0x8D89;
        public const uint ALPHA16I_EXT = 0x8D8A;
        public const uint INTENSITY16I_EXT = 0x8D8B;
        public const uint LUMINANCE16I_EXT = 0x8D8C;
        public const uint LUMINANCE_ALPHA16I_EXT = 0x8D8D;
        public const uint RGBA8I_EXT = 0x8D8E;
        public const uint RGB8I_EXT = 0x8D8F;
        public const uint ALPHA8I_EXT = 0x8D90;
        public const uint INTENSITY8I_EXT = 0x8D91;
        public const uint LUMINANCE8I_EXT = 0x8D92;
        public const uint LUMINANCE_ALPHA8I_EXT = 0x8D93;
        public const uint RED_INTEGER_EXT = 0x8D94;
        public const uint GREEN_INTEGER_EXT = 0x8D95;
        public const uint BLUE_INTEGER_EXT = 0x8D96;
        public const uint ALPHA_INTEGER_EXT = 0x8D97;
        public const uint RGB_INTEGER_EXT = 0x8D98;
        public const uint RGBA_INTEGER_EXT = 0x8D99;
        public const uint BGR_INTEGER_EXT = 0x8D9A;
        public const uint BGRA_INTEGER_EXT = 0x8D9B;
        public const uint LUMINANCE_INTEGER_EXT = 0x8D9C;
        public const uint LUMINANCE_ALPHA_INTEGER_EXT = 0x8D9D;
        public const uint RGBA_INTEGER_MODE_EXT = 0x8D9E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //344 - GLX_EXT_texture_from_pixmap

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //345
        //GREMEDY_frame_terminator

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //346
        //NV_conditional_render
        public const uint QUERY_WAIT_NV = 0x8E13;
        public const uint QUERY_NO_WAIT_NV = 0x8E14;
        public const uint QUERY_BY_REGION_WAIT_NV = 0x8E15;
        public const uint QUERY_BY_REGION_NO_WAIT_NV = 0x8E16;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //347
        //NV_present_video
        public const uint FRAME_NV = 0x8E26;
        public const uint FIELDS_NV = 0x8E27;
        public const uint CURRENT_TIME_NV = 0x8E28;
        public const uint NUM_FILL_STREAMS_NV = 0x8E29;
        public const uint PRESENT_TIME_NV = 0x8E2A;
        public const uint PRESENT_DURATION_NV = 0x8E2B;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //348 - GLX_NV_video_out
        //// Extension //349 - WGL_NV_video_out
        //// Extension //350 - GLX_NV_swap_group
        //// Extension //351 - WGL_NV_swap_group

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //352
        //EXT_transform_feedback
        public const uint TRANSFORM_FEEDBACK_BUFFER_EXT = 0x8C8E;
        public const uint TRANSFORM_FEEDBACK_BUFFER_START_EXT = 0x8C84;
        public const uint TRANSFORM_FEEDBACK_BUFFER_SIZE_EXT = 0x8C85;
        public const uint TRANSFORM_FEEDBACK_BUFFER_BINDING_EXT = 0x8C8F;
        public const uint INTERLEAVED_ATTRIBS_EXT = 0x8C8C;
        public const uint SEPARATE_ATTRIBS_EXT = 0x8C8D;
        public const uint PRIMITIVES_GENERATED_EXT = 0x8C87;
        public const uint TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN_EXT = 0x8C88;
        public const uint RASTERIZER_DISCARD_EXT = 0x8C89;
        public const uint MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS_EXT = 0x8C8A;
        public const uint MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS_EXT = 0x8C8B;
        public const uint MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS_EXT = 0x8C80;
        public const uint TRANSFORM_FEEDBACK_VARYINGS_EXT = 0x8C83;
        public const uint TRANSFORM_FEEDBACK_BUFFER_MODE_EXT = 0x8C7F;
        public const uint TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH_EXT = 0x8C76;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //353
        //EXT_direct_state_access
        public const uint PROGRAM_MATRIX_EXT = 0x8E2D;
        public const uint TRANSPOSE_PROGRAM_MATRIX_EXT = 0x8E2E;
        public const uint PROGRAM_MATRIX_STACK_DEPTH_EXT = 0x8E2F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //354
        //EXT_vertex_array_bgra
        //    use VERSION_1_2			    BGRA

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //355 - WGL_NV_gpu_affinity

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //356
        //EXT_texture_swizzle
        public const uint TEXTURE_SWIZZLE_R_EXT = 0x8E42;
        public const uint TEXTURE_SWIZZLE_G_EXT = 0x8E43;
        public const uint TEXTURE_SWIZZLE_B_EXT = 0x8E44;
        public const uint TEXTURE_SWIZZLE_A_EXT = 0x8E45;
        public const uint TEXTURE_SWIZZLE_RGBA_EXT = 0x8E46;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //357
        //NV_explicit_multisample
        public const uint SAMPLE_POSITION_NV = 0x8E50;
        public const uint SAMPLE_MASK_NV = 0x8E51;
        public const uint SAMPLE_MASK_VALUE_NV = 0x8E52;
        public const uint TEXTURE_BINDING_RENDERBUFFER_NV = 0x8E53;
        public const uint TEXTURE_RENDERBUFFER_DATA_STORE_BINDING_NV = 0x8E54;
        public const uint TEXTURE_RENDERBUFFER_NV = 0x8E55;
        public const uint SAMPLER_RENDERBUFFER_NV = 0x8E56;
        public const uint INT_SAMPLER_RENDERBUFFER_NV = 0x8E57;
        public const uint UNSIGNED_INT_SAMPLER_RENDERBUFFER_NV = 0x8E58;
        public const uint MAX_SAMPLE_MASK_WORDS_NV = 0x8E59;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //358
        //NV_transform_feedback2
        public const uint TRANSFORM_FEEDBACK_NV = 0x8E22;
        public const uint TRANSFORM_FEEDBACK_BUFFER_PAUSED_NV = 0x8E23;
        public const uint TRANSFORM_FEEDBACK_BUFFER_ACTIVE_NV = 0x8E24;
        public const uint TRANSFORM_FEEDBACK_BINDING_NV = 0x8E25;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //359
        //ATI_meminfo
        public const uint VBO_FREE_MEMORY_ATI = 0x87FB;
        public const uint TEXTURE_FREE_MEMORY_ATI = 0x87FC;
        public const uint RENDERBUFFER_FREE_MEMORY_ATI = 0x87FD;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //360
        //AMD_performance_monitor
        public const uint COUNTER_TYPE_AMD = 0x8BC0;
        public const uint COUNTER_RANGE_AMD = 0x8BC1;
        public const uint UNSIGNED_INT64_AMD = 0x8BC2;
        public const uint PERCENTAGE_AMD = 0x8BC3;
        public const uint PERFMON_RESULT_AVAILABLE_AMD = 0x8BC4;
        public const uint PERFMON_RESULT_SIZE_AMD = 0x8BC5;
        public const uint PERFMON_RESULT_AMD = 0x8BC6;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //361 - WGL_AMD_gpu_association

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //362
        //AMD_texture_texture4

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //363
        //AMD_vertex_shader_tessellator
        public const uint SAMPLER_BUFFER_AMD = 0x9001;
        public const uint INT_SAMPLER_BUFFER_AMD = 0x9002;
        public const uint UNSIGNED_INT_SAMPLER_BUFFER_AMD = 0x9003;
        public const uint TESSELLATION_MODE_AMD = 0x9004;
        public const uint TESSELLATION_FACTOR_AMD = 0x9005;
        public const uint DISCRETE_AMD = 0x9006;
        public const uint CONTINUOUS_AMD = 0x9007;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //364
        //EXT_provoking_vertex
        public const uint QUADS_FOLLOW_PROVOKING_VERTEX_CONVENTION_EXT = 0x8E4C;
        public const uint FIRST_VERTEX_CONVENTION_EXT = 0x8E4D;
        public const uint LAST_VERTEX_CONVENTION_EXT = 0x8E4E;
        public const uint PROVOKING_VERTEX_EXT = 0x8E4F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //365
        //EXT_texture_snorm
        public const uint ALPHA_SNORM = 0x9010;
        public const uint LUMINANCE_SNORM = 0x9011;
        public const uint LUMINANCE_ALPHA_SNORM = 0x9012;
        public const uint INTENSITY_SNORM = 0x9013;
        public const uint ALPHA8_SNORM = 0x9014;
        public const uint LUMINANCE8_SNORM = 0x9015;
        public const uint LUMINANCE8_ALPHA8_SNORM = 0x9016;
        public const uint INTENSITY8_SNORM = 0x9017;
        public const uint ALPHA16_SNORM = 0x9018;
        public const uint LUMINANCE16_SNORM = 0x9019;
        public const uint LUMINANCE16_ALPHA16_SNORM = 0x901A;
        public const uint INTENSITY16_SNORM = 0x901B;
        //    use VERSION_3_1			    RED_SNORM
        //    use VERSION_3_1			    RG_SNORM
        //    use VERSION_3_1			    RGB_SNORM
        //    use VERSION_3_1			    RGBA_SNORM
        //    use VERSION_3_1			    R8_SNORM
        //    use VERSION_3_1			    RG8_SNORM
        //    use VERSION_3_1			    RGB8_SNORM
        //    use VERSION_3_1			    RGBA8_SNORM
        //    use VERSION_3_1			    R16_SNORM
        //    use VERSION_3_1			    RG16_SNORM
        //    use VERSION_3_1			    RGB16_SNORM
        //    use VERSION_3_1			    RGBA16_SNORM
        //    use VERSION_3_1			    SIGNED_NORMALIZED

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //366
        //AMD_draw_buffers_blend

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //367
        //APPLE_texture_range
        public const uint TEXTURE_RANGE_LENGTH_APPLE = 0x85B7;
        public const uint TEXTURE_RANGE_POINTER_APPLE = 0x85B8;
        public const uint TEXTURE_STORAGE_HINT_APPLE = 0x85BC;
        public const uint STORAGE_PRIVATE_APPLE = 0x85BD;
        //    use APPLE_vertex_array_range	    STORAGE_CACHED_APPLE
        //    use APPLE_vertex_array_range	    STORAGE_SHARED_APPLE

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //368
        //APPLE_float_pixels
        public const uint HALF_APPLE = 0x140B;
        public const uint RGBA_FLOAT32_APPLE = 0x8814;
        public const uint RGB_FLOAT32_APPLE = 0x8815;
        public const uint ALPHA_FLOAT32_APPLE = 0x8816;
        public const uint INTENSITY_FLOAT32_APPLE = 0x8817;
        public const uint LUMINANCE_FLOAT32_APPLE = 0x8818;
        public const uint LUMINANCE_ALPHA_FLOAT32_APPLE = 0x8819;
        public const uint RGBA_FLOAT16_APPLE = 0x881A;
        public const uint RGB_FLOAT16_APPLE = 0x881B;
        public const uint ALPHA_FLOAT16_APPLE = 0x881C;
        public const uint INTENSITY_FLOAT16_APPLE = 0x881D;
        public const uint LUMINANCE_FLOAT16_APPLE = 0x881E;
        public const uint LUMINANCE_ALPHA_FLOAT16_APPLE = 0x881F;
        public const uint COLOR_FLOAT_APPLE = 0x8A0F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //369
        //APPLE_vertex_program_evaluators
        public const uint VERTEX_ATTRIB_MAP1_APPLE = 0x8A00;
        public const uint VERTEX_ATTRIB_MAP2_APPLE = 0x8A01;
        public const uint VERTEX_ATTRIB_MAP1_SIZE_APPLE = 0x8A02;
        public const uint VERTEX_ATTRIB_MAP1_COEFF_APPLE = 0x8A03;
        public const uint VERTEX_ATTRIB_MAP1_ORDER_APPLE = 0x8A04;
        public const uint VERTEX_ATTRIB_MAP1_DOMAIN_APPLE = 0x8A05;
        public const uint VERTEX_ATTRIB_MAP2_SIZE_APPLE = 0x8A06;
        public const uint VERTEX_ATTRIB_MAP2_COEFF_APPLE = 0x8A07;
        public const uint VERTEX_ATTRIB_MAP2_ORDER_APPLE = 0x8A08;
        public const uint VERTEX_ATTRIB_MAP2_DOMAIN_APPLE = 0x8A09;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //370
        //APPLE_aux_depth_stencil
        public const uint AUX_DEPTH_STENCIL_APPLE = 0x8A14;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //371
        //APPLE_object_purgeable
        public const uint BUFFER_OBJECT_APPLE = 0x85B3;
        public const uint RELEASED_APPLE = 0x8A19;
        public const uint VOLATILE_APPLE = 0x8A1A;
        public const uint RETAINED_APPLE = 0x8A1B;
        public const uint UNDEFINED_APPLE = 0x8A1C;
        public const uint PURGEABLE_APPLE = 0x8A1D;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //372
        //APPLE_row_bytes
        public const uint PACK_ROW_BYTES_APPLE = 0x8A15;
        public const uint UNPACK_ROW_BYTES_APPLE = 0x8A16;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //373
        //APPLE_rgb_422
        public const uint RGB_422_APPLE = 0x8A1F;
        //    use APPLE_ycbcr_422		    UNSIGNED_SHORT_8_8_APPLE
        //    use APPLE_ycbcr_422		    UNSIGNED_SHORT_8_8_REV_APPLE

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //374

        //NV_video_capture
        public const uint VIDEO_BUFFER_NV = 0x9020;
        public const uint VIDEO_BUFFER_BINDING_NV = 0x9021;
        public const uint FIELD_UPPER_NV = 0x9022;
        public const uint FIELD_LOWER_NV = 0x9023;
        public const uint NUM_VIDEO_CAPTURE_STREAMS_NV = 0x9024;
        public const uint NEXT_VIDEO_CAPTURE_BUFFER_STATUS_NV = 0x9025;
        public const uint VIDEO_CAPTURE_TO_422_SUPPORTED_NV = 0x9026;
        public const uint LAST_VIDEO_CAPTURE_STATUS_NV = 0x9027;
        public const uint VIDEO_BUFFER_PITCH_NV = 0x9028;
        public const uint VIDEO_COLOR_CONVERSION_MATRIX_NV = 0x9029;
        public const uint VIDEO_COLOR_CONVERSION_MAX_NV = 0x902A;
        public const uint VIDEO_COLOR_CONVERSION_MIN_NV = 0x902B;
        public const uint VIDEO_COLOR_CONVERSION_OFFSET_NV = 0x902C;
        public const uint VIDEO_BUFFER_INTERNAL_FORMAT_NV = 0x902D;
        public const uint PARTIAL_SUCCESS_NV = 0x902E;
        public const uint SUCCESS_NV = 0x902F;
        public const uint FAILURE_NV = 0x9030;
        public const uint YCBYCR8_422_NV = 0x9031;
        public const uint YCBAYCR8A_4224_NV = 0x9032;
        public const uint Z6Y10Z6CB10Z6Y10Z6CR10_422_NV = 0x9033;
        public const uint Z6Y10Z6CB10Z6A10Z6Y10Z6CR10Z6A10_4224_NV = 0x9034;
        public const uint Z4Y12Z4CB12Z4Y12Z4CR12_422_NV = 0x9035;
        public const uint Z4Y12Z4CB12Z4A12Z4Y12Z4CR12Z4A12_4224_NV = 0x9036;
        public const uint Z4Y12Z4CB12Z4CR12_444_NV = 0x9037;
        public const uint VIDEO_CAPTURE_FRAME_WIDTH_NV = 0x9038;
        public const uint VIDEO_CAPTURE_FRAME_HEIGHT_NV = 0x9039;
        public const uint VIDEO_CAPTURE_FIELD_UPPER_HEIGHT_NV = 0x903A;
        public const uint VIDEO_CAPTURE_FIELD_LOWER_HEIGHT_NV = 0x903B;
        public const uint VIDEO_CAPTURE_SURFACE_ORIGIN_NV = 0x903C;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //375 - GLX_EXT_swap_control

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //376 - also GLX_NV_copy_image, WGL_NV_copy_image
        //NV_copy_image

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //377
        //EXT_separate_shader_objects
        public const uint ACTIVE_PROGRAM_EXT = 0x8B8D;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //378
        //NV_parameter_buffer_object2

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //379
        //NV_shader_buffer_load
        public const uint BUFFER_GPU_ADDRESS_NV = 0x8F1D;
        public const uint GPU_ADDRESS_NV = 0x8F34;
        public const uint MAX_SHADER_BUFFER_ADDRESS_NV = 0x8F35;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //380
        //NV_vertex_buffer_unified_memory
        public const uint VERTEX_ATTRIB_ARRAY_UNIFIED_NV = 0x8F1E;
        public const uint ELEMENT_ARRAY_UNIFIED_NV = 0x8F1F;
        public const uint VERTEX_ATTRIB_ARRAY_ADDRESS_NV = 0x8F20;
        public const uint VERTEX_ARRAY_ADDRESS_NV = 0x8F21;
        public const uint NORMAL_ARRAY_ADDRESS_NV = 0x8F22;
        public const uint COLOR_ARRAY_ADDRESS_NV = 0x8F23;
        public const uint INDEX_ARRAY_ADDRESS_NV = 0x8F24;
        public const uint TEXTURE_COORD_ARRAY_ADDRESS_NV = 0x8F25;
        public const uint EDGE_FLAG_ARRAY_ADDRESS_NV = 0x8F26;
        public const uint SECONDARY_COLOR_ARRAY_ADDRESS_NV = 0x8F27;
        public const uint FOG_COORD_ARRAY_ADDRESS_NV = 0x8F28;
        public const uint ELEMENT_ARRAY_ADDRESS_NV = 0x8F29;
        public const uint VERTEX_ATTRIB_ARRAY_LENGTH_NV = 0x8F2A;
        public const uint VERTEX_ARRAY_LENGTH_NV = 0x8F2B;
        public const uint NORMAL_ARRAY_LENGTH_NV = 0x8F2C;
        public const uint COLOR_ARRAY_LENGTH_NV = 0x8F2D;
        public const uint INDEX_ARRAY_LENGTH_NV = 0x8F2E;
        public const uint TEXTURE_COORD_ARRAY_LENGTH_NV = 0x8F2F;
        public const uint EDGE_FLAG_ARRAY_LENGTH_NV = 0x8F30;
        public const uint SECONDARY_COLOR_ARRAY_LENGTH_NV = 0x8F31;
        public const uint FOG_COORD_ARRAY_LENGTH_NV = 0x8F32;
        public const uint ELEMENT_ARRAY_LENGTH_NV = 0x8F33;
        public const uint DRAW_INDIRECT_UNIFIED_NV = 0x8F40;    // Requires ARB_draw_indirect
        public const uint DRAW_INDIRECT_ADDRESS_NV = 0x8F41;    // Requires ARB_draw_indirect
        public const uint DRAW_INDIRECT_LENGTH_NV = 0x8F42;    // Requires ARB_draw_indirect

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //381
        //NV_texture_barrier

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //382
        //AMD_shader_stencil_export

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //383
        //AMD_seamless_cubemap_per_texture
        //    use ARB_seamless_cube_map	    TEXTURE_CUBE_MAP_SEAMLESS

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //384 - GLX_INTEL_swap_event

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //385
        //AMD_conservative_depth

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //386
        //EXT_shader_image_load_store
        public const uint MAX_IMAGE_UNITS_EXT = 0x8F38;
        public const uint MAX_COMBINED_IMAGE_UNITS_AND_FRAGMENT_OUTPUTS_EXT = 0x8F39;
        public const uint IMAGE_BINDING_NAME_EXT = 0x8F3A;
        public const uint IMAGE_BINDING_LEVEL_EXT = 0x8F3B;
        public const uint IMAGE_BINDING_LAYERED_EXT = 0x8F3C;
        public const uint IMAGE_BINDING_LAYER_EXT = 0x8F3D;
        public const uint IMAGE_BINDING_ACCESS_EXT = 0x8F3E;
        public const uint IMAGE_1D_EXT = 0x904C;
        public const uint IMAGE_2D_EXT = 0x904D;
        public const uint IMAGE_3D_EXT = 0x904E;
        public const uint IMAGE_2D_RECT_EXT = 0x904F;
        public const uint IMAGE_CUBE_EXT = 0x9050;
        public const uint IMAGE_BUFFER_EXT = 0x9051;
        public const uint IMAGE_1D_ARRAY_EXT = 0x9052;
        public const uint IMAGE_2D_ARRAY_EXT = 0x9053;
        public const uint IMAGE_CUBE_MAP_ARRAY_EXT = 0x9054;
        public const uint IMAGE_2D_MULTISAMPLE_EXT = 0x9055;
        public const uint IMAGE_2D_MULTISAMPLE_ARRAY_EXT = 0x9056;
        public const uint INT_IMAGE_1D_EXT = 0x9057;
        public const uint INT_IMAGE_2D_EXT = 0x9058;
        public const uint INT_IMAGE_3D_EXT = 0x9059;
        public const uint INT_IMAGE_2D_RECT_EXT = 0x905A;
        public const uint INT_IMAGE_CUBE_EXT = 0x905B;
        public const uint INT_IMAGE_BUFFER_EXT = 0x905C;
        public const uint INT_IMAGE_1D_ARRAY_EXT = 0x905D;
        public const uint INT_IMAGE_2D_ARRAY_EXT = 0x905E;
        public const uint INT_IMAGE_CUBE_MAP_ARRAY_EXT = 0x905F;
        public const uint INT_IMAGE_2D_MULTISAMPLE_EXT = 0x9060;
        public const uint INT_IMAGE_2D_MULTISAMPLE_ARRAY_EXT = 0x9061;
        public const uint UNSIGNED_INT_IMAGE_1D_EXT = 0x9062;
        public const uint UNSIGNED_INT_IMAGE_2D_EXT = 0x9063;
        public const uint UNSIGNED_INT_IMAGE_3D_EXT = 0x9064;
        public const uint UNSIGNED_INT_IMAGE_2D_RECT_EXT = 0x9065;
        public const uint UNSIGNED_INT_IMAGE_CUBE_EXT = 0x9066;
        public const uint UNSIGNED_INT_IMAGE_BUFFER_EXT = 0x9067;
        public const uint UNSIGNED_INT_IMAGE_1D_ARRAY_EXT = 0x9068;
        public const uint UNSIGNED_INT_IMAGE_2D_ARRAY_EXT = 0x9069;
        public const uint UNSIGNED_INT_IMAGE_CUBE_MAP_ARRAY_EXT = 0x906A;
        public const uint UNSIGNED_INT_IMAGE_2D_MULTISAMPLE_EXT = 0x906B;
        public const uint UNSIGNED_INT_IMAGE_2D_MULTISAMPLE_ARRAY_EXT = 0x906C;
        public const uint MAX_IMAGE_SAMPLES_EXT = 0x906D;
        public const uint IMAGE_BINDING_FORMAT_EXT = 0x906E;
        //// ??? Not clear where to put new types of mask bits yet
        public const uint VERTEX_ATTRIB_ARRAY_BARRIER_BIT_EXT = 0x00000001;
        public const uint ELEMENT_ARRAY_BARRIER_BIT_EXT = 0x00000002;
        public const uint UNIFORM_BARRIER_BIT_EXT = 0x00000004;
        public const uint TEXTURE_FETCH_BARRIER_BIT_EXT = 0x00000008;
        public const uint SHADER_IMAGE_ACCESS_BARRIER_BIT_EXT = 0x00000020;
        public const uint COMMAND_BARRIER_BIT_EXT = 0x00000040;
        public const uint PIXEL_BUFFER_BARRIER_BIT_EXT = 0x00000080;
        public const uint TEXTURE_UPDATE_BARRIER_BIT_EXT = 0x00000100;
        public const uint BUFFER_UPDATE_BARRIER_BIT_EXT = 0x00000200;
        public const uint FRAMEBUFFER_BARRIER_BIT_EXT = 0x00000400;
        public const uint TRANSFORM_FEEDBACK_BARRIER_BIT_EXT = 0x00000800;
        public const uint ATOMIC_COUNTER_BARRIER_BIT_EXT = 0x00001000;
        public const uint ALL_BARRIER_BITS_EXT = 0xFFFFFFFF;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //387
        //EXT_vertex_attrib_64bit
        //    use VERSION_1_1			    DOUBLE
        public const uint DOUBLE_VEC2_EXT = 0x8FFC;
        public const uint DOUBLE_VEC3_EXT = 0x8FFD;
        public const uint DOUBLE_VEC4_EXT = 0x8FFE;
        public const uint DOUBLE_MAT2_EXT = 0x8F46;
        public const uint DOUBLE_MAT3_EXT = 0x8F47;
        public const uint DOUBLE_MAT4_EXT = 0x8F48;
        public const uint DOUBLE_MAT2x3_EXT = 0x8F49;
        public const uint DOUBLE_MAT2x4_EXT = 0x8F4A;
        public const uint DOUBLE_MAT3x2_EXT = 0x8F4B;
        public const uint DOUBLE_MAT3x4_EXT = 0x8F4C;
        public const uint DOUBLE_MAT4x2_EXT = 0x8F4D;
        public const uint DOUBLE_MAT4x3_EXT = 0x8F4E;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //388
        //NV_gpu_program5
        public const uint MAX_GEOMETRY_PROGRAM_INVOCATIONS_NV = 0x8E5A;
        public const uint MIN_FRAGMENT_INTERPOLATION_OFFSET_NV = 0x8E5B;
        public const uint MAX_FRAGMENT_INTERPOLATION_OFFSET_NV = 0x8E5C;
        public const uint FRAGMENT_PROGRAM_INTERPOLATION_OFFSET_BITS_NV = 0x8E5D;
        public const uint MIN_PROGRAM_TEXTURE_GATHER_OFFSET_NV = 0x8E5E;
        public const uint MAX_PROGRAM_TEXTURE_GATHER_OFFSET_NV = 0x8E5F;
        public const uint MAX_PROGRAM_SUBROUTINE_PARAMETERS_NV = 0x8F44;    // Requires ARB_shader_subroutine
        public const uint MAX_PROGRAM_SUBROUTINE_NUM_NV = 0x8F45;    // Requires ARB_shader_subroutine

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //389
        //NV_gpu_shader5
        public const uint INT64_NV = 0x140E;
        public const uint UNSIGNED_INT64_NV = 0x140F;
        public const uint INT8_NV = 0x8FE0;
        public const uint INT8_VEC2_NV = 0x8FE1;
        public const uint INT8_VEC3_NV = 0x8FE2;
        public const uint INT8_VEC4_NV = 0x8FE3;
        public const uint INT16_NV = 0x8FE4;
        public const uint INT16_VEC2_NV = 0x8FE5;
        public const uint INT16_VEC3_NV = 0x8FE6;
        public const uint INT16_VEC4_NV = 0x8FE7;
        public const uint INT64_VEC2_NV = 0x8FE9;
        public const uint INT64_VEC3_NV = 0x8FEA;
        public const uint INT64_VEC4_NV = 0x8FEB;
        public const uint UNSIGNED_INT8_NV = 0x8FEC;
        public const uint UNSIGNED_INT8_VEC2_NV = 0x8FED;
        public const uint UNSIGNED_INT8_VEC3_NV = 0x8FEE;
        public const uint UNSIGNED_INT8_VEC4_NV = 0x8FEF;
        public const uint UNSIGNED_INT16_NV = 0x8FF0;
        public const uint UNSIGNED_INT16_VEC2_NV = 0x8FF1;
        public const uint UNSIGNED_INT16_VEC3_NV = 0x8FF2;
        public const uint UNSIGNED_INT16_VEC4_NV = 0x8FF3;
        public const uint UNSIGNED_INT64_VEC2_NV = 0x8FF5;
        public const uint UNSIGNED_INT64_VEC3_NV = 0x8FF6;
        public const uint UNSIGNED_INT64_VEC4_NV = 0x8FF7;
        public const uint FLOAT16_NV = 0x8FF8;
        public const uint FLOAT16_VEC2_NV = 0x8FF9;
        public const uint FLOAT16_VEC3_NV = 0x8FFA;
        public const uint FLOAT16_VEC4_NV = 0x8FFB;
        //    use ARB_tessellation_shader	    PATCHES

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //390
        //NV_shader_buffer_store
        public const uint SHADER_GLOBAL_ACCESS_BARRIER_BIT_NV = 0x00000010;
        //    use VERSION_1_5			    READ_WRITE
        //    use VERSION_1_5			    WRITE_ONLY

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //391
        //NV_tessellation_program5
        public const uint MAX_PROGRAM_PATCH_ATTRIBS_NV = 0x86D8;
        public const uint TESS_CONTROL_PROGRAM_NV = 0x891E;
        public const uint TESS_EVALUATION_PROGRAM_NV = 0x891F;
        public const uint TESS_CONTROL_PROGRAM_PARAMETER_BUFFER_NV = 0x8C74;
        public const uint TESS_EVALUATION_PROGRAM_PARAMETER_BUFFER_NV = 0x8C75;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //392
        //NV_vertex_attrib_integer_64bit
        //    use NV_gpu_shader5		    INT64_NV
        //    use NV_gpu_shader5		    UNSIGNED_INT64_NV

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //393
        //// Revision 4 removed COVERAGE_SAMPLES_NV, which was an alias for
        //// SAMPLES_ARB, due to a collision with the GL_NV_coverage_sample
        //// OpenGL ES extension.
        //NV_multisample_coverage
        public const uint COLOR_SAMPLES_NV = 0x8E20;
        //    use ARB_multisample		    SAMPLES_ARB

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //394
        //AMD_name_gen_delete
        public const uint DATA_BUFFER_AMD = 0x9151;
        public const uint PERFORMANCE_MONITOR_AMD = 0x9152;
        public const uint QUERY_OBJECT_AMD = 0x9153;
        public const uint VERTEX_ARRAY_OBJECT_AMD = 0x9154;
        public const uint SAMPLER_OBJECT_AMD = 0x9155;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //395
        //AMD_debug_output
        public const uint MAX_DEBUG_MESSAGE_LENGTH_AMD = 0x9143;
        public const uint MAX_DEBUG_LOGGED_MESSAGES_AMD = 0x9144;
        public const uint DEBUG_LOGGED_MESSAGES_AMD = 0x9145;
        public const uint DEBUG_SEVERITY_HIGH_AMD = 0x9146;
        public const uint DEBUG_SEVERITY_MEDIUM_AMD = 0x9147;
        public const uint DEBUG_SEVERITY_LOW_AMD = 0x9148;
        public const uint DEBUG_CATEGORY_API_ERROR_AMD = 0x9149;
        public const uint DEBUG_CATEGORY_WINDOW_SYSTEM_AMD = 0x914A;
        public const uint DEBUG_CATEGORY_DEPRECATION_AMD = 0x914B;
        public const uint DEBUG_CATEGORY_UNDEFINED_BEHAVIOR_AMD = 0x914C;
        public const uint DEBUG_CATEGORY_PERFORMANCE_AMD = 0x914D;
        public const uint DEBUG_CATEGORY_SHADER_COMPILER_AMD = 0x914E;
        public const uint DEBUG_CATEGORY_APPLICATION_AMD = 0x914F;
        public const uint DEBUG_CATEGORY_OTHER_AMD = 0x9150;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //396
        //NV_vdpau_interop
        public const uint SURFACE_STATE_NV = 0x86EB;
        public const uint SURFACE_REGISTERED_NV = 0x86FD;
        public const uint SURFACE_MAPPED_NV = 0x8700;
        public const uint WRITE_DISCARD_NV = 0x88BE;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //397
        //AMD_transform_feedback3_lines_triangles

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //398 - GLX_AMD_gpu_association
        //// Extension //399 - GLX_EXT_create_context_es2_profile
        //// Extension //400 - WGL_EXT_create_context_es2_profile

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //401
        //AMD_depth_clamp_separate
        public const uint DEPTH_CLAMP_NEAR_AMD = 0x901E;
        public const uint DEPTH_CLAMP_FAR_AMD = 0x901F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //402
        //EXT_texture_sRGB_decode
        public const uint TEXTURE_SRGB_DECODE_EXT = 0x8A48;
        public const uint DECODE_EXT = 0x8A49;
        public const uint SKIP_DECODE_EXT = 0x8A4A;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //403
        //NV_texture_multisample
        public const uint TEXTURE_COVERAGE_SAMPLES_NV = 0x9045;
        public const uint TEXTURE_COLOR_SAMPLES_NV = 0x9046;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //404
        //AMD_blend_minmax_factor
        public const uint FACTOR_MIN_AMD = 0x901C;
        public const uint FACTOR_MAX_AMD = 0x901D;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //405
        //AMD_sample_positions
        public const uint SUBSAMPLE_DISTANCE_AMD = 0x883F;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //406
        //EXT_x11_sync_object
        public const uint SYNC_X11_FENCE_EXT = 0x90E1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //407 - WGL_NV_DX_interop

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //408
        //AMD_multi_draw_indirect

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //409
        //EXT_framebuffer_multisample_blit_scaled
        public const uint SCALED_RESOLVE_FASTEST_EXT = 0x90BA;
        public const uint SCALED_RESOLVE_NICEST_EXT = 0x90BB;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //410
        //// '////' tokens below were removed in later versions of the extension
        //NV_path_rendering
        public const uint PATH_FORMAT_SVG_NV = 0x9070;
        public const uint PATH_FORMAT_PS_NV = 0x9071;
        public const uint STANDARD_FONT_NAME_NV = 0x9072;
        public const uint SYSTEM_FONT_NAME_NV = 0x9073;
        public const uint FILE_NAME_NV = 0x9074;
        public const uint PATH_STROKE_WIDTH_NV = 0x9075;
        public const uint PATH_END_CAPS_NV = 0x9076;
        public const uint PATH_INITIAL_END_CAP_NV = 0x9077;
        public const uint PATH_TERMINAL_END_CAP_NV = 0x9078;
        public const uint PATH_JOIN_STYLE_NV = 0x9079;
        public const uint PATH_MITER_LIMIT_NV = 0x907A;
        public const uint PATH_DASH_CAPS_NV = 0x907B;
        public const uint PATH_INITIAL_DASH_CAP_NV = 0x907C;
        public const uint PATH_TERMINAL_DASH_CAP_NV = 0x907D;
        public const uint PATH_DASH_OFFSET_NV = 0x907E;
        public const uint PATH_CLIENT_LENGTH_NV = 0x907F;
        public const uint PATH_FILL_MODE_NV = 0x9080;
        public const uint PATH_FILL_MASK_NV = 0x9081;
        public const uint PATH_FILL_COVER_MODE_NV = 0x9082;
        public const uint PATH_STROKE_COVER_MODE_NV = 0x9083;
        public const uint PATH_STROKE_MASK_NV = 0x9084;
        ////public const uint PATH_SAMPLE_QUALITY_NV = 0x9085;
        ////public const uint PATH_STROKE_BOUND_NV = 0x9086;
        ////public const uint PATH_STROKE_OVERSAMPLE_COUNT_NV = 0x9087;
        public const uint COUNT_UP_NV = 0x9088;
        public const uint COUNT_DOWN_NV = 0x9089;
        public const uint PATH_OBJECT_BOUNDING_BOX_NV = 0x908A;
        public const uint CONVEX_HULL_NV = 0x908B;
        ////public const uint MULTI_HULLS_NV = 0x908C;
        public const uint BOUNDING_BOX_NV = 0x908D;
        public const uint TRANSLATE_X_NV = 0x908E;
        public const uint TRANSLATE_Y_NV = 0x908F;
        public const uint TRANSLATE_2D_NV = 0x9090;
        public const uint TRANSLATE_3D_NV = 0x9091;
        public const uint AFFINE_2D_NV = 0x9092;
        ////public const uint PROJECTIVE_2D_NV = 0x9093;
        public const uint AFFINE_3D_NV = 0x9094;
        ////public const uint PROJECTIVE_3D_NV = 0x9095;
        public const uint TRANSPOSE_AFFINE_2D_NV = 0x9096;
        ////public const uint TRANSPOSE_PROJECTIVE_2D_NV = 0x9097;
        public const uint TRANSPOSE_AFFINE_3D_NV = 0x9098;
        ////public const uint TRANSPOSE_PROJECTIVE_3D_NV = 0x9099;
        public const uint UTF8_NV = 0x909A;
        public const uint UTF16_NV = 0x909B;
        public const uint BOUNDING_BOX_OF_BOUNDING_BOXES_NV = 0x909C;
        public const uint PATH_COMMAND_COUNT_NV = 0x909D;
        public const uint PATH_COORD_COUNT_NV = 0x909E;
        public const uint PATH_DASH_ARRAY_COUNT_NV = 0x909F;
        public const uint PATH_COMPUTED_LENGTH_NV = 0x90A0;
        public const uint PATH_FILL_BOUNDING_BOX_NV = 0x90A1;
        public const uint PATH_STROKE_BOUNDING_BOX_NV = 0x90A2;
        public const uint SQUARE_NV = 0x90A3;
        public const uint ROUND_NV = 0x90A4;
        public const uint TRIANGULAR_NV = 0x90A5;
        public const uint BEVEL_NV = 0x90A6;
        public const uint MITER_REVERT_NV = 0x90A7;
        public const uint MITER_TRUNCATE_NV = 0x90A8;
        public const uint SKIP_MISSING_GLYPH_NV = 0x90A9;
        public const uint USE_MISSING_GLYPH_NV = 0x90AA;
        public const uint PATH_ERROR_POSITION_NV = 0x90AB;
        public const uint PATH_FOG_GEN_MODE_NV = 0x90AC;
        public const uint ACCUM_ADJACENT_PAIRS_NV = 0x90AD;
        public const uint ADJACENT_PAIRS_NV = 0x90AE;
        public const uint FIRST_TO_REST_NV = 0x90AF;
        public const uint PATH_GEN_MODE_NV = 0x90B0;
        public const uint PATH_GEN_COEFF_NV = 0x90B1;
        public const uint PATH_GEN_COLOR_FORMAT_NV = 0x90B2;
        public const uint PATH_GEN_COMPONENTS_NV = 0x90B3;
        public const uint PATH_STENCIL_FUNC_NV = 0x90B7;
        public const uint PATH_STENCIL_REF_NV = 0x90B8;
        public const uint PATH_STENCIL_VALUE_MASK_NV = 0x90B9;
        public const uint PATH_STENCIL_DEPTH_OFFSET_FACTOR_NV = 0x90BD;
        public const uint PATH_STENCIL_DEPTH_OFFSET_UNITS_NV = 0x90BE;
        public const uint PATH_COVER_DEPTH_FUNC_NV = 0x90BF;
        public const uint PATH_DASH_OFFSET_RESET_NV = 0x90B4;
        public const uint MOVE_TO_RESETS_NV = 0x90B5;
        public const uint MOVE_TO_CONTINUES_NV = 0x90B6;
        public const uint CLOSE_PATH_NV = 0x00;
        public const uint MOVE_TO_NV = 0x02;
        public const uint RELATIVE_MOVE_TO_NV = 0x03;
        public const uint LINE_TO_NV = 0x04;
        public const uint RELATIVE_LINE_TO_NV = 0x05;
        public const uint HORIZONTAL_LINE_TO_NV = 0x06;
        public const uint RELATIVE_HORIZONTAL_LINE_TO_NV = 0x07;
        public const uint VERTICAL_LINE_TO_NV = 0x08;
        public const uint RELATIVE_VERTICAL_LINE_TO_NV = 0x09;
        public const uint QUADRATIC_CURVE_TO_NV = 0x0A;
        public const uint RELATIVE_QUADRATIC_CURVE_TO_NV = 0x0B;
        public const uint CUBIC_CURVE_TO_NV = 0x0C;
        public const uint RELATIVE_CUBIC_CURVE_TO_NV = 0x0D;
        public const uint SMOOTH_QUADRATIC_CURVE_TO_NV = 0x0E;
        public const uint RELATIVE_SMOOTH_QUADRATIC_CURVE_TO_NV = 0x0F;
        public const uint SMOOTH_CUBIC_CURVE_TO_NV = 0x10;
        public const uint RELATIVE_SMOOTH_CUBIC_CURVE_TO_NV = 0x11;
        public const uint SMALL_CCW_ARC_TO_NV = 0x12;
        public const uint RELATIVE_SMALL_CCW_ARC_TO_NV = 0x13;
        public const uint SMALL_CW_ARC_TO_NV = 0x14;
        public const uint RELATIVE_SMALL_CW_ARC_TO_NV = 0x15;
        public const uint LARGE_CCW_ARC_TO_NV = 0x16;
        public const uint RELATIVE_LARGE_CCW_ARC_TO_NV = 0x17;
        public const uint LARGE_CW_ARC_TO_NV = 0x18;
        public const uint RELATIVE_LARGE_CW_ARC_TO_NV = 0x19;
        public const uint RESTART_PATH_NV = 0xF0;
        public const uint DUP_FIRST_CUBIC_CURVE_TO_NV = 0xF2;
        public const uint DUP_LAST_CUBIC_CURVE_TO_NV = 0xF4;
        public const uint RECT_NV = 0xF6;
        public const uint CIRCULAR_CCW_ARC_TO_NV = 0xF8;
        public const uint CIRCULAR_CW_ARC_TO_NV = 0xFA;
        public const uint CIRCULAR_TANGENT_ARC_TO_NV = 0xFC;
        public const uint ARC_TO_NV = 0xFE;
        public const uint RELATIVE_ARC_TO_NV = 0xFF;
        public const uint BOLD_BIT_NV = 0x01;
        public const uint ITALIC_BIT_NV = 0x02;
        public const uint GLYPH_WIDTH_BIT_NV = 0x01;
        public const uint GLYPH_HEIGHT_BIT_NV = 0x02;
        public const uint GLYPH_HORIZONTAL_BEARING_X_BIT_NV = 0x04;
        public const uint GLYPH_HORIZONTAL_BEARING_Y_BIT_NV = 0x08;
        public const uint GLYPH_HORIZONTAL_BEARING_ADVANCE_BIT_NV = 0x10;
        public const uint GLYPH_VERTICAL_BEARING_X_BIT_NV = 0x20;
        public const uint GLYPH_VERTICAL_BEARING_Y_BIT_NV = 0x40;
        public const uint GLYPH_VERTICAL_BEARING_ADVANCE_BIT_NV = 0x80;
        public const uint GLYPH_HAS_KERNING_BIT_NV = 0x100;
        public const uint FONT_X_MIN_BOUNDS_BIT_NV = 0x00010000;
        public const uint FONT_Y_MIN_BOUNDS_BIT_NV = 0x00020000;
        public const uint FONT_X_MAX_BOUNDS_BIT_NV = 0x00040000;
        public const uint FONT_Y_MAX_BOUNDS_BIT_NV = 0x00080000;
        public const uint FONT_UNITS_PER_EM_BIT_NV = 0x00100000;
        public const uint FONT_ASCENDER_BIT_NV = 0x00200000;
        public const uint FONT_DESCENDER_BIT_NV = 0x00400000;
        public const uint FONT_HEIGHT_BIT_NV = 0x00800000;
        public const uint FONT_MAX_ADVANCE_WIDTH_BIT_NV = 0x01000000;
        public const uint FONT_MAX_ADVANCE_HEIGHT_BIT_NV = 0x02000000;
        public const uint FONT_UNDERLINE_POSITION_BIT_NV = 0x04000000;
        public const uint FONT_UNDERLINE_THICKNESS_BIT_NV = 0x08000000;
        public const uint FONT_HAS_KERNING_BIT_NV = 0x10000000;
        //    use VERSION_1_3			    PRIMARY_COLOR
        //    use NV_register_combiners	    PRIMARY_COLOR_NV
        //    use NV_register_combiners	    SECONDARY_COLOR_NV

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //411
        //AMD_pinned_memory
        public const uint EXTERNAL_VIRTUAL_MEMORY_BUFFER_AMD = 0x9160;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //412 - WGL_NV_DX_interop2

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //413
        //AMD_stencil_operation_extended
        public const uint SET_AMD = 0x874A;
        public const uint REPLACE_VALUE_AMD = 0x874B;
        public const uint STENCIL_OP_VALUE_AMD = 0x874C;
        public const uint STENCIL_BACK_OP_VALUE_AMD = 0x874D;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //414 - GLX_EXT_swap_control_tear
        //// Extension //415 - WGL_EXT_swap_control_tear

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //416
        //AMD_vertex_shader_viewport_index

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //417
        //AMD_vertex_shader_layer

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //418
        //NV_bindless_texture

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //419
        //NV_shader_atomic_float

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //420
        //AMD_query_buffer_object
        public const uint QUERY_BUFFER_AMD = 0x9192;
        public const uint QUERY_BUFFER_BINDING_AMD = 0x9193;
        public const uint QUERY_RESULT_NO_WAIT_AMD = 0x9194;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //421
        //NV_compute_program5
        public const uint COMPUTE_PROGRAM_NV = 0x90FB;
        public const uint COMPUTE_PROGRAM_PARAMETER_BUFFER_NV = 0x90FC;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //422
        //NV_shader_storage_buffer_object

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //423
        //NV_shader_atomic_counters

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //424
        //NV_deep_texture3D
        public const uint MAX_DEEP_3D_TEXTURE_WIDTH_HEIGHT_NV = 0x90D0;
        public const uint MAX_DEEP_3D_TEXTURE_DEPTH_NV = 0x90D1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //425
        //NVX_conditional_render

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //426
        //AMD_sparse_texture
        public const uint VIRTUAL_PAGE_SIZE_X_AMD = 0x9195;
        public const uint VIRTUAL_PAGE_SIZE_Y_AMD = 0x9196;
        public const uint VIRTUAL_PAGE_SIZE_Z_AMD = 0x9197;
        public const uint MAX_SPARSE_TEXTURE_SIZE_AMD = 0x9198;
        public const uint MAX_SPARSE_3D_TEXTURE_SIZE_AMD = 0x9199;
        public const uint MAX_SPARSE_ARRAY_TEXTURE_LAYERS = 0x919A;
        public const uint MIN_SPARSE_LEVEL_AMD = 0x919B;
        public const uint MIN_LOD_WARNING_AMD = 0x919C;
        public const uint TEXTURE_STORAGE_SPARSE_BIT_AMD = 0x00000001;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //427 - GLX_EXT_buffer_age

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //428
        //AMD_shader_trinary_minmax

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Extension //429

        //INTEL_map_texture
        public const uint TEXTURE_MEMORY_LAYOUT_INTEL = 0x83FF;
        public const uint LAYOUT_DEFAULT_INTEL = 0;
        public const uint LAYOUT_LINEAR_INTEL = 1;
        public const uint LAYOUT_LINEAR_CPU_CACHED_INTEL = 2;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// No new tokens
        //// Extension //430
        //NV_draw_texture


        ///*************************************************************/
        

        #region VERSION_2_0

        ///* GL type for program/shader text */
        //typedef char GLchar;
        #endregion

        #region VERSION_1_5

        ///* GL types for handling large vertex buffer objects */
        //typedef ptrdiff_t GLintptr;
        //typedef ptrdiff_t GLsizeiptr;
        #endregion

        #region ARB_vertex_buffer_object

        ///* GL types for handling large vertex buffer objects */
        //typedef ptrdiff_t GLintptrARB;
        //typedef ptrdiff_t GLsizeiptrARB;
        #endregion

        #region ARB_shader_objects

        ///* GL types for program/shader text and shader object handles */
        //typedef char GLcharARB;
        //typedef unsigned int GLhandleARB;
        #endregion

        ///* GL type for "half" precision (s10e5) float data in host memory */
        #region ARB_half_float_pixel

        //typedef unsigned short GLhalfARB;
        #endregion

        #region NV_half_float

        //typedef unsigned short GLhalfNV;
        #endregion

        #region EXT_timer_query

        //typedef int64_t GLint64EXT;
        //typedef uint64_t GLuint64EXT;
        #endregion

        #region ARB_cl_event

        ///* These incomplete types let us declare types compatible with OpenCL's cl_context and cl_event */
        //struct _cl_context;
        //struct _cl_event;
        #endregion

        #region ARB_debug_output

        //typedef void (*GLDEBUGPROCARB)(GLenum source,GLenum type,GLuint id,GLenum severity,GLsizei length,const GLchar *message,GLvoid *userparameter);
        #endregion

        #region AMD_debug_output

        //typedef void (*GLDEBUGPROCAMD)(GLuint id,GLenum category,GLenum severity,GLsizei length,const GLchar *message,GLvoid *userparameter);
        #endregion

        #region KHR_debug

        //typedef void (*GLDEBUGPROC)(GLenum source,GLenum type,GLuint id,GLenum severity,GLsizei length,const GLchar *message,GLvoid *userparameter);
        #endregion

        #region NV_vdpau_interop

        //typedef GLintptr GLvdpauSurfaceNV;
        #endregion

        #region OES_fixed_point

        ///* GLint must be 32 bits, a relatively safe assumption on modern CPUs */
        //typedef GLint GLfixed;
        #endregion

        #region VERSION_1_0
        private IntPtr glCullFace;
        public static void CullFace(uint mode) 
        {
            throw new NotImplementedException();
        }

        private IntPtr glFrontFace;
        public static void FrontFace(uint mode)
        {
            throw new NotImplementedException();
        }

        //public static void glHint (GLenum target, GLenum mode);

        private IntPtr glLineWidth;
        public static void LineWidth(float width)
        {
            throw new NotImplementedException();
        }

        private IntPtr glPointSize;
        public static void PointSize(float size)
        {
            throw new NotImplementedException();
        }

        private IntPtr glPolygonMode;
        public static void PolygonMode(uint face, uint mode)
        {
            throw new NotImplementedException();
        }

        private IntPtr glScissor;
        public static void Scissor(int x, int y, int width, int height)
        {
            throw new NotImplementedException();
        }

        private IntPtr glTexParameterf;
        public static void TexParameterf(uint target, uint pname, float parameter)
        {
            throw new NotImplementedException();
        }

        private IntPtr glTexParameterfv;
        public static void TexParameterfv(uint target, uint pname, float* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glTexParameteri;
        public static void TexParameteri(uint target, uint pname, int parameter)
        {
            throw new NotImplementedException();
        }

        private IntPtr glTexParameteriv;
        public static void TexParameteriv(uint target, uint pname, int* parameters)
        {
            throw new NotImplementedException();
        }

        //public static void glTexImage1D (GLenum target, GLint level, GLint internalformat, GLsizei width, GLint border, GLenum format, GLenum type, const GLvoid *pixels);
        //public static void glTexImage2D (GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, const GLvoid *pixels);
        //public static void glDrawBuffer (GLenum mode);

        private IntPtr glClear;
        public static void Clear(uint mask)
        {
            throw new NotImplementedException();
        }

        private IntPtr glClearColor;
        public static void ClearColor(float red, float green, float blue, float alpha)
        {
            throw new NotImplementedException();
        }

        private IntPtr glClearStencil;
        public static void ClearStencil(int s)
        {
            throw new NotImplementedException();
        }

        private IntPtr glClearDepth;
        public static void ClearDepth(double depth)
        {
            throw new NotImplementedException();
        }
        //public static void glStencilMask (GLuint mask);
        //public static void glColorMask (GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);

        private IntPtr glDepthMask;
        public static void DepthMask(byte flag)
        {
            throw new NotImplementedException();
        }

        private IntPtr glDisable;
        public static void Disable(uint cap)
        {
            throw new NotImplementedException();
        }

        private IntPtr glEnable;
        public static void Enable(uint cap)
        {
            throw new NotImplementedException();
        }

        private IntPtr glFinish;
        public static void Finish()
        {
            throw new NotImplementedException();
        }

        private IntPtr glFlush;
        public static void Flush()
        {
            throw new NotImplementedException();
        }

        //public static void glBlendFunc (GLenum sfactor, GLenum dfactor);
        //public static void glLogicOp (GLenum opcode);
        //public static void glStencilFunc (GLenum func, GLint ref, GLuint mask);
        //public static void glStencilOp (GLenum fail, GLenum zfail, GLenum zpass);

        private IntPtr glDepthFunc;
        public static void DepthFunc(uint func)
        {
            throw new NotImplementedException();
        }

        //public static void glPixelStoref (GLenum pname, GLfloat parameter);
        //public static void glPixelStorei (GLenum pname, GLint parameter);
        //public static void glReadBuffer (GLenum mode);
        //public static void glReadPixels (GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, GLvoid *pixels);

        private IntPtr glGetBooleanv;
        public static void GetBooleanv(uint pname, byte* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetDoublev;
        public static void GetDoublev(uint pname, double* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetError;
        public static uint GetError()
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetFloatv;
        public static void GetFloatv(uint pname, float* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetIntegerv;
        public static void GetIntegerv(uint pname, int* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetString;
        public static byte* GetString(uint name)
        {
            throw new NotImplementedException();
        }

        //public static void glGetTexImage (GLenum target, GLint level, GLenum format, GLenum type, GLvoid *pixels);

        private IntPtr glGetTexParameterfv;
        public static void GetTexParameterfv(uint target, uint pname, float* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetTexParameteriv;
        public static void GetTexParameteriv(uint target, uint pname, int* parameters)
        {
            throw new NotImplementedException();
        }

        //public static void glGetTexLevelParameterfv (GLenum target, GLint level, GLenum pname, GLfloat *parameters);
        //public static void glGetTexLevelParameteriv (GLenum target, GLint level, GLenum pname, GLint *parameters);

        private IntPtr glIsEnabled;
        public static byte IsEnabled(uint cap)
        {
            throw new NotImplementedException();
        }

        private IntPtr glDepthRange;
        public static void DepthRange(double near, double far)
        {
            throw new NotImplementedException();
        }

        private IntPtr glViewport;
        public static void Viewport(int x, int y, int width, int height)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region VERSION_1_1

        private IntPtr glDrawArrays;
        public static void DrawArrays(uint mode, int first, int count)
        {
            throw new NotImplementedException();
        }

        private IntPtr glDrawElements;
        public static void DrawElements(uint mode, int count, uint type, void* indices)
        {
            throw new NotImplementedException();
        }
        //public static void glGetPointerv (GLenum pname, GLvoid* *parameters);
        //public static void glPolygonOffset (GLfloat factor, GLfloat units);
        //public static void glCopyTexImage1D (GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLint border);
        //public static void glCopyTexImage2D (GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);
        //public static void glCopyTexSubImage1D (GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);
        //public static void glCopyTexSubImage2D (GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);
        //public static void glTexSubImage1D (GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, const GLvoid *pixels);
        //public static void glTexSubImage2D (GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, const GLvoid *pixels);

        private IntPtr glBindTexture;
        public static void BindTexture(uint target, uint texture)
        {
            throw new NotImplementedException();
        }

        private IntPtr glDeleteTextures;
        public static void DeleteTextures(int n, uint* textures)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGenTextures;
        public static void GenTextures(int n, uint* textures)
        {
            throw new NotImplementedException();
        }

        private IntPtr glIsTexture;
        public static byte IsTexture(uint texture)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region VERSION_1_2
        //public static void glBlendColor (GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);
        //public static void glBlendEquation (GLenum mode);
        //public static void glDrawRangeElements (GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, const GLvoid *indices);
        //public static void glTexImage3D (GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLenum format, GLenum type, const GLvoid *pixels);
        //public static void glTexSubImage3D (GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, const GLvoid *pixels);
        //public static void glCopyTexSubImage3D (GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);
        #endregion

        #region VERSION_1_3
        //public static void glActiveTexture (GLenum texture);
        //public static void glSampleCoverage (GLfloat value, GLboolean invert);
        //public static void glCompressedTexImage3D (GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLsizei imageSize, const GLvoid *data);
        //public static void glCompressedTexImage2D (GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, GLsizei imageSize, const GLvoid *data);
        //public static void glCompressedTexImage1D (GLenum target, GLint level, GLenum internalformat, GLsizei width, GLint border, GLsizei imageSize, const GLvoid *data);
        //public static void glCompressedTexSubImage3D (GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLsizei imageSize, const GLvoid *data);
        //public static void glCompressedTexSubImage2D (GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, const GLvoid *data);
        //public static void glCompressedTexSubImage1D (GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLsizei imageSize, const GLvoid *data);
        //public static void glGetCompressedTexImage (GLenum target, GLint level, GLvoid *img);
        #endregion

        #region VERSION_1_4
        //public static void glBlendFuncSeparate (GLenum sfactorRGB, GLenum dfactorRGB, GLenum sfactorAlpha, GLenum dfactorAlpha);
        //public static void glMultiDrawArrays (GLenum mode, const GLint *first, const GLsizei *count, GLsizei drawcount);
        //public static void glMultiDrawElements (GLenum mode, const GLsizei *count, GLenum type, const GLvoid* const *indices, GLsizei drawcount);
        //public static void glPointParameterf (GLenum pname, GLfloat parameter);
        //public static void glPointParameterfv (GLenum pname, const GLfloat *parameters);
        //public static void glPointParameteri (GLenum pname, GLint parameter);
        //public static void glPointParameteriv (GLenum pname, const GLint *parameters);
        #endregion

        #region VERSION_1_5

        private IntPtr glGenQueries;
        public static void GenQueries(int n, uint* ids)
        {
            throw new NotImplementedException();
        }

        private IntPtr glDeleteQueries;
        public static void DeleteQueries(int n, uint* ids)
        {
            throw new NotImplementedException();
        }

        private IntPtr glIsQuery;
        public static byte IsQuery(uint id)
        {
            throw new NotImplementedException();
        }

        private IntPtr glBeginQuery;
        public static void BeginQuery(uint target, uint id)
        {
            throw new NotImplementedException();
        }

        private IntPtr glEndQuery;
        public static void EndQuery(uint target)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetQueryiv;
        public static void GetQueryiv(uint target, uint pname, int* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetQueryObjectiv;
        public static void GetQueryObjectiv(uint target, uint pname, int* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetQueryObjectuiv;
        public static void GetQueryObjectuiv(uint target, uint pname, uint* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glBindBuffer;
        public static void BindBuffer(uint target, uint buffer)
        {
            throw new NotImplementedException();
        }

        private IntPtr glDeleteBuffers;
        public static void DeleteBuffers(int n, uint* buffers)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGenBuffers;
        public static void GenBuffers(int n, uint* buffers)
        {
            throw new NotImplementedException();
        }

        private IntPtr glIsBuffer;
        public static byte IsBuffer(uint buffer)
        {
            throw new NotImplementedException();
        }

        private IntPtr glBufferData;
        public static void BufferData(uint target, void* size, void* data, uint usage)
        {
            throw new NotImplementedException();
        }

        private IntPtr glBufferSubData;
        public static void BufferSubData(uint target, void* offset, void* size, void* data)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetBufferSubData;
        public static void GetBufferSubData(uint target, void* offset, void* size, void* data)
        {
            throw new NotImplementedException();
        }

        private IntPtr glMapBuffer;
        public static void* MapBuffer(uint target, uint access)
        {
            throw new NotImplementedException();
        }

        private IntPtr glUnmapBuffer;
        public static byte UnmapBuffer(uint target)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetBufferParameteriv;
        public static void GetBufferParameteriv(uint target, uint pname, int* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetBufferPointerv;
        public static void GetBufferPointerv(uint target, uint pname, void** parameters)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region VERSION_2_0
        //public static void glBlendEquationSeparate (GLenum modeRGB, GLenum modeAlpha);
        //public static void glDrawBuffers (GLsizei n, const GLenum *bufs);
        //public static void glStencilOpSeparate (GLenum face, GLenum sfail, GLenum dpfail, GLenum dppass);
        //public static void glStencilFuncSeparate (GLenum face, GLenum func, GLint ref, GLuint mask);
        //public static void glStencilMaskSeparate (GLenum face, GLuint mask);

        private IntPtr glAttachShader;
        public static void AttachShader(uint program, uint shader)
        {
            throw new NotImplementedException();
        }
        //public static void glBindAttribLocation (GLuint program, GLuint index, const GLchar *name);

        private IntPtr glCompileShader;
        public static void CompileShader(uint shader)
        {
            throw new NotImplementedException();
        }

        private IntPtr glCreateProgram;
        public static uint CreateProgram()
        {
            throw new NotImplementedException();
        }

        private IntPtr glCreateShader;
        public static uint CreateShader(uint type)
        {
            throw new NotImplementedException();
        }

        private IntPtr glDeleteProgram;
        public static void DeleteProgram(uint program)
        {
            throw new NotImplementedException();
        }

        private IntPtr glDeleteShader;
        public static void DeleteShader(uint shader)
        {
            throw new NotImplementedException();
        }

        private IntPtr glDetachShader;
        public static void DetachShader(uint program, uint shader)
        {
            throw new NotImplementedException();
        }

        private IntPtr glDisableVertexAttribArray;
        public static void DisableVertexAttribArray(uint index)
        {
            throw new NotImplementedException();
        }

        private IntPtr glEnableVertexAttribArray;
        public static void EnableVertexAttribArray(uint index)
        {
            throw new NotImplementedException();
        }
        //public static void glGetActiveAttrib (GLuint program, GLuint index, GLsizei bufSize, GLsizei *length, GLint *size, GLenum *type, GLchar *name);
        //public static void glGetActiveUniform (GLuint program, GLuint index, GLsizei bufSize, GLsizei *length, GLint *size, GLenum *type, GLchar *name);
        //public static void glGetAttachedShaders (GLuint program, GLsizei maxCount, GLsizei *count, GLuint *obj);
        //public static GLint glGetAttribLocation (GLuint program, const GLchar *name);

        private IntPtr glGetProgramiv;
        public static void GetProgramiv(uint program, uint pname, int* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetProgramInfoLog;
        public static void GetProgramInfoLog(uint program, int bufSize, int* length, byte* infoLog)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetShaderiv;
        public static void GetShaderiv(uint shader, uint pname, int* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetShaderInfoLog;
        public static void GetShaderInfoLog(uint shader, int bufSize, int* length, byte* infoLog)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetShaderSource;
        public static void GetShaderSource(uint shader, int bufSize, int* length, byte* source)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetUniformLocation;
        public static int GetUniformLocation (uint program, byte* name)
        {
            throw new NotImplementedException();
        }
        //public static void glGetUniformfv (GLuint program, GLint location, GLfloat *parameters);
        //public static void glGetUniformiv (GLuint program, GLint location, GLint *parameters);
        //public static void glGetVertexAttribdv (GLuint index, GLenum pname, GLdouble *parameters);
        //public static void glGetVertexAttribfv (GLuint index, GLenum pname, GLfloat *parameters);

        private IntPtr glGetVertexAttribiv;
        public static void GetVertexAttribiv(uint index, uint pname, int* parameters)
        {
            throw new NotImplementedException();
        }
        //public static void glGetVertexAttribPointerv (GLuint index, GLenum pname, GLvoid* *pointer);

        private IntPtr glIsProgram;
        public static byte IsProgram(uint program)
        {
            throw new NotImplementedException();
        }

        private IntPtr glIsShader;
        public static byte IsShader(uint shader)
        {
            throw new NotImplementedException();
        }

        private IntPtr glLinkProgram;
        public static void LinkProgram(uint program)
        {
            throw new NotImplementedException();
        }

        private IntPtr glShaderSource;
        public static void ShaderSource(uint shader, int count, byte** str, int* length)
        {
            throw new NotImplementedException();
        }

        private IntPtr glUseProgram;
        public static void UseProgram(uint program)
        {
            throw new NotImplementedException();
        }

        private IntPtr glUniform1f;
        public static void Uniform1f(int location, float v01)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform2f;
        public static void Uniform2f(int location, float v0, float v1)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform3f;
        public static void Uniform3f(int location, float v0, float v1, float v2)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform4f;
        public static void Uniform4f(int location, float v0, float v1, float v2, float v3)
        {
            throw new NotImplementedException();
        }

        private IntPtr glUniform1i;
        public static void Uniform1i(int location, int v01)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform2i;
        public static void Uniform2i(int location, int v0, int v1)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform3i;
        public static void Uniform3i(int location, int v0, int v1, int v2)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform4i;
        public static void Uniform4i(int location, int v0, int v1, int v2, int v3)
        {
            throw new NotImplementedException();
        }

        private IntPtr glUniform1fv;
        public static void Uniform1fv(int location, int count, float* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform2fv;
        public static void Uniform2fv(int location, int count, float* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform3fv;
        public static void Uniform3fv(int location, int count, float* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform4fv;
        public static void Uniform4fv(int location, int count, float* value)
        {
            throw new NotImplementedException();
        }

        private IntPtr glUniform1iv;
        public static void Uniform1iv(int location, int count, int* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform2iv;
        public static void Uniform2iv(int location, int count, int* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform3iv;
        public static void Uniform3iv(int location, int count, int* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform4iv;
        public static void Uniform4iv(int location, int count, int* value)
        {
            throw new NotImplementedException();
        }

        private IntPtr glUniformMatrix2fv;
        public static void UniformMatrix2fv(int location, int count, byte transpose, float* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniformMatrix3fv;
        public static void UniformMatrix3fv(int location, int count, byte transpose, float* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniformMatrix4fv;
        public static void UniformMatrix4fv(int location, int count, byte transpose, float* value)
        {
            throw new NotImplementedException();
        }
        
        //public static void glValidateProgram (GLuint program);
        //public static void glVertexAttrib1d (GLuint index, GLdouble x);
        //public static void glVertexAttrib1dv (GLuint index, const GLdouble *v);
        //public static void glVertexAttrib1f (GLuint index, GLfloat x);
        //public static void glVertexAttrib1fv (GLuint index, const GLfloat *v);
        //public static void glVertexAttrib1s (GLuint index, GLshort x);
        //public static void glVertexAttrib1sv (GLuint index, const GLshort *v);
        //public static void glVertexAttrib2d (GLuint index, GLdouble x, GLdouble y);
        //public static void glVertexAttrib2dv (GLuint index, const GLdouble *v);
        //public static void glVertexAttrib2f (GLuint index, GLfloat x, GLfloat y);
        //public static void glVertexAttrib2fv (GLuint index, const GLfloat *v);
        //public static void glVertexAttrib2s (GLuint index, GLshort x, GLshort y);
        //public static void glVertexAttrib2sv (GLuint index, const GLshort *v);
        //public static void glVertexAttrib3d (GLuint index, GLdouble x, GLdouble y, GLdouble z);
        //public static void glVertexAttrib3dv (GLuint index, const GLdouble *v);
        //public static void glVertexAttrib3f (GLuint index, GLfloat x, GLfloat y, GLfloat z);
        //public static void glVertexAttrib3fv (GLuint index, const GLfloat *v);
        //public static void glVertexAttrib3s (GLuint index, GLshort x, GLshort y, GLshort z);
        //public static void glVertexAttrib3sv (GLuint index, const GLshort *v);
        //public static void glVertexAttrib4Nbv (GLuint index, const GLbyte *v);
        //public static void glVertexAttrib4Niv (GLuint index, const GLint *v);
        //public static void glVertexAttrib4Nsv (GLuint index, const GLshort *v);
        //public static void glVertexAttrib4Nub (GLuint index, GLubyte x, GLubyte y, GLubyte z, GLubyte w);
        //public static void glVertexAttrib4Nubv (GLuint index, const GLubyte *v);
        //public static void glVertexAttrib4Nuiv (GLuint index, const GLuint *v);
        //public static void glVertexAttrib4Nusv (GLuint index, const GLushort *v);
        //public static void glVertexAttrib4bv (GLuint index, const GLbyte *v);
        //public static void glVertexAttrib4d (GLuint index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
        //public static void glVertexAttrib4dv (GLuint index, const GLdouble *v);
        //public static void glVertexAttrib4f (GLuint index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);
        //public static void glVertexAttrib4fv (GLuint index, const GLfloat *v);
        //public static void glVertexAttrib4iv (GLuint index, const GLint *v);
        //public static void glVertexAttrib4s (GLuint index, GLshort x, GLshort y, GLshort z, GLshort w);
        //public static void glVertexAttrib4sv (GLuint index, const GLshort *v);
        //public static void glVertexAttrib4ubv (GLuint index, const GLubyte *v);
        //public static void glVertexAttrib4uiv (GLuint index, const GLuint *v);
        //public static void glVertexAttrib4usv (GLuint index, const GLushort *v);

        private IntPtr glVertexAttribPointer;
        public static void VertexAttribPointer(uint index, int size, uint type, byte normalized, int stride, void* pointer)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region VERSION_2_1
        private IntPtr glUniformMatrix2x3fv;
        public static void UniformMatrix2x3fv(int location, int count, byte transpose, float* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniformMatrix3x2fv;
        public static void UniformMatrix3x2fv(int location, int count, byte transpose, float* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniformMatrix2x4fv;
        public static void UniformMatrix2x4fv(int location, int count, byte transpose, float* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniformMatrix4x2fv;
        public static void UniformMatrix4x2fv(int location, int count, byte transpose, float* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniformMatrix3x4fv;
        public static void UniformMatrix3x4fv(int location, int count, byte transpose, float* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniformMatrix4x3fv;
        public static void UniformMatrix4x3fv(int location, int count, byte transpose, float* value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region VERSION_3_0
        ///* OpenGL 3.0 also reuses entry points from these extensions: */
        ///* ARB_framebuffer_object */
        ///* ARB_map_buffer_range */
        ///* ARB_vertex_array_object */
        
        //public static void glColorMaski (GLuint index, GLboolean r, GLboolean g, GLboolean b, GLboolean a);
        //public static void glGetBooleani_v (GLenum target, GLuint index, GLboolean *data);
        //public static void glGetIntegeri_v (GLenum target, GLuint index, GLint *data);
        //public static void glEnablei (GLenum target, GLuint index);
        //public static void glDisablei (GLenum target, GLuint index);
        //public static GLboolean glIsEnabledi (GLenum target, GLuint index);
        //public static void glBeginTransformFeedback (GLenum primitiveMode);
        //public static void glEndTransformFeedback (void);
        //public static void glBindBufferRange (GLenum target, GLuint index, GLuint buffer, GLintptr offset, GLsizeiptr size);
        //public static void glBindBufferBase (GLenum target, GLuint index, GLuint buffer);
        //public static void glTransformFeedbackVaryings (GLuint program, GLsizei count, const GLchar* const *varyings, GLenum bufferMode);
        //public static void glGetTransformFeedbackVarying (GLuint program, GLuint index, GLsizei bufSize, GLsizei *length, GLsizei *size, GLenum *type, GLchar *name);
        //public static void glClampColor (GLenum target, GLenum clamp);
        //public static void glBeginConditionalRender (GLuint id, GLenum mode);
        //public static void glEndConditionalRender (void);
        //public static void glVertexAttribIPointer (GLuint index, GLint size, GLenum type, GLsizei stride, const GLvoid *pointer);
        //public static void glGetVertexAttribIiv (GLuint index, GLenum pname, GLint *parameters);
        //public static void glGetVertexAttribIuiv (GLuint index, GLenum pname, GLuint *parameters);
        //public static void glVertexAttribI1i (GLuint index, GLint x);
        //public static void glVertexAttribI2i (GLuint index, GLint x, GLint y);
        //public static void glVertexAttribI3i (GLuint index, GLint x, GLint y, GLint z);
        //public static void glVertexAttribI4i (GLuint index, GLint x, GLint y, GLint z, GLint w);
        //public static void glVertexAttribI1ui (GLuint index, GLuint x);
        //public static void glVertexAttribI2ui (GLuint index, GLuint x, GLuint y);
        //public static void glVertexAttribI3ui (GLuint index, GLuint x, GLuint y, GLuint z);
        //public static void glVertexAttribI4ui (GLuint index, GLuint x, GLuint y, GLuint z, GLuint w);
        //public static void glVertexAttribI1iv (GLuint index, const GLint *v);
        //public static void glVertexAttribI2iv (GLuint index, const GLint *v);
        //public static void glVertexAttribI3iv (GLuint index, const GLint *v);
        //public static void glVertexAttribI4iv (GLuint index, const GLint *v);
        //public static void glVertexAttribI1uiv (GLuint index, const GLuint *v);
        //public static void glVertexAttribI2uiv (GLuint index, const GLuint *v);
        //public static void glVertexAttribI3uiv (GLuint index, const GLuint *v);
        //public static void glVertexAttribI4uiv (GLuint index, const GLuint *v);
        //public static void glVertexAttribI4bv (GLuint index, const GLbyte *v);
        //public static void glVertexAttribI4sv (GLuint index, const GLshort *v);
        //public static void glVertexAttribI4ubv (GLuint index, const GLubyte *v);
        //public static void glVertexAttribI4usv (GLuint index, const GLushort *v);
        //public static void glGetUniformuiv (GLuint program, GLint location, GLuint *parameters);
        //public static void glBindFragDataLocation (GLuint program, GLuint color, const GLchar *name);
        //public static GLint glGetFragDataLocation (GLuint program, const GLchar *name);
        private IntPtr glUniform1ui;
        public static void Uniform1ui(int location, uint v01)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform2ui;
        public static void Uniform2ui(int location, uint v0, uint v1)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform3ui;
        public static void Uniform3ui(int location, uint v0, uint v1, uint v2)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform4ui;
        public static void Uniform4ui(int location, uint v0, uint v1, uint v2, uint v3)
        {
            throw new NotImplementedException();
        }
        
        private IntPtr glUniform1uiv;
        public static void Uniform1uiv(int location, int count, uint* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform2uiv;
        public static void Uniform2uiv(int location, int count, uint* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform3uiv;
        public static void Uniform3uiv(int location, int count, uint* value)
        {
            throw new NotImplementedException();
        }
        private IntPtr glUniform4uiv;
        public static void Uniform4uiv(int location, int count, uint* value)
        {
            throw new NotImplementedException();
        }

        //public static void glTexParameterIiv (GLenum target, GLenum pname, const GLint *parameters);
        //public static void glTexParameterIuiv (GLenum target, GLenum pname, const GLuint *parameters);
        //public static void glGetTexParameterIiv (GLenum target, GLenum pname, GLint *parameters);
        //public static void glGetTexParameterIuiv (GLenum target, GLenum pname, GLuint *parameters);
        //public static void glClearBufferiv (GLenum buffer, GLint drawbuffer, const GLint *value);
        //public static void glClearBufferuiv (GLenum buffer, GLint drawbuffer, const GLuint *value);
        //public static void glClearBufferfv (GLenum buffer, GLint drawbuffer, const GLfloat *value);
        //public static void glClearBufferfi (GLenum buffer, GLint drawbuffer, GLfloat depth, GLint stencil);

        private IntPtr glGetStringi;
        public static byte* GetStringi(uint name, uint index)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region VERSION_3_1
        ///* OpenGL 3.1 also reuses entry points from these extensions: */
        ///* ARB_copy_buffer */
        ///* ARB_uniform_buffer_object */

        //public static void glDrawArraysInstanced (GLenum mode, GLint first, GLsizei count, GLsizei instancecount);
        //public static void glDrawElementsInstanced (GLenum mode, GLsizei count, GLenum type, const GLvoid *indices, GLsizei instancecount);
        //public static void glTexBuffer (GLenum target, GLenum internalformat, GLuint buffer);
        //public static void glPrimitiveRestartIndex (GLuint index);
        #endregion

        #region VERSION_3_2
        ///* OpenGL 3.2 also reuses entry points from these extensions: */
        ///* ARB_draw_elements_base_vertex */
        ///* ARB_provoking_vertex */
        ///* ARB_sync */
        ///* ARB_texture_multisample */

        private IntPtr glGetInteger64i_v;
        public static void GetInteger64i_v(uint target, uint index, long* data)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetBufferParameteri64v;
        public static void GetBufferParameteri64v(uint target, uint pname, long* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glFramebufferTexture;
        public static void FramebufferTexture(uint target, uint attachment, uint texture, int level)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region VERSION_3_3
        ///* OpenGL 3.3 also reuses entry points from these extensions: */
        ///* ARB_blend_func_extended */
        ///* ARB_sampler_objects */
        ///* ARB_explicit_attrib_location, but it has none */
        ///* ARB_occlusion_query2 (no entry points) */
        ///* ARB_shader_bit_encoding (no entry points) */
        ///* ARB_texture_rgb10_a2ui (no entry points) */
        ///* ARB_texture_swizzle (no entry points) */
        ///* ARB_timer_query */
        ///* ARB_vertex_type_2_10_10_10_rev */

        //public static void glVertexAttribDivisor (GLuint index, GLuint divisor);
        #endregion

        #region VERSION_4_0
        ///* OpenGL 4.0 also reuses entry points from these extensions: */
        ///* ARB_texture_query_lod (no entry points) */
        ///* ARB_draw_indirect */
        ///* ARB_gpu_shader5 (no entry points) */
        ///* ARB_gpu_shader_fp64 */
        ///* ARB_shader_subroutine */
        ///* ARB_tessellation_shader */
        ///* ARB_texture_buffer_object_rgb32 (no entry points) */
        ///* ARB_texture_cube_map_array (no entry points) */
        ///* ARB_texture_gather (no entry points) */
        ///* ARB_transform_feedback2 */
        ///* ARB_transform_feedback3 */

        //public static void glMinSampleShading (GLfloat value);
        //public static void glBlendEquationi (GLuint buf, GLenum mode);
        //public static void glBlendEquationSeparatei (GLuint buf, GLenum modeRGB, GLenum modeAlpha);
        //public static void glBlendFunci (GLuint buf, GLenum src, GLenum dst);
        //public static void glBlendFuncSeparatei (GLuint buf, GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);
        #endregion

        #region VERSION_4_1
        ///* OpenGL 4.1 reuses entry points from these extensions: */
        ///* ARB_ES2_compatibility */
        ///* ARB_get_program_binary */
        ///* ARB_separate_shader_objects */
        ///* ARB_shader_precision (no entry points) */
        ///* ARB_vertex_attrib_64bit */
        ///* ARB_viewport_array */
        #endregion

        #region VERSION_4_2
        ///* OpenGL 4.2 reuses entry points from these extensions: */
        ///* ARB_base_instance */
        ///* ARB_shading_language_420pack (no entry points) */
        ///* ARB_transform_feedback_instanced */
        ///* ARB_compressed_texture_pixel_storage (no entry points) */
        ///* ARB_conservative_depth (no entry points) */
        ///* ARB_internalformat_query */
        ///* ARB_map_buffer_alignment (no entry points) */
        ///* ARB_shader_atomic_counters */
        ///* ARB_shader_image_load_store */
        ///* ARB_shading_language_packing (no entry points) */
        ///* ARB_texture_storage */
        #endregion

        #region VERSION_4_3
        ///* OpenGL 4.3 reuses entry points from these extensions: */
        ///* ARB_arrays_of_arrays (no entry points, GLSL only) */
        ///* ARB_fragment_layer_viewport (no entry points, GLSL only) */
        ///* ARB_shader_image_size (no entry points, GLSL only) */
        ///* ARB_ES3_compatibility (no entry points) */
        ///* ARB_clear_buffer_object */
        ///* ARB_compute_shader */
        ///* ARB_copy_image */
        ///* KHR_debug (includes ARB_debug_output commands promoted to KHR without suffixes) */
        ///* ARB_explicit_uniform_location (no entry points) */
        ///* ARB_framebuffer_no_attachments */
        ///* ARB_internalformat_query2 */
        ///* ARB_invalidate_subdata */
        ///* ARB_multi_draw_indirect */
        ///* ARB_program_interface_query */
        ///* ARB_robust_buffer_access_behavior (no entry points) */
        ///* ARB_shader_storage_buffer_object */
        ///* ARB_stencil_texturing (no entry points) */
        ///* ARB_texture_buffer_range */
        ///* ARB_texture_query_levels (no entry points) */
        ///* ARB_texture_storage_multisample */
        ///* ARB_texture_view */
        ///* ARB_vertex_attrib_binding */
        #endregion

        #region ARB_depth_buffer_float

        //public const uint ARB_depth_buffer_float 1
        #endregion

        #region ARB_framebuffer_object

        //public const uint ARB_framebuffer_object 1
        #region GLCOREARB_PROTOTYPES

        //public static GLboolean glIsRenderbuffer (GLuint renderbuffer);
        //public static void glBindRenderbuffer (GLenum target, GLuint renderbuffer);
        //public static void glDeleteRenderbuffers (GLsizei n, const GLuint *renderbuffers);
        //public static void glGenRenderbuffers (GLsizei n, GLuint *renderbuffers);
        //public static void glRenderbufferStorage (GLenum target, GLenum internalformat, GLsizei width, GLsizei height);
        //public static void glGetRenderbufferParameteriv (GLenum target, GLenum pname, GLint *parameters);
        //public static GLboolean glIsFramebuffer (GLuint framebuffer);
        //public static void glBindFramebuffer (GLenum target, GLuint framebuffer);
        //public static void glDeleteFramebuffers (GLsizei n, const GLuint *framebuffers);
        //public static void glGenFramebuffers (GLsizei n, GLuint *framebuffers);
        //public static GLenum glCheckFramebufferStatus (GLenum target);
        //public static void glFramebufferTexture1D (GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);
        //public static void glFramebufferTexture2D (GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);
        //public static void glFramebufferTexture3D (GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level, GLint zoffset);
        //public static void glFramebufferRenderbuffer (GLenum target, GLenum attachment, GLenum renderbuffertarget, GLuint renderbuffer);
        //public static void glGetFramebufferAttachmentParameteriv (GLenum target, GLenum attachment, GLenum pname, GLint *parameters);
        //public static void glGenerateMipmap (GLenum target);
        //public static void glBlitFramebuffer (GLint srcX0, GLint srcY0, GLint srcX1, GLint srcY1, GLint dstX0, GLint dstY0, GLint dstX1, GLint dstY1, GLbitfield mask, GLenum filter);
        //public static void glRenderbufferStorageMultisample (GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height);
        //public static void glFramebufferTextureLayer (GLenum target, GLenum attachment, GLuint texture, GLint level, GLint layer);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef GLboolean (APIENTRYP PFNGLISRENDERBUFFERPROC) (GLuint renderbuffer);
        //typedef void (APIENTRYP PFNGLBINDRENDERBUFFERPROC) (GLenum target, GLuint renderbuffer);
        //typedef void (APIENTRYP PFNGLDELETERENDERBUFFERSPROC) (GLsizei n, const GLuint *renderbuffers);
        //typedef void (APIENTRYP PFNGLGENRENDERBUFFERSPROC) (GLsizei n, GLuint *renderbuffers);
        //typedef void (APIENTRYP PFNGLRENDERBUFFERSTORAGEPROC) (GLenum target, GLenum internalformat, GLsizei width, GLsizei height);
        //typedef void (APIENTRYP PFNGLGETRENDERBUFFERPARAMETERIVPROC) (GLenum target, GLenum pname, GLint *parameters);
        //typedef GLboolean (APIENTRYP PFNGLISFRAMEBUFFERPROC) (GLuint framebuffer);
        //typedef void (APIENTRYP PFNGLBINDFRAMEBUFFERPROC) (GLenum target, GLuint framebuffer);
        //typedef void (APIENTRYP PFNGLDELETEFRAMEBUFFERSPROC) (GLsizei n, const GLuint *framebuffers);
        //typedef void (APIENTRYP PFNGLGENFRAMEBUFFERSPROC) (GLsizei n, GLuint *framebuffers);
        //typedef GLenum (APIENTRYP PFNGLCHECKFRAMEBUFFERSTATUSPROC) (GLenum target);
        //typedef void (APIENTRYP PFNGLFRAMEBUFFERTEXTURE1DPROC) (GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);
        //typedef void (APIENTRYP PFNGLFRAMEBUFFERTEXTURE2DPROC) (GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);
        //typedef void (APIENTRYP PFNGLFRAMEBUFFERTEXTURE3DPROC) (GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level, GLint zoffset);
        //typedef void (APIENTRYP PFNGLFRAMEBUFFERRENDERBUFFERPROC) (GLenum target, GLenum attachment, GLenum renderbuffertarget, GLuint renderbuffer);
        //typedef void (APIENTRYP PFNGLGETFRAMEBUFFERATTACHMENTPARAMETERIVPROC) (GLenum target, GLenum attachment, GLenum pname, GLint *parameters);
        //typedef void (APIENTRYP PFNGLGENERATEMIPMAPPROC) (GLenum target);
        //typedef void (APIENTRYP PFNGLBLITFRAMEBUFFERPROC) (GLint srcX0, GLint srcY0, GLint srcX1, GLint srcY1, GLint dstX0, GLint dstY0, GLint dstX1, GLint dstY1, GLbitfield mask, GLenum filter);
        //typedef void (APIENTRYP PFNGLRENDERBUFFERSTORAGEMULTISAMPLEPROC) (GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height);
        //typedef void (APIENTRYP PFNGLFRAMEBUFFERTEXTURELAYERPROC) (GLenum target, GLenum attachment, GLuint texture, GLint level, GLint layer);
        #endregion

        #region ARB_framebuffer_sRGB

        //public const uint ARB_framebuffer_sRGB 1
        #endregion

        #region ARB_half_float_vertex

        //public const uint ARB_half_float_vertex 1
        #endregion

        #region ARB_map_buffer_range

        //public const uint ARB_map_buffer_range 1
        #region GLCOREARB_PROTOTYPES

        //public static GLvoid* glMapBufferRange (GLenum target, GLintptr offset, GLsizeiptr length, GLbitfield access);
        //public static void glFlushMappedBufferRange (GLenum target, GLintptr offset, GLsizeiptr length);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef GLvoid* (APIENTRYP PFNGLMAPBUFFERRANGEPROC) (GLenum target, GLintptr offset, GLsizeiptr length, GLbitfield access);
        //typedef void (APIENTRYP PFNGLFLUSHMAPPEDBUFFERRANGEPROC) (GLenum target, GLintptr offset, GLsizeiptr length);
        #endregion

        #region ARB_texture_compression_rgtc

        //public const uint ARB_texture_compression_rgtc 1
        #endregion

        #region ARB_texture_rg

        //public const uint ARB_texture_rg 1
        #endregion

        #region ARB_vertex_array_object
        private IntPtr glBindVertexArray;
        public static void BindVertexArray(uint array)
        {
            throw new NotImplementedException();
        }

        private IntPtr glDeleteVertexArrays;
        public static void DeleteVertexArrays(int n, uint* arrays)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGenVertexArrays;
        public static void GenVertexArrays(int n, uint* arrays)
        {
            throw new NotImplementedException();
        }

        private IntPtr glIsVertexArray;
        public static byte IsVertexArray(uint array)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ARB_uniform_buffer_object
        //public static void glGetUniformIndices (GLuint program, GLsizei uniformCount, const GLchar* const *uniformNames, GLuint *uniformIndices);
        //public static void glGetActiveUniformsiv (GLuint program, GLsizei uniformCount, const GLuint *uniformIndices, GLenum pname, GLint *parameters);
        //public static void glGetActiveUniformName (GLuint program, GLuint uniformIndex, GLsizei bufSize, GLsizei *length, GLchar *uniformName);

        private IntPtr glGetUniformBlockIndex;
        public static uint GetUniformBlockIndex (uint program, byte* uniformBlockName)
        {
            throw new NotImplementedException();
        }

        //public static void glGetActiveUniformBlockiv (GLuint program, GLuint uniformBlockIndex, GLenum pname, GLint *parameters);
        //public static void glGetActiveUniformBlockName (GLuint program, GLuint uniformBlockIndex, GLsizei bufSize, GLsizei *length, GLchar *uniformBlockName);

        private IntPtr glUniformBlockBinding;
        public static uint UniformBlockBinding(uint program,  uint uniformBlockIndex, uint uniformBlockBinding)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ARB_copy_buffer

        //public const uint ARB_copy_buffer 1
        #region GLCOREARB_PROTOTYPES

        //public static void glCopyBufferSubData (GLenum readTarget, GLenum writeTarget, GLintptr readOffset, GLintptr writeOffset, GLsizeiptr size);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLCOPYBUFFERSUBDATAPROC) (GLenum readTarget, GLenum writeTarget, GLintptr readOffset, GLintptr writeOffset, GLsizeiptr size);
        #endregion

        #region ARB_depth_clamp

        //public const uint ARB_depth_clamp 1
        #endregion

        #region ARB_draw_elements_base_vertex

        //public const uint ARB_draw_elements_base_vertex 1
        #region GLCOREARB_PROTOTYPES

        private IntPtr glDrawElementsBaseVertex;
        public static void DrawElementsBaseVertex(uint mode, int count, uint type, void* indices, int basevertex)
        {
            throw new NotImplementedException();
        }
    
        //public static void glDrawRangeElementsBaseVertex (GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, const GLvoid *indices, GLint basevertex);
        //public static void glDrawElementsInstancedBaseVertex (GLenum mode, GLsizei count, GLenum type, const GLvoid *indices, GLsizei instancecount, GLint basevertex);
        //public static void glMultiDrawElementsBaseVertex (GLenum mode, const GLsizei *count, GLenum type, const GLvoid* const *indices, GLsizei drawcount, const GLint *basevertex);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLDRAWELEMENTSBASEVERTEXPROC) (GLenum mode, GLsizei count, GLenum type, const GLvoid *indices, GLint basevertex);
        //typedef void (APIENTRYP PFNGLDRAWRANGEELEMENTSBASEVERTEXPROC) (GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, const GLvoid *indices, GLint basevertex);
        //typedef void (APIENTRYP PFNGLDRAWELEMENTSINSTANCEDBASEVERTEXPROC) (GLenum mode, GLsizei count, GLenum type, const GLvoid *indices, GLsizei instancecount, GLint basevertex);
        //typedef void (APIENTRYP PFNGLMULTIDRAWELEMENTSBASEVERTEXPROC) (GLenum mode, const GLsizei *count, GLenum type, const GLvoid* const *indices, GLsizei drawcount, const GLint *basevertex);
        #endregion

        #region ARB_fragment_coord_conventions

        //public const uint ARB_fragment_coord_conventions 1
        #endregion

        #region ARB_provoking_vertex

        //public const uint ARB_provoking_vertex 1
        #region GLCOREARB_PROTOTYPES

        //public static void glProvokingVertex (GLenum mode);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLPROVOKINGVERTEXPROC) (GLenum mode);
        #endregion

        #region ARB_seamless_cube_map

        //public const uint ARB_seamless_cube_map 1
        #endregion

        #region ARB_sync

        private IntPtr glFenceSync;
        public static void* FenceSync(uint condition, uint flags)
        {
            throw new NotImplementedException();
        }

        private IntPtr glIsSync;
        public static byte IsSync(void* sync)
        {
            throw new NotImplementedException();
        }

        private IntPtr glDeleteSync;
        public static void DeleteSync(void* sync)
        {
            throw new NotImplementedException();
        }

        private IntPtr glClientWaitSync;
        public static uint ClientWaitSync(void* sync, uint flags, ulong timeout)
        {
            throw new NotImplementedException();
        }

        private IntPtr glWaitSync;
        public static void WaitSync(void* sync, uint flags, ulong timeout)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetInteger64v;
        public static void GetInteger64v(uint pname, long* parameters)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetSynciv;
        public static void GetSynciv(void* sync, uint pname, int bufSize, int* length, int* values)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ARB_texture_multisample

        //public const uint ARB_texture_multisample 1
        #region GLCOREARB_PROTOTYPES

        //public static void glTexImage2DMultisample (GLenum target, GLsizei samples, GLint internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);
        //public static void glTexImage3DMultisample (GLenum target, GLsizei samples, GLint internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);
        //public static void glGetMultisamplefv (GLenum pname, GLuint index, GLfloat *val);
        //public static void glSampleMaski (GLuint index, GLbitfield mask);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLTEXIMAGE2DMULTISAMPLEPROC) (GLenum target, GLsizei samples, GLint internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);
        //typedef void (APIENTRYP PFNGLTEXIMAGE3DMULTISAMPLEPROC) (GLenum target, GLsizei samples, GLint internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);
        //typedef void (APIENTRYP PFNGLGETMULTISAMPLEFVPROC) (GLenum pname, GLuint index, GLfloat *val);
        //typedef void (APIENTRYP PFNGLSAMPLEMASKIPROC) (GLuint index, GLbitfield mask);
        #endregion

        #region ARB_vertex_array_bgra

        //public const uint ARB_vertex_array_bgra 1
        #endregion

        #region ARB_draw_buffers_blend

        //public const uint ARB_draw_buffers_blend 1
        #region GLCOREARB_PROTOTYPES

        //public static void glBlendEquationiARB (GLuint buf, GLenum mode);
        //public static void glBlendEquationSeparateiARB (GLuint buf, GLenum modeRGB, GLenum modeAlpha);
        //public static void glBlendFunciARB (GLuint buf, GLenum src, GLenum dst);
        //public static void glBlendFuncSeparateiARB (GLuint buf, GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLBLENDEQUATIONIARBPROC) (GLuint buf, GLenum mode);
        //typedef void (APIENTRYP PFNGLBLENDEQUATIONSEPARATEIARBPROC) (GLuint buf, GLenum modeRGB, GLenum modeAlpha);
        //typedef void (APIENTRYP PFNGLBLENDFUNCIARBPROC) (GLuint buf, GLenum src, GLenum dst);
        //typedef void (APIENTRYP PFNGLBLENDFUNCSEPARATEIARBPROC) (GLuint buf, GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);
        #endregion

        #region ARB_sample_shading

        //public const uint ARB_sample_shading 1
        #region GLCOREARB_PROTOTYPES

        //public static void glMinSampleShadingARB (GLfloat value);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLMINSAMPLESHADINGARBPROC) (GLfloat value);
        #endregion

        #region ARB_texture_cube_map_array

        //public const uint ARB_texture_cube_map_array 1
        #endregion

        #region ARB_texture_gather

        //public const uint ARB_texture_gather 1
        #endregion

        #region ARB_texture_query_lod

        //public const uint ARB_texture_query_lod 1
        #endregion

        #region ARB_shading_language_include

        //public const uint ARB_shading_language_include 1
        #region GLCOREARB_PROTOTYPES

        //public static void glNamedStringARB (GLenum type, GLint namelen, const GLchar *name, GLint stringlen, const GLchar *string);
        //public static void glDeleteNamedStringARB (GLint namelen, const GLchar *name);
        //public static void glCompileShaderIncludeARB (GLuint shader, GLsizei count, const GLchar* *path, const GLint *length);
        //public static GLboolean glIsNamedStringARB (GLint namelen, const GLchar *name);
        //public static void glGetNamedStringARB (GLint namelen, const GLchar *name, GLsizei bufSize, GLint *stringlen, GLchar *string);
        //public static void glGetNamedStringivARB (GLint namelen, const GLchar *name, GLenum pname, GLint *parameters);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLNAMEDSTRINGARBPROC) (GLenum type, GLint namelen, const GLchar *name, GLint stringlen, const GLchar *string);
        //typedef void (APIENTRYP PFNGLDELETENAMEDSTRINGARBPROC) (GLint namelen, const GLchar *name);
        //typedef void (APIENTRYP PFNGLCOMPILESHADERINCLUDEARBPROC) (GLuint shader, GLsizei count, const GLchar* *path, const GLint *length);
        //typedef GLboolean (APIENTRYP PFNGLISNAMEDSTRINGARBPROC) (GLint namelen, const GLchar *name);
        //typedef void (APIENTRYP PFNGLGETNAMEDSTRINGARBPROC) (GLint namelen, const GLchar *name, GLsizei bufSize, GLint *stringlen, GLchar *string);
        //typedef void (APIENTRYP PFNGLGETNAMEDSTRINGIVARBPROC) (GLint namelen, const GLchar *name, GLenum pname, GLint *parameters);
        #endregion

        #region ARB_texture_compression_bptc

        //public const uint ARB_texture_compression_bptc 1
        #endregion

        #region ARB_blend_func_extended

        //public const uint ARB_blend_func_extended 1
        #region GLCOREARB_PROTOTYPES

        //public static void glBindFragDataLocationIndexed (GLuint program, GLuint colorNumber, GLuint index, const GLchar *name);
        //public static GLint glGetFragDataIndex (GLuint program, const GLchar *name);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLBINDFRAGDATALOCATIONINDEXEDPROC) (GLuint program, GLuint colorNumber, GLuint index, const GLchar *name);
        //typedef GLint (APIENTRYP PFNGLGETFRAGDATAINDEXPROC) (GLuint program, const GLchar *name);
        #endregion

        #region ARB_explicit_attrib_location

        //public const uint ARB_explicit_attrib_location 1
        #endregion

        #region ARB_occlusion_query2

        //public const uint ARB_occlusion_query2 1
        #endregion

        #region ARB_sampler_objects

        //public const uint ARB_sampler_objects 1
        #region GLCOREARB_PROTOTYPES

        //public static void glGenSamplers (GLsizei count, GLuint *samplers);
        //public static void glDeleteSamplers (GLsizei count, const GLuint *samplers);
        //public static GLboolean glIsSampler (GLuint sampler);
        //public static void glBindSampler (GLuint unit, GLuint sampler);
        //public static void glSamplerParameteri (GLuint sampler, GLenum pname, GLint parameter);
        //public static void glSamplerParameteriv (GLuint sampler, GLenum pname, const GLint *parameter);
        //public static void glSamplerParameterf (GLuint sampler, GLenum pname, GLfloat parameter);
        //public static void glSamplerParameterfv (GLuint sampler, GLenum pname, const GLfloat *parameter);
        //public static void glSamplerParameterIiv (GLuint sampler, GLenum pname, const GLint *parameter);
        //public static void glSamplerParameterIuiv (GLuint sampler, GLenum pname, const GLuint *parameter);
        //public static void glGetSamplerParameteriv (GLuint sampler, GLenum pname, GLint *parameters);
        //public static void glGetSamplerParameterIiv (GLuint sampler, GLenum pname, GLint *parameters);
        //public static void glGetSamplerParameterfv (GLuint sampler, GLenum pname, GLfloat *parameters);
        //public static void glGetSamplerParameterIuiv (GLuint sampler, GLenum pname, GLuint *parameters);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLGENSAMPLERSPROC) (GLsizei count, GLuint *samplers);
        //typedef void (APIENTRYP PFNGLDELETESAMPLERSPROC) (GLsizei count, const GLuint *samplers);
        //typedef GLboolean (APIENTRYP PFNGLISSAMPLERPROC) (GLuint sampler);
        //typedef void (APIENTRYP PFNGLBINDSAMPLERPROC) (GLuint unit, GLuint sampler);
        //typedef void (APIENTRYP PFNGLSAMPLERPARAMETERIPROC) (GLuint sampler, GLenum pname, GLint parameter);
        //typedef void (APIENTRYP PFNGLSAMPLERPARAMETERIVPROC) (GLuint sampler, GLenum pname, const GLint *parameter);
        //typedef void (APIENTRYP PFNGLSAMPLERPARAMETERFPROC) (GLuint sampler, GLenum pname, GLfloat parameter);
        //typedef void (APIENTRYP PFNGLSAMPLERPARAMETERFVPROC) (GLuint sampler, GLenum pname, const GLfloat *parameter);
        //typedef void (APIENTRYP PFNGLSAMPLERPARAMETERIIVPROC) (GLuint sampler, GLenum pname, const GLint *parameter);
        //typedef void (APIENTRYP PFNGLSAMPLERPARAMETERIUIVPROC) (GLuint sampler, GLenum pname, const GLuint *parameter);
        //typedef void (APIENTRYP PFNGLGETSAMPLERPARAMETERIVPROC) (GLuint sampler, GLenum pname, GLint *parameters);
        //typedef void (APIENTRYP PFNGLGETSAMPLERPARAMETERIIVPROC) (GLuint sampler, GLenum pname, GLint *parameters);
        //typedef void (APIENTRYP PFNGLGETSAMPLERPARAMETERFVPROC) (GLuint sampler, GLenum pname, GLfloat *parameters);
        //typedef void (APIENTRYP PFNGLGETSAMPLERPARAMETERIUIVPROC) (GLuint sampler, GLenum pname, GLuint *parameters);
        #endregion

        #region ARB_shader_bit_encoding

        //public const uint ARB_shader_bit_encoding 1
        #endregion

        #region ARB_texture_rgb10_a2ui

        //public const uint ARB_texture_rgb10_a2ui 1
        #endregion

        #region ARB_texture_swizzle

        //public const uint ARB_texture_swizzle 1
        #endregion

        #region ARB_timer_query
        private static IntPtr glQueryCounter;
        public static void QueryCounter(uint id, uint target)
        {
            throw new NotImplementedException();
        }

        private static IntPtr glGetQueryObjecti64v;
        public static void GetQueryObjecti64v(uint id, uint pname, long* parameters)
        {
            throw new NotImplementedException();
        }

        private static IntPtr glGetQueryObjectui64v;
        public static void GetQueryObjectui64v(uint id, uint pname, ulong* parameters)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ARB_vertex_type_2_10_10_10_rev

        //public const uint ARB_vertex_type_2_10_10_10_rev 1
        #region GLCOREARB_PROTOTYPES

        //public static void glVertexP2ui (GLenum type, GLuint value);
        //public static void glVertexP2uiv (GLenum type, const GLuint *value);
        //public static void glVertexP3ui (GLenum type, GLuint value);
        //public static void glVertexP3uiv (GLenum type, const GLuint *value);
        //public static void glVertexP4ui (GLenum type, GLuint value);
        //public static void glVertexP4uiv (GLenum type, const GLuint *value);
        //public static void glTexCoordP1ui (GLenum type, GLuint coords);
        //public static void glTexCoordP1uiv (GLenum type, const GLuint *coords);
        //public static void glTexCoordP2ui (GLenum type, GLuint coords);
        //public static void glTexCoordP2uiv (GLenum type, const GLuint *coords);
        //public static void glTexCoordP3ui (GLenum type, GLuint coords);
        //public static void glTexCoordP3uiv (GLenum type, const GLuint *coords);
        //public static void glTexCoordP4ui (GLenum type, GLuint coords);
        //public static void glTexCoordP4uiv (GLenum type, const GLuint *coords);
        //public static void glMultiTexCoordP1ui (GLenum texture, GLenum type, GLuint coords);
        //public static void glMultiTexCoordP1uiv (GLenum texture, GLenum type, const GLuint *coords);
        //public static void glMultiTexCoordP2ui (GLenum texture, GLenum type, GLuint coords);
        //public static void glMultiTexCoordP2uiv (GLenum texture, GLenum type, const GLuint *coords);
        //public static void glMultiTexCoordP3ui (GLenum texture, GLenum type, GLuint coords);
        //public static void glMultiTexCoordP3uiv (GLenum texture, GLenum type, const GLuint *coords);
        //public static void glMultiTexCoordP4ui (GLenum texture, GLenum type, GLuint coords);
        //public static void glMultiTexCoordP4uiv (GLenum texture, GLenum type, const GLuint *coords);
        //public static void glNormalP3ui (GLenum type, GLuint coords);
        //public static void glNormalP3uiv (GLenum type, const GLuint *coords);
        //public static void glColorP3ui (GLenum type, GLuint color);
        //public static void glColorP3uiv (GLenum type, const GLuint *color);
        //public static void glColorP4ui (GLenum type, GLuint color);
        //public static void glColorP4uiv (GLenum type, const GLuint *color);
        //public static void glSecondaryColorP3ui (GLenum type, GLuint color);
        //public static void glSecondaryColorP3uiv (GLenum type, const GLuint *color);
        //public static void glVertexAttribP1ui (GLuint index, GLenum type, GLboolean normalized, GLuint value);
        //public static void glVertexAttribP1uiv (GLuint index, GLenum type, GLboolean normalized, const GLuint *value);
        //public static void glVertexAttribP2ui (GLuint index, GLenum type, GLboolean normalized, GLuint value);
        //public static void glVertexAttribP2uiv (GLuint index, GLenum type, GLboolean normalized, const GLuint *value);
        //public static void glVertexAttribP3ui (GLuint index, GLenum type, GLboolean normalized, GLuint value);
        //public static void glVertexAttribP3uiv (GLuint index, GLenum type, GLboolean normalized, const GLuint *value);
        //public static void glVertexAttribP4ui (GLuint index, GLenum type, GLboolean normalized, GLuint value);
        //public static void glVertexAttribP4uiv (GLuint index, GLenum type, GLboolean normalized, const GLuint *value);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLVERTEXP2UIPROC) (GLenum type, GLuint value);
        //typedef void (APIENTRYP PFNGLVERTEXP2UIVPROC) (GLenum type, const GLuint *value);
        //typedef void (APIENTRYP PFNGLVERTEXP3UIPROC) (GLenum type, GLuint value);
        //typedef void (APIENTRYP PFNGLVERTEXP3UIVPROC) (GLenum type, const GLuint *value);
        //typedef void (APIENTRYP PFNGLVERTEXP4UIPROC) (GLenum type, GLuint value);
        //typedef void (APIENTRYP PFNGLVERTEXP4UIVPROC) (GLenum type, const GLuint *value);
        //typedef void (APIENTRYP PFNGLTEXCOORDP1UIPROC) (GLenum type, GLuint coords);
        //typedef void (APIENTRYP PFNGLTEXCOORDP1UIVPROC) (GLenum type, const GLuint *coords);
        //typedef void (APIENTRYP PFNGLTEXCOORDP2UIPROC) (GLenum type, GLuint coords);
        //typedef void (APIENTRYP PFNGLTEXCOORDP2UIVPROC) (GLenum type, const GLuint *coords);
        //typedef void (APIENTRYP PFNGLTEXCOORDP3UIPROC) (GLenum type, GLuint coords);
        //typedef void (APIENTRYP PFNGLTEXCOORDP3UIVPROC) (GLenum type, const GLuint *coords);
        //typedef void (APIENTRYP PFNGLTEXCOORDP4UIPROC) (GLenum type, GLuint coords);
        //typedef void (APIENTRYP PFNGLTEXCOORDP4UIVPROC) (GLenum type, const GLuint *coords);
        //typedef void (APIENTRYP PFNGLMULTITEXCOORDP1UIPROC) (GLenum texture, GLenum type, GLuint coords);
        //typedef void (APIENTRYP PFNGLMULTITEXCOORDP1UIVPROC) (GLenum texture, GLenum type, const GLuint *coords);
        //typedef void (APIENTRYP PFNGLMULTITEXCOORDP2UIPROC) (GLenum texture, GLenum type, GLuint coords);
        //typedef void (APIENTRYP PFNGLMULTITEXCOORDP2UIVPROC) (GLenum texture, GLenum type, const GLuint *coords);
        //typedef void (APIENTRYP PFNGLMULTITEXCOORDP3UIPROC) (GLenum texture, GLenum type, GLuint coords);
        //typedef void (APIENTRYP PFNGLMULTITEXCOORDP3UIVPROC) (GLenum texture, GLenum type, const GLuint *coords);
        //typedef void (APIENTRYP PFNGLMULTITEXCOORDP4UIPROC) (GLenum texture, GLenum type, GLuint coords);
        //typedef void (APIENTRYP PFNGLMULTITEXCOORDP4UIVPROC) (GLenum texture, GLenum type, const GLuint *coords);
        //typedef void (APIENTRYP PFNGLNORMALP3UIPROC) (GLenum type, GLuint coords);
        //typedef void (APIENTRYP PFNGLNORMALP3UIVPROC) (GLenum type, const GLuint *coords);
        //typedef void (APIENTRYP PFNGLCOLORP3UIPROC) (GLenum type, GLuint color);
        //typedef void (APIENTRYP PFNGLCOLORP3UIVPROC) (GLenum type, const GLuint *color);
        //typedef void (APIENTRYP PFNGLCOLORP4UIPROC) (GLenum type, GLuint color);
        //typedef void (APIENTRYP PFNGLCOLORP4UIVPROC) (GLenum type, const GLuint *color);
        //typedef void (APIENTRYP PFNGLSECONDARYCOLORP3UIPROC) (GLenum type, GLuint color);
        //typedef void (APIENTRYP PFNGLSECONDARYCOLORP3UIVPROC) (GLenum type, const GLuint *color);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBP1UIPROC) (GLuint index, GLenum type, GLboolean normalized, GLuint value);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBP1UIVPROC) (GLuint index, GLenum type, GLboolean normalized, const GLuint *value);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBP2UIPROC) (GLuint index, GLenum type, GLboolean normalized, GLuint value);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBP2UIVPROC) (GLuint index, GLenum type, GLboolean normalized, const GLuint *value);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBP3UIPROC) (GLuint index, GLenum type, GLboolean normalized, GLuint value);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBP3UIVPROC) (GLuint index, GLenum type, GLboolean normalized, const GLuint *value);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBP4UIPROC) (GLuint index, GLenum type, GLboolean normalized, GLuint value);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBP4UIVPROC) (GLuint index, GLenum type, GLboolean normalized, const GLuint *value);
        #endregion

        #region ARB_draw_indirect

        //public const uint ARB_draw_indirect 1
        #region GLCOREARB_PROTOTYPES

        //public static void glDrawArraysIndirect (GLenum mode, const GLvoid *indirect);
        //public static void glDrawElementsIndirect (GLenum mode, GLenum type, const GLvoid *indirect);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLDRAWARRAYSINDIRECTPROC) (GLenum mode, const GLvoid *indirect);
        //typedef void (APIENTRYP PFNGLDRAWELEMENTSINDIRECTPROC) (GLenum mode, GLenum type, const GLvoid *indirect);
        #endregion

        #region ARB_gpu_shader5

        //public const uint ARB_gpu_shader5 1
        #endregion

        #region ARB_gpu_shader_fp64

        //public const uint ARB_gpu_shader_fp64 1
        #region GLCOREARB_PROTOTYPES

        //public static void glUniform1d (GLint location, GLdouble x);
        //public static void glUniform2d (GLint location, GLdouble x, GLdouble y);
        //public static void glUniform3d (GLint location, GLdouble x, GLdouble y, GLdouble z);
        //public static void glUniform4d (GLint location, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
        //public static void glUniform1dv (GLint location, GLsizei count, const GLdouble *value);
        //public static void glUniform2dv (GLint location, GLsizei count, const GLdouble *value);
        //public static void glUniform3dv (GLint location, GLsizei count, const GLdouble *value);
        //public static void glUniform4dv (GLint location, GLsizei count, const GLdouble *value);
        //public static void glUniformMatrix2dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glUniformMatrix3dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glUniformMatrix4dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glUniformMatrix2x3dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glUniformMatrix2x4dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glUniformMatrix3x2dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glUniformMatrix3x4dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glUniformMatrix4x2dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glUniformMatrix4x3dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glGetUniformdv (GLuint program, GLint location, GLdouble *parameters);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLUNIFORM1DPROC) (GLint location, GLdouble x);
        //typedef void (APIENTRYP PFNGLUNIFORM2DPROC) (GLint location, GLdouble x, GLdouble y);
        //typedef void (APIENTRYP PFNGLUNIFORM3DPROC) (GLint location, GLdouble x, GLdouble y, GLdouble z);
        //typedef void (APIENTRYP PFNGLUNIFORM4DPROC) (GLint location, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
        //typedef void (APIENTRYP PFNGLUNIFORM1DVPROC) (GLint location, GLsizei count, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLUNIFORM2DVPROC) (GLint location, GLsizei count, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLUNIFORM3DVPROC) (GLint location, GLsizei count, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLUNIFORM4DVPROC) (GLint location, GLsizei count, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLUNIFORMMATRIX2DVPROC) (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLUNIFORMMATRIX3DVPROC) (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLUNIFORMMATRIX4DVPROC) (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLUNIFORMMATRIX2X3DVPROC) (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLUNIFORMMATRIX2X4DVPROC) (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLUNIFORMMATRIX3X2DVPROC) (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLUNIFORMMATRIX3X4DVPROC) (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLUNIFORMMATRIX4X2DVPROC) (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLUNIFORMMATRIX4X3DVPROC) (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLGETUNIFORMDVPROC) (GLuint program, GLint location, GLdouble *parameters);
        #endregion

        #region ARB_shader_subroutine

        //public const uint ARB_shader_subroutine 1
        #region GLCOREARB_PROTOTYPES

        //public static GLint glGetSubroutineUniformLocation (GLuint program, GLenum shadertype, const GLchar *name);
        //public static GLuint glGetSubroutineIndex (GLuint program, GLenum shadertype, const GLchar *name);
        //public static void glGetActiveSubroutineUniformiv (GLuint program, GLenum shadertype, GLuint index, GLenum pname, GLint *values);
        //public static void glGetActiveSubroutineUniformName (GLuint program, GLenum shadertype, GLuint index, GLsizei bufsize, GLsizei *length, GLchar *name);
        //public static void glGetActiveSubroutineName (GLuint program, GLenum shadertype, GLuint index, GLsizei bufsize, GLsizei *length, GLchar *name);
        //public static void glUniformSubroutinesuiv (GLenum shadertype, GLsizei count, const GLuint *indices);
        //public static void glGetUniformSubroutineuiv (GLenum shadertype, GLint location, GLuint *parameters);
        //public static void glGetProgramStageiv (GLuint program, GLenum shadertype, GLenum pname, GLint *values);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef GLint (APIENTRYP PFNGLGETSUBROUTINEUNIFORMLOCATIONPROC) (GLuint program, GLenum shadertype, const GLchar *name);
        //typedef GLuint (APIENTRYP PFNGLGETSUBROUTINEINDEXPROC) (GLuint program, GLenum shadertype, const GLchar *name);
        //typedef void (APIENTRYP PFNGLGETACTIVESUBROUTINEUNIFORMIVPROC) (GLuint program, GLenum shadertype, GLuint index, GLenum pname, GLint *values);
        //typedef void (APIENTRYP PFNGLGETACTIVESUBROUTINEUNIFORMNAMEPROC) (GLuint program, GLenum shadertype, GLuint index, GLsizei bufsize, GLsizei *length, GLchar *name);
        //typedef void (APIENTRYP PFNGLGETACTIVESUBROUTINENAMEPROC) (GLuint program, GLenum shadertype, GLuint index, GLsizei bufsize, GLsizei *length, GLchar *name);
        //typedef void (APIENTRYP PFNGLUNIFORMSUBROUTINESUIVPROC) (GLenum shadertype, GLsizei count, const GLuint *indices);
        //typedef void (APIENTRYP PFNGLGETUNIFORMSUBROUTINEUIVPROC) (GLenum shadertype, GLint location, GLuint *parameters);
        //typedef void (APIENTRYP PFNGLGETPROGRAMSTAGEIVPROC) (GLuint program, GLenum shadertype, GLenum pname, GLint *values);
        #endregion

        #region ARB_tessellation_shader

        //public const uint ARB_tessellation_shader 1
        #region GLCOREARB_PROTOTYPES

        //public static void glPatchParameteri (GLenum pname, GLint value);
        //public static void glPatchParameterfv (GLenum pname, const GLfloat *values);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLPATCHPARAMETERIPROC) (GLenum pname, GLint value);
        //typedef void (APIENTRYP PFNGLPATCHPARAMETERFVPROC) (GLenum pname, const GLfloat *values);
        #endregion

        #region ARB_texture_buffer_object_rgb32

        //public const uint ARB_texture_buffer_object_rgb32 1
        #endregion

        #region ARB_transform_feedback2

        //public const uint ARB_transform_feedback2 1
        #region GLCOREARB_PROTOTYPES

        //public static void glBindTransformFeedback (GLenum target, GLuint id);
        //public static void glDeleteTransformFeedbacks (GLsizei n, const GLuint *ids);
        //public static void glGenTransformFeedbacks (GLsizei n, GLuint *ids);
        //public static GLboolean glIsTransformFeedback (GLuint id);
        //public static void glPauseTransformFeedback (void);
        //public static void glResumeTransformFeedback (void);
        //public static void glDrawTransformFeedback (GLenum mode, GLuint id);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLBINDTRANSFORMFEEDBACKPROC) (GLenum target, GLuint id);
        //typedef void (APIENTRYP PFNGLDELETETRANSFORMFEEDBACKSPROC) (GLsizei n, const GLuint *ids);
        //typedef void (APIENTRYP PFNGLGENTRANSFORMFEEDBACKSPROC) (GLsizei n, GLuint *ids);
        //typedef GLboolean (APIENTRYP PFNGLISTRANSFORMFEEDBACKPROC) (GLuint id);
        //typedef void (APIENTRYP PFNGLPAUSETRANSFORMFEEDBACKPROC) (void);
        //typedef void (APIENTRYP PFNGLRESUMETRANSFORMFEEDBACKPROC) (void);
        //typedef void (APIENTRYP PFNGLDRAWTRANSFORMFEEDBACKPROC) (GLenum mode, GLuint id);
        #endregion

        #region ARB_transform_feedback3

        //public const uint ARB_transform_feedback3 1
        #region GLCOREARB_PROTOTYPES

        //public static void glDrawTransformFeedbackStream (GLenum mode, GLuint id, GLuint stream);
        //public static void glBeginQueryIndexed (GLenum target, GLuint index, GLuint id);
        //public static void glEndQueryIndexed (GLenum target, GLuint index);
        //public static void glGetQueryIndexediv (GLenum target, GLuint index, GLenum pname, GLint *parameters);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLDRAWTRANSFORMFEEDBACKSTREAMPROC) (GLenum mode, GLuint id, GLuint stream);
        //typedef void (APIENTRYP PFNGLBEGINQUERYINDEXEDPROC) (GLenum target, GLuint index, GLuint id);
        //typedef void (APIENTRYP PFNGLENDQUERYINDEXEDPROC) (GLenum target, GLuint index);
        //typedef void (APIENTRYP PFNGLGETQUERYINDEXEDIVPROC) (GLenum target, GLuint index, GLenum pname, GLint *parameters);
        #endregion

        #region ARB_ES2_compatibility

        //public const uint ARB_ES2_compatibility 1
        #region GLCOREARB_PROTOTYPES

        //public static void glReleaseShaderCompiler (void);
        //public static void glShaderBinary (GLsizei count, const GLuint *shaders, GLenum binaryformat, const GLvoid *binary, GLsizei length);
        //public static void glGetShaderPrecisionFormat (GLenum shadertype, GLenum precisiontype, GLint *range, GLint *precision);
        //public static void glDepthRangef (GLfloat n, GLfloat f);
        //public static void glClearDepthf (GLfloat d);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLRELEASESHADERCOMPILERPROC) (void);
        //typedef void (APIENTRYP PFNGLSHADERBINARYPROC) (GLsizei count, const GLuint *shaders, GLenum binaryformat, const GLvoid *binary, GLsizei length);
        //typedef void (APIENTRYP PFNGLGETSHADERPRECISIONFORMATPROC) (GLenum shadertype, GLenum precisiontype, GLint *range, GLint *precision);
        //typedef void (APIENTRYP PFNGLDEPTHRANGEFPROC) (GLfloat n, GLfloat f);
        //typedef void (APIENTRYP PFNGLCLEARDEPTHFPROC) (GLfloat d);
        #endregion

        #region ARB_get_program_binary

        //public const uint ARB_get_program_binary 1
        #region GLCOREARB_PROTOTYPES

        //public static void glGetProgramBinary (GLuint program, GLsizei bufSize, GLsizei *length, GLenum *binaryFormat, GLvoid *binary);
        //public static void glProgramBinary (GLuint program, GLenum binaryFormat, const GLvoid *binary, GLsizei length);
        //public static void glProgramParameteri (GLuint program, GLenum pname, GLint value);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLGETPROGRAMBINARYPROC) (GLuint program, GLsizei bufSize, GLsizei *length, GLenum *binaryFormat, GLvoid *binary);
        //typedef void (APIENTRYP PFNGLPROGRAMBINARYPROC) (GLuint program, GLenum binaryFormat, const GLvoid *binary, GLsizei length);
        //typedef void (APIENTRYP PFNGLPROGRAMPARAMETERIPROC) (GLuint program, GLenum pname, GLint value);
        #endregion

        #region ARB_separate_shader_objects

        //public const uint ARB_separate_shader_objects 1
        #region GLCOREARB_PROTOTYPES

        //public static void glUseProgramStages (GLuint pipeline, GLbitfield stages, GLuint program);
        //public static void glActiveShaderProgram (GLuint pipeline, GLuint program);
        //public static GLuint glCreateShaderProgramv (GLenum type, GLsizei count, const GLchar* const *strings);
        //public static void glBindProgramPipeline (GLuint pipeline);
        //public static void glDeleteProgramPipelines (GLsizei n, const GLuint *pipelines);
        //public static void glGenProgramPipelines (GLsizei n, GLuint *pipelines);
        //public static GLboolean glIsProgramPipeline (GLuint pipeline);
        //public static void glGetProgramPipelineiv (GLuint pipeline, GLenum pname, GLint *parameters);
        //public static void glProgramUniform1i (GLuint program, GLint location, GLint v0);
        //public static void glProgramUniform1iv (GLuint program, GLint location, GLsizei count, const GLint *value);
        //public static void glProgramUniform1f (GLuint program, GLint location, GLfloat v0);
        //public static void glProgramUniform1fv (GLuint program, GLint location, GLsizei count, const GLfloat *value);
        //public static void glProgramUniform1d (GLuint program, GLint location, GLdouble v0);
        //public static void glProgramUniform1dv (GLuint program, GLint location, GLsizei count, const GLdouble *value);
        //public static void glProgramUniform1ui (GLuint program, GLint location, GLuint v0);
        //public static void glProgramUniform1uiv (GLuint program, GLint location, GLsizei count, const GLuint *value);
        //public static void glProgramUniform2i (GLuint program, GLint location, GLint v0, GLint v1);
        //public static void glProgramUniform2iv (GLuint program, GLint location, GLsizei count, const GLint *value);
        //public static void glProgramUniform2f (GLuint program, GLint location, GLfloat v0, GLfloat v1);
        //public static void glProgramUniform2fv (GLuint program, GLint location, GLsizei count, const GLfloat *value);
        //public static void glProgramUniform2d (GLuint program, GLint location, GLdouble v0, GLdouble v1);
        //public static void glProgramUniform2dv (GLuint program, GLint location, GLsizei count, const GLdouble *value);
        //public static void glProgramUniform2ui (GLuint program, GLint location, GLuint v0, GLuint v1);
        //public static void glProgramUniform2uiv (GLuint program, GLint location, GLsizei count, const GLuint *value);
        //public static void glProgramUniform3i (GLuint program, GLint location, GLint v0, GLint v1, GLint v2);
        //public static void glProgramUniform3iv (GLuint program, GLint location, GLsizei count, const GLint *value);
        //public static void glProgramUniform3f (GLuint program, GLint location, GLfloat v0, GLfloat v1, GLfloat v2);
        //public static void glProgramUniform3fv (GLuint program, GLint location, GLsizei count, const GLfloat *value);
        //public static void glProgramUniform3d (GLuint program, GLint location, GLdouble v0, GLdouble v1, GLdouble v2);
        //public static void glProgramUniform3dv (GLuint program, GLint location, GLsizei count, const GLdouble *value);
        //public static void glProgramUniform3ui (GLuint program, GLint location, GLuint v0, GLuint v1, GLuint v2);
        //public static void glProgramUniform3uiv (GLuint program, GLint location, GLsizei count, const GLuint *value);
        //public static void glProgramUniform4i (GLuint program, GLint location, GLint v0, GLint v1, GLint v2, GLint v3);
        //public static void glProgramUniform4iv (GLuint program, GLint location, GLsizei count, const GLint *value);
        //public static void glProgramUniform4f (GLuint program, GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);
        //public static void glProgramUniform4fv (GLuint program, GLint location, GLsizei count, const GLfloat *value);
        //public static void glProgramUniform4d (GLuint program, GLint location, GLdouble v0, GLdouble v1, GLdouble v2, GLdouble v3);
        //public static void glProgramUniform4dv (GLuint program, GLint location, GLsizei count, const GLdouble *value);
        //public static void glProgramUniform4ui (GLuint program, GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);
        //public static void glProgramUniform4uiv (GLuint program, GLint location, GLsizei count, const GLuint *value);
        //public static void glProgramUniformMatrix2fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //public static void glProgramUniformMatrix3fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //public static void glProgramUniformMatrix4fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //public static void glProgramUniformMatrix2dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glProgramUniformMatrix3dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glProgramUniformMatrix4dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glProgramUniformMatrix2x3fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //public static void glProgramUniformMatrix3x2fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //public static void glProgramUniformMatrix2x4fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //public static void glProgramUniformMatrix4x2fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //public static void glProgramUniformMatrix3x4fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //public static void glProgramUniformMatrix4x3fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //public static void glProgramUniformMatrix2x3dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glProgramUniformMatrix3x2dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glProgramUniformMatrix2x4dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glProgramUniformMatrix4x2dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glProgramUniformMatrix3x4dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glProgramUniformMatrix4x3dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //public static void glValidateProgramPipeline (GLuint pipeline);
        //public static void glGetProgramPipelineInfoLog (GLuint pipeline, GLsizei bufSize, GLsizei *length, GLchar *infoLog);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLUSEPROGRAMSTAGESPROC) (GLuint pipeline, GLbitfield stages, GLuint program);
        //typedef void (APIENTRYP PFNGLACTIVESHADERPROGRAMPROC) (GLuint pipeline, GLuint program);
        //typedef GLuint (APIENTRYP PFNGLCREATESHADERPROGRAMVPROC) (GLenum type, GLsizei count, const GLchar* const *strings);
        //typedef void (APIENTRYP PFNGLBINDPROGRAMPIPELINEPROC) (GLuint pipeline);
        //typedef void (APIENTRYP PFNGLDELETEPROGRAMPIPELINESPROC) (GLsizei n, const GLuint *pipelines);
        //typedef void (APIENTRYP PFNGLGENPROGRAMPIPELINESPROC) (GLsizei n, GLuint *pipelines);
        //typedef GLboolean (APIENTRYP PFNGLISPROGRAMPIPELINEPROC) (GLuint pipeline);
        //typedef void (APIENTRYP PFNGLGETPROGRAMPIPELINEIVPROC) (GLuint pipeline, GLenum pname, GLint *parameters);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM1IPROC) (GLuint program, GLint location, GLint v0);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM1IVPROC) (GLuint program, GLint location, GLsizei count, const GLint *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM1FPROC) (GLuint program, GLint location, GLfloat v0);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM1FVPROC) (GLuint program, GLint location, GLsizei count, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM1DPROC) (GLuint program, GLint location, GLdouble v0);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM1DVPROC) (GLuint program, GLint location, GLsizei count, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM1UIPROC) (GLuint program, GLint location, GLuint v0);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM1UIVPROC) (GLuint program, GLint location, GLsizei count, const GLuint *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM2IPROC) (GLuint program, GLint location, GLint v0, GLint v1);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM2IVPROC) (GLuint program, GLint location, GLsizei count, const GLint *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM2FPROC) (GLuint program, GLint location, GLfloat v0, GLfloat v1);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM2FVPROC) (GLuint program, GLint location, GLsizei count, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM2DPROC) (GLuint program, GLint location, GLdouble v0, GLdouble v1);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM2DVPROC) (GLuint program, GLint location, GLsizei count, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM2UIPROC) (GLuint program, GLint location, GLuint v0, GLuint v1);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM2UIVPROC) (GLuint program, GLint location, GLsizei count, const GLuint *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM3IPROC) (GLuint program, GLint location, GLint v0, GLint v1, GLint v2);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM3IVPROC) (GLuint program, GLint location, GLsizei count, const GLint *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM3FPROC) (GLuint program, GLint location, GLfloat v0, GLfloat v1, GLfloat v2);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM3FVPROC) (GLuint program, GLint location, GLsizei count, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM3DPROC) (GLuint program, GLint location, GLdouble v0, GLdouble v1, GLdouble v2);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM3DVPROC) (GLuint program, GLint location, GLsizei count, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM3UIPROC) (GLuint program, GLint location, GLuint v0, GLuint v1, GLuint v2);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM3UIVPROC) (GLuint program, GLint location, GLsizei count, const GLuint *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM4IPROC) (GLuint program, GLint location, GLint v0, GLint v1, GLint v2, GLint v3);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM4IVPROC) (GLuint program, GLint location, GLsizei count, const GLint *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM4FPROC) (GLuint program, GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM4FVPROC) (GLuint program, GLint location, GLsizei count, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM4DPROC) (GLuint program, GLint location, GLdouble v0, GLdouble v1, GLdouble v2, GLdouble v3);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM4DVPROC) (GLuint program, GLint location, GLsizei count, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM4UIPROC) (GLuint program, GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORM4UIVPROC) (GLuint program, GLint location, GLsizei count, const GLuint *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX2FVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX3FVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX4FVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX2DVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX3DVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX4DVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX2X3FVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX3X2FVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX2X4FVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX4X2FVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX3X4FVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX4X3FVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX2X3DVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX3X2DVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX2X4DVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX4X2DVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX3X4DVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLPROGRAMUNIFORMMATRIX4X3DVPROC) (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
        //typedef void (APIENTRYP PFNGLVALIDATEPROGRAMPIPELINEPROC) (GLuint pipeline);
        //typedef void (APIENTRYP PFNGLGETPROGRAMPIPELINEINFOLOGPROC) (GLuint pipeline, GLsizei bufSize, GLsizei *length, GLchar *infoLog);
        #endregion

        #region ARB_vertex_attrib_64bit

        //public const uint ARB_vertex_attrib_64bit 1
        #region GLCOREARB_PROTOTYPES

        //public static void glVertexAttribL1d (GLuint index, GLdouble x);
        //public static void glVertexAttribL2d (GLuint index, GLdouble x, GLdouble y);
        //public static void glVertexAttribL3d (GLuint index, GLdouble x, GLdouble y, GLdouble z);
        //public static void glVertexAttribL4d (GLuint index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
        //public static void glVertexAttribL1dv (GLuint index, const GLdouble *v);
        //public static void glVertexAttribL2dv (GLuint index, const GLdouble *v);
        //public static void glVertexAttribL3dv (GLuint index, const GLdouble *v);
        //public static void glVertexAttribL4dv (GLuint index, const GLdouble *v);
        //public static void glVertexAttribLPointer (GLuint index, GLint size, GLenum type, GLsizei stride, const GLvoid *pointer);
        //public static void glGetVertexAttribLdv (GLuint index, GLenum pname, GLdouble *parameters);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBL1DPROC) (GLuint index, GLdouble x);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBL2DPROC) (GLuint index, GLdouble x, GLdouble y);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBL3DPROC) (GLuint index, GLdouble x, GLdouble y, GLdouble z);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBL4DPROC) (GLuint index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBL1DVPROC) (GLuint index, const GLdouble *v);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBL2DVPROC) (GLuint index, const GLdouble *v);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBL3DVPROC) (GLuint index, const GLdouble *v);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBL4DVPROC) (GLuint index, const GLdouble *v);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBLPOINTERPROC) (GLuint index, GLint size, GLenum type, GLsizei stride, const GLvoid *pointer);
        //typedef void (APIENTRYP PFNGLGETVERTEXATTRIBLDVPROC) (GLuint index, GLenum pname, GLdouble *parameters);
        #endregion

        #region ARB_viewport_array

        //public const uint ARB_viewport_array 1
        #region GLCOREARB_PROTOTYPES

        //public static void glViewportArrayv (GLuint first, GLsizei count, const GLfloat *v);
        //public static void glViewportIndexedf (GLuint index, GLfloat x, GLfloat y, GLfloat w, GLfloat h);
        //public static void glViewportIndexedfv (GLuint index, const GLfloat *v);
        //public static void glScissorArrayv (GLuint first, GLsizei count, const GLint *v);
        //public static void glScissorIndexed (GLuint index, GLint left, GLint bottom, GLsizei width, GLsizei height);
        //public static void glScissorIndexedv (GLuint index, const GLint *v);
        //public static void glDepthRangeArrayv (GLuint first, GLsizei count, const GLdouble *v);
        //public static void glDepthRangeIndexed (GLuint index, GLdouble n, GLdouble f);
        //public static void glGetFloati_v (GLenum target, GLuint index, GLfloat *data);
        //public static void glGetDoublei_v (GLenum target, GLuint index, GLdouble *data);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLVIEWPORTARRAYVPROC) (GLuint first, GLsizei count, const GLfloat *v);
        //typedef void (APIENTRYP PFNGLVIEWPORTINDEXEDFPROC) (GLuint index, GLfloat x, GLfloat y, GLfloat w, GLfloat h);
        //typedef void (APIENTRYP PFNGLVIEWPORTINDEXEDFVPROC) (GLuint index, const GLfloat *v);
        //typedef void (APIENTRYP PFNGLSCISSORARRAYVPROC) (GLuint first, GLsizei count, const GLint *v);
        //typedef void (APIENTRYP PFNGLSCISSORINDEXEDPROC) (GLuint index, GLint left, GLint bottom, GLsizei width, GLsizei height);
        //typedef void (APIENTRYP PFNGLSCISSORINDEXEDVPROC) (GLuint index, const GLint *v);
        //typedef void (APIENTRYP PFNGLDEPTHRANGEARRAYVPROC) (GLuint first, GLsizei count, const GLdouble *v);
        //typedef void (APIENTRYP PFNGLDEPTHRANGEINDEXEDPROC) (GLuint index, GLdouble n, GLdouble f);
        //typedef void (APIENTRYP PFNGLGETFLOATI_VPROC) (GLenum target, GLuint index, GLfloat *data);
        //typedef void (APIENTRYP PFNGLGETDOUBLEI_VPROC) (GLenum target, GLuint index, GLdouble *data);
        #endregion

        #region ARB_cl_event

        //public const uint ARB_cl_event 1
        #region GLCOREARB_PROTOTYPES

        //public static GLsync glCreateSyncFromCLeventARB (struct _cl_context * context, struct _cl_event * event, GLbitfield flags);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef GLsync (APIENTRYP PFNGLCREATESYNCFROMCLEVENTARBPROC) (struct _cl_context * context, struct _cl_event * event, GLbitfield flags);
        #endregion

        #region ARB_debug_output

        //public const uint ARB_debug_output 1
        #region GLCOREARB_PROTOTYPES

        //public static void glDebugMessageControlARB (GLenum source, GLenum type, GLenum severity, GLsizei count, const GLuint *ids, GLboolean enabled);
        //public static void glDebugMessageInsertARB (GLenum source, GLenum type, GLuint id, GLenum severity, GLsizei length, const GLchar *buf);
        //public static void glDebugMessageCallbackARB (GLDEBUGPROCARB callback, const GLvoid *userparameter);
        //public static GLuint glGetDebugMessageLogARB (GLuint count, GLsizei bufsize, GLenum *sources, GLenum *types, GLuint *ids, GLenum *severities, GLsizei *lengths, GLchar *messageLog);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLDEBUGMESSAGECONTROLARBPROC) (GLenum source, GLenum type, GLenum severity, GLsizei count, const GLuint *ids, GLboolean enabled);
        //typedef void (APIENTRYP PFNGLDEBUGMESSAGEINSERTARBPROC) (GLenum source, GLenum type, GLuint id, GLenum severity, GLsizei length, const GLchar *buf);
        //typedef void (APIENTRYP PFNGLDEBUGMESSAGECALLBACKARBPROC) (GLDEBUGPROCARB callback, const GLvoid *userparameter);
        //typedef GLuint (APIENTRYP PFNGLGETDEBUGMESSAGELOGARBPROC) (GLuint count, GLsizei bufsize, GLenum *sources, GLenum *types, GLuint *ids, GLenum *severities, GLsizei *lengths, GLchar *messageLog);
        #endregion

        #region ARB_robustness

        //public const uint ARB_robustness 1
        #region GLCOREARB_PROTOTYPES

        //public static GLenum glGetGraphicsResetStatusARB (void);
        //public static void glGetnTexImageARB (GLenum target, GLint level, GLenum format, GLenum type, GLsizei bufSize, GLvoid *img);
        //public static void glReadnPixelsARB (GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, GLsizei bufSize, GLvoid *data);
        //public static void glGetnCompressedTexImageARB (GLenum target, GLint lod, GLsizei bufSize, GLvoid *img);
        //public static void glGetnUniformfvARB (GLuint program, GLint location, GLsizei bufSize, GLfloat *parameters);
        //public static void glGetnUniformivARB (GLuint program, GLint location, GLsizei bufSize, GLint *parameters);
        //public static void glGetnUniformuivARB (GLuint program, GLint location, GLsizei bufSize, GLuint *parameters);
        //public static void glGetnUniformdvARB (GLuint program, GLint location, GLsizei bufSize, GLdouble *parameters);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef GLenum (APIENTRYP PFNGLGETGRAPHICSRESETSTATUSARBPROC) (void);
        //typedef void (APIENTRYP PFNGLGETNTEXIMAGEARBPROC) (GLenum target, GLint level, GLenum format, GLenum type, GLsizei bufSize, GLvoid *img);
        //typedef void (APIENTRYP PFNGLREADNPIXELSARBPROC) (GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, GLsizei bufSize, GLvoid *data);
        //typedef void (APIENTRYP PFNGLGETNCOMPRESSEDTEXIMAGEARBPROC) (GLenum target, GLint lod, GLsizei bufSize, GLvoid *img);
        //typedef void (APIENTRYP PFNGLGETNUNIFORMFVARBPROC) (GLuint program, GLint location, GLsizei bufSize, GLfloat *parameters);
        //typedef void (APIENTRYP PFNGLGETNUNIFORMIVARBPROC) (GLuint program, GLint location, GLsizei bufSize, GLint *parameters);
        //typedef void (APIENTRYP PFNGLGETNUNIFORMUIVARBPROC) (GLuint program, GLint location, GLsizei bufSize, GLuint *parameters);
        //typedef void (APIENTRYP PFNGLGETNUNIFORMDVARBPROC) (GLuint program, GLint location, GLsizei bufSize, GLdouble *parameters);
        #endregion

        #region ARB_shader_stencil_export

        //public const uint ARB_shader_stencil_export 1
        #endregion

        #region ARB_base_instance

        //public const uint ARB_base_instance 1
        #region GLCOREARB_PROTOTYPES

        //public static void glDrawArraysInstancedBaseInstance (GLenum mode, GLint first, GLsizei count, GLsizei instancecount, GLuint baseinstance);
        //public static void glDrawElementsInstancedBaseInstance (GLenum mode, GLsizei count, GLenum type, const void *indices, GLsizei instancecount, GLuint baseinstance);
        //public static void glDrawElementsInstancedBaseVertexBaseInstance (GLenum mode, GLsizei count, GLenum type, const void *indices, GLsizei instancecount, GLint basevertex, GLuint baseinstance);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLDRAWARRAYSINSTANCEDBASEINSTANCEPROC) (GLenum mode, GLint first, GLsizei count, GLsizei instancecount, GLuint baseinstance);
        //typedef void (APIENTRYP PFNGLDRAWELEMENTSINSTANCEDBASEINSTANCEPROC) (GLenum mode, GLsizei count, GLenum type, const void *indices, GLsizei instancecount, GLuint baseinstance);
        //typedef void (APIENTRYP PFNGLDRAWELEMENTSINSTANCEDBASEVERTEXBASEINSTANCEPROC) (GLenum mode, GLsizei count, GLenum type, const void *indices, GLsizei instancecount, GLint basevertex, GLuint baseinstance);
        #endregion

        #region ARB_shading_language_420pack

        //public const uint ARB_shading_language_420pack 1
        #endregion

        #region ARB_transform_feedback_instanced

        //public const uint ARB_transform_feedback_instanced 1
        #region GLCOREARB_PROTOTYPES

        //public static void glDrawTransformFeedbackInstanced (GLenum mode, GLuint id, GLsizei instancecount);
        //public static void glDrawTransformFeedbackStreamInstanced (GLenum mode, GLuint id, GLuint stream, GLsizei instancecount);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLDRAWTRANSFORMFEEDBACKINSTANCEDPROC) (GLenum mode, GLuint id, GLsizei instancecount);
        //typedef void (APIENTRYP PFNGLDRAWTRANSFORMFEEDBACKSTREAMINSTANCEDPROC) (GLenum mode, GLuint id, GLuint stream, GLsizei instancecount);
        #endregion

        #region ARB_compressed_texture_pixel_storage

        //public const uint ARB_compressed_texture_pixel_storage 1
        #endregion

        #region ARB_conservative_depth

        //public const uint ARB_conservative_depth 1
        #endregion

        #region ARB_internalformat_query

        //public const uint ARB_internalformat_query 1
        #region GLCOREARB_PROTOTYPES

        //public static void glGetInternalformativ (GLenum target, GLenum internalformat, GLenum pname, GLsizei bufSize, GLint *parameters);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLGETINTERNALFORMATIVPROC) (GLenum target, GLenum internalformat, GLenum pname, GLsizei bufSize, GLint *parameters);
        #endregion

        #region ARB_map_buffer_alignment

        //public const uint ARB_map_buffer_alignment 1
        #endregion

        #region ARB_shader_atomic_counters

        //public const uint ARB_shader_atomic_counters 1
        #region GLCOREARB_PROTOTYPES

        //public static void glGetActiveAtomicCounterBufferiv (GLuint program, GLuint bufferIndex, GLenum pname, GLint *parameters);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLGETACTIVEATOMICCOUNTERBUFFERIVPROC) (GLuint program, GLuint bufferIndex, GLenum pname, GLint *parameters);
        #endregion

        #region ARB_shader_image_load_store

        //public const uint ARB_shader_image_load_store 1
        #region GLCOREARB_PROTOTYPES

        //public static void glBindImageTexture (GLuint unit, GLuint texture, GLint level, GLboolean layered, GLint layer, GLenum access, GLenum format);
        //public static void glMemoryBarrier (GLbitfield barriers);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLBINDIMAGETEXTUREPROC) (GLuint unit, GLuint texture, GLint level, GLboolean layered, GLint layer, GLenum access, GLenum format);
        //typedef void (APIENTRYP PFNGLMEMORYBARRIERPROC) (GLbitfield barriers);
        #endregion

        #region ARB_shading_language_packing

        //public const uint ARB_shading_language_packing 1
        #endregion

        #region ARB_texture_storage

        //public const uint ARB_texture_storage 1
        #region GLCOREARB_PROTOTYPES

        //public static void glTexStorage1D (GLenum target, GLsizei levels, GLenum internalformat, GLsizei width);
        //public static void glTexStorage2D (GLenum target, GLsizei levels, GLenum internalformat, GLsizei width, GLsizei height);
        //public static void glTexStorage3D (GLenum target, GLsizei levels, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth);
        //public static void glTextureStorage1DEXT (GLuint texture, GLenum target, GLsizei levels, GLenum internalformat, GLsizei width);
        //public static void glTextureStorage2DEXT (GLuint texture, GLenum target, GLsizei levels, GLenum internalformat, GLsizei width, GLsizei height);
        //public static void glTextureStorage3DEXT (GLuint texture, GLenum target, GLsizei levels, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLTEXSTORAGE1DPROC) (GLenum target, GLsizei levels, GLenum internalformat, GLsizei width);
        //typedef void (APIENTRYP PFNGLTEXSTORAGE2DPROC) (GLenum target, GLsizei levels, GLenum internalformat, GLsizei width, GLsizei height);
        //typedef void (APIENTRYP PFNGLTEXSTORAGE3DPROC) (GLenum target, GLsizei levels, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth);
        //typedef void (APIENTRYP PFNGLTEXTURESTORAGE1DEXTPROC) (GLuint texture, GLenum target, GLsizei levels, GLenum internalformat, GLsizei width);
        //typedef void (APIENTRYP PFNGLTEXTURESTORAGE2DEXTPROC) (GLuint texture, GLenum target, GLsizei levels, GLenum internalformat, GLsizei width, GLsizei height);
        //typedef void (APIENTRYP PFNGLTEXTURESTORAGE3DEXTPROC) (GLuint texture, GLenum target, GLsizei levels, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth);
        #endregion

        #region KHR_texture_compression_astc_ldr

        //public const uint KHR_texture_compression_astc_ldr 1
        #endregion

        #region KHR_debug
        //public static void glDebugMessageControl (GLenum source, GLenum type, GLenum severity, GLsizei count, const GLuint *ids, GLboolean enabled);
        //public static void glDebugMessageInsert (GLenum source, GLenum type, GLuint id, GLenum severity, GLsizei length, const GLchar *buf);
        //public static void glDebugMessageCallback (GLDEBUGPROC callback, const void *userparameter);
        //public static GLuint glGetDebugMessageLog (GLuint count, GLsizei bufsize, GLenum *sources, GLenum *types, GLuint *ids, GLenum *severities, GLsizei *lengths, GLchar *messageLog);
        //public static void glPushDebugGroup (GLenum source, GLuint id, GLsizei length, const GLchar *message);
        //public static void glPopDebugGroup (void);

        private IntPtr glObjectLabel;
        public static void ObjectLabel(uint identifier, uint name, int length, byte* label)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetObjectLabel;
        public static void GetObjectLabel(uint identifier, uint name, int bufSize, int* length, byte* label)
        {
            throw new NotImplementedException();
        }

        private IntPtr glObjectPtrLabel;
        public static void ObjectPtrLabel(void* ptr, int length, byte* label)
        {
            throw new NotImplementedException();
        }

        private IntPtr glGetObjectPtrLabel;
        public static void GetObjectPtrLabel(void* ptr, int bufSize, int* length, byte* label)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ARB_arrays_of_arrays

        //public const uint ARB_arrays_of_arrays 1
        #endregion

        #region ARB_clear_buffer_object

        //public const uint ARB_clear_buffer_object 1
        #region GLCOREARB_PROTOTYPES

        //public static void glClearBufferData (GLenum target, GLenum internalformat, GLenum format, GLenum type, const void *data);
        //public static void glClearBufferSubData (GLenum target, GLenum internalformat, GLintptr offset, GLsizeiptr size, GLenum format, GLenum type, const void *data);
        //public static void glClearNamedBufferDataEXT (GLuint buffer, GLenum internalformat, GLenum format, GLenum type, const void *data);
        //public static void glClearNamedBufferSubDataEXT (GLuint buffer, GLenum internalformat, GLenum format, GLenum type, GLsizeiptr offset, GLsizeiptr size, const void *data);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLCLEARBUFFERDATAPROC) (GLenum target, GLenum internalformat, GLenum format, GLenum type, const void *data);
        //typedef void (APIENTRYP PFNGLCLEARBUFFERSUBDATAPROC) (GLenum target, GLenum internalformat, GLintptr offset, GLsizeiptr size, GLenum format, GLenum type, const void *data);
        //typedef void (APIENTRYP PFNGLCLEARNAMEDBUFFERDATAEXTPROC) (GLuint buffer, GLenum internalformat, GLenum format, GLenum type, const void *data);
        //typedef void (APIENTRYP PFNGLCLEARNAMEDBUFFERSUBDATAEXTPROC) (GLuint buffer, GLenum internalformat, GLenum format, GLenum type, GLsizeiptr offset, GLsizeiptr size, const void *data);
        #endregion

        #region ARB_compute_shader

        //public const uint ARB_compute_shader 1
        #region GLCOREARB_PROTOTYPES

        //public static void glDispatchCompute (GLuint num_groups_x, GLuint num_groups_y, GLuint num_groups_z);
        //public static void glDispatchComputeIndirect (GLintptr indirect);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLDISPATCHCOMPUTEPROC) (GLuint num_groups_x, GLuint num_groups_y, GLuint num_groups_z);
        //typedef void (APIENTRYP PFNGLDISPATCHCOMPUTEINDIRECTPROC) (GLintptr indirect);
        #endregion

        #region ARB_copy_image

        //public const uint ARB_copy_image 1
        #region GLCOREARB_PROTOTYPES

        //public static void glCopyImageSubData (GLuint srcName, GLenum srcTarget, GLint srcLevel, GLint srcX, GLint srcY, GLint srcZ, GLuint dstName, GLenum dstTarget, GLint dstLevel, GLint dstX, GLint dstY, GLint dstZ, GLsizei srcWidth, GLsizei srcHeight, GLsizei srcDepth);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLCOPYIMAGESUBDATAPROC) (GLuint srcName, GLenum srcTarget, GLint srcLevel, GLint srcX, GLint srcY, GLint srcZ, GLuint dstName, GLenum dstTarget, GLint dstLevel, GLint dstX, GLint dstY, GLint dstZ, GLsizei srcWidth, GLsizei srcHeight, GLsizei srcDepth);
        #endregion

        #region ARB_texture_view

        //public const uint ARB_texture_view 1
        #region GLCOREARB_PROTOTYPES

        //public static void glTextureView (GLuint texture, GLenum target, GLuint origtexture, GLenum internalformat, GLuint minlevel, GLuint numlevels, GLuint minlayer, GLuint numlayers);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLTEXTUREVIEWPROC) (GLuint texture, GLenum target, GLuint origtexture, GLenum internalformat, GLuint minlevel, GLuint numlevels, GLuint minlayer, GLuint numlayers);
        #endregion

        #region ARB_vertex_attrib_binding

        //public const uint ARB_vertex_attrib_binding 1
        #region GLCOREARB_PROTOTYPES

        //public static void glBindVertexBuffer (GLuint bindingindex, GLuint buffer, GLintptr offset, GLsizei stride);
        //public static void glVertexAttribFormat (GLuint attribindex, GLint size, GLenum type, GLboolean normalized, GLuint relativeoffset);
        //public static void glVertexAttribIFormat (GLuint attribindex, GLint size, GLenum type, GLuint relativeoffset);
        //public static void glVertexAttribLFormat (GLuint attribindex, GLint size, GLenum type, GLuint relativeoffset);
        //public static void glVertexAttribBinding (GLuint attribindex, GLuint bindingindex);
        //public static void glVertexBindingDivisor (GLuint bindingindex, GLuint divisor);
        //public static void glVertexArrayBindVertexBufferEXT (GLuint vaobj, GLuint bindingindex, GLuint buffer, GLintptr offset, GLsizei stride);
        //public static void glVertexArrayVertexAttribFormatEXT (GLuint vaobj, GLuint attribindex, GLint size, GLenum type, GLboolean normalized, GLuint relativeoffset);
        //public static void glVertexArrayVertexAttribIFormatEXT (GLuint vaobj, GLuint attribindex, GLint size, GLenum type, GLuint relativeoffset);
        //public static void glVertexArrayVertexAttribLFormatEXT (GLuint vaobj, GLuint attribindex, GLint size, GLenum type, GLuint relativeoffset);
        //public static void glVertexArrayVertexAttribBindingEXT (GLuint vaobj, GLuint attribindex, GLuint bindingindex);
        //public static void glVertexArrayVertexBindingDivisorEXT (GLuint vaobj, GLuint bindingindex, GLuint divisor);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLBINDVERTEXBUFFERPROC) (GLuint bindingindex, GLuint buffer, GLintptr offset, GLsizei stride);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBFORMATPROC) (GLuint attribindex, GLint size, GLenum type, GLboolean normalized, GLuint relativeoffset);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBIFORMATPROC) (GLuint attribindex, GLint size, GLenum type, GLuint relativeoffset);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBLFORMATPROC) (GLuint attribindex, GLint size, GLenum type, GLuint relativeoffset);
        //typedef void (APIENTRYP PFNGLVERTEXATTRIBBINDINGPROC) (GLuint attribindex, GLuint bindingindex);
        //typedef void (APIENTRYP PFNGLVERTEXBINDINGDIVISORPROC) (GLuint bindingindex, GLuint divisor);
        //typedef void (APIENTRYP PFNGLVERTEXARRAYBINDVERTEXBUFFEREXTPROC) (GLuint vaobj, GLuint bindingindex, GLuint buffer, GLintptr offset, GLsizei stride);
        //typedef void (APIENTRYP PFNGLVERTEXARRAYVERTEXATTRIBFORMATEXTPROC) (GLuint vaobj, GLuint attribindex, GLint size, GLenum type, GLboolean normalized, GLuint relativeoffset);
        //typedef void (APIENTRYP PFNGLVERTEXARRAYVERTEXATTRIBIFORMATEXTPROC) (GLuint vaobj, GLuint attribindex, GLint size, GLenum type, GLuint relativeoffset);
        //typedef void (APIENTRYP PFNGLVERTEXARRAYVERTEXATTRIBLFORMATEXTPROC) (GLuint vaobj, GLuint attribindex, GLint size, GLenum type, GLuint relativeoffset);
        //typedef void (APIENTRYP PFNGLVERTEXARRAYVERTEXATTRIBBINDINGEXTPROC) (GLuint vaobj, GLuint attribindex, GLuint bindingindex);
        //typedef void (APIENTRYP PFNGLVERTEXARRAYVERTEXBINDINGDIVISOREXTPROC) (GLuint vaobj, GLuint bindingindex, GLuint divisor);
        #endregion

        #region ARB_robustness_isolation

        //public const uint ARB_robustness_isolation 1
        #endregion

        #region ARB_ES3_compatibility

        //public const uint ARB_ES3_compatibility 1
        #endregion

        #region ARB_explicit_uniform_location

        //public const uint ARB_explicit_uniform_location 1
        #endregion

        #region ARB_fragment_layer_viewport

        //public const uint ARB_fragment_layer_viewport 1
        #endregion

        #region ARB_framebuffer_no_attachments

        //public const uint ARB_framebuffer_no_attachments 1
        #region GLCOREARB_PROTOTYPES

        //public static void glFramebufferParameteri (GLenum target, GLenum pname, GLint parameter);
        //public static void glGetFramebufferParameteriv (GLenum target, GLenum pname, GLint *parameters);
        //public static void glNamedFramebufferParameteriEXT (GLuint framebuffer, GLenum pname, GLint parameter);
        //public static void glGetNamedFramebufferParameterivEXT (GLuint framebuffer, GLenum pname, GLint *parameters);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLFRAMEBUFFERPARAMETERIPROC) (GLenum target, GLenum pname, GLint parameter);
        //typedef void (APIENTRYP PFNGLGETFRAMEBUFFERPARAMETERIVPROC) (GLenum target, GLenum pname, GLint *parameters);
        //typedef void (APIENTRYP PFNGLNAMEDFRAMEBUFFERPARAMETERIEXTPROC) (GLuint framebuffer, GLenum pname, GLint parameter);
        //typedef void (APIENTRYP PFNGLGETNAMEDFRAMEBUFFERPARAMETERIVEXTPROC) (GLuint framebuffer, GLenum pname, GLint *parameters);
        #endregion

        #region ARB_internalformat_query2

        //public const uint ARB_internalformat_query2 1
        #region GLCOREARB_PROTOTYPES

        //public static void glGetInternalformati64v (GLenum target, GLenum internalformat, GLenum pname, GLsizei bufSize, GLint64 *parameters);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLGETINTERNALFORMATI64VPROC) (GLenum target, GLenum internalformat, GLenum pname, GLsizei bufSize, GLint64 *parameters);
        #endregion

        #region ARB_invalidate_subdata

        //public const uint ARB_invalidate_subdata 1
        #region GLCOREARB_PROTOTYPES

        //public static void glInvalidateTexSubImage (GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth);
        //public static void glInvalidateTexImage (GLuint texture, GLint level);
        //public static void glInvalidateBufferSubData (GLuint buffer, GLintptr offset, GLsizeiptr length);
        //public static void glInvalidateBufferData (GLuint buffer);
        //public static void glInvalidateFramebuffer (GLenum target, GLsizei numAttachments, const GLenum *attachments);
        //public static void glInvalidateSubFramebuffer (GLenum target, GLsizei numAttachments, const GLenum *attachments, GLint x, GLint y, GLsizei width, GLsizei height);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLINVALIDATETEXSUBIMAGEPROC) (GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth);
        //typedef void (APIENTRYP PFNGLINVALIDATETEXIMAGEPROC) (GLuint texture, GLint level);
        //typedef void (APIENTRYP PFNGLINVALIDATEBUFFERSUBDATAPROC) (GLuint buffer, GLintptr offset, GLsizeiptr length);
        //typedef void (APIENTRYP PFNGLINVALIDATEBUFFERDATAPROC) (GLuint buffer);
        //typedef void (APIENTRYP PFNGLINVALIDATEFRAMEBUFFERPROC) (GLenum target, GLsizei numAttachments, const GLenum *attachments);
        //typedef void (APIENTRYP PFNGLINVALIDATESUBFRAMEBUFFERPROC) (GLenum target, GLsizei numAttachments, const GLenum *attachments, GLint x, GLint y, GLsizei width, GLsizei height);
        #endregion

        #region ARB_multi_draw_indirect

        //public const uint ARB_multi_draw_indirect 1
        #region GLCOREARB_PROTOTYPES

        //public static void glMultiDrawArraysIndirect (GLenum mode, const void *indirect, GLsizei drawcount, GLsizei stride);
        //public static void glMultiDrawElementsIndirect (GLenum mode, GLenum type, const void *indirect, GLsizei drawcount, GLsizei stride);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLMULTIDRAWARRAYSINDIRECTPROC) (GLenum mode, const void *indirect, GLsizei drawcount, GLsizei stride);
        //typedef void (APIENTRYP PFNGLMULTIDRAWELEMENTSINDIRECTPROC) (GLenum mode, GLenum type, const void *indirect, GLsizei drawcount, GLsizei stride);
        #endregion

        #region ARB_program_interface_query

        //public const uint ARB_program_interface_query 1
        #region GLCOREARB_PROTOTYPES

        //public static void glGetProgramInterfaceiv (GLuint program, GLenum programInterface, GLenum pname, GLint *parameters);
        //public static GLuint glGetProgramResourceIndex (GLuint program, GLenum programInterface, const GLchar *name);
        //public static void glGetProgramResourceName (GLuint program, GLenum programInterface, GLuint index, GLsizei bufSize, GLsizei *length, GLchar *name);
        //public static void glGetProgramResourceiv (GLuint program, GLenum programInterface, GLuint index, GLsizei propCount, const GLenum *props, GLsizei bufSize, GLsizei *length, GLint *parameters);
        //public static GLint glGetProgramResourceLocation (GLuint program, GLenum programInterface, const GLchar *name);
        //public static GLint glGetProgramResourceLocationIndex (GLuint program, GLenum programInterface, const GLchar *name);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLGETPROGRAMINTERFACEIVPROC) (GLuint program, GLenum programInterface, GLenum pname, GLint *parameters);
        //typedef GLuint (APIENTRYP PFNGLGETPROGRAMRESOURCEINDEXPROC) (GLuint program, GLenum programInterface, const GLchar *name);
        //typedef void (APIENTRYP PFNGLGETPROGRAMRESOURCENAMEPROC) (GLuint program, GLenum programInterface, GLuint index, GLsizei bufSize, GLsizei *length, GLchar *name);
        //typedef void (APIENTRYP PFNGLGETPROGRAMRESOURCEIVPROC) (GLuint program, GLenum programInterface, GLuint index, GLsizei propCount, const GLenum *props, GLsizei bufSize, GLsizei *length, GLint *parameters);
        //typedef GLint (APIENTRYP PFNGLGETPROGRAMRESOURCELOCATIONPROC) (GLuint program, GLenum programInterface, const GLchar *name);
        //typedef GLint (APIENTRYP PFNGLGETPROGRAMRESOURCELOCATIONINDEXPROC) (GLuint program, GLenum programInterface, const GLchar *name);
        #endregion

        #region ARB_robust_buffer_access_behavior

        //public const uint ARB_robust_buffer_access_behavior 1
        #endregion

        #region ARB_shader_image_size

        //public const uint ARB_shader_image_size 1
        #endregion

        #region ARB_shader_storage_buffer_object

        //public const uint ARB_shader_storage_buffer_object 1
        #region GLCOREARB_PROTOTYPES

        //public static void glShaderStorageBlockBinding (GLuint program, GLuint storageBlockIndex, GLuint storageBlockBinding);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLSHADERSTORAGEBLOCKBINDINGPROC) (GLuint program, GLuint storageBlockIndex, GLuint storageBlockBinding);
        #endregion

        #region ARB_stencil_texturing

        //public const uint ARB_stencil_texturing 1
        #endregion

        #region ARB_texture_buffer_range

        //public const uint ARB_texture_buffer_range 1
        #region GLCOREARB_PROTOTYPES

        //public static void glTexBufferRange (GLenum target, GLenum internalformat, GLuint buffer, GLintptr offset, GLsizeiptr size);
        //public static void glTextureBufferRangeEXT (GLuint texture, GLenum target, GLenum internalformat, GLuint buffer, GLintptr offset, GLsizeiptr size);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLTEXBUFFERRANGEPROC) (GLenum target, GLenum internalformat, GLuint buffer, GLintptr offset, GLsizeiptr size);
        //typedef void (APIENTRYP PFNGLTEXTUREBUFFERRANGEEXTPROC) (GLuint texture, GLenum target, GLenum internalformat, GLuint buffer, GLintptr offset, GLsizeiptr size);
        #endregion

        #region ARB_texture_query_levels

        //public const uint ARB_texture_query_levels 1
        #endregion

        #region ARB_texture_storage_multisample

        //public const uint ARB_texture_storage_multisample 1
        #region GLCOREARB_PROTOTYPES

        //public static void glTexStorage2DMultisample (GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);
        //public static void glTexStorage3DMultisample (GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);
        //public static void glTextureStorage2DMultisampleEXT (GLuint texture, GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);
        //public static void glTextureStorage3DMultisampleEXT (GLuint texture, GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);
        #endregion /* GLCOREARB_PROTOTYPES */
        //typedef void (APIENTRYP PFNGLTEXSTORAGE2DMULTISAMPLEPROC) (GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);
        //typedef void (APIENTRYP PFNGLTEXSTORAGE3DMULTISAMPLEPROC) (GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);
        //typedef void (APIENTRYP PFNGLTEXTURESTORAGE2DMULTISAMPLEEXTPROC) (GLuint texture, GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);
        //typedef void (APIENTRYP PFNGLTEXTURESTORAGE3DMULTISAMPLEEXTPROC) (GLuint texture, GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);
        #endregion
    }
}
