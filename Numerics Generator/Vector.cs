using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Vector : Generator
    {
        public static int[] Sizes { get { return new int[] { 2, 3, 4, 8, 16 }; } }
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

        public NumberType Type { get; private set; }
        public int Dimension { get; private set; }
        public Component[] Components { get; private set; }

        public Vector(NumberType type, int dimension)
        {
            Type = type;
            Dimension = dimension;
            Components = Component.Components(Dimension);
        }

        public string Name
        {
            get
            {
                return string.Format("Vector{0}{1}", Dimension, Type.Suffix);
            }
        }

        public void Generate()
        {
            Namespace();
        }

        void Namespace()
        {
            WriteLine("using System;");
            WriteLine("using System.Diagnostics.Contracts;");
            WriteLine("using System.Globalization;");
            WriteLine("using System.Runtime.InteropServices;");
            WriteLine("");
            WriteLine("namespace Ibasa.Numerics.Geometry");
            WriteLine("{");
            Indent();
            Declaration();
            Functions();
            Dedent();
            WriteLine("}");
        }

        void Declaration()
        {
            WriteLine("/// <summary>");
            if (Dimension <= 4)
            {
                WriteLine("/// Represents a {0} component vector of {1}s, of the form ({2}).",
                    Dimension.Name(), Type, string.Join(", ", (IEnumerable<Component>)Components));
            }
            else
            {
                WriteLine("/// Represents a {0} component vector of {1}s.", Dimension.Name(), Type);
            }
            WriteLine("/// </summary>");
            WriteLine("[Serializable]");
            WriteLine("[ComVisible(true)]");
            WriteLine("[StructLayout(LayoutKind.Sequential)]");
            if (!Type.IsCLSCompliant)
            {
                WriteLine("[CLSCompliant(false)]");
            }
            WriteLine("public struct {0}: IEquatable<{0}>, IFormattable", Name);
            WriteLine("{");
            Indent();
            Constants();
            Fields();
            Properties();
            Constructors();
            Operations();
            Conversions();
            Equatable();
            String();
            Dedent();
            WriteLine("}");
        }

        void Constants()
        {
            WriteLine("#region Constants");
            
            WriteLine("/// <summary>");
            WriteLine("/// Returns a new <see cref=\"{0}\"/> with all of its components equal to zero.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} Zero = new {0}(0);", Name);

            WriteLine("/// <summary>");
            WriteLine("/// Returns a new <see cref=\"{0}\"/> with all of its components equal to one.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} One = new {0}(1);", Name);

            if (Dimension <= 4)
            {
                foreach (var component in Components)
                {
                    var vector = Component.UnitVector(Dimension, component.Index);

                    WriteLine("/// <summary>");
                    WriteLine("/// Returns the {0} unit <see cref=\"{1}\"/> {2}.", component, Name, vector);
                    WriteLine("/// </summary>");
                    WriteLine("public static readonly {0} Unit{1} = new {0}{2};", Name, component, vector);
                }
            }
            
            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            foreach (var component in Components)
            {
                WriteLine("/// <summary>");
                WriteLine("/// The {0} component of the vector.", Dimension <= 4 ? component.ToString() : Common.OrderName(component.Index + 1));
                WriteLine("/// </summary>");
                WriteLine("public readonly {0} {1};", Type, component);
            }

            WriteLine("#endregion");
        }

        void Properties()
        {
            WriteLine("#region Properties");
            #region Indexer
            WriteLine("/// <summary>");
            WriteLine("/// Returns the indexed component of this vector.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"index\">The index of the component.</param>");
            WriteLine("/// <returns>The value of the indexed component.</returns>");
            WriteLine("public {0} this[int index]", Type);
            WriteLine("{");
            Indent();
            WriteLine("get");
            WriteLine("{");
            Indent();
            WriteLine("switch (index)");
            WriteLine("{");

            foreach (var component in Components)
            {
                Indent();
                WriteLine("case {0}:", component.Index);
                Indent();
                WriteLine("return {0};", component);
                Dedent();
                Dedent();
            }

            Indent();
            WriteLine("default:");
            Indent();
            WriteLine("throw new IndexOutOfRangeException(\"Indices for {0} run from 0 to {1}, inclusive.\");", Name, Dimension - 1);
            Dedent();
            Dedent();
            WriteLine("}");
            Dedent();
            WriteLine("}");
            Dedent();
            WriteLine("}");
            #endregion

            WriteLine("public {0}[] ToArray()", Type);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}[]", Type);
            WriteLine("{");
            Indent();
            WriteLine(string.Join(", ", (IEnumerable<Component>)Components));
            Dedent();
            WriteLine("};");
            Dedent();
            WriteLine("}");

            #region Swizzles
            if(Dimension <= 4)
            {
                WriteLine("#region Swizzles");
                foreach(var size in Sizes.Where(s => s <= 4))
                {
                    foreach (var permutation in
                        Ibasa.EnumerableExtensions.PermutationsWithRepetition(Components, size))
                    {
                        string components = string.Concat("(", string.Join(", ", permutation), ")");

                        var sizeVector = new Vector(Type, size);

                        WriteLine("/// <summary>");
                        WriteLine("/// Returns the vector {0}.", components);
                        WriteLine("/// </summary>");
                        WriteLine("public {0} {1}", sizeVector, string.Concat(permutation));
                        WriteLine("{");
                        Indent();
                        WriteLine("get");
                        WriteLine("{");
                        Indent();
                        WriteLine("return new {0}{1};", sizeVector, components);
                        Dedent();
                        WriteLine("}");
                        Dedent();
                        WriteLine("}");
                    }
                }
                WriteLine("#endregion");
            }
#endregion
            WriteLine("#endregion");
        }

        void Constructors()
        {
            WriteLine("#region Constructors");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified value.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">The value that will be assigned to all components.</param>");
            WriteLine("public {0}({1} value)", Name, Type);
            WriteLine("{");
            Indent();
            foreach(var component in Components)
            {
                WriteLine("{0} = value;", component);
            }
            Dedent();
            WriteLine("}");

            if (Dimension == 3)
            {
                WriteLine("/// <summary>");
                WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified vector and values.", Name);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector containing the values with which to initialize the X and Y components</param>");
                WriteLine("/// <param name=\"z\">Value for the Z component of the vector.</param>");
                WriteLine("public {0}({1} value, {2} z)", Name, new Vector(Type, 2), Type);
                WriteLine("{");
                WriteLine("\tX = value.X;");
                WriteLine("\tY = value.Y;");
                WriteLine("\tZ = z;");
                WriteLine("}");
            }
            else if (Dimension == 4)
            {
                WriteLine("/// <summary>");
                WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified vector and values.", Name);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector containing the values with which to initialize the X and Y components</param>");
                WriteLine("/// <param name=\"z\">Value for the Z component of the vector.</param>");
                WriteLine("/// <param name=\"w\">Value for the W component of the vector.</param>");
                WriteLine("public {0}({1} value, {2} z, {2} w)", Name, new Vector(Type, 2), Type);
                WriteLine("{");
                WriteLine("\tX = value.X;");
                WriteLine("\tY = value.Y;");
                WriteLine("\tZ = z;");
                WriteLine("\tW = w;");
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified vector and values.", Name);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector containing the values with which to initialize the X, Y and Z components</param>");
                WriteLine("/// <param name=\"w\">Value for the W component of the vector.</param>");
                WriteLine("public {0}({1} value, {2} w)", Name, new Vector(Type, 3), Type);
                WriteLine("{");
                WriteLine("\tX = value.X;");
                WriteLine("\tY = value.Y;");
                WriteLine("\tZ = value.Z;");
                WriteLine("\tW = w;");
                WriteLine("}");
            }
            else if (Dimension > 4)
            {
                foreach (var size in Sizes.Where(s => s < Dimension))
                {
                    int scalars = Dimension - size;
                    var vector = new Vector(Type, size);

                    WriteLine("/// <summary>");
                    WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified vector and values.", Name);
                    WriteLine("/// </summary>");
                    WriteLine("/// <param name=\"value\">A vector containing the values with which to initialize the first {0} components</param>", size);
                    for (int i = 0; i < scalars; ++i)
                    {
                        WriteLine("/// <param name=\"v{0}\">Value for the {1} component of the vector.</param>", size + i, Components[size + i]);
                    }
                    var args = string.Join(", ", Enumerable.Range(size, Dimension).Select(i => string.Format("{0} v{1}", Type, i)));
                    WriteLine("public {0}({1} value, {2})", Name, vector, args);
                    WriteLine("{");
                    Indent();

                    for (int i = 0; i < size; ++i)
                    {
                        WriteLine("{0} = value.{1};", Components[i], vector.Components[i]);
                    } 
                    for (int i = 0; i < scalars; ++i)
                    {
                        WriteLine("{0} = v{1};", Components[size + i], size + i);
                    }
                    Dedent();
                    WriteLine("}");
                }
            }

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified values.", Name);
            WriteLine("/// </summary>");
            foreach (var component in Components)
            {
                WriteLine("/// <param name=\"{0}\">Value for the {1} component of the vector.</param>", component.Name.ToLower(), component);
            }
            WriteLine("public {0}({1})", Name, string.Join(", ", Components.Select(component => Type + " " + component.Name.ToLower())));
            WriteLine("{");
            Indent();
            foreach (var component in Components)
            {
                WriteLine("{0} = {1};", component, component.Name.ToLower());
            }
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified array.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"array\">Array of values for the vector.</param>");
            WriteLine("public {0}({1}[] array)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("if (array.Length < 2)");
            Indent();
            WriteLine("throw new ArgumentException(\"Not enough elements in array.\", \"array\");");
            Dedent(); 
            foreach (var component in Components)
            {
                WriteLine("{0} = array[{1}];", component, component.Index);
            }
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified array.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"array\">Array of values for the vector.</param>");
            WriteLine("/// <param name=\"offset\">Offset to start copying values from.</param>");
            WriteLine("public {0}({1}[] array, int offset)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("if (array.Length < 2)");
            Indent();
            WriteLine("throw new ArgumentException(\"Not enough elements in array.\", \"array\");");
            Dedent(); 
            foreach (var component in Components)
            {
                WriteLine("{0} = array[offset + {1}];", component, component.Index);
            }
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Operations()
        {
            var result = new Vector(Type.PositiveType, Dimension);

            WriteLine("#region Operations");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the identity of a specified vector.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A vector.</param>");
            WriteLine("/// <returns>The identity of value.</returns>");
            WriteLine("public static {0} operator +({1} value)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return value;");
            Dedent();
            WriteLine("}");

            if (Type.NegativeType != NumberType.Invalid)
            {
                var negVector = new Vector(Type.NegativeType, Dimension);

                WriteLine("/// <summary>");
                WriteLine("/// Returns the additive inverse of a specified vector.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector.</param>");
                WriteLine("/// <returns>The negative of value.</returns>");
                WriteLine("public static {0} operator -({1} value)", negVector, Name);
                WriteLine("{");
                Indent();
                WriteLine("return Vector.Negative(value);");
                Dedent();
                WriteLine("}");
            }

            WriteLine("/// <summary>");
            WriteLine("/// Adds two vectors and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first value to add.</param>");
            WriteLine("/// <param name=\"right\">The second value to add.</param>");
            WriteLine("/// <returns>The sum of left and right.</returns>");
            WriteLine("public static {0} operator +({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Vector.Add(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Subtracts one vector from another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The value to subtract from (the minuend).</param>");
            WriteLine("/// <param name=\"right\">The value to subtract (the subtrahend).</param>");
            WriteLine("/// <returns>The result of subtracting right from left (the difference).</returns>");
            WriteLine("public static {0} operator -({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Vector.Subtract(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a vector and scalar.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The vector to multiply.</param>");
            WriteLine("/// <param name=\"right\">The scalar to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} operator *({1} left, {2} right)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("return Vector.Multiply(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a scalar and vector.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The scalar to multiply.</param>");
            WriteLine("/// <param name=\"right\">The vector to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} operator *({1} left, {2} right)", result, result.Type, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Vector.Multiply(right, left);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Divides a vector by a scalar and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The vector to be divided (the dividend).</param>");
            WriteLine("/// <param name=\"right\">The scalar to divide by (the divisor).</param>");
            WriteLine("/// <returns>The result of dividing left by right (the quotient).</returns>");
            WriteLine("public static {0} operator /({1} left, {2} right)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("return Vector.Divide(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Conversions()
        {
            WriteLine("#region Conversions");

            foreach (var type in Types.Where(t => t != Type))
            {
                string imex = type.IsImplicitlyConvertibleTo(Type) ? "implicit" : "explicit";

                Vector other = new Vector(type, Dimension);

                var casts = string.Join(", ",
                    Components.Select(component => string.Format("({0})value.{1}", Type, component)));
                
                WriteLine("/// <summary>");
                WriteLine("/// Defines an {0} conversion of a {1} value to a {2}.", imex, other, Name);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The value to convert to a {0}.</param>", Name);
                WriteLine("/// <returns>A {0} that has all components equal to value.</returns>", Name); 
                if (Type.IsCLSCompliant && !type.IsCLSCompliant)
                {
                    WriteLine("[CLSCompliant(false)]");
                }
                WriteLine("public static {0} operator {1}({2} value)", imex, Name, other);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name, casts);
                Dedent();
                WriteLine("}");
            }

            WriteLine("#endregion");
        }

        void Equatable()
        {
            WriteLine("#region Equatable");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the hash code for the current <see cref=\"{0}\"/>.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <returns>A 32-bit signed integer hash code.</returns>");
            WriteLine("public override int GetHashCode()");
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" + ", Components.Select(component => string.Format("{0}.GetHashCode()", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether the current instance and a specified");
            WriteLine("/// object have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"obj\">The object to compare.</param>");
            WriteLine("/// <returns>true if the obj parameter is a <see cref=\"{0}\"/> object or a type capable", Name);
            WriteLine("/// of implicit conversion to a <see cref=\"{0}\"/> object, and its value", Name);
            WriteLine("/// is equal to the current <see cref=\"{0}\"/> object; otherwise, false.</returns>", Name);
            WriteLine("public override bool Equals(object obj)");
            WriteLine("{");
            Indent();
            WriteLine("if (obj is {0}) {{ return Equals(({0})obj); }}", Name);
            WriteLine("return false;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether the current instance and a specified");
            WriteLine("/// vector have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The vector to compare.</param>");
            WriteLine("/// <returns>true if this vector and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two vectors are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first vector to compare.</param>");
            WriteLine("/// <param name=\"right\">The second vector to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" & ", Components.Select(component => string.Format("left.{0} == right.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two vectors are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first vector to compare.</param>");
            WriteLine("/// <param name=\"right\">The second vector to compare.</param>");
            WriteLine("/// <returns>true if the left and right are not equal; otherwise, false.</returns>");
            WriteLine("public static bool operator !=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" | ", Components.Select(component => string.Format("left.{0} != right.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void String()
        {
            WriteLine("#region ToString");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current vector to its equivalent string");
            WriteLine("/// representation in Cartesian form.");
            WriteLine("/// </summary>");
            WriteLine("/// <returns>The string representation of the current instance in Cartesian form.</returns>");
            WriteLine("public override string ToString()");
            WriteLine("{");
            Indent();
            WriteLine("return ToString(\"G\", CultureInfo.CurrentCulture);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current vector to its equivalent string");
            WriteLine("/// representation in Cartesian form by using the specified culture-specific");
            WriteLine("/// formatting information.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"provider\">An object that supplies culture-specific formatting information.</param>");
            WriteLine("/// <returns>The string representation of the current instance in Cartesian form, as specified");
            WriteLine("/// by provider.</returns>");
            WriteLine("public string ToString(IFormatProvider provider)");
            WriteLine("{");
            Indent();
            WriteLine("return ToString(\"G\", provider);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current vector to its equivalent string");
            WriteLine("/// representation in Cartesian form by using the specified format for its components.");
            WriteLine("/// formatting information.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"format\">A standard or custom numeric format string.</param>");
            WriteLine("/// <returns>The string representation of the current instance in Cartesian form.</returns>");
            WriteLine("/// <exception cref=\"System.FormatException\">format is not a valid format string.</exception>");
            WriteLine("public string ToString(string format)");
            WriteLine("{");
            Indent();
            WriteLine("return ToString(format, CultureInfo.CurrentCulture);");
            Dedent();
            WriteLine("}");
            
            var formatString = string.Join(", ", 
                Components.Select(component => "{" + component.Index + "}"));
            var components = string.Join(", ", 
                Components.Select(component => component + ".ToString(format, provider)"));

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current vector to its equivalent string");
            WriteLine("/// representation in Cartesian form by using the specified format and culture-specific");
            WriteLine("/// format information for its components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"format\">A standard or custom numeric format string.</param>");
            WriteLine("/// <returns>The string representation of the current instance in Cartesian form, as specified");
            WriteLine("/// by format and provider.</returns>");
            WriteLine("/// <exception cref=\"System.FormatException\">format is not a valid format string.</exception>");
            WriteLine("public string ToString(string format, IFormatProvider provider)");
            WriteLine("{");
            Indent();
            WriteLine("return String.Format(\"({0})\", {1});", formatString, components);
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Functions()
        {
            WriteLine("/// <summary>");
            WriteLine("/// Provides static methods for vector functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Vector");
            WriteLine("{");
            Indent();

            var result = new Vector(Type.PositiveType, Dimension);

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to a Ibasa.IO.BinaryWriter.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this Ibasa.IO.BinaryWriter writer, {0} vector)", Name);
            WriteLine("{");
            Indent();
            for (int i = 0; i < Dimension; ++i)
            {
                WriteLine("writer.Write(vector.{0});", Components[i]);
            }
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> to a Ibasa.IO.BinaryReader.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static {0} Read{0}(this Ibasa.IO.BinaryReader reader)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, string.Join(", ", Components.Select(c => string.Format("reader.Read{0}()", Type.CLRName))));
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Pack
            if(!Type.IsReal)
            {
                WriteLine("#region Pack");

                int size = Components.Length * Type.Size;

                var packed = NumberType.Types.
                    Where(type => !type.IsReal && (type.IsCLSCompliant || !Type.IsCLSCompliant) && type.Size >= size).
                    Concat(new NumberType[] { NumberType.Long }).
                    OrderBy(type => type.Size).
                    First();

                var args = Components.Select(c => string.Format("{0}Bits", c.Name.ToLower())).ToArray();
                int bits = packed.Size * 8;

                WriteLine("public static {0} Pack({1}, {2} vector)", packed, 
                    string.Join(", ", args.Select(arg => string.Format("int {0}", arg))), Name);
                WriteLine("{");
                Indent();
                for (int i = 0; i < args.Length; ++i)
                {
                    WriteLine("Contract.Requires(0 <= {0} && {0} <= {1}, \"{0} must be between 0 and {1} inclusive.\");", 
                        args[i], Type.Size * 8);
                }
                WriteLine("Contract.Requires({0} <= {1});", string.Join(" + ", args), bits);

                for (int i = 0; i < Components.Length; ++i)
                {
                    var var = Components[i].Name.ToLower();
                    var name = Components[i].Name;
                    var bitarg = args[i];
                    var offset = string.Join(" + ", Enumerable.Range(0, i).Select(j => args[j]));
                    WriteLine("ulong {0} = (ulong)(vector.{1}) >> ({2} - {3});", var, name, bits, bitarg);
                    if (offset.Length != 0)
                    {
                        WriteLine("{0} <<= {1};", var, offset);
                    }
                }

                WriteLine("return ({0})({1});", packed, string.Join(" | ", Components.Select(component => component.Name.ToLower())));
                Dedent(); 
                WriteLine("}");

                if (packed.Size >= size)
                {
                    WriteLine("public static {0} Pack({1} vector)", packed, Name);
                    WriteLine("{");
                    Indent();
                    for (int i = 0; i < Components.Length; ++i)
                    {
                        var var = Components[i].Name.ToLower();
                        var name = Components[i].Name;
                        var offset = (Type.Size * 8 * i).ToString();
                        WriteLine("ulong {0} = (ulong)(vector.{1}) << {2};", var, name, offset);
                    }

                    WriteLine("return ({0})({1});", packed, string.Join(" | ", Components.Select(component => component.Name.ToLower())));
                    Dedent();
                    WriteLine("}");
                }

                // Unpack

                if (packed == Type)
                {
                    WriteLine("public static {0} Unpack({1}, {2} bits)", Name,
                       string.Join(", ", args.Select(arg => string.Format("int {0}", arg))), packed);
                    WriteLine("{");
                    Indent();
                    for (int i = 0; i < args.Length; ++i)
                    {
                        WriteLine("Contract.Requires(0 <= {0} && {0} <= {1}, \"{0} must be between 0 and {1} inclusive.\");",
                            args[i], Type.Size * 8);
                    }
                    WriteLine("Contract.Requires({0} <= {1});", string.Join(" + ", args), bits);

                    for (int i = 0; i < Components.Length; ++i)
                    {
                        var var = Components[i].Name.ToLower();
                        var name = Components[i].Name;
                        var bitarg = args[i];
                        var offset = string.Join(" + ", Enumerable.Range(0, i).Select(j => args[j]));
                        if (offset != "")
                        {
                            WriteLine("ulong {0} = (ulong)(bits) >> ({1});", var, offset);
                        }
                        else
                        {
                            WriteLine("ulong {0} = (ulong)(bits);", var);
                        }
                        WriteLine("{0} &= ((1UL << {1}) - 1);", var, bitarg);
                    }

                    WriteLine("return new {0}({1});", Name,
                        string.Join(", ", Components.Select(component => string.Format("({0}){1}", Type, component.Name.ToLower()))));
                    Dedent();
                    WriteLine("}");
                }
                
                WriteLine("#endregion");
            }
            #endregion

            #region Operations
            WriteLine("#region Operations");
            if (Type.NegativeType != NumberType.Invalid)
            {
                var negVector = new Vector(Type.NegativeType, Dimension);

                WriteLine("/// <summary>");
                WriteLine("/// Returns the additive inverse of a vector.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector.</param>");
                WriteLine("/// <returns>The negative of value.</returns>");
                WriteLine("public static {0} Negative({1} value)", negVector, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", negVector,
                    string.Join(", ", Components.Select(component => "-value." + component)));
                Dedent();
                WriteLine("}");
            }

            WriteLine("/// <summary>");
            WriteLine("/// Adds two vectors and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first value to add.</param>");
            WriteLine("/// <param name=\"right\">The second value to add.</param>");
            WriteLine("/// <returns>The sum of left and right.</returns>");
            WriteLine("public static {0} Add({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", result,
                string.Join(", ", Components.Select(component => string.Format("left.{0} + right.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Subtracts one vectors from another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The value to subtract from (the minuend).</param>");
            WriteLine("/// <param name=\"right\">The value to subtract (the subtrahend).</param>");
            WriteLine("/// <returns>The result of subtracting right from left (the difference).</returns>");
            WriteLine("public static {0} Subtract({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", result,
                string.Join(", ", Components.Select(component => string.Format("left.{0} - right.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a vector and scalar.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"vector\">The vector to multiply.</param>");
            WriteLine("/// <param name=\"scalar\">The scalar to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} Multiply({1} vector, {2} scalar)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", result,
                string.Join(", ", Components.Select(component => string.Format("vector.{0} * scalar", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Divides a vector by a scalar and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"vector\">The vector to be divided (the dividend).</param>");
            WriteLine("/// <param name=\"scalar\">The scalar to divide by (the divisor).</param>");
            WriteLine("/// <returns>The result of dividing left by right (the quotient).</returns>");
            WriteLine("public static {0} Divide({1} vector, {2} scalar)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", result,
                string.Join(", ", Components.Select(component => string.Format("vector.{0} / scalar", component))));
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two vectors are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first vector to compare.</param>");
            WriteLine("/// <param name=\"right\">The second vector to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool Equals({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left == right;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Products
            WriteLine("#region Products");            
            WriteLine("/// <summary>");
            WriteLine("/// Calculates the dot product (inner product) of two vectors.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">First source vector.</param>");
            WriteLine("/// <param name=\"right\">Second source vector.</param>");
            WriteLine("/// <returns>The dot product of the two vectors.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Dot({1} left, {1} right)", result.Type, Name);
            WriteLine("{");
            Indent();
            var dotproduct = string.Join(" + ", Components.Select(component => string.Format("left.{0} * right.{0}", component)));
            WriteLine("return {0};", dotproduct);
            Dedent();
            WriteLine("}");

            if (Dimension == 3 && Type.IsReal)
            {
                WriteLine("/// <summary>");
                WriteLine("/// Calculates the cross product (outer product) of two vectors.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"left\">First source vector.</param>");
                WriteLine("/// <param name=\"right\">Second source vector.</param>");
                WriteLine("/// <returns>The cross product of the two vectors.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Cross({1} left, {1} right)", result, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}(", result);
                Indent();
                WriteLine("left.Y * right.Z - left.Z * right.Y,");
                WriteLine("left.Z * right.X - left.X * right.Z,");
                WriteLine("left.X * right.Y - left.Y * right.X);");
                Dedent();
                Dedent();
                WriteLine("}");
            }
            WriteLine("#endregion");      
            #endregion

            #region Test
            WriteLine("#region Test");
            WriteLine("/// <summary>");
            WriteLine("/// Determines whether all components of a vector are non-zero.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A vector.</param>");
            WriteLine("/// <returns>true if all components are non-zero; false otherwise.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static bool All({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" && ", Components.Select(component => string.Format("value.{0} != 0", component))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Determines whether all components of a vector satisfy a condition."); 
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A vector.</param>");
            WriteLine("/// <param name=\"predicate\">A function to test each component for a condition.</param>");
            WriteLine("/// <returns>true if every component of the vector passes the test in the specified");
            WriteLine("/// predicate; otherwise, false.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static bool All({0} value, Predicate<{1}> predicate)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" && ", Components.Select(component => string.Format("predicate(value.{0})", component))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Determines whether any component of a vector is non-zero.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A vector.</param>");
            WriteLine("/// <returns>true if any components are non-zero; false otherwise.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static bool Any({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" || ", Components.Select(component => string.Format("value.{0} != 0", component))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Determines whether any components of a vector satisfy a condition.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A vector.</param>");
            WriteLine("/// <param name=\"predicate\">A function to test each component for a condition.</param>");
            WriteLine("/// <returns>true if any component of the vector passes the test in the specified");
            WriteLine("/// predicate; otherwise, false.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static bool Any({0} value, Predicate<{1}> predicate)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" || ", Components.Select(component => string.Format("predicate(value.{0})", component))));
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Properties
            WriteLine("#region Properties");
            WriteLine("/// <summary>");
            WriteLine("/// Computes the absolute squared value of a vector and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A vector.</param>");
            WriteLine("/// <returns>The absolute squared value of value.</returns> ");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} AbsoluteSquared({1} value)", result.Type, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Dot(value, value);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Computes the absolute value (or modulus or magnitude) of a vector and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A vector.</param>");
            WriteLine("/// <returns>The absolute value of value.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Absolute({1} value)", Type.RealType, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Functions.Sqrt(AbsoluteSquared(value));");
            Dedent();
            WriteLine("}");
            var realVector = new Vector(Type.RealType, Dimension);
            WriteLine("/// <summary>");
            WriteLine("/// Computes the normalized value (or unit) of a vector.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A vector.</param>");
            WriteLine("/// <returns>The normalized value of value.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Normalize({1} value)", realVector, Name);
            WriteLine("{");
            Indent();
            WriteLine("return ({0})value / Absolute(value);", realVector);
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Per component
            WriteLine("#region Per component");

            WriteLine("#region Transform");
            foreach(var type in Types)
            {
                var transform = new Vector(type, Dimension);

                WriteLine("/// <summary>");
                WriteLine("/// Transforms the components of a vector and returns the result.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The vector to transform.</param>");
                WriteLine("/// <param name=\"transformer\">A transform function to apply to each component.</param>");
                WriteLine("/// <returns>The result of transforming each component of value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Transform({1} value, Func<{2}, {3}> transformer)", transform, Name, Type, transform.Type);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", transform,
                    string.Join(", ", Components.Select(component => string.Format("transformer(value.{0})", component))));
                Dedent();
                WriteLine("}");
            }
            WriteLine("#endregion");

            WriteLine("/// <summary>");
            WriteLine("/// Multiplys the components of two vectors and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first vector to modulate.</param>");
            WriteLine("/// <param name=\"right\">The second vector to modulate.</param>");
            WriteLine("/// <returns>The result of multiplying each component of left by the matching component in right.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Modulate({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", result, 
                string.Join(", ", Components.Select(component => string.Format("left.{0} * right.{0}", component))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Returns the absolute value (per component).");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A vector.</param>");
            WriteLine("/// <returns>The absolute value (per component) of value.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Abs({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, 
                string.Join(", ", Components.Select(component => string.Format("Functions.Abs(value.{0})", component))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a vector that contains the lowest value from each pair of components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value1\">The first vector.</param>");
            WriteLine("/// <param name=\"value2\">The second vector.</param>");
            WriteLine("/// <returns>The lowest of each component in left and the matching component in right.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Min({0} value1, {0} value2)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Components.Select(component => string.Format("Functions.Min(value1.{0}, value2.{0})", component))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a vector that contains the highest value from each pair of components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value1\">The first vector.</param>");
            WriteLine("/// <param name=\"value2\">The second vector.</param>");
            WriteLine("/// <returns>The highest of each component in left and the matching component in right.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Max({0} value1, {0} value2)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Components.Select(component => string.Format("Functions.Max(value1.{0}, value2.{0})", component))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Constrains each component to a given range.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A vector to constrain.</param>");
            WriteLine("/// <param name=\"min\">The minimum values for each component.</param>");
            WriteLine("/// <param name=\"max\">The maximum values for each component.</param>");
            WriteLine("/// <returns>A vector with each component constrained to the given range.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Clamp({0} value, {0} min, {0} max)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, 
                string.Join(", ", Components.Select(component => string.Format("Functions.Clamp(value.{0}, min.{0}, max.{0})", component))));
            Dedent();
            WriteLine("}");
            if(Type.IsReal)
            {
                WriteLine("/// <summary>");
                WriteLine("/// Constrains each component to the range 0 to 1.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector to saturate.</param>");
                WriteLine("/// <returns>A vector with each component constrained to the range 0 to 1.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Saturate({0} value)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name, 
                    string.Join(", ", Components.Select(component => string.Format("Functions.Saturate(value.{0})", component))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a vector where each component is the smallest integral value that");
                WriteLine("/// is greater than or equal to the specified component.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector.</param>");
                WriteLine("/// <returns>The ceiling of value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Ceiling({1} value)", result, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", result, 
                    string.Join(", ", Components.Select(component => string.Format("Functions.Ceiling(value.{0})", component))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a vector where each component is the largest integral value that");
                WriteLine("/// is less than or equal to the specified component.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector.</param>");
                WriteLine("/// <returns>The floor of value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Floor({1} value)", result, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", result, 
                    string.Join(", ", Components.Select(component => string.Format("Functions.Floor(value.{0})", component))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a vector where each component is the integral part of the specified component.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector.</param>");
                WriteLine("/// <returns>The integral of value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Truncate({1} value)", result, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", result, 
                    string.Join(", ", Components.Select(component => string.Format("Functions.Truncate(value.{0})", component))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a vector where each component is the fractional part of the specified component.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector.</param>");
                WriteLine("/// <returns>The fractional of value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Fractional({1} value)", result, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", result, 
                    string.Join(", ", Components.Select(component => string.Format("Functions.Fractional(value.{0})", component))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a vector where each component is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector.</param>");
                WriteLine("/// <returns>The result of rounding value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Round({1} value)", result, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", result, 
                    string.Join(", ", Components.Select(component => string.Format("Functions.Round(value.{0})", component))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a vector where each component is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector.</param>");
                WriteLine("/// <param name=\"digits\">The number of fractional digits in the return value.</param>");
                WriteLine("/// <returns>The result of rounding value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Round({1} value, int digits)", result, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", result, 
                    string.Join(", ", Components.Select(component => string.Format("Functions.Round(value.{0}, digits)", component))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a vector where each component is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector.</param>");
                WriteLine("/// <param name=\"mode\">Specification for how to round value if it is midway between two other numbers.</param>");
                WriteLine("/// <returns>The result of rounding value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Round({1} value, MidpointRounding mode)", result, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", result, 
                    string.Join(", ", Components.Select(component => string.Format("Functions.Round(value.{0}, mode)", component))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a vector where each component is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector.</param>");
                WriteLine("/// <param name=\"digits\">The number of fractional digits in the return value.</param>");
                WriteLine("/// <param name=\"mode\">Specification for how to round value if it is midway between two other numbers.</param>");
                WriteLine("/// <returns>The result of rounding value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Round({1} value, int digits, MidpointRounding mode)", result, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", result, 
                    string.Join(", ", Components.Select(component => string.Format("Functions.Round(value.{0}, digits, mode)", component))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Calculates the reciprocal of each component in the vector.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A vector.</param>");
                WriteLine("/// <returns>A vector with the reciprocal of each of values components.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Reciprocal({1} value)", result, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", result,
                    string.Join(", ", Components.Select(component => string.Format("1 / value.{0}", component))));
                Dedent();
                WriteLine("}");
            }
            WriteLine("#endregion");
            #endregion 

            #region Barycentric, Reflect, Refract
            if(Type.IsReal)
            {
                WriteLine("#region Barycentric, Reflect, Refract");
                WriteLine("/// <summary>");
                WriteLine("/// Returns the Cartesian coordinate for one axis of a point that is defined");
                WriteLine("/// by a given triangle and two normalized barycentric (areal) coordinates.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value1\">The coordinate of vertex 1 of the defining triangle.</param>");
                WriteLine("/// <param name=\"value2\">The coordinate of vertex 2 of the defining triangle.</param>");
                WriteLine("/// <param name=\"value3\">The coordinate of vertex 3 of the defining triangle.</param>");
                WriteLine("/// <param name=\"amount1\">The normalized barycentric (areal) coordinate b2, equal to the weighting");
                WriteLine("/// factor for vertex 2, the coordinate of which is specified in value2.</param>");
                WriteLine("/// <param name=\"amount2\">The normalized barycentric (areal) coordinate b3, equal to the weighting");
                WriteLine("/// factor for vertex 3, the coordinate of which is specified in value3.</param>");
                WriteLine("/// <returns>Cartesian coordinate of the specified point.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Barycentric({1} value1, {1} value2, {1} value3, {2} amount1, {2} amount2)", result, Name, result.Type);
                WriteLine("{");
                Indent();
                WriteLine("return ((1 - amount1 - amount2) * value1) + (amount1 * value2) + (amount2 * value3);");
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns the reflection of a vector off a surface that has the specified normal.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"vector\">The source vector.</param>");
                WriteLine("/// <param name=\"normal\">Normal of the surface.</param>");
                WriteLine("/// <returns>The reflected vector.</returns>");
                WriteLine("/// <remarks>Reflect only gives the direction of a reflection off a surface, it does not determine");
                WriteLine("/// whether the original vector was close enough to the surface to hit it.</remarks>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Reflect({1} vector, {1} normal)", result, Name);
                WriteLine("{");
                Indent();
                WriteLine("return vector - ((2 * Dot(vector, normal)) * normal);");
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns the refraction of a vector off a surface that has the specified normal, and refractive index.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"vector\">The source vector.</param>");
                WriteLine("/// <param name=\"normal\">Normal of the surface.</param>");
                WriteLine("/// <param name=\"index\">The refractive index, destination index over source index.</param>");
                WriteLine("/// <returns>The refracted vector.</returns>");
                WriteLine("/// <remarks>Refract only gives the direction of a refraction off a surface, it does not determine");
                WriteLine("/// whether the original vector was close enough to the surface to hit it.</remarks>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Refract({1} vector, {1} normal, {2} index)", result, Name, result.Type);
                WriteLine("{");
                Indent();
                WriteLine("var cos1 = Dot(vector, normal);");
                WriteLine("var radicand = 1 - (index * index) * (1 - (cos1 * cos1));");
                WriteLine("if (radicand < 0)");
                WriteLine("{");
                Indent();
                WriteLine("return {0}.Zero;", result);
                Dedent();
                WriteLine("}");
                WriteLine("return (index * vector) + ((Functions.Sqrt(radicand) - index * cos1) * normal);");
                Dedent();
                WriteLine("}");
                WriteLine("#endregion");
            }
            #endregion

            #region Interpolation
            if(Type.IsReal)
            {
                WriteLine("#region Interpolation");
                //WriteLine("/// <summary>");
                //WriteLine("/// Performs a Hermite spline interpolation.");
                //WriteLine("/// </summary>");
                //WriteLine("/// <param name=\"value1\">First source position.</param>");
                //WriteLine("/// <param name=\"tangent1\">First source tangent.</param>");
                //WriteLine("/// <param name=\"value2\">Second source position.</param>");
                //WriteLine("/// <param name=\"tangent2\">Second source tangent.</param>");
                //WriteLine("/// <param name=\"amount\">Weighting factor.</param>");
                //WriteLine("/// <returns>The result of the Hermite spline interpolation.</returns>");
                //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); } 
                //WriteLine("public static {0} Hermite({1} value1, {1} tangent1, {1} value2, {1} tangent2, {2} amount)", result, Name, result.Type);
                //WriteLine("{");
                //Indent();
                //WriteLine("var amount2 = amount * amount;");
                //WriteLine("var amount3 = amount2 * amount;");
                //WriteLine("var h00 = 2 * amount3 - 3 * amount2 + 1;");
                //WriteLine("var h10 = amount3 - 2 * amount2 + amount;");
                //WriteLine("var h01 = -2 * amount3 + 3 * amount2;");
                //WriteLine("var h11 = amount3 - amount2;");
                //WriteLine("return h00 * value1 + h10 * tangent1 + h01 * value2 + h11 * tangent2;");
                //Dedent();
                //WriteLine("}");
                //WriteLine("/// <summary>");
                //WriteLine("/// Performs a Catmull-Rom interpolation using the specified positions.");
                //WriteLine("/// </summary>");
                //WriteLine("/// <param name=\"value1\">The first position in the interpolation.</param>");
                //WriteLine("/// <param name=\"value2\">The second position in the interpolation.</param>");
                //WriteLine("/// <param name=\"value3\">The third position in the interpolation.</param>");
                //WriteLine("/// <param name=\"value4\">The fourth position in the interpolation.</param>");
                //WriteLine("/// <param name=\"amount\">Weighting factor.</param>");
                //WriteLine("/// <returns>A vector that is the result of the Catmull-Rom interpolation.</returns>");
                //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                //WriteLine("public static {0} CatmullRom({1} value1, {1} value2, {1} value3, {1} value4, {2} amount)", result, Name, result.Type);
                //WriteLine("{");
                //Indent();
                //WriteLine("var tangent0 = (value3 - value1) / 2;");
                //WriteLine("var tangent1 = (value4 - value2) / 2;");
                //WriteLine("return Hermite(value2, tangent0, value3, tangent1, amount);");
                //Dedent();
                //WriteLine("}");
                //WriteLine("/// <summary>");
                //WriteLine("/// Performs a cubic interpolation between two values.");
                //WriteLine("/// </summary>");
                //WriteLine("/// <param name=\"value1\">First values.</param>");
                //WriteLine("/// <param name=\"value2\">Second values.</param>");
                //WriteLine("/// <param name=\"amount\">Value between 0 and 1 indicating the weight of <paramref name=\"value2\"/>.</param>");
                //WriteLine("/// <returns>The cubic interpolation of the two vectors.</returns>");
                //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                //WriteLine("public static {0} SmoothStep({1} value1, {1} value2, {2} amount)", result, Name, result.Type);
                //WriteLine("{");
                //Indent();
                //WriteLine("amount = Functions.Saturate(amount);");
                //WriteLine("amount = amount * amount * (3 - 2 * amount);");
                //WriteLine("return Lerp(value1, value2, amount);");
                //Dedent();
                //WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Performs a linear interpolation between two values.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value1\">First value.</param>");
                WriteLine("/// <param name=\"value2\">Second value.</param>");
                WriteLine("/// <param name=\"amount\">Value between 0 and 1 indicating the weight of <paramref name=\"value2\"/>.</param>");
                WriteLine("/// <returns>The linear interpolation of the two values.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Lerp({1} value1, {1} value2, {2} amount)", result, Name, result.Type);
                WriteLine("{");
                Indent();
                WriteLine("return (1 - amount) * value1 + amount * value2;");
                Dedent();
                WriteLine("}");
                //WriteLine("/// <summary>");
                //WriteLine("/// Performs a quadratic interpolation between three values.");
                //WriteLine("/// </summary>");
                //WriteLine("/// <param name=\"value1\">First value.</param>");
                //WriteLine("/// <param name=\"value2\">Second value.</param>");
                //WriteLine("/// <param name=\"value3\">Third value.</param>");
                //WriteLine("/// <param name=\"amount\">Value between 0 and 1 indicating the weight towards <paramref name=\"value3\"/>.</param>");
                //WriteLine("/// <returns>The quadratic interpolation of the three values.</returns>");
                //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                //WriteLine("public static {0} Qerp({1} value1, {1} value2, {1} value3, {2} amount)", result, Name, result.Type);
                //WriteLine("{");
                //Indent();
                //WriteLine("var p = 2 * value1 - value2 + 2 * value3;");
                //WriteLine("var q = value2 - value3 - 3 * value1;");
                //WriteLine("var r = value1;");
                //WriteLine("var amount2 = amount * amount;");
                //WriteLine("return (p * amount * amount2) + (q * amount) + (r);");
                //Dedent();
                //WriteLine("}");
                //WriteLine("/// <summary>");
                //WriteLine("/// Performs a cubic interpolation between four values.");
                //WriteLine("/// </summary>");
                //WriteLine("/// <param name=\"value1\">First value.</param>");
                //WriteLine("/// <param name=\"value2\">Second value.</param>");
                //WriteLine("/// <param name=\"value3\">Third value.</param>");
                //WriteLine("/// <param name=\"value4\">Fourth value.</param>");
                //WriteLine("/// <param name=\"amount\">Value between 0 and 1 indicating the weight towards <paramref name=\"value4\"/>.</param>");
                //WriteLine("/// <returns>The cubic interpolation of the four values.</returns>");
                //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                //WriteLine("public static {0} Cerp({1} value1, {1} value2, {1} value3, {1} value4, {2} amount)", result, Name, result.Type);
                //WriteLine("{");
                //Indent();
                //WriteLine("var p = (value4 - value3) - (value1 - value2);");
                //WriteLine("var q = (value1 - value2) - p;");
                //WriteLine("var r = value3 - value1;");
                //WriteLine("var s = value2;");
                //WriteLine("var amount2 = amount * amount;");
                //WriteLine("return (p * amount * amount2) + (q * amount2) + (r * amount) + (s);");
                //Dedent();
                //WriteLine("}");
                WriteLine("#endregion");
            }
            #endregion
    
            Dedent();
            WriteLine("}");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
