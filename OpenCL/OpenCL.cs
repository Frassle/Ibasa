using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public static class OpenCL
    {
        [StructLayout(LayoutKind.Sequential, Pack=1)]
        public struct cl_image_format 
        {
            public uint image_channel_order;
            public uint image_channel_data_type;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack=1)]
        public struct cl_image_desc 
        {
            public uint image_type;
            public UIntPtr image_width;
            public UIntPtr image_height;
            public UIntPtr image_depth;
            public UIntPtr image_array_size;
            public UIntPtr image_row_pitch;
            public UIntPtr image_slice_pitch;
            public uint num_mip_levels;
            public uint num_samples;
            public IntPtr buffer;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack=1)]
        public struct cl_buffer_region 
        {
            public UIntPtr origin;
            public UIntPtr size;
        }

        /* Error Codes */
        public const int CL_SUCCESS = 0;
        public const int CL_DEVICE_NOT_FOUND = -1;
        public const int CL_DEVICE_NOT_AVAILABLE = -2;
        public const int CL_COMPILER_NOT_AVAILABLE = -3;
        public const int CL_MEM_OBJECT_ALLOCATION_FAILURE = -4;
        public const int CL_OUT_OF_RESOURCES = -5;
        public const int CL_OUT_OF_HOST_MEMORY = -6;
        public const int CL_PROFILING_INFO_NOT_AVAILABLE = -7;
        public const int CL_MEM_COPY_OVERLAP = -8;
        public const int CL_IMAGE_FORMAT_MISMATCH = -9;
        public const int CL_IMAGE_FORMAT_NOT_SUPPORTED = -10;
        public const int CL_BUILD_PROGRAM_FAILURE = -11;
        public const int CL_MAP_FAILURE = -12;
        public const int CL_MISALIGNED_SUB_BUFFER_OFFSET = -13;
        public const int CL_EXEC_STATUS_ERROR_FOR_EVENTS_IN_WAIT_LIST = -14;
        public const int CL_COMPILE_PROGRAM_FAILURE = -15;
        public const int CL_LINKER_NOT_AVAILABLE = -16;
        public const int CL_LINK_PROGRAM_FAILURE = -17;
        public const int CL_DEVICE_PARTITION_FAILED = -18;
        public const int CL_KERNEL_ARG_INFO_NOT_AVAILABLE = -19;

        public const int CL_INVALID_VALUE = -30;
        public const int CL_INVALID_DEVICE_TYPE = -31;
        public const int CL_INVALID_PLATFORM = -32;
        public const int CL_INVALID_DEVICE = -33;
        public const int CL_INVALID_CONTEXT = -34;
        public const int CL_INVALID_QUEUE_PROPERTIES = -35;
        public const int CL_INVALID_COMMAND_QUEUE = -36;
        public const int CL_INVALID_HOST_PTR = -37;
        public const int CL_INVALID_MEM_OBJECT = -38;
        public const int CL_INVALID_IMAGE_FORMAT_DESCRIPTOR = -39;
        public const int CL_INVALID_IMAGE_SIZE = -40;
        public const int CL_INVALID_SAMPLER = -41;
        public const int CL_INVALID_BINARY = -42;
        public const int CL_INVALID_BUILD_OPTIONS = -43;
        public const int CL_INVALID_PROGRAM = -44;
        public const int CL_INVALID_PROGRAM_EXECUTABLE = -45;
        public const int CL_INVALID_KERNEL_NAME = -46;
        public const int CL_INVALID_KERNEL_DEFINITION = -47;
        public const int CL_INVALID_KERNEL = -48;
        public const int CL_INVALID_ARG_INDEX = -49;
        public const int CL_INVALID_ARG_VALUE = -50;
        public const int CL_INVALID_ARG_SIZE = -51;
        public const int CL_INVALID_KERNEL_ARGS = -52;
        public const int CL_INVALID_WORK_DIMENSION = -53;
        public const int CL_INVALID_WORK_GROUP_SIZE = -54;
        public const int CL_INVALID_WORK_ITEM_SIZE = -55;
        public const int CL_INVALID_GLOBAL_OFFSET = -56;
        public const int CL_INVALID_EVENT_WAIT_LIST = -57;
        public const int CL_INVALID_EVENT = -58;
        public const int CL_INVALID_OPERATION = -59;
        public const int CL_INVALID_GL_OBJECT = -60;
        public const int CL_INVALID_BUFFER_SIZE = -61;
        public const int CL_INVALID_MIP_LEVEL = -62;
        public const int CL_INVALID_GLOBAL_WORK_SIZE = -63;
        public const int CL_INVALID_PROPERTY = -64;
        public const int CL_INVALID_IMAGE_DESCRIPTOR = -65;
        public const int CL_INVALID_COMPILER_OPTIONS = -66;
        public const int CL_INVALID_LINKER_OPTIONS = -67;
        public const int CL_INVALID_DEVICE_PARTITION_COUNT = -68;

        /* OpenCL Version */
        public const int CL_VERSION_1_0 = 1;
        public const int CL_VERSION_1_1 = 1;
        public const int CL_VERSION_1_2 = 1;

        /* cl_bool */
        public const int CL_FALSE = 0;
        public const int CL_TRUE = 1;
        public const int CL_BLOCKING = CL_TRUE;
        public const int CL_NON_BLOCKING = CL_FALSE;

        /* cl_platform_info */
        public const int CL_PLATFORM_PROFILE = 0x0900;
        public const int CL_PLATFORM_VERSION = 0x0901;
        public const int CL_PLATFORM_NAME = 0x0902;
        public const int CL_PLATFORM_VENDOR = 0x0903;
        public const int CL_PLATFORM_EXTENSIONS = 0x0904;

        /* cl_device_type - bitfield */
        public const int CL_DEVICE_TYPE_DEFAULT = (1 << 0);
        public const int CL_DEVICE_TYPE_CPU = (1 << 1);
        public const int CL_DEVICE_TYPE_GPU = (1 << 2);
        public const int CL_DEVICE_TYPE_ACCELERATOR = (1 << 3);
        public const int CL_DEVICE_TYPE_CUSTOM = (1 << 4);
        public const int CL_DEVICE_TYPE_ALL = -1;

        /* cl_device_info */
        public const int CL_DEVICE_TYPE = 0x1000;
        public const int CL_DEVICE_VENDOR_ID = 0x1001;
        public const int CL_DEVICE_MAX_COMPUTE_UNITS = 0x1002;
        public const int CL_DEVICE_MAX_WORK_ITEM_DIMENSIONS = 0x1003;
        public const int CL_DEVICE_MAX_WORK_GROUP_SIZE = 0x1004;
        public const int CL_DEVICE_MAX_WORK_ITEM_SIZES = 0x1005;
        public const int CL_DEVICE_PREFERRED_VECTOR_WIDTH_CHAR = 0x1006;
        public const int CL_DEVICE_PREFERRED_VECTOR_WIDTH_SHORT = 0x1007;
        public const int CL_DEVICE_PREFERRED_VECTOR_WIDTH_INT = 0x1008;
        public const int CL_DEVICE_PREFERRED_VECTOR_WIDTH_LONG = 0x1009;
        public const int CL_DEVICE_PREFERRED_VECTOR_WIDTH_FLOAT = 0x100A;
        public const int CL_DEVICE_PREFERRED_VECTOR_WIDTH_DOUBLE = 0x100B;
        public const int CL_DEVICE_MAX_CLOCK_FREQUENCY = 0x100C;
        public const int CL_DEVICE_ADDRESS_BITS = 0x100D;
        public const int CL_DEVICE_MAX_READ_IMAGE_ARGS = 0x100E;
        public const int CL_DEVICE_MAX_WRITE_IMAGE_ARGS = 0x100F;
        public const int CL_DEVICE_MAX_MEM_ALLOC_SIZE = 0x1010;
        public const int CL_DEVICE_IMAGE2D_MAX_WIDTH = 0x1011;
        public const int CL_DEVICE_IMAGE2D_MAX_HEIGHT = 0x1012;
        public const int CL_DEVICE_IMAGE3D_MAX_WIDTH = 0x1013;
        public const int CL_DEVICE_IMAGE3D_MAX_HEIGHT = 0x1014;
        public const int CL_DEVICE_IMAGE3D_MAX_DEPTH = 0x1015;
        public const int CL_DEVICE_IMAGE_SUPPORT = 0x1016;
        public const int CL_DEVICE_MAX_PARAMETER_SIZE = 0x1017;
        public const int CL_DEVICE_MAX_SAMPLERS = 0x1018;
        public const int CL_DEVICE_MEM_BASE_ADDR_ALIGN = 0x1019;
        public const int CL_DEVICE_MIN_DATA_TYPE_ALIGN_SIZE = 0x101A;
        public const int CL_DEVICE_SINGLE_FP_CONFIG = 0x101B;
        public const int CL_DEVICE_GLOBAL_MEM_CACHE_TYPE = 0x101C;
        public const int CL_DEVICE_GLOBAL_MEM_CACHELINE_SIZE = 0x101D;
        public const int CL_DEVICE_GLOBAL_MEM_CACHE_SIZE = 0x101E;
        public const int CL_DEVICE_GLOBAL_MEM_SIZE = 0x101F;
        public const int CL_DEVICE_MAX_CONSTANT_BUFFER_SIZE = 0x1020;
        public const int CL_DEVICE_MAX_CONSTANT_ARGS = 0x1021;
        public const int CL_DEVICE_LOCAL_MEM_TYPE = 0x1022;
        public const int CL_DEVICE_LOCAL_MEM_SIZE = 0x1023;
        public const int CL_DEVICE_ERROR_CORRECTION_SUPPORT = 0x1024;
        public const int CL_DEVICE_PROFILING_TIMER_RESOLUTION = 0x1025;
        public const int CL_DEVICE_ENDIAN_LITTLE = 0x1026;
        public const int CL_DEVICE_AVAILABLE = 0x1027;
        public const int CL_DEVICE_COMPILER_AVAILABLE = 0x1028;
        public const int CL_DEVICE_EXECUTION_CAPABILITIES = 0x1029;
        public const int CL_DEVICE_QUEUE_PROPERTIES = 0x102A;
        public const int CL_DEVICE_NAME = 0x102B;
        public const int CL_DEVICE_VENDOR = 0x102C;
        public const int CL_DRIVER_VERSION = 0x102D;
        public const int CL_DEVICE_PROFILE = 0x102E;
        public const int CL_DEVICE_VERSION = 0x102F;
        public const int CL_DEVICE_EXTENSIONS = 0x1030;
        public const int CL_DEVICE_PLATFORM = 0x1031;
        public const int CL_DEVICE_DOUBLE_FP_CONFIG = 0x1032;
        /* 0x1033 reserved for CL_DEVICE_HALF_FP_CONFIG */
        public const int CL_DEVICE_PREFERRED_VECTOR_WIDTH_HALF = 0x1034;
        public const int CL_DEVICE_HOST_UNIFIED_MEMORY = 0x1035;
        public const int CL_DEVICE_NATIVE_VECTOR_WIDTH_CHAR = 0x1036;
        public const int CL_DEVICE_NATIVE_VECTOR_WIDTH_SHORT = 0x1037;
        public const int CL_DEVICE_NATIVE_VECTOR_WIDTH_INT = 0x1038;
        public const int CL_DEVICE_NATIVE_VECTOR_WIDTH_LONG = 0x1039;
        public const int CL_DEVICE_NATIVE_VECTOR_WIDTH_FLOAT = 0x103A;
        public const int CL_DEVICE_NATIVE_VECTOR_WIDTH_DOUBLE = 0x103B;
        public const int CL_DEVICE_NATIVE_VECTOR_WIDTH_HALF = 0x103C;
        public const int CL_DEVICE_OPENCL_C_VERSION = 0x103D;
        public const int CL_DEVICE_LINKER_AVAILABLE = 0x103E;
        public const int CL_DEVICE_BUILT_IN_KERNELS = 0x103F;
        public const int CL_DEVICE_IMAGE_MAX_BUFFER_SIZE = 0x1040;
        public const int CL_DEVICE_IMAGE_MAX_ARRAY_SIZE = 0x1041;
        public const int CL_DEVICE_PARENT_DEVICE = 0x1042;
        public const int CL_DEVICE_PARTITION_MAX_SUB_DEVICES = 0x1043;
        public const int CL_DEVICE_PARTITION_PROPERTIES = 0x1044;
        public const int CL_DEVICE_PARTITION_AFFINITY_DOMAIN = 0x1045;
        public const int CL_DEVICE_PARTITION_TYPE = 0x1046;
        public const int CL_DEVICE_REFERENCE_COUNT = 0x1047;
        public const int CL_DEVICE_PREFERRED_INTEROP_USER_SYNC = 0x1048;
        public const int CL_DEVICE_PRINTF_BUFFER_SIZE = 0x1049;
        public const int CL_DEVICE_IMAGE_PITCH_ALIGNMENT = 0x104A;
        public const int CL_DEVICE_IMAGE_BASE_ADDRESS_ALIGNMENT = 0x104B;

        /* cl_device_fp_config - bitfield */
        public const int CL_FP_DENORM = (1 << 0);
        public const int CL_FP_INF_NAN = (1 << 1);
        public const int CL_FP_ROUND_TO_NEAREST = (1 << 2);
        public const int CL_FP_ROUND_TO_ZERO = (1 << 3);
        public const int CL_FP_ROUND_TO_INF = (1 << 4);
        public const int CL_FP_FMA = (1 << 5);
        public const int CL_FP_SOFT_FLOAT = (1 << 6);
        public const int CL_FP_CORRECTLY_ROUNDED_DIVIDE_SQRT = (1 << 7);

        /* cl_device_mem_cache_type */
        public const int CL_NONE = 0x0;
        public const int CL_READ_ONLY_CACHE = 0x1;
        public const int CL_READ_WRITE_CACHE = 0x2;

        /* cl_device_local_mem_type */
        public const int CL_LOCAL = 0x1;
        public const int CL_GLOBAL = 0x2;

        /* cl_device_exec_capabilities - bitfield */
        public const int CL_EXEC_KERNEL = (1 << 0);
        public const int CL_EXEC_NATIVE_KERNEL = (1 << 1);

        /* cl_command_queue_properties - bitfield */
        public const int CL_QUEUE_OUT_OF_ORDER_EXEC_MODE_ENABLE = (1 << 0);
        public const int CL_QUEUE_PROFILING_ENABLE = (1 << 1);

        /* cl_context_info */
        public const int CL_CONTEXT_REFERENCE_COUNT = 0x1080;
        public const int CL_CONTEXT_DEVICES = 0x1081;
        public const int CL_CONTEXT_PROPERTIES = 0x1082;
        public const int CL_CONTEXT_NUM_DEVICES = 0x1083;

        /* cl_context_properties */
        public const int CL_CONTEXT_PLATFORM = 0x1084;
        public const int CL_CONTEXT_INTEROP_USER_SYNC = 0x1085;

        /* cl_device_partition_property */
        public const int CL_DEVICE_PARTITION_EQUALLY = 0x1086;
        public const int CL_DEVICE_PARTITION_BY_COUNTS = 0x1087;
        public const int CL_DEVICE_PARTITION_BY_COUNTS_LIST_END = 0x0;
        public const int CL_DEVICE_PARTITION_BY_AFFINITY_DOMAIN = 0x1088;

        /* cl_device_affinity_domain */
        public const int CL_DEVICE_AFFINITY_DOMAIN_NUMA = (1 << 0);
        public const int CL_DEVICE_AFFINITY_DOMAIN_L4_CACHE = (1 << 1);
        public const int CL_DEVICE_AFFINITY_DOMAIN_L3_CACHE = (1 << 2);
        public const int CL_DEVICE_AFFINITY_DOMAIN_L2_CACHE = (1 << 3);
        public const int CL_DEVICE_AFFINITY_DOMAIN_L1_CACHE = (1 << 4);
        public const int CL_DEVICE_AFFINITY_DOMAIN_NEXT_PARTITIONABLE = (1 << 5);

        /* cl_command_queue_info */
        public const int CL_QUEUE_CONTEXT = 0x1090;
        public const int CL_QUEUE_DEVICE = 0x1091;
        public const int CL_QUEUE_REFERENCE_COUNT = 0x1092;
        public const int CL_QUEUE_PROPERTIES = 0x1093;

        /* cl_mem_flags - bitfield */
        public const int CL_MEM_READ_WRITE = (1 << 0);
        public const int CL_MEM_WRITE_ONLY = (1 << 1);
        public const int CL_MEM_READ_ONLY = (1 << 2);
        public const int CL_MEM_USE_HOST_PTR = (1 << 3);
        public const int CL_MEM_ALLOC_HOST_PTR = (1 << 4);
        public const int CL_MEM_COPY_HOST_PTR = (1 << 5);
        // reserved (1 << 6)
        public const int CL_MEM_HOST_WRITE_ONLY = (1 << 7);
        public const int CL_MEM_HOST_READ_ONLY = (1 << 8);
        public const int CL_MEM_HOST_NO_ACCESS = (1 << 9);

        /* cl_mem_migration_flags - bitfield */
        public const int CL_MIGRATE_MEM_OBJECT_HOST = (1 << 0);
        public const int CL_MIGRATE_MEM_OBJECT_CONTENT_UNDEFINED = (1 << 1);

        /* cl_channel_order */
        public const int CL_R = 0x10B0;
        public const int CL_A = 0x10B1;
        public const int CL_RG = 0x10B2;
        public const int CL_RA = 0x10B3;
        public const int CL_RGB = 0x10B4;
        public const int CL_RGBA = 0x10B5;
        public const int CL_BGRA = 0x10B6;
        public const int CL_ARGB = 0x10B7;
        public const int CL_INTENSITY = 0x10B8;
        public const int CL_LUMINANCE = 0x10B9;
        public const int CL_Rx = 0x10BA;
        public const int CL_RGx = 0x10BB;
        public const int CL_RGBx = 0x10BC;
        public const int CL_DEPTH = 0x10BD;
        public const int CL_DEPTH_STENCIL = 0x10BE;

        /* cl_channel_type */
        public const int CL_SNORM_INT8 = 0x10D0;
        public const int CL_SNORM_INT16 = 0x10D1;
        public const int CL_UNORM_INT8 = 0x10D2;
        public const int CL_UNORM_INT16 = 0x10D3;
        public const int CL_UNORM_SHORT_565 = 0x10D4;
        public const int CL_UNORM_SHORT_555 = 0x10D5;
        public const int CL_UNORM_INT_101010 = 0x10D6;
        public const int CL_SIGNED_INT8 = 0x10D7;
        public const int CL_SIGNED_INT16 = 0x10D8;
        public const int CL_SIGNED_INT32 = 0x10D9;
        public const int CL_UNSIGNED_INT8 = 0x10DA;
        public const int CL_UNSIGNED_INT16 = 0x10DB;
        public const int CL_UNSIGNED_INT32 = 0x10DC;
        public const int CL_HALF_FLOAT = 0x10DD;
        public const int CL_FLOAT = 0x10DE;
        public const int CL_UNORM_INT24 = 0x10DF;

        /* cl_mem_object_type */
        public const int CL_MEM_OBJECT_BUFFER = 0x10F0;
        public const int CL_MEM_OBJECT_IMAGE2D = 0x10F1;
        public const int CL_MEM_OBJECT_IMAGE3D = 0x10F2;
        public const int CL_MEM_OBJECT_IMAGE2D_ARRAY = 0x10F3;
        public const int CL_MEM_OBJECT_IMAGE1D = 0x10F4;
        public const int CL_MEM_OBJECT_IMAGE1D_ARRAY = 0x10F5;
        public const int CL_MEM_OBJECT_IMAGE1D_BUFFER = 0x10F6;

        /* cl_mem_info */
        public const int CL_MEM_TYPE = 0x1100;
        public const int CL_MEM_FLAGS = 0x1101;
        public const int CL_MEM_SIZE = 0x1102;
        public const int CL_MEM_HOST_PTR = 0x1103;
        public const int CL_MEM_MAP_COUNT = 0x1104;
        public const int CL_MEM_REFERENCE_COUNT = 0x1105;
        public const int CL_MEM_CONTEXT = 0x1106;
        public const int CL_MEM_ASSOCIATED_MEMOBJECT = 0x1107;
        public const int CL_MEM_OFFSET = 0x1108;

        /* cl_image_info */
        public const int CL_IMAGE_FORMAT = 0x1110;
        public const int CL_IMAGE_ELEMENT_SIZE = 0x1111;
        public const int CL_IMAGE_ROW_PITCH = 0x1112;
        public const int CL_IMAGE_SLICE_PITCH = 0x1113;
        public const int CL_IMAGE_WIDTH = 0x1114;
        public const int CL_IMAGE_HEIGHT = 0x1115;
        public const int CL_IMAGE_DEPTH = 0x1116;
        public const int CL_IMAGE_ARRAY_SIZE = 0x1117;
        public const int CL_IMAGE_BUFFER = 0x1118;
        public const int CL_IMAGE_NUM_MIP_LEVELS = 0x1119;
        public const int CL_IMAGE_NUM_SAMPLES = 0x111A;

        /* cl_addressing_mode */
        public const int CL_ADDRESS_NONE = 0x1130;
        public const int CL_ADDRESS_CLAMP_TO_EDGE = 0x1131;
        public const int CL_ADDRESS_CLAMP = 0x1132;
        public const int CL_ADDRESS_REPEAT = 0x1133;
        public const int CL_ADDRESS_MIRRORED_REPEAT = 0x1134;

        /* cl_filter_mode */
        public const int CL_FILTER_NEAREST = 0x1140;
        public const int CL_FILTER_LINEAR = 0x1141;

        /* cl_sampler_info */
        public const int CL_SAMPLER_REFERENCE_COUNT = 0x1150;
        public const int CL_SAMPLER_CONTEXT = 0x1151;
        public const int CL_SAMPLER_NORMALIZED_COORDS = 0x1152;
        public const int CL_SAMPLER_ADDRESSING_MODE = 0x1153;
        public const int CL_SAMPLER_FILTER_MODE = 0x1154;

        /* cl_map_flags - bitfield */
        public const int CL_MAP_READ = (1 << 0);
        public const int CL_MAP_WRITE = (1 << 1);
        public const int CL_MAP_WRITE_INVALIDATE_REGION = (1 << 2);

        /* cl_program_info */
        public const int CL_PROGRAM_REFERENCE_COUNT = 0x1160;
        public const int CL_PROGRAM_CONTEXT = 0x1161;
        public const int CL_PROGRAM_NUM_DEVICES = 0x1162;
        public const int CL_PROGRAM_DEVICES = 0x1163;
        public const int CL_PROGRAM_SOURCE = 0x1164;
        public const int CL_PROGRAM_BINARY_SIZES = 0x1165;
        public const int CL_PROGRAM_BINARIES = 0x1166;
        public const int CL_PROGRAM_NUM_KERNELS = 0x1167;
        public const int CL_PROGRAM_KERNEL_NAMES = 0x1168;

        /* cl_program_build_info */
        public const int CL_PROGRAM_BUILD_STATUS = 0x1181;
        public const int CL_PROGRAM_BUILD_OPTIONS = 0x1182;
        public const int CL_PROGRAM_BUILD_LOG = 0x1183;
        public const int CL_PROGRAM_BINARY_TYPE = 0x1184;

        /* cl_program_binary_type */
        public const int CL_PROGRAM_BINARY_TYPE_NONE = 0x0;
        public const int CL_PROGRAM_BINARY_TYPE_COMPILED_OBJECT = 0x1;
        public const int CL_PROGRAM_BINARY_TYPE_LIBRARY = 0x2;
        public const int CL_PROGRAM_BINARY_TYPE_EXECUTABLE = 0x4;

        /* cl_build_status */
        public const int CL_BUILD_SUCCESS = 0;
        public const int CL_BUILD_NONE = -1;
        public const int CL_BUILD_ERROR = -2;
        public const int CL_BUILD_IN_PROGRESS = -3;

        /* cl_kernel_info */
        public const int CL_KERNEL_FUNCTION_NAME = 0x1190;
        public const int CL_KERNEL_NUM_ARGS = 0x1191;
        public const int CL_KERNEL_REFERENCE_COUNT = 0x1192;
        public const int CL_KERNEL_CONTEXT = 0x1193;
        public const int CL_KERNEL_PROGRAM = 0x1194;
        public const int CL_KERNEL_ATTRIBUTES = 0x1195;

        /* cl_kernel_arg_info */
        public const int CL_KERNEL_ARG_ADDRESS_QUALIFIER = 0x1196;
        public const int CL_KERNEL_ARG_ACCESS_QUALIFIER = 0x1197;
        public const int CL_KERNEL_ARG_TYPE_NAME = 0x1198;
        public const int CL_KERNEL_ARG_TYPE_QUALIFIER = 0x1199;
        public const int CL_KERNEL_ARG_NAME = 0x119A;

        /* cl_kernel_arg_address_qualifier */
        public const int CL_KERNEL_ARG_ADDRESS_GLOBAL = 0x119B;
        public const int CL_KERNEL_ARG_ADDRESS_LOCAL = 0x119C;
        public const int CL_KERNEL_ARG_ADDRESS_CONSTANT = 0x119D;
        public const int CL_KERNEL_ARG_ADDRESS_PRIVATE = 0x119E;

        /* cl_kernel_arg_access_qualifier */
        public const int CL_KERNEL_ARG_ACCESS_READ_ONLY = 0x11A0;
        public const int CL_KERNEL_ARG_ACCESS_WRITE_ONLY = 0x11A1;
        public const int CL_KERNEL_ARG_ACCESS_READ_WRITE = 0x11A2;
        public const int CL_KERNEL_ARG_ACCESS_NONE = 0x11A3;

        /* cl_kernel_arg_type_qualifer */
        public const int CL_KERNEL_ARG_TYPE_NONE = 0;
        public const int CL_KERNEL_ARG_TYPE_CONST = (1 << 0);
        public const int CL_KERNEL_ARG_TYPE_RESTRICT = (1 << 1);
        public const int CL_KERNEL_ARG_TYPE_VOLATILE = (1 << 2);

        /* cl_kernel_work_group_info */
        public const int CL_KERNEL_WORK_GROUP_SIZE = 0x11B0;
        public const int CL_KERNEL_COMPILE_WORK_GROUP_SIZE = 0x11B1;
        public const int CL_KERNEL_LOCAL_MEM_SIZE = 0x11B2;
        public const int CL_KERNEL_PREFERRED_WORK_GROUP_SIZE_MULTIPLE = 0x11B3;
        public const int CL_KERNEL_PRIVATE_MEM_SIZE = 0x11B4;
        public const int CL_KERNEL_GLOBAL_WORK_SIZE = 0x11B5;

        /* cl_event_info */
        public const int CL_EVENT_COMMAND_QUEUE = 0x11D0;
        public const int CL_EVENT_COMMAND_TYPE = 0x11D1;
        public const int CL_EVENT_REFERENCE_COUNT = 0x11D2;
        public const int CL_EVENT_COMMAND_EXECUTION_STATUS = 0x11D3;
        public const int CL_EVENT_CONTEXT = 0x11D4;

        /* cl_command_type */
        public const int CL_COMMAND_NDRANGE_KERNEL = 0x11F0;
        public const int CL_COMMAND_TASK = 0x11F1;
        public const int CL_COMMAND_NATIVE_KERNEL = 0x11F2;
        public const int CL_COMMAND_READ_BUFFER = 0x11F3;
        public const int CL_COMMAND_WRITE_BUFFER = 0x11F4;
        public const int CL_COMMAND_COPY_BUFFER = 0x11F5;
        public const int CL_COMMAND_READ_IMAGE = 0x11F6;
        public const int CL_COMMAND_WRITE_IMAGE = 0x11F7;
        public const int CL_COMMAND_COPY_IMAGE = 0x11F8;
        public const int CL_COMMAND_COPY_IMAGE_TO_BUFFER = 0x11F9;
        public const int CL_COMMAND_COPY_BUFFER_TO_IMAGE = 0x11FA;
        public const int CL_COMMAND_MAP_BUFFER = 0x11FB;
        public const int CL_COMMAND_MAP_IMAGE = 0x11FC;
        public const int CL_COMMAND_UNMAP_MEM_OBJECT = 0x11FD;
        public const int CL_COMMAND_MARKER = 0x11FE;
        public const int CL_COMMAND_ACQUIRE_GL_OBJECTS = 0x11FF;
        public const int CL_COMMAND_RELEASE_GL_OBJECTS = 0x1200;
        public const int CL_COMMAND_READ_BUFFER_RECT = 0x1201;
        public const int CL_COMMAND_WRITE_BUFFER_RECT = 0x1202;
        public const int CL_COMMAND_COPY_BUFFER_RECT = 0x1203;
        public const int CL_COMMAND_USER = 0x1204;
        public const int CL_COMMAND_BARRIER = 0x1205;
        public const int CL_COMMAND_MIGRATE_MEM_OBJECTS = 0x1206;
        public const int CL_COMMAND_FILL_BUFFER = 0x1207;
        public const int CL_COMMAND_FILL_IMAGE = 0x1208;

        /* command execution status */
        public const int CL_COMPLETE = 0x0;
        public const int CL_RUNNING = 0x1;
        public const int CL_SUBMITTED = 0x2;
        public const int CL_QUEUED = 0x3;

        /* cl_buffer_create_type */
        public const int CL_BUFFER_CREATE_TYPE_REGION = 0x1220;

        /* cl_profiling_info */
        public const int CL_PROFILING_COMMAND_QUEUED = 0x1280;
        public const int CL_PROFILING_COMMAND_SUBMIT = 0x1281;
        public const int CL_PROFILING_COMMAND_START = 0x1282;
        public const int CL_PROFILING_COMMAND_END = 0x1283;

        /********************************************************************************************************/

        /* Platform API */
        
        [DllImport("opencl.dll", EntryPoint = "clGetPlatformIDs", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetPlatformIDs(
            uint num_entries, 
            IntPtr* platforms, 
            uint* num_platforms);

        [DllImport("opencl.dll", EntryPoint = "clGetPlatformInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetPlatformInfo(
            IntPtr platform, 
            int param_name, 
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);

        /* Device APIs */

        [DllImport("opencl.dll", EntryPoint = "clGetDeviceIDs", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetDeviceIDs(
            IntPtr platform,
            uint device_type,
            uint num_entries,
            IntPtr* devices,
            uint* num_devices);

        [DllImport("opencl.dll", EntryPoint = "clGetDeviceInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetDeviceInfo(
            IntPtr device,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);
        
        [DllImport("opencl.dll", EntryPoint = "clCreateSubDevices", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clCreateSubDevices(
            IntPtr in_device,
            IntPtr* properties,
            uint num_devices,
            IntPtr* out_devices,
            uint* num_devices_ret);

        [DllImport("opencl.dll", EntryPoint = "clRetainDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clRetainDevice(
            IntPtr device);

        [DllImport("opencl.dll", EntryPoint = "clReleaseDevice", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clReleaseDevice(
            IntPtr device);
    
        /* Context APIs  */

        [DllImport("opencl.dll", EntryPoint = "clCreateContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clCreateContext(
            IntPtr* properties,
            uint num_devices,
            IntPtr* devices,
            IntPtr pfn_notify,
            void* user_data,
            int* errcode_ret);

        [DllImport("opencl.dll", EntryPoint = "clCreateContextFromType", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clCreateContext(
            IntPtr* properties,
            int device_type,
            IntPtr pfn_notify,
            void* user_data,
            int* errcode_ret);
        
        [DllImport("opencl.dll", EntryPoint = "clRetainContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clRetainContext(
            IntPtr context);

        [DllImport("opencl.dll", EntryPoint = "clReleaseContext", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clReleaseContext(
            IntPtr context);

        [DllImport("opencl.dll", EntryPoint = "clGetContextInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetContextInfo(
            IntPtr context,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);

        /* Command Queue APIs */
        [DllImport("opencl.dll", EntryPoint = "clCreateCommandQueue", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clCreateCommandQueue(
            IntPtr context,
            IntPtr device,
            ulong properties,
            int* errcode_ret);

        [DllImport("opencl.dll", EntryPoint = "clRetainCommandQueue", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clRetainCommandQueue(
            IntPtr command_queue);

        [DllImport("opencl.dll", EntryPoint = "clReleaseCommandQueue", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clReleaseCommandQueue(
            IntPtr command_queue);

        [DllImport("opencl.dll", EntryPoint = "clGetCommandQueueInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetCommandQueueInfo(
            IntPtr command_queue,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);

        /* Memory Object APIs */
        [DllImport("opencl.dll", EntryPoint = "clCreateBuffer", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clCreateBuffer(
            IntPtr context,
            ulong flags,
            UIntPtr size,
            void* host_ptr,
            int* errcode_ret);

        [DllImport("opencl.dll", EntryPoint = "clCreateSubBuffer", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clCreateSubBuffer(
            IntPtr buffer,
            ulong flags,
            uint buffer_create_type,
            void* buffer_create_info,
            int* errcode_ret);

        [DllImport("opencl.dll", EntryPoint = "clCreateImage", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clCreateImage(
            IntPtr context,
            ulong flags,
            cl_image_format* image_format,
            cl_image_desc* image_desc,
            void* host_ptr,
            int* errcode_ret);

        [DllImport("opencl.dll", EntryPoint = "clRetainMemObject", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clRetainMemObject(
            IntPtr memobj);

        [DllImport("opencl.dll", EntryPoint = "clReleaseMemObject", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clReleaseMemObject(
            IntPtr memobj);

        [DllImport("opencl.dll", EntryPoint = "clGetSupportedImageFormats", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetSupportedImageFormats(
            IntPtr context,
            ulong flags,
            uint image_type,
            uint num_entries,
            cl_image_format* image_formats,
            uint* num_image_formats);
        
        [DllImport("opencl.dll", EntryPoint = "clGetMemObjectInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetMemObjectInfo(
            IntPtr memobj,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);

        [DllImport("opencl.dll", EntryPoint = "clGetImageInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetImageInfo(
            IntPtr memobj,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);

        [DllImport("opencl.dll", EntryPoint = "clSetMemObjectDestructorCallback", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clSetMemObjectDestructorCallback(
            IntPtr memobj,
            IntPtr pfn_notify,
            void* user_data);


        /* Sampler APIs */
        [DllImport("opencl.dll", EntryPoint = "clCreateSampler", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clCreateSampler(
            IntPtr context,
            uint normalized_coords,
            uint addressing_mode,
            uint filter_mode,
            int* errcode_ret);

        [DllImport("opencl.dll", EntryPoint = "clRetainSampler", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clRetainSampler(
            IntPtr sampler);

        [DllImport("opencl.dll", EntryPoint = "clReleaseSampler", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clReleaseSampler(
            IntPtr sampler);

        [DllImport("opencl.dll", EntryPoint = "clGetSamplerInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetSamplerInfo(
            IntPtr sampler,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);
                            
        /* Program Object APIs  */
        [DllImport("opencl.dll", EntryPoint = "clCreateProgramWithSource", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clCreateProgramWithSource(
            IntPtr context,
            uint count,
            byte** strings,
            UIntPtr* lengths,
            int* errcode_ret);

        [DllImport("opencl.dll", EntryPoint = "clCreateProgramWithBinary", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clCreateProgramWithBinary(
            IntPtr context,
            uint num_devices,
            IntPtr* device_list,
            UIntPtr* lengths,
            byte** binaries,
            int* binary_status,
            int* errcode_ret);

        [DllImport("opencl.dll", EntryPoint = "clCreateProgramWithBuiltInKernels", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clCreateProgramWithBuiltInKernels(
            IntPtr context,
            uint num_devices,
            IntPtr* device_list,
            byte* kernel_names,
            int* errcode_ret);

        [DllImport("opencl.dll", EntryPoint = "clRetainProgram", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clRetainProgram(
            IntPtr program);

        [DllImport("opencl.dll", EntryPoint = "clReleaseProgram", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clReleaseProgram(
            IntPtr program);

        [DllImport("opencl.dll", EntryPoint = "clBuildProgram", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clBuildProgram(
            IntPtr program,
            uint num_devices,
            IntPtr* device_list,
            byte* options,
            IntPtr pfn_notify,
            void* user_data);
        
        [DllImport("opencl.dll", EntryPoint = "clCompileProgram", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clCompileProgram(
            IntPtr program,
            uint num_devices,
            IntPtr* device_list,
            byte* options,
            uint num_input_headers,
            IntPtr* input_headers,
            byte** header_include_names,
            IntPtr pfn_notify,
            void* user_data);

        [DllImport("opencl.dll", EntryPoint = "clLinkProgram", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clLinkProgram(
            IntPtr context,
            uint num_devices,
            IntPtr* device_list,
            byte* options,
            uint num_input_programs,
            IntPtr* input_programs,
            IntPtr pfn_notify,
            void* user_data,
            int* errcode_ret);

        [DllImport("opencl.dll", EntryPoint = "clUnloadPlatformCompiler", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clUnloadPlatformCompiler(
            IntPtr platform);

        [DllImport("opencl.dll", EntryPoint = "clGetProgramInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetProgramInfo(
            IntPtr program,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);

        [DllImport("opencl.dll", EntryPoint = "clGetProgramBuildInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetProgramBuildInfo(
            IntPtr program,
            IntPtr device,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);
                            
        /* Kernel Object APIs */
        [DllImport("opencl.dll", EntryPoint = "clCreateKernel", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clCreateKernel(
            IntPtr program,
            byte* kernel_name,
            int* errcode_ret);

        [DllImport("opencl.dll", EntryPoint = "clCreateKernelsInProgram", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clCreateKernelsInProgram(
            IntPtr program,
            uint num_kernels,
            IntPtr* kernels,
            uint* num_kernels_ret);

        [DllImport("opencl.dll", EntryPoint = "clRetainKernel", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clRetainKernel(
            IntPtr kernel);

        [DllImport("opencl.dll", EntryPoint = "clReleaseKernel", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clReleaseKernel(
            IntPtr kernel);

        [DllImport("opencl.dll", EntryPoint = "clSetKernelArg", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clSetKernelArg(
            IntPtr kernel,
            uint arg_index,
            UIntPtr arg_size,
            void* arg_value);
        
        [DllImport("opencl.dll", EntryPoint = "clGetKernelInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetKernelInfo(
            IntPtr kernel,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);
        
        [DllImport("opencl.dll", EntryPoint = "clGetKernelArgInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetKernelArgInfo(
            IntPtr kernel,
            uint arg_index,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);
        
        [DllImport("opencl.dll", EntryPoint = "clGetKernelWorkGroupInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetKernelWorkGroupInfo(
            IntPtr kernel,
            IntPtr device,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);

        /* Event Object APIs */
        [DllImport("opencl.dll", EntryPoint = "clWaitForEvents", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clWaitForEvents(
            uint num_events,
            IntPtr* event_list);

        [DllImport("opencl.dll", EntryPoint = "clGetEventInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetEventInfo(
            IntPtr @event,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);

        
        [DllImport("opencl.dll", EntryPoint = "clCreateUserEvent", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern IntPtr clCreateUserEvent(
            IntPtr context,
            int* errcode_ret);

        [DllImport("opencl.dll", EntryPoint = "clRetainEvent", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clRetainEvent(
            IntPtr @event);

        [DllImport("opencl.dll", EntryPoint = "clReleaseEvent", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clReleaseEvent(
            IntPtr @event);

        [DllImport("opencl.dll", EntryPoint = "clSetUserEventStatus", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clSetUserEventStatus(
            IntPtr @event,
            int execution_status);

        [DllImport("opencl.dll", EntryPoint = "clSetEventCallback", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clSetEventCallback(
            IntPtr @event,
            int command_exec_callback_type,
            IntPtr pfn_notify,
            void* user_data);

        /* Profiling APIs */
        [DllImport("opencl.dll", EntryPoint = "clGetEventProfilingInfo", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clGetEventProfilingInfo(
            IntPtr @event,
            int param_name,
            UIntPtr param_value_size,
            void* param_value,
            UIntPtr* param_value_size_ret);

        ///* Flush and Finish APIs */
        [DllImport("opencl.dll", EntryPoint = "clFlush", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clFlush(
            IntPtr command_queue);

        [DllImport("opencl.dll", EntryPoint = "clFinish", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clFinish(
            IntPtr command_queue);

        /* Enqueued Commands APIs */
        [DllImport("opencl.dll", EntryPoint = "clEnqueueReadBuffer", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern int clEnqueueReadBuffer(
            IntPtr command_queue,
            IntPtr buffer,
            uint blocking_read,
            UIntPtr offset,
            UIntPtr size,
            void* ptr,
            uint num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* @event);
                            
        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueReadBufferRect(cl_command_queue    /* command_queue */,
        //                        cl_mem              /* buffer */,
        //                        cl_bool             /* blocking_read */,
        //                        const UIntPtr *      /* buffer_offset */,
        //                        const UIntPtr *      /* host_offset */, 
        //                        const UIntPtr *      /* region */,
        //                        UIntPtr              /* buffer_row_pitch */,
        //                        UIntPtr              /* buffer_slice_pitch */,
        //                        UIntPtr              /* host_row_pitch */,
        //                        UIntPtr              /* host_slice_pitch */,                        
        //                        void *              /* ptr */,
        //                        uint             /* num_events_in_wait_list */,
        //                        const cl_event *    /* event_wait_list */,
        //                        cl_event *          /* event */) CL_API_SUFFIX__VERSION_1_1;
                            
        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueWriteBuffer(cl_command_queue   /* command_queue */, 
        //                     cl_mem             /* buffer */, 
        //                     cl_bool            /* blocking_write */, 
        //                     UIntPtr             /* offset */, 
        //                     UIntPtr             /* size */, 
        //                     const void *       /* ptr */, 
        //                     uint            /* num_events_in_wait_list */, 
        //                     const cl_event *   /* event_wait_list */, 
        //                     cl_event *         /* event */);
                            
        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueWriteBufferRect(cl_command_queue    /* command_queue */,
        //                         cl_mem              /* buffer */,
        //                         cl_bool             /* blocking_write */,
        //                         const UIntPtr *      /* buffer_offset */,
        //                         const UIntPtr *      /* host_offset */, 
        //                         const UIntPtr *      /* region */,
        //                         UIntPtr              /* buffer_row_pitch */,
        //                         UIntPtr              /* buffer_slice_pitch */,
        //                         UIntPtr              /* host_row_pitch */,
        //                         UIntPtr              /* host_slice_pitch */,                        
        //                         const void *        /* ptr */,
        //                         uint             /* num_events_in_wait_list */,
        //                         const cl_event *    /* event_wait_list */,
        //                         cl_event *          /* event */) CL_API_SUFFIX__VERSION_1_1;
                            
        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueFillBuffer(cl_command_queue   /* command_queue */,
        //                    cl_mem             /* buffer */, 
        //                    const void *       /* pattern */, 
        //                    UIntPtr             /* pattern_size */, 
        //                    UIntPtr             /* offset */, 
        //                    UIntPtr             /* size */, 
        //                    uint            /* num_events_in_wait_list */, 
        //                    const cl_event *   /* event_wait_list */, 
        //                    cl_event *         /* event */);
                            
        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueCopyBuffer(cl_command_queue    /* command_queue */, 
        //                    cl_mem              /* src_buffer */,
        //                    cl_mem              /* dst_buffer */, 
        //                    UIntPtr              /* src_offset */,
        //                    UIntPtr              /* dst_offset */,
        //                    UIntPtr              /* size */, 
        //                    uint             /* num_events_in_wait_list */,
        //                    const cl_event *    /* event_wait_list */,
        //                    cl_event *          /* event */);
                            
        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueCopyBufferRect(cl_command_queue    /* command_queue */, 
        //                        cl_mem              /* src_buffer */,
        //                        cl_mem              /* dst_buffer */, 
        //                        const UIntPtr *      /* src_origin */,
        //                        const UIntPtr *      /* dst_origin */,
        //                        const UIntPtr *      /* region */, 
        //                        UIntPtr              /* src_row_pitch */,
        //                        UIntPtr              /* src_slice_pitch */,
        //                        UIntPtr              /* dst_row_pitch */,
        //                        UIntPtr              /* dst_slice_pitch */,
        //                        uint             /* num_events_in_wait_list */,
        //                        const cl_event *    /* event_wait_list */,
        //                        cl_event *          /* event */) CL_API_SUFFIX__VERSION_1_1;
                            
        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueReadImage(cl_command_queue     /* command_queue */,
        //                   cl_mem               /* image */,
        //                   cl_bool              /* blocking_read */, 
        //                   const UIntPtr *       /* origin[3] */,
        //                   const UIntPtr *       /* region[3] */,
        //                   UIntPtr               /* row_pitch */,
        //                   UIntPtr               /* slice_pitch */, 
        //                   void *               /* ptr */,
        //                   uint              /* num_events_in_wait_list */,
        //                   const cl_event *     /* event_wait_list */,
        //                   cl_event *           /* event */);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueWriteImage(cl_command_queue    /* command_queue */,
        //                    cl_mem              /* image */,
        //                    cl_bool             /* blocking_write */, 
        //                    const UIntPtr *      /* origin[3] */,
        //                    const UIntPtr *      /* region[3] */,
        //                    UIntPtr              /* input_row_pitch */,
        //                    UIntPtr              /* input_slice_pitch */, 
        //                    const void *        /* ptr */,
        //                    uint             /* num_events_in_wait_list */,
        //                    const cl_event *    /* event_wait_list */,
        //                    cl_event *          /* event */);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueFillImage(cl_command_queue   /* command_queue */,
        //                   cl_mem             /* image */, 
        //                   const void *       /* fill_color */, 
        //                   const UIntPtr *     /* origin[3] */, 
        //                   const UIntPtr *     /* region[3] */, 
        //                   uint            /* num_events_in_wait_list */, 
        //                   const cl_event *   /* event_wait_list */, 
        //                   cl_event *         /* event */);
                            
        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueCopyImage(cl_command_queue     /* command_queue */,
        //                   cl_mem               /* src_image */,
        //                   cl_mem               /* dst_image */, 
        //                   const UIntPtr *       /* src_origin[3] */,
        //                   const UIntPtr *       /* dst_origin[3] */,
        //                   const UIntPtr *       /* region[3] */, 
        //                   uint              /* num_events_in_wait_list */,
        //                   const cl_event *     /* event_wait_list */,
        //                   cl_event *           /* event */);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueCopyImageToBuffer(cl_command_queue /* command_queue */,
        //                           cl_mem           /* src_image */,
        //                           cl_mem           /* dst_buffer */, 
        //                           const UIntPtr *   /* src_origin[3] */,
        //                           const UIntPtr *   /* region[3] */, 
        //                           UIntPtr           /* dst_offset */,
        //                           uint          /* num_events_in_wait_list */,
        //                           const cl_event * /* event_wait_list */,
        //                           cl_event *       /* event */);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueCopyBufferToImage(cl_command_queue /* command_queue */,
        //                           cl_mem           /* src_buffer */,
        //                           cl_mem           /* dst_image */, 
        //                           UIntPtr           /* src_offset */,
        //                           const UIntPtr *   /* dst_origin[3] */,
        //                           const UIntPtr *   /* region[3] */, 
        //                           uint          /* num_events_in_wait_list */,
        //                           const cl_event * /* event_wait_list */,
        //                           cl_event *       /* event */);

        //extern CL_API_ENTRY void * CL_API_CALL
        //clEnqueueMapBuffer(cl_command_queue /* command_queue */,
        //                   cl_mem           /* buffer */,
        //                   cl_bool          /* blocking_map */, 
        //                   cl_map_flags     /* map_flags */,
        //                   UIntPtr           /* offset */,
        //                   UIntPtr           /* size */,
        //                   uint          /* num_events_in_wait_list */,
        //                   const cl_event * /* event_wait_list */,
        //                   cl_event *       /* event */,
        //                   cl_int *         /* errcode_ret */);

        //extern CL_API_ENTRY void * CL_API_CALL
        //clEnqueueMapImage(cl_command_queue  /* command_queue */,
        //                  cl_mem            /* image */, 
        //                  cl_bool           /* blocking_map */, 
        //                  cl_map_flags      /* map_flags */, 
        //                  const UIntPtr *    /* origin[3] */,
        //                  const UIntPtr *    /* region[3] */,
        //                  UIntPtr *          /* image_row_pitch */,
        //                  UIntPtr *          /* image_slice_pitch */,
        //                  uint           /* num_events_in_wait_list */,
        //                  const cl_event *  /* event_wait_list */,
        //                  cl_event *        /* event */,
        //                  cl_int *          /* errcode_ret */);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueUnmapMemObject(cl_command_queue /* command_queue */,
        //                        cl_mem           /* memobj */,
        //                        void *           /* mapped_ptr */,
        //                        uint          /* num_events_in_wait_list */,
        //                        const cl_event *  /* event_wait_list */,
        //                        cl_event *        /* event */);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueMigrateMemObjects(cl_command_queue       /* command_queue */,
        //                           uint                /* num_mem_objects */,
        //                           const cl_mem *         /* mem_objects */,
        //                           cl_mem_migration_flags /* flags */,
        //                           uint                /* num_events_in_wait_list */,
        //                           const cl_event *       /* event_wait_list */,
        //                           cl_event *             /* event */);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueNDRangeKernel(cl_command_queue /* command_queue */,
        //                       cl_kernel        /* kernel */,
        //                       uint          /* work_dim */,
        //                       const UIntPtr *   /* global_work_offset */,
        //                       const UIntPtr *   /* global_work_size */,
        //                       const UIntPtr *   /* local_work_size */,
        //                       uint          /* num_events_in_wait_list */,
        //                       const cl_event * /* event_wait_list */,
        //                       cl_event *       /* event */);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueTask(cl_command_queue  /* command_queue */,
        //              cl_kernel         /* kernel */,
        //              uint           /* num_events_in_wait_list */,
        //              const cl_event *  /* event_wait_list */,
        //              cl_event *        /* event */);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueNativeKernel(cl_command_queue  /* command_queue */,
        //                      void (CL_CALLBACK * /*user_func*/)(void *), 
        //                      void *            /* args */,
        //                      UIntPtr            /* cb_args */, 
        //                      uint           /* num_mem_objects */,
        //                      const cl_mem *    /* mem_list */,
        //                      const void **     /* args_mem_loc */,
        //                      uint           /* num_events_in_wait_list */,
        //                      const cl_event *  /* event_wait_list */,
        //                      cl_event *        /* event */);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueMarkerWithWaitList(cl_command_queue /* command_queue */,
        //                            uint           /* num_events_in_wait_list */,
        //                            const cl_event *  /* event_wait_list */,
        //                            cl_event *        /* event */);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueBarrierWithWaitList(cl_command_queue /* command_queue */,
        //                             uint           /* num_events_in_wait_list */,
        //                             const cl_event *  /* event_wait_list */,
        //                             cl_event *        /* event */);


        /* Extension function access
         *
         * Returns the extension function address for the given function name,
         * or NULL if a valid function can not be found.  The client must
         * check to make sure the address is not NULL, before using or 
         * calling the returned function address.
         */       

        [DllImport("opencl.dll", EntryPoint = "clGetExtensionFunctionAddressForPlatform", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity()]
        public static unsafe extern void* clGetExtensionFunctionAddressForPlatform(
            IntPtr platform,
            byte* func_name);

        //// Deprecated OpenCL 1.1 APIs
        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED cl_mem CL_API_CALL
        //clCreateImage2D(cl_context              /* context */,
        //                cl_mem_flags            /* flags */,
        //                const cl_image_format * /* image_format */,
        //                UIntPtr                  /* image_width */,
        //                UIntPtr                  /* image_height */,
        //                UIntPtr                  /* image_row_pitch */, 
        //                void *                  /* host_ptr */,
        //                cl_int *                /* errcode_ret */) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;
    
        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED cl_mem CL_API_CALL
        //clCreateImage3D(cl_context              /* context */,
        //                cl_mem_flags            /* flags */,
        //                const cl_image_format * /* image_format */,
        //                UIntPtr                  /* image_width */, 
        //                UIntPtr                  /* image_height */,
        //                UIntPtr                  /* image_depth */, 
        //                UIntPtr                  /* image_row_pitch */, 
        //                UIntPtr                  /* image_slice_pitch */, 
        //                void *                  /* host_ptr */,
        //                cl_int *                /* errcode_ret */) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;
    
        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED cl_int CL_API_CALL
        //clEnqueueMarker(cl_command_queue    /* command_queue */,
        //                cl_event *          /* event */) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;
    
        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED cl_int CL_API_CALL
        //clEnqueueWaitForEvents(cl_command_queue /* command_queue */,
        //                        uint          /* num_events */,
        //                        const cl_event * /* event_list */) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;
    
        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED cl_int CL_API_CALL
        //clEnqueueBarrier(cl_command_queue /* command_queue */) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;

        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED cl_int CL_API_CALL
        //clUnloadCompiler(void) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;
    
        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED void * CL_API_CALL
        //clGetExtensionFunctionAddress(const char * /* func_name */) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;
    }
}
