using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise
{
    public sealed class Checkerboard : Module<double>
    {
        public override double Evaluate(double x)
        {
            bool isEven = x == 0 | (x % 2) < Functions.Sign(x);
            return isEven ? -1.0 : 1.0;
        }

        public override double Evaluate(double x, double y)
        {
            bool isEvenX = x == 0 | (x % 2) < Functions.Sign(x);
            bool isEvenY = y == 0 | (y % 2) < Functions.Sign(y);
            bool isEven = isEvenX ^ isEvenY;
            return isEven ? -1.0 : 1.0;
        }

        public override double Evaluate(double x, double y, double z)
        {
            bool isEvenX = x == 0 | (x % 2) < Functions.Sign(x);
            bool isEvenY = y == 0 | (y % 2) < Functions.Sign(y);
            bool isEvenZ = z == 0 | (z % 2) < Functions.Sign(z);
            bool isEven = isEvenX ^ isEvenY ^ isEvenZ;
            return isEven ? -1.0 : 1.0;
        }

        public override double Evaluate(double x, double y, double z, double w)
        {
            bool isEvenX = x == 0 | (x % 2) < Functions.Sign(x);
            bool isEvenY = y == 0 | (y % 2) < Functions.Sign(y);
            bool isEvenZ = z == 0 | (z % 2) < Functions.Sign(z);
            bool isEvenW = w == 0 | (w % 2) < Functions.Sign(w);
            bool isEven = isEvenX ^ isEvenY ^ isEvenZ ^ isEvenW;
            return isEven ? -1.0 : 1.0;
        }

        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            bool isEvenX = x == 0 | (x % 2) < Functions.Sign(x);
            bool isEvenY = y == 0 | (y % 2) < Functions.Sign(y);
            bool isEvenZ = z == 0 | (z % 2) < Functions.Sign(z);
            bool isEvenW = w == 0 | (w % 2) < Functions.Sign(w);
            bool isEvenV = v == 0 | (v % 2) < Functions.Sign(v);
            bool isEvenU = u == 0 | (u % 2) < Functions.Sign(u);
            bool isEven = isEvenX ^ isEvenY ^ isEvenZ ^ isEvenW ^ isEvenV ^ isEvenU;
            return isEven ? -1.0 : 1.0;
        }
    }
}
