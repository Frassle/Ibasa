using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise.Colors
{
    public sealed class Convolution : Module<Color>
    {
        #region Source
        private Module<Color> property_Source;
        /// <summary>
        /// Source module.
        /// </summary>
        public Module<Color> Source
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

        #region Kernel
        private Matrix property_Kernel;
        public Matrix Kernel
        {
            get { return property_Kernel; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");

                property_Kernel = value;
            }
        }
        #endregion

        #region Step
        private double property_Step;
        public double Step
        {
            get { return property_Step; }
            set
            {
                if (value <= 0.0)
                    throw new global::System.ArgumentException("value", "The value for this property is zero or less.");

                property_Step = value;
            }
        }
        #endregion

        public Convolution(Module<Color> source, Matrix kernel, double step)
        {
            Source = source;
            Kernel = kernel;
            Step = step;
        }

        public override Color Evaluate(double x)
        {
            Color result = new Color();
            for (int i = 0; i < Kernel.Rows.Count; ++i)
            {
                result += Source.Evaluate(x - i) * Kernel[0, i];
            }
            return result;
        }

        public override Color Evaluate(double x, double y)
        {
            Color result = new Color();
            for (int i = 0; i < Kernel.Rows.Count; ++i)
            {
                for (int j = 0; j < Kernel.Columns.Count; ++j)
                {
                    result += Source.Evaluate(x - j, y - i) * Kernel[i, j];
                }
            }
            return result;
        }
    }
}
