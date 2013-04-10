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
}