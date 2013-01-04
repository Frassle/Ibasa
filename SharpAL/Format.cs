using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.IO;

namespace Ibasa.SharpAL
{
    public abstract class Format<T> : Format
    {
        public Format(string name, int channels)
            : base(name, channels)
        { }
        public Format(string name, int channels, double min, double max, bool isNormalized)
            : base(name, channels, min, max, isNormalized)
        {
        }
        public Format(string name, int channels, double min, double max, bool isNormalized, bool isCompressed)
            : base(name, channels, min, max, isNormalized, isCompressed)
        {
        }

        public abstract void GetDataBytes(
            T[] source, int index, int samples,
            Stream destination, int offset);

        public virtual void GetDataBytes(
            T[] source, int index, int samples,
            byte[] destination, int offset)
        {
            GetDataBytes(
                source, index, samples,
                new MemoryStream(destination), offset);
        }

        public abstract void GetData(
            Stream source, int offset, int samples,
            T[] destination, int index);

        public virtual void GetData(
            byte[] source, int offset, int samples,
            T[] destination, int index)
        {
            GetData(
                new MemoryStream(source), offset, samples,
                destination, index);
        }
    }

    //[ContractClass(typeof(FormatContracts))]
    public abstract class Format
    {
        public static void Convert(Format sourceFormat, Format destinationFormat,
            Stream source, int sourceOffset, int samples,
            Stream destination, int destinationOffset)
        {
            Contract.Requires(sourceFormat != null);
            Contract.Requires(destinationFormat != null);

            double[] intermediate = new double[samples];

            sourceFormat.GetSamples(
                source, sourceOffset, samples,
                intermediate, 0);

            destinationFormat.GetBytes(
                intermediate, 0, samples,
                destination, destinationOffset);
        }

        public static void Convert(Format sourceFormat, Format destinationFormat,
            byte[] source, int sourceOffset, int samples,
            byte[] destination, int destinationOffset)
        {
            Convert(sourceFormat, destinationFormat,
                new MemoryStream(source), sourceOffset, samples,
                new MemoryStream(destination), destinationOffset);
        }

        public Format(string name, int channels)
            : this(name, channels, 0, 1, true)
        { }
        public Format(string name, int channels, double min, double max, bool isNormalized)
            : this(name, channels, min, max, isNormalized, false)
        {
        }
        public Format(string name, int channels, double min, double max, bool isNormalized, bool isCompressed)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(name), "name cannot be null or whitespace.");
            Contract.Requires(max >= min, "max must be greater than or equal to min");
            Contract.Requires(channels > 0, "channels must be greater than zero.");

            Name = name;
            Min = min;
            Max = max;
            Channels = channels;
            IsNormalized = isNormalized;
            IsCompressed = isCompressed;
        }

        public string Name { get; private set; }
        public double Min { get; private set; }
        public double Max { get; private set; }
        public int Channels { get; private set; }
        public bool IsNormalized { get; private set; }
        public bool IsCompressed { get; private set; }

        public abstract int GetByteCount(int samples);

        public abstract void GetBytes(
            double[] source, int index, int samples,
            Stream destination, int offset);

        public virtual void GetBytes(
            double[] source, int index, int samples,
            byte[] destination, int offset)
        {
            GetBytes(
                source, index, samples,
                new MemoryStream(destination), offset);
        }

        public abstract int GetSampleCount(int byteCount);

        public abstract void GetSamples(
            Stream source, int offset, int samples,
            double[] destination, int index);

        public virtual void GetSamples(
            byte[] source, int offset, int samples,
            double[] destination, int index)
        {
            GetSamples(
                new MemoryStream(source), offset, samples,
                destination, index);
        }
    }
}
