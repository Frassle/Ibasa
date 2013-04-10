using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public struct Device : IEquatable<Device>
    {
        public IntPtr Handle { get; private set; }

        public Device(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public void Retain()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.RetainDevice(Handle));
        }

        public void Release()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.ReleaseDevice(Handle));
        }

        private unsafe Device[] CreateSubDevices(IntPtr* properties)
        {
            uint num_devices;
            CLHelper.GetError(CL.CreateSubDevices(Handle, properties, 0, null, &num_devices));

            IntPtr* device_ptrs = stackalloc IntPtr[(int)num_devices];

            CLHelper.GetError(CL.CreateSubDevices(Handle, properties, num_devices, device_ptrs, null));

            Device[] sub_devices = new Device[num_devices];
            for (uint i = 0; i < num_devices; ++i)
            {
                sub_devices[i] = new Device(device_ptrs[i]);
            }

            return sub_devices;
        }

        public Device[] CreateSubDevicesEqually(int units)
        {
            CLHelper.ThrowNullException(Handle);

            unsafe
            {
                IntPtr* properties = stackalloc IntPtr[3];
                properties[0] = new IntPtr(CL.DEVICE_PARTITION_EQUALLY);
                properties[1] = new IntPtr(units);
                properties[2] = new IntPtr(0);

                return CreateSubDevices(properties);
            }
        }

        public Device[] CreateSubDevicesByCounts(params int[] counts)
        {
            CLHelper.ThrowNullException(Handle);

            unsafe
            {
                IntPtr* properties = stackalloc IntPtr[3 + counts.Length];
                properties[0] = new IntPtr(CL.DEVICE_PARTITION_BY_COUNTS);

                for (int i = 0; i < counts.Length; ++i)
                {
                    properties[1 + i] = new IntPtr(counts[i]);
                }
                properties[counts.Length + 1] = new IntPtr(CL.DEVICE_PARTITION_BY_COUNTS_LIST_END);
                properties[counts.Length + 2] = new IntPtr(0);

                return CreateSubDevices(properties);
            }
        }

        public long AddressBits
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_ADDRESS_BITS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public bool Available
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_AVAILABLE, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public string[] BuiltInKernels
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_BUILT_IN_KERNELS, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_BUILT_IN_KERNELS, param_value_size_ret, data_ptr, null));

                    var str = Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                    return str.Split(';');
                }
            }
        }

        public bool CompilerAvailable
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_COMPILER_AVAILABLE, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public string Name
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_NAME, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_NAME, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
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
            if (obj is Device)
            {
                return Equals((Device)obj);
            }
            return false;
        }

        public bool Equals(Device other)
        {
            CLHelper.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Device left, Device right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Device left, Device right)
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
