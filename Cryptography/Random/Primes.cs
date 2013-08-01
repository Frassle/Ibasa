using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Ibasa.Cryptography
{
    public static class Primes
    {
        public static BigInteger GetPrime(int bits, Ibasa.Numerics.Random.Generator random)
        {
            if (bits < 2)
                throw new ArgumentException("bits is less than two.", "bits");

            BigInteger pi = 2;

            if (bits > 4)
                pi = pi * 3;
            if (bits > 6)
                pi = pi * 5 * 7;
            if (bits > 8)
                pi = pi * 11 * 13;
            if (bits > 10)
                pi = pi * 17 * 19 * 23 * 29;

            byte[] bytes = new byte[((bits + 7) / 8) + 1];
            var bytemask = (byte)(uint.MaxValue >> (32 - (bits % 8)));
            var intmask = BigInteger.Pow(2, bits) - 1;

            BigInteger prime;
            do
            {
                do
                {
                    random.NextBytes(bytes);
                    bytes[bytes.Length - 1] = 0;
                    bytes[bytes.Length - 2] &= bytemask;
                    prime = new BigInteger(bytes);
                } while (prime.IsEven);
            } while (BigInteger.GreatestCommonDivisor(prime, pi) != 1);

            while (!prime.IsProbablyPrime(100, random))
            {
                prime = (prime + pi) & intmask;
            }

            return prime;
        }

        public static bool IsProbablyPrime(this BigInteger prime, int certainty, Ibasa.Numerics.Random.Generator random)
        {
            if (prime == 2 || prime == 3)
                return true;
            if (prime < 2 || prime % 2 == 0)
                return false;
            if (prime <= long.MaxValue)
                return IsProbablyPrime((long)prime, certainty, random);

            int s = 0;
            var d = prime - 1;
            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            byte[] bytes = new byte[prime.ToByteArray().Length];

            for (int i = 0; i < certainty; ++i)
            {
                BigInteger a;
                do
                {
                    random.NextBytes(bytes);
                    a = new BigInteger(bytes);
                } while (a < 2 || a >= prime - 2);

                BigInteger x = BigInteger.ModPow(a, d, prime);
                if (x != 1 && x != prime - 1)
                {
                    for (int r = 1; r < s; ++r)
                    {
                        x = BigInteger.ModPow(x, 2, prime);
                        if (x == 1)
                            return false;
                        if (x == prime - 1)
                            break;
                    }

                    if (x != prime - 1)
                        return false;                        
                }
            }

            return true;
        }

        public static bool IsProbablyPrime(this long prime, int certainty, Ibasa.Numerics.Random.Generator random)
        {
            if (prime == 2 || prime == 3)
                return true;
            if (prime < 2 || prime % 2 == 0)
                return false;

            int s = 0;
            var d = prime - 1;
            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            for (int i = 0; i < certainty; ++i)
            {
                long a = random.Next(2, prime - 2);

                long x = Ibasa.Numerics.Functions.ModPow(a, d, prime);
                if (x != 1 && x != prime - 1)
                {
                    for (int r = 1; r < s; ++r)
                    {
                        x = Ibasa.Numerics.Functions.ModPow(x, 2, prime);
                        if (x == 1)
                            return false;
                        if (x == prime - 1)
                            break;
                    }

                    if (x != prime - 1)
                        return false;
                }
            }

            return true;
        }
    }
}
