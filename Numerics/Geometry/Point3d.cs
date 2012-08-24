using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents an ordered triple of real x, y and z coordinates that defines a
	/// point in a three-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Point3d: IEquatable<Point3d>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns the point (0, 0, 0).
		/// </summary>
		public static readonly Point3d Zero = new Point3d(0, 0, 0);
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
		/// <summary>
		/// The Z coordinate of the point.
		/// </summary>
		public readonly double Z;
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
					case 2:
						return Z;
					default:
						throw new IndexOutOfRangeException("Indices for Point3d run from 0 to 2, inclusive.");
				}
			}
		}
		public double[] ToArray()
		{
			return new double[]
			{
				X, Y, Z
			};
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3d"/> using the specified point and value.
		/// </summary>
		/// <param name="value">A point containing the values with which to initialize the X and Y coordinates</param>
		/// <param name="z">Value for the Z coordinate of the point.</param>
		public Point3d(Point2d value, double z)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3d"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X coordinate of the point.</param>
		/// <param name="y">Value for the Y coordinate of the point.</param>
		/// <param name="z">Value for the Z coordinate of the point.</param>
		public Point3d(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3d"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		public Point3d(double[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3d"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Point3d(double[] array, int offset)
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
		public static Point3d operator +(Point3d value)
		{
			return value;
		}
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point3d operator +(Point3d point, Vector3d vector)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Adds a vector and a point and returns the result.
		/// </summary>
		/// <param name="vector">The vector value to add.</param>
		/// <param name="point">The point value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point3d operator +(Vector3d vector, Point3d point)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Subtracts one point from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3d operator -(Point3d left, Point3d right)
		{
			return Point.Subtract(left, right);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point3d operator -(Point3d point, Vector3d vector)
		{
			return Point.Subtract(point, vector);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="left">The point to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point3d operator *(Point3d left, double right)
		{
			return Point.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and point.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The point to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point3d operator *(double left, Point3d right)
		{
			return Point.Multiply(right, left);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The point to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point3d operator /(Point3d left, double right)
		{
			return Point.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a Point3f value to a Point3d.
		/// </summary>
		/// <param name="value">The value to convert to a Point3d.</param>
		/// <returns>A Point3d that has all components equal to value.</returns>
		public static implicit operator Point3d(Point3f value)
		{
			return new Point3d((double)value.X, (double)value.Y, (double)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Point3l value to a Point3d.
		/// </summary>
		/// <param name="value">The value to convert to a Point3d.</param>
		/// <returns>A Point3d that has all components equal to value.</returns>
		public static implicit operator Point3d(Point3l value)
		{
			return new Point3d((double)value.X, (double)value.Y, (double)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Point3i value to a Point3d.
		/// </summary>
		/// <param name="value">The value to convert to a Point3d.</param>
		/// <returns>A Point3d that has all components equal to value.</returns>
		public static implicit operator Point3d(Point3i value)
		{
			return new Point3d((double)value.X, (double)value.Y, (double)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3d value to a Point3d.
		/// </summary>
		/// <param name="value">The value to convert to a Point3d.</param>
		/// <returns>A Point3d that has all components equal to value.</returns>
		public static explicit operator Point3d(Vector3d value)
		{
			return new Point3d(value.X, value.Y, value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Point3d value to a Vector3d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3d.</param>
		/// <returns>A Vector3d that has all components equal to value.</returns>
		public static explicit operator Vector3d(Point3d value)
		{
			return new Vector3d(value.X, value.Y, value.Z);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Point3d"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Point3d"/> object or a type capable
		/// of implicit conversion to a <see cref="Point3d"/> object, and its value
		/// is equal to the current <see cref="Point3d"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Point3d) { return Equals((Point3d)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// point have the same value.
		/// </summary>
		/// <param name="other">The point to compare.</param>
		/// <returns>true if this point and value have the same value; otherwise, false.</returns>
		public bool Equals(Point3d other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Point3d left, Point3d right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are not equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Point3d left, Point3d right)
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
		/// Writes the given <see cref="Point3d"/> to a System.IO.BinaryWriter.
		/// </summary>
		public static void Write(this System.IO.BinaryWriter writer, Point3d point)
		{
			writer.Write(point.X);
			writer.Write(point.Y);
			writer.Write(point.Z);
		}
		/// <summary>
		/// Reads a <see cref="Point3d"/> to a System.IO.BinaryReader.
		/// </summary>
		public static Point3d ReadPoint3d(this System.IO.BinaryReader reader)
		{
			return new Point3d(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point3d Add(Point3d point, Vector3d vector)
		{
			return new Point3d(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
		}
		/// <summary>
		/// Subtracts one points from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3d Subtract(Point3d left, Point3d right)
		{
			return new Vector3d(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point3d Subtract(Point3d point, Vector3d vector)
		{
			return new Point3d(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="point">The point to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point3d Multiply(Point3d point, double scalar)
		{
			return new Point3d(point.X * scalar, point.Y * scalar, point.Z * scalar);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="point">The point to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point3d Divide(Point3d point, double scalar)
		{
			return new Point3d(point.X / scalar, point.Y / scalar, point.Z / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Point3d left, Point3d right)
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
		public static double Distance(Point3d value1, Point3d value2)
		{
			return Vector.Absolute(value2 - value1);
		}
		/// <summary>
		/// Returns the squared distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The squared distance between value1 and value2.</returns>
		public static double DistanceSquared(Point3d value1, Point3d value2)
		{
			return Vector.AbsoluteSquared(value2 - value1);
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all components of a point are non-zero.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>true if all components are non-zero; false otherwise.</returns>
		public static bool All(Point3d value)
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
		public static bool All(Point3d value, Predicate<double> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z);
		}
		/// <summary>
		/// Determines whether any component of a point is non-zero.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Point3d value)
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
		public static bool Any(Point3d value, Predicate<double> predicate)
		{
			return predicate(value.X) || predicate(value.Y) || predicate(value.Z);
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
		public static Point3d Transform(Point3d value, Func<double, double> transformer)
		{
			return new Point3d(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point3f Transform(Point3d value, Func<double, float> transformer)
		{
			return new Point3f(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point3l Transform(Point3d value, Func<double, long> transformer)
		{
			return new Point3l(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point3i Transform(Point3d value, Func<double, int> transformer)
		{
			return new Point3i(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		#endregion
		/// <summary>
		/// Multiplys the components of two points and returns the result.
		/// </summary>
		/// <param name="left">The first point to modulate.</param>
		/// <param name="right">The second point to modulate.</param>
		/// <returns>The result of multiplying each component of left by the matching component in right.</returns>
		public static Point3d Modulate(Point3d left, Point3d right)
		{
			return new Point3d(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Point3d Abs(Point3d value)
		{
			return new Point3d(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z));
		}
		/// <summary>
		/// Returns a point that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Point3d Min(Point3d value1, Point3d value2)
		{
			return new Point3d(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z));
		}
		/// <summary>
		/// Returns a point that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Point3d Max(Point3d value1, Point3d value2)
		{
			return new Point3d(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A point to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A point with each component constrained to the given range.</returns>
		public static Point3d Clamp(Point3d value, Point3d min, Point3d max)
		{
			return new Point3d(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z));
		}
		/// <summary>
		/// Constrains each component to the range 0 to 1.
		/// </summary>
		/// <param name="value">A point to saturate.</param>
		/// <returns>A point with each component constrained to the range 0 to 1.</returns>
		public static Point3d Saturate(Point3d value)
		{
			return new Point3d(Functions.Saturate(value.X), Functions.Saturate(value.Y), Functions.Saturate(value.Z));
		}
		/// <summary>
		/// Returns a point where each component is the smallest integral value that
		/// is greater than or equal to the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The ceiling of value.</returns>
		public static Point3d Ceiling(Point3d value)
		{
			return new Point3d(Functions.Ceiling(value.X), Functions.Ceiling(value.Y), Functions.Ceiling(value.Z));
		}
		/// <summary>
		/// Returns a point where each component is the largest integral value that
		/// is less than or equal to the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The floor of value.</returns>
		public static Point3d Floor(Point3d value)
		{
			return new Point3d(Functions.Floor(value.X), Functions.Floor(value.Y), Functions.Floor(value.Z));
		}
		/// <summary>
		/// Returns a point where each component is the integral part of the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The integral of value.</returns>
		public static Point3d Truncate(Point3d value)
		{
			return new Point3d(Functions.Truncate(value.X), Functions.Truncate(value.Y), Functions.Truncate(value.Z));
		}
		/// <summary>
		/// Returns a point where each component is the fractional part of the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The fractional of value.</returns>
		public static Point3d Fractional(Point3d value)
		{
			return new Point3d(Functions.Fractional(value.X), Functions.Fractional(value.Y), Functions.Fractional(value.Z));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point3d Round(Point3d value)
		{
			return new Point3d(Functions.Round(value.X), Functions.Round(value.Y), Functions.Round(value.Z));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point3d Round(Point3d value, int digits)
		{
			return new Point3d(Functions.Round(value.X, digits), Functions.Round(value.Y, digits), Functions.Round(value.Z, digits));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point3d Round(Point3d value, MidpointRounding mode)
		{
			return new Point3d(Functions.Round(value.X, mode), Functions.Round(value.Y, mode), Functions.Round(value.Z, mode));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point3d Round(Point3d value, int digits, MidpointRounding mode)
		{
			return new Point3d(Functions.Round(value.X, digits, mode), Functions.Round(value.Y, digits, mode), Functions.Round(value.Z, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each component in the point.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>A point with the reciprocal of each of values components.</returns>
		public static Point3d Reciprocal(Point3d value)
		{
			return new Point3d(1 / value.X, 1 / value.Y, 1 / value.Z);
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
		public static Point3d Lerp(Point3d point1, Point3d point2, double amount)
		{
			return point1 + (point2 - point1) * amount;
		}
		#endregion
	}
}
