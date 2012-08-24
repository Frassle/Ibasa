using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa
{
    public static class Buffer
    {
        /// <summary>Compares a specified number of bytes from one array starting at a particular offset to another array starting at a particular offset.</summary>
        /// <param name="arr1">The first buffer.</param>
        /// <param name="arr1Offset">The zero-based byte offset into arr1.</param>
        /// <param name="arr2">The second buffer.</param>
        /// <param name="arr2Offset">The zero-based byte offset into arr2.</param>
        /// <param name="count">The number of bytes to compare.</param>
        /// <returns>Returns an integral value indicating the relationship between the content of the arrays:
        /// A zero value indicates that the contents of both arrays are equal.
        /// A value greater than zero indicates that the first byte that does not match in both memory blocks has a greater value in arr1 than in arr2; And a value less than zero indicates the opposite.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">arr1 or arr2 is null.</exception>
        /// <exception cref="System.ArgumentException">arr1 or arr2 is an object array, not a value type array.</exception>
        /// <exception cref="System.ArgumentException">The length of arr1 is less than arr1Offset plus count.</exception>
        /// <exception cref="System.ArgumentException">The length of arr2 is less than arr2Offset plus count.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arr1Offset, arr2Offset, or count is less than 0.</exception>
        public static int BlockCompare(Array arr1, int arr1Offset, Array arr2, int arr2Offset, int count)
        {
            Contract.Requires(arr1 != null, "arr1 is null");
            Contract.Requires(arr2 != null, "arr2 is null");

           /* if (!(arr1.GetType().GetElementType().IsPrimitive))
                throw new ArgumentException("Object must be an array of primitives.", "arr1");
            if (!(arr2.GetType().GetElementType().IsPrimitive))
                throw new ArgumentException("Object must be an array of primitives.", "arr2");

            if (arr1.Length < arr1Offset + count)
                throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            if (arr2.Length < arr2Offset + count)
                throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");

            if (arr1Offset < 0)
                throw new ArgumentOutOfRangeException("arr1Offset", "Non-negative number required.");
            if (arr2Offset < 0)
                throw new ArgumentOutOfRangeException("arr2Offset", "Non-negative number required.");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "Non-negative number required.");*/


            for (int i = 0; i < count; ++i)
            {
                byte b1 = System.Buffer.GetByte(arr1, arr1Offset);
                byte b2 = System.Buffer.GetByte(arr2, arr2Offset);

                if (b1 > b2)
                    return 1;
                else if (b1 < b2)
                    return -1;

                ++arr1Offset;
                ++arr2Offset;
            }

            return 0;
        }
        /// <summary>Assigns a specified value to a range of bytes at a particular location in a specified array.</summary>
        /// <param name="array">An array.</param>
        /// <param name="offset">A location in the array.</param>
        /// <param name="count">The number of bytes to set.</param>
        /// <param name="value">A value to assign.</param>
        /// <exception cref="System.ArgumentException">array is not a primitive.</exception>
        /// <exception cref="System.ArgumentException">The length of array is less than offset plus count.</exception>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">offset or count is less than 0.</exception>
        public static void BlockSet(Array array, int offset, int count, byte value)
        {
         /*   if (array == null)
                throw new ArgumentNullException("array", "Value cannot be null.");

            if (!(array.GetType().GetElementType().IsPrimitive))
                throw new ArgumentException("Object must be an array of primitives.", "array");

            if (array.Length < offset + count)
                throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");

            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "Non-negative number required.");*/


            for (int i = 0; i < count; ++i)
            {
                System.Buffer.SetByte(array, offset, value);
                ++offset;
            }
        }
        /// <summary>Searches for a byte value in an array starting at a particular offset.</summary>
        /// <param name="array">An array.</param>
        /// <param name="offset">A location in the array.</param>
        /// <param name="count">The number of bytes to search.</param>
        /// <param name="value">A value to search for.</param>
        /// <returns>The offset in the array that the value was found at</returns>
        /// <returns>-1 if the value is not found.</returns>
        /// <exception cref="System.ArgumentException">array is not a primitive.</excpetion>
        /// <exception cref="System.ArgumentException">The length of array is less than offset plus count.</exception>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">offset or count is less than 0.</exception>
		public static int BlockSearch(Array array, int offset, int count, byte value)
		{
            Contract.Requires(array != null);

			/*if (array == null)
                throw new ArgumentNullException("array", "Value cannot be null.");

            if (!(array.GetType().GetElementType().IsPrimitive))
                throw new ArgumentException("Object must be an array of primitives.", "array");

            if (array.Length < offset + count)
                throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");

            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "Non-negative number required.");*/


			for (int i = 0; i < count; ++i)
            {
                if(System.Buffer.GetByte(array, offset) == value)
					return offset;
                ++offset;
            }
			
			return -1;
		}

        public static void BlockCopy(Array src, int srcOffset, Array dst, int dstOffset, int count)
        {
            System.Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
        }

        public static int ByteLength(Array array)
        {
            return System.Buffer.ByteLength(array);
        }

        public static byte GetByte(Array array, int index)
        {
            return System.Buffer.GetByte(array, index);
        }

        public static void SetByte(Array array, int index, byte value)
        {
            System.Buffer.SetByte(array, index, value);
        }
    }
}
