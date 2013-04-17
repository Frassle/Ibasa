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
            ClHelper.ThrowNullException(Handle);
            try
            {
                ClHelper.GetError(Cl.RetainDevice(Handle));
            }
            catch (EntryPointNotFoundException)
            {
                throw ClHelper.VersionException(Version, 1, 2);
            }
        }

        public void Release()
        {
            ClHelper.ThrowNullException(Handle);
            try
            {
                int error = Cl.ReleaseDevice(Handle);
                Handle = IntPtr.Zero;
                ClHelper.GetError(error);
            }
            catch (EntryPointNotFoundException)
            {
                throw ClHelper.VersionException(Version, 1, 2);
            }
        }

        private unsafe Device[] CreateSubDevices(IntPtr* properties)
        {
            try
            {
                uint num_devices;
                ClHelper.GetError(Cl.CreateSubDevices(Handle, properties, 0, null, &num_devices));

                IntPtr* device_ptrs = stackalloc IntPtr[(int)num_devices];

                ClHelper.GetError(Cl.CreateSubDevices(Handle, properties, num_devices, device_ptrs, null));

                Device[] sub_devices = new Device[num_devices];
                for (uint i = 0; i < num_devices; ++i)
                {
                    sub_devices[i] = new Device(device_ptrs[i]);
                }

                return sub_devices;
            }
            catch (EntryPointNotFoundException)
            {
                throw ClHelper.VersionException(Version, 1, 2);
            }
        }

        public Device[] CreateSubDevicesEqually(int units)
        {
            ClHelper.ThrowNullException(Handle);

            unsafe
            {
                IntPtr* properties = stackalloc IntPtr[3];
                properties[0] = new IntPtr(Cl.DEVICE_PARTITION_EQUALLY);
                properties[1] = new IntPtr(units);
                properties[2] = new IntPtr(0);

                return CreateSubDevices(properties);
            }
        }

        public Device[] CreateSubDevicesByCounts(params int[] counts)
        {
            ClHelper.ThrowNullException(Handle);

            if (counts == null)
                throw new ArgumentNullException("counts");

            unsafe
            {
                IntPtr* properties = stackalloc IntPtr[3 + counts.Length];
                properties[0] = new IntPtr(Cl.DEVICE_PARTITION_BY_COUNTS);

                for (int i = 0; i < counts.Length; ++i)
                {
                    properties[i + 1] = new IntPtr(counts[i]);
                }
                properties[counts.Length + 1] = new IntPtr(Cl.DEVICE_PARTITION_BY_COUNTS_LIST_END);
                properties[counts.Length + 2] = new IntPtr(0);

                return CreateSubDevices(properties);
            }
        }

        public Device[] CreateSubDevicesByAffinityDomain(AffinityDomain domain)
        {
            ClHelper.ThrowNullException(Handle);

            unsafe
            {
                IntPtr* properties = stackalloc IntPtr[3];
                properties[0] = new IntPtr(Cl.DEVICE_PARTITION_BY_AFFINITY_DOMAIN);
                properties[1] = new IntPtr((int)domain);
                properties[2] = new IntPtr(0);

                return CreateSubDevices(properties);
            }
        }

        public uint AddressBits
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_ADDRESS_BITS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public bool Available
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_AVAILABLE, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public string[] BuiltInKernels
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr param_value_size_ret = UIntPtr.Zero;
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_BUILT_IN_KERNELS, UIntPtr.Zero, null, &param_value_size_ret));

                        byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_BUILT_IN_KERNELS, param_value_size_ret, data_ptr, null));

                        var str = Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                        return str.Split(';');
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public bool CompilerAvailable
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_COMPILER_AVAILABLE, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public FloatingPointCapability DoubleFloatingPointCapability
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_DOUBLE_FP_CONFIG, param_value_size, &value, null));
                    return (FloatingPointCapability)value;
                }
            }
        }
        
        public bool LittleEndian
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_ENDIAN_LITTLE, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public bool ErrorCorrectionSupport
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_ERROR_CORRECTION_SUPPORT, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public ExecutionCapabilities ExecutionCapabilities
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_EXECUTION_CAPABILITIES, param_value_size, &value, null));
                    return (ExecutionCapabilities)value;
                }
            }
        }

        public string[] Extensions
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_EXTENSIONS, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_EXTENSIONS, param_value_size_ret, data_ptr, null));

                    var str = Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                    return str.Split(' ');
                }
            }
        }

        public ulong GlobalMemoryCacheSize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_GLOBAL_MEM_CACHE_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public MemoryCacheType GlobalMemoryCacheType
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_GLOBAL_MEM_CACHE_TYPE, param_value_size, &value, null));
                    return (MemoryCacheType)value;
                }
            }
        }

        public uint GlobalMemoryCachelineSize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_GLOBAL_MEM_CACHELINE_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong GlobalMemorySize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_GLOBAL_MEM_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public FloatingPointCapability HalfFloatingPointCapability
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_HALF_FP_CONFIG, param_value_size, &value, null));
                    return (FloatingPointCapability)value;
                }
            }
        }

        public bool HostUnifiedMemory
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_HOST_UNIFIED_MEMORY, param_value_size, &value, null));
                        return value != 0;
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public bool ImageSupport
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_IMAGE_SUPPORT, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public ulong Image2dMaxHeight
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_IMAGE2D_MAX_HEIGHT, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public ulong Image2dMaxWidth
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_IMAGE2D_MAX_WIDTH, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public ulong Image3dMaxDepth
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_IMAGE3D_MAX_DEPTH, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public ulong Image3dMaxHeight
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_IMAGE3D_MAX_HEIGHT, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public ulong Image3dMaxWidth
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_IMAGE3D_MAX_WIDTH, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public ulong ImageMaxBufferSize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr value;
                        UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_IMAGE_MAX_BUFFER_SIZE, param_value_size, &value, null));
                        return value.ToUInt64();
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public ulong ImageMaxArraySize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr value;
                        UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_IMAGE_MAX_ARRAY_SIZE, param_value_size, &value, null));
                        return value.ToUInt64();
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public bool LinkerAvailable
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_LINKER_AVAILABLE, param_value_size, &value, null));
                        return value != 0;
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public ulong LocalMemorySize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_LOCAL_MEM_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public MemoryCacheType LocalMemoryType
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_LOCAL_MEM_TYPE, param_value_size, &value, null));
                    return (MemoryCacheType)value;
                }
            }
        }

        public uint MaxClockFrequency
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MAX_CLOCK_FREQUENCY, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public uint MaxComputeUnits
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MAX_COMPUTE_UNITS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public uint MaxConstantArgs
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MAX_CONSTANT_ARGS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong MaxConstantBufferSize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MAX_CONSTANT_BUFFER_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong MaxMemoryAllocSize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MAX_MEM_ALLOC_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong MaxParameterSize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MAX_PARAMETER_SIZE, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public uint MaxReadImageArgs
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MAX_READ_IMAGE_ARGS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public uint MaxSamplers
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MAX_SAMPLERS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong MaxWorkGroupSize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MAX_WORK_GROUP_SIZE, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public uint MaxWorkItemDimensions
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MAX_WORK_ITEM_DIMENSIONS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong[] MaxWorkItemSizes
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    int dim = (int)MaxWorkItemDimensions;
                    UIntPtr* sizes = stackalloc UIntPtr[dim];
                    UIntPtr param_value_size = new UIntPtr((uint)(UIntPtr.Size * dim));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MAX_WORK_ITEM_SIZES, param_value_size, sizes, null));

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
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MAX_WRITE_IMAGE_ARGS, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public uint MemBaseAddrAlign
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MEM_BASE_ADDR_ALIGN, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public uint MinDataTypeAlignSize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_MIN_DATA_TYPE_ALIGN_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public string Name
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_NAME, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_NAME, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public long NativeVectorWidthByte
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_NATIVE_VECTOR_WIDTH_CHAR, param_value_size, &value, null));
                        return value;
                    }
                }
                catch(OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public long NativeVectorWidthShort
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_NATIVE_VECTOR_WIDTH_SHORT, param_value_size, &value, null));
                        return value;
                    }
                }
                catch(OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public long NativeVectorWidthInt
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_NATIVE_VECTOR_WIDTH_INT, param_value_size, &value, null));
                        return value;
                    }
                }
                catch(OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public long NativeVectorWidthLong
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_NATIVE_VECTOR_WIDTH_LONG, param_value_size, &value, null));
                        return value;
                    }
                }
                catch(OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public long NativeVectorWidthSingle
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_NATIVE_VECTOR_WIDTH_FLOAT, param_value_size, &value, null));
                        return value;
                    }
                }
                catch(OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public long NativeVectorWidthDouble
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_NATIVE_VECTOR_WIDTH_DOUBLE, param_value_size, &value, null));
                        return value;
                    }
                }
                catch(OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public long NativeVectorWidthHalf
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_PREFERRED_VECTOR_WIDTH_HALF, param_value_size, &value, null));
                        return value;
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 1);
                }
            }
        }

        public string CVersion
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr param_value_size_ret = UIntPtr.Zero;
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_OPENCL_C_VERSION, UIntPtr.Zero, null, &param_value_size_ret));

                        byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_OPENCL_C_VERSION, param_value_size_ret, data_ptr, null));

                        return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 1);
                }
            }
        }
        
        public Device ParentDevice
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        IntPtr value;
                        UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_PARENT_DEVICE, param_value_size, &value, null));
                        return new Device(value);
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public long PartitionMaxSubDevices
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_PARTITION_MAX_SUB_DEVICES, param_value_size, &value, null));
                        return value;
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public PartitionProperties PartitionProperties
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr param_value_size = UIntPtr.Zero;
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_PARTITION_PROPERTIES, UIntPtr.Zero, null, &param_value_size));

                        IntPtr* properties = stackalloc IntPtr[(int)(param_value_size.ToUInt32() / IntPtr.Size)];

                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_PARTITION_PROPERTIES, param_value_size, properties, null));

                        bool equally = false;
                        bool byCounts = false;
                        bool byAffinityDomain = false;

                        while (*properties != IntPtr.Zero)
                        {
                            equally |= *properties == new IntPtr(Cl.DEVICE_PARTITION_EQUALLY);
                            byCounts |= *properties == new IntPtr(Cl.DEVICE_PARTITION_BY_COUNTS);
                            byAffinityDomain |= *properties == new IntPtr(Cl.DEVICE_PARTITION_BY_AFFINITY_DOMAIN);
                        }

                        return new PartitionProperties(equally, byCounts, byAffinityDomain);
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public AffinityDomain AffinityDomain
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        ulong value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_PARTITION_AFFINITY_DOMAIN, param_value_size, &value, null));
                        return (AffinityDomain)value;
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public PartitionType PartitionType
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr param_value_size = UIntPtr.Zero;
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_PARTITION_TYPE, UIntPtr.Zero, null, &param_value_size));

                        IntPtr* properties = stackalloc IntPtr[(int)(param_value_size.ToUInt32() / IntPtr.Size)];

                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_PARTITION_TYPE, param_value_size, properties, null));

                        if (properties[0] == new IntPtr(Cl.DEVICE_PARTITION_EQUALLY))
                        {
                            var units = properties[1].ToInt32();

                            return new PartitionTypeEqually(units);
                        }
                        else if (properties[0] == new IntPtr(Cl.DEVICE_PARTITION_BY_COUNTS))
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
                        else if (properties[0] == new IntPtr(Cl.DEVICE_PARTITION_BY_AFFINITY_DOMAIN))
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
                    throw ClHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public Platform Platform
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_PLATFORM, param_value_size, &value, null));
                    return new Platform(value);
                }
            }
        }

        public long PreferredVectorWidthByte
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_PREFERRED_VECTOR_WIDTH_CHAR, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long PreferredVectorWidthShort
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_PREFERRED_VECTOR_WIDTH_SHORT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long PreferredVectorWidthInt
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_PREFERRED_VECTOR_WIDTH_INT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long PreferredVectorWidthLong
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_PREFERRED_VECTOR_WIDTH_LONG, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long PreferredVectorWidthSingle
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_PREFERRED_VECTOR_WIDTH_FLOAT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long PreferredVectorWidthDouble
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_PREFERRED_VECTOR_WIDTH_DOUBLE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long PreferredVectorWidthHalf
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_PREFERRED_VECTOR_WIDTH_HALF, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public ulong PrintfBufferSize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        UIntPtr value;
                        UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_PRINTF_BUFFER_SIZE, param_value_size, &value, null));
                        return value.ToUInt64();
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public bool PreferredInteropUserSync
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_PREFERRED_INTEROP_USER_SYNC, param_value_size, &value, null));
                        return value != 0;
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public string Profile
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_PROFILE, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_PROFILE, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public ulong ProfilingTimerResolution
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)UIntPtr.Size);
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_PROFILING_TIMER_RESOLUTION, param_value_size, &value, null));
                    return value.ToUInt64();
                }
            }
        }

        public CommandQueueProperties QueueProperties
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr((uint)sizeof(ulong));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_PROFILING_TIMER_RESOLUTION, param_value_size, &value, null));
                    return (CommandQueueProperties)value;
                }
            }
        }

        public long ReferenceCount
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                try
                {
                    unsafe
                    {
                        uint value;
                        UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                        ClHelper.GetError(Cl.GetDeviceInfo(
                            Handle, Cl.DEVICE_REFERENCE_COUNT, param_value_size, &value, null));
                        return value;
                    }
                }
                catch (OpenCLException)
                {
                    throw ClHelper.VersionException(Version, 1, 2);
                }
            }
        }

        public FloatingPointCapability SingleFloatingPointCapability
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_SINGLE_FP_CONFIG, param_value_size, &value, null));
                    return (FloatingPointCapability)value;
                }
            }
        }

        public DeviceType Type
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_TYPE, param_value_size, &value, null));
                    return (DeviceType)value;
                }
            }
        }

        public string Vendor
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_VENDOR, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_VENDOR, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public long VendorID
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_VENDOR_ID, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public string Version
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_VERSION, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DEVICE_VERSION, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public string DriverVersion
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    UIntPtr param_value_size_ret = UIntPtr.Zero;
                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DRIVER_VERSION, UIntPtr.Zero, null, &param_value_size_ret));

                    byte* data_ptr = stackalloc byte[(int)param_value_size_ret.ToUInt32()];

                    ClHelper.GetError(Cl.GetDeviceInfo(
                        Handle, Cl.DRIVER_VERSION, param_value_size_ret, data_ptr, null));

                    return Marshal.PtrToStringAnsi(new IntPtr(data_ptr), (int)param_value_size_ret.ToUInt32() - 1);
                }
            }
        }

        public override int GetHashCode()
        {
            ClHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ClHelper.ThrowNullException(Handle);
            if (obj is Device)
            {
                return Equals((Device)obj);
            }
            return false;
        }

        public bool Equals(Device other)
        {
            ClHelper.ThrowNullException(Handle);
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
            ClHelper.ThrowNullException(Handle);
            return string.Format("Device: {0}", Handle.ToString());
        }
    }
}
