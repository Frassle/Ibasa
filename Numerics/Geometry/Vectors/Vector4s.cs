using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a four component vector of shorts, of the form (X, Y, Z, W).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector4s: IEquatable<Vector4s>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector4s"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector4s Zero = new Vector4s(0);
		/// <summary>
		/// Returns a new <see cref="Vector4s"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector4s One = new Vector4s(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector4s"/> (1, 0, 0, 0).
		/// </summary>
		public static readonly Vector4s UnitX = new Vector4s(1, 0, 0, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector4s"/> (0, 1, 0, 0).
		/// </summary>
		public static readonly Vector4s UnitY = new Vector4s(0, 1, 0, 0);
		/// <summary>
		/// Returns the Z unit <see cref="Vector4s"/> (0, 0, 1, 0).
		/// </summary>
		public static readonly Vector4s UnitZ = new Vector4s(0, 0, 1, 0);
		/// <summary>
		/// Returns the W unit <see cref="Vector4s"/> (0, 0, 0, 1).
		/// </summary>
		public static readonly Vector4s UnitW = new Vector4s(0, 0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the vector.
		/// </summary>
		public readonly short X;
		/// <summary>
		/// The Y component of the vector.
		/// </summary>
		public readonly short Y;
		/// <summary>
		/// The Z component of the vector.
		/// </summary>
		public readonly short Z;
		/// <summary>
		/// The W component of the vector.
		/// </summary>
		public readonly short W;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public short this[int index]
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
						throw new IndexOutOfRangeException("Indices for Vector4s run from 0 to 3, inclusive.");
				}
			}
		}
		public short[] ToArray()
		{
			return new short[]
			{
				X, Y, Z, W
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the vector (X, X).
		/// </summary>
		public Vector2s XX
		{
			get
			{
				return new Vector2s(X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y).
		/// </summary>
		public Vector2s XY
		{
			get
			{
				return new Vector2s(X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z).
		/// </summary>
		public Vector2s XZ
		{
			get
			{
				return new Vector2s(X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W).
		/// </summary>
		public Vector2s XW
		{
			get
			{
				return new Vector2s(X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X).
		/// </summary>
		public Vector2s YX
		{
			get
			{
				return new Vector2s(Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y).
		/// </summary>
		public Vector2s YY
		{
			get
			{
				return new Vector2s(Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z).
		/// </summary>
		public Vector2s YZ
		{
			get
			{
				return new Vector2s(Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W).
		/// </summary>
		public Vector2s YW
		{
			get
			{
				return new Vector2s(Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X).
		/// </summary>
		public Vector2s ZX
		{
			get
			{
				return new Vector2s(Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y).
		/// </summary>
		public Vector2s ZY
		{
			get
			{
				return new Vector2s(Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z).
		/// </summary>
		public Vector2s ZZ
		{
			get
			{
				return new Vector2s(Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W).
		/// </summary>
		public Vector2s ZW
		{
			get
			{
				return new Vector2s(Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X).
		/// </summary>
		public Vector2s WX
		{
			get
			{
				return new Vector2s(W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y).
		/// </summary>
		public Vector2s WY
		{
			get
			{
				return new Vector2s(W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z).
		/// </summary>
		public Vector2s WZ
		{
			get
			{
				return new Vector2s(W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W).
		/// </summary>
		public Vector2s WW
		{
			get
			{
				return new Vector2s(W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X).
		/// </summary>
		public Vector3s XXX
		{
			get
			{
				return new Vector3s(X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y).
		/// </summary>
		public Vector3s XXY
		{
			get
			{
				return new Vector3s(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z).
		/// </summary>
		public Vector3s XXZ
		{
			get
			{
				return new Vector3s(X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W).
		/// </summary>
		public Vector3s XXW
		{
			get
			{
				return new Vector3s(X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X).
		/// </summary>
		public Vector3s XYX
		{
			get
			{
				return new Vector3s(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y).
		/// </summary>
		public Vector3s XYY
		{
			get
			{
				return new Vector3s(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z).
		/// </summary>
		public Vector3s XYZ
		{
			get
			{
				return new Vector3s(X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W).
		/// </summary>
		public Vector3s XYW
		{
			get
			{
				return new Vector3s(X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X).
		/// </summary>
		public Vector3s XZX
		{
			get
			{
				return new Vector3s(X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y).
		/// </summary>
		public Vector3s XZY
		{
			get
			{
				return new Vector3s(X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z).
		/// </summary>
		public Vector3s XZZ
		{
			get
			{
				return new Vector3s(X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W).
		/// </summary>
		public Vector3s XZW
		{
			get
			{
				return new Vector3s(X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X).
		/// </summary>
		public Vector3s XWX
		{
			get
			{
				return new Vector3s(X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y).
		/// </summary>
		public Vector3s XWY
		{
			get
			{
				return new Vector3s(X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z).
		/// </summary>
		public Vector3s XWZ
		{
			get
			{
				return new Vector3s(X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W).
		/// </summary>
		public Vector3s XWW
		{
			get
			{
				return new Vector3s(X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X).
		/// </summary>
		public Vector3s YXX
		{
			get
			{
				return new Vector3s(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y).
		/// </summary>
		public Vector3s YXY
		{
			get
			{
				return new Vector3s(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z).
		/// </summary>
		public Vector3s YXZ
		{
			get
			{
				return new Vector3s(Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W).
		/// </summary>
		public Vector3s YXW
		{
			get
			{
				return new Vector3s(Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X).
		/// </summary>
		public Vector3s YYX
		{
			get
			{
				return new Vector3s(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y).
		/// </summary>
		public Vector3s YYY
		{
			get
			{
				return new Vector3s(Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z).
		/// </summary>
		public Vector3s YYZ
		{
			get
			{
				return new Vector3s(Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W).
		/// </summary>
		public Vector3s YYW
		{
			get
			{
				return new Vector3s(Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X).
		/// </summary>
		public Vector3s YZX
		{
			get
			{
				return new Vector3s(Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y).
		/// </summary>
		public Vector3s YZY
		{
			get
			{
				return new Vector3s(Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z).
		/// </summary>
		public Vector3s YZZ
		{
			get
			{
				return new Vector3s(Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W).
		/// </summary>
		public Vector3s YZW
		{
			get
			{
				return new Vector3s(Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X).
		/// </summary>
		public Vector3s YWX
		{
			get
			{
				return new Vector3s(Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y).
		/// </summary>
		public Vector3s YWY
		{
			get
			{
				return new Vector3s(Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z).
		/// </summary>
		public Vector3s YWZ
		{
			get
			{
				return new Vector3s(Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W).
		/// </summary>
		public Vector3s YWW
		{
			get
			{
				return new Vector3s(Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X).
		/// </summary>
		public Vector3s ZXX
		{
			get
			{
				return new Vector3s(Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y).
		/// </summary>
		public Vector3s ZXY
		{
			get
			{
				return new Vector3s(Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z).
		/// </summary>
		public Vector3s ZXZ
		{
			get
			{
				return new Vector3s(Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W).
		/// </summary>
		public Vector3s ZXW
		{
			get
			{
				return new Vector3s(Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X).
		/// </summary>
		public Vector3s ZYX
		{
			get
			{
				return new Vector3s(Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y).
		/// </summary>
		public Vector3s ZYY
		{
			get
			{
				return new Vector3s(Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z).
		/// </summary>
		public Vector3s ZYZ
		{
			get
			{
				return new Vector3s(Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W).
		/// </summary>
		public Vector3s ZYW
		{
			get
			{
				return new Vector3s(Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X).
		/// </summary>
		public Vector3s ZZX
		{
			get
			{
				return new Vector3s(Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y).
		/// </summary>
		public Vector3s ZZY
		{
			get
			{
				return new Vector3s(Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z).
		/// </summary>
		public Vector3s ZZZ
		{
			get
			{
				return new Vector3s(Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W).
		/// </summary>
		public Vector3s ZZW
		{
			get
			{
				return new Vector3s(Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X).
		/// </summary>
		public Vector3s ZWX
		{
			get
			{
				return new Vector3s(Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y).
		/// </summary>
		public Vector3s ZWY
		{
			get
			{
				return new Vector3s(Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z).
		/// </summary>
		public Vector3s ZWZ
		{
			get
			{
				return new Vector3s(Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W).
		/// </summary>
		public Vector3s ZWW
		{
			get
			{
				return new Vector3s(Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X).
		/// </summary>
		public Vector3s WXX
		{
			get
			{
				return new Vector3s(W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y).
		/// </summary>
		public Vector3s WXY
		{
			get
			{
				return new Vector3s(W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z).
		/// </summary>
		public Vector3s WXZ
		{
			get
			{
				return new Vector3s(W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W).
		/// </summary>
		public Vector3s WXW
		{
			get
			{
				return new Vector3s(W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X).
		/// </summary>
		public Vector3s WYX
		{
			get
			{
				return new Vector3s(W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y).
		/// </summary>
		public Vector3s WYY
		{
			get
			{
				return new Vector3s(W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z).
		/// </summary>
		public Vector3s WYZ
		{
			get
			{
				return new Vector3s(W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W).
		/// </summary>
		public Vector3s WYW
		{
			get
			{
				return new Vector3s(W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X).
		/// </summary>
		public Vector3s WZX
		{
			get
			{
				return new Vector3s(W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y).
		/// </summary>
		public Vector3s WZY
		{
			get
			{
				return new Vector3s(W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z).
		/// </summary>
		public Vector3s WZZ
		{
			get
			{
				return new Vector3s(W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W).
		/// </summary>
		public Vector3s WZW
		{
			get
			{
				return new Vector3s(W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X).
		/// </summary>
		public Vector3s WWX
		{
			get
			{
				return new Vector3s(W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y).
		/// </summary>
		public Vector3s WWY
		{
			get
			{
				return new Vector3s(W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z).
		/// </summary>
		public Vector3s WWZ
		{
			get
			{
				return new Vector3s(W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W).
		/// </summary>
		public Vector3s WWW
		{
			get
			{
				return new Vector3s(W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, X).
		/// </summary>
		public Vector4s XXXX
		{
			get
			{
				return new Vector4s(X, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Y).
		/// </summary>
		public Vector4s XXXY
		{
			get
			{
				return new Vector4s(X, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Z).
		/// </summary>
		public Vector4s XXXZ
		{
			get
			{
				return new Vector4s(X, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, W).
		/// </summary>
		public Vector4s XXXW
		{
			get
			{
				return new Vector4s(X, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, X).
		/// </summary>
		public Vector4s XXYX
		{
			get
			{
				return new Vector4s(X, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Y).
		/// </summary>
		public Vector4s XXYY
		{
			get
			{
				return new Vector4s(X, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Z).
		/// </summary>
		public Vector4s XXYZ
		{
			get
			{
				return new Vector4s(X, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, W).
		/// </summary>
		public Vector4s XXYW
		{
			get
			{
				return new Vector4s(X, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, X).
		/// </summary>
		public Vector4s XXZX
		{
			get
			{
				return new Vector4s(X, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Y).
		/// </summary>
		public Vector4s XXZY
		{
			get
			{
				return new Vector4s(X, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Z).
		/// </summary>
		public Vector4s XXZZ
		{
			get
			{
				return new Vector4s(X, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, W).
		/// </summary>
		public Vector4s XXZW
		{
			get
			{
				return new Vector4s(X, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, X).
		/// </summary>
		public Vector4s XXWX
		{
			get
			{
				return new Vector4s(X, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Y).
		/// </summary>
		public Vector4s XXWY
		{
			get
			{
				return new Vector4s(X, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Z).
		/// </summary>
		public Vector4s XXWZ
		{
			get
			{
				return new Vector4s(X, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, W).
		/// </summary>
		public Vector4s XXWW
		{
			get
			{
				return new Vector4s(X, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, X).
		/// </summary>
		public Vector4s XYXX
		{
			get
			{
				return new Vector4s(X, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Y).
		/// </summary>
		public Vector4s XYXY
		{
			get
			{
				return new Vector4s(X, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Z).
		/// </summary>
		public Vector4s XYXZ
		{
			get
			{
				return new Vector4s(X, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, W).
		/// </summary>
		public Vector4s XYXW
		{
			get
			{
				return new Vector4s(X, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, X).
		/// </summary>
		public Vector4s XYYX
		{
			get
			{
				return new Vector4s(X, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Y).
		/// </summary>
		public Vector4s XYYY
		{
			get
			{
				return new Vector4s(X, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Z).
		/// </summary>
		public Vector4s XYYZ
		{
			get
			{
				return new Vector4s(X, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, W).
		/// </summary>
		public Vector4s XYYW
		{
			get
			{
				return new Vector4s(X, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, X).
		/// </summary>
		public Vector4s XYZX
		{
			get
			{
				return new Vector4s(X, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Y).
		/// </summary>
		public Vector4s XYZY
		{
			get
			{
				return new Vector4s(X, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Z).
		/// </summary>
		public Vector4s XYZZ
		{
			get
			{
				return new Vector4s(X, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, W).
		/// </summary>
		public Vector4s XYZW
		{
			get
			{
				return new Vector4s(X, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, X).
		/// </summary>
		public Vector4s XYWX
		{
			get
			{
				return new Vector4s(X, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Y).
		/// </summary>
		public Vector4s XYWY
		{
			get
			{
				return new Vector4s(X, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Z).
		/// </summary>
		public Vector4s XYWZ
		{
			get
			{
				return new Vector4s(X, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, W).
		/// </summary>
		public Vector4s XYWW
		{
			get
			{
				return new Vector4s(X, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, X).
		/// </summary>
		public Vector4s XZXX
		{
			get
			{
				return new Vector4s(X, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Y).
		/// </summary>
		public Vector4s XZXY
		{
			get
			{
				return new Vector4s(X, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Z).
		/// </summary>
		public Vector4s XZXZ
		{
			get
			{
				return new Vector4s(X, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, W).
		/// </summary>
		public Vector4s XZXW
		{
			get
			{
				return new Vector4s(X, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, X).
		/// </summary>
		public Vector4s XZYX
		{
			get
			{
				return new Vector4s(X, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Y).
		/// </summary>
		public Vector4s XZYY
		{
			get
			{
				return new Vector4s(X, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Z).
		/// </summary>
		public Vector4s XZYZ
		{
			get
			{
				return new Vector4s(X, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, W).
		/// </summary>
		public Vector4s XZYW
		{
			get
			{
				return new Vector4s(X, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, X).
		/// </summary>
		public Vector4s XZZX
		{
			get
			{
				return new Vector4s(X, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Y).
		/// </summary>
		public Vector4s XZZY
		{
			get
			{
				return new Vector4s(X, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Z).
		/// </summary>
		public Vector4s XZZZ
		{
			get
			{
				return new Vector4s(X, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, W).
		/// </summary>
		public Vector4s XZZW
		{
			get
			{
				return new Vector4s(X, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, X).
		/// </summary>
		public Vector4s XZWX
		{
			get
			{
				return new Vector4s(X, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Y).
		/// </summary>
		public Vector4s XZWY
		{
			get
			{
				return new Vector4s(X, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Z).
		/// </summary>
		public Vector4s XZWZ
		{
			get
			{
				return new Vector4s(X, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, W).
		/// </summary>
		public Vector4s XZWW
		{
			get
			{
				return new Vector4s(X, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, X).
		/// </summary>
		public Vector4s XWXX
		{
			get
			{
				return new Vector4s(X, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Y).
		/// </summary>
		public Vector4s XWXY
		{
			get
			{
				return new Vector4s(X, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Z).
		/// </summary>
		public Vector4s XWXZ
		{
			get
			{
				return new Vector4s(X, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, W).
		/// </summary>
		public Vector4s XWXW
		{
			get
			{
				return new Vector4s(X, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, X).
		/// </summary>
		public Vector4s XWYX
		{
			get
			{
				return new Vector4s(X, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Y).
		/// </summary>
		public Vector4s XWYY
		{
			get
			{
				return new Vector4s(X, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Z).
		/// </summary>
		public Vector4s XWYZ
		{
			get
			{
				return new Vector4s(X, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, W).
		/// </summary>
		public Vector4s XWYW
		{
			get
			{
				return new Vector4s(X, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, X).
		/// </summary>
		public Vector4s XWZX
		{
			get
			{
				return new Vector4s(X, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Y).
		/// </summary>
		public Vector4s XWZY
		{
			get
			{
				return new Vector4s(X, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Z).
		/// </summary>
		public Vector4s XWZZ
		{
			get
			{
				return new Vector4s(X, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, W).
		/// </summary>
		public Vector4s XWZW
		{
			get
			{
				return new Vector4s(X, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, X).
		/// </summary>
		public Vector4s XWWX
		{
			get
			{
				return new Vector4s(X, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Y).
		/// </summary>
		public Vector4s XWWY
		{
			get
			{
				return new Vector4s(X, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Z).
		/// </summary>
		public Vector4s XWWZ
		{
			get
			{
				return new Vector4s(X, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, W).
		/// </summary>
		public Vector4s XWWW
		{
			get
			{
				return new Vector4s(X, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, X).
		/// </summary>
		public Vector4s YXXX
		{
			get
			{
				return new Vector4s(Y, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Y).
		/// </summary>
		public Vector4s YXXY
		{
			get
			{
				return new Vector4s(Y, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Z).
		/// </summary>
		public Vector4s YXXZ
		{
			get
			{
				return new Vector4s(Y, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, W).
		/// </summary>
		public Vector4s YXXW
		{
			get
			{
				return new Vector4s(Y, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, X).
		/// </summary>
		public Vector4s YXYX
		{
			get
			{
				return new Vector4s(Y, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Y).
		/// </summary>
		public Vector4s YXYY
		{
			get
			{
				return new Vector4s(Y, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Z).
		/// </summary>
		public Vector4s YXYZ
		{
			get
			{
				return new Vector4s(Y, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, W).
		/// </summary>
		public Vector4s YXYW
		{
			get
			{
				return new Vector4s(Y, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, X).
		/// </summary>
		public Vector4s YXZX
		{
			get
			{
				return new Vector4s(Y, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Y).
		/// </summary>
		public Vector4s YXZY
		{
			get
			{
				return new Vector4s(Y, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Z).
		/// </summary>
		public Vector4s YXZZ
		{
			get
			{
				return new Vector4s(Y, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, W).
		/// </summary>
		public Vector4s YXZW
		{
			get
			{
				return new Vector4s(Y, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, X).
		/// </summary>
		public Vector4s YXWX
		{
			get
			{
				return new Vector4s(Y, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Y).
		/// </summary>
		public Vector4s YXWY
		{
			get
			{
				return new Vector4s(Y, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Z).
		/// </summary>
		public Vector4s YXWZ
		{
			get
			{
				return new Vector4s(Y, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, W).
		/// </summary>
		public Vector4s YXWW
		{
			get
			{
				return new Vector4s(Y, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, X).
		/// </summary>
		public Vector4s YYXX
		{
			get
			{
				return new Vector4s(Y, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Y).
		/// </summary>
		public Vector4s YYXY
		{
			get
			{
				return new Vector4s(Y, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Z).
		/// </summary>
		public Vector4s YYXZ
		{
			get
			{
				return new Vector4s(Y, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, W).
		/// </summary>
		public Vector4s YYXW
		{
			get
			{
				return new Vector4s(Y, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, X).
		/// </summary>
		public Vector4s YYYX
		{
			get
			{
				return new Vector4s(Y, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Y).
		/// </summary>
		public Vector4s YYYY
		{
			get
			{
				return new Vector4s(Y, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Z).
		/// </summary>
		public Vector4s YYYZ
		{
			get
			{
				return new Vector4s(Y, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, W).
		/// </summary>
		public Vector4s YYYW
		{
			get
			{
				return new Vector4s(Y, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, X).
		/// </summary>
		public Vector4s YYZX
		{
			get
			{
				return new Vector4s(Y, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Y).
		/// </summary>
		public Vector4s YYZY
		{
			get
			{
				return new Vector4s(Y, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Z).
		/// </summary>
		public Vector4s YYZZ
		{
			get
			{
				return new Vector4s(Y, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, W).
		/// </summary>
		public Vector4s YYZW
		{
			get
			{
				return new Vector4s(Y, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, X).
		/// </summary>
		public Vector4s YYWX
		{
			get
			{
				return new Vector4s(Y, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Y).
		/// </summary>
		public Vector4s YYWY
		{
			get
			{
				return new Vector4s(Y, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Z).
		/// </summary>
		public Vector4s YYWZ
		{
			get
			{
				return new Vector4s(Y, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, W).
		/// </summary>
		public Vector4s YYWW
		{
			get
			{
				return new Vector4s(Y, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, X).
		/// </summary>
		public Vector4s YZXX
		{
			get
			{
				return new Vector4s(Y, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Y).
		/// </summary>
		public Vector4s YZXY
		{
			get
			{
				return new Vector4s(Y, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Z).
		/// </summary>
		public Vector4s YZXZ
		{
			get
			{
				return new Vector4s(Y, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, W).
		/// </summary>
		public Vector4s YZXW
		{
			get
			{
				return new Vector4s(Y, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, X).
		/// </summary>
		public Vector4s YZYX
		{
			get
			{
				return new Vector4s(Y, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Y).
		/// </summary>
		public Vector4s YZYY
		{
			get
			{
				return new Vector4s(Y, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Z).
		/// </summary>
		public Vector4s YZYZ
		{
			get
			{
				return new Vector4s(Y, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, W).
		/// </summary>
		public Vector4s YZYW
		{
			get
			{
				return new Vector4s(Y, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, X).
		/// </summary>
		public Vector4s YZZX
		{
			get
			{
				return new Vector4s(Y, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Y).
		/// </summary>
		public Vector4s YZZY
		{
			get
			{
				return new Vector4s(Y, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Z).
		/// </summary>
		public Vector4s YZZZ
		{
			get
			{
				return new Vector4s(Y, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, W).
		/// </summary>
		public Vector4s YZZW
		{
			get
			{
				return new Vector4s(Y, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, X).
		/// </summary>
		public Vector4s YZWX
		{
			get
			{
				return new Vector4s(Y, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Y).
		/// </summary>
		public Vector4s YZWY
		{
			get
			{
				return new Vector4s(Y, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Z).
		/// </summary>
		public Vector4s YZWZ
		{
			get
			{
				return new Vector4s(Y, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, W).
		/// </summary>
		public Vector4s YZWW
		{
			get
			{
				return new Vector4s(Y, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, X).
		/// </summary>
		public Vector4s YWXX
		{
			get
			{
				return new Vector4s(Y, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Y).
		/// </summary>
		public Vector4s YWXY
		{
			get
			{
				return new Vector4s(Y, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Z).
		/// </summary>
		public Vector4s YWXZ
		{
			get
			{
				return new Vector4s(Y, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, W).
		/// </summary>
		public Vector4s YWXW
		{
			get
			{
				return new Vector4s(Y, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, X).
		/// </summary>
		public Vector4s YWYX
		{
			get
			{
				return new Vector4s(Y, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Y).
		/// </summary>
		public Vector4s YWYY
		{
			get
			{
				return new Vector4s(Y, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Z).
		/// </summary>
		public Vector4s YWYZ
		{
			get
			{
				return new Vector4s(Y, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, W).
		/// </summary>
		public Vector4s YWYW
		{
			get
			{
				return new Vector4s(Y, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, X).
		/// </summary>
		public Vector4s YWZX
		{
			get
			{
				return new Vector4s(Y, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Y).
		/// </summary>
		public Vector4s YWZY
		{
			get
			{
				return new Vector4s(Y, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Z).
		/// </summary>
		public Vector4s YWZZ
		{
			get
			{
				return new Vector4s(Y, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, W).
		/// </summary>
		public Vector4s YWZW
		{
			get
			{
				return new Vector4s(Y, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, X).
		/// </summary>
		public Vector4s YWWX
		{
			get
			{
				return new Vector4s(Y, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Y).
		/// </summary>
		public Vector4s YWWY
		{
			get
			{
				return new Vector4s(Y, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Z).
		/// </summary>
		public Vector4s YWWZ
		{
			get
			{
				return new Vector4s(Y, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, W).
		/// </summary>
		public Vector4s YWWW
		{
			get
			{
				return new Vector4s(Y, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, X).
		/// </summary>
		public Vector4s ZXXX
		{
			get
			{
				return new Vector4s(Z, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Y).
		/// </summary>
		public Vector4s ZXXY
		{
			get
			{
				return new Vector4s(Z, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Z).
		/// </summary>
		public Vector4s ZXXZ
		{
			get
			{
				return new Vector4s(Z, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, W).
		/// </summary>
		public Vector4s ZXXW
		{
			get
			{
				return new Vector4s(Z, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, X).
		/// </summary>
		public Vector4s ZXYX
		{
			get
			{
				return new Vector4s(Z, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Y).
		/// </summary>
		public Vector4s ZXYY
		{
			get
			{
				return new Vector4s(Z, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Z).
		/// </summary>
		public Vector4s ZXYZ
		{
			get
			{
				return new Vector4s(Z, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, W).
		/// </summary>
		public Vector4s ZXYW
		{
			get
			{
				return new Vector4s(Z, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, X).
		/// </summary>
		public Vector4s ZXZX
		{
			get
			{
				return new Vector4s(Z, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Y).
		/// </summary>
		public Vector4s ZXZY
		{
			get
			{
				return new Vector4s(Z, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Z).
		/// </summary>
		public Vector4s ZXZZ
		{
			get
			{
				return new Vector4s(Z, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, W).
		/// </summary>
		public Vector4s ZXZW
		{
			get
			{
				return new Vector4s(Z, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, X).
		/// </summary>
		public Vector4s ZXWX
		{
			get
			{
				return new Vector4s(Z, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Y).
		/// </summary>
		public Vector4s ZXWY
		{
			get
			{
				return new Vector4s(Z, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Z).
		/// </summary>
		public Vector4s ZXWZ
		{
			get
			{
				return new Vector4s(Z, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, W).
		/// </summary>
		public Vector4s ZXWW
		{
			get
			{
				return new Vector4s(Z, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, X).
		/// </summary>
		public Vector4s ZYXX
		{
			get
			{
				return new Vector4s(Z, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Y).
		/// </summary>
		public Vector4s ZYXY
		{
			get
			{
				return new Vector4s(Z, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Z).
		/// </summary>
		public Vector4s ZYXZ
		{
			get
			{
				return new Vector4s(Z, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, W).
		/// </summary>
		public Vector4s ZYXW
		{
			get
			{
				return new Vector4s(Z, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, X).
		/// </summary>
		public Vector4s ZYYX
		{
			get
			{
				return new Vector4s(Z, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Y).
		/// </summary>
		public Vector4s ZYYY
		{
			get
			{
				return new Vector4s(Z, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Z).
		/// </summary>
		public Vector4s ZYYZ
		{
			get
			{
				return new Vector4s(Z, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, W).
		/// </summary>
		public Vector4s ZYYW
		{
			get
			{
				return new Vector4s(Z, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, X).
		/// </summary>
		public Vector4s ZYZX
		{
			get
			{
				return new Vector4s(Z, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Y).
		/// </summary>
		public Vector4s ZYZY
		{
			get
			{
				return new Vector4s(Z, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Z).
		/// </summary>
		public Vector4s ZYZZ
		{
			get
			{
				return new Vector4s(Z, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, W).
		/// </summary>
		public Vector4s ZYZW
		{
			get
			{
				return new Vector4s(Z, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, X).
		/// </summary>
		public Vector4s ZYWX
		{
			get
			{
				return new Vector4s(Z, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Y).
		/// </summary>
		public Vector4s ZYWY
		{
			get
			{
				return new Vector4s(Z, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Z).
		/// </summary>
		public Vector4s ZYWZ
		{
			get
			{
				return new Vector4s(Z, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, W).
		/// </summary>
		public Vector4s ZYWW
		{
			get
			{
				return new Vector4s(Z, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, X).
		/// </summary>
		public Vector4s ZZXX
		{
			get
			{
				return new Vector4s(Z, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Y).
		/// </summary>
		public Vector4s ZZXY
		{
			get
			{
				return new Vector4s(Z, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Z).
		/// </summary>
		public Vector4s ZZXZ
		{
			get
			{
				return new Vector4s(Z, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, W).
		/// </summary>
		public Vector4s ZZXW
		{
			get
			{
				return new Vector4s(Z, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, X).
		/// </summary>
		public Vector4s ZZYX
		{
			get
			{
				return new Vector4s(Z, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Y).
		/// </summary>
		public Vector4s ZZYY
		{
			get
			{
				return new Vector4s(Z, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Z).
		/// </summary>
		public Vector4s ZZYZ
		{
			get
			{
				return new Vector4s(Z, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, W).
		/// </summary>
		public Vector4s ZZYW
		{
			get
			{
				return new Vector4s(Z, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, X).
		/// </summary>
		public Vector4s ZZZX
		{
			get
			{
				return new Vector4s(Z, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Y).
		/// </summary>
		public Vector4s ZZZY
		{
			get
			{
				return new Vector4s(Z, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Z).
		/// </summary>
		public Vector4s ZZZZ
		{
			get
			{
				return new Vector4s(Z, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, W).
		/// </summary>
		public Vector4s ZZZW
		{
			get
			{
				return new Vector4s(Z, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, X).
		/// </summary>
		public Vector4s ZZWX
		{
			get
			{
				return new Vector4s(Z, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Y).
		/// </summary>
		public Vector4s ZZWY
		{
			get
			{
				return new Vector4s(Z, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Z).
		/// </summary>
		public Vector4s ZZWZ
		{
			get
			{
				return new Vector4s(Z, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, W).
		/// </summary>
		public Vector4s ZZWW
		{
			get
			{
				return new Vector4s(Z, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, X).
		/// </summary>
		public Vector4s ZWXX
		{
			get
			{
				return new Vector4s(Z, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Y).
		/// </summary>
		public Vector4s ZWXY
		{
			get
			{
				return new Vector4s(Z, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Z).
		/// </summary>
		public Vector4s ZWXZ
		{
			get
			{
				return new Vector4s(Z, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, W).
		/// </summary>
		public Vector4s ZWXW
		{
			get
			{
				return new Vector4s(Z, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, X).
		/// </summary>
		public Vector4s ZWYX
		{
			get
			{
				return new Vector4s(Z, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Y).
		/// </summary>
		public Vector4s ZWYY
		{
			get
			{
				return new Vector4s(Z, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Z).
		/// </summary>
		public Vector4s ZWYZ
		{
			get
			{
				return new Vector4s(Z, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, W).
		/// </summary>
		public Vector4s ZWYW
		{
			get
			{
				return new Vector4s(Z, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, X).
		/// </summary>
		public Vector4s ZWZX
		{
			get
			{
				return new Vector4s(Z, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Y).
		/// </summary>
		public Vector4s ZWZY
		{
			get
			{
				return new Vector4s(Z, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Z).
		/// </summary>
		public Vector4s ZWZZ
		{
			get
			{
				return new Vector4s(Z, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, W).
		/// </summary>
		public Vector4s ZWZW
		{
			get
			{
				return new Vector4s(Z, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, X).
		/// </summary>
		public Vector4s ZWWX
		{
			get
			{
				return new Vector4s(Z, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Y).
		/// </summary>
		public Vector4s ZWWY
		{
			get
			{
				return new Vector4s(Z, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Z).
		/// </summary>
		public Vector4s ZWWZ
		{
			get
			{
				return new Vector4s(Z, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, W).
		/// </summary>
		public Vector4s ZWWW
		{
			get
			{
				return new Vector4s(Z, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, X).
		/// </summary>
		public Vector4s WXXX
		{
			get
			{
				return new Vector4s(W, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Y).
		/// </summary>
		public Vector4s WXXY
		{
			get
			{
				return new Vector4s(W, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Z).
		/// </summary>
		public Vector4s WXXZ
		{
			get
			{
				return new Vector4s(W, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, W).
		/// </summary>
		public Vector4s WXXW
		{
			get
			{
				return new Vector4s(W, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, X).
		/// </summary>
		public Vector4s WXYX
		{
			get
			{
				return new Vector4s(W, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Y).
		/// </summary>
		public Vector4s WXYY
		{
			get
			{
				return new Vector4s(W, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Z).
		/// </summary>
		public Vector4s WXYZ
		{
			get
			{
				return new Vector4s(W, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, W).
		/// </summary>
		public Vector4s WXYW
		{
			get
			{
				return new Vector4s(W, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, X).
		/// </summary>
		public Vector4s WXZX
		{
			get
			{
				return new Vector4s(W, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Y).
		/// </summary>
		public Vector4s WXZY
		{
			get
			{
				return new Vector4s(W, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Z).
		/// </summary>
		public Vector4s WXZZ
		{
			get
			{
				return new Vector4s(W, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, W).
		/// </summary>
		public Vector4s WXZW
		{
			get
			{
				return new Vector4s(W, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, X).
		/// </summary>
		public Vector4s WXWX
		{
			get
			{
				return new Vector4s(W, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Y).
		/// </summary>
		public Vector4s WXWY
		{
			get
			{
				return new Vector4s(W, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Z).
		/// </summary>
		public Vector4s WXWZ
		{
			get
			{
				return new Vector4s(W, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, W).
		/// </summary>
		public Vector4s WXWW
		{
			get
			{
				return new Vector4s(W, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, X).
		/// </summary>
		public Vector4s WYXX
		{
			get
			{
				return new Vector4s(W, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Y).
		/// </summary>
		public Vector4s WYXY
		{
			get
			{
				return new Vector4s(W, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Z).
		/// </summary>
		public Vector4s WYXZ
		{
			get
			{
				return new Vector4s(W, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, W).
		/// </summary>
		public Vector4s WYXW
		{
			get
			{
				return new Vector4s(W, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, X).
		/// </summary>
		public Vector4s WYYX
		{
			get
			{
				return new Vector4s(W, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Y).
		/// </summary>
		public Vector4s WYYY
		{
			get
			{
				return new Vector4s(W, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Z).
		/// </summary>
		public Vector4s WYYZ
		{
			get
			{
				return new Vector4s(W, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, W).
		/// </summary>
		public Vector4s WYYW
		{
			get
			{
				return new Vector4s(W, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, X).
		/// </summary>
		public Vector4s WYZX
		{
			get
			{
				return new Vector4s(W, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Y).
		/// </summary>
		public Vector4s WYZY
		{
			get
			{
				return new Vector4s(W, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Z).
		/// </summary>
		public Vector4s WYZZ
		{
			get
			{
				return new Vector4s(W, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, W).
		/// </summary>
		public Vector4s WYZW
		{
			get
			{
				return new Vector4s(W, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, X).
		/// </summary>
		public Vector4s WYWX
		{
			get
			{
				return new Vector4s(W, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Y).
		/// </summary>
		public Vector4s WYWY
		{
			get
			{
				return new Vector4s(W, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Z).
		/// </summary>
		public Vector4s WYWZ
		{
			get
			{
				return new Vector4s(W, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, W).
		/// </summary>
		public Vector4s WYWW
		{
			get
			{
				return new Vector4s(W, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, X).
		/// </summary>
		public Vector4s WZXX
		{
			get
			{
				return new Vector4s(W, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Y).
		/// </summary>
		public Vector4s WZXY
		{
			get
			{
				return new Vector4s(W, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Z).
		/// </summary>
		public Vector4s WZXZ
		{
			get
			{
				return new Vector4s(W, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, W).
		/// </summary>
		public Vector4s WZXW
		{
			get
			{
				return new Vector4s(W, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, X).
		/// </summary>
		public Vector4s WZYX
		{
			get
			{
				return new Vector4s(W, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Y).
		/// </summary>
		public Vector4s WZYY
		{
			get
			{
				return new Vector4s(W, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Z).
		/// </summary>
		public Vector4s WZYZ
		{
			get
			{
				return new Vector4s(W, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, W).
		/// </summary>
		public Vector4s WZYW
		{
			get
			{
				return new Vector4s(W, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, X).
		/// </summary>
		public Vector4s WZZX
		{
			get
			{
				return new Vector4s(W, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Y).
		/// </summary>
		public Vector4s WZZY
		{
			get
			{
				return new Vector4s(W, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Z).
		/// </summary>
		public Vector4s WZZZ
		{
			get
			{
				return new Vector4s(W, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, W).
		/// </summary>
		public Vector4s WZZW
		{
			get
			{
				return new Vector4s(W, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, X).
		/// </summary>
		public Vector4s WZWX
		{
			get
			{
				return new Vector4s(W, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Y).
		/// </summary>
		public Vector4s WZWY
		{
			get
			{
				return new Vector4s(W, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Z).
		/// </summary>
		public Vector4s WZWZ
		{
			get
			{
				return new Vector4s(W, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, W).
		/// </summary>
		public Vector4s WZWW
		{
			get
			{
				return new Vector4s(W, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, X).
		/// </summary>
		public Vector4s WWXX
		{
			get
			{
				return new Vector4s(W, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Y).
		/// </summary>
		public Vector4s WWXY
		{
			get
			{
				return new Vector4s(W, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Z).
		/// </summary>
		public Vector4s WWXZ
		{
			get
			{
				return new Vector4s(W, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, W).
		/// </summary>
		public Vector4s WWXW
		{
			get
			{
				return new Vector4s(W, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, X).
		/// </summary>
		public Vector4s WWYX
		{
			get
			{
				return new Vector4s(W, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Y).
		/// </summary>
		public Vector4s WWYY
		{
			get
			{
				return new Vector4s(W, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Z).
		/// </summary>
		public Vector4s WWYZ
		{
			get
			{
				return new Vector4s(W, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, W).
		/// </summary>
		public Vector4s WWYW
		{
			get
			{
				return new Vector4s(W, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, X).
		/// </summary>
		public Vector4s WWZX
		{
			get
			{
				return new Vector4s(W, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Y).
		/// </summary>
		public Vector4s WWZY
		{
			get
			{
				return new Vector4s(W, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Z).
		/// </summary>
		public Vector4s WWZZ
		{
			get
			{
				return new Vector4s(W, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, W).
		/// </summary>
		public Vector4s WWZW
		{
			get
			{
				return new Vector4s(W, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, X).
		/// </summary>
		public Vector4s WWWX
		{
			get
			{
				return new Vector4s(W, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Y).
		/// </summary>
		public Vector4s WWWY
		{
			get
			{
				return new Vector4s(W, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Z).
		/// </summary>
		public Vector4s WWWZ
		{
			get
			{
				return new Vector4s(W, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, W).
		/// </summary>
		public Vector4s WWWW
		{
			get
			{
				return new Vector4s(W, W, W, W);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4s"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector4s(short value)
		{
			X = value;
			Y = value;
			Z = value;
			W = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4s"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X and Y components</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4s(Vector2s value, short z, short w)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4s"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X, Y and Z components</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4s(Vector3s value, short w)
		{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4s"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4s(short x, short y, short z, short w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4s"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector4s(short[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
			W = array[3];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4s"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector4s(short[] array, int offset)
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
		public static Vector4i operator +(Vector4s value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4i operator -(Vector4s value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4i operator +(Vector4s left, Vector4s right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4i operator -(Vector4s left, Vector4s right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4i operator *(Vector4s left, int right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4i operator *(int left, Vector4s right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4i operator /(Vector4s left, int right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector4d value to a Vector4s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4s.</param>
		/// <returns>A Vector4s that has all components equal to value.</returns>
		public static explicit operator Vector4s(Vector4d value)
		{
			return new Vector4s((short)value.X, (short)value.Y, (short)value.Z, (short)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4f value to a Vector4s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4s.</param>
		/// <returns>A Vector4s that has all components equal to value.</returns>
		public static explicit operator Vector4s(Vector4f value)
		{
			return new Vector4s((short)value.X, (short)value.Y, (short)value.Z, (short)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4h value to a Vector4s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4s.</param>
		/// <returns>A Vector4s that has all components equal to value.</returns>
		public static explicit operator Vector4s(Vector4h value)
		{
			return new Vector4s((short)value.X, (short)value.Y, (short)value.Z, (short)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4ul value to a Vector4s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4s.</param>
		/// <returns>A Vector4s that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4s(Vector4ul value)
		{
			return new Vector4s((short)value.X, (short)value.Y, (short)value.Z, (short)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4l value to a Vector4s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4s.</param>
		/// <returns>A Vector4s that has all components equal to value.</returns>
		public static explicit operator Vector4s(Vector4l value)
		{
			return new Vector4s((short)value.X, (short)value.Y, (short)value.Z, (short)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4ui value to a Vector4s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4s.</param>
		/// <returns>A Vector4s that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4s(Vector4ui value)
		{
			return new Vector4s((short)value.X, (short)value.Y, (short)value.Z, (short)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4i value to a Vector4s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4s.</param>
		/// <returns>A Vector4s that has all components equal to value.</returns>
		public static explicit operator Vector4s(Vector4i value)
		{
			return new Vector4s((short)value.X, (short)value.Y, (short)value.Z, (short)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4us value to a Vector4s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4s.</param>
		/// <returns>A Vector4s that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4s(Vector4us value)
		{
			return new Vector4s((short)value.X, (short)value.Y, (short)value.Z, (short)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4b value to a Vector4s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4s.</param>
		/// <returns>A Vector4s that has all components equal to value.</returns>
		public static implicit operator Vector4s(Vector4b value)
		{
			return new Vector4s((short)value.X, (short)value.Y, (short)value.Z, (short)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4sb value to a Vector4s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4s.</param>
		/// <returns>A Vector4s that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector4s(Vector4sb value)
		{
			return new Vector4s((short)value.X, (short)value.Y, (short)value.Z, (short)value.W);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector4s"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector4s"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector4s"/> object, and its value
		/// is equal to the current <see cref="Vector4s"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector4s) { return Equals((Vector4s)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector4s other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector4s left, Vector4s right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z & left.W == right.W;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector4s left, Vector4s right)
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
		/// Writes the given <see cref="Vector4s"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector4s vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
			writer.Write(vector.Z);
			writer.Write(vector.W);
		}
		/// <summary>
		/// Reads a <see cref="Vector4s"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector4s ReadVector4s(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector4s(reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16());
		}
		#endregion
		#region Pack
		public static long Pack(int xBits, int yBits, int zBits, int wBits, Vector4s vector)
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
			return (long)(x | y | z | w);
		}
		public static long Pack(Vector4s vector)
		{
			ulong x = (ulong)(vector.X) << 0;
			ulong y = (ulong)(vector.Y) << 16;
			ulong z = (ulong)(vector.Z) << 32;
			ulong w = (ulong)(vector.W) << 48;
			return (long)(x | y | z | w);
		}
		public static Vector4s Unpack(int xBits, int yBits, int zBits, int wBits, short bits)
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
			return new Vector4s((short)x, (short)y, (short)z, (short)w);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4i Negative(Vector4s value)
		{
			return new Vector4i(-value.X, -value.Y, -value.Z, -value.W);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4i Add(Vector4s left, Vector4s right)
		{
			return new Vector4i(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4i Subtract(Vector4s left, Vector4s right)
		{
			return new Vector4i(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4i Multiply(Vector4s vector, int scalar)
		{
			return new Vector4i(vector.X * scalar, vector.Y * scalar, vector.Z * scalar, vector.W * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4i Divide(Vector4s vector, int scalar)
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
		public static bool Equals(Vector4s left, Vector4s right)
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
		public static int Dot(Vector4s left, Vector4s right)
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
		public static bool All(Vector4s value)
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
		public static bool All(Vector4s value, Predicate<short> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z) && predicate(value.W);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Vector4s value)
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
		public static bool Any(Vector4s value, Predicate<short> predicate)
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
		public static int AbsoluteSquared(Vector4s value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		public static float Absolute(Vector4s value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		public static Vector4f Normalize(Vector4s value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Vector4s.Zero;
			}
			else
			{
				return (Vector4f)value / absolute;
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
		public static Vector4d Transform(Vector4s value, Func<short, double> transformer)
		{
			return new Vector4d(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4f Transform(Vector4s value, Func<short, float> transformer)
		{
			return new Vector4f(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4h Transform(Vector4s value, Func<short, Half> transformer)
		{
			return new Vector4h(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4ul Transform(Vector4s value, Func<short, ulong> transformer)
		{
			return new Vector4ul(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4l Transform(Vector4s value, Func<short, long> transformer)
		{
			return new Vector4l(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4ui Transform(Vector4s value, Func<short, uint> transformer)
		{
			return new Vector4ui(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4i Transform(Vector4s value, Func<short, int> transformer)
		{
			return new Vector4i(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4us Transform(Vector4s value, Func<short, ushort> transformer)
		{
			return new Vector4us(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4s Transform(Vector4s value, Func<short, short> transformer)
		{
			return new Vector4s(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4b Transform(Vector4s value, Func<short, byte> transformer)
		{
			return new Vector4b(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4sb Transform(Vector4s value, Func<short, sbyte> transformer)
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
		public static Vector4i Modulate(Vector4s left, Vector4s right)
		{
			return new Vector4i(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Vector4s Abs(Vector4s value)
		{
			return new Vector4s(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z), Functions.Abs(value.W));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Vector4s Min(Vector4s value1, Vector4s value2)
		{
			return new Vector4s(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z), Functions.Min(value1.W, value2.W));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Vector4s Max(Vector4s value1, Vector4s value2)
		{
			return new Vector4s(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z), Functions.Max(value1.W, value2.W));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		public static Vector4s Clamp(Vector4s value, Vector4s min, Vector4s max)
		{
			return new Vector4s(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z), Functions.Clamp(value.W, min.W, max.W));
		}
		#endregion
	}
}
