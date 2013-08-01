using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents an ordered pair of integer x and y coordinates that defines a
	/// point in a two-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Point2i: IEquatable<Point2i>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns the point (0, 0).
		/// </summary>
		public static readonly Point2i Zero = new Point2i(0, 0);
		#endregion
		#region Fields
		/// <summary>
		/// The X coordinate of the point.
		/// </summary>
		public readonly int X;
		/// <summary>
		/// The Y coordinate of the point.
		/// </summary>
		public readonly int Y;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed coordinate of this point.
		/// </summary>
		/// <param name="index">The index of the coordinate.</param>
		/// <returns>The value of the indexed coordinate.</returns>
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
						throw new IndexOutOfRangeException("Indices for Point2i run from 0 to 1, inclusive.");
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
		/// Returns the point (X, X).
		/// </summary>
		public Point2i XX
		{
			get
			{
				return new Point2i(X, X);
			}
		}
		/// <summary>
		/// Returns the point (X, Y).
		/// </summary>
		public Point2i XY
		{
			get
			{
				return new Point2i(X, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, X).
		/// </summary>
		public Point2i YX
		{
			get
			{
				return new Point2i(Y, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y).
		/// </summary>
		public Point2i YY
		{
			get
			{
				return new Point2i(Y, Y);
			}
		}
		/// <summary>
		/// Returns the point (X, X, X).
		/// </summary>
		public Point3i XXX
		{
			get
			{
				return new Point3i(X, X, X);
			}
		}
		/// <summary>
		/// Returns the point (X, X, Y).
		/// </summary>
		public Point3i XXY
		{
			get
			{
				return new Point3i(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the point (X, Y, X).
		/// </summary>
		public Point3i XYX
		{
			get
			{
				return new Point3i(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the point (X, Y, Y).
		/// </summary>
		public Point3i XYY
		{
			get
			{
				return new Point3i(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, X, X).
		/// </summary>
		public Point3i YXX
		{
			get
			{
				return new Point3i(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, X, Y).
		/// </summary>
		public Point3i YXY
		{
			get
			{
				return new Point3i(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y, X).
		/// </summary>
		public Point3i YYX
		{
			get
			{
				return new Point3i(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y, Y).
		/// </summary>
		public Point3i YYY
		{
			get
			{
				return new Point3i(Y, Y, Y);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Point2i"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X coordinate of the point.</param>
		/// <param name="y">Value for the Y coordinate of the point.</param>
		public Point2i(int x, int y)
		{
			X = x;
			Y = y;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point2i"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		public Point2i(int[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point2i"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Point2i(int[] array, int offset)
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
		public static Point2i operator +(Point2i value)
		{
			return value;
		}
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point2i operator +(Point2i point, Vector2i vector)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Adds a vector and a point and returns the result.
		/// </summary>
		/// <param name="vector">The vector value to add.</param>
		/// <param name="point">The point value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point2i operator +(Vector2i vector, Point2i point)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Subtracts one point from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector2i operator -(Point2i left, Point2i right)
		{
			return Point.Subtract(left, right);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point2i operator -(Point2i point, Vector2i vector)
		{
			return Point.Subtract(point, vector);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="left">The point to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point2i operator *(Point2i left, int right)
		{
			return Point.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and point.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The point to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point2i operator *(int left, Point2i right)
		{
			return Point.Multiply(right, left);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The point to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point2i operator /(Point2i left, int right)
		{
			return Point.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Point2d value to a Point2i.
		/// </summary>
		/// <param name="value">The value to convert to a Point2i.</param>
		/// <returns>A Point2i that has all components equal to value.</returns>
		public static explicit operator Point2i(Point2d value)
		{
			return new Point2i((int)value.X, (int)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Point2f value to a Point2i.
		/// </summary>
		/// <param name="value">The value to convert to a Point2i.</param>
		/// <returns>A Point2i that has all components equal to value.</returns>
		public static explicit operator Point2i(Point2f value)
		{
			return new Point2i((int)value.X, (int)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Point2l value to a Point2i.
		/// </summary>
		/// <param name="value">The value to convert to a Point2i.</param>
		/// <returns>A Point2i that has all components equal to value.</returns>
		public static explicit operator Point2i(Point2l value)
		{
			return new Point2i((int)value.X, (int)value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector2i value to a Point2i.
		/// </summary>
		/// <param name="value">The value to convert to a Point2i.</param>
		/// <returns>A Point2i that has all components equal to value.</returns>
		public static explicit operator Point2i(Vector2i value)
		{
			return new Point2i(value.X, value.Y);
		}
		/// <summary>
		/// Defines an explicit conversion of a Point2i value to a Vector2i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector2i.</param>
		/// <returns>A Vector2i that has all components equal to value.</returns>
		public static explicit operator Vector2i(Point2i value)
		{
			return new Vector2i(value.X, value.Y);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Point2i"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Point2i"/> object or a type capable
		/// of implicit conversion to a <see cref="Point2i"/> object, and its value
		/// is equal to the current <see cref="Point2i"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Point2i) { return Equals((Point2i)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// point have the same value.
		/// </summary>
		/// <param name="other">The point to compare.</param>
		/// <returns>true if this point and value have the same value; otherwise, false.</returns>
		public bool Equals(Point2i other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Point2i left, Point2i right)
		{
			return left.X == right.X & left.Y == right.Y;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are not equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Point2i left, Point2i right)
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
		/// Writes the given <see cref="Point2i"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Point2i point)
		{
			writer.Write(point.X);
			writer.Write(point.Y);
		}
		/// <summary>
		/// Reads a <see cref="Point2i"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Point2i ReadPoint2i(this Ibasa.IO.BinaryReader reader)
		{
			return new Point2i(reader.ReadInt32(), reader.ReadInt32());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point2i Add(Point2i point, Vector2i vector)
		{
			return new Point2i(point.X + vector.X, point.Y + vector.Y);
		}
		/// <summary>
		/// Subtracts one points from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector2i Subtract(Point2i left, Point2i right)
		{
			return new Vector2i(left.X - right.X, left.Y - right.Y);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point2i Subtract(Point2i point, Vector2i vector)
		{
			return new Point2i(point.X - vector.X, point.Y - vector.Y);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="point">The point to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point2i Multiply(Point2i point, int scalar)
		{
			return new Point2i(point.X * scalar, point.Y * scalar);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="point">The point to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point2i Divide(Point2i point, int scalar)
		{
			return new Point2i(point.X / scalar, point.Y / scalar);
		}
		public static Point2d Sum(IEnumerable<Point2i> points, IEnumerable<double> weights)
		{
			Contract.Requires(weights.Sum() == 1.0);
			double sumX = 0;
			double sumY = 0;
			var point = points.GetEnumerator();
			var weight = weights.GetEnumerator();
			while(point.MoveNext() && weight.MoveNext())
			{
				var p = point.Current;
				var w = weight.Current;
				sumX += p.X * w;
				sumY += p.Y * w;
			}
			return new Point2d(sumX, sumY);
		}
		public static Point2d Sum(IEnumerable<Point2i> points, double weight)
		{
			Contract.Requires(weight * points.Count() == 1.0);
			double sumX = 0;
			double sumY = 0;
			foreach (var point in points)
			{
				sumX += point.X * weight;
				sumY += point.Y * weight;
			}
			return new Point2d(sumX, sumY);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Point2i left, Point2i right)
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
		public static double Distance(Point2i value1, Point2i value2)
		{
			return Vector.Absolute(value2 - value1);
		}
		/// <summary>
		/// Returns the squared distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The squared distance between value1 and value2.</returns>
		public static int DistanceSquared(Point2i value1, Point2i value2)
		{
			return Vector.AbsoluteSquared(value2 - value1);
		}
		/// <summary>
		/// Returns the manhatten distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The manhatten distance between value1 and value2.</returns>
		public static int ManhattenDistance(Point2i value1, Point2i value2)
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
		public static bool All(Point2i value)
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
		public static bool All(Point2i value, Predicate<int> predicate)
		{
			return predicate(value.X) && predicate(value.Y);
		}
		/// <summary>
		/// Determines whether any component of a point is non-zero.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Point2i value)
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
		public static bool Any(Point2i value, Predicate<int> predicate)
		{
			return predicate(value.X) || predicate(value.Y);
		}
		#endregion
		#region Per component
		#region Map
		/// <summary>
		/// Maps the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		public static Point2d Map(Point2i value, Func<int, double> mapping)
		{
			return new Point2d(mapping(value.X), mapping(value.Y));
		}
		/// <summary>
		/// Maps the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		public static Point2f Map(Point2i value, Func<int, float> mapping)
		{
			return new Point2f(mapping(value.X), mapping(value.Y));
		}
		/// <summary>
		/// Maps the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		public static Point2l Map(Point2i value, Func<int, long> mapping)
		{
			return new Point2l(mapping(value.X), mapping(value.Y));
		}
		/// <summary>
		/// Maps the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		public static Point2i Map(Point2i value, Func<int, int> mapping)
		{
			return new Point2i(mapping(value.X), mapping(value.Y));
		}
		#endregion
		/// <summary>
		/// Multiplys the components of two points and returns the result.
		/// </summary>
		/// <param name="left">The first point to modulate.</param>
		/// <param name="right">The second point to modulate.</param>
		/// <returns>The result of multiplying each component of left by the matching component in right.</returns>
		public static Point2i Modulate(Point2i left, Point2i right)
		{
			return new Point2i(left.X * right.X, left.Y * right.Y);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Point2i Abs(Point2i value)
		{
			return new Point2i(Functions.Abs(value.X), Functions.Abs(value.Y));
		}
		/// <summary>
		/// Returns a point that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Point2i Min(Point2i value1, Point2i value2)
		{
			return new Point2i(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y));
		}
		/// <summary>
		/// Returns a point that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Point2i Max(Point2i value1, Point2i value2)
		{
			return new Point2i(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A point to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A point with each component constrained to the given range.</returns>
		public static Point2i Clamp(Point2i value, Point2i min, Point2i max)
		{
			return new Point2i(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y));
		}
		#endregion
		#region Coordinate spaces
		/// <summary>
		/// Transforms a point in cartesian coordinates to polar coordinates.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <returns>The polar coordinates of value.</returns>
		public static PolarCoordinate CartesianToPolar(Point2i value)
		{
			double theta = Functions.Atan2(value.Y, value.X);
			if (theta < 0)
				theta += 2 * Constants.Pi;
			return new PolarCoordinate(
			     theta,
			     (double)Functions.Sqrt(value.X * value.X + value.Y * value.Y));
		}
		#endregion
		#region Project
		/// <summary>
		/// Projects a point onto a vector, returns the distance of the projection from the origin.
		/// </summary>
		/// <param name="vector">The vector to project onto.</param>
		/// <param name="point">The point to project.</param>
		/// <returns>The distance from the origin of the projection.</returns>
		public static int Project(Point2i point, Vector2i vector)
		{
			return vector.X * point.X + vector.Y * point.Y;
		}
		#endregion
	}
}
