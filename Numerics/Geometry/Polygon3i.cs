using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a polygon in a three-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Polygon3i: IEquatable<Polygon3i>, IFormattable
	{
		#region Constants
		#endregion
		#region Fields
		/// <summary>
		/// The coordinates of the points that make up this polygon.
		/// </summary>
		private readonly Point3i[] Points;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the number of points that make up this polygon.
		/// </summary>
		/// <returns>The number of points that make up this polygon.</returns>
		public int Count
		{
			get
			{
				return Points == null ? 0 : Points.Length;
			}
		}
		/// <summary>
		/// Returns the indexed point of this polygon.
		/// </summary>
		/// <param name="index">The index of the point.</param>
		/// <returns>The value of the indexed point.</returns>
		public Point3i this[int index]
		{
			get
			{
				if (index < 0 || index >= Count)
				{
					throw new IndexOutOfRangeException("Index out of range.");
				}
				return Points[index];
			}
		}
		/// <summary>
		/// Gets the coordinates of the center of this polygon.
		/// </summary>
		public Point3d Center
		{
			get
			{
				return Points == null ? Point3d.Zero : Point.Sum(Points, 1.0f / Points.Length);
			}
		}
		public Point3i[] ToArray()
		{
			var result = new Point3i[Count];
			for (int i=0; i<Count; ++i)
			{
				result[i] = this[i];
			}
			return result;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Polygon3i"/> using the specified values.
		/// </summary>
		/// <param name="points">Points to define this polygon.</param>
		private Polygon3i(Point3i[] points)
		{
			Contract.Requires(points != null);
			Points = points;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Polygon3i"/> using the specified values.
		/// </summary>
		/// <param name="points">Points to define this polygon.</param>
		public Polygon3i(IEnumerable<Point3i> points)
		{
			Contract.Requires(points != null);
			Points = points.ToArray();
		}
		#endregion
		#region Operations
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Polygon3d value to a Polygon3i.
		/// </summary>
		/// <param name="value">The value to convert to a Polygon3i.</param>
		/// <returns>A Polygon3i that has all components equal to value.</returns>
		public static explicit operator Polygon3i(Polygon3d value)
		{
			var points = new Point3i[value.Count];
			for (int i=0; i<points.Length; ++i)
			{
				points[i] = (Point3i)value[i];
			}
			return new Polygon3i(points);
		}
		/// <summary>
		/// Defines an explicit conversion of a Polygon3f value to a Polygon3i.
		/// </summary>
		/// <param name="value">The value to convert to a Polygon3i.</param>
		/// <returns>A Polygon3i that has all components equal to value.</returns>
		public static explicit operator Polygon3i(Polygon3f value)
		{
			var points = new Point3i[value.Count];
			for (int i=0; i<points.Length; ++i)
			{
				points[i] = (Point3i)value[i];
			}
			return new Polygon3i(points);
		}
		/// <summary>
		/// Defines an explicit conversion of a Polygon3l value to a Polygon3i.
		/// </summary>
		/// <param name="value">The value to convert to a Polygon3i.</param>
		/// <returns>A Polygon3i that has all components equal to value.</returns>
		public static explicit operator Polygon3i(Polygon3l value)
		{
			var points = new Point3i[value.Count];
			for (int i=0; i<points.Length; ++i)
			{
				points[i] = (Point3i)value[i];
			}
			return new Polygon3i(points);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Polygon3i"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Points.Sum(point => point.GetHashCode());
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Polygon3i"/> object or a type capable
		/// of implicit conversion to a <see cref="Polygon3i"/> object, and its value
		/// is equal to the current <see cref="Polygon3i"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Polygon3i) return Equals((Polygon3i)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// polygon have the same value.
		/// </summary>
		/// <param name="other">The polygon to compare.</param>
		/// <returns>true if this polygon and value have the same value; otherwise, false.</returns>
		public bool Equals(Polygon3i other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two polygons are equal.
		/// </summary>
		/// <param name="left">The first polygon to compare.</param>
		/// <param name="right">The second polygon to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Polygon3i left, Polygon3i right)
		{
			if (left.Points.Length != right.Points.Length) { return false; }
			var left_points = left.Points.GetEnumerator();
			var right_points = right.Points.GetEnumerator();
			while (left_points.MoveNext() && right_points.MoveNext())
			{
				if (left_points.Current != right_points.Current) { return false; }
			}
			return true;
		}
		/// <summary>
		/// Returns a value that indicates whether two polygons are not equal.
		/// </summary>
		/// <param name="left">The first polygon to compare.</param>
		/// <param name="right">The second polygon to compare.</param>
		/// <returns>true if left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Polygon3i left, Polygon3i right)
		{
			if (left.Points.Length != right.Points.Length) { return true; }
			var left_points = left.Points.GetEnumerator();
			var right_points = right.Points.GetEnumerator();
			while (left_points.MoveNext() && right_points.MoveNext())
			{
				if (left_points.Current != right_points.Current) { return true; }
			}
			return false;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current polygon to its equivalent string
		/// representation.
		/// </summary>
		/// <returns>The string representation of the current instance.</returns>
		public override string ToString()
		{
			Contract.Ensures(Contract.Result<string>() != null);
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current polygon to its equivalent string
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
		/// Converts the value of the current polygon to its equivalent string
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
		/// Converts the value of the current polygon to its equivalent string
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
			return "Polygon";
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for polygon functions.
	/// </summary>
	public static partial class Polygon
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Polygon3i"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Polygon3i polygon)
		{
			var array = polygon.ToArray();
			writer.Write(array.Length);
			foreach(var point in array)
			{
				writer.Write(point);
			}
		}
		/// <summary>
		/// Reads a <see cref="Polygon3i"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Polygon3i ReadPolygon3i(this Ibasa.IO.BinaryReader reader)
		{
			var length = reader.ReadInt32();
			var array = new Point3i[length];
			for (int i=0; i<length; ++i)
			{
				array[i] = reader.ReadPoint3i();
			}
			return new Polygon3i(array);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two polygons are equal.
		/// </summary>
		/// <param name="left">The first polygon to compare.</param>
		/// <param name="right">The second polygon to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Polygon3i left, Polygon3i right)
		{
			return left == right;
		}
		#endregion
	}
}
