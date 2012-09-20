using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa
{
    public sealed class SparseList<T> where T : IEquatable<T>
    {
        internal struct Element
        {
            public Element(byte length, T value)
            {
                Length = length;
                Value = value;
            }

            public readonly byte Length;
            public readonly T Value;
        }

        internal List<Element> Compressed = new List<Element>();

        public SparseList()
        {
        }

        public SparseList(IEnumerable<T> collection)
        {
            AddRange(collection);
        }

        private int _Count = 0;
        public int Count { get { return _Count; } }

        public T this[int index]
        {
            get
            {
                int i = 0;
                while (index > Compressed[i].Length)
                {
                    index -= Compressed[i].Length + 1;
                    ++i;
                }

                return Compressed[i].Value;
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
            if (Compressed.Count > 0)
            {
                var last = Compressed[Compressed.Count - 1];
                if (last.Length != byte.MaxValue && last.Value.Equals(value))
                {
                    Compressed[Compressed.Count - 1] = new Element((byte)(last.Length + 1), last.Value);
                    return;
                }
            }
            
            // Else add new 1 element run
            Compressed.Add(new Element(0, value));
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
            Compressed.Clear();
        }

        public void CopyTo(ArraySegment<T> data)
        {
            Contract.Requires(data != null);
            Contract.Requires(data.Count >= Count);

            for (int i = 0; i < Compressed.Count; ++i)
            {
                int length = Compressed[i].Length + 1;
                T value = Compressed[i].Value;

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

    public static class SparseListExtensions
    {
        public static void Write<T>(this Ibasa.IO.BinaryWriter writer, SparseList<T> list) where T : struct, IEquatable<T>
        {
            writer.Write(list.Compressed.Count);
            foreach (var element in list.Compressed)
            {
                writer.Write(element.Length);
                writer.Write<T>(element.Value);
            }
        }

        public static SparseList<T> ReadSparseList<T>(this Ibasa.IO.BinaryReader reader) where T : struct, IEquatable<T>
        {
            var list = new SparseList<T>();

            var count = reader.ReadInt32();
            list.Compressed.Capacity = count;
            for(int i=0; i<count; ++i)
            {
                var length = reader.ReadByte();
                var value = reader.Read<T>();

                list.Compressed.Add(new SparseList<T>.Element(length, value));
            }

            return list;
        }
    }
}
