using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics
{
	/// <summary>
	/// Represents a 2 by 3 matrix of doubles.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix2x3d: IEquatable<Matrix2x3d>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Matrix2x3d"/> with all of its elements equal to zero.
		/// </summary>
		public static readonly Matrix2x3d Zero = new Matrix2x3d(0);
		/// <summary>
		/// Returns a new <see cref="Matrix2x3d"/> with all of its elements equal to one.
		/// </summary>
		public static readonly Matrix2x3d One = new Matrix2x3d(1);
		#endregion
		#region Fields
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and first column.
		/// </summary>
		public readonly double M11;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and first column.
		/// </summary>
		public readonly double M21;
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and second column.
		/// </summary>
		public readonly double M12;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and second column.
		/// </summary>
		public readonly double M22;
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and third column.
		/// </summary>
		public readonly double M13;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and third column.
		/// </summary>
		public readonly double M23;
		#endregion
		#region Properties
		public double this[int row, int column]
		{
			get
			{
				if (row < 0 || row > 1)
					throw new ArgumentOutOfRangeException("row", "Rows for Matrix2x3d run from 0 to 1, inclusive.");
				if (column < 0 || column > 2)
					throw new ArgumentOutOfRangeException("column", "Columns for Matrix2x3d run from 0 to 2, inclusive.");
				int index = row + column * 2;
				return this[index];
			}
		}
		public double this[int index]
		{
			get
			{
				if (index < 0 || index > 5)
					throw new ArgumentOutOfRangeException("index", "Indices for Matrix2x3d run from 0 to 5, inclusive.");
				switch (index)
				{
					case 0: return M11;
					case 1: return M21;
					case 2: return M12;
					case 3: return M22;
					case 4: return M13;
					case 5: return M23;
				}
				return 0;
			}
		}
		public Vector3d GetRow(int row)
		{
			if (row < 0 || row > 1)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix2x3d run from 0 to 1, inclusive.");
			switch (row)
			{
				case 0:
					return new Vector3d(M11, M12, M13);
				case 1:
					return new Vector3d(M21, M22, M23);
			}
			return Vector3d.Zero;
		}
		public Vector2d GetColumn(int column)
		{
			if (column < 0 || column > 2)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix2x3d run from 0 to 2, inclusive.");
			switch (column)
			{
				case 0:
					return new Vector2d(M11, M21);
				case 1:
					return new Vector2d(M12, M22);
				case 2:
					return new Vector2d(M13, M23);
			}
			return Vector2d.Zero;
		}
		public double[] ToArray()
		{
			return new double[]
			{
				M11, M21, M12, M22, M13, M23
			};
		}
		#endregion
		#region Constructors
		public Matrix2x3d(double value)
		{
			M11 = value;
			M12 = value;
			M13 = value;
			M21 = value;
			M22 = value;
			M23 = value;
		}
		public Matrix2x3d(double m11, double m12, double m13, double m21, double m22, double m23)
		{
			M11 = m11;
			M21 = m21;
			M12 = m12;
			M22 = m22;
			M13 = m13;
			M23 = m23;
		}
		#endregion
		#region Operations
		public static Matrix2x3d operator +(Matrix2x3d value)
		{
			return value;
		}
		public static Matrix2x3d operator -(Matrix2x3d value)
		{
			return Matrix.Negate(value);
		}
		public static Matrix2x3d operator +(Matrix2x3d left, Matrix2x3d right)
		{
			return Matrix.Add(left, right);
		}
		public static Matrix2x3d operator -(Matrix2x3d left, Matrix2x3d right)
		{
			return Matrix.Subtract(left, right);
		}
		public static Matrix2x3d operator *(Matrix2x3d matrix, double scalar)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix2x3d operator *(double scalar, Matrix2x3d matrix)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix2x2d operator *(Matrix2x3d left, Matrix3x2d right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix2x3d operator *(Matrix2x3d left, Matrix3x3d right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix2x4d operator *(Matrix2x3d left, Matrix3x4d right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix2x3d operator /(Matrix2x3d matrix, double scalar)
		{
			return Matrix.Divide(matrix, scalar);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a Matrix2x3f value to a Matrix2x3d.
		/// </summary>
		/// <param name="value">The value to convert to a Matrix2x3d.</param>
		/// <returns>A Matrix2x3d that has all components equal to value.</returns>
		public static implicit operator Matrix2x3d(Matrix2x3f value)
		{
			return new Matrix2x3d((double)value.M11, (double)value.M21, (double)value.M12, (double)value.M22, (double)value.M13, (double)value.M23);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Matrix2x3d"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return M11.GetHashCode() + M21.GetHashCode() + M12.GetHashCode() + M22.GetHashCode() + M13.GetHashCode() + M23.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Matrix2x3d"/> object or a type capable
		/// of implicit conversion to a <see cref="Matrix2x3d"/> object, and its value
		/// is equal to the current <see cref="Matrix2x3d"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix2x3d) { return Equals((Matrix2x3d)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// matrix have the same value.
		/// </summary>
		/// <param name="other">The matrix to compare.</param>
		/// <returns>true if this matrix and value have the same value; otherwise, false.</returns>
		public bool Equals(Matrix2x3d other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Matrix2x3d left, Matrix2x3d right)
		{
			return left == right;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Matrix2x3d left, Matrix2x3d right)
		{
			return left.M11 == right.M11 & left.M21 == right.M21 & left.M12 == right.M12 & left.M22 == right.M22 & left.M13 == right.M13 & left.M23 == right.M23;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are not equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Matrix2x3d left, Matrix2x3d right)
		{
			return left.M11 != right.M11 | left.M21 != right.M21 | left.M12 != right.M12 | left.M22 != right.M22 | left.M13 != right.M13 | left.M23 != right.M23;
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
			return String.Format("[({0}, {1}, {2})({3}, {4}, {5})]", M11.ToString(format, provider), M21.ToString(format, provider), M12.ToString(format, provider), M22.ToString(format, provider), M13.ToString(format, provider), M23.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for matrix functions.
	/// </summary>
	public static partial class Matrix
	{
		#region Operations
		public static Matrix2x3d Negate(Matrix2x3d value)
		{
			return new Matrix2x3d(-value.M11, -value.M21, -value.M12, -value.M22, -value.M13, -value.M23);
		}
		public static Matrix2x3d Add(Matrix2x3d left, Matrix2x3d right)
		{
			return new Matrix2x3d(left.M11 + right.M11, left.M21 + right.M21, left.M12 + right.M12, left.M22 + right.M22, left.M13 + right.M13, left.M23 + right.M23);
		}
		public static Matrix2x3d Subtract(Matrix2x3d left, Matrix2x3d right)
		{
			return new Matrix2x3d(left.M11 - right.M11, left.M21 - right.M21, left.M12 - right.M12, left.M22 - right.M22, left.M13 - right.M13, left.M23 - right.M23);
		}
		public static Matrix2x2d Multiply(Matrix2x3d left, Matrix3x2d right)
		{
			return new Matrix2x2d(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32);
		}
		public static Matrix2x3d Multiply(Matrix2x3d left, Matrix3x3d right)
		{
			return new Matrix2x3d(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32, left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33, left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33);
		}
		public static Matrix2x4d Multiply(Matrix2x3d left, Matrix3x4d right)
		{
			return new Matrix2x4d(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32, left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33, left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33, left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34, left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34);
		}
		public static Matrix2x3d Multiply(Matrix2x3d matrix, double scalar)
		{
			return new Matrix2x3d(matrix.M11 * scalar, matrix.M21 * scalar, matrix.M12 * scalar, matrix.M22 * scalar, matrix.M13 * scalar, matrix.M23 * scalar);
		}
		public static Matrix2x3d Divide(Matrix2x3d matrix, double scalar)
		{
			return new Matrix2x3d(matrix.M11 / scalar, matrix.M21 / scalar, matrix.M12 / scalar, matrix.M22 / scalar, matrix.M13 / scalar, matrix.M23 / scalar);
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all elements of a matrix are non-zero.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>true if all elements are non-zero; false otherwise.</returns>
		public static bool All(Matrix2x3d value)
		{
			return value.M11 != 0 && value.M21 != 0 && value.M12 != 0 && value.M22 != 0 && value.M13 != 0 && value.M23 != 0;
		}
		/// <summary>
		/// Determines whether all elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if every element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Matrix2x3d value, Predicate<double> predicate)
		{
			return predicate(value.M11) && predicate(value.M21) && predicate(value.M12) && predicate(value.M22) && predicate(value.M13) && predicate(value.M23);
		}
		/// <summary>
		/// Determines whether any element of a matrix is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any elements are non-zero; false otherwise.</returns>
		public static bool Any(Matrix2x3d value)
		{
			return value.M11 != 0 || value.M21 != 0 || value.M12 != 0 || value.M22 != 0 || value.M13 != 0 || value.M23 != 0;
		}
		/// <summary>
		/// Determines whether any elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if any element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Matrix2x3d value, Predicate<double> predicate)
		{
			return predicate(value.M11) || predicate(value.M21) || predicate(value.M12) || predicate(value.M22) || predicate(value.M13) || predicate(value.M23);
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
		public static Matrix2x3d Map(Matrix2x3d value, Func<double, double> mapping)
		{
			return new Matrix2x3d(mapping(value.M11), mapping(value.M21), mapping(value.M12), mapping(value.M22), mapping(value.M13), mapping(value.M23));
		}
		/// <summary>
		/// Maps the elements of a matrix and returns the result.
		/// </summary>
		/// <param name="value">The matrix to map.</param>
		/// <param name="mapping">A mapping function to apply to each element.</param>
		/// <returns>The result of mapping each element of value.</returns>
		public static Matrix2x3f Map(Matrix2x3d value, Func<double, float> mapping)
		{
			return new Matrix2x3f(mapping(value.M11), mapping(value.M21), mapping(value.M12), mapping(value.M22), mapping(value.M13), mapping(value.M23));
		}
		#endregion
		/// <summary>
		/// Multiplys the elements of two matrices and returns the result.
		/// </summary>
		/// <param name="left">The first matrix to modulate.</param>
		/// <param name="right">The second matrix to modulate.</param>
		/// <returns>The result of multiplying each element of left by the matching element in right.</returns>
		public static Matrix2x3d Modulate(Matrix2x3d left, Matrix2x3d right)
		{
			return new Matrix2x3d(left.M11 * right.M11, left.M21 * right.M21, left.M12 * right.M12, left.M22 * right.M22, left.M13 * right.M13, left.M23 * right.M23);
		}
		/// <summary>
		/// Returns the absolute value (per element).
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The absolute value (per element) of value.</returns>
		public static Matrix2x3d Abs(Matrix2x3d value)
		{
			return new Matrix2x3d(Functions.Abs(value.M11), Functions.Abs(value.M21), Functions.Abs(value.M12), Functions.Abs(value.M22), Functions.Abs(value.M13), Functions.Abs(value.M23));
		}
		/// <summary>
		/// Returns a matrix that contains the lowest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The lowest of each element in left and the matching element in right.</returns>
		public static Matrix2x3d Min(Matrix2x3d value1, Matrix2x3d value2)
		{
			return new Matrix2x3d(Functions.Min(value1.M11, value2.M11), Functions.Min(value1.M21, value2.M21), Functions.Min(value1.M12, value2.M12), Functions.Min(value1.M22, value2.M22), Functions.Min(value1.M13, value2.M13), Functions.Min(value1.M23, value2.M23));
		}
		/// <summary>
		/// Returns a matrix that contains the highest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The highest of each element in left and the matching element in right.</returns>
		public static Matrix2x3d Max(Matrix2x3d value1, Matrix2x3d value2)
		{
			return new Matrix2x3d(Functions.Max(value1.M11, value2.M11), Functions.Max(value1.M21, value2.M21), Functions.Max(value1.M12, value2.M12), Functions.Max(value1.M22, value2.M22), Functions.Max(value1.M13, value2.M13), Functions.Max(value1.M23, value2.M23));
		}
		/// <summary>
		/// Constrains each element to a given range.
		/// </summary>
		/// <param name="value">A matrix to constrain.</param>
		/// <param name="min">The minimum values for each element.</param>
		/// <param name="max">The maximum values for each element.</param>
		/// <returns>A matrix with each element constrained to the given range.</returns>
		public static Matrix2x3d Clamp(Matrix2x3d value, Matrix2x3d min, Matrix2x3d max)
		{
			return new Matrix2x3d(Functions.Clamp(value.M11, min.M11, max.M11), Functions.Clamp(value.M21, min.M21, max.M21), Functions.Clamp(value.M12, min.M12, max.M12), Functions.Clamp(value.M22, min.M22, max.M22), Functions.Clamp(value.M13, min.M13, max.M13), Functions.Clamp(value.M23, min.M23, max.M23));
		}
		/// <summary>
		/// Constrains each element to the range 0 to 1.
		/// </summary>
		/// <param name="value">A matrix to saturate.</param>
		/// <returns>A matrix with each element constrained to the range 0 to 1.</returns>
		public static Matrix2x3d Saturate(Matrix2x3d value)
		{
			return new Matrix2x3d(Functions.Saturate(value.M11), Functions.Saturate(value.M21), Functions.Saturate(value.M12), Functions.Saturate(value.M22), Functions.Saturate(value.M13), Functions.Saturate(value.M23));
		}
		/// <summary>
		/// Returns a matrix where each element is the smallest integral value that
		/// is greater than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The ceiling of value.</returns>
		public static Matrix2x3d Ceiling(Matrix2x3d value)
		{
			return new Matrix2x3d(Functions.Ceiling(value.M11), Functions.Ceiling(value.M21), Functions.Ceiling(value.M12), Functions.Ceiling(value.M22), Functions.Ceiling(value.M13), Functions.Ceiling(value.M23));
		}
		/// <summary>
		/// Returns a matrix where each element is the largest integral value that
		/// is less than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The floor of value.</returns>
		public static Matrix2x3d Floor(Matrix2x3d value)
		{
			return new Matrix2x3d(Functions.Floor(value.M11), Functions.Floor(value.M21), Functions.Floor(value.M12), Functions.Floor(value.M22), Functions.Floor(value.M13), Functions.Floor(value.M23));
		}
		/// <summary>
		/// Returns a matrix where each element is the integral part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The integral of value.</returns>
		public static Matrix2x3d Truncate(Matrix2x3d value)
		{
			return new Matrix2x3d(Functions.Truncate(value.M11), Functions.Truncate(value.M21), Functions.Truncate(value.M12), Functions.Truncate(value.M22), Functions.Truncate(value.M13), Functions.Truncate(value.M23));
		}
		/// <summary>
		/// Returns a matrix where each element is the fractional part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The fractional of value.</returns>
		public static Matrix2x3d Fractional(Matrix2x3d value)
		{
			return new Matrix2x3d(Functions.Fractional(value.M11), Functions.Fractional(value.M21), Functions.Fractional(value.M12), Functions.Fractional(value.M22), Functions.Fractional(value.M13), Functions.Fractional(value.M23));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix2x3d Round(Matrix2x3d value)
		{
			return new Matrix2x3d(Functions.Round(value.M11), Functions.Round(value.M21), Functions.Round(value.M12), Functions.Round(value.M22), Functions.Round(value.M13), Functions.Round(value.M23));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix2x3d Round(Matrix2x3d value, int digits)
		{
			return new Matrix2x3d(Functions.Round(value.M11, digits), Functions.Round(value.M21, digits), Functions.Round(value.M12, digits), Functions.Round(value.M22, digits), Functions.Round(value.M13, digits), Functions.Round(value.M23, digits));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix2x3d Round(Matrix2x3d value, MidpointRounding mode)
		{
			return new Matrix2x3d(Functions.Round(value.M11, mode), Functions.Round(value.M21, mode), Functions.Round(value.M12, mode), Functions.Round(value.M22, mode), Functions.Round(value.M13, mode), Functions.Round(value.M23, mode));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix2x3d Round(Matrix2x3d value, int digits, MidpointRounding mode)
		{
			return new Matrix2x3d(Functions.Round(value.M11, digits, mode), Functions.Round(value.M21, digits, mode), Functions.Round(value.M12, digits, mode), Functions.Round(value.M22, digits, mode), Functions.Round(value.M13, digits, mode), Functions.Round(value.M23, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each element in the matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>A matrix with the reciprocal of each of values elements.</returns>
		public static Matrix2x3d Reciprocal(Matrix2x3d value)
		{
			return new Matrix2x3d(1 / value.M11, 1 / value.M21, 1 / value.M12, 1 / value.M22, 1 / value.M13, 1 / value.M23);
		}
		#endregion
		#region Submatrix
		/// <summary>
		/// Returns the specified submatrix of the given matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose submatrix is to returned.</param>
		/// <param name="row">The row to be removed.</param>
		/// <param name="column">The column to be removed.</param>
		public static Vector2d Submatrix(Matrix2x3d matrix, int row, int column)
		{
			if (row < 0 || row > 1)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix2x3d run from 0 to 1, inclusive.");
			if (column < 0 || column > 2)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix2x3d run from 0 to 2, inclusive.");
			if (row == 0 && column == 0)
			{
				return new Vector2d(matrix.M22, matrix.M23);
			}
			else if (row == 0 && column == 1)
			{
				return new Vector2d(matrix.M21, matrix.M23);
			}
			else if (row == 0 && column == 2)
			{
				return new Vector2d(matrix.M21, matrix.M22);
			}
			else if (row == 1 && column == 0)
			{
				return new Vector2d(matrix.M12, matrix.M13);
			}
			else if (row == 1 && column == 1)
			{
				return new Vector2d(matrix.M11, matrix.M13);
			}
			else
			{
				return new Vector2d(matrix.M11, matrix.M12);
			}
		}
		#endregion
		#region Transpose
		/// <summary>
		/// Calculates the transpose of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose transpose is to be calculated.</param>
		/// <returns>The transpose of the specified matrix.</returns>
		public static Matrix3x2d Transpose(Matrix2x3d matrix)
		{
			return new Matrix3x2d(matrix.M11, matrix.M12, matrix.M13, matrix.M21, matrix.M22, matrix.M23);
		}
		#endregion
	}
}
