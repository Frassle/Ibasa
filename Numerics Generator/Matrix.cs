﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Matrix : Generator
    {
        public static int[] Sizes { get { return new int[] { 2, 3, 4}; } }
        public static NumberType[] Types
        {
            get
            {
                return new NumberType[] {
                    NumberType.Double, NumberType.Float
                };
            }
        }

        private NumberType Type;
        private int Rows;
        private int Columns;
        Element[] Elements;

        public Matrix(NumberType type, int rows, int columns)
        {
            Type = type;
            Rows = rows;
            Columns = columns;
            Elements = Element.Elements(rows, columns);
        }

        public string Name { get { return string.Format("Matrix{0}x{1}{2}", Rows, Columns, Type.Suffix); } }

        public void Generate()
        {
            Namespace();
        }

        void Namespace()
        {
            WriteLine("using System;");
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
            WriteLine("/// Represents a {0} by {1} matrix of {2}s.", Rows, Columns, Type);
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
            WriteLine("/// Returns a new <see cref=\"{0}\"/> with all of its elements equal to zero.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} Zero = new {0}(0);", Name);

            WriteLine("/// <summary>");
            WriteLine("/// Returns a new <see cref=\"{0}\"/> with all of its elements equal to one.", Name);
            WriteLine("/// </summary>");
            WriteLine("public static readonly {0} One = new {0}(1);", Name);

            if (Rows == Columns)
            {
                var identity = string.Join(", ",
                    Elements.Select(element => element.Row == element.Column ? 1 : 0));

                WriteLine("/// <summary>");
                WriteLine("/// Returns the identity <see cref=\"{0}\"/>.", Name);
                WriteLine("/// </summary>");
                WriteLine("public static readonly {0} Identity = new {0}({1});", Name, identity);
            }

            WriteLine("#endregion");
        }

        private void Fields()
        {
            WriteLine("#region Fields");
            for(int row = 1; row <= Rows; ++row)
            {
                for(int column = 1; column <= Columns; ++column)
                {
                    WriteLine("/// <summary>");
                    WriteLine("/// Gets the element of the matrix that exists in the {0} row and {1} column.", row.OrderName(), column.OrderName());
                    WriteLine("/// </summary>");
                    WriteLine("public readonly {0} M{1}{2};", Type, row, column);
                }
            }
            WriteLine("#endregion");
        }

        private void RowBounds()
        {
            WriteLine("if (row < 0 || row > {0})", Rows - 1);
            Indent();
            WriteLine("throw new ArgumentOutOfRangeException(\"row\", \"Rows for {0} run from 0 to {1}, inclusive.\");", Name, Rows - 1);
            Dedent();
        }
        private void ColumnBounds()
        {
            WriteLine("if (column < 0 || column > {0})", Columns - 1);
            Indent();
            WriteLine("throw new ArgumentOutOfRangeException(\"column\", \"Columns for {0} run from 0 to {1}, inclusive.\");", Name, Columns - 1);
            Dedent();
        }

        private void Properties()
        {
            WriteLine("#region Properties");

            WriteLine("public {0} this[int row, int column]", Type);
            WriteLine("{");
            Indent();
            WriteLine("get");
            WriteLine("{");
            Indent();
            RowBounds();
            ColumnBounds();
            WriteLine("int index = column + row * {0};", Columns);
            WriteLine("return this[index];");
            Dedent();
            WriteLine("}");
            Dedent();
            WriteLine("}");

            WriteLine("public {0} this[int index]", Type);
            WriteLine("{");
            Indent();
            WriteLine("get");
            WriteLine("{");
            Indent();
            WriteLine("if (index < 0 || index > {0})", (Rows * Columns) - 1);
            Indent();
            WriteLine("throw new ArgumentOutOfRangeException(\"index\", \"Indices for {0} run from 0 to {1}, inclusive.\");", Name, (Rows * Columns) - 1);
            Dedent();
            WriteLine("switch (index)");
            WriteLine("{");
            Indent();
            foreach(var element in Elements)
            {
                WriteLine("case {0}: return {1};", element.Index, element);
            }
            Dedent();
            WriteLine("}");
            WriteLine("return 0;");
            Dedent();
            WriteLine("}");
            Dedent();
            WriteLine("}");

            var rowVector = new Vector(Type, Columns);

            WriteLine("public {0} GetRow(int row)", rowVector);
            WriteLine("{");
            Indent();
            RowBounds();
            WriteLine("switch (row)");
            WriteLine("{");
            Indent();
            for(int row = 0; row < Rows; ++row)
            {
                WriteLine("case {0}:", row);
                Indent();
                WriteLine("return new {0}({1});", rowVector,
                    string.Join(", ", Enumerable.Range(0, Columns).Select(column => string.Format("M{0}{1}", row + 1, column + 1))));
                Dedent();
            }
            Dedent();
            WriteLine("}");
            WriteLine("return {0}.Zero;", rowVector);
            Dedent();
            WriteLine("}");

            var columnVector = new Vector(Type, Rows);

            WriteLine("public {0} GetColumn(int column)", columnVector);
            WriteLine("{");
            Indent();
            ColumnBounds();
            WriteLine("switch (column)");
            WriteLine("{");
            Indent();
            for (int column = 0; column < Columns; ++column)
            {
                WriteLine("case {0}:", column);
                Indent();
                WriteLine("return new {0}({1});", columnVector,
                    string.Join(", ", Enumerable.Range(0, Rows).Select(row => string.Format("M{0}{1}", row + 1, column + 1))));
                Dedent();
            }
            Dedent();
            WriteLine("}");
            WriteLine("return {0}.Zero;", columnVector);
            Dedent();
            WriteLine("}");

            WriteLine("public {0}[] ToArray()", Type);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}[]", Type);
            WriteLine("{");
            Indent();
            WriteLine(string.Join(", ", Elements.Select(element => string.Format("M{0}{1}", element.Row + 1, element.Column + 1))));
            Dedent();
            WriteLine("};");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        private void Constructors()
        {
            WriteLine("#region Constructors");

            WriteLine("public {0}({1} value)", Name, Type);
            WriteLine("{");
            Indent();
            for (int row = 1; row <= Rows; ++row)
            {
                for (int column = 1; column <= Columns; ++column)
                {
                    WriteLine("M{0}{1} = value;", row, column);
                }
            }
            Dedent();
            WriteLine("}");

            WriteLine("public {0}({1})", Name,
                string.Join(", ", Elements.Select(element => string.Format("{0} m{1}{2}", Type, element.Row + 1, element.Column + 1))));
            WriteLine("{");
            Indent();
            for (int row = 1; row <= Rows; ++row)
            {
                for (int column = 1; column <= Columns; ++column)
                {
                    WriteLine("M{0}{1} = m{0}{1};", row, column);
                }
            }
            Dedent();
            WriteLine("}");

            var rowVector = new Vector(Type, Columns);

            WriteLine("public {0}({1})", Name,
                string.Join(", ", Enumerable.Range(0, Rows).Select(row => string.Format("{0} row{1}", rowVector, row + 1))));
            WriteLine("{");
            Indent();
            for (int row = 1; row <= Rows; ++row)
            {
                for (int column = 1; column <= Columns; ++column)
                {
                    WriteLine("M{0}{1} = row{0}.{2};", row, column, rowVector.Components[column - 1]);
                }
            }
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        private void Operations()
        {
            WriteLine("#region Operations");

            WriteLine("public static {0} operator +({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return value;");
            Dedent();
            WriteLine("}");

            WriteLine("public static {0} operator -({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return Matrix.Negate(value);");
            Dedent();
            WriteLine("}");

            WriteLine("public static {0} operator +({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return Matrix.Add(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("public static {0} operator -({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return Matrix.Subtract(left, right);");
            Dedent();
            WriteLine("}");

            WriteLine("public static {0} operator *({0} matrix, {1} scalar)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return Matrix.Multiply(matrix, scalar);");
            Dedent();
            WriteLine("}");

            WriteLine("public static {0} operator *({1} scalar, {0} matrix)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return Matrix.Multiply(matrix, scalar);");
            Dedent();
            WriteLine("}");

            foreach (var size in Matrix.Sizes)
            {
                var other = new Matrix(Type, Columns, size);
                var result = new Matrix(Type, Rows, size);

                WriteLine("public static {2} operator *({0} left, {1} right)", Name, other, result);
                WriteLine("{");
                Indent();
                WriteLine("return Matrix.Multiply(left, right);");
                Dedent();
                WriteLine("}");
            }

            WriteLine("public static {0} operator /({0} matrix, {1} scalar)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return Matrix.Divide(matrix, scalar);");
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        private void Conversions()
        {
            WriteLine("#region Conversions");
            foreach (var type in Types.Where(t => t != Type))
            {
                string imex = type.IsImplicitlyConvertibleTo(Type) ? "implicit" : "explicit";

                Matrix other = new Matrix(type, Rows, Columns);

                var casts = string.Join(", ",
                    Elements.Select(component => string.Format("({0})value.{1}", Type, component)));

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

        private void Equatable()
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
                string.Join(" + ", Elements.Select(element => string.Format("{0}.GetHashCode()", element))));
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
            WriteLine("/// matrix have the same value.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"other\">The matrix to compare.</param>");
            WriteLine("/// <returns>true if this matrix and value have the same value; otherwise, false.</returns>");
            WriteLine("public bool Equals({0} other)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return this == other;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two matrices are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first matrix to compare.</param>");
            WriteLine("/// <param name=\"right\">The second matrix to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool Equals({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return left == right;");
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two matrices are equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first matrix to compare.</param>");
            WriteLine("/// <param name=\"right\">The second matrix to compare.</param>");
            WriteLine("/// <returns>true if the left and right are equal; otherwise, false.</returns>");
            WriteLine("public static bool operator ==({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" & ", Elements.Select(element => string.Format("left.{0} == right.{0}", element))));
            Dedent();
            WriteLine("}");

            WriteLine("/// <summary>");
            WriteLine("/// Returns a value that indicates whether two matrices are not equal.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first matrix to compare.</param>");
            WriteLine("/// <param name=\"right\">The second matrix to compare.</param>");
            WriteLine("/// <returns>true if the left and right are not equal; otherwise, false.</returns>");
            WriteLine("public static bool operator !=({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" | ", Elements.Select(element => string.Format("left.{0} != right.{0}", element))));
            Dedent();
            WriteLine("}");

            WriteLine("#endregion");
        }

        private void String()
        {
            WriteLine("#region ToString");
            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current matrix to its equivalent string");
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
            WriteLine("/// Converts the value of the current matrix to its equivalent string");
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
            WriteLine("/// Converts the value of the current matrix to its equivalent string");
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

            var formatString = new StringBuilder();
            formatString.Append("[");
            for (int row = 0; row < Rows; ++row)
            {
                formatString.AppendFormat("({0})", string.Join(", ", Enumerable.Range(0, Columns).Select(
                    column => "{" + (column + row * Columns) + "}")));
            }
            formatString.Append("]");

            var components = string.Join(", ",
                Elements.Select(component => component + ".ToString(format, provider)"));

            WriteLine("/// <summary>");
            WriteLine("/// Converts the value of the current matrix to its equivalent string");
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

        private void Submatrix(string matrix, int row, int column)
        {
            if (Rows == 2 && Columns == 2)
            {
                if (row == 0 && column == 0) { Write(matrix + ".M22"); }
                if (row == 0 && column == 1) { Write(matrix + ".M21"); }
                if (row == 1 && column == 0) { Write(matrix + ".M12"); }
                if (row == 1 && column == 1) { Write(matrix + ".M11"); }
            }
            else if (Rows == 2 || Columns == 2)
            {
                var minor = new Vector(Type, (Rows == 2 ? Columns : Rows) - 1);
                
                Write("new {0}({1})", minor,
                            string.Join(", ", Elements.Where(element => element.Row != row && element.Column != column).Select(
                                e => string.Format("{0}.{1}", matrix, e))));
            }
            else
            {
                var minor = new Matrix(Type, Rows - 1, Columns - 1);
                
                Write("new {0}({1})", minor,
                    string.Join(", ", Elements.Where(element => element.Row != row && element.Column != column).Select(
                        e => string.Format("{0}.{1}", matrix, e))));
            }
        }

        private void Functions()
        {
            WriteLine("/// <summary>");
            WriteLine("/// Provides static methods for matrix functions.");
            WriteLine("/// </summary>");
            WriteLine("public static partial class Matrix");
            WriteLine("{");
            Indent();

            #region Operations
            WriteLine("#region Operations");
            WriteLine("public static {0} Negate({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, string.Join(", ", Elements.Select(element => string.Format("-value.{0}", element))));
            Dedent();
            WriteLine("}");

            WriteLine("public static {0} Add({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, string.Join(", ", Elements.Select(element => string.Format("left.{0} + right.{0}", element))));
            Dedent();
            WriteLine("}");

            WriteLine("public static {0} Subtract({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, string.Join(", ", Elements.Select(element => string.Format("left.{0} - right.{0}", element))));
            Dedent();
            WriteLine("}");

            foreach (var size in Matrix.Sizes)
            {
                var other = new Matrix(Type, Columns, size);
                var result = new Matrix(Type, Rows, size);

                WriteLine("public static {2} Multiply({0} left, {1} right)", Name, other, result);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", result, string.Join(", ",
                    result.Elements.Select(element => string.Join(" + ", Enumerable.Range(0, Columns).Select(k =>
                        string.Format("left.M{0}{1} * right.M{2}{3}", element.Row + 1, k + 1, k + 1, element.Column + 1))))));
                Dedent();
                WriteLine("}");
            }

            WriteLine("public static {0} Multiply({0} matrix, {1} scalar)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, string.Join(", ", Elements.Select(element => string.Format("matrix.{0} * scalar", element))));
            Dedent();
            WriteLine("}");

            WriteLine("public static {0} Divide({0} matrix, {1} scalar)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name, string.Join(", ", Elements.Select(element => string.Format("matrix.{0} / scalar", element))));
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Test
            WriteLine("#region Test");
            WriteLine("/// <summary>");
            WriteLine("/// Determines whether all elements of a matrix are non-zero.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A matrix.</param>");
            WriteLine("/// <returns>true if all elements are non-zero; false otherwise.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static bool All({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" && ", Elements.Select(element => string.Format("value.{0} != 0", element))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Determines whether all elements of a matrix satisfy a condition.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A matrix.</param>");
            WriteLine("/// <param name=\"predicate\">A function to test each element for a condition.</param>");
            WriteLine("/// <returns>true if every element of the matrix passes the test in the specified");
            WriteLine("/// predicate; otherwise, false.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static bool All({0} value, Predicate<{1}> predicate)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" && ", Elements.Select(element => string.Format("predicate(value.{0})", element))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Determines whether any element of a matrix is non-zero.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A vector.</param>");
            WriteLine("/// <returns>true if any elements are non-zero; false otherwise.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static bool Any({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" || ", Elements.Select(element => string.Format("value.{0} != 0", element))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Determines whether any elements of a matrix satisfy a condition.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A vector.</param>");
            WriteLine("/// <param name=\"predicate\">A function to test each element for a condition.</param>");
            WriteLine("/// <returns>true if any element of the matrix passes the test in the specified");
            WriteLine("/// predicate; otherwise, false.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static bool Any({0} value, Predicate<{1}> predicate)", Name, Type);
            WriteLine("{");
            Indent();
            WriteLine("return {0};",
                string.Join(" || ", Elements.Select(element => string.Format("predicate(value.{0})", element))));
            Dedent();
            WriteLine("}");
            WriteLine("#endregion");
            #endregion

            #region Per element
            WriteLine("#region Per element");

            WriteLine("#region Transform");
            foreach (var type in Types)
            {
                var transform = new Matrix(type, Rows, Columns);

                WriteLine("/// <summary>");
                WriteLine("/// Transforms the elements of a matrix and returns the result.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">The matrix to transform.</param>");
                WriteLine("/// <param name=\"transformer\">A transform function to apply to each element.</param>");
                WriteLine("/// <returns>The result of transforming each element of value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Transform({1} value, Func<{2}, {3}> transformer)", transform, Name, Type, transform.Type);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", transform,
                    string.Join(", ", Elements.Select(element => string.Format("transformer(value.{0})", element))));
                Dedent();
                WriteLine("}");
            }
            WriteLine("#endregion");

            WriteLine("/// <summary>");
            WriteLine("/// Multiplys the elements of two matrices and returns the result.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"left\">The first matrix to modulate.</param>");
            WriteLine("/// <param name=\"right\">The second matrix to modulate.</param>");
            WriteLine("/// <returns>The result of multiplying each element of left by the matching element in right.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Modulate({0} left, {0} right)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Elements.Select(element => string.Format("left.{0} * right.{0}", element))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Returns the absolute value (per element).");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A matrix.</param>");
            WriteLine("/// <returns>The absolute value (per element) of value.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Abs({0} value)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Elements.Select(element => string.Format("Functions.Abs(value.{0})", element))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a matrix that contains the lowest value from each pair of elements.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value1\">The first matrix.</param>");
            WriteLine("/// <param name=\"value2\">The second matrix.</param>");
            WriteLine("/// <returns>The lowest of each element in left and the matching element in right.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Min({0} value1, {0} value2)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Elements.Select(element => string.Format("Functions.Min(value1.{0}, value2.{0})", element))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Returns a matrix that contains the highest value from each pair of elements.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value1\">The first matrix.</param>");
            WriteLine("/// <param name=\"value2\">The second matrix.</param>");
            WriteLine("/// <returns>The highest of each element in left and the matching element in right.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Max({0} value1, {0} value2)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Elements.Select(element => string.Format("Functions.Max(value1.{0}, value2.{0})", element))));
            Dedent();
            WriteLine("}");
            WriteLine("/// <summary>");
            WriteLine("/// Constrains each element to a given range.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"value\">A matrix to constrain.</param>");
            WriteLine("/// <param name=\"min\">The minimum values for each element.</param>");
            WriteLine("/// <param name=\"max\">The maximum values for each element.</param>");
            WriteLine("/// <returns>A matrix with each element constrained to the given range.</returns>");
            if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
            WriteLine("public static {0} Clamp({0} value, {0} min, {0} max)", Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", Name,
                string.Join(", ", Elements.Select(element => string.Format("Functions.Clamp(value.{0}, min.{0}, max.{0})", element))));
            Dedent();
            WriteLine("}");
            if (Type.IsReal)
            {
                WriteLine("/// <summary>");
                WriteLine("/// Constrains each element to the range 0 to 1.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A matrix to saturate.</param>");
                WriteLine("/// <returns>A matrix with each element constrained to the range 0 to 1.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Saturate({0} value)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name,
                    string.Join(", ", Elements.Select(element => string.Format("Functions.Saturate(value.{0})", element))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a matrix where each element is the smallest integral value that");
                WriteLine("/// is greater than or equal to the specified element.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A matrix.</param>");
                WriteLine("/// <returns>The ceiling of value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Ceiling({0} value)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name,
                    string.Join(", ", Elements.Select(element => string.Format("Functions.Ceiling(value.{0})", element))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a matrix where each element is the largest integral value that");
                WriteLine("/// is less than or equal to the specified element.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A matrix.</param>");
                WriteLine("/// <returns>The floor of value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Floor({0} value)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name,
                    string.Join(", ", Elements.Select(element => string.Format("Functions.Floor(value.{0})", element))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a matrix where each element is the integral part of the specified element.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A matrix.</param>");
                WriteLine("/// <returns>The integral of value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Truncate({0} value)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name,
                    string.Join(", ", Elements.Select(element => string.Format("Functions.Truncate(value.{0})", element))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a matrix where each element is the fractional part of the specified element.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A matrix.</param>");
                WriteLine("/// <returns>The fractional of value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Fractional({0} value)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name,
                    string.Join(", ", Elements.Select(element => string.Format("Functions.Fractional(value.{0})", element))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a matrix where each element is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A matrix.</param>");
                WriteLine("/// <returns>The result of rounding value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Round({0} value)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name,
                    string.Join(", ", Elements.Select(element => string.Format("Functions.Round(value.{0})", element))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a matrix where each element is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A matrix.</param>");
                WriteLine("/// <param name=\"digits\">The number of fractional digits in the return value.</param>");
                WriteLine("/// <returns>The result of rounding value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Round({0} value, int digits)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name,
                    string.Join(", ", Elements.Select(element => string.Format("Functions.Round(value.{0}, digits)", element))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a matrix where each element is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A matrix.</param>");
                WriteLine("/// <param name=\"mode\">Specification for how to round value if it is midway between two other numbers.</param>");
                WriteLine("/// <returns>The result of rounding value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Round({0} value, MidpointRounding mode)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name,
                    string.Join(", ", Elements.Select(element => string.Format("Functions.Round(value.{0}, mode)", element))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Returns a matrix where each element is rounded to the nearest integral value.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A matrix.</param>");
                WriteLine("/// <param name=\"digits\">The number of fractional digits in the return value.</param>");
                WriteLine("/// <param name=\"mode\">Specification for how to round value if it is midway between two other numbers.</param>");
                WriteLine("/// <returns>The result of rounding value.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Round({0} value, int digits, MidpointRounding mode)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name,
                    string.Join(", ", Elements.Select(element => string.Format("Functions.Round(value.{0}, digits, mode)", element))));
                Dedent();
                WriteLine("}");
                WriteLine("/// <summary>");
                WriteLine("/// Calculates the reciprocal of each element in the matrix.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"value\">A matrix.</param>");
                WriteLine("/// <returns>A matrix with the reciprocal of each of values elements.</returns>");
                if (!Type.IsCLSCompliant) { WriteLine("[CLSCompliant(false)]"); }
                WriteLine("public static {0} Reciprocal({0} value)", Name);
                WriteLine("{");
                Indent();
                WriteLine("return new {0}({1});", Name,
                    string.Join(", ", Elements.Select(element => string.Format("1 / value.{0}", element))));
                Dedent();
                WriteLine("}");
            }
            WriteLine("#endregion");
            #endregion 
            
            #region Submatrix
            WriteLine("#region Submatrix");
            if (Rows == 2 && Columns == 2)
            {
                WriteLine("/// <summary>");
                WriteLine("/// Returns the specified submatrix of the given matrix.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"matrix\">The matrix whose submatrix is to returned.</param>");
                WriteLine("/// <param name=\"row\">The row to be removed.</param>");
                WriteLine("/// <param name=\"column\">The column to be removed.</param>");
                WriteLine("public static {0} Submatrix({1} matrix, int row, int column)", Type, Name);
                WriteLine("{");
                Indent();
                RowBounds();
                ColumnBounds();
                Write("if(row == 0 && column == 0) { return "); Submatrix("matrix", 0, 0); WriteLine("; }");
                Write("else if(row == 0 && column == 1) { return "); Submatrix("matrix", 0, 1); WriteLine("; }");
                Write("else if(row == 1 && column == 0) { return "); Submatrix("matrix", 1, 0); WriteLine("; }");
                Write("else { return "); Submatrix("matrix", 1, 1); WriteLine("; }");
                Dedent();
                WriteLine("}");
            }
            else if (Rows == 2 || Columns == 2)
            {
                var minor = new Vector(Type, (Rows == 2 ? Columns : Rows) - 1);

                WriteLine("/// <summary>");
                WriteLine("/// Returns the specified submatrix of the given matrix.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"matrix\">The matrix whose submatrix is to returned.</param>");
                WriteLine("/// <param name=\"row\">The row to be removed.</param>");
                WriteLine("/// <param name=\"column\">The column to be removed.</param>");
                WriteLine("public static {0} Submatrix({1} matrix, int row, int column)", minor, Name);
                WriteLine("{");
                Indent();
                RowBounds();
                ColumnBounds();
                for (int i = 0; i < Rows; ++i)
                {
                    for (int j = 0; j < Columns; ++j)
                    {
                        if (i == 0 && j == 0)
                        {
                            WriteLine("if (row == {0} && column == {1})", i, j);
                        }
                        else if (i == Rows - 1 && j == Columns - 1)
                        {
                            WriteLine("else");
                        }
                        else
                        {
                            WriteLine("else if (row == {0} && column == {1})", i, j);
                        }
                        WriteLine("{");
                        Indent();
                        Write("return "); Submatrix("matrix", i, j); WriteLine(";");
                        Dedent();
                        WriteLine("}");
                    }
                }
                Dedent();
                WriteLine("}");
            }
            else
            {
                var minor = new Matrix(Type, Rows - 1, Columns - 1);

                WriteLine("/// <summary>");
                WriteLine("/// Returns the specified submatrix of the given matrix.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"matrix\">The matrix whose submatrix is to returned.</param>");
                WriteLine("/// <param name=\"row\">The row to be removed.</param>");
                WriteLine("/// <param name=\"column\">The column to be removed.</param>");
                WriteLine("public static {0} Submatrix({1} matrix, int row, int column)", minor, Name);
                WriteLine("{");
                Indent();
                RowBounds();
                ColumnBounds();
                for (int i = 0; i < Rows; ++i)
                {
                    for (int j = 0; j < Columns; ++j)
                    {
                        if (i == 0 && j == 0)
                        {
                            WriteLine("if (row == {0} && column == {1})", i, j);
                        }
                        else if (i == Rows - 1 && j == Columns - 1)
                        {
                            WriteLine("else");
                        }
                        else 
                        {
                            WriteLine("else if (row == {0} && column == {1})", i, j);
                        }
                        WriteLine("{");
                        Indent();
                        WriteLine("return new {0}({1});", minor,
                            string.Join(", ", Elements.Where(element => element.Row != i && element.Column != j).Select(
                                e => string.Format("matrix.{0}", e))));
                        Dedent();
                        WriteLine("}");
                    }
                }
                Dedent();
                WriteLine("}");
            }
            WriteLine("#endregion");
            #endregion

            #region Invert, Determinant
            if (Rows == Columns)
            {
                WriteLine("#region Invert, Determinant");

                WriteLine("/// <summary>");
                WriteLine("/// Calculates the inverse of the specified matrix.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"matrix\">The matrix whose inverse is to be calculated.</param>");
                WriteLine("/// <param name=\"determinant\">When the method completes, contains the determinant of the matrix.</param>");
                WriteLine("public static {0} Invert({0} matrix, out {1} determinant)", Name, Type);
                WriteLine("{");
                Indent();
                if (Rows == 2)
                {
                    WriteLine("determinant = matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21;");
                    WriteLine("return new {0}(determinant * matrix.M22, determinant * -matrix.M12, determinant * -matrix.M21, determinant * matrix.M11);", Name);                    
                }
                else
                {
                    for (int row = 0; row < Rows; ++row)
                    {
                        for (int column = 0; column < Columns; ++column)
                        {
                            Write("var cofactor{1}{0} = {2}", row, column, Math.Pow(-1, column + row) == -1 ? "-" : "");
                            Write("Determinant(", row, column); Submatrix("matrix", row, column); WriteLine(");");
                        }
                    }

                    WriteLine("determinant = {0};", string.Join(" + ",
                        Enumerable.Range(0, Rows).Select(row => string.Format("matrix.M{0}1 * cofactor{1}0", row + 1, row))));

                    WriteLine("return new {0}({1}) / determinant;", Name,
                        string.Join(", ", Elements.Select(element => string.Format("cofactor{0}{1}", element.Column, element.Row))));
                }
                Dedent();
                WriteLine("}");

                WriteLine("/// <summary>");
                WriteLine("/// Calculates the inverse of the specified matrix.");
                WriteLine("/// </summary>");
                WriteLine("/// <param name=\"matrix\">The matrix whose inverse is to be calculated.</param>");
                WriteLine("/// <param name=\"determinant\">When the method completes, contains the determinant of the matrix.</param>");
                WriteLine("public static {0} Invert({0} matrix)", Name);
                WriteLine("{");
                Indent();
                WriteLine("{0} determinant;", Type);
                WriteLine("return Invert(matrix, out determinant);");
                Dedent();
                WriteLine("}");

                WriteLine("/// <summary>");
                WriteLine("/// Calculates the determinant of the matrix.");
                WriteLine("/// </summary>");
                WriteLine("/// <returns>The determinant of the matrix.</returns>");
                WriteLine("public static {0} Determinant({1} matrix)", Type, Name);
                WriteLine("{");
                Indent();
                if (Rows == 2)
                {
                    WriteLine("return matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21;");
                }
                else
                {
                    for (int row = 0; row < Rows; ++row)
                    {
                        Write("var minor{0} = Determinant(", row); Submatrix("matrix", row, 0); WriteLine(");");
                    }
                    for (int row = 0; row < Rows; ++row)
                    {
                        WriteLine("var cofactor{0} = {1}minor{0};", row, Math.Pow(-1, 2+row) == -1 ? "-" : "");
                    }
                    
                    WriteLine("return {0};", string.Join(" + ", 
                        Enumerable.Range(0, Rows).Select(row => string.Format("matrix.M{0}1 * cofactor{1}", row + 1, row))));
                }
                Dedent();
                WriteLine("}");

                WriteLine("#endregion");
            }
            #endregion

            #region Transpose
            Matrix transpose = new Matrix(Type, Columns, Rows);
            WriteLine("#region Transpose");
            WriteLine("/// <summary>");
            WriteLine("/// Calculates the transpose of the specified matrix.");
            WriteLine("/// </summary>");
            WriteLine("/// <param name=\"matrix\">The matrix whose transpose is to be calculated.</param>");
            WriteLine("/// <returns>The transpose of the specified matrix.</returns>");
            WriteLine("public static {0} Transpose({1} matrix)", transpose, Name);
            WriteLine("{");
            Indent();
            WriteLine("return new {0}({1});", transpose, string.Join(", ",
                transpose.Elements.Select(element => string.Format("matrix.M{0}{1}", element.Column + 1, element.Row + 1))));
            Dedent();
            WriteLine("}");
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
