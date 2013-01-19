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
                switch ((OpenTK.Audio.OpenAL.AlcContextAttributes)attributes[i])
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
            ThrowNullException();
            return OpenTK.Audio.OpenAL.Alc.CloseDevice(Handle);
        }

        public string Name
        {
            get
            {
                ThrowNullException();
                return OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.AlcGetString.DeviceSpecifier);
            }
        }

        public Version Version
        {
            get
            {
                ThrowNullException();
                int major = OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.AlcGetInteger.MajorVersion);
                int minor = OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.AlcGetInteger.MinorVersion);
                return new Version(major, minor);
            }
        }

        public DeviceAttributes Attributes
        {
            get
            {
                ThrowNullException();
                int attributes_size = OpenTK.Audio.OpenAL.Alc.GetInteger(
                    Handle, OpenTK.Audio.OpenAL.AlcGetInteger.AttributesSize);

                int[] attributes = new int[attributes_size];
                OpenTK.Audio.OpenAL.Alc.GetInteger(
                    Handle, OpenTK.Audio.OpenAL.AlcGetInteger.AllAttributes, attributes_size, attributes);

                return new DeviceAttributes(attributes);
            }
        }

        public string[] Extensions
        {
            get
            {
                ThrowNullException();
                var value = OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.AlcGetString.Extensions);
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

        public bool IsExtensionPresent(string extension)
        {
            ThrowNullException();
            return OpenTK.Audio.OpenAL.Alc.IsExtensionPresent(Handle, extension);
        }

        private void ThrowNullException()
        {
            if (Handle == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }
        }

        internal void ThrowIfError()
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
            ThrowNullException();
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ThrowNullException();
            if (obj is Device)
            {
                return Equals((Device)obj);
            }
            return false;
        }

        public bool Equals(Device other)
        {
            ThrowNullException();
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
            ThrowNullException();
            return Handle.ToString();
        }
    }
}
