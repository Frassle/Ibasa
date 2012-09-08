using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a four component vector of longs, of the form (X, Y, Z, W).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector4l: IEquatable<Vector4l>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector4l"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector4l Zero = new Vector4l(0);
		/// <summary>
		/// Returns a new <see cref="Vector4l"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector4l One = new Vector4l(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector4l"/> (1, 0, 0, 0).
		/// </summary>
		public static readonly Vector4l UnitX = new Vector4l(1, 0, 0, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector4l"/> (0, 1, 0, 0).
		/// </summary>
		public static readonly Vector4l UnitY = new Vector4l(0, 1, 0, 0);
		/// <summary>
		/// Returns the Z unit <see cref="Vector4l"/> (0, 0, 1, 0).
		/// </summary>
		public static readonly Vector4l UnitZ = new Vector4l(0, 0, 1, 0);
		/// <summary>
		/// Returns the W unit <see cref="Vector4l"/> (0, 0, 0, 1).
		/// </summary>
		public static readonly Vector4l UnitW = new Vector4l(0, 0, 0, 1);
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
		/// <summary>
		/// The W component of the vector.
		/// </summary>
		public readonly long W;
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
					case 3:
						return W;
					default:
						throw new IndexOutOfRangeException("Indices for Vector4l run from 0 to 3, inclusive.");
				}
			}
		}
		public long[] ToArray()
		{
			return new long[]
			{
				X, Y, Z, W
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
		/// Returns the vector (X, W).
		/// </summary>
		public Vector2l XW
		{
			get
			{
				return new Vector2l(X, W);
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
		/// Returns the vector (Y, W).
		/// </summary>
		public Vector2l YW
		{
			get
			{
				return new Vector2l(Y, W);
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
		/// Returns the vector (Z, W).
		/// </summary>
		public Vector2l ZW
		{
			get
			{
				return new Vector2l(Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X).
		/// </summary>
		public Vector2l WX
		{
			get
			{
				return new Vector2l(W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y).
		/// </summary>
		public Vector2l WY
		{
			get
			{
				return new Vector2l(W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z).
		/// </summary>
		public Vector2l WZ
		{
			get
			{
				return new Vector2l(W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W).
		/// </summary>
		public Vector2l WW
		{
			get
			{
				return new Vector2l(W, W);
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
		/// Returns the vector (X, X, W).
		/// </summary>
		public Vector3l XXW
		{
			get
			{
				return new Vector3l(X, X, W);
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
		/// Returns the vector (X, Y, W).
		/// </summary>
		public Vector3l XYW
		{
			get
			{
				return new Vector3l(X, Y, W);
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
		/// Returns the vector (X, Z, W).
		/// </summary>
		public Vector3l XZW
		{
			get
			{
				return new Vector3l(X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X).
		/// </summary>
		public Vector3l XWX
		{
			get
			{
				return new Vector3l(X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y).
		/// </summary>
		public Vector3l XWY
		{
			get
			{
				return new Vector3l(X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z).
		/// </summary>
		public Vector3l XWZ
		{
			get
			{
				return new Vector3l(X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W).
		/// </summary>
		public Vector3l XWW
		{
			get
			{
				return new Vector3l(X, W, W);
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
		/// Returns the vector (Y, X, W).
		/// </summary>
		public Vector3l YXW
		{
			get
			{
				return new Vector3l(Y, X, W);
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
		/// Returns the vector (Y, Y, W).
		/// </summary>
		public Vector3l YYW
		{
			get
			{
				return new Vector3l(Y, Y, W);
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
		/// Returns the vector (Y, Z, W).
		/// </summary>
		public Vector3l YZW
		{
			get
			{
				return new Vector3l(Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X).
		/// </summary>
		public Vector3l YWX
		{
			get
			{
				return new Vector3l(Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y).
		/// </summary>
		public Vector3l YWY
		{
			get
			{
				return new Vector3l(Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z).
		/// </summary>
		public Vector3l YWZ
		{
			get
			{
				return new Vector3l(Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W).
		/// </summary>
		public Vector3l YWW
		{
			get
			{
				return new Vector3l(Y, W, W);
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
		/// Returns the vector (Z, X, W).
		/// </summary>
		public Vector3l ZXW
		{
			get
			{
				return new Vector3l(Z, X, W);
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
		/// Returns the vector (Z, Y, W).
		/// </summary>
		public Vector3l ZYW
		{
			get
			{
				return new Vector3l(Z, Y, W);
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
		/// Returns the vector (Z, Z, W).
		/// </summary>
		public Vector3l ZZW
		{
			get
			{
				return new Vector3l(Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X).
		/// </summary>
		public Vector3l ZWX
		{
			get
			{
				return new Vector3l(Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y).
		/// </summary>
		public Vector3l ZWY
		{
			get
			{
				return new Vector3l(Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z).
		/// </summary>
		public Vector3l ZWZ
		{
			get
			{
				return new Vector3l(Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W).
		/// </summary>
		public Vector3l ZWW
		{
			get
			{
				return new Vector3l(Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X).
		/// </summary>
		public Vector3l WXX
		{
			get
			{
				return new Vector3l(W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y).
		/// </summary>
		public Vector3l WXY
		{
			get
			{
				return new Vector3l(W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z).
		/// </summary>
		public Vector3l WXZ
		{
			get
			{
				return new Vector3l(W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W).
		/// </summary>
		public Vector3l WXW
		{
			get
			{
				return new Vector3l(W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X).
		/// </summary>
		public Vector3l WYX
		{
			get
			{
				return new Vector3l(W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y).
		/// </summary>
		public Vector3l WYY
		{
			get
			{
				return new Vector3l(W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z).
		/// </summary>
		public Vector3l WYZ
		{
			get
			{
				return new Vector3l(W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W).
		/// </summary>
		public Vector3l WYW
		{
			get
			{
				return new Vector3l(W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X).
		/// </summary>
		public Vector3l WZX
		{
			get
			{
				return new Vector3l(W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y).
		/// </summary>
		public Vector3l WZY
		{
			get
			{
				return new Vector3l(W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z).
		/// </summary>
		public Vector3l WZZ
		{
			get
			{
				return new Vector3l(W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W).
		/// </summary>
		public Vector3l WZW
		{
			get
			{
				return new Vector3l(W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X).
		/// </summary>
		public Vector3l WWX
		{
			get
			{
				return new Vector3l(W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y).
		/// </summary>
		public Vector3l WWY
		{
			get
			{
				return new Vector3l(W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z).
		/// </summary>
		public Vector3l WWZ
		{
			get
			{
				return new Vector3l(W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W).
		/// </summary>
		public Vector3l WWW
		{
			get
			{
				return new Vector3l(W, W, W);
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
		/// Returns the vector (X, X, X, W).
		/// </summary>
		public Vector4l XXXW
		{
			get
			{
				return new Vector4l(X, X, X, W);
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
		/// Returns the vector (X, X, Y, W).
		/// </summary>
		public Vector4l XXYW
		{
			get
			{
				return new Vector4l(X, X, Y, W);
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
		/// Returns the vector (X, X, Z, W).
		/// </summary>
		public Vector4l XXZW
		{
			get
			{
				return new Vector4l(X, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, X).
		/// </summary>
		public Vector4l XXWX
		{
			get
			{
				return new Vector4l(X, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Y).
		/// </summary>
		public Vector4l XXWY
		{
			get
			{
				return new Vector4l(X, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Z).
		/// </summary>
		public Vector4l XXWZ
		{
			get
			{
				return new Vector4l(X, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, W).
		/// </summary>
		public Vector4l XXWW
		{
			get
			{
				return new Vector4l(X, X, W, W);
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
		/// Returns the vector (X, Y, X, W).
		/// </summary>
		public Vector4l XYXW
		{
			get
			{
				return new Vector4l(X, Y, X, W);
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
		/// Returns the vector (X, Y, Y, W).
		/// </summary>
		public Vector4l XYYW
		{
			get
			{
				return new Vector4l(X, Y, Y, W);
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
		/// Returns the vector (X, Y, Z, W).
		/// </summary>
		public Vector4l XYZW
		{
			get
			{
				return new Vector4l(X, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, X).
		/// </summary>
		public Vector4l XYWX
		{
			get
			{
				return new Vector4l(X, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Y).
		/// </summary>
		public Vector4l XYWY
		{
			get
			{
				return new Vector4l(X, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Z).
		/// </summary>
		public Vector4l XYWZ
		{
			get
			{
				return new Vector4l(X, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, W).
		/// </summary>
		public Vector4l XYWW
		{
			get
			{
				return new Vector4l(X, Y, W, W);
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
		/// Returns the vector (X, Z, X, W).
		/// </summary>
		public Vector4l XZXW
		{
			get
			{
				return new Vector4l(X, Z, X, W);
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
		/// Returns the vector (X, Z, Y, W).
		/// </summary>
		public Vector4l XZYW
		{
			get
			{
				return new Vector4l(X, Z, Y, W);
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
		/// Returns the vector (X, Z, Z, W).
		/// </summary>
		public Vector4l XZZW
		{
			get
			{
				return new Vector4l(X, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, X).
		/// </summary>
		public Vector4l XZWX
		{
			get
			{
				return new Vector4l(X, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Y).
		/// </summary>
		public Vector4l XZWY
		{
			get
			{
				return new Vector4l(X, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Z).
		/// </summary>
		public Vector4l XZWZ
		{
			get
			{
				return new Vector4l(X, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, W).
		/// </summary>
		public Vector4l XZWW
		{
			get
			{
				return new Vector4l(X, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, X).
		/// </summary>
		public Vector4l XWXX
		{
			get
			{
				return new Vector4l(X, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Y).
		/// </summary>
		public Vector4l XWXY
		{
			get
			{
				return new Vector4l(X, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Z).
		/// </summary>
		public Vector4l XWXZ
		{
			get
			{
				return new Vector4l(X, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, W).
		/// </summary>
		public Vector4l XWXW
		{
			get
			{
				return new Vector4l(X, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, X).
		/// </summary>
		public Vector4l XWYX
		{
			get
			{
				return new Vector4l(X, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Y).
		/// </summary>
		public Vector4l XWYY
		{
			get
			{
				return new Vector4l(X, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Z).
		/// </summary>
		public Vector4l XWYZ
		{
			get
			{
				return new Vector4l(X, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, W).
		/// </summary>
		public Vector4l XWYW
		{
			get
			{
				return new Vector4l(X, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, X).
		/// </summary>
		public Vector4l XWZX
		{
			get
			{
				return new Vector4l(X, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Y).
		/// </summary>
		public Vector4l XWZY
		{
			get
			{
				return new Vector4l(X, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Z).
		/// </summary>
		public Vector4l XWZZ
		{
			get
			{
				return new Vector4l(X, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, W).
		/// </summary>
		public Vector4l XWZW
		{
			get
			{
				return new Vector4l(X, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, X).
		/// </summary>
		public Vector4l XWWX
		{
			get
			{
				return new Vector4l(X, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Y).
		/// </summary>
		public Vector4l XWWY
		{
			get
			{
				return new Vector4l(X, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Z).
		/// </summary>
		public Vector4l XWWZ
		{
			get
			{
				return new Vector4l(X, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, W).
		/// </summary>
		public Vector4l XWWW
		{
			get
			{
				return new Vector4l(X, W, W, W);
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
		/// Returns the vector (Y, X, X, W).
		/// </summary>
		public Vector4l YXXW
		{
			get
			{
				return new Vector4l(Y, X, X, W);
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
		/// Returns the vector (Y, X, Y, W).
		/// </summary>
		public Vector4l YXYW
		{
			get
			{
				return new Vector4l(Y, X, Y, W);
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
		/// Returns the vector (Y, X, Z, W).
		/// </summary>
		public Vector4l YXZW
		{
			get
			{
				return new Vector4l(Y, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, X).
		/// </summary>
		public Vector4l YXWX
		{
			get
			{
				return new Vector4l(Y, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Y).
		/// </summary>
		public Vector4l YXWY
		{
			get
			{
				return new Vector4l(Y, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Z).
		/// </summary>
		public Vector4l YXWZ
		{
			get
			{
				return new Vector4l(Y, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, W).
		/// </summary>
		public Vector4l YXWW
		{
			get
			{
				return new Vector4l(Y, X, W, W);
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
		/// Returns the vector (Y, Y, X, W).
		/// </summary>
		public Vector4l YYXW
		{
			get
			{
				return new Vector4l(Y, Y, X, W);
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
		/// Returns the vector (Y, Y, Y, W).
		/// </summary>
		public Vector4l YYYW
		{
			get
			{
				return new Vector4l(Y, Y, Y, W);
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
		/// Returns the vector (Y, Y, Z, W).
		/// </summary>
		public Vector4l YYZW
		{
			get
			{
				return new Vector4l(Y, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, X).
		/// </summary>
		public Vector4l YYWX
		{
			get
			{
				return new Vector4l(Y, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Y).
		/// </summary>
		public Vector4l YYWY
		{
			get
			{
				return new Vector4l(Y, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Z).
		/// </summary>
		public Vector4l YYWZ
		{
			get
			{
				return new Vector4l(Y, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, W).
		/// </summary>
		public Vector4l YYWW
		{
			get
			{
				return new Vector4l(Y, Y, W, W);
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
		/// Returns the vector (Y, Z, X, W).
		/// </summary>
		public Vector4l YZXW
		{
			get
			{
				return new Vector4l(Y, Z, X, W);
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
		/// Returns the vector (Y, Z, Y, W).
		/// </summary>
		public Vector4l YZYW
		{
			get
			{
				return new Vector4l(Y, Z, Y, W);
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
		/// Returns the vector (Y, Z, Z, W).
		/// </summary>
		public Vector4l YZZW
		{
			get
			{
				return new Vector4l(Y, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, X).
		/// </summary>
		public Vector4l YZWX
		{
			get
			{
				return new Vector4l(Y, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Y).
		/// </summary>
		public Vector4l YZWY
		{
			get
			{
				return new Vector4l(Y, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Z).
		/// </summary>
		public Vector4l YZWZ
		{
			get
			{
				return new Vector4l(Y, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, W).
		/// </summary>
		public Vector4l YZWW
		{
			get
			{
				return new Vector4l(Y, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, X).
		/// </summary>
		public Vector4l YWXX
		{
			get
			{
				return new Vector4l(Y, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Y).
		/// </summary>
		public Vector4l YWXY
		{
			get
			{
				return new Vector4l(Y, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Z).
		/// </summary>
		public Vector4l YWXZ
		{
			get
			{
				return new Vector4l(Y, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, W).
		/// </summary>
		public Vector4l YWXW
		{
			get
			{
				return new Vector4l(Y, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, X).
		/// </summary>
		public Vector4l YWYX
		{
			get
			{
				return new Vector4l(Y, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Y).
		/// </summary>
		public Vector4l YWYY
		{
			get
			{
				return new Vector4l(Y, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Z).
		/// </summary>
		public Vector4l YWYZ
		{
			get
			{
				return new Vector4l(Y, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, W).
		/// </summary>
		public Vector4l YWYW
		{
			get
			{
				return new Vector4l(Y, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, X).
		/// </summary>
		public Vector4l YWZX
		{
			get
			{
				return new Vector4l(Y, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Y).
		/// </summary>
		public Vector4l YWZY
		{
			get
			{
				return new Vector4l(Y, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Z).
		/// </summary>
		public Vector4l YWZZ
		{
			get
			{
				return new Vector4l(Y, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, W).
		/// </summary>
		public Vector4l YWZW
		{
			get
			{
				return new Vector4l(Y, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, X).
		/// </summary>
		public Vector4l YWWX
		{
			get
			{
				return new Vector4l(Y, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Y).
		/// </summary>
		public Vector4l YWWY
		{
			get
			{
				return new Vector4l(Y, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Z).
		/// </summary>
		public Vector4l YWWZ
		{
			get
			{
				return new Vector4l(Y, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, W).
		/// </summary>
		public Vector4l YWWW
		{
			get
			{
				return new Vector4l(Y, W, W, W);
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
		/// Returns the vector (Z, X, X, W).
		/// </summary>
		public Vector4l ZXXW
		{
			get
			{
				return new Vector4l(Z, X, X, W);
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
		/// Returns the vector (Z, X, Y, W).
		/// </summary>
		public Vector4l ZXYW
		{
			get
			{
				return new Vector4l(Z, X, Y, W);
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
		/// Returns the vector (Z, X, Z, W).
		/// </summary>
		public Vector4l ZXZW
		{
			get
			{
				return new Vector4l(Z, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, X).
		/// </summary>
		public Vector4l ZXWX
		{
			get
			{
				return new Vector4l(Z, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Y).
		/// </summary>
		public Vector4l ZXWY
		{
			get
			{
				return new Vector4l(Z, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Z).
		/// </summary>
		public Vector4l ZXWZ
		{
			get
			{
				return new Vector4l(Z, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, W).
		/// </summary>
		public Vector4l ZXWW
		{
			get
			{
				return new Vector4l(Z, X, W, W);
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
		/// Returns the vector (Z, Y, X, W).
		/// </summary>
		public Vector4l ZYXW
		{
			get
			{
				return new Vector4l(Z, Y, X, W);
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
		/// Returns the vector (Z, Y, Y, W).
		/// </summary>
		public Vector4l ZYYW
		{
			get
			{
				return new Vector4l(Z, Y, Y, W);
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
		/// Returns the vector (Z, Y, Z, W).
		/// </summary>
		public Vector4l ZYZW
		{
			get
			{
				return new Vector4l(Z, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, X).
		/// </summary>
		public Vector4l ZYWX
		{
			get
			{
				return new Vector4l(Z, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Y).
		/// </summary>
		public Vector4l ZYWY
		{
			get
			{
				return new Vector4l(Z, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Z).
		/// </summary>
		public Vector4l ZYWZ
		{
			get
			{
				return new Vector4l(Z, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, W).
		/// </summary>
		public Vector4l ZYWW
		{
			get
			{
				return new Vector4l(Z, Y, W, W);
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
		/// Returns the vector (Z, Z, X, W).
		/// </summary>
		public Vector4l ZZXW
		{
			get
			{
				return new Vector4l(Z, Z, X, W);
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
		/// Returns the vector (Z, Z, Y, W).
		/// </summary>
		public Vector4l ZZYW
		{
			get
			{
				return new Vector4l(Z, Z, Y, W);
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
		/// <summary>
		/// Returns the vector (Z, Z, Z, W).
		/// </summary>
		public Vector4l ZZZW
		{
			get
			{
				return new Vector4l(Z, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, X).
		/// </summary>
		public Vector4l ZZWX
		{
			get
			{
				return new Vector4l(Z, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Y).
		/// </summary>
		public Vector4l ZZWY
		{
			get
			{
				return new Vector4l(Z, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Z).
		/// </summary>
		public Vector4l ZZWZ
		{
			get
			{
				return new Vector4l(Z, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, W).
		/// </summary>
		public Vector4l ZZWW
		{
			get
			{
				return new Vector4l(Z, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, X).
		/// </summary>
		public Vector4l ZWXX
		{
			get
			{
				return new Vector4l(Z, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Y).
		/// </summary>
		public Vector4l ZWXY
		{
			get
			{
				return new Vector4l(Z, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Z).
		/// </summary>
		public Vector4l ZWXZ
		{
			get
			{
				return new Vector4l(Z, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, W).
		/// </summary>
		public Vector4l ZWXW
		{
			get
			{
				return new Vector4l(Z, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, X).
		/// </summary>
		public Vector4l ZWYX
		{
			get
			{
				return new Vector4l(Z, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Y).
		/// </summary>
		public Vector4l ZWYY
		{
			get
			{
				return new Vector4l(Z, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Z).
		/// </summary>
		public Vector4l ZWYZ
		{
			get
			{
				return new Vector4l(Z, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, W).
		/// </summary>
		public Vector4l ZWYW
		{
			get
			{
				return new Vector4l(Z, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, X).
		/// </summary>
		public Vector4l ZWZX
		{
			get
			{
				return new Vector4l(Z, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Y).
		/// </summary>
		public Vector4l ZWZY
		{
			get
			{
				return new Vector4l(Z, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Z).
		/// </summary>
		public Vector4l ZWZZ
		{
			get
			{
				return new Vector4l(Z, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, W).
		/// </summary>
		public Vector4l ZWZW
		{
			get
			{
				return new Vector4l(Z, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, X).
		/// </summary>
		public Vector4l ZWWX
		{
			get
			{
				return new Vector4l(Z, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Y).
		/// </summary>
		public Vector4l ZWWY
		{
			get
			{
				return new Vector4l(Z, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Z).
		/// </summary>
		public Vector4l ZWWZ
		{
			get
			{
				return new Vector4l(Z, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, W).
		/// </summary>
		public Vector4l ZWWW
		{
			get
			{
				return new Vector4l(Z, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, X).
		/// </summary>
		public Vector4l WXXX
		{
			get
			{
				return new Vector4l(W, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Y).
		/// </summary>
		public Vector4l WXXY
		{
			get
			{
				return new Vector4l(W, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Z).
		/// </summary>
		public Vector4l WXXZ
		{
			get
			{
				return new Vector4l(W, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, W).
		/// </summary>
		public Vector4l WXXW
		{
			get
			{
				return new Vector4l(W, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, X).
		/// </summary>
		public Vector4l WXYX
		{
			get
			{
				return new Vector4l(W, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Y).
		/// </summary>
		public Vector4l WXYY
		{
			get
			{
				return new Vector4l(W, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Z).
		/// </summary>
		public Vector4l WXYZ
		{
			get
			{
				return new Vector4l(W, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, W).
		/// </summary>
		public Vector4l WXYW
		{
			get
			{
				return new Vector4l(W, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, X).
		/// </summary>
		public Vector4l WXZX
		{
			get
			{
				return new Vector4l(W, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Y).
		/// </summary>
		public Vector4l WXZY
		{
			get
			{
				return new Vector4l(W, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Z).
		/// </summary>
		public Vector4l WXZZ
		{
			get
			{
				return new Vector4l(W, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, W).
		/// </summary>
		public Vector4l WXZW
		{
			get
			{
				return new Vector4l(W, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, X).
		/// </summary>
		public Vector4l WXWX
		{
			get
			{
				return new Vector4l(W, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Y).
		/// </summary>
		public Vector4l WXWY
		{
			get
			{
				return new Vector4l(W, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Z).
		/// </summary>
		public Vector4l WXWZ
		{
			get
			{
				return new Vector4l(W, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, W).
		/// </summary>
		public Vector4l WXWW
		{
			get
			{
				return new Vector4l(W, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, X).
		/// </summary>
		public Vector4l WYXX
		{
			get
			{
				return new Vector4l(W, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Y).
		/// </summary>
		public Vector4l WYXY
		{
			get
			{
				return new Vector4l(W, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Z).
		/// </summary>
		public Vector4l WYXZ
		{
			get
			{
				return new Vector4l(W, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, W).
		/// </summary>
		public Vector4l WYXW
		{
			get
			{
				return new Vector4l(W, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, X).
		/// </summary>
		public Vector4l WYYX
		{
			get
			{
				return new Vector4l(W, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Y).
		/// </summary>
		public Vector4l WYYY
		{
			get
			{
				return new Vector4l(W, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Z).
		/// </summary>
		public Vector4l WYYZ
		{
			get
			{
				return new Vector4l(W, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, W).
		/// </summary>
		public Vector4l WYYW
		{
			get
			{
				return new Vector4l(W, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, X).
		/// </summary>
		public Vector4l WYZX
		{
			get
			{
				return new Vector4l(W, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Y).
		/// </summary>
		public Vector4l WYZY
		{
			get
			{
				return new Vector4l(W, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Z).
		/// </summary>
		public Vector4l WYZZ
		{
			get
			{
				return new Vector4l(W, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, W).
		/// </summary>
		public Vector4l WYZW
		{
			get
			{
				return new Vector4l(W, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, X).
		/// </summary>
		public Vector4l WYWX
		{
			get
			{
				return new Vector4l(W, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Y).
		/// </summary>
		public Vector4l WYWY
		{
			get
			{
				return new Vector4l(W, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Z).
		/// </summary>
		public Vector4l WYWZ
		{
			get
			{
				return new Vector4l(W, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, W).
		/// </summary>
		public Vector4l WYWW
		{
			get
			{
				return new Vector4l(W, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, X).
		/// </summary>
		public Vector4l WZXX
		{
			get
			{
				return new Vector4l(W, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Y).
		/// </summary>
		public Vector4l WZXY
		{
			get
			{
				return new Vector4l(W, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Z).
		/// </summary>
		public Vector4l WZXZ
		{
			get
			{
				return new Vector4l(W, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, W).
		/// </summary>
		public Vector4l WZXW
		{
			get
			{
				return new Vector4l(W, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, X).
		/// </summary>
		public Vector4l WZYX
		{
			get
			{
				return new Vector4l(W, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Y).
		/// </summary>
		public Vector4l WZYY
		{
			get
			{
				return new Vector4l(W, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Z).
		/// </summary>
		public Vector4l WZYZ
		{
			get
			{
				return new Vector4l(W, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, W).
		/// </summary>
		public Vector4l WZYW
		{
			get
			{
				return new Vector4l(W, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, X).
		/// </summary>
		public Vector4l WZZX
		{
			get
			{
				return new Vector4l(W, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Y).
		/// </summary>
		public Vector4l WZZY
		{
			get
			{
				return new Vector4l(W, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Z).
		/// </summary>
		public Vector4l WZZZ
		{
			get
			{
				return new Vector4l(W, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, W).
		/// </summary>
		public Vector4l WZZW
		{
			get
			{
				return new Vector4l(W, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, X).
		/// </summary>
		public Vector4l WZWX
		{
			get
			{
				return new Vector4l(W, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Y).
		/// </summary>
		public Vector4l WZWY
		{
			get
			{
				return new Vector4l(W, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Z).
		/// </summary>
		public Vector4l WZWZ
		{
			get
			{
				return new Vector4l(W, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, W).
		/// </summary>
		public Vector4l WZWW
		{
			get
			{
				return new Vector4l(W, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, X).
		/// </summary>
		public Vector4l WWXX
		{
			get
			{
				return new Vector4l(W, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Y).
		/// </summary>
		public Vector4l WWXY
		{
			get
			{
				return new Vector4l(W, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Z).
		/// </summary>
		public Vector4l WWXZ
		{
			get
			{
				return new Vector4l(W, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, W).
		/// </summary>
		public Vector4l WWXW
		{
			get
			{
				return new Vector4l(W, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, X).
		/// </summary>
		public Vector4l WWYX
		{
			get
			{
				return new Vector4l(W, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Y).
		/// </summary>
		public Vector4l WWYY
		{
			get
			{
				return new Vector4l(W, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Z).
		/// </summary>
		public Vector4l WWYZ
		{
			get
			{
				return new Vector4l(W, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, W).
		/// </summary>
		public Vector4l WWYW
		{
			get
			{
				return new Vector4l(W, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, X).
		/// </summary>
		public Vector4l WWZX
		{
			get
			{
				return new Vector4l(W, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Y).
		/// </summary>
		public Vector4l WWZY
		{
			get
			{
				return new Vector4l(W, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Z).
		/// </summary>
		public Vector4l WWZZ
		{
			get
			{
				return new Vector4l(W, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, W).
		/// </summary>
		public Vector4l WWZW
		{
			get
			{
				return new Vector4l(W, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, X).
		/// </summary>
		public Vector4l WWWX
		{
			get
			{
				return new Vector4l(W, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Y).
		/// </summary>
		public Vector4l WWWY
		{
			get
			{
				return new Vector4l(W, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Z).
		/// </summary>
		public Vector4l WWWZ
		{
			get
			{
				return new Vector4l(W, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, W).
		/// </summary>
		public Vector4l WWWW
		{
			get
			{
				return new Vector4l(W, W, W, W);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4l"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector4l(long value)
		{
			X = value;
			Y = value;
			Z = value;
			W = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4l"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X and Y components</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4l(Vector2l value, long z, long w)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4l"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X, Y and Z components</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4l(Vector3l value, long w)
		{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4l"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4l(long x, long y, long z, long w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4l"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector4l(long[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
			W = array[3];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4l"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector4l(long[] array, int offset)
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
		public static Vector4l operator +(Vector4l value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4l operator -(Vector4l value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4l operator +(Vector4l left, Vector4l right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4l operator -(Vector4l left, Vector4l right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4l operator *(Vector4l left, long right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4l operator *(long left, Vector4l right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4l operator /(Vector4l left, long right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector4d value to a Vector4l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4l.</param>
		/// <returns>A Vector4l that has all components equal to value.</returns>
		public static explicit operator Vector4l(Vector4d value)
		{
			return new Vector4l((long)value.X, (long)value.Y, (long)value.Z, (long)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4f value to a Vector4l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4l.</param>
		/// <returns>A Vector4l that has all components equal to value.</returns>
		public static explicit operator Vector4l(Vector4f value)
		{
			return new Vector4l((long)value.X, (long)value.Y, (long)value.Z, (long)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4h value to a Vector4l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4l.</param>
		/// <returns>A Vector4l that has all components equal to value.</returns>
		public static explicit operator Vector4l(Vector4h value)
		{
			return new Vector4l((long)value.X, (long)value.Y, (long)value.Z, (long)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4ul value to a Vector4l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4l.</param>
		/// <returns>A Vector4l that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4l(Vector4ul value)
		{
			return new Vector4l((long)value.X, (long)value.Y, (long)value.Z, (long)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4ui value to a Vector4l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4l.</param>
		/// <returns>A Vector4l that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector4l(Vector4ui value)
		{
			return new Vector4l((long)value.X, (long)value.Y, (long)value.Z, (long)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4i value to a Vector4l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4l.</param>
		/// <returns>A Vector4l that has all components equal to value.</returns>
		public static implicit operator Vector4l(Vector4i value)
		{
			return new Vector4l((long)value.X, (long)value.Y, (long)value.Z, (long)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4us value to a Vector4l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4l.</param>
		/// <returns>A Vector4l that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector4l(Vector4us value)
		{
			return new Vector4l((long)value.X, (long)value.Y, (long)value.Z, (long)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4s value to a Vector4l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4l.</param>
		/// <returns>A Vector4l that has all components equal to value.</returns>
		public static implicit operator Vector4l(Vector4s value)
		{
			return new Vector4l((long)value.X, (long)value.Y, (long)value.Z, (long)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4b value to a Vector4l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4l.</param>
		/// <returns>A Vector4l that has all components equal to value.</returns>
		public static implicit operator Vector4l(Vector4b value)
		{
			return new Vector4l((long)value.X, (long)value.Y, (long)value.Z, (long)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4sb value to a Vector4l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4l.</param>
		/// <returns>A Vector4l that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Vector4l(Vector4sb value)
		{
			return new Vector4l((long)value.X, (long)value.Y, (long)value.Z, (long)value.W);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector4l"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector4l"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector4l"/> object, and its value
		/// is equal to the current <see cref="Vector4l"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector4l) { return Equals((Vector4l)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector4l other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector4l left, Vector4l right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z & left.W == right.W;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector4l left, Vector4l right)
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
		/// Writes the given <see cref="Vector4l"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector4l vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
			writer.Write(vector.Z);
			writer.Write(vector.W);
		}
		/// <summary>
		/// Reads a <see cref="Vector4l"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector4l ReadVector4l(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector4l(reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64());
		}
		#endregion
		#region Pack
		public static long Pack(int xBits, int yBits, int zBits, int wBits, Vector4l vector)
		{
			Contract.Requires(0 <= xBits && xBits <= 64, "xBits must be between 0 and 64 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 64, "yBits must be between 0 and 64 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 64, "zBits must be between 0 and 64 inclusive.");
			Contract.Requires(0 <= wBits && wBits <= 64, "wBits must be between 0 and 64 inclusive.");
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
		public static Vector4l Unpack(int xBits, int yBits, int zBits, int wBits, long bits)
		{
			Contract.Requires(0 <= xBits && xBits <= 64, "xBits must be between 0 and 64 inclusive.");
			Contract.Requires(0 <= yBits && yBits <= 64, "yBits must be between 0 and 64 inclusive.");
			Contract.Requires(0 <= zBits && zBits <= 64, "zBits must be between 0 and 64 inclusive.");
			Contract.Requires(0 <= wBits && wBits <= 64, "wBits must be between 0 and 64 inclusive.");
			Contract.Requires(xBits + yBits + zBits + wBits <= 64);
			ulong x = (ulong)(bits);
			x &= ((1UL << xBits) - 1);
			ulong y = (ulong)(bits) >> (xBits);
			y &= ((1UL << yBits) - 1);
			ulong z = (ulong)(bits) >> (xBits + yBits);
			z &= ((1UL << zBits) - 1);
			ulong w = (ulong)(bits) >> (xBits + yBits + zBits);
			w &= ((1UL << wBits) - 1);
			return new Vector4l((long)x, (long)y, (long)z, (long)w);
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4l Negative(Vector4l value)
		{
			return new Vector4l(-value.X, -value.Y, -value.Z, -value.W);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4l Add(Vector4l left, Vector4l right)
		{
			return new Vector4l(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4l Subtract(Vector4l left, Vector4l right)
		{
			return new Vector4l(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4l Multiply(Vector4l vector, long scalar)
		{
			return new Vector4l(vector.X * scalar, vector.Y * scalar, vector.Z * scalar, vector.W * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4l Divide(Vector4l vector, long scalar)
		{
			return new Vector4l(vector.X / scalar, vector.Y / scalar, vector.Z / scalar, vector.W / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Vector4l left, Vector4l right)
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
		public static long Dot(Vector4l left, Vector4l right)
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
		public static bool All(Vector4l value)
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
		public static bool All(Vector4l value, Predicate<long> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z) && predicate(value.W);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Vector4l value)
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
		public static bool Any(Vector4l value, Predicate<long> predicate)
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
		public static long AbsoluteSquared(Vector4l value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		public static float Absolute(Vector4l value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		public static Vector4f Normalize(Vector4l value)
		{
			var absolute = Absolute(value);
			if(absolute <= float.Epsilon)
			{
				return Vector4l.Zero;
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
		public static Vector4d Transform(Vector4l value, Func<long, double> transformer)
		{
			return new Vector4d(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4f Transform(Vector4l value, Func<long, float> transformer)
		{
			return new Vector4f(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4h Transform(Vector4l value, Func<long, Half> transformer)
		{
			return new Vector4h(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4ul Transform(Vector4l value, Func<long, ulong> transformer)
		{
			return new Vector4ul(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4l Transform(Vector4l value, Func<long, long> transformer)
		{
			return new Vector4l(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4ui Transform(Vector4l value, Func<long, uint> transformer)
		{
			return new Vector4ui(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4i Transform(Vector4l value, Func<long, int> transformer)
		{
			return new Vector4i(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4us Transform(Vector4l value, Func<long, ushort> transformer)
		{
			return new Vector4us(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4s Transform(Vector4l value, Func<long, short> transformer)
		{
			return new Vector4s(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4b Transform(Vector4l value, Func<long, byte> transformer)
		{
			return new Vector4b(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4sb Transform(Vector4l value, Func<long, sbyte> transformer)
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
		public static Vector4l Modulate(Vector4l left, Vector4l right)
		{
			return new Vector4l(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Vector4l Abs(Vector4l value)
		{
			return new Vector4l(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z), Functions.Abs(value.W));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Vector4l Min(Vector4l value1, Vector4l value2)
		{
			return new Vector4l(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z), Functions.Min(value1.W, value2.W));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Vector4l Max(Vector4l value1, Vector4l value2)
		{
			return new Vector4l(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z), Functions.Max(value1.W, value2.W));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		public static Vector4l Clamp(Vector4l value, Vector4l min, Vector4l max)
		{
			return new Vector4l(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z), Functions.Clamp(value.W, min.W, max.W));
		}
		#endregion
	}
}
