using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    [Serializable]
    public class OpenCLException : Exception
    {
        public OpenCLException() { }
        public OpenCLException(string message) : base(message) { }
        public OpenCLException(string message, Exception inner) : base(message, inner) { }
        protected OpenCLException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
