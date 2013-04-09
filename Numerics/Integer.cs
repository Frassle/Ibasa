namespace Ibasa.Numerics
{
	/// <summary>
	/// A 128 bit signed integer.
	/// </summary>
	[System.Serializable]
	[System.Runtime.InteropServices.ComVisible(true)]
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct Int128: System.IEquatable<Int128>, System.IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Int128"/> equal to zero.
		/// </summary>
		public static readonly Int128 Zero = new Int128(0);
		/// <summary>
		/// Returns a new <see cref="Int128"/> equal to one.
		/// </summary>
		public static readonly Int128 One = new Int128(1);
		#endregion
		#region Fields
		private readonly uint Part0;
		private readonly uint Part1;
		private readonly uint Part2;
		private readonly uint Part3;
		#endregion
		#region Properties
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Int128"/> using the specified bytes.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public Int128(byte[] bytes)
		{
			Part0 = (uint)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0]);
			Part1 = (uint)((bytes[4] << 24) | (bytes[3] << 16) | (bytes[2] << 8) | bytes[1]);
			Part2 = (uint)((bytes[5] << 24) | (bytes[4] << 16) | (bytes[3] << 8) | bytes[2]);
			Part3 = (uint)((bytes[6] << 24) | (bytes[5] << 16) | (bytes[4] << 8) | bytes[3]);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int128"/> using the specified 32 bit parts.
		/// </summary>
		[System.CLSCompliant(false)]
		public Int128(uint part0, uint part1, uint part2, uint part3)
		{
			Part0 = part0;
			Part1 = part1;
			Part2 = part2;
			Part3 = part3;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int128"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public Int128(uint value)
		{
			Part0 = value;
			Part1 = 0;
			Part2 = 0;
			Part3 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int128"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public Int128(int value)
		{
			Part0 = (uint)value;
			Part1 = value < 0 ? uint.MaxValue : 0;
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int128"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public Int128(ulong value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = 0;
			Part3 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int128"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public Int128(long value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The identity of value.</returns>
		public static Int128 operator +(Int128 value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The negative of value.</returns>
		public static Int128 operator -(Int128 value)
		{
			return (Int128)(~value + Int128.One);
		}
		/// <summary>
		/// Adds two integers and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Int128 operator +(Int128 left, Int128 right)
		{
			unsafe
			{
				uint* parts = stackalloc uint[4];
				ulong carry = 0;
				ulong n = 0;
				n = carry + left.Part0 + right.Part0;
				parts[0] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part1 + right.Part1;
				parts[1] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part2 + right.Part2;
				parts[2] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part3 + right.Part3;
				parts[3] = (uint)n;
				carry = n >> 32;
				return new Int128(parts[0], parts[1], parts[2], parts[3]);
			}
		}
		/// <summary>
		/// Subtracts one integer from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Int128 operator -(Int128 left, Int128 right)
		{
			return left + (Int128)(-right);
		}
		/// <summary>
		/// Returns the product of a integer and scalar.
		/// </summary>
		/// <param name="left">The first integer to multiply.</param>
		/// <param name="right">The second integer to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Int128 operator *(Int128 left, Int128 right)
		{
			throw new System.NotImplementedException("operator *");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Int128 operator /(Int128 left, Int128 right)
		{
			throw new System.NotImplementedException("operator /");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the remainder.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The modulus from dividing left by right (the remainder).</returns>
		public static Int128 operator %(Int128 left, Int128 right)
		{
			throw new System.NotImplementedException("operator %");
		}
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static Int128 operator ~(Int128 value)
		{
			return new Int128(~value.Part0, ~value.Part1, ~value.Part2, ~value.Part3);
		}
		/// <summary>
		/// Returns the bitwise AND of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise AND of left and right.</returns>
		public static Int128 operator &(Int128 left, Int128 right)
		{
			return new Int128(left.Part0 & right.Part0, left.Part1 & right.Part1, left.Part2 & right.Part2, left.Part3 & right.Part3);
		}
		/// <summary>
		/// Returns the bitwise OR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise OR of left and right.</returns>
		public static Int128 operator |(Int128 left, Int128 right)
		{
			return new Int128(left.Part0 | right.Part0, left.Part1 | right.Part1, left.Part2 | right.Part2, left.Part3 | right.Part3);
		}
		/// <summary>
		/// Returns the bitwise XOR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise XOR of left and right.</returns>
		public static Int128 operator ^(Int128 left, Int128 right)
		{
			return new Int128(left.Part0 ^ right.Part0, left.Part1 ^ right.Part1, left.Part2 ^ right.Part2, left.Part3 ^ right.Part3);
		}
		public static Int128 operator <<(Int128 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[4];
				uint* vparts = stackalloc uint[4];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 4; i++)
				{
					if (i + k + 1 < 4)
					{
						parts[i + k + 1] |= (vparts[i] >> (32 - shift));
					}
					if (i + k < 4)
					{
						parts[i + k] |= (vparts[i] << shift);
					}
				}
				return new Int128(parts[0], parts[1], parts[2], parts[3]);
			}
		}
		public static Int128 operator >>(Int128 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[4];
				uint* vparts = stackalloc uint[4];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 4; i++)
				{
					if (i - k - 1 >= 0)
					{
						parts[i - k - 1] |= (vparts[i] << (32 - shift));
					}
					if (i - k >= 0)
					{
						parts[i - k] |= (vparts[i] >> shift);
					}
				}
				uint negative = (uint)((int)vparts[3] >> 32);
				for (int i = System.Math.Max(0, 4 - k); i < 4; i++)
				{
					parts[i] = negative;
				}
				negative <<= (32 - shift);
				if(4 - k - 1 >= 0)
				{
					parts[4 - k - 1] |= negative;
				}
				return new Int128(parts[0], parts[1], parts[2], parts[3]);
			}
		}
		public byte[] ToByteArray()
		{
			var bytes = new byte[16];
			bytes[0] = (byte)(Part0 >> 0);
			bytes[1] = (byte)(Part0 >> 8);
			bytes[2] = (byte)(Part0 >> 16);
			bytes[3] = (byte)(Part0 >> 24);
			bytes[4] = (byte)(Part1 >> 0);
			bytes[5] = (byte)(Part1 >> 8);
			bytes[6] = (byte)(Part1 >> 16);
			bytes[7] = (byte)(Part1 >> 24);
			bytes[8] = (byte)(Part2 >> 0);
			bytes[9] = (byte)(Part2 >> 8);
			bytes[10] = (byte)(Part2 >> 16);
			bytes[11] = (byte)(Part2 >> 24);
			bytes[12] = (byte)(Part3 >> 0);
			bytes[13] = (byte)(Part3 >> 8);
			bytes[14] = (byte)(Part3 >> 16);
			bytes[15] = (byte)(Part3 >> 24);
			return bytes;
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of an Int128 value to an UInt128.
		/// </summary>
		/// <param name="value">The value to convert to an UInt128.</param>
		/// <returns>An UInt128 that is bitwise equal to value.</returns>
		public static explicit operator UInt128(Int128 value)
		{
			return new UInt128(value.Part0, value.Part1, value.Part2, value.Part3);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Int128"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Part0.GetHashCode() + Part1.GetHashCode() + Part2.GetHashCode() + Part3.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Int128"/> object or a type capable
		/// of implicit conversion to a <see cref="Int128"/> object, and its value
		/// is equal to the current <see cref="Int128"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Int128) { return Equals((Int128)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// integer have the same value.
		/// </summary>
		/// <param name="other">The integer to compare.</param>
		/// <returns>true if this integer and value have the same value; otherwise, false.</returns>
		public bool Equals(Int128 other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Int128 left, Int128 right)
		{
			return left.Part0 == right.Part0 & left.Part1 == right.Part1 & left.Part2 == right.Part2 & left.Part3 == right.Part3;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are not equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Int128 left, Int128 right)
		{
			return left.Part0 != right.Part0 | left.Part1 != right.Part1 | left.Part2 != right.Part2 | left.Part3 != right.Part3;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified culture-specific
		/// formatting information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by provider.</returns>
		public string ToString(System.IFormatProvider provider)
		{
			return ToString("G", provider);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format for its components.
		/// formatting information.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format)
		{
			return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, System.IFormatProvider provider)
		{
			var bytes = ToByteArray();
			return new System.Numerics.BigInteger(bytes).ToString(format, provider);
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for integer functions.
	/// </summary>
	public static partial class Integer
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Int128"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Int128 integer)
		{
			writer.Write(integer.ToByteArray());
		}
		/// <summary>
		/// Reads a <see cref="Int128"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Int128 ReadInt128(this Ibasa.IO.BinaryReader reader)
		{
			return new Int128(reader.ReadBytes(16));
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static Int128 BitwiseNot(Int128 value)
		{
			return ~value;
		}
		/// <summary>
		/// Returns the bitwise and of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise and of left and right.</returns>
		public static Int128 BitwiseAnd(Int128 left, Int128 right)
		{
			return left & right;
		}
		/// <summary>
		/// Returns the bitwise or of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise or of left and right.</returns>
		public static Int128 BitwiseOr(Int128 left, Int128 right)
		{
			return left | right;
		}
		/// <summary>
		/// Returns the bitwise xor of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise xor of left and right.</returns>
		public static Int128 BitwiseXor(Int128 left, Int128 right)
		{
			return left ^ right;
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Int128 left, Int128 right)
		{
			return left == right;
		}
		#endregion
	}
}
namespace Ibasa.Numerics
{
	/// <summary>
	/// A 128 bit unsigned integer.
	/// </summary>
	[System.Serializable]
	[System.Runtime.InteropServices.ComVisible(true)]
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct UInt128: System.IEquatable<UInt128>, System.IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="UInt128"/> equal to zero.
		/// </summary>
		public static readonly UInt128 Zero = new UInt128(0);
		/// <summary>
		/// Returns a new <see cref="UInt128"/> equal to one.
		/// </summary>
		public static readonly UInt128 One = new UInt128(1);
		#endregion
		#region Fields
		private readonly uint Part0;
		private readonly uint Part1;
		private readonly uint Part2;
		private readonly uint Part3;
		#endregion
		#region Properties
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt128"/> using the specified bytes.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public UInt128(byte[] bytes)
		{
			Part0 = (uint)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0]);
			Part1 = (uint)((bytes[4] << 24) | (bytes[3] << 16) | (bytes[2] << 8) | bytes[1]);
			Part2 = (uint)((bytes[5] << 24) | (bytes[4] << 16) | (bytes[3] << 8) | bytes[2]);
			Part3 = (uint)((bytes[6] << 24) | (bytes[5] << 16) | (bytes[4] << 8) | bytes[3]);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt128"/> using the specified 32 bit parts.
		/// </summary>
		[System.CLSCompliant(false)]
		public UInt128(uint part0, uint part1, uint part2, uint part3)
		{
			Part0 = part0;
			Part1 = part1;
			Part2 = part2;
			Part3 = part3;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt128"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public UInt128(uint value)
		{
			Part0 = value;
			Part1 = 0;
			Part2 = 0;
			Part3 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt128"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public UInt128(int value)
		{
			Part0 = (uint)value;
			Part1 = value < 0 ? uint.MaxValue : 0;
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt128"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public UInt128(ulong value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = 0;
			Part3 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt128"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public UInt128(long value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The identity of value.</returns>
		public static UInt128 operator +(UInt128 value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The negative of value.</returns>
		public static Int128 operator -(UInt128 value)
		{
			return (Int128)(~value + UInt128.One);
		}
		/// <summary>
		/// Adds two integers and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static UInt128 operator +(UInt128 left, UInt128 right)
		{
			unsafe
			{
				uint* parts = stackalloc uint[4];
				ulong carry = 0;
				ulong n = 0;
				n = carry + left.Part0 + right.Part0;
				parts[0] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part1 + right.Part1;
				parts[1] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part2 + right.Part2;
				parts[2] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part3 + right.Part3;
				parts[3] = (uint)n;
				carry = n >> 32;
				return new UInt128(parts[0], parts[1], parts[2], parts[3]);
			}
		}
		/// <summary>
		/// Subtracts one integer from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static UInt128 operator -(UInt128 left, UInt128 right)
		{
			return left + (UInt128)(-right);
		}
		/// <summary>
		/// Returns the product of a integer and scalar.
		/// </summary>
		/// <param name="left">The first integer to multiply.</param>
		/// <param name="right">The second integer to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static UInt128 operator *(UInt128 left, UInt128 right)
		{
			throw new System.NotImplementedException("operator *");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static UInt128 operator /(UInt128 left, UInt128 right)
		{
			throw new System.NotImplementedException("operator /");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the remainder.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The modulus from dividing left by right (the remainder).</returns>
		public static UInt128 operator %(UInt128 left, UInt128 right)
		{
			throw new System.NotImplementedException("operator %");
		}
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static UInt128 operator ~(UInt128 value)
		{
			return new UInt128(~value.Part0, ~value.Part1, ~value.Part2, ~value.Part3);
		}
		/// <summary>
		/// Returns the bitwise AND of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise AND of left and right.</returns>
		public static UInt128 operator &(UInt128 left, UInt128 right)
		{
			return new UInt128(left.Part0 & right.Part0, left.Part1 & right.Part1, left.Part2 & right.Part2, left.Part3 & right.Part3);
		}
		/// <summary>
		/// Returns the bitwise OR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise OR of left and right.</returns>
		public static UInt128 operator |(UInt128 left, UInt128 right)
		{
			return new UInt128(left.Part0 | right.Part0, left.Part1 | right.Part1, left.Part2 | right.Part2, left.Part3 | right.Part3);
		}
		/// <summary>
		/// Returns the bitwise XOR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise XOR of left and right.</returns>
		public static UInt128 operator ^(UInt128 left, UInt128 right)
		{
			return new UInt128(left.Part0 ^ right.Part0, left.Part1 ^ right.Part1, left.Part2 ^ right.Part2, left.Part3 ^ right.Part3);
		}
		public static UInt128 operator <<(UInt128 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[4];
				uint* vparts = stackalloc uint[4];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 4; i++)
				{
					if (i + k + 1 < 4)
					{
						parts[i + k + 1] |= (vparts[i] >> (32 - shift));
					}
					if (i + k < 4)
					{
						parts[i + k] |= (vparts[i] << shift);
					}
				}
				return new UInt128(parts[0], parts[1], parts[2], parts[3]);
			}
		}
		public static UInt128 operator >>(UInt128 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[4];
				uint* vparts = stackalloc uint[4];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 4; i++)
				{
					if (i - k - 1 >= 0)
					{
						parts[i - k - 1] |= (vparts[i] << (32 - shift));
					}
					if (i - k >= 0)
					{
						parts[i - k] |= (vparts[i] >> shift);
					}
				}
				return new UInt128(parts[0], parts[1], parts[2], parts[3]);
			}
		}
		public byte[] ToByteArray()
		{
			var bytes = new byte[16];
			bytes[0] = (byte)(Part0 >> 0);
			bytes[1] = (byte)(Part0 >> 8);
			bytes[2] = (byte)(Part0 >> 16);
			bytes[3] = (byte)(Part0 >> 24);
			bytes[4] = (byte)(Part1 >> 0);
			bytes[5] = (byte)(Part1 >> 8);
			bytes[6] = (byte)(Part1 >> 16);
			bytes[7] = (byte)(Part1 >> 24);
			bytes[8] = (byte)(Part2 >> 0);
			bytes[9] = (byte)(Part2 >> 8);
			bytes[10] = (byte)(Part2 >> 16);
			bytes[11] = (byte)(Part2 >> 24);
			bytes[12] = (byte)(Part3 >> 0);
			bytes[13] = (byte)(Part3 >> 8);
			bytes[14] = (byte)(Part3 >> 16);
			bytes[15] = (byte)(Part3 >> 24);
			return bytes;
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of an UInt128 value to an Int128.
		/// </summary>
		/// <param name="value">The value to convert to an Int128.</param>
		/// <returns>An Int128 that is bitwise equal to value.</returns>
		public static explicit operator Int128(UInt128 value)
		{
			return new Int128(value.Part0, value.Part1, value.Part2, value.Part3);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="UInt128"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Part0.GetHashCode() + Part1.GetHashCode() + Part2.GetHashCode() + Part3.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="UInt128"/> object or a type capable
		/// of implicit conversion to a <see cref="UInt128"/> object, and its value
		/// is equal to the current <see cref="UInt128"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is UInt128) { return Equals((UInt128)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// integer have the same value.
		/// </summary>
		/// <param name="other">The integer to compare.</param>
		/// <returns>true if this integer and value have the same value; otherwise, false.</returns>
		public bool Equals(UInt128 other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(UInt128 left, UInt128 right)
		{
			return left.Part0 == right.Part0 & left.Part1 == right.Part1 & left.Part2 == right.Part2 & left.Part3 == right.Part3;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are not equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(UInt128 left, UInt128 right)
		{
			return left.Part0 != right.Part0 | left.Part1 != right.Part1 | left.Part2 != right.Part2 | left.Part3 != right.Part3;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified culture-specific
		/// formatting information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by provider.</returns>
		public string ToString(System.IFormatProvider provider)
		{
			return ToString("G", provider);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format for its components.
		/// formatting information.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format)
		{
			return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, System.IFormatProvider provider)
		{
			var bytes = ToByteArray();
			bytes = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(bytes, new byte[] { 0 }));
			return new System.Numerics.BigInteger(bytes).ToString(format, provider);
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for integer functions.
	/// </summary>
	public static partial class Integer
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="UInt128"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, UInt128 integer)
		{
			writer.Write(integer.ToByteArray());
		}
		/// <summary>
		/// Reads a <see cref="UInt128"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static UInt128 ReadUInt128(this Ibasa.IO.BinaryReader reader)
		{
			return new UInt128(reader.ReadBytes(16));
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static UInt128 BitwiseNot(UInt128 value)
		{
			return ~value;
		}
		/// <summary>
		/// Returns the bitwise and of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise and of left and right.</returns>
		public static UInt128 BitwiseAnd(UInt128 left, UInt128 right)
		{
			return left & right;
		}
		/// <summary>
		/// Returns the bitwise or of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise or of left and right.</returns>
		public static UInt128 BitwiseOr(UInt128 left, UInt128 right)
		{
			return left | right;
		}
		/// <summary>
		/// Returns the bitwise xor of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise xor of left and right.</returns>
		public static UInt128 BitwiseXor(UInt128 left, UInt128 right)
		{
			return left ^ right;
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(UInt128 left, UInt128 right)
		{
			return left == right;
		}
		#endregion
	}
}
namespace Ibasa.Numerics
{
	/// <summary>
	/// A 160 bit signed integer.
	/// </summary>
	[System.Serializable]
	[System.Runtime.InteropServices.ComVisible(true)]
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct Int160: System.IEquatable<Int160>, System.IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Int160"/> equal to zero.
		/// </summary>
		public static readonly Int160 Zero = new Int160(0);
		/// <summary>
		/// Returns a new <see cref="Int160"/> equal to one.
		/// </summary>
		public static readonly Int160 One = new Int160(1);
		#endregion
		#region Fields
		private readonly uint Part0;
		private readonly uint Part1;
		private readonly uint Part2;
		private readonly uint Part3;
		private readonly uint Part4;
		#endregion
		#region Properties
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Int160"/> using the specified bytes.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public Int160(byte[] bytes)
		{
			Part0 = (uint)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0]);
			Part1 = (uint)((bytes[4] << 24) | (bytes[3] << 16) | (bytes[2] << 8) | bytes[1]);
			Part2 = (uint)((bytes[5] << 24) | (bytes[4] << 16) | (bytes[3] << 8) | bytes[2]);
			Part3 = (uint)((bytes[6] << 24) | (bytes[5] << 16) | (bytes[4] << 8) | bytes[3]);
			Part4 = (uint)((bytes[7] << 24) | (bytes[6] << 16) | (bytes[5] << 8) | bytes[4]);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int160"/> using the specified 32 bit parts.
		/// </summary>
		[System.CLSCompliant(false)]
		public Int160(uint part0, uint part1, uint part2, uint part3, uint part4)
		{
			Part0 = part0;
			Part1 = part1;
			Part2 = part2;
			Part3 = part3;
			Part4 = part4;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int160"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public Int160(uint value)
		{
			Part0 = value;
			Part1 = 0;
			Part2 = 0;
			Part3 = 0;
			Part4 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int160"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public Int160(int value)
		{
			Part0 = (uint)value;
			Part1 = value < 0 ? uint.MaxValue : 0;
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
			Part4 = value < 0 ? uint.MaxValue : 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int160"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public Int160(ulong value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = 0;
			Part3 = 0;
			Part4 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int160"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public Int160(long value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
			Part4 = value < 0 ? uint.MaxValue : 0;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The identity of value.</returns>
		public static Int160 operator +(Int160 value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The negative of value.</returns>
		public static Int160 operator -(Int160 value)
		{
			return (Int160)(~value + Int160.One);
		}
		/// <summary>
		/// Adds two integers and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Int160 operator +(Int160 left, Int160 right)
		{
			unsafe
			{
				uint* parts = stackalloc uint[5];
				ulong carry = 0;
				ulong n = 0;
				n = carry + left.Part0 + right.Part0;
				parts[0] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part1 + right.Part1;
				parts[1] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part2 + right.Part2;
				parts[2] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part3 + right.Part3;
				parts[3] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part4 + right.Part4;
				parts[4] = (uint)n;
				carry = n >> 32;
				return new Int160(parts[0], parts[1], parts[2], parts[3], parts[4]);
			}
		}
		/// <summary>
		/// Subtracts one integer from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Int160 operator -(Int160 left, Int160 right)
		{
			return left + (Int160)(-right);
		}
		/// <summary>
		/// Returns the product of a integer and scalar.
		/// </summary>
		/// <param name="left">The first integer to multiply.</param>
		/// <param name="right">The second integer to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Int160 operator *(Int160 left, Int160 right)
		{
			throw new System.NotImplementedException("operator *");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Int160 operator /(Int160 left, Int160 right)
		{
			throw new System.NotImplementedException("operator /");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the remainder.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The modulus from dividing left by right (the remainder).</returns>
		public static Int160 operator %(Int160 left, Int160 right)
		{
			throw new System.NotImplementedException("operator %");
		}
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static Int160 operator ~(Int160 value)
		{
			return new Int160(~value.Part0, ~value.Part1, ~value.Part2, ~value.Part3, ~value.Part4);
		}
		/// <summary>
		/// Returns the bitwise AND of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise AND of left and right.</returns>
		public static Int160 operator &(Int160 left, Int160 right)
		{
			return new Int160(left.Part0 & right.Part0, left.Part1 & right.Part1, left.Part2 & right.Part2, left.Part3 & right.Part3, left.Part4 & right.Part4);
		}
		/// <summary>
		/// Returns the bitwise OR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise OR of left and right.</returns>
		public static Int160 operator |(Int160 left, Int160 right)
		{
			return new Int160(left.Part0 | right.Part0, left.Part1 | right.Part1, left.Part2 | right.Part2, left.Part3 | right.Part3, left.Part4 | right.Part4);
		}
		/// <summary>
		/// Returns the bitwise XOR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise XOR of left and right.</returns>
		public static Int160 operator ^(Int160 left, Int160 right)
		{
			return new Int160(left.Part0 ^ right.Part0, left.Part1 ^ right.Part1, left.Part2 ^ right.Part2, left.Part3 ^ right.Part3, left.Part4 ^ right.Part4);
		}
		public static Int160 operator <<(Int160 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[5];
				uint* vparts = stackalloc uint[5];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				vparts[4] = value.Part4;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 5; i++)
				{
					if (i + k + 1 < 5)
					{
						parts[i + k + 1] |= (vparts[i] >> (32 - shift));
					}
					if (i + k < 5)
					{
						parts[i + k] |= (vparts[i] << shift);
					}
				}
				return new Int160(parts[0], parts[1], parts[2], parts[3], parts[4]);
			}
		}
		public static Int160 operator >>(Int160 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[5];
				uint* vparts = stackalloc uint[5];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				vparts[4] = value.Part4;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 5; i++)
				{
					if (i - k - 1 >= 0)
					{
						parts[i - k - 1] |= (vparts[i] << (32 - shift));
					}
					if (i - k >= 0)
					{
						parts[i - k] |= (vparts[i] >> shift);
					}
				}
				uint negative = (uint)((int)vparts[4] >> 32);
				for (int i = System.Math.Max(0, 5 - k); i < 5; i++)
				{
					parts[i] = negative;
				}
				negative <<= (32 - shift);
				if(5 - k - 1 >= 0)
				{
					parts[5 - k - 1] |= negative;
				}
				return new Int160(parts[0], parts[1], parts[2], parts[3], parts[4]);
			}
		}
		public byte[] ToByteArray()
		{
			var bytes = new byte[20];
			bytes[0] = (byte)(Part0 >> 0);
			bytes[1] = (byte)(Part0 >> 8);
			bytes[2] = (byte)(Part0 >> 16);
			bytes[3] = (byte)(Part0 >> 24);
			bytes[4] = (byte)(Part1 >> 0);
			bytes[5] = (byte)(Part1 >> 8);
			bytes[6] = (byte)(Part1 >> 16);
			bytes[7] = (byte)(Part1 >> 24);
			bytes[8] = (byte)(Part2 >> 0);
			bytes[9] = (byte)(Part2 >> 8);
			bytes[10] = (byte)(Part2 >> 16);
			bytes[11] = (byte)(Part2 >> 24);
			bytes[12] = (byte)(Part3 >> 0);
			bytes[13] = (byte)(Part3 >> 8);
			bytes[14] = (byte)(Part3 >> 16);
			bytes[15] = (byte)(Part3 >> 24);
			bytes[16] = (byte)(Part4 >> 0);
			bytes[17] = (byte)(Part4 >> 8);
			bytes[18] = (byte)(Part4 >> 16);
			bytes[19] = (byte)(Part4 >> 24);
			return bytes;
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of an Int160 value to an UInt160.
		/// </summary>
		/// <param name="value">The value to convert to an UInt160.</param>
		/// <returns>An UInt160 that is bitwise equal to value.</returns>
		public static explicit operator UInt160(Int160 value)
		{
			return new UInt160(value.Part0, value.Part1, value.Part2, value.Part3, value.Part4);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Int160"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Part0.GetHashCode() + Part1.GetHashCode() + Part2.GetHashCode() + Part3.GetHashCode() + Part4.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Int160"/> object or a type capable
		/// of implicit conversion to a <see cref="Int160"/> object, and its value
		/// is equal to the current <see cref="Int160"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Int160) { return Equals((Int160)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// integer have the same value.
		/// </summary>
		/// <param name="other">The integer to compare.</param>
		/// <returns>true if this integer and value have the same value; otherwise, false.</returns>
		public bool Equals(Int160 other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Int160 left, Int160 right)
		{
			return left.Part0 == right.Part0 & left.Part1 == right.Part1 & left.Part2 == right.Part2 & left.Part3 == right.Part3 & left.Part4 == right.Part4;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are not equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Int160 left, Int160 right)
		{
			return left.Part0 != right.Part0 | left.Part1 != right.Part1 | left.Part2 != right.Part2 | left.Part3 != right.Part3 | left.Part4 != right.Part4;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified culture-specific
		/// formatting information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by provider.</returns>
		public string ToString(System.IFormatProvider provider)
		{
			return ToString("G", provider);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format for its components.
		/// formatting information.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format)
		{
			return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, System.IFormatProvider provider)
		{
			var bytes = ToByteArray();
			return new System.Numerics.BigInteger(bytes).ToString(format, provider);
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for integer functions.
	/// </summary>
	public static partial class Integer
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Int160"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Int160 integer)
		{
			writer.Write(integer.ToByteArray());
		}
		/// <summary>
		/// Reads a <see cref="Int160"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Int160 ReadInt160(this Ibasa.IO.BinaryReader reader)
		{
			return new Int160(reader.ReadBytes(20));
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static Int160 BitwiseNot(Int160 value)
		{
			return ~value;
		}
		/// <summary>
		/// Returns the bitwise and of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise and of left and right.</returns>
		public static Int160 BitwiseAnd(Int160 left, Int160 right)
		{
			return left & right;
		}
		/// <summary>
		/// Returns the bitwise or of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise or of left and right.</returns>
		public static Int160 BitwiseOr(Int160 left, Int160 right)
		{
			return left | right;
		}
		/// <summary>
		/// Returns the bitwise xor of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise xor of left and right.</returns>
		public static Int160 BitwiseXor(Int160 left, Int160 right)
		{
			return left ^ right;
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Int160 left, Int160 right)
		{
			return left == right;
		}
		#endregion
	}
}
namespace Ibasa.Numerics
{
	/// <summary>
	/// A 160 bit unsigned integer.
	/// </summary>
	[System.Serializable]
	[System.Runtime.InteropServices.ComVisible(true)]
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct UInt160: System.IEquatable<UInt160>, System.IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="UInt160"/> equal to zero.
		/// </summary>
		public static readonly UInt160 Zero = new UInt160(0);
		/// <summary>
		/// Returns a new <see cref="UInt160"/> equal to one.
		/// </summary>
		public static readonly UInt160 One = new UInt160(1);
		#endregion
		#region Fields
		private readonly uint Part0;
		private readonly uint Part1;
		private readonly uint Part2;
		private readonly uint Part3;
		private readonly uint Part4;
		#endregion
		#region Properties
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt160"/> using the specified bytes.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public UInt160(byte[] bytes)
		{
			Part0 = (uint)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0]);
			Part1 = (uint)((bytes[4] << 24) | (bytes[3] << 16) | (bytes[2] << 8) | bytes[1]);
			Part2 = (uint)((bytes[5] << 24) | (bytes[4] << 16) | (bytes[3] << 8) | bytes[2]);
			Part3 = (uint)((bytes[6] << 24) | (bytes[5] << 16) | (bytes[4] << 8) | bytes[3]);
			Part4 = (uint)((bytes[7] << 24) | (bytes[6] << 16) | (bytes[5] << 8) | bytes[4]);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt160"/> using the specified 32 bit parts.
		/// </summary>
		[System.CLSCompliant(false)]
		public UInt160(uint part0, uint part1, uint part2, uint part3, uint part4)
		{
			Part0 = part0;
			Part1 = part1;
			Part2 = part2;
			Part3 = part3;
			Part4 = part4;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt160"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public UInt160(uint value)
		{
			Part0 = value;
			Part1 = 0;
			Part2 = 0;
			Part3 = 0;
			Part4 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt160"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public UInt160(int value)
		{
			Part0 = (uint)value;
			Part1 = value < 0 ? uint.MaxValue : 0;
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
			Part4 = value < 0 ? uint.MaxValue : 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt160"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public UInt160(ulong value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = 0;
			Part3 = 0;
			Part4 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt160"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public UInt160(long value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
			Part4 = value < 0 ? uint.MaxValue : 0;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The identity of value.</returns>
		public static UInt160 operator +(UInt160 value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The negative of value.</returns>
		public static Int160 operator -(UInt160 value)
		{
			return (Int160)(~value + UInt160.One);
		}
		/// <summary>
		/// Adds two integers and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static UInt160 operator +(UInt160 left, UInt160 right)
		{
			unsafe
			{
				uint* parts = stackalloc uint[5];
				ulong carry = 0;
				ulong n = 0;
				n = carry + left.Part0 + right.Part0;
				parts[0] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part1 + right.Part1;
				parts[1] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part2 + right.Part2;
				parts[2] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part3 + right.Part3;
				parts[3] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part4 + right.Part4;
				parts[4] = (uint)n;
				carry = n >> 32;
				return new UInt160(parts[0], parts[1], parts[2], parts[3], parts[4]);
			}
		}
		/// <summary>
		/// Subtracts one integer from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static UInt160 operator -(UInt160 left, UInt160 right)
		{
			return left + (UInt160)(-right);
		}
		/// <summary>
		/// Returns the product of a integer and scalar.
		/// </summary>
		/// <param name="left">The first integer to multiply.</param>
		/// <param name="right">The second integer to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static UInt160 operator *(UInt160 left, UInt160 right)
		{
			throw new System.NotImplementedException("operator *");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static UInt160 operator /(UInt160 left, UInt160 right)
		{
			throw new System.NotImplementedException("operator /");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the remainder.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The modulus from dividing left by right (the remainder).</returns>
		public static UInt160 operator %(UInt160 left, UInt160 right)
		{
			throw new System.NotImplementedException("operator %");
		}
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static UInt160 operator ~(UInt160 value)
		{
			return new UInt160(~value.Part0, ~value.Part1, ~value.Part2, ~value.Part3, ~value.Part4);
		}
		/// <summary>
		/// Returns the bitwise AND of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise AND of left and right.</returns>
		public static UInt160 operator &(UInt160 left, UInt160 right)
		{
			return new UInt160(left.Part0 & right.Part0, left.Part1 & right.Part1, left.Part2 & right.Part2, left.Part3 & right.Part3, left.Part4 & right.Part4);
		}
		/// <summary>
		/// Returns the bitwise OR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise OR of left and right.</returns>
		public static UInt160 operator |(UInt160 left, UInt160 right)
		{
			return new UInt160(left.Part0 | right.Part0, left.Part1 | right.Part1, left.Part2 | right.Part2, left.Part3 | right.Part3, left.Part4 | right.Part4);
		}
		/// <summary>
		/// Returns the bitwise XOR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise XOR of left and right.</returns>
		public static UInt160 operator ^(UInt160 left, UInt160 right)
		{
			return new UInt160(left.Part0 ^ right.Part0, left.Part1 ^ right.Part1, left.Part2 ^ right.Part2, left.Part3 ^ right.Part3, left.Part4 ^ right.Part4);
		}
		public static UInt160 operator <<(UInt160 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[5];
				uint* vparts = stackalloc uint[5];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				vparts[4] = value.Part4;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 5; i++)
				{
					if (i + k + 1 < 5)
					{
						parts[i + k + 1] |= (vparts[i] >> (32 - shift));
					}
					if (i + k < 5)
					{
						parts[i + k] |= (vparts[i] << shift);
					}
				}
				return new UInt160(parts[0], parts[1], parts[2], parts[3], parts[4]);
			}
		}
		public static UInt160 operator >>(UInt160 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[5];
				uint* vparts = stackalloc uint[5];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				vparts[4] = value.Part4;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 5; i++)
				{
					if (i - k - 1 >= 0)
					{
						parts[i - k - 1] |= (vparts[i] << (32 - shift));
					}
					if (i - k >= 0)
					{
						parts[i - k] |= (vparts[i] >> shift);
					}
				}
				return new UInt160(parts[0], parts[1], parts[2], parts[3], parts[4]);
			}
		}
		public byte[] ToByteArray()
		{
			var bytes = new byte[20];
			bytes[0] = (byte)(Part0 >> 0);
			bytes[1] = (byte)(Part0 >> 8);
			bytes[2] = (byte)(Part0 >> 16);
			bytes[3] = (byte)(Part0 >> 24);
			bytes[4] = (byte)(Part1 >> 0);
			bytes[5] = (byte)(Part1 >> 8);
			bytes[6] = (byte)(Part1 >> 16);
			bytes[7] = (byte)(Part1 >> 24);
			bytes[8] = (byte)(Part2 >> 0);
			bytes[9] = (byte)(Part2 >> 8);
			bytes[10] = (byte)(Part2 >> 16);
			bytes[11] = (byte)(Part2 >> 24);
			bytes[12] = (byte)(Part3 >> 0);
			bytes[13] = (byte)(Part3 >> 8);
			bytes[14] = (byte)(Part3 >> 16);
			bytes[15] = (byte)(Part3 >> 24);
			bytes[16] = (byte)(Part4 >> 0);
			bytes[17] = (byte)(Part4 >> 8);
			bytes[18] = (byte)(Part4 >> 16);
			bytes[19] = (byte)(Part4 >> 24);
			return bytes;
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of an UInt160 value to an Int160.
		/// </summary>
		/// <param name="value">The value to convert to an Int160.</param>
		/// <returns>An Int160 that is bitwise equal to value.</returns>
		public static explicit operator Int160(UInt160 value)
		{
			return new Int160(value.Part0, value.Part1, value.Part2, value.Part3, value.Part4);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="UInt160"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Part0.GetHashCode() + Part1.GetHashCode() + Part2.GetHashCode() + Part3.GetHashCode() + Part4.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="UInt160"/> object or a type capable
		/// of implicit conversion to a <see cref="UInt160"/> object, and its value
		/// is equal to the current <see cref="UInt160"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is UInt160) { return Equals((UInt160)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// integer have the same value.
		/// </summary>
		/// <param name="other">The integer to compare.</param>
		/// <returns>true if this integer and value have the same value; otherwise, false.</returns>
		public bool Equals(UInt160 other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(UInt160 left, UInt160 right)
		{
			return left.Part0 == right.Part0 & left.Part1 == right.Part1 & left.Part2 == right.Part2 & left.Part3 == right.Part3 & left.Part4 == right.Part4;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are not equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(UInt160 left, UInt160 right)
		{
			return left.Part0 != right.Part0 | left.Part1 != right.Part1 | left.Part2 != right.Part2 | left.Part3 != right.Part3 | left.Part4 != right.Part4;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified culture-specific
		/// formatting information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by provider.</returns>
		public string ToString(System.IFormatProvider provider)
		{
			return ToString("G", provider);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format for its components.
		/// formatting information.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format)
		{
			return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, System.IFormatProvider provider)
		{
			var bytes = ToByteArray();
			bytes = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(bytes, new byte[] { 0 }));
			return new System.Numerics.BigInteger(bytes).ToString(format, provider);
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for integer functions.
	/// </summary>
	public static partial class Integer
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="UInt160"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, UInt160 integer)
		{
			writer.Write(integer.ToByteArray());
		}
		/// <summary>
		/// Reads a <see cref="UInt160"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static UInt160 ReadUInt160(this Ibasa.IO.BinaryReader reader)
		{
			return new UInt160(reader.ReadBytes(20));
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static UInt160 BitwiseNot(UInt160 value)
		{
			return ~value;
		}
		/// <summary>
		/// Returns the bitwise and of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise and of left and right.</returns>
		public static UInt160 BitwiseAnd(UInt160 left, UInt160 right)
		{
			return left & right;
		}
		/// <summary>
		/// Returns the bitwise or of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise or of left and right.</returns>
		public static UInt160 BitwiseOr(UInt160 left, UInt160 right)
		{
			return left | right;
		}
		/// <summary>
		/// Returns the bitwise xor of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise xor of left and right.</returns>
		public static UInt160 BitwiseXor(UInt160 left, UInt160 right)
		{
			return left ^ right;
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(UInt160 left, UInt160 right)
		{
			return left == right;
		}
		#endregion
	}
}
namespace Ibasa.Numerics
{
	/// <summary>
	/// A 256 bit signed integer.
	/// </summary>
	[System.Serializable]
	[System.Runtime.InteropServices.ComVisible(true)]
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct Int256: System.IEquatable<Int256>, System.IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Int256"/> equal to zero.
		/// </summary>
		public static readonly Int256 Zero = new Int256(0);
		/// <summary>
		/// Returns a new <see cref="Int256"/> equal to one.
		/// </summary>
		public static readonly Int256 One = new Int256(1);
		#endregion
		#region Fields
		private readonly uint Part0;
		private readonly uint Part1;
		private readonly uint Part2;
		private readonly uint Part3;
		private readonly uint Part4;
		private readonly uint Part5;
		private readonly uint Part6;
		private readonly uint Part7;
		#endregion
		#region Properties
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Int256"/> using the specified bytes.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public Int256(byte[] bytes)
		{
			Part0 = (uint)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0]);
			Part1 = (uint)((bytes[4] << 24) | (bytes[3] << 16) | (bytes[2] << 8) | bytes[1]);
			Part2 = (uint)((bytes[5] << 24) | (bytes[4] << 16) | (bytes[3] << 8) | bytes[2]);
			Part3 = (uint)((bytes[6] << 24) | (bytes[5] << 16) | (bytes[4] << 8) | bytes[3]);
			Part4 = (uint)((bytes[7] << 24) | (bytes[6] << 16) | (bytes[5] << 8) | bytes[4]);
			Part5 = (uint)((bytes[8] << 24) | (bytes[7] << 16) | (bytes[6] << 8) | bytes[5]);
			Part6 = (uint)((bytes[9] << 24) | (bytes[8] << 16) | (bytes[7] << 8) | bytes[6]);
			Part7 = (uint)((bytes[10] << 24) | (bytes[9] << 16) | (bytes[8] << 8) | bytes[7]);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int256"/> using the specified 32 bit parts.
		/// </summary>
		[System.CLSCompliant(false)]
		public Int256(uint part0, uint part1, uint part2, uint part3, uint part4, uint part5, uint part6, uint part7)
		{
			Part0 = part0;
			Part1 = part1;
			Part2 = part2;
			Part3 = part3;
			Part4 = part4;
			Part5 = part5;
			Part6 = part6;
			Part7 = part7;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int256"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public Int256(uint value)
		{
			Part0 = value;
			Part1 = 0;
			Part2 = 0;
			Part3 = 0;
			Part4 = 0;
			Part5 = 0;
			Part6 = 0;
			Part7 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int256"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public Int256(int value)
		{
			Part0 = (uint)value;
			Part1 = value < 0 ? uint.MaxValue : 0;
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
			Part4 = value < 0 ? uint.MaxValue : 0;
			Part5 = value < 0 ? uint.MaxValue : 0;
			Part6 = value < 0 ? uint.MaxValue : 0;
			Part7 = value < 0 ? uint.MaxValue : 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int256"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public Int256(ulong value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = 0;
			Part3 = 0;
			Part4 = 0;
			Part5 = 0;
			Part6 = 0;
			Part7 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int256"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public Int256(long value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
			Part4 = value < 0 ? uint.MaxValue : 0;
			Part5 = value < 0 ? uint.MaxValue : 0;
			Part6 = value < 0 ? uint.MaxValue : 0;
			Part7 = value < 0 ? uint.MaxValue : 0;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The identity of value.</returns>
		public static Int256 operator +(Int256 value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The negative of value.</returns>
		public static Int256 operator -(Int256 value)
		{
			return (Int256)(~value + Int256.One);
		}
		/// <summary>
		/// Adds two integers and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Int256 operator +(Int256 left, Int256 right)
		{
			unsafe
			{
				uint* parts = stackalloc uint[8];
				ulong carry = 0;
				ulong n = 0;
				n = carry + left.Part0 + right.Part0;
				parts[0] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part1 + right.Part1;
				parts[1] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part2 + right.Part2;
				parts[2] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part3 + right.Part3;
				parts[3] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part4 + right.Part4;
				parts[4] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part5 + right.Part5;
				parts[5] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part6 + right.Part6;
				parts[6] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part7 + right.Part7;
				parts[7] = (uint)n;
				carry = n >> 32;
				return new Int256(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7]);
			}
		}
		/// <summary>
		/// Subtracts one integer from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Int256 operator -(Int256 left, Int256 right)
		{
			return left + (Int256)(-right);
		}
		/// <summary>
		/// Returns the product of a integer and scalar.
		/// </summary>
		/// <param name="left">The first integer to multiply.</param>
		/// <param name="right">The second integer to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Int256 operator *(Int256 left, Int256 right)
		{
			throw new System.NotImplementedException("operator *");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Int256 operator /(Int256 left, Int256 right)
		{
			throw new System.NotImplementedException("operator /");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the remainder.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The modulus from dividing left by right (the remainder).</returns>
		public static Int256 operator %(Int256 left, Int256 right)
		{
			throw new System.NotImplementedException("operator %");
		}
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static Int256 operator ~(Int256 value)
		{
			return new Int256(~value.Part0, ~value.Part1, ~value.Part2, ~value.Part3, ~value.Part4, ~value.Part5, ~value.Part6, ~value.Part7);
		}
		/// <summary>
		/// Returns the bitwise AND of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise AND of left and right.</returns>
		public static Int256 operator &(Int256 left, Int256 right)
		{
			return new Int256(left.Part0 & right.Part0, left.Part1 & right.Part1, left.Part2 & right.Part2, left.Part3 & right.Part3, left.Part4 & right.Part4, left.Part5 & right.Part5, left.Part6 & right.Part6, left.Part7 & right.Part7);
		}
		/// <summary>
		/// Returns the bitwise OR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise OR of left and right.</returns>
		public static Int256 operator |(Int256 left, Int256 right)
		{
			return new Int256(left.Part0 | right.Part0, left.Part1 | right.Part1, left.Part2 | right.Part2, left.Part3 | right.Part3, left.Part4 | right.Part4, left.Part5 | right.Part5, left.Part6 | right.Part6, left.Part7 | right.Part7);
		}
		/// <summary>
		/// Returns the bitwise XOR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise XOR of left and right.</returns>
		public static Int256 operator ^(Int256 left, Int256 right)
		{
			return new Int256(left.Part0 ^ right.Part0, left.Part1 ^ right.Part1, left.Part2 ^ right.Part2, left.Part3 ^ right.Part3, left.Part4 ^ right.Part4, left.Part5 ^ right.Part5, left.Part6 ^ right.Part6, left.Part7 ^ right.Part7);
		}
		public static Int256 operator <<(Int256 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[8];
				uint* vparts = stackalloc uint[8];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				vparts[4] = value.Part4;
				vparts[5] = value.Part5;
				vparts[6] = value.Part6;
				vparts[7] = value.Part7;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 8; i++)
				{
					if (i + k + 1 < 8)
					{
						parts[i + k + 1] |= (vparts[i] >> (32 - shift));
					}
					if (i + k < 8)
					{
						parts[i + k] |= (vparts[i] << shift);
					}
				}
				return new Int256(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7]);
			}
		}
		public static Int256 operator >>(Int256 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[8];
				uint* vparts = stackalloc uint[8];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				vparts[4] = value.Part4;
				vparts[5] = value.Part5;
				vparts[6] = value.Part6;
				vparts[7] = value.Part7;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 8; i++)
				{
					if (i - k - 1 >= 0)
					{
						parts[i - k - 1] |= (vparts[i] << (32 - shift));
					}
					if (i - k >= 0)
					{
						parts[i - k] |= (vparts[i] >> shift);
					}
				}
				uint negative = (uint)((int)vparts[7] >> 32);
				for (int i = System.Math.Max(0, 8 - k); i < 8; i++)
				{
					parts[i] = negative;
				}
				negative <<= (32 - shift);
				if(8 - k - 1 >= 0)
				{
					parts[8 - k - 1] |= negative;
				}
				return new Int256(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7]);
			}
		}
		public byte[] ToByteArray()
		{
			var bytes = new byte[32];
			bytes[0] = (byte)(Part0 >> 0);
			bytes[1] = (byte)(Part0 >> 8);
			bytes[2] = (byte)(Part0 >> 16);
			bytes[3] = (byte)(Part0 >> 24);
			bytes[4] = (byte)(Part1 >> 0);
			bytes[5] = (byte)(Part1 >> 8);
			bytes[6] = (byte)(Part1 >> 16);
			bytes[7] = (byte)(Part1 >> 24);
			bytes[8] = (byte)(Part2 >> 0);
			bytes[9] = (byte)(Part2 >> 8);
			bytes[10] = (byte)(Part2 >> 16);
			bytes[11] = (byte)(Part2 >> 24);
			bytes[12] = (byte)(Part3 >> 0);
			bytes[13] = (byte)(Part3 >> 8);
			bytes[14] = (byte)(Part3 >> 16);
			bytes[15] = (byte)(Part3 >> 24);
			bytes[16] = (byte)(Part4 >> 0);
			bytes[17] = (byte)(Part4 >> 8);
			bytes[18] = (byte)(Part4 >> 16);
			bytes[19] = (byte)(Part4 >> 24);
			bytes[20] = (byte)(Part5 >> 0);
			bytes[21] = (byte)(Part5 >> 8);
			bytes[22] = (byte)(Part5 >> 16);
			bytes[23] = (byte)(Part5 >> 24);
			bytes[24] = (byte)(Part6 >> 0);
			bytes[25] = (byte)(Part6 >> 8);
			bytes[26] = (byte)(Part6 >> 16);
			bytes[27] = (byte)(Part6 >> 24);
			bytes[28] = (byte)(Part7 >> 0);
			bytes[29] = (byte)(Part7 >> 8);
			bytes[30] = (byte)(Part7 >> 16);
			bytes[31] = (byte)(Part7 >> 24);
			return bytes;
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of an Int256 value to an UInt256.
		/// </summary>
		/// <param name="value">The value to convert to an UInt256.</param>
		/// <returns>An UInt256 that is bitwise equal to value.</returns>
		public static explicit operator UInt256(Int256 value)
		{
			return new UInt256(value.Part0, value.Part1, value.Part2, value.Part3, value.Part4, value.Part5, value.Part6, value.Part7);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Int256"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Part0.GetHashCode() + Part1.GetHashCode() + Part2.GetHashCode() + Part3.GetHashCode() + Part4.GetHashCode() + Part5.GetHashCode() + Part6.GetHashCode() + Part7.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Int256"/> object or a type capable
		/// of implicit conversion to a <see cref="Int256"/> object, and its value
		/// is equal to the current <see cref="Int256"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Int256) { return Equals((Int256)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// integer have the same value.
		/// </summary>
		/// <param name="other">The integer to compare.</param>
		/// <returns>true if this integer and value have the same value; otherwise, false.</returns>
		public bool Equals(Int256 other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Int256 left, Int256 right)
		{
			return left.Part0 == right.Part0 & left.Part1 == right.Part1 & left.Part2 == right.Part2 & left.Part3 == right.Part3 & left.Part4 == right.Part4 & left.Part5 == right.Part5 & left.Part6 == right.Part6 & left.Part7 == right.Part7;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are not equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Int256 left, Int256 right)
		{
			return left.Part0 != right.Part0 | left.Part1 != right.Part1 | left.Part2 != right.Part2 | left.Part3 != right.Part3 | left.Part4 != right.Part4 | left.Part5 != right.Part5 | left.Part6 != right.Part6 | left.Part7 != right.Part7;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified culture-specific
		/// formatting information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by provider.</returns>
		public string ToString(System.IFormatProvider provider)
		{
			return ToString("G", provider);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format for its components.
		/// formatting information.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format)
		{
			return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, System.IFormatProvider provider)
		{
			var bytes = ToByteArray();
			return new System.Numerics.BigInteger(bytes).ToString(format, provider);
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for integer functions.
	/// </summary>
	public static partial class Integer
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Int256"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Int256 integer)
		{
			writer.Write(integer.ToByteArray());
		}
		/// <summary>
		/// Reads a <see cref="Int256"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Int256 ReadInt256(this Ibasa.IO.BinaryReader reader)
		{
			return new Int256(reader.ReadBytes(32));
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static Int256 BitwiseNot(Int256 value)
		{
			return ~value;
		}
		/// <summary>
		/// Returns the bitwise and of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise and of left and right.</returns>
		public static Int256 BitwiseAnd(Int256 left, Int256 right)
		{
			return left & right;
		}
		/// <summary>
		/// Returns the bitwise or of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise or of left and right.</returns>
		public static Int256 BitwiseOr(Int256 left, Int256 right)
		{
			return left | right;
		}
		/// <summary>
		/// Returns the bitwise xor of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise xor of left and right.</returns>
		public static Int256 BitwiseXor(Int256 left, Int256 right)
		{
			return left ^ right;
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Int256 left, Int256 right)
		{
			return left == right;
		}
		#endregion
	}
}
namespace Ibasa.Numerics
{
	/// <summary>
	/// A 256 bit unsigned integer.
	/// </summary>
	[System.Serializable]
	[System.Runtime.InteropServices.ComVisible(true)]
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct UInt256: System.IEquatable<UInt256>, System.IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="UInt256"/> equal to zero.
		/// </summary>
		public static readonly UInt256 Zero = new UInt256(0);
		/// <summary>
		/// Returns a new <see cref="UInt256"/> equal to one.
		/// </summary>
		public static readonly UInt256 One = new UInt256(1);
		#endregion
		#region Fields
		private readonly uint Part0;
		private readonly uint Part1;
		private readonly uint Part2;
		private readonly uint Part3;
		private readonly uint Part4;
		private readonly uint Part5;
		private readonly uint Part6;
		private readonly uint Part7;
		#endregion
		#region Properties
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt256"/> using the specified bytes.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public UInt256(byte[] bytes)
		{
			Part0 = (uint)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0]);
			Part1 = (uint)((bytes[4] << 24) | (bytes[3] << 16) | (bytes[2] << 8) | bytes[1]);
			Part2 = (uint)((bytes[5] << 24) | (bytes[4] << 16) | (bytes[3] << 8) | bytes[2]);
			Part3 = (uint)((bytes[6] << 24) | (bytes[5] << 16) | (bytes[4] << 8) | bytes[3]);
			Part4 = (uint)((bytes[7] << 24) | (bytes[6] << 16) | (bytes[5] << 8) | bytes[4]);
			Part5 = (uint)((bytes[8] << 24) | (bytes[7] << 16) | (bytes[6] << 8) | bytes[5]);
			Part6 = (uint)((bytes[9] << 24) | (bytes[8] << 16) | (bytes[7] << 8) | bytes[6]);
			Part7 = (uint)((bytes[10] << 24) | (bytes[9] << 16) | (bytes[8] << 8) | bytes[7]);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt256"/> using the specified 32 bit parts.
		/// </summary>
		[System.CLSCompliant(false)]
		public UInt256(uint part0, uint part1, uint part2, uint part3, uint part4, uint part5, uint part6, uint part7)
		{
			Part0 = part0;
			Part1 = part1;
			Part2 = part2;
			Part3 = part3;
			Part4 = part4;
			Part5 = part5;
			Part6 = part6;
			Part7 = part7;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt256"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public UInt256(uint value)
		{
			Part0 = value;
			Part1 = 0;
			Part2 = 0;
			Part3 = 0;
			Part4 = 0;
			Part5 = 0;
			Part6 = 0;
			Part7 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt256"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public UInt256(int value)
		{
			Part0 = (uint)value;
			Part1 = value < 0 ? uint.MaxValue : 0;
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
			Part4 = value < 0 ? uint.MaxValue : 0;
			Part5 = value < 0 ? uint.MaxValue : 0;
			Part6 = value < 0 ? uint.MaxValue : 0;
			Part7 = value < 0 ? uint.MaxValue : 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt256"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public UInt256(ulong value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = 0;
			Part3 = 0;
			Part4 = 0;
			Part5 = 0;
			Part6 = 0;
			Part7 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt256"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public UInt256(long value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
			Part4 = value < 0 ? uint.MaxValue : 0;
			Part5 = value < 0 ? uint.MaxValue : 0;
			Part6 = value < 0 ? uint.MaxValue : 0;
			Part7 = value < 0 ? uint.MaxValue : 0;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The identity of value.</returns>
		public static UInt256 operator +(UInt256 value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The negative of value.</returns>
		public static Int256 operator -(UInt256 value)
		{
			return (Int256)(~value + UInt256.One);
		}
		/// <summary>
		/// Adds two integers and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static UInt256 operator +(UInt256 left, UInt256 right)
		{
			unsafe
			{
				uint* parts = stackalloc uint[8];
				ulong carry = 0;
				ulong n = 0;
				n = carry + left.Part0 + right.Part0;
				parts[0] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part1 + right.Part1;
				parts[1] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part2 + right.Part2;
				parts[2] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part3 + right.Part3;
				parts[3] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part4 + right.Part4;
				parts[4] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part5 + right.Part5;
				parts[5] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part6 + right.Part6;
				parts[6] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part7 + right.Part7;
				parts[7] = (uint)n;
				carry = n >> 32;
				return new UInt256(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7]);
			}
		}
		/// <summary>
		/// Subtracts one integer from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static UInt256 operator -(UInt256 left, UInt256 right)
		{
			return left + (UInt256)(-right);
		}
		/// <summary>
		/// Returns the product of a integer and scalar.
		/// </summary>
		/// <param name="left">The first integer to multiply.</param>
		/// <param name="right">The second integer to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static UInt256 operator *(UInt256 left, UInt256 right)
		{
			throw new System.NotImplementedException("operator *");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static UInt256 operator /(UInt256 left, UInt256 right)
		{
			throw new System.NotImplementedException("operator /");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the remainder.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The modulus from dividing left by right (the remainder).</returns>
		public static UInt256 operator %(UInt256 left, UInt256 right)
		{
			throw new System.NotImplementedException("operator %");
		}
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static UInt256 operator ~(UInt256 value)
		{
			return new UInt256(~value.Part0, ~value.Part1, ~value.Part2, ~value.Part3, ~value.Part4, ~value.Part5, ~value.Part6, ~value.Part7);
		}
		/// <summary>
		/// Returns the bitwise AND of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise AND of left and right.</returns>
		public static UInt256 operator &(UInt256 left, UInt256 right)
		{
			return new UInt256(left.Part0 & right.Part0, left.Part1 & right.Part1, left.Part2 & right.Part2, left.Part3 & right.Part3, left.Part4 & right.Part4, left.Part5 & right.Part5, left.Part6 & right.Part6, left.Part7 & right.Part7);
		}
		/// <summary>
		/// Returns the bitwise OR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise OR of left and right.</returns>
		public static UInt256 operator |(UInt256 left, UInt256 right)
		{
			return new UInt256(left.Part0 | right.Part0, left.Part1 | right.Part1, left.Part2 | right.Part2, left.Part3 | right.Part3, left.Part4 | right.Part4, left.Part5 | right.Part5, left.Part6 | right.Part6, left.Part7 | right.Part7);
		}
		/// <summary>
		/// Returns the bitwise XOR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise XOR of left and right.</returns>
		public static UInt256 operator ^(UInt256 left, UInt256 right)
		{
			return new UInt256(left.Part0 ^ right.Part0, left.Part1 ^ right.Part1, left.Part2 ^ right.Part2, left.Part3 ^ right.Part3, left.Part4 ^ right.Part4, left.Part5 ^ right.Part5, left.Part6 ^ right.Part6, left.Part7 ^ right.Part7);
		}
		public static UInt256 operator <<(UInt256 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[8];
				uint* vparts = stackalloc uint[8];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				vparts[4] = value.Part4;
				vparts[5] = value.Part5;
				vparts[6] = value.Part6;
				vparts[7] = value.Part7;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 8; i++)
				{
					if (i + k + 1 < 8)
					{
						parts[i + k + 1] |= (vparts[i] >> (32 - shift));
					}
					if (i + k < 8)
					{
						parts[i + k] |= (vparts[i] << shift);
					}
				}
				return new UInt256(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7]);
			}
		}
		public static UInt256 operator >>(UInt256 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[8];
				uint* vparts = stackalloc uint[8];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				vparts[4] = value.Part4;
				vparts[5] = value.Part5;
				vparts[6] = value.Part6;
				vparts[7] = value.Part7;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 8; i++)
				{
					if (i - k - 1 >= 0)
					{
						parts[i - k - 1] |= (vparts[i] << (32 - shift));
					}
					if (i - k >= 0)
					{
						parts[i - k] |= (vparts[i] >> shift);
					}
				}
				return new UInt256(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7]);
			}
		}
		public byte[] ToByteArray()
		{
			var bytes = new byte[32];
			bytes[0] = (byte)(Part0 >> 0);
			bytes[1] = (byte)(Part0 >> 8);
			bytes[2] = (byte)(Part0 >> 16);
			bytes[3] = (byte)(Part0 >> 24);
			bytes[4] = (byte)(Part1 >> 0);
			bytes[5] = (byte)(Part1 >> 8);
			bytes[6] = (byte)(Part1 >> 16);
			bytes[7] = (byte)(Part1 >> 24);
			bytes[8] = (byte)(Part2 >> 0);
			bytes[9] = (byte)(Part2 >> 8);
			bytes[10] = (byte)(Part2 >> 16);
			bytes[11] = (byte)(Part2 >> 24);
			bytes[12] = (byte)(Part3 >> 0);
			bytes[13] = (byte)(Part3 >> 8);
			bytes[14] = (byte)(Part3 >> 16);
			bytes[15] = (byte)(Part3 >> 24);
			bytes[16] = (byte)(Part4 >> 0);
			bytes[17] = (byte)(Part4 >> 8);
			bytes[18] = (byte)(Part4 >> 16);
			bytes[19] = (byte)(Part4 >> 24);
			bytes[20] = (byte)(Part5 >> 0);
			bytes[21] = (byte)(Part5 >> 8);
			bytes[22] = (byte)(Part5 >> 16);
			bytes[23] = (byte)(Part5 >> 24);
			bytes[24] = (byte)(Part6 >> 0);
			bytes[25] = (byte)(Part6 >> 8);
			bytes[26] = (byte)(Part6 >> 16);
			bytes[27] = (byte)(Part6 >> 24);
			bytes[28] = (byte)(Part7 >> 0);
			bytes[29] = (byte)(Part7 >> 8);
			bytes[30] = (byte)(Part7 >> 16);
			bytes[31] = (byte)(Part7 >> 24);
			return bytes;
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of an UInt256 value to an Int256.
		/// </summary>
		/// <param name="value">The value to convert to an Int256.</param>
		/// <returns>An Int256 that is bitwise equal to value.</returns>
		public static explicit operator Int256(UInt256 value)
		{
			return new Int256(value.Part0, value.Part1, value.Part2, value.Part3, value.Part4, value.Part5, value.Part6, value.Part7);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="UInt256"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Part0.GetHashCode() + Part1.GetHashCode() + Part2.GetHashCode() + Part3.GetHashCode() + Part4.GetHashCode() + Part5.GetHashCode() + Part6.GetHashCode() + Part7.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="UInt256"/> object or a type capable
		/// of implicit conversion to a <see cref="UInt256"/> object, and its value
		/// is equal to the current <see cref="UInt256"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is UInt256) { return Equals((UInt256)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// integer have the same value.
		/// </summary>
		/// <param name="other">The integer to compare.</param>
		/// <returns>true if this integer and value have the same value; otherwise, false.</returns>
		public bool Equals(UInt256 other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(UInt256 left, UInt256 right)
		{
			return left.Part0 == right.Part0 & left.Part1 == right.Part1 & left.Part2 == right.Part2 & left.Part3 == right.Part3 & left.Part4 == right.Part4 & left.Part5 == right.Part5 & left.Part6 == right.Part6 & left.Part7 == right.Part7;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are not equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(UInt256 left, UInt256 right)
		{
			return left.Part0 != right.Part0 | left.Part1 != right.Part1 | left.Part2 != right.Part2 | left.Part3 != right.Part3 | left.Part4 != right.Part4 | left.Part5 != right.Part5 | left.Part6 != right.Part6 | left.Part7 != right.Part7;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified culture-specific
		/// formatting information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by provider.</returns>
		public string ToString(System.IFormatProvider provider)
		{
			return ToString("G", provider);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format for its components.
		/// formatting information.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format)
		{
			return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, System.IFormatProvider provider)
		{
			var bytes = ToByteArray();
			bytes = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(bytes, new byte[] { 0 }));
			return new System.Numerics.BigInteger(bytes).ToString(format, provider);
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for integer functions.
	/// </summary>
	public static partial class Integer
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="UInt256"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, UInt256 integer)
		{
			writer.Write(integer.ToByteArray());
		}
		/// <summary>
		/// Reads a <see cref="UInt256"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static UInt256 ReadUInt256(this Ibasa.IO.BinaryReader reader)
		{
			return new UInt256(reader.ReadBytes(32));
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static UInt256 BitwiseNot(UInt256 value)
		{
			return ~value;
		}
		/// <summary>
		/// Returns the bitwise and of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise and of left and right.</returns>
		public static UInt256 BitwiseAnd(UInt256 left, UInt256 right)
		{
			return left & right;
		}
		/// <summary>
		/// Returns the bitwise or of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise or of left and right.</returns>
		public static UInt256 BitwiseOr(UInt256 left, UInt256 right)
		{
			return left | right;
		}
		/// <summary>
		/// Returns the bitwise xor of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise xor of left and right.</returns>
		public static UInt256 BitwiseXor(UInt256 left, UInt256 right)
		{
			return left ^ right;
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(UInt256 left, UInt256 right)
		{
			return left == right;
		}
		#endregion
	}
}
namespace Ibasa.Numerics
{
	/// <summary>
	/// A 512 bit signed integer.
	/// </summary>
	[System.Serializable]
	[System.Runtime.InteropServices.ComVisible(true)]
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct Int512: System.IEquatable<Int512>, System.IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Int512"/> equal to zero.
		/// </summary>
		public static readonly Int512 Zero = new Int512(0);
		/// <summary>
		/// Returns a new <see cref="Int512"/> equal to one.
		/// </summary>
		public static readonly Int512 One = new Int512(1);
		#endregion
		#region Fields
		private readonly uint Part0;
		private readonly uint Part1;
		private readonly uint Part2;
		private readonly uint Part3;
		private readonly uint Part4;
		private readonly uint Part5;
		private readonly uint Part6;
		private readonly uint Part7;
		private readonly uint Part8;
		private readonly uint Part9;
		private readonly uint Part10;
		private readonly uint Part11;
		private readonly uint Part12;
		private readonly uint Part13;
		private readonly uint Part14;
		private readonly uint Part15;
		#endregion
		#region Properties
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Int512"/> using the specified bytes.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public Int512(byte[] bytes)
		{
			Part0 = (uint)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0]);
			Part1 = (uint)((bytes[4] << 24) | (bytes[3] << 16) | (bytes[2] << 8) | bytes[1]);
			Part2 = (uint)((bytes[5] << 24) | (bytes[4] << 16) | (bytes[3] << 8) | bytes[2]);
			Part3 = (uint)((bytes[6] << 24) | (bytes[5] << 16) | (bytes[4] << 8) | bytes[3]);
			Part4 = (uint)((bytes[7] << 24) | (bytes[6] << 16) | (bytes[5] << 8) | bytes[4]);
			Part5 = (uint)((bytes[8] << 24) | (bytes[7] << 16) | (bytes[6] << 8) | bytes[5]);
			Part6 = (uint)((bytes[9] << 24) | (bytes[8] << 16) | (bytes[7] << 8) | bytes[6]);
			Part7 = (uint)((bytes[10] << 24) | (bytes[9] << 16) | (bytes[8] << 8) | bytes[7]);
			Part8 = (uint)((bytes[11] << 24) | (bytes[10] << 16) | (bytes[9] << 8) | bytes[8]);
			Part9 = (uint)((bytes[12] << 24) | (bytes[11] << 16) | (bytes[10] << 8) | bytes[9]);
			Part10 = (uint)((bytes[13] << 24) | (bytes[12] << 16) | (bytes[11] << 8) | bytes[10]);
			Part11 = (uint)((bytes[14] << 24) | (bytes[13] << 16) | (bytes[12] << 8) | bytes[11]);
			Part12 = (uint)((bytes[15] << 24) | (bytes[14] << 16) | (bytes[13] << 8) | bytes[12]);
			Part13 = (uint)((bytes[16] << 24) | (bytes[15] << 16) | (bytes[14] << 8) | bytes[13]);
			Part14 = (uint)((bytes[17] << 24) | (bytes[16] << 16) | (bytes[15] << 8) | bytes[14]);
			Part15 = (uint)((bytes[18] << 24) | (bytes[17] << 16) | (bytes[16] << 8) | bytes[15]);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int512"/> using the specified 32 bit parts.
		/// </summary>
		[System.CLSCompliant(false)]
		public Int512(uint part0, uint part1, uint part2, uint part3, uint part4, uint part5, uint part6, uint part7, uint part8, uint part9, uint part10, uint part11, uint part12, uint part13, uint part14, uint part15)
		{
			Part0 = part0;
			Part1 = part1;
			Part2 = part2;
			Part3 = part3;
			Part4 = part4;
			Part5 = part5;
			Part6 = part6;
			Part7 = part7;
			Part8 = part8;
			Part9 = part9;
			Part10 = part10;
			Part11 = part11;
			Part12 = part12;
			Part13 = part13;
			Part14 = part14;
			Part15 = part15;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int512"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public Int512(uint value)
		{
			Part0 = value;
			Part1 = 0;
			Part2 = 0;
			Part3 = 0;
			Part4 = 0;
			Part5 = 0;
			Part6 = 0;
			Part7 = 0;
			Part8 = 0;
			Part9 = 0;
			Part10 = 0;
			Part11 = 0;
			Part12 = 0;
			Part13 = 0;
			Part14 = 0;
			Part15 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int512"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public Int512(int value)
		{
			Part0 = (uint)value;
			Part1 = value < 0 ? uint.MaxValue : 0;
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
			Part4 = value < 0 ? uint.MaxValue : 0;
			Part5 = value < 0 ? uint.MaxValue : 0;
			Part6 = value < 0 ? uint.MaxValue : 0;
			Part7 = value < 0 ? uint.MaxValue : 0;
			Part8 = value < 0 ? uint.MaxValue : 0;
			Part9 = value < 0 ? uint.MaxValue : 0;
			Part10 = value < 0 ? uint.MaxValue : 0;
			Part11 = value < 0 ? uint.MaxValue : 0;
			Part12 = value < 0 ? uint.MaxValue : 0;
			Part13 = value < 0 ? uint.MaxValue : 0;
			Part14 = value < 0 ? uint.MaxValue : 0;
			Part15 = value < 0 ? uint.MaxValue : 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int512"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public Int512(ulong value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = 0;
			Part3 = 0;
			Part4 = 0;
			Part5 = 0;
			Part6 = 0;
			Part7 = 0;
			Part8 = 0;
			Part9 = 0;
			Part10 = 0;
			Part11 = 0;
			Part12 = 0;
			Part13 = 0;
			Part14 = 0;
			Part15 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int512"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public Int512(long value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
			Part4 = value < 0 ? uint.MaxValue : 0;
			Part5 = value < 0 ? uint.MaxValue : 0;
			Part6 = value < 0 ? uint.MaxValue : 0;
			Part7 = value < 0 ? uint.MaxValue : 0;
			Part8 = value < 0 ? uint.MaxValue : 0;
			Part9 = value < 0 ? uint.MaxValue : 0;
			Part10 = value < 0 ? uint.MaxValue : 0;
			Part11 = value < 0 ? uint.MaxValue : 0;
			Part12 = value < 0 ? uint.MaxValue : 0;
			Part13 = value < 0 ? uint.MaxValue : 0;
			Part14 = value < 0 ? uint.MaxValue : 0;
			Part15 = value < 0 ? uint.MaxValue : 0;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The identity of value.</returns>
		public static Int512 operator +(Int512 value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The negative of value.</returns>
		public static Int512 operator -(Int512 value)
		{
			return (Int512)(~value + Int512.One);
		}
		/// <summary>
		/// Adds two integers and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Int512 operator +(Int512 left, Int512 right)
		{
			unsafe
			{
				uint* parts = stackalloc uint[16];
				ulong carry = 0;
				ulong n = 0;
				n = carry + left.Part0 + right.Part0;
				parts[0] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part1 + right.Part1;
				parts[1] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part2 + right.Part2;
				parts[2] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part3 + right.Part3;
				parts[3] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part4 + right.Part4;
				parts[4] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part5 + right.Part5;
				parts[5] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part6 + right.Part6;
				parts[6] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part7 + right.Part7;
				parts[7] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part8 + right.Part8;
				parts[8] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part9 + right.Part9;
				parts[9] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part10 + right.Part10;
				parts[10] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part11 + right.Part11;
				parts[11] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part12 + right.Part12;
				parts[12] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part13 + right.Part13;
				parts[13] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part14 + right.Part14;
				parts[14] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part15 + right.Part15;
				parts[15] = (uint)n;
				carry = n >> 32;
				return new Int512(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], parts[8], parts[9], parts[10], parts[11], parts[12], parts[13], parts[14], parts[15]);
			}
		}
		/// <summary>
		/// Subtracts one integer from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Int512 operator -(Int512 left, Int512 right)
		{
			return left + (Int512)(-right);
		}
		/// <summary>
		/// Returns the product of a integer and scalar.
		/// </summary>
		/// <param name="left">The first integer to multiply.</param>
		/// <param name="right">The second integer to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Int512 operator *(Int512 left, Int512 right)
		{
			throw new System.NotImplementedException("operator *");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Int512 operator /(Int512 left, Int512 right)
		{
			throw new System.NotImplementedException("operator /");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the remainder.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The modulus from dividing left by right (the remainder).</returns>
		public static Int512 operator %(Int512 left, Int512 right)
		{
			throw new System.NotImplementedException("operator %");
		}
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static Int512 operator ~(Int512 value)
		{
			return new Int512(~value.Part0, ~value.Part1, ~value.Part2, ~value.Part3, ~value.Part4, ~value.Part5, ~value.Part6, ~value.Part7, ~value.Part8, ~value.Part9, ~value.Part10, ~value.Part11, ~value.Part12, ~value.Part13, ~value.Part14, ~value.Part15);
		}
		/// <summary>
		/// Returns the bitwise AND of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise AND of left and right.</returns>
		public static Int512 operator &(Int512 left, Int512 right)
		{
			return new Int512(left.Part0 & right.Part0, left.Part1 & right.Part1, left.Part2 & right.Part2, left.Part3 & right.Part3, left.Part4 & right.Part4, left.Part5 & right.Part5, left.Part6 & right.Part6, left.Part7 & right.Part7, left.Part8 & right.Part8, left.Part9 & right.Part9, left.Part10 & right.Part10, left.Part11 & right.Part11, left.Part12 & right.Part12, left.Part13 & right.Part13, left.Part14 & right.Part14, left.Part15 & right.Part15);
		}
		/// <summary>
		/// Returns the bitwise OR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise OR of left and right.</returns>
		public static Int512 operator |(Int512 left, Int512 right)
		{
			return new Int512(left.Part0 | right.Part0, left.Part1 | right.Part1, left.Part2 | right.Part2, left.Part3 | right.Part3, left.Part4 | right.Part4, left.Part5 | right.Part5, left.Part6 | right.Part6, left.Part7 | right.Part7, left.Part8 | right.Part8, left.Part9 | right.Part9, left.Part10 | right.Part10, left.Part11 | right.Part11, left.Part12 | right.Part12, left.Part13 | right.Part13, left.Part14 | right.Part14, left.Part15 | right.Part15);
		}
		/// <summary>
		/// Returns the bitwise XOR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise XOR of left and right.</returns>
		public static Int512 operator ^(Int512 left, Int512 right)
		{
			return new Int512(left.Part0 ^ right.Part0, left.Part1 ^ right.Part1, left.Part2 ^ right.Part2, left.Part3 ^ right.Part3, left.Part4 ^ right.Part4, left.Part5 ^ right.Part5, left.Part6 ^ right.Part6, left.Part7 ^ right.Part7, left.Part8 ^ right.Part8, left.Part9 ^ right.Part9, left.Part10 ^ right.Part10, left.Part11 ^ right.Part11, left.Part12 ^ right.Part12, left.Part13 ^ right.Part13, left.Part14 ^ right.Part14, left.Part15 ^ right.Part15);
		}
		public static Int512 operator <<(Int512 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[16];
				uint* vparts = stackalloc uint[16];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				vparts[4] = value.Part4;
				vparts[5] = value.Part5;
				vparts[6] = value.Part6;
				vparts[7] = value.Part7;
				vparts[8] = value.Part8;
				vparts[9] = value.Part9;
				vparts[10] = value.Part10;
				vparts[11] = value.Part11;
				vparts[12] = value.Part12;
				vparts[13] = value.Part13;
				vparts[14] = value.Part14;
				vparts[15] = value.Part15;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 16; i++)
				{
					if (i + k + 1 < 16)
					{
						parts[i + k + 1] |= (vparts[i] >> (32 - shift));
					}
					if (i + k < 16)
					{
						parts[i + k] |= (vparts[i] << shift);
					}
				}
				return new Int512(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], parts[8], parts[9], parts[10], parts[11], parts[12], parts[13], parts[14], parts[15]);
			}
		}
		public static Int512 operator >>(Int512 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[16];
				uint* vparts = stackalloc uint[16];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				vparts[4] = value.Part4;
				vparts[5] = value.Part5;
				vparts[6] = value.Part6;
				vparts[7] = value.Part7;
				vparts[8] = value.Part8;
				vparts[9] = value.Part9;
				vparts[10] = value.Part10;
				vparts[11] = value.Part11;
				vparts[12] = value.Part12;
				vparts[13] = value.Part13;
				vparts[14] = value.Part14;
				vparts[15] = value.Part15;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 16; i++)
				{
					if (i - k - 1 >= 0)
					{
						parts[i - k - 1] |= (vparts[i] << (32 - shift));
					}
					if (i - k >= 0)
					{
						parts[i - k] |= (vparts[i] >> shift);
					}
				}
				uint negative = (uint)((int)vparts[15] >> 32);
				for (int i = System.Math.Max(0, 16 - k); i < 16; i++)
				{
					parts[i] = negative;
				}
				negative <<= (32 - shift);
				if(16 - k - 1 >= 0)
				{
					parts[16 - k - 1] |= negative;
				}
				return new Int512(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], parts[8], parts[9], parts[10], parts[11], parts[12], parts[13], parts[14], parts[15]);
			}
		}
		public byte[] ToByteArray()
		{
			var bytes = new byte[64];
			bytes[0] = (byte)(Part0 >> 0);
			bytes[1] = (byte)(Part0 >> 8);
			bytes[2] = (byte)(Part0 >> 16);
			bytes[3] = (byte)(Part0 >> 24);
			bytes[4] = (byte)(Part1 >> 0);
			bytes[5] = (byte)(Part1 >> 8);
			bytes[6] = (byte)(Part1 >> 16);
			bytes[7] = (byte)(Part1 >> 24);
			bytes[8] = (byte)(Part2 >> 0);
			bytes[9] = (byte)(Part2 >> 8);
			bytes[10] = (byte)(Part2 >> 16);
			bytes[11] = (byte)(Part2 >> 24);
			bytes[12] = (byte)(Part3 >> 0);
			bytes[13] = (byte)(Part3 >> 8);
			bytes[14] = (byte)(Part3 >> 16);
			bytes[15] = (byte)(Part3 >> 24);
			bytes[16] = (byte)(Part4 >> 0);
			bytes[17] = (byte)(Part4 >> 8);
			bytes[18] = (byte)(Part4 >> 16);
			bytes[19] = (byte)(Part4 >> 24);
			bytes[20] = (byte)(Part5 >> 0);
			bytes[21] = (byte)(Part5 >> 8);
			bytes[22] = (byte)(Part5 >> 16);
			bytes[23] = (byte)(Part5 >> 24);
			bytes[24] = (byte)(Part6 >> 0);
			bytes[25] = (byte)(Part6 >> 8);
			bytes[26] = (byte)(Part6 >> 16);
			bytes[27] = (byte)(Part6 >> 24);
			bytes[28] = (byte)(Part7 >> 0);
			bytes[29] = (byte)(Part7 >> 8);
			bytes[30] = (byte)(Part7 >> 16);
			bytes[31] = (byte)(Part7 >> 24);
			bytes[32] = (byte)(Part8 >> 0);
			bytes[33] = (byte)(Part8 >> 8);
			bytes[34] = (byte)(Part8 >> 16);
			bytes[35] = (byte)(Part8 >> 24);
			bytes[36] = (byte)(Part9 >> 0);
			bytes[37] = (byte)(Part9 >> 8);
			bytes[38] = (byte)(Part9 >> 16);
			bytes[39] = (byte)(Part9 >> 24);
			bytes[40] = (byte)(Part10 >> 0);
			bytes[41] = (byte)(Part10 >> 8);
			bytes[42] = (byte)(Part10 >> 16);
			bytes[43] = (byte)(Part10 >> 24);
			bytes[44] = (byte)(Part11 >> 0);
			bytes[45] = (byte)(Part11 >> 8);
			bytes[46] = (byte)(Part11 >> 16);
			bytes[47] = (byte)(Part11 >> 24);
			bytes[48] = (byte)(Part12 >> 0);
			bytes[49] = (byte)(Part12 >> 8);
			bytes[50] = (byte)(Part12 >> 16);
			bytes[51] = (byte)(Part12 >> 24);
			bytes[52] = (byte)(Part13 >> 0);
			bytes[53] = (byte)(Part13 >> 8);
			bytes[54] = (byte)(Part13 >> 16);
			bytes[55] = (byte)(Part13 >> 24);
			bytes[56] = (byte)(Part14 >> 0);
			bytes[57] = (byte)(Part14 >> 8);
			bytes[58] = (byte)(Part14 >> 16);
			bytes[59] = (byte)(Part14 >> 24);
			bytes[60] = (byte)(Part15 >> 0);
			bytes[61] = (byte)(Part15 >> 8);
			bytes[62] = (byte)(Part15 >> 16);
			bytes[63] = (byte)(Part15 >> 24);
			return bytes;
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of an Int512 value to an UInt512.
		/// </summary>
		/// <param name="value">The value to convert to an UInt512.</param>
		/// <returns>An UInt512 that is bitwise equal to value.</returns>
		public static explicit operator UInt512(Int512 value)
		{
			return new UInt512(value.Part0, value.Part1, value.Part2, value.Part3, value.Part4, value.Part5, value.Part6, value.Part7, value.Part8, value.Part9, value.Part10, value.Part11, value.Part12, value.Part13, value.Part14, value.Part15);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Int512"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Part0.GetHashCode() + Part1.GetHashCode() + Part2.GetHashCode() + Part3.GetHashCode() + Part4.GetHashCode() + Part5.GetHashCode() + Part6.GetHashCode() + Part7.GetHashCode() + Part8.GetHashCode() + Part9.GetHashCode() + Part10.GetHashCode() + Part11.GetHashCode() + Part12.GetHashCode() + Part13.GetHashCode() + Part14.GetHashCode() + Part15.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Int512"/> object or a type capable
		/// of implicit conversion to a <see cref="Int512"/> object, and its value
		/// is equal to the current <see cref="Int512"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Int512) { return Equals((Int512)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// integer have the same value.
		/// </summary>
		/// <param name="other">The integer to compare.</param>
		/// <returns>true if this integer and value have the same value; otherwise, false.</returns>
		public bool Equals(Int512 other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Int512 left, Int512 right)
		{
			return left.Part0 == right.Part0 & left.Part1 == right.Part1 & left.Part2 == right.Part2 & left.Part3 == right.Part3 & left.Part4 == right.Part4 & left.Part5 == right.Part5 & left.Part6 == right.Part6 & left.Part7 == right.Part7 & left.Part8 == right.Part8 & left.Part9 == right.Part9 & left.Part10 == right.Part10 & left.Part11 == right.Part11 & left.Part12 == right.Part12 & left.Part13 == right.Part13 & left.Part14 == right.Part14 & left.Part15 == right.Part15;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are not equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Int512 left, Int512 right)
		{
			return left.Part0 != right.Part0 | left.Part1 != right.Part1 | left.Part2 != right.Part2 | left.Part3 != right.Part3 | left.Part4 != right.Part4 | left.Part5 != right.Part5 | left.Part6 != right.Part6 | left.Part7 != right.Part7 | left.Part8 != right.Part8 | left.Part9 != right.Part9 | left.Part10 != right.Part10 | left.Part11 != right.Part11 | left.Part12 != right.Part12 | left.Part13 != right.Part13 | left.Part14 != right.Part14 | left.Part15 != right.Part15;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified culture-specific
		/// formatting information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by provider.</returns>
		public string ToString(System.IFormatProvider provider)
		{
			return ToString("G", provider);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format for its components.
		/// formatting information.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format)
		{
			return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, System.IFormatProvider provider)
		{
			var bytes = ToByteArray();
			return new System.Numerics.BigInteger(bytes).ToString(format, provider);
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for integer functions.
	/// </summary>
	public static partial class Integer
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Int512"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Int512 integer)
		{
			writer.Write(integer.ToByteArray());
		}
		/// <summary>
		/// Reads a <see cref="Int512"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Int512 ReadInt512(this Ibasa.IO.BinaryReader reader)
		{
			return new Int512(reader.ReadBytes(64));
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static Int512 BitwiseNot(Int512 value)
		{
			return ~value;
		}
		/// <summary>
		/// Returns the bitwise and of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise and of left and right.</returns>
		public static Int512 BitwiseAnd(Int512 left, Int512 right)
		{
			return left & right;
		}
		/// <summary>
		/// Returns the bitwise or of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise or of left and right.</returns>
		public static Int512 BitwiseOr(Int512 left, Int512 right)
		{
			return left | right;
		}
		/// <summary>
		/// Returns the bitwise xor of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise xor of left and right.</returns>
		public static Int512 BitwiseXor(Int512 left, Int512 right)
		{
			return left ^ right;
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Int512 left, Int512 right)
		{
			return left == right;
		}
		#endregion
	}
}
namespace Ibasa.Numerics
{
	/// <summary>
	/// A 512 bit unsigned integer.
	/// </summary>
	[System.Serializable]
	[System.Runtime.InteropServices.ComVisible(true)]
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct UInt512: System.IEquatable<UInt512>, System.IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="UInt512"/> equal to zero.
		/// </summary>
		public static readonly UInt512 Zero = new UInt512(0);
		/// <summary>
		/// Returns a new <see cref="UInt512"/> equal to one.
		/// </summary>
		public static readonly UInt512 One = new UInt512(1);
		#endregion
		#region Fields
		private readonly uint Part0;
		private readonly uint Part1;
		private readonly uint Part2;
		private readonly uint Part3;
		private readonly uint Part4;
		private readonly uint Part5;
		private readonly uint Part6;
		private readonly uint Part7;
		private readonly uint Part8;
		private readonly uint Part9;
		private readonly uint Part10;
		private readonly uint Part11;
		private readonly uint Part12;
		private readonly uint Part13;
		private readonly uint Part14;
		private readonly uint Part15;
		#endregion
		#region Properties
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt512"/> using the specified bytes.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public UInt512(byte[] bytes)
		{
			Part0 = (uint)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0]);
			Part1 = (uint)((bytes[4] << 24) | (bytes[3] << 16) | (bytes[2] << 8) | bytes[1]);
			Part2 = (uint)((bytes[5] << 24) | (bytes[4] << 16) | (bytes[3] << 8) | bytes[2]);
			Part3 = (uint)((bytes[6] << 24) | (bytes[5] << 16) | (bytes[4] << 8) | bytes[3]);
			Part4 = (uint)((bytes[7] << 24) | (bytes[6] << 16) | (bytes[5] << 8) | bytes[4]);
			Part5 = (uint)((bytes[8] << 24) | (bytes[7] << 16) | (bytes[6] << 8) | bytes[5]);
			Part6 = (uint)((bytes[9] << 24) | (bytes[8] << 16) | (bytes[7] << 8) | bytes[6]);
			Part7 = (uint)((bytes[10] << 24) | (bytes[9] << 16) | (bytes[8] << 8) | bytes[7]);
			Part8 = (uint)((bytes[11] << 24) | (bytes[10] << 16) | (bytes[9] << 8) | bytes[8]);
			Part9 = (uint)((bytes[12] << 24) | (bytes[11] << 16) | (bytes[10] << 8) | bytes[9]);
			Part10 = (uint)((bytes[13] << 24) | (bytes[12] << 16) | (bytes[11] << 8) | bytes[10]);
			Part11 = (uint)((bytes[14] << 24) | (bytes[13] << 16) | (bytes[12] << 8) | bytes[11]);
			Part12 = (uint)((bytes[15] << 24) | (bytes[14] << 16) | (bytes[13] << 8) | bytes[12]);
			Part13 = (uint)((bytes[16] << 24) | (bytes[15] << 16) | (bytes[14] << 8) | bytes[13]);
			Part14 = (uint)((bytes[17] << 24) | (bytes[16] << 16) | (bytes[15] << 8) | bytes[14]);
			Part15 = (uint)((bytes[18] << 24) | (bytes[17] << 16) | (bytes[16] << 8) | bytes[15]);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt512"/> using the specified 32 bit parts.
		/// </summary>
		[System.CLSCompliant(false)]
		public UInt512(uint part0, uint part1, uint part2, uint part3, uint part4, uint part5, uint part6, uint part7, uint part8, uint part9, uint part10, uint part11, uint part12, uint part13, uint part14, uint part15)
		{
			Part0 = part0;
			Part1 = part1;
			Part2 = part2;
			Part3 = part3;
			Part4 = part4;
			Part5 = part5;
			Part6 = part6;
			Part7 = part7;
			Part8 = part8;
			Part9 = part9;
			Part10 = part10;
			Part11 = part11;
			Part12 = part12;
			Part13 = part13;
			Part14 = part14;
			Part15 = part15;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt512"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public UInt512(uint value)
		{
			Part0 = value;
			Part1 = 0;
			Part2 = 0;
			Part3 = 0;
			Part4 = 0;
			Part5 = 0;
			Part6 = 0;
			Part7 = 0;
			Part8 = 0;
			Part9 = 0;
			Part10 = 0;
			Part11 = 0;
			Part12 = 0;
			Part13 = 0;
			Part14 = 0;
			Part15 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt512"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public UInt512(int value)
		{
			Part0 = (uint)value;
			Part1 = value < 0 ? uint.MaxValue : 0;
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
			Part4 = value < 0 ? uint.MaxValue : 0;
			Part5 = value < 0 ? uint.MaxValue : 0;
			Part6 = value < 0 ? uint.MaxValue : 0;
			Part7 = value < 0 ? uint.MaxValue : 0;
			Part8 = value < 0 ? uint.MaxValue : 0;
			Part9 = value < 0 ? uint.MaxValue : 0;
			Part10 = value < 0 ? uint.MaxValue : 0;
			Part11 = value < 0 ? uint.MaxValue : 0;
			Part12 = value < 0 ? uint.MaxValue : 0;
			Part13 = value < 0 ? uint.MaxValue : 0;
			Part14 = value < 0 ? uint.MaxValue : 0;
			Part15 = value < 0 ? uint.MaxValue : 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt512"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		[System.CLSCompliant(false)]
		public UInt512(ulong value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = 0;
			Part3 = 0;
			Part4 = 0;
			Part5 = 0;
			Part6 = 0;
			Part7 = 0;
			Part8 = 0;
			Part9 = 0;
			Part10 = 0;
			Part11 = 0;
			Part12 = 0;
			Part13 = 0;
			Part14 = 0;
			Part15 = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt512"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned.</param>
		public UInt512(long value)
		{
			Part0 = (uint)value;
			Part1 = (uint)(value >> 32);
			Part2 = value < 0 ? uint.MaxValue : 0;
			Part3 = value < 0 ? uint.MaxValue : 0;
			Part4 = value < 0 ? uint.MaxValue : 0;
			Part5 = value < 0 ? uint.MaxValue : 0;
			Part6 = value < 0 ? uint.MaxValue : 0;
			Part7 = value < 0 ? uint.MaxValue : 0;
			Part8 = value < 0 ? uint.MaxValue : 0;
			Part9 = value < 0 ? uint.MaxValue : 0;
			Part10 = value < 0 ? uint.MaxValue : 0;
			Part11 = value < 0 ? uint.MaxValue : 0;
			Part12 = value < 0 ? uint.MaxValue : 0;
			Part13 = value < 0 ? uint.MaxValue : 0;
			Part14 = value < 0 ? uint.MaxValue : 0;
			Part15 = value < 0 ? uint.MaxValue : 0;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The identity of value.</returns>
		public static UInt512 operator +(UInt512 value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The negative of value.</returns>
		public static Int512 operator -(UInt512 value)
		{
			return (Int512)(~value + UInt512.One);
		}
		/// <summary>
		/// Adds two integers and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static UInt512 operator +(UInt512 left, UInt512 right)
		{
			unsafe
			{
				uint* parts = stackalloc uint[16];
				ulong carry = 0;
				ulong n = 0;
				n = carry + left.Part0 + right.Part0;
				parts[0] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part1 + right.Part1;
				parts[1] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part2 + right.Part2;
				parts[2] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part3 + right.Part3;
				parts[3] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part4 + right.Part4;
				parts[4] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part5 + right.Part5;
				parts[5] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part6 + right.Part6;
				parts[6] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part7 + right.Part7;
				parts[7] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part8 + right.Part8;
				parts[8] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part9 + right.Part9;
				parts[9] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part10 + right.Part10;
				parts[10] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part11 + right.Part11;
				parts[11] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part12 + right.Part12;
				parts[12] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part13 + right.Part13;
				parts[13] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part14 + right.Part14;
				parts[14] = (uint)n;
				carry = n >> 32;
				n = carry + left.Part15 + right.Part15;
				parts[15] = (uint)n;
				carry = n >> 32;
				return new UInt512(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], parts[8], parts[9], parts[10], parts[11], parts[12], parts[13], parts[14], parts[15]);
			}
		}
		/// <summary>
		/// Subtracts one integer from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static UInt512 operator -(UInt512 left, UInt512 right)
		{
			return left + (UInt512)(-right);
		}
		/// <summary>
		/// Returns the product of a integer and scalar.
		/// </summary>
		/// <param name="left">The first integer to multiply.</param>
		/// <param name="right">The second integer to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static UInt512 operator *(UInt512 left, UInt512 right)
		{
			throw new System.NotImplementedException("operator *");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static UInt512 operator /(UInt512 left, UInt512 right)
		{
			throw new System.NotImplementedException("operator /");
		}
		/// <summary>
		/// Divides a integer by a scalar and returns the remainder.
		/// </summary>
		/// <param name="left">The integer to be divided (the dividend).</param>
		/// <param name="right">The integer to divide by (the divisor).</param>
		/// <returns>The modulus from dividing left by right (the remainder).</returns>
		public static UInt512 operator %(UInt512 left, UInt512 right)
		{
			throw new System.NotImplementedException("operator %");
		}
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static UInt512 operator ~(UInt512 value)
		{
			return new UInt512(~value.Part0, ~value.Part1, ~value.Part2, ~value.Part3, ~value.Part4, ~value.Part5, ~value.Part6, ~value.Part7, ~value.Part8, ~value.Part9, ~value.Part10, ~value.Part11, ~value.Part12, ~value.Part13, ~value.Part14, ~value.Part15);
		}
		/// <summary>
		/// Returns the bitwise AND of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise AND of left and right.</returns>
		public static UInt512 operator &(UInt512 left, UInt512 right)
		{
			return new UInt512(left.Part0 & right.Part0, left.Part1 & right.Part1, left.Part2 & right.Part2, left.Part3 & right.Part3, left.Part4 & right.Part4, left.Part5 & right.Part5, left.Part6 & right.Part6, left.Part7 & right.Part7, left.Part8 & right.Part8, left.Part9 & right.Part9, left.Part10 & right.Part10, left.Part11 & right.Part11, left.Part12 & right.Part12, left.Part13 & right.Part13, left.Part14 & right.Part14, left.Part15 & right.Part15);
		}
		/// <summary>
		/// Returns the bitwise OR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise OR of left and right.</returns>
		public static UInt512 operator |(UInt512 left, UInt512 right)
		{
			return new UInt512(left.Part0 | right.Part0, left.Part1 | right.Part1, left.Part2 | right.Part2, left.Part3 | right.Part3, left.Part4 | right.Part4, left.Part5 | right.Part5, left.Part6 | right.Part6, left.Part7 | right.Part7, left.Part8 | right.Part8, left.Part9 | right.Part9, left.Part10 | right.Part10, left.Part11 | right.Part11, left.Part12 | right.Part12, left.Part13 | right.Part13, left.Part14 | right.Part14, left.Part15 | right.Part15);
		}
		/// <summary>
		/// Returns the bitwise XOR of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise XOR of left and right.</returns>
		public static UInt512 operator ^(UInt512 left, UInt512 right)
		{
			return new UInt512(left.Part0 ^ right.Part0, left.Part1 ^ right.Part1, left.Part2 ^ right.Part2, left.Part3 ^ right.Part3, left.Part4 ^ right.Part4, left.Part5 ^ right.Part5, left.Part6 ^ right.Part6, left.Part7 ^ right.Part7, left.Part8 ^ right.Part8, left.Part9 ^ right.Part9, left.Part10 ^ right.Part10, left.Part11 ^ right.Part11, left.Part12 ^ right.Part12, left.Part13 ^ right.Part13, left.Part14 ^ right.Part14, left.Part15 ^ right.Part15);
		}
		public static UInt512 operator <<(UInt512 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[16];
				uint* vparts = stackalloc uint[16];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				vparts[4] = value.Part4;
				vparts[5] = value.Part5;
				vparts[6] = value.Part6;
				vparts[7] = value.Part7;
				vparts[8] = value.Part8;
				vparts[9] = value.Part9;
				vparts[10] = value.Part10;
				vparts[11] = value.Part11;
				vparts[12] = value.Part12;
				vparts[13] = value.Part13;
				vparts[14] = value.Part14;
				vparts[15] = value.Part15;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 16; i++)
				{
					if (i + k + 1 < 16)
					{
						parts[i + k + 1] |= (vparts[i] >> (32 - shift));
					}
					if (i + k < 16)
					{
						parts[i + k] |= (vparts[i] << shift);
					}
				}
				return new UInt512(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], parts[8], parts[9], parts[10], parts[11], parts[12], parts[13], parts[14], parts[15]);
			}
		}
		public static UInt512 operator >>(UInt512 value, int amount)
		{
			unsafe
			{
				uint* parts = stackalloc uint[16];
				uint* vparts = stackalloc uint[16];
				vparts[0] = value.Part0;
				vparts[1] = value.Part1;
				vparts[2] = value.Part2;
				vparts[3] = value.Part3;
				vparts[4] = value.Part4;
				vparts[5] = value.Part5;
				vparts[6] = value.Part6;
				vparts[7] = value.Part7;
				vparts[8] = value.Part8;
				vparts[9] = value.Part9;
				vparts[10] = value.Part10;
				vparts[11] = value.Part11;
				vparts[12] = value.Part12;
				vparts[13] = value.Part13;
				vparts[14] = value.Part14;
				vparts[15] = value.Part15;
				int k = amount / 32;
				int shift = amount % 32;
				for (int i = 0; i < 16; i++)
				{
					if (i - k - 1 >= 0)
					{
						parts[i - k - 1] |= (vparts[i] << (32 - shift));
					}
					if (i - k >= 0)
					{
						parts[i - k] |= (vparts[i] >> shift);
					}
				}
				return new UInt512(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], parts[8], parts[9], parts[10], parts[11], parts[12], parts[13], parts[14], parts[15]);
			}
		}
		public byte[] ToByteArray()
		{
			var bytes = new byte[64];
			bytes[0] = (byte)(Part0 >> 0);
			bytes[1] = (byte)(Part0 >> 8);
			bytes[2] = (byte)(Part0 >> 16);
			bytes[3] = (byte)(Part0 >> 24);
			bytes[4] = (byte)(Part1 >> 0);
			bytes[5] = (byte)(Part1 >> 8);
			bytes[6] = (byte)(Part1 >> 16);
			bytes[7] = (byte)(Part1 >> 24);
			bytes[8] = (byte)(Part2 >> 0);
			bytes[9] = (byte)(Part2 >> 8);
			bytes[10] = (byte)(Part2 >> 16);
			bytes[11] = (byte)(Part2 >> 24);
			bytes[12] = (byte)(Part3 >> 0);
			bytes[13] = (byte)(Part3 >> 8);
			bytes[14] = (byte)(Part3 >> 16);
			bytes[15] = (byte)(Part3 >> 24);
			bytes[16] = (byte)(Part4 >> 0);
			bytes[17] = (byte)(Part4 >> 8);
			bytes[18] = (byte)(Part4 >> 16);
			bytes[19] = (byte)(Part4 >> 24);
			bytes[20] = (byte)(Part5 >> 0);
			bytes[21] = (byte)(Part5 >> 8);
			bytes[22] = (byte)(Part5 >> 16);
			bytes[23] = (byte)(Part5 >> 24);
			bytes[24] = (byte)(Part6 >> 0);
			bytes[25] = (byte)(Part6 >> 8);
			bytes[26] = (byte)(Part6 >> 16);
			bytes[27] = (byte)(Part6 >> 24);
			bytes[28] = (byte)(Part7 >> 0);
			bytes[29] = (byte)(Part7 >> 8);
			bytes[30] = (byte)(Part7 >> 16);
			bytes[31] = (byte)(Part7 >> 24);
			bytes[32] = (byte)(Part8 >> 0);
			bytes[33] = (byte)(Part8 >> 8);
			bytes[34] = (byte)(Part8 >> 16);
			bytes[35] = (byte)(Part8 >> 24);
			bytes[36] = (byte)(Part9 >> 0);
			bytes[37] = (byte)(Part9 >> 8);
			bytes[38] = (byte)(Part9 >> 16);
			bytes[39] = (byte)(Part9 >> 24);
			bytes[40] = (byte)(Part10 >> 0);
			bytes[41] = (byte)(Part10 >> 8);
			bytes[42] = (byte)(Part10 >> 16);
			bytes[43] = (byte)(Part10 >> 24);
			bytes[44] = (byte)(Part11 >> 0);
			bytes[45] = (byte)(Part11 >> 8);
			bytes[46] = (byte)(Part11 >> 16);
			bytes[47] = (byte)(Part11 >> 24);
			bytes[48] = (byte)(Part12 >> 0);
			bytes[49] = (byte)(Part12 >> 8);
			bytes[50] = (byte)(Part12 >> 16);
			bytes[51] = (byte)(Part12 >> 24);
			bytes[52] = (byte)(Part13 >> 0);
			bytes[53] = (byte)(Part13 >> 8);
			bytes[54] = (byte)(Part13 >> 16);
			bytes[55] = (byte)(Part13 >> 24);
			bytes[56] = (byte)(Part14 >> 0);
			bytes[57] = (byte)(Part14 >> 8);
			bytes[58] = (byte)(Part14 >> 16);
			bytes[59] = (byte)(Part14 >> 24);
			bytes[60] = (byte)(Part15 >> 0);
			bytes[61] = (byte)(Part15 >> 8);
			bytes[62] = (byte)(Part15 >> 16);
			bytes[63] = (byte)(Part15 >> 24);
			return bytes;
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of an UInt512 value to an Int512.
		/// </summary>
		/// <param name="value">The value to convert to an Int512.</param>
		/// <returns>An Int512 that is bitwise equal to value.</returns>
		public static explicit operator Int512(UInt512 value)
		{
			return new Int512(value.Part0, value.Part1, value.Part2, value.Part3, value.Part4, value.Part5, value.Part6, value.Part7, value.Part8, value.Part9, value.Part10, value.Part11, value.Part12, value.Part13, value.Part14, value.Part15);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="UInt512"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Part0.GetHashCode() + Part1.GetHashCode() + Part2.GetHashCode() + Part3.GetHashCode() + Part4.GetHashCode() + Part5.GetHashCode() + Part6.GetHashCode() + Part7.GetHashCode() + Part8.GetHashCode() + Part9.GetHashCode() + Part10.GetHashCode() + Part11.GetHashCode() + Part12.GetHashCode() + Part13.GetHashCode() + Part14.GetHashCode() + Part15.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="UInt512"/> object or a type capable
		/// of implicit conversion to a <see cref="UInt512"/> object, and its value
		/// is equal to the current <see cref="UInt512"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is UInt512) { return Equals((UInt512)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// integer have the same value.
		/// </summary>
		/// <param name="other">The integer to compare.</param>
		/// <returns>true if this integer and value have the same value; otherwise, false.</returns>
		public bool Equals(UInt512 other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(UInt512 left, UInt512 right)
		{
			return left.Part0 == right.Part0 & left.Part1 == right.Part1 & left.Part2 == right.Part2 & left.Part3 == right.Part3 & left.Part4 == right.Part4 & left.Part5 == right.Part5 & left.Part6 == right.Part6 & left.Part7 == right.Part7 & left.Part8 == right.Part8 & left.Part9 == right.Part9 & left.Part10 == right.Part10 & left.Part11 == right.Part11 & left.Part12 == right.Part12 & left.Part13 == right.Part13 & left.Part14 == right.Part14 & left.Part15 == right.Part15;
		}
		/// <summary>
		/// Returns a value that indicates whether two integers are not equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(UInt512 left, UInt512 right)
		{
			return left.Part0 != right.Part0 | left.Part1 != right.Part1 | left.Part2 != right.Part2 | left.Part3 != right.Part3 | left.Part4 != right.Part4 | left.Part5 != right.Part5 | left.Part6 != right.Part6 | left.Part7 != right.Part7 | left.Part8 != right.Part8 | left.Part9 != right.Part9 | left.Part10 != right.Part10 | left.Part11 != right.Part11 | left.Part12 != right.Part12 | left.Part13 != right.Part13 | left.Part14 != right.Part14 | left.Part15 != right.Part15;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified culture-specific
		/// formatting information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by provider.</returns>
		public string ToString(System.IFormatProvider provider)
		{
			return ToString("G", provider);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format for its components.
		/// formatting information.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format)
		{
			return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current integer to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, System.IFormatProvider provider)
		{
			var bytes = ToByteArray();
			bytes = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(bytes, new byte[] { 0 }));
			return new System.Numerics.BigInteger(bytes).ToString(format, provider);
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for integer functions.
	/// </summary>
	public static partial class Integer
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="UInt512"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, UInt512 integer)
		{
			writer.Write(integer.ToByteArray());
		}
		/// <summary>
		/// Reads a <see cref="UInt512"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static UInt512 ReadUInt512(this Ibasa.IO.BinaryReader reader)
		{
			return new UInt512(reader.ReadBytes(64));
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the bitwise not of an integer.
		/// </summary>
		/// <param name="value">An integer.</param>
		/// <returns>The bitwise not of value.</returns>
		public static UInt512 BitwiseNot(UInt512 value)
		{
			return ~value;
		}
		/// <summary>
		/// Returns the bitwise and of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise and of left and right.</returns>
		public static UInt512 BitwiseAnd(UInt512 left, UInt512 right)
		{
			return left & right;
		}
		/// <summary>
		/// Returns the bitwise or of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise or of left and right.</returns>
		public static UInt512 BitwiseOr(UInt512 left, UInt512 right)
		{
			return left | right;
		}
		/// <summary>
		/// Returns the bitwise xor of two integers.
		/// </summary>
		/// <param name="left">The first integer.</param>
		/// <param name="right">The second integer.</param>
		/// <returns>The bitwise xor of left and right.</returns>
		public static UInt512 BitwiseXor(UInt512 left, UInt512 right)
		{
			return left ^ right;
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two integers are equal.
		/// </summary>
		/// <param name="left">The first integer to compare.</param>
		/// <param name="right">The second integer to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(UInt512 left, UInt512 right)
		{
			return left == right;
		}
		#endregion
	}
}
