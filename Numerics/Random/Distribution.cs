using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Numerics.Random
{
    /// <summary>
    /// Represents a pseudo-random number distribution.
    /// </summary>
    public abstract class Distribution
    {        
        #region Generator
        private Generator property_Generator;
        /// <summary>
        /// Generator to use to generate random numbers.
        /// </summary>
        public Generator Generator
        {
            get { return property_Generator; }
            set
            {
                global::System.Diagnostics.Contracts.Contract.Requires(value != null);
                property_Generator = value;
                OnGeneratorSet(value);
            }
        }

        protected virtual void OnGeneratorSet(Generator generator)
        {

        }
        #endregion

        /// <summary>
        /// Create a new distribution using the given generator for random numbers.
        /// </summary>
        /// <param name="generator">A random number generator.</param>
        protected Distribution(Generator generator)
        {
            Contract.Requires(generator != null);
            property_Generator = generator;
        }

        /// <summary>
        /// Sample the random distribution.
        /// </summary>
        /// <returns>A random number from this distribution.</returns>
        public abstract double Sample();        
    }
}
