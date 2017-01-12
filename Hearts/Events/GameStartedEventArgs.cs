using System;

namespace Hearts.Events
{
    public class GameStartedEventArgs
    {
        private readonly int randomSeed;

        public GameStartedEventArgs(int randomSeed)
        {
            this.randomSeed = randomSeed;
        }

        public int RandomSeed
        {
            get
            {
                return this.randomSeed;
            }
        }
    }
}
