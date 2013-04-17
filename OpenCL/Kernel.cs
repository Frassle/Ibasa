using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public struct Kernel : IEquatable<Kernel>
    {
        public static readonly Kernel Null = new Kernel();

        public IntPtr Handle { get; private set; }

        public Kernel(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Kernel(Program program, string kernel_name)
            : this()
        {
            if (program == Program.Null)
                throw new ArgumentNullException("context");

            unsafe
            {
                int error;
                IntPtr str = Marshal.StringToHGlobalAnsi(kernel_name);
                Handle = Cl.CreateKernel(program.Handle, (byte*)str.ToPointer(), &error);
                Marshal.FreeHGlobal(str);
                ClHelper.GetError(error);
            }
        }

        public static Kernel[] CreateKernelsInProgram(Program program)
        {
            if (program == Program.Null)
                throw new ArgumentNullException("context");

            unsafe
            {
                uint num_kernels = 0;
                ClHelper.GetError(Cl.CreateKernelsInProgram(program.Handle, 0, null, &num_kernels));

                IntPtr* kernel_ptrs = stackalloc IntPtr[(int)num_kernels];
                ClHelper.GetError(Cl.CreateKernelsInProgram(program.Handle, num_kernels, kernel_ptrs, null));

                Kernel[] kernels = new Kernel[(int)num_kernels];
                for (int i = 0; i < kernels.Length; ++i)
                {
                    kernels[i] = new Kernel(kernel_ptrs[i]);
                }

                return kernels;
            }
        }

        public void Retain()
        {
            ClHelper.ThrowNullException(Handle);
            ClHelper.GetError(Cl.RetainKernel(Handle));
        }

        public void Release()
        {
            ClHelper.ThrowNullException(Handle);
            int error = Cl.ReleaseKernel(Handle);
            Handle = IntPtr.Zero;
            ClHelper.GetError(error);
        }

        public void SetArgument(int index, Buffer buffer)
        {
            ClHelper.ThrowNullException(Handle);
            if (buffer == Buffer.Null)
                throw new ArgumentNullException("buffer");

            unsafe
            {
                IntPtr value = buffer.Handle;
                ClHelper.GetError(Cl.SetKernelArg(Handle,
                    (uint)index, (UIntPtr)IntPtr.Size, &value));
            }
        }

        public void SetArgument(int index, int value)
        {
            ClHelper.ThrowNullException(Handle);

            unsafe
            {
                ClHelper.GetError(Cl.SetKernelArg(Handle,
                    (uint)index, (UIntPtr)sizeof(int), &value));
            }
        }

        public void SetArgument(int index, long value)
        {
            ClHelper.ThrowNullException(Handle);

            unsafe
            {
                ClHelper.GetError(Cl.SetKernelArg(Handle,
                    (uint)index, (UIntPtr)sizeof(long), &value));
            }
        }

        public void SetArgument(int index, IntPtr value)
        {
            ClHelper.ThrowNullException(Handle);

            unsafe
            {
                ClHelper.GetError(Cl.SetKernelArg(Handle,
                    (uint)index, (UIntPtr)IntPtr.Size, &value));
            }
        }

        public string FunctionName
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    ClHelper.GetError(Cl.GetKernelInfo(
                        Handle, Cl.KERNEL_FUNCTION_NAME, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    ClHelper.GetError(Cl.GetKernelInfo(
                        Handle, Cl.KERNEL_FUNCTION_NAME, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public long ArgumentCount
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetKernelInfo(
                        Handle, Cl.KERNEL_NUM_ARGS, param_value_size, &value, null));
                    return value;
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
                    ClHelper.GetError(Cl.GetKernelInfo(
                        Handle, Cl.KERNEL_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        Context Context
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    ClHelper.GetError(Cl.GetKernelInfo(
                        Handle, Cl.KERNEL_CONTEXT, param_value_size, &value, null));
                    return new Context(value);
                }
            }
        }

        Program Program
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    ClHelper.GetError(Cl.GetKernelInfo(
                        Handle, Cl.KERNEL_PROGRAM, param_value_size, &value, null));
                    return new Program(value);
                }
            }
        }

        public string Attributes
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    ClHelper.GetError(Cl.GetKernelInfo(
                        Handle, Cl.KERNEL_ATTRIBUTES, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    ClHelper.GetError(Cl.GetKernelInfo(
                        Handle, Cl.KERNEL_ATTRIBUTES, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public struct WorkGroupInfo
        {
            IntPtr Kernel;
            IntPtr Device;

            internal WorkGroupInfo(IntPtr kernel, IntPtr device)
            {
                Kernel = kernel;
                Device = device;
            }

            public ulong WorkGroupSize
            {
                get
                {
                    unsafe
                    {
                        UIntPtr value;
                        UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                        ClHelper.GetError(Cl.GetKernelWorkGroupInfo(Kernel, Device,
                            Cl.KERNEL_WORK_GROUP_SIZE, param_value_size, &value, null));
                        return value.ToUInt64();
                    }
                }
            }

            public ulong[] CompileWorkGroupSize
            {
                get
                {
                    unsafe
                    {
                        UIntPtr* value = stackalloc UIntPtr[3];
                        UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size * 3);
                        ClHelper.GetError(Cl.GetKernelWorkGroupInfo(Kernel, Device,
                            Cl.KERNEL_COMPILE_WORK_GROUP_SIZE, param_value_size, value, null));

                        ulong[] result = new ulong[3];
                        result[0] = value[0].ToUInt64();
                        result[1] = value[1].ToUInt64();
                        result[2] = value[2].ToUInt64();
                        return result;
                    }
                }
            }

            public ulong LocalMemorySize
            {
                get
                {
                    unsafe
                    {
                        ulong value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                        ClHelper.GetError(Cl.GetKernelWorkGroupInfo(Kernel, Device,
                            Cl.KERNEL_LOCAL_MEM_SIZE, param_value_size, &value, null));
                        return value;
                    }
                }
            }

            public ulong PreferredWorkGroupSizeMultiple
            {
                get
                {
                    unsafe
                    {
                        UIntPtr value;
                        UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                        ClHelper.GetError(Cl.GetKernelWorkGroupInfo(Kernel, Device,
                            Cl.KERNEL_PREFERRED_WORK_GROUP_SIZE_MULTIPLE, param_value_size, &value, null));
                        return value.ToUInt64();
                    }
                }
            }

            public ulong PrivateMemSize
            {
                get
                {
                    unsafe
                    {
                        ulong value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                        ClHelper.GetError(Cl.GetKernelWorkGroupInfo(Kernel, Device,
                            Cl.KERNEL_PRIVATE_MEM_SIZE, param_value_size, &value, null));
                        return value;
                    }
                }
            }
        }

        public WorkGroupInfo GetWorkGroupInfo(Device device)
        {
            ClHelper.ThrowNullException(Handle);
            if (device == Device.Null)
                throw new ArgumentNullException("device");

            return new WorkGroupInfo(Handle, device.Handle);
        }


        public override int GetHashCode()
        {
            ClHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ClHelper.ThrowNullException(Handle);
            if (obj is Kernel)
            {
                return Equals((Kernel)obj);
            }
            return false;
        }

        public bool Equals(Kernel other)
        {
            ClHelper.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Kernel left, Kernel right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Kernel left, Kernel right)
        {
            return left.Handle != right.Handle;
        }

        public override string ToString()
        {
            ClHelper.ThrowNullException(Handle);
            return string.Format("Kernel: {0}", Handle.ToString());
        }
    }
}
