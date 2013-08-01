using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Game
{
    public sealed class Time
    {
        private System.Diagnostics.Stopwatch Stopwatch = new System.Diagnostics.Stopwatch();
        
        /// <summary>
        /// Number of frames that have passed, one frame per tick.
        /// </summary>
        private long FrameCount;
        /// <summary>
        /// Real time that game was started.
        /// </summary>
        private DateTime StartTime;

        /// <summary>
        /// Time scale between real time and game time.
        /// </summary>
        private double _TimeScale;
        /// <summary>
        /// Time per fixed step.
        /// </summary>
        private TimeSpan _TimeStep;
        /// <summary>
        /// Maximum time per tick before clamping.
        /// </summary>
        private TimeSpan _MaximumTimeStep;


        private TimeSpan TotalRealTime;
        private TimeSpan TotalFixedTime;
        private TimeSpan TotalTime;

        private TimeSpan AccumulatedTime;

        private bool Started = false;

        private bool IsRunningSlowly;

        public Time()
        {
            MaximumTimeStep = TimeSpan.MaxValue;
            TimeStep = TimeSpan.FromMilliseconds(16.0);
            TimeScale = 1;
            StartTime = DateTime.MinValue;
        }

        /// <summary>
        /// Starts, or resumes, measuring elapsed time for an interval.
        /// </summary>
        public void Start()
        {
            if (!Started)
            {
                Reset();
            }
            Started = true;
            Stopwatch.Start();
        }

        /// <summary>
        /// Stops time interval measurement, resets the elapsed time to zero, and starts
        /// measuring elapsed time.
        /// </summary>
        public void Restart()
        {
            Reset();
            Start();
        }

        /// <summary>
        /// Stops measuring elapsed time for an interval.
        /// </summary>
        public void Stop()
        {
            Stopwatch.Stop();
        }

        /// <summary>
        /// Stops time interval measurement and resets the elapsed time to zero.
        /// </summary>
        public void Reset()
        {
            Stopwatch.Reset();

            FrameCount = 0;
            StartTime = DateTime.Now;

            TotalRealTime = TimeSpan.Zero;
            TotalFixedTime = TimeSpan.Zero;
            TotalTime = TimeSpan.Zero;
            AccumulatedTime = TimeSpan.Zero;
        }

        /// <summary>
        /// Gets a value indicating whether the internal timer is running.
        /// </summary>
        public bool IsRunning
        {
            get { return Stopwatch.IsRunning; }
        }

        public void Tick(Action<GameTime> fixedStep, Action<GameTime> variableStep)
        {
            if (IsRunning)
            {
                var elapsed = TimeSpan.FromTicks((long)(Stopwatch.ElapsedTicks * TimeScale));
                Stopwatch.Restart();

                IsRunningSlowly = false;

                AccumulatedTime += elapsed;

                if (AccumulatedTime > MaximumTimeStep)
                {
                    AccumulatedTime = MaximumTimeStep;
                    IsRunningSlowly = true;
                }

                if (AccumulatedTime.Ticks > (2 * TimeStep.Ticks))
                {
                    IsRunningSlowly = true;
                }

                while (AccumulatedTime >= TimeStep)
                {
                    TotalRealTime = DateTime.Now - StartTime;

                    fixedStep(new GameTime(
                        TotalFixedTime, TimeStep, IsRunningSlowly,
                        TotalRealTime, FrameCount));

                    TotalFixedTime += TimeStep;
                    AccumulatedTime -= TimeStep;
                }

                TotalRealTime = DateTime.Now - StartTime;

                variableStep(new GameTime(
                    TotalTime, elapsed, IsRunningSlowly,
                    TotalRealTime, ++FrameCount));

                TotalTime += elapsed;
            }
        }

        public TimeSpan TimeStep
        {
            get { return _TimeStep; }
            set
            {
                if (value >= MaximumTimeStep)
                    throw new ArgumentException("Tried to set TimeStep to greater than MaximumTimeStep.", "value");

                _TimeStep = value;
            }
        }

        public TimeSpan MaximumTimeStep
        {
            get { return _MaximumTimeStep; }
            set
            {
                if (value <= TimeStep)
                    throw new ArgumentException("Tried to set MaximumTimeStep to less than TimeStep.", "value");
                _MaximumTimeStep = value;
            }
        }

        public double TimeScale
        {
            get
            {
                return _TimeScale;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Tried to set TimeScale to less than zero.", "value");

                _TimeScale = value;
            }
        }
    }
}
