using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa
{
    public static class EnumerableExtensions
    {
        public static T Min<T, K>(this IEnumerable<T> source, Func<T,K> selector) where K : IComparable<K>
        {
            IEnumerator<T> e = source.GetEnumerator();

            if (e.MoveNext())
            {
                var min = e.Current;
                var mink = selector(min);

                while (e.MoveNext())
                {
                    var current = e.Current;
                    var key = selector(current);

                    if (key.CompareTo(mink) < 0)
                    {
                        mink = key;
                        min = current;
                    }
                }

                return min;
            }

            throw new ArgumentException("source is empty");
        }

        public static T Max<T, K>(this IEnumerable<T> source, Func<T, K> selector) where K : IComparable<K>
        {
            IEnumerator<T> e = source.GetEnumerator();

            if (e.MoveNext())
            {
                var max = e.Current;
                var maxk = selector(max);

                while (e.MoveNext())
                {
                    var current = e.Current;
                    var key = selector(current);

                    if (key.CompareTo(maxk) < 0)
                    {
                        maxk = key;
                        max = current;
                    }
                }

                return max;
            }

            throw new ArgumentException("source is empty");
        }

        public static IEnumerable<T> Single<T>(T element)
        {
            yield return element;
        }

        public static IEnumerable<T> ExceptAt<T>(this IEnumerable<T> source, long index)
        {
            var e = source.GetEnumerator();
            long i = 0;
            while (e.MoveNext())
            {
                if (i++ == index)
                    break;
                else
                    yield return e.Current;
            }
            while (e.MoveNext())
            {
                yield return e.Current;
            }
        }

        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> collection, int count)
        {
            if (count == 0)
                yield return Enumerable.Empty<T>();
            else
            {
                if (!collection.Any())
                    throw new ArgumentException("count is larger than collection.");

                long index = 0;
                foreach (T element in collection)
                {
                    IEnumerable<T> remaining = collection.ExceptAt(index++);

                    foreach (IEnumerable<T> permutation in Permutations(remaining, count - 1))
                    {
                        yield return Enumerable.Concat(Single(element), permutation);
                    }
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> PermutationsWithRepetition<T>(this IEnumerable<T> collection, int count)
        {
            if (count == 0 || !collection.Any())
                yield return Enumerable.Empty<T>();
            else
            {                
                foreach (T element in collection)
                {
                    foreach (IEnumerable<T> permutation in PermutationsWithRepetition(collection, count - 1))
                    {
                        yield return Enumerable.Concat(Single(element), permutation);
                    }
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> collection, int count)
        {
            if (count == 0)
                yield return Enumerable.Empty<T>();
            else
            {
                if (!collection.Any())
                    throw new ArgumentException("count is larger than collection.");

                int length = collection.Count() - count;
                for(int i = 0; i <= length; ++i)
                {
                    IEnumerable<T> remaining = collection.Skip(i);

                    foreach (IEnumerable<T> combination in Combinations(remaining.Skip(1), count - 1))
                    {
                        yield return Enumerable.Concat(remaining.Take(1), combination);
                    }
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> CombinationsWithRepetition<T>(this IEnumerable<T> collection, int count)
        {
            throw new NotImplementedException();
        }
    }
}
