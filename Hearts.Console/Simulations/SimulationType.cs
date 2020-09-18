namespace Hearts.Console.Simulations
{
    public enum SimulationType
    {
        /// <summary>
        /// Simulates the list of Bots in the given seat order.
        /// </summary>
        Standard,

        /// <summary>
        /// All seating combinations of the Bots will be used. 24x as many games.
        /// </summary>
        AllSeatCombinations,

        /// <summary>
        /// To performance test each Bot, the seating combinations will
        /// pitch each Bot in the AIs folder against itself (x4).
        /// Note that ONLY agents with a parameterless constructor are 
        /// picked up.
        /// </summary>
        PerformanceTest
    }
}
