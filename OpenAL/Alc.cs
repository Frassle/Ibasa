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
        * errors
        */

        /**
        * No error
        */
        public const int ALC_NO_ERROR = 0;

        /**
        * No device
        */
        public const int ALC_INVALID_DEVICE = 0xA001;

        /**
        * invalid context ID
        */
        public const int ALC_INVALID_CONTEXT = 0xA002;

        /**
        * bad enum
        */
        public const int ALC_INVALID_ENUM = 0xA003;

        /**
        * bad value
        */
        public const int ALC_INVALID_VALUE = 0xA004;

        /**
        * Out of memory.
        */
        public const int ALC_OUT_OF_MEMORY = 0xA005;


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

        #region Context Management

        #region CreateContext

        [DllImport("openal32.dll", EntryPoint = "alcCreateContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public unsafe static extern IntPtr CreateContext([In] IntPtr device, [In] int* attrlist);

        #endregion

        [DllImport("openal32.dll", EntryPoint = "alcMakeContextCurrent", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern bool MakeContextCurrent(IntPtr context);
        // ALC_API ALCboolean      ALC_APIENTRY alcMakeContextCurrent( ALCcontext *context );

        [DllImport("openal32.dll", EntryPoint = "alcProcessContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void ProcessContext(IntPtr context);
        // ALC_API void            ALC_APIENTRY alcProcessContext( ALCcontext *context );

        [DllImport("openal32.dll", EntryPoint = "alcSuspendContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void SuspendContext(IntPtr context);
        // ALC_API void            ALC_APIENTRY alcSuspendContext( ALCcontext *context );

        [DllImport("openal32.dll", EntryPoint = "alcDestroyContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void DestroyContext(IntPtr context);
        // ALC_API void            ALC_APIENTRY alcDestroyContext( ALCcontext *context );

        [DllImport("openal32.dll", EntryPoint = "alcGetCurrentContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr GetCurrentContext();
        // ALC_API ALCcontext *    ALC_APIENTRY alcGetCurrentContext( void );

        [DllImport("openal32.dll", EntryPoint = "alcGetContextsDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr GetContextsDevice(IntPtr context);
        // ALC_API ALCdevice*      ALC_APIENTRY alcGetContextsDevice( ALCcontext *context );

        #endregion Context Management

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

        #region Error support.

        /// <summary>This function retrieves the current context error state.</summary>
        /// <param name="device">a pointer to the device to retrieve the error state from</param>
        /// <returns>Errorcode Int32.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcGetError", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern int GetError([In] IntPtr device);
        // ALC_API ALCenum         ALC_APIENTRY alcGetError( ALCdevice *device );

        internal static void ThrowError(IntPtr device)
        {
            var error = Alc.GetError(device);

            if (error == Alc.ALC_NO_ERROR)
            {
                return;
            }
            else if (error == Alc.ALC_INVALID_DEVICE)
            {
                throw new OpenALException("No Device. The device handle or specifier names an inaccessible driver/server.");
            }
            else if (error == Alc.ALC_INVALID_CONTEXT)
            {
                throw new OpenALException("Invalid context ID. The Context argument does not name a valid context.");
            }
            else if (error == Alc.ALC_INVALID_ENUM)
            {
                throw new OpenALException("Bad enum. A token used is not valid, or not applicable.");
            }
            else if (error == Alc.ALC_INVALID_VALUE)
            {
                throw new OpenALException("Bad value. A value (e.g. Attribute) is not valid, or not applicable.");
            }
            else if (error == Alc.ALC_OUT_OF_MEMORY)
            {
                throw new OpenALException("Out of memory. Unable to allocate memory.");
            }
            else
            {
                throw new OpenALException(string.Format("Unknown OpenAL error: {0}", error));
            }
        }

        #endregion Error support.

        #region Extension support.

        /// <summary>This function queries if a specified context extension is available.</summary>
        /// <param name="device">a pointer to the device to be queried for an extension.</param>
        /// <param name="extname">a null-terminated string describing the extension.</param>
        /// <returns>Returns True if the extension is available, False if the extension is not available.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcIsExtensionPresent", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        public static extern bool IsExtensionPresent([In] IntPtr device, [In] string extname);
        // ALC_API ALCboolean      ALC_APIENTRY alcIsExtensionPresent( ALCdevice *device, const ALCchar *extname );

        /// <summary>This function retrieves the address of a specified context extension function.</summary>
        /// <param name="device">a pointer to the device to be queried for the function.</param>
        /// <param name="funcname">a null-terminated string describing the function.</param>
        /// <returns>Returns the address of the function, or NULL if it is not found.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcGetProcAddress", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr GetProcAddress([In] IntPtr device, [In] string funcname);
        // ALC_API void  *         ALC_APIENTRY alcGetProcAddress( ALCdevice *device, const ALCchar *funcname );

        /// <summary>This function retrieves the enum value for a specified enumeration name.</summary>
        /// <param name="device">a pointer to the device to be queried.</param>
        /// <param name="enumname">a null terminated string describing the enum value.</param>
        /// <returns>Returns the enum value described by the enumName string. This is most often used for querying an enum value for an ALC extension.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcGetEnumValue", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        public static extern int GetEnumValue([In] IntPtr device, [In] string enumname);
        // ALC_API ALCenum         ALC_APIENTRY alcGetEnumValue( ALCdevice *device, const ALCchar *enumname );

        #endregion Extension support.

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