using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public sealed class RSA : AsymmetricAlgorithm
    {
        public override int BlockSize
        {
            get { throw new NotImplementedException(); }
        }

        public override int KeySize
        {
            get { throw new NotImplementedException(); }
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        public override void Update(ArraySegment<byte> data)
        {
            throw new NotImplementedException();
        }

        public override byte[] Final()
        {
            throw new NotImplementedException();
        }
    }
}
