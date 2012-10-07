using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a polygon in a two-dimensional space.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Polygon2f: IEquatable<Polygon2f>, IFormattable
	{
		#region Constants
		#endregion
		#region Fields
		/// <summary>
		/// The coordinates of the points that make up this polygon.
		/// </summary>
		private readonly Point2f[] Points;
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
		public Point2f this[int index]
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
		public Point2f Center
		{
			get
			{
				return Points == null ? Point2f.Zero : Point.Sum(Points, 1.0f / Points.Length);
			}
		}
		public Point2f[] ToArray()
		{
			var result = new Point2f[Count];
			for (int i=0; i<Count; ++i)
			{
				result[i] = this[i];
			}
			return result;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Polygon2f"/> using the specified values.
		/// </summary>
		/// <param name="points">Points to define this polygon.</param>
		private Polygon2f(Point2f[] points)
		{
			Contract.Requires(points != null);
			Points = points;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Polygon2f"/> using the specified values.
		/// </summary>
		/// <param name="points">Points to define this polygon.</param>
		public Polygon2f(IEnumerable<Point2f> points)
		{
			Contract.Requires(points != null);
			Points = points.ToArray();
		}
		#endregion
		#region Operations
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Polygon2d value to a Polygon2f.
		/// </summary>
		/// <param name="value">The value to convert to a Polygon2f.</param>
		/// <returns>A Polygon2f that has all components equal to value.</returns>
		public static explicit operator Polygon2f(Polygon2d value)
		{
			var points = new Point2f[value.Count];
			for (int i=0; i<points.Length; ++i)
			{
				points[i] = (Point2f)value[i];
			}
			return new Polygon2f(points);
		}
		/// <summary>
		/// Defines an implicit conversion of a Polygon2l value to a Polygon2f.
		/// </summary>
		/// <param name="value">The value to convert to a Polygon2f.</param>
		/// <returns>A Polygon2f that has all components equal to value.</returns>
		public static implicit operator Polygon2f(Polygon2l value)
		{
			var points = new Point2f[value.Count];
			for (int i=0; i<points.Length; ++i)
			{
				points[i] = (Point2f)value[i];
			}
			return new Polygon2f(points);
		}
		/// <summary>
		/// Defines an implicit conversion of a Polygon2i value to a Polygon2f.
		/// </summary>
		/// <param name="value">The value to convert to a Polygon2f.</param>
		/// <returns>A Polygon2f that has all components equal to value.</returns>
		public static implicit operator Polygon2f(Polygon2i value)
		{
			var points = new Point2f[value.Count];
			for (int i=0; i<points.Length; ++i)
			{
				points[i] = (Point2f)value[i];
			}
			return new Polygon2f(points);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Polygon2f"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Polygon2f"/> object or a type capable
		/// of implicit conversion to a <see cref="Polygon2f"/> object, and its value
		/// is equal to the current <see cref="Polygon2f"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Polygon2f) return Equals((Polygon2f)obj);
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// polygon have the same value.
		/// </summary>
		/// <param name="other">The polygon to compare.</param>
		/// <returns>true if this polygon and value have the same value; otherwise, false.</returns>
		public bool Equals(Polygon2f other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two polygons are equal.
		/// </summary>
		/// <param name="left">The first polygon to compare.</param>
		/// <param name="right">The second polygon to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Polygon2f left, Polygon2f right)
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
		public static bool operator !=(Polygon2f left, Polygon2f right)
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
		/// Writes the given <see cref="Polygon2f"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Polygon2f polygon)
		{
			var array = polygon.ToArray();
			writer.Write(array.Length);
			foreach(var point in array)
			{
				writer.Write(point);
			}
		}
		/// <summary>
		/// Reads a <see cref="Polygon2f"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Polygon2f ReadPolygon2f(this Ibasa.IO.BinaryReader reader)
		{
			var length = reader.ReadInt32();
			var array = new Point2f[length];
			for (int i=0; i<length; ++i)
			{
				array[i] = reader.ReadPoint2f();
			}
			return new Polygon2f(array);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two polygons are equal.
		/// </summary>
		/// <param name="left">The first polygon to compare.</param>
		/// <param name="right">The second polygon to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Polygon2f left, Polygon2f right)
		{
			return left == right;
		}
		#endregion
		#region Projection
		/// <summary>
		/// Projects a polygon onto an axis.
		/// </summary>
		/// <param name="polygon">The polygon to project.</param>
		/// <param name="axis">The axis to project onto.</param>
		/// <returns>The interval on the axis that the polygon projects onto.</returns>
		public static Linef Project(Polygon2f polygon, Vector2f axis)
		{
			var min = float.MaxValue;
			var max = float.MinValue;
			for (int i=0; i < polygon.Count; ++i)
			{
				var proj = Vector.Dot((Vector2f)polygon[i], axis);
				min = Functions.Min(min, proj);
				max = Functions.Max(max, proj);
			}
			return new Linef(min, max);
		}
		#endregion
	}
}
