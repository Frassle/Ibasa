using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise.Sounds
{
    public sealed class SineWave : Module<double>
    {
        public double Frequency { get; set; }
        public double Phase { get; set; }
        public double Amplitude { get; set; }

        public SineWave(double frequency, double phase, double amplitude)
        {
            Frequency = frequency;
            Phase = phase;
            Amplitude = amplitude;
        }

        public override double Evaluate(double t)
        {
            return Amplitude * Functions.Sin(Frequency * t + Phase);
        }
    }
}
