using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    internal static class CLHelper
    {
        internal static void GetError(int error)
        {
            switch(error)
            {
                case CL.SUCCESS: return; // throw new OpenCLException("SUCCESS");
                case CL.DEVICE_NOT_FOUND: throw new OpenCLException("DEVICE_NOT_FOUND");
                case CL.DEVICE_NOT_AVAILABLE: throw new OpenCLException("DEVICE_NOT_AVAILABLE");
                case CL.COMPILER_NOT_AVAILABLE: throw new OpenCLException("COMPILER_NOT_AVAILABLE");
                case CL.MEM_OBJECT_ALLOCATION_FAILURE: throw new OpenCLException("MEM_OBJECT_ALLOCATION_FAILURE");
                case CL.OUT_OF_RESOURCES: throw new OpenCLException("OUT_OF_RESOURCES");
                case CL.OUT_OF_HOST_MEMORY: throw new OpenCLException("OUT_OF_HOST_MEMORY");
                case CL.PROFILING_INFO_NOT_AVAILABLE: throw new OpenCLException("PROFILING_INFO_NOT_AVAILABLE");
                case CL.MEM_COPY_OVERLAP: throw new OpenCLException("MEM_COPY_OVERLAP");
                case CL.IMAGE_FORMAT_MISMATCH: throw new OpenCLException("IMAGE_FORMAT_MISMATCH");
                case CL.IMAGE_FORMAT_NOT_SUPPORTED: throw new OpenCLException("IMAGE_FORMAT_NOT_SUPPORTED");
                case CL.BUILD_PROGRAM_FAILURE: throw new OpenCLException("BUILD_PROGRAM_FAILURE");
                case CL.MAP_FAILURE: throw new OpenCLException("MAP_FAILURE");
                case CL.MISALIGNED_SUB_BUFFER_OFFSET: throw new OpenCLException("MISALIGNED_SUB_BUFFER_OFFSET");
                case CL.EXEC_STATUS_ERROR_FOR_EVENTS_IN_WAIT_LIST: throw new OpenCLException("EXEC_STATUS_ERROR_FOR_EVENTS_IN_WAIT_LIST");
                case CL.COMPILE_PROGRAM_FAILURE: throw new OpenCLException("COMPILE_PROGRAM_FAILURE");
                case CL.LINKER_NOT_AVAILABLE: throw new OpenCLException("LINKER_NOT_AVAILABLE");
                case CL.LINK_PROGRAM_FAILURE: throw new OpenCLException("LINK_PROGRAM_FAILURE");
                case CL.DEVICE_PARTITION_FAILED: throw new OpenCLException("DEVICE_PARTITION_FAILED");
                case CL.KERNEL_ARG_INFO_NOT_AVAILABLE: throw new OpenCLException("KERNEL_ARG_INFO_NOT_AVAILABLE");

                case CL.INVALID_VALUE: throw new OpenCLException("INVALID_VALUE");
                case CL.INVALID_DEVICE_TYPE: throw new OpenCLException("INVALID_DEVICE_TYPE");
                case CL.INVALID_PLATFORM: throw new OpenCLException("INVALID_PLATFORM");
                case CL.INVALID_DEVICE: throw new OpenCLException("INVALID_DEVICE");
                case CL.INVALID_CONTEXT: throw new OpenCLException("INVALID_CONTEXT");
                case CL.INVALID_QUEUE_PROPERTIES: throw new OpenCLException("INVALID_QUEUE_PROPERTIES");
                case CL.INVALID_COMMAND_QUEUE: throw new OpenCLException("INVALID_COMMAND_QUEUE");
                case CL.INVALID_HOST_PTR: throw new OpenCLException("INVALID_HOST_PTR");
                case CL.INVALID_MEM_OBJECT: throw new OpenCLException("INVALID_MEM_OBJECT");
                case CL.INVALID_IMAGE_FORMAT_DESCRIPTOR: throw new OpenCLException("INVALID_IMAGE_FORMAT_DESCRIPTOR");
                case CL.INVALID_IMAGE_SIZE: throw new OpenCLException("INVALID_IMAGE_SIZE");
                case CL.INVALID_SAMPLER: throw new OpenCLException("INVALID_SAMPLER");
                case CL.INVALID_BINARY: throw new OpenCLException("INVALID_BINARY");
                case CL.INVALID_BUILD_OPTIONS: throw new OpenCLException("INVALID_BUILD_OPTIONS");
                case CL.INVALID_PROGRAM: throw new OpenCLException("INVALID_PROGRAM");
                case CL.INVALID_PROGRAM_EXECUTABLE: throw new OpenCLException("INVALID_PROGRAM_EXECUTABLE");
                case CL.INVALID_KERNEL_NAME: throw new OpenCLException("INVALID_KERNEL_NAME");
                case CL.INVALID_KERNEL_DEFINITION: throw new OpenCLException("INVALID_KERNEL_DEFINITION");
                case CL.INVALID_KERNEL: throw new OpenCLException("INVALID_KERNEL");
                case CL.INVALID_ARG_INDEX: throw new OpenCLException("INVALID_ARG_INDEX");
                case CL.INVALID_ARG_VALUE: throw new OpenCLException("INVALID_ARG_VALUE");
                case CL.INVALID_ARG_SIZE: throw new OpenCLException("INVALID_ARG_SIZE");
                case CL.INVALID_KERNEL_ARGS: throw new OpenCLException("INVALID_KERNEL_ARGS");
                case CL.INVALID_WORK_DIMENSION: throw new OpenCLException("INVALID_WORK_DIMENSION");
                case CL.INVALID_WORK_GROUP_SIZE: throw new OpenCLException("INVALID_WORK_GROUP_SIZE");
                case CL.INVALID_WORK_ITEM_SIZE: throw new OpenCLException("INVALID_WORK_ITEM_SIZE");
                case CL.INVALID_GLOBAL_OFFSET: throw new OpenCLException("INVALID_GLOBAL_OFFSET");
                case CL.INVALID_EVENT_WAIT_LIST: throw new OpenCLException("INVALID_EVENT_WAIT_LIST");
                case CL.INVALID_EVENT: throw new OpenCLException("INVALID_EVENT");
                case CL.INVALID_OPERATION: throw new OpenCLException("INVALID_OPERATION");
                case CL.INVALID_GL_OBJECT: throw new OpenCLException("INVALID_GL_OBJECT");
                case CL.INVALID_BUFFER_SIZE: throw new OpenCLException("INVALID_BUFFER_SIZE");
                case CL.INVALID_MIP_LEVEL: throw new OpenCLException("INVALID_MIP_LEVEL");
                case CL.INVALID_GLOBAL_WORK_SIZE: throw new OpenCLException("INVALID_GLOBAL_WORK_SIZE");
                case CL.INVALID_PROPERTY: throw new OpenCLException("INVALID_PROPERTY");
                case CL.INVALID_IMAGE_DESCRIPTOR: throw new OpenCLException("INVALID_IMAGE_DESCRIPTOR");
                case CL.INVALID_COMPILER_OPTIONS: throw new OpenCLException("INVALID_COMPILER_OPTIONS");
                case CL.INVALID_LINKER_OPTIONS: throw new OpenCLException("INVALID_LINKER_OPTIONS");
                case CL.INVALID_DEVICE_PARTITION_COUNT: throw new OpenCLException("INVALID_DEVICE_PARTITION_COUNT");
                default:
                    throw new OpenCLException(string.Format("Unknown error: {0}", error));
            }
        }

        internal static bool CheckVersion(string version, int major, int minor)
        {
            int start = version.IndexOf(' ');
            int end = version.IndexOf(' ', start + 1);

            var v = new Version(version.Substring(start, end - start));

            return v.Major > major || (v.Major == major && v.Minor >= minor);
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
