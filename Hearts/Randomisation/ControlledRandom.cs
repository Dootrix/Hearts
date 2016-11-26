using System;
using Hearts.Logging;

namespace Hearts.Randomisation
{
    public class ControlledRandom : IControlledRandom
    {
        protected int seed;
        protected Random privateRandom;

        public ControlledRandom()
        {
            this.ResetSeedToTime();
        }

        public ControlledRandom(int seed)
        {
            this.ResetSeedToValue(seed);
        }

        public int Next(int minValue, int maxValue)
        {
            return this.privateRandom.Next(minValue, maxValue);
        }

        public void ResetSeedToValue(int value)
        {
            this.seed = value;
            this.privateRandom = new Random(value);
            Log.LogRandomSeed(this.seed);
        }

        public void ResetSeedToTime()
        {
            this.seed = Environment.TickCount;
            this.privateRandom = new Random(this.seed);
            Log.LogRandomSeed(this.seed);
        }

        public void ResetRandomWithCurrentSeed()
        {
            this.privateRandom = new Random(this.seed);
        }

        public int GetSeed()
        {
            return this.seed;
        }
    }
}
