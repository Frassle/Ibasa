﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public struct CommandQueue : IEquatable<CommandQueue>
    {
        public static readonly CommandQueue Null = new CommandQueue();

        public IntPtr Handle { get; private set; }

        public CommandQueue(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public CommandQueue(Context context, Device device, CommandQueueProperties properties) : this()
        {
            if (context == Context.Null)
                throw new ArgumentNullException("context");

            unsafe
            {
                int error = 0;
                Handle = Cl.CreateCommandQueue(context.Handle, device.Handle, (ulong)properties, &error);
                ClHelper.GetError(error);
            }
        }

        public void Retain()
        {
            ClHelper.ThrowNullException(Handle);
            ClHelper.GetError(Cl.RetainCommandQueue(Handle));
        }

        public void Release()
        {
            ClHelper.ThrowNullException(Handle);
            var error = Cl.ReleaseCommandQueue(Handle);
            Handle = IntPtr.Zero;
            ClHelper.GetError(error);
        }

        public void Flush()
        {
            ClHelper.ThrowNullException(Handle);
            ClHelper.GetError(Cl.Flush(Handle));
        }

        public void Finish()
        {
            ClHelper.ThrowNullException(Handle);
            ClHelper.GetError(Cl.Finish(Handle));
        }

        public Event EnqueueKernel(Kernel kernel, ulong[] global_work_offset, ulong[] global_work_size, ulong[] local_work_size, Event[] events)
        {
            ClHelper.ThrowNullException(Handle);

            if(kernel == Kernel.Null)
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

                ClHelper.GetError(Cl.EnqueueNDRangeKernel(Handle, kernel.Handle,
                    (uint)work_dim, global_offset, global_size, local_size, 
                    (uint)num_events_in_wait_list, wait_list, &event_ptr));

                return new Event(event_ptr);
            }
        }

        public Event EnqueueTask(Kernel kernel, Event[] events)
        {
            ClHelper.ThrowNullException(Handle);

            if (kernel == Kernel.Null)
                throw new ArgumentNullException("kernel");

            unsafe
            {
                int num_events_in_wait_list = events == null ? 0 : events.Length;
                IntPtr* wait_list = stackalloc IntPtr[num_events_in_wait_list];
                for (int i = 0; i < num_events_in_wait_list; ++i)
                {
                    wait_list[i] = events[i].Handle;
                }
                if (events == null)
                    wait_list = null;

                IntPtr event_ptr = IntPtr.Zero;

                ClHelper.GetError(Cl.EnqueueTask(Handle, kernel.Handle,
                    (uint)num_events_in_wait_list, wait_list, &event_ptr));

                return new Event(event_ptr);
            }
        }

        public Event EnqueueReadBuffer(Buffer buffer, bool blocking, ulong offset, ulong count, IntPtr destination, Event[] events)
        {
            ClHelper.ThrowNullException(Handle);

            if (buffer == Buffer.Null)
                throw new ArgumentNullException("buffer");
            if (destination == IntPtr.Zero)
                throw new ArgumentNullException("destination");

            unsafe
            {
                int num_events_in_wait_list = events == null ? 0 : events.Length;
                IntPtr* wait_list = stackalloc IntPtr[num_events_in_wait_list];
                for (int i = 0; i < num_events_in_wait_list; ++i)
                {
                    wait_list[i] = events[i].Handle;
                }
                if (events == null)
                    wait_list = null;

                IntPtr event_ptr = IntPtr.Zero;

                ClHelper.GetError(Cl.EnqueueReadBuffer(Handle, buffer.Handle,
                    blocking ? 1u : 0u, new UIntPtr(offset), new UIntPtr(count), destination.ToPointer(), 
                    (uint)num_events_in_wait_list, wait_list, &event_ptr));

                return new Event(event_ptr);
            }
        }

        public Event EnqueueWriteBuffer(Buffer buffer, bool blocking, ulong offset, ulong count, IntPtr destination, Event[] events)
        {
            ClHelper.ThrowNullException(Handle);

            if (buffer == Buffer.Null)
                throw new ArgumentNullException("buffer");
            if (destination == IntPtr.Zero)
                throw new ArgumentNullException("destination");

            unsafe
            {
                int num_events_in_wait_list = events == null ? 0 : events.Length;
                IntPtr* wait_list = stackalloc IntPtr[num_events_in_wait_list];
                for (int i = 0; i < num_events_in_wait_list; ++i)
                {
                    wait_list[i] = events[i].Handle;
                }
                if (events == null)
                    wait_list = null;

                IntPtr event_ptr = IntPtr.Zero;

                ClHelper.GetError(Cl.EnqueueWriteBuffer(Handle, buffer.Handle,
                    blocking ? 1u : 0u, new UIntPtr(offset), new UIntPtr(count), destination.ToPointer(),
                    (uint)num_events_in_wait_list, wait_list, &event_ptr));

                return new Event(event_ptr);
            }
        }

        public Event EnqueueCopyBuffer(
            Buffer source, ulong sourceOffset,
            Buffer destination, ulong destinationOffset, ulong count, Event[] events)
        {
            ClHelper.ThrowNullException(Handle);

            if (source == Buffer.Null)
                throw new ArgumentNullException("source");
            if (destination == Buffer.Null)
                throw new ArgumentNullException("destination");

            unsafe
            {
                int num_events_in_wait_list = events == null ? 0 : events.Length;
                IntPtr* wait_list = stackalloc IntPtr[num_events_in_wait_list];
                for (int i = 0; i < num_events_in_wait_list; ++i)
                {
                    wait_list[i] = events[i].Handle;
                }
                if (events == null)
                    wait_list = null;

                IntPtr event_ptr = IntPtr.Zero;

                ClHelper.GetError(Cl.EnqueueCopyBuffer(Handle,
                    source.Handle, destination.Handle,
                    new UIntPtr(sourceOffset), new UIntPtr(sourceOffset), new UIntPtr(count),
                    (uint)num_events_in_wait_list, wait_list, &event_ptr));

                return new Event(event_ptr);
            }
        }

        public Event EnqueueMapBuffer(
            Buffer buffer, bool blocking, MapFlags flags,
            ulong offset, ulong size, Event[] events, out IntPtr pointer)
        {
            ClHelper.ThrowNullException(Handle);

            if (buffer == Buffer.Null)
                throw new ArgumentNullException("buffer");

            unsafe
            {
                int num_events_in_wait_list = events == null ? 0 : events.Length;
                IntPtr* wait_list = stackalloc IntPtr[num_events_in_wait_list];
                for (int i = 0; i < num_events_in_wait_list; ++i)
                {
                    wait_list[i] = events[i].Handle;
                }
                if (events == null)
                    wait_list = null;

                IntPtr event_ptr = IntPtr.Zero;

                int error;
                void* result = Cl.EnqueueMapBuffer(Handle,
                    buffer.Handle, blocking ? 1u : 0u, (ulong)flags,
                    new UIntPtr(offset), new UIntPtr(size),
                    (uint)num_events_in_wait_list, wait_list, &event_ptr, &error);
                ClHelper.GetError(error);

                pointer = new IntPtr(result);

                return new Event(event_ptr);
            }
        }

        public Event EnqueueUnmapBuffer(
            Buffer buffer, IntPtr pointer, Event[] events)
        {
            ClHelper.ThrowNullException(Handle);

            if (buffer == Buffer.Null)
                throw new ArgumentNullException("buffer");

            unsafe
            {
                int num_events_in_wait_list = events == null ? 0 : events.Length;
                IntPtr* wait_list = stackalloc IntPtr[num_events_in_wait_list];
                for (int i = 0; i < num_events_in_wait_list; ++i)
                {
                    wait_list[i] = events[i].Handle;
                }
                if (events == null)
                    wait_list = null;

                IntPtr event_ptr = IntPtr.Zero;

                ClHelper.GetError(Cl.EnqueueUnmapMemObject(Handle,
                    buffer.Handle, pointer.ToPointer(), 
                    (uint)num_events_in_wait_list, wait_list, &event_ptr));

                return new Event(event_ptr);
            }
        }

        public Event EnqueueMigrateBuffer(
            Buffer[] buffers, MemoryMigrationFlags flags, Event[] events)
        {
            ClHelper.ThrowNullException(Handle);

            if (buffers == null)
                throw new ArgumentNullException("buffers");

            unsafe
            {
                var buffer_list = stackalloc IntPtr[buffers.Length];

                for (int i = 0; i < buffers.Length; ++i)
                {
                    buffer_list[i] = buffers[i].Handle;
                }

                int num_events_in_wait_list = events == null ? 0 : events.Length;
                IntPtr* wait_list = stackalloc IntPtr[num_events_in_wait_list];
                for (int i = 0; i < num_events_in_wait_list; ++i)
                {
                    wait_list[i] = events[i].Handle;
                }
                if (events == null)
                    wait_list = null;

                IntPtr event_ptr = IntPtr.Zero;

                ClHelper.GetError(Cl.EnqueueMigrateMemObjects(Handle,
                    (uint)buffers.Length, buffer_list, (ulong)flags,
                    (uint)num_events_in_wait_list, wait_list, &event_ptr));

                return new Event(event_ptr);
            }
        }

        public Event EnqueueMarker()
        {
            ClHelper.ThrowNullException(Handle);

            unsafe
            {
                IntPtr event_ptr = IntPtr.Zero;

                ClHelper.GetError(Cl.EnqueueMarker(Handle, &event_ptr));

                return new Event(event_ptr);
            }
        }

        public Event EnqueueMarker(Event[] events)
        {
            ClHelper.ThrowNullException(Handle);

            try
            {
                unsafe
                {
                    int num_events_in_wait_list = events == null ? 0 : events.Length;
                    IntPtr* wait_list = stackalloc IntPtr[num_events_in_wait_list];
                    for (int i = 0; i < num_events_in_wait_list; ++i)
                    {
                        wait_list[i] = events[i].Handle;
                    }
                    if (events == null)
                        wait_list = null;

                    IntPtr event_ptr = IntPtr.Zero;

                    ClHelper.GetError(Cl.EnqueueMarkerWithWaitList(
                        Handle, (uint)num_events_in_wait_list, wait_list, &event_ptr));

                    return new Event(event_ptr);
                }
            }
            catch (EntryPointNotFoundException)
            {
                throw ClHelper.VersionException(1, 2);
            }
        }

        public void EnqueueWaitForEvents(Event[] events)
        {
            ClHelper.ThrowNullException(Handle);
            if (events == null)
                throw new ArgumentNullException("events");
            if (events.Length == 0)
                throw new ArgumentException("events is empty.");

            unsafe
            {
                int num_events = events == null ? 0 : events.Length;
                IntPtr* wait_list = stackalloc IntPtr[num_events];
                for (int i = 0; i < num_events; ++i)
                {
                    wait_list[i] = events[i].Handle;
                }

                ClHelper.GetError(Cl.EnqueueWaitForEvents(Handle, (uint)num_events, wait_list));
            }
        }

        public void EnqueueBarrier()
        {
            ClHelper.ThrowNullException(Handle);

            unsafe
            {
                ClHelper.GetError(Cl.EnqueueBarrier(Handle));
            }
        }

        public Event EnqueueBarrier(Event[] events)
        {
            ClHelper.ThrowNullException(Handle);

            try
            {
                unsafe
                {
                    int num_events_in_wait_list = events == null ? 0 : events.Length;
                    IntPtr* wait_list = stackalloc IntPtr[num_events_in_wait_list];
                    for (int i = 0; i < num_events_in_wait_list; ++i)
                    {
                        wait_list[i] = events[i].Handle;
                    }
                    if (events == null)
                        wait_list = null;

                    IntPtr event_ptr = IntPtr.Zero;

                    ClHelper.GetError(Cl.EnqueueBarrierWithWaitList(
                        Handle, (uint)num_events_in_wait_list, wait_list, &event_ptr));

                    return new Event(event_ptr);
                }
            }
            catch (EntryPointNotFoundException)
            {
                throw ClHelper.VersionException(1, 2);
            }
        }

        public Context Context
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    ClHelper.GetError(Cl.GetCommandQueueInfo(
                        Handle, Cl.QUEUE_CONTEXT, param_value_size, &value, null));
                    return new Context(value);
                }
            }
        }

        public Device Device
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    ClHelper.GetError(Cl.GetCommandQueueInfo(
                        Handle, Cl.QUEUE_DEVICE, param_value_size, &value, null));
                    return new Device(value);
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
                    ClHelper.GetError(Cl.GetContextInfo(
                        Handle, Cl.CONTEXT_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public CommandQueueProperties Properties
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetContextInfo(
                        Handle, Cl.CONTEXT_REFERENCE_COUNT, param_value_size, &value, null));
                    return (CommandQueueProperties)value;
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
            if (obj is CommandQueue)
            {
                return Equals((CommandQueue)obj);
            }
            return false;
        }

        public bool Equals(CommandQueue other)
        {
            ClHelper.ThrowNullException(Handle);
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
            ClHelper.ThrowNullException(Handle);
            return Handle.ToString();
        }
    }
}
