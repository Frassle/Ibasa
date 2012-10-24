using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Ellipse : Generator
    {
        public NumberType Type { get; private set; }

        public Ellipse(NumberType type)
        {
            Type = type;
        }

        public string Name
        {
            get
            {
                return string.Format("Ellipse{0}", Type.Suffix);
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
            WriteLine("/// Represents a ellipse in a two-dimensional space.");
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
            WriteLine("/// Returns a new unit <see cref=\"{0}\"/> at the origin.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} Unit = new {0}(0, 0, 1, 1);", Name);

            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            WriteLine("/// <summary>");
            WriteLine("/// The X component of the ellipse.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} X;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The Y component of the ellipse.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Y;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The Width of the ellipse.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Width;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The Height of the ellipse.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Height;", Type);

            WriteLine("#endregion");
        }

        void Properties()
        {
            WriteLine("#region Properties");
            WriteLine("/// <summary>");
            WriteLine("/// Gets the coordinates of the center of this ellipse.");
            WriteLine("/// </summary>");
            WriteLine("public Point2{0} Center {{ get {{ return new Point2{0}(X, Y); }} }}", Type.Suffix);

            WriteLine("/// <summary>");
            WriteLine("/// Gets the size of this ellipse.");
            WriteLine("/// </summary>");
            WriteLine("public Size2{0} Size {{ get {{ return new Size2{0}(Width, Height); }} }}", Type.Suffix);

            WriteLine("#endregion");
        }

        void Constructors()
        {
            WriteLine("#region Constructors");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified values.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"x\">Value for the X component of the ellipse.</param>");
            WriteLine("/// <param name=\"y\">Value for the Y component of the ellipse.</param>");
            WriteLine("/// <param name=\"width\">Value for the width of the ellipse.</param>");
            WriteLine("/// <param name=\"height\">Value for the height of the ellipse.</param>");
            WriteLine("public {0}({1} x, {1} y, {1} width, {1} height)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= width);");
            WriteLine("Contract.Requires(0 <= height);");
            WriteLine("X = x;");
            WriteLine("Y = y;");
            WriteLine("Width = width;");
            WriteLine("Height = height;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified location and radius.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"center\">The center of the ellipse.</param>");
            WriteLine("/// <param name=\"width\">The width of the ellipse.</param>");
            WriteLine("/// <param name=\"height\">The height of the ellipse.</param>");
            WriteLine("public {0}(Point2{1} center, {2} width, {2} height)", Name, Type.Suffix, Type);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= width);");
            WriteLine("Contract.Requires(0 <= height);");
            WriteLine("X = center.X;");
            WriteLine("Y = center.Y;");
            WriteLine("Width = width;");
            WriteLine("Height = height;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified location and radius.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"center\">The center of the ellipse.</param>");
            WriteLine("/// <param name=\"size\">The size of the ellipse.</param>");
            WriteLine("public {0}(Point2{1} center, Size2{1} size)", Name, Type.Suffix);
            WriteLine("{");
            Indent();
            WriteLine("X = center.X;");
            WriteLine("Y = center.Y;");
            WriteLine("Width = size.Width;");
            WriteLine("Height = size.Height;");
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

                Ellipse other = new Ellipse(type);

                var casts = string.Format("({0})value.X, ({0})value.Y, ({0})value.Width, ({0})value.Height", Type);

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

            foreach (var type in Shapes.Types.Where(t => t != Type))
            {
                string imex = type.IsImplicitlyConvertibleTo(Type) ? "implicit" : "explicit";

                Circle other = new Circle(type);

                var casts = string.Format("({0})value.X, ({0})value.Y, ({0})(value.Radius * 2), ({0})(value.Radius * 2)", Type);

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
            WriteLine("return X.GetHashCode() + Y.GetHashCode() + Width.GetHashCode() + Height.GetHashCode();");
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
            WriteLine("/// ellipse have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The ellipse to compare.</param>");
            WriteLine("/// <returns>true if this ellipse and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two ellipses are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first ellipse to compare.</param>");
            WriteLine("/// <param name=\"right\">The second ellipse to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.X == right.X & left.Y == right.Y & left.Width == right.Width & left.Height == right.Height;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two ellipses are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first ellipse to compare.</param>");
            WriteLine("/// <param name=\"right\">The second ellipse to compare.</param>");
            WriteLine("/// <returns>true if left and right are not equal; otherwise, false.</returns>");
            WriteLine("public static bool operator !=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.X != right.X | left.Y != right.Y | left.Width != right.Width & left.Height != right.Height;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void String()
        {
            WriteLine("#region ToString");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current ellipse to its equivalent string");
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
            WriteLine("/// Converts the value of the current ellipse to its equivalent string");
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
            WriteLine("/// Converts the value of the current ellipse to its equivalent string");
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
            WriteLine("/// Converts the value of the current ellipse to its equivalent string");
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
            WriteLine("return String.Format(\"({0}, {1})\", Center.ToString(format, provider), Width.ToString(format, provider), Height.ToString(format, provider));");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Functions()
        {
            WriteLine("/// <summary>");
            WriteLine("/// Provides static methods for ellipse functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Ellipse");
            WriteLine("{");
            Indent();

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to an <see cref=\"Ibasa.IO.BinaryWriter\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this Ibasa.IO.BinaryWriter writer, {0} ellipse)", Name);
            WriteLine("{");
            Indent();
            WriteLine("writer.Write(ellipse.X);");
            WriteLine("writer.Write(ellipse.Y);");
            WriteLine("writer.Write(ellipse.Width);");
            WriteLine("writer.Write(ellipse.Height);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> from an <see cref=\"Ibasa.IO.BinaryReader\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static {0} Read{0}(this Ibasa.IO.BinaryReader reader)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1}, {1}, {1}, {1});", Name, string.Format("reader.Read{0}()", Type.CLRName));
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two ellipsees are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first ellipse to compare.</param>");
            WriteLine("/// <param name=\"right\">The second ellipse to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool Equals({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left == right;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            //public static ellipse Intersect(ellipse value1, ellipse value2)
            //{
            //    int l = Functions.Max(value1.Left, value2.Left);
            //    int r = Functions.Min(value1.Right, value2.Right);
            //    int b = Functions.Max(value1.Bottom, value2.Bottom);
            //    int t = Functions.Min(value1.Top, value2.Top);
            //    int fr = Functions.Max(value1.Front, value2.Front);
            //    int bk = Functions.Min(value1.Back, value2.Back);
            //    if ((r >= l) && (t >= b) && (bk >= fr))
            //    {
            //        return new ellipse(l, b, fr, r - l, t - b, bk - fr);
            //    }
            //    return Empty;

            //}
            //public static ellipse Union(ellipse value1, ellipse value2)
            //{
            //    int l = Functions.Max(value1.Left, value2.Left);
            //    int r = Functions.Min(value1.Right, value2.Right);
            //    int b = Functions.Max(value1.Bottom, value2.Bottom);
            //    int t = Functions.Min(value1.Top, value2.Top);
            //    int fr = Functions.Max(value1.Front, value2.Front);
            //    int bk = Functions.Min(value1.Back, value2.Back);
            //    return new ellipse(l, b, fr, r - l, t - b, bk - fr);
            //}

            //public static ellipse Inflate(ellipse ellipse, Size3D amount)
            //{
            //    return new ellipse(
            //        ellipse.X - amount.Width,
            //        ellipse.Y - amount.Height,
            //        ellipse.Z - amount.Depth,
            //        ellipse.Width + (amount.Width * 2),
            //        ellipse.Height + (amount.Height * 2),
            //        ellipse.Depth + (amount.Depth * 2));
            //}
            //public static ellipse Offset(ellipse ellipse, Point3D amount)
            //{
            //    return new ellipse(
            //        ellipse.Location + amount,
            //        ellipse.Size);
            //}

            //WriteLine("#region Contains");
            //WriteLine("public static bool Contains({0} ellipse, {1} point)", Name, new Point(Type, 2));
            //WriteLine("{");
            //Indent();
            //WriteLine("var distance = Vector.AbsoluteSquared(ellipse.Center - point);");
            //WriteLine("return distance <= ellipse.XRadius * ellipse.XRadius || distance <= ellipse.YRadius * ellipse.YRadius;");
            //Dedent();
            //WriteLine("}");
            //WriteLine("#endregion");
            //public bool Contains(ellipse ellipse)
            //{
            //    return (Left <= ellipse.Left) && (Right >= ellipse.Right) &&
            //        (Bottom <= ellipse.Bottom) && (Top >= ellipse.Top) &&
            //        (Front <= ellipse.Front) && (Back >= ellipse.Back);
            //}
            //public bool Intersects(ellipse ellipse)
            //{
            //    return (Right > ellipse.Left) && (Left < ellipse.Right) &&
            //        (Top < ellipse.Bottom) && (Bottom < ellipse.Top) &&
            //        (Back < ellipse.Front) && (Front < ellipse.Back);
            //}


            ///// <summary>
            ///// Returns a value that indicates whether two ellipses are equal.
            ///// </summary>
            ///// <param name="left">The first ellipse to compare.</param>
            ///// <param name="right">The second ellipse to compare.</param>
            ///// <returns>true if the left and right are equal; otherwise, false.</returns>
            //public static bool Equals(ellipse left, ellipse right)
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
