using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a 16 component vector of bytes.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector16b: IEquatable<Vector16b>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector16b"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector16b Zero = new Vector16b(0);
		/// <summary>
		/// Returns a new <see cref="Vector16b"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector16b One = new Vector16b(1);
		#endregion
		#region Fields
		/// <summary>
		/// The first component of the vector.
		/// </summary>
		public readonly byte V0;
		/// <summary>
		/// The second component of the vector.
		/// </summary>
		public readonly byte V1;
		/// <summary>
		/// The third component of the vector.
		/// </summary>
		public readonly byte V2;
		/// <summary>
		/// The fourth component of the vector.
		/// </summary>
		public readonly byte V3;
		/// <summary>
		/// The fifth component of the vector.
		/// </summary>
		public readonly byte V4;
		/// <summary>
		/// The sixth component of the vector.
		/// </summary>
		public readonly byte V5;
		/// <summary>
		/// The seventh component of the vector.
		/// </summary>
		public readonly byte V6;
		/// <summary>
		/// The eighth component of the vector.
		/// </summary>
		public readonly byte V7;
		/// <summary>
		/// The nineth component of the vector.
		/// </summary>
		public readonly byte V8;
		/// <summary>
		/// The tenth component of the vector.
		/// </summary>
		public readonly byte V9;
		/// <summary>
		/// The eleventh component of the vector.
		/// </summary>
		public readonly byte V10;
		/// <summary>
		/// The twelfth component of the vector.
		/// </summary>
		public readonly byte V11;
		/// <summary>
		/// The thirteenth component of the vector.
		/// </summary>
		public readonly byte V12;
		/// <summary>
		/// The fourteenth component of the vector.
		/// </summary>
		public readonly byte V13;
		/// <summary>
		/// The fifeteenth component of the vector.
		/// </summary>
		public readonly byte V14;
		/// <summary>
		/// The sixteenth component of the vector.
		/// </summary>
		public readonly byte V15;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public byte this[int index]
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
						throw new IndexOutOfRangeException("Indices for Vector16b run from 0 to 15, inclusive.");
				}
			}
		}
		public byte[] ToArray()
		{
			return new byte[]
			{
				V0, V1, V2, V3, V4, V5, V6, V7, V8, V9, V10, V11, V12, V13, V14, V15
			};
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector16b"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector16b(byte value)
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
		/// Initializes a new instance of the <see cref="Vector16b"/> using the specified vector and values.
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
		public Vector16b(Vector2b value, byte v2, byte v3, byte v4, byte v5, byte v6, byte v7, byte v8, byte v9, byte v10, byte v11, byte v12, byte v13, byte v14, byte v15, byte v16, byte v17)
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
		/// Initializes a new instance of the <see cref="Vector16b"/> using the specified vector and values.
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
		public Vector16b(Vector3b value, byte v3, byte v4, byte v5, byte v6, byte v7, byte v8, byte v9, byte v10, byte v11, byte v12, byte v13, byte v14, byte v15, byte v16, byte v17, byte v18)
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
		/// Initializes a new instance of the <see cref="Vector16b"/> using the specified vector and values.
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
		public Vector16b(Vector4b value, byte v4, byte v5, byte v6, byte v7, byte v8, byte v9, byte v10, byte v11, byte v12, byte v13, byte v14, byte v15, byte v16, byte v17, byte v18, byte v19)
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
		/// Initializes a new instance of the <see cref="Vector16b"/> using the specified vector and values.
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
		public Vector16b(Vector8b value, byte v8, byte v9, byte v10, byte v11, byte v12, byte v13, byte v14, byte v15, byte v16, byte v17, byte v18, byte v19, byte v20, byte v21, byte v22, byte v23)
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
		/// Initializes a new instance of the <see cref="Vector16b"/> using the specified values.
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
		public Vector16b(byte v0, byte v1, byte v2, byte v3, byte v4, byte v5, byte v6, byte v7, byte v8, byte v9, byte v10, byte v11, byte v12, byte v13, byte v14, byte v15)
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
		/// Initializes a new instance of the <see cref="Vector16b"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector16b(byte[] array)
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
		/// Initializes a new instance of the <see cref="Vector16b"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector16b(byte[] array, int offset)
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
		public static Vector16i operator +(Vector16b value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector16i operator -(Vector16b value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector16i operator +(Vector16b left, Vector16b right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector16i operator -(Vector16b left, Vector16b right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector16i operator *(Vector16b left, int right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector16i operator *(int left, Vector16b right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector16i operator /(Vector16b left, int right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector16d value to a Vector16b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16b.</param>
		/// <returns>A Vector16b that has all components equal to value.</returns>
		public static explicit operator Vector16b(Vector16d value)
		{
			return new Vector16b((byte)value.V0, (byte)value.V1, (byte)value.V2, (byte)value.V3, (byte)value.V4, (byte)value.V5, (byte)value.V6, (byte)value.V7, (byte)value.V8, (byte)value.V9, (byte)value.V10, (byte)value.V11, (byte)value.V12, (byte)value.V13, (byte)value.V14, (byte)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16f value to a Vector16b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16b.</param>
		/// <returns>A Vector16b that has all components equal to value.</returns>
		public static explicit operator Vector16b(Vector16f value)
		{
			return new Vector16b((byte)value.V0, (byte)value.V1, (byte)value.V2, (byte)value.V3, (byte)value.V4, (byte)value.V5, (byte)value.V6, (byte)value.V7, (byte)value.V8, (byte)value.V9, (byte)value.V10, (byte)value.V11, (byte)value.V12, (byte)value.V13, (byte)value.V14, (byte)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16h value to a Vector16b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16b.</param>
		/// <returns>A Vector16b that has all components equal to value.</returns>
		public static explicit operator Vector16b(Vector16h value)
		{
			return new Vector16b((byte)value.V0, (byte)value.V1, (byte)value.V2, (byte)value.V3, (byte)value.V4, (byte)value.V5, (byte)value.V6, (byte)value.V7, (byte)value.V8, (byte)value.V9, (byte)value.V10, (byte)value.V11, (byte)value.V12, (byte)value.V13, (byte)value.V14, (byte)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16ul value to a Vector16b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16b.</param>
		/// <returns>A Vector16b that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector16b(Vector16ul value)
		{
			return new Vector16b((byte)value.V0, (byte)value.V1, (byte)value.V2, (byte)value.V3, (byte)value.V4, (byte)value.V5, (byte)value.V6, (byte)value.V7, (byte)value.V8, (byte)value.V9, (byte)value.V10, (byte)value.V11, (byte)value.V12, (byte)value.V13, (byte)value.V14, (byte)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16l value to a Vector16b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16b.</param>
		/// <returns>A Vector16b that has all components equal to value.</returns>
		public static explicit operator Vector16b(Vector16l value)
		{
			return new Vector16b((byte)value.V0, (byte)value.V1, (byte)value.V2, (byte)value.V3, (byte)value.V4, (byte)value.V5, (byte)value.V6, (byte)value.V7, (byte)value.V8, (byte)value.V9, (byte)value.V10, (byte)value.V11, (byte)value.V12, (byte)value.V13, (byte)value.V14, (byte)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16ui value to a Vector16b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16b.</param>
		/// <returns>A Vector16b that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector16b(Vector16ui value)
		{
			return new Vector16b((byte)value.V0, (byte)value.V1, (byte)value.V2, (byte)value.V3, (byte)value.V4, (byte)value.V5, (byte)value.V6, (byte)value.V7, (byte)value.V8, (byte)value.V9, (byte)value.V10, (byte)value.V11, (byte)value.V12, (byte)value.V13, (byte)value.V14, (byte)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16i value to a Vector16b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16b.</param>
		/// <returns>A Vector16b that has all components equal to value.</returns>
		public static explicit operator Vector16b(Vector16i value)
		{
			return new Vector16b((byte)value.V0, (byte)value.V1, (byte)value.V2, (byte)value.V3, (byte)value.V4, (byte)value.V5, (byte)value.V6, (byte)value.V7, (byte)value.V8, (byte)value.V9, (byte)value.V10, (byte)value.V11, (byte)value.V12, (byte)value.V13, (byte)value.V14, (byte)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16us value to a Vector16b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16b.</param>
		/// <returns>A Vector16b that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector16b(Vector16us value)
		{
			return new Vector16b((byte)value.V0, (byte)value.V1, (byte)value.V2, (byte)value.V3, (byte)value.V4, (byte)value.V5, (byte)value.V6, (byte)value.V7, (byte)value.V8, (byte)value.V9, (byte)value.V10, (byte)value.V11, (byte)value.V12, (byte)value.V13, (byte)value.V14, (byte)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16s value to a Vector16b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16b.</param>
		/// <returns>A Vector16b that has all components equal to value.</returns>
		public static explicit operator Vector16b(Vector16s value)
		{
			return new Vector16b((byte)value.V0, (byte)value.V1, (byte)value.V2, (byte)value.V3, (byte)value.V4, (byte)value.V5, (byte)value.V6, (byte)value.V7, (byte)value.V8, (byte)value.V9, (byte)value.V10, (byte)value.V11, (byte)value.V12, (byte)value.V13, (byte)value.V14, (byte)value.V15);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector16sb value to a Vector16b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector16b.</param>
		/// <returns>A Vector16b that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector16b(Vector16sb value)
		{
			return new Vector16b((byte)value.V0, (byte)value.V1, (byte)value.V2, (byte)value.V3, (byte)value.V4, (byte)value.V5, (byte)value.V6, (byte)value.V7, (byte)value.V8, (byte)value.V9, (byte)value.V10, (byte)value.V11, (byte)value.V12, (byte)value.V13, (byte)value.V14, (byte)value.V15);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector16b"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector16b"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector16b"/> object, and its value
		/// is equal to the current <see cref="Vector16b"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector16b) { return Equals((Vector16b)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector16b other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector16b left, Vector16b right)
		{
			return left.V0 == right.V0 & left.V1 == right.V1 & left.V2 == right.V2 & left.V3 == right.V3 & left.V4 == right.V4 & left.V5 == right.V5 & left.V6 == right.V6 & left.V7 == right.V7 & left.V8 == right.V8 & left.V9 == right.V9 & left.V10 == right.V10 & left.V11 == right.V11 & left.V12 == right.V12 & left.V13 == right.V13 & left.V14 == right.V14 & left.V15 == right.V15;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector16b left, Vector16b right)
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
		/// Writes the given <see cref="Vector16b"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector16b vector)
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
		/// Reads a <see cref="Vector16b"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector16b ReadVector16b(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector16b(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
		}
		#endregion
		#region Pack
		public static long Pack(int v0Bits, int v1Bits, int v2Bits, int v3Bits, int v4Bits, int v5Bits, int v6Bits, int v7Bits, int v8Bits, int v9Bits, int v10Bits, int v11Bits, int v12Bits, int v13Bits, int v14Bits, int v15Bits, Vector16b vector)
		{
			Contract.Requires(0 <= v0Bits && v0Bits <= 8, "v0Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v1Bits && v1Bits <= 8, "v1Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v2Bits && v2Bits <= 8, "v2Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v3Bits && v3Bits <= 8, "v3Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v4Bits && v4Bits <= 8, "v4Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v5Bits && v5Bits <= 8, "v5Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v6Bits && v6Bits <= 8, "v6Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v7Bits && v7Bits <= 8, "v7Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v8Bits && v8Bits <= 8, "v8Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v9Bits && v9Bits <= 8, "v9Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v10Bits && v10Bits <= 8, "v10Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v11Bits && v11Bits <= 8, "v11Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v12Bits && v12Bits <= 8, "v12Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v13Bits && v13Bits <= 8, "v13Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v14Bits && v14Bits <= 8, "v14Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v15Bits && v15Bits <= 8, "v15Bits must be between 0 and 8 inclusive.");
			Contract.Requires(v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits + v10Bits + v11Bits + v12Bits + v13Bits + v14Bits + v15Bits <= 64);
			ulong v0 = (ulong)(vector.V0) >> (64 - v0Bits);
			ulong v1 = (ulong)(vector.V1) >> (64 - v1Bits);
			v1 <<= v0Bits;
			ulong v2 = (ulong)(vector.V2) >> (64 - v2Bits);
			v2 <<= v0Bits + v1Bits;
			ulong v3 = (ulong)(vector.V3) >> (64 - v3Bits);
			v3 <<= v0Bits + v1Bits + v2Bits;
			ulong v4 = (ulong)(vector.V4) >> (64 - v4Bits);
			v4 <<= v0Bits + v1Bits + v2Bits + v3Bits;
			ulong v5 = (ulong)(vector.V5) >> (64 - v5Bits);
			v5 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits;
			ulong v6 = (ulong)(vector.V6) >> (64 - v6Bits);
			v6 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits;
			ulong v7 = (ulong)(vector.V7) >> (64 - v7Bits);
			v7 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits;
			ulong v8 = (ulong)(vector.V8) >> (64 - v8Bits);
			v8 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits;
			ulong v9 = (ulong)(vector.V9) >> (64 - v9Bits);
			v9 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits;
			ulong v10 = (ulong)(vector.V10) >> (64 - v10Bits);
			v10 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits;
			ulong v11 = (ulong)(vector.V11) >> (64 - v11Bits);
			v11 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits + v10Bits;
			ulong v12 = (ulong)(vector.V12) >> (64 - v12Bits);
			v12 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits + v10Bits + v11Bits;
			ulong v13 = (ulong)(vector.V13) >> (64 - v13Bits);
			v13 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits + v10Bits + v11Bits + v12Bits;
			ulong v14 = (ulong)(vector.V14) >> (64 - v14Bits);
			v14 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits + v10Bits + v11Bits + v12Bits + v13Bits;
			ulong v15 = (ulong)(vector.V15) >> (64 - v15Bits);
			v15 <<= v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits + v10Bits + v11Bits + v12Bits + v13Bits + v14Bits;
			return (long)(v0 | v1 | v2 | v3 | v4 | v5 | v6 | v7 | v8 | v9 | v10 | v11 | v12 | v13 | v14 | v15);
		}
		public static Vector16b Unpack(int v0Bits, int v1Bits, int v2Bits, int v3Bits, int v4Bits, int v5Bits, int v6Bits, int v7Bits, int v8Bits, int v9Bits, int v10Bits, int v11Bits, int v12Bits, int v13Bits, int v14Bits, int v15Bits, byte bits)
		{
			Contract.Requires(0 <= v0Bits && v0Bits <= 8, "v0Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v1Bits && v1Bits <= 8, "v1Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v2Bits && v2Bits <= 8, "v2Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v3Bits && v3Bits <= 8, "v3Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v4Bits && v4Bits <= 8, "v4Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v5Bits && v5Bits <= 8, "v5Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v6Bits && v6Bits <= 8, "v6Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v7Bits && v7Bits <= 8, "v7Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v8Bits && v8Bits <= 8, "v8Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v9Bits && v9Bits <= 8, "v9Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v10Bits && v10Bits <= 8, "v10Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v11Bits && v11Bits <= 8, "v11Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v12Bits && v12Bits <= 8, "v12Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v13Bits && v13Bits <= 8, "v13Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v14Bits && v14Bits <= 8, "v14Bits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= v15Bits && v15Bits <= 8, "v15Bits must be between 0 and 8 inclusive.");
			Contract.Requires(v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits + v10Bits + v11Bits + v12Bits + v13Bits + v14Bits + v15Bits <= 64);
			ulong v0 = (ulong)(bits);
			v0 &= ((1UL << v0Bits) - 1);
			ulong v1 = (ulong)(bits) >> (v0Bits);
			v1 &= ((1UL << v1Bits) - 1);
			ulong v2 = (ulong)(bits) >> (v0Bits + v1Bits);
			v2 &= ((1UL << v2Bits) - 1);
			ulong v3 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits);
			v3 &= ((1UL << v3Bits) - 1);
			ulong v4 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits);
			v4 &= ((1UL << v4Bits) - 1);
			ulong v5 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits);
			v5 &= ((1UL << v5Bits) - 1);
			ulong v6 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits);
			v6 &= ((1UL << v6Bits) - 1);
			ulong v7 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits);
			v7 &= ((1UL << v7Bits) - 1);
			ulong v8 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits);
			v8 &= ((1UL << v8Bits) - 1);
			ulong v9 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits);
			v9 &= ((1UL << v9Bits) - 1);
			ulong v10 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits);
			v10 &= ((1UL << v10Bits) - 1);
			ulong v11 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits + v10Bits);
			v11 &= ((1UL << v11Bits) - 1);
			ulong v12 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits + v10Bits + v11Bits);
			v12 &= ((1UL << v12Bits) - 1);
			ulong v13 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits + v10Bits + v11Bits + v12Bits);
			v13 &= ((1UL << v13Bits) - 1);
			ulong v14 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits + v10Bits + v11Bits + v12Bits + v13Bits);
			v14 &= ((1UL << v14Bits) - 1);
			ulong v15 = (ulong)(bits) >> (v0Bits + v1Bits + v2Bits + v3Bits + v4Bits + v5Bits + v6Bits + v7Bits + v8Bits + v9Bits + v10Bits + v11Bits + v12Bits + v13Bits + v14Bits);
			v15 &= ((1UL << v15Bits) - 1);
			return new Vector16b((byte)v0, (byte)v1, (byte)v2, (byte)v3, (byte)v4, (byte)v5, (byte)v6, (byte)v7, (byte)v8, (byte)v9, (byte)v10, (byte)v11, (byte)v12, (byte)v13, (byte)v14, (byte)v15);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector16i Negative(Vector16b value)
		{
			return new Vector16i(-value.V0, -value.V1, -value.V2, -value.V3, -value.V4, -value.V5, -value.V6, -value.V7, -value.V8, -value.V9, -value.V10, -value.V11, -value.V12, -value.V13, -value.V14, -value.V15);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector16i Add(Vector16b left, Vector16b right)
		{
			return new Vector16i(left.V0 + right.V0, left.V1 + right.V1, left.V2 + right.V2, left.V3 + right.V3, left.V4 + right.V4, left.V5 + right.V5, left.V6 + right.V6, left.V7 + right.V7, left.V8 + right.V8, left.V9 + right.V9, left.V10 + right.V10, left.V11 + right.V11, left.V12 + right.V12, left.V13 + right.V13, left.V14 + right.V14, left.V15 + right.V15);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector16i Subtract(Vector16b left, Vector16b right)
		{
			return new Vector16i(left.V0 - right.V0, left.V1 - right.V1, left.V2 - right.V2, left.V3 - right.V3, left.V4 - right.V4, left.V5 - right.V5, left.V6 - right.V6, left.V7 - right.V7, left.V8 - right.V8, left.V9 - right.V9, left.V10 - right.V10, left.V11 - right.V11, left.V12 - right.V12, left.V13 - right.V13, left.V14 - right.V14, left.V15 - right.V15);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector16i Multiply(Vector16b vector, int scalar)
		{
			return new Vector16i(vector.V0 * scalar, vector.V1 * scalar, vector.V2 * scalar, vector.V3 * scalar, vector.V4 * scalar, vector.V5 * scalar, vector.V6 * scalar, vector.V7 * scalar, vector.V8 * scalar, vector.V9 * scalar, vector.V10 * scalar, vector.V11 * scalar, vector.V12 * scalar, vector.V13 * scalar, vector.V14 * scalar, vector.V15 * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector16i Divide(Vector16b vector, int scalar)
		{
			return new Vector16i(vector.V0 / scalar, vector.V1 / scalar, vector.V2 / scalar, vector.V3 / scalar, vector.V4 / scalar, vector.V5 / scalar, vector.V6 / scalar, vector.V7 / scalar, vector.V8 / scalar, vector.V9 / scalar, vector.V10 / scalar, vector.V11 / scalar, vector.V12 / scalar, vector.V13 / scalar, vector.V14 / scalar, vector.V15 / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Vector16b left, Vector16b right)
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
		public static int Dot(Vector16b left, Vector16b right)
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
		public static bool All(Vector16b value)
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
		public static bool All(Vector16b value, Predicate<byte> predicate)
		{
			return predicate(value.V0) && predicate(value.V1) && predicate(value.V2) && predicate(value.V3) && predicate(value.V4) && predicate(value.V5) && predicate(value.V6) && predicate(value.V7) && predicate(value.V8) && predicate(value.V9) && predicate(value.V10) && predicate(value.V11) && predicate(value.V12) && predicate(value.V13) && predicate(value.V14) && predicate(value.V15);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Vector16b value)
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
		public static bool Any(Vector16b value, Predicate<byte> predicate)
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
		public static int AbsoluteSquared(Vector16b value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		public static float Absolute(Vector16b value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		public static Vector16f Normalize(Vector16b value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Vector16b.Zero;
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
		public static Vector16d Transform(Vector16b value, Func<byte, double> transformer)
		{
			return new Vector16d(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16f Transform(Vector16b value, Func<byte, float> transformer)
		{
			return new Vector16f(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16h Transform(Vector16b value, Func<byte, Half> transformer)
		{
			return new Vector16h(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16ul Transform(Vector16b value, Func<byte, ulong> transformer)
		{
			return new Vector16ul(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16l Transform(Vector16b value, Func<byte, long> transformer)
		{
			return new Vector16l(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16ui Transform(Vector16b value, Func<byte, uint> transformer)
		{
			return new Vector16ui(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16i Transform(Vector16b value, Func<byte, int> transformer)
		{
			return new Vector16i(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16us Transform(Vector16b value, Func<byte, ushort> transformer)
		{
			return new Vector16us(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16s Transform(Vector16b value, Func<byte, short> transformer)
		{
			return new Vector16s(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16b Transform(Vector16b value, Func<byte, byte> transformer)
		{
			return new Vector16b(transformer(value.V0), transformer(value.V1), transformer(value.V2), transformer(value.V3), transformer(value.V4), transformer(value.V5), transformer(value.V6), transformer(value.V7), transformer(value.V8), transformer(value.V9), transformer(value.V10), transformer(value.V11), transformer(value.V12), transformer(value.V13), transformer(value.V14), transformer(value.V15));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector16sb Transform(Vector16b value, Func<byte, sbyte> transformer)
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
		public static Vector16i Modulate(Vector16b left, Vector16b right)
		{
			return new Vector16i(left.V0 * right.V0, left.V1 * right.V1, left.V2 * right.V2, left.V3 * right.V3, left.V4 * right.V4, left.V5 * right.V5, left.V6 * right.V6, left.V7 * right.V7, left.V8 * right.V8, left.V9 * right.V9, left.V10 * right.V10, left.V11 * right.V11, left.V12 * right.V12, left.V13 * right.V13, left.V14 * right.V14, left.V15 * right.V15);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Vector16b Abs(Vector16b value)
		{
			return new Vector16b(Functions.Abs(value.V0), Functions.Abs(value.V1), Functions.Abs(value.V2), Functions.Abs(value.V3), Functions.Abs(value.V4), Functions.Abs(value.V5), Functions.Abs(value.V6), Functions.Abs(value.V7), Functions.Abs(value.V8), Functions.Abs(value.V9), Functions.Abs(value.V10), Functions.Abs(value.V11), Functions.Abs(value.V12), Functions.Abs(value.V13), Functions.Abs(value.V14), Functions.Abs(value.V15));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Vector16b Min(Vector16b value1, Vector16b value2)
		{
			return new Vector16b(Functions.Min(value1.V0, value2.V0), Functions.Min(value1.V1, value2.V1), Functions.Min(value1.V2, value2.V2), Functions.Min(value1.V3, value2.V3), Functions.Min(value1.V4, value2.V4), Functions.Min(value1.V5, value2.V5), Functions.Min(value1.V6, value2.V6), Functions.Min(value1.V7, value2.V7), Functions.Min(value1.V8, value2.V8), Functions.Min(value1.V9, value2.V9), Functions.Min(value1.V10, value2.V10), Functions.Min(value1.V11, value2.V11), Functions.Min(value1.V12, value2.V12), Functions.Min(value1.V13, value2.V13), Functions.Min(value1.V14, value2.V14), Functions.Min(value1.V15, value2.V15));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Vector16b Max(Vector16b value1, Vector16b value2)
		{
			return new Vector16b(Functions.Max(value1.V0, value2.V0), Functions.Max(value1.V1, value2.V1), Functions.Max(value1.V2, value2.V2), Functions.Max(value1.V3, value2.V3), Functions.Max(value1.V4, value2.V4), Functions.Max(value1.V5, value2.V5), Functions.Max(value1.V6, value2.V6), Functions.Max(value1.V7, value2.V7), Functions.Max(value1.V8, value2.V8), Functions.Max(value1.V9, value2.V9), Functions.Max(value1.V10, value2.V10), Functions.Max(value1.V11, value2.V11), Functions.Max(value1.V12, value2.V12), Functions.Max(value1.V13, value2.V13), Functions.Max(value1.V14, value2.V14), Functions.Max(value1.V15, value2.V15));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		public static Vector16b Clamp(Vector16b value, Vector16b min, Vector16b max)
		{
			return new Vector16b(Functions.Clamp(value.V0, min.V0, max.V0), Functions.Clamp(value.V1, min.V1, max.V1), Functions.Clamp(value.V2, min.V2, max.V2), Functions.Clamp(value.V3, min.V3, max.V3), Functions.Clamp(value.V4, min.V4, max.V4), Functions.Clamp(value.V5, min.V5, max.V5), Functions.Clamp(value.V6, min.V6, max.V6), Functions.Clamp(value.V7, min.V7, max.V7), Functions.Clamp(value.V8, min.V8, max.V8), Functions.Clamp(value.V9, min.V9, max.V9), Functions.Clamp(value.V10, min.V10, max.V10), Functions.Clamp(value.V11, min.V11, max.V11), Functions.Clamp(value.V12, min.V12, max.V12), Functions.Clamp(value.V13, min.V13, max.V13), Functions.Clamp(value.V14, min.V14, max.V14), Functions.Clamp(value.V15, min.V15, max.V15));
		}
		#endregion
		#region Coordinate spaces
		#endregion
	}
}
