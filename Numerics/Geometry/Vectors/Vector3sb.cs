using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a three component vector of sbytes, of the form (X, Y, Z).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	[CLSCompliant(false)]
	public struct Vector3sb: IEquatable<Vector3sb>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector3sb"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector3sb Zero = new Vector3sb(0);
		/// <summary>
		/// Returns a new <see cref="Vector3sb"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector3sb One = new Vector3sb(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector3sb"/> (1, 0, 0).
		/// </summary>
		public static readonly Vector3sb UnitX = new Vector3sb(1, 0, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector3sb"/> (0, 1, 0).
		/// </summary>
		public static readonly Vector3sb UnitY = new Vector3sb(0, 1, 0);
		/// <summary>
		/// Returns the Z unit <see cref="Vector3sb"/> (0, 0, 1).
		/// </summary>
		public static readonly Vector3sb UnitZ = new Vector3sb(0, 0, 1);
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
					default:
						throw new IndexOutOfRangeException("Indices for Vector3sb run from 0 to 2, inclusive.");
				}
			}
		}
		public sbyte[] ToArray()
		{
			return new sbyte[]
			{
				X, Y, Z
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
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3sb"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector3sb(sbyte value)
		{
			X = value;
			Y = value;
			Z = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3sb"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X and Y components</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		public Vector3sb(Vector2sb value, sbyte z)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3sb"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		public Vector3sb(sbyte x, sbyte y, sbyte z)
		{
			X = x;
			Y = y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3sb"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector3sb(sbyte[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3sb"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector3sb(sbyte[] array, int offset)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[offset + 0];
			Y = array[offset + 1];
			Z = array[offset + 2];
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The identity of value.</returns>
		public static Vector3i operator +(Vector3sb value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector3i operator -(Vector3sb value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector3i operator +(Vector3sb left, Vector3sb right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3i operator -(Vector3sb left, Vector3sb right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector3i operator *(Vector3sb left, int right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector3i operator *(int left, Vector3sb right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector3i operator /(Vector3sb left, int right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector3d value to a Vector3sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3sb.</param>
		/// <returns>A Vector3sb that has all components equal to value.</returns>
		public static explicit operator Vector3sb(Vector3d value)
		{
			return new Vector3sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3f value to a Vector3sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3sb.</param>
		/// <returns>A Vector3sb that has all components equal to value.</returns>
		public static explicit operator Vector3sb(Vector3f value)
		{
			return new Vector3sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3h value to a Vector3sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3sb.</param>
		/// <returns>A Vector3sb that has all components equal to value.</returns>
		public static explicit operator Vector3sb(Vector3h value)
		{
			return new Vector3sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3ul value to a Vector3sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3sb.</param>
		/// <returns>A Vector3sb that has all components equal to value.</returns>
		public static explicit operator Vector3sb(Vector3ul value)
		{
			return new Vector3sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3l value to a Vector3sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3sb.</param>
		/// <returns>A Vector3sb that has all components equal to value.</returns>
		public static explicit operator Vector3sb(Vector3l value)
		{
			return new Vector3sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3ui value to a Vector3sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3sb.</param>
		/// <returns>A Vector3sb that has all components equal to value.</returns>
		public static explicit operator Vector3sb(Vector3ui value)
		{
			return new Vector3sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3i value to a Vector3sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3sb.</param>
		/// <returns>A Vector3sb that has all components equal to value.</returns>
		public static explicit operator Vector3sb(Vector3i value)
		{
			return new Vector3sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3us value to a Vector3sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3sb.</param>
		/// <returns>A Vector3sb that has all components equal to value.</returns>
		public static explicit operator Vector3sb(Vector3us value)
		{
			return new Vector3sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3s value to a Vector3sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3sb.</param>
		/// <returns>A Vector3sb that has all components equal to value.</returns>
		public static explicit operator Vector3sb(Vector3s value)
		{
			return new Vector3sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3b value to a Vector3sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3sb.</param>
		/// <returns>A Vector3sb that has all components equal to value.</returns>
		public static explicit operator Vector3sb(Vector3b value)
		{
			return new Vector3sb((sbyte)value.X, (sbyte)value.Y, (sbyte)value.Z);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector3sb"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Vector3sb"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector3sb"/> object, and its value
		/// is equal to the current <see cref="Vector3sb"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector3sb) { return Equals((Vector3sb)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector3sb other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector3sb left, Vector3sb right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector3sb left, Vector3sb right)
		{
			return left.X != right.X | left.Y != right.Y | left.Z != right.Z;
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
			return String.Format("({0}, {1}, {2})", X.ToString(format, provider), Y.ToString(format, provider), Z.ToString(format, provider));
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
		/// Writes the given <see cref="Vector3sb"/> to a Ibasa.IO.BinaryWriter.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector3sb vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
			writer.Write(vector.Z);
		}
		/// <summary>
		/// Reads a <see cref="Vector3sb"/> to a Ibasa.IO.BinaryReader.
		/// </summary>
		public static Vector3sb ReadVector3sb(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector3sb(reader.ReadSByte(), reader.ReadSByte(), reader.ReadSByte());
		}
		#endregion
		#region Pack
		public static uint Pack(int xBits, int yBits, int zBits, Vector3sb vector)
		{
			Contract.Requires(0 <= xBits && xBits <= 8, "xBits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 8, "yBits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 8, "zBits must be between 0 and 8 inclusive.");
			Contract.Requires(xBits + yBits + zBits <= 32);
			ulong x = (ulong)(vector.X) >> (32 - xBits);
			ulong y = (ulong)(vector.Y) >> (32 - yBits);
			y <<= xBits;
			ulong z = (ulong)(vector.Z) >> (32 - zBits);
			z <<= xBits + yBits;
			return (uint)(x | y | z);
		}
		public static uint Pack(Vector3sb vector)
		{
			ulong x = (ulong)(vector.X) << 0;
			ulong y = (ulong)(vector.Y) << 8;
			ulong z = (ulong)(vector.Z) << 16;
			return (uint)(x | y | z);
		}
		public static Vector3sb Unpack(int xBits, int yBits, int zBits, sbyte bits)
		{
			Contract.Requires(0 <= xBits && xBits <= 8, "xBits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 8, "yBits must be between 0 and 8 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 8, "zBits must be between 0 and 8 inclusive.");
			Contract.Requires(xBits + yBits + zBits <= 32);
			ulong x = (ulong)(bits);
			x &= ((1UL << xBits) - 1);
			ulong y = (ulong)(bits) >> (xBits);
			y &= ((1UL << yBits) - 1);
			ulong z = (ulong)(bits) >> (xBits + yBits);
			z &= ((1UL << zBits) - 1);
			return new Vector3sb((sbyte)x, (sbyte)y, (sbyte)z);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector3i Negative(Vector3sb value)
		{
			return new Vector3i(-value.X, -value.Y, -value.Z);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector3i Add(Vector3sb left, Vector3sb right)
		{
			return new Vector3i(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3i Subtract(Vector3sb left, Vector3sb right)
		{
			return new Vector3i(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector3i Multiply(Vector3sb vector, int scalar)
		{
			return new Vector3i(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector3i Divide(Vector3sb vector, int scalar)
		{
			return new Vector3i(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Vector3sb left, Vector3sb right)
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
		public static int Dot(Vector3sb left, Vector3sb right)
		{
			return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all components of a vector are non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if all components are non-zero; false otherwise.</returns>
		[CLSCompliant(false)]
		public static bool All(Vector3sb value)
		{
			return value.X != 0 && value.Y != 0 && value.Z != 0;
		}
		/// <summary>
		/// Determines whether all components of a vector satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if every component of the vector passes the test in the specified
		/// predicate; otherwise, false.</returns>
		[CLSCompliant(false)]
		public static bool All(Vector3sb value, Predicate<sbyte> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		[CLSCompliant(false)]
		public static bool Any(Vector3sb value)
		{
			return value.X != 0 || value.Y != 0 || value.Z != 0;
		}
		/// <summary>
		/// Determines whether any components of a vector satisfy a condition.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if any component of the vector passes the test in the specified
		/// predicate; otherwise, false.</returns>
		[CLSCompliant(false)]
		public static bool Any(Vector3sb value, Predicate<sbyte> predicate)
		{
			return predicate(value.X) || predicate(value.Y) || predicate(value.Z);
		}
		#endregion
		#region Properties
		/// <summary>
		/// Computes the absolute squared value of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute squared value of value.</returns> 
		[CLSCompliant(false)]
		public static int AbsoluteSquared(Vector3sb value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		[CLSCompliant(false)]
		public static float Absolute(Vector3sb value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		[CLSCompliant(false)]
		public static Vector3f Normalize(Vector3sb value)
		{
			return (Vector3f)value / Absolute(value);
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
		[CLSCompliant(false)]
		public static Vector3d Transform(Vector3sb value, Func<sbyte, double> transformer)
		{
			return new Vector3d(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3f Transform(Vector3sb value, Func<sbyte, float> transformer)
		{
			return new Vector3f(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3h Transform(Vector3sb value, Func<sbyte, Half> transformer)
		{
			return new Vector3h(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3ul Transform(Vector3sb value, Func<sbyte, ulong> transformer)
		{
			return new Vector3ul(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3l Transform(Vector3sb value, Func<sbyte, long> transformer)
		{
			return new Vector3l(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3ui Transform(Vector3sb value, Func<sbyte, uint> transformer)
		{
			return new Vector3ui(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3i Transform(Vector3sb value, Func<sbyte, int> transformer)
		{
			return new Vector3i(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3us Transform(Vector3sb value, Func<sbyte, ushort> transformer)
		{
			return new Vector3us(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3s Transform(Vector3sb value, Func<sbyte, short> transformer)
		{
			return new Vector3s(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3b Transform(Vector3sb value, Func<sbyte, byte> transformer)
		{
			return new Vector3b(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3sb Transform(Vector3sb value, Func<sbyte, sbyte> transformer)
		{
			return new Vector3sb(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		#endregion
		/// <summary>
		/// Multiplys the components of two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first vector to modulate.</param>
		/// <param name="right">The second vector to modulate.</param>
		/// <returns>The result of multiplying each component of left by the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector3i Modulate(Vector3sb left, Vector3sb right)
		{
			return new Vector3i(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		[CLSCompliant(false)]
		public static Vector3sb Abs(Vector3sb value)
		{
			return new Vector3sb(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector3sb Min(Vector3sb value1, Vector3sb value2)
		{
			return new Vector3sb(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector3sb Max(Vector3sb value1, Vector3sb value2)
		{
			return new Vector3sb(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		[CLSCompliant(false)]
		public static Vector3sb Clamp(Vector3sb value, Vector3sb min, Vector3sb max)
		{
			return new Vector3sb(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z));
		}
		#endregion
	}
}
