using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public sealed class Salsa20Expansion : KeyDerivation
    {
        private Salsa20Hash Salsa;

        public int Rounds { get { return Salsa.Rounds; } set { Salsa.Rounds = value; } }

        private byte[] _Key;
        public byte[] Key
        {
            get
            {
                return _Key;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value.Length != 32 || value.Length != 16)
                    throw new ArgumentException("Key must be  16 or 32 bytes.", "value");
                _Key = value;
            }
        }

        private byte[] _Nonce;
        public byte[] Nonce
        {
            get
            {
                return _Nonce;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value.Length != 16)
                    throw new ArgumentException("Nonce must be  16 bytes.", "value");
                _Nonce = value;
            }
        }

        public Salsa20Expansion(byte[] key, byte[] nonce)
        {
            Key = key;
            Nonce = nonce;
            Salsa = new Salsa20Hash();
        }

        public Salsa20Expansion(byte[] key, byte[] nonce, int rounds)
        {
            Key = key;
            Nonce = nonce;
            Salsa = new Salsa20Hash(rounds);
        }

        public override void Reset()
        {
            Salsa.Reset();
        }

        public override byte[] DeriveBytes(int count)
        {
            if (count > 64)
                throw new ArgumentException("Salsa20 can only derive up to 64 bytes.", "count");

            byte[] hash;
            var block = new byte[64];

            if (Key.Length == 16)
            {
                block[0] = 101;
                block[1] = 120;
                block[2] = 112;
                block[3] = 97;
                Array.Copy(Key, 0, block, 4, 16);
                block[20] = 110;
                block[21] = 100;
                block[22] = 32;
                block[23] = 49;
                Array.Copy(Nonce, 0, block, 24, 16);
                block[40] = 54;
                block[41] = 45;
                block[42] = 98;
                block[43] = 121;
                Array.Copy(Key, 0, block, 44, 16);
                block[60] = 116;
                block[61] = 101;
                block[62] = 32;
                block[63] = 107;

                hash = Salsa.ComputeHash(block);
            }
            else
            {
                block[0] = 101;
                block[1] = 120;
                block[2] = 112;
                block[3] = 97;
                Array.Copy(Key, 0, block, 4, 16);
                block[20] = 110;
                block[21] = 100;
                block[22] = 32;
                block[23] = 51;
                Array.Copy(Nonce, 0, block, 24, 16);
                block[40] = 50;
                block[41] = 45;
                block[42] = 98;
                block[43] = 121;
                Array.Copy(Key, 16, block, 44, 16);
                block[60] = 116;
                block[61] = 101;
                block[62] = 32;
                block[63] = 107;

                hash = Salsa.ComputeHash(block);
            }

            var result = new byte[count];
            Array.Copy(hash, result, count);
            return result;
        }
    }
}
