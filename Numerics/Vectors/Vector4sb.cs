using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Numerics
{
	/// <summary>
	/// Represents a four component vector of sbytes, of the form (X, Y, Z, W).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	[CLSCompliant(false)]
	public struct Vector4sb: IEquatable<Vector4sb>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector4sb"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector4sb Zero = new Vector4sb(0);
		/// <summary>
		/// Returns a new <see cref="Vector4sb"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector4sb One = new Vector4sb(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector4sb"/> (1, 0, 0, 0).
		/// </summary>
		public static readonly Vector4sb UnitX = new Vector4sb(1, 0, 0, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector4sb"/> (0, 1, 0, 0).
		/// </summary>
		public static readonly Vector4sb UnitY = new Vector4sb(0, 1, 0, 0);
		/// <summary>
		/// Returns the Z unit <see cref="Vector4sb"/> (0, 0, 1, 0).
		/// </summary>
		public static readonly Vector4sb UnitZ = new Vector4sb(0, 0, 1, 0);
		/// <summary>
		/// Returns the W unit <see cref="Vector4sb"/> (0, 0, 0, 1).
		/// </summary>
		public static readonly Vector4sb UnitW = new Vector4sb(0, 0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the vector.
		/// </summary>
		public readonly sbyte X;
		/// <summary>
		/// The Y component of the vector.
		/// </summary>
		public readonly sbyte Y;
		/// <summary>
		/// The Z component of the vector.
		/// </summary>
		public readonly sbyte Z;
		/// <summary>
		/// The W component of the vector.
		/// </summary>
		public readonly sbyte W;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public sbyte this[int index]
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
						throw new IndexOutOfRangeException("Indices for Vector4sb run from 0 to 3, inclusive.");
				}
			}
		}
		public sbyte[] ToArray()
		{
			return new sbyte[]
			{
				X, Y, Z, W
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the vector (X, X).
		/// </summary>
		public Vector2sb XX
		{
			get
			{
				return new Vector2sb(X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y).
		/// </summary>
		public Vector2sb XY
		{
			get
			{
				return new Vector2sb(X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z).
		/// </summary>
		public Vector2sb XZ
		{
			get
			{
				return new Vector2sb(X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W).
		/// </summary>
		public Vector2sb XW
		{
			get
			{
				return new Vector2sb(X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X).
		/// </summary>
		public Vector2sb YX
		{
			get
			{
				return new Vector2sb(Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y).
		/// </summary>
		public Vector2sb YY
		{
			get
			{
				return new Vector2sb(Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z).
		/// </summary>
		public Vector2sb YZ
		{
			get
			{
				return new Vector2sb(Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W).
		/// </summary>
		public Vector2sb YW
		{
			get
			{
				return new Vector2sb(Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X).
		/// </summary>
		public Vector2sb ZX
		{
			get
			{
				return new Vector2sb(Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y).
		/// </summary>
		public Vector2sb ZY
		{
			get
			{
				return new Vector2sb(Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z).
		/// </summary>
		public Vector2sb ZZ
		{
			get
			{
				return new Vector2sb(Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W).
		/// </summary>
		public Vector2sb ZW
		{
			get
			{
				return new Vector2sb(Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X).
		/// </summary>
		public Vector2sb WX
		{
			get
			{
				return new Vector2sb(W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y).
		/// </summary>
		public Vector2sb WY
		{
			get
			{
				return new Vector2sb(W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z).
		/// </summary>
		public Vector2sb WZ
		{
			get
			{
				return new Vector2sb(W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W).
		/// </summary>
		public Vector2sb WW
		{
			get
			{
				return new Vector2sb(W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X).
		/// </summary>
		public Vector3sb XXX
		{
			get
			{
				return new Vector3sb(X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y).
		/// </summary>
		public Vector3sb XXY
		{
			get
			{
				return new Vector3sb(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z).
		/// </summary>
		public Vector3sb XXZ
		{
			get
			{
				return new Vector3sb(X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W).
		/// </summary>
		public Vector3sb XXW
		{
			get
			{
				return new Vector3sb(X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X).
		/// </summary>
		public Vector3sb XYX
		{
			get
			{
				return new Vector3sb(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y).
		/// </summary>
		public Vector3sb XYY
		{
			get
			{
				return new Vector3sb(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z).
		/// </summary>
		public Vector3sb XYZ
		{
			get
			{
				return new Vector3sb(X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W).
		/// </summary>
		public Vector3sb XYW
		{
			get
			{
				return new Vector3sb(X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X).
		/// </summary>
		public Vector3sb XZX
		{
			get
			{
				return new Vector3sb(X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y).
		/// </summary>
		public Vector3sb XZY
		{
			get
			{
				return new Vector3sb(X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z).
		/// </summary>
		public Vector3sb XZZ
		{
			get
			{
				return new Vector3sb(X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W).
		/// </summary>
		public Vector3sb XZW
		{
			get
			{
				return new Vector3sb(X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X).
		/// </summary>
		public Vector3sb XWX
		{
			get
			{
				return new Vector3sb(X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y).
		/// </summary>
		public Vector3sb XWY
		{
			get
			{
				return new Vector3sb(X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z).
		/// </summary>
		public Vector3sb XWZ
		{
			get
			{
				return new Vector3sb(X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W).
		/// </summary>
		public Vector3sb XWW
		{
			get
			{
				return new Vector3sb(X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X).
		/// </summary>
		public Vector3sb YXX
		{
			get
			{
				return new Vector3sb(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y).
		/// </summary>
		public Vector3sb YXY
		{
			get
			{
				return new Vector3sb(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z).
		/// </summary>
		public Vector3sb YXZ
		{
			get
			{
				return new Vector3sb(Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W).
		/// </summary>
		public Vector3sb YXW
		{
			get
			{
				return new Vector3sb(Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X).
		/// </summary>
		public Vector3sb YYX
		{
			get
			{
				return new Vector3sb(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y).
		/// </summary>
		public Vector3sb YYY
		{
			get
			{
				return new Vector3sb(Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z).
		/// </summary>
		public Vector3sb YYZ
		{
			get
			{
				return new Vector3sb(Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W).
		/// </summary>
		public Vector3sb YYW
		{
			get
			{
				return new Vector3sb(Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X).
		/// </summary>
		public Vector3sb YZX
		{
			get
			{
				return new Vector3sb(Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y).
		/// </summary>
		public Vector3sb YZY
		{
			get
			{
				return new Vector3sb(Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z).
		/// </summary>
		public Vector3sb YZZ
		{
			get
			{
				return new Vector3sb(Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W).
		/// </summary>
		public Vector3sb YZW
		{
			get
			{
				return new Vector3sb(Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X).
		/// </summary>
		public Vector3sb YWX
		{
			get
			{
				return new Vector3sb(Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y).
		/// </summary>
		public Vector3sb YWY
		{
			get
			{
				return new Vector3sb(Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z).
		/// </summary>
		public Vector3sb YWZ
		{
			get
			{
				return new Vector3sb(Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W).
		/// </summary>
		public Vector3sb YWW
		{
			get
			{
				return new Vector3sb(Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X).
		/// </summary>
		public Vector3sb ZXX
		{
			get
			{
				return new Vector3sb(Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y).
		/// </summary>
		public Vector3sb ZXY
		{
			get
			{
				return new Vector3sb(Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z).
		/// </summary>
		public Vector3sb ZXZ
		{
			get
			{
				return new Vector3sb(Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W).
		/// </summary>
		public Vector3sb ZXW
		{
			get
			{
				return new Vector3sb(Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X).
		/// </summary>
		public Vector3sb ZYX
		{
			get
			{
				return new Vector3sb(Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y).
		/// </summary>
		public Vector3sb ZYY
		{
			get
			{
				return new Vector3sb(Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z).
		/// </summary>
		public Vector3sb ZYZ
		{
			get
			{
				return new Vector3sb(Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W).
		/// </summary>
		public Vector3sb ZYW
		{
			get
			{
				return new Vector3sb(Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X).
		/// </summary>
		public Vector3sb ZZX
		{
			get
			{
				return new Vector3sb(Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y).
		/// </summary>
		public Vector3sb ZZY
		{
			get
			{
				return new Vector3sb(Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z).
		/// </summary>
		public Vector3sb ZZZ
		{
			get
			{
				return new Vector3sb(Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W).
		/// </summary>
		public Vector3sb ZZW
		{
			get
			{
				return new Vector3sb(Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X).
		/// </summary>
		public Vector3sb ZWX
		{
			get
			{
				return new Vector3sb(Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y).
		/// </summary>
		public Vector3sb ZWY
		{
			get
			{
				return new Vector3sb(Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z).
		/// </summary>
		public Vector3sb ZWZ
		{
			get
			{
				return new Vector3sb(Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W).
		/// </summary>
		public Vector3sb ZWW
		{
			get
			{
				return new Vector3sb(Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X).
		/// </summary>
		public Vector3sb WXX
		{
			get
			{
				return new Vector3sb(W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y).
		/// </summary>
		public Vector3sb WXY
		{
			get
			{
				return new Vector3sb(W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z).
		/// </summary>
		public Vector3sb WXZ
		{
			get
			{
				return new Vector3sb(W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W).
		/// </summary>
		public Vector3sb WXW
		{
			get
			{
				return new Vector3sb(W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X).
		/// </summary>
		public Vector3sb WYX
		{
			get
			{
				return new Vector3sb(W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y).
		/// </summary>
		public Vector3sb WYY
		{
			get
			{
				return new Vector3sb(W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z).
		/// </summary>
		public Vector3sb WYZ
		{
			get
			{
				return new Vector3sb(W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W).
		/// </summary>
		public Vector3sb WYW
		{
			get
			{
				return new Vector3sb(W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X).
		/// </summary>
		public Vector3sb WZX
		{
			get
			{
				return new Vector3sb(W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y).
		/// </summary>
		public Vector3sb WZY
		{
			get
			{
				return new Vector3sb(W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z).
		/// </summary>
		public Vector3sb WZZ
		{
			get
			{
				return new Vector3sb(W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W).
		/// </summary>
		public Vector3sb WZW
		{
			get
			{
				return new Vector3sb(W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X).
		/// </summary>
		public Vector3sb WWX
		{
			get
			{
				return new Vector3sb(W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y).
		/// </summary>
		public Vector3sb WWY
		{
			get
			{
				return new Vector3sb(W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z).
		/// </summary>
		public Vector3sb WWZ
		{
			get
			{
				return new Vector3sb(W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W).
		/// </summary>
		public Vector3sb WWW
		{
			get
			{
				return new Vector3sb(W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, X).
		/// </summary>
		public Vector4sb XXXX
		{
			get
			{
				return new Vector4sb(X, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Y).
		/// </summary>
		public Vector4sb XXXY
		{
			get
			{
				return new Vector4sb(X, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Z).
		/// </summary>
		public Vector4sb XXXZ
		{
			get
			{
				return new Vector4sb(X, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, W).
		/// </summary>
		public Vector4sb XXXW
		{
			get
			{
				return new Vector4sb(X, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, X).
		/// </summary>
		public Vector4sb XXYX
		{
			get
			{
				return new Vector4sb(X, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Y).
		/// </summary>
		public Vector4sb XXYY
		{
			get
			{
				return new Vector4sb(X, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Z).
		/// </summary>
		public Vector4sb XXYZ
		{
			get
			{
				return new Vector4sb(X, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, W).
		/// </summary>
		public Vector4sb XXYW
		{
			get
			{
				return new Vector4sb(X, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, X).
		/// </summary>
		public Vector4sb XXZX
		{
			get
			{
				return new Vector4sb(X, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Y).
		/// </summary>
		public Vector4sb XXZY
		{
			get
			{
				return new Vector4sb(X, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Z).
		/// </summary>
		public Vector4sb XXZZ
		{
			get
			{
				return new Vector4sb(X, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, W).
		/// </summary>
		public Vector4sb XXZW
		{
			get
			{
				return new Vector4sb(X, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, X).
		/// </summary>
		public Vector4sb XXWX
		{
			get
			{
				return new Vector4sb(X, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Y).
		/// </summary>
		public Vector4sb XXWY
		{
			get
			{
				return new Vector4sb(X, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Z).
		/// </summary>
		public Vector4sb XXWZ
		{
			get
			{
				return new Vector4sb(X, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, W).
		/// </summary>
		public Vector4sb XXWW
		{
			get
			{
				return new Vector4sb(X, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, X).
		/// </summary>
		public Vector4sb XYXX
		{
			get
			{
				return new Vector4sb(X, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Y).
		/// </summary>
		public Vector4sb XYXY
		{
			get
			{
				return new Vector4sb(X, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Z).
		/// </summary>
		public Vector4sb XYXZ
		{
			get
			{
				return new Vector4sb(X, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, W).
		/// </summary>
		public Vector4sb XYXW
		{
			get
			{
				return new Vector4sb(X, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, X).
		/// </summary>
		public Vector4sb XYYX
		{
			get
			{
				return new Vector4sb(X, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Y).
		/// </summary>
		public Vector4sb XYYY
		{
			get
			{
				return new Vector4sb(X, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Z).
		/// </summary>
		public Vector4sb XYYZ
		{
			get
			{
				return new Vector4sb(X, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, W).
		/// </summary>
		public Vector4sb XYYW
		{
			get
			{
				return new Vector4sb(X, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, X).
		/// </summary>
		public Vector4sb XYZX
		{
			get
			{
				return new Vector4sb(X, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Y).
		/// </summary>
		public Vector4sb XYZY
		{
			get
			{
				return new Vector4sb(X, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Z).
		/// </summary>
		public Vector4sb XYZZ
		{
			get
			{
				return new Vector4sb(X, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, W).
		/// </summary>
		public Vector4sb XYZW
		{
			get
			{
				return new Vector4sb(X, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, X).
		/// </summary>
		public Vector4sb XYWX
		{
			get
			{
				return new Vector4sb(X, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Y).
		/// </summary>
		public Vector4sb XYWY
		{
			get
			{
				return new Vector4sb(X, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Z).
		/// </summary>
		public Vector4sb XYWZ
		{
			get
			{
				return new Vector4sb(X, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, W).
		/// </summary>
		public Vector4sb XYWW
		{
			get
			{
				return new Vector4sb(X, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, X).
		/// </summary>
		public Vector4sb XZXX
		{
			get
			{
				return new Vector4sb(X, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Y).
		/// </summary>
		public Vector4sb XZXY
		{
			get
			{
				return new Vector4sb(X, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Z).
		/// </summary>
		public Vector4sb XZXZ
		{
			get
			{
				return new Vector4sb(X, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, W).
		/// </summary>
		public Vector4sb XZXW
		{
			get
			{
				return new Vector4sb(X, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, X).
		/// </summary>
		public Vector4sb XZYX
		{
			get
			{
				return new Vector4sb(X, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Y).
		/// </summary>
		public Vector4sb XZYY
		{
			get
			{
				return new Vector4sb(X, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Z).
		/// </summary>
		public Vector4sb XZYZ
		{
			get
			{
				return new Vector4sb(X, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, W).
		/// </summary>
		public Vector4sb XZYW
		{
			get
			{
				return new Vector4sb(X, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, X).
		/// </summary>
		public Vector4sb XZZX
		{
			get
			{
				return new Vector4sb(X, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Y).
		/// </summary>
		public Vector4sb XZZY
		{
			get
			{
				return new Vector4sb(X, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Z).
		/// </summary>
		public Vector4sb XZZZ
		{
			get
			{
				return new Vector4sb(X, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, W).
		/// </summary>
		public Vector4sb XZZW
		{
			get
			{
				return new Vector4sb(X, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, X).
		/// </summary>
		public Vector4sb XZWX
		{
			get
			{
				return new Vector4sb(X, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Y).
		/// </summary>
		public Vector4sb XZWY
		{
			get
			{
				return new Vector4sb(X, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Z).
		/// </summary>
		public Vector4sb XZWZ
		{
			get
			{
				return new Vector4sb(X, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, W).
		/// </summary>
		public Vector4sb XZWW
		{
			get
			{
				return new Vector4sb(X, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, X).
		/// </summary>
		public Vector4sb XWXX
		{
			get
			{
				return new Vector4sb(X, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Y).
		/// </summary>
		public Vector4sb XWXY
		{
			get
			{
				return new Vector4sb(X, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Z).
		/// </summary>
		public Vector4sb XWXZ
		{
			get
			{
				return new Vector4sb(X, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, W).
		/// </summary>
		public Vector4sb XWXW
		{
			get
			{
				return new Vector4sb(X, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, X).
		/// </summary>
		public Vector4sb XWYX
		{
			get
			{
				return new Vector4sb(X, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Y).
		/// </summary>
		public Vector4sb XWYY
		{
			get
			{
				return new Vector4sb(X, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Z).
		/// </summary>
		public Vector4sb XWYZ
		{
			get
			{
				return new Vector4sb(X, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, W).
		/// </summary>
		public Vector4sb XWYW
		{
			get
			{
				return new Vector4sb(X, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, X).
		/// </summary>
		public Vector4sb XWZX
		{
			get
			{
				return new Vector4sb(X, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Y).
		/// </summary>
		public Vector4sb XWZY
		{
			get
			{
				return new Vector4sb(X, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Z).
		/// </summary>
		public Vector4sb XWZZ
		{
			get
			{
				return new Vector4sb(X, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, W).
		/// </summary>
		public Vector4sb XWZW
		{
			get
			{
				return new Vector4sb(X, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, X).
		/// </summary>
		public Vector4sb XWWX
		{
			get
			{
				return new Vector4sb(X, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Y).
		/// </summary>
		public Vector4sb XWWY
		{
			get
			{
				return new Vector4sb(X, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Z).
		/// </summary>
		public Vector4sb XWWZ
		{
			get
			{
				return new Vector4sb(X, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, W).
		/// </summary>
		public Vector4sb XWWW
		{
			get
			{
				return new Vector4sb(X, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, X).
		/// </summary>
		public Vector4sb YXXX
		{
			get
			{
				return new Vector4sb(Y, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Y).
		/// </summary>
		public Vector4sb YXXY
		{
			get
			{
				return new Vector4sb(Y, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Z).
		/// </summary>
		public Vector4sb YXXZ
		{
			get
			{
				return new Vector4sb(Y, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, W).
		/// </summary>
		public Vector4sb YXXW
		{
			get
			{
				return new Vector4sb(Y, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, X).
		/// </summary>
		public Vector4sb YXYX
		{
			get
			{
				return new Vector4sb(Y, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Y).
		/// </summary>
		public Vector4sb YXYY
		{
			get
			{
				return new Vector4sb(Y, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Z).
		/// </summary>
		public Vector4sb YXYZ
		{
			get
			{
				return new Vector4sb(Y, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, W).
		/// </summary>
		public Vector4sb YXYW
		{
			get
			{
				return new Vector4sb(Y, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, X).
		/// </summary>
		public Vector4sb YXZX
		{
			get
			{
				return new Vector4sb(Y, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Y).
		/// </summary>
		public Vector4sb YXZY
		{
			get
			{
				return new Vector4sb(Y, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Z).
		/// </summary>
		public Vector4sb YXZZ
		{
			get
			{
				return new Vector4sb(Y, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, W).
		/// </summary>
		public Vector4sb YXZW
		{
			get
			{
				return new Vector4sb(Y, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, X).
		/// </summary>
		public Vector4sb YXWX
		{
			get
			{
				return new Vector4sb(Y, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Y).
		/// </summary>
		public Vector4sb YXWY
		{
			get
			{
				return new Vector4sb(Y, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Z).
		/// </summary>
		public Vector4sb YXWZ
		{
			get
			{
				return new Vector4sb(Y, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, W).
		/// </summary>
		public Vector4sb YXWW
		{
			get
			{
				return new Vector4sb(Y, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, X).
		/// </summary>
		public Vector4sb YYXX
		{
			get
			{
				return new Vector4sb(Y, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Y).
		/// </summary>
		public Vector4sb YYXY
		{
			get
			{
				return new Vector4sb(Y, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Z).
		/// </summary>
		public Vector4sb YYXZ
		{
			get
			{
				return new Vector4sb(Y, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, W).
		/// </summary>
		public Vector4sb YYXW
		{
			get
			{
				return new Vector4sb(Y, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, X).
		/// </summary>
		public Vector4sb YYYX
		{
			get
			{
				return new Vector4sb(Y, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Y).
		/// </summary>
		public Vector4sb YYYY
		{
			get
			{
				return new Vector4sb(Y, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Z).
		/// </summary>
		public Vector4sb YYYZ
		{
			get
			{
				return new Vector4sb(Y, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, W).
		/// </summary>
		public Vector4sb YYYW
		{
			get
			{
				return new Vector4sb(Y, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, X).
		/// </summary>
		public Vector4sb YYZX
		{
			get
			{
				return new Vector4sb(Y, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Y).
		/// </summary>
		public Vector4sb YYZY
		{
			get
			{
				return new Vector4sb(Y, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Z).
		/// </summary>
		public Vector4sb YYZZ
		{
			get
			{
				return new Vector4sb(Y, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, W).
		/// </summary>
		public Vector4sb YYZW
		{
			get
			{
				return new Vector4sb(Y, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, X).
		/// </summary>
		public Vector4sb YYWX
		{
			get
			{
				return new Vector4sb(Y, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Y).
		/// </summary>
		public Vector4sb YYWY
		{
			get
			{
				return new Vector4sb(Y, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Z).
		/// </summary>
		public Vector4sb YYWZ
		{
			get
			{
				return new Vector4sb(Y, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, W).
		/// </summary>
		public Vector4sb YYWW
		{
			get
			{
				return new Vector4sb(Y, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, X).
		/// </summary>
		public Vector4sb YZXX
		{
			get
			{
				return new Vector4sb(Y, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Y).
		/// </summary>
		public Vector4sb YZXY
		{
			get
			{
				return new Vector4sb(Y, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Z).
		/// </summary>
		public Vector4sb YZXZ
		{
			get
			{
				return new Vector4sb(Y, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, W).
		/// </summary>
		public Vector4sb YZXW
		{
			get
			{
				return new Vector4sb(Y, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, X).
		/// </summary>
		public Vector4sb YZYX
		{
			get
			{
				return new Vector4sb(Y, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Y).
		/// </summary>
		public Vector4sb YZYY
		{
			get
			{
				return new Vector4sb(Y, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Z).
		/// </summary>
		public Vector4sb YZYZ
		{
			get
			{
				return new Vector4sb(Y, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, W).
		/// </summary>
		public Vector4sb YZYW
		{
			get
			{
				return new Vector4sb(Y, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, X).
		/// </summary>
		public Vector4sb YZZX
		{
			get
			{
				return new Vector4sb(Y, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Y).
		/// </summary>
		public Vector4sb YZZY
		{
			get
			{
				return new Vector4sb(Y, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Z).
		/// </summary>
		public Vector4sb YZZZ
		{
			get
			{
				return new Vector4sb(Y, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, W).
		/// </summary>
		public Vector4sb YZZW
		{
			get
			{
				return new Vector4sb(Y, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, X).
		/// </summary>
		public Vector4sb YZWX
		{
			get
			{
				return new Vector4sb(Y, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Y).
		/// </summary>
		public Vector4sb YZWY
		{
			get
			{
				return new Vector4sb(Y, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Z).
		/// </summary>
		public Vector4sb YZWZ
		{
			get
			{
				return new Vector4sb(Y, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, W).
		/// </summary>
		public Vector4sb YZWW
		{
			get
			{
				return new Vector4sb(Y, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, X).
		/// </summary>
		public Vector4sb YWXX
		{
			get
			{
				return new Vector4sb(Y, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Y).
		/// </summary>
		public Vector4sb YWXY
		{
			get
			{
				return new Vector4sb(Y, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Z).
		/// </summary>
		public Vector4sb YWXZ
		{
			get
			{
				return new Vector4sb(Y, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, W).
		/// </summary>
		public Vector4sb YWXW
		{
			get
			{
				return new Vector4sb(Y, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, X).
		/// </summary>
		public Vector4sb YWYX
		{
			get
			{
				return new Vector4sb(Y, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Y).
		/// </summary>
		public Vector4sb YWYY
		{
			get
			{
				return new Vector4sb(Y, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Z).
		/// </summary>
		public Vector4sb YWYZ
		{
			get
			{
				return new Vector4sb(Y, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, W).
		/// </summary>
		public Vector4sb YWYW
		{
			get
			{
				return new Vector4sb(Y, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, X).
		/// </summary>
		public Vector4sb YWZX
		{
			get
			{
				return new Vector4sb(Y, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Y).
		/// </summary>
		public Vector4sb YWZY
		{
			get
			{
				return new Vector4sb(Y, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Z).
		/// </summary>
		public Vector4sb YWZZ
		{
			get
			{
				return new Vector4sb(Y, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, W).
		/// </summary>
		public Vector4sb YWZW
		{
			get
			{
				return new Vector4sb(Y, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, X).
		/// </summary>
		public Vector4sb YWWX
		{
			get
			{
				return new Vector4sb(Y, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Y).
		/// </summary>
		public Vector4sb YWWY
		{
			get
			{
				return new Vector4sb(Y, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Z).
		/// </summary>
		public Vector4sb YWWZ
		{
			get
			{
				return new Vector4sb(Y, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, W).
		/// </summary>
		public Vector4sb YWWW
		{
			get
			{
				return new Vector4sb(Y, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, X).
		/// </summary>
		public Vector4sb ZXXX
		{
			get
			{
				return new Vector4sb(Z, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Y).
		/// </summary>
		public Vector4sb ZXXY
		{
			get
			{
				return new Vector4sb(Z, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Z).
		/// </summary>
		public Vector4sb ZXXZ
		{
			get
			{
				return new Vector4sb(Z, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, W).
		/// </summary>
		public Vector4sb ZXXW
		{
			get
			{
				return new Vector4sb(Z, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, X).
		/// </summary>
		public Vector4sb ZXYX
		{
			get
			{
				return new Vector4sb(Z, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Y).
		/// </summary>
		public Vector4sb ZXYY
		{
			get
			{
				return new Vector4sb(Z, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Z).
		/// </summary>
		public Vector4sb ZXYZ
		{
			get
			{
				return new Vector4sb(Z, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, W).
		/// </summary>
		public Vector4sb ZXYW
		{
			get
			{
				return new Vector4sb(Z, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, X).
		/// </summary>
		public Vector4sb ZXZX
		{
			get
			{
				return new Vector4sb(Z, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Y).
		/// </summary>
		public Vector4sb ZXZY
		{
			get
			{
				return new Vector4sb(Z, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Z).
		/// </summary>
		public Vector4sb ZXZZ
		{
			get
			{
				return new Vector4sb(Z, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, W).
		/// </summary>
		public Vector4sb ZXZW
		{
			get
			{
				return new Vector4sb(Z, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, X).
		/// </summary>
		public Vector4sb ZXWX
		{
			get
			{
				return new Vector4sb(Z, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Y).
		/// </summary>
		public Vector4sb ZXWY
		{
			get
			{
				return new Vector4sb(Z, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Z).
		/// </summary>
		public Vector4sb ZXWZ
		{
			get
			{
				return new Vector4sb(Z, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, W).
		/// </summary>
		public Vector4sb ZXWW
		{
			get
			{
				return new Vector4sb(Z, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, X).
		/// </summary>
		public Vector4sb ZYXX
		{
			get
			{
				return new Vector4sb(Z, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Y).
		/// </summary>
		public Vector4sb ZYXY
		{
			get
			{
				return new Vector4sb(Z, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Z).
		/// </summary>
		public Vector4sb ZYXZ
		{
			get
			{
				return new Vector4sb(Z, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, W).
		/// </summary>
		public Vector4sb ZYXW
		{
			get
			{
				return new Vector4sb(Z, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, X).
		/// </summary>
		public Vector4sb ZYYX
		{
			get
			{
				return new Vector4sb(Z, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Y).
		/// </summary>
		public Vector4sb ZYYY
		{
			get
			{
				return new Vector4sb(Z, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Z).
		/// </summary>
		public Vector4sb ZYYZ
		{
			get
			{
				return new Vector4sb(Z, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, W).
		/// </summary>
		public Vector4sb ZYYW
		{
			get
			{
				return new Vector4sb(Z, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, X).
		/// </summary>
		public Vector4sb ZYZX
		{
			get
			{
				return new Vector4sb(Z, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Y).
		/// </summary>
		public Vector4sb ZYZY
		{
			get
			{
				return new Vector4sb(Z, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Z).
		/// </summary>
		public Vector4sb ZYZZ
		{
			get
			{
				return new Vector4sb(Z, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, W).
		/// </summary>
		public Vector4sb ZYZW
		{
			get
			{
				return new Vector4sb(Z, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, X).
		/// </summary>
		public Vector4sb ZYWX
		{
			get
			{
				return new Vector4sb(Z, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Y).
		/// </summary>
		public Vector4sb ZYWY
		{
			get
			{
				return new Vector4sb(Z, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Z).
		/// </summary>
		public Vector4sb ZYWZ
		{
			get
			{
				return new Vector4sb(Z, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, W).
		/// </summary>
		public Vector4sb ZYWW
		{
			get
			{
				return new Vector4sb(Z, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, X).
		/// </summary>
		public Vector4sb ZZXX
		{
			get
			{
				return new Vector4sb(Z, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Y).
		/// </summary>
		public Vector4sb ZZXY
		{
			get
			{
				return new Vector4sb(Z, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Z).
		/// </summary>
		public Vector4sb ZZXZ
		{
			get
			{
				return new Vector4sb(Z, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, W).
		/// </summary>
		public Vector4sb ZZXW
		{
			get
			{
				return new Vector4sb(Z, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, X).
		/// </summary>
		public Vector4sb ZZYX
		{
			get
			{
				return new Vector4sb(Z, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Y).
		/// </summary>
		public Vector4sb ZZYY
		{
			get
			{
				return new Vector4sb(Z, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Z).
		/// </summary>
		public Vector4sb ZZYZ
		{
			get
			{
				return new Vector4sb(Z, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, W).
		/// </summary>
		public Vector4sb ZZYW
		{
			get
			{
				return new Vector4sb(Z, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, X).
		/// </summary>
		public Vector4sb ZZZX
		{
			get
			{
				return new Vector4sb(Z, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Y).
		/// </summary>
		public Vector4sb ZZZY
		{
			get
			{
				return new Vector4sb(Z, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Z).
		/// </summary>
		public Vector4sb ZZZZ
		{
			get
			{
				return new Vector4sb(Z, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, W).
		/// </summary>
		public Vector4sb ZZZW
		{
			get
			{
				return new Vector4sb(Z, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, X).
		/// </summary>
		public Vector4sb ZZWX
		{
			get
			{
				return new Vector4sb(Z, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Y).
		/// </summary>
		public Vector4sb ZZWY
		{
			get
			{
				return new Vector4sb(Z, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Z).
		/// </summary>
		public Vector4sb ZZWZ
		{
			get
			{
				return new Vector4sb(Z, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, W).
		/// </summary>
		public Vector4sb ZZWW
		{
			get
			{
				return new Vector4sb(Z, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, X).
		/// </summary>
		public Vector4sb ZWXX
		{
			get
			{
				return new Vector4sb(Z, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Y).
		/// </summary>
		public Vector4sb ZWXY
		{
			get
			{
				return new Vector4sb(Z, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Z).
		/// </summary>
		public Vector4sb ZWXZ
		{
			get
			{
				return new Vector4sb(Z, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, W).
		/// </summary>
		public Vector4sb ZWXW
		{
			get
			{
				return new Vector4sb(Z, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, X).
		/// </summary>
		public Vector4sb ZWYX
		{
			get
			{
				return new Vector4sb(Z, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Y).
		/// </summary>
		public Vector4sb ZWYY
		{
			get
			{
				return new Vector4sb(Z, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Z).
		/// </summary>
		public Vector4sb ZWYZ
		{
			get
			{
				return new Vector4sb(Z, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, W).
		/// </summary>
		public Vector4sb ZWYW
		{
			get
			{
				return new Vector4sb(Z, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, X).
		/// </summary>
		public Vector4sb ZWZX
		{
			get
			{
				return new Vector4sb(Z, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Y).
		/// </summary>
		public Vector4sb ZWZY
		{
			get
			{
				return new Vector4sb(Z, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Z).
		/// </summary>
		public Vector4sb ZWZZ
		{
			get
			{
				return new Vector4sb(Z, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, W).
		/// </summary>
		public Vector4sb ZWZW
		{
			get
			{
				return new Vector4sb(Z, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, X).
		/// </summary>
		public Vector4sb ZWWX
		{
			get
			{
				return new Vector4sb(Z, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Y).
		/// </summary>
		public Vector4sb ZWWY
		{
			get
			{
				return new Vector4sb(Z, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Z).
		/// </summary>
		public Vector4sb ZWWZ
		{
			get
			{
				return new Vector4sb(Z, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, W).
		/// </summary>
		public Vector4sb ZWWW
		{
			get
			{
				return new Vector4sb(Z, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, X).
		/// </summary>
		public Vector4sb WXXX
		{
			get
			{
				return new Vector4sb(W, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Y).
		/// </summary>
		public Vector4sb WXXY
		{
			get
			{
				return new Vector4sb(W, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Z).
		/// </summary>
		public Vector4sb WXXZ
		{
			get
			{
				return new Vector4sb(W, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, W).
		/// </summary>
		public Vector4sb WXXW
		{
			get
			{
				return new Vector4sb(W, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, X).
		/// </summary>
		public Vector4sb WXYX
		{
			get
			{
				return new Vector4sb(W, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Y).
		/// </summary>
		public Vector4sb WXYY
		{
			get
			{
				return new Vector4sb(W, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Z).
		/// </summary>
		public Vector4sb WXYZ
		{
			get
			{
				return new Vector4sb(W, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, W).
		/// </summary>
		public Vector4sb WXYW
		{
			get
			{
				return new Vector4sb(W, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, X).
		/// </summary>
		public Vector4sb WXZX
		{
			get
			{
				return new Vector4sb(W, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Y).
		/// </summary>
		public Vector4sb WXZY
		{
			get
			{
				return new Vector4sb(W, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Z).
		/// </summary>
		public Vector4sb WXZZ
		{
			get
			{
				return new Vector4sb(W, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, W).
		/// </summary>
		public Vector4sb WXZW
		{
			get
			{
				return new Vector4sb(W, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, X).
		/// </summary>
		public Vector4sb WXWX
		{
			get
			{
				return new Vector4sb(W, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Y).
		/// </summary>
		public Vector4sb WXWY
		{
			get
			{
				return new Vector4sb(W, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Z).
		/// </summary>
		public Vector4sb WXWZ
		{
			get
			{
				return new Vector4sb(W, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, W).
		/// </summary>
		public Vector4sb WXWW
		{
			get
			{
				return new Vector4sb(W, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, X).
		/// </summary>
		public Vector4sb WYXX
		{
			get
			{
				return new Vector4sb(W, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Y).
		/// </summary>
		public Vector4sb WYXY
		{
			get
			{
				return new Vector4sb(W, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Z).
		/// </summary>
		public Vector4sb WYXZ
		{
			get
			{
				return new Vector4sb(W, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, W).
		/// </summary>
		public Vector4sb WYXW
		{
			get
			{
				return new Vector4sb(W, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, X).
		/// </summary>
		public Vector4sb WYYX
		{
			get
			{
				return new Vector4sb(W, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Y).
		/// </summary>
		public Vector4sb WYYY
		{
			get
			{
				return new Vector4sb(W, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Z).
		/// </summary>
		public Vector4sb WYYZ
		{
			get
			{
				return new Vector4sb(W, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, W).
		/// </summary>
		public Vector4sb WYYW
		{
			get
			{
				return new Vector4sb(W, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, X).
		/// </summary>
		public Vector4sb WYZX
		{
			get
			{
				return new Vector4sb(W, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Y).
		/// </summary>
		public Vector4sb WYZY
		{
			get
			{
				return new Vector4sb(W, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Z).
		/// </summary>
		public Vector4sb WYZZ
		{
			get
			{
				return new Vector4sb(W, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, W).
		/// </summary>
		public Vector4sb WYZW
		{
			get
			{
				return new Vector4sb(W, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, X).
		/// </summary>
		public Vector4sb WYWX
		{
			get
			{
				return new Vector4sb(W, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Y).
		/// </summary>
		public Vector4sb WYWY
		{
			get
			{
				return new Vector4sb(W, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Z).
		/// </summary>
		public Vector4sb WYWZ
		{
			get
			{
				return new Vector4sb(W, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, W).
		/// </summary>
		public Vector4sb WYWW
		{
			get
			{
				return new Vector4sb(W, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, X).
		/// </summary>
		public Vector4sb WZXX
		{
			get
			{
				return new Vector4sb(W, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Y).
		/// </summary>
		public Vector4sb WZXY
		{
			get
			{
				return new Vector4sb(W, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Z).
		/// </summary>
		public Vector4sb WZXZ
		{
			get
			{
				return new Vector4sb(W, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, W).
		/// </summary>
		public Vector4sb WZXW
		{
			get
			{
				return new Vector4sb(W, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, X).
		/// </summary>
		public Vector4sb WZYX
		{
			get
			{
				return new Vector4sb(W, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Y).
		/// </summary>
		public Vector4sb WZYY
		{
			get
			{
				return new Vector4sb(W, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Z).
		/// </summary>
		public Vector4sb WZYZ
		{
			get
			{
				return new Vector4sb(W, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, W).
		/// </summary>
		public Vector4sb WZYW
		{
			get
			{
				return new Vector4sb(W, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, X).
		/// </summary>
		public Vector4sb WZZX
		{
			get
			{
				return new Vector4sb(W, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Y).
		/// </summary>
		public Vector4sb WZZY
		{
			get
			{
				return new Vector4sb(W, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Z).
		/// </summary>
		public Vector4sb WZZZ
		{
			get
			{
				return new Vector4sb(W, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, W).
		/// </summary>
		public Vector4sb WZZW
		{
			get
			{
				return new Vector4sb(W, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, X).
		/// </summary>
		public Vector4sb WZWX
		{
			get
			{
				return new Vector4sb(W, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Y).
		/// </summary>
		public Vector4sb WZWY
		{
			get
			{
				return new Vector4sb(W, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Z).
		/// </summary>
		public Vector4sb WZWZ
		{
			get
			{
				return new Vector4sb(W, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, W).
		/// </summary>
		public Vector4sb WZWW
		{
			get
			{
				return new Vector4sb(W, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, X).
		/// </summary>
		public Vector4sb WWXX
		{
			get
			{
				return new Vector4sb(W, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Y).
		/// </summary>
		public Vector4sb WWXY
		{
			get
			{
				return new Vector4sb(W, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Z).
		/// </summary>
		public Vector4sb WWXZ
		{
			get
			{
				return new Vector4sb(W, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, W).
		/// </summary>
		public Vector4sb WWXW
		{
			get
			{
				return new Vector4sb(W, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, X).
		/// </summary>
		public Vector4sb WWYX
		{
			get
			{
				return new Vector4sb(W, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Y).
		/// </summary>
		public Vector4sb WWYY
		{
			get
			{
				return new Vector4sb(W, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Z).
		/// </summary>
		public Vector4sb WWYZ
		{
			get
			{
				return new Vector4sb(W, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, W).
		/// </summary>
		public Vector4sb WWYW
		{
			get
			{
				return new Vector4sb(W, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, X).
		/// </summary>
		public Vector4sb WWZX
		{
			get
			{
				return new Vector4sb(W, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Y).
		/// </summary>
		public Vector4sb WWZY
		{
			get
			{
				return new Vector4sb(W, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Z).
		/// </summary>
		public Vector4sb WWZZ
		{
			get
			{
				return new Vector4sb(W, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, W).
		/// </summary>
		public Vector4sb WWZW
		{
			get
			{
				return new Vector4sb(W, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, X).
		/// </summary>
		public Vector4sb WWWX
		{
			get
			{
				return new Vector4sb(W, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Y).
		/// </summary>
		public Vector4sb WWWY
		{
			get
			{
				return new Vector4sb(W, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Z).
		/// </summary>
		public Vector4sb WWWZ
		{
			get
			{
				return new Vector4sb(W, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, W).
		/// </summary>
		public Vector4sb WWWW
		{
			get
			{
				return new Vector4sb(W, W, W, W);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4sb"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector4sb(sbyte value)
		{
			X = value;
			Y = value;
			Z = value;
			W = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4sb"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X and Y components</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4sb(Vector2sb value, sbyte z, sbyte w)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4sb"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X, Y and Z components</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4sb(Vector3sb value, sbyte w)
		{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4sb"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4sb(sbyte x, sbyte y, sbyte z, sbyte w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4sb"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector4sb(sbyte[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
			W = array[3];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4sb"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector4sb(sbyte[] array, int offset)
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
		public static Vector4i operator +(Vector4sb value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4i operator -(Vector4sb value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4i operator +(Vector4sb left, Vector4sb right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4i operator -(Vector4sb left, Vector4sb right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4i operator *(Vector4sb left, int right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4i operator *(int left, Vector4sb right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4i operator /(Vector4sb left, int right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector4d value to a Vector4sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4sb.</param>
		/// <returns>A Vector4sb that has all components equal to value.</returns>
		public static explicit operator Vector4sb(Vector4d value)
		{
			return new Vector4sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z, (sbyte)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4f value to a Vector4sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4sb.</param>
		/// <returns>A Vector4sb that has all components equal to value.</returns>
		public static explicit operator Vector4sb(Vector4f value)
		{
			return new Vector4sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z, (sbyte)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4h value to a Vector4sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4sb.</param>
		/// <returns>A Vector4sb that has all components equal to value.</returns>
		public static explicit operator Vector4sb(Vector4h value)
		{
			return new Vector4sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z, (sbyte)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4ul value to a Vector4sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4sb.</param>
		/// <returns>A Vector4sb that has all components equal to value.</returns>
		public static explicit operator Vector4sb(Vector4ul value)
		{
			return new Vector4sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z, (sbyte)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4l value to a Vector4sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4sb.</param>
		/// <returns>A Vector4sb that has all components equal to value.</returns>
		public static explicit operator Vector4sb(Vector4l value)
		{
			return new Vector4sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z, (sbyte)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4ui value to a Vector4sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4sb.</param>
		/// <returns>A Vector4sb that has all components equal to value.</returns>
		public static explicit operator Vector4sb(Vector4ui value)
		{
			return new Vector4sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z, (sbyte)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4i value to a Vector4sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4sb.</param>
		/// <returns>A Vector4sb that has all components equal to value.</returns>
		public static explicit operator Vector4sb(Vector4i value)
		{
			return new Vector4sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z, (sbyte)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4us value to a Vector4sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4sb.</param>
		/// <returns>A Vector4sb that has all components equal to value.</returns>
		public static explicit operator Vector4sb(Vector4us value)
		{
			return new Vector4sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z, (sbyte)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4s value to a Vector4sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4sb.</param>
		/// <returns>A Vector4sb that has all components equal to value.</returns>
		public static explicit operator Vector4sb(Vector4s value)
		{
			return new Vector4sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z, (sbyte)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4b value to a Vector4sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4sb.</param>
		/// <returns>A Vector4sb that has all components equal to value.</returns>
		public static explicit operator Vector4sb(Vector4b value)
		{
			return new Vector4sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z, (sbyte)value.W);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector4sb"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector4sb"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector4sb"/> object, and its value
		/// is equal to the current <see cref="Vector4sb"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector4sb) { return Equals((Vector4sb)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector4sb other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector4sb left, Vector4sb right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z & left.W == right.W;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector4sb left, Vector4sb right)
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
		/// Writes the given <see cref="Vector4sb"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector4sb vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
			writer.Write(vector.Z);
			writer.Write(vector.W);
		}
		/// <summary>
		/// Reads a <see cref="Vector4sb"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector4sb ReadVector4sb(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector4sb(reader.ReadSByte(), reader.ReadSByte(), reader.ReadSByte(), reader.ReadSByte());
		}
		#endregion
		#region Pack
		public static uint Pack(int xBits, int yBits, int zBits, int wBits, Vector4sb vector)
		{
			Contract.Requires(0 <= xBits && xBits <= 8, "xBits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 8, "yBits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 8, "zBits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= wBits && wBits <= 8, "wBits must be between 0 and 8 inclusive.");
			Contract.Requires(xBits + yBits + zBits + wBits <= 32);
			ulong x = (ulong)(vector.X) >> (32 - xBits);
			ulong y = (ulong)(vector.Y) >> (32 - yBits);
			y <<= xBits;
			ulong z = (ulong)(vector.Z) >> (32 - zBits);
			z <<= xBits + yBits;
			ulong w = (ulong)(vector.W) >> (32 - wBits);
			w <<= xBits + yBits + zBits;
			return (uint)(x | y | z | w);
		}
		public static uint Pack(Vector4sb vector)
		{
			ulong x = (ulong)(vector.X) << 0;
			ulong y = (ulong)(vector.Y) << 8;
			ulong z = (ulong)(vector.Z) << 16;
			ulong w = (ulong)(vector.W) << 24;
			return (uint)(x | y | z | w);
		}
		public static Vector4sb Unpack(int xBits, int yBits, int zBits, int wBits, sbyte bits)
		{
			Contract.Requires(0 <= xBits && xBits <= 8, "xBits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 8, "yBits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 8, "zBits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= wBits && wBits <= 8, "wBits must be between 0 and 8 inclusive.");
			Contract.Requires(xBits + yBits + zBits + wBits <= 32);
			ulong x = (ulong)(bits);
			x &= ((1UL << xBits) - 1);
			ulong y = (ulong)(bits) >> (xBits);
			y &= ((1UL << yBits) - 1);
			ulong z = (ulong)(bits) >> (xBits + yBits);
			z &= ((1UL << zBits) - 1);
			ulong w = (ulong)(bits) >> (xBits + yBits + zBits);
			w &= ((1UL << wBits) - 1);
			return new Vector4sb((sbyte)x, (sbyte)y, (sbyte)z, (sbyte)w);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4i Negative(Vector4sb value)
		{
			return new Vector4i(-value.X, -value.Y, -value.Z, -value.W);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4i Add(Vector4sb left, Vector4sb right)
		{
			return new Vector4i(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4i Subtract(Vector4sb left, Vector4sb right)
		{
			return new Vector4i(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4i Multiply(Vector4sb vector, int scalar)
		{
			return new Vector4i(vector.X * scalar, vector.Y * scalar, vector.Z * scalar, vector.W * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4i Divide(Vector4sb vector, int scalar)
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
		public static bool Equals(Vector4sb left, Vector4sb right)
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
		public static int Dot(Vector4sb left, Vector4sb right)
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
		public static bool All(Vector4sb value)
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
		public static bool All(Vector4sb value, Predicate<sbyte> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z) && predicate(value.W);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		[CLSCompliant(false)]
		public static bool Any(Vector4sb value)
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
		public static bool Any(Vector4sb value, Predicate<sbyte> predicate)
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
		public static int AbsoluteSquared(Vector4sb value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		[CLSCompliant(false)]
		public static float Absolute(Vector4sb value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		[CLSCompliant(false)]
		public static Vector4f Normalize(Vector4sb value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Vector4sb.Zero;
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
		public static Vector4d Map(Vector4sb value, Func<sbyte, double> mapping)
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
		public static Vector4f Map(Vector4sb value, Func<sbyte, float> mapping)
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
		public static Vector4h Map(Vector4sb value, Func<sbyte, Half> mapping)
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
		public static Vector4ul Map(Vector4sb value, Func<sbyte, ulong> mapping)
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
		public static Vector4l Map(Vector4sb value, Func<sbyte, long> mapping)
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
		public static Vector4ui Map(Vector4sb value, Func<sbyte, uint> mapping)
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
		public static Vector4i Map(Vector4sb value, Func<sbyte, int> mapping)
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
		public static Vector4us Map(Vector4sb value, Func<sbyte, ushort> mapping)
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
		public static Vector4s Map(Vector4sb value, Func<sbyte, short> mapping)
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
		public static Vector4b Map(Vector4sb value, Func<sbyte, byte> mapping)
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
		public static Vector4sb Map(Vector4sb value, Func<sbyte, sbyte> mapping)
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
		public static Vector4i Modulate(Vector4sb left, Vector4sb right)
		{
			return new Vector4i(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		[CLSCompliant(false)]
		public static Vector4sb Abs(Vector4sb value)
		{
			return new Vector4sb(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z), Functions.Abs(value.W));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector4sb Min(Vector4sb value1, Vector4sb value2)
		{
			return new Vector4sb(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z), Functions.Min(value1.W, value2.W));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector4sb Max(Vector4sb value1, Vector4sb value2)
		{
			return new Vector4sb(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z), Functions.Max(value1.W, value2.W));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		[CLSCompliant(false)]
		public static Vector4sb Clamp(Vector4sb value, Vector4sb min, Vector4sb max)
		{
			return new Vector4sb(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z), Functions.Clamp(value.W, min.W, max.W));
		}
		#endregion
		#region Coordinate spaces
		#endregion
	}
}
