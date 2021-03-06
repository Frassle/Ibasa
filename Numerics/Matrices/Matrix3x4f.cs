using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics
{
	/// <summary>
	/// Represents a 3 by 4 matrix of floats.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix3x4f: IEquatable<Matrix3x4f>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Matrix3x4f"/> with all of its elements equal to zero.
		/// </summary>
		public static readonly Matrix3x4f Zero = new Matrix3x4f(0);
		/// <summary>
		/// Returns a new <see cref="Matrix3x4f"/> with all of its elements equal to one.
		/// </summary>
		public static readonly Matrix3x4f One = new Matrix3x4f(1);
		#endregion
		#region Fields
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and first column.
		/// </summary>
		public readonly float M11;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and first column.
		/// </summary>
		public readonly float M21;
		/// <summary>
		/// Gets the element of the matrix that exists in the third row and first column.
		/// </summary>
		public readonly float M31;
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and second column.
		/// </summary>
		public readonly float M12;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and second column.
		/// </summary>
		public readonly float M22;
		/// <summary>
		/// Gets the element of the matrix that exists in the third row and second column.
		/// </summary>
		public readonly float M32;
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and third column.
		/// </summary>
		public readonly float M13;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and third column.
		/// </summary>
		public readonly float M23;
		/// <summary>
		/// Gets the element of the matrix that exists in the third row and third column.
		/// </summary>
		public readonly float M33;
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and fourth column.
		/// </summary>
		public readonly float M14;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and fourth column.
		/// </summary>
		public readonly float M24;
		/// <summary>
		/// Gets the element of the matrix that exists in the third row and fourth column.
		/// </summary>
		public readonly float M34;
		#endregion
		#region Properties
		public float this[int row, int column]
		{
			get
			{
				if (row < 0 || row > 2)
					throw new ArgumentOutOfRangeException("row", "Rows for Matrix3x4f run from 0 to 2, inclusive.");
				if (column < 0 || column > 3)
					throw new ArgumentOutOfRangeException("column", "Columns for Matrix3x4f run from 0 to 3, inclusive.");
				int index = row + column * 3;
				return this[index];
			}
		}
		public float this[int index]
		{
			get
			{
				if (index < 0 || index > 11)
					throw new ArgumentOutOfRangeException("index", "Indices for Matrix3x4f run from 0 to 11, inclusive.");
				switch (index)
				{
					case 0: return M11;
					case 1: return M21;
					case 2: return M31;
					case 3: return M12;
					case 4: return M22;
					case 5: return M32;
					case 6: return M13;
					case 7: return M23;
					case 8: return M33;
					case 9: return M14;
					case 10: return M24;
					case 11: return M34;
				}
				return 0;
			}
		}
		public Vector4f GetRow(int row)
		{
			if (row < 0 || row > 2)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix3x4f run from 0 to 2, inclusive.");
			switch (row)
			{
				case 0:
					return new Vector4f(M11, M12, M13, M14);
				case 1:
					return new Vector4f(M21, M22, M23, M24);
				case 2:
					return new Vector4f(M31, M32, M33, M34);
			}
			return Vector4f.Zero;
		}
		public Vector3f GetColumn(int column)
		{
			if (column < 0 || column > 3)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix3x4f run from 0 to 3, inclusive.");
			switch (column)
			{
				case 0:
					return new Vector3f(M11, M21, M31);
				case 1:
					return new Vector3f(M12, M22, M32);
				case 2:
					return new Vector3f(M13, M23, M33);
				case 3:
					return new Vector3f(M14, M24, M34);
			}
			return Vector3f.Zero;
		}
		public float[] ToArray()
		{
			return new float[]
			{
				M11, M21, M31, M12, M22, M32, M13, M23, M33, M14, M24, M34
			};
		}
		#endregion
		#region Constructors
		public Matrix3x4f(float value)
		{
			M11 = value;
			M12 = value;
			M13 = value;
			M14 = value;
			M21 = value;
			M22 = value;
			M23 = value;
			M24 = value;
			M31 = value;
			M32 = value;
			M33 = value;
			M34 = value;
		}
		public Matrix3x4f(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34)
		{
			M11 = m11;
			M21 = m21;
			M31 = m31;
			M12 = m12;
			M22 = m22;
			M32 = m32;
			M13 = m13;
			M23 = m23;
			M33 = m33;
			M14 = m14;
			M24 = m24;
			M34 = m34;
		}
		#endregion
		#region Operations
		public static Matrix3x4f operator +(Matrix3x4f value)
		{
			return value;
		}
		public static Matrix3x4f operator -(Matrix3x4f value)
		{
			return Matrix.Negate(value);
		}
		public static Matrix3x4f operator +(Matrix3x4f left, Matrix3x4f right)
		{
			return Matrix.Add(left, right);
		}
		public static Matrix3x4f operator -(Matrix3x4f left, Matrix3x4f right)
		{
			return Matrix.Subtract(left, right);
		}
		public static Matrix3x4f operator *(Matrix3x4f matrix, float scalar)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix3x4f operator *(float scalar, Matrix3x4f matrix)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix3x2f operator *(Matrix3x4f left, Matrix4x2f right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix3x3f operator *(Matrix3x4f left, Matrix4x3f right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix3x4f operator *(Matrix3x4f left, Matrix4x4f right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix3x4f operator /(Matrix3x4f matrix, float scalar)
		{
			return Matrix.Divide(matrix, scalar);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Matrix3x4d value to a Matrix3x4f.
		/// </summary>
		/// <param name="value">The value to convert to a Matrix3x4f.</param>
		/// <returns>A Matrix3x4f that has all components equal to value.</returns>
		public static explicit operator Matrix3x4f(Matrix3x4d value)
		{
			return new Matrix3x4f((float)value.M11, (float)value.M21, (float)value.M31, (float)value.M12, (float)value.M22, (float)value.M32, (float)value.M13, (float)value.M23, (float)value.M33, (float)value.M14, (float)value.M24, (float)value.M34);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Matrix3x4f"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return M11.GetHashCode() + M21.GetHashCode() + M31.GetHashCode() + M12.GetHashCode() + M22.GetHashCode() + M32.GetHashCode() + M13.GetHashCode() + M23.GetHashCode() + M33.GetHashCode() + M14.GetHashCode() + M24.GetHashCode() + M34.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Matrix3x4f"/> object or a type capable
		/// of implicit conversion to a <see cref="Matrix3x4f"/> object, and its value
		/// is equal to the current <see cref="Matrix3x4f"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix3x4f) { return Equals((Matrix3x4f)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// matrix have the same value.
		/// </summary>
		/// <param name="other">The matrix to compare.</param>
		/// <returns>true if this matrix and value have the same value; otherwise, false.</returns>
		public bool Equals(Matrix3x4f other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Matrix3x4f left, Matrix3x4f right)
		{
			return left == right;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Matrix3x4f left, Matrix3x4f right)
		{
			return left.M11 == right.M11 & left.M21 == right.M21 & left.M31 == right.M31 & left.M12 == right.M12 & left.M22 == right.M22 & left.M32 == right.M32 & left.M13 == right.M13 & left.M23 == right.M23 & left.M33 == right.M33 & left.M14 == right.M14 & left.M24 == right.M24 & left.M34 == right.M34;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are not equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Matrix3x4f left, Matrix3x4f right)
		{
			return left.M11 != right.M11 | left.M21 != right.M21 | left.M31 != right.M31 | left.M12 != right.M12 | left.M22 != right.M22 | left.M32 != right.M32 | left.M13 != right.M13 | left.M23 != right.M23 | left.M33 != right.M33 | left.M14 != right.M14 | left.M24 != right.M24 | left.M34 != right.M34;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current matrix to its equivalent string
		/// representation in Cartesian form.
		/// </summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current matrix to its equivalent string
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
		/// Converts the value of the current matrix to its equivalent string
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
		/// Converts the value of the current matrix to its equivalent string
		/// representation in Cartesian form by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, IFormatProvider provider)
		{
			return String.Format("[({0}, {1}, {2}, {3})({4}, {5}, {6}, {7})({8}, {9}, {10}, {11})]", M11.ToString(format, provider), M21.ToString(format, provider), M31.ToString(format, provider), M12.ToString(format, provider), M22.ToString(format, provider), M32.ToString(format, provider), M13.ToString(format, provider), M23.ToString(format, provider), M33.ToString(format, provider), M14.ToString(format, provider), M24.ToString(format, provider), M34.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for matrix functions.
	/// </summary>
	public static partial class Matrix
	{
		#region Operations
		public static Matrix3x4f Negate(Matrix3x4f value)
		{
			return new Matrix3x4f(-value.M11, -value.M21, -value.M31, -value.M12, -value.M22, -value.M32, -value.M13, -value.M23, -value.M33, -value.M14, -value.M24, -value.M34);
		}
		public static Matrix3x4f Add(Matrix3x4f left, Matrix3x4f right)
		{
			return new Matrix3x4f(left.M11 + right.M11, left.M21 + right.M21, left.M31 + right.M31, left.M12 + right.M12, left.M22 + right.M22, left.M32 + right.M32, left.M13 + right.M13, left.M23 + right.M23, left.M33 + right.M33, left.M14 + right.M14, left.M24 + right.M24, left.M34 + right.M34);
		}
		public static Matrix3x4f Subtract(Matrix3x4f left, Matrix3x4f right)
		{
			return new Matrix3x4f(left.M11 - right.M11, left.M21 - right.M21, left.M31 - right.M31, left.M12 - right.M12, left.M22 - right.M22, left.M32 - right.M32, left.M13 - right.M13, left.M23 - right.M23, left.M33 - right.M33, left.M14 - right.M14, left.M24 - right.M24, left.M34 - right.M34);
		}
		public static Matrix3x2f Multiply(Matrix3x4f left, Matrix4x2f right)
		{
			return new Matrix3x2f(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41, left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42, left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42);
		}
		public static Matrix3x3f Multiply(Matrix3x4f left, Matrix4x3f right)
		{
			return new Matrix3x3f(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41, left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42, left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42, left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43, left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43, left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43);
		}
		public static Matrix3x4f Multiply(Matrix3x4f left, Matrix4x4f right)
		{
			return new Matrix3x4f(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41, left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42, left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42, left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43, left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43, left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43, left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34 + left.M14 * right.M44, left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34 + left.M24 * right.M44, left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34 + left.M34 * right.M44);
		}
		public static Matrix3x4f Multiply(Matrix3x4f matrix, float scalar)
		{
			return new Matrix3x4f(matrix.M11 * scalar, matrix.M21 * scalar, matrix.M31 * scalar, matrix.M12 * scalar, matrix.M22 * scalar, matrix.M32 * scalar, matrix.M13 * scalar, matrix.M23 * scalar, matrix.M33 * scalar, matrix.M14 * scalar, matrix.M24 * scalar, matrix.M34 * scalar);
		}
		public static Matrix3x4f Divide(Matrix3x4f matrix, float scalar)
		{
			return new Matrix3x4f(matrix.M11 / scalar, matrix.M21 / scalar, matrix.M31 / scalar, matrix.M12 / scalar, matrix.M22 / scalar, matrix.M32 / scalar, matrix.M13 / scalar, matrix.M23 / scalar, matrix.M33 / scalar, matrix.M14 / scalar, matrix.M24 / scalar, matrix.M34 / scalar);
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all elements of a matrix are non-zero.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>true if all elements are non-zero; false otherwise.</returns>
		public static bool All(Matrix3x4f value)
		{
			return value.M11 != 0 && value.M21 != 0 && value.M31 != 0 && value.M12 != 0 && value.M22 != 0 && value.M32 != 0 && value.M13 != 0 && value.M23 != 0 && value.M33 != 0 && value.M14 != 0 && value.M24 != 0 && value.M34 != 0;
		}
		/// <summary>
		/// Determines whether all elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if every element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Matrix3x4f value, Predicate<float> predicate)
		{
			return predicate(value.M11) && predicate(value.M21) && predicate(value.M31) && predicate(value.M12) && predicate(value.M22) && predicate(value.M32) && predicate(value.M13) && predicate(value.M23) && predicate(value.M33) && predicate(value.M14) && predicate(value.M24) && predicate(value.M34);
		}
		/// <summary>
		/// Determines whether any element of a matrix is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any elements are non-zero; false otherwise.</returns>
		public static bool Any(Matrix3x4f value)
		{
			return value.M11 != 0 || value.M21 != 0 || value.M31 != 0 || value.M12 != 0 || value.M22 != 0 || value.M32 != 0 || value.M13 != 0 || value.M23 != 0 || value.M33 != 0 || value.M14 != 0 || value.M24 != 0 || value.M34 != 0;
		}
		/// <summary>
		/// Determines whether any elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if any element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Matrix3x4f value, Predicate<float> predicate)
		{
			return predicate(value.M11) || predicate(value.M21) || predicate(value.M31) || predicate(value.M12) || predicate(value.M22) || predicate(value.M32) || predicate(value.M13) || predicate(value.M23) || predicate(value.M33) || predicate(value.M14) || predicate(value.M24) || predicate(value.M34);
		}
		#endregion
		#region Per element
		#region Map
		/// <summary>
		/// Maps the elements of a matrix and returns the result.
		/// </summary>
		/// <param name="value">The matrix to map.</param>
		/// <param name="mapping">A mapping function to apply to each element.</param>
		/// <returns>The result of mapping each element of value.</returns>
		public static Matrix3x4d Map(Matrix3x4f value, Func<float, double> mapping)
		{
			return new Matrix3x4d(mapping(value.M11), mapping(value.M21), mapping(value.M31), mapping(value.M12), mapping(value.M22), mapping(value.M32), mapping(value.M13), mapping(value.M23), mapping(value.M33), mapping(value.M14), mapping(value.M24), mapping(value.M34));
		}
		/// <summary>
		/// Maps the elements of a matrix and returns the result.
		/// </summary>
		/// <param name="value">The matrix to map.</param>
		/// <param name="mapping">A mapping function to apply to each element.</param>
		/// <returns>The result of mapping each element of value.</returns>
		public static Matrix3x4f Map(Matrix3x4f value, Func<float, float> mapping)
		{
			return new Matrix3x4f(mapping(value.M11), mapping(value.M21), mapping(value.M31), mapping(value.M12), mapping(value.M22), mapping(value.M32), mapping(value.M13), mapping(value.M23), mapping(value.M33), mapping(value.M14), mapping(value.M24), mapping(value.M34));
		}
		#endregion
		/// <summary>
		/// Multiplys the elements of two matrices and returns the result.
		/// </summary>
		/// <param name="left">The first matrix to modulate.</param>
		/// <param name="right">The second matrix to modulate.</param>
		/// <returns>The result of multiplying each element of left by the matching element in right.</returns>
		public static Matrix3x4f Modulate(Matrix3x4f left, Matrix3x4f right)
		{
			return new Matrix3x4f(left.M11 * right.M11, left.M21 * right.M21, left.M31 * right.M31, left.M12 * right.M12, left.M22 * right.M22, left.M32 * right.M32, left.M13 * right.M13, left.M23 * right.M23, left.M33 * right.M33, left.M14 * right.M14, left.M24 * right.M24, left.M34 * right.M34);
		}
		/// <summary>
		/// Returns the absolute value (per element).
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The absolute value (per element) of value.</returns>
		public static Matrix3x4f Abs(Matrix3x4f value)
		{
			return new Matrix3x4f(Functions.Abs(value.M11), Functions.Abs(value.M21), Functions.Abs(value.M31), Functions.Abs(value.M12), Functions.Abs(value.M22), Functions.Abs(value.M32), Functions.Abs(value.M13), Functions.Abs(value.M23), Functions.Abs(value.M33), Functions.Abs(value.M14), Functions.Abs(value.M24), Functions.Abs(value.M34));
		}
		/// <summary>
		/// Returns a matrix that contains the lowest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The lowest of each element in left and the matching element in right.</returns>
		public static Matrix3x4f Min(Matrix3x4f value1, Matrix3x4f value2)
		{
			return new Matrix3x4f(Functions.Min(value1.M11, value2.M11), Functions.Min(value1.M21, value2.M21), Functions.Min(value1.M31, value2.M31), Functions.Min(value1.M12, value2.M12), Functions.Min(value1.M22, value2.M22), Functions.Min(value1.M32, value2.M32), Functions.Min(value1.M13, value2.M13), Functions.Min(value1.M23, value2.M23), Functions.Min(value1.M33, value2.M33), Functions.Min(value1.M14, value2.M14), Functions.Min(value1.M24, value2.M24), Functions.Min(value1.M34, value2.M34));
		}
		/// <summary>
		/// Returns a matrix that contains the highest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The highest of each element in left and the matching element in right.</returns>
		public static Matrix3x4f Max(Matrix3x4f value1, Matrix3x4f value2)
		{
			return new Matrix3x4f(Functions.Max(value1.M11, value2.M11), Functions.Max(value1.M21, value2.M21), Functions.Max(value1.M31, value2.M31), Functions.Max(value1.M12, value2.M12), Functions.Max(value1.M22, value2.M22), Functions.Max(value1.M32, value2.M32), Functions.Max(value1.M13, value2.M13), Functions.Max(value1.M23, value2.M23), Functions.Max(value1.M33, value2.M33), Functions.Max(value1.M14, value2.M14), Functions.Max(value1.M24, value2.M24), Functions.Max(value1.M34, value2.M34));
		}
		/// <summary>
		/// Constrains each element to a given range.
		/// </summary>
		/// <param name="value">A matrix to constrain.</param>
		/// <param name="min">The minimum values for each element.</param>
		/// <param name="max">The maximum values for each element.</param>
		/// <returns>A matrix with each element constrained to the given range.</returns>
		public static Matrix3x4f Clamp(Matrix3x4f value, Matrix3x4f min, Matrix3x4f max)
		{
			return new Matrix3x4f(Functions.Clamp(value.M11, min.M11, max.M11), Functions.Clamp(value.M21, min.M21, max.M21), Functions.Clamp(value.M31, min.M31, max.M31), Functions.Clamp(value.M12, min.M12, max.M12), Functions.Clamp(value.M22, min.M22, max.M22), Functions.Clamp(value.M32, min.M32, max.M32), Functions.Clamp(value.M13, min.M13, max.M13), Functions.Clamp(value.M23, min.M23, max.M23), Functions.Clamp(value.M33, min.M33, max.M33), Functions.Clamp(value.M14, min.M14, max.M14), Functions.Clamp(value.M24, min.M24, max.M24), Functions.Clamp(value.M34, min.M34, max.M34));
		}
		/// <summary>
		/// Constrains each element to the range 0 to 1.
		/// </summary>
		/// <param name="value">A matrix to saturate.</param>
		/// <returns>A matrix with each element constrained to the range 0 to 1.</returns>
		public static Matrix3x4f Saturate(Matrix3x4f value)
		{
			return new Matrix3x4f(Functions.Saturate(value.M11), Functions.Saturate(value.M21), Functions.Saturate(value.M31), Functions.Saturate(value.M12), Functions.Saturate(value.M22), Functions.Saturate(value.M32), Functions.Saturate(value.M13), Functions.Saturate(value.M23), Functions.Saturate(value.M33), Functions.Saturate(value.M14), Functions.Saturate(value.M24), Functions.Saturate(value.M34));
		}
		/// <summary>
		/// Returns a matrix where each element is the smallest integral value that
		/// is greater than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The ceiling of value.</returns>
		public static Matrix3x4f Ceiling(Matrix3x4f value)
		{
			return new Matrix3x4f(Functions.Ceiling(value.M11), Functions.Ceiling(value.M21), Functions.Ceiling(value.M31), Functions.Ceiling(value.M12), Functions.Ceiling(value.M22), Functions.Ceiling(value.M32), Functions.Ceiling(value.M13), Functions.Ceiling(value.M23), Functions.Ceiling(value.M33), Functions.Ceiling(value.M14), Functions.Ceiling(value.M24), Functions.Ceiling(value.M34));
		}
		/// <summary>
		/// Returns a matrix where each element is the largest integral value that
		/// is less than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The floor of value.</returns>
		public static Matrix3x4f Floor(Matrix3x4f value)
		{
			return new Matrix3x4f(Functions.Floor(value.M11), Functions.Floor(value.M21), Functions.Floor(value.M31), Functions.Floor(value.M12), Functions.Floor(value.M22), Functions.Floor(value.M32), Functions.Floor(value.M13), Functions.Floor(value.M23), Functions.Floor(value.M33), Functions.Floor(value.M14), Functions.Floor(value.M24), Functions.Floor(value.M34));
		}
		/// <summary>
		/// Returns a matrix where each element is the integral part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The integral of value.</returns>
		public static Matrix3x4f Truncate(Matrix3x4f value)
		{
			return new Matrix3x4f(Functions.Truncate(value.M11), Functions.Truncate(value.M21), Functions.Truncate(value.M31), Functions.Truncate(value.M12), Functions.Truncate(value.M22), Functions.Truncate(value.M32), Functions.Truncate(value.M13), Functions.Truncate(value.M23), Functions.Truncate(value.M33), Functions.Truncate(value.M14), Functions.Truncate(value.M24), Functions.Truncate(value.M34));
		}
		/// <summary>
		/// Returns a matrix where each element is the fractional part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The fractional of value.</returns>
		public static Matrix3x4f Fractional(Matrix3x4f value)
		{
			return new Matrix3x4f(Functions.Fractional(value.M11), Functions.Fractional(value.M21), Functions.Fractional(value.M31), Functions.Fractional(value.M12), Functions.Fractional(value.M22), Functions.Fractional(value.M32), Functions.Fractional(value.M13), Functions.Fractional(value.M23), Functions.Fractional(value.M33), Functions.Fractional(value.M14), Functions.Fractional(value.M24), Functions.Fractional(value.M34));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix3x4f Round(Matrix3x4f value)
		{
			return new Matrix3x4f(Functions.Round(value.M11), Functions.Round(value.M21), Functions.Round(value.M31), Functions.Round(value.M12), Functions.Round(value.M22), Functions.Round(value.M32), Functions.Round(value.M13), Functions.Round(value.M23), Functions.Round(value.M33), Functions.Round(value.M14), Functions.Round(value.M24), Functions.Round(value.M34));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix3x4f Round(Matrix3x4f value, int digits)
		{
			return new Matrix3x4f(Functions.Round(value.M11, digits), Functions.Round(value.M21, digits), Functions.Round(value.M31, digits), Functions.Round(value.M12, digits), Functions.Round(value.M22, digits), Functions.Round(value.M32, digits), Functions.Round(value.M13, digits), Functions.Round(value.M23, digits), Functions.Round(value.M33, digits), Functions.Round(value.M14, digits), Functions.Round(value.M24, digits), Functions.Round(value.M34, digits));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix3x4f Round(Matrix3x4f value, MidpointRounding mode)
		{
			return new Matrix3x4f(Functions.Round(value.M11, mode), Functions.Round(value.M21, mode), Functions.Round(value.M31, mode), Functions.Round(value.M12, mode), Functions.Round(value.M22, mode), Functions.Round(value.M32, mode), Functions.Round(value.M13, mode), Functions.Round(value.M23, mode), Functions.Round(value.M33, mode), Functions.Round(value.M14, mode), Functions.Round(value.M24, mode), Functions.Round(value.M34, mode));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix3x4f Round(Matrix3x4f value, int digits, MidpointRounding mode)
		{
			return new Matrix3x4f(Functions.Round(value.M11, digits, mode), Functions.Round(value.M21, digits, mode), Functions.Round(value.M31, digits, mode), Functions.Round(value.M12, digits, mode), Functions.Round(value.M22, digits, mode), Functions.Round(value.M32, digits, mode), Functions.Round(value.M13, digits, mode), Functions.Round(value.M23, digits, mode), Functions.Round(value.M33, digits, mode), Functions.Round(value.M14, digits, mode), Functions.Round(value.M24, digits, mode), Functions.Round(value.M34, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each element in the matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>A matrix with the reciprocal of each of values elements.</returns>
		public static Matrix3x4f Reciprocal(Matrix3x4f value)
		{
			return new Matrix3x4f(1 / value.M11, 1 / value.M21, 1 / value.M31, 1 / value.M12, 1 / value.M22, 1 / value.M32, 1 / value.M13, 1 / value.M23, 1 / value.M33, 1 / value.M14, 1 / value.M24, 1 / value.M34);
		}
		#endregion
		#region Submatrix
		/// <summary>
		/// Returns the specified submatrix of the given matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose submatrix is to returned.</param>
		/// <param name="row">The row to be removed.</param>
		/// <param name="column">The column to be removed.</param>
		public static Matrix2x3f Submatrix(Matrix3x4f matrix, int row, int column)
		{
			if (row < 0 || row > 2)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix3x4f run from 0 to 2, inclusive.");
			if (column < 0 || column > 3)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix3x4f run from 0 to 3, inclusive.");
			if (row == 0 && column == 0)
			{
				return new Matrix2x3f(matrix.M22, matrix.M32, matrix.M23, matrix.M33, matrix.M24, matrix.M34);
			}
			else if (row == 0 && column == 1)
			{
				return new Matrix2x3f(matrix.M21, matrix.M31, matrix.M23, matrix.M33, matrix.M24, matrix.M34);
			}
			else if (row == 0 && column == 2)
			{
				return new Matrix2x3f(matrix.M21, matrix.M31, matrix.M22, matrix.M32, matrix.M24, matrix.M34);
			}
			else if (row == 0 && column == 3)
			{
				return new Matrix2x3f(matrix.M21, matrix.M31, matrix.M22, matrix.M32, matrix.M23, matrix.M33);
			}
			else if (row == 1 && column == 0)
			{
				return new Matrix2x3f(matrix.M12, matrix.M32, matrix.M13, matrix.M33, matrix.M14, matrix.M34);
			}
			else if (row == 1 && column == 1)
			{
				return new Matrix2x3f(matrix.M11, matrix.M31, matrix.M13, matrix.M33, matrix.M14, matrix.M34);
			}
			else if (row == 1 && column == 2)
			{
				return new Matrix2x3f(matrix.M11, matrix.M31, matrix.M12, matrix.M32, matrix.M14, matrix.M34);
			}
			else if (row == 1 && column == 3)
			{
				return new Matrix2x3f(matrix.M11, matrix.M31, matrix.M12, matrix.M32, matrix.M13, matrix.M33);
			}
			else if (row == 2 && column == 0)
			{
				return new Matrix2x3f(matrix.M12, matrix.M22, matrix.M13, matrix.M23, matrix.M14, matrix.M24);
			}
			else if (row == 2 && column == 1)
			{
				return new Matrix2x3f(matrix.M11, matrix.M21, matrix.M13, matrix.M23, matrix.M14, matrix.M24);
			}
			else if (row == 2 && column == 2)
			{
				return new Matrix2x3f(matrix.M11, matrix.M21, matrix.M12, matrix.M22, matrix.M14, matrix.M24);
			}
			else
			{
				return new Matrix2x3f(matrix.M11, matrix.M21, matrix.M12, matrix.M22, matrix.M13, matrix.M23);
			}
		}
		#endregion
		#region Transpose
		/// <summary>
		/// Calculates the transpose of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose transpose is to be calculated.</param>
		/// <returns>The transpose of the specified matrix.</returns>
		public static Matrix4x3f Transpose(Matrix3x4f matrix)
		{
			return new Matrix4x3f(matrix.M11, matrix.M12, matrix.M13, matrix.M14, matrix.M21, matrix.M22, matrix.M23, matrix.M24, matrix.M31, matrix.M32, matrix.M33, matrix.M34);
		}
		#endregion
	}
}
