using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Numerics.Random
{
    /// <summary>
    /// Mersenne twister pseudo random number generator.
    /// </summary>
    public sealed class MersenneTwister : Generator
    {
        private ulong u, v, w;

        /// <summary>
        /// The largest number returned by this generator.
        /// Equal to System.Int64.MaxValue.
        /// </summary>
        public override long Max
        {
            get { return long.MaxValue; }
        }

        /// <summary>
        /// Initializes a new instance of the MersenneTwister class, using a time-dependent
        /// default seed value.
        /// </summary>
        public MersenneTwister()
            : this(DateTime.Now.Ticks)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MersenneTwister class, using the specified
        /// seed value.
        /// </summary>
        /// <param name="seed">
        /// A number used to calculate a starting value for the pseudo-random number
        /// sequence. If a negative number is specified, the absolute value of the number
        /// is used.
        /// </param>
        public MersenneTwister(long seed)
        {
            w = 1;
            v = 4101842887655102017;
            u = (ulong)seed ^ v; Next();
            v = u; Next();
            w = v; Next();
        }

        /// <summary>
        /// Returns a nonnegative random number.
        /// </summary>
        /// <returns>
        /// A 64-bit signed integer greater than or equal to zero and less than System.Int64.MaxValue.
        /// </returns>
        public sealed override long Next()
        {
            u = u * 2862933555777941757 + 7046029254386353087;
            v ^= v >> 17; v ^= v << 31; v ^= v >> 8;
            w = 4294957665 * (w & 0xFFFFFFFF) + (w >> 32);
            ulong x = u ^ (u << 21); x ^= x >> 35; x ^= x << 4;
            return (long)((x + v) ^ w) & long.MaxValue;
        }
    }
}
