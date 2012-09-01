using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Numerics
{
		/// <summary>
		/// Provides common mathematical and physical constants.
		/// </summary>
    public static class Constants
    {
        /// <summary>
        /// Represents the natural logarithmic base, specified by the constant, e.
        /// </summary>
        public const double E = Math.E;
        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified
        /// by the constant, π.
        /// </summary>
        public const double PI = Math.PI;
        /// <summary>
        /// Represents the ratio of the circumference of a circle to its radius, specified
        /// by the constant, τ.
        /// </summary>
        public const double TAU = PI * 2.0;
        /// <summary>
        /// The speed of light in a vacuum, in meters per second.
        /// </summary>
        public const double C = 299792458.0;

        /// <summary>
        /// Represents the natural logarithmic base, specified by the constant, e.
        /// </summary>
        public const float Ef = (float)E;
        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified
        /// by the constant, π.
        /// </summary>
        public const float PIf = (float)PI;
        /// <summary>
        /// Represents the ratio of the circumference of a circle to its radius, specified
        /// by the constant, τ.
        /// </summary>
        public const float TAUf = (float)TAU;
        /// <summary>
        /// The speed of light in a vacuum, in meters per second.
        /// </summary>
        public const float Cf = (float)C;
    }
}
