using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            string root = System.IO.Path.Combine("..", "..", "..", "Numerics");

            //foreach (int dimension in Exterior.Sizes)
            //{
            //    foreach (var type in Exterior.Types)
            //    {
            //        foreach (int grade in Enumerable.Range(1, Grade.Grades(dimension) - 2))
            //        {
            //            var exterior = new Exterior(type, dimension, new Grade(grade, dimension));
            //            exterior.Generate();

            //            var path = System.IO.Path.Combine(root, "Exterior", exterior.Name + ".cs");
            //            System.IO.File.WriteAllText(path, exterior.Text);
            //            Console.WriteLine("Done - " + exterior.Name);
            //        }
            //    }
            //}

            //foreach (int dimension in Exterior.Sizes)
            //{
            //    foreach (var type in Exterior.Types)
            //    {
            //        var antiscalar = new Antiscalar(type, dimension);
            //        antiscalar.Generate();

            //        var path = System.IO.Path.Combine(root, "Exterior", antiscalar.Name + ".cs");
            //        System.IO.File.WriteAllText(path, antiscalar.Text);
            //        Console.WriteLine("Done - " + antiscalar.Name);
            //    }
            //}
            foreach (var type in Color.Types)
            {
                var color = new Color(type);
                color.Generate();

                var path = System.IO.Path.Combine(root, color.Name + ".cs");
                System.IO.File.WriteAllText(path, color.Text);
                Console.WriteLine("Done - " + color.Name);
            }

            foreach (int dimension in Vector.Sizes)
            {
                foreach (var type in Vector.Types)
                {
                    var vector = new Vector(type, dimension);
                    vector.Generate();

                    var path = System.IO.Path.Combine(root, "Geometry", "Vectors", vector.Name + ".cs");
                    System.IO.File.WriteAllText(path, vector.Text);
                    Console.WriteLine("Done - " + vector.Name);
                }
            }

            foreach (int rows in Matrix.Sizes)
            {
                foreach (int columns in Matrix.Sizes)
                {
                    foreach (var type in Matrix.Types)
                    {
                        var matrix = new Matrix(type, rows, columns);
                        matrix.Generate();
                        var path = System.IO.Path.Combine(root, "Geometry", "Matrices", matrix.Name + ".cs");
                        System.IO.File.WriteAllText(path, matrix.Text);
                        Console.WriteLine("Done - " + matrix.Name);
                    }
                }
            }

            foreach (var type in Shapes.Types)
            {
                var rpath = System.IO.Path.Combine(root, "Geometry");
                foreach (int dimension in Shapes.Sizes)
                {
                    var point = new Point(type, dimension);
                    point.Generate();
                    System.IO.File.WriteAllText(System.IO.Path.Combine(rpath, point.Name + ".cs"), point.Text);
                    Console.WriteLine("Done - " + point.Name);

                    var size = new Size(type, dimension);
                    size.Generate();
                    System.IO.File.WriteAllText(System.IO.Path.Combine(rpath, size.Name + ".cs"), size.Text);
                    Console.WriteLine("Done - " + size.Name);
                }

                var rectangle = new Rectangle(type);
                rectangle.Generate();
                System.IO.File.WriteAllText(System.IO.Path.Combine(rpath, rectangle.Name + ".cs"), rectangle.Text);
                Console.WriteLine("Done - " + rectangle.Name);

                var box = new Box(type);
                box.Generate();
                System.IO.File.WriteAllText(System.IO.Path.Combine(rpath, box.Name + ".cs"), box.Text);
                Console.WriteLine("Done - " + box.Name);
            }

            Console.ReadLine();
        }
    }
}
