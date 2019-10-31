using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ibasa;
using Ibasa.Numerics;

namespace Ibasa.Noise
{
    public sealed class Perlin : Module<double>
    {
        public Quality Quality { get; set; }
        public readonly int Seed;
        int[] Permutation;

        public Perlin(Quality quality = Quality.Quintic)
        {
            Quality = quality;
            Seed = (int)DateTime.Now.Ticks;
            BuildPermutationTable();
        }
        public Perlin(int seed, Quality quality = Quality.Quintic)
        {
            Quality = quality;
            Seed = seed;
            BuildPermutationTable();
        }

        void BuildPermutationTable()
        {
            System.Random random = new System.Random(Seed);

            Permutation = new int[512];
            for (int i = 0; i < 256; ++i)
            {
                Permutation[i] = i;
                Permutation[256 + i] = i;
            }
            for (int i = 0; i < 512; ++i)
            {
                int randomIndex = random.Next(512);

                int value = Permutation[i];
                Permutation[i] = Permutation[randomIndex];
                Permutation[randomIndex] = value;
            }
        }

        static double CubicCurve(double t)
        {
            return t * t * (3.0 - 2.0 * t);
        }
        static double QuinticCurve(double t)
        {
            return t * t * t * (t * (t * 6.0 - 15.0) + 10.0);
        }

