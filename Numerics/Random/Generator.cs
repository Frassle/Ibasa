using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Numerics.Random
{
    /// <summary>
    /// Represents a random number generator, a device that produces a sequence
    /// of numbers that meet certain statistical requirements for randomness.
    /// </summary>
    public abstract class Generator
    {
        /// <summary>
        /// The largest number returned by this generator.
        /// </summary>
        public abstract long Max { get; }

        /// <summary>
        /// Returns a nonnegative random number.
        /// </summary>
        /// <returns>
        /// A 64-bit signed integer greater than or equal to zero and less than Max.
        /// </returns>
        public abstract long Next();

        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number to be generated. maxValue
        /// must be greater than or equal to zero.
        /// </param>
        /// <returns>
        /// A 64-bit signed integer greater than or equal to zero, and less than maxValue;
        /// that is, the range of return values ordinarily includes zero but not maxValue.
        /// However, if maxValue equals zero, maxValue is returned.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// maxValue is less than zero.
        /// </exception>
        public long Next(long maxValue)
        {
            Contract.Requires(maxValue >= 0, "maxValue is less than zero.");

            return Next() % maxValue;
        }
        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be
        /// greater than or equal to minValue.</param>
        /// <returns>A 64-bit signed integer greater than or equal to minValue and less than maxValue;
        /// that is, the range of return values includes minValue but not maxValue. If
        /// minValue equals maxValue, minValue is returned.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">minValue is greater than maxValue.</exception>
        public long Next(long minValue, long maxValue)
        {
            Contract.Requires(maxValue >= 0, "maxValue is less than zero.");
            Contract.Requires(minValue >= 0, "minValue is less than zero.");
            Contract.Requires(minValue <= maxValue, "minValue is greater than maxValue.");

            return minValue + Next(maxValue - minValue);
        }

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        /// <exception cref="System.ArgumentNullException">buffer is null.</exception>
        public virtual void NextBytes(byte[] buffer)
        {
            Contract.Requires(buffer != null, "buffer is null.");

            int j = 4;
            long next = Next();

            for (int i = 0; i < buffer.Length; ++i)
            {
                if (j == 0)
                {
                    next = Next();
                    j = 4;
                }

                buffer[i] = (byte)(next);

                next >>= 8;
                --j;
            }
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns>
        /// A double-precision floating point number greater than or equal to 0.0, 
        /// and less than or equal to 1.0.
        /// </returns>
        public virtual double NextDouble()
        {
            return (1.0 / Max) * Next();
        }
    }
}
