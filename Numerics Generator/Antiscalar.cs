using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Antiscalar : Generator
    {
        public NumberType Type { get; private set; }
        public int Dimension { get; private set; }
        public Grade Grade { get; private set; }

        public Antiscalar(NumberType type, int dimension)
        {
            Type = type;
            Dimension = dimension;
            Grade = new Grade(Numerics_Generator.Grade.Grades(dimension) - 1, Dimension);
        }

        public string Name { get { return "Antiscalar" + Type.Suffix; } }

        public void Generate()
        {
            Namespace();
        }

        void Namespace()
        {
            WriteLine("using System;");
            WriteLine("using System.Globalization;");
            WriteLine("using System.Runtime.InteropServices;");
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
            WriteLine("/// Represents a {0} dimensional {1} antiscalar.", Dimension.Name(), Type, Name);
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
            WriteLine("/// Returns a new <see cref=\"{0}\"/> equal to zero.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} Zero = new {0}(0);", Name);

            WriteLine("/// <summary>");
            WriteLine("/// Returns a new <see cref=\"{0}\"/> equal to one.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} One = new {0}(1);", Name);

            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");
    
            WriteLine("/// <summary>");
            WriteLine("/// The value of the antiscalar.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Value;", Type);

            WriteLine("#endregion");
        }

        void Properties()
        {
            WriteLine("#region Properties");
            
            WriteLine("#endregion");
        }

        void Constructors()
        {
            WriteLine("#region Constructors");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified value.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">The value that will be assigned to this instance.</param>");
            WriteLine("private {0}({1} value)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("Value = value;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Operations()
        {
            var result = new Antiscalar(Type.PositiveType, Dimension);

            WriteLine("#region Operations");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the identity of a specified antiscalar.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">An antiscalar.</param>");
            WriteLine("/// <returns>The identity of value.</returns>");
            WriteLine("public static {0} operator +({1} value)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return value;");
            Dedent();
            WriteLine("}");

            if (Type.NegativeType != NumberType.Invalid)
            {
                var negAntiscalar = new Antiscalar(Type.NegativeType, Dimension);

                WriteLine("/// <summary>");
                WriteLine("/// Returns the additive inverse of a specified antiscalar.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">An antiscalar.</param>");
                WriteLine("/// <returns>The negative of value.</returns>");
                WriteLine("public static {0} operator -({1} value)", negAntiscalar, Name);
                WriteLine("{");
                Indent();
                WriteLine("return Antiscalar.Negative(value);");
                Dedent();
                WriteLine("}");
            }

            WriteLine("/// <summary>");
            WriteLine("/// Adds two antiscalars and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first value to add.</param>");
            WriteLine("/// <param name=\"right\">The second value to add.</param>");
            WriteLine("/// <returns>The sum of left and right.</returns>");
            WriteLine("public static {0} operator +({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Antiscalar.Add(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Subtracts one antiscalar from another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The value to subtract from (the minuend).</param>");
            WriteLine("/// <param name=\"right\">The value to subtract (the subtrahend).</param>");
            WriteLine("/// <returns>The result of subtracting right from left (the difference).</returns>");
            WriteLine("public static {0} operator -({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Antiscalar.Subtract(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Conversions()
        {
            WriteLine("#region Conversions");

            foreach (var type in Exterior.Types)
            {
                string imex = type.IsImplicitlyConvertibleTo(Type) ? "implicit" : "explicit";

                Antiscalar other = new Antiscalar(type, Dimension);

                var casts = string.Format("({0})value.Value", Type);

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

            foreach (var type in NumberType.Types)
            {
                WriteLine("/// <summary>");
                WriteLine("/// Defines an explicit conversion of a {0} value to a {1}.", type, Name);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The value to convert to a {0}.</param>", Name);
                WriteLine("/// <returns>A {0} that is equal to value.</returns>", Name);
                if (Type.IsCLSCompliant && !type.IsCLSCompliant)
                {
                    WriteLine("[CLSCompliant(false)]");
                }
                WriteLine("public static explicit operator {0}({1} value)", Name, type);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}(({1})value.Value);", Name, Type);
                Dedent();
                WriteLine("}");

                WriteLine("/// <summary>");
                WriteLine("/// Defines an explicit conversion of a {0} value to a {1}.", Name, type);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The value to convert to a {0}.</param>", type);
                WriteLine("/// <returns>A {0} that is equal to value.</returns>", type);
                if (Type.IsCLSCompliant && !type.IsCLSCompliant)
                {
                    WriteLine("[CLSCompliant(false)]");
                }
                WriteLine("public static explicit operator {0}({1} value)", type, Name);
                WriteLine("{");
                Indent();
                WriteLine("return ({0})(value.Value);", type);
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
            WriteLine("return Value.GetHashCode();");
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
            WriteLine("/// antiscalar have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The antiscalar to compare.</param>");
            WriteLine("/// <returns>true if this antiscalar and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two antiscalars are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first antiscalar to compare.</param>");
            WriteLine("/// <param name=\"right\">The second antiscalar to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.Value == right.Value;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two antiscalars are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first antiscalar to compare.</param>");
            WriteLine("/// <param name=\"right\">The second antiscalar to compare.</param>");
            WriteLine("/// <returns>true if the left and right are not equal; otherwise, false.</returns>");
            WriteLine("public static bool operator !=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.Value != right.Value;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void String()
        {
            WriteLine("#region ToString");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current antiscalar to its equivalent string");
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
            WriteLine("/// Converts the value of the current antiscalar to its equivalent string");
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
            WriteLine("/// Converts the value of the current antiscalar to its equivalent string");
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

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current antiscalar to its equivalent string");
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
            WriteLine("return Value.ToString(format, provider);");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Functions()
        {
            WriteLine("/// <summary>");
            WriteLine("/// Provides static methods for antiscalar functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Antiscalar");
            WriteLine("{");
            Indent();

            var result = new Antiscalar(Type.PositiveType, Dimension);

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to a System.IO.BinaryWriter.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this System.IO.BinaryWriter writer, {0} antiscalar)", Name);
            WriteLine("{");
            Indent();
            WriteLine("writer.Write(antiscalar.Value);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> to a System.IO.BinaryReader.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static {0} Read{0}(this System.IO.BinaryReader reader)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, string.Format("reader.Read{0}()", Type.CLRName));
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Operations
            WriteLine("#region Operations");
            if (Type.NegativeType != NumberType.Invalid)
            {
                var negAntiscalar = new Antiscalar(Type.NegativeType, Dimension);

                WriteLine("/// <summary>");
                WriteLine("/// Returns the additive inverse of a antiscalar.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A antiscalar.</param>");
                WriteLine("/// <returns>The negative of value.</returns>");
                WriteLine("public static {0} Negative({1} value)", negAntiscalar, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}(value.Value);", negAntiscalar);
                Dedent();
                WriteLine("}");
            }

            WriteLine("/// <summary>");
            WriteLine("/// Adds two antiscalars and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first value to add.</param>");
            WriteLine("/// <param name=\"right\">The second value to add.</param>");
            WriteLine("/// <returns>The sum of left and right.</returns>");
            WriteLine("public static {0} Add({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}(left.Value + right.Value);", result);
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Subtracts one antiscalars from another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The value to subtract from (the minuend).</param>");
            WriteLine("/// <param name=\"right\">The value to subtract (the subtrahend).</param>");
            WriteLine("/// <returns>The result of subtracting right from left (the difference).</returns>");
            WriteLine("public static {0} Subtract({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}(left.Value - right.Value);");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two antiscalars are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first antiscalar to compare.</param>");
            WriteLine("/// <param name=\"right\">The second antiscalar to compare.</param>");
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
            
            WriteLine("#endregion");
            #endregion

            #region Per component
            WriteLine("#region Per component");
            //WriteLine("/// <summary>");
            //WriteLine("/// Returns the absolute value (per component).");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"value\">A antiscalar.</param>");
            //WriteLine("/// <returns>The absolute value (per component) of value.</returns>");
            //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //WriteLine("public static {0} Abs({0} value)", Name);
            //WriteLine("{");
            //Indent();
            //WriteLine("return new {0}({1});", Name,
            //    string.Join(", ", Components.Select(component => string.Format("Functions.Abs(value.{0})", component))));
            //Dedent();
            //WriteLine("}");
            //WriteLine("/// <summary>");
            //WriteLine("/// Returns a antiscalar that contains the lowest value from each pair of components.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"value1\">The first antiscalar.</param>");
            //WriteLine("/// <param name=\"value2\">The second antiscalar.</param>");
            //WriteLine("/// <returns>The lowest of each component in left and the matching component in right.</returns>");
            //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //WriteLine("public static {0} Min({0} value1, {0} value2)", Name);
            //WriteLine("{");
            //Indent();
            //WriteLine("return new {0}({1});", Name,
            //    string.Join(", ", Components.Select(component => string.Format("Functions.Min(value1.{0}, value2.{0})", component))));
            //Dedent();
            //WriteLine("}");
            //WriteLine("/// <summary>");
            //WriteLine("/// Returns a antiscalar that contains the highest value from each pair of components.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"value1\">The first antiscalar.</param>");
            //WriteLine("/// <param name=\"value2\">The second antiscalar.</param>");
            //WriteLine("/// <returns>The highest of each component in left and the matching component in right.</returns>");
            //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //WriteLine("public static {0} Max({0} value1, {0} value2)", Name);
            //WriteLine("{");
            //Indent();
            //WriteLine("return new {0}({1});", Name,
            //    string.Join(", ", Components.Select(component => string.Format("Functions.Max(value1.{0}, value2.{0})", component))));
            //Dedent();
            //WriteLine("}");
            //WriteLine("/// <summary>");
            //WriteLine("/// Constrains each component to a given range.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"value\">A antiscalar to constrain.</param>");
            //WriteLine("/// <param name=\"min\">The minimum values for each component.</param>");
            //WriteLine("/// <param name=\"max\">The maximum values for each component.</param>");
            //WriteLine("/// <returns>A antiscalar with each component constrained to the given range.</returns>");
            //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //WriteLine("public static {0} Clamp({0} value, {0} min, {0} max)", Name);
            //WriteLine("{");
            //Indent();
            //WriteLine("return new {0}({1});", Name,
            //    string.Join(", ", Components.Select(component => string.Format("Functions.Clamp(value.{0}, min.{0}, max.{0})", component))));
            //Dedent();
            //WriteLine("}");
            //if (Type.IsReal)
            //{
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Constrains each component to the range 0 to 1.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A antiscalar to saturate.</param>");
            //    WriteLine("/// <returns>A antiscalar with each component constrained to the range 0 to 1.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Saturate({0} value)", Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", Name,
            //        string.Join(", ", Components.Select(component => string.Format("Functions.Saturate(value.{0})", component))));
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Returns a antiscalar where each component is the smallest integral value that");
            //    WriteLine("/// is greater than or equal to the specified component.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A antiscalar.</param>");
            //    WriteLine("/// <returns>The ceiling of value.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Ceiling({1} value)", result, Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", result,
            //        string.Join(", ", Components.Select(component => string.Format("Functions.Ceiling(value.{0})", component))));
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Returns a antiscalar where each component is the largest integral value that");
            //    WriteLine("/// is less than or equal to the specified component.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A antiscalar.</param>");
            //    WriteLine("/// <returns>The floor of value.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Floor({1} value)", result, Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", result,
            //        string.Join(", ", Components.Select(component => string.Format("Functions.Floor(value.{0})", component))));
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Returns a antiscalar where each component is the integral part of the specified component.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A antiscalar.</param>");
            //    WriteLine("/// <returns>The integral of value.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Truncate({1} value)", result, Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", result,
            //        string.Join(", ", Components.Select(component => string.Format("Functions.Truncate(value.{0})", component))));
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Returns a antiscalar where each component is the fractional part of the specified component.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A antiscalar.</param>");
            //    WriteLine("/// <returns>The fractional of value.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Fractional({1} value)", result, Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", result,
            //        string.Join(", ", Components.Select(component => string.Format("Functions.Fractional(value.{0})", component))));
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Returns a antiscalar where each component is rounded to the nearest integral value.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A antiscalar.</param>");
            //    WriteLine("/// <returns>The result of rounding value.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Round({1} value)", result, Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", result,
            //        string.Join(", ", Components.Select(component => string.Format("Functions.Round(value.{0})", component))));
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Returns a antiscalar where each component is rounded to the nearest integral value.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A antiscalar.</param>");
            //    WriteLine("/// <param name=\"digits\">The number of fractional digits in the return value.</param>");
            //    WriteLine("/// <returns>The result of rounding value.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Round({1} value, int digits)", result, Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", result,
            //        string.Join(", ", Components.Select(component => string.Format("Functions.Round(value.{0}, digits)", component))));
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Returns a antiscalar where each component is rounded to the nearest integral value.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A antiscalar.</param>");
            //    WriteLine("/// <param name=\"mode\">Specification for how to round value if it is midway between two other numbers.</param>");
            //    WriteLine("/// <returns>The result of rounding value.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Round({1} value, MidpointRounding mode)", result, Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", result,
            //        string.Join(", ", Components.Select(component => string.Format("Functions.Round(value.{0}, mode)", component))));
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Returns a antiscalar where each component is rounded to the nearest integral value.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A antiscalar.</param>");
            //    WriteLine("/// <param name=\"digits\">The number of fractional digits in the return value.</param>");
            //    WriteLine("/// <param name=\"mode\">Specification for how to round value if it is midway between two other numbers.</param>");
            //    WriteLine("/// <returns>The result of rounding value.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Round({1} value, int digits, MidpointRounding mode)", result, Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", result,
            //        string.Join(", ", Components.Select(component => string.Format("Functions.Round(value.{0}, digits, mode)", component))));
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Calculates the reciprocal of each component in the antiscalar.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A antiscalar.</param>");
            //    WriteLine("/// <returns>A antiscalar with the reciprocal of each of values components.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Reciprocal({1} value)", result, Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", result,
            //        string.Join(", ", Components.Select(component => string.Format("1 / value.{0}", component))));
            //    Dedent();
            //    WriteLine("}");
            //}
            WriteLine("#endregion");
            #endregion
            
            #region Interpolation
            if (Type.IsReal)
            {
                WriteLine("#region Interpolation");
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
