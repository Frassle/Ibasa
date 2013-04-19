using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Ibasa.OpenAL
{
    public static class Al
    {
        /* bad value */
        public const int INVALID = -1;

        public const int NONE = 0;

        /* Boolean False. */
        public const int FALSE = 0;

        /** Boolean True. */
        public const int TRUE = 1;

        /** Indicate Source has relative coordinates. */
        public const int SOURCE_RELATIVE = 0x202;


        /**
        * Directional source, inner cone angle, in degrees.
        * Range: [0-360]
        * Default: 360
        */
        public const int CONE_INNER_ANGLE = 0x1001;

        /**
        * Directional source, outer cone angle, in degrees.
        * Range: [0-360]
        * Default: 360
        */
        public const int CONE_OUTER_ANGLE = 0x1002;

        /**
        * Specify the pitch to be applied, either at source,
        * or on mixer results, at listener.
        * Range: [0.5-2.0]
        * Default: 1.0
        */
        public const int PITCH = 0x1003;

        /**
        * Specify the current location in three dimensional space.
        * OpenAL, like OpenGL, uses a right handed coordinate system,
        * where in a frontal default view X (thumb) points right,
        * Y points up (index finger), and Z points towards the
        * viewer/camera (middle finger).
        * To switch from a left handed coordinate system, flip the
        * sign on the Z coordinate.
        * Listener position is always in the world coordinate system.
        */
        public const int POSITION = 0x1004;

        /** Specify the current direction. */
        public const int DIRECTION = 0x1005;

        /** Specify the current velocity in three dimensional space. */
        public const int VELOCITY = 0x1006;

        /**
        * Indicate whether source is looping.
        * Type: ALboolean?
        * Range: [TRUE, FALSE]
        * Default: FALSE.
        */
        public const int LOOPING = 0x1007;

        /**
        * Indicate the buffer to provide sound samples.
        * Type: ALuint.
        * Range: any valid Buffer id.
        */
        public const int BUFFER = 0x1009;

        /**
        * Indicate the gain (volume amplification) applied.
        * Type: ALfloat.
        * Range: ]0.0- ]
        * A value of 1.0 means un-attenuated/unchanged.
        * Each division by 2 equals an attenuation of -6dB.
        * Each multiplicaton with 2 equals an amplification of +6dB.
        * A value of 0.0 is meaningless with respect to a logarithmic
        * scale; it is interpreted as zero volume - the channel
        * is effectively disabled.
        */
        public const int GAIN = 0x100A;

        /*
        * Indicate minimum source attenuation
        * Type: ALfloat
        * Range: [0.0 - 1.0]
        *
        * Logarthmic
        */
        public const int MIN_GAIN = 0x100D;

        /**
        * Indicate maximum source attenuation
        * Type: ALfloat
        * Range: [0.0 - 1.0]
        *
        * Logarthmic
        */
        public const int MAX_GAIN = 0x100E;

        /**
        * Indicate listener orientation.
        *
        * at/up
        */
        public const int ORIENTATION = 0x100F;

        /**
        * Specify the channel mask. (Creative)
        * Type: ALuint
        * Range: [0 - 255]
        */
        public const int CHANNEL_MASK = 0x3000;


        /**
        * Source state information.
        */
        public const int SOURCE_STATE = 0x1010;
        public const int INITIAL = 0x1011;
        public const int PLAYING = 0x1012;
        public const int PAUSED = 0x1013;
        public const int STOPPED = 0x1014;

        /**
        * Buffer Queue params
        */
        public const int BUFFERS_QUEUED = 0x1015;
        public const int BUFFERS_PROCESSED = 0x1016;

        /**
        * Source buffer position information
        */
        public const int SEC_OFFSET = 0x1024;
        public const int SAMPLE_OFFSET = 0x1025;
        public const int BYTE_OFFSET = 0x1026;

        /*
        * Source type (Static, Streaming or undetermined)
        * Source is Static if a Buffer has been attached using BUFFER
        * Source is Streaming if one or more Buffers have been attached using alSourceQueueBuffers
        * Source is undetermined when it has the NULL buffer attached
        */
        public const int SOURCE_TYPE = 0x1027;
        public const int STATIC = 0x1028;
        public const int STREAMING = 0x1029;
        public const int UNDETERMINED = 0x1030;

        /** Sound samples: format specifier. */
        public const int FORMAT_MONO8 = 0x1100;
        public const int FORMAT_MONO16 = 0x1101;
        public const int FORMAT_STEREO8 = 0x1102;
        public const int FORMAT_STEREO16 = 0x1103;

        /**
        * source specific reference distance
        * Type: ALfloat
        * Range: 0.0 - +inf
        *
        * At 0.0, no distance attenuation occurs. Default is
        * 1.0.
        */
        public const int REFERENCE_DISTANCE = 0x1020;

        /**
        * source specific rolloff factor
        * Type: ALfloat
        * Range: 0.0 - +inf
        *
        */
        public const int ROLLOFF_FACTOR = 0x1021;

        /**
        * Directional source, outer cone gain.
        *
        * Default: 0.0
        * Range: [0.0 - 1.0]
        * Logarithmic
        */
        public const int CONE_OUTER_GAIN = 0x1022;

        /**
        * Indicate distance above which sources are not
        * attenuated using the inverse clamped distance model.
        *
        * Default: +inf
        * Type: ALfloat
        * Range: 0.0 - +inf
        */
        public const int MAX_DISTANCE = 0x1023;

        /**
        * Sound samples: frequency, in units of Hertz [Hz].
        * This is the number of samples per second. Half of the
        * sample frequency marks the maximum significant
        * frequency component.
        */
        public const int FREQUENCY = 0x2001;
        public const int BITS = 0x2002;
        public const int CHANNELS = 0x2003;
        public const int SIZE = 0x2004;

        /**
        * Buffer state.
        *
        * Not supported for public use (yet).
        */
        public const int UNUSED = 0x2010;
        public const int PENDING = 0x2011;
        public const int PROCESSED = 0x2012;


        /** Errors: No Error. */
        public const int NO_ERROR = FALSE;

        /**
        * Invalid Name paramater passed to AL call.
        */
        public const int INVALID_NAME = 0xA001;

        /**
        * Invalid parameter passed to AL call.
        */
        public const int ILLEGENUM = 0xA002;
        public const int INVALID_ENUM = 0xA002;

        /**
        * Invalid enum parameter value.
        */
        public const int INVALID_VALUE = 0xA003;

        /**
        * Illegal call.
        */
        public const int ILLEGCOMMAND = 0xA004;
        public const int INVALID_OPERATION = 0xA004;


        /**
        * No mojo.
        */
        public const int OUT_OF_MEMORY = 0xA005;


        /** Context strings: Vendor Name. */
        public const int VENDOR = 0xB001;
        public const int VERSION = 0xB002;
        public const int RENDERER = 0xB003;
        public const int EXTENSIONS = 0xB004;

        /** Global tweakage. */

        /**
        * Doppler scale. Default 1.0
        */
        public const int DOPPLER_FACTOR = 0xC000;

        /**
        * Tweaks speed of propagation.
        */
        public const int DOPPLER_VELOCITY = 0xC001;

        /**
        * Speed of Sound in units per second
        */
        public const int SPEED_OF_SOUND = 0xC003;

        /**
        * Distance models
        *
        * used in conjunction with DistanceModel
        *
        * implicit: NONE, which disances distance attenuation.
        */
        public const int DISTANCE_MODEL = 0xD000;
        public const int INVERSE_DISTANCE = 0xD001;
        public const int INVERSE_DISTANCE_CLAMPED = 0xD002;
        public const int LINEAR_DISTANCE = 0xD003;
        public const int LINEAR_DISTANCE_CLAMPED = 0xD004;
        public const int EXPONENT_DISTANCE = 0xD005;
        public const int EXPONENT_DISTANCE_CLAMPED = 0xD006;


        /*
         * Renderer State management
         */
        [DllImport("openal32.dll", EntryPoint = "alEnable", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Enable(int capability);

        [DllImport("openal32.dll", EntryPoint = "alDisable", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Disable(int capability); 
        
        [DllImport("openal32.dll", EntryPoint = "alDisable", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe byte IsEnabled(int capability); 

        /*
         * State retrieval
         */        
        [DllImport("openal32.dll", EntryPoint = "alGetString", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe byte* GetString(int param);

        [DllImport("openal32.dll", EntryPoint = "alGetBooleanv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetBooleanv(int param, byte* data);

        [DllImport("openal32.dll", EntryPoint = "alGetIntegerv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetIntegerv(int param, int* data);

        [DllImport("openal32.dll", EntryPoint = "alGetFloatv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetFloatv(int param, float* data);

        [DllImport("openal32.dll", EntryPoint = "alGetDoublev", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetDoublev(int param, double* data);

        [DllImport("openal32.dll", EntryPoint = "alGetBoolean", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe byte GetBoolean(int param);

        [DllImport("openal32.dll", EntryPoint = "alGetInteger", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe int GetInteger(int param);

        [DllImport("openal32.dll", EntryPoint = "alGetFloat", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe float GetFloat(int param);

        [DllImport("openal32.dll", EntryPoint = "alGetDouble", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe double GetDouble(int param);


        /*
         * Error support.
         * Obtain the most recent error generated in the AL state machine.
         */
        [DllImport("openal32.dll", EntryPoint = "alGetError", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe int GetError();


        /* 
         * Extension support.
         * Query for the presence of an extension, and obtain any appropriate
         * function pointers and enum values.
         */        
        [DllImport("openal32.dll", EntryPoint = "alIsExtensionPresent", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern unsafe bool IsExtensionPresent(byte* extname);
        
        [DllImport("openal32.dll", EntryPoint = "alGetProcAddress", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void* GetProcAddress(byte* fname);

        [DllImport("openal32.dll", EntryPoint = "alGetEnumValue", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe int GetEnumValue(byte* ename);


        /*
         * LISTENER
         * Listener represents the location and orientation of the
         * 'user' in 3D-space.
         *
         * Properties include: -
         *
         * Gain         GAIN         ALfloat
         * Position     POSITION     ALfloat[3]
         * Velocity     VELOCITY     ALfloat[3]
         * Orientation  ORIENTATION  ALfloat[6] (Forward then Up vectors)
        */

        /*
         * Set Listener parameters
         */
        [DllImport("openal32.dll", EntryPoint = "alListenerf", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Listenerf(int param, float value);

        [DllImport("openal32.dll", EntryPoint = "alListener3f", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Listener3f(int param, float value1, float value2, float value3);

        [DllImport("openal32.dll", EntryPoint = "alListenerfv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Listenerfv(int param, float* values); 

        [DllImport("openal32.dll", EntryPoint = "alListeneri", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Listeneri(int param, int value);

        [DllImport("openal32.dll", EntryPoint = "alListener3i", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Listener3i(int param, int value1, int value2, int value3);

        [DllImport("openal32.dll", EntryPoint = "alListeneriv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Listeneriv(int param, int* values);

        /*
         * Get Listener parameters
         */
        [DllImport("openal32.dll", EntryPoint = "alGetListenerf", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetListenerf(int param, float* value);

        [DllImport("openal32.dll", EntryPoint = "alGetListener3f", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetListener3f(int param, float* value1, float* value2, float* value3);

        [DllImport("openal32.dll", EntryPoint = "alGetListenerfv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetListenerfv(int param, float* values);

        [DllImport("openal32.dll", EntryPoint = "alGetListeneri", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetListeneri(int param, int* value);

        [DllImport("openal32.dll", EntryPoint = "alGetListener3i", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetListener3i(int param, int* value1, int* value2, int* value3);

        [DllImport("openal32.dll", EntryPoint = "alGetListeneriv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetListeneriv(int param, int* values);


        /**
         * SOURCE
         * Sources represent individual sound objects in 3D-space.
         * Sources take the PCM data provided in the specified Buffer,
         * apply Source-specific modifications, and then
         * submit them to be mixed according to spatial arrangement etc.
         * 
         * Properties include: -
         *
         * Gain                              GAIN                 ALfloat
         * Min Gain                          MIN_GAIN             ALfloat
         * Max Gain                          MAX_GAIN             ALfloat
         * Position                          POSITION             ALfloat[3]
         * Velocity                          VELOCITY             ALfloat[3]
         * Direction                         DIRECTION            ALfloat[3]
         * Head Relative Mode                SOURCE_RELATIVE      ALint (TRUE or FALSE)
         * Reference Distance                REFERENCE_DISTANCE   ALfloat
         * Max Distance                      MAX_DISTANCE         ALfloat
         * RollOff Factor                    ROLLOFF_FACTOR       ALfloat
         * Inner Angle                       CONE_INNER_ANGLE     ALint or ALfloat
         * Outer Angle                       CONE_OUTER_ANGLE     ALint or ALfloat
         * Cone Outer Gain                   CONE_OUTER_GAIN      ALint or ALfloat
         * Pitch                             PITCH                ALfloat
         * Looping                           LOOPING              ALint (TRUE or FALSE)
         * MS Offset                         MSEC_OFFSET          ALint or ALfloat
         * Byte Offset                       BYTE_OFFSET          ALint or ALfloat
         * Sample Offset                     SAMPLE_OFFSET        ALint or ALfloat
         * Attached Buffer                   BUFFER               ALint
         * State (Query only)                SOURCE_STATE         ALint
         * Buffers Queued (Query only)       BUFFERS_QUEUED       ALint
         * Buffers Processed (Query only)    BUFFERS_PROCESSED    ALint
         */

        /* Create Source objects */
        [DllImport("openal32.dll", EntryPoint = "alGenSources", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GenSources(int n, uint* sources); 

        /* Delete Source objects */
        [DllImport("openal32.dll", EntryPoint = "alDeleteSources", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void DeleteSources(int n, uint* sources);

        /* Verify a handle is a valid Source */ 
        [DllImport("openal32.dll", EntryPoint = "alIsSource", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe byte IsSource(uint sid); 

        /*
         * Set Source parameters
         */
        [DllImport("openal32.dll", EntryPoint = "alSourcef", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Sourcef(uint sid, int param, float value); 

        [DllImport("openal32.dll", EntryPoint = "alSource3f", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Source3f(uint sid, int param, float value1, float value2, float value3);

        [DllImport("openal32.dll", EntryPoint = "alSourcefv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Sourcefv(uint sid, int param, float* values); 

        [DllImport("openal32.dll", EntryPoint = "alSourcei", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Sourcei(uint sid, int param, int value); 

        [DllImport("openal32.dll", EntryPoint = "alSource3i", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Source3i(uint sid, int param, int value1, int value2, int value3);

        [DllImport("openal32.dll", EntryPoint = "alSourceiv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Sourceiv(uint sid, int param, int* values);

        /*
         * Get Source parameters
         */
        [DllImport("openal32.dll", EntryPoint = "alGetSourcef", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetSourcef(uint sid, int param, float* value);

        [DllImport("openal32.dll", EntryPoint = "alGetSource3f", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetSource3f(uint sid, int param, float* value1, float* value2, float* value3);

        [DllImport("openal32.dll", EntryPoint = "alGetSourcefv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetSourcefv(uint sid, int param, float* values);

        [DllImport("openal32.dll", EntryPoint = "alGetSourcei", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetSourcei(uint sid, int param, int* value);

        [DllImport("openal32.dll", EntryPoint = "alGetSource3i", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetSource3i(uint sid, int param, int* value1, int* value2, int* value3);

        [DllImport("openal32.dll", EntryPoint = "alGetSourceiv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetSourceiv(uint sid, int param, int* values);


        /*
         * Source vector based playback calls
         */

        /* Play, replay, or resume (if paused) a list of Sources */
        [DllImport("openal32.dll", EntryPoint = "alSourcePlayv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void SourcePlayv(int ns, uint* sids);

        /* Stop a list of Sources */
        [DllImport("openal32.dll", EntryPoint = "alSourceStopv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void SourceStopv(int ns, uint* sids);

        /* Rewind a list of Sources */
        [DllImport("openal32.dll", EntryPoint = "alSourceRewindv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void SourceRewindv(int ns, uint* sids);

        /* Pause a list of Sources */
        [DllImport("openal32.dll", EntryPoint = "alSourcePausev", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void SourcePausev(int ns, uint* sids);

        /*
         * Source based playback calls
         */

        /* Play, replay, or resume a Source */
        [DllImport("openal32.dll", EntryPoint = "alSourcePlay", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void SourcePlay(uint sid);

        /* Stop a Source */
        [DllImport("openal32.dll", EntryPoint = "alSourceStop", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void SourceStop(uint sid);

        /* Rewind a Source (set playback postiton to beginning) */
        [DllImport("openal32.dll", EntryPoint = "alSourceRewind", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void SourceRewind(uint sid);

        /* Pause a Source */
        [DllImport("openal32.dll", EntryPoint = "alSourcePause", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void SourcePause(uint sid);

        /*
         * Source Queuing 
         */
        [DllImport("openal32.dll", EntryPoint = "alSourceQueueBuffers", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void SourceQueueBuffers(uint sid, int numEntries, uint* bids);

        [DllImport("openal32.dll", EntryPoint = "alSourceUnqueueBuffers", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void SourceUnqueueBuffers(uint sid, int numEntries, uint* bids);


        /**
         * BUFFER
         * Buffer objects are storage space for sample data.
         * Buffers are referred to by Sources. One Buffer can be used
         * by multiple Sources.
         *
         * Properties include: -
         *
         * Frequency (Query only)    FREQUENCY      ALint
         * Size (Query only)         SIZE           ALint
         * Bits (Query only)         BITS           ALint
         * Channels (Query only)     CHANNELS       ALint
         */

        /* Create Buffer objects */
        [DllImport("openal32.dll", EntryPoint = "alGenBuffers", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GenBuffers(int n, uint* buffers);

        /* Delete Buffer objects */
        [DllImport("openal32.dll", EntryPoint = "alDeleteBuffers", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void DeleteBuffers(int n, uint* buffers);

        /* Verify a handle is a valid Buffer */
        [DllImport("openal32.dll", EntryPoint = "alIsBuffer", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe byte IsBuffer(uint bid);

        /* Specify the data to be copied into a buffer */
        [DllImport("openal32.dll", EntryPoint = "alBufferData", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void BufferData(uint bid, int format, void* data, int size, int freq);

        /*
         * Set Buffer parameters
         */
        [DllImport("openal32.dll", EntryPoint = "alBufferf", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Bufferf(uint bid, int param, float value);

        [DllImport("openal32.dll", EntryPoint = "alBuffer3f", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Buffer3f(uint bid, int param, float value1, float value2, float value3);

        [DllImport("openal32.dll", EntryPoint = "alBufferfv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Bufferfv(uint bid, int param, float* values);

        [DllImport("openal32.dll", EntryPoint = "alBufferi", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Bufferi(uint bid, int param, int value);

        [DllImport("openal32.dll", EntryPoint = "alBuffer3i", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Buffer3i(uint bid, int param, int value1, int value2, int value3);

        [DllImport("openal32.dll", EntryPoint = "alBufferiv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void Bufferiv(uint bid, int param, int* values);

        /*
        * Get Buffer parameters
        */
        [DllImport("openal32.dll", EntryPoint = "alGetBufferf", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetBufferf(uint bid, int param, float* value);

        [DllImport("openal32.dll", EntryPoint = "alGetBuffer3f", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetBuffer3f(uint bid, int param, float* value1, float* value2, float* value3);

        [DllImport("openal32.dll", EntryPoint = "alGetBufferfv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetBufferfv(uint bid, int param, float* values);

        [DllImport("openal32.dll", EntryPoint = "alGetBufferi", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetBufferi(uint bid, int param, int* value);

        [DllImport("openal32.dll", EntryPoint = "alGetBuffer3i", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetBuffer3i(uint bid, int param, int* value1, int* value2, int* value3);

        [DllImport("openal32.dll", EntryPoint = "alGetBufferiv", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void GetBufferiv(uint bid, int param, int* values);


        /*
         * Global Parameters
         */
        [DllImport("openal32.dll", EntryPoint = "alDopplerFactor", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void DopplerFactor(float value);

        [DllImport("openal32.dll", EntryPoint = "alDopplerVelocity", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void DopplerVelocity(float value);

        [DllImport("openal32.dll", EntryPoint = "alSpeedOfSound", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void SpeedOfSound(float value);

        [DllImport("openal32.dll", EntryPoint = "alDistanceModel", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static extern unsafe void DistanceModel(int distanceModel);
    }
}
