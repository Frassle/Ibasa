using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Quaternion : Generator
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

        public Quaternion(NumberType type)
        {
            Type = type;
            Components = Component.Components(4, false);
        }

        public string Name
        {
            get
            {
                return string.Format("Quaternion{0}", Type.Suffix);
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
            WriteLine("/// Represents a quaternion, of the form (A + Bi + Cj + Dk).");
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
            WriteLine("/// Returns a new <see cref=\"{0}\"/> instance equal to zero.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} Zero = new {0}();", Name);
            WriteLine("/// <summary>");
            WriteLine("/// Returns a new <see cref=\"{0}\"/> instance with a real number equal to one.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} One = new {0}(1, 0, 0, 0);", Name);
            WriteLine("/// <summary>");
            WriteLine("/// Returns a new <see cref=\"{0}\"/> instance with i equal to one.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} I = new {0}(0, 1, 0, 0);", Name);
            WriteLine("/// <summary>");
            WriteLine("/// Returns a new <see cref=\"{0}\"/> instance with j equal to one.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} J = new {0}(0, 0, 1, 0);", Name);
            WriteLine("/// <summary>");
            WriteLine("/// Returns a new <see cref=\"{0}\"/> instance with k equal to one.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} K = new {0}(0, 0, 0, 1);", Name);
            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            WriteLine("/// <summary>");
            WriteLine("/// The real component of the quaternion.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} A;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The i component of the quaternion.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} B;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The j component of the quaternion.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} C;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The k component of the quaternion.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} D;", Type);

            WriteLine("#endregion");
        }

        void Properties()
        {
            WriteLine("#region Properties");
            #region Indexer
            WriteLine("/// <summary>");
            WriteLine("/// Returns the indexed component of this quaternion.");
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
            foreach (var component in Components)
            {
                WriteLine("{0} = value;", component);
            }
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified values.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"a\">The real component of the quaternion.</param>");
            WriteLine("/// <param name=\"b\">The i component of the quaternion.</param>");
            WriteLine("/// <param name=\"c\">The j component of the quaternion.</param>");
            WriteLine("/// <param name=\"d\">The k component of the quaternion.</param>");
            WriteLine("public {0}({1} a, {1} b, {1} c, {1} d)", Name, Type);
            WriteLine("{");
            WriteLine("\tA = a;");
            WriteLine("\tB = b;");
            WriteLine("\tC = c;");
            WriteLine("\tD = d;");
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Operations()
        {
            var result = new Quaternion(Type.PositiveType);

            WriteLine("#region Operations");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the identity of a specified quaternion.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
            WriteLine("/// <returns>The identity of value.</returns>");
            WriteLine("public static {0} operator +({1} value)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return value;");
            Dedent();
            WriteLine("}");

            if (Type.NegativeType != NumberType.Invalid)
            {
                var negQuaternion = new Quaternion(Type.NegativeType);

                WriteLine("/// <summary>");
                WriteLine("/// Returns the additive inverse of a specified quaternion.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A quaternion.</param>");
                WriteLine("/// <returns>The negative of value.</returns>");
                WriteLine("public static {0} operator -({1} value)", negQuaternion, Name);
                WriteLine("{");
                Indent();
                WriteLine("return Quaternion.Negative(value);");
                Dedent();
                WriteLine("}");
            }

            WriteLine("/// <summary>");
            WriteLine("/// Adds two quaternions and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first value to add.</param>");
            WriteLine("/// <param name=\"right\">The second value to add.</param>");
            WriteLine("/// <returns>The sum of left and right.</returns>");
            WriteLine("public static {0} operator +({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Quaternion.Add(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Subtracts one quaternion from another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The value to subtract from (the minuend).</param>");
            WriteLine("/// <param name=\"right\">The value to subtract (the subtrahend).</param>");
            WriteLine("/// <returns>The result of subtracting right from left (the difference).</returns>");
            WriteLine("public static {0} operator -({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Quaternion.Subtract(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Multiplies one quaternion by another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first value to add.</param>");
            WriteLine("/// <param name=\"right\">The second value to add.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} operator *({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Quaternion.Multiply(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Divides one quaternion by another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The quaternion to be divided (the dividend).</param>");
            WriteLine("/// <param name=\"right\">The quaternion to divide by (the divisor).</param>");
            WriteLine("/// <returns>The result of dividing left by right (the quotient).</returns>");
            WriteLine("public static {0} operator /({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Quaternion.Divide(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Conversions()
        {
            WriteLine("#region Conversions");

            foreach (var type in Types)
            {
                string imex = type.IsImplicitlyConvertibleTo(Type) ? "implicit" : "explicit";

                if (type != Type)
                {
                    Quaternion other = new Quaternion(type);

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

                WriteLine("/// <summary>");
                WriteLine("/// Defines an {0} conversion of a {1} value to a {2}.", imex, type, Name);
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The value to convert to a {0}.</param>", Name);
                WriteLine("/// <returns>A {0} that has all a real component equal to value.</returns>", Name);
                if (Type.IsCLSCompliant && !type.IsCLSCompliant)
                {
                    WriteLine("[CLSCompliant(false)]");
                }
                WriteLine("public static {0} operator {1}({2} value)", imex, Name, type);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}(({1})value, 0, 0, 0);", Name, Type);
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
            WriteLine("/// quaternion have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The quaternion to compare.</param>");
            WriteLine("/// <returns>true if this quaternion and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two quaternions are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first quaternion to compare.</param>");
            WriteLine("/// <param name=\"right\">The second quaternion to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" & ", Components.Select(component => string.Format("left.{0} == right.{0}", component))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two quaternions are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first quaternion to compare.</param>");
            WriteLine("/// <param name=\"right\">The second quaternion to compare.</param>");
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
            WriteLine("/// Converts the value of the current quaternion to its equivalent string");
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
            WriteLine("/// Converts the value of the current quaternion to its equivalent string");
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
            WriteLine("/// Converts the value of the current quaternion to its equivalent string");
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

            var formatString = "{0} + {1}i + {2}j + {3}k";
            var components = string.Join(", ",
                Components.Select(component => component + ".ToString(format, provider)"));

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current quaternion to its equivalent string");
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
            WriteLine("return String.Format(\"{0}\", {1});", formatString, components);
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Functions()
        {
            WriteLine("/// <summary>");
            WriteLine("/// Provides static methods for quaternion functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Quaternion");
            WriteLine("{");
            Indent();

            var result = new Quaternion(Type.PositiveType);

            #region Factory

            WriteLine("#region Factory");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> structure given a rotation and an axis.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"axis\">The axis of rotation.</param>");
            WriteLine("/// <param name=\"angle\">The angle of rotation.</param>");
            WriteLine("/// <returns>The newly created quaternion.</returns>");
            WriteLine("public static {0} FromRotationAxis({1} axis, {2} angle)", Name, new Vector(Type, 3), Type);
            Indent("{");
            WriteLine("axis = Vector.Normalize(axis);");
            WriteLine("var half = angle * 0.5f;");
            WriteLine("var sin =  Functions.Sin(half);");
            WriteLine("var cos =  Functions.Cos(half);");
            WriteLine("return new {0}(cos, axis.X * sin, axis.Y * sin, axis.Z * sin);", Name);
            Dedent("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> structure given a yaw, pitch, and roll value.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"yaw\">The yaw of rotation.</param>");
            WriteLine("/// <param name=\"pitch\">The pitch of rotation.</param>");
            WriteLine("/// <param name=\"roll\">The roll of rotation.</param>");
            WriteLine("/// <returns>The newly created quaternion.</returns>");
            WriteLine("public static {0} FromRotationAngles({1} yaw, {1} pitch, {1} roll)", Name, Type);
            Indent("{");
            WriteLine("var halfRoll = roll * 0.5f;");
            WriteLine("var sinRoll = Functions.Sin(halfRoll);");
            WriteLine("var cosRoll = Functions.Cos(halfRoll);");
            WriteLine("var halfPitch = pitch * 0.5f;");
            WriteLine("var sinPitch = Functions.Sin(halfPitch);");
            WriteLine("var cosPitch = Functions.Cos(halfPitch);");
            WriteLine("var halfYaw = yaw * 0.5f;");
            WriteLine("var sinYaw = Functions.Sin(halfYaw);");
            WriteLine("var cosYaw = Functions.Cos(halfYaw);");
            WriteLine("return new {0}(", Name);
            WriteLine("\t(cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll),");
            WriteLine("\t(cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll),");
            WriteLine("\t(sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll),");
            WriteLine("\t(cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll));");
            Dedent("}");

            WriteLine("#endregion");
            #endregion

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to an <see cref=\"Ibasa.IO.BinaryWriter\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this Ibasa.IO.BinaryWriter writer, {0} quaternion)", Name);
            WriteLine("{");
            Indent();
            foreach(var component in Components)
            {
                WriteLine("writer.Write(quaternion.{0});", component);
            }
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> from an <see cref=\"Ibasa.IO.BinaryReader\">.", Name);
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
            if (Type.NegativeType != NumberType.Invalid)
            {
                var negQuaternion = new Quaternion(Type.NegativeType);

                WriteLine("/// <summary>");
                WriteLine("/// Returns the additive inverse of a quaternion.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A quaternion.</param>");
                WriteLine("/// <returns>The negative of value.</returns>");
                WriteLine("public static {0} Negative({1} value)", negQuaternion, Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", negQuaternion,
                    string.Join(", ", Components.Select(component => "-value." + component)));
                Dedent();
                WriteLine("}");
            }

            WriteLine("/// <summary>");
            WriteLine("/// Adds two quaternions and returns the result.");
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
            WriteLine("/// Subtracts one quaternion from another and returns the result.");
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
            WriteLine("/// Returns the product of two quaternions.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first value to add.</param>");
            WriteLine("/// <param name=\"right\">The second value to add.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} Multiply({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}(", Name);
            WriteLine("\t(left.A * right.A) - ((left.B * right.B) + (left.C * right.C) + (left.D * right.D)),");
            WriteLine("\t(left.A * right.B) + (left.B * right.A) + (left.D * right.C) - (left.C * right.D),");
            WriteLine("\t(left.A * right.C) + (left.C * right.A) + (left.B * right.D) - (left.D * right.B),");
            WriteLine("\t(left.A * right.D) + (left.D * right.A) + (left.C * right.B) - (left.B * right.C));");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Divides one quaternion by another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The quaternion to be divided (the dividend).</param>");
            WriteLine("/// <param name=\"right\">The quaternion to divide by (the divisor).</param>");
            WriteLine("/// <returns>The result of dividing left by right (the quotient).</returns>");
            WriteLine("public static {0} Divide({1} left, {1} right)", result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}(", Name);
            WriteLine("\t(left.A * right.A) - ((left.B * -right.B) + (left.C * -right.C) + (left.D * -right.D)) / AbsoluteSquared(right),");
            WriteLine("\t((left.A * -right.B) + (left.B * right.A) + (left.D * -right.C) - (left.C * -right.D)) / AbsoluteSquared(right),");
            WriteLine("\t((left.A * -right.C) + (left.C * right.A) + (left.B * -right.D) - (left.D * -right.B)) / AbsoluteSquared(right),");
            WriteLine("\t((left.A * -right.D) + (left.D * right.A) + (left.C * -right.B) - (left.B * -right.C)) / AbsoluteSquared(right));");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two quaternions are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first quaternion to compare.</param>");
            WriteLine("/// <param name=\"right\">The second quaternion to compare.</param>");
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
            WriteLine("/// Determines whether all components of a quaternion are non-zero.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
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
            WriteLine("/// Determines whether all components of a quaternion satisfy a condition.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
            WriteLine("/// <param name=\"predicate\">A function to test each component for a condition.</param>");
            WriteLine("/// <returns>true if every component of the quaternion passes the test in the specified");
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
            WriteLine("/// Determines whether any component of a quaternion is non-zero.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
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
            WriteLine("/// Determines whether any components of a quaternion satisfy a condition.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
            WriteLine("/// <param name=\"predicate\">A function to test each component for a condition.</param>");
            WriteLine("/// <returns>true if any component of the quaternion passes the test in the specified");
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
            /// <summary>
            //    /// Gets the angle of the quaternion.
            //    /// </summary>
            //    public double Angle
            //    {
            //        get
            //        {
            //            return 2.0 * Math.Acos(A);
            //        }
            //    }
            //    /// <summary>
            //    /// Gets the axis components of the quaternion.
            //    /// </summary>
            //    public Double3 Axis
            //    {
            //        get
            //        {
            //            double s = Math.Sqrt(1.0 - A - A);
            //            return new Double3(B / s, C / s, D / s);
            //        }
            //    }

            WriteLine("/// <summary>");
            WriteLine("/// Return the axis angle representation of a unit quaternion.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A unit quaternion.</param>");
            WriteLine("/// <returns>The axis angle of a quaternion.</returns>");
            WriteLine("public static Tuple<{0}, {1}> AxisAngle({2} value)", new Vector(Type, 3), Type, Name);
            WriteLine("{");
            Indent();
            WriteLine("var s = Functions.Sqrt(1 - value.A - value.A);");
            WriteLine("return Tuple.Create(");
            WriteLine("\tnew {0}(value.B / s, value.C / s, value.D / s),", new Vector(Type, 3));
            WriteLine("\t2 * Functions.Acos(value.A));");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Return real part of a quaternion.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
            WriteLine("/// <returns>The real part of a quaternion.</returns>");
            WriteLine("public static {0} Real({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}(value.A, 0, 0, 0);", Name);
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Return imaginary part of a quaternion.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
            WriteLine("/// <returns>The imaginary part of a quaternion.</returns>");
            WriteLine("public static {0} Imaginary({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}(0, value.B, value.C, value.D);", Name);
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Computes the absolute squared value of a quaternion and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
            WriteLine("/// <returns>The absolute squared value of value.</returns>");
            WriteLine("public static {0} AbsoluteSquared({1} value)", Type.PositiveType, Name);
            WriteLine("{");
            Indent();
            WriteLine("return (value.A * value.A + value.B * value.B + value.C * value.C + value.D * value.D);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Computes the absolute value (or modulus or magnitude) of a quaternion and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
            WriteLine("/// <returns>The absolute value of value.</returns>");
            WriteLine("public static {0} Absolute({1} value)", Type.RealType, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Functions.Sqrt(value.A * value.A + value.B * value.B + value.C * value.C + value.D * value.D);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Computes the normalized value (or unit) of a quaternion.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
            WriteLine("/// <returns>The normalized value of value.</returns>");
            WriteLine("public static {0} Normalize({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("var absolute = Absolute(value);");
            WriteLine("if(absolute <= {0}.Epsilon)", Type.RealType);
            WriteLine("{");
            Indent();
            WriteLine("return {0}.Zero;", Name);
            Dedent();
            WriteLine("}");
            WriteLine("return value / absolute;");
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Returns the multiplicative inverse of a quaternion.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
            WriteLine("/// <returns>The reciprocal of value.</returns>");
            WriteLine("public static {0} Reciprocal({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("var absoluteSquared = AbsoluteSquared(value);");
            WriteLine("return new {0}(", Name);
            WriteLine("\tvalue.A / absoluteSquared,");
            WriteLine("\t-value.B / absoluteSquared,");
            WriteLine("\t-value.C / absoluteSquared,");
            WriteLine("\t-value.D / absoluteSquared);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Computes the conjugate of a quaternion and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
            WriteLine("/// <returns>The conjugate of value.</returns>");
            WriteLine("public static {0} Conjugate({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}(value.A, -value.B, -value.C, -value.D);", Name);
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Computes the argument of a quaternion and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A quaternion.</param>");
            WriteLine("/// <returns>The argument of value.</returns>");
            WriteLine("public static {0} Argument({1} value)", Type.RealType, Name);
            WriteLine("{");
            Indent();
            WriteLine("return Functions.Atan2(Absolute(Imaginary(value)), value.A);");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion
            
            #region Product
            WriteLine("#region Product");
            WriteLine("/// <summary>");
            WriteLine("/// Calculates the dot product (inner product) of two quaternions.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">First source quaternion.</param>");
            WriteLine("/// <param name=\"right\">Second source quaternion.</param>");
            WriteLine("/// <returns>The dot product of the two quaternions.</returns>");
            WriteLine("public static {0} Dot({1} left, {1} right)", Type, Name);
            Indent("{");
            WriteLine("return left.A * right.A + left.B * right.B + left.C * right.C + left.D * right.D;");
            Dedent("}");
            WriteLine("#endregion");
            #endregion

            #region Interpolation
            WriteLine("#region Interpolation");
            WriteLine("/// <summary>");
            WriteLine("/// Performs a linear interpolation between two quaternions.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"start\">Start quaternion.</param>");
            WriteLine("/// <param name=\"end\">End quaternion.</param>");
            WriteLine("/// <param name=\"amount\">Value between 0 and 1 indicating the weight of <paramref name=\"end\"/>.</param>");
            WriteLine("/// <returns>The linear interpolation of the two quaternions.</returns>");
            WriteLine("/// <remarks>");
            WriteLine("/// This method performs the linear interpolation based on the following formula.");
            WriteLine("/// <code>start + (end - start) * amount</code>");
            WriteLine("/// Passing <paramref name=\"amount\"/> a value of 0 will cause <paramref name=\"start\"/> to be returned; a value of 1 will cause <paramref name=\"end\"/> to be returned. ");
            WriteLine("/// </remarks>");
            WriteLine("public static {0} Lerp({0} start, {0} end, {1} amount)", Name, Type);
            Indent("{");
            WriteLine("return new {0}(", Name);
            WriteLine("\tFunctions.Lerp(start.A, end.A, amount),");
            WriteLine("\tFunctions.Lerp(start.B, end.B, amount),");
            WriteLine("\tFunctions.Lerp(start.C, end.C, amount),");
            WriteLine("\tFunctions.Lerp(start.D, end.D, amount));");
            Dedent("}");
            WriteLine("/// <summary>");
            WriteLine("/// Interpolates between two unit quaternions, using spherical linear interpolation.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"start\">Start quaternion.</param>");
            WriteLine("/// <param name=\"end\">End quaternion.</param>");
            WriteLine("/// <param name=\"amount\">Value between 0 and 1 indicating the weight of <paramref name=\"end\"/>.</param>");
            WriteLine("/// <returns>The spherical linear interpolation of the two quaternions.</returns>");
            WriteLine("///  <remarks>");
            WriteLine("/// Passing <paramref name=\"amount\"/> a value of 0 will cause <paramref name=\"start\"/> to be returned; a value of 1 will cause <paramref name=\"end\"/> to be returned. ");
            WriteLine("/// </remarks>");
            WriteLine("public static {0} Slerp({0} start, {0} end, {1} amount)", Name, Type);
            Indent("{");
            WriteLine("var cosTheta = Dot(start, end);");
            WriteLine("//Cannot use slerp, use lerp instead");
            WriteLine("if (Functions.Abs(cosTheta) - 1 < {0}.Epsilon)", Type);
            Indent("{");
            WriteLine("return Lerp(start, end, amount);");
            Dedent("}");
            WriteLine("var theta = Functions.Acos(cosTheta);");
            WriteLine("var sinTheta = Functions.Sin(theta);");
            WriteLine("var t0 = Functions.Sin((1 - amount) * theta) / sinTheta;");
            WriteLine("var t1 = Functions.Sin(amount * theta) / sinTheta;");
            WriteLine("return t0 * start + t1 * end;");
            Dedent("}");
            WriteLine("#endregion");
            #endregion

            #region Transform
            WriteLine("#region Transform");

            var vector4 = new Vector(Type, 4);

            WriteLine("public static {0} Transform({0} vector, {1} rotation)", vector4, Name);
            Indent("{");
            WriteLine("var v = rotation * new {0}(vector.X, vector.Y, vector.Z, 0) * Conjugate(rotation);", Name);
            WriteLine("return new {0}(v.A, v.B, v.C, vector.W);", vector4);
            Dedent("}");
            
            var vector3 = new Vector(Type, 3);

            WriteLine("public static {0} Transform({0} vector, {1} rotation)", vector3, Name);
            Indent("{");
            WriteLine("var v = rotation * new {0}(vector.X, vector.Y, vector.Z, 0) * Conjugate(rotation);", Name);
            WriteLine("return new {0}(v.A, v.B, v.C);", vector3);
            Dedent("}");

            WriteLine("#endregion");
            #endregion

            #region Old
            //#region Per component
            //WriteLine("#region Per component");

            //WriteLine("#region Transform");
            //foreach (var type in Types)
            //{
            //    var transform = new Quaternion(type, Dimension);

            //    WriteLine("/// <summary>");
            //    WriteLine("/// Transforms the components of a quaternion and returns the result.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">The quaternion to transform.</param>");
            //    WriteLine("/// <param name=\"transformer\">A transform function to apply to each component.</param>");
            //    WriteLine("/// <returns>The result of transforming each component of value.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Transform({1} value, Func<{2}, {3}> transformer)", transform, Name, Type, transform.Type);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", transform,
            //        string.Join(", ", Components.Select(component => string.Format("transformer(value.{0})", component))));
            //    Dedent();
            //    WriteLine("}");
            //}
            //WriteLine("#endregion");

            //WriteLine("/// <summary>");
            //WriteLine("/// Multiplys the components of two quaternions and returns the result.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"left\">The first quaternion to modulate.</param>");
            //WriteLine("/// <param name=\"right\">The second quaternion to modulate.</param>");
            //WriteLine("/// <returns>The result of multiplying each component of left by the matching component in right.</returns>");
            //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //WriteLine("public static {0} Modulate({1} left, {1} right)", result, Name);
            //WriteLine("{");
            //Indent();
            //WriteLine("return new {0}({1});", result,
            //    string.Join(", ", Components.Select(component => string.Format("left.{0} * right.{0}", component))));
            //Dedent();
            //WriteLine("}");
            //WriteLine("/// <summary>");
            //WriteLine("/// Returns the absolute value (per component).");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"value\">A quaternion.</param>");
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
            //WriteLine("/// Returns a quaternion that contains the lowest value from each pair of components.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"value1\">The first quaternion.</param>");
            //WriteLine("/// <param name=\"value2\">The second quaternion.</param>");
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
            //WriteLine("/// Returns a quaternion that contains the highest value from each pair of components.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"value1\">The first quaternion.</param>");
            //WriteLine("/// <param name=\"value2\">The second quaternion.</param>");
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
            //WriteLine("/// <param name=\"value\">A quaternion to constrain.</param>");
            //WriteLine("/// <param name=\"min\">The minimum values for each component.</param>");
            //WriteLine("/// <param name=\"max\">The maximum values for each component.</param>");
            //WriteLine("/// <returns>A quaternion with each component constrained to the given range.</returns>");
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
            //    WriteLine("/// <param name=\"value\">A quaternion to saturate.</param>");
            //    WriteLine("/// <returns>A quaternion with each component constrained to the range 0 to 1.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Saturate({0} value)", Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", Name,
            //        string.Join(", ", Components.Select(component => string.Format("Functions.Saturate(value.{0})", component))));
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Returns a quaternion where each component is the smallest integral value that");
            //    WriteLine("/// is greater than or equal to the specified component.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A quaternion.</param>");
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
            //    WriteLine("/// Returns a quaternion where each component is the largest integral value that");
            //    WriteLine("/// is less than or equal to the specified component.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A quaternion.</param>");
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
            //    WriteLine("/// Returns a quaternion where each component is the integral part of the specified component.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A quaternion.</param>");
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
            //    WriteLine("/// Returns a quaternion where each component is the fractional part of the specified component.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A quaternion.</param>");
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
            //    WriteLine("/// Returns a quaternion where each component is rounded to the nearest integral value.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A quaternion.</param>");
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
            //    WriteLine("/// Returns a quaternion where each component is rounded to the nearest integral value.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A quaternion.</param>");
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
            //    WriteLine("/// Returns a quaternion where each component is rounded to the nearest integral value.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A quaternion.</param>");
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
            //    WriteLine("/// Returns a quaternion where each component is rounded to the nearest integral value.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A quaternion.</param>");
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
            //    WriteLine("/// Calculates the reciprocal of each component in the quaternion.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">A quaternion.</param>");
            //    WriteLine("/// <returns>A quaternion with the reciprocal of each of values components.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Reciprocal({1} value)", result, Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", result,
            //        string.Join(", ", Components.Select(component => string.Format("1 / value.{0}", component))));
            //    Dedent();
            //    WriteLine("}");
            //}
            //WriteLine("#endregion");
            //#endregion

            //#region Barycentric, Reflect, Refract
            //if (Type.IsReal)
            //{
            //    WriteLine("#region Barycentric, Reflect, Refract");
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
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Returns the reflection of a quaternion off a surface that has the specified normal.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"quaternion\">The source quaternion.</param>");
            //    WriteLine("/// <param name=\"normal\">Normal of the surface.</param>");
            //    WriteLine("/// <returns>The reflected quaternion.</returns>");
            //    WriteLine("/// <remarks>Reflect only gives the direction of a reflection off a surface, it does not determine");
            //    WriteLine("/// whether the original quaternion was close enough to the surface to hit it.</remarks>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Reflect({1} quaternion, {1} normal)", result, Name);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return quaternion - ((2 * Dot(quaternion, normal)) * normal);");
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Returns the refraction of a quaternion off a surface that has the specified normal, and refractive index.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"quaternion\">The source quaternion.</param>");
            //    WriteLine("/// <param name=\"normal\">Normal of the surface.</param>");
            //    WriteLine("/// <param name=\"index\">The refractive index, destination index over source index.</param>");
            //    WriteLine("/// <returns>The refracted quaternion.</returns>");
            //    WriteLine("/// <remarks>Refract only gives the direction of a refraction off a surface, it does not determine");
            //    WriteLine("/// whether the original quaternion was close enough to the surface to hit it.</remarks>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Refract({1} quaternion, {1} normal, {2} index)", result, Name, result.Type);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("var cos1 = Dot(quaternion, normal);");
            //    WriteLine("var radicand = 1 - (index * index) * (1 - (cos1 * cos1));");
            //    WriteLine("if (radicand < 0)");
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return {0}.Zero;", result);
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("return (index * quaternion) + ((Functions.Sqrt(radicand) - index * cos1) * normal);");
            //    Dedent();
            //    WriteLine("}");
            //    WriteLine("#endregion");
            //}
            //#endregion

            //#region Interpolation
            //if (Type.IsReal)
            //{
            //    WriteLine("#region Interpolation");
            //    //WriteLine("/// <summary>");
            //    //WriteLine("/// Performs a Hermite spline interpolation.");
            //    //WriteLine("/// </summary>");
            //    //WriteLine("/// <param name=\"value1\">First source position.</param>");
            //    //WriteLine("/// <param name=\"tangent1\">First source tangent.</param>");
            //    //WriteLine("/// <param name=\"value2\">Second source position.</param>");
            //    //WriteLine("/// <param name=\"tangent2\">Second source tangent.</param>");
            //    //WriteLine("/// <param name=\"amount\">Weighting factor.</param>");
            //    //WriteLine("/// <returns>The result of the Hermite spline interpolation.</returns>");
            //    //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); } 
            //    //WriteLine("public static {0} Hermite({1} value1, {1} tangent1, {1} value2, {1} tangent2, {2} amount)", result, Name, result.Type);
            //    //WriteLine("{");
            //    //Indent();
            //    //WriteLine("var amount2 = amount * amount;");
            //    //WriteLine("var amount3 = amount2 * amount;");
            //    //WriteLine("var h00 = 2 * amount3 - 3 * amount2 + 1;");
            //    //WriteLine("var h10 = amount3 - 2 * amount2 + amount;");
            //    //WriteLine("var h01 = -2 * amount3 + 3 * amount2;");
            //    //WriteLine("var h11 = amount3 - amount2;");
            //    //WriteLine("return h00 * value1 + h10 * tangent1 + h01 * value2 + h11 * tangent2;");
            //    //Dedent();
            //    //WriteLine("}");
            //    //WriteLine("/// <summary>");
            //    //WriteLine("/// Performs a Catmull-Rom interpolation using the specified positions.");
            //    //WriteLine("/// </summary>");
            //    //WriteLine("/// <param name=\"value1\">The first position in the interpolation.</param>");
            //    //WriteLine("/// <param name=\"value2\">The second position in the interpolation.</param>");
            //    //WriteLine("/// <param name=\"value3\">The third position in the interpolation.</param>");
            //    //WriteLine("/// <param name=\"value4\">The fourth position in the interpolation.</param>");
            //    //WriteLine("/// <param name=\"amount\">Weighting factor.</param>");
            //    //WriteLine("/// <returns>A quaternion that is the result of the Catmull-Rom interpolation.</returns>");
            //    //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    //WriteLine("public static {0} CatmullRom({1} value1, {1} value2, {1} value3, {1} value4, {2} amount)", result, Name, result.Type);
            //    //WriteLine("{");
            //    //Indent();
            //    //WriteLine("var tangent0 = (value3 - value1) / 2;");
            //    //WriteLine("var tangent1 = (value4 - value2) / 2;");
            //    //WriteLine("return Hermite(value2, tangent0, value3, tangent1, amount);");
            //    //Dedent();
            //    //WriteLine("}");
            //    //WriteLine("/// <summary>");
            //    //WriteLine("/// Performs a cubic interpolation between two values.");
            //    //WriteLine("/// </summary>");
            //    //WriteLine("/// <param name=\"value1\">First values.</param>");
            //    //WriteLine("/// <param name=\"value2\">Second values.</param>");
            //    //WriteLine("/// <param name=\"amount\">Value between 0 and 1 indicating the weight of <paramref name=\"value2\"/>.</param>");
            //    //WriteLine("/// <returns>The cubic interpolation of the two quaternions.</returns>");
            //    //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    //WriteLine("public static {0} SmoothStep({1} value1, {1} value2, {2} amount)", result, Name, result.Type);
            //    //WriteLine("{");
            //    //Indent();
            //    //WriteLine("amount = Functions.Saturate(amount);");
            //    //WriteLine("amount = amount * amount * (3 - 2 * amount);");
            //    //WriteLine("return Lerp(value1, value2, amount);");
            //    //Dedent();
            //    //WriteLine("}");
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Performs a linear interpolation between two values.");
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value1\">First value.</param>");
            //    WriteLine("/// <param name=\"value2\">Second value.</param>");
            //    WriteLine("/// <param name=\"amount\">Value between 0 and 1 indicating the weight of <paramref name=\"value2\"/>.</param>");
            //    WriteLine("/// <returns>The linear interpolation of the two values.</returns>");
            //    if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    WriteLine("public static {0} Lerp({1} value1, {1} value2, {2} amount)", result, Name, result.Type);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return (1 - amount) * value1 + amount * value2;");
            //    Dedent();
            //    WriteLine("}");
            //    //WriteLine("/// <summary>");
            //    //WriteLine("/// Performs a quadratic interpolation between three values.");
            //    //WriteLine("/// </summary>");
            //    //WriteLine("/// <param name=\"value1\">First value.</param>");
            //    //WriteLine("/// <param name=\"value2\">Second value.</param>");
            //    //WriteLine("/// <param name=\"value3\">Third value.</param>");
            //    //WriteLine("/// <param name=\"amount\">Value between 0 and 1 indicating the weight towards <paramref name=\"value3\"/>.</param>");
            //    //WriteLine("/// <returns>The quadratic interpolation of the three values.</returns>");
            //    //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    //WriteLine("public static {0} Qerp({1} value1, {1} value2, {1} value3, {2} amount)", result, Name, result.Type);
            //    //WriteLine("{");
            //    //Indent();
            //    //WriteLine("var p = 2 * value1 - value2 + 2 * value3;");
            //    //WriteLine("var q = value2 - value3 - 3 * value1;");
            //    //WriteLine("var r = value1;");
            //    //WriteLine("var amount2 = amount * amount;");
            //    //WriteLine("return (p * amount * amount2) + (q * amount) + (r);");
            //    //Dedent();
            //    //WriteLine("}");
            //    //WriteLine("/// <summary>");
            //    //WriteLine("/// Performs a cubic interpolation between four values.");
            //    //WriteLine("/// </summary>");
            //    //WriteLine("/// <param name=\"value1\">First value.</param>");
            //    //WriteLine("/// <param name=\"value2\">Second value.</param>");
            //    //WriteLine("/// <param name=\"value3\">Third value.</param>");
            //    //WriteLine("/// <param name=\"value4\">Fourth value.</param>");
            //    //WriteLine("/// <param name=\"amount\">Value between 0 and 1 indicating the weight towards <paramref name=\"value4\"/>.</param>");
            //    //WriteLine("/// <returns>The cubic interpolation of the four values.</returns>");
            //    //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //    //WriteLine("public static {0} Cerp({1} value1, {1} value2, {1} value3, {1} value4, {2} amount)", result, Name, result.Type);
            //    //WriteLine("{");
            //    //Indent();
            //    //WriteLine("var p = (value4 - value3) - (value1 - value2);");
            //    //WriteLine("var q = (value1 - value2) - p;");
            //    //WriteLine("var r = value3 - value1;");
            //    //WriteLine("var s = value2;");
            //    //WriteLine("var amount2 = amount * amount;");
            //    //WriteLine("return (p * amount * amount2) + (q * amount2) + (r * amount) + (s);");
            //    //Dedent();
            //    //WriteLine("}");
            //    WriteLine("#endregion");
            //}
            //#endregion
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
