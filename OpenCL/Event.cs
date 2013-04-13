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
            if (context == Context.Null)
                throw new ArgumentNullException("context");

            try
            {
                unsafe
                {
                    int error;
                    Handle = Cl.CreateUserEvent(context.Handle, &error);
                    ClHelper.GetError(error);
                }
            }
            catch (EntryPointNotFoundException)
            {
                throw ClHelper.VersionException(1, 1);
            }
        }

        public void Retain()
        {
            ClHelper.ThrowNullException(Handle);
            ClHelper.GetError(Cl.RetainEvent(Handle));
        }

        public void Release()
        {
            ClHelper.ThrowNullException(Handle);
            int error = Cl.ReleaseEvent(Handle);
            Handle = IntPtr.Zero;
            ClHelper.GetError(error);
        }

        public void SetUserEventStatus(int status)
        {
            ClHelper.ThrowNullException(Handle);

            if (status > 0)
                throw new ArgumentException("status must be zero or negative.");

            try
            {
                ClHelper.GetError(Cl.SetUserEventStatus(Handle, status));
            }
            catch (EntryPointNotFoundException)
            {
                throw ClHelper.VersionException(1, 1);
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
            ClHelper.ThrowNullException(Handle);

            if (notify == null)
                throw new ArgumentNullException("notify");

            unsafe
            {
                var function_ptr = IntPtr.Zero;
                var data_handle = new GCHandle();
                var data = Tuple.Create(notify, user_data);
                data_handle = GCHandle.Alloc(data);

                function_ptr = Marshal.GetFunctionPointerForDelegate(new CallbackDelegete(Callback));

                try
                {
                    ClHelper.GetError(Cl.SetEventCallback(
                        Handle, (int)CommandExecutionStatus.Complete, function_ptr, GCHandle.ToIntPtr(data_handle).ToPointer()));
                }
                catch (Exception)
                {
                    data_handle.Free();
                    throw;
                }
            }
        }

        CommandQueue CommandQueue
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    ClHelper.GetError(Cl.GetEventInfo(
                        Handle, Cl.EVENT_COMMAND_QUEUE, param_value_size, &value, null));
                    return new CommandQueue(value);
                }
            }
        }

        Context Context
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        IntPtr value;
                        UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                        ClHelper.GetError(Cl.GetEventInfo(
                            Handle, Cl.EVENT_CONTEXT, param_value_size, &value, null));
                        return new Context(value);
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(1, 1);
                }
            }
        }

        public uint CommandType
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetEventInfo(
                        Handle, Cl.EVENT_COMMAND_TYPE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public CommandExecutionStatus CommandExecutionStatus
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    int value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(int));
                    ClHelper.GetError(Cl.GetEventInfo(
                        Handle, Cl.EVENT_COMMAND_EXECUTION_STATUS, param_value_size, &value, null));
                    return (CommandExecutionStatus)value;
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
                    ClHelper.GetError(Cl.GetEventInfo(
                        Handle, Cl.EVENT_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public TimeSpan TimeQueued
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong nanoseconds;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetEventProfilingInfo(
                        Handle, Cl.PROFILING_COMMAND_QUEUED, param_value_size, &nanoseconds, null));
                    return new TimeSpan((long)(nanoseconds / 100));
                }
            }
        }

        public TimeSpan TimeSubmited
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong nanoseconds;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetEventProfilingInfo(
                        Handle, Cl.PROFILING_COMMAND_SUBMIT, param_value_size, &nanoseconds, null));
                    return new TimeSpan((long)(nanoseconds / 100));
                }
            }
        }

        public TimeSpan TimeStarted
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong nanoseconds;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetEventProfilingInfo(
                        Handle, Cl.PROFILING_COMMAND_START, param_value_size, &nanoseconds, null));
                    return new TimeSpan((long)(nanoseconds / 100));
                }
            }
        }

        public TimeSpan TimeEnded
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong nanoseconds;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetEventProfilingInfo(
                        Handle, Cl.PROFILING_COMMAND_END, param_value_size, &nanoseconds, null));
                    return new TimeSpan((long)(nanoseconds / 100));
                }
            }
        }

        public static void Wait(Event @event)
        {
            if (@event == null)
                throw new ArgumentNullException("event");

            unsafe
            {
                IntPtr list = @event.Handle;
                int error = Cl.WaitForEvents(1, &list);
                ClHelper.GetError(error);
            }
        }

        public static void Wait(Event[] events)
        {
            if (events == null)
                throw new ArgumentNullException("events");
            if (events.Length == 0)
                throw new ArgumentException("events is empty.", "events");

            unsafe
            {
                IntPtr* list = stackalloc IntPtr[events.Length];
                for (int i = 0; i < events.Length; ++i)
                {
                    list[i] = events[i].Handle;
                }
                int error = Cl.WaitForEvents((uint)events.Length, list);
                ClHelper.GetError(error);
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
            if (obj is Event)
            {
                return Equals((Event)obj);
            }
            return false;
        }

        public bool Equals(Event other)
        {
            ClHelper.ThrowNullException(Handle);
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
            ClHelper.ThrowNullException(Handle);
            return Handle.ToString();
        }
    }
}