        public override double Evaluate(double x)
        {
            double x0 = Functions.Floor(x);

            int ix0 = (int)x0;

            double a = 0.0;
            switch (Quality)
            {
                case Quality.Point:
                    return PerlinNoise(ix0, x);
                case Quality.Linear:
                    a = x - x0;
                    break;
                case Quality.Cubic:
                    a = CubicCurve(x - x0);
                    break;
                case Quality.Quintic:
                    a = QuinticCurve(x - x0);
                    break;
            }

            double x1 = x0 + 1;

            int ix1 = (int)x1;

            return Functions.Lerp(PerlinNoise(ix0, x), PerlinNoise(ix1, x), a);
        }
        public override double Evaluate(double x, double y)
        {
            double x0 = Functions.Floor(x);
            double y0 = Functions.Floor(y);

            int ix0 = (int)x0;
            int iy0 = (int)y0;

            double a = 0.0;
            double b = 0.0;
            switch (Quality)
            {
                case Quality.Point:
                    return PerlinNoise(ix0, iy0, x, y);
                case Quality.Linear:
                    a = x - x0;
                    b = y - y0;
                    break;
                case Quality.Cubic:
                    a = CubicCurve(x - x0);
                    b = CubicCurve(y - y0);
                    break;
                case Quality.Quintic:
                    a = QuinticCurve(x - x0);
                    b = QuinticCurve(y - y0);
                    break;
            }

            double x1 = x0 + 1;
            double y1 = y0 + 1;

            int ix1 = (int)x1;
            int iy1 = (int)y1;

            return Functions.Lerp(
               Functions.Lerp(PerlinNoise(ix0, iy0, x, y), PerlinNoise(ix1, iy0, x, y), a),
               Functions.Lerp(PerlinNoise(ix0, iy1, x, y), PerlinNoise(ix1, iy1, x, y), a), b);
        }
        public override double Evaluate(double x, double y, double z)
        {
            double x0 = Functions.Floor(x);
            double y0 = Functions.Floor(y);
            double z0 = Functions.Floor(z);

            int ix0 = (int)x0;
            int iy0 = (int)y0;
            int iz0 = (int)z0;

            double a = 0.0;
            double b = 0.0;
            double c = 0.0;
            switch (Quality)
            {
                case Quality.Point:
                    return PerlinNoise(ix0, iy0, iz0, x, y, z);
                case Quality.Linear:
                    a = x - x0;
                    b = y - y0;
                    c = z - z0;
                    break;
                case Quality.Cubic:
                    a = CubicCurve(x - x0);
                    b = CubicCurve(y - y0);
                    c = CubicCurve(z - z0);
                    break;
                case Quality.Quintic:
                    a = QuinticCurve(x - x0);
                    b = QuinticCurve(y - y0);
                    c = QuinticCurve(z - z0);
                    break;
            }

            double x1 = x0 + 1;
            double y1 = y0 + 1;
            double z1 = z0 + 1;

            int ix1 = (int)x1;
            int iy1 = (int)y1;
            int iz1 = (int)z1;

            return Functions.Lerp(
               Functions.Lerp(
                   Functions.Lerp(PerlinNoise(ix0, iy0, iz0, x, y, z), PerlinNoise(ix1, iy0, iz0, x, y, z), a),
                   Functions.Lerp(PerlinNoise(ix0, iy1, iz0, x, y, z), PerlinNoise(ix1, iy1, iz0, x, y, z), a), b),
               Functions.Lerp(
                   Functions.Lerp(PerlinNoise(ix0, iy0, iz1, x, y, z), PerlinNoise(ix1, iy0, iz1, x, y, z), a),
                   Functions.Lerp(PerlinNoise(ix0, iy1, iz1, x, y, z), PerlinNoise(ix1, iy1, iz1, x, y, z), a), b), c);
        }
        public override double Evaluate(double x, double y, double z, double w)
        {
            double x0 = Functions.Floor(x);
            double y0 = Functions.Floor(y);
            double z0 = Functions.Floor(z);
            double w0 = Functions.Floor(w);

            int ix0 = (int)x0;
            int iy0 = (int)y0;
            int iz0 = (int)z0;
            int iw0 = (int)w0;

            double a = 0.0;
            double b = 0.0;
            double c = 0.0;
            double d = 0.0;
            switch (Quality)
            {
                case Quality.Point:
                    return PerlinNoise(ix0, iy0, iz0, iw0, x, y, z, w);
                case Quality.Linear:
                    a = x - x0;
                    b = y - y0;
                    c = z - z0;
                    d = w - w0;
                    break;
                case Quality.Cubic:
                    a = CubicCurve(x - x0);
                    b = CubicCurve(y - y0);
                    c = CubicCurve(z - z0);
                    d = CubicCurve(w - w0);
                    break;
                case Quality.Quintic:
                    a = QuinticCurve(x - x0);
                    b = QuinticCurve(y - y0);
                    c = QuinticCurve(z - z0);
                    d = QuinticCurve(w - w0);
                    break;
            }

            double x1 = x0 + 1;
            double y1 = y0 + 1;
            double z1 = z0 + 1;
            double w1 = w0 + 1;

            int ix1 = (int)x1;
            int iy1 = (int)y1;
            int iz1 = (int)z1;
            int iw1 = (int)w1;

            return Functions.Lerp(
               Functions.Lerp(
                   Functions.Lerp(
                       Functions.Lerp(PerlinNoise(ix0, iy0, iz0, iw0, x, y, z, w), PerlinNoise(ix1, iy0, iz0, iw0, x, y, z, w), a),
                       Functions.Lerp(PerlinNoise(ix0, iy1, iz0, iw0, x, y, z, w), PerlinNoise(ix1, iy1, iz0, iw0, x, y, z, w), a), b),
                   Functions.Lerp(
                       Functions.Lerp(PerlinNoise(ix0, iy0, iz1, iw0, x, y, z, w), PerlinNoise(ix1, iy0, iz1, iw0, x, y, z, w), a),
                       Functions.Lerp(PerlinNoise(ix0, iy1, iz1, iw0, x, y, z, w), PerlinNoise(ix1, iy1, iz1, iw0, x, y, z, w), a), b), c),
               Functions.Lerp(
                   Functions.Lerp(
                       Functions.Lerp(PerlinNoise(ix0, iy0, iz0, iw1, x, y, z, w), PerlinNoise(ix1, iy0, iz0, iw1, x, y, z, w), a),
                       Functions.Lerp(PerlinNoise(ix0, iy1, iz0, iw1, x, y, z, w), PerlinNoise(ix1, iy1, iz0, iw1, x, y, z, w), a), b),
                   Functions.Lerp(
                       Functions.Lerp(PerlinNoise(ix0, iy0, iz1, iw1, x, y, z, w), PerlinNoise(ix1, iy0, iz1, iw1, x, y, z, w), a),
                       Functions.Lerp(PerlinNoise(ix0, iy1, iz1, iw1, x, y, z, w), PerlinNoise(ix1, iy1, iz1, iw1, x, y, z, w), a), b), c), d);
        }
        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            double x0 = Functions.Floor(x);
            double y0 = Functions.Floor(y);
            double z0 = Functions.Floor(z);
            double w0 = Functions.Floor(w);
            double v0 = Functions.Floor(v);
            double u0 = Functions.Floor(u);

            int ix0 = (int)x0;
            int iy0 = (int)y0;
            int iz0 = (int)z0;
            int iw0 = (int)w0;
            int iv0 = (int)v0;
            int iu0 = (int)u0;

