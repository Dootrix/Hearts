using System;

namespace Hearts.Events
{
    public class SimulationStartedEventArgs
    {
        private readonly int randomSeed;

        public SimulationStartedEventArgs(int randomSeed)
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
