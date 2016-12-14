using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var agents = BotHelper.GetGameBots().Select(i => i.Agent).ToList();
            
            return new List<Bot>
            {
                new Bot(players[indiciesList[0]], agents[indiciesList[0]]),
                new Bot(players[indiciesList[1]], agents[indiciesList[1]]),
                new Bot(players[indiciesList[2]], agents[indiciesList[2]]),
                new Bot(players[indiciesList[3]], agents[indiciesList[3]])
            };
        }
    }
}
