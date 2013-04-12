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
        public static readonly Context Null = new Context();

        public IntPtr Handle { get; private set; }

        public Context(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        private delegate void CallbackDelegete(IntPtr errinfo, IntPtr private_info, UIntPtr cb, IntPtr user_data);
        private static unsafe void Callback(IntPtr errinfo, IntPtr private_info, UIntPtr cb, IntPtr user_data)
        {
            var handel = GCHandle.FromIntPtr(user_data);
            var data = (Tuple<Action<string, byte[], object>, object>)handel.Target;
            handel.Free();

            var str = Marshal.PtrToStringAnsi(errinfo);

            var info = new byte[cb.ToUInt32()];

            Marshal.Copy(private_info, info, 0, info.Length);

            data.Item1(str, info, data.Item2);
        }

        public Context(Dictionary<IntPtr, IntPtr> properties, Device[] devices, Action<string, byte[], object> notify, object user_data)
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

                var function_ptr = IntPtr.Zero;
                var data_ptr = IntPtr.Zero;

                if (notify != null)
                {
                    var data = Tuple.Create(notify, user_data);
                    data_ptr = GCHandle.ToIntPtr(GCHandle.Alloc(data));

                    function_ptr = Marshal.GetFunctionPointerForDelegate(new CallbackDelegete(Callback));
                }
                
                int errcode = 0;
                Handle = CL.CreateContext(properties_ptr, (uint)devices.Length, device_ptrs, function_ptr, data_ptr.ToPointer(), &errcode);
                CLHelper.GetError(errcode);
            }
        }

        public Context(Dictionary<IntPtr, IntPtr> properties, DeviceType deviceType, Action<string, byte[], object> notify, object user_data)
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

                var function_ptr = IntPtr.Zero;
                var data_ptr = IntPtr.Zero;

                if (notify != null)
                {
                    var data = Tuple.Create(notify, user_data);
                    data_ptr = GCHandle.ToIntPtr(GCHandle.Alloc(data));

                    function_ptr = Marshal.GetFunctionPointerForDelegate(new Action<IntPtr, IntPtr, UIntPtr, IntPtr>(Callback));
                }

                int errcode = 0;
                Handle = CL.CreateContext(properties_ptr, (int)deviceType, function_ptr, data_ptr.ToPointer(), &errcode);
                CLHelper.GetError(errcode);
            }
        }

        public void Retain()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.RetainContext(Handle));
        }

        public void Release()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.ReleaseContext(Handle));
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

        public Device[] Devices
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint num_devices;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetContextInfo(
                        Handle, CL.CONTEXT_NUM_DEVICES, param_value_size, &num_devices, null));

                    IntPtr* device_ptrs = stackalloc IntPtr[(int)num_devices];

                    param_value_size = new UIntPtr((uint)(IntPtr.Size * num_devices));
                    CLHelper.GetError(CL.GetContextInfo(
                        Handle, CL.CONTEXT_DEVICES, param_value_size, device_ptrs, null));

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
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetContextInfo(
                        Handle, CL.CONTEXT_PROPERTIES, UIntPtr.Zero, null, &param_value_size_ret));

                    var properties = new Dictionary<IntPtr, IntPtr>();

                    if (param_value_size_ret != UIntPtr.Zero)
                    {
                        IntPtr* data_ptr = stackalloc IntPtr[(int)param_value_size_ret.ToUInt32()];

                        CLHelper.GetError(CL.GetContextInfo(
                            Handle, CL.CONTEXT_PROPERTIES, param_value_size_ret, data_ptr, null));

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
            CLHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            CLHelper.ThrowNullException(Handle);
            if (obj is Context)
            {
                return Equals((Context)obj);
            }
            return false;
        }

        public bool Equals(Context other)
        {
            CLHelper.ThrowNullException(Handle);
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
            CLHelper.ThrowNullException(Handle);
            return Handle.ToString();
        }
    }
}
