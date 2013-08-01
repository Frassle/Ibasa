using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public abstract class HashAlgorithm
    {
        public abstract int BlockSize { get; }
        public abstract int HashSize { get; }

        public abstract void Reset();
        public abstract void Update(ArraySegment<byte> data);
        public abstract void Final(ArraySegment<byte> hash);

        public void ComputeHash(ArraySegment<byte> data, ArraySegment<byte> hash)
        {
            Reset();
            Update(data);
            Final(hash);
        }

        public byte[] ComputeHash(ArraySegment<byte> data)
        {
            Reset();
            Update(data);
            byte[] hash = new byte[HashSize];
            Final(hash);
            return hash;
        }
    }

    public abstract class KeyedHashAlgorithm : HashAlgorithm
    {
        public abstract byte[] Key { get; set; }
    }
}
