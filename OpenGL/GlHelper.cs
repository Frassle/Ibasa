using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    internal static class GlHelper
    {
        internal static void GetError()
        {
            uint error = Gl.GetError();
            switch (error)
            {
                case Gl.NO_ERROR: return; // throw new OpenGLException("SUCCESS");
                case Gl.INVALID_ENUM: throw new OpenGLException("INVALID_ENUM");
                case Gl.INVALID_VALUE: throw new OpenGLException("INVALID_VALUE");
                case Gl.INVALID_OPERATION: throw new OpenGLException("INVALID_OPERATION");
                case Gl.INVALID_FRAMEBUFFER_OPERATION: throw new OpenGLException("INVALID_FRAMEBUFFER_OPERATION");
                case Gl.OUT_OF_MEMORY: throw new OpenGLException("OUT_OF_MEMORY");
                case Gl.STACK_OVERFLOW: throw new OpenGLException("STACK_OVERFLOW");
                case Gl.STACK_UNDERFLOW: throw new OpenGLException("STACK_UNDERFLOW");
                default:
                    throw new OpenGLException(string.Format("Unknown error: {0}", error));
            }
        }

        internal static void ThrowNullException(uint id)
        {
            if (id == 0)
            {
                throw new NullReferenceException();
            }
        }

        internal static void ThrowNullException(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }
        }
    }
}
