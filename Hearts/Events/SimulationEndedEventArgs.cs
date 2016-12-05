using Hearts.Scoring;
using System;

namespace Hearts.Events
{
    public class SimulationEndedEventArgs
    {
        private readonly SimulationResult result;

        public SimulationEndedEventArgs(SimulationResult result)
        {
            this.result = result;
        }

        public SimulationResult Result
        {
            get
            {
                return this.result;
            }
        }
    }
}
