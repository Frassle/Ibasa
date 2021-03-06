using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents an ordered triple of integer x, y and z coordinates that defines a
	/// point in a three-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Point3l: IEquatable<Point3l>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns the point (0, 0, 0).
		/// </summary>
		public static readonly Point3l Zero = new Point3l(0, 0, 0);
		#endregion
		#region Fields
		/// <summary>
		/// The X coordinate of the point.
		/// </summary>
		public readonly long X;
		/// <summary>
		/// The Y coordinate of the point.
		/// </summary>
		public readonly long Y;
		/// <summary>
		/// The Z coordinate of the point.
		/// </summary>
		public readonly long Z;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed coordinate of this point.
		/// </summary>
		/// <param name="index">The index of the coordinate.</param>
		/// <returns>The value of the indexed coordinate.</returns>
		public long this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return X;
					case 1:
						return Y;
					case 2:
						return Z;
					default:
						throw new IndexOutOfRangeException("Indices for Point3l run from 0 to 2, inclusive.");
				}
			}
		}
		public long[] ToArray()
		{
			return new long[]
			{
				X, Y, Z
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the point (X, X).
		/// </summary>
		public Point2l XX
		{
			get
			{
				return new Point2l(X, X);
			}
		}
		/// <summary>
		/// Returns the point (X, Y).
		/// </summary>
		public Point2l XY
		{
			get
			{
				return new Point2l(X, Y);
			}
		}
		/// <summary>
		/// Returns the point (X, Z).
		/// </summary>
		public Point2l XZ
		{
			get
			{
				return new Point2l(X, Z);
			}
		}
		/// <summary>
		/// Returns the point (Y, X).
		/// </summary>
		public Point2l YX
		{
			get
			{
				return new Point2l(Y, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y).
		/// </summary>
		public Point2l YY
		{
			get
			{
				return new Point2l(Y, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, Z).
		/// </summary>
		public Point2l YZ
		{
			get
			{
				return new Point2l(Y, Z);
			}
		}
		/// <summary>
		/// Returns the point (Z, X).
		/// </summary>
		public Point2l ZX
		{
			get
			{
				return new Point2l(Z, X);
			}
		}
		/// <summary>
		/// Returns the point (Z, Y).
		/// </summary>
		public Point2l ZY
		{
			get
			{
				return new Point2l(Z, Y);
			}
		}
		/// <summary>
		/// Returns the point (Z, Z).
		/// </summary>
		public Point2l ZZ
		{
			get
			{
				return new Point2l(Z, Z);
			}
		}
		/// <summary>
		/// Returns the point (X, X, X).
		/// </summary>
		public Point3l XXX
		{
			get
			{
				return new Point3l(X, X, X);
			}
		}
		/// <summary>
		/// Returns the point (X, X, Y).
		/// </summary>
		public Point3l XXY
		{
			get
			{
				return new Point3l(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the point (X, X, Z).
		/// </summary>
		public Point3l XXZ
		{
			get
			{
				return new Point3l(X, X, Z);
			}
		}
		/// <summary>
		/// Returns the point (X, Y, X).
		/// </summary>
		public Point3l XYX
		{
			get
			{
				return new Point3l(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the point (X, Y, Y).
		/// </summary>
		public Point3l XYY
		{
			get
			{
				return new Point3l(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the point (X, Y, Z).
		/// </summary>
		public Point3l XYZ
		{
			get
			{
				return new Point3l(X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the point (X, Z, X).
		/// </summary>
		public Point3l XZX
		{
			get
			{
				return new Point3l(X, Z, X);
			}
		}
		/// <summary>
		/// Returns the point (X, Z, Y).
		/// </summary>
		public Point3l XZY
		{
			get
			{
				return new Point3l(X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the point (X, Z, Z).
		/// </summary>
		public Point3l XZZ
		{
			get
			{
				return new Point3l(X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the point (Y, X, X).
		/// </summary>
		public Point3l YXX
		{
			get
			{
				return new Point3l(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, X, Y).
		/// </summary>
		public Point3l YXY
		{
			get
			{
				return new Point3l(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, X, Z).
		/// </summary>
		public Point3l YXZ
		{
			get
			{
				return new Point3l(Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y, X).
		/// </summary>
		public Point3l YYX
		{
			get
			{
				return new Point3l(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y, Y).
		/// </summary>
		public Point3l YYY
		{
			get
			{
				return new Point3l(Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, Y, Z).
		/// </summary>
		public Point3l YYZ
		{
			get
			{
				return new Point3l(Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the point (Y, Z, X).
		/// </summary>
		public Point3l YZX
		{
			get
			{
				return new Point3l(Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, Z, Y).
		/// </summary>
		public Point3l YZY
		{
			get
			{
				return new Point3l(Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, Z, Z).
		/// </summary>
		public Point3l YZZ
		{
			get
			{
				return new Point3l(Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the point (Z, X, X).
		/// </summary>
		public Point3l ZXX
		{
			get
			{
				return new Point3l(Z, X, X);
			}
		}
		/// <summary>
		/// Returns the point (Z, X, Y).
		/// </summary>
		public Point3l ZXY
		{
			get
			{
				return new Point3l(Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the point (Z, X, Z).
		/// </summary>
		public Point3l ZXZ
		{
			get
			{
				return new Point3l(Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the point (Z, Y, X).
		/// </summary>
		public Point3l ZYX
		{
			get
			{
				return new Point3l(Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the point (Z, Y, Y).
		/// </summary>
		public Point3l ZYY
		{
			get
			{
				return new Point3l(Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the point (Z, Y, Z).
		/// </summary>
		public Point3l ZYZ
		{
			get
			{
				return new Point3l(Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the point (Z, Z, X).
		/// </summary>
		public Point3l ZZX
		{
			get
			{
				return new Point3l(Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the point (Z, Z, Y).
		/// </summary>
		public Point3l ZZY
		{
			get
			{
				return new Point3l(Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the point (Z, Z, Z).
		/// </summary>
		public Point3l ZZZ
		{
			get
			{
				return new Point3l(Z, Z, Z);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3l"/> using the specified point and value.
		/// </summary>
		/// <param name="value">A point containing the values with which to initialize the X and Y coordinates</param>
		/// <param name="z">Value for the Z coordinate of the point.</param>
		public Point3l(Point2l value, long z)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3l"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X coordinate of the point.</param>
		/// <param name="y">Value for the Y coordinate of the point.</param>
		/// <param name="z">Value for the Z coordinate of the point.</param>
		public Point3l(long x, long y, long z)
		{
			X = x;
			Y = y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3l"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		public Point3l(long[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3l"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Point3l(long[] array, int offset)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[offset + 0];
			Y = array[offset + 1];
			Z = array[offset + 2];
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified point.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The identity of value.</returns>
		public static Point3l operator +(Point3l value)
		{
			return value;
		}
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point3l operator +(Point3l point, Vector3l vector)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Adds a vector and a point and returns the result.
		/// </summary>
		/// <param name="vector">The vector value to add.</param>
		/// <param name="point">The point value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point3l operator +(Vector3l vector, Point3l point)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Subtracts one point from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3l operator -(Point3l left, Point3l right)
		{
			return Point.Subtract(left, right);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point3l operator -(Point3l point, Vector3l vector)
		{
			return Point.Subtract(point, vector);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="left">The point to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point3l operator *(Point3l left, long right)
		{
			return Point.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and point.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The point to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point3l operator *(long left, Point3l right)
		{
			return Point.Multiply(right, left);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The point to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point3l operator /(Point3l left, long right)
		{
			return Point.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Point3d value to a Point3l.
		/// </summary>
		/// <param name="value">The value to convert to a Point3l.</param>
		/// <returns>A Point3l that has all components equal to value.</returns>
		public static explicit operator Point3l(Point3d value)
		{
			return new Point3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Point3f value to a Point3l.
		/// </summary>
		/// <param name="value">The value to convert to a Point3l.</param>
		/// <returns>A Point3l that has all components equal to value.</returns>
		public static explicit operator Point3l(Point3f value)
		{
			return new Point3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Point3i value to a Point3l.
		/// </summary>
		/// <param name="value">The value to convert to a Point3l.</param>
		/// <returns>A Point3l that has all components equal to value.</returns>
		public static implicit operator Point3l(Point3i value)
		{
			return new Point3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3l value to a Point3l.
		/// </summary>
		/// <param name="value">The value to convert to a Point3l.</param>
		/// <returns>A Point3l that has all components equal to value.</returns>
		public static explicit operator Point3l(Vector3l value)
		{
			return new Point3l(value.X, value.Y, value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Point3l value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		public static explicit operator Vector3l(Point3l value)
		{
			return new Vector3l(value.X, value.Y, value.Z);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Point3l"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Point3l"/> object or a type capable
		/// of implicit conversion to a <see cref="Point3l"/> object, and its value
		/// is equal to the current <see cref="Point3l"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Point3l) { return Equals((Point3l)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// point have the same value.
		/// </summary>
		/// <param name="other">The point to compare.</param>
		/// <returns>true if this point and value have the same value; otherwise, false.</returns>
		public bool Equals(Point3l other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Point3l left, Point3l right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are not equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Point3l left, Point3l right)
		{
			return left.X != right.X | left.Y != right.Y | left.Z != right.Z;
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
			return String.Format("({0}, {1}, {2})", X.ToString(format, provider), Y.ToString(format, provider), Z.ToString(format, provider));
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
		/// Writes the given <see cref="Point3l"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Point3l point)
		{
			writer.Write(point.X);
			writer.Write(point.Y);
			writer.Write(point.Z);
		}
		/// <summary>
		/// Reads a <see cref="Point3l"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Point3l ReadPoint3l(this Ibasa.IO.BinaryReader reader)
		{
			return new Point3l(reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point3l Add(Point3l point, Vector3l vector)
		{
			return new Point3l(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
		}
		/// <summary>
		/// Subtracts one points from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3l Subtract(Point3l left, Point3l right)
		{
			return new Vector3l(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point3l Subtract(Point3l point, Vector3l vector)
		{
			return new Point3l(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="point">The point to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point3l Multiply(Point3l point, long scalar)
		{
			return new Point3l(point.X * scalar, point.Y * scalar, point.Z * scalar);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="point">The point to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point3l Divide(Point3l point, long scalar)
		{
			return new Point3l(point.X / scalar, point.Y / scalar, point.Z / scalar);
		}
		public static Point3d Sum(IEnumerable<Point3l> points, IEnumerable<double> weights)
		{
			Contract.Requires(weights.Sum() == 1.0);
			double sumX = 0;
			double sumY = 0;
			double sumZ = 0;
			var point = points.GetEnumerator();
			var weight = weights.GetEnumerator();
			while(point.MoveNext() && weight.MoveNext())
			{
				var p = point.Current;
				var w = weight.Current;
				sumX += p.X * w;
				sumY += p.Y * w;
				sumZ += p.Z * w;
			}
			return new Point3d(sumX, sumY, sumZ);
		}
		public static Point3d Sum(IEnumerable<Point3l> points, double weight)
		{
			Contract.Requires(weight * points.Count() == 1.0);
			double sumX = 0;
			double sumY = 0;
			double sumZ = 0;
			foreach (var point in points)
			{
				sumX += point.X * weight;
				sumY += point.Y * weight;
				sumZ += point.Z * weight;
			}
			return new Point3d(sumX, sumY, sumZ);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Point3l left, Point3l right)
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
		public static double Distance(Point3l value1, Point3l value2)
		{
			return Vector.Absolute(value2 - value1);
		}
		/// <summary>
		/// Returns the squared distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The squared distance between value1 and value2.</returns>
		public static long DistanceSquared(Point3l value1, Point3l value2)
		{
			return Vector.AbsoluteSquared(value2 - value1);
		}
		/// <summary>
		/// Returns the manhatten distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The manhatten distance between value1 and value2.</returns>
		public static long ManhattenDistance(Point3l value1, Point3l value2)
		{
			return Functions.Abs(value2.X - value1.X)+Functions.Abs(value2.Y - value1.Y)+Functions.Abs(value2.Z - value1.Z);
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all components of a point are non-zero.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>true if all components are non-zero; false otherwise.</returns>
		public static bool All(Point3l value)
		{
			return value.X != 0 && value.Y != 0 && value.Z != 0;
		}
		/// <summary>
		/// Determines whether all components of a point satisfy a condition.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if every component of the point passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Point3l value, Predicate<long> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z);
		}
		/// <summary>
		/// Determines whether any component of a point is non-zero.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Point3l value)
		{
			return value.X != 0 || value.Y != 0 || value.Z != 0;
		}
		/// <summary>
		/// Determines whether any components of a point satisfy a condition.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if any component of the point passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Point3l value, Predicate<long> predicate)
		{
			return predicate(value.X) || predicate(value.Y) || predicate(value.Z);
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
		public static Point3d Map(Point3l value, Func<long, double> mapping)
		{
			return new Point3d(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		public static Point3f Map(Point3l value, Func<long, float> mapping)
		{
			return new Point3f(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		public static Point3l Map(Point3l value, Func<long, long> mapping)
		{
			return new Point3l(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		public static Point3i Map(Point3l value, Func<long, int> mapping)
		{
			return new Point3i(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		#endregion
		/// <summary>
		/// Multiplys the components of two points and returns the result.
		/// </summary>
		/// <param name="left">The first point to modulate.</param>
		/// <param name="right">The second point to modulate.</param>
		/// <returns>The result of multiplying each component of left by the matching component in right.</returns>
		public static Point3l Modulate(Point3l left, Point3l right)
		{
			return new Point3l(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Point3l Abs(Point3l value)
		{
			return new Point3l(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z));
		}
		/// <summary>
		/// Returns a point that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Point3l Min(Point3l value1, Point3l value2)
		{
			return new Point3l(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z));
		}
		/// <summary>
		/// Returns a point that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Point3l Max(Point3l value1, Point3l value2)
		{
			return new Point3l(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A point to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A point with each component constrained to the given range.</returns>
		public static Point3l Clamp(Point3l value, Point3l min, Point3l max)
		{
			return new Point3l(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z));
		}
		#endregion
		#region Coordinate spaces
		/// <summary>
		/// Transforms a point in cartesian coordinates to spherical coordinates.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <returns>The spherical coordinates of value.</returns>
		public static SphericalCoordinate CartesianToSpherical (Point3l value)
		{
			double r = Functions.Sqrt(value.X * value.X + value.Y * value.Y + value.Z * value.Z);
			double theta = Functions.Atan2(value.Y, value.X);
			if (theta < 0)
				theta += 2 * Constants.Pi;
			return new SphericalCoordinate(
			     r,
			     (double)Functions.Acos(value.Z / r),
			     theta);
		}
		#endregion
		#region Project
		/// <summary>
		/// Projects a point onto a vector, returns the distance of the projection from the origin.
		/// </summary>
		/// <param name="vector">The vector to project onto.</param>
		/// <param name="point">The point to project.</param>
		/// <returns>The distance from the origin of the projection.</returns>
		public static long Project(Point3l point, Vector3l vector)
		{
			return vector.X * point.X + vector.Y * point.Y + vector.Z * point.Z;
		}
		#endregion
	}
}
