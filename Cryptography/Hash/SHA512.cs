using Ibasa.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public sealed class SHA512 : HashAlgorithm
    {
        static ulong[] K = new ulong[]
        {
            0x428A2F98D728AE22, 0x7137449123EF65CD, 0xB5C0FBCFEC4D3B2F, 0xE9B5DBA58189DBBC,
            0x3956C25BF348B538, 0x59F111F1B605D019, 0x923F82A4AF194F9B, 0xAB1C5ED5DA6D8118,
            0xD807AA98A3030242, 0x12835B0145706FBE, 0x243185BE4EE4B28C, 0x550C7DC3D5FFB4E2,
            0x72BE5D74F27B896F, 0x80DEB1FE3B1696B1, 0x9BDC06A725C71235, 0xC19BF174CF692694,
            0xE49B69C19EF14AD2, 0xEFBE4786384F25E3, 0x0FC19DC68B8CD5B5, 0x240CA1CC77AC9C65,
            0x2DE92C6F592B0275, 0x4A7484AA6EA6E483, 0x5CB0A9DCBD41FBD4, 0x76F988DA831153B5,
            0x983E5152EE66DFAB, 0xA831C66D2DB43210, 0xB00327C898FB213F, 0xBF597FC7BEEF0EE4,
            0xC6E00BF33DA88FC2, 0xD5A79147930AA725, 0x06CA6351E003826F, 0x142929670A0E6E70,
            0x27B70A8546D22FFC, 0x2E1B21385C26C926, 0x4D2C6DFC5AC42AED, 0x53380D139D95B3DF,
            0x650A73548BAF63DE, 0x766A0ABB3C77B2A8, 0x81C2C92E47EDAEE6, 0x92722C851482353B,
            0xA2BFE8A14CF10364, 0xA81A664BBC423001, 0xC24B8B70D0F89791, 0xC76C51A30654BE30,
            0xD192E819D6EF5218, 0xD69906245565A910, 0xF40E35855771202A, 0x106AA07032BBD1B8,
            0x19A4C116B8D2D0C8, 0x1E376C085141AB53, 0x2748774CDF8EEB99, 0x34B0BCB5E19B48A8,
            0x391C0CB3C5C95A63, 0x4ED8AA4AE3418ACB, 0x5B9CCA4F7763E373, 0x682E6FF3D6B2B8A3,
            0x748F82EE5DEFB2FC, 0x78A5636F43172F60, 0x84C87814A1F0AB72, 0x8CC702081A6439EC,
            0x90BEFFFA23631E28, 0xA4506CEBDE82BDE9, 0xBEF9A3F7B2C67915, 0xC67178F2E372532B,
            0xCA273ECEEA26619C, 0xD186B8C721C0C207, 0xEADA7DD6CDE0EB1E, 0xF57D4F7FEE6ED178,
            0x06F067AA72176FBA, 0x0A637DC5A2C898A6, 0x113F9804BEF90DAE, 0x1B710B35131C471B,
            0x28DB77F523047D84, 0x32CAAB7B40C72493, 0x3C9EBE0A15C9BEBC, 0x431D67C49C100D4C,
            0x4CC5D4BECB3E42B6, 0x597F299CFC657E2A, 0x5FCB6FAB3AD6FAEC, 0x6C44198C4A475817,
        };

        ulong Length;
        ulong H0, H1, H2, H3, H4, H5, H6, H7;
        ulong[] W = new ulong[80];
        byte[] Block = new byte[128];
        int Index;

        public SHA512()
        {
            Reset();
        }

        public override int BlockSize
        {
            get { return 1024 / 8; }
        }

        public override int HashSize
        {
            get { return 512 / 8; }
        }

        public override void Reset()
        {
            Length = 0;
            H0 = 0x6A09E667F3BCC908;
            H1 = 0xBB67AE8584CAA73B;
            H2 = 0x3C6EF372FE94F82B;
            H3 = 0xA54FF53A5F1D36F1;
            H4 = 0x510E527FADE682D1;
            H5 = 0x9B05688C2B3E6C1F;
            H6 = 0x1F83D9ABFB41BD6B;
            H7 = 0x5BE0CD19137E2179;
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
                W[i] = BitConverter.ToUInt64(Block, i * 8);
                W[i] = BitConverter.SwapBigEndian(W[i]);
            }

            for (int i = 16; i < 80; ++i)
            {
                var s0 = 
                    Binary.RotateRight(W[i - 15], 1) ^
                    Binary.RotateRight(W[i - 15], 8) ^
                    (W[i - 15] >> 7);

                var s1 =
                    Binary.RotateRight(W[i - 2], 19) ^
                    Binary.RotateRight(W[i - 2], 61) ^
                    (W[i - 2] >> 6);

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

            for (int i = 0; i < 80; ++i)
            {
                var s1 =
                    Binary.RotateRight(e, 14) ^
                    Binary.RotateRight(e, 18) ^
                    Binary.RotateRight(e, 41);
                var ch = (e & f) ^ (~e & g);
                var temp1 = h + s1 + ch + K[i] + W[i];
                var s0 =
                    Binary.RotateRight(a, 28) ^
                    Binary.RotateRight(a, 34) ^
                    Binary.RotateRight(a, 39);
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
            if (Index > 111)
            {
                Block[Index++] = 0x80;
                while (Index < 128)
                {
                    Block[Index++] = 0;
                }

                DoBlock();

                while (Index < 120)
                {
                    Block[Index++] = 0;
                }
            }
            else
            {
                Block[Index++] = 0x80;
                while (Index < 120)
                {
                    Block[Index++] = 0;
                }
            }

            var length = BitConverter.SwapBigEndian(Length * 8);
            BitConverter.GetBytes(Block, 120, length);
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
            BitConverter.GetBytes(hash.Array, hash.Offset + 8, H1);
            BitConverter.GetBytes(hash.Array, hash.Offset + 16, H2);
            BitConverter.GetBytes(hash.Array, hash.Offset + 24, H3);
            BitConverter.GetBytes(hash.Array, hash.Offset + 32, H4);
            BitConverter.GetBytes(hash.Array, hash.Offset + 40, H5);
            BitConverter.GetBytes(hash.Array, hash.Offset + 48, H6);
            BitConverter.GetBytes(hash.Array, hash.Offset + 56, H7);

            Reset();
        }
    }
}
