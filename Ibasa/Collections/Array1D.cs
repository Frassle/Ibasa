using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Collections
{
    public static class Array1D
    {
        public static T[] Initalize<T>(int length, Func<int, T> initalizer)
        {
            var array = new T[length];

            for (int i = 0; i < length; ++i)
            {
                array[i] = initalizer(i);
            }

            return array;
        }

        public static U[] Map<T, U>(T[] array, Func<T, U> transform)
        {
            var result = new U[array.GetLength(0)];

            for (int i = 0; i < array.GetLength(0); ++i)
            {
                result[i] = transform(array[i]);
            }

            return result;
        }

        public static U[] MapIndexed<T, U>(T[] array, Func<int, T, U> transform)
        {
            var result = new U[array.GetLength(0)];

            for (int i = 0; i < array.GetLength(0); ++i)
            {
                result[i] = transform(i, array[i]);
            }

            return result;
        }

        public static U[] Map2<T1, T2, U>(T1[] array1, T2[] array2, Func<T1, T2, U> transform)
        {
            Contract.Requires(array1.GetLength(0) == array2.GetLength(0));

            var result = new U[array1.GetLength(0)];

            for (int i = 0; i < array1.GetLength(0); ++i)
            {
                result[i] = transform(array1[i], array2[i]);
            }

            return result;
        }

        public static U[] Map2Indexed<T1, T2, U>(T1[] array1, T2[] array2, Func<int, T1, T2, U> transform)
        {
            Contract.Requires(array1.GetLength(0) == array2.GetLength(0));

            var result = new U[array1.GetLength(0)];

            for (int i = 0; i < array1.GetLength(0); ++i)
            {
                result[i] = transform(i, array1[i], array2[i]);
            }

            return result;
        }
    }
}
