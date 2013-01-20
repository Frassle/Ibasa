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

        #region Device Management

        /// <summary>This function opens a device by name.</summary>
        /// <param name="devicename">a null-terminated string describing a device.</param>
        /// <returns>Returns a pointer to the opened device. The return value will be NULL if there is an error.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcOpenDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr OpenDevice([In] string devicename);
        // ALC_API ALCdevice *     ALC_APIENTRY alcOpenDevice( const ALCchar *devicename );

        /// <summary>This function closes a device by name.</summary>
        /// <param name="device">a pointer to an opened device</param>
        /// <returns>True will be returned on success or False on failure. Closing a device will fail if the device contains any contexts or buffers.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcCloseDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern bool CloseDevice([In] IntPtr device);
        // ALC_API ALCboolean      ALC_APIENTRY alcCloseDevice( ALCdevice *device );

        #endregion Device Management

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

        #region Capture functions

        /// <summary>This function opens a capture device by name. </summary>
        /// <param name="devicename">a pointer to a device name string.</param>
        /// <param name="frequency">the frequency that the buffer should be captured at.</param>
        /// <param name="format">the requested capture buffer format.</param>
        /// <param name="buffersize">the size of the capture buffer in samples, not bytes.</param>
        /// <returns>Returns the capture device pointer, or NULL on failure.</returns>
        [CLSCompliant(false), DllImport("openal32.dll", EntryPoint = "alcCaptureOpenDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr CaptureOpenDevice(string devicename, uint frequency, int format, int buffersize);

        /// <summary>This function opens a capture device by name. </summary>
        /// <param name="devicename">a pointer to a device name string.</param>
        /// <param name="frequency">the frequency that the buffer should be captured at.</param>
        /// <param name="format">the requested capture buffer format.</param>
        /// <param name="buffersize">the size of the capture buffer in samples, not bytes.</param>
        /// <returns>Returns the capture device pointer, or NULL on failure.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcCaptureOpenDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr CaptureOpenDevice(string devicename, int frequency, int format, int buffersize);

        // ALC_API ALCdevice*      ALC_APIENTRY alcCaptureOpenDevice( const ALCchar *devicename, ALCuint frequency, ALCenum format, ALCsizei buffersize );

        /// <summary>This function closes the specified capture device.</summary>
        /// <param name="device">a pointer to a capture device.</param>
        /// <returns>Returns True if the close operation was successful, False on failure.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcCaptureCloseDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern bool CaptureCloseDevice([In] IntPtr device);
        // ALC_API ALCboolean      ALC_APIENTRY alcCaptureCloseDevice( ALCdevice *device );

        /// <summary>This function begins a capture operation.</summary>
        /// <remarks>alcCaptureStart will begin recording to an internal ring buffer of the size specified when opening the capture device. The application can then retrieve the number of samples currently available using the ALC_CAPTURE_SAPMPLES token with alcGetIntegerv. When the application determines that enough samples are available for processing, then it can obtain them with a call to alcCaptureSamples.</remarks>
        /// <param name="device">a pointer to a capture device.</param>
        [DllImport("openal32.dll", EntryPoint = "alcCaptureStart", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void CaptureStart([In] IntPtr device);
        // ALC_API void            ALC_APIENTRY alcCaptureStart( ALCdevice *device );

        /// <summary>This function stops a capture operation.</summary>
        /// <param name="device">a pointer to a capture device.</param>
        [DllImport("openal32.dll", EntryPoint = "alcCaptureStop", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void CaptureStop([In] IntPtr device);
        // ALC_API void            ALC_APIENTRY alcCaptureStop( ALCdevice *device );

        /// <summary>This function completes a capture operation, and does not block.</summary>
        /// <param name="device">a pointer to a capture device.</param>
        /// <param name="buffer">a pointer to a buffer, which must be large enough to accommodate the number of samples.</param>
        /// <param name="samples">the number of samples to be retrieved.</param>
        [DllImport("openal32.dll", EntryPoint = "alcCaptureSamples", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern void CaptureSamples(IntPtr device, void* buffer, int samples);
        // ALC_API void            ALC_APIENTRY alcCaptureSamples( ALCdevice *device, ALCvoid *buffer, ALCsizei samples );

        #endregion Capture functions
    }
}