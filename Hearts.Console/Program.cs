using Hearts.Logging;

namespace Hearts.Console
{
    public class Program
    {
        public static void Main()
        {
            if (!Settings.ShowFullOutput)
            {
                Log.Options = new SummaryOnlyLogOptions();
            }

            new Launcher().ExecuteSimulations();

            System.Console.ReadLine();
        }
    }
}
