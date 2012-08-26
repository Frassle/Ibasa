using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Media.Visual
{
    /// <summary>
    /// Resource data formats which includes fully-typed and typeless formats.
    /// </summary>
    public enum DxgiFormat
    {
        ///<summary>
        /// The format is not known.
        ///</summary>
        Unknown                      = 0,
        ///<summary>
        /// A four-component, 128-bit typeless format.
        ///</summary>
        R32G32B32A32_Typeless       = 1,
        ///<summary>
        /// A four-component, 128-bit floating-point format.
        ///</summary>
        R32G32B32A32_Float          = 2,
        ///<summary>
        /// A four-component, 128-bit unsigned-integer format.
        ///</summary>
        R32G32B32A32_UInt           = 3,
        ///<summary>
        /// A four-component, 128-bit signed-integer format.
        ///</summary>
        R32G32B32A32_SInt           = 4,
        ///<summary>
        /// A three-component, 96-bit typeless format.
        ///</summary>
        R32G32B32_Typeless          = 5,
        ///<summary>
        /// A three-component, 96-bit floating-point format.
        ///</summary>
        R32G32B32_Float             = 6,
        ///<summary>
        /// A three-component, 96-bit unsigned-integer format.
        ///</summary>
        R32G32B32_UInt              = 7,
        ///<summary>
        /// A three-component, 96-bit signed-integer format.
        ///</summary>
        R32G32B32_SInt              = 8,
        ///<summary>
        /// A four-component, 64-bit typeless format.
        ///</summary>
        R16G16B16A16_Typeless       = 9,
        ///<summary>
        /// A four-component, 64-bit floating-point format.
        ///</summary>
        R16G16B16A16_Float          = 10,
        ///<summary>
        /// A four-component, 64-bit unsigned-integer format.
        ///</summary>
        R16G16B16A16_UNorm          = 11,
        ///<summary>
        /// A four-component, 64-bit unsigned-integer format.
        ///</summary>
        R16G16B16A16_UInt           = 12,
        ///<summary>
        /// A four-component, 64-bit signed-integer format.
        ///</summary>
        R16G16B16A16_SNorm          = 13,
        ///<summary>
        /// A four-component, 64-bit signed-integer format.
        ///</summary>
        R16G16B16A16_SInt           = 14,
        ///<summary>
        /// A two-component, 64-bit typeless format.
        ///</summary>
        R32G32_Typeless             = 15,
        ///<summary>
        /// A two-component, 64-bit floating-point format.
        ///</summary>
        R32G32_Float                = 16,
        ///<summary>
        /// A two-component, 64-bit unsigned-integer format.
        ///</summary>
        R32G32_UInt                 = 17,
        ///<summary>
        /// A two-component, 64-bit signed-integer format.
        ///</summary>
        R32G32_SInt                 = 18,
        ///<summary>
        /// A two-component, 64-bit typeless format.
        ///</summary>
        R32G8X24_Typeless           = 19,
        ///<summary>
        /// A 32-bit floating-point component, and two unsigned-integer components (with an additional 32 bits).
        ///</summary>
        D32_FLOAT_S8X24_UInt        = 20,
        ///<summary>
        /// A 32-bit floating-point component, and two typeless components (with an additional 32 bits).
        ///</summary>
        R32_FLOAT_X8X24_Typeless    = 21,
        ///<summary>
        /// A 32-bit typeless component, and two unsigned-integer components (with an additional 32 bits).
        ///</summary>
        X32_TYPELESS_G8X24_UInt     = 22,
        ///<summary>
        /// A four-component, 32-bit typeless format.
        ///</summary>
        R10G10B10A2_Typeless        = 23,
        ///<summary>
        /// A four-component, 32-bit unsigned-integer format.
        ///</summary>
        R10G10B10A2_UNorm           = 24,
        ///<summary>
        /// A four-component, 32-bit unsigned-integer format.
        ///</summary>
        R10G10B10A2_UInt            = 25,
        ///<summary>
        /// Three partial-precision floating-point numbers encodeded into a single 32-bit value.
        ///</summary>
        R11G11B10_Float             = 26,
        ///<summary>
        /// A three-component, 32-bit typeless format.
        ///</summary>
        R8G8B8A8_Typeless           = 27,
        ///<summary>
        /// A four-component, 32-bit unsigned-integer format.
        ///</summary>
        R8G8B8A8_UNorm              = 28,
        ///<summary>
        /// A four-component, 32-bit unsigned-normalized integer sRGB format.
        ///</summary>
        R8G8B8A8_UNorm_SRGB         = 29,
        ///<summary>
        /// A four-component, 32-bit unsigned-integer format.
        ///</summary>
        R8G8B8A8_UInt               = 30,
        ///<summary>
        /// A three-component, 32-bit signed-integer format.
        ///</summary>
        R8G8B8A8_SNorm              = 31,
        ///<summary>
        /// A three-component, 32-bit signed-integer format.
        ///</summary>
        R8G8B8A8_SInt               = 32,
        ///<summary>
        /// A two-component, 32-bit typeless format.
        ///</summary>
        R16G16_Typeless             = 33,
        ///<summary>
        /// A two-component, 32-bit floating-point format.
        ///</summary>
        R16G16_Float                = 34,
        ///<summary>
        /// A two-component, 32-bit unsigned-integer format.
        ///</summary>
        R16G16_UNorm                = 35,
        ///<summary>
        /// A two-component, 32-bit unsigned-integer format.
        ///</summary>
        R16G16_UInt                 = 36,
        ///<summary>
        /// A two-component, 32-bit signed-integer format.
        ///</summary>
        R16G16_SNorm                = 37,
        ///<summary>
        /// A two-component, 32-bit signed-integer format.
        ///</summary>
        R16G16_SInt                 = 38,
        ///<summary>
        /// A single-component, 32-bit typeless format.
        ///</summary>
        R32_Typeless                = 39,
        ///<summary>
        /// A single-component, 32-bit floating-point format.
        ///</summary>
        D32_Float                   = 40,
        ///<summary>
        /// A single-component, 32-bit floating-point format.
        ///</summary>
        R32_Float                   = 41,
        ///<summary>
        /// A single-component, 32-bit unsigned-integer format.
        ///</summary>
        R32_UInt                    = 42,
        ///<summary>
        /// A single-component, 32-bit signed-integer format.
        ///</summary>
        R32_SInt                    = 43,
        ///<summary>
        /// A two-component, 32-bit typeless format.
        ///</summary>
        R24G8_Typeless              = 44,
        ///<summary>
        /// A 32-bit z-buffer format that uses 24 bits for the depth channel and 8 bits for the stencil channel.
        ///</summary>
        D24_UNORM_S8_UInt           = 45,
        ///<summary>
        /// A 32-bit format, that contains a 24 bit, single-component, unsigned-normalized integer, with an additional typeless 8 bits.
        ///</summary>
        R24_UNORM_X8_Typeless       = 46,
        ///<summary>
        /// A 32-bit format, that contains a 24 bit, single-component, typeless format,  with an additional 8 bit unsigned integer component.
        ///</summary>
        X24_TYPELESS_G8_UInt        = 47,
        ///<summary>
        /// A two-component, 16-bit typeless format.
        ///</summary>
        R8G8_Typeless               = 48,
        ///<summary>
        /// A two-component, 16-bit unsigned-integer format.
        ///</summary>
        R8G8_UNorm                  = 49,
        ///<summary>
        /// A two-component, 16-bit unsigned-integer format.
        ///</summary>
        R8G8_UInt                   = 50,
        ///<summary>
        /// A two-component, 16-bit signed-integer format.
        ///</summary>
        R8G8_SNorm                  = 51,
        ///<summary>
        /// A two-component, 16-bit signed-integer format.
        ///</summary>
        R8G8_SInt                   = 52,
        ///<summary>
        /// A single-component, 16-bit typeless format.
        ///</summary>
        R16_Typeless                = 53,
        ///<summary>
        /// A single-component, 16-bit floating-point format.
        ///</summary>
        R16_Float                   = 54,
        ///<summary>
        /// A single-component, 16-bit unsigned-normalized integer format.
        ///</summary>
        D16_UNorm                   = 55,
        ///<summary>
        /// A single-component, 16-bit unsigned-integer format.
        ///</summary>
        R16_UNorm                   = 56,
        ///<summary>
        /// A single-component, 16-bit unsigned-integer format.
        ///</summary>
        R16_UInt                    = 57,
        ///<summary>
        /// A single-component, 16-bit signed-integer format.
        ///</summary>
        R16_SNorm                   = 58,
        ///<summary>
        /// A single-component, 16-bit signed-integer format.
        ///</summary>
        R16_SInt                    = 59,
        ///<summary>
        /// A single-component, 8-bit typeless format.
        ///</summary>
        R8_Typeless                 = 60,
        ///<summary>
        /// A single-component, 8-bit unsigned-integer format.
        ///</summary>
        R8_UNorm                    = 61,
        ///<summary>
        /// A single-component, 8-bit unsigned-integer format.
        ///</summary>
        R8_UInt                     = 62,
        ///<summary>
        /// A single-component, 8-bit signed-integer format.
        ///</summary>
        R8_SNorm                    = 63,
        ///<summary>
        /// A single-component, 8-bit signed-integer format.
        ///</summary>
        R8_SInt                     = 64,
        ///<summary>
        /// A single-component, 8-bit unsigned-integer format.
        ///</summary>
        A8_UNorm                    = 65,
        ///<summary>
        /// A single-component, 1-bit unsigned-normalized integer format.
        ///</summary>
        R1_UNorm                    = 66,
        ///<summary>
        /// Three partial-precision floating-point numbers encoded into a single 32-bit value all sharing the same 5-bit exponent (variant of s10e5). 
        /// There is no sign bit, and there is a shared 5-bit biased (15) exponent and a 9-bit mantissa for each channel.
        ///</summary>
        R9G9B9E5_SharedExponent     = 67,
        ///<summary>
        /// A four-component, 32-bit unsigned-normalized integer format.
        ///</summary>
        R8G8_B8G8_UNorm             = 68,
        ///<summary>
        /// A four-component, 32-bit unsigned-normalized integer format.
        ///</summary>
        G8R8_G8B8_UNorm             = 69,
        ///<summary>
        /// Four-component typeless block-compression format.
        ///</summary>
        BC1_Typeless                = 70,
        ///<summary>
        /// Four-component block-compression format.
        ///</summary>
        BC1_UNorm                   = 71,
        ///<summary>
        /// Four-component block-compression format for sRGB data.
        ///</summary>
        BC1_UNorm_SRGB              = 72,
        ///<summary>
        /// Four-component typeless block-compression format.
        ///</summary>
        BC2_Typeless                = 73,
        ///<summary>
        /// Four-component block-compression format.
        ///</summary>
        BC2_UNorm                   = 74,
        ///<summary>
        /// Four-component block-compression format for sRGB data.
        ///</summary>
        BC2_UNorm_SRGB              = 75,
        ///<summary>
        /// Four-component typeless block-compression format.
        ///</summary>
        BC3_Typeless                = 76,
        ///<summary>
        /// Four-component block-compression format.
        ///</summary>
        BC3_UNorm                   = 77,
        ///<summary>
        /// Four-component block-compression format for sRGB data.
        ///</summary>
        BC3_UNorm_SRGB              = 78,
        ///<summary>
        /// One-component typeless block-compression format.
        ///</summary>
        BC4_Typeless                = 79,
        ///<summary>
        /// One-component block-compression format.
        ///</summary>
        BC4_UNorm                   = 80,
        ///<summary>
        /// One-component block-compression format.
        ///</summary>
        BC4_SNorm                   = 81,
        ///<summary>
        /// Two-component typeless block-compression format.
        ///</summary>
        BC5_Typeless                = 82,
        ///<summary>
        /// Two-component block-compression format.
        ///</summary>
        BC5_UNorm                   = 83,
        ///<summary>
        /// Two-component block-compression format.
        ///</summary>
        BC5_SNorm                   = 84,
        ///<summary>
        /// A three-component, 16-bit unsigned-normalized integer format.
        ///</summary>
        B5G6R5_UNorm                = 85,
        ///<summary>
        /// A four-component, 16-bit unsigned-normalized integer format that supports 1-bit alpha.
        ///</summary>
        B5G5R5A1_UNorm              = 86,
        ///<summary>
        /// A four-component, 32-bit unsigned-normalized integer format that supports 8-bit alpha.
        ///</summary>
        B8G8R8A8_UNorm              = 87,
        ///<summary>
        /// A four-component, 32-bit unsigned-normalized integer format.
        ///</summary>
        B8G8R8X8_UNorm              = 88,
        ///<summary>
        /// A four-component, 32-bit format that supports 2-bit alpha.
        ///</summary>
        R10G10B10_XR_BIAS_A2_UNorm  = 89,
        ///<summary>
        /// A four-component, 32-bit typeless format that supports 8-bit alpha.
        ///</summary>
        B8G8R8A8_Typeless           = 90,
        ///<summary>
        /// A four-component, 32-bit unsigned-normalized standard RGB format that supports 8-bit alpha.
        ///</summary>
        B8G8R8A8_UNorm_SRGB         = 91,
        ///<summary>
        /// A four-component, 32-bit typeless format.
        ///</summary>
        B8G8R8X8_Typeless           = 92,
        ///<summary>
        /// A four-component, 32-bit unsigned-normalized standard RGB format.
        ///</summary>
        B8G8R8X8_UNorm_SRGB         = 93,
        ///<summary>
        /// A typeless block-compression format.
        ///</summary>
        BC6H_Typeless               = 94,
        ///<summary>
        /// A block-compression format.
        ///</summary>
        BC6H_UF16                   = 95,
        ///<summary>
        /// A block-compression format.
        ///</summary>
        BC6H_SF16                   = 96,
        ///<summary>
        /// A typeless block-compression format.
        ///</summary>
        BC7_Typeless                = 97,
        ///<summary>
        /// A block-compression format.
        ///</summary>
        BC7_UNorm                   = 98,
        ///<summary>
        /// A block-compression format.
        ///</summary>
        BC7_UNorm_SRGB              = 99,
    }
}
