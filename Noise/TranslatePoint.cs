using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Noise
{
    public sealed class TranslatePoint<T> : Module<T>
    {
        #region Source
        private Module<T> property_Source;
        /// <summary>
        /// Source module.
        /// </summary>
        public Module<T> Source
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
        
        #region ControlX
        private Module<double> property_ControlX;
        /// <summary>
        /// Control for X Translatement.
        /// </summary>
        public Module<double> ControlX
        {
            get { return property_ControlX; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_ControlX = value;
            }
        }
        #endregion

        #region ControlY
        private Module<double> property_ControlY;
        /// <summary>
        /// Control for Y Translatement.
        /// </summary>
        public Module<double> ControlY
        {
            get { return property_ControlY; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_ControlY = value;
            }
        }
        #endregion

        #region ControlZ
        private Module<double> property_ControlZ;
        /// <summary>
        /// Control for Z Translatement.
        /// </summary>
        public Module<double> ControlZ
        {
            get { return property_ControlZ; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_ControlZ = value;
            }
        }
        #endregion

        #region ControlW
        private Module<double> property_ControlW;
        /// <summary>
        /// Control for W Translatement.
        /// </summary>
        public Module<double> ControlW
        {
            get { return property_ControlW; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_ControlW = value;
            }
        }
        #endregion

        #region ControlV
        private Module<double> property_ControlV;
        /// <summary>
        /// Control for V Translatement.
        /// </summary>
        public Module<double> ControlV
        {
            get { return property_ControlV; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_ControlV = value;
            }
        }
        #endregion

        #region ControlU
        private Module<double> property_ControlU;
        /// <summary>
        /// Control for U Translatement.
        /// </summary>
        public Module<double> ControlU
        {
            get { return property_ControlU; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_ControlU = value;
            }
        }
        #endregion

        public TranslatePoint(Module<T> source, Module<double> controlX, Module<double> controlY, Module<double> controlZ, Module<double> controlW, Module<double> controlV, Module<double> controlU)
        {
            Source = source;
            ControlX = controlX;
            ControlY = controlY;
            ControlZ = controlZ;
            ControlW = controlW;
            ControlV = controlV;
            ControlU = controlU;
        }
        public TranslatePoint(Module<T> source, double xTranslate = 0.0, double yTranslate = 0.0, double zTranslate = 0.0, double wTranslate = 0.0, double vTranslate = 0.0, double uTranslate = 0.0)
        {
            Source = source;
            ControlX = new Constant<double>(xTranslate);
            ControlY = new Constant<double>(yTranslate);
            ControlZ = new Constant<double>(zTranslate);
            ControlW = new Constant<double>(wTranslate);
            ControlV = new Constant<double>(vTranslate);
            ControlU = new Constant<double>(uTranslate);
        }

        public override T Evaluate(double x)
        {
            double xTranslate = x + ControlX.Evaluate(x);
            return Source.Evaluate(xTranslate);
        }

        public override T Evaluate(double x, double y)
        {
            double xTranslate = x + ControlX.Evaluate(x, y);
            double yTranslate = y + ControlY.Evaluate(x, y);
            return Source.Evaluate(xTranslate, yTranslate);
        }

        public override T Evaluate(double x, double y, double z)
        {
            double xTranslate = x + ControlX.Evaluate(x, y, z);
            double yTranslate = y + ControlY.Evaluate(x, y, z);
            double zTranslate = z + ControlZ.Evaluate(x, y, z);
            return Source.Evaluate(xTranslate, yTranslate, zTranslate);
        }

        public override T Evaluate(double x, double y, double z, double w)
        {
            double xTranslate = x + ControlX.Evaluate(x, y, z, w);
            double yTranslate = y + ControlY.Evaluate(x, y, z, w);
            double zTranslate = z + ControlZ.Evaluate(x, y, z, w);
            double wTranslate = w + ControlW.Evaluate(x, y, z, w);
            return Source.Evaluate(xTranslate, yTranslate, zTranslate, wTranslate);
        }

        public override T Evaluate(double x, double y, double z, double w, double v, double u)
        {
            double xTranslate = x + ControlX.Evaluate(x, y, z, w, v, u);
            double yTranslate = y + ControlY.Evaluate(x, y, z, w, v, u);
            double zTranslate = z + ControlZ.Evaluate(x, y, z, w, v, u);
            double wTranslate = w + ControlW.Evaluate(x, y, z, w, v, u);
            double vTranslate = v + ControlZ.Evaluate(x, y, z, w, v, u);
            double uTranslate = u + ControlW.Evaluate(x, y, z, w, v, u);
            return Source.Evaluate(xTranslate, yTranslate, zTranslate, wTranslate, vTranslate, uTranslate);
        }
    }
}
