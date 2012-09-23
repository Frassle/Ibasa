using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics.Contracts;

namespace Ibasa.Collections
{
    /// <remarks>
    /// Represents a fixed size, cache of objects.
    /// </remarks>
    /// <typeparam name="T">Specifies the type of elements in the cache.</typeparam>
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("Count = {Count} Capacity = {Capacity}")]
    public class Cache<T> : IEnumerable<T>
    {
        #region Fields
        Func<T, T, bool> _eviction;
        T[] _list;
        int _count, _version;
        #endregion

        #region Contracts
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_eviction != null);
            Contract.Invariant(_list != null);
            Contract.Invariant(_list.Length > 0);
            Contract.Invariant(_count >= 0);
            Contract.Invariant(_count < _list.Length);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the number of elements contained in the Cache{T}.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the Cache{T}.
        /// </returns>
        [Pure]
        public int Count
        {
            get
            {
                Contract.Ensures(0 <= Contract.Result<int>());
                Contract.Ensures(Contract.Result<int>() <= Capacity);

                return _count;
            }
        }        
        /// <summary>
        /// Gets or sets the total number of elements the internal data structure can hold.
        /// </summary>
        /// <returns>
        /// The number of elements that the Cache{T} can contain.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Cache{T}.Capacity is set to a value that is less than Cache{T}.Count.
        /// </exception>
        /// <exception cref="System.OutOfMemoryException">
        /// There is not enough memory available on the system.
        /// </exception>
        public int Capacity
        {
            [Pure]
            get
            {
                Contract.Ensures(0 <= Contract.Result<int>()); 

                return _list.Length;
            }
            set
            {
                Contract.Requires(Count <= value);

                ++_version;
                if (value == Capacity)
                {
                    return;
                }

                Array.Resize(ref _list, value);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Cache{T} class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the Cache{T} can contain.</param>
        /// <param name="eviction">Function to implement the eviction stratagy.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">capacity is less than zero.</exception>
        public Cache(int capacity, Func<T, T, bool> eviction)
        {
            Contract.Requires(0 < capacity);
            Contract.Requires(eviction != null);

            _eviction = eviction;
            _list = new T[capacity];
            _count = 0;
        }
        #endregion

        #region Methods
        #region Add/Remove
        /// <summary>
        /// Adds an object to the Cache{T}.
        /// </summary>
        /// <param name="value">
        /// The object to be added to the start of the Cache{T}.
        /// The value can be null for reference types.
        /// </param>
        /// <returns>
        /// Returns the element that was evicited, or default(T) if nothing was evicited.
        /// </returns>
        public T Add(T value)
        {
            Contract.Ensures(Contract.OldValue(Count) <= Count);

            if (Count == Capacity)
            {
                int evict = 0;
                for (int i = 1; i < _count; ++i)
                {
                    if (_eviction(_list[evict], _list[i]))
                        evict = i;
                }

                var evicted = _list[evict];
                _list[evict] = value;
                _version++;
                return evicted;
            }
            else
            {
                _list[_count++] = value;
                _version++;
                return default(T);
            }
        }

        private void RemoveAt(int index)
        {
            int last = _count - 1;
            _list[index] = _list[last];
            _count--;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the Cache{T}.
        /// </summary>
        /// <param name="value">
        /// The object to remove from the Cache{T}. 
        /// The value can be null for reference types.
        /// </param>
        /// <returns>
        /// true if item is removed; otherwise false if item was not found in the System.Collections.Cache{T}.
        /// </returns>
        public bool Remove(T value)
        {
            Contract.Ensures(Count == Contract.OldValue(Count) || Count == Contract.OldValue(Count) - 1);

            int index = Array.IndexOf<T>(_list, value);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }
        /// <summary>
        /// Removes the all the elements that match the conditions defined by the specified
        /// predicate.
        /// </summary>
        /// <param name="match">
        /// The System.Predicate<T> delegate that defines the conditions of the elements
        /// to remove.
        /// </param>
        /// <returns>The number of elements removed from the Cache{T}.</returns>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public int RemoveAll(Predicate<T> match)
        {
            Contract.Requires(match != null);
            Contract.Ensures(Count <= Contract.OldValue(Count));

            int remove_count = 0;
            int i = 0;
            while(i < _count)
            {
                if (match(_list[i]))
                {
                    RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }

            return remove_count;            
        }
        /// <summary>
        /// Removes all objects from the Cache{T}.
        /// </summary>
        public void Clear()
        {
            Contract.Ensures(Count == 0);

            Array.Clear(_list, 0, _count);
            _count = 0;
            _version++;
        }
        #endregion
        #region CopyTo
        /// <summary>
        /// Copies the entire Cache{T} to a compatible one-dimensional
        /// array, starting at the beginning of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements
        /// copied from Cache{T}. The System.Array must have
        /// zero-based indexing.</param>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// The number of elements in the source Cache{T} is greater than the number of 
        /// elements that the destination array can contain.</exception>
        public void CopyTo(T[] array)
        {
            Contract.Requires(array != null);
            Contract.Requires(Count <= array.Length);
            CopyTo(array, 0, Count);
        }
        /// <summary>
        /// Copies the Cache{T} elements to an existing one-dimensional
        /// System.Array, starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements
        /// copied from Cache{T}. The System.Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than zero.</exception>
        /// <exception cref="System.ArgumentException">
        /// The number of elements in the source Cache{T} is greater than the available space 
        /// from arrayIndex to the end of the destination array.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex, Count);
        }
        /// <summary>
        /// Copies a range of elements from the Cache{T} to
        /// a compatible one-dimensional array, starting at the specified index of the
        /// target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements
        /// copied from Cache{T}. The System.Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <param name="count"> The number of elements to copy.</param>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">count is less than 0.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">count is greater than Cache{T}.Count.</exception>
        /// <exception cref="System.ArgumentException">
        /// count is greater than the available space from arrayIndex to the end of the destination array.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex, int count)
        {
            Contract.Requires(array != null, "array is null.");
            Contract.Requires(arrayIndex >= 0, "arrayIndex is less than 0.");
            Contract.Requires(count >= 0, "count is less than 0.");
            Contract.Requires(count <= Count, "count is greater than Cache{T}.Count.");
            Contract.Requires(arrayIndex + count <= array.Length, 
                "count is greater than the available space from arrayIndex to the end of the destination array.");

            Array.Copy(_list, 0, array, arrayIndex, count);
        }
        /// <summary>
        /// Copies the Cache{T} elements to a new array.
        /// </summary>
        /// <returns>A new array containing elements copied from the Cache{T}.</returns>
        public T[] ToArray()
        {
            T[] array = new T[Count];
            CopyTo(array);
            return array;
        }
        #endregion
        #endregion

        #region Enumerator
        /// <summary>
        /// Enumerates the elements of a Cache{T}.
        /// </summary>
        [Serializable]
        public struct Enumerator : IEnumerator<T>
        {
            private Cache<T> cache;
            private int index, version;
            
            internal Enumerator(Cache<T> cache)
            {
                Contract.Requires(cache != null);

                this.cache = cache;
                this.index = -1;
                this.version = cache._version;
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            /// <returns>The element in the Cache{T} at the current position
            /// of the enumerator.</returns>
            public T Current
            {
                get
                {
                    if (version != cache._version) 
                    { 
                        throw new InvalidOperationException(); 
                    }
                    return cache._list[index];
                }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the Cache{T}.
            /// </summary>
            /// <returns> true if the enumerator was successfully advanced to the next element; 
            /// false if the enumerator has passed the end of the collection.</returns>
            /// <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            public bool MoveNext()
            {
                if (version != cache._version)
                {
                    throw new InvalidOperationException(); 
                }

                ++index;
                return index <= cache._count;
            }

            #region Explicit interface methods
            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or
            /// resetting unmanaged resources.
            /// </summary>
            void IDisposable.Dispose()
            {
            }
            /// <summary>
            /// Gets the current element in the collection.
            /// </summary>
            /// <returns>The current element in the collection.</returns>
            object IEnumerator.Current
            {
                get { return Current; }
            }
            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            /// <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            /// <remarks>This always throws.</remarks>
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }
            #endregion
        }

        /// <summary>
        /// Returns an enumerator that iterates through the Cache{T}.
        /// </summary>
        /// <returns>An Cache{T}.Enumerator for the Cache{T}.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
          
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }      
        #endregion
    }
}
