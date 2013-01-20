using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.OpenAL
{
    [Serializable]
    public class OpenALException : Exception
    {
        public OpenALException() { }
        public OpenALException(string message) : base(message) { }
        public OpenALException(string message, Exception inner) : base(message, inner) { }
        protected OpenALException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
