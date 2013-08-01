using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Ibasa.Collections
{
    /// <summary>
    /// Represents a double ended queue of objects that can be accessed by index.
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the deque.</typeparam>
    public sealed class Deque<T> : IList<T>
    {
        // Stores items in Blocks, each block holds 16 items.
        // Blocks are structs so cheep on GC, keeps _list items close in memory.

        const int BlockSize = 16;

        class Block
        {
            T Item0;
            T Item1;
            T Item2;
            T Item3;
            T Item4;
            T Item5;
            T Item6;
            T Item7;
            T Item8;
            T Item9;
            T ItemA;
            T ItemB;
            T ItemC;
            T ItemD;
            T ItemE;
            T ItemF;

            public T this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0x0:
                            return Item0;
                        case 0x1:
                            return Item1;
                        case 0x2:
                            return Item2;
                        case 0x3:
                            return Item3;
                        case 0x4:
                            return Item4;
                        case 0x5:
                            return Item5;
                        case 0x6:
                            return Item6;
                        case 0x7:
                            return Item7;
                        case 0x8:
                            return Item8;
                        case 0x9:
                            return Item9;
                        case 0xA:
                            return ItemA;
                        case 0xB:
                            return ItemB;
                        case 0xC:
                            return ItemC;
                        case 0xD:
                            return ItemD;
                        case 0xE:
                            return ItemE;
                        case 0xF:
                            return ItemF;
                    }
                    return default(T);
                }
                set
                {
                    switch (index)
                    {
                        case 0x0:
                            Item0 = value; break;
                        case 0x1:
                            Item1 = value; break;
                        case 0x2:
                            Item2 = value; break;
                        case 0x3:
                            Item3 = value; break;
                        case 0x4:
                            Item4 = value; break;
                        case 0x5:
                            Item5 = value; break;
                        case 0x6:
                            Item6 = value; break;
                        case 0x7:
                            Item7 = value; break;
                        case 0x8:
                            Item8 = value; break;
                        case 0x9:
                            Item9 = value; break;
                        case 0xA:
                            ItemA = value; break;
                        case 0xB:
                            ItemB = value; break;
                        case 0xC:
                            ItemC = value; break;
                        case 0xD:
                            ItemD = value; break;
                        case 0xE:
                            ItemE = value; break;
                        case 0xF:
                            ItemF = value; break;
                    }
                }
            }
        }

        #region Fields
        Block[] _list;
        int _front, _count, _version;
        #endregion

        #region Contracts
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_list != null);
            Contract.Invariant(_front >= 0);
            Contract.Invariant(_front < _list.Length * BlockSize);
            Contract.Invariant(_count >= 0);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the number of elements contained in the Deque{T}.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the Deque{T}.
        /// </returns>
        public int Count
        {
            get
            {
                return _count;
            }
        }
        /// <summary>
        /// Gets or sets the total number of elements the internal data structure can hold.
        /// </summary>
        /// <returns>
        /// The number of elements that the Deque{T} can contain.
        ///</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Deque{T}.Capacity is 
        /// set to a value that is less than Deque{T}.Count.</exception>
        /// <exception cref="System.OutOfMemoryException">There is not enough memory available on the system.</exception>
        public int Capacity
        {
            get { return _list.Length * BlockSize; }
            set { GrowBack(value); }
        }
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index is equal to or greater than Deque{T}.Count.</exception>
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "Index was out of range. Must be non-negative and less than the size of the collection.", "index");
                }

                int block, offset;
                Translate(index, out block, out offset);

                return _list[block][offset];
            }
            set
            {
                if (index >= Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "Index was out of range. Must be non-negative and less than the size of the collection.", "index");
                }

                int block, offset;
                Translate(index, out block, out offset);
                _list[block][offset] = value;
                _version++;
            }
        }
        /// <summary>
        /// Gets the first element of the Deque{T}.
        /// </summary>
        /// <returns>The first element of the Deque{T}.</returns>
        /// <exception cref="System.InvalidOperationException">The Deque{T} is empty.</exception>
        public T First
        {
            get
            {
                if (_count == 0)
                {
                    throw new InvalidOperationException("Deque empty.");
                }

                int block, offset;
                Translate(0, out block, out offset);

                return _list[block][offset];
            }
        }
        /// <summary>
        /// Gets the last element of the Deque{T}.
        /// </summary>
        /// <returns>The last element of the Deque{T}.</returns>
        /// <exception cref="System.InvalidOperationException">The Deque{T} is empty.</exception>
        public T Last
        {
            get
            {
                if (_count == 0)
                {
                    throw new InvalidOperationException("Deque empty.");
                }

                int block, offset;
                Translate(Count - 1, out block, out offset);

                return _list[block][offset];
            }
        }
        #endregion

        #region Constructors
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.List<T> class
        //     that is empty and has the default initial capacity.
        public Deque()
        {
            _list = new Block[0];
            _count = _front = 0;
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.List<T> class
        //     that contains elements copied from the specified collection and has sufficient
        //     capacity to accommodate the number of elements copied.
        //
        // Parameters:
        //   collection:
        //     The collection whose elements are copied to the new list.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     collection is null.
        public Deque(IEnumerable<T> collection)
            : this(8)
        {
            Contract.Requires<ArgumentException>(collection != null);

            foreach (var item in collection)
            {
                AddLast(item);
            }
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.List<T> class
        //     that is empty and has the specified initial capacity.
        //
        // Parameters:
        //   capacity:
        //     The number of elements that the new list can initially store.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     capacity is less than 0.
        public Deque(int capacity)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity >= 0, "capacity is less than zero.");
            _list = new Block[(capacity + (BlockSize - 1)) / BlockSize];
            _count = _front = 0;
        }
        #endregion

        #region Methods
        void Translate(int index, out int block, out int offset)
        {
            index += _front;

            block = index / BlockSize;
            offset = index % BlockSize;
        }
        void GrowBack(int capacity)
        {
            if (capacity < Count)
            {
                throw new ArgumentOutOfRangeException(
                    "Capacity must be set to Count or greater.", "value");
            }
            ++_version;
            if (capacity == Capacity)
            {
                return;
            }

            Block[] newList = new Block[(capacity + (BlockSize - 1)) / BlockSize];
            _list.CopyTo(newList, 0);
            _list = newList;
        }
        void GrowFront(int capacity)
        {
            if (capacity < Count)
            {
                throw new ArgumentOutOfRangeException(
                    "Capacity must be set to Count or greater.", "value");
            }
            ++_version;
            if (capacity == Capacity)
            {
                return;
            }

            Block[] newList = new Block[(capacity + (BlockSize - 1)) / BlockSize];

            int offset = newList.Length - _list.Length;

            _list.CopyTo(newList, offset);
            _list = newList;
            _front += offset * BlockSize;
        }
        Block SafeGet(int block_index)
        {
            var block = _list[block_index];
            if (block == null)
            {
                block = new Block();
                _list[block_index] = block;
            }
            return block;
        }

        #region Add/Remove
        /// <summary>
        /// Adds an object to the start of the Deque{T}.
        /// </summary>
        /// <param name="value">The object to be added to the start of the Deque{T}.
        /// The value can be null for reference types.</param>
        public void AddFirst(T value)
        {
            if (_front == 0)
            {
                GrowFront((int)(Capacity == 0 ? BlockSize : Capacity * 1.5));
            }

            int block, offset;
            Translate(-1, out block, out offset);

            SafeGet(block)[offset] = value;
            --_front;
            ++_count;
            ++_version;
        }
        /// <summary>
        /// Adds an object to the end of the Deque{T}.
        /// </summary>
        /// <param name="value">The object to be added to the end of the Deque{T}.
        /// The value can be null for reference types.</param>
        public void AddLast(T value)
        {
            int block, offset;
            Translate(Count, out block, out offset);

            if (block >= _list.Length)
            {
                GrowBack((int)(Capacity == 0 ? BlockSize : Capacity * 1.5));
            }

            SafeGet(block)[offset] = value;
            ++_count;
            ++_version;
        }
        /// <summary>
        /// Inserts an element into the Deque{T} at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index is greater than Deque{T}.Count.</exception>
        public void Insert(int index, T item)
        {
            if (index == 0)
            {
                AddFirst(item);
            }
            else if (index == Count)
            {
                AddLast(item);
            }
            else
            {
                AddLast(default(T));
                for (int i = Count - 1; i > index; --i)
                {
                    this[i] = this[i - 1];
                }
                this[index] = item;
            }
        }
        /// <summary>
        /// Removes the element at the start of the Deque{T}.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The Deque{T} is empty.</exception>
        public void RemoveFirst()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Deque empty.");
            }

            int block, offset;
            Translate(0, out block, out offset);

            _list[block][offset] = default(T);
            --_count;
            ++_front;
            ++_version;
        }
        /// <summary>
        /// Removes the element at the end of the Deque{T}.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The Deque{T} is empty.</exception>
        public void RemoveLast()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Deque empty.");
            }

            int block, offset;
            Translate(Count - 1, out block, out offset);

            _list[block][offset] = default(T);
            --_count;
            ++_version;
        }
        /// <summary>
        /// Removes the first occurrence of a specific object from the Deque{T}.
        /// </summary>
        /// <param name="value">The object to remove from the Deque{T}. The value
        /// can be null for reference types.</param>
        /// <returns>true if item is successfully removed; otherwise, false. This method also
        /// returns false if item was not found in the System.Collections.Generic.List<T>.</returns>
        public bool Remove(T value)
        {
            if (_count == 0)
            {
                return false;
            }

            var index = IndexOf(value);

            if (index == -1)
            {
                return false;
            }
            else
            {
                RemoveAt(index);
                return true;
            }
        }
        /// <summary>
        /// Removes the element at the specified index of the Deque{T}.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index is equal to or greater than Deque{T}.Count.</exception>
        public void RemoveAt(int index)
        {
            if (_count == 0)
            {
                throw new ArgumentException("Deque empty.");
            }
            
            if (index == 0)
            {
                RemoveFirst();
            }
            else if (index == Count - 1)
            {
                RemoveLast();
            }
            else
            {
                for (int i = index; i < Count - 1; ++i)
                {
                    this[i] = this[i + 1];
                }
                RemoveLast();
            }
        }
        /// <summary>
        /// Removes a range of elements from the Deque{T}.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
        /// <param name="count">The number of elements to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">count is less than 0.</exception>
        /// <exception cref="System.ArgumentException">
        /// index and count do not denote a valid range of elements in the Deque{T}.
        /// </exception>
        public void RemoveRange(int index, int count)
        {
            if (_count == 0)
            {
                throw new ArgumentException("Deque empty.");
            }

            if (index == 0)
            {
                while (count-- > 0)
                {
                    RemoveFirst();
                }
            }
            else if (index == Count - count)
            {
                while (count-- > 0)
                {
                    RemoveLast();
                }
            }
            else
            {
                for (int i = index; i < Count - count; ++i)
                {
                    this[i] = this[i + count];
                }
                while (count-- > 0)
                {
                    RemoveLast();
                }
            }
        }
        /// <summary>
        /// Removes the all the elements that match the conditions defined by the specified
        /// predicate.
        /// </summary>
        /// <param name="match">
        /// The System.Predicate<T> delegate that defines the conditions of the elements
        /// to remove.
        /// </param>
        /// <returns>The number of elements removed from the Deque{T}.</returns>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public int RemoveAll(Predicate<T> match)
        {
            int count = 0;
            for (int i = 0; i < Count; )
            {
                if (match(this[i]))
                {
                    RemoveAt(i);
                    ++count;
                }
                else
                {
                    ++i;
                }
            }
            return count;
        }
        /// <summary>
        /// Removes all objects from the Deque{T}.
        /// </summary>
        public void Clear()
        {
            int first_block, last_block, offset;

            Translate(0, out first_block, out offset);
            Translate(Count - 1, out last_block, out offset);

            Array.Clear(_list, first_block, last_block - first_block);
            _count = _front = 0;
        }
        #endregion
        #region Find/IndexOf
        #region Find
        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the first occurrence within the entire System.Collections.Generic.List<T>.
        //
        // Parameters:
        //   match:
        //     The System.Predicate<T> delegate that defines the conditions of the element
        //     to search for.
        //
        // Returns:
        //     The first element that matches the conditions defined by the specified predicate,
        //     if found; otherwise, the default value for type T.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     match is null.
        public T Find(Predicate<T> match)
        {
            foreach (var item in this)
            {
                if (match(item))
                    return item;
            }
            return default(T);
        }
        //
        // Summary:
        //     Retrieves all the elements that match the conditions defined by the specified
        //     predicate.
        //
        // Parameters:
        //   match:
        //     The System.Predicate<T> delegate that defines the conditions of the elements
        //     to search for.
        //
        // Returns:
        //     A System.Collections.Generic.List<T> containing all the elements that match
        //     the conditions defined by the specified predicate, if found { throw new NotImplementedException(); } otherwise, an
        //     empty System.Collections.Generic.List<T>.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     match is null.
        public List<T> FindAll(Predicate<T> match)
        {
            List<T> list = new List<T>();
            foreach (var item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the last occurrence within the entire System.Collections.Generic.List<T>.
        //
        // Parameters:
        //   match:
        //     The System.Predicate<T> delegate that defines the conditions of the element
        //     to search for.
        //
        // Returns:
        //     The last element that matches the conditions defined by the specified predicate,
        //     if found { throw new NotImplementedException(); } otherwise, the default value for type T.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     match is null.
        public T FindLast(Predicate<T> match)
        {
            for(int i = Count - 1; i >= 0; --i)
            {
                var item = this[i];
                if (match(item))
                    return item;
            }
            return default(T);
        }
        #endregion
        #region FindIndex
        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the zero-based index of the first occurrence within
        //     the entire System.Collections.Generic.List<T>.
        //
        // Parameters:
        //   match:
        //     The System.Predicate<T> delegate that defines the conditions of the element
        //     to search for.
        //
        // Returns:
        //     The zero-based index of the first occurrence of an element that matches the
        //     conditions defined by match, if found; otherwise, –1.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     match is null.
        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < Count; ++i)
            {
                var item = this[i];
                if (match(item))
                    return i;
            }
            return -1;
        }
        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the zero-based index of the first occurrence within
        //     the range of elements in the System.Collections.Generic.List<T> that extends
        //     from the specified index to the last element.
        //
        // Parameters:
        //   startIndex:
        //     The zero-based starting index of the search.
        //
        //   match:
        //     The System.Predicate<T> delegate that defines the conditions of the element
        //     to search for.
        //
        // Returns:
        //     The zero-based index of the first occurrence of an element that matches the
        //     conditions defined by match, if found { throw new NotImplementedException(); } otherwise, –1.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     match is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is outside the range of valid indexes for the System.Collections.Generic.List<T>.
        public int FindIndex(int startIndex, Predicate<T> match)
        {
            for (int i = startIndex; i < Count; ++i)
            {
                var item = this[i];
                if (match(item))
                    return i;
            }
            return -1;
        }
        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the zero-based index of the first occurrence within
        //     the range of elements in the System.Collections.Generic.List<T> that starts
        //     at the specified index and contains the specified number of elements.
        //
        // Parameters:
        //   startIndex:
        //     The zero-based starting index of the search.
        //
        //   count:
        //     The number of elements in the section to search.
        //
        //   match:
        //     The System.Predicate<T> delegate that defines the conditions of the element
        //     to search for.
        //
        // Returns:
        //     The zero-based index of the first occurrence of an element that matches the
        //     conditions defined by match, if found { throw new NotImplementedException(); } otherwise, –1.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     match is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is outside the range of valid indexes for the System.Collections.Generic.List<T>.-or-count
        //     is less than 0.-or-startIndex and count do not specify a valid section in
        //     the System.Collections.Generic.List<T>.
        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            for (int i = startIndex; i < startIndex + count; ++i)
            {
                var item = this[i];
                if (match(item))
                    return i;
            }
            return -1;
        }
        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the zero-based index of the last occurrence within
        //     the entire System.Collections.Generic.List<T>.
        //
        // Parameters:
        //   match:
        //     The System.Predicate<T> delegate that defines the conditions of the element
        //     to search for.
        //
        // Returns:
        //     The zero-based index of the last occurrence of an element that matches the
        //     conditions defined by match, if found { throw new NotImplementedException(); } otherwise, –1.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     match is null.
        public int FindLastIndex(Predicate<T> match)
        {
            for (int i = Count - 1; i >= 0; --i)
            {
                var item = this[i];
                if (match(item))
                    return i;
            }
            return -1;
        }
        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the zero-based index of the last occurrence within
        //     the range of elements in the System.Collections.Generic.List<T> that extends
        //     from the first element to the specified index.
        //
        // Parameters:
        //   startIndex:
        //     The zero-based starting index of the backward search.
        //
        //   match:
        //     The System.Predicate<T> delegate that defines the conditions of the element
        //     to search for.
        //
        // Returns:
        //     The zero-based index of the last occurrence of an element that matches the
        //     conditions defined by match, if found { throw new NotImplementedException(); } otherwise, –1.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     match is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is outside the range of valid indexes for the System.Collections.Generic.List<T>.
        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            for (int i = Count - 1; i >= startIndex; --i)
            {
                var item = this[i];
                if (match(item))
                    return i;
            }
            return -1;
        }
        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the zero-based index of the last occurrence within
        //     the range of elements in the System.Collections.Generic.List<T> that contains
        //     the specified number of elements and ends at the specified index.
        //
        // Parameters:
        //   startIndex:
        //     The zero-based starting index of the backward search.
        //
        //   count:
        //     The number of elements in the section to search.
        //
        //   match:
        //     The System.Predicate<T> delegate that defines the conditions of the element
        //     to search for.
        //
        // Returns:
        //     The zero-based index of the last occurrence of an element that matches the
        //     conditions defined by match, if found { throw new NotImplementedException(); } otherwise, –1.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     match is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is outside the range of valid indexes for the System.Collections.Generic.List<T>.-or-count
        //     is less than 0.-or-startIndex and count do not specify a valid section in
        //     the System.Collections.Generic.List<T>.
        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            for (int i = startIndex + count - 1; i >= startIndex; --i)
            {
                var item = this[i];
                if (match(item))
                    return i;
            }
            return -1;
        }
        #endregion
        #region IndexOf
        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the
        //     first occurrence within the entire System.Collections.Generic.List<T>.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List<T>. The value
        //     can be null for reference types.
        //
        // Returns:
        //     The zero-based index of the first occurrence of item within the entire System.Collections.Generic.List<T>,
        //     if found; otherwise, –1.
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (item.Equals(this[i]))
                    return i;
            }
            return -1;
        }
        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the
        //     first occurrence within the range of elements in the System.Collections.Generic.List<T>
        //     that extends from the specified index to the last element.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List<T>. The value
        //     can be null for reference types.
        //
        //   index:
        //     The zero-based starting index of the search. 0 (zero) is valid in an empty
        //     list.
        //
        // Returns:
        //     The zero-based index of the first occurrence of item within the range of
        //     elements in the System.Collections.Generic.List<T> that extends from index
        //     to the last element, if found; otherwise, –1.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is outside the range of valid indexes for the System.Collections.Generic.List<T>.
        public int IndexOf(T item, int index)
        {
            for (int i = index; i < Count; ++i)
            {
                if (item.Equals(this[i]))
                    return i;
            }
            return -1;
        }
        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the
        //     first occurrence within the range of elements in the System.Collections.Generic.List<T>
        //     that starts at the specified index and contains the specified number of elements.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List<T>. The value
        //     can be null for reference types.
        //
        //   index:
        //     The zero-based starting index of the search. 0 (zero) is valid in an empty
        //     list.
        //
        //   count:
        //     The number of elements in the section to search.
        //
        // Returns:
        //     The zero-based index of the first occurrence of item within the range of
        //     elements in the System.Collections.Generic.List<T> that starts at index and
        //     contains count number of elements, if found { throw new NotImplementedException(); } otherwise, –1.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is outside the range of valid indexes for the System.Collections.Generic.List<T>.-or-count
        //     is less than 0.-or-index and count do not specify a valid section in the
        //     System.Collections.Generic.List<T>.
        public int IndexOf(T item, int index, int count)
        {
            for (int i = index; i < index + count; ++i)
            {
                if (item.Equals(this[i]))
                    return i;
            }
            return -1;
        }
        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the
        //     last occurrence within the entire System.Collections.Generic.List<T>.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List<T>. The value
        //     can be null for reference types.
        //
        // Returns:
        //     The zero-based index of the last occurrence of item within the entire the
        //     System.Collections.Generic.List<T>, if found { throw new NotImplementedException(); } otherwise, –1.
        public int LastIndexOf(T item)
        {
            for (int i = Count - 1; i >= 0; --i)
            {
                if (item.Equals(this[i]))
                    return i;
            }
            return -1;
        }
        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the
        //     last occurrence within the range of elements in the System.Collections.Generic.List<T>
        //     that extends from the first element to the specified index.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List<T>. The value
        //     can be null for reference types.
        //
        //   index:
        //     The zero-based starting index of the backward search.
        //
        // Returns:
        //     The zero-based index of the last occurrence of item within the range of elements
        //     in the System.Collections.Generic.List<T> that extends from the first element
        //     to index, if found { throw new NotImplementedException(); } otherwise, –1.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is outside the range of valid indexes for the System.Collections.Generic.List<T>.
        public int LastIndexOf(T item, int index)
        {
            for (int i = Count - 1; i >= index; --i)
            {
                if (item.Equals(this[i]))
                    return i;
            }
            return -1;
        }
        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the
        //     last occurrence within the range of elements in the System.Collections.Generic.List<T>
        //     that contains the specified number of elements and ends at the specified
        //     index.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List<T>. The value
        //     can be null for reference types.
        //
        //   index:
        //     The zero-based starting index of the backward search.
        //
        //   count:
        //     The number of elements in the section to search.
        //
        // Returns:
        //     The zero-based index of the last occurrence of item within the range of elements
        //     in the System.Collections.Generic.List<T> that contains count number of elements
        //     and ends at index, if found { throw new NotImplementedException(); } otherwise, –1.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is outside the range of valid indexes for the System.Collections.Generic.List<T>.-or-count
        //     is less than 0.-or-index and count do not specify a valid section in the
        //     System.Collections.Generic.List<T>.
        public int LastIndexOf(T item, int index, int count)
        {
            for (int i = index + count - 1; i >= index; --i)
            {
                if (item.Equals(this[i]))
                    return i;
            }
            return -1;
        }
        #endregion
        #endregion
        #region Search/Sort
        #region BinarySearch
        //
        // Summary:
        //     Searches the entire sorted System.Collections.Generic.List<T> for an element
        //     using the default comparer and returns the zero-based index of the element.
        //
        // Parameters:
        //   item:
        //     The object to locate. The value can be null for reference types.
        //
        // Returns:
        //     The zero-based index of item in the sorted System.Collections.Generic.List<T>,
        //     if item is found; otherwise, a negative number that is the bitwise complement
        //     of the index of the next element that is larger than item or, if there is
        //     no larger element, the bitwise complement of System.Collections.Generic.List<T>.Count.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The default comparer System.Collections.Generic.Comparer<T>.Default cannot
        //     find an implementation of the System.IComparable<T> generic interface or
        //     the System.IComparable interface for type T.
        public int BinarySearch(T item) { throw new NotImplementedException(); }
        //
        // Summary:
        //     Searches the entire sorted System.Collections.Generic.List<T> for an element
        //     using the specified comparer and returns the zero-based index of the element.
        //
        // Parameters:
        //   item:
        //     The object to locate. The value can be null for reference types.
        //
        //   comparer:
        //     The System.Collections.Generic.IComparer<T> implementation to use when comparing
        //     elements.-or-null to use the default comparer System.Collections.Generic.Comparer<T>.Default.
        //
        // Returns:
        //     The zero-based index of item in the sorted System.Collections.Generic.List<T>,
        //     if item is found; otherwise, a negative number that is the bitwise complement
        //     of the index of the next element that is larger than item or, if there is
        //     no larger element, the bitwise complement of System.Collections.Generic.List<T>.Count.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     comparer is null, and the default comparer System.Collections.Generic.Comparer<T>.Default
        //     cannot find an implementation of the System.IComparable<T> generic interface
        //     or the System.IComparable interface for type T.
        public int BinarySearch(T item, IComparer<T> comparer) { throw new NotImplementedException(); }
        //
        // Summary:
        //     Searches a range of elements in the sorted System.Collections.Generic.List<T>
        //     for an element using the specified comparer and returns the zero-based index
        //     of the element.
        //
        // Parameters:
        //   index:
        //     The zero-based starting index of the range to search.
        //
        //   count:
        //     The length of the range to search.
        //
        //   item:
        //     The object to locate. The value can be null for reference types.
        //
        //   comparer:
        //     The System.Collections.Generic.IComparer<T> implementation to use when comparing
        //     elements, or null to use the default comparer System.Collections.Generic.Comparer<T>.Default.
        //
        // Returns:
        //     The zero-based index of item in the sorted System.Collections.Generic.List<T>,
        //     if item is found; otherwise, a negative number that is the bitwise complement
        //     of the index of the next element that is larger than item or, if there is
        //     no larger element, the bitwise complement of System.Collections.Generic.List<T>.Count.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than 0.-or-count is less than 0.
        //
        //   System.ArgumentException:
        //     index and count do not denote a valid range in the System.Collections.Generic.List<T>.
        //
        //   System.InvalidOperationException:
        //     comparer is null, and the default comparer System.Collections.Generic.Comparer<T>.Default
        //     cannot find an implementation of the System.IComparable<T> generic interface
        //     or the System.IComparable interface for type T.
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer) { throw new NotImplementedException(); }
        #endregion
        #region Sort
        //
        // Summary:
        //     Sorts the elements in the entire System.Collections.Generic.List<T> using
        //     the default comparer.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The default comparer System.Collections.Generic.Comparer<T>.Default cannot
        //     find an implementation of the System.IComparable<T> generic interface or
        //     the System.IComparable interface for type T.
        public void Sort() { throw new NotImplementedException(); }
        //
        // Summary:
        //     Sorts the elements in the entire System.Collections.Generic.List<T> using
        //     the specified System.Comparison<T>.
        //
        // Parameters:
        //   comparison:
        //     The System.Comparison<T> to use when comparing elements.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     comparison is null.
        //
        //   System.ArgumentException:
        //     The implementation of comparison caused an error during the sort. For example,
        //     comparison might not return 0 when comparing an item with itself.
        public void Sort(Comparison<T> comparison) { throw new NotImplementedException(); }
        //
        // Summary:
        //     Sorts the elements in the entire System.Collections.Generic.List<T> using
        //     the specified comparer.
        //
        // Parameters:
        //   comparer:
        //     The System.Collections.Generic.IComparer<T> implementation to use when comparing
        //     elements, or null to use the default comparer System.Collections.Generic.Comparer<T>.Default.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     comparer is null, and the default comparer System.Collections.Generic.Comparer<T>.Default
        //     cannot find implementation of the System.IComparable<T> generic interface
        //     or the System.IComparable interface for type T.
        //
        //   System.ArgumentException:
        //     The implementation of comparer caused an error during the sort. For example,
        //     comparer might not return 0 when comparing an item with itself.
        public void Sort(IComparer<T> comparer) { throw new NotImplementedException(); }
        //
        // Summary:
        //     Sorts the elements in a range of elements in System.Collections.Generic.List<T>
        //     using the specified comparer.
        //
        // Parameters:
        //   index:
        //     The zero-based starting index of the range to sort.
        //
        //   count:
        //     The length of the range to sort.
        //
        //   comparer:
        //     The System.Collections.Generic.IComparer<T> implementation to use when comparing
        //     elements, or null to use the default comparer System.Collections.Generic.Comparer<T>.Default.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than 0.-or-count is less than 0.
        //
        //   System.ArgumentException:
        //     index and count do not specify a valid range in the System.Collections.Generic.List<T>.-or-The
        //     implementation of comparer caused an error during the sort. For example,
        //     comparer might not return 0 when comparing an item with itself.
        //
        //   System.InvalidOperationException:
        //     comparer is null, and the default comparer System.Collections.Generic.Comparer<T>.Default
        //     cannot find implementation of the System.IComparable<T> generic interface
        //     or the System.IComparable interface for type T.
        public void Sort(int index, int count, IComparer<T> comparer) { throw new NotImplementedException(); }
        #endregion
        #endregion
        #region Reverse
        //
        // Summary:
        //     Reverses the order of the elements in the entire System.Collections.Generic.List<T>.
        public void Reverse()
        {
            Reverse(0, Count);
        }
        //
        // Summary:
        //     Reverses the order of the elements in the specified range.
        //
        // Parameters:
        //   index:
        //     The zero-based starting index of the range to reverse.
        //
        //   count:
        //     The number of elements in the range to reverse.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than 0.-or-count is less than 0.
        //
        //   System.ArgumentException:
        //     index and count do not denote a valid range of elements in the System.Collections.Generic.List<T>.
        public void Reverse(int index, int count) { throw new NotImplementedException(); }
        #endregion
        #region Contains
        /// <summary>
        /// Determines whether the Deque{T} contains elements that match the conditions 
        /// defined by the specified predicate.
        /// </summary>
        /// <param name="match">The System.Predicate{T} delegate that defines the conditions 
        /// of the elements to search for.</param>
        /// <returns>true if the Deque{T} contains one or more elements that match the conditions 
        /// defined by the specified predicate; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public bool Exists(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match is null.");

            foreach (T t in this)
            {
                if (match(t))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Determines whether every element in the Deque{T} matches the conditions 
        /// defined by the specified predicate.
        /// </summary>
        /// <param name="match">The System.Predicate{T} delegate that defines the conditions 
        /// to check against the elements.</param>
        /// <returns>true if every element in the Deque{T} matches the conditions 
        /// defined by the specified predicate; otherwise, false. 
        /// If the deque has no elements, the return value is true.</returns>
        /// <exception cref="System.ArgumentNullException">match is null.</exception>
        public bool TrueForAll(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match is null.");

            foreach (T t in this)
            {
                if (!match(t))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Determines whether an element is in the Deque{T}.
        /// </summary>
        /// <param name="item">The object to locate in the Deque{T}. The value
        /// can be null for reference types.</param>
        /// <returns>true if item is found in the Deque{T} otherwise,
        /// false.</returns>
        public bool Contains(T item)
        {
            foreach (T t in this)
            {
                if (item.Equals(t))
                    return true;
            }
            return false;
        }
        #endregion
        #region CopyTo
        /// <summary>
        /// Copies the entire Deque{T} to a compatible one-dimensional
        /// array, starting at the beginning of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements
        /// copied from Deque{T}. The System.Array must have
        /// zero-based indexing.</param>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentException">The number of elements in the source Deque{T}
        /// is greater than the number of elements that the destination array can contain.</exception>
        public void CopyTo(T[] array)
        {
            Contract.Requires<ArgumentNullException>(array != null);
            Contract.Requires<ArgumentException>(Count <= array.Length);
            CopyTo(array, 0, Count);
        }
        /// <summary>
        /// Copies the Deque{T} elements to an existing one-dimensional
        /// System.Array, starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements
        /// copied from Deque{T}. The System.Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than zero.</exception>
        /// <exception cref="System.ArgumentException">The number of elements in the source 
        /// Deque{T} is greater than the available space from arrayIndex 
        /// to the end of the destination array.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex, Count);
        }
        /// <summary>
        /// Copies a range of elements from the Deque{T} to
        /// a compatible one-dimensional array, starting at the specified index of the
        /// target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements
        /// copied from Deque{T}. The System.Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <param name="count"> The number of elements to copy.</param>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">count is less than 0.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">count is greater than 
        /// Deque{T}.Count.</exception>
        /// <exception cref="System.ArgumentException"> count is greater than the available space from arrayIndex
        /// to the end of the destination array. </exception>
        public void CopyTo(T[] array, int arrayIndex, int count)
        {
            int i = 0;
            foreach (var item in this)
            {
                if (count-- == 0)
                    break;

                array[arrayIndex + i++] = item;
            }
        }
        /// <summary>
        /// Copies the Deque{T} elements to a new array.
        /// </summary>
        /// <returns>A new array containing elements copied from the Deque{T}.</returns>
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
        /// Enumerates the elements of a Deque{T}.
        /// </summary>
        [Serializable]
        public struct Enumerator : IEnumerator<T>
        {
            private Deque<T> deque;
            private int index, version;

            internal Enumerator(Deque<T> deque)
            {
                Contract.Requires(deque != null);

                this.deque = deque;
                this.index = -1;
                this.version = deque._version;
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            /// <returns>The element in the Deque{T} at the current position
            /// of the enumerator.</returns>
            public T Current
            {
                get
                {
                    if (version != deque._version)
                    {
                        throw new InvalidOperationException();
                    }
                    return deque[index];
                }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the Deque{T}.
            /// </summary>
            /// <returns> true if the enumerator was successfully advanced to the next element; 
            /// false if the enumerator has passed the end of the collection.</returns>
            /// <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            public bool MoveNext()
            {
                if (version != deque._version)
                {
                    throw new InvalidOperationException();
                }

                if (index < deque.Count)
                {
                    ++index;
                    return (index != deque.Count);
                }
                else
                {
                    return false;
                }
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
        /// Returns an enumerator that iterates through the Deque{T}.
        /// </summary>
        /// <returns>An Deque{T}.Enumerator for the Deque{T}.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
        #endregion

        #region Explicit interface methods
        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A System.Collections.Generic.IEnumerator{T} that can be used to iterate through the collection.</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An System.Collections.IEnumerator object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
        #region ICollection
        /// <summary>
        /// Adds an item to the Deque{T}.
        /// </summary>
        /// <param name="item">The object to add to the Deque{T}.</param>
        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }
        /// <summary>
        /// Gets a value indicating whether the Deque{T} is read-only.
        /// </summary>
        /// <returns>
        /// true if the Deque{T} is read-only; otherwise, false.
        /// </returns>
        /// <remarks>Always returns false.</remarks>
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }
        #endregion
        #region IList
        #endregion
        #endregion
    }
}
