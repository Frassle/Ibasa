using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Noise
{
    public sealed class Curve : Module<double>
    {
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

        #region Curve
        private Ibasa.Numerics.Curve property_Curve;
        /// <summary>
        /// Curve.
        /// </summary>
        public Ibasa.Numerics.Curve MappingCurve
        {
            get { return property_Curve; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Curve = value;
            }
        }
        #endregion

        public Curve(Module<double> source, Ibasa.Numerics.Curve curve)
        {
            Source = source;
            MappingCurve = curve;
        }

        public override double Evaluate(double x)
        {
            return MappingCurve.Evaluate(Source.Evaluate(x));
        }

        public override double Evaluate(double x, double y)
        {
            return MappingCurve.Evaluate(Source.Evaluate(x, y));
        }

        public override double Evaluate(double x, double y, double z)
        {
            return MappingCurve.Evaluate(Source.Evaluate(x, y, z));
        }

        public override double Evaluate(double x, double y, double z, double w)
        {
            return MappingCurve.Evaluate(Source.Evaluate(x, y, z, w));
        }

        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return MappingCurve.Evaluate(Source.Evaluate(x, y, z, w, v, u));
        }
    }
}
