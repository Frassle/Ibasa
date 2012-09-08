using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a eight component vector of sbytes.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	[CLSCompliant(false)]
	public struct Vector8sb: IEquatable<Vector8sb>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector8sb"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector8sb Zero = new Vector8sb(0);
		/// <summary>
		/// Returns a new <see cref="Vector8sb"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector8sb One = new Vector8sb(1);
		#endregion
		#region Fields
		/// <summary>
		/// The first component of the vector.
		/// </summary>
		public readonly sbyte V0;
		/// <summary>
		/// The second component of the vector.
		/// </summary>
		public readonly sbyte V1;
		/// <summary>
		/// The third component of the vector.
		/// </summary>
		public readonly sbyte V2;
		/// <summary>
		/// The fourth component of the vector.
		/// </summary>
		public readonly sbyte V3;
		/// <summary>
		/// The fifth component of the vector.
		/// </summary>
		public readonly sbyte V4;
		/// <summary>
		/// The sixth component of the vector.
		/// </summary>
		public readonly sbyte V5;
		/// <summary>
		/// The seventh component of the vector.
		/// </summary>
		public readonly sbyte V6;
		/// <summary>
		/// The eighth component of the vector.
		/// </summary>
		public readonly sbyte V7;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public sbyte this[int index]
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
						throw new IndexOutOfRangeException("Indices for Vector8sb run from 0 to 7, inclusive.");
				}
			}
		}
		public sbyte[] ToArray()
		{
			return new sbyte[]
			{
				V0, V1, V2, V3, V4, V5, V6, V7
			};
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector8sb"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector8sb(sbyte value)
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
		/// Initializes a new instance of the <see cref="Vector8sb"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the first 2 components</param>
		/// <param name="v2">Value for the V2 component of the vector.</param>
		/// <param name="v3">Value for the V3 component of the vector.</param>
		/// <param name="v4">Value for the V4 component of the vector.</param>
		/// <param name="v5">Value for the V5 component of the vector.</param>
		/// <param name="v6">Value for the V6 component of the vector.</param>
		/// <param name="v7">Value for the V7 component of the vector.</param>
		public Vector8sb(Vector2sb value, sbyte v2, sbyte v3, sbyte v4, sbyte v5, sbyte v6, sbyte v7, sbyte v8, sbyte v9)
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
		/// Initializes a new instance of the <see cref="Vector8sb"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the first 3 components</param>
		/// <param name="v3">Value for the V3 component of the vector.</param>
		/// <param name="v4">Value for the V4 component of the vector.</param>
		/// <param name="v5">Value for the V5 component of the vector.</param>
		/// <param name="v6">Value for the V6 component of the vector.</param>
		/// <param name="v7">Value for the V7 component of the vector.</param>
		public Vector8sb(Vector3sb value, sbyte v3, sbyte v4, sbyte v5, sbyte v6, sbyte v7, sbyte v8, sbyte v9, sbyte v10)
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
		/// Initializes a new instance of the <see cref="Vector8sb"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the first 4 components</param>
		/// <param name="v4">Value for the V4 component of the vector.</param>
		/// <param name="v5">Value for the V5 component of the vector.</param>
		/// <param name="v6">Value for the V6 component of the vector.</param>
		/// <param name="v7">Value for the V7 component of the vector.</param>
		public Vector8sb(Vector4sb value, sbyte v4, sbyte v5, sbyte v6, sbyte v7, sbyte v8, sbyte v9, sbyte v10, sbyte v11)
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
		/// Initializes a new instance of the <see cref="Vector8sb"/> using the specified values.
		/// </summary>
		/// <param name="v0">Value for the V0 component of the vector.</param>
		/// <param name="v1">Value for the V1 component of the vector.</param>
		/// <param name="v2">Value for the V2 component of the vector.</param>
		/// <param name="v3">Value for the V3 component of the vector.</param>
		/// <param name="v4">Value for the V4 component of the vector.</param>
		/// <param name="v5">Value for the V5 component of the vector.</param>
		/// <param name="v6">Value for the V6 component of the vector.</param>
		/// <param name="v7">Value for the V7 component of the vector.</param>
		public Vector8sb(sbyte v0, sbyte v1, sbyte v2, sbyte v3, sbyte v4, sbyte v5, sbyte v6, sbyte v7)
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
		/// Initializes a new instance of the <see cref="Vector8sb"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector8sb(sbyte[] array)
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
		/// Initializes a new instance of the <see cref="Vector8sb"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector8sb(sbyte[] array, int offset)
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
		public static Vector8i operator +(Vector8sb value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector8i operator -(Vector8sb value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector8i operator +(Vector8sb left, Vector8sb right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector8i operator -(Vector8sb left, Vector8sb right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector8i operator *(Vector8sb left, int right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector8i operator *(int left, Vector8sb right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector8i operator /(Vector8sb left, int right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector8d value to a Vector8sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8sb.</param>
		/// <returns>A Vector8sb that has all components equal to value.</returns>
		public static explicit operator Vector8sb(Vector8d value)
		{
			return new Vector8sb((sbyte)value.V0, (sbyte)value.V1, (sbyte)value.V2, (sbyte)value.V3, (sbyte)value.V4, (sbyte)value.V5, (sbyte)value.V6, (sbyte)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8f value to a Vector8sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8sb.</param>
		/// <returns>A Vector8sb that has all components equal to value.</returns>
		public static explicit operator Vector8sb(Vector8f value)
		{
			return new Vector8sb((sbyte)value.V0, (sbyte)value.V1, (sbyte)value.V2, (sbyte)value.V3, (sbyte)value.V4, (sbyte)value.V5, (sbyte)value.V6, (sbyte)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8h value to a Vector8sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8sb.</param>
		/// <returns>A Vector8sb that has all components equal to value.</returns>
		public static explicit operator Vector8sb(Vector8h value)
		{
			return new Vector8sb((sbyte)value.V0, (sbyte)value.V1, (sbyte)value.V2, (sbyte)value.V3, (sbyte)value.V4, (sbyte)value.V5, (sbyte)value.V6, (sbyte)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8ul value to a Vector8sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8sb.</param>
		/// <returns>A Vector8sb that has all components equal to value.</returns>
		public static explicit operator Vector8sb(Vector8ul value)
		{
			return new Vector8sb((sbyte)value.V0, (sbyte)value.V1, (sbyte)value.V2, (sbyte)value.V3, (sbyte)value.V4, (sbyte)value.V5, (sbyte)value.V6, (sbyte)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8l value to a Vector8sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8sb.</param>
		/// <returns>A Vector8sb that has all components equal to value.</returns>
		public static explicit operator Vector8sb(Vector8l value)
		{
			return new Vector8sb((sbyte)value.V0, (sbyte)value.V1, (sbyte)value.V2, (sbyte)value.V3, (sbyte)value.V4, (sbyte)value.V5, (sbyte)value.V6, (sbyte)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8ui value to a Vector8sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8sb.</param>
		/// <returns>A Vector8sb that has all components equal to value.</returns>
		public static explicit operator Vector8sb(Vector8ui value)
		{
			return new Vector8sb((sbyte)value.V0, (sbyte)value.V1, (sbyte)value.V2, (sbyte)value.V3, (sbyte)value.V4, (sbyte)value.V5, (sbyte)value.V6, (sbyte)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8i value to a Vector8sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8sb.</param>
		/// <returns>A Vector8sb that has all components equal to value.</returns>
		public static explicit operator Vector8sb(Vector8i value)
		{
			return new Vector8sb((sbyte)value.V0, (sbyte)value.V1, (sbyte)value.V2, (sbyte)value.V3, (sbyte)value.V4, (sbyte)value.V5, (sbyte)value.V6, (sbyte)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8us value to a Vector8sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8sb.</param>
		/// <returns>A Vector8sb that has all components equal to value.</returns>
		public static explicit operator Vector8sb(Vector8us value)
		{
			return new Vector8sb((sbyte)value.V0, (sbyte)value.V1, (sbyte)value.V2, (sbyte)value.V3, (sbyte)value.V4, (sbyte)value.V5, (sbyte)value.V6, (sbyte)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8s value to a Vector8sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8sb.</param>
		/// <returns>A Vector8sb that has all components equal to value.</returns>
		public static explicit operator Vector8sb(Vector8s value)
		{
			return new Vector8sb((sbyte)value.V0, (sbyte)value.V1, (sbyte)value.V2, (sbyte)value.V3, (sbyte)value.V4, (sbyte)value.V5, (sbyte)value.V6, (sbyte)value.V7);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector8b value to a Vector8sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector8sb.</param>
		/// <returns>A Vector8sb that has all components equal to value.</returns>
		public static explicit operator Vector8sb(Vector8b value)
		{
			return new Vector8sb((sbyte)value.V0, (sbyte)value.V1, (sbyte)value.V2, (sbyte)value.V3, (sbyte)value.V4, (sbyte)value.V5, (sbyte)value.V6, (sbyte)value.V7);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector8sb"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector8sb"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector8sb"/> object, and its value
		/// is equal to the current <see cref="Vector8sb"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector8sb) { return Equals((Vector8sb)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector8sb other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector8sb left, Vector8sb right)
		{
			return left.V0 == right.V0 & left.V1 == right.V1 & left.V2 == right.V2 & left.V3 == right.V3 & left.V4 == right.V4 & left.V5 == right.V5 & left.V6 == right.V6 & left.V7 == right.V7;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector8sb left, Vector8sb right)
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
		/// Writes the given <see cref="Vector8sb"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector8sb vector)
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
		/// Reads a <see cref="Vector8sb"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector8sb ReadVector8sb(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector8sb(reader.ReadSByte(), reader.ReadSByte(), reader.ReadSByte(), reader.ReadSByte(), reader.ReadSByte(), reader.ReadSByte(), reader.ReadSByte(), reader.ReadSByte());
		}
		#endregion
		#region Pack
		public static ulong Pack(int v0Bits, int v1Bits, int v2Bits, int v3Bits, int v4Bits, int v5Bits, int v6Bits, int v7Bits, Vector8sb vector)
		{
			Contract.Requires(0 <= v0Bits && v0Bits <= 8, "v0Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v1Bits && v1Bits <= 8, "v1Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v2Bits && v2Bits <= 8, "v2Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v3Bits && v3Bits <= 8, "v3Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v4Bits && v4Bits <= 8, "v4Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v5Bits && v5Bits <= 8, "v5Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v6Bits && v6Bits <= 8, "v6Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v7Bits && v7Bits <= 8, "v7Bits must be between 0 and 8 inclusive.");
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
			return (ulong)(v0 | v1 | v2 | v3 | v4 | v5 | v6 | v7);
		}
		public static ulong Pack(Vector8sb vector)
		{
			ulong v0 = (ulong)(vector.V0) << 0;
			ulong v1 = (ulong)(vector.V1) << 8;
			ulong v2 = (ulong)(vector.V2) << 16;
			ulong v3 = (ulong)(vector.V3) << 24;
			ulong v4 = (ulong)(vector.V4) << 32;
			ulong v5 = (ulong)(vector.V5) << 40;
			ulong v6 = (ulong)(vector.V6) << 48;
			ulong v7 = (ulong)(vector.V7) << 56;
			return (ulong)(v0 | v1 | v2 | v3 | v4 | v5 | v6 | v7);
		}
		public static Vector8sb Unpack(int v0Bits, int v1Bits, int v2Bits, int v3Bits, int v4Bits, int v5Bits, int v6Bits, int v7Bits, sbyte bits)
		{
			Contract.Requires(0 <= v0Bits && v0Bits <= 8, "v0Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v1Bits && v1Bits <= 8, "v1Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v2Bits && v2Bits <= 8, "v2Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v3Bits && v3Bits <= 8, "v3Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v4Bits && v4Bits <= 8, "v4Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v5Bits && v5Bits <= 8, "v5Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v6Bits && v6Bits <= 8, "v6Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v7Bits && v7Bits <= 8, "v7Bits must be between 0 and 8 inclusive.");
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
			return new Vector8sb((sbyte)v0, (sbyte)v1, (sbyte)v2, (sbyte)v3, (sbyte)v4, (sbyte)v5, (sbyte)v6, (sbyte)v7);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector8i Negative(Vector8sb value)
		{
			return new Vector8i(-value.V0, -value.V1, -value.V2, -value.V3, -value.V4, -value.V5, -value.V6, -value.V7);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector8i Add(Vector8sb left, Vector8sb right)
		{
			return new Vector8i(left.V0 + right.V0, left.V1 + right.V1, left.V2 + right.V2, left.V3 + right.V3, left.V4 + right.V4, left.V5 + right.V5, left.V6 + right.V6, left.V7 + right.V7);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector8i Subtract(Vector8sb left, Vector8sb right)
		{
			return new Vector8i(left.V0 - right.V0, left.V1 - right.V1, left.V2 - right.V2, left.V3 - right.V3, left.V4 - right.V4, left.V5 - right.V5, left.V6 - right.V6, left.V7 - right.V7);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector8i Multiply(Vector8sb vector, int scalar)
		{
			return new Vector8i(vector.V0 * scalar, vector.V1 * scalar, vector.V2 * scalar, vector.V3 * scalar, vector.V4 * scalar, vector.V5 * scalar, vector.V6 * scalar, vector.V7 * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector8i Divide(Vector8sb vector, int scalar)
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
		public static bool Equals(Vector8sb left, Vector8sb right)
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
		[CLSCompliant(false)]
		public static int Dot(Vector8sb left, Vector8sb right)
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
		[CLSCompliant(false)]
		public static bool All(Vector8sb value)
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
		[CLSCompliant(false)]
		public static bool All(Vector8sb value, Predicate<sbyte> predicate)
		{
			return predicate(value.V0) && predicate(value.V1) && predicate(value.V2) && predicate(value.V3) && predicate(value.V4) && predicate(value.V5) && predicate(value.V6) && predicate(value.V7);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		[CLSCompliant(false)]
		public static bool Any(Vector8sb value)
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
		[CLSCompliant(false)]
		public static bool Any(Vector8sb value, Predicate<sbyte> predicate)
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
		[CLSCompliant(false)]
		public static int AbsoluteSquared(Vector8sb value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		[CLSCompliant(false)]
		public static float Absolute(Vector8sb value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		[CLSCompliant(false)]
		public static Vector8f Normalize(Vector8sb value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Vector8sb.Zero;
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
		[CLSCompliant(false)]
		public static Vector8d Transform(Vector8sb value, Func<sbyte, double> transformer)
		{
			return new Vector8d(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector8f Transform(Vector8sb value, Func<sbyte, float> transformer)
		{
			return new Vector8f(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector8h Transform(Vector8sb value, Func<sbyte, Half> transformer)
		{
			return new Vector8h(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector8ul Transform(Vector8sb value, Func<sbyte, ulong> transformer)
		{
			return new Vector8ul(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector8l Transform(Vector8sb value, Func<sbyte, long> transformer)
		{
			return new Vector8l(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector8ui Transform(Vector8sb value, Func<sbyte, uint> transformer)
		{
			return new Vector8ui(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector8i Transform(Vector8sb value, Func<sbyte, int> transformer)
		{
			return new Vector8i(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector8us Transform(Vector8sb value, Func<sbyte, ushort> transformer)
		{
			return new Vector8us(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector8s Transform(Vector8sb value, Func<sbyte, short> transformer)
		{
			return new Vector8s(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector8b Transform(Vector8sb value, Func<sbyte, byte> transformer)
		{
			return new Vector8b(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector8sb Transform(Vector8sb value, Func<sbyte, sbyte> transformer)
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
		[CLSCompliant(false)]
		public static Vector8i Modulate(Vector8sb left, Vector8sb right)
		{
			return new Vector8i(left.V0 * right.V0, left.V1 * right.V1, left.V2 * right.V2, left.V3 * right.V3, left.V4 * right.V4, left.V5 * right.V5, left.V6 * right.V6, left.V7 * right.V7);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		[CLSCompliant(false)]
		public static Vector8sb Abs(Vector8sb value)
		{
			return new Vector8sb(Functions.Abs(value.V0), Functions.Abs(value.V1), Functions.Abs(value.V2), Functions.Abs(value.V3), Functions.Abs(value.V4), Functions.Abs(value.V5), Functions.Abs(value.V6), Functions.Abs(value.V7));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector8sb Min(Vector8sb value1, Vector8sb value2)
		{
			return new Vector8sb(Functions.Min(value1.V0, value2.V0), Functions.Min(value1.V1, value2.V1), Functions.Min(value1.V2, value2.V2), Functions.Min(value1.V3, value2.V3), Functions.Min(value1.V4, value2.V4), Functions.Min(value1.V5, value2.V5), Functions.Min(value1.V6, value2.V6), Functions.Min(value1.V7, value2.V7));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector8sb Max(Vector8sb value1, Vector8sb value2)
		{
			return new Vector8sb(Functions.Max(value1.V0, value2.V0), Functions.Max(value1.V1, value2.V1), Functions.Max(value1.V2, value2.V2), Functions.Max(value1.V3, value2.V3), Functions.Max(value1.V4, value2.V4), Functions.Max(value1.V5, value2.V5), Functions.Max(value1.V6, value2.V6), Functions.Max(value1.V7, value2.V7));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		[CLSCompliant(false)]
		public static Vector8sb Clamp(Vector8sb value, Vector8sb min, Vector8sb max)
		{
			return new Vector8sb(Functions.Clamp(value.V0, min.V0, max.V0), Functions.Clamp(value.V1, min.V1, max.V1), Functions.Clamp(value.V2, min.V2, max.V2), Functions.Clamp(value.V3, min.V3, max.V3), Functions.Clamp(value.V4, min.V4, max.V4), Functions.Clamp(value.V5, min.V5, max.V5), Functions.Clamp(value.V6, min.V6, max.V6), Functions.Clamp(value.V7, min.V7, max.V7));
		}
		#endregion
	}
}
