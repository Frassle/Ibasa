using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a 2 by 2 matrix of floats.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix2x2f: IEquatable<Matrix2x2f>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Matrix2x2f"/> with all of its elements equal to zero.
		/// </summary>
		public static readonly Matrix2x2f Zero = new Matrix2x2f(0);
		/// <summary>
		/// Returns a new <see cref="Matrix2x2f"/> with all of its elements equal to one.
		/// </summary>
		public static readonly Matrix2x2f One = new Matrix2x2f(1);
		/// <summary>
		/// Returns the identity <see cref="Matrix2x2f"/>.
		/// </summary>
		public static readonly Matrix2x2f Identity = new Matrix2x2f(1, 0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and first column.
		/// </summary>
		public readonly float M11;
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and second column.
		/// </summary>
		public readonly float M12;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and first column.
		/// </summary>
		public readonly float M21;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and second column.
		/// </summary>
		public readonly float M22;
		#endregion
		#region Properties
		public float this[int row, int column]
		{
			get
			{
				if (row < 0 || row > 1)
					throw new ArgumentOutOfRangeException("row", "Rows for Matrix2x2f run from 0 to 1, inclusive.");
				if (column < 0 || column > 1)
					throw new ArgumentOutOfRangeException("column", "Columns for Matrix2x2f run from 0 to 1, inclusive.");
				int index = column + row * 2;
				return this[index];
			}
		}
		public float this[int index]
		{
			get
			{
				if (index < 0 || index > 3)
					throw new ArgumentOutOfRangeException("index", "Indices for Matrix2x2f run from 0 to 3, inclusive.");
				switch (index)
				{
					case 0: return M11;
					case 1: return M12;
					case 2: return M21;
					case 3: return M22;
				}
				return 0;
			}
		}
		public Vector2f GetRow(int row)
		{
			if (row < 0 || row > 1)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix2x2f run from 0 to 1, inclusive.");
			switch (row)
			{
				case 0:
					return new Vector2f(M11, M12);
				case 1:
					return new Vector2f(M21, M22);
			}
			return Vector2f.Zero;
		}
		public Vector2f GetColumn(int column)
		{
			if (column < 0 || column > 1)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix2x2f run from 0 to 1, inclusive.");
			switch (column)
			{
				case 0:
					return new Vector2f(M11, M21);
				case 1:
					return new Vector2f(M12, M22);
			}
			return Vector2f.Zero;
		}
		public float[] ToArray()
		{
			return new float[]
			{
				M11, M12, M21, M22
			};
		}
		#endregion
		#region Constructors
		public Matrix2x2f(float value)
		{
			M11 = value;
			M12 = value;
			M21 = value;
			M22 = value;
		}
		public Matrix2x2f(float m11, float m12, float m21, float m22)
		{
			M11 = m11;
			M12 = m12;
			M21 = m21;
			M22 = m22;
		}
		public Matrix2x2f(Vector2f row1, Vector2f row2)
		{
			M11 = row1.X;
			M12 = row1.Y;
			M21 = row2.X;
			M22 = row2.Y;
		}
		#endregion
		#region Operations
		public static Matrix2x2f operator +(Matrix2x2f value)
		{
			return value;
		}
		public static Matrix2x2f operator -(Matrix2x2f value)
		{
			return Matrix.Negate(value);
		}
		public static Matrix2x2f operator +(Matrix2x2f left, Matrix2x2f right)
		{
			return Matrix.Add(left, right);
		}
		public static Matrix2x2f operator -(Matrix2x2f left, Matrix2x2f right)
		{
			return Matrix.Subtract(left, right);
		}
		public static Matrix2x2f operator *(Matrix2x2f matrix, float scalar)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix2x2f operator *(float scalar, Matrix2x2f matrix)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix2x2f operator *(Matrix2x2f left, Matrix2x2f right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix2x3f operator *(Matrix2x2f left, Matrix2x3f right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix2x4f operator *(Matrix2x2f left, Matrix2x4f right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix2x2f operator /(Matrix2x2f matrix, float scalar)
		{
			return Matrix.Divide(matrix, scalar);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Matrix2x2d value to a Matrix2x2f.
		/// </summary>
		/// <param name="value">The value to convert to a Matrix2x2f.</param>
		/// <returns>A Matrix2x2f that has all components equal to value.</returns>
		public static explicit operator Matrix2x2f(Matrix2x2d value)
		{
			return new Matrix2x2f((float)value.M11, (float)value.M12, (float)value.M21, (float)value.M22);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Matrix2x2f"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return M11.GetHashCode() + M12.GetHashCode() + M21.GetHashCode() + M22.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Matrix2x2f"/> object or a type capable
		/// of implicit conversion to a <see cref="Matrix2x2f"/> object, and its value
		/// is equal to the current <see cref="Matrix2x2f"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix2x2f) { return Equals((Matrix2x2f)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// matrix have the same value.
		/// </summary>
		/// <param name="other">The matrix to compare.</param>
		/// <returns>true if this matrix and value have the same value; otherwise, false.</returns>
		public bool Equals(Matrix2x2f other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Matrix2x2f left, Matrix2x2f right)
		{
			return left == right;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Matrix2x2f left, Matrix2x2f right)
		{
			return left.M11 == right.M11 & left.M12 == right.M12 & left.M21 == right.M21 & left.M22 == right.M22;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are not equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Matrix2x2f left, Matrix2x2f right)
		{
			return left.M11 != right.M11 | left.M12 != right.M12 | left.M21 != right.M21 | left.M22 != right.M22;
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
			return String.Format("[({0}, {1})({2}, {3})]", M11.ToString(format, provider), M12.ToString(format, provider), M21.ToString(format, provider), M22.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for matrix functions.
	/// </summary>
	public static partial class Matrix
	{
		#region Operations
		public static Matrix2x2f Negate(Matrix2x2f value)
		{
			return new Matrix2x2f(-value.M11, -value.M12, -value.M21, -value.M22);
		}
		public static Matrix2x2f Add(Matrix2x2f left, Matrix2x2f right)
		{
			return new Matrix2x2f(left.M11 + right.M11, left.M12 + right.M12, left.M21 + right.M21, left.M22 + right.M22);
		}
		public static Matrix2x2f Subtract(Matrix2x2f left, Matrix2x2f right)
		{
			return new Matrix2x2f(left.M11 - right.M11, left.M12 - right.M12, left.M21 - right.M21, left.M22 - right.M22);
		}
		public static Matrix2x2f Multiply(Matrix2x2f left, Matrix2x2f right)
		{
			return new Matrix2x2f(left.M11 * right.M11 + left.M12 * right.M21, left.M11 * right.M12 + left.M12 * right.M22, left.M21 * right.M11 + left.M22 * right.M21, left.M21 * right.M12 + left.M22 * right.M22);
		}
		public static Matrix2x3f Multiply(Matrix2x2f left, Matrix2x3f right)
		{
			return new Matrix2x3f(left.M11 * right.M11 + left.M12 * right.M21, left.M11 * right.M12 + left.M12 * right.M22, left.M11 * right.M13 + left.M12 * right.M23, left.M21 * right.M11 + left.M22 * right.M21, left.M21 * right.M12 + left.M22 * right.M22, left.M21 * right.M13 + left.M22 * right.M23);
		}
		public static Matrix2x4f Multiply(Matrix2x2f left, Matrix2x4f right)
		{
			return new Matrix2x4f(left.M11 * right.M11 + left.M12 * right.M21, left.M11 * right.M12 + left.M12 * right.M22, left.M11 * right.M13 + left.M12 * right.M23, left.M11 * right.M14 + left.M12 * right.M24, left.M21 * right.M11 + left.M22 * right.M21, left.M21 * right.M12 + left.M22 * right.M22, left.M21 * right.M13 + left.M22 * right.M23, left.M21 * right.M14 + left.M22 * right.M24);
		}
		public static Matrix2x2f Multiply(Matrix2x2f matrix, float scalar)
		{
			return new Matrix2x2f(matrix.M11 * scalar, matrix.M12 * scalar, matrix.M21 * scalar, matrix.M22 * scalar);
		}
		public static Matrix2x2f Divide(Matrix2x2f matrix, float scalar)
		{
			return new Matrix2x2f(matrix.M11 / scalar, matrix.M12 / scalar, matrix.M21 / scalar, matrix.M22 / scalar);
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all elements of a matrix are non-zero.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>true if all elements are non-zero; false otherwise.</returns>
		public static bool All(Matrix2x2f value)
		{
			return value.M11 != 0 && value.M12 != 0 && value.M21 != 0 && value.M22 != 0;
		}
		/// <summary>
		/// Determines whether all elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if every element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Matrix2x2f value, Predicate<float> predicate)
		{
			return predicate(value.M11) && predicate(value.M12) && predicate(value.M21) && predicate(value.M22);
		}
		/// <summary>
		/// Determines whether any element of a matrix is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any elements are non-zero; false otherwise.</returns>
		public static bool Any(Matrix2x2f value)
		{
			return value.M11 != 0 || value.M12 != 0 || value.M21 != 0 || value.M22 != 0;
		}
		/// <summary>
		/// Determines whether any elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if any element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Matrix2x2f value, Predicate<float> predicate)
		{
			return predicate(value.M11) || predicate(value.M12) || predicate(value.M21) || predicate(value.M22);
		}
		#endregion
		#region Per element
		#region Transform
		/// <summary>
		/// Transforms the elements of a matrix and returns the result.
		/// </summary>
		/// <param name="value">The matrix to transform.</param>
		/// <param name="transformer">A transform function to apply to each element.</param>
		/// <returns>The result of transforming each element of value.</returns>
		public static Matrix2x2d Transform(Matrix2x2f value, Func<float, double> transformer)
		{
			return new Matrix2x2d(transformer(value.M11), transformer(value.M12), transformer(value.M21), transformer(value.M22));
		}
		/// <summary>
		/// Transforms the elements of a matrix and returns the result.
		/// </summary>
		/// <param name="value">The matrix to transform.</param>
		/// <param name="transformer">A transform function to apply to each element.</param>
		/// <returns>The result of transforming each element of value.</returns>
		public static Matrix2x2f Transform(Matrix2x2f value, Func<float, float> transformer)
		{
			return new Matrix2x2f(transformer(value.M11), transformer(value.M12), transformer(value.M21), transformer(value.M22));
		}
		#endregion
		/// <summary>
		/// Multiplys the elements of two matrices and returns the result.
		/// </summary>
		/// <param name="left">The first matrix to modulate.</param>
		/// <param name="right">The second matrix to modulate.</param>
		/// <returns>The result of multiplying each element of left by the matching element in right.</returns>
		public static Matrix2x2f Modulate(Matrix2x2f left, Matrix2x2f right)
		{
			return new Matrix2x2f(left.M11 * right.M11, left.M12 * right.M12, left.M21 * right.M21, left.M22 * right.M22);
		}
		/// <summary>
		/// Returns the absolute value (per element).
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The absolute value (per element) of value.</returns>
		public static Matrix2x2f Abs(Matrix2x2f value)
		{
			return new Matrix2x2f(Functions.Abs(value.M11), Functions.Abs(value.M12), Functions.Abs(value.M21), Functions.Abs(value.M22));
		}
		/// <summary>
		/// Returns a matrix that contains the lowest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The lowest of each element in left and the matching element in right.</returns>
		public static Matrix2x2f Min(Matrix2x2f value1, Matrix2x2f value2)
		{
			return new Matrix2x2f(Functions.Min(value1.M11, value2.M11), Functions.Min(value1.M12, value2.M12), Functions.Min(value1.M21, value2.M21), Functions.Min(value1.M22, value2.M22));
		}
		/// <summary>
		/// Returns a matrix that contains the highest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The highest of each element in left and the matching element in right.</returns>
		public static Matrix2x2f Max(Matrix2x2f value1, Matrix2x2f value2)
		{
			return new Matrix2x2f(Functions.Max(value1.M11, value2.M11), Functions.Max(value1.M12, value2.M12), Functions.Max(value1.M21, value2.M21), Functions.Max(value1.M22, value2.M22));
		}
		/// <summary>
		/// Constrains each element to a given range.
		/// </summary>
		/// <param name="value">A matrix to constrain.</param>
		/// <param name="min">The minimum values for each element.</param>
		/// <param name="max">The maximum values for each element.</param>
		/// <returns>A matrix with each element constrained to the given range.</returns>
		public static Matrix2x2f Clamp(Matrix2x2f value, Matrix2x2f min, Matrix2x2f max)
		{
			return new Matrix2x2f(Functions.Clamp(value.M11, min.M11, max.M11), Functions.Clamp(value.M12, min.M12, max.M12), Functions.Clamp(value.M21, min.M21, max.M21), Functions.Clamp(value.M22, min.M22, max.M22));
		}
		/// <summary>
		/// Constrains each element to the range 0 to 1.
		/// </summary>
		/// <param name="value">A matrix to saturate.</param>
		/// <returns>A matrix with each element constrained to the range 0 to 1.</returns>
		public static Matrix2x2f Saturate(Matrix2x2f value)
		{
			return new Matrix2x2f(Functions.Saturate(value.M11), Functions.Saturate(value.M12), Functions.Saturate(value.M21), Functions.Saturate(value.M22));
		}
		/// <summary>
		/// Returns a matrix where each element is the smallest integral value that
		/// is greater than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The ceiling of value.</returns>
		public static Matrix2x2f Ceiling(Matrix2x2f value)
		{
			return new Matrix2x2f(Functions.Ceiling(value.M11), Functions.Ceiling(value.M12), Functions.Ceiling(value.M21), Functions.Ceiling(value.M22));
		}
		/// <summary>
		/// Returns a matrix where each element is the largest integral value that
		/// is less than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The floor of value.</returns>
		public static Matrix2x2f Floor(Matrix2x2f value)
		{
			return new Matrix2x2f(Functions.Floor(value.M11), Functions.Floor(value.M12), Functions.Floor(value.M21), Functions.Floor(value.M22));
		}
		/// <summary>
		/// Returns a matrix where each element is the integral part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The integral of value.</returns>
		public static Matrix2x2f Truncate(Matrix2x2f value)
		{
			return new Matrix2x2f(Functions.Truncate(value.M11), Functions.Truncate(value.M12), Functions.Truncate(value.M21), Functions.Truncate(value.M22));
		}
		/// <summary>
		/// Returns a matrix where each element is the fractional part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The fractional of value.</returns>
		public static Matrix2x2f Fractional(Matrix2x2f value)
		{
			return new Matrix2x2f(Functions.Fractional(value.M11), Functions.Fractional(value.M12), Functions.Fractional(value.M21), Functions.Fractional(value.M22));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix2x2f Round(Matrix2x2f value)
		{
			return new Matrix2x2f(Functions.Round(value.M11), Functions.Round(value.M12), Functions.Round(value.M21), Functions.Round(value.M22));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix2x2f Round(Matrix2x2f value, int digits)
		{
			return new Matrix2x2f(Functions.Round(value.M11, digits), Functions.Round(value.M12, digits), Functions.Round(value.M21, digits), Functions.Round(value.M22, digits));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix2x2f Round(Matrix2x2f value, MidpointRounding mode)
		{
			return new Matrix2x2f(Functions.Round(value.M11, mode), Functions.Round(value.M12, mode), Functions.Round(value.M21, mode), Functions.Round(value.M22, mode));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix2x2f Round(Matrix2x2f value, int digits, MidpointRounding mode)
		{
			return new Matrix2x2f(Functions.Round(value.M11, digits, mode), Functions.Round(value.M12, digits, mode), Functions.Round(value.M21, digits, mode), Functions.Round(value.M22, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each element in the matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>A matrix with the reciprocal of each of values elements.</returns>
		public static Matrix2x2f Reciprocal(Matrix2x2f value)
		{
			return new Matrix2x2f(1 / value.M11, 1 / value.M12, 1 / value.M21, 1 / value.M22);
		}
		#endregion
		#region Submatrix
		/// <summary>
		/// Returns the specified submatrix of the given matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose submatrix is to returned.</param>
		/// <param name="row">The row to be removed.</param>
		/// <param name="column">The column to be removed.</param>
		public static float Submatrix(Matrix2x2f matrix, int row, int column)
		{
			if (row < 0 || row > 1)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix2x2f run from 0 to 1, inclusive.");
			if (column < 0 || column > 1)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix2x2f run from 0 to 1, inclusive.");
			if(row == 0 && column == 0) { return matrix.M22; }
			else if(row == 0 && column == 1) { return matrix.M21; }
			else if(row == 1 && column == 0) { return matrix.M12; }
			else { return matrix.M11; }
		}
		#endregion
		#region Invert, Determinant
		/// <summary>
		/// Calculates the inverse of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose inverse is to be calculated.</param>
		/// <param name="determinant">When the method completes, contains the determinant of the matrix.</param>
		public static Matrix2x2f Invert(Matrix2x2f matrix, out float determinant)
		{
			determinant = matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21;
			return new Matrix2x2f(determinant * matrix.M22, determinant * -matrix.M12, determinant * -matrix.M21, determinant * matrix.M11);
		}
		/// <summary>
		/// Calculates the inverse of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose inverse is to be calculated.</param>
		/// <param name="determinant">When the method completes, contains the determinant of the matrix.</param>
		public static Matrix2x2f Invert(Matrix2x2f matrix)
		{
			float determinant;
			return Invert(matrix, out determinant);
		}
		/// <summary>
		/// Calculates the determinant of the matrix.
		/// </summary>
		/// <returns>The determinant of the matrix.</returns>
		public static float Determinant(Matrix2x2f matrix)
		{
			return matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21;
		}
		#endregion
		#region Transpose
		/// <summary>
		/// Calculates the transpose of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose transpose is to be calculated.</param>
		/// <returns>The transpose of the specified matrix.</returns>
		public static Matrix2x2f Transpose(Matrix2x2f matrix)
		{
			return new Matrix2x2f(matrix.M11, matrix.M21, matrix.M12, matrix.M22);
		}
		#endregion
	}
}
