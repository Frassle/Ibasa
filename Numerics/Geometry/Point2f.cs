using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents an ordered pair of real x and y coordinates that defines a
	/// point in a two-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Point2f: IEquatable<Point2f>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns the point (0, 0).
		/// </summary>
		public static readonly Point2f Zero = new Point2f(0, 0);
		#endregion
		#region Fields
		/// <summary>
		/// The X coordinate of the point.
		/// </summary>
		public readonly float X;
		/// <summary>
		/// The Y coordinate of the point.
		/// </summary>
		public readonly float Y;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed coordinate of this point.
		/// </summary>
		/// <param name="index">The index of the coordinate.</param>
		/// <returns>The value of the indexed coordinate.</returns>
		public float this[int index]
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
						throw new IndexOutOfRangeException("Indices for Point2f run from 0 to 1, inclusive.");
				}
			}
		}
		public float[] ToArray()
		{
			return new float[]
			{
				X, Y
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the point (X, X).
		/// </summary>
		public Point2f XX
		{
			get
			{
				return new Point2f(X, X);
			}
		}
		/// <summary>
		/// Returns the point (X, Y).
		/// </summary>
		public Point2f XY
		{
			get
			{
				return new Point2f(X, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, X).
		/// </summary>
		public Point2f YX
		{
			get
			{
				return new Point2f(Y, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y).
		/// </summary>
		public Point2f YY
		{
			get
			{
				return new Point2f(Y, Y);
			}
		}
		/// <summary>
		/// Returns the point (X, X, X).
		/// </summary>
		public Point3f XXX
		{
			get
			{
				return new Point3f(X, X, X);
			}
		}
		/// <summary>
		/// Returns the point (X, X, Y).
		/// </summary>
		public Point3f XXY
		{
			get
			{
				return new Point3f(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the point (X, Y, X).
		/// </summary>
		public Point3f XYX
		{
			get
			{
				return new Point3f(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the point (X, Y, Y).
		/// </summary>
		public Point3f XYY
		{
			get
			{
				return new Point3f(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, X, X).
		/// </summary>
		public Point3f YXX
		{
			get
			{
				return new Point3f(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, X, Y).
		/// </summary>
		public Point3f YXY
		{
			get
			{
				return new Point3f(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y, X).
		/// </summary>
		public Point3f YYX
		{
			get
			{
				return new Point3f(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y, Y).
		/// </summary>
		public Point3f YYY
		{
			get
			{
				return new Point3f(Y, Y, Y);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Point2f"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X coordinate of the point.</param>
		/// <param name="y">Value for the Y coordinate of the point.</param>
		public Point2f(float x, float y)
		{
			X = x;
			Y = y;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point2f"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		public Point2f(float[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point2f"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Point2f(float[] array, int offset)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[offset + 0];
			Y = array[offset + 1];
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified point.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The identity of value.</returns>
		public static Point2f operator +(Point2f value)
		{
			return value;
		}
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point2f operator +(Point2f point, Vector2f vector)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Adds a vector and a point and returns the result.
		/// </summary>
		/// <param name="vector">The vector value to add.</param>
		/// <param name="point">The point value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point2f operator +(Vector2f vector, Point2f point)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Subtracts one point from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector2f operator -(Point2f left, Point2f right)
		{
			return Point.Subtract(left, right);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point2f operator -(Point2f point, Vector2f vector)
		{
			return Point.Subtract(point, vector);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="left">The point to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point2f operator *(Point2f left, float right)
		{
			return Point.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and point.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The point to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point2f operator *(float left, Point2f right)
		{
			return Point.Multiply(right, left);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The point to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point2f operator /(Point2f left, float right)
		{
			return Point.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Point2d value to a Point2f.
		/// </summary>
		/// <param name="value">The value to convert to a Point2f.</param>
		/// <returns>A Point2f that has all components equal to value.</returns>
		public static explicit operator Point2f(Point2d value)
		{
			return new Point2f((float)value.X, (float)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Point2l value to a Point2f.
		/// </summary>
		/// <param name="value">The value to convert to a Point2f.</param>
		/// <returns>A Point2f that has all components equal to value.</returns>
		public static implicit operator Point2f(Point2l value)
		{
			return new Point2f((float)value.X, (float)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Point2i value to a Point2f.
		/// </summary>
		/// <param name="value">The value to convert to a Point2f.</param>
		/// <returns>A Point2f that has all components equal to value.</returns>
		public static implicit operator Point2f(Point2i value)
		{
			return new Point2f((float)value.X, (float)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2f value to a Point2f.
		/// </summary>
		/// <param name="value">The value to convert to a Point2f.</param>
		/// <returns>A Point2f that has all components equal to value.</returns>
		public static explicit operator Point2f(Vector2f value)
		{
			return new Point2f(value.X, value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Point2f value to a Vector2f.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2f.</param>
		/// <returns>A Vector2f that has all components equal to value.</returns>
		public static explicit operator Vector2f(Point2f value)
		{
			return new Vector2f(value.X, value.Y);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Point2f"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Point2f"/> object or a type capable
		/// of implicit conversion to a <see cref="Point2f"/> object, and its value
		/// is equal to the current <see cref="Point2f"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Point2f) { return Equals((Point2f)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// point have the same value.
		/// </summary>
		/// <param name="other">The point to compare.</param>
		/// <returns>true if this point and value have the same value; otherwise, false.</returns>
		public bool Equals(Point2f other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Point2f left, Point2f right)
		{
			return left.X == right.X & left.Y == right.Y;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are not equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Point2f left, Point2f right)
		{
			return left.X != right.X | left.Y != right.Y;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current point to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current point to its equivalent string
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
		/// Converts the value of the current point to its equivalent string
		/// representation in Cartesian form by using the specified format for its coordinates.
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
		/// Converts the value of the current point to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its coordinates.
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
	/// Provides static methods for point functions.
	/// </summary>
	public static partial class Point
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Point2f"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Point2f point)
		{
			writer.Write(point.X);
			writer.Write(point.Y);
		}
		/// <summary>
		/// Reads a <see cref="Point2f"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Point2f ReadPoint2f(this Ibasa.IO.BinaryReader reader)
		{
			return new Point2f(reader.ReadSingle(), reader.ReadSingle());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point2f Add(Point2f point, Vector2f vector)
		{
			return new Point2f(point.X + vector.X, point.Y + vector.Y);
		}
		/// <summary>
		/// Subtracts one points from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector2f Subtract(Point2f left, Point2f right)
		{
			return new Vector2f(left.X - right.X, left.Y - right.Y);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point2f Subtract(Point2f point, Vector2f vector)
		{
			return new Point2f(point.X - vector.X, point.Y - vector.Y);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="point">The point to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point2f Multiply(Point2f point, float scalar)
		{
			return new Point2f(point.X * scalar, point.Y * scalar);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="point">The point to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point2f Divide(Point2f point, float scalar)
		{
			return new Point2f(point.X / scalar, point.Y / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Point2f left, Point2f right)
		{
			return left == right;
		}
		#endregion
		#region Distance
		/// <summary>
		/// Returns the distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance between value1 and value2.</returns>
		public static float Distance(Point2f value1, Point2f value2)
		{
			return Vector.Absolute(value2 - value1);
		}
		/// <summary>
		/// Returns the squared distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The squared distance between value1 and value2.</returns>
		public static float DistanceSquared(Point2f value1, Point2f value2)
		{
			return Vector.AbsoluteSquared(value2 - value1);
		}
		/// <summary>
		/// Returns the manhatten distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The manhatten distance between value1 and value2.</returns>
		public static float ManhattenDistance(Point2f value1, Point2f value2)
		{
			return Functions.Abs(value2.X - value1.X)+Functions.Abs(value2.Y - value1.Y);
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all components of a point are non-zero.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>true if all components are non-zero; false otherwise.</returns>
		public static bool All(Point2f value)
		{
			return value.X != 0 && value.Y != 0;
		}
		/// <summary>
		/// Determines whether all components of a point satisfy a condition.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if every component of the point passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Point2f value, Predicate<float> predicate)
		{
			return predicate(value.X) && predicate(value.Y);
		}
		/// <summary>
		/// Determines whether any component of a point is non-zero.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Point2f value)
		{
			return value.X != 0 || value.Y != 0;
		}
		/// <summary>
		/// Determines whether any components of a point satisfy a condition.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if any component of the point passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Point2f value, Predicate<float> predicate)
		{
			return predicate(value.X) || predicate(value.Y);
		}
		#endregion
		#region Per component
		#region Transform
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point2d Transform(Point2f value, Func<float, double> transformer)
		{
			return new Point2d(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point2f Transform(Point2f value, Func<float, float> transformer)
		{
			return new Point2f(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point2l Transform(Point2f value, Func<float, long> transformer)
		{
			return new Point2l(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point2i Transform(Point2f value, Func<float, int> transformer)
		{
			return new Point2i(transformer(value.X), transformer(value.Y));
		}
		#endregion
		/// <summary>
		/// Multiplys the components of two points and returns the result.
		/// </summary>
		/// <param name="left">The first point to modulate.</param>
		/// <param name="right">The second point to modulate.</param>
		/// <returns>The result of multiplying each component of left by the matching component in right.</returns>
		public static Point2f Modulate(Point2f left, Point2f right)
		{
			return new Point2f(left.X * right.X, left.Y * right.Y);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Point2f Abs(Point2f value)
		{
			return new Point2f(Functions.Abs(value.X), Functions.Abs(value.Y));
		}
		/// <summary>
		/// Returns a point that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Point2f Min(Point2f value1, Point2f value2)
		{
			return new Point2f(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y));
		}
		/// <summary>
		/// Returns a point that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Point2f Max(Point2f value1, Point2f value2)
		{
			return new Point2f(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A point to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A point with each component constrained to the given range.</returns>
		public static Point2f Clamp(Point2f value, Point2f min, Point2f max)
		{
			return new Point2f(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y));
		}
		/// <summary>
		/// Constrains each component to the range 0 to 1.
		/// </summary>
		/// <param name="value">A point to saturate.</param>
		/// <returns>A point with each component constrained to the range 0 to 1.</returns>
		public static Point2f Saturate(Point2f value)
		{
			return new Point2f(Functions.Saturate(value.X), Functions.Saturate(value.Y));
		}
		/// <summary>
		/// Returns a point where each component is the smallest integral value that
		/// is greater than or equal to the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The ceiling of value.</returns>
		public static Point2f Ceiling(Point2f value)
		{
			return new Point2f(Functions.Ceiling(value.X), Functions.Ceiling(value.Y));
		}
		/// <summary>
		/// Returns a point where each component is the largest integral value that
		/// is less than or equal to the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The floor of value.</returns>
		public static Point2f Floor(Point2f value)
		{
			return new Point2f(Functions.Floor(value.X), Functions.Floor(value.Y));
		}
		/// <summary>
		/// Returns a point where each component is the integral part of the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The integral of value.</returns>
		public static Point2f Truncate(Point2f value)
		{
			return new Point2f(Functions.Truncate(value.X), Functions.Truncate(value.Y));
		}
		/// <summary>
		/// Returns a point where each component is the fractional part of the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The fractional of value.</returns>
		public static Point2f Fractional(Point2f value)
		{
			return new Point2f(Functions.Fractional(value.X), Functions.Fractional(value.Y));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point2f Round(Point2f value)
		{
			return new Point2f(Functions.Round(value.X), Functions.Round(value.Y));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point2f Round(Point2f value, int digits)
		{
			return new Point2f(Functions.Round(value.X, digits), Functions.Round(value.Y, digits));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point2f Round(Point2f value, MidpointRounding mode)
		{
			return new Point2f(Functions.Round(value.X, mode), Functions.Round(value.Y, mode));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point2f Round(Point2f value, int digits, MidpointRounding mode)
		{
			return new Point2f(Functions.Round(value.X, digits, mode), Functions.Round(value.Y, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each component in the point.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>A point with the reciprocal of each of values components.</returns>
		public static Point2f Reciprocal(Point2f value)
		{
			return new Point2f(1 / value.X, 1 / value.Y);
		}
		#endregion
		#region Interpolation
		/// <summary>
		/// Performs a linear interpolation between two points.
		/// </summary>
		/// <param name="point1">First point.</param>
		/// <param name="point2">Second point.</param>
		/// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="value2"/>.</param>
		/// <returns>The linear interpolation of the two points.</returns>
		public static Point2f Lerp(Point2f point1, Point2f point2, float amount)
		{
			return point1 + (point2 - point1) * amount;
		}
		#endregion
	}
}
