using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Security;

namespace Ibasa
{
    /// <summary>
    /// Converts base data types to an array of bytes, and an array of bytes to base data types.
    /// </summary>
    public static class BitConverter
    {
        #region IsLittleEndian
        /// <summary>
        /// Indicates the byte order ("endianess") in which data is stored in this computer architecture.
        /// </summary>
        public static readonly bool IsLittleEndian = System.BitConverter.IsLittleEndian;
        #endregion

        #region FloatToInt
        /// <summary>
        /// Converts the specified double-precision floating point number to a 64-bit signed integer.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A 64-bit signed integer whose value is equivalent to value.</returns>
        [SecuritySafeCritical]
        public static long DoubleToInt64Bits(double value)
        {
            return System.BitConverter.DoubleToInt64Bits(value);
        }
        /// <summary>
        /// Converts the specified 64-bit signed integer to a double-precision floating point number.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A double-precision floating point number whose value is equivalent to value.</returns>
        [SecuritySafeCritical]
        public static double Int64BitsToDouble(long value)
        {
            return System.BitConverter.Int64BitsToDouble(value);
        }
        /// <summary>
        /// Converts the specified single-precision floating point number to a 32-bit
        /// signed integer.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A 32-bit signed integer whose value is equivalent to value.</returns>
        [SecuritySafeCritical]
        public static int SingleToInt32Bits(float value)
        {
            return ToInt32(GetBytes(value), 0);
        }
        /// <summary>
        /// Converts the specified 32-bit signed integer to a single-precision floating
        /// point number.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A single-precision floating point number whose value is equivalent to value.</returns>
        [SecuritySafeCritical]
        public static float Int32BitsToSingle(int value)
        {
            return ToSingle(GetBytes(value), 0);
        }
        #endregion

        #region GetBytes(T value)
        /// <summary>
        /// Returns the specified byte value as an array of bytes.
        /// </summary>
        /// <param name="value">A byte value.</param>
        /// <returns>An array of bytes with length 1.</returns>
        [SecuritySafeCritical]
        public static byte[] GetBytes(byte value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 1);

            return new byte[] { value };
        }
        /// <summary>
        /// Returns the specified sbyte value as an array of bytes.
        /// </summary>
        /// <param name="value">A sbyte value.</param>
        /// <returns>An array of bytes with length 1.</returns>
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static byte[] GetBytes(sbyte value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 1);

