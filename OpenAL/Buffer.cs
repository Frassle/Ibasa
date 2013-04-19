using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.OpenAL
{
    public struct Buffer : IEquatable<Buffer>
    {
        public static readonly Buffer Null = new Buffer(0);

        public uint Id { get; private set; }

        public Buffer(uint id)
            : this()
        {
            if (Al.IsBuffer(id) == 0)
                throw new ArgumentException("id is not an OpenAL buffer identifier.", "id");

            Id = id;
        }

        public static Buffer Gen()
        {
            unsafe
            {
                uint id;
                Al.GenBuffers(1, &id);
                AlHelper.GetAlError(Al.GetError());
                return new Buffer(id);
            }
        }

        public static void Gen(Buffer[] buffers, int index, int count)
        {
            if (buffers == null)
                throw new ArgumentNullException("buffers");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", index, "index is less than 0.");
            if (index + count > buffers.Length)
                throw new ArgumentOutOfRangeException("count", count, "index + count is greater than buffers.Length.");

            unsafe
            {
                uint* ids = stackalloc uint[count];
                Al.GenBuffers(count, ids);
                AlHelper.GetAlError(Al.GetError());
                for (int i = 0; i < count; ++i)
                {
                    buffers[index + i] = new Buffer(ids[i]);
                }
            }
        }

        public void Delete()
        {
            AlHelper.ThrowNullException(Id);
            unsafe
            {
                uint id = Id;
                Al.DeleteBuffers(1, &id);
            }
        }

        public void BufferData(Format format, IntPtr data, int size, int frequency)
        {
            AlHelper.ThrowNullException(Id);
            if (data == IntPtr.Zero)
                throw new ArgumentNullException("data");

            unsafe
            {
                uint id = Id;
                Al.BufferData(Id, (int)format, data.ToPointer(), size, frequency);
            }
        }

        public void BufferData(Format format, byte[] data, int size, int frequency)
        {
            AlHelper.ThrowNullException(Id);
            if (data == null)
                throw new ArgumentNullException("data");

            unsafe
            {
                fixed(byte* pointer = data)
                {
                    uint id = Id;
                    Al.BufferData(Id, (int)format, pointer, size, frequency);
                }
            }
        }

        public int Bits
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Al.GetBufferi(Id, Al.BITS, &value);
                    return value;
                }
            }
        }

        public int Channels
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Al.GetBufferi(Id, Al.CHANNELS, &value);
                    return value;
                }
            }
        }

        public int Frequency
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Al.GetBufferi(Id, Al.FREQUENCY, &value);
                    return value;
                }
            }
        }

        public int Size
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Al.GetBufferi(Id, Al.SIZE, &value);
                    return value;
                }
            }
        }

        public override int GetHashCode()
        {
            AlHelper.ThrowNullException(Id);
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            AlHelper.ThrowNullException(Id);
            if (obj is Buffer)
            {
                return Equals((Buffer)obj);
            }
            return false;
        }

        public bool Equals(Buffer other)
        {
            AlHelper.ThrowNullException(Id);
            return Id == other.Id;
        }

        public static bool operator ==(Buffer left, Buffer right)
        {
            return left.Id == right.Id;
        }

        public static bool operator !=(Buffer left, Buffer right)
        {
            return left.Id != right.Id;
        }

        public override string ToString()
        {
            AlHelper.ThrowNullException(Id);
            return string.Format("Buffer: {0}", Id.ToString());
        }
    }
}