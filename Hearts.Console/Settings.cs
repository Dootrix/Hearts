using System.Collections.Generic;
using Hearts.Model;
using Hearts.AI;

namespace Hearts.Console
{
    public class Settings
    {
        // Note: This simulates at about 300 games per second, without console outputs
        // 1 game - v. fast
        // 1,000 games - 3 seconds
        // 10,000 games - 30 seconds
        public static int GameSimulationCount = 10000;
        public static bool ShowFullOutput = false;
        public static List<Player> Bots = new List<Player>
            {
                // Available bots in comments...
                // TerribleRandomAiAgent
                // Noob1AiExampleAgent
                // Noob2AiExampleAgent
                // BeastAi.SavageBeast
                new Player("A", new SavageBeast()),
                new Player("B", new Noob2AiExampleAgent()),
                new Player("C", new Noob2AiExampleAgent()),
                //new Player("D", new TerribleRandomAiAgent())
                new Player("D", new NoobCrusher())
            };
    }
}
