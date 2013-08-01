using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.Platform.Windows
{
    public static unsafe class Winuser
    {
        /// <summary>
        /// Changes the size, position, and Z order of a child, pop-up, or 
        /// top-level window. These windows are ordered according to their 
        /// appearance on the screen. The topmost window receives the highest 
        /// rank and is the first window in the Z order.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="hWndInsertAfter">
        /// A handle to the window to precede the positioned window in the Z 
        /// order. This parameter must be a window handle or one of the 
        /// following values.
        /// </param>
        /// <param name="X">
        /// The new position of the left side of the window, in client coordinates.
        /// </param>
        /// <param name="Y">
        /// The new position of the top of the window, in client coordinates.
        /// </param>
        /// <param name="cx">
        /// The new width of the window, in pixels.
        /// </param>
        /// <param name="cy">
        /// The new height of the window, in pixels.
        /// </param>
        /// <param name="uFlags">
        /// The window sizing and positioning flags. This parameter can be a 
        /// combination of the following values.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended 
        /// error information, call GetLastError.
        /// </returns>
        [SuppressUnmanagedCodeSecurity()]
        [DllImport("User32.dll", EntryPoint = "SetWindowPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X,
            int Y,
            int cx,
            int cy,
            uint uFlags);

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified
        /// window. The dimensions are given in screen coordinates that are 
        /// relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="lpRect">
        /// A pointer to a RECT structure that receives the screen coordinates 
        /// of the upper-left and lower-right corners of the window.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended 
        /// error information, call GetLastError.
        /// </returns>
        [SuppressUnmanagedCodeSecurity()]
        [DllImport("User32.dll", EntryPoint = "GetWindowRect")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(
            IntPtr hWnd,
            out Windef.RECT lpRect);

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        public struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public Windef.POINT pt;
        }

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("User32.dll", EntryPoint = "PeekMessage")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(MSG* lpMsg, IntPtr hwnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("User32.dll", EntryPoint = "GetMessage")]
        public static extern int GetMessage(MSG* lpMsg, IntPtr hwnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("User32.dll", EntryPoint = "TranslateMessage")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TranslateMessage(MSG* lpMsg);

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("User32.dll", EntryPoint = "DispatchMessage")]
        public static extern void* DispatchMessage(MSG* lpMsg);
    }
}
