using Hearts.AI;
using Hearts.Console.Simulations;
using Hearts.Events;
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
        private EventNotifier notifier;

        public Launcher() :
            this(new EventNotifier())
        {
        }

        public Launcher(EventNotifier notifier)
        {
            this.notifier = notifier;
        }

        public void ExecuteSimulations(IEnumerable<IEnumerable<Bot>> seatingCombinations)
        {
            int randomSeed = Settings.UseFixedSeed
                ? Settings.FixedSeed
                : Environment.TickCount;

            Log.LogRandomSeed(randomSeed);

            this.notifier.CallSimulationStarted(randomSeed);

            var timer = Stopwatch.StartNew();
            var results = new List<SimulationResult>();

            foreach (var seatingArrangement in seatingCombinations)
            {
                this.notifier.CallSimulationStartedForSeatingArrangement(seatingArrangement);

                var simulator = new Simulator(this.notifier);

                var result = simulator.SimulateGames(
                    seatingArrangement,
                    Settings.GameSimulationCount,
                    new ControlledRandom(randomSeed),
                    logOutput: Settings.SimulationType != SimulationType.AllSeatCombinations);

                this.notifier.CallSimulationEndedForSeatingArrangement(seatingArrangement);

                results.Add(result);
            }

            timer.Stop();
            Log.TotalSimulationTime(timer.ElapsedMilliseconds);

            if (Settings.SimulationType == SimulationType.AllSeatCombinations)
            {
                var combinedResult = this.CombineSimulations(results, seatingCombinations.First());
                Log.LogSimulationSummary(combinedResult);
                this.notifier.CallSimulationEnded(combinedResult);
            }
            else
            {
                this.notifier.CallSimulationEnded(results.First());
            }
        }

        private SimulationResult CombineSimulations(List<SimulationResult> results, IEnumerable<Bot> bots)
        {
            // Note: Timings are just based on the last game
            return new SimulationResult(results.SelectMany(i => i.GameResults).ToList(), bots, results.Last().TimerService);
        }
    }
}
