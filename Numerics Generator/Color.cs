using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Color : Generator
    {
        public static NumberType[] Types
        {
            get
            {
                return new NumberType[] {
                    NumberType.Double, NumberType.Float,
                };
            }
        }

        public NumberType Type { get; private set; }
        public Component[] Components { get; private set; }

        public string ColorName(Component component)
        {
            switch (component.Index)
            {
                case 0:
                    return "Red";
                case 1:
                    return "Green";
                case 2:
                    return "Blue";
                case 3:
                    return "Alpha";
            }
            return null;
        }

        public Color(NumberType type)
        {
            Type = type;
            Components = new Component[] {
                new Component("R", 0),
                new Component("G", 1),
                new Component("B", 2),
                new Component("A", 3)
            };
        }

        public string Name
        {
            get
            {
                return string.Format("Color{0}", Type.Suffix);
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
            WriteLine("using Ibasa.Numerics.Geometry;");
            WriteLine("");
            WriteLine("namespace Ibasa.Numerics");
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
            WriteLine("/// Represents a color.");
            WriteLine("/// </summary>");
            WriteLine("[Serializable]");
            WriteLine("[ComVisible(true)]");
            WriteLine("[StructLayout(LayoutKind.Sequential)]");
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

            foreach (var property in 
                typeof(System.Drawing.Color).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                System.Drawing.Color color = (System.Drawing.Color)property.GetValue(null, null);

                string r = string.Format("{0}{1}", color.R / 255.0, Type == NumberType.Float ? "f" : "");
                string g = string.Format("{0}{1}", color.G / 255.0, Type == NumberType.Float ? "f" : "");
                string b = string.Format("{0}{1}", color.B / 255.0, Type == NumberType.Float ? "f" : "");
                string a = string.Format("{0}{1}", color.A / 255.0, Type == NumberType.Float ? "f" : "");

                string desc = string.Format("Red:{0} Green:{1} Blue:{2} Alpha:{3}", color.R, color.G, color.B, color.A);
                string args = string.Join(", ", r, g, b, a);

                WriteLine("/// <summary>");
                WriteLine("/// Gets a color with the value {0}.", desc);
                WriteLine("/// </summary>");
                WriteLine("public static {0} {1}", Name, property.Name);
                WriteLine("{");
                Indent();
                WriteLine("get {{ return new {0}({1}); }}", Name, args);
                Dedent();
                WriteLine("}");
            }  
            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            foreach (var component in Components)
            {
                WriteLine("/// <summary>");
                WriteLine("/// The color's {0} component.", ColorName(component).ToLower());
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
            WriteLine("/// Returns the indexed component of this color.");
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
            WriteLine("throw new IndexOutOfRangeException(\"Indices for {0} run from 0 to 3, inclusive.\");", Name);
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
            WriteLine("#region Swizzles");
            foreach (var permutation in
                Ibasa.EnumerableExtensions.PermutationsWithRepetition(Components, 4))
            {
                string components = string.Concat("(", string.Join(", ", permutation), ")");

                var swizzle = new Color(Type);

                WriteLine("/// <summary>");
                WriteLine("/// Returns the color {0}.", components);
                WriteLine("/// </summary>");
                WriteLine("public {0} {1}", swizzle, string.Concat(permutation));
                WriteLine("{");
                Indent();
                WriteLine("get");
                WriteLine("{");
                Indent();
                WriteLine("return new {0}{1};", swizzle, components);
                Dedent();
                WriteLine("}");
                Dedent();
                WriteLine("}");
            }
            WriteLine("#endregion");
            #endregion
            WriteLine("#endregion");
        }

        void Constructors()
        {
            WriteLine("#region Constructors");
            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified values.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"red\">Value for the red component of the color.</param>");
            WriteLine("/// <param name=\"green\">Value for the green component of the color.</param>");
            WriteLine("/// <param name=\"blue\">Value for the blue component of the color.</param>");
            WriteLine("public {0}({1} red, {1} green, {1} blue)", Name, Type);
            Indent(); 
            WriteLine(": this(red, green, blue, 1)");
            Dedent();
            WriteLine("{");
            Indent(); 
            WriteLine("Contract.Requires(0 <= red);");
            WriteLine("Contract.Requires(0 <= green);");
            WriteLine("Contract.Requires(0 <= blue);");
            Dedent(); 
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified values.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"red\">Value for the red component of the color.</param>");
            WriteLine("/// <param name=\"green\">Value for the green component of the color.</param>");
            WriteLine("/// <param name=\"blue\">Value for the blue component of the color.</param>");
            WriteLine("/// <param name=\"alpha\">Value for the alpha component of the color.</param>");
            WriteLine("public {0}({1} red, {1} green, {1} blue, {1} alpha)", Name, Type);
            WriteLine("{");
            WriteLine("Contract.Requires(0 <= red);");
            WriteLine("Contract.Requires(0 <= green);");
            WriteLine("Contract.Requires(0 <= blue);");
            WriteLine("Contract.Requires(0 <= alpha);");
            Indent();
            WriteLine("R = red;");
            WriteLine("G = green;");
            WriteLine("B = blue;");
            WriteLine("A = alpha;");
            Dedent(); 
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified array.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"array\">Array of values for the vector.</param>");
            WriteLine("public {0}({1}[] array)", Name, Type);
            Indent();
            WriteLine(": this(array, 0)");
            Dedent();
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(array != null);");
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
            WriteLine("Contract.Requires(array != null);");
            WriteLine("if (array.Length < offset + 3)");
            WriteLine("{");
            Indent();
            WriteLine("throw new ArgumentException(\"Not enough elements in array.\", \"array\");");
            Dedent();
            WriteLine("}");
            WriteLine("R = array[offset];");
            WriteLine("G = array[offset + 1];");
            WriteLine("B = array[offset + 2];");
            WriteLine("if (array.Length < offset + 4)");
            WriteLine("{");
            Indent();
            WriteLine("A = 1;");
            Dedent();
            WriteLine("}");
            WriteLine("else");
            WriteLine("{");
            Indent();
            WriteLine("A = array[offset + 3];");
            Dedent();
            WriteLine("}");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Operations()
        {
            WriteLine("#region Operations");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the identity of a specified color.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A color.</param>");
            WriteLine("/// <returns>The identity of value.</returns>");
            WriteLine("public static {0} operator +({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return value;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Adds two colors and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first value to add.</param>");
            WriteLine("/// <param name=\"right\">The second value to add.</param>");
            WriteLine("/// <returns>The sum of left and right.</returns>");
            WriteLine("public static {0} operator +({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return Color.Add(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Subtracts one color from another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The value to subtract from (the minuend).</param>");
            WriteLine("/// <param name=\"right\">The value to subtract (the subtrahend).</param>");
            WriteLine("/// <returns>The result of subtracting right from left (the difference).</returns>");
            WriteLine("public static {0} operator -({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return Color.Subtract(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a color and scalar.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The color to multiply.</param>");
            WriteLine("/// <param name=\"right\">The scalar to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} operator *({0} left, {1} right)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return Color.Multiply(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a scalar and color.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The scalar to multiply.</param>");
            WriteLine("/// <param name=\"right\">The color to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} operator *({1} left, {0} right)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return Color.Multiply(right, left);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Divides a color by a scalar and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The color to be divided (the dividend).</param>");
            WriteLine("/// <param name=\"right\">The scalar to divide by (the divisor).</param>");
            WriteLine("/// <returns>The result of dividing left by right (the quotient).</returns>");
            WriteLine("public static {0} operator /({0} left, {1} right)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return Color.Divide(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Conversions()
        {
            WriteLine("#region Conversions");

            foreach (var type in Color.Types.Where(t => t != Type))
            {
                string imex = type.IsImplicitlyConvertibleTo(Type) ? "implicit" : "explicit";

                Color other = new Color(type);

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

            foreach (var type in Vector.Types)
            {
                string to_vector_imex = Type.IsImplicitlyConvertibleTo(type) ? "implicit" : "explicit";
                string to_color_imex = type.IsImplicitlyConvertibleTo(Type) ? "implicit" : "explicit";

                Vector vector = new Vector(type, 4);

                var to_vector = string.Join(", ",
                    this.Components.Select(component => string.Format("({0})value.{1}", type, component)));

                var to_color = string.Join(", ",
                    vector.Components.Select(component => string.Format("({0})value.{1}", Type, component)));

                WriteLine("/// <summary>");
                WriteLine("/// Defines an {0} conversion of a {1} value to a {2}.", to_vector_imex, Name, vector);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The value to convert to a {0}.</param>", vector);
                WriteLine("/// <returns>A {0} that has all components equal to value.</returns>", vector);
                if (Type.IsCLSCompliant && !type.IsCLSCompliant)
                {
                    WriteLine("[CLSCompliant(false)]");
                }
                WriteLine("public static {0} operator {1}({2} value)", to_vector_imex, vector, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", vector, to_vector);
                Dedent();
                WriteLine("}");

                WriteLine("/// <summary>");
                WriteLine("/// Defines an {0} conversion of a {1} value to a {2}.", to_color_imex, vector, Name);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The value to convert to a {0}.</param>", Name);
                WriteLine("/// <returns>A {0} that has all components equal to value.</returns>", Name);
                if (Type.IsCLSCompliant && !type.IsCLSCompliant)
                {
                    WriteLine("[CLSCompliant(false)]");
                }
                WriteLine("public static {0} operator {1}({2} value)", to_color_imex, Name, vector);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name, to_color);
                Dedent();
                WriteLine("}");
            }

            foreach (var type in Vector.Types)
            {
                Vector vector = new Vector(type, 3);

                var to_vector = string.Join(", ",
                    string.Format("({0})value.R", type),
                    string.Format("({0})value.G", type),
                    string.Format("({0})value.B", type));

                var to_color = string.Join(", ",
                    string.Format("({0})value.X", Type),
                    string.Format("({0})value.Y", Type),
                    string.Format("({0})value.Z", Type), "1");

                WriteLine("/// <summary>");
                WriteLine("/// Defines an explicit conversion of a {0} value to a {1}.", Name, vector);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The value to convert to a {0}.</param>", vector);
                WriteLine("/// <returns>A {0} that has all components equal to value.</returns>", vector);
                if (Type.IsCLSCompliant && !type.IsCLSCompliant)
                {
                    WriteLine("[CLSCompliant(false)]");
                }
                WriteLine("public static explicit operator {0}({1} value)", vector, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", vector, to_vector);
                Dedent();
                WriteLine("}");

                WriteLine("/// <summary>");
                WriteLine("/// Defines an explicit conversion of a {0} value to a {1}.", vector, Name);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The value to convert to a {0}.</param>", Name);
                WriteLine("/// <returns>A explicit that has all components equal to value.</returns>", Name);
                if (Type.IsCLSCompliant && !type.IsCLSCompliant)
                {
                    WriteLine("[CLSCompliant(false)]");
                }
                WriteLine("public static explicit operator {0}({1} value)", Name, vector);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name, to_color);
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
            WriteLine("/// Returns a value that indicates whether two colors are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first color to compare.</param>");
            WriteLine("/// <param name=\"right\">The second color to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" & ", Components.Select(component => string.Format("left.{0} == right.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two colors are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first color to compare.</param>");
            WriteLine("/// <param name=\"right\">The second color to compare.</param>");
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
            WriteLine("/// Converts the value of the current color to its equivalent string representation.");
            WriteLine("/// </summary>");
            WriteLine("/// <returns>The string representation of the current instance.</returns>");
            WriteLine("public override string ToString()");
            WriteLine("{");
            Indent();
            WriteLine("return ToString(\"G\", CultureInfo.CurrentCulture);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current color to its equivalent string");
            WriteLine("/// representation by using the specified culture-specific");
            WriteLine("/// formatting information.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"provider\">An object that supplies culture-specific formatting information.</param>");
            WriteLine("/// <returns>The string representation of the current instance, as specified");
            WriteLine("/// by provider.</returns>");
            WriteLine("public string ToString(IFormatProvider provider)");
            WriteLine("{");
            Indent();
            WriteLine("return ToString(\"G\", provider);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current color to its equivalent string");
            WriteLine("/// representation by using the specified format for its components.");
            WriteLine("/// formatting information.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"format\">A standard or custom numeric format string.</param>");
            WriteLine("/// <returns>The string representation of the current instance.</returns>");
            WriteLine("/// <exception cref=\"System.FormatException\">format is not a valid format string.</exception>");
            WriteLine("public string ToString(string format)");
            WriteLine("{");
            Indent();
            WriteLine("return ToString(format, CultureInfo.CurrentCulture);");
            Dedent();
            WriteLine("}");

            var formatString = "Red:{0} Green:{1} Blue:{2} Alpha:{3}";
            var components = string.Join(", ",
                Components.Select(component => component + ".ToString(format, provider)"));

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current color to its equivalent string");
            WriteLine("/// representation by using the specified format and culture-specific");
            WriteLine("/// format information for its components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"format\">A standard or custom numeric format string.</param>");
            WriteLine("/// <returns>The string representation of the current instance, as specified");
            WriteLine("/// by format and provider.</returns>");
            WriteLine("/// <exception cref=\"System.FormatException\">format is not a valid format string.</exception>");
            WriteLine("public string ToString(string format, IFormatProvider provider)");
            WriteLine("{");
            Indent();
            WriteLine("return String.Format(\"{0}\", {1});", formatString, components);
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Functions()
        {
            WriteLine("/// <summary>");
            WriteLine("/// Provides static methods for color functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Color");
            WriteLine("{");
            Indent();

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to a Ibasa.IO.BinaryWriter.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this Ibasa.IO.BinaryWriter writer, {0} vector)", Name);
            WriteLine("{");
            Indent();
            foreach(var component in Components)
            {
                WriteLine("writer.Write(vector.{0});", component);
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

            #region Operations
            WriteLine("#region Operations");
            WriteLine("/// <summary>");
            WriteLine("/// Adds two colors and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first value to add.</param>");
            WriteLine("/// <param name=\"right\">The second value to add.</param>");
            WriteLine("/// <returns>The sum of left and right.</returns>");
            WriteLine("public static {0} Add({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Components.Select(component => string.Format("left.{0} + right.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Subtracts one colors from another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The value to subtract from (the minuend).</param>");
            WriteLine("/// <param name=\"right\">The value to subtract (the subtrahend).</param>");
            WriteLine("/// <returns>The result of subtracting right from left (the difference).</returns>");
            WriteLine("public static {0} Subtract({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Components.Select(component => string.Format("left.{0} - right.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a color and scalar.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"color\">The color to multiply.</param>");
            WriteLine("/// <param name=\"scalar\">The scalar to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} Multiply({0} color, {1} scalar)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Components.Select(component => string.Format("color.{0} * scalar", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Divides a color by a scalar and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"color\">The color to be divided (the dividend).</param>");
            WriteLine("/// <param name=\"scalar\">The scalar to divide by (the divisor).</param>");
            WriteLine("/// <returns>The result of dividing left by right (the quotient).</returns>");
            WriteLine("public static {0} Divide({0} color, {1} scalar)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Components.Select(component => string.Format("color.{0} / scalar", component))));
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two colors are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first color to compare.</param>");
            WriteLine("/// <param name=\"right\">The second color to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool Equals({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left == right;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Test
            WriteLine("#region Test");
            WriteLine("/// <summary>");
            WriteLine("/// Determines whether all components of a color are non-zero.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A color.</param>");
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
            WriteLine("/// Determines whether all components of a color satisfy a condition.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A color.</param>");
            WriteLine("/// <param name=\"predicate\">A function to test each component for a condition.</param>");
            WriteLine("/// <returns>true if every component of the color passes the test in the specified");
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
            WriteLine("/// Determines whether any component of a color is non-zero.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A color.</param>");
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
            WriteLine("/// Determines whether any components of a color satisfy a condition.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A color.</param>");
            WriteLine("/// <param name=\"predicate\">A function to test each component for a condition.</param>");
            WriteLine("/// <returns>true if any component of the color passes the test in the specified");
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

            #region Color transforms
            WriteLine("#region Negative, Premultiply and Normalize");
            WriteLine("/// <summary>");
            WriteLine("/// Returns the color negative of a normalized color.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"color\">A normalized color.</param>");
            WriteLine("/// <returns>The negative of color.</returns>");
            WriteLine("public static {0} Negative({0} color)", Name);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= color.R && color.R <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= color.G && color.G <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= color.B && color.B <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Ensures(0 <= Contract.Result<{0}>().R && Contract.Result<{0}>().R <= 1, \"Result must be normalized.\");", Name);
            WriteLine("Contract.Ensures(0 <= Contract.Result<{0}>().G && Contract.Result<{0}>().G <= 1, \"Result must be normalized.\");", Name);
            WriteLine("Contract.Ensures(0 <= Contract.Result<{0}>().B && Contract.Result<{0}>().B <= 1, \"Result must be normalized.\");", Name);
            WriteLine("return new {0}(1 - color.R, 1 - color.G, 1 - color.B, color.A);", Name);
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Multiplies the RGB values of the color by the alpha value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"color\">The color to premultiply.</param>");
            WriteLine("/// <returns>The premultipled color.</returns>");
            WriteLine("public static {0} Premultiply({0} color)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}(color.R * color.A, color.G * color.A, color.B * color.A, color.A);", Name);
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Normalizes a color so all its RGB values are in the range [0.0, 1.0].");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"color\">The color to normalize.</param>");
            WriteLine("/// <returns>The normalized color.</returns>");
            WriteLine("public static {0} Normalize({0} color)", Name);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Ensures(0 <= Contract.Result<{0}>().R && Contract.Result<{0}>().R <= 1, \"Result must be normalized.\");", Name);
            WriteLine("Contract.Ensures(0 <= Contract.Result<{0}>().G && Contract.Result<{0}>().G <= 1, \"Result must be normalized.\");", Name);
            WriteLine("Contract.Ensures(0 <= Contract.Result<{0}>().B && Contract.Result<{0}>().B <= 1, \"Result must be normalized.\");", Name);
            WriteLine("var bias = Functions.Min(Functions.Min(Functions.Min(color.R, color.G), color.B), 0);");
            WriteLine("color -= new {0}(bias, bias, bias, 0);", Name);
            WriteLine("var scale = Functions.Max(Functions.Max(Functions.Max(color.R, color.G), color.B), 1);");
            WriteLine("return color / scale;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Colorspace
            WriteLine("#region Colorspace");

            WriteLine("/// <summary>");
            WriteLine("/// Converts a color to greyscale.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"color\">The color to convert.</param>");
            WriteLine("/// <returns>color in greyscale.</returns>");
            WriteLine("public static {0} Greyscale({0} color)", Name);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= color.R && color.R <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= color.G && color.G <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= color.B && color.B <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Ensures(0 <= Contract.Result<{0}>().R && Contract.Result<{0}>().R <= 1, \"Result must be normalized.\");", Name);
            WriteLine("Contract.Ensures(0 <= Contract.Result<{0}>().G && Contract.Result<{0}>().G <= 1, \"Result must be normalized.\");", Name);
            WriteLine("Contract.Ensures(0 <= Contract.Result<{0}>().B && Contract.Result<{0}>().B <= 1, \"Result must be normalized.\");", Name);
            WriteLine("var greyscale = 0.2125{0} * color.R + 0.7154{0} * color.G + 0.0721{0} * color.B;", Type == NumberType.Float ? "f" : "");
            WriteLine("return new {0}(greyscale, greyscale, greyscale, color.A);", Name);
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Converts a color to black or white.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"color\">The color to convert.</param>");
            WriteLine("/// <returns>color in black or white.</returns>");
            WriteLine("public static {0} BlackWhite({0} color)", Name);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= color.R && color.R <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= color.G && color.G <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= color.B && color.B <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Ensures(0 <= Contract.Result<{0}>().R && Contract.Result<{0}>().R <= 1, \"Result must be normalized.\");", Name);
            WriteLine("Contract.Ensures(0 <= Contract.Result<{0}>().G && Contract.Result<{0}>().G <= 1, \"Result must be normalized.\");", Name);
            WriteLine("Contract.Ensures(0 <= Contract.Result<{0}>().B && Contract.Result<{0}>().B <= 1, \"Result must be normalized.\");", Name);
            WriteLine("var bw = Functions.Round(0.2125{0} * color.R + 0.7154{0} * color.G + 0.0721{0} * color.B);", Type == NumberType.Float ? "f" : "");
            WriteLine("return new {0}(bw, bw, bw, color.A);", Name);
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Gamma correct a color.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"color\">Color to gamma correct.</param>");
            WriteLine("/// <param name=\"gamma\">Gamma value to use.</param>");
            WriteLine("/// <returns>The gamma corrected color.</returns>");
            WriteLine("public static {0} Gamma({0} color, {1} gamma)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("var r = Functions.Pow(color.R, gamma);");
            WriteLine("var g = Functions.Pow(color.G, gamma);");
            WriteLine("var b = Functions.Pow(color.B, gamma);");
            WriteLine("var a = Functions.Pow(color.A, gamma);");
            WriteLine("return new {0}(r, g, b, a);", Name);
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
            #endregion

            #region Packing and quantization
            WriteLine("#region Quantization");
            WriteLine("public static Vector4l Quantize(int redBits, int greenBits, int blueBits, int alphaBits, {0} color)", Name);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= redBits && redBits <= 63, \"redBits must be between 0 and 63 inclusive.\");");
            WriteLine("Contract.Requires(0 <= greenBits && greenBits <= 63, \"greenBits must be between 0 and 63 inclusive.\");");
            WriteLine("Contract.Requires(0 <= blueBits && blueBits <= 63, \"blueBits must be between 0 and 63 inclusive.\");");
            WriteLine("Contract.Requires(0 <= alphaBits && alphaBits <= 63, \"alphaBits must be between 0 and 63 inclusive.\");");
            WriteLine("Contract.Requires(0 <= color.R && color.R <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= color.G && color.G <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= color.B && color.B <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= color.A && color.A <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= Contract.Result<Vector4l>().X, \"result must be positive.\");");
            WriteLine("Contract.Requires(0 <= Contract.Result<Vector4l>().Y, \"result must be positive.\");");
            WriteLine("Contract.Requires(0 <= Contract.Result<Vector4l>().Z, \"result must be positive.\");");
            WriteLine("Contract.Requires(0 <= Contract.Result<Vector4l>().W, \"result must be positive.\");");
            WriteLine("long r = (long)(color.R * long.MaxValue);");
            WriteLine("long g = (long)(color.G * long.MaxValue);");
            WriteLine("long b = (long)(color.B * long.MaxValue);");
            WriteLine("long a = (long)(color.A * long.MaxValue);");
            WriteLine("r >>= (63 - redBits);");
            WriteLine("g >>= (63 - greenBits);");
            WriteLine("b >>= (63 - blueBits);");
            WriteLine("a >>= (63 - alphaBits);");
            WriteLine("return new Vector4l(r, g, b, a);");
            Dedent();
            WriteLine("}");

            WriteLine("public static Vector3l Quantize(int redBits, int greenBits, int blueBits, {0} color)", Name);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= redBits && redBits <= 63, \"redBits must be between 0 and 63 inclusive.\");");
            WriteLine("Contract.Requires(0 <= greenBits && greenBits <= 63, \"greenBits must be between 0 and 63 inclusive.\");");
            WriteLine("Contract.Requires(0 <= blueBits && blueBits <= 63, \"blueBits must be between 0 and 63 inclusive.\");");
            WriteLine("Contract.Requires(0 <= color.R && color.R <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= color.G && color.G <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= color.B && color.B <= 1, \"color must be normalized.\");");
            WriteLine("Contract.Requires(0 <= Contract.Result<Vector3l>().X, \"result must be positive.\");", Name);
            WriteLine("Contract.Requires(0 <= Contract.Result<Vector3l>().Y, \"result must be positive.\");", Name);
            WriteLine("Contract.Requires(0 <= Contract.Result<Vector3l>().Z, \"result must be positive.\");", Name);
            WriteLine("long r = (long)(color.R * long.MaxValue);");
            WriteLine("long g = (long)(color.G * long.MaxValue);");
            WriteLine("long b = (long)(color.B * long.MaxValue);");
            WriteLine("r >>= (63 - redBits);");
            WriteLine("g >>= (63 - greenBits);");
            WriteLine("b >>= (63 - blueBits);");
            WriteLine("return new Vector3l(r, g, b);");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Per component
            WriteLine("#region Per component");

            WriteLine("#region Transform");
            foreach (var type in Types)
            {
                var transform = new Color(type);

                WriteLine("/// <summary>");
                WriteLine("/// Transforms the components of a color and returns the result.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The color to transform.</param>");
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
            WriteLine("/// Multiplys the components of two colors and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first color to modulate.</param>");
            WriteLine("/// <param name=\"right\">The second color to modulate.</param>");
            WriteLine("/// <returns>The result of multiplying each component of left by the matching component in right.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Modulate({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Components.Select(component => string.Format("left.{0} * right.{0}", component))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Returns the absolute value (per component).");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A color.</param>");
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
            WriteLine("/// Returns a color that contains the lowest value from each pair of components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value1\">The first color.</param>");
            WriteLine("/// <param name=\"value2\">The second color.</param>");
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
            WriteLine("/// Returns a color that contains the highest value from each pair of components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value1\">The first color.</param>");
            WriteLine("/// <param name=\"value2\">The second color.</param>");
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
            WriteLine("/// <param name=\"value\">A color to constrain.</param>");
            WriteLine("/// <param name=\"min\">The minimum values for each component.</param>");
            WriteLine("/// <param name=\"max\">The maximum values for each component.</param>");
            WriteLine("/// <returns>A color with each component constrained to the given range.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Clamp({0} value, {0} min, {0} max)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Components.Select(component => string.Format("Functions.Clamp(value.{0}, min.{0}, max.{0})", component))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Constrains each component to the range 0 to 1.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A color to saturate.</param>");
            WriteLine("/// <returns>A color with each component constrained to the range 0 to 1.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Saturate({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Components.Select(component => string.Format("Functions.Saturate(value.{0})", component))));
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Interpolation
            WriteLine("#region Interpolation");
            WriteLine("/// <summary>");
            WriteLine("/// Performs a linear interpolation between two colors.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"color1\">First color.</param>");
            WriteLine("/// <param name=\"color2\">Second color.</param>");
            WriteLine("/// <param name=\"amount\">Value between 0 and 1 indicating the weight of <paramref name=\"color2\"/>.</param>");
            WriteLine("/// <returns>The linear interpolation of the two values.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Lerp({0} color1, {0} color2, {1} amount)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return (1 - amount) * color1 + amount * color2;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
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
