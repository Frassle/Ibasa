using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents an ordered triple of integer width, height and depth components that defines a
	/// size in a three-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Size3l: IEquatable<Size3l>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Size3l"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Size3l Empty = new Size3l(0, 0, 0);
		/// <summary>
		/// Returns a new <see cref="Size3l"/> with all of its components equal to one.
		/// </summary>
		public static readonly Size3l Unit = new Size3l(1, 1, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The Width component of the size.
		/// </summary>
		public readonly long Width;
		/// <summary>
		/// The Height component of the size.
		/// </summary>
		public readonly long Height;
		/// <summary>
		/// The Depth component of the size.
		/// </summary>
		public readonly long Depth;
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Size3l"/> using the specified size and value.
		/// </summary>
		/// <param name="value">A size containing the values with which to initialize the Width and Height components</param>
		/// <param name="depth">Value for the Depth component of the size.</param>
		public Size3l(Size2l value, long depth)
		{
			Contract.Requires(0 <= depth);
			Width = value.Width;
			Height = value.Height;
			Depth = depth;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Size3l"/> using the specified values.
		/// </summary>
		/// <param name="width">Value for the Width component of the size.</param>
		/// <param name="height">Value for the Height component of the size.</param>
		/// <param name="depth">Value for the Depth component of the size.</param>
		public Size3l(long width, long height, long depth)
		{
			Contract.Requires(0 <= width);
			Contract.Requires(0 <= height);
			Contract.Requires(0 <= depth);
			Width = width;
			Height = height;
			Depth = depth;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the product of a size and scalar.
		/// </summary>
		/// <param name="left">The size to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Size3l operator *(Size3l left, long right)
		{
			Contract.Requires(0 <= right);
			return Size.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and size.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The size to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Size3l operator *(long left, Size3l right)
		{
			Contract.Requires(0 <= left);
			return Size.Multiply(right, left);
		}
		/// <summary>
		/// Divides a size by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The size to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Size3l operator /(Size3l left, long right)
		{
			Contract.Requires(0 <= right);
			return Size.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Size3d value to a Size3l.
		/// </summary>
		/// <param name="value">The value to convert to a Size3l.</param>
		/// <returns>A Size3l that has all components equal to value.</returns>
		public static explicit operator Size3l(Size3d value)
		{
			return new Size3l((long)value.Width, (long)value.Height, (long)value.Depth);
		}
		/// <summary>
		/// Defines an explicit conversion of a Size3f value to a Size3l.
		/// </summary>
		/// <param name="value">The value to convert to a Size3l.</param>
		/// <returns>A Size3l that has all components equal to value.</returns>
		public static explicit operator Size3l(Size3f value)
		{
			return new Size3l((long)value.Width, (long)value.Height, (long)value.Depth);
		}
		/// <summary>
		/// Defines an implicit conversion of a Size3i value to a Size3l.
		/// </summary>
		/// <param name="value">The value to convert to a Size3l.</param>
		/// <returns>A Size3l that has all components equal to value.</returns>
		public static implicit operator Size3l(Size3i value)
		{
			return new Size3l((long)value.Width, (long)value.Height, (long)value.Depth);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Size3l"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Width.GetHashCode() + Height.GetHashCode() + Depth.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Size3l"/> object or a type capable
		/// of implicit conversion to a <see cref="Size3l"/> object, and its value
		/// is equal to the current <see cref="Size3l"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Size3l) { return Equals((Size3l)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// size have the same value.
		/// </summary>
		/// <param name="other">The size to compare.</param>
		/// <returns>true if this size and value have the same value; otherwise, false.</returns>
		public bool Equals(Size3l other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two sizes are equal.
		/// </summary>
		/// <param name="left">The first size to compare.</param>
		/// <param name="right">The second size to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Size3l left, Size3l right)
		{
			return left.Width == right.Width & left.Height == right.Height & left.Depth == right.Depth;
		}
		/// <summary>
		/// Returns a value that indicates whether two sizes are not equal.
		/// </summary>
		/// <param name="left">The first size to compare.</param>
		/// <param name="right">The second size to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Size3l left, Size3l right)
		{
			return left.Width != right.Width | left.Height != right.Height | left.Depth != right.Depth;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current size to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current size to its equivalent string
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
		/// Converts the value of the current size to its equivalent string
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
		/// Converts the value of the current size to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, IFormatProvider provider)
		{
			return String.Format("(Width: {0}, Height: {1}, Depth: {2})", Width.ToString(format, provider), Height.ToString(format, provider), Depth.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for size functions.
	/// </summary>
	public static partial class Size
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Size3l"/> to a System.IO.BinaryWriter.
		/// </summary>
		public static void Write(this System.IO.BinaryWriter writer, Size3l size)
		{
			writer.Write(size.Width);
			writer.Write(size.Height);
			writer.Write(size.Depth);
		}
		/// <summary>
		/// Reads a <see cref="Size3l"/> to a System.IO.BinaryReader.
		/// </summary>
		public static Size3l ReadSize3l(this System.IO.BinaryReader reader)
		{
			return new Size3l(reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the product of a size and scalar.
		/// </summary>
		/// <param name="size">The size to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Size3l Multiply(Size3l size, long scalar)
		{
			Contract.Requires(0 <= scalar);
			return new Size3l(size.Width * scalar, size.Height * scalar, size.Depth * scalar);
		}
		/// <summary>
		/// Divides a size by a scalar and returns the result.
		/// </summary>
		/// <param name="size">The size to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Size3l Divide(Size3l size, long scalar)
		{
			Contract.Requires(0 <= scalar);
			return new Size3l(size.Width / scalar, size.Height / scalar, size.Depth / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two sizes are equal.
		/// </summary>
		/// <param name="left">The first size to compare.</param>
		/// <param name="right">The second size to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Size3l left, Size3l right)
		{
			return left == right;
		}
		#endregion
		#region Per component
		#region Transform
		/// <summary>
		/// Transforms the components of a size and returns the result.
		/// </summary>
		/// <param name="value">The size to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Size3d Transform(Size3l value, Func<long, double> transformer)
		{
			return new Size3d(transformer(value.Width), transformer(value.Height), transformer(value.Depth));
		}
		/// <summary>
		/// Transforms the components of a size and returns the result.
		/// </summary>
		/// <param name="value">The size to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Size3f Transform(Size3l value, Func<long, float> transformer)
		{
			return new Size3f(transformer(value.Width), transformer(value.Height), transformer(value.Depth));
		}
		/// <summary>
		/// Transforms the components of a size and returns the result.
		/// </summary>
		/// <param name="value">The size to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Size3l Transform(Size3l value, Func<long, long> transformer)
		{
			return new Size3l(transformer(value.Width), transformer(value.Height), transformer(value.Depth));
		}
		/// <summary>
		/// Transforms the components of a size and returns the result.
		/// </summary>
		/// <param name="value">The size to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Size3i Transform(Size3l value, Func<long, int> transformer)
		{
			return new Size3i(transformer(value.Width), transformer(value.Height), transformer(value.Depth));
		}
		#endregion
		/// <summary>
		/// Returns a size that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first size.</param>
		/// <param name="value2">The second size.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Size3l Min(Size3l value1, Size3l value2)
		{
			return new Size3l(Functions.Min(value1.Width, value2.Width), Functions.Min(value1.Height, value2.Height), Functions.Min(value1.Depth, value2.Depth));
		}
		/// <summary>
		/// Returns a size that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first size.</param>
		/// <param name="value2">The second size.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Size3l Max(Size3l value1, Size3l value2)
		{
			return new Size3l(Functions.Max(value1.Width, value2.Width), Functions.Max(value1.Height, value2.Height), Functions.Max(value1.Depth, value2.Depth));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A size to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A size with each component constrained to the given range.</returns>
		public static Size3l Clamp(Size3l value, Size3l min, Size3l max)
		{
			return new Size3l(Functions.Clamp(value.Width, min.Width, max.Width), Functions.Clamp(value.Height, min.Height, max.Height), Functions.Clamp(value.Depth, min.Depth, max.Depth));
		}
		#endregion
	}
}
