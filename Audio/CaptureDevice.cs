using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio
{
    public sealed class CaptureDevice : IDisposable
    {
        public string Name { get; private set; }
        public IntPtr Handle { get; private set; }

        internal CaptureDevice(string name, int frequency, OpenTK.Audio.OpenAL.ALFormat format, int buffersize)
        {
            Handle = OpenTK.Audio.OpenAL.Alc.CaptureOpenDevice(Name, frequency, format, buffersize);
            if (Handle == IntPtr.Zero)
            {
                throw new AudioException(string.Format("OpenDevice({0}) failed.", name));
            }

            Name = OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.AlcGetString.CaptureDeviceSpecifier);
        }

        ~CaptureDevice()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!OpenTK.Audio.OpenAL.Alc.CaptureCloseDevice(Handle))
            {
                throw new AudioException(string.Format("CaptureCloseDevice({0}) failed.", Name));
            }
            else
            {
                GC.SuppressFinalize(this);
            }
        }

        public int CaptureSamples
        {
            get
            {
                return OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.AlcGetInteger.CaptureSamples);
            }
        }

        public void Start()
        {
            OpenTK.Audio.OpenAL.Alc.CaptureStart(Handle);
        }

        public void Stop()
        {
            OpenTK.Audio.OpenAL.Alc.CaptureStop(Handle);
        }

        public void Read<T>(T[] buffer, int samples) where T : struct
        {
            OpenTK.Audio.OpenAL.Alc.CaptureSamples<T>(Handle, buffer, samples);
        }

        public void Read(IntPtr buffer, int samples)
        {
            OpenTK.Audio.OpenAL.Alc.CaptureSamples(Handle, buffer, samples);
        }
    }
}
