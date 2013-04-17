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
        Default = Cl.DEVICE_TYPE_DEFAULT,
        Cpu = Cl.DEVICE_TYPE_CPU,
        Gpu = Cl.DEVICE_TYPE_GPU,
        Accelerator = Cl.DEVICE_TYPE_ACCELERATOR,
        Custom = Cl.DEVICE_TYPE_CUSTOM,
        All = Cl.DEVICE_TYPE_ALL,
    }

    public enum ChannelOrder
    {
        R = Cl.R,
        A = Cl.A,
        RG = Cl.RG,
        RA = Cl.RA,
        RGB = Cl.RGB,
        RGBA = Cl.RGBA,
        BGRA = Cl.BGRA,
        ARGB = Cl.ARGB,
        Intensity = Cl.INTENSITY,
        Luminance = Cl.LUMINANCE,
        Rx = Cl.Rx,
        RGx = Cl.RGx,
        RGBx = Cl.RGBx,
        Depth = Cl.DEPTH,
        DepthStencil = Cl.DEPTH_STENCIL,
    }
    

    public enum ChannelType
    {
        SNormInt8 = Cl.SNORM_INT8,
        SNormInt16 = Cl.SNORM_INT16,
        UNormInt8 = Cl.UNORM_INT8,
        UNormInt16 = Cl.UNORM_INT16,
        UNormShort565 = Cl.UNORM_SHORT_565,
        UNormShort555 = Cl.UNORM_SHORT_555,
        UNormInt101010 = Cl.UNORM_INT_101010,
        SignedInt8 = Cl.SIGNED_INT8,
        SignedInt16 = Cl.SIGNED_INT16,
        SignedInt32 = Cl.SIGNED_INT32,
        UnsignedInt8 = Cl.UNSIGNED_INT8,
        UnsignedInt16 = Cl.UNSIGNED_INT16,
        UnsignedInt32 = Cl.UNSIGNED_INT32,
        HalfFloat = Cl.HALF_FLOAT,
        Float = Cl.FLOAT,
        UNormInt24 = Cl.UNORM_INT24,
    }

    public enum AffinityDomain
    {
        /// <summary>
        /// Non-Uniform Memory Access
        /// </summary>
        NUMA = Cl.DEVICE_AFFINITY_DOMAIN_NUMA,
        L1Cache = Cl.DEVICE_AFFINITY_DOMAIN_L1_CACHE,
        L2Cache = Cl.DEVICE_AFFINITY_DOMAIN_L2_CACHE,
        L3Cache = Cl.DEVICE_AFFINITY_DOMAIN_L3_CACHE,
        L4Cache = Cl.DEVICE_AFFINITY_DOMAIN_L4_CACHE,
        NextPartitionable = Cl.DEVICE_AFFINITY_DOMAIN_NEXT_PARTITIONABLE,
    }

    [Flags]
    public enum MapFlags
    {
        Read = Cl.MAP_READ,
        Write = Cl.MAP_WRITE,
        WriteInvalidateRegion = Cl.MAP_WRITE_INVALIDATE_REGION,
    }

    [Flags]
    public enum MemoryMigrationFlags
    {
        None = 0,
        ObjectHost = Cl.MIGRATE_MEM_OBJECT_HOST,
        ObjectContentUndefined = Cl.MIGRATE_MEM_OBJECT_CONTENT_UNDEFINED,
    }

    public enum AddressingMode
    {
        None = Cl.ADDRESS_NONE,
        Clamp = Cl.ADDRESS_CLAMP,
        ClampToEdge = Cl.ADDRESS_CLAMP_TO_EDGE,
        Repeat = Cl.ADDRESS_REPEAT,
        MirroredRepeat = Cl.ADDRESS_MIRRORED_REPEAT,
    }

    public enum FilterMode
    {
        Nearest = Cl.FILTER_NEAREST,
        Linear = Cl.FILTER_LINEAR,
    }

    [Flags]
    public enum CommandQueueProperties
    {
        None = 0,
        OutOfOrderExecModeEnable = Cl.QUEUE_OUT_OF_ORDER_EXEC_MODE_ENABLE,
        ProfilingEnable = Cl.QUEUE_PROFILING_ENABLE,
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
        ReadWrite = Cl.MEM_READ_WRITE,
        WriteOnly = Cl.MEM_WRITE_ONLY,
        ReadOnly = Cl.MEM_READ_ONLY,
        UseHostPtr = Cl.MEM_USE_HOST_PTR,
        AllocHostPtr = Cl.MEM_ALLOC_HOST_PTR,
        CopyHostPtr = Cl.MEM_COPY_HOST_PTR,
        HostWriteOnly = Cl.MEM_HOST_WRITE_ONLY,
        HostReadOnly = Cl.MEM_HOST_READ_ONLY,
        HostNoAccess = Cl.MEM_HOST_NO_ACCESS,
    }

    public enum ImageType
    {
        Image1D = Cl.MEM_OBJECT_IMAGE1D,
        Image1DArray = Cl.MEM_OBJECT_IMAGE1D_ARRAY,
        Image1DBuffer = Cl.MEM_OBJECT_IMAGE1D_BUFFER,
        Image2D = Cl.MEM_OBJECT_IMAGE2D,
        Image2DArray = Cl.MEM_OBJECT_IMAGE2D_ARRAY,
        Image3D = Cl.MEM_OBJECT_IMAGE3D,
    }

    public enum MemoryObjectType
    {
        Buffer = Cl.MEM_OBJECT_BUFFER,
        Image1D = Cl.MEM_OBJECT_IMAGE1D,
        Image1DArray = Cl.MEM_OBJECT_IMAGE1D_ARRAY,
        Image1DBuffer = Cl.MEM_OBJECT_IMAGE1D_BUFFER,
        Image2D = Cl.MEM_OBJECT_IMAGE2D,
        Image2DArray = Cl.MEM_OBJECT_IMAGE2D_ARRAY,
        Image3D = Cl.MEM_OBJECT_IMAGE3D,
    }

    public enum CommandExecutionStatus
    {
        Complete = Cl.COMPLETE,
        Running = Cl.RUNNING,
        Submitted = Cl.SUBMITTED,
        Queued = Cl.QUEUED,
    }

    public enum BuildStatus
    {
        None = Cl.BUILD_NONE,
        Error = Cl.BUILD_ERROR,
        Success = Cl.BUILD_SUCCESS,
        InProgress = Cl.BUILD_IN_PROGRESS,
    }

    public enum BinaryType
    {
        None = Cl.PROGRAM_BINARY_TYPE_NONE,
        CompiledObject = Cl.PROGRAM_BINARY_TYPE_COMPILED_OBJECT,
        Library = Cl.PROGRAM_BINARY_TYPE_LIBRARY,
        Executable = Cl.PROGRAM_BINARY_TYPE_EXECUTABLE,
    }
    
    [Flags]
    public enum FloatingPointCapability
    {
        Denorm = Cl.FP_DENORM,
        InfinityNaN = Cl.FP_INF_NAN, 
        RoundToNearest = Cl.FP_ROUND_TO_NEAREST,
        RoundToZero = Cl.FP_ROUND_TO_ZERO,
        RoundToInfinity = Cl.FP_ROUND_TO_INF,
        FusedMultiplyAdd = Cl.FP_FMA,
        Software = Cl.FP_SOFT_FLOAT,
        CorrectlyRoundedDivideSqrt = Cl.FP_CORRECTLY_ROUNDED_DIVIDE_SQRT,
    }

    [Flags]
    public enum ExecutionCapabilities
    {
        ExecuteKernel = Cl.EXEC_KERNEL,
        ExecuteNativeKernel = Cl.EXEC_NATIVE_KERNEL,
    }

    public enum MemoryCacheType
    {
        ReadOnly = Cl.READ_ONLY_CACHE,
        ReadWrite = Cl.READ_WRITE_CACHE,
    }
}