            return new byte[] { (byte)value };
        }
        /// <summary>
        /// Returns the specified Boolean value as an array of bytes.
        /// </summary>
        /// <param name="value">A Boolean value.</param>
        /// <returns>An array of bytes with length 1.</returns>
        [SecuritySafeCritical]
        public static byte[] GetBytes(bool value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 1);

            return System.BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Returns the specified Unicode character value as an array of bytes.
        /// </summary>
        /// <param name="value">A character to convert.</param>
        /// <returns>An array of bytes with length 2.</returns>
        [SecuritySafeCritical]
        public static byte[] GetBytes(char value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 2);

            return System.BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Returns the specified decimal value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 16.</returns>
        [SecuritySafeCritical]
        public static byte[] GetBytes(decimal value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 16);

            int[] bits = decimal.GetBits(value);
            byte[] bytes = new byte[16];
            Buffer.BlockCopy(bits, 0, bytes, 0, 16);
            return bytes;
        }
        /// <summary>
        /// Returns the specified double-precision floating point value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 8.</returns>
        [SecuritySafeCritical]
        public static byte[] GetBytes(double value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 8);

            return System.BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Returns the specified single-precision floating point value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 4.</returns>
        [SecuritySafeCritical]
        public static byte[] GetBytes(float value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 4);

            return System.BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Returns the specified 32-bit signed integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 4.</returns>
        [SecuritySafeCritical]
        public static byte[] GetBytes(int value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 4);

            return System.BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Returns the specified 64-bit signed integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 8.</returns>
        [SecuritySafeCritical]
        public static byte[] GetBytes(long value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 8);

            return System.BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Returns the specified 16-bit signed integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 2.</returns>
        [SecuritySafeCritical]
        public static byte[] GetBytes(short value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 2);

            return System.BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Returns the specified 32-bit unsigned integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 4.</returns>
        [CLSCompliant(false)]
        public static byte[] GetBytes(uint value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 4);

            return System.BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Returns the specified 64-bit unsigned integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 8.</returns>
        [CLSCompliant(false)]
        public static byte[] GetBytes(ulong value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 8);

            return System.BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Returns the specified 16-bit unsigned integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 2.</returns>
        [CLSCompliant(false)]
        public static byte[] GetBytes(ushort value)
        {
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length == 2);

            return System.BitConverter.GetBytes(value);
        }
        #endregion

        #region GetBytes(byte[] buffer, int offset, T value)
        [SecuritySafeCritical]
        public static void GetBytes(byte[] buffer, int offset, byte value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset < buffer.Length);

            buffer[offset] = value;
        }
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static void GetBytes(byte[] buffer, int offset, sbyte value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset < buffer.Length);

            buffer[offset] = (byte)value;
        }
        [SecuritySafeCritical]
        public static void GetBytes(byte[] buffer, int offset, bool value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset < buffer.Length);

            byte[] bytes = System.BitConverter.GetBytes(value);
            buffer[offset] = bytes[0];
        }
        [SecuritySafeCritical] 
        public static void GetBytes(byte[] buffer, int offset, char value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset <= buffer.Length - 2);

            byte[] bytes = System.BitConverter.GetBytes(value);
            buffer[offset] = bytes[0];
            buffer[offset + 1] = bytes[1];
        }
        [SecuritySafeCritical]
        public static void GetBytes(byte[] buffer, int offset, decimal value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset <= buffer.Length - 16);

            byte[] bytes = GetBytes(value);
            buffer[offset] = bytes[0];
            buffer[offset + 1] = bytes[1];
            buffer[offset + 2] = bytes[2];
            buffer[offset + 3] = bytes[3];
            buffer[offset + 4] = bytes[4];
            buffer[offset + 5] = bytes[5];
            buffer[offset + 6] = bytes[6];
            buffer[offset + 7] = bytes[7];
            buffer[offset + 8] = bytes[8];
            buffer[offset + 9] = bytes[9];
            buffer[offset + 10] = bytes[10];
            buffer[offset + 11] = bytes[11];
            buffer[offset + 12] = bytes[12];
            buffer[offset + 13] = bytes[13];
            buffer[offset + 14] = bytes[14];
            buffer[offset + 15] = bytes[15];
        }
        [SecuritySafeCritical]
        public static void GetBytes(byte[] buffer, int offset, double value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset <= buffer.Length - 8);

            byte[] bytes = System.BitConverter.GetBytes(value);
            buffer[offset] = bytes[0];
            buffer[offset + 1] = bytes[1];
            buffer[offset + 2] = bytes[2];
            buffer[offset + 3] = bytes[3];
            buffer[offset + 4] = bytes[4];
            buffer[offset + 5] = bytes[5];
            buffer[offset + 6] = bytes[6];
            buffer[offset + 7] = bytes[7];
        }
        [SecuritySafeCritical]
        public static void GetBytes(byte[] buffer, int offset, float value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset <= buffer.Length - 4);

            byte[] bytes = System.BitConverter.GetBytes(value);
            buffer[offset] = bytes[0];
            buffer[offset + 1] = bytes[1];
            buffer[offset + 2] = bytes[2];
            buffer[offset + 3] = bytes[3];
        }
        [SecuritySafeCritical]
        public static void GetBytes(byte[] buffer, int offset, int value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset <= buffer.Length - 4);

            byte[] bytes = System.BitConverter.GetBytes(value);
            buffer[offset] = bytes[0];
            buffer[offset + 1] = bytes[1];
            buffer[offset + 2] = bytes[2];
            buffer[offset + 3] = bytes[3];
        }
        [SecuritySafeCritical]
        public static void GetBytes(byte[] buffer, int offset, long value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset <= buffer.Length - 8);

            byte[] bytes = System.BitConverter.GetBytes(value);
            buffer[offset] = bytes[0];
            buffer[offset + 1] = bytes[1];
            buffer[offset + 2] = bytes[2];
            buffer[offset + 3] = bytes[3];
            buffer[offset + 4] = bytes[4];
            buffer[offset + 5] = bytes[5];
            buffer[offset + 6] = bytes[6];
            buffer[offset + 7] = bytes[7];
        }
        [SecuritySafeCritical]
        public static void GetBytes(byte[] buffer, int offset, short value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset <= buffer.Length - 2);

            byte[] bytes = System.BitConverter.GetBytes(value);
            buffer[offset] = bytes[0];
            buffer[offset + 1] = bytes[1];
        }
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static void GetBytes(byte[] buffer, int offset, uint value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset <= buffer.Length - 4);

            byte[] bytes = System.BitConverter.GetBytes(value);
            buffer[offset] = bytes[0];
            buffer[offset + 1] = bytes[1];
            buffer[offset + 2] = bytes[2];
            buffer[offset + 3] = bytes[3];
        }
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static void GetBytes(byte[] buffer, int offset, ulong value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset <= buffer.Length - 8);

            byte[] bytes = System.BitConverter.GetBytes(value);
            buffer[offset] = bytes[0];
            buffer[offset + 1] = bytes[1];
            buffer[offset + 2] = bytes[2];
            buffer[offset + 3] = bytes[3];
            buffer[offset + 4] = bytes[4];
            buffer[offset + 5] = bytes[5];
            buffer[offset + 6] = bytes[6];
            buffer[offset + 7] = bytes[7];
        }
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static void GetBytes(byte[] buffer, int offset, ushort value)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= offset);
            Contract.Requires(offset <= buffer.Length - 2);

            byte[] bytes = System.BitConverter.GetBytes(value);
            buffer[offset] = bytes[0];
            buffer[offset + 1] = bytes[1];
        }
        #endregion

        #region T ToT(byte[] value, int startIndex)
        [SecuritySafeCritical]
        public static byte ToByte(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex < value.Length);

            return value[startIndex];
        }
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static sbyte ToSByte(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex < value.Length);

            return (sbyte)value[startIndex];
        }
        [SecuritySafeCritical]
        public static bool ToBoolean(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex < value.Length);

            return System.BitConverter.ToBoolean(value, startIndex);
        }
        //
        // Summary:
        //     Returns a Unicode character converted from two bytes at a specified position
        //     in a byte array.
        //
        // Parameters:
        //   value:
        //     An array.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A character formed by two bytes beginning at startIndex.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     startIndex equals the length of value minus 1.
        //
        //   System.ArgumentNullException:
        //     value is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        [SecuritySafeCritical]
        public static char ToChar(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex <= value.Length - 2);

            return System.BitConverter.ToChar(value, startIndex);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static decimal ToDecimal(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex <= value.Length - 16);

            int[] bits = new int[4];
            Buffer.BlockCopy(value, startIndex, bits, 0, 16);
            return new decimal(bits);
        }
        //
        // Summary:
        //     Returns a double-precision floating point number converted from eight bytes
        //     at a specified position in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A double precision floating point number formed by eight bytes beginning
        //     at startIndex.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     startIndex is greater than or equal to the length of value minus 7, and is
        //     less than or equal to the length of value minus 1.
        //
        //   System.ArgumentNullException:
        //     value is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        [SecuritySafeCritical]
        public static double ToDouble(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex <= value.Length - 8);
            return System.BitConverter.ToDouble(value, startIndex);
        }
        /// <summary>
        /// Returns a single-precision floating point number converted from four bytes
        /// at a specified position in a byte array.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <returns>
        /// A single-precision floating point number formed by four bytes beginning at
        /// startIndex.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// startIndex is greater than or equal to the length of value minus 3, and is
        /// less than or equal to the length of value minus 1.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// value is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex is less than zero or greater than the length of value minus 1.
        /// </exception>
        [SecuritySafeCritical]
        public static float ToSingle(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex <= value.Length - 4);
            return System.BitConverter.ToSingle(value, startIndex);
        }
        //
        // Summary:
        //     Returns a 32-bit signed integer converted from four bytes at a specified
        //     position in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A 32-bit signed integer formed by four bytes beginning at startIndex.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     startIndex is greater than or equal to the length of value minus 3, and is
        //     less than or equal to the length of value minus 1.
        //
        //   System.ArgumentNullException:
        //     value is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        [SecuritySafeCritical]
        public static int ToInt32(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex <= value.Length - 4);
            return System.BitConverter.ToInt32(value, startIndex);
        }
        //
        // Summary:
        //     Returns a 64-bit signed integer converted from eight bytes at a specified
        //     position in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A 64-bit signed integer formed by eight bytes beginning at startIndex.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     startIndex is greater than or equal to the length of value minus 7, and is
        //     less than or equal to the length of value minus 1.
        //
        //   System.ArgumentNullException:
        //     value is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        [SecuritySafeCritical]
        public static long ToInt64(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex <= value.Length - 8);
            return System.BitConverter.ToInt64(value, startIndex);
        }
        //
        // Summary:
        //     Returns a 16-bit signed integer converted from two bytes at a specified position
        //     in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A 16-bit signed integer formed by two bytes beginning at startIndex.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     startIndex equals the length of value minus 1.
        //
        //   System.ArgumentNullException:
        //     value is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static short ToInt16(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex <= value.Length - 2);
            return System.BitConverter.ToInt16(value, startIndex);
        }
        //
        // Summary:
        //     Returns a 32-bit unsigned integer converted from four bytes at a specified
        //     position in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A 32-bit unsigned integer formed by four bytes beginning at startIndex.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     startIndex is greater than or equal to the length of value minus 3, and is
        //     less than or equal to the length of value minus 1.
        //
        //   System.ArgumentNullException:
        //     value is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static uint ToUInt32(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex <= value.Length - 4);
            return System.BitConverter.ToUInt32(value, startIndex);
        }
        //
        // Summary:
        //     Returns a 64-bit unsigned integer converted from eight bytes at a specified
        //     position in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A 64-bit unsigned integer formed by the eight bytes beginning at startIndex.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     startIndex is greater than or equal to the length of value minus 7, and is
        //     less than or equal to the length of value minus 1.
        //
        //   System.ArgumentNullException:
        //     value is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static ulong ToUInt64(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex <= value.Length - 8);
            return System.BitConverter.ToUInt64(value, startIndex);
        }
        //
        // Summary:
        //     Returns a 16-bit unsigned integer converted from two bytes at a specified
        //     position in a byte array.
        //
        // Parameters:
        //   value:
        //     The array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A 16-bit unsigned integer formed by two bytes beginning at startIndex.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     startIndex equals the length of value minus 1.
        //
        //   System.ArgumentNullException:
        //     value is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static ushort ToUInt16(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex <= value.Length - 2);
            return System.BitConverter.ToUInt16(value, startIndex);
        }
        //
        // Summary:
        //     Converts the numeric value of each element of a specified array of bytes
        //     to its equivalent hexadecimal string representation.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        // Returns:
        //     A System.String of hexadecimal pairs separated by hyphens, where each pair
        //     represents the corresponding element in value; for example, "7F-2C-4A".
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     value is null.
        [SecuritySafeCritical] 
        public static string ToString(byte[] value)
        {
            Contract.Requires(value != null);

            return System.BitConverter.ToString(value);
        }
        //
        // Summary:
        //     Converts the numeric value of each element of a specified subarray of bytes
        //     to its equivalent hexadecimal string representation.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A System.String of hexadecimal pairs separated by hyphens, where each pair
        //     represents the corresponding element in a subarray of value; for example,
        //     "7F-2C-4A".
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     value is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        [SecuritySafeCritical]
        public static string ToString(byte[] value, int startIndex)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(startIndex < value.Length);

            return System.BitConverter.ToString(value, startIndex);
        }
        /// <summary>
        /// Converts the numeric value of each element of a specified subarray of bytes
        /// to its equivalent hexadecimal string representation.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <param name="length">The number of array elements in value to convert.</param>
        /// <returns>
        /// A System.String of hexadecimal pairs separated by hyphens, where each pair
        /// represents the corresponding element in a subarray of value; for example,
        /// "7F-2C-4A".
        /// </returns>
        /// <exception cref="System.ArgumentNullException">value is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex or length is less than zero.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex is greater than zero and is greater than or equal to the length of value.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The combination of startIndex and length does not specify a position within
        /// value; that is, the startIndex parameter is greater than the length of value
        /// minus the length parameter.
        /// </exception>
        [SecuritySafeCritical]
        public static string ToString(byte[] value, int startIndex, int length)
        {
            Contract.Requires(value != null);
            Contract.Requires(0 <= startIndex);
            Contract.Requires(0 <= length);
            Contract.Requires(startIndex < value.Length - length);

            return System.BitConverter.ToString(value, startIndex, length);
        }
        #endregion

        #region SwapBytes
        [SecuritySafeCritical]
        public static byte SwapBytes(byte value)
        {
            return value;
        }
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static sbyte SwapBytes(sbyte value)
        {
            return value;
        }
        [SecuritySafeCritical]
        public static bool SwapBytes(bool value)
        {
            return value;
        }
        [SecuritySafeCritical]
        public static char SwapBytes(char value)
        {
            return (char)SwapBytes((ushort)value);
        }
        [SecuritySafeCritical]
        public static decimal SwapBytes(decimal value)
        {
            byte[] bytes = GetBytes(value);
            Array.Reverse(bytes);
            return ToDecimal(bytes, 0);
        }
        [SecuritySafeCritical]
        public static double SwapBytes(double value)
        {
            return Int64BitsToDouble(SwapBytes(DoubleToInt64Bits(value)));
        }
        [SecuritySafeCritical]
        public static float SwapBytes(float value)
        {
            return Int32BitsToSingle(SwapBytes(SingleToInt32Bits(value)));
        }
        [SecuritySafeCritical]
        public static int SwapBytes(int value)
        {
            return (int)SwapBytes((uint)value);
        }
        [SecuritySafeCritical]
        public static long SwapBytes(long value)
        {
            return (long)SwapBytes((ulong)value);
        }
        [SecuritySafeCritical]
        public static short SwapBytes(short value)
        {
            return (short)SwapBytes((ushort)value);
        }
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static uint SwapBytes(uint value)
        {
            return (uint)(SwapBytes((ushort)(value >> 0x10)) | (SwapBytes((ushort)value) << 0x10));
        }
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static ulong SwapBytes(ulong value)
        {
            return (ulong)SwapBytes((uint)(value >> 0x20)) | ((ulong)SwapBytes((uint)value) << 0x20);
        }
        [SecuritySafeCritical]
        [CLSCompliant(false)]
        public static ushort SwapBytes(ushort value)
        {
            return (ushort)((value >> 0x8) | (value << 0x8));
        }
        public static void SwapBytes(byte[] value)
        {
            Contract.Requires(value != null);
            for (int i = 0; i < value.Length / 2; ++i)
            {
                byte temp = value[i];
                value[i] = value[value.Length - (i + 1)];
                value[value.Length - (i + 1)] = temp;
            }
        }
        #endregion

        #region Conditional swap
        //
        // Summary:
        //     Converts an integer value from host byte order to network byte order.
        //
        // Parameters:
        //   host:
        //     The number to convert, expressed in host byte order.
        //
        // Returns:
        //     An integer value, expressed in network byte order.
        //public static int HostToNetworkOrder(int host);

        //
        // Summary:
        //     Converts an integer value from network byte order to host byte order.
        //
        // Parameters:
        //   network:
        //     The number to convert, expressed in network byte order.
        //
        // Returns:
        //     An integer value, expressed in host byte order.
        //public static int NetworkToHostOrder(int network);

        public static byte SwapLittleEndian(byte value)
        {
            return value;
        }
        public static short SwapLittleEndian(short value)
        {
            return IsLittleEndian ? value : SwapBytes(value);
        }
        public static int SwapLittleEndian(int value)
        {
            return IsLittleEndian ? value : SwapBytes(value);
        }
        public static long SwapLittleEndian(long value)
        {
            return IsLittleEndian ? value : SwapBytes(value);
        }
        [CLSCompliant(false)]
        public static sbyte SwapLittleEndian(sbyte value)
        {
            return value;
        }
        [CLSCompliant(false)]
        public static ushort SwapLittleEndian(ushort value)
        {
            return IsLittleEndian ? value : SwapBytes(value);
        }
        [CLSCompliant(false)]
        public static uint SwapLittleEndian(uint value)
        {
            return IsLittleEndian ? value : SwapBytes(value);
        }
        [CLSCompliant(false)]
        public static ulong SwapLittleEndian(ulong value)
        {
            return IsLittleEndian ? value : SwapBytes(value);
        }
        public static float SwapLittleEndian(float value)
        {
            return IsLittleEndian ? value : SwapBytes(value);
        }
        public static double SwapLittleEndian(double value)
        {
            return IsLittleEndian ? value : SwapBytes(value);
        }
        public static decimal SwapLittleEndian(decimal value)
        {
            return IsLittleEndian ? value : SwapBytes(value);
        }
        public static void SwapLittleEndian(byte[] value)
        {
            Contract.Requires(value != null);
            if (IsLittleEndian)
                return;

            for (int i = 0; i < value.Length / 2; ++i)
            {
                byte temp = value[i];
                value[i] = value[value.Length - (i + 1)];
                value[value.Length - (i + 1)] = temp;
            }
        }

        public static byte SwapBigEndian(byte value)
        {
            return value;
        }
        public static short SwapBigEndian(short value)
        {
            return !IsLittleEndian ? value : SwapBytes(value);
        }
        public static int SwapBigEndian(int value)
        {
            return !IsLittleEndian ? value : SwapBytes(value);
        }
        public static long SwapBigEndian(long value)
        {
            return !IsLittleEndian ? value : SwapBytes(value);
        }
        [CLSCompliant(false)]
        public static sbyte SwapBigEndian(sbyte value)
        {
            return value;
        }
        [CLSCompliant(false)]
        public static ushort SwapBigEndian(ushort value)
        {
            return !IsLittleEndian ? value : SwapBytes(value);
        }
        [CLSCompliant(false)]
        public static uint SwapBigEndian(uint value)
        {
            return !IsLittleEndian ? value : SwapBytes(value);
        }
        [CLSCompliant(false)]
        public static ulong SwapBigEndian(ulong value)
        {
            return !IsLittleEndian ? value : SwapBytes(value);
        }
        public static float SwapBigEndian(float value)
        {
            return !IsLittleEndian ? value : SwapBytes(value);
        }
        public static double SwapBigEndian(double value)
        {
            return !IsLittleEndian ? value : SwapBytes(value);
        }
        public static decimal SwapBigEndian(decimal value)
        {
            return !IsLittleEndian ? value : SwapBytes(value);
        }
        public static void SwapBigEndian(byte[] value)
        {
            Contract.Requires(value != null);
            if (!IsLittleEndian)
                return;

            for (int i = 0; i < value.Length / 2; ++i)
            {
                byte temp = value[i];
                value[i] = value[value.Length - (i + 1)];
                value[value.Length - (i + 1)] = temp;
            }
        }
        #endregion
    }
}
