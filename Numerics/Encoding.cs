using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Numerics
{
    public sealed class Encoding
    {
        public Ibasa.Collections.Immutable.ImmutableArray<char> Alphabet { get; private set; }
        public int Base { get { return Alphabet.Count; } }

        public Encoding(char[] alphabet)
        {
            Alphabet = new Collections.Immutable.ImmutableArray<char>(alphabet);
        }

        public string Encode(byte[] data)
        {
            byte[] temp = new byte[data.Length + 1];
            for (int i = 1; i <= data.Length; ++i)
            {
                temp[i - 1] = data[data.Length - i];
            }

            var num = new System.Numerics.BigInteger(temp);

            var sb = new StringBuilder(data.Length);

            while(num > 0)
            {
                System.Numerics.BigInteger rem;
                num = System.Numerics.BigInteger.DivRem(num, 58, out rem);
                sb.Append(Alphabet[(int)rem]);
            }
            
            // Leading zeroes encoded as alphabet zeros
            for(int i=0; i<data.Length && data[i] == 0; ++i)
            {
                sb.Append(Alphabet[0]);
            }

            // Convert little endian string to big endian
            for (int i = 0; i < sb.Length / 2; ++i)
            {
                var t = sb[i];
                sb[i] = sb[sb.Length - i - 1];
                sb[sb.Length - i - 1] = t;
            }

            return sb.ToString();
        }

        public byte[] Decode(string code)
        {
            var num = System.Numerics.BigInteger.Zero;

            for (var i = code.Length - 1; i >= 0; --i)
            {
                var index = Alphabet.IndexOf(code[i]);
                if (index == -1)
                {
                    throw new Exception("Illegal character " + code[i] + " at " + i);
                }
                num *= Base;
                num += index;
            }
            
            // Get bignum as little endian data
            var bytes = num.ToByteArray();
            
            // Trim off sign byte if present
            int signbyte = 
                bytes.Length >= 2 && bytes[bytes.Length - 1] == 0 && bytes[bytes.Length - 2] >= 0x80 ?
                1 : 0;
            
            // Restore leading zeros
            int leadingZeros = 0;
            for(int i=0; i<code.Length && code[i] == Alphabet[0]; ++i)
            {
                ++leadingZeros;
            }

            // Convert little endian data to big endian
            var result = new byte[bytes.Length + leadingZeros - signbyte];            
            for (int i = 0; i < bytes.Length - signbyte; ++i)
            {
                result[leadingZeros + i] = bytes[bytes.Length - i - 1];
            }

            return result;
        }
    }
}
