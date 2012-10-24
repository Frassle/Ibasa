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

                var line1 = new Line(type, 1);
                line1.Generate();
                System.IO.File.WriteAllText(System.IO.Path.Combine(rpath, line1.Name + ".cs"), line1.Text);
                Console.WriteLine("Done - " + line1.Name);

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

                    var polygon = new Polygon(type, dimension);
                    polygon.Generate();
                    System.IO.File.WriteAllText(System.IO.Path.Combine(rpath, polygon.Name + ".cs"), polygon.Text);
                    Console.WriteLine("Done - " + polygon.Name);

                    var line = new Line(type, dimension);
                    line.Generate();
                    System.IO.File.WriteAllText(System.IO.Path.Combine(rpath, line.Name + ".cs"), line.Text);
                    Console.WriteLine("Done - " + line.Name);
                }

                var rectangle = new Rectangle(type);
                rectangle.Generate();
                System.IO.File.WriteAllText(System.IO.Path.Combine(rpath, rectangle.Name + ".cs"), rectangle.Text);
                Console.WriteLine("Done - " + rectangle.Name);

                var box = new Box(type);
                box.Generate();
                System.IO.File.WriteAllText(System.IO.Path.Combine(rpath, box.Name + ".cs"), box.Text);
                Console.WriteLine("Done - " + box.Name);

                var ellipse = new Ellipse(type);
                ellipse.Generate();
                System.IO.File.WriteAllText(System.IO.Path.Combine(rpath, ellipse.Name + ".cs"), ellipse.Text);
                Console.WriteLine("Done - " + ellipse.Name);

                var circle = new Circle(type);
                circle.Generate();
                System.IO.File.WriteAllText(System.IO.Path.Combine(rpath, circle.Name + ".cs"), circle.Text);
                Console.WriteLine("Done - " + circle.Name);

                var sphere = new Sphere(type);
                sphere.Generate();
                System.IO.File.WriteAllText(System.IO.Path.Combine(rpath, sphere.Name + ".cs"), sphere.Text);
                Console.WriteLine("Done - " + sphere.Name);
            }

            foreach (var type in Ray.Types)
            {
                var ray = new Ray(type);
                ray.Generate();

                var path = System.IO.Path.Combine(root, "Geometry", ray.Name + ".cs");
                System.IO.File.WriteAllText(path, ray.Text);
                Console.WriteLine("Done - " + ray.Name);
            }

            Console.ReadLine();
        }
    }
}
