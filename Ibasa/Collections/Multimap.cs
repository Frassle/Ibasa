using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Collections
{
    public class Multimap<TKey, TValue> : IEnumerable<KeyValuePair<TKey, IEnumerable<TValue>>>
    {
        Dictionary<TKey, HashSet<TValue>> map;

        public Multimap()
        {
            map = new Dictionary<TKey, HashSet<TValue>>();
        }

        public Multimap(int capacity)
        {
            map = new Dictionary<TKey, HashSet<TValue>>(capacity);
        }

        public Multimap(IEqualityComparer<TKey> comparer)
        {
            map = new Dictionary<TKey, HashSet<TValue>>(comparer);
        }

        public IEqualityComparer<TKey> Comparer { get { return map.Comparer; } }

        public int Count { get; private set; }

        public bool Add(TKey key, TValue value)
        {
            HashSet<TValue> set;
            bool added = true;
            if (map.TryGetValue(key, out set))
            {
                added = set.Add(value);
            }
            else
            {
                set = new HashSet<TValue>();
                set.Add(value);
                map.Add(key, set);
            }

            if(added) 
            {
                ++Count;
            }
            return added;
        }

        public void Clear()
        {
            map.Clear();
        }

        public IEnumerable<TValue> this[TKey key]
        {
            get
            {
                return map[key];
            }
            set
            {
                HashSet<TValue> set;
                if (map.TryGetValue(key, out set))
                {
                    set.Clear();
                }
                else
                {
                    set = new HashSet<TValue>();
                    map.Add(key, set);
                }

                foreach (var item in value)
                {
                    set.Add(item);
                }
            }
        }

        public bool TryGetValue(TKey key, out IEnumerable<TValue> values)
        {
            HashSet<TValue> set = null;
            bool result = map.TryGetValue(key, out set);
            values = set;
            return result;
        }

        public IEnumerator<KeyValuePair<TKey, IEnumerable<TValue>>> GetEnumerator()
        {
            foreach (var key_value in map)
            {
                yield return new KeyValuePair<TKey, IEnumerable<TValue>>(key_value.Key, key_value.Value);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
