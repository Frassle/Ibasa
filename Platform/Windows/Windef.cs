using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.Platform.Windows
{
    public static unsafe class Windef
    {
        public static ushort MAKEWORD(ulong a, ulong b)
        {
            return (ushort)((byte)a | ((byte)b << 8));
        }

        public static uint MAKELONG(ulong a, ulong b)
        {
            return (uint)((ushort)a | ((ushort)b << 16));
        }

        public static ushort LOWORD(ulong l)
        {
            return (ushort)l;
        }

        public static ushort HIWORD(ulong l)
        {
            return (ushort)(l >> 16);
        }

        public static byte LOBYTE(ulong w)
        {
            return (byte)w;
        }

        public static byte HIBYTE(ulong w)
        {
            return (byte)(w >> 8);
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct POINT
        {
            public int x;
            public int y;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SIZE
        {
            public int cx;
            public int cy;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FILETIME
        {
            public uint dwLowDateTime;
            public uint dwHighDateTime;
        }

        public const int DM_UPDATE = 1;
        public const int DM_COPY = 2;
        public const int DM_PROMPT = 4;
        public const int DM_MODIFY = 8;

        public const int DM_IN_BUFFER = DM_MODIFY;
        public const int DM_IN_PROMPT = DM_PROMPT;
        public const int DM_OUT_BUFFER = DM_COPY ;
        public const int DM_OUT_DEFAULT = DM_UPDATE;

        public const int DC_FIELDS = 1;
        public const int DC_PAPERS = 2;
        public const int DC_PAPERSIZE = 3;
        public const int DC_MINEXTENT = 4;
        public const int DC_MAXEXTENT = 5;
        public const int DC_BINS = 6;
        public const int DC_DUPLEX = 7;
        public const int DC_SIZE = 8;
        public const int DC_EXTRA = 9;
        public const int DC_VERSION = 10;
        public const int DC_DRIVER = 11;
        public const int DC_BINNAMES = 12;
        public const int DC_ENUMRESOLUTIONS = 13;
        public const int DC_FILEDEPENDENCIES = 14;
        public const int DC_TRUETYPE = 15;
        public const int DC_PAPERNAMES = 16;
        public const int DC_ORIENTATION = 17;
        public const int DC_COPIES = 18;
    }
}
