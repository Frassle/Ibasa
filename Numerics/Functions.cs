using System;
using System.Diagnostics.Contracts;
using System.Security;
using System.Runtime;
using System.Runtime.ConstrainedExecution;

namespace Ibasa.Numerics
{
    public static class Functions
    {
        #region Tests
        /// <summary>
        /// Checks if a number is a power of 2.
        /// </summary>
        /// <param name="value">A number to check.</param>
        /// <returns>true if value is a power of two, otherwise false.</returns>
        [Pure]
        public static bool IsPowerOf2(int value)
        {
            return (value & (value - 1)) == 0;
        }

        /// <summary>
        /// Checks if a number is a power of 2.
        /// </summary>
        /// <param name="value">A number to check.</param>
        /// <returns>true if value is a power of two, otherwise false.</returns>
        [Pure]
        public static bool IsPowerOf2(long value)
        {
            return (value & (value - 1)) == 0;
        }

        [Pure]
        public static bool IsOdd(int value)
        {
            return (value & 1) == 1;
        }

        [Pure]
        public static bool IsEven(int value)
        {
            return (value & 1) == 0;
        }
        #endregion

        #region Trigonometric
        /// <summary>
        /// Converts a number in radians to degrees.
        /// </summary>
        /// <param name="radians">A number in radians.</param>
        /// <returns>The number converted to degrees.</returns>
        [Pure]
        public static double ToDegrees(double radians)
        {
            return radians * 360.0 / Constants.TAU;
        }
        /// <summary>
        /// Converts a number in degrees to radians.
        /// </summary>
        /// <param name="degrees">A number in degrees.</param>
        /// <returns>The number converted to radians.</returns>
        [Pure]
        public static double ToRadians(double degrees)
        {
            return degrees * Constants.TAU / 360.0;
        }

        #region Sin, Cos, Tan
        /// <summary>
        /// Returns the sine of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>
        /// The sine of a. 
        /// If a is equal to System.Double.NaN, System.Double.NegativeInfinity,
        /// or System.Double.PositiveInfinity, this method returns System.Double.NaN.
        /// </returns>
        [Pure]
        public static double Sin(double a)
        {
            return Math.Sin(a);
        }
        /// <summary>
        /// Returns the sine of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>
        /// The sine of a. 
        /// If a is equal to System.Single.NaN, System.Single.NegativeInfinity,
        /// or System.Single.PositiveInfinity, this method returns System.Single.NaN.</returns>
        [Pure]
        public static float Sin(float a)
        {
            return (float)Math.Sin(a);
        }
        /// <summary>
        /// Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>
        /// The cosine of a. 
        /// If a is equal to System.Double.NaN, System.Double.NegativeInfinity,
        /// or System.Double.PositiveInfinity, this method returns System.Double.NaN.
        /// </returns>
        [Pure]
        public static double Cos(double a)
        {
            return Math.Cos(a);
        }
        /// <summary>
        /// Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>
        /// The cosine of a. 
        /// If a is equal to System.Single.NaN, System.Single.NegativeInfinity,
        /// or System.Single.PositiveInfinity, this method returns System.Single.NaN.
        /// </returns>
        [Pure]
        public static float Cos(float a)
        {
            return (float)Math.Cos(a);
        }
        /// <summary>
        /// Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>
        /// The tangent of a. 
        /// If a is equal to System.Double.NaN, System.Double.NegativeInfinity,
        /// or System.Double.PositiveInfinity, this method returns System.Double.NaN.
        /// </returns>
        [Pure]
        public static double Tan(double a)
        {
            return Math.Tan(a);
        }
        /// <summary>
        /// Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>
        /// The tangent of a. 
        /// If a is equal to System.Single.NaN, System.Single.NegativeInfinity,
        /// or System.Single.PositiveInfinity, this method returns System.Single.NaN.
        /// </returns>
        [Pure]
        public static float Tan(float a)
        {
            return (float)Math.Tan(a);
        }
        #endregion

        #region Acos, Asin, Atan
        /// <summary>
        /// Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="x">A number representing a cosine, where -1 ≤ x ≤ 1.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that 0 ≤ θ ≤ π
        /// or System.Double.NaN if x &lt; -1 or x > 1.
        /// </returns>
        [Pure]
        public static double Acos(double x)
        {
            return Math.Acos(x);
        }
        /// <summary>
        /// Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="x">A number representing a cosine, where -1 ≤ x ≤ 1.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that 0 ≤ θ ≤ π
        /// or System.Single.NaN if x &lt; -1 or x > 1.
        /// </returns>
        [Pure]
        public static float Acos(float x)
        {
            return (float)Math.Acos(x);
        }
        /// <summary>
        /// Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="x">A number representing a sine, where -1 ≤ x ≤ 1.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2
        /// or System.Double.NaN if x &lt; -1 or x > 1.
        /// </returns>
        [Pure]
        public static double Asin(double x)
        {
            return Math.Asin(x);
        }
        /// <summary>
        /// Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="x">A number representing a sine, where -1 ≤ x ≤ 1.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2
        /// or System.Single.NaN if x &lt; -1 or x > 1.
        /// </returns>
        [Pure]
        public static float Asin(float x)
        {
            return (float)Math.Asin(x);
        }
        /// <summary>
        /// Returns the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="x">A number representing a tangent.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2.
        /// or System.Double.NaN if x equals System.Double.NaN, 
        /// or -π/2 if x equals System.Double.NegativeInfinity, 
        /// or π/2 if x equals System.Double.PositiveInfinity.
        /// </returns>
        [Pure]
        public static double Atan(double x)
        {
            return Math.Atan(x);
        }
        /// <summary>
        /// Returns the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="x">A number representing a tangent.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2.
        /// or System.Single.NaN if x equals System.Single.NaN, 
        /// or -π/2 if x equals System.Single.NegativeInfinity, 
        /// or π/2 if x equals System.Single.PositiveInfinity.
        /// </returns>
        [Pure]
        public static float Atan(float x)
        {
            return (float)Math.Atan(x);
        }
        /// <summary>
        /// Returns the angle whose tangent is the quotient of two specified numbers.
        /// </summary>
        /// <param name="y">The y coordinate of a point.</param>
        /// <param name="x">The x coordinate of a point.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -π ≤ θ ≤ π, and tan(θ) = y / x, where
        /// (x, y) is a point in the Cartesian plane. 
        /// Observe the following: 
        /// For (x, y) in quadrant 1, 0 &lt; θ &lt; π/2.
        /// For (x, y) in quadrant 2, π/2 &lt; θ ≤ π.
        /// For (x, y) in quadrant 3, -π &lt; θ &lt; -π/2.
        /// For (x, y) in quadrant 4, -π/2 &lt; θ &lt; 0.
        /// For points on the boundaries of the quadrants, the return value is the following:
        /// If y is 0 and x is not negative, θ = 0.
        /// If y is 0 and x is negative, θ = π.
        /// If y is positive and x is 0, θ = π/2.
        /// If y is negative and x is 0, θ = -π/2.
        /// </returns>
        [Pure]
        public static double Atan2(double y, double x)
        {
            return Math.Atan2(y, x);
        }
        /// <summary>
        /// Returns the angle whose tangent is the quotient of two specified numbers.
        /// </summary>
        /// <param name="y">The y coordinate of a point.</param>
        /// <param name="x">The x coordinate of a point.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -π ≤ θ ≤ π, and tan(θ) = y / x, where
        /// (x, y) is a point in the Cartesian plane. 
        /// Observe the following: 
        /// For (x, y) in quadrant 1, 0 &lt; θ &lt; π/2.
        /// For (x, y) in quadrant 2, π/2 &lt; θ ≤ π.
        /// For (x, y) in quadrant 3, -π &lt; θ &lt; -π/2.
        /// For (x, y) in quadrant 4, -π/2 &lt; θ &lt; 0.
        /// For points on the boundaries of the quadrants, the return value is the following:
        /// If y is 0 and x is not negative, θ = 0.
        /// If y is 0 and x is negative, θ = π.
        /// If y is positive and x is 0, θ = π/2.
        /// If y is negative and x is 0, θ = -π/2.
        /// </returns>
        [Pure]
        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }
        #endregion

