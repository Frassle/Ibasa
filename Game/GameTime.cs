using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Game
{
    /// <summary>
    /// Holds time values for a game.
    /// </summary>
    public struct GameTime
    {
        /// <summary>
        /// Creates a new instance of GameTime. 
        /// </summary>
        /// <param name="total">The amount of game time since the start of the game.</param>
        /// <param name="elapsed">The amount of elapsed game time since the last update.</param>
        /// <param name="isRunningSlowly">Whether the game is running multiple updates this frame.</param>
        public GameTime(
         TimeSpan total,
         TimeSpan elapsed,
         bool isRunningSlowly) : this()
        {
            Total = total;
            Elapsed = elapsed;
            IsRunningSlowly = isRunningSlowly;
        }
        
        /// <summary>
        /// The amount of elapsed game time since the last update.
        /// </summary>
        public TimeSpan Elapsed { get; private set; }
        /// <summary>
        /// The amount of game time since the start of the game.
        /// </summary>
        public TimeSpan Total { get; private set; }
        /// <summary>
        /// Whether the game is running multiple updates this frame.
        /// </summary>
        public bool IsRunningSlowly { get; private set; }

        public override string ToString()
        {
            return string.Format("Total={0} Elapsed={1}{2}", Total, Elapsed, IsRunningSlowly ? " Is running slowly!" : "");
        }
    }
}
