using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.OpenAL
{
    public enum Format
    {
        Mono8 = Al.FORMAT_MONO8,
        Mono16 = Al.FORMAT_MONO16,
        Stereo8 = Al.FORMAT_STEREO8,
        Stereo16 = Al.FORMAT_STEREO16,
    }

    public enum SourceType
    {
        Static = Al.STATIC,
        Streaming = Al.STREAMING,
        Undetermined = Al.UNDETERMINED,
    }

    public enum SourceState
    {
        Initial = Al.INITIAL,
        Paused = Al.PAUSED,
        Playing = Al.PLAYING,
        Stopped = Al.STOPPED,
    }
}
