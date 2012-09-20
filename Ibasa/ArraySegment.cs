using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace Ibasa
{
    [Serializable]
    public sealed class ArraySegment<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        #region Contract
        [ContractInvariantMethod()]
        void ObjectInvariant()
        {
            Contract.Invariant(Array != null, "Array is null.");
            Contract.Invariant(Offset >= 0, "Offset is less than 0.");
            Contract.Invariant(Count >= 0, "Count is less than zero.");
            Contract.Invariant(Offset + Count <= Array.Length, "Offset and Count do not specify a valid range in Array.");
        }
        #endregion

        #region Properties

        public T[] Array {get; private set;}
		public int Offset {get; private set;}
		public int Count {get; private set; }

        #endregion

        #region Constructors

        public ArraySegment(T[] array)
		{
            Contract.Requires(array != null, "array is null.");

            Contract.Ensures(Array == array);
            Contract.Ensures(Offset == 0);
            Contract.Ensures(Count == array.Length);

			Array = array;
            Offset = 0;
			Count = array.Length;
		}
        public ArraySegment(T[] array, int offset, int count)
		{
            Contract.Requires(array != null, "array is null");
            Contract.Requires(offset >= 0, "offset is less than 0.");
            Contract.Requires(count >= 0, "count is less than 0");
            Contract.Requires(offset + count <= array.Length, "offset and count do not specify a valid range in array.");

            Contract.Ensures(Array == array);
            Contract.Ensures(Offset == offset);
            Contract.Ensures(Count == count);

			Array = array;
			Offset = offset;
			Count = count;
		}
        public ArraySegment(ArraySegment<T> arraySegment, int offset, int count)
        {
            Contract.Requires(arraySegment != null, "array is null");
            Contract.Requires(offset >= 0, "offset is less than 0.");
            Contract.Requires(count >= 0, "count is less than 0");
            Contract.Requires(offset + count <= arraySegment.Count, "offset and count do not specify a valid range in array.");

            Contract.Ensures(Array == arraySegment.Array);
            Contract.Ensures(Offset == arraySegment.Offset + offset);
            Contract.Ensures(Count == count);

            Array = arraySegment.Array;
            Offset = arraySegment.Offset + offset;
            Count = count;
        }

        public ArraySegment<T> Segment(int offset, int count)
		{
            Contract.Requires(offset >= 0, "offset is less than 0.");
            Contract.Requires(count >= 0, "count is less than 0");
            Contract.Requires(offset + count <= Count, "offset and count do not specify a valid range in Array.");

            return new ArraySegment<T>(this, offset, count);
		}

        public static implicit operator ArraySegment<T>(T[] array)
		{
            if (array == null)
                return null;
            return new ArraySegment<T>(array, 0, array.Length);
		}
		#endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T>
        {
            T[] Array;
            int Index;
            int End;

            internal Enumerator(ArraySegment<T> segment)
            {
                Array = segment.Array;
                Index = segment.Offset - 1;
                End = segment.Offset + segment.Count;
            }

            public bool MoveNext()
            {
                return ++Index < End;
            }

            public T Current
            {
                get { return Array[Index]; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

		#region Equality
        public static bool operator ==(ArraySegment<T> left, ArraySegment<T> right)
		{
            if (Object.ReferenceEquals(left, null) && Object.ReferenceEquals(right, null))
                return true;
            if (Object.ReferenceEquals(left, null) || Object.ReferenceEquals(right, null))
                return false;

			return left.Array == right.Array && left.Offset == right.Offset && left.Count == right.Count;
		}
        public static bool operator !=(ArraySegment<T> left, ArraySegment<T> right)
		{
            return !(left == right);
		}
		public override bool Equals(object obj)
		{
            if (obj == null || GetType() != obj.GetType())
                return false;
            return this.Equals((ArraySegment<T>)obj);
		}
        public bool Equals(ArraySegment<T> obj)
		{
            return this == obj;
		}
		public override int GetHashCode()
		{
			return Array.GetHashCode() + Offset.GetHashCode() + Count.GetHashCode();
		}
		#endregion

        #region IList<T> Members

        public int IndexOf(T item)
        {
            int index = System.Array.IndexOf(Array, item, Offset, Count);
            if (index < Offset || index >= Count)
                return -1;
            else
                return index - Offset;
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new InvalidOperationException();
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new InvalidOperationException();
        }

        public T this[int index]
        {
            get
            {
                //Contract.Requires(index > 0);
                //Contract.Requires(index < Count);
                return Array[Offset + index];
            }
            set
            {
               // Contract.Requires(index > 0);
                //Contract.Requires(index < Count); 
                Array[Offset + index] = value;
            }
        }

        #endregion

        #region ICollection<T> Members

        void ICollection<T>.Add(T item)
        {
            throw new InvalidOperationException();
        }

        void ICollection<T>.Clear()
        {
            throw new InvalidOperationException();
        }

        public bool Contains(T item)
        {
            return this.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            //Contract.Requires(array != null, "array is null.");
            //Contract.Requires(arrayIndex >= 0, "arrayIndex is less than zero.");
            System.Array.Copy(Array, Offset, array, arrayIndex, Count);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new InvalidOperationException();
        }

        #endregion
    }
}
