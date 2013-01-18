using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.SharpAL;

namespace Ibasa.Audio
{
    public static class OpenAL
    {
        public static IEnumerable<Device> Devices
        {
            get
            {
                var devices = OpenTK.Audio.OpenAL.Alc.GetString(IntPtr.Zero, OpenTK.Audio.OpenAL.AlcGetStringList.AllDevicesSpecifier);
                return devices.Select(name => Device.Open(name));
            }
        }

        public static Device DefaultDevice
        {
            get
            {
                return new Device(IntPtr.Zero);
            }
        }

        public static IEnumerable<CaptureDevice> CaptureDevices(int frequency, Format format, int buffersize)
        {
            var oal_format = OpenAL.Format(format);
            var devices = OpenTK.Audio.OpenAL.Alc.GetString(IntPtr.Zero, OpenTK.Audio.OpenAL.AlcGetStringList.CaptureDeviceSpecifier);
            return devices.Select(name => new CaptureDevice(name, frequency, oal_format, buffersize));
        }

        public static CaptureDevice DefaultCaptureDevice(int frequency, Format format, int buffersize)
        {
            var oal_format = OpenAL.Format(format);
            return new CaptureDevice(null, frequency, oal_format, buffersize);
        }

        public static Version Version
        {
            get
            {
                int major = OpenTK.Audio.OpenAL.Alc.GetInteger(IntPtr.Zero, OpenTK.Audio.OpenAL.AlcGetInteger.MajorVersion);
                int minor = OpenTK.Audio.OpenAL.Alc.GetInteger(IntPtr.Zero, OpenTK.Audio.OpenAL.AlcGetInteger.MinorVersion);
                return new Version(major, minor);
            }
        }

        internal static OpenTK.Audio.OpenAL.ALFormat Format(Ibasa.SharpAL.Format format)
        {
            if (format is Ibasa.SharpAL.Formats.Pcm8)
            {
                switch (format.Channels)
                {
                    case 1:
                        return OpenTK.Audio.OpenAL.ALFormat.Mono8;
                    case 2:
                        return OpenTK.Audio.OpenAL.ALFormat.Stereo8;
                    case 4:
                        return OpenTK.Audio.OpenAL.ALFormat.MultiQuad8Ext;
                    case 6:
                        return OpenTK.Audio.OpenAL.ALFormat.Multi51Chn8Ext;
                    case 7:
                        return OpenTK.Audio.OpenAL.ALFormat.Multi61Chn8Ext;
                    case 8:
                        return OpenTK.Audio.OpenAL.ALFormat.Multi71Chn8Ext;
                    default:
                        throw new ArgumentException(string.Format("{0} channel 8 bit PCM not supported.", format.Channels), "format");
                }
            }
            if (format is Ibasa.SharpAL.Formats.Pcm16)
            {
                switch (format.Channels)
                {
                    case 1:
                        return OpenTK.Audio.OpenAL.ALFormat.Mono16;
                    case 2:
                        return OpenTK.Audio.OpenAL.ALFormat.Stereo16;
                    case 4:
                        return OpenTK.Audio.OpenAL.ALFormat.MultiQuad16Ext;
                    case 6:
                        return OpenTK.Audio.OpenAL.ALFormat.Multi51Chn16Ext;
                    case 7:
                        return OpenTK.Audio.OpenAL.ALFormat.Multi61Chn16Ext;
                    case 8:
                        return OpenTK.Audio.OpenAL.ALFormat.Multi71Chn16Ext;
                    default:
                        throw new ArgumentException(string.Format("{0} channel 16 bit PCM not supported.", format.Channels), "format");
                }
            }
            if (format is Ibasa.SharpAL.Formats.Float32)
            {
                switch (format.Channels)
                {
                    case 1:
                        return OpenTK.Audio.OpenAL.ALFormat.MonoFloat32Ext;
                    case 2:
                        return OpenTK.Audio.OpenAL.ALFormat.StereoFloat32Ext;
                    default:
                        throw new ArgumentException(string.Format("{0} channel 32 bit float PCM not supported.", format.Channels), "format");
                }
            }
            if (format is Ibasa.SharpAL.Formats.Float64)
            {
                switch (format.Channels)
                {
                    case 1:
                        return OpenTK.Audio.OpenAL.ALFormat.MonoDoubleExt;
                    case 2:
                        return OpenTK.Audio.OpenAL.ALFormat.StereoDoubleExt;
                    default:
                        throw new ArgumentException(string.Format("{0} channel 64 bit float PCM not supported.", format.Channels), "format");
                }
            }

            throw new ArgumentException(string.Format("{0} not supported", format), "format");
        }
    }
}
