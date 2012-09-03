using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a four component vector of doubles, of the form (X, Y, Z, W).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector4d: IEquatable<Vector4d>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector4d"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector4d Zero = new Vector4d(0);
		/// <summary>
		/// Returns a new <see cref="Vector4d"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector4d One = new Vector4d(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector4d"/> (1, 0, 0, 0).
		/// </summary>
		public static readonly Vector4d UnitX = new Vector4d(1, 0, 0, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector4d"/> (0, 1, 0, 0).
		/// </summary>
		public static readonly Vector4d UnitY = new Vector4d(0, 1, 0, 0);
		/// <summary>
		/// Returns the Z unit <see cref="Vector4d"/> (0, 0, 1, 0).
		/// </summary>
		public static readonly Vector4d UnitZ = new Vector4d(0, 0, 1, 0);
		/// <summary>
		/// Returns the W unit <see cref="Vector4d"/> (0, 0, 0, 1).
		/// </summary>
		public static readonly Vector4d UnitW = new Vector4d(0, 0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the vector.
		/// </summary>
		public readonly double X;
		/// <summary>
		/// The Y component of the vector.
		/// </summary>
		public readonly double Y;
		/// <summary>
		/// The Z component of the vector.
		/// </summary>
		public readonly double Z;
		/// <summary>
		/// The W component of the vector.
		/// </summary>
		public readonly double W;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public double this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return X;
					case 1:
						return Y;
					case 2:
						return Z;
					case 3:
						return W;
					default:
						throw new IndexOutOfRangeException("Indices for Vector4d run from 0 to 3, inclusive.");
				}
			}
		}
		public double[] ToArray()
		{
			return new double[]
			{
				X, Y, Z, W
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the vector (X, X).
		/// </summary>
		public Vector2d XX
		{
			get
			{
				return new Vector2d(X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y).
		/// </summary>
		public Vector2d XY
		{
			get
			{
				return new Vector2d(X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z).
		/// </summary>
		public Vector2d XZ
		{
			get
			{
				return new Vector2d(X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W).
		/// </summary>
		public Vector2d XW
		{
			get
			{
				return new Vector2d(X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X).
		/// </summary>
		public Vector2d YX
		{
			get
			{
				return new Vector2d(Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y).
		/// </summary>
		public Vector2d YY
		{
			get
			{
				return new Vector2d(Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z).
		/// </summary>
		public Vector2d YZ
		{
			get
			{
				return new Vector2d(Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W).
		/// </summary>
		public Vector2d YW
		{
			get
			{
				return new Vector2d(Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X).
		/// </summary>
		public Vector2d ZX
		{
			get
			{
				return new Vector2d(Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y).
		/// </summary>
		public Vector2d ZY
		{
			get
			{
				return new Vector2d(Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z).
		/// </summary>
		public Vector2d ZZ
		{
			get
			{
				return new Vector2d(Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W).
		/// </summary>
		public Vector2d ZW
		{
			get
			{
				return new Vector2d(Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X).
		/// </summary>
		public Vector2d WX
		{
			get
			{
				return new Vector2d(W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y).
		/// </summary>
		public Vector2d WY
		{
			get
			{
				return new Vector2d(W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z).
		/// </summary>
		public Vector2d WZ
		{
			get
			{
				return new Vector2d(W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W).
		/// </summary>
		public Vector2d WW
		{
			get
			{
				return new Vector2d(W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X).
		/// </summary>
		public Vector3d XXX
		{
			get
			{
				return new Vector3d(X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y).
		/// </summary>
		public Vector3d XXY
		{
			get
			{
				return new Vector3d(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z).
		/// </summary>
		public Vector3d XXZ
		{
			get
			{
				return new Vector3d(X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W).
		/// </summary>
		public Vector3d XXW
		{
			get
			{
				return new Vector3d(X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X).
		/// </summary>
		public Vector3d XYX
		{
			get
			{
				return new Vector3d(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y).
		/// </summary>
		public Vector3d XYY
		{
			get
			{
				return new Vector3d(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z).
		/// </summary>
		public Vector3d XYZ
		{
			get
			{
				return new Vector3d(X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W).
		/// </summary>
		public Vector3d XYW
		{
			get
			{
				return new Vector3d(X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X).
		/// </summary>
		public Vector3d XZX
		{
			get
			{
				return new Vector3d(X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y).
		/// </summary>
		public Vector3d XZY
		{
			get
			{
				return new Vector3d(X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z).
		/// </summary>
		public Vector3d XZZ
		{
			get
			{
				return new Vector3d(X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W).
		/// </summary>
		public Vector3d XZW
		{
			get
			{
				return new Vector3d(X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X).
		/// </summary>
		public Vector3d XWX
		{
			get
			{
				return new Vector3d(X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y).
		/// </summary>
		public Vector3d XWY
		{
			get
			{
				return new Vector3d(X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z).
		/// </summary>
		public Vector3d XWZ
		{
			get
			{
				return new Vector3d(X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W).
		/// </summary>
		public Vector3d XWW
		{
			get
			{
				return new Vector3d(X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X).
		/// </summary>
		public Vector3d YXX
		{
			get
			{
				return new Vector3d(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y).
		/// </summary>
		public Vector3d YXY
		{
			get
			{
				return new Vector3d(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z).
		/// </summary>
		public Vector3d YXZ
		{
			get
			{
				return new Vector3d(Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W).
		/// </summary>
		public Vector3d YXW
		{
			get
			{
				return new Vector3d(Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X).
		/// </summary>
		public Vector3d YYX
		{
			get
			{
				return new Vector3d(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y).
		/// </summary>
		public Vector3d YYY
		{
			get
			{
				return new Vector3d(Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z).
		/// </summary>
		public Vector3d YYZ
		{
			get
			{
				return new Vector3d(Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W).
		/// </summary>
		public Vector3d YYW
		{
			get
			{
				return new Vector3d(Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X).
		/// </summary>
		public Vector3d YZX
		{
			get
			{
				return new Vector3d(Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y).
		/// </summary>
		public Vector3d YZY
		{
			get
			{
				return new Vector3d(Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z).
		/// </summary>
		public Vector3d YZZ
		{
			get
			{
				return new Vector3d(Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W).
		/// </summary>
		public Vector3d YZW
		{
			get
			{
				return new Vector3d(Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X).
		/// </summary>
		public Vector3d YWX
		{
			get
			{
				return new Vector3d(Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y).
		/// </summary>
		public Vector3d YWY
		{
			get
			{
				return new Vector3d(Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z).
		/// </summary>
		public Vector3d YWZ
		{
			get
			{
				return new Vector3d(Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W).
		/// </summary>
		public Vector3d YWW
		{
			get
			{
				return new Vector3d(Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X).
		/// </summary>
		public Vector3d ZXX
		{
			get
			{
				return new Vector3d(Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y).
		/// </summary>
		public Vector3d ZXY
		{
			get
			{
				return new Vector3d(Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z).
		/// </summary>
		public Vector3d ZXZ
		{
			get
			{
				return new Vector3d(Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W).
		/// </summary>
		public Vector3d ZXW
		{
			get
			{
				return new Vector3d(Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X).
		/// </summary>
		public Vector3d ZYX
		{
			get
			{
				return new Vector3d(Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y).
		/// </summary>
		public Vector3d ZYY
		{
			get
			{
				return new Vector3d(Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z).
		/// </summary>
		public Vector3d ZYZ
		{
			get
			{
				return new Vector3d(Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W).
		/// </summary>
		public Vector3d ZYW
		{
			get
			{
				return new Vector3d(Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X).
		/// </summary>
		public Vector3d ZZX
		{
			get
			{
				return new Vector3d(Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y).
		/// </summary>
		public Vector3d ZZY
		{
			get
			{
				return new Vector3d(Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z).
		/// </summary>
		public Vector3d ZZZ
		{
			get
			{
				return new Vector3d(Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W).
		/// </summary>
		public Vector3d ZZW
		{
			get
			{
				return new Vector3d(Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X).
		/// </summary>
		public Vector3d ZWX
		{
			get
			{
				return new Vector3d(Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y).
		/// </summary>
		public Vector3d ZWY
		{
			get
			{
				return new Vector3d(Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z).
		/// </summary>
		public Vector3d ZWZ
		{
			get
			{
				return new Vector3d(Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W).
		/// </summary>
		public Vector3d ZWW
		{
			get
			{
				return new Vector3d(Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X).
		/// </summary>
		public Vector3d WXX
		{
			get
			{
				return new Vector3d(W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y).
		/// </summary>
		public Vector3d WXY
		{
			get
			{
				return new Vector3d(W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z).
		/// </summary>
		public Vector3d WXZ
		{
			get
			{
				return new Vector3d(W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W).
		/// </summary>
		public Vector3d WXW
		{
			get
			{
				return new Vector3d(W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X).
		/// </summary>
		public Vector3d WYX
		{
			get
			{
				return new Vector3d(W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y).
		/// </summary>
		public Vector3d WYY
		{
			get
			{
				return new Vector3d(W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z).
		/// </summary>
		public Vector3d WYZ
		{
			get
			{
				return new Vector3d(W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W).
		/// </summary>
		public Vector3d WYW
		{
			get
			{
				return new Vector3d(W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X).
		/// </summary>
		public Vector3d WZX
		{
			get
			{
				return new Vector3d(W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y).
		/// </summary>
		public Vector3d WZY
		{
			get
			{
				return new Vector3d(W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z).
		/// </summary>
		public Vector3d WZZ
		{
			get
			{
				return new Vector3d(W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W).
		/// </summary>
		public Vector3d WZW
		{
			get
			{
				return new Vector3d(W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X).
		/// </summary>
		public Vector3d WWX
		{
			get
			{
				return new Vector3d(W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y).
		/// </summary>
		public Vector3d WWY
		{
			get
			{
				return new Vector3d(W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z).
		/// </summary>
		public Vector3d WWZ
		{
			get
			{
				return new Vector3d(W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W).
		/// </summary>
		public Vector3d WWW
		{
			get
			{
				return new Vector3d(W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, X).
		/// </summary>
		public Vector4d XXXX
		{
			get
			{
				return new Vector4d(X, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Y).
		/// </summary>
		public Vector4d XXXY
		{
			get
			{
				return new Vector4d(X, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Z).
		/// </summary>
		public Vector4d XXXZ
		{
			get
			{
				return new Vector4d(X, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, W).
		/// </summary>
		public Vector4d XXXW
		{
			get
			{
				return new Vector4d(X, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, X).
		/// </summary>
		public Vector4d XXYX
		{
			get
			{
				return new Vector4d(X, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Y).
		/// </summary>
		public Vector4d XXYY
		{
			get
			{
				return new Vector4d(X, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Z).
		/// </summary>
		public Vector4d XXYZ
		{
			get
			{
				return new Vector4d(X, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, W).
		/// </summary>
		public Vector4d XXYW
		{
			get
			{
				return new Vector4d(X, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, X).
		/// </summary>
		public Vector4d XXZX
		{
			get
			{
				return new Vector4d(X, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Y).
		/// </summary>
		public Vector4d XXZY
		{
			get
			{
				return new Vector4d(X, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Z).
		/// </summary>
		public Vector4d XXZZ
		{
			get
			{
				return new Vector4d(X, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, W).
		/// </summary>
		public Vector4d XXZW
		{
			get
			{
				return new Vector4d(X, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, X).
		/// </summary>
		public Vector4d XXWX
		{
			get
			{
				return new Vector4d(X, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Y).
		/// </summary>
		public Vector4d XXWY
		{
			get
			{
				return new Vector4d(X, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Z).
		/// </summary>
		public Vector4d XXWZ
		{
			get
			{
				return new Vector4d(X, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, W).
		/// </summary>
		public Vector4d XXWW
		{
			get
			{
				return new Vector4d(X, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, X).
		/// </summary>
		public Vector4d XYXX
		{
			get
			{
				return new Vector4d(X, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Y).
		/// </summary>
		public Vector4d XYXY
		{
			get
			{
				return new Vector4d(X, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Z).
		/// </summary>
		public Vector4d XYXZ
		{
			get
			{
				return new Vector4d(X, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, W).
		/// </summary>
		public Vector4d XYXW
		{
			get
			{
				return new Vector4d(X, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, X).
		/// </summary>
		public Vector4d XYYX
		{
			get
			{
				return new Vector4d(X, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Y).
		/// </summary>
		public Vector4d XYYY
		{
			get
			{
				return new Vector4d(X, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Z).
		/// </summary>
		public Vector4d XYYZ
		{
			get
			{
				return new Vector4d(X, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, W).
		/// </summary>
		public Vector4d XYYW
		{
			get
			{
				return new Vector4d(X, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, X).
		/// </summary>
		public Vector4d XYZX
		{
			get
			{
				return new Vector4d(X, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Y).
		/// </summary>
		public Vector4d XYZY
		{
			get
			{
				return new Vector4d(X, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Z).
		/// </summary>
		public Vector4d XYZZ
		{
			get
			{
				return new Vector4d(X, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, W).
		/// </summary>
		public Vector4d XYZW
		{
			get
			{
				return new Vector4d(X, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, X).
		/// </summary>
		public Vector4d XYWX
		{
			get
			{
				return new Vector4d(X, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Y).
		/// </summary>
		public Vector4d XYWY
		{
			get
			{
				return new Vector4d(X, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Z).
		/// </summary>
		public Vector4d XYWZ
		{
			get
			{
				return new Vector4d(X, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, W).
		/// </summary>
		public Vector4d XYWW
		{
			get
			{
				return new Vector4d(X, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, X).
		/// </summary>
		public Vector4d XZXX
		{
			get
			{
				return new Vector4d(X, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Y).
		/// </summary>
		public Vector4d XZXY
		{
			get
			{
				return new Vector4d(X, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Z).
		/// </summary>
		public Vector4d XZXZ
		{
			get
			{
				return new Vector4d(X, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, W).
		/// </summary>
		public Vector4d XZXW
		{
			get
			{
				return new Vector4d(X, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, X).
		/// </summary>
		public Vector4d XZYX
		{
			get
			{
				return new Vector4d(X, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Y).
		/// </summary>
		public Vector4d XZYY
		{
			get
			{
				return new Vector4d(X, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Z).
		/// </summary>
		public Vector4d XZYZ
		{
			get
			{
				return new Vector4d(X, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, W).
		/// </summary>
		public Vector4d XZYW
		{
			get
			{
				return new Vector4d(X, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, X).
		/// </summary>
		public Vector4d XZZX
		{
			get
			{
				return new Vector4d(X, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Y).
		/// </summary>
		public Vector4d XZZY
		{
			get
			{
				return new Vector4d(X, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Z).
		/// </summary>
		public Vector4d XZZZ
		{
			get
			{
				return new Vector4d(X, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, W).
		/// </summary>
		public Vector4d XZZW
		{
			get
			{
				return new Vector4d(X, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, X).
		/// </summary>
		public Vector4d XZWX
		{
			get
			{
				return new Vector4d(X, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Y).
		/// </summary>
		public Vector4d XZWY
		{
			get
			{
				return new Vector4d(X, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Z).
		/// </summary>
		public Vector4d XZWZ
		{
			get
			{
				return new Vector4d(X, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, W).
		/// </summary>
		public Vector4d XZWW
		{
			get
			{
				return new Vector4d(X, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, X).
		/// </summary>
		public Vector4d XWXX
		{
			get
			{
				return new Vector4d(X, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Y).
		/// </summary>
		public Vector4d XWXY
		{
			get
			{
				return new Vector4d(X, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Z).
		/// </summary>
		public Vector4d XWXZ
		{
			get
			{
				return new Vector4d(X, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, W).
		/// </summary>
		public Vector4d XWXW
		{
			get
			{
				return new Vector4d(X, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, X).
		/// </summary>
		public Vector4d XWYX
		{
			get
			{
				return new Vector4d(X, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Y).
		/// </summary>
		public Vector4d XWYY
		{
			get
			{
				return new Vector4d(X, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Z).
		/// </summary>
		public Vector4d XWYZ
		{
			get
			{
				return new Vector4d(X, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, W).
		/// </summary>
		public Vector4d XWYW
		{
			get
			{
				return new Vector4d(X, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, X).
		/// </summary>
		public Vector4d XWZX
		{
			get
			{
				return new Vector4d(X, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Y).
		/// </summary>
		public Vector4d XWZY
		{
			get
			{
				return new Vector4d(X, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Z).
		/// </summary>
		public Vector4d XWZZ
		{
			get
			{
				return new Vector4d(X, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, W).
		/// </summary>
		public Vector4d XWZW
		{
			get
			{
				return new Vector4d(X, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, X).
		/// </summary>
		public Vector4d XWWX
		{
			get
			{
				return new Vector4d(X, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Y).
		/// </summary>
		public Vector4d XWWY
		{
			get
			{
				return new Vector4d(X, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Z).
		/// </summary>
		public Vector4d XWWZ
		{
			get
			{
				return new Vector4d(X, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, W).
		/// </summary>
		public Vector4d XWWW
		{
			get
			{
				return new Vector4d(X, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, X).
		/// </summary>
		public Vector4d YXXX
		{
			get
			{
				return new Vector4d(Y, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Y).
		/// </summary>
		public Vector4d YXXY
		{
			get
			{
				return new Vector4d(Y, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Z).
		/// </summary>
		public Vector4d YXXZ
		{
			get
			{
				return new Vector4d(Y, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, W).
		/// </summary>
		public Vector4d YXXW
		{
			get
			{
				return new Vector4d(Y, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, X).
		/// </summary>
		public Vector4d YXYX
		{
			get
			{
				return new Vector4d(Y, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Y).
		/// </summary>
		public Vector4d YXYY
		{
			get
			{
				return new Vector4d(Y, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Z).
		/// </summary>
		public Vector4d YXYZ
		{
			get
			{
				return new Vector4d(Y, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, W).
		/// </summary>
		public Vector4d YXYW
		{
			get
			{
				return new Vector4d(Y, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, X).
		/// </summary>
		public Vector4d YXZX
		{
			get
			{
				return new Vector4d(Y, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Y).
		/// </summary>
		public Vector4d YXZY
		{
			get
			{
				return new Vector4d(Y, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Z).
		/// </summary>
		public Vector4d YXZZ
		{
			get
			{
				return new Vector4d(Y, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, W).
		/// </summary>
		public Vector4d YXZW
		{
			get
			{
				return new Vector4d(Y, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, X).
		/// </summary>
		public Vector4d YXWX
		{
			get
			{
				return new Vector4d(Y, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Y).
		/// </summary>
		public Vector4d YXWY
		{
			get
			{
				return new Vector4d(Y, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Z).
		/// </summary>
		public Vector4d YXWZ
		{
			get
			{
				return new Vector4d(Y, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, W).
		/// </summary>
		public Vector4d YXWW
		{
			get
			{
				return new Vector4d(Y, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, X).
		/// </summary>
		public Vector4d YYXX
		{
			get
			{
				return new Vector4d(Y, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Y).
		/// </summary>
		public Vector4d YYXY
		{
			get
			{
				return new Vector4d(Y, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Z).
		/// </summary>
		public Vector4d YYXZ
		{
			get
			{
				return new Vector4d(Y, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, W).
		/// </summary>
		public Vector4d YYXW
		{
			get
			{
				return new Vector4d(Y, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, X).
		/// </summary>
		public Vector4d YYYX
		{
			get
			{
				return new Vector4d(Y, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Y).
		/// </summary>
		public Vector4d YYYY
		{
			get
			{
				return new Vector4d(Y, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Z).
		/// </summary>
		public Vector4d YYYZ
		{
			get
			{
				return new Vector4d(Y, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, W).
		/// </summary>
		public Vector4d YYYW
		{
			get
			{
				return new Vector4d(Y, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, X).
		/// </summary>
		public Vector4d YYZX
		{
			get
			{
				return new Vector4d(Y, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Y).
		/// </summary>
		public Vector4d YYZY
		{
			get
			{
				return new Vector4d(Y, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Z).
		/// </summary>
		public Vector4d YYZZ
		{
			get
			{
				return new Vector4d(Y, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, W).
		/// </summary>
		public Vector4d YYZW
		{
			get
			{
				return new Vector4d(Y, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, X).
		/// </summary>
		public Vector4d YYWX
		{
			get
			{
				return new Vector4d(Y, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Y).
		/// </summary>
		public Vector4d YYWY
		{
			get
			{
				return new Vector4d(Y, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Z).
		/// </summary>
		public Vector4d YYWZ
		{
			get
			{
				return new Vector4d(Y, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, W).
		/// </summary>
		public Vector4d YYWW
		{
			get
			{
				return new Vector4d(Y, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, X).
		/// </summary>
		public Vector4d YZXX
		{
			get
			{
				return new Vector4d(Y, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Y).
		/// </summary>
		public Vector4d YZXY
		{
			get
			{
				return new Vector4d(Y, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Z).
		/// </summary>
		public Vector4d YZXZ
		{
			get
			{
				return new Vector4d(Y, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, W).
		/// </summary>
		public Vector4d YZXW
		{
			get
			{
				return new Vector4d(Y, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, X).
		/// </summary>
		public Vector4d YZYX
		{
			get
			{
				return new Vector4d(Y, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Y).
		/// </summary>
		public Vector4d YZYY
		{
			get
			{
				return new Vector4d(Y, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Z).
		/// </summary>
		public Vector4d YZYZ
		{
			get
			{
				return new Vector4d(Y, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, W).
		/// </summary>
		public Vector4d YZYW
		{
			get
			{
				return new Vector4d(Y, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, X).
		/// </summary>
		public Vector4d YZZX
		{
			get
			{
				return new Vector4d(Y, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Y).
		/// </summary>
		public Vector4d YZZY
		{
			get
			{
				return new Vector4d(Y, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Z).
		/// </summary>
		public Vector4d YZZZ
		{
			get
			{
				return new Vector4d(Y, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, W).
		/// </summary>
		public Vector4d YZZW
		{
			get
			{
				return new Vector4d(Y, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, X).
		/// </summary>
		public Vector4d YZWX
		{
			get
			{
				return new Vector4d(Y, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Y).
		/// </summary>
		public Vector4d YZWY
		{
			get
			{
				return new Vector4d(Y, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Z).
		/// </summary>
		public Vector4d YZWZ
		{
			get
			{
				return new Vector4d(Y, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, W).
		/// </summary>
		public Vector4d YZWW
		{
			get
			{
				return new Vector4d(Y, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, X).
		/// </summary>
		public Vector4d YWXX
		{
			get
			{
				return new Vector4d(Y, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Y).
		/// </summary>
		public Vector4d YWXY
		{
			get
			{
				return new Vector4d(Y, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Z).
		/// </summary>
		public Vector4d YWXZ
		{
			get
			{
				return new Vector4d(Y, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, W).
		/// </summary>
		public Vector4d YWXW
		{
			get
			{
				return new Vector4d(Y, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, X).
		/// </summary>
		public Vector4d YWYX
		{
			get
			{
				return new Vector4d(Y, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Y).
		/// </summary>
		public Vector4d YWYY
		{
			get
			{
				return new Vector4d(Y, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Z).
		/// </summary>
		public Vector4d YWYZ
		{
			get
			{
				return new Vector4d(Y, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, W).
		/// </summary>
		public Vector4d YWYW
		{
			get
			{
				return new Vector4d(Y, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, X).
		/// </summary>
		public Vector4d YWZX
		{
			get
			{
				return new Vector4d(Y, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Y).
		/// </summary>
		public Vector4d YWZY
		{
			get
			{
				return new Vector4d(Y, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Z).
		/// </summary>
		public Vector4d YWZZ
		{
			get
			{
				return new Vector4d(Y, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, W).
		/// </summary>
		public Vector4d YWZW
		{
			get
			{
				return new Vector4d(Y, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, X).
		/// </summary>
		public Vector4d YWWX
		{
			get
			{
				return new Vector4d(Y, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Y).
		/// </summary>
		public Vector4d YWWY
		{
			get
			{
				return new Vector4d(Y, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Z).
		/// </summary>
		public Vector4d YWWZ
		{
			get
			{
				return new Vector4d(Y, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, W).
		/// </summary>
		public Vector4d YWWW
		{
			get
			{
				return new Vector4d(Y, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, X).
		/// </summary>
		public Vector4d ZXXX
		{
			get
			{
				return new Vector4d(Z, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Y).
		/// </summary>
		public Vector4d ZXXY
		{
			get
			{
				return new Vector4d(Z, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Z).
		/// </summary>
		public Vector4d ZXXZ
		{
			get
			{
				return new Vector4d(Z, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, W).
		/// </summary>
		public Vector4d ZXXW
		{
			get
			{
				return new Vector4d(Z, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, X).
		/// </summary>
		public Vector4d ZXYX
		{
			get
			{
				return new Vector4d(Z, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Y).
		/// </summary>
		public Vector4d ZXYY
		{
			get
			{
				return new Vector4d(Z, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Z).
		/// </summary>
		public Vector4d ZXYZ
		{
			get
			{
				return new Vector4d(Z, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, W).
		/// </summary>
		public Vector4d ZXYW
		{
			get
			{
				return new Vector4d(Z, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, X).
		/// </summary>
		public Vector4d ZXZX
		{
			get
			{
				return new Vector4d(Z, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Y).
		/// </summary>
		public Vector4d ZXZY
		{
			get
			{
				return new Vector4d(Z, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Z).
		/// </summary>
		public Vector4d ZXZZ
		{
			get
			{
				return new Vector4d(Z, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, W).
		/// </summary>
		public Vector4d ZXZW
		{
			get
			{
				return new Vector4d(Z, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, X).
		/// </summary>
		public Vector4d ZXWX
		{
			get
			{
				return new Vector4d(Z, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Y).
		/// </summary>
		public Vector4d ZXWY
		{
			get
			{
				return new Vector4d(Z, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Z).
		/// </summary>
		public Vector4d ZXWZ
		{
			get
			{
				return new Vector4d(Z, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, W).
		/// </summary>
		public Vector4d ZXWW
		{
			get
			{
				return new Vector4d(Z, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, X).
		/// </summary>
		public Vector4d ZYXX
		{
			get
			{
				return new Vector4d(Z, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Y).
		/// </summary>
		public Vector4d ZYXY
		{
			get
			{
				return new Vector4d(Z, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Z).
		/// </summary>
		public Vector4d ZYXZ
		{
			get
			{
				return new Vector4d(Z, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, W).
		/// </summary>
		public Vector4d ZYXW
		{
			get
			{
				return new Vector4d(Z, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, X).
		/// </summary>
		public Vector4d ZYYX
		{
			get
			{
				return new Vector4d(Z, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Y).
		/// </summary>
		public Vector4d ZYYY
		{
			get
			{
				return new Vector4d(Z, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Z).
		/// </summary>
		public Vector4d ZYYZ
		{
			get
			{
				return new Vector4d(Z, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, W).
		/// </summary>
		public Vector4d ZYYW
		{
			get
			{
				return new Vector4d(Z, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, X).
		/// </summary>
		public Vector4d ZYZX
		{
			get
			{
				return new Vector4d(Z, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Y).
		/// </summary>
		public Vector4d ZYZY
		{
			get
			{
				return new Vector4d(Z, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Z).
		/// </summary>
		public Vector4d ZYZZ
		{
			get
			{
				return new Vector4d(Z, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, W).
		/// </summary>
		public Vector4d ZYZW
		{
			get
			{
				return new Vector4d(Z, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, X).
		/// </summary>
		public Vector4d ZYWX
		{
			get
			{
				return new Vector4d(Z, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Y).
		/// </summary>
		public Vector4d ZYWY
		{
			get
			{
				return new Vector4d(Z, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Z).
		/// </summary>
		public Vector4d ZYWZ
		{
			get
			{
				return new Vector4d(Z, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, W).
		/// </summary>
		public Vector4d ZYWW
		{
			get
			{
				return new Vector4d(Z, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, X).
		/// </summary>
		public Vector4d ZZXX
		{
			get
			{
				return new Vector4d(Z, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Y).
		/// </summary>
		public Vector4d ZZXY
		{
			get
			{
				return new Vector4d(Z, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Z).
		/// </summary>
		public Vector4d ZZXZ
		{
			get
			{
				return new Vector4d(Z, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, W).
		/// </summary>
		public Vector4d ZZXW
		{
			get
			{
				return new Vector4d(Z, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, X).
		/// </summary>
		public Vector4d ZZYX
		{
			get
			{
				return new Vector4d(Z, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Y).
		/// </summary>
		public Vector4d ZZYY
		{
			get
			{
				return new Vector4d(Z, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Z).
		/// </summary>
		public Vector4d ZZYZ
		{
			get
			{
				return new Vector4d(Z, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, W).
		/// </summary>
		public Vector4d ZZYW
		{
			get
			{
				return new Vector4d(Z, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, X).
		/// </summary>
		public Vector4d ZZZX
		{
			get
			{
				return new Vector4d(Z, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Y).
		/// </summary>
		public Vector4d ZZZY
		{
			get
			{
				return new Vector4d(Z, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Z).
		/// </summary>
		public Vector4d ZZZZ
		{
			get
			{
				return new Vector4d(Z, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, W).
		/// </summary>
		public Vector4d ZZZW
		{
			get
			{
				return new Vector4d(Z, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, X).
		/// </summary>
		public Vector4d ZZWX
		{
			get
			{
				return new Vector4d(Z, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Y).
		/// </summary>
		public Vector4d ZZWY
		{
			get
			{
				return new Vector4d(Z, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Z).
		/// </summary>
		public Vector4d ZZWZ
		{
			get
			{
				return new Vector4d(Z, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, W).
		/// </summary>
		public Vector4d ZZWW
		{
			get
			{
				return new Vector4d(Z, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, X).
		/// </summary>
		public Vector4d ZWXX
		{
			get
			{
				return new Vector4d(Z, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Y).
		/// </summary>
		public Vector4d ZWXY
		{
			get
			{
				return new Vector4d(Z, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Z).
		/// </summary>
		public Vector4d ZWXZ
		{
			get
			{
				return new Vector4d(Z, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, W).
		/// </summary>
		public Vector4d ZWXW
		{
			get
			{
				return new Vector4d(Z, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, X).
		/// </summary>
		public Vector4d ZWYX
		{
			get
			{
				return new Vector4d(Z, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Y).
		/// </summary>
		public Vector4d ZWYY
		{
			get
			{
				return new Vector4d(Z, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Z).
		/// </summary>
		public Vector4d ZWYZ
		{
			get
			{
				return new Vector4d(Z, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, W).
		/// </summary>
		public Vector4d ZWYW
		{
			get
			{
				return new Vector4d(Z, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, X).
		/// </summary>
		public Vector4d ZWZX
		{
			get
			{
				return new Vector4d(Z, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Y).
		/// </summary>
		public Vector4d ZWZY
		{
			get
			{
				return new Vector4d(Z, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Z).
		/// </summary>
		public Vector4d ZWZZ
		{
			get
			{
				return new Vector4d(Z, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, W).
		/// </summary>
		public Vector4d ZWZW
		{
			get
			{
				return new Vector4d(Z, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, X).
		/// </summary>
		public Vector4d ZWWX
		{
			get
			{
				return new Vector4d(Z, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Y).
		/// </summary>
		public Vector4d ZWWY
		{
			get
			{
				return new Vector4d(Z, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Z).
		/// </summary>
		public Vector4d ZWWZ
		{
			get
			{
				return new Vector4d(Z, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, W).
		/// </summary>
		public Vector4d ZWWW
		{
			get
			{
				return new Vector4d(Z, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, X).
		/// </summary>
		public Vector4d WXXX
		{
			get
			{
				return new Vector4d(W, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Y).
		/// </summary>
		public Vector4d WXXY
		{
			get
			{
				return new Vector4d(W, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Z).
		/// </summary>
		public Vector4d WXXZ
		{
			get
			{
				return new Vector4d(W, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, W).
		/// </summary>
		public Vector4d WXXW
		{
			get
			{
				return new Vector4d(W, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, X).
		/// </summary>
		public Vector4d WXYX
		{
			get
			{
				return new Vector4d(W, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Y).
		/// </summary>
		public Vector4d WXYY
		{
			get
			{
				return new Vector4d(W, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Z).
		/// </summary>
		public Vector4d WXYZ
		{
			get
			{
				return new Vector4d(W, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, W).
		/// </summary>
		public Vector4d WXYW
		{
			get
			{
				return new Vector4d(W, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, X).
		/// </summary>
		public Vector4d WXZX
		{
			get
			{
				return new Vector4d(W, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Y).
		/// </summary>
		public Vector4d WXZY
		{
			get
			{
				return new Vector4d(W, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Z).
		/// </summary>
		public Vector4d WXZZ
		{
			get
			{
				return new Vector4d(W, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, W).
		/// </summary>
		public Vector4d WXZW
		{
			get
			{
				return new Vector4d(W, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, X).
		/// </summary>
		public Vector4d WXWX
		{
			get
			{
				return new Vector4d(W, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Y).
		/// </summary>
		public Vector4d WXWY
		{
			get
			{
				return new Vector4d(W, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Z).
		/// </summary>
		public Vector4d WXWZ
		{
			get
			{
				return new Vector4d(W, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, W).
		/// </summary>
		public Vector4d WXWW
		{
			get
			{
				return new Vector4d(W, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, X).
		/// </summary>
		public Vector4d WYXX
		{
			get
			{
				return new Vector4d(W, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Y).
		/// </summary>
		public Vector4d WYXY
		{
			get
			{
				return new Vector4d(W, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Z).
		/// </summary>
		public Vector4d WYXZ
		{
			get
			{
				return new Vector4d(W, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, W).
		/// </summary>
		public Vector4d WYXW
		{
			get
			{
				return new Vector4d(W, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, X).
		/// </summary>
		public Vector4d WYYX
		{
			get
			{
				return new Vector4d(W, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Y).
		/// </summary>
		public Vector4d WYYY
		{
			get
			{
				return new Vector4d(W, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Z).
		/// </summary>
		public Vector4d WYYZ
		{
			get
			{
				return new Vector4d(W, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, W).
		/// </summary>
		public Vector4d WYYW
		{
			get
			{
				return new Vector4d(W, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, X).
		/// </summary>
		public Vector4d WYZX
		{
			get
			{
				return new Vector4d(W, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Y).
		/// </summary>
		public Vector4d WYZY
		{
			get
			{
				return new Vector4d(W, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Z).
		/// </summary>
		public Vector4d WYZZ
		{
			get
			{
				return new Vector4d(W, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, W).
		/// </summary>
		public Vector4d WYZW
		{
			get
			{
				return new Vector4d(W, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, X).
		/// </summary>
		public Vector4d WYWX
		{
			get
			{
				return new Vector4d(W, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Y).
		/// </summary>
		public Vector4d WYWY
		{
			get
			{
				return new Vector4d(W, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Z).
		/// </summary>
		public Vector4d WYWZ
		{
			get
			{
				return new Vector4d(W, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, W).
		/// </summary>
		public Vector4d WYWW
		{
			get
			{
				return new Vector4d(W, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, X).
		/// </summary>
		public Vector4d WZXX
		{
			get
			{
				return new Vector4d(W, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Y).
		/// </summary>
		public Vector4d WZXY
		{
			get
			{
				return new Vector4d(W, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Z).
		/// </summary>
		public Vector4d WZXZ
		{
			get
			{
				return new Vector4d(W, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, W).
		/// </summary>
		public Vector4d WZXW
		{
			get
			{
				return new Vector4d(W, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, X).
		/// </summary>
		public Vector4d WZYX
		{
			get
			{
				return new Vector4d(W, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Y).
		/// </summary>
		public Vector4d WZYY
		{
			get
			{
				return new Vector4d(W, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Z).
		/// </summary>
		public Vector4d WZYZ
		{
			get
			{
				return new Vector4d(W, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, W).
		/// </summary>
		public Vector4d WZYW
		{
			get
			{
				return new Vector4d(W, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, X).
		/// </summary>
		public Vector4d WZZX
		{
			get
			{
				return new Vector4d(W, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Y).
		/// </summary>
		public Vector4d WZZY
		{
			get
			{
				return new Vector4d(W, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Z).
		/// </summary>
		public Vector4d WZZZ
		{
			get
			{
				return new Vector4d(W, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, W).
		/// </summary>
		public Vector4d WZZW
		{
			get
			{
				return new Vector4d(W, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, X).
		/// </summary>
		public Vector4d WZWX
		{
			get
			{
				return new Vector4d(W, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Y).
		/// </summary>
		public Vector4d WZWY
		{
			get
			{
				return new Vector4d(W, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Z).
		/// </summary>
		public Vector4d WZWZ
		{
			get
			{
				return new Vector4d(W, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, W).
		/// </summary>
		public Vector4d WZWW
		{
			get
			{
				return new Vector4d(W, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, X).
		/// </summary>
		public Vector4d WWXX
		{
			get
			{
				return new Vector4d(W, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Y).
		/// </summary>
		public Vector4d WWXY
		{
			get
			{
				return new Vector4d(W, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Z).
		/// </summary>
		public Vector4d WWXZ
		{
			get
			{
				return new Vector4d(W, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, W).
		/// </summary>
		public Vector4d WWXW
		{
			get
			{
				return new Vector4d(W, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, X).
		/// </summary>
		public Vector4d WWYX
		{
			get
			{
				return new Vector4d(W, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Y).
		/// </summary>
		public Vector4d WWYY
		{
			get
			{
				return new Vector4d(W, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Z).
		/// </summary>
		public Vector4d WWYZ
		{
			get
			{
				return new Vector4d(W, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, W).
		/// </summary>
		public Vector4d WWYW
		{
			get
			{
				return new Vector4d(W, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, X).
		/// </summary>
		public Vector4d WWZX
		{
			get
			{
				return new Vector4d(W, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Y).
		/// </summary>
		public Vector4d WWZY
		{
			get
			{
				return new Vector4d(W, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Z).
		/// </summary>
		public Vector4d WWZZ
		{
			get
			{
				return new Vector4d(W, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, W).
		/// </summary>
		public Vector4d WWZW
		{
			get
			{
				return new Vector4d(W, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, X).
		/// </summary>
		public Vector4d WWWX
		{
			get
			{
				return new Vector4d(W, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Y).
		/// </summary>
		public Vector4d WWWY
		{
			get
			{
				return new Vector4d(W, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Z).
		/// </summary>
		public Vector4d WWWZ
		{
			get
			{
				return new Vector4d(W, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, W).
		/// </summary>
		public Vector4d WWWW
		{
			get
			{
				return new Vector4d(W, W, W, W);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4d"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector4d(double value)
		{
			X = value;
			Y = value;
			Z = value;
			W = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4d"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X and Y components</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4d(Vector2d value, double z, double w)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4d"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X, Y and Z components</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4d(Vector3d value, double w)
		{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4d"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4d(double x, double y, double z, double w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4d"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector4d(double[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
			W = array[3];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4d"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector4d(double[] array, int offset)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[offset + 0];
			Y = array[offset + 1];
			Z = array[offset + 2];
			W = array[offset + 3];
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The identity of value.</returns>
		public static Vector4d operator +(Vector4d value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4d operator -(Vector4d value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4d operator +(Vector4d left, Vector4d right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4d operator -(Vector4d left, Vector4d right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4d operator *(Vector4d left, double right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4d operator *(double left, Vector4d right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4d operator /(Vector4d left, double right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a Vector4f value to a Vector4d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4d.</param>
		/// <returns>A Vector4d that has all components equal to value.</returns>
		public static implicit operator Vector4d(Vector4f value)
		{
			return new Vector4d((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4h value to a Vector4d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4d.</param>
		/// <returns>A Vector4d that has all components equal to value.</returns>
		public static implicit operator Vector4d(Vector4h value)
		{
			return new Vector4d((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4ul value to a Vector4d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4d.</param>
		/// <returns>A Vector4d that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector4d(Vector4ul value)
		{
			return new Vector4d((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4l value to a Vector4d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4d.</param>
		/// <returns>A Vector4d that has all components equal to value.</returns>
		public static implicit operator Vector4d(Vector4l value)
		{
			return new Vector4d((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4ui value to a Vector4d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4d.</param>
		/// <returns>A Vector4d that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector4d(Vector4ui value)
		{
			return new Vector4d((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4i value to a Vector4d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4d.</param>
		/// <returns>A Vector4d that has all components equal to value.</returns>
		public static implicit operator Vector4d(Vector4i value)
		{
			return new Vector4d((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4us value to a Vector4d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4d.</param>
		/// <returns>A Vector4d that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector4d(Vector4us value)
		{
			return new Vector4d((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4s value to a Vector4d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4d.</param>
		/// <returns>A Vector4d that has all components equal to value.</returns>
		public static implicit operator Vector4d(Vector4s value)
		{
			return new Vector4d((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4b value to a Vector4d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4d.</param>
		/// <returns>A Vector4d that has all components equal to value.</returns>
		public static implicit operator Vector4d(Vector4b value)
		{
			return new Vector4d((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4sb value to a Vector4d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4d.</param>
		/// <returns>A Vector4d that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector4d(Vector4sb value)
		{
			return new Vector4d((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector4d"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Vector4d"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector4d"/> object, and its value
		/// is equal to the current <see cref="Vector4d"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector4d) { return Equals((Vector4d)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector4d other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector4d left, Vector4d right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z & left.W == right.W;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector4d left, Vector4d right)
		{
			return left.X != right.X | left.Y != right.Y | left.Z != right.Z | left.W != right.W;
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
			return String.Format("({0}, {1}, {2}, {3})", X.ToString(format, provider), Y.ToString(format, provider), Z.ToString(format, provider), W.ToString(format, provider));
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
		/// Writes the given <see cref="Vector4d"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector4d vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
			writer.Write(vector.Z);
			writer.Write(vector.W);
		}
		/// <summary>
		/// Reads a <see cref="Vector4d"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector4d ReadVector4d(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector4d(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4d Negative(Vector4d value)
		{
			return new Vector4d(-value.X, -value.Y, -value.Z, -value.W);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4d Add(Vector4d left, Vector4d right)
		{
			return new Vector4d(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4d Subtract(Vector4d left, Vector4d right)
		{
			return new Vector4d(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4d Multiply(Vector4d vector, double scalar)
		{
			return new Vector4d(vector.X * scalar, vector.Y * scalar, vector.Z * scalar, vector.W * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4d Divide(Vector4d vector, double scalar)
		{
			return new Vector4d(vector.X / scalar, vector.Y / scalar, vector.Z / scalar, vector.W / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Vector4d left, Vector4d right)
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
		public static double Dot(Vector4d left, Vector4d right)
		{
			return left.X * right.X + left.Y * right.Y + left.Z * right.Z + left.W * right.W;
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all components of a vector are non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if all components are non-zero; false otherwise.</returns>
		public static bool All(Vector4d value)
		{
			return value.X != 0 && value.Y != 0 && value.Z != 0 && value.W != 0;
		}
		/// <summary>
		/// Determines whether all components of a vector satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if every component of the vector passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Vector4d value, Predicate<double> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z) && predicate(value.W);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Vector4d value)
		{
			return value.X != 0 || value.Y != 0 || value.Z != 0 || value.W != 0;
		}
		/// <summary>
		/// Determines whether any components of a vector satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if any component of the vector passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Vector4d value, Predicate<double> predicate)
		{
			return predicate(value.X) || predicate(value.Y) || predicate(value.Z) || predicate(value.W);
		}
		#endregion
		#region Properties
		/// <summary>
		/// Computes the absolute squared value of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute squared value of value.</returns> 
		public static double AbsoluteSquared(Vector4d value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		public static double Absolute(Vector4d value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		public static Vector4d Normalize(Vector4d value)
		{
			return (Vector4d)value / Absolute(value);
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
		public static Vector4d Transform(Vector4d value, Func<double, double> transformer)
		{
			return new Vector4d(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4f Transform(Vector4d value, Func<double, float> transformer)
		{
			return new Vector4f(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4h Transform(Vector4d value, Func<double, Half> transformer)
		{
			return new Vector4h(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4ul Transform(Vector4d value, Func<double, ulong> transformer)
		{
			return new Vector4ul(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4l Transform(Vector4d value, Func<double, long> transformer)
		{
			return new Vector4l(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4ui Transform(Vector4d value, Func<double, uint> transformer)
		{
			return new Vector4ui(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4i Transform(Vector4d value, Func<double, int> transformer)
		{
			return new Vector4i(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4us Transform(Vector4d value, Func<double, ushort> transformer)
		{
			return new Vector4us(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4s Transform(Vector4d value, Func<double, short> transformer)
		{
			return new Vector4s(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4b Transform(Vector4d value, Func<double, byte> transformer)
		{
			return new Vector4b(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4sb Transform(Vector4d value, Func<double, sbyte> transformer)
		{
			return new Vector4sb(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		#endregion
		/// <summary>
		/// Multiplys the components of two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first vector to modulate.</param>
		/// <param name="right">The second vector to modulate.</param>
		/// <returns>The result of multiplying each component of left by the matching component in right.</returns>
		public static Vector4d Modulate(Vector4d left, Vector4d right)
		{
			return new Vector4d(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Vector4d Abs(Vector4d value)
		{
			return new Vector4d(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z), Functions.Abs(value.W));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Vector4d Min(Vector4d value1, Vector4d value2)
		{
			return new Vector4d(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z), Functions.Min(value1.W, value2.W));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Vector4d Max(Vector4d value1, Vector4d value2)
		{
			return new Vector4d(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z), Functions.Max(value1.W, value2.W));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		public static Vector4d Clamp(Vector4d value, Vector4d min, Vector4d max)
		{
			return new Vector4d(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z), Functions.Clamp(value.W, min.W, max.W));
		}
		/// <summary>
		/// Constrains each component to the range 0 to 1.
		/// </summary>
		/// <param name="value">A vector to saturate.</param>
		/// <returns>A vector with each component constrained to the range 0 to 1.</returns>
		public static Vector4d Saturate(Vector4d value)
		{
			return new Vector4d(Functions.Saturate(value.X), Functions.Saturate(value.Y), Functions.Saturate(value.Z), Functions.Saturate(value.W));
		}
		/// <summary>
		/// Returns a vector where each component is the smallest integral value that
		/// is greater than or equal to the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The ceiling of value.</returns>
		public static Vector4d Ceiling(Vector4d value)
		{
			return new Vector4d(Functions.Ceiling(value.X), Functions.Ceiling(value.Y), Functions.Ceiling(value.Z), Functions.Ceiling(value.W));
		}
		/// <summary>
		/// Returns a vector where each component is the largest integral value that
		/// is less than or equal to the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The floor of value.</returns>
		public static Vector4d Floor(Vector4d value)
		{
			return new Vector4d(Functions.Floor(value.X), Functions.Floor(value.Y), Functions.Floor(value.Z), Functions.Floor(value.W));
		}
		/// <summary>
		/// Returns a vector where each component is the integral part of the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The integral of value.</returns>
		public static Vector4d Truncate(Vector4d value)
		{
			return new Vector4d(Functions.Truncate(value.X), Functions.Truncate(value.Y), Functions.Truncate(value.Z), Functions.Truncate(value.W));
		}
		/// <summary>
		/// Returns a vector where each component is the fractional part of the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The fractional of value.</returns>
		public static Vector4d Fractional(Vector4d value)
		{
			return new Vector4d(Functions.Fractional(value.X), Functions.Fractional(value.Y), Functions.Fractional(value.Z), Functions.Fractional(value.W));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector4d Round(Vector4d value)
		{
			return new Vector4d(Functions.Round(value.X), Functions.Round(value.Y), Functions.Round(value.Z), Functions.Round(value.W));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector4d Round(Vector4d value, int digits)
		{
			return new Vector4d(Functions.Round(value.X, digits), Functions.Round(value.Y, digits), Functions.Round(value.Z, digits), Functions.Round(value.W, digits));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector4d Round(Vector4d value, MidpointRounding mode)
		{
			return new Vector4d(Functions.Round(value.X, mode), Functions.Round(value.Y, mode), Functions.Round(value.Z, mode), Functions.Round(value.W, mode));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector4d Round(Vector4d value, int digits, MidpointRounding mode)
		{
			return new Vector4d(Functions.Round(value.X, digits, mode), Functions.Round(value.Y, digits, mode), Functions.Round(value.Z, digits, mode), Functions.Round(value.W, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each component in the vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>A vector with the reciprocal of each of values components.</returns>
		public static Vector4d Reciprocal(Vector4d value)
		{
			return new Vector4d(1 / value.X, 1 / value.Y, 1 / value.Z, 1 / value.W);
		}
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
		public static Vector4d Barycentric(Vector4d value1, Vector4d value2, Vector4d value3, double amount1, double amount2)
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
		public static Vector4d Reflect(Vector4d vector, Vector4d normal)
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
		public static Vector4d Refract(Vector4d vector, Vector4d normal, double index)
		{
			var cos1 = Dot(vector, normal);
			var radicand = 1 - (index * index) * (1 - (cos1 * cos1));
			if (radicand < 0)
			{
				return Vector4d.Zero;
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
		public static Vector4d Lerp(Vector4d value1, Vector4d value2, double amount)
		{
			return (1 - amount) * value1 + amount * value2;
		}
		#endregion
	}
}
