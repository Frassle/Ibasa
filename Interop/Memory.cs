using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.Interop
{
    /// <summary>
    /// A class for native memory operations.
    /// </summary>
    public static class Memory
    {
        public static unsafe int Compare(void* ptr1, void* ptr2, int count)
        {
            ulong* lptr1 = (ulong*)ptr1;
            ulong* lptr2 = (ulong*)ptr2;

            int long_count = count >> 3;
            while (long_count > 0)
            {
                if (lptr1 < lptr2)
                    return -1;
                if (lptr1 > lptr2)
                    return 1;

                lptr1++;
                lptr2++;
                --long_count;
            }
            
            byte* bptr1 = (byte*)lptr1;
            byte* bptr2 = (byte*)lptr2;

            int byte_count = count & 7;
            while (byte_count > 0)
            {
                if (bptr1 < bptr2)
                    return -1;
                if (bptr1 > bptr2)
                    return 1;

                bptr1++;
                bptr2++;
                --byte_count;
            }

            return 0;
        }

        public static unsafe void* Search(void* ptr, int count, byte value)
        {
            byte* bptr = (byte*)ptr;

            while (count > 0)
            {
                if (*bptr == value)
                    return bptr;
                --count;
            }

            return null;
        }

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
