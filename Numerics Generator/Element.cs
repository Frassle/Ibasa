using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Element
    {
        public string Name { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int Index { get; private set; }

        public Element(int row, int column, int index)
        {
            Name = string.Format("M{0}{1}", row + 1, column + 1);
            Row = row;
            Column = column;
            Index = index;
        }

        public static Element[] Elements(int rows, int columns)
        {
            Element[] elements = new Element[rows * columns];
            for (int column = 0; column < columns; ++column)
            {
                for (int row = 0; row < rows; ++row)
                {
                    int index = row + column * rows;
                    elements[index] = new Element(row, column, index);
                }
            }
            return elements;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
