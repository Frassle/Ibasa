using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa
{
    public sealed class CompressedList<T> where T : IEquatable<T>
    {
        struct Run
        {
            public Run(byte length, T value)
            {
                Length = length;
                Value = value;
            }

            public readonly byte Length;
            public readonly T Value;
        }

        public List<Run> Runs
        {
            get;
            private set;
        }

        public CompressedList()
        {
            Runs = new List<Run>();
        }

        public CompressedList(IEnumerable<T> collection)
        {
            Runs = new List<Run>();
            AddRange(collection);
        }

        private int _Count = 0;
        public int Count { get { return _Count; } }

        public T this[int index]
        {
            get
            {
                int i = 0;
                while (index > Runs[i].Length)
                {
                    index -= Runs[i].Length + 1;
                    ++i;
                }

                return Runs[i].Value;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(T value)
        {
            ++_Count;

            // See if we can add it to the last run
            if (Runs.Count > 0)
            {
                var last = Runs[Runs.Count - 1];
                if (last.Length != byte.MaxValue && last.Value.Equals(value))
                {
                    Runs[Runs.Count - 1] = new Run((byte)(last.Length + 1), last.Value);
                    return;
                }
            }

            // Else add new 1 element run
            Runs.Add(new Run(0, value));
        }

        public void AddRange(IEnumerable<T> collection)
        {
            Contract.Requires(collection != null);

            // TODO: Make this more efficent
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public void Clear()
        {
            _Count = 0;
            Runs.Clear();
        }

        public void CopyTo(ArraySegment<T> data)
        {
            Contract.Requires(data != null);
            Contract.Requires(data.Count >= Count);

            for (int i = 0; i < Runs.Count; ++i)
            {
                int length = Runs[i].Length + 1;
                T value = Runs[i].Value;

                for (int j = 0; j < length; ++j)
                {
                    data[i++] = value;
                }
            }
        }

        public T[] ToArray()
        {
            T[] data = new T[Count];
            CopyTo(data);
            return data;
        }
    }
}