        #region Cosh, Sinh, Tanh
        /// <summary>
        /// Returns the hyperbolic cosine of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>
        /// The hyperbolic cosine of value. 
        /// If value is equal to System.Double.NegativeInfinity
        /// or System.Double.PositiveInfinity, System.Double.PositiveInfinity is returned.
        /// If value is equal to System.Double.NaN, System.Double.NaN is returned.
        /// </returns>
        [Pure]
        public static double Cosh(double a)
        {
            return Math.Cosh(a);
        }
        /// <summary>
        /// Returns the hyperbolic cosine of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>
        /// The hyperbolic cosine of value. 
        /// If value is equal to System.Single.NegativeInfinity
        /// or System.Single.PositiveInfinity, System.Single.PositiveInfinity is returned.
        /// If value is equal to System.Single.NaN, System.Single.NaN is returned.
        /// </returns>
        [Pure]
        public static float Cosh(float a)
        {
            return (float)Math.Cosh(a);
        }
        /// <summary>
        /// Returns the hyperbolic sine of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>
        /// The hyperbolic sine of value. 
        /// If value is equal to System.Double.NegativeInfinity,
        /// System.Double.PositiveInfinity, or System.Double.NaN, 
        /// this method returns a System.Double equal to value.
        /// </returns>
        [Pure]
        public static double Sinh(double a)
        {
            return Math.Sinh(a);
        }
        /// <summary>
        /// Returns the hyperbolic sine of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>
        /// The hyperbolic sine of value. 
        /// If value is equal to System.Single.NegativeInfinity,
        /// System.Single.PositiveInfinity, or System.Single.NaN, 
        /// this method returns a System.Single equal to value.
        /// </returns>
        [Pure]
        public static float Sinh(float a)
        {
            return (float)Math.Sinh(a);
        }
        /// <summary>
        /// Returns the hyperbolic tangent of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>
        /// The hyperbolic tangent of value. 
        /// If value is equal to System.Double.NegativeInfinity, this method returns -1. 
        /// If value is equal to System.Double.PositiveInfinity, this method returns 1. 
        /// If value is equal to System.Double.NaN, this method returns System.Double.NaN.
        /// </returns>
        [Pure]
        public static double Tanh(double a)
        {
            return Math.Tanh(a);
        }
        /// <summary>
        /// Returns the hyperbolic tangent of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        /// <returns>
        /// The hyperbolic tangent of value. 
        /// If value is equal to System.Single.NegativeInfinity, this method returns -1. 
        /// If value is equal to System.Single.PositiveInfinity, this method returns 1. 
        /// If value is equal to System.Single.NaN, this method returns System.Single.NaN.
        /// </returns>
        [Pure]
        public static float Tanh(float a)
        {
            return (float)Math.Tanh(a);
        }
        #endregion

        #region Acosh, Asinh, Atanh
        /// <summary>
        /// Returns the hyperbolic angle whose hyperbolic cosine is the specified number.
        /// </summary>
        /// <param name="x">A number representing a hyperbolic cosine, where 1 ≤ x.</param>
        /// <returns>A hyperbolic angle, θ, measured in radians.</returns>
        /// <returns>System.Double.NaN if x &lt; 1.</returns>
        [Pure]
        public static double Acosh(double x)
        {
            return Log(x + Sqrt(x * x - 1.0));
        }
        /// <summary>
        /// Returns the hyperbolic angle whose hyperbolic cosine is the specified number.
        /// </summary>
        /// <param name="x">A number representing a hyperbolic cosine, where 1 ≤ x.</param>
        /// <returns>A hyperbolic angle, θ, measured in radians.</returns>
        /// <returns>System.Single.NaN if x &lt; 1.</returns>
        [Pure]
        public static float Acosh(float x)
        {
            return Log(x + Sqrt(x * x - 1.0f));
        }
        /// <summary>
        /// Returns the hyperbolic angle whose hyperbolic sine is the specified number.
        /// </summary>
        /// <param name="x">A number representing a hyperbolic sine.</param>
        /// <returns>A hyperbolic angle, θ, measured in radians.</returns>
        [Pure]
        public static double Asinh(double x)
        {
            return Log(x + Sqrt(x * x + 1.0));
        }
        /// <summary>
        /// Returns the hyperbolic angle whose hyperbolic sine is the specified number.
        /// </summary>
        /// <param name="x">A number representing a hyperbolic sine.</param>
        /// <returns>A hyperbolic angle, θ, measured in radians.</returns>
        [Pure]
        public static float Asinh(float x)
        {
            return Log(x + Sqrt(x * x + 1.0f));
        }
        /// <summary>
        /// Returns the hyperbolic angle whose hyperbolic tangent is the specified number.
        /// </summary>
        /// <param name="x">A number representing a hyperbolic tangent, where |x| ≤ 1.</param>
        /// <returns>A hyperbolic angle, θ, measured in radians.</returns>
        /// <returns>System.Double.NaN if |x| ≥ 1.</returns>
        [Pure]
        public static double Atanh(double x)
        {
            return Log((1.0 + x) / (1.0 - x)) / 2.0;
        }
        /// <summary>
        /// Returns the hyperbolic angle whose hyperbolic tangent is the specified number.
        /// </summary>
        /// <param name="x">A number representing a hyperbolic tangent, where |x| ≤ 1.</param>
        /// <returns>A hyperbolic angle, θ, measured in radians.</returns>
        /// <returns>System.Single.NaN if |d| ≥ 1.</returns>
        [Pure]
        public static float Atanh(float x)
        {
            return Log((1.0f + x) / (1.0f - x)) / 2.0f;
        }
        #endregion
        #endregion

        #region Exponential
        /// <summary>
        /// Returns e raised to the specified power.
        /// </summary>
        /// <param name="x">A number specifying a power.</param>
        /// <returns>
        /// The number e raised to the power x. 
        /// If x equals System.Double.NaN or System.Double.PositiveInfinity, that value is returned. 
        /// If x equals System.Double.NegativeInfinity, 0 is returned.
        /// </returns>
        [Pure]
        public static double Exp(double x)
        {
            return Math.Exp(x);
        }
        /// <summary>
        /// Returns e raised to the specified power.
        /// </summary>
        /// <param name="x">A number specifying a power.</param>
        /// <returns>
        /// The number e raised to the power x. 
        /// If x equals System.Single.NaN or System.Single.PositiveInfinity, that value is returned. 
        /// If x equals System.Single.NegativeInfinity, 0 is returned.
        /// </returns>
        [Pure]
        public static float Exp(float x)
        {
            return (float)Math.Exp(x);
        } 
        /// <summary>
        /// Returns the natural (base e) logarithm of a specified number.
        /// </summary>
        /// <param name="x">A number whose logarithm is to be found.</param>
        /// <returns>
        /// One of the values in the following table. 
        /// d parameterReturn value Positive
        /// The natural logarithm of d; that is, ln d, or log edZero System.Double.NegativeInfinityNegative
        /// System.Double.NaNEqual to System.Double.NaNSystem.Double.NaNEqual to System.Double.PositiveInfinitySystem.Double.PositiveInfinity
        /// </returns>
        [Pure]
        public static double Log(double x)
        {
            return Math.Log(x);
        }
        /// <summary>
        /// Returns the natural (base e) logarithm of a specified number.
        /// </summary>
        /// <param name="x">A number whose logarithm is to be found.</param>
        [Pure]
        public static float Log(float x)
        {
            return (float)Math.Log(x);
        }

