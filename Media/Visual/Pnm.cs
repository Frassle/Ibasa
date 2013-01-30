using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.SharpIL;
using System.IO;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Media.Visual
{
    public sealed class Pnm
    {
        Resource ImageData;

        public Resource Image
        {
            get { return ImageData; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();                

                if (!(
                    value.Format is SharpIL.Formats.R8G8B8UNorm ||
                    value.Format is SharpIL.Formats.R8UNorm ||
                    value.Format is SharpIL.Formats.R32G32B32Float ||
                    value.Format is SharpIL.Formats.R32Float))
                    throw new ArgumentException(
                        "Unsupported format. Pnm only supports R8G8B8, R8, R32G32B32 and R32.",
                        "value");

                if (value.Size.Depth > 1)
                    throw new ArgumentException(
                        "Unsupported image. Pnm does not support 3D images.",
                        "value");
                if (value.MipLevels > 1)
                    throw new ArgumentException(
                        "Unsupported image. Pnm does not support mipmaps.",
                        "value");
                if (value.ArraySize > 1)
                    throw new ArgumentException(
                        "Unsupported image. Pnm does not support arrays.",
                        "value");
                
                ImageData = value;
            }
        }

        public Pnm(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                Load(stream);
            }
        }
        public Pnm(Stream stream)
        {
            Load(stream);
        }

        private void Load(Stream stream)
        {
            int pByte = stream.ReadByte();
            int iByte = stream.ReadByte();

            if ((pByte | iByte) < 0)
                throw new EndOfStreamException();

            if (pByte != 'P')
                throw new InvalidDataException();


            int type;

            if (iByte == 'f')
            {
                type = 7;
            }
            else if (iByte == 'F')
            {
                type = 8;
            }
            else
            {
                type = iByte - '0';
            }

            if (type < 1 || type > 8)
                throw new InvalidDataException();

            BinaryReader reader = new BinaryReader(stream, Encoding.ASCII);
            int width = Parse(reader);
            int height = Parse(reader);
            int max = 0;
            if (type != 1 && type != 4)
                max = Parse(reader);
            int scale = 0;
            if (type == 7 || type == 8)
                scale = Parse(reader);

            if (scale < 0)
                throw new NotSupportedException("Does not support big endian floats.");

            switch (type)
            {
                case 1: // Bitmap
                    ImageData = ReadASCIIBitmap(reader, width, height);
                    break;
                case 2: // Graymap
                    ImageData = ReadASCIIGraymap(reader, width, height, max);
                    break;
                case 3: // Pixmap
                    ImageData = ReadASCIIPixmap(reader, width, height, max);
                    break;
                //Binary
                case 4: // Bitmap
                    ImageData = ReadBinaryBitmap(reader, width, height);
                    break;
                case 5: // Graymap
                    ImageData = ReadBinaryGraymap(reader, width, height, max);
                    break;
                case 6: // Pixmap
                    ImageData = ReadBinaryPixmap(reader, width, height, max);
                    break;
                case 7: // Float graymap
                    ImageData = ReadBinaryFloatGraymap(reader, width, height, scale);
                    break;
                case 8: // Float Pixmap
                    ImageData = ReadBinaryFloatPixmap(reader, width, height, scale);
                    break;
            }
        }

        private Resource ReadASCIIBitmap(BinaryReader reader, int width, int height)
        {
            Resource image = new Resource(new Size3i(width, height, 1), 1, 1, Format.R8UNorm);
            byte[] data = image[0, 0];

            for (int i = 0; i < data.Length; )
            {
                data[i++] = (byte)((Parse(reader) * byte.MaxValue));
            }

            return image;
        }

        private Resource ReadASCIIGraymap(BinaryReader reader, int width, int height, int max)
        {
            Resource image = new Resource(new Size3i(width, height, 1), 1, 1, Format.R8UNorm);
            byte[] data = image[0, 0];

            for (int i = 0; i < data.Length; )
            {
                data[i++] = (byte)((Parse(reader) * byte.MaxValue) / max);
            }

            return image;
        }

        private Resource ReadASCIIPixmap(BinaryReader reader, int width, int height, int max)
        {
            Resource image = new Resource(new Size3i(width, height, 1), 1, 1, Format.R8G8B8UNorm);
            byte[] data = image[0, 0];

            for (int i = 0; i < data.Length; )
            {
                data[i++] = (byte)((Parse(reader) * byte.MaxValue) / max);
                data[i++] = (byte)((Parse(reader) * byte.MaxValue) / max);
                data[i++] = (byte)((Parse(reader) * byte.MaxValue) / max);
            }

            return image;
        }

        private Resource ReadBinaryBitmap(BinaryReader reader, int width, int height)
        {
            Resource image = new Resource(new Size3i(width, height, 1), 1, 1, Format.R8UNorm);
            byte[] data = image[0, 0];

            int c = reader.PeekChar();
            if (c == -1 || char.IsWhiteSpace((char)c))
                reader.ReadChar();

            for (int i = 0; i < data.Length; i += 8)
            {
                byte bits = reader.ReadByte();

                for (int j = 0; j < 8 && i + j < data.Length; ++j)
                {
                    data[i + j] = (byte)(((bits >> (7 - j)) & 1) * 255);
                }
            }

            return image;
        }

        private Resource ReadBinaryGraymap(BinaryReader reader, int width, int height, int max)
        {
            Resource image = new Resource(new Size3i(width, height, 1), 1, 1, Format.R8UNorm);
            byte[] data = image[0, 0];

            int c = reader.PeekChar();
            if (c == -1 || char.IsWhiteSpace((char)c))
                reader.ReadChar();

            for (int i = 0; i < data.Length; )
            {
                data[i++] = (byte)((reader.ReadByte() * byte.MaxValue) / max);
            }

            return image;
        }

        private Resource ReadBinaryPixmap(BinaryReader reader, int width, int height, int max)
        {
            Resource image = new Resource(new Size3i(width, height, 1), 1, 1, Format.R8G8B8UNorm);
            byte[] data = image[0, 0];

            int c = reader.PeekChar();
            if (c == -1 || char.IsWhiteSpace((char)c))
                reader.ReadChar();

            for (int i = 0; i < data.Length; )
            {
                data[i++] = (byte)((reader.ReadByte() * byte.MaxValue) / max);
                data[i++] = (byte)((reader.ReadByte() * byte.MaxValue) / max);
                data[i++] = (byte)((reader.ReadByte() * byte.MaxValue) / max);
            }

            return image;
        }

        private Resource ReadBinaryFloatGraymap(BinaryReader reader, int width, int height, int scale)
        {
            Resource image = new Resource(new Size3i(width, height, 1), 1, 1, Format.R32Float);
            byte[] data = image[0, 0];

            int c = reader.PeekChar();
            if (c == -1 || char.IsWhiteSpace((char)c))
                reader.ReadChar();

            for (int i = 0; i < data.Length; i += 4)
            {
                BitConverter.GetBytes(data, i, reader.ReadSingle() * scale);
            }

            return image;
        }

        private Resource ReadBinaryFloatPixmap(BinaryReader reader, int width, int height, int scale)
        {
            Resource image = new Resource(new Size3i(width, height, 1), 1, 1, Format.R32G32B32Float);
            byte[] data = image[0, 0];

            int c = reader.PeekChar();
            if (c == -1 || char.IsWhiteSpace((char)c))
                reader.ReadChar();

            for (int i = 0; i < data.Length; i += 12)
            {
                BitConverter.GetBytes(data, i + 0, reader.ReadSingle() * scale);
                BitConverter.GetBytes(data, i + 4, reader.ReadSingle() * scale);
                BitConverter.GetBytes(data, i + 8, reader.ReadSingle() * scale);
            }

            return image;
        }

        private int Parse(BinaryReader reader)
        {
            while (char.IsWhiteSpace(reader.ReadChar()))
            {
                if (reader.PeekChar() == '#')
                {
                    while (reader.ReadChar() != '\n')
                    {
                    }
                }
            }

            int result = 0;
            bool negative = false;

            char c = reader.ReadChar();
            if (c == '+')
            {
                c = reader.ReadChar();
            }
            else if (c == '-')
            {
                negative = true;
                c = reader.ReadChar();
            }

            for(; char.IsDigit(c); c = reader.ReadChar())
            {
                result *= 10;
                result += (c - '0');
            }

            return negative ? -result : result;
        }

        public void Save(string path, bool ascii)
        {
            using (var stream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Save(stream, ascii);
            }
        }
        public void Save(Stream stream, bool ascii)
        {
            BinaryWriter writer = new BinaryWriter(stream, Encoding.ASCII);

            writer.Write('P');

            bool floating = Image.Format is SharpIL.Formats.R32G32B32Float || Image.Format is SharpIL.Formats.R32Float;

            if (ascii)
            {
                if (floating)
                    throw new ArgumentException("Cannot save float image as ASCII", "ascii");

                if (Image.Format is SharpIL.Formats.R8G8B8UNorm)
                    writer.Write('3');
                else
                    writer.Write('2');
            }
            else
            {
                if (Image.Format is SharpIL.Formats.R8G8B8UNorm)
                    writer.Write('6');
                else if(Image.Format is SharpIL.Formats.R8UNorm)
                    writer.Write('5');
                else if (Image.Format is SharpIL.Formats.R32G32B32Float)
                    writer.Write('F');
                else if (Image.Format is SharpIL.Formats.R32Float)
                    writer.Write('f');
            }

            writer.Write(Environment.NewLine);
            writer.Write(Image.Size.Width.ToString());
            writer.Write(" ");
            writer.Write(Image.Size.Height.ToString());
            writer.Write(Environment.NewLine);
            writer.Write(floating ? "1" : "255");
            writer.Write(Environment.NewLine);

            byte[] data = Image[0, 0];
            
            if (ascii)
            {
                if (Image.Format is SharpIL.Formats.R8G8B8UNorm)
                {
                    for (int y = Image.Size.Height - 1; y >= 0; --y)
                    {
                        for (int x = 0; x < Image.Size.Width; ++x)
                        {
                            int offset = (3 * x) + (y * 3 * Image.Size.Width);

                            writer.Write(data[offset + 0].ToString());
                            writer.Write(" ");
                            writer.Write(data[offset + 1].ToString());
                            writer.Write(" ");
                            writer.Write(data[offset + 2].ToString());
                            writer.Write(" ");
                        }
                        writer.Write(Environment.NewLine);
                    }
                }
                else
                {
                    for (int y = Image.Size.Height - 1; y >= 0; --y)
                    {
                        for (int x = 0; x < Image.Size.Width; ++x)
                        {
                            int offset = x + y * Image.Size.Width;

                            writer.Write(data[offset].ToString());
                            writer.Write(" ");
                        }
                        writer.Write(Environment.NewLine);
                    }
                }
            }
            else
            {
                writer.Write(data, 0, data.Length);
            }

            writer.Close();
        }
    }
}
