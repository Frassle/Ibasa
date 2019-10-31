using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise
{
    public sealed class Select : Module<double>
    {  
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double EdgeFalloff { get; set; }

        #region Source0
        private Module<double> property_Source0;
        /// <summary>
        /// Source module.
        /// </summary>
        public Module<double> Source0
        {
            get { return property_Source0; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source0 = value;
            }
        }
        #endregion

        #region Source1
        private Module<double> property_Source1;
        /// <summary>
        /// Source module.
        /// </summary>
        public Module<double> Source1
        {
            get { return property_Source1; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source1 = value;
            }
        }
        #endregion

        #region Control
        private Module<double> property_Control;
        /// <summary>
        /// Control module.
        /// </summary>
        public Module<double> Control
        {
            get { return property_Control; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Control = value;
            }
        }
        #endregion

        public Select(Module<double> source0, Module<double> source1, Module<double> control, 
            double lowerBound = -1.0, double upperBound = 1.0, double edgeFalloff = 0.0)
        {
            Source0 = source0; 
            Source1 = source1; 
            Control = control;

            LowerBound = lowerBound;
            UpperBound = upperBound;
            EdgeFalloff = edgeFalloff;
        }

        public override double Evaluate(double x)
        {
            double controlValue = Control.Evaluate(x);

            if (controlValue < (LowerBound - EdgeFalloff))
            {
                return Source0.Evaluate(x);
            }
            else if (controlValue < (LowerBound + EdgeFalloff))
            {
                double lowerCurve = LowerBound - EdgeFalloff;
                double upperCurve = LowerBound + EdgeFalloff;
                double alpha = (controlValue - lowerCurve) / (upperCurve - lowerCurve);
                return Functions.SmoothStep(
                    Source0.Evaluate(x), Source1.Evaluate(x), alpha);
            }
            else if (controlValue < (UpperBound - EdgeFalloff))
            {
                return Source1.Evaluate(x);
            }
            else if (controlValue < (UpperBound + EdgeFalloff))
            {
                double lowerCurve = UpperBound - EdgeFalloff;
                double upperCurve = UpperBound + EdgeFalloff;
                double alpha = (controlValue - lowerCurve) / (upperCurve - lowerCurve);
                return Functions.SmoothStep(
                    Source1.Evaluate(x), Source0.Evaluate(x), alpha);
            }
            else
            {
                return Source0.Evaluate(x);
            }
        }

        public override double Evaluate(double x, double y)
        {
            double controlValue = Control.Evaluate(x, y);

            if (controlValue < (LowerBound - EdgeFalloff))
            {
                return Source0.Evaluate(x, y);
            }
            else if (controlValue < (LowerBound + EdgeFalloff))
            {
                double lowerCurve = LowerBound - EdgeFalloff;
                double upperCurve = LowerBound + EdgeFalloff;
                double alpha = (controlValue - lowerCurve) / (upperCurve - lowerCurve);
                return Numerics.Functions.Lerp(
                    Source0.Evaluate(x, y), Source1.Evaluate(x, y), alpha);
            }
            else if (controlValue <= (UpperBound - EdgeFalloff))
            {
                return Source1.Evaluate(x, y);
            }
            else if (controlValue <= (UpperBound + EdgeFalloff))
            {
                double lowerCurve = UpperBound - EdgeFalloff;
                double upperCurve = UpperBound + EdgeFalloff;
                double alpha = (controlValue - lowerCurve) / (upperCurve - lowerCurve);
                return Numerics.Functions.Lerp(
                    Source1.Evaluate(x, y), Source0.Evaluate(x, y), alpha);
            }
            else
            {
                return Source0.Evaluate(x, y);
            }
        }

        public override double Evaluate(double x, double y, double z)
        {
            double controlValue = Control.Evaluate(x, y, z);

            if (controlValue < (LowerBound - EdgeFalloff))
            {
                return Source0.Evaluate(x, y, z);
            }
            else if (controlValue < (LowerBound + EdgeFalloff))
            {
                double lowerCurve = LowerBound - EdgeFalloff;
                double upperCurve = LowerBound + EdgeFalloff;
                double alpha = (controlValue - lowerCurve) / (upperCurve - lowerCurve);
                return Numerics.Functions.SmoothStep(
                    Source0.Evaluate(x, y, z), Source1.Evaluate(x, y, z), alpha);
            }
            else if (controlValue < (UpperBound - EdgeFalloff))
            {
                return Source1.Evaluate(x, y, z);
            }
            else if (controlValue < (UpperBound + EdgeFalloff))
            {
                double lowerCurve = UpperBound - EdgeFalloff;
                double upperCurve = UpperBound + EdgeFalloff;
                double alpha = (controlValue - lowerCurve) / (upperCurve - lowerCurve);
                return Numerics.Functions.SmoothStep(
                    Source1.Evaluate(x, y, z), Source0.Evaluate(x, y, z), alpha);
            }
            else
            {
                return Source0.Evaluate(x, y, z);
            }
        }

        public override double Evaluate(double x, double y, double z, double w)
        {
            double controlValue = Control.Evaluate(x, y, z, w);

            if (controlValue < (LowerBound - EdgeFalloff))
            {
                return Source0.Evaluate(x, y, z, w);
            }
            else if (controlValue < (LowerBound + EdgeFalloff))
            {
                double lowerCurve = LowerBound - EdgeFalloff;
                double upperCurve = LowerBound + EdgeFalloff;
                double alpha = (controlValue - lowerCurve) / (upperCurve - lowerCurve);
                return Numerics.Functions.SmoothStep(
                    Source0.Evaluate(x, y, z, w), Source1.Evaluate(x, y, z, w), alpha);
            }
            else if (controlValue < (UpperBound - EdgeFalloff))
            {
                return Source1.Evaluate(x, y, z, w);
            }
            else if (controlValue < (UpperBound + EdgeFalloff))
            {
                double lowerCurve = UpperBound - EdgeFalloff;
                double upperCurve = UpperBound + EdgeFalloff;
                double alpha = (controlValue - lowerCurve) / (upperCurve - lowerCurve);
                return Numerics.Functions.SmoothStep(
                    Source1.Evaluate(x, y, z, w), Source0.Evaluate(x, y, z, w), alpha);
            }
            else
            {
                return Source0.Evaluate(x, y, z, w);
            }
        }

        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            double controlValue = Control.Evaluate(x, y, z, w, v, u);

            if (controlValue < (LowerBound - EdgeFalloff))
            {
                return Source0.Evaluate(x, y, z, w, v, u);
            }
            else if (controlValue < (LowerBound + EdgeFalloff))
            {
                double lowerCurve = LowerBound - EdgeFalloff;
                double upperCurve = LowerBound + EdgeFalloff;
                double alpha = (controlValue - lowerCurve) / (upperCurve - lowerCurve);
                return Numerics.Functions.SmoothStep(
                    Source0.Evaluate(x, y, z, w, v, u), Source1.Evaluate(x, y, z, w, v, u), alpha);
            }
            else if (controlValue < (UpperBound - EdgeFalloff))
            {
                return Source1.Evaluate(x, y, z, w, v, u);
            }
            else if (controlValue < (UpperBound + EdgeFalloff))
            {
                double lowerCurve = UpperBound - EdgeFalloff;
                double upperCurve = UpperBound + EdgeFalloff;
                double alpha = (controlValue - lowerCurve) / (upperCurve - lowerCurve);
                return Numerics.Functions.SmoothStep(
                    Source1.Evaluate(x, y, z, w, v, u), Source0.Evaluate(x, y, z, w, v, u), alpha);
            }
            else
            {
                return Source0.Evaluate(x, y, z, w, v, u);
            }
        }
    }
}
