using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa
{
    public static class ArrayExtensions
    {
        public static void Swap<T>(this T[] array, int index1, int index2)
        {
            Contract.Requires(array != null, "array is null.");
            Contract.Requires(0 <= index1 && index1 < array.Length, "index1 is out of bounds.");
            Contract.Requires(0 <= index2 && index2 < array.Length, "index2 is out of bounds.");

            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }

        public static ArraySegment<T> Segment<T>(this T[] array, int offset, int count)
        {
            Contract.Requires(array != null, "array is null.");
            Contract.Requires(offset >= array.GetLowerBound(0), "offset is less than lower bound.");
            Contract.Requires(count >= 0, "count is less than 0.");
            Contract.Requires(offset + count <= array.GetUpperBound(0), "offset and count do not specify a valid range in array.");

            Contract.Ensures(Contract.Result<ArraySegment<T>>().Array == array);
            Contract.Ensures(Contract.Result<ArraySegment<T>>().Offset == offset);
            Contract.Ensures(Contract.Result<ArraySegment<T>>().Count == count);

            return new ArraySegment<T>(array, offset, count);
        }
    }
}
