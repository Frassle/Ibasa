using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.SharpAL.Formats
{
    public sealed class Float32 : Format<float>
    {
        public Float32(int channels)
            : base("Float32", channels, 0, 1, true, false)
        {
        }

        public override int GetByteCount(int samples)
        {
            return 4 * samples * Channels;
        }

        public override void GetDataBytes(float[] source, int index, int samples, System.IO.Stream destination, int offset)
        {
            destination.Seek(offset, System.IO.SeekOrigin.Current);

            byte[] buffer = new byte[4];

            for (int i = 0; i < samples * Channels; ++i)
            {
                Ibasa.BitConverter.GetBytes(buffer, 0, source[index + i]);

                destination.Write(buffer, 0, 4);
            }
        }

        public override void GetBytes(double[] source, int index, int samples, System.IO.Stream destination, int offset)
        {
            destination.Seek(offset, System.IO.SeekOrigin.Current);

            byte[] buffer = new byte[4];

            for (int i = 0; i < samples * Channels; ++i)
            {
                Ibasa.BitConverter.GetBytes(buffer, 0, (float)source[index + i]);

                destination.Write(buffer, 0, 4);
            }
        }

        public override void GetData(System.IO.Stream source, int offset, int samples, float[] destination, int index)
        {
            source.Seek(offset, System.IO.SeekOrigin.Current);

            byte[] buffer = new byte[4];

            for (int i = 0; i < samples * Channels; ++i)
            {
                if(source.Read(buffer, 0, 4) != 4)
                    throw new System.IO.EndOfStreamException();

                destination[index + i] = BitConverter.ToSingle(buffer, 0);
            }
        }

        public override void GetSamples(System.IO.Stream source, int offset, int samples, double[] destination, int index)
        {
            source.Seek(offset, System.IO.SeekOrigin.Current);

            byte[] buffer = new byte[4];

            for (int i = 0; i < samples * Channels; ++i)
            {
                if (source.Read(buffer, 0, 4) != 4)
                    throw new System.IO.EndOfStreamException();

                destination[index + i] = BitConverter.ToSingle(buffer, 0);
            }
        }

        public override int GetSampleCount(int byteCount)
        {
            return byteCount / (4 * Channels);
        }
    }

    public sealed class Float64 : Format<double>
    {
        public Float64(int channels)
            : base("Float64", channels, 0, 1, true, false)
        {
        }

        public override int GetByteCount(int samples)
        {
            return 8 * samples * Channels;
        }

        public override void GetDataBytes(double[] source, int index, int samples, System.IO.Stream destination, int offset)
        {
            destination.Seek(offset, System.IO.SeekOrigin.Current);

            byte[] buffer = new byte[8];

            for (int i = 0; i < samples * Channels; ++i)
            {
                Ibasa.BitConverter.GetBytes(buffer, 0, source[index + i]);

                destination.Write(buffer, 0, 8);
            }
        }

        public override void GetBytes(double[] source, int index, int samples, System.IO.Stream destination, int offset)
        {
            destination.Seek(offset, System.IO.SeekOrigin.Current);

            byte[] buffer = new byte[8];

            for (int i = 0; i < samples * Channels; ++i)
            {
                Ibasa.BitConverter.GetBytes(buffer, 0, source[index + i]);

                destination.Write(buffer, 0, 8);
            }
        }

        public override void GetData(System.IO.Stream source, int offset, int samples, double[] destination, int index)
        {
            source.Seek(offset, System.IO.SeekOrigin.Current);

            byte[] buffer = new byte[8];

            for (int i = 0; i < samples * Channels; ++i)
            {
                if (source.Read(buffer, 0, 8) != 8)
                    throw new System.IO.EndOfStreamException();

                destination[index + i] = BitConverter.ToDouble(buffer, 0);
            }
        }

        public override void GetSamples(System.IO.Stream source, int offset, int samples, double[] destination, int index)
        {
            source.Seek(offset, System.IO.SeekOrigin.Current);

            byte[] buffer = new byte[8];

            for (int i = 0; i < samples * Channels; ++i)
            {
                if (source.Read(buffer, 0, 8) != 8)
                    throw new System.IO.EndOfStreamException();

                destination[index + i] = BitConverter.ToSingle(buffer, 0);
            }
        }

        public override int GetSampleCount(int byteCount)
        {
            return byteCount / (8 * Channels);
        }
    }
}
