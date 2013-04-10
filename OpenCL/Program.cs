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
        public IntPtr Handle { get; private set; }

        public Program(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Program(Context context, string source)
            : this()
        {
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
                Handle = CL.CreateProgramWithSource(context.Handle, 1, &strings, &length, &errcode);
                CLHelper.GetError(errcode);
            }
        }

        public Program(Context context, Device[] devices, byte[][] binaries)
            : this()
        {
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
                Handle = CL.CreateProgramWithBinary(context.Handle,
                    (uint)devices.Length, device_list, lengths_list, binary_list, binary_status, &errcode);

                for (int i = 0; i < binaries.Length; ++i)
                {
                    var handel = GCHandle.FromIntPtr(new IntPtr(binary_list[i]));
                    handel.Free();
                }

                List<int> errors = new List<int>(binaries.Length);

                for (int i = 0; i < binaries.Length; ++i)
                {
                    if (binary_status[i] != CL.SUCCESS)
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

                CLHelper.GetError(errcode);
            }
        }

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
                var device_list = stackalloc IntPtr[devices.Length];

                for (int i = 0; i < devices.Length; ++i)
                {
                    device_list[i] = devices[i].Handle;
                }

                var byte_count = Encoding.ASCII.GetByteCount(options);
                byte* bytes = stackalloc byte[byte_count];

                fixed (char* options_ptr = options)
                {
                    Encoding.ASCII.GetBytes(options_ptr, options.Length, bytes, byte_count);
                }

                var function_ptr = IntPtr.Zero;
                var data_ptr = IntPtr.Zero;

                if (notify != null)
                {
                    var data = Tuple.Create(notify, user_data);
                    data_ptr = GCHandle.ToIntPtr(GCHandle.Alloc(data));

                    function_ptr = Marshal.GetFunctionPointerForDelegate(new Action<IntPtr, IntPtr>(Callback));
                }

                int errcode = 0;
                CL.BuildProgram(Handle, (uint)devices.Length, device_list, bytes, function_ptr, data_ptr.ToPointer());
                CLHelper.GetError(errcode);
            }
        }

        public void RetainProgram()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.RetainProgram(Handle));
        }

        public void ReleaseProgram()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.ReleaseProgram(Handle));
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
                    CLHelper.GetError(CL.GetProgramInfo(
                        Handle, CL.PROGRAM_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public Context Context
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    CLHelper.GetError(CL.GetProgramInfo(
                        Handle, CL.PROGRAM_CONTEXT, param_value_size, &value, null));
                    return new Context(value);
                }
            }
        }

        public string Source
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetProgramInfo(
                        Handle, CL.PROGRAM_SOURCE, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetProgramInfo(
                        Handle, CL.PROGRAM_SOURCE, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public long KernelCount
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    CLHelper.GetError(CL.GetProgramInfo(
                        Handle, CL.PROGRAM_CONTEXT, param_value_size, &value, null));
                    return (long)value.ToUInt64();
                }
            }
        }

        public string[] KernelNames
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetProgramInfo(
                        Handle, CL.PROGRAM_KERNEL_NAMES, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetProgramInfo(
                        Handle, CL.PROGRAM_KERNEL_NAMES, param_value_size_ret, data_ptr, null));

                    var str = Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                    return str.Split(';');
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
            if (obj is Program)
            {
                return Equals((Program)obj);
            }
            return false;
        }

        public bool Equals(Program other)
        {
            CLHelper.ThrowNullException(Handle);
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
            CLHelper.ThrowNullException(Handle);
            return Handle.ToString();
        }
    }
}
