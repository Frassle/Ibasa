using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public abstract class KeyDerivation
    {
        public abstract void Reset();
        public abstract byte[] DeriveBytes(int count);
    }
}
