using Ibasa.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public sealed class SHA256 : HashAlgorithm
    {
        static uint[] K = new uint[]
        {
            0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5, 0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
            0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3, 0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
            0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc, 0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
            0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7, 0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
            0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13, 0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
            0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3, 0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
            0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5, 0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
            0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208, 0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2,
        };

        ulong Length;
        uint H0, H1, H2, H3, H4, H5, H6, H7;
        uint[] W = new uint[80];
        byte[] Block = new byte[64];
        int Index;

        public SHA256()
        {
            Reset();
        }

        public override int BlockSize
        {
            get { return 512 / 8; }
        }

        public override int HashSize
        {
            get { return 256 / 8; }
        }

        public override void Reset()
        {
            Length = 0;
            H0 = 0x6A09E667;
            H1 = 0xBB67AE85;
            H2 = 0x3C6EF372;
            H3 = 0xA54FF53A;
            H4 = 0x510E527F;
            H5 = 0x9B05688C;
            H6 = 0x1F83D9AB;
            H7 = 0x5BE0CD19;
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
                var s0 = 
                    Binary.RotateRight(W[i - 15], 7) ^
                    Binary.RotateRight(W[i - 15], 18) ^
                    (W[i - 15] >> 3);

                var s1 =
                    Binary.RotateRight(W[i - 2], 17) ^
                    Binary.RotateRight(W[i - 2], 19) ^
                    (W[i - 2] >> 10);

                W[i] = W[i - 16] + s0 + W[i - 7] + s1;
            }

            var a = H0;
            var b = H1;
            var c = H2;
            var d = H3;
            var e = H4;
            var f = H5;
            var g = H6;
            var h = H7;

            for (int i = 0; i < 64; ++i)
            {
                var s1 =
                    Binary.RotateRight(e, 6) ^
                    Binary.RotateRight(e, 11) ^
                    Binary.RotateRight(e, 25);
                var ch = (e & f) ^ (~e & g);
                var temp1 = h + s1 + ch + K[i] + W[i];
                var s0 =
                    Binary.RotateRight(a, 2) ^
                    Binary.RotateRight(a, 13) ^
                    Binary.RotateRight(a, 22);
                var maj = (a & b) ^ (a & c) ^ (b & c);
                var temp2 = s0 + maj;

                h = g;
                g = f;
                f = e;
                e = d + temp1;
                d = c;
                c = b;
                b = a;
                a = temp1 + temp2;
            }

            H0 += a;
            H1 += b;
            H2 += c;
            H3 += d;
            H4 += e;
            H5 += f;
            H6 += g;
            H7 += h;
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
            H5 = BitConverter.SwapBigEndian(H5);
            H6 = BitConverter.SwapBigEndian(H6);
            H7 = BitConverter.SwapBigEndian(H7);

            BitConverter.GetBytes(hash.Array, hash.Offset + 0, H0);
            BitConverter.GetBytes(hash.Array, hash.Offset + 4, H1);
            BitConverter.GetBytes(hash.Array, hash.Offset + 8, H2);
            BitConverter.GetBytes(hash.Array, hash.Offset + 12, H3);
            BitConverter.GetBytes(hash.Array, hash.Offset + 16, H4);
            BitConverter.GetBytes(hash.Array, hash.Offset + 20, H5);
            BitConverter.GetBytes(hash.Array, hash.Offset + 24, H6);
            BitConverter.GetBytes(hash.Array, hash.Offset + 28, H7);

            Reset();
        }
    }
}
