using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise
{ 
    public sealed class Exponent : Module<double>
    {
        public double Exp { get; set; }

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
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source = value;
            }
        }
        #endregion

        public Exponent(Module<double> source, double exp)
        {
            Source = source; 
            Exp = exp;
        }

        public override double Evaluate(double x)
        {
            return Functions.Pow(Functions.Abs((Source.Evaluate(x) + 1.0) * 0.5), Exp) * 2.0 - 1.0;
        }

        public override double Evaluate(double x, double y)
        {
            return Functions.Pow(Functions.Abs((Source.Evaluate(x, y) + 1.0) * 0.5), Exp) * 2.0 - 1.0;
        }

        public override double Evaluate(double x, double y, double z)
        {
            return Functions.Pow(Functions.Abs((Source.Evaluate(x, y, z) + 1.0) * 0.5), Exp) * 2.0 - 1.0;
        }

        public override double Evaluate(double x, double y, double z, double w)
        {
            return Functions.Pow(Functions.Abs((Source.Evaluate(x, y, z, w) + 1.0) * 0.5), Exp) * 2.0 - 1.0;
        }

        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return Functions.Pow(Functions.Abs((Source.Evaluate(x, y, z, w, v, u) + 1.0) * 0.5), Exp) * 2.0 - 1.0;
        }
    }
}
