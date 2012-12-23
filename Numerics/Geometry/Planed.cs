using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a plane as a normal vector and distance from the origin.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Planed: IEquatable<Planed>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns the yz plane.
		/// </summary>
		public static readonly Planed YZ = new Planed(new Vector3d(1, 0, 0), 0);
		/// <summary>
		/// Returns the xz plane.
		/// </summary>
		public static readonly Planed XZ = new Planed(new Vector3d(0, 1, 0), 0);
		/// <summary>
		/// Returns the xy plane.
		/// </summary>
		public static readonly Planed XY = new Planed(new Vector3d(0, 0, 1), 0);
		#endregion
		#region Fields
		/// <summary>
		/// The normal vector of the plane.
		/// </summary>
		public readonly Vector3d Normal;
		/// <summary>
		/// The distance of the plane along its normal from the origin.
		/// </summary>
		public readonly double D;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this plane.
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
						return Normal.X;
					case 1:
						return Normal.Y;
					case 2:
						return Normal.Z;
					case 3:
						return D;
					default:
						throw new IndexOutOfRangeException("Indices for Planed run from 0 to 3, inclusive.");
				}
			}
		}
		public double[] ToArray()
		{
			return new double[]
			{
				Normal.X, Normal.Y, Normal.Z, D
			};
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Planed"/> class.
		/// </summary>
		/// <param name="a">X component of the normal defining the plane.</param>
		/// <param name="b">Y component of the normal defining the plane.</param>
		/// <param name="c">Z component of the normal defining the plane.</param>
		/// <param name="d">Distance of the plane along its normal from the origin.</param>
		public Planed(double a, double b, double c, double d)
		{
			Normal = new Vector3d(a, b, c);
			D = d;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Planed"/> class.
		/// </summary>
		/// <param name="normal">The normal vector to the plane.</param>
		/// <param name="d">Distance of the plane along its normal from the origin.</param>
		public Planed(Vector3d normal, double d)
		{
			Normal = normal;
			D = d;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Planed"/> class.
		/// </summary>
		/// <param name="point">Any point that lies along the plane.</param>
		/// <param name="normal">The normal vector to the plane.</param>
		public Planed(Point3d point, Vector3d normal)
		{
			Normal = normal;
			D = -Point.Project(point, normal);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Planed"/> class.
		/// </summary>
		/// <param name="point1">First point of a triangle defining the plane.</param>
		/// <param name="point2">Second point of a triangle defining the plane.</param>
		/// <param name="point3">Third point of a triangle defining the plane.</param>
		public Planed(Point3d point1, Point3d point2, Point3d point3)
		{
			Normal = Vector.Normalize(Vector.Cross(point2 - point1, point3 - point1));
			D = -Point.Project(point1, Normal);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Planed"/> class.
		/// </summary>
		/// <param name="value">
		/// A vector with the X, Y, and Z components defining the normal to the plane.
		/// The W component defines the distance of the plane along its normal from the origin.
		/// </param>
		public Planed(Vector4d value)
		{
			Normal = new Vector3d(value.X, value.Y, value.Z);
			D = value.W;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Scales the plane by the given scaling factor.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="scale">The scaling factor.</param>
		/// <returns>The scaled plane.</returns>
		public static Planed operator *(Planed plane, double scale)
		{
			return Plane.Multiply(plane, scale);
		}
		/// <summary>
		/// Scales the plane by the given scaling factor.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="scale">The scaling factor.</param>
		/// <returns>The scaled plane.</returns>
		public static Planed operator *(double scale, Planed plane)
		{
			return Plane.Multiply(plane, scale);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a Planef value to a Planed.
		/// </summary>
		/// <param name="value">The value to convert to a Planed.</param>
		/// <returns>A Planed that has all components equal to value.</returns>
		public static implicit operator Planed(Planef value)
		{
			return new Planed((double)value.Normal.X, (double)value.Normal.Y, (double)value.Normal.Z, (double)value.D);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Planed"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Normal.GetHashCode() + D.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Planed"/> object or a type capable
		/// of implicit conversion to a <see cref="Planed"/> object, and its value
		/// is equal to the current <see cref="Planed"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Planed) { return Equals((Planed)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Planed other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Planed left, Planed right)
		{
			return left.Normal == right.Normal && left.D == right.D;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Planed left, Planed right)
		{
			return left.Normal != right.Normal || left.D != right.D;
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
			return String.Format("({0}, {1})", Normal.ToString(format, provider), D.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for plane functions.
	/// </summary>
	public static partial class Plane
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Planed"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Planed plane)
		{
			writer.Write(plane.Normal.X);
			writer.Write(plane.Normal.Y);
			writer.Write(plane.Normal.Z);
			writer.Write(plane.D);
		}
		/// <summary>
		/// Reads a <see cref="Planed"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Planed ReadPlaned(this Ibasa.IO.BinaryReader reader)
		{
			return new Planed(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the product of a plane and scalar.
		/// </summary>
		/// <param name="plane">The plane to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Planed Multiply(Planed plane, double scalar)
		{
			return new Planed(plane.Normal.X * scalar, plane.Normal.Y * scalar, plane.Normal.Z * scalar, plane.D * scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two planes are equal.
		/// </summary>
		/// <param name="left">The first plane to compare.</param>
		/// <param name="right">The second plane to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Planed left, Planed right)
		{
			return left == right;
		}
		#endregion
		#region Products
		/// <summary>
		/// Calculates the dot product of the specified vector and plane.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="vector">The source vector.</param>
		/// <returns>The dot product of the specified point and plane.</returns>
		public static double Dot(Planed plane, Vector4d vector)
		{
			return (plane.Normal.X * vector.X) + (plane.Normal.Y * vector.Y) + (plane.Normal.Z * vector.Z) + (plane.D * vector.W);
		}
		/// <summary>
		/// Calculates the dot product of a specified vector and the normal of the plane plus the distance value of the plane.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="point">The source point.</param>
		/// <returns>The dot product of a specified vector and the normal of the Plane plus the distance value of the plane.</returns>
		public static double DotCoordinate(Planed plane, Point3d point)
		{
			return (plane.Normal.X * point.X) + (plane.Normal.Y * point.Y) + (plane.Normal.Z * point.Z) + plane.D;
		}
		/// <summary>
		/// Calculates the dot product of the specified vector and the normal of the plane.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="normal">The source vector.</param>
		/// <returns>The dot product of the specified vector and the normal of the plane.</returns>
		public static double DotNormal(Planed plane, Vector3d normal)
		{
			return (plane.Normal.X * normal.X) + (plane.Normal.Y * normal.Y) + (plane.Normal.Z * normal.Z);
		}
		#endregion
	}
}
