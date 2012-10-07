using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a line in a two-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Line2l: IEquatable<Line2l>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new empty <see cref="Line2l"/>.
		/// </summary>
		public static readonly Line2l Empty = new Line2l(Point2l.Zero, Point2l.Zero);
		#endregion
		#region Fields
		/// <summary>
		/// The coordinates of the start of this line.
		/// </summary>
		public readonly Point2l Start;
		/// <summary>
		/// The coordinates of the end of this line.
		/// </summary>
		public readonly Point2l End;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the length of this line.
		/// </summary>
		/// <returns>The length of this line.</returns>
		public double Length
		{
			get
			{
				return Point.Distance(Start, End);
			}
		}
		/// <summary>
		/// Gets the coordinates of the center of this line.
		/// </summary>
		public Point2l Center
		{
			get
			{
				return new Point2l((Start.X + End.X) / 2, (Start.Y + End.Y) / 2);
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Line2l"/> using the specified values.
		/// </summary>
		/// <param name="start">Start point of the line.</param>
		/// <param name="end">End point of the line.</param>
		public Line2l(Point2l start, Point2l end)
		{
			Start = start;
			End = end;
		}
		#endregion
		#region Operations
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Line2d value to a Line2l.
		/// </summary>
		/// <param name="value">The value to convert to a Line2l.</param>
		/// <returns>A Line2l that has all components equal to value.</returns>
		public static explicit operator Line2l(Line2d value)
		{
			return new Line2l((Point2l)value.Start, (Point2l)value.End);
		}
		/// <summary>
		/// Defines an explicit conversion of a Line2f value to a Line2l.
		/// </summary>
		/// <param name="value">The value to convert to a Line2l.</param>
		/// <returns>A Line2l that has all components equal to value.</returns>
		public static explicit operator Line2l(Line2f value)
		{
			return new Line2l((Point2l)value.Start, (Point2l)value.End);
		}
		/// <summary>
		/// Defines an implicit conversion of a Line2i value to a Line2l.
		/// </summary>
		/// <param name="value">The value to convert to a Line2l.</param>
		/// <returns>A Line2l that has all components equal to value.</returns>
		public static implicit operator Line2l(Line2i value)
		{
			return new Line2l((Point2l)value.Start, (Point2l)value.End);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Line2l"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Start.GetHashCode() + End.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Line2l"/> object or a type capable
		/// of implicit conversion to a <see cref="Line2l"/> object, and its value
		/// is equal to the current <see cref="Line2l"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Line2l) return Equals((Line2l)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// line have the same value.
		/// </summary>
		/// <param name="other">The line to compare.</param>
		/// <returns>true if this line and value have the same value; otherwise, false.</returns>
		public bool Equals(Line2l other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two lines are equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Line2l left, Line2l right)
		{
			return left.Start == right.Start && left.End == right.End;
		}
		/// <summary>
		/// Returns a value that indicates whether two lines are not equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Line2l left, Line2l right)
		{
			return left.Start != right.Start || left.End != right.End;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current line to its equivalent string
		/// representation.
		/// </summary>
		/// <returns>The string representation of the current instance.</returns>
		public override string ToString()
		{
			Contract.Ensures(Contract.Result<string>() != null);
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current line to its equivalent string
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
		/// Converts the value of the current line to its equivalent string
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
		/// Converts the value of the current line to its equivalent string
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
			return String.Format("({0}, {1})", Start.ToString(format, provider), End.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for line functions.
	/// </summary>
	public static partial class Line
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Line2l"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Line2l line)
		{
			writer.Write(line.Start);
			writer.Write(line.End);
		}
		/// <summary>
		/// Reads a <see cref="Line2l"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Line2l ReadLine2l(this Ibasa.IO.BinaryReader reader)
		{
			return new Line2l(reader.ReadPoint2l(), reader.ReadPoint2l());
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two lines are equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Line2l left, Line2l right)
		{
			return left == right;
		}
		#endregion
		#region Intersect
		#endregion
	}
}
