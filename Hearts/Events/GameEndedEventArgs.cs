using Hearts.Scoring;
using System;

namespace Hearts.Events
{
    public class GameEndedEventArgs
    {
        private readonly GameResult result;

        public GameEndedEventArgs(GameResult result)
        {
            this.result = result;
        }

        public GameResult Result
        {
            get
            {
                return this.result;
            }
        }
    }
}
