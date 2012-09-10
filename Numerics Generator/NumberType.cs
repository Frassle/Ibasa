using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class NumberType
    {
        public static readonly NumberType Invalid = new NumberType("invalid", "", "", 0, true, true, false);

        public static readonly NumberType Double = new NumberType("double", "Double", "d", 8, true, false, true);
        public static readonly NumberType Float = new NumberType("float", "Single", "f", 4, true, false, true);
        public static readonly NumberType Half = new NumberType("Half", "Half", "h", 2, true, false, true, Float, Float, Float);

        public static readonly NumberType ULong = new NumberType("ulong", "UInt64", "ul", 8, false, true, false, Double, Invalid);
        public static readonly NumberType Long = new NumberType("long", "Int64", "l", 8, true, false, false, Double);

        public static readonly NumberType UInt = new NumberType("uint", "UInt32", "ui", 4, false, true, false, Double, Long);
        public static readonly NumberType Int = new NumberType("int", "Int32", "i", 4, true, false, false, Double);

        public static readonly NumberType UShort = new NumberType("ushort", "UInt16", "us", 2, false, true, false, Float, Int, Int);
        public static readonly NumberType Short = new NumberType("short", "Int16", "s", 2, true, false, false, Float, Int, Int);

        public static readonly NumberType Byte = new NumberType("byte", "Byte", "b", 1, true, true, false, Float, Int, Int);
        public static readonly NumberType SByte = new NumberType("sbyte", "SByte", "sb", 1, false, false, false, Float, Int, Int);

        public static NumberType[] Types
        {
            get
            {
                return new NumberType[] {
                    NumberType.Double, NumberType.Float, NumberType.Half,
                    NumberType.ULong, NumberType.Long,
                    NumberType.UInt, NumberType.Int,
                    NumberType.UShort, NumberType.Short,
                    NumberType.Byte, NumberType.SByte
                };
            }
        }

        private NumberType(string name, string clrName, string suffix, int size, bool clsCompliant, bool unsigned, bool real, NumberType realType = null, NumberType negativeType = null, NumberType positiveType = null)
	    {
		    Name = name;
            CLRName = clrName;
            Suffix = suffix;
            Size = size;
		    IsCLSCompliant = clsCompliant;
		    IsUnsigned = unsigned;
		    IsReal = real;

            RealType = realType ?? this;
            NegativeType = negativeType ?? this;
		    PositiveType = positiveType ?? this;
	    }

        public bool IsImplicitlyConvertibleTo(NumberType to)
        {
            if (this == to)
                return true;

            if (this == Byte)
                return
                    to == UShort || to == Short ||
                    to == UInt || to == Int ||
                    to == ULong || to == Long ||
                    to == Float || to == Double;

            if (this == SByte)
                return
                    to == Short || to == Int || to == Long ||
                    to == Float || to == Double;


            if (this == UShort)
                return
                    to == UInt || to == Int ||
                    to == ULong || to == Long |
                    to == Float || to == Double;

            if (this == Short)
                return
                    to == Int || to == Long ||
                    to == Float || to == Double;


            if (this == UInt)
                return
                    to == ULong || to == Long ||
                    to == Float || to == Double;

            if (this == Int)
                return
                    to == Long ||
                    to == Float || to == Double;


            if (this == ULong)
                return
                    to == Float || to == Double;

            if (this == Long)
                return
                    to == Float || to == Double;

            if (this == Half)
                return
                    to == Float || to == Double;

            if (this == Float)
                return
                    to == Double;

            return false;
        }

        public string Name { get; private set; }
        public string CLRName { get; private set; }
        public string Suffix { get; private set; }
        public int Size { get; private set; }
        public bool IsCLSCompliant { get; private set; }
        public bool IsUnsigned { get; private set; }
        public bool IsReal { get; private set; }
        public NumberType RealType { get; private set; }
        public NumberType NegativeType { get; private set; }
        public NumberType PositiveType { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
