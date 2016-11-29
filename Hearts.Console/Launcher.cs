using Hearts.Logging;
using Hearts.Randomisation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hearts.AI;
using Hearts.Extensions;
using Hearts.Model;
using Hearts.Scoring;
using System.Threading.Tasks;

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

            var gameBots = Launcher.GetGameBots();

            Settings.Notifier.CallSimulationStarted();

            if (Settings.GameSimulationCount == 1)
            {
                StaticRandomAccessor.ControlledRandoms = new List<IControlledRandom> { new ControlledRandom(Settings.UseFixedSeed ? Settings.FixedSeed : Environment.TickCount) };
                Log.LogRandomSeed(StaticRandomAccessor.ControlledRandoms[0].GetSeed());
                var timer = Stopwatch.StartNew();
                new Simulator(Settings.Notifier).SimulateGames(gameBots, Settings.GameSimulationCount);
                timer.Stop();

                Log.TotalSimulationTime(timer.ElapsedMilliseconds);
            }
            else
            {
                var orderedBots = new List<IEnumerable<Bot>>();
                var seatingCombinations = GetEveryBotSeatingCombination(gameBots).ToList();
                StaticRandomAccessor.ControlledRandoms = GetControlledRandoms(seatingCombinations);
                Log.LogRandomSeed(StaticRandomAccessor.ControlledRandoms[0].GetSeed());
                var results = new List<SimulationResult>();
                var timer = Stopwatch.StartNew();
                for (int i = 0; i < seatingCombinations.Count; i++)
                {
                    results.Add(new Simulator(Settings.Notifier).SimulateGames(gameBots, Settings.GameSimulationCount, logOutput: false, randomIndex: i));
                };
                timer.Stop();

                var combinedResult = CombineSimulations(results);
                Log.LogSimulationSummary(combinedResult);

                Log.TotalSimulationTime(timer.ElapsedMilliseconds);
            }

            Settings.Notifier.CallSimulationEnded();

            System.Console.ReadLine();
        }

        private static SimulationResult CombineSimulations(List<SimulationResult> results)
        {
            // Note: Timings are just based on the last game
            return new SimulationResult(results.SelectMany(i => i.GameResults).ToList(), results.Last().TimerService);
        }

        // Assumes 4 players
        private static IEnumerable<IEnumerable<Bot>> GetEveryBotSeatingCombination(IEnumerable<Bot> bots)
        {
            var ordered = bots.ToList();

            foreach (var a in ordered)
            {
                foreach (var b in ordered.Except(new[] { a }))
                {
                    foreach (var c in ordered.Except(new[] { a, b }))
                    {
                        foreach (var d in ordered.Except(new[] { a, b, c }))
                        {
                            yield return new[] { a, b, c, d };
                        }
                    }
                }
            }
        }

        private static List<IControlledRandom> GetControlledRandoms(IEnumerable<IEnumerable<Bot>> seatingCombinations)
        {
            int j = Environment.TickCount;
            return seatingCombinations.Select(i => new ControlledRandom(Settings.UseFixedSeed ? Settings.FixedSeed : j)).Cast<IControlledRandom>().ToList();
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
