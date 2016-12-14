using Hearts.AI;
using Hearts.Console.Simulations;
using QueenCatcherBot;
using BenRead.Hearts;
using HeartsCrusher.Agents;

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
        public static int GameSimulationCount = 10000/24;

        public static SimulationType SimulationType = SimulationType.Standard;

        // True:    Shows full game breakdown
        // False:   Shows summary
        public static bool ShowFullOutput = GameSimulationCount == 1 
            && SimulationType == SimulationType.Standard;

        // Demo Bots:
        //      CLASS                                       AUTHOR              TIME PER ROUND
        //      TerribleRandomAiAgent                       Adam Hill           0ms
        //      Noob1AiExampleAgent                         Adam Hill           0ms
        //      Noob2AiExampleAgent                         Adam Hill           0ms
        //      Noob3AiExampleAgent                         Adam Hill           0ms
        //      SuicideNoob1AiExampleAgent                  Adam Hill           0ms

        // Contender Bots:
        //      CLASS                                       AUTHOR              TIME PER ROUND
        //      SavageBeast                                 Adam Hill           3ms
        //      NoobCrusherV1                               Tony Beasley        0ms
        //      NoobCrusherV2                               Tony Beasley        0ms
        //      NoobCrusherV3                               Tony Beasley        0ms
        //      ShootCrusher                                Tony Beasley        0ms
        //      Craghoul                                    Craig Rowe          0ms
        //      Deathstar                                   James Robinson      0ms
        //      QueenCatcher                                Dan White           0ms
        //      DefensiveAfter90							Ben Read		    0ms  
        //      TrashBot                                    Mary Hyde           0ms

        public static HeartsPlayerList GameBots()
        {
            return new HeartsPlayerList
            {
                new ShootCrusher(allowShoot: true, allowAntiShoot: true),
                new Deathstar(allowShoot: true, allowAntiShoot: true),
                new DefensiveAfter90(),
                new SavageBeast()
            };
        }
    }
}
