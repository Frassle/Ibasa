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
    public sealed class DirectDrawSurface
    {
        [Flags]
        private enum PixelFlagsEnum
        {
            /// <summary>
            /// Texture contains alpha data; dwRGBAlphaBitMask contains valid data.
            /// </summary>	
            AlphaPixels = 0x1,
            /// <summary>
            /// Used in some older DDS files for alpha channel only uncompressed data (dwRGBBitCount contains the alpha channel bitcount; dwABitMask contains valid data).
            /// </summary>
            Alpha = 0x2,
            /// <summary>
            /// Texture contains compressed RGB data; dwFourCC contains valid data.
            /// </summary>
            FourCC = 0x4,
            /// <summary>
            /// Texture contains uncompressed RGB data; dwRGBBitCount and the RGB masks (dwRBitMask, dwRBitMask, dwRBitMask) contain valid data.
            /// </summary>
            Rgb = 0x40,
            /// <summary>
            /// Used in some older DDS files for YUV uncompressed data (dwRGBBitCount contains the YUV bit count; dwRBitMask contains the Y mask, dwGBitMask contains the U mask, dwBBitMask contains the V mask).
            /// </summary>
            Yuv = 0x200,
            /// <summary>
            /// Used in some older DDS files for single channel color uncompressed data (dwRGBBitCount contains the luminance channel bit count; dwRBitMask contains the channel mask). Can be combined with DDPF_ALPHAPIXELS for a two channel DDS file.
            /// </summary>
            Luminance = 0x20000,
        }

        [Flags]
        private enum HeaderFlagsEnum
        {
            /// <summary>
            /// Required in every .dds file.
            /// </summary>
            Caps = 0x1,
            /// <summary>
            /// Required in every .dds file.
            /// </summary>
            Height = 0x2,
            /// <summary>
            /// Required in every .dds file.
            /// </summary>
            Width = 0x4,
            /// <summary>
            /// Required when pitch is provided for an uncompressed texture.
            /// </summary>
            Pitch = 0x8,
            /// <summary>
            /// Required in every .dds file.
            /// </summary>
            PixelFormat = 0x1000,
            /// <summary>
            /// Required in a mipmapped texture.
            /// </summary>
            MipmapCount = 0x20000,
            /// <summary>
            /// Required when pitch is provided for a compressed texture.
            /// </summary>
            LinearSize = 0x80000,
            /// <summary>
            /// Required in a depth texture.
            /// </summary>
            Depth = 0x800000,
        }
        [Flags]
        private enum SurfaceFlagsEnum
        {
            /// <summary>
            /// Optional; must be used on any file that contains more than one surface (a mipmap, a cubic environment map, or mipmapped volume texture).
            /// </summary>
            Complex = 0x8,
            /// <summary>
            /// Optional; should be used for a mipmap.
            /// </summary>
            Mipmap = 0x400000,
            /// <summary>
            /// Required.
            /// </summary>
            Texture = 0x1000
        }
        [Flags]
        private enum CubemapFlagsEnum
        {
            /// <summary>
            /// Required for a cube map.
            /// </summary>
            Cubemap = 0x200,
            /// <summary>
            /// Required when these surfaces are stored in a cube map.
            /// </summary>
            PositiveX = 0x400,
            /// <summary>
            /// Required when these surfaces are stored in a cube map.
            /// </summary>
            NegativeX = 0x800,
            /// <summary>
            /// Required when these surfaces are stored in a cube map.
            /// </summary>
            PositiveY = 0x1000,
            /// <summary>
            /// Required when these surfaces are stored in a cube map.
            /// </summary>
            NegativeY = 0x2000,
            /// <summary>
            /// Required when these surfaces are stored in a cube map.
            /// </summary>
            PositiveZ = 0x4000,
            /// <summary>
            /// Required when these surfaces are stored in a cube map.
            /// </summary>
            NegativeZ = 0x8000,
            /// <summary>
            /// Required for a volume texture.
            /// </summary>
            Volume = 0x200000,
            /// <summary>
            /// Required when these surfaces are stored in a cube map.
            /// </summary>
            AllFaces = PositiveX | PositiveY | PositiveZ | NegativeX | NegativeY | NegativeZ,
        }
        private enum ResourceDimensionEnum
        {
            /// <summary>
            /// Resource is a 1D texture.
            /// </summary>
            Texture1D = 2,
            /// <summary>
            /// Resource is a 2D texture.
            /// </summary>
            Texture2D = 3,
            /// <summary>
            /// Resource is a 3D texture.
            /// </summary>
            Texture3D = 4,
        }
        [Flags]
        private enum MiscFlagsEnum
        {
            /// <summary>
            /// Indicates a 2D texture is a cube-map texture.
            /// </summary>
            TextureCube = 0x4,
        }

        private HeaderFlagsEnum HeaderFlags;
        private int Height;
        private int Width;
        private int Pitch;
        private int Depth;
        private int MipmapCount;
        private SurfaceFlagsEnum SurfaceFlags;
        private CubemapFlagsEnum CubemapFlags;
        private DxgiFormat DxgiFormat;
        private ResourceDimensionEnum ResourceDimension;
        private MiscFlagsEnum MiscFlags;
        private int ArraySize;

        private PixelFlagsEnum PixelFlags;
        private FourCC FourCC;
        private int RGBBitCount;
        private int RBitMask;
        private int GBitMask;
        private int BBitMask;
        private int ABitMask;

        private bool IsDX10Mode
        {
            get { return (PixelFlags.HasFlag(PixelFlagsEnum.FourCC) && FourCC == new FourCC("DX10")); }
        }

        public Resource Image { get; set; }
        private bool Cubemap;
        public bool IsCubemap
        {
            get { return Cubemap; }
            set 
            {
                if (Image.Size.Depth > 1)
                    throw new InvalidOperationException("Volume textures cannot be cubemaps.");
                Cubemap = value;
            }
        }

        public DirectDrawSurface(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                Load(stream);
            }
        }
        public DirectDrawSurface(Stream stream)
        {
            Load(stream);
        }

        private void Load(Stream stream)
        {
            var reader = new Ibasa.IO.BinaryReader(stream, Encoding.ASCII);

            FourCC signature = new FourCC(reader.ReadInt32());

            if (signature != new FourCC("DDS "))
                throw new InvalidDataException("File signature does not match 'DDS '.");

            int headerSize = reader.ReadInt32();
            if (headerSize != 124)
                throw new InvalidDataException(string.Format("Header size {0} does not match 124.", headerSize));

            ReadHeader(reader);
            ReadDx10Header(reader);
            FixupInternalState();
            Cubemap = CubemapFlags.HasFlag(CubemapFlagsEnum.Cubemap);
            ReadData(reader);
        }

        private void FixupInternalState()
        {
            HeaderFlags |= (HeaderFlagsEnum.Caps | HeaderFlagsEnum.Height | HeaderFlagsEnum.Width | HeaderFlagsEnum.PixelFormat);
            SurfaceFlags |= SurfaceFlagsEnum.Texture;

            //cubemap
            if(CubemapFlags != CubemapFlagsEnum.Volume && CubemapFlags != 0)
            {
                CubemapFlags |= CubemapFlagsEnum.Cubemap;
                SurfaceFlags |= SurfaceFlagsEnum.Complex;
                if (IsDX10Mode)
                {
                    CubemapFlags |= CubemapFlagsEnum.AllFaces;
                    MiscFlags |= MiscFlagsEnum.TextureCube;
                }
            }
            if(IsDX10Mode && MiscFlags.HasFlag(MiscFlagsEnum.TextureCube))
            {
                
                CubemapFlags |= CubemapFlagsEnum.Cubemap| CubemapFlagsEnum.AllFaces;
                SurfaceFlags |= SurfaceFlagsEnum.Complex;
            }

            //volume textures
            if(HeaderFlags.HasFlag(HeaderFlagsEnum.Depth) || CubemapFlags.HasFlag(CubemapFlagsEnum.Volume))
            {
                HeaderFlags |= HeaderFlagsEnum.Depth;
                CubemapFlags |= CubemapFlagsEnum.Volume;
                SurfaceFlags |= SurfaceFlagsEnum.Complex;
            }

            //sanatize pixel flags
            if(PixelFlags.HasFlag(PixelFlagsEnum.Rgb) && PixelFlags.HasFlag(PixelFlagsEnum.AlphaPixels))
                PixelFlags = PixelFlagsEnum.Rgb | PixelFlagsEnum.AlphaPixels;
            else if(PixelFlags.HasFlag(PixelFlagsEnum.Rgb))
                PixelFlags = PixelFlagsEnum.Rgb;
            else if(PixelFlags.HasFlag(PixelFlagsEnum.Luminance) && PixelFlags.HasFlag(PixelFlagsEnum.AlphaPixels))
                PixelFlags = PixelFlagsEnum.Luminance | PixelFlagsEnum.AlphaPixels;
            else if(PixelFlags.HasFlag(PixelFlagsEnum.Luminance))
                PixelFlags = PixelFlagsEnum.Luminance;
            else if(PixelFlags.HasFlag(PixelFlagsEnum.FourCC))
                PixelFlags = PixelFlagsEnum.FourCC;
            else if(PixelFlags.HasFlag(PixelFlagsEnum.Yuv))
                PixelFlags = PixelFlagsEnum.Yuv;
            else if(PixelFlags.HasFlag(PixelFlagsEnum.Alpha))
                PixelFlags = PixelFlagsEnum.Alpha;

            if (!PixelFlags.HasFlag(PixelFlagsEnum.FourCC))
            { 
                //uncompressed
                HeaderFlags |= HeaderFlagsEnum.Pitch;
                HeaderFlags &= ~HeaderFlagsEnum.LinearSize;
            }
            else
            {
                //dont know
                //HeaderFlags |= HeaderFlagsEnum.LinearSize;
                //HeaderFlags &= ~HeaderFlagsEnum.Pitch;
            }

            if (IsDX10Mode)
            {
                if(ResourceDimension == ResourceDimensionEnum.Texture3D || HeaderFlags.HasFlag(HeaderFlagsEnum.Depth))
                {
                    ResourceDimension = ResourceDimensionEnum.Texture3D;
                    HeaderFlags |= HeaderFlagsEnum.Depth;
                    CubemapFlags |= CubemapFlagsEnum.Volume;
                    SurfaceFlags |= SurfaceFlagsEnum.Complex;
                }
            }

            Width = Math.Max(Width, 1);
            Height = Math.Max(Height, 1);
            Depth = HeaderFlags.HasFlag(HeaderFlagsEnum.Depth) ? Math.Max(Depth, 1) : 0;
            MipmapCount = HeaderFlags.HasFlag(HeaderFlagsEnum.MipmapCount) ? Math.Max(MipmapCount, 1) : 0;
        }

        private void ReadHeader(Ibasa.IO.BinaryReader reader)
        {
            HeaderFlags = (HeaderFlagsEnum)reader.ReadInt32();
            Height = reader.ReadInt32();
            Width = reader.ReadInt32();
            Pitch = reader.ReadInt32();
            Depth = reader.ReadInt32();
            MipmapCount = reader.ReadInt32();
            reader.ReadBytes(44); //skip padding

            int pixelSize = reader.ReadInt32();
            if (pixelSize != 32)
                throw new InvalidDataException(string.Format("Pixel format size {0} does not match 32.", pixelSize));

            PixelFlags = (PixelFlagsEnum)reader.ReadInt32();
            FourCC = new FourCC(reader.ReadInt32());
            RGBBitCount = reader.ReadInt32();
            RBitMask = reader.ReadInt32();
            GBitMask = reader.ReadInt32();
            BBitMask = reader.ReadInt32();
            ABitMask = reader.ReadInt32();

            SurfaceFlags = (SurfaceFlagsEnum)reader.ReadInt32();
            CubemapFlags = (CubemapFlagsEnum)reader.ReadInt32();

            reader.ReadBytes(12); //skip padding
        }

        private void ReadDx10Header(Ibasa.IO.BinaryReader reader)
        {
            if (!IsDX10Mode)
                return;

            DxgiFormat = (DxgiFormat)reader.ReadInt32();
            ResourceDimension = (ResourceDimensionEnum)reader.ReadInt32();
            MiscFlags = (MiscFlagsEnum)reader.ReadInt32();
            ArraySize = reader.ReadInt32();
            reader.ReadBytes(4); //skip padding
        }

        private Format SelectLoadFormat()
        {
            if (IsDX10Mode)
            {
                //select a dxgi format
                switch (DxgiFormat)
                {
                    case DxgiFormat.R32G32B32A32_Float:
                        return SharpIL.Format.R32G32B32A32Float;
                    case DxgiFormat.R32G32B32A32_UInt:
                        return SharpIL.Format.R32G32B32A32UInt;
                    case DxgiFormat.R32G32B32A32_SInt:
                        return SharpIL.Format.R32G32B32A32Int;
                    case DxgiFormat.R32G32B32_Float:
                        return SharpIL.Format.R32G32B32Float;
                    case DxgiFormat.R32G32B32_UInt:
                        return SharpIL.Format.R32G32B32UInt;
                    case DxgiFormat.R32G32B32_SInt:
                        return SharpIL.Format.R32G32B32Int;
                    case DxgiFormat.R16G16B16A16_Float:
                        return SharpIL.Format.R16G16B16A16Float;
                    case DxgiFormat.R16G16B16A16_UNorm:
                        return SharpIL.Format.R16G16B16A16UNorm;
                    case DxgiFormat.R16G16B16A16_UInt:
                        return SharpIL.Format.R16G16B16A16UInt;
                    case DxgiFormat.R16G16B16A16_SNorm:
                        return SharpIL.Format.R16G16B16A16SNorm;
                    case DxgiFormat.R16G16B16A16_SInt:
                        return SharpIL.Format.R16G16B16A16Int;
                    case DxgiFormat.R32G32_Float:
                        return SharpIL.Format.R32G32Float;
                    case DxgiFormat.R32G32_UInt:
                        return SharpIL.Format.R32G32UInt;
                    case DxgiFormat.R32G32_SInt:
                        return SharpIL.Format.R32G32Int;
                    case DxgiFormat.R8G8B8A8_UNorm:
                    case DxgiFormat.R8G8B8A8_UNorm_SRGB:
                        return SharpIL.Format.R8G8B8A8UNorm;
                    case DxgiFormat.R8G8B8A8_UInt:
                        return SharpIL.Format.R8G8B8A8UInt;
                    case DxgiFormat.R8G8B8A8_SNorm:
                        return SharpIL.Format.R8G8B8A8UNorm;
                    case DxgiFormat.R8G8B8A8_SInt:
                        return SharpIL.Format.R8G8B8A8Int;
                    case DxgiFormat.R16G16_Float:
                        return SharpIL.Format.R16G16Float;
                    case DxgiFormat.R16G16_UNorm:
                        return SharpIL.Format.R16G16UNorm;
                    case DxgiFormat.R16G16_UInt:
                        return SharpIL.Format.R16G16UInt;
                    case DxgiFormat.R16G16_SNorm:
                        return SharpIL.Format.R16G16Norm;
                    case DxgiFormat.R16G16_SInt:
                        return SharpIL.Format.R16G16Int;
                    case DxgiFormat.D32_Float:
                    case DxgiFormat.R32_Float:
                        return SharpIL.Format.R32Float;
                    case DxgiFormat.R32_UInt:
                        return SharpIL.Format.R32UInt;
                    case DxgiFormat.R32_SInt:
                        return SharpIL.Format.R32Int;
                    case DxgiFormat.R8G8_UNorm:
                        return SharpIL.Format.R8G8UNorm;
                    case DxgiFormat.R8G8_UInt:
                        return SharpIL.Format.R8G8UInt;
                    case DxgiFormat.R8G8_SNorm:
                        return SharpIL.Format.R8G8Norm;
                    case DxgiFormat.R8G8_SInt:
                        return SharpIL.Format.R8G8Int;
                    case DxgiFormat.R16_Float:
                        return SharpIL.Format.R16Float;
                    case DxgiFormat.D16_UNorm:
                    case DxgiFormat.R16_UNorm:
                        return SharpIL.Format.R16UNorm;
                    case DxgiFormat.R16_UInt:
                        return SharpIL.Format.R16UInt;
                    case DxgiFormat.R16_SNorm:
                        return SharpIL.Format.R16Norm;
                    case DxgiFormat.R16_SInt:
                        return SharpIL.Format.R16Int;
                    case DxgiFormat.R8_UNorm:
                        return SharpIL.Format.R8UNorm;
                    case DxgiFormat.R8_UInt:
                        return SharpIL.Format.R8UInt;
                    case DxgiFormat.R8_SNorm:
                        return SharpIL.Format.R8Norm;
                    case DxgiFormat.R8_SInt:
                        return SharpIL.Format.R8Int;
                    case DxgiFormat.A8_UNorm:
                        return SharpIL.Format.A8UNorm;
                    case DxgiFormat.BC1_UNorm:
                    case DxgiFormat.BC1_UNorm_SRGB:
                        return SharpIL.Format.BC1;
                    //case DxgiFormat.BC2_UNorm:
                    //case DxgiFormat.BC2_UNorm_SRGB:
                    //    return SharpIL.Format.BC2;
                    //case DxgiFormat.BC3_UNorm:
                    //case DxgiFormat.BC3_UNorm_SRGB:
                    //    return SharpIL.Format.BC3;
                    //case DxgiFormat.BC4_UNorm:
                    //    return SharpIL.Format.BC4;
                    //case DxgiFormat.BC4_SNorm:
                    //    return SharpIL.Format.BC4Norm;
                    //case DxgiFormat.BC5_UNorm:
                    //    return SharpIL.Format.BC5;
                    //case DxgiFormat.BC5_SNorm:
                    //    return SharpIL.Format.BC5Norm;
                    case DxgiFormat.B5G6R5_UNorm:
                        return SharpIL.Format.B5G6R5UNorm;
                    case DxgiFormat.B5G5R5A1_UNorm:
                        return SharpIL.Format.B5G5R5A1UNorm;
                    case DxgiFormat.B8G8R8X8_UNorm:
                    case DxgiFormat.B8G8R8X8_UNorm_SRGB:
                    case DxgiFormat.B8G8R8A8_UNorm:
                    case DxgiFormat.B8G8R8A8_UNorm_SRGB:
                        return SharpIL.Format.B8G8R8A8UNorm;
                    //case DxgiFormat.BC6H_UF16:
                    //    return SharpIL.Format.BC6U;
                    //case DxgiFormat.BC6H_SF16:
                    //    return SharpIL.Format.BC6S;
                    //case DxgiFormat.BC7_UNorm:
                    //case DxgiFormat.BC7_UNorm_SRGB:
                    //    return SharpIL.Format.BC7;
                }
            }
            else if(PixelFlags.HasFlag(PixelFlagsEnum.FourCC))
            {
                //FourCC code
                if (FourCC == "DXT1")
                    return SharpIL.Format.BC1;
                if (FourCC == "DXT2" || FourCC == "DXT3")
                    return SharpIL.Format.BC2;
                if (FourCC == "DXT4" || FourCC == "DXT5")
                    return SharpIL.Format.BC3;
            }

            throw new NotSupportedException("Unsupported format.");
        }

        private void ReadData(Ibasa.IO.BinaryReader reader)
        {
            int depth = (HeaderFlags.HasFlag(HeaderFlagsEnum.Depth) ? Depth : 1);
            int mipmapCount = (HeaderFlags.HasFlag(HeaderFlagsEnum.MipmapCount) ? MipmapCount : 1);

            if (IsDX10Mode)
            {
                int arraySize = (CubemapFlags.HasFlag(CubemapFlagsEnum.Cubemap) ? 6 : 1) * ArraySize;

                Image = new Resource(new Size3i(Width, Height, depth), mipmapCount, arraySize, SelectLoadFormat());

                for (int arraySlice = 0; arraySlice < Image.ArraySize; ++arraySlice)
                {
                    for (int mipSlice = 0; mipSlice < Image.MipLevels; ++mipSlice)
                    {
                        byte[] subresource = Image[mipSlice, arraySlice];
                        reader.Read(subresource, 0, subresource.Length);
                    }
                }
            }
            else if (PixelFlags.HasFlag(PixelFlagsEnum.FourCC))
            {
                int arraySize = (CubemapFlags.HasFlag(CubemapFlagsEnum.Cubemap) ? 6 : 1);

                Image = new Resource(new Size3i(Width, Height, depth), mipmapCount, arraySize, SelectLoadFormat());

                for (int arraySlice = 0; arraySlice < Image.ArraySize; ++arraySlice)
                {
                    for (int mipSlice = 0; mipSlice < Image.MipLevels; ++mipSlice)
                    {
                        byte[] subresource = Image[mipSlice, arraySlice];
                        reader.Read(subresource, 0, subresource.Length);
                    }
                }
            }
        }

        public void Save(string path)
        {
            using (var stream = File.OpenWrite(path))
            {
                Save(stream);
            }
        }
        public void Save(Stream stream)
        {
            var writer = new Ibasa.IO.BinaryWriter(stream, Encoding.ASCII);
            
            writer.Write((int)new FourCC("DDS "));
            writer.Write(124);

            SetInternalState();
            WriteHeader(writer);
            WriteDx10Header(writer);
            WriteData(writer);
        }

        private void SetInternalState()
        {
            HeaderFlags = HeaderFlagsEnum.Caps | HeaderFlagsEnum.Width | HeaderFlagsEnum.Height | HeaderFlagsEnum.PixelFormat;
            SurfaceFlags = SurfaceFlagsEnum.Texture;
            CubemapFlags = Cubemap ? (CubemapFlagsEnum.Cubemap | CubemapFlagsEnum.AllFaces) : 0;
            MiscFlags = 0;

            if (Image.MipLevels > 1)
            {
                HeaderFlags |= HeaderFlagsEnum.MipmapCount;
                SurfaceFlags |= SurfaceFlagsEnum.Complex;
            }
            if (Image.Size.Depth > 1)
            {
                HeaderFlags |= HeaderFlagsEnum.Depth;
            }

            if (Image.Format.IsCompressed)
            {
                int unused;
                Pitch = Image.Format.GetByteCount(Image.Size, out unused, out unused);
                HeaderFlags |= HeaderFlagsEnum.LinearSize;
            }
            else
            {
                int unused;
                Image.Format.GetByteCount(Image.Size, out Pitch, out unused);
                HeaderFlags |= HeaderFlagsEnum.Pitch;
            }

            Height = Image.Size.Height;
            Width = Image.Size.Width;
            Depth = Image.Size.Depth > 1 ? Image.Size.Depth : 0;
            MipmapCount = Image.MipLevels > 1 ? Image.MipLevels : 0;

            SelectSaveFormat();

            if (IsDX10Mode)
            {
                ArraySize = Cubemap ? Image.ArraySize / 6 : Image.ArraySize;
                ResourceDimension = Image.Size.Depth > 1 ? ResourceDimensionEnum.Texture3D :
                    (Image.Size.Height > 1 ? ResourceDimensionEnum.Texture2D : ResourceDimensionEnum.Texture1D);
                MiscFlags = Cubemap ? MiscFlagsEnum.TextureCube : 0;
            }
        }

        private void SelectSaveFormat()
        {
            if (Image.Format.IsCompressed || Image.ArraySize > 1)
            {
                PixelFlags = PixelFlagsEnum.FourCC;
                RGBBitCount = RBitMask = GBitMask = BBitMask = ABitMask = 0;

                if (Image.ArraySize == 1)
                {
                    //try to use DXT1/2/3

                    if (Image.Format.Name == "BC1_UNorm")
                    {
                        FourCC = new FourCC("DXT1");
                    }
                    else if (Image.Format.Name == "BC2_UNorm")
                    {
                        FourCC = new FourCC("DXT3");
                    }
                    else if (Image.Format.Name == "BC3_UNorm")
                    {
                        FourCC = new FourCC("DXT5");
                    }
                }
                else
                {
                    //use dx10
                    FourCC = new FourCC("DX10");
                    if (Enum.TryParse<DxgiFormat>(Image.Format.Name, out DxgiFormat))
                        return;
                }
            }
            else
            {
                //try plain formats
                if (Image.Format.Name == "R8G8B8A8_UNorm")
                {
                    PixelFlags = PixelFlagsEnum.Rgb | PixelFlagsEnum.AlphaPixels;
                    FourCC = new FourCC(0);
                    RGBBitCount = 32;
                    RBitMask = 0xff;
                    GBitMask = 0xff00;
                    BBitMask = 0xff0000;
                    ABitMask = unchecked((int)0xff000000);
                }
                else if (Image.Format.Name == "B5G6R5_UNorm")
                {
                    PixelFlags = PixelFlagsEnum.Rgb;
                    FourCC = new FourCC(0);
                    RGBBitCount = 15;
                    RBitMask = 0xf800;
                    GBitMask = 0x7e0;
                    BBitMask = 0x1f;
                    ABitMask = 0;
                }
                else if (Image.Format.Name == "B5G5R5A1_UNorm")
                {
                    PixelFlags = PixelFlagsEnum.Rgb | PixelFlagsEnum.AlphaPixels;
                    FourCC = new FourCC(0);
                    RGBBitCount = 15;
                    RBitMask = 0xf800;
                    GBitMask = 0x7e0;
                    BBitMask = 0x1f;
                    ABitMask = 0x8000;
                }
                else if (Image.Format.Name == "A8_UNorm")
                {
                    PixelFlags = PixelFlagsEnum.Alpha;
                    FourCC = new FourCC(0);
                    RGBBitCount = 8;
                    RBitMask = 0;
                    GBitMask = 0;
                    BBitMask = 0;
                    ABitMask = 0xff;
                }
                else
                {
                    //use dx10
                    FourCC = new FourCC("DX10");
                    if (Enum.TryParse<DxgiFormat>(Image.Format.Name, out DxgiFormat))
                        return;
                }
            }
            
            throw new NotSupportedException("Unsupported format.");
        }

        private void WriteHeader(Ibasa.IO.BinaryWriter writer)
        {
            writer.Write((int)HeaderFlags);
            writer.Write(Height);
            writer.Write(Width);
            writer.Write(Pitch);
            writer.Write(Depth);
            writer.Write(MipmapCount);
            writer.Write(new byte[44]); //write padding

            writer.Write(32);
            writer.Write((int)PixelFlags);
            writer.Write((int)FourCC);
            writer.Write(RGBBitCount);
            writer.Write(RBitMask);
            writer.Write(GBitMask);
            writer.Write(BBitMask);
            writer.Write(ABitMask);

            writer.Write((int)SurfaceFlags);
            writer.Write((int)CubemapFlags);
            writer.Write(new byte[12]); //write padding
        }

        private void WriteDx10Header(Ibasa.IO.BinaryWriter writer)
        {
            if (!IsDX10Mode)
                return;

            writer.Write((int)DxgiFormat);
            writer.Write((int)ResourceDimension);
            writer.Write((int)MiscFlags);
            writer.Write(ArraySize);
            writer.Write(new byte[4]); //write padding
        }

        private void WriteData(Ibasa.IO.BinaryWriter writer)
        {
            if (IsDX10Mode)
            {
                for (int arraySlice = 0; arraySlice < Image.ArraySize; ++arraySlice)
                {
                    for (int mipSlice = 0; mipSlice < Image.MipLevels; ++mipSlice)
                    {
                        byte[] subresource = Image[mipSlice, arraySlice];
                        writer.Write(subresource);
                    }
                }
            }
            else if (PixelFlags.HasFlag(PixelFlagsEnum.FourCC))
            {
                for (int arraySlice = 0; arraySlice < Image.ArraySize; ++arraySlice)
                {
                    for (int mipSlice = 0; mipSlice < Image.MipLevels; ++mipSlice)
                    {
                        byte[] subresource = Image[mipSlice, arraySlice];
                        writer.Write(subresource);
                    }
                }
            }
            else
            {
                for (int arraySlice = 0; arraySlice < Image.ArraySize; ++arraySlice)
                {
                    for (int mipSlice = 0; mipSlice < Image.MipLevels; ++mipSlice)
                    {
                        byte[] subresource = Image[mipSlice, arraySlice];
                        writer.Write(subresource);
                    }
                }
            }
        }
    }
}
