using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public abstract class CryptoTransform
    {
        public abstract void Reset();
        public abstract void Update(ArraySegment<byte> data, ArraySegment<byte> output);
        public abstract void Final(ArraySegment<byte> output);

        public void Transform(ArraySegment<byte> data, ArraySegment<byte> output)
        {
            Reset();
            Update(data, output);
            Final(output);
        }

        public byte[] Transform(ArraySegment<byte> data)
        {
            var output = new byte[0];
            Reset();
            Update(data, output);
            Final(output);
            return output;
        }
    }

    public abstract class SymmetricAlgorithm
    {
        public abstract int InputSize { get; }
        public abstract int OutputSize { get; }

        public abstract CryptoTransform CreateEncryptor();
        public abstract CryptoTransform CreateDecryptor();
        
    }
}
