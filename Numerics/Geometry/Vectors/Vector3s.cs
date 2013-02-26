using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a three component vector of shorts, of the form (X, Y, Z).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3s: IEquatable<Vector3s>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector3s"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector3s Zero = new Vector3s(0);
		/// <summary>
		/// Returns a new <see cref="Vector3s"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector3s One = new Vector3s(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector3s"/> (1, 0, 0).
		/// </summary>
		public static readonly Vector3s UnitX = new Vector3s(1, 0, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector3s"/> (0, 1, 0).
		/// </summary>
		public static readonly Vector3s UnitY = new Vector3s(0, 1, 0);
		/// <summary>
		/// Returns the Z unit <see cref="Vector3s"/> (0, 0, 1).
		/// </summary>
		public static readonly Vector3s UnitZ = new Vector3s(0, 0, 1);
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
					default:
						throw new IndexOutOfRangeException("Indices for Vector3s run from 0 to 2, inclusive.");
				}
			}
		}
		public short[] ToArray()
		{
			return new short[]
			{
				X, Y, Z
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
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3s"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector3s(short value)
		{
			X = value;
			Y = value;
			Z = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3s"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X and Y components</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		public Vector3s(Vector2s value, short z)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3s"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		public Vector3s(short x, short y, short z)
		{
			X = x;
			Y = y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3s"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector3s(short[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3s"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector3s(short[] array, int offset)
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
		public static Vector3i operator +(Vector3s value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector3i operator -(Vector3s value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector3i operator +(Vector3s left, Vector3s right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3i operator -(Vector3s left, Vector3s right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector3i operator *(Vector3s left, int right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector3i operator *(int left, Vector3s right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector3i operator /(Vector3s left, int right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector3d value to a Vector3s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3s.</param>
		/// <returns>A Vector3s that has all components equal to value.</returns>
		public static explicit operator Vector3s(Vector3d value)
		{
			return new Vector3s((short)value.X, (short)value.Y, (short)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3f value to a Vector3s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3s.</param>
		/// <returns>A Vector3s that has all components equal to value.</returns>
		public static explicit operator Vector3s(Vector3f value)
		{
			return new Vector3s((short)value.X, (short)value.Y, (short)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3h value to a Vector3s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3s.</param>
		/// <returns>A Vector3s that has all components equal to value.</returns>
		public static explicit operator Vector3s(Vector3h value)
		{
			return new Vector3s((short)value.X, (short)value.Y, (short)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3ul value to a Vector3s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3s.</param>
		/// <returns>A Vector3s that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector3s(Vector3ul value)
		{
			return new Vector3s((short)value.X, (short)value.Y, (short)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3l value to a Vector3s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3s.</param>
		/// <returns>A Vector3s that has all components equal to value.</returns>
		public static explicit operator Vector3s(Vector3l value)
		{
			return new Vector3s((short)value.X, (short)value.Y, (short)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3ui value to a Vector3s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3s.</param>
		/// <returns>A Vector3s that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector3s(Vector3ui value)
		{
			return new Vector3s((short)value.X, (short)value.Y, (short)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3i value to a Vector3s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3s.</param>
		/// <returns>A Vector3s that has all components equal to value.</returns>
		public static explicit operator Vector3s(Vector3i value)
		{
			return new Vector3s((short)value.X, (short)value.Y, (short)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3us value to a Vector3s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3s.</param>
		/// <returns>A Vector3s that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector3s(Vector3us value)
		{
			return new Vector3s((short)value.X, (short)value.Y, (short)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector3b value to a Vector3s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3s.</param>
		/// <returns>A Vector3s that has all components equal to value.</returns>
		public static implicit operator Vector3s(Vector3b value)
		{
			return new Vector3s((short)value.X, (short)value.Y, (short)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector3sb value to a Vector3s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3s.</param>
		/// <returns>A Vector3s that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector3s(Vector3sb value)
		{
			return new Vector3s((short)value.X, (short)value.Y, (short)value.Z);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector3s"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector3s"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector3s"/> object, and its value
		/// is equal to the current <see cref="Vector3s"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector3s) { return Equals((Vector3s)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector3s other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector3s left, Vector3s right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector3s left, Vector3s right)
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
		/// Writes the given <see cref="Vector3s"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector3s vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
			writer.Write(vector.Z);
		}
		/// <summary>
		/// Reads a <see cref="Vector3s"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector3s ReadVector3s(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector3s(reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16());
		}
		#endregion
		#region Pack
		public static long Pack(int xBits, int yBits, int zBits, Vector3s vector)
		{
			Contract.Requires(0 <= xBits && xBits <= 16, "xBits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 16, "yBits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 16, "zBits must be between 0 and 16 inclusive.");
			Contract.Requires(xBits + yBits + zBits <= 64);
			ulong x = (ulong)(vector.X) >> (64 - xBits);
			ulong y = (ulong)(vector.Y) >> (64 - yBits);
			y <<= xBits;
			ulong z = (ulong)(vector.Z) >> (64 - zBits);
			z <<= xBits + yBits;
			return (long)(x | y | z);
		}
		public static long Pack(Vector3s vector)
		{
			ulong x = (ulong)(vector.X) << 0;
			ulong y = (ulong)(vector.Y) << 16;
			ulong z = (ulong)(vector.Z) << 32;
			return (long)(x | y | z);
		}
		public static Vector3s Unpack(int xBits, int yBits, int zBits, short bits)
		{
			Contract.Requires(0 <= xBits && xBits <= 16, "xBits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 16, "yBits must be between 0 and 16 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 16, "zBits must be between 0 and 16 inclusive.");
			Contract.Requires(xBits + yBits + zBits <= 64);
			ulong x = (ulong)(bits);
			x &= ((1UL << xBits) - 1);
			ulong y = (ulong)(bits) >> (xBits);
			y &= ((1UL << yBits) - 1);
			ulong z = (ulong)(bits) >> (xBits + yBits);
			z &= ((1UL << zBits) - 1);
			return new Vector3s((short)x, (short)y, (short)z);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector3i Negative(Vector3s value)
		{
			return new Vector3i(-value.X, -value.Y, -value.Z);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector3i Add(Vector3s left, Vector3s right)
		{
			return new Vector3i(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3i Subtract(Vector3s left, Vector3s right)
		{
			return new Vector3i(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector3i Multiply(Vector3s vector, int scalar)
		{
			return new Vector3i(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector3i Divide(Vector3s vector, int scalar)
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
		public static bool Equals(Vector3s left, Vector3s right)
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
		public static int Dot(Vector3s left, Vector3s right)
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
		public static bool All(Vector3s value)
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
		public static bool All(Vector3s value, Predicate<short> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Vector3s value)
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
		public static bool Any(Vector3s value, Predicate<short> predicate)
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
		public static int AbsoluteSquared(Vector3s value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		public static float Absolute(Vector3s value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		public static Vector3f Normalize(Vector3s value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Vector3s.Zero;
			}
			else
			{
				return (Vector3f)value / absolute;
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
		public static Vector3d Transform(Vector3s value, Func<short, double> transformer)
		{
			return new Vector3d(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3f Transform(Vector3s value, Func<short, float> transformer)
		{
			return new Vector3f(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3h Transform(Vector3s value, Func<short, Half> transformer)
		{
			return new Vector3h(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3ul Transform(Vector3s value, Func<short, ulong> transformer)
		{
			return new Vector3ul(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3l Transform(Vector3s value, Func<short, long> transformer)
		{
			return new Vector3l(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3ui Transform(Vector3s value, Func<short, uint> transformer)
		{
			return new Vector3ui(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3i Transform(Vector3s value, Func<short, int> transformer)
		{
			return new Vector3i(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3us Transform(Vector3s value, Func<short, ushort> transformer)
		{
			return new Vector3us(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3s Transform(Vector3s value, Func<short, short> transformer)
		{
			return new Vector3s(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3b Transform(Vector3s value, Func<short, byte> transformer)
		{
			return new Vector3b(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3sb Transform(Vector3s value, Func<short, sbyte> transformer)
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
		public static Vector3i Modulate(Vector3s left, Vector3s right)
		{
			return new Vector3i(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Vector3s Abs(Vector3s value)
		{
			return new Vector3s(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Vector3s Min(Vector3s value1, Vector3s value2)
		{
			return new Vector3s(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Vector3s Max(Vector3s value1, Vector3s value2)
		{
			return new Vector3s(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		public static Vector3s Clamp(Vector3s value, Vector3s min, Vector3s max)
		{
			return new Vector3s(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z));
		}
		#endregion
		#region Coordinate spaces
		/// <summary>
		/// Transforms a vector in cartesian coordinates to spherical coordinates.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <returns>The spherical coordinates of value.</returns>
		public static SphericalCoordinate CartesianToSpherical (Vector3s value)
		{
			double r = Functions.Sqrt(value.X * value.X + value.Y * value.Y + value.Z * value.Z);
			double theta = Functions.Atan2(value.Y, value.X);
			if (theta < 0)
				theta += 2 * Constants.PI;
			return new SphericalCoordinate(
			     (double)Functions.Acos(value.Z / r),
			     theta,
			     r);
		}
		#endregion
	}
}
