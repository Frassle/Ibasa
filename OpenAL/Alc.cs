#region --- OpenTK.OpenAL License ---
/* AlcFunctions.cs
 * C header: \OpenAL 1.1 SDK\include\Alc.h
 * Spec: http://www.openal.org/openal_webstf/specs/OpenAL11Specification.pdf
 * Copyright (c) 2008 Christoph Brandtner and Stefanos Apostolopoulos
 * See license.txt for license details
 * http://www.OpenTK.net */
#endregion

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

/* Type Mapping
// 8-bit boolean 
typedef char ALCboolean;
 * byte
// character 
typedef char ALCchar;
 * byte
// signed 8-bit 2's complement integer 
typedef char ALCbyte;
 * byte

// unsigned 8-bit integer 
typedef unsigned char ALCubyte;
 * ubyte

// signed 16-bit 2's complement integer 
typedef short ALCshort;
 * short

// unsigned 16-bit integer 
typedef unsigned short ALCushort;
 * ushort

// unsigned 32-bit integer 
typedef unsigned int ALCuint;
 * uint
  
// signed 32-bit 2's complement integer 
typedef int ALCint;
 * int
// non-negative 32-bit binary integer size
typedef int ALCsizei;
 * int
// enumerated 32-bit value
typedef int ALCenum;
 * int

// 32-bit IEEE754 floating-point
typedef float ALCfloat;
 * float

// 64-bit IEEE754 floating-point
typedef double ALCdouble;
 * double
 
// void type (for opaque pointers only)
typedef void ALCvoid;
 * void
 
 * ALCdevice
 * ALCcontext *context
 * IntPtr
*/

namespace Ibasa.OpenAL
{
    /// <summary>Alc = Audio Library Context</summary>
    internal static class Alc
    {
        #region Constants


        /**
        * The Specifier string for default device
        */
        public const int ALC_DEFAULT_DEVICE_SPECIFIER = 0x1004;
        public const int ALC_DEVICE_SPECIFIER = 0x1005;
        public const int ALC_EXTENSIONS = 0x1006;

        public const int ALC_MAJOR_VERSION = 0x1000;
        public const int ALC_MINOR_VERSION = 0x1001;

        public const int ALC_ATTRIBUTES_SIZE = 0x1002;
        public const int ALC_ALL_ATTRIBUTES = 0x1003;

        /**
        * ALC_ENUMERATE_ALL_EXT enums
        */
        public const int ALC_DEFAULT_ALL_DEVICES_SPECIFIER = 0x1012;
        public const int ALC_ALL_DEVICES_SPECIFIER = 0x1013;

        /**
        * Capture extension
        */
        public const int ALC_CAPTURE_DEVICE_SPECIFIER = 0x310;
        public const int ALC_CAPTURE_DEFAULT_DEVICE_SPECIFIER = 0x311;
        public const int ALC_CAPTURE_SAMPLES = 0x312;

        #endregion

        #region Query functions

        [DllImport("openal32.dll", EntryPoint = "alcGetString", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr GetString([In] IntPtr device, int param);
        // ALC_API const ALCchar * ALC_APIENTRY alcGetString( ALCdevice *device, ALCenum param );

        public static string GetMarshaledString(IntPtr device, int param)
        {
            return Marshal.PtrToStringAnsi(GetString(device, param));
        }

        public static List<string> GetMarshaledStringList(IntPtr device, int param)
        {
            List<string> result = new List<string>();
            IntPtr ptr = GetString(device, param);

            while (Marshal.ReadByte(ptr) != 0)
            {
                var str = Marshal.PtrToStringAnsi(ptr);
                result.Add(str);
                ptr += str.Length + 1;
            }

            return result;
        }

        [DllImport("openal32.dll", EntryPoint = "alcGetIntegerv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        public unsafe static extern void GetInteger(IntPtr device, int param, int size, int* data);
        // ALC_API void            ALC_APIENTRY alcGetIntegerv( ALCdevice *device, ALCenum param, ALCsizei size, ALCint *buffer );

        public static int GetInteger(IntPtr device, int param)
        {
            unsafe
            {
                int data = 0;
                GetInteger(device, param, 1, &data);
                return data;
            }
        }

        public static void GetInteger(IntPtr device, int param, int size, int[] data)
        {
            unsafe
            {
                fixed (int* ptr = data)
                {
                    GetInteger(device, param, size, ptr);
                }
            }
        }

        #endregion Query functions

        
    }
}