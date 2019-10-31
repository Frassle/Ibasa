using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;
using System.Diagnostics.Contracts;

namespace Ibasa.Noise
{
    public sealed class Cylinders : Module<double>
    {
        public double Frequency { get; set; }

        public Cylinders(double frequency = 1.0)
        {
            Contract.Requires(0.0 <= frequency);

            Frequency = frequency;
        }

        public override double Evaluate(double x)
        {
            var fx = Functions.Floor(x / Frequency) * Frequency;
            var cx = Functions.Ceiling(x / Frequency) * Frequency;

            double dist0 = Functions.Sqrt((x - fx) * (x - fx));
            double dist1 = Functions.Sqrt((x - cx) * (x - cx));

            return Functions.Min(dist0, dist1);
        }

        public override double Evaluate(double x, double y)
        {
            var fx = Functions.Floor(x / Frequency) * Frequency;
            var fy = Functions.Floor(y / Frequency) * Frequency;
            var cx = Functions.Ceiling(x / Frequency) * Frequency;
            var cy = Functions.Ceiling(y / Frequency) * Frequency;

            double dist0 = Functions.Sqrt((x - fx) * (x - fx) + (y - fy) * (y - fy));
            double dist1 = Functions.Sqrt((x - fx) * (x - fx) + (y - cy) * (y - cy));
            double dist2 = Functions.Sqrt((x - cx) * (x - cx) + (y - fy) * (y - fy));
            double dist3 = Functions.Sqrt((x - cx) * (x - cx) + (y - cy) * (y - cy));

            return Functions.Min(dist0, Functions.Min(dist1, Functions.Min(dist2, dist3)));
        }

        public override double Evaluate(double x, double y, double z)
        {
            var fx = Functions.Floor(x / Frequency) * Frequency;
            var fy = Functions.Floor(y / Frequency) * Frequency;
            var cx = Functions.Ceiling(x / Frequency) * Frequency;
            var cy = Functions.Ceiling(y / Frequency) * Frequency;

            double dist0 = Functions.Sqrt((x - fx) * (x - fx) + (y - fy) * (y - fy));
            double dist1 = Functions.Sqrt((x - fx) * (x - fx) + (y - cy) * (y - cy));
            double dist2 = Functions.Sqrt((x - cx) * (x - cx) + (y - fy) * (y - fy));
            double dist3 = Functions.Sqrt((x - cx) * (x - cx) + (y - cy) * (y - cy));

            return Functions.Min(dist0, Functions.Min(dist1, Functions.Min(dist2, dist3)));
        }

        public override double Evaluate(double x, double y, double z, double w)
        {
            var fx = Functions.Floor(x / Frequency) * Frequency;
            var fy = Functions.Floor(y / Frequency) * Frequency;
            var cx = Functions.Ceiling(x / Frequency) * Frequency;
            var cy = Functions.Ceiling(y / Frequency) * Frequency;

            double dist0 = Functions.Sqrt((x - fx) * (x - fx) + (y - fy) * (y - fy));
            double dist1 = Functions.Sqrt((x - fx) * (x - fx) + (y - cy) * (y - cy));
            double dist2 = Functions.Sqrt((x - cx) * (x - cx) + (y - fy) * (y - fy));
            double dist3 = Functions.Sqrt((x - cx) * (x - cx) + (y - cy) * (y - cy));

            return Functions.Min(dist0, Functions.Min(dist1, Functions.Min(dist2, dist3)));
        }

        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            var fx = Functions.Floor(x / Frequency) * Frequency;
            var fy = Functions.Floor(y / Frequency) * Frequency;
            var cx = Functions.Ceiling(x / Frequency) * Frequency;
            var cy = Functions.Ceiling(y / Frequency) * Frequency;

            double dist0 = Functions.Sqrt((x - fx) * (x - fx) + (y - fy) * (y - fy));
            double dist1 = Functions.Sqrt((x - fx) * (x - fx) + (y - cy) * (y - cy));
            double dist2 = Functions.Sqrt((x - cx) * (x - cx) + (y - fy) * (y - fy));
            double dist3 = Functions.Sqrt((x - cx) * (x - cx) + (y - cy) * (y - cy));

            return Functions.Min(dist0, Functions.Min(dist1, Functions.Min(dist2, dist3)));
        }
    }
}
