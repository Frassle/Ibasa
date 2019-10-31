using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise
{
    public sealed class Spheres : Module<double>
    {
        public double Frequency { get; set; }

        public Spheres(double frequency=1.0)
        {
            Frequency = frequency;
        }

        public override double Evaluate(double x)
        {
            x *= Frequency;

            double distFromCenter = x;
            double distFromSmallerSphere = distFromCenter - Functions.Floor(distFromCenter);
            double distFromLargerSpere = 1.0 - distFromSmallerSphere;
            double nearestDist = Functions.Min(distFromSmallerSphere, distFromLargerSpere);
            return 1.0 - (nearestDist * 4.0);
        }

        public override double Evaluate(double x, double y)
        {
            x *= Frequency;
            y *= Frequency;

            double distFromCenter = Functions.Sqrt(x * x + y * y);
            double distFromSmallerSphere = distFromCenter - Functions.Floor(distFromCenter);
            double distFromLargerSpere = 1.0 - distFromSmallerSphere;
            double nearestDist = Functions.Min(distFromSmallerSphere, distFromLargerSpere);
            return 1.0 - (nearestDist * 4.0);
        }

        public override double Evaluate(double x, double y, double z)
        {
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;

            double distFromCenter = Functions.Sqrt(x * x + y * y + z * z);
            double distFromSmallerSphere = distFromCenter - Functions.Floor(distFromCenter);
            double distFromLargerSpere = 1.0 - distFromSmallerSphere;
            double nearestDist = Functions.Min(distFromSmallerSphere, distFromLargerSpere);
            return 1.0 - (nearestDist * 4.0);
        }

        public override double Evaluate(double x, double y, double z, double w)
        {
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;
            w *= Frequency;

            double distFromCenter = Functions.Sqrt(x * x + y * y + z * z + w * w);
            double distFromSmallerSphere = distFromCenter - Functions.Floor(distFromCenter);
            double distFromLargerSpere = 1.0 - distFromSmallerSphere;
            double nearestDist = Functions.Min(distFromSmallerSphere, distFromLargerSpere);
            return 1.0 - (nearestDist * 4.0);
        }

        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;
            w *= Frequency;
            v *= Frequency;
            u *= Frequency;

            double distFromCenter = Functions.Sqrt(x * x + y * y + z * z + w * w + v * v + u * u);
            double distFromSmallerSphere = distFromCenter - Functions.Floor(distFromCenter);
            double distFromLargerSpere = 1.0 - distFromSmallerSphere;
            double nearestDist = Functions.Min(distFromSmallerSphere, distFromLargerSpere);
            return 1.0 - (nearestDist * 4.0);
        }
    }
}
