using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Collections
{
    public sealed class ArrayMap<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private KeyValuePair<TKey, TValue>[] Array;
        private Dictionary<TKey, int> Map;

        public ArrayMap()
            : this(10)
        {
        }

        public ArrayMap(int capacity)
        {
            Array = new KeyValuePair<TKey, TValue>[capacity];
            Map = new Dictionary<TKey, int>(capacity);
        }

        public int Capacity
        {
            get
            {
                return Array.Length;
            }
            set
            {
                System.Array.Resize(ref Array, Math.Min(Array.Length, value));
            }
        }

        public int Count
        {
            get
            {
                return Map.Count;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                var index = Map[key];
                return Array[index].Value;
            }
            set
            {
                var index = Map[key];
                Array[index] = new KeyValuePair<TKey, TValue>(key, value);
            }
        }

        public void Clear()
        {
            Map.Clear();
        }

        public void Add(TKey key, TValue value)
        {
            if (Count == Capacity)
            {
                Capacity = (Capacity * 3) / 2; // Capacity *= 1.5;
            }

            Map.Add(key, Count);
            Array[Count] = new KeyValuePair<TKey,TValue>(key, value);
        }

        public bool Remove(TKey key)
        {
            if (Count == 0)
                return false;
            int index;
            if(Map.TryGetValue(key, out index))
            {
                var end = Array[Count - 1];
                Array[index] = end;
                Map[end.Key] = index;

                Map.Remove(key);

                return true;
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int index;
            if (Map.TryGetValue(key, out index))
            {
                value = Array[index].Value;
                return true;
            }
            value = default(TValue);
            return false;
        }


        public struct ArrayMapEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            ArrayMap<TKey, TValue> Map;
            int Index;

            internal ArrayMapEnumerator(ArrayMap<TKey, TValue> map)
            {
                Map = map;
                Index = -1;
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get { return Map.Array[Index]; }
            }

            public bool MoveNext()
            {
                ++Index;
                return Index != Map.Count;
            }

            public void Dispose()
            {
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        public ArrayMapEnumerator GetEnumerator()
        {
            return new ArrayMapEnumerator(this);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
