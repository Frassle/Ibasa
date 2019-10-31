using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Noise
{
    public sealed class ScalePoint<T> : Module<T>
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
        /// Control for X Scalement.
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
        /// Control for Y Scalement.
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
        /// Control for Z Scalement.
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
        /// Control for W Scalement.
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
        /// Control for V Scalement.
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
        /// Control for U Scalement.
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

        public ScalePoint(Module<T> source, Module<double> controlX, Module<double> controlY, Module<double> controlZ, Module<double> controlW, Module<double> controlV, Module<double> controlU)
        {
            Source = source;
            ControlX = controlX;
            ControlY = controlY;
            ControlZ = controlZ;
            ControlW = controlW;
            ControlV = controlV;
            ControlU = controlU;
        }
        public ScalePoint(Module<T> source, double xScale = 1.0, double yScale = 1.0, double zScale = 1.0, double wScale = 1.0, double vScale = 1.0, double uScale = 1.0)
        {
            Source = source;
            ControlX = new Constant<double>(xScale);
            ControlY = new Constant<double>(yScale);
            ControlZ = new Constant<double>(zScale);
            ControlW = new Constant<double>(wScale);
            ControlV = new Constant<double>(vScale);
            ControlU = new Constant<double>(uScale);
        }

        public override T Evaluate(double x)
        {
            double xScale = x * ControlX.Evaluate(x);
            return Source.Evaluate(xScale);
        }

        public override T Evaluate(double x, double y)
        {
            double xScale = x * ControlX.Evaluate(x, y);
            double yScale = y * ControlY.Evaluate(x, y);
            return Source.Evaluate(xScale, yScale);
        }

        public override T Evaluate(double x, double y, double z)
        {
            double xScale = x * ControlX.Evaluate(x, y, z);
            double yScale = y * ControlY.Evaluate(x, y, z);
            double zScale = z * ControlZ.Evaluate(x, y, z);
            return Source.Evaluate(xScale, yScale, zScale);
        }

        public override T Evaluate(double x, double y, double z, double w)
        {
            double xScale = x * ControlX.Evaluate(x, y, z, w);
            double yScale = y * ControlY.Evaluate(x, y, z, w);
            double zScale = z * ControlZ.Evaluate(x, y, z, w);
            double wScale = w * ControlW.Evaluate(x, y, z, w);
            return Source.Evaluate(xScale, yScale, zScale, wScale);
        }

        public override T Evaluate(double x, double y, double z, double w, double v, double u)
        {
            double xScale = x * ControlX.Evaluate(x, y, z, w, v, u);
            double yScale = y * ControlY.Evaluate(x, y, z, w, v, u);
            double zScale = z * ControlZ.Evaluate(x, y, z, w, v, u);
            double wScale = w * ControlW.Evaluate(x, y, z, w, v, u);
            double vScale = v * ControlZ.Evaluate(x, y, z, w, v, u);
            double uScale = u * ControlW.Evaluate(x, y, z, w, v, u);
            return Source.Evaluate(xScale, yScale, zScale, wScale, vScale, uScale);
        }
    }
}
