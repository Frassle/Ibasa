using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.OpenAL
{
    public struct Buffer
    {
        internal uint Id { get; private set; }

        public static readonly Buffer Null = new Buffer(0);

        internal Buffer(uint bid)
            : this()
        {
            Id = bid;
            Contract.Assert(Id == 0 || Al.IsBuffer(Id));
        }

        public static Buffer Gen()
        {
            return new Buffer(Al.GenBuffer());
        }

        public void Delete()
        {
            Al.DeleteBuffer(Id);
        }

        public void BufferData(int format, byte[] data, int count, int frequency)
        {
            Al.BufferData(Id, format, data, count, frequency);
        }

        public int Bits
        {
            get
            {
                int value;
                Al.GetBuffer(Id, AlGetBufferi.Bits, out value);
                return value;
            }
        }

        public int Channels
        {
            get
            {
                int value;
                Al.GetBuffer(Id, AlGetBufferi.Channels, out value);
                return value;
            }
        }

        public int Frequency
        {
            get
            {
                int value;
                Al.GetBuffer(Id, AlGetBufferi.Frequency, out value);
                return value;
            }
        }

        public int Size
        {
            get
            {
                int value;
                Al.GetBuffer(Id, AlGetBufferi.Size, out value);
                return value;
            }
        }
    }
}