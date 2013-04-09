using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Numerics
{
    public static partial class Functions
    {
        public static Complex Gamma(Complex z, int order = 10)
        {
            // ∫[0,∞] t^-z e^-t dt.

            var integral = Complex.Zero;

            double dt = 1.0 / order;
            double t = 0;
            
            for (int n = 0; n <= order; ++n)
            {   
                integral += Complex.Pow(t, z) * Functions.Exp(-t);
                t += dt;
            }

            return integral;
        }

        /// <summary>
        /// Calculates the natural logarithm of the gamma function.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [Pure]
        public static double GammaLn(double n)
        {
            //bounds for gammaln
            Contract.Requires(0.0 < n);
            Contract.Ensures(0.0 <= Contract.Result<double>());

            double x = n;
            double y = n + 1.0;
            double t = x + (671.0 / 128.0);

            t = (x + 0.5) * Log(t) - t;

            double ser =
                0.999999999999997092 +
                (57.1562356658629235 / (y + 0.0)) +
                (-59.5979603554754912 / (y + 1.0)) +
                (14.1360979747417471 / (y + 2.0)) +
                (-0.491913816097620199 / (y + 3.0)) +
                (0.0000339946499848118887 / (y + 4.0)) +
                (0.0000465236289270485756 / (y + 5.0)) +
                (-0.0000983744753048795646 / (y + 6.0)) +
                (0.000158088703224912494 / (y + 7.0)) +
                (-0.000210264441724104883 / (y + 8.0)) +
                (0.000217439618115212643 / (y + 9.0)) +
                (-0.00016431810653676389 / (y + 10.0)) +
                (0.0000844182239838527433 / (y + 11.0)) +
                (-0.0000261908384015814087 / (y + 12.0)) +
                (0.00000368991826595316234 / (y + 13.0));

            return t + Log(2.5066282746310005 * (ser / x));
        }
        
        /// <summary>
        /// Calculates Γ(x)/Γ(y)
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [Pure]
        public static double GammaRatio(long x, long y)
        {
            Contract.Requires(0 <= x);
            Contract.Requires(0 <= y);

            return FactorialRatio(x - 1, y - 1);
        }

        /// <summary>
        /// Calculates the beta function.
        /// </summary>
        /// <param name="z"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        [Pure]
        public static double Beta(double z, double w)
        {
            return Exp(GammaLn(z) + GammaLn(w) - GammaLn(z + w));
        }

        #region Private factorial table
        private static long[] FactorialTable = CalculateFactorialTable();
        private static long[] CalculateFactorialTable()
        {
            //Factorial(20) is as high as we can go before overflow
            var result = new long[21]; 

            result[0] = 1;

            for (int i = 1; i < result.Length; ++i)
            {
                result[i] = i * result[i - 1];
            }

            return result;
        }
        #endregion

        /// <summary>
        /// Calculates the factorial function.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [Pure]
        public static long Factorial(long n)
        {
            Contract.Requires(0 <= n);
            Contract.Requires(n <= 20);
            Contract.Ensures(1 <= Contract.Result<long>() && Contract.Result<long>() <= 2432902008176640000);

            return FactorialTable[n];
        }

        /// <summary>
        /// Calculates x!/y!
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [Pure]
        public static double FactorialRatio(long x, long y)
        {
            Contract.Requires(0 <= x);
            Contract.Requires(0 <= y);

            var start = Functions.Min(x, y);
            var end = Functions.Max(x, y);

            var result = 1L;

            for (var i = start + 1; i <= end; ++i)
            {
                result *= i;
            }

            return x > y ? result : 1.0 / result;
        }

        /// <summary>
        /// Calculates the natural logarithm of the factorial function.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [Pure]
        public static double LnFactorial(long n)
        {
            Contract.Requires(0 <= n);
            Contract.Ensures(0 <= Contract.Result<double>() && Contract.Result<double>() < double.MaxValue);

            return GammaLn(n + 1.0);
        }

        /// <summary>
        /// Calculates the natural logarithm binomial coefficient (n k).
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        [Pure]
        public static double LnBinomialCoefficient(long n, long k)
        {
            Contract.Requires(0 <= k && k <= n);
            Contract.Ensures(0 < Contract.Result<double>());

            return (LnFactorial(n) - (LnFactorial(k) + LnFactorial(n - k)));
        }

        /// <summary>
        /// Calculates the binomial coefficient (n k).
        /// </summary>
        /// <remarks>Will not overflow internally unless final result would overflow.</remarks>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        [Pure]
        public static long BinomialCoefficient(long n, long k)
        {
            Contract.Ensures(0 <= Contract.Result<long>());

            if (k > n)
                return 0;

            long r = 1;
            for (long d = 1; d <= k; d++)
            {
                r *= n--;
                r /= d;
            }
            return r;
        }
    }
}
