using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ibasa;
using Ibasa.Numerics;

namespace Ibasa.Noise
{
    public sealed class RotatePoint<T> : Module<T>
    {
        Matrix4x4 matrix;

        double xy, yz, zx, xw, yw, zw;
        double RotationXY { get { return xy; } }
        double RotationYZ { get { return yz; } }
        double RotationZX { get { return zx; } }
        double RotationXW { get { return xw; } }
        double RotationYW { get { return yw; } }
        double RotationZW { get { return zw; } }

        public void SetRotation(double rotationXY = 0.0, double rotationYZ = 0.0, double rotationZX = 0.0,
            double rotationXW = 0.0, double rotationYW = 0.0, double rotationZW = 0.0)
        {
            xy = rotationXY;
            yz = rotationYZ;
            zx = rotationZX;
            xw = rotationXW;
            yw = rotationYW;
            zw = rotationZW;

            double cosXY = Functions.Cos(xy);
            double sinXY = Functions.Sin(xy);
            double cosYZ = Functions.Cos(yz);
            double sinYZ = Functions.Sin(yz);
            double cosZX = Functions.Cos(zx);
            double sinZX = Functions.Sin(zx);
            double cosXW = Functions.Cos(xw);
            double sinXW = Functions.Sin(xw);
            double cosYW = Functions.Cos(yw);
            double sinYW = Functions.Sin(yw);
            double cosZW = Functions.Cos(zw);
            double sinZW = Functions.Sin(zw);

            Matrix4x4 matXY = new Matrix4x4(
                cosXY, sinXY, 0, 0,
                -sinXY, cosXY, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
            Matrix4x4 matYZ = new Matrix4x4(
                1, 0, 0, 0,
                0, cosYZ, sinYZ, 0,
                0, -sinYZ, cosYZ, 0,
                0, 0, 0, 1);
            Matrix4x4 matZX = new Matrix4x4(
                cosZX, 0, -sinZX, 0,
                0, 1, 0, 0,
                sinZX, 0, cosZX, 0,
                0, 0, 0, 1);
            Matrix4x4 matXW = new Matrix4x4(
                cosXW, 0, 0, sinXW,
                0, 1, 0, 0,
                0, 0, 1, 0,
                -sinXW, 0, 0, cosXW);
            Matrix4x4 matYW = new Matrix4x4(
                1, 0, 0, 0,
                0, cosYW, 0, -sinYW,
                0, 0, 1, 0,
                0, sinYW, 0, cosYW);
            Matrix4x4 matZW = new Matrix4x4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, cosZW, -sinZW,
                0, 0, sinZW, cosZW);

            matrix = matXY * matYZ * matZX * matXW * matYW * matZW;

        }

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

        public RotatePoint(Module<T> source, 
            double rotationXY = 0.0, double rotationYZ = 0.0, double rotationZX = 0.0,
            double rotationXW = 0.0, double rotationYW = 0.0, double rotationZW = 0.0)
        {
            Source = source;
            SetRotation(rotationXY, rotationYZ, rotationZX, rotationXW, rotationYW, rotationZW);
        }

        public override T Evaluate(double x)
        {
            double rx = (x * matrix.M11) + matrix.M41;

            return Source.Evaluate(rx);
        }

        public override T Evaluate(double x, double y)
        {
            double rx = (x * matrix.M11) + (y * matrix.M21) + matrix.M41;
            double ry = (x * matrix.M12) + (y * matrix.M22) + matrix.M42;

            return Source.Evaluate(rx, ry);
        }

        public override T Evaluate(double x, double y, double z)
        {
            double rx = (x * matrix.M11) + (y * matrix.M21) + (z * matrix.M31) + matrix.M41;
            double ry = (x * matrix.M12) + (y * matrix.M22) + (z * matrix.M32) + matrix.M42;
            double rz = (x * matrix.M13) + (y * matrix.M23) + (z * matrix.M33) + matrix.M43;

            return Source.Evaluate(rx, ry, rz);
        }

        public override T Evaluate(double x, double y, double z, double w)
        {
            double rx = (x * matrix.M11) + (y * matrix.M21) + (z * matrix.M31) + (w * matrix.M41);
            double ry = (x * matrix.M12) + (y * matrix.M22) + (z * matrix.M32) + (w * matrix.M42);
            double rz = (x * matrix.M13) + (y * matrix.M23) + (z * matrix.M33) + (w * matrix.M43);
            double rw = (x * matrix.M14) + (y * matrix.M24) + (z * matrix.M34) + (w * matrix.M44);

            return Source.Evaluate(rx, ry, rz, rw);
        }
    }
}
