using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise.Colors
{
    public sealed class Sobel : Module<Color>
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

        public Sobel(Module<double> source, double step)
        {
            Source = source;
            Step = step;
        }

        public override Color Evaluate(double x)
        {
            double dx = 0.0;
            double p;

            p = Source.Evaluate(x - Step);
            dx += p;

            p = Source.Evaluate(x + Step);
            dx -= p;

            return new Color(dx, 0.0, 0.0);
        }

        public override Color Evaluate(double x, double y)
        {
            double dx = 0.0;
            double dy = 0.0;
            double p;

            p = Source.Evaluate(x - Step, y - Step);
            dx -= p;
            dy -= p;

            p = Source.Evaluate(x, y - Step);
            dy -= 2 * p;

            p = Source.Evaluate(x + Step, y - Step);
            dx += p;
            dy -= p;

            p = Source.Evaluate(x - Step, y);
            dx -= 2 * p;

            p = Source.Evaluate(x + Step, y);
            dx += 2 * p;

            p = Source.Evaluate(x - Step, y + Step);
            dx -= p;
            dy += p;

            p = Source.Evaluate(x, y + Step);
            dy += 2 * p;

            p = Source.Evaluate(x + Step, y + Step);
            dx += p;
            dy += p;

            return new Color(dx, dy, Functions.Sqrt(dx * dx + dy * dy));
        }
    }
}
