using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Valve.Material
{
    [Flags]
    public enum TextureFlags
    {
	    PointSample = 0x00000001,
	    Trilinear = 0x00000002,
	    ClampS = 0x00000004,
	    ClampT = 0x00000008,
	    Ansiotropic = 0x00000010,
	    HintDXT5 = 0x00000020,
	    NoCompress = 0x00000040,
	    Normal = 0x00000080,
	    NoMip = 0x00000100,
	    NoLod = 0x00000200,
	    MinMip = 0x00000400,
	    Procedural = 0x00000800,
	    OneBitAlpha = 0x00001000,
	    EightBitAlpha = 0x00002000,
	    EnvMap = 0x00004000,
	    RenderTarget = 0x00008000,
	    DepthRenderTarget = 0x00010000,
	    NoDebugOverride = 0x00020000,
	    SingleCopy = 0x00040000,
	    OneOverMipLevelInAlpha = 0x00080000,
	    PreMultColorByOneOverMipLevel = 0x00100000,
	    NormalToDuDv = 0x00200000,
	    AlphaTestMipGeneration = 0x00400000,
	    NoDepthBuffer = 0x00800000,
	    NiceFiltered = 0x01000000,
	    ClampU = 0x02000000
    }
}
