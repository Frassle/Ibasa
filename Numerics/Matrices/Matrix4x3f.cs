using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics
{
	/// <summary>
	/// Represents a 4 by 3 matrix of floats.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix4x3f: IEquatable<Matrix4x3f>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Matrix4x3f"/> with all of its elements equal to zero.
		/// </summary>
		public static readonly Matrix4x3f Zero = new Matrix4x3f(0);
		/// <summary>
		/// Returns a new <see cref="Matrix4x3f"/> with all of its elements equal to one.
		/// </summary>
		public static readonly Matrix4x3f One = new Matrix4x3f(1);
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
		/// Gets the element of the matrix that exists in the fourth row and first column.
		/// </summary>
		public readonly float M41;
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
		/// Gets the element of the matrix that exists in the fourth row and second column.
		/// </summary>
		public readonly float M42;
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
		/// Gets the element of the matrix that exists in the fourth row and third column.
		/// </summary>
		public readonly float M43;
		#endregion
		#region Properties
		public float this[int row, int column]
		{
			get
			{
				if (row < 0 || row > 3)
					throw new ArgumentOutOfRangeException("row", "Rows for Matrix4x3f run from 0 to 3, inclusive.");
				if (column < 0 || column > 2)
					throw new ArgumentOutOfRangeException("column", "Columns for Matrix4x3f run from 0 to 2, inclusive.");
				int index = row + column * 4;
				return this[index];
			}
		}
		public float this[int index]
		{
			get
			{
				if (index < 0 || index > 11)
					throw new ArgumentOutOfRangeException("index", "Indices for Matrix4x3f run from 0 to 11, inclusive.");
				switch (index)
				{
					case 0: return M11;
					case 1: return M21;
					case 2: return M31;
					case 3: return M41;
					case 4: return M12;
					case 5: return M22;
					case 6: return M32;
					case 7: return M42;
					case 8: return M13;
					case 9: return M23;
					case 10: return M33;
					case 11: return M43;
				}
				return 0;
			}
		}
		public Vector3f GetRow(int row)
		{
			if (row < 0 || row > 3)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix4x3f run from 0 to 3, inclusive.");
			switch (row)
			{
				case 0:
					return new Vector3f(M11, M12, M13);
				case 1:
					return new Vector3f(M21, M22, M23);
				case 2:
					return new Vector3f(M31, M32, M33);
				case 3:
					return new Vector3f(M41, M42, M43);
			}
			return Vector3f.Zero;
		}
		public Vector4f GetColumn(int column)
		{
			if (column < 0 || column > 2)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix4x3f run from 0 to 2, inclusive.");
			switch (column)
			{
				case 0:
					return new Vector4f(M11, M21, M31, M41);
				case 1:
					return new Vector4f(M12, M22, M32, M42);
				case 2:
					return new Vector4f(M13, M23, M33, M43);
			}
			return Vector4f.Zero;
		}
		public float[] ToArray()
		{
			return new float[]
			{
				M11, M21, M31, M41, M12, M22, M32, M42, M13, M23, M33, M43
			};
		}
		#endregion
		#region Constructors
		public Matrix4x3f(float value)
		{
			M11 = value;
			M12 = value;
			M13 = value;
			M21 = value;
			M22 = value;
			M23 = value;
			M31 = value;
			M32 = value;
			M33 = value;
			M41 = value;
			M42 = value;
			M43 = value;
		}
		public Matrix4x3f(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33, float m41, float m42, float m43)
		{
			M11 = m11;
			M21 = m21;
			M31 = m31;
			M41 = m41;
			M12 = m12;
			M22 = m22;
			M32 = m32;
			M42 = m42;
			M13 = m13;
			M23 = m23;
			M33 = m33;
			M43 = m43;
		}
		#endregion
		#region Operations
		public static Matrix4x3f operator +(Matrix4x3f value)
		{
			return value;
		}
		public static Matrix4x3f operator -(Matrix4x3f value)
		{
			return Matrix.Negate(value);
		}
		public static Matrix4x3f operator +(Matrix4x3f left, Matrix4x3f right)
		{
			return Matrix.Add(left, right);
		}
		public static Matrix4x3f operator -(Matrix4x3f left, Matrix4x3f right)
		{
			return Matrix.Subtract(left, right);
		}
		public static Matrix4x3f operator *(Matrix4x3f matrix, float scalar)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix4x3f operator *(float scalar, Matrix4x3f matrix)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix4x2f operator *(Matrix4x3f left, Matrix3x2f right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix4x3f operator *(Matrix4x3f left, Matrix3x3f right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix4x4f operator *(Matrix4x3f left, Matrix3x4f right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix4x3f operator /(Matrix4x3f matrix, float scalar)
		{
			return Matrix.Divide(matrix, scalar);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Matrix4x3d value to a Matrix4x3f.
		/// </summary>
		/// <param name="value">The value to convert to a Matrix4x3f.</param>
		/// <returns>A Matrix4x3f that has all components equal to value.</returns>
		public static explicit operator Matrix4x3f(Matrix4x3d value)
		{
			return new Matrix4x3f((float)value.M11, (float)value.M21, (float)value.M31, (float)value.M41, (float)value.M12, (float)value.M22, (float)value.M32, (float)value.M42, (float)value.M13, (float)value.M23, (float)value.M33, (float)value.M43);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Matrix4x3f"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return M11.GetHashCode() + M21.GetHashCode() + M31.GetHashCode() + M41.GetHashCode() + M12.GetHashCode() + M22.GetHashCode() + M32.GetHashCode() + M42.GetHashCode() + M13.GetHashCode() + M23.GetHashCode() + M33.GetHashCode() + M43.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Matrix4x3f"/> object or a type capable
		/// of implicit conversion to a <see cref="Matrix4x3f"/> object, and its value
		/// is equal to the current <see cref="Matrix4x3f"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix4x3f) { return Equals((Matrix4x3f)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// matrix have the same value.
		/// </summary>
		/// <param name="other">The matrix to compare.</param>
		/// <returns>true if this matrix and value have the same value; otherwise, false.</returns>
		public bool Equals(Matrix4x3f other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Matrix4x3f left, Matrix4x3f right)
		{
			return left == right;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Matrix4x3f left, Matrix4x3f right)
		{
			return left.M11 == right.M11 & left.M21 == right.M21 & left.M31 == right.M31 & left.M41 == right.M41 & left.M12 == right.M12 & left.M22 == right.M22 & left.M32 == right.M32 & left.M42 == right.M42 & left.M13 == right.M13 & left.M23 == right.M23 & left.M33 == right.M33 & left.M43 == right.M43;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are not equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Matrix4x3f left, Matrix4x3f right)
		{
			return left.M11 != right.M11 | left.M21 != right.M21 | left.M31 != right.M31 | left.M41 != right.M41 | left.M12 != right.M12 | left.M22 != right.M22 | left.M32 != right.M32 | left.M42 != right.M42 | left.M13 != right.M13 | left.M23 != right.M23 | left.M33 != right.M33 | left.M43 != right.M43;
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
			return String.Format("[({0}, {1}, {2})({3}, {4}, {5})({6}, {7}, {8})({9}, {10}, {11})]", M11.ToString(format, provider), M21.ToString(format, provider), M31.ToString(format, provider), M41.ToString(format, provider), M12.ToString(format, provider), M22.ToString(format, provider), M32.ToString(format, provider), M42.ToString(format, provider), M13.ToString(format, provider), M23.ToString(format, provider), M33.ToString(format, provider), M43.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for matrix functions.
	/// </summary>
	public static partial class Matrix
	{
		#region Operations
		public static Matrix4x3f Negate(Matrix4x3f value)
		{
			return new Matrix4x3f(-value.M11, -value.M21, -value.M31, -value.M41, -value.M12, -value.M22, -value.M32, -value.M42, -value.M13, -value.M23, -value.M33, -value.M43);
		}
		public static Matrix4x3f Add(Matrix4x3f left, Matrix4x3f right)
		{
			return new Matrix4x3f(left.M11 + right.M11, left.M21 + right.M21, left.M31 + right.M31, left.M41 + right.M41, left.M12 + right.M12, left.M22 + right.M22, left.M32 + right.M32, left.M42 + right.M42, left.M13 + right.M13, left.M23 + right.M23, left.M33 + right.M33, left.M43 + right.M43);
		}
		public static Matrix4x3f Subtract(Matrix4x3f left, Matrix4x3f right)
		{
			return new Matrix4x3f(left.M11 - right.M11, left.M21 - right.M21, left.M31 - right.M31, left.M41 - right.M41, left.M12 - right.M12, left.M22 - right.M22, left.M32 - right.M32, left.M42 - right.M42, left.M13 - right.M13, left.M23 - right.M23, left.M33 - right.M33, left.M43 - right.M43);
		}
		public static Matrix4x2f Multiply(Matrix4x3f left, Matrix3x2f right)
		{
			return new Matrix4x2f(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31, left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31, left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32, left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32, left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32);
		}
		public static Matrix4x3f Multiply(Matrix4x3f left, Matrix3x3f right)
		{
			return new Matrix4x3f(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31, left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31, left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32, left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32, left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32, left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33, left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33, left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33, left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33);
		}
		public static Matrix4x4f Multiply(Matrix4x3f left, Matrix3x4f right)
		{
			return new Matrix4x4f(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31, left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31, left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32, left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32, left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32, left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33, left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33, left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33, left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33, left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34, left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34, left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34, left.M41 * right.M14 + left.M42 * right.M24 + left.M43 * right.M34);
		}
		public static Matrix4x3f Multiply(Matrix4x3f matrix, float scalar)
		{
			return new Matrix4x3f(matrix.M11 * scalar, matrix.M21 * scalar, matrix.M31 * scalar, matrix.M41 * scalar, matrix.M12 * scalar, matrix.M22 * scalar, matrix.M32 * scalar, matrix.M42 * scalar, matrix.M13 * scalar, matrix.M23 * scalar, matrix.M33 * scalar, matrix.M43 * scalar);
		}
		public static Matrix4x3f Divide(Matrix4x3f matrix, float scalar)
		{
			return new Matrix4x3f(matrix.M11 / scalar, matrix.M21 / scalar, matrix.M31 / scalar, matrix.M41 / scalar, matrix.M12 / scalar, matrix.M22 / scalar, matrix.M32 / scalar, matrix.M42 / scalar, matrix.M13 / scalar, matrix.M23 / scalar, matrix.M33 / scalar, matrix.M43 / scalar);
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all elements of a matrix are non-zero.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>true if all elements are non-zero; false otherwise.</returns>
		public static bool All(Matrix4x3f value)
		{
			return value.M11 != 0 && value.M21 != 0 && value.M31 != 0 && value.M41 != 0 && value.M12 != 0 && value.M22 != 0 && value.M32 != 0 && value.M42 != 0 && value.M13 != 0 && value.M23 != 0 && value.M33 != 0 && value.M43 != 0;
		}
		/// <summary>
		/// Determines whether all elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if every element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Matrix4x3f value, Predicate<float> predicate)
		{
			return predicate(value.M11) && predicate(value.M21) && predicate(value.M31) && predicate(value.M41) && predicate(value.M12) && predicate(value.M22) && predicate(value.M32) && predicate(value.M42) && predicate(value.M13) && predicate(value.M23) && predicate(value.M33) && predicate(value.M43);
		}
		/// <summary>
		/// Determines whether any element of a matrix is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any elements are non-zero; false otherwise.</returns>
		public static bool Any(Matrix4x3f value)
		{
			return value.M11 != 0 || value.M21 != 0 || value.M31 != 0 || value.M41 != 0 || value.M12 != 0 || value.M22 != 0 || value.M32 != 0 || value.M42 != 0 || value.M13 != 0 || value.M23 != 0 || value.M33 != 0 || value.M43 != 0;
		}
		/// <summary>
		/// Determines whether any elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if any element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Matrix4x3f value, Predicate<float> predicate)
		{
			return predicate(value.M11) || predicate(value.M21) || predicate(value.M31) || predicate(value.M41) || predicate(value.M12) || predicate(value.M22) || predicate(value.M32) || predicate(value.M42) || predicate(value.M13) || predicate(value.M23) || predicate(value.M33) || predicate(value.M43);
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
		public static Matrix4x3d Map(Matrix4x3f value, Func<float, double> mapping)
		{
			return new Matrix4x3d(mapping(value.M11), mapping(value.M21), mapping(value.M31), mapping(value.M41), mapping(value.M12), mapping(value.M22), mapping(value.M32), mapping(value.M42), mapping(value.M13), mapping(value.M23), mapping(value.M33), mapping(value.M43));
		}
		/// <summary>
		/// Maps the elements of a matrix and returns the result.
		/// </summary>
		/// <param name="value">The matrix to map.</param>
		/// <param name="mapping">A mapping function to apply to each element.</param>
		/// <returns>The result of mapping each element of value.</returns>
		public static Matrix4x3f Map(Matrix4x3f value, Func<float, float> mapping)
		{
			return new Matrix4x3f(mapping(value.M11), mapping(value.M21), mapping(value.M31), mapping(value.M41), mapping(value.M12), mapping(value.M22), mapping(value.M32), mapping(value.M42), mapping(value.M13), mapping(value.M23), mapping(value.M33), mapping(value.M43));
		}
		#endregion
		/// <summary>
		/// Multiplys the elements of two matrices and returns the result.
		/// </summary>
		/// <param name="left">The first matrix to modulate.</param>
		/// <param name="right">The second matrix to modulate.</param>
		/// <returns>The result of multiplying each element of left by the matching element in right.</returns>
		public static Matrix4x3f Modulate(Matrix4x3f left, Matrix4x3f right)
		{
			return new Matrix4x3f(left.M11 * right.M11, left.M21 * right.M21, left.M31 * right.M31, left.M41 * right.M41, left.M12 * right.M12, left.M22 * right.M22, left.M32 * right.M32, left.M42 * right.M42, left.M13 * right.M13, left.M23 * right.M23, left.M33 * right.M33, left.M43 * right.M43);
		}
		/// <summary>
		/// Returns the absolute value (per element).
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The absolute value (per element) of value.</returns>
		public static Matrix4x3f Abs(Matrix4x3f value)
		{
			return new Matrix4x3f(Functions.Abs(value.M11), Functions.Abs(value.M21), Functions.Abs(value.M31), Functions.Abs(value.M41), Functions.Abs(value.M12), Functions.Abs(value.M22), Functions.Abs(value.M32), Functions.Abs(value.M42), Functions.Abs(value.M13), Functions.Abs(value.M23), Functions.Abs(value.M33), Functions.Abs(value.M43));
		}
		/// <summary>
		/// Returns a matrix that contains the lowest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The lowest of each element in left and the matching element in right.</returns>
		public static Matrix4x3f Min(Matrix4x3f value1, Matrix4x3f value2)
		{
			return new Matrix4x3f(Functions.Min(value1.M11, value2.M11), Functions.Min(value1.M21, value2.M21), Functions.Min(value1.M31, value2.M31), Functions.Min(value1.M41, value2.M41), Functions.Min(value1.M12, value2.M12), Functions.Min(value1.M22, value2.M22), Functions.Min(value1.M32, value2.M32), Functions.Min(value1.M42, value2.M42), Functions.Min(value1.M13, value2.M13), Functions.Min(value1.M23, value2.M23), Functions.Min(value1.M33, value2.M33), Functions.Min(value1.M43, value2.M43));
		}
		/// <summary>
		/// Returns a matrix that contains the highest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The highest of each element in left and the matching element in right.</returns>
		public static Matrix4x3f Max(Matrix4x3f value1, Matrix4x3f value2)
		{
			return new Matrix4x3f(Functions.Max(value1.M11, value2.M11), Functions.Max(value1.M21, value2.M21), Functions.Max(value1.M31, value2.M31), Functions.Max(value1.M41, value2.M41), Functions.Max(value1.M12, value2.M12), Functions.Max(value1.M22, value2.M22), Functions.Max(value1.M32, value2.M32), Functions.Max(value1.M42, value2.M42), Functions.Max(value1.M13, value2.M13), Functions.Max(value1.M23, value2.M23), Functions.Max(value1.M33, value2.M33), Functions.Max(value1.M43, value2.M43));
		}
		/// <summary>
		/// Constrains each element to a given range.
		/// </summary>
		/// <param name="value">A matrix to constrain.</param>
		/// <param name="min">The minimum values for each element.</param>
		/// <param name="max">The maximum values for each element.</param>
		/// <returns>A matrix with each element constrained to the given range.</returns>
		public static Matrix4x3f Clamp(Matrix4x3f value, Matrix4x3f min, Matrix4x3f max)
		{
			return new Matrix4x3f(Functions.Clamp(value.M11, min.M11, max.M11), Functions.Clamp(value.M21, min.M21, max.M21), Functions.Clamp(value.M31, min.M31, max.M31), Functions.Clamp(value.M41, min.M41, max.M41), Functions.Clamp(value.M12, min.M12, max.M12), Functions.Clamp(value.M22, min.M22, max.M22), Functions.Clamp(value.M32, min.M32, max.M32), Functions.Clamp(value.M42, min.M42, max.M42), Functions.Clamp(value.M13, min.M13, max.M13), Functions.Clamp(value.M23, min.M23, max.M23), Functions.Clamp(value.M33, min.M33, max.M33), Functions.Clamp(value.M43, min.M43, max.M43));
		}
		/// <summary>
		/// Constrains each element to the range 0 to 1.
		/// </summary>
		/// <param name="value">A matrix to saturate.</param>
		/// <returns>A matrix with each element constrained to the range 0 to 1.</returns>
		public static Matrix4x3f Saturate(Matrix4x3f value)
		{
			return new Matrix4x3f(Functions.Saturate(value.M11), Functions.Saturate(value.M21), Functions.Saturate(value.M31), Functions.Saturate(value.M41), Functions.Saturate(value.M12), Functions.Saturate(value.M22), Functions.Saturate(value.M32), Functions.Saturate(value.M42), Functions.Saturate(value.M13), Functions.Saturate(value.M23), Functions.Saturate(value.M33), Functions.Saturate(value.M43));
		}
		/// <summary>
		/// Returns a matrix where each element is the smallest integral value that
		/// is greater than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The ceiling of value.</returns>
		public static Matrix4x3f Ceiling(Matrix4x3f value)
		{
			return new Matrix4x3f(Functions.Ceiling(value.M11), Functions.Ceiling(value.M21), Functions.Ceiling(value.M31), Functions.Ceiling(value.M41), Functions.Ceiling(value.M12), Functions.Ceiling(value.M22), Functions.Ceiling(value.M32), Functions.Ceiling(value.M42), Functions.Ceiling(value.M13), Functions.Ceiling(value.M23), Functions.Ceiling(value.M33), Functions.Ceiling(value.M43));
		}
		/// <summary>
		/// Returns a matrix where each element is the largest integral value that
		/// is less than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The floor of value.</returns>
		public static Matrix4x3f Floor(Matrix4x3f value)
		{
			return new Matrix4x3f(Functions.Floor(value.M11), Functions.Floor(value.M21), Functions.Floor(value.M31), Functions.Floor(value.M41), Functions.Floor(value.M12), Functions.Floor(value.M22), Functions.Floor(value.M32), Functions.Floor(value.M42), Functions.Floor(value.M13), Functions.Floor(value.M23), Functions.Floor(value.M33), Functions.Floor(value.M43));
		}
		/// <summary>
		/// Returns a matrix where each element is the integral part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The integral of value.</returns>
		public static Matrix4x3f Truncate(Matrix4x3f value)
		{
			return new Matrix4x3f(Functions.Truncate(value.M11), Functions.Truncate(value.M21), Functions.Truncate(value.M31), Functions.Truncate(value.M41), Functions.Truncate(value.M12), Functions.Truncate(value.M22), Functions.Truncate(value.M32), Functions.Truncate(value.M42), Functions.Truncate(value.M13), Functions.Truncate(value.M23), Functions.Truncate(value.M33), Functions.Truncate(value.M43));
		}
		/// <summary>
		/// Returns a matrix where each element is the fractional part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The fractional of value.</returns>
		public static Matrix4x3f Fractional(Matrix4x3f value)
		{
			return new Matrix4x3f(Functions.Fractional(value.M11), Functions.Fractional(value.M21), Functions.Fractional(value.M31), Functions.Fractional(value.M41), Functions.Fractional(value.M12), Functions.Fractional(value.M22), Functions.Fractional(value.M32), Functions.Fractional(value.M42), Functions.Fractional(value.M13), Functions.Fractional(value.M23), Functions.Fractional(value.M33), Functions.Fractional(value.M43));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix4x3f Round(Matrix4x3f value)
		{
			return new Matrix4x3f(Functions.Round(value.M11), Functions.Round(value.M21), Functions.Round(value.M31), Functions.Round(value.M41), Functions.Round(value.M12), Functions.Round(value.M22), Functions.Round(value.M32), Functions.Round(value.M42), Functions.Round(value.M13), Functions.Round(value.M23), Functions.Round(value.M33), Functions.Round(value.M43));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix4x3f Round(Matrix4x3f value, int digits)
		{
			return new Matrix4x3f(Functions.Round(value.M11, digits), Functions.Round(value.M21, digits), Functions.Round(value.M31, digits), Functions.Round(value.M41, digits), Functions.Round(value.M12, digits), Functions.Round(value.M22, digits), Functions.Round(value.M32, digits), Functions.Round(value.M42, digits), Functions.Round(value.M13, digits), Functions.Round(value.M23, digits), Functions.Round(value.M33, digits), Functions.Round(value.M43, digits));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix4x3f Round(Matrix4x3f value, MidpointRounding mode)
		{
			return new Matrix4x3f(Functions.Round(value.M11, mode), Functions.Round(value.M21, mode), Functions.Round(value.M31, mode), Functions.Round(value.M41, mode), Functions.Round(value.M12, mode), Functions.Round(value.M22, mode), Functions.Round(value.M32, mode), Functions.Round(value.M42, mode), Functions.Round(value.M13, mode), Functions.Round(value.M23, mode), Functions.Round(value.M33, mode), Functions.Round(value.M43, mode));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix4x3f Round(Matrix4x3f value, int digits, MidpointRounding mode)
		{
			return new Matrix4x3f(Functions.Round(value.M11, digits, mode), Functions.Round(value.M21, digits, mode), Functions.Round(value.M31, digits, mode), Functions.Round(value.M41, digits, mode), Functions.Round(value.M12, digits, mode), Functions.Round(value.M22, digits, mode), Functions.Round(value.M32, digits, mode), Functions.Round(value.M42, digits, mode), Functions.Round(value.M13, digits, mode), Functions.Round(value.M23, digits, mode), Functions.Round(value.M33, digits, mode), Functions.Round(value.M43, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each element in the matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>A matrix with the reciprocal of each of values elements.</returns>
		public static Matrix4x3f Reciprocal(Matrix4x3f value)
		{
			return new Matrix4x3f(1 / value.M11, 1 / value.M21, 1 / value.M31, 1 / value.M41, 1 / value.M12, 1 / value.M22, 1 / value.M32, 1 / value.M42, 1 / value.M13, 1 / value.M23, 1 / value.M33, 1 / value.M43);
		}
		#endregion
		#region Submatrix
		/// <summary>
		/// Returns the specified submatrix of the given matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose submatrix is to returned.</param>
		/// <param name="row">The row to be removed.</param>
		/// <param name="column">The column to be removed.</param>
		public static Matrix3x2f Submatrix(Matrix4x3f matrix, int row, int column)
		{
			if (row < 0 || row > 3)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix4x3f run from 0 to 3, inclusive.");
			if (column < 0 || column > 2)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix4x3f run from 0 to 2, inclusive.");
			if (row == 0 && column == 0)
			{
				return new Matrix3x2f(matrix.M22, matrix.M32, matrix.M42, matrix.M23, matrix.M33, matrix.M43);
			}
			else if (row == 0 && column == 1)
			{
				return new Matrix3x2f(matrix.M21, matrix.M31, matrix.M41, matrix.M23, matrix.M33, matrix.M43);
			}
			else if (row == 0 && column == 2)
			{
				return new Matrix3x2f(matrix.M21, matrix.M31, matrix.M41, matrix.M22, matrix.M32, matrix.M42);
			}
			else if (row == 1 && column == 0)
			{
				return new Matrix3x2f(matrix.M12, matrix.M32, matrix.M42, matrix.M13, matrix.M33, matrix.M43);
			}
			else if (row == 1 && column == 1)
			{
				return new Matrix3x2f(matrix.M11, matrix.M31, matrix.M41, matrix.M13, matrix.M33, matrix.M43);
			}
			else if (row == 1 && column == 2)
			{
				return new Matrix3x2f(matrix.M11, matrix.M31, matrix.M41, matrix.M12, matrix.M32, matrix.M42);
			}
			else if (row == 2 && column == 0)
			{
				return new Matrix3x2f(matrix.M12, matrix.M22, matrix.M42, matrix.M13, matrix.M23, matrix.M43);
			}
			else if (row == 2 && column == 1)
			{
				return new Matrix3x2f(matrix.M11, matrix.M21, matrix.M41, matrix.M13, matrix.M23, matrix.M43);
			}
			else if (row == 2 && column == 2)
			{
				return new Matrix3x2f(matrix.M11, matrix.M21, matrix.M41, matrix.M12, matrix.M22, matrix.M42);
			}
			else if (row == 3 && column == 0)
			{
				return new Matrix3x2f(matrix.M12, matrix.M22, matrix.M32, matrix.M13, matrix.M23, matrix.M33);
			}
			else if (row == 3 && column == 1)
			{
				return new Matrix3x2f(matrix.M11, matrix.M21, matrix.M31, matrix.M13, matrix.M23, matrix.M33);
			}
			else
			{
				return new Matrix3x2f(matrix.M11, matrix.M21, matrix.M31, matrix.M12, matrix.M22, matrix.M32);
			}
		}
		#endregion
		#region Transpose
		/// <summary>
		/// Calculates the transpose of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose transpose is to be calculated.</param>
		/// <returns>The transpose of the specified matrix.</returns>
		public static Matrix3x4f Transpose(Matrix4x3f matrix)
		{
			return new Matrix3x4f(matrix.M11, matrix.M12, matrix.M13, matrix.M21, matrix.M22, matrix.M23, matrix.M31, matrix.M32, matrix.M33, matrix.M41, matrix.M42, matrix.M43);
		}
		#endregion
	}
}
