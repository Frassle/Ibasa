using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio
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

    public struct Device
    {
        internal IntPtr Handle { get; private set; }

        internal Device(IntPtr handle) : this()
        {
            Handle = handle;
        }

        public static Device OpenDevice(string name)
        {
            var handle = OpenTK.Audio.OpenAL.Alc.OpenDevice(name);
            if (handle == IntPtr.Zero)
            {
                throw new AudioException(string.Format("OpenDevice({0}) failed.", name));
            }
            return new Device(handle);
        }

        public static bool CloseDevice(Device device)
        {
            return OpenTK.Audio.OpenAL.Alc.CloseDevice(device.Handle);
        }

        public string Name
        {
            get
            {
                return OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.AlcGetString.DeviceSpecifier);
            }
        }

        public DeviceAttributes Attributes
        {
            get
            {
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
                return OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.AlcGetString.Extensions).Split();
            }
        }

        public Version EfxVersion
        {
            get
            {
                var major = OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.AlcGetInteger.EfxMajorVersion);
                var minor = OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.AlcGetInteger.EfxMinorVersion);
                return new Version(major, minor);
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
    }
}
