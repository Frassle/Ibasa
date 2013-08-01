using Ibasa.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public sealed class SCrypt : KeyDerivation 
    {
        PBKDF2 PBKDF2;
        Salsa20Hash Salsa = new Salsa20Hash(8);

        public SCrypt(byte[] password, byte[] salt, int cost, int blockSize, int parallelization) 
        {
            Password = password;
            Salt = salt;
            Cost = cost;
            BlockSize = blockSize;
            Parallelization = parallelization;
            PBKDF2 = new PBKDF2(new HMAC(new SHA256(), password), password, salt, 1);
            Reset();
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

        private int _Cost;
        public int Cost
        {
            get
            {
                return _Cost;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentException("value less than 1.");
                _Cost = value;
            }
        }

        private int _BlockSize;
        public int BlockSize
        {
            get
            {
                return _BlockSize;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentException("value less than 1.");
                _BlockSize = value;
            }
        }

        private int _Parallelization;
        public int Parallelization
        {
            get
            {
                return _Parallelization;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentException("value less than 1.");
                _Parallelization = value;
            }
        }
        
        public override void Reset()
        {
            
        }

        public override byte[] DeriveBytes(int count)
        {
            PBKDF2.Reset();
            PBKDF2.Password = Password;
            PBKDF2.Salt = Salt;
            PBKDF2.Iterations = 1;

            var B = PBKDF2.DeriveBytes(Parallelization * 128 * BlockSize);
            var X = new byte[128 * BlockSize];
            var Y = new byte[128 * BlockSize];
            var V = new byte[128 * BlockSize * Cost];
            
            for(int i=0; i<Parallelization; ++i)
            {
                ROMix(B.Segment(i * 128 * BlockSize, 128 * BlockSize), X, Y, V);
            }

            PBKDF2.Reset();
            PBKDF2.Salt = B;
            return PBKDF2.DeriveBytes(count);
        }        

        #region BlockMix
        private byte[] BlockX = new byte[64];

        private void BlockMix(ArraySegment<byte> B, byte[] Y)
        {
            ArraySegment<byte>.Copy(B, (2 * BlockSize - 1) * 64, BlockX, 0, 64);

            for (int i = 0; i < 2 * BlockSize; ++i)
            {
                for (int j = 0; j < 64; ++j)
                {
                    BlockX[j] ^= B[i * 64 + j];
                }

                Salsa.ComputeHash(BlockX, BlockX);

                Array.Copy(BlockX, 0, Y, i * 64, 64);
            }

            for (int i = 0; i < BlockSize; ++i)
            {
                ArraySegment<byte>.Copy(Y, (i * 2) * 64, B, i * 64, 64);
            }
            for (int i = 0; i < BlockSize; ++i)
            {
                ArraySegment<byte>.Copy(Y, (i * 2 + 1) * 64, B, (i + BlockSize) * 64, 64);
            }
        }
        #endregion

        #region ROMix
        private void ROMix(ArraySegment<byte> B, byte[] X, byte[] Y, byte[] V)
        {
            B.CopyTo(X, 0);

            for (int i = 0; i < Cost; ++i)
            {
                Array.Copy(X, 0, V, i * (128 * BlockSize), 128 * BlockSize);
                BlockMix(X, Y);
            }

            for (int i = 0; i < Cost; ++i)
            {
                ulong j = BitConverter.ToUInt64(X, (2 * BlockSize - 1) * 64);
                j %= (ulong)Cost;

                for (int k = 0; k < 128 * BlockSize; ++k)
                {
                    X[k] ^= V[j * (128 * (ulong)BlockSize) + (ulong)k];
                }
                BlockMix(X, Y);
            }

            ArraySegment<byte>.Copy(X, B, 128 * BlockSize);
        }
        #endregion
    }
}
