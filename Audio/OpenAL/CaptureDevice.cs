using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.SharpAL;

namespace Ibasa.Audio.OpenAL
{
    public struct CaptureDevice : IEquatable<CaptureDevice>
    {
        public static IEnumerable<CaptureDevice> CaptureDevices(int frequency, Format format, int buffersize)
        {
            var oal_format = OpenAL.Format(format);
            if (OpenAL.IsExtensionPresent("ALC_EXT_CAPTURE"))
            {
                var devices = OpenTK.Audio.OpenAL.Alc.GetStringList(IntPtr.Zero, OpenTK.Audio.OpenAL.Alc.ALC_CAPTURE_DEVICE_SPECIFIER);
                return devices.Select(name => new CaptureDevice(name, frequency, format, buffersize));
            }
            else
            {
                return new CaptureDevice[0];
            }
        }

        public static CaptureDevice DefaultCaptureDevice(int frequency, Format format, int buffersize)
        {
            if (OpenAL.IsExtensionPresent("ALC_EXT_CAPTURE"))
            {
                return new CaptureDevice(null, frequency, format, buffersize);
            }
            else
            {
                return default(CaptureDevice);
            }
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
            OpenAL.ThrowNullException(Handle);
            return OpenTK.Audio.OpenAL.Alc.CaptureCloseDevice(Handle);
        }

        public string Name
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                return OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.Alc.ALC_CAPTURE_DEVICE_SPECIFIER);
            }
        }

        public int CaptureSamples
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                return OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.Alc.ALC_CAPTURE_SAMPLES);
            }
        }

        public void Start()
        {
            OpenAL.ThrowNullException(Handle);
            OpenTK.Audio.OpenAL.Alc.CaptureStart(Handle);
        }

        public void Stop()
        {
            OpenAL.ThrowNullException(Handle);
            OpenTK.Audio.OpenAL.Alc.CaptureStop(Handle);
        }

        public void Read(byte[] buffer, int samples)
        {
            OpenAL.ThrowNullException(Handle);
            OpenTK.Audio.OpenAL.Alc.CaptureSamples(Handle, buffer, samples);
        }

        public override int GetHashCode()
        {
            OpenAL.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            OpenAL.ThrowNullException(Handle);
            if (obj is CaptureDevice)
            {
                return Equals((CaptureDevice)obj);
            }
            return false;
        }

        public bool Equals(CaptureDevice other)
        {
            OpenAL.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(CaptureDevice left, CaptureDevice right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(CaptureDevice left, CaptureDevice right)
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
