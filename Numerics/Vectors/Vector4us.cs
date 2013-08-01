using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Numerics
{
	/// <summary>
	/// Represents a four component vector of ushorts, of the form (X, Y, Z, W).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	[CLSCompliant(false)]
	public struct Vector4us: IEquatable<Vector4us>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector4us"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector4us Zero = new Vector4us(0);
		/// <summary>
		/// Returns a new <see cref="Vector4us"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector4us One = new Vector4us(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector4us"/> (1, 0, 0, 0).
		/// </summary>
		public static readonly Vector4us UnitX = new Vector4us(1, 0, 0, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector4us"/> (0, 1, 0, 0).
		/// </summary>
		public static readonly Vector4us UnitY = new Vector4us(0, 1, 0, 0);
		/// <summary>
		/// Returns the Z unit <see cref="Vector4us"/> (0, 0, 1, 0).
		/// </summary>
		public static readonly Vector4us UnitZ = new Vector4us(0, 0, 1, 0);
		/// <summary>
		/// Returns the W unit <see cref="Vector4us"/> (0, 0, 0, 1).
		/// </summary>
		public static readonly Vector4us UnitW = new Vector4us(0, 0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the vector.
		/// </summary>
		public readonly ushort X;
		/// <summary>
		/// The Y component of the vector.
		/// </summary>
		public readonly ushort Y;
		/// <summary>
		/// The Z component of the vector.
		/// </summary>
		public readonly ushort Z;
		/// <summary>
		/// The W component of the vector.
		/// </summary>
		public readonly ushort W;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public ushort this[int index]
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
						throw new IndexOutOfRangeException("Indices for Vector4us run from 0 to 3, inclusive.");
				}
			}
		}
		public ushort[] ToArray()
		{
			return new ushort[]
			{
				X, Y, Z, W
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the vector (X, X).
		/// </summary>
		public Vector2us XX
		{
			get
			{
				return new Vector2us(X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y).
		/// </summary>
		public Vector2us XY
		{
			get
			{
				return new Vector2us(X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z).
		/// </summary>
		public Vector2us XZ
		{
			get
			{
				return new Vector2us(X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W).
		/// </summary>
		public Vector2us XW
		{
			get
			{
				return new Vector2us(X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X).
		/// </summary>
		public Vector2us YX
		{
			get
			{
				return new Vector2us(Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y).
		/// </summary>
		public Vector2us YY
		{
			get
			{
				return new Vector2us(Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z).
		/// </summary>
		public Vector2us YZ
		{
			get
			{
				return new Vector2us(Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W).
		/// </summary>
		public Vector2us YW
		{
			get
			{
				return new Vector2us(Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X).
		/// </summary>
		public Vector2us ZX
		{
			get
			{
				return new Vector2us(Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y).
		/// </summary>
		public Vector2us ZY
		{
			get
			{
				return new Vector2us(Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z).
		/// </summary>
		public Vector2us ZZ
		{
			get
			{
				return new Vector2us(Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W).
		/// </summary>
		public Vector2us ZW
		{
			get
			{
				return new Vector2us(Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X).
		/// </summary>
		public Vector2us WX
		{
			get
			{
				return new Vector2us(W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y).
		/// </summary>
		public Vector2us WY
		{
			get
			{
				return new Vector2us(W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z).
		/// </summary>
		public Vector2us WZ
		{
			get
			{
				return new Vector2us(W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W).
		/// </summary>
		public Vector2us WW
		{
			get
			{
				return new Vector2us(W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X).
		/// </summary>
		public Vector3us XXX
		{
			get
			{
				return new Vector3us(X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y).
		/// </summary>
		public Vector3us XXY
		{
			get
			{
				return new Vector3us(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z).
		/// </summary>
		public Vector3us XXZ
		{
			get
			{
				return new Vector3us(X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W).
		/// </summary>
		public Vector3us XXW
		{
			get
			{
				return new Vector3us(X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X).
		/// </summary>
		public Vector3us XYX
		{
			get
			{
				return new Vector3us(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y).
		/// </summary>
		public Vector3us XYY
		{
			get
			{
				return new Vector3us(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z).
		/// </summary>
		public Vector3us XYZ
		{
			get
			{
				return new Vector3us(X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W).
		/// </summary>
		public Vector3us XYW
		{
			get
			{
				return new Vector3us(X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X).
		/// </summary>
		public Vector3us XZX
		{
			get
			{
				return new Vector3us(X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y).
		/// </summary>
		public Vector3us XZY
		{
			get
			{
				return new Vector3us(X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z).
		/// </summary>
		public Vector3us XZZ
		{
			get
			{
				return new Vector3us(X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W).
		/// </summary>
		public Vector3us XZW
		{
			get
			{
				return new Vector3us(X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X).
		/// </summary>
		public Vector3us XWX
		{
			get
			{
				return new Vector3us(X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y).
		/// </summary>
		public Vector3us XWY
		{
			get
			{
				return new Vector3us(X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z).
		/// </summary>
		public Vector3us XWZ
		{
			get
			{
				return new Vector3us(X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W).
		/// </summary>
		public Vector3us XWW
		{
			get
			{
				return new Vector3us(X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X).
		/// </summary>
		public Vector3us YXX
		{
			get
			{
				return new Vector3us(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y).
		/// </summary>
		public Vector3us YXY
		{
			get
			{
				return new Vector3us(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z).
		/// </summary>
		public Vector3us YXZ
		{
			get
			{
				return new Vector3us(Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W).
		/// </summary>
		public Vector3us YXW
		{
			get
			{
				return new Vector3us(Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X).
		/// </summary>
		public Vector3us YYX
		{
			get
			{
				return new Vector3us(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y).
		/// </summary>
		public Vector3us YYY
		{
			get
			{
				return new Vector3us(Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z).
		/// </summary>
		public Vector3us YYZ
		{
			get
			{
				return new Vector3us(Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W).
		/// </summary>
		public Vector3us YYW
		{
			get
			{
				return new Vector3us(Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X).
		/// </summary>
		public Vector3us YZX
		{
			get
			{
				return new Vector3us(Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y).
		/// </summary>
		public Vector3us YZY
		{
			get
			{
				return new Vector3us(Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z).
		/// </summary>
		public Vector3us YZZ
		{
			get
			{
				return new Vector3us(Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W).
		/// </summary>
		public Vector3us YZW
		{
			get
			{
				return new Vector3us(Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X).
		/// </summary>
		public Vector3us YWX
		{
			get
			{
				return new Vector3us(Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y).
		/// </summary>
		public Vector3us YWY
		{
			get
			{
				return new Vector3us(Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z).
		/// </summary>
		public Vector3us YWZ
		{
			get
			{
				return new Vector3us(Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W).
		/// </summary>
		public Vector3us YWW
		{
			get
			{
				return new Vector3us(Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X).
		/// </summary>
		public Vector3us ZXX
		{
			get
			{
				return new Vector3us(Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y).
		/// </summary>
		public Vector3us ZXY
		{
			get
			{
				return new Vector3us(Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z).
		/// </summary>
		public Vector3us ZXZ
		{
			get
			{
				return new Vector3us(Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W).
		/// </summary>
		public Vector3us ZXW
		{
			get
			{
				return new Vector3us(Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X).
		/// </summary>
		public Vector3us ZYX
		{
			get
			{
				return new Vector3us(Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y).
		/// </summary>
		public Vector3us ZYY
		{
			get
			{
				return new Vector3us(Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z).
		/// </summary>
		public Vector3us ZYZ
		{
			get
			{
				return new Vector3us(Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W).
		/// </summary>
		public Vector3us ZYW
		{
			get
			{
				return new Vector3us(Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X).
		/// </summary>
		public Vector3us ZZX
		{
			get
			{
				return new Vector3us(Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y).
		/// </summary>
		public Vector3us ZZY
		{
			get
			{
				return new Vector3us(Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z).
		/// </summary>
		public Vector3us ZZZ
		{
			get
			{
				return new Vector3us(Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W).
		/// </summary>
		public Vector3us ZZW
		{
			get
			{
				return new Vector3us(Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X).
		/// </summary>
		public Vector3us ZWX
		{
			get
			{
				return new Vector3us(Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y).
		/// </summary>
		public Vector3us ZWY
		{
			get
			{
				return new Vector3us(Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z).
		/// </summary>
		public Vector3us ZWZ
		{
			get
			{
				return new Vector3us(Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W).
		/// </summary>
		public Vector3us ZWW
		{
			get
			{
				return new Vector3us(Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X).
		/// </summary>
		public Vector3us WXX
		{
			get
			{
				return new Vector3us(W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y).
		/// </summary>
		public Vector3us WXY
		{
			get
			{
				return new Vector3us(W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z).
		/// </summary>
		public Vector3us WXZ
		{
			get
			{
				return new Vector3us(W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W).
		/// </summary>
		public Vector3us WXW
		{
			get
			{
				return new Vector3us(W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X).
		/// </summary>
		public Vector3us WYX
		{
			get
			{
				return new Vector3us(W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y).
		/// </summary>
		public Vector3us WYY
		{
			get
			{
				return new Vector3us(W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z).
		/// </summary>
		public Vector3us WYZ
		{
			get
			{
				return new Vector3us(W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W).
		/// </summary>
		public Vector3us WYW
		{
			get
			{
				return new Vector3us(W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X).
		/// </summary>
		public Vector3us WZX
		{
			get
			{
				return new Vector3us(W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y).
		/// </summary>
		public Vector3us WZY
		{
			get
			{
				return new Vector3us(W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z).
		/// </summary>
		public Vector3us WZZ
		{
			get
			{
				return new Vector3us(W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W).
		/// </summary>
		public Vector3us WZW
		{
			get
			{
				return new Vector3us(W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X).
		/// </summary>
		public Vector3us WWX
		{
			get
			{
				return new Vector3us(W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y).
		/// </summary>
		public Vector3us WWY
		{
			get
			{
				return new Vector3us(W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z).
		/// </summary>
		public Vector3us WWZ
		{
			get
			{
				return new Vector3us(W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W).
		/// </summary>
		public Vector3us WWW
		{
			get
			{
				return new Vector3us(W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, X).
		/// </summary>
		public Vector4us XXXX
		{
			get
			{
				return new Vector4us(X, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Y).
		/// </summary>
		public Vector4us XXXY
		{
			get
			{
				return new Vector4us(X, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Z).
		/// </summary>
		public Vector4us XXXZ
		{
			get
			{
				return new Vector4us(X, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, W).
		/// </summary>
		public Vector4us XXXW
		{
			get
			{
				return new Vector4us(X, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, X).
		/// </summary>
		public Vector4us XXYX
		{
			get
			{
				return new Vector4us(X, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Y).
		/// </summary>
		public Vector4us XXYY
		{
			get
			{
				return new Vector4us(X, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Z).
		/// </summary>
		public Vector4us XXYZ
		{
			get
			{
				return new Vector4us(X, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, W).
		/// </summary>
		public Vector4us XXYW
		{
			get
			{
				return new Vector4us(X, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, X).
		/// </summary>
		public Vector4us XXZX
		{
			get
			{
				return new Vector4us(X, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Y).
		/// </summary>
		public Vector4us XXZY
		{
			get
			{
				return new Vector4us(X, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Z).
		/// </summary>
		public Vector4us XXZZ
		{
			get
			{
				return new Vector4us(X, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, W).
		/// </summary>
		public Vector4us XXZW
		{
			get
			{
				return new Vector4us(X, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, X).
		/// </summary>
		public Vector4us XXWX
		{
			get
			{
				return new Vector4us(X, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Y).
		/// </summary>
		public Vector4us XXWY
		{
			get
			{
				return new Vector4us(X, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Z).
		/// </summary>
		public Vector4us XXWZ
		{
			get
			{
				return new Vector4us(X, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, W).
		/// </summary>
		public Vector4us XXWW
		{
			get
			{
				return new Vector4us(X, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, X).
		/// </summary>
		public Vector4us XYXX
		{
			get
			{
				return new Vector4us(X, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Y).
		/// </summary>
		public Vector4us XYXY
		{
			get
			{
				return new Vector4us(X, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Z).
		/// </summary>
		public Vector4us XYXZ
		{
			get
			{
				return new Vector4us(X, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, W).
		/// </summary>
		public Vector4us XYXW
		{
			get
			{
				return new Vector4us(X, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, X).
		/// </summary>
		public Vector4us XYYX
		{
			get
			{
				return new Vector4us(X, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Y).
		/// </summary>
		public Vector4us XYYY
		{
			get
			{
				return new Vector4us(X, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Z).
		/// </summary>
		public Vector4us XYYZ
		{
			get
			{
				return new Vector4us(X, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, W).
		/// </summary>
		public Vector4us XYYW
		{
			get
			{
				return new Vector4us(X, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, X).
		/// </summary>
		public Vector4us XYZX
		{
			get
			{
				return new Vector4us(X, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Y).
		/// </summary>
		public Vector4us XYZY
		{
			get
			{
				return new Vector4us(X, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Z).
		/// </summary>
		public Vector4us XYZZ
		{
			get
			{
				return new Vector4us(X, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, W).
		/// </summary>
		public Vector4us XYZW
		{
			get
			{
				return new Vector4us(X, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, X).
		/// </summary>
		public Vector4us XYWX
		{
			get
			{
				return new Vector4us(X, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Y).
		/// </summary>
		public Vector4us XYWY
		{
			get
			{
				return new Vector4us(X, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Z).
		/// </summary>
		public Vector4us XYWZ
		{
			get
			{
				return new Vector4us(X, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, W).
		/// </summary>
		public Vector4us XYWW
		{
			get
			{
				return new Vector4us(X, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, X).
		/// </summary>
		public Vector4us XZXX
		{
			get
			{
				return new Vector4us(X, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Y).
		/// </summary>
		public Vector4us XZXY
		{
			get
			{
				return new Vector4us(X, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Z).
		/// </summary>
		public Vector4us XZXZ
		{
			get
			{
				return new Vector4us(X, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, W).
		/// </summary>
		public Vector4us XZXW
		{
			get
			{
				return new Vector4us(X, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, X).
		/// </summary>
		public Vector4us XZYX
		{
			get
			{
				return new Vector4us(X, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Y).
		/// </summary>
		public Vector4us XZYY
		{
			get
			{
				return new Vector4us(X, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Z).
		/// </summary>
		public Vector4us XZYZ
		{
			get
			{
				return new Vector4us(X, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, W).
		/// </summary>
		public Vector4us XZYW
		{
			get
			{
				return new Vector4us(X, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, X).
		/// </summary>
		public Vector4us XZZX
		{
			get
			{
				return new Vector4us(X, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Y).
		/// </summary>
		public Vector4us XZZY
		{
			get
			{
				return new Vector4us(X, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Z).
		/// </summary>
		public Vector4us XZZZ
		{
			get
			{
				return new Vector4us(X, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, W).
		/// </summary>
		public Vector4us XZZW
		{
			get
			{
				return new Vector4us(X, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, X).
		/// </summary>
		public Vector4us XZWX
		{
			get
			{
				return new Vector4us(X, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Y).
		/// </summary>
		public Vector4us XZWY
		{
			get
			{
				return new Vector4us(X, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Z).
		/// </summary>
		public Vector4us XZWZ
		{
			get
			{
				return new Vector4us(X, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, W).
		/// </summary>
		public Vector4us XZWW
		{
			get
			{
				return new Vector4us(X, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, X).
		/// </summary>
		public Vector4us XWXX
		{
			get
			{
				return new Vector4us(X, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Y).
		/// </summary>
		public Vector4us XWXY
		{
			get
			{
				return new Vector4us(X, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Z).
		/// </summary>
		public Vector4us XWXZ
		{
			get
			{
				return new Vector4us(X, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, W).
		/// </summary>
		public Vector4us XWXW
		{
			get
			{
				return new Vector4us(X, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, X).
		/// </summary>
		public Vector4us XWYX
		{
			get
			{
				return new Vector4us(X, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Y).
		/// </summary>
		public Vector4us XWYY
		{
			get
			{
				return new Vector4us(X, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Z).
		/// </summary>
		public Vector4us XWYZ
		{
			get
			{
				return new Vector4us(X, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, W).
		/// </summary>
		public Vector4us XWYW
		{
			get
			{
				return new Vector4us(X, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, X).
		/// </summary>
		public Vector4us XWZX
		{
			get
			{
				return new Vector4us(X, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Y).
		/// </summary>
		public Vector4us XWZY
		{
			get
			{
				return new Vector4us(X, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Z).
		/// </summary>
		public Vector4us XWZZ
		{
			get
			{
				return new Vector4us(X, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, W).
		/// </summary>
		public Vector4us XWZW
		{
			get
			{
				return new Vector4us(X, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, X).
		/// </summary>
		public Vector4us XWWX
		{
			get
			{
				return new Vector4us(X, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Y).
		/// </summary>
		public Vector4us XWWY
		{
			get
			{
				return new Vector4us(X, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Z).
		/// </summary>
		public Vector4us XWWZ
		{
			get
			{
				return new Vector4us(X, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, W).
		/// </summary>
		public Vector4us XWWW
		{
			get
			{
				return new Vector4us(X, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, X).
		/// </summary>
		public Vector4us YXXX
		{
			get
			{
				return new Vector4us(Y, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Y).
		/// </summary>
		public Vector4us YXXY
		{
			get
			{
				return new Vector4us(Y, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Z).
		/// </summary>
		public Vector4us YXXZ
		{
			get
			{
				return new Vector4us(Y, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, W).
		/// </summary>
		public Vector4us YXXW
		{
			get
			{
				return new Vector4us(Y, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, X).
		/// </summary>
		public Vector4us YXYX
		{
			get
			{
				return new Vector4us(Y, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Y).
		/// </summary>
		public Vector4us YXYY
		{
			get
			{
				return new Vector4us(Y, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Z).
		/// </summary>
		public Vector4us YXYZ
		{
			get
			{
				return new Vector4us(Y, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, W).
		/// </summary>
		public Vector4us YXYW
		{
			get
			{
				return new Vector4us(Y, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, X).
		/// </summary>
		public Vector4us YXZX
		{
			get
			{
				return new Vector4us(Y, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Y).
		/// </summary>
		public Vector4us YXZY
		{
			get
			{
				return new Vector4us(Y, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Z).
		/// </summary>
		public Vector4us YXZZ
		{
			get
			{
				return new Vector4us(Y, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, W).
		/// </summary>
		public Vector4us YXZW
		{
			get
			{
				return new Vector4us(Y, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, X).
		/// </summary>
		public Vector4us YXWX
		{
			get
			{
				return new Vector4us(Y, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Y).
		/// </summary>
		public Vector4us YXWY
		{
			get
			{
				return new Vector4us(Y, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Z).
		/// </summary>
		public Vector4us YXWZ
		{
			get
			{
				return new Vector4us(Y, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, W).
		/// </summary>
		public Vector4us YXWW
		{
			get
			{
				return new Vector4us(Y, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, X).
		/// </summary>
		public Vector4us YYXX
		{
			get
			{
				return new Vector4us(Y, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Y).
		/// </summary>
		public Vector4us YYXY
		{
			get
			{
				return new Vector4us(Y, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Z).
		/// </summary>
		public Vector4us YYXZ
		{
			get
			{
				return new Vector4us(Y, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, W).
		/// </summary>
		public Vector4us YYXW
		{
			get
			{
				return new Vector4us(Y, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, X).
		/// </summary>
		public Vector4us YYYX
		{
			get
			{
				return new Vector4us(Y, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Y).
		/// </summary>
		public Vector4us YYYY
		{
			get
			{
				return new Vector4us(Y, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Z).
		/// </summary>
		public Vector4us YYYZ
		{
			get
			{
				return new Vector4us(Y, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, W).
		/// </summary>
		public Vector4us YYYW
		{
			get
			{
				return new Vector4us(Y, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, X).
		/// </summary>
		public Vector4us YYZX
		{
			get
			{
				return new Vector4us(Y, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Y).
		/// </summary>
		public Vector4us YYZY
		{
			get
			{
				return new Vector4us(Y, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Z).
		/// </summary>
		public Vector4us YYZZ
		{
			get
			{
				return new Vector4us(Y, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, W).
		/// </summary>
		public Vector4us YYZW
		{
			get
			{
				return new Vector4us(Y, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, X).
		/// </summary>
		public Vector4us YYWX
		{
			get
			{
				return new Vector4us(Y, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Y).
		/// </summary>
		public Vector4us YYWY
		{
			get
			{
				return new Vector4us(Y, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Z).
		/// </summary>
		public Vector4us YYWZ
		{
			get
			{
				return new Vector4us(Y, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, W).
		/// </summary>
		public Vector4us YYWW
		{
			get
			{
				return new Vector4us(Y, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, X).
		/// </summary>
		public Vector4us YZXX
		{
			get
			{
				return new Vector4us(Y, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Y).
		/// </summary>
		public Vector4us YZXY
		{
			get
			{
				return new Vector4us(Y, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Z).
		/// </summary>
		public Vector4us YZXZ
		{
			get
			{
				return new Vector4us(Y, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, W).
		/// </summary>
		public Vector4us YZXW
		{
			get
			{
				return new Vector4us(Y, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, X).
		/// </summary>
		public Vector4us YZYX
		{
			get
			{
				return new Vector4us(Y, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Y).
		/// </summary>
		public Vector4us YZYY
		{
			get
			{
				return new Vector4us(Y, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Z).
		/// </summary>
		public Vector4us YZYZ
		{
			get
			{
				return new Vector4us(Y, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, W).
		/// </summary>
		public Vector4us YZYW
		{
			get
			{
				return new Vector4us(Y, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, X).
		/// </summary>
		public Vector4us YZZX
		{
			get
			{
				return new Vector4us(Y, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Y).
		/// </summary>
		public Vector4us YZZY
		{
			get
			{
				return new Vector4us(Y, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Z).
		/// </summary>
		public Vector4us YZZZ
		{
			get
			{
				return new Vector4us(Y, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, W).
		/// </summary>
		public Vector4us YZZW
		{
			get
			{
				return new Vector4us(Y, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, X).
		/// </summary>
		public Vector4us YZWX
		{
			get
			{
				return new Vector4us(Y, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Y).
		/// </summary>
		public Vector4us YZWY
		{
			get
			{
				return new Vector4us(Y, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Z).
		/// </summary>
		public Vector4us YZWZ
		{
			get
			{
				return new Vector4us(Y, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, W).
		/// </summary>
		public Vector4us YZWW
		{
			get
			{
				return new Vector4us(Y, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, X).
		/// </summary>
		public Vector4us YWXX
		{
			get
			{
				return new Vector4us(Y, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Y).
		/// </summary>
		public Vector4us YWXY
		{
			get
			{
				return new Vector4us(Y, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Z).
		/// </summary>
		public Vector4us YWXZ
		{
			get
			{
				return new Vector4us(Y, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, W).
		/// </summary>
		public Vector4us YWXW
		{
			get
			{
				return new Vector4us(Y, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, X).
		/// </summary>
		public Vector4us YWYX
		{
			get
			{
				return new Vector4us(Y, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Y).
		/// </summary>
		public Vector4us YWYY
		{
			get
			{
				return new Vector4us(Y, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Z).
		/// </summary>
		public Vector4us YWYZ
		{
			get
			{
				return new Vector4us(Y, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, W).
		/// </summary>
		public Vector4us YWYW
		{
			get
			{
				return new Vector4us(Y, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, X).
		/// </summary>
		public Vector4us YWZX
		{
			get
			{
				return new Vector4us(Y, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Y).
		/// </summary>
		public Vector4us YWZY
		{
			get
			{
				return new Vector4us(Y, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Z).
		/// </summary>
		public Vector4us YWZZ
		{
			get
			{
				return new Vector4us(Y, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, W).
		/// </summary>
		public Vector4us YWZW
		{
			get
			{
				return new Vector4us(Y, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, X).
		/// </summary>
		public Vector4us YWWX
		{
			get
			{
				return new Vector4us(Y, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Y).
		/// </summary>
		public Vector4us YWWY
		{
			get
			{
				return new Vector4us(Y, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Z).
		/// </summary>
		public Vector4us YWWZ
		{
			get
			{
				return new Vector4us(Y, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, W).
		/// </summary>
		public Vector4us YWWW
		{
			get
			{
				return new Vector4us(Y, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, X).
		/// </summary>
		public Vector4us ZXXX
		{
			get
			{
				return new Vector4us(Z, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Y).
		/// </summary>
		public Vector4us ZXXY
		{
			get
			{
				return new Vector4us(Z, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Z).
		/// </summary>
		public Vector4us ZXXZ
		{
			get
			{
				return new Vector4us(Z, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, W).
		/// </summary>
		public Vector4us ZXXW
		{
			get
			{
				return new Vector4us(Z, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, X).
		/// </summary>
		public Vector4us ZXYX
		{
			get
			{
				return new Vector4us(Z, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Y).
		/// </summary>
		public Vector4us ZXYY
		{
			get
			{
				return new Vector4us(Z, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Z).
		/// </summary>
		public Vector4us ZXYZ
		{
			get
			{
				return new Vector4us(Z, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, W).
		/// </summary>
		public Vector4us ZXYW
		{
			get
			{
				return new Vector4us(Z, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, X).
		/// </summary>
		public Vector4us ZXZX
		{
			get
			{
				return new Vector4us(Z, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Y).
		/// </summary>
		public Vector4us ZXZY
		{
			get
			{
				return new Vector4us(Z, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Z).
		/// </summary>
		public Vector4us ZXZZ
		{
			get
			{
				return new Vector4us(Z, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, W).
		/// </summary>
		public Vector4us ZXZW
		{
			get
			{
				return new Vector4us(Z, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, X).
		/// </summary>
		public Vector4us ZXWX
		{
			get
			{
				return new Vector4us(Z, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Y).
		/// </summary>
		public Vector4us ZXWY
		{
			get
			{
				return new Vector4us(Z, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Z).
		/// </summary>
		public Vector4us ZXWZ
		{
			get
			{
				return new Vector4us(Z, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, W).
		/// </summary>
		public Vector4us ZXWW
		{
			get
			{
				return new Vector4us(Z, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, X).
		/// </summary>
		public Vector4us ZYXX
		{
			get
			{
				return new Vector4us(Z, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Y).
		/// </summary>
		public Vector4us ZYXY
		{
			get
			{
				return new Vector4us(Z, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Z).
		/// </summary>
		public Vector4us ZYXZ
		{
			get
			{
				return new Vector4us(Z, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, W).
		/// </summary>
		public Vector4us ZYXW
		{
			get
			{
				return new Vector4us(Z, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, X).
		/// </summary>
		public Vector4us ZYYX
		{
			get
			{
				return new Vector4us(Z, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Y).
		/// </summary>
		public Vector4us ZYYY
		{
			get
			{
				return new Vector4us(Z, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Z).
		/// </summary>
		public Vector4us ZYYZ
		{
			get
			{
				return new Vector4us(Z, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, W).
		/// </summary>
		public Vector4us ZYYW
		{
			get
			{
				return new Vector4us(Z, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, X).
		/// </summary>
		public Vector4us ZYZX
		{
			get
			{
				return new Vector4us(Z, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Y).
		/// </summary>
		public Vector4us ZYZY
		{
			get
			{
				return new Vector4us(Z, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Z).
		/// </summary>
		public Vector4us ZYZZ
		{
			get
			{
				return new Vector4us(Z, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, W).
		/// </summary>
		public Vector4us ZYZW
		{
			get
			{
				return new Vector4us(Z, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, X).
		/// </summary>
		public Vector4us ZYWX
		{
			get
			{
				return new Vector4us(Z, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Y).
		/// </summary>
		public Vector4us ZYWY
		{
			get
			{
				return new Vector4us(Z, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Z).
		/// </summary>
		public Vector4us ZYWZ
		{
			get
			{
				return new Vector4us(Z, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, W).
		/// </summary>
		public Vector4us ZYWW
		{
			get
			{
				return new Vector4us(Z, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, X).
		/// </summary>
		public Vector4us ZZXX
		{
			get
			{
				return new Vector4us(Z, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Y).
		/// </summary>
		public Vector4us ZZXY
		{
			get
			{
				return new Vector4us(Z, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Z).
		/// </summary>
		public Vector4us ZZXZ
		{
			get
			{
				return new Vector4us(Z, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, W).
		/// </summary>
		public Vector4us ZZXW
		{
			get
			{
				return new Vector4us(Z, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, X).
		/// </summary>
		public Vector4us ZZYX
		{
			get
			{
				return new Vector4us(Z, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Y).
		/// </summary>
		public Vector4us ZZYY
		{
			get
			{
				return new Vector4us(Z, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Z).
		/// </summary>
		public Vector4us ZZYZ
		{
			get
			{
				return new Vector4us(Z, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, W).
		/// </summary>
		public Vector4us ZZYW
		{
			get
			{
				return new Vector4us(Z, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, X).
		/// </summary>
		public Vector4us ZZZX
		{
			get
			{
				return new Vector4us(Z, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Y).
		/// </summary>
		public Vector4us ZZZY
		{
			get
			{
				return new Vector4us(Z, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Z).
		/// </summary>
		public Vector4us ZZZZ
		{
			get
			{
				return new Vector4us(Z, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, W).
		/// </summary>
		public Vector4us ZZZW
		{
			get
			{
				return new Vector4us(Z, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, X).
		/// </summary>
		public Vector4us ZZWX
		{
			get
			{
				return new Vector4us(Z, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Y).
		/// </summary>
		public Vector4us ZZWY
		{
			get
			{
				return new Vector4us(Z, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Z).
		/// </summary>
		public Vector4us ZZWZ
		{
			get
			{
				return new Vector4us(Z, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, W).
		/// </summary>
		public Vector4us ZZWW
		{
			get
			{
				return new Vector4us(Z, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, X).
		/// </summary>
		public Vector4us ZWXX
		{
			get
			{
				return new Vector4us(Z, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Y).
		/// </summary>
		public Vector4us ZWXY
		{
			get
			{
				return new Vector4us(Z, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Z).
		/// </summary>
		public Vector4us ZWXZ
		{
			get
			{
				return new Vector4us(Z, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, W).
		/// </summary>
		public Vector4us ZWXW
		{
			get
			{
				return new Vector4us(Z, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, X).
		/// </summary>
		public Vector4us ZWYX
		{
			get
			{
				return new Vector4us(Z, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Y).
		/// </summary>
		public Vector4us ZWYY
		{
			get
			{
				return new Vector4us(Z, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Z).
		/// </summary>
		public Vector4us ZWYZ
		{
			get
			{
				return new Vector4us(Z, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, W).
		/// </summary>
		public Vector4us ZWYW
		{
			get
			{
				return new Vector4us(Z, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, X).
		/// </summary>
		public Vector4us ZWZX
		{
			get
			{
				return new Vector4us(Z, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Y).
		/// </summary>
		public Vector4us ZWZY
		{
			get
			{
				return new Vector4us(Z, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Z).
		/// </summary>
		public Vector4us ZWZZ
		{
			get
			{
				return new Vector4us(Z, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, W).
		/// </summary>
		public Vector4us ZWZW
		{
			get
			{
				return new Vector4us(Z, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, X).
		/// </summary>
		public Vector4us ZWWX
		{
			get
			{
				return new Vector4us(Z, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Y).
		/// </summary>
		public Vector4us ZWWY
		{
			get
			{
				return new Vector4us(Z, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Z).
		/// </summary>
		public Vector4us ZWWZ
		{
			get
			{
				return new Vector4us(Z, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, W).
		/// </summary>
		public Vector4us ZWWW
		{
			get
			{
				return new Vector4us(Z, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, X).
		/// </summary>
		public Vector4us WXXX
		{
			get
			{
				return new Vector4us(W, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Y).
		/// </summary>
		public Vector4us WXXY
		{
			get
			{
				return new Vector4us(W, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Z).
		/// </summary>
		public Vector4us WXXZ
		{
			get
			{
				return new Vector4us(W, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, W).
		/// </summary>
		public Vector4us WXXW
		{
			get
			{
				return new Vector4us(W, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, X).
		/// </summary>
		public Vector4us WXYX
		{
			get
			{
				return new Vector4us(W, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Y).
		/// </summary>
		public Vector4us WXYY
		{
			get
			{
				return new Vector4us(W, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Z).
		/// </summary>
		public Vector4us WXYZ
		{
			get
			{
				return new Vector4us(W, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, W).
		/// </summary>
		public Vector4us WXYW
		{
			get
			{
				return new Vector4us(W, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, X).
		/// </summary>
		public Vector4us WXZX
		{
			get
			{
				return new Vector4us(W, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Y).
		/// </summary>
		public Vector4us WXZY
		{
			get
			{
				return new Vector4us(W, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Z).
		/// </summary>
		public Vector4us WXZZ
		{
			get
			{
				return new Vector4us(W, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, W).
		/// </summary>
		public Vector4us WXZW
		{
			get
			{
				return new Vector4us(W, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, X).
		/// </summary>
		public Vector4us WXWX
		{
			get
			{
				return new Vector4us(W, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Y).
		/// </summary>
		public Vector4us WXWY
		{
			get
			{
				return new Vector4us(W, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Z).
		/// </summary>
		public Vector4us WXWZ
		{
			get
			{
				return new Vector4us(W, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, W).
		/// </summary>
		public Vector4us WXWW
		{
			get
			{
				return new Vector4us(W, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, X).
		/// </summary>
		public Vector4us WYXX
		{
			get
			{
				return new Vector4us(W, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Y).
		/// </summary>
		public Vector4us WYXY
		{
			get
			{
				return new Vector4us(W, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Z).
		/// </summary>
		public Vector4us WYXZ
		{
			get
			{
				return new Vector4us(W, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, W).
		/// </summary>
		public Vector4us WYXW
		{
			get
			{
				return new Vector4us(W, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, X).
		/// </summary>
		public Vector4us WYYX
		{
			get
			{
				return new Vector4us(W, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Y).
		/// </summary>
		public Vector4us WYYY
		{
			get
			{
				return new Vector4us(W, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Z).
		/// </summary>
		public Vector4us WYYZ
		{
			get
			{
				return new Vector4us(W, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, W).
		/// </summary>
		public Vector4us WYYW
		{
			get
			{
				return new Vector4us(W, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, X).
		/// </summary>
		public Vector4us WYZX
		{
			get
			{
				return new Vector4us(W, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Y).
		/// </summary>
		public Vector4us WYZY
		{
			get
			{
				return new Vector4us(W, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Z).
		/// </summary>
		public Vector4us WYZZ
		{
			get
			{
				return new Vector4us(W, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, W).
		/// </summary>
		public Vector4us WYZW
		{
			get
			{
				return new Vector4us(W, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, X).
		/// </summary>
		public Vector4us WYWX
		{
			get
			{
				return new Vector4us(W, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Y).
		/// </summary>
		public Vector4us WYWY
		{
			get
			{
				return new Vector4us(W, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Z).
		/// </summary>
		public Vector4us WYWZ
		{
			get
			{
				return new Vector4us(W, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, W).
		/// </summary>
		public Vector4us WYWW
		{
			get
			{
				return new Vector4us(W, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, X).
		/// </summary>
		public Vector4us WZXX
		{
			get
			{
				return new Vector4us(W, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Y).
		/// </summary>
		public Vector4us WZXY
		{
			get
			{
				return new Vector4us(W, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Z).
		/// </summary>
		public Vector4us WZXZ
		{
			get
			{
				return new Vector4us(W, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, W).
		/// </summary>
		public Vector4us WZXW
		{
			get
			{
				return new Vector4us(W, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, X).
		/// </summary>
		public Vector4us WZYX
		{
			get
			{
				return new Vector4us(W, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Y).
		/// </summary>
		public Vector4us WZYY
		{
			get
			{
				return new Vector4us(W, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Z).
		/// </summary>
		public Vector4us WZYZ
		{
			get
			{
				return new Vector4us(W, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, W).
		/// </summary>
		public Vector4us WZYW
		{
			get
			{
				return new Vector4us(W, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, X).
		/// </summary>
		public Vector4us WZZX
		{
			get
			{
				return new Vector4us(W, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Y).
		/// </summary>
		public Vector4us WZZY
		{
			get
			{
				return new Vector4us(W, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Z).
		/// </summary>
		public Vector4us WZZZ
		{
			get
			{
				return new Vector4us(W, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, W).
		/// </summary>
		public Vector4us WZZW
		{
			get
			{
				return new Vector4us(W, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, X).
		/// </summary>
		public Vector4us WZWX
		{
			get
			{
				return new Vector4us(W, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Y).
		/// </summary>
		public Vector4us WZWY
		{
			get
			{
				return new Vector4us(W, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Z).
		/// </summary>
		public Vector4us WZWZ
		{
			get
			{
				return new Vector4us(W, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, W).
		/// </summary>
		public Vector4us WZWW
		{
			get
			{
				return new Vector4us(W, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, X).
		/// </summary>
		public Vector4us WWXX
		{
			get
			{
				return new Vector4us(W, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Y).
		/// </summary>
		public Vector4us WWXY
		{
			get
			{
				return new Vector4us(W, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Z).
		/// </summary>
		public Vector4us WWXZ
		{
			get
			{
				return new Vector4us(W, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, W).
		/// </summary>
		public Vector4us WWXW
		{
			get
			{
				return new Vector4us(W, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, X).
		/// </summary>
		public Vector4us WWYX
		{
			get
			{
				return new Vector4us(W, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Y).
		/// </summary>
		public Vector4us WWYY
		{
			get
			{
				return new Vector4us(W, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Z).
		/// </summary>
		public Vector4us WWYZ
		{
			get
			{
				return new Vector4us(W, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, W).
		/// </summary>
		public Vector4us WWYW
		{
			get
			{
				return new Vector4us(W, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, X).
		/// </summary>
		public Vector4us WWZX
		{
			get
			{
				return new Vector4us(W, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Y).
		/// </summary>
		public Vector4us WWZY
		{
			get
			{
				return new Vector4us(W, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Z).
		/// </summary>
		public Vector4us WWZZ
		{
			get
			{
				return new Vector4us(W, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, W).
		/// </summary>
		public Vector4us WWZW
		{
			get
			{
				return new Vector4us(W, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, X).
		/// </summary>
		public Vector4us WWWX
		{
			get
			{
				return new Vector4us(W, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Y).
		/// </summary>
		public Vector4us WWWY
		{
			get
			{
				return new Vector4us(W, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Z).
		/// </summary>
		public Vector4us WWWZ
		{
			get
			{
				return new Vector4us(W, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, W).
		/// </summary>
		public Vector4us WWWW
		{
			get
			{
				return new Vector4us(W, W, W, W);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4us"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector4us(ushort value)
		{
			X = value;
			Y = value;
			Z = value;
			W = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4us"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X and Y components</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4us(Vector2us value, ushort z, ushort w)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4us"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X, Y and Z components</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4us(Vector3us value, ushort w)
		{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4us"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4us(ushort x, ushort y, ushort z, ushort w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4us"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector4us(ushort[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
			W = array[3];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4us"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector4us(ushort[] array, int offset)
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
		public static Vector4i operator +(Vector4us value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4i operator -(Vector4us value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4i operator +(Vector4us left, Vector4us right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4i operator -(Vector4us left, Vector4us right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4i operator *(Vector4us left, int right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4i operator *(int left, Vector4us right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4i operator /(Vector4us left, int right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector4d value to a Vector4us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4us.</param>
		/// <returns>A Vector4us that has all components equal to value.</returns>
		public static explicit operator Vector4us(Vector4d value)
		{
			return new Vector4us((ushort)value.X, (ushort)value.Y, (ushort)value.Z, (ushort)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4f value to a Vector4us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4us.</param>
		/// <returns>A Vector4us that has all components equal to value.</returns>
		public static explicit operator Vector4us(Vector4f value)
		{
			return new Vector4us((ushort)value.X, (ushort)value.Y, (ushort)value.Z, (ushort)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4h value to a Vector4us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4us.</param>
		/// <returns>A Vector4us that has all components equal to value.</returns>
		public static explicit operator Vector4us(Vector4h value)
		{
			return new Vector4us((ushort)value.X, (ushort)value.Y, (ushort)value.Z, (ushort)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4ul value to a Vector4us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4us.</param>
		/// <returns>A Vector4us that has all components equal to value.</returns>
		public static explicit operator Vector4us(Vector4ul value)
		{
			return new Vector4us((ushort)value.X, (ushort)value.Y, (ushort)value.Z, (ushort)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4l value to a Vector4us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4us.</param>
		/// <returns>A Vector4us that has all components equal to value.</returns>
		public static explicit operator Vector4us(Vector4l value)
		{
			return new Vector4us((ushort)value.X, (ushort)value.Y, (ushort)value.Z, (ushort)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4ui value to a Vector4us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4us.</param>
		/// <returns>A Vector4us that has all components equal to value.</returns>
		public static explicit operator Vector4us(Vector4ui value)
		{
			return new Vector4us((ushort)value.X, (ushort)value.Y, (ushort)value.Z, (ushort)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4i value to a Vector4us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4us.</param>
		/// <returns>A Vector4us that has all components equal to value.</returns>
		public static explicit operator Vector4us(Vector4i value)
		{
			return new Vector4us((ushort)value.X, (ushort)value.Y, (ushort)value.Z, (ushort)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4s value to a Vector4us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4us.</param>
		/// <returns>A Vector4us that has all components equal to value.</returns>
		public static explicit operator Vector4us(Vector4s value)
		{
			return new Vector4us((ushort)value.X, (ushort)value.Y, (ushort)value.Z, (ushort)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4b value to a Vector4us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4us.</param>
		/// <returns>A Vector4us that has all components equal to value.</returns>
		public static implicit operator Vector4us(Vector4b value)
		{
			return new Vector4us((ushort)value.X, (ushort)value.Y, (ushort)value.Z, (ushort)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4sb value to a Vector4us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4us.</param>
		/// <returns>A Vector4us that has all components equal to value.</returns>
		public static explicit operator Vector4us(Vector4sb value)
		{
			return new Vector4us((ushort)value.X, (ushort)value.Y, (ushort)value.Z, (ushort)value.W);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector4us"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector4us"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector4us"/> object, and its value
		/// is equal to the current <see cref="Vector4us"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector4us) { return Equals((Vector4us)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector4us other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector4us left, Vector4us right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z & left.W == right.W;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector4us left, Vector4us right)
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
		/// Writes the given <see cref="Vector4us"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector4us vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
			writer.Write(vector.Z);
			writer.Write(vector.W);
		}
		/// <summary>
		/// Reads a <see cref="Vector4us"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector4us ReadVector4us(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector4us(reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadUInt16());
		}
		#endregion
		#region Pack
		public static ulong Pack(int xBits, int yBits, int zBits, int wBits, Vector4us vector)
		{
			Contract.Requires(0 <= xBits && xBits <= 16, "xBits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 16, "yBits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 16, "zBits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= wBits && wBits <= 16, "wBits must be between 0 and 16 inclusive.");
			Contract.Requires(xBits + yBits + zBits + wBits <= 64);
			ulong x = (ulong)(vector.X) >> (64 - xBits);
			ulong y = (ulong)(vector.Y) >> (64 - yBits);
			y <<= xBits;
			ulong z = (ulong)(vector.Z) >> (64 - zBits);
			z <<= xBits + yBits;
			ulong w = (ulong)(vector.W) >> (64 - wBits);
			w <<= xBits + yBits + zBits;
			return (ulong)(x | y | z | w);
		}
		public static ulong Pack(Vector4us vector)
		{
			ulong x = (ulong)(vector.X) << 0;
			ulong y = (ulong)(vector.Y) << 16;
			ulong z = (ulong)(vector.Z) << 32;
			ulong w = (ulong)(vector.W) << 48;
			return (ulong)(x | y | z | w);
		}
		public static Vector4us Unpack(int xBits, int yBits, int zBits, int wBits, ushort bits)
		{
			Contract.Requires(0 <= xBits && xBits <= 16, "xBits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 16, "yBits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 16, "zBits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= wBits && wBits <= 16, "wBits must be between 0 and 16 inclusive.");
			Contract.Requires(xBits + yBits + zBits + wBits <= 64);
			ulong x = (ulong)(bits);
			x &= ((1UL << xBits) - 1);
			ulong y = (ulong)(bits) >> (xBits);
			y &= ((1UL << yBits) - 1);
			ulong z = (ulong)(bits) >> (xBits + yBits);
			z &= ((1UL << zBits) - 1);
			ulong w = (ulong)(bits) >> (xBits + yBits + zBits);
			w &= ((1UL << wBits) - 1);
			return new Vector4us((ushort)x, (ushort)y, (ushort)z, (ushort)w);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4i Negative(Vector4us value)
		{
			return new Vector4i(-value.X, -value.Y, -value.Z, -value.W);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4i Add(Vector4us left, Vector4us right)
		{
			return new Vector4i(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4i Subtract(Vector4us left, Vector4us right)
		{
			return new Vector4i(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4i Multiply(Vector4us vector, int scalar)
		{
			return new Vector4i(vector.X * scalar, vector.Y * scalar, vector.Z * scalar, vector.W * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4i Divide(Vector4us vector, int scalar)
		{
			return new Vector4i(vector.X / scalar, vector.Y / scalar, vector.Z / scalar, vector.W / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Vector4us left, Vector4us right)
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
		[CLSCompliant(false)]
		public static int Dot(Vector4us left, Vector4us right)
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
		[CLSCompliant(false)]
		public static bool All(Vector4us value)
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
		[CLSCompliant(false)]
		public static bool All(Vector4us value, Predicate<ushort> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z) && predicate(value.W);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		[CLSCompliant(false)]
		public static bool Any(Vector4us value)
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
		[CLSCompliant(false)]
		public static bool Any(Vector4us value, Predicate<ushort> predicate)
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
		[CLSCompliant(false)]
		public static int AbsoluteSquared(Vector4us value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		[CLSCompliant(false)]
		public static float Absolute(Vector4us value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		[CLSCompliant(false)]
		public static Vector4f Normalize(Vector4us value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Vector4us.Zero;
			}
			else
			{
				return (Vector4f)value / absolute;
			}
		}
		#endregion
		#region Per component
		#region Map
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector4d Map(Vector4us value, Func<ushort, double> mapping)
		{
			return new Vector4d(mapping(value.X), mapping(value.Y), mapping(value.Z), mapping(value.W));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector4f Map(Vector4us value, Func<ushort, float> mapping)
		{
			return new Vector4f(mapping(value.X), mapping(value.Y), mapping(value.Z), mapping(value.W));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector4h Map(Vector4us value, Func<ushort, Half> mapping)
		{
			return new Vector4h(mapping(value.X), mapping(value.Y), mapping(value.Z), mapping(value.W));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector4ul Map(Vector4us value, Func<ushort, ulong> mapping)
		{
			return new Vector4ul(mapping(value.X), mapping(value.Y), mapping(value.Z), mapping(value.W));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector4l Map(Vector4us value, Func<ushort, long> mapping)
		{
			return new Vector4l(mapping(value.X), mapping(value.Y), mapping(value.Z), mapping(value.W));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector4ui Map(Vector4us value, Func<ushort, uint> mapping)
		{
			return new Vector4ui(mapping(value.X), mapping(value.Y), mapping(value.Z), mapping(value.W));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector4i Map(Vector4us value, Func<ushort, int> mapping)
		{
			return new Vector4i(mapping(value.X), mapping(value.Y), mapping(value.Z), mapping(value.W));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector4us Map(Vector4us value, Func<ushort, ushort> mapping)
		{
			return new Vector4us(mapping(value.X), mapping(value.Y), mapping(value.Z), mapping(value.W));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector4s Map(Vector4us value, Func<ushort, short> mapping)
		{
			return new Vector4s(mapping(value.X), mapping(value.Y), mapping(value.Z), mapping(value.W));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector4b Map(Vector4us value, Func<ushort, byte> mapping)
		{
			return new Vector4b(mapping(value.X), mapping(value.Y), mapping(value.Z), mapping(value.W));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector4sb Map(Vector4us value, Func<ushort, sbyte> mapping)
		{
			return new Vector4sb(mapping(value.X), mapping(value.Y), mapping(value.Z), mapping(value.W));
		}
		#endregion
		/// <summary>
		/// Multiplys the components of two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first vector to modulate.</param>
		/// <param name="right">The second vector to modulate.</param>
		/// <returns>The result of multiplying each component of left by the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector4i Modulate(Vector4us left, Vector4us right)
		{
			return new Vector4i(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		[CLSCompliant(false)]
		public static Vector4us Abs(Vector4us value)
		{
			return new Vector4us(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z), Functions.Abs(value.W));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector4us Min(Vector4us value1, Vector4us value2)
		{
			return new Vector4us(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z), Functions.Min(value1.W, value2.W));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector4us Max(Vector4us value1, Vector4us value2)
		{
			return new Vector4us(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z), Functions.Max(value1.W, value2.W));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		[CLSCompliant(false)]
		public static Vector4us Clamp(Vector4us value, Vector4us min, Vector4us max)
		{
			return new Vector4us(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z), Functions.Clamp(value.W, min.W, max.W));
		}
		#endregion
		#region Coordinate spaces
		#endregion
	}
}
