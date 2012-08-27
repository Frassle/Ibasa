using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Numerics.Geometry.Patches
{
    /// <summary>
    /// Quadratic bezier patch
    /// </summary>
    public sealed class Bezier
    {
        #region ControlPoints
        private Vector3d[] property_ControlPoints;
        /// <summary>
        /// The 9 control points for this bezier curve.
        /// </summary>
        public Vector3d[] ControlPoints
        {
            get { return property_ControlPoints; }
            set
            {
                global::System.Diagnostics.Contracts.Contract.Requires(value != null);
                global::System.Diagnostics.Contracts.Contract.Requires(value.Length == 9);
                property_ControlPoints = value;
            }
        }
        #endregion

        public Bezier(
            Vector3d a, Vector3d b, Vector3d c,
            Vector3d d, Vector3d e, Vector3d f,
            Vector3d g, Vector3d h, Vector3d i)
        {
            ControlPoints = new Vector3d[9];

            ControlPoints[0] = a;
            ControlPoints[1] = b;
            ControlPoints[2] = c;
            ControlPoints[3] = d;
            ControlPoints[4] = e;
            ControlPoints[5] = f;
            ControlPoints[6] = g;
            ControlPoints[7] = h;
            ControlPoints[8] = i;
        }

        public Bezier(IEnumerable<Vector3d> controlPoints)
        {
            ControlPoints = new Vector3d[9];

            IEnumerator<Vector3d> enumerator = controlPoints.GetEnumerator();
            for (int i = 0; i < 9; ++i)
            {
                if (!enumerator.MoveNext())
                    throw new ArgumentException("controlPoints must have 9 control points", "controlPoints");
                ControlPoints[i] = enumerator.Current;
            }
        }

        public PatchPoint Evaluate(double u, double v)
        {
            double Bu0 = (1.0 - u) * (1.0 - u);
            double Bu1 = 2.0 * u * (1.0 - u);
            double Bu2 = u * u;

            double Bv0 = (1.0 - v) * (1.0 - v);
            double Bv1 = 2.0 * v * (1.0 - v);
            double Bv2 = v * v;

            double Bdu0 = 2.0 * u - 2.0;
            double Bdu1 = 2.0 - 4.0 * u;
            double Bdu2 = 2.0 * u;

            double Bdv0 = 2.0 * v - 2.0;
            double Bdv1 = 2.0 - 4.0 * v;
            double Bdv2 = 2.0 * v;

            Vector3d position =
                (ControlPoints[0] * Bu0 + ControlPoints[1] * Bu1 + ControlPoints[2] * Bu2) * Bv0 +
                (ControlPoints[3] * Bu0 + ControlPoints[4] * Bu1 + ControlPoints[5] * Bu2) * Bv1 +
                (ControlPoints[6] * Bu0 + ControlPoints[7] * Bu1 + ControlPoints[8] * Bu2) * Bv2;

            Vector3d tangent = Vector.Normalize(
                (ControlPoints[0] * Bdu0 + ControlPoints[1] * Bdu1 + ControlPoints[2] * Bdu2) * Bv0 +
                (ControlPoints[3] * Bdu0 + ControlPoints[4] * Bdu1 + ControlPoints[5] * Bdu2) * Bv1 +
                (ControlPoints[6] * Bdu0 + ControlPoints[7] * Bdu1 + ControlPoints[8] * Bdu2) * Bv2);

            Vector3d bitangent = Vector.Normalize(
                (ControlPoints[0] * Bu0 + ControlPoints[1] * Bu1 + ControlPoints[2] * Bu2) * Bdv0 +
                (ControlPoints[3] * Bu0 + ControlPoints[4] * Bu1 + ControlPoints[5] * Bu2) * Bdv1 +
                (ControlPoints[6] * Bu0 + ControlPoints[7] * Bu1 + ControlPoints[8] * Bu2) * Bdv2);

            Vector3d normal = Vector.Cross(bitangent, tangent);

            return new PatchPoint(position, normal, tangent, bitangent);
        }
    }

    /// <summary>
    /// Cubic bezier patch
    /// </summary>
    public sealed class CubicBezier
    {
        #region ControlPoints
        private Vector3d[] property_ControlPoints;
        /// <summary>
        /// The 16 control points for this bezier curve.
        /// </summary>
        public Vector3d[] ControlPoints
        {
            get { return property_ControlPoints; }
            set
            {
                global::System.Diagnostics.Contracts.Contract.Requires(value != null);
                global::System.Diagnostics.Contracts.Contract.Requires(value.Length == 16);
                property_ControlPoints = value;
            }
        }
        #endregion

        public CubicBezier(
            Vector3d a, Vector3d b, Vector3d c, Vector3d d,
            Vector3d e, Vector3d f, Vector3d g, Vector3d h,
            Vector3d i, Vector3d j, Vector3d k, Vector3d l,
            Vector3d m, Vector3d n, Vector3d o, Vector3d p)
        {
            ControlPoints = new Vector3d[16];

            ControlPoints[0] = a;
            ControlPoints[1] = b;
            ControlPoints[2] = c;
            ControlPoints[3] = d;
            ControlPoints[4] = e;
            ControlPoints[5] = f;
            ControlPoints[6] = g;
            ControlPoints[7] = h;
            ControlPoints[8] = i;
            ControlPoints[9] = j;
            ControlPoints[10] = k;
            ControlPoints[11] = l;
            ControlPoints[12] = m;
            ControlPoints[13] = n;
            ControlPoints[14] = o;
            ControlPoints[15] = p;
        }

        public CubicBezier(IEnumerable<Vector3d> controlPoints)
        {
            ControlPoints = new Vector3d[16];

            IEnumerator<Vector3d> enumerator = controlPoints.GetEnumerator();
            for (int i = 0; i < 16; ++i)
            {
                if (!enumerator.MoveNext())
                    throw new ArgumentException("controlPoints must have 16 control points", "controlPoints");
                ControlPoints[i] = enumerator.Current;
            }
        }

        public PatchPoint Evaluate(double u, double v)
        {
            double Bu0 = ((1.0 - u) * (1.0 - u) * (1.0 - u));
            double Bu1 = (3.0 * u * (1.0 - u) * (1.0 - u));
            double Bu2 = (3.0 * u * u * (1.0 - u));
            double Bu3 = (u * u * u);

            double Bv0 = ((1.0 - v) * (1.0 - v) * (1.0 - v));
            double Bv1 = (3.0 * v * (1.0 - v) * (1.0 - v));
            double Bv2 = (3.0 * v * v * (1.0 - v));
            double Bv3 = (v * v * v);

            double Bdu0 = (-3.0 * (1.0 - u) * (1.0 - u));
            double Bdu1 = (-6.0 * u + 6.0 * u * u + 3.0 * (1.0 - u) * (1.0 - u));
            double Bdu2 = (6.0 * u - 9.0 * u * u);
            double Bdu3 = (3.0 * u * u);

            double Bdv0 = (-3.0 * (1.0 - v) * (1.0 - v));
            double Bdv1 = (-6.0 * v + 6.0 * v * v + 3.0 * (1.0 - v) * (1.0 - v));
            double Bdv2 = (6.0 * v - 9.0 * v * v);
            double Bdv3 = (3.0 * v * v);

            Vector3d position =
                (ControlPoints[0] * Bu0 + ControlPoints[1] * Bu1 + ControlPoints[2] * Bu2 + ControlPoints[3] * Bu3) * Bv0 +
                (ControlPoints[4] * Bu0 + ControlPoints[5] * Bu1 + ControlPoints[6] * Bu2 + ControlPoints[7] * Bu3) * Bv1 +
                (ControlPoints[8] * Bu0 + ControlPoints[9] * Bu1 + ControlPoints[10] * Bu2 + ControlPoints[11] * Bu3) * Bv2 +
                (ControlPoints[12] * Bu0 + ControlPoints[13] * Bu1 + ControlPoints[14] * Bu2 + ControlPoints[15] * Bu3) * Bv3;

            Vector3d tangent = Vector.Normalize(
                (ControlPoints[0] * Bdu0 + ControlPoints[1] * Bdu1 + ControlPoints[2] * Bdu2 + ControlPoints[3] * Bdu3) * Bv0 +
                (ControlPoints[4] * Bdu0 + ControlPoints[5] * Bdu1 + ControlPoints[6] * Bdu2 + ControlPoints[7] * Bdu3) * Bv1 +
                (ControlPoints[8] * Bdu0 + ControlPoints[9] * Bdu1 + ControlPoints[10] * Bdu2 + ControlPoints[11] * Bdu3) * Bv2 +
                (ControlPoints[12] * Bdu0 + ControlPoints[13] * Bdu1 + ControlPoints[14] * Bdu2 + ControlPoints[15] * Bdu3) * Bv3);

            Vector3d bitangent = Vector.Normalize(
                (ControlPoints[0] * Bu0 + ControlPoints[1] * Bu1 + ControlPoints[2] * Bu2 + ControlPoints[3] * Bu3) * Bdv0 +
                (ControlPoints[4] * Bu0 + ControlPoints[5] * Bu1 + ControlPoints[6] * Bu2 + ControlPoints[7] * Bu3) * Bdv1 +
                (ControlPoints[8] * Bu0 + ControlPoints[9] * Bu1 + ControlPoints[10] * Bu2 + ControlPoints[11] * Bu3) * Bdv2 +
                (ControlPoints[12] * Bu0 + ControlPoints[13] * Bu1 + ControlPoints[14] * Bu2 + ControlPoints[15] * Bu3) * Bdv3);

            Vector3d normal = Vector.Cross(bitangent, tangent);

            return new PatchPoint(position, normal, tangent, bitangent);
        }
    }
}
