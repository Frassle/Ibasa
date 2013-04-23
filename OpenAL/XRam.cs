using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Ibasa.OpenAL
{
    public static class XRam
    {
        private static bool EAX_RAM;
        private static int AL_EAX_RAM_SIZE;
        private static int AL_EAX_RAM_FREE;
        private static int AL_STORAGE_AUTOMATIC;
        private static int AL_STORAGE_HARDWARE;
        private static int AL_STORAGE_ACCESSIBLE;
        private unsafe delegate byte EAXSetBufferModeDelegate(int n, uint* buffers, int value);
        private unsafe delegate int EAXGetBufferModeDelegate(uint buffer, int* value);
        private static EAXSetBufferModeDelegate EAXSetBufferMode;
        private static EAXGetBufferModeDelegate EAXGetBufferMode;

        public static bool Initalize()
        {
            if (Context.CurrentContext == Context.Null)
                throw new OpenALException("No OpenAL context.");

            EAX_RAM = Context.IsExtensionPresent("EAX-RAM");

            if (!EAX_RAM)
                return false;

            AL_EAX_RAM_SIZE = Context.GetEnumValue("AL_EAX_RAM_SIZE");
            AL_EAX_RAM_FREE = Context.GetEnumValue("AL_EAX_RAM_FREE");
            AL_STORAGE_AUTOMATIC = Context.GetEnumValue("AL_STORAGE_AUTOMATIC");
            AL_STORAGE_HARDWARE = Context.GetEnumValue("AL_STORAGE_HARDWARE");
            AL_STORAGE_ACCESSIBLE = Context.GetEnumValue("AL_STORAGE_ACCESSIBLE");
            var setBufferMode = Context.GetFunctionPointer("EAXSetBufferMode");
            var getBufferMode = Context.GetFunctionPointer("EAXGetBufferMode");
            EAXSetBufferMode = (EAXSetBufferModeDelegate)Marshal.GetDelegateForFunctionPointer(
                setBufferMode, typeof(EAXSetBufferModeDelegate));
            EAXGetBufferMode = (EAXGetBufferModeDelegate)Marshal.GetDelegateForFunctionPointer(
                setBufferMode, typeof(EAXGetBufferModeDelegate));

            return true;
        }

        public enum Mode
        {
            Automatic,
            Hardward,
            Accessible,
        }

        public static bool IsExtensionPresent
        {
            get
            {
                return EAX_RAM;
            }
        }

        public static long RamSize
        {
            get
            {
                return Al.GetInteger(AL_EAX_RAM_SIZE);
            }
        }

        public static long RamFree
        {
            get
            {
                return Al.GetInteger(AL_EAX_RAM_FREE);
            }
        }

        public static bool SetBufferMode(Buffer buffer, Mode mode)
        {
            unsafe
            {
                uint id = buffer.Id;
                int value = 0;
                switch (mode)
                {
                    case Mode.Automatic:
                        value = AL_STORAGE_AUTOMATIC; break;
                    case Mode.Hardward:
                        value = AL_STORAGE_HARDWARE; break;
                    case Mode.Accessible:
                        value = AL_STORAGE_ACCESSIBLE; break;
                }

                return EAXSetBufferMode(1, &id, value) != 0;
            }
        }

        public static Mode GetBufferMode(Buffer buffer)
        {
            unsafe
            {
                var value = EAXGetBufferMode(buffer.Id, null);
                if (value == AL_STORAGE_AUTOMATIC)
                    return Mode.Automatic;
                if (value == AL_STORAGE_HARDWARE)
                    return Mode.Hardward;
                if (value == AL_STORAGE_ACCESSIBLE)
                    return Mode.Accessible;
                
                throw new OpenALException(string.Format("Unknown return value: {0}", value));
            }
        }
    }
}
