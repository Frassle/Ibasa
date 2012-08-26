using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Ibasa.Game
{
    public sealed class GameClock
    {
        private Stopwatch Stopwatch = new System.Diagnostics.Stopwatch();

        private TimeSpan T1;
        private TimeSpan T0;

        public GameClock()
        {
        }

        public void Stop()
        {
            Stopwatch.Reset();
        }

        public void Start()
        {
            Stopwatch.Restart();
        }

        public void Suspend()
        {
            Stopwatch.Stop();
        }

        public void Resume()
        {
            Stopwatch.Start();
        }

        public void Tick()
        {
            if (!Stopwatch.IsRunning)
                return;

            T1 = Stopwatch.Elapsed;
            Elapsed = T1 - T0;
            T0 = T1;
        }

        public TimeSpan Elapsed { get; private set; }
    }
}
