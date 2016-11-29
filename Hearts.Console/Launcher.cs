using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hearts.AI;
using Hearts.Extensions;
using Hearts.Logging;
using Hearts.Model;
using Hearts.Randomisation;
using Hearts.Scoring;

namespace Hearts.Console
{
    public class Launcher
    {
        public void ExecuteSimulations()
        {
            var gameBots = this.GetGameBots();
            Settings.Notifier.CallSimulationStarted();

            var timer = Stopwatch.StartNew();
            var results = new List<SimulationResult>();

            if (Settings.GameSimulationCount == 1)
            {
                results.Add(new Simulator(Settings.Notifier).SimulateGames(gameBots, Settings.GameSimulationCount, new ControlledRandom(Settings.UseFixedSeed ? Settings.FixedSeed : Environment.TickCount)));
            }
            else
            {
                // We run one simulation for every possible seating combination, each with the same starting seed, to eliminate the advantage of any cardset
                var roundRobinSeatingArrangements = this.PermutateOrdering(gameBots).ToList();
                StaticRandomAccessor.ControlledRandoms = this.GetControlledRandoms(roundRobinSeatingArrangements.Count);

                for (int i = 0; i < roundRobinSeatingArrangements.Count; i++)
                {
                    results.Add(new Simulator(Settings.Notifier).SimulateGames(roundRobinSeatingArrangements[i], Settings.GameSimulationCount, StaticRandomAccessor.ControlledRandoms[i], logOutput: false));
                };
            }

            timer.Stop();
            Log.TotalSimulationTime(timer.ElapsedMilliseconds);
            var combinedResult = CombineSimulations(results);
            Log.LogSimulationSummary(combinedResult);
            Log.LogRandomSeed(StaticRandomAccessor.ControlledRandoms[0].GetSeed());
            Settings.Notifier.CallSimulationEnded();
        }

        private SimulationResult CombineSimulations(List<SimulationResult> results)
        {
            // Note: Timings are just based on the last game
            return new SimulationResult(results.SelectMany(i => i.GameResults).ToList(), results.Last().TimerService);
        }

        // Assumes 4 players
        private IEnumerable<IEnumerable<T>> PermutateOrdering<T>(IEnumerable<T> elements)
        {
            var ordered = elements.ToList();

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

        private List<IControlledRandom> GetControlledRandoms(int seatArrangementCount)
        {
            int tickCount = Environment.TickCount;

            return Enumerable.Range(0, seatArrangementCount).Select(i => new ControlledRandom(Settings.UseFixedSeed ? Settings.FixedSeed : tickCount)).Cast<IControlledRandom>().ToList();
        }

        private IEnumerable<Bot> GetGameBots()
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
