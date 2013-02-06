using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a 16 component vector of Halfs.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector16h: IEquatable<Vector16h>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector16h"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector16h Zero = new Vector16h(0);
		/// <summary>
		/// Returns a new <see cref="Vector16h"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector16h One = new Vector16h(1);
		#endregion
		#region Fields
		/// <summary>
		/// The first component of the vector.
		/// </summary>
		public readonly Half V0;
		/// <summary>
		/// The second component of the vector.
		/// </summary>
		public readonly Half V1;
		/// <summary>
		/// The third component of the vector.
		/// </summary>
		public readonly Half V2;
		/// <summary>
		/// The fourth component of the vector.
		/// </summary>
		public readonly Half V3;
		/// <summary>
		/// The fifth component of the vector.
		/// </summary>
		public readonly Half V4;
		/// <summary>
		/// The sixth component of the vector.
		/// </summary>
		public readonly Half V5;
		/// <summary>
		/// The seventh component of the vector.
		/// </summary>
		public readonly Half V6;
		/// <summary>
		/// The eighth component of the vector.
		/// </summary>
		public readonly Half V7;
		/// <summary>
		/// The nineth component of the vector.
		/// </summary>
		public readonly Half V8;
		/// <summary>
		/// The tenth component of the vector.
		/// </summary>
		public readonly Half V9;
		/// <summary>
		/// The eleventh component of the vector.
		/// </summary>
		public readonly Half V10;
		/// <summary>
		/// The twelfth component of the vector.
		/// </summary>
		public readonly Half V11;
		/// <summary>
		/// The thirteenth component of the vector.
		/// </summary>
		public readonly Half V12;
		/// <summary>
		/// The fourteenth component of the vector.
		/// </summary>
		public readonly Half V13;
		/// <summary>
		/// The fifeteenth component of the vector.
		/// </summary>
		public readonly Half V14;
		/// <summary>
		/// The sixteenth component of the vector.
		/// </summary>
		public readonly Half V15;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public Half this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return V0;
					case 1:
						return V1;
					case 2:
						return V2;
					case 3:
						return V3;
					case 4:
						return V4;
					case 5:
						return V5;
					case 6:
						return V6;
					case 7:
						return V7;
					case 8:
						return V8;
					case 9:
						return V9;
					case 10:
						return V10;
					case 11:
						return V11;
					case 12:
						return V12;
					case 13:
						return V13;
					case 14:
						return V14;
					case 15:
						return V15;
					default:
						throw new IndexOutOfRangeException("Indices for Vector16h run from 0 to 15, inclusive.");
				}
			}
		}
		public Half[] ToArray()
		{
			return new Half[]
			{
				V0, V1, V2, V3, V4, V5, V6, V7, V8, V9, V10, V11, V12, V13, V14, V15
			};
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector16h"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector16h(Half value)
		{
			V0 = value;
			V1 = value;
			V2 = value;
			V3 = value;
			V4 = value;
			V5 = value;
			V6 = value;
			V7 = value;
			V8 = value;
			V9 = value;
			V10 = value;
			V11 = value;
			V12 = value;
			V13 = value;
			V14 = value;
			V15 = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector16h"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the first 2 components</param>
		/// <param name="v2">Value for the V2 component of the vector.</param>
		/// <param name="v3">Value for the V3 component of the vector.</param>
		/// <param name="v4">Value for the V4 component of the vector.</param>
		/// <param name="v5">Value for the V5 component of the vector.</param>
		/// <param name="v6">Value for the V6 component of the vector.</param>
		/// <param name="v7">Value for the V7 component of the vector.</param>
		/// <param name="v8">Value for the V8 component of the vector.</param>
		/// <param name="v9">Value for the V9 component of the vector.</param>
		/// <param name="v10">Value for the V10 component of the vector.</param>
		/// <param name="v11">Value for the V11 component of the vector.</param>
		/// <param name="v12">Value for the V12 component of the vector.</param>
		/// <param name="v13">Value for the V13 component of the vector.</param>
		/// <param name="v14">Value for the V14 component of the vector.</param>
		/// <param name="v15">Value for the V15 component of the vector.</param>
		public Vector16h(Vector2h value, Half v2, Half v3, Half v4, Half v5, Half v6, Half v7, Half v8, Half v9, Half v10, Half v11, Half v12, Half v13, Half v14, Half v15, Half v16, Half v17)
		{
			V0 = value.X;
			V1 = value.Y;
			V2 = v2;
			V3 = v3;
			V4 = v4;
			V5 = v5;
			V6 = v6;
			V7 = v7;
			V8 = v8;
			V9 = v9;
			V10 = v10;
			V11 = v11;
			V12 = v12;
			V13 = v13;
			V14 = v14;
			V15 = v15;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector16h"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the first 3 components</param>
		/// <param name="v3">Value for the V3 component of the vector.</param>
		/// <param name="v4">Value for the V4 component of the vector.</param>
		/// <param name="v5">Value for the V5 component of the vector.</param>
		/// <param name="v6">Value for the V6 component of the vector.</param>
		/// <param name="v7">Value for the V7 component of the vector.</param>
		/// <param name="v8">Value for the V8 component of the vector.</param>
		/// <param name="v9">Value for the V9 component of the vector.</param>
		/// <param name="v10">Value for the V10 component of the vector.</param>
		/// <param name="v11">Value for the V11 component of the vector.</param>
		/// <param name="v12">Value for the V12 component of the vector.</param>
		/// <param name="v13">Value for the V13 component of the vector.</param>
		/// <param name="v14">Value for the V14 component of the vector.</param>
		/// <param name="v15">Value for the V15 component of the vector.</param>
		public Vector16h(Vector3h value, Half v3, Half v4, Half v5, Half v6, Half v7, Half v8, Half v9, Half v10, Half v11, Half v12, Half v13, Half v14, Half v15, Half v16, Half v17, Half v18)
		{
			V0 = value.X;
			V1 = value.Y;
			V2 = value.Z;
			V3 = v3;
			V4 = v4;
			V5 = v5;
			V6 = v6;
			V7 = v7;
			V8 = v8;
			V9 = v9;
			V10 = v10;
			V11 = v11;
			V12 = v12;
			V13 = v13;
			V14 = v14;
			V15 = v15;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector16h"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the first 4 components</param>
		/// <param name="v4">Value for the V4 component of the vector.</param>
		/// <param name="v5">Value for the V5 component of the vector.</param>
		/// <param name="v6">Value for the V6 component of the vector.</param>
		/// <param name="v7">Value for the V7 component of the vector.</param>
		/// <param name="v8">Value for the V8 component of the vector.</param>
		/// <param name="v9">Value for the V9 component of the vector.</param>
		/// <param name="v10">Value for the V10 component of the vector.</param>
		/// <param name="v11">Value for the V11 component of the vector.</param>
		/// <param name="v12">Value for the V12 component of the vector.</param>
		/// <param name="v13">Value for the V13 component of the vector.</param>
		/// <param name="v14">Value for the V14 component of the vector.</param>
		/// <param name="v15">Value for the V15 component of the vector.</param>
		public Vector16h(Vector4h value, Half v4, Half v5, Half v6, Half v7, Half v8, Half v9, Half v10, Half v11, Half v12, Half v13, Half v14, Half v15, Half v16, Half v17, Half v18, Half v19)
		{
			V0 = value.X;
			V1 = value.Y;
			V2 = value.Z;
			V3 = value.W;
			V4 = v4;
			V5 = v5;
			V6 = v6;
			V7 = v7;
			V8 = v8;
			V9 = v9;
			V10 = v10;
			V11 = v11;
			V12 = v12;
			V13 = v13;
			V14 = v14;
			V15 = v15;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector16h"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the first 8 components</param>
		/// <param name="v8">Value for the V8 component of the vector.</param>
		/// <param name="v9">Value for the V9 component of the vector.</param>
		/// <param name="v10">Value for the V10 component of the vector.</param>
		/// <param name="v11">Value for the V11 component of the vector.</param>
		/// <param name="v12">Value for the V12 component of the vector.</param>
		/// <param name="v13">Value for the V13 component of the vector.</param>
		/// <param name="v14">Value for the V14 component of the vector.</param>
		/// <param name="v15">Value for the V15 component of the vector.</param>
		public Vector16h(Vector8h value, Half v8, Half v9, Half v10, Half v11, Half v12, Half v13, Half v14, Half v15, Half v16, Half v17, Half v18, Half v19, Half v20, Half v21, Half v22, Half v23)
		{
			V0 = value.V0;
			V1 = value.V1;
			V2 = value.V2;
			V3 = value.V3;
			V4 = value.V4;
			V5 = value.V5;
			V6 = value.V6;
			V7 = value.V7;
			V8 = v8;
			V9 = v9;
			V10 = v10;
			V11 = v11;
			V12 = v12;
			V13 = v13;
			V14 = v14;
			V15 = v15;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector16h"/> using the specified values.
		/// </summary>
		/// <param name="v0">Value for the V0 component of the vector.</param>
		/// <param name="v1">Value for the V1 component of the vector.</param>
		/// <param name="v2">Value for the V2 component of the vector.</param>
		/// <param name="v3">Value for the V3 component of the vector.</param>
		/// <param name="v4">Value for the V4 component of the vector.</param>
		/// <param name="v5">Value for the V5 component of the vector.</param>
		/// <param name="v6">Value for the V6 component of the vector.</param>
		/// <param name="v7">Value for the V7 component of the vector.</param>
		/// <param name="v8">Value for the V8 component of the vector.</param>
		/// <param name="v9">Value for the V9 component of the vector.</param>
		/// <param name="v10">Value for the V10 component of the vector.</param>
		/// <param name="v11">Value for the V11 component of the vector.</param>
		/// <param name="v12">Value for the V12 component of the vector.</param>
		/// <param name="v13">Value for the V13 component of the vector.</param>
		/// <param name="v14">Value for the V14 component of the vector.</param>
		/// <param name="v15">Value for the V15 component of the vector.</param>
		public Vector16h(Half v0, Half v1, Half v2, Half v3, Half v4, Half v5, Half v6, Half v7, Half v8, Half v9, Half v10, Half v11, Half v12, Half v13, Half v14, Half v15)
		{
			V0 = v0;
			V1 = v1;
			V2 = v2;
			V3 = v3;
			V4 = v4;
			V5 = v5;
			V6 = v6;
			V7 = v7;
			V8 = v8;
			V9 = v9;
			V10 = v10;
			V11 = v11;
			V12 = v12;
			V13 = v13;
			V14 = v14;
			V15 = v15;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector16h"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector16h(Half[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			V0 = array[0];
			V1 = array[1];
			V2 = array[2];
			V3 = array[3];
			V4 = array[4];
			V5 = array[5];
			V6 = array[6];
			V7 = array[7];
			V8 = array[8];
			V9 = array[9];
			V10 = array[10];
			V11 = array[11];
			V12 = array[12];
			V13 = array[13];
			V14 = array[14];
			V15 = array[15];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector16h"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector16h(Half[] array, int offset)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			V0 = array[offset + 0];
			V1 = array[offset + 1];
			V2 = array[offset + 2];
			V3 = array[offset + 3];
			V4 = array[offset + 4];
			V5 = array[offset + 5];
			V6 = array[offset + 6];
			V7 = array[offset + 7];
			V8 = array[offset + 8];
			V9 = array[offset + 9];
			V10 = array[offset + 10];
			V11 = array[offset + 11];
			V12 = array[offset + 12];
			V13 = array[offset + 13];
			V14 = array[offset + 14];
			V15 = array[offset + 15];
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The identity of value.</returns>
		public static Vector16f operator +(Vector16h value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector16f operator -(Vector16h value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector16f operator +(Vector16h left, Vector16h right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector16f operator -(Vector16h left, Vector16h right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector16f operator *(Vector16h left, float right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector16f operator *(float left, Vector16h right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector16f operator /(Vector16h left, float right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector16d value to a Vector16h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16h.</param>
		/// <returns>A Vector16h that has all components equal to value.</returns>
		public static explicit operator Vector16h(Vector16d value)
		{
			return new Vector16h((Half)value.V0, (Half)value.V1, (Half)value.V2, (Half)value.V3, (Half)value.V4, (Half)value.V5, (Half)value.V6, (Half)value.V7, (Half)value.V8, (Half)value.V9, (Half)value.V10, (Half)value.V11, (Half)value.V12, (Half)value.V13, (Half)value.V14, (Half)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16f value to a Vector16h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16h.</param>
		/// <returns>A Vector16h that has all components equal to value.</returns>
		public static explicit operator Vector16h(Vector16f value)
		{
			return new Vector16h((Half)value.V0, (Half)value.V1, (Half)value.V2, (Half)value.V3, (Half)value.V4, (Half)value.V5, (Half)value.V6, (Half)value.V7, (Half)value.V8, (Half)value.V9, (Half)value.V10, (Half)value.V11, (Half)value.V12, (Half)value.V13, (Half)value.V14, (Half)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16ul value to a Vector16h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16h.</param>
		/// <returns>A Vector16h that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector16h(Vector16ul value)
		{
			return new Vector16h((Half)value.V0, (Half)value.V1, (Half)value.V2, (Half)value.V3, (Half)value.V4, (Half)value.V5, (Half)value.V6, (Half)value.V7, (Half)value.V8, (Half)value.V9, (Half)value.V10, (Half)value.V11, (Half)value.V12, (Half)value.V13, (Half)value.V14, (Half)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16l value to a Vector16h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16h.</param>
		/// <returns>A Vector16h that has all components equal to value.</returns>
		public static explicit operator Vector16h(Vector16l value)
		{
			return new Vector16h((Half)value.V0, (Half)value.V1, (Half)value.V2, (Half)value.V3, (Half)value.V4, (Half)value.V5, (Half)value.V6, (Half)value.V7, (Half)value.V8, (Half)value.V9, (Half)value.V10, (Half)value.V11, (Half)value.V12, (Half)value.V13, (Half)value.V14, (Half)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16ui value to a Vector16h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16h.</param>
		/// <returns>A Vector16h that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector16h(Vector16ui value)
		{
			return new Vector16h((Half)value.V0, (Half)value.V1, (Half)value.V2, (Half)value.V3, (Half)value.V4, (Half)value.V5, (Half)value.V6, (Half)value.V7, (Half)value.V8, (Half)value.V9, (Half)value.V10, (Half)value.V11, (Half)value.V12, (Half)value.V13, (Half)value.V14, (Half)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16i value to a Vector16h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16h.</param>
		/// <returns>A Vector16h that has all components equal to value.</returns>
		public static explicit operator Vector16h(Vector16i value)
		{
			return new Vector16h((Half)value.V0, (Half)value.V1, (Half)value.V2, (Half)value.V3, (Half)value.V4, (Half)value.V5, (Half)value.V6, (Half)value.V7, (Half)value.V8, (Half)value.V9, (Half)value.V10, (Half)value.V11, (Half)value.V12, (Half)value.V13, (Half)value.V14, (Half)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16us value to a Vector16h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16h.</param>
		/// <returns>A Vector16h that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector16h(Vector16us value)
		{
			return new Vector16h((Half)value.V0, (Half)value.V1, (Half)value.V2, (Half)value.V3, (Half)value.V4, (Half)value.V5, (Half)value.V6, (Half)value.V7, (Half)value.V8, (Half)value.V9, (Half)value.V10, (Half)value.V11, (Half)value.V12, (Half)value.V13, (Half)value.V14, (Half)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16s value to a Vector16h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16h.</param>
		/// <returns>A Vector16h that has all components equal to value.</returns>
		public static explicit operator Vector16h(Vector16s value)
		{
			return new Vector16h((Half)value.V0, (Half)value.V1, (Half)value.V2, (Half)value.V3, (Half)value.V4, (Half)value.V5, (Half)value.V6, (Half)value.V7, (Half)value.V8, (Half)value.V9, (Half)value.V10, (Half)value.V11, (Half)value.V12, (Half)value.V13, (Half)value.V14, (Half)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16b value to a Vector16h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16h.</param>
		/// <returns>A Vector16h that has all components equal to value.</returns>
		public static explicit operator Vector16h(Vector16b value)
		{
			return new Vector16h((Half)value.V0, (Half)value.V1, (Half)value.V2, (Half)value.V3, (Half)value.V4, (Half)value.V5, (Half)value.V6, (Half)value.V7, (Half)value.V8, (Half)value.V9, (Half)value.V10, (Half)value.V11, (Half)value.V12, (Half)value.V13, (Half)value.V14, (Half)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16sb value to a Vector16h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16h.</param>
		/// <returns>A Vector16h that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector16h(Vector16sb value)
		{
			return new Vector16h((Half)value.V0, (Half)value.V1, (Half)value.V2, (Half)value.V3, (Half)value.V4, (Half)value.V5, (Half)value.V6, (Half)value.V7, (Half)value.V8, (Half)value.V9, (Half)value.V10, (Half)value.V11, (Half)value.V12, (Half)value.V13, (Half)value.V14, (Half)value.V15);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector16h"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return V0.GetHashCode() + V1.GetHashCode() + V2.GetHashCode() + V3.GetHashCode() + V4.GetHashCode() + V5.GetHashCode() + V6.GetHashCode() + V7.GetHashCode() + V8.GetHashCode() + V9.GetHashCode() + V10.GetHashCode() + V11.GetHashCode() + V12.GetHashCode() + V13.GetHashCode() + V14.GetHashCode() + V15.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Vector16h"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector16h"/> object, and its value
		/// is equal to the current <see cref="Vector16h"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector16h) { return Equals((Vector16h)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector16h other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector16h left, Vector16h right)
		{
			return left.V0 == right.V0 & left.V1 == right.V1 & left.V2 == right.V2 & left.V3 == right.V3 & left.V4 == right.V4 & left.V5 == right.V5 & left.V6 == right.V6 & left.V7 == right.V7 & left.V8 == right.V8 & left.V9 == right.V9 & left.V10 == right.V10 & left.V11 == right.V11 & left.V12 == right.V12 & left.V13 == right.V13 & left.V14 == right.V14 & left.V15 == right.V15;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector16h left, Vector16h right)
		{
			return left.V0 != right.V0 | left.V1 != right.V1 | left.V2 != right.V2 | left.V3 != right.V3 | left.V4 != right.V4 | left.V5 != right.V5 | left.V6 != right.V6 | left.V7 != right.V7 | left.V8 != right.V8 | left.V9 != right.V9 | left.V10 != right.V10 | left.V11 != right.V11 | left.V12 != right.V12 | left.V13 != right.V13 | left.V14 != right.V14 | left.V15 != right.V15;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current vector to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current vector to its equivalent string
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
		/// Converts the value of the current vector to its equivalent string
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
		/// Converts the value of the current vector to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, IFormatProvider provider)
		{
			return String.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15})", V0.ToString(format, provider), V1.ToString(format, provider), V2.ToString(format, provider), V3.ToString(format, provider), V4.ToString(format, provider), V5.ToString(format, provider), V6.ToString(format, provider), V7.ToString(format, provider), V8.ToString(format, provider), V9.ToString(format, provider), V10.ToString(format, provider), V11.ToString(format, provider), V12.ToString(format, provider), V13.ToString(format, provider), V14.ToString(format, provider), V15.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for vector functions.
	/// </summary>
	public static partial class Vector
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Vector16h"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector16h vector)
		{
			writer.Write(vector.V0);
			writer.Write(vector.V1);
			writer.Write(vector.V2);
			writer.Write(vector.V3);
			writer.Write(vector.V4);
			writer.Write(vector.V5);
			writer.Write(vector.V6);
			writer.Write(vector.V7);
			writer.Write(vector.V8);
			writer.Write(vector.V9);
			writer.Write(vector.V10);
			writer.Write(vector.V11);
			writer.Write(vector.V12);
			writer.Write(vector.V13);
			writer.Write(vector.V14);
			writer.Write(vector.V15);
		}
		/// <summary>
		/// Reads a <see cref="Vector16h"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector16h ReadVector16h(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector16h(reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector16f Negative(Vector16h value)
		{
			return new Vector16f(-value.V0, -value.V1, -value.V2, -value.V3, -value.V4, -value.V5, -value.V6, -value.V7, -value.V8, -value.V9, -value.V10, -value.V11, -value.V12, -value.V13, -value.V14, -value.V15);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector16f Add(Vector16h left, Vector16h right)
		{
			return new Vector16f(left.V0 + right.V0, left.V1 + right.V1, left.V2 + right.V2, left.V3 + right.V3, left.V4 + right.V4, left.V5 + right.V5, left.V6 + right.V6, left.V7 + right.V7, left.V8 + right.V8, left.V9 + right.V9, left.V10 + right.V10, left.V11 + right.V11, left.V12 + right.V12, left.V13 + right.V13, left.V14 + right.V14, left.V15 + right.V15);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector16f Subtract(Vector16h left, Vector16h right)
		{
			return new Vector16f(left.V0 - right.V0, left.V1 - right.V1, left.V2 - right.V2, left.V3 - right.V3, left.V4 - right.V4, left.V5 - right.V5, left.V6 - right.V6, left.V7 - right.V7, left.V8 - right.V8, left.V9 - right.V9, left.V10 - right.V10, left.V11 - right.V11, left.V12 - right.V12, left.V13 - right.V13, left.V14 - right.V14, left.V15 - right.V15);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector16f Multiply(Vector16h vector, float scalar)
		{
			return new Vector16f(vector.V0 * scalar, vector.V1 * scalar, vector.V2 * scalar, vector.V3 * scalar, vector.V4 * scalar, vector.V5 * scalar, vector.V6 * scalar, vector.V7 * scalar, vector.V8 * scalar, vector.V9 * scalar, vector.V10 * scalar, vector.V11 * scalar, vector.V12 * scalar, vector.V13 * scalar, vector.V14 * scalar, vector.V15 * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector16f Divide(Vector16h vector, float scalar)
		{
			return new Vector16f(vector.V0 / scalar, vector.V1 / scalar, vector.V2 / scalar, vector.V3 / scalar, vector.V4 / scalar, vector.V5 / scalar, vector.V6 / scalar, vector.V7 / scalar, vector.V8 / scalar, vector.V9 / scalar, vector.V10 / scalar, vector.V11 / scalar, vector.V12 / scalar, vector.V13 / scalar, vector.V14 / scalar, vector.V15 / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Vector16h left, Vector16h right)
		{
			return left == right;
		}
		#endregion
		#region Products
		/// <summary>
		/// Calculates the dot product (inner product) of two vectors.
		/// </summary>
		/// <param name="left">First source vector.</param>
		/// <param name="right">Second source vector.</param>
		/// <returns>The dot product of the two vectors.</returns>
		public static float Dot(Vector16h left, Vector16h right)
		{
			return left.V0 * right.V0 + left.V1 * right.V1 + left.V2 * right.V2 + left.V3 * right.V3 + left.V4 * right.V4 + left.V5 * right.V5 + left.V6 * right.V6 + left.V7 * right.V7 + left.V8 * right.V8 + left.V9 * right.V9 + left.V10 * right.V10 + left.V11 * right.V11 + left.V12 * right.V12 + left.V13 * right.V13 + left.V14 * right.V14 + left.V15 * right.V15;
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all components of a vector are non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if all components are non-zero; false otherwise.</returns>
		public static bool All(Vector16h value)
		{
			return value.V0 != 0 && value.V1 != 0 && value.V2 != 0 && value.V3 != 0 && value.V4 != 0 && value.V5 != 0 && value.V6 != 0 && value.V7 != 0 && value.V8 != 0 && value.V9 != 0 && value.V10 != 0 && value.V11 != 0 && value.V12 != 0 && value.V13 != 0 && value.V14 != 0 && value.V15 != 0;
		}
		/// <summary>
		/// Determines whether all components of a vector satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if every component of the vector passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Vector16h value, Predicate<Half> predicate)
		{
			return predicate(value.V0) && predicate(value.V1) && predicate(value.V2) && predicate(value.V3) && predicate(value.V4) && predicate(value.V5) && predicate(value.V6) && predicate(value.V7) && predicate(value.V8) && predicate(value.V9) && predicate(value.V10) && predicate(value.V11) && predicate(value.V12) && predicate(value.V13) && predicate(value.V14) && predicate(value.V15);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Vector16h value)
		{
			return value.V0 != 0 || value.V1 != 0 || value.V2 != 0 || value.V3 != 0 || value.V4 != 0 || value.V5 != 0 || value.V6 != 0 || value.V7 != 0 || value.V8 != 0 || value.V9 != 0 || value.V10 != 0 || value.V11 != 0 || value.V12 != 0 || value.V13 != 0 || value.V14 != 0 || value.V15 != 0;
		}
		/// <summary>
		/// Determines whether any components of a vector satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if any component of the vector passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Vector16h value, Predicate<Half> predicate)
		{
			return predicate(value.V0) || predicate(value.V1) || predicate(value.V2) || predicate(value.V3) || predicate(value.V4) || predicate(value.V5) || predicate(value.V6) || predicate(value.V7) || predicate(value.V8) || predicate(value.V9) || predicate(value.V10) || predicate(value.V11) || predicate(value.V12) || predicate(value.V13) || predicate(value.V14) || predicate(value.V15);
		}
		#endregion
		#region Properties
		/// <summary>
		/// Computes the absolute squared value of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute squared value of value.</returns> 
		public static float AbsoluteSquared(Vector16h value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		public static float Absolute(Vector16h value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		public static Vector16f Normalize(Vector16h value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Vector16h.Zero;
			}
			else
			{
				return (Vector16f)value / absolute;
			}
		}
		#endregion
		#region Per component
		#region Transform
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16d Transform(Vector16h value, Func<Half, double> transformer)
		{
			return new Vector16d(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16f Transform(Vector16h value, Func<Half, float> transformer)
		{
			return new Vector16f(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16h Transform(Vector16h value, Func<Half, Half> transformer)
		{
			return new Vector16h(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16ul Transform(Vector16h value, Func<Half, ulong> transformer)
		{
			return new Vector16ul(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16l Transform(Vector16h value, Func<Half, long> transformer)
		{
			return new Vector16l(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16ui Transform(Vector16h value, Func<Half, uint> transformer)
		{
			return new Vector16ui(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16i Transform(Vector16h value, Func<Half, int> transformer)
		{
			return new Vector16i(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16us Transform(Vector16h value, Func<Half, ushort> transformer)
		{
			return new Vector16us(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16s Transform(Vector16h value, Func<Half, short> transformer)
		{
			return new Vector16s(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16b Transform(Vector16h value, Func<Half, byte> transformer)
		{
			return new Vector16b(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16sb Transform(Vector16h value, Func<Half, sbyte> transformer)
		{
			return new Vector16sb(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		#endregion
		/// <summary>
		/// Multiplys the components of two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first vector to modulate.</param>
		/// <param name="right">The second vector to modulate.</param>
		/// <returns>The result of multiplying each component of left by the matching component in right.</returns>
		public static Vector16f Modulate(Vector16h left, Vector16h right)
		{
			return new Vector16f(left.V0 * right.V0, left.V1 * right.V1, left.V2 * right.V2, left.V3 * right.V3, left.V4 * right.V4, left.V5 * right.V5, left.V6 * right.V6, left.V7 * right.V7, left.V8 * right.V8, left.V9 * right.V9, left.V10 * right.V10, left.V11 * right.V11, left.V12 * right.V12, left.V13 * right.V13, left.V14 * right.V14, left.V15 * right.V15);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Vector16h Abs(Vector16h value)
		{
			return new Vector16h(Functions.Abs(value.V0), Functions.Abs(value.V1), Functions.Abs(value.V2), Functions.Abs(value.V3), Functions.Abs(value.V4), Functions.Abs(value.V5), Functions.Abs(value.V6), Functions.Abs(value.V7), Functions.Abs(value.V8), Functions.Abs(value.V9), Functions.Abs(value.V10), Functions.Abs(value.V11), Functions.Abs(value.V12), Functions.Abs(value.V13), Functions.Abs(value.V14), Functions.Abs(value.V15));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Vector16h Min(Vector16h value1, Vector16h value2)
		{
			return new Vector16h(Functions.Min(value1.V0, value2.V0), Functions.Min(value1.V1, value2.V1), Functions.Min(value1.V2, value2.V2), Functions.Min(value1.V3, value2.V3), Functions.Min(value1.V4, value2.V4), Functions.Min(value1.V5, value2.V5), Functions.Min(value1.V6, value2.V6), Functions.Min(value1.V7, value2.V7), Functions.Min(value1.V8, value2.V8), Functions.Min(value1.V9, value2.V9), Functions.Min(value1.V10, value2.V10), Functions.Min(value1.V11, value2.V11), Functions.Min(value1.V12, value2.V12), Functions.Min(value1.V13, value2.V13), Functions.Min(value1.V14, value2.V14), Functions.Min(value1.V15, value2.V15));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Vector16h Max(Vector16h value1, Vector16h value2)
		{
			return new Vector16h(Functions.Max(value1.V0, value2.V0), Functions.Max(value1.V1, value2.V1), Functions.Max(value1.V2, value2.V2), Functions.Max(value1.V3, value2.V3), Functions.Max(value1.V4, value2.V4), Functions.Max(value1.V5, value2.V5), Functions.Max(value1.V6, value2.V6), Functions.Max(value1.V7, value2.V7), Functions.Max(value1.V8, value2.V8), Functions.Max(value1.V9, value2.V9), Functions.Max(value1.V10, value2.V10), Functions.Max(value1.V11, value2.V11), Functions.Max(value1.V12, value2.V12), Functions.Max(value1.V13, value2.V13), Functions.Max(value1.V14, value2.V14), Functions.Max(value1.V15, value2.V15));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		public static Vector16h Clamp(Vector16h value, Vector16h min, Vector16h max)
		{
			return new Vector16h(Functions.Clamp(value.V0, min.V0, max.V0), Functions.Clamp(value.V1, min.V1, max.V1), Functions.Clamp(value.V2, min.V2, max.V2), Functions.Clamp(value.V3, min.V3, max.V3), Functions.Clamp(value.V4, min.V4, max.V4), Functions.Clamp(value.V5, min.V5, max.V5), Functions.Clamp(value.V6, min.V6, max.V6), Functions.Clamp(value.V7, min.V7, max.V7), Functions.Clamp(value.V8, min.V8, max.V8), Functions.Clamp(value.V9, min.V9, max.V9), Functions.Clamp(value.V10, min.V10, max.V10), Functions.Clamp(value.V11, min.V11, max.V11), Functions.Clamp(value.V12, min.V12, max.V12), Functions.Clamp(value.V13, min.V13, max.V13), Functions.Clamp(value.V14, min.V14, max.V14), Functions.Clamp(value.V15, min.V15, max.V15));
		}
		/// <summary>
		/// Constrains each component to the range 0 to 1.
		/// </summary>
		/// <param name="value">A vector to saturate.</param>
		/// <returns>A vector with each component constrained to the range 0 to 1.</returns>
		public static Vector16h Saturate(Vector16h value)
		{
			return new Vector16h(Functions.Saturate(value.V0), Functions.Saturate(value.V1), Functions.Saturate(value.V2), Functions.Saturate(value.V3), Functions.Saturate(value.V4), Functions.Saturate(value.V5), Functions.Saturate(value.V6), Functions.Saturate(value.V7), Functions.Saturate(value.V8), Functions.Saturate(value.V9), Functions.Saturate(value.V10), Functions.Saturate(value.V11), Functions.Saturate(value.V12), Functions.Saturate(value.V13), Functions.Saturate(value.V14), Functions.Saturate(value.V15));
		}
		/// <summary>
		/// Returns a vector where each component is the smallest integral value that
		/// is greater than or equal to the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The ceiling of value.</returns>
		public static Vector16f Ceiling(Vector16h value)
		{
			return new Vector16f(Functions.Ceiling(value.V0), Functions.Ceiling(value.V1), Functions.Ceiling(value.V2), Functions.Ceiling(value.V3), Functions.Ceiling(value.V4), Functions.Ceiling(value.V5), Functions.Ceiling(value.V6), Functions.Ceiling(value.V7), Functions.Ceiling(value.V8), Functions.Ceiling(value.V9), Functions.Ceiling(value.V10), Functions.Ceiling(value.V11), Functions.Ceiling(value.V12), Functions.Ceiling(value.V13), Functions.Ceiling(value.V14), Functions.Ceiling(value.V15));
		}
		/// <summary>
		/// Returns a vector where each component is the largest integral value that
		/// is less than or equal to the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The floor of value.</returns>
		public static Vector16f Floor(Vector16h value)
		{
			return new Vector16f(Functions.Floor(value.V0), Functions.Floor(value.V1), Functions.Floor(value.V2), Functions.Floor(value.V3), Functions.Floor(value.V4), Functions.Floor(value.V5), Functions.Floor(value.V6), Functions.Floor(value.V7), Functions.Floor(value.V8), Functions.Floor(value.V9), Functions.Floor(value.V10), Functions.Floor(value.V11), Functions.Floor(value.V12), Functions.Floor(value.V13), Functions.Floor(value.V14), Functions.Floor(value.V15));
		}
		/// <summary>
		/// Returns a vector where each component is the integral part of the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The integral of value.</returns>
		public static Vector16f Truncate(Vector16h value)
		{
			return new Vector16f(Functions.Truncate(value.V0), Functions.Truncate(value.V1), Functions.Truncate(value.V2), Functions.Truncate(value.V3), Functions.Truncate(value.V4), Functions.Truncate(value.V5), Functions.Truncate(value.V6), Functions.Truncate(value.V7), Functions.Truncate(value.V8), Functions.Truncate(value.V9), Functions.Truncate(value.V10), Functions.Truncate(value.V11), Functions.Truncate(value.V12), Functions.Truncate(value.V13), Functions.Truncate(value.V14), Functions.Truncate(value.V15));
		}
		/// <summary>
		/// Returns a vector where each component is the fractional part of the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The fractional of value.</returns>
		public static Vector16f Fractional(Vector16h value)
		{
			return new Vector16f(Functions.Fractional(value.V0), Functions.Fractional(value.V1), Functions.Fractional(value.V2), Functions.Fractional(value.V3), Functions.Fractional(value.V4), Functions.Fractional(value.V5), Functions.Fractional(value.V6), Functions.Fractional(value.V7), Functions.Fractional(value.V8), Functions.Fractional(value.V9), Functions.Fractional(value.V10), Functions.Fractional(value.V11), Functions.Fractional(value.V12), Functions.Fractional(value.V13), Functions.Fractional(value.V14), Functions.Fractional(value.V15));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector16f Round(Vector16h value)
		{
			return new Vector16f(Functions.Round(value.V0), Functions.Round(value.V1), Functions.Round(value.V2), Functions.Round(value.V3), Functions.Round(value.V4), Functions.Round(value.V5), Functions.Round(value.V6), Functions.Round(value.V7), Functions.Round(value.V8), Functions.Round(value.V9), Functions.Round(value.V10), Functions.Round(value.V11), Functions.Round(value.V12), Functions.Round(value.V13), Functions.Round(value.V14), Functions.Round(value.V15));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector16f Round(Vector16h value, int digits)
		{
			return new Vector16f(Functions.Round(value.V0, digits), Functions.Round(value.V1, digits), Functions.Round(value.V2, digits), Functions.Round(value.V3, digits), Functions.Round(value.V4, digits), Functions.Round(value.V5, digits), Functions.Round(value.V6, digits), Functions.Round(value.V7, digits), Functions.Round(value.V8, digits), Functions.Round(value.V9, digits), Functions.Round(value.V10, digits), Functions.Round(value.V11, digits), Functions.Round(value.V12, digits), Functions.Round(value.V13, digits), Functions.Round(value.V14, digits), Functions.Round(value.V15, digits));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector16f Round(Vector16h value, MidpointRounding mode)
		{
			return new Vector16f(Functions.Round(value.V0, mode), Functions.Round(value.V1, mode), Functions.Round(value.V2, mode), Functions.Round(value.V3, mode), Functions.Round(value.V4, mode), Functions.Round(value.V5, mode), Functions.Round(value.V6, mode), Functions.Round(value.V7, mode), Functions.Round(value.V8, mode), Functions.Round(value.V9, mode), Functions.Round(value.V10, mode), Functions.Round(value.V11, mode), Functions.Round(value.V12, mode), Functions.Round(value.V13, mode), Functions.Round(value.V14, mode), Functions.Round(value.V15, mode));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector16f Round(Vector16h value, int digits, MidpointRounding mode)
		{
			return new Vector16f(Functions.Round(value.V0, digits, mode), Functions.Round(value.V1, digits, mode), Functions.Round(value.V2, digits, mode), Functions.Round(value.V3, digits, mode), Functions.Round(value.V4, digits, mode), Functions.Round(value.V5, digits, mode), Functions.Round(value.V6, digits, mode), Functions.Round(value.V7, digits, mode), Functions.Round(value.V8, digits, mode), Functions.Round(value.V9, digits, mode), Functions.Round(value.V10, digits, mode), Functions.Round(value.V11, digits, mode), Functions.Round(value.V12, digits, mode), Functions.Round(value.V13, digits, mode), Functions.Round(value.V14, digits, mode), Functions.Round(value.V15, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each component in the vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>A vector with the reciprocal of each of values components.</returns>
		public static Vector16f Reciprocal(Vector16h value)
		{
			return new Vector16f(1 / value.V0, 1 / value.V1, 1 / value.V2, 1 / value.V3, 1 / value.V4, 1 / value.V5, 1 / value.V6, 1 / value.V7, 1 / value.V8, 1 / value.V9, 1 / value.V10, 1 / value.V11, 1 / value.V12, 1 / value.V13, 1 / value.V14, 1 / value.V15);
		}
		#endregion
		#region Coordinate spaces
		#endregion
		#region Barycentric, Reflect, Refract
		/// <summary>
		/// Returns the Cartesian coordinate for one axis of a point that is defined
		/// by a given triangle and two normalized barycentric (areal) coordinates.
		/// </summary>
		/// <param name="value1">The coordinate of vertex 1 of the defining triangle.</param>
		/// <param name="value2">The coordinate of vertex 2 of the defining triangle.</param>
		/// <param name="value3">The coordinate of vertex 3 of the defining triangle.</param>
		/// <param name="amount1">The normalized barycentric (areal) coordinate b2, equal to the weighting
		/// factor for vertex 2, the coordinate of which is specified in value2.</param>
		/// <param name="amount2">The normalized barycentric (areal) coordinate b3, equal to the weighting
		/// factor for vertex 3, the coordinate of which is specified in value3.</param>
		/// <returns>Cartesian coordinate of the specified point.</returns>
		public static Vector16f Barycentric(Vector16h value1, Vector16h value2, Vector16h value3, float amount1, float amount2)
		{
			return ((1 - amount1 - amount2) * value1) + (amount1 * value2) + (amount2 * value3);
		}
		/// <summary>
		/// Returns the reflection of a vector off a surface that has the specified normal.
		/// </summary>
		/// <param name="vector">The source vector.</param>
		/// <param name="normal">Normal of the surface.</param>
		/// <returns>The reflected vector.</returns>
		/// <remarks>Reflect only gives the direction of a reflection off a surface, it does not determine
		/// whether the original vector was close enough to the surface to hit it.</remarks>
		public static Vector16f Reflect(Vector16h vector, Vector16h normal)
		{
			return vector - ((2 * Dot(vector, normal)) * normal);
		}
		/// <summary>
		/// Returns the refraction of a vector off a surface that has the specified normal, and refractive index.
		/// </summary>
		/// <param name="vector">The source vector.</param>
		/// <param name="normal">Normal of the surface.</param>
		/// <param name="index">The refractive index, destination index over source index.</param>
		/// <returns>The refracted vector.</returns>
		/// <remarks>Refract only gives the direction of a refraction off a surface, it does not determine
		/// whether the original vector was close enough to the surface to hit it.</remarks>
		public static Vector16f Refract(Vector16h vector, Vector16h normal, float index)
		{
			var cos1 = Dot(vector, normal);
			var radicand = 1 - (index * index) * (1 - (cos1 * cos1));
			if (radicand < 0)
			{
				return Vector16f.Zero;
			}
			return (index * vector) + ((Functions.Sqrt(radicand) - index * cos1) * normal);
		}
		#endregion
		#region Interpolation
		/// <summary>
		/// Performs a linear interpolation between two values.
		/// </summary>
		/// <param name="value1">First value.</param>
		/// <param name="value2">Second value.</param>
		/// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="value2"/>.</param>
		/// <returns>The linear interpolation of the two values.</returns>
		public static Vector16f Lerp(Vector16h value1, Vector16h value2, float amount)
		{
			return (1 - amount) * value1 + amount * value2;
		}
		#endregion
	}
}
