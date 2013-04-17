using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.Interop
{
    public class UnmanagedBuffer
    {
        /// <summary>
        /// Gets the pointer to unmanaged memory where the buffer is stored.
        /// </summary>
        public IntPtr Pointer { get; private set; }

        /// <summary>
        /// Gets the size in bytes of the buffer.
        /// </summary>
        public int Size { get; private set;}

        private bool OwnsPointer;

        public UnmanagedBuffer(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException("size", size, "size is less than zero.");

            Size = size;
            Pointer = Marshal.AllocHGlobal(Size);
            OwnsPointer = true;
            GC.AddMemoryPressure(Size);
        }

        public UnmanagedBuffer(IntPtr pointer, int size, bool takeOwnership = false)
        {
            if (pointer == IntPtr.Zero)
                throw new ArgumentOutOfRangeException("pointer", pointer, "pointer is zero.");
            if (size < 0)
                throw new ArgumentOutOfRangeException("size", size, "size is less than zero.");

            Size = size;
            Pointer = pointer;
            OwnsPointer = takeOwnership;
        }

        ~UnmanagedBuffer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Pointer == IntPtr.Zero)
                return;

            if(OwnsPointer)
            {
                GC.RemoveMemoryPressure(Size);
                Marshal.FreeHGlobal(Pointer);
            }
            Pointer = IntPtr.Zero;
        }

        public void Write<T>(int offset, ref T value) where T : struct
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "offset is less than zero.");
            if (offset + Memory.SizeOf<T>() > Size)
                throw new ArgumentOutOfRangeException("offset", offset, "offset + sizeof(T) is greater than Size.");

            Memory.Write<T>(Pointer + offset, ref value);
        }

        public void Write<T>(int offset, T value) where T : struct
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "offset is less than zero.");
            if (offset + Memory.SizeOf<T>() > Size)
                throw new ArgumentOutOfRangeException("offset", offset, "offset + sizeof(T) is greater than Size.");

            Memory.Write<T>(Pointer + offset, ref value);
        }

        public void Read<T>(int offset, out T value) where T : struct
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "offset is less than zero.");
            if (offset + Memory.SizeOf<T>() > Size)
                throw new ArgumentOutOfRangeException("offset", offset, "offset + sizeof(T) is greater than Size.");

            Memory.Read<T>(Pointer + offset, out value);
        }

        public T Read<T>(int offset) where T : struct
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "offset is less than zero.");
            if (offset + Memory.SizeOf<T>() > Size)
                throw new ArgumentOutOfRangeException("offset", offset, "offset + sizeof(T) is greater than Size.");

            T value;
            Memory.Read<T>(Pointer + offset, out value);
            return value;
        }
    }
}
