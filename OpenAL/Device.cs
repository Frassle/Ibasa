using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace Ibasa.OpenAL
{
    public struct Device : IEquatable<Device>
    {
        #region Device Management

        /// <summary>This function opens a device by name.</summary>
        /// <param name="devicename">a null-terminated string describing a device.</param>
        /// <returns>Returns a pointer to the opened device. The return value will be NULL if there is an error.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcOpenDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity()]
        static extern IntPtr OpenDevice([In] string devicename);
        // ALC_API ALCdevice *     ALC_APIENTRY alcOpenDevice( const ALCchar *devicename );

        /// <summary>This function closes a device by name.</summary>
        /// <param name="device">a pointer to an opened device</param>
        /// <returns>True will be returned on success or False on failure. Closing a device will fail if the device contains any contexts or buffers.</returns>
        [DllImport("openal32.dll", EntryPoint = "alcCloseDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        static extern bool CloseDevice([In] IntPtr device);
        // ALC_API ALCboolean      ALC_APIENTRY alcCloseDevice( ALCdevice *device );

        #endregion Device Management

        static readonly int ALC_DEFAULT_DEVICE_SPECIFIER = OpenAL.GetEnumValue("ALC_DEFAULT_DEVICE_SPECIFIER");
        static readonly int ALC_DEVICE_SPECIFIER = OpenAL.GetEnumValue("ALC_DEVICE_SPECIFIER");
        static readonly int ALC_EXTENSIONS = OpenAL.GetEnumValue("ALC_EXTENSIONS");

        static readonly int ALC_MAJOR_VERSION = OpenAL.GetEnumValue("ALC_MAJOR_VERSION");
        static readonly int ALC_MINOR_VERSION = OpenAL.GetEnumValue("ALC_MINOR_VERSION");

        static readonly int ALC_ATTRIBUTES_SIZE = OpenAL.GetEnumValue("ALC_ATTRIBUTES_SIZE");
        static readonly int ALC_ALL_ATTRIBUTES = OpenAL.GetEnumValue("ALC_ALL_ATTRIBUTES");

        static readonly int ALC_DEFAULT_ALL_DEVICES_SPECIFIER = OpenAL.GetEnumValue("ALC_DEFAULT_ALL_DEVICES_SPECIFIER");
        static readonly int ALC_ALL_DEVICES_SPECIFIER = OpenAL.GetEnumValue("ALC_ALL_DEVICES_SPECIFIER");

        public static IEnumerable<Device> Devices
        {
            get
            {
                if (OpenAL.IsExtensionPresent("ALC_ENUMERATE_ALL_EXT"))
                {
                    var devices = OpenAL.GetStringList(ALC_ALL_DEVICES_SPECIFIER);
                    return devices.Select(name => new Device(name));
                }
                else if (OpenAL.IsExtensionPresent("ALC_ENUMERATE_EXT"))
                {
                    var devices = OpenAL.GetStringList(ALC_DEVICE_SPECIFIER);
                    return devices.Select(name => new Device(name));
                }
                else
                {
                    return new Device[] { new Device(null) };
                }
            }
        }

        public static Device DefaultDevice
        {
            get
            {
                if (OpenAL.IsExtensionPresent("ALC_ENUMERATE_ALL_EXT"))
                {
                    var device = OpenAL.GetString(ALC_DEFAULT_ALL_DEVICES_SPECIFIER);
                    return new Device(device);
                }
                else if (OpenAL.IsExtensionPresent("ALC_ENUMERATE_EXT"))
                {
                    var device = OpenAL.GetString(ALC_DEFAULT_DEVICE_SPECIFIER);
                    return new Device(device);
                }
                else
                {
                    return new Device(null);
                }
            }
        }

        internal IntPtr Handle { get; private set; }

        internal Device(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        internal Device(string name)
            : this()
        {
            Handle = OpenDevice(name);
            if (Handle == IntPtr.Zero)
            {
                throw new OpenALException(string.Format("OpenDevice({0}) failed.", name));
            }
        }

        public bool Close()
        {
            OpenAL.ThrowNullException(Handle);
            return CloseDevice(Handle);
        }

        public string GetString(int param)
        {
            OpenAL.ThrowNullException(Handle);
            return OpenAL.GetMarshaledString(Handle, param);
        }

        public List<string> GetStringList(int param)
        {
            OpenAL.ThrowNullException(Handle);
            return OpenAL.GetMarshaledStringList(Handle, param);
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
                return GetString(ALC_DEVICE_SPECIFIER);
            }
        }

        public Version Version
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                int major = GetInteger(ALC_MAJOR_VERSION);
                int minor = GetInteger(ALC_MINOR_VERSION);
                return new Version(major, minor);
            }
        }

        public Dictionary<int, int> Attributes
        {
            get
            {
                OpenAL.ThrowNullException(Handle);

                unsafe
                {
                    int attributes_size = GetInteger(ALC_ATTRIBUTES_SIZE);

                    int* attributes = stackalloc int[attributes_size];
                    GetInteger(ALC_ALL_ATTRIBUTES, attributes_size, attributes);

                    var dictionary = new Dictionary<int, int>();

                    int index = 0;
                    while(attributes[index] != 0)
                    {
                        var key = attributes[index++];
                        var value = attributes[index++];
                        dictionary.Add(key, value);
                    }

                    return dictionary;
                }
            }
        }

        public string[] Extensions
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                var value = GetString(ALC_EXTENSIONS);
                if (value == null)
                {
                    return null;
                }
                else
                {
                    return value.Split();
                }
            }
        }

        public int GetEnumValue(string enumname)
        {
            OpenAL.ThrowNullException(Handle);
            return OpenAL.GetEnumValue(Handle, enumname);
        }

        public bool IsExtensionPresent(string extension)
        {
            OpenAL.ThrowNullException(Handle);
            return OpenAL.IsExtensionPresent(Handle, extension);
        }

        public IntPtr GetProcAddress(string funcname)
        {
            OpenAL.ThrowNullException(Handle);
            return OpenAL.GetProcAddress(Handle, funcname);
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
            if (obj is Device)
            {
                return Equals((Device)obj);
            }
            return false;
        }

        public bool Equals(Device other)
        {
            OpenAL.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Device left, Device right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Device left, Device right)
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
