using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.OpenAL
{
    public static class Listener
    {
        public static Point3f Position
        {
            get
            {
                unsafe
                {
                    float x, y, z;
                    Al.GetListener3f(Al.POSITION, &x, &y, &z);
                    return new Point3f(x, y, z);
                }
            }
            set
            {
                Al.Listener3f(Al.POSITION, value.X, value.Y, value.Z);
            }
        }

        public static Vector3f Velocity
        {
            get
            {
                unsafe
                {
                    float x, y, z;
                    Al.GetListener3f(Al.VELOCITY, &x, &y, &z);
                    return new Vector3f(x, y, z);
                }
            }
            set
            {
                Al.Listener3f(Al.VELOCITY, value.X, value.Y, value.Z);
            }
        }

        public static float Gain
        {
            get
            {
                unsafe
                {
                    float value;
                    Al.GetListenerf(Al.GAIN, &value);
                    return value;
                }
            }
            set
            {
                Al.Listenerf(Al.GAIN, value);
            }
        }

        public static Quaternionf Orientation
        {
            get
            {
                unsafe
                {
                    float* at_up = stackalloc float[6];
                    Al.GetListenerfv(Al.ORIENTATION, at_up);
                    return Quaternion.FromOrientation(
                        new Vector3f(at_up[0], at_up[1], at_up[2]),
                        new Vector3f(at_up[3], at_up[4], at_up[5]));
                }
            }
            set
            {
                unsafe
                {
                    Vector3f at = Quaternion.Transform(Vector3f.UnitZ, value);
                    Vector3f up = Quaternion.Transform(Vector3f.UnitY, value);

                    float* at_up = stackalloc float[6];

                    at_up[0] = at.X;
                    at_up[1] = at.Y;
                    at_up[2] = at.Z;
                    at_up[3] = at.X;
                    at_up[4] = at.Y;
                    at_up[5] = at.Z;

                    Al.Listenerfv(Al.ORIENTATION, at_up);
                }
            }
        }
    }
}
