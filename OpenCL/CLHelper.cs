using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    internal static class ClHelper
    {
        internal static void GetError(int error)
        {
            switch(error)
            {
                case Cl.SUCCESS: return; // throw new OpenCLException("SUCCESS");
                case Cl.DEVICE_NOT_FOUND: throw new OpenCLException("DEVICE_NOT_FOUND");
                case Cl.DEVICE_NOT_AVAILABLE: throw new OpenCLException("DEVICE_NOT_AVAILABLE");
                case Cl.COMPILER_NOT_AVAILABLE: throw new OpenCLException("COMPILER_NOT_AVAILABLE");
                case Cl.MEM_OBJECT_ALLOCATION_FAILURE: throw new OpenCLException("MEM_OBJECT_ALLOCATION_FAILURE");
                case Cl.OUT_OF_RESOURCES: throw new OpenCLException("OUT_OF_RESOURCES");
                case Cl.OUT_OF_HOST_MEMORY: throw new OpenCLException("OUT_OF_HOST_MEMORY");
                case Cl.PROFILING_INFO_NOT_AVAILABLE: throw new OpenCLException("PROFILING_INFO_NOT_AVAILABLE");
                case Cl.MEM_COPY_OVERLAP: throw new OpenCLException("MEM_COPY_OVERLAP");
                case Cl.IMAGE_FORMAT_MISMATCH: throw new OpenCLException("IMAGE_FORMAT_MISMATCH");
                case Cl.IMAGE_FORMAT_NOT_SUPPORTED: throw new OpenCLException("IMAGE_FORMAT_NOT_SUPPORTED");
                case Cl.BUILD_PROGRAM_FAILURE: throw new OpenCLException("BUILD_PROGRAM_FAILURE");
                case Cl.MAP_FAILURE: throw new OpenCLException("MAP_FAILURE");
                case Cl.MISALIGNED_SUB_BUFFER_OFFSET: throw new OpenCLException("MISALIGNED_SUB_BUFFER_OFFSET");
                case Cl.EXEC_STATUS_ERROR_FOR_EVENTS_IN_WAIT_LIST: throw new OpenCLException("EXEC_STATUS_ERROR_FOR_EVENTS_IN_WAIT_LIST");
                case Cl.COMPILE_PROGRAM_FAILURE: throw new OpenCLException("COMPILE_PROGRAM_FAILURE");
                case Cl.LINKER_NOT_AVAILABLE: throw new OpenCLException("LINKER_NOT_AVAILABLE");
                case Cl.LINK_PROGRAM_FAILURE: throw new OpenCLException("LINK_PROGRAM_FAILURE");
                case Cl.DEVICE_PARTITION_FAILED: throw new OpenCLException("DEVICE_PARTITION_FAILED");
                case Cl.KERNEL_ARG_INFO_NOT_AVAILABLE: throw new OpenCLException("KERNEL_ARG_INFO_NOT_AVAILABLE");

                case Cl.INVALID_VALUE: throw new OpenCLException("INVALID_VALUE");
                case Cl.INVALID_DEVICE_TYPE: throw new OpenCLException("INVALID_DEVICE_TYPE");
                case Cl.INVALID_PLATFORM: throw new OpenCLException("INVALID_PLATFORM");
                case Cl.INVALID_DEVICE: throw new OpenCLException("INVALID_DEVICE");
                case Cl.INVALID_CONTEXT: throw new OpenCLException("INVALID_CONTEXT");
                case Cl.INVALID_QUEUE_PROPERTIES: throw new OpenCLException("INVALID_QUEUE_PROPERTIES");
                case Cl.INVALID_COMMAND_QUEUE: throw new OpenCLException("INVALID_COMMAND_QUEUE");
                case Cl.INVALID_HOST_PTR: throw new OpenCLException("INVALID_HOST_PTR");
                case Cl.INVALID_MEM_OBJECT: throw new OpenCLException("INVALID_MEM_OBJECT");
                case Cl.INVALID_IMAGE_FORMAT_DESCRIPTOR: throw new OpenCLException("INVALID_IMAGE_FORMAT_DESCRIPTOR");
                case Cl.INVALID_IMAGE_SIZE: throw new OpenCLException("INVALID_IMAGE_SIZE");
                case Cl.INVALID_SAMPLER: throw new OpenCLException("INVALID_SAMPLER");
                case Cl.INVALID_BINARY: throw new OpenCLException("INVALID_BINARY");
                case Cl.INVALID_BUILD_OPTIONS: throw new OpenCLException("INVALID_BUILD_OPTIONS");
                case Cl.INVALID_PROGRAM: throw new OpenCLException("INVALID_PROGRAM");
                case Cl.INVALID_PROGRAM_EXECUTABLE: throw new OpenCLException("INVALID_PROGRAM_EXECUTABLE");
                case Cl.INVALID_KERNEL_NAME: throw new OpenCLException("INVALID_KERNEL_NAME");
                case Cl.INVALID_KERNEL_DEFINITION: throw new OpenCLException("INVALID_KERNEL_DEFINITION");
                case Cl.INVALID_KERNEL: throw new OpenCLException("INVALID_KERNEL");
                case Cl.INVALID_ARG_INDEX: throw new OpenCLException("INVALID_ARG_INDEX");
                case Cl.INVALID_ARG_VALUE: throw new OpenCLException("INVALID_ARG_VALUE");
                case Cl.INVALID_ARG_SIZE: throw new OpenCLException("INVALID_ARG_SIZE");
                case Cl.INVALID_KERNEL_ARGS: throw new OpenCLException("INVALID_KERNEL_ARGS");
                case Cl.INVALID_WORK_DIMENSION: throw new OpenCLException("INVALID_WORK_DIMENSION");
                case Cl.INVALID_WORK_GROUP_SIZE: throw new OpenCLException("INVALID_WORK_GROUP_SIZE");
                case Cl.INVALID_WORK_ITEM_SIZE: throw new OpenCLException("INVALID_WORK_ITEM_SIZE");
                case Cl.INVALID_GLOBAL_OFFSET: throw new OpenCLException("INVALID_GLOBAL_OFFSET");
                case Cl.INVALID_EVENT_WAIT_LIST: throw new OpenCLException("INVALID_EVENT_WAIT_LIST");
                case Cl.INVALID_EVENT: throw new OpenCLException("INVALID_EVENT");
                case Cl.INVALID_OPERATION: throw new OpenCLException("INVALID_OPERATION");
                case Cl.INVALID_GL_OBJECT: throw new OpenCLException("INVALID_GL_OBJECT");
                case Cl.INVALID_BUFFER_SIZE: throw new OpenCLException("INVALID_BUFFER_SIZE");
                case Cl.INVALID_MIP_LEVEL: throw new OpenCLException("INVALID_MIP_LEVEL");
                case Cl.INVALID_GLOBAL_WORK_SIZE: throw new OpenCLException("INVALID_GLOBAL_WORK_SIZE");
                case Cl.INVALID_PROPERTY: throw new OpenCLException("INVALID_PROPERTY");
                case Cl.INVALID_IMAGE_DESCRIPTOR: throw new OpenCLException("INVALID_IMAGE_DESCRIPTOR");
                case Cl.INVALID_COMPILER_OPTIONS: throw new OpenCLException("INVALID_COMPILER_OPTIONS");
                case Cl.INVALID_LINKER_OPTIONS: throw new OpenCLException("INVALID_LINKER_OPTIONS");
                case Cl.INVALID_DEVICE_PARTITION_COUNT: throw new OpenCLException("INVALID_DEVICE_PARTITION_COUNT");
                default:
                    throw new OpenCLException(string.Format("Unknown error: {0}", error));
            }
        }

        internal static OpenCLException VersionException(string version, int major, int minor)
        {
            var clversion = ClHelper.ParseCLVersion(version);
            return new OpenCLException(string.Format(
                "This method requires OpenCL {0}.{1}, currently running OpenCL {2}.{3}.",
                major, minor, clversion.Major, clversion.Minor));
        }

        internal static OpenCLException VersionException(int major, int minor)
        {
            return new OpenCLException(string.Format(
                "This method requires OpenCL {0}.{1}.",
                major, minor));
        }

        internal static Version ParseCLVersion(string version)
        {
            int start = version.IndexOf(' ');
            int end = version.IndexOf(' ', start + 1);

            return new Version(version.Substring(start, end - start));
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
