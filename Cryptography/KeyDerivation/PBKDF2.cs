using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public sealed class PBKDF2 : KeyDerivation
    {
        public PBKDF2(KeyedHashAlgorithm hash, byte[] password, byte[] salt, int iterations)
        {
            HashAlgorithm = hash;
            Password = password;
            Salt = salt;
            Iterations = iterations;
            Reset();
        }

        private KeyedHashAlgorithm _HashAlgorithm;
        public KeyedHashAlgorithm HashAlgorithm
        {
            get
            {
                return _HashAlgorithm;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _HashAlgorithm = value;
            }
        }

        private byte[] _Password;
        public byte[] Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _Password = value;
            }
        }

        private byte[] _Salt;
        public byte[] Salt
        {
            get
            {
                return _Salt;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _Salt = value;
            }
        }

        private int _Iterations;
        public int Iterations
        {
            get
            {
                return _Iterations;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentException("value less than 1.");
                _Iterations = value;
            }
        }

        private byte[] Hash;
        private int Index;
        private int Block;

        public override void Reset()
        {
            Hash = new byte[HashAlgorithm.HashSize];
            Index = 0;
            Block = 1;
        }

        public override byte[] DeriveBytes(int count)
        {
            byte[] result = new byte[count];
            int offset = 0;

            while (offset < count)
            {
                if (Index == 0)
                    Iterate(Block++);

                int copy = Math.Min(Hash.Length - Index, count - offset);
                Array.Copy(Hash, Index, result, offset, copy);
                offset += copy;
                Index = (Index + copy) % HashAlgorithm.HashSize;
            }

            return result;
        }

        private void Iterate(int index)
        {
            var indexbytes = new byte[4];
            BitConverter.GetBytes(indexbytes, 0, index);
            BitConverter.SwapBigEndian(indexbytes);

            HashAlgorithm.Reset();
            HashAlgorithm.Key = Password;
            HashAlgorithm.Update(Salt);
            HashAlgorithm.Update(indexbytes);
            HashAlgorithm.Final(Hash);
            var U = Hash;

            for (int u = 1; u < Iterations; ++u)
            {
                HashAlgorithm.ComputeHash(U, U);

                for (int k = 0; k < Hash.Length; ++k)
                {
                    Hash[k] ^= U[k];
                }
            }
        }
    }
}
