using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Audio.OpenAL
{
    public struct Context : IEquatable<Context>
    {
        internal IntPtr Handle { get; private set; }

        public static readonly Context Null = new Context(IntPtr.Zero);

        private Context(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Context(Device device)
            : this()
        {
            if (device == default(Device))
            {
                throw new ArgumentNullException("device");
            }

            int[] attriblist = null;

            unsafe
            {
                fixed (int* attribs = attriblist)
                {
                    Handle = OpenTK.Audio.OpenAL.Alc.CreateContext(device.Handle, attribs);
                    Device.ThrowError();
                }
            }
        }

        public void Process()
        {
            OpenAL.ThrowNullException(Handle);
            OpenTK.Audio.OpenAL.Alc.ProcessContext(Handle);
        }

        public void Suspend()
        {
            OpenAL.ThrowNullException(Handle);
            OpenTK.Audio.OpenAL.Alc.SuspendContext(Handle);
        }

        public static bool MakeContextCurrent(Context context)
        {
            return OpenTK.Audio.OpenAL.Alc.MakeContextCurrent(context.Handle);
        }

        public static void Destroy(Context context)
        {
            OpenTK.Audio.OpenAL.Alc.DestroyContext(context.Handle);
        }

        public static Context CurrentContext
        {
            get
            {
                return new Context(OpenTK.Audio.OpenAL.Alc.GetCurrentContext());
            }
        }

        public static Device Device
        {
            get
            {
                return new Device(OpenTK.Audio.OpenAL.Alc.GetContextsDevice(OpenTK.Audio.OpenAL.Alc.GetCurrentContext()));
            }
        }

        public static float DopplerFactor
        {
            get
            {
                return OpenTK.Audio.OpenAL.AL.Get(OpenTK.Audio.OpenAL.ALGetFloat.DopplerFactor);
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.DopplerFactor(value);
            }
        }

        public static float SpeedOfSound
        {
            get
            {
                return OpenTK.Audio.OpenAL.AL.Get(OpenTK.Audio.OpenAL.ALGetFloat.SpeedOfSound);
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.SpeedOfSound(value);
            }
        }

        public static string Version
        {
            get
            {
                return OpenTK.Audio.OpenAL.AL.Get(OpenTK.Audio.OpenAL.ALGetString.Version);
            }
        }

        public static string Vendor
        {
            get
            {
                return OpenTK.Audio.OpenAL.AL.Get(OpenTK.Audio.OpenAL.ALGetString.Vendor);
            }
        }

        public static string Renderer
        {
            get
            {
                return OpenTK.Audio.OpenAL.AL.Get(OpenTK.Audio.OpenAL.ALGetString.Renderer);
            }
        }

        public static string[] Extensions
        {
            get
            {
                var value = OpenTK.Audio.OpenAL.AL.Get(OpenTK.Audio.OpenAL.ALGetString.Extensions);
                if (value == null)
                {
                    return null;
                }
                else
                {
                    return value.Split();
                }
            }
        }

        public override int GetHashCode()
        {
            OpenAL.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            OpenAL.ThrowNullException(Handle);
            if (obj is Context)
            {
                return Equals((Context)obj);
            }
            return false;
        }

        public bool Equals(Context other)
        {
            OpenAL.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Context left, Context right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Context left, Context right)
        {
            return left.Handle != right.Handle;
        }

        public override string ToString()
        {
            OpenAL.ThrowNullException(Handle);
            return Handle.ToString();
        }
    }
}