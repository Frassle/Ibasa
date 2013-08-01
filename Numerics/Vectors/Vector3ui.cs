using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Numerics
{
	/// <summary>
	/// Represents a three component vector of uints, of the form (X, Y, Z).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	[CLSCompliant(false)]
	public struct Vector3ui: IEquatable<Vector3ui>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector3ui"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector3ui Zero = new Vector3ui(0);
		/// <summary>
		/// Returns a new <see cref="Vector3ui"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector3ui One = new Vector3ui(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector3ui"/> (1, 0, 0).
		/// </summary>
		public static readonly Vector3ui UnitX = new Vector3ui(1, 0, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector3ui"/> (0, 1, 0).
		/// </summary>
		public static readonly Vector3ui UnitY = new Vector3ui(0, 1, 0);
		/// <summary>
		/// Returns the Z unit <see cref="Vector3ui"/> (0, 0, 1).
		/// </summary>
		public static readonly Vector3ui UnitZ = new Vector3ui(0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the vector.
		/// </summary>
		public readonly uint X;
		/// <summary>
		/// The Y component of the vector.
		/// </summary>
		public readonly uint Y;
		/// <summary>
		/// The Z component of the vector.
		/// </summary>
		public readonly uint Z;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public uint this[int index]
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
						throw new IndexOutOfRangeException("Indices for Vector3ui run from 0 to 2, inclusive.");
				}
			}
		}
		public uint[] ToArray()
		{
			return new uint[]
			{
				X, Y, Z
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the vector (X, X).
		/// </summary>
		public Vector2ui XX
		{
			get
			{
				return new Vector2ui(X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y).
		/// </summary>
		public Vector2ui XY
		{
			get
			{
				return new Vector2ui(X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z).
		/// </summary>
		public Vector2ui XZ
		{
			get
			{
				return new Vector2ui(X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X).
		/// </summary>
		public Vector2ui YX
		{
			get
			{
				return new Vector2ui(Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y).
		/// </summary>
		public Vector2ui YY
		{
			get
			{
				return new Vector2ui(Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z).
		/// </summary>
		public Vector2ui YZ
		{
			get
			{
				return new Vector2ui(Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X).
		/// </summary>
		public Vector2ui ZX
		{
			get
			{
				return new Vector2ui(Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y).
		/// </summary>
		public Vector2ui ZY
		{
			get
			{
				return new Vector2ui(Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z).
		/// </summary>
		public Vector2ui ZZ
		{
			get
			{
				return new Vector2ui(Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X).
		/// </summary>
		public Vector3ui XXX
		{
			get
			{
				return new Vector3ui(X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y).
		/// </summary>
		public Vector3ui XXY
		{
			get
			{
				return new Vector3ui(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z).
		/// </summary>
		public Vector3ui XXZ
		{
			get
			{
				return new Vector3ui(X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X).
		/// </summary>
		public Vector3ui XYX
		{
			get
			{
				return new Vector3ui(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y).
		/// </summary>
		public Vector3ui XYY
		{
			get
			{
				return new Vector3ui(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z).
		/// </summary>
		public Vector3ui XYZ
		{
			get
			{
				return new Vector3ui(X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X).
		/// </summary>
		public Vector3ui XZX
		{
			get
			{
				return new Vector3ui(X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y).
		/// </summary>
		public Vector3ui XZY
		{
			get
			{
				return new Vector3ui(X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z).
		/// </summary>
		public Vector3ui XZZ
		{
			get
			{
				return new Vector3ui(X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X).
		/// </summary>
		public Vector3ui YXX
		{
			get
			{
				return new Vector3ui(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y).
		/// </summary>
		public Vector3ui YXY
		{
			get
			{
				return new Vector3ui(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z).
		/// </summary>
		public Vector3ui YXZ
		{
			get
			{
				return new Vector3ui(Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X).
		/// </summary>
		public Vector3ui YYX
		{
			get
			{
				return new Vector3ui(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y).
		/// </summary>
		public Vector3ui YYY
		{
			get
			{
				return new Vector3ui(Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z).
		/// </summary>
		public Vector3ui YYZ
		{
			get
			{
				return new Vector3ui(Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X).
		/// </summary>
		public Vector3ui YZX
		{
			get
			{
				return new Vector3ui(Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y).
		/// </summary>
		public Vector3ui YZY
		{
			get
			{
				return new Vector3ui(Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z).
		/// </summary>
		public Vector3ui YZZ
		{
			get
			{
				return new Vector3ui(Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X).
		/// </summary>
		public Vector3ui ZXX
		{
			get
			{
				return new Vector3ui(Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y).
		/// </summary>
		public Vector3ui ZXY
		{
			get
			{
				return new Vector3ui(Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z).
		/// </summary>
		public Vector3ui ZXZ
		{
			get
			{
				return new Vector3ui(Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X).
		/// </summary>
		public Vector3ui ZYX
		{
			get
			{
				return new Vector3ui(Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y).
		/// </summary>
		public Vector3ui ZYY
		{
			get
			{
				return new Vector3ui(Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z).
		/// </summary>
		public Vector3ui ZYZ
		{
			get
			{
				return new Vector3ui(Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X).
		/// </summary>
		public Vector3ui ZZX
		{
			get
			{
				return new Vector3ui(Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y).
		/// </summary>
		public Vector3ui ZZY
		{
			get
			{
				return new Vector3ui(Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z).
		/// </summary>
		public Vector3ui ZZZ
		{
			get
			{
				return new Vector3ui(Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, X).
		/// </summary>
		public Vector4ui XXXX
		{
			get
			{
				return new Vector4ui(X, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Y).
		/// </summary>
		public Vector4ui XXXY
		{
			get
			{
				return new Vector4ui(X, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Z).
		/// </summary>
		public Vector4ui XXXZ
		{
			get
			{
				return new Vector4ui(X, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, X).
		/// </summary>
		public Vector4ui XXYX
		{
			get
			{
				return new Vector4ui(X, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Y).
		/// </summary>
		public Vector4ui XXYY
		{
			get
			{
				return new Vector4ui(X, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Z).
		/// </summary>
		public Vector4ui XXYZ
		{
			get
			{
				return new Vector4ui(X, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, X).
		/// </summary>
		public Vector4ui XXZX
		{
			get
			{
				return new Vector4ui(X, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Y).
		/// </summary>
		public Vector4ui XXZY
		{
			get
			{
				return new Vector4ui(X, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Z).
		/// </summary>
		public Vector4ui XXZZ
		{
			get
			{
				return new Vector4ui(X, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, X).
		/// </summary>
		public Vector4ui XYXX
		{
			get
			{
				return new Vector4ui(X, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Y).
		/// </summary>
		public Vector4ui XYXY
		{
			get
			{
				return new Vector4ui(X, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Z).
		/// </summary>
		public Vector4ui XYXZ
		{
			get
			{
				return new Vector4ui(X, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, X).
		/// </summary>
		public Vector4ui XYYX
		{
			get
			{
				return new Vector4ui(X, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Y).
		/// </summary>
		public Vector4ui XYYY
		{
			get
			{
				return new Vector4ui(X, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Z).
		/// </summary>
		public Vector4ui XYYZ
		{
			get
			{
				return new Vector4ui(X, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, X).
		/// </summary>
		public Vector4ui XYZX
		{
			get
			{
				return new Vector4ui(X, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Y).
		/// </summary>
		public Vector4ui XYZY
		{
			get
			{
				return new Vector4ui(X, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Z).
		/// </summary>
		public Vector4ui XYZZ
		{
			get
			{
				return new Vector4ui(X, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, X).
		/// </summary>
		public Vector4ui XZXX
		{
			get
			{
				return new Vector4ui(X, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Y).
		/// </summary>
		public Vector4ui XZXY
		{
			get
			{
				return new Vector4ui(X, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Z).
		/// </summary>
		public Vector4ui XZXZ
		{
			get
			{
				return new Vector4ui(X, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, X).
		/// </summary>
		public Vector4ui XZYX
		{
			get
			{
				return new Vector4ui(X, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Y).
		/// </summary>
		public Vector4ui XZYY
		{
			get
			{
				return new Vector4ui(X, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Z).
		/// </summary>
		public Vector4ui XZYZ
		{
			get
			{
				return new Vector4ui(X, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, X).
		/// </summary>
		public Vector4ui XZZX
		{
			get
			{
				return new Vector4ui(X, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Y).
		/// </summary>
		public Vector4ui XZZY
		{
			get
			{
				return new Vector4ui(X, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Z).
		/// </summary>
		public Vector4ui XZZZ
		{
			get
			{
				return new Vector4ui(X, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, X).
		/// </summary>
		public Vector4ui YXXX
		{
			get
			{
				return new Vector4ui(Y, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Y).
		/// </summary>
		public Vector4ui YXXY
		{
			get
			{
				return new Vector4ui(Y, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Z).
		/// </summary>
		public Vector4ui YXXZ
		{
			get
			{
				return new Vector4ui(Y, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, X).
		/// </summary>
		public Vector4ui YXYX
		{
			get
			{
				return new Vector4ui(Y, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Y).
		/// </summary>
		public Vector4ui YXYY
		{
			get
			{
				return new Vector4ui(Y, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Z).
		/// </summary>
		public Vector4ui YXYZ
		{
			get
			{
				return new Vector4ui(Y, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, X).
		/// </summary>
		public Vector4ui YXZX
		{
			get
			{
				return new Vector4ui(Y, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Y).
		/// </summary>
		public Vector4ui YXZY
		{
			get
			{
				return new Vector4ui(Y, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Z).
		/// </summary>
		public Vector4ui YXZZ
		{
			get
			{
				return new Vector4ui(Y, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, X).
		/// </summary>
		public Vector4ui YYXX
		{
			get
			{
				return new Vector4ui(Y, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Y).
		/// </summary>
		public Vector4ui YYXY
		{
			get
			{
				return new Vector4ui(Y, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Z).
		/// </summary>
		public Vector4ui YYXZ
		{
			get
			{
				return new Vector4ui(Y, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, X).
		/// </summary>
		public Vector4ui YYYX
		{
			get
			{
				return new Vector4ui(Y, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Y).
		/// </summary>
		public Vector4ui YYYY
		{
			get
			{
				return new Vector4ui(Y, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Z).
		/// </summary>
		public Vector4ui YYYZ
		{
			get
			{
				return new Vector4ui(Y, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, X).
		/// </summary>
		public Vector4ui YYZX
		{
			get
			{
				return new Vector4ui(Y, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Y).
		/// </summary>
		public Vector4ui YYZY
		{
			get
			{
				return new Vector4ui(Y, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Z).
		/// </summary>
		public Vector4ui YYZZ
		{
			get
			{
				return new Vector4ui(Y, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, X).
		/// </summary>
		public Vector4ui YZXX
		{
			get
			{
				return new Vector4ui(Y, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Y).
		/// </summary>
		public Vector4ui YZXY
		{
			get
			{
				return new Vector4ui(Y, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Z).
		/// </summary>
		public Vector4ui YZXZ
		{
			get
			{
				return new Vector4ui(Y, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, X).
		/// </summary>
		public Vector4ui YZYX
		{
			get
			{
				return new Vector4ui(Y, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Y).
		/// </summary>
		public Vector4ui YZYY
		{
			get
			{
				return new Vector4ui(Y, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Z).
		/// </summary>
		public Vector4ui YZYZ
		{
			get
			{
				return new Vector4ui(Y, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, X).
		/// </summary>
		public Vector4ui YZZX
		{
			get
			{
				return new Vector4ui(Y, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Y).
		/// </summary>
		public Vector4ui YZZY
		{
			get
			{
				return new Vector4ui(Y, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Z).
		/// </summary>
		public Vector4ui YZZZ
		{
			get
			{
				return new Vector4ui(Y, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, X).
		/// </summary>
		public Vector4ui ZXXX
		{
			get
			{
				return new Vector4ui(Z, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Y).
		/// </summary>
		public Vector4ui ZXXY
		{
			get
			{
				return new Vector4ui(Z, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Z).
		/// </summary>
		public Vector4ui ZXXZ
		{
			get
			{
				return new Vector4ui(Z, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, X).
		/// </summary>
		public Vector4ui ZXYX
		{
			get
			{
				return new Vector4ui(Z, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Y).
		/// </summary>
		public Vector4ui ZXYY
		{
			get
			{
				return new Vector4ui(Z, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Z).
		/// </summary>
		public Vector4ui ZXYZ
		{
			get
			{
				return new Vector4ui(Z, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, X).
		/// </summary>
		public Vector4ui ZXZX
		{
			get
			{
				return new Vector4ui(Z, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Y).
		/// </summary>
		public Vector4ui ZXZY
		{
			get
			{
				return new Vector4ui(Z, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Z).
		/// </summary>
		public Vector4ui ZXZZ
		{
			get
			{
				return new Vector4ui(Z, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, X).
		/// </summary>
		public Vector4ui ZYXX
		{
			get
			{
				return new Vector4ui(Z, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Y).
		/// </summary>
		public Vector4ui ZYXY
		{
			get
			{
				return new Vector4ui(Z, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Z).
		/// </summary>
		public Vector4ui ZYXZ
		{
			get
			{
				return new Vector4ui(Z, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, X).
		/// </summary>
		public Vector4ui ZYYX
		{
			get
			{
				return new Vector4ui(Z, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Y).
		/// </summary>
		public Vector4ui ZYYY
		{
			get
			{
				return new Vector4ui(Z, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Z).
		/// </summary>
		public Vector4ui ZYYZ
		{
			get
			{
				return new Vector4ui(Z, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, X).
		/// </summary>
		public Vector4ui ZYZX
		{
			get
			{
				return new Vector4ui(Z, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Y).
		/// </summary>
		public Vector4ui ZYZY
		{
			get
			{
				return new Vector4ui(Z, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Z).
		/// </summary>
		public Vector4ui ZYZZ
		{
			get
			{
				return new Vector4ui(Z, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, X).
		/// </summary>
		public Vector4ui ZZXX
		{
			get
			{
				return new Vector4ui(Z, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Y).
		/// </summary>
		public Vector4ui ZZXY
		{
			get
			{
				return new Vector4ui(Z, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Z).
		/// </summary>
		public Vector4ui ZZXZ
		{
			get
			{
				return new Vector4ui(Z, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, X).
		/// </summary>
		public Vector4ui ZZYX
		{
			get
			{
				return new Vector4ui(Z, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Y).
		/// </summary>
		public Vector4ui ZZYY
		{
			get
			{
				return new Vector4ui(Z, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Z).
		/// </summary>
		public Vector4ui ZZYZ
		{
			get
			{
				return new Vector4ui(Z, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, X).
		/// </summary>
		public Vector4ui ZZZX
		{
			get
			{
				return new Vector4ui(Z, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Y).
		/// </summary>
		public Vector4ui ZZZY
		{
			get
			{
				return new Vector4ui(Z, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Z).
		/// </summary>
		public Vector4ui ZZZZ
		{
			get
			{
				return new Vector4ui(Z, Z, Z, Z);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3ui"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector3ui(uint value)
		{
			X = value;
			Y = value;
			Z = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3ui"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X and Y components</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		public Vector3ui(Vector2ui value, uint z)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3ui"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		public Vector3ui(uint x, uint y, uint z)
		{
			X = x;
			Y = y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3ui"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector3ui(uint[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3ui"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector3ui(uint[] array, int offset)
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
		public static Vector3ui operator +(Vector3ui value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector3l operator -(Vector3ui value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector3ui operator +(Vector3ui left, Vector3ui right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3ui operator -(Vector3ui left, Vector3ui right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector3ui operator *(Vector3ui left, uint right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector3ui operator *(uint left, Vector3ui right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector3ui operator /(Vector3ui left, uint right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector3d value to a Vector3ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ui.</param>
		/// <returns>A Vector3ui that has all components equal to value.</returns>
		public static explicit operator Vector3ui(Vector3d value)
		{
			return new Vector3ui((uint)value.X, (uint)value.Y, (uint)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3f value to a Vector3ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ui.</param>
		/// <returns>A Vector3ui that has all components equal to value.</returns>
		public static explicit operator Vector3ui(Vector3f value)
		{
			return new Vector3ui((uint)value.X, (uint)value.Y, (uint)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3h value to a Vector3ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ui.</param>
		/// <returns>A Vector3ui that has all components equal to value.</returns>
		public static explicit operator Vector3ui(Vector3h value)
		{
			return new Vector3ui((uint)value.X, (uint)value.Y, (uint)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3ul value to a Vector3ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ui.</param>
		/// <returns>A Vector3ui that has all components equal to value.</returns>
		public static explicit operator Vector3ui(Vector3ul value)
		{
			return new Vector3ui((uint)value.X, (uint)value.Y, (uint)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3l value to a Vector3ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ui.</param>
		/// <returns>A Vector3ui that has all components equal to value.</returns>
		public static explicit operator Vector3ui(Vector3l value)
		{
			return new Vector3ui((uint)value.X, (uint)value.Y, (uint)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3i value to a Vector3ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ui.</param>
		/// <returns>A Vector3ui that has all components equal to value.</returns>
		public static explicit operator Vector3ui(Vector3i value)
		{
			return new Vector3ui((uint)value.X, (uint)value.Y, (uint)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector3us value to a Vector3ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ui.</param>
		/// <returns>A Vector3ui that has all components equal to value.</returns>
		public static implicit operator Vector3ui(Vector3us value)
		{
			return new Vector3ui((uint)value.X, (uint)value.Y, (uint)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3s value to a Vector3ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ui.</param>
		/// <returns>A Vector3ui that has all components equal to value.</returns>
		public static explicit operator Vector3ui(Vector3s value)
		{
			return new Vector3ui((uint)value.X, (uint)value.Y, (uint)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector3b value to a Vector3ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ui.</param>
		/// <returns>A Vector3ui that has all components equal to value.</returns>
		public static implicit operator Vector3ui(Vector3b value)
		{
			return new Vector3ui((uint)value.X, (uint)value.Y, (uint)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3sb value to a Vector3ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ui.</param>
		/// <returns>A Vector3ui that has all components equal to value.</returns>
		public static explicit operator Vector3ui(Vector3sb value)
		{
			return new Vector3ui((uint)value.X, (uint)value.Y, (uint)value.Z);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector3ui"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector3ui"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector3ui"/> object, and its value
		/// is equal to the current <see cref="Vector3ui"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector3ui) { return Equals((Vector3ui)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector3ui other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector3ui left, Vector3ui right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector3ui left, Vector3ui right)
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
		/// Writes the given <see cref="Vector3ui"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector3ui vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
			writer.Write(vector.Z);
		}
		/// <summary>
		/// Reads a <see cref="Vector3ui"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector3ui ReadVector3ui(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector3ui(reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32());
		}
		#endregion
		#region Pack
		public static long Pack(int xBits, int yBits, int zBits, Vector3ui vector)
		{
			Contract.Requires(0 <= xBits && xBits <= 32, "xBits must be between 0 and 32 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 32, "yBits must be between 0 and 32 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 32, "zBits must be between 0 and 32 inclusive.");
			Contract.Requires(xBits + yBits + zBits <= 64);
			ulong x = (ulong)(vector.X) >> (64 - xBits);
			ulong y = (ulong)(vector.Y) >> (64 - yBits);
			y <<= xBits;
			ulong z = (ulong)(vector.Z) >> (64 - zBits);
			z <<= xBits + yBits;
			return (long)(x | y | z);
		}
		public static Vector3ui Unpack(int xBits, int yBits, int zBits, uint bits)
		{
			Contract.Requires(0 <= xBits && xBits <= 32, "xBits must be between 0 and 32 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 32, "yBits must be between 0 and 32 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 32, "zBits must be between 0 and 32 inclusive.");
			Contract.Requires(xBits + yBits + zBits <= 64);
			ulong x = (ulong)(bits);
			x &= ((1UL << xBits) - 1);
			ulong y = (ulong)(bits) >> (xBits);
			y &= ((1UL << yBits) - 1);
			ulong z = (ulong)(bits) >> (xBits + yBits);
			z &= ((1UL << zBits) - 1);
			return new Vector3ui((uint)x, (uint)y, (uint)z);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector3l Negative(Vector3ui value)
		{
			return new Vector3l(-value.X, -value.Y, -value.Z);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector3ui Add(Vector3ui left, Vector3ui right)
		{
			return new Vector3ui(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3ui Subtract(Vector3ui left, Vector3ui right)
		{
			return new Vector3ui(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector3ui Multiply(Vector3ui vector, uint scalar)
		{
			return new Vector3ui(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector3ui Divide(Vector3ui vector, uint scalar)
		{
			return new Vector3ui(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Vector3ui left, Vector3ui right)
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
		public static uint Dot(Vector3ui left, Vector3ui right)
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
		public static bool All(Vector3ui value)
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
		public static bool All(Vector3ui value, Predicate<uint> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		[CLSCompliant(false)]
		public static bool Any(Vector3ui value)
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
		public static bool Any(Vector3ui value, Predicate<uint> predicate)
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
		public static uint AbsoluteSquared(Vector3ui value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		[CLSCompliant(false)]
		public static double Absolute(Vector3ui value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		[CLSCompliant(false)]
		public static Vector3d Normalize(Vector3ui value)
		{
			var absolute = Absolute(value);
			if(absolute <= double.Epsilon)
			{
				return Vector3ui.Zero;
			}
			else
			{
				return (Vector3d)value / absolute;
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
		public static Vector3d Map(Vector3ui value, Func<uint, double> mapping)
		{
			return new Vector3d(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3f Map(Vector3ui value, Func<uint, float> mapping)
		{
			return new Vector3f(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3h Map(Vector3ui value, Func<uint, Half> mapping)
		{
			return new Vector3h(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3ul Map(Vector3ui value, Func<uint, ulong> mapping)
		{
			return new Vector3ul(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3l Map(Vector3ui value, Func<uint, long> mapping)
		{
			return new Vector3l(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3ui Map(Vector3ui value, Func<uint, uint> mapping)
		{
			return new Vector3ui(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3i Map(Vector3ui value, Func<uint, int> mapping)
		{
			return new Vector3i(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3us Map(Vector3ui value, Func<uint, ushort> mapping)
		{
			return new Vector3us(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3s Map(Vector3ui value, Func<uint, short> mapping)
		{
			return new Vector3s(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3b Map(Vector3ui value, Func<uint, byte> mapping)
		{
			return new Vector3b(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		/// <summary>
		/// Maps the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to map.</param>
		/// <param name="mapping">A mapping function to apply to each component.</param>
		/// <returns>The result of mapping each component of value.</returns>
		[CLSCompliant(false)]
		public static Vector3sb Map(Vector3ui value, Func<uint, sbyte> mapping)
		{
			return new Vector3sb(mapping(value.X), mapping(value.Y), mapping(value.Z));
		}
		#endregion
		/// <summary>
		/// Multiplys the components of two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first vector to modulate.</param>
		/// <param name="right">The second vector to modulate.</param>
		/// <returns>The result of multiplying each component of left by the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector3ui Modulate(Vector3ui left, Vector3ui right)
		{
			return new Vector3ui(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		[CLSCompliant(false)]
		public static Vector3ui Abs(Vector3ui value)
		{
			return new Vector3ui(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector3ui Min(Vector3ui value1, Vector3ui value2)
		{
			return new Vector3ui(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		[CLSCompliant(false)]
		public static Vector3ui Max(Vector3ui value1, Vector3ui value2)
		{
			return new Vector3ui(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		[CLSCompliant(false)]
		public static Vector3ui Clamp(Vector3ui value, Vector3ui min, Vector3ui max)
		{
			return new Vector3ui(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z));
		}
		#endregion
		#region Coordinate spaces
		/// <summary>
		/// Transforms a vector in cartesian coordinates to spherical coordinates.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <returns>The spherical coordinates of value.</returns>
		[CLSCompliant(false)]
		public static SphericalCoordinate CartesianToSpherical (Vector3ui value)
		{
			double r = Functions.Sqrt(value.X * value.X + value.Y * value.Y + value.Z * value.Z);
			double theta = Functions.Atan2(value.Y, value.X);
			if (theta < 0)
				theta += 2 * Constants.Pi;
			return new SphericalCoordinate(
			     (double)Functions.Acos(value.Z / r),
			     theta,
			     r);
		}
		#endregion
	}
}
