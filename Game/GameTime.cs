using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Game
{
    /// <summary>
    /// Holds time values for a game.
    /// </summary>
    public sealed class GameTime
    {
        /// <summary>
        /// Creates a new instance of GameTime. 
        /// </summary>
        /// <param name="time">The amount of game time since the start of the game.</param>
        /// <param name="elapsed">The amount of elapsed game time since the last update.</param>
        /// <param name="isRunningSlowly">Whether the game is running multiple updates this frame.</param>
        /// <param name="realTime">The amount of real time that has passed since the start of the game.</param>
        /// <param name="frameCount">The number of frames that have passed since the start of the game.</param>
        internal GameTime(
            TimeSpan time,
            TimeSpan elapsed,
            bool isRunningSlowly,
            TimeSpan realTime,
            long frameCount)
        {
            Time = time;
            Elapsed = elapsed;
            IsRunningSlowly = isRunningSlowly;
            RealTime = realTime;
            FrameCount = frameCount;
        }

        /// <summary>
        /// The amount of game time since the start of the game.
        /// </summary>
        public TimeSpan Time { get; private set; }
        /// <summary>
        /// The amount of elapsed game time since the last update.
        /// </summary>
        public TimeSpan Elapsed { get; private set; }

        /// <summary>
        /// Whether the game is running multiple updates this frame.
        /// </summary>
        public bool IsRunningSlowly { get; private set; }

        /// <summary>
        /// The amount of real time that has passed since the start of the game.
        /// </summary>
        public TimeSpan RealTime { get; private set; }

        /// <summary>
        /// The number of frames that have passed since the start of the game.
        /// </summary>
        public long FrameCount { get; private set; }

        public override string ToString()
        {
            return string.Format("Time={0} Elapsed={1}{2} ({3} frames in {4})", 
                Time, Elapsed, IsRunningSlowly ? " Is running slowly!" : "",
                FrameCount, RealTime);
        }
    }
}
