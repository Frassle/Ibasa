﻿using Ibasa.OpenCL;
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

        static void ProgramCallback(Ibasa.OpenCL.Program program, object user_data)
        {
            Console.WriteLine("Program built.");

            var device = program.Devices[0];

            Console.WriteLine(device.Name);

            var info = program.GetBuildInfo(device);

            Console.WriteLine(info.BuildOptions);
            Console.WriteLine(info.BuildStatus);
            Console.WriteLine(info.Log);
        }

        struct Pair { public int A; public int B; }
        
        static void Main(string[] args)
        {
            unsafe
            {
                long longs = 1337;
                int* ints = stackalloc int[2];

                Ibasa.Interop.Memory.Copy(&longs, ints, 8);

                Pair pair = new Pair();
                Ibasa.Interop.Memory.Read(ints, out pair);

                Console.WriteLine("{0}:{1}", ints[0], pair.A);
                Console.WriteLine("{0}:{1}", ints[1], pair.B);

                pair.A = 8008;
                pair.B = 1337;

                Ibasa.Interop.Memory.Write(ints, ref pair);

                Console.WriteLine("{0}:{1}", ints[0], pair.A);
                Console.WriteLine("{0}:{1}", ints[1], pair.B);

                Console.WriteLine("Sizeof(Pair) = {0}", Ibasa.Interop.Memory.SizeOf<Pair>());
            }

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
            Console.WriteLine(device.Type);

            var context = new Context(null, new Device[] { device }, ContextCallback, null);

            var program = new Ibasa.OpenCL.Program(context, source);
            program.BuildProgram(new Device[] { device }, "", ProgramCallback, null);

            Console.WriteLine(program.Source);

            while (program.GetBuildInfo(device).BuildStatus == BuildStatus.InProgress)
            {
            }

            var kernel = new Kernel(program, "vector_add_gpu");

            Console.WriteLine(kernel.FunctionName);
            Console.WriteLine(kernel.ArgumentCount);

            Ibasa.Interop.UnmanagedArray<float> srca = new Ibasa.Interop.UnmanagedArray<float>(50);
            Ibasa.Interop.UnmanagedArray<float> srcb = new Ibasa.Interop.UnmanagedArray<float>(50);
            Ibasa.Interop.UnmanagedArray<float> dest = new Ibasa.Interop.UnmanagedArray<float>(50);

            for (int i = 0; i < 50; ++i)
            {
                srca[i] = i;
                srcb[i] = 50;
            }

            var buffera = new Ibasa.OpenCL.Buffer(
                context, MemoryFlags.ReadOnly | MemoryFlags.CopyHostPtr, srca.Size, srca.Pointer);
            var bufferb = new Ibasa.OpenCL.Buffer(
                context, MemoryFlags.ReadOnly | MemoryFlags.CopyHostPtr, srcb.Size, srcb.Pointer);
            var bufferd = new Ibasa.OpenCL.Buffer(
                context, MemoryFlags.WriteOnly, dest.Size);

            kernel.SetArgument(0, buffera);
            kernel.SetArgument(1, bufferb);
            kernel.SetArgument(2, bufferd);
            kernel.SetArgument(3, 50);

            var queue = new CommandQueue(context, device, CommandQueueProperties.ProfilingEnable);

            var eventk = queue.EnqueueKernel(kernel, null, new ulong[] { 50 }, null, null);

            eventk.SetCallback((eve, status, obj) => Console.WriteLine(status), null);
            
            queue.EnqueueReadBuffer(bufferd, true, 0, dest.Size, dest.Pointer, new Event[] { eventk });
            
            for (int i = 0; i < 50; ++i)
            {
                Console.WriteLine(dest[i]);
            }

            var time = eventk.TimeEnded - eventk.TimeStarted;

            Console.WriteLine("Took {0}", time);

            Console.ReadLine();
        }
    }
}
