using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ibasa;
using Ibasa.Numerics;

namespace Ibasa.Noise
{
    public sealed class Billow : Module<double>
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
        public double Persistance { get; set; }
        public double Frequency { get; set; }
        public double Lacunarity { get; set; }

        public Billow(Module<double> source,
            int octaves = 8, double persistance = 0.5, double frequency = 1.0, double lacunarity = 2.0)
        {
            Source = source;
            Octaves = octaves;
            Persistance = persistance;
            Frequency = frequency;
            Lacunarity = lacunarity;
        }

        public override double Evaluate(double x)
        {
            double total = 0.0;
            double frequency = Frequency;
            double amplitude = 1.0;

            for (int i = 0; i < Octaves; ++i)
            {
                total += Functions.Abs(Source.Evaluate(x * frequency) * amplitude);

                frequency *= Lacunarity;
                amplitude *= Persistance;
            }
            return total;
        }
        public override double Evaluate(double x, double y)
        {
            double total = 0.0;
            double frequency = Frequency;
            double amplitude = 1.0;

            for (int i = 0; i < Octaves; ++i)
            {
                total += Functions.Abs(Source.Evaluate(x * frequency, y * frequency) * amplitude);

                frequency *= Lacunarity;
                amplitude *= Persistance;
            }
            return total;
        }
        public override double Evaluate(double x, double y, double z)
        {
            double total = 0.0;
            double frequency = Frequency;
            double amplitude = 1.0;

            for (int i = 0; i < Octaves; ++i)
            {
                total += Functions.Abs(Source.Evaluate(x * frequency, y * frequency, z * frequency) * amplitude);

                frequency *= Lacunarity;
                amplitude *= Persistance;
            }
            return total;
        }
        public override double Evaluate(double x, double y, double z, double w)
        {
            double total = 0.0;
            double frequency = Frequency;
            double amplitude = 1.0;

            for (int i = 0; i < Octaves; ++i)
            {
                total += Functions.Abs(Source.Evaluate(x * frequency, y * frequency, z * frequency, w * frequency) * amplitude);

                frequency *= Lacunarity;
                amplitude *= Persistance;
            }
            return total;
        }
        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            double total = 0.0;
            double frequency = Frequency;
            double amplitude = 1.0;

            for (int i = 0; i < Octaves; ++i)
            {
                total += Functions.Abs(Source.Evaluate(x * frequency, y * frequency, z * frequency, w * frequency, v * frequency, u * frequency) * amplitude);

                frequency *= Lacunarity;
                amplitude *= Persistance;
            }
            return total;
        }
    }
}