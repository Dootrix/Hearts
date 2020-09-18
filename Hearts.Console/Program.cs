using Hearts.Console.Simulations;
using Hearts.Logging;

namespace Hearts.Console
{
    public class Program
    {
        public static void Main()
        {
            Log.BeginLogging(Settings.LoggingLevel, Settings.LoggingOutput);
            SimulationFactory.CreateSimulation().Execute();
            Log.StopLogging();
            System.Console.WriteLine("Done.");
            System.Console.ReadLine();
        }
    }
}
