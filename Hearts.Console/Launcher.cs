using Hearts.Logging;
using Hearts.Randomisation;
using System;
using System.Diagnostics;

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

            var timer = Stopwatch.StartNew();
            new Simulator().SimulateGames(Settings.Bots, Settings.GameSimulationCount);
            timer.Stop();

            Log.TotalSimulationTime(timer.ElapsedMilliseconds);

            System.Console.ReadLine();
        }
    }
}
