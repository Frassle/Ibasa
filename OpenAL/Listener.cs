using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;

namespace Ibasa.OpenAL
{
    public static class Listener
    {
        public static Point3f Position
        {
            get
            {
                float x, y, z;
                Al.GetListener(AlListener3f.Position, out x, out y, out z);
                return new Point3f(x, y, z);
            }
            set
            {
                Al.Listener(AlListener3f.Position, value.X, value.Y, value.Z);
            }
        }

        public static Vector3f Velocity
        {
            get
            {
                float x, y, z;
                Al.GetListener(AlListener3f.Velocity, out x, out y, out z);
                return new Vector3f(x, y, z);
            }
            set
            {
                Al.Listener(AlListener3f.Velocity, value.X, value.Y, value.Z);
            }
        }

        public static float Gain
        {
            get
            {
                float value;
                Al.GetListener(AlListenerf.Gain, out value);
                return value;
            }
            set
            {
                Al.Listener(AlListenerf.Gain, value);
            }
        }

        public static float EfxMetersPerUnit
        {
            get
            {
                float value;
                Al.GetListener(AlListenerf.EfxMetersPerUnit, out value);
                return value;
            }
            set
            {
                Al.Listener(AlListenerf.EfxMetersPerUnit, value);
            }
        }

        public static Quaternionf Orientation
        {
            get
            {
                Vector3f at, up;
                Al.GetListener(AlListenerfv.Orientation, out at, out up);
                return Quaternion.FromOrientation(new Vector3f(at.X, at.Y, at.Z), new Vector3f(up.X, up.Y, up.Z));
            }
            set
            {
                Vector3f at = Quaternion.Transform(Vector3f.UnitZ, value);
                Vector3f up = Quaternion.Transform(Vector3f.UnitY, value);

                Al.Listener(AlListenerfv.Orientation, ref at, ref up);
            }
        }
    }
}
