using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise.Sounds
{
    public sealed class TriangleWave : Module<double>
    {
        public double Frequency { get; set; }
        public double Phase { get; set; }
        public double Amplitude { get; set; }

        public TriangleWave(double frequency, double phase, double amplitude)
        {
            Frequency = frequency;
            Phase = phase;
            Amplitude = amplitude;
        }

        public override double Evaluate(double t)
        {
            return Amplitude * Functions.Abs((Frequency * t + Phase) - Functions.Floor(Frequency * t + Phase));
        }
    }
}
