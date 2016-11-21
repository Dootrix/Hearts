using Hearts.Logging;

namespace Hearts.Console
{
    public class Launcher
    {
        public static void Main()
        {
            if (!Settings.ShowFullOutput)
            {
                Log.Options = new SummaryOnlyLogOptions();
            }

            new Simulator().SimulateGames(Settings.GameSimulationCount);
            System.Console.ReadLine();
        }
    }
}
