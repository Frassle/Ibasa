using System;
using System.Diagnostics.Contracts;
using System.Globalization;
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
	public struct Point3i: IEquatable<Point3i>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns the point (0, 0, 0).
		/// </summary>
		public static readonly Point3i Zero = new Point3i(0, 0, 0);
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
		/// <summary>
		/// The Z coordinate of the point.
		/// </summary>
		public readonly int Z;
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
					case 2:
						return Z;
					default:
						throw new IndexOutOfRangeException("Indices for Point3i run from 0 to 2, inclusive.");
				}
			}
		}
		public int[] ToArray()
		{
			return new int[]
			{
				X, Y, Z
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
		/// Returns the point (X, Z).
		/// </summary>
		public Point2i XZ
		{
			get
			{
				return new Point2i(X, Z);
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
		/// Returns the point (Y, Z).
		/// </summary>
		public Point2i YZ
		{
			get
			{
				return new Point2i(Y, Z);
			}
		}
		/// <summary>
		/// Returns the point (Z, X).
		/// </summary>
		public Point2i ZX
		{
			get
			{
				return new Point2i(Z, X);
			}
		}
		/// <summary>
		/// Returns the point (Z, Y).
		/// </summary>
		public Point2i ZY
		{
			get
			{
				return new Point2i(Z, Y);
			}
		}
		/// <summary>
		/// Returns the point (Z, Z).
		/// </summary>
		public Point2i ZZ
		{
			get
			{
				return new Point2i(Z, Z);
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
		/// Returns the point (X, X, Z).
		/// </summary>
		public Point3i XXZ
		{
			get
			{
				return new Point3i(X, X, Z);
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
		/// Returns the point (X, Y, Z).
		/// </summary>
		public Point3i XYZ
		{
			get
			{
				return new Point3i(X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the point (X, Z, X).
		/// </summary>
		public Point3i XZX
		{
			get
			{
				return new Point3i(X, Z, X);
			}
		}
		/// <summary>
		/// Returns the point (X, Z, Y).
		/// </summary>
		public Point3i XZY
		{
			get
			{
				return new Point3i(X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the point (X, Z, Z).
		/// </summary>
		public Point3i XZZ
		{
			get
			{
				return new Point3i(X, Z, Z);
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
		/// Returns the point (Y, X, Z).
		/// </summary>
		public Point3i YXZ
		{
			get
			{
				return new Point3i(Y, X, Z);
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
		/// <summary>
		/// Returns the point (Y, Y, Z).
		/// </summary>
		public Point3i YYZ
		{
			get
			{
				return new Point3i(Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the point (Y, Z, X).
		/// </summary>
		public Point3i YZX
		{
			get
			{
				return new Point3i(Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, Z, Y).
		/// </summary>
		public Point3i YZY
		{
			get
			{
				return new Point3i(Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, Z, Z).
		/// </summary>
		public Point3i YZZ
		{
			get
			{
				return new Point3i(Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the point (Z, X, X).
		/// </summary>
		public Point3i ZXX
		{
			get
			{
				return new Point3i(Z, X, X);
			}
		}
		/// <summary>
		/// Returns the point (Z, X, Y).
		/// </summary>
		public Point3i ZXY
		{
			get
			{
				return new Point3i(Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the point (Z, X, Z).
		/// </summary>
		public Point3i ZXZ
		{
			get
			{
				return new Point3i(Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the point (Z, Y, X).
		/// </summary>
		public Point3i ZYX
		{
			get
			{
				return new Point3i(Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the point (Z, Y, Y).
		/// </summary>
		public Point3i ZYY
		{
			get
			{
				return new Point3i(Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the point (Z, Y, Z).
		/// </summary>
		public Point3i ZYZ
		{
			get
			{
				return new Point3i(Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the point (Z, Z, X).
		/// </summary>
		public Point3i ZZX
		{
			get
			{
				return new Point3i(Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the point (Z, Z, Y).
		/// </summary>
		public Point3i ZZY
		{
			get
			{
				return new Point3i(Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the point (Z, Z, Z).
		/// </summary>
		public Point3i ZZZ
		{
			get
			{
				return new Point3i(Z, Z, Z);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3i"/> using the specified point and value.
		/// </summary>
		/// <param name="value">A point containing the values with which to initialize the X and Y coordinates</param>
		/// <param name="z">Value for the Z coordinate of the point.</param>
		public Point3i(Point2i value, int z)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3i"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X coordinate of the point.</param>
		/// <param name="y">Value for the Y coordinate of the point.</param>
		/// <param name="z">Value for the Z coordinate of the point.</param>
		public Point3i(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3i"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		public Point3i(int[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3i"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Point3i(int[] array, int offset)
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
		public static Point3i operator +(Point3i value)
		{
			return value;
		}
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point3i operator +(Point3i point, Vector3i vector)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Adds a vector and a point and returns the result.
		/// </summary>
		/// <param name="vector">The vector value to add.</param>
		/// <param name="point">The point value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point3i operator +(Vector3i vector, Point3i point)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Subtracts one point from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3i operator -(Point3i left, Point3i right)
		{
			return Point.Subtract(left, right);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point3i operator -(Point3i point, Vector3i vector)
		{
			return Point.Subtract(point, vector);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="left">The point to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point3i operator *(Point3i left, int right)
		{
			return Point.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and point.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The point to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point3i operator *(int left, Point3i right)
		{
			return Point.Multiply(right, left);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The point to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point3i operator /(Point3i left, int right)
		{
			return Point.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Point3d value to a Point3i.
		/// </summary>
		/// <param name="value">The value to convert to a Point3i.</param>
		/// <returns>A Point3i that has all components equal to value.</returns>
		public static explicit operator Point3i(Point3d value)
		{
			return new Point3i((int)value.X, (int)value.Y, (int)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Point3f value to a Point3i.
		/// </summary>
		/// <param name="value">The value to convert to a Point3i.</param>
		/// <returns>A Point3i that has all components equal to value.</returns>
		public static explicit operator Point3i(Point3f value)
		{
			return new Point3i((int)value.X, (int)value.Y, (int)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Point3l value to a Point3i.
		/// </summary>
		/// <param name="value">The value to convert to a Point3i.</param>
		/// <returns>A Point3i that has all components equal to value.</returns>
		public static explicit operator Point3i(Point3l value)
		{
			return new Point3i((int)value.X, (int)value.Y, (int)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3i value to a Point3i.
		/// </summary>
		/// <param name="value">The value to convert to a Point3i.</param>
		/// <returns>A Point3i that has all components equal to value.</returns>
		public static explicit operator Point3i(Vector3i value)
		{
			return new Point3i(value.X, value.Y, value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Point3i value to a Vector3i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3i.</param>
		/// <returns>A Vector3i that has all components equal to value.</returns>
		public static explicit operator Vector3i(Point3i value)
		{
			return new Vector3i(value.X, value.Y, value.Z);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Point3i"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Point3i"/> object or a type capable
		/// of implicit conversion to a <see cref="Point3i"/> object, and its value
		/// is equal to the current <see cref="Point3i"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Point3i) { return Equals((Point3i)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// point have the same value.
		/// </summary>
		/// <param name="other">The point to compare.</param>
		/// <returns>true if this point and value have the same value; otherwise, false.</returns>
		public bool Equals(Point3i other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Point3i left, Point3i right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are not equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Point3i left, Point3i right)
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
		/// Writes the given <see cref="Point3i"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Point3i point)
		{
			writer.Write(point.X);
			writer.Write(point.Y);
			writer.Write(point.Z);
		}
		/// <summary>
		/// Reads a <see cref="Point3i"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Point3i ReadPoint3i(this Ibasa.IO.BinaryReader reader)
		{
			return new Point3i(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point3i Add(Point3i point, Vector3i vector)
		{
			return new Point3i(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
		}
		/// <summary>
		/// Subtracts one points from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3i Subtract(Point3i left, Point3i right)
		{
			return new Vector3i(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point3i Subtract(Point3i point, Vector3i vector)
		{
			return new Point3i(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="point">The point to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point3i Multiply(Point3i point, int scalar)
		{
			return new Point3i(point.X * scalar, point.Y * scalar, point.Z * scalar);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="point">The point to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point3i Divide(Point3i point, int scalar)
		{
			return new Point3i(point.X / scalar, point.Y / scalar, point.Z / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Point3i left, Point3i right)
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
		public static float Distance(Point3i value1, Point3i value2)
		{
			return Vector.Absolute(value2 - value1);
		}
		/// <summary>
		/// Returns the squared distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The squared distance between value1 and value2.</returns>
		public static int DistanceSquared(Point3i value1, Point3i value2)
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
		public static bool All(Point3i value)
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
		public static bool All(Point3i value, Predicate<int> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z);
		}
		/// <summary>
		/// Determines whether any component of a point is non-zero.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Point3i value)
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
		public static bool Any(Point3i value, Predicate<int> predicate)
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
		public static Point3d Transform(Point3i value, Func<int, double> transformer)
		{
			return new Point3d(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point3f Transform(Point3i value, Func<int, float> transformer)
		{
			return new Point3f(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point3l Transform(Point3i value, Func<int, long> transformer)
		{
			return new Point3l(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point3i Transform(Point3i value, Func<int, int> transformer)
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
		public static Point3i Modulate(Point3i left, Point3i right)
		{
			return new Point3i(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Point3i Abs(Point3i value)
		{
			return new Point3i(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z));
		}
		/// <summary>
		/// Returns a point that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Point3i Min(Point3i value1, Point3i value2)
		{
			return new Point3i(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z));
		}
		/// <summary>
		/// Returns a point that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Point3i Max(Point3i value1, Point3i value2)
		{
			return new Point3i(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A point to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A point with each component constrained to the given range.</returns>
		public static Point3i Clamp(Point3i value, Point3i min, Point3i max)
		{
			return new Point3i(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z));
		}
		#endregion
	}
}
