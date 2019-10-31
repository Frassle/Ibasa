using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise.Colors
{
    public sealed class Transform : Module<Color>
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

        #region Matrix
        public Matrix4x4 Matrix { get; set; }
        #endregion

        public Transform(Module<Color> source, Matrix4x4 matrix)
        {
            Source = source;
            Matrix = matrix;
        }

        public override Color Evaluate(double x)
        {
            return Matrix4x4.Multiply(Matrix, Source.Evaluate(x));
        }

        public override Color Evaluate(double x, double y)
        {
            return Matrix4x4.Multiply(Matrix, Source.Evaluate(x, y));
        }

        public override Color Evaluate(double x, double y, double z)
        {
            return Matrix4x4.Multiply(Matrix, Source.Evaluate(x, y, z));
        }

        public override Color Evaluate(double x, double y, double z, double w)
        {
            return Matrix4x4.Multiply(Matrix, Source.Evaluate(x, y, z, w));
        }

        public override Color Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return Matrix4x4.Multiply(Matrix, Source.Evaluate(x, y, z, w, v, u));
        }
    }
}
