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
    public struct Singleton<T1>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ibasa.Singleton<T1>"/> structure.
        /// </summary>
        /// <param name="value1">The value of the tuple's only component.</param>
        public Singleton(T1 item1)
        {
            Item1 = item1;
        }

        /// <summary>
        /// The value of the <see cref="Ibasa.Singleton<T1>"/> object's single component.
        /// </summary>
        public readonly T1 Item1;

        public static explicit operator Tuple<T1>(Singleton<T1> singleton)
        {
            return new Tuple<T1>(singleton.Item1);
        }

        public static explicit operator Singleton<T1>(Tuple<T1> tuple)
        {
            return new Singleton<T1>(tuple.Item1);
        }

    }

    /// <summary>
    /// Represents a 2-tuple, or pair.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
    /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
    public struct Pair<T1, T2>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ibasa.Pair<T1,T2>"/> class.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        public Pair(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        /// <summary>
        /// The value of the <see cref="Ibasa.Pair<T1,T2>"/> object's first component.
        /// </summary>
        public readonly T1 Item1;

        /// <summary>
        /// The value of the <see cref="Ibasa.Pair<T1,T2>"/> object's second component.
        /// </summary>
        public readonly T2 Item2;

        public static explicit operator Tuple<T1, T2>(Pair<T1, T2> triple)
        {
            return new Tuple<T1, T2>(triple.Item1, triple.Item2);
        }

        public static explicit operator Pair<T1, T2>(Tuple<T1, T2> tuple)
        {
            return new Pair<T1, T2>(tuple.Item1, tuple.Item2);
        }
    }

    /// <summary>
    /// Represents a 3-tuple, or triple.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
    /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
    /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
    public struct Triple<T1, T2, T3>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ibasa.Triple<T1,T2,T3>"/> class.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        /// <param name="item3">The value of the tuple's third component.</param>
        public Triple(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        /// <summary>
        /// The value of the <see cref="Ibasa.Triple<T1,T2,T3>"/> object's first component.
        /// </summary>
        public readonly T1 Item1;

        /// <summary>
        /// The value of the <see cref="Ibasa.Triple<T1,T2,T3>"/> object's second component.
        /// </summary>
        public readonly T2 Item2;

        /// <summary>
        /// The value of the <see cref="Ibasa.Triple<T1,T2,T3>"/> object's thrid component.
        /// </summary>
        public readonly T3 Item3;


        public static explicit operator Tuple<T1, T2, T3>(Triple<T1, T2, T3> triple)
        {
            return new Tuple<T1, T2, T3>(triple.Item1, triple.Item2, triple.Item3);
        }

        public static explicit operator Triple<T1, T2, T3>(Tuple<T1, T2, T3> tuple)
        {
            return new Triple<T1, T2, T3>(tuple.Item1, tuple.Item2, tuple.Item3);
        }
    }
}
