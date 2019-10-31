using System;
using Ibasa.Noise;
using Ibasa.Numerics;

namespace Dyna
{
    public static class Dyna
    {
        public static Module<Color> Func()
        {
            var perlin = new Ibasa.Noise.Perlin(0);
            var fbm = new Ibasa.Noise.Fbm(source: perlin,
                                                                octaves: 6, persistance: 0.5, frequency: 0.5,
                                                                lacunarity: 1.0);
            var ridged = new Ibasa.Noise.RidgedMulti(source: perlin,
                                                                  octaves: 6, frequency: 1.0, lacunarity: 2.0,
                                                                    offset: 1.0, gain: 2.0, sharpness: 5.5);
            var exp = new Ibasa.Noise.Exponent(fbm, 0.6);
            var billow = new Ibasa.Noise.Billow(source: perlin);
            var billowScale = new Ibasa.Noise.ScaleBias(billow, 0.35);
            var mult = new Ibasa.Noise.Map<double, double, double>(exp, ridged, (d1, d2) => d1 * d2);
            var multScale = new Ibasa.Noise.ScaleBias(mult, 4.0, -2.0);
            var select = new Ibasa.Noise.Select(exp, billowScale, multScale, -1.0, 1.0, 0.75);
            var check = new Checkerboard();
            var scale = new Ibasa.Noise.ScalePoint<double>(select, 0.01f, 0.01f);
            var rotate = new Ibasa.Noise.RotatePoint<double>(scale, Math.PI / 1.5);
            var bias = new Ibasa.Noise.ScaleBias(rotate, 50.0, -25.0);
            Ibasa.Noise.Module<Color> grey = new Map<double,Color>(bias, (d) => new Color(d, d, d));
            return grey;
        }
    }
}