using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace Ibasa.OpenAL
{
    public static class OpenAL
    {
        #region Extension support.

        /// <summary>This function queries if a specified context extension is available.</summary>
        /// <param name="device">a pointer to the device to be queried for an extension.</param>
        /// <param name="extname">a null-terminated string describing the extension.</param>
        /// <returns>Returns True if the extension is available, False if the extension is not available.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcIsExtensionPresent", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        internal static extern bool IsExtensionPresent([In] IntPtr device, [In] string extname);
        // ALC_API ALCboolean      ALC_APIENTRY alcIsExtensionPresent( ALCdevice *device, const ALCchar *extname );

        /// <summary>This function retrieves the address of a specified context extension function.</summary>
        /// <param name="device">a pointer to the device to be queried for the function.</param>
        /// <param name="funcname">a null-terminated string describing the function.</param>
        /// <returns>Returns the address of the function, or NULL if it is not found.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcGetProcAddress", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        internal static extern IntPtr GetProcAddress([In] IntPtr device, [In] string funcname);
        // ALC_API void  *         ALC_APIENTRY alcGetProcAddress( ALCdevice *device, const ALCchar *funcname );

        /// <summary>This function retrieves the enum value for a specified enumeration name.</summary>
        /// <param name="device">a pointer to the device to be queried.</param>
        /// <param name="enumname">a null terminated string describing the enum value.</param>
        /// <returns>Returns the enum value described by the enumName string. This is most often used for querying an enum value for an ALC extension.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcGetEnumValue", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        internal static extern int GetEnumValue([In] IntPtr device, [In] string enumname);
        // ALC_API ALCenum         ALC_APIENTRY alcGetEnumValue( ALCdevice *device, const ALCchar *enumname );

        public static bool IsExtensionPresent(string extension)
        {
            return IsExtensionPresent(IntPtr.Zero, extension);
        }
        
        public static IntPtr GetProcAddress(string function)
        {
            return GetProcAddress(IntPtr.Zero, function);
        }

        public static int GetEnumValue(string enumeration)
        {
            return GetEnumValue(IntPtr.Zero, enumeration);
        }

        #endregion Extension support.

        #region Query functions

        [DllImport("openal32.dll", EntryPoint = "alcGetString", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        internal static extern IntPtr GetString([In] IntPtr device, int param);
        // ALC_API const ALCchar * ALC_APIENTRY alcGetString( ALCdevice *device, ALCenum param );

        public static string GetString(int param)
        {
            return MarshalString(GetString(IntPtr.Zero, param));
        }

        public static List<string> GetStrings(int param)
        {
            return MarshalStringList(GetString(IntPtr.Zero, param));
        }

        internal static string MarshalString(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }

        internal static List<string> MarshalStringList(IntPtr ptr)
        {
            List<string> result = new List<string>();

            while (Marshal.ReadByte(ptr) != 0)
            {
                var str = Marshal.PtrToStringAnsi(ptr);
                result.Add(str);
                ptr += str.Length + 1;
            }

            return result;
        }

        [DllImport("openal32.dll", EntryPoint = "alcGetIntegerv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        internal unsafe static extern void GetInteger(IntPtr device, int param, int size, int* data);
        // ALC_API void            ALC_APIENTRY alcGetIntegerv( ALCdevice *device, ALCenum param, ALCsizei size, ALCint *buffer );

        internal static int GetInteger(IntPtr device, int param)
        {
            unsafe
            {
                int data;
                GetInteger(device, param, 1, &data);
                return data;
            }
        }

        public static unsafe void GetInteger(int param, int size, int* data)
        {
            GetInteger(IntPtr.Zero, param, size, data);
        }

        public static void GetInteger(int param, int size, int[] data)
        {
            unsafe
            {
                fixed (int* ptr = data)
                {
                    GetInteger(IntPtr.Zero, param, size, ptr);
                }
            }
        }

        public static int GetInteger(int param)
        {
            return GetInteger(IntPtr.Zero, param);
        }

        #endregion Query functions

        #region GetError

        [DllImport("openal32.dll", EntryPoint = "alcGetError", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        private static extern int alcGetError([In] IntPtr device);
        // ALC_API ALCenum         ALC_APIENTRY alcGetError( ALCdevice *device );

        internal static void GetError(IntPtr device)
        {
            var error = alcGetError(device);

            if (error == ALC_NO_ERROR)
            {
                return;
            }
            else if (error == ALC_INVALID_DEVICE)
            {
                throw new OpenALException("No Device. The device handle or specifier names an inaccessible driver/server.");
            }
            else if (error == ALC_INVALID_CONTEXT)
            {
                throw new OpenALException("Invalid context ID. The Context argument does not name a valid context.");
            }
            else if (error == ALC_INVALID_ENUM)
            {
                throw new OpenALException("Bad enum. A token used is not valid, or not applicable.");
            }
            else if (error == ALC_INVALID_VALUE)
            {
                throw new OpenALException("Bad value. A value (e.g. Attribute) is not valid, or not applicable.");
            }
            else if (error == ALC_OUT_OF_MEMORY)
            {
                throw new OpenALException("Out of memory. Unable to allocate memory.");
            }
            else
            {
                throw new OpenALException(string.Format("Unknown OpenAL error: {0}", error));
            }
        }

        internal static readonly int ALC_NO_ERROR = GetEnumValue("ALC_NO_ERROR");
        internal static readonly int ALC_INVALID_DEVICE = GetEnumValue("ALC_INVALID_DEVICE");
        internal static readonly int ALC_INVALID_CONTEXT = GetEnumValue("ALC_INVALID_CONTEXT");
        internal static readonly int ALC_INVALID_ENUM = GetEnumValue("ALC_INVALID_ENUM");
        internal static readonly int ALC_INVALID_VALUE = GetEnumValue("ALC_INVALID_VALUE");
        internal static readonly int ALC_OUT_OF_MEMORY = GetEnumValue("ALC_OUT_OF_MEMORY");

        #endregion GetError

        internal static void ThrowNullException(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }
        }

        public static readonly int ALC_FREQUENCY = GetEnumValue("ALC_FREQUENCY");
        public static readonly int ALC_REFRESH = GetEnumValue("ALC_REFRESH");
        public static readonly int ALC_SYNC = GetEnumValue("ALC_SYNC");
        public static readonly int ALC_MONO_SOURCES = GetEnumValue("ALC_MONO_SOURCES");
        public static readonly int ALC_STEREO_SOURCES = GetEnumValue("ALC_STEREO_SOURCES");
    }
}
