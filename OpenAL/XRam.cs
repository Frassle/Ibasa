using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Ibasa.OpenAL
{
    public sealed class XRam
    {
        // [CLSCompliant(false)]
        private delegate bool SetBufferMode(int n, uint* buffers, int value);
        //typedef ALboolean (__cdecl *EAXSetBufferMode)(ALsizei n, ALuint *buffers, ALint value);

        // [CLSCompliant( false )]
        private delegate int GetBufferMode(uint buffer, int* value);
        //typedef ALenum    (__cdecl *EAXGetBufferMode)(ALuint buffer, ALint *value);

        //[CLSCompliant(false)]
        private SetBufferMode SetBufferMode;
        //[CLSCompliant(false)]
        private GetBufferMode GetBufferMode;

        private readonly int AL_EAX_RAM_SIZE, AL_EAX_RAM_FREE,
                    AL_STORAGE_AUTOMATIC, AL_STORAGE_HARDWARE, AL_STORAGE_ACCESSIBLE;
        
        public XRam()
        {
            if (Context.IsExtensionPresent("EAX-RAM"))
            {
                throw new OpenALException("EAX-RAM not supported.");
            }

            var AL_EAX_RAM_SIZE = Context.GetEnumValue("AL_EAX_RAM_SIZE");
            var AL_EAX_RAM_FREE = Context.GetEnumValue("AL_EAX_RAM_FREE");
            var AL_STORAGE_AUTOMATIC = Context.GetEnumValue("AL_STORAGE_AUTOMATIC");
            var AL_STORAGE_HARDWARE = Context.GetEnumValue("AL_STORAGE_HARDWARE");
            var AL_STORAGE_ACCESSIBLE = Context.GetEnumValue("AL_STORAGE_ACCESSIBLE");

            if (AL_EAX_RAM_SIZE == 0 ||
                 AL_EAX_RAM_FREE == 0 ||
                 AL_STORAGE_AUTOMATIC == 0 ||
                 AL_STORAGE_HARDWARE == 0 ||
                 AL_STORAGE_ACCESSIBLE == 0)
            {
                throw new OpenALException("X-Ram: Token values could not be retrieved.");
            }

            try
            {
                GetBufferMode = (GetBufferMode)Marshal.GetDelegateForFunctionPointer(Al.GetProcAddress("EAXGetBufferMode"), typeof(Delegate_GetBufferMode));
                SetBufferMode = (SetBufferMode)Marshal.GetDelegateForFunctionPointer(Al.GetProcAddress("EAXSetBufferMode"), typeof(Delegate_SetBufferMode));
            }
            catch (Exception e)
            {
                throw new OpenALException("X-Ram: Attempt to marshal function pointers with AL.GetProcAddress failed. " + e.ToString());
            }
        }
    }
}
