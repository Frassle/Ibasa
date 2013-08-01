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
        public const double Pi = Math.PI;
        /// <summary>
        /// Represents the ratio of the circumference of a circle to its radius, specified
        /// by the constant, τ.
        /// </summary>
        public const double Tau = Pi * 2.0;
        /// <summary>
        /// The speed of light in a vacuum, in meters per second.
        /// </summary>
        public const double C = 299792458.0;
        /// <summary>
        /// The gravitational constant, in meters cubed per kilogram second squared.
        /// </summary>
        public const double G = 0.00000000006673848;
    }

    /// <summary>
    /// Provides common mathematical and physical constants.
    /// </summary>
    public static class ConstantsF
    {
        /// <summary>
        /// Represents the natural logarithmic base, specified by the constant, e.
        /// </summary>
        public const float E = (float)Constants.E;
        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified
        /// by the constant, π.
        /// </summary>
        public const float Pi = (float)Constants.Pi;
        /// <summary>
        /// Represents the ratio of the circumference of a circle to its radius, specified
        /// by the constant, τ.
        /// </summary>
        public const float Tau = (float)Constants.Tau;
        /// <summary>
        /// The speed of light in a vacuum, in meters per second.
        /// </summary>
        public const float C = (float)Constants.C;
        /// <summary>
        /// The gravitational constant, in meters cubed per kilogram second squared.
        /// </summary>
        public const double G = (float)Constants.G;
    }
}
