using System.Collections.Generic;
using Hearts.Model;
using Hearts.AI;

namespace Hearts.Console
{
    public class Settings
    {
        // Note: This simulates at about 300 games per second, without console outputs
        //      1 game  - 0s
        //  1,000 games - 3s
        // 10,000 games - 30s
        public static int GameSimulationCount = 1;
        public static bool ShowFullOutput = true;
        public static List<Player> Bots = new List<Player>
            {
                // Available bots:
                //    • TerribleRandomAiAgent
                //    • Noob1AiExampleAgent
                //    • Noob2AiExampleAgent
                //    • SavageBeast (Adam Hill)
                //    • NoobCrusher (Tony Beasley)
                new Player("A", new NoobCrusher()),
                new Player("B", new NoobCrusher()),
                new Player("C", new NoobCrusher()),
                //new Player("D", new TerribleRandomAiAgent())
                new Player("D", new SavageBeast())
            };
    }
}
