using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Size : Generator
    {
        public NumberType Type { get; private set; }
        public int Dimension { get; private set; }
        public Component[] Components { get; private set; }

        public Size(NumberType type, int dimension)
        {
            Type = type;
            Dimension = dimension;
            Components = Shapes.SizeComponents(Dimension);
        }

        public string Name
        {
            get
            {
                return string.Format("Size{0}{1}", Dimension, Type.Suffix);
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
            if (Dimension == 2)
            {
                WriteLine("/// Represents an ordered pair of {0} width and height components that defines a", Type.IsReal ? "real" : "integer");
                WriteLine("/// size in a two-dimensional space.");
            }
            else
            {
                WriteLine("/// Represents an ordered triple of {0} width, height and depth components that defines a", Type.IsReal ? "real" : "integer");
                WriteLine("/// size in a three-dimensional space.");
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

            var zeros = string.Join(", ", Components.Select(c => "0"));
            var ones = string.Join(", ", Components.Select(c => "1"));

            WriteLine("/// <summary>");
            WriteLine("/// Returns a new <see cref=\"{0}\"/> with all of its components equal to zero.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} Empty = new {0}({1});", Name, zeros);

            WriteLine("/// <summary>");
            WriteLine("/// Returns a new <see cref=\"{0}\"/> with all of its components equal to one.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} Unit = new {0}({1});", Name, ones);

            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            foreach (var component in Components)
            {
                WriteLine("/// <summary>");
                WriteLine("/// The {0} component of the size.", component);
                WriteLine("/// </summary>");
                WriteLine("public readonly {0} {1};", Type, component);
            }

            WriteLine("#endregion");
        }

        void Properties()
        {
            //WriteLine("#region Properties");

            //WriteLine("#endregion");
        }

        void Constructors()
        {
            WriteLine("#region Constructors");

            if (Dimension == 3)
            {
                WriteLine("/// <summary>");
                WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified size and value.", Name);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A size containing the values with which to initialize the Width and Height components</param>");
                WriteLine("/// <param name=\"depth\">Value for the Depth component of the size.</param>");
                WriteLine("public {0}({1} value, {2} depth)", Name, new Size(Type, 2), Type);
                WriteLine("{");
                Indent();
                WriteLine("Contract.Requires(0 <= depth);");
                WriteLine("Width = value.Width;");
                WriteLine("Height = value.Height;");
                WriteLine("Depth = depth;");
                Dedent();
                WriteLine("}");
            }

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified values.", Name);
            WriteLine("/// </summary>");
            foreach (var component in Components)
            {
                WriteLine("/// <param name=\"{0}\">Value for the {1} component of the size.</param>", component.Name.ToLower(), component);
            }
            WriteLine("public {0}({1})", Name, string.Join(", ", Components.Select(component => Type + " " + component.Name.ToLower())));
            WriteLine("{");
            Indent();
            foreach (var component in Components)
            {
                WriteLine("Contract.Requires(0 <= {0});", component.Name.ToLower());
            }
            foreach (var component in Components)
            {
                WriteLine("{0} = {1};", component, component.Name.ToLower());
            }
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Operations()
        {
            var result = new Size(Type.PositiveType, Dimension);

            WriteLine("#region Operations");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a size and scalar.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The size to multiply.</param>");
            WriteLine("/// <param name=\"right\">The scalar to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} operator *({1} left, {2} right)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= right);");
            WriteLine("return Size.Multiply(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a scalar and size.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The scalar to multiply.</param>");
            WriteLine("/// <param name=\"right\">The size to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} operator *({1} left, {2} right)", result, result.Type, Name);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= left);");
            WriteLine("return Size.Multiply(right, left);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Divides a size by a scalar and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The size to be divided (the dividend).</param>");
            WriteLine("/// <param name=\"right\">The scalar to divide by (the divisor).</param>");
            WriteLine("/// <returns>The result of dividing left by right (the quotient).</returns>");
            WriteLine("public static {0} operator /({1} left, {2} right)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= right);");
            WriteLine("return Size.Divide(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Conversions()
        {
            WriteLine("#region Conversions");

            foreach (var type in Shapes.Types.Where(t => t != Type))
            {
                string imex = type.IsImplicitlyConvertibleTo(Type) ? "implicit" : "explicit";

                Size other = new Size(type, Dimension);

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
            WriteLine("/// size have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The size to compare.</param>");
            WriteLine("/// <returns>true if this size and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two sizes are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first size to compare.</param>");
            WriteLine("/// <param name=\"right\">The second size to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" & ", Components.Select(component => string.Format("left.{0} == right.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two sizes are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first size to compare.</param>");
            WriteLine("/// <param name=\"right\">The second size to compare.</param>");
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
            WriteLine("/// Converts the value of the current size to its equivalent string");
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
            WriteLine("/// Converts the value of the current size to its equivalent string");
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
            WriteLine("/// Converts the value of the current size to its equivalent string");
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
                Components.Select(component => component.Name + ": {" + component.Index + "}"));
            var components = string.Join(", ", 
                Components.Select(component => component + ".ToString(format, provider)"));

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current size to its equivalent string");
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
            WriteLine("/// Provides static methods for size functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Size");
            WriteLine("{");
            Indent();

            var result = new Size(Type.PositiveType, Dimension);

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to an <see cref=\"Ibasa.IO.BinaryWriter\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this Ibasa.IO.BinaryWriter writer, {0} size)", Name);
            WriteLine("{");
            Indent();
            for (int i = 0; i < Dimension; ++i)
            {
                WriteLine("writer.Write(size.{0});", Components[i]);
            }
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> from an <see cref=\"Ibasa.IO.BinaryWriter\">.", Name);
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
            WriteLine("/// Returns the product of a size and scalar.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"size\">The size to multiply.</param>");
            WriteLine("/// <param name=\"scalar\">The scalar to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} Multiply({1} size, {2} scalar)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= scalar);");
            WriteLine("return new {0}({1});", result,
                string.Join(", ", Components.Select(component => string.Format("size.{0} * scalar", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Divides a size by a scalar and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"size\">The size to be divided (the dividend).</param>");
            WriteLine("/// <param name=\"scalar\">The scalar to divide by (the divisor).</param>");
            WriteLine("/// <returns>The result of dividing left by right (the quotient).</returns>");
            WriteLine("public static {0} Divide({1} size, {2} scalar)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= scalar);");
            WriteLine("return new {0}({1});", result,
                string.Join(", ", Components.Select(component => string.Format("size.{0} / scalar", component))));
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two sizes are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first size to compare.</param>");
            WriteLine("/// <param name=\"right\">The second size to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool Equals({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left == right;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Per component
            WriteLine("#region Per component");

            WriteLine("#region Transform");
            foreach (var type in Shapes.Types)
            {
                var transform = new Size(type, Dimension);

                WriteLine("/// <summary>");
                WriteLine("/// Transforms the components of a size and returns the result.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The size to transform.</param>");
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
            WriteLine("/// Returns a size that contains the lowest value from each pair of components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value1\">The first size.</param>");
            WriteLine("/// <param name=\"value2\">The second size.</param>");
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
            WriteLine("/// Returns a size that contains the highest value from each pair of components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value1\">The first size.</param>");
            WriteLine("/// <param name=\"value2\">The second size.</param>");
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
            WriteLine("/// <param name=\"value\">A size to constrain.</param>");
            WriteLine("/// <param name=\"min\">The minimum values for each component.</param>");
            WriteLine("/// <param name=\"max\">The maximum values for each component.</param>");
            WriteLine("/// <returns>A size with each component constrained to the given range.</returns>");
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
                WriteLine("/// <param name=\"value\">A size to saturate.</param>");
                WriteLine("/// <returns>A size with each component constrained to the range 0 to 1.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Saturate({0} value)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name, 
                    string.Join(", ", Components.Select(component => string.Format("Functions.Saturate(value.{0})", component))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a size where each component is the smallest integral value that");
                WriteLine("/// is greater than or equal to the specified component.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A size.</param>");
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
                WriteLine("/// Returns a size where each component is the largest integral value that");
                WriteLine("/// is less than or equal to the specified component.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A size.</param>");
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
                WriteLine("/// Returns a size where each component is the integral part of the specified component.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A size.</param>");
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
                WriteLine("/// Returns a size where each component is the fractional part of the specified component.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A size.</param>");
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
                WriteLine("/// Returns a size where each component is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A size.</param>");
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
                WriteLine("/// Returns a size where each component is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A size.</param>");
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
                WriteLine("/// Returns a size where each component is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A size.</param>");
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
                WriteLine("/// Returns a size where each component is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A size.</param>");
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
            }
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
