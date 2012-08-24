using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Numerics.Random
{
    /// <summary>
    /// Represents a discrete distribution.
    /// </summary>
    public sealed class Discrete : Distribution
    {
        private struct Alias : IComparable<Alias>
        {
            public int Lower;
            public int Upper;
            public double Threshold;

            public int CompareTo(Alias other)
            {
                return Threshold.CompareTo(other.Threshold);
            }
        }

        private Alias[] Aliases;

        public Discrete(double[] eventProbabilities, Generator generator) : base(generator)
        {
            Contract.Requires(eventProbabilities != null);
            Contract.Requires(Enumerable.Sum(eventProbabilities) == 1.0);

            Aliases = new Alias[eventProbabilities.Length];

            var high = new HashSet<int>();
            var low = new HashSet<int>();

            for (int i = 0; i < Aliases.Length; ++i)
            {
                Aliases[i].Lower = i;
                Aliases[i].Upper = i;
                Aliases[i].Threshold = Aliases.Length * eventProbabilities[i];

                if (Aliases[i].Threshold > 1.0)
                    high.Add(i);
                else
                    low.Add(i);
            }

            while(high.Count != 0)
            {
                int j = low.First();
                int k = high.First();

                Aliases[j].Upper = k;
                Aliases[k].Threshold = Aliases[k].Threshold + Aliases[j].Threshold - 1.0;

                if (Aliases[k].Threshold < 1.0)
                    low.Add(k);
                else //Aliases[k].Threshold >= 1.0
                    high.Remove(k);

                low.Remove(j);
            }
        }

        public override double Sample()
        {
            long index = Generator.Next(Aliases.Length);
            double threshold = Generator.NextDouble();

            Alias alias = Aliases[index];

            return threshold <= alias.Threshold ? alias.Lower : alias.Upper;
        }
    }
}
