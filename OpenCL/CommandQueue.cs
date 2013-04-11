using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public struct CommandQueue : IEquatable<CommandQueue>
    {
        public IntPtr Handle { get; private set; }

        public CommandQueue(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public CommandQueue(Context context, Device device, CommandQueueProperties properties) : this()
        {
            unsafe
            {
                int error = 0;
                Handle = CL.CreateCommandQueue(context.Handle, device.Handle, (ulong)properties, &error);
                CLHelper.GetError(error);
            }
        }

        public void Retain()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.RetainCommandQueue(Handle));
        }

        public void Release()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.ReleaseCommandQueue(Handle));
        }

        public void Flush()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.Flush(Handle));
        }

        public void Finish()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.Finish(Handle));
        }

        public Event Enqueue(Kernel kernel, ulong[] global_work_offset, ulong[] global_work_size, ulong[] local_work_size, Event[] events)
        {
            CLHelper.ThrowNullException(Handle);

            if(kernel == default(Kernel))
                throw new ArgumentNullException("kernel");
            if(global_work_size == null)
                throw new ArgumentNullException("global_work_size");
            if(global_work_offset != null && global_work_size.Length != global_work_offset.Length)
                throw new ArgumentException("global_work_size and global_work_offset must be the same length.");
            if(local_work_size != null && global_work_size.Length != local_work_size.Length)
                throw new ArgumentException("global_work_size and local_work_size must be the same length.");

            int work_dim = global_work_size.Length;

            unsafe
            {
                UIntPtr* global_offset = stackalloc UIntPtr[work_dim];
                UIntPtr* global_size = stackalloc UIntPtr[work_dim];
                UIntPtr* local_size = stackalloc UIntPtr[work_dim];

                for (int i = 0; i < work_dim; ++i)
                {
                    if(global_work_offset != null)
                        global_offset[i] = new UIntPtr(global_work_offset[i]);

                    global_size[i] = new UIntPtr(global_work_size[i]);

                    if (local_work_size != null)
                        local_size[i] = new UIntPtr(local_work_size[i]);
                }

                if (global_work_offset == null)
                    global_offset = null;
                if (local_work_size == null)
                    local_size = null;

                int num_events_in_wait_list = events == null ? 0 : events.Length;                
                IntPtr* wait_list = stackalloc IntPtr[num_events_in_wait_list];
                for (int i = 0; i < num_events_in_wait_list; ++i)
                {
                    wait_list[i] = events[i].Handle;
                }
                if (events == null)
                    wait_list = null;

                IntPtr event_ptr = IntPtr.Zero;

                CLHelper.GetError(CL.EnqueueNDRangeKernel(Handle, kernel.Handle,
                    (uint)work_dim, global_offset, global_size, local_size, 
                    (uint)num_events_in_wait_list, wait_list, &event_ptr));

                return new Event(event_ptr);
            }
        }

        public Context Context
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    CLHelper.GetError(CL.GetCommandQueueInfo(
                        Handle, CL.QUEUE_CONTEXT, param_value_size, &value, null));
                    return new Context(value);
                }
            }
        }

        public Device Device
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    CLHelper.GetError(CL.GetCommandQueueInfo(
                        Handle, CL.QUEUE_DEVICE, param_value_size, &value, null));
                    return new Device(value);
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
                    CLHelper.GetError(CL.GetContextInfo(
                        Handle, CL.CONTEXT_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public CommandQueueProperties Properties
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetContextInfo(
                        Handle, CL.CONTEXT_REFERENCE_COUNT, param_value_size, &value, null));
                    return (CommandQueueProperties)value;
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
            if (obj is CommandQueue)
            {
                return Equals((CommandQueue)obj);
            }
            return false;
        }

        public bool Equals(CommandQueue other)
        {
            CLHelper.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(CommandQueue left, CommandQueue right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(CommandQueue left, CommandQueue right)
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
