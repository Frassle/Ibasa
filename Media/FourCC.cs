using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Media
{
    public struct FourCC : IEquatable<FourCC>, IEquatable<string>, IEquatable<int>
    {
        public static readonly FourCC Zero = new FourCC(0);

        int Code;

        public FourCC(int code)
        {
            Code = code;
        }

        public FourCC(byte a, byte b, byte c, byte d)
        {
            Code = ((a << 0) | (b << 8) | (c << 16) | (d << 24));
        }

        public FourCC(byte[] bytes)
            : this(bytes[0], bytes[1], bytes[2], bytes[3])
        {
        }

        public FourCC(byte[] bytes, int index)
            : this(bytes[index + 0], bytes[index + 1], bytes[index + 2], bytes[index + 3])
        {
        }

        public FourCC(char a, char b, char c, char d)
            : this((byte)a, (byte)b, (byte)c, (byte)d)
        {
        }

        public FourCC(char[] chars)
            : this(chars[0], chars[1], chars[2], chars[3])
        {
        }

        public FourCC(char[] chars, int index)
            : this(chars[index + 0], chars[index + 1], chars[index + 2], chars[index + 3])
        {
        }

        public FourCC(string code)
            : this(code[0], code[1], code[1], code[1])
        {
            Contract.Requires(code != null, "code cannot be null.");
            Contract.Requires(code.Length >= 4, "code must have a length of at least 4.");
        }

        public FourCC(string code, int index)
            : this(code[index + 0], code[index + 1], code[index + 2], code[index + 3])
        {
            Contract.Requires(code != null, "code cannot be null.");
            Contract.Requires(code.Length >= index + 4, "code must have a length of at least index + 4.");
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj is FourCC)
                return Equals((FourCC)obj);
            if (obj is string)
                return Equals((string)obj);
            if (obj is int)
                return Equals((int)obj);
            return false;
        }

        #region FourCC
        public bool Equals(FourCC other)
        {
            return Equals(this, other);
        }

        public static bool Equals(FourCC left, FourCC right)
        {
            return left.Code == right.Code;
        }

        public static bool operator ==(FourCC left, FourCC right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FourCC left, FourCC right)
        {
            return !Equals(left, right);
        }
        #endregion
        #region string
        public bool Equals(string other)
        {
            return Equals(this, other);
        }

        public static bool Equals(FourCC left, string right)
        {
            return left.ToString() == right;
        }

        public static bool Equals(string left, FourCC right)
        {
            return Equals(right, left);
        }

        public static bool operator ==(FourCC left, string right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FourCC left, string right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(string left, FourCC right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(string left, FourCC right)
        {
            return !Equals(left, right);
        }
        #endregion
        #region int
        public bool Equals(int other)
        {
            return Equals(this, other);
        }

        public static bool Equals(FourCC left, int right)
        {
            return left.Code == right;
        }

        public static bool Equals(int left, FourCC right)
        {
            return Equals(right, left);
        }

        public static bool operator ==(FourCC left, int right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FourCC left, int right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(int left, FourCC right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(int left, FourCC right)
        {
            return !Equals(left, right);
        }
        #endregion
        #endregion

        public override string ToString()
        {
            var chars = new char[]
            {
                (char)((Code >> 0) & 0xFF),
                (char)((Code >> 8) & 0xFF),
                (char)((Code >> 16) & 0xFF),
                (char)((Code >> 24) & 0xFF)
            };

            return new string(chars);
        }

        public static explicit operator FourCC(int code)
        {
            return new FourCC(code);
        }

        public static explicit operator int(FourCC fourCC)
        {
            return fourCC.Code;
        }
    }
}
