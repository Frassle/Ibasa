using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a sphere in a three-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Spheref: IEquatable<Spheref>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new unit <see cref="Spheref"/> at the origin.
		/// </summary>
		public static readonly Spheref Unit = new Spheref(0, 0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the sphere.
		/// </summary>
		public readonly float X;
		/// <summary>
		/// The Y component of the sphere.
		/// </summary>
		public readonly float Y;
		/// <summary>
		/// The Z component of the sphere.
		/// </summary>
		public readonly float Z;
		/// <summary>
		/// The Radius component of the sphere.
		/// </summary>
		public readonly float Radius;
		#endregion
		#region Properties
		/// <summary>
		/// Gets the diameter of this sphere.
		/// </summary>
		public float Diameter { get { return Radius * 2; } }
		/// <summary>
		/// Gets the coordinates of the center of this sphere.
		/// </summary>
		public Point3f Center { get { return new Point3f(X, Y, Z); } }
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Spheref"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the sphere.</param>
		/// <param name="y">Value for the Y component of the sphere.</param>
		/// <param name="z">Value for the Z component of the sphere.</param>
		/// <param name="radius">Value for the Radius of the sphere.</param>
		public Spheref(float x, float y, float z, float radius)
		{
			Contract.Requires(0 <= radius);
			X = x;
			Y = y;
			Z = z;
			Radius = radius;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Spheref"/> using the specified location and radius.
		/// </summary>
		/// <param name="center">The center of the sphere.</param>
		/// <param name="radius">The radius of the sphere.</param>
		public Spheref(Point3f center, float radius)
		{
			Contract.Requires(0 <= radius);
			X = center.X;
			Y = center.Y;
			Z = center.Z;
			Radius = radius;
		}
		#endregion
		#region Operations
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Sphered value to a Spheref.
		/// </summary>
		/// <param name="value">The value to convert to a Spheref.</param>
		/// <returns>A Spheref that has all components equal to value.</returns>
		public static explicit operator Spheref(Sphered value)
		{
			return new Spheref((float)value.X, (float)value.Y, (float)value.Z, (float)value.Radius);
		}
		/// <summary>
		/// Defines an implicit conversion of a Spherel value to a Spheref.
		/// </summary>
		/// <param name="value">The value to convert to a Spheref.</param>
		/// <returns>A Spheref that has all components equal to value.</returns>
		public static implicit operator Spheref(Spherel value)
		{
			return new Spheref((float)value.X, (float)value.Y, (float)value.Z, (float)value.Radius);
		}
		/// <summary>
		/// Defines an implicit conversion of a Spherei value to a Spheref.
		/// </summary>
		/// <param name="value">The value to convert to a Spheref.</param>
		/// <returns>A Spheref that has all components equal to value.</returns>
		public static implicit operator Spheref(Spherei value)
		{
			return new Spheref((float)value.X, (float)value.Y, (float)value.Z, (float)value.Radius);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Spheref"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + Radius.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Spheref"/> object or a type capable
		/// of implicit conversion to a <see cref="Spheref"/> object, and its value
		/// is equal to the current <see cref="Spheref"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Spheref) return Equals((Spheref)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// sphere have the same value.
		/// </summary>
		/// <param name="other">The sphere to compare.</param>
		/// <returns>true if this sphere and value have the same value; otherwise, false.</returns>
		public bool Equals(Spheref other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two spheres are equal.
		/// </summary>
		/// <param name="left">The first sphere to compare.</param>
		/// <param name="right">The second sphere to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Spheref left, Spheref right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z & left.Radius == right.Radius;
		}
		/// <summary>
		/// Returns a value that indicates whether two spheres are not equal.
		/// </summary>
		/// <param name="left">The first sphere to compare.</param>
		/// <param name="right">The second sphere to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Spheref left, Spheref right)
		{
			return left.X != right.X | left.Y != right.Y | left.Z != right.Z | left.Radius != right.Radius;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current sphere to its equivalent string
		/// representation.
		/// </summary>
		/// <returns>The string representation of the current instance.</returns>
		public override string ToString()
		{
			Contract.Ensures(Contract.Result<string>() != null);
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current sphere to its equivalent string
		/// representation by using the specified culture-specific
		/// formatting information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance, as specified
		/// by provider.</returns>
		public string ToString(IFormatProvider provider)
		{
			Contract.Ensures(Contract.Result<string>() != null);
			return ToString("G", provider);
		}
		/// <summary>
		/// Converts the value of the current sphere to its equivalent string
		/// representation by using the specified format for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format)
		{
			Contract.Ensures(Contract.Result<string>() != null);
			return ToString(format, CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current sphere to its equivalent string
		/// representation by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, IFormatProvider provider)
		{
			Contract.Ensures(Contract.Result<string>() != null);
			return String.Format("({0}, {1})", Center.ToString(format, provider), Radius.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for sphere functions.
	/// </summary>
	public static partial class sphere
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Spheref"/> to a System.IO.BinaryWriter.
		/// </summary>
		public static void Write(this System.IO.BinaryWriter writer, Spheref sphere)
		{
			writer.Write(sphere.X);
			writer.Write(sphere.Y);
			writer.Write(sphere.Z);
			writer.Write(sphere.Radius);
		}
		/// <summary>
		/// Reads a <see cref="Spheref"/> to a System.IO.BinaryReader.
		/// </summary>
		public static Spheref ReadSpheref(this System.IO.BinaryReader reader)
		{
			return new Spheref(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two spherees are equal.
		/// </summary>
		/// <param name="left">The first sphere to compare.</param>
		/// <param name="right">The second sphere to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Spheref left, Spheref right)
		{
			return left == right;
		}
		#endregion
		#region Contains
		public static bool Contains(Spheref sphere, Point3f point)
		{
			return Vector.AbsoluteSquared(sphere.Center - point) <= sphere.Radius * sphere.Radius;
		}
		#endregion
	}
}
