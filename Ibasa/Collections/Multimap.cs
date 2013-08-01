using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ibasa.Collections
{
    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public sealed class Multimap<TKey, TValue> : ILookup<TKey, TValue>
    {
        [Serializable]
        [DebuggerDisplay("Count = {Count}")]
        public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>
        {
            private Multimap<TKey, TValue> Multimap;

            public KeyCollection(Multimap<TKey, TValue> multimap)
            {
                if (multimap == null)
                    throw new ArgumentNullException("multimap");

                Multimap = multimap;
            }

            public int Count
            {
                get
                {
                    return Multimap.map.Count;
                }
            }

            public void CopyTo(TKey[] array, int index)
            {
                foreach (var key in this)
                {
                    array[index++] = key;
                }
            }

            public bool Contains(TKey key)
            {
                return Multimap.ContainsKey(key);
            }

            public Enumerator GetEnumerator()
            {
                return new KeyCollection.Enumerator(Multimap);
            }

            System.Collections.Generic.IEnumerator<TKey> System.Collections.Generic.IEnumerable<TKey>.GetEnumerator()
            {
                return GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            void System.Collections.Generic.ICollection<TKey>.Add(TKey item)
            {
                throw new NotSupportedException();
            }

            void System.Collections.Generic.ICollection<TKey>.Clear()
            {
                throw new NotSupportedException();
            }

            bool System.Collections.Generic.ICollection<TKey>.Remove(TKey item)
            {
                throw new NotSupportedException();
            }

            bool System.Collections.Generic.ICollection<TKey>.IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            [Serializable]
            public struct Enumerator : IEnumerator<TKey>
            {
                private Dictionary<TKey, List<TValue>>.KeyCollection.Enumerator MapEnumerator;

                public Enumerator(Multimap<TKey, TValue> multimap)
                {
                    MapEnumerator = multimap.map.Keys.GetEnumerator();
                }

                public TKey Current { get { return MapEnumerator.Current; } }

                public bool MoveNext()
                {
                    return MapEnumerator.MoveNext();
                }

                public void Dispose()
                {
                    MapEnumerator.Dispose();
                }

                object System.Collections.IEnumerator.Current
                {
                    get { return Current; }
                }

                void System.Collections.IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }
            }
        }

        [Serializable]
        [DebuggerDisplay("Count = {Count}")]
        public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>
        {
            private Multimap<TKey, TValue> Multimap;

            public ValueCollection(Multimap<TKey, TValue> multimap)
            {
                if (multimap == null)
                    throw new ArgumentNullException("multimap");

                Multimap = multimap;
            }

            public int Count { get { return Multimap.Count; } }

            public void CopyTo(TValue[] array, int index)
            {
                foreach (var value in this)
                {
                    array[index++] = value;
                }
            }

            public bool Contains(TValue key)
            {
                return Multimap.ContainsValue(key);
            }

            public Enumerator GetEnumerator()
            {
                return new ValueCollection.Enumerator(Multimap);
            }

            System.Collections.Generic.IEnumerator<TValue> System.Collections.Generic.IEnumerable<TValue>.GetEnumerator()
            {
                return GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            void System.Collections.Generic.ICollection<TValue>.Add(TValue item)
            {
                throw new NotSupportedException();
            }

            void System.Collections.Generic.ICollection<TValue>.Clear()
            {
                throw new NotSupportedException();
            }

            bool System.Collections.Generic.ICollection<TValue>.Remove(TValue item)
            {
                throw new NotSupportedException();
            }

            bool System.Collections.Generic.ICollection<TValue>.IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            [Serializable]
            public struct Enumerator : IEnumerator<TValue>
            {
                private Dictionary<TKey, List<TValue>>.ValueCollection.Enumerator MapEnumerator;
                private List<TValue>.Enumerator ListEnumerator;
                private bool Started;

                public Enumerator(Multimap<TKey, TValue> multimap)
                {
                    MapEnumerator = multimap.map.Values.GetEnumerator();
                    ListEnumerator = default(List<TValue>.Enumerator);
                    Started = false;
                }

                public TValue Current { get { return ListEnumerator.Current; } }

                public bool MoveNext()
                {
                    if (Started)
                    {
                        if (ListEnumerator.MoveNext())
                        {
                            return true;
                        }
                    }

                    if (!MapEnumerator.MoveNext())
                    {
                        return false;
                    }

                    ListEnumerator = MapEnumerator.Current.GetEnumerator();
                    return ListEnumerator.MoveNext();
                }

                public void Dispose()
                {
                    MapEnumerator.Dispose();
                    ListEnumerator.Dispose();
                }

                object System.Collections.IEnumerator.Current
                {
                    get { return Current; }
                }

                void System.Collections.IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }
            }
        }

        Dictionary<TKey, List<TValue>> map;

        public Multimap()
        {
            map = new Dictionary<TKey, List<TValue>>();
        }

        public Multimap(int capacity)
        {
            map = new Dictionary<TKey, List<TValue>>(capacity);
        }

        public Multimap(Multimap<TKey, TValue> multimap)
        {
            map = new Dictionary<TKey, List<TValue>>(multimap.Comparer);
            foreach (var pair in multimap)
            {
                List<TValue> list = new List<TValue>();
                foreach(var item in pair)
                {
                    list.Add(item);
                }
                map.Add(pair.Key, list);
            }
        }

        public Multimap(IEqualityComparer<TKey> comparer)
        {
            map = new Dictionary<TKey, List<TValue>>(comparer);
        }

        public Multimap(int capacity, IEqualityComparer<TKey> comparer)
        {
            map = new Dictionary<TKey, List<TValue>>(capacity, comparer);
        }

        public Multimap(Multimap<TKey, TValue> multimap, IEqualityComparer<TKey> comparer)
        {
            map = new Dictionary<TKey, List<TValue>>(comparer);
            foreach (var pair in multimap)
            {
                List<TValue> list = new List<TValue>();
                foreach (var item in pair)
                {
                    list.Add(item);
                }
                map.Add(pair.Key, list);
            }
        }

        public IEqualityComparer<TKey> Comparer { get { return map.Comparer; } }

        public int Count { get; private set; }

        public KeyCollection Keys
        {
            get
            {
                return new KeyCollection(this);
            }
        }

        public ValueCollection Values
        {
            get
            {
                return new ValueCollection(this);
            }
        }

        public IEnumerable<TValue> this[TKey key]
        {
            get
            {
                return map[key];
            }
            set
            {
                List<TValue> list;
                if (map.TryGetValue(key, out list))
                {
                    list.Clear();
                }
                else
                {
                    list = new List<TValue>();
                    map.Add(key, list);
                }

                foreach (var item in value)
                {
                    list.Add(item);
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            List<TValue> list;
            if (map.TryGetValue(key, out list))
            {
                list.Add(value);
            }
            else
            {
                list = new List<TValue>();
                list.Add(value);
                map.Add(key, list);
            }
            
            ++Count;
        }

        public void Clear()
        {
            map.Clear();
        }

        bool ILookup<TKey, TValue>.Contains(TKey key)
        {
            return ContainsKey(key);
        }

        public bool ContainsKey(TKey key)
        {
            return map.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            foreach (var pair in map)
            {
                if (pair.Value.Contains(value))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Remove all values with the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            return map.Remove(key);
        }

        /// <summary>
        /// Removes the given key, value pair.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove(TKey key, TValue value)
        {
            List<TValue> list;
            if (map.TryGetValue(key, out list))
            {
                return list.Remove(value);
            }
            return false;
        }

        /// <summary>
        /// Removes 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int RemoveAll(TValue value)
        {
            int count = 0;
            foreach (var pair in map)
            {
                bool removed = pair.Value.Remove(value);
                count += removed ? 1 : 0;
            }
            return count;
        }

        public bool TryGetValue(TKey key, out IEnumerable<TValue> values)
        {
            List<TValue> list = null;
            bool result = map.TryGetValue(key, out list);
            values = list;
            return result;
        }

        public struct Grouping : IGrouping<TKey, TValue>
        {
            public TKey Key
            {
                get;
                private set;
            }

            private List<TValue> Values;

            internal Grouping(KeyValuePair<TKey, List<TValue>> pair)
                : this()
            {
                Key = pair.Key;
                Values = pair.Value;
            }

            public int Count { get { return Values.Count; } }

            public void CopyTo(TValue[] array, int index)
            {
                Values.CopyTo(array, index);
            }

            public bool Contains(TValue key)
            {
                return Values.Contains(key);
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(Values);
            }

            System.Collections.Generic.IEnumerator<TValue> System.Collections.Generic.IEnumerable<TValue>.GetEnumerator()
            {
                return GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public struct Enumerator : IEnumerator<TValue>
            {
                List<TValue>.Enumerator ListEnumerator;

                internal Enumerator(List<TValue> list)
                {
                    ListEnumerator = list.GetEnumerator();
                }

                public TValue Current
                {
                    get
                    {
                        return ListEnumerator.Current;
                    }
                }

                public bool MoveNext()
                {
                    return ListEnumerator.MoveNext();
                }

                public void Dispose()
                {
                    ListEnumerator.Dispose();
                }

                object System.Collections.IEnumerator.Current
                {
                    get
                    {
                        return Current;
                    }
                }

                void System.Collections.IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        System.Collections.Generic.IEnumerator<IGrouping<TKey, TValue>> System.Collections.Generic.IEnumerable<IGrouping<TKey, TValue>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<IGrouping<TKey, TValue>>
        {
            Dictionary<TKey, List<TValue>>.Enumerator MapEnumerator;

            internal Enumerator(Multimap<TKey, TValue> multimap)
            {
                MapEnumerator = multimap.map.GetEnumerator();
            }

            public Grouping Current
            {
                get
                {
                    return new Grouping(MapEnumerator.Current);
                }
            }

            public bool MoveNext()
            {
                return MapEnumerator.MoveNext();
            }

            public void Dispose()
            {
                MapEnumerator.Dispose();
            }

            IGrouping<TKey, TValue> System.Collections.Generic.IEnumerator<IGrouping<TKey, TValue>>.Current
            {
                get
                {
                    return Current;
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            void System.Collections.IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }
        }
    }
}
