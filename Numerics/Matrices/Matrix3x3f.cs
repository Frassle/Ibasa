using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics
{
	/// <summary>
	/// Represents a 3 by 3 matrix of floats.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix3x3f: IEquatable<Matrix3x3f>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Matrix3x3f"/> with all of its elements equal to zero.
		/// </summary>
		public static readonly Matrix3x3f Zero = new Matrix3x3f(0);
		/// <summary>
		/// Returns a new <see cref="Matrix3x3f"/> with all of its elements equal to one.
		/// </summary>
		public static readonly Matrix3x3f One = new Matrix3x3f(1);
		/// <summary>
		/// Returns the identity <see cref="Matrix3x3f"/>.
		/// </summary>
		public static readonly Matrix3x3f Identity = new Matrix3x3f(1, 0, 0, 0, 1, 0, 0, 0, 1);
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
		#endregion
		#region Properties
		public float this[int row, int column]
		{
			get
			{
				if (row < 0 || row > 2)
					throw new ArgumentOutOfRangeException("row", "Rows for Matrix3x3f run from 0 to 2, inclusive.");
				if (column < 0 || column > 2)
					throw new ArgumentOutOfRangeException("column", "Columns for Matrix3x3f run from 0 to 2, inclusive.");
				int index = row + column * 3;
				return this[index];
			}
		}
		public float this[int index]
		{
			get
			{
				if (index < 0 || index > 8)
					throw new ArgumentOutOfRangeException("index", "Indices for Matrix3x3f run from 0 to 8, inclusive.");
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
				}
				return 0;
			}
		}
		public Vector3f GetRow(int row)
		{
			if (row < 0 || row > 2)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix3x3f run from 0 to 2, inclusive.");
			switch (row)
			{
				case 0:
					return new Vector3f(M11, M12, M13);
				case 1:
					return new Vector3f(M21, M22, M23);
				case 2:
					return new Vector3f(M31, M32, M33);
			}
			return Vector3f.Zero;
		}
		public Vector3f GetColumn(int column)
		{
			if (column < 0 || column > 2)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix3x3f run from 0 to 2, inclusive.");
			switch (column)
			{
				case 0:
					return new Vector3f(M11, M21, M31);
				case 1:
					return new Vector3f(M12, M22, M32);
				case 2:
					return new Vector3f(M13, M23, M33);
			}
			return Vector3f.Zero;
		}
		public float[] ToArray()
		{
			return new float[]
			{
				M11, M21, M31, M12, M22, M32, M13, M23, M33
			};
		}
		#endregion
		#region Constructors
		public Matrix3x3f(float value)
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
		}
		public Matrix3x3f(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
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
		}
		#endregion
		#region Transform
		#region Rotation
		/// <summary>
		/// Creates a matrix that rotates around the x-axis.
		/// </summary>
		/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
		/// <returns>The created rotation matrix.</returns>
		public static Matrix3x3f RotationX(float angle)
		{
			var cos = Functions.Cos(angle);
			var sin = Functions.Sin(angle);
			return new Matrix3x3f(
				1, 0, 0,
				0, cos, sin,
				0, -sin, cos
			);
		}
		/// <summary>
		/// Creates a matrix that rotates around the y-axis.
		/// </summary>
		/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
		/// <returns>The created rotation matrix.</returns>
		public static Matrix3x3f RotationY(float angle)
		{
			var cos = Functions.Cos(angle);
			var sin = Functions.Sin(angle);
			return new Matrix3x3f(
				cos, 0, -sin,
				0, 1, 0,
				sin, 0, cos
			);
		}
		/// <summary>
		/// Creates a matrix that rotates around the z-axis.
		/// </summary>
		/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
		/// <returns>The created rotation matrix.</returns>
		public static Matrix3x3f RotationZ(float angle)
		{
			var cos = Functions.Cos(angle);
			var sin = Functions.Sin(angle);
			return new Matrix3x3f(
				cos, sin, 0,
				-sin, cos, 0,
				0, 0, 1
			);
		}
		#endregion
		#region Scaling
		/// <summary>
		/// Creates a matrix that scales along the x-axis, y-axis and z-axis.
		/// </summary>
		/// <param name="x">Scaling factor that is applied along the x-axis.</param>
		/// <param name="y">Scaling factor that is applied along the y-axis.</param>
		/// <param name="z">Scaling factor that is applied along the z-axis.</param>
		/// <returns>The created scaling matrix.</returns>
		public static Matrix3x3f Scaling(float x, float y, float z)
		{
			return new Matrix3x3f(
				x, 0, 0,
				0, y, 0,
				0, 0, z
			);
		}
		#endregion
		#region Translation
		/// <summary>
		/// Creates a matrix that translates along the x-axis and y-axis.
		/// </summary>
		/// <param name="x">Translation along the x-axis.</param>
		/// <param name="y">Translation along the y-axis.</param>
		/// <returns>The created translation matrix.</returns>
		public static Matrix3x3f Translation(float x, float y)
		{
			return new Matrix3x3f(
				1, 0, x,
				0, 1, y,
				0, 0, 1
			);
		}
		#endregion
		#endregion
		#region Operations
		public static Matrix3x3f operator +(Matrix3x3f value)
		{
			return value;
		}
		public static Matrix3x3f operator -(Matrix3x3f value)
		{
			return Matrix.Negate(value);
		}
		public static Matrix3x3f operator +(Matrix3x3f left, Matrix3x3f right)
		{
			return Matrix.Add(left, right);
		}
		public static Matrix3x3f operator -(Matrix3x3f left, Matrix3x3f right)
		{
			return Matrix.Subtract(left, right);
		}
		public static Matrix3x3f operator *(Matrix3x3f matrix, float scalar)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix3x3f operator *(float scalar, Matrix3x3f matrix)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix3x2f operator *(Matrix3x3f left, Matrix3x2f right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix3x3f operator *(Matrix3x3f left, Matrix3x3f right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix3x4f operator *(Matrix3x3f left, Matrix3x4f right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix3x3f operator /(Matrix3x3f matrix, float scalar)
		{
			return Matrix.Divide(matrix, scalar);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Matrix3x3d value to a Matrix3x3f.
		/// </summary>
		/// <param name="value">The value to convert to a Matrix3x3f.</param>
		/// <returns>A Matrix3x3f that has all components equal to value.</returns>
		public static explicit operator Matrix3x3f(Matrix3x3d value)
		{
			return new Matrix3x3f((float)value.M11, (float)value.M21, (float)value.M31, (float)value.M12, (float)value.M22, (float)value.M32, (float)value.M13, (float)value.M23, (float)value.M33);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Matrix3x3f"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return M11.GetHashCode() + M21.GetHashCode() + M31.GetHashCode() + M12.GetHashCode() + M22.GetHashCode() + M32.GetHashCode() + M13.GetHashCode() + M23.GetHashCode() + M33.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Matrix3x3f"/> object or a type capable
		/// of implicit conversion to a <see cref="Matrix3x3f"/> object, and its value
		/// is equal to the current <see cref="Matrix3x3f"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix3x3f) { return Equals((Matrix3x3f)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// matrix have the same value.
		/// </summary>
		/// <param name="other">The matrix to compare.</param>
		/// <returns>true if this matrix and value have the same value; otherwise, false.</returns>
		public bool Equals(Matrix3x3f other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Matrix3x3f left, Matrix3x3f right)
		{
			return left == right;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Matrix3x3f left, Matrix3x3f right)
		{
			return left.M11 == right.M11 & left.M21 == right.M21 & left.M31 == right.M31 & left.M12 == right.M12 & left.M22 == right.M22 & left.M32 == right.M32 & left.M13 == right.M13 & left.M23 == right.M23 & left.M33 == right.M33;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are not equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Matrix3x3f left, Matrix3x3f right)
		{
			return left.M11 != right.M11 | left.M21 != right.M21 | left.M31 != right.M31 | left.M12 != right.M12 | left.M22 != right.M22 | left.M32 != right.M32 | left.M13 != right.M13 | left.M23 != right.M23 | left.M33 != right.M33;
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
			return String.Format("[({0}, {1}, {2})({3}, {4}, {5})({6}, {7}, {8})]", M11.ToString(format, provider), M21.ToString(format, provider), M31.ToString(format, provider), M12.ToString(format, provider), M22.ToString(format, provider), M32.ToString(format, provider), M13.ToString(format, provider), M23.ToString(format, provider), M33.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for matrix functions.
	/// </summary>
	public static partial class Matrix
	{
		#region Operations
		public static Matrix3x3f Negate(Matrix3x3f value)
		{
			return new Matrix3x3f(-value.M11, -value.M21, -value.M31, -value.M12, -value.M22, -value.M32, -value.M13, -value.M23, -value.M33);
		}
		public static Matrix3x3f Add(Matrix3x3f left, Matrix3x3f right)
		{
			return new Matrix3x3f(left.M11 + right.M11, left.M21 + right.M21, left.M31 + right.M31, left.M12 + right.M12, left.M22 + right.M22, left.M32 + right.M32, left.M13 + right.M13, left.M23 + right.M23, left.M33 + right.M33);
		}
		public static Matrix3x3f Subtract(Matrix3x3f left, Matrix3x3f right)
		{
			return new Matrix3x3f(left.M11 - right.M11, left.M21 - right.M21, left.M31 - right.M31, left.M12 - right.M12, left.M22 - right.M22, left.M32 - right.M32, left.M13 - right.M13, left.M23 - right.M23, left.M33 - right.M33);
		}
		public static Matrix3x2f Multiply(Matrix3x3f left, Matrix3x2f right)
		{
			return new Matrix3x2f(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31, left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32, left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32);
		}
		public static Matrix3x3f Multiply(Matrix3x3f left, Matrix3x3f right)
		{
			return new Matrix3x3f(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31, left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32, left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32, left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33, left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33, left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33);
		}
		public static Matrix3x4f Multiply(Matrix3x3f left, Matrix3x4f right)
		{
			return new Matrix3x4f(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31, left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32, left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32, left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33, left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33, left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33, left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34, left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34, left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34);
		}
		public static Matrix3x3f Multiply(Matrix3x3f matrix, float scalar)
		{
			return new Matrix3x3f(matrix.M11 * scalar, matrix.M21 * scalar, matrix.M31 * scalar, matrix.M12 * scalar, matrix.M22 * scalar, matrix.M32 * scalar, matrix.M13 * scalar, matrix.M23 * scalar, matrix.M33 * scalar);
		}
		public static Matrix3x3f Divide(Matrix3x3f matrix, float scalar)
		{
			return new Matrix3x3f(matrix.M11 / scalar, matrix.M21 / scalar, matrix.M31 / scalar, matrix.M12 / scalar, matrix.M22 / scalar, matrix.M32 / scalar, matrix.M13 / scalar, matrix.M23 / scalar, matrix.M33 / scalar);
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all elements of a matrix are non-zero.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>true if all elements are non-zero; false otherwise.</returns>
		public static bool All(Matrix3x3f value)
		{
			return value.M11 != 0 && value.M21 != 0 && value.M31 != 0 && value.M12 != 0 && value.M22 != 0 && value.M32 != 0 && value.M13 != 0 && value.M23 != 0 && value.M33 != 0;
		}
		/// <summary>
		/// Determines whether all elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if every element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Matrix3x3f value, Predicate<float> predicate)
		{
			return predicate(value.M11) && predicate(value.M21) && predicate(value.M31) && predicate(value.M12) && predicate(value.M22) && predicate(value.M32) && predicate(value.M13) && predicate(value.M23) && predicate(value.M33);
		}
		/// <summary>
		/// Determines whether any element of a matrix is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any elements are non-zero; false otherwise.</returns>
		public static bool Any(Matrix3x3f value)
		{
			return value.M11 != 0 || value.M21 != 0 || value.M31 != 0 || value.M12 != 0 || value.M22 != 0 || value.M32 != 0 || value.M13 != 0 || value.M23 != 0 || value.M33 != 0;
		}
		/// <summary>
		/// Determines whether any elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if any element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Matrix3x3f value, Predicate<float> predicate)
		{
			return predicate(value.M11) || predicate(value.M21) || predicate(value.M31) || predicate(value.M12) || predicate(value.M22) || predicate(value.M32) || predicate(value.M13) || predicate(value.M23) || predicate(value.M33);
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
		public static Matrix3x3d Map(Matrix3x3f value, Func<float, double> mapping)
		{
			return new Matrix3x3d(mapping(value.M11), mapping(value.M21), mapping(value.M31), mapping(value.M12), mapping(value.M22), mapping(value.M32), mapping(value.M13), mapping(value.M23), mapping(value.M33));
		}
		/// <summary>
		/// Maps the elements of a matrix and returns the result.
		/// </summary>
		/// <param name="value">The matrix to map.</param>
		/// <param name="mapping">A mapping function to apply to each element.</param>
		/// <returns>The result of mapping each element of value.</returns>
		public static Matrix3x3f Map(Matrix3x3f value, Func<float, float> mapping)
		{
			return new Matrix3x3f(mapping(value.M11), mapping(value.M21), mapping(value.M31), mapping(value.M12), mapping(value.M22), mapping(value.M32), mapping(value.M13), mapping(value.M23), mapping(value.M33));
		}
		#endregion
		/// <summary>
		/// Multiplys the elements of two matrices and returns the result.
		/// </summary>
		/// <param name="left">The first matrix to modulate.</param>
		/// <param name="right">The second matrix to modulate.</param>
		/// <returns>The result of multiplying each element of left by the matching element in right.</returns>
		public static Matrix3x3f Modulate(Matrix3x3f left, Matrix3x3f right)
		{
			return new Matrix3x3f(left.M11 * right.M11, left.M21 * right.M21, left.M31 * right.M31, left.M12 * right.M12, left.M22 * right.M22, left.M32 * right.M32, left.M13 * right.M13, left.M23 * right.M23, left.M33 * right.M33);
		}
		/// <summary>
		/// Returns the absolute value (per element).
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The absolute value (per element) of value.</returns>
		public static Matrix3x3f Abs(Matrix3x3f value)
		{
			return new Matrix3x3f(Functions.Abs(value.M11), Functions.Abs(value.M21), Functions.Abs(value.M31), Functions.Abs(value.M12), Functions.Abs(value.M22), Functions.Abs(value.M32), Functions.Abs(value.M13), Functions.Abs(value.M23), Functions.Abs(value.M33));
		}
		/// <summary>
		/// Returns a matrix that contains the lowest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The lowest of each element in left and the matching element in right.</returns>
		public static Matrix3x3f Min(Matrix3x3f value1, Matrix3x3f value2)
		{
			return new Matrix3x3f(Functions.Min(value1.M11, value2.M11), Functions.Min(value1.M21, value2.M21), Functions.Min(value1.M31, value2.M31), Functions.Min(value1.M12, value2.M12), Functions.Min(value1.M22, value2.M22), Functions.Min(value1.M32, value2.M32), Functions.Min(value1.M13, value2.M13), Functions.Min(value1.M23, value2.M23), Functions.Min(value1.M33, value2.M33));
		}
		/// <summary>
		/// Returns a matrix that contains the highest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The highest of each element in left and the matching element in right.</returns>
		public static Matrix3x3f Max(Matrix3x3f value1, Matrix3x3f value2)
		{
			return new Matrix3x3f(Functions.Max(value1.M11, value2.M11), Functions.Max(value1.M21, value2.M21), Functions.Max(value1.M31, value2.M31), Functions.Max(value1.M12, value2.M12), Functions.Max(value1.M22, value2.M22), Functions.Max(value1.M32, value2.M32), Functions.Max(value1.M13, value2.M13), Functions.Max(value1.M23, value2.M23), Functions.Max(value1.M33, value2.M33));
		}
		/// <summary>
		/// Constrains each element to a given range.
		/// </summary>
		/// <param name="value">A matrix to constrain.</param>
		/// <param name="min">The minimum values for each element.</param>
		/// <param name="max">The maximum values for each element.</param>
		/// <returns>A matrix with each element constrained to the given range.</returns>
		public static Matrix3x3f Clamp(Matrix3x3f value, Matrix3x3f min, Matrix3x3f max)
		{
			return new Matrix3x3f(Functions.Clamp(value.M11, min.M11, max.M11), Functions.Clamp(value.M21, min.M21, max.M21), Functions.Clamp(value.M31, min.M31, max.M31), Functions.Clamp(value.M12, min.M12, max.M12), Functions.Clamp(value.M22, min.M22, max.M22), Functions.Clamp(value.M32, min.M32, max.M32), Functions.Clamp(value.M13, min.M13, max.M13), Functions.Clamp(value.M23, min.M23, max.M23), Functions.Clamp(value.M33, min.M33, max.M33));
		}
		/// <summary>
		/// Constrains each element to the range 0 to 1.
		/// </summary>
		/// <param name="value">A matrix to saturate.</param>
		/// <returns>A matrix with each element constrained to the range 0 to 1.</returns>
		public static Matrix3x3f Saturate(Matrix3x3f value)
		{
			return new Matrix3x3f(Functions.Saturate(value.M11), Functions.Saturate(value.M21), Functions.Saturate(value.M31), Functions.Saturate(value.M12), Functions.Saturate(value.M22), Functions.Saturate(value.M32), Functions.Saturate(value.M13), Functions.Saturate(value.M23), Functions.Saturate(value.M33));
		}
		/// <summary>
		/// Returns a matrix where each element is the smallest integral value that
		/// is greater than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The ceiling of value.</returns>
		public static Matrix3x3f Ceiling(Matrix3x3f value)
		{
			return new Matrix3x3f(Functions.Ceiling(value.M11), Functions.Ceiling(value.M21), Functions.Ceiling(value.M31), Functions.Ceiling(value.M12), Functions.Ceiling(value.M22), Functions.Ceiling(value.M32), Functions.Ceiling(value.M13), Functions.Ceiling(value.M23), Functions.Ceiling(value.M33));
		}
		/// <summary>
		/// Returns a matrix where each element is the largest integral value that
		/// is less than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The floor of value.</returns>
		public static Matrix3x3f Floor(Matrix3x3f value)
		{
			return new Matrix3x3f(Functions.Floor(value.M11), Functions.Floor(value.M21), Functions.Floor(value.M31), Functions.Floor(value.M12), Functions.Floor(value.M22), Functions.Floor(value.M32), Functions.Floor(value.M13), Functions.Floor(value.M23), Functions.Floor(value.M33));
		}
		/// <summary>
		/// Returns a matrix where each element is the integral part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The integral of value.</returns>
		public static Matrix3x3f Truncate(Matrix3x3f value)
		{
			return new Matrix3x3f(Functions.Truncate(value.M11), Functions.Truncate(value.M21), Functions.Truncate(value.M31), Functions.Truncate(value.M12), Functions.Truncate(value.M22), Functions.Truncate(value.M32), Functions.Truncate(value.M13), Functions.Truncate(value.M23), Functions.Truncate(value.M33));
		}
		/// <summary>
		/// Returns a matrix where each element is the fractional part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The fractional of value.</returns>
		public static Matrix3x3f Fractional(Matrix3x3f value)
		{
			return new Matrix3x3f(Functions.Fractional(value.M11), Functions.Fractional(value.M21), Functions.Fractional(value.M31), Functions.Fractional(value.M12), Functions.Fractional(value.M22), Functions.Fractional(value.M32), Functions.Fractional(value.M13), Functions.Fractional(value.M23), Functions.Fractional(value.M33));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix3x3f Round(Matrix3x3f value)
		{
			return new Matrix3x3f(Functions.Round(value.M11), Functions.Round(value.M21), Functions.Round(value.M31), Functions.Round(value.M12), Functions.Round(value.M22), Functions.Round(value.M32), Functions.Round(value.M13), Functions.Round(value.M23), Functions.Round(value.M33));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix3x3f Round(Matrix3x3f value, int digits)
		{
			return new Matrix3x3f(Functions.Round(value.M11, digits), Functions.Round(value.M21, digits), Functions.Round(value.M31, digits), Functions.Round(value.M12, digits), Functions.Round(value.M22, digits), Functions.Round(value.M32, digits), Functions.Round(value.M13, digits), Functions.Round(value.M23, digits), Functions.Round(value.M33, digits));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix3x3f Round(Matrix3x3f value, MidpointRounding mode)
		{
			return new Matrix3x3f(Functions.Round(value.M11, mode), Functions.Round(value.M21, mode), Functions.Round(value.M31, mode), Functions.Round(value.M12, mode), Functions.Round(value.M22, mode), Functions.Round(value.M32, mode), Functions.Round(value.M13, mode), Functions.Round(value.M23, mode), Functions.Round(value.M33, mode));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix3x3f Round(Matrix3x3f value, int digits, MidpointRounding mode)
		{
			return new Matrix3x3f(Functions.Round(value.M11, digits, mode), Functions.Round(value.M21, digits, mode), Functions.Round(value.M31, digits, mode), Functions.Round(value.M12, digits, mode), Functions.Round(value.M22, digits, mode), Functions.Round(value.M32, digits, mode), Functions.Round(value.M13, digits, mode), Functions.Round(value.M23, digits, mode), Functions.Round(value.M33, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each element in the matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>A matrix with the reciprocal of each of values elements.</returns>
		public static Matrix3x3f Reciprocal(Matrix3x3f value)
		{
			return new Matrix3x3f(1 / value.M11, 1 / value.M21, 1 / value.M31, 1 / value.M12, 1 / value.M22, 1 / value.M32, 1 / value.M13, 1 / value.M23, 1 / value.M33);
		}
		#endregion
		#region Submatrix
		/// <summary>
		/// Returns the specified submatrix of the given matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose submatrix is to returned.</param>
		/// <param name="row">The row to be removed.</param>
		/// <param name="column">The column to be removed.</param>
		public static Matrix2x2f Submatrix(Matrix3x3f matrix, int row, int column)
		{
			if (row < 0 || row > 2)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix3x3f run from 0 to 2, inclusive.");
			if (column < 0 || column > 2)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix3x3f run from 0 to 2, inclusive.");
			if (row == 0 && column == 0)
			{
				return new Matrix2x2f(matrix.M22, matrix.M32, matrix.M23, matrix.M33);
			}
			else if (row == 0 && column == 1)
			{
				return new Matrix2x2f(matrix.M21, matrix.M31, matrix.M23, matrix.M33);
			}
			else if (row == 0 && column == 2)
			{
				return new Matrix2x2f(matrix.M21, matrix.M31, matrix.M22, matrix.M32);
			}
			else if (row == 1 && column == 0)
			{
				return new Matrix2x2f(matrix.M12, matrix.M32, matrix.M13, matrix.M33);
			}
			else if (row == 1 && column == 1)
			{
				return new Matrix2x2f(matrix.M11, matrix.M31, matrix.M13, matrix.M33);
			}
			else if (row == 1 && column == 2)
			{
				return new Matrix2x2f(matrix.M11, matrix.M31, matrix.M12, matrix.M32);
			}
			else if (row == 2 && column == 0)
			{
				return new Matrix2x2f(matrix.M12, matrix.M22, matrix.M13, matrix.M23);
			}
			else if (row == 2 && column == 1)
			{
				return new Matrix2x2f(matrix.M11, matrix.M21, matrix.M13, matrix.M23);
			}
			else
			{
				return new Matrix2x2f(matrix.M11, matrix.M21, matrix.M12, matrix.M22);
			}
		}
		#endregion
		#region Invert, Determinant
		/// <summary>
		/// Calculates the inverse of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose inverse is to be calculated.</param>
		/// <param name="determinant">When the method completes, contains the determinant of the matrix.</param>
		public static Matrix3x3f Invert(Matrix3x3f matrix, out float determinant)
		{
			var cofactor00 = Determinant(new Matrix2x2f(matrix.M22, matrix.M32, matrix.M23, matrix.M33));
			var cofactor10 = -Determinant(new Matrix2x2f(matrix.M21, matrix.M31, matrix.M23, matrix.M33));
			var cofactor20 = Determinant(new Matrix2x2f(matrix.M21, matrix.M31, matrix.M22, matrix.M32));
			var cofactor01 = -Determinant(new Matrix2x2f(matrix.M12, matrix.M32, matrix.M13, matrix.M33));
			var cofactor11 = Determinant(new Matrix2x2f(matrix.M11, matrix.M31, matrix.M13, matrix.M33));
			var cofactor21 = -Determinant(new Matrix2x2f(matrix.M11, matrix.M31, matrix.M12, matrix.M32));
			var cofactor02 = Determinant(new Matrix2x2f(matrix.M12, matrix.M22, matrix.M13, matrix.M23));
			var cofactor12 = -Determinant(new Matrix2x2f(matrix.M11, matrix.M21, matrix.M13, matrix.M23));
			var cofactor22 = Determinant(new Matrix2x2f(matrix.M11, matrix.M21, matrix.M12, matrix.M22));
			determinant = matrix.M11 * cofactor00 + matrix.M21 * cofactor10 + matrix.M31 * cofactor20;
			return new Matrix3x3f(cofactor00, cofactor01, cofactor02, cofactor10, cofactor11, cofactor12, cofactor20, cofactor21, cofactor22) / determinant;
		}
		/// <summary>
		/// Calculates the inverse of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose inverse is to be calculated.</param>
		/// <param name="determinant">When the method completes, contains the determinant of the matrix.</param>
		public static Matrix3x3f Invert(Matrix3x3f matrix)
		{
			float determinant;
			return Invert(matrix, out determinant);
		}
		/// <summary>
		/// Calculates the determinant of the matrix.
		/// </summary>
		/// <returns>The determinant of the matrix.</returns>
		public static float Determinant(Matrix3x3f matrix)
		{
			var minor0 = Determinant(new Matrix2x2f(matrix.M22, matrix.M32, matrix.M23, matrix.M33));
			var minor1 = Determinant(new Matrix2x2f(matrix.M12, matrix.M32, matrix.M13, matrix.M33));
			var minor2 = Determinant(new Matrix2x2f(matrix.M12, matrix.M22, matrix.M13, matrix.M23));
			var cofactor0 = minor0;
			var cofactor1 = -minor1;
			var cofactor2 = minor2;
			return matrix.M11 * cofactor0 + matrix.M21 * cofactor1 + matrix.M31 * cofactor2;
		}
		#endregion
		#region Transpose
		/// <summary>
		/// Calculates the transpose of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose transpose is to be calculated.</param>
		/// <returns>The transpose of the specified matrix.</returns>
		public static Matrix3x3f Transpose(Matrix3x3f matrix)
		{
			return new Matrix3x3f(matrix.M11, matrix.M12, matrix.M13, matrix.M21, matrix.M22, matrix.M23, matrix.M31, matrix.M32, matrix.M33);
		}
		#endregion
	}
}
