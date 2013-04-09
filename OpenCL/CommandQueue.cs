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

        public CommandQueue(Context context, Device device, CommandQueueProperties properties)
        {
            unsafe
            {
                int error = 0;
                Handle = OpenCL.clCreateCommandQueue(context.Handle, device.Handle, (ulong)properties, &error);
                OpenCLHelper.GetError(error);
            }
        }

        public void RetainCommandQueue()
        {
            OpenCLHelper.ThrowNullException(Handle);
            OpenCLHelper.GetError(OpenCL.clRetainCommandQueue(Handle));
        }

        public void ReleaseCommandQueue()
        {
            OpenCLHelper.ThrowNullException(Handle);
            OpenCLHelper.GetError(OpenCL.clReleaseCommandQueue(Handle));
        }

        public void Flush()
        {
            OpenCLHelper.ThrowNullException(Handle);
            OpenCLHelper.GetError(OpenCL.clFlush(Handle));
        }

        public void Finish()
        {
            OpenCLHelper.ThrowNullException(Handle);
            OpenCLHelper.GetError(OpenCL.clFinish(Handle));
        }

        public Context Context
        {
            get
            {
                OpenCLHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    OpenCLHelper.GetError(OpenCL.clGetCommandQueueInfo(
                        Handle, OpenCL.CL_QUEUE_CONTEXT, param_value_size, &value, null));
                    return new Context(value);
                }
            }
        }

        public Device Device
        {
            get
            {
                OpenCLHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    OpenCLHelper.GetError(OpenCL.clGetCommandQueueInfo(
                        Handle, OpenCL.CL_QUEUE_DEVICE, param_value_size, &value, null));
                    return new Device(value);
                }
            }
        }

        public long ReferenceCount
        {
            get
            {
                OpenCLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    OpenCLHelper.GetError(OpenCL.clGetContextInfo(
                        Handle, OpenCL.CL_CONTEXT_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public CommandQueueProperties Properties
        {
            get
            {
                OpenCLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    OpenCLHelper.GetError(OpenCL.clGetContextInfo(
                        Handle, OpenCL.CL_CONTEXT_REFERENCE_COUNT, param_value_size, &value, null));
                    return (CommandQueueProperties)value;
                }
            }
        }

        public override int GetHashCode()
        {
            OpenCLHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            OpenCLHelper.ThrowNullException(Handle);
            if (obj is CommandQueue)
            {
                return Equals((CommandQueue)obj);
            }
            return false;
        }

        public bool Equals(CommandQueue other)
        {
            OpenCLHelper.ThrowNullException(Handle);
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
            OpenCLHelper.ThrowNullException(Handle);
            return Handle.ToString();
        }
    }
}
