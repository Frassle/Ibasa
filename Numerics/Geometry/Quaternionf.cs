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
	public struct Quaternionf: IEquatable<Quaternionf>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Quaternionf"/> instance equal to zero.
		/// </summary>
		public static readonly Quaternionf Zero = new Quaternionf();
		/// <summary>
		/// Returns a new <see cref="Quaternionf"/> instance with a real number equal to one.
		/// </summary>
		public static readonly Quaternionf One = new Quaternionf(1, 0, 0, 0);
		/// <summary>
		/// Returns a new <see cref="Quaternionf"/> instance with i equal to one.
		/// </summary>
		public static readonly Quaternionf I = new Quaternionf(0, 1, 0, 0);
		/// <summary>
		/// Returns a new <see cref="Quaternionf"/> instance with j equal to one.
		/// </summary>
		public static readonly Quaternionf J = new Quaternionf(0, 0, 1, 0);
		/// <summary>
		/// Returns a new <see cref="Quaternionf"/> instance with k equal to one.
		/// </summary>
		public static readonly Quaternionf K = new Quaternionf(0, 0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The real component of the quaternion.
		/// </summary>
		public readonly float A;
		/// <summary>
		/// The i component of the quaternion.
		/// </summary>
		public readonly float B;
		/// <summary>
		/// The j component of the quaternion.
		/// </summary>
		public readonly float C;
		/// <summary>
		/// The k component of the quaternion.
		/// </summary>
		public readonly float D;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this quaternion.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public float this[int index]
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
						throw new IndexOutOfRangeException("Indices for Quaternionf run from 0 to 3, inclusive.");
				}
			}
		}
		public float[] ToArray()
		{
			return new float[]
			{
				A, B, C, D
			};
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Quaternionf"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Quaternionf(float value)
		{
			A = value;
			B = value;
			C = value;
			D = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Quaternionf"/> using the specified values.
		/// </summary>
		/// <param name="a">The real component of the quaternion.</param>
		/// <param name="b">The i component of the quaternion.</param>
		/// <param name="c">The j component of the quaternion.</param>
		/// <param name="d">The k component of the quaternion.</param>
		public Quaternionf(float a, float b, float c, float d)
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
		public static Quaternionf operator +(Quaternionf value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The negative of value.</returns>
		public static Quaternionf operator -(Quaternionf value)
		{
			return Quaternion.Negative(value);
		}
		/// <summary>
		/// Adds two quaternions and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Quaternionf operator +(Quaternionf left, Quaternionf right)
		{
			return Quaternion.Add(left, right);
		}
		/// <summary>
		/// Subtracts one quaternion from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Quaternionf operator -(Quaternionf left, Quaternionf right)
		{
			return Quaternion.Subtract(left, right);
		}
		/// <summary>
		/// Multiplies one quaternion by another and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Quaternionf operator *(Quaternionf left, Quaternionf right)
		{
			return Quaternion.Multiply(left, right);
		}
		/// <summary>
		/// Divides one quaternion by another and returns the result.
		/// </summary>
		/// <param name="left">The quaternion to be divided (the dividend).</param>
		/// <param name="right">The quaternion to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Quaternionf operator /(Quaternionf left, Quaternionf right)
		{
			return Quaternion.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Quaterniond value to a Quaternionf.
		/// </summary>
		/// <param name="value">The value to convert to a Quaternionf.</param>
		/// <returns>A Quaternionf that has all components equal to value.</returns>
		public static explicit operator Quaternionf(Quaterniond value)
		{
			return new Quaternionf((float)value.A, (float)value.B, (float)value.C, (float)value.D);
		}
		/// <summary>
		/// Defines an explicit conversion of a double value to a Quaternionf.
		/// </summary>
		/// <param name="value">The value to convert to a Quaternionf.</param>
		/// <returns>A Quaternionf that has all a real component equal to value.</returns>
		public static explicit operator Quaternionf(double value)
		{
			return new Quaternionf((float)value, 0, 0, 0);
		}
		/// <summary>
		/// Defines an implicit conversion of a float value to a Quaternionf.
		/// </summary>
		/// <param name="value">The value to convert to a Quaternionf.</param>
		/// <returns>A Quaternionf that has all a real component equal to value.</returns>
		public static implicit operator Quaternionf(float value)
		{
			return new Quaternionf((float)value, 0, 0, 0);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Quaternionf"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Quaternionf"/> object or a type capable
		/// of implicit conversion to a <see cref="Quaternionf"/> object, and its value
		/// is equal to the current <see cref="Quaternionf"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Quaternionf) { return Equals((Quaternionf)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// quaternion have the same value.
		/// </summary>
		/// <param name="other">The quaternion to compare.</param>
		/// <returns>true if this quaternion and value have the same value; otherwise, false.</returns>
		public bool Equals(Quaternionf other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two quaternions are equal.
		/// </summary>
		/// <param name="left">The first quaternion to compare.</param>
		/// <param name="right">The second quaternion to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Quaternionf left, Quaternionf right)
		{
			return left.A == right.A & left.B == right.B & left.C == right.C & left.D == right.D;
		}
		/// <summary>
		/// Returns a value that indicates whether two quaternions are not equal.
		/// </summary>
		/// <param name="left">The first quaternion to compare.</param>
		/// <param name="right">The second quaternion to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Quaternionf left, Quaternionf right)
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
		/// Initializes a new instance of the <see cref="Quaternionf"/> structure given a rotation and an axis.
		/// </summary>
		/// <param name="axis">The axis of rotation.</param>
		/// <param name="angle">The angle of rotation.</param>
		/// <returns>The newly created quaternion.</returns>
		public static Quaternionf FromRotationAxis(Vector3f axis, float angle)
		{
			axis = Vector.Normalize(axis);
			var half = angle * 0.5f;
			var sin =  Functions.Sin(half);
			var cos =  Functions.Cos(half);
			return new Quaternionf(cos, axis.X * sin, axis.Y * sin, axis.Z * sin);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Quaternionf"/> structure given a yaw, pitch, and roll value.
		/// </summary>
		/// <param name="yaw">The yaw of rotation.</param>
		/// <param name="pitch">The pitch of rotation.</param>
		/// <param name="roll">The roll of rotation.</param>
		/// <returns>The newly created quaternion.</returns>
		public static Quaternionf FromRotationAngles(float yaw, float pitch, float roll)
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
			return new Quaternionf(
				(cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll),
				(cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll),
				(sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll),
				(cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll));
		}
		#endregion
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Quaternionf"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Quaternionf quaternion)
		{
			writer.Write(quaternion.A);
			writer.Write(quaternion.B);
			writer.Write(quaternion.C);
			writer.Write(quaternion.D);
		}
		/// <summary>
		/// Reads a <see cref="Quaternionf"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Quaternionf ReadQuaternionf(this Ibasa.IO.BinaryReader reader)
		{
			return new Quaternionf(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The negative of value.</returns>
		public static Quaternionf Negative(Quaternionf value)
		{
			return new Quaternionf(-value.A, -value.B, -value.C, -value.D);
		}
		/// <summary>
		/// Adds two quaternions and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Quaternionf Add(Quaternionf left, Quaternionf right)
		{
			return new Quaternionf(left.A + right.A, left.B + right.B, left.C + right.C, left.D + right.D);
		}
		/// <summary>
		/// Subtracts one quaternion from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Quaternionf Subtract(Quaternionf left, Quaternionf right)
		{
			return new Quaternionf(left.A - right.A, left.B - right.B, left.C - right.C, left.D - right.D);
		}
		/// <summary>
		/// Returns the product of two quaternions.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Quaternionf Multiply(Quaternionf left, Quaternionf right)
		{
			return new Quaternionf(
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
		public static Quaternionf Divide(Quaternionf left, Quaternionf right)
		{
			return new Quaternionf(
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
		public static bool Equals(Quaternionf left, Quaternionf right)
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
		public static bool All(Quaternionf value)
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
		public static bool All(Quaternionf value, Predicate<float> predicate)
		{
			return predicate(value.A) && predicate(value.B) && predicate(value.C) && predicate(value.D);
		}
		/// <summary>
		/// Determines whether any component of a quaternion is non-zero.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Quaternionf value)
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
		public static bool Any(Quaternionf value, Predicate<float> predicate)
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
		public static Quaternionf Real(Quaternionf value)
		{
			return new Quaternionf(value.A, 0, 0, 0);
		}
		/// <summary>
		/// Return imaginary part of a quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The imaginary part of a quaternion.</returns>
		public static Quaternionf Imaginary(Quaternionf value)
		{
			return new Quaternionf(0, value.B, value.C, value.D);
		}
		/// <summary>
		/// Computes the absolute squared value of a quaternion and returns the result.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The absolute squared value of value.</returns>
		public static float AbsoluteSquared(Quaternionf value)
		{
			return (value.A * value.A + value.B * value.B + value.C * value.C + value.D * value.D);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a quaternion and returns the result.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The absolute value of value.</returns>
		public static float Absolute(Quaternionf value)
		{
			return Functions.Sqrt(value.A * value.A + value.B * value.B + value.C * value.C + value.D * value.D);
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a quaternion.
		/// </summary>
		/// <param name="value">A quaternion.</param>
		/// <returns>The normalized value of value.</returns>
		public static Quaternionf Normalize(Quaternionf value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Quaternionf.Zero;
			}
			return value / absolute;
			}
			/// <summary>
			/// Returns the multiplicative inverse of a quaternion.
			/// </summary>
			/// <param name="value">A quaternion.</param>
			/// <returns>The reciprocal of value.</returns>
			public static Quaternionf Reciprocal(Quaternionf value)
			{
				var absoluteSquared = AbsoluteSquared(value);
				return new Quaternionf(
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
			public static Quaternionf Conjugate(Quaternionf value)
			{
				return new Quaternionf(value.A, -value.B, -value.C, -value.D);
			}
			/// <summary>
			/// Computes the argument of a quaternion and returns the result.
			/// </summary>
			/// <param name="value">A quaternion.</param>
			/// <returns>The argument of value.</returns>
			public static float Argument(Quaternionf value)
			{
				return Functions.Atan2(Absolute(Imaginary(value)), value.A);
			}
			#endregion
			#region Transform
			public static Vector4f Transform(Vector4f vector, Quaternionf rotation)
			{
				var v = rotation * new Quaternionf(vector.X, vector.Y, vector.Z, 0) * Conjugate(rotation);
				return new Vector4f(v.A, v.B, v.C, vector.W);
			}
			public static Vector3f Transform(Vector3f vector, Quaternionf rotation)
			{
				var v = rotation * new Quaternionf(vector.X, vector.Y, vector.Z, 0) * Conjugate(rotation);
				return new Vector3f(v.A, v.B, v.C);
			}
			#endregion
		}
	}
