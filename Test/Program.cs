using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Ibasa.Audio.OpenAL.Version);

            foreach (var dev in Ibasa.Audio.OpenAL.Devices)
            {
                Console.WriteLine("------------");
                Console.WriteLine("Name: {0}", dev.Name);
                Console.WriteLine("Capture Samples: {0}", dev.CaptureSamples);
                Console.WriteLine("Frequency: {0}", dev.Frequency);
                Console.WriteLine("Refresh: {0}", dev.Refresh);
                Console.WriteLine("Mono Sources: {0}", dev.MonoSources);
                Console.WriteLine("Stereo Sources: {0}", dev.StereoSources);
                Console.WriteLine("Sync: {0}", dev.Sync);
                Console.WriteLine(string.Join(" ", dev.UnknownAttributes));
                Console.WriteLine(string.Join(" ", dev.Extensions));
                Console.WriteLine(dev.EfxVersion);

                dev.Dispose();
            }

            var device = Ibasa.Audio.OpenAL.DefaultDevice;

            Console.WriteLine("------------");
            Console.WriteLine(device.Name);
            Console.WriteLine("------------");

            var context = new Ibasa.Audio.Context(device);
            Ibasa.Audio.Context.MakeContextCurrent(context);

            Console.WriteLine("Version: {0}", Ibasa.Audio.Context.Version);
            Console.WriteLine("Vendor: {0}", Ibasa.Audio.Context.Vendor);
            Console.WriteLine("Renderer: {0}", Ibasa.Audio.Context.Renderer);
            Console.WriteLine("Extensions: {0}", Ibasa.Audio.Context.Extensions);
            Console.WriteLine("Doppler Factor: {0}", Ibasa.Audio.Context.DopplerFactor);
            Console.WriteLine("Speed Of Sound: {0}", Ibasa.Audio.Context.SpeedOfSound);

            Console.ReadLine();
        }
    }
}
