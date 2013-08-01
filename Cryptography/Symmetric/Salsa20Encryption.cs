using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public sealed class Salsa20Encryption : SymmetricAlgorithm
    {
        private Salsa20Expansion Salsa;

        public byte[] Key { get { return Salsa.Key; } set { Salsa.Key = value; } }

        private byte[] _Nonce;
        public byte[] Nonce
        {
            get
            {
                return (byte[])_Nonce.Clone();
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value.Length != 16)
                    throw new ArgumentException("Nonce must be  8 bytes.", "value");
                _Nonce = value;
            }
        }
        private ulong Counter;
        
        public Salsa20Encryption(byte[] key, byte[] nonce)
        {
            Nonce = nonce;
            var fullnonce = new byte[16];
            Array.Copy(nonce, fullnonce, 8);
            Salsa = new Salsa20Expansion(key, fullnonce);
        }

        public Salsa20Encryption(byte[] key, byte[] nonce, int rounds)
        {
            Nonce = nonce;
            var fullnonce = new byte[16];
            Array.Copy(nonce, fullnonce, 8);
            Salsa = new Salsa20Expansion(key, fullnonce, rounds);
        }

        public override int InputSize
        {
            get { return 1; }
        }

        public override int OutputSize
        {
            get { return 1; }
        }

        public override CryptoTransform CreateDecryptor()
        {
            throw new NotImplementedException();
        }

        public override CryptoTransform CreateEncryptor()
        {
            throw new NotImplementedException();
        }
    }
}
