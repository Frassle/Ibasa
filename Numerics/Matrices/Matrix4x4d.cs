using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics
{
	/// <summary>
	/// Represents a 4 by 4 matrix of doubles.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix4x4d: IEquatable<Matrix4x4d>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Matrix4x4d"/> with all of its elements equal to zero.
		/// </summary>
		public static readonly Matrix4x4d Zero = new Matrix4x4d(0);
		/// <summary>
		/// Returns a new <see cref="Matrix4x4d"/> with all of its elements equal to one.
		/// </summary>
		public static readonly Matrix4x4d One = new Matrix4x4d(1);
		/// <summary>
		/// Returns the identity <see cref="Matrix4x4d"/>.
		/// </summary>
		public static readonly Matrix4x4d Identity = new Matrix4x4d(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
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
		/// Gets the element of the matrix that exists in the third row and first column.
		/// </summary>
		public readonly double M31;
		/// <summary>
		/// Gets the element of the matrix that exists in the fourth row and first column.
		/// </summary>
		public readonly double M41;
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and second column.
		/// </summary>
		public readonly double M12;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and second column.
		/// </summary>
		public readonly double M22;
		/// <summary>
		/// Gets the element of the matrix that exists in the third row and second column.
		/// </summary>
		public readonly double M32;
		/// <summary>
		/// Gets the element of the matrix that exists in the fourth row and second column.
		/// </summary>
		public readonly double M42;
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and third column.
		/// </summary>
		public readonly double M13;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and third column.
		/// </summary>
		public readonly double M23;
		/// <summary>
		/// Gets the element of the matrix that exists in the third row and third column.
		/// </summary>
		public readonly double M33;
		/// <summary>
		/// Gets the element of the matrix that exists in the fourth row and third column.
		/// </summary>
		public readonly double M43;
		/// <summary>
		/// Gets the element of the matrix that exists in the first row and fourth column.
		/// </summary>
		public readonly double M14;
		/// <summary>
		/// Gets the element of the matrix that exists in the second row and fourth column.
		/// </summary>
		public readonly double M24;
		/// <summary>
		/// Gets the element of the matrix that exists in the third row and fourth column.
		/// </summary>
		public readonly double M34;
		/// <summary>
		/// Gets the element of the matrix that exists in the fourth row and fourth column.
		/// </summary>
		public readonly double M44;
		#endregion
		#region Properties
		public double this[int row, int column]
		{
			get
			{
				if (row < 0 || row > 3)
					throw new ArgumentOutOfRangeException("row", "Rows for Matrix4x4d run from 0 to 3, inclusive.");
				if (column < 0 || column > 3)
					throw new ArgumentOutOfRangeException("column", "Columns for Matrix4x4d run from 0 to 3, inclusive.");
				int index = row + column * 4;
				return this[index];
			}
		}
		public double this[int index]
		{
			get
			{
				if (index < 0 || index > 15)
					throw new ArgumentOutOfRangeException("index", "Indices for Matrix4x4d run from 0 to 15, inclusive.");
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
					case 12: return M14;
					case 13: return M24;
					case 14: return M34;
					case 15: return M44;
				}
				return 0;
			}
		}
		public Vector4d GetRow(int row)
		{
			if (row < 0 || row > 3)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix4x4d run from 0 to 3, inclusive.");
			switch (row)
			{
				case 0:
					return new Vector4d(M11, M12, M13, M14);
				case 1:
					return new Vector4d(M21, M22, M23, M24);
				case 2:
					return new Vector4d(M31, M32, M33, M34);
				case 3:
					return new Vector4d(M41, M42, M43, M44);
			}
			return Vector4d.Zero;
		}
		public Vector4d GetColumn(int column)
		{
			if (column < 0 || column > 3)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix4x4d run from 0 to 3, inclusive.");
			switch (column)
			{
				case 0:
					return new Vector4d(M11, M21, M31, M41);
				case 1:
					return new Vector4d(M12, M22, M32, M42);
				case 2:
					return new Vector4d(M13, M23, M33, M43);
				case 3:
					return new Vector4d(M14, M24, M34, M44);
			}
			return Vector4d.Zero;
		}
		public double[] ToArray()
		{
			return new double[]
			{
				M11, M21, M31, M41, M12, M22, M32, M42, M13, M23, M33, M43, M14, M24, M34, M44
			};
		}
		#endregion
		#region Constructors
		public Matrix4x4d(double value)
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
			M41 = value;
			M42 = value;
			M43 = value;
			M44 = value;
		}
		public Matrix4x4d(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34, double m41, double m42, double m43, double m44)
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
			M14 = m14;
			M24 = m24;
			M34 = m34;
			M44 = m44;
		}
		#endregion
		#region Transform
		#region Rotation
		/// <summary>
		/// Creates a matrix that rotates around the x-axis.
		/// </summary>
		/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
		/// <returns>The created rotation matrix.</returns>
		public static Matrix4x4d RotationX(double angle)
		{
			var cos = Functions.Cos(angle);
			var sin = Functions.Sin(angle);
			return new Matrix4x4d(
				1, 0, 0, 0,
				0, cos, sin, 0,
				0, -sin, cos, 0,
				0, 0, 0, 1
			);
		}
		/// <summary>
		/// Creates a matrix that rotates around the y-axis.
		/// </summary>
		/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
		/// <returns>The created rotation matrix.</returns>
		public static Matrix4x4d RotationY(double angle)
		{
			var cos = Functions.Cos(angle);
			var sin = Functions.Sin(angle);
			return new Matrix4x4d(
				cos, 0, -sin, 0,
				0, 1, 0, 0,
				sin, 0, cos, 0,
				0, 0, 0, 1
			);
		}
		/// <summary>
		/// Creates a matrix that rotates around the z-axis.
		/// </summary>
		/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
		/// <returns>The created rotation matrix.</returns>
		public static Matrix4x4d RotationZ(double angle)
		{
			var cos = Functions.Cos(angle);
			var sin = Functions.Sin(angle);
			return new Matrix4x4d(
				cos, sin, 0, 0,
				-sin, cos, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1
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
		public static Matrix4x4d Scaling(double x, double y, double z)
		{
			return new Matrix4x4d(
				x, 0, 0, 0,
				0, y, 0, 0,
				0, 0, z, 0,
				0, 0, 0, 1
			);
		}
		#endregion
		#region Translation
		/// <summary>
		/// Creates a matrix that translates along the x-axis, y-axis and z-axis.
		/// </summary>
		/// <param name="x">Translation along the x-axis.</param>
		/// <param name="y">Translation along the y-axis.</param>
		/// <param name="z">Translation along the z-axis.</param>
		/// <returns>The created translation matrix.</returns>
		public static Matrix4x4d Translation(double x, double y, double z)
		{
			return new Matrix4x4d(
				1, 0, 0, x,
				0, 1, 0, y,
				0, 0, 1, z,
				0, 0, 0, 1
			);
		}
		#endregion
		#endregion
		#region Projection
		/// <summary>
		/// Creates an orthographic projection matrix.
		/// </summary>
		/// <param name="handedness">Handedness of the created matrix.</param>
		/// <param name="width">Width of the viewing volume.</param>
		/// <param name="height">Height of the viewing volume.</param>
		/// <param name="znear">Minimum z-value of the viewing volume.</param>
		/// <param name="zfar">Maximum z-value of the viewing volume.</param>
		/// <returns>The created projection matrix.</returns>
		public static Matrix4x4d Ortho(Handedness handedness, double width, double height, double znear, double zfar)
		{
			return Projection(ProjectionType.Orthographic, handedness, -width / 2, width / 2, -height / 2, height / 2, znear, zfar);
		}
		/// <summary>
		/// Creates a perspective projection matrix.
		/// </summary>
		/// <param name="handedness">Handedness of the created matrix.</param>
		/// <param name="width">Width of the viewing volume.</param>
		/// <param name="height">Height of the viewing volume.</param>
		/// <param name="znear">Minimum z-value of the viewing volume.</param>
		/// <param name="zfar">Maximum z-value of the viewing volume.</param>
		/// <returns>The created projection matrix.</returns>
		public static Matrix4x4d Perspective(Handedness handedness, double width, double height, double znear, double zfar)
		{
			return Projection(ProjectionType.Perspective, handedness, -width / 2, width / 2, -height / 2, height / 2, znear, zfar);
		}
		/// <summary>
		/// Creates a perspective projection matrix based on a field of view.
		/// </summary>
		/// <param name="handedness">Handedness of the created matrix.</param>
		/// <param name="fov">Field of view in the y direction, in radians.</param>
		/// <param name="aspect">Aspect ratio, defined as view space width divided by height.</param>
		/// <param name="znear">Minimum z-value of the viewing volume.</param>
		/// <param name="zfar">Maximum z-value of the viewing volume.</param>
		/// <returns>The created projection matrix.</returns>
		public static Matrix4x4d PerspectiveFov(Handedness handedness, double fov, double aspect, double znear, double zfar)
		{
			var yScale = (1 / Functions.Tan(fov / 2));
			var xScale = yScale / aspect;
			var width = 2 * znear / xScale;
			var height = 2 * znear / yScale;
			return Projection(ProjectionType.Perspective, handedness, -width / 2, width / 2, -height / 2, height / 2, znear, zfar);
		}
		public static Matrix4x4d Projection(ProjectionType type, Handedness handedness, double left, double right, double bottom, double top, double znear, double zfar)
		{
			var M11 = 2.0 / (right - left);
			var M12 = 0.0;
			var M13 = 0.0;
			var M14 = 0.0;
			var M21 = 0.0;
			var M22 = 2.0 / (top - bottom);
			var M23 = 0.0;
			var M24 = 0.0;
			var M31 = 0.0;
			var M32 = 0.0;
			var M33 = 2.0 / (zfar - znear);
			var M34 = 0.0;
			var M41 = 0.0;
			var M42 = 0.0;
			var M43 = 0.0;
			var M44 = 0.0;
			if (type == ProjectionType.Orthographic)
			{
				M14 = -(right + left) / (right - left);
				M24 = -(top + bottom) / (top - bottom);
				M34 = -(zfar + znear) / (zfar - znear);
				M44 = 1.0;
			}
			else //if (type == ProjectionType.Perspective)
			{
				M11 *= znear;
				M22 *= znear;
				M13 = (left + right) / (left - right);
				M23 = (bottom + top) / (bottom - top);
				M33 = zfar / (zfar - znear);
				M43 = 1.0;
				M34 = (znear * zfar) / (znear - zfar);
			}
			if (handedness == Handedness.Right)
			{
				M13 *= -1.0;
				M23 *= -1.0;
				M33 *= -1.0;
				M43 *= -1.0;
			}
			return new Matrix4x4d(
				(double)M11, (double)M12, (double)M13, (double)M14,
				(double)M21, (double)M22, (double)M23, (double)M24,
				(double)M31, (double)M32, (double)M33, (double)M34,
				(double)M41, (double)M42, (double)M43, (double)M44
			);
		}
		#endregion
		#region Operations
		public static Matrix4x4d operator +(Matrix4x4d value)
		{
			return value;
		}
		public static Matrix4x4d operator -(Matrix4x4d value)
		{
			return Matrix.Negate(value);
		}
		public static Matrix4x4d operator +(Matrix4x4d left, Matrix4x4d right)
		{
			return Matrix.Add(left, right);
		}
		public static Matrix4x4d operator -(Matrix4x4d left, Matrix4x4d right)
		{
			return Matrix.Subtract(left, right);
		}
		public static Matrix4x4d operator *(Matrix4x4d matrix, double scalar)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix4x4d operator *(double scalar, Matrix4x4d matrix)
		{
			return Matrix.Multiply(matrix, scalar);
		}
		public static Matrix4x2d operator *(Matrix4x4d left, Matrix4x2d right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix4x3d operator *(Matrix4x4d left, Matrix4x3d right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix4x4d operator *(Matrix4x4d left, Matrix4x4d right)
		{
			return Matrix.Multiply(left, right);
		}
		public static Matrix4x4d operator /(Matrix4x4d matrix, double scalar)
		{
			return Matrix.Divide(matrix, scalar);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a Matrix4x4f value to a Matrix4x4d.
		/// </summary>
		/// <param name="value">The value to convert to a Matrix4x4d.</param>
		/// <returns>A Matrix4x4d that has all components equal to value.</returns>
		public static implicit operator Matrix4x4d(Matrix4x4f value)
		{
			return new Matrix4x4d((double)value.M11, (double)value.M21, (double)value.M31, (double)value.M41, (double)value.M12, (double)value.M22, (double)value.M32, (double)value.M42, (double)value.M13, (double)value.M23, (double)value.M33, (double)value.M43, (double)value.M14, (double)value.M24, (double)value.M34, (double)value.M44);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Matrix4x4d"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return M11.GetHashCode() + M21.GetHashCode() + M31.GetHashCode() + M41.GetHashCode() + M12.GetHashCode() + M22.GetHashCode() + M32.GetHashCode() + M42.GetHashCode() + M13.GetHashCode() + M23.GetHashCode() + M33.GetHashCode() + M43.GetHashCode() + M14.GetHashCode() + M24.GetHashCode() + M34.GetHashCode() + M44.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Matrix4x4d"/> object or a type capable
		/// of implicit conversion to a <see cref="Matrix4x4d"/> object, and its value
		/// is equal to the current <see cref="Matrix4x4d"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix4x4d) { return Equals((Matrix4x4d)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// matrix have the same value.
		/// </summary>
		/// <param name="other">The matrix to compare.</param>
		/// <returns>true if this matrix and value have the same value; otherwise, false.</returns>
		public bool Equals(Matrix4x4d other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Matrix4x4d left, Matrix4x4d right)
		{
			return left == right;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Matrix4x4d left, Matrix4x4d right)
		{
			return left.M11 == right.M11 & left.M21 == right.M21 & left.M31 == right.M31 & left.M41 == right.M41 & left.M12 == right.M12 & left.M22 == right.M22 & left.M32 == right.M32 & left.M42 == right.M42 & left.M13 == right.M13 & left.M23 == right.M23 & left.M33 == right.M33 & left.M43 == right.M43 & left.M14 == right.M14 & left.M24 == right.M24 & left.M34 == right.M34 & left.M44 == right.M44;
		}
		/// <summary>
		/// Returns a value that indicates whether two matrices are not equal.
		/// </summary>
		/// <param name="left">The first matrix to compare.</param>
		/// <param name="right">The second matrix to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Matrix4x4d left, Matrix4x4d right)
		{
			return left.M11 != right.M11 | left.M21 != right.M21 | left.M31 != right.M31 | left.M41 != right.M41 | left.M12 != right.M12 | left.M22 != right.M22 | left.M32 != right.M32 | left.M42 != right.M42 | left.M13 != right.M13 | left.M23 != right.M23 | left.M33 != right.M33 | left.M43 != right.M43 | left.M14 != right.M14 | left.M24 != right.M24 | left.M34 != right.M34 | left.M44 != right.M44;
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
			return String.Format("[({0}, {1}, {2}, {3})({4}, {5}, {6}, {7})({8}, {9}, {10}, {11})({12}, {13}, {14}, {15})]", M11.ToString(format, provider), M21.ToString(format, provider), M31.ToString(format, provider), M41.ToString(format, provider), M12.ToString(format, provider), M22.ToString(format, provider), M32.ToString(format, provider), M42.ToString(format, provider), M13.ToString(format, provider), M23.ToString(format, provider), M33.ToString(format, provider), M43.ToString(format, provider), M14.ToString(format, provider), M24.ToString(format, provider), M34.ToString(format, provider), M44.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for matrix functions.
	/// </summary>
	public static partial class Matrix
	{
		#region Operations
		public static Matrix4x4d Negate(Matrix4x4d value)
		{
			return new Matrix4x4d(-value.M11, -value.M21, -value.M31, -value.M41, -value.M12, -value.M22, -value.M32, -value.M42, -value.M13, -value.M23, -value.M33, -value.M43, -value.M14, -value.M24, -value.M34, -value.M44);
		}
		public static Matrix4x4d Add(Matrix4x4d left, Matrix4x4d right)
		{
			return new Matrix4x4d(left.M11 + right.M11, left.M21 + right.M21, left.M31 + right.M31, left.M41 + right.M41, left.M12 + right.M12, left.M22 + right.M22, left.M32 + right.M32, left.M42 + right.M42, left.M13 + right.M13, left.M23 + right.M23, left.M33 + right.M33, left.M43 + right.M43, left.M14 + right.M14, left.M24 + right.M24, left.M34 + right.M34, left.M44 + right.M44);
		}
		public static Matrix4x4d Subtract(Matrix4x4d left, Matrix4x4d right)
		{
			return new Matrix4x4d(left.M11 - right.M11, left.M21 - right.M21, left.M31 - right.M31, left.M41 - right.M41, left.M12 - right.M12, left.M22 - right.M22, left.M32 - right.M32, left.M42 - right.M42, left.M13 - right.M13, left.M23 - right.M23, left.M33 - right.M33, left.M43 - right.M43, left.M14 - right.M14, left.M24 - right.M24, left.M34 - right.M34, left.M44 - right.M44);
		}
		public static Matrix4x2d Multiply(Matrix4x4d left, Matrix4x2d right)
		{
			return new Matrix4x2d(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41, left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41, left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31 + left.M44 * right.M41, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42, left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42, left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32 + left.M44 * right.M42);
		}
		public static Matrix4x3d Multiply(Matrix4x4d left, Matrix4x3d right)
		{
			return new Matrix4x3d(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41, left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41, left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31 + left.M44 * right.M41, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42, left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42, left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32 + left.M44 * right.M42, left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43, left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43, left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43, left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33 + left.M44 * right.M43);
		}
		public static Matrix4x4d Multiply(Matrix4x4d left, Matrix4x4d right)
		{
			return new Matrix4x4d(left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41, left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41, left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41, left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31 + left.M44 * right.M41, left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42, left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42, left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42, left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32 + left.M44 * right.M42, left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43, left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43, left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43, left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33 + left.M44 * right.M43, left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34 + left.M14 * right.M44, left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34 + left.M24 * right.M44, left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34 + left.M34 * right.M44, left.M41 * right.M14 + left.M42 * right.M24 + left.M43 * right.M34 + left.M44 * right.M44);
		}
		public static Matrix4x4d Multiply(Matrix4x4d matrix, double scalar)
		{
			return new Matrix4x4d(matrix.M11 * scalar, matrix.M21 * scalar, matrix.M31 * scalar, matrix.M41 * scalar, matrix.M12 * scalar, matrix.M22 * scalar, matrix.M32 * scalar, matrix.M42 * scalar, matrix.M13 * scalar, matrix.M23 * scalar, matrix.M33 * scalar, matrix.M43 * scalar, matrix.M14 * scalar, matrix.M24 * scalar, matrix.M34 * scalar, matrix.M44 * scalar);
		}
		public static Matrix4x4d Divide(Matrix4x4d matrix, double scalar)
		{
			return new Matrix4x4d(matrix.M11 / scalar, matrix.M21 / scalar, matrix.M31 / scalar, matrix.M41 / scalar, matrix.M12 / scalar, matrix.M22 / scalar, matrix.M32 / scalar, matrix.M42 / scalar, matrix.M13 / scalar, matrix.M23 / scalar, matrix.M33 / scalar, matrix.M43 / scalar, matrix.M14 / scalar, matrix.M24 / scalar, matrix.M34 / scalar, matrix.M44 / scalar);
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all elements of a matrix are non-zero.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>true if all elements are non-zero; false otherwise.</returns>
		public static bool All(Matrix4x4d value)
		{
			return value.M11 != 0 && value.M21 != 0 && value.M31 != 0 && value.M41 != 0 && value.M12 != 0 && value.M22 != 0 && value.M32 != 0 && value.M42 != 0 && value.M13 != 0 && value.M23 != 0 && value.M33 != 0 && value.M43 != 0 && value.M14 != 0 && value.M24 != 0 && value.M34 != 0 && value.M44 != 0;
		}
		/// <summary>
		/// Determines whether all elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if every element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Matrix4x4d value, Predicate<double> predicate)
		{
			return predicate(value.M11) && predicate(value.M21) && predicate(value.M31) && predicate(value.M41) && predicate(value.M12) && predicate(value.M22) && predicate(value.M32) && predicate(value.M42) && predicate(value.M13) && predicate(value.M23) && predicate(value.M33) && predicate(value.M43) && predicate(value.M14) && predicate(value.M24) && predicate(value.M34) && predicate(value.M44);
		}
		/// <summary>
		/// Determines whether any element of a matrix is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any elements are non-zero; false otherwise.</returns>
		public static bool Any(Matrix4x4d value)
		{
			return value.M11 != 0 || value.M21 != 0 || value.M31 != 0 || value.M41 != 0 || value.M12 != 0 || value.M22 != 0 || value.M32 != 0 || value.M42 != 0 || value.M13 != 0 || value.M23 != 0 || value.M33 != 0 || value.M43 != 0 || value.M14 != 0 || value.M24 != 0 || value.M34 != 0 || value.M44 != 0;
		}
		/// <summary>
		/// Determines whether any elements of a matrix satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>true if any element of the matrix passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Matrix4x4d value, Predicate<double> predicate)
		{
			return predicate(value.M11) || predicate(value.M21) || predicate(value.M31) || predicate(value.M41) || predicate(value.M12) || predicate(value.M22) || predicate(value.M32) || predicate(value.M42) || predicate(value.M13) || predicate(value.M23) || predicate(value.M33) || predicate(value.M43) || predicate(value.M14) || predicate(value.M24) || predicate(value.M34) || predicate(value.M44);
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
		public static Matrix4x4d Map(Matrix4x4d value, Func<double, double> mapping)
		{
			return new Matrix4x4d(mapping(value.M11), mapping(value.M21), mapping(value.M31), mapping(value.M41), mapping(value.M12), mapping(value.M22), mapping(value.M32), mapping(value.M42), mapping(value.M13), mapping(value.M23), mapping(value.M33), mapping(value.M43), mapping(value.M14), mapping(value.M24), mapping(value.M34), mapping(value.M44));
		}
		/// <summary>
		/// Maps the elements of a matrix and returns the result.
		/// </summary>
		/// <param name="value">The matrix to map.</param>
		/// <param name="mapping">A mapping function to apply to each element.</param>
		/// <returns>The result of mapping each element of value.</returns>
		public static Matrix4x4f Map(Matrix4x4d value, Func<double, float> mapping)
		{
			return new Matrix4x4f(mapping(value.M11), mapping(value.M21), mapping(value.M31), mapping(value.M41), mapping(value.M12), mapping(value.M22), mapping(value.M32), mapping(value.M42), mapping(value.M13), mapping(value.M23), mapping(value.M33), mapping(value.M43), mapping(value.M14), mapping(value.M24), mapping(value.M34), mapping(value.M44));
		}
		#endregion
		/// <summary>
		/// Multiplys the elements of two matrices and returns the result.
		/// </summary>
		/// <param name="left">The first matrix to modulate.</param>
		/// <param name="right">The second matrix to modulate.</param>
		/// <returns>The result of multiplying each element of left by the matching element in right.</returns>
		public static Matrix4x4d Modulate(Matrix4x4d left, Matrix4x4d right)
		{
			return new Matrix4x4d(left.M11 * right.M11, left.M21 * right.M21, left.M31 * right.M31, left.M41 * right.M41, left.M12 * right.M12, left.M22 * right.M22, left.M32 * right.M32, left.M42 * right.M42, left.M13 * right.M13, left.M23 * right.M23, left.M33 * right.M33, left.M43 * right.M43, left.M14 * right.M14, left.M24 * right.M24, left.M34 * right.M34, left.M44 * right.M44);
		}
		/// <summary>
		/// Returns the absolute value (per element).
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The absolute value (per element) of value.</returns>
		public static Matrix4x4d Abs(Matrix4x4d value)
		{
			return new Matrix4x4d(Functions.Abs(value.M11), Functions.Abs(value.M21), Functions.Abs(value.M31), Functions.Abs(value.M41), Functions.Abs(value.M12), Functions.Abs(value.M22), Functions.Abs(value.M32), Functions.Abs(value.M42), Functions.Abs(value.M13), Functions.Abs(value.M23), Functions.Abs(value.M33), Functions.Abs(value.M43), Functions.Abs(value.M14), Functions.Abs(value.M24), Functions.Abs(value.M34), Functions.Abs(value.M44));
		}
		/// <summary>
		/// Returns a matrix that contains the lowest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The lowest of each element in left and the matching element in right.</returns>
		public static Matrix4x4d Min(Matrix4x4d value1, Matrix4x4d value2)
		{
			return new Matrix4x4d(Functions.Min(value1.M11, value2.M11), Functions.Min(value1.M21, value2.M21), Functions.Min(value1.M31, value2.M31), Functions.Min(value1.M41, value2.M41), Functions.Min(value1.M12, value2.M12), Functions.Min(value1.M22, value2.M22), Functions.Min(value1.M32, value2.M32), Functions.Min(value1.M42, value2.M42), Functions.Min(value1.M13, value2.M13), Functions.Min(value1.M23, value2.M23), Functions.Min(value1.M33, value2.M33), Functions.Min(value1.M43, value2.M43), Functions.Min(value1.M14, value2.M14), Functions.Min(value1.M24, value2.M24), Functions.Min(value1.M34, value2.M34), Functions.Min(value1.M44, value2.M44));
		}
		/// <summary>
		/// Returns a matrix that contains the highest value from each pair of elements.
		/// </summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The highest of each element in left and the matching element in right.</returns>
		public static Matrix4x4d Max(Matrix4x4d value1, Matrix4x4d value2)
		{
			return new Matrix4x4d(Functions.Max(value1.M11, value2.M11), Functions.Max(value1.M21, value2.M21), Functions.Max(value1.M31, value2.M31), Functions.Max(value1.M41, value2.M41), Functions.Max(value1.M12, value2.M12), Functions.Max(value1.M22, value2.M22), Functions.Max(value1.M32, value2.M32), Functions.Max(value1.M42, value2.M42), Functions.Max(value1.M13, value2.M13), Functions.Max(value1.M23, value2.M23), Functions.Max(value1.M33, value2.M33), Functions.Max(value1.M43, value2.M43), Functions.Max(value1.M14, value2.M14), Functions.Max(value1.M24, value2.M24), Functions.Max(value1.M34, value2.M34), Functions.Max(value1.M44, value2.M44));
		}
		/// <summary>
		/// Constrains each element to a given range.
		/// </summary>
		/// <param name="value">A matrix to constrain.</param>
		/// <param name="min">The minimum values for each element.</param>
		/// <param name="max">The maximum values for each element.</param>
		/// <returns>A matrix with each element constrained to the given range.</returns>
		public static Matrix4x4d Clamp(Matrix4x4d value, Matrix4x4d min, Matrix4x4d max)
		{
			return new Matrix4x4d(Functions.Clamp(value.M11, min.M11, max.M11), Functions.Clamp(value.M21, min.M21, max.M21), Functions.Clamp(value.M31, min.M31, max.M31), Functions.Clamp(value.M41, min.M41, max.M41), Functions.Clamp(value.M12, min.M12, max.M12), Functions.Clamp(value.M22, min.M22, max.M22), Functions.Clamp(value.M32, min.M32, max.M32), Functions.Clamp(value.M42, min.M42, max.M42), Functions.Clamp(value.M13, min.M13, max.M13), Functions.Clamp(value.M23, min.M23, max.M23), Functions.Clamp(value.M33, min.M33, max.M33), Functions.Clamp(value.M43, min.M43, max.M43), Functions.Clamp(value.M14, min.M14, max.M14), Functions.Clamp(value.M24, min.M24, max.M24), Functions.Clamp(value.M34, min.M34, max.M34), Functions.Clamp(value.M44, min.M44, max.M44));
		}
		/// <summary>
		/// Constrains each element to the range 0 to 1.
		/// </summary>
		/// <param name="value">A matrix to saturate.</param>
		/// <returns>A matrix with each element constrained to the range 0 to 1.</returns>
		public static Matrix4x4d Saturate(Matrix4x4d value)
		{
			return new Matrix4x4d(Functions.Saturate(value.M11), Functions.Saturate(value.M21), Functions.Saturate(value.M31), Functions.Saturate(value.M41), Functions.Saturate(value.M12), Functions.Saturate(value.M22), Functions.Saturate(value.M32), Functions.Saturate(value.M42), Functions.Saturate(value.M13), Functions.Saturate(value.M23), Functions.Saturate(value.M33), Functions.Saturate(value.M43), Functions.Saturate(value.M14), Functions.Saturate(value.M24), Functions.Saturate(value.M34), Functions.Saturate(value.M44));
		}
		/// <summary>
		/// Returns a matrix where each element is the smallest integral value that
		/// is greater than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The ceiling of value.</returns>
		public static Matrix4x4d Ceiling(Matrix4x4d value)
		{
			return new Matrix4x4d(Functions.Ceiling(value.M11), Functions.Ceiling(value.M21), Functions.Ceiling(value.M31), Functions.Ceiling(value.M41), Functions.Ceiling(value.M12), Functions.Ceiling(value.M22), Functions.Ceiling(value.M32), Functions.Ceiling(value.M42), Functions.Ceiling(value.M13), Functions.Ceiling(value.M23), Functions.Ceiling(value.M33), Functions.Ceiling(value.M43), Functions.Ceiling(value.M14), Functions.Ceiling(value.M24), Functions.Ceiling(value.M34), Functions.Ceiling(value.M44));
		}
		/// <summary>
		/// Returns a matrix where each element is the largest integral value that
		/// is less than or equal to the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The floor of value.</returns>
		public static Matrix4x4d Floor(Matrix4x4d value)
		{
			return new Matrix4x4d(Functions.Floor(value.M11), Functions.Floor(value.M21), Functions.Floor(value.M31), Functions.Floor(value.M41), Functions.Floor(value.M12), Functions.Floor(value.M22), Functions.Floor(value.M32), Functions.Floor(value.M42), Functions.Floor(value.M13), Functions.Floor(value.M23), Functions.Floor(value.M33), Functions.Floor(value.M43), Functions.Floor(value.M14), Functions.Floor(value.M24), Functions.Floor(value.M34), Functions.Floor(value.M44));
		}
		/// <summary>
		/// Returns a matrix where each element is the integral part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The integral of value.</returns>
		public static Matrix4x4d Truncate(Matrix4x4d value)
		{
			return new Matrix4x4d(Functions.Truncate(value.M11), Functions.Truncate(value.M21), Functions.Truncate(value.M31), Functions.Truncate(value.M41), Functions.Truncate(value.M12), Functions.Truncate(value.M22), Functions.Truncate(value.M32), Functions.Truncate(value.M42), Functions.Truncate(value.M13), Functions.Truncate(value.M23), Functions.Truncate(value.M33), Functions.Truncate(value.M43), Functions.Truncate(value.M14), Functions.Truncate(value.M24), Functions.Truncate(value.M34), Functions.Truncate(value.M44));
		}
		/// <summary>
		/// Returns a matrix where each element is the fractional part of the specified element.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The fractional of value.</returns>
		public static Matrix4x4d Fractional(Matrix4x4d value)
		{
			return new Matrix4x4d(Functions.Fractional(value.M11), Functions.Fractional(value.M21), Functions.Fractional(value.M31), Functions.Fractional(value.M41), Functions.Fractional(value.M12), Functions.Fractional(value.M22), Functions.Fractional(value.M32), Functions.Fractional(value.M42), Functions.Fractional(value.M13), Functions.Fractional(value.M23), Functions.Fractional(value.M33), Functions.Fractional(value.M43), Functions.Fractional(value.M14), Functions.Fractional(value.M24), Functions.Fractional(value.M34), Functions.Fractional(value.M44));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix4x4d Round(Matrix4x4d value)
		{
			return new Matrix4x4d(Functions.Round(value.M11), Functions.Round(value.M21), Functions.Round(value.M31), Functions.Round(value.M41), Functions.Round(value.M12), Functions.Round(value.M22), Functions.Round(value.M32), Functions.Round(value.M42), Functions.Round(value.M13), Functions.Round(value.M23), Functions.Round(value.M33), Functions.Round(value.M43), Functions.Round(value.M14), Functions.Round(value.M24), Functions.Round(value.M34), Functions.Round(value.M44));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix4x4d Round(Matrix4x4d value, int digits)
		{
			return new Matrix4x4d(Functions.Round(value.M11, digits), Functions.Round(value.M21, digits), Functions.Round(value.M31, digits), Functions.Round(value.M41, digits), Functions.Round(value.M12, digits), Functions.Round(value.M22, digits), Functions.Round(value.M32, digits), Functions.Round(value.M42, digits), Functions.Round(value.M13, digits), Functions.Round(value.M23, digits), Functions.Round(value.M33, digits), Functions.Round(value.M43, digits), Functions.Round(value.M14, digits), Functions.Round(value.M24, digits), Functions.Round(value.M34, digits), Functions.Round(value.M44, digits));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix4x4d Round(Matrix4x4d value, MidpointRounding mode)
		{
			return new Matrix4x4d(Functions.Round(value.M11, mode), Functions.Round(value.M21, mode), Functions.Round(value.M31, mode), Functions.Round(value.M41, mode), Functions.Round(value.M12, mode), Functions.Round(value.M22, mode), Functions.Round(value.M32, mode), Functions.Round(value.M42, mode), Functions.Round(value.M13, mode), Functions.Round(value.M23, mode), Functions.Round(value.M33, mode), Functions.Round(value.M43, mode), Functions.Round(value.M14, mode), Functions.Round(value.M24, mode), Functions.Round(value.M34, mode), Functions.Round(value.M44, mode));
		}
		/// <summary>
		/// Returns a matrix where each element is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Matrix4x4d Round(Matrix4x4d value, int digits, MidpointRounding mode)
		{
			return new Matrix4x4d(Functions.Round(value.M11, digits, mode), Functions.Round(value.M21, digits, mode), Functions.Round(value.M31, digits, mode), Functions.Round(value.M41, digits, mode), Functions.Round(value.M12, digits, mode), Functions.Round(value.M22, digits, mode), Functions.Round(value.M32, digits, mode), Functions.Round(value.M42, digits, mode), Functions.Round(value.M13, digits, mode), Functions.Round(value.M23, digits, mode), Functions.Round(value.M33, digits, mode), Functions.Round(value.M43, digits, mode), Functions.Round(value.M14, digits, mode), Functions.Round(value.M24, digits, mode), Functions.Round(value.M34, digits, mode), Functions.Round(value.M44, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each element in the matrix.
		/// </summary>
		/// <param name="value">A matrix.</param>
		/// <returns>A matrix with the reciprocal of each of values elements.</returns>
		public static Matrix4x4d Reciprocal(Matrix4x4d value)
		{
			return new Matrix4x4d(1 / value.M11, 1 / value.M21, 1 / value.M31, 1 / value.M41, 1 / value.M12, 1 / value.M22, 1 / value.M32, 1 / value.M42, 1 / value.M13, 1 / value.M23, 1 / value.M33, 1 / value.M43, 1 / value.M14, 1 / value.M24, 1 / value.M34, 1 / value.M44);
		}
		#endregion
		#region Submatrix
		/// <summary>
		/// Returns the specified submatrix of the given matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose submatrix is to returned.</param>
		/// <param name="row">The row to be removed.</param>
		/// <param name="column">The column to be removed.</param>
		public static Matrix3x3d Submatrix(Matrix4x4d matrix, int row, int column)
		{
			if (row < 0 || row > 3)
				throw new ArgumentOutOfRangeException("row", "Rows for Matrix4x4d run from 0 to 3, inclusive.");
			if (column < 0 || column > 3)
				throw new ArgumentOutOfRangeException("column", "Columns for Matrix4x4d run from 0 to 3, inclusive.");
			if (row == 0 && column == 0)
			{
				return new Matrix3x3d(matrix.M22, matrix.M32, matrix.M42, matrix.M23, matrix.M33, matrix.M43, matrix.M24, matrix.M34, matrix.M44);
			}
			else if (row == 0 && column == 1)
			{
				return new Matrix3x3d(matrix.M21, matrix.M31, matrix.M41, matrix.M23, matrix.M33, matrix.M43, matrix.M24, matrix.M34, matrix.M44);
			}
			else if (row == 0 && column == 2)
			{
				return new Matrix3x3d(matrix.M21, matrix.M31, matrix.M41, matrix.M22, matrix.M32, matrix.M42, matrix.M24, matrix.M34, matrix.M44);
			}
			else if (row == 0 && column == 3)
			{
				return new Matrix3x3d(matrix.M21, matrix.M31, matrix.M41, matrix.M22, matrix.M32, matrix.M42, matrix.M23, matrix.M33, matrix.M43);
			}
			else if (row == 1 && column == 0)
			{
				return new Matrix3x3d(matrix.M12, matrix.M32, matrix.M42, matrix.M13, matrix.M33, matrix.M43, matrix.M14, matrix.M34, matrix.M44);
			}
			else if (row == 1 && column == 1)
			{
				return new Matrix3x3d(matrix.M11, matrix.M31, matrix.M41, matrix.M13, matrix.M33, matrix.M43, matrix.M14, matrix.M34, matrix.M44);
			}
			else if (row == 1 && column == 2)
			{
				return new Matrix3x3d(matrix.M11, matrix.M31, matrix.M41, matrix.M12, matrix.M32, matrix.M42, matrix.M14, matrix.M34, matrix.M44);
			}
			else if (row == 1 && column == 3)
			{
				return new Matrix3x3d(matrix.M11, matrix.M31, matrix.M41, matrix.M12, matrix.M32, matrix.M42, matrix.M13, matrix.M33, matrix.M43);
			}
			else if (row == 2 && column == 0)
			{
				return new Matrix3x3d(matrix.M12, matrix.M22, matrix.M42, matrix.M13, matrix.M23, matrix.M43, matrix.M14, matrix.M24, matrix.M44);
			}
			else if (row == 2 && column == 1)
			{
				return new Matrix3x3d(matrix.M11, matrix.M21, matrix.M41, matrix.M13, matrix.M23, matrix.M43, matrix.M14, matrix.M24, matrix.M44);
			}
			else if (row == 2 && column == 2)
			{
				return new Matrix3x3d(matrix.M11, matrix.M21, matrix.M41, matrix.M12, matrix.M22, matrix.M42, matrix.M14, matrix.M24, matrix.M44);
			}
			else if (row == 2 && column == 3)
			{
				return new Matrix3x3d(matrix.M11, matrix.M21, matrix.M41, matrix.M12, matrix.M22, matrix.M42, matrix.M13, matrix.M23, matrix.M43);
			}
			else if (row == 3 && column == 0)
			{
				return new Matrix3x3d(matrix.M12, matrix.M22, matrix.M32, matrix.M13, matrix.M23, matrix.M33, matrix.M14, matrix.M24, matrix.M34);
			}
			else if (row == 3 && column == 1)
			{
				return new Matrix3x3d(matrix.M11, matrix.M21, matrix.M31, matrix.M13, matrix.M23, matrix.M33, matrix.M14, matrix.M24, matrix.M34);
			}
			else if (row == 3 && column == 2)
			{
				return new Matrix3x3d(matrix.M11, matrix.M21, matrix.M31, matrix.M12, matrix.M22, matrix.M32, matrix.M14, matrix.M24, matrix.M34);
			}
			else
			{
				return new Matrix3x3d(matrix.M11, matrix.M21, matrix.M31, matrix.M12, matrix.M22, matrix.M32, matrix.M13, matrix.M23, matrix.M33);
			}
		}
		#endregion
		#region Invert, Determinant
		/// <summary>
		/// Calculates the inverse of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose inverse is to be calculated.</param>
		/// <param name="determinant">When the method completes, contains the determinant of the matrix.</param>
		public static Matrix4x4d Invert(Matrix4x4d matrix, out double determinant)
		{
			var cofactor00 = Determinant(new Matrix3x3d(matrix.M22, matrix.M32, matrix.M42, matrix.M23, matrix.M33, matrix.M43, matrix.M24, matrix.M34, matrix.M44));
			var cofactor10 = -Determinant(new Matrix3x3d(matrix.M21, matrix.M31, matrix.M41, matrix.M23, matrix.M33, matrix.M43, matrix.M24, matrix.M34, matrix.M44));
			var cofactor20 = Determinant(new Matrix3x3d(matrix.M21, matrix.M31, matrix.M41, matrix.M22, matrix.M32, matrix.M42, matrix.M24, matrix.M34, matrix.M44));
			var cofactor30 = -Determinant(new Matrix3x3d(matrix.M21, matrix.M31, matrix.M41, matrix.M22, matrix.M32, matrix.M42, matrix.M23, matrix.M33, matrix.M43));
			var cofactor01 = -Determinant(new Matrix3x3d(matrix.M12, matrix.M32, matrix.M42, matrix.M13, matrix.M33, matrix.M43, matrix.M14, matrix.M34, matrix.M44));
			var cofactor11 = Determinant(new Matrix3x3d(matrix.M11, matrix.M31, matrix.M41, matrix.M13, matrix.M33, matrix.M43, matrix.M14, matrix.M34, matrix.M44));
			var cofactor21 = -Determinant(new Matrix3x3d(matrix.M11, matrix.M31, matrix.M41, matrix.M12, matrix.M32, matrix.M42, matrix.M14, matrix.M34, matrix.M44));
			var cofactor31 = Determinant(new Matrix3x3d(matrix.M11, matrix.M31, matrix.M41, matrix.M12, matrix.M32, matrix.M42, matrix.M13, matrix.M33, matrix.M43));
			var cofactor02 = Determinant(new Matrix3x3d(matrix.M12, matrix.M22, matrix.M42, matrix.M13, matrix.M23, matrix.M43, matrix.M14, matrix.M24, matrix.M44));
			var cofactor12 = -Determinant(new Matrix3x3d(matrix.M11, matrix.M21, matrix.M41, matrix.M13, matrix.M23, matrix.M43, matrix.M14, matrix.M24, matrix.M44));
			var cofactor22 = Determinant(new Matrix3x3d(matrix.M11, matrix.M21, matrix.M41, matrix.M12, matrix.M22, matrix.M42, matrix.M14, matrix.M24, matrix.M44));
			var cofactor32 = -Determinant(new Matrix3x3d(matrix.M11, matrix.M21, matrix.M41, matrix.M12, matrix.M22, matrix.M42, matrix.M13, matrix.M23, matrix.M43));
			var cofactor03 = -Determinant(new Matrix3x3d(matrix.M12, matrix.M22, matrix.M32, matrix.M13, matrix.M23, matrix.M33, matrix.M14, matrix.M24, matrix.M34));
			var cofactor13 = Determinant(new Matrix3x3d(matrix.M11, matrix.M21, matrix.M31, matrix.M13, matrix.M23, matrix.M33, matrix.M14, matrix.M24, matrix.M34));
			var cofactor23 = -Determinant(new Matrix3x3d(matrix.M11, matrix.M21, matrix.M31, matrix.M12, matrix.M22, matrix.M32, matrix.M14, matrix.M24, matrix.M34));
			var cofactor33 = Determinant(new Matrix3x3d(matrix.M11, matrix.M21, matrix.M31, matrix.M12, matrix.M22, matrix.M32, matrix.M13, matrix.M23, matrix.M33));
			determinant = matrix.M11 * cofactor00 + matrix.M21 * cofactor10 + matrix.M31 * cofactor20 + matrix.M41 * cofactor30;
			return new Matrix4x4d(cofactor00, cofactor01, cofactor02, cofactor03, cofactor10, cofactor11, cofactor12, cofactor13, cofactor20, cofactor21, cofactor22, cofactor23, cofactor30, cofactor31, cofactor32, cofactor33) / determinant;
		}
		/// <summary>
		/// Calculates the inverse of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose inverse is to be calculated.</param>
		/// <param name="determinant">When the method completes, contains the determinant of the matrix.</param>
		public static Matrix4x4d Invert(Matrix4x4d matrix)
		{
			double determinant;
			return Invert(matrix, out determinant);
		}
		/// <summary>
		/// Calculates the determinant of the matrix.
		/// </summary>
		/// <returns>The determinant of the matrix.</returns>
		public static double Determinant(Matrix4x4d matrix)
		{
			var minor0 = Determinant(new Matrix3x3d(matrix.M22, matrix.M32, matrix.M42, matrix.M23, matrix.M33, matrix.M43, matrix.M24, matrix.M34, matrix.M44));
			var minor1 = Determinant(new Matrix3x3d(matrix.M12, matrix.M32, matrix.M42, matrix.M13, matrix.M33, matrix.M43, matrix.M14, matrix.M34, matrix.M44));
			var minor2 = Determinant(new Matrix3x3d(matrix.M12, matrix.M22, matrix.M42, matrix.M13, matrix.M23, matrix.M43, matrix.M14, matrix.M24, matrix.M44));
			var minor3 = Determinant(new Matrix3x3d(matrix.M12, matrix.M22, matrix.M32, matrix.M13, matrix.M23, matrix.M33, matrix.M14, matrix.M24, matrix.M34));
			var cofactor0 = minor0;
			var cofactor1 = -minor1;
			var cofactor2 = minor2;
			var cofactor3 = -minor3;
			return matrix.M11 * cofactor0 + matrix.M21 * cofactor1 + matrix.M31 * cofactor2 + matrix.M41 * cofactor3;
		}
		#endregion
		#region Transpose
		/// <summary>
		/// Calculates the transpose of the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix whose transpose is to be calculated.</param>
		/// <returns>The transpose of the specified matrix.</returns>
		public static Matrix4x4d Transpose(Matrix4x4d matrix)
		{
			return new Matrix4x4d(matrix.M11, matrix.M12, matrix.M13, matrix.M14, matrix.M21, matrix.M22, matrix.M23, matrix.M24, matrix.M31, matrix.M32, matrix.M33, matrix.M34, matrix.M41, matrix.M42, matrix.M43, matrix.M44);
		}
		#endregion
	}
}
