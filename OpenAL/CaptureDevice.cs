using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace Ibasa.OpenAL
{
    public struct CaptureDevice : IEquatable<CaptureDevice>
    {
        #region Capture functions

        [DllImport("openal32.dll", EntryPoint = "alcCaptureOpenDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        static extern IntPtr CaptureOpenDevice(string devicename, uint frequency, int format, int buffersize);
        // ALC_API ALCdevice*      ALC_APIENTRY alcCaptureOpenDevice( const ALCchar *devicename, ALCuint frequency, ALCenum format, ALCsizei buffersize );

        [DllImport("openal32.dll", EntryPoint = "alcCaptureCloseDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        static extern bool CaptureCloseDevice([In] IntPtr device);
        // ALC_API ALCboolean      ALC_APIENTRY alcCaptureCloseDevice( ALCdevice *device );

        [DllImport("openal32.dll", EntryPoint = "alcCaptureStart", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        static extern void CaptureStart([In] IntPtr device);
        // ALC_API void            ALC_APIENTRY alcCaptureStart( ALCdevice *device );

        [DllImport("openal32.dll", EntryPoint = "alcCaptureStop", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        static extern void CaptureStop([In] IntPtr device);
        // ALC_API void            ALC_APIENTRY alcCaptureStop( ALCdevice *device );

        [DllImport("openal32.dll", EntryPoint = "alcCaptureSamples", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        static unsafe extern void alcCaptureSamples(IntPtr device, void* buffer, int samples);
        // ALC_API void            ALC_APIENTRY alcCaptureSamples( ALCdevice *device, ALCvoid *buffer, ALCsizei samples );

        #endregion Capture functions

        static readonly int ALC_CAPTURE_DEVICE_SPECIFIER = OpenAL.GetEnumValue("ALC_CAPTURE_DEVICE_SPECIFIER");
        static readonly int ALC_CAPTURE_DEFAULT_DEVICE_SPECIFIER = OpenAL.GetEnumValue("ALC_CAPTURE_DEFAULT_DEVICE_SPECIFIER");
        static readonly int ALC_CAPTURE_SAMPLES = OpenAL.GetEnumValue("ALC_CAPTURE_SAMPLES");

        public static IEnumerable<CaptureDevice> CaptureDevices(uint frequency, int format, int buffersize)
        {
            if (OpenAL.IsExtensionPresent("ALC_EXT_CAPTURE"))
            {
                var devices = OpenAL.GetStrings(ALC_CAPTURE_DEVICE_SPECIFIER);
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
            Handle = CaptureOpenDevice(name, frequency, format, buffersize);
            if (Handle == IntPtr.Zero)
            {
                throw new OpenALException(
                    string.Format("CaptureOpenDevice({0}, {1}, {2}, {3}) failed.", name, frequency, format, buffersize));
            }
        }

        public bool Close()
        {
            OpenAL.ThrowNullException(Handle);
            return CaptureCloseDevice(Handle);
        }

        public string GetString(int param)
        {
            OpenAL.ThrowNullException(Handle);
            return OpenAL.MarshalString(OpenAL.GetString(IntPtr.Zero, param));
        }

        public List<string> GetStrings(int param)
        {
            OpenAL.ThrowNullException(Handle);
            return OpenAL.MarshalStringList(OpenAL.GetString(IntPtr.Zero, param));
        }

        public int GetInteger(int param)
        {
            OpenAL.ThrowNullException(Handle);
            return OpenAL.GetInteger(Handle, param);
        }

        public unsafe void GetInteger(int param, int size, int* data)
        {
            OpenAL.ThrowNullException(Handle);
            OpenAL.GetInteger(Handle, param, size, data);
        }

        public void GetInteger(int param, int count, int[] values)
        {
            OpenAL.ThrowNullException(Handle);
            unsafe
            {
                fixed (int* ptr = values)
                {
                    OpenAL.GetInteger(Handle, param, count, ptr);
                }
            }
        }

        public string Name
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                return GetString(ALC_CAPTURE_DEVICE_SPECIFIER);
            }
        }

        public int CaptureSamples
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                return GetInteger(ALC_CAPTURE_SAMPLES);
            }
        }

        public void Start()
        {
            OpenAL.ThrowNullException(Handle);
            CaptureStart(Handle);
        }

        public void Stop()
        {
            OpenAL.ThrowNullException(Handle);
            CaptureStop(Handle);
        }

        public void Read(byte[] buffer, int samples)
        {
            OpenAL.ThrowNullException(Handle);
            unsafe
            {
                fixed (byte* ptr = buffer)
                {
                    alcCaptureSamples(Handle, ptr, samples);
                }
            }
        }

        internal void GetError()
        {
            OpenAL.ThrowNullException(Handle);
            OpenAL.GetError(Handle);
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
