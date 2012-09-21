using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Numerics
{
    public static class Morton
    {
        // From http://fgiesen.wordpress.com/2009/12/13/decoding-morton-codes/

        private static uint Part1By1(uint x)
        {
            x &= 0x0000ffff;                 // x = ---- ---- ---- ---- fedc ba98 7654 3210
            x = (x ^ (x << 8)) & 0x00ff00ff; // x = ---- ---- fedc ba98 ---- ---- 7654 3210
            x = (x ^ (x << 4)) & 0x0f0f0f0f; // x = ---- fedc ---- ba98 ---- 7654 ---- 3210
            x = (x ^ (x << 2)) & 0x33333333; // x = --fe --dc --ba --98 --76 --54 --32 --10
            x = (x ^ (x << 1)) & 0x55555555; // x = -f-e -d-c -b-a -9-8 -7-6 -5-4 -3-2 -1-0
            return x;
        }
        
        private static uint Part1By2(uint x)
        {
            x &= 0x000003ff;                  // x = ---- ---- ---- ---- ---- --98 7654 3210
            x = (x ^ (x << 16)) & 0xff0000ff; // x = ---- --98 ---- ---- ---- ---- 7654 3210
            x = (x ^ (x << 8)) & 0x0300f00f;  // x = ---- --98 ---- ---- 7654 ---- ---- 3210
            x = (x ^ (x << 4)) & 0x030c30c3;  // x = ---- --98 ---- 76-- --54 ---- 32-- --10
            x = (x ^ (x << 2)) & 0x09249249;  // x = ---- 9--8 --7- -6-- 5--4 --3- -2-- 1--0
            return x;
        }

        private static ulong Part1By1(ulong x)
        {
            x &= 0x00000000ffffffff;                  // x = ---- ---- ---- ---- ---- ---- ---- ---- vuts rqpo nmlk jihg fedc ba98 7654 3210
            x = (x ^ (x << 16)) & 0x0000ffff0000ffff; // x = ---- ---- ---- ---- vuts rqpo nmlk jihg ---- ---- ---- ---- fedc ba98 7654 3210
            x = (x ^ (x << 8)) & 0x00ff00ff00ff00ff;  // x = ---- ---- vuts rqpo ---- ---- nmlk jihg ---- ---- fedc ba98 ---- ---- 7654 3210
            x = (x ^ (x << 4)) & 0x0f0f0f0f0f0f0f0f;  // x = ---- vuts ---- rqpo ---- nmlk ---- jihg ---- fedc ---- ba98 ---- 7654 ---- 3210
            x = (x ^ (x << 2)) & 0x3333333333333333;  // x = --vu --ts --rq --po --nm --lk --ji --hg --fe --dc --ba --98 --76 --54 --32 --10
            x = (x ^ (x << 1)) & 0x5555555555555555;  // x = -v-u -t-s -r-q -p-o -n-m -l-k -j-i -h-g -f-e -d-c -b-a -9-8 -7-6 -5-4 -3-2 -1-0
            return x;
        }

        private static ulong Part1By2(ulong x)
        {
            x = 0x00000000001fffff;                    // x = ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---k jihg fedc ba98 7654 3210
            x = (x ^ (x << 32)) & 0xffff00000000ffff;  // x = ---- ---- ---k jihg ---- ---- ---- ---- ---- ---- ---- ---- fedc ba98 7654 3210
            x = (x ^ (x << 16)) & 0xffff0000ff0000ff;  // x = ---- ---- ---k jihg ---- ---- ---- ---- fedc ba98 ---- ---- ---- ---- 7654 3210
            x = (x ^ (x << 8)) & 0xf00f00f00f00f00f;   // x = ---k ---- ---- jihg ---- ---- fedc ---- ---- ba98 ---- ---- 7654 ---- ---- 3210
            x = (x ^ (x << 4)) & 0x10c30c30c30c30c3;   // x = ---k ---- ji-- --hg ---- fe-- --dc ---- ba-- --98 ---- 76-- --54 ---- 32-- --10
            x = (x ^ (x << 2)) & 0x1249249249249249;   // x = ---k --j- -i-- h--g --f- -e-- d--c --b- -a-- 9--8 --7- -6-- 5--4 --3- -2-- 1--0
            return x;
        }

        public static int Encode(int x, int y)
        {
            Contract.Requires(0 <= x && x < ushort.MaxValue);
            Contract.Requires(0 <= y && y < ushort.MaxValue);

            return (int)((Part1By1((uint)y) << 1) + Part1By1((uint)x));
        }

        public static int Encode(int x, int y, int z)
        {
            Contract.Requires(0 <= x && x < 1024); // 1023 = low 10 bits set
            Contract.Requires(0 <= y && y < 1024);
            Contract.Requires(0 <= z && z < 1024);

            return (int)((Part1By2((uint)z) << 2) + (Part1By2((uint)y) << 1) + Part1By2((uint)x));
        }


        public static long Encode(long x, long y)
        {
            Contract.Requires(0 <= x && x < uint.MaxValue);
            Contract.Requires(0 <= y && y < uint.MaxValue);

            return (long)((Part1By1((ulong)y) << 1) + Part1By1((ulong)x));
        }

        public static long Encode(long x, long y, long z)
        {
            Contract.Requires(0 <= x && x < 2097152); // 2097151 = low 21 bits set
            Contract.Requires(0 <= y && y < 2097152);
            Contract.Requires(0 <= z && z < 2097152);

            return (long)((Part1By2((ulong)z) << 2) + (Part1By2((ulong)y) << 1) + Part1By2((ulong)x));
        }

        private static uint Compact1By1(uint x)
        {
            x &= 0x55555555;                 // x = -f-e -d-c -b-a -9-8 -7-6 -5-4 -3-2 -1-0
            x = (x ^ (x >> 1)) & 0x33333333; // x = --fe --dc --ba --98 --76 --54 --32 --10
            x = (x ^ (x >> 2)) & 0x0f0f0f0f; // x = ---- fedc ---- ba98 ---- 7654 ---- 3210
            x = (x ^ (x >> 4)) & 0x00ff00ff; // x = ---- ---- fedc ba98 ---- ---- 7654 3210
            x = (x ^ (x >> 8)) & 0x0000ffff; // x = ---- ---- ---- ---- fedc ba98 7654 3210
            return x;
        }

        // Inverse of Part1By2 - "delete" all bits not at positions divisible by 3
        private static uint Compact1By2(uint x)
        {
            x &= 0x09249249;                  // x = ---- 9--8 --7- -6-- 5--4 --3- -2-- 1--0
            x = (x ^ (x >> 2)) & 0x030c30c3;  // x = ---- --98 ---- 76-- --54 ---- 32-- --10
            x = (x ^ (x >> 4)) & 0x0300f00f;  // x = ---- --98 ---- ---- 7654 ---- ---- 3210
            x = (x ^ (x >> 8)) & 0xff0000ff;  // x = ---- --98 ---- ---- ---- ---- 7654 3210
            x = (x ^ (x >> 16)) & 0x000003ff; // x = ---- ---- ---- ---- ---- --98 7654 3210
            return x;
        }

        public static Pair<int,int> DecodeMorton2(int code)
        {
            return new Pair<int, int>(
                (int)Compact1By1((uint)code >> 0),
                (int)Compact1By1((uint)code >> 1));
        }

        public static Triple<int, int, int> DecodeMorton3(int code)
        {
            return new Triple<int, int, int>(
                (int)Compact1By2((uint)code >> 0),
                (int)Compact1By2((uint)code >> 1),
                (int)Compact1By2((uint)code >> 2));
        }
    }
}
