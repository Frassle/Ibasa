using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Numerics.Random
{
    /// <summary>
    /// Represents a uniform distribution.
    /// </summary>
    public sealed class Uniform : Distribution
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public Uniform(Generator generator)
            : base(generator)
        {
            MinValue = 0.0;
            MaxValue = 1.0;
        }
        public Uniform(double maxValue, Generator generator)
            : base(generator)
        {
            MinValue = 0.0;
            MaxValue = maxValue;
        }
        public Uniform(double minValue, double maxValue, Generator generator)
            : base(generator)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public override double Sample()
        {
            return MinValue + Generator.NextDouble() * (MaxValue - MinValue);
        }
    }
}
