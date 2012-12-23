using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ibasa.SharpIL;
using Ibasa.Valve.Material;
using Ibasa.IO;
using System.Diagnostics.Contracts;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Valve.Package
{
    public sealed class Wad : IEnumerable<KeyValuePair<string, Resource>>
    {
        internal struct Lump
        {
            public uint Offset;
            public uint CompressedSize;
            public uint Size;
            public byte Type;
            public byte Compression;
            public string Name;
        }

        Ibasa.IO.BinaryReader Reader;

        public long Count { get; private set; }
        long Directory;

        public Wad(string path):
            this(File.OpenRead(path))
        { }
        public Wad(Stream stream)
        {
            Contract.Requires(stream.CanSeek, "stream must be seekable.");
            Contract.Requires(stream.CanRead, "stream must be readable.");

            Reader = new Ibasa.IO.BinaryReader(stream, Encoding.ASCII);

            string signature = Encoding.ASCII.GetString(Reader.ReadBytes(4));

            if (signature != "WAD3")
                throw new InvalidDataException("File signature does not match 'WAD3'.");

            Count = Reader.ReadUInt32();
            Directory = Reader.ReadUInt32();
        }

        static string Trim(string str)
        {
            int nullbyte = str.IndexOf('\0');
            return nullbyte == -1 ? str : str.Substring(0, nullbyte);
        }

        Lump GetLump(long index)
        {  
            Reader.Seek(Directory + index * 32, SeekOrigin.Begin);

            Lump lump;

            lump.Offset = Reader.ReadUInt32();
            lump.CompressedSize = Reader.ReadUInt32();
            lump.Size = Reader.ReadUInt32();
            lump.Type = Reader.ReadByte();
            lump.Compression = Reader.ReadByte();
            Reader.Seek(2, SeekOrigin.Current); //2 bytes padding
            lump.Name = Trim(Encoding.ASCII.GetString(Reader.ReadBytes(16)));

            return lump;
        }

        Resource GetResource(Lump lump)
        {
            Reader.Seek(lump.Offset, SeekOrigin.Begin);

            if (lump.Type == 0x40 || lump.Type == 0x43)
            {
                string name = Trim(Encoding.ASCII.GetString(Reader.ReadBytes(16)));
            }

            int width = Reader.ReadInt32();
            int height = Reader.ReadInt32();

            int mips = lump.Type == 0x40 || lump.Type == 0x43 ? 4 : 1;

            Resource resource = new Resource(new Size3i(width, height, 1), mips, 1, Format.R8G8B8A8UNorm);
                
            if(lump.Type == 0x40 || lump.Type == 0x43)
            {
                long offset0 = Reader.ReadUInt32();
                long offset1 = Reader.ReadUInt32();
                long offset2 = Reader.ReadUInt32();
                long offset3 = Reader.ReadUInt32();
            }

            byte[][] images = new byte[mips][];
            images[0] = Reader.ReadBytes(width * height); 
                
            if (lump.Type == 0x40 || lump.Type == 0x43)
            {
                images[1] = Reader.ReadBytes((width / 2) * (height / 2));
                images[2] = Reader.ReadBytes((width / 4) * (height / 4));
                images[3] = Reader.ReadBytes((width / 8) * (height / 8));
            }

            Reader.Seek(2, SeekOrigin.Current);

            byte[] pallet;
            if (lump.Type == 0x42 || lump.Type == 0x43)
                pallet = Reader.ReadBytes(256 * 3);
            else
            {
                pallet = new byte[256 * 3];
                for (int i = 0; i < 256; ++i)
                {
                    int palletIndex = i * 3;
                    pallet[palletIndex + 0] = (byte)i;
                    pallet[palletIndex + 1] = (byte)i;
                    pallet[palletIndex + 2] = (byte)i;
                }
            }

            for (int mipSlice = 0; mipSlice < mips; ++mipSlice)
            {
                byte[] image = images[mipSlice];
                byte[] data = resource[mipSlice, 0];

                for (int i = 0; i < image.Length; ++i)
                {
                    int palletIndex = image[i] * 3;
                    int dataIndex = i * 3;

                    data[dataIndex + 0] = pallet[palletIndex + 0];
                    data[dataIndex + 1] = pallet[palletIndex + 1];
                    data[dataIndex + 2] = pallet[palletIndex + 2];
                }
            }

            return resource;
        }

        public Resource this[string name]
        {
            get
            {
                if (name == null)
                    throw new ArgumentNullException("name");

                for (long i = 0; i < Count; ++i)
                {
                    Lump lump = GetLump(i);

                    if (string.Equals(lump.Name, name, StringComparison.OrdinalIgnoreCase))
                        return GetResource(lump);
                }

                return null;
            }
        }

        public KeyValuePair<string, Resource> this[long index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException("index");

                Lump lump = GetLump(index);

                return new KeyValuePair<string,Resource>(lump.Name, GetResource(lump));
            }
        }

        public IEnumerator<KeyValuePair<string, Resource>> GetEnumerator()
        {
            for (long i = 0; i < Count; ++i)
            {
                yield return this[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
