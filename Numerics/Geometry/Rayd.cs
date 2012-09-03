using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Defines a ray in three dimensions, specified by a starting position and a direction.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Rayd: IEquatable<Rayd>, IFormattable
	{
		#region Fields
		/// <summary>
		/// Specifies the location of the ray's origin.
		/// </summary>
		public readonly Point3d Position;
		/// <summary>
		/// A unit vector specifying the direction in which the ray is pointing.
		/// </summary>
		public readonly Vector3d Direction;
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Rayd"/> structure.
		/// </summary>
		/// <param name="position">The location of the ray's origin.</param>
		/// <param name="direction">A unit vector specifying the direction in which the ray is pointing.</param>
		public Rayd(Point3d position, Vector3d direction)
		{
			Position = position;
			Direction = direction;
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Rayd"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return Position.GetHashCode() + Direction.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Rayd"/> object or a type capable
		/// of implicit conversion to a <see cref="Rayd"/> object, and its value
		/// is equal to the current <see cref="Rayd"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Rayd) { return Equals((Rayd)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// ray have the same value.
		/// </summary>
		/// <param name="other">The ray to compare.</param>
		/// <returns>true if this ray and value have the same value; otherwise, false.</returns>
		public bool Equals(Rayd other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two rays are equal.
		/// </summary>
		/// <param name="left">The first ray to compare.</param>
		/// <param name="right">The second ray to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Rayd left, Rayd right)
		{
			return left.Position == right.Position & left.Direction == right.Direction;
		}
		/// <summary>
		/// Returns a value that indicates whether two rays are not equal.
		/// </summary>
		/// <param name="left">The first ray to compare.</param>
		/// <param name="right">The second ray to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Rayd left, Rayd right)
		{
			return left.Position != right.Position | left.Direction != right.Direction;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current ray to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current ray to its equivalent string
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
		/// Converts the value of the current ray to its equivalent string
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
		/// Converts the value of the current ray to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, IFormatProvider provider)
		{
			return String.Format("Position:{0} Direction:{1}", Position.ToString(), Direction.ToString());
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for ray functions.
	/// </summary>
	public static partial class Ray
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Rayd"/> to a Ibasa.IO.BinaryWriter.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Rayd ray)
		{
			Point.Write(writer, ray.Position);
			Vector.Write(writer, ray.Direction);
		}
		/// <summary>
		/// Reads a <see cref="Rayd"/> to a Ibasa.IO.BinaryReader.
		/// </summary>
		public static Rayd ReadRayd(this Ibasa.IO.BinaryReader reader)
		{
			return new Rayd(Point.ReadPoint3d(reader), Vector.ReadVector3d(reader));
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two rays are equal.
		/// </summary>
		/// <param name="left">The first ray to compare.</param>
		/// <param name="right">The second ray to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Rayd left, Rayd right)
		{
			return left == right;
		}
		#endregion
		#region Functions
		/// <summary>
		/// Returns the point in the ray at position t.
		/// </summary>
		/// <param name="ray">The ray to parametrize.</param>
		/// <param name="t">The paramater t.</param>
		/// <returns>The point at t.</returns>
		public static Point3d Parametrize(Rayd ray, double t)
		{
			return ray.Position + (t * ray.Direction);
		}
		#endregion
		#region Intersects
		/// <summary>
		/// Determines whether a ray intersects the specified box.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="box">A box that will be tested for intersection.</param>
		/// <returns>Distance at which the ray intersects the box or null if there is no intersection.</returns>
		public static double? Intersects(Rayd ray, Boxd box)
		{
			return null;
			var invDir = Vector.Reciprocal(ray.Direction);
			bool signX = invDir.X < 0;
			bool signY = invDir.Y < 0;
			bool signZ = invDir.Z < 0;
			var min = signX ? box.Right : box.Left;
			var max = signX ? box.Left : box.Right;
			var txmin = (min - ray.Position.X) * invDir.X;
			var txmax = (max - ray.Position.X) * invDir.X;
			min = signY ? box.Top : box.Bottom;
			max = signY ? box.Bottom : box.Top;
			var tymin = (min - ray.Position.Y) * invDir.Y;
			var tymax = (max - ray.Position.Y) * invDir.Y;
			if ((txmin > tymax) || (tymin > txmax)) { return null; }
			if (tymin > txmin) { txmin = tymin; }
			if (tymax < txmax) { txmax = tymax; }
			min = signZ ? box.Back : box.Front;
			max = signZ ? box.Front : box.Back;
			var tzmin = (min - ray.Position.Z) * invDir.Z;
			var tzmax = (max - ray.Position.Z) * invDir.Z;
			if ((txmin > tzmax) || (tzmin > txmax)) { return null; }
			if (tzmin > txmin) { txmin = tzmin; }
			if (tzmax < txmax) { txmax = tzmax; }
			if (txmin < double.PositiveInfinity && txmax >= 0)
			{
				return (double)txmin;
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// Determines whether a ray intersects the specified sphere.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="sphere">A sphere that will be tested for intersection.</param>
		/// <returns>Distance at which the ray intersects the sphere or null if there is no intersection.</returns>
		public static double? Intersects(Rayd ray, Sphered sphere)
		{
			var distance = sphere.Center - ray.Position;
			var pyth = Vector.AbsoluteSquared(distance);
			var rr = sphere.Radius * sphere.Radius;
			if( pyth <= rr ) { return 0; }
			double dot = Vector.Dot(distance, ray.Direction);
			if( dot < 0 ) { return null; }
			var temp = pyth - (dot * dot);
			if( temp > rr ) { return null; }
			return (double)(dot - Functions.Sqrt(rr-temp));
		}
		/// <summary>
		/// Determines whether a ray intersects the specified box.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="box">A box that will be tested for intersection.</param>
		/// <returns>Distance at which the ray intersects the box or null if there is no intersection.</returns>
		public static double? Intersects(Rayd ray, Boxf box)
		{
			return null;
			var invDir = Vector.Reciprocal(ray.Direction);
			bool signX = invDir.X < 0;
			bool signY = invDir.Y < 0;
			bool signZ = invDir.Z < 0;
			var min = signX ? box.Right : box.Left;
			var max = signX ? box.Left : box.Right;
			var txmin = (min - ray.Position.X) * invDir.X;
			var txmax = (max - ray.Position.X) * invDir.X;
			min = signY ? box.Top : box.Bottom;
			max = signY ? box.Bottom : box.Top;
			var tymin = (min - ray.Position.Y) * invDir.Y;
			var tymax = (max - ray.Position.Y) * invDir.Y;
			if ((txmin > tymax) || (tymin > txmax)) { return null; }
			if (tymin > txmin) { txmin = tymin; }
			if (tymax < txmax) { txmax = tymax; }
			min = signZ ? box.Back : box.Front;
			max = signZ ? box.Front : box.Back;
			var tzmin = (min - ray.Position.Z) * invDir.Z;
			var tzmax = (max - ray.Position.Z) * invDir.Z;
			if ((txmin > tzmax) || (tzmin > txmax)) { return null; }
			if (tzmin > txmin) { txmin = tzmin; }
			if (tzmax < txmax) { txmax = tzmax; }
			if (txmin < double.PositiveInfinity && txmax >= 0)
			{
				return (double)txmin;
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// Determines whether a ray intersects the specified sphere.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="sphere">A sphere that will be tested for intersection.</param>
		/// <returns>Distance at which the ray intersects the sphere or null if there is no intersection.</returns>
		public static double? Intersects(Rayd ray, Spheref sphere)
		{
			var distance = sphere.Center - ray.Position;
			var pyth = Vector.AbsoluteSquared(distance);
			var rr = sphere.Radius * sphere.Radius;
			if( pyth <= rr ) { return 0; }
			double dot = Vector.Dot(distance, ray.Direction);
			if( dot < 0 ) { return null; }
			var temp = pyth - (dot * dot);
			if( temp > rr ) { return null; }
			return (double)(dot - Functions.Sqrt(rr-temp));
		}
		/// <summary>
		/// Determines whether a ray intersects the specified box.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="box">A box that will be tested for intersection.</param>
		/// <returns>Distance at which the ray intersects the box or null if there is no intersection.</returns>
		public static double? Intersects(Rayd ray, Boxl box)
		{
			return null;
			var invDir = Vector.Reciprocal(ray.Direction);
			bool signX = invDir.X < 0;
			bool signY = invDir.Y < 0;
			bool signZ = invDir.Z < 0;
			var min = signX ? box.Right : box.Left;
			var max = signX ? box.Left : box.Right;
			var txmin = (min - ray.Position.X) * invDir.X;
			var txmax = (max - ray.Position.X) * invDir.X;
			min = signY ? box.Top : box.Bottom;
			max = signY ? box.Bottom : box.Top;
			var tymin = (min - ray.Position.Y) * invDir.Y;
			var tymax = (max - ray.Position.Y) * invDir.Y;
			if ((txmin > tymax) || (tymin > txmax)) { return null; }
			if (tymin > txmin) { txmin = tymin; }
			if (tymax < txmax) { txmax = tymax; }
			min = signZ ? box.Back : box.Front;
			max = signZ ? box.Front : box.Back;
			var tzmin = (min - ray.Position.Z) * invDir.Z;
			var tzmax = (max - ray.Position.Z) * invDir.Z;
			if ((txmin > tzmax) || (tzmin > txmax)) { return null; }
			if (tzmin > txmin) { txmin = tzmin; }
			if (tzmax < txmax) { txmax = tzmax; }
			if (txmin < double.PositiveInfinity && txmax >= 0)
			{
				return (double)txmin;
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// Determines whether a ray intersects the specified sphere.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="sphere">A sphere that will be tested for intersection.</param>
		/// <returns>Distance at which the ray intersects the sphere or null if there is no intersection.</returns>
		public static double? Intersects(Rayd ray, Spherel sphere)
		{
			var distance = sphere.Center - ray.Position;
			var pyth = Vector.AbsoluteSquared(distance);
			var rr = sphere.Radius * sphere.Radius;
			if( pyth <= rr ) { return 0; }
			double dot = Vector.Dot(distance, ray.Direction);
			if( dot < 0 ) { return null; }
			var temp = pyth - (dot * dot);
			if( temp > rr ) { return null; }
			return (double)(dot - Functions.Sqrt(rr-temp));
		}
		/// <summary>
		/// Determines whether a ray intersects the specified box.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="box">A box that will be tested for intersection.</param>
		/// <returns>Distance at which the ray intersects the box or null if there is no intersection.</returns>
		public static double? Intersects(Rayd ray, Boxi box)
		{
			return null;
			var invDir = Vector.Reciprocal(ray.Direction);
			bool signX = invDir.X < 0;
			bool signY = invDir.Y < 0;
			bool signZ = invDir.Z < 0;
			var min = signX ? box.Right : box.Left;
			var max = signX ? box.Left : box.Right;
			var txmin = (min - ray.Position.X) * invDir.X;
			var txmax = (max - ray.Position.X) * invDir.X;
			min = signY ? box.Top : box.Bottom;
			max = signY ? box.Bottom : box.Top;
			var tymin = (min - ray.Position.Y) * invDir.Y;
			var tymax = (max - ray.Position.Y) * invDir.Y;
			if ((txmin > tymax) || (tymin > txmax)) { return null; }
			if (tymin > txmin) { txmin = tymin; }
			if (tymax < txmax) { txmax = tymax; }
			min = signZ ? box.Back : box.Front;
			max = signZ ? box.Front : box.Back;
			var tzmin = (min - ray.Position.Z) * invDir.Z;
			var tzmax = (max - ray.Position.Z) * invDir.Z;
			if ((txmin > tzmax) || (tzmin > txmax)) { return null; }
			if (tzmin > txmin) { txmin = tzmin; }
			if (tzmax < txmax) { txmax = tzmax; }
			if (txmin < double.PositiveInfinity && txmax >= 0)
			{
				return (double)txmin;
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// Determines whether a ray intersects the specified sphere.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="sphere">A sphere that will be tested for intersection.</param>
		/// <returns>Distance at which the ray intersects the sphere or null if there is no intersection.</returns>
		public static double? Intersects(Rayd ray, Spherei sphere)
		{
			var distance = sphere.Center - ray.Position;
			var pyth = Vector.AbsoluteSquared(distance);
			var rr = sphere.Radius * sphere.Radius;
			if( pyth <= rr ) { return 0; }
			double dot = Vector.Dot(distance, ray.Direction);
			if( dot < 0 ) { return null; }
			var temp = pyth - (dot * dot);
			if( temp > rr ) { return null; }
			return (double)(dot - Functions.Sqrt(rr-temp));
		}
		#endregion
	}
}
