using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public sealed class SHA1 : HashAlgorithm
    {
        ulong Length;
        uint H0, H1, H2, H3, H4;
        uint[] W = new uint[80];
        byte[] Block = new byte[64];
        int Index;

        public SHA1()
        {
            Reset();
        }

        public override int BlockSize
        {
            get { return 512 / 8; }
        }

        public override int HashSize
        {
            get { return 160 / 8; }
        }

        public override void Reset()
        {
            Length = 0;
            H0 = 0x67452301;
            H1 = 0xEFCDAB89;
            H2 = 0x98BADCFE;
            H3 = 0x10325476;
            H4 = 0xC3D2E1F0;
            Array.Clear(W, 0, 80);
            Index = 0;
            Array.Clear(Block, 0, Block.Length);
        }

        public override void Update(ArraySegment<byte> data)
        {
            foreach (var value in data)
            {
                ++Length;
                Block[Index++] = value;
                if (Index == BlockSize)
                {
                    DoBlock();
                }
            }
        }

        private void DoBlock()
        {
            for (int i = 0; i < 16; ++i)
            {
                W[i] = BitConverter.ToUInt32(Block, i * 4);
                W[i] = BitConverter.SwapBigEndian(W[i]);
            }

            for (int i = 16; i < 80; ++i)
            {
                W[i] = Ibasa.Numerics.Binary.RotateLeft(W[i - 3] ^ W[i - 8] ^ W[i - 14] ^ W[i - 16], 1);
            }

            var a = H0;
            var b = H1;
            var c = H2;
            var d = H3;
            var e = H4;

            for (int i = 0; i < 20; ++i)
            {
                uint f = (b & c) | (~b & d);
                uint k = 0x5A827999;

                var temp = Ibasa.Numerics.Binary.RotateLeft(a, 5) + f + e + k + W[i];
                e = d;
                d = c;
                c = Ibasa.Numerics.Binary.RotateLeft(b, 30);
                b = a;
                a = temp;
            }
            for (int i = 20; i < 40; ++i)
            {
                uint f = b ^ c ^ d;
                uint k = 0x6ED9EBA1;

                var temp = Ibasa.Numerics.Binary.RotateLeft(a, 5) + f + e + k + W[i];
                e = d;
                d = c;
                c = Ibasa.Numerics.Binary.RotateLeft(b, 30);
                b = a;
                a = temp;
            }
            for (int i = 40; i < 60; ++i)
            {
                uint f = (b & c) | (b & d) | (c & d);
                uint k = 0x8F1BBCDC;

                var temp = Ibasa.Numerics.Binary.RotateLeft(a, 5) + f + e + k + W[i];
                e = d;
                d = c;
                c = Ibasa.Numerics.Binary.RotateLeft(b, 30);
                b = a;
                a = temp;
            }
            for (int i = 60; i < 80; ++i)
            {
                uint f = b ^ c ^ d;
                uint k = 0xCA62C1D6;

                var temp = Ibasa.Numerics.Binary.RotateLeft(a, 5) + f + e + k + W[i];
                e = d;
                d = c;
                c = Ibasa.Numerics.Binary.RotateLeft(b, 30);
                b = a;
                a = temp;
            }

            H0 += a;
            H1 += b;
            H2 += c;
            H3 += d;
            H4 += e;
            Index = 0;
        }

        public override void Final(ArraySegment<byte> hash)
        {
            if (Index > 55)
            {
                Block[Index++] = 0x80;
                while (Index < 64)
                {
                    Block[Index++] = 0;
                }

                DoBlock();

                while (Index < 56)
                {
                    Block[Index++] = 0;
                }
            }
            else
            {
                Block[Index++] = 0x80;
                while (Index < 56)
                {
                    Block[Index++] = 0;
                }
            }

            var length = BitConverter.SwapBigEndian(Length * 8);
            BitConverter.GetBytes(Block, 56, length);
            DoBlock();

            H0 = BitConverter.SwapBigEndian(H0);
            H1 = BitConverter.SwapBigEndian(H1);
            H2 = BitConverter.SwapBigEndian(H2);
            H3 = BitConverter.SwapBigEndian(H3);
            H4 = BitConverter.SwapBigEndian(H4);

            BitConverter.GetBytes(hash.Array, hash.Offset + 0, H0);
            BitConverter.GetBytes(hash.Array, hash.Offset + 4, H1);
            BitConverter.GetBytes(hash.Array, hash.Offset + 8, H2);
            BitConverter.GetBytes(hash.Array, hash.Offset + 12, H3);
            BitConverter.GetBytes(hash.Array, hash.Offset + 16, H4);

            Reset();
        }
    }
}
