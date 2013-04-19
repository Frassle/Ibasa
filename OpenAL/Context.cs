using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Security;

namespace Ibasa.OpenAL
{
    public struct Context : IEquatable<Context>
    {
        public static readonly Context Null = new Context();

        public IntPtr Handle { get; private set; }

        public Context(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Context(Device device)
            : this()
        {
            if (device == Device.Null)
                throw new ArgumentNullException("device");

            unsafe
            {
                Handle = Alc.CreateContext(device.Handle, null);
                AlHelper.GetAlcError(Alc.GetError(device.Handle));
            }
        }

        public Context(Device device, Dictionary<int, int> attributes)
            : this()
        {
            if (device == Device.Null)
                throw new ArgumentNullException("device");

            unsafe
            {
                var attribs_length = attributes == null ? 0 : attributes.Count * 2;
                int* attribs = stackalloc int[attribs_length + 1];

                if (attributes != null)
                {
                    int index = 0;
                    foreach (var pair in attributes)
                    {
                        attribs[index++] = pair.Key;
                        attribs[index++] = pair.Value;
                    }
                    attribs[attribs_length] = 0;
                }
                else
                {
                    attribs = null;
                }

                Handle = Alc.CreateContext(device.Handle, attribs);
                AlHelper.GetAlcError(Alc.GetError(device.Handle));
            }
        }

        public void Process()
        {
            AlHelper.ThrowNullException(Handle);
            Alc.ProcessContext(Handle);
        }

        public void Suspend()
        {
            AlHelper.ThrowNullException(Handle);
            Alc.SuspendContext(Handle);
        }

        public static bool MakeContextCurrent(Context context)
        {
            return Alc.MakeContextCurrent(context.Handle) != 0;
        }

        public void Destroy()
        {
            AlHelper.ThrowNullException(Handle);
            Alc.DestroyContext(Handle);
        }

        public static Context CurrentContext
        {
            get
            {
                return new Context(Alc.GetCurrentContext());
            }
        }

        public static bool IsExtensionPresent(string extension)
        {
            unsafe
            {
                int length = Encoding.ASCII.GetByteCount(extension);
                byte* chars = stackalloc byte[length];
                AlHelper.StringToAnsi(extension, chars, length);

                return Al.IsExtensionPresent(chars);
            }
        }

        public static int GetEnumValue(string enumname)
        {
            unsafe
            {
                int length = Encoding.ASCII.GetByteCount(enumname);
                byte* chars = stackalloc byte[length];
                AlHelper.StringToAnsi(enumname, chars, length);

                return Al.GetEnumValue(chars);
            }
        }

        public Device Device
        {
            get
            {
                AlHelper.ThrowNullException(Handle);
                return new Device(Alc.GetContextsDevice(Handle));
            }
        }

        public static float DopplerFactor
        {
            get
            {
                return Al.GetFloat(Al.DOPPLER_FACTOR);
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
                return Al.GetFloat(Al.SPEED_OF_SOUND);
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
                unsafe
                {
                    return AlHelper.MarshalString(Al.GetString(Al.VERSION));
                }
            }
        }

        public static string Vendor
        {
            get
            {
                unsafe
                {
                    return AlHelper.MarshalString(Al.GetString(Al.VENDOR));
                }
            }
        }

        public static string Renderer
        {
            get
            {
                unsafe
                {
                    return AlHelper.MarshalString(Al.GetString(Al.RENDERER));
                }
            }
        }

        public static string[] Extensions
        {
            get
            {
                unsafe
                {
                    byte* extensions = Al.GetString(Al.EXTENSIONS);

                    if (extensions == null)
                    {
                        return new string[0];
                    }
                    else
                    {
                        return AlHelper.MarshalString(extensions).Split();
                    }
                }
            }
        }

        public override int GetHashCode()
        {
            AlHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            AlHelper.ThrowNullException(Handle);
            if (obj is Context)
            {
                return Equals((Context)obj);
            }
            return false;
        }

        public bool Equals(Context other)
        {
            AlHelper.ThrowNullException(Handle);
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
            AlHelper.ThrowNullException(Handle);
            return string.Format("Context: {0}", Handle.ToString());
        }
    }
}