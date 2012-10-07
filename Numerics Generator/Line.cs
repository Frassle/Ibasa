using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Line : Generator
    {
        public NumberType Type { get; private set; }
        public int Dimension { get; private set; }

        public Line(NumberType type, int dimension)
        {
            Type = type;
            Dimension = dimension;
        }

        public string Name
        {
            get
            {
                return string.Format("Line{0}{1}", Dimension == 1 ? "" : Dimension.ToString(), Type.Suffix);
            }
        }

        public string Point
        {
            get 
            {
                return Dimension == 1 ? Type.ToString() : new Point(Type, Dimension).ToString();
            }
        }

        public void Generate()
        {
            Namespace();
        }

        void Namespace()
        {
            WriteLine("using System;");
            WriteLine("using System.Collections.Generic;");
            WriteLine("using System.Diagnostics.Contracts;");
            WriteLine("using System.Globalization;");
            WriteLine("using System.Linq;");
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
            if (Dimension == 1)
            {
                WriteLine("/// Represents a line in a one-dimensional space.");
            }
            else if (Dimension == 2)
            {
                WriteLine("/// Represents a line in a two-dimensional space.");
            }
            else
            {
                WriteLine("/// Represents a line in a three-dimensional space.");
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
            WriteLine("/// Returns a new empty <see cref=\"{0}\"/>.", Name);
            WriteLine("/// </summary>");
            if (Dimension == 1)
            {
                WriteLine("public static readonly {0} Empty = new {0}(0, 0);", Name);
            }
            else
            {
                WriteLine("public static readonly {0} Empty = new {0}({1}.Zero, {1}.Zero);", Name, Point);
            }
            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            WriteLine("/// <summary>");
            WriteLine("/// The coordinates of the start of this line.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Start;", Point);
            WriteLine("/// <summary>");
            WriteLine("/// The coordinates of the end of this line.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} End;", Point);

            WriteLine("#endregion");
        }

        void Properties()
        {
            WriteLine("#region Properties");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the length of this line.");
            WriteLine("/// </summary>");
            WriteLine("/// <returns>The length of this line.</returns>");
            WriteLine("public {0} Length", Type.RealType);
            WriteLine("{");
            Indent();
            WriteLine("get");
            WriteLine("{");
            Indent();
            if (Dimension == 1)
            {
                WriteLine("return Functions.Abs(End - Start);");
            }
            else
            {
                WriteLine("return Point.Distance(Start, End);");
            }
            Dedent();
            WriteLine("}");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Gets the coordinates of the center of this line.");
            WriteLine("/// </summary>");
            WriteLine("public {0} Center", Point);
            WriteLine("{");
            Indent();
            WriteLine("get");
            WriteLine("{");
            Indent();
            if (Dimension == 1)
            {
                WriteLine("return (Start + End) / 2;");
            }
            else
            {
                Point p = new Point(Type, Dimension);
                var sums = string.Join(", ", p.Components.Select(c => string.Format("(Start.{0} + End.{0}) / 2", c)));
                WriteLine("return new {0}({1});", Point, sums);
            }
            Dedent();
            WriteLine("}");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Constructors()
        {
            WriteLine("#region Constructors");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified values.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"start\">Start point of the line.</param>");
            WriteLine("/// <param name=\"end\">End point of the line.</param>");
            WriteLine("public {0}({1} start, {1} end)", Name, Point);
            WriteLine("{");
            Indent();
            WriteLine("Start = start;");
            WriteLine("End = end;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Operations()
        {
            WriteLine("#region Operations");

            WriteLine("#endregion");
        }

        void Conversions()
        {
            WriteLine("#region Conversions");

            foreach (var type in Shapes.Types.Where(t => t != Type))
            {
                string imex = type.IsImplicitlyConvertibleTo(Type) ? "implicit" : "explicit";
                Line other = new Line(type, Dimension);

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
                WriteLine("return new {0}(({1})value.Start, ({1})value.End);", Name, Point);
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
            WriteLine("return Start.GetHashCode() + End.GetHashCode();");
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
            WriteLine("if (obj is {0}) return Equals(({0})obj);", Name);
            WriteLine("return false;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether the current instance and a specified");
            WriteLine("/// line have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The line to compare.</param>");
            WriteLine("/// <returns>true if this line and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two lines are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first line to compare.</param>");
            WriteLine("/// <param name=\"right\">The second line to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.Start == right.Start && left.End == right.End;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two lines are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first line to compare.</param>");
            WriteLine("/// <param name=\"right\">The second line to compare.</param>");
            WriteLine("/// <returns>true if left and right are not equal; otherwise, false.</returns>");
            WriteLine("public static bool operator !=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.Start != right.Start || left.End != right.End;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void String()
        {
            WriteLine("#region ToString");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current line to its equivalent string");
            WriteLine("/// representation.");
            WriteLine("/// </summary>");
            WriteLine("/// <returns>The string representation of the current instance.</returns>");
            WriteLine("public override string ToString()");
            WriteLine("{");
            Indent();
            WriteLine("Contract.Ensures(Contract.Result<string>() != null);");
            WriteLine("return ToString(\"G\", CultureInfo.CurrentCulture);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current line to its equivalent string");
            WriteLine("/// representation by using the specified culture-specific");
            WriteLine("/// formatting information.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"provider\">An object that supplies culture-specific formatting information.</param>");
            WriteLine("/// <returns>The string representation of the current instance, as specified");
            WriteLine("/// by provider.</returns>");
            WriteLine("public string ToString(IFormatProvider provider)");
            WriteLine("{");
            Indent();
            WriteLine("Contract.Ensures(Contract.Result<string>() != null);");
            WriteLine("return ToString(\"G\", provider);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current line to its equivalent string");
            WriteLine("/// representation by using the specified format for its components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"format\">A standard or custom numeric format string.</param>");
            WriteLine("/// <returns>The string representation of the current instance.</returns>");
            WriteLine("/// <exception cref=\"System.FormatException\">format is not a valid format string.</exception>");
            WriteLine("public string ToString(string format)");
            WriteLine("{");
            Indent();
            WriteLine("Contract.Ensures(Contract.Result<string>() != null);");
            WriteLine("return ToString(format, CultureInfo.CurrentCulture);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current line to its equivalent string");
            WriteLine("/// representation by using the specified format and culture-specific");
            WriteLine("/// format information for its components.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"format\">A standard or custom numeric format string.</param>");
            WriteLine("/// <param name=\"provider\">An object that supplies culture-specific formatting information.</param>");
            WriteLine("/// <returns>The string representation of the current instance, as specified");
            WriteLine("/// by format and provider.</returns>");
            WriteLine("/// <exception cref=\"System.FormatException\">format is not a valid format string.</exception>");
            WriteLine("public string ToString(string format, IFormatProvider provider)");
            WriteLine("{");
            Indent();
            WriteLine("Contract.Ensures(Contract.Result<string>() != null);");
            WriteLine("return String.Format(\"({0}, {1})\", Start.ToString(format, provider), End.ToString(format, provider));");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Functions()
        {
            WriteLine("/// <summary>");
            WriteLine("/// Provides static methods for line functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Line");
            WriteLine("{");
            Indent();

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to an <see cref=\"Ibasa.IO.BinaryWriter\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this Ibasa.IO.BinaryWriter writer, {0} line)", Name);
            WriteLine("{");
            Indent();
            WriteLine("writer.Write(line.Start);");
            WriteLine("writer.Write(line.End);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> from an <see cref=\"Ibasa.IO.BinaryReader\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static {0} Read{0}(this Ibasa.IO.BinaryReader reader)", Name);
            WriteLine("{");
            Indent();
            if (Dimension == 1)
            {
                WriteLine("return new {0}(reader.Read{1}(), reader.Read{1}());", Name, Type.CLRName);
            }
            else
            {
                WriteLine("return new {0}(reader.Read{1}(), reader.Read{1}());", Name, Point);
            }
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two lines are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first line to compare.</param>");
            WriteLine("/// <param name=\"right\">The second line to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool Equals({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left == right;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Intersect
            WriteLine("#region Intersect");
            if (Dimension == 1)
            {
                WriteLine("/// <summary>");
                WriteLine("/// Returns the intersection of two lines.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"left\">The first line.</param>");
                WriteLine("/// <param name=\"right\">The second line.</param>");
                WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
                WriteLine("public static {0}? Intersect({0} left, {0} right)", Name);
                WriteLine("{");
                Indent();
                WriteLine("var left_min = Functions.Min(left.Start, left.End);");
                WriteLine("var left_max = Functions.Max(left.Start, left.End);");
                WriteLine("var right_min = Functions.Min(right.Start, right.End);");
                WriteLine("var right_max = Functions.Max(right.Start, right.End);");
                WriteLine("var min = Functions.Max(left_min, right_min);");
                WriteLine("var max = Functions.Min(left_max, right_max);");
                WriteLine("return min <= max ? new {0}?(new {0}(min, max)) : null;", Name);
                Dedent();
                WriteLine("}");
            }
            WriteLine("#endregion");
            #endregion

            //public static line Intersect(line value1, line value2)
            //{
            //    int l = Functions.Max(value1.Left, value2.Left);
            //    int r = Functions.Min(value1.Right, value2.Right);
            //    int b = Functions.Max(value1.Bottom, value2.Bottom);
            //    int t = Functions.Min(value1.Top, value2.Top);
            //    int fr = Functions.Max(value1.Front, value2.Front);
            //    int bk = Functions.Min(value1.Back, value2.Back);
            //    if ((r >= l) && (t >= b) && (bk >= fr))
            //    {
            //        return new line(l, b, fr, r - l, t - b, bk - fr);
            //    }
            //    return Empty;

            //}
            //public static line Union(line value1, line value2)
            //{
            //    int l = Functions.Max(value1.Left, value2.Left);
            //    int r = Functions.Min(value1.Right, value2.Right);
            //    int b = Functions.Max(value1.Bottom, value2.Bottom);
            //    int t = Functions.Min(value1.Top, value2.Top);
            //    int fr = Functions.Max(value1.Front, value2.Front);
            //    int bk = Functions.Min(value1.Back, value2.Back);
            //    return new line(l, b, fr, r - l, t - b, bk - fr);
            //}

            //public static line Inflate(line line, Size3D amount)
            //{
            //    return new line(
            //        line.X - amount.Width,
            //        line.Y - amount.Height,
            //        line.Z - amount.Depth,
            //        line.Width + (amount.Width * 2),
            //        line.Height + (amount.Height * 2),
            //        line.Depth + (amount.Depth * 2));
            //}
            //public static line Offset(line line, Point3D amount)
            //{
            //    return new line(
            //        line.Location + amount,
            //        line.Size);
            //}

            //WriteLine("#region Contains");
            //WriteLine("public static bool Contains({0} line, {1} point)", Name, new Point(Type, 2));
            //WriteLine("{");
            //Indent();
            //WriteLine("return Vector.AbsoluteSquared(line.Center - point) <= line.Radius * line.Radius;");
            //Dedent();
            //WriteLine("}");
            //WriteLine("#endregion");
            //public bool Contains(line line)
            //{
            //    return (Left <= line.Left) && (Right >= line.Right) &&
            //        (Bottom <= line.Bottom) && (Top >= line.Top) &&
            //        (Front <= line.Front) && (Back >= line.Back);
            //}
            //public bool Intersects(line line)
            //{
            //    return (Right > line.Left) && (Left < line.Right) &&
            //        (Top < line.Bottom) && (Bottom < line.Top) &&
            //        (Back < line.Front) && (Front < line.Back);
            //}


            ///// <summary>
            ///// Returns a value that indicates whether two lines are equal.
            ///// </summary>
            ///// <param name="left">The first line to compare.</param>
            ///// <param name="right">The second line to compare.</param>
            ///// <returns>true if the left and right are equal; otherwise, false.</returns>
            //public static bool Equals(line left, line right)
            //{
            //    return left == right;
            //}

            Dedent();
            WriteLine("}");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
