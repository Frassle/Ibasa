using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    internal static class OpenCLHelper
    {
        internal static void GetError(int error)
        {
            switch(error)
            {
                case 0: return; // throw new OpenCLException("CL_SUCCESS");
                case -1: throw new OpenCLException("CL_DEVICE_NOT_FOUND");
                case -2: throw new OpenCLException("CL_DEVICE_NOT_AVAILABLE");
                case -3: throw new OpenCLException("CL_COMPILER_NOT_AVAILABLE");
                case -4: throw new OpenCLException("CL_MEM_OBJECT_ALLOCATION_FAILURE");
                case -5: throw new OpenCLException("CL_OUT_OF_RESOURCES");
                case -6: throw new OpenCLException("CL_OUT_OF_HOST_MEMORY");
                case -7: throw new OpenCLException("CL_PROFILING_INFO_NOT_AVAILABLE");
                case -8: throw new OpenCLException("CL_MEM_COPY_OVERLAP");
                case -9: throw new OpenCLException("CL_IMAGE_FORMAT_MISMATCH");
                case -10: throw new OpenCLException("CL_IMAGE_FORMAT_NOT_SUPPORTED");
                case -11: throw new OpenCLException("CL_BUILD_PROGRAM_FAILURE");
                case -12: throw new OpenCLException("CL_MAP_FAILURE");
                case -13: throw new OpenCLException("CL_MISALIGNED_SUB_BUFFER_OFFSET");
                case -14: throw new OpenCLException("CL_EXEC_STATUS_ERROR_FOR_EVENTS_IN_WAIT_LIST");
                case -15: throw new OpenCLException("CL_COMPILE_PROGRAM_FAILURE");
                case -16: throw new OpenCLException("CL_LINKER_NOT_AVAILABLE");
                case -17: throw new OpenCLException("CL_LINK_PROGRAM_FAILURE");
                case -18: throw new OpenCLException("CL_DEVICE_PARTITION_FAILED");
                case -19: throw new OpenCLException("CL_KERNEL_ARG_INFO_NOT_AVAILABLE");

                case -30: throw new OpenCLException("CL_INVALID_VALUE");
                case -31: throw new OpenCLException("CL_INVALID_DEVICE_TYPE");
                case -32: throw new OpenCLException("CL_INVALID_PLATFORM");
                case -33: throw new OpenCLException("CL_INVALID_DEVICE");
                case -34: throw new OpenCLException("CL_INVALID_CONTEXT");
                case -35: throw new OpenCLException("CL_INVALID_QUEUE_PROPERTIES");
                case -36: throw new OpenCLException("CL_INVALID_COMMAND_QUEUE");
                case -37: throw new OpenCLException("CL_INVALID_HOST_PTR");
                case -38: throw new OpenCLException("CL_INVALID_MEM_OBJECT");
                case -39: throw new OpenCLException("CL_INVALID_IMAGE_FORMAT_DESCRIPTOR");
                case -40: throw new OpenCLException("CL_INVALID_IMAGE_SIZE");
                case -41: throw new OpenCLException("CL_INVALID_SAMPLER");
                case -42: throw new OpenCLException("CL_INVALID_BINARY");
                case -43: throw new OpenCLException("CL_INVALID_BUILD_OPTIONS");
                case -44: throw new OpenCLException("CL_INVALID_PROGRAM");
                case -45: throw new OpenCLException("CL_INVALID_PROGRAM_EXECUTABLE");
                case -46: throw new OpenCLException("CL_INVALID_KERNEL_NAME");
                case -47: throw new OpenCLException("CL_INVALID_KERNEL_DEFINITION");
                case -48: throw new OpenCLException("CL_INVALID_KERNEL");
                case -49: throw new OpenCLException("CL_INVALID_ARG_INDEX");
                case -50: throw new OpenCLException("CL_INVALID_ARG_VALUE");
                case -51: throw new OpenCLException("CL_INVALID_ARG_SIZE");
                case -52: throw new OpenCLException("CL_INVALID_KERNEL_ARGS");
                case -53: throw new OpenCLException("CL_INVALID_WORK_DIMENSION");
                case -54: throw new OpenCLException("CL_INVALID_WORK_GROUP_SIZE");
                case -55: throw new OpenCLException("CL_INVALID_WORK_ITEM_SIZE");
                case -56: throw new OpenCLException("CL_INVALID_GLOBAL_OFFSET");
                case -57: throw new OpenCLException("CL_INVALID_EVENT_WAIT_LIST");
                case -58: throw new OpenCLException("CL_INVALID_EVENT");
                case -59: throw new OpenCLException("CL_INVALID_OPERATION");
                case -60: throw new OpenCLException("CL_INVALID_GL_OBJECT");
                case -61: throw new OpenCLException("CL_INVALID_BUFFER_SIZE");
                case -62: throw new OpenCLException("CL_INVALID_MIP_LEVEL");
                case -63: throw new OpenCLException("CL_INVALID_GLOBAL_WORK_SIZE");
                case -64: throw new OpenCLException("CL_INVALID_PROPERTY");
                case -65: throw new OpenCLException("CL_INVALID_IMAGE_DESCRIPTOR");
                case -66: throw new OpenCLException("CL_INVALID_COMPILER_OPTIONS");
                case -67: throw new OpenCLException("CL_INVALID_LINKER_OPTIONS");
                case -68: throw new OpenCLException("CL_INVALID_DEVICE_PARTITION_COUNT");
            }

        }

        internal static void ThrowNullException(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }
        }
    }
}
