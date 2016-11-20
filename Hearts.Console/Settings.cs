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
        public static int GameSimulationCount = 10000;
        public static bool ShowFullOutput = false;
        public static List<Player> Bots = new List<Player>
            {
                // Available bots:
                //    • TerribleRandomAiAgent
                //    • Noob1AiExampleAgent
                //    • Noob2AiExampleAgent
                //    • Noob3AiExampleAgent
                //    • SavageBeast (Adam Hill)
                //    • NoobCrusher.Create(NoobCrusherVersion.v1) (Tony Beasley)
                //    • NoobCrusher.Create(NoobCrusherVersion.v2) (Tony Beasley)
                new Player("A", new Noob1AiExampleAgent()),
                new Player("B", new Noob2AiExampleAgent()),
                new Player("C", new Noob3AiExampleAgent()),
                new Player("D", NoobCrusher.Create(NoobCrusherVersion.v2))
            };
    }
}
