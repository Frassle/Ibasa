using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a quaternion, of the form (A + Bi + Cj + Dk).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Quaterniond: IEquatable<Quaterniond>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Quaterniond"/> instance equal to zero.
		/// </summary>
		public static readonly Quaterniond Zero = new Quaterniond();
		/// <summary>
		/// Returns a new <see cref="Quaterniond"/> instance with a real number equal to one.
		/// </summary>
		public static readonly Quaterniond One = new Quaterniond(1, 0, 0, 0);
		/// <summary>
		/// Returns a new <see cref="Quaterniond"/> instance with i equal to one.
		/// </summary>
		public static readonly Quaterniond I = new Quaterniond(0, 1, 0, 0);
		/// <summary>
		/// Returns a new <see cref="Quaterniond"/> instance with j equal to one.
		/// </summary>
		public static readonly Quaterniond J = new Quaterniond(0, 0, 1, 0);
		/// <summary>
		/// Returns a new <see cref="Quaterniond"/> instance with k equal to one.
		/// </summary>
		public static readonly Quaterniond K = new Quaterniond(0, 0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The real component of the quaternion.
		/// </summary>
		public readonly double A;
		/// <summary>
		/// The i component of the quaternion.
		/// </summary>
		public readonly double B;
		/// <summary>
		/// The j component of the quaternion.
		/// </summary>
		public readonly double C;
		/// <summary>
		/// The k component of the quaternion.
		/// </summary>
		public readonly double D;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this quaternion.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public double this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return A;
					case 1:
						return B;
					case 2:
						return C;
					case 3:
						return D;
					default:
						throw new IndexOutOfRangeException("Indices for Quaterniond run from 0 to 3, inclusive.");
				}
			}
		}
		public double[] ToArray()
		{
			return new double[]
			{
				A, B, C, D
			};
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Quaterniond"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Quaterniond(double value)
		{
			A = value;
			B = value;
			C = value;
			D = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Quaterniond"/> using the specified values.
		/// </summary>
		/// <param name="a">The real component of the quaternion.</param>
		/// <param name="b">The i component of the quaternion.</param>
		/// <param name="c">The j component of the quaternion.</param>
		/// <param name="d">The k component of the quaternion.</param>
		public Quaterniond(double a, double b, double c, double d)
		{
			A = a;
			B = b;
			C = c;
			D = d;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The identity of value.</returns>
		public static Quaterniond operator +(Quaterniond value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The negative of value.</returns>
		public static Quaterniond operator -(Quaterniond value)
		{
			return Quaternion.Negative(value);
		}
		/// <summary>
		/// Adds two quaternions and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Quaterniond operator +(Quaterniond left, Quaterniond right)
		{
			return Quaternion.Add(left, right);
		}
		/// <summary>
		/// Subtracts one quaternion from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Quaterniond operator -(Quaterniond left, Quaterniond right)
		{
			return Quaternion.Subtract(left, right);
		}
		/// <summary>
		/// Multiplies one quaternion by another and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Quaterniond operator *(Quaterniond left, Quaterniond right)
		{
			return Quaternion.Multiply(left, right);
		}
		/// <summary>
		/// Divides one quaternion by another and returns the result.
		/// </summary>
		/// <param name="left">The quaternion to be divided (the dividend).</param>
		/// <param name="right">The quaternion to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Quaterniond operator /(Quaterniond left, Quaterniond right)
		{
			return Quaternion.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a double value to a Quaterniond.
		/// </summary>
		/// <param name="value">The value to convert to a Quaterniond.</param>
		/// <returns>A Quaterniond that has all a real component equal to value.</returns>
		public static implicit operator Quaterniond(double value)
		{
			return new Quaterniond((double)value, 0, 0, 0);
		}
		/// <summary>
		/// Defines an implicit conversion of a Quaternionf value to a Quaterniond.
		/// </summary>
		/// <param name="value">The value to convert to a Quaterniond.</param>
		/// <returns>A Quaterniond that has all components equal to value.</returns>
		public static implicit operator Quaterniond(Quaternionf value)
		{
			return new Quaterniond((double)value.A, (double)value.B, (double)value.C, (double)value.D);
		}
		/// <summary>
		/// Defines an implicit conversion of a float value to a Quaterniond.
		/// </summary>
		/// <param name="value">The value to convert to a Quaterniond.</param>
		/// <returns>A Quaterniond that has all a real component equal to value.</returns>
		public static implicit operator Quaterniond(float value)
		{
			return new Quaterniond((double)value, 0, 0, 0);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Quaterniond"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return A.GetHashCode() + B.GetHashCode() + C.GetHashCode() + D.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Quaterniond"/> object or a type capable
		/// of implicit conversion to a <see cref="Quaterniond"/> object, and its value
		/// is equal to the current <see cref="Quaterniond"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Quaterniond) { return Equals((Quaterniond)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// quaternion have the same value.
		/// </summary>
		/// <param name="other">The quaternion to compare.</param>
		/// <returns>true if this quaternion and value have the same value; otherwise, false.</returns>
		public bool Equals(Quaterniond other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two quaternions are equal.
		/// </summary>
		/// <param name="left">The first quaternion to compare.</param>
		/// <param name="right">The second quaternion to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Quaterniond left, Quaterniond right)
		{
			return left.A == right.A & left.B == right.B & left.C == right.C & left.D == right.D;
		}
		/// <summary>
		/// Returns a value that indicates whether two quaternions are not equal.
		/// </summary>
		/// <param name="left">The first quaternion to compare.</param>
		/// <param name="right">The second quaternion to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Quaterniond left, Quaterniond right)
		{
			return left.A != right.A | left.B != right.B | left.C != right.C | left.D != right.D;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current quaternion to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current quaternion to its equivalent string
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
		/// Converts the value of the current quaternion to its equivalent string
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
		/// Converts the value of the current quaternion to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, IFormatProvider provider)
		{
			return String.Format("{0} + {1}i + {2}j + {3}k", A.ToString(format, provider), B.ToString(format, provider), C.ToString(format, provider), D.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for quaternion functions.
	/// </summary>
	public static partial class Quaternion
	{
		#region Factory
		/// <summary>
		/// Initializes a new instance of the <see cref="Quaterniond"/> structure given a rotation and an axis.
		/// </summary>
		/// <param name="axis">The axis of rotation.</param>
		/// <param name="angle">The angle of rotation.</param>
		/// <returns>The newly created quaternion.</returns>
		public static Quaterniond FromRotationAxis(Vector3d axis, double angle)
		{
			axis = Vector.Normalize(axis);
			var half = angle * 0.5f;
			var sin =  Functions.Sin(half);
			var cos =  Functions.Cos(half);
			return new Quaterniond(cos, axis.X * sin, axis.Y * sin, axis.Z * sin);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Quaterniond"/> structure given a yaw, pitch, and roll value.
		/// </summary>
		/// <param name="yaw">The yaw of rotation.</param>
		/// <param name="pitch">The pitch of rotation.</param>
		/// <param name="roll">The roll of rotation.</param>
		/// <returns>The newly created quaternion.</returns>
		public static Quaterniond FromRotationAngles(double yaw, double pitch, double roll)
		{
			var halfRoll = roll * 0.5f;
			var sinRoll = Functions.Sin(halfRoll);
			var cosRoll = Functions.Cos(halfRoll);
			var halfPitch = pitch * 0.5f;
			var sinPitch = Functions.Sin(halfPitch);
			var cosPitch = Functions.Cos(halfPitch);
			var halfYaw = yaw * 0.5f;
			var sinYaw = Functions.Sin(halfYaw);
			var cosYaw = Functions.Cos(halfYaw);
			return new Quaterniond(
				(cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll),
				(cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll),
				(sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll),
				(cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll));
		}
		#endregion
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Quaterniond"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Quaterniond quaternion)
		{
			writer.Write(quaternion.A);
			writer.Write(quaternion.B);
			writer.Write(quaternion.C);
			writer.Write(quaternion.D);
		}
		/// <summary>
		/// Reads a <see cref="Quaterniond"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Quaterniond ReadQuaterniond(this Ibasa.IO.BinaryReader reader)
		{
			return new Quaterniond(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The negative of value.</returns>
		public static Quaterniond Negative(Quaterniond value)
		{
			return new Quaterniond(-value.A, -value.B, -value.C, -value.D);
		}
		/// <summary>
		/// Adds two quaternions and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Quaterniond Add(Quaterniond left, Quaterniond right)
		{
			return new Quaterniond(left.A + right.A, left.B + right.B, left.C + right.C, left.D + right.D);
		}
		/// <summary>
		/// Subtracts one quaternion from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Quaterniond Subtract(Quaterniond left, Quaterniond right)
		{
			return new Quaterniond(left.A - right.A, left.B - right.B, left.C - right.C, left.D - right.D);
		}
		/// <summary>
		/// Returns the product of two quaternions.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Quaterniond Multiply(Quaterniond left, Quaterniond right)
		{
			return new Quaterniond(
				(left.A * right.A) - ((left.B * right.B) + (left.C * right.C) + (left.D * right.D)),
				(left.A * right.B) + (left.B * right.A) + (left.D * right.C) - (left.C * right.D),
				(left.A * right.C) + (left.C * right.A) + (left.B * right.D) - (left.D * right.B),
				(left.A * right.D) + (left.D * right.A) + (left.C * right.B) - (left.B * right.C));
		}
		/// <summary>
		/// Divides one quaternion by another and returns the result.
		/// </summary>
		/// <param name="left">The quaternion to be divided (the dividend).</param>
		/// <param name="right">The quaternion to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Quaterniond Divide(Quaterniond left, Quaterniond right)
		{
			return new Quaterniond(
				(left.A * right.A) - ((left.B * -right.B) + (left.C * -right.C) + (left.D * -right.D)) / AbsoluteSquared(right),
				((left.A * -right.B) + (left.B * right.A) + (left.D * -right.C) - (left.C * -right.D)) / AbsoluteSquared(right),
				((left.A * -right.C) + (left.C * right.A) + (left.B * -right.D) - (left.D * -right.B)) / AbsoluteSquared(right),
				((left.A * -right.D) + (left.D * right.A) + (left.C * -right.B) - (left.B * -right.C)) / AbsoluteSquared(right));
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two quaternions are equal.
		/// </summary>
		/// <param name="left">The first quaternion to compare.</param>
		/// <param name="right">The second quaternion to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Quaterniond left, Quaterniond right)
		{
			return left == right;
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all components of a quaternion are non-zero.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>true if all components are non-zero; false otherwise.</returns>
		public static bool All(Quaterniond value)
		{
			return value.A != 0 && value.B != 0 && value.C != 0 && value.D != 0;
		}
		/// <summary>
		/// Determines whether all components of a quaternion satisfy a condition.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if every component of the quaternion passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Quaterniond value, Predicate<double> predicate)
		{
			return predicate(value.A) && predicate(value.B) && predicate(value.C) && predicate(value.D);
		}
		/// <summary>
		/// Determines whether any component of a quaternion is non-zero.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Quaterniond value)
		{
			return value.A != 0 || value.B != 0 || value.C != 0 || value.D != 0;
		}
		/// <summary>
		/// Determines whether any components of a quaternion satisfy a condition.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if any component of the quaternion passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Quaterniond value, Predicate<double> predicate)
		{
			return predicate(value.A) || predicate(value.B) || predicate(value.C) || predicate(value.D);
		}
		#endregion
		#region Properties
		/// <summary>
		/// Return real part of a quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The real part of a quaternion.</returns>
		public static Quaterniond Real(Quaterniond value)
		{
			return new Quaterniond(value.A, 0, 0, 0);
		}
		/// <summary>
		/// Return imaginary part of a quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The imaginary part of a quaternion.</returns>
		public static Quaterniond Imaginary(Quaterniond value)
		{
			return new Quaterniond(0, value.B, value.C, value.D);
		}
		/// <summary>
		/// Computes the absolute squared value of a quaternion and returns the result.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The absolute squared value of value.</returns>
		public static double AbsoluteSquared(Quaterniond value)
		{
			return (value.A * value.A + value.B * value.B + value.C * value.C + value.D * value.D);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a quaternion and returns the result.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The absolute value of value.</returns>
		public static double Absolute(Quaterniond value)
		{
			return Functions.Sqrt(value.A * value.A + value.B * value.B + value.C * value.C + value.D * value.D);
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The normalized value of value.</returns>
		public static Quaterniond Normalize(Quaterniond value)
		{
			var absolute = Absolute(value);
			if(absolute <= double.Epsilon)
			{
				return Quaterniond.Zero;
			}
			return value / absolute;
			}
			/// <summary>
			/// Returns the multiplicative inverse of a quaternion.
			/// </summary>
			/// <param name="value">A quaternion.</param>
			/// <returns>The reciprocal of value.</returns>
			public static Quaterniond Reciprocal(Quaterniond value)
			{
				var absoluteSquared = AbsoluteSquared(value);
				return new Quaterniond(
					value.A / absoluteSquared,
					-value.B / absoluteSquared,
					-value.C / absoluteSquared,
					-value.D / absoluteSquared);
			}
			/// <summary>
			/// Computes the conjugate of a quaternion and returns the result.
			/// </summary>
			/// <param name="value">A quaternion.</param>
			/// <returns>The conjugate of value.</returns>
			public static Quaterniond Conjugate(Quaterniond value)
			{
				return new Quaterniond(value.A, -value.B, -value.C, -value.D);
			}
			/// <summary>
			/// Computes the argument of a quaternion and returns the result.
			/// </summary>
			/// <param name="value">A quaternion.</param>
			/// <returns>The argument of value.</returns>
			public static double Argument(Quaterniond value)
			{
				return Functions.Atan2(Absolute(Imaginary(value)), value.A);
			}
			#endregion
		}
	}
