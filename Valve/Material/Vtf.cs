using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;
using System.IO;
using Ibasa.Collections.Immutable;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Valve.Material
{
    /// <summary>
    /// Parse, save and modify VTFs (Valve Texture Format).
    /// </summary>
    [Serializable]
    public sealed class Vtf
    {
        [Flags]
        public enum ResourceFlags
        {
            None = 0,
            HasNoDataChunk = 0x02 << 12,

            Mask = 0xFF << 12,
        }
        public enum ResourceType
        {
            LowResImage = 0x01,
            Image = 0x30,

            Sheet = 0x10,

            Mask = 0xFFFFFF,
        }        
        public struct Resource
        {
            public readonly int Data;
            public readonly ResourceType Type;
            public readonly ResourceFlags Flags;

            public Resource(ResourceType type, ResourceFlags flags, int data)
            {
                Type = type;
                Flags = flags;
                Data = data;
            }
        }

        public int VersionMajor { get; private set; }
        public int VersionMinor { get; private set; }
        private int HeaderSize { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public TextureFlags Flags { get; private set; }
        public int Frames { get; private set; }
        public int StartFrame { get; private set; }
        public Vector3f Reflectivity { get; private set; }
        public double BumpmapScale { get; private set; }
        public ImageFormat ImageFormat { get; private set; }
        public int MipmapCount { get; private set; }
        public ImageFormat LowResImageFormat { get; private set; }
        public int LowResImageWidth { get; private set; }
        public int LowResImageHeight { get; private set; }
        public int Depth { get; private set; }
        public ImmutableArray<Resource> Resources { get; private set; }

        public SharpIL.Resource LowResImage { get; private set; }
        public SharpIL.Resource HighResImage { get; private set; }

        //env map has 7 faces unless StartFrame == 0xFFFF then it has no sphere map
        public int Faces { get { return (Flags.HasFlag(TextureFlags.EnvMap)) ? (StartFrame == 0xFFFF ? 6 : 7) : 1; } }


        //char		signature[4];		// File signature ("VTF\0").
        //unsigned int	version[2];		// version[0].version[1] (currently 7.2).
        //unsigned int	headerSize;		// Size of the header struct (16 byte aligned; currently 80 bytes).
        //unsigned short	width;			// Width of the largest mipmap in pixels. Must be a power of 2.
        //unsigned short	height;			// Height of the largest mipmap in pixels. Must be a power of 2.
        //unsigned int	flags;			// VTF flags.
        //unsigned short	frames;			// Number of frames, if animated (1 for no animation).
        //unsigned short	firstFrame;		// First frame in animation (0 based).
        //unsigned char	padding0[4];		// reflectivity padding (16 byte alignment).
        //float		reflectivity[3];	// reflectivity vector.
        //unsigned char	padding1[4];		// reflectivity padding (8 byte packing).
        //float		bumpmapScale;		// Bumpmap scale.
        //unsigned int	highResImageFormat;	// High resolution image format.
        //unsigned char	mipmapCount;		// Number of mipmaps.
        //unsigned int	lowResImageFormat;	// Low resolution image format (always DXT1).
        //unsigned char	lowResImageWidth;	// Low resolution image width.
        //unsigned char	lowResImageHeight;	// Low resolution image height.
        //unsigned short	depth;			// Depth of the largest mipmap in pixels.
						// Must be a power of 2. Can be 0 or 1 for a 2D texture (v7.2 only).

        public Vtf(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                Load(stream);
            }
        }
        public Vtf(Stream stream)
        {
            Load(stream);
        }

        public void Load(Stream stream)
        {
            var reader = new Ibasa.IO.BinaryReader(stream, Encoding.ASCII);

            string signature = Encoding.ASCII.GetString(reader.ReadBytes(4));

            if (signature != "VTF\0")
                throw new InvalidDataException("File signature does not match 'VTF'.");

            VersionMajor = reader.ReadInt32();
            VersionMinor = reader.ReadInt32();

            if (VersionMajor != 7 && (VersionMinor < 0 || VersionMinor > 5))
                throw new InvalidDataException(string.Format("File version {0}.{1} does not match 7.0 to 7.5.", VersionMajor, VersionMinor));

            HeaderSize = reader.ReadInt32();

            ReadHeader70(reader);
            ReadHeader72(reader);
            ReadHeader73(reader);

            ReadData(reader);
        }

        private void ReadHeader70(Ibasa.IO.BinaryReader reader)
        {
            Width = reader.ReadUInt16();
            Height = reader.ReadUInt16();
            Flags = (TextureFlags)reader.ReadInt32();
            Frames = reader.ReadUInt16();
            StartFrame = reader.ReadUInt16();
            reader.ReadBytes(4); //skip padding
            Reflectivity = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            reader.ReadBytes(4); //skip padding
            BumpmapScale = reader.ReadSingle();
            ImageFormat = (ImageFormat)reader.ReadInt32();
            MipmapCount = reader.ReadByte();
            LowResImageFormat = (ImageFormat)reader.ReadInt32();
            LowResImageWidth = reader.ReadByte();
            LowResImageHeight = reader.ReadByte();
        }

        private void ReadHeader72(Ibasa.IO.BinaryReader reader)
        {
            if (VersionMinor >= 2)
            {
                Depth = reader.ReadUInt16();
                Depth = Depth == 0 ? 1 : Depth; //map 0 to 1
            }
            else
            {
                Depth = 1;
                reader.ReadBytes(1); //skip padding
            }
        }

        private void ReadHeader73(Ibasa.IO.BinaryReader reader)
        {
            if (VersionMinor >= 3)
            {
                reader.ReadBytes(3); //skip padding
                int resourceCount = reader.ReadInt32();
                reader.ReadBytes(8); //skip padding

                var resources = new Resource[resourceCount];

                for (int res = 0; res < resourceCount; ++res)
                {
                    int type = reader.ReadInt32();
                    resources[res] = new Resource(
                        (ResourceType)type & ResourceType.Mask,
                        (ResourceFlags)type & ResourceFlags.Mask, 
                        reader.ReadInt32());
                }

                Resources = new ImmutableArray<Resource>(resources);
            }
            else if(VersionMinor == 2)
            {
                //skip padding
                reader.ReadBytes(15); //skip padding

                Resources = new ImmutableArray<Resource>(0);
            }
        }

        private void ReadData(Ibasa.IO.BinaryReader reader)
        {
            LowResImage = new SharpIL.Resource(
                new Size3i(LowResImageWidth, LowResImageHeight, 1), 1, 1, EncodingFromFormat(LowResImageFormat));
            HighResImage = new SharpIL.Resource(
                new Size3i(Width, Height, Depth), MipmapCount, Frames * Faces, EncodingFromFormat(ImageFormat));

            if (VersionMinor < 3)
            {
                reader.Read(LowResImage[0, 0], 0, LowResImage[0, 0].Length);

                for (int mip = MipmapCount-1; mip >=0 ; --mip)
                {
                    Size3f size = HighResImage.ComputeMipSliceSize(mip);

                    for (int frame = 0; frame < Frames; ++frame)
                    {
                        for (int face = 0; face < Faces; ++face)
                        {
                            int array = face + (Faces * frame);
                            reader.Read(
                                HighResImage[mip, array], 0,
                                HighResImage[mip, array].Length);
                        }
                    }
                }
            }
            else
            {
                //low res
                int lowResOffset = Resources.Find((resource) => resource.Type == ResourceType.LowResImage).Data;
                reader.Seek((uint)lowResOffset, SeekOrigin.Begin);
                reader.Read(LowResImage[0, 0], 0, LowResImage[0, 0].Length);

                //high res
                int highResOffset = Resources.Find((resource) => resource.Type == ResourceType.Image).Data;
                reader.Seek((uint)highResOffset, SeekOrigin.Begin);

                for (int mip = MipmapCount - 1; mip >= 0; --mip)
                {
                    Size3f size = HighResImage.ComputeMipSliceSize(mip);

                    for (int frame = 0; frame < Frames; ++frame)
                    {
                        for (int face = 0; face < Faces; ++face)
                        {
                            byte[] data = HighResImage[mip, face + (Faces * frame)];
                            reader.Read(data, 0, data.Length);
                        }
                    }
                }
            }
        }

        private static Ibasa.SharpIL.Format EncodingFromFormat(ImageFormat format)
        {
            switch (format)
            {
                case ImageFormat.None:
                    return null;
                case ImageFormat.DXT1:
                case ImageFormat.DXT1_OneBitAlpha:
                    return Ibasa.SharpIL.Format.BC1;
                case ImageFormat.DXT3:
                    return Ibasa.SharpIL.Format.BC2;
                case ImageFormat.DXT5:
                    return Ibasa.SharpIL.Format.BC3;
                case ImageFormat.BGR565:
                    return Ibasa.SharpIL.Format.B5G6R5UNorm;
                case ImageFormat.BGRA5551:
                case ImageFormat.BGRX5551:
                    return Ibasa.SharpIL.Format.B5G5R5A1UNorm;
                case ImageFormat.RGBA16161616F:
                    return Ibasa.SharpIL.Format.R16G16B16A16Float;
                case ImageFormat.RGBA16161616:
                    return Ibasa.SharpIL.Format.R16G16B16A16UNorm;
                case ImageFormat.RGBA8888:
                    return Ibasa.SharpIL.Format.R8G8B8A8UNorm;
                case ImageFormat.BGRA8888:
                case ImageFormat.BGRX8888:
                    return Ibasa.SharpIL.Format.B8G8R8A8UNorm;
                case ImageFormat.UVWQ8888:
                    return Ibasa.SharpIL.Format.R8G8B8A8UNorm;
                case ImageFormat.A8:
                    return Ibasa.SharpIL.Format.A8UNorm;
                case ImageFormat.I8:
                    return Ibasa.SharpIL.Format.R8UNorm;
                case ImageFormat.UV88:
                    return Ibasa.SharpIL.Format.R8G8Norm;
                case ImageFormat.BGR888:
                case ImageFormat.BGR888_Bluescreen:
                    return Ibasa.SharpIL.Format.B8G8R8UNorm;
                case ImageFormat.RGB888:
                case ImageFormat.RGB888_Bluescreen:
                    return Ibasa.SharpIL.Format.R8G8B8UNorm;

                case ImageFormat.ABGR8888:
                case ImageFormat.ARGB8888:
                case ImageFormat.RGB565:
                    throw new NotSupportedException("Misordered channel formats not supported.");
                case ImageFormat.BGRA4444:
                    throw new NotSupportedException("4bit formats not supported.");
                case ImageFormat.UVLX8888:
                case ImageFormat.IA88:
                    throw new NotSupportedException("Mixed formats not supported.");
                case ImageFormat.P8:
                    throw new NotSupportedException("Paletted formats not supported.");
                default:
                    throw new NotSupportedException("ImageFormat not supported.");
            }
        }
    }
}
