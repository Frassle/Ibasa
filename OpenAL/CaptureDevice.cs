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
        public static readonly CaptureDevice Null = new CaptureDevice();

        public static IEnumerable<string> CaptureDevices
        {
            get
            {
                unsafe
                {
                    int ext_capture_length = Encoding.ASCII.GetByteCount("ALC_EXT_CAPTURE");
                    byte* ext_capture = stackalloc byte[ext_capture_length + 1];
                    AlHelper.StringToAnsi("ALC_EXT_CAPTURE", ext_capture, ext_capture_length);

                    if (Alc.IsExtensionPresent(IntPtr.Zero, ext_capture) != 0)
                    {
                        var device_string = Alc.GetString(IntPtr.Zero, Alc.CAPTURE_DEVICE_SPECIFIER);

                        return AlHelper.MarshalStringList(device_string);
                    }
                    else
                    {
                        return new string[0];
                    }
                }
            }
        }

        public static string DefaultCaptureDevice
        {
            get
            {
                unsafe
                {
                    int ext_capture_length = Encoding.ASCII.GetByteCount("ALC_EXT_CAPTURE");
                    byte* ext_capture = stackalloc byte[ext_capture_length + 1];
                    AlHelper.StringToAnsi("ALC_EXT_CAPTURE", ext_capture, ext_capture_length);

                    if (Alc.IsExtensionPresent(IntPtr.Zero, ext_capture) != 0)
                    {
                        var device_string = Alc.GetString(IntPtr.Zero, Alc.CAPTURE_DEFAULT_DEVICE_SPECIFIER);

                        return AlHelper.MarshalString(device_string);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public IntPtr Handle { get; private set; }

        public CaptureDevice(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public CaptureDevice(string name, uint frequency, Format format, int buffersize)
            : this()
        {
            unsafe
            {
                int name_length = name == null ? 0 : Encoding.ASCII.GetByteCount(name);
                byte* name_ansi = stackalloc byte[name_length + 1];
                AlHelper.StringToAnsi(name, name_ansi, name_length);
                name_ansi = name == null ? null : name_ansi;

                Handle = Alc.CaptureOpenDevice(name_ansi, frequency, (int)format, buffersize);
                if (Handle == IntPtr.Zero)
                {
                    throw new OpenALException(string.Format(
                        "CaptureOpenDevice({0}, {1}, {2}, {3}) failed.", name, frequency, format, buffersize));
                }
            }
        }

        public bool Close()
        {
            AlHelper.ThrowNullException(Handle);
            if (Alc.CaptureCloseDevice(Handle) != 0)
            {
                Handle = IntPtr.Zero;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string Name
        {
            get
            {
                AlHelper.ThrowNullException(Handle);
                unsafe
                {
                    return AlHelper.MarshalString(Alc.GetString(Handle, Alc.CAPTURE_DEVICE_SPECIFIER));
                }
            }
        }


        public int CaptureSamples
        {
            get
            {
                AlHelper.ThrowNullException(Handle);
                unsafe
                {
                    int value;
                    Alc.GetIntegerv(Handle, Alc.CAPTURE_SAMPLES, 1, &value);
                    return value;                
                }
            }
        }

        public void Start()
        {
            AlHelper.ThrowNullException(Handle);
            Alc.CaptureStart(Handle);
            AlHelper.GetAlcError(Alc.GetError(Handle));
        }

        public void Stop()
        {
            AlHelper.ThrowNullException(Handle);
            Alc.CaptureStop(Handle);
            AlHelper.GetAlcError(Alc.GetError(Handle));
        }

        public void Read(IntPtr buffer, int samples)
        {
            AlHelper.ThrowNullException(Handle);
            unsafe
            {
                Alc.CaptureSamples(Handle, buffer.ToPointer(), samples);
                AlHelper.GetAlcError(Alc.GetError(Handle));
            }
        }

        public void Read(byte[] buffer, int samples)
        {
            AlHelper.ThrowNullException(Handle);
            unsafe
            {
                fixed (byte* pointer = buffer)
                {
                    Alc.CaptureSamples(Handle, pointer, samples);
                    AlHelper.GetAlcError(Alc.GetError(Handle));
                }
            }
        }

        public override int GetHashCode()
        {
            AlHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            AlHelper.ThrowNullException(Handle);
            if (obj is CaptureDevice)
            {
                return Equals((CaptureDevice)obj);
            }
            return false;
        }

        public bool Equals(CaptureDevice other)
        {
            AlHelper.ThrowNullException(Handle);
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
            AlHelper.ThrowNullException(Handle);
            return string.Format("CaputreDevice: {0}", Handle.ToString());
        }
    }
}
