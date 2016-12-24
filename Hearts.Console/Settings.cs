using Hearts.AI;
using Hearts.Console.Simulations;
using QueenCatcherBot;
using BenRead.Hearts;
using HeartsCrusher.Agents;
using System.Collections.Generic;

namespace Hearts.Console
{
    public class Settings
    {
        // Controlled Randomisation
        public static bool UseFixedSeed = true;
        public static int FixedSeed = 8048609;

        // Note: This simulates at about 300 games per second, without console outputs, depending on the efficiency of the bots used
        //
        //      1 game  - 0s
        //  1,000 games - 3s
        // 10,000 games - 30s
        public static int GameSimulationCount = 1;

        public static SimulationType SimulationType = SimulationType.Standard;

        // True:    Shows full game breakdown
        // False:   Shows summary
        public static bool ShowFullOutput = GameSimulationCount == 1
            && SimulationType == SimulationType.Standard;

        // Demo Bots:
        //      CLASS                                       AUTHOR              TIME PER ROUND      CURRENTLY BROKEN
        //      TerribleRandomAiAgent                       Adam Hill           0ms
        //      Noob1AiExampleAgent                         Adam Hill           0ms
        //      Noob2AiExampleAgent                         Adam Hill           0ms
        //      Noob3AiExampleAgent                         Adam Hill           0ms
        //      SuicideNoob1AiExampleAgent                  Adam Hill           0ms

        // Contender Bots:
        //      CLASS                                       AUTHOR              TIME PER ROUND      CURRENTLY BROKEN
        //      SavageBeast                                 Adam Hill           3ms
        //      NoobCrusherV1                               Tony Beasley        0ms
        //      NoobCrusherV2                               Tony Beasley        0ms
        //      NoobCrusherV3                               Tony Beasley        0ms
        //      ShootCrusher                                Tony Beasley        0ms
        //      Craghoul                                    Craig Rowe          0ms
        //      Deathstar                                   James Robinson      0ms
        //      QueenCatcher                                Dan White           0ms                 Yes - Crashes in certain edge cases due to calling .Lowest() extension method on an empty enumerable
        //      DefensiveAfter90							Ben Read		    0ms  
        //      TrashBot                                    Mary Hyde           0ms

        public static HeartsPlayerList GameBots()
        {
            // Example of using AgentOptions:
            // ... new AgentFactory(typeof(ShootCrusher), new AgentOptions { IntentionalShootingEnabled = true, ShootingDisruptionEnabled = true }),
            return new HeartsPlayerList(new List<AgentFactory>
            {
                new AgentFactory(typeof(ShootCrusher)),
                new AgentFactory(typeof(Deathstar)),
                new AgentFactory(typeof(DefensiveAfter90)),
                new AgentFactory(typeof(SavageBeast))
            });
        }
    }
}
