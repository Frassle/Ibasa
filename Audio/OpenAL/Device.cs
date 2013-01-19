using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio.OpenAL
{
    public struct Device : IEquatable<Device>
    {
        public static IEnumerable<Device> Devices
        {
            get
            {
                if (OpenAL.IsExtensionPresent("ALC_ENUMERATE_ALL_EXT"))
                {
                    var devices = OpenTK.Audio.OpenAL.Alc.GetStringList(IntPtr.Zero, OpenTK.Audio.OpenAL.Alc.ALC_ALL_DEVICES_SPECIFIER);
                    return devices.Select(name => new Device(name));
                }
                else if (OpenAL.IsExtensionPresent("ALC_ENUMERATE_EXT"))
                {
                    var devices = OpenTK.Audio.OpenAL.Alc.GetStringList(IntPtr.Zero, OpenTK.Audio.OpenAL.Alc.ALC_DEVICE_SPECIFIER);
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
                return new Device(null);
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
            Handle = OpenTK.Audio.OpenAL.Alc.OpenDevice(name);
            if (Handle == IntPtr.Zero)
            {
                throw new AudioException(string.Format("OpenDevice({0}) failed.", name));
            }
        }

        public bool Close()
        {
            OpenAL.ThrowNullException(Handle);
            return OpenTK.Audio.OpenAL.Alc.CloseDevice(Handle);
        }

        public string Name
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                return OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.Alc.ALC_DEVICE_SPECIFIER);
            }
        }

        public Version Version
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                int major = OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.Alc.ALC_MAJOR_VERSION);
                int minor = OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.Alc.ALC_MINOR_VERSION);
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
                    int attributes_size = OpenTK.Audio.OpenAL.Alc.GetInteger(
                       Handle, OpenTK.Audio.OpenAL.Alc.ALC_ATTRIBUTES_SIZE);

                    int* attributes = stackalloc int[attributes_size];
                    OpenTK.Audio.OpenAL.Alc.GetInteger(
                        Handle, OpenTK.Audio.OpenAL.Alc.ALC_ALL_ATTRIBUTES, attributes_size, attributes);

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
                var value = OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.Alc.ALC_EXTENSIONS);
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
            return OpenTK.Audio.OpenAL.Alc.GetEnumValue(Handle, enumname);
        }

        public bool IsExtensionPresent(string extension)
        {
            OpenAL.ThrowNullException(Handle);
            return OpenTK.Audio.OpenAL.Alc.IsExtensionPresent(Handle, extension);
        }

        public IntPtr GetProcAddress(string funcname)
        {
            OpenAL.ThrowNullException(Handle);
            return OpenTK.Audio.OpenAL.Alc.GetProcAddress(Handle, funcname);
        }

        internal void ThrowError()
        {
            var error = OpenTK.Audio.OpenAL.Alc.GetError(Handle);

            if(error == OpenTK.Audio.OpenAL.Alc.ALC_NO_ERROR)
            {
                return;
            }
            else if(error == OpenTK.Audio.OpenAL.Alc.ALC_INVALID_DEVICE)
            {
                throw new AudioException("No Device. The device handle or specifier names an inaccessible driver/server.");
            }
            else if(error == OpenTK.Audio.OpenAL.Alc.ALC_INVALID_CONTEXT)
            {
                throw new AudioException("Invalid context ID. The Context argument does not name a valid context.");
            }
            else if(error == OpenTK.Audio.OpenAL.Alc.ALC_INVALID_ENUM)
            {
                throw new AudioException("Bad enum. A token used is not valid, or not applicable.");
            }
            else if(error == OpenTK.Audio.OpenAL.Alc.ALC_INVALID_VALUE)
            {
                throw new AudioException("Bad value. A value (e.g. Attribute) is not valid, or not applicable.");
            }
            else if(error == OpenTK.Audio.OpenAL.Alc.ALC_OUT_OF_MEMORY)
            {
                throw new AudioException("Out of memory. Unable to allocate memory.");
            }
            else 
            {
                throw new AudioException(string.Format("Unknown OpenAL error: {0}", error));
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
