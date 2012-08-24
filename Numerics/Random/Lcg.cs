using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Numerics.Random
{
    /// <summary>
    /// Linear congruential pseudo random number generator.
    /// </summary>
    public sealed class Lcg : Generator
    {
        private long x;

        /// <summary>
        /// The largest number returned by this generator.
        /// Equal to System.UInt32.MaxValue.
        /// </summary>
        public override long Max
        {
            get { return uint.MaxValue; }
        }

        /// <summary>
        /// Initializes a new instance of the Lcg class, using a time-dependent
        /// default seed value.
        /// </summary>
        public Lcg()
            : this(DateTime.Now.Ticks)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Lcg class, using the specified
        /// seed value.
        /// </summary>
        /// <param name="seed">
        /// A number used to calculate a starting value for the pseudo-random number
        /// sequence. If a negative number is specified, the absolute value of the number
        /// is used.
        /// </param>
        public Lcg(long seed)
        {
            x = seed;
        }

        /// <summary>
        /// Returns a nonnegative random number.
        /// </summary>
        /// <returns>
        /// A 64-bit signed integer greater than or equal to zero and less than System.UInt32.MaxValue.
        /// </returns>
        public override long Next()
        {
            x = (6364136223846793005 * x + 1442695040888963407);
            return (x >> 32);
        }
    }
}
