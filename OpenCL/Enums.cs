using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    [Flags]
    public enum DeviceType
    {
        Default = CL.DEVICE_TYPE_DEFAULT,
        Cpu = CL.DEVICE_TYPE_CPU,
        Gpu = CL.DEVICE_TYPE_GPU,
        Accelerator = CL.DEVICE_TYPE_ACCELERATOR,
        Custom = CL.DEVICE_TYPE_CUSTOM,
        All = CL.DEVICE_TYPE_ALL,
    }

    [Flags]
    public enum CommandQueueProperties
    {
        None = 0,
        OutOfOrderExecModeEnable = CL.QUEUE_OUT_OF_ORDER_EXEC_MODE_ENABLE,
        ProfilingEnable = CL.QUEUE_PROFILING_ENABLE,
    }

    /// <summary>
    /// A bit-field that is used to specify allocation and usage information
    /// such as the memory arena that should be used to allocate the buffer 
    /// object and how it will be used. The following table describes the 
    /// possible values for flags. If value specified for flags is 0, the 
    /// default is used which is ReadWrite.
    /// </summary>
    [Flags]
    public enum MemoryFlags
    {
        /// <summary>
        /// This flag specifies that the memory object will be read and 
        /// written by a kernel. This is the default.
        /// </summary>
        ReadWrite = CL.MEM_READ_WRITE,
        WriteOnly = CL.MEM_WRITE_ONLY,
        ReadOnly = CL.MEM_READ_ONLY,
        UseHostPtr = CL.MEM_USE_HOST_PTR,
        AllocHostPtr = CL.MEM_ALLOC_HOST_PTR,
        CopyHostPtr = CL.MEM_COPY_HOST_PTR,
        HostWriteOnly = CL.MEM_HOST_WRITE_ONLY,
        HostReadOnly = CL.MEM_HOST_READ_ONLY,
        HostNoAccess = CL.MEM_HOST_NO_ACCESS,
    }

    public enum MemoryObjectType
    {
        Buffer = CL.MEM_OBJECT_BUFFER,
        Image1D = CL.MEM_OBJECT_IMAGE1D,
        Image2D = CL.MEM_OBJECT_IMAGE2D,
        Image3D = CL.MEM_OBJECT_IMAGE3D,
    }

    public enum CommandExecutionStatus
    {
        Complete = CL.COMPLETE,
        Running = CL.RUNNING,
        Submitted = CL.SUBMITTED,
        Queued = CL.QUEUED,
    }

    public enum BuildStatus
    {
        None = CL.BUILD_NONE,
        Error = CL.BUILD_ERROR,
        Success = CL.BUILD_SUCCESS,
        InProgress = CL.BUILD_IN_PROGRESS,
    }

    public enum BinaryType
    {
        None = CL.PROGRAM_BINARY_TYPE_NONE,
        CompiledObject = CL.PROGRAM_BINARY_TYPE_COMPILED_OBJECT,
        Library = CL.PROGRAM_BINARY_TYPE_LIBRARY,
        Executable = CL.PROGRAM_BINARY_TYPE_EXECUTABLE,
    }
    
    [Flags]
    public enum FloatingPointCapability
    {
        Denorm = CL.FP_DENORM,
        InfinityNaN = CL.FP_INF_NAN, 
        RoundToNearest = CL.FP_ROUND_TO_NEAREST,
        RoundToZero = CL.FP_ROUND_TO_ZERO,
        RoundToInfinity = CL.FP_ROUND_TO_INF,
        FusedMultiplyAdd = CL.FP_FMA,
        Software = CL.FP_SOFT_FLOAT,
        CorrectlyRoundedDivideSqrt = CL.FP_CORRECTLY_ROUNDED_DIVIDE_SQRT,
    }

    [Flags]
    public enum ExecutionCapabilities
    {
        ExecuteKernel = CL.EXEC_KERNEL,
        ExecuteNativeKernel = CL.EXEC_NATIVE_KERNEL,
    }

    public enum MemoryCacheType
    {
        ReadOnly = CL.READ_ONLY_CACHE,
        ReadWrite = CL.READ_WRITE_CACHE,
    }
}