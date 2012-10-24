using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a ellipse in a two-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Ellipsel: IEquatable<Ellipsel>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new unit <see cref="Ellipsel"/> at the origin.
		/// </summary>
		public static readonly Ellipsel Unit = new Ellipsel(0, 0, 1, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the ellipse.
		/// </summary>
		public readonly long X;
		/// <summary>
		/// The Y component of the ellipse.
		/// </summary>
		public readonly long Y;
		/// <summary>
		/// The Width of the ellipse.
		/// </summary>
		public readonly long Width;
		/// <summary>
		/// The Height of the ellipse.
		/// </summary>
		public readonly long Height;
		#endregion
		#region Properties
		/// <summary>
		/// Gets the coordinates of the center of this ellipse.
		/// </summary>
		public Point2l Center { get { return new Point2l(X, Y); } }
		/// <summary>
		/// Gets the size of this ellipse.
		/// </summary>
		public Size2l Size { get { return new Size2l(Width, Height); } }
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Ellipsel"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the ellipse.</param>
		/// <param name="y">Value for the Y component of the ellipse.</param>
		/// <param name="width">Value for the width of the ellipse.</param>
		/// <param name="height">Value for the height of the ellipse.</param>
		public Ellipsel(long x, long y, long width, long height)
		{
			Contract.Requires(0 <= width);
			Contract.Requires(0 <= height);
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Ellipsel"/> using the specified location and radius.
		/// </summary>
		/// <param name="center">The center of the ellipse.</param>
		/// <param name="width">The width of the ellipse.</param>
		/// <param name="height">The height of the ellipse.</param>
		public Ellipsel(Point2l center, long width, long height)
		{
			Contract.Requires(0 <= width);
			Contract.Requires(0 <= height);
			X = center.X;
			Y = center.Y;
			Width = width;
			Height = height;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Ellipsel"/> using the specified location and radius.
		/// </summary>
		/// <param name="center">The center of the ellipse.</param>
		/// <param name="size">The size of the ellipse.</param>
		public Ellipsel(Point2l center, Size2l size)
		{
			X = center.X;
			Y = center.Y;
			Width = size.Width;
			Height = size.Height;
		}
		#endregion
		#region Operations
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Ellipsed value to a Ellipsel.
		/// </summary>
		/// <param name="value">The value to convert to a Ellipsel.</param>
		/// <returns>A Ellipsel that has all components equal to value.</returns>
		public static explicit operator Ellipsel(Ellipsed value)
		{
			return new Ellipsel((long)value.X, (long)value.Y, (long)value.Width, (long)value.Height);
		}
		/// <summary>
		/// Defines an explicit conversion of a Ellipsef value to a Ellipsel.
		/// </summary>
		/// <param name="value">The value to convert to a Ellipsel.</param>
		/// <returns>A Ellipsel that has all components equal to value.</returns>
		public static explicit operator Ellipsel(Ellipsef value)
		{
			return new Ellipsel((long)value.X, (long)value.Y, (long)value.Width, (long)value.Height);
		}
		/// <summary>
		/// Defines an implicit conversion of a Ellipsei value to a Ellipsel.
		/// </summary>
		/// <param name="value">The value to convert to a Ellipsel.</param>
		/// <returns>A Ellipsel that has all components equal to value.</returns>
		public static implicit operator Ellipsel(Ellipsei value)
		{
			return new Ellipsel((long)value.X, (long)value.Y, (long)value.Width, (long)value.Height);
		}
		/// <summary>
		/// Defines an explicit conversion of a Circled value to a Ellipsel.
		/// </summary>
		/// <param name="value">The value to convert to a Ellipsel.</param>
		/// <returns>A Ellipsel that has all components equal to value.</returns>
		public static explicit operator Ellipsel(Circled value)
		{
			return new Ellipsel((long)value.X, (long)value.Y, (long)(value.Radius * 2), (long)(value.Radius * 2));
		}
		/// <summary>
		/// Defines an explicit conversion of a Circlef value to a Ellipsel.
		/// </summary>
		/// <param name="value">The value to convert to a Ellipsel.</param>
		/// <returns>A Ellipsel that has all components equal to value.</returns>
		public static explicit operator Ellipsel(Circlef value)
		{
			return new Ellipsel((long)value.X, (long)value.Y, (long)(value.Radius * 2), (long)(value.Radius * 2));
		}
		/// <summary>
		/// Defines an implicit conversion of a Circlei value to a Ellipsel.
		/// </summary>
		/// <param name="value">The value to convert to a Ellipsel.</param>
		/// <returns>A Ellipsel that has all components equal to value.</returns>
		public static implicit operator Ellipsel(Circlei value)
		{
			return new Ellipsel((long)value.X, (long)value.Y, (long)(value.Radius * 2), (long)(value.Radius * 2));
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Ellipsel"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode() + Width.GetHashCode() + Height.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Ellipsel"/> object or a type capable
		/// of implicit conversion to a <see cref="Ellipsel"/> object, and its value
		/// is equal to the current <see cref="Ellipsel"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Ellipsel) return Equals((Ellipsel)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// ellipse have the same value.
		/// </summary>
		/// <param name="other">The ellipse to compare.</param>
		/// <returns>true if this ellipse and value have the same value; otherwise, false.</returns>
		public bool Equals(Ellipsel other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two ellipses are equal.
		/// </summary>
		/// <param name="left">The first ellipse to compare.</param>
		/// <param name="right">The second ellipse to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Ellipsel left, Ellipsel right)
		{
			return left.X == right.X & left.Y == right.Y & left.Width == right.Width & left.Height == right.Height;
		}
		/// <summary>
		/// Returns a value that indicates whether two ellipses are not equal.
		/// </summary>
		/// <param name="left">The first ellipse to compare.</param>
		/// <param name="right">The second ellipse to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Ellipsel left, Ellipsel right)
		{
			return left.X != right.X | left.Y != right.Y | left.Width != right.Width & left.Height != right.Height;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current ellipse to its equivalent string
		/// representation.
		/// </summary>
		/// <returns>The string representation of the current instance.</returns>
		public override string ToString()
		{
			Contract.Ensures(Contract.Result<string>() != null);
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current ellipse to its equivalent string
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
		/// Converts the value of the current ellipse to its equivalent string
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
		/// Converts the value of the current ellipse to its equivalent string
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
			return String.Format("({0}, {1})", Center.ToString(format, provider), Width.ToString(format, provider), Height.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for ellipse functions.
	/// </summary>
	public static partial class Ellipse
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Ellipsel"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Ellipsel ellipse)
		{
			writer.Write(ellipse.X);
			writer.Write(ellipse.Y);
			writer.Write(ellipse.Width);
			writer.Write(ellipse.Height);
		}
		/// <summary>
		/// Reads a <see cref="Ellipsel"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Ellipsel ReadEllipsel(this Ibasa.IO.BinaryReader reader)
		{
			return new Ellipsel(reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64());
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two ellipsees are equal.
		/// </summary>
		/// <param name="left">The first ellipse to compare.</param>
		/// <param name="right">The second ellipse to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Ellipsel left, Ellipsel right)
		{
			return left == right;
		}
		#endregion
	}
}
