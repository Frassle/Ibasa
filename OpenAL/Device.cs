using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.OpenAL
{
    public struct Device : IEquatable<Device>
    {
        public static IEnumerable<Device> Devices
        {
            get
            {
                if (OpenAL.IsExtensionPresent("ALC_ENUMERATE_ALL_EXT"))
                {
                    var devices = OpenAL.GetStringList(Alc.ALC_ALL_DEVICES_SPECIFIER);
                    return devices.Select(name => new Device(name));
                }
                else if (OpenAL.IsExtensionPresent("ALC_ENUMERATE_EXT"))
                {
                    var devices = OpenAL.GetStringList(Alc.ALC_DEVICE_SPECIFIER);
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
                    var device = OpenAL.GetString(Alc.ALC_DEFAULT_ALL_DEVICES_SPECIFIER);
                    return new Device(device);
                }
                else if (OpenAL.IsExtensionPresent("ALC_ENUMERATE_EXT"))
                {
                    var device = OpenAL.GetString(Alc.ALC_DEFAULT_DEVICE_SPECIFIER);
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
            Handle = Alc.OpenDevice(name);
            if (Handle == IntPtr.Zero)
            {
                throw new OpenALException(string.Format("OpenDevice({0}) failed.", name));
            }
        }

        public bool Close()
        {
            OpenAL.ThrowNullException(Handle);
            return Alc.CloseDevice(Handle);
        }

        public string GetString(int param)
        {
            OpenAL.ThrowNullException(Handle);
            return Alc.GetMarshaledString(Handle, param);
        }

        public List<string> GetStringList(int param)
        {
            OpenAL.ThrowNullException(Handle);
            return Alc.GetMarshaledStringList(Handle, param);
        }

        public int GetInteger(int param)
        {
            OpenAL.ThrowNullException(Handle);
            return Alc.GetInteger(Handle, param);
        }

        public void GetInteger(int param, int count, int[] values)
        {
            OpenAL.ThrowNullException(Handle);
            unsafe
            {
                fixed (int* ptr = values)
                {
                    Alc.GetInteger(Handle, param, count, ptr);
                }
            }
        }

        unsafe void GetInteger(int param, int count, int* values)
        {
            OpenAL.ThrowNullException(Handle);
            Alc.GetInteger(Handle, param, count, values);
        }

        public string Name
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                return GetString(Alc.ALC_DEVICE_SPECIFIER);
            }
        }

        public Version Version
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                int major = GetInteger(Alc.ALC_MAJOR_VERSION);
                int minor = GetInteger(Alc.ALC_MINOR_VERSION);
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
                    int attributes_size = GetInteger(Alc.ALC_ATTRIBUTES_SIZE);

                    int* attributes = stackalloc int[attributes_size];
                    GetInteger(Alc.ALC_ALL_ATTRIBUTES, attributes_size, attributes);

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
                var value = GetString(Alc.ALC_EXTENSIONS);
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
            return Alc.GetEnumValue(Handle, enumname);
        }

        public bool IsExtensionPresent(string extension)
        {
            OpenAL.ThrowNullException(Handle);
            return Alc.IsExtensionPresent(Handle, extension);
        }

        public IntPtr GetProcAddress(string funcname)
        {
            OpenAL.ThrowNullException(Handle);
            return Alc.GetProcAddress(Handle, funcname);
        }

        internal void ThrowError()
        {
            var error = Alc.GetError(Handle);

            if(error == Alc.ALC_NO_ERROR)
            {
                return;
            }
            else if(error == Alc.ALC_INVALID_DEVICE)
            {
                throw new OpenALException("No Device. The device handle or specifier names an inaccessible driver/server.");
            }
            else if(error == Alc.ALC_INVALID_CONTEXT)
            {
                throw new OpenALException("Invalid context ID. The Context argument does not name a valid context.");
            }
            else if(error == Alc.ALC_INVALID_ENUM)
            {
                throw new OpenALException("Bad enum. A token used is not valid, or not applicable.");
            }
            else if(error == Alc.ALC_INVALID_VALUE)
            {
                throw new OpenALException("Bad value. A value (e.g. Attribute) is not valid, or not applicable.");
            }
            else if(error == Alc.ALC_OUT_OF_MEMORY)
            {
                throw new OpenALException("Out of memory. Unable to allocate memory.");
            }
            else 
            {
                throw new OpenALException(string.Format("Unknown OpenAL error: {0}", error));
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
