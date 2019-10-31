using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Noise
{
    public sealed class Fbm : Module<double>
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

        #region Octaves
        private int property_Octaves;
        /// <summary>
        /// How many octaves of noise to sum.
        /// </summary>
        public int Octaves
        {
            get { return property_Octaves; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("The value is zero or less.", "value");

                property_Octaves = value;
            }
        }
        #endregion
        /// <summary>
        /// Scales amplitude between octaves.
        /// </summary>
        public double Persistance { get; set; }
        /// <summary>
        /// The starting frequency to sample noise at.
        /// </summary>
        public double Frequency { get; set; }
        /// <summary>
        /// Scales frequency between octaves.
        /// </summary>
        public double Lacunarity { get; set; }

        public Fbm(Module<double> source,
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
                total += Source.Evaluate(x * frequency) * amplitude;

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
                total += Source.Evaluate(x * frequency, y * frequency) * amplitude;

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
                total += Source.Evaluate(x * frequency, y * frequency, z * frequency) * amplitude;

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
                total += Source.Evaluate(x * frequency, y * frequency, z * frequency, w * frequency) * amplitude;

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
                total += Source.Evaluate(x * frequency, y * frequency, z * frequency, w * frequency, v * frequency, u * frequency) * amplitude;

                frequency *= Lacunarity;
                amplitude *= Persistance;
            }
            return total;
        }
    }
}
