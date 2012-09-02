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
	public struct Spherel: IEquatable<Spherel>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new unit <see cref="Spherel"/> at the origin.
		/// </summary>
		public static readonly Spherel Unit = new Spherel(0, 0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the sphere.
		/// </summary>
		public readonly long X;
		/// <summary>
		/// The Y component of the sphere.
		/// </summary>
		public readonly long Y;
		/// <summary>
		/// The Z component of the sphere.
		/// </summary>
		public readonly long Z;
		/// <summary>
		/// The Radius component of the sphere.
		/// </summary>
		public readonly long Radius;
		#endregion
		#region Properties
		/// <summary>
		/// Gets the diameter of this sphere.
		/// </summary>
		public long Diameter { get { return Radius * 2; } }
		/// <summary>
		/// Gets the coordinates of the center of this sphere.
		/// </summary>
		public Point3l Center { get { return new Point3l(X, Y, Z); } }
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Spherel"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the sphere.</param>
		/// <param name="y">Value for the Y component of the sphere.</param>
		/// <param name="z">Value for the Z component of the sphere.</param>
		/// <param name="radius">Value for the Radius of the sphere.</param>
		public Spherel(long x, long y, long z, long radius)
		{
			Contract.Requires(0 <= radius);
			X = x;
			Y = y;
			Z = z;
			Radius = radius;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Spherel"/> using the specified location and radius.
		/// </summary>
		/// <param name="center">The center of the sphere.</param>
		/// <param name="radius">The radius of the sphere.</param>
		public Spherel(Point3l center, long radius)
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
		/// Defines an explicit conversion of a Sphered value to a Spherel.
		/// </summary>
		/// <param name="value">The value to convert to a Spherel.</param>
		/// <returns>A Spherel that has all components equal to value.</returns>
		public static explicit operator Spherel(Sphered value)
		{
			return new Spherel((long)value.X, (long)value.Y, (long)value.Z, (long)value.Radius);
		}
		/// <summary>
		/// Defines an explicit conversion of a Spheref value to a Spherel.
		/// </summary>
		/// <param name="value">The value to convert to a Spherel.</param>
		/// <returns>A Spherel that has all components equal to value.</returns>
		public static explicit operator Spherel(Spheref value)
		{
			return new Spherel((long)value.X, (long)value.Y, (long)value.Z, (long)value.Radius);
		}
		/// <summary>
		/// Defines an implicit conversion of a Spherei value to a Spherel.
		/// </summary>
		/// <param name="value">The value to convert to a Spherel.</param>
		/// <returns>A Spherel that has all components equal to value.</returns>
		public static implicit operator Spherel(Spherei value)
		{
			return new Spherel((long)value.X, (long)value.Y, (long)value.Z, (long)value.Radius);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Spherel"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Spherel"/> object or a type capable
		/// of implicit conversion to a <see cref="Spherel"/> object, and its value
		/// is equal to the current <see cref="Spherel"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Spherel) return Equals((Spherel)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// sphere have the same value.
		/// </summary>
		/// <param name="other">The sphere to compare.</param>
		/// <returns>true if this sphere and value have the same value; otherwise, false.</returns>
		public bool Equals(Spherel other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two spheres are equal.
		/// </summary>
		/// <param name="left">The first sphere to compare.</param>
		/// <param name="right">The second sphere to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Spherel left, Spherel right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z & left.Radius == right.Radius;
		}
		/// <summary>
		/// Returns a value that indicates whether two spheres are not equal.
		/// </summary>
		/// <param name="left">The first sphere to compare.</param>
		/// <param name="right">The second sphere to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Spherel left, Spherel right)
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
	public static partial class Sphere
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Spherel"/> to a System.IO.BinaryWriter.
		/// </summary>
		public static void Write(this System.IO.BinaryWriter writer, Spherel sphere)
		{
			writer.Write(sphere.X);
			writer.Write(sphere.Y);
			writer.Write(sphere.Z);
			writer.Write(sphere.Radius);
		}
		/// <summary>
		/// Reads a <see cref="Spherel"/> to a System.IO.BinaryReader.
		/// </summary>
		public static Spherel ReadSpherel(this System.IO.BinaryReader reader)
		{
			return new Spherel(reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64());
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two spherees are equal.
		/// </summary>
		/// <param name="left">The first sphere to compare.</param>
		/// <param name="right">The second sphere to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Spherel left, Spherel right)
		{
			return left == right;
		}
		#endregion
		#region Contains
		public static bool Contains(Spherel sphere, Point3l point)
		{
			return Vector.AbsoluteSquared(sphere.Center - point) <= sphere.Radius * sphere.Radius;
		}
		#endregion
	}
}
