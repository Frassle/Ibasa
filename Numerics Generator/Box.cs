using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Box : Generator
    {
        public NumberType Type { get; private set; }

        public Box(NumberType type)
        {
            Type = type;
        }

        public string Name
        {
            get
            {
                return string.Format("Box{0}", Type.Suffix);
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
            WriteLine("/// Represents an ordered tuple of integer x, y, width, and height components that defines a");
            WriteLine("/// location and size in a three-dimensional space.");
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
            WriteLine("/// Returns a new <see cref=\"{0}\"/> that has an X, Y, Z, Width, Height and Depth value of 0.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} Empty = new {0}(0, 0, 0, 0, 0, 0);", Name);

            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            WriteLine("/// <summary>");
            WriteLine("/// The X component of the box.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} X;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The Y component of the box.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Y;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The Z component of the box.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Z;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The Width component of the box.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Width;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The Height component of the box.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Height;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The Depth component of the box.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Depth;", Type);

            WriteLine("#endregion");
        }

        void Properties()
        {
            WriteLine("#region Properties");

            WriteLine("/// <summary>");
            WriteLine("/// Gets the y-coordinate of the top face of this box.");
            WriteLine("/// </summary>");
            WriteLine("public {0} Top {{ get {{ return Y + Height; }} }}", Type);
            WriteLine("/// <summary>");
            WriteLine("/// Gets the y-coordinate of the bottom face of this box.");
            WriteLine("/// </summary>");
            WriteLine("public {0} Bottom {{ get {{ return Y; }} }}", Type);
            WriteLine("/// <summary>");
            WriteLine("/// Gets the x-coordinate of the left face of this box.");
            WriteLine("/// </summary>");
            WriteLine("public {0} Left {{ get {{ return X; }} }}", Type);
            WriteLine("/// <summary>");
            WriteLine("/// Gets the x-coordinate of the right face of this box.");
            WriteLine("/// </summary>");
            WriteLine("public {0} Right {{ get {{ return X + Width; }} }}", Type);
            WriteLine("/// <summary>");
            WriteLine("/// Gets the z-coordinate of the front face of this box.");
            WriteLine("/// </summary>");
            WriteLine("public {0} Front {{ get {{ return Z; }} }}", Type);
            WriteLine("/// <summary>");
            WriteLine("/// Gets the z-coordinate of the back face of this box.");
            WriteLine("/// </summary>");
            WriteLine("public {0} Back {{ get {{ return Z + Depth; }} }}", Type);
            WriteLine("/// <summary>");
            WriteLine("/// Gets the coordinates of the center of this box.");
            WriteLine("/// </summary>");
            WriteLine("public Point3{0} Center {{ get {{ return new Point3{0}(X + (Width / 2), Y + (Height / 2), Z + (Depth / 2)); }} }}", Type.Suffix);
            WriteLine("/// <summary>");
            WriteLine("/// Gets the size of this box.");
            WriteLine("/// </summary>");
            WriteLine("public Size3{0} Size {{ get {{ return new Size3{0}(Width, Height, Depth); }} }}", Type.Suffix);
            WriteLine("/// <summary>");
            WriteLine("/// Gets the coordinates of the front-lower-left corner of this box.");
            WriteLine("/// </summary>");
            WriteLine("public Point3{0} Location {{ get {{ return new Point3{0}(X, Y, Z); }} }}", Type.Suffix);
            WriteLine("/// <summary>");
            WriteLine("/// Gets the corners of this box.");
            WriteLine("/// </summary>");
            WriteLine("public Point3{0}[] Corners", Type.Suffix);
            WriteLine("{");
            Indent();
            WriteLine("get");
            WriteLine("{");
            Indent();
            WriteLine("return new Point3{0}[]", Type.Suffix);
            WriteLine("{");
            Indent();
            WriteLine("new Point3{0}(X, Y, Z), new Point3{0}(X + Width, Y, Z), new Point3{0}(X + Width, Y + Height, Z), new Point3{0}(X, Y + Height, Z),", Type.Suffix);
            WriteLine("new Point3{0}(X, Y, Z + Depth), new Point3{0}(X + Width, Y, Z + Depth), new Point3{0}(X + Width, Y + Height, Z + Depth), new Point3{0}(X, Y + Height, Z + Depth)", Type.Suffix);
            Dedent();
            WriteLine("};");
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
            WriteLine("/// <param name=\"x\">Value for the X component of the box.</param>");
            WriteLine("/// <param name=\"y\">Value for the Y component of the box.</param>");
            WriteLine("/// <param name=\"z\">Value for the Z component of the box.</param>");
            WriteLine("/// <param name=\"width\">Value for the Width component of the box.</param>");
            WriteLine("/// <param name=\"height\">Value for the Height component of the box.</param>");
            WriteLine("/// <param name=\"depth\">Value for the Depth component of the box.</param>");
            WriteLine("public {0}({1} x, {1} y, {1} z, {1} width, {1} height, {1} depth)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= width);");
            WriteLine("Contract.Requires(0 <= height);");
            WriteLine("Contract.Requires(0 <= depth);");
            WriteLine("X = x;");
            WriteLine("Y = y;");
            WriteLine("Z = z;");
            WriteLine("Width = width;");
            WriteLine("Height = height;");
            WriteLine("Depth = depth;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified location and size.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"location\">The front-lower-left corner of the box.</param>");
            WriteLine("/// <param name=\"size\">The size of the box.</param>");
            WriteLine("public {0}(Point3{1} location, Size3{1} size)", Name, Type.Suffix);
            WriteLine("{");
            Indent();
            WriteLine("X = location.X;");
            WriteLine("Y = location.Y;");
            WriteLine("Z = location.Z;");
            WriteLine("Width = size.Width;");
            WriteLine("Height = size.Height;");
            WriteLine("Depth = size.Depth;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified location and size.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"x\">Value for the X component of the box.</param>");
            WriteLine("/// <param name=\"y\">Value for the Y component of the box.</param>");
            WriteLine("/// <param name=\"z\">Value for the Z component of the box.</param>");
            WriteLine("/// <param name=\"size\">The size of the box.</param>");
            WriteLine("public {0}({1} x, {1} y, {1} z, Size3{2} size)", Name, Type, Type.Suffix);
            WriteLine("{");
            Indent();
            WriteLine("X = x;");
            WriteLine("Y = y;");
            WriteLine("Z = z;");
            WriteLine("Width = size.Width;");
            WriteLine("Height = size.Height;");
            WriteLine("Depth = size.Depth;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified location and size.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"location\">The front-lower-left corner of the box.</param>");
            WriteLine("/// <param name=\"width\">Value for the Width component of the box.</param>");
            WriteLine("/// <param name=\"height\">Value for the Height component of the box.</param>");
            WriteLine("/// <param name=\"depth\">Value for the Depth component of the box.</param>");
            WriteLine("public {0}(Point3{1} location, {2} width, {2} height, {2} depth)", Name, Type.Suffix, Type);
            WriteLine("{");
            Indent();
            WriteLine("X = location.X;");
            WriteLine("Y = location.Y;");
            WriteLine("Z = location.Z;");
            WriteLine("Width = width;");
            WriteLine("Height = height;");
            WriteLine("Depth = depth;");
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

                Box other = new Box(type);

                var casts = string.Format("({0})value.X, ({0})value.Y, ({0})value.Z, ({0})value.Width, ({0})value.Height, ({0})value.Depth", Type);

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
            WriteLine("return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + Width.GetHashCode() + Height.GetHashCode() + Depth.GetHashCode();");
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
            WriteLine("/// box have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The box to compare.</param>");
            WriteLine("/// <returns>true if this box and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two boxs are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first box to compare.</param>");
            WriteLine("/// <param name=\"right\">The second box to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.X == right.X & left.Y == right.Y & left.Z == right.Z & left.Width == right.Width & left.Height == right.Height & left.Depth == right.Depth;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two boxs are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first box to compare.</param>");
            WriteLine("/// <param name=\"right\">The second box to compare.</param>");
            WriteLine("/// <returns>true if left and right are not equal; otherwise, false.</returns>");
            WriteLine("public static bool operator !=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.X != right.X | left.Y != right.Y | left.Z != right.Z | left.Width != right.Width | left.Height != right.Height | left.Depth != right.Depth;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void String()
        {
            WriteLine("#region ToString");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current box to its equivalent string");
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
            WriteLine("/// Converts the value of the current box to its equivalent string");
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
            WriteLine("/// Converts the value of the current box to its equivalent string");
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
            WriteLine("/// Converts the value of the current box to its equivalent string");
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
            WriteLine("return String.Format(\"({0}, {1})\", Location.ToString(format, provider), Size.ToString(format, provider));");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Functions()
        {
            WriteLine("/// <summary>");
            WriteLine("/// Provides static methods for box functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Box");
            WriteLine("{");
            Indent();

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to a System.IO.BinaryWriter.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this System.IO.BinaryWriter writer, {0} box)", Name);
            WriteLine("{");
            Indent();
            WriteLine("writer.Write(box.X);");
            WriteLine("writer.Write(box.Y);");
            WriteLine("writer.Write(box.Z);");
            WriteLine("writer.Write(box.Width);");
            WriteLine("writer.Write(box.Height);");
            WriteLine("writer.Write(box.Depth);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> to a System.IO.BinaryReader.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static {0} Read{0}(this System.IO.BinaryReader reader)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1}, {1}, {1}, {1}, {1}, {1});", Name, string.Format("reader.Read{0}()", Type.CLRName));
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two boxes are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first box to compare.</param>");
            WriteLine("/// <param name=\"right\">The second box to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool Equals({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left == right;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            //public static Box Intersect(Box value1, Box value2)
            //{
            //    int l = Functions.Max(value1.Left, value2.Left);
            //    int r = Functions.Min(value1.Right, value2.Right);
            //    int b = Functions.Max(value1.Bottom, value2.Bottom);
            //    int t = Functions.Min(value1.Top, value2.Top);
            //    int fr = Functions.Max(value1.Front, value2.Front);
            //    int bk = Functions.Min(value1.Back, value2.Back);
            //    if ((r >= l) && (t >= b) && (bk >= fr))
            //    {
            //        return new Box(l, b, fr, r - l, t - b, bk - fr);
            //    }
            //    return Empty;

            //}
            //public static Box Union(Box value1, Box value2)
            //{
            //    int l = Functions.Max(value1.Left, value2.Left);
            //    int r = Functions.Min(value1.Right, value2.Right);
            //    int b = Functions.Max(value1.Bottom, value2.Bottom);
            //    int t = Functions.Min(value1.Top, value2.Top);
            //    int fr = Functions.Max(value1.Front, value2.Front);
            //    int bk = Functions.Min(value1.Back, value2.Back);
            //    return new Box(l, b, fr, r - l, t - b, bk - fr);
            //}

            //public static Box Inflate(Box box, Size3D amount)
            //{
            //    return new Box(
            //        box.X - amount.Width,
            //        box.Y - amount.Height,
            //        box.Z - amount.Depth,
            //        box.Width + (amount.Width * 2),
            //        box.Height + (amount.Height * 2),
            //        box.Depth + (amount.Depth * 2));
            //}
            //public static Box Offset(Box box, Point3D amount)
            //{
            //    return new Box(
            //        box.Location + amount,
            //        box.Size);
            //}
            
            WriteLine("#region Contains");
            WriteLine("public static bool Contains({0} box, {1} point)", Name, new Point(Type, 3));
            WriteLine("{");
            Indent();
            WriteLine("return (box.Left <= point.X) && (box.Right >= point.X) &&");
            WriteLine("       (box.Bottom <= point.Y) && (box.Top >= point.Y) &&");
            WriteLine("       (box.Front <= point.Z) && (box.Back >= point.Z);");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            //public bool Contains(Box Box)
            //{
            //    return (Left <= Box.Left) && (Right >= Box.Right) &&
            //        (Bottom <= Box.Bottom) && (Top >= Box.Top) &&
            //        (Front <= Box.Front) && (Back >= Box.Back);
            //}
            //public bool Intersects(Box Box)
            //{
            //    return (Right > Box.Left) && (Left < Box.Right) &&
            //        (Top < Box.Bottom) && (Bottom < Box.Top) &&
            //        (Back < Box.Front) && (Front < Box.Back);
            //}

            
            ///// <summary>
            ///// Returns a value that indicates whether two boxs are equal.
            ///// </summary>
            ///// <param name="left">The first box to compare.</param>
            ///// <param name="right">The second box to compare.</param>
            ///// <returns>true if the left and right are equal; otherwise, false.</returns>
            //public static bool Equals(Box left, Box right)
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