            double a = 0.0;
            double b = 0.0;
            double c = 0.0;
            double d = 0.0;
            double e = 0.0;
            double f = 0.0;
            switch (Quality)
            {
                case Quality.Point:
                    return PerlinNoise(ix0, iy0, iz0, iw0, iv0, iu0, x, y, z, w, v, u);
                case Quality.Linear:
                    a = x - x0;
                    b = y - y0;
                    c = z - z0;
                    d = w - w0;
                    e = v - v0;
                    f = u - u0;
                    break;
                case Quality.Cubic:
                    a = CubicCurve(x - x0);
                    b = CubicCurve(y - y0);
                    c = CubicCurve(z - z0);
                    d = CubicCurve(w - w0);
                    e = CubicCurve(v - v0);
                    f = CubicCurve(u - u0);
                    break;
                case Quality.Quintic:
                    a = QuinticCurve(x - x0);
                    b = QuinticCurve(y - y0);
                    c = QuinticCurve(z - z0);
                    d = QuinticCurve(w - w0);
                    e = QuinticCurve(v - v0);
                    f = QuinticCurve(u - u0);
                    break;
            }

            double x1 = x0 + 1;
            double y1 = y0 + 1;
            double z1 = z0 + 1;
            double w1 = w0 + 1;
            double v1 = v0 + 1;
            double u1 = u0 + 1;

            int ix1 = (int)x1;
            int iy1 = (int)y1;
            int iz1 = (int)z1;
            int iw1 = (int)w1;
            int iv1 = (int)v1;
            int iu1 = (int)u1;

