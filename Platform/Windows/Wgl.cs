using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.Platform.Windows
{
    public static unsafe class Wgl
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PixelFormatDescriptor
        {
            public ushort nSize;
            public ushort nVersion;
            public uint dwFlags;
            public byte iPixelType;
            public byte cColorBits;
            public byte cRedBits;
            public byte cRedShift;
            public byte cGreenBits;
            public byte cGreenShift;
            public byte cBlueBits;
            public byte cBlueShift;
            public byte cAlphaBits;
            public byte cAlphaShift;
            public byte cAccumBits;
            public byte cAccumRedBits;
            public byte cAccumGreenBits;
            public byte cAccumBlueBits;
            public byte cAccumAlphaBits;
            public byte cDepthBits;
            public byte cStencilBits;
            public byte cAuxBuffers;
            public byte iLayerType;
            public byte bReserved;
            public uint dwLayerMask;
            public uint dwVisibleMask;
            public uint dwDamageMask;
        }

        /* pixel types */
        public const int PFD_TYPE_RGBA = 0;
        public const int PFD_TYPE_COLORINDEX = 1;

        /* layer types */
        public const int PFD_MAIN_PLANE = 0;
        public const int PFD_OVERLAY_PLANE = 1;
        public const int PFD_UNDERLAY_PLANE = (-1);

        /* PIXELFORMATDESCRIPTOR flags */
        public const int PFD_DOUBLEBUFFER = 0x00000001;
        public const int PFD_STEREO = 0x00000002;
        public const int PFD_DRAW_TO_WINDOW = 0x00000004;
        public const int PFD_DRAW_TO_BITMAP = 0x00000008;
        public const int PFD_SUPPORT_GDI = 0x00000010;
        public const int PFD_SUPPORT_OPENGL = 0x00000020;
        public const int PFD_GENERIC_FORMAT = 0x00000040;
        public const int PFD_NEED_PALETTE = 0x00000080;
        public const int PFD_NEED_SYSTEM_PALETTE = 0x00000100;
        public const int PFD_SWAP_EXCHANGE = 0x00000200;
        public const int PFD_SWAP_COPY = 0x00000400;
        public const int PFD_SWAP_LAYER_BUFFERS = 0x00000800;
        public const int PFD_GENERIC_ACCELERATED = 0x00001000;
        public const int PFD_SUPPORT_DIRECTDRAW = 0x00002000;

        /* PIXELFORMATDESCRIPTOR flags for use in ChoosePixelFormat only */
        public const int PFD_DEPTH_DONTCARE = 0x20000000;
        public const int PFD_DOUBLEBUFFER_DONTCARE = 0x40000000;
        public const int PFD_STEREO_DONTCARE = unchecked((int)0x80000000);

        [DllImport("Gdi32.dll", EntryPoint = "ChoosePixelFormat", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern int ChoosePixelFormat(IntPtr hdc, PixelFormatDescriptor* ppfd);

        [DllImport("Gdi32.dll", EntryPoint = "SetPixelFormat", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetPixelFormat(IntPtr hdc, int iPixelFormat, PixelFormatDescriptor* ppfd);

        [DllImport("Gdi32.dll", EntryPoint = "SwapBuffers", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SwapBuffers(IntPtr hdc);

        [DllImport("Opengl32.dll", EntryPoint = "wglCreateContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr CreateContext(IntPtr hdc);

        [DllImport("Opengl32.dll", EntryPoint = "wglDeleteContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteContext(IntPtr hglrc);

        [DllImport("Opengl32.dll", EntryPoint = "wglGetCurrentContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr GetCurrentContext();

        [DllImport("Opengl32.dll", EntryPoint = "wglGetCurrentDC", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr GetCurrentDC();

        [DllImport("Opengl32.dll", EntryPoint = "wglGetProcAddress", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr GetProcAddress([MarshalAs(UnmanagedType.LPStr)]string lpszProc);

        [DllImport("Opengl32.dll", EntryPoint = "wglMakeCurrent", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MakeCurrent(IntPtr hdc, IntPtr hglrc); 
    }
}
