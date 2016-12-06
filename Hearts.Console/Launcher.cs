using Hearts.AI;
using Hearts.Console.Simulations;
using Hearts.Extensions;
using Hearts.Logging;
using Hearts.Model;
using Hearts.Randomisation;
using Hearts.Scoring;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hearts.Console
{
    public class Launcher
    {
        public void ExecuteSimulations(IEnumerable<IEnumerable<Bot>> seatingCombinations)
        {
            int randomSeed = Settings.UseFixedSeed
                ? Settings.FixedSeed
                : Environment.TickCount;

            Log.LogRandomSeed(randomSeed);

            Settings.Notifier.CallSimulationStarted(randomSeed);

            var timer = Stopwatch.StartNew();
            var results = new List<SimulationResult>();

            foreach (var seatingArrangement in seatingCombinations)
            {
                Settings.Notifier.CallSimulationStartedForSeatingArrangement(seatingArrangement);

                var simulator = new Simulator(Settings.Notifier);

                var result = simulator.SimulateGames(
                    seatingArrangement,
                    Settings.GameSimulationCount,
                    new ControlledRandom(randomSeed),
                    logOutput: Settings.SimulationType != SimulationType.AllSeatCombinations);

                Settings.Notifier.CallSimulationEndedForSeatingArrangement(seatingArrangement);

                results.Add(result);
            }

            timer.Stop();
            Log.TotalSimulationTime(timer.ElapsedMilliseconds);

            if (Settings.SimulationType == SimulationType.AllSeatCombinations)
            {
                var combinedResult = CombineSimulations(results);
                Log.LogSimulationSummary(combinedResult);
                Settings.Notifier.CallSimulationEnded(combinedResult);
            }
            else
            {
                Settings.Notifier.CallSimulationEnded(results.First());
            }
        }

        private SimulationResult CombineSimulations(List<SimulationResult> results)
        {
            // Note: Timings are just based on the last game
            return new SimulationResult(results.SelectMany(i => i.GameResults).ToList(), results.Last().TimerService);
        }
    }
}
