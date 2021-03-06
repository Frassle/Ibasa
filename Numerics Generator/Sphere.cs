﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Sphere : Generator
    {
        public NumberType Type { get; private set; }

        public Sphere(NumberType type)
        {
            Type = type;
        }

        public string Name
        {
            get
            {
                return string.Format("Sphere{0}", Type.Suffix);
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
            WriteLine("/// Represents a sphere in a three-dimensional space.");
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
            WriteLine("public static readonly {0} Unit = new {0}(0, 0, 0, 1);", Name);

            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            WriteLine("/// <summary>");
            WriteLine("/// The X component of the sphere.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} X;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The Y component of the sphere.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Y;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The Z component of the sphere.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Z;", Type);
            WriteLine("/// <summary>");
            WriteLine("/// The Radius component of the sphere.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Radius;", Type);

            WriteLine("#endregion");
        }

        void Properties()
        {
            WriteLine("#region Properties");

            WriteLine("/// <summary>");
            WriteLine("/// Gets the diameter of this sphere.");
            WriteLine("/// </summary>");
            WriteLine("public {0} Diameter {{ get {{ return Radius * 2; }} }}", Type);
            WriteLine("/// <summary>");
            WriteLine("/// Gets the coordinates of the center of this sphere.");
            WriteLine("/// </summary>");
            WriteLine("public Point3{0} Center {{ get {{ return new Point3{0}(X, Y, Z); }} }}", Type.Suffix);

            WriteLine("#endregion");
        }

        void Constructors()
        {
            WriteLine("#region Constructors");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified values.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"x\">Value for the X component of the sphere.</param>");
            WriteLine("/// <param name=\"y\">Value for the Y component of the sphere.</param>");
            WriteLine("/// <param name=\"z\">Value for the Z component of the sphere.</param>");
            WriteLine("/// <param name=\"radius\">Value for the Radius of the sphere.</param>");
            WriteLine("public {0}({1} x, {1} y, {1} z, {1} radius)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= radius);");
            WriteLine("X = x;");
            WriteLine("Y = y;");
            WriteLine("Z = z;");
            WriteLine("Radius = radius;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified location and radius.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"center\">The center of the sphere.</param>");
            WriteLine("/// <param name=\"radius\">The radius of the sphere.</param>");
            WriteLine("public {0}(Point3{1} center, {2} radius)", Name, Type.Suffix, Type);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(0 <= radius);");
            WriteLine("X = center.X;");
            WriteLine("Y = center.Y;");
            WriteLine("Z = center.Z;");
            WriteLine("Radius = radius;");
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

                Sphere other = new Sphere(type);

                var casts = string.Format("({0})value.X, ({0})value.Y, ({0})value.Z, ({0})value.Radius", Type);

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
            WriteLine("return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + Radius.GetHashCode();");
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
            WriteLine("/// sphere have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The sphere to compare.</param>");
            WriteLine("/// <returns>true if this sphere and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two spheres are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first sphere to compare.</param>");
            WriteLine("/// <param name=\"right\">The second sphere to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.X == right.X & left.Y == right.Y & left.Z == right.Z & left.Radius == right.Radius;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two spheres are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first sphere to compare.</param>");
            WriteLine("/// <param name=\"right\">The second sphere to compare.</param>");
            WriteLine("/// <returns>true if left and right are not equal; otherwise, false.</returns>");
            WriteLine("public static bool operator !=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.X != right.X | left.Y != right.Y | left.Z != right.Z | left.Radius != right.Radius;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void String()
        {
            WriteLine("#region ToString");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current sphere to its equivalent string");
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
            WriteLine("/// Converts the value of the current sphere to its equivalent string");
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
            WriteLine("/// Converts the value of the current sphere to its equivalent string");
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
            WriteLine("/// Converts the value of the current sphere to its equivalent string");
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
            WriteLine("return String.Format(\"({0}, {1})\", Center.ToString(format, provider), Radius.ToString(format, provider));");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Functions()
        {
            WriteLine("/// <summary>");
            WriteLine("/// Provides static methods for sphere functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Sphere");
            WriteLine("{");
            Indent();

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to an <see cref=\"Ibasa.IO.BinaryWriter\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this Ibasa.IO.BinaryWriter writer, {0} sphere)", Name);
            WriteLine("{");
            Indent();
            WriteLine("writer.Write(sphere.X);");
            WriteLine("writer.Write(sphere.Y);");
            WriteLine("writer.Write(sphere.Z);");
            WriteLine("writer.Write(sphere.Radius);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> from an <see cref=\"Ibasa.IO.BinaryWriter\">.", Name);
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
            WriteLine("/// Returns a value that indicates whether two spherees are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first sphere to compare.</param>");
            WriteLine("/// <param name=\"right\">The second sphere to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool Equals({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left == right;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            //public static sphere Intersect(sphere value1, sphere value2)
            //{
            //    int l = Functions.Max(value1.Left, value2.Left);
            //    int r = Functions.Min(value1.Right, value2.Right);
            //    int b = Functions.Max(value1.Bottom, value2.Bottom);
            //    int t = Functions.Min(value1.Top, value2.Top);
            //    int fr = Functions.Max(value1.Front, value2.Front);
            //    int bk = Functions.Min(value1.Back, value2.Back);
            //    if ((r >= l) && (t >= b) && (bk >= fr))
            //    {
            //        return new sphere(l, b, fr, r - l, t - b, bk - fr);
            //    }
            //    return Empty;

            //}
            //public static sphere Union(sphere value1, sphere value2)
            //{
            //    int l = Functions.Max(value1.Left, value2.Left);
            //    int r = Functions.Min(value1.Right, value2.Right);
            //    int b = Functions.Max(value1.Bottom, value2.Bottom);
            //    int t = Functions.Min(value1.Top, value2.Top);
            //    int fr = Functions.Max(value1.Front, value2.Front);
            //    int bk = Functions.Min(value1.Back, value2.Back);
            //    return new sphere(l, b, fr, r - l, t - b, bk - fr);
            //}

            //public static sphere Inflate(sphere sphere, Size3D amount)
            //{
            //    return new sphere(
            //        sphere.X - amount.Width,
            //        sphere.Y - amount.Height,
            //        sphere.Z - amount.Depth,
            //        sphere.Width + (amount.Width * 2),
            //        sphere.Height + (amount.Height * 2),
            //        sphere.Depth + (amount.Depth * 2));
            //}
            //public static sphere Offset(sphere sphere, Point3D amount)
            //{
            //    return new sphere(
            //        sphere.Location + amount,
            //        sphere.Size);
            //}
            
            WriteLine("#region Contains");
            WriteLine("public static bool Contains({0} sphere, {1} point)", Name, new Point(Type, 3));
            WriteLine("{");
            Indent();
            WriteLine("return Vector.AbsoluteSquared(sphere.Center - point) <= sphere.Radius * sphere.Radius;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            //public bool Contains(sphere sphere)
            //{
            //    return (Left <= sphere.Left) && (Right >= sphere.Right) &&
            //        (Bottom <= sphere.Bottom) && (Top >= sphere.Top) &&
            //        (Front <= sphere.Front) && (Back >= sphere.Back);
            //}
            //public bool Intersects(sphere sphere)
            //{
            //    return (Right > sphere.Left) && (Left < sphere.Right) &&
            //        (Top < sphere.Bottom) && (Bottom < sphere.Top) &&
            //        (Back < sphere.Front) && (Front < sphere.Back);
            //}

            
            ///// <summary>
            ///// Returns a value that indicates whether two spheres are equal.
            ///// </summary>
            ///// <param name="left">The first sphere to compare.</param>
            ///// <param name="right">The second sphere to compare.</param>
            ///// <returns>true if the left and right are equal; otherwise, false.</returns>
            //public static bool Equals(sphere left, sphere right)
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
