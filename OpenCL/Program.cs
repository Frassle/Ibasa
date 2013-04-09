using System;
using System.Collections.Generic;
using System.Linq;
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
        {
            unsafe
            {
                var bytes = Encoding.UTF8.GetByteCount(source);
                byte* strings = stackalloc byte[bytes];

                fixed (char* source_ptr = source)
                {
                    Encoding.ASCII.GetBytes(source_ptr, source.Length, strings, bytes);
                }

                UIntPtr length = new UIntPtr((uint)bytes);

                int errcode = 0;
                OpenCL.clCreateProgramWithSource(context.Handle, 1, &strings, &length, &errcode);
                OpenCLHelper.GetError(errcode);
            }
        }


        public void BuildProgram(Device[] devices, string options)
        {
            unsafe
            {
                var device_list = stackalloc IntPtr[devices.Length];

                for (int i = 0; i < devices.Length; ++i)
                {
                    device_list[i] = devices[i].Handle;
                }

                var byte_count = Encoding.UTF8.GetByteCount(options);
                byte* bytes = stackalloc byte[byte_count];

                fixed (char* options_ptr = options)
                {
                    Encoding.ASCII.GetBytes(options_ptr, options.Length, bytes, byte_count);
                }

                int errcode = 0;
                OpenCL.clBuildProgram(Handle, (uint)devices.Length, device_list, bytes, IntPtr.Zero, null);
                OpenCLHelper.GetError(errcode);
            }
        }

        public void RetainProgram()
        {
            OpenCLHelper.ThrowNullException(Handle);
            OpenCLHelper.GetError(OpenCL.clRetainProgram(Handle));
        }

        public void ReleaseProgram()
        {
            OpenCLHelper.ThrowNullException(Handle);
            OpenCLHelper.GetError(OpenCL.clReleaseProgram(Handle));
        }

        public override int GetHashCode()
        {
            OpenCLHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            OpenCLHelper.ThrowNullException(Handle);
            if (obj is Program)
            {
                return Equals((Program)obj);
            }
            return false;
        }

        public bool Equals(Program other)
        {
            OpenCLHelper.ThrowNullException(Handle);
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
            OpenCLHelper.ThrowNullException(Handle);
            return Handle.ToString();
        }
    }
}
