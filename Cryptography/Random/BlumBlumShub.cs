using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Ibasa.Cryptography
{
    public sealed class BlumBlumShub : Ibasa.Numerics.Random.Generator
    {
        private BigInteger M;
        private BigInteger State;

        private static BigInteger GetPrime(int bits, Ibasa.Numerics.Random.Generator generator)
        {
            BigInteger prime;
            do
            {
                prime = Primes.GetPrime(bits, generator);
            } while (prime % 4 != 3);
            return prime;
        }

        public BlumBlumShub(int bits, Ibasa.Numerics.Random.Generator generator)
        {
            if (bits < 6)
                throw new ArgumentException("bits is less than 6.", "bits");

            var p = GetPrime(bits / 2, generator);
            var q = GetPrime(bits / 2, generator);

            while (q == p)
            {
                q = GetPrime(bits / 2, generator);
            }

            M = p * q;

            do
            {
                var bytes = new byte[((bits + 7) / 8) + 1];
                generator.NextBytes(bytes);
                bytes[bytes.Length - 1] = 0;
                State = new BigInteger(bytes);
            } while (State == 0 || State == 1 || State % p == 0 || State % q == 0);
            State = BigInteger.ModPow(State, 2, M);
        }

        public override long Max
        {
            get
            {
                return long.MaxValue;
            }
        }

        public override long Next()
        {
            State = BigInteger.ModPow(State, 2, M);
            return (long)(State & long.MaxValue);
        }

        public BigInteger NextBigInteger()
        {
            State = BigInteger.ModPow(State, 2, M);
            return State;
        }
    }
}
