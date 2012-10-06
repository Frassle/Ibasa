using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
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
	public struct Point3f: IEquatable<Point3f>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns the point (0, 0, 0).
		/// </summary>
		public static readonly Point3f Zero = new Point3f(0, 0, 0);
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
		/// <summary>
		/// The Z coordinate of the point.
		/// </summary>
		public readonly float Z;
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
					case 2:
						return Z;
					default:
						throw new IndexOutOfRangeException("Indices for Point3f run from 0 to 2, inclusive.");
				}
			}
		}
		public float[] ToArray()
		{
			return new float[]
			{
				X, Y, Z
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
		/// Returns the point (X, Z).
		/// </summary>
		public Point2f XZ
		{
			get
			{
				return new Point2f(X, Z);
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
		/// Returns the point (Y, Z).
		/// </summary>
		public Point2f YZ
		{
			get
			{
				return new Point2f(Y, Z);
			}
		}
		/// <summary>
		/// Returns the point (Z, X).
		/// </summary>
		public Point2f ZX
		{
			get
			{
				return new Point2f(Z, X);
			}
		}
		/// <summary>
		/// Returns the point (Z, Y).
		/// </summary>
		public Point2f ZY
		{
			get
			{
				return new Point2f(Z, Y);
			}
		}
		/// <summary>
		/// Returns the point (Z, Z).
		/// </summary>
		public Point2f ZZ
		{
			get
			{
				return new Point2f(Z, Z);
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
		/// Returns the point (X, X, Z).
		/// </summary>
		public Point3f XXZ
		{
			get
			{
				return new Point3f(X, X, Z);
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
		/// Returns the point (X, Y, Z).
		/// </summary>
		public Point3f XYZ
		{
			get
			{
				return new Point3f(X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the point (X, Z, X).
		/// </summary>
		public Point3f XZX
		{
			get
			{
				return new Point3f(X, Z, X);
			}
		}
		/// <summary>
		/// Returns the point (X, Z, Y).
		/// </summary>
		public Point3f XZY
		{
			get
			{
				return new Point3f(X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the point (X, Z, Z).
		/// </summary>
		public Point3f XZZ
		{
			get
			{
				return new Point3f(X, Z, Z);
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
		/// Returns the point (Y, X, Z).
		/// </summary>
		public Point3f YXZ
		{
			get
			{
				return new Point3f(Y, X, Z);
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
		/// <summary>
		/// Returns the point (Y, Y, Z).
		/// </summary>
		public Point3f YYZ
		{
			get
			{
				return new Point3f(Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the point (Y, Z, X).
		/// </summary>
		public Point3f YZX
		{
			get
			{
				return new Point3f(Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the point (Y, Z, Y).
		/// </summary>
		public Point3f YZY
		{
			get
			{
				return new Point3f(Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the point (Y, Z, Z).
		/// </summary>
		public Point3f YZZ
		{
			get
			{
				return new Point3f(Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the point (Z, X, X).
		/// </summary>
		public Point3f ZXX
		{
			get
			{
				return new Point3f(Z, X, X);
			}
		}
		/// <summary>
		/// Returns the point (Z, X, Y).
		/// </summary>
		public Point3f ZXY
		{
			get
			{
				return new Point3f(Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the point (Z, X, Z).
		/// </summary>
		public Point3f ZXZ
		{
			get
			{
				return new Point3f(Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the point (Z, Y, X).
		/// </summary>
		public Point3f ZYX
		{
			get
			{
				return new Point3f(Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the point (Z, Y, Y).
		/// </summary>
		public Point3f ZYY
		{
			get
			{
				return new Point3f(Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the point (Z, Y, Z).
		/// </summary>
		public Point3f ZYZ
		{
			get
			{
				return new Point3f(Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the point (Z, Z, X).
		/// </summary>
		public Point3f ZZX
		{
			get
			{
				return new Point3f(Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the point (Z, Z, Y).
		/// </summary>
		public Point3f ZZY
		{
			get
			{
				return new Point3f(Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the point (Z, Z, Z).
		/// </summary>
		public Point3f ZZZ
		{
			get
			{
				return new Point3f(Z, Z, Z);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3f"/> using the specified point and value.
		/// </summary>
		/// <param name="value">A point containing the values with which to initialize the X and Y coordinates</param>
		/// <param name="z">Value for the Z coordinate of the point.</param>
		public Point3f(Point2f value, float z)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3f"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X coordinate of the point.</param>
		/// <param name="y">Value for the Y coordinate of the point.</param>
		/// <param name="z">Value for the Z coordinate of the point.</param>
		public Point3f(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3f"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		public Point3f(float[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Point3f"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the point.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Point3f(float[] array, int offset)
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
		public static Point3f operator +(Point3f value)
		{
			return value;
		}
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point3f operator +(Point3f point, Vector3f vector)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Adds a vector and a point and returns the result.
		/// </summary>
		/// <param name="vector">The vector value to add.</param>
		/// <param name="point">The point value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point3f operator +(Vector3f vector, Point3f point)
		{
			return Point.Add(point, vector);
		}
		/// <summary>
		/// Subtracts one point from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3f operator -(Point3f left, Point3f right)
		{
			return Point.Subtract(left, right);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point3f operator -(Point3f point, Vector3f vector)
		{
			return Point.Subtract(point, vector);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="left">The point to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point3f operator *(Point3f left, float right)
		{
			return Point.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and point.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The point to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point3f operator *(float left, Point3f right)
		{
			return Point.Multiply(right, left);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The point to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point3f operator /(Point3f left, float right)
		{
			return Point.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Point3d value to a Point3f.
		/// </summary>
		/// <param name="value">The value to convert to a Point3f.</param>
		/// <returns>A Point3f that has all components equal to value.</returns>
		public static explicit operator Point3f(Point3d value)
		{
			return new Point3f((float)value.X, (float)value.Y, (float)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Point3l value to a Point3f.
		/// </summary>
		/// <param name="value">The value to convert to a Point3f.</param>
		/// <returns>A Point3f that has all components equal to value.</returns>
		public static implicit operator Point3f(Point3l value)
		{
			return new Point3f((float)value.X, (float)value.Y, (float)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Point3i value to a Point3f.
		/// </summary>
		/// <param name="value">The value to convert to a Point3f.</param>
		/// <returns>A Point3f that has all components equal to value.</returns>
		public static implicit operator Point3f(Point3i value)
		{
			return new Point3f((float)value.X, (float)value.Y, (float)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3f value to a Point3f.
		/// </summary>
		/// <param name="value">The value to convert to a Point3f.</param>
		/// <returns>A Point3f that has all components equal to value.</returns>
		public static explicit operator Point3f(Vector3f value)
		{
			return new Point3f(value.X, value.Y, value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Point3f value to a Vector3f.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3f.</param>
		/// <returns>A Vector3f that has all components equal to value.</returns>
		public static explicit operator Vector3f(Point3f value)
		{
			return new Vector3f(value.X, value.Y, value.Z);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Point3f"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Point3f"/> object or a type capable
		/// of implicit conversion to a <see cref="Point3f"/> object, and its value
		/// is equal to the current <see cref="Point3f"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Point3f) { return Equals((Point3f)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// point have the same value.
		/// </summary>
		/// <param name="other">The point to compare.</param>
		/// <returns>true if this point and value have the same value; otherwise, false.</returns>
		public bool Equals(Point3f other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Point3f left, Point3f right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z;
		}
		/// <summary>
		/// Returns a value that indicates whether two points are not equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Point3f left, Point3f right)
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
		/// Writes the given <see cref="Point3f"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Point3f point)
		{
			writer.Write(point.X);
			writer.Write(point.Y);
			writer.Write(point.Z);
		}
		/// <summary>
		/// Reads a <see cref="Point3f"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Point3f ReadPoint3f(this Ibasa.IO.BinaryReader reader)
		{
			return new Point3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Adds a point and a vector and returns the result.
		/// </summary>
		/// <param name="point">The point value to add.</param>
		/// <param name="vector">The vector value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Point3f Add(Point3f point, Vector3f vector)
		{
			return new Point3f(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
		}
		/// <summary>
		/// Subtracts one points from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3f Subtract(Point3f left, Point3f right)
		{
			return new Vector3f(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}
		/// <summary>
		/// Subtracts a vector from a point and returns the result.
		/// </summary>
		/// <param name="point">The point value to subtract from (the minuend).</param>
		/// <param name="vector">The vector value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting vector from point (the difference).</returns>
		public static Point3f Subtract(Point3f point, Vector3f vector)
		{
			return new Point3f(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);
		}
		/// <summary>
		/// Returns the product of a point and scalar.
		/// </summary>
		/// <param name="point">The point to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Point3f Multiply(Point3f point, float scalar)
		{
			return new Point3f(point.X * scalar, point.Y * scalar, point.Z * scalar);
		}
		/// <summary>
		/// Divides a point by a scalar and returns the result.
		/// </summary>
		/// <param name="point">The point to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Point3f Divide(Point3f point, float scalar)
		{
			return new Point3f(point.X / scalar, point.Y / scalar, point.Z / scalar);
		}
		public static Point3f Sum(IEnumerable<Point3f> points, IEnumerable<float> weights)
		{
			Contract.Requires(weights.Sum() == 1.0);
			float sumX = 0;
			float sumY = 0;
			float sumZ = 0;
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
			return new Point3f(sumX, sumY, sumZ);
		}
		public static Point3f Sum(IEnumerable<Point3f> points, float weight)
		{
			Contract.Requires(weight * points.Count() == 1.0);
			float sumX = 0;
			float sumY = 0;
			float sumZ = 0;
			foreach (var point in points)
			{
				sumX += point.X * weight;
				sumY += point.Y * weight;
				sumZ += point.Z * weight;
			}
			return new Point3f(sumX, sumY, sumZ);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two points are equal.
		/// </summary>
		/// <param name="left">The first point to compare.</param>
		/// <param name="right">The second point to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Point3f left, Point3f right)
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
		public static float Distance(Point3f value1, Point3f value2)
		{
			return Vector.Absolute(value2 - value1);
		}
		/// <summary>
		/// Returns the squared distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The squared distance between value1 and value2.</returns>
		public static float DistanceSquared(Point3f value1, Point3f value2)
		{
			return Vector.AbsoluteSquared(value2 - value1);
		}
		/// <summary>
		/// Returns the manhatten distance between two points.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The manhatten distance between value1 and value2.</returns>
		public static float ManhattenDistance(Point3f value1, Point3f value2)
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
		public static bool All(Point3f value)
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
		public static bool All(Point3f value, Predicate<float> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z);
		}
		/// <summary>
		/// Determines whether any component of a point is non-zero.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Point3f value)
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
		public static bool Any(Point3f value, Predicate<float> predicate)
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
		public static Point3d Transform(Point3f value, Func<float, double> transformer)
		{
			return new Point3d(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point3f Transform(Point3f value, Func<float, float> transformer)
		{
			return new Point3f(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point3l Transform(Point3f value, Func<float, long> transformer)
		{
			return new Point3l(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a point and returns the result.
		/// </summary>
		/// <param name="value">The point to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Point3i Transform(Point3f value, Func<float, int> transformer)
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
		public static Point3f Modulate(Point3f left, Point3f right)
		{
			return new Point3f(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Point3f Abs(Point3f value)
		{
			return new Point3f(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z));
		}
		/// <summary>
		/// Returns a point that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Point3f Min(Point3f value1, Point3f value2)
		{
			return new Point3f(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z));
		}
		/// <summary>
		/// Returns a point that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Point3f Max(Point3f value1, Point3f value2)
		{
			return new Point3f(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A point to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A point with each component constrained to the given range.</returns>
		public static Point3f Clamp(Point3f value, Point3f min, Point3f max)
		{
			return new Point3f(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z));
		}
		/// <summary>
		/// Constrains each component to the range 0 to 1.
		/// </summary>
		/// <param name="value">A point to saturate.</param>
		/// <returns>A point with each component constrained to the range 0 to 1.</returns>
		public static Point3f Saturate(Point3f value)
		{
			return new Point3f(Functions.Saturate(value.X), Functions.Saturate(value.Y), Functions.Saturate(value.Z));
		}
		/// <summary>
		/// Returns a point where each component is the smallest integral value that
		/// is greater than or equal to the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The ceiling of value.</returns>
		public static Point3f Ceiling(Point3f value)
		{
			return new Point3f(Functions.Ceiling(value.X), Functions.Ceiling(value.Y), Functions.Ceiling(value.Z));
		}
		/// <summary>
		/// Returns a point where each component is the largest integral value that
		/// is less than or equal to the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The floor of value.</returns>
		public static Point3f Floor(Point3f value)
		{
			return new Point3f(Functions.Floor(value.X), Functions.Floor(value.Y), Functions.Floor(value.Z));
		}
		/// <summary>
		/// Returns a point where each component is the integral part of the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The integral of value.</returns>
		public static Point3f Truncate(Point3f value)
		{
			return new Point3f(Functions.Truncate(value.X), Functions.Truncate(value.Y), Functions.Truncate(value.Z));
		}
		/// <summary>
		/// Returns a point where each component is the fractional part of the specified component.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The fractional of value.</returns>
		public static Point3f Fractional(Point3f value)
		{
			return new Point3f(Functions.Fractional(value.X), Functions.Fractional(value.Y), Functions.Fractional(value.Z));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point3f Round(Point3f value)
		{
			return new Point3f(Functions.Round(value.X), Functions.Round(value.Y), Functions.Round(value.Z));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point3f Round(Point3f value, int digits)
		{
			return new Point3f(Functions.Round(value.X, digits), Functions.Round(value.Y, digits), Functions.Round(value.Z, digits));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point3f Round(Point3f value, MidpointRounding mode)
		{
			return new Point3f(Functions.Round(value.X, mode), Functions.Round(value.Y, mode), Functions.Round(value.Z, mode));
		}
		/// <summary>
		/// Returns a point where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Point3f Round(Point3f value, int digits, MidpointRounding mode)
		{
			return new Point3f(Functions.Round(value.X, digits, mode), Functions.Round(value.Y, digits, mode), Functions.Round(value.Z, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each component in the point.
		/// </summary>
		/// <param name="value">A point.</param>
		/// <returns>A point with the reciprocal of each of values components.</returns>
		public static Point3f Reciprocal(Point3f value)
		{
			return new Point3f(1 / value.X, 1 / value.Y, 1 / value.Z);
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
		public static Point3f Lerp(Point3f point1, Point3f point2, float amount)
		{
			return point1 + (point2 - point1) * amount;
		}
		#endregion
	}
}
