using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Ibasa.Numerics
{
    /// <summary>
    /// Represents a complex number, of the form (A + Bi).
    /// </summary>
    [Serializable]
    public struct Complex : IEquatable<Complex>, IFormattable
    {
        #region Constants
        /// <summary>
        /// Returns a new <see cref="Complex"/> with a real number equal to
        /// zero and an imaginary number equal to zero.
        /// </summary>
        public static readonly Complex Zero = new Complex();
        /// <summary>
        /// Returns a new <see cref="Complex"/> with a real number equal to
        /// one and an imaginary number equal to zero.
        /// </summary>
        public static readonly Complex One = new Complex(1.0, 0.0);
        /// <summary>
        /// Returns a new <see cref="Complex"/> with a real number equal to
        /// zero and an imaginary number equal to one.
        /// </summary>
        public static readonly Complex I = new Complex(0.0, 1.0);
        #endregion

        #region Fields
        /// <summary>
        /// The real component of the complex number.
        /// </summary>
        public readonly double A;
        /// <summary>
        /// The imaginary component of the complex number. 
        /// </summary>
        public readonly double B;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> using
        /// the specified real and imaginary values.
        /// </summary>
        /// <param name="a">The real component of the complex number.</param>
        /// <param name="b">The imaginary component of the complex number.</param>
        public Complex(double a, double b)
        {
            A = a;
            B = b;
        }
        /// <summary>
        /// Creates a complex number from a point's polar coordinates.
        /// </summary>
        /// <param name="magnitude">The magnitude, which is the distance from the origin (the intersection of
        /// the x-axis and the y-axis) to the number.</param>
        /// <param name="phase">The phase, which is the angle from the line to the horizontal axis, measured
        /// in radians.</param>
        public static Complex FromPolarCoordinates(double magnitude, double phase)
        {
            return new Complex(Functions.Cos(phase) * magnitude, Functions.Sin(phase) * magnitude);
        }
        /// <summary>
        /// Creates a complex number from a point's polar coordinates.
        /// </summary>
        /// <param name="coordinate">The polar coordinate</param>
        public static Complex FromPolarCoordinates(Geometry.PolarCoordinate coordinate)
        {
            return new Complex(
                Functions.Cos(coordinate.Theta) * coordinate.Rho, 
                Functions.Sin(coordinate.Theta) * coordinate.Rho);
        }
        #endregion

        #region Operations
        /// <summary>
        /// Returns the additive inverse of a complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The negative of value.</returns>
        public static Complex Negative(Complex value)
        {
            return new Complex(-value.A, -value.B);
        }

        /// <summary>
        /// Adds two complex numbers and returns the result.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of left and right.</returns>
        public static Complex Add(Complex left, Complex right)
        {
            return new Complex(left.A + right.A, left.B + right.B);
        }
        /// <summary>
        /// Subtracts one complex number from another and returns the result.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting right from left (the difference).</returns>
        public static Complex Subtract(Complex left, Complex right)
        {
            return new Complex(left.A - right.A, left.B - right.B);
        }
        /// <summary>
        /// Returns the product of two complex numbers.
        /// </summary>
        /// <param name="left">The first complex number to multiply.</param>
        /// <param name="right">The second complex number to multiply.</param>
        /// <returns>The product of the left and right parameters.</returns>
        public static Complex Multiply(Complex left, Complex right)
        {
            return new Complex((left.A * right.A) - (left.B * right.B), (left.B * right.A) + (left.A * right.B));
        }
        /// <summary>
        /// Divides one complex number by another and returns the result.
        /// </summary>
        /// <param name="left">The complex number to be divided (the dividend).</param>
        /// <param name="right">The complex number to divide by (the divisor).</param>
        /// <returns>The result of dividing left by right (the quotient).</returns>
        public static Complex Divide(Complex left, Complex right)
        {
            return new Complex(
                ((left.A * right.A) + (left.B * right.B)) / AbsoluteSquared(right),
                ((left.B * right.A) - (left.A * right.B)) / AbsoluteSquared(right));
        }

        /// <summary>
        /// Returns the identity of a specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The identity of value.</returns>
        public static Complex operator +(Complex value)
        {
            return value;
        }
        /// <summary>
        /// Returns the additive inverse of a specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The negative of value.</returns>
        public static Complex operator -(Complex value)
        {
            return Negative(value);
        }
        /// <summary>
        /// Adds two complex numbers.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of left and right.</returns>
        public static Complex operator +(Complex left, Complex right)
        {
            return Add(left, right);
        }
        /// <summary>
        /// Subtracts a complex number from another complex number.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting right from left (the difference).</returns>
        public static Complex operator -(Complex left, Complex right)
        {
            return Subtract(left, right);
        }
        /// <summary>
        /// Multiplies two specified complex numbers.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of left and right.</returns>
        public static Complex operator *(Complex left, Complex right)
        {
            return Multiply(left, right);
        }
        /// <summary>
        /// Divides a specified complex number by another specified complex number.
        /// </summary>
        /// <param name="left">The value to be divided (the dividend).</param>
        /// <param name="right">The value to divide by (the divisor).</param>
        /// <returns>The result of dividing left by right (the quotient).</returns>
        public static Complex operator /(Complex left, Complex right)
        {
            return Divide(left, right);
        }
        #endregion

        #region Functions
        #region Product
        /// <summary>
        /// Calculates the dot product (inner product) of two complex numbers.
        /// </summary>
        /// <param name="left">First source complex number.</param>
        /// <param name="right">Second source complex number.</param>
        /// <returns>The dot product of the two complex numbers.</returns>
        public static double Dot(Complex left, Complex right)
        {
            return left.A * right.A + left.B * right.B;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Return real part of a complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The real part of a complex number.</returns>
        public static Complex Real(Complex value)
        {
            return new Complex(value.A, 0.0);
        }
        /// <summary>
        /// Return imaginary part of a complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The imaginary part of a complex number.</returns>
        public static Complex Imaginary(Complex value)
        {
            return new Complex(0.0, value.B);
        }
        /// <summary>
        /// Computes the absolute squared value of a complex number and returns the result.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The absolute squared value of value.</returns>
        public static double AbsoluteSquared(Complex value)
        {
            return (value.A * value.A + value.B * value.B);
        }
        /// <summary>
        /// Computes the absolute value (or modulus or magnitude) of a complex number and returns the result.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The absolute value of value.</returns>
        public static double Absolute(Complex value)
        {
            return Functions.Sqrt(value.A * value.A + value.B * value.B);
        }
        /// <summary>
        /// Computes the normalized value (or unit) of a complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The normalized value of value.</returns>
        public static Complex Normalize(Complex value)
        {
            return value / Absolute(value);
        }
        /// <summary>
        /// Returns the multiplicative inverse of a complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The reciprocal of value.</returns>
        public static Complex Reciprocal(Complex value)
        {
            return new Complex(value.A / AbsoluteSquared(value), -value.B / AbsoluteSquared(value));
        }
        /// <summary>
        /// Computes the conjugate of a complex number and returns the result.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The conjugate of value.</returns>
        public static Complex Conjugate(Complex value)
        {
            return new Complex(value.A, -value.B);
        }
        /// <summary>
        /// Computes the argument (or phase) of a complex number and returns the result.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The argument of value.</returns>
        public static double Argument(Complex value)
        {
            return Functions.Atan2(value.B, value.A);
        }
        /// <summary>
        /// Returns a value indicating wheter any components of the specified complex number evaluate to 
        /// negative or positive infinity.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>true if any component evaluates to System.Double.PositiveInfinity or System.Double.NegativeInfinity;
        /// otherwise, false.</returns>
        public static bool IsInfinity(Complex value)
        {
            return double.IsInfinity(value.A) | double.IsInfinity(value.B);
        }
        /// <summary>
        /// Returns a value indicating wheter any components of the specified complex number evaluate to 
        /// a value that is not a number (System.Double.NaN).
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>true if any component evaluates to System.Double.NaN; otherwise, false.</returns>
        public static bool IsNaN(Complex value)
        {
            return double.IsNaN(value.A) | double.IsNaN(value.B);
        }
        #endregion
        #region Integral
        /// <summary>
        /// Returns a complex number where each component is the smallest integral value that 
        /// is greater than or equal to the specified component.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The ceiling of value.</returns>
        public static Complex Ceiling(Complex value)
        {
            return new Complex(
                Functions.Ceiling(value.A),
                Functions.Ceiling(value.B));
        }
        /// <summary>
        /// Returns a complex number where each component is the largest integral value that 
        /// is less than or equal to the specified component.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The floor of value.</returns>
        public static Complex Floor(Complex value)
        {
            return new Complex(
                Functions.Floor(value.A),
                Functions.Floor(value.B));
        }
        /// <summary>
        /// Returns a complex number where each component is the integral part of the specified component.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The integral of value.</returns>
        public static Complex Truncate(Complex value)
        {
            return new Complex(
                Functions.Truncate(value.A),
                Functions.Truncate(value.B));
        }
        /// <summary>
        /// Returns a complex number where each component is the fractional part of the specified component.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The fractional of value.</returns>
        public static Complex Fractional(Complex value)
        {
            return new Complex(
                Functions.Fractional(value.A),
                Functions.Fractional(value.B));
        }
        /// <summary>
        /// Returns a complex number where each component is rounded to the nearest integral value.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The result of rounding value.</returns>
        public static Complex Round(Complex value)
        {
            return new Complex(
                Functions.Round(value.A),
                Functions.Round(value.B));
        }
        /// <summary>
        /// Returns a complex number where each component is rounded to the nearest integral value.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <param name="digits">The number of fractional digits in the return value.</param>
        /// <returns>The result of rounding value.</returns>
        public static Complex Round(Complex value, int digits)
        {
            return new Complex(
                Functions.Round(value.A, digits),
                Functions.Round(value.B, digits));
        }
        /// <summary>
        /// Returns a complex number where each component is rounded to the nearest integral value.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
        /// <returns>The result of rounding value.</returns>
        public static Complex Round(Complex value, MidpointRounding mode)
        {
            return new Complex(
                Functions.Round(value.A, mode),
                Functions.Round(value.B, mode));
        }
        /// <summary>
        /// Returns a complex number where each component is rounded to the nearest integral value.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <param name="digits">The number of fractional digits in the return value.</param>
        /// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
        /// <returns>The result of rounding value.</returns>
        public static Complex Round(Complex value, int digits, MidpointRounding mode)
        {
            return new Complex(
                Functions.Round(value.A, digits, mode),
                Functions.Round(value.B, digits, mode));
        }
        #endregion
        #region Logarithmic
        /// <summary>
        /// Returns the square root of a specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The square root of value.</returns>
        public static Complex Sqrt(Complex value)
        {
            double magnitude = Functions.Sqrt(value.A * value.A + value.B * value.B);
            return new Complex(
                Functions.Sqrt((magnitude + value.A) / 2.0),
                (value.B < 0 ? -1.0 : 1.0) * Functions.Sqrt((magnitude - value.A) / 2.0));
        }
        /// <summary>
        /// Returns a specified complex number raised to a power specified by a complex number.
        /// </summary>
        /// <param name="value">A complex number to be raised to a power.</param>
        /// <param name="power">A complex number that specifies a power.</param>
        /// <returns>The complex number value raised to the power power.</returns>
        public static Complex Pow(Complex value, Complex power)
        {
            return Exp(power * Log(value));
        }
        /// <summary>
        /// Returns e raised to the power specified by a complex number.
        /// </summary>
        /// <param name="value">A complex number that specifies a power.</param>
        /// <returns>The number e raised to the power value.</returns>
        public static Complex Exp(Complex value)
        {
            double e = Functions.Exp(value.A);
            return new Complex(e * Functions.Cos(value.B), e * Functions.Sin(value.B));
        }
        /// <summary>
        /// Returns the natural (base e) logarithm of a specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The natural (base e) logarithm of value.</returns>
        public static Complex Log(Complex value)
        {
            return new Complex(Functions.Log(Absolute(value)), Argument(value));
        }
        /// <summary>
        /// Returns the logarithm of a specified complex number in a specified base.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <param name="baseValue">The base of the logarithm.</param>
        /// <returns>The logarithm of value in base baseValue.</returns>
        public static Complex Log(Complex value, double baseValue)
        {
            return Log(value) / Functions.Log(baseValue);
        }
        /// <summary>
        /// Returns the base-10 logarithm of a specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The base-10 logarithm of value.</returns>
        public static Complex Log10(Complex value)
        {
            return Log(value) / Functions.Log(10.0);
        }
        #endregion
        #region Trigonometric
        /// <summary>
        /// Returns the cosine of the specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The cosine of value.</returns>
        public static Complex Cos(Complex value)
        {
            return new Complex(Functions.Cos(value.A) * Functions.Cosh(value.B), -Functions.Sin(value.A) * Functions.Sinh(value.B));
        }
        /// <summary>
        /// Returns the angle that is the arc cosine of the specified complex number.
        /// </summary>
        /// <param name="value">A complex number that represents a cosine.</param>
        /// <returns>The angle, measured in radians, which is the arc cosine of value.</returns>
        public static Complex Acos(Complex value)
        {
            return -I * Log(value + I * Sqrt(1 - (value * value)));
        }
        /// <summary>
        /// Returns the hyperbolic cosine of the specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The hyperbolic cosine of value.</returns>
        public static Complex Cosh(Complex value)
        {
            return (Exp(value) + Exp(-value)) / 2.0;
        }
        /// <summary>
        /// Returns the hyperbolic angle whose hyperbolic cosine is the specified number.
        /// </summary>
        /// <param name="value">A number representing a hyperbolic cosine.</param>
        /// <returns>A hyperbolic angle, θ, measured in radians.</returns>
        public static Complex Acosh(Complex value)
        {
            return Log(value + Sqrt(value - 1.0) * Sqrt(value + 1.0));
        }
        /// <summary>
        /// Returns the sine of the specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The sine of value.</returns>
        public static Complex Sin(Complex value)
        {
            return new Complex(Functions.Sin(value.A) * Functions.Cosh(value.B), Functions.Cos(value.A) * Functions.Sinh(value.B));
        }
        /// <summary>
        /// Returns the angle that is the arc sine of the specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The angle which is the arc sine of value.</returns>
        public static Complex Asin(Complex value)
        {
            return -I * Log(I * value + Sqrt(1 - (value * value)));
        }
        /// <summary>
        /// Returns the hyperbolic sine of the specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The hyperbolic sine of value.</returns>
        public static Complex Sinh(Complex value)
        {
            return (Exp(value) - Exp(-value)) / 2.0;
        }
        /// <summary>
        /// Returns the hyperbolic angle whose hyperbolic sine is the specified number.
        /// </summary>
        /// <param name="value">A number representing a hyperbolic sine.</param>
        /// <returns>A hyperbolic angle, θ, measured in radians.</returns>
        public static Complex Asinh(Complex value)
        {
            return Log(value + Sqrt(value * value + 1.0));
        }
        /// <summary>
        /// Returns the tangent of the specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The tangent of value.</returns>
        public static Complex Tan(Complex value)
        {
            double sa = Functions.Sin(value.A);
            double ca = Functions.Cos(value.A);
            double shb = Functions.Sinh(value.B);
            double cha = Functions.Cosh(value.B);

            return (new Complex(sa * cha, ca * shb)) / (new Complex(ca * cha, -sa * shb));
        }
        /// <summary>
        /// Returns the angle that is the arc tangent of the specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The angle that is the arc tangent of value.</returns>
        public static Complex Atan(Complex value)
        {
            return -0.5 * I * Log((1 + I * value) / (1 - I * value));
        }
        /// <summary>
        /// Returns the hyperbolic tangent of the specified complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The hyperbolic tangent of value.</returns>
        public static Complex Tanh(Complex value)
        {
            Complex e = Exp(value);
            Complex me = Exp(-value);

            return ((e - me) / 2.0) / ((e + me) / 2.0);
        }
        /// <summary>
        /// Returns the hyperbolic angle whose hyperbolic tangent is the specified number.
        /// </summary>
        /// <param name="value">A number representing a hyperbolic tangent.</param>
        /// <returns>A hyperbolic angle, θ, measured in radians.</returns>
        public static Complex Atanh(Complex value)
        {
            return Log((1.0 + value) / (1.0 - value)) / 2.0;
        }
        #endregion
        #region Interpolation
        /// <summary>
        /// Performs a linear interpolation between two complex numbers.
        /// </summary>
        /// <param name="start">Start complex number.</param>
        /// <param name="end">End complex number.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
        /// <returns>The linear interpolation of the two complex numbers.</returns>
        /// <remarks>
        /// This method performs the linear interpolation based on the following formula.
        /// <code>start + (end - start) * amount</code>
        /// Passing <paramref name="amount"/> a value of 0 will cause <paramref name="start"/> to be returned; a value of 1 will cause <paramref name="end"/> to be returned. 
        /// </remarks>
        public Complex Lerp(Complex start, Complex end, double amount)
        {
            return new Complex(Functions.Lerp(start.A, end.A, amount), Functions.Lerp(start.B, end.B, amount));
        }
        /// <summary>
        /// Interpolates between two unit complex numbers, using spherical linear interpolation.
        /// </summary>
        /// <param name="start">Start complex number.</param>
        /// <param name="end">End complex number.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
        /// <returns>The spherical linear interpolation of the two complex numbers.</returns>
        ///  <remarks>
        /// Passing <paramref name="amount"/> a value of 0 will cause <paramref name="start"/> to be returned; a value of 1 will cause <paramref name="end"/> to be returned. 
        /// </remarks>
        public Complex Slerp(Complex start, Complex end, double amount)
        {
            double cosTheta = Dot(start, end);

            //Cannot use slerp, use lerp instead
            if (Functions.Abs(cosTheta) - 1.0 < double.Epsilon)
                return Lerp(start, end, amount);

            double theta = Functions.Acos(cosTheta);
            double sinTheta = Functions.Sin(theta);
            double t0 = Functions.Sin((1.0 - amount) * theta) / sinTheta;
            double t1 = Functions.Sin(amount * theta) / sinTheta;

            return t0 * start + t1 * end;
        }
        #endregion
        #endregion

        #region Conversions
        /// <summary>
        /// Defines an explicit conversion of a System.Decimal value to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>A complex number that has a real component equal to value and an imaginary
        /// component equal to zero.</returns>
        public static explicit operator Complex(decimal value)
        {
            return new Complex((double)value, 0.0);
        }
        /// <summary>
        /// Defines an implicit conversion of a double-precision floating-point number
        /// to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>A complex number that has a real component equal to value and an imaginary
        /// component equal to zero.</returns>
        public static implicit operator Complex(double value)
        {
            return new Complex(value, 0.0);
        }
        /// <summary>
        /// Defines an implicit conversion of a single-precision floating-point number
        /// to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>A complex number that has a real component equal to value and an imaginary
        /// component equal to zero.</returns>
        public static implicit operator Complex(float value)
        {
            return new Complex(value, 0.0);
        }
        /// <summary>
        /// Defines an implicit conversion of a 32-bit signed integer
        /// to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>A complex number that has a real component equal to value and an imaginary
        /// component equal to zero.</returns>
        public static implicit operator Complex(int value)
        {
            return new Complex(value, 0.0);
        }
        /// <summary>
        /// Defines an implicit conversion of a 64-bit signed integer
        /// to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>A complex number that has a real component equal to value and an imaginary
        /// component equal to zero.</returns>
        public static implicit operator Complex(long value)
        {
            return new Complex(value, 0.0);
        }
        /// <summary>
        /// Defines an implicit conversion of a signed byte
        /// to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>A complex number that has a real component equal to value and an imaginary
        /// component equal to zero.</returns>
        [CLSCompliant(false)]
        public static implicit operator Complex(sbyte value)
        {
            return new Complex(value, 0.0);
        }
        /// <summary>
        /// Defines an implicit conversion of a 16-bit signed integer
        /// to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>A complex number that has a real component equal to value and an imaginary
        /// component equal to zero.</returns>
        public static implicit operator Complex(short value)
        {
            return new Complex(value, 0.0);
        }
        /// <summary>
        /// Defines an implicit conversion of a 32-bit unsigned integer
        /// to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>A complex number that has a real component equal to value and an imaginary
        /// component equal to zero.</returns>
        [CLSCompliant(false)]
        public static implicit operator Complex(uint value)
        {
            return new Complex(value, 0.0);
        }
        /// <summary>
        /// Defines an implicit conversion of a 64-bit unsigned integer
        /// to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>A complex number that has a real component equal to value and an imaginary
        /// component equal to zero.</returns>
        [CLSCompliant(false)]
        public static implicit operator Complex(ulong value)
        {
            return new Complex(value, 0.0);
        }
        /// <summary>
        /// Defines an implicit conversion of a unsigned byte integer
        /// to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>A complex number that has a real component equal to value and an imaginary
        /// component equal to zero.</returns>
        public static implicit operator Complex(byte value)
        {
            return new Complex(value, 0.0);
        }
        /// <summary>
        /// Defines an implicit conversion of a 16-bit unsigned integer
        /// to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>A complex number that has a real component equal to value and an imaginary
        /// component equal to zero.</returns>
        [CLSCompliant(false)]
        public static implicit operator Complex(ushort value)
        {
            return new Complex(value, 0.0);
        }
        #endregion

        #region Equatable
        /// <summary>
        /// Returns the hash code for the current Complex object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return A.GetHashCode() + B.GetHashCode();
        }
        /// <summary>
        /// Returns a value that indicates whether the current instance and a specified
        /// object have the same value.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>true if the obj parameter is a Complex object and its value
        /// is equal to the current Complex object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Complex)
                return Equals((Complex)obj);

            return false;
        }
        /// <summary>
        /// Returns a value that indicates whether the current instance and a specified
        /// complex number have the same value.
        /// </summary>
        /// <param name="other">The complex number to compare.</param>
        /// <returns>true if this complex number and value have the same value; otherwise, false.</returns>
        public bool Equals(Complex other)
        {
            return this == other;
        }
        /// <summary>
        /// Returns a value that indicates whether two complex numbers are equal.
        /// </summary>
        /// <param name="left">The first complex number to compare.</param>
        /// <param name="right">The second complex number to compare.</param>
        /// <returns>true if the left and right are equal; otherwise, false.</returns>
        public static bool Equals(Complex left, Complex right)
        {
            return left == right;
        }
        /// <summary>
        /// Returns a value that indicates whether two complex numbers are equal.
        /// </summary>
        /// <param name="left">The first complex number to compare.</param>
        /// <param name="right">The second complex number to compare.</param>
        /// <returns>true if the left and right are equal; otherwise, false.</returns>
        public static bool operator ==(Complex left, Complex right)
        {
            return left.A == right.A & left.B == right.B;
        }
        /// <summary>
        /// Returns a value that indicates whether two complex numbers are not equal.
        /// </summary>
        /// <param name="left">The first complex number to compare.</param>
        /// <param name="right">The second complex number to compare.</param>
        /// <returns>true if left and right are not equal; otherwise, false.</returns>
        public static bool operator !=(Complex left, Complex right)
        {
            return left.A != right.A | left.B != right.B;
        }
        #endregion

        #region ToString
        /// <summary>
        /// Converts the value of the current complex number to its equivalent string
        /// representation in Cartesian form.
        /// </summary>
        /// <returns>The string representation of the current instance in Cartesian form.</returns>
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Converts the value of the current complex number to its equivalent string
        /// representation in Cartesian form by using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the current instance in Cartesian form, as specified
        /// by provider.</returns>
        public string ToString(IFormatProvider provider)
        {
            return ToString("G", provider);
        }
        /// <summary>
        /// Converts the value of the current complex number to its equivalent string
        /// representation in Cartesian form by using the specified format for its real
        /// and imaginary parts.
        /// </summary>
        /// <param name="format">A standard or custom numeric format string.</param>
        /// <returns>The string representation of the current instance in Cartesian form.</returns>
        /// <exception cref="System.FormatException">format is not a valid format string.</exception>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Converts the value of the current complex number to its equivalent string
        /// representation in Cartesian form by using the specified format and culture-specific
        /// format information for its real and imaginary parts.
        /// </summary>
        /// <param name="format">A standard or custom numeric format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the current instance in Cartesian form, as specified
        /// by format and provider.</returns>
        /// <exception cref="System.FormatException">format is not a valid format string.</exception>
        public string ToString(string format, IFormatProvider provider)
        {
            return String.Format("{0} + {1}i", A.ToString(format, provider), B.ToString(format, provider));
        }
        #endregion
    }
}
