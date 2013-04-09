using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public struct Platform : IEquatable<Platform>
    {
        public static Platform[] GetPlatforms()
        {
            unsafe
            {
                uint num_platforms = 0;
                OpenCLHelper.GetError(OpenCL.clGetPlatformIDs(0, null, &num_platforms));

                IntPtr* platform_ptrs = stackalloc IntPtr[(int)num_platforms];

                OpenCLHelper.GetError(OpenCL.clGetPlatformIDs(num_platforms, platform_ptrs, null));

                Platform[] platforms = new Platform[num_platforms];

                for (uint i = 0; i < num_platforms; ++i)
                {
                    platforms[i] = new Platform(platform_ptrs[i]);
                }

                return platforms;
            }
        }

        public IntPtr Handle { get; private set; }

        public Platform(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public string Profile
        {
            get
            {
                OpenCLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    OpenCLHelper.GetError(OpenCL.clGetPlatformInfo(
                        Handle, OpenCL.CL_PLATFORM_PROFILE, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    OpenCLHelper.GetError(OpenCL.clGetPlatformInfo(
                        Handle, OpenCL.CL_PLATFORM_PROFILE, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public string Version
        {
            get
            {
                OpenCLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    OpenCLHelper.GetError(OpenCL.clGetPlatformInfo(
                        Handle, OpenCL.CL_PLATFORM_VERSION, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    OpenCLHelper.GetError(OpenCL.clGetPlatformInfo(
                        Handle, OpenCL.CL_PLATFORM_VERSION, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public string Name
        {
            get
            {
                OpenCLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    OpenCLHelper.GetError(OpenCL.clGetPlatformInfo(
                        Handle, OpenCL.CL_PLATFORM_NAME, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    OpenCLHelper.GetError(OpenCL.clGetPlatformInfo(
                        Handle, OpenCL.CL_PLATFORM_NAME, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public string Vendor
        {
            get
            {
                OpenCLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    OpenCLHelper.GetError(OpenCL.clGetPlatformInfo(
                        Handle, OpenCL.CL_PLATFORM_VENDOR, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    OpenCLHelper.GetError(OpenCL.clGetPlatformInfo(
                        Handle, OpenCL.CL_PLATFORM_VENDOR, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public string[] Extensions
        {
            get
            {
                OpenCLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    OpenCLHelper.GetError(OpenCL.clGetPlatformInfo(
                        Handle, OpenCL.CL_PLATFORM_EXTENSIONS, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    OpenCLHelper.GetError(OpenCL.clGetPlatformInfo(
                        Handle, OpenCL.CL_PLATFORM_EXTENSIONS, param_value_size_ret, data_ptr, null));

                    var exts = Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                    return exts.Split(' ');
                }
            }
        }

        public Device[] GetDevices(DeviceType deviceType)
        {
            OpenCLHelper.ThrowNullException(Handle);
            unsafe
            {
                uint num_devices = 0;
                OpenCLHelper.GetError(OpenCL.clGetDeviceIDs(Handle, (uint)deviceType, 0, null, &num_devices));

                IntPtr* device_ptrs = stackalloc IntPtr[(int)num_devices];

                OpenCLHelper.GetError(OpenCL.clGetDeviceIDs(Handle, (uint)deviceType, num_devices, device_ptrs, null));

                Device[] devices = new Device[num_devices];

                for (uint i = 0; i < num_devices; ++i)
                {
                    devices[i] = new Device(device_ptrs[i]);
                }

                return devices;
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
            if (obj is Platform)
            {
                return Equals((Platform)obj);
            }
            return false;
        }

        public bool Equals(Platform other)
        {
            OpenCLHelper.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Platform left, Platform right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Platform left, Platform right)
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
