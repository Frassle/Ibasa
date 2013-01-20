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


        #region Renderer State management

        /// <summary>This function enables a feature of the OpenAL driver. There are no capabilities defined in OpenAL 1.1 to be used with this function, but it may be used by an extension.</summary>
        /// <param name="capability">The name of a capability to enable.</param>
        [DllImport("openal32.dll", EntryPoint = "alEnable", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void Enable(int capability);
        //AL_API void AL_APIENTRY alEnable( ALenum capability );

        /// <summary>This function disables a feature of the OpenAL driver.</summary>
        /// <param name="capability">The name of a capability to disable.</param>
        [DllImport("openal32.dll", EntryPoint = "alDisable", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void Disable(int capability);
        // AL_API void AL_APIENTRY alDisable( ALenum capability ); 

        /// <summary>This function returns a boolean indicating if a specific feature is enabled in the OpenAL driver.</summary>
        /// <param name="capability">The name of a capability to enable.</param>
        /// <returns>True if enabled, False if disabled.</returns>
        [DllImport("openal32.dll", EntryPoint = "alIsEnabled", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern bool IsEnabled(int capability);
        // AL_API ALboolean AL_APIENTRY alIsEnabled( ALenum capability ); 

        #endregion Renderer State management

        #region State retrieval

        [DllImport("openal32.dll", EntryPoint = "alGetString", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        private static extern IntPtr alGetString(int param); // accepts the enums AlError, AlContextString
        // AL_API const ALchar* AL_APIENTRY alGetString( ALenum param );

        /// <summary>This function retrieves an OpenAL string property.</summary>
        /// <param name="param">The property to be returned: Vendor, Version, Renderer and Extensions</param>
        /// <returns>Returns a pointer to a null-terminated string.</returns>
        public static string GetString(int param)
        {
            return OpenAL.MarshalString(alGetString(param));
        }

        /* no functions return more than 1 result ..
        // AL_API void AL_APIENTRY alGetBooleanv( ALenum param, ALboolean* buffer );
        // AL_API void AL_APIENTRY alGetIntegerv( ALenum param, ALint* buffer );
        // AL_API void AL_APIENTRY alGetFloatv( ALenum param, ALfloat* buffer );
        // AL_API void AL_APIENTRY alGetDoublev( ALenum param, ALdouble* buffer );
        */

        /* disabled due to no token using it
        /// <summary>This function returns a boolean OpenAL state.</summary>
        /// <param name="param">the state to be queried: AL_DOPPLER_FACTOR, AL_SPEED_OF_SOUND, AL_DISTANCE_MODEL</param>
        /// <returns>The boolean state described by param will be returned.</returns>
        [DllImport( "openal32.dll", EntryPoint = "alGetBoolean", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl ), SuppressUnmanagedCodeSecurity( )]
        public static extern bool Get( ALGetBoolean param );
        // AL_API ALboolean AL_APIENTRY alGetBoolean( ALenum param );
        */

        /// <summary>This function returns an integer OpenAL state.</summary>
        /// <param name="param">the state to be queried: DistanceModel.</param>
        /// <returns>The integer state described by param will be returned.</returns>
        [DllImport("openal32.dll", EntryPoint = "alGetInteger", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern int GetInteger(int param);
        // AL_API ALint AL_APIENTRY alGetInteger( ALenum param );

        /// <summary>This function returns a floating-point OpenAL state.</summary>
        /// <param name="param">the state to be queried: DopplerFactor, SpeedOfSound.</param>
        /// <returns>The floating-point state described by param will be returned.</returns>
        [DllImport("openal32.dll", EntryPoint = "alGetFloat", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern float GetFloat(int param);
        // AL_API ALfloat AL_APIENTRY alGetFloat( ALenum param );

        /* disabled due to no token using it
        /// <summary>This function returns a double-precision floating-point OpenAL state.</summary>
        /// <param name="param">the state to be queried: AL_DOPPLER_FACTOR, AL_SPEED_OF_SOUND, AL_DISTANCE_MODEL</param>
        /// <returns>The double value described by param will be returned.</returns>
        [DllImport( "openal32.dll", EntryPoint = "alGetDouble", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl ), SuppressUnmanagedCodeSecurity( )]
        public static extern double Get( ALGetDouble param );
        // AL_API ALdouble AL_APIENTRY alGetDouble( ALenum param );
        */

        /// <summary>Error support. Obtain the most recent error generated in the AL state machine. When an error is detected by AL, a flag is set and the error code is recorded. Further errors, if they occur, do not affect this recorded code. When alGetError is called, the code is returned and the flag is cleared, so that a further error will again record its code.</summary>
        /// <returns>The first error that occured. can be used with AL.GetString. Returns an Alenum representing the error state. When an OpenAL error occurs, the error state is set and will not be changed until the error state is retrieved using alGetError. Whenever alGetError is called, the error state is cleared and the last state (the current state when the call was made) is returned. To isolate error detection to a specific portion of code, alGetError should be called before the isolated section to clear the current error state.</returns>
        [DllImport("openal32.dll", EntryPoint = "alGetError", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern AlError GetError();
        // AL_API ALenum AL_APIENTRY alGetError( void );

        #endregion State retrieval


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