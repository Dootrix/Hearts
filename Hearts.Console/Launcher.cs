using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts;
using Hearts.Extensions;
using Hearts.Model;
using Hearts.AI;
using Hearts.Logging;

namespace Hearts.Console
{
    public class Launcher
    {
        public static void Main()
        {
            // Note: Swap this options class to configure the output display, e.g. Default or Summary etc.
            Log.Options = new SummaryOnlyLogOptions();

            var players = CreatePlayers();
            var game = new Game(players);
            var cumulativeScores = players.ToDictionary(i => i, i => 0);
            int roundNumber = 0;

            do
            {
                var result = game.Play(roundNumber);

                foreach (var score in result.Scores)
                {
                    cumulativeScores[score.Key] += score.Value;
                }

                ++roundNumber;

            } while (cumulativeScores.All(i => i.Value < 100));

            Log.LogFinalWinner(cumulativeScores);

            System.Console.ReadLine();
        }

        private static List<Player> CreatePlayers()
        {
            return new List<Player>
                {
                    new Player("A", new TerribleRandomAiAgent()),
                    new Player("B", new Noob1AiExampleAgent()),
                    new Player("C", new TerribleRandomAiAgent()),
                    new Player("D", new Noob2AiExampleAgent())
                };
        }
    }
}
