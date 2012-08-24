using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Numerics.Random
{
    /// <summary>
    /// Represents an cauchy distribution parameterized by lambda.
    /// </summary>
    public sealed class Cauchy : Distribution
    {
        public double Alpha { get; set; }
        public double Beta { get; set; }

        private readonly Normal Normal;

        protected override void OnGeneratorSet(Generator generator)
        {
            Normal.Generator = generator;
        }

        public Cauchy(double alpha, double beta, Generator generator)
            : base(generator)
        {
            Alpha = alpha;
            Beta = beta;
            Normal = new Normal(Generator);
        }

        public override double Sample()
        {
            return (Normal.Sample() / Normal.Sample()) * Beta + Alpha;
        }
    }
}
