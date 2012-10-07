using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a line in a one-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Linef: IEquatable<Linef>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new empty <see cref="Linef"/>.
		/// </summary>
		public static readonly Linef Empty = new Linef(0, 0);
		#endregion
		#region Fields
		/// <summary>
		/// The coordinates of the start of this line.
		/// </summary>
		public readonly float Start;
		/// <summary>
		/// The coordinates of the end of this line.
		/// </summary>
		public readonly float End;
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
				return Functions.Abs(End - Start);
			}
		}
		/// <summary>
		/// Gets the coordinates of the center of this line.
		/// </summary>
		public float Center
		{
			get
			{
				return (Start + End) / 2;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Linef"/> using the specified values.
		/// </summary>
		/// <param name="start">Start point of the line.</param>
		/// <param name="end">End point of the line.</param>
		public Linef(float start, float end)
		{
			Start = start;
			End = end;
		}
		#endregion
		#region Operations
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Lined value to a Linef.
		/// </summary>
		/// <param name="value">The value to convert to a Linef.</param>
		/// <returns>A Linef that has all components equal to value.</returns>
		public static explicit operator Linef(Lined value)
		{
			return new Linef((float)value.Start, (float)value.End);
		}
		/// <summary>
		/// Defines an implicit conversion of a Linel value to a Linef.
		/// </summary>
		/// <param name="value">The value to convert to a Linef.</param>
		/// <returns>A Linef that has all components equal to value.</returns>
		public static implicit operator Linef(Linel value)
		{
			return new Linef((float)value.Start, (float)value.End);
		}
		/// <summary>
		/// Defines an implicit conversion of a Linei value to a Linef.
		/// </summary>
		/// <param name="value">The value to convert to a Linef.</param>
		/// <returns>A Linef that has all components equal to value.</returns>
		public static implicit operator Linef(Linei value)
		{
			return new Linef((float)value.Start, (float)value.End);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Linef"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Linef"/> object or a type capable
		/// of implicit conversion to a <see cref="Linef"/> object, and its value
		/// is equal to the current <see cref="Linef"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Linef) return Equals((Linef)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// line have the same value.
		/// </summary>
		/// <param name="other">The line to compare.</param>
		/// <returns>true if this line and value have the same value; otherwise, false.</returns>
		public bool Equals(Linef other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two lines are equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Linef left, Linef right)
		{
			return left.Start == right.Start && left.End == right.End;
		}
		/// <summary>
		/// Returns a value that indicates whether two lines are not equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Linef left, Linef right)
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
		/// Writes the given <see cref="Linef"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Linef line)
		{
			writer.Write(line.Start);
			writer.Write(line.End);
		}
		/// <summary>
		/// Reads a <see cref="Linef"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Linef ReadLinef(this Ibasa.IO.BinaryReader reader)
		{
			return new Linef(reader.ReadSingle(), reader.ReadSingle());
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two lines are equal.
		/// </summary>
		/// <param name="left">The first line to compare.</param>
		/// <param name="right">The second line to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Linef left, Linef right)
		{
			return left == right;
		}
		#endregion
		#region Intersect
		/// <summary>
		/// Returns the intersection of two lines.
		/// </summary>
		/// <param name="left">The first line.</param>
		/// <param name="right">The second line.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static Linef Intersect(Linef left, Linef right)
		{
			var left_min = Functions.Min(left.Start, left.End);
			var left_max = Functions.Max(left.Start, left.End);
			var right_min = Functions.Min(right.Start, right.End);
			var right_max = Functions.Max(right.Start, right.End);
			var min = Functions.Max(left_min, right_min);
			var max = Functions.Min(left_max, right_max);
			return min < max ? new Linef(min, max) : Linef.Empty;
		}
		#endregion
	}
}
