using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise.Colors
{
    public sealed class Channel : Module<double>
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

        #region SourceChannel
        private int property_SourceChannel;
        /// <summary>
        /// Source channel.
        /// </summary>
        public int SourceChannel
        {
            get { return property_SourceChannel; }
            set
            {
                if (value < 0)
                    throw new global::System.ArgumentException("value", "The value for this property is less than zero.");
                if (value > 3)
                    throw new global::System.ArgumentException("value", "The value for this property is greater than three.");

                property_SourceChannel = value;
            }
        }
        #endregion

        public Channel(Module<Color> source, int sourceChannel)
        {
            Source = source;
        }

        public override double Evaluate(double x)
        {
            return Source.Evaluate(x)[SourceChannel];
        }

        public override double Evaluate(double x, double y)
        {
            return Source.Evaluate(x, y)[SourceChannel];
        }

        public override double Evaluate(double x, double y, double z)
        {
            return Source.Evaluate(x, y, z)[SourceChannel];
        }

        public override double Evaluate(double x, double y, double z, double w)
        {
            return Source.Evaluate(x, y, z, w)[SourceChannel];
        }

        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return Source.Evaluate(x, y, z, w, v, u)[SourceChannel];
        }
    }
}
