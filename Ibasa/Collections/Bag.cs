using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Collections
{
    /// <summary>
    /// Represents an unordered collection of objects.
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the cache.</typeparam>
    public sealed class Bag<T> : ICollection<T>
    {
        private List<T> _list;

        public int Count
        {
            get { return _list.Count; }
        }

        public int Capacity
        {
            get
            {
                return _list.Capacity;
            }
            set
            {
                _list.Capacity = value;
            }
        }

        public Bag()
        {
            _list = new List<T>();
        }

        public Bag(IEnumerable<T> collection)
        {
            _list = new List<T>(collection);
        }

        public Bag(int capacity)
        {
            _list = new List<T>(capacity);
        }

        public void Add(T item)
        {
            _list.Add(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            _list.AddRange(collection);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public Bag<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            Bag<TOutput> output = new Bag<TOutput>(Count);

            foreach (var item in this)
            {
                output.Add(converter(item));
            }

            return output;
        }

        public void CopyTo(T[] array)
        {
            _list.CopyTo(array);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public void CopyTo(T[] array, int arrayIndex, int count)
        {
            _list.CopyTo(0, array, arrayIndex, count);
        }

        public bool Exists(Predicate<T> match)
        {
            return _list.Exists(match);
        }

        public T Find(Predicate<T> match)
        {
            return _list.Find(match);
        }

        public Bag<T> FindAll(Predicate<T> match)
        {
            return new Bag<T>(_list.FindAll(match));
        }

        public bool Remove(T item)
        {
            // Unordered so we can do the swap to end trick
            int index = _list.IndexOf(item);
            if (index != -1)
            {
                _list[index] = _list[_list.Count - 1];
                _list.RemoveAt(_list.Count - 1);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int RemoveAll(Predicate<T> match)
        {
            // Unordered so we can do the swap to end trick
            int count = 0;
            int index = 0;
            while (index < _list.Count)
            {
                if (match(_list[index]))
                {
                    _list[index] = _list[_list.Count - 1];
                    _list.RemoveAt(_list.Count - 1);
                    ++count;
                }
                else
                {
                    ++index;
                }
            }
            return count;
        }

        public T[] ToArray()
        {
            return _list.ToArray();
        }

        public void TrimExcess()
        {
            _list.TrimExcess();
        }

        public bool TrueForAll(Predicate<T> match)
        {
            return _list.TrueForAll(match);
        }

        public List<T>.Enumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }
    }
}
