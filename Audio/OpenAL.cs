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
                return devices.Select(name => new Device(name));
            }
        }

        public static Device DefaultDevice
        {
            get
            {
                return new Device(null);
            }
        }

        public static Version Version
        {
            get
            {
                int major, minor;
                OpenTK.Audio.OpenAL.Alc.GetInteger(IntPtr.Zero, OpenTK.Audio.OpenAL.AlcGetInteger.MajorVersion, 1, out major);
                OpenTK.Audio.OpenAL.Alc.GetInteger(IntPtr.Zero, OpenTK.Audio.OpenAL.AlcGetInteger.MinorVersion, 1, out minor);
                return new Version(major, minor);
            }
        }

        internal static OpenTK.Audio.OpenAL.ALFormat Format(Ibasa.SharpAL.Format format)
        {
            if (format is Ibasa.SharpAL.Formats.PCM8)
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

            throw new ArgumentException(string.Format("{0} not supported", format), "format");
        }
    }
}
