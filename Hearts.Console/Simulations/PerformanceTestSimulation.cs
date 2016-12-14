using Hearts.AI;
using Hearts.Events;
using Hearts.Logging;
using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hearts.Console.Simulations
{
    public class PerformanceTestSimulation : ISimulation
    {
        private Stopwatch stopwatch;
        private EventNotifier notifier;

        public void Execute()
        {
            this.notifier = new EventNotifier();
            this.notifier.SimulationStartedForSeatingArrangement += this.OnSimulationStartedForSeatingArrangement;
            this.notifier.SimulationEndedForSeatingArrangement += this.OnSimulationEndedForSeatingArrangement;

            new Launcher(this.notifier)
                .ExecuteSimulations(PerformanceTestSimulation.CreateSeatingArrangement());
        }

        private void OnSimulationStartedForSeatingArrangement(object sender, Events.EventArg<IEnumerable<Bot>> e)
        {
            this.stopwatch = Stopwatch.StartNew();
        }

        private void OnSimulationEndedForSeatingArrangement(object sender, Events.EventArg<IEnumerable<Bot>> args)
        {
            var seatingArrangement = args.Data;

            this.stopwatch.Stop();
            Log.TotalSimulationTime(this.stopwatch.ElapsedMilliseconds);
        }

        private static IEnumerable<IEnumerable<Bot>> CreateSeatingArrangement()
        {
            var combinations = new List<IEnumerable<Bot>>();

            var agentTypes = Agent
                .GetAvailableAgentTypes()
                .Except(new[] { typeof(IllegalMoveAgent) })
                .OrderBy(x => x.Name)
                .ToList();

            // make each bot play at a table with 4 of itself in total!
            foreach (var agentType in agentTypes)
            {
                var sameAgents = Enumerable
                    .Range(1, 4)
                    .Select(x => Agent.CreateAgent(agentType));

                if (sameAgents.All(x => x != null))
                {
                    var sameBots = new HeartsPlayerList();
                    sameBots.AddRange(sameAgents);
                    combinations.Add(sameBots);
                }
            }

            return combinations;
        }
    }
}