        //
        // Summary:
        //     Returns the logarithm of a specified number in a specified base.
        //
        // Parameters:
        //   a:
        //     A number whose logarithm is to be found.
        //
        //   newBase:
        //     The base of the logarithm.
        //
        // Returns:
        //     One of the values in the following table. (+Infinity denotes System.Double.PositiveInfinity,
        //     -Infinity denotes System.Double.NegativeInfinity, and NaN denotes System.Double.NaN.)anewBaseReturn
        //     valuea> 0(0 <newBase< 1) -or-(newBase> 1)lognewBase(a)a< 0(any value)NaN(any
        //     value)newBase< 0NaNa != 1newBase = 0NaNa != 1newBase = +InfinityNaNa = NaN(any
        //     value)NaN(any value)newBase = NaNNaN(any value)newBase = 1NaNa = 00 <newBase<
        //     1 +Infinitya = 0newBase> 1-Infinitya = +Infinity0 <newBase< 1-Infinitya =
        //     +InfinitynewBase> 1+Infinitya = 1newBase = 00a = 1newBase = +Infinity0
        public static double Log(double x, double logBase)
        {
            return Math.Log(x, logBase);
        }
        //
        // Summary:
        //     Returns the logarithm of a specified number in a specified base.
        //
        // Parameters:
        //   a:
        //     A number whose logarithm is to be found.
        //
        //   newBase:
        //     The base of the logarithm.
        //
        // Returns:
        //     One of the values in the following table. (+Infinity denotes System.Double.PositiveInfinity,
        //     -Infinity denotes System.Double.NegativeInfinity, and NaN denotes System.Double.NaN.)anewBaseReturn
        //     valuea> 0(0 <newBase< 1) -or-(newBase> 1)lognewBase(a)a< 0(any value)NaN(any
        //     value)newBase< 0NaNa != 1newBase = 0NaNa != 1newBase = +InfinityNaNa = NaN(any
        //     value)NaN(any value)newBase = NaNNaN(any value)newBase = 1NaNa = 00 <newBase<
        //     1 +Infinitya = 0newBase> 1-Infinitya = +Infinity0 <newBase< 1-Infinitya =
        //     +InfinitynewBase> 1+Infinitya = 1newBase = 00a = 1newBase = +Infinity0
        public static float Log(double x, float logBase)
        {
            return (float)Math.Log(x, logBase);
        }
        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="x">A double-precision floating-point number to be raised to a power.</param>
        /// <param name="y">A double-precision floating-point number that specifies a power.</param>
        /// <returns>The number x raised to the power y.</returns>
        [Pure]
        public static double Pow(double x, double y)
        {
            return Math.Pow(x, y);
        }
        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="x">A single-precision floating-point number to be raised to a power.</param>
        /// <param name="y">A single-precision floating-point number that specifies a power.</param>
        /// <returns>The number x raised to the power y.</returns>
        [Pure]
        public static float Pow(float x, float y)
        {
            return (float)Math.Pow(x, y);
        }
        /// <summary>
        /// Returns the square root of a specified number.
        /// </summary>
        /// <param name="x">A number.</param>
        /// <returns>
        /// One of the values in the following table. d parameter Return value Zero,
        /// or positive The positive square root of d. Negative System.Double.NaNEquals
        /// System.Double.NaNSystem.Double.NaNEquals System.Double.PositiveInfinitySystem.Double.PositiveInfinity
        /// </returns>
        [Pure]
        public static double Sqrt(double x)
        {
            return Math.Sqrt(x);
        }
        /// <summary>
        /// Returns the square root of a specified number.
        /// </summary>
        /// <param name="x">A number.</param>
        /// <returns>
        /// </returns>
        [Pure]
        public static float Sqrt(float x)
        {
            return (float)Math.Sqrt(x);
        }
        #endregion

        #region Comparisons
        #region Max
        //
        // Summary:
        //     Returns the larger of two 8-bit unsigned integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 8-bit unsigned integers to compare.
        //
        //   val2:
        //     The second of two 8-bit unsigned integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is larger.
        public static byte Max(byte val1, byte val2)
        {
            return Math.Max(val1, val2);
        }
        //
        // Summary:
        //     Returns the larger of two decimal numbers.
        //
        // Parameters:
        //   val1:
        //     The first of two System.Decimal numbers to compare.
        //
        //   val2:
        //     The second of two System.Decimal numbers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is larger.
        public static decimal Max(decimal val1, decimal val2)
        {
            return Math.Max(val1, val2);
        }
        //
        // Summary:
        //     Returns the larger of two double-precision floating-point numbers.
        //
        // Parameters:
        //   val1:
        //     The first of two double-precision floating-point numbers to compare.
        //
        //   val2:
        //     The second of two double-precision floating-point numbers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is larger. If val1, val2, or both val1
        //     and val2 are equal to System.Double.NaN, System.Double.NaN is returned.
        public static double Max(double val1, double val2)
        {
            return Math.Max(val1, val2);
        }
        //
        // Summary:
        //     Returns the larger of two single-precision floating-point numbers.
        //
        // Parameters:
        //   val1:
        //     The first of two single-precision floating-point numbers to compare.
        //
        //   val2:
        //     The second of two single-precision floating-point numbers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is larger. If val1, or val2, or both val1
        //     and val2 are equal to System.Single.NaN, System.Single.NaN is returned.
        public static float Max(float val1, float val2)
        {
            return Math.Max(val1, val2);
        }
        //
        // Summary:
        //     Returns the larger of two 32-bit signed integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 32-bit signed integers to compare.
        //
        //   val2:
        //     The second of two 32-bit signed integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is larger.
        public static int Max(int val1, int val2)
        {
            return Math.Max(val1, val2);
        }
        //
        // Summary:
        //     Returns the larger of two 64-bit signed integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 64-bit signed integers to compare.
        //
        //   val2:
        //     The second of two 64-bit signed integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is larger.
        public static long Max(long val1, long val2)
        {
            return Math.Max(val1, val2);
        }
        //
        // Summary:
        //     Returns the larger of two 8-bit signed integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 8-bit signed integers to compare.
        //
        //   val2:
        //     The second of two 8-bit signed integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is larger.
        [CLSCompliant(false)]
        public static sbyte Max(sbyte val1, sbyte val2)
        {
            return Math.Max(val1, val2);
        }
        //
        // Summary:
        //     Returns the larger of two 16-bit signed integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 16-bit signed integers to compare.
        //
        //   val2:
        //     The second of two 16-bit signed integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is larger.
        public static short Max(short val1, short val2)
        {
            return Math.Max(val1, val2);
        }
        //
        // Summary:
        //     Returns the larger of two 32-bit unsigned integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 32-bit unsigned integers to compare.
        //
        //   val2:
        //     The second of two 32-bit unsigned integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is larger.
        [CLSCompliant(false)]
        public static uint Max(uint val1, uint val2)
        {
            return Math.Max(val1, val2);
        }
        //
        // Summary:
        //     Returns the larger of two 64-bit unsigned integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 64-bit unsigned integers to compare.
        //
        //   val2:
        //     The second of two 64-bit unsigned integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is larger.
        [CLSCompliant(false)]
        public static ulong Max(ulong val1, ulong val2)
        {
            return Math.Max(val1, val2);
        }
        //
        // Summary:
        //     Returns the larger of two 16-bit unsigned integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 16-bit unsigned integers to compare.
        //
        //   val2:
        //     The second of two 16-bit unsigned integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is larger.
        [CLSCompliant(false)]
        public static ushort Max(ushort val1, ushort val2)
        {
            return Math.Max(val1, val2);
        }
        #endregion

