using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a two component vector of doubles, of the form (X, Y).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector2d: IEquatable<Vector2d>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector2d"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector2d Zero = new Vector2d(0);
		/// <summary>
		/// Returns a new <see cref="Vector2d"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector2d One = new Vector2d(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector2d"/> (1, 0).
		/// </summary>
		public static readonly Vector2d UnitX = new Vector2d(1, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector2d"/> (0, 1).
		/// </summary>
		public static readonly Vector2d UnitY = new Vector2d(0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the vector.
		/// </summary>
		public readonly double X;
		/// <summary>
		/// The Y component of the vector.
		/// </summary>
		public readonly double Y;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public double this[int index]
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
						throw new IndexOutOfRangeException("Indices for Vector2d run from 0 to 1, inclusive.");
				}
			}
		}
		public double[] ToArray()
		{
			return new double[]
			{
				X, Y
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the vector (X, X).
		/// </summary>
		public Vector2d XX
		{
			get
			{
				return new Vector2d(X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y).
		/// </summary>
		public Vector2d XY
		{
			get
			{
				return new Vector2d(X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X).
		/// </summary>
		public Vector2d YX
		{
			get
			{
				return new Vector2d(Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y).
		/// </summary>
		public Vector2d YY
		{
			get
			{
				return new Vector2d(Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X).
		/// </summary>
		public Vector3d XXX
		{
			get
			{
				return new Vector3d(X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y).
		/// </summary>
		public Vector3d XXY
		{
			get
			{
				return new Vector3d(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X).
		/// </summary>
		public Vector3d XYX
		{
			get
			{
				return new Vector3d(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y).
		/// </summary>
		public Vector3d XYY
		{
			get
			{
				return new Vector3d(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X).
		/// </summary>
		public Vector3d YXX
		{
			get
			{
				return new Vector3d(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y).
		/// </summary>
		public Vector3d YXY
		{
			get
			{
				return new Vector3d(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X).
		/// </summary>
		public Vector3d YYX
		{
			get
			{
				return new Vector3d(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y).
		/// </summary>
		public Vector3d YYY
		{
			get
			{
				return new Vector3d(Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, X).
		/// </summary>
		public Vector4d XXXX
		{
			get
			{
				return new Vector4d(X, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Y).
		/// </summary>
		public Vector4d XXXY
		{
			get
			{
				return new Vector4d(X, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, X).
		/// </summary>
		public Vector4d XXYX
		{
			get
			{
				return new Vector4d(X, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Y).
		/// </summary>
		public Vector4d XXYY
		{
			get
			{
				return new Vector4d(X, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, X).
		/// </summary>
		public Vector4d XYXX
		{
			get
			{
				return new Vector4d(X, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Y).
		/// </summary>
		public Vector4d XYXY
		{
			get
			{
				return new Vector4d(X, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, X).
		/// </summary>
		public Vector4d XYYX
		{
			get
			{
				return new Vector4d(X, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Y).
		/// </summary>
		public Vector4d XYYY
		{
			get
			{
				return new Vector4d(X, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, X).
		/// </summary>
		public Vector4d YXXX
		{
			get
			{
				return new Vector4d(Y, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Y).
		/// </summary>
		public Vector4d YXXY
		{
			get
			{
				return new Vector4d(Y, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, X).
		/// </summary>
		public Vector4d YXYX
		{
			get
			{
				return new Vector4d(Y, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Y).
		/// </summary>
		public Vector4d YXYY
		{
			get
			{
				return new Vector4d(Y, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, X).
		/// </summary>
		public Vector4d YYXX
		{
			get
			{
				return new Vector4d(Y, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Y).
		/// </summary>
		public Vector4d YYXY
		{
			get
			{
				return new Vector4d(Y, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, X).
		/// </summary>
		public Vector4d YYYX
		{
			get
			{
				return new Vector4d(Y, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Y).
		/// </summary>
		public Vector4d YYYY
		{
			get
			{
				return new Vector4d(Y, Y, Y, Y);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2d"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector2d(double value)
		{
			X = value;
			Y = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2d"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		public Vector2d(double x, double y)
		{
			X = x;
			Y = y;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2d"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector2d(double[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2d"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector2d(double[] array, int offset)
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
		public static Vector2d operator +(Vector2d value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector2d operator -(Vector2d value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector2d operator +(Vector2d left, Vector2d right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector2d operator -(Vector2d left, Vector2d right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector2d operator *(Vector2d left, double right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector2d operator *(double left, Vector2d right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector2d operator /(Vector2d left, double right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a Vector2f value to a Vector2d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2d.</param>
		/// <returns>A Vector2d that has all components equal to value.</returns>
		public static implicit operator Vector2d(Vector2f value)
		{
			return new Vector2d((double)value.X, (double)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2h value to a Vector2d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2d.</param>
		/// <returns>A Vector2d that has all components equal to value.</returns>
		public static implicit operator Vector2d(Vector2h value)
		{
			return new Vector2d((double)value.X, (double)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2ul value to a Vector2d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2d.</param>
		/// <returns>A Vector2d that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector2d(Vector2ul value)
		{
			return new Vector2d((double)value.X, (double)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2l value to a Vector2d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2d.</param>
		/// <returns>A Vector2d that has all components equal to value.</returns>
		public static implicit operator Vector2d(Vector2l value)
		{
			return new Vector2d((double)value.X, (double)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2ui value to a Vector2d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2d.</param>
		/// <returns>A Vector2d that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector2d(Vector2ui value)
		{
			return new Vector2d((double)value.X, (double)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2i value to a Vector2d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2d.</param>
		/// <returns>A Vector2d that has all components equal to value.</returns>
		public static implicit operator Vector2d(Vector2i value)
		{
			return new Vector2d((double)value.X, (double)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2us value to a Vector2d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2d.</param>
		/// <returns>A Vector2d that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector2d(Vector2us value)
		{
			return new Vector2d((double)value.X, (double)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2s value to a Vector2d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2d.</param>
		/// <returns>A Vector2d that has all components equal to value.</returns>
		public static implicit operator Vector2d(Vector2s value)
		{
			return new Vector2d((double)value.X, (double)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2b value to a Vector2d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2d.</param>
		/// <returns>A Vector2d that has all components equal to value.</returns>
		public static implicit operator Vector2d(Vector2b value)
		{
			return new Vector2d((double)value.X, (double)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector2sb value to a Vector2d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2d.</param>
		/// <returns>A Vector2d that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector2d(Vector2sb value)
		{
			return new Vector2d((double)value.X, (double)value.Y);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector2d"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector2d"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector2d"/> object, and its value
		/// is equal to the current <see cref="Vector2d"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector2d) { return Equals((Vector2d)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector2d other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector2d left, Vector2d right)
		{
			return left.X == right.X & left.Y == right.Y;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector2d left, Vector2d right)
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
		/// Writes the given <see cref="Vector2d"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector2d vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
		}
		/// <summary>
		/// Reads a <see cref="Vector2d"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector2d ReadVector2d(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector2d(reader.ReadDouble(), reader.ReadDouble());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector2d Negative(Vector2d value)
		{
			return new Vector2d(-value.X, -value.Y);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector2d Add(Vector2d left, Vector2d right)
		{
			return new Vector2d(left.X + right.X, left.Y + right.Y);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector2d Subtract(Vector2d left, Vector2d right)
		{
			return new Vector2d(left.X - right.X, left.Y - right.Y);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector2d Multiply(Vector2d vector, double scalar)
		{
			return new Vector2d(vector.X * scalar, vector.Y * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector2d Divide(Vector2d vector, double scalar)
		{
			return new Vector2d(vector.X / scalar, vector.Y / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Vector2d left, Vector2d right)
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
		public static double Dot(Vector2d left, Vector2d right)
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
		public static bool All(Vector2d value)
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
		public static bool All(Vector2d value, Predicate<double> predicate)
		{
			return predicate(value.X) && predicate(value.Y);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Vector2d value)
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
		public static bool Any(Vector2d value, Predicate<double> predicate)
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
		public static double AbsoluteSquared(Vector2d value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		public static double Absolute(Vector2d value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		public static Vector2d Normalize(Vector2d value)
		{
			var absolute = Absolute(value);
			if(absolute <= double.Epsilon)
			{
				return Vector2d.Zero;
			}
			else
			{
				return (Vector2d)value / absolute;
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
		public static Vector2d Transform(Vector2d value, Func<double, double> transformer)
		{
			return new Vector2d(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2f Transform(Vector2d value, Func<double, float> transformer)
		{
			return new Vector2f(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2h Transform(Vector2d value, Func<double, Half> transformer)
		{
			return new Vector2h(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2ul Transform(Vector2d value, Func<double, ulong> transformer)
		{
			return new Vector2ul(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2l Transform(Vector2d value, Func<double, long> transformer)
		{
			return new Vector2l(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2ui Transform(Vector2d value, Func<double, uint> transformer)
		{
			return new Vector2ui(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2i Transform(Vector2d value, Func<double, int> transformer)
		{
			return new Vector2i(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2us Transform(Vector2d value, Func<double, ushort> transformer)
		{
			return new Vector2us(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2s Transform(Vector2d value, Func<double, short> transformer)
		{
			return new Vector2s(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2b Transform(Vector2d value, Func<double, byte> transformer)
		{
			return new Vector2b(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector2sb Transform(Vector2d value, Func<double, sbyte> transformer)
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
		public static Vector2d Modulate(Vector2d left, Vector2d right)
		{
			return new Vector2d(left.X * right.X, left.Y * right.Y);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Vector2d Abs(Vector2d value)
		{
			return new Vector2d(Functions.Abs(value.X), Functions.Abs(value.Y));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Vector2d Min(Vector2d value1, Vector2d value2)
		{
			return new Vector2d(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Vector2d Max(Vector2d value1, Vector2d value2)
		{
			return new Vector2d(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		public static Vector2d Clamp(Vector2d value, Vector2d min, Vector2d max)
		{
			return new Vector2d(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y));
		}
		/// <summary>
		/// Constrains each component to the range 0 to 1.
		/// </summary>
		/// <param name="value">A vector to saturate.</param>
		/// <returns>A vector with each component constrained to the range 0 to 1.</returns>
		public static Vector2d Saturate(Vector2d value)
		{
			return new Vector2d(Functions.Saturate(value.X), Functions.Saturate(value.Y));
		}
		/// <summary>
		/// Returns a vector where each component is the smallest integral value that
		/// is greater than or equal to the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The ceiling of value.</returns>
		public static Vector2d Ceiling(Vector2d value)
		{
			return new Vector2d(Functions.Ceiling(value.X), Functions.Ceiling(value.Y));
		}
		/// <summary>
		/// Returns a vector where each component is the largest integral value that
		/// is less than or equal to the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The floor of value.</returns>
		public static Vector2d Floor(Vector2d value)
		{
			return new Vector2d(Functions.Floor(value.X), Functions.Floor(value.Y));
		}
		/// <summary>
		/// Returns a vector where each component is the integral part of the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The integral of value.</returns>
		public static Vector2d Truncate(Vector2d value)
		{
			return new Vector2d(Functions.Truncate(value.X), Functions.Truncate(value.Y));
		}
		/// <summary>
		/// Returns a vector where each component is the fractional part of the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The fractional of value.</returns>
		public static Vector2d Fractional(Vector2d value)
		{
			return new Vector2d(Functions.Fractional(value.X), Functions.Fractional(value.Y));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector2d Round(Vector2d value)
		{
			return new Vector2d(Functions.Round(value.X), Functions.Round(value.Y));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector2d Round(Vector2d value, int digits)
		{
			return new Vector2d(Functions.Round(value.X, digits), Functions.Round(value.Y, digits));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector2d Round(Vector2d value, MidpointRounding mode)
		{
			return new Vector2d(Functions.Round(value.X, mode), Functions.Round(value.Y, mode));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector2d Round(Vector2d value, int digits, MidpointRounding mode)
		{
			return new Vector2d(Functions.Round(value.X, digits, mode), Functions.Round(value.Y, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each component in the vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>A vector with the reciprocal of each of values components.</returns>
		public static Vector2d Reciprocal(Vector2d value)
		{
			return new Vector2d(1 / value.X, 1 / value.Y);
		}
		#endregion
		#region Coordinate spaces
		/// <summary>
		/// Transforms a vector in cartesian coordinates to polar coordinates.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <returns>The polar coordinates of value.</returns>
		public static PolarCoordinate CartesianToPolar(Vector2d value)
		{
			double theta = Functions.Atan2(value.Y, value.X);
			if (theta < 0)
				theta += 2 * Constants.PI;
			return new PolarCoordinate(
			     theta,
			     (double)Functions.Sqrt(value.X * value.X + value.Y * value.Y));
		}
		/// <summary>
		/// Transforms a vector in polar coordinates to cartesian coordinates.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <returns>The cartesian coordinates of value.</returns>
		public static Vector2d PolarToCartesian(PolarCoordinate value)
		{
			return new Vector2d(
			     value.Rho * Functions.Cos(value.Theta), value.Rho * Functions.Sin(value.Theta));
		}
		#endregion
		#region Barycentric, Reflect, Refract
		/// <summary>
		/// Returns the Cartesian coordinate for one axis of a point that is defined
		/// by a given triangle and two normalized barycentric (areal) coordinates.
		/// </summary>
		/// <param name="value1">The coordinate of vertex 1 of the defining triangle.</param>
		/// <param name="value2">The coordinate of vertex 2 of the defining triangle.</param>
		/// <param name="value3">The coordinate of vertex 3 of the defining triangle.</param>
		/// <param name="amount1">The normalized barycentric (areal) coordinate b2, equal to the weighting
		/// factor for vertex 2, the coordinate of which is specified in value2.</param>
		/// <param name="amount2">The normalized barycentric (areal) coordinate b3, equal to the weighting
		/// factor for vertex 3, the coordinate of which is specified in value3.</param>
		/// <returns>Cartesian coordinate of the specified point.</returns>
		public static Vector2d Barycentric(Vector2d value1, Vector2d value2, Vector2d value3, double amount1, double amount2)
		{
			return ((1 - amount1 - amount2) * value1) + (amount1 * value2) + (amount2 * value3);
		}
		/// <summary>
		/// Returns the reflection of a vector off a surface that has the specified normal.
		/// </summary>
		/// <param name="vector">The source vector.</param>
		/// <param name="normal">Normal of the surface.</param>
		/// <returns>The reflected vector.</returns>
		/// <remarks>Reflect only gives the direction of a reflection off a surface, it does not determine
		/// whether the original vector was close enough to the surface to hit it.</remarks>
		public static Vector2d Reflect(Vector2d vector, Vector2d normal)
		{
			return vector - ((2 * Dot(vector, normal)) * normal);
		}
		/// <summary>
		/// Returns the refraction of a vector off a surface that has the specified normal, and refractive index.
		/// </summary>
		/// <param name="vector">The source vector.</param>
		/// <param name="normal">Normal of the surface.</param>
		/// <param name="index">The refractive index, destination index over source index.</param>
		/// <returns>The refracted vector.</returns>
		/// <remarks>Refract only gives the direction of a refraction off a surface, it does not determine
		/// whether the original vector was close enough to the surface to hit it.</remarks>
		public static Vector2d Refract(Vector2d vector, Vector2d normal, double index)
		{
			var cos1 = Dot(vector, normal);
			var radicand = 1 - (index * index) * (1 - (cos1 * cos1));
			if (radicand < 0)
			{
				return Vector2d.Zero;
			}
			return (index * vector) + ((Functions.Sqrt(radicand) - index * cos1) * normal);
		}
		#endregion
		#region Interpolation
		/// <summary>
		/// Performs a linear interpolation between two values.
		/// </summary>
		/// <param name="value1">First value.</param>
		/// <param name="value2">Second value.</param>
		/// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="value2"/>.</param>
		/// <returns>The linear interpolation of the two values.</returns>
		public static Vector2d Lerp(Vector2d value1, Vector2d value2, double amount)
		{
			return (1 - amount) * value1 + amount * value2;
		}
		#endregion
	}
}
