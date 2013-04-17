using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public struct Program : IEquatable<Program>
    {
        public static readonly Program Null = new Program();

        public IntPtr Handle { get; private set; }

        public Program(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Program(Context context, string source)
            : this()
        {
            if (context == Context.Null)
                throw new ArgumentNullException("context");
            if (source == null)
                throw new ArgumentNullException("source");

            unsafe
            {
                var bytes = Encoding.ASCII.GetByteCount(source);
                byte* strings = stackalloc byte[bytes];

                fixed (char* source_ptr = source)
                {
                    Encoding.ASCII.GetBytes(source_ptr, source.Length, strings, bytes);
                }

                UIntPtr length = new UIntPtr((uint)bytes);

                int errcode = 0;
                Handle = Cl.CreateProgramWithSource(context.Handle, 1, &strings, &length, &errcode);
                ClHelper.GetError(errcode);
            }
        }

        public Program(Context context, Device[] devices, byte[][] binaries)
            : this()
        {
            if (context == Context.Null)
                throw new ArgumentNullException("context");
            if (devices == null)
                throw new ArgumentNullException("devices");
            if (binaries == null)
                throw new ArgumentNullException("binaries");
            if (devices.Length == 0)
                throw new ArgumentException("devices is empty.", "devices");
            if (devices.Length != binaries.Length)
                throw new ArgumentException("binaries must be the same length as devices.", "binaries");

            unsafe
            {
                var device_list = stackalloc IntPtr[devices.Length];

                for (int i = 0; i < devices.Length; ++i)
                {
                    device_list[i] = devices[i].Handle;
                }

                var lengths_list = stackalloc UIntPtr[binaries.Length];
                var binary_list = stackalloc byte*[binaries.Length];
                var binary_status = stackalloc int[binaries.Length];

                for (int i = 0; i < binaries.Length; ++i)
                {
                    lengths_list[i] = new UIntPtr((uint)binaries[i].Length);

                    var handle = GCHandle.Alloc(binaries[i], GCHandleType.Pinned);
                    binary_list[i] = (byte*)GCHandle.ToIntPtr(handle).ToPointer();
                }

                int errcode = 0;
                Handle = Cl.CreateProgramWithBinary(context.Handle,
                    (uint)devices.Length, device_list, lengths_list, binary_list, binary_status, &errcode);

                for (int i = 0; i < binaries.Length; ++i)
                {
                    var handel = GCHandle.FromIntPtr(new IntPtr(binary_list[i]));
                    handel.Free();
                }

                List<int> errors = new List<int>(binaries.Length);

                for (int i = 0; i < binaries.Length; ++i)
                {
                    if (binary_status[i] != Cl.SUCCESS)
                    {
                        errors.Add(i);
                    }
                }

                if (errors.Count != 0)
                {
                    throw new OpenCLException(string.Format("{0} {1} failed.", 
                        errors.Count == 1 ? "Binary" : "Binaries",
                        string.Join(", ", errors)));
                }

                ClHelper.GetError(errcode);
            }
        }

        private delegate void CallbackDelegate(IntPtr program, IntPtr user_data);
        private static unsafe void Callback(IntPtr program, IntPtr user_data)
        {
            var handel = GCHandle.FromIntPtr(user_data);
            var data = (Tuple<Action<Program, object>, object>)handel.Target;
            handel.Free();

            data.Item1(new Program(program), data.Item2);
        }

        public void BuildProgram(Device[] devices, string options, Action<Program, object> notify, object user_data)
        {
            unsafe
            {
                var devices_length = devices == null ? 0 : devices.Length;
                var device_list = stackalloc IntPtr[devices_length];

                for (int i = 0; i < devices_length; ++i)
                {
                    device_list[i] = devices[i].Handle;
                }

                device_list = devices_length == 0 ? null : device_list;

                int length = options == null ? 0 : Encoding.ASCII.GetByteCount(options);
                byte* chars = stackalloc byte[length + 1];

                if (options != null)
                {
                    fixed (char* options_ptr = options)
                    {
                        Encoding.ASCII.GetBytes(options_ptr, options.Length, chars, length);
                    }
                    chars[length] = 0; //null terminator
                }
                else
                {
                    chars = null;
                }

                var function_ptr = IntPtr.Zero;
                var data_ptr = new GCHandle();

                if (notify != null)
                {
                    var data = Tuple.Create(notify, user_data);
                    data_ptr = GCHandle.Alloc(data);

                    function_ptr = Marshal.GetFunctionPointerForDelegate(new CallbackDelegate(Callback));
                }

                try
                {
                    ClHelper.GetError(Cl.BuildProgram(Handle, (uint)devices_length, device_list, chars,
                        function_ptr, GCHandle.ToIntPtr(data_ptr).ToPointer()));
                }
                catch(Exception)
                {
                    data_ptr.Free();
                    throw;
                }
            }
        }

        public void CompileProgram(Device[] devices, string options, Tuple<Program, string>[] headers, Action<Program, object> notify, object user_data)
        {
            unsafe
            {
                var devices_length = devices == null ? 0 : devices.Length;
                var device_list = stackalloc IntPtr[devices_length];

                for (int i = 0; i < devices_length; ++i)
                {
                    device_list[i] = devices[i].Handle;
                }

                device_list = devices_length == 0 ? null : device_list;

                int length = options == null ? 0 : Encoding.ASCII.GetByteCount(options);
                byte* chars = stackalloc byte[length + 1];

                if (options != null)
                {
                    fixed (char* options_ptr = options)
                    {
                        Encoding.ASCII.GetBytes(options_ptr, options.Length, chars, length);
                    }
                    chars[length] = 0; //null terminator
                }
                else
                {
                    chars = null;
                }

                var num_headers = headers == null ? 0 : headers.Length;

                IntPtr* input_headers = stackalloc IntPtr[num_headers];
                byte** header_include_names = stackalloc byte*[num_headers];

                for (int i = 0; i < num_headers; ++i)
                {
                    input_headers[i] = headers[i].Item1.Handle;

                    var name = headers[i].Item2;
                    var header_length = Encoding.ASCII.GetByteCount(name);
                    byte* header_chars = (byte*)Marshal.AllocHGlobal(length + 1).ToPointer();
                    fixed (char* name_ptr = name)
                    {
                        Encoding.ASCII.GetBytes(name_ptr, name.Length, header_chars, header_length);
                    }
                    header_chars[header_length] = 0; //null terminator
                    header_include_names[i] = header_chars;
                }

                if (headers == null)
                {
                    input_headers = null;
                    header_include_names = null;
                }

                var function_ptr = IntPtr.Zero;
                var data_ptr = new GCHandle();

                if (notify != null)
                {
                    var data = Tuple.Create(notify, user_data);
                    data_ptr = GCHandle.Alloc(data);

                    function_ptr = Marshal.GetFunctionPointerForDelegate(new CallbackDelegate(Callback));
                }

                try
                {
                    ClHelper.GetError(Cl.CompileProgram(Handle, (uint)devices_length, device_list, chars,
                        (uint)num_headers, input_headers, header_include_names,
                        function_ptr, GCHandle.ToIntPtr(data_ptr).ToPointer()));
                }
                catch (Exception)
                {
                    data_ptr.Free();
                    throw;
                }
                finally
                {
                    for (int i = 0; i < num_headers; ++i)
                    {
                        Marshal.FreeHGlobal(new IntPtr(header_include_names[i]));
                    }
                }
            }
        }

        public static Program LinkProgram(Context context, Device[] devices, string options, Program[] programs, Action<Program, object> notify, object user_data)
        {
            if (context == Context.Null)
                throw new ArgumentNullException("context");

            unsafe
            {
                var devices_length = devices == null ? 0 : devices.Length;
                var device_list = stackalloc IntPtr[devices_length];

                for (int i = 0; i < devices_length; ++i)
                {
                    device_list[i] = devices[i].Handle;
                }

                device_list = devices_length == 0 ? null : device_list;

                int length = options == null ? 0 : Encoding.ASCII.GetByteCount(options);
                byte* chars = stackalloc byte[length + 1];

                if (options != null)
                {
                    fixed (char* options_ptr = options)
                    {
                        Encoding.ASCII.GetBytes(options_ptr, options.Length, chars, length);
                    }
                    chars[length] = 0; //null terminator
                }
                else
                {
                    chars = null;
                }

                var num_programs = programs == null ? 0 : programs.Length;

                IntPtr* input_programs = stackalloc IntPtr[num_programs];

                for (int i = 0; i < num_programs; ++i)
                {
                    input_programs[i] = programs[i].Handle;
                }

                var function_ptr = IntPtr.Zero;
                var data_ptr = new GCHandle();

                if (notify != null)
                {
                    var data = Tuple.Create(notify, user_data);
                    data_ptr = GCHandle.Alloc(data);

                    function_ptr = Marshal.GetFunctionPointerForDelegate(new CallbackDelegate(Callback));
                }

                try
                {
                    int errcode = 0;
                    var program = Cl.LinkProgram(context.Handle, (uint)devices_length, device_list, chars,
                        (uint)num_programs, input_programs,
                        function_ptr, GCHandle.ToIntPtr(data_ptr).ToPointer(), &errcode);
                    ClHelper.GetError(errcode);

                    return new Program(program);
                }
                catch (Exception)
                {
                    data_ptr.Free();
                    throw;
                }
            }
        }

        public void RetainProgram()
        {
            ClHelper.ThrowNullException(Handle);
            ClHelper.GetError(Cl.RetainProgram(Handle));
        }

        public void ReleaseProgram()
        {
            ClHelper.ThrowNullException(Handle);
            int error = Cl.ReleaseProgram(Handle);
            Handle = IntPtr.Zero;
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
                    ClHelper.GetError(Cl.GetProgramInfo(
                        Handle, Cl.PROGRAM_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
                }
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
                    ClHelper.GetError(Cl.GetProgramInfo(
                        Handle, Cl.PROGRAM_CONTEXT, param_value_size, &value, null));
                    return new Context(value);
                }
            }
        }

        public Device[] Devices
        {
            get
            {
                unsafe
                {
                    uint num_devices;
                    UIntPtr param_value_size = new UIntPtr((uint)sizeof(uint));
                    ClHelper.GetError(Cl.GetProgramInfo(
                        Handle, Cl.PROGRAM_NUM_DEVICES, param_value_size, &num_devices, null));

                    IntPtr* device_ptrs = stackalloc IntPtr[(int)num_devices];
                    param_value_size = new UIntPtr((uint)(IntPtr.Size * num_devices));
                    ClHelper.GetError(Cl.GetProgramInfo(
                        Handle, Cl.PROGRAM_DEVICES, param_value_size, device_ptrs, null));

                    Device[] devices = new Device[(int)num_devices];
                    for (int i = 0; i < devices.Length; ++i)
                    {
                        devices[i] = new Device(device_ptrs[i]);
                    }

                    return devices;
                }
            }
        }

        public string Source
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    ClHelper.GetError(Cl.GetProgramInfo(
                        Handle, Cl.PROGRAM_SOURCE, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    ClHelper.GetError(Cl.GetProgramInfo(
                        Handle, Cl.PROGRAM_SOURCE, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public byte[][] Binaries
        {
            get
            {
                unsafe
                {
                    uint num_devices;
                    UIntPtr param_value_size = new UIntPtr((uint)sizeof(uint));
                    ClHelper.GetError(Cl.GetProgramInfo(
                        Handle, Cl.PROGRAM_NUM_DEVICES, param_value_size, &num_devices, null));

                    UIntPtr* binary_sizes = stackalloc UIntPtr[(int)num_devices];
                    param_value_size = new UIntPtr((uint)(UIntPtr.Size * num_devices));
                    ClHelper.GetError(Cl.GetProgramInfo(
                        Handle, Cl.PROGRAM_BINARY_SIZES, param_value_size, &binary_sizes, null));

                    IntPtr* binaries = stackalloc IntPtr[(int)num_devices];
                    param_value_size = new UIntPtr((uint)(IntPtr.Size * num_devices));
                    ClHelper.GetError(Cl.GetProgramInfo(
                        Handle, Cl.PROGRAM_BINARIES, param_value_size, &binaries, null));

                    byte[][] result = new byte[num_devices][];

                    for (int i = 0; i < result.Length; ++i)
                    {
                        byte[] binary = new byte[binary_sizes[i].ToUInt32()];
                        Marshal.Copy(binaries[i], binary, 0, binary.Length);
                        result[i] = binary;
                    }

                    return result;
                }
            }
        }

        public long KernelCount
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    ClHelper.GetError(Cl.GetProgramInfo(
                        Handle, Cl.PROGRAM_NUM_KERNELS, param_value_size, &value, null));
                    return (long)value.ToUInt64();
                }
            }
        }

        public string[] KernelNames
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    ClHelper.GetError(Cl.GetProgramInfo(
                        Handle, Cl.PROGRAM_KERNEL_NAMES, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    ClHelper.GetError(Cl.GetProgramInfo(
                        Handle, Cl.PROGRAM_KERNEL_NAMES, param_value_size_ret, data_ptr, null));

                    var str = Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                    return str.Split(';');
                }
            }
        }

        public struct BuildInfo
        {
            Program Program;
            Device Device;

            internal BuildInfo(Program program, Device device)
            {
                Program = program;
                Device = device;
            }

            public BuildStatus BuildStatus
            {
                get
                {
                    ClHelper.ThrowNullException(Program.Handle);
                    unsafe
                    {
                        int value;
                        UIntPtr param_value_size = new UIntPtr((uint)sizeof(int));
                        ClHelper.GetError(Cl.GetProgramBuildInfo(Program.Handle, Device.Handle,
                            Cl.PROGRAM_BUILD_STATUS, param_value_size, &value, null));
                        return (BuildStatus)value;
                    }
                }
            }

            public string BuildOptions
            {
                get
                {
                    ClHelper.ThrowNullException(Program.Handle);
                    unsafe
                    {
                        UIntPtr param_value_size_ret = UIntPtr.Zero;
                        ClHelper.GetError(Cl.GetProgramBuildInfo(Program.Handle, Device.Handle,
                            Cl.PROGRAM_BUILD_OPTIONS, UIntPtr.Zero, null, &param_value_size_ret));

                        byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                        ClHelper.GetError(Cl.GetProgramBuildInfo(Program.Handle, Device.Handle,
                            Cl.PROGRAM_BUILD_OPTIONS, param_value_size_ret, data_ptr, null));

                        return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                    }
                }
            }

            public string Log
            {
                get
                {
                    ClHelper.ThrowNullException(Program.Handle);
                    unsafe
                    {
                        UIntPtr param_value_size_ret = UIntPtr.Zero;
                        ClHelper.GetError(Cl.GetProgramBuildInfo(Program.Handle, Device.Handle,
                            Cl.PROGRAM_BUILD_LOG, UIntPtr.Zero, null, &param_value_size_ret));

                        byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                        ClHelper.GetError(Cl.GetProgramBuildInfo(Program.Handle, Device.Handle,
                            Cl.PROGRAM_BUILD_LOG, param_value_size_ret, data_ptr, null));

                        return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                    }
                }
            }

            public BinaryType BinaryType
            {
                get
                {
                    ClHelper.ThrowNullException(Program.Handle);
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr((uint)sizeof(uint));
                        ClHelper.GetError(Cl.GetProgramBuildInfo(Program.Handle, Device.Handle,
                            Cl.PROGRAM_BINARY_TYPE, param_value_size, &value, null));
                        return (BinaryType)value;
                    }
                }
            }
        }

        public BuildInfo GetBuildInfo(Device device)
        {
            ClHelper.ThrowNullException(Handle);
            return new BuildInfo(this, device);
        }

        public override int GetHashCode()
        {
            ClHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ClHelper.ThrowNullException(Handle);
            if (obj is Program)
            {
                return Equals((Program)obj);
            }
            return false;
        }

        public bool Equals(Program other)
        {
            ClHelper.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Program left, Program right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Program left, Program right)
        {
            return left.Handle != right.Handle;
        }

        public override string ToString()
        {
            ClHelper.ThrowNullException(Handle);
            return string.Format("Program: {0}", Handle.ToString());
        }
    }
}