        #region Min
        //
        // Summary:
        //     Returns the smaller of two 8-bit unsigned integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 8-bit unsigned integers to compare.
        //
        //   val2:
        //     The second of two 8-bit unsigned integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller.
        public static byte Min(byte val1, byte val2)
        {
            return Math.Min(val1, val2);
        }
        //
        // Summary:
        //     Returns the smaller of two decimal numbers.
        //
        // Parameters:
        //   val1:
        //     The first of two System.Decimal numbers to compare.
        //
        //   val2:
        //     The second of two System.Decimal numbers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller.
        public static decimal Min(decimal val1, decimal val2)
        {
            return Math.Min(val1, val2);
        }
        //
        // Summary:
        //     Returns the smaller of two double-precision floating-point numbers.
        //
        // Parameters:
        //   val1:
        //     The first of two double-precision floating-point numbers to compare.
        //
        //   val2:
        //     The second of two double-precision floating-point numbers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller. If val1, val2, or both val1
        //     and val2 are equal to System.Double.NaN, System.Double.NaN is returned.
        public static double Min(double val1, double val2)
        {
            return Math.Min(val1, val2);
        }
        //
        // Summary:
        //     Returns the smaller of two single-precision floating-point numbers.
        //
        // Parameters:
        //   val1:
        //     The first of two single-precision floating-point numbers to compare.
        //
        //   val2:
        //     The second of two single-precision floating-point numbers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller. If val1, val2, or both val1
        //     and val2 are equal to System.Single.NaN, System.Single.NaN is returned.
        public static float Min(float val1, float val2)
        {
            return Math.Min(val1, val2);
        }
        //
        // Summary:
        //     Returns the smaller of two 32-bit signed integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 32-bit signed integers to compare.
        //
        //   val2:
        //     The second of two 32-bit signed integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller.
        public static int Min(int val1, int val2)
        {
            return Math.Min(val1, val2);
        }
        //
        // Summary:
        //     Returns the smaller of two 64-bit signed integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 64-bit signed integers to compare.
        //
        //   val2:
        //     The second of two 64-bit signed integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller.
        public static long Min(long val1, long val2)
        {
            return Math.Min(val1, val2);
        }
        //
        // Summary:
        //     Returns the smaller of two 8-bit signed integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 8-bit signed integers to compare.
        //
        //   val2:
        //     The second of two 8-bit signed integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller.
        [CLSCompliant(false)]
        public static sbyte Min(sbyte val1, sbyte val2)
        {
            return Math.Min(val1, val2);
        }
        //
        // Summary:
        //     Returns the smaller of two 16-bit signed integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 16-bit signed integers to compare.
        //
        //   val2:
        //     The second of two 16-bit signed integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller.
        public static short Min(short val1, short val2)
        {
            return Math.Min(val1, val2);
        }
        //
        // Summary:
        //     Returns the smaller of two 32-bit unsigned integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 32-bit unsigned integers to compare.
        //
        //   val2:
        //     The second of two 32-bit unsigned integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller.
        [CLSCompliant(false)]
        public static uint Min(uint val1, uint val2)
        {
            return Math.Min(val1, val2);
        }
        //
        // Summary:
        //     Returns the smaller of two 64-bit unsigned integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 64-bit unsigned integers to compare.
        //
        //   val2:
        //     The second of two 64-bit unsigned integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller.
        [CLSCompliant(false)]
        public static ulong Min(ulong val1, ulong val2)
        {
            return Math.Min(val1, val2);
        }
        //
        // Summary:
        //     Returns the smaller of two 16-bit unsigned integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 16-bit unsigned integers to compare.
        //
        //   val2:
        //     The second of two 16-bit unsigned integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller.
        [CLSCompliant(false)]
        public static ushort Min(ushort val1, ushort val2)
        {
            return Math.Min(val1, val2);
        }
        #endregion

        #region Sign
        //
        // Summary:
        //     Returns a value indicating the sign of a decimal number.
        //
        // Parameters:
        //   value:
        //     A signed System.Decimal number.
        //
        // Returns:
        //     A number that indicates the sign of value, as shown in the following table.Return
        //     value Meaning -1 value is less than zero. 0 value is equal to zero. 1 value
        //     is greater than zero.
        public static int Sign(decimal value)
        {
            return Math.Sign(value);
        }
        //
        // Summary:
        //     Returns a value indicating the sign of a double-precision floating-point
        //     number.
        //
        // Parameters:
        //   value:
        //     A signed number.
        //
        // Returns:
        //     A number that indicates the sign of value, as shown in the following table.Return
        //     value Meaning -1 value is less than zero. 0 value is equal to zero. 1 value
        //     is greater than zero.
        //
        // Exceptions:
        //   System.ArithmeticException:
        //     value is equal to System.Double.NaN.
        public static int Sign(double value)
        {
            return Math.Sign(value);
        }
        //
        // Summary:
        //     Returns a value indicating the sign of a single-precision floating-point
        //     number.
        //
        // Parameters:
        //   value:
        //     A signed number.
        //
        // Returns:
        //     A number that indicates the sign of value, as shown in the following table.Return
        //     value Meaning -1 value is less than zero. 0 value is equal to zero. 1 value
        //     is greater than zero.
        //
        // Exceptions:
        //   System.ArithmeticException:
        //     value is equal to System.Single.NaN.
        public static int Sign(float value)
        {
            return Math.Sign(value);
        }
        //
        // Summary:
        //     Returns a value indicating the sign of a 32-bit signed integer.
        //
        // Parameters:
        //   value:
        //     A signed number.
        //
        // Returns:
        //     A number that indicates the sign of value, as shown in the following table.Return
        //     value Meaning -1 value is less than zero. 0 value is equal to zero. 1 value
        //     is greater than zero.
        public static int Sign(int value)
        {
            return Math.Sign(value);
        }
        //
        // Summary:
        //     Returns a value indicating the sign of a 64-bit signed integer.
        //
        // Parameters:
        //   value:
        //     A signed number.
        //
        // Returns:
        //     A number that indicates the sign of value, as shown in the following table.Return
        //     value Meaning -1 value is less than zero. 0 value is equal to zero. 1 value
        //     is greater than zero.
        public static int Sign(long value)
        {
            return Math.Sign(value);
        }
        //
        // Summary:
        //     Returns a value indicating the sign of an 8-bit signed integer.
        //
        // Parameters:
        //   value:
        //     A signed number.
        //
        // Returns:
        //     A number that indicates the sign of value, as shown in the following table.Return
        //     value Meaning -1 value is less than zero. 0 value is equal to zero. 1 value
        //     is greater than zero.
        [CLSCompliant(false)]
        public static int Sign(sbyte value)
        {
            return Math.Sign(value);
        }
        //
        // Summary:
        //     Returns a value indicating the sign of a 16-bit signed integer.
        //
        // Parameters:
        //   value:
        //     A signed number.
        //
        // Returns:
        //     A number that indicates the sign of value, as shown in the following table.Return
        //     value Meaning -1 value is less than zero. 0 value is equal to zero. 1 value
        //     is greater than zero.
        public static int Sign(short value)
        {
            return Math.Sign(value);
        }
        #endregion
        #endregion

