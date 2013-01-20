using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.OpenAL
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

            unsafe
            {
                Handle = Alc.CreateContext(device.Handle, null);
                device.ThrowError();
            }
        }

        public Context(Device device, Dictionary<int, int> attributes)
            : this()
        {
            if (device == default(Device))
            {
                throw new ArgumentNullException("device");
            }

            unsafe
            {
                int* attribs = stackalloc int[attributes.Count * 2];

                int index = 0;
                foreach(var pair in attributes)
                {
                    attribs[index++] = pair.Key;
                    attribs[index++] = pair.Value;
                }

                Handle = Alc.CreateContext(device.Handle, attribs);
                device.ThrowError();
            }
        }

        public void Process()
        {
            OpenAL.ThrowNullException(Handle);
            Alc.ProcessContext(Handle);
        }

        public void Suspend()
        {
            OpenAL.ThrowNullException(Handle);
            Alc.SuspendContext(Handle);
        }

        public static bool MakeContextCurrent(Context context)
        {
            return Alc.MakeContextCurrent(context.Handle);
        }

        public void Destroy()
        {
            OpenAL.ThrowNullException(Handle);
            Alc.DestroyContext(Handle);
        }

        public static Context CurrentContext
        {
            get
            {
                return new Context(Alc.GetCurrentContext());
            }
        }

        public Device Device
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                return new Device(Alc.GetContextsDevice(Handle));
            }
        }

        public static float DopplerFactor
        {
            get
            {
                return Al.Get(AlGetFloat.DopplerFactor);
            }
            set
            {
                Al.DopplerFactor(value);
            }
        }

        public static float SpeedOfSound
        {
            get
            {
                return Al.Get(AlGetFloat.SpeedOfSound);
            }
            set
            {
                Al.SpeedOfSound(value);
            }
        }

        public static string Version
        {
            get
            {
                return Al.Get(AlGetString.Version);
            }
        }

        public static string Vendor
        {
            get
            {
                return Al.Get(AlGetString.Vendor);
            }
        }

        public static string Renderer
        {
            get
            {
                return Al.Get(AlGetString.Renderer);
            }
        }

        public static string[] Extensions
        {
            get
            {
                var value = Al.Get(AlGetString.Extensions);
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