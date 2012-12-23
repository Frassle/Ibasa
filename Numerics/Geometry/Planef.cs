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
	public struct Planef: IEquatable<Planef>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns the yz plane.
		/// </summary>
		public static readonly Planef YZ = new Planef(new Vector3f(1, 0, 0), 0);
		/// <summary>
		/// Returns the xz plane.
		/// </summary>
		public static readonly Planef XZ = new Planef(new Vector3f(0, 1, 0), 0);
		/// <summary>
		/// Returns the xy plane.
		/// </summary>
		public static readonly Planef XY = new Planef(new Vector3f(0, 0, 1), 0);
		#endregion
		#region Fields
		/// <summary>
		/// The normal vector of the plane.
		/// </summary>
		public readonly Vector3f Normal;
		/// <summary>
		/// The distance of the plane along its normal from the origin.
		/// </summary>
		public readonly float D;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this plane.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return normal.X;
					case 1:
						return normal.Y;
					case 2:
						return normal.Z;
					case 3:
						return D;
					default:
						throw new IndexOutOfRangeException("Indices for Planef run from 0 to 3, inclusive.");
				}
			}
		}
		public float[] ToArray()
		{
			return new float[]
			{
				normal.X, normal.Y, normal.Z, D
			};
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Planef"/> class.
		/// </summary>
		/// <param name="a">X component of the normal defining the plane.</param>
		/// <param name="b">Y component of the normal defining the plane.</param>
		/// <param name="c">Z component of the normal defining the plane.</param>
		/// <param name="d">Distance of the plane along its normal from the origin.</param>
		public Planef(float a, float b, float c, float d)
		{
			Normal = new Vector3f(a, b, c);
			D = d;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Planef"/> class.
		/// </summary>
		/// <param name="normal">The normal vector to the plane.</param>
		/// <param name="d">Distance of the plane along its normal from the origin.</param>
		public Planef(Vector3f normal, float d)
		{
			Normal = normal;
			D = d;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Planef"/> class.
		/// </summary>
		/// <param name="point">Any point that lies along the plane.</param>
		/// <param name="normal">The normal vector to the plane.</param>
		public Planef(Point3f point, Vector3f normal)
		{
			Normal = normal;
			D = -Vector.Dot(normal, point);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Planef"/> class.
		/// </summary>
		/// <param name="point1">First point of a triangle defining the plane.</param>
		/// <param name="point2">Second point of a triangle defining the plane.</param>
		/// <param name="point3">Third point of a triangle defining the plane.</param>
		public Planef(Point3f point1, Point3f point2, Point3f point3)
		{
			Normal = Vector.Normalize(Vector.Cross(point2 - point1, point3 - point1));
			D = -Vector.Dot(Normal, point1);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Planef"/> class.
		/// </summary>
		/// <param name="value">
		/// A vector with the X, Y, and Z components defining the normal to the plane.
		/// The W component defines the distance of the plane along its normal from the origin.
		/// </param>
		public Planef(Vector4f value)
		{
			Normal = new Vector3f(value.X, value.Y, value.Z);
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
		public static Planef operator *(Planef plane, float scale)
		{
			return Plane.Multiply(plane, scale);
		}
		/// <summary>
		/// Scales the plane by the given scaling factor.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="scale">The scaling factor.</param>
		/// <returns>The scaled plane.</returns>
		public static Planef operator *(float scale, Planef plane)
		{
			return Plane.Multiply(plane, scale);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Planed value to a Planef.
		/// </summary>
		/// <param name="value">The value to convert to a Planef.</param>
		/// <returns>A Planef that has all components equal to value.</returns>
		public static explicit operator Planef(Planed value)
		{
			return new Planef((float)value.Normal.X, (float)value.Normal.Y, (float)value.Normal.Z, (float)value.D);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Planef"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Planef"/> object or a type capable
		/// of implicit conversion to a <see cref="Planef"/> object, and its value
		/// is equal to the current <see cref="Planef"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Planef) { return Equals((Planef)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Planef other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Planef left, Planef right)
		{
			return left.Normal == right.Normal && left.D == right.D;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Planef left, Planef right)
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
		/// Writes the given <see cref="Planef"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Planef plane)
		{
			writer.Write(plane.Normal.X);
			writer.Write(plane.Normal.Y);
			writer.Write(plane.Normal.Z);
			writer.Write(plane.D);
		}
		/// <summary>
		/// Reads a <see cref="Planef"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Planef ReadPlanef(this Ibasa.IO.BinaryReader reader)
		{
			return new Planef(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the product of a plane and scalar.
		/// </summary>
		/// <param name="plane">The plane to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Planef Multiply(Planef plane, float scalar)
		{
			return new Planef(vector.Normal.X * scalar, vector.Normal.Y * scalar, vector.Normal.Z * scalar, vector.D * scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two planes are equal.
		/// </summary>
		/// <param name="left">The first plane to compare.</param>
		/// <param name="right">The second plane to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Planef left, Planef right)
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
		public static float Dot(Planef plane, Vector4f vector)
		{
			return (plane.Normal.X * vector.X) + (plane.Normal.Y * vector.Y) + (plane.Normal.Z * vector.Z) + (plane.D * vector.W);
		}
		/// <summary>
		/// Calculates the dot product of a specified vector and the normal of the plane plus the distance value of the plane.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="vector">The source vector.</param>
		/// <returns>The dot product of a specified vector and the normal of the Plane plus the distance value of the plane.</returns>
		public static float DotCoordinate(Planef plane, Vector3f vector)
		{
			return (plane.Normal.X * vector.X) + (plane.Normal.Y * vector.Y) + (plane.Normal.Z * vector.Z) + plane.D;
		}
		/// <summary>
		/// Calculates the dot product of the specified vector and the normal of the plane.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="normal">The source vector.</param>
		/// <returns>The dot product of the specified vector and the normal of the plane.</returns>
		public static float DotNormal(Planef plane, Vector3f normal)
		{
			return (plane.Normal.X * normal.X) + (plane.Normal.Y * normal.Y) + (plane.Normal.Z * normal.Z);
		}
		#endregion
	}
}
