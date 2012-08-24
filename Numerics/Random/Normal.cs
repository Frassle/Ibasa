using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Numerics.Random
{
    /// <summary>
    /// Represents a normal distribution parameterized by mean and variance.
    /// </summary>
    public sealed class Normal : Distribution
    {
        double _variance;
        double _standardDeviation;

        public double Mean { get; set; }
        public double Variance
        {
            get { return _variance; }
            set
            {
                _variance = value;
                _standardDeviation = Functions.Sqrt(value);
            }
        }
        public double StandardDeviation
        {
            get { return _standardDeviation; }
            set
            {
                _variance = value * value;
                _standardDeviation = value;
            }
        }

        public Normal(Generator generator)
            : base(generator)
        {
            Mean = 0.0;
            Variance = 1.0;
        }

        public Normal(double mean , double variance, Generator generator)
            : base(generator)
        {
            Mean = mean;
            Variance = variance;
        }

        public override double Sample()
        {
            double u = Generator.NextDouble();
            double v = Generator.NextDouble();
            double z = Functions.Sqrt(-2.0 * Functions.Log(u)) * Functions.Cos(2.0 * Constants.PI * v);
            return Mean + StandardDeviation * z;
        }
    }
}
