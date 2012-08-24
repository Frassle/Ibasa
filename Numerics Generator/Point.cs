using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Point : Generator
    {
        public NumberType Type { get; private set; }
        public int Dimension { get; private set; }
        public Component[] Components { get; private set; }

        public Point(NumberType type, int dimension)
        {
            Type = type;
            Dimension = dimension;
            Components = Component.Components(Dimension);
        }

        public string Name
        {
            get
            {
                return string.Format("Point{0}{1}", Dimension, Type.Suffix);
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
                WriteLine("/// Represents an ordered pair of {0} x and y coordinates that defines a", Type.IsReal ? "real" : "integer");
                WriteLine("/// point in a two-dimensional space.");
            }
            else
            {
                WriteLine("/// Represents an ordered triple of {0} x, y and z coordinates that defines a", Type.IsReal ? "real" : "integer");
                WriteLine("/// point in a three-dimensional space.");
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

            WriteLine("/// <summary>");
            WriteLine("/// Returns the point ({0}).", zeros);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} Zero = new {0}({1});", Name, zeros);

            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            foreach (var component in Components)
            {
                WriteLine("/// <summary>");
                WriteLine("/// The {0} coordinate of the point.", component);
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
            WriteLine("/// Returns the indexed coordinate of this point.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"index\">The index of the coordinate.</param>");
            WriteLine("/// <returns>The value of the indexed coordinate.</returns>");
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

            WriteLine("#endregion");
        }

        void Constructors()
        {
            WriteLine("#region Constructors");

            if (Dimension == 3)
            {
                WriteLine("/// <summary>");
                WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified point and value.", Name);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A point containing the values with which to initialize the X and Y coordinates</param>");
                WriteLine("/// <param name=\"z\">Value for the Z coordinate of the point.</param>");
                WriteLine("public {0}({1} value, {2} z)", Name, new Point(Type, 2), Type);
                WriteLine("{");
                WriteLine("\tX = value.X;");
                WriteLine("\tY = value.Y;");
                WriteLine("\tZ = z;");
                WriteLine("}");
            }

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified values.", Name);
            WriteLine("/// </summary>");
            foreach (var component in Components)
            {
                WriteLine("/// <param name=\"{0}\">Value for the {1} coordinate of the point.</param>", component.Name.ToLower(), component);
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
            WriteLine("/// <param name=\"array\">Array of values for the point.</param>");
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
            WriteLine("/// <param name=\"array\">Array of values for the point.</param>");
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
            var result = new Point(Type.PositiveType, Dimension);
            var vector = new Vector(Type.PositiveType, Dimension);

            WriteLine("#region Operations");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the identity of a specified point.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A point.</param>");
            WriteLine("/// <returns>The identity of value.</returns>");
            WriteLine("public static {0} operator +({1} value)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return value;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Adds a point and a vector and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"point\">The point value to add.</param>");
            WriteLine("/// <param name=\"vector\">The vector value to add.</param>");
            WriteLine("/// <returns>The sum of left and right.</returns>");
            WriteLine("public static {0} operator +({1} point, {2} vector)", result, Name, vector);
            WriteLine("{");
            Indent();
            WriteLine("return Point.Add(point, vector);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Adds a vector and a point and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"vector\">The vector value to add.</param>");
            WriteLine("/// <param name=\"point\">The point value to add.</param>");
            WriteLine("/// <returns>The sum of left and right.</returns>");
            WriteLine("public static {0} operator +({1} vector, {2} point)", result, vector, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Point.Add(point, vector);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Subtracts one point from another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The value to subtract from (the minuend).</param>");
            WriteLine("/// <param name=\"right\">The value to subtract (the subtrahend).</param>");
            WriteLine("/// <returns>The result of subtracting right from left (the difference).</returns>");
            WriteLine("public static {0} operator -({1} left, {1} right)", new Vector(result.Type, Dimension), Name);
            WriteLine("{");
            Indent();
            WriteLine("return Point.Subtract(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Subtracts a vector from a point and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"point\">The point value to subtract from (the minuend).</param>");
            WriteLine("/// <param name=\"vector\">The vector value to subtract (the subtrahend).</param>");
            WriteLine("/// <returns>The result of subtracting vector from point (the difference).</returns>");
            WriteLine("public static {0} operator -({1} point, {2} vector)", result, Name, vector);
            WriteLine("{");
            Indent();
            WriteLine("return Point.Subtract(point, vector);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a point and scalar.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The point to multiply.</param>");
            WriteLine("/// <param name=\"right\">The scalar to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} operator *({1} left, {2} right)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("return Point.Multiply(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a scalar and point.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The scalar to multiply.</param>");
            WriteLine("/// <param name=\"right\">The point to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} operator *({1} left, {2} right)", result, result.Type, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Point.Multiply(right, left);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Divides a point by a scalar and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The point to be divided (the dividend).</param>");
            WriteLine("/// <param name=\"right\">The scalar to divide by (the divisor).</param>");
            WriteLine("/// <returns>The result of dividing left by right (the quotient).</returns>");
            WriteLine("public static {0} operator /({1} left, {2} right)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("return Point.Divide(left, right);");
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

                Point other = new Point(type, Dimension);

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

            var vector = new Vector(Type, Dimension);

            WriteLine("/// <summary>");
            WriteLine("/// Defines an explicit conversion of a {0} value to a {1}.", vector, Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">The value to convert to a {0}.</param>", Name);
            WriteLine("/// <returns>A {0} that has all components equal to value.</returns>", Name);
            WriteLine("public static explicit operator {0}({1} value)", Name, vector);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, string.Join(", ", Components.Select(component => string.Format("value.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Defines an explicit conversion of a {0} value to a {1}.", Name, vector);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">The value to convert to a {0}.</param>", vector);
            WriteLine("/// <returns>A {0} that has all components equal to value.</returns>", vector);
            WriteLine("public static explicit operator {0}({1} value)", vector, Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", vector, string.Join(", ", Components.Select(component => string.Format("value.{0}", component))));
            Dedent();
            WriteLine("}");
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
            WriteLine("/// point have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The point to compare.</param>");
            WriteLine("/// <returns>true if this point and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two points are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first point to compare.</param>");
            WriteLine("/// <param name=\"right\">The second point to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" & ", Components.Select(component => string.Format("left.{0} == right.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two points are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first point to compare.</param>");
            WriteLine("/// <param name=\"right\">The second point to compare.</param>");
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
            WriteLine("/// Converts the value of the current point to its equivalent string");
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
            WriteLine("/// Converts the value of the current point to its equivalent string");
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
            WriteLine("/// Converts the value of the current point to its equivalent string");
            WriteLine("/// representation in Cartesian form by using the specified format for its coordinates.");
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
            WriteLine("/// Converts the value of the current point to its equivalent string");
            WriteLine("/// representation in Cartesian form by using the specified format and culture-specific");
            WriteLine("/// format information for its coordinates.");
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
            WriteLine("/// Provides static methods for point functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Point");
            WriteLine("{");
            Indent();

            var result = new Point(Type.PositiveType, Dimension);
            var vector = new Vector(Type.PositiveType, Dimension);

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to a System.IO.BinaryWriter.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this System.IO.BinaryWriter writer, {0} point)", Name);
            WriteLine("{");
            Indent();
            for (int i = 0; i < Dimension; ++i)
            {
                WriteLine("writer.Write(point.{0});", Components[i]);
            }
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> to a System.IO.BinaryReader.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static {0} Read{0}(this System.IO.BinaryReader reader)", Name);
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
            WriteLine("/// Adds a point and a vector and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"point\">The point value to add.</param>");
            WriteLine("/// <param name=\"vector\">The vector value to add.</param>");
            WriteLine("/// <returns>The sum of left and right.</returns>");
            WriteLine("public static {0} Add({1} point, {2} vector)", result, Name, vector);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", result,
                string.Join(", ", Components.Select(component => string.Format("point.{0} + vector.{0}", component))));
            Dedent();
            WriteLine("}");

            var subvec = new Vector(result.Type, Dimension);
            WriteLine("/// <summary>");
            WriteLine("/// Subtracts one points from another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The value to subtract from (the minuend).</param>");
            WriteLine("/// <param name=\"right\">The value to subtract (the subtrahend).</param>");
            WriteLine("/// <returns>The result of subtracting right from left (the difference).</returns>");
            WriteLine("public static {0} Subtract({1} left, {1} right)", subvec, Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", subvec,
                string.Join(", ", Components.Select(component => string.Format("left.{0} - right.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Subtracts a vector from a point and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"point\">The point value to subtract from (the minuend).</param>");
            WriteLine("/// <param name=\"vector\">The vector value to subtract (the subtrahend).</param>");
            WriteLine("/// <returns>The result of subtracting vector from point (the difference).</returns>");
            WriteLine("public static {0} Subtract({1} point, {2} vector)", result, Name, vector);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", result,
                string.Join(", ", Components.Select(component => string.Format("point.{0} - vector.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a point and scalar.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"point\">The point to multiply.</param>");
            WriteLine("/// <param name=\"scalar\">The scalar to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} Multiply({1} point, {2} scalar)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", result,
                string.Join(", ", Components.Select(component => string.Format("point.{0} * scalar", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Divides a point by a scalar and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"point\">The point to be divided (the dividend).</param>");
            WriteLine("/// <param name=\"scalar\">The scalar to divide by (the divisor).</param>");
            WriteLine("/// <returns>The result of dividing left by right (the quotient).</returns>");
            WriteLine("public static {0} Divide({1} point, {2} scalar)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", result,
                string.Join(", ", Components.Select(component => string.Format("point.{0} / scalar", component))));
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two points are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first point to compare.</param>");
            WriteLine("/// <param name=\"right\">The second point to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool Equals({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left == right;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Distance
            WriteLine("#region Distance");
            WriteLine("/// <summary>");
            WriteLine("/// Returns the distance between two points.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value1\">The first point.</param>");
            WriteLine("/// <param name=\"value2\">The second point.</param>");
            WriteLine("/// <returns>The distance between value1 and value2.</returns>");
            WriteLine("public static {0} Distance({1} value1, {1} value2)", Type.RealType, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Vector.Absolute(value2 - value1);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Returns the squared distance between two points.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value1\">The first point.</param>");
            WriteLine("/// <param name=\"value2\">The second point.</param>");
            WriteLine("/// <returns>The squared distance between value1 and value2.</returns>");
            WriteLine("public static {0} DistanceSquared({1} value1, {1} value2)", result.Type, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Vector.AbsoluteSquared(value2 - value1);");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Test
            WriteLine("#region Test");
            WriteLine("/// <summary>");
            WriteLine("/// Determines whether all components of a point are non-zero.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A point.</param>");
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
            WriteLine("/// Determines whether all components of a point satisfy a condition."); 
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A point.</param>");
            WriteLine("/// <param name=\"predicate\">A function to test each component for a condition.</param>");
            WriteLine("/// <returns>true if every component of the point passes the test in the specified");
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
            WriteLine("/// Determines whether any component of a point is non-zero.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A point.</param>");
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
            WriteLine("/// Determines whether any components of a point satisfy a condition.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A point.</param>");
            WriteLine("/// <param name=\"predicate\">A function to test each component for a condition.</param>");
            WriteLine("/// <returns>true if any component of the point passes the test in the specified");
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

            #region Per component
            WriteLine("#region Per component");

            WriteLine("#region Transform");
            foreach (var type in Shapes.Types)
            {
                var transform = new Point(type, Dimension);

                WriteLine("/// <summary>");
                WriteLine("/// Transforms the components of a point and returns the result.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The point to transform.</param>");
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
            WriteLine("/// Multiplys the components of two points and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first point to modulate.</param>");
            WriteLine("/// <param name=\"right\">The second point to modulate.</param>");
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
            WriteLine("/// <param name=\"value\">A point.</param>");
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
            WriteLine("/// Returns a point that contains the lowest value from each pair of components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value1\">The first point.</param>");
            WriteLine("/// <param name=\"value2\">The second point.</param>");
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
            WriteLine("/// Returns a point that contains the highest value from each pair of components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value1\">The first point.</param>");
            WriteLine("/// <param name=\"value2\">The second point.</param>");
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
            WriteLine("/// <param name=\"value\">A point to constrain.</param>");
            WriteLine("/// <param name=\"min\">The minimum values for each component.</param>");
            WriteLine("/// <param name=\"max\">The maximum values for each component.</param>");
            WriteLine("/// <returns>A point with each component constrained to the given range.</returns>");
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
                WriteLine("/// <param name=\"value\">A point to saturate.</param>");
                WriteLine("/// <returns>A point with each component constrained to the range 0 to 1.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Saturate({0} value)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name, 
                    string.Join(", ", Components.Select(component => string.Format("Functions.Saturate(value.{0})", component))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a point where each component is the smallest integral value that");
                WriteLine("/// is greater than or equal to the specified component.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A point.</param>");
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
                WriteLine("/// Returns a point where each component is the largest integral value that");
                WriteLine("/// is less than or equal to the specified component.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A point.</param>");
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
                WriteLine("/// Returns a point where each component is the integral part of the specified component.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A point.</param>");
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
                WriteLine("/// Returns a point where each component is the fractional part of the specified component.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A point.</param>");
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
                WriteLine("/// Returns a point where each component is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A point.</param>");
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
                WriteLine("/// Returns a point where each component is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A point.</param>");
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
                WriteLine("/// Returns a point where each component is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A point.</param>");
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
                WriteLine("/// Returns a point where each component is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A point.</param>");
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
                WriteLine("/// Calculates the reciprocal of each component in the point.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A point.</param>");
                WriteLine("/// <returns>A point with the reciprocal of each of values components.</returns>");
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

            #region Barycentric
            //if(Type.IsReal)
            //{
            //    WriteLine("#region Barycentric");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Returns the Cartesian coordinate for one axis of a point that is defined");
            //    WriteLine("/// by a given triangle and two normalized barycentric (areal) coordinates.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value1\">The coordinate of vertex 1 of the defining triangle.</param>");
            //    WriteLine("/// <param name=\"value2\">The coordinate of vertex 2 of the defining triangle.</param>");
            //    WriteLine("/// <param name=\"value3\">The coordinate of vertex 3 of the defining triangle.</param>");
            //    WriteLine("/// <param name=\"amount1\">The normalized barycentric (areal) coordinate b2, equal to the weighting");
            //    WriteLine("/// factor for vertex 2, the coordinate of which is specified in value2.</param>");
            //    WriteLine("/// <param name=\"amount2\">The normalized barycentric (areal) coordinate b3, equal to the weighting");
            //    WriteLine("/// factor for vertex 3, the coordinate of which is specified in value3.</param>");
            //    WriteLine("/// <returns>Cartesian coordinate of the specified point.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Barycentric({1} value1, {1} value2, {1} value3, {2} amount1, {2} amount2)", result, Name, result.Type);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return ((1 - amount1 - amount2) * value1) + (amount1 * value2) + (amount2 * value3);");
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("#endregion");
            //}
            #endregion

            #region Interpolation
            if(Type.IsReal)
            {
                WriteLine("#region Interpolation");
                WriteLine("/// <summary>");
                WriteLine("/// Performs a linear interpolation between two points.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"point1\">First point.</param>");
                WriteLine("/// <param name=\"point2\">Second point.</param>");
                WriteLine("/// <param name=\"amount\">Value between 0 and 1 indicating the weight of <paramref name=\"value2\"/>.</param>");
                WriteLine("/// <returns>The linear interpolation of the two points.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Lerp({1} point1, {1} point2, {2} amount)", result, Name, result.Type);
                WriteLine("{");
                Indent();
                WriteLine("return point1 + (point2 - point1) * amount;");
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
