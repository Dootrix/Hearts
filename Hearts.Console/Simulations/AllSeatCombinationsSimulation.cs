using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.AI;
using Hearts.Extensions;
using Hearts.Model;

namespace Hearts.Console.Simulations
{
    public class AllSeatCombinationsSimulation : ISimulation
    {
        public void Execute()
        {
            // Create new lists of bots, each with new instances of each bot, for each distinct seating order
            var playerList = BotHelper.GetGameBots().Select(i => i.Player).ToList();
            var seatingCombinations = Enumerable.Range(0, 4).Permute().Select(i => this.GetBots(i, playerList)).ToArray();

            // Execute a simulation for every seating combination
            new Launcher().ExecuteSimulations(seatingCombinations);
        }

        private List<Bot> GetBots(IEnumerable<int> indicies, List<Player> players)
        {
            // Due to the coupling of Player and Agent we need to be careful in ensuring the player instances remain the same, and only bot instances are recreated
            var indiciesList = indicies.ToList();
            var agents = BotHelper.GetGameBots().ToList();
            
            return new List<Bot>
            {
                agents[indiciesList[0]].Clone(players[indiciesList[0]]),
                agents[indiciesList[1]].Clone(players[indiciesList[1]]),
                agents[indiciesList[2]].Clone(players[indiciesList[2]]),
                agents[indiciesList[3]].Clone(players[indiciesList[3]]),
            };
        }
    }
}
