using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ibasa;
using Ibasa.Numerics;

namespace Ibasa.Noise
{
    public sealed class Value : Module<double>
    {
        public Quality Quality { get; set; }

        public readonly int Seed;
        
        public Value(Quality quality = Quality.Quintic)
        {
            Quality = quality;
            Seed = (int)DateTime.Now.Ticks;
        }
        public Value(int seed, Quality quality = Quality.Quintic)
        {
            Quality = quality;
            Seed = seed;
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
                    return ValueNoise(ix0);
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

            return Functions.Lerp(ValueNoise(ix0), ValueNoise(ix1), a);
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
                    return ValueNoise(ix0, iy0);
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
               Functions.Lerp(ValueNoise(ix0, iy0), ValueNoise(ix1, iy0), a),
               Functions.Lerp(ValueNoise(ix0, iy1), ValueNoise(ix1, iy1), a), b);
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
                    return ValueNoise(ix0, iy0, iz0);
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
                   Functions.Lerp(ValueNoise(ix0, iy0, iz0), ValueNoise(ix1, iy0, iz0), a),
                   Functions.Lerp(ValueNoise(ix0, iy1, iz0), ValueNoise(ix1, iy1, iz0), a), b),
               Functions.Lerp(
                   Functions.Lerp(ValueNoise(ix0, iy0, iz1), ValueNoise(ix1, iy0, iz1), a),
                   Functions.Lerp(ValueNoise(ix0, iy1, iz1), ValueNoise(ix1, iy1, iz1), a), b), c);
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
                    return ValueNoise(ix0, iy0, iz0, iw0);
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
                       Functions.Lerp(ValueNoise(ix0, iy0, iz0, iw0), ValueNoise(ix1, iy0, iz0, iw0), a),
                       Functions.Lerp(ValueNoise(ix0, iy1, iz0, iw0), ValueNoise(ix1, iy1, iz0, iw0), a), b),
                   Functions.Lerp(
                       Functions.Lerp(ValueNoise(ix0, iy0, iz1, iw0), ValueNoise(ix1, iy0, iz1, iw0), a),
                       Functions.Lerp(ValueNoise(ix0, iy1, iz1, iw0), ValueNoise(ix1, iy1, iz1, iw0), a), b), c),
               Functions.Lerp(
                   Functions.Lerp(
                       Functions.Lerp(ValueNoise(ix0, iy0, iz0, iw1), ValueNoise(ix1, iy0, iz0, iw1), a),
                       Functions.Lerp(ValueNoise(ix0, iy1, iz0, iw1), ValueNoise(ix1, iy1, iz0, iw1), a), b),
                   Functions.Lerp(
                       Functions.Lerp(ValueNoise(ix0, iy0, iz1, iw1), ValueNoise(ix1, iy0, iz1, iw1), a),
                       Functions.Lerp(ValueNoise(ix0, iy1, iz1, iw1), ValueNoise(ix1, iy1, iz1, iw1), a), b), c), d);
        }

        static long Hash(long x)
        {
            ulong v = (ulong)x * 3935559000370003845 + 2691343689449507681;
            v ^= v >> 21; v ^= v << 31; v ^= v >> 4;
            v *= 4768777513237032717;
            v ^= v << 20; v ^= v >> 41; v ^= v << 5;
            return (long)v & long.MaxValue;
        }
        static double HashDouble(long x)
        {
            return (1.0 / long.MaxValue) * Hash(x);
        }

        double ValueNoise(int x)
        {
            long n = Hash(x) + Seed;
            return 1.0 - 2.0 * HashDouble(n);
        }
        double ValueNoise(int x, int y)
        {
            long n =
                Hash(x) +
                Hash(y) + Seed;
            return 1.0 - 2.0 * HashDouble(n);
        }
        double ValueNoise(int x, int y, int z)
        {
            long n =
                Hash(x) +
                Hash(y) +
                Hash(z) + Seed;
            return 1.0 - 2.0 * HashDouble(n);
        }
        double ValueNoise(int x, int y, int z, int w)
        {
            long n = 
                Hash(x) + 
                Hash(y) + 
                Hash(z) +
                Hash(w) + Seed;
            return 1.0 - 2.0 * HashDouble(n);
        }
        double ValueNoise(int x, int y, int z, int w, int v, int u)
        {
            long n =
                   Hash(x) +
                   Hash(y) +
                   Hash(z) +
                   Hash(w) +
                   Hash(v) +
                   Hash(u) + Seed;
            return 1.0 - 2.0 * HashDouble(n);
        }
    }
}