        #region Rounding
        #region Ceiling, Floor, Truncate
        //
        // Summary:
        //     Returns the smallest integral value that is greater than or equal to the
        //     specified decimal number.
        //
        // Parameters:
        //   d:
        //     A decimal number.
        //
        // Returns:
        //     The smallest integral value that is greater than or equal to d. Note that
        //     this method returns a System.Decimal instead of an integral type.
        public static decimal Ceiling(decimal d)
        {
            return Math.Ceiling(d);
        }
        //
        // Summary:
        //     Returns the smallest integral value that is greater than or equal to the
        //     specified double-precision floating-point number.
        //
        // Parameters:
        //   a:
        //     A double-precision floating-point number.
        //
        // Returns:
        //     The smallest integral value that is greater than or equal to a. If a is equal
        //     to System.Double.NaN, System.Double.NegativeInfinity, or System.Double.PositiveInfinity,
        //     that value is returned. Note that this method returns a System.Double instead
        //     of an integral type.
        public static double Ceiling(double a)
        {
            return Math.Ceiling(a);
        }
        //
        // Summary:
        //     Returns the smallest integral value that is greater than or equal to the
        //     specified double-precision floating-point number.
        //
        // Parameters:
        //   a:
        //     A double-precision floating-point number.
        //
        // Returns:
        //     The smallest integral value that is greater than or equal to a. If a is equal
        //     to System.Double.NaN, System.Double.NegativeInfinity, or System.Double.PositiveInfinity,
        //     that value is returned. Note that this method returns a System.Double instead
        //     of an integral type.
        public static float Ceiling(float a)
        {
            return (float)Math.Ceiling(a);
        }
        //
        // Summary:
        //     Returns the largest integer less than or equal to the specified decimal number.
        //
        // Parameters:
        //   d:
        //     A decimal number.
        //
        // Returns:
        //     The largest integer less than or equal to d.
        public static decimal Floor(decimal d)
        {
            return Math.Floor(d);
        }
        //
        // Summary:
        //     Returns the largest integer less than or equal to the specified double-precision
        //     floating-point number.
        //
        // Parameters:
        //   d:
        //     A double-precision floating-point number.
        //
        // Returns:
        //     The largest integer less than or equal to d. If d is equal to System.Double.NaN,
        //     System.Double.NegativeInfinity, or System.Double.PositiveInfinity, that value
        //     is returned.
        public static double Floor(double d)
        {
            return Math.Floor(d);
        }
        //
        // Summary:
        //     Returns the largest integer less than or equal to the specified double-precision
        //     floating-point number.
        //
        // Parameters:
        //   d:
        //     A double-precision floating-point number.
        //
        // Returns:
        //     The largest integer less than or equal to d. If d is equal to System.Double.NaN,
        //     System.Double.NegativeInfinity, or System.Double.PositiveInfinity, that value
        //     is returned.
        public static float Floor(float d)
        {
            return (float)Math.Floor(d);
        }
        //
        // Summary:
        //     Calculates the integral part of a specified decimal number.
        //
        // Parameters:
        //   d:
        //     A number to truncate.
        //
        // Returns:
        //     The integral part of d; that is, the number that remains after any fractional
        //     digits have been discarded.
        public static decimal Truncate(decimal d)
        {
            return Math.Truncate(d);
        }
        //
        // Summary:
        //     Calculates the integral part of a specified double-precision floating-point
        //     number.
        //
        // Parameters:
        //   d:
        //     A number to truncate.
        //
        // Returns:
        //     The integral part of d; that is, the number that remains after any fractional
        //     digits have been discarded.
        public static double Truncate(double d)
        {
            return Math.Truncate(d);
        }
        //
        // Summary:
        //     Calculates the integral part of a specified double-precision floating-point
        //     number.
        //
        // Parameters:
        //   d:
        //     A number to truncate.
        //
        // Returns:
        //     The integral part of d; that is, the number that remains after any fractional
        //     digits have been discarded.
        public static float Truncate(float d)
        {
            return (float)Math.Truncate(d);
        }
        #endregion
        #region Round
        //
        // Summary:
        //     Rounds a decimal value to the nearest integral value.
        //
        // Parameters:
        //   d:
        //     A decimal number to be rounded.
        //
        // Returns:
        //     The integer nearest parameter d. If the fractional component of d is halfway
        //     between two integers, one of which is even and the other odd, the even number
        //     is returned. Note that this method returns a System.Decimal instead of an
        //     integral type.
        //
        // Exceptions:
        //   System.OverflowException:
        //     The result is outside the range of a System.Decimal.
        public static decimal Round(decimal d)
        {
            return Math.Round(d);
        }
        //
        // Summary:
        //     Rounds a double-precision floating-point value to the nearest integral value.
        //
        // Parameters:
        //   a:
        //     A double-precision floating-point number to be rounded.
        //
        // Returns:
        //     The integer nearest a. If the fractional component of a is halfway between
        //     two integers, one of which is even and the other odd, then the even number
        //     is returned. Note that this method returns a System.Double instead of an
        //     integral type.
        public static double Round(double a)
        {
            return Math.Round(a);
        }
        //
        // Summary:
        //     Rounds a double-precision floating-point value to the nearest integral value.
        //
        // Parameters:
        //   a:
        //     A double-precision floating-point number to be rounded.
        //
        // Returns:
        //     The integer nearest a. If the fractional component of a is halfway between
        //     two integers, one of which is even and the other odd, then the even number
        //     is returned. Note that this method returns a System.Double instead of an
        //     integral type.
        public static float Round(float a)
        {
            return (float)Math.Round(a);
        }
        //
        // Summary:
        //     Rounds a decimal value to a specified number of fractional digits.
        //
        // Parameters:
        //   d:
        //     A decimal number to be rounded.
        //
        //   decimals:
        //     The number of decimal places in the return value.
        //
        // Returns:
        //     The number nearest to d that contains a number of fractional digits equal
        //     to decimals.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     decimals is less than 0 or greater than 28.
        //
        //   System.OverflowException:
        //     The result is outside the range of a System.Decimal.
        public static decimal Round(decimal d, int decimals)
        {
            return Math.Round(d, decimals);
        }
        //
        // Summary:
        //     Rounds a decimal value to the nearest integer. A parameter specifies how
        //     to round the value if it is midway between two other numbers.
        //
        // Parameters:
        //   d:
        //     A decimal number to be rounded.
        //
        //   mode:
        //     Specification for how to round d if it is midway between two other numbers.
        //
        // Returns:
        //     The integer nearest d. If d is halfway between two numbers, one of which
        //     is even and the other odd, then mode determines which of the two is returned.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     mode is not a valid value of System.MidpointRounding.
        //
        //   System.OverflowException:
        //     The result is outside the range of a System.Decimal.
        public static decimal Round(decimal d, MidpointRounding mode)
        {
            return Math.Round(d, mode);
        }
        //
        // Summary:
        //     Rounds a double-precision floating-point value to a specified number of fractional
        //     digits.
        //
        // Parameters:
        //   value:
        //     A double-precision floating-point number to be rounded.
        //
        //   digits:
        //     The number of fractional digits in the return value.
        //
        // Returns:
        //     The number nearest to value that contains a number of fractional digits equal
        //     to digits.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     digits is less than 0 or greater than 15.
        public static double Round(double value, int digits)
        {
            return Math.Round(value, digits);
        }
        //
        // Summary:
        //     Rounds a double-precision floating-point value to the nearest integer. A
        //     parameter specifies how to round the value if it is midway between two other
        //     numbers.
        //
        // Parameters:
        //   value:
        //     A double-precision floating-point number to be rounded.
        //
        //   mode:
        //     Specification for how to round value if it is midway between two other numbers.
        //
        // Returns:
        //     The integer nearest value. If value is halfway between two integers, one
        //     of which is even and the other odd, then mode determines which of the two
        //     is returned.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     mode is not a valid value of System.MidpointRounding.
        public static double Round(double value, MidpointRounding mode)
        {
            return Math.Round(value, mode);
        }
        //
        // Summary:
        //     Rounds a double-precision floating-point value to a specified number of fractional
        //     digits.
        //
        // Parameters:
        //   value:
        //     A double-precision floating-point number to be rounded.
        //
        //   digits:
        //     The number of fractional digits in the return value.
        //
        // Returns:
        //     The number nearest to value that contains a number of fractional digits equal
        //     to digits.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     digits is less than 0 or greater than 15.
        public static float Round(float value, int digits)
        {
            return (float)Math.Round(value, digits);
        }
        //
        // Summary:
        //     Rounds a double-precision floating-point value to the nearest integer. A
        //     parameter specifies how to round the value if it is midway between two other
        //     numbers.
        //
        // Parameters:
        //   value:
        //     A double-precision floating-point number to be rounded.
        //
        //   mode:
        //     Specification for how to round value if it is midway between two other numbers.
        //
        // Returns:
        //     The integer nearest value. If value is halfway between two integers, one
        //     of which is even and the other odd, then mode determines which of the two
        //     is returned.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     mode is not a valid value of System.MidpointRounding.
        public static float Round(float value, MidpointRounding mode)
        {
            return (float)Math.Round(value, mode);
        }
        //
        // Summary:
        //     Rounds a decimal value to a specified number of fractional digits. A parameter
        //     specifies how to round the value if it is midway between two other numbers.
        //
        // Parameters:
        //   d:
        //     A decimal number to be rounded.
        //
        //   decimals:
        //     The number of decimal places in the return value.
        //
        //   mode:
        //     Specification for how to round d if it is midway between two other numbers.
        //
        // Returns:
        //     The number nearest to d that contains a number of fractional digits equal
        //     to decimals. If the number of fractional digits in d is less than decimals,
        //     d is returned unchanged.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     decimals is less than 0 or greater than 28.
        //
        //   System.ArgumentException:
        //     mode is not a valid value of System.MidpointRounding.
        //
        //   System.OverflowException:
        //     The result is outside the range of a System.Decimal.
        public static decimal Round(decimal d, int decimals, MidpointRounding mode)
        {
            return Math.Round(d, decimals, mode);
        }
        //
        // Summary:
        //     Rounds a double-precision floating-point value to the specified number of
        //     fractional digits. A parameter specifies how to round the value if it is
        //     midway between two other numbers.
        //
        // Parameters:
        //   value:
        //     A double-precision floating-point number to be rounded.
        //
        //   digits:
        //     The number of fractional digits in the return value.
        //
        //   mode:
        //     Specification for how to round value if it is midway between two other numbers.
        //
        // Returns:
        //     The number nearest to value that has a number of fractional digits equal
        //     to digits. If the number of fractional digits in value is less than digits,
        //     value is returned unchanged.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     digits is less than 0 or greater than 15.
        //
        //   System.ArgumentException:
        //     mode is not a valid value of System.MidpointRounding.
        public static double Round(double value, int digits, MidpointRounding mode)
        {
            return Math.Round(value, digits, mode);
        }
        //
        // Summary:
        //     Rounds a double-precision floating-point value to the specified number of
        //     fractional digits. A parameter specifies how to round the value if it is
        //     midway between two other numbers.
        //
        // Parameters:
        //   value:
        //     A double-precision floating-point number to be rounded.
        //
        //   digits:
        //     The number of fractional digits in the return value.
        //
        //   mode:
        //     Specification for how to round value if it is midway between two other numbers.
        //
        // Returns:
        //     The number nearest to value that has a number of fractional digits equal
        //     to digits. If the number of fractional digits in value is less than digits,
        //     value is returned unchanged.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     digits is less than 0 or greater than 15.
        //
        //   System.ArgumentException:
        //     mode is not a valid value of System.MidpointRounding.
        public static float Round(float value, int digits, MidpointRounding mode)
        {
            return (float)Math.Round(value, digits, mode);
        }
        #endregion

