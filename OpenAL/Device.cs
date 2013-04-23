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
        public static readonly Device Null = new Device();

        public static IEnumerable<string> Devices
        {
            get
            {
                unsafe
                {
                    int enumerate_all_ext_length = Encoding.ASCII.GetByteCount("ALC_ENUMERATE_ALL_EXT");
                    byte* enumerate_all_ext = stackalloc byte[enumerate_all_ext_length];
                    AlHelper.StringToAnsi("ALC_ENUMERATE_ALL_EXT", enumerate_all_ext, enumerate_all_ext_length);

                    int enumerate_ext_length = Encoding.ASCII.GetByteCount("ALC_ENUMERATE_EXT");
                    byte* enumerate_ext = stackalloc byte[enumerate_ext_length];
                    AlHelper.StringToAnsi("ALC_ENUMERATE_EXT", enumerate_ext, enumerate_ext_length);

                    if (Alc.IsExtensionPresent(IntPtr.Zero, enumerate_all_ext) != 0)
                    {
                        var device_string = Alc.GetString(IntPtr.Zero, Alc.ALL_DEVICES_SPECIFIER);

                        return AlHelper.MarshalStringList(device_string);
                    }
                    else if (Alc.IsExtensionPresent(IntPtr.Zero, enumerate_ext) != 0)
                    {
                        var device_string = Alc.GetString(IntPtr.Zero, Alc.DEVICE_SPECIFIER);

                        return AlHelper.MarshalStringList(device_string);
                    }
                    else
                    {
                        return new string[0];
                    }
                }
            }
        }

        public static string DefaultDevice
        {
            get
            {
                unsafe
                {
                    int enumerate_all_ext_length = Encoding.ASCII.GetByteCount("ALC_ENUMERATE_ALL_EXT");
                    byte* enumerate_all_ext = stackalloc byte[enumerate_all_ext_length];
                    AlHelper.StringToAnsi("ALC_ENUMERATE_ALL_EXT", enumerate_all_ext, enumerate_all_ext_length);

                    int enumerate_ext_length = Encoding.ASCII.GetByteCount("ALC_ENUMERATE_EXT");
                    byte* enumerate_ext = stackalloc byte[enumerate_ext_length];
                    AlHelper.StringToAnsi("ALC_ENUMERATE_EXT", enumerate_ext, enumerate_ext_length);

                    if (Alc.IsExtensionPresent(IntPtr.Zero, enumerate_all_ext) != 0)
                    {
                        return AlHelper.MarshalString(Alc.GetString(IntPtr.Zero, Alc.ALL_DEVICES_SPECIFIER));
                    }
                    else if (Alc.IsExtensionPresent(IntPtr.Zero, enumerate_ext) != 0)
                    {
                        return AlHelper.MarshalString(Alc.GetString(IntPtr.Zero, Alc.DEVICE_SPECIFIER));
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public IntPtr Handle { get; private set; }

        public Device(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Device(string name)
            : this()
        {
            unsafe
            {
                int name_length = name == null ? 0 : Encoding.ASCII.GetByteCount(name);
                byte* name_ansi = stackalloc byte[name_length + 1];
                AlHelper.StringToAnsi(name, name_ansi, name_length);
                name_ansi = name == null ? null : name_ansi;

                Handle = Alc.OpenDevice(name_ansi);
                if (Handle == IntPtr.Zero)
                {
                    throw new OpenALException(string.Format(
                    "OpenDevice({0}) failed.", name));
                }
            }
        }

        public bool Close()
        {
            AlHelper.ThrowNullException(Handle);
            if (Alc.CloseDevice(Handle) != 0)
            {
                Handle = IntPtr.Zero;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Version Version
        {
            get
            {
                AlHelper.ThrowNullException(Handle);
                unsafe
                {
                    int major, minor;

                    Alc.GetIntegerv(Handle, Alc.MAJOR_VERSION, 1, &major);
                    Alc.GetIntegerv(Handle, Alc.MINOR_VERSION, 1, &minor);

                    return new Version(major, minor);
                }
            }
        }

        public string Name
        {
            get
            {
                AlHelper.ThrowNullException(Handle);
                unsafe
                {
                    return AlHelper.MarshalString(Alc.GetString(Handle, Alc.DEVICE_SPECIFIER));
                }
            }
        }

        public Dictionary<int, int> Attributes
        {
            get
            {
                AlHelper.ThrowNullException(Handle);

                unsafe
                {
                    int attributes_size;
                    Alc.GetIntegerv(Handle, Alc.ATTRIBUTES_SIZE, 1, &attributes_size);

                    int* attributes = stackalloc int[attributes_size];
                    Alc.GetIntegerv(Handle, Alc.ALL_ATTRIBUTES, attributes_size, attributes);

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
                AlHelper.ThrowNullException(Handle);
                unsafe
                {
                    byte* extensions = Alc.GetString(Handle, Alc.EXTENSIONS);
                    if (extensions == null)
                    {
                        return new string[0];
                    }
                    else
                    {
                        return AlHelper.MarshalString(extensions).Split();
                    }
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
            if (obj is Device)
            {
                return Equals((Device)obj);
            }
            return false;
        }

        public bool Equals(Device other)
        {
            AlHelper.ThrowNullException(Handle);
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
            AlHelper.ThrowNullException(Handle);
            return string.Format("Device: {0}", Handle.ToString());
        }
    }
}
