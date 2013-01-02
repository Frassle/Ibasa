using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio
{
    public sealed class Buffer : ALObject
    {
        private Buffer(int bid) : base(bid)
        {
        }

        public Buffer() : base(OpenTK.Audio.OpenAL.AL.GenBuffer())
        {
        }

        public override void Delete()
        {
            OpenTK.Audio.OpenAL.AL.DeleteSource(Id);
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
