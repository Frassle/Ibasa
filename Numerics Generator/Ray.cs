using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Ray : Generator
    {
        public static NumberType[] Types
        {
            get
            {
                return new NumberType[] {
                    NumberType.Double, NumberType.Float
                };
            }
        }

        public NumberType Type { get; private set; }

        public Ray(NumberType type)
        {
            Type = type;
        }

        public string Name
        {
            get
            {
                return string.Format("Ray{0}", Type.Suffix);
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
            WriteLine("/// Defines a ray in three dimensions, specified by a starting position and a direction.");
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
            Fields();
            Constructors();
            Equatable();
            String();
            Dedent();
            WriteLine("}");
        }

        void Fields()
        {
            WriteLine("#region Fields");

            
            WriteLine("/// <summary>");
            WriteLine("/// Specifies the location of the ray's origin.");
            WriteLine("/// </summary>");
            WriteLine("public readonly Point3{0} Position;", Type.Suffix);
            WriteLine("/// <summary>");
            WriteLine("/// A unit vector specifying the direction in which the ray is pointing.");
            WriteLine("/// </summary>");
            WriteLine("public readonly Vector3{0} Direction;", Type.Suffix);

            WriteLine("#endregion");
        }

        void Constructors()
        {
            WriteLine("#region Constructors");

            WriteLine("/// <summary>");
            WriteLine("/// Initializes a new instance of the <see cref=\"{0}\"/> structure.", Name);
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"position\">The location of the ray's origin.</param>");
            WriteLine("/// <param name=\"direction\">A unit vector specifying the direction in which the ray is pointing.</param>");
            WriteLine("public {0}(Point3{1} position, Vector3{1} direction)", Name, Type.Suffix);
            WriteLine("{");
            Indent();
            WriteLine("Position = position;");
            WriteLine("Direction = direction;");
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
            WriteLine("return Position.GetHashCode() + Direction.GetHashCode();");
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
            WriteLine("/// ray have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The ray to compare.</param>");
            WriteLine("/// <returns>true if this ray and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two rays are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first ray to compare.</param>");
            WriteLine("/// <param name=\"right\">The second ray to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.Position == right.Position & left.Direction == right.Direction;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two rays are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first ray to compare.</param>");
            WriteLine("/// <param name=\"right\">The second ray to compare.</param>");
            WriteLine("/// <returns>true if the left and right are not equal; otherwise, false.</returns>");
            WriteLine("public static bool operator !=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left.Position != right.Position | left.Direction != right.Direction;");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void String()
        {
            WriteLine("#region ToString");

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current ray to its equivalent string");
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
            WriteLine("/// Converts the value of the current ray to its equivalent string");
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
            WriteLine("/// Converts the value of the current ray to its equivalent string");
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
            WriteLine("/// Converts the value of the current ray to its equivalent string");
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
            WriteLine("return String.Format(\"Position:{0} Direction:{1}\", Position.ToString(), Direction.ToString());");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        void Functions()
        {
            WriteLine("/// <summary>");
            WriteLine("/// Provides static methods for ray functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Ray");
            WriteLine("{");
            Indent();

            #region Binary
            WriteLine("#region Binary");
            WriteLine("/// <summary>");
            WriteLine("/// Writes the given <see cref=\"{0}\"/> to a Ibasa.IO.BinaryWriter.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static void Write(this Ibasa.IO.BinaryWriter writer, {0} ray)", Name);
            WriteLine("{");
            Indent();
            WriteLine("Point.Write(writer, ray.Position);");
            WriteLine("Vector.Write(writer, ray.Direction);");
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Reads a <see cref=\"{0}\"/> to a Ibasa.IO.BinaryReader.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static {0} Read{0}(this Ibasa.IO.BinaryReader reader)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}(Point.ReadPoint3{1}(reader), Vector.ReadVector3{1}(reader));", Name, Type.Suffix);
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Equatable
            WriteLine("#region Equatable");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two rays are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first ray to compare.</param>");
            WriteLine("/// <param name=\"right\">The second ray to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool Equals({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left == right;");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Functions
            WriteLine("#region Functions");
            WriteLine("/// <summary>");
            WriteLine("/// Returns the point in the ray at position t.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"ray\">The ray to parametrize.</param>");
            WriteLine("/// <param name=\"t\">The paramater t.</param>");
            WriteLine("/// <returns>The point at t.</returns>");
            WriteLine("public static Point3{0} Parametrize({1} ray, {2} t)", Type.Suffix, Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return ray.Position + (t * ray.Direction);");
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Intersects
            WriteLine("#region Intersects");

            foreach(var type in Shapes.Types)
            {
                var box = new Box(type);

                WriteLine("/// <summary>");
                WriteLine("/// Determines whether a ray intersects the specified box.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"ray\">The ray which will be tested for intersection.</param>");
                WriteLine("/// <param name=\"box\">A box that will be tested for intersection.</param>");
                WriteLine("/// <returns>Distance at which the ray intersects the box or null if there is no intersection.</returns>");
                WriteLine("public static {0}? Intersects({1} ray, {2} box)", Type, Name, box);
                WriteLine("{");
                Indent();
                WriteLine("return null;");
                WriteLine("var invDir = Vector.Reciprocal(ray.Direction);");
                WriteLine("bool signX = invDir.X < 0;");
                WriteLine("bool signY = invDir.Y < 0;");
                WriteLine("bool signZ = invDir.Z < 0;");
                WriteLine("var min = signX ? box.Right : box.Left;");
                WriteLine("var max = signX ? box.Left : box.Right;");
                WriteLine("var txmin = (min - ray.Position.X) * invDir.X;");
                WriteLine("var txmax = (max - ray.Position.X) * invDir.X;");
                
                WriteLine("min = signY ? box.Top : box.Bottom;");
                WriteLine("max = signY ? box.Bottom : box.Top;");
                WriteLine("var tymin = (min - ray.Position.Y) * invDir.Y;");
                WriteLine("var tymax = (max - ray.Position.Y) * invDir.Y;");

                WriteLine("if ((txmin > tymax) || (tymin > txmax)) { return null; }");
                WriteLine("if (tymin > txmin) { txmin = tymin; }");
                WriteLine("if (tymax < txmax) { txmax = tymax; }");
                
                WriteLine("min = signZ ? box.Back : box.Front;");
                WriteLine("max = signZ ? box.Front : box.Back;");
                WriteLine("var tzmin = (min - ray.Position.Z) * invDir.Z;");
                WriteLine("var tzmax = (max - ray.Position.Z) * invDir.Z;");

                WriteLine("if ((txmin > tzmax) || (tzmin > txmax)) { return null; }");
                WriteLine("if (tzmin > txmin) { txmin = tzmin; }");
                WriteLine("if (tzmax < txmax) { txmax = tzmax; }");

                WriteLine("if (txmin < double.PositiveInfinity && txmax >= 0)");
                WriteLine("{");
                Indent();
                WriteLine("return ({0})txmin;", Type);
                Dedent();
                WriteLine("}");
                WriteLine("else");                
                WriteLine("{");
                Indent();
                WriteLine("return null;");
                Dedent();
                WriteLine("}");
                Dedent();
                WriteLine("}");

                var sphere = new Sphere(type);

                WriteLine("/// <summary>");
                WriteLine("/// Determines whether a ray intersects the specified sphere.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"ray\">The ray which will be tested for intersection.</param>");
                WriteLine("/// <param name=\"sphere\">A sphere that will be tested for intersection.</param>");
                WriteLine("/// <returns>Distance at which the ray intersects the sphere or null if there is no intersection.</returns>");
                WriteLine("public static {0}? Intersects({1} ray, {2} sphere)", Type, Name, sphere);
                WriteLine("{");
                Indent();
                WriteLine("var distance = sphere.Center - ray.Position;");
                WriteLine("var pyth = Vector.AbsoluteSquared(distance);");
    		    WriteLine("var rr = sphere.Radius * sphere.Radius;");
    		    WriteLine("if( pyth <= rr ) { return 0; }");
                WriteLine("double dot = Vector.Dot(distance, ray.Direction);");
    		    WriteLine("if( dot < 0 ) { return null; }");
    		    WriteLine("var temp = pyth - (dot * dot);");
    		    WriteLine("if( temp > rr ) { return null; }");
                WriteLine("return ({0})(dot - Functions.Sqrt(rr-temp));", Type);
                Dedent();
                WriteLine("}");
            }
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
