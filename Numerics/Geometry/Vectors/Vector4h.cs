using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ibasa.Numerics.Geometry
{
	/// <summary>
	/// Represents a four component vector of Halfs, of the form (X, Y, Z, W).
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector4h: IEquatable<Vector4h>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Returns a new <see cref="Vector4h"/> with all of its components equal to zero.
		/// </summary>
		public static readonly Vector4h Zero = new Vector4h(0);
		/// <summary>
		/// Returns a new <see cref="Vector4h"/> with all of its components equal to one.
		/// </summary>
		public static readonly Vector4h One = new Vector4h(1);
		/// <summary>
		/// Returns the X unit <see cref="Vector4h"/> (1, 0, 0, 0).
		/// </summary>
		public static readonly Vector4h UnitX = new Vector4h(1, 0, 0, 0);
		/// <summary>
		/// Returns the Y unit <see cref="Vector4h"/> (0, 1, 0, 0).
		/// </summary>
		public static readonly Vector4h UnitY = new Vector4h(0, 1, 0, 0);
		/// <summary>
		/// Returns the Z unit <see cref="Vector4h"/> (0, 0, 1, 0).
		/// </summary>
		public static readonly Vector4h UnitZ = new Vector4h(0, 0, 1, 0);
		/// <summary>
		/// Returns the W unit <see cref="Vector4h"/> (0, 0, 0, 1).
		/// </summary>
		public static readonly Vector4h UnitW = new Vector4h(0, 0, 0, 1);
		#endregion
		#region Fields
		/// <summary>
		/// The X component of the vector.
		/// </summary>
		public readonly Half X;
		/// <summary>
		/// The Y component of the vector.
		/// </summary>
		public readonly Half Y;
		/// <summary>
		/// The Z component of the vector.
		/// </summary>
		public readonly Half Z;
		/// <summary>
		/// The W component of the vector.
		/// </summary>
		public readonly Half W;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this vector.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public Half this[int index]
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
						throw new IndexOutOfRangeException("Indices for Vector4h run from 0 to 3, inclusive.");
				}
			}
		}
		public Half[] ToArray()
		{
			return new Half[]
			{
				X, Y, Z, W
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the vector (X, X).
		/// </summary>
		public Vector2h XX
		{
			get
			{
				return new Vector2h(X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y).
		/// </summary>
		public Vector2h XY
		{
			get
			{
				return new Vector2h(X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z).
		/// </summary>
		public Vector2h XZ
		{
			get
			{
				return new Vector2h(X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W).
		/// </summary>
		public Vector2h XW
		{
			get
			{
				return new Vector2h(X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X).
		/// </summary>
		public Vector2h YX
		{
			get
			{
				return new Vector2h(Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y).
		/// </summary>
		public Vector2h YY
		{
			get
			{
				return new Vector2h(Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z).
		/// </summary>
		public Vector2h YZ
		{
			get
			{
				return new Vector2h(Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W).
		/// </summary>
		public Vector2h YW
		{
			get
			{
				return new Vector2h(Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X).
		/// </summary>
		public Vector2h ZX
		{
			get
			{
				return new Vector2h(Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y).
		/// </summary>
		public Vector2h ZY
		{
			get
			{
				return new Vector2h(Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z).
		/// </summary>
		public Vector2h ZZ
		{
			get
			{
				return new Vector2h(Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W).
		/// </summary>
		public Vector2h ZW
		{
			get
			{
				return new Vector2h(Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X).
		/// </summary>
		public Vector2h WX
		{
			get
			{
				return new Vector2h(W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y).
		/// </summary>
		public Vector2h WY
		{
			get
			{
				return new Vector2h(W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z).
		/// </summary>
		public Vector2h WZ
		{
			get
			{
				return new Vector2h(W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W).
		/// </summary>
		public Vector2h WW
		{
			get
			{
				return new Vector2h(W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X).
		/// </summary>
		public Vector3h XXX
		{
			get
			{
				return new Vector3h(X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y).
		/// </summary>
		public Vector3h XXY
		{
			get
			{
				return new Vector3h(X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z).
		/// </summary>
		public Vector3h XXZ
		{
			get
			{
				return new Vector3h(X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W).
		/// </summary>
		public Vector3h XXW
		{
			get
			{
				return new Vector3h(X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X).
		/// </summary>
		public Vector3h XYX
		{
			get
			{
				return new Vector3h(X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y).
		/// </summary>
		public Vector3h XYY
		{
			get
			{
				return new Vector3h(X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z).
		/// </summary>
		public Vector3h XYZ
		{
			get
			{
				return new Vector3h(X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W).
		/// </summary>
		public Vector3h XYW
		{
			get
			{
				return new Vector3h(X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X).
		/// </summary>
		public Vector3h XZX
		{
			get
			{
				return new Vector3h(X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y).
		/// </summary>
		public Vector3h XZY
		{
			get
			{
				return new Vector3h(X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z).
		/// </summary>
		public Vector3h XZZ
		{
			get
			{
				return new Vector3h(X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W).
		/// </summary>
		public Vector3h XZW
		{
			get
			{
				return new Vector3h(X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X).
		/// </summary>
		public Vector3h XWX
		{
			get
			{
				return new Vector3h(X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y).
		/// </summary>
		public Vector3h XWY
		{
			get
			{
				return new Vector3h(X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z).
		/// </summary>
		public Vector3h XWZ
		{
			get
			{
				return new Vector3h(X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W).
		/// </summary>
		public Vector3h XWW
		{
			get
			{
				return new Vector3h(X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X).
		/// </summary>
		public Vector3h YXX
		{
			get
			{
				return new Vector3h(Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y).
		/// </summary>
		public Vector3h YXY
		{
			get
			{
				return new Vector3h(Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z).
		/// </summary>
		public Vector3h YXZ
		{
			get
			{
				return new Vector3h(Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W).
		/// </summary>
		public Vector3h YXW
		{
			get
			{
				return new Vector3h(Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X).
		/// </summary>
		public Vector3h YYX
		{
			get
			{
				return new Vector3h(Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y).
		/// </summary>
		public Vector3h YYY
		{
			get
			{
				return new Vector3h(Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z).
		/// </summary>
		public Vector3h YYZ
		{
			get
			{
				return new Vector3h(Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W).
		/// </summary>
		public Vector3h YYW
		{
			get
			{
				return new Vector3h(Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X).
		/// </summary>
		public Vector3h YZX
		{
			get
			{
				return new Vector3h(Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y).
		/// </summary>
		public Vector3h YZY
		{
			get
			{
				return new Vector3h(Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z).
		/// </summary>
		public Vector3h YZZ
		{
			get
			{
				return new Vector3h(Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W).
		/// </summary>
		public Vector3h YZW
		{
			get
			{
				return new Vector3h(Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X).
		/// </summary>
		public Vector3h YWX
		{
			get
			{
				return new Vector3h(Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y).
		/// </summary>
		public Vector3h YWY
		{
			get
			{
				return new Vector3h(Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z).
		/// </summary>
		public Vector3h YWZ
		{
			get
			{
				return new Vector3h(Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W).
		/// </summary>
		public Vector3h YWW
		{
			get
			{
				return new Vector3h(Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X).
		/// </summary>
		public Vector3h ZXX
		{
			get
			{
				return new Vector3h(Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y).
		/// </summary>
		public Vector3h ZXY
		{
			get
			{
				return new Vector3h(Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z).
		/// </summary>
		public Vector3h ZXZ
		{
			get
			{
				return new Vector3h(Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W).
		/// </summary>
		public Vector3h ZXW
		{
			get
			{
				return new Vector3h(Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X).
		/// </summary>
		public Vector3h ZYX
		{
			get
			{
				return new Vector3h(Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y).
		/// </summary>
		public Vector3h ZYY
		{
			get
			{
				return new Vector3h(Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z).
		/// </summary>
		public Vector3h ZYZ
		{
			get
			{
				return new Vector3h(Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W).
		/// </summary>
		public Vector3h ZYW
		{
			get
			{
				return new Vector3h(Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X).
		/// </summary>
		public Vector3h ZZX
		{
			get
			{
				return new Vector3h(Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y).
		/// </summary>
		public Vector3h ZZY
		{
			get
			{
				return new Vector3h(Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z).
		/// </summary>
		public Vector3h ZZZ
		{
			get
			{
				return new Vector3h(Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W).
		/// </summary>
		public Vector3h ZZW
		{
			get
			{
				return new Vector3h(Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X).
		/// </summary>
		public Vector3h ZWX
		{
			get
			{
				return new Vector3h(Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y).
		/// </summary>
		public Vector3h ZWY
		{
			get
			{
				return new Vector3h(Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z).
		/// </summary>
		public Vector3h ZWZ
		{
			get
			{
				return new Vector3h(Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W).
		/// </summary>
		public Vector3h ZWW
		{
			get
			{
				return new Vector3h(Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X).
		/// </summary>
		public Vector3h WXX
		{
			get
			{
				return new Vector3h(W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y).
		/// </summary>
		public Vector3h WXY
		{
			get
			{
				return new Vector3h(W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z).
		/// </summary>
		public Vector3h WXZ
		{
			get
			{
				return new Vector3h(W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W).
		/// </summary>
		public Vector3h WXW
		{
			get
			{
				return new Vector3h(W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X).
		/// </summary>
		public Vector3h WYX
		{
			get
			{
				return new Vector3h(W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y).
		/// </summary>
		public Vector3h WYY
		{
			get
			{
				return new Vector3h(W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z).
		/// </summary>
		public Vector3h WYZ
		{
			get
			{
				return new Vector3h(W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W).
		/// </summary>
		public Vector3h WYW
		{
			get
			{
				return new Vector3h(W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X).
		/// </summary>
		public Vector3h WZX
		{
			get
			{
				return new Vector3h(W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y).
		/// </summary>
		public Vector3h WZY
		{
			get
			{
				return new Vector3h(W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z).
		/// </summary>
		public Vector3h WZZ
		{
			get
			{
				return new Vector3h(W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W).
		/// </summary>
		public Vector3h WZW
		{
			get
			{
				return new Vector3h(W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X).
		/// </summary>
		public Vector3h WWX
		{
			get
			{
				return new Vector3h(W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y).
		/// </summary>
		public Vector3h WWY
		{
			get
			{
				return new Vector3h(W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z).
		/// </summary>
		public Vector3h WWZ
		{
			get
			{
				return new Vector3h(W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W).
		/// </summary>
		public Vector3h WWW
		{
			get
			{
				return new Vector3h(W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, X).
		/// </summary>
		public Vector4h XXXX
		{
			get
			{
				return new Vector4h(X, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Y).
		/// </summary>
		public Vector4h XXXY
		{
			get
			{
				return new Vector4h(X, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, Z).
		/// </summary>
		public Vector4h XXXZ
		{
			get
			{
				return new Vector4h(X, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, X, W).
		/// </summary>
		public Vector4h XXXW
		{
			get
			{
				return new Vector4h(X, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, X).
		/// </summary>
		public Vector4h XXYX
		{
			get
			{
				return new Vector4h(X, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Y).
		/// </summary>
		public Vector4h XXYY
		{
			get
			{
				return new Vector4h(X, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, Z).
		/// </summary>
		public Vector4h XXYZ
		{
			get
			{
				return new Vector4h(X, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Y, W).
		/// </summary>
		public Vector4h XXYW
		{
			get
			{
				return new Vector4h(X, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, X).
		/// </summary>
		public Vector4h XXZX
		{
			get
			{
				return new Vector4h(X, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Y).
		/// </summary>
		public Vector4h XXZY
		{
			get
			{
				return new Vector4h(X, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, Z).
		/// </summary>
		public Vector4h XXZZ
		{
			get
			{
				return new Vector4h(X, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, Z, W).
		/// </summary>
		public Vector4h XXZW
		{
			get
			{
				return new Vector4h(X, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, X).
		/// </summary>
		public Vector4h XXWX
		{
			get
			{
				return new Vector4h(X, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Y).
		/// </summary>
		public Vector4h XXWY
		{
			get
			{
				return new Vector4h(X, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, Z).
		/// </summary>
		public Vector4h XXWZ
		{
			get
			{
				return new Vector4h(X, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, X, W, W).
		/// </summary>
		public Vector4h XXWW
		{
			get
			{
				return new Vector4h(X, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, X).
		/// </summary>
		public Vector4h XYXX
		{
			get
			{
				return new Vector4h(X, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Y).
		/// </summary>
		public Vector4h XYXY
		{
			get
			{
				return new Vector4h(X, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, Z).
		/// </summary>
		public Vector4h XYXZ
		{
			get
			{
				return new Vector4h(X, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, X, W).
		/// </summary>
		public Vector4h XYXW
		{
			get
			{
				return new Vector4h(X, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, X).
		/// </summary>
		public Vector4h XYYX
		{
			get
			{
				return new Vector4h(X, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Y).
		/// </summary>
		public Vector4h XYYY
		{
			get
			{
				return new Vector4h(X, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, Z).
		/// </summary>
		public Vector4h XYYZ
		{
			get
			{
				return new Vector4h(X, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Y, W).
		/// </summary>
		public Vector4h XYYW
		{
			get
			{
				return new Vector4h(X, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, X).
		/// </summary>
		public Vector4h XYZX
		{
			get
			{
				return new Vector4h(X, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Y).
		/// </summary>
		public Vector4h XYZY
		{
			get
			{
				return new Vector4h(X, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, Z).
		/// </summary>
		public Vector4h XYZZ
		{
			get
			{
				return new Vector4h(X, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, Z, W).
		/// </summary>
		public Vector4h XYZW
		{
			get
			{
				return new Vector4h(X, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, X).
		/// </summary>
		public Vector4h XYWX
		{
			get
			{
				return new Vector4h(X, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Y).
		/// </summary>
		public Vector4h XYWY
		{
			get
			{
				return new Vector4h(X, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, Z).
		/// </summary>
		public Vector4h XYWZ
		{
			get
			{
				return new Vector4h(X, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Y, W, W).
		/// </summary>
		public Vector4h XYWW
		{
			get
			{
				return new Vector4h(X, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, X).
		/// </summary>
		public Vector4h XZXX
		{
			get
			{
				return new Vector4h(X, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Y).
		/// </summary>
		public Vector4h XZXY
		{
			get
			{
				return new Vector4h(X, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, Z).
		/// </summary>
		public Vector4h XZXZ
		{
			get
			{
				return new Vector4h(X, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, X, W).
		/// </summary>
		public Vector4h XZXW
		{
			get
			{
				return new Vector4h(X, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, X).
		/// </summary>
		public Vector4h XZYX
		{
			get
			{
				return new Vector4h(X, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Y).
		/// </summary>
		public Vector4h XZYY
		{
			get
			{
				return new Vector4h(X, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, Z).
		/// </summary>
		public Vector4h XZYZ
		{
			get
			{
				return new Vector4h(X, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Y, W).
		/// </summary>
		public Vector4h XZYW
		{
			get
			{
				return new Vector4h(X, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, X).
		/// </summary>
		public Vector4h XZZX
		{
			get
			{
				return new Vector4h(X, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Y).
		/// </summary>
		public Vector4h XZZY
		{
			get
			{
				return new Vector4h(X, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, Z).
		/// </summary>
		public Vector4h XZZZ
		{
			get
			{
				return new Vector4h(X, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, Z, W).
		/// </summary>
		public Vector4h XZZW
		{
			get
			{
				return new Vector4h(X, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, X).
		/// </summary>
		public Vector4h XZWX
		{
			get
			{
				return new Vector4h(X, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Y).
		/// </summary>
		public Vector4h XZWY
		{
			get
			{
				return new Vector4h(X, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, Z).
		/// </summary>
		public Vector4h XZWZ
		{
			get
			{
				return new Vector4h(X, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, Z, W, W).
		/// </summary>
		public Vector4h XZWW
		{
			get
			{
				return new Vector4h(X, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, X).
		/// </summary>
		public Vector4h XWXX
		{
			get
			{
				return new Vector4h(X, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Y).
		/// </summary>
		public Vector4h XWXY
		{
			get
			{
				return new Vector4h(X, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, Z).
		/// </summary>
		public Vector4h XWXZ
		{
			get
			{
				return new Vector4h(X, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, X, W).
		/// </summary>
		public Vector4h XWXW
		{
			get
			{
				return new Vector4h(X, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, X).
		/// </summary>
		public Vector4h XWYX
		{
			get
			{
				return new Vector4h(X, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Y).
		/// </summary>
		public Vector4h XWYY
		{
			get
			{
				return new Vector4h(X, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, Z).
		/// </summary>
		public Vector4h XWYZ
		{
			get
			{
				return new Vector4h(X, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Y, W).
		/// </summary>
		public Vector4h XWYW
		{
			get
			{
				return new Vector4h(X, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, X).
		/// </summary>
		public Vector4h XWZX
		{
			get
			{
				return new Vector4h(X, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Y).
		/// </summary>
		public Vector4h XWZY
		{
			get
			{
				return new Vector4h(X, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, Z).
		/// </summary>
		public Vector4h XWZZ
		{
			get
			{
				return new Vector4h(X, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, Z, W).
		/// </summary>
		public Vector4h XWZW
		{
			get
			{
				return new Vector4h(X, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, X).
		/// </summary>
		public Vector4h XWWX
		{
			get
			{
				return new Vector4h(X, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Y).
		/// </summary>
		public Vector4h XWWY
		{
			get
			{
				return new Vector4h(X, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, Z).
		/// </summary>
		public Vector4h XWWZ
		{
			get
			{
				return new Vector4h(X, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (X, W, W, W).
		/// </summary>
		public Vector4h XWWW
		{
			get
			{
				return new Vector4h(X, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, X).
		/// </summary>
		public Vector4h YXXX
		{
			get
			{
				return new Vector4h(Y, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Y).
		/// </summary>
		public Vector4h YXXY
		{
			get
			{
				return new Vector4h(Y, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, Z).
		/// </summary>
		public Vector4h YXXZ
		{
			get
			{
				return new Vector4h(Y, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, X, W).
		/// </summary>
		public Vector4h YXXW
		{
			get
			{
				return new Vector4h(Y, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, X).
		/// </summary>
		public Vector4h YXYX
		{
			get
			{
				return new Vector4h(Y, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Y).
		/// </summary>
		public Vector4h YXYY
		{
			get
			{
				return new Vector4h(Y, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, Z).
		/// </summary>
		public Vector4h YXYZ
		{
			get
			{
				return new Vector4h(Y, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Y, W).
		/// </summary>
		public Vector4h YXYW
		{
			get
			{
				return new Vector4h(Y, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, X).
		/// </summary>
		public Vector4h YXZX
		{
			get
			{
				return new Vector4h(Y, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Y).
		/// </summary>
		public Vector4h YXZY
		{
			get
			{
				return new Vector4h(Y, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, Z).
		/// </summary>
		public Vector4h YXZZ
		{
			get
			{
				return new Vector4h(Y, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, Z, W).
		/// </summary>
		public Vector4h YXZW
		{
			get
			{
				return new Vector4h(Y, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, X).
		/// </summary>
		public Vector4h YXWX
		{
			get
			{
				return new Vector4h(Y, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Y).
		/// </summary>
		public Vector4h YXWY
		{
			get
			{
				return new Vector4h(Y, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, Z).
		/// </summary>
		public Vector4h YXWZ
		{
			get
			{
				return new Vector4h(Y, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, X, W, W).
		/// </summary>
		public Vector4h YXWW
		{
			get
			{
				return new Vector4h(Y, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, X).
		/// </summary>
		public Vector4h YYXX
		{
			get
			{
				return new Vector4h(Y, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Y).
		/// </summary>
		public Vector4h YYXY
		{
			get
			{
				return new Vector4h(Y, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, Z).
		/// </summary>
		public Vector4h YYXZ
		{
			get
			{
				return new Vector4h(Y, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, X, W).
		/// </summary>
		public Vector4h YYXW
		{
			get
			{
				return new Vector4h(Y, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, X).
		/// </summary>
		public Vector4h YYYX
		{
			get
			{
				return new Vector4h(Y, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Y).
		/// </summary>
		public Vector4h YYYY
		{
			get
			{
				return new Vector4h(Y, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, Z).
		/// </summary>
		public Vector4h YYYZ
		{
			get
			{
				return new Vector4h(Y, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Y, W).
		/// </summary>
		public Vector4h YYYW
		{
			get
			{
				return new Vector4h(Y, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, X).
		/// </summary>
		public Vector4h YYZX
		{
			get
			{
				return new Vector4h(Y, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Y).
		/// </summary>
		public Vector4h YYZY
		{
			get
			{
				return new Vector4h(Y, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, Z).
		/// </summary>
		public Vector4h YYZZ
		{
			get
			{
				return new Vector4h(Y, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, Z, W).
		/// </summary>
		public Vector4h YYZW
		{
			get
			{
				return new Vector4h(Y, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, X).
		/// </summary>
		public Vector4h YYWX
		{
			get
			{
				return new Vector4h(Y, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Y).
		/// </summary>
		public Vector4h YYWY
		{
			get
			{
				return new Vector4h(Y, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, Z).
		/// </summary>
		public Vector4h YYWZ
		{
			get
			{
				return new Vector4h(Y, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Y, W, W).
		/// </summary>
		public Vector4h YYWW
		{
			get
			{
				return new Vector4h(Y, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, X).
		/// </summary>
		public Vector4h YZXX
		{
			get
			{
				return new Vector4h(Y, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Y).
		/// </summary>
		public Vector4h YZXY
		{
			get
			{
				return new Vector4h(Y, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, Z).
		/// </summary>
		public Vector4h YZXZ
		{
			get
			{
				return new Vector4h(Y, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, X, W).
		/// </summary>
		public Vector4h YZXW
		{
			get
			{
				return new Vector4h(Y, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, X).
		/// </summary>
		public Vector4h YZYX
		{
			get
			{
				return new Vector4h(Y, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Y).
		/// </summary>
		public Vector4h YZYY
		{
			get
			{
				return new Vector4h(Y, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, Z).
		/// </summary>
		public Vector4h YZYZ
		{
			get
			{
				return new Vector4h(Y, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Y, W).
		/// </summary>
		public Vector4h YZYW
		{
			get
			{
				return new Vector4h(Y, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, X).
		/// </summary>
		public Vector4h YZZX
		{
			get
			{
				return new Vector4h(Y, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Y).
		/// </summary>
		public Vector4h YZZY
		{
			get
			{
				return new Vector4h(Y, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, Z).
		/// </summary>
		public Vector4h YZZZ
		{
			get
			{
				return new Vector4h(Y, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, Z, W).
		/// </summary>
		public Vector4h YZZW
		{
			get
			{
				return new Vector4h(Y, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, X).
		/// </summary>
		public Vector4h YZWX
		{
			get
			{
				return new Vector4h(Y, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Y).
		/// </summary>
		public Vector4h YZWY
		{
			get
			{
				return new Vector4h(Y, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, Z).
		/// </summary>
		public Vector4h YZWZ
		{
			get
			{
				return new Vector4h(Y, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, Z, W, W).
		/// </summary>
		public Vector4h YZWW
		{
			get
			{
				return new Vector4h(Y, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, X).
		/// </summary>
		public Vector4h YWXX
		{
			get
			{
				return new Vector4h(Y, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Y).
		/// </summary>
		public Vector4h YWXY
		{
			get
			{
				return new Vector4h(Y, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, Z).
		/// </summary>
		public Vector4h YWXZ
		{
			get
			{
				return new Vector4h(Y, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, X, W).
		/// </summary>
		public Vector4h YWXW
		{
			get
			{
				return new Vector4h(Y, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, X).
		/// </summary>
		public Vector4h YWYX
		{
			get
			{
				return new Vector4h(Y, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Y).
		/// </summary>
		public Vector4h YWYY
		{
			get
			{
				return new Vector4h(Y, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, Z).
		/// </summary>
		public Vector4h YWYZ
		{
			get
			{
				return new Vector4h(Y, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Y, W).
		/// </summary>
		public Vector4h YWYW
		{
			get
			{
				return new Vector4h(Y, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, X).
		/// </summary>
		public Vector4h YWZX
		{
			get
			{
				return new Vector4h(Y, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Y).
		/// </summary>
		public Vector4h YWZY
		{
			get
			{
				return new Vector4h(Y, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, Z).
		/// </summary>
		public Vector4h YWZZ
		{
			get
			{
				return new Vector4h(Y, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, Z, W).
		/// </summary>
		public Vector4h YWZW
		{
			get
			{
				return new Vector4h(Y, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, X).
		/// </summary>
		public Vector4h YWWX
		{
			get
			{
				return new Vector4h(Y, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Y).
		/// </summary>
		public Vector4h YWWY
		{
			get
			{
				return new Vector4h(Y, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, Z).
		/// </summary>
		public Vector4h YWWZ
		{
			get
			{
				return new Vector4h(Y, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Y, W, W, W).
		/// </summary>
		public Vector4h YWWW
		{
			get
			{
				return new Vector4h(Y, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, X).
		/// </summary>
		public Vector4h ZXXX
		{
			get
			{
				return new Vector4h(Z, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Y).
		/// </summary>
		public Vector4h ZXXY
		{
			get
			{
				return new Vector4h(Z, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, Z).
		/// </summary>
		public Vector4h ZXXZ
		{
			get
			{
				return new Vector4h(Z, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, X, W).
		/// </summary>
		public Vector4h ZXXW
		{
			get
			{
				return new Vector4h(Z, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, X).
		/// </summary>
		public Vector4h ZXYX
		{
			get
			{
				return new Vector4h(Z, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Y).
		/// </summary>
		public Vector4h ZXYY
		{
			get
			{
				return new Vector4h(Z, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, Z).
		/// </summary>
		public Vector4h ZXYZ
		{
			get
			{
				return new Vector4h(Z, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Y, W).
		/// </summary>
		public Vector4h ZXYW
		{
			get
			{
				return new Vector4h(Z, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, X).
		/// </summary>
		public Vector4h ZXZX
		{
			get
			{
				return new Vector4h(Z, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Y).
		/// </summary>
		public Vector4h ZXZY
		{
			get
			{
				return new Vector4h(Z, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, Z).
		/// </summary>
		public Vector4h ZXZZ
		{
			get
			{
				return new Vector4h(Z, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, Z, W).
		/// </summary>
		public Vector4h ZXZW
		{
			get
			{
				return new Vector4h(Z, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, X).
		/// </summary>
		public Vector4h ZXWX
		{
			get
			{
				return new Vector4h(Z, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Y).
		/// </summary>
		public Vector4h ZXWY
		{
			get
			{
				return new Vector4h(Z, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, Z).
		/// </summary>
		public Vector4h ZXWZ
		{
			get
			{
				return new Vector4h(Z, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, X, W, W).
		/// </summary>
		public Vector4h ZXWW
		{
			get
			{
				return new Vector4h(Z, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, X).
		/// </summary>
		public Vector4h ZYXX
		{
			get
			{
				return new Vector4h(Z, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Y).
		/// </summary>
		public Vector4h ZYXY
		{
			get
			{
				return new Vector4h(Z, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, Z).
		/// </summary>
		public Vector4h ZYXZ
		{
			get
			{
				return new Vector4h(Z, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, X, W).
		/// </summary>
		public Vector4h ZYXW
		{
			get
			{
				return new Vector4h(Z, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, X).
		/// </summary>
		public Vector4h ZYYX
		{
			get
			{
				return new Vector4h(Z, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Y).
		/// </summary>
		public Vector4h ZYYY
		{
			get
			{
				return new Vector4h(Z, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, Z).
		/// </summary>
		public Vector4h ZYYZ
		{
			get
			{
				return new Vector4h(Z, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Y, W).
		/// </summary>
		public Vector4h ZYYW
		{
			get
			{
				return new Vector4h(Z, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, X).
		/// </summary>
		public Vector4h ZYZX
		{
			get
			{
				return new Vector4h(Z, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Y).
		/// </summary>
		public Vector4h ZYZY
		{
			get
			{
				return new Vector4h(Z, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, Z).
		/// </summary>
		public Vector4h ZYZZ
		{
			get
			{
				return new Vector4h(Z, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, Z, W).
		/// </summary>
		public Vector4h ZYZW
		{
			get
			{
				return new Vector4h(Z, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, X).
		/// </summary>
		public Vector4h ZYWX
		{
			get
			{
				return new Vector4h(Z, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Y).
		/// </summary>
		public Vector4h ZYWY
		{
			get
			{
				return new Vector4h(Z, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, Z).
		/// </summary>
		public Vector4h ZYWZ
		{
			get
			{
				return new Vector4h(Z, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Y, W, W).
		/// </summary>
		public Vector4h ZYWW
		{
			get
			{
				return new Vector4h(Z, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, X).
		/// </summary>
		public Vector4h ZZXX
		{
			get
			{
				return new Vector4h(Z, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Y).
		/// </summary>
		public Vector4h ZZXY
		{
			get
			{
				return new Vector4h(Z, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, Z).
		/// </summary>
		public Vector4h ZZXZ
		{
			get
			{
				return new Vector4h(Z, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, X, W).
		/// </summary>
		public Vector4h ZZXW
		{
			get
			{
				return new Vector4h(Z, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, X).
		/// </summary>
		public Vector4h ZZYX
		{
			get
			{
				return new Vector4h(Z, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Y).
		/// </summary>
		public Vector4h ZZYY
		{
			get
			{
				return new Vector4h(Z, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, Z).
		/// </summary>
		public Vector4h ZZYZ
		{
			get
			{
				return new Vector4h(Z, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Y, W).
		/// </summary>
		public Vector4h ZZYW
		{
			get
			{
				return new Vector4h(Z, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, X).
		/// </summary>
		public Vector4h ZZZX
		{
			get
			{
				return new Vector4h(Z, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Y).
		/// </summary>
		public Vector4h ZZZY
		{
			get
			{
				return new Vector4h(Z, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, Z).
		/// </summary>
		public Vector4h ZZZZ
		{
			get
			{
				return new Vector4h(Z, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, Z, W).
		/// </summary>
		public Vector4h ZZZW
		{
			get
			{
				return new Vector4h(Z, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, X).
		/// </summary>
		public Vector4h ZZWX
		{
			get
			{
				return new Vector4h(Z, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Y).
		/// </summary>
		public Vector4h ZZWY
		{
			get
			{
				return new Vector4h(Z, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, Z).
		/// </summary>
		public Vector4h ZZWZ
		{
			get
			{
				return new Vector4h(Z, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, Z, W, W).
		/// </summary>
		public Vector4h ZZWW
		{
			get
			{
				return new Vector4h(Z, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, X).
		/// </summary>
		public Vector4h ZWXX
		{
			get
			{
				return new Vector4h(Z, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Y).
		/// </summary>
		public Vector4h ZWXY
		{
			get
			{
				return new Vector4h(Z, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, Z).
		/// </summary>
		public Vector4h ZWXZ
		{
			get
			{
				return new Vector4h(Z, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, X, W).
		/// </summary>
		public Vector4h ZWXW
		{
			get
			{
				return new Vector4h(Z, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, X).
		/// </summary>
		public Vector4h ZWYX
		{
			get
			{
				return new Vector4h(Z, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Y).
		/// </summary>
		public Vector4h ZWYY
		{
			get
			{
				return new Vector4h(Z, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, Z).
		/// </summary>
		public Vector4h ZWYZ
		{
			get
			{
				return new Vector4h(Z, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Y, W).
		/// </summary>
		public Vector4h ZWYW
		{
			get
			{
				return new Vector4h(Z, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, X).
		/// </summary>
		public Vector4h ZWZX
		{
			get
			{
				return new Vector4h(Z, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Y).
		/// </summary>
		public Vector4h ZWZY
		{
			get
			{
				return new Vector4h(Z, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, Z).
		/// </summary>
		public Vector4h ZWZZ
		{
			get
			{
				return new Vector4h(Z, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, Z, W).
		/// </summary>
		public Vector4h ZWZW
		{
			get
			{
				return new Vector4h(Z, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, X).
		/// </summary>
		public Vector4h ZWWX
		{
			get
			{
				return new Vector4h(Z, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Y).
		/// </summary>
		public Vector4h ZWWY
		{
			get
			{
				return new Vector4h(Z, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, Z).
		/// </summary>
		public Vector4h ZWWZ
		{
			get
			{
				return new Vector4h(Z, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (Z, W, W, W).
		/// </summary>
		public Vector4h ZWWW
		{
			get
			{
				return new Vector4h(Z, W, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, X).
		/// </summary>
		public Vector4h WXXX
		{
			get
			{
				return new Vector4h(W, X, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Y).
		/// </summary>
		public Vector4h WXXY
		{
			get
			{
				return new Vector4h(W, X, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, Z).
		/// </summary>
		public Vector4h WXXZ
		{
			get
			{
				return new Vector4h(W, X, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, X, W).
		/// </summary>
		public Vector4h WXXW
		{
			get
			{
				return new Vector4h(W, X, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, X).
		/// </summary>
		public Vector4h WXYX
		{
			get
			{
				return new Vector4h(W, X, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Y).
		/// </summary>
		public Vector4h WXYY
		{
			get
			{
				return new Vector4h(W, X, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, Z).
		/// </summary>
		public Vector4h WXYZ
		{
			get
			{
				return new Vector4h(W, X, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Y, W).
		/// </summary>
		public Vector4h WXYW
		{
			get
			{
				return new Vector4h(W, X, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, X).
		/// </summary>
		public Vector4h WXZX
		{
			get
			{
				return new Vector4h(W, X, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Y).
		/// </summary>
		public Vector4h WXZY
		{
			get
			{
				return new Vector4h(W, X, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, Z).
		/// </summary>
		public Vector4h WXZZ
		{
			get
			{
				return new Vector4h(W, X, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, Z, W).
		/// </summary>
		public Vector4h WXZW
		{
			get
			{
				return new Vector4h(W, X, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, X).
		/// </summary>
		public Vector4h WXWX
		{
			get
			{
				return new Vector4h(W, X, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Y).
		/// </summary>
		public Vector4h WXWY
		{
			get
			{
				return new Vector4h(W, X, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, Z).
		/// </summary>
		public Vector4h WXWZ
		{
			get
			{
				return new Vector4h(W, X, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, X, W, W).
		/// </summary>
		public Vector4h WXWW
		{
			get
			{
				return new Vector4h(W, X, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, X).
		/// </summary>
		public Vector4h WYXX
		{
			get
			{
				return new Vector4h(W, Y, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Y).
		/// </summary>
		public Vector4h WYXY
		{
			get
			{
				return new Vector4h(W, Y, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, Z).
		/// </summary>
		public Vector4h WYXZ
		{
			get
			{
				return new Vector4h(W, Y, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, X, W).
		/// </summary>
		public Vector4h WYXW
		{
			get
			{
				return new Vector4h(W, Y, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, X).
		/// </summary>
		public Vector4h WYYX
		{
			get
			{
				return new Vector4h(W, Y, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Y).
		/// </summary>
		public Vector4h WYYY
		{
			get
			{
				return new Vector4h(W, Y, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, Z).
		/// </summary>
		public Vector4h WYYZ
		{
			get
			{
				return new Vector4h(W, Y, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Y, W).
		/// </summary>
		public Vector4h WYYW
		{
			get
			{
				return new Vector4h(W, Y, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, X).
		/// </summary>
		public Vector4h WYZX
		{
			get
			{
				return new Vector4h(W, Y, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Y).
		/// </summary>
		public Vector4h WYZY
		{
			get
			{
				return new Vector4h(W, Y, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, Z).
		/// </summary>
		public Vector4h WYZZ
		{
			get
			{
				return new Vector4h(W, Y, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, Z, W).
		/// </summary>
		public Vector4h WYZW
		{
			get
			{
				return new Vector4h(W, Y, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, X).
		/// </summary>
		public Vector4h WYWX
		{
			get
			{
				return new Vector4h(W, Y, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Y).
		/// </summary>
		public Vector4h WYWY
		{
			get
			{
				return new Vector4h(W, Y, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, Z).
		/// </summary>
		public Vector4h WYWZ
		{
			get
			{
				return new Vector4h(W, Y, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Y, W, W).
		/// </summary>
		public Vector4h WYWW
		{
			get
			{
				return new Vector4h(W, Y, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, X).
		/// </summary>
		public Vector4h WZXX
		{
			get
			{
				return new Vector4h(W, Z, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Y).
		/// </summary>
		public Vector4h WZXY
		{
			get
			{
				return new Vector4h(W, Z, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, Z).
		/// </summary>
		public Vector4h WZXZ
		{
			get
			{
				return new Vector4h(W, Z, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, X, W).
		/// </summary>
		public Vector4h WZXW
		{
			get
			{
				return new Vector4h(W, Z, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, X).
		/// </summary>
		public Vector4h WZYX
		{
			get
			{
				return new Vector4h(W, Z, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Y).
		/// </summary>
		public Vector4h WZYY
		{
			get
			{
				return new Vector4h(W, Z, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, Z).
		/// </summary>
		public Vector4h WZYZ
		{
			get
			{
				return new Vector4h(W, Z, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Y, W).
		/// </summary>
		public Vector4h WZYW
		{
			get
			{
				return new Vector4h(W, Z, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, X).
		/// </summary>
		public Vector4h WZZX
		{
			get
			{
				return new Vector4h(W, Z, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Y).
		/// </summary>
		public Vector4h WZZY
		{
			get
			{
				return new Vector4h(W, Z, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, Z).
		/// </summary>
		public Vector4h WZZZ
		{
			get
			{
				return new Vector4h(W, Z, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, Z, W).
		/// </summary>
		public Vector4h WZZW
		{
			get
			{
				return new Vector4h(W, Z, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, X).
		/// </summary>
		public Vector4h WZWX
		{
			get
			{
				return new Vector4h(W, Z, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Y).
		/// </summary>
		public Vector4h WZWY
		{
			get
			{
				return new Vector4h(W, Z, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, Z).
		/// </summary>
		public Vector4h WZWZ
		{
			get
			{
				return new Vector4h(W, Z, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, Z, W, W).
		/// </summary>
		public Vector4h WZWW
		{
			get
			{
				return new Vector4h(W, Z, W, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, X).
		/// </summary>
		public Vector4h WWXX
		{
			get
			{
				return new Vector4h(W, W, X, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Y).
		/// </summary>
		public Vector4h WWXY
		{
			get
			{
				return new Vector4h(W, W, X, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, Z).
		/// </summary>
		public Vector4h WWXZ
		{
			get
			{
				return new Vector4h(W, W, X, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, X, W).
		/// </summary>
		public Vector4h WWXW
		{
			get
			{
				return new Vector4h(W, W, X, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, X).
		/// </summary>
		public Vector4h WWYX
		{
			get
			{
				return new Vector4h(W, W, Y, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Y).
		/// </summary>
		public Vector4h WWYY
		{
			get
			{
				return new Vector4h(W, W, Y, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, Z).
		/// </summary>
		public Vector4h WWYZ
		{
			get
			{
				return new Vector4h(W, W, Y, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Y, W).
		/// </summary>
		public Vector4h WWYW
		{
			get
			{
				return new Vector4h(W, W, Y, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, X).
		/// </summary>
		public Vector4h WWZX
		{
			get
			{
				return new Vector4h(W, W, Z, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Y).
		/// </summary>
		public Vector4h WWZY
		{
			get
			{
				return new Vector4h(W, W, Z, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, Z).
		/// </summary>
		public Vector4h WWZZ
		{
			get
			{
				return new Vector4h(W, W, Z, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, Z, W).
		/// </summary>
		public Vector4h WWZW
		{
			get
			{
				return new Vector4h(W, W, Z, W);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, X).
		/// </summary>
		public Vector4h WWWX
		{
			get
			{
				return new Vector4h(W, W, W, X);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Y).
		/// </summary>
		public Vector4h WWWY
		{
			get
			{
				return new Vector4h(W, W, W, Y);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, Z).
		/// </summary>
		public Vector4h WWWZ
		{
			get
			{
				return new Vector4h(W, W, W, Z);
			}
		}
		/// <summary>
		/// Returns the vector (W, W, W, W).
		/// </summary>
		public Vector4h WWWW
		{
			get
			{
				return new Vector4h(W, W, W, W);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4h"/> using the specified value.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Vector4h(Half value)
		{
			X = value;
			Y = value;
			Z = value;
			W = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4h"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X and Y components</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4h(Vector2h value, Half z, Half w)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4h"/> using the specified vector and values.
		/// </summary>
		/// <param name="value">A vector containing the values with which to initialize the X, Y and Z components</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4h(Vector3h value, Half w)
		{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4h"/> using the specified values.
		/// </summary>
		/// <param name="x">Value for the X component of the vector.</param>
		/// <param name="y">Value for the Y component of the vector.</param>
		/// <param name="z">Value for the Z component of the vector.</param>
		/// <param name="w">Value for the W component of the vector.</param>
		public Vector4h(Half x, Half y, Half z, Half w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4h"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Vector4h(Half[] array)
		{
			if (array.Length < 2)
				throw new ArgumentException("Not enough elements in array.", "array");
			X = array[0];
			Y = array[1];
			Z = array[2];
			W = array[3];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector4h"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Vector4h(Half[] array, int offset)
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
		public static Vector4f operator +(Vector4h value)
		{
			return value;
		}
		/// <summary>
		/// Returns the additive inverse of a specified vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4f operator -(Vector4h value)
		{
			return Vector.Negative(value);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4f operator +(Vector4h left, Vector4h right)
		{
			return Vector.Add(left, right);
		}
		/// <summary>
		/// Subtracts one vector from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4f operator -(Vector4h left, Vector4h right)
		{
			return Vector.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4f operator *(Vector4h left, float right)
		{
			return Vector.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and vector.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The vector to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4f operator *(float left, Vector4h right)
		{
			return Vector.Multiply(right, left);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The vector to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4f operator /(Vector4h left, float right)
		{
			return Vector.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Vector4d value to a Vector4h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4h.</param>
		/// <returns>A Vector4h that has all components equal to value.</returns>
		public static explicit operator Vector4h(Vector4d value)
		{
			return new Vector4h((Half)value.X, (Half)value.Y, (Half)value.Z, (Half)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4f value to a Vector4h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4h.</param>
		/// <returns>A Vector4h that has all components equal to value.</returns>
		public static explicit operator Vector4h(Vector4f value)
		{
			return new Vector4h((Half)value.X, (Half)value.Y, (Half)value.Z, (Half)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4ul value to a Vector4h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4h.</param>
		/// <returns>A Vector4h that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4h(Vector4ul value)
		{
			return new Vector4h((Half)value.X, (Half)value.Y, (Half)value.Z, (Half)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4l value to a Vector4h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4h.</param>
		/// <returns>A Vector4h that has all components equal to value.</returns>
		public static explicit operator Vector4h(Vector4l value)
		{
			return new Vector4h((Half)value.X, (Half)value.Y, (Half)value.Z, (Half)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4ui value to a Vector4h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4h.</param>
		/// <returns>A Vector4h that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4h(Vector4ui value)
		{
			return new Vector4h((Half)value.X, (Half)value.Y, (Half)value.Z, (Half)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4i value to a Vector4h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4h.</param>
		/// <returns>A Vector4h that has all components equal to value.</returns>
		public static explicit operator Vector4h(Vector4i value)
		{
			return new Vector4h((Half)value.X, (Half)value.Y, (Half)value.Z, (Half)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4us value to a Vector4h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4h.</param>
		/// <returns>A Vector4h that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4h(Vector4us value)
		{
			return new Vector4h((Half)value.X, (Half)value.Y, (Half)value.Z, (Half)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4s value to a Vector4h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4h.</param>
		/// <returns>A Vector4h that has all components equal to value.</returns>
		public static explicit operator Vector4h(Vector4s value)
		{
			return new Vector4h((Half)value.X, (Half)value.Y, (Half)value.Z, (Half)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4b value to a Vector4h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4h.</param>
		/// <returns>A Vector4h that has all components equal to value.</returns>
		public static explicit operator Vector4h(Vector4b value)
		{
			return new Vector4h((Half)value.X, (Half)value.Y, (Half)value.Z, (Half)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4sb value to a Vector4h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4h.</param>
		/// <returns>A Vector4h that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4h(Vector4sb value)
		{
			return new Vector4h((Half)value.X, (Half)value.Y, (Half)value.Z, (Half)value.W);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Vector4h"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Vector4h"/> object or a type capable
		/// of implicit conversion to a <see cref="Vector4h"/> object, and its value
		/// is equal to the current <see cref="Vector4h"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector4h) { return Equals((Vector4h)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Vector4h other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Vector4h left, Vector4h right)
		{
			return left.X == right.X & left.Y == right.Y & left.Z == right.Z & left.W == right.W;
		}
		/// <summary>
		/// Returns a value that indicates whether two vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Vector4h left, Vector4h right)
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
		/// Writes the given <see cref="Vector4h"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Vector4h vector)
		{
			writer.Write(vector.X);
			writer.Write(vector.Y);
			writer.Write(vector.Z);
			writer.Write(vector.W);
		}
		/// <summary>
		/// Reads a <see cref="Vector4h"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Vector4h ReadVector4h(this Ibasa.IO.BinaryReader reader)
		{
			return new Vector4h(reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf(), reader.ReadHalf());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the additive inverse of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The negative of value.</returns>
		public static Vector4f Negative(Vector4h value)
		{
			return new Vector4f(-value.X, -value.Y, -value.Z, -value.W);
		}
		/// <summary>
		/// Adds two vectors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Vector4f Add(Vector4h left, Vector4h right)
		{
			return new Vector4f(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}
		/// <summary>
		/// Subtracts one vectors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Vector4f Subtract(Vector4h left, Vector4h right)
		{
			return new Vector4f(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}
		/// <summary>
		/// Returns the product of a vector and scalar.
		/// </summary>
		/// <param name="vector">The vector to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Vector4f Multiply(Vector4h vector, float scalar)
		{
			return new Vector4f(vector.X * scalar, vector.Y * scalar, vector.Z * scalar, vector.W * scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar and returns the result.
		/// </summary>
		/// <param name="vector">The vector to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Vector4f Divide(Vector4h vector, float scalar)
		{
			return new Vector4f(vector.X / scalar, vector.Y / scalar, vector.Z / scalar, vector.W / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Vector4h left, Vector4h right)
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
		public static float Dot(Vector4h left, Vector4h right)
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
		public static bool All(Vector4h value)
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
		public static bool All(Vector4h value, Predicate<Half> predicate)
		{
			return predicate(value.X) && predicate(value.Y) && predicate(value.Z) && predicate(value.W);
		}
		/// <summary>
		/// Determines whether any component of a vector is non-zero.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Vector4h value)
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
		public static bool Any(Vector4h value, Predicate<Half> predicate)
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
		public static float AbsoluteSquared(Vector4h value)
		{
			return Dot(value, value);
		}
		/// <summary>
		/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value of value.</returns>
		public static float Absolute(Vector4h value)
		{
			return Functions.Sqrt(AbsoluteSquared(value));
		}
		/// <summary>
		/// Computes the normalized value (or unit) of a vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The normalized value of value.</returns>
		public static Vector4f Normalize(Vector4h value)
		{
			return (Vector4f)value / Absolute(value);
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
		public static Vector4d Transform(Vector4h value, Func<Half, double> transformer)
		{
			return new Vector4d(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4f Transform(Vector4h value, Func<Half, float> transformer)
		{
			return new Vector4f(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4h Transform(Vector4h value, Func<Half, Half> transformer)
		{
			return new Vector4h(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4ul Transform(Vector4h value, Func<Half, ulong> transformer)
		{
			return new Vector4ul(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4l Transform(Vector4h value, Func<Half, long> transformer)
		{
			return new Vector4l(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4ui Transform(Vector4h value, Func<Half, uint> transformer)
		{
			return new Vector4ui(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4i Transform(Vector4h value, Func<Half, int> transformer)
		{
			return new Vector4i(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4us Transform(Vector4h value, Func<Half, ushort> transformer)
		{
			return new Vector4us(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4s Transform(Vector4h value, Func<Half, short> transformer)
		{
			return new Vector4s(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4b Transform(Vector4h value, Func<Half, byte> transformer)
		{
			return new Vector4b(transformer(value.X), transformer(value.Y), transformer(value.Z), transformer(value.W));
		}
		/// <summary>
		/// Transforms the components of a vector and returns the result.
		/// </summary>
		/// <param name="value">The vector to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Vector4sb Transform(Vector4h value, Func<Half, sbyte> transformer)
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
		public static Vector4f Modulate(Vector4h left, Vector4h right)
		{
			return new Vector4f(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Vector4h Abs(Vector4h value)
		{
			return new Vector4h(Functions.Abs(value.X), Functions.Abs(value.Y), Functions.Abs(value.Z), Functions.Abs(value.W));
		}
		/// <summary>
		/// Returns a vector that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Vector4h Min(Vector4h value1, Vector4h value2)
		{
			return new Vector4h(Functions.Min(value1.X, value2.X), Functions.Min(value1.Y, value2.Y), Functions.Min(value1.Z, value2.Z), Functions.Min(value1.W, value2.W));
		}
		/// <summary>
		/// Returns a vector that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Vector4h Max(Vector4h value1, Vector4h value2)
		{
			return new Vector4h(Functions.Max(value1.X, value2.X), Functions.Max(value1.Y, value2.Y), Functions.Max(value1.Z, value2.Z), Functions.Max(value1.W, value2.W));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A vector to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A vector with each component constrained to the given range.</returns>
		public static Vector4h Clamp(Vector4h value, Vector4h min, Vector4h max)
		{
			return new Vector4h(Functions.Clamp(value.X, min.X, max.X), Functions.Clamp(value.Y, min.Y, max.Y), Functions.Clamp(value.Z, min.Z, max.Z), Functions.Clamp(value.W, min.W, max.W));
		}
		/// <summary>
		/// Constrains each component to the range 0 to 1.
		/// </summary>
		/// <param name="value">A vector to saturate.</param>
		/// <returns>A vector with each component constrained to the range 0 to 1.</returns>
		public static Vector4h Saturate(Vector4h value)
		{
			return new Vector4h(Functions.Saturate(value.X), Functions.Saturate(value.Y), Functions.Saturate(value.Z), Functions.Saturate(value.W));
		}
		/// <summary>
		/// Returns a vector where each component is the smallest integral value that
		/// is greater than or equal to the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The ceiling of value.</returns>
		public static Vector4f Ceiling(Vector4h value)
		{
			return new Vector4f(Functions.Ceiling(value.X), Functions.Ceiling(value.Y), Functions.Ceiling(value.Z), Functions.Ceiling(value.W));
		}
		/// <summary>
		/// Returns a vector where each component is the largest integral value that
		/// is less than or equal to the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The floor of value.</returns>
		public static Vector4f Floor(Vector4h value)
		{
			return new Vector4f(Functions.Floor(value.X), Functions.Floor(value.Y), Functions.Floor(value.Z), Functions.Floor(value.W));
		}
		/// <summary>
		/// Returns a vector where each component is the integral part of the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The integral of value.</returns>
		public static Vector4f Truncate(Vector4h value)
		{
			return new Vector4f(Functions.Truncate(value.X), Functions.Truncate(value.Y), Functions.Truncate(value.Z), Functions.Truncate(value.W));
		}
		/// <summary>
		/// Returns a vector where each component is the fractional part of the specified component.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The fractional of value.</returns>
		public static Vector4f Fractional(Vector4h value)
		{
			return new Vector4f(Functions.Fractional(value.X), Functions.Fractional(value.Y), Functions.Fractional(value.Z), Functions.Fractional(value.W));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector4f Round(Vector4h value)
		{
			return new Vector4f(Functions.Round(value.X), Functions.Round(value.Y), Functions.Round(value.Z), Functions.Round(value.W));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector4f Round(Vector4h value, int digits)
		{
			return new Vector4f(Functions.Round(value.X, digits), Functions.Round(value.Y, digits), Functions.Round(value.Z, digits), Functions.Round(value.W, digits));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector4f Round(Vector4h value, MidpointRounding mode)
		{
			return new Vector4f(Functions.Round(value.X, mode), Functions.Round(value.Y, mode), Functions.Round(value.Z, mode), Functions.Round(value.W, mode));
		}
		/// <summary>
		/// Returns a vector where each component is rounded to the nearest integral value.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
		/// <returns>The result of rounding value.</returns>
		public static Vector4f Round(Vector4h value, int digits, MidpointRounding mode)
		{
			return new Vector4f(Functions.Round(value.X, digits, mode), Functions.Round(value.Y, digits, mode), Functions.Round(value.Z, digits, mode), Functions.Round(value.W, digits, mode));
		}
		/// <summary>
		/// Calculates the reciprocal of each component in the vector.
		/// </summary>
		/// <param name="value">A vector.</param>
		/// <returns>A vector with the reciprocal of each of values components.</returns>
		public static Vector4f Reciprocal(Vector4h value)
		{
			return new Vector4f(1 / value.X, 1 / value.Y, 1 / value.Z, 1 / value.W);
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
		public static Vector4f Barycentric(Vector4h value1, Vector4h value2, Vector4h value3, float amount1, float amount2)
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
		public static Vector4f Reflect(Vector4h vector, Vector4h normal)
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
		public static Vector4f Refract(Vector4h vector, Vector4h normal, float index)
		{
			var cos1 = Dot(vector, normal);
			var radicand = 1 - (index * index) * (1 - (cos1 * cos1));
			if (radicand < 0)
			{
				return Vector4f.Zero;
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
		public static Vector4f Lerp(Vector4h value1, Vector4h value2, float amount)
		{
			return (1 - amount) * value1 + amount * value2;
		}
		#endregion
	}
}
