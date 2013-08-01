using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.Platform.Windows
{
    public static unsafe class Wincon
    { 
        /// <summary>
        /// Retrieves the window handle used by the console associated with the
        /// calling process.
        /// </summary>
        /// <returns>
        /// The return value is a handle to the window used by the console 
        /// associated with the calling process or NULL if there is no such 
        /// associated console.
        /// </returns>
        [SuppressUnmanagedCodeSecurity()]
        [DllImport("Kernel32.dll", EntryPoint = "GetConsoleWindow")]
        public static extern IntPtr GetConsoleWindow();
    }

    public static unsafe class Winbase
    {
        [SuppressUnmanagedCodeSecurity()]
        [DllImport("Kernel32.dll", EntryPoint = "LoadLibrary")]
        public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("Kernel32.dll", EntryPoint = "LoadLibraryEx")]
        public static extern IntPtr LoadLibraryEx(
            [MarshalAs(UnmanagedType.LPStr)]string lpFileName,
            IntPtr hFile,
            uint dwFlags);

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("Kernel32.dll", EntryPoint = "GetModuleHandle")]
        public static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPStr)]string lpModuleName);

        [SuppressUnmanagedCodeSecurity()]
        [DllImport("Kernel32.dll", EntryPoint = "GetProcAddress")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)]string lpProcName);
    }
}
