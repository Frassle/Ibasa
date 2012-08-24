using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    static class Shapes
    {
        public static int[] Sizes { get { return new int[] { 2, 3 }; } }
        public static NumberType[] Types
        {
            get
            {
                return new NumberType[] {
                    NumberType.Double, NumberType.Float,
                    NumberType.Long, NumberType.Int,
                };
            }
        }

        public static Component[] SizeComponents(int dimension)
        {
            if(dimension == 2)
            {
                return new Component[]
                {
                    new Component("Width", 0),
                    new Component("Height", 1)
                };
            } 
            if (dimension == 3)
            {
                return new Component[]
                {
                    new Component("Width", 0),
                    new Component("Height", 1),
                    new Component("Depth", 2)
                };
            }

            throw new ArgumentOutOfRangeException("dimension", "dimension must be 2 or 3");
        }

    }
}
