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
        public IntPtr Handle { get; private set; }

        public Kernel(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Kernel(Program program, string kernel_name)
            : this()
        {
            unsafe
            {
                int error;
                IntPtr str = Marshal.StringToHGlobalAnsi(kernel_name);
                Handle = CL.CreateKernel(program.Handle, (byte*)str.ToPointer(), &error);
                Marshal.FreeHGlobal(str);
                CLHelper.GetError(error);
            }
        }

        public static Kernel[] CreateKernelsInProgram(Program program)
        {
            unsafe
            {
                uint num_kernels = 0;
                CLHelper.GetError(CL.CreateKernelsInProgram(program.Handle, 0, null, &num_kernels));

                IntPtr* kernel_ptrs = stackalloc IntPtr[(int)num_kernels];
                CLHelper.GetError(CL.CreateKernelsInProgram(program.Handle, num_kernels, kernel_ptrs, null));

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
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.RetainKernel(Handle));
        }

        public void Release()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.ReleaseKernel(Handle));
        }
        public override int GetHashCode()
        {
            CLHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            CLHelper.ThrowNullException(Handle);
            if (obj is Kernel)
            {
                return Equals((Kernel)obj);
            }
            return false;
        }

        public bool Equals(Kernel other)
        {
            CLHelper.ThrowNullException(Handle);
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
            CLHelper.ThrowNullException(Handle);
            return Handle.ToString();
        }
    }
}
