using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public abstract class AsymmetricAlgorithm
    {
        public abstract int BlockSize { get; }
        public abstract int KeySize { get; }

        public abstract void Reset();
        public abstract void Update(ArraySegment<byte> data);
        public abstract byte[] Final();

        public byte[] Encrypt(ArraySegment<byte> data)
        {
            Reset();
            Update(data);
            return Final();
        }

        public byte[] Decrypt(ArraySegment<byte> data)
        {
            Reset();
            Update(data);
            return Final();
        }
    }
}
