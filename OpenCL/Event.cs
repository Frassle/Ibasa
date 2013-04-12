using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public struct Event : IEquatable<Event>
    {
        public static readonly Event Null = new Event();

        public IntPtr Handle { get; private set; }

        public Event(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Event(Context context)
            : this()
        {
            try
            {
                unsafe
                {
                    int error;
                    Handle = CL.CreateUserEvent(context.Handle, &error);
                    CLHelper.GetError(error);
                }
            }
            catch (EntryPointNotFoundException)
            {
                throw CLHelper.VersionException(1, 1);
            }
        }

        public void Retain()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.RetainEvent(Handle));
        }

        public void Release()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.ReleaseEvent(Handle));
        }

        public void SetUserEventStatus(int status)
        {
            CLHelper.ThrowNullException(Handle);

            if (status > 0)
                throw new ArgumentException("status must be zero or negative.");

            try
            {
                CLHelper.GetError(CL.SetUserEventStatus(Handle, status));
            }
            catch (EntryPointNotFoundException)
            {
                throw CLHelper.VersionException(1, 1);
            }
        }

        private delegate void CallbackDelegete(IntPtr @event, int event_command_exec_status, IntPtr user_data);
        private static void Callback(IntPtr @event, int event_command_exec_status, IntPtr user_data)
        {
            var handel = GCHandle.FromIntPtr(user_data);
            var data = (Tuple<Action<Event, CommandExecutionStatus, object>, object>)handel.Target;
            handel.Free();

            data.Item1(new Event(@event), (CommandExecutionStatus)event_command_exec_status, data.Item2);            
        }

        public void SetCallback(
            Action<Event, CommandExecutionStatus, object> notify,
            object user_data)
        {
            CLHelper.ThrowNullException(Handle);

            if (notify == null)
                throw new ArgumentNullException("notify");

            var function_ptr = IntPtr.Zero;
            var data_handle = new GCHandle();

            try
            {
                unsafe
                {
                    var data = Tuple.Create(notify, user_data);
                    data_handle = GCHandle.Alloc(data);

                    function_ptr = Marshal.GetFunctionPointerForDelegate(new CallbackDelegete(Callback));

                    CLHelper.GetError(CL.SetEventCallback(
                        Handle, (int)CommandExecutionStatus.Complete, function_ptr, GCHandle.ToIntPtr(data_handle).ToPointer()));
                }
            }
            catch (OpenCLException)
            {
                data_handle.Free();

                throw CLHelper.VersionException(1, 1);
            }
        }

        CommandQueue CommandQueue
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    CLHelper.GetError(CL.GetEventInfo(
                        Handle, CL.EVENT_COMMAND_QUEUE, param_value_size, &value, null));
                    return new CommandQueue(value);
                }
            }
        }

        Context Context
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        IntPtr value;
                        UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                        CLHelper.GetError(CL.GetEventInfo(
                            Handle, CL.EVENT_CONTEXT, param_value_size, &value, null));
                        return new Context(value);
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(1, 1);
                }
            }
        }

        public uint CommandType
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetEventInfo(
                        Handle, CL.EVENT_COMMAND_TYPE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public CommandExecutionStatus CommandExecutionStatus
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    int value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(int));
                    CLHelper.GetError(CL.GetEventInfo(
                        Handle, CL.EVENT_COMMAND_EXECUTION_STATUS, param_value_size, &value, null));
                    return (CommandExecutionStatus)value;
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
                    CLHelper.GetError(CL.GetEventInfo(
                        Handle, CL.EVENT_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public TimeSpan TimeQueued
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong nanoseconds;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetEventProfilingInfo(
                        Handle, CL.PROFILING_COMMAND_QUEUED, param_value_size, &nanoseconds, null));
                    return new TimeSpan((long)(nanoseconds / 100));
                }
            }
        }

        public TimeSpan TimeSubmited
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong nanoseconds;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetEventProfilingInfo(
                        Handle, CL.PROFILING_COMMAND_SUBMIT, param_value_size, &nanoseconds, null));
                    return new TimeSpan((long)(nanoseconds / 100));
                }
            }
        }

        public TimeSpan TimeStarted
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong nanoseconds;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetEventProfilingInfo(
                        Handle, CL.PROFILING_COMMAND_START, param_value_size, &nanoseconds, null));
                    return new TimeSpan((long)(nanoseconds / 100));
                }
            }
        }

        public TimeSpan TimeEnded
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong nanoseconds;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetEventProfilingInfo(
                        Handle, CL.PROFILING_COMMAND_END, param_value_size, &nanoseconds, null));
                    return new TimeSpan((long)(nanoseconds / 100));
                }
            }
        }

        public static void Wait(Event @event)
        {
            unsafe
            {
                IntPtr list = @event.Handle;
                int error = CL.WaitForEvents(1, &list);
                CLHelper.GetError(error);
            }
        }

        public static void Wait(Event[] events)
        {
            unsafe
            {
                IntPtr* list = stackalloc IntPtr[events.Length];
                for (int i = 0; i < events.Length; ++i)
                {
                    list[i] = events[i].Handle;
                }
                int error = CL.WaitForEvents((uint)events.Length, list);
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
            if (obj is Event)
            {
                return Equals((Event)obj);
            }
            return false;
        }

        public bool Equals(Event other)
        {
            CLHelper.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Event left, Event right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Event left, Event right)
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
