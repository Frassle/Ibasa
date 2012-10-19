using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents an ordered tuple of integer x, y, z, width, height and depth components that defines a
	/// location and size in a three-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Boxl: IEquatable<Boxl>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Boxl"/> that has an X, Y, Z, Width, Height and Depth value of 0.
		/// </summary>
		public static readonly Boxl Empty = new Boxl(0, 0, 0, 0, 0, 0);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the box.
		/// </summary>
		public readonly long X;
		/// <summary>
		/// The Y component of the box.
		/// </summary>
		public readonly long Y;
		/// <summary>
		/// The Z component of the box.
		/// </summary>
		public readonly long Z;
		/// <summary>
		/// The Width component of the box.
		/// </summary>
		public readonly long Width;
		/// <summary>
		/// The Height component of the box.
		/// </summary>
		public readonly long Height;
		/// <summary>
		/// The Depth component of the box.
		/// </summary>
		public readonly long Depth;
		#endregion
		#region Properties
		/// <summary>
		/// Gets the y-coordinate of the top face of this box.
		/// </summary>
		public long Top { get { return Y + Height; } }
		/// <summary>
		/// Gets the y-coordinate of the bottom face of this box.
		/// </summary>
		public long Bottom { get { return Y; } }
		/// <summary>
		/// Gets the x-coordinate of the left face of this box.
		/// </summary>
		public long Left { get { return X; } }
		/// <summary>
		/// Gets the x-coordinate of the right face of this box.
		/// </summary>
		public long Right { get { return X + Width; } }
		/// <summary>
		/// Gets the z-coordinate of the front face of this box.
		/// </summary>
		public long Front { get { return Z; } }
		/// <summary>
		/// Gets the z-coordinate of the back face of this box.
		/// </summary>
		public long Back { get { return Z + Depth; } }
		/// <summary>
		/// Gets the coordinates of the center of this box.
		/// </summary>
		public Point3l Center { get { return new Point3l(X + (Width / 2), Y + (Height / 2), Z + (Depth / 2)); } }
		/// <summary>
		/// Gets the size of this box.
		/// </summary>
		public Size3l Size { get { return new Size3l(Width, Height, Depth); } }
		/// <summary>
		/// Gets the coordinates of the front-lower-left corner of this box.
		/// </summary>
		public Point3l Location { get { return new Point3l(X, Y, Z); } }
		/// <summary>
		/// Gets the coordinates of the minimum corner of this box.
		/// </summary>
		public Point3l Minimum { get { return new Point3l(Left, Bottom, Front); } }
		/// <summary>
		/// Gets the coordinates of the maximum corner of this box.
		/// </summary>
		public Point3l Maximum { get { return new Point3l(Right, Top, Back); } }
		/// <summary>
		/// Gets the corners of this box.
		/// </summary>
		public Point3l[] Corners
		{
			get
			{
				return new Point3l[]
				{
					new Point3l(X, Y, Z), new Point3l(X + Width, Y, Z), new Point3l(X + Width, Y + Height, Z), new Point3l(X, Y + Height, Z),
					new Point3l(X, Y, Z + Depth), new Point3l(X + Width, Y, Z + Depth), new Point3l(X + Width, Y + Height, Z + Depth), new Point3l(X, Y + Height, Z + Depth)
				};
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Boxl"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the box.</param>
		/// <param name="y">Value for the Y component of the box.</param>
		/// <param name="z">Value for the Z component of the box.</param>
		/// <param name="width">Value for the Width component of the box.</param>
		/// <param name="height">Value for the Height component of the box.</param>
		/// <param name="depth">Value for the Depth component of the box.</param>
		public Boxl(long x, long y, long z, long width, long height, long depth)
		{
			Contract.Requires(0 <= width);
			Contract.Requires(0 <= height);
			Contract.Requires(0 <= depth);
			X = x;
			Y = y;
			Z = z;
			Width = width;
			Height = height;
			Depth = depth;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Boxl"/> using the specified location and size.
		/// </summary>
		/// <param name="location">The front-lower-left corner of the box.</param>
		/// <param name="size">The size of the box.</param>
		public Boxl(Point3l location, Size3l size)
		{
			X = location.X;
			Y = location.Y;
			Z = location.Z;
			Width = size.Width;
			Height = size.Height;
			Depth = size.Depth;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Boxl"/> using the specified location and size.
		/// </summary>
		/// <param name="x">Value for the X component of the box.</param>
		/// <param name="y">Value for the Y component of the box.</param>
		/// <param name="z">Value for the Z component of the box.</param>
		/// <param name="size">The size of the box.</param>
		public Boxl(long x, long y, long z, Size3l size)
		{
			X = x;
			Y = y;
			Z = z;
			Width = size.Width;
			Height = size.Height;
			Depth = size.Depth;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Boxl"/> using the specified location and size.
		/// </summary>
		/// <param name="location">The front-lower-left corner of the box.</param>
		/// <param name="width">Value for the Width component of the box.</param>
		/// <param name="height">Value for the Height component of the box.</param>
		/// <param name="depth">Value for the Depth component of the box.</param>
		public Boxl(Point3l location, long width, long height, long depth)
		{
			X = location.X;
			Y = location.Y;
			Z = location.Z;
			Width = width;
			Height = height;
			Depth = depth;
		}
		#endregion
		#region Operations
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Boxd value to a Boxl.
		/// </summary>
		/// <param name="value">The value to convert to a Boxl.</param>
		/// <returns>A Boxl that has all components equal to value.</returns>
		public static explicit operator Boxl(Boxd value)
		{
			return new Boxl((long)value.X, (long)value.Y, (long)value.Z, (long)value.Width, (long)value.Height, (long)value.Depth);
		}
		/// <summary>
		/// Defines an explicit conversion of a Boxf value to a Boxl.
		/// </summary>
		/// <param name="value">The value to convert to a Boxl.</param>
		/// <returns>A Boxl that has all components equal to value.</returns>
		public static explicit operator Boxl(Boxf value)
		{
			return new Boxl((long)value.X, (long)value.Y, (long)value.Z, (long)value.Width, (long)value.Height, (long)value.Depth);
		}
		/// <summary>
		/// Defines an implicit conversion of a Boxi value to a Boxl.
		/// </summary>
		/// <param name="value">The value to convert to a Boxl.</param>
		/// <returns>A Boxl that has all components equal to value.</returns>
		public static implicit operator Boxl(Boxi value)
		{
			return new Boxl((long)value.X, (long)value.Y, (long)value.Z, (long)value.Width, (long)value.Height, (long)value.Depth);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Boxl"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + Width.GetHashCode() + Height.GetHashCode() + Depth.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Boxl"/> object or a type capable
		/// of implicit conversion to a <see cref="Boxl"/> object, and its value
		/// is equal to the current <see cref="Boxl"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Boxl) return Equals((Boxl)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// box have the same value.
		/// </summary>
		/// <param name="other">The box to compare.</param>
		/// <returns>true if this box and value have the same value; otherwise, false.</returns>
		public bool Equals(Boxl other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two boxs are equal.
		/// </summary>
		/// <param name="left">The first box to compare.</param>
		/// <param name="right">The second box to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Boxl left, Boxl right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z & left.Width == right.Width & left.Height == right.Height & left.Depth == right.Depth;
		}
		/// <summary>
		/// Returns a value that indicates whether two boxs are not equal.
		/// </summary>
		/// <param name="left">The first box to compare.</param>
		/// <param name="right">The second box to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Boxl left, Boxl right)
		{
			return left.X != right.X | left.Y != right.Y | left.Z != right.Z | left.Width != right.Width | left.Height != right.Height | left.Depth != right.Depth;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current box to its equivalent string
		/// representation.
		/// </summary>
		/// <returns>The string representation of the current instance.</returns>
		public override string ToString()
		{
			Contract.Ensures(Contract.Result<string>() != null);
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current box to its equivalent string
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
		/// Converts the value of the current box to its equivalent string
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
		/// Converts the value of the current box to its equivalent string
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
	/// Provides static methods for box functions.
	/// </summary>
	public static partial class Box
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Boxl"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Boxl box)
		{
			writer.Write(box.X);
			writer.Write(box.Y);
			writer.Write(box.Z);
			writer.Write(box.Width);
			writer.Write(box.Height);
			writer.Write(box.Depth);
		}
		/// <summary>
		/// Reads a <see cref="Boxl"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Boxl ReadBoxl(this Ibasa.IO.BinaryReader reader)
		{
			return new Boxl(reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64());
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two boxes are equal.
		/// </summary>
		/// <param name="left">The first box to compare.</param>
		/// <param name="right">The second box to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Boxl left, Boxl right)
		{
			return left == right;
		}
		#endregion
		#region Contains
		public static bool Contains(Boxl box, Point3l point)
		{
			return (box.Left <= point.X) && (box.Right >= point.X) &&
			       (box.Bottom <= point.Y) && (box.Top >= point.Y) &&
			       (box.Front <= point.Z) && (box.Back >= point.Z);
		}
		#endregion
	}
}
