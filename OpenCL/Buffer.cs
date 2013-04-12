using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public struct Buffer : IEquatable<Buffer>
    {
        public static readonly Buffer Null = new Buffer();

        public IntPtr Handle { get; private set; }

        public Buffer(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Buffer(Context context, MemoryFlags flags, ulong size)
            : this()
        {
            unsafe
            {
                int error;
                Handle = CL.CreateBuffer(context.Handle, (ulong)flags, new UIntPtr(size), null, &error);
                CLHelper.GetError(error);
            }
        }

        public Buffer(Context context, MemoryFlags flags, ulong size, IntPtr hostPtr)
            : this()
        {
            unsafe
            {
                int error;
                Handle = CL.CreateBuffer(context.Handle, (ulong)flags, new UIntPtr(size), hostPtr.ToPointer(), &error);
                CLHelper.GetError(error);
            }
        }

        public MemoryFlags MemoryFlags
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetMemObjectInfo(
                        Handle, CL.MEM_FLAGS, param_value_size, &value, null));
                    return (MemoryFlags)value;
                }
            }
        }

        public MemoryObjectType MemoryObjectType
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetMemObjectInfo(
                        Handle, CL.MEM_TYPE, param_value_size, &value, null));
                    return (MemoryObjectType)value;
                }
            }
        }

        public ulong Size
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetMemObjectInfo(
                        Handle, CL.MEM_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public IntPtr HostPtr
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    CLHelper.GetError(CL.GetMemObjectInfo(
                        Handle, CL.MEM_HOST_PTR, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long MapCount
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetMemObjectInfo(
                        Handle, CL.MEM_MAP_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long ReferenceCount
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetMemObjectInfo(
                        Handle, CL.MEM_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        Context Context
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    CLHelper.GetError(CL.GetMemObjectInfo(
                        Handle, CL.MEM_CONTEXT, param_value_size, &value, null));
                    return new Context(value);
                }
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
