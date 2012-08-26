using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace Ibasa.Numerics
{
    /// <summary>
    /// Defines a rational number as a numerator / denominator pair.
    /// </summary>
    [Serializable]
    public struct Rational // : IComparable, IFormattable, IConvertible, IComparable<Rational>, IEquatable<Rational>
    {
        #region Constants
        // Summary:
        //     Represents the smallest positive System.Single value greater than zero. This
        //     field is constant.
        public static readonly Rational Epsilon = new Rational(1, int.MaxValue, false);
        //
        // Summary:
        //     Represents the largest possible value of System.Single. This field is constant.
        public static readonly Rational MaxValue = new Rational(int.MaxValue, 1, false);
        //
        // Summary:
        //     Represents the smallest possible value of System.Single. This field is constant.
        public static readonly Rational MinValue = new Rational(int.MinValue, 1, false);
        #endregion

        #region Fields
        /// <summary>
        /// Gets the numerator of the rational pair.
        /// </summary>
        public readonly int Numerator;
        /// <summary>
        /// Gets the denominator of the rational pair.
        /// </summary>
        public readonly int Denominator;
        #endregion

        #region Constructor
        [Pure]
        private Rational(int numerator, int denominator, bool normalize)
        {
            if (denominator < 1)
                throw new ArgumentException("denominator must be positive.", "denominator");

            if (normalize)
            {
                int gcd = Functions.GCD(numerator, denominator);
                Numerator = numerator / gcd;
                Denominator = denominator / gcd;
            }
            else
            {
                Numerator = numerator;
                Denominator = denominator;
            }
        }
        [Pure]
        public Rational(int numerator, int denominator)
        {
            if (denominator < 1)
                throw new ArgumentException("denominator must be positive.", "denominator");

            int gcd = Functions.GCD(numerator, denominator);
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }
        #endregion

        #region Simplify
        static int CommonRationals(Rational a, Rational b, out int newA, out int newB)
        {
            if (a.Denominator == b.Denominator)
            {
                newA = a.Numerator;
                newB = b.Numerator;
                return a.Denominator;
            }
            else
            {
                int lcm = Functions.LCM(a.Denominator, b.Denominator);
                newA = a.Numerator * (lcm / a.Denominator);
                newB = b.Numerator * (lcm / b.Denominator);
                return lcm;
            }
        }
        #endregion

        #region Operations
        public static Rational operator +(Rational value)
        {
            return value;
        }
        public static Rational operator -(Rational value)
        {
            return new Rational(-value.Numerator, value.Denominator);
        }
        public static Rational operator +(Rational left, Rational right)
        {
            int a, b;
            int lcm = CommonRationals(left, right, out a, out b);
            return new Rational(a + b, lcm);
        }
        public static Rational operator -(Rational left, Rational right)
        {
            int a, b;
            int lcm = CommonRationals(left, right, out a, out b);
            return new Rational(a - b, lcm);
        }
        public static Rational operator *(Rational left, Rational right)
        {
            return new Rational(left.Numerator * right.Numerator, left.Denominator * right.Denominator);
        }
        public static Rational operator /(Rational left, Rational right)
        {
            return new Rational(left.Numerator * right.Denominator, left.Denominator * right.Numerator);
        }
        public static Rational operator %(Rational left, Rational right)
        {
            Rational div = left / right;
            int q = div.Numerator / div.Denominator;

            return left - right * q;
        }

        public static bool operator >(Rational left, Rational right)
        {
            int a, b;
            int lcm = CommonRationals(left, right, out a, out b);
            return (a > b);
        }
        public static bool operator >=(Rational left, Rational right)
        {
            int a, b;
            int lcm = CommonRationals(left, right, out a, out b);
            return (a >= b);
        }
        public static bool operator <(Rational left, Rational right)
        {
            int a, b;
            int lcm = CommonRationals(left, right, out a, out b);
            return (a < b);
        }
        public static bool operator <=(Rational left, Rational right)
        {
            int a, b;
            int lcm = CommonRationals(left, right, out a, out b);
            return (a <= b);
        }

        #region Conversions
        public static implicit operator Rational(byte value)
        {
            return new Rational((int)value, 1);
        }
        public static implicit operator Rational(short value)
        {
            return new Rational((int)value, 1);
        }
        public static implicit operator Rational(int value)
        {
            return new Rational((int)value, 1);
        }
        public static explicit operator Rational(long value)
        {
            return new Rational((int)value, 1);
        }
        [CLSCompliant(false)]
        public static implicit operator Rational(sbyte value)
        {
            return new Rational((int)value, 1);
        }
        [CLSCompliant(false)]
        public static implicit operator Rational(ushort value)
        {
            return new Rational((int)value, 1);
        }
        [CLSCompliant(false)]
        public static explicit operator Rational(uint value)
        {
            return new Rational((int)value, 1);
        }
        [CLSCompliant(false)]
        public static explicit operator Rational(ulong value)
        {
            return new Rational((int)value, 1);
        }

        public static explicit operator Rational(float value)
        {
            int tens = 0;
            while (Functions.Abs(value - Functions.Round(value)) > float.Epsilon)
            {
                ++tens;
                value *= 10;
            }

            return new Rational((int)value, tens == 0 ? 1 : tens * 10);
        }
        public static explicit operator Rational(double value)
        {
            int tens = 0;
            while (Functions.Abs(value - Functions.Round(value)) > double.Epsilon)
            {
                ++tens;
                value *= 10;
            }

            return new Rational((int)value, tens == 0 ? 1 : tens * 10);
        }
        public static explicit operator Rational(decimal value)
        {
            int tens = 0;
            while (Functions.Abs(value - Functions.Round(value)) > 0)
            {
                ++tens;
                value *= 10;
            }

            return new Rational((int)value, tens == 0 ? 1 : tens * 10);
        }

        public static explicit operator byte(Rational value)
        {
            return (byte)(value.Numerator / value.Denominator);
        }
        public static explicit operator short(Rational value)
        {
            return (short)(value.Numerator / value.Denominator);
        }
        public static explicit operator int(Rational value)
        {
            return (int)(value.Numerator / value.Denominator);
        }
        public static explicit operator long(Rational value)
        {
            return (long)(value.Numerator / value.Denominator);
        }
        [CLSCompliant(false)]
        public static explicit operator sbyte(Rational value)
        {
            return (sbyte)(value.Numerator / value.Denominator);
        }
        [CLSCompliant(false)]
        public static explicit operator ushort(Rational value)
        {
            return (ushort)(value.Numerator / value.Denominator);
        }
        [CLSCompliant(false)]
        public static explicit operator uint(Rational value)
        {
            return (uint)(value.Numerator / value.Denominator);
        }
        [CLSCompliant(false)]
        public static explicit operator ulong(Rational value)
        {
            return (ulong)(value.Numerator / value.Denominator);
        }

        public static explicit operator float(Rational value)
        {
            return (float)value.Numerator / value.Denominator;
        }
        public static explicit operator double(Rational value)
        {
            return (double)value.Numerator / value.Denominator;
        }
        public static explicit operator decimal(Rational value)
        {
            return (decimal)value.Numerator / value.Denominator;
        }
        #endregion
        #endregion

        #region Equatable
        public override int GetHashCode()
        {
            return Numerator.GetHashCode() + Denominator.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is Rational)
                return Equals((Rational)obj);

            return false;
        }
        public bool Equals(Rational other)
        {
            return this == other;
        }
        public static bool Equals(Rational left, Rational right)
        {
            return left == right;
        }
        public static bool operator ==(Rational left, Rational right)
        {
            int a, b;
            int lcm = CommonRationals(left, right, out a, out b);
            return (a == b);
        }
        public static bool operator !=(Rational left, Rational right)
        {
            int a, b;
            int lcm = CommonRationals(left, right, out a, out b);
            return (a != b);
        }
        #endregion

        #region ToString
        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation
        /// using the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by provider.</returns>
        public string ToString(IFormatProvider provider)
        {
            return ToString("G", provider);
        }
        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation,
        /// using the specified format.
        /// </summary>
        /// <param name="format">A standard or custom numeric format string.</param>
        /// <returns>The string representation of the value of this instance as specified by format.</returns>
        /// <exception cref="System.FormatException">format is invalid.</exception>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation
        /// using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by format
        /// and provider.</returns>
        public string ToString(string format, IFormatProvider provider)
        {
            return String.Format("{0}/{1}",
                Numerator.ToString(format, provider),
                Denominator.ToString(format, provider));
        }
        #endregion

        #region Functions
        /// <summary>
        /// Returns the absolute value of a rational number.
        /// </summary>
        /// <param name="value">A number in the range Rational.MinValue &lt; value ≤ Rational.MaxValue.</param>
        /// <returns>A rational number, x, such that 0 ≤ x ≤ Rational.MaxValue</returns>
        public static Rational Abs(Rational value)
        {
            if (value < 0)
                return -value;
            else
                return value;
        }
        #endregion
    }
}