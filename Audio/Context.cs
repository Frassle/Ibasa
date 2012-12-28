using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio
{
    public sealed class Context : IDisposable
    {
        public Device Device { get; private set; }

        OpenTK.ContextHandle Handle;

        public Context(Device device)
        {
            int[] attriblist = null;

            Device = device;
            Handle = OpenTK.Audio.OpenAL.Alc.CreateContext(Device.Handle, attriblist);
            Device.GetError();
        }

        ~Context()
        {
            Dispose();
        }

        public void Dispose()
        {
            OpenTK.Audio.OpenAL.Alc.DestroyContext(Handle);
            GC.SuppressFinalize(this);
        }

        public static void MakeContextCurrent(Context context)
        {
            if (context == null)
            {
                OpenTK.Audio.OpenAL.Alc.MakeContextCurrent(OpenTK.ContextHandle.Zero);
            }
            else
            {
                if (OpenTK.Audio.OpenAL.Alc.MakeContextCurrent(context.Handle))
                {
                    context.Device.GetError();
                }
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
    }
}
