using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Audio
{
    public static class Listener
    {
        public static Point3f Position
        {
            get
            {
                float x, y, z;
                OpenTK.Audio.OpenAL.AL.GetListener(OpenTK.Audio.OpenAL.ALListener3f.Position, out x, out y, out z);
                return new Point3f(x, y, z);
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Listener(OpenTK.Audio.OpenAL.ALListener3f.Position, value.X, value.Y, value.Z);
            }
        }

        public static Vector3f Velocity
        {
            get
            {
                float x, y, z;
                OpenTK.Audio.OpenAL.AL.GetListener(OpenTK.Audio.OpenAL.ALListener3f.Velocity, out x, out y, out z);
                return new Vector3f(x, y, z);
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Listener(OpenTK.Audio.OpenAL.ALListener3f.Velocity, value.X, value.Y, value.Z);
            }
        }

        public static float Gain
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetListener(OpenTK.Audio.OpenAL.ALListenerf.Gain, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Listener(OpenTK.Audio.OpenAL.ALListenerf.Gain, value);
            }
        }

        public static float EfxMetersPerUnit
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetListener(OpenTK.Audio.OpenAL.ALListenerf.EfxMetersPerUnit, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Listener(OpenTK.Audio.OpenAL.ALListenerf.EfxMetersPerUnit, value);
            }
        }

        public static Quaternionf Orientation
        {
            get
            {
                OpenTK.Vector3 at, up;
                OpenTK.Audio.OpenAL.AL.GetListener(OpenTK.Audio.OpenAL.ALListenerfv.Orientation, out at, out up);
                return Quaternion.FromOrientation(new Vector3f(at.X, at.Y, at.Z), new Vector3f(up.X, up.Y, up.Z));
            }
            set
            {
                Vector3f at = Quaternion.Transform(Vector3f.UnitZ, value);
                Vector3f up = Quaternion.Transform(Vector3f.UnitY, value);

                OpenTK.Vector3 tk_at = new OpenTK.Vector3(at.X, at.Y, at.Z);
                OpenTK.Vector3 tk_up = new OpenTK.Vector3(up.X, up.Y, up.Z);

                OpenTK.Audio.OpenAL.AL.Listener(OpenTK.Audio.OpenAL.ALListenerfv.Orientation, ref tk_at, ref tk_up);
            }
        }
    }
}
