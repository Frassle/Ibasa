using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.OpenAL
{
    public struct CaptureDevice : IEquatable<CaptureDevice>
    {
        public static IEnumerable<CaptureDevice> CaptureDevices(uint frequency, int format, int buffersize)
        {
            if (OpenAL.IsExtensionPresent("ALC_EXT_CAPTURE"))
            {
                var devices = OpenAL.GetStringList(Alc.ALC_CAPTURE_DEVICE_SPECIFIER);
                return devices.Select(name => new CaptureDevice(name, frequency, format, buffersize));
            }
            else
            {
                return new CaptureDevice[0];
            }
        }

        public static CaptureDevice DefaultCaptureDevice(uint frequency, int format, int buffersize)
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

        internal CaptureDevice(string name, uint frequency, int format, int buffersize)
            : this()
        {
            Handle = Alc.CaptureOpenDevice(name, frequency, format, buffersize);
            if (Handle == IntPtr.Zero)
            {
                throw new OpenALException(
                    string.Format("CaptureOpenDevice({0}, {1}, {2}, {3}) failed.", name, frequency, format, buffersize));
            }
        }

        public bool Close()
        {
            OpenAL.ThrowNullException(Handle);
            return Alc.CaptureCloseDevice(Handle);
        }

        public string Name
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                return Alc.GetMarshaledString(Handle, Alc.ALC_CAPTURE_DEVICE_SPECIFIER);
            }
        }

        public int CaptureSamples
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                return Alc.GetInteger(Handle, Alc.ALC_CAPTURE_SAMPLES);
            }
        }

        public void Start()
        {
            OpenAL.ThrowNullException(Handle);
            Alc.CaptureStart(Handle);
        }

        public void Stop()
        {
            OpenAL.ThrowNullException(Handle);
            Alc.CaptureStop(Handle);
        }

        public void Read(byte[] buffer, int samples)
        {
            OpenAL.ThrowNullException(Handle);
            unsafe
            {
                fixed (byte* ptr = buffer)
                {
                    Alc.CaptureSamples(Handle, ptr, samples);
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
