using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Ibasa.Game
{
    public abstract class Game
    {
        private GameClock Clock = new GameClock();

        public TimeSpan InactiveSleepTime { get; set; }
        public bool IsActive { get; set; }

        private TimeSpan AccumulatedTime;
        private TimeSpan CurrentTime;
        public TimeSpan TimeStep { get; set; }
        public TimeSpan MaximumTimeStep { get; set; }

        public Game()
        {
            InactiveSleepTime = TimeSpan.FromSeconds(1.0);

            TimeStep = TimeSpan.FromMilliseconds(16.0);
            MaximumTimeStep = TimeSpan.MaxValue;

            IsActive = true;
        }

        private bool _initalized = false;
        public virtual void Initalize()
        {
            _initalized = true;
            Clock.Start();
        }

        public void Tick()
        {
            if (!_initalized)
                throw new System.InvalidOperationException("Initalize not called.");

            if (!IsActive)
                System.Threading.Thread.Sleep(InactiveSleepTime);

            Clock.Tick();

            AccumulatedTime += Clock.Elapsed;
            bool isRunningSlowly = false;

            if (AccumulatedTime > MaximumTimeStep)
            {
                AccumulatedTime = MaximumTimeStep;
                isRunningSlowly = true;
            }

            TimeSpan time = CurrentTime;
            while (AccumulatedTime >= TimeStep)
            {
                CurrentTime += TimeStep;
                Update(new GameTime(CurrentTime, TimeStep, isRunningSlowly));
                AccumulatedTime -= TimeStep;
            }

            Render(new GameTime(CurrentTime, CurrentTime - time, isRunningSlowly));
        }

        protected virtual void Update(GameTime elapsed)
        {
        }

        protected virtual void Render(GameTime elapsed)
        {
        }
    }
}
