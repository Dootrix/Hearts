
using System.Collections.Generic;
using Hearts.AI;
using Hearts.Model;
using QueenCatcherAI;
using Hearts.Events;
using HeartsCrusher.Agents;

namespace Hearts.Console
{
    public class Settings
    {
        // Controlled Randomisation
        public static bool UseFixedSeed = false;
        public static int FixedSeed = 4174562;
        public static EventNotifier Notifier = new EventNotifier();

        // Note: This simulates at about 300 games per second, without console outputs, depending on the efficiency of the bots used
        //
        //      1 game  - 0s
        //  1,000 games - 3s
        // 10,000 games - 30s
        public static int GameSimulationCount = 1;

        // True:    Shows full game breakdown
        // True:    Shows summary
        public static bool ShowFullOutput = GameSimulationCount == 1;

        // Available bots: (* = Doesn't currently build against current IAgent signature)
        // 
        //    • TerribleRandomAiAgent                                       0ms
        //    • Noob1AiExampleAgent                                         0ms
        //    • Noob2AiExampleAgent                                         0ms
        //    • Noob3AiExampleAgent                                         0ms
        //    • SavageBeast(Notifier, allowShoot: false)    Adam Hill       ?ms
        //    • SavageBeast(Notifier, allowShoot: true)     Adam Hill       3ms
        //    • NoobCrusher.Create(NoobCrusherVersion.v1)   Tony Beasley    0ms
        //    • NoobCrusher.Create(NoobCrusherVersion.v2)   Tony Beasley    0ms
        //    • NoobCrusher.Create(NoobCrusherVersion.v3)   Tony Beasley    0ms
        //    • ShootCrusher                                Tony Beasley    0ms
        //    • Craghoul                                    Craig Rowe      0ms
        //    • Deathstar                                   James Robinson  0ms
        //    • QueenCatcher                                Dan White       0ms
        //
        public static HeartsPlayerList Bots = new HeartsPlayerList
            {
                new ShootCrusher(),
                new Deathstar(),
                new QueenCatcher(),
                new SavageBeast(Notifier, allowShoot: true)
            };
    }
}
