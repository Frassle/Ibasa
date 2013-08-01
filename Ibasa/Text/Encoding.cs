using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Text
{
    public class BaseEncoding : System.Text.Encoding
    {
        public static BaseEncoding Base64 { get { return new BaseEncoding("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_"); } }
        public static BaseEncoding Base32 { get { return new BaseEncoding("ABCDEFGHIJKLMNOPQRSTUVWXYZ234567"); } }
        public static BaseEncoding Base16 { get { return new BaseEncoding("0123456789ABCDEF"); } }
        public static BaseEncoding Base10 { get { return new BaseEncoding("0123456789"); } }
        public static BaseEncoding Base8 { get { return new BaseEncoding("01234567"); } }
        public static BaseEncoding Base2 { get { return new BaseEncoding("01"); } }

        public Ibasa.Collections.Immutable.ImmutableArray<char> Alphabet { get; private set; }
        public int Base { get { return Alphabet.Count; } }

        public BaseEncoding(IEnumerable<char> alphabet)
        {
            Alphabet = new Collections.Immutable.ImmutableArray<char>(alphabet);

            if (Alphabet.Count == 1)
            {
                throw new Exception("Require at least two characters in the alphabet.");
            }
        }

        public override int GetByteCount(char[] chars, int index, int count)
        {
            return GetMaxByteCount(count);
        }

        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            var num = new System.Numerics.BigInteger();

            for (int i = 0; i < charCount; ++i)
            {
                num *= Base;
                var index = Alphabet.IndexOf(chars[charIndex + i]);
                if (index == -1)
                {
                    throw new Exception("Illegal character " + chars[charIndex + i] + " at " + (charIndex + i));
                }
                num += index;
            }

            var numbytes = num.ToByteArray();
            var byteCount = GetMaxByteCount(charCount);
            Array.Copy(numbytes, 0, bytes, byteIndex, Math.Min(byteCount, numbytes.Length));

            return byteCount;
        }

        public override int GetCharCount(byte[] bytes, int index, int count)
        {
            return GetMaxCharCount(count);
        }

        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            var charCount = GetMaxCharCount(byteCount);

            var numbytes = new byte[byteCount + 1];
            Array.Copy(bytes, byteIndex, numbytes, 0, byteCount);
            var num = new System.Numerics.BigInteger(numbytes);

            charIndex += charCount;
            for (int i = 0; i < charCount; ++i)
            {
                System.Numerics.BigInteger rem;
                num = System.Numerics.BigInteger.DivRem(num, Base, out rem);
                chars[--charIndex] = Alphabet[(int)rem];
            }

            return charCount;
        }

        public override int GetMaxByteCount(int charCount)
        {
            var bits = Math.Log(Base, 2) * charCount;
            return (int)Math.Ceiling(bits / 8);
        }

        public override int GetMaxCharCount(int byteCount)
        {
            var chars = (byteCount * 8) / Math.Log(Base, 2);
            return (int)Math.Ceiling(chars);
        }
    }
}
