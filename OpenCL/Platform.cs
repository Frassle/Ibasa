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
        public static readonly Platform Null = new Platform();

        public static Platform[] GetPlatforms()
        {
            unsafe
            {
                uint num_platforms = 0;
                CLHelper.GetError(CL.GetPlatformIDs(0, null, &num_platforms));

                IntPtr* platform_ptrs = stackalloc IntPtr[(int)num_platforms];

                CLHelper.GetError(CL.GetPlatformIDs(num_platforms, platform_ptrs, null));

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
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetPlatformInfo(
                        Handle, CL.PLATFORM_PROFILE, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetPlatformInfo(
                        Handle, CL.PLATFORM_PROFILE, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public string Version
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetPlatformInfo(
                        Handle, CL.PLATFORM_VERSION, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetPlatformInfo(
                        Handle, CL.PLATFORM_VERSION, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
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
                    CLHelper.GetError(CL.GetPlatformInfo(
                        Handle, CL.PLATFORM_NAME, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetPlatformInfo(
                        Handle, CL.PLATFORM_NAME, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public string Vendor
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetPlatformInfo(
                        Handle, CL.PLATFORM_VENDOR, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetPlatformInfo(
                        Handle, CL.PLATFORM_VENDOR, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public string[] Extensions
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetPlatformInfo(
                        Handle, CL.PLATFORM_EXTENSIONS, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetPlatformInfo(
                        Handle, CL.PLATFORM_EXTENSIONS, param_value_size_ret, data_ptr, null));

                    var exts = Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                    return exts.Split(' ');
                }
            }
        }

        public Device[] GetDevices(DeviceType deviceType)
        {
            CLHelper.ThrowNullException(Handle);
            unsafe
            {
                uint num_devices = 0;
                CLHelper.GetError(CL.GetDeviceIDs(Handle, (uint)deviceType, 0, null, &num_devices));

                IntPtr* device_ptrs = stackalloc IntPtr[(int)num_devices];

                CLHelper.GetError(CL.GetDeviceIDs(Handle, (uint)deviceType, num_devices, device_ptrs, null));

                Device[] devices = new Device[num_devices];

                for (uint i = 0; i < num_devices; ++i)
                {
                    devices[i] = new Device(device_ptrs[i]);
                }

                return devices;
            }
        }

        [Obsolete("Deprecated OpenCL 1.1 API.")]
        public static void UnloadCompiler()
        {
            CLHelper.GetError(CL.UnloadCompiler());
        }

        public void UnloadPlatformCompiler()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.UnloadPlatformCompiler(Handle));
        }

        public override int GetHashCode()
        {
            CLHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            CLHelper.ThrowNullException(Handle);
            if (obj is Platform)
            {
                return Equals((Platform)obj);
            }
            return false;
        }

        public bool Equals(Platform other)
        {
            CLHelper.ThrowNullException(Handle);
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
            CLHelper.ThrowNullException(Handle);
            return Handle.ToString();
        }
    }
}
