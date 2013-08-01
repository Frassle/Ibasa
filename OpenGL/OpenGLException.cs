using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    [Serializable]
    public class OpenGLException : Exception
    {
        public OpenGLException() { }
        public OpenGLException(string message) : base(message) { }
        public OpenGLException(string message, Exception inner) : base(message, inner) { }
        protected OpenGLException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
