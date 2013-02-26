using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a two component vector of sbytes, of the form (X, Y).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	[CLSCompliant(false)]
	public struct Vector2sb: IEquatable<Vector2sb>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector2sb"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector2sb Zero = new Vector2sb(0);
		/// <summary>
		/// Returns a new <see cref="Vector2sb"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector2sb One = new Vector2sb(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector2sb"/> (1, 0).
		/// </summary>
		public static readonly Vector2sb UnitX = new Vector2sb(1, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector2sb"/> (0, 1).
		/// </summary>
		public static readonly Vector2sb UnitY = new Vector2sb(0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the vector.
		/// </summary>
		public readonly sbyte X;
		/// <summary>
		/// The Y component of the vector.
		/// </summary>
		public readonly sbyte Y;
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
						return X;
					case 1:
						return Y;
					default:
						throw new IndexOutOfRangeException("Indices for Vector2sb run from 0 to 1, inclusive.");
				}
			}
		}
		public sbyte[] ToArray()
		{
			return new sbyte[]
			{
				X, Y
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the vector (X, X).
		/// </summary>
		public Vector2sb XX
		{
			get
			{
				return new Vector2sb(X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y).
		/// </summary>
		public Vector2sb XY
		{
			get
			{
				return new Vector2sb(X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X).
		/// </summary>
		public Vector2sb YX
		{
			get
			{
				return new Vector2sb(Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y).
		/// </summary>
		public Vector2sb YY
		{
			get
			{
				return new Vector2sb(Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X).
		/// </summary>
		public Vector3sb XXX
		{
			get
			{
				return new Vector3sb(X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y).
		/// </summary>
		public Vector3sb XXY
		{
			get
			{
				return new Vector3sb(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X).
		/// </summary>
		public Vector3sb XYX
		{
			get
			{
				return new Vector3sb(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y).
		/// </summary>
		public Vector3sb XYY
		{
			get
			{
				return new Vector3sb(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X).
		/// </summary>
		public Vector3sb YXX
		{
			get
			{
				return new Vector3sb(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y).
		/// </summary>
		public Vector3sb YXY
		{
			get
			{
				return new Vector3sb(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X).
		/// </summary>
		public Vector3sb YYX
		{
			get
			{
				return new Vector3sb(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y).
		/// </summary>
		public Vector3sb YYY
		{
			get
			{
				return new Vector3sb(Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, X).
		/// </summary>
		public Vector4sb XXXX
		{
			get
			{
				return new Vector4sb(X, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Y).
		/// </summary>
		public Vector4sb XXXY
		{
			get
			{
				return new Vector4sb(X, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, X).
		/// </summary>
		public Vector4sb XXYX
		{
			get
			{
				return new Vector4sb(X, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Y).
		/// </summary>
		public Vector4sb XXYY
		{
			get
			{
				return new Vector4sb(X, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, X).
		/// </summary>
		public Vector4sb XYXX
		{
			get
			{
				return new Vector4sb(X, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Y).
		/// </summary>
		public Vector4sb XYXY
		{
			get
			{
				return new Vector4sb(X, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, X).
		/// </summary>
		public Vector4sb XYYX
		{
			get
			{
				return new Vector4sb(X, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Y).
		/// </summary>
		public Vector4sb XYYY
		{
			get
			{
				return new Vector4sb(X, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, X).
		/// </summary>
		public Vector4sb YXXX
		{
			get
			{
				return new Vector4sb(Y, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Y).
		/// </summary>
		public Vector4sb YXXY
		{
			get
			{
				return new Vector4sb(Y, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, X).
		/// </summary>
		public Vector4sb YXYX
		{
			get
			{
				return new Vector4sb(Y, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Y).
		/// </summary>
		public Vector4sb YXYY
		{
			get
			{
				return new Vector4sb(Y, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, X).
		/// </summary>
		public Vector4sb YYXX
		{
			get
			{
				return new Vector4sb(Y, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Y).
		/// </summary>
		public Vector4sb YYXY
		{
			get
			{
				return new Vector4sb(Y, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, X).
		/// </summary>
		public Vector4sb YYYX
		{
			get
			{
				return new Vector4sb(Y, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Y).
		/// </summary>
		public Vector4sb YYYY
		{
			get
			{
				return new Vector4sb(Y, Y, Y, Y);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2sb"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector2sb(sbyte value)
		{
			X = value;
			Y = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2sb"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		public Vector2sb(sbyte x, sbyte y)
		{
			X = x;
			Y = y;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2sb"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector2sb(sbyte[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2sb"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector2sb(sbyte[] array, int offset)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[offset + 0];
			Y = array[offset + 1];
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The identity of value.</returns>
		public static Vector2i operator +(Vector2sb value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector2i operator -(Vector2sb value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector2i operator +(Vector2sb left, Vector2sb right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector2i operator -(Vector2sb left, Vector2sb right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector2i operator *(Vector2sb left, int right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector2i operator *(int left, Vector2sb right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector2i operator /(Vector2sb left, int right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector2d value to a Vector2sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2sb.</param>
		/// <returns>A Vector2sb that has all components equal to value.</returns>
		public static explicit operator Vector2sb(Vector2d value)
		{
			return new Vector2sb((sbyte)value.X, (sbyte)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2f value to a Vector2sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2sb.</param>
		/// <returns>A Vector2sb that has all components equal to value.</returns>
		public static explicit operator Vector2sb(Vector2f value)
		{
			return new Vector2sb((sbyte)value.X, (sbyte)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2h value to a Vector2sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2sb.</param>
		/// <returns>A Vector2sb that has all components equal to value.</returns>
		public static explicit operator Vector2sb(Vector2h value)
		{
			return new Vector2sb((sbyte)value.X, (sbyte)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2ul value to a Vector2sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2sb.</param>
		/// <returns>A Vector2sb that has all components equal to value.</returns>
		public static explicit operator Vector2sb(Vector2ul value)
		{
			return new Vector2sb((sbyte)value.X, (sbyte)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2l value to a Vector2sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2sb.</param>
		/// <returns>A Vector2sb that has all components equal to value.</returns>
		public static explicit operator Vector2sb(Vector2l value)
		{
			return new Vector2sb((sbyte)value.X, (sbyte)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2ui value to a Vector2sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2sb.</param>
		/// <returns>A Vector2sb that has all components equal to value.</returns>
		public static explicit operator Vector2sb(Vector2ui value)
		{
			return new Vector2sb((sbyte)value.X, (sbyte)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2i value to a Vector2sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2sb.</param>
		/// <returns>A Vector2sb that has all components equal to value.</returns>
		public static explicit operator Vector2sb(Vector2i value)
		{
			return new Vector2sb((sbyte)value.X, (sbyte)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2us value to a Vector2sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2sb.</param>
		/// <returns>A Vector2sb that has all components equal to value.</returns>
		public static explicit operator Vector2sb(Vector2us value)
		{
			return new Vector2sb((sbyte)value.X, (sbyte)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2s value to a Vector2sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2sb.</param>
		/// <returns>A Vector2sb that has all components equal to value.</returns>
		public static explicit operator Vector2sb(Vector2s value)
		{
			return new Vector2sb((sbyte)value.X, (sbyte)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2b value to a Vector2sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2sb.</param>
		/// <returns>A Vector2sb that has all components equal to value.</returns>
		public static explicit operator Vector2sb(Vector2b value)
		{
			return new Vector2sb((sbyte)value.X, (sbyte)value.Y);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector2sb"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Vector2sb"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector2sb"/> object, and its value
		/// is equal to the current <see cref="Vector2sb"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector2sb) { return Equals((Vector2sb)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector2sb other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector2sb left, Vector2sb right)
		{
			return left.X == right.X & left.Y == right.Y;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector2sb left, Vector2sb right)
		{
			return left.X != right.X | left.Y != right.Y;
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
			return String.Format("({0}, {1})", X.ToString(format, provider), Y.ToString(format, provider));
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
		/// Writes the given <see cref="Vector2sb"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector2sb vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
		}
		/// <summary>
		/// Reads a <see cref="Vector2sb"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector2sb ReadVector2sb(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector2sb(reader.ReadSByte(), reader.ReadSByte());
		}
		#endregion
		#region Pack
		public static ushort Pack(int xBits, int yBits, Vector2sb vector)
		{
			Contract.Requires(0 <= xBits && xBits <= 8, "xBits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 8, "yBits must be between 0 and 8 inclusive.");
			Contract.Requires(xBits + yBits <= 16);
			ulong x = (ulong)(vector.X) >> (16 - xBits);
			ulong y = (ulong)(vector.Y) >> (16 - yBits);
			y <<= xBits;
			return (ushort)(x | y);
		}
		public static ushort Pack(Vector2sb vector)
		{
			ulong x = (ulong)(vector.X) << 0;
			ulong y = (ulong)(vector.Y) << 8;
			return (ushort)(x | y);
		}
		public static Vector2sb Unpack(int xBits, int yBits, sbyte bits)
		{
			Contract.Requires(0 <= xBits && xBits <= 8, "xBits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 8, "yBits must be between 0 and 8 inclusive.");
			Contract.Requires(xBits + yBits <= 16);
			ulong x = (ulong)(bits);
			x &= ((1UL << xBits) - 1);
			ulong y = (ulong)(bits) >> (xBits);
			y &= ((1UL << yBits) - 1);
			return new Vector2sb((sbyte)x, (sbyte)y);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector2i Negative(Vector2sb value)
		{
			return new Vector2i(-value.X, -value.Y);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector2i Add(Vector2sb left, Vector2sb right)
		{
			return new Vector2i(left.X + right.X, left.Y + right.Y);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector2i Subtract(Vector2sb left, Vector2sb right)
		{
			return new Vector2i(left.X - right.X, left.Y - right.Y);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector2i Multiply(Vector2sb vector, int scalar)
		{
			return new Vector2i(vector.X * scalar, vector.Y * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector2i Divide(Vector2sb vector, int scalar)
		{
			return new Vector2i(vector.X / scalar, vector.Y / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Vector2sb left, Vector2sb right)
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
		public static int Dot(Vector2sb left, Vector2sb right)
		{
			return left.X * right.X + left.Y * right.Y;
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all components of a vector are non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if all components are non-zero; false otherwise.</returns>
		[CLSCompliant(false)]
		public static bool All(Vector2sb value)
		{
			return value.X != 0 && value.Y != 0;
		}
		/// <summary>
		/// Determines whether all components of a vector satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if every component of the vector passes the test in the specified
		/// predicate; otherwise, false.</returns>
		[CLSCompliant(false)]
		public static bool All(Vector2sb value, Predicate<sbyte> predicate)
		{
			return predicate(value.X) && predicate(value.Y);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		[CLSCompliant(false)]
		public static bool Any(Vector2sb value)
		{
			return value.X != 0 || value.Y != 0;
		}
		/// <summary>
		/// Determines whether any components of a vector satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if any component of the vector passes the test in the specified
		/// predicate; otherwise, false.</returns>
		[CLSCompliant(false)]
		public static bool Any(Vector2sb value, Predicate<sbyte> predicate)
		{
			return predicate(value.X) || predicate(value.Y);
		}
		#endregion
		#region Properties
		/// <summary>
		/// Computes the absolute squared value of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute squared value of value.</returns> 
		[CLSCompliant(false)]
		public static int AbsoluteSquared(Vector2sb value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		[CLSCompliant(false)]
		public static float Absolute(Vector2sb value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		[CLSCompliant(false)]
		public static Vector2f Normalize(Vector2sb value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Vector2sb.Zero;
			}
			else
			{
				return (Vector2f)value / absolute;
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
		public static Vector2d Transform(Vector2sb value, Func<sbyte, double> transformer)
		{
			return new Vector2d(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector2f Transform(Vector2sb value, Func<sbyte, float> transformer)
		{
			return new Vector2f(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector2h Transform(Vector2sb value, Func<sbyte, Half> transformer)
		{
			return new Vector2h(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector2ul Transform(Vector2sb value, Func<sbyte, ulong> transformer)
		{
			return new Vector2ul(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector2l Transform(Vector2sb value, Func<sbyte, long> transformer)
		{
			return new Vector2l(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector2ui Transform(Vector2sb value, Func<sbyte, uint> transformer)
		{
			return new Vector2ui(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector2i Transform(Vector2sb value, Func<sbyte, int> transformer)
		{
			return new Vector2i(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector2us Transform(Vector2sb value, Func<sbyte, ushort> transformer)
		{
			return new Vector2us(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector2s Transform(Vector2sb value, Func<sbyte, short> transformer)
		{
			return new Vector2s(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector2b Transform(Vector2sb value, Func<sbyte, byte> transformer)
		{
			return new Vector2b(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector2sb Transform(Vector2sb value, Func<sbyte, sbyte> transformer)
		{
			return new Vector2sb(transformer(value.X), transformer(value.Y));
		}
		#endregion
		/// <summary>
		/// Multiplys the components of two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first vector to modulate.</param>
		/// <param name="right">The second vector to modulate.</param>
		/// <returns>The result of multiplying each component of left by the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector2i Modulate(Vector2sb left, Vector2sb right)
		{
			return new Vector2i(left.X * right.X, left.Y * right.Y);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		[CLSCompliant(false)]
		public static Vector2sb Abs(Vector2sb value)
		{
			return new Vector2sb(Functions.Abs(value.X), Functions.Abs(value.Y));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector2sb Min(Vector2sb value1, Vector2sb value2)
		{
			return new Vector2sb(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector2sb Max(Vector2sb value1, Vector2sb value2)
		{
			return new Vector2sb(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		[CLSCompliant(false)]
		public static Vector2sb Clamp(Vector2sb value, Vector2sb min, Vector2sb max)
		{
			return new Vector2sb(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y));
		}
		#endregion
		#region Coordinate spaces
		/// <summary>
		/// Transforms a vector in cartesian coordinates to polar coordinates.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <returns>The polar coordinates of value.</returns>
		[CLSCompliant(false)]
		public static PolarCoordinate CartesianToPolar(Vector2sb value)
		{
			double theta = Functions.Atan2(value.Y, value.X);
			if (theta < 0)
				theta += 2 * Constants.PI;
			return new PolarCoordinate(
			     theta,
			     (double)Functions.Sqrt(value.X * value.X + value.Y * value.Y));
		}
		#endregion
	}
}
