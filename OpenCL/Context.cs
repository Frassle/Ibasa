using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public struct Context : IEquatable<Context>
    {
        public IntPtr Handle { get; private set; }

        public Context(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Context(Dictionary<IntPtr, IntPtr> properties, Device[] devices)
            : this()
        {
            unsafe
            {
                IntPtr* device_ptrs = stackalloc IntPtr[devices.Length];

                for (int i = 0; i < devices.Length; ++i)
                {
                    device_ptrs[i] = devices[i].Handle;
                }

                int property_count = properties == null ? 0 : properties.Count;

                IntPtr* properties_ptr = stackalloc IntPtr[property_count * 2 + 1];

                int index = 0;
                if (properties != null)
                {
                    foreach (var pair in properties)
                    {
                        properties_ptr[index++] = pair.Key;
                        properties_ptr[index++] = pair.Value;
                    }
                }
                properties_ptr[index] = IntPtr.Zero;

                int errcode = 0;
                Handle = OpenCL.clCreateContext(properties_ptr, (uint)devices.Length, device_ptrs, IntPtr.Zero, null, &errcode);
                OpenCLHelper.GetError(errcode);
            }
        }

        public Context(Dictionary<IntPtr, IntPtr> properties, DeviceType deviceType)
            : this()
        {
            unsafe
            {
                int property_count = properties == null ? 0 : properties.Count;

                IntPtr* properties_ptr = stackalloc IntPtr[property_count * 2];

                int index = 0;
                if (properties != null)
                {
                    foreach (var pair in properties)
                    {
                        properties_ptr[index++] = pair.Key;
                        properties_ptr[index++] = pair.Value;
                    }
                }
                properties_ptr[index] = IntPtr.Zero;

                int errcode = 0;
                Handle = OpenCL.clCreateContext(properties_ptr, (int)deviceType, IntPtr.Zero, null, &errcode);
                OpenCLHelper.GetError(errcode);
            }
        }


        public void RetainContext()
        {
            OpenCLHelper.ThrowNullException(Handle);
            OpenCLHelper.GetError(OpenCL.clRetainContext(Handle));
        }

        public void ReleaseContext()
        {
            OpenCLHelper.ThrowNullException(Handle);
            OpenCLHelper.GetError(OpenCL.clReleaseContext(Handle));
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

        public Device[] Devices
        {
            get
            {
                OpenCLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint num_devices;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    OpenCLHelper.GetError(OpenCL.clGetContextInfo(
                        Handle, OpenCL.CL_CONTEXT_NUM_DEVICES, param_value_size, &num_devices, null));

                    IntPtr* device_ptrs = stackalloc IntPtr[(int)num_devices];

                    param_value_size = new UIntPtr((uint)(IntPtr.Size * num_devices));
                    OpenCLHelper.GetError(OpenCL.clGetContextInfo(
                        Handle, OpenCL.CL_CONTEXT_DEVICES, param_value_size, device_ptrs, null));

                    Device[] devices = new Device[num_devices];
                    for (int i = 0; i < devices.Length; ++i)
                    {
                        devices[i] = new Device(device_ptrs[i]);
                    }

                    return devices;
                }
            }
        }

        public Dictionary<IntPtr, IntPtr> Properties
        {
            get
            {
                OpenCLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    OpenCLHelper.GetError(OpenCL.clGetContextInfo(
                        Handle, OpenCL.CL_CONTEXT_PROPERTIES, UIntPtr.Zero, null, &param_value_size_ret));

                    var properties = new Dictionary<IntPtr, IntPtr>();

                    if (param_value_size_ret != UIntPtr.Zero)
                    {
                        IntPtr* data_ptr = stackalloc IntPtr[(int)param_value_size_ret.ToUInt32()];

                        OpenCLHelper.GetError(OpenCL.clGetContextInfo(
                            Handle, OpenCL.CL_CONTEXT_PROPERTIES, param_value_size_ret, data_ptr, null));

                        while (*data_ptr != IntPtr.Zero)
                        {
                            var key = *data_ptr++;
                            var value = *data_ptr++;

                            properties.Add(key, value);
                        }
                    }

                    return properties;
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
            if (obj is Context)
            {
                return Equals((Context)obj);
            }
            return false;
        }

        public bool Equals(Context other)
        {
            OpenCLHelper.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Context left, Context right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Context left, Context right)
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
