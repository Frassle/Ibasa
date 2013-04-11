using Ibasa.OpenCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static string source = @"
__kernel void vector_add_gpu (__global const float* src_a,
    __global const float* src_b,
    __global float* res,
    const int num)
{
    const int idx = get_global_id(0);
    if (idx < num)
        res[idx] = src_a[idx] + src_b[idx];
}";

        static void ContextCallback(string error, byte[] info, object user_data)
        {
            Console.WriteLine("Error: {0}", error);
        }

        static int flag = 0;
        static void ProgramCallback(Ibasa.OpenCL.Program program, object user_data)
        {
            Console.WriteLine("Program built.");

            var device = program.Devices[0];

            Console.WriteLine(device.Name);

            var info = program.GetBuildInfo(device);

            Console.WriteLine(info.BuildOptions);
            Console.WriteLine(info.BuildStatus);
            Console.WriteLine(info.Log);

            flag = 1;
        }

        static void Main(string[] args)
        {
            var platform = Platform.GetPlatforms()[0];
            var device = platform.GetDevices(DeviceType.All)[0];

            Console.WriteLine(platform.Name);
            Console.WriteLine(platform.Vendor);
            Console.WriteLine(platform.Profile);
            Console.WriteLine(platform.Version);
            Console.WriteLine(string.Join(", ", platform.Extensions));

            Console.WriteLine(device.Name);
            Console.WriteLine(device.Profile);
            Console.WriteLine(device.Vendor);
            Console.WriteLine(device.VendorID);
            Console.WriteLine(device.Version);
            Console.WriteLine(device.DriverVersion);
            Console.WriteLine(device.DoubleFloatingPointCapability);
            Console.WriteLine(device.SingleFloatingPointCapability);
            Console.WriteLine(device.HalfFloatingPointCapability);
            Console.WriteLine(device.Type);

            var context = new Context(null, new Device[] { device }, ContextCallback, null);

            var program = new Ibasa.OpenCL.Program(context, source);
            program.BuildProgram(new Device[] { device }, "", ProgramCallback, null);

            Console.WriteLine(program.Source);

            while (flag == 0)
            {
            }

            var kernel = new Kernel(program, "vector_add_gpu");

            Console.WriteLine(kernel.FunctionName);
            Console.WriteLine(kernel.ArgumentCount);

            uint mem_size = 50 * sizeof(float);

            float[] srca = new float[50];
            float[] srcb = new float[50];
            float[] dest = new float[50];

            for (int i = 0; i < 50; ++i)
            {
                srca[i] = i;
                srcb[i] = 50;
            }

            var srca_ptr = GCHandle.Alloc(srca, GCHandleType.Pinned);
            var srcb_ptr = GCHandle.Alloc(srcb, GCHandleType.Pinned);
            var dest_ptr = GCHandle.Alloc(dest, GCHandleType.Pinned);

            var buffera = new Ibasa.OpenCL.Buffer(
                context, MemoryFlags.ReadOnly | MemoryFlags.CopyHostPtr, mem_size, srca_ptr.AddrOfPinnedObject());
            var bufferb = new Ibasa.OpenCL.Buffer(
                context, MemoryFlags.ReadOnly | MemoryFlags.CopyHostPtr, mem_size, srcb_ptr.AddrOfPinnedObject());
            var bufferd = new Ibasa.OpenCL.Buffer(
                context, MemoryFlags.WriteOnly, mem_size);

            kernel.SetArgument(0, buffera);
            kernel.SetArgument(1, bufferb);
            kernel.SetArgument(2, bufferd);
            kernel.SetArgument(3, 50);

            var queue = new CommandQueue(context, device, CommandQueueProperties.None);

            var eventk = queue.EnqueueKernel(kernel, null, new ulong[] { 50 }, null, null);

            queue.EnqueueReadBuffer(bufferd, true, 0, mem_size, dest_ptr.AddrOfPinnedObject(), new Event[] { eventk });

            for (int i = 0; i < 50; ++i)
            {
                Console.WriteLine(dest[i]);
            }

            Console.ReadLine();
        }
    }
}
