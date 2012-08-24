using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a 4 by 2 matrix of doubles.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix4x2d: IEquatable<Matrix4x2d>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Matrix4x2d"/> with all of its elements equal to zero.
		/// </summary>
		public static readonly Matrix4x2d Zero = new Matrix4x2d(0);
		/// <summary>
		/// Returns a new <see cref="Matrix4x2d"/> with all of its elements equal to one.
		/// </summary>
		public static readonly Matrix4x2d One = new Matrix4x2d(1);
		#endregion
		#region Fields
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and first column.
		/// </summary>
		public readonly double M11;
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and second column.
		/// </summary>
		public readonly double M12;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and first column.
		/// </summary>
		public readonly double M21;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and second column.
		/// </summary>
		public readonly double M22;
		/// <summary>
		/// Gets the element of the matrix that exists in the third row and first column.
		/// </summary>
		public readonly double M31;
		/// <summary>
		/// Gets the element of the matrix that exists in the third row and second column.
		/// </summary>
		public readonly double M32;
		/// <summary>
		/// Gets the element of the matrix that exists in the fourth row and first column.
		/// </summary>
		public readonly double M41;
		/// <summary>
		/// Gets the element of the matrix that exists in the fourth row and second column.
		/// </summary>
		public readonly double M42;
		#endregion
		#region Properties
		public double this[int row, int column]
		{
			get
			{
				if (row < 0 || row > 3)
					throw new ArgumentOutOfRangeException("row", "Rows for Matrix4x2d run from 0 to 3, inclusive.");
				if (column < 0 || column > 1)
					throw new ArgumentOutOfRangeException("column", "Columns for Matrix4x2d run from 0 to 1, inclusive.");
				int index = column + row * 2;
				return this[index];
			}
		}
		public double this[int index]
		{
			get
			{
				if (index < 0 || index > 7)
					throw new ArgumentOutOfRangeException("index", "Indices for Matrix4x2d run from 0 to 7, inclusive.");
				switch (index)
				{
					case 0: return M11;
					case 1: return M12;
					case 2: return M21;
					case 3: return M22;
					case 4: return M31;
					case 5: return M32;
					case 6: return M41;
					case 7: return M42;
				}
				return 0;
			}
		}
		public Vector2d GetRow(int row)
		{
			if (row < 0 || row > 3)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix4x2d run from 0 to 3, inclusive.");
			switch (row)
			{
				case 0:
					return new Vector2d(M11, M12);
				case 1:
					return new Vector2d(M21, M22);
				case 2:
					return new Vector2d(M31, M32);
				case 3:
					return new Vector2d(M41, M42);
			}
			return Vector2d.Zero;
		}
		public Vector4d GetColumn(int column)
		{
			if (column < 0 || column > 1)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix4x2d run from 0 to 1, inclusive.");
			switch (column)
			{
				case 0:
					return new Vector4d(M11, M21, M31, M41);
				case 1:
					return new Vector4d(M12, M22, M32, M42);
			}
			return Vector4d.Zero;
		}
		public double[] ToArray()
		{
			return new double[]
			{
				M11, M12, M21, M22, M31, M32, M41, M42
			};
		}
		#endregion
		#region Constructors
		public Matrix4x2d(double value)
		{
			M11 = value;
			M12 = value;
			M21 = value;
			M22 = value;
			M31 = value;
			M32 = value;
			M41 = value;
			M42 = value;
		}
		public Matrix4x2d(double m11, double m12, double m21, double m22, double m31, double m32, double m41, double m42)
		{
			M11 = m11;
			M12 = m12;
			M21 = m21;
			M22 = m22;
			M31 = m31;
			M32 = m32;
			M41 = m41;
			M42 = m42;
		}
		public Matrix4x2d(Vector2d row1, Vector2d row2, Vector2d row3, Vector2d row4)
		{
			M11 = row1.X;
			M12 = row1.Y;
			M21 = row2.X;
			M22 = row2.Y;
			M31 = row3.X;
			M32 = row3.Y;
			M41 = row4.X;
			M42 = row4.Y;
		}
		#endregion
		#region Operations
		public static Matrix4x2d operator +(Matrix4x2d value)
		{
			return value;
		}
		public static Matrix4x2d operator -(Matrix4x2d value)
		{
			return Matrix.Negate(value);
		}
		public static Matrix4x2d operator +(Matrix4x2d left, Matrix4x2d right)
		{
			return Matrix.Add(left, right);
		}
		public static Matrix4x2d operator -(Matrix4x2d left, Matrix4x2d right)
		{
			return Matrix.Subtract(left, right);
		}
		public static Matrix4x2d operator *(Matrix4x2d matrix, double scalar)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix4x2d operator *(double scalar, Matrix4x2d matrix)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix4x2d operator *(Matrix4x2d left, Matrix2x2d right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix4x3d operator *(Matrix4x2d left, Matrix2x3d right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix4x4d operator *(Matrix4x2d left, Matrix2x4d right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix4x2d operator /(Matrix4x2d matrix, double scalar)
		{
			return Matrix.Divide(matrix, scalar);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a Matrix4x2f value to a Matrix4x2d.
		/// </summary>
		/// <param name="value">The value to convert to a Matrix4x2d.</param>
		/// <returns>A Matrix4x2d that has all components equal to value.</returns>
		public static implicit operator Matrix4x2d(Matrix4x2f value)
		{
			return new Matrix4x2d((double)value.M11, (double)value.M12, (double)value.M21, (double)value.M22, (double)value.M31, (double)value.M32, (double)value.M41, (double)value.M42);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Matrix4x2d"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return M11.GetHashCode() + M12.GetHashCode() + M21.GetHashCode() + M22.GetHashCode() + M31.GetHashCode() + M32.GetHashCode() + M41.GetHashCode() + M42.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Matrix4x2d"/> object or a type capable
		/// of implicit conversion to a <see cref="Matrix4x2d"/> object, and its value
		/// is equal to the current <see cref="Matrix4x2d"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix4x2d) { return Equals((Matrix4x2d)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// matrix have the same value.
		/// </summary>
		/// <param name="other">The matrix to compare.</param>
		/// <returns>true if this matrix and value have the same value; otherwise, false.</returns>
		public bool Equals(Matrix4x2d other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Matrix4x2d left, Matrix4x2d right)
		{
			return left == right;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Matrix4x2d left, Matrix4x2d right)
		{
			return left.M11 == right.M11 & left.M12 == right.M12 & left.M21 == right.M21 & left.M22 == right.M22 & left.M31 == right.M31 & left.M32 == right.M32 & left.M41 == right.M41 & left.M42 == right.M42;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are not equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Matrix4x2d left, Matrix4x2d right)
		{
			return left.M11 != right.M11 | left.M12 != right.M12 | left.M21 != right.M21 | left.M22 != right.M22 | left.M31 != right.M31 | left.M32 != right.M32 | left.M41 != right.M41 | left.M42 != right.M42;
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
			return String.Format("[({0}, {1})({2}, {3})({4}, {5})({6}, {7})]", M11.ToString(format, provider), M12.ToString(format, provider), M21.ToString(format, provider), M22.ToString(format, provider), M31.ToString(format, provider), M32.ToString(format, provider), M41.ToString(format, provider), M42.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for matrix functions.
	/// </summary>
	public static partial class Matrix
	{
		#region Operations
		public static Matrix4x2d Negate(Matrix4x2d value)
		{
			return new Matrix4x2d(-value.M11, -value.M12, -value.M21, -value.M22, -value.M31, -value.M32, -value.M41, -value.M42);
		}
		public static Matrix4x2d Add(Matrix4x2d left, Matrix4x2d right)
		{
			return new Matrix4x2d(left.M11 + right.M11, left.M12 + right.M12, left.M21 + right.M21, left.M22 + right.M22, left.M31 + right.M31, left.M32 + right.M32, left.M41 + right.M41, left.M42 + right.M42);
		}
		public static Matrix4x2d Subtract(Matrix4x2d left, Matrix4x2d right)
		{
			return new Matrix4x2d(left.M11 - right.M11, left.M12 - right.M12, left.M21 - right.M21, left.M22 - right.M22, left.M31 - right.M31, left.M32 - right.M32, left.M41 - right.M41, left.M42 - right.M42);
		}
		public static Matrix4x2d Multiply(Matrix4x2d left, Matrix2x2d right)
		{
			return new Matrix4x2d(left.M11 * right.M11 + left.M12 * right.M21, left.M11 * right.M12 + left.M12 * right.M22, left.M21 * right.M11 + left.M22 * right.M21, left.M21 * right.M12 + left.M22 * right.M22, left.M31 * right.M11 + left.M32 * right.M21, left.M31 * right.M12 + left.M32 * right.M22, left.M41 * right.M11 + left.M42 * right.M21, left.M41 * right.M12 + left.M42 * right.M22);
		}
		public static Matrix4x3d Multiply(Matrix4x2d left, Matrix2x3d right)
		{
			return new Matrix4x3d(left.M11 * right.M11 + left.M12 * right.M21, left.M11 * right.M12 + left.M12 * right.M22, left.M11 * right.M13 + left.M12 * right.M23, left.M21 * right.M11 + left.M22 * right.M21, left.M21 * right.M12 + left.M22 * right.M22, left.M21 * right.M13 + left.M22 * right.M23, left.M31 * right.M11 + left.M32 * right.M21, left.M31 * right.M12 + left.M32 * right.M22, left.M31 * right.M13 + left.M32 * right.M23, left.M41 * right.M11 + left.M42 * right.M21, left.M41 * right.M12 + left.M42 * right.M22, left.M41 * right.M13 + left.M42 * right.M23);
		}
		public static Matrix4x4d Multiply(Matrix4x2d left, Matrix2x4d right)
		{
			return new Matrix4x4d(left.M11 * right.M11 + left.M12 * right.M21, left.M11 * right.M12 + left.M12 * right.M22, left.M11 * right.M13 + left.M12 * right.M23, left.M11 * right.M14 + left.M12 * right.M24, left.M21 * right.M11 + left.M22 * right.M21, left.M21 * right.M12 + left.M22 * right.M22, left.M21 * right.M13 + left.M22 * right.M23, left.M21 * right.M14 + left.M22 * right.M24, left.M31 * right.M11 + left.M32 * right.M21, left.M31 * right.M12 + left.M32 * right.M22, left.M31 * right.M13 + left.M32 * right.M23, left.M31 * right.M14 + left.M32 * right.M24, left.M41 * right.M11 + left.M42 * right.M21, left.M41 * right.M12 + left.M42 * right.M22, left.M41 * right.M13 + left.M42 * right.M23, left.M41 * right.M14 + left.M42 * right.M24);
		}
		public static Matrix4x2d Multiply(Matrix4x2d matrix, double scalar)
		{
			return new Matrix4x2d(matrix.M11 * scalar, matrix.M12 * scalar, matrix.M21 * scalar, matrix.M22 * scalar, matrix.M31 * scalar, matrix.M32 * scalar, matrix.M41 * scalar, matrix.M42 * scalar);
		}
		public static Matrix4x2d Divide(Matrix4x2d matrix, double scalar)
		{
			return new Matrix4x2d(matrix.M11 / scalar, matrix.M12 / scalar, matrix.M21 / scalar, matrix.M22 / scalar, matrix.M31 / scalar, matrix.M32 / scalar, matrix.M41 / scalar, matrix.M42 / scalar);
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all elements of a matrix are non-zero.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>true if all elements are non-zero; false otherwise.</returns>
		public static bool All(Matrix4x2d value)
		{
			return value.M11 != 0 && value.M12 != 0 && value.M21 != 0 && value.M22 != 0 && value.M31 != 0 && value.M32 != 0 && value.M41 != 0 && value.M42 != 0;
		}
		/// <summary>
		/// Determines whether all elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if every element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Matrix4x2d value, Predicate<double> predicate)
		{
			return predicate(value.M11) && predicate(value.M12) && predicate(value.M21) && predicate(value.M22) && predicate(value.M31) && predicate(value.M32) && predicate(value.M41) && predicate(value.M42);
		}
		/// <summary>
		/// Determines whether any element of a matrix is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any elements are non-zero; false otherwise.</returns>
		public static bool Any(Matrix4x2d value)
		{
			return value.M11 != 0 || value.M12 != 0 || value.M21 != 0 || value.M22 != 0 || value.M31 != 0 || value.M32 != 0 || value.M41 != 0 || value.M42 != 0;
		}
		/// <summary>
		/// Determines whether any elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if any element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Matrix4x2d value, Predicate<double> predicate)
		{
			return predicate(value.M11) || predicate(value.M12) || predicate(value.M21) || predicate(value.M22) || predicate(value.M31) || predicate(value.M32) || predicate(value.M41) || predicate(value.M42);
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
		public static Matrix4x2d Transform(Matrix4x2d value, Func<double, double> transformer)
		{
			return new Matrix4x2d(transformer(value.M11), transformer(value.M12), transformer(value.M21), transformer(value.M22), transformer(value.M31), transformer(value.M32), transformer(value.M41), transformer(value.M42));
		}
		/// <summary>
		/// Transforms the elements of a matrix and returns the result.
		/// </summary>
		/// <param name="value">The matrix to transform.</param>
		/// <param name="transformer">A transform function to apply to each element.</param>
		/// <returns>The result of transforming each element of value.</returns>
		public static Matrix4x2f Transform(Matrix4x2d value, Func<double, float> transformer)
		{
			return new Matrix4x2f(transformer(value.M11), transformer(value.M12), transformer(value.M21), transformer(value.M22), transformer(value.M31), transformer(value.M32), transformer(value.M41), transformer(value.M42));
		}
		#endregion
		/// <summary>
		/// Multiplys the elements of two matrices and returns the result.
		/// </summary>
		/// <param name="left">The first matrix to modulate.</param>
		/// <param name="right">The second matrix to modulate.</param>
		/// <returns>The result of multiplying each element of left by the matching element in right.</returns>
		public static Matrix4x2d Modulate(Matrix4x2d left, Matrix4x2d right)
		{
			return new Matrix4x2d(left.M11 * right.M11, left.M12 * right.M12, left.M21 * right.M21, left.M22 * right.M22, left.M31 * right.M31, left.M32 * right.M32, left.M41 * right.M41, left.M42 * right.M42);
		}
		/// <summary>
		/// Returns the absolute value (per element).
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The absolute value (per element) of value.</returns>
		public static Matrix4x2d Abs(Matrix4x2d value)
		{
			return new Matrix4x2d(Functions.Abs(value.M11), Functions.Abs(value.M12), Functions.Abs(value.M21), Functions.Abs(value.M22), Functions.Abs(value.M31), Functions.Abs(value.M32), Functions.Abs(value.M41), Functions.Abs(value.M42));
		}
		/// <summary>
		/// Returns a matrix that contains the lowest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The lowest of each element in left and the matching element in right.</returns>
		public static Matrix4x2d Min(Matrix4x2d value1, Matrix4x2d value2)
		{
			return new Matrix4x2d(Functions.Min(value1.M11, value2.M11), Functions.Min(value1.M12, value2.M12), Functions.Min(value1.M21, value2.M21), Functions.Min(value1.M22, value2.M22), Functions.Min(value1.M31, value2.M31), Functions.Min(value1.M32, value2.M32), Functions.Min(value1.M41, value2.M41), Functions.Min(value1.M42, value2.M42));
		}
		/// <summary>
		/// Returns a matrix that contains the highest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The highest of each element in left and the matching element in right.</returns>
		public static Matrix4x2d Max(Matrix4x2d value1, Matrix4x2d value2)
		{
			return new Matrix4x2d(Functions.Max(value1.M11, value2.M11), Functions.Max(value1.M12, value2.M12), Functions.Max(value1.M21, value2.M21), Functions.Max(value1.M22, value2.M22), Functions.Max(value1.M31, value2.M31), Functions.Max(value1.M32, value2.M32), Functions.Max(value1.M41, value2.M41), Functions.Max(value1.M42, value2.M42));
		}
		/// <summary>
		/// Constrains each element to a given range.
		/// </summary>
		/// <param name="value">A matrix to constrain.</param>
		/// <param name="min">The minimum values for each element.</param>
		/// <param name="max">The maximum values for each element.</param>
		/// <returns>A matrix with each element constrained to the given range.</returns>
		public static Matrix4x2d Clamp(Matrix4x2d value, Matrix4x2d min, Matrix4x2d max)
		{
			return new Matrix4x2d(Functions.Clamp(value.M11, min.M11, max.M11), Functions.Clamp(value.M12, min.M12, max.M12), Functions.Clamp(value.M21, min.M21, max.M21), Functions.Clamp(value.M22, min.M22, max.M22), Functions.Clamp(value.M31, min.M31, max.M31), Functions.Clamp(value.M32, min.M32, max.M32), Functions.Clamp(value.M41, min.M41, max.M41), Functions.Clamp(value.M42, min.M42, max.M42));
		}
		/// <summary>
		/// Constrains each element to the range 0 to 1.
		/// </summary>
		/// <param name="value">A matrix to saturate.</param>
		/// <returns>A matrix with each element constrained to the range 0 to 1.</returns>
		public static Matrix4x2d Saturate(Matrix4x2d value)
		{
			return new Matrix4x2d(Functions.Saturate(value.M11), Functions.Saturate(value.M12), Functions.Saturate(value.M21), Functions.Saturate(value.M22), Functions.Saturate(value.M31), Functions.Saturate(value.M32), Functions.Saturate(value.M41), Functions.Saturate(value.M42));
		}
		/// <summary>
		/// Returns a matrix where each element is the smallest integral value that
		/// is greater than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The ceiling of value.</returns>
		public static Matrix4x2d Ceiling(Matrix4x2d value)
		{
			return new Matrix4x2d(Functions.Ceiling(value.M11), Functions.Ceiling(value.M12), Functions.Ceiling(value.M21), Functions.Ceiling(value.M22), Functions.Ceiling(value.M31), Functions.Ceiling(value.M32), Functions.Ceiling(value.M41), Functions.Ceiling(value.M42));
		}
		/// <summary>
		/// Returns a matrix where each element is the largest integral value that
		/// is less than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The floor of value.</returns>
		public static Matrix4x2d Floor(Matrix4x2d value)
		{
			return new Matrix4x2d(Functions.Floor(value.M11), Functions.Floor(value.M12), Functions.Floor(value.M21), Functions.Floor(value.M22), Functions.Floor(value.M31), Functions.Floor(value.M32), Functions.Floor(value.M41), Functions.Floor(value.M42));
		}
		/// <summary>
		/// Returns a matrix where each element is the integral part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The integral of value.</returns>
		public static Matrix4x2d Truncate(Matrix4x2d value)
		{
			return new Matrix4x2d(Functions.Truncate(value.M11), Functions.Truncate(value.M12), Functions.Truncate(value.M21), Functions.Truncate(value.M22), Functions.Truncate(value.M31), Functions.Truncate(value.M32), Functions.Truncate(value.M41), Functions.Truncate(value.M42));
		}
		/// <summary>
		/// Returns a matrix where each element is the fractional part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The fractional of value.</returns>
		public static Matrix4x2d Fractional(Matrix4x2d value)
		{
			return new Matrix4x2d(Functions.Fractional(value.M11), Functions.Fractional(value.M12), Functions.Fractional(value.M21), Functions.Fractional(value.M22), Functions.Fractional(value.M31), Functions.Fractional(value.M32), Functions.Fractional(value.M41), Functions.Fractional(value.M42));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix4x2d Round(Matrix4x2d value)
		{
			return new Matrix4x2d(Functions.Round(value.M11), Functions.Round(value.M12), Functions.Round(value.M21), Functions.Round(value.M22), Functions.Round(value.M31), Functions.Round(value.M32), Functions.Round(value.M41), Functions.Round(value.M42));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix4x2d Round(Matrix4x2d value, int digits)
		{
			return new Matrix4x2d(Functions.Round(value.M11, digits), Functions.Round(value.M12, digits), Functions.Round(value.M21, digits), Functions.Round(value.M22, digits), Functions.Round(value.M31, digits), Functions.Round(value.M32, digits), Functions.Round(value.M41, digits), Functions.Round(value.M42, digits));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix4x2d Round(Matrix4x2d value, MidpointRounding mode)
		{
			return new Matrix4x2d(Functions.Round(value.M11, mode), Functions.Round(value.M12, mode), Functions.Round(value.M21, mode), Functions.Round(value.M22, mode), Functions.Round(value.M31, mode), Functions.Round(value.M32, mode), Functions.Round(value.M41, mode), Functions.Round(value.M42, mode));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix4x2d Round(Matrix4x2d value, int digits, MidpointRounding mode)
		{
			return new Matrix4x2d(Functions.Round(value.M11, digits, mode), Functions.Round(value.M12, digits, mode), Functions.Round(value.M21, digits, mode), Functions.Round(value.M22, digits, mode), Functions.Round(value.M31, digits, mode), Functions.Round(value.M32, digits, mode), Functions.Round(value.M41, digits, mode), Functions.Round(value.M42, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each element in the matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>A matrix with the reciprocal of each of values elements.</returns>
		public static Matrix4x2d Reciprocal(Matrix4x2d value)
		{
			return new Matrix4x2d(1 / value.M11, 1 / value.M12, 1 / value.M21, 1 / value.M22, 1 / value.M31, 1 / value.M32, 1 / value.M41, 1 / value.M42);
		}
		#endregion
		#region Submatrix
		/// <summary>
		/// Returns the specified submatrix of the given matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose submatrix is to returned.</param>
		/// <param name="row">The row to be removed.</param>
		/// <param name="column">The column to be removed.</param>
		public static Vector3d Submatrix(Matrix4x2d matrix, int row, int column)
		{
			if (row < 0 || row > 3)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix4x2d run from 0 to 3, inclusive.");
			if (column < 0 || column > 1)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix4x2d run from 0 to 1, inclusive.");
			if (row == 0 && column == 0)
			{
				return new Vector3d(matrix.M22, matrix.M32, matrix.M42);
			}
			else if (row == 0 && column == 1)
			{
				return new Vector3d(matrix.M21, matrix.M31, matrix.M41);
			}
			else if (row == 1 && column == 0)
			{
				return new Vector3d(matrix.M12, matrix.M32, matrix.M42);
			}
			else if (row == 1 && column == 1)
			{
				return new Vector3d(matrix.M11, matrix.M31, matrix.M41);
			}
			else if (row == 2 && column == 0)
			{
				return new Vector3d(matrix.M12, matrix.M22, matrix.M42);
			}
			else if (row == 2 && column == 1)
			{
				return new Vector3d(matrix.M11, matrix.M21, matrix.M41);
			}
			else if (row == 3 && column == 0)
			{
				return new Vector3d(matrix.M12, matrix.M22, matrix.M32);
			}
			else
			{
				return new Vector3d(matrix.M11, matrix.M21, matrix.M31);
			}
		}
		#endregion
		#region Transpose
		/// <summary>
		/// Calculates the transpose of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose transpose is to be calculated.</param>
		/// <returns>The transpose of the specified matrix.</returns>
		public static Matrix2x4d Transpose(Matrix4x2d matrix)
		{
			return new Matrix2x4d(matrix.M11, matrix.M21, matrix.M31, matrix.M41, matrix.M12, matrix.M22, matrix.M32, matrix.M42);
		}
		#endregion
	}
}
