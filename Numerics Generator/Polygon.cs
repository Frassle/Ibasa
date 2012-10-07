using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Polygon : Generator
    {
        public NumberType Type { get; private set; }
        public int Dimension { get; private set; }

        public Polygon(NumberType type, int dimension)
        {
            Type = type;
            Dimension = dimension;
        }

        public string Name
        {
            get
            {
                return string.Format("Polygon{0}{1}", Dimension, Type.Suffix);
            }
        }

        public Point Point
        {
            get
            {
                return new Point(Type, Dimension);
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
            if (Dimension == 2)
            {
                WriteLine("/// Represents a polygon in a two-dimensional space.");
            }
            else
            {
                WriteLine("/// Represents a polygon in a three-dimensional space.");
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

            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            WriteLine("/// <summary>");
            WriteLine("/// The coordinates of the points that make up this polygon.");
            WriteLine("/// </summary>");
            WriteLine("private readonly {0}[] Points;", Point);

            WriteLine("#endregion");
        }

        void Properties()
        {
            WriteLine("#region Properties");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the number of points that make up this polygon.");
            WriteLine("/// </summary>");
            WriteLine("/// <returns>The number of points that make up this polygon.</returns>");
            WriteLine("public int Count");
            WriteLine("{");
            Indent();
            WriteLine("get");
            WriteLine("{");
            Indent();
            WriteLine("return Points == null ? 0 : Points.Length;");
            Dedent();
            WriteLine("}");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the indexed point of this polygon.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"index\">The index of the point.</param>");
            WriteLine("/// <returns>The value of the indexed point.</returns>");
            WriteLine("public {0} this[int index]", Point);
            WriteLine("{");
            Indent();
            WriteLine("get");
            WriteLine("{");
            Indent();
            WriteLine("if (index < 0 || index >= Count)");
            WriteLine("{");
            Indent();
            WriteLine("throw new IndexOutOfRangeException(\"Index out of range.\");");
            Dedent();
            WriteLine("}");
            WriteLine("return Points[index];");
            Dedent();
            WriteLine("}");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Gets the coordinates of the center of this polygon.");
            WriteLine("/// </summary>");
            WriteLine("public {0} Center", new Point(Type.RealType, Dimension));
            WriteLine("{");
            Indent();
            WriteLine("get");
            WriteLine("{");
            Indent();
            WriteLine("return Points == null ? {0}.Zero : Point.Sum(Points, 1.0f / Points.Length);", new Point(Type.RealType, Dimension));
            Dedent();
            WriteLine("}");
            Dedent();
            WriteLine("}");

            WriteLine("public {0}[] ToArray()", Point);
            WriteLine("{");
            Indent();
            WriteLine("var result = new {0}[Count];", Point);
            WriteLine("for (int i=0; i<Count; ++i)");
            WriteLine("{");
            Indent();
            WriteLine("result[i] = this[i];", Point);
            Dedent();
            WriteLine("}");
            WriteLine("return result;");
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
            WriteLine("/// <param name=\"points\">Points to define this polygon.</param>");
            WriteLine("private {0}({1}[] points)", Name, Point);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(points != null);");
            WriteLine("Points = points;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> using the specified values.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"points\">Points to define this polygon.</param>");
            WriteLine("public {0}(IEnumerable<{1}> points)", Name, Point);
            WriteLine("{");
            Indent();
            WriteLine("Contract.Requires(points != null);");
            WriteLine("Points = points.ToArray();");
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
                Polygon other = new Polygon(type, Dimension);

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
                WriteLine("var points = new {0}[value.Count];", Point);
                WriteLine("for (int i=0; i<points.Length; ++i)");
                WriteLine("{");
                Indent();
                WriteLine("points[i] = ({0})value[i];", Point);
                Dedent();
                WriteLine("}");
                WriteLine("return new {0}(points);", Name);
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
            WriteLine("return Points.Sum(point => point.GetHashCode());");
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
            WriteLine("/// polygon have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The polygon to compare.</param>");
            WriteLine("/// <returns>true if this polygon and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two polygons are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first polygon to compare.</param>");
            WriteLine("/// <param name=\"right\">The second polygon to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("if (left.Points.Length != right.Points.Length) { return false; }");
            WriteLine("var left_points = left.Points.GetEnumerator();");
            WriteLine("var right_points = right.Points.GetEnumerator();");
            WriteLine("while (left_points.MoveNext() && right_points.MoveNext())");
            WriteLine("{");
            Indent();
            WriteLine("if (left_points.Current != right_points.Current) { return false; }");
            Dedent();
            WriteLine("}");
            WriteLine("return true;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two polygons are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first polygon to compare.</param>");
            WriteLine("/// <param name=\"right\">The second polygon to compare.</param>");
            WriteLine("/// <returns>true if left and right are not equal; otherwise, false.</returns>");
            WriteLine("public static bool operator !=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("if (left.Points.Length != right.Points.Length) { return true; }");
            WriteLine("var left_points = left.Points.GetEnumerator();");
            WriteLine("var right_points = right.Points.GetEnumerator();");
            WriteLine("while (left_points.MoveNext() && right_points.MoveNext())");
            WriteLine("{");
            Indent();
            WriteLine("if (left_points.Current != right_points.Current) { return true; }");
            Dedent();
            WriteLine("}");
            WriteLine("return false;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void String()
        {
            WriteLine("#region ToString");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current polygon to its equivalent string");
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
            WriteLine("/// Converts the value of the current polygon to its equivalent string");
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
            WriteLine("/// Converts the value of the current polygon to its equivalent string");
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
            WriteLine("/// Converts the value of the current polygon to its equivalent string");
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
            WriteLine("return \"Polygon\";");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Functions()
        {
            WriteLine("/// <summary>");
            WriteLine("/// Provides static methods for polygon functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Polygon");
            WriteLine("{");
            Indent();

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to an <see cref=\"Ibasa.IO.BinaryWriter\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this Ibasa.IO.BinaryWriter writer, {0} polygon)", Name);
            WriteLine("{");
            Indent();
            WriteLine("var array = polygon.ToArray();");
            WriteLine("writer.Write(array.Length);");
            WriteLine("foreach(var point in array)");
            WriteLine("{");
            Indent();
            WriteLine("writer.Write(point);");
            Dedent();
            WriteLine("}");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> from an <see cref=\"Ibasa.IO.BinaryReader\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static {0} Read{0}(this Ibasa.IO.BinaryReader reader)", Name);
            WriteLine("{");
            Indent();
            WriteLine("var length = reader.ReadInt32();");
            WriteLine("var array = new {0}[length];", Point);
            WriteLine("for (int i=0; i<length; ++i)");
            WriteLine("{");
            Indent();
            WriteLine("array[i] = reader.Read{0}();", Point);
            Dedent();
            WriteLine("}");
            WriteLine("return new {0}(array);", Name);
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two polygones are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first polygon to compare.</param>");
            WriteLine("/// <param name=\"right\">The second polygon to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool Equals({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left == right;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            //public static polygon Intersect(polygon value1, polygon value2)
            //{
            //    int l = Functions.Max(value1.Left, value2.Left);
            //    int r = Functions.Min(value1.Right, value2.Right);
            //    int b = Functions.Max(value1.Bottom, value2.Bottom);
            //    int t = Functions.Min(value1.Top, value2.Top);
            //    int fr = Functions.Max(value1.Front, value2.Front);
            //    int bk = Functions.Min(value1.Back, value2.Back);
            //    if ((r >= l) && (t >= b) && (bk >= fr))
            //    {
            //        return new polygon(l, b, fr, r - l, t - b, bk - fr);
            //    }
            //    return Empty;

            //}
            //public static polygon Union(polygon value1, polygon value2)
            //{
            //    int l = Functions.Max(value1.Left, value2.Left);
            //    int r = Functions.Min(value1.Right, value2.Right);
            //    int b = Functions.Max(value1.Bottom, value2.Bottom);
            //    int t = Functions.Min(value1.Top, value2.Top);
            //    int fr = Functions.Max(value1.Front, value2.Front);
            //    int bk = Functions.Min(value1.Back, value2.Back);
            //    return new polygon(l, b, fr, r - l, t - b, bk - fr);
            //}

            //public static polygon Inflate(polygon polygon, Size3D amount)
            //{
            //    return new polygon(
            //        polygon.X - amount.Width,
            //        polygon.Y - amount.Height,
            //        polygon.Z - amount.Depth,
            //        polygon.Width + (amount.Width * 2),
            //        polygon.Height + (amount.Height * 2),
            //        polygon.Depth + (amount.Depth * 2));
            //}
            //public static polygon Offset(polygon polygon, Point3D amount)
            //{
            //    return new polygon(
            //        polygon.Location + amount,
            //        polygon.Size);
            //}

            //WriteLine("#region Contains");
            //WriteLine("public static bool Contains({0} polygon, {1} point)", Name, new Point(Type, 2));
            //WriteLine("{");
            //Indent();
            //WriteLine("return Vector.AbsoluteSquared(polygon.Center - point) <= polygon.Radius * polygon.Radius;");
            //Dedent();
            //WriteLine("}");
            //WriteLine("#endregion");
            //public bool Contains(polygon polygon)
            //{
            //    return (Left <= polygon.Left) && (Right >= polygon.Right) &&
            //        (Bottom <= polygon.Bottom) && (Top >= polygon.Top) &&
            //        (Front <= polygon.Front) && (Back >= polygon.Back);
            //}
            //public bool Intersects(polygon polygon)
            //{
            //    return (Right > polygon.Left) && (Left < polygon.Right) &&
            //        (Top < polygon.Bottom) && (Bottom < polygon.Top) &&
            //        (Back < polygon.Front) && (Front < polygon.Back);
            //}


            ///// <summary>
            ///// Returns a value that indicates whether two polygons are equal.
            ///// </summary>
            ///// <param name="left">The first polygon to compare.</param>
            ///// <param name="right">The second polygon to compare.</param>
            ///// <returns>true if the left and right are equal; otherwise, false.</returns>
            //public static bool Equals(polygon left, polygon right)
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
