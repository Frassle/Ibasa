using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.IO;

namespace Test
{
    class Program
    {
        static void Reduce(byte[] samples)
        {
            for (int i = 0; i < samples.Length; ++i)
            {
                samples[i] = (byte)(samples[i] * 0.5);
            }
        }

        static byte[] SinWave(int sample_frequency, int time, int frequency)
        {
            var samples = sample_frequency * time;
            var data = new byte[samples];
            for (int i = 0; i < data.Length; ++i)
            {
                var t = (double)i / sample_frequency;

                var period = 1.0 / frequency;
                var theta = ((2 * Math.PI) / period) * t;
                var y = Math.Sin(theta);

                var scalebias = (y + 1) / 2;
                var sample = (byte)(255 * scalebias);

                data[i] = sample;
            }
            return data;
        }

        static byte[] Interleave(byte[] fl, byte[] fr, byte[] rl, byte[] rr)
        {
            byte[] interleaved = new byte[fl.Length * 4];

            int i;
            for (i = 0; i < fl.Length; ++i)
            {
                interleaved[i * 4] = fl[i];
                interleaved[i * 4 + 1] = fr[i];
                interleaved[i * 4 + 2] = rl[i];
                interleaved[i * 4 + 3] = rr[i];
            }

            return interleaved;
        }

        static byte[] Interleave(byte[] left, byte[] right)
        {
            byte[] interleaved = new byte[left.Length + right.Length];

            int i;
            for (i = 0; i < Math.Min(left.Length, right.Length); ++i)
            {
                interleaved[i * 2] = left[i];
                interleaved[i * 2 + 1] = right[i];
            }

            byte[] tail = left.Length < right.Length ? right : left;

            for (; i < tail.Length; ++i)
            {
                interleaved[i * 2] = tail[i];
                interleaved[i * 2 + 1] = tail[i];
            }

            return interleaved;
        }

        static void Main(string[] args)
        {
            var source_sounds = new Ibasa.Valve.Package.Gcf(@"D:\Steam\steamapps\source sounds.gcf", System.IO.FileShare.ReadWrite);
            var half_life = new Ibasa.Valve.Package.Gcf(@"D:\Steam\steamapps\half-life.gcf", System.IO.FileShare.ReadWrite);

            var natural_selection = new Ibasa.Packaging.FileSystemPackage(@"D:\Steam\steamapps\frassle@hotmail.com\half-life\ns");


            Console.WriteLine(Ibasa.Audio.OpenAL.Version);

            foreach (var dev in Ibasa.Audio.OpenAL.Devices)
            {
                Console.WriteLine("------------");
                Console.WriteLine("Name: {0}", dev.Name);
                var attributes = dev.Attributes;

                Console.WriteLine("Frequency: {0}", attributes.Frequency);
                Console.WriteLine("Refresh: {0}", attributes.Refresh);
                Console.WriteLine("Mono Sources: {0}", attributes.MonoSources);
                Console.WriteLine("Stereo Sources: {0}", attributes.StereoSources);
                Console.WriteLine("Sync: {0}", attributes.Sync);
                Console.WriteLine(string.Join(", ", attributes.UnknownAttributes));
                Console.WriteLine(string.Join(", ", dev.Extensions));
                Console.WriteLine(dev.EfxVersion);

                dev.Close();
            }

            Console.ReadLine();

            var device = Ibasa.Audio.OpenAL.DefaultDevice;
            {
                Console.WriteLine("------------");
                Console.WriteLine("Name: {0}", device.Name);
                var attributes = device.Attributes;

                Console.WriteLine("Frequency: {0}", attributes.Frequency);
                Console.WriteLine("Refresh: {0}", attributes.Refresh);
                Console.WriteLine("Mono Sources: {0}", attributes.MonoSources);
                Console.WriteLine("Stereo Sources: {0}", attributes.StereoSources);
                Console.WriteLine("Sync: {0}", attributes.Sync);
                Console.WriteLine(string.Join(", ", attributes.UnknownAttributes));
                Console.WriteLine(string.Join(", ", device.Extensions));
                Console.WriteLine(device.EfxVersion);
            }

            var context = Ibasa.Audio.Context.Create(device);
            Ibasa.Audio.Context.MakeContextCurrent(context);

            Console.WriteLine("Version: {0}", Ibasa.Audio.Context.Version);
            Console.WriteLine("Vendor: {0}", Ibasa.Audio.Context.Vendor);
            Console.WriteLine("Renderer: {0}", Ibasa.Audio.Context.Renderer);
            Console.WriteLine("Extensions: {0}", Ibasa.Audio.Context.Extensions);
            Console.WriteLine("Doppler Factor: {0}", Ibasa.Audio.Context.DopplerFactor);
            Console.WriteLine("Speed Of Sound: {0}", Ibasa.Audio.Context.SpeedOfSound);

            Console.WriteLine("------------");

            Console.WriteLine("Position: {0}", Ibasa.Audio.Listener.Position);
            Console.WriteLine("Orientation: {0}", Ibasa.Audio.Listener.Orientation);
            Console.WriteLine("Velocity: {0}", Ibasa.Audio.Listener.Velocity);
            Console.WriteLine("Gain: {0}", Ibasa.Audio.Listener.Gain);
            Console.WriteLine("EfxMetersPerUnit: {0}", Ibasa.Audio.Listener.EfxMetersPerUnit);

            Ibasa.Audio.Source source = Ibasa.Audio.Source.Gen();
            source.Gain = 1;
            source.Looping = false;

            Ibasa.Audio.Buffer buffer = Ibasa.Audio.Buffer.Gen();

            foreach (var item in source_sounds.Root.EnumerateFiles(".*\\.wav", System.IO.SearchOption.AllDirectories))
            {
                var wav = new Ibasa.Media.Audio.Wav(item.OpenRead());

                Console.WriteLine(item.Name);
                Console.WriteLine("Format   : {0}", wav.Format);
                Console.WriteLine("Frequency: {0}", wav.Frequency);

                wav.Stream.Seek(wav.DataOffset, System.IO.SeekOrigin.Begin);
                var data = wav.Stream.ReadBytes(wav.DataLength);

                buffer.BufferData(wav.Format, data, data.Length, wav.Frequency);

                source.Buffer = buffer;
                source.Play();

                while (source.State == Ibasa.Audio.SourceState.Playing && !Console.KeyAvailable)
                {
                    System.Threading.Thread.Sleep(0);
                }

                if (Console.KeyAvailable)
                    Console.ReadKey(true);

                source.Stop();
                source.Buffer = Ibasa.Audio.Buffer.Null;
            }

            buffer.Delete();
            source.Delete();
            Ibasa.Audio.Context.Destroy(context);
            device.Close();

            Console.ReadLine();
        }
    }
}
