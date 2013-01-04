using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.SharpAL
{
    public sealed class Sound
    {
        #region Samples
        public double[] Samples { get; private set; }
        public int Channels { get; private set; }

        public double this[int index]
        {
            get
            {
                return Samples[index];
            }
            set
            {
                Samples[index] = value;
            }
        }

        public double this[int channel, int sample]
        {
            get
            {
                return Samples[(sample * Channels) + channel];
            }
            set
            {
                Samples[(sample * Channels) + channel] = value;
            }
        }
        #endregion

        #region Constructors

        public Sound(Sound source)
        {
            Contract.Requires(source != null);

            Channels = source.Channels;

            Samples = new double[source.Samples.Length];
            Array.Copy(source.Samples, Samples, Samples.Length);
        }

        public Sound(int channels, int samples)
        {
            Channels = channels;
            Samples = new double[samples * channels];
        }

        #endregion

        #region Clear
        public void Clear(double sample)
        {
            for (int i = 0; i < Samples.Length; ++i)
            {
                Samples[i] = sample;
            }
        }
        #endregion
    }
}
