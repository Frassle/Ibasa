using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio
{
    public static class Context
    {
        public static Device Device { get; private set; }

        static OpenTK.ContextHandle Handle;

        public static void Create(Device device)
        {
            int[] attriblist = null;

            Device = device;
            Handle = OpenTK.Audio.OpenAL.Alc.CreateContext(Device.Handle, attriblist);
            OpenTK.Audio.OpenAL.Alc.MakeContextCurrent(Handle);
            ThrowIfError();
        }

        public static void Destroy()
        {
            OpenTK.Audio.OpenAL.Alc.MakeContextCurrent(OpenTK.ContextHandle.Zero);
            OpenTK.Audio.OpenAL.Alc.DestroyContext(Handle);
            Handle = OpenTK.ContextHandle.Zero;
            Device = null;
        }

        internal static void ThrowIfError()
        {
            Device.ThrowIfError();
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
    }
}
