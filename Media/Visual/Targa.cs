using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ibasa.SharpIL;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Media.Visual
{
    public sealed class Targa
    {
        private struct HeaderStruct
        {
            public byte[] ImageId;
            public byte ColorMapType;
            public byte ImageType;
            public ushort ColourMapOrigin;
            public ushort ColorMapLength;
            public byte ColorMapDepth;
            public ushort XOrigin;
            public ushort YOrigin;
            public ushort Width;
            public ushort Height;
            public byte PixelDepth;
            public byte ImageDescriptor;
        }
        HeaderStruct Header;

        private uint[] ColorMap;
        private Resource ImageData;

        private struct ExtensionStruct
        {
            public byte[] AuthorName;
            public byte[] AuthorComments;
            public ushort DateTimeStampMonth;
            public ushort DateTimeStampDay;
            public ushort DateTimeStampYear;
            public ushort DateTimeStampHour;
            public ushort DateTimeStampMinute;
            public ushort DateTimeStampSecond;
            public byte[] JobId;
            public ushort JobTimeHours;
            public ushort JobTimeMinutes;
            public ushort JobTimeSeconds;
            public byte[] SoftwareId;
            public ushort SoftwareVersionNumber;
            public byte SoftwareVersionLetter;
            public int KeyColor;
            public ushort PixelAspectRatioN;
            public ushort PixelAspectRatioD;
            public ushort GammaValueN;
            public ushort GammaValueD;
            public Vector4i[] ColorCorrection;
            public Resource PostageStamp;
            public long[] ScanLines;
            public byte Attributes;

            public bool AllDefault
            {
                get
                {
                    return (
                        (AuthorName == null || AuthorName.All((b) => b == 0)) &&
                        (AuthorComments == null || AuthorComments.All((b) => b == 0)) &&
                        (DateTimeStampMonth == 0 && DateTimeStampDay == 0 && DateTimeStampYear == 0 &&
                        DateTimeStampHour == 0 && DateTimeStampMinute == 0 && DateTimeStampSecond == 0) &&
                        (JobId == null || JobId.All((b) => b == 0)) &&
                        (JobTimeHours == 0 && JobTimeMinutes == 0 && JobTimeSeconds == 0) &&
                        (SoftwareId == null || SoftwareId.All((b) => b == 0)) &&
                        (SoftwareVersionNumber == 0 && SoftwareVersionLetter == ' ') &&
                        (KeyColor == 0) &&
                        (PixelAspectRatioD == 0) &&
                        (GammaValueD == 0) &&
                        (ColorCorrection == null) &&
                        (PostageStamp == null) &&
                        (ScanLines == null) &&
                        (Attributes == 0));
                }
            }
        }
        ExtensionStruct Extension;

        public string ImageId
        {
            get { return System.Text.Encoding.ASCII.GetString(Header.ImageId); }
            set
            {
                if (value == null)
                {
                    Header.ImageId = new byte[0];
                    return;
                }

                byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
                if (data.Length > 255)
                    throw new ArgumentException("ImageId must be stored in less than 256 bytes.", "value");
                Header.ImageId = data;
            }
        }
        public Resource Image
        {
            get { return ImageData; }
            set
            {
                if (value != null)
                {
                    if (!(
                        value.Format is SharpIL.Formats.B8G8R8A8UNorm ||
                        value.Format is SharpIL.Formats.B8G8R8UNorm ||
                        value.Format is SharpIL.Formats.R8G8B8A8UNorm ||
                        value.Format is SharpIL.Formats.R8G8B8UNorm ||
                        value.Format is SharpIL.Formats.R8UNorm ||
                        value.Format is SharpIL.Formats.A8UNorm))
                        throw new ArgumentException(
                            "Unsupported format. Targa only supports B8G8R8A8, B8G8R8, R8G8B8A8, R8G8B8, R8 and A8.",
                            "value");

                    if (value.Size.Depth > 1)
                        throw new ArgumentException(
                            "Unsupported image. Targa does not support 3D images.",
                            "value");
                    if (value.MipLevels > 1)
                        throw new ArgumentException(
                            "Unsupported image. Targa does not support mipmaps.",
                            "value");
                    if (value.ArraySize > 1)
                        throw new ArgumentException(
                            "Unsupported image. Targa does not support arrays.",
                            "value");

                    if (value.Size.Width > ushort.MaxValue || value.Size.Height > ushort.MaxValue)
                        throw new ArgumentException(
                            "Unsupported image. Targa only supports image dimensions of 1 to 65535.",
                            "value");
                }
                ImageData = value;
            }
        }

        public sealed class DeveloperDirectory : IList<KeyValuePair<int, byte[]>>
        {
            List<KeyValuePair<ushort, byte[]>> List = new List<KeyValuePair<ushort, byte[]>>();

            public int IndexOf(KeyValuePair<int, byte[]> item)
            {
                if (item.Key < 0 || item.Key > ushort.MaxValue)
                    return -1; //we know we wont find it

                return List.IndexOf(new KeyValuePair<ushort, byte[]>((ushort)item.Key, item.Value));
            }

            public void Insert(int index, KeyValuePair<int, byte[]> item)
            {
                if (Count == ushort.MaxValue)
                    throw new InvalidOperationException("DeveloperDirectory can only hold 65535 items.");
                if(item.Key < 0 || item.Key > ushort.MaxValue)
                    throw new ArgumentException("item.Key must be between 0 and 65535 inclusive.", "item");

                List.Insert(index, new KeyValuePair<ushort, byte[]>((ushort)item.Key, item.Value));
            }

            public void RemoveAt(int index)
            {
                List.RemoveAt(index);
            }

            public KeyValuePair<int, byte[]> this[int index]
            {
                get
                {
                    KeyValuePair<ushort, byte[]> pair = List[index];
                    return new KeyValuePair<int, byte[]>(pair.Key, pair.Value);
                }
                set
                {
                    if (value.Key < 0 || value.Key > ushort.MaxValue)
                        throw new ArgumentException("value.Key must be between 0 and 65535 inclusive.", "value");

                    KeyValuePair<ushort, byte[]> pair = new KeyValuePair<ushort, byte[]>((ushort)value.Key, value.Value);
                    List[index] = pair;
                }
            }

            public void Add(KeyValuePair<int, byte[]> item)
            {
                if (Count == ushort.MaxValue)
                    throw new InvalidOperationException("DeveloperDirectory can only hold 65535 items.");
                if (item.Key < 0 || item.Key > ushort.MaxValue)
                    throw new ArgumentException("item.Key must be between 0 and 65535 inclusive.", "item");

                List.Add(new KeyValuePair<ushort, byte[]>((ushort)item.Key, item.Value));
            }

            public void Clear()
            {
                List.Clear();
            }

            public bool Contains(KeyValuePair<int, byte[]> item)
            {
                if (item.Key < 0 || item.Key > ushort.MaxValue)
                    return false; //we know we wont find it

                return List.Contains(new KeyValuePair<ushort, byte[]>((ushort)item.Key, item.Value));
            }

            public void CopyTo(KeyValuePair<int, byte[]>[] array, int arrayIndex)
            {
                foreach (var pair in this)
                {
                    array[arrayIndex++] = new KeyValuePair<int, byte[]>(pair.Key, pair.Value);
                }
            }

            public int Count
            {
                get { return List.Count; }
            }

            public bool IsReadOnly
            {
                get { return false; }
            }

            public bool Remove(KeyValuePair<int, byte[]> item)
            {
                if (item.Key < 0 || item.Key > ushort.MaxValue)
                    return false; //we know we wont find it

                return List.Remove(new KeyValuePair<ushort, byte[]>((ushort)item.Key, item.Value));
            }

            public IEnumerator<KeyValuePair<int, byte[]>> GetEnumerator()
            {
                foreach (var pair in List)
                {
                    yield return new KeyValuePair<int, byte[]>(pair.Key, pair.Value);
                }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private DeveloperDirectory _DeveloperArea = new DeveloperDirectory();
        public DeveloperDirectory DeveloperArea { get { return _DeveloperArea; } }

        public string AuthorName
        {
            get
            {
                return Extension.AuthorName == null ? null : System.Text.Encoding.ASCII.GetString(Extension.AuthorName);
            }
            set
            {
                if (value == null)
                {
                    Extension.AuthorName = null;
                    return;
                }

                int length = System.Text.Encoding.ASCII.GetByteCount(value);
                if (length > 40)
                    throw new ArgumentException("AuthorName must be stored in less than 41 bytes.", "value");
                if (Extension.AuthorName == null)
                    Extension.AuthorName = new byte[41];
                System.Text.Encoding.ASCII.GetBytes(value, 0, value.Length, Extension.AuthorName, 0);
                Array.Clear(Extension.AuthorName, length, 41 - length);
            }
        }

        public string[] AuthorComments
        {
            get
            {
                if (Extension.AuthorComments == null)
                    return null;

                string[] comments = new string[4];

                comments[0] = System.Text.Encoding.ASCII.GetString(Extension.AuthorComments, 0, 80);
                comments[1] = System.Text.Encoding.ASCII.GetString(Extension.AuthorComments, 81, 80);
                comments[2] = System.Text.Encoding.ASCII.GetString(Extension.AuthorComments, 162, 80);
                comments[3] = System.Text.Encoding.ASCII.GetString(Extension.AuthorComments, 243, 80);

                return comments;
            }
            set
            {
                if (value == null)
                {
                    Extension.AuthorComments = null;
                    return;
                }

                if (value.Length > 4)
                    throw new ArgumentException("AuthorComment must have less than 5 lines.", "value");

                int line = 0;
                for (; line < value.Length; ++line)
                {
                    int length = System.Text.Encoding.ASCII.GetByteCount(value[line]);
                    if (length > 80)
                        throw new ArgumentException("AuthorComment must be stored in less than 80 bytes per line.", "value");

                    if (Extension.AuthorComments == null)
                        Extension.AuthorComments = new byte[324];

                    System.Text.Encoding.ASCII.GetBytes(value[line], 0, value.Length, Extension.AuthorComments, line * 81);
                    Array.Clear(Extension.AuthorName, line * 81 + length, 81 - length);
                }
                for (; line < 4; ++line)
                {
                    if (Extension.AuthorComments != null)
                        Array.Clear(Extension.AuthorComments, line * 81, 81);
                }
            }
        }

        public DateTime DateTimeStamp
        {
            get
            {
                return new DateTime(
                Extension.DateTimeStampYear, Extension.DateTimeStampMonth, Extension.DateTimeStampDay,
                Extension.DateTimeStampHour, Extension.DateTimeStampMinute, Extension.DateTimeStampSecond);
            }
            set
            {
                Extension.DateTimeStampYear = (ushort)value.Year;
                Extension.DateTimeStampMonth = (ushort)value.Month;
                Extension.DateTimeStampDay = (ushort)value.Day;
                Extension.DateTimeStampHour = (ushort)value.Hour;
                Extension.DateTimeStampMinute = (ushort)value.Minute;
                Extension.DateTimeStampSecond = (ushort)value.Second;
            }
        }

        public string JobId
        {
            get
            {
                return Extension.JobId == null ? null : System.Text.Encoding.ASCII.GetString(Extension.JobId);
            }
            set
            {
                if (value == null)
                {
                    Extension.JobId = null;
                    return;
                }

                int length = System.Text.Encoding.ASCII.GetByteCount(value);
                if (length > 40)
                    throw new ArgumentException("JobId must be stored in less than 41 bytes.", "value");
                if (Extension.JobId == null)
                    Extension.JobId = new byte[41];
                System.Text.Encoding.ASCII.GetBytes(value, 0, value.Length, Extension.JobId, 0);
                Array.Clear(Extension.JobId, length, 41 - length);
            }
        }

        public TimeSpan JobTime
        {
            get
            {
                return new TimeSpan(Extension.JobTimeHours, Extension.JobTimeMinutes, Extension.JobTimeSeconds);
            }
            set
            {
                if ((value.Days * 24 + value.Hours) > ushort.MaxValue)
                    throw new ArgumentOutOfRangeException("value", "TotalHours must be less than 65536.");

                Extension.JobTimeHours = (ushort)(value.Days * 24 + value.Hours);
                Extension.JobTimeMinutes = (ushort)value.Minutes;
                Extension.JobTimeSeconds = (ushort)value.Seconds;
            }
        }

        public string SoftwareId
        {
            get
            {
                return Extension.SoftwareId == null ? null : System.Text.Encoding.ASCII.GetString(Extension.SoftwareId);
            }
            set
            {
                if (value == null)
                {
                    Extension.SoftwareId = null;
                    return;
                }

                int length = System.Text.Encoding.ASCII.GetByteCount(value);
                if (length > 40)
                    throw new ArgumentException("SoftwareId must be stored in less than 41 bytes.", "value");
                if (Extension.SoftwareId == null)
                    Extension.SoftwareId = new byte[41];
                System.Text.Encoding.ASCII.GetBytes(value, 0, value.Length, Extension.SoftwareId, 0);
                Array.Clear(Extension.SoftwareId, length, 41 - length);
            }
        }

        public string SoftwareVersion
        {
            get
            {
                char[] version = new char[5];
                version[0] = (char)('0' + Extension.SoftwareVersionNumber / 100);
                version[1] = '.';
                version[2] = (char)('0' + (Extension.SoftwareVersionNumber % 100) / 10);
                version[3] = (char)('0' + Extension.SoftwareVersionNumber / 10);
                version[4] = (char)Extension.SoftwareVersionLetter;

                if (version[4] == ' ')
                    return new string(version, 0, 4);
                else
                    return new string(version, 0, 5);
            }
            set
            {
                if(value == null)
                {
                    Extension.SoftwareVersionNumber = 0;
                    Extension.SoftwareVersionLetter = (byte)' ';
                }

                if (value.Length != 4 || value.Length != 5)
                {
                    throw new ArgumentException("Incorect version format, must be of the format d.dd or d.ddl.", "value");
                }

                if (value.Length == 4)
                {
                    if (!(
                        char.IsDigit(value[0]) &&
                        value[1] == '.' &&
                        char.IsDigit(value[1]) &&
                        char.IsDigit(value[2])))
                        throw new ArgumentException("Incorect version format, must be of the format d.dd or d.ddl.", "value");

                    Extension.SoftwareVersionNumber = (ushort)(
                        (((int)value[0] - '0') * 100) +
                        (((int)value[2] - '0') * 10) +
                        (((int)value[3] - '0')));
                    Extension.SoftwareVersionLetter = (byte)' ';
                }
                else
                {
                    if (!(
                        char.IsDigit(value[0]) &&
                        value[1] == '.' &&
                        char.IsDigit(value[1]) &&
                        char.IsDigit(value[2]) &&
                        char.IsLetter(value[3])))
                        throw new ArgumentException("Incorect version format, must be of the format d.dd or d.ddl.", "value");

                    Extension.SoftwareVersionNumber = (ushort)(
                        (((int)value[0] - '0') * 100) +
                        (((int)value[2] - '0') * 10) +
                        (((int)value[3] - '0')));
                    Extension.SoftwareVersionLetter = (byte)value[4];
                }
            }
        }

        public Vector4b KeyColor
        {
            get { return (Vector4b)Vector.Unpack(8, 8, 8, 8, Extension.KeyColor); }
            set { Extension.KeyColor = (int)Vector.Pack(8, 8, 8, 8, value); }
        }

        public Numerics.Rational? PixelAspectRatio
        {
            get 
            {
                if (Extension.PixelAspectRatioD == 0)
                    return null;
                else
                    return new Numerics.Rational(Extension.PixelAspectRatioN, Extension.PixelAspectRatioD);
            }
            set
            {
                if (value.HasValue)
                {
                    Numerics.Rational rational = value.Value;

                    if (rational.Numerator < 0 || rational.Numerator > ushort.MaxValue || rational.Denominator > ushort.MaxValue)
                        throw new ArgumentOutOfRangeException("value");

                    Extension.PixelAspectRatioN = (ushort)rational.Numerator;
                    Extension.PixelAspectRatioD = (ushort)rational.Denominator;
                }
                else
                {
                    Extension.PixelAspectRatioN = 0;
                    Extension.PixelAspectRatioD = 0;
                }
            }
        }

        public Numerics.Rational? GammaValue
        {
            get
            {
                if (Extension.GammaValueD == 0)
                    return null;
                else
                    return new Numerics.Rational(Extension.GammaValueN, Extension.GammaValueD);
            }
            set
            {
                if (value.HasValue)
                {
                    Numerics.Rational rational = value.Value;

                    if (rational.Numerator < 0 || rational.Numerator > ushort.MaxValue || rational.Denominator > ushort.MaxValue)
                        throw new ArgumentOutOfRangeException("value");

                    Extension.GammaValueN = (ushort)rational.Numerator;
                    Extension.GammaValueD = (ushort)rational.Denominator;
                }
                else
                {
                    Extension.GammaValueN = 0;
                    Extension.GammaValueD = 0;
                }
            }
        }

        public Vector4i[] ColorCorrection
        {
            get
            {
                if (Extension.ColorCorrection == null)
                    Extension.ColorCorrection = new Vector4i[256];
                return Extension.ColorCorrection;
            }
        }

        public Resource PostageStamp
        {
            get { return Extension.PostageStamp; }
            set
            {
                if (value != null)
                {
                    if (Image == null || value.Format != Image.Format)
                        throw new ArgumentException("PostageStamp.Format must match Image.Format", "value");

                    if (value.Size.Depth > 1)
                        throw new ArgumentException(
                            "Unsupported image. Targa only supports 2D images.",
                            "value");

                    if (value.Size.Width > byte.MaxValue || value.Size.Height > byte.MaxValue)
                        throw new ArgumentException(
                            "Unsupported image. Targa only supports image dimensions of 1 to 255.",
                            "value");
                }
                Extension.PostageStamp = value;
            }
        }

        public long[] Scanlines
        {
            get
            {
                if (Extension.ScanLines == null)
                    Extension.ScanLines = new long[Header.Height];
                return Extension.ScanLines;
            }
        }

        public int Attributes
        {
            get { return Extension.Attributes; }
            set
            {
                if (value < 0 || value > 4)
                    throw new ArgumentOutOfRangeException("Attributes must be between 0 and 4 inclusive.", "value");
                Extension.Attributes = (byte)value;
            }
        }

        public Targa(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                Load(stream);
            }
        }

        public Targa(Stream stream)
        {
            Load(stream);
        }

        private void Load(Stream stream)
        {
            var reader = new Ibasa.IO.BinaryReader(stream, System.Text.Encoding.ASCII);

            ReadHeader(reader);
            ReadColorMap(reader);
            ImageData = ReadData(reader, Header.Width, Header.Height);
            ReadFooter(reader);
        }

        private void ReadHeader(Ibasa.IO.BinaryReader reader)
        {
            Header.ImageId = new byte[reader.ReadByte()];
            Header.ColorMapType = reader.ReadByte();
            Header.ImageType = reader.ReadByte();

            Header.ColourMapOrigin = reader.ReadUInt16();
            Header.ColorMapLength = reader.ReadUInt16();
            Header.ColorMapDepth = reader.ReadByte();

            Header.XOrigin = reader.ReadUInt16();
            Header.YOrigin = reader.ReadUInt16();
            Header.Width = reader.ReadUInt16();
            Header.Height = reader.ReadUInt16();

            Header.PixelDepth = reader.ReadByte();
            Header.ImageDescriptor = reader.ReadByte();

            reader.Read(Header.ImageId, 0, Header.ImageId.Length);
        }

        private void ReadColorMap(Ibasa.IO.BinaryReader reader)
        {
            if (Header.ColorMapType == 0)
                return;

            ColorMap = new uint[256];

            switch (Header.ColorMapDepth)
            {
                case 16:
                    {
                        for (int i = Header.ColourMapOrigin; i < Header.ColorMapLength; ++i)
                        {
                            uint color = reader.ReadUInt16();
                            uint r = (color >> 10) & 0x1F;
                            uint g = (color >> 5) & 0x1F;
                            uint b = (color) & 0x1F;
                            //expand 8 bits
                            r = (r << 3) | (r >> 2);
                            g = (g << 3) | (g >> 2);
                            b = (b << 3) | (b >> 2);
                            ColorMap[i] = (r << 16) | (g << 8) | b;
                        }
                    } break;
                case 24:
                    {
                        for (int i = Header.ColourMapOrigin; i < Header.ColorMapLength; ++i)
                        {
                            uint b = reader.ReadByte();
                            uint g = reader.ReadByte();
                            uint r = reader.ReadByte();
                            ColorMap[i] = (r << 16) | (g << 8) | b;
                        }
                    } break;
                case 32:
                    {
                        for (int i = Header.ColourMapOrigin; i < Header.ColorMapLength; ++i)
                        {
                            ColorMap[i] = reader.ReadUInt32();
                        }
                    } break;
                default:
                    throw new InvalidDataException("Unrecognized color map depth.");
            }
        }

        private Resource ReadData(Ibasa.IO.BinaryReader reader, int width, int height)
        {
            if (Header.ImageType == 0)
                return null;

            switch (Header.ImageType)
            {
                case 1:
                    return ReadUncompressedMappedData(reader, width, height);
                case 2:
                    return ReadUncompressedColorData(reader, width, height);
                case 3:
                    return ReadUncompressedBWData(reader, width, height);
                case 5:
                    return ReadRLEMappedData(reader, width, height);
                case 10:
                    return ReadRLEColorData(reader, width, height);
                case 11:
                    return ReadRLEBWData(reader, width, height);
                default:
                    throw new InvalidDataException("Unrecognized image type.");
            }
        }

        private Resource ReadUncompressedMappedData(Ibasa.IO.BinaryReader reader, int width, int height)
        {
            switch (Header.PixelDepth)
            {
                case 8:
                    {
                        Resource resource = new Resource(new Size3i(width, height, 1), 1, 1, SharpIL.Format.B8G8R8A8UNorm);
                        byte[] data = resource[0, 0];
                        for (int y = 0; y < resource.Size.Height; ++y)
                        {
                            for (int x = 0; x < resource.Size.Width; ++x)
                            {
                                uint color = ColorMap[reader.ReadByte()];

                                int offset = x * 4 + y * 4 * resource.Size.Width;

                                data[offset + 0] = (byte)(color & 0xFF);
                                data[offset + 1] = (byte)((color >> 8) & 0xFF);
                                data[offset + 2] = (byte)((color >> 16) & 0xFF);
                                data[offset + 3] = (byte)((color >> 24) & 0xFF);
                            }
                        }
                        return resource;
                    }
                default:
                    throw new InvalidDataException("Unrecognized image depth.");
            }
        }

        private Resource ReadUncompressedColorData(Ibasa.IO.BinaryReader reader, int width, int height)
        {
            switch (Header.PixelDepth)
            {
                case 16:
                    {
                        Resource resource = new Resource(new Size3i(width, height, 1), 1, 1, SharpIL.Format.B8G8R8UNorm);
                        byte[] data = resource[0, 0];
                        for (int y = 0; y < resource.Size.Height; ++y)
                        {
                            for (int x = 0; x < resource.Size.Width; ++x)
                            {
                                ushort color = reader.ReadUInt16();

                                byte r = (byte)(color & 0x1F);
                                byte g = (byte)((color >> 5) & 0x1F);
                                byte b = (byte)((color >> 10) & 0x1F);

                                int offset = x * 3 + y * 3 * resource.Size.Width;

                                data[offset + 0] = b;
                                data[offset + 1] = g;
                                data[offset + 2] = r;
                            }
                        }
                        return resource;
                    }
                case 24:
                    {
                        Resource resource = new Resource(new Size3i(width, height, 1), 1, 1, SharpIL.Format.B8G8R8UNorm);
                        byte[] data = resource[0, 0];
                        for (int y = 0; y < resource.Size.Height; ++y)
                        {
                            for (int x = 0; x < resource.Size.Width; ++x)
                            {
                                byte b = reader.ReadByte();
                                byte g = reader.ReadByte();
                                byte r = reader.ReadByte();

                                int offset = x * 3 + y * 3 * resource.Size.Width;

                                data[offset + 0] = b;
                                data[offset + 1] = g;
                                data[offset + 2] = r;
                            }
                        }
                        return resource;
                    }
                case 32:
                    {
                        Resource resource = new Resource(new Size3i(width, height, 1), 1, 1, SharpIL.Format.B8G8R8A8UNorm);
                        byte[] data = resource[0, 0];
                        for (int y = 0; y < resource.Size.Height; ++y)
                        {
                            for (int x = 0; x < resource.Size.Width; ++x)
                            {
                                byte b = reader.ReadByte();
                                byte g = reader.ReadByte();
                                byte r = reader.ReadByte();
                                byte a = reader.ReadByte();

                                int offset = x * 4 + y * 4 * resource.Size.Width;

                                data[offset + 0] = b;
                                data[offset + 1] = g;
                                data[offset + 2] = r;
                                data[offset + 3] = a;
                            }
                        }
                        return resource;
                    }
                default:
                    throw new InvalidDataException("Unrecognized image depth.");
            }
        }

        private Resource ReadUncompressedBWData(Ibasa.IO.BinaryReader reader, int width, int height)
        {
            switch (Header.PixelDepth)
            {
                case 8:
                    {
                        Resource resource = new Resource(new Size3i(width, height, 1), 1, 1, SharpIL.Format.A8UNorm);
                        byte[] data = resource[0, 0];
                        for (int y = 0; y < resource.Size.Height; ++y)
                        {
                            for (int x = 0; x < resource.Size.Width; ++x)
                            {
                                byte color = reader.ReadByte();

                                int offset = x + y * resource.Size.Width;

                                data[offset] = color;
                            }
                        }
                        return resource;
                    }
                default:
                    throw new InvalidDataException("Unrecognized image depth.");
            }
        }

        private Resource ReadRLEMappedData(Ibasa.IO.BinaryReader reader, int width, int height)
        {
            throw new NotImplementedException();
        }

        private Resource ReadRLEColorData(Ibasa.IO.BinaryReader reader, int width, int height)
        {
            throw new NotImplementedException();
        }

        private Resource ReadRLEBWData(Ibasa.IO.BinaryReader reader, int width, int height)
        {
            throw new NotImplementedException();
        }

        private void ReadFooter(Ibasa.IO.BinaryReader reader)
        {
            if (reader.PeekChar() == -1)
                return; //end of stream or cant seek no footer possible

            reader.BaseStream.Seek(-26, SeekOrigin.End);

            long extensionOffset = reader.ReadUInt32();
            long developerOffset = reader.ReadUInt32();
            string signature = new string(reader.ReadChars(18));
            if (signature != "TRUEVISION-XFILE.\0")
                return;

            if (extensionOffset != 0)
            {
                reader.BaseStream.Seek(extensionOffset, SeekOrigin.Begin);
                ReadExtension(reader);
            }
            if (developerOffset != 0)
            {
                reader.BaseStream.Seek(developerOffset, SeekOrigin.Begin);
                ReadDeveloper(reader);
            }
        }

        private void ReadExtension(Ibasa.IO.BinaryReader reader)
        {
            int size = reader.ReadUInt16();
            if (size < 495) //size can grow with new versions
                return;

            Extension.AuthorName = reader.ReadBytes(41);
            Extension.AuthorComments = reader.ReadBytes(324);
            Extension.DateTimeStampMonth = reader.ReadUInt16();
            Extension.DateTimeStampDay = reader.ReadUInt16();
            Extension.DateTimeStampYear = reader.ReadUInt16();
            Extension.DateTimeStampHour = reader.ReadUInt16();
            Extension.DateTimeStampMinute = reader.ReadUInt16();
            Extension.DateTimeStampSecond = reader.ReadUInt16();
            Extension.JobId = reader.ReadBytes(41);
            Extension.JobTimeHours = reader.ReadUInt16();
            Extension.JobTimeMinutes = reader.ReadUInt16();
            Extension.JobTimeSeconds = reader.ReadUInt16();
            Extension.SoftwareId = reader.ReadBytes(41);
            Extension.SoftwareVersionNumber = reader.ReadUInt16();
            Extension.SoftwareVersionLetter = reader.ReadByte();
            Extension.KeyColor = reader.ReadInt32();
            Extension.PixelAspectRatioN = reader.ReadUInt16();
            Extension.PixelAspectRatioD = reader.ReadUInt16();
            Extension.GammaValueN = reader.ReadUInt16();
            Extension.GammaValueD = reader.ReadUInt16();
            long colorCorrectionOffset = reader.ReadUInt32();
            long postageStampOffset = reader.ReadUInt32();
            long scanLineOffset = reader.ReadUInt32();
            Extension.Attributes = reader.ReadByte();

            //read color correction
            if (colorCorrectionOffset != 0)
            {
                reader.BaseStream.Seek(colorCorrectionOffset, SeekOrigin.Begin);
                Extension.ColorCorrection = new Vector4i[256];
                for (int i = 0; i < 256; ++i)
                {
                    int a = reader.ReadUInt16();
                    int r = reader.ReadUInt16();
                    int g = reader.ReadUInt16();
                    int b = reader.ReadUInt16();

                    Extension.ColorCorrection[i] = new Vector4i(r, g, b, a);
                }
            }
            //read scan lines
            if (scanLineOffset != 0)
            {
                reader.BaseStream.Seek(scanLineOffset, SeekOrigin.Begin);
                Extension.ScanLines = new long[Header.Height];
                for (int i = 0; i < Header.Height; ++i)
                {
                    Extension.ScanLines[i] = reader.ReadUInt32();
                }
            }
            //read postage stamp
            if (postageStampOffset != 0)
            {
                reader.BaseStream.Seek(postageStampOffset, SeekOrigin.Begin);

                int width = reader.ReadByte();
                int height = reader.ReadByte();
                Extension.PostageStamp = ReadData(reader, width, height);
            }
        }

        private void ReadDeveloper(Ibasa.IO.BinaryReader reader)
        {
            int tags = reader.ReadUInt16();

            uint[] tol = new uint[tags * 3];

            for (int i = 0; i < tags; ++i)
            {
                tol[i * 3 + 0] = reader.ReadUInt16();
                tol[i * 3 + 1] = reader.ReadUInt32();
                tol[i * 3 + 2] = reader.ReadUInt32();
            }

            for (int i = 0; i < tags; ++i)
            {
                int tag = (int)tol[i * 3 + 0];
                long offset = tol[i * 3 + 1];
                int length = (int)tol[i * 3 + 2];

                reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                DeveloperArea.Add(new KeyValuePair<int, byte[]>(tag, reader.ReadBytes(length)));
            }
        }

        public void Save(string path)
        {
            using (var stream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Save(stream);
            }
        }

        public void Save(Stream stream)
        {
            var writer = new Ibasa.IO.BinaryWriter(stream, System.Text.Encoding.ASCII);

            WriteHeader(writer);
            //WriteColorMap(writer); dont use color maps
            WriteData(writer, ImageData);
            WriteFooter(writer);
        }

        private void WriteHeader(Ibasa.IO.BinaryWriter writer)
        {
            int depth = 0;
            int type = 0;
            if (Image.Format is SharpIL.Formats.B8G8R8UNorm || Image.Format is SharpIL.Formats.R8G8B8UNorm)
            {
                depth = 24;
                type = 2; //uncompressed true color
            }
            if (Image.Format is SharpIL.Formats.B8G8R8A8UNorm || Image.Format is SharpIL.Formats.R8G8B8A8UNorm)
            {
                depth = 32;
                type = 2; //uncompressed true color
            }
            if (Image.Format is SharpIL.Formats.R8UNorm || Image.Format is SharpIL.Formats.A8UNorm)
            {
                depth = 8;
                type = 3; //uncompressed bw
            }

            writer.Write((byte)Header.ImageId.Length);
            writer.Write((byte)0); //no color map
            writer.Write((byte)type); 
            writer.Write((ushort)0); //ColourMapOrigin
            writer.Write((ushort)0); //ColorMapLength
            writer.Write((byte)0); //ColorMapDepth
            writer.Write((ushort)0); //XOrigin
            writer.Write((ushort)0); //YOrigin
            writer.Write((ushort)ImageData.Size.Width);
            writer.Write((ushort)ImageData.Size.Height);
            writer.Write((byte)depth);
            writer.Write((byte)0);
            writer.Write(Header.ImageId);
        }

        private void WriteData(Ibasa.IO.BinaryWriter writer, Resource resource)
        {
            if (Image.Format is SharpIL.Formats.B8G8R8UNorm)
            {                
                WriteData24(writer, resource, false);
            }
            if (Image.Format is SharpIL.Formats.R8G8B8UNorm)
            {
                WriteData24(writer, resource, true);
            }
            if (Image.Format is SharpIL.Formats.B8G8R8A8UNorm)
            {
                WriteData32(writer, resource, false);
            }
            if (Image.Format is SharpIL.Formats.R8G8B8A8UNorm)
            {
                WriteData32(writer, resource, true);
            }
            if (Image.Format is SharpIL.Formats.R8UNorm || Image.Format is SharpIL.Formats.A8UNorm)
            {
                WriteData8(writer, resource);
            }
        }

        private void WriteData32(Ibasa.IO.BinaryWriter writer, Resource resource, bool flip)
        {
            byte[] data = resource[0, 0];

            for (int i = 0; i < data.Length; i += 4)
            {
                byte r, g, b, a;
                if (flip)
                {
                    r = data[i + 0];
                    g = data[i + 1];
                    b = data[i + 2];
                    a = data[i + 3];
                }
                else
                {
                    b = data[i + 0];
                    g = data[i + 1];
                    r = data[i + 2];
                    a = data[i + 3];
                }

                writer.Write(b);
                writer.Write(g);
                writer.Write(r);
                writer.Write(a);
            }
        }

        private void WriteData24(Ibasa.IO.BinaryWriter writer, Resource resource, bool flip)
        {
            byte[] data = resource[0, 0];

            for (int i = 0; i < data.Length; i += 3)
            {
                byte r, g, b;
                if (flip)
                {
                    r = data[i + 0];
                    g = data[i + 1];
                    b = data[i + 2];
                }
                else
                {
                    b = data[i + 0];
                    g = data[i + 1];
                    r = data[i + 2];
                }

                writer.Write(b);
                writer.Write(g);
                writer.Write(r);
            }
        }

        private void WriteData8(Ibasa.IO.BinaryWriter writer, Resource resource)
        {
            writer.Write(resource[0, 0]);
        }

        private void WriteFooter(Ibasa.IO.BinaryWriter writer)
        {
            long developerOffset = WriteDeveloper(writer);
            long extensionOffset = WriteExtension(writer);

            writer.Write((uint)extensionOffset);
            writer.Write((uint)developerOffset);
            writer.Write(System.Text.Encoding.ASCII.GetBytes("TRUEVISION-XFILE.\0"));
        }


        private long WriteDeveloper(Ibasa.IO.BinaryWriter writer)
        {
            if (DeveloperArea.Count == 0)
                return 0;

            long[] offsets = new long[DeveloperArea.Count];

            for (int i = 0; i < DeveloperArea.Count; ++i)
            {
                offsets[i] = writer.BaseStream.Position;
                writer.Write(DeveloperArea[i].Value);
            }

            long developerOffset = writer.BaseStream.Position;
            writer.Write((ushort)DeveloperArea.Count);
            for (int i = 0; i < DeveloperArea.Count; ++i)
            {
                writer.Write((ushort)DeveloperArea[i].Key);
                writer.Write((uint)offsets[i]);
                writer.Write((uint)DeveloperArea[i].Value.Length);
            }
            return developerOffset;
        }

        private long WriteExtension(Ibasa.IO.BinaryWriter writer)
        {
            if (Extension.AllDefault)
                return 0;

            long colorCorrectionOffset = 0;
            //write color correction
            if (Extension.ColorCorrection != null)
            {
                colorCorrectionOffset = writer.BaseStream.Position;

                for (int i = 0; i < 256; ++i)
                {
                    writer.Write((ushort)Extension.ColorCorrection[i].W);
                    writer.Write((ushort)Extension.ColorCorrection[i].X);
                    writer.Write((ushort)Extension.ColorCorrection[i].Y);
                    writer.Write((ushort)Extension.ColorCorrection[i].Z);
                }
            }
            long scanLineOffset = 0;
            //read scan lines
            if(Extension.ScanLines != null)
            {
                scanLineOffset = writer.BaseStream.Position;
                for (int i = 0; i < Extension.ScanLines.Length; ++i)
                {
                    writer.Write((uint)Extension.ScanLines[i]);
                }
            }
            long postageStampOffset = 0;
            //read postage stamp
            if (Extension.PostageStamp != null)
            {
                postageStampOffset = writer.BaseStream.Position;

                writer.Write((byte)Extension.PostageStamp.Size.Width);
                writer.Write((byte)Extension.PostageStamp.Size.Height);
                WriteData(writer, Extension.PostageStamp);
            }

            long extensionOffset = writer.BaseStream.Position;
            
            writer.Write((ushort)495); //extension size
            writer.Write(Extension.AuthorName ?? new byte[41]);
            writer.Write(Extension.AuthorComments ?? new byte[324]);
            writer.Write((ushort)Extension.DateTimeStampMonth);
            writer.Write((ushort)Extension.DateTimeStampDay);
            writer.Write((ushort)Extension.DateTimeStampYear);
            writer.Write((ushort)Extension.DateTimeStampHour);
            writer.Write((ushort)Extension.DateTimeStampMinute);
            writer.Write((ushort)Extension.DateTimeStampSecond);
            writer.Write(Extension.JobId ?? new byte[41]);
            writer.Write((ushort)Extension.JobTimeHours);
            writer.Write((ushort)Extension.JobTimeMinutes);
            writer.Write((ushort)Extension.JobTimeSeconds);
            writer.Write(Extension.SoftwareId ?? new byte[41]);
            writer.Write((ushort)Extension.SoftwareVersionNumber);
            writer.Write((byte)Extension.SoftwareVersionLetter);
            writer.Write((uint)Extension.KeyColor);
            writer.Write((ushort)Extension.PixelAspectRatioN);
            writer.Write((ushort)Extension.PixelAspectRatioD);
            writer.Write((ushort)Extension.GammaValueN);
            writer.Write((ushort)Extension.GammaValueD);
            writer.Write((uint)colorCorrectionOffset);
            writer.Write((uint)postageStampOffset);
            writer.Write((uint)scanLineOffset);
            writer.Write((byte)Extension.Attributes);

            return extensionOffset;
        }
    }
}
