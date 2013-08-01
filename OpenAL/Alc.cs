using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Ibasa.OpenAL
{
    public static unsafe class Alc
    {
        /* Boolean False. */
        public const int FALSE = 0;

        /** Boolean True. */
        public const int TRUE = 1;


        /**
        * followed by <int> Hz
        */
        public const int FREQUENCY = 0x1007;

        /**
        * followed by <int> Hz
        */
        public const int REFRESH = 0x1008;

        /**
        * followed by AL_TRUE, AL_FALSE
        */
        public const int SYNC = 0x1009;

        /**
        * followed by <int> Num of requested Mono (3D) Sources
        */
        public const int MONO_SOURCES = 0x1010;

        /**
        * followed by <int> Num of requested Stereo Sources
        */
        public const int STEREO_SOURCES = 0x1011;

        /**
        * errors
        */

        /**
        * No error
        */
        public const int NO_ERROR = FALSE;

        /**
        * No device
        */
        public const int INVALID_DEVICE = 0xA001;

        /**
        * invalid context ID
        */
        public const int INVALID_CONTEXT = 0xA002;

        /**
        * bad enum
        */
        public const int INVALID_ENUM = 0xA003;

        /**
        * bad value
        */
        public const int INVALID_VALUE = 0xA004;

        /**
        * Out of memory.
        */
        public const int OUT_OF_MEMORY = 0xA005;


        /**
        * The Specifier string for default device
        */
        public const int DEFAULT_DEVICE_SPECIFIER = 0x1004;
        public const int DEVICE_SPECIFIER = 0x1005;
        public const int EXTENSIONS = 0x1006;

        public const int MAJOR_VERSION = 0x1000;
        public const int MINOR_VERSION = 0x1001;

        public const int ATTRIBUTES_SIZE = 0x1002;
        public const int ALL_ATTRIBUTES = 0x1003;

        /**
        * ENUMERATE_ALL_EXT enums
        */
        public const int DEFAULT_ALL_DEVICES_SPECIFIER = 0x1012;
        public const int ALL_DEVICES_SPECIFIER = 0x1013;

        /**
        * Capture extension
        */
        public const int CAPTURE_DEVICE_SPECIFIER = 0x310;
        public const int CAPTURE_DEFAULT_DEVICE_SPECIFIER = 0x311;
        public const int CAPTURE_SAMPLES = 0x312;


        /*
        * Context Management
        */
        [DllImport("openal32.dll", EntryPoint = "alcCreateContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr CreateContext(IntPtr device, int* attrlist);

        [DllImport("openal32.dll", EntryPoint = "alcMakeContextCurrent", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern byte MakeContextCurrent(IntPtr context);

        [DllImport("openal32.dll", EntryPoint = "alcProcessContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void ProcessContext(IntPtr context);

        [DllImport("openal32.dll", EntryPoint = "alcSuspendContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void SuspendContext(IntPtr context);

        [DllImport("openal32.dll", EntryPoint = "alcDestroyContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void DestroyContext(IntPtr context);

        [DllImport("openal32.dll", EntryPoint = "alcGetCurrentContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr GetCurrentContext();

        [DllImport("openal32.dll", EntryPoint = "alcGetContextsDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr GetContextsDevice(IntPtr context);


        /*
        * Device Management
        */
        [DllImport("openal32.dll", EntryPoint = "alcOpenDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr OpenDevice(byte* devicename);

        [DllImport("openal32.dll", EntryPoint = "alcCloseDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern byte CloseDevice(IntPtr device);


        /*
        * Error support.
        * Obtain the most recent Context error
        */
        [DllImport("openal32.dll", EntryPoint = "alcGetError", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern int GetError(IntPtr device);


        /*
        * Extension support.
        * Query for the presence of an extension, and obtain any appropriate
        * function pointers and enum values.
        */
        [DllImport("openal32.dll", EntryPoint = "alcIsExtensionPresent", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern byte IsExtensionPresent(IntPtr device, byte* extname);

        [DllImport("openal32.dll", EntryPoint = "alcGetProcAddress", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void* GetProcAddress(IntPtr device, byte* funcname);

        [DllImport("openal32.dll", EntryPoint = "alcGetEnumValue", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern int GetEnumValue(IntPtr device, byte* enumname);


        /*
        * Query functions
        */
        [DllImport("openal32.dll", EntryPoint = "alcGetString", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern byte* GetString(IntPtr device, int param);

        [DllImport("openal32.dll", EntryPoint = "alcGetIntegerv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void GetIntegerv(IntPtr device, int param, int size, int* data);


        /*
        * Capture functions
        */
        [DllImport("openal32.dll", EntryPoint = "alcCaptureOpenDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern IntPtr CaptureOpenDevice(byte* devicename, uint frequency, int format, int buffersize);

        [DllImport("openal32.dll", EntryPoint = "alcCaptureCloseDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern byte CaptureCloseDevice(IntPtr device);

        [DllImport("openal32.dll", EntryPoint = "alcCaptureStart", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void CaptureStart(IntPtr device);

        [DllImport("openal32.dll", EntryPoint = "alcCaptureStop", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void CaptureStop(IntPtr device);
        
        [DllImport("openal32.dll", EntryPoint = "alcCaptureSamples", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern void CaptureSamples(IntPtr device, void* buffer, int samples);
    }
}
