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
        Default = OpenCL.CL_DEVICE_TYPE_DEFAULT,
        Cpu = OpenCL.CL_DEVICE_TYPE_CPU,
        Gpu = OpenCL.CL_DEVICE_TYPE_GPU,
        Accelerator =OpenCL.CL_DEVICE_TYPE_ACCELERATOR,
        Custom = OpenCL.CL_DEVICE_TYPE_CUSTOM,
        All = OpenCL.CL_DEVICE_TYPE_ALL,
    }

    [Flags]
    public enum CommandQueueProperties
    {
        None = 0,
        OutOfOrderExecModeEnable = OpenCL.CL_QUEUE_OUT_OF_ORDER_EXEC_MODE_ENABLE,
        ProfilingEnable = OpenCL.CL_QUEUE_PROFILING_ENABLE,
    }
}