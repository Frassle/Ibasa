using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a four component vector of ints, of the form (X, Y, Z, W).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector4i: IEquatable<Vector4i>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector4i"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector4i Zero = new Vector4i(0);
		/// <summary>
		/// Returns a new <see cref="Vector4i"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector4i One = new Vector4i(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector4i"/> (1, 0, 0, 0).
		/// </summary>
		public static readonly Vector4i UnitX = new Vector4i(1, 0, 0, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector4i"/> (0, 1, 0, 0).
		/// </summary>
		public static readonly Vector4i UnitY = new Vector4i(0, 1, 0, 0);
		/// <summary>
		/// Returns the Z unit <see cref="Vector4i"/> (0, 0, 1, 0).
		/// </summary>
		public static readonly Vector4i UnitZ = new Vector4i(0, 0, 1, 0);
		/// <summary>
		/// Returns the W unit <see cref="Vector4i"/> (0, 0, 0, 1).
		/// </summary>
		public static readonly Vector4i UnitW = new Vector4i(0, 0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the vector.
		/// </summary>
		public readonly int X;
		/// <summary>
		/// The Y component of the vector.
		/// </summary>
		public readonly int Y;
		/// <summary>
		/// The Z component of the vector.
		/// </summary>
		public readonly int Z;
		/// <summary>
		/// The W component of the vector.
		/// </summary>
		public readonly int W;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public int this[int index]
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
						throw new IndexOutOfRangeException("Indices for Vector4i run from 0 to 3, inclusive.");
				}
			}
		}
		public int[] ToArray()
		{
			return new int[]
			{
				X, Y, Z, W
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the vector (X, X).
		/// </summary>
		public Vector2i XX
		{
			get
			{
				return new Vector2i(X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y).
		/// </summary>
		public Vector2i XY
		{
			get
			{
				return new Vector2i(X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z).
		/// </summary>
		public Vector2i XZ
		{
			get
			{
				return new Vector2i(X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W).
		/// </summary>
		public Vector2i XW
		{
			get
			{
				return new Vector2i(X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X).
		/// </summary>
		public Vector2i YX
		{
			get
			{
				return new Vector2i(Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y).
		/// </summary>
		public Vector2i YY
		{
			get
			{
				return new Vector2i(Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z).
		/// </summary>
		public Vector2i YZ
		{
			get
			{
				return new Vector2i(Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W).
		/// </summary>
		public Vector2i YW
		{
			get
			{
				return new Vector2i(Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X).
		/// </summary>
		public Vector2i ZX
		{
			get
			{
				return new Vector2i(Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y).
		/// </summary>
		public Vector2i ZY
		{
			get
			{
				return new Vector2i(Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z).
		/// </summary>
		public Vector2i ZZ
		{
			get
			{
				return new Vector2i(Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W).
		/// </summary>
		public Vector2i ZW
		{
			get
			{
				return new Vector2i(Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X).
		/// </summary>
		public Vector2i WX
		{
			get
			{
				return new Vector2i(W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y).
		/// </summary>
		public Vector2i WY
		{
			get
			{
				return new Vector2i(W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z).
		/// </summary>
		public Vector2i WZ
		{
			get
			{
				return new Vector2i(W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W).
		/// </summary>
		public Vector2i WW
		{
			get
			{
				return new Vector2i(W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X).
		/// </summary>
		public Vector3i XXX
		{
			get
			{
				return new Vector3i(X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y).
		/// </summary>
		public Vector3i XXY
		{
			get
			{
				return new Vector3i(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z).
		/// </summary>
		public Vector3i XXZ
		{
			get
			{
				return new Vector3i(X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W).
		/// </summary>
		public Vector3i XXW
		{
			get
			{
				return new Vector3i(X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X).
		/// </summary>
		public Vector3i XYX
		{
			get
			{
				return new Vector3i(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y).
		/// </summary>
		public Vector3i XYY
		{
			get
			{
				return new Vector3i(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z).
		/// </summary>
		public Vector3i XYZ
		{
			get
			{
				return new Vector3i(X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W).
		/// </summary>
		public Vector3i XYW
		{
			get
			{
				return new Vector3i(X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X).
		/// </summary>
		public Vector3i XZX
		{
			get
			{
				return new Vector3i(X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y).
		/// </summary>
		public Vector3i XZY
		{
			get
			{
				return new Vector3i(X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z).
		/// </summary>
		public Vector3i XZZ
		{
			get
			{
				return new Vector3i(X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W).
		/// </summary>
		public Vector3i XZW
		{
			get
			{
				return new Vector3i(X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X).
		/// </summary>
		public Vector3i XWX
		{
			get
			{
				return new Vector3i(X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y).
		/// </summary>
		public Vector3i XWY
		{
			get
			{
				return new Vector3i(X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z).
		/// </summary>
		public Vector3i XWZ
		{
			get
			{
				return new Vector3i(X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W).
		/// </summary>
		public Vector3i XWW
		{
			get
			{
				return new Vector3i(X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X).
		/// </summary>
		public Vector3i YXX
		{
			get
			{
				return new Vector3i(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y).
		/// </summary>
		public Vector3i YXY
		{
			get
			{
				return new Vector3i(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z).
		/// </summary>
		public Vector3i YXZ
		{
			get
			{
				return new Vector3i(Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W).
		/// </summary>
		public Vector3i YXW
		{
			get
			{
				return new Vector3i(Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X).
		/// </summary>
		public Vector3i YYX
		{
			get
			{
				return new Vector3i(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y).
		/// </summary>
		public Vector3i YYY
		{
			get
			{
				return new Vector3i(Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z).
		/// </summary>
		public Vector3i YYZ
		{
			get
			{
				return new Vector3i(Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W).
		/// </summary>
		public Vector3i YYW
		{
			get
			{
				return new Vector3i(Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X).
		/// </summary>
		public Vector3i YZX
		{
			get
			{
				return new Vector3i(Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y).
		/// </summary>
		public Vector3i YZY
		{
			get
			{
				return new Vector3i(Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z).
		/// </summary>
		public Vector3i YZZ
		{
			get
			{
				return new Vector3i(Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W).
		/// </summary>
		public Vector3i YZW
		{
			get
			{
				return new Vector3i(Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X).
		/// </summary>
		public Vector3i YWX
		{
			get
			{
				return new Vector3i(Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y).
		/// </summary>
		public Vector3i YWY
		{
			get
			{
				return new Vector3i(Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z).
		/// </summary>
		public Vector3i YWZ
		{
			get
			{
				return new Vector3i(Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W).
		/// </summary>
		public Vector3i YWW
		{
			get
			{
				return new Vector3i(Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X).
		/// </summary>
		public Vector3i ZXX
		{
			get
			{
				return new Vector3i(Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y).
		/// </summary>
		public Vector3i ZXY
		{
			get
			{
				return new Vector3i(Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z).
		/// </summary>
		public Vector3i ZXZ
		{
			get
			{
				return new Vector3i(Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W).
		/// </summary>
		public Vector3i ZXW
		{
			get
			{
				return new Vector3i(Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X).
		/// </summary>
		public Vector3i ZYX
		{
			get
			{
				return new Vector3i(Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y).
		/// </summary>
		public Vector3i ZYY
		{
			get
			{
				return new Vector3i(Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z).
		/// </summary>
		public Vector3i ZYZ
		{
			get
			{
				return new Vector3i(Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W).
		/// </summary>
		public Vector3i ZYW
		{
			get
			{
				return new Vector3i(Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X).
		/// </summary>
		public Vector3i ZZX
		{
			get
			{
				return new Vector3i(Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y).
		/// </summary>
		public Vector3i ZZY
		{
			get
			{
				return new Vector3i(Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z).
		/// </summary>
		public Vector3i ZZZ
		{
			get
			{
				return new Vector3i(Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W).
		/// </summary>
		public Vector3i ZZW
		{
			get
			{
				return new Vector3i(Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X).
		/// </summary>
		public Vector3i ZWX
		{
			get
			{
				return new Vector3i(Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y).
		/// </summary>
		public Vector3i ZWY
		{
			get
			{
				return new Vector3i(Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z).
		/// </summary>
		public Vector3i ZWZ
		{
			get
			{
				return new Vector3i(Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W).
		/// </summary>
		public Vector3i ZWW
		{
			get
			{
				return new Vector3i(Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X).
		/// </summary>
		public Vector3i WXX
		{
			get
			{
				return new Vector3i(W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y).
		/// </summary>
		public Vector3i WXY
		{
			get
			{
				return new Vector3i(W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z).
		/// </summary>
		public Vector3i WXZ
		{
			get
			{
				return new Vector3i(W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W).
		/// </summary>
		public Vector3i WXW
		{
			get
			{
				return new Vector3i(W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X).
		/// </summary>
		public Vector3i WYX
		{
			get
			{
				return new Vector3i(W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y).
		/// </summary>
		public Vector3i WYY
		{
			get
			{
				return new Vector3i(W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z).
		/// </summary>
		public Vector3i WYZ
		{
			get
			{
				return new Vector3i(W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W).
		/// </summary>
		public Vector3i WYW
		{
			get
			{
				return new Vector3i(W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X).
		/// </summary>
		public Vector3i WZX
		{
			get
			{
				return new Vector3i(W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y).
		/// </summary>
		public Vector3i WZY
		{
			get
			{
				return new Vector3i(W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z).
		/// </summary>
		public Vector3i WZZ
		{
			get
			{
				return new Vector3i(W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W).
		/// </summary>
		public Vector3i WZW
		{
			get
			{
				return new Vector3i(W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X).
		/// </summary>
		public Vector3i WWX
		{
			get
			{
				return new Vector3i(W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y).
		/// </summary>
		public Vector3i WWY
		{
			get
			{
				return new Vector3i(W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z).
		/// </summary>
		public Vector3i WWZ
		{
			get
			{
				return new Vector3i(W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W).
		/// </summary>
		public Vector3i WWW
		{
			get
			{
				return new Vector3i(W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, X).
		/// </summary>
		public Vector4i XXXX
		{
			get
			{
				return new Vector4i(X, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Y).
		/// </summary>
		public Vector4i XXXY
		{
			get
			{
				return new Vector4i(X, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Z).
		/// </summary>
		public Vector4i XXXZ
		{
			get
			{
				return new Vector4i(X, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, W).
		/// </summary>
		public Vector4i XXXW
		{
			get
			{
				return new Vector4i(X, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, X).
		/// </summary>
		public Vector4i XXYX
		{
			get
			{
				return new Vector4i(X, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Y).
		/// </summary>
		public Vector4i XXYY
		{
			get
			{
				return new Vector4i(X, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Z).
		/// </summary>
		public Vector4i XXYZ
		{
			get
			{
				return new Vector4i(X, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, W).
		/// </summary>
		public Vector4i XXYW
		{
			get
			{
				return new Vector4i(X, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, X).
		/// </summary>
		public Vector4i XXZX
		{
			get
			{
				return new Vector4i(X, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Y).
		/// </summary>
		public Vector4i XXZY
		{
			get
			{
				return new Vector4i(X, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Z).
		/// </summary>
		public Vector4i XXZZ
		{
			get
			{
				return new Vector4i(X, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, W).
		/// </summary>
		public Vector4i XXZW
		{
			get
			{
				return new Vector4i(X, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, X).
		/// </summary>
		public Vector4i XXWX
		{
			get
			{
				return new Vector4i(X, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Y).
		/// </summary>
		public Vector4i XXWY
		{
			get
			{
				return new Vector4i(X, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Z).
		/// </summary>
		public Vector4i XXWZ
		{
			get
			{
				return new Vector4i(X, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, W).
		/// </summary>
		public Vector4i XXWW
		{
			get
			{
				return new Vector4i(X, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, X).
		/// </summary>
		public Vector4i XYXX
		{
			get
			{
				return new Vector4i(X, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Y).
		/// </summary>
		public Vector4i XYXY
		{
			get
			{
				return new Vector4i(X, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Z).
		/// </summary>
		public Vector4i XYXZ
		{
			get
			{
				return new Vector4i(X, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, W).
		/// </summary>
		public Vector4i XYXW
		{
			get
			{
				return new Vector4i(X, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, X).
		/// </summary>
		public Vector4i XYYX
		{
			get
			{
				return new Vector4i(X, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Y).
		/// </summary>
		public Vector4i XYYY
		{
			get
			{
				return new Vector4i(X, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Z).
		/// </summary>
		public Vector4i XYYZ
		{
			get
			{
				return new Vector4i(X, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, W).
		/// </summary>
		public Vector4i XYYW
		{
			get
			{
				return new Vector4i(X, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, X).
		/// </summary>
		public Vector4i XYZX
		{
			get
			{
				return new Vector4i(X, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Y).
		/// </summary>
		public Vector4i XYZY
		{
			get
			{
				return new Vector4i(X, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Z).
		/// </summary>
		public Vector4i XYZZ
		{
			get
			{
				return new Vector4i(X, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, W).
		/// </summary>
		public Vector4i XYZW
		{
			get
			{
				return new Vector4i(X, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, X).
		/// </summary>
		public Vector4i XYWX
		{
			get
			{
				return new Vector4i(X, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Y).
		/// </summary>
		public Vector4i XYWY
		{
			get
			{
				return new Vector4i(X, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Z).
		/// </summary>
		public Vector4i XYWZ
		{
			get
			{
				return new Vector4i(X, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, W).
		/// </summary>
		public Vector4i XYWW
		{
			get
			{
				return new Vector4i(X, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, X).
		/// </summary>
		public Vector4i XZXX
		{
			get
			{
				return new Vector4i(X, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Y).
		/// </summary>
		public Vector4i XZXY
		{
			get
			{
				return new Vector4i(X, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Z).
		/// </summary>
		public Vector4i XZXZ
		{
			get
			{
				return new Vector4i(X, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, W).
		/// </summary>
		public Vector4i XZXW
		{
			get
			{
				return new Vector4i(X, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, X).
		/// </summary>
		public Vector4i XZYX
		{
			get
			{
				return new Vector4i(X, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Y).
		/// </summary>
		public Vector4i XZYY
		{
			get
			{
				return new Vector4i(X, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Z).
		/// </summary>
		public Vector4i XZYZ
		{
			get
			{
				return new Vector4i(X, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, W).
		/// </summary>
		public Vector4i XZYW
		{
			get
			{
				return new Vector4i(X, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, X).
		/// </summary>
		public Vector4i XZZX
		{
			get
			{
				return new Vector4i(X, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Y).
		/// </summary>
		public Vector4i XZZY
		{
			get
			{
				return new Vector4i(X, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Z).
		/// </summary>
		public Vector4i XZZZ
		{
			get
			{
				return new Vector4i(X, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, W).
		/// </summary>
		public Vector4i XZZW
		{
			get
			{
				return new Vector4i(X, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, X).
		/// </summary>
		public Vector4i XZWX
		{
			get
			{
				return new Vector4i(X, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Y).
		/// </summary>
		public Vector4i XZWY
		{
			get
			{
				return new Vector4i(X, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Z).
		/// </summary>
		public Vector4i XZWZ
		{
			get
			{
				return new Vector4i(X, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, W).
		/// </summary>
		public Vector4i XZWW
		{
			get
			{
				return new Vector4i(X, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, X).
		/// </summary>
		public Vector4i XWXX
		{
			get
			{
				return new Vector4i(X, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Y).
		/// </summary>
		public Vector4i XWXY
		{
			get
			{
				return new Vector4i(X, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Z).
		/// </summary>
		public Vector4i XWXZ
		{
			get
			{
				return new Vector4i(X, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, W).
		/// </summary>
		public Vector4i XWXW
		{
			get
			{
				return new Vector4i(X, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, X).
		/// </summary>
		public Vector4i XWYX
		{
			get
			{
				return new Vector4i(X, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Y).
		/// </summary>
		public Vector4i XWYY
		{
			get
			{
				return new Vector4i(X, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Z).
		/// </summary>
		public Vector4i XWYZ
		{
			get
			{
				return new Vector4i(X, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, W).
		/// </summary>
		public Vector4i XWYW
		{
			get
			{
				return new Vector4i(X, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, X).
		/// </summary>
		public Vector4i XWZX
		{
			get
			{
				return new Vector4i(X, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Y).
		/// </summary>
		public Vector4i XWZY
		{
			get
			{
				return new Vector4i(X, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Z).
		/// </summary>
		public Vector4i XWZZ
		{
			get
			{
				return new Vector4i(X, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, W).
		/// </summary>
		public Vector4i XWZW
		{
			get
			{
				return new Vector4i(X, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, X).
		/// </summary>
		public Vector4i XWWX
		{
			get
			{
				return new Vector4i(X, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Y).
		/// </summary>
		public Vector4i XWWY
		{
			get
			{
				return new Vector4i(X, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Z).
		/// </summary>
		public Vector4i XWWZ
		{
			get
			{
				return new Vector4i(X, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, W).
		/// </summary>
		public Vector4i XWWW
		{
			get
			{
				return new Vector4i(X, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, X).
		/// </summary>
		public Vector4i YXXX
		{
			get
			{
				return new Vector4i(Y, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Y).
		/// </summary>
		public Vector4i YXXY
		{
			get
			{
				return new Vector4i(Y, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Z).
		/// </summary>
		public Vector4i YXXZ
		{
			get
			{
				return new Vector4i(Y, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, W).
		/// </summary>
		public Vector4i YXXW
		{
			get
			{
				return new Vector4i(Y, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, X).
		/// </summary>
		public Vector4i YXYX
		{
			get
			{
				return new Vector4i(Y, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Y).
		/// </summary>
		public Vector4i YXYY
		{
			get
			{
				return new Vector4i(Y, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Z).
		/// </summary>
		public Vector4i YXYZ
		{
			get
			{
				return new Vector4i(Y, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, W).
		/// </summary>
		public Vector4i YXYW
		{
			get
			{
				return new Vector4i(Y, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, X).
		/// </summary>
		public Vector4i YXZX
		{
			get
			{
				return new Vector4i(Y, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Y).
		/// </summary>
		public Vector4i YXZY
		{
			get
			{
				return new Vector4i(Y, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Z).
		/// </summary>
		public Vector4i YXZZ
		{
			get
			{
				return new Vector4i(Y, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, W).
		/// </summary>
		public Vector4i YXZW
		{
			get
			{
				return new Vector4i(Y, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, X).
		/// </summary>
		public Vector4i YXWX
		{
			get
			{
				return new Vector4i(Y, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Y).
		/// </summary>
		public Vector4i YXWY
		{
			get
			{
				return new Vector4i(Y, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Z).
		/// </summary>
		public Vector4i YXWZ
		{
			get
			{
				return new Vector4i(Y, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, W).
		/// </summary>
		public Vector4i YXWW
		{
			get
			{
				return new Vector4i(Y, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, X).
		/// </summary>
		public Vector4i YYXX
		{
			get
			{
				return new Vector4i(Y, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Y).
		/// </summary>
		public Vector4i YYXY
		{
			get
			{
				return new Vector4i(Y, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Z).
		/// </summary>
		public Vector4i YYXZ
		{
			get
			{
				return new Vector4i(Y, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, W).
		/// </summary>
		public Vector4i YYXW
		{
			get
			{
				return new Vector4i(Y, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, X).
		/// </summary>
		public Vector4i YYYX
		{
			get
			{
				return new Vector4i(Y, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Y).
		/// </summary>
		public Vector4i YYYY
		{
			get
			{
				return new Vector4i(Y, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Z).
		/// </summary>
		public Vector4i YYYZ
		{
			get
			{
				return new Vector4i(Y, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, W).
		/// </summary>
		public Vector4i YYYW
		{
			get
			{
				return new Vector4i(Y, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, X).
		/// </summary>
		public Vector4i YYZX
		{
			get
			{
				return new Vector4i(Y, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Y).
		/// </summary>
		public Vector4i YYZY
		{
			get
			{
				return new Vector4i(Y, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Z).
		/// </summary>
		public Vector4i YYZZ
		{
			get
			{
				return new Vector4i(Y, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, W).
		/// </summary>
		public Vector4i YYZW
		{
			get
			{
				return new Vector4i(Y, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, X).
		/// </summary>
		public Vector4i YYWX
		{
			get
			{
				return new Vector4i(Y, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Y).
		/// </summary>
		public Vector4i YYWY
		{
			get
			{
				return new Vector4i(Y, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Z).
		/// </summary>
		public Vector4i YYWZ
		{
			get
			{
				return new Vector4i(Y, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, W).
		/// </summary>
		public Vector4i YYWW
		{
			get
			{
				return new Vector4i(Y, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, X).
		/// </summary>
		public Vector4i YZXX
		{
			get
			{
				return new Vector4i(Y, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Y).
		/// </summary>
		public Vector4i YZXY
		{
			get
			{
				return new Vector4i(Y, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Z).
		/// </summary>
		public Vector4i YZXZ
		{
			get
			{
				return new Vector4i(Y, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, W).
		/// </summary>
		public Vector4i YZXW
		{
			get
			{
				return new Vector4i(Y, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, X).
		/// </summary>
		public Vector4i YZYX
		{
			get
			{
				return new Vector4i(Y, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Y).
		/// </summary>
		public Vector4i YZYY
		{
			get
			{
				return new Vector4i(Y, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Z).
		/// </summary>
		public Vector4i YZYZ
		{
			get
			{
				return new Vector4i(Y, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, W).
		/// </summary>
		public Vector4i YZYW
		{
			get
			{
				return new Vector4i(Y, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, X).
		/// </summary>
		public Vector4i YZZX
		{
			get
			{
				return new Vector4i(Y, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Y).
		/// </summary>
		public Vector4i YZZY
		{
			get
			{
				return new Vector4i(Y, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Z).
		/// </summary>
		public Vector4i YZZZ
		{
			get
			{
				return new Vector4i(Y, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, W).
		/// </summary>
		public Vector4i YZZW
		{
			get
			{
				return new Vector4i(Y, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, X).
		/// </summary>
		public Vector4i YZWX
		{
			get
			{
				return new Vector4i(Y, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Y).
		/// </summary>
		public Vector4i YZWY
		{
			get
			{
				return new Vector4i(Y, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Z).
		/// </summary>
		public Vector4i YZWZ
		{
			get
			{
				return new Vector4i(Y, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, W).
		/// </summary>
		public Vector4i YZWW
		{
			get
			{
				return new Vector4i(Y, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, X).
		/// </summary>
		public Vector4i YWXX
		{
			get
			{
				return new Vector4i(Y, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Y).
		/// </summary>
		public Vector4i YWXY
		{
			get
			{
				return new Vector4i(Y, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Z).
		/// </summary>
		public Vector4i YWXZ
		{
			get
			{
				return new Vector4i(Y, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, W).
		/// </summary>
		public Vector4i YWXW
		{
			get
			{
				return new Vector4i(Y, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, X).
		/// </summary>
		public Vector4i YWYX
		{
			get
			{
				return new Vector4i(Y, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Y).
		/// </summary>
		public Vector4i YWYY
		{
			get
			{
				return new Vector4i(Y, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Z).
		/// </summary>
		public Vector4i YWYZ
		{
			get
			{
				return new Vector4i(Y, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, W).
		/// </summary>
		public Vector4i YWYW
		{
			get
			{
				return new Vector4i(Y, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, X).
		/// </summary>
		public Vector4i YWZX
		{
			get
			{
				return new Vector4i(Y, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Y).
		/// </summary>
		public Vector4i YWZY
		{
			get
			{
				return new Vector4i(Y, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Z).
		/// </summary>
		public Vector4i YWZZ
		{
			get
			{
				return new Vector4i(Y, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, W).
		/// </summary>
		public Vector4i YWZW
		{
			get
			{
				return new Vector4i(Y, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, X).
		/// </summary>
		public Vector4i YWWX
		{
			get
			{
				return new Vector4i(Y, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Y).
		/// </summary>
		public Vector4i YWWY
		{
			get
			{
				return new Vector4i(Y, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Z).
		/// </summary>
		public Vector4i YWWZ
		{
			get
			{
				return new Vector4i(Y, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, W).
		/// </summary>
		public Vector4i YWWW
		{
			get
			{
				return new Vector4i(Y, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, X).
		/// </summary>
		public Vector4i ZXXX
		{
			get
			{
				return new Vector4i(Z, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Y).
		/// </summary>
		public Vector4i ZXXY
		{
			get
			{
				return new Vector4i(Z, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Z).
		/// </summary>
		public Vector4i ZXXZ
		{
			get
			{
				return new Vector4i(Z, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, W).
		/// </summary>
		public Vector4i ZXXW
		{
			get
			{
				return new Vector4i(Z, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, X).
		/// </summary>
		public Vector4i ZXYX
		{
			get
			{
				return new Vector4i(Z, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Y).
		/// </summary>
		public Vector4i ZXYY
		{
			get
			{
				return new Vector4i(Z, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Z).
		/// </summary>
		public Vector4i ZXYZ
		{
			get
			{
				return new Vector4i(Z, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, W).
		/// </summary>
		public Vector4i ZXYW
		{
			get
			{
				return new Vector4i(Z, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, X).
		/// </summary>
		public Vector4i ZXZX
		{
			get
			{
				return new Vector4i(Z, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Y).
		/// </summary>
		public Vector4i ZXZY
		{
			get
			{
				return new Vector4i(Z, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Z).
		/// </summary>
		public Vector4i ZXZZ
		{
			get
			{
				return new Vector4i(Z, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, W).
		/// </summary>
		public Vector4i ZXZW
		{
			get
			{
				return new Vector4i(Z, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, X).
		/// </summary>
		public Vector4i ZXWX
		{
			get
			{
				return new Vector4i(Z, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Y).
		/// </summary>
		public Vector4i ZXWY
		{
			get
			{
				return new Vector4i(Z, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Z).
		/// </summary>
		public Vector4i ZXWZ
		{
			get
			{
				return new Vector4i(Z, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, W).
		/// </summary>
		public Vector4i ZXWW
		{
			get
			{
				return new Vector4i(Z, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, X).
		/// </summary>
		public Vector4i ZYXX
		{
			get
			{
				return new Vector4i(Z, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Y).
		/// </summary>
		public Vector4i ZYXY
		{
			get
			{
				return new Vector4i(Z, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Z).
		/// </summary>
		public Vector4i ZYXZ
		{
			get
			{
				return new Vector4i(Z, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, W).
		/// </summary>
		public Vector4i ZYXW
		{
			get
			{
				return new Vector4i(Z, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, X).
		/// </summary>
		public Vector4i ZYYX
		{
			get
			{
				return new Vector4i(Z, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Y).
		/// </summary>
		public Vector4i ZYYY
		{
			get
			{
				return new Vector4i(Z, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Z).
		/// </summary>
		public Vector4i ZYYZ
		{
			get
			{
				return new Vector4i(Z, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, W).
		/// </summary>
		public Vector4i ZYYW
		{
			get
			{
				return new Vector4i(Z, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, X).
		/// </summary>
		public Vector4i ZYZX
		{
			get
			{
				return new Vector4i(Z, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Y).
		/// </summary>
		public Vector4i ZYZY
		{
			get
			{
				return new Vector4i(Z, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Z).
		/// </summary>
		public Vector4i ZYZZ
		{
			get
			{
				return new Vector4i(Z, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, W).
		/// </summary>
		public Vector4i ZYZW
		{
			get
			{
				return new Vector4i(Z, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, X).
		/// </summary>
		public Vector4i ZYWX
		{
			get
			{
				return new Vector4i(Z, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Y).
		/// </summary>
		public Vector4i ZYWY
		{
			get
			{
				return new Vector4i(Z, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Z).
		/// </summary>
		public Vector4i ZYWZ
		{
			get
			{
				return new Vector4i(Z, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, W).
		/// </summary>
		public Vector4i ZYWW
		{
			get
			{
				return new Vector4i(Z, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, X).
		/// </summary>
		public Vector4i ZZXX
		{
			get
			{
				return new Vector4i(Z, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Y).
		/// </summary>
		public Vector4i ZZXY
		{
			get
			{
				return new Vector4i(Z, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Z).
		/// </summary>
		public Vector4i ZZXZ
		{
			get
			{
				return new Vector4i(Z, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, W).
		/// </summary>
		public Vector4i ZZXW
		{
			get
			{
				return new Vector4i(Z, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, X).
		/// </summary>
		public Vector4i ZZYX
		{
			get
			{
				return new Vector4i(Z, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Y).
		/// </summary>
		public Vector4i ZZYY
		{
			get
			{
				return new Vector4i(Z, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Z).
		/// </summary>
		public Vector4i ZZYZ
		{
			get
			{
				return new Vector4i(Z, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, W).
		/// </summary>
		public Vector4i ZZYW
		{
			get
			{
				return new Vector4i(Z, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, X).
		/// </summary>
		public Vector4i ZZZX
		{
			get
			{
				return new Vector4i(Z, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Y).
		/// </summary>
		public Vector4i ZZZY
		{
			get
			{
				return new Vector4i(Z, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Z).
		/// </summary>
		public Vector4i ZZZZ
		{
			get
			{
				return new Vector4i(Z, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, W).
		/// </summary>
		public Vector4i ZZZW
		{
			get
			{
				return new Vector4i(Z, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, X).
		/// </summary>
		public Vector4i ZZWX
		{
			get
			{
				return new Vector4i(Z, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Y).
		/// </summary>
		public Vector4i ZZWY
		{
			get
			{
				return new Vector4i(Z, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Z).
		/// </summary>
		public Vector4i ZZWZ
		{
			get
			{
				return new Vector4i(Z, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, W).
		/// </summary>
		public Vector4i ZZWW
		{
			get
			{
				return new Vector4i(Z, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, X).
		/// </summary>
		public Vector4i ZWXX
		{
			get
			{
				return new Vector4i(Z, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Y).
		/// </summary>
		public Vector4i ZWXY
		{
			get
			{
				return new Vector4i(Z, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Z).
		/// </summary>
		public Vector4i ZWXZ
		{
			get
			{
				return new Vector4i(Z, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, W).
		/// </summary>
		public Vector4i ZWXW
		{
			get
			{
				return new Vector4i(Z, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, X).
		/// </summary>
		public Vector4i ZWYX
		{
			get
			{
				return new Vector4i(Z, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Y).
		/// </summary>
		public Vector4i ZWYY
		{
			get
			{
				return new Vector4i(Z, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Z).
		/// </summary>
		public Vector4i ZWYZ
		{
			get
			{
				return new Vector4i(Z, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, W).
		/// </summary>
		public Vector4i ZWYW
		{
			get
			{
				return new Vector4i(Z, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, X).
		/// </summary>
		public Vector4i ZWZX
		{
			get
			{
				return new Vector4i(Z, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Y).
		/// </summary>
		public Vector4i ZWZY
		{
			get
			{
				return new Vector4i(Z, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Z).
		/// </summary>
		public Vector4i ZWZZ
		{
			get
			{
				return new Vector4i(Z, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, W).
		/// </summary>
		public Vector4i ZWZW
		{
			get
			{
				return new Vector4i(Z, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, X).
		/// </summary>
		public Vector4i ZWWX
		{
			get
			{
				return new Vector4i(Z, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Y).
		/// </summary>
		public Vector4i ZWWY
		{
			get
			{
				return new Vector4i(Z, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Z).
		/// </summary>
		public Vector4i ZWWZ
		{
			get
			{
				return new Vector4i(Z, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, W).
		/// </summary>
		public Vector4i ZWWW
		{
			get
			{
				return new Vector4i(Z, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, X).
		/// </summary>
		public Vector4i WXXX
		{
			get
			{
				return new Vector4i(W, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Y).
		/// </summary>
		public Vector4i WXXY
		{
			get
			{
				return new Vector4i(W, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Z).
		/// </summary>
		public Vector4i WXXZ
		{
			get
			{
				return new Vector4i(W, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, W).
		/// </summary>
		public Vector4i WXXW
		{
			get
			{
				return new Vector4i(W, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, X).
		/// </summary>
		public Vector4i WXYX
		{
			get
			{
				return new Vector4i(W, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Y).
		/// </summary>
		public Vector4i WXYY
		{
			get
			{
				return new Vector4i(W, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Z).
		/// </summary>
		public Vector4i WXYZ
		{
			get
			{
				return new Vector4i(W, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, W).
		/// </summary>
		public Vector4i WXYW
		{
			get
			{
				return new Vector4i(W, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, X).
		/// </summary>
		public Vector4i WXZX
		{
			get
			{
				return new Vector4i(W, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Y).
		/// </summary>
		public Vector4i WXZY
		{
			get
			{
				return new Vector4i(W, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Z).
		/// </summary>
		public Vector4i WXZZ
		{
			get
			{
				return new Vector4i(W, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, W).
		/// </summary>
		public Vector4i WXZW
		{
			get
			{
				return new Vector4i(W, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, X).
		/// </summary>
		public Vector4i WXWX
		{
			get
			{
				return new Vector4i(W, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Y).
		/// </summary>
		public Vector4i WXWY
		{
			get
			{
				return new Vector4i(W, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Z).
		/// </summary>
		public Vector4i WXWZ
		{
			get
			{
				return new Vector4i(W, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, W).
		/// </summary>
		public Vector4i WXWW
		{
			get
			{
				return new Vector4i(W, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, X).
		/// </summary>
		public Vector4i WYXX
		{
			get
			{
				return new Vector4i(W, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Y).
		/// </summary>
		public Vector4i WYXY
		{
			get
			{
				return new Vector4i(W, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Z).
		/// </summary>
		public Vector4i WYXZ
		{
			get
			{
				return new Vector4i(W, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, W).
		/// </summary>
		public Vector4i WYXW
		{
			get
			{
				return new Vector4i(W, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, X).
		/// </summary>
		public Vector4i WYYX
		{
			get
			{
				return new Vector4i(W, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Y).
		/// </summary>
		public Vector4i WYYY
		{
			get
			{
				return new Vector4i(W, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Z).
		/// </summary>
		public Vector4i WYYZ
		{
			get
			{
				return new Vector4i(W, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, W).
		/// </summary>
		public Vector4i WYYW
		{
			get
			{
				return new Vector4i(W, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, X).
		/// </summary>
		public Vector4i WYZX
		{
			get
			{
				return new Vector4i(W, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Y).
		/// </summary>
		public Vector4i WYZY
		{
			get
			{
				return new Vector4i(W, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Z).
		/// </summary>
		public Vector4i WYZZ
		{
			get
			{
				return new Vector4i(W, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, W).
		/// </summary>
		public Vector4i WYZW
		{
			get
			{
				return new Vector4i(W, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, X).
		/// </summary>
		public Vector4i WYWX
		{
			get
			{
				return new Vector4i(W, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Y).
		/// </summary>
		public Vector4i WYWY
		{
			get
			{
				return new Vector4i(W, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Z).
		/// </summary>
		public Vector4i WYWZ
		{
			get
			{
				return new Vector4i(W, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, W).
		/// </summary>
		public Vector4i WYWW
		{
			get
			{
				return new Vector4i(W, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, X).
		/// </summary>
		public Vector4i WZXX
		{
			get
			{
				return new Vector4i(W, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Y).
		/// </summary>
		public Vector4i WZXY
		{
			get
			{
				return new Vector4i(W, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Z).
		/// </summary>
		public Vector4i WZXZ
		{
			get
			{
				return new Vector4i(W, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, W).
		/// </summary>
		public Vector4i WZXW
		{
			get
			{
				return new Vector4i(W, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, X).
		/// </summary>
		public Vector4i WZYX
		{
			get
			{
				return new Vector4i(W, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Y).
		/// </summary>
		public Vector4i WZYY
		{
			get
			{
				return new Vector4i(W, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Z).
		/// </summary>
		public Vector4i WZYZ
		{
			get
			{
				return new Vector4i(W, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, W).
		/// </summary>
		public Vector4i WZYW
		{
			get
			{
				return new Vector4i(W, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, X).
		/// </summary>
		public Vector4i WZZX
		{
			get
			{
				return new Vector4i(W, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Y).
		/// </summary>
		public Vector4i WZZY
		{
			get
			{
				return new Vector4i(W, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Z).
		/// </summary>
		public Vector4i WZZZ
		{
			get
			{
				return new Vector4i(W, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, W).
		/// </summary>
		public Vector4i WZZW
		{
			get
			{
				return new Vector4i(W, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, X).
		/// </summary>
		public Vector4i WZWX
		{
			get
			{
				return new Vector4i(W, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Y).
		/// </summary>
		public Vector4i WZWY
		{
			get
			{
				return new Vector4i(W, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Z).
		/// </summary>
		public Vector4i WZWZ
		{
			get
			{
				return new Vector4i(W, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, W).
		/// </summary>
		public Vector4i WZWW
		{
			get
			{
				return new Vector4i(W, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, X).
		/// </summary>
		public Vector4i WWXX
		{
			get
			{
				return new Vector4i(W, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Y).
		/// </summary>
		public Vector4i WWXY
		{
			get
			{
				return new Vector4i(W, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Z).
		/// </summary>
		public Vector4i WWXZ
		{
			get
			{
				return new Vector4i(W, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, W).
		/// </summary>
		public Vector4i WWXW
		{
			get
			{
				return new Vector4i(W, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, X).
		/// </summary>
		public Vector4i WWYX
		{
			get
			{
				return new Vector4i(W, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Y).
		/// </summary>
		public Vector4i WWYY
		{
			get
			{
				return new Vector4i(W, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Z).
		/// </summary>
		public Vector4i WWYZ
		{
			get
			{
				return new Vector4i(W, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, W).
		/// </summary>
		public Vector4i WWYW
		{
			get
			{
				return new Vector4i(W, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, X).
		/// </summary>
		public Vector4i WWZX
		{
			get
			{
				return new Vector4i(W, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Y).
		/// </summary>
		public Vector4i WWZY
		{
			get
			{
				return new Vector4i(W, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Z).
		/// </summary>
		public Vector4i WWZZ
		{
			get
			{
				return new Vector4i(W, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, W).
		/// </summary>
		public Vector4i WWZW
		{
			get
			{
				return new Vector4i(W, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, X).
		/// </summary>
		public Vector4i WWWX
		{
			get
			{
				return new Vector4i(W, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Y).
		/// </summary>
		public Vector4i WWWY
		{
			get
			{
				return new Vector4i(W, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Z).
		/// </summary>
		public Vector4i WWWZ
		{
			get
			{
				return new Vector4i(W, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, W).
		/// </summary>
		public Vector4i WWWW
		{
			get
			{
				return new Vector4i(W, W, W, W);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4i"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector4i(int value)
		{
			X = value;
			Y = value;
			Z = value;
			W = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4i"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X and Y components</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4i(Vector2i value, int z, int w)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4i"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X, Y and Z components</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4i(Vector3i value, int w)
		{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4i"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4i(int x, int y, int z, int w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4i"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector4i(int[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
			W = array[3];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4i"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector4i(int[] array, int offset)
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
		public static Vector4i operator +(Vector4i value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4i operator -(Vector4i value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4i operator +(Vector4i left, Vector4i right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4i operator -(Vector4i left, Vector4i right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4i operator *(Vector4i left, int right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4i operator *(int left, Vector4i right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4i operator /(Vector4i left, int right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector4d value to a Vector4i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4i.</param>
		/// <returns>A Vector4i that has all components equal to value.</returns>
		public static explicit operator Vector4i(Vector4d value)
		{
			return new Vector4i((int)value.X, (int)value.Y, (int)value.Z, (int)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4f value to a Vector4i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4i.</param>
		/// <returns>A Vector4i that has all components equal to value.</returns>
		public static explicit operator Vector4i(Vector4f value)
		{
			return new Vector4i((int)value.X, (int)value.Y, (int)value.Z, (int)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4h value to a Vector4i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4i.</param>
		/// <returns>A Vector4i that has all components equal to value.</returns>
		public static explicit operator Vector4i(Vector4h value)
		{
			return new Vector4i((int)value.X, (int)value.Y, (int)value.Z, (int)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4ul value to a Vector4i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4i.</param>
		/// <returns>A Vector4i that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4i(Vector4ul value)
		{
			return new Vector4i((int)value.X, (int)value.Y, (int)value.Z, (int)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4l value to a Vector4i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4i.</param>
		/// <returns>A Vector4i that has all components equal to value.</returns>
		public static explicit operator Vector4i(Vector4l value)
		{
			return new Vector4i((int)value.X, (int)value.Y, (int)value.Z, (int)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4ui value to a Vector4i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4i.</param>
		/// <returns>A Vector4i that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4i(Vector4ui value)
		{
			return new Vector4i((int)value.X, (int)value.Y, (int)value.Z, (int)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4us value to a Vector4i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4i.</param>
		/// <returns>A Vector4i that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector4i(Vector4us value)
		{
			return new Vector4i((int)value.X, (int)value.Y, (int)value.Z, (int)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4s value to a Vector4i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4i.</param>
		/// <returns>A Vector4i that has all components equal to value.</returns>
		public static implicit operator Vector4i(Vector4s value)
		{
			return new Vector4i((int)value.X, (int)value.Y, (int)value.Z, (int)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4b value to a Vector4i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4i.</param>
		/// <returns>A Vector4i that has all components equal to value.</returns>
		public static implicit operator Vector4i(Vector4b value)
		{
			return new Vector4i((int)value.X, (int)value.Y, (int)value.Z, (int)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4sb value to a Vector4i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4i.</param>
		/// <returns>A Vector4i that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector4i(Vector4sb value)
		{
			return new Vector4i((int)value.X, (int)value.Y, (int)value.Z, (int)value.W);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector4i"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector4i"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector4i"/> object, and its value
		/// is equal to the current <see cref="Vector4i"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector4i) { return Equals((Vector4i)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector4i other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector4i left, Vector4i right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z & left.W == right.W;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector4i left, Vector4i right)
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
		/// Writes the given <see cref="Vector4i"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector4i vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
			writer.Write(vector.Z);
			writer.Write(vector.W);
		}
		/// <summary>
		/// Reads a <see cref="Vector4i"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector4i ReadVector4i(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector4i(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
		}
		#endregion
		#region Pack
		public static long Pack(int xBits, int yBits, int zBits, int wBits, Vector4i vector)
		{
			Contract.Requires(0 <= xBits && xBits <= 32, "xBits must be between 0 and 32 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 32, "yBits must be between 0 and 32 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 32, "zBits must be between 0 and 32 inclusive.");
			Contract.Requires(0 <= wBits && wBits <= 32, "wBits must be between 0 and 32 inclusive.");
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
		public static Vector4i Unpack(int xBits, int yBits, int zBits, int wBits, int bits)
		{
			Contract.Requires(0 <= xBits && xBits <= 32, "xBits must be between 0 and 32 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 32, "yBits must be between 0 and 32 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 32, "zBits must be between 0 and 32 inclusive.");
			Contract.Requires(0 <= wBits && wBits <= 32, "wBits must be between 0 and 32 inclusive.");
			Contract.Requires(xBits + yBits + zBits + wBits <= 64);
			ulong x = (ulong)(bits);
			x &= ((1UL << xBits) - 1);
			ulong y = (ulong)(bits) >> (xBits);
			y &= ((1UL << yBits) - 1);
			ulong z = (ulong)(bits) >> (xBits + yBits);
			z &= ((1UL << zBits) - 1);
			ulong w = (ulong)(bits) >> (xBits + yBits + zBits);
			w &= ((1UL << wBits) - 1);
			return new Vector4i((int)x, (int)y, (int)z, (int)w);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4i Negative(Vector4i value)
		{
			return new Vector4i(-value.X, -value.Y, -value.Z, -value.W);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4i Add(Vector4i left, Vector4i right)
		{
			return new Vector4i(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4i Subtract(Vector4i left, Vector4i right)
		{
			return new Vector4i(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4i Multiply(Vector4i vector, int scalar)
		{
			return new Vector4i(vector.X * scalar, vector.Y * scalar, vector.Z * scalar, vector.W * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4i Divide(Vector4i vector, int scalar)
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
		public static bool Equals(Vector4i left, Vector4i right)
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
		public static int Dot(Vector4i left, Vector4i right)
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
		public static bool All(Vector4i value)
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
		public static bool All(Vector4i value, Predicate<int> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z) && predicate(value.W);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Vector4i value)
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
		public static bool Any(Vector4i value, Predicate<int> predicate)
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
		public static int AbsoluteSquared(Vector4i value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		public static float Absolute(Vector4i value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		public static Vector4f Normalize(Vector4i value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Vector4i.Zero;
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
		public static Vector4d Transform(Vector4i value, Func<int, double> transformer)
		{
			return new Vector4d(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4f Transform(Vector4i value, Func<int, float> transformer)
		{
			return new Vector4f(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4h Transform(Vector4i value, Func<int, Half> transformer)
		{
			return new Vector4h(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4ul Transform(Vector4i value, Func<int, ulong> transformer)
		{
			return new Vector4ul(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4l Transform(Vector4i value, Func<int, long> transformer)
		{
			return new Vector4l(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4ui Transform(Vector4i value, Func<int, uint> transformer)
		{
			return new Vector4ui(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4i Transform(Vector4i value, Func<int, int> transformer)
		{
			return new Vector4i(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4us Transform(Vector4i value, Func<int, ushort> transformer)
		{
			return new Vector4us(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4s Transform(Vector4i value, Func<int, short> transformer)
		{
			return new Vector4s(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4b Transform(Vector4i value, Func<int, byte> transformer)
		{
			return new Vector4b(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4sb Transform(Vector4i value, Func<int, sbyte> transformer)
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
		public static Vector4i Modulate(Vector4i left, Vector4i right)
		{
			return new Vector4i(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Vector4i Abs(Vector4i value)
		{
			return new Vector4i(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z), Functions.Abs(value.W));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Vector4i Min(Vector4i value1, Vector4i value2)
		{
			return new Vector4i(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z), Functions.Min(value1.W, value2.W));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Vector4i Max(Vector4i value1, Vector4i value2)
		{
			return new Vector4i(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z), Functions.Max(value1.W, value2.W));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		public static Vector4i Clamp(Vector4i value, Vector4i min, Vector4i max)
		{
			return new Vector4i(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z), Functions.Clamp(value.W, min.W, max.W));
		}
		#endregion
	}
}
