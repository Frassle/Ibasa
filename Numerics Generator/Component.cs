using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Component
    {
        public string Name { get; private set; }
        public int Index { get; private set; }

        public Component(string name, int index)
        {
            Name = name;
            Index = index;
        }

        public static Component[] Components(int dimensions)
        {
            if (dimensions == 1)
                return new Component[] {
                    new Component("X", 0)
                };
            if (dimensions == 2)
                return new Component[] {
                    new Component("X", 0),
                    new Component("Y", 1)
                };
            if (dimensions == 3)
                return new Component[] {
                    new Component("X", 0),
                    new Component("Y", 1),
                    new Component("Z", 2)
                };
            if (dimensions == 4)
                return new Component[] {
                    new Component("X", 0),
                    new Component("Y", 1),
                    new Component("Z", 2),
                    new Component("W", 3)
                };
            if (dimensions == 8)
                return new Component[] {
                    new Component("V0", 0),
                    new Component("V1", 1),
                    new Component("V2", 2),
                    new Component("V3", 3),
                    new Component("V4", 4),
                    new Component("V5", 5),
                    new Component("V6", 6),
                    new Component("V7", 7)
                };
            if (dimensions == 16)
                return new Component[] {
                    new Component("V0", 0),
                    new Component("V1", 1),
                    new Component("V2", 2),
                    new Component("V3", 3),
                    new Component("V4", 4),
                    new Component("V5", 5),
                    new Component("V6", 6),
                    new Component("V7", 7),
                    new Component("V8", 8),
                    new Component("V9", 9),
                    new Component("V10", 10),
                    new Component("V11", 11),
                    new Component("V12", 12),
                    new Component("V13", 13),
                    new Component("V14", 14),
                    new Component("V15", 15)
                };

            else return new Component[0];
        }

        public static string UnitVector(int dimensions, int setComponent)
        {
            var components = new int[dimensions];
            components[setComponent] = 1;
            return "(" + string.Join(", ", components) + ")";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
