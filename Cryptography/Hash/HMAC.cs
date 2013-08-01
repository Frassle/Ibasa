using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public sealed class HMAC : KeyedHashAlgorithm
    {
        private byte[] _Key;
        private byte[] _Inner;
        private byte[] _Outer;

        public override byte[] Key
        {
            get
            {
                return _Key;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _Key = value;
                RecomputePadding();
            }    
        }

        private HashAlgorithm _HashAlgorithm;
        public HashAlgorithm HashAlgorithm
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
                RecomputePadding();
            }
        }

        private void RecomputePadding()
        {
            byte[] key = _Key;
            if (_Key.Length > HashAlgorithm.BlockSize)
            {
                key = HashAlgorithm.ComputeHash(_Key);
            }
            if (_Key.Length < HashAlgorithm.BlockSize)
            {
                key = new byte[HashAlgorithm.BlockSize];
                Array.Copy(_Key, key, _Key.Length);
            }

            _Inner = _Inner != null && _Inner.Length == HashAlgorithm.BlockSize
                ? _Inner : new byte[HashAlgorithm.BlockSize];
            _Outer = _Outer != null && _Outer.Length == HashAlgorithm.BlockSize
                ? _Outer : new byte[HashAlgorithm.BlockSize];

            for (int i = 0; i < HashAlgorithm.BlockSize; ++i)
            {
                _Inner[i] = (byte)(0x36 ^ key[i]);
                _Outer[i] = (byte)(0x5C ^ key[i]);
            }
        }

        public HMAC(HashAlgorithm hash, byte[] key)
        {
            if (hash == null)
                throw new ArgumentNullException("hash");
            if (key == null)
                throw new ArgumentNullException("key");

            _HashAlgorithm = hash;
            _Key = key;
            RecomputePadding();
        }

        public override int BlockSize
        {
            get { return HashAlgorithm.BlockSize; }
        }

        public override int HashSize
        {
            get { return HashAlgorithm.HashSize; }
        }

        public override void Reset()
        {
            HashAlgorithm.Reset();
            HashAlgorithm.Update(_Inner);
        }

        public override void Update(ArraySegment<byte> data)
        {
            HashAlgorithm.Update(data);
        }

        public override void Final(ArraySegment<byte> hash)
        {
            HashAlgorithm.Final(hash);

            HashAlgorithm.Reset();
            HashAlgorithm.Update(_Outer);
            HashAlgorithm.Update(hash);
            HashAlgorithm.Final(hash);
            Reset();
        }
    }
}
