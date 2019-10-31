using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;

namespace Ibasa.Noise
{
    public sealed class Voronoi : Module<double>
    {
        public double Displacement { get; set; }
        public double Frequency { get; set; }
        public bool EnableDistance { get; set; }
        public readonly int Seed;

        public Voronoi(int seed, double displacement = 1.0, double frequency = 1.0, bool enableDistance = false)
        {
            Seed = seed;
            Displacement = displacement;
            Frequency = frequency;
            EnableDistance = enableDistance;
        }
        public Voronoi(double displacement = 1.0, double frequency = 1.0, bool enableDistance = false)
        {
            Seed = (int)DateTime.Now.Ticks;
            Displacement = displacement;
            Frequency = frequency;
            EnableDistance = enableDistance;
        }

        static long Hash(long x)
        {
            ulong v = (ulong)x * 3935559000370003845 + 2691343689449507681;
            v ^= v >> 21; v ^= v << 31; v ^= v >> 4;
            v *= 4768777513237032717;
            v ^= v << 20; v ^= v >> 41; v ^= v << 5;
            return (long)v & long.MaxValue;
        }
        static double HashDouble(long x)
        {
            return (1.0 / long.MaxValue) * Hash(x);
        }

        double ValueNoise(int x)
        {
            long n = Hash(x) + Seed;
            return 1.0 - 2.0 * HashDouble(n);
        }
        double ValueNoise(int x, int y)
        {
            long n =
                Hash(x) +
                Hash(y) + Seed;
            return 1.0 - 2.0 * HashDouble(n);
        }
        double ValueNoise(int x, int y, int z)
        {
            long n =
                Hash(x) +
                Hash(y) +
                Hash(z) + Seed;
            return 1.0 - 2.0 * HashDouble(n);
        }
        double ValueNoise(int x, int y, int z, int w)
        {
            long n =
                Hash(x) +
                Hash(y) +
                Hash(z) +
                Hash(w) + Seed;
            return 1.0 - 2.0 * HashDouble(n);
        }
        double ValueNoise(int x, int y, int z, int w, int v, int u)
        {
            long n =
                   Hash(x) +
                   Hash(y) +
                   Hash(z) +
                   Hash(w) +
                   Hash(v) +
                   Hash(u) + Seed;
            return 1.0 - 2.0 * HashDouble(n);
        }

        public override double Evaluate(double x)
        {
            x *= Frequency;

            int ix = (int)Functions.Floor(x);

            double minDist = double.MaxValue;
            double xPoint = 0;

            // Inside each unit line, there is a seed point at a random position.  Go
            // through each of the nearby line until we find a cube with a seed point
            // that is closest to the specified position.
            for (int xCur = ix - 2; xCur <= ix + 2; ++xCur)
            {
                // Calculate the position and distance to the seed point inside of
                // this unit line.
                double xPos = xCur + ValueNoise(xCur);
                double xDist = xPos - x;
                double dist = xDist * xDist;

                if (dist < minDist)
                {
                    //This seed point is closer to any others found so far, so record it
                    minDist = dist;
                    xPoint = xPos;
                }

            }

            double distance = 0.0;
            if (EnableDistance) // Determine the distance to the nearest seed point.
                distance = Functions.Sqrt(minDist) - 1.0;

            return distance + (Displacement * ValueNoise((int)Functions.Floor(xPoint)));
        }

        public override double Evaluate(double x, double y)
        {
            x *= Frequency;
            y *= Frequency;

            int ix = (int)Functions.Floor(x);
            int iy = (int)Functions.Floor(y);

            double minDist = double.MaxValue;
            double xPoint = 0;
            double yPoint = 0;

            // Inside each unit square, there is a seed point at a random position.  Go
            // through each of the nearby squares until we find a square with a seed point
            // that is closest to the specified position.
            for (int yCur = iy - 2; yCur <= iy + 2; ++yCur)
            {
                for (int xCur = ix - 2; xCur <= ix + 2; ++xCur)
                {
                    // Calculate the position and distance to the seed point inside of
                    // this unit square.
                    double xPos = xCur + ValueNoise(xCur, yCur);
                    double yPos = yCur + ValueNoise(xCur, yCur);
                    double xDist = xPos - x;
                    double yDist = yPos - y;
                    double dist = xDist * xDist + yDist * yDist;

                    if (dist < minDist)
                    {
                        //This seed point is closer to any others found so far, so record it
                        minDist = dist;
                        xPoint = xPos;
                        yPoint = yPos;
                    }
                }
            }

            double distance = 0.0;
            if (EnableDistance) // Determine the distance to the nearest seed point.
                distance = Functions.Sqrt(minDist) * Functions.Sqrt(2) - 1.0;

            return distance + (Displacement * ValueNoise(
                (int)Functions.Floor(xPoint),
                (int)Functions.Floor(yPoint)));
        }

