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

            int randomSeed = Settings.UseFixedSeed 
                ? Settings.FixedSeed 
                : Environment.TickCount;

            // Simulating every possible seating combination, 
            // each with the same starting seed eliminates the advantage of any card set.
            var seatingCombinations = Settings.SimulateAllSeatCombinations
                ? gameBots.Permute() : new[] { gameBots };          

            foreach (var seatingArrangement in seatingCombinations)
            {
                var simulator = new Simulator(Settings.Notifier);

                var result = simulator.SimulateGames(
                    seatingArrangement,
                    Settings.GameSimulationCount,
                    new ControlledRandom(randomSeed),
                    logOutput: !Settings.SimulateAllSeatCombinations);

                results.Add(result);
            }

            timer.Stop();
            Log.TotalSimulationTime(timer.ElapsedMilliseconds);

            if (Settings.SimulateAllSeatCombinations)
            {
                var combinedResult = CombineSimulations(results);
                Log.LogSimulationSummary(combinedResult);
            }

            Log.LogRandomSeed(randomSeed);
            Settings.Notifier.CallSimulationEnded();
        }

        private static IControlledRandom CreateControlledRandom(int tickCount)
        {
            return new ControlledRandom(Settings.UseFixedSeed ? Settings.FixedSeed : tickCount);
        }

        private SimulationResult CombineSimulations(List<SimulationResult> results)
        {
            // Note: Timings are just based on the last game
            return new SimulationResult(results.SelectMany(i => i.GameResults).ToList(), results.Last().TimerService);
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
