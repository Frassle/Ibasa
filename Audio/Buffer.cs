using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio
{
    public sealed class Buffer
    {
        internal readonly int Bid;

        private Buffer(int bid)
        {
            Bid = bid;
        }

        public Buffer()
        {
            Bid = OpenTK.Audio.OpenAL.AL.GenSource();
        }

        ~Buffer()
        {
            Dispose();
        }

        public void Dispose()
        {
            OpenTK.Audio.OpenAL.AL.DeleteSource(Bid);
            GC.SuppressFinalize(this);
        }

        Buffer[] Create(int count)
        {
            var names = OpenTK.Audio.OpenAL.AL.GenBuffers(count);
            Buffer[] buffers = new Buffer[count];
            for (int i = 0; i < count; ++i)
            {
                buffers[i] = new Buffer(names[i]);
            }
            return buffers;
        }

        public void BufferData(byte[] data, int size, int frequency)
        {
            OpenTK.Audio.OpenAL.AL.BufferData(Bid, OpenTK.Audio.OpenAL.ALFormat.Mono8, data, size, frequency);
        }

        public int Bits
        {
            get
            {
                int value;
                OpenTK.Audio.OpenAL.AL.GetBuffer(Bid, OpenTK.Audio.OpenAL.ALGetBufferi.Bits, out value);
                return value;
            }
        }

        public int Channels
        {
            get
            {
                int value;
                OpenTK.Audio.OpenAL.AL.GetBuffer(Bid, OpenTK.Audio.OpenAL.ALGetBufferi.Channels, out value);
                return value;
            }
        }

        public int Frequency
        {
            get
            {
                int value;
                OpenTK.Audio.OpenAL.AL.GetBuffer(Bid, OpenTK.Audio.OpenAL.ALGetBufferi.Frequency, out value);
                return value;
            }
        }

        public int Size
        {
            get
            {
                int value;
                OpenTK.Audio.OpenAL.AL.GetBuffer(Bid, OpenTK.Audio.OpenAL.ALGetBufferi.Size, out value);
                return value;
            }
        }
    }
}
