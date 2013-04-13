using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Collections
{
    /// <summary>
    /// Implements a bloom filter.
    /// </summary>
    public sealed class Bloom
    {
        System.Collections.BitArray Bits;
        int HashFunctions;

        public Bloom(int elements, double falsePositiveRate)
        {
            var size = (int)(-elements * Math.Log(falsePositiveRate) / Math.Pow(Math.Log(2), 2)) / 8;
            HashFunctions = (int)(size * 8 / elements * Math.Log(2));

            Bits = new System.Collections.BitArray(size * 8);
        }

        private uint RotL(uint x, int r)
        {
            return (x << r) | (x >> (32-r));
        }

        private uint MurmurHash3(uint seed, byte[] data)
        {
            var h1 = seed;
            var c1 = (uint)0xcc9e2d51;
            var c2 = (uint)0x1b873593;

            var block_count = data.Length / 4;
            var block_end = block_count * 4;

            uint k1;

            for(int i = -block_count; i < 0; ++i)
            {
                k1 = System.BitConverter.ToUInt32(data, block_end + i);

                k1 = k1 * c1;
                k1 = RotL(k1, 15);
                k1 = k1 * c2;

                h1 = h1 ^ k1;
                h1 = RotL(h1, 13);
                h1 = h1 * 0xe6546b64;
            }

            k1 = 0;

            if ((data.Length & 3) >= 3)
            {
                k1 = k1 ^ (uint)(data[block_end + 2] << 16);
            } 
            if ((data.Length & 3) >= 2)
            {
                k1 = k1 ^ (uint)(data[block_end + 1] << 8);
            } 
            if ((data.Length & 3) >= 1)
            {  
                k1 = k1 ^ data[block_end];
                k1 = k1 * c1;
                k1 = RotL(k1, 15);
                k1 = k1 * c2;
                h1 = h1 ^ k1;
            }
                
            h1 = h1 ^ (uint)data.Length;
            h1 = h1 ^ (h1 >> 16);
            h1 = h1 * 0x85ebca6b;
            h1 = h1 ^ (h1 >> 13);
            h1 = h1 * 0xc2b2ae35;
            h1 = h1 ^ (h1 >> 16);

            return h1;
        }

        private int Hash(int hash, byte[] data)
        {
            return (int)MurmurHash3((uint)hash * 0xFBA4C795, data) % (data.Length * 8);
        }

        public void Add(byte[] data)
        {
            if(data.Length == 1 && data[0] == 0xFF)
                return;

            for (int i = 0; i < HashFunctions; ++i)
            {
                int index = Hash(i, data);
                Bits.Set(index, true);
            }
        }

        public bool Contains(byte[] data)
        {
            if(data.Length == 1 && data[0] == 0xFF)
                return true;

            for(int i = 0; i <HashFunctions; ++i)
            {
                int index = Hash(i, data);

                if(!Bits[i])
                    return false;
            }

            return true;
        }
    }
}
