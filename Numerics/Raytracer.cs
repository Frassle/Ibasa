using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Numerics
{
    public class Raytracer
    {
        readonly Size3l GridSize;

        public Raytracer(Size2l grid_size)
        {
            GridSize = new Size3l(grid_size.Width, grid_size.Height, 0);
        }

        public Raytracer(Size3l grid_size)
        {
            GridSize = grid_size;
        }

        public IEnumerable<Tuple<Point2l, Point2l>> Trace(Point2l start, Point2l end)
        {
            var x0 = start.X / GridSize.Width;
            var y0 = start.Y / GridSize.Height;

            var x1 = end.X / GridSize.Width;
            var y1 = end.Y / GridSize.Height;

            var dir = new Vector2l(end.X - start.X, end.Y - start.Y);

            var dx = Math.Sign(dir.X);
            var dy = Math.Sign(dir.Y);

            var deltax = (double)GridSize.Width / Math.Abs(dir.X);
            var deltay = (double)GridSize.Height / Math.Abs(dir.Y);

            var minx = x0 * GridSize.Width;
            var maxx = minx + GridSize.Width;

            var miny = y0 * GridSize.Height;
            var maxy = miny + GridSize.Height;

            var distx = ((dx >= 0) ? (maxx - start.X) : (start.X - minx)) / (double)GridSize.Width;
            var disty = ((dy >= 0) ? (maxy - start.Y) : (start.Y - miny)) / (double)GridSize.Height;

            var tx = distx * deltax;
            var ty = disty * deltay;

            var prev = new Point2l(x0, y0);
            var hit = start;
            while (true)
            {
                var grid = new Point2l(x0, y0);
                yield return Tuple.Create(grid, hit);

                prev = grid;

                if (tx <= ty)
                {
                    if (x0 == x1) break;

                    hit = new Point2l(
                        start.X + (long)(dir.X * tx),
                        start.Y + (long)(dir.Y * tx));

                    tx += deltax;
                    x0 += dx;
                }
                else
                {
                    if (y0 == y1) break;

                    hit = new Point2l(
                        start.X + (long)(dir.X * ty),
                        start.Y + (long)(dir.Y * ty));

                    ty += deltay;
                    y0 += dy;
                }
            }
        }

        public IEnumerable<Tuple<Point3l, Point3l>> Trace(Point3l start, Point3l end)
        {
            var x0 = start.X / GridSize.Width;
            var y0 = start.Y / GridSize.Height;
            var z0 = start.Z / GridSize.Depth;

            var x1 = end.X / GridSize.Width;
            var y1 = end.Y / GridSize.Height;
            var z1 = end.Z / GridSize.Depth;

            var dir = new Vector3l(end.X - start.X, end.Y - start.Y, end.Z - start.Z);

            var dx = Math.Sign(dir.X);
            var dy = Math.Sign(dir.Y);
            var dz = Math.Sign(dir.Z);

            var deltax = (double)GridSize.Width / Math.Abs(dir.X);
            var deltay = (double)GridSize.Height / Math.Abs(dir.Y);
            var deltaz = (double)GridSize.Depth / Math.Abs(dir.Z);

            var minx = x0 * GridSize.Width;
            var maxx = minx + GridSize.Width;

            var miny = y0 * GridSize.Height;
            var maxy = miny + GridSize.Height;

            var minz = z0 * GridSize.Depth;
            var maxz = minz + GridSize.Depth;

            var distx = ((dx >= 0) ? (maxx - start.X) : (start.X - minx)) / (double)GridSize.Width;
            var disty = ((dy >= 0) ? (maxy - start.Y) : (start.Y - miny)) / (double)GridSize.Height;
            var distz = ((dz >= 0) ? (maxz - start.Z) : (start.Z - minz)) / (double)GridSize.Depth;

            var tx = distx * deltax;
            var ty = disty * deltay;
            var tz = distz * deltaz;

            var prev = new Point3l(x0, y0, z0);
            var hit = start;
            while (true)
            {
                var grid = new Point3l(x0, y0, z0);
                yield return Tuple.Create(grid, hit);

                prev = grid;

                if (tx <= ty && tx <= tz)
                {
                    if (x0 == x1) break;

                    hit = new Point3l(
                        start.X + (long)(dir.X * tx),
                        start.Y + (long)(dir.Y * tx),
                        start.Z + (long)(dir.Z * tz));

                    tx += deltax;
                    x0 += dx;
                }
                else if(ty <= tz)
                {
                    if (y0 == y1) break;

                    hit = new Point3l(
                        start.X + (long)(dir.X * tx),
                        start.Y + (long)(dir.Y * tx),
                        start.Z + (long)(dir.Z * tz));

                    ty += deltay;
                    y0 += dy;
                }
                else 
                {
                    if (z0 == z1) break;

                    hit = new Point3l(
                        start.X + (long)(dir.X * tx),
                        start.Y + (long)(dir.Y * tx),
                        start.Z + (long)(dir.Z * tz));

                    tz += deltaz;
                    z0 += dz;
                }
            }
        }
    }
}
