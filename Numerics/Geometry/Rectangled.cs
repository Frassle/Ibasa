using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents an ordered tuple of integer x, y, width, and height components that defines a
	/// location and size in a two-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Rectangled: IEquatable<Rectangled>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Rectangled"/> that has an X, Y, Width and Height value of 0.
		/// </summary>
		public static readonly Rectangled Empty = new Rectangled(0, 0, 0, 0);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the rectangle.
		/// </summary>
		public readonly double X;
		/// <summary>
		/// The Y component of the rectangle.
		/// </summary>
		public readonly double Y;
		/// <summary>
		/// The Width component of the rectangle.
		/// </summary>
		public readonly double Width;
		/// <summary>
		/// The Height component of the rectangle.
		/// </summary>
		public readonly double Height;
		#endregion
		#region Properties
		/// <summary>
		/// Gets the y-coordinate of the top edge of this rectangle.
		/// </summary>
		public double Top { get { return Y + Height; } }
		/// <summary>
		/// Gets the y-coordinate of the bottom edge of this rectangle.
		/// </summary>
		public double Bottom { get { return Y; } }
		/// <summary>
		/// Gets the x-coordinate of the left edge of this rectangle.
		/// </summary>
		public double Left { get { return X; } }
		/// <summary>
		/// Gets the x-coordinate of the right edge of this rectangle.
		/// </summary>
		public double Right { get { return X + Width; } }
		/// <summary>
		/// Gets the coordinates of the center of this rectangle.
		/// </summary>
		public Point2d Center { get { return new Point2d(X + (Width / 2), Y + (Height / 2)); } }
		/// <summary>
		/// Gets the size of this rectangle.
		/// </summary>
		public Size2d Size { get { return new Size2d(Width, Height); } }
		/// <summary>
		/// Gets the coordinates of the lower-left corner of this rectangle.
		/// </summary>
		public Point2d Location { get { return new Point2d(X, Y); } }
		/// <summary>
		/// Gets the coordinates of the minimum corner of this rectangle.
		/// </summary>
		public Point2d Minimum { get { return new Point2d(Left, Bottom); } }
		/// <summary>
		/// Gets the coordinates of the maximum corner of this rectangle.
		/// </summary>
		public Point2d Maximum { get { return new Point2d(Right, Top); } }
		/// <summary>
		/// Gets the corners of this rectangle.
		/// </summary>
		public Point2d[] Corners
		{
			get
			{
				return new Point2d[] { new Point2d(X, Y), new Point2d(X + Width, Y), new Point2d(X + Width, Y + Height), new Point2d(X, Y + Height) };
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Rectangled"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the rectangle.</param>
		/// <param name="y">Value for the Y component of the rectangle.</param>
		/// <param name="width">Value for the Width component of the rectangle.</param>
		/// <param name="height">Value for the Height component of the rectangle.</param>
		public Rectangled(double x, double y, double width, double height)
		{
			Contract.Requires(0 <= width);
			Contract.Requires(0 <= height);
			
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Rectangled"/> using the specified location and size.
		/// </summary>
		/// <param name="location">The lower-left corner of the rectangle.</param>
		/// <param name="size">The size of the rectangle.</param>
		public Rectangled(Point2d location, Size2d size)
		{
			X = location.X;
			Y = location.Y;
			Width = size.Width;
			Height = size.Height;
		}
		#endregion
		#region Operations
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a Rectanglef value to a Rectangled.
		/// </summary>
		/// <param name="value">The value to convert to a Rectangled.</param>
		/// <returns>A Rectangled that has all components equal to value.</returns>
		public static implicit operator Rectangled(Rectanglef value)
		{
			return new Rectangled((double)value.X, (double)value.Y, (double)value.Width, (double)value.Height);
		}
		/// <summary>
		/// Defines an implicit conversion of a Rectanglel value to a Rectangled.
		/// </summary>
		/// <param name="value">The value to convert to a Rectangled.</param>
		/// <returns>A Rectangled that has all components equal to value.</returns>
		public static implicit operator Rectangled(Rectanglel value)
		{
			return new Rectangled((double)value.X, (double)value.Y, (double)value.Width, (double)value.Height);
		}
		/// <summary>
		/// Defines an implicit conversion of a Rectanglei value to a Rectangled.
		/// </summary>
		/// <param name="value">The value to convert to a Rectangled.</param>
		/// <returns>A Rectangled that has all components equal to value.</returns>
		public static implicit operator Rectangled(Rectanglei value)
		{
			return new Rectangled((double)value.X, (double)value.Y, (double)value.Width, (double)value.Height);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Rectangled"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Rectangled"/> object or a type capable
		/// of implicit conversion to a <see cref="Rectangled"/> object, and its value
		/// is equal to the current <see cref="Rectangled"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Rectangled) return Equals((Rectangled)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// rectangle have the same value.
		/// </summary>
		/// <param name="other">The rectangle to compare.</param>
		/// <returns>true if this rectangle and value have the same value; otherwise, false.</returns>
		public bool Equals(Rectangled other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two rectangles are equal.
		/// </summary>
		/// <param name="left">The first rectangle to compare.</param>
		/// <param name="right">The second rectangle to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Rectangled left, Rectangled right)
		{
			return left.X == right.X & left.Y == right.Y & left.Width == right.Width & left.Height == right.Height;
		}
		/// <summary>
		/// Returns a value that indicates whether two rectangles are not equal.
		/// </summary>
		/// <param name="left">The first rectangle to compare.</param>
		/// <param name="right">The second rectangle to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Rectangled left, Rectangled right)
		{
			return left.X != right.X | left.Y != right.Y | left.Width != right.Width | left.Height != right.Height;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current rectangle to its equivalent string
		/// representation.
		/// </summary>
		/// <returns>The string representation of the current instance.</returns>
		public override string ToString()
		{
			Contract.Ensures(Contract.Result<string>() != null);
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current rectangle to its equivalent string
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
		/// Converts the value of the current rectangle to its equivalent string
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
		/// Converts the value of the current rectangle to its equivalent string
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
			return String.Format("({0}, {1})", Location.ToString(format, provider), Size.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for rectangle functions.
	/// </summary>
	public static partial class Rectangle
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Rectangled"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Rectangled rectangle)
		{
			writer.Write(rectangle.X);
			writer.Write(rectangle.Y);
			writer.Write(rectangle.Width);
			writer.Write(rectangle.Height);
		}
		/// <summary>
		/// Reads a <see cref="Rectangled"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Rectangled ReadRectangled(this Ibasa.IO.BinaryReader reader)
		{
			return new Rectangled(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two rectangles are equal.
		/// </summary>
		/// <param name="left">The first rectangle to compare.</param>
		/// <param name="right">The second rectangle to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Rectangled left, Rectangled right)
		{
			return left == right;
		}
		#endregion
		#region Contains
		public static bool Contains(Rectangled rectangle, Point2d point)
		{
			return (rectangle.Left <= point.X) && (rectangle.Right >= point.X) &&
			       (rectangle.Bottom <= point.Y) && (rectangle.Top >= point.Y);
		}
		#endregion
	}
}
