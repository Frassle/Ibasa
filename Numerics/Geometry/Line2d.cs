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
	public struct Line2d: IEquatable<Line2d>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new empty <see cref="Line2d"/>.
		/// </summary>
		public static readonly Line2d Empty = new Line2d(Point2d.Zero, Point2d.Zero);
		#endregion
		#region Fields
		/// <summary>
		/// The coordinates of the start of this line.
		/// </summary>
		public readonly Point2d Start;
		/// <summary>
		/// The coordinates of the end of this line.
		/// </summary>
		public readonly Point2d End;
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
		public Point2d Center
		{
			get
			{
				return new Point2d((Start.X + End.X) / 2, (Start.Y + End.Y) / 2);
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Line2d"/> using the specified values.
		/// </summary>
		/// <param name="start">Start point of the line.</param>
		/// <param name="end">End point of the line.</param>
		public Line2d(Point2d start, Point2d end)
		{
			Start = start;
			End = end;
		}
		#endregion
		#region Operations
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a Line2f value to a Line2d.
		/// </summary>
		/// <param name="value">The value to convert to a Line2d.</param>
		/// <returns>A Line2d that has all components equal to value.</returns>
		public static implicit operator Line2d(Line2f value)
		{
			return new Line2d((Point2d)value.Start, (Point2d)value.End);
		}
		/// <summary>
		/// Defines an implicit conversion of a Line2l value to a Line2d.
		/// </summary>
		/// <param name="value">The value to convert to a Line2d.</param>
		/// <returns>A Line2d that has all components equal to value.</returns>
		public static implicit operator Line2d(Line2l value)
		{
			return new Line2d((Point2d)value.Start, (Point2d)value.End);
		}
		/// <summary>
		/// Defines an implicit conversion of a Line2i value to a Line2d.
		/// </summary>
		/// <param name="value">The value to convert to a Line2d.</param>
		/// <returns>A Line2d that has all components equal to value.</returns>
		public static implicit operator Line2d(Line2i value)
		{
			return new Line2d((Point2d)value.Start, (Point2d)value.End);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Line2d"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Line2d"/> object or a type capable
		/// of implicit conversion to a <see cref="Line2d"/> object, and its value
		/// is equal to the current <see cref="Line2d"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Line2d) return Equals((Line2d)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// line have the same value.
		/// </summary>
		/// <param name="other">The line to compare.</param>
		/// <returns>true if this line and value have the same value; otherwise, false.</returns>
		public bool Equals(Line2d other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two lines are equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Line2d left, Line2d right)
		{
			return left.Start == right.Start && left.End == right.End;
		}
		/// <summary>
		/// Returns a value that indicates whether two lines are not equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Line2d left, Line2d right)
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
		/// Writes the given <see cref="Line2d"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Line2d line)
		{
			writer.Write(line.Start);
			writer.Write(line.End);
		}
		/// <summary>
		/// Reads a <see cref="Line2d"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Line2d ReadLine2d(this Ibasa.IO.BinaryReader reader)
		{
			return new Line2d(reader.ReadPoint2d(), reader.ReadPoint2d());
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two lines are equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Line2d left, Line2d right)
		{
			return left == right;
		}
		#endregion
		#region Intersect
		#endregion
	}
}
