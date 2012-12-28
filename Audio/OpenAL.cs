using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
