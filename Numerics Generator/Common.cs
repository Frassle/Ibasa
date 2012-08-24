using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    static class Common
    {
        public static string Name(this int number)
        {
            switch (number)
            {
                case 0: return "zero";
                case 1: return "one";
                case 2: return "two";
                case 3: return "three";
                case 4: return "four";
                case 5: return "five";
                case 6: return "six";
                case 7: return "seven";
                case 8: return "eight";
                case 9: return "nine";
                case 10: return "ten";
                default:
                    return number.ToString();
            }
        }

        public static string OrderName(this int number)
        {
            switch (number)
            {
                case 0: return "zeroth";
                case 1: return "first";
                case 2: return "second";
                case 3: return "third";
                case 4: return "fourth";
                case 5: return "fifth";
                case 6: return "sixth";
                case 7: return "seventh";
                case 8: return "eighth";
                case 9: return "nineth";
                case 10: return "tenth";
                case 11: return "eleventh";
                case 12: return "twelfth";
                case 13: return "thirteenth";
                case 14: return "fourteenth";
                case 15: return "fifeteenth";
                case 16: return "sixteenth";
                default:
                    return number.ToString();
            }
        }
    }
}
