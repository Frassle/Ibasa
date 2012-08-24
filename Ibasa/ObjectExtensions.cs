using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Swaps object a and b.
        /// </summary>
        /// <typeparam name="T">The type of a and b.</typeparam>
        /// <param name="a">The location to move b to.</param>
        /// <param name="b">The location to move a to.</param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
