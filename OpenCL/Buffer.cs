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
                Handle = Cl.CreateBuffer(context.Handle, (ulong)flags, new UIntPtr(size), null, &error);
                ClHelper.GetError(error);
            }
        }

        public Buffer(Context context, MemoryFlags flags, long size)
            : this(context, flags, (ulong)size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException("size", size, "size is less than 0.");
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
                if (flags.HasFlag(MemoryFlags.UseHostPtr) & flags.HasFlag(MemoryFlags.CopyHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr and MemoryFlags.CopyHostPtr are mutually exclusive.");
                if (flags.HasFlag(MemoryFlags.UseHostPtr) & flags.HasFlag(MemoryFlags.AllocHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr and MemoryFlags.AllocHostPtr are mutually exclusive.");
            }

            if (size == 0)
                throw new ArgumentOutOfRangeException("size", size, "size is 0.");

            unsafe
            {
                int error;
                Handle = Cl.CreateBuffer(context.Handle, (ulong)flags, new UIntPtr(size), hostPtr.ToPointer(), &error);
                ClHelper.GetError(error);
            }
        }

        public Buffer(Context context, MemoryFlags flags, long size,  IntPtr hostPtr)
            : this(context, flags, (ulong)size, hostPtr)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException("size", size, "size is less than 0.");
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
            if (count == 0)
                throw new ArgumentOutOfRangeException("count", count, "count is 0.");
            if (index + count > data.Length)
                throw new ArgumentOutOfRangeException("count", count, "index + count is greater than data.Length.");
            
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var size = Marshal.SizeOf(typeof(T));
            var ptr = handle.AddrOfPinnedObject();

            flags |= MemoryFlags.CopyHostPtr;

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

        private delegate void CallbackDelegete(IntPtr buffer, IntPtr user_data);
        private static void Callback(IntPtr buffer, IntPtr user_data)
        {
            var handel = GCHandle.FromIntPtr(user_data);
            var data = (Tuple<Action<Buffer, object>, object>)handel.Target;
            handel.Free();

            data.Item1(new Buffer(buffer), data.Item2);
        }

        public void SetDestructorCallback(Action<Buffer, object> notify, object user_data)
        {
            ClHelper.ThrowNullException(Handle);

            if (notify == null)
                throw new ArgumentNullException("notify");

            unsafe
            {
                var function_ptr = IntPtr.Zero;
                var data = Tuple.Create(notify, user_data);
                var data_handle = GCHandle.Alloc(data);

                function_ptr = Marshal.GetFunctionPointerForDelegate(new CallbackDelegete(Callback));

                try
                {
                    ClHelper.GetError(Cl.SetMemObjectDestructorCallback(
                        Handle, function_ptr, GCHandle.ToIntPtr(data_handle).ToPointer()));
                }
                catch (EntryPointNotFoundException)
                {
                    data_handle.Free();
                    throw ClHelper.VersionException(1, 1);
                }
                catch (Exception)
                {
                    data_handle.Free();
                    throw;
                }
            }
        }

        public void RetainBuffer()
        {
            ClHelper.ThrowNullException(Handle);
            ClHelper.GetError(Cl.RetainMemObject(Handle));
        }

        public void ReleaseBuffer()
        {
            ClHelper.ThrowNullException(Handle);
            int error = Cl.ReleaseMemObject(Handle);
            Handle = IntPtr.Zero;
            ClHelper.GetError(error);
        }

        public MemoryFlags MemoryFlags
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_FLAGS, param_value_size, &value, null));
                    return (MemoryFlags)value;
                }
            }
        }

        public MemoryObjectType MemoryObjectType
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_TYPE, param_value_size, &value, null));
                    return (MemoryObjectType)value;
                }
            }
        }

        public ulong Size
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public IntPtr HostPtr
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_HOST_PTR, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long MapCount
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_MAP_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long ReferenceCount
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        Context Context
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_CONTEXT, param_value_size, &value, null));
                    return new Context(value);
                }
            }
        }

        Buffer Parent
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_ASSOCIATED_MEMOBJECT, param_value_size, &value, null));
                    return new Buffer(value);
                }
            }
        }

        ulong Offset
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_OFFSET, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public override int GetHashCode()
        {
            ClHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ClHelper.ThrowNullException(Handle);
            if (obj is Buffer)
            {
                return Equals((Buffer)obj);
            }
            return false;
        }

        public bool Equals(Buffer other)
        {
            ClHelper.ThrowNullException(Handle);
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
            ClHelper.ThrowNullException(Handle);
            return Handle.ToString();
        }
    }
}
