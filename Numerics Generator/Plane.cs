using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Plane : Generator
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

        public Plane(NumberType type)
        {
            Type = type;
        }

        public string Name
        {
            get
            {
                return string.Format("Plane{0}", Type.Suffix);
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
            WriteLine("/// Represents a plane as a normal vector and distance from the origin.");
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
            WriteLine("/// Returns the yz plane.");
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} YZ = new {0}(new {1}(1, 0, 0), 0);", Name, new Vector(Type, 3));

            WriteLine("/// <summary>");
            WriteLine("/// Returns the xz plane.");
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} XZ = new {0}(new {1}(0, 1, 0), 0);", Name, new Vector(Type, 3));

            WriteLine("/// <summary>");
            WriteLine("/// Returns the xy plane.");
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} XY = new {0}(new {1}(0, 0, 1), 0);", Name, new Vector(Type, 3));
            
            WriteLine("#endregion");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            WriteLine("/// <summary>");
            WriteLine("/// The normal vector of the plane.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} Normal;", new Vector(Type, 3));
            WriteLine("/// <summary>");
            WriteLine("/// The distance of the plane along its normal from the origin.");
            WriteLine("/// </summary>");
            WriteLine("public readonly {0} D;", Type);

            WriteLine("#endregion");
        }

        void Properties()
        {
            WriteLine("#region Properties");
            #region Indexer
            WriteLine("/// <summary>");
            WriteLine("/// Returns the indexed component of this plane.");
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

            Indent();
            WriteLine("case 0:");
            Indent();
            WriteLine("return Normal.X;");
            Dedent();
            Dedent();

            Indent();
            WriteLine("case 1:");
            Indent();
            WriteLine("return Normal.Y;");
            Dedent();
            Dedent();

            Indent();
            WriteLine("case 2:");
            Indent();
            WriteLine("return Normal.Z;");
            Dedent();
            Dedent();

            Indent();
            WriteLine("case 3:");
            Indent();
            WriteLine("return D;");
            Dedent();
            Dedent();

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
            WriteLine("Normal.X, Normal.Y, Normal.Z, D");
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
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> class.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"a\">X component of the normal defining the plane.</param>");
            WriteLine("/// <param name=\"b\">Y component of the normal defining the plane.</param>");
            WriteLine("/// <param name=\"c\">Z component of the normal defining the plane.</param>");
            WriteLine("/// <param name=\"d\">Distance of the plane along its normal from the origin.</param>");
            WriteLine("public {0}({1} a, {1} b, {1} c, {1} d)", Name, Type);
            Indent("{");
            WriteLine("Normal = new {0}(a, b, c);", new Vector(Type, 3));
            WriteLine("D = d;");
            Dedent("}");
            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> class.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"normal\">The normal vector to the plane.</param>");
            WriteLine("/// <param name=\"d\">Distance of the plane along its normal from the origin.</param>");
            WriteLine("public {0}({1} normal, {2} d)", Name, new Vector(Type, 3), Type);
            Indent("{");
            WriteLine("Normal = normal;");
            WriteLine("D = d;");
            Dedent("}");
            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> class.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"point\">Any point that lies along the plane.</param>");
            WriteLine("/// <param name=\"normal\">The normal vector to the plane.</param>");
            WriteLine("public {0}({1} point, {2} normal)", Name, new Point(Type, 3), new Vector(Type, 3));
            Indent("{");
            WriteLine("Normal = normal;");
            WriteLine("D = -Point.Project(point, normal);");
            Dedent("}");
            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> class.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"point1\">First point of a triangle defining the plane.</param>");
            WriteLine("/// <param name=\"point2\">Second point of a triangle defining the plane.</param>");
            WriteLine("/// <param name=\"point3\">Third point of a triangle defining the plane.</param>");
            WriteLine("public {0}({1} point1, {1} point2, {1} point3)", Name, new Point(Type, 3));
            Indent("{");
            WriteLine("Normal = Vector.Normalize(Vector.Cross(point2 - point1, point3 - point1));");
            WriteLine("D = -Point.Project(point1, Normal);");
            Dedent("}");
            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> class.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">");
            WriteLine("/// A vector with the X, Y, and Z components defining the normal to the plane.");
            WriteLine("/// The W component defines the distance of the plane along its normal from the origin.");
            WriteLine("/// </param>");
            WriteLine("public {0}({1} value)", Name, new Vector(Type, 4));
            Indent("{");
            WriteLine("Normal = new {0}(value.X, value.Y, value.Z);", new Vector(Type, 3));
            WriteLine("D = value.W;");
            Dedent("}");
            WriteLine("#endregion");
        }

        void Operations()
        {
            var result = new Plane(Type.PositiveType);

            WriteLine("#region Operations");

            WriteLine("/// <summary>");
		    WriteLine("/// Scales the plane by the given scaling factor.");
		    WriteLine("/// </summary>");
		    WriteLine("/// <param name=\"plane\">The source plane.</param>");
		    WriteLine("/// <param name=\"scale\">The scaling factor.</param>");
		    WriteLine("/// <returns>The scaled plane.</returns>");
            WriteLine("public static {0} operator *({0} plane, {1} scale)", Name, Type);
            Indent("{");
            WriteLine("return Plane.Multiply(plane, scale);");
            Dedent("}");
		    WriteLine("/// <summary>");
		    WriteLine("/// Scales the plane by the given scaling factor.");
		    WriteLine("/// </summary>");
		    WriteLine("/// <param name=\"plane\">The source plane.</param>");
		    WriteLine("/// <param name=\"scale\">The scaling factor.</param>");
            WriteLine("/// <returns>The scaled plane.</returns>");
            WriteLine("public static {0} operator *({1} scale, {0} plane)", Name, Type);
            Indent("{");
            WriteLine("return Plane.Multiply(plane, scale);");
            Dedent("}");

            WriteLine("#endregion");
        }

        void Conversions()
        {
            WriteLine("#region Conversions");

            foreach (var type in Types.Where(t => t != Type))
            {
                string imex = type.IsImplicitlyConvertibleTo(Type) ? "implicit" : "explicit";

                Plane other = new Plane(type);

                var casts = string.Format("({0})value.Normal.X, ({0})value.Normal.Y, ({0})value.Normal.Z, ({0})value.D", Type);
                
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
            WriteLine("return Normal.GetHashCode() + D.GetHashCode();");
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
            WriteLine("/// vector have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The vector to compare.</param>");
            WriteLine("/// <returns>true if this vector and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two vectors are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first vector to compare.</param>");
            WriteLine("/// <param name=\"right\">The second vector to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.Normal == right.Normal && left.D == right.D;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two vectors are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first vector to compare.</param>");
            WriteLine("/// <param name=\"right\">The second vector to compare.</param>");
            WriteLine("/// <returns>true if the left and right are not equal; otherwise, false.</returns>");
            WriteLine("public static bool operator !=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.Normal != right.Normal || left.D != right.D;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void String()
        {
            WriteLine("#region ToString");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current vector to its equivalent string");
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
            WriteLine("/// Converts the value of the current vector to its equivalent string");
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
            WriteLine("/// Converts the value of the current vector to its equivalent string");
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

            var formatString = "{0}, {1}";
            var components = "Normal.ToString(format, provider), D.ToString(format, provider)";

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current vector to its equivalent string");
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
            WriteLine("/// Provides static methods for plane functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Plane");
            WriteLine("{");
            Indent();

            var result = new Plane(Type.PositiveType);

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to an <see cref=\"Ibasa.IO.BinaryWriter\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this Ibasa.IO.BinaryWriter writer, {0} plane)", Name);
            WriteLine("{");
            Indent();
            WriteLine("writer.Write(plane.Normal.X);");
            WriteLine("writer.Write(plane.Normal.Y);");
            WriteLine("writer.Write(plane.Normal.Z);");
            WriteLine("writer.Write(plane.D);");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> from an <see cref=\"Ibasa.IO.BinaryReader\">.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static {0} Read{0}(this Ibasa.IO.BinaryReader reader)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}(reader.Read{1}(), reader.Read{1}(), reader.Read{1}(), reader.Read{1}());", Name, Type.CLRName);
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Operations
            WriteLine("#region Operations");

            WriteLine("/// <summary>");
            WriteLine("/// Returns the product of a plane and scalar.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"plane\">The plane to multiply.</param>");
            WriteLine("/// <param name=\"scalar\">The scalar to multiply.</param>");
            WriteLine("/// <returns>The product of the left and right parameters.</returns>");
            WriteLine("public static {0} Multiply({1} plane, {2} scalar)", result, Name, result.Type);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}(plane.Normal.X * scalar, plane.Normal.Y * scalar, plane.Normal.Z * scalar, plane.D * scalar);", result);
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two planes are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first plane to compare.</param>");
            WriteLine("/// <param name=\"right\">The second plane to compare.</param>");
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

            WriteLine("/// <summary>");
            WriteLine("/// Calculates the dot product of the specified vector and plane.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"plane\">The source plane.</param>");
            WriteLine("/// <param name=\"vector\">The source vector.</param>");
            WriteLine("/// <returns>The dot product of the specified point and plane.</returns>");
            WriteLine("public static {0} Dot({1} plane, {2} vector)", Type, Name, new Vector(Type, 4));
            Indent("{");
            WriteLine("return (plane.Normal.X * vector.X) + (plane.Normal.Y * vector.Y) + (plane.Normal.Z * vector.Z) + (plane.D * vector.W);");
            Dedent("}");
            WriteLine("/// <summary>");
            WriteLine("/// Calculates the dot product of a specified vector and the normal of the plane plus the distance value of the plane.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"plane\">The source plane.</param>");
            WriteLine("/// <param name=\"point\">The source point.</param>");
            WriteLine("/// <returns>The dot product of a specified vector and the normal of the Plane plus the distance value of the plane.</returns>");
            WriteLine("public static {0} DotCoordinate({1} plane, {2} point)", Type, Name, new Point(Type, 3));
            Indent("{");
            WriteLine("return (plane.Normal.X * point.X) + (plane.Normal.Y * point.Y) + (plane.Normal.Z * point.Z) + plane.D;");
	        Dedent("}");
            WriteLine("/// <summary>");
            WriteLine("/// Calculates the dot product of the specified vector and the normal of the plane.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"plane\">The source plane.</param>");
            WriteLine("/// <param name=\"normal\">The source vector.</param>");
            WriteLine("/// <returns>The dot product of the specified vector and the normal of the plane.</returns>");
            WriteLine("public static {0} DotNormal({1} plane, {2} normal)", Type, Name, new Vector(Type, 3));
            Indent("{");
            WriteLine("return (plane.Normal.X * normal.X) + (plane.Normal.Y * normal.Y) + (plane.Normal.Z * normal.Z);");
            Dedent("}");

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
