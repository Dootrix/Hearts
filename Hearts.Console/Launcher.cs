using Hearts.Logging;
using Hearts.Randomisation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hearts.AI;
using Hearts.Extensions;
using Hearts.Model;

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

            var gameBots = Launcher.GetGameBots();

            var timer = Stopwatch.StartNew();
            new Simulator(Settings.Notifier).SimulateGames(gameBots, Settings.GameSimulationCount);
            timer.Stop();

            Log.TotalSimulationTime(timer.ElapsedMilliseconds);

            System.Console.ReadLine();
        }

        private static IEnumerable<Bot> GetGameBots()
        {
            var chosenBots = Settings.Bots;
            var availableBots = Agent.GetAvailableAgents()
                .OrderBy(x => x.AgentName)
                .ToList();

            while (chosenBots.Count < 4)
            {
                System.Console.WriteLine("Please pick a bot for seat {0}", chosenBots.Count + 1);
                availableBots.ForEach((item, i) =>
                {
                    System.Console.WriteLine("{0}: {1}", i + 1, item.AgentName);
                });
                var request = System.Console.ReadLine();
                var chosenBot = request.ToNullableInt(null);
                if (chosenBot.HasValue && chosenBot.Value <= availableBots.Count)
                {
                    chosenBots.Add(availableBots[chosenBot.Value - 1]);
                }
                System.Console.WriteLine();
            }

            return chosenBots;
        }
    }
}
