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
	public struct Circled: IEquatable<Circled>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new unit <see cref="Circled"/> at the origin.
		/// </summary>
		public static readonly Circled Unit = new Circled(0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the circle.
		/// </summary>
		public readonly double X;
		/// <summary>
		/// The Y component of the circle.
		/// </summary>
		public readonly double Y;
		/// <summary>
		/// The Radius component of the circle.
		/// </summary>
		public readonly double Radius;
		#endregion
		#region Properties
		/// <summary>
		/// Gets the diameter of this circle.
		/// </summary>
		public double Diameter { get { return Radius * 2; } }
		/// <summary>
		/// Gets the coordinates of the center of this circle.
		/// </summary>
		public Point2d Center { get { return new Point2d(X, Y); } }
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Circled"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the circle.</param>
		/// <param name="y">Value for the Y component of the circle.</param>
		/// <param name="radius">Value for the Radius of the circle.</param>
		public Circled(double x, double y, double radius)
		{
			Contract.Requires(0 <= radius);
			X = x;
			Y = y;
			Radius = radius;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Circled"/> using the specified location and radius.
		/// </summary>
		/// <param name="center">The center of the circle.</param>
		/// <param name="radius">The radius of the circle.</param>
		public Circled(Point2d center, double radius)
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
		/// Defines an implicit conversion of a Circlef value to a Circled.
		/// </summary>
		/// <param name="value">The value to convert to a Circled.</param>
		/// <returns>A Circled that has all components equal to value.</returns>
		public static implicit operator Circled(Circlef value)
		{
			return new Circled((double)value.X, (double)value.Y, (double)value.Radius);
		}
		/// <summary>
		/// Defines an implicit conversion of a Circlel value to a Circled.
		/// </summary>
		/// <param name="value">The value to convert to a Circled.</param>
		/// <returns>A Circled that has all components equal to value.</returns>
		public static implicit operator Circled(Circlel value)
		{
			return new Circled((double)value.X, (double)value.Y, (double)value.Radius);
		}
		/// <summary>
		/// Defines an implicit conversion of a Circlei value to a Circled.
		/// </summary>
		/// <param name="value">The value to convert to a Circled.</param>
		/// <returns>A Circled that has all components equal to value.</returns>
		public static implicit operator Circled(Circlei value)
		{
			return new Circled((double)value.X, (double)value.Y, (double)value.Radius);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Circled"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Circled"/> object or a type capable
		/// of implicit conversion to a <see cref="Circled"/> object, and its value
		/// is equal to the current <see cref="Circled"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Circled) return Equals((Circled)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// circle have the same value.
		/// </summary>
		/// <param name="other">The circle to compare.</param>
		/// <returns>true if this circle and value have the same value; otherwise, false.</returns>
		public bool Equals(Circled other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two circles are equal.
		/// </summary>
		/// <param name="left">The first circle to compare.</param>
		/// <param name="right">The second circle to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Circled left, Circled right)
		{
			return left.X == right.X & left.Y == right.Y & left.Radius == right.Radius;
		}
		/// <summary>
		/// Returns a value that indicates whether two circles are not equal.
		/// </summary>
		/// <param name="left">The first circle to compare.</param>
		/// <param name="right">The second circle to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Circled left, Circled right)
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
		/// Writes the given <see cref="Circled"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Circled circle)
		{
			writer.Write(circle.X);
			writer.Write(circle.Y);
			writer.Write(circle.Radius);
		}
		/// <summary>
		/// Reads a <see cref="Circled"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Circled ReadCircled(this Ibasa.IO.BinaryReader reader)
		{
			return new Circled(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two circlees are equal.
		/// </summary>
		/// <param name="left">The first circle to compare.</param>
		/// <param name="right">The second circle to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Circled left, Circled right)
		{
			return left == right;
		}
		#endregion
		#region Contains
		public static bool Contains(Circled circle, Point2d point)
		{
			return Vector.AbsoluteSquared(circle.Center - point) <= circle.Radius * circle.Radius;
		}
		#endregion
	}
}
