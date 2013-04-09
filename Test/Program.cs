using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {

        static string Type(byte[] bytes)
        {
            return string.Join(", ",
                Enumerable.Reverse(bytes.Select(b => b.ToString("X"))));
        }

        static Ibasa.Numerics.Int128 Big(long i)
        {
            return new Ibasa.Numerics.Int128(i);
        }

        static void Main(string[] args)
        {
            var intA = new Ibasa.Numerics.Int128(32);
            var intB = new Ibasa.Numerics.Int128(-32);
            var intC = new Ibasa.Numerics.UInt128(-32);

            Console.WriteLine(Type(intA.ToByteArray()));
            Console.WriteLine(Type(intB.ToByteArray()));
            Console.WriteLine(Type(intC.ToByteArray()));
            Console.WriteLine(intA);
            Console.WriteLine(intB);
            Console.WriteLine(intC);

            var shiftA = intA >> 2;
            var shiftB = intB >> 2;
            var shiftC = intC >> 2;

            Console.WriteLine(Type(shiftA.ToByteArray()));
            Console.WriteLine(Type(shiftB.ToByteArray()));
            Console.WriteLine(Type(shiftC.ToByteArray()));
            Console.WriteLine(shiftA);
            Console.WriteLine(shiftB);
            Console.WriteLine(shiftC);

            var shift2A = intA >> 48;
            var shift2B = intB >> 48;
            var shift2C = intC >> 48;

            Console.WriteLine(Type(shift2A.ToByteArray()));
            Console.WriteLine(Type(shift2B.ToByteArray()));
            Console.WriteLine(Type(shift2C.ToByteArray()));
            Console.WriteLine(shift2A);
            Console.WriteLine(shift2B);
            Console.WriteLine(shift2C);

            var shift3A = intA >> 256;
            var shift3B = intB >> 256;
            var shift3C = intC >> 256;

            Console.WriteLine(Type(shift3A.ToByteArray()));
            Console.WriteLine(Type(shift3B.ToByteArray()));
            Console.WriteLine(Type(shift3C.ToByteArray()));
            Console.WriteLine(shift3A);
            Console.WriteLine(shift3B);
            Console.WriteLine(shift3C);

            Console.ReadLine();
        }
    }
}
