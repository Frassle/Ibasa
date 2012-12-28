using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio
{
    [Serializable]
    public class AudioException : Exception
    {
        public AudioException() { }
        public AudioException(string message) : base(message) { }
        public AudioException(string message, Exception inner) : base(message, inner) { }
        protected AudioException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
