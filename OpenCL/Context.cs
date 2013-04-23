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

            var str = Marshal.PtrToStringAnsi(errinfo);

            var info = new byte[cb.ToUInt32()];

            Marshal.Copy(private_info, info, 0, info.Length);

            data.Item1(str, info, data.Item2);
        }

        private static Dictionary<IntPtr, GCHandle> CallbackPointers = new Dictionary<IntPtr, GCHandle>();

        public Context(Dictionary<IntPtr, IntPtr> properties, Device[] devices, Action<string, byte[], object> notify, object user_data)
            : this()
        {
            if (devices == null)
                throw new ArgumentNullException("devices");

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
                    properties_ptr[index] = IntPtr.Zero;
                }
                else
                {
                    properties_ptr = null;
                }

                var function_ptr = IntPtr.Zero;
                var data_handle = new GCHandle();
                var data_ptr = IntPtr.Zero;

                if (notify != null)
                {
                    var data = Tuple.Create(notify, user_data);
                    data_handle = GCHandle.Alloc(data);
                    data_ptr = GCHandle.ToIntPtr(data_handle);

                    function_ptr = Marshal.GetFunctionPointerForDelegate(new CallbackDelegete(Callback));
                }

                try
                {
                    int errcode = 0;
                    Handle = Cl.CreateContext(properties_ptr, (uint)devices.Length, device_ptrs, 
                        function_ptr, data_ptr.ToPointer(), &errcode);
                    ClHelper.GetError(errcode);

                    CallbackPointers.Add(Handle, data_handle);
                }
                catch(Exception)
                {
                    if (data_handle.IsAllocated)
                        data_handle.Free();
                    throw;
                }
            }
        }

        public Context(Dictionary<IntPtr, IntPtr> properties, DeviceType deviceType, Action<string, byte[], object> notify, object user_data)
            : this()
        {
            unsafe
            {
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
                    properties_ptr[index] = IntPtr.Zero;
                }
                else
                {
                    properties_ptr = null;
                }

                var function_ptr = IntPtr.Zero;
                var data_handle = new GCHandle();
                var data_ptr = IntPtr.Zero;

                if (notify != null)
                {
                    var data = Tuple.Create(notify, user_data);
                    data_handle = GCHandle.Alloc(data);
                    data_ptr = GCHandle.ToIntPtr(data_handle);

                    function_ptr = Marshal.GetFunctionPointerForDelegate(new Action<IntPtr, IntPtr, UIntPtr, IntPtr>(Callback));
                }
                
                try
                {
                    int errcode = 0;
                    Handle = Cl.CreateContextFromType(properties_ptr, (int)deviceType,
                        function_ptr, data_ptr.ToPointer(), &errcode);
                    ClHelper.GetError(errcode);

                    CallbackPointers.Add(Handle, data_handle);
                }
                catch (Exception)
                {
                    if(data_handle.IsAllocated)
                        data_handle.Free();
                    throw;
                }
            }
        }

        public void Retain()
        {
            ClHelper.ThrowNullException(Handle);
            ClHelper.GetError(Cl.RetainContext(Handle));
        }

        public void Release()
        {
            ClHelper.ThrowNullException(Handle);
            int error = Cl.ReleaseContext(Handle);

            try
            {
                // try to get the ref count
                var test = ReferenceCount;
            }
            catch (OpenCLException)
            {
                // if the context is released ReferenceCount will throw
                var data = CallbackPointers[Handle];
                data.Free();
                CallbackPointers.Remove(Handle);
            }
            finally
            {
                Handle = IntPtr.Zero;
            }

            ClHelper.GetError(error);
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

        public Device[] Devices
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint num_devices;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetContextInfo(
                        Handle, Cl.CONTEXT_NUM_DEVICES, param_value_size, &num_devices, null));

                    IntPtr* device_ptrs = stackalloc IntPtr[(int)num_devices];

                    param_value_size = new UIntPtr((uint)(IntPtr.Size * num_devices));
                    ClHelper.GetError(Cl.GetContextInfo(
                        Handle, Cl.CONTEXT_DEVICES, param_value_size, device_ptrs, null));

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
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    ClHelper.GetError(Cl.GetContextInfo(
                        Handle, Cl.CONTEXT_PROPERTIES, UIntPtr.Zero, null, &param_value_size_ret));

                    var properties = new Dictionary<IntPtr, IntPtr>();

                    if (param_value_size_ret != UIntPtr.Zero)
                    {
                        IntPtr* data_ptr = stackalloc IntPtr[(int)param_value_size_ret.ToUInt32()];

                        ClHelper.GetError(Cl.GetContextInfo(
                            Handle, Cl.CONTEXT_PROPERTIES, param_value_size_ret, data_ptr, null));

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

        public Tuple<ChannelOrder, ChannelType>[] GetSupportedImageFormats(MemoryFlags flags, ImageType type)
        {
            unsafe
            {
                uint num_image_formats = 0;
                ClHelper.GetError(Cl.GetSupportedImageFormats(
                    Handle, (ulong)flags, (uint)type, 0, null, &num_image_formats));

                Cl.image_format* image_formats = stackalloc Cl.image_format[(int)num_image_formats];

                ClHelper.GetError(Cl.GetSupportedImageFormats(
                    Handle, (ulong)flags, (uint)type, num_image_formats, image_formats, null));

                Tuple<ChannelOrder, ChannelType>[] formats = new Tuple<ChannelOrder, ChannelType>[num_image_formats];

                for (uint i = 0; i < num_image_formats; ++i)
                {
                    formats[i] = Tuple.Create(
                        (ChannelOrder)image_formats[i].image_channel_order,
                        (ChannelType)image_formats[i].image_channel_data_type);
                }

                return formats;
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
            if (obj is Context)
            {
                return Equals((Context)obj);
            }
            return false;
        }

        public bool Equals(Context other)
        {
            ClHelper.ThrowNullException(Handle);
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
            ClHelper.ThrowNullException(Handle);
            return string.Format("Context: {0}", Handle.ToString());
        }
    }
}
