using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa
{
    /// <summary>
    /// Represents a 1-tuple, or singleton.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's only component.</typeparam>
    public struct Tuple<T1>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ibasa.Tuple<T1>"/> structure.
        /// </summary>
        /// <param name="value1">The value of the tuple's only component.</param>
        public Tuple(T1 item1)
        {
            Item1 = item1;
        }

        /// <summary>
        /// The value of the <see cref="Ibasa.Tuple<T1>"/> object's single component.
        /// </summary>
        public readonly T1 Item1;
    }

    /// <summary>
    /// Represents a 2-tuple, or pair.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
    /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
    public struct Tuple<T1, T2>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ibasa.Tuple<T1,T2>"/> class.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        /// <summary>
        /// The value of the <see cref="Ibasa.Tuple<T1,T2>"/> object's first component.
        /// </summary>
        public readonly T1 Item1;

        /// <summary>
        /// The value of the <see cref="Ibasa.Tuple<T1,T2>"/> object's second component.
        /// </summary>
        public readonly T2 Item2;
    }

    public struct Tuple<T1, T2, T3>
    {
        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }
        
        public readonly T1 Item1;

        public readonly T2 Item2;

        public readonly T3 Item3;
    }

    /// <summary>
    /// Provides static methods for creating tuple objects.
    /// </summary>
    public static class Tuple
    {
        // Summary:
        //     Creates a new 1-tuple, or singleton.
        //
        // Parameters:
        //   item1:
        //     The value of the only component of the tuple.
        //
        // Type parameters:
        //   T1:
        //     The type of the only component of the tuple.
        //
        // Returns:
        //     A tuple whose value is (item1).
        public static Tuple<T1> Create<T1>(T1 item1)
        {
            return new Tuple<T1>(item1);
        }
        //
        // Summary:
        //     Creates a new 2-tuple, or pair.
        //
        // Parameters:
        //   item1:
        //     The value of the first component of the tuple.
        //
        //   item2:
        //     The value of the second component of the tuple.
        //
        // Type parameters:
        //   T1:
        //     The type of the first component of the tuple.
        //
        //   T2:
        //     The type of the second component of the tuple.
        //
        // Returns:
        //     A 2-tuple whose value is (item1, item2).
        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new Tuple<T1, T2>(item1, item2);
        }

        public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new Tuple<T1, T2, T3>(item1, item2, item3);
        }
    }
}
