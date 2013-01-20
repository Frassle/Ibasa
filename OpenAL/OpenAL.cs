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
        public static string GetString(int param)
        {
            return Alc.GetMarshaledString(IntPtr.Zero, param);
        }

        public static List<string> GetStringList(int param)
        {
            return Alc.GetMarshaledStringList(IntPtr.Zero, param);
        }

        public static int GetInteger(int param)
        {
            return Alc.GetInteger(IntPtr.Zero, param);
        }

        public static void GetInteger(int param, int size, int[] data)
        {
            Alc.GetInteger(IntPtr.Zero, param, size, data);
        }

        public static int GetEnumValue(string enumname)
        {
            return Alc.GetEnumValue(IntPtr.Zero, enumname);
        }

        public static bool IsExtensionPresent(string extension)
        {
            return Alc.IsExtensionPresent(IntPtr.Zero, extension);
        }

        public static IntPtr GetProcAddress(string funcname)
        {
            return Alc.GetProcAddress(IntPtr.Zero, funcname);
        }

        internal static void ThrowNullException(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }
        }

        internal static void ThrowError()
        {
            Alc.ThrowError(IntPtr.Zero);
        }

        public static readonly int ALC_FREQUENCY = GetEnumValue("ALC_FREQUENCY");
        public static readonly int ALC_REFRESH = GetEnumValue("ALC_REFRESH");
        public static readonly int ALC_SYNC = GetEnumValue("ALC_SYNC");
        public static readonly int ALC_MONO_SOURCES = GetEnumValue("ALC_MONO_SOURCES");
        public static readonly int ALC_STEREO_SOURCES = GetEnumValue("ALC_STEREO_SOURCES");
    }
}
