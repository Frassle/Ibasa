using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise
{
    public sealed class Clamp : Module<double>
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
        
        #region LowerBound
        private Module<double> property_LowerBound;
        /// <summary>
        /// Inclusive lower bound
        /// </summary>
        public Module<double> LowerBound
        {
            get { return property_LowerBound; }
            set
            {
                global::System.Diagnostics.Contracts.Contract.Requires(value != null);
                property_LowerBound = value;
            }
        }
        #endregion

        #region UpperBound
        private Module<double> property_UpperBound;
        /// <summary>
        /// Exclusive upper bound
        /// </summary>
        public Module<double> UpperBound
        {
            get { return property_UpperBound; }
            set
            {
                global::System.Diagnostics.Contracts.Contract.Requires(value != null);
                property_UpperBound = value;
            }
        }
        #endregion

        public Clamp(Module<double> source)
        {
            Source = source;
            LowerBound = new Constant<double>(-1.0);
            UpperBound = new Constant<double>(+1.0);
        }
        public Clamp(Module<double> source, double lowerBound, double upperBound)
        {
            Source = source;
            LowerBound = new Constant<double>(lowerBound);
            UpperBound = new Constant<double>(upperBound);
        }
        public Clamp(Module<double> source, Module<double> lowerBound, Module<double> upperBound)
        {
            Source = source; 
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        public override double Evaluate(double x)
        {
            return Functions.Clamp(Source.Evaluate(x), LowerBound.Evaluate(x), UpperBound.Evaluate(x));
        }
        public override double Evaluate(double x, double y)
        {
            return Functions.Clamp(Source.Evaluate(x, y), LowerBound.Evaluate(x, y), UpperBound.Evaluate(x, y));
        }
        public override double Evaluate(double x, double y, double z)
        {
            return Functions.Clamp(Source.Evaluate(x, y, z), LowerBound.Evaluate(x, y, z), UpperBound.Evaluate(x, y, z));
        }
        public override double Evaluate(double x, double y, double z, double w)
        {
            return Functions.Clamp(Source.Evaluate(x, y, z, w), LowerBound.Evaluate(x, y, z, w), UpperBound.Evaluate(x, y, z, w));
        }
        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return Functions.Clamp(Source.Evaluate(x, y, z, w, v, u), LowerBound.Evaluate(x, y, z, w, v, u), UpperBound.Evaluate(x, y, z, w, v, u));
        }
    }
}
