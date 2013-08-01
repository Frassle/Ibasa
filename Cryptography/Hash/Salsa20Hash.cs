using Ibasa.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public sealed class Salsa20Hash : HashAlgorithm
    {
        int Length;
        private uint[] SalsaX = new uint[16];
        private uint[] SalsaB = new uint[16];

        private int _Rounds;
        public int Rounds
        {
            get
            {
                return _Rounds;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentException("Rounds must be positive.", "value");
                if (Functions.IsOdd(value))
                    throw new ArgumentException("Rounds must be even.", "value");
                _Rounds = value;
            }
        }

        public override int BlockSize
        {
            get { return 64; }
        }

        public override int HashSize
        {
            get { return 64; }
        }

        public Salsa20Hash() : this(20)
        {
        }

        public Salsa20Hash(int rounds)
        {
            Rounds = rounds;
        }

        public override void Reset()
        {
            Length = 0;
        }

        public override void Update(ArraySegment<byte> data)
        {
            foreach (var value in data)
            {
                if (Length == 64)
                {
                    throw new InvalidOperationException("Trying to fed more than 64 bytes into Salsa20.");
                }

                Buffer.SetByte(SalsaB, Length++, value);
            }
        }

        public override void Final(ArraySegment<byte> hash)
        {
            if (Length < 64)
            {
                throw new InvalidOperationException(string.Format("Only {0} bytes fed into Salsa20, needs exactly 64.", Length));
            }
            Salsa(hash);
        }

        private void Salsa(ArraySegment<byte> hash)
        {
            for (int i = 0; i < 16; ++i)
            {
                SalsaX[i] = SalsaB[i] = BitConverter.SwapLittleEndian(SalsaB[i]);
            }
            for (int i = 0; i < Rounds; i += 2)
            {
                SalsaX[4] ^= Binary.RotateLeft(SalsaX[0] + SalsaX[12], 7);
                SalsaX[8] ^= Binary.RotateLeft(SalsaX[4] + SalsaX[0], 9);
                SalsaX[12] ^= Binary.RotateLeft(SalsaX[8] + SalsaX[4], 13);
                SalsaX[0] ^= Binary.RotateLeft(SalsaX[12] + SalsaX[8], 18);
                SalsaX[9] ^= Binary.RotateLeft(SalsaX[5] + SalsaX[1], 7);
                SalsaX[13] ^= Binary.RotateLeft(SalsaX[9] + SalsaX[5], 9);
                SalsaX[1] ^= Binary.RotateLeft(SalsaX[13] + SalsaX[9], 13);
                SalsaX[5] ^= Binary.RotateLeft(SalsaX[1] + SalsaX[13], 18);
                SalsaX[14] ^= Binary.RotateLeft(SalsaX[10] + SalsaX[6], 7);
                SalsaX[2] ^= Binary.RotateLeft(SalsaX[14] + SalsaX[10], 9);
                SalsaX[6] ^= Binary.RotateLeft(SalsaX[2] + SalsaX[14], 13);
                SalsaX[10] ^= Binary.RotateLeft(SalsaX[6] + SalsaX[2], 18);
                SalsaX[3] ^= Binary.RotateLeft(SalsaX[15] + SalsaX[11], 7);
                SalsaX[7] ^= Binary.RotateLeft(SalsaX[3] + SalsaX[15], 9);
                SalsaX[11] ^= Binary.RotateLeft(SalsaX[7] + SalsaX[3], 13);
                SalsaX[15] ^= Binary.RotateLeft(SalsaX[11] + SalsaX[7], 18);

                SalsaX[1] ^= Binary.RotateLeft(SalsaX[0] + SalsaX[3], 7);
                SalsaX[2] ^= Binary.RotateLeft(SalsaX[1] + SalsaX[0], 9);
                SalsaX[3] ^= Binary.RotateLeft(SalsaX[2] + SalsaX[1], 13);
                SalsaX[0] ^= Binary.RotateLeft(SalsaX[3] + SalsaX[2], 18);
                SalsaX[6] ^= Binary.RotateLeft(SalsaX[5] + SalsaX[4], 7);
                SalsaX[7] ^= Binary.RotateLeft(SalsaX[6] + SalsaX[5], 9);
                SalsaX[4] ^= Binary.RotateLeft(SalsaX[7] + SalsaX[6], 13);
                SalsaX[5] ^= Binary.RotateLeft(SalsaX[4] + SalsaX[7], 18);
                SalsaX[11] ^= Binary.RotateLeft(SalsaX[10] + SalsaX[9], 7);
                SalsaX[8] ^= Binary.RotateLeft(SalsaX[11] + SalsaX[10], 9);
                SalsaX[9] ^= Binary.RotateLeft(SalsaX[8] + SalsaX[11], 13);
                SalsaX[10] ^= Binary.RotateLeft(SalsaX[9] + SalsaX[8], 18);
                SalsaX[12] ^= Binary.RotateLeft(SalsaX[15] + SalsaX[14], 7);
                SalsaX[13] ^= Binary.RotateLeft(SalsaX[12] + SalsaX[15], 9);
                SalsaX[14] ^= Binary.RotateLeft(SalsaX[13] + SalsaX[12], 13);
                SalsaX[15] ^= Binary.RotateLeft(SalsaX[14] + SalsaX[13], 18);
            }
            for (int i = 0; i < 16; ++i)
            {
                BitConverter.GetBytes(hash.Array, hash.Offset + i * 4, SalsaB[i] + SalsaX[i]);
            }
        }
    }
}
