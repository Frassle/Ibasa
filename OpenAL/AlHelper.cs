using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenAL
{
    internal static class AlHelper
    {
        internal static void GetAlcError(int error)
        {
            switch (error)
            {
                case Ibasa.OpenAL.Alc.NO_ERROR: return;
                case Ibasa.OpenAL.Alc.INVALID_DEVICE: throw new OpenALException("INVALID_DEVICE");
                case Ibasa.OpenAL.Alc.INVALID_CONTEXT: throw new OpenALException("INVALID_CONTEXT");
                case Ibasa.OpenAL.Alc.INVALID_ENUM: throw new OpenALException("INVALID_ENUM");
                case Ibasa.OpenAL.Alc.INVALID_VALUE: throw new OpenALException("INVALID_VALUE");
                case Ibasa.OpenAL.Alc.OUT_OF_MEMORY: throw new OpenALException("OUT_OF_MEMORY");

                default:
                    throw new OpenALException(string.Format("Unknown error: {0}", error));
            }
        }

        internal static void GetAlError(int error)
        {
            switch (error)
            {
                case Ibasa.OpenAL.Al.NO_ERROR: return;
                case Ibasa.OpenAL.Al.INVALID_NAME: throw new OpenALException("INVALID_NAME");
                case Ibasa.OpenAL.Al.INVALID_ENUM: throw new OpenALException("INVALID_ENUM");
                case Ibasa.OpenAL.Al.INVALID_VALUE: throw new OpenALException("INVALID_VALUE");
                case Ibasa.OpenAL.Al.INVALID_OPERATION: throw new OpenALException("INVALID_OPERATION");
                case Ibasa.OpenAL.Al.OUT_OF_MEMORY: throw new OpenALException("OUT_OF_MEMORY");

                default:
                    throw new OpenALException(string.Format("Unknown error: {0}", error));
            }
        }

        internal unsafe static void StringToAnsi(string str, byte* chars, int length)
        {
            fixed (char* ptr = str)
            {
                Encoding.ASCII.GetBytes(ptr, str.Length, chars, length);
                chars[length] = 0;
            }
        }

        internal static unsafe string MarshalString(byte* ptr)
        {
            return Marshal.PtrToStringAnsi(new IntPtr(ptr));
        }

        internal static unsafe List<string> MarshalStringList(byte* ptr)
        {
            List<string> result = new List<string>();

            while (*ptr != 0)
            {
                var str = MarshalString(ptr);
                result.Add(str);
                ptr += str.Length + 1;
            }

            return result;
        }

        internal static void ThrowNullException(uint id)
        {
            if (id == 0)
            {
                throw new NullReferenceException();
            }
        }

        internal static void ThrowNullException(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }
        }
    }
}