        public override double Evaluate(double x, double y, double z)
        {
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;

            int ix = (int)Functions.Floor(x);
            int iy = (int)Functions.Floor(y);
            int iz = (int)Functions.Floor(z);

            double minDist = double.MaxValue;
            double xPoint = 0;
            double yPoint = 0;
            double zPoint = 0;

            // Inside each unit cube, there is a seed point at a random position.  Go
            // through each of the nearby cubes until we find a cube with a seed point
            // that is closest to the specified position.
            for (int zCur = iz - 2; zCur <= iz + 2; ++zCur)
            {
                for (int yCur = iy - 2; yCur <= iy + 2; ++yCur)
                {
                    for (int xCur = ix - 2; xCur <= ix + 2; ++xCur)
                    {
                        // Calculate the position and distance to the seed point inside of
                        // this unit cube.
                        double xPos = xCur + ValueNoise(xCur, yCur, zCur);
                        double yPos = yCur + ValueNoise(xCur, yCur, zCur);
                        double zPos = zCur + ValueNoise(xCur, yCur, zCur);
                        double xDist = xPos - x;
                        double yDist = yPos - y;
                        double zDist = zPos - z;
                        double dist = xDist * xDist + yDist * yDist + zDist * zDist;

                        if (dist < minDist)
                        {
                            //This seed point is closer to any others found so far, so record it
                            minDist = dist;
                            xPoint = xPos;
                            yPoint = yPos;
                            zPoint = zPos;
                        }
                    }
                }
            }

            double distance = 0.0;
            if (EnableDistance) // Determine the distance to the nearest seed point.
                distance = Functions.Sqrt(minDist) * Functions.Sqrt(3) - 1.0;

            return distance + (Displacement * ValueNoise(
                (int)Functions.Floor(xPoint),
                (int)Functions.Floor(yPoint),
                (int)Functions.Floor(zPoint)));
        }

        public override double Evaluate(double x, double y, double z, double w)
        {
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;
            w *= Frequency;

            int ix = (int)Functions.Floor(x);
            int iy = (int)Functions.Floor(y);
            int iz = (int)Functions.Floor(z);
            int iw = (int)Functions.Floor(w);

            double minDist = double.MaxValue;
            double xPoint = 0;
            double yPoint = 0;
            double zPoint = 0;
            double wPoint = 0;

            // Inside each unit hypercube, there is a seed point at a random position.  Go
            // through each of the nearby cubes until we find a cube with a seed point
            // that is closest to the specified position.
            for (int wCur = iw - 2; wCur <= iw + 2; ++wCur)
            {
                for (int zCur = iz - 2; zCur <= iz + 2; ++zCur)
                {
                    for (int yCur = iy - 2; yCur <= iy + 2; ++yCur)
                    {
                        for (int xCur = ix - 2; xCur <= ix + 2; ++xCur)
                        {
                            // Calculate the position and distance to the seed point inside of
                            // this unit hypercube.
                            double xPos = xCur + ValueNoise(xCur, yCur, zCur, wCur);
                            double yPos = yCur + ValueNoise(xCur, yCur, zCur, wCur);
                            double zPos = zCur + ValueNoise(xCur, yCur, zCur, wCur);
                            double wPos = wCur + ValueNoise(xCur, yCur, zCur, wCur);
                            double xDist = xPos - x;
                            double yDist = yPos - y;
                            double zDist = zPos - z;
                            double wDist = wPos - w;
                            double dist = xDist * xDist + yDist * yDist + zDist * zDist + wDist * wDist;

                            if (dist < minDist)
                            {
                                //This seed point is closer to any others found so far, so record it
                                minDist = dist;
                                xPoint = xPos;
                                yPoint = yPos;
                                zPoint = zPos;
                                wPoint = wPos;
                            }
                        }
                    }
                }
            }

            double distance = 0.0;
            if (EnableDistance) // Determine the distance to the nearest seed point.
                distance = Functions.Sqrt(minDist) * Functions.Sqrt(4) - 1.0;

            return distance + (Displacement * ValueNoise(
                (int)Functions.Floor(xPoint),
                (int)Functions.Floor(yPoint),
                (int)Functions.Floor(zPoint),
                (int)Functions.Floor(wPoint)));
        }

