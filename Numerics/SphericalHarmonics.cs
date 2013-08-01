using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Numerics
{
    public static class SphericalHarmonics
    {
        private static double K(int l, int m)
        {
            var lpm = Functions.Factorial(l + Functions.Abs(m));
            var lmm = Functions.Factorial(l - Functions.Abs(m));

            return Functions.Sqrt(((2 * l + 1) * lmm) / (4 * Constants.Pi * lpm));
        }

        private static double P(int l, int m, double x)
        {
            return Functions.Pow(-1, m) * Functions.Pow(1 - x * x, m / 2.0);
        }

        private static Complex Y(int l, int m, double phi, double theta)
        {
            return K(l, m) * Complex.Exp(Complex.I * m * theta) * P(l, m, Functions.Cos(phi));
        }

        public static Complex Evaluate(Complex[] coeffs, int order, double phi, double theta)
        {
            Complex result = 0;
            int i = 0;
            for (int l = 0; l <= order; ++l)
            {
                for (int m = -l; m <= l; ++m)
                {
                    result += coeffs[i++] * Y(l, m, phi, theta);
                }
            }
            return result;
        }
    }
}