            return Functions.Lerp(
                Functions.Lerp(
                    Functions.Lerp(
                       Functions.Lerp(
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz0, iw0, iv0, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz0, iw0, iv0, iu0, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz0, iw0, iv0, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz0, iw0, iv0, iu0, x, y, z, w, v, u), a), b),
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz1, iw0, iv0, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz1, iw0, iv0, iu0, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz1, iw0, iv0, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz1, iw0, iv0, iu0, x, y, z, w, v, u), a), b), c),
                       Functions.Lerp(
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz0, iw1, iv0, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz0, iw1, iv0, iu0, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz0, iw1, iv0, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz0, iw1, iv0, iu0, x, y, z, w, v, u), a), b),
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz1, iw1, iv0, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz1, iw1, iv0, iu0, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz1, iw1, iv0, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz1, iw1, iv0, iu0, x, y, z, w, v, u), a), b), c), d),
                    Functions.Lerp(
                       Functions.Lerp(
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz0, iw0, iv1, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz0, iw0, iv1, iu0, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz0, iw0, iv1, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz0, iw0, iv1, iu0, x, y, z, w, v, u), a), b),
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz1, iw0, iv1, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz1, iw0, iv1, iu0, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz1, iw0, iv1, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz1, iw0, iv1, iu0, x, y, z, w, v, u), a), b), c),
                       Functions.Lerp(
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz0, iw1, iv1, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz0, iw1, iv1, iu0, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz0, iw1, iv1, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz0, iw1, iv1, iu0, x, y, z, w, v, u), a), b),
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz1, iw1, iv1, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz1, iw1, iv1, iu0, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz1, iw1, iv1, iu0, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz1, iw1, iv1, iu0, x, y, z, w, v, u), a), b), c), d), e),
                Functions.Lerp(
                    Functions.Lerp(
                       Functions.Lerp(
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz0, iw0, iv0, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz0, iw0, iv0, iu1, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz0, iw0, iv0, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz0, iw0, iv0, iu1, x, y, z, w, v, u), a), b),
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz1, iw0, iv0, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz1, iw0, iv0, iu1, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz1, iw0, iv0, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz1, iw0, iv0, iu1, x, y, z, w, v, u), a), b), c),
                       Functions.Lerp(
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz0, iw1, iv0, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz0, iw1, iv0, iu1, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz0, iw1, iv0, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz0, iw1, iv0, iu1, x, y, z, w, v, u), a), b),
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz1, iw1, iv0, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz1, iw1, iv0, iu1, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz1, iw1, iv0, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz1, iw1, iv0, iu1, x, y, z, w, v, u), a), b), c), d),
                    Functions.Lerp(
                       Functions.Lerp(
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz0, iw0, iv1, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz0, iw0, iv1, iu1, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz0, iw0, iv1, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz0, iw0, iv1, iu1, x, y, z, w, v, u), a), b),
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz1, iw0, iv1, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz1, iw0, iv1, iu1, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz1, iw0, iv1, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz1, iw0, iv1, iu1, x, y, z, w, v, u), a), b), c),
                       Functions.Lerp(
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz0, iw1, iv1, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz0, iw1, iv1, iu1, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz0, iw1, iv1, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz0, iw1, iv1, iu1, x, y, z, w, v, u), a), b),
                           Functions.Lerp(
                               Functions.Lerp(PerlinNoise(ix0, iy0, iz1, iw1, iv1, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy0, iz1, iw1, iv1, iu1, x, y, z, w, v, u), a),
                               Functions.Lerp(PerlinNoise(ix0, iy1, iz1, iw1, iv1, iu1, x, y, z, w, v, u), PerlinNoise(ix1, iy1, iz1, iw1, iv1, iu1, x, y, z, w, v, u), a), b), c), d), e), f);
        }

        double PerlinNoise(int gx, double px)
        {
            int ix = gx & 255;

            int h0 = Permutation[ix];
            int hash = Permutation[h0];

            double vx = px - gx;

            double p = (hash & (1 << 0)) == 0 ? -vx : vx;
            return (p);
        }
        double PerlinNoise(int gx, int gy, double px, double py)
        {
            int ix = gx & 255;
            int iy = gy & 255;

            int h0 = Permutation[ix];
            int h1 = Permutation[h0 + iy];
            int hash = Permutation[h1];

            double vx = px - gx;
            double vy = py - gy;

            double p = (hash & (1 << 0)) == 0 ? -vx : vx;
            double q = (hash & (1 << 1)) == 0 ? -vy : vy;
            return (p + q);
        }
        double PerlinNoise(int gx, int gy, int gz, double px, double py, double pz)
        {
            int ix = gx & 255;
            int iy = gy & 255;
            int iz = gz & 255;

            int h0 = Permutation[ix];
            int h1 = Permutation[h0 + iy];
            int h2 = Permutation[h1 + iz];
            int hash = Permutation[h2];

            double vx = px - gx;
            double vy = py - gy;
            double vz = pz - gz;

            double p = (hash & (1 << 0)) == 0 ? -vx : vx;
            double q = (hash & (1 << 1)) == 0 ? -vy : vy;
            double r = (hash & (1 << 2)) == 0 ? -vz : vz;
            return (p + q + r);
        }
        double PerlinNoise(int gx, int gy, int gz, int gw, double px, double py, double pz, double pw)
        {
            int ix = gx & 255;
            int iy = gy & 255;
            int iz = gz & 255;
            int iw = gw & 255;

            int h0 = Permutation[ix];
            int h1 = Permutation[h0 + iy];
            int h2 = Permutation[h1 + iz];
            int h3 = Permutation[h2 + iw];
            int hash = Permutation[h3];

            double vx = px - gx;
            double vy = py - gy;
            double vz = pz - gz;
            double vw = pw - gw;

            double p = (hash & (1 << 0)) == 0 ? -vx : vx;
            double q = (hash & (1 << 1)) == 0 ? -vy : vy;
            double r = (hash & (1 << 2)) == 0 ? -vz : vz;
            double s = (hash & (1 << 3)) == 0 ? -vw : vw;
            return (p + q + r + s);
        }
        double PerlinNoise(int gx, int gy, int gz, int gw, int gv, int gu, double px, double py, double pz, double pw, double pv, double pu)
        {
            int ix = gx & 255;
            int iy = gy & 255;
            int iz = gz & 255;
            int iw = gw & 255;
            int iv = gv & 255;
            int iu = gu & 255;

            int h0 = Permutation[ix];
            int h1 = Permutation[h0 + iy];
            int h2 = Permutation[h1 + iz];
            int h3 = Permutation[h2 + iw];
            int h4 = Permutation[h3 + iv];
            int h5 = Permutation[h4 + iu];
            int hash = Permutation[h5];

            double vx = px - gx;
            double vy = py - gy;
            double vz = pz - gz;
            double vw = pw - gw;
            double vv = pv - gv;
            double vu = pu - gu;

            double p = (hash & (1 << 0)) == 0 ? -vx : vx;
            double q = (hash & (1 << 1)) == 0 ? -vy : vy;
            double r = (hash & (1 << 2)) == 0 ? -vz : vz;
            double s = (hash & (1 << 3)) == 0 ? -vw : vw;
            double t = (hash & (1 << 4)) == 0 ? -vv : vv;
            double u = (hash & (1 << 5)) == 0 ? -vu : vu;
            return (p + q + r + s + t + u);
        }
    }
}