        /// <summary>
        /// Calculates the fractional part of a specified double-precision floating-point number.
        /// </summary>
        /// <param name="value">A double-precision floating-point number.</param>
        /// <returns>The fractional part of value; that is, the number that remains after any integral
        /// digits have been discarded.</returns>
        [Pure]
        public static double Fractional(double value)
        {
            Contract.Ensures(0.0 <= Contract.Result<double>() && Contract.Result<double>() <= 1.0);
            return value - Functions.Truncate(value);
        }
        /// <summary>
        /// Calculates the fractional part of a specified single-precision floating-point number.
        /// </summary>
        /// <param name="value">A single-precision floating-point number.</param>
        /// <returns>The fractional part of value; that is, the number that remains after any integral
        /// digits have been discarded.</returns>
        [Pure]
        public static float Fractional(float value)
        {
            Contract.Ensures(0.0 <= Contract.Result<float>() && Contract.Result<float>() <= 1.0);
            return value - Functions.Truncate(value);
        }
        #endregion

        #region Range
        #region Abs
        // Summary:
        //     Returns the absolute value of a System.Decimal number.
        //
        // Parameters:
        //   value:
        //     A number in the range System.Decimal.MinValue≤ value ≤System.Decimal.MaxValue.
        //
        // Returns:
        //     A System.Decimal, x, such that 0 ≤ x ≤System.Decimal.MaxValue.
        public static decimal Abs(decimal value)
        {
            return Math.Abs(value);
        }
        //
        // Summary:
        //     Returns the absolute value of a double-precision floating-point number.
        //
        // Parameters:
        //   value:
        //     A number in the range System.Double.MinValue≤value≤System.Double.MaxValue.
        //
        // Returns:
        //     A double-precision floating-point number, x, such that 0 ≤ x ≤System.Double.MaxValue.
        public static double Abs(double value)
        {
            return Math.Abs(value);
        }
        //
        // Summary:
        //     Returns the absolute value of a single-precision floating-point number.
        //
        // Parameters:
        //   value:
        //     A number in the range System.Single.MinValue≤value≤System.Single.MaxValue.
        //
        // Returns:
        //     A single-precision floating-point number, x, such that 0 ≤ x ≤System.Single.MaxValue.
        public static float Abs(float value)
        {
            return Math.Abs(value);
        }
        //
        // Summary:
        //     Returns the absolute value of a 32-bit signed integer.
        //
        // Parameters:
        //   value:
        //     A number in the range System.Int32.MinValue < value≤System.Int32.MaxValue.
        //
        // Returns:
        //     A 32-bit signed integer, x, such that 0 ≤ x ≤System.Int32.MaxValue.
        //
        // Exceptions:
        //   System.OverflowException:
        //     value equals System.Int32.MinValue.
        public static int Abs(int value)
        {
            return Math.Abs(value);
        }
        //
        // Summary:
        //     Returns the absolute value of a 64-bit signed integer.
        //
        // Parameters:
        //   value:
        //     A number in the range System.Int64.MinValue < value≤System.Int64.MaxValue.
        //
        // Returns:
        //     A 64-bit signed integer, x, such that 0 ≤ x ≤System.Int64.MaxValue.
        //
        // Exceptions:
        //   System.OverflowException:
        //     value equals System.Int64.MinValue.
        public static long Abs(long value)
        {
            return Math.Abs(value);
        }
        //
        // Summary:
        //     Returns the absolute value of an 8-bit signed integer.
        //
        // Parameters:
        //   value:
        //     A number in the range System.SByte.MinValue < value≤System.SByte.MaxValue.
        //
        // Returns:
        //     An 8-bit signed integer, x, such that 0 ≤ x ≤System.SByte.MaxValue.
        //
        // Exceptions:
        //   System.OverflowException:
        //     value equals System.SByte.MinValue.
        [CLSCompliant(false)]
        public static sbyte Abs(sbyte value)
        {
            return Math.Abs(value);
        }
        //
        // Summary:
        //     Returns the absolute value of a 16-bit signed integer.
        //
        // Parameters:
        //   value:
        //     A number in the range System.Int16.MinValue < value≤System.Int16.MaxValue.
        //
        // Returns:
        //     A 16-bit signed integer, x, such that 0 ≤ x ≤System.Int16.MaxValue.
        //
        // Exceptions:
        //   System.OverflowException:
        //     value equals System.Int16.MinValue.
        public static short Abs(short value)
        {
            return Math.Abs(value);
        }
        #endregion

