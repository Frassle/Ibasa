using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Numerics.Geometry
{
    /// <summary>
    /// Represents a polar coordinate, given by azimuth and radius.
    /// </summary>
    public struct PolarCoordinate
    {
        /// <summary>
        /// Radius.
        /// </summary>
        /// <remarks>
        /// The value of rho is always 0 or greater.
        /// </remarks>
        public readonly double Rho;
        /// <summary>
        /// Azimuth angle, in radians.
        /// </summary>
        /// <remarks>
        /// The value of theta is always between 0 and 2π.
        /// </remarks>
        public readonly double Theta;

        public PolarCoordinate(double theta, double rho = 1)
        {
            Contract.Requires(0 <= rho);
            Contract.Requires(0 <= theta && theta <= 2 * Constants.PI);

            Rho = rho;
            Theta = theta;
        }
    }
}
