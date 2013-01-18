using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Audio
{
    public struct Buffer
    {
        internal uint Id { get; private set; }

        public static readonly Buffer Null = new Buffer(0);

        internal Buffer(uint bid)
            : this()
        {
            Id = bid;
            Contract.Assert(Id == 0 || OpenTK.Audio.OpenAL.AL.IsBuffer(Id));
        }

        public static Buffer Gen()
        {
            return new Buffer(OpenTK.Audio.OpenAL.AL.GenBuffer());
        }

        public void Delete()
        {
            OpenTK.Audio.OpenAL.AL.DeleteBuffer(Id);
            Context.ThrowIfError();
        }

        public void BufferData(Ibasa.SharpAL.Format format, byte[] data, int count, int frequency)
        {
            OpenTK.Audio.OpenAL.AL.BufferData(Id, OpenAL.Format(format), data, count, frequency);
            Context.ThrowIfError();
        }

        public int Bits
        {
            get
            {
                int value;
                OpenTK.Audio.OpenAL.AL.GetBuffer(Id, OpenTK.Audio.OpenAL.ALGetBufferi.Bits, out value);
                return value;
            }
        }

        public int Channels
        {
            get
            {
                int value;
                OpenTK.Audio.OpenAL.AL.GetBuffer(Id, OpenTK.Audio.OpenAL.ALGetBufferi.Channels, out value);
                return value;
            }
        }

        public int Frequency
        {
            get
            {
                int value;
                OpenTK.Audio.OpenAL.AL.GetBuffer(Id, OpenTK.Audio.OpenAL.ALGetBufferi.Frequency, out value);
                return value;
            }
        }

        public int Size
        {
            get
            {
                int value;
                OpenTK.Audio.OpenAL.AL.GetBuffer(Id, OpenTK.Audio.OpenAL.ALGetBufferi.Size, out value);
                return value;
            }
        }
    }
}
