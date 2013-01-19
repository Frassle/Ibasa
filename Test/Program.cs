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

        static void Extension(string name)
        {
            Console.WriteLine("{0}: {1}", name, Ibasa.Audio.OpenAL.Device.IsContextExtensionPresent(name));
        }

        static void Main(string[] args)
        {
            var source_sounds = new Ibasa.Valve.Package.Gcf(@"D:\Steam\steamapps\source sounds.gcf", System.IO.FileShare.ReadWrite);
            var half_life = new Ibasa.Valve.Package.Gcf(@"D:\Steam\steamapps\half-life.gcf", System.IO.FileShare.ReadWrite);

            var natural_selection = new Ibasa.Packaging.FileSystemPackage(@"D:\Steam\steamapps\frassle@hotmail.com\half-life\ns");

            Extension("ALC_ENUMERATION_EXT");
            Extension("ALC_EXT_CAPTURE");
            Extension("AL_EXT_MP3");
            Extension("EAX2.0");
            Extension("EAX3.0");
            Extension("EAX4.0");
            Extension("EAX5.0");
            Extension("ALC_EXT_EFX");
            Extension("EAX_RAM");

            Console.ReadLine();

            foreach (var dev in Ibasa.Audio.OpenAL.Device.Devices)
            {
                Console.WriteLine("------------");
                Console.WriteLine("Name: {0}", dev.Name);
                Console.WriteLine("Version: {0}", dev.Version);
                var attributes = dev.Attributes;

                Console.WriteLine("Frequency: {0}", attributes.Frequency);
                Console.WriteLine("Refresh: {0}", attributes.Refresh);
                Console.WriteLine("Mono Sources: {0}", attributes.MonoSources);
                Console.WriteLine("Stereo Sources: {0}", attributes.StereoSources);
                Console.WriteLine("Sync: {0}", attributes.Sync);
                Console.WriteLine(string.Join(", ", attributes.UnknownAttributes));
                Console.WriteLine(string.Join(", ", dev.Extensions));
                Console.WriteLine(string.Join(", ", dev.Extensions.Select(ex => dev.IsExtensionPresent(ex))));

                dev.Close();
            }

            Console.ReadLine();

            var device = Ibasa.Audio.OpenAL.Device.DefaultDevice;
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
            }

            var context = new Ibasa.Audio.OpenAL.Context(device);
            Ibasa.Audio.OpenAL.Context.MakeContextCurrent(context);

            Console.WriteLine("Version: {0}", Ibasa.Audio.OpenAL.Context.Version);
            Console.WriteLine("Vendor: {0}", Ibasa.Audio.OpenAL.Context.Vendor);
            Console.WriteLine("Renderer: {0}", Ibasa.Audio.OpenAL.Context.Renderer);
            Console.WriteLine("Extensions: {0}", Ibasa.Audio.OpenAL.Context.Extensions);
            Console.WriteLine("Doppler Factor: {0}", Ibasa.Audio.OpenAL.Context.DopplerFactor);
            Console.WriteLine("Speed Of Sound: {0}", Ibasa.Audio.OpenAL.Context.SpeedOfSound);

            Console.WriteLine("------------");

            Console.WriteLine("Position: {0}", Ibasa.Audio.OpenAL.Listener.Position);
            Console.WriteLine("Orientation: {0}", Ibasa.Audio.OpenAL.Listener.Orientation);
            Console.WriteLine("Velocity: {0}", Ibasa.Audio.OpenAL.Listener.Velocity);
            Console.WriteLine("Gain: {0}", Ibasa.Audio.OpenAL.Listener.Gain);
            Console.WriteLine("EfxMetersPerUnit: {0}", Ibasa.Audio.OpenAL.Listener.EfxMetersPerUnit);

            Ibasa.Audio.OpenAL.Source source = Ibasa.Audio.OpenAL.Source.Gen();
            source.Gain = 1;
            source.Looping = false;

            Ibasa.Audio.OpenAL.Buffer buffer = Ibasa.Audio.OpenAL.Buffer.Gen();

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

                while (source.State == Ibasa.Audio.OpenAL.SourceState.Playing && !Console.KeyAvailable)
                {
                    System.Threading.Thread.Sleep(0);
                }

                if (Console.KeyAvailable)
                    Console.ReadKey(true);

                source.Stop();
                source.Buffer = Ibasa.Audio.OpenAL.Buffer.Null;
            }

            buffer.Delete();
            source.Delete();
            Ibasa.Audio.OpenAL.Context.Destroy(context);
            device.Close();

            Console.ReadLine();
        }
    }
}
