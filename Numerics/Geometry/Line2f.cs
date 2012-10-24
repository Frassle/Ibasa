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
	public struct Line2f: IEquatable<Line2f>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new empty <see cref="Line2f"/>.
		/// </summary>
		public static readonly Line2f Empty = new Line2f(Point2f.Zero, Point2f.Zero);
		#endregion
		#region Fields
		/// <summary>
		/// The coordinates of the start of this line.
		/// </summary>
		public readonly Point2f Start;
		/// <summary>
		/// The coordinates of the end of this line.
		/// </summary>
		public readonly Point2f End;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the length of this line.
		/// </summary>
		/// <returns>The length of this line.</returns>
		public float Length
		{
			get
			{
				return Point.Distance(Start, End);
			}
		}
		/// <summary>
		/// Gets the coordinates of the center of this line.
		/// </summary>
		public Point2f Center
		{
			get
			{
				return new Point2f((Start.X + End.X) / 2, (Start.Y + End.Y) / 2);
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Line2f"/> using the specified values.
		/// </summary>
		/// <param name="start">Start point of the line.</param>
		/// <param name="end">End point of the line.</param>
		public Line2f(Point2f start, Point2f end)
		{
			Start = start;
			End = end;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Line2f"/> using the specified values.
		/// </summary>
		/// <param name="startX">X coordinate of the start point of the line.</param>
		/// <param name="startY">Y coordinate of the start point of the line.</param>
		/// <param name="endX">X coordinate of the end point of the line.</param>
		/// <param name="endY">Y coordinate of the end point of the line.</param>
		public Line2f(float startX, float startY, float endX, float endY)
		{
			Start = new Point2f(startX, startY);
			End = new Point2f(endX, endY);
		}
		#endregion
		#region Operations
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Line2d value to a Line2f.
		/// </summary>
		/// <param name="value">The value to convert to a Line2f.</param>
		/// <returns>A Line2f that has all components equal to value.</returns>
		public static explicit operator Line2f(Line2d value)
		{
			return new Line2f((Point2f)value.Start, (Point2f)value.End);
		}
		/// <summary>
		/// Defines an implicit conversion of a Line2l value to a Line2f.
		/// </summary>
		/// <param name="value">The value to convert to a Line2f.</param>
		/// <returns>A Line2f that has all components equal to value.</returns>
		public static implicit operator Line2f(Line2l value)
		{
			return new Line2f((Point2f)value.Start, (Point2f)value.End);
		}
		/// <summary>
		/// Defines an implicit conversion of a Line2i value to a Line2f.
		/// </summary>
		/// <param name="value">The value to convert to a Line2f.</param>
		/// <returns>A Line2f that has all components equal to value.</returns>
		public static implicit operator Line2f(Line2i value)
		{
			return new Line2f((Point2f)value.Start, (Point2f)value.End);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Line2f"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Line2f"/> object or a type capable
		/// of implicit conversion to a <see cref="Line2f"/> object, and its value
		/// is equal to the current <see cref="Line2f"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Line2f) return Equals((Line2f)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// line have the same value.
		/// </summary>
		/// <param name="other">The line to compare.</param>
		/// <returns>true if this line and value have the same value; otherwise, false.</returns>
		public bool Equals(Line2f other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two lines are equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Line2f left, Line2f right)
		{
			return left.Start == right.Start && left.End == right.End;
		}
		/// <summary>
		/// Returns a value that indicates whether two lines are not equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Line2f left, Line2f right)
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
		/// Writes the given <see cref="Line2f"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Line2f line)
		{
			writer.Write(line.Start);
			writer.Write(line.End);
		}
		/// <summary>
		/// Reads a <see cref="Line2f"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Line2f ReadLine2f(this Ibasa.IO.BinaryReader reader)
		{
			return new Line2f(reader.ReadPoint2f(), reader.ReadPoint2f());
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two lines are equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Line2f left, Line2f right)
		{
			return left == right;
		}
		#endregion
		#region Intersect
		#endregion
	}
}
