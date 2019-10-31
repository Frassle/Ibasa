using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Ibasa.Numerics;

namespace Ibasa.Noise
{
    public sealed class Tileable : Module<double>
    {
        private double XRadius;
        private double YRadius;
        private double ZRadius;

        public double Width
        {
            get { return 2.0 * Functions.PI * XRadius; }
            set
            {
                Contract.Requires(value > 0);
                XRadius = value / (2.0 * Functions.PI);
            }
        }
        public double Height 
        {
            get { return 2.0 * Functions.PI * YRadius; }
            set
            {
                Contract.Requires(value > 0); 
                YRadius = value / (2.0 * Functions.PI);
            }
        }
        public double Depth
        {
            get { return 2.0 * Functions.PI * ZRadius; }
            set
            {
                Contract.Requires(value > 0); 
                ZRadius = value / (2.0 * Functions.PI);
            }
        }

        #region Source
        private Module<double> property_Source;
        /// <summary>
        /// Source module.
        /// </summary>
        public Module<double> Source
        {
            get { return property_Source; }
            set
            {
                global::System.Diagnostics.Contracts.Contract.Requires(value != null);
                property_Source = value;
            }
        }
        #endregion

        public Tileable(Module<double> source,
            double width, double height = 2.0 * Functions.PI, double depth = 2.0 * Functions.PI)
        {
            Source = source; 
            Width = width;
            Height = height;
            Depth = depth;
        }

        public override double Evaluate(double x)
        {
            x /= XRadius;

            double cos = Functions.Cos(x) * XRadius;
            double sin = Functions.Sin(x) * XRadius;

            return Source.Evaluate(cos, sin);
        }

        public override double Evaluate(double x, double y)
        {
            x /= XRadius;
            y /= YRadius;

            double xcos = Functions.Cos(x) * XRadius;
            double xsin = Functions.Sin(x) * XRadius;

            double ycos = Functions.Cos(y) * YRadius;
            double ysin = Functions.Sin(y) * YRadius;

            return Source.Evaluate(xcos, xsin, ycos, ysin);
        }

        public override double Evaluate(double x, double y, double z)
        {
            x /= XRadius;
            y /= YRadius;
            z /= ZRadius;

            double xcos = Functions.Cos(x) * XRadius;
            double xsin = Functions.Sin(x) * XRadius;

            double ycos = Functions.Cos(y) * YRadius;
            double ysin = Functions.Sin(y) * YRadius;
            
            double zcos = Functions.Cos(z) * ZRadius;
            double zsin = Functions.Sin(z) * ZRadius;

            return Source.Evaluate(xcos, xsin, ycos, ysin, zcos, zsin);
        }

        public override double Evaluate(double x, double y, double z, double w)
        {
            throw new InvalidOperationException("Cannot tile 4D noise.");
        }

        public override double Evaluate(double x, double y, double z, double w, double u, double v)
        {
            throw new InvalidOperationException("Cannot tile 6D noise.");
        }
    }
}
