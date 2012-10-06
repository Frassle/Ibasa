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
	public struct Point2d: IEquatable<Point2d>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns the point (0, 0).
		/// </summary>
		public static readonly Point2d Zero = new Point2d(0, 0);
		#endregion
		#region Fields
		/// <summary>
		/// The X coordinate of the point.
		/// </summary>
		public readonly double X;
		/// <summary>
		/// The Y coordinate of the point.
		/// </summary>
		public readonly double Y;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed coordinate of this point.
		/// </summary>
		/// <param name="index">The index of the coordinate.</param>
		/// <returns>The value of the indexed coordinate.</returns>
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
						throw new IndexOutOfRangeException("Indices for Point2d run from 0 to 1, inclusive.");
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
		/// Returns the point (X, X).
		/// </summary>
		public Point2d XX
		{
			get
			{
				return new Point2d(X, X);
			}
		}
		/// <summary>
		/// Returns the point (X, Y).
		/// </summary>
		public Point2d XY
		{
			get
			{
				return new Point2d(X, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, X).
		/// </summary>
		public Point2d YX
		{
			get
			{
				return new Point2d(Y, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y).
		/// </summary>
		public Point2d YY
		{
			get
			{
				return new Point2d(Y, Y);
			}
		}
		/// <summary>
		/// Returns the point (X, X, X).
		/// </summary>
		public Point3d XXX
		{
			get
			{
				return new Point3d(X, X, X);
			}
		}
		/// <summary>
		/// Returns the point (X, X, Y).
		/// </summary>
		public Point3d XXY
		{
			get
			{
				return new Point3d(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the point (X, Y, X).
		/// </summary>
		public Point3d XYX
		{
			get
			{
				return new Point3d(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the point (X, Y, Y).
		/// </summary>
		public Point3d XYY
		{
			get
			{
				return new Point3d(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, X, X).
		/// </summary>
		public Point3d YXX
		{
			get
			{
				return new Point3d(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, X, Y).
		/// </summary>
		public Point3d YXY
		{
			get
			{
				return new Point3d(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y, X).
		/// </summary>
		public Point3d YYX
		{
			get
			{
				return new Point3d(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y, Y).
		/// </summary>
		public Point3d YYY
		{
			get
			{
				return new Point3d(Y, Y, Y);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Point2d"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X coordinate of the point.</param>
		/// <param name="y">Value for the Y coordinate of the point.</param>
		public Point2d(double x, double y)
		{
			X = x;
			Y = y;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point2d"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		public Point2d(double[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point2d"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Point2d(double[] array, int offset)
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
		public static Point2d operator +(Point2d value)
		{
			return value;
		}
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point2d operator +(Point2d point, Vector2d vector)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Adds a vector and a point and returns the result.
		/// </summary>
		/// <param name="vector">The vector value to add.</param>
		/// <param name="point">The point value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point2d operator +(Vector2d vector, Point2d point)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Subtracts one point from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector2d operator -(Point2d left, Point2d right)
		{
			return Point.Subtract(left, right);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point2d operator -(Point2d point, Vector2d vector)
		{
			return Point.Subtract(point, vector);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="left">The point to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point2d operator *(Point2d left, double right)
		{
			return Point.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and point.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The point to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point2d operator *(double left, Point2d right)
		{
			return Point.Multiply(right, left);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The point to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point2d operator /(Point2d left, double right)
		{
			return Point.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a Point2f value to a Point2d.
		/// </summary>
		/// <param name="value">The value to convert to a Point2d.</param>
		/// <returns>A Point2d that has all components equal to value.</returns>
		public static implicit operator Point2d(Point2f value)
		{
			return new Point2d((double)value.X, (double)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Point2l value to a Point2d.
		/// </summary>
		/// <param name="value">The value to convert to a Point2d.</param>
		/// <returns>A Point2d that has all components equal to value.</returns>
		public static implicit operator Point2d(Point2l value)
		{
			return new Point2d((double)value.X, (double)value.Y);
		}
		/// <summary>
		/// Defines an implicit conversion of a Point2i value to a Point2d.
		/// </summary>
		/// <param name="value">The value to convert to a Point2d.</param>
		/// <returns>A Point2d that has all components equal to value.</returns>
		public static implicit operator Point2d(Point2i value)
		{
			return new Point2d((double)value.X, (double)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2d value to a Point2d.
		/// </summary>
		/// <param name="value">The value to convert to a Point2d.</param>
		/// <returns>A Point2d that has all components equal to value.</returns>
		public static explicit operator Point2d(Vector2d value)
		{
			return new Point2d(value.X, value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Point2d value to a Vector2d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2d.</param>
		/// <returns>A Vector2d that has all components equal to value.</returns>
		public static explicit operator Vector2d(Point2d value)
		{
			return new Vector2d(value.X, value.Y);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Point2d"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Point2d"/> object or a type capable
		/// of implicit conversion to a <see cref="Point2d"/> object, and its value
		/// is equal to the current <see cref="Point2d"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Point2d) { return Equals((Point2d)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// point have the same value.
		/// </summary>
		/// <param name="other">The point to compare.</param>
		/// <returns>true if this point and value have the same value; otherwise, false.</returns>
		public bool Equals(Point2d other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Point2d left, Point2d right)
		{
			return left.X == right.X & left.Y == right.Y;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are not equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Point2d left, Point2d right)
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
		/// Writes the given <see cref="Point2d"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Point2d point)
		{
			writer.Write(point.X);
			writer.Write(point.Y);
		}
		/// <summary>
		/// Reads a <see cref="Point2d"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Point2d ReadPoint2d(this Ibasa.IO.BinaryReader reader)
		{
			return new Point2d(reader.ReadDouble(), reader.ReadDouble());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point2d Add(Point2d point, Vector2d vector)
		{
			return new Point2d(point.X + vector.X, point.Y + vector.Y);
		}
		/// <summary>
		/// Subtracts one points from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector2d Subtract(Point2d left, Point2d right)
		{
			return new Vector2d(left.X - right.X, left.Y - right.Y);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point2d Subtract(Point2d point, Vector2d vector)
		{
			return new Point2d(point.X - vector.X, point.Y - vector.Y);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="point">The point to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point2d Multiply(Point2d point, double scalar)
		{
			return new Point2d(point.X * scalar, point.Y * scalar);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="point">The point to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point2d Divide(Point2d point, double scalar)
		{
			return new Point2d(point.X / scalar, point.Y / scalar);
		}
		public static Point2d Sum(IEnumerable<Point2d> points, IEnumerable<double> weights)
		{
			Contract.Requires(weights.Sum() == 1.0);
			var sum = Point2d.Zero;
			var point = points.GetEnumerator();
			var weight = weights.GetEnumerator();
			while(point.MoveNext() && weight.MoveNext())
			{
				sum += (Vector2d)(point.Current * weight.Current);
			}
			return sum;
		}
		public static Point2d Sum(IEnumerable<Point2d> points, double weight)
		{
			Contract.Requires(weight * points.Count() == 1.0);
			var sum = Point2d.Zero;
			foreach (var point in points)
			{
				sum += (Vector2d)(point * weight);
			}
			return sum;
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Point2d left, Point2d right)
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
		public static double Distance(Point2d value1, Point2d value2)
		{
			return Vector.Absolute(value2 - value1);
		}
		/// <summary>
		/// Returns the squared distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The squared distance between value1 and value2.</returns>
		public static double DistanceSquared(Point2d value1, Point2d value2)
		{
			return Vector.AbsoluteSquared(value2 - value1);
		}
		/// <summary>
		/// Returns the manhatten distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The manhatten distance between value1 and value2.</returns>
		public static double ManhattenDistance(Point2d value1, Point2d value2)
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
		public static bool All(Point2d value)
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
		public static bool All(Point2d value, Predicate<double> predicate)
		{
			return predicate(value.X) && predicate(value.Y);
		}
		/// <summary>
		/// Determines whether any component of a point is non-zero.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Point2d value)
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
		public static bool Any(Point2d value, Predicate<double> predicate)
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
		public static Point2d Transform(Point2d value, Func<double, double> transformer)
		{
			return new Point2d(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point2f Transform(Point2d value, Func<double, float> transformer)
		{
			return new Point2f(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point2l Transform(Point2d value, Func<double, long> transformer)
		{
			return new Point2l(transformer(value.X), transformer(value.Y));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point2i Transform(Point2d value, Func<double, int> transformer)
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
		public static Point2d Modulate(Point2d left, Point2d right)
		{
			return new Point2d(left.X * right.X, left.Y * right.Y);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Point2d Abs(Point2d value)
		{
			return new Point2d(Functions.Abs(value.X), Functions.Abs(value.Y));
		}
		/// <summary>
		/// Returns a point that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Point2d Min(Point2d value1, Point2d value2)
		{
			return new Point2d(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y));
		}
		/// <summary>
		/// Returns a point that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Point2d Max(Point2d value1, Point2d value2)
		{
			return new Point2d(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A point to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A point with each component constrained to the given range.</returns>
		public static Point2d Clamp(Point2d value, Point2d min, Point2d max)
		{
			return new Point2d(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y));
		}
		/// <summary>
		/// Constrains each component to the range 0 to 1.
		/// </summary>
		/// <param name="value">A point to saturate.</param>
		/// <returns>A point with each component constrained to the range 0 to 1.</returns>
		public static Point2d Saturate(Point2d value)
		{
			return new Point2d(Functions.Saturate(value.X), Functions.Saturate(value.Y));
		}
		/// <summary>
		/// Returns a point where each component is the smallest integral value that
		/// is greater than or equal to the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The ceiling of value.</returns>
		public static Point2d Ceiling(Point2d value)
		{
			return new Point2d(Functions.Ceiling(value.X), Functions.Ceiling(value.Y));
		}
		/// <summary>
		/// Returns a point where each component is the largest integral value that
		/// is less than or equal to the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The floor of value.</returns>
		public static Point2d Floor(Point2d value)
		{
			return new Point2d(Functions.Floor(value.X), Functions.Floor(value.Y));
		}
		/// <summary>
		/// Returns a point where each component is the integral part of the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The integral of value.</returns>
		public static Point2d Truncate(Point2d value)
		{
			return new Point2d(Functions.Truncate(value.X), Functions.Truncate(value.Y));
		}
		/// <summary>
		/// Returns a point where each component is the fractional part of the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The fractional of value.</returns>
		public static Point2d Fractional(Point2d value)
		{
			return new Point2d(Functions.Fractional(value.X), Functions.Fractional(value.Y));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point2d Round(Point2d value)
		{
			return new Point2d(Functions.Round(value.X), Functions.Round(value.Y));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point2d Round(Point2d value, int digits)
		{
			return new Point2d(Functions.Round(value.X, digits), Functions.Round(value.Y, digits));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point2d Round(Point2d value, MidpointRounding mode)
		{
			return new Point2d(Functions.Round(value.X, mode), Functions.Round(value.Y, mode));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point2d Round(Point2d value, int digits, MidpointRounding mode)
		{
			return new Point2d(Functions.Round(value.X, digits, mode), Functions.Round(value.Y, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each component in the point.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>A point with the reciprocal of each of values components.</returns>
		public static Point2d Reciprocal(Point2d value)
		{
			return new Point2d(1 / value.X, 1 / value.Y);
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
		public static Point2d Lerp(Point2d point1, Point2d point2, double amount)
		{
			return point1 + (point2 - point1) * amount;
		}
		#endregion
	}
}
