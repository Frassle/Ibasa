using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Collections.Immutable
{
    /// <summary>
    /// Represents a strongly typed immutable array of objects that can be accessed by index.
    /// Provides methods to search, sort, and manipulate immutable arrays.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("Length = {Length}")]
    public sealed class ImmutableArray<T> : IEnumerable<T>
    {
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.RootHidden)]
        readonly T[] _array;

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_array != null);
            Contract.Invariant(_array.Length == Length);
        }
        
        /// <summary>
        /// Initializes a new instance of the ImmutableArray{T} class using the specified
        /// array, optionally doing a deep copy.
        /// </summary>
        /// <param name="array">The array whose elements are copied to the new ImmutableArray{T}.</param>
        /// <param name="copy">If true the array will be deep copied, otherwise just the reference is copied.</param>
        private ImmutableArray(T[] array, bool copy)
        {
            Contract.Requires(array != null);

        	if(copy)
	        	_array = (T[])array.Clone();
		      else
        		_array = array;
        }

        /// <summary>
        /// Initializes a new instance of the ImmutableArray{T} class with the specified length.
        /// </summary>
        /// <param name="length">The number of elements that the new ImmutableArray{T} can store.</param>
        public ImmutableArray(int length)
        {
            Contract.Requires(0 <= length);
            Contract.Ensures(Length == length);

            _array = new T[length];
        }
        /// <summary>
        /// Initializes a new instance of the ImmutableArray{T} class that contains elements 
        /// copied from the specified array.
        /// </summary>
        /// <param name="array">The array whose elements are copied to the new ImmutableArray{T}.</param>
        public ImmutableArray(T[] array)
            : this(array, 0, array.Length)
        {
            Contract.Requires(array != null);
            Contract.Ensures(Length == array.Length);
        }
        /// <summary>
        /// Initializes a new instance of the ImmutableArray{T} class that contains elements 
        /// copied from the specified array starting at the specified index.
        /// </summary>
        /// <param name="array">The array whose elements are copied to the new ImmutableArray{T}.</param>
        /// <param name="index">The index in the array to start copying from.</param>
        public ImmutableArray(T[] array, int index)
            : this(array, index, array.Length - index)
        {
            Contract.Requires(array != null);
            Contract.Requires(0 <= index);
            Contract.Requires(index <= array.Length);
            Contract.Ensures(Length == (array.Length - index));
        }
        /// <summary>
        /// Initializes a new instance of the ImmutableArray{T} class that contains the 
        /// specified number of elements from the specified array starting at the specified index. 
        /// </summary>
        /// <param name="array">The array whose elements are copied to the new ImmutableArray{T}.</param>
        /// <param name="index">The index in the array to start copying from.</param>
        /// <param name="count">The number of element to copy.</param>
        public ImmutableArray(T[] array, int index, int count)
        {
            Contract.Requires(array != null);
            Contract.Requires(0 <= index);
            Contract.Requires(index <= array.Length);
            Contract.Requires(0 <= count);
            Contract.Requires(index + count <= array.Length);
            Contract.Ensures(Length == count);

            _array = new T[count];
            Array.Copy(array, index, _array, 0, count);
        }

        #region Copy
        //
        // Summary:
        //     Copies the entire System.Collections.Generic.List<T> to a compatible one-dimensional
        //     array, starting at the beginning of the target array.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements
        //     copied from System.Collections.Generic.List<T>. The System.Array must have
        //     zero-based indexing.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     array is null.
        //
        //   System.ArgumentException:
        //     The number of elements in the source System.Collections.Generic.List<T> is
        //     greater than the number of elements that the destination array can contain.
        //[TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [Pure]
        public void CopyTo(T[] array)
        {
            Contract.Requires(array != null);
            Contract.Requires(Length <= array.Length);

            Array.Copy(_array, array, Length);
        }
        /// <summary>
        /// Copies the entire ImmutableArray{T} to a compatible one-dimensional
        /// array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional System.Array that is the destination of the elements
        /// copied from ImmutableArray{T}.
        /// </param>
        /// <param name="index">
        /// The zero-based index in array at which copying begins.
        /// </param>
        [Pure]
        public void CopyTo(T[] array, int index)
        {
            Contract.Requires(array != null);
            Contract.Requires(0 <= index);
            Contract.Requires(index <= array.Length);
            Contract.Requires(index + Length <= array.Length);

            Array.Copy(_array, 0, array, index, Length);
        }
        //
        // Summary:
        //     Copies a range of elements from the System.Collections.Generic.List<T> to
        //     a compatible one-dimensional array, starting at the specified index of the
        //     target array.
        //
        // Parameters:
        //   index:
        //     The zero-based index in the source System.Collections.Generic.List<T> at
        //     which copying begins.
        //
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements
        //     copied from System.Collections.Generic.List<T>. The System.Array must have
        //     zero-based indexing.
        //
        //   arrayIndex:
        //     The zero-based index in array at which copying begins.
        //
        //   count:
        //     The number of elements to copy.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     array is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     index is less than 0.-or-arrayIndex is less than 0.-or-count is less than
        //     0.
        //
        //   System.ArgumentException:
        //     index is equal to or greater than the System.Collections.Generic.List<T>.Count
        //     of the source System.Collections.Generic.List<T>.-or-The number of elements
        //     from index to the end of the source System.Collections.Generic.List<T> is
        //     greater than the available space from arrayIndex to the end of the destination
        //     array.
        [Pure]
        public void CopyTo(int sourceIndex, T[] array, int index, int count)
        {
            Contract.Requires(array != null);
            Contract.Requires(0 <= index);
            Contract.Requires(index <= array.Length);
            Contract.Requires(0 <= count);
            Contract.Requires(index + count <= array.Length);
            Contract.Requires(0 <= sourceIndex);
            Contract.Requires(sourceIndex <= Length);
            Contract.Requires(sourceIndex + count <= Length);

            Array.Copy(_array, sourceIndex, array, index, count);
        }

        [Pure]
        public ImmutableArray<T> Copy()
        {
            return Copy(0, Length);
        }
        [Pure]
        public ImmutableArray<T> Copy(int index)
        {
            Contract.Requires(0 <= index);
            Contract.Requires(index <= Length);
            return Copy(index, Length - index);
        }
        [Pure]
        public ImmutableArray<T> Copy(int index, int count)
        {
            Contract.Requires(0 <= index);
            Contract.Requires(index <= Length);
            Contract.Requires(0 <= count);
            Contract.Requires(index + count <= Length);

            ImmutableArray<T> array = new ImmutableArray<T>(count);
            Array.Copy(_array, index, array._array, 0, count);
            return array;
        }

        /// <summary>
        /// Copies the elements of the ImmutableArray{T} to a new array.
        /// </summary>
        /// <returns>
        /// An array containing copies of the elements of the ImmutableArray{T}.
        /// </returns>
        [Pure]
        public T[] ToArray()
        {
            T[] array = new T[Length];
            Array.Copy(_array, array, Length);
            return array;
        }

        //
        // Summary:
        //     Converts an array of one type to an array of another type.
        //
        // Parameters:
        //   array:
        //     The one-dimensional, zero-based System.Array to convert to a target type.
        //
        //   converter:
        //     A System.Converter<TInput,TOutput> that converts each element from one type
        //     to another type.
        //
        // Type parameters:
        //   TInput:
        //     The type of the elements of the source array.
        //
        //   TOutput:
        //     The type of the elements of the target array.
        //
        // Returns:
        //     An array of the target type containing the converted elements from the source
        //     array.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     array is null.-or-converter is null.
        public ImmutableArray<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            Contract.Requires(converter != null);

            ImmutableArray<TOutput> output = new ImmutableArray<TOutput>(Length);

            for (int i = 0; i < Length; ++i)
            {
                output._array[i] = converter(_array[i]);
            }

            return output;
        }
        #endregion

        #region Acccessors
        /// <summary>
        /// Gets a 32-bit integer that represents the total number of elements in the ImmutableArray.
        /// </summary>
        /// <returns>
        /// A 32-bit integer that represents the total number of elements in the ImmutableArray.
        /// </returns>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [Pure]
        public int Length
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);

                return _array.Length;
            }
        }
        /// <summary>
        /// Gets or the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The element at the specified index.</returns>[Pure]
        public T this[int index]
        {
            get
            {
                if (index >= Length || index < 0)
                    throw new ArgumentOutOfRangeException(
                        "Index was out of range. Must be non-negative and less than the size of the collection.", "index");
                return _array[index];
            }
        }
        /// <summary>
        /// Gets the value at the specified position in the ImmutableArray{T}.
        /// The index is specified as a 32-bit integer.
        /// </summary>
        /// <param name="index">
        /// A 32-bit integer that represents the position 
        /// of the ImmutableArray{T} element to get.
        /// </param>
        /// <returns>
        /// The value at the specified position in the ImmutableArray{T}.
        /// </returns>
        [Pure]
        public T GetValue(int index)
        {
            return this[index];
        }
        #endregion

        #region Mutation
        /// <summary>
        /// Sets a value to the element at the specified position in the ImmutableArray{T}.
        /// The index is specified as a 32-bit integer.
        /// </summary>
        /// <param name="value">
        /// The new value for the specified element.
        /// </param>
        /// <param name="index">
        /// A 32-bit integer that represents the position 
        /// of the ImmutableArray{T} element to set.
        /// </param>
        /// <returns>
        /// A new ImmutableArray{T} with the new value at the specified position.
        /// </returns>   
        [Pure]
        public ImmutableArray<T> SetValue(T value, int index)
        {
            Contract.Requires(0 <= index);
            Contract.Requires(index < Length);

            ImmutableArray<T> immutableArray = new ImmutableArray<T>(_array);
            immutableArray._array[index] = value;
            return immutableArray;
        }
        /// <summary>
        /// Reverses the order of the elements in the entire ImmutableArray{T}.
        /// </summary>
        /// <returns>A new ImmutableArray{T} with all elements reversed.</returns>
        [Pure]
        public ImmutableArray<T> Reverse()
        {
            return Reverse(0, Length);
        }
        /// <summary>
        /// Reverses the order of the elements in the specified range.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range to reverse.</param>
        /// <param name="count">The number of elements in the range to reverse.</param>
        /// <returns>A new ImmutableArray{T} with the specified elements reversed.</returns>
        [Pure]
        public ImmutableArray<T> Reverse(int index, int count)
        {
            Contract.Requires(0 <= index);
            Contract.Requires(index <= Length);
            Contract.Requires(0 <= count);
            Contract.Requires(index + count <= Length);

            ImmutableArray<T> array = new ImmutableArray<T>(_array);
            Array.Reverse(array._array, index, count);
            return array;
        }
        ////
        //// Summary:
        ////     Sorts the elements in the entire System.Collections.Generic.List<T> using
        ////     the default comparer.
        ////
        //// Exceptions:
        ////   System.InvalidOperationException:
        ////     The default comparer System.Collections.Generic.Comparer<T>.Default cannot
        ////     find an implementation of the System.IComparable<T> generic interface or
        ////     the System.IComparable interface for type T.
        [Pure]
        public ImmutableArray<T> Sort()
        {
            ImmutableArray<T> array = new ImmutableArray<T>(_array);
            Array.Sort(array._array);
            return array;
        }
        ////
        //// Summary:
        ////     Sorts the elements in the entire System.Collections.Generic.List<T> using
        ////     the specified System.Comparison<T>.
        ////
        //// Parameters:
        ////   comparison:
        ////     The System.Comparison<T> to use when comparing elements.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     comparison is null.
        ////
        ////   System.ArgumentException:
        ////     The implementation of comparison caused an error during the sort. For example,
        ////     comparison might not return 0 when comparing an item with itself.
        [Pure]
        public ImmutableArray<T> Sort(Comparison<T> comparison)
        {
            Contract.Requires(comparison != null);

            ImmutableArray<T> array = new ImmutableArray<T>(_array);
            Array.Sort(array._array, comparison);
            return array;
        }
        ////
        //// Summary:
        ////     Sorts the elements in the entire System.Collections.Generic.List<T> using
        ////     the specified comparer.
        ////
        //// Parameters:
        ////   comparer:
        ////     The System.Collections.Generic.IComparer<T> implementation to use when comparing
        ////     elements, or null to use the default comparer System.Collections.Generic.Comparer<T>.Default.
        ////
        //// Exceptions:
        ////   System.InvalidOperationException:
        ////     comparer is null, and the default comparer System.Collections.Generic.Comparer<T>.Default
        ////     cannot find implementation of the System.IComparable<T> generic interface
        ////     or the System.IComparable interface for type T.
        ////
        ////   System.ArgumentException:
        ////     The implementation of comparer caused an error during the sort. For example,
        ////     comparer might not return 0 when comparing an item with itself.
        [Pure]
        public ImmutableArray<T> Sort(IComparer<T> comparer)
        {
            Contract.Requires(comparer != null);

            ImmutableArray<T> array = new ImmutableArray<T>(_array);
            Array.Sort(array._array, comparer);
            return array;
        }
        ////
        //// Summary:
        ////     Sorts the elements in a range of elements in System.Collections.Generic.List<T>
        ////     using the specified comparer.
        ////
        //// Parameters:
        ////   index:
        ////     The zero-based starting index of the range to sort.
        ////
        ////   count:
        ////     The length of the range to sort.
        ////
        ////   comparer:
        ////     The System.Collections.Generic.IComparer<T> implementation to use when comparing
        ////     elements, or null to use the default comparer System.Collections.Generic.Comparer<T>.Default.
        ////
        //// Exceptions:
        ////   System.ArgumentOutOfRangeException:
        ////     index is less than 0.-or-count is less than 0.
        ////
        ////   System.ArgumentException:
        ////     index and count do not specify a valid range in the System.Collections.Generic.List<T>.-or-The
        ////     implementation of comparer caused an error during the sort. For example,
        ////     comparer might not return 0 when comparing an item with itself.
        ////
        ////   System.InvalidOperationException:
        ////     comparer is null, and the default comparer System.Collections.Generic.Comparer<T>.Default
        ////     cannot find implementation of the System.IComparable<T> generic interface
        ////     or the System.IComparable interface for type T.
        [Pure]
        public ImmutableArray<T> Sort(int index, int count, IComparer<T> comparer)
        {
            Contract.Requires(0 <= index && index < Length);
            Contract.Requires(0 <= count);
            Contract.Requires(index + count <= Length);
            Contract.Requires(comparer != null);

            ImmutableArray<T> array = new ImmutableArray<T>(_array);
            Array.Sort(array._array, index, count, comparer);
            return array;
        }
        #endregion

        #region Search
        ////
        //// Summary:
        ////     Searches the entire sorted System.Collections.Generic.List<T> for an element
        ////     using the default comparer and returns the zero-based index of the element.
        ////
        //// Parameters:
        ////   item:
        ////     The object to locate. The value can be null for reference types.
        ////
        //// Returns:
        ////     The zero-based index of item in the sorted System.Collections.Generic.List<T>,
        ////     if item is found; otherwise, a negative number that is the bitwise complement
        ////     of the index of the next element that is larger than item or, if there is
        ////     no larger element, the bitwise complement of System.Collections.Generic.List<T>.Count.
        ////
        //// Exceptions:
        ////   System.InvalidOperationException:
        ////     The default comparer System.Collections.Generic.Comparer<T>.Default cannot
        ////     find an implementation of the System.IComparable<T> generic interface or
        ////     the System.IComparable interface for type T.
        [Pure]
        public int BinarySearch(T item)
        {
        	return Array.BinarySearch(_array, item);
        }
        ////
        //// Summary:
        ////     Searches the entire sorted System.Collections.Generic.List<T> for an element
        ////     using the specified comparer and returns the zero-based index of the element.
        ////
        //// Parameters:
        ////   item:
        ////     The object to locate. The value can be null for reference types.
        ////
        ////   comparer:
        ////     The System.Collections.Generic.IComparer<T> implementation to use when comparing
        ////     elements.-or-null to use the default comparer System.Collections.Generic.Comparer<T>.Default.
        ////
        //// Returns:
        ////     The zero-based index of item in the sorted System.Collections.Generic.List<T>,
        ////     if item is found; otherwise, a negative number that is the bitwise complement
        ////     of the index of the next element that is larger than item or, if there is
        ////     no larger element, the bitwise complement of System.Collections.Generic.List<T>.Count.
        ////
        //// Exceptions:
        ////   System.InvalidOperationException:
        ////     comparer is null, and the default comparer System.Collections.Generic.Comparer<T>.Default
        ////     cannot find an implementation of the System.IComparable<T> generic interface
        ////     or the System.IComparable interface for type T.
        [Pure]
        public int BinarySearch(T item, IComparer<T> comparer)
        {
        	return Array.BinarySearch(_array, item, comparer);
        }
        ////
        //// Summary:
        ////     Searches a range of elements in the sorted System.Collections.Generic.List<T>
        ////     for an element using the specified comparer and returns the zero-based index
        ////     of the element.
        ////
        //// Parameters:
        ////   index:
        ////     The zero-based starting index of the range to search.
        ////
        ////   count:
        ////     The length of the range to search.
        ////
        ////   item:
        ////     The object to locate. The value can be null for reference types.
        ////
        ////   comparer:
        ////     The System.Collections.Generic.IComparer<T> implementation to use when comparing
        ////     elements, or null to use the default comparer System.Collections.Generic.Comparer<T>.Default.
        ////
        //// Returns:
        ////     The zero-based index of item in the sorted System.Collections.Generic.List<T>,
        ////     if item is found; otherwise, a negative number that is the bitwise complement
        ////     of the index of the next element that is larger than item or, if there is
        ////     no larger element, the bitwise complement of System.Collections.Generic.List<T>.Count.
        ////
        //// Exceptions:
        ////   System.ArgumentOutOfRangeException:
        ////     index is less than 0.-or-count is less than 0.
        ////
        ////   System.ArgumentException:
        ////     index and count do not denote a valid range in the System.Collections.Generic.List<T>.
        ////
        ////   System.InvalidOperationException:
        ////     comparer is null, and the default comparer System.Collections.Generic.Comparer<T>.Default
        ////     cannot find an implementation of the System.IComparable<T> generic interface
        ////     or the System.IComparable interface for type T.
        [Pure]
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
        	return Array.BinarySearch(_array, index, count, item, comparer);
        }
        ////
        //// Summary:
        ////     Determines whether an element is in the System.Collections.Generic.List<T>.
        ////
        //// Parameters:
        ////   item:
        ////     The object to locate in the System.Collections.Generic.List<T>. The value
        ////     can be null for reference types.
        ////
        //// Returns:
        ////     true if item is found in the System.Collections.Generic.List<T>; otherwise,
        ////     false.
        [Pure]
        public bool Contains(T item)
        {
            return Array.IndexOf(_array, item) != -1;
        }
        ////
        //// Summary:
        ////     Determines whether the specified array contains elements that match the conditions
        ////     defined by the specified predicate.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   match:
        ////     The System.Predicate<T> that defines the conditions of the elements to search
        ////     for.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     true if array contains one or more elements that match the conditions defined
        ////     by the specified predicate; otherwise, false.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.-or-match is null.
        [Pure]
        public bool Exists(Predicate<T> match)
        {
        	return Array.Exists(_array, match);
        }
        ////
        //// Summary:
        ////     Determines whether every element in the array matches the conditions defined
        ////     by the specified predicate.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to check against the conditions
        ////
        ////   match:
        ////     The System.Predicate<T> that defines the conditions to check against the
        ////     elements.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     true if every element in array matches the conditions defined by the specified
        ////     predicate; otherwise, false. If there are no elements in the array, the return
        ////     value is true.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.-or-match is null.
        [Pure]
        public bool TrueForAll(Predicate<T> match)
        {
        	return Array.TrueForAll(_array, match);
        }
        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified
        /// predicate, and returns the first occurrence within the entire ImmutableArray{T}.
        /// </summary>
        /// <param name="match">
        /// The System.Predicate{T} that defines the conditions of the element to search for.
        /// </param>
        /// <returns>
        /// The first element that matches the conditions defined by the specified predicate,
        /// if found; otherwise, the default value for type T.
        /// </returns>
        [Pure]
        public T Find(Predicate<T> match)
        {
            Contract.Requires(match != null);
            
            return Array.Find(_array, match);
        }
        ////
        //// Summary:
        ////     Retrieves all the elements that match the conditions defined by the specified
        ////     predicate.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   match:
        ////     The System.Predicate<T> that defines the conditions of the elements to search
        ////     for.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     An System.Array containing all the elements that match the conditions defined
        ////     by the specified predicate, if found; otherwise, an empty System.Array.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.-or-match is null.
        [Pure]
        public ImmutableArray<T> FindAll(Predicate<T> match)
        {
            Contract.Requires(match != null);

            T[] array = Array.FindAll(_array, match);
            if (array == null)
                return null;

            return new ImmutableArray<T>(array, false);
        }
        ////
        //// Summary:
        ////     Searches for an element that matches the conditions defined by the specified
        ////     predicate, and returns the zero-based index of the first occurrence within
        ////     the entire System.Array.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   match:
        ////     The System.Predicate<T> that defines the conditions of the element to search
        ////     for.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The zero-based index of the first occurrence of an element that matches the
        ////     conditions defined by match, if found; otherwise, �?1.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.-or-match is null.
        [Pure]
        public int FindIndex(Predicate<T> match)
        {
            return Array.FindIndex(_array, match);
        }
        ////
        //// Summary:
        ////     Searches for an element that matches the conditions defined by the specified
        ////     predicate, and returns the zero-based index of the first occurrence within
        ////     the range of elements in the System.Array that extends from the specified
        ////     index to the last element.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   startIndex:
        ////     The zero-based starting index of the search.
        ////
        ////   match:
        ////     The System.Predicate<T> that defines the conditions of the element to search
        ////     for.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The zero-based index of the first occurrence of an element that matches the
        ////     conditions defined by match, if found; otherwise, �?1.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.-or-match is null.
        ////
        ////   System.ArgumentOutOfRangeException:
        ////     startIndex is outside the range of valid indexes for array.
        [Pure]
        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return Array.FindIndex(_array, startIndex, match);
        }
        ////
        //// Summary:
        ////     Searches for an element that matches the conditions defined by the specified
        ////     predicate, and returns the zero-based index of the first occurrence within
        ////     the range of elements in the System.Array that starts at the specified index
        ////     and contains the specified number of elements.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   startIndex:
        ////     The zero-based starting index of the search.
        ////
        ////   count:
        ////     The number of elements in the section to search.
        ////
        ////   match:
        ////     The System.Predicate<T> that defines the conditions of the element to search
        ////     for.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The zero-based index of the first occurrence of an element that matches the
        ////     conditions defined by match, if found; otherwise, �?1.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.-or-match is null.
        ////
        ////   System.ArgumentOutOfRangeException:
        ////     startIndex is outside the range of valid indexes for array.-or-count is less
        ////     than zero.-or-startIndex and count do not specify a valid section in array.
        [Pure]
        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return Array.FindIndex(_array, startIndex, count, match);
        }
        ////
        //// Summary:
        ////     Searches for an element that matches the conditions defined by the specified
        ////     predicate, and returns the last occurrence within the entire System.Array.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   match:
        ////     The System.Predicate<T> that defines the conditions of the element to search
        ////     for.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The last element that matches the conditions defined by the specified predicate,
        ////     if found; otherwise, the default value for type T.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.-or-match is null.
        [Pure]
        public T FindLast(Predicate<T> match)
        {
            return Array.FindLast(_array, match);
        }
        ////
        //// Summary:
        ////     Searches for an element that matches the conditions defined by the specified
        ////     predicate, and returns the zero-based index of the last occurrence within
        ////     the entire System.Array.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   match:
        ////     The System.Predicate<T> that defines the conditions of the element to search
        ////     for.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The zero-based index of the last occurrence of an element that matches the
        ////     conditions defined by match, if found; otherwise, �?1.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.-or-match is null.
        [Pure]
        public int FindLastIndex(Predicate<T> match)
        {
            return Array.FindLastIndex(_array, match);
        }
        ////
        //// Summary:
        ////     Searches for an element that matches the conditions defined by the specified
        ////     predicate, and returns the zero-based index of the last occurrence within
        ////     the range of elements in the System.Array that extends from the first element
        ////     to the specified index.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   startIndex:
        ////     The zero-based starting index of the backward search.
        ////
        ////   match:
        ////     The System.Predicate<T> that defines the conditions of the element to search
        ////     for.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The zero-based index of the last occurrence of an element that matches the
        ////     conditions defined by match, if found; otherwise, �?1.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.-or-match is null.
        ////
        ////   System.ArgumentOutOfRangeException:
        ////     startIndex is outside the range of valid indexes for array.
        [Pure]
        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return Array.FindLastIndex(_array, startIndex, match);
        }
        ////
        //// Summary:
        ////     Searches for an element that matches the conditions defined by the specified
        ////     predicate, and returns the zero-based index of the last occurrence within
        ////     the range of elements in the System.Array that contains the specified number
        ////     of elements and ends at the specified index.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   startIndex:
        ////     The zero-based starting index of the backward search.
        ////
        ////   count:
        ////     The number of elements in the section to search.
        ////
        ////   match:
        ////     The System.Predicate<T> that defines the conditions of the element to search
        ////     for.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The zero-based index of the last occurrence of an element that matches the
        ////     conditions defined by match, if found; otherwise, �?1.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.-or-match is null.
        ////
        ////   System.ArgumentOutOfRangeException:
        ////     startIndex is outside the range of valid indexes for array.-or-count is less
        ////     than zero.-or-startIndex and count do not specify a valid section in array.
        [Pure]
        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return Array.FindLastIndex(_array, startIndex, count, match);
        }
        ////
        //// Summary:
        ////     Searches for the specified object and returns the index of the first occurrence
        ////     within the entire System.Array.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   value:
        ////     The object to locate in array.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The zero-based index of the first occurrence of value within the entire array,
        ////     if found; otherwise, �?1.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.
        [Pure]
        public int IndexOf(T value)
        {
            return Array.IndexOf(_array, value);
        }
        ////
        //// Summary:
        ////     Searches for the specified object and returns the index of the first occurrence
        ////     within the range of elements in the System.Array that extends from the specified
        ////     index to the last element.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   value:
        ////     The object to locate in array.
        ////
        ////   startIndex:
        ////     The zero-based starting index of the search. 0 (zero) is valid in an empty
        ////     array.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The zero-based index of the first occurrence of value within the range of
        ////     elements in array that extends from startIndex to the last element, if found;
        ////     otherwise, �?1.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.
        ////
        ////   System.ArgumentOutOfRangeException:
        ////     startIndex is outside the range of valid indexes for array.
        [Pure]
        public int IndexOf(T value, int startIndex)
        {
            return Array.IndexOf(_array, value, startIndex);
        }
        ////
        //// Summary:
        ////     Searches for the specified object and returns the index of the first occurrence
        ////     within the range of elements in the System.Array that starts at the specified
        ////     index and contains the specified number of elements.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   value:
        ////     The object to locate in array.
        ////
        ////   startIndex:
        ////     The zero-based starting index of the search. 0 (zero) is valid in an empty
        ////     array.
        ////
        ////   count:
        ////     The number of elements in the section to search.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The zero-based index of the first occurrence of value within the range of
        ////     elements in array that starts at startIndex and contains the number of elements
        ////     specified in count, if found; otherwise, �?1.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.
        ////
        ////   System.ArgumentOutOfRangeException:
        ////     startIndex is outside the range of valid indexes for array.-or-count is less
        ////     than zero.-or-startIndex and count do not specify a valid section in array.
        [Pure]
        public int IndexOf(T value, int startIndex, int count)
        {
            return Array.IndexOf(_array, value, startIndex, count);
        }
        ////
        //// Summary:
        ////     Searches for the specified object and returns the index of the last occurrence
        ////     within the entire System.Array.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   value:
        ////     The object to locate in array.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The zero-based index of the last occurrence of value within the entire array,
        ////     if found; otherwise, �?1.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.
        [Pure]
        public int LastIndexOf(T value)
        {
            return Array.LastIndexOf(_array, value);
        }
        ////
        //// Summary:
        ////     Searches for the specified object and returns the index of the last occurrence
        ////     within the range of elements in the System.Array that extends from the first
        ////     element to the specified index.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   value:
        ////     The object to locate in array.
        ////
        ////   startIndex:
        ////     The zero-based starting index of the backward search.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The zero-based index of the last occurrence of value within the range of
        ////     elements in array that extends from the first element to startIndex, if found;
        ////     otherwise, �?1.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.
        ////
        ////   System.ArgumentOutOfRangeException:
        ////     startIndex is outside the range of valid indexes for array.
        [Pure]
        public int LastIndexOf(T value, int startIndex)
        {
            return Array.LastIndexOf(_array, value, startIndex);
        }
        ////
        //// Summary:
        ////     Searches for the specified object and returns the index of the last occurrence
        ////     within the range of elements in the System.Array that contains the specified
        ////     number of elements and ends at the specified index.
        ////
        //// Parameters:
        ////   array:
        ////     The one-dimensional, zero-based System.Array to search.
        ////
        ////   value:
        ////     The object to locate in array.
        ////
        ////   startIndex:
        ////     The zero-based starting index of the backward search.
        ////
        ////   count:
        ////     The number of elements in the section to search.
        ////
        //// Type parameters:
        ////   T:
        ////     The type of the elements of the array.
        ////
        //// Returns:
        ////     The zero-based index of the last occurrence of value within the range of
        ////     elements in array that contains the number of elements specified in count
        ////     and ends at startIndex, if found; otherwise, �?1.
        ////
        //// Exceptions:
        ////   System.ArgumentNullException:
        ////     array is null.
        ////
        ////   System.ArgumentOutOfRangeException:
        ////     startIndex is outside the range of valid indexes for array.-or-count is less
        ////     than zero.-or-startIndex and count do not specify a valid section in array.
        [Pure]
        public int LastIndexOf(T value, int startIndex, int count)
        {
            return Array.LastIndexOf(_array, value, startIndex, count);
        }
        #endregion

        #region IEnumerable
        /// <summary>
        /// Enumerates the elements of an ImmutableArray{T}.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            ImmutableArray<T> _array;
            int index;

            [ContractInvariantMethod]
            private void InvariantMethod()
            {
                Contract.Invariant(_array != null);
                Contract.Invariant(-1 <= index);
                Contract.Invariant(index <= _array.Length);
            }

            internal Enumerator(ImmutableArray<T> array)
            {
                Contract.Requires(array != null);

                _array = array;
                index = -1;
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            /// <return>
            /// The element in the ImmutableArray{T} at the current position of the enumerator.
            /// </return>
            public T Current
            {
                get { return _array[index]; }
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
                if (index == _array.Length)
                    return false;

                ++index;
                return index == _array.Length;
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

        /// <summary>
        /// Returns an ImmutableArray{T}.Enumerator for the ImmutableArray{T}.
        /// </summary>
        /// <returns>An ImmutableArray{T}.Enumerator for the ImmutableArray{T}.</returns>
        Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Returns an System.Collections.IEnumerator{T} for the ImmutableArray{T}.
        /// </summary>
        /// <returns>An System.Collections.IEnumerator{T} for the ImmutableArray{T}.</returns>
        System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// Returns an System.Collections.IEnumerator for the ImmutableArray{T}.
        /// </summary>
        /// <returns>An System.Collections.IEnumerator for the ImmutableArray{T}.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
