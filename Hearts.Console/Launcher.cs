using System;
using Hearts.Logging;
using Hearts.Randomisation;

namespace Hearts.Console
{
    public class Launcher
    {
        public static void Main()
        {
            StaticRandomAccessor.ControlledRandom = new ControlledRandom(Settings.UseFixedSeed ? Settings.FixedSeed : Environment.TickCount);

            if (!Settings.ShowFullOutput)
            {
                Log.Options = new SummaryOnlyLogOptions();
            }

            new Simulator().SimulateGames(Settings.Bots, Settings.GameSimulationCount);
            System.Console.ReadLine();
        }
    }
}
