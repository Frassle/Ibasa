using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public class PartitionProperties
    {
        public bool Equally { get; private set; }
        public bool ByCounts { get; private set; }
        public bool ByAffinityDomain { get; private set; }

        internal PartitionProperties(bool equally, bool byCounts, bool byAffinityDomain)
        {
            Equally = equally;
            ByCounts = byCounts;
            ByAffinityDomain = byAffinityDomain;
        }
    }

    public abstract class PartitionType
    {
    }

    public sealed class PartitionTypeEqually : PartitionType
    {
        public int Units { get; private set; }

        internal PartitionTypeEqually(int units)
        {
            Units = units;
        }
    }

    public sealed class PartitionTypeByCounts : PartitionType
    {
        public int[] Counts { get; private set; }

        internal PartitionTypeByCounts(int[] counts)
        {
            Counts = counts;
        }
    }

    public sealed class PartitionTypeByAffinityDomain : PartitionType
    {
        public AffinityDomain AffinityDomain { get; private set; }

        internal PartitionTypeByAffinityDomain(AffinityDomain affinityDomain)
        {
            AffinityDomain = affinityDomain;
        }
    }

    public struct Device : IEquatable<Device>
    {
        public static readonly Device Null = new Device();

        public IntPtr Handle { get; private set; }

        public Device(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public void Retain()
        {
            CLHelper.ThrowNullException(Handle);
            try
            {
                CLHelper.GetError(CL.RetainDevice(Handle));
            }
            catch (EntryPointNotFoundException)
            {
                throw CLHelper.VersionException(Version, 1, 2);
            }
        }

        public void Release()
        {
            CLHelper.ThrowNullException(Handle);
            try
            {
                CLHelper.GetError(CL.ReleaseDevice(Handle));
            }
            catch (EntryPointNotFoundException)
            {
                throw CLHelper.VersionException(Version, 1, 2);
            }
        }

        private unsafe Device[] CreateSubDevices(IntPtr* properties)
        {
            try
            {
                uint num_devices;
                CLHelper.GetError(CL.CreateSubDevices(Handle, properties, 0, null, &num_devices));

                IntPtr* device_ptrs = stackalloc IntPtr[(int)num_devices];

                CLHelper.GetError(CL.CreateSubDevices(Handle, properties, num_devices, device_ptrs, null));

                Device[] sub_devices = new Device[num_devices];
                for (uint i = 0; i < num_devices; ++i)
                {
                    sub_devices[i] = new Device(device_ptrs[i]);
                }

                return sub_devices;
            }
            catch (EntryPointNotFoundException)
            {
                throw CLHelper.VersionException(Version, 1, 2);
            }
        }

        public Device[] CreateSubDevicesEqually(int units)
        {
            CLHelper.ThrowNullException(Handle);

            unsafe
            {
                IntPtr* properties = stackalloc IntPtr[3];
                properties[0] = new IntPtr(CL.DEVICE_PARTITION_EQUALLY);
                properties[1] = new IntPtr(units);
                properties[2] = new IntPtr(0);

                return CreateSubDevices(properties);
            }
        }

        public Device[] CreateSubDevicesByCounts(params int[] counts)
        {
            CLHelper.ThrowNullException(Handle);

            unsafe
            {
                IntPtr* properties = stackalloc IntPtr[3 + counts.Length];
                properties[0] = new IntPtr(CL.DEVICE_PARTITION_BY_COUNTS);

                for (int i = 0; i < counts.Length; ++i)
                {
                    properties[i + 1] = new IntPtr(counts[i]);
                }
                properties[counts.Length + 1] = new IntPtr(CL.DEVICE_PARTITION_BY_COUNTS_LIST_END);
                properties[counts.Length + 2] = new IntPtr(0);

                return CreateSubDevices(properties);
            }
        }

        public Device[] CreateSubDevicesByAffinityDomain(AffinityDomain domain)
        {
            CLHelper.ThrowNullException(Handle);

            unsafe
            {
                IntPtr* properties = stackalloc IntPtr[3];
                properties[0] = new IntPtr(CL.DEVICE_PARTITION_BY_AFFINITY_DOMAIN);
                properties[1] = new IntPtr((int)domain);
                properties[2] = new IntPtr(0);

                return CreateSubDevices(properties);
            }
        }

        public uint AddressBits
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_ADDRESS_BITS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public bool Available
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_AVAILABLE, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public string[] BuiltInKernels
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr param_value_size_ret = UIntPtr.Zero;
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_BUILT_IN_KERNELS, UIntPtr.Zero, null, &param_value_size_ret));

                        byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_BUILT_IN_KERNELS, param_value_size_ret, data_ptr, null));

                        var str = Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                        return str.Split(';');
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public bool CompilerAvailable
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_COMPILER_AVAILABLE, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public FloatingPointCapability DoubleFloatingPointCapability
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_DOUBLE_FP_CONFIG, param_value_size, &value, null));
                    return (FloatingPointCapability)value;
                }
            }
        }
        
        public bool LittleEndian
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_ENDIAN_LITTLE, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public bool ErrorCorrectionSupport
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_ERROR_CORRECTION_SUPPORT, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public ExecutionCapabilities ExecutionCapabilities
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_EXECUTION_CAPABILITIES, param_value_size, &value, null));
                    return (ExecutionCapabilities)value;
                }
            }
        }

        public string[] Extensions
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_EXTENSIONS, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_EXTENSIONS, param_value_size_ret, data_ptr, null));

                    var str = Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                    return str.Split(' ');
                }
            }
        }

        public ulong GlobalMemoryCacheSize
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_GLOBAL_MEM_CACHE_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public MemoryCacheType GlobalMemoryCacheType
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_GLOBAL_MEM_CACHE_TYPE, param_value_size, &value, null));
                    return (MemoryCacheType)value;
                }
            }
        }

        public uint GlobalMemoryCachelineSize
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_GLOBAL_MEM_CACHELINE_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong GlobalMemorySize
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_GLOBAL_MEM_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public FloatingPointCapability HalfFloatingPointCapability
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_HALF_FP_CONFIG, param_value_size, &value, null));
                    return (FloatingPointCapability)value;
                }
            }
        }

        public bool HostUnifiedMemory
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_HOST_UNIFIED_MEMORY, param_value_size, &value, null));
                        return value != 0;
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public bool ImageSupport
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_IMAGE_SUPPORT, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public ulong Image2dMaxHeight
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_IMAGE2D_MAX_HEIGHT, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public ulong Image2dMaxWidth
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_IMAGE2D_MAX_WIDTH, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public ulong Image3dMaxDepth
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_IMAGE3D_MAX_DEPTH, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public ulong Image3dMaxHeight
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_IMAGE3D_MAX_HEIGHT, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public ulong Image3dMaxWidth
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_IMAGE3D_MAX_WIDTH, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public ulong ImageMaxBufferSize
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr value;
                        UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_IMAGE_MAX_BUFFER_SIZE, param_value_size, &value, null));
                        return value.ToUInt64();
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public ulong ImageMaxArraySize
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr value;
                        UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_IMAGE_MAX_ARRAY_SIZE, param_value_size, &value, null));
                        return value.ToUInt64();
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public bool LinkerAvailable
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_LINKER_AVAILABLE, param_value_size, &value, null));
                        return value != 0;
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public ulong LocalMemorySize
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_LOCAL_MEM_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public MemoryCacheType LocalMemoryType
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_LOCAL_MEM_TYPE, param_value_size, &value, null));
                    return (MemoryCacheType)value;
                }
            }
        }

        public uint MaxClockFrequency
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MAX_CLOCK_FREQUENCY, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public uint MaxComputeUnits
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MAX_COMPUTE_UNITS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public uint MaxConstantArgs
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MAX_CONSTANT_ARGS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong MaxConstantBufferSize
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MAX_CONSTANT_BUFFER_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong MaxMemoryAllocSize
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MAX_MEM_ALLOC_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong MaxParameterSize
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MAX_PARAMETER_SIZE, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public uint MaxReadImageArgs
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MAX_READ_IMAGE_ARGS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public uint MaxSamplers
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MAX_SAMPLERS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong MaxWorkGroupSize
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MAX_WORK_GROUP_SIZE, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public uint MaxWorkItemDimensions
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MAX_WORK_ITEM_DIMENSIONS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong[] MaxWorkItemSizes
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    int dim = (int)MaxWorkItemDimensions;
                    UIntPtr* sizes = stackalloc UIntPtr[dim];
                    UIntPtr param_value_size = new UIntPtr((uint)(UIntPtr.Size * dim));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MAX_WORK_ITEM_SIZES, param_value_size, sizes, null));

                    var value = new ulong[dim];
                    for (int i = 0; i < dim; ++i)
                    {
                        value[i] = sizes[i].ToUInt64();
                    }

                    return value;
                }
            }
        }

        public uint MaxWriteImageArgs
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MAX_WRITE_IMAGE_ARGS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public uint MemBaseAddrAlign
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MEM_BASE_ADDR_ALIGN, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public uint MinDataTypeAlignSize
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_MIN_DATA_TYPE_ALIGN_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public string Name
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_NAME, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_NAME, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public long NativeVectorWidthByte
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_NATIVE_VECTOR_WIDTH_CHAR, param_value_size, &value, null));
                        return value;
                    }
                }
                catch(OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public long NativeVectorWidthShort
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_NATIVE_VECTOR_WIDTH_SHORT, param_value_size, &value, null));
                        return value;
                    }
                }
                catch(OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public long NativeVectorWidthInt
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_NATIVE_VECTOR_WIDTH_INT, param_value_size, &value, null));
                        return value;
                    }
                }
                catch(OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public long NativeVectorWidthLong
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_NATIVE_VECTOR_WIDTH_LONG, param_value_size, &value, null));
                        return value;
                    }
                }
                catch(OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public long NativeVectorWidthSingle
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_NATIVE_VECTOR_WIDTH_FLOAT, param_value_size, &value, null));
                        return value;
                    }
                }
                catch(OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public long NativeVectorWidthDouble
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_NATIVE_VECTOR_WIDTH_DOUBLE, param_value_size, &value, null));
                        return value;
                    }
                }
                catch(OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public long NativeVectorWidthHalf
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_PREFERRED_VECTOR_WIDTH_HALF, param_value_size, &value, null));
                        return value;
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public string CVersion
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr param_value_size_ret = UIntPtr.Zero;
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_OPENCL_C_VERSION, UIntPtr.Zero, null, &param_value_size_ret));

                        byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_OPENCL_C_VERSION, param_value_size_ret, data_ptr, null));

                        return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 1);
                }
            }
        }
        
        public Device ParentDevice
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        IntPtr value;
                        UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_PARENT_DEVICE, param_value_size, &value, null));
                        return new Device(value);
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public long PartitionMaxSubDevices
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_PARTITION_MAX_SUB_DEVICES, param_value_size, &value, null));
                        return value;
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public PartitionProperties PartitionProperties
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr param_value_size = UIntPtr.Zero;
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_PARTITION_PROPERTIES, UIntPtr.Zero, null, &param_value_size));

                        IntPtr* properties = stackalloc IntPtr[(int)(param_value_size.ToUInt32() / IntPtr.Size)];

                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_PARTITION_PROPERTIES, param_value_size, properties, null));

                        bool equally = false;
                        bool byCounts = false;
                        bool byAffinityDomain = false;

                        while (*properties != IntPtr.Zero)
                        {
                            equally |= *properties == new IntPtr(CL.DEVICE_PARTITION_EQUALLY);
                            byCounts |= *properties == new IntPtr(CL.DEVICE_PARTITION_BY_COUNTS);
                            byAffinityDomain |= *properties == new IntPtr(CL.DEVICE_PARTITION_BY_AFFINITY_DOMAIN);
                        }

                        return new PartitionProperties(equally, byCounts, byAffinityDomain);
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public AffinityDomain AffinityDomain
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        ulong value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_PARTITION_AFFINITY_DOMAIN, param_value_size, &value, null));
                        return (AffinityDomain)value;
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public PartitionType PartitionType
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr param_value_size = UIntPtr.Zero;
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_PARTITION_TYPE, UIntPtr.Zero, null, &param_value_size));

                        IntPtr* properties = stackalloc IntPtr[(int)(param_value_size.ToUInt32() / IntPtr.Size)];

                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_PARTITION_TYPE, param_value_size, properties, null));

                        if (properties[0] == new IntPtr(CL.DEVICE_PARTITION_EQUALLY))
                        {
                            var units = properties[1].ToInt32();

                            return new PartitionTypeEqually(units);
                        }
                        else if (properties[0] == new IntPtr(CL.DEVICE_PARTITION_BY_COUNTS))
                        {
                            int count = 0;
                            for (int i = 1; properties[i] != IntPtr.Zero; ++i)
                            {
                                ++count;
                            }

                            var counts = new int[count];
                            for (int i = 0; i < count; ++i)
                            {
                                counts[i] = properties[i + 1].ToInt32();
                            }

                            return new PartitionTypeByCounts(counts);
                        }
                        else if (properties[0] == new IntPtr(CL.DEVICE_PARTITION_BY_AFFINITY_DOMAIN))
                        {
                            var domain = (AffinityDomain)properties[1].ToInt64();

                            return new PartitionTypeByAffinityDomain(domain);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public Platform Platform
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_PLATFORM, param_value_size, &value, null));
                    return new Platform(value);
                }
            }
        }

        public long PreferredVectorWidthByte
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_PREFERRED_VECTOR_WIDTH_CHAR, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long PreferredVectorWidthShort
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_PREFERRED_VECTOR_WIDTH_SHORT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long PreferredVectorWidthInt
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_PREFERRED_VECTOR_WIDTH_INT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long PreferredVectorWidthLong
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_PREFERRED_VECTOR_WIDTH_LONG, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long PreferredVectorWidthSingle
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_PREFERRED_VECTOR_WIDTH_FLOAT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long PreferredVectorWidthDouble
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_PREFERRED_VECTOR_WIDTH_DOUBLE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long PreferredVectorWidthHalf
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_PREFERRED_VECTOR_WIDTH_HALF, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong PrintfBufferSize
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr value;
                        UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_PRINTF_BUFFER_SIZE, param_value_size, &value, null));
                        return value.ToUInt64();
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public bool PreferredInteropUserSync
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_PREFERRED_INTEROP_USER_SYNC, param_value_size, &value, null));
                        return value != 0;
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public string Profile
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_PROFILE, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_PROFILE, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public ulong ProfilingTimerResolution
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_PROFILING_TIMER_RESOLUTION, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public CommandQueueProperties QueueProperties
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr((uint)sizeof(ulong));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_PROFILING_TIMER_RESOLUTION, param_value_size, &value, null));
                    return (CommandQueueProperties)value;
                }
            }
        }

        public long ReferenceCount
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        CLHelper.GetError(CL.GetDeviceInfo(
                            Handle, CL.DEVICE_REFERENCE_COUNT, param_value_size, &value, null));
                        return value;
                    }
                }
                catch (OpenCLException)
                {
                    throw CLHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public FloatingPointCapability SingleFloatingPointCapability
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_SINGLE_FP_CONFIG, param_value_size, &value, null));
                    return (FloatingPointCapability)value;
                }
            }
        }

        public DeviceType Type
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_TYPE, param_value_size, &value, null));
                    return (DeviceType)value;
                }
            }
        }

        public string Vendor
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_VENDOR, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_VENDOR, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public long VendorID
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_VENDOR_ID, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public string Version
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_VERSION, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DEVICE_VERSION, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public string DriverVersion
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DRIVER_VERSION, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    CLHelper.GetError(CL.GetDeviceInfo(
                        Handle, CL.DRIVER_VERSION, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
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
            if (obj is Device)
            {
                return Equals((Device)obj);
            }
            return false;
        }

        public bool Equals(Device other)
        {
            CLHelper.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Device left, Device right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Device left, Device right)
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
