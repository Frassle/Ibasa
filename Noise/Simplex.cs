using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise
{
    public sealed class Simplex : Module<double>
    {
        public readonly int Seed;

        public Simplex()
        {
            Seed = (int)DateTime.Now.Ticks;
        }
        public Simplex(int seed)
        {
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

        //void CalculateFG(int d, out double f, out double g)
        //{
        //    double skew = Functions.Sqrt(d + 1);
        //    double d2 = d * skew;
        //    double delta = skew - 1.0;
        //    f = delta / d;
        //    g = delta / d2;
        //}

        private int Hash(int i)
        {
            i = ((i << 16) ^ i) + Seed;

            //32 to 16
            int high = (i >> 16);
            int low = (i & 0xFFFF);
            int hash = high ^ low;
            //16 to 8
            high = (hash >> 8);
            low = (hash & 0xFF);
            hash = high ^ low;
            //8 to 4
            high = (hash >> 4);
            low = (hash & 0xF);
            hash = high ^ low;
            //4 to 2
            high = (hash >> 2);
            low = (hash & 0x3);
            hash = high ^ low;
            //2 to 1
            high = (hash >> 1);
            low = (hash & 0x1);
            hash = high ^ low;

            return hash;
        }
        private int Hash(int i, int j)
        {
            i = ((i << 16) ^ i) + Seed;
            j = ((j << 16) ^ j) + Seed;

            //64 to 32
            int high = i;
            int low = j;
            int hash = high ^ low;
            //32 to 16
            high = (hash >> 16);
            low = (hash & 0xFFFF);
            hash = high ^ low;
            //16 to 8
            high = (hash >> 8);
            low = (hash & 0xFF);
            hash = high ^ low;
            //8 to 4
            high = (hash >> 4);
            low = (hash & 0xF);
            hash = high ^ low;

            return hash;
        }
        private int Hash(int i, int j, int k)
        {
            //96 to 6
            //awkward just do 4 but l == 0 and grab bottom 6 bits
            return (Hash(i, j, k, 0) & 0x3F);
        }
        private int Hash(int i, int j, int k, int l)
        {
            i = ((i << 16) ^ i) + Seed;
            j = ((j << 16) ^ j) + Seed;
            k = ((k << 16) ^ k) + Seed;
            l = ((l << 16) ^ l) + Seed;

            //128 to 64
            long lhigh = (i << 32) | j;
            long llow = (k << 32) | l;
            long lhash = lhigh ^ llow;
            //64 to 32
            int high = (int)(lhash >> 32);
            int low = (int)(lhash);
            int hash = high ^ low;
            //32 to 16
            high = (hash >> 16);
            low = (hash & 0xFFFF);
            hash = high ^ low;
            //16 to 8
            high = (hash >> 8);
            low = (hash & 0xFF);
            hash = high ^ low;
            //8 to 4
            high = (hash >> 4);
            low = (hash & 0xF);
            hash = high ^ low;

            return hash;
        }

        public override double Evaluate(double x)
        {
            double F = 0.414213562;
            double G = 0.292893219;

            //skew factor
            double s = x * F;
            //skewed simplex corner
            double xi = Functions.Floor(x + s);
            //unskew factor
            double sp = xi * G;
            //unskewed simplex corner
            double x0 = xi - sp;
            //other corner
            double x1 = x0 + (1.0 - G);
            //deltas
            double u0 = x - x0;
            double u1 = x - x1;

            //lattice points
            int i = (int)x0;

            //calculate hash at each corner
            int hash0 = Hash(i);
            int hash1 = Hash(i + 1);

            //accumlate noise
            double n = 0;

            //calculate gradients
            double p0 = (hash0 & (1 << 0)) == 0 ? -u0 : u0;
            double p1 = (hash1 & (1 << 0)) == 0 ? -u1 : u1;

            double t0 = 0.5 - (p0 * p0);
            if (t0 > 0.0)
            {
                t0 = t0 * t0;
                n += t0 * t0;
            }

            double t1 = 0.5 - (p1 * p1);
            if (t1 > 0.0)
            {
                t1 = t1 * t1;
                n += t1 * t1;
            }

            return n;
        }

        public override double Evaluate(double x, double y)
        {
            double F = 0.366025404;
            double G = 0.211324865;

            //skew factor
            double s = (x + y) * F;
            //skewed simplex corner
            double xi = Functions.Floor(x + s);
            double yi = Functions.Floor(y + s);
            //unskew factor
            double sp = (xi + yi) * G;
            //unskewed simplex corner
            double x0 = xi - sp;
            double y0 = yi - sp;

            //other corner
            double x1 = x0 + (1.0 - G);


            //deltas
            double u0 = x - x0;
            double v0 = y - y0;
            double u1 = x - x1;
            double v1 = y - y1;
            double u2 = x - x2;
            double v2 = y - y2;

            //lattice points
            int i = (int)x0;

            //calculate hash at each corner
            int hash0 = Hash(i);
            int hash1 = Hash(i + 1);

            //accumlate noise
            double n = 0;

            //calculate gradients
            double p0 = (hash0 & (1 << 0)) == 0 ? -u0 : u0;
            double p1 = (hash1 & (1 << 0)) == 0 ? -u1 : u1;

            double t0 = 0.5 - (p0 * p0);
            if (t0 > 0.0)
            {
                t0 = t0 * t0;
                n += t0 * t0;
            }

            double t1 = 0.5 - (p1 * p1);
            if (t1 > 0.0)
            {
                t1 = t1 * t1;
                n += t1 * t1;
            }

            return n;
        }

        public override double Evaluate(double x, double y, double z)
        {
            double x0 = Functions.Floor(x);
            double x1 = x0 + 1.0;
            double y0 = Functions.Floor(y);
            double y1 = y0 + 1.0;
            double z0 = Functions.Floor(z);
            double z1 = z0 + 1.0;

            int i = (int)x0;
            int j = (int)y0;
            int k = (int)z0;

            double u = x - x0;
            double v = y - y0;
            double w = z - z0;

            int hash = Hash(i, j, k);

            double p = (hash & (1 << 3)) == 0 ? 0 : (hash & (1 << 0)) == 0 ? -u : u;
            double q = (hash & (1 << 4)) == 0 ? 0 : (hash & (1 << 1)) == 0 ? -v : v;
            double r = (hash & (1 << 5)) == 0 ? 0 : (hash & (1 << 2)) == 0 ? -w : w;


        }
        public void Sort(Vector2 vector, out int a, out int b)
        {
            int hash =
                vector.X >= vector.Y ? 1 : 0;

            //[0..hash..1] - 2
        }
        public void Sort(Vector3 vector, int a, int b, int c)
        {
            int hash =
                vector.X >= vector.Y ? 1 : 0 +
                vector.X >= vector.Z ? 2 : 0 +
                vector.Y >= vector.Z ? 4 : 0;

            //[0..hash..7] - 8
        }
        public void Sort(Vector4 vector, out int a, out int b, out int c, out int d)
        {
            int hash =
                vector.X >= vector.Y ? 1 : 0 +
                vector.X >= vector.Z ? 2 : 0 +
                vector.X >= vector.W ? 4 : 0 +
                vector.Y >= vector.Z ? 8 : 0 +
                vector.Y >= vector.W ? 16 : 0 +
                vector.Z >= vector.W ? 32 : 0;

            //[0..hash..63] - 64
        }

        public override double Evaluate(double x, double y, double z, double w)
        {
            double F = 0.309016994;
            double G = 0.138196601;

            Vector4 p = new Vector4(x, y, z, w);
            
            //skew factor
            double s = Vector4.HorizontalAdd(p) * F;
            //skewed simplex corner
            Vector4 i = Vector4.Floor(p + s);
            //unskew factor
            double sp = Vector4.HorizontalAdd(i) * G;
            //unskewed delta
            Vector4 d = p - i + s;



            //lattice points
            int i = (int)x0;

            //calculate hash at each corner
            int hash0 = Hash(i);
            int hash1 = Hash(i + 1);

            //accumlate noise
            double n = 0;

            //calculate gradients
            double p0 = (hash0 & (1 << 0)) == 0 ? -u0 : u0;
            double p1 = (hash1 & (1 << 0)) == 0 ? -u1 : u1;

            double t0 = 0.5 - (p0 * p0);
            if (t0 > 0.0)
            {
                t0 = t0 * t0;
                n += t0 * t0;
            }

            double t1 = 0.5 - (p1 * p1);
            if (t1 > 0.0)
            {
                t1 = t1 * t1;
                n += t1 * t1;
            }

            return n;
        }
    }
}
