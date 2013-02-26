using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a three component vector of longs, of the form (X, Y, Z).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3l: IEquatable<Vector3l>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector3l"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector3l Zero = new Vector3l(0);
		/// <summary>
		/// Returns a new <see cref="Vector3l"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector3l One = new Vector3l(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector3l"/> (1, 0, 0).
		/// </summary>
		public static readonly Vector3l UnitX = new Vector3l(1, 0, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector3l"/> (0, 1, 0).
		/// </summary>
		public static readonly Vector3l UnitY = new Vector3l(0, 1, 0);
		/// <summary>
		/// Returns the Z unit <see cref="Vector3l"/> (0, 0, 1).
		/// </summary>
		public static readonly Vector3l UnitZ = new Vector3l(0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the vector.
		/// </summary>
		public readonly long X;
		/// <summary>
		/// The Y component of the vector.
		/// </summary>
		public readonly long Y;
		/// <summary>
		/// The Z component of the vector.
		/// </summary>
		public readonly long Z;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public long this[int index]
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
						throw new IndexOutOfRangeException("Indices for Vector3l run from 0 to 2, inclusive.");
				}
			}
		}
		public long[] ToArray()
		{
			return new long[]
			{
				X, Y, Z
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the vector (X, X).
		/// </summary>
		public Vector2l XX
		{
			get
			{
				return new Vector2l(X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y).
		/// </summary>
		public Vector2l XY
		{
			get
			{
				return new Vector2l(X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z).
		/// </summary>
		public Vector2l XZ
		{
			get
			{
				return new Vector2l(X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X).
		/// </summary>
		public Vector2l YX
		{
			get
			{
				return new Vector2l(Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y).
		/// </summary>
		public Vector2l YY
		{
			get
			{
				return new Vector2l(Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z).
		/// </summary>
		public Vector2l YZ
		{
			get
			{
				return new Vector2l(Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X).
		/// </summary>
		public Vector2l ZX
		{
			get
			{
				return new Vector2l(Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y).
		/// </summary>
		public Vector2l ZY
		{
			get
			{
				return new Vector2l(Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z).
		/// </summary>
		public Vector2l ZZ
		{
			get
			{
				return new Vector2l(Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X).
		/// </summary>
		public Vector3l XXX
		{
			get
			{
				return new Vector3l(X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y).
		/// </summary>
		public Vector3l XXY
		{
			get
			{
				return new Vector3l(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z).
		/// </summary>
		public Vector3l XXZ
		{
			get
			{
				return new Vector3l(X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X).
		/// </summary>
		public Vector3l XYX
		{
			get
			{
				return new Vector3l(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y).
		/// </summary>
		public Vector3l XYY
		{
			get
			{
				return new Vector3l(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z).
		/// </summary>
		public Vector3l XYZ
		{
			get
			{
				return new Vector3l(X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X).
		/// </summary>
		public Vector3l XZX
		{
			get
			{
				return new Vector3l(X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y).
		/// </summary>
		public Vector3l XZY
		{
			get
			{
				return new Vector3l(X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z).
		/// </summary>
		public Vector3l XZZ
		{
			get
			{
				return new Vector3l(X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X).
		/// </summary>
		public Vector3l YXX
		{
			get
			{
				return new Vector3l(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y).
		/// </summary>
		public Vector3l YXY
		{
			get
			{
				return new Vector3l(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z).
		/// </summary>
		public Vector3l YXZ
		{
			get
			{
				return new Vector3l(Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X).
		/// </summary>
		public Vector3l YYX
		{
			get
			{
				return new Vector3l(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y).
		/// </summary>
		public Vector3l YYY
		{
			get
			{
				return new Vector3l(Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z).
		/// </summary>
		public Vector3l YYZ
		{
			get
			{
				return new Vector3l(Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X).
		/// </summary>
		public Vector3l YZX
		{
			get
			{
				return new Vector3l(Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y).
		/// </summary>
		public Vector3l YZY
		{
			get
			{
				return new Vector3l(Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z).
		/// </summary>
		public Vector3l YZZ
		{
			get
			{
				return new Vector3l(Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X).
		/// </summary>
		public Vector3l ZXX
		{
			get
			{
				return new Vector3l(Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y).
		/// </summary>
		public Vector3l ZXY
		{
			get
			{
				return new Vector3l(Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z).
		/// </summary>
		public Vector3l ZXZ
		{
			get
			{
				return new Vector3l(Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X).
		/// </summary>
		public Vector3l ZYX
		{
			get
			{
				return new Vector3l(Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y).
		/// </summary>
		public Vector3l ZYY
		{
			get
			{
				return new Vector3l(Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z).
		/// </summary>
		public Vector3l ZYZ
		{
			get
			{
				return new Vector3l(Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X).
		/// </summary>
		public Vector3l ZZX
		{
			get
			{
				return new Vector3l(Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y).
		/// </summary>
		public Vector3l ZZY
		{
			get
			{
				return new Vector3l(Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z).
		/// </summary>
		public Vector3l ZZZ
		{
			get
			{
				return new Vector3l(Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, X).
		/// </summary>
		public Vector4l XXXX
		{
			get
			{
				return new Vector4l(X, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Y).
		/// </summary>
		public Vector4l XXXY
		{
			get
			{
				return new Vector4l(X, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Z).
		/// </summary>
		public Vector4l XXXZ
		{
			get
			{
				return new Vector4l(X, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, X).
		/// </summary>
		public Vector4l XXYX
		{
			get
			{
				return new Vector4l(X, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Y).
		/// </summary>
		public Vector4l XXYY
		{
			get
			{
				return new Vector4l(X, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Z).
		/// </summary>
		public Vector4l XXYZ
		{
			get
			{
				return new Vector4l(X, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, X).
		/// </summary>
		public Vector4l XXZX
		{
			get
			{
				return new Vector4l(X, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Y).
		/// </summary>
		public Vector4l XXZY
		{
			get
			{
				return new Vector4l(X, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Z).
		/// </summary>
		public Vector4l XXZZ
		{
			get
			{
				return new Vector4l(X, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, X).
		/// </summary>
		public Vector4l XYXX
		{
			get
			{
				return new Vector4l(X, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Y).
		/// </summary>
		public Vector4l XYXY
		{
			get
			{
				return new Vector4l(X, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Z).
		/// </summary>
		public Vector4l XYXZ
		{
			get
			{
				return new Vector4l(X, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, X).
		/// </summary>
		public Vector4l XYYX
		{
			get
			{
				return new Vector4l(X, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Y).
		/// </summary>
		public Vector4l XYYY
		{
			get
			{
				return new Vector4l(X, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Z).
		/// </summary>
		public Vector4l XYYZ
		{
			get
			{
				return new Vector4l(X, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, X).
		/// </summary>
		public Vector4l XYZX
		{
			get
			{
				return new Vector4l(X, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Y).
		/// </summary>
		public Vector4l XYZY
		{
			get
			{
				return new Vector4l(X, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Z).
		/// </summary>
		public Vector4l XYZZ
		{
			get
			{
				return new Vector4l(X, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, X).
		/// </summary>
		public Vector4l XZXX
		{
			get
			{
				return new Vector4l(X, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Y).
		/// </summary>
		public Vector4l XZXY
		{
			get
			{
				return new Vector4l(X, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Z).
		/// </summary>
		public Vector4l XZXZ
		{
			get
			{
				return new Vector4l(X, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, X).
		/// </summary>
		public Vector4l XZYX
		{
			get
			{
				return new Vector4l(X, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Y).
		/// </summary>
		public Vector4l XZYY
		{
			get
			{
				return new Vector4l(X, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Z).
		/// </summary>
		public Vector4l XZYZ
		{
			get
			{
				return new Vector4l(X, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, X).
		/// </summary>
		public Vector4l XZZX
		{
			get
			{
				return new Vector4l(X, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Y).
		/// </summary>
		public Vector4l XZZY
		{
			get
			{
				return new Vector4l(X, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Z).
		/// </summary>
		public Vector4l XZZZ
		{
			get
			{
				return new Vector4l(X, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, X).
		/// </summary>
		public Vector4l YXXX
		{
			get
			{
				return new Vector4l(Y, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Y).
		/// </summary>
		public Vector4l YXXY
		{
			get
			{
				return new Vector4l(Y, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Z).
		/// </summary>
		public Vector4l YXXZ
		{
			get
			{
				return new Vector4l(Y, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, X).
		/// </summary>
		public Vector4l YXYX
		{
			get
			{
				return new Vector4l(Y, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Y).
		/// </summary>
		public Vector4l YXYY
		{
			get
			{
				return new Vector4l(Y, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Z).
		/// </summary>
		public Vector4l YXYZ
		{
			get
			{
				return new Vector4l(Y, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, X).
		/// </summary>
		public Vector4l YXZX
		{
			get
			{
				return new Vector4l(Y, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Y).
		/// </summary>
		public Vector4l YXZY
		{
			get
			{
				return new Vector4l(Y, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Z).
		/// </summary>
		public Vector4l YXZZ
		{
			get
			{
				return new Vector4l(Y, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, X).
		/// </summary>
		public Vector4l YYXX
		{
			get
			{
				return new Vector4l(Y, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Y).
		/// </summary>
		public Vector4l YYXY
		{
			get
			{
				return new Vector4l(Y, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Z).
		/// </summary>
		public Vector4l YYXZ
		{
			get
			{
				return new Vector4l(Y, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, X).
		/// </summary>
		public Vector4l YYYX
		{
			get
			{
				return new Vector4l(Y, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Y).
		/// </summary>
		public Vector4l YYYY
		{
			get
			{
				return new Vector4l(Y, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Z).
		/// </summary>
		public Vector4l YYYZ
		{
			get
			{
				return new Vector4l(Y, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, X).
		/// </summary>
		public Vector4l YYZX
		{
			get
			{
				return new Vector4l(Y, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Y).
		/// </summary>
		public Vector4l YYZY
		{
			get
			{
				return new Vector4l(Y, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Z).
		/// </summary>
		public Vector4l YYZZ
		{
			get
			{
				return new Vector4l(Y, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, X).
		/// </summary>
		public Vector4l YZXX
		{
			get
			{
				return new Vector4l(Y, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Y).
		/// </summary>
		public Vector4l YZXY
		{
			get
			{
				return new Vector4l(Y, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Z).
		/// </summary>
		public Vector4l YZXZ
		{
			get
			{
				return new Vector4l(Y, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, X).
		/// </summary>
		public Vector4l YZYX
		{
			get
			{
				return new Vector4l(Y, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Y).
		/// </summary>
		public Vector4l YZYY
		{
			get
			{
				return new Vector4l(Y, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Z).
		/// </summary>
		public Vector4l YZYZ
		{
			get
			{
				return new Vector4l(Y, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, X).
		/// </summary>
		public Vector4l YZZX
		{
			get
			{
				return new Vector4l(Y, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Y).
		/// </summary>
		public Vector4l YZZY
		{
			get
			{
				return new Vector4l(Y, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Z).
		/// </summary>
		public Vector4l YZZZ
		{
			get
			{
				return new Vector4l(Y, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, X).
		/// </summary>
		public Vector4l ZXXX
		{
			get
			{
				return new Vector4l(Z, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Y).
		/// </summary>
		public Vector4l ZXXY
		{
			get
			{
				return new Vector4l(Z, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Z).
		/// </summary>
		public Vector4l ZXXZ
		{
			get
			{
				return new Vector4l(Z, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, X).
		/// </summary>
		public Vector4l ZXYX
		{
			get
			{
				return new Vector4l(Z, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Y).
		/// </summary>
		public Vector4l ZXYY
		{
			get
			{
				return new Vector4l(Z, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Z).
		/// </summary>
		public Vector4l ZXYZ
		{
			get
			{
				return new Vector4l(Z, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, X).
		/// </summary>
		public Vector4l ZXZX
		{
			get
			{
				return new Vector4l(Z, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Y).
		/// </summary>
		public Vector4l ZXZY
		{
			get
			{
				return new Vector4l(Z, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Z).
		/// </summary>
		public Vector4l ZXZZ
		{
			get
			{
				return new Vector4l(Z, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, X).
		/// </summary>
		public Vector4l ZYXX
		{
			get
			{
				return new Vector4l(Z, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Y).
		/// </summary>
		public Vector4l ZYXY
		{
			get
			{
				return new Vector4l(Z, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Z).
		/// </summary>
		public Vector4l ZYXZ
		{
			get
			{
				return new Vector4l(Z, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, X).
		/// </summary>
		public Vector4l ZYYX
		{
			get
			{
				return new Vector4l(Z, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Y).
		/// </summary>
		public Vector4l ZYYY
		{
			get
			{
				return new Vector4l(Z, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Z).
		/// </summary>
		public Vector4l ZYYZ
		{
			get
			{
				return new Vector4l(Z, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, X).
		/// </summary>
		public Vector4l ZYZX
		{
			get
			{
				return new Vector4l(Z, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Y).
		/// </summary>
		public Vector4l ZYZY
		{
			get
			{
				return new Vector4l(Z, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Z).
		/// </summary>
		public Vector4l ZYZZ
		{
			get
			{
				return new Vector4l(Z, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, X).
		/// </summary>
		public Vector4l ZZXX
		{
			get
			{
				return new Vector4l(Z, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Y).
		/// </summary>
		public Vector4l ZZXY
		{
			get
			{
				return new Vector4l(Z, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Z).
		/// </summary>
		public Vector4l ZZXZ
		{
			get
			{
				return new Vector4l(Z, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, X).
		/// </summary>
		public Vector4l ZZYX
		{
			get
			{
				return new Vector4l(Z, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Y).
		/// </summary>
		public Vector4l ZZYY
		{
			get
			{
				return new Vector4l(Z, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Z).
		/// </summary>
		public Vector4l ZZYZ
		{
			get
			{
				return new Vector4l(Z, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, X).
		/// </summary>
		public Vector4l ZZZX
		{
			get
			{
				return new Vector4l(Z, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Y).
		/// </summary>
		public Vector4l ZZZY
		{
			get
			{
				return new Vector4l(Z, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Z).
		/// </summary>
		public Vector4l ZZZZ
		{
			get
			{
				return new Vector4l(Z, Z, Z, Z);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3l"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector3l(long value)
		{
			X = value;
			Y = value;
			Z = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3l"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X and Y components</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		public Vector3l(Vector2l value, long z)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3l"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		public Vector3l(long x, long y, long z)
		{
			X = x;
			Y = y;
			Z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3l"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector3l(long[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3l"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector3l(long[] array, int offset)
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
		public static Vector3l operator +(Vector3l value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector3l operator -(Vector3l value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector3l operator +(Vector3l left, Vector3l right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3l operator -(Vector3l left, Vector3l right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector3l operator *(Vector3l left, long right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector3l operator *(long left, Vector3l right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector3l operator /(Vector3l left, long right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector3d value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		public static explicit operator Vector3l(Vector3d value)
		{
			return new Vector3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3f value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		public static explicit operator Vector3l(Vector3f value)
		{
			return new Vector3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3h value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		public static explicit operator Vector3l(Vector3h value)
		{
			return new Vector3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3ul value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector3l(Vector3ul value)
		{
			return new Vector3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector3ui value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector3l(Vector3ui value)
		{
			return new Vector3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector3i value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		public static implicit operator Vector3l(Vector3i value)
		{
			return new Vector3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector3us value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector3l(Vector3us value)
		{
			return new Vector3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector3s value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		public static implicit operator Vector3l(Vector3s value)
		{
			return new Vector3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector3b value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		public static implicit operator Vector3l(Vector3b value)
		{
			return new Vector3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector3sb value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector3l(Vector3sb value)
		{
			return new Vector3l((long)value.X, (long)value.Y, (long)value.Z);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector3l"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector3l"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector3l"/> object, and its value
		/// is equal to the current <see cref="Vector3l"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector3l) { return Equals((Vector3l)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector3l other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector3l left, Vector3l right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector3l left, Vector3l right)
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
		/// Writes the given <see cref="Vector3l"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector3l vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
			writer.Write(vector.Z);
		}
		/// <summary>
		/// Reads a <see cref="Vector3l"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector3l ReadVector3l(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector3l(reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64());
		}
		#endregion
		#region Pack
		public static long Pack(int xBits, int yBits, int zBits, Vector3l vector)
		{
			Contract.Requires(0 <= xBits && xBits <= 64, "xBits must be between 0 and 64 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 64, "yBits must be between 0 and 64 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 64, "zBits must be between 0 and 64 inclusive.");
			Contract.Requires(xBits + yBits + zBits <= 64);
			ulong x = (ulong)(vector.X) >> (64 - xBits);
			ulong y = (ulong)(vector.Y) >> (64 - yBits);
			y <<= xBits;
			ulong z = (ulong)(vector.Z) >> (64 - zBits);
			z <<= xBits + yBits;
			return (long)(x | y | z);
		}
		public static Vector3l Unpack(int xBits, int yBits, int zBits, long bits)
		{
			Contract.Requires(0 <= xBits && xBits <= 64, "xBits must be between 0 and 64 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 64, "yBits must be between 0 and 64 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 64, "zBits must be between 0 and 64 inclusive.");
			Contract.Requires(xBits + yBits + zBits <= 64);
			ulong x = (ulong)(bits);
			x &= ((1UL << xBits) - 1);
			ulong y = (ulong)(bits) >> (xBits);
			y &= ((1UL << yBits) - 1);
			ulong z = (ulong)(bits) >> (xBits + yBits);
			z &= ((1UL << zBits) - 1);
			return new Vector3l((long)x, (long)y, (long)z);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector3l Negative(Vector3l value)
		{
			return new Vector3l(-value.X, -value.Y, -value.Z);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector3l Add(Vector3l left, Vector3l right)
		{
			return new Vector3l(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector3l Subtract(Vector3l left, Vector3l right)
		{
			return new Vector3l(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector3l Multiply(Vector3l vector, long scalar)
		{
			return new Vector3l(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector3l Divide(Vector3l vector, long scalar)
		{
			return new Vector3l(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Vector3l left, Vector3l right)
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
		public static long Dot(Vector3l left, Vector3l right)
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
		public static bool All(Vector3l value)
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
		public static bool All(Vector3l value, Predicate<long> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Vector3l value)
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
		public static bool Any(Vector3l value, Predicate<long> predicate)
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
		public static long AbsoluteSquared(Vector3l value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		public static double Absolute(Vector3l value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		public static Vector3d Normalize(Vector3l value)
		{
			var absolute = Absolute(value);
			if(absolute <= double.Epsilon)
			{
				return Vector3l.Zero;
			}
			else
			{
				return (Vector3d)value / absolute;
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
		public static Vector3d Transform(Vector3l value, Func<long, double> transformer)
		{
			return new Vector3d(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3f Transform(Vector3l value, Func<long, float> transformer)
		{
			return new Vector3f(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3h Transform(Vector3l value, Func<long, Half> transformer)
		{
			return new Vector3h(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3ul Transform(Vector3l value, Func<long, ulong> transformer)
		{
			return new Vector3ul(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3l Transform(Vector3l value, Func<long, long> transformer)
		{
			return new Vector3l(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3ui Transform(Vector3l value, Func<long, uint> transformer)
		{
			return new Vector3ui(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3i Transform(Vector3l value, Func<long, int> transformer)
		{
			return new Vector3i(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3us Transform(Vector3l value, Func<long, ushort> transformer)
		{
			return new Vector3us(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3s Transform(Vector3l value, Func<long, short> transformer)
		{
			return new Vector3s(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3b Transform(Vector3l value, Func<long, byte> transformer)
		{
			return new Vector3b(transformer(value.X), transformer(value.Y), transformer(value.Z));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector3sb Transform(Vector3l value, Func<long, sbyte> transformer)
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
		public static Vector3l Modulate(Vector3l left, Vector3l right)
		{
			return new Vector3l(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Vector3l Abs(Vector3l value)
		{
			return new Vector3l(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Vector3l Min(Vector3l value1, Vector3l value2)
		{
			return new Vector3l(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Vector3l Max(Vector3l value1, Vector3l value2)
		{
			return new Vector3l(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		public static Vector3l Clamp(Vector3l value, Vector3l min, Vector3l max)
		{
			return new Vector3l(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z));
		}
		#endregion
		#region Coordinate spaces
		/// <summary>
		/// Transforms a vector in cartesian coordinates to spherical coordinates.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <returns>The spherical coordinates of value.</returns>
		public static SphericalCoordinate CartesianToSpherical (Vector3l value)
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
