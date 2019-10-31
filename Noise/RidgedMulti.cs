using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ibasa;
using Ibasa.Numerics;

namespace Ibasa.Noise
{
    public sealed class RidgedMulti : Module<double>
    {
        #region Source
        private Module<double> property_Source;
        /// <summary>
        /// Source module.
        /// </summary>
        public Module<double> Source
        {
            get { return property_Source; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source = value;
            }
        }
        #endregion

        public int Octaves { get; set; }
        public double Frequency { get; set; }
        public double Lacunarity { get; set; }
        public double Offset { get; set; }
        public double Gain { get; set; }
        public double Sharpness { get; set; }

        public RidgedMulti(Module<double> source,
            int octaves = 6, double frequency = 1.0, double lacunarity = 2.0, 
            double offset = 1.0, double gain = 2.0, double sharpness = 1.0)
        {
            Source = source;
            Octaves = octaves;
            Frequency = frequency;
            Lacunarity = lacunarity;
            Offset = offset;
            Gain = gain;
            Sharpness = sharpness;
        }

        public override double Evaluate(double x)
        {
            x *= Frequency;

            double value = 0.0;
            double weight = 1.0;

            double frequency = 1.0;
            for (int octave = 0; octave < Octaves; ++octave)
            {
                // Get the coherent-noise value.
                double signal = Source.Evaluate(x);

                // Make the ridges.
                signal = Functions.Abs(signal);
                signal = Offset - signal;

                // Square the signal to increase the sharpness of the ridges.
                signal *= signal;

                // The weighting from the previous octave is applied to the signal.
                // Larger values have higher weights, producing sharp points along the
                // ridges.
                signal *= weight;

                // Weight successive contributions by the previous signal.
                weight = Functions.Clamp(signal * Gain, 0.0, 1.0);

                double spectralWeight = Functions.Pow(frequency, -Sharpness);
                value += (signal * spectralWeight);

                x *= Lacunarity;
                frequency *= Lacunarity;
            }
            return (value * 1.25) - 1.0;
        }
        public override double Evaluate(double x, double y)
        {
            x *= Frequency;
            y *= Frequency;

            double value = 0.0;
            double weight = 1.0;

            double frequency = 1.0;
            for (int octave = 0; octave < Octaves; ++octave)
            {
                // Get the coherent-noise value.
                double signal = Source.Evaluate(x, y);

                // Make the ridges.
                signal = Functions.Abs(signal);
                signal = Offset - signal;

                // Square the signal to increase the sharpness of the ridges.
                signal *= signal;

                // The weighting from the previous octave is applied to the signal.
                // Larger values have higher weights, producing sharp points along the
                // ridges.
                signal *= weight;

                // Weight successive contributions by the previous signal.
                weight = Functions.Clamp(signal * Gain, 0.0, 1.0);

                double spectralWeight = Functions.Pow(frequency, -Sharpness);
                value += (signal * spectralWeight);

                x *= Lacunarity;
                y *= Lacunarity;
                frequency *= Lacunarity;
            }
            return (value * 1.25) - 1.0;
        }
        public override double Evaluate(double x, double y, double z)
        {
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;

            double value = 0.0;
            double weight = 1.0;

            double frequency = 1.0;
            for (int octave = 0; octave < Octaves; ++octave)
            {
                // Get the coherent-noise value.
                double signal = Source.Evaluate(x, y, z);

                // Make the ridges.
                signal = Functions.Abs(signal);
                signal = Offset - signal;

                // Square the signal to increase the sharpness of the ridges.
                signal *= signal;

                // The weighting from the previous octave is applied to the signal.
                // Larger values have higher weights, producing sharp points along the
                // ridges.
                signal *= weight;

                // Weight successive contributions by the previous signal.
                weight = Functions.Clamp(signal * Gain, 0.0, 1.0);

                double spectralWeight = Functions.Pow(frequency, -Sharpness);
                value += (signal * spectralWeight);

                x *= Lacunarity;
                y *= Lacunarity;
                z *= Lacunarity;
                frequency *= Lacunarity;
            }
            return (value * 1.25) - 1.0;
        }
        public override double Evaluate(double x, double y, double z, double w)
        {
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;
            w *= Frequency;

            double value = 0.0;
            double weight = 1.0;

            double frequency = 1.0;
            for (int octave = 0; octave < Octaves; ++octave)
            {
                // Get the coherent-noise value.
                double signal = Source.Evaluate(x, y, z, w);

                // Make the ridges.
                signal = Functions.Abs(signal);
                signal = Offset - signal;

                // Square the signal to increase the sharpness of the ridges.
                signal *= signal;

                // The weighting from the previous octave is applied to the signal.
                // Larger values have higher weights, producing sharp points along the
                // ridges.
                signal *= weight;

                // Weight successive contributions by the previous signal.
                weight = Functions.Clamp(signal * Gain, 0.0, 1.0);

                double spectralWeight = Functions.Pow(frequency, -Sharpness);
                value += (signal * spectralWeight);

                x *= Lacunarity;
                y *= Lacunarity;
                z *= Lacunarity;
                w *= Lacunarity;
                frequency *= Lacunarity;
            }
            return (value * 1.25) - 1.0;
        }
        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;
            w *= Frequency;
            v *= Frequency;
            u *= Frequency;

            double value = 0.0;
            double weight = 1.0;

            double frequency = 1.0;
            for (int octave = 0; octave < Octaves; ++octave)
            {
                // Get the coherent-noise value.
                double signal = Source.Evaluate(x, y, z, w, v, u);

                // Make the ridges.
                signal = Functions.Abs(signal);
                signal = Offset - signal;

                // Square the signal to increase the sharpness of the ridges.
                signal *= signal;

                // The weighting from the previous octave is applied to the signal.
                // Larger values have higher weights, producing sharp points along the
                // ridges.
                signal *= weight;

                // Weight successive contributions by the previous signal.
                weight = Functions.Clamp(signal * Gain, 0.0, 1.0);

                double spectralWeight = Functions.Pow(frequency, -Sharpness);
                value += (signal * spectralWeight);

                x *= Lacunarity;
                y *= Lacunarity;
                z *= Lacunarity;
                w *= Lacunarity;
                v *= Lacunarity;
                u *= Lacunarity;
                frequency *= Lacunarity;
            }
            return (value * 1.25) - 1.0;
        }
    }
}
