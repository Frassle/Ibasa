using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Integer : Generator
    {
        public static int[] Sizes { get { return new int[] { 128, 160, 256, 512 }; } }
        
        public int Size { get; private set; }
        public bool Signed { get; private set; }

        private int Bytes { get { return Size / 8; } }
        private string[] Parts { get; set; }

        public Integer(int size, bool signed)
        {
            Size = size;
            Signed = signed;
            Parts = Enumerable.Range(0, Size / 32).Select(i => string.Format("Part{0}", i)).ToArray();
        }

        public string Name
        {
            get
            {
                return string.Format("{0}Int{1}", Signed ? "" : "U", Size);
            }
        }

        public void Generate()
        {
            Namespace();
        }

        void Namespace()
        {
            //WriteLine("using System;");
            //WriteLine("using System.Diagnostics.Contracts;");
            //WriteLine("using System.Globalization;");
            //WriteLine("using System.Runtime.InteropServices;");
            //WriteLine("");
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
            WriteLine("/// A {0} bit {1} integer.", Size, Signed ? "signed" : "unsigned");
            WriteLine("/// </summary>");
            WriteLine("[System.Serializable]");
            WriteLine("[System.Runtime.InteropServices.ComVisible(true)]");
            WriteLine("[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]");
            WriteLine("public struct {0}: System.IComparable<{0}>, System.IEquatable<{0}>, System.IFormattable", Name);
            WriteLine("{");
            Indent();
            Constants();
            Fields();
            Properties();
            Constructors();
            Operations();
            Conversions();
            Equatable();
            Comparable();
            Parse();
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

            if(Signed)
            {
                var parts = new string[Parts.Length];

                parts[parts.Length - 1] = "(uint)int.MaxValue";
                for (int i = 0; i < parts.Length - 1; ++i)
                {
                    parts[i] = "uint.MaxValue";
                }

                WriteLine("/// <summary>");
                WriteLine("/// Represents the largest possible value of an {0}.", Name);
                WriteLine("/// </summary>");
                WriteLine("public static readonly {0} MaxValue = new {0}({1});", Name,
                    string.Join(", ", parts));

                parts[parts.Length - 1] = "unchecked((uint)int.MinValue)";
                for (int i = 0; i < parts.Length - 1; ++i)
                {
                    parts[i] = "0";
                }

                WriteLine("/// <summary>");
                WriteLine("/// Represents the smallest possible value of an {0}.", Name);
                WriteLine("/// </summary>");
                WriteLine("public static readonly {0} MinValue = new {0}({1});", Name,
                    string.Join(", ", parts));
            }
            else
            {
                WriteLine("/// <summary>");
                WriteLine("/// Represents the largest possible value of an {0}.", Name);
                WriteLine("/// </summary>");
                WriteLine("public static readonly {0} MaxValue = new {0}({1});", Name,
                    string.Join(", ", Parts.Select(part => "uint.MaxValue")));

                WriteLine("/// <summary>");
                WriteLine("/// Represents the smallest possible value of an {0}.", Name);
                WriteLine("/// </summary>");
                WriteLine("public static readonly {0} MinValue = new {0}(0);", Name);
            }
            
            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            foreach(var part in Parts)
            {
                WriteLine("private readonly uint {0};", part);
            }

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
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified bytes.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"bytes\">The bytes that will be used.</param>");
            WriteLine("public {0}(byte[] bytes)", Name);
            WriteLine("{");
            Indent();
            for (int i = 0; i < Parts.Length; ++i)
            {
                WriteLine("{0} = (uint)((bytes[{4}] << 24) | (bytes[{3}] << 16) | (bytes[{2}] << 8) | bytes[{1}]);", Parts[i], i, i+1, i+2, i+3);
            }
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified 32 bit parts.", Name);
            WriteLine("/// </summary>");
            for (int i = 0; i < Parts.Length; ++i)
            {
                WriteLine("/// <param name=\"{0}\">Contains bytes {1} to {2}.</param>", Parts[i].ToLower(), i * 4, (i * 4) + 3);
            }
            WriteLine("[System.CLSCompliant(false)]");
            WriteLine("public {0}({1})", Name, 
                string.Join(", ", Parts.Select(part => string.Format("uint {0}", part.ToLower()))));
            WriteLine("{");
            Indent();
            foreach(var part in Parts)
            {
                WriteLine("{0} = {1};", part, part.ToLower());
            }
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified value.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">The value that will be assigned.</param>");
            WriteLine("[System.CLSCompliant(false)]");
            WriteLine("public {0}(uint value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("Part0 = value;");
            for (int i = 1; i < Parts.Length; ++i)
            {
                WriteLine("{0} = 0;", Parts[i]);
            }
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified value.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">The value that will be assigned.</param>");
            WriteLine("public {0}(int value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("Part0 = (uint)value;");
            for (int i = 1; i < Parts.Length; ++i) 
            {
                WriteLine("{0} = {1};", Parts[i], "value < 0 ? uint.MaxValue : 0");
            }
            Dedent();
            WriteLine("}");


            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified value.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">The value that will be assigned.</param>");
            WriteLine("[System.CLSCompliant(false)]");
            WriteLine("public {0}(ulong value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("Part0 = (uint)value;");
            WriteLine("Part1 = (uint)(value >> 32);");
            for (int i = 2; i < Parts.Length; ++i) 
            {
                WriteLine("{0} = 0;", Parts[i]);
            }
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified value.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">The value that will be assigned.</param>");
            WriteLine("public {0}(long value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("Part0 = (uint)value;");
            WriteLine("Part1 = (uint)(value >> 32);");
            for (int i = 2; i < Parts.Length; ++i)
            {
                WriteLine("{0} = {1};", Parts[i], "value < 0 ? uint.MaxValue : 0");
            }
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Operations()
        {
            WriteLine("#region Operations");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the identity of a specified integer.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">An integer.</param>");
            WriteLine("/// <returns>The identity of value.</returns>");
            WriteLine("public static {0} operator +({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return value;");
            Dedent();
            WriteLine("}");

            var neg_result = new Integer(Size, true);

            WriteLine("/// <summary>");
            WriteLine("/// Returns the additive inverse of a specified integer.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">An integer.</param>");
            WriteLine("/// <returns>The negative of value.</returns>");
            WriteLine("public static {0} operator -({1} value)", neg_result, Name);
            WriteLine("{");
            Indent();
            WriteLine("return ({0})(~value + {1}.One);", neg_result, Name);
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Adds two integers and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first value to add.</param>");
            WriteLine("/// <param name=\"right\">The second value to add.</param>");
            WriteLine("/// <returns>The sum of left and right.</returns>");
            WriteLine("public static {0} operator +({0} left, {0} right)", Name);
            Indent("{");
            WriteLine("unsafe");
            Indent("{");
            WriteLine("uint* parts = stackalloc uint[{0}];", Parts.Length);
            WriteLine("ulong carry = 0;");
            WriteLine("ulong n = 0;");
            for (int i = 0; i < Parts.Length; ++i)
            {
                WriteLine("n = carry + left.{0} + right.{0};", Parts[i]);
                WriteLine("parts[{0}] = (uint)n;", i);
                WriteLine("carry = n >> 32;");
            }

            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Enumerable.Range(0, Parts.Length).Select(i => string.Format("parts[{0}]", i))));

            Dedent("}");
            Dedent("}");

            WriteLine("/// <summary>");
            WriteLine("/// Subtracts one integer from another and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The value to subtract from (the minuend).</param>");
            WriteLine("/// <param name=\"right\">The value to subtract (the subtrahend).</param>");
            WriteLine("/// <returns>The result of subtracting right from left (the difference).</returns>");
            WriteLine("public static {0} operator -({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left + ({0})(-right);", Name);
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a integer and scalar.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first integer to multiply.</param>");
            WriteLine("/// <param name=\"right\">The second integer to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} operator *({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("throw new System.NotImplementedException(\"operator *\");"); 
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Divides a integer by a scalar and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The integer to be divided (the dividend).</param>");
            WriteLine("/// <param name=\"right\">The integer to divide by (the divisor).</param>");
            WriteLine("/// <returns>The result of dividing left by right (the quotient).</returns>");
            WriteLine("public static {0} operator /({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("throw new System.NotImplementedException(\"operator /\");"); 
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Divides a integer by a scalar and returns the remainder.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The integer to be divided (the dividend).</param>");
            WriteLine("/// <param name=\"right\">The integer to divide by (the divisor).</param>");
            WriteLine("/// <returns>The modulus from dividing left by right (the remainder).</returns>");
            WriteLine("public static {0} operator %({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("throw new System.NotImplementedException(\"operator %\");"); 
            Dedent();
            WriteLine("}"); 
            
            WriteLine("/// <summary>");
            WriteLine("/// Returns the bitwise not of an integer.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">An integer.</param>");
            WriteLine("/// <returns>The bitwise not of value.</returns>");
            WriteLine("public static {0} operator ~({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, string.Join(", ",
                Parts.Select(part => string.Format("~value.{0}", part))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the bitwise AND of two integers.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first integer.</param>");
            WriteLine("/// <param name=\"right\">The second integer.</param>");
            WriteLine("/// <returns>The bitwise AND of left and right.</returns>");
            WriteLine("public static {0} operator &({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, string.Join(", ",
                Parts.Select(part => string.Format("left.{0} & right.{0}", part))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the bitwise OR of two integers.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first integer.</param>");
            WriteLine("/// <param name=\"right\">The second integer.</param>");
            WriteLine("/// <returns>The bitwise OR of left and right.</returns>");
            WriteLine("public static {0} operator |({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, string.Join(", ",
                Parts.Select(part => string.Format("left.{0} | right.{0}", part))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the bitwise XOR of two integers.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first integer.</param>");
            WriteLine("/// <param name=\"right\">The second integer.</param>");
            WriteLine("/// <returns>The bitwise XOR of left and right.</returns>");
            WriteLine("public static {0} operator ^({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, string.Join(", ",
                Parts.Select(part => string.Format("left.{0} ^ right.{0}", part))));
            Dedent();
            WriteLine("}");

            WriteLine("public static {0} operator <<({0} value, int amount)", Name);
            Indent("{");
            WriteLine("unsafe");
            Indent("{");

            WriteLine("uint* parts = stackalloc uint[{0}];", Parts.Length);
            WriteLine("uint* vparts = stackalloc uint[{0}];", Parts.Length);
            for (int i = 0; i < Parts.Length; ++i)
            {
                WriteLine("vparts[{0}] = value.{1};", i, Parts[i]);
            }

            WriteLine("int k = amount / 32;");
            WriteLine("int shift = amount % 32;");
            WriteLine("for (int i = 0; i < {0}; i++)", Parts.Length);
            Indent("{");
            WriteLine("if (i + k + 1 < {0})", Parts.Length);
            Indent("{");
            WriteLine("parts[i + k + 1] |= (vparts[i] >> (32 - shift));");
            Dedent("}");            
            WriteLine("if (i + k < {0})", Parts.Length);
            Indent("{");
            WriteLine("parts[i + k] |= (vparts[i] << shift);");
            Dedent("}");
            Dedent("}");

            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Enumerable.Range(0, Parts.Length).Select(i => string.Format("parts[{0}]", i))));

            Dedent("}");
            Dedent("}");

            WriteLine("public static {0} operator >>({0} value, int amount)", Name);
            Indent("{");
            WriteLine("unsafe");
            Indent("{");

            WriteLine("uint* parts = stackalloc uint[{0}];", Parts.Length);
            WriteLine("uint* vparts = stackalloc uint[{0}];", Parts.Length);
            for (int i = 0; i < Parts.Length; ++i)
            {
                WriteLine("vparts[{0}] = value.{1};", i, Parts[i]);
            }

            WriteLine("int k = amount / 32;");
            WriteLine("int shift = amount % 32;");
            WriteLine("for (int i = 0; i < {0}; i++)", Parts.Length);
            Indent("{");
            WriteLine("if (i - k - 1 >= 0)");
            Indent("{");
            WriteLine("parts[i - k - 1] |= (vparts[i] << (32 - shift));");
            Dedent("}");
            WriteLine("if (i - k >= 0)");
            Indent("{");
            WriteLine("parts[i - k] |= (vparts[i] >> shift);");
            Dedent("}");
            Dedent("}");

            if (Signed)
            {
                WriteLine("uint negative = (uint)((int)vparts[{0}] >> 32);", Parts.Length - 1);
                WriteLine("for (int i = System.Math.Max(0, {0} - k); i < {0}; i++)", Parts.Length);
                Indent("{");
                WriteLine("parts[i] = negative;");
                Dedent("}");
                WriteLine("negative <<= (32 - shift);");
                WriteLine("if({0} - k - 1 >= 0)", Parts.Length);
                Indent("{");
                WriteLine("parts[{0} - k - 1] |= negative;", Parts.Length);
                Dedent("}");
            }

            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Enumerable.Range(0, Parts.Length).Select(i => string.Format("parts[{0}]", i))));

            Dedent("}");
            Dedent("}");

            WriteLine("public byte[] ToByteArray()");
            WriteLine("{");
            Indent();
            WriteLine("var bytes = new byte[{0}];", Bytes);
            for (int i = 0; i < Parts.Length; ++i)
            {
                WriteLine("bytes[{0}] = (byte)({1} >> 0);", i * 4 + 0, Parts[i]);
                WriteLine("bytes[{0}] = (byte)({1} >> 8);", i * 4 + 1, Parts[i]);
                WriteLine("bytes[{0}] = (byte)({1} >> 16);", i * 4 + 2, Parts[i]);
                WriteLine("bytes[{0}] = (byte)({1} >> 24);", i * 4 + 3, Parts[i]);
            }
            WriteLine("return bytes;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Conversions()
        {
            WriteLine("#region Conversions");

            var other = new Integer(Size, !Signed);

            WriteLine("/// <summary>");
            WriteLine("/// Defines an explicit conversion of an {0} value to an {1}.", Name, other);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">The value to convert to an {0}.</param>", other);
            WriteLine("/// <returns>An {0} that is bitwise equal to value.</returns>", other);
            WriteLine("public static explicit operator {0}({1} value)", other, Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", other, 
                string.Join(", ", Parts.Select(part => string.Format("value.{0}", part))));
            Dedent();
            WriteLine("}");

            //foreach (var type in NumberType.Types)
            //{
            //    WriteLine("/// <summary>");
            //    WriteLine("/// Defines an explicit conversion of a {1} value to a {2}.", imex, other, Name);
            //    WriteLine("/// </summary>");
            //    WriteLine("/// <param name=\"value\">The value to convert to a {0}.</param>", Name);
            //    WriteLine("/// <returns>A {0} that has all components equal to value.</returns>", Name); 
            //    if (Type.IsCLSCompliant && !type.IsCLSCompliant)
            //    {
            //        WriteLine("[CLSCompliant(false)]");
            //    }
            //    WriteLine("public static {0} operator {1}({2} value)", imex, Name, other);
            //    WriteLine("{");
            //    Indent();
            //    WriteLine("return new {0}({1});", Name, casts);
            //    Dedent();
            //    WriteLine("}");
            //}

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
                string.Join(" + ", Parts.Select(part => string.Format("{0}.GetHashCode()", part))));
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
            WriteLine("/// integer have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The integer to compare.</param>");
            WriteLine("/// <returns>true if this integer and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two integers are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first integer to compare.</param>");
            WriteLine("/// <param name=\"right\">The second integer to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" & ", Parts.Select(part => string.Format("left.{0} == right.{0}", part))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two integers are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first integer to compare.</param>");
            WriteLine("/// <param name=\"right\">The second integer to compare.</param>");
            WriteLine("/// <returns>true if the left and right are not equal; otherwise, false.</returns>");
            WriteLine("public static bool operator !=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" | ", Parts.Select(part => string.Format("left.{0} != right.{0}", part))));
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Comparable()
        {

            WriteLine("#region Comparable");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether the current instance and a specified");
            WriteLine("/// integer have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The integer to compare.</param>");
            WriteLine("/// <returns>true if this integer and value have the same value; otherwise, false.</returns>");
            WriteLine("public int CompareTo({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("var sub = this - other;");
            WriteLine("if((~int.MaxValue & sub.{0}) != 0) return -1;", Parts.Last());
            WriteLine("if({0}) return 0;", string.Join(" & ",
                Parts.Select(part => string.Format("sub.{0} == 0", part))));
            WriteLine("return 1;");
            Dedent();
            WriteLine("}");

            WriteLine("public static bool operator <({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.CompareTo(right) < 0;");
            Dedent();
            WriteLine("}");

            WriteLine("public static bool operator <=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.CompareTo(right) <= 0;");
            Dedent();
            WriteLine("}");

            WriteLine("public static bool operator >({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.CompareTo(right) > 0;");
            Dedent();
            WriteLine("}");

            WriteLine("public static bool operator >=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.CompareTo(right) >= 0;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Parse()
        {
            WriteLine("#region Parse"); 
            
            WriteLine("public static bool TryParse(string value, out {0} result)", Name);
            Indent("{");
            WriteLine("return TryParse(value, System.Globalization.NumberStyles.Any, null, out result);");
            Dedent("}");

            WriteLine("public static bool TryParse(string value, System.Globalization.NumberStyles style, System.IFormatProvider provider, out {0} result)", Name);
            Indent("{");
            WriteLine("System.Numerics.BigInteger bigint;");
            WriteLine("bool parse = System.Numerics.BigInteger.TryParse(value, style, provider, out bigint);");
            WriteLine("byte[] bytes = bigint.ToByteArray();");
            WriteLine("if (bytes.Length <= {0} || (bytes.Length == {1} && bytes[{0}] == 0))", Bytes, Bytes + 1);
            Indent("{");
            WriteLine("result = new {0}(bytes);", Name);
            WriteLine("return true;");
            Dedent("}");
            WriteLine("else");
            Indent("{");
            WriteLine("result = Zero;");
            WriteLine("return false;");
            Dedent("}");
            Dedent("}");            

            WriteLine("#endregion");
        }


        void String()
        {
            WriteLine("#region ToString");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current integer to its equivalent string");
            WriteLine("/// representation in Cartesian form.");
            WriteLine("/// </summary>");
            WriteLine("/// <returns>The string representation of the current instance in Cartesian form.</returns>");
            WriteLine("public override string ToString()");
            WriteLine("{");
            Indent();
            WriteLine("return ToString(\"G\", System.Globalization.CultureInfo.CurrentCulture);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current integer to its equivalent string");
            WriteLine("/// representation in Cartesian form by using the specified culture-specific");
            WriteLine("/// formatting information.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"provider\">An object that supplies culture-specific formatting information.</param>");
            WriteLine("/// <returns>The string representation of the current instance in Cartesian form, as specified");
            WriteLine("/// by provider.</returns>");
            WriteLine("public string ToString(System.IFormatProvider provider)");
            WriteLine("{");
            Indent();
            WriteLine("return ToString(\"G\", provider);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current integer to its equivalent string");
            WriteLine("/// representation in Cartesian form by using the specified format for its components.");
            WriteLine("/// formatting information.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"format\">A standard or custom numeric format string.</param>");
            WriteLine("/// <returns>The string representation of the current instance in Cartesian form.</returns>");
            WriteLine("/// <exception cref=\"System.FormatException\">format is not a valid format string.</exception>");
            WriteLine("public string ToString(string format)");
            WriteLine("{");
            Indent();
            WriteLine("return ToString(format, System.Globalization.CultureInfo.CurrentCulture);");
            Dedent();
            WriteLine("}");            

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current integer to its equivalent string");
            WriteLine("/// representation in Cartesian form by using the specified format and culture-specific");
            WriteLine("/// format information for its components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"format\">A standard or custom numeric format string.</param>");
            WriteLine("/// <returns>The string representation of the current instance in Cartesian form, as specified");
            WriteLine("/// by format and provider.</returns>");
            WriteLine("/// <exception cref=\"System.FormatException\">format is not a valid format string.</exception>");
            WriteLine("public string ToString(string format, System.IFormatProvider provider)");
            WriteLine("{");
            Indent();
            WriteLine("var bytes = ToByteArray();");
            if (!Signed)
            {
                WriteLine("bytes = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(bytes, new byte[] { 0 }));");
            }
            WriteLine("return new System.Numerics.BigInteger(bytes).ToString(format, provider);");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Functions()
        {
            WriteLine("/// <summary>");
            WriteLine("/// Provides static methods for integer functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Integer");
            WriteLine("{");
            Indent();

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to an <see cref=\"Ibasa.IO.BinaryWriter\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this Ibasa.IO.BinaryWriter writer, {0} integer)", Name);
            WriteLine("{");
            Indent();
            WriteLine("writer.Write(integer.ToByteArray());");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> from an <see cref=\"Ibasa.IO.BinaryReader\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static {0} Read{0}(this Ibasa.IO.BinaryReader reader)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}(reader.ReadBytes({1}));", Name, Bytes);
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Operations
            WriteLine("#region Operations");

            //var neg_result = new Integer(Size, true);

            //WriteLine("/// <summary>");
            //WriteLine("/// Returns the additive inverse of a integer.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"value\">A integer.</param>");
            //WriteLine("/// <returns>The negative of value.</returns>");
            //WriteLine("public static {0} Negative({1} value)", neg_result, Name);
            //WriteLine("{");
            //Indent();
            //WriteLine("return new {0}({1});", neg_integer,
            //    string.Join(", ", Components.Select(component => "-value." + component)));
            //Dedent();
            //WriteLine("}");

            //WriteLine("/// <summary>");
            //WriteLine("/// Adds two integers and returns the result.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"left\">The first value to add.</param>");
            //WriteLine("/// <param name=\"right\">The second value to add.</param>");
            //WriteLine("/// <returns>The sum of left and right.</returns>");
            //WriteLine("public static {0} Add({1} left, {1} right)", result, Name);
            //WriteLine("{");
            //Indent();
            //WriteLine("return new {0}({1});", result,
            //    string.Join(", ", Components.Select(component => string.Format("left.{0} + right.{0}", component))));
            //Dedent();
            //WriteLine("}");

            //WriteLine("/// <summary>");
            //WriteLine("/// Subtracts one integers from another and returns the result.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"left\">The value to subtract from (the minuend).</param>");
            //WriteLine("/// <param name=\"right\">The value to subtract (the subtrahend).</param>");
            //WriteLine("/// <returns>The result of subtracting right from left (the difference).</returns>");
            //WriteLine("public static {0} Subtract({1} left, {1} right)", result, Name);
            //WriteLine("{");
            //Indent();
            //WriteLine("return new {0}({1});", result,
            //    string.Join(", ", Components.Select(component => string.Format("left.{0} - right.{0}", component))));
            //Dedent();
            //WriteLine("}");

            //WriteLine("/// <summary>");
            //WriteLine("/// Returns the product of a integer and scalar.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"integer\">The integer to multiply.</param>");
            //WriteLine("/// <param name=\"scalar\">The scalar to multiply.</param>");
            //WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            //WriteLine("public static {0} Multiply({1} integer, {2} scalar)", result, Name, result.Type);
            //WriteLine("{");
            //Indent();
            //WriteLine("return new {0}({1});", result,
            //    string.Join(", ", Components.Select(component => string.Format("integer.{0} * scalar", component))));
            //Dedent();
            //WriteLine("}");

            //WriteLine("/// <summary>");
            //WriteLine("/// Divides a integer by a scalar and returns the result.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"integer\">The integer to be divided (the dividend).</param>");
            //WriteLine("/// <param name=\"scalar\">The scalar to divide by (the divisor).</param>");
            //WriteLine("/// <returns>The result of dividing left by right (the quotient).</returns>");
            //WriteLine("public static {0} Divide({1} integer, {2} scalar)", result, Name, result.Type);
            //WriteLine("{");
            //Indent();
            //WriteLine("return new {0}({1});", result,
            //    string.Join(", ", Components.Select(component => string.Format("integer.{0} / scalar", component))));
            //Dedent();
            //WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the bitwise not of an integer.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">An integer.</param>");
            WriteLine("/// <returns>The bitwise not of value.</returns>");
            WriteLine("public static {0} BitwiseNot({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return ~value;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the bitwise and of two integers.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first integer.</param>");
            WriteLine("/// <param name=\"right\">The second integer.</param>");
            WriteLine("/// <returns>The bitwise and of left and right.</returns>");
            WriteLine("public static {0} BitwiseAnd({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left & right;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the bitwise or of two integers.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first integer.</param>");
            WriteLine("/// <param name=\"right\">The second integer.</param>");
            WriteLine("/// <returns>The bitwise or of left and right.</returns>");
            WriteLine("public static {0} BitwiseOr({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left | right;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the bitwise xor of two integers.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first integer.</param>");
            WriteLine("/// <param name=\"right\">The second integer.</param>");
            WriteLine("/// <returns>The bitwise xor of left and right.</returns>");
            WriteLine("public static {0} BitwiseXor({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left ^ right;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two integers are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first integer to compare.</param>");
            WriteLine("/// <param name=\"right\">The second integer to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool Equals({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left == right;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            //WriteLine("/// <summary>");
            //WriteLine("/// Returns the absolute value (per component).");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"value\">A integer.</param>");
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
            //WriteLine("/// Returns a integer that contains the lowest value from each pair of components.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"value1\">The first integer.</param>");
            //WriteLine("/// <param name=\"value2\">The second integer.</param>");
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
            //WriteLine("/// Returns a integer that contains the highest value from each pair of components.");
            //WriteLine("/// </summary>");
            //WriteLine("/// <param name=\"value1\">The first integer.</param>");
            //WriteLine("/// <param name=\"value2\">The second integer.</param>");
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
            //WriteLine("/// <param name=\"value\">A integer to constrain.</param>");
            //WriteLine("/// <param name=\"min\">The minimum values for each component.</param>");
            //WriteLine("/// <param name=\"max\">The maximum values for each component.</param>");
            //WriteLine("/// <returns>A integer with each component constrained to the given range.</returns>");
            //if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            //WriteLine("public static {0} Clamp({0} value, {0} min, {0} max)", Name);
            //WriteLine("{");
            //Indent();
            //WriteLine("return new {0}({1});", Name, 
            //    string.Join(", ", Components.Select(component => string.Format("Functions.Clamp(value.{0}, min.{0}, max.{0})", component))));
            //Dedent();
            //WriteLine("}");
            //WriteLine("#endregion");
    
            Dedent();
            WriteLine("}");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
