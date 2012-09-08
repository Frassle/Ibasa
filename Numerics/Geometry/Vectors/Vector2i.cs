using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a two component vector of ints, of the form (X, Y).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector2i: IEquatable<Vector2i>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector2i"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector2i Zero = new Vector2i(0);
		/// <summary>
		/// Returns a new <see cref="Vector2i"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector2i One = new Vector2i(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector2i"/> (1, 0).
		/// </summary>
		public static readonly Vector2i UnitX = new Vector2i(1, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector2i"/> (0, 1).
		/// </summary>
		public static readonly Vector2i UnitY = new Vector2i(0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the vector.
		/// </summary>
		public readonly int X;
		/// <summary>
		/// The Y component of the vector.
		/// </summary>
		public readonly int Y;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public int this[int index]
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
						throw new IndexOutOfRangeException("Indices for Vector2i run from 0 to 1, inclusive.");
				}
			}
		}
		public int[] ToArray()
		{
			return new int[]
			{
				X, Y
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the vector (X, X).
		/// </summary>
		public Vector2i XX
		{
			get
			{
				return new Vector2i(X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y).
		/// </summary>
		public Vector2i XY
		{
			get
			{
				return new Vector2i(X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X).
		/// </summary>
		public Vector2i YX
		{
			get
			{
				return new Vector2i(Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y).
		/// </summary>
		public Vector2i YY
		{
			get
			{
				return new Vector2i(Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X).
		/// </summary>
		public Vector3i XXX
		{
			get
			{
				return new Vector3i(X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y).
		/// </summary>
		public Vector3i XXY
		{
			get
			{
				return new Vector3i(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X).
		/// </summary>
		public Vector3i XYX
		{
			get
			{
				return new Vector3i(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y).
		/// </summary>
		public Vector3i XYY
		{
			get
			{
				return new Vector3i(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X).
		/// </summary>
		public Vector3i YXX
		{
			get
			{
				return new Vector3i(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y).
		/// </summary>
		public Vector3i YXY
		{
			get
			{
				return new Vector3i(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X).
		/// </summary>
		public Vector3i YYX
		{
			get
			{
				return new Vector3i(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y).
		/// </summary>
		public Vector3i YYY
		{
			get
			{
				return new Vector3i(Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, X).
		/// </summary>
		public Vector4i XXXX
		{
			get
			{
				return new Vector4i(X, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Y).
		/// </summary>
		public Vector4i XXXY
		{
			get
			{
				return new Vector4i(X, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, X).
		/// </summary>
		public Vector4i XXYX
		{
			get
			{
				return new Vector4i(X, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Y).
		/// </summary>
		public Vector4i XXYY
		{
			get
			{
				return new Vector4i(X, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, X).
		/// </summary>
		public Vector4i XYXX
		{
			get
			{
				return new Vector4i(X, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Y).
		/// </summary>
		public Vector4i XYXY
		{
			get
			{
				return new Vector4i(X, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, X).
		/// </summary>
		public Vector4i XYYX
		{
			get
			{
				return new Vector4i(X, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Y).
		/// </summary>
		public Vector4i XYYY
		{
			get
			{
				return new Vector4i(X, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, X).
		/// </summary>
		public Vector4i YXXX
		{
			get
			{
				return new Vector4i(Y, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Y).
		/// </summary>
		public Vector4i YXXY
		{
			get
			{
				return new Vector4i(Y, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, X).
		/// </summary>
		public Vector4i YXYX
		{
			get
			{
				return new Vector4i(Y, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Y).
		/// </summary>
		public Vector4i YXYY
		{
			get
			{
				return new Vector4i(Y, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, X).
		/// </summary>
		public Vector4i YYXX
		{
			get
			{
				return new Vector4i(Y, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Y).
		/// </summary>
		public Vector4i YYXY
		{
			get
			{
				return new Vector4i(Y, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, X).
		/// </summary>
		public Vector4i YYYX
		{
			get
			{
				return new Vector4i(Y, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Y).
		/// </summary>
		public Vector4i YYYY
		{
			get
			{
				return new Vector4i(Y, Y, Y, Y);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2i"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector2i(int value)
		{
			X = value;
			Y = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2i"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		public Vector2i(int x, int y)
		{
			X = x;
			Y = y;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2i"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector2i(int[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2i"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector2i(int[] array, int offset)
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
		public static Vector2i operator +(Vector2i value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector2i operator -(Vector2i value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector2i operator +(Vector2i left, Vector2i right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector2i operator -(Vector2i left, Vector2i right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector2i operator *(Vector2i left, int right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector2i operator *(int left, Vector2i right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector2i operator /(Vector2i left, int right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector2d value to a Vector2i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2i.</param>
		/// <returns>A Vector2i that has all components equal to value.</returns>
		public static explicit operator Vector2i(Vector2d value)
		{
			return new Vector2i((int)value.X, (int)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2f value to a Vector2i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2i.</param>
		/// <returns>A Vector2i that has all components equal to value.</returns>
		public static explicit operator Vector2i(Vector2f value)
		{
			return new Vector2i((int)value.X, (int)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2h value to a Vector2i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2i.</param>
		/// <returns>A Vector2i that has all components equal to value.</returns>
		public static explicit operator Vector2i(Vector2h value)
		{
			return new Vector2i((int)value.X, (int)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2ul value to a Vector2i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2i.</param>
		/// <returns>A Vector2i that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector2i(Vector2ul value)
		{
			return new Vector2i((int)value.X, (int)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2l value to a Vector2i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2i.</param>
		/// <returns>A Vector2i that has all components equal to value.</returns>
		public static explicit operator Vector2i(Vector2l value)
		{
			return new Vector2i((int)value.X, (int)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2ui value to a Vector2i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2i.</param>
		/// <returns>A Vector2i that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector2i(Vector2ui value)
		{
			return new Vector2i((int)value.X, (int)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2us value to a Vector2i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2i.</param>
		/// <returns>A Vector2i that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector2i(Vector2us value)
		{
			return new Vector2i((int)value.X, (int)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2s value to a Vector2i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2i.</param>
		/// <returns>A Vector2i that has all components equal to value.</returns>
		public static implicit operator Vector2i(Vector2s value)
		{
			return new Vector2i((int)value.X, (int)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2b value to a Vector2i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2i.</param>
		/// <returns>A Vector2i that has all components equal to value.</returns>
		public static implicit operator Vector2i(Vector2b value)
		{
			return new Vector2i((int)value.X, (int)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2sb value to a Vector2i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2i.</param>
		/// <returns>A Vector2i that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector2i(Vector2sb value)
		{
			return new Vector2i((int)value.X, (int)value.Y);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector2i"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector2i"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector2i"/> object, and its value
		/// is equal to the current <see cref="Vector2i"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector2i) { return Equals((Vector2i)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector2i other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector2i left, Vector2i right)
		{
			return left.X == right.X & left.Y == right.Y;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector2i left, Vector2i right)
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
		/// Writes the given <see cref="Vector2i"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector2i vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
		}
		/// <summary>
		/// Reads a <see cref="Vector2i"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector2i ReadVector2i(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector2i(reader.ReadInt32(), reader.ReadInt32());
		}
		#endregion
		#region Pack
		public static long Pack(int xBits, int yBits, Vector2i vector)
		{
			Contract.Requires(0 <= xBits && xBits <= 32, "xBits must be between 0 and 32 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 32, "yBits must be between 0 and 32 inclusive.");
			Contract.Requires(xBits + yBits <= 64);
			ulong x = (ulong)(vector.X) >> (64 - xBits);
			ulong y = (ulong)(vector.Y) >> (64 - yBits);
			y <<= xBits;
			return (long)(x | y);
		}
		public static long Pack(Vector2i vector)
		{
			ulong x = (ulong)(vector.X) << 0;
			ulong y = (ulong)(vector.Y) << 32;
			return (long)(x | y);
		}
		public static Vector2i Unpack(int xBits, int yBits, int bits)
		{
			Contract.Requires(0 <= xBits && xBits <= 32, "xBits must be between 0 and 32 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 32, "yBits must be between 0 and 32 inclusive.");
			Contract.Requires(xBits + yBits <= 64);
			ulong x = (ulong)(bits);
			x &= ((1UL << xBits) - 1);
			ulong y = (ulong)(bits) >> (xBits);
			y &= ((1UL << yBits) - 1);
			return new Vector2i((int)x, (int)y);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector2i Negative(Vector2i value)
		{
			return new Vector2i(-value.X, -value.Y);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector2i Add(Vector2i left, Vector2i right)
		{
			return new Vector2i(left.X + right.X, left.Y + right.Y);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector2i Subtract(Vector2i left, Vector2i right)
		{
			return new Vector2i(left.X - right.X, left.Y - right.Y);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector2i Multiply(Vector2i vector, int scalar)
		{
			return new Vector2i(vector.X * scalar, vector.Y * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector2i Divide(Vector2i vector, int scalar)
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
		public static bool Equals(Vector2i left, Vector2i right)
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
		public static int Dot(Vector2i left, Vector2i right)
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
		public static bool All(Vector2i value)
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
		public static bool All(Vector2i value, Predicate<int> predicate)
		{
			return predicate(value.X) && predicate(value.Y);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Vector2i value)
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
		public static bool Any(Vector2i value, Predicate<int> predicate)
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
		public static int AbsoluteSquared(Vector2i value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		public static float Absolute(Vector2i value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		public static Vector2f Normalize(Vector2i value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Vector2i.Zero;
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
		public static Vector2d Transform(Vector2i value, Func<int, double> transformer)
		{
			return new Vector2d(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2f Transform(Vector2i value, Func<int, float> transformer)
		{
			return new Vector2f(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2h Transform(Vector2i value, Func<int, Half> transformer)
		{
			return new Vector2h(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2ul Transform(Vector2i value, Func<int, ulong> transformer)
		{
			return new Vector2ul(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2l Transform(Vector2i value, Func<int, long> transformer)
		{
			return new Vector2l(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2ui Transform(Vector2i value, Func<int, uint> transformer)
		{
			return new Vector2ui(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2i Transform(Vector2i value, Func<int, int> transformer)
		{
			return new Vector2i(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2us Transform(Vector2i value, Func<int, ushort> transformer)
		{
			return new Vector2us(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2s Transform(Vector2i value, Func<int, short> transformer)
		{
			return new Vector2s(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2b Transform(Vector2i value, Func<int, byte> transformer)
		{
			return new Vector2b(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2sb Transform(Vector2i value, Func<int, sbyte> transformer)
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
		public static Vector2i Modulate(Vector2i left, Vector2i right)
		{
			return new Vector2i(left.X * right.X, left.Y * right.Y);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Vector2i Abs(Vector2i value)
		{
			return new Vector2i(Functions.Abs(value.X), Functions.Abs(value.Y));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Vector2i Min(Vector2i value1, Vector2i value2)
		{
			return new Vector2i(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Vector2i Max(Vector2i value1, Vector2i value2)
		{
			return new Vector2i(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		public static Vector2i Clamp(Vector2i value, Vector2i min, Vector2i max)
		{
			return new Vector2i(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y));
		}
		#endregion
	}
}
