using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Numerics
{
    public enum GJKResult
    {
        Disjoint,
        Intersect,
        Unsolved,
    }

    public sealed class GJK
    {
        public static Vector3d Support(Boxd box, Vector3d direction)
        {
            return new Vector3d(
                direction.X >= 0.0 ? box.Maximum.X : box.Minimum.X,
                direction.Y >= 0.0 ? box.Maximum.Y : box.Minimum.Y,
                direction.Z >= 0.0 ? box.Maximum.Z : box.Minimum.Z);
        }
        public static Vector3d Support(Sphered sphere, Vector3d direction)
        {
            double radius = sphere.Radius / Vector.Absolute(direction);
            return (Vector3d)sphere.Center + (direction * radius);
        }
        public static Vector3d Support(Polygon3d hull, Vector3d direction)
        {
            Vector3d result = Vector3d.Zero;
            double maxdot = double.NegativeInfinity;
            for (int i = 0; i < hull.Count; ++i)
            {
                var point = (Vector3d)hull[i];

                double dot = Vector.Dot(point, direction);
                if (dot > maxdot)
                {
                    maxdot = dot;
                    result = point;
                }
            }
            return result;
        }

        /// <summary>
        /// Collides two shapes using GJK.
        /// </summary>
        /// <typeparam name="T1">The type of the first shape.</typeparam>
        /// <typeparam name="T2">The type of the second shape.</typeparam>
        /// <param name="shape1">The first shape.</param>
        /// <param name="shape2">The second shape.</param>
        /// <param name="support1">The GJK support function for first shape.</param>
        /// <param name="support2">The GJK support function for the second shape.</param>
        /// <returns>A result indicating if the shapes are disjoint or intersect.</returns>
        /// <remarks>
        /// Collide uses the following algorithm, if delgates are proving slow you can copy the below and call
        /// the support functions direct.
        /// <code>
        /// GJK gjk = new GJK();
        /// Double3 a = support1(shape1, gjk,Direction);
        /// Double3 b = support2(shape2, -gjk.Direction);
        /// GJKResult result;
        /// while ((result = gjk.AddSupportPoint(a - b)) == GJKResult.Unsolved)
        /// {
        ///     a = support1(shape1, gjk.Direction);
        ///     b = support2(shape2, -gjk.Direction);
        /// }
        /// return result;
        /// </code>
        /// </remarks>
        public static GJKResult Collide<T1, T2>(T1 shape1, T2 shape2, Func<T1, Vector3d, Vector3d> support1, Func<T2, Vector3d, Vector3d> support2)
        {
            GJK gjk = new GJK();
            var a = support1(shape1, gjk.Direction);
            var b = support2(shape2, -gjk.Direction);
            GJKResult result;
            while ((result = gjk.AddSupportPoint(a - b)) == GJKResult.Unsolved)
            {
                a = support1(shape1, gjk.Direction);
                b = support2(shape2, -gjk.Direction);
            }
            return result;
        }
        /// <summary>
        /// Collides two shapes using GJK taking a maximum of stepLimit steps.
        /// </summary>
        /// <typeparam name="T1">The type of the first shape.</typeparam>
        /// <typeparam name="T2">The type of the second shape.</typeparam>
        /// <param name="shape1">The first shape.</param>
        /// <param name="shape2">The second shape.</param>
        /// <param name="support1">The GJK support function for first shape.</param>
        /// <param name="support2">The GJK support function for the second shape.</param>
        /// <param name="stepLimit">The GJK support function for the second shape.</param>
        /// <returns>A result indicating if the shapes are disjoint or intersect.</returns>
        /// <remarks>
        /// Collide uses the following algorithm, if delgates are proving slow you can copy the below and call
        /// the support functions direct.
        /// <code>
        /// GJK gjk = new GJK();
        /// Double3 a = support1(shape1, gjk,Direction);
        /// Double3 b = support2(shape2, -gjk.Direction);
        /// GJKResult result;
        /// while (--stepLimit > 0 &amp; (result = gjk.AddSupportPoint(a - b)) == GJKResult.Unsolved)
        /// {
        ///     a = support1(shape1, gjk.Direction);
        ///     b = support2(shape2, -gjk.Direction); 
        /// } 
        /// return result; 
        /// </code>
        /// </remarks>
        public static GJKResult Collide<T1, T2>(T1 shape1, T2 shape2, Func<T1, Vector3d, Vector3d> support1, Func<T2, Vector3d, Vector3d> support2, int stepLimit)
        {
            GJK gjk = new GJK();
            var a = support1(shape1, gjk.Direction);
            var b = support2(shape2, -gjk.Direction);
            GJKResult result;
            //& NOT &&, both sides should always be evaluated, no shortcircuiting.
            while (--stepLimit > 0 & (result = gjk.AddSupportPoint(a - b)) == GJKResult.Unsolved)
            {
                a = support1(shape1, gjk.Direction);
                b = support2(shape2, -gjk.Direction);
            }
            return result;
        }

        Vector3d[] Simplex = new Vector3d[4];
        int Count;

        public Vector3d Direction { get; private set; }

        public GJK()
        {
            Reset();
        }

        public void Reset()
        {
            Count = 0;
            Direction = Vector3d.Zero;
        }

        public GJKResult AddSupportPoint(Vector3d point)
        {
            Simplex[Count++] = point;
            if (Count == 1)
            {
                Direction = -point;
            }
            else
            {
                if (Vector.Dot(point, Direction) < 0.0)
                    return GJKResult.Disjoint;

                switch (Count)
                {
                    case 2:
                        {
                            DoLine(Simplex[1], Simplex[0]);
                            break;
                        }
                    case 3:
                        {
                            DoTriangle(Simplex[2], Simplex[1], Simplex[0]);
                            break;
                        }
                    case 4:
                        {
                            if (DoTetrahedron(Simplex[3], Simplex[2], Simplex[1], Simplex[0]))
                                return GJKResult.Intersect;
                            break;
                        }
                }
            }
            return GJKResult.Unsolved;
        }

        private void DoLine(Vector3d a, Vector3d b)
        {
            var ab = b - a;

            if (Vector.Dot(ab, -a) > 0.0)
            {
                Simplex[0] = b;
                Simplex[1] = a;
                Count = 2;
                Direction = Vector.Cross(ab, Vector.Cross(-a, ab));
            }
            else
            {
                Simplex[0] = a;
                Count = 1;
                Direction = -a;
            }
        }
        private void DoTriangle(Vector3d a, Vector3d b, Vector3d c)
        {
            var ab = b - a;
            var ac = c - a;

            var abc = Vector.Cross(ab, ac);
            var ababc = Vector.Cross(ab, abc);
            var abcac = Vector.Cross(abc, ac);

            if (Vector.Dot(abcac, -a) > 0.0)
            {
                if (Vector.Dot(ac, -a) > 0.0)
                {
                    Simplex[0] = c;
                    Simplex[1] = a;
                    Count = 2;
                    Direction = Vector.Cross(ac, Vector.Cross(-a, ac));
                }
                else
                {
                    DoLine(a, b);
                }
            }
            else
            {
                if (Vector.Dot(ababc, -a) > 0.0)
                {
                    DoLine(a, b);
                }
                else
                {
                    if (Vector.Dot(abc, -a) > 0.0)
                    {
                        Simplex[0] = c;
                        Simplex[1] = b;
                        Simplex[2] = a;
                        Count = 3;
                        Direction = abc;
                    }
                    else
                    {
                        Simplex[0] = b;
                        Simplex[1] = c;
                        Simplex[2] = a;
                        Count = 3;
                        Direction = -abc;
                    }
                }
            }
        }
        private bool DoTetrahedron(Vector3d a, Vector3d b, Vector3d c, Vector3d d)
        {
            var ab = b - a;
            var ac = c - a;
            var ad = d - a;
            var abc = Vector.Cross(ab, ac);
            var acd = Vector.Cross(ac, ad);
            var adb = Vector.Cross(ad, ab);

            if (Vector.Dot(abc, -a) > 0.0)
            {
                if (Vector.Dot(acd, -a) > 0.0)
                {
                    if (Vector.Dot(adb, -a) > 0.0)
                    {
                        Simplex[0] = a;
                        Count = 1;
                        Direction = -a;
                        return false;
                    }
                    else
                    {
                        DoLine(a, c);
                        return false;
                    }
                }
                else
                {
                    if (Vector.Dot(adb, -a) > 0.0)
                    {
                        DoLine(a, b);
                        return false;
                    }
                    else
                    {
                        DoTriangle(a, b, c);
                        return false;
                    }
                }
            }
            else
            {
                if (Vector.Dot(acd, -a) > 0.0)
                {
                    if (Vector.Dot(adb, -a) > 0.0)
                    {
                        DoLine(a, d);
                        return false;
                    }
                    else
                    {
                        DoTriangle(a, c, d);
                        return false;
                    }
                }
                else
                {
                    if (Vector.Dot(adb, -a) > 0.0)
                    {
                        DoTriangle(a, d, b);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }
}
