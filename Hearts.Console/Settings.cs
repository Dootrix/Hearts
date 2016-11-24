using System.Collections.Generic;
using Hearts.Model;
using Hearts.AI;
using HeartsCrusher;

namespace Hearts.Console
{
    public class Settings
    {
        // Note: This simulates at about 300 games per second, without console outputs
        //      1 game  - 0s
        //  1,000 games - 3s
        // 10,000 games - 30s
        public static int GameSimulationCount = 1;
        public static bool ShowFullOutput = GameSimulationCount == 1;
        public static List<Bot> Bots = new List<Bot>
            {
                // Available bots:
                // (* = Doesn't currently build against current IAgent signature)
                //    • TerribleRandomAiAgent
                //    • Noob1AiExampleAgent
                //    • Noob2AiExampleAgent
                //    • Noob3AiExampleAgent
                //    • SavageBeast (Adam Hill)
                //    • NoobCrusher.Create(NoobCrusherVersion.v1) (Tony Beasley)
                //    • NoobCrusher.Create(NoobCrusherVersion.v2) (Tony Beasley)
                
                Bot.Create(new Player("A"), new SavageBeast(true)),
                Bot.Create(new Player("B"), NoobCrusher.Create(NoobCrusherVersion.v2)),
                Bot.Create(new Player("C"), new QueenCatcherAI.QueenCatcher()),
                Bot.Create(new Player("D"), new Noob3AiExampleAgent())
            };
    }
}
