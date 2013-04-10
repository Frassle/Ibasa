using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public struct Buffer : IEquatable<Buffer>
    {
        public IntPtr Handle { get; private set; }

        public Buffer(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Buffer(Context context, MemFlags flags, ulong size)
            : this()
        {
            unsafe
            {
                int error;
                Handle = CL.CreateBuffer(context.Handle, (ulong)flags, new UIntPtr(size), null, &error);
                CLHelper.GetError(error);
            }
        }

        public override int GetHashCode()
        {
            CLHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            CLHelper.ThrowNullException(Handle);
            if (obj is Buffer)
            {
                return Equals((Buffer)obj);
            }
            return false;
        }

        public bool Equals(Buffer other)
        {
            CLHelper.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Buffer left, Buffer right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Buffer left, Buffer right)
        {
            return left.Handle != right.Handle;
        }

        public override string ToString()
        {
            CLHelper.ThrowNullException(Handle);
            return Handle.ToString();
        }
    }
}
