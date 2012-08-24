using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Numerics
{
    public static class Binary
    {
        #region Rotate
        /// <summary>
        /// Rotates a System.Byte value a specified number of bits to the right.
        /// </summary>
        /// <param name="value">The value whose bits are to be rotated.</param>
        /// <param name="shift">The number of bits to rotate value to the right.</param>
        /// <returns>A value that has been rotated to the right by the specified number of bits.</returns>
        [Pure]
        public static byte RotateRight(byte value, int shift)
        {
            return (byte)(value >> shift | value << (8 - shift));
        }
        /// <summary>
        /// Rotates a System.Byte value a specified number of bits to the left.
        /// </summary>
        /// <param name="value">The value whose bits are to be rotated.</param>
        /// <param name="shift">The number of bits to rotate value to the left.</param>
        /// <returns>A value that has been rotated to the left by the specified number of bits.</returns>
        [Pure]
        public static byte Rotateleft(byte value, int shift)
        {
            return (byte)(value << shift | value >> (8 - shift));
        }
        /// <summary>
        /// Rotates a System.UInt16 value a specified number of bits to the right.
        /// </summary>
        /// <param name="value">The value whose bits are to be rotated.</param>
        /// <param name="shift">The number of bits to rotate value to the right.</param>
        /// <returns>A value that has been rotated to the right by the specified number of bits.</returns>
        [Pure]
        [CLSCompliant(false)]
        public static int RotateRight(ushort value, int shift)
        {
            return (ushort)(value >> shift | value << (16 - shift));
        }
        /// <summary>
        /// Rotates a System.UInt16 value a specified number of bits to the left.
        /// </summary>
        /// <param name="value">The value whose bits are to be rotated.</param>
        /// <param name="shift">The number of bits to rotate value to the left.</param>
        /// <returns>A value that has been rotated to the left by the specified number of bits.</returns>
        [Pure]
        [CLSCompliant(false)]
        public static int Rotateleft(ushort value, int shift)
        {
            return (short)(value << shift | value >> (16 - shift));
        }
        /// <summary>
        /// Rotates a System.UInt32 value a specified number of bits to the right.
        /// </summary>
        /// <param name="value">The value whose bits are to be rotated.</param>
        /// <param name="shift">The number of bits to rotate value to the right.</param>
        /// <returns>A value that has been rotated to the right by the specified number of bits.</returns>
        [Pure]
        [CLSCompliant(false)]
        public static uint RotateRight(uint value, int shift)
        {
            return value >> shift | value << (32 - shift);
        }
        /// <summary>
        /// Rotates a System.UInt32 value a specified number of bits to the left.
        /// </summary>
        /// <param name="value">The value whose bits are to be rotated.</param>
        /// <param name="shift">The number of bits to rotate value to the left.</param>
        /// <returns>A value that has been rotated to the left by the specified number of bits.</returns>
        [Pure]
        [CLSCompliant(false)]
        public static uint Rotateleft(uint value, int shift)
        {
            return value << shift | value >> (32 - shift);
        }
        /// <summary>
        /// Rotates a System.UInt64 value a specified number of bits to the right.
        /// </summary>
        /// <param name="value">The value whose bits are to be rotated.</param>
        /// <param name="shift">The number of bits to rotate value to the right.</param>
        /// <returns>A value that has been rotated to the right by the specified number of bits.</returns>
        [Pure]
        [CLSCompliant(false)]
        public static ulong RotateRight(ulong value, int shift)
        {
            return value >> shift | value << (64 - shift);
        }
        /// <summary>
        /// Rotates a System.UInt64 value a specified number of bits to the left.
        /// </summary>
        /// <param name="value">The value whose bits are to be rotated.</param>
        /// <param name="shift">The number of bits to rotate value to the left.</param>
        /// <returns>A value that has been rotated to the left by the specified number of bits.</returns>
        [Pure]
        [CLSCompliant(false)]
        public static ulong Rotateleft(ulong value, int shift)
        {
            return value << shift | value >> (64 - shift);
        }
        #endregion        

        /// <summary>
        /// Returns the floor of log base 2 of the the specified number.
        /// </summary>
        /// <param name="value">A number to find the floor log base 2.</param>
        /// <returns>Floor of log base 2 of value.</returns>
        [Pure]
        public static int FloorLog2(int value)
        {
            Contract.Requires(value >= 0, "value must be greater than or equal to zero.");
            Contract.Ensures(value > 0 && Contract.Result<int>() >= 0, "Result must be greater than or equal to zero.");
            Contract.Ensures(value == 0 && Contract.Result<int>() == -1, "Result must be equal to -1.");

            uint x = (uint)value;
            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);
            return PopulationCount((int)x) - 1;
        }

        /// <summary>
        /// Returns the ceiling of log base 2 of the the specified number.
        /// </summary>
        /// <param name="value">A number to find the ceiling log base 2.</param>
        /// <returns>Ceiling of log base 2 of value.</returns>
        [Pure]
        public static int CeilingLog2(int value)
        {
            int y = (value & (value - 1));
            y |= -y;
            y >>= 31;
            uint x = (uint)value;
            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);
            return PopulationCount((int)x) - 1 - y;
        }

        [Pure]
        public static int PopulationCount(int n)
        {
            Contract.Ensures(0 <= Contract.Result<int>() && Contract.Result<int>() <= 32, "Result is between 0 and 32 inclusive.");

            uint x = (uint)n;
            x -= ((x >> 1) & 0x55555555);
            x = (((x >> 2) & 0x33333333) + (x & 0x33333333));
            x = (((x >> 4) + x) & 0x0f0f0f0f);
            return (int)((x * 0x01010101) >> 24);
        }

        [Pure]
        public static byte BitReverse(byte n)
        {
            uint x = (uint)n;
            x = (((x & 0xaa) >> 1) | ((x & 0x55) << 1));
            x = (((x & 0xcc) >> 2) | ((x & 0x33) << 2));
            return (byte)((x >> 4) | (x << 4));
        }

        [Pure]
        public static short BitReverse(short n)
        {
            uint x = (uint)n;
            x = (((x & 0xaaaa) >> 1) | ((x & 0x5555) << 1));
            x = (((x & 0xcccc) >> 2) | ((x & 0x3333) << 2));
            x = (((x & 0xf0f0) >> 4) | ((x & 0x0f0f) << 4));
            return (short)((x >> 8) | (x << 8));
        }

        [Pure]
        public static int BitReverse(int n)
        {
            uint x = (uint)n;
	        x = (((x & 0xaaaaaaaa) >> 1) | ((x & 0x55555555) << 1));
	        x = (((x & 0xcccccccc) >> 2) | ((x & 0x33333333) << 2));
	        x = (((x & 0xf0f0f0f0) >> 4) | ((x & 0x0f0f0f0f) << 4));
	        x = (((x & 0xff00ff00) >> 8) | ((x & 0x00ff00ff) << 8));
	        return (int)((x >> 16) | (x << 16));
        }

        [Pure]
        public static long BitReverse(long n)
        {
            ulong x = (ulong)n;
            x = (((x & 0xaaaaaaaaaaaaaaaa) >> 1) | ((x & 0x5555555555555555) << 1));
            x = (((x & 0xcccccccccccccccc) >> 2) | ((x & 0x3333333333333333) << 2));
            x = (((x & 0xf0f0f0f0f0f0f0f0) >> 4) | ((x & 0x0f0f0f0f0f0f0f0f) << 4));
            x = (((x & 0xff00ff00ff00ff00) >> 8) | ((x & 0x00ff00ff00ff00ff) << 8));
            x = (((x & 0xffff0000ffff0000) >> 16) | ((x & 0x0000ffff0000ffff) << 16));
            return (long)((x >> 32) | (x << 32));
        }

        [Pure]
        public static int ComparisonMask(int n)
        {
            return (n | -n) >> 31;
        }
        [Pure]
        public static int ComparisonBit(int n)
        {
            uint x = (uint)n;
            return (int)((x | -x) >> 31);
        }

        [Pure]
        public static int BinaryToGray(int binary)
        {
            uint x = (uint)binary;
            return (int)(x ^ (x >> 1));
        }
        [Pure]
        public static int GrayToBinary(int gray)
        {
            uint x = (uint)gray;
            x ^= (x >> 16);
            x ^= (x >> 8);
            x ^= (x >> 4);
            x ^= (x >> 2);
            x ^= (x >> 1);
            return (int)x;
        }

        [Pure]
        public static int SelectLt(int a, int b, int c, int d)
        {
            return ((((a - b) >> 31) & (c ^ d)) ^ d);
        }
        [Pure]
        public static int SelectGt(int a, int b, int c, int d)
        {
            return ((((a - b) >> 31) & (c ^ d)) ^ d);
        }
        [Pure]
        public static int SelectEq(int a, int b, int c, int d)
        {
            return ((~(((a - b) >> 31) & ((a - b) >> 31)) & (c ^ d)) ^ d);
        }

        [Pure]
        public static int LeadingZeroCount(int n)
        {
            uint x = (uint)n;
            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);
            return (31 - PopulationCount((int)x));
        }
        [Pure]
        public static int TrailingZeroCount(int n)
        {
            return PopulationCount((n & -n) - 1);
        }

        [Pure]
        public static int LeastSignificantBitMask(int n)
        {
            return (n & -n);
        }
        [Pure]
        public static int MostSignificantBitMask(int n)
        {
            uint x = (uint)n;
            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);
            return (int)(x & ~(x >> 1)); 
        }

        [Pure]
        public static int Expand(int n, int lowPrecisionBits, int highPrecisionBits)
        {
            uint x = (uint)n;
            return (int)(
                (x << (highPrecisionBits - lowPrecisionBits)) | 
                (x >> (lowPrecisionBits - (highPrecisionBits - lowPrecisionBits))));
        }

        [Pure]
        public static int SignExtend(int n, int bits)
        {
            int h = 1 << bits;
            return ((n ^ h) - h);
        }
    }
}
