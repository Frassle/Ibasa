﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio
{
    public struct Context
    {
        internal IntPtr Handle { get; private set; }

        public static readonly Context Null = new Context(IntPtr.Zero);

        private Context(IntPtr handle) : this()
        {
            Handle = handle;
        }

        public static Context Create(Device device)
        {
            int[] attriblist = null;

            var handle = OpenTK.Audio.OpenAL.Alc.CreateContext(device.Handle, attriblist);
            ThrowIfError();
            return new Context(handle);
        }

        public static bool MakeContextCurrent(Context context)
        {
            return OpenTK.Audio.OpenAL.Alc.MakeContextCurrent(context.Handle);
        }

        public static void Destroy(Context context)
        {
            OpenTK.Audio.OpenAL.Alc.DestroyContext(context.Handle);
        }

        internal static void ThrowIfError()
        {
            //Device.ThrowIfError();
        }

        public static Context CurrentContext
        {
            get
            {
                return new Context(OpenTK.Audio.OpenAL.Alc.GetCurrentContext());
            }
        }

        public static IntPtr Device
        {
            get
            {
                return OpenTK.Audio.OpenAL.Alc.GetContextsDevice(OpenTK.Audio.OpenAL.Alc.GetCurrentContext());
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
