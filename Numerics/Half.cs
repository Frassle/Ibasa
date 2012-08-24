using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Security;

namespace Ibasa
{
    /// <summary>
    /// Represents a half-precision floating-point number.
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    public struct Half : IFormattable, IConvertible, IComparable, IComparable<Half>, IEquatable<Half>
    {
        #region Constants
        /// <summary>
        /// Represents the smallest positive Half value that is greater than
        /// zero. This field is constant.
        /// </summary>
        public static readonly Half Epsilon = new Half(0x0001);
        /// <summary>
        /// Represents the largest possible value of Half. This field is constant.
        /// </summary>
        public static readonly Half MaxValue = new Half(0x7BFF);
        /// <summary>
        /// Represents the smallest possible value of Half. This field is constant.
        /// </summary>
        public static readonly Half MinValue = new Half(0xFBFF);
        /// <summary>
        /// Represents not a number (NaN). This field is constant.
        /// </summary>
        public static readonly Half NaN = new Half(0x7FFF);
        /// <summary>
        /// Represents negative infinity. This field is constant.
        /// </summary>
        public static readonly Half NegativeInfinity = new Half(0xFC00);
        /// <summary>
        /// Represents positive infinity. This field is constant.
        /// </summary>
        public static readonly Half PositiveInfinity = new Half(0x7C00);
        #endregion

        #region Fields
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly ushort Value;
        #endregion

        #region Constructors
        private Half(ushort value)
        {
            Value = value;
        }
        #endregion

        #region Operations
        /// <summary>
        /// Increments a half-precision floating point number by 1.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>The value of the value parameter incremented by 1.</returns>
        public static Half operator ++(Half value)
        {
            return (Half)(Unpack(value) + 1.0f);
        }
        /// <summary>
        /// Decrements a half-precision floating point number by 1.
        /// </summary>
        /// <param name="value">The value to decrement.</param>
        /// <returns>The value of the value parameter decremented by 1.</returns>
        public static Half operator --(Half value)
        {
            return (Half)(Unpack(value) - 1.0f);
        }
        /// <summary>
        /// Returns the value of the Half operand. 
        /// (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">A half-precision value.</param>
        /// <returns>The value of the value operand.</returns>
        public static float operator +(Half value)
        {
            return +Unpack(value);
        }
        /// <summary>
        ///  Negates a specified Half value.
        /// </summary>
        /// <param name="value">The value to negate.</param>
        /// <returns>The result of the value parameter multiplied by negative one (-1).</returns>
        public static float operator -(Half value)
        {
            return -Unpack(value);
        }
        /// <summary>
        /// Adds the values of two specified Half values.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of left and right.</returns>
        public static float operator +(Half left, Half right)
        {
            return Unpack(left) + Unpack(right);
        }
        /// <summary>
        /// Subtracts a Half value from another Half value.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting right from left.</returns>
        public static float operator -(Half left, Half right)
        {
            return Unpack(left) - Unpack(right);
        }
        /// <summary>
        /// Multiplies two specified Half values.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of left and right.</returns>
        public static float operator *(Half left, Half right)
        {
            return Unpack(left) * Unpack(right);
        }
        /// <summary>
        /// Divides a specified Half value by another specified Half value.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        public static float operator /(Half left, Half right)
        {
            return Unpack(left) / Unpack(right);
        }
        /// <summary>
        /// Returns the remainder that results from division with two specified Half values.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The remainder that results from the division.</returns>
        public static float operator %(Half left, Half right)
        {
            return Unpack(left) % Unpack(right);
        }
        #endregion

        #region Functions

        private static float Int32BitsToSingle(int x)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(x), 0);
        }

        private static int SingleToInt32Bits(float x)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(x), 0);
        }

        #region Convert
        static Half Pack(float value)
        {
            uint full = (uint)SingleToInt32Bits(value);

            uint s = (full >> 16) & 0x8000;
            uint e = full & 0x7F800000;
            uint f = full & 0x007FFFFF;

            if (e == 0x7F800000)
            {
                if (f == 0)
                {
                    //Infinity
                    return new Half((ushort)(s | 0x7C00));
                }
                else
                {
                    //NaN
                    return new Half((ushort)(s | 0x7FFF));
                }
            }
            else if (e == 0)
            {
                //Zero and denorm, denorms will never fit into a 16bit.
                return new Half((ushort)s);
            }
            else
            {
                //Normalized
                //0x47000000 = (15 + 127) << 23;
                //0x38800000 = (-14 + 127) << 23;
                //0x33800000 = (-24 + 127) << 23;

                if (e > 0x47000000)
                {
                    //Overflow to infinity
                    return new Half((ushort)(s | 0x7C00));
                }
                else if (e < 0x33800000)
                {
                    //Underflow to 0
                    return new Half((ushort)s);
                }
                else if (e < 0x38800000)
                {
                    //Denormalized 16bit
                    //0x33800000 <= e < 0x38800000
                    //10 <= shiftamount < 0

                    // -24 <= e < -14

                    //int shift = 13 + (Math.Abs((e >> 23) - 127) - 14);
                    //int shift = 126 - (e >> 23);

                    return new Half((ushort)(s | ((0x800000 | f) >> (int)(126 - (e >> 23)))));
                }
                else
                {
                    //Normalized 16bit
                    //0x38000000 = (127 - 15) << 23;

                    return new Half((ushort)(s | ((e - 0x38000000) >> 13) | (f >> 13)));
                }
            }
        }
        static float Unpack(Half value)
        {
            int s = (value.Value & 0x8000) << 16;
            int e = value.Value & 0x7C00;
            int f = value.Value & 0x03FF;

            if (e == 0x7C00)
            {
                if (f == 0)
                {
                    //Infinity
                    return Int32BitsToSingle(s | 0x7F800000);
                }
                else
                {
                    //NaN
                    return Int32BitsToSingle(s | 0x7F800000 | (f << 13));
                }
            }
            else if (e == 0)
            {
                if (f == 0)
                {
                    //Zero
                    return Int32BitsToSingle(s);
                }
                else
                {
                    //Denormalized
                    //0x800000 = 1<<23;
                    //0x38800000 = (-14 + 127) << 23;

                    //Normalize
                    f <<= 13;

                    while ((f & 0x800000) == 0)
                    {
                        f <<= 1;
                        e -= 0x800000;
                    }

                    return Int32BitsToSingle(s | (e + 0x38800000) | (f & ~0x800000));
                }
            }
            else
            {
                //Normalized
                //0x1C000 = (-15 + 127) << 10;
                return Int32BitsToSingle(s | ((e + 0x1C000) << 13) | (f << 13));
            }
        }
        #endregion
        #region Extensions
        /// <summary>
        /// Converts the specified half-precision floating point number to a 16-bit
        /// signed integer.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A 16-bit signed integer whose value is equivalent to value.</returns>
        [SecuritySafeCritical]
        public static short HalfToInt16Bits(Half value)
        {
            return (short)value.Value;
        }
        /// <summary>
        /// Converts the specified 16-bit signed integer to a half-precision floating
        /// point number.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A half-precision floating point number whose value is equivalent to value.</returns>
        [SecuritySafeCritical]
        public static Half Int16BitsToHalf(short value)
        {
            return new Half((ushort)value);
        }
        /// <summary>
        /// Returns a half-precision floating point number converted from two bytes
        /// at a specified position in a byte array.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <returns>
        /// A half-precision floating point number formed by two bytes beginning at
        /// startIndex.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// startIndex is equal to the length of value minus 1.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// value is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex is less than zero or greater than the length of value minus 1.
        /// </exception>
        [SecuritySafeCritical]
        public static Half ToHalf(byte[] value, int startIndex)
        {
            return new Half(BitConverter.ToUInt16(value, startIndex));
        }
        /// <summary>
        /// Returns the specified half-precision floating point value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 2.</returns>
        [SecuritySafeCritical]
        public static byte[] GetBytes(Half value)
        {
            return BitConverter.GetBytes(value.Value);
        }
        #endregion
        /// <summary>
        /// Returns the absolute value of a half-precision floating-point number.
        /// </summary>
        /// <param name="value">A number in the range Half.MinValue ≤ value ≤ Half.MaxValue.</param>
        /// <returns>A half-precision floating-point number, x, such that 0 ≤ x ≤ Half.MaxValue.</returns>
        public static Half Abs(Half value)
        {
            return new Half((ushort)(value.Value & ~0x8000u));
        }
        /// <summary>
        /// Returns the larger of two half-precision floating-point numbers.
        /// </summary>
        /// <param name="val1">The first of two half-precision floating-point numbers to compare.</param>
        /// <param name="val2">The second of two half-precision floating-point numbers to compare.</param>
        /// <returns>
        /// Parameter val1 or val2, whichever is larger. If val1, or val2, or both val1
        /// and val2 are equal to Half.NaN, Half.NaN is returned.
        /// </returns>
        public static Half Max(Half val1, Half val2)
        {
            if (val1 > val2)
                return val1;
            if (val2 > val1)
                return val2;

            return NaN;
        }
        /// <summary>
        /// Returns the smaller of two half-precision floating-point numbers.
        /// </summary>
        /// <param name="val1">The first of two half-precision floating-point numbers to compare.</param>
        /// <param name="val2">The second of two half-precision floating-point numbers to compare.</param>
        /// <returns>
        /// Parameter val1 or val2, whichever is smaller. If val1, or val2, or both val1
        /// and val2 are equal to Half.NaN, Half.NaN is returned.
        /// </returns>
        public static Half Min(Half val1, Half val2)
        {
            if (val1 < val2)
                return val1;
            if (val2 < val1)
                return val2;

            return NaN;
        }
        /// <summary>
        /// Returns a value indicating the sign of a half-precision floating-point number.
        /// </summary>
        /// <param name="value">A signed number.</param>
        /// <returns>
        /// A number that indicates the sign of value, as shown in the following table.
        /// <list type="table">  
        /// <listheader><term>Return value</term><description>Meaning</description></listheader>  
        /// <item><term>-1</term><description>value is less than zero.</description></item>  
        /// <item><term>0</term><description>value is equal to zero.</description></item>  
        /// <item><term>1</term><description>value is greater than zero.</description></item> 
        /// </list>
        /// </returns>
        /// <exception cref="System.ArithmeticException">value is equal to Half.NaN.</exception>
        public static int Sign(Half value)
        {
            if (IsNaN(value))
                throw new ArithmeticException("value is equal to Half.NaN.");

            return
                value == 0 ? 0 :
                value < 0 ? -1 : 1;
        }
        #endregion

        #region Conversions
        #region To Half
        /// <summary>
        /// Defines an explicit conversion of a System.Decimal value to a half-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a half-precision floating-point number.</param>
        /// <returns>A half-precision floating-point number.</returns>
        public static explicit operator Half(decimal value)
        {
            return Pack((float)value);
        }
        /// <summary>
        /// Defines an explicit conversion of a double-precision floating-point number
        /// to a half-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a half-precision floating-point number.</param>
        /// <returns>A half-precision floating-point number.</returns>
        public static explicit operator Half(double value)
        {
            return Pack((float)value);
        }
        /// <summary>
        /// Defines an explicit conversion of a single-precision floating-point number
        /// to a half-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a half-precision floating-point number.</param>
        /// <returns>A half-precision floating-point number.</returns>
        public static explicit operator Half(float value)
        {
            return Pack(value);
        }
        /// <summary>
        /// Defines an implicit conversion of a 32-bit signed integer
        /// to a half-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a half-precision floating-point number.</param>
        /// <returns>A half-precision floating-point number.</returns>
        public static implicit operator Half(int value)
        {
            return Pack(value);
        }
        /// <summary>
        /// Defines an implicit conversion of a 64-bit signed integer
        /// to a half-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a half-precision floating-point number.</param>
        /// <returns>A half-precision floating-point number.</returns>
        public static implicit operator Half(long value)
        {
            return Pack(value);
        }
        /// <summary>
        /// Defines an implicit conversion of a signed byte
        /// to a half-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a half-precision floating-point number.</param>
        /// <returns>A half-precision floating-point number.</returns>
        [CLSCompliant(false)]
        public static implicit operator Half(sbyte value)
        {
            return Pack(value);
        }
        /// <summary>
        /// Defines an implicit conversion of a 16-bit signed integer
        /// to a half-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a half-precision floating-point number.</param>
        /// <returns>A half-precision floating-point number.</returns>
        public static implicit operator Half(short value)
        {
            return Pack(value);
        }
        /// <summary>
        /// Defines an implicit conversion of a 32-bit unsigned integer
        /// to a half-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a half-precision floating-point number.</param>
        /// <returns>A half-precision floating-point number.</returns>
        [CLSCompliant(false)]
        public static implicit operator Half(uint value)
        {
            return Pack(value);
        }
        /// <summary>
        /// Defines an implicit conversion of a 64-bit unsigned integer
        /// to a half-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a half-precision floating-point number.</param>
        /// <returns>A half-precision floating-point number.</returns>
        [CLSCompliant(false)]
        public static implicit operator Half(ulong value)
        {
            return Pack(value);
        }
        /// <summary>
        /// Defines an implicit conversion of an unsigned byte
        /// to a half-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a half-precision floating-point number.</param>
        /// <returns>A half-precision floating-point number.</returns>
        public static implicit operator Half(byte value)
        {
            return Pack(value);
        }
        /// <summary>
        /// Defines an implicit conversion of a 16-bit unsigned integer
        /// to a half-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a half-precision floating-point number.</param>
        /// <returns>A half-precision floating-point number.</returns>
        [CLSCompliant(false)]
        public static implicit operator Half(ushort value)
        {
            return Pack(value);
        }
        #endregion
        #region From Half
        /// <summary>
        /// Defines an explicit conversion of a half-precision floating-point number to a System.Decimal.
        /// </summary>
        /// <param name="value">The value to convert to a System.Decimal.</param>
        /// <returns>A System.Decimal.</returns>
        public static explicit operator decimal(Half value)
        {
            return (decimal)Unpack(value);
        }
        /// <summary>
        /// Defines an implicit conversion of a half-precision floating-point number to a double-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a double-precision floating-point number.</param>
        /// <returns>A double-precision floating-point number.</returns>
        public static implicit operator double(Half value)
        {
            return Unpack(value);
        }
        /// <summary>
        /// Defines an implicit conversion of a half-precision floating-point number to a single-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to convert to a single-precision floating-point number.</param>
        /// <returns>A single-precision floating-point number.</returns>
        public static implicit operator float(Half value)
        {
            return Unpack(value);
        }
        /// <summary>
        /// Defines an explicit conversion of a half-precision floating-point number to a 32-bit signed integer.
        /// </summary>
        /// <param name="value">The value to convert to a 32-bit signed integer.</param>
        /// <returns>A 32-bit signed integer.</returns>
        public static explicit operator int(Half value)
        {
            return (int)Unpack(value);
        }
        /// <summary>
        /// Defines an explicit conversion of a half-precision floating-point number to a 64-bit signed integer.
        /// </summary>
        /// <param name="value">The value to convert to a 64-bit signed integer.</param>
        /// <returns>A 64-bit signed integer.</returns>
        public static explicit operator long(Half value)
        {
            return (long)Unpack(value);
        }
        /// <summary>
        /// Defines an explicit conversion of a half-precision floating-point number to a signed byte.
        /// </summary>
        /// <param name="value">The value to convert to a signed byte.</param>
        /// <returns>A signed byte.</returns>
        [CLSCompliant(false)]
        public static explicit operator sbyte(Half value)
        {
            return (sbyte)Unpack(value);
        }
        /// <summary>
        /// Defines an explicit conversion of a half-precision floating-point number to a 16-bit signed integer.
        /// </summary>
        /// <param name="value">The value to convert to a 16-bit signed integer.</param>
        /// <returns>A 16-bit signed integer.</returns>
        public static explicit operator short(Half value)
        {
            return (short)Unpack(value);
        }
        /// <summary>
        /// Defines an explicit conversion of a half-precision floating-point number to a 32-bit unsigned integer.
        /// </summary>
        /// <param name="value">The value to convert to a 32-bit unsigned integer.</param>
        /// <returns>A 32-bit unsigned integer.</returns>
        [CLSCompliant(false)]
        public static explicit operator uint(Half value)
        {
            return (uint)Unpack(value);
        }
        /// <summary>
        /// Defines an explicit conversion of a half-precision floating-point number to a 64-bit unsigned integer.
        /// </summary>
        /// <param name="value">The value to convert to a 64-bit unsigned integer.</param>
        /// <returns>A 64-bit unsigned integer.</returns>
        [CLSCompliant(false)]
        public static explicit operator ulong(Half value)
        {
            return (ulong)Unpack(value);
        }
        /// <summary>
        /// Defines an explicit conversion of a half-precision floating-point number to an unsigned byte.
        /// </summary>
        /// <param name="value">The value to convert to an unsigned byte.</param>
        /// <returns>An unsigned byte.</returns>
        public static explicit operator byte(Half value)
        {
            return (byte)Unpack(value);
        }
        /// <summary>
        /// Defines an explicit conversion of a half-precision floating-point number to a 16-bit unsigned integer.
        /// </summary>
        /// <param name="value">The value to convert to a 16-bit unsigned integer.</param>
        /// <returns>A 16-bit unsigned integer.</returns>
        [CLSCompliant(false)]
        public static explicit operator ushort(Half value)
        {
            return (ushort)Unpack(value);
        }
        #endregion
        #endregion

        #region Comparable
        /// <summary>
        /// Compares this instance to a specified object and returns an integer that
        /// indicates whether the value of this instance is less than, equal to, or greater
        /// than the value of the specified object.
        /// </summary>
        /// <param name="value">An object to compare, or null.</param>
        /// <returns>
        /// <list type="table">
        /// <listheader><term>Return Value</term></listheader>
        /// <listheader><description>Less than zero</description></listheader>
        /// <listheader><description>Zero</description></listheader>
        /// <listheader><description>Greater than zero</description></listheader>
        /// <listheader><term>Description</term></listheader>
        /// <listheader><description>
        /// This instance is less than value.
        /// This instance is not a number (NaN) and value is a number.
        /// </description></listheader>
        /// <listheader><description>
        /// This instance is equal to value.
        /// Both this instance and value are not a number (NaN), PositiveInfinity, or NegativeInfinity.
        /// </description></listheader>
        /// <listheader><description>
        /// This instance is greater than value.
        /// This instance is a number and value is not a number (NaN). 
        /// </description></listheader>
        /// </list>
        /// </returns>
        /// <exception cref="System.ArgumentException">value is not a Half.</exception>
        public int CompareTo(object value)
        {
            if (value is Half)
                return CompareTo((Half)value);

            throw new ArgumentException("value is not a Half.");
        }
        /// <summary>
        /// Compares this instance to a specified half-precision floating-point number
        /// and returns an integer that indicates whether the value of this instance
        /// is less than, equal to, or greater than the value of the specified half-precision
        /// floating-point number.
        /// </summary>
        /// <param name="value">A half-precision floating-point number to compare.</param>
        /// <returns>
        /// <list type="table">
        /// <listheader><term>Return Value</term></listheader>
        /// <listheader><description>Less than zero</description></listheader>
        /// <listheader><description>Zero</description></listheader>
        /// <listheader><description>Greater than zero</description></listheader>
        /// <listheader><term>Description</term></listheader>
        /// <listheader><description>
        /// This instance is less than value.
        /// This instance is not a number (NaN) and value is a number.
        /// </description></listheader>
        /// <listheader><description>
        /// This instance is equal to value.
        /// Both this instance and value are not a number (NaN), PositiveInfinity, or NegativeInfinity.
        /// </description></listheader>
        /// <listheader><description>
        /// This instance is greater than value.
        /// This instance is a number and value is not a number (NaN). 
        /// </description></listheader>
        /// </list>
        /// </returns>
        public int CompareTo(Half value)
        {
            //return ((float)this).CompareTo((float)value);

            if (IsNaN(this) && !IsNaN(value))
                return -1;
            if (!IsNaN(this) && IsNaN(value))
                return 1;

            if (
                (IsNaN(this) && IsNaN(value)) ||
                (IsPositiveInfinity(this) && IsPositiveInfinity(value)) ||
                (IsNegativeInfinity(this) && IsNegativeInfinity(value)))
                return 0;

            bool thislt0 = (this.Value > 0x8000);
            bool valuelt0 = (value.Value > 0x8000);

            if (thislt0 && !valuelt0)
                return -1;
            if (!thislt0 && valuelt0)
                return 1;

            if (thislt0 && valuelt0)
            {
                //negative space, larger == smaller
                return value.Value.CompareTo(this.Value);
            }
            else
            {
                //positive space, larger == larger
                return this.Value.CompareTo(value.Value);
            }
        }
        /// <summary>
        /// Returns a value that indicates whether a specified Half value is
        /// less than another specified Half value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left is less than right; otherwise, false.</returns>
        public static bool operator <(Half left, Half right)
        {
            //NaN always compares false
            if (IsNaN(left) || IsNaN(right))
                return false;
            return left.CompareTo(right) < 0;
        }
        /// <summary>
        /// Returns a value that indicates whether a specified Half value is
        /// less than or equal to another specified Half value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left is less than or equal to right; otherwise, false.</returns>
        public static bool operator <=(Half left, Half right)
        {
            //NaN always compares false
            if (IsNaN(left) || IsNaN(right))
                return false;
            return left.CompareTo(right) <= 0;
        }
        /// <summary>
        /// Returns a value that indicates whether a specified Half value is
        /// greater than another specified Half value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left is greater than right; otherwise, false.</returns>
        public static bool operator >(Half left, Half right)
        {
            //NaN always compares false
            if (IsNaN(left) || IsNaN(right))
                return false;
            return left.CompareTo(right) > 0;
        }
        /// <summary>
        /// Returns a value that indicates whether a specified Half value is
        /// greater than or equal to another specified Half value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left is greater than or equal to right; otherwise, false.</returns>
        public static bool operator >=(Half left, Half right)
        {
            //NaN always compares false
            if (IsNaN(left) || IsNaN(right))
                return false;
            return left.CompareTo(right) >= 0;
        }
        /// <summary>
        /// Returns a value that indicates whether two specified Half values are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left and right are equal; otherwise, false.</returns>
        public static bool operator ==(Half left, Half right)
        {
            //NaN always compares false
            if (IsNaN(left) || IsNaN(right))
                return false;
            return left.Value == right.Value;
        }
        /// <summary>
        /// Returns a value that indicates whether two specified Half values are not equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if left and right are not equal; otherwise, false.</returns>
        public static bool operator !=(Half left, Half right)
        {
            //NaN always compares false
            if (IsNaN(left) || IsNaN(right))
                return false;
            return left.Value != right.Value;
        }
        #endregion

        #region Convertible
        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Equatable
        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative
        /// or positive infinity.
        /// </summary>
        /// <param name="h">A half-precision floating-point number.</param>
        /// <returns>true if h evaluates to Half.PositiveInfinity or Half.NegativeInfinity;
        /// otherwise, false.</returns>
        public static bool IsInfinity(Half h)
        {
            int e = h.Value & 0x7C00;
            int f = h.Value & 0x03FF;

            return (e == 0x7C00) && (f == 0);
        }
        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to not
        /// a number (Half.NaN).
        /// </summary>
        /// <param name="h">A half-precision floating-point number.</param>
        /// <returns>true if h evaluates to not a number (Half.NaN); otherwise, false.</returns>
        public static bool IsNaN(Half h)
        {
            int e = h.Value & 0x7C00;
            int f = h.Value & 0x03FF;

            return (e == 0x7C00) && (f != 0);
        }
        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative
        /// infinity.
        /// </summary>
        /// <param name="h">A half-precision floating-point number.</param>
        /// <returns>true if h evaluates to Half.NegativeInfinity; otherwise, false.</returns>
        public static bool IsNegativeInfinity(Half h)
        {
            int s = (h.Value & 0x8000) << 16;
            int e = h.Value & 0x7C00;
            int f = h.Value & 0x03FF;

            return (e == 0x7C00) && (f == 0) && (s != 0);
        }
        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to positive
        /// infinity.
        /// </summary>
        /// <param name="h">A half-precision floating-point number.</param>
        /// <returns>true if h evaluates to Half.PositiveInfinity; otherwise, false.</returns>
        public static bool IsPositiveInfinity(Half h)
        {
            int s = (h.Value & 0x8000) << 16;
            int e = h.Value & 0x7C00;
            int f = h.Value & 0x03FF;

            return (e == 0x7C00) && (f == 0) && (s == 0);
        }

        /// <summary>
        /// Returns the System.TypeCode for value type Half.
        /// </summary>
        /// <returns>The enumerated constant, System.TypeCode.Object.</returns>
        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified
        /// object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>true if obj is an instance of Half and equals the value of this
        /// instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Half)
                return Equals((Half)obj);

            return false;
        }
        /// <summary>
        /// Returns a value indicating whether this instance and a specified Half
        /// object represent the same value.
        /// </summary>
        /// <param name="obj">A Half object to compare to this instance.</param>
        /// <returns>true if obj is equal to this instance; otherwise, false.</returns>
        public bool Equals(Half obj)
        {
            return this == obj;
        }
        /// <summary>
        /// Returns a value that indicates whether two Halfs are equal.
        /// </summary>
        /// <param name="left">The first Half to compare.</param>
        /// <param name="right">The second Half to compare.</param>
        /// <returns>true if the left and right are equal; otherwise, false.</returns>
        public static bool Equals(Half left, Half right)
        {
            return left == right;
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
            return Unpack(this).ToString(format, provider);
        }
        #endregion
    }

    public static class HalfExtensions
    {
        public static void Write(this Ibasa.IO.BinaryWriter writer, Half half)
        {
            writer.Write(Half.HalfToInt16Bits(half));
        }

        public static Half ReadHalf(this Ibasa.IO.BinaryReader reader)
        {
            return Half.Int16BitsToHalf(reader.ReadInt16());
        }
    }
}