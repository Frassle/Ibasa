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
        #region Context Management

        [DllImport("openal32.dll", EntryPoint = "alcCreateContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public unsafe static extern IntPtr CreateContext([In] IntPtr device, [In] int* attrlist);

        [DllImport("openal32.dll", EntryPoint = "alcMakeContextCurrent", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        private static extern bool MakeContextCurrent(IntPtr context);
        // ALC_API ALCboolean      ALC_APIENTRY alcMakeContextCurrent( ALCcontext *context );

        [DllImport("openal32.dll", EntryPoint = "alcProcessContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        private static extern void ProcessContext(IntPtr context);
        // ALC_API void            ALC_APIENTRY alcProcessContext( ALCcontext *context );

        [DllImport("openal32.dll", EntryPoint = "alcSuspendContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        private static extern void SuspendContext(IntPtr context);
        // ALC_API void            ALC_APIENTRY alcSuspendContext( ALCcontext *context );

        [DllImport("openal32.dll", EntryPoint = "alcDestroyContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        private static extern void DestroyContext(IntPtr context);
        // ALC_API void            ALC_APIENTRY alcDestroyContext( ALCcontext *context );

        [DllImport("openal32.dll", EntryPoint = "alcGetCurrentContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        private static extern IntPtr GetCurrentContext();
        // ALC_API ALCcontext *    ALC_APIENTRY alcGetCurrentContext( void );

        [DllImport("openal32.dll", EntryPoint = "alcGetContextsDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        private static extern IntPtr GetContextsDevice(IntPtr context);
        // ALC_API ALCdevice*      ALC_APIENTRY alcGetContextsDevice( ALCcontext *context );

        #endregion Context Management

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
                Handle = CreateContext(device.Handle, null);
                device.GetError();
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

                Handle = CreateContext(device.Handle, attribs);
                device.GetError();
            }
        }

        public void Process()
        {
            OpenAL.ThrowNullException(Handle);
            ProcessContext(Handle);
        }

        public void Suspend()
        {
            OpenAL.ThrowNullException(Handle);
            SuspendContext(Handle);
        }

        public static void MakeContextCurrent(Context context)
        {
            if (!MakeContextCurrent(context.Handle))
            {
                context.GetDeviceError();
            }
        }

        public void Destroy()
        {
            OpenAL.ThrowNullException(Handle);
            DestroyContext(Handle);
        }

        internal void GetDeviceError()
        {
            OpenAL.GetError(GetContextsDevice(Handle));
        }

        public static Context CurrentContext
        {
            get
            {
                return new Context(GetCurrentContext());
            }
        }

        public static bool IsExtensionPresent(string extension)
        {
            return Al.IsExtensionPresent(extension);
        }

        public static int GetEnumValue(string enumname)
        {
            return Al.GetEnumValue(enumname);
        }

        public Device Device
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                return new Device(GetContextsDevice(Handle));
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