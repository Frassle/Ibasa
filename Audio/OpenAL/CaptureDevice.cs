using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.SharpAL;

namespace Ibasa.Audio.OpenAL
{
    public struct CaptureDevice
    {
        public static IEnumerable<CaptureDevice> CaptureDevices(int frequency, Format format, int buffersize)
        {
            var oal_format = OpenAL.Format(format);
            var devices = OpenTK.Audio.OpenAL.Alc.GetString(IntPtr.Zero, OpenTK.Audio.OpenAL.AlcGetStringList.CaptureDeviceSpecifier);
            return devices.Select(name => Open(name, frequency, format, buffersize));
        }

        public static CaptureDevice DefaultCaptureDevice(int frequency, Format format, int buffersize)
        {
            return Open(null, frequency, format, buffersize);
        }

        internal IntPtr Handle { get; private set; }

        internal CaptureDevice(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public static CaptureDevice Open(string name, int frequency, Format format, int buffersize)
        {
            var oal_format = OpenAL.Format(format);
            var handle = OpenTK.Audio.OpenAL.Alc.CaptureOpenDevice(name, frequency, oal_format, buffersize);
            if (handle == IntPtr.Zero)
            {
                throw new AudioException(string.Format("CaptureOpenDevice({0}) failed.", name));
            }
            return new CaptureDevice(handle);
        }

        public bool Close()
        {
            return OpenTK.Audio.OpenAL.Alc.CaptureCloseDevice(Handle);
        }

        public string Name
        {
            get
            {
                return OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.AlcGetString.CaptureDeviceSpecifier);
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
