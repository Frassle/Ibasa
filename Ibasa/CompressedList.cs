using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa
{
    public sealed class CompressedList<T> : IEnumerable<T> where T : IEquatable<T>
    {
        public struct Run
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

        public int Count { get { return Runs.Sum(run => run.Length + 1); } }

        public T this[int index]
        {
            get
            {
                int run, offset;
                RunAndOffset(index, out run, out offset);
                return Runs[run].Value;
            }
            set
            {
                int run, offset;
                RunAndOffset(index, out run, out offset);

                if(!Runs[run].Value.Equals(value))
                {
                    var before = offset;
                    var after = Runs[run].Length - offset;
                    var val = Runs[run].Value;

                    Runs[run] = new Run(0, value);

                    if (before != 0)
                    {
                        Runs.Insert(run, new Run((byte)(before - 1), val));
                    }
                    if (after != 0)
                    {
                        Runs.Insert(run + 1, new Run((byte)(after - 1), val));
                    }                    
                }
            }
        }

        private void RunAndOffset(int index, out int run, out int offset)
        {
            run = 0;
            while (index > Runs[run].Length)
            {
                index -= Runs[run].Length + 1;
                ++run;
            }
            offset = index;
        }

        public void Add(T value)
        {
            var last = Runs.Count - 1;

            // See if we can add it to the last run
            if (Runs.Count > 0 && Runs[last].Length != byte.MaxValue && Runs[last].Value.Equals(value))
            {
                Runs[last] = new Run((byte)(Runs[last].Length + 1), Runs[last].Value);
            }
            else
            {
                // Else add new 1 element run
                Runs.Add(new Run(0, value));
            }
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

        public void Insert(int index, T value)
        {
            int run, offset;
            RunAndOffset(index, out run, out offset);

            if (Runs[run].Length != byte.MaxValue && Runs[run].Value.Equals(value))
            {
                Runs[run] = new Run((byte)(Runs[run].Length + 1), Runs[run].Value);
            }
            else if(offset == 0 && run > 0 && Runs[run - 1].Length != byte.MaxValue && Runs[run - 1].Value.Equals(value))
            {
                Runs[run - 1] = new Run((byte)(Runs[run - 1].Length + 1), Runs[run - 1].Value);
            }
            else if (offset == Runs[run].Length && run < Runs.Count - 1 && Runs[run + 1].Length != byte.MaxValue && Runs[run + 1].Value.Equals(value))
            {
                Runs[run + 1] = new Run((byte)(Runs[run + 1].Length + 1), Runs[run + 1].Value);
            }
            else
            {
                var before = offset;
                var after = (Runs[run].Length + 1) - offset;
                var val = Runs[run].Value;

                Runs[run] = new Run(0, value);

                if (before != 0)
                {
                    Runs.Insert(run, new Run((byte)(before - 1), val));
                }
                if (after != 0)
                {
                    Runs.Insert(run + 1, new Run((byte)(after - 1), val));
                }
            }
        }

        public void RemoveAt(int index)
        {
            int run, offset;
            RunAndOffset(index, out run, out offset);

            if (Runs[run].Length != 0)
            {
                Runs[run] = new Run((byte)(Runs[run].Length - 1), Runs[run].Value);
            }
            else
            {
                Runs.RemoveAt(run);
            }
        }

        public int IndexOf(T value, int index, int count)
        {
            int index_of = 0;
            int index_end = index + count;
            foreach (var run in Runs)
            {
                if (index_of < index)
                {
                    index_of += run.Length + 1;
                    continue;
                }
                if (index_of >= index_end)
                {
                    break;
                }

                if (run.Value.Equals(value))
                {
                    return index_of;
                }
                else
                {
                    index_of += run.Length + 1;
                }
            }
            return -1;
        }

        public int IndexOf(T value, int index)
        {
            int index_of = 0;
            foreach (var run in Runs)
            {
                if (index_of < index)
                {
                    index_of += run.Length + 1;
                    continue;
                }

                if (run.Value.Equals(value))
                {
                    return index_of;
                }
                else
                {
                    index_of += run.Length + 1;
                }
            }
            return -1;
        }

        public int IndexOf(T value)
        {
            int index_of = 0;
            foreach(var run in Runs)
            {
                if (run.Value.Equals(value))
                {
                    return index_of;
                }
                else
                {
                    index_of += run.Length + 1;
                }
            }
            return -1;
        }

        public void Clear()
        {
            Runs.Clear();
        }

        public void CopyTo(ArraySegment<T> data)
        {
            Contract.Requires(data != null);
            Contract.Requires(data.Count >= Count);

            int i = 0;
            foreach(var run in Runs)
            {
                int end = i + run.Length;

                for (; i <= end; ++i)
                {
                    data[i] = run.Value;
                }
            }
        }

        public T[] ToArray()
        {
            T[] data = new T[Count];
            CopyTo(data);
            return data;
        }

        #region IEnumerable
        public struct Enumerator : IEnumerator<T>
        {
            CompressedList<T> list;
            int index;
            int length;

            internal Enumerator(CompressedList<T> list)
            {
                this.list = list;
                this.index = -1;
                this.length = 0;
            }

            public bool MoveNext()
            {
                --length;
                if (length < 0)
                {
                    ++index;
                    if (index < list.Runs.Count)
                    {
                        length = list.Runs[index].Length;
                    }
                }
                return index < list.Runs.Count;
            }

            public T Current
            {
                get
                {
                    return list.Runs[index].Value;
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            void IDisposable.Dispose()
            {

            }

            void System.Collections.IEnumerator.Reset()
            {
                throw new NotImplementedException();
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}