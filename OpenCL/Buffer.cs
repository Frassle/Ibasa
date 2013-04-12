using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            if (context == Context.Null)
                throw new ArgumentNullException("context");

            if (flags.HasFlag(MemoryFlags.WriteOnly) & flags.HasFlag(MemoryFlags.ReadOnly))
                throw new ArgumentException("MemoryFlags.WriteOnly and MemoryFlags.ReadOnly are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostWriteOnly) & flags.HasFlag(MemoryFlags.HostReadOnly))
                throw new ArgumentException("MemoryFlags.HostWriteOnly and MemoryFlags.HostReadOnly are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostWriteOnly) & flags.HasFlag(MemoryFlags.HostNoAccess))
                throw new ArgumentException("MemoryFlags.HostWriteOnly and MemoryFlags.HostNoAccess are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostReadOnly) & flags.HasFlag(MemoryFlags.HostNoAccess))
                throw new ArgumentException("MemoryFlags.HostReadOnly and MemoryFlags.HostNoAccess are mutually exclusive.");

            if (flags.HasFlag(MemoryFlags.UseHostPtr))
                throw new ArgumentException("MemoryFlags.UseHostPtr is not valid.");
            if (flags.HasFlag(MemoryFlags.CopyHostPtr))
                throw new ArgumentException("MemoryFlags.CopyHostPtr is not valid.");

            if (size == 0)
                throw new ArgumentOutOfRangeException("size", size, "size is 0.");

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
            if (context == Context.Null)
                throw new ArgumentNullException("context");

            if (flags.HasFlag(MemoryFlags.WriteOnly) & flags.HasFlag(MemoryFlags.ReadOnly))
                throw new ArgumentException("MemoryFlags.WriteOnly and MemoryFlags.ReadOnly are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostWriteOnly) & flags.HasFlag(MemoryFlags.HostReadOnly))
                throw new ArgumentException("MemoryFlags.HostWriteOnly and MemoryFlags.HostReadOnly are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostWriteOnly) & flags.HasFlag(MemoryFlags.HostNoAccess))
                throw new ArgumentException("MemoryFlags.HostWriteOnly and MemoryFlags.HostNoAccess are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostReadOnly) & flags.HasFlag(MemoryFlags.HostNoAccess))
                throw new ArgumentException("MemoryFlags.HostReadOnly and MemoryFlags.HostNoAccess are mutually exclusive.");

            if (hostPtr == IntPtr.Zero)
            {
                if (flags.HasFlag(MemoryFlags.UseHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr is not valid.");
                if (flags.HasFlag(MemoryFlags.CopyHostPtr))
                    throw new ArgumentException("MemoryFlags.CopyHostPtr is not valid.");
            }
            else
            {
                if (!flags.HasFlag(MemoryFlags.UseHostPtr) & !flags.HasFlag(MemoryFlags.CopyHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr or MemoryFlags.CopyHostPtr is required.");
            }

            if (size == 0)
                throw new ArgumentOutOfRangeException("size", size, "size is 0.");

            unsafe
            {
                int error;
                Handle = CL.CreateBuffer(context.Handle, (ulong)flags, new UIntPtr(size), hostPtr.ToPointer(), &error);
                CLHelper.GetError(error);
            }
        }

        public static Buffer Create<T>(Context context, MemoryFlags flags, T[] data) where T : struct
        {
            return Create(context, flags, data, 0, data.Length);
        }

        public static Buffer Create<T>(Context context, MemoryFlags flags, T[] data, int index, int count) where T : struct
        {
            if (context == Context.Null)
                throw new ArgumentNullException("context");

            if (flags.HasFlag(MemoryFlags.WriteOnly) & flags.HasFlag(MemoryFlags.ReadOnly))
                throw new ArgumentException("MemoryFlags.WriteOnly and MemoryFlags.ReadOnly are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostWriteOnly) & flags.HasFlag(MemoryFlags.HostReadOnly))
                throw new ArgumentException("MemoryFlags.HostWriteOnly and MemoryFlags.HostReadOnly are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostWriteOnly) & flags.HasFlag(MemoryFlags.HostNoAccess))
                throw new ArgumentException("MemoryFlags.HostWriteOnly and MemoryFlags.HostNoAccess are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostReadOnly) & flags.HasFlag(MemoryFlags.HostNoAccess))
                throw new ArgumentException("MemoryFlags.HostReadOnly and MemoryFlags.HostNoAccess are mutually exclusive.");

            if (flags.HasFlag(MemoryFlags.UseHostPtr))
                throw new ArgumentException("MemoryFlags.UseHostPtr is not valid.");
            if (flags.HasFlag(MemoryFlags.CopyHostPtr))
                throw new ArgumentException("MemoryFlags.CopyHostPtr is not valid.");

            if (data == null)
                throw new ArgumentNullException("data");

            if (index < 0)
                throw new ArgumentOutOfRangeException("index", index, "index is less than 0.");
            if (index >= data.Length)
                throw new ArgumentOutOfRangeException("index", index, "index is greater than or equal to data.Length.");

            if (count == 0)
                throw new ArgumentOutOfRangeException("count", count, "count is 0.");
            if (index + count >= data.Length)
                throw new ArgumentOutOfRangeException("count", count, "index + count is greater than or equal to data.Length.");
            
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var size = Marshal.SizeOf(typeof(T));
            var ptr = handle.AddrOfPinnedObject();

            Buffer buffer = Buffer.Null;
            try
            {
                buffer = new Buffer(context, flags, (ulong)(size * count), IntPtr.Add(ptr, size * index));
            }
            finally
            {
                handle.Free();
            }

            return buffer;
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
