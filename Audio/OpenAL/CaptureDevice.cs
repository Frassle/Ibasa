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
            var devices = OpenTK.Audio.OpenAL.Alc.GetStringList(IntPtr.Zero, OpenTK.Audio.OpenAL.GetString.CaptureDeviceSpecifier);
            return devices.Select(name => new CaptureDevice(name, frequency, format, buffersize));
        }

        public static CaptureDevice DefaultCaptureDevice(int frequency, Format format, int buffersize)
        {
            return new CaptureDevice(null, frequency, format, buffersize);
        }

        internal IntPtr Handle { get; private set; }

        internal CaptureDevice(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        internal CaptureDevice(string name, int frequency, Format format, int buffersize)
            : this()
        {
            var oal_format = OpenAL.Format(format);
            Handle = OpenTK.Audio.OpenAL.Alc.CaptureOpenDevice(name, frequency, oal_format, buffersize);
            if (Handle == IntPtr.Zero)
            {
                throw new AudioException(
                    string.Format("CaptureOpenDevice({0}, {1}, {2}, {3}) failed.", name, frequency, oal_format, buffersize));
            }
        }

        public bool Close()
        {
            return OpenTK.Audio.OpenAL.Alc.CaptureCloseDevice(Handle);
        }

        public string Name
        {
            get
            {
                return OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.GetString.CaptureDeviceSpecifier);
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
