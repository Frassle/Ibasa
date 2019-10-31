using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Ibasa.Noise
{
    /// <summary>
    /// Abstract base class for a noise module.
    /// </summary>
    public abstract class Module<T>
    {
        /// <summary>
        /// Evaluates 1D noise.
        /// </summary>
        /// <param name="x">The x position to sample noise from.</param>
        /// <returns>A noise sample.</returns>
        public abstract T Evaluate(double x);
        /// <summary>
        /// Evaluates 2D noise.
        /// </summary>
        /// <param name="x">The x position to sample noise from.</param>
        /// <param name="y">The y position to sample noise from.</param>
        /// <returns>A noise sample.</returns>
        public virtual T Evaluate(double x, double y)
        {
            return Evaluate(x);
        }
        /// <summary>
        /// Evaluates 3D noise.
        /// </summary>
        /// <param name="x">The x position to sample noise from.</param>
        /// <param name="y">The y position to sample noise from.</param>
        /// <param name="z">The z position to sample noise from.</param>
        /// <returns>A noise sample.</returns>
        public virtual T Evaluate(double x, double y, double z)
        {
            return Evaluate(x, y);
        }
        /// <summary>
        /// Evaluates 4D noise.
        /// </summary>
        /// <param name="x">The x position to sample noise from.</param>
        /// <param name="y">The y position to sample noise from.</param>
        /// <param name="z">The z position to sample noise from.</param>
        /// <param name="w">The w position to sample noise from.</param>
        /// <returns>A noise sample.</returns>
        public virtual T Evaluate(double x, double y, double z, double w)
        {
            return Evaluate(x, y, z);
        }
        /// <summary>
        /// Evaluates 6D noise.
        /// </summary>
        /// <param name="x">The x position to sample noise from.</param>
        /// <param name="y">The y position to sample noise from.</param>
        /// <param name="z">The z position to sample noise from.</param>
        /// <param name="w">The w position to sample noise from.</param>
        /// <param name="v">The v position to sample noise from.</param>
        /// <param name="u">The u position to sample noise from.</param>
        /// <returns>A noise sample.</returns>
        public virtual T Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return Evaluate(x, y, z, w);
        }
        
        /// <summary>
        /// A null module that always returns 0.
        /// </summary>
        public static readonly Module<T> Null = new NullModule();
        private sealed class NullModule : Module<T>
        {
            public override T Evaluate(double x)
            {
                return default(T);
            }
        }
    }
}
