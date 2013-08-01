using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa
{
    /// <summary>
    /// Provides static methods for creating structure objects.
    /// </summary>
    public static class Structure
    {
        /// <summary>
        /// Creates a new 2-structure, or pair.
        /// </summary>
        /// <typeparam name="T1">The type of the first component of the structure.</typeparam>
        /// <typeparam name="T2">The type of the second component of the structure.</typeparam>
        /// <param name="item1">The value of the first component of the structure.</param>
        /// <param name="item2">The value of the second component of the structure.</param>
        /// <returns>A 2-structure whose value is (item1, item2).</returns>
        public static Structure<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new Structure<T1, T2>(item1, item2);
        }
    }

    /// <summary>
    /// Represents a 2-structure, or pair.
    /// </summary>
    /// <typeparam name="T1">The type of the structure's first component.</typeparam>
    /// <typeparam name="T2">The type of the structure's second component.</typeparam>
    public struct Structure<T1, T2>
    {  
        /// <summary>
        /// Gets the value of the current Structure{T1,T2} object's first component.
        /// </summary>
        /// <returns>
        /// The value of the current Structure{T1,T2} object's first component.
        /// </returns>
        public T1 Item1 { get; private set; }

        /// <summary>
        /// Gets the value of the current Structure{T1,T2} object's second component.
        /// </summary>
        /// <returns>
        /// The value of the current Structure{T1,T2} object's second component.
        /// </returns>
        public T2 Item2 { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Structure{T1,T2} struct.
        /// </summary>
        /// <param name="item1">The value of the structure's first component.</param>
        /// <param name="item2">The value of the structure's second component.</param>
        public Structure(T1 item1, T2 item2)
            : this()
        {
            Item1 = item1;
            Item2 = item2;
        }

        /// <summary>
        /// Returns a value that indicates whether the current Structure{T1,T2}
        /// object is equal to a specified object.
        /// </summary>
        /// <param name="obj">
        /// The object to compare with this instance.
        /// </param>
        /// <returns>
        /// true if the current instance is equal to the specified object; 
        /// otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Structure<T1, T2>)
            {
                var structure = (Structure<T1, T2>)obj;
                return Item1.Equals(structure.Item1) && Item2.Equals(structure.Item2);
            }
            return false;
        }

        /// <summary>
        /// Returns the hash code for the current Structure{T1,T2} object.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return Item1.GetHashCode() + Item2.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the value of this Structure{T1,T2} instance.
        /// </summary>
        /// <returns>
        /// The string representation of this Structure{T1,T2} object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("<{0}, {1}>", Item1.ToString(), Item2.ToString());
        }
    }
}
