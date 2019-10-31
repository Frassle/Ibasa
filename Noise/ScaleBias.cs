using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Noise
{
    /// <summary>
    /// Evaluates (Source * Scale) + Bias.
    /// </summary>
    public sealed class ScaleBias : Module<double>
    {
        #region Scale
        private Module<double> property_Scale;
        /// <summary>
        /// Scale module.
        /// </summary>
        public Module<double> Scale
        {
            get { return property_Scale; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Scale = value;
            }
        }
        #endregion        
        
        #region Bias
        private Module<double> property_Bias;
        /// <summary>
        /// Bias module.
        /// </summary>
        public Module<double> Bias
        {
            get { return property_Bias; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Bias = value;
            }
        }
        #endregion

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

        public ScaleBias(Module<double> source, Module<double> scale, Module<double> bias)
        {
            Source = source;
            Scale = scale;
            Bias = bias;
        }
        public ScaleBias(Module<double> source, Module<double> scale)
        {
            Source = source;
            Scale = scale;
            Bias = new Constant<double>(0.0);
        }
        public ScaleBias(Module<double> source, double scale = 1.0, double bias = 0.0)
        {
            Source = source;
            Scale = new Constant<double>(scale);
            Bias = new Constant<double>(bias);
        }

        public override double Evaluate(double x)
        {
            return (Source.Evaluate(x) * Scale.Evaluate(x)) + Bias.Evaluate(x);
        }
        public override double Evaluate(double x, double y)
        {
            return (Source.Evaluate(x, y) * Scale.Evaluate(x, y)) + Bias.Evaluate(x, y);
        }
        public override double Evaluate(double x, double y, double z)
        {
            return (Source.Evaluate(x, y, z) * Scale.Evaluate(x, y, z)) + Bias.Evaluate(x, y, z);
        }
        public override double Evaluate(double x, double y, double z, double w)
        {
            return (Source.Evaluate(x, y, z, w) * Scale.Evaluate(x, y, z, w)) + Bias.Evaluate(x, y, z, w);
        }
        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return (Source.Evaluate(x, y, z, w, v, u) * Scale.Evaluate(x, y, z, w, v, u)) + Bias.Evaluate(x, y, z, w, v, u);
        }
    }
}