        #region Clamp
        /// <summary>
        /// Constrains a value to a given range.
        /// </summary>
        /// <param name="value">A number to constrain.</param>
        /// <param name="min">The minimum value in the range.</param>
        /// <param name="max">The maximum value in the range.</param>
        /// <returns>If value is less than min, return min. 
        /// If value is greater than max return max.
        /// Else return value.</returns>
        [Pure]
        public static int Clamp(int value, int min, int max)
        {
            return Functions.Min(max, Functions.Max(min, value));
        }
        /// <summary>
        /// Constrains a value to a given range.
        /// </summary>
        /// <param name="value">A number to constrain.</param>
        /// <param name="min">The minimum value in the range.</param>
        /// <param name="max">The maximum value in the range.</param>
        /// <returns>If value is less than min, return min. 
        /// If value is greater than max return max.
        /// Else return value.</returns>
        [Pure]
        public static long Clamp(long value, long min, long max)
        {
            return Functions.Min(max, Functions.Max(min, value));
        }
        /// <summary>
        /// Constrains a value to a given range.
        /// </summary>
        /// <param name="value">A number to constrain.</param>
        /// <param name="min">The minimum value in the range.</param>
        /// <param name="max">The maximum value in the range.</param>
        /// <returns>If value is less than min, return min. 
        /// If value is greater than max return max.
        /// Else return value.</returns>
        [Pure]
        public static float Clamp(float value, float min, float max)
        {
            return Functions.Min(max, Functions.Max(min, value));
        }
        /// <summary>
        /// Constrains a value to a given range.
        /// </summary>
        /// <param name="value">A number to constrain.</param>
        /// <param name="min">The minimum value in the range.</param>
        /// <param name="max">The maximum value in the range.</param>
        /// <returns>If value is less than min, return min. 
        /// If value is greater than max return max.
        /// Else return value.</returns>
        [Pure]
        public static double Clamp(double value, double min, double max)
        {
            return Functions.Min(max, Functions.Max(min, value));
        }
        #endregion

        /// <summary>
        /// Clamps a number between 0 and 1.
        /// </summary>
        /// <param name="value">A number.</param>
        /// <returns>value clamped between 0 and 1.</returns>
        [Pure]
        public static double Saturate(double value)
        {
            Contract.Ensures(0.0 <= Contract.Result<double>() && Contract.Result<double>() <= 1.0);
            return Functions.Min(1.0, Functions.Max(0.0, value));
        }

        /// <summary>
        /// Wraps a number between min and max.
        /// </summary>
        /// <param name="value">The number to wrap.</param>
        /// <param name="min">The maximum value.</param>
        /// <param name="max">The minimum value.</param>
        /// <returns>value wrapped around the range [min, max].</returns>
        [Pure]
        public static double Wrap(double value, double min, double max)
        {
            Contract.Requires(min != max);
            Contract.Ensures(min <= Contract.Result<double>() && Contract.Result<double>() <= max);

            return min + Modulus(value - min, max - min);
        }
        #endregion

        #region Division

        //
        // Summary:
        //     Calculates the quotient of two 32-bit signed integers and also returns the
        //     remainder in an output parameter.
        //
        // Parameters:
        //   a:
        //     The dividend.
        //
        //   b:
        //     The divisor.
        //
        //   result:
        //     The remainder.
        //
        // Returns:
        //     The quotient of the specified numbers.
        //
        // Exceptions:
        //   System.DivideByZeroException:
        //     b is zero.
        public static Tuple<int, int> DivRem(int a, int b)
        {
            int rem;
            int div = Math.DivRem(a, b, out rem);
            return Tuple.Create(div, rem);
        }
        //
        // Summary:
        //     Calculates the quotient of two 64-bit signed integers and also returns the
        //     remainder in an output parameter.
        //
        // Parameters:
        //   a:
        //     The dividend.
        //
        //   b:
        //     The divisor.
        //
        //   result:
        //     The remainder.
        //
        // Returns:
        //     The quotient of the specified numbers.
        //
        // Exceptions:
        //   System.DivideByZeroException:
        //     b is zero.
        public static Tuple<long, long> DivRem(long a, long b)
        {
            long rem;
            long div = Math.DivRem(a, b, out rem);
            return Tuple.Create(div, rem);
        }

        //
        // Summary:
        //     Returns the remainder resulting from the division of a specified number by
        //     another specified number.
        //
        // Parameters:
        //   x:
        //     A dividend.
        //
        //   y:
        //     A divisor.
        //
        // Returns:
        //     A number equal to x - (y Q), where Q is the quotient of x / y rounded to
        //     the nearest integer (if x / y falls halfway between two integers, the even
        //     integer is returned).If x - (y Q) is zero, the value +0 is returned if x
        //     is positive, or -0 if x is negative.If y = 0, System.Double.NaN is returned.
        [SecuritySafeCritical]
        public static double IEEERemainder(double x, double y)
        {
            return Math.IEEERemainder(x, y);
        }

