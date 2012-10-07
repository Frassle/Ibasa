using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a line in a three-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Line3d: IEquatable<Line3d>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new empty <see cref="Line3d"/>.
		/// </summary>
		public static readonly Line3d Empty = new Line3d(Point3d.Zero, Point3d.Zero);
		#endregion
		#region Fields
		/// <summary>
		/// The coordinates of the start of this line.
		/// </summary>
		public readonly Point3d Start;
		/// <summary>
		/// The coordinates of the end of this line.
		/// </summary>
		public readonly Point3d End;
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
		public Point3d Center
		{
			get
			{
				return new Point3d((Start.X + End.X) / 2, (Start.Y + End.Y) / 2, (Start.Z + End.Z) / 2);
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Line3d"/> using the specified values.
		/// </summary>
		/// <param name="start">Start point of the line.</param>
		/// <param name="end">End point of the line.</param>
		public Line3d(Point3d start, Point3d end)
		{
			Start = start;
			End = end;
		}
		#endregion
		#region Operations
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a Line3f value to a Line3d.
		/// </summary>
		/// <param name="value">The value to convert to a Line3d.</param>
		/// <returns>A Line3d that has all components equal to value.</returns>
		public static implicit operator Line3d(Line3f value)
		{
			return new Line3d((Point3d)value.Start, (Point3d)value.End);
		}
		/// <summary>
		/// Defines an implicit conversion of a Line3l value to a Line3d.
		/// </summary>
		/// <param name="value">The value to convert to a Line3d.</param>
		/// <returns>A Line3d that has all components equal to value.</returns>
		public static implicit operator Line3d(Line3l value)
		{
			return new Line3d((Point3d)value.Start, (Point3d)value.End);
		}
		/// <summary>
		/// Defines an implicit conversion of a Line3i value to a Line3d.
		/// </summary>
		/// <param name="value">The value to convert to a Line3d.</param>
		/// <returns>A Line3d that has all components equal to value.</returns>
		public static implicit operator Line3d(Line3i value)
		{
			return new Line3d((Point3d)value.Start, (Point3d)value.End);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Line3d"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Line3d"/> object or a type capable
		/// of implicit conversion to a <see cref="Line3d"/> object, and its value
		/// is equal to the current <see cref="Line3d"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Line3d) return Equals((Line3d)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// line have the same value.
		/// </summary>
		/// <param name="other">The line to compare.</param>
		/// <returns>true if this line and value have the same value; otherwise, false.</returns>
		public bool Equals(Line3d other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two lines are equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Line3d left, Line3d right)
		{
			return left.Start == right.Start && left.End == right.End;
		}
		/// <summary>
		/// Returns a value that indicates whether two lines are not equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Line3d left, Line3d right)
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
		/// Writes the given <see cref="Line3d"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Line3d line)
		{
			writer.Write(line.Start);
			writer.Write(line.End);
		}
		/// <summary>
		/// Reads a <see cref="Line3d"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Line3d ReadLine3d(this Ibasa.IO.BinaryReader reader)
		{
			return new Line3d(reader.ReadPoint3d(), reader.ReadPoint3d());
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two lines are equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Line3d left, Line3d right)
		{
			return left == right;
		}
		#endregion
		#region Intersect
		#endregion
	}
}
