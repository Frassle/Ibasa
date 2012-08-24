using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Numerics.Random
{
    public sealed class Truncated : Distribution
    {
        public double Min { get; set; }
        public double Max { get; set; }

        private readonly Distribution Distribution;

        protected override void OnGeneratorSet(Generator generator)
        {
            Distribution.Generator = generator;
        }

        public Truncated(double min, double max, Distribution distribution)
            : base(distribution.Generator)
        {
            Distribution = distribution;
            Min = min;
            Max = max;
        }

        public override double Sample()
        {
            double sample = Min;
            while (sample <= Min || sample >= Max)
            {
                sample = Distribution.Sample();
            }
            return sample;
        }
    }
}
