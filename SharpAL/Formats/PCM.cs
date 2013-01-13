using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.SharpAL.Formats
{
    public sealed class Pcm8 : Format<byte>
    {
        public Pcm8(int channels)
            : base("PCM8", channels, 0, 1, true, false)
        {
        }

        public override int GetByteCount(int samples)
        {
            return samples * Channels;
        }

        public override void GetDataBytes(byte[] source, int index, int samples, System.IO.Stream destination, int offset)
        {
            destination.Seek(offset, System.IO.SeekOrigin.Current);

            for (int i = 0; i < samples * Channels; ++i)
            {
                destination.WriteByte(source[index + i]);
            }
        }

        public override void GetBytes(double[] source, int index, int samples, System.IO.Stream destination, int offset)
        {
            destination.Seek(offset, System.IO.SeekOrigin.Current);

            for (int i = 0; i < samples * Channels; ++i)
            {
                destination.WriteByte((byte)(255.0 * source[index + i]));
            }
        }

        public override void GetData(System.IO.Stream source, int offset, int samples, byte[] destination, int index)
        {
            source.Seek(offset, System.IO.SeekOrigin.Current);

            for (int i = 0; i < samples * Channels; ++i)
            {
                int sample = source.ReadByte();

                if (sample == -1)
                    throw new System.IO.EndOfStreamException();

                destination[index + i] = (byte)sample;
            }
        }

        public override void GetSamples(System.IO.Stream source, int offset, int samples, double[] destination, int index)
        {
            source.Seek(offset, System.IO.SeekOrigin.Current);

            for (int i = 0; i < samples * Channels; ++i)
            {
                int sample = source.ReadByte();

                if (sample == -1)
                    throw new System.IO.EndOfStreamException();

                destination[index + i] = sample / 255.0;
            }
        }

        public override int GetSampleCount(int byteCount)
        {
            return byteCount / Channels;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Name, Channels, Channels == 1 ? "channel" : "channels");
        }
    }

    public sealed class Pcm16 : Format<short>
    {
        public Pcm16(int channels)
            : base("PCM16", channels, 0, 1, true, false)
        {
        }

        public override int GetByteCount(int samples)
        {
            return 2 * samples * Channels;
        }

        public override void GetDataBytes(short[] source, int index, int samples, System.IO.Stream destination, int offset)
        {
            destination.Seek(offset, System.IO.SeekOrigin.Current);

            for (int i = 0; i < samples * Channels; ++i)
            {
                var sample = source[index + i];

                destination.WriteByte((byte)sample);
                destination.WriteByte((byte)(sample >> 8));
            }
        }

        public override void GetBytes(double[] source, int index, int samples, System.IO.Stream destination, int offset)
        {
            destination.Seek(offset, System.IO.SeekOrigin.Current);

            for (int i = 0; i < samples * Channels; ++i)
            {
                var sample = (byte)(255.0 * source[index + i]);

                destination.WriteByte((byte)sample);
                destination.WriteByte((byte)(sample >> 8));
            }
        }

        public override void GetData(System.IO.Stream source, int offset, int samples, short[] destination, int index)
        {
            source.Seek(offset, System.IO.SeekOrigin.Current);

            for (int i = 0; i < samples * Channels; ++i)
            {
                int low = source.ReadByte();
                int high = source.ReadByte();

                if (low == -1 || high == -1)
                    throw new System.IO.EndOfStreamException();

                destination[index + i] = (short)((high << 8) | high);
            }
        }

        public override void GetSamples(System.IO.Stream source, int offset, int samples, double[] destination, int index)
        {
            source.Seek(offset, System.IO.SeekOrigin.Current);

            for (int i = 0; i < samples * Channels; ++i)
            {
                int low = source.ReadByte();
                int high = source.ReadByte();

                if (low == -1 || high == -1)
                    throw new System.IO.EndOfStreamException();

                destination[index + i] = ((high << 8) | high) / 255.0;
            }
        }

        public override int GetSampleCount(int byteCount)
        {
            return byteCount / (2 * Channels);
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Name, Channels, Channels == 1 ? "channel" : "channels");
        }
    }
}
