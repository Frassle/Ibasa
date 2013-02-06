using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Collections
{
    public static class Array2D
    {
        public static T[,] Initalize<T>(int length1, int length2, Func<int, int, T> initalizer)
        {
            var array = new T[length1, length2];

            for (int i = 0; i < length1; ++i)
            {
                for (int j = 0; j < length2; ++j)
                {
                    array[i, j] = initalizer(i, j);
                }
            }

            return array;
        }

        public static U[,] Map<T, U>(T[,] array, Func<T,U> transform)
        {
            var result = new U[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); ++i)
            {
                for (int j = 0; j < array.GetLength(1); ++j)
                {
                    result[i, j] = transform(array[i, j]);
                }
            }

            return result;
        }

        public static U[,] MapIndexed<T, U>(T[,] array, Func<int, int, T, U> transform)
        {
            var result = new U[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); ++i)
            {
                for (int j = 0; j < array.GetLength(1); ++j)
                {
                    result[i, j] = transform(i, j, array[i, j]);
                }
            }

            return result;
        }

        public static U[,] Map2<T1, T2, U>(T1[,] array1, T2[,] array2, Func<T1, T2, U> transform)
        {
            Contract.Requires(array1.GetLength(0) == array2.GetLength(0));
            Contract.Requires(array1.GetLength(1) == array2.GetLength(1));

            var result = new U[array1.GetLength(0), array1.GetLength(1)];

            for (int i = 0; i < array1.GetLength(0); ++i)
            {
                for (int j = 0; j < array1.GetLength(1); ++j)
                {
                    result[i, j] = transform(array1[i, j], array2[i, j]);
                }
            }

            return result;
        }

        public static U[,] Map2Indexed<T1, T2, U>(T1[,] array1, T2[,] array2, Func<int, int, T1, T2, U> transform)
        {
            Contract.Requires(array1.GetLength(0) == array2.GetLength(0));
            Contract.Requires(array1.GetLength(1) == array2.GetLength(1));

            var result = new U[array1.GetLength(0), array1.GetLength(1)];

            for (int i = 0; i < array1.GetLength(0); ++i)
            {
                for (int j = 0; j < array1.GetLength(1); ++j)
                {
                    result[i, j] = transform(i, j, array1[i, j], array2[i, j]);
                }
            }

            return result;
        }
    }
}