        public override double Evaluate(double x, double y, double z, double w, double v, double u)
        {
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;
            w *= Frequency;
            v *= Frequency;
            u *= Frequency;

            int ix = (int)Functions.Floor(x);
            int iy = (int)Functions.Floor(y);
            int iz = (int)Functions.Floor(z);
            int iw = (int)Functions.Floor(w);
            int iv = (int)Functions.Floor(v);
            int iu = (int)Functions.Floor(u);

            double minDist = double.MaxValue;
            double xPoint = 0;
            double yPoint = 0;
            double zPoint = 0;
            double wPoint = 0;
            double vPoint = 0;
            double uPoint = 0;

            // Inside each unit hypercube, there is a seed point at a random position.  Go
            // through each of the nearby cubes until we find a cube with a seed point
            // that is closest to the specified position.
            for (int uCur = iu - 2; uCur <= iu + 2; ++uCur)
            {
                for (int vCur = iv - 2; vCur <= iv + 2; ++vCur)
                {
                    for (int wCur = iw - 2; wCur <= iw + 2; ++wCur)
                    {
                        for (int zCur = iz - 2; zCur <= iz + 2; ++zCur)
                        {
                            for (int yCur = iy - 2; yCur <= iy + 2; ++yCur)
                            {
                                for (int xCur = ix - 2; xCur <= ix + 2; ++xCur)
                                {
                                    // Calculate the position and distance to the seed point inside of
                                    // this unit hypercube.
                                    double xPos = xCur + ValueNoise(xCur, yCur, zCur, wCur, vCur, uCur);
                                    double yPos = yCur + ValueNoise(xCur, yCur, zCur, wCur, vCur, uCur);
                                    double zPos = zCur + ValueNoise(xCur, yCur, zCur, wCur, vCur, uCur);
                                    double wPos = wCur + ValueNoise(xCur, yCur, zCur, wCur, vCur, uCur);
                                    double vPos = vCur + ValueNoise(xCur, yCur, zCur, wCur, vCur, uCur);
                                    double uPos = uCur + ValueNoise(xCur, yCur, zCur, wCur, vCur, uCur);
                                    double xDist = xPos - x;
                                    double yDist = yPos - y;
                                    double zDist = zPos - z;
                                    double wDist = wPos - w;
                                    double vDist = vPos - v;
                                    double uDist = uPos - u;
                                    double dist = xDist * xDist + yDist * yDist + zDist * zDist + wDist * wDist + vDist * uDist + vDist * vDist;

                                    if (dist < minDist)
                                    {
                                        //This seed point is closer to any others found so far, so record it
                                        minDist = dist;
                                        xPoint = xPos;
                                        yPoint = yPos;
                                        zPoint = zPos;
                                        wPoint = wPos;
                                        vPoint = vPos;
                                        uPoint = uPos;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            double distance = 0.0;
            if (EnableDistance) // Determine the distance to the nearest seed point.
                distance = Functions.Sqrt(minDist) * Functions.Sqrt(4) - 1.0;

            return distance + (Displacement * ValueNoise(
                (int)Functions.Floor(xPoint),
                (int)Functions.Floor(yPoint),
                (int)Functions.Floor(zPoint),
                (int)Functions.Floor(wPoint),
                (int)Functions.Floor(vPoint),
                (int)Functions.Floor(uPoint)));
        }
    }
}
