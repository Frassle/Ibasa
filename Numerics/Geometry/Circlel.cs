using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a circle in a two-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Circlel: IEquatable<Circlel>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new unit <see cref="Circlel"/> at the origin.
		/// </summary>
		public static readonly Circlel Unit = new Circlel(0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the circle.
		/// </summary>
		public readonly long X;
		/// <summary>
		/// The Y component of the circle.
		/// </summary>
		public readonly long Y;
		/// <summary>
		/// The Radius component of the circle.
		/// </summary>
		public readonly long Radius;
		#endregion
		#region Properties
		/// <summary>
		/// Gets the diameter of this circle.
		/// </summary>
		public long Diameter { get { return Radius * 2; } }
		/// <summary>
		/// Gets the coordinates of the center of this circle.
		/// </summary>
		public Point2l Center { get { return new Point2l(X, Y); } }
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Circlel"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the circle.</param>
		/// <param name="y">Value for the Y component of the circle.</param>
		/// <param name="radius">Value for the Radius of the circle.</param>
		public Circlel(long x, long y, long radius)
		{
			Contract.Requires(0 <= radius);
			X = x;
			Y = y;
			Radius = radius;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Circlel"/> using the specified location and radius.
		/// </summary>
		/// <param name="center">The center of the circle.</param>
		/// <param name="radius">The radius of the circle.</param>
		public Circlel(Point2l center, long radius)
		{
			Contract.Requires(0 <= radius);
			X = center.X;
			Y = center.Y;
			Radius = radius;
		}
		#endregion
		#region Operations
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Circled value to a Circlel.
		/// </summary>
		/// <param name="value">The value to convert to a Circlel.</param>
		/// <returns>A Circlel that has all components equal to value.</returns>
		public static explicit operator Circlel(Circled value)
		{
			return new Circlel((long)value.X, (long)value.Y, (long)value.Radius);
		}
		/// <summary>
		/// Defines an explicit conversion of a Circlef value to a Circlel.
		/// </summary>
		/// <param name="value">The value to convert to a Circlel.</param>
		/// <returns>A Circlel that has all components equal to value.</returns>
		public static explicit operator Circlel(Circlef value)
		{
			return new Circlel((long)value.X, (long)value.Y, (long)value.Radius);
		}
		/// <summary>
		/// Defines an implicit conversion of a Circlei value to a Circlel.
		/// </summary>
		/// <param name="value">The value to convert to a Circlel.</param>
		/// <returns>A Circlel that has all components equal to value.</returns>
		public static implicit operator Circlel(Circlei value)
		{
			return new Circlel((long)value.X, (long)value.Y, (long)value.Radius);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Circlel"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode() + Radius.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Circlel"/> object or a type capable
		/// of implicit conversion to a <see cref="Circlel"/> object, and its value
		/// is equal to the current <see cref="Circlel"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Circlel) return Equals((Circlel)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// circle have the same value.
		/// </summary>
		/// <param name="other">The circle to compare.</param>
		/// <returns>true if this circle and value have the same value; otherwise, false.</returns>
		public bool Equals(Circlel other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two circles are equal.
		/// </summary>
		/// <param name="left">The first circle to compare.</param>
		/// <param name="right">The second circle to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Circlel left, Circlel right)
		{
			return left.X == right.X & left.Y == right.Y & left.Radius == right.Radius;
		}
		/// <summary>
		/// Returns a value that indicates whether two circles are not equal.
		/// </summary>
		/// <param name="left">The first circle to compare.</param>
		/// <param name="right">The second circle to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Circlel left, Circlel right)
		{
			return left.X != right.X | left.Y != right.Y | left.Radius != right.Radius;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current circle to its equivalent string
		/// representation.
		/// </summary>
		/// <returns>The string representation of the current instance.</returns>
		public override string ToString()
		{
			Contract.Ensures(Contract.Result<string>() != null);
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current circle to its equivalent string
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
		/// Converts the value of the current circle to its equivalent string
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
		/// Converts the value of the current circle to its equivalent string
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
	/// Provides static methods for circle functions.
	/// </summary>
	public static partial class Circle
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Circlel"/> to a System.IO.BinaryWriter.
		/// </summary>
		public static void Write(this System.IO.BinaryWriter writer, Circlel circle)
		{
			writer.Write(circle.X);
			writer.Write(circle.Y);
			writer.Write(circle.Radius);
		}
		/// <summary>
		/// Reads a <see cref="Circlel"/> to a System.IO.BinaryReader.
		/// </summary>
		public static Circlel ReadCirclel(this System.IO.BinaryReader reader)
		{
			return new Circlel(reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64());
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two circlees are equal.
		/// </summary>
		/// <param name="left">The first circle to compare.</param>
		/// <param name="right">The second circle to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Circlel left, Circlel right)
		{
			return left == right;
		}
		#endregion
		#region Contains
		public static bool Contains(Circlel circle, Point2l point)
		{
			return Vector.AbsoluteSquared(circle.Center - point) <= circle.Radius * circle.Radius;
		}
		#endregion
	}
}
