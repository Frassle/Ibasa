using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.Interop
{
    /// <summary>
    /// A class for native memory operations.
    /// </summary>
    public static class Memory
    {
        public static unsafe void Copy(void* src, void* dst, int count)
        {
            throw new NotImplementedException();
        }

        public static unsafe void Fill(void* dst, byte value, int count)
        {
            throw new NotImplementedException();
        }

        public static unsafe void* Write<T>(void* dst, ref T value) where T : struct
        {
            throw new NotImplementedException();
        }

        public static unsafe void* Read<T>(void* src, out T value) where T : struct
        {
            throw new NotImplementedException();
        }

        public static unsafe int SizeOf<T>() where T : struct
        {
            throw new NotImplementedException();
        }

        public static unsafe void Copy(IntPtr src, IntPtr dst, int count)
        {
            Copy(src.ToPointer(), dst.ToPointer(), count);
        }

        public static unsafe void Fill(IntPtr dst, byte value, int count)
        {
            Fill(dst.ToPointer(), value, count);
        }

        public static unsafe IntPtr Write<T>(IntPtr dst, ref T value) where T : struct
        {
            return new IntPtr(Write(dst.ToPointer(), ref value));
        }

        public static unsafe IntPtr Read<T>(IntPtr src, out T value) where T : struct
        {
            return new IntPtr(Read(src.ToPointer(), out value));
        }
    }
}
