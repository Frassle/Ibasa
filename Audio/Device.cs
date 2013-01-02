using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio
{
    public sealed class Device : IDisposable
    {
        public string Name { get; private set; }
        public IntPtr Handle { get; private set; }

        internal Device(string name)
        {
            Handle = OpenTK.Audio.OpenAL.Alc.OpenDevice(name);
            if (Handle == IntPtr.Zero)
            {
                throw new AudioException(string.Format("OpenDevice({0}) failed.", name));
            }

            Name = OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.AlcGetString.DeviceSpecifier);

            int attributes_size;
            OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.AlcGetInteger.AttributesSize, 1, out attributes_size);

            int[] attributes = new int[attributes_size];
            OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.AlcGetInteger.AllAttributes, attributes_size, attributes);

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
            }

            UnknownAttributes = new Collections.Immutable.ImmutableArray<int>(unknownAttributes);

            Extensions = new Collections.Immutable.ImmutableArray<string>(
                OpenTK.Audio.OpenAL.Alc.GetString(Handle, OpenTK.Audio.OpenAL.AlcGetString.Extensions).Split());
        }

        ~Device()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!OpenTK.Audio.OpenAL.Alc.CloseDevice(Handle))
            {
                throw new AudioException(string.Format("CloseDevice({0}) failed.", Name));
            }
            else
            {
                GC.SuppressFinalize(this);
            }
        }
        
        public int Frequency
        {
            get;
            private set;
        }

        public int Refresh
        {
            get;
            private set;
        }

        public int MonoSources
        {
            get;
            private set;
        }

        public int StereoSources
        {
            get;
            private set;
        }

        public bool Sync
        {
            get;
            private set;
        }

        public Ibasa.Collections.Immutable.ImmutableArray<int> UnknownAttributes
        {
            get;
            private set;
        }

        public Ibasa.Collections.Immutable.ImmutableArray<string> Extensions
        {
            get;
            private set;
        }

        public Version EfxVersion
        {
            get
            {
                int major, minor;
                OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.AlcGetInteger.EfxMajorVersion, 1, out major);
                OpenTK.Audio.OpenAL.Alc.GetInteger(Handle, OpenTK.Audio.OpenAL.AlcGetInteger.EfxMinorVersion, 1, out minor);
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
