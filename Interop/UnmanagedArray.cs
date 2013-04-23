using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.Interop
{
    public class UnmanagedArray<T> : IDisposable, IEnumerable<T>
        where T : struct
    {
        internal static readonly int SizeOfT = Memory.SizeOf<T>();

        /// <summary>
        /// Gets the number of elements actually contained in the Ibasa.Interop.UnmanagedArray{T}.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the pointer to unmanaged memory where the array is stored.
        /// </summary>
        public IntPtr Pointer { get; private set; }

        /// <summary>
        /// Gets the size in bytes of the array.
        /// </summary>
        public int Size { get { return SizeOfT * Count; } }

        internal int Version = 0;
        private bool OwnsPointer;

        public UnmanagedArray(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", count, "count is less than zero.");

            Count = count;
            Pointer = Marshal.AllocHGlobal(Count * SizeOfT);
            OwnsPointer = true;
            GC.AddMemoryPressure(Count * SizeOfT);
        }

        public UnmanagedArray(IntPtr pointer, int size, bool takeOwnership = false)
        {
            if (pointer == IntPtr.Zero)
                throw new ArgumentOutOfRangeException("pointer", pointer, "pointer is zero.");
            if (size < 0)
                throw new ArgumentOutOfRangeException("size", size, "size is less than zero.");

            Count = size / SizeOfT;
            Pointer = pointer;
            OwnsPointer = takeOwnership;
        }

        ~UnmanagedArray()
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

            if (OwnsPointer)
            {
                GC.RemoveMemoryPressure(Count * SizeOfT);
                Marshal.FreeHGlobal(Pointer);
            }
            Pointer = IntPtr.Zero;
            Count = 0;
            ++Version;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException("index", index, "index is less than zero.");
                if (index >= Count)
                    throw new ArgumentOutOfRangeException("index", Count, "index is greater than or equal to Count.");

                int offset = index * SizeOfT;
                T value;
                Memory.Read<T>(Pointer + offset, out value);
                return value;
            }
            set
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException("index", index, "index is less than zero.");
                if (index >= Count)
                    throw new ArgumentOutOfRangeException("index", Count, "index is greater than or equal to Count.");

                int offset = index * SizeOfT;
                Memory.Write<T>(Pointer + offset, ref value);
                ++Version;
            }
        }

        public struct Enumerator : IEnumerator<T>
        {
            UnmanagedArray<T> Array;
            int Index, Version;

            internal Enumerator(UnmanagedArray<T> array)
            {
                Array = array;
                Index = -1;
                Version = array.Version;
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            /// <return>
            /// The element in the ImmutableArray{T} at the current position of the enumerator.
            /// </return>
            public T Current
            {
                get
                {
                    if (Version != Array.Version)
                    {
                        throw new InvalidOperationException();
                    }
                    return Array[Index];
                }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the ImmutableArray{T}.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false
            /// if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                if (Version != Array.Version)
                {
                    throw new InvalidOperationException();
                }

                if (Index == Array.Count)
                    return false;

                ++Index;
                return Index < Array.Count;
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            void IDisposable.Dispose()
            {
            }

            void System.Collections.IEnumerator.Reset()
            {
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class UnmanagedArray
    {
        public static void Clear<T>(UnmanagedArray<T> array, int index, int count) where T : struct
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", index, "index is less than zero.");
            if (index + count > array.Count)
                throw new ArgumentOutOfRangeException("count", count, "index + count is greater than array.Count.");

            Memory.Fill(array.Pointer + index * UnmanagedArray<T>.SizeOfT, 0, count * UnmanagedArray<T>.SizeOfT);
            ++array.Version;
        }

        public static void Copy<T>(UnmanagedArray<T> source, int sourceIndex,
            UnmanagedArray<T> destination, int destinationIndex, int count) where T : struct
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (sourceIndex < 0)
                throw new ArgumentOutOfRangeException("sourceIndex", sourceIndex, "sourceIndex is less than zero.");
            if (sourceIndex + count > source.Count)
                throw new ArgumentOutOfRangeException("count", count, "sourceIndex + count is greater than source.Count.");

            if (destination == null)
                throw new ArgumentNullException("destination");
            if (destinationIndex < 0)
                throw new ArgumentOutOfRangeException("destinationIndex", destinationIndex, "destinationIndex is less than zero.");
            if (destinationIndex + count > destination.Count)
                throw new ArgumentOutOfRangeException("count", count, "destinationIndex + count is greater than destination.Count.");

            Memory.Copy(
                source.Pointer + sourceIndex * UnmanagedArray<T>.SizeOfT,
                destination.Pointer + destinationIndex * UnmanagedArray<T>.SizeOfT,
                count * UnmanagedArray<T>.SizeOfT);
            ++destination.Version;
        }
    }
}
