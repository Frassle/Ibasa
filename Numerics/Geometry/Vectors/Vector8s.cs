using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a eight component vector of shorts.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector8s: IEquatable<Vector8s>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector8s"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector8s Zero = new Vector8s(0);
		/// <summary>
		/// Returns a new <see cref="Vector8s"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector8s One = new Vector8s(1);
		#endregion
		#region Fields
		/// <summary>
		/// The first component of the vector.
		/// </summary>
		public readonly short V0;
		/// <summary>
		/// The second component of the vector.
		/// </summary>
		public readonly short V1;
		/// <summary>
		/// The third component of the vector.
		/// </summary>
		public readonly short V2;
		/// <summary>
		/// The fourth component of the vector.
		/// </summary>
		public readonly short V3;
		/// <summary>
		/// The fifth component of the vector.
		/// </summary>
		public readonly short V4;
		/// <summary>
		/// The sixth component of the vector.
		/// </summary>
		public readonly short V5;
		/// <summary>
		/// The seventh component of the vector.
		/// </summary>
		public readonly short V6;
		/// <summary>
		/// The eighth component of the vector.
		/// </summary>
		public readonly short V7;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public short this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return V0;
					case 1:
						return V1;
					case 2:
						return V2;
					case 3:
						return V3;
					case 4:
						return V4;
					case 5:
						return V5;
					case 6:
						return V6;
					case 7:
						return V7;
					default:
						throw new IndexOutOfRangeException("Indices for Vector8s run from 0 to 7, inclusive.");
				}
			}
		}
		public short[] ToArray()
		{
			return new short[]
			{
				V0, V1, V2, V3, V4, V5, V6, V7
			};
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector8s"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector8s(short value)
		{
			V0 = value;
			V1 = value;
			V2 = value;
			V3 = value;
			V4 = value;
			V5 = value;
			V6 = value;
			V7 = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector8s"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the first 2 components</param>
		/// <param name="v2">Value for the V2 component of the vector.</param>
		/// <param name="v3">Value for the V3 component of the vector.</param>
		/// <param name="v4">Value for the V4 component of the vector.</param>
		/// <param name="v5">Value for the V5 component of the vector.</param>
		/// <param name="v6">Value for the V6 component of the vector.</param>
		/// <param name="v7">Value for the V7 component of the vector.</param>
		public Vector8s(Vector2s value, short v2, short v3, short v4, short v5, short v6, short v7, short v8, short v9)
		{
			V0 = value.X;
			V1 = value.Y;
			V2 = v2;
			V3 = v3;
			V4 = v4;
			V5 = v5;
			V6 = v6;
			V7 = v7;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector8s"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the first 3 components</param>
		/// <param name="v3">Value for the V3 component of the vector.</param>
		/// <param name="v4">Value for the V4 component of the vector.</param>
		/// <param name="v5">Value for the V5 component of the vector.</param>
		/// <param name="v6">Value for the V6 component of the vector.</param>
		/// <param name="v7">Value for the V7 component of the vector.</param>
		public Vector8s(Vector3s value, short v3, short v4, short v5, short v6, short v7, short v8, short v9, short v10)
		{
			V0 = value.X;
			V1 = value.Y;
			V2 = value.Z;
			V3 = v3;
			V4 = v4;
			V5 = v5;
			V6 = v6;
			V7 = v7;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector8s"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the first 4 components</param>
		/// <param name="v4">Value for the V4 component of the vector.</param>
		/// <param name="v5">Value for the V5 component of the vector.</param>
		/// <param name="v6">Value for the V6 component of the vector.</param>
		/// <param name="v7">Value for the V7 component of the vector.</param>
		public Vector8s(Vector4s value, short v4, short v5, short v6, short v7, short v8, short v9, short v10, short v11)
		{
			V0 = value.X;
			V1 = value.Y;
			V2 = value.Z;
			V3 = value.W;
			V4 = v4;
			V5 = v5;
			V6 = v6;
			V7 = v7;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector8s"/> using the specified values.
		/// </summary>
		/// <param name="v0">Value for the V0 component of the vector.</param>
		/// <param name="v1">Value for the V1 component of the vector.</param>
		/// <param name="v2">Value for the V2 component of the vector.</param>
		/// <param name="v3">Value for the V3 component of the vector.</param>
		/// <param name="v4">Value for the V4 component of the vector.</param>
		/// <param name="v5">Value for the V5 component of the vector.</param>
		/// <param name="v6">Value for the V6 component of the vector.</param>
		/// <param name="v7">Value for the V7 component of the vector.</param>
		public Vector8s(short v0, short v1, short v2, short v3, short v4, short v5, short v6, short v7)
		{
			V0 = v0;
			V1 = v1;
			V2 = v2;
			V3 = v3;
			V4 = v4;
			V5 = v5;
			V6 = v6;
			V7 = v7;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector8s"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector8s(short[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			V0 = array[0];
			V1 = array[1];
			V2 = array[2];
			V3 = array[3];
			V4 = array[4];
			V5 = array[5];
			V6 = array[6];
			V7 = array[7];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector8s"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector8s(short[] array, int offset)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			V0 = array[offset + 0];
			V1 = array[offset + 1];
			V2 = array[offset + 2];
			V3 = array[offset + 3];
			V4 = array[offset + 4];
			V5 = array[offset + 5];
			V6 = array[offset + 6];
			V7 = array[offset + 7];
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The identity of value.</returns>
		public static Vector8i operator +(Vector8s value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector8i operator -(Vector8s value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector8i operator +(Vector8s left, Vector8s right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector8i operator -(Vector8s left, Vector8s right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector8i operator *(Vector8s left, int right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector8i operator *(int left, Vector8s right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector8i operator /(Vector8s left, int right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector8d value to a Vector8s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8s.</param>
		/// <returns>A Vector8s that has all components equal to value.</returns>
		public static explicit operator Vector8s(Vector8d value)
		{
			return new Vector8s((short)value.V0, (short)value.V1, (short)value.V2, (short)value.V3, (short)value.V4, (short)value.V5, (short)value.V6, (short)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8f value to a Vector8s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8s.</param>
		/// <returns>A Vector8s that has all components equal to value.</returns>
		public static explicit operator Vector8s(Vector8f value)
		{
			return new Vector8s((short)value.V0, (short)value.V1, (short)value.V2, (short)value.V3, (short)value.V4, (short)value.V5, (short)value.V6, (short)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8h value to a Vector8s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8s.</param>
		/// <returns>A Vector8s that has all components equal to value.</returns>
		public static explicit operator Vector8s(Vector8h value)
		{
			return new Vector8s((short)value.V0, (short)value.V1, (short)value.V2, (short)value.V3, (short)value.V4, (short)value.V5, (short)value.V6, (short)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8ul value to a Vector8s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8s.</param>
		/// <returns>A Vector8s that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector8s(Vector8ul value)
		{
			return new Vector8s((short)value.V0, (short)value.V1, (short)value.V2, (short)value.V3, (short)value.V4, (short)value.V5, (short)value.V6, (short)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8l value to a Vector8s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8s.</param>
		/// <returns>A Vector8s that has all components equal to value.</returns>
		public static explicit operator Vector8s(Vector8l value)
		{
			return new Vector8s((short)value.V0, (short)value.V1, (short)value.V2, (short)value.V3, (short)value.V4, (short)value.V5, (short)value.V6, (short)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8ui value to a Vector8s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8s.</param>
		/// <returns>A Vector8s that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector8s(Vector8ui value)
		{
			return new Vector8s((short)value.V0, (short)value.V1, (short)value.V2, (short)value.V3, (short)value.V4, (short)value.V5, (short)value.V6, (short)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8i value to a Vector8s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8s.</param>
		/// <returns>A Vector8s that has all components equal to value.</returns>
		public static explicit operator Vector8s(Vector8i value)
		{
			return new Vector8s((short)value.V0, (short)value.V1, (short)value.V2, (short)value.V3, (short)value.V4, (short)value.V5, (short)value.V6, (short)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8us value to a Vector8s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8s.</param>
		/// <returns>A Vector8s that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector8s(Vector8us value)
		{
			return new Vector8s((short)value.V0, (short)value.V1, (short)value.V2, (short)value.V3, (short)value.V4, (short)value.V5, (short)value.V6, (short)value.V7);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector8b value to a Vector8s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8s.</param>
		/// <returns>A Vector8s that has all components equal to value.</returns>
		public static implicit operator Vector8s(Vector8b value)
		{
			return new Vector8s((short)value.V0, (short)value.V1, (short)value.V2, (short)value.V3, (short)value.V4, (short)value.V5, (short)value.V6, (short)value.V7);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector8sb value to a Vector8s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8s.</param>
		/// <returns>A Vector8s that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector8s(Vector8sb value)
		{
			return new Vector8s((short)value.V0, (short)value.V1, (short)value.V2, (short)value.V3, (short)value.V4, (short)value.V5, (short)value.V6, (short)value.V7);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector8s"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return V0.GetHashCode() + V1.GetHashCode() + V2.GetHashCode() + V3.GetHashCode() + V4.GetHashCode() + V5.GetHashCode() + V6.GetHashCode() + V7.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Vector8s"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector8s"/> object, and its value
		/// is equal to the current <see cref="Vector8s"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector8s) { return Equals((Vector8s)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector8s other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector8s left, Vector8s right)
		{
			return left.V0 == right.V0 & left.V1 == right.V1 & left.V2 == right.V2 & left.V3 == right.V3 & left.V4 == right.V4 & left.V5 == right.V5 & left.V6 == right.V6 & left.V7 == right.V7;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector8s left, Vector8s right)
		{
			return left.V0 != right.V0 | left.V1 != right.V1 | left.V2 != right.V2 | left.V3 != right.V3 | left.V4 != right.V4 | left.V5 != right.V5 | left.V6 != right.V6 | left.V7 != right.V7;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current vector to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current vector to its equivalent string
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
		/// Converts the value of the current vector to its equivalent string
		/// representation in Cartesian form by using the specified format for its components.
		/// formatting information.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format)
		{
			return ToString(format, CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current vector to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, IFormatProvider provider)
		{
			return String.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", V0.ToString(format, provider), V1.ToString(format, provider), V2.ToString(format, provider), V3.ToString(format, provider), V4.ToString(format, provider), V5.ToString(format, provider), V6.ToString(format, provider), V7.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for vector functions.
	/// </summary>
	public static partial class Vector
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Vector8s"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector8s vector)
		{
			writer.Write(vector.V0);
			writer.Write(vector.V1);
			writer.Write(vector.V2);
			writer.Write(vector.V3);
			writer.Write(vector.V4);
			writer.Write(vector.V5);
			writer.Write(vector.V6);
			writer.Write(vector.V7);
		}
		/// <summary>
		/// Reads a <see cref="Vector8s"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector8s ReadVector8s(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector8s(reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16());
		}
		#endregion
		#region Pack
		public static long Pack(int v0Bits, int v1Bits, int v2Bits, int v3Bits, int v4Bits, int v5Bits, int v6Bits, int v7Bits, Vector8s vector)
		{
			Contract.Requires(0 <= v0Bits && v0Bits <= 16, "v0Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v1Bits && v1Bits <= 16, "v1Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v2Bits && v2Bits <= 16, "v2Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v3Bits && v3Bits <= 16, "v3Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v4Bits && v4Bits <= 16, "v4Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v5Bits && v5Bits <= 16, "v5Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v6Bits && v6Bits <= 16, "v6Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v7Bits && v7Bits <= 16, "v7Bits must be between 0 and 16 inclusive.");
			Contract.Requires(v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits <= 64);
			ulong v0 = (ulong)(vector.V0) >> (64 - v0Bits);
			ulong v1 = (ulong)(vector.V1) >> (64 - v1Bits);
			v1 <<= v0Bits;
			ulong v2 = (ulong)(vector.V2) >> (64 - v2Bits);
			v2 <<= v0Bits + v1Bits;
			ulong v3 = (ulong)(vector.V3) >> (64 - v3Bits);
			v3 <<= v0Bits + v1Bits + v2Bits;
			ulong v4 = (ulong)(vector.V4) >> (64 - v4Bits);
			v4 <<= v0Bits + v1Bits + v2Bits + v3Bits;
			ulong v5 = (ulong)(vector.V5) >> (64 - v5Bits);
			v5 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits;
			ulong v6 = (ulong)(vector.V6) >> (64 - v6Bits);
			v6 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits;
			ulong v7 = (ulong)(vector.V7) >> (64 - v7Bits);
			v7 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits;
			return (long)(v0 | v1 | v2 | v3 | v4 | v5 | v6 | v7);
		}
		public static Vector8s Unpack(int v0Bits, int v1Bits, int v2Bits, int v3Bits, int v4Bits, int v5Bits, int v6Bits, int v7Bits, short bits)
		{
			Contract.Requires(0 <= v0Bits && v0Bits <= 16, "v0Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v1Bits && v1Bits <= 16, "v1Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v2Bits && v2Bits <= 16, "v2Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v3Bits && v3Bits <= 16, "v3Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v4Bits && v4Bits <= 16, "v4Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v5Bits && v5Bits <= 16, "v5Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v6Bits && v6Bits <= 16, "v6Bits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= v7Bits && v7Bits <= 16, "v7Bits must be between 0 and 16 inclusive.");
			Contract.Requires(v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits <= 64);
			ulong v0 = (ulong)(bits);
			v0 &= ((1UL << v0Bits) - 1);
			ulong v1 = (ulong)(bits) >> (v0Bits);
			v1 &= ((1UL << v1Bits) - 1);
			ulong v2 = (ulong)(bits) >> (v0Bits + v1Bits);
			v2 &= ((1UL << v2Bits) - 1);
			ulong v3 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits);
			v3 &= ((1UL << v3Bits) - 1);
			ulong v4 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits);
			v4 &= ((1UL << v4Bits) - 1);
			ulong v5 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits);
			v5 &= ((1UL << v5Bits) - 1);
			ulong v6 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits);
			v6 &= ((1UL << v6Bits) - 1);
			ulong v7 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits);
			v7 &= ((1UL << v7Bits) - 1);
			return new Vector8s((short)v0, (short)v1, (short)v2, (short)v3, (short)v4, (short)v5, (short)v6, (short)v7);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector8i Negative(Vector8s value)
		{
			return new Vector8i(-value.V0, -value.V1, -value.V2, -value.V3, -value.V4, -value.V5, -value.V6, -value.V7);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector8i Add(Vector8s left, Vector8s right)
		{
			return new Vector8i(left.V0 + right.V0, left.V1 + right.V1, left.V2 + right.V2, left.V3 + right.V3, left.V4 + right.V4, left.V5 + right.V5, left.V6 + right.V6, left.V7 + right.V7);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector8i Subtract(Vector8s left, Vector8s right)
		{
			return new Vector8i(left.V0 - right.V0, left.V1 - right.V1, left.V2 - right.V2, left.V3 - right.V3, left.V4 - right.V4, left.V5 - right.V5, left.V6 - right.V6, left.V7 - right.V7);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector8i Multiply(Vector8s vector, int scalar)
		{
			return new Vector8i(vector.V0 * scalar, vector.V1 * scalar, vector.V2 * scalar, vector.V3 * scalar, vector.V4 * scalar, vector.V5 * scalar, vector.V6 * scalar, vector.V7 * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector8i Divide(Vector8s vector, int scalar)
		{
			return new Vector8i(vector.V0 / scalar, vector.V1 / scalar, vector.V2 / scalar, vector.V3 / scalar, vector.V4 / scalar, vector.V5 / scalar, vector.V6 / scalar, vector.V7 / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Vector8s left, Vector8s right)
		{
			return left == right;
		}
		#endregion
		#region Products
		/// <summary>
		/// Calculates the dot product (inner product) of two vectors.
		/// </summary>
		/// <param name="left">First source vector.</param>
		/// <param name="right">Second source vector.</param>
		/// <returns>The dot product of the two vectors.</returns>
		public static int Dot(Vector8s left, Vector8s right)
		{
			return left.V0 * right.V0 + left.V1 * right.V1 + left.V2 * right.V2 + left.V3 * right.V3 + left.V4 * right.V4 + left.V5 * right.V5 + left.V6 * right.V6 + left.V7 * right.V7;
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all components of a vector are non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if all components are non-zero; false otherwise.</returns>
		public static bool All(Vector8s value)
		{
			return value.V0 != 0 && value.V1 != 0 && value.V2 != 0 && value.V3 != 0 && value.V4 != 0 && value.V5 != 0 && value.V6 != 0 && value.V7 != 0;
		}
		/// <summary>
		/// Determines whether all components of a vector satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if every component of the vector passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Vector8s value, Predicate<short> predicate)
		{
			return predicate(value.V0) && predicate(value.V1) && predicate(value.V2) && predicate(value.V3) && predicate(value.V4) && predicate(value.V5) && predicate(value.V6) && predicate(value.V7);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Vector8s value)
		{
			return value.V0 != 0 || value.V1 != 0 || value.V2 != 0 || value.V3 != 0 || value.V4 != 0 || value.V5 != 0 || value.V6 != 0 || value.V7 != 0;
		}
		/// <summary>
		/// Determines whether any components of a vector satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if any component of the vector passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Vector8s value, Predicate<short> predicate)
		{
			return predicate(value.V0) || predicate(value.V1) || predicate(value.V2) || predicate(value.V3) || predicate(value.V4) || predicate(value.V5) || predicate(value.V6) || predicate(value.V7);
		}
		#endregion
		#region Properties
		/// <summary>
		/// Computes the absolute squared value of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute squared value of value.</returns> 
		public static int AbsoluteSquared(Vector8s value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		public static float Absolute(Vector8s value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		public static Vector8f Normalize(Vector8s value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Vector8s.Zero;
			}
			else
			{
				return (Vector8f)value / absolute;
			}
		}
		#endregion
		#region Per component
		#region Transform
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector8d Transform(Vector8s value, Func<short, double> transformer)
		{
			return new Vector8d(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector8f Transform(Vector8s value, Func<short, float> transformer)
		{
			return new Vector8f(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector8h Transform(Vector8s value, Func<short, Half> transformer)
		{
			return new Vector8h(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector8ul Transform(Vector8s value, Func<short, ulong> transformer)
		{
			return new Vector8ul(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector8l Transform(Vector8s value, Func<short, long> transformer)
		{
			return new Vector8l(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector8ui Transform(Vector8s value, Func<short, uint> transformer)
		{
			return new Vector8ui(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector8i Transform(Vector8s value, Func<short, int> transformer)
		{
			return new Vector8i(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector8us Transform(Vector8s value, Func<short, ushort> transformer)
		{
			return new Vector8us(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector8s Transform(Vector8s value, Func<short, short> transformer)
		{
			return new Vector8s(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector8b Transform(Vector8s value, Func<short, byte> transformer)
		{
			return new Vector8b(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector8sb Transform(Vector8s value, Func<short, sbyte> transformer)
		{
			return new Vector8sb(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		#endregion
		/// <summary>
		/// Multiplys the components of two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first vector to modulate.</param>
		/// <param name="right">The second vector to modulate.</param>
		/// <returns>The result of multiplying each component of left by the matching component in right.</returns>
		public static Vector8i Modulate(Vector8s left, Vector8s right)
		{
			return new Vector8i(left.V0 * right.V0, left.V1 * right.V1, left.V2 * right.V2, left.V3 * right.V3, left.V4 * right.V4, left.V5 * right.V5, left.V6 * right.V6, left.V7 * right.V7);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Vector8s Abs(Vector8s value)
		{
			return new Vector8s(Functions.Abs(value.V0), Functions.Abs(value.V1), Functions.Abs(value.V2), Functions.Abs(value.V3), Functions.Abs(value.V4), Functions.Abs(value.V5), Functions.Abs(value.V6), Functions.Abs(value.V7));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Vector8s Min(Vector8s value1, Vector8s value2)
		{
			return new Vector8s(Functions.Min(value1.V0, value2.V0), Functions.Min(value1.V1, value2.V1), Functions.Min(value1.V2, value2.V2), Functions.Min(value1.V3, value2.V3), Functions.Min(value1.V4, value2.V4), Functions.Min(value1.V5, value2.V5), Functions.Min(value1.V6, value2.V6), Functions.Min(value1.V7, value2.V7));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Vector8s Max(Vector8s value1, Vector8s value2)
		{
			return new Vector8s(Functions.Max(value1.V0, value2.V0), Functions.Max(value1.V1, value2.V1), Functions.Max(value1.V2, value2.V2), Functions.Max(value1.V3, value2.V3), Functions.Max(value1.V4, value2.V4), Functions.Max(value1.V5, value2.V5), Functions.Max(value1.V6, value2.V6), Functions.Max(value1.V7, value2.V7));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		public static Vector8s Clamp(Vector8s value, Vector8s min, Vector8s max)
		{
			return new Vector8s(Functions.Clamp(value.V0, min.V0, max.V0), Functions.Clamp(value.V1, min.V1, max.V1), Functions.Clamp(value.V2, min.V2, max.V2), Functions.Clamp(value.V3, min.V3, max.V3), Functions.Clamp(value.V4, min.V4, max.V4), Functions.Clamp(value.V5, min.V5, max.V5), Functions.Clamp(value.V6, min.V6, max.V6), Functions.Clamp(value.V7, min.V7, max.V7));
		}
		#endregion
	}
}
