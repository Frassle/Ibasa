using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Numerics
{
    public static class Legendre
    {
        /// <summary>
        /// Calculates the l+1 legendre polynomial.
        /// </summary>
        /// <param name="l">The iteration currently computed up to.</param>
        /// <param name="x">The paramater to the polynomial.</param>
        /// <param name="p0">The value of P(l-1,x).</param>
        /// <param name="p1">The value of P(l,x).</param>
        /// <returns>The value of P(l+1,x)</returns>
        public static double Next(int l, double x, double p0, double p1)
        {
            return ((2 * l + 1) * x * p1 - l * p0) / (l + 1);
        }

        /// <summary>
        /// Calculates the l+1 legendre polynomial.
        /// </summary>
        /// <param name="l">The iteration currently computed up to.</param>
        /// <param name="m">The order.</param>
        /// <param name="x">The paramater to the polynomial.</param>
        /// <param name="p0">The value of P(l-1,m,x).</param>
        /// <param name="p1">The value of P(l,m,x).</param>
        /// <returns>The value of P(l+1,m,x)</returns>
        public static double Next(int l, int m, double x, double p0, double p1)
        {
            return ((2 * l + 1) * x * p1 - (l + m) * p0) / (l + 1 - m);
        }

        /// <summary>
        /// Calculates the legendre polynomial(l) with parameter x.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Polynomial(int l, double x)
        {
            Contract.Requires(-1 <= x && x <= 1);

            if (l == 0)
                return 1;

            double p0 = 1;
            double p1 = x;

            for (int n = 1; n < l; ++n)
            {
                var p2 = Next(n, x, p0, p1);

                p0 = p1;
                p1 = p2;
            }

            return p1;
        }

        /// <summary>
        /// Calculates the associated  legendre polynomial(m,l) with parameter x.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double AssociatedPolynomial(int l, int m, double x)
        {
            Contract.Requires(-1 <= x && x <= 1);

            if (l < 0)
                return AssociatedPolynomial(-l - 1, m, x);

            if (m < 0)
            {
                int sign = Functions.IsOdd(m) ? 1 : -1;
                return sign * Functions.GammaRatio(l + m + 1, l + 1 - m) * AssociatedPolynomial(l, -m, x);
            }

            if (m > l)
                return 0;
            if (m == 0)
                return Polynomial(l, x);

            double p0 = Functions.Factorial(Functions.Factorial(2 * m - 1));
            if (Functions.IsOdd(m))
                p0 *= -1;
            if (m == l)
                return p0;

            double p1 = x * (2 * m + 1) * p0;

            for (int n = m + 1; n < l; ++n)
            {
                var p2 = Next(n, m, x, p0, p1);

                p0 = p1;
                p1 = p2;
            }

            return p1;
        }
    }
}
