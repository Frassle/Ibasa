using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                Console.WriteLine(string.Join(", ", dev.UnknownAttributes));
                Console.WriteLine(string.Join(", ", dev.Extensions));
                Console.WriteLine(dev.EfxVersion);

                dev.Dispose();
            }

            var device = Ibasa.Audio.OpenAL.DefaultDevice;

            Console.WriteLine("------------");
            Console.WriteLine(device.Name);
            Console.WriteLine("------------");

            Ibasa.Audio.Context.Create(device);

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

            Ibasa.Audio.Source source = new Ibasa.Audio.Source();
            source.Gain = 1;

            var sample_frequency = 44100;
            var time = 1;

            var fl = SinWave(sample_frequency, time, 100);
            var fr = SinWave(sample_frequency, time, 200);
            var rl = SinWave(sample_frequency, time, 300);
            var rr = SinWave(sample_frequency, time, 400);

            var data = Interleave(fl, fr, rl, rr);

            Ibasa.Audio.Buffer buffer1 = new Ibasa.Audio.Buffer();
            buffer1.BufferData(new Ibasa.SharpAL.Formats.PCM8(1), data, data.Length, sample_frequency);

            Ibasa.Audio.Buffer buffer2 = new Ibasa.Audio.Buffer();
            buffer2.BufferData(new Ibasa.SharpAL.Formats.PCM8(1), data, data.Length, sample_frequency);

            source.Queue(buffer1);
            source.Queue(buffer2);
            source.Play();
            source.Gain = 2;

            source.Position = new Ibasa.Numerics.Geometry.Point3f(0, 0, 5);

            Console.WriteLine("Playing");
            for (int step = 1; step < 360; ++step)
            {
                while (source.BuffersProcessed == 0)
                {
                    System.Threading.Thread.Sleep(1);
                }

                var buffer = source.Unqueue();
                buffer.BufferData(new Ibasa.SharpAL.Formats.PCM8(1), data, data.Length, sample_frequency);
                source.Queue(buffer);

                var x = Ibasa.Numerics.Functions.Sin((float)Ibasa.Numerics.Functions.ToRadians(step) * 24);
                var y = Ibasa.Numerics.Functions.Cos((float)Ibasa.Numerics.Functions.ToRadians(step) * 24);

                x *= 5;
                y *= 5;

                var position = new Ibasa.Numerics.Geometry.Point3f(x, 0, y);
                Console.WriteLine(position);
                source.Position = position;
            }

            while (source.BuffersProcessed != 2)
            {
                System.Threading.Thread.Sleep(1);
            }

            source.Unqueue(2);

            buffer1.Delete();
            buffer2.Delete();
            source.Delete();
            Ibasa.Audio.Context.Destroy();

            Console.ReadLine();
        }
    }
}
