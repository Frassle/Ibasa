using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Numerics.Random
{
    /// <summary>
    /// Represents an exponential distribution parameterized by lambda.
    /// </summary>
    public sealed class Exponential : Distribution
    {
        public double Lambda { get; set; }

        public Exponential(double lambda, Generator generator) : base(generator)
        {
            Lambda = lambda;
        }

        public override double Sample()
        {
            return -Functions.Log(Generator.NextDouble()) / Lambda;
        }
    }
}
