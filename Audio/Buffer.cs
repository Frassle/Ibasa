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

        private static OpenTK.Audio.OpenAL.ALFormat Cast(Ibasa.SharpAL.Format format)
        {
            if (format is Ibasa.SharpAL.Formats.PCM8)
            {
                switch (format.Channels)
                {
                    case 1:
                        return OpenTK.Audio.OpenAL.ALFormat.Mono8;
                    case 2:
                        return OpenTK.Audio.OpenAL.ALFormat.Stereo8;
                    case 4:
                        return OpenTK.Audio.OpenAL.ALFormat.MultiQuad8Ext;
                    case 6:
                        return OpenTK.Audio.OpenAL.ALFormat.Multi51Chn8Ext;
                    case 7:
                        return OpenTK.Audio.OpenAL.ALFormat.Multi61Chn8Ext;
                    case 8:
                        return OpenTK.Audio.OpenAL.ALFormat.Multi71Chn8Ext;
                    default:
                        throw new ArgumentException(string.Format("{0} channel 8 bit PCM not supported.", format.Channels), "format");
                }
            }

            throw new ArgumentException(string.Format("{0} not supported", format), "format");
        }

        public void BufferData(Ibasa.SharpAL.Format format, byte[] data, int count, int frequency)
        {
            OpenTK.Audio.OpenAL.AL.BufferData(Id, Cast(format), data, count, frequency);
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
