using System;

namespace Hearts.Console.Simulations
{
    public static class SimulationFactory
    {
        public static ISimulation CreateSimulation()
        {
            switch (Settings.SimulationType)
            {
                case SimulationType.Standard:
                    return new StandardSimulation();
                case SimulationType.AllSeatCombinations:
                    return new AllSeatCombinationsSimulation();
                case SimulationType.PerformanceTest:
                    return new PerformanceTestSimulation();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
