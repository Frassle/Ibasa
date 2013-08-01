using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public sealed class Random : Ibasa.Numerics.Random.Generator
    {
        private System.Security.Cryptography.RandomNumberGenerator Generator;

        public Random()
        {
            Generator = System.Security.Cryptography.RandomNumberGenerator.Create();
        }
        
        public override long Max
        {
            get { return long.MaxValue; }
        }

        public override long Next()
        {
            byte[] value = new byte[8];
            Generator.GetBytes(value);
            return (long)(BitConverter.ToUInt64(value, 0) & long.MaxValue);
        }

        public override void NextBytes(byte[] buffer)
        {
            Generator.GetBytes(buffer);
        }
    }
}