        #region Modulus
        /// <summary>
        /// Euclidean modulus.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The modulus.</returns>
        [Pure()]
        public static int Modulus(int dividend, int divisor)
        {
            Contract.Requires(divisor != 0, "divisor equals zero.");

            int rt = dividend - (divisor * (dividend / divisor));
            int i = 0;
            if (rt < 0)
                i = Functions.Sign(divisor);
            return rt + i * divisor;
        }
        /// <summary>
        /// Euclidean modulus.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The modulus.</returns>
        [Pure]
        public static long Modulus(long dividend, long divisor)
        {
            Contract.Requires(divisor != 0, "divisor equals zero.");

            long rt = dividend - (divisor * (dividend / divisor));
            long i = 0;
            if (rt < 0)
                i = Functions.Sign(divisor);
            return rt + i * divisor;
        }       
        /// <summary>
        /// Euclidean modulus.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The modulus.</returns>
        [Pure]
        public static double Modulus(double dividend, double divisor)
        {
            Contract.Requires(divisor != 0.0, "divisor equals zero.");

            double q;
            if (divisor > 0.0)
                q = Functions.Floor(dividend / divisor);
            else
                q = Functions.Ceiling(dividend / divisor);
            return dividend - (divisor * q);
        }
        /// <summary>
        /// Euclidean modulus.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The modulus.</returns>
        [Pure]
        public static float Modulus(float dividend, float divisor)
        {
            Contract.Requires(divisor != 0.0, "divisor equals zero.");

            float q;
            if (divisor > 0.0)
                q = Functions.Floor(dividend / divisor);
            else
                q = Functions.Ceiling(dividend / divisor);
            return dividend - (divisor * q);
        }
        #endregion

        /// <summary>
        /// Calculates the greatest common divisor of two numbers.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>The greatest common divisor, also known as the greatest common factor, 
        /// or highest common factor, of the two given numbers.</returns>
        [Pure]
        public static int GCD(int a, int b)
        {
            if (a == int.MinValue)
            {
                return GCD(b, a % b);
            }

            a = Abs(a);
            b = Abs(b);

            if (b > a)
            {
                int t = a;
                a = b;
                b = t;
            }

            while (b != 0)
            {
                int t = b;
                b = a % b;
                a = t;
            }

            return a;
        }
        /// <summary>
        /// Calculates the lowest common multiple of two numbers.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>The lowest common multiple, also known as the least common multiple, 
        /// or smallest common multiple, of the two given numbers.</returns>
        [Pure]
        public static int LCM(int a, int b)
        {
            Contract.Requires((a * b) != Int32.MinValue);

            return Abs(a * b) / GCD(a, b);
        }
        #endregion

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

        /// <summary>
        /// Calculates the natural logarithm of the factorial function.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [Pure]
        public static double FactorialLn(long n)
        {
            Contract.Requires(0 <= n);
            Contract.Ensures(0 <= Contract.Result<double>() && Contract.Result<double>() < double.MaxValue);

            return GammaLn((double)n + 1.0);
        }

        /// <summary>
        /// Calculates the natural logarithm binomial coefficient (n k).
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        [Pure]
        public static double BinomialCoefficientLn(long n, long k)
        {
            Contract.Requires(0 <= k && k <= n);
            Contract.Ensures(0 < Contract.Result<double>());

            return (FactorialLn(n) - (FactorialLn(k) + FactorialLn(n - k)));
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

        /// <summary>
        /// Returns the nth fibonnacci number.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [Pure]
        public static double Fibonnacci(int n)
        {
            //bounds for fibonnacci
            Contract.Requires(0 <= n);
            Contract.Ensures(0.0 <= Contract.Result<double>());

            double root = Sqrt(5.0);
            return ((1.0 / root) * (Pow((1.0 + root) / 2.0, n) - Pow((1.0 - root) / 2.0, n)));
        }

        /// <summary>
        /// Returns the Cartesian coordinate for one axis of a point that is defined
        /// by a given triangle and two normalized barycentric (areal) coordinates.
        /// </summary>
        /// <param name="value1">The coordinate on one axis of vertex 1 of the defining triangle.</param>
        /// <param name="value2">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
        /// <param name="value3">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
        /// <param name="amount1">The normalized barycentric (areal) coordinate b2, equal to the weighting
        /// factor for vertex 2, the coordinate of which is specified in value2.</param>
        /// <param name="amount2">The normalized barycentric (areal) coordinate b3, equal to the weighting
        /// factor for vertex 3, the coordinate of which is specified in value3.</param>
        /// <returns>Cartesian coordinate of the specified point with respect to the axis being
        /// used.</returns>
        [Pure]
        public static double Barycentric(double value1, double value2, double value3, double amount1, double amount2)
        {
            return ((1 - amount1 - amount2) * value1) + (amount1 * value2) + (amount2 * value3);
        }

        /// <summary>
        /// Performs a linear interpolation between two values.
        /// </summary>
        /// <param name="value1">First value.</param>
        /// <param name="value2">Second value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="value2"/>.</param>
        /// <returns>The linear interpolation of the two values.</returns>
        [Pure]
        public static double Lerp(double value1, double value2, double amount)
        {
            return value1 + amount * (value2 - value1);
        }
        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        /// <param name="value1">First value.</param>
        /// <param name="tangent1">First tangent.</param>
        /// <param name="value2">Second value.</param>
        /// <param name="tangent2">Second tangent.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="value2"/>.</param>
        /// <returns>The Hermite spline interpolation of the two values.</returns>
        [Pure]
        public static double Hermite(double value1, double tangent1, double value2, double tangent2, double amount)
        {
            double amount2 = amount * amount;
            double amount3 = amount2 * amount;

            double h00 = 2.0 * amount3 - 3.0 * amount2 + 1;
            double h10 = amount3 - 2.0 * amount2 + amount;
            double h01 = -2.0 * amount3 + 3.0 * amount2;
            double h11 = amount3 - amount2;

            return h00 * value1 + h10 * tangent1 + h01 * value2 + h11 * tangent2;
        }
        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="value3">The third value.</param>
        /// <param name="value4">The fourth value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight between <paramref name="value2"/> and <paramref name="value3"/>.</param>
        /// <returns>The Catmull-Rom interpolation of the four values.</returns>
        [Pure]
        public static double CatmullRom(double value1, double value2, double value3, double value4, double amount)
        {
            double tangent0 = (value3 - value1) / 2.0;
            double tangent1 = (value4 - value2) / 2.0;
            return Hermite(value2, tangent0, value3, tangent1, amount);
        }
        /// <summary>
        /// Performs a cubic interpolation between two values.
        /// </summary>
        /// <param name="value1">First values.</param>
        /// <param name="value2">Second values.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="value2"/>.</param>
        /// <returns>The cubic interpolation of the two vectors.</returns>
        [Pure]
        public static double SmoothStep(double value1, double value2, double amount)
        {
            amount = Saturate(amount);
            amount = (amount * amount) * (3.0 - (2.0 * amount));
            return value1 + amount * (value2 - value1);
        }
    }

    public static class FloatExtensions
    {
        [Pure]
        public static bool EpsilonEquals(this float a, float b, float epsilon)
        {
            return Functions.Abs(a - b) <=
                Functions.Max(epsilon, epsilon * Functions.Max(Functions.Abs(a), Functions.Abs(b)));
        }
        [Pure]
        public static bool EpsilonEquals(this double a, double b, double epsilon)
        {
            return Functions.Abs(a - b) <=
                Functions.Max(epsilon, epsilon * Functions.Max(Functions.Abs(a), Functions.Abs(b)));
        }
    }
}