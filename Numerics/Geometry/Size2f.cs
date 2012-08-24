using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents an ordered pair of real width and height components that defines a
	/// size in a two-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Size2f: IEquatable<Size2f>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Size2f"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Size2f Empty = new Size2f(0, 0);
		/// <summary>
		/// Returns a new <see cref="Size2f"/> with all of its components equal to one.
		/// </summary>
		public static readonly Size2f Unit = new Size2f(1, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The Width component of the size.
		/// </summary>
		public readonly float Width;
		/// <summary>
		/// The Height component of the size.
		/// </summary>
		public readonly float Height;
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Size2f"/> using the specified values.
		/// </summary>
		/// <param name="width">Value for the Width component of the size.</param>
		/// <param name="height">Value for the Height component of the size.</param>
		public Size2f(float width, float height)
		{
			Contract.Requires(0 <= width);
			Contract.Requires(0 <= height);
			Width = width;
			Height = height;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the product of a size and scalar.
		/// </summary>
		/// <param name="left">The size to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Size2f operator *(Size2f left, float right)
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
		public static Size2f operator *(float left, Size2f right)
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
		public static Size2f operator /(Size2f left, float right)
		{
			Contract.Requires(0 <= right);
			return Size.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Size2d value to a Size2f.
		/// </summary>
		/// <param name="value">The value to convert to a Size2f.</param>
		/// <returns>A Size2f that has all components equal to value.</returns>
		public static explicit operator Size2f(Size2d value)
		{
			return new Size2f((float)value.Width, (float)value.Height);
		}
		/// <summary>
		/// Defines an implicit conversion of a Size2l value to a Size2f.
		/// </summary>
		/// <param name="value">The value to convert to a Size2f.</param>
		/// <returns>A Size2f that has all components equal to value.</returns>
		public static implicit operator Size2f(Size2l value)
		{
			return new Size2f((float)value.Width, (float)value.Height);
		}
		/// <summary>
		/// Defines an implicit conversion of a Size2i value to a Size2f.
		/// </summary>
		/// <param name="value">The value to convert to a Size2f.</param>
		/// <returns>A Size2f that has all components equal to value.</returns>
		public static implicit operator Size2f(Size2i value)
		{
			return new Size2f((float)value.Width, (float)value.Height);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Size2f"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Width.GetHashCode() + Height.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Size2f"/> object or a type capable
		/// of implicit conversion to a <see cref="Size2f"/> object, and its value
		/// is equal to the current <see cref="Size2f"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Size2f) { return Equals((Size2f)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// size have the same value.
		/// </summary>
		/// <param name="other">The size to compare.</param>
		/// <returns>true if this size and value have the same value; otherwise, false.</returns>
		public bool Equals(Size2f other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two sizes are equal.
		/// </summary>
		/// <param name="left">The first size to compare.</param>
		/// <param name="right">The second size to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Size2f left, Size2f right)
		{
			return left.Width == right.Width & left.Height == right.Height;
		}
		/// <summary>
		/// Returns a value that indicates whether two sizes are not equal.
		/// </summary>
		/// <param name="left">The first size to compare.</param>
		/// <param name="right">The second size to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Size2f left, Size2f right)
		{
			return left.Width != right.Width | left.Height != right.Height;
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
			return String.Format("(Width: {0}, Height: {1})", Width.ToString(format, provider), Height.ToString(format, provider));
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
		/// Writes the given <see cref="Size2f"/> to a System.IO.BinaryWriter.
		/// </summary>
		public static void Write(this System.IO.BinaryWriter writer, Size2f size)
		{
			writer.Write(size.Width);
			writer.Write(size.Height);
		}
		/// <summary>
		/// Reads a <see cref="Size2f"/> to a System.IO.BinaryReader.
		/// </summary>
		public static Size2f ReadSize2f(this System.IO.BinaryReader reader)
		{
			return new Size2f(reader.ReadSingle(), reader.ReadSingle());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the product of a size and scalar.
		/// </summary>
		/// <param name="size">The size to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Size2f Multiply(Size2f size, float scalar)
		{
			Contract.Requires(0 <= scalar);
			return new Size2f(size.Width * scalar, size.Height * scalar);
		}
		/// <summary>
		/// Divides a size by a scalar and returns the result.
		/// </summary>
		/// <param name="size">The size to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Size2f Divide(Size2f size, float scalar)
		{
			Contract.Requires(0 <= scalar);
			return new Size2f(size.Width / scalar, size.Height / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two sizes are equal.
		/// </summary>
		/// <param name="left">The first size to compare.</param>
		/// <param name="right">The second size to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Size2f left, Size2f right)
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
		public static Size2d Transform(Size2f value, Func<float, double> transformer)
		{
			return new Size2d(transformer(value.Width), transformer(value.Height));
		}
		/// <summary>
		/// Transforms the components of a size and returns the result.
		/// </summary>
		/// <param name="value">The size to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Size2f Transform(Size2f value, Func<float, float> transformer)
		{
			return new Size2f(transformer(value.Width), transformer(value.Height));
		}
		/// <summary>
		/// Transforms the components of a size and returns the result.
		/// </summary>
		/// <param name="value">The size to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Size2l Transform(Size2f value, Func<float, long> transformer)
		{
			return new Size2l(transformer(value.Width), transformer(value.Height));
		}
		/// <summary>
		/// Transforms the components of a size and returns the result.
		/// </summary>
		/// <param name="value">The size to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Size2i Transform(Size2f value, Func<float, int> transformer)
		{
			return new Size2i(transformer(value.Width), transformer(value.Height));
		}
		#endregion
		/// <summary>
		/// Returns a size that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first size.</param>
		/// <param name="value2">The second size.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Size2f Min(Size2f value1, Size2f value2)
		{
			return new Size2f(Functions.Min(value1.Width, value2.Width), Functions.Min(value1.Height, value2.Height));
		}
		/// <summary>
		/// Returns a size that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first size.</param>
		/// <param name="value2">The second size.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Size2f Max(Size2f value1, Size2f value2)
		{
			return new Size2f(Functions.Max(value1.Width, value2.Width), Functions.Max(value1.Height, value2.Height));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A size to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A size with each component constrained to the given range.</returns>
		public static Size2f Clamp(Size2f value, Size2f min, Size2f max)
		{
			return new Size2f(Functions.Clamp(value.Width, min.Width, max.Width), Functions.Clamp(value.Height, min.Height, max.Height));
		}
		/// <summary>
		/// Constrains each component to the range 0 to 1.
		/// </summary>
		/// <param name="value">A size to saturate.</param>
		/// <returns>A size with each component constrained to the range 0 to 1.</returns>
		public static Size2f Saturate(Size2f value)
		{
			return new Size2f(Functions.Saturate(value.Width), Functions.Saturate(value.Height));
		}
		/// <summary>
		/// Returns a size where each component is the smallest integral value that
		/// is greater than or equal to the specified component.
		/// </summary>
		/// <param name="value">A size.</param>
		/// <returns>The ceiling of value.</returns>
		public static Size2f Ceiling(Size2f value)
		{
			return new Size2f(Functions.Ceiling(value.Width), Functions.Ceiling(value.Height));
		}
		/// <summary>
		/// Returns a size where each component is the largest integral value that
		/// is less than or equal to the specified component.
		/// </summary>
		/// <param name="value">A size.</param>
		/// <returns>The floor of value.</returns>
		public static Size2f Floor(Size2f value)
		{
			return new Size2f(Functions.Floor(value.Width), Functions.Floor(value.Height));
		}
		/// <summary>
		/// Returns a size where each component is the integral part of the specified component.
		/// </summary>
		/// <param name="value">A size.</param>
		/// <returns>The integral of value.</returns>
		public static Size2f Truncate(Size2f value)
		{
			return new Size2f(Functions.Truncate(value.Width), Functions.Truncate(value.Height));
		}
		/// <summary>
		/// Returns a size where each component is the fractional part of the specified component.
		/// </summary>
		/// <param name="value">A size.</param>
		/// <returns>The fractional of value.</returns>
		public static Size2f Fractional(Size2f value)
		{
			return new Size2f(Functions.Fractional(value.Width), Functions.Fractional(value.Height));
		}
		/// <summary>
		/// Returns a size where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A size.</param>
		/// <returns>The result of rounding value.</returns>
		public static Size2f Round(Size2f value)
		{
			return new Size2f(Functions.Round(value.Width), Functions.Round(value.Height));
		}
		/// <summary>
		/// Returns a size where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A size.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Size2f Round(Size2f value, int digits)
		{
			return new Size2f(Functions.Round(value.Width, digits), Functions.Round(value.Height, digits));
		}
		/// <summary>
		/// Returns a size where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A size.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Size2f Round(Size2f value, MidpointRounding mode)
		{
			return new Size2f(Functions.Round(value.Width, mode), Functions.Round(value.Height, mode));
		}
		/// <summary>
		/// Returns a size where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A size.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Size2f Round(Size2f value, int digits, MidpointRounding mode)
		{
			return new Size2f(Functions.Round(value.Width, digits, mode), Functions.Round(value.Height, digits, mode));
		}
		#endregion
	}
}
