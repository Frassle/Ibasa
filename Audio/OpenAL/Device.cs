using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio.OpenAL
{
    public sealed class DeviceAttributes
    {
        internal DeviceAttributes(int[] attributes)
        {
            var unknownAttributes = new List<int>();

            for (int i = 0; i < attributes.Length; ++i)
            {
                switch (attributes[i])
                {
                    case OpenTK.Audio.OpenAL.AlcContextAttributes.Frequency:
                        {
                            ++i;
                            Frequency = attributes[i];
                            break;
                        }
                    case OpenTK.Audio.OpenAL.AlcContextAttributes.Refresh:
                        {
                            ++i;
                            Refresh = attributes[i];
                            break;
                        }
                    case OpenTK.Audio.OpenAL.AlcContextAttributes.MonoSources:
                        {
                            ++i;
                            MonoSources = attributes[i];
                            break;
                        }
                    case OpenTK.Audio.OpenAL.AlcContextAttributes.StereoSources:
                        {
                            ++i;
                            StereoSources = attributes[i];
                            break;
                        }
                    case OpenTK.Audio.OpenAL.AlcContextAttributes.Sync:
                        {
                            ++i;
                            Sync = attributes[i] != 0;
                            break;
                        }
                    default:
                        {
                            unknownAttributes.Add(attributes[i]);
                            break;
                        }
                }

                UnknownAttributes = new Collections.Immutable.ImmutableArray<int>(unknownAttributes);
            }
        }

        public readonly int Frequency;

        public readonly int Refresh;

        public readonly int MonoSources;

        public readonly int StereoSources;

        public readonly bool Sync;

        public readonly Ibasa.Collections.Immutable.ImmutableArray<int> UnknownAttributes;
    }

    public struct Device : IEquatable<Device>
    {
        public static IEnumerable<Device> Devices
        {
            get
            {
                if (OpenAL.IsExtensionPresent("ALC_ENUMERATE_ALL_EXT"))
                {
                    var devices = OpenTK.Audio.OpenAL.Alc.GetStringList(IntPtr.Zero, OpenTK.Audio.OpenAL.GetString.AllDevicesSpecifier);
                    return devices.Select(name => new Device(name));
                }
                else if (OpenAL.IsExtensionPresent("ALC_ENUMERATE_EXT"))
                {
                    var devices = OpenTK.Audio.OpenAL.Alc.GetStringList(IntPtr.Zero, OpenTK.Audio.OpenAL.GetString.DeviceSpecifier);
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
                return OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.GetString.DeviceSpecifier);
            }
        }

        public Version Version
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                int major = OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.GetInteger.MajorVersion);
                int minor = OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.GetInteger.MinorVersion);
                return new Version(major, minor);
            }
        }

        public DeviceAttributes Attributes
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                int attributes_size = OpenTK.Audio.OpenAL.Alc.GetInteger(
                    Handle, OpenTK.Audio.OpenAL.GetInteger.AttributesSize);

                int[] attributes = new int[attributes_size];
                OpenTK.Audio.OpenAL.Alc.GetInteger(
                    Handle, OpenTK.Audio.OpenAL.GetInteger.AllAttributes, attributes_size, attributes);

                return new DeviceAttributes(attributes);
            }
        }

        public string[] Extensions
        {
            get
            {
                OpenAL.ThrowNullException(Handle);
                var value = OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.GetString.Extensions);
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

            switch (error)
            {
                case OpenTK.Audio.OpenAL.AlcError.NoError:
                    return;
                case OpenTK.Audio.OpenAL.AlcError.InvalidDevice:
                    throw new AudioException("No Device. The device handle or specifier names an inaccessible driver/server.");
                case OpenTK.Audio.OpenAL.AlcError.InvalidContext:
                    throw new AudioException("Invalid context ID. The Context argument does not name a valid context.");
                case OpenTK.Audio.OpenAL.AlcError.InvalidEnum:
                    throw new AudioException("Bad enum. A token used is not valid, or not applicable.");
                case OpenTK.Audio.OpenAL.AlcError.InvalidValue:
                    throw new AudioException("Bad value. A value (e.g. Attribute) is not valid, or not applicable.");
                case OpenTK.Audio.OpenAL.AlcError.OutOfMemory:
                    throw new AudioException("Out of memory. Unable to allocate memory.");
                default:
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
