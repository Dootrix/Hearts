
using System.Collections.Generic;
using Hearts.AI;
using Hearts.Model;
using HeartsCrusher;
using QueenCatcherAI;
using Hearts.Events;

namespace Hearts.Console
{
    public class Settings
    {
        // Controlled Randomisation
        public static bool UseFixedSeed = false;
        public static int FixedSeed = 4174562; //4174562
        public static EventNotifier Notifier = new EventNotifier();

        // Note: This simulates at about 300 games per second, without console outputs, depending on the efficiency of the bots used
        //
        //      1 game  - 0s
        //  1,000 games - 3s
        // 10,000 games - 30s
        public static int GameSimulationCount = 10;

        public static bool ShowFullOutput = GameSimulationCount == 1;

        // Available bots: (* = Doesn't currently build against current IAgent signature)
        // 
        //    • TerribleRandomAiAgent
        //    • Noob1AiExampleAgent
        //    • Noob2AiExampleAgent
        //    • Noob3AiExampleAgent
        //    • SavageBeast (Adam Hill) - Note: 11ms per pass
        //    • NoobCrusher.Create(NoobCrusherVersion.v1) (Tony Beasley)
        //    • NoobCrusher.Create(NoobCrusherVersion.v2) (Tony Beasley)
        //    • NoobCrusher.Create(NoobCrusherVersion.v3) (Tony Beasley)
        //    • Craghoul (Craig Rowe)
        //    • Deathstar (James Robinson)
        //    • QueenCatcher (Dan White)

        public static HeartsPlayerList Bots = new HeartsPlayerList
            {
                //NoobCrusher.Create(NoobCrusherVersion.v3),
                //new Deathstar(),
                //new QueenCatcher(),
                //new Craghoul()
            };

        public static List<Bot> VersusNoob1(IAgent bot)
        {
            return new HeartsPlayerList
            {
                new Noob1AiExampleAgent(),
                new Noob1AiExampleAgent(),
                new Noob1AiExampleAgent(),
                bot
            };
        }

        public static List<Bot> VersusNoob2(IAgent bot)
        {
            return new HeartsPlayerList
            {
                new Noob2AiExampleAgent(),
                new Noob2AiExampleAgent(),
                new Noob2AiExampleAgent(),
                bot
            };
        } 

        public static List<Bot> VersusNoob3(IAgent bot)
        {
            return new HeartsPlayerList
            {
                new Noob3AiExampleAgent(),
                new Noob3AiExampleAgent(),
                new Noob3AiExampleAgent(),
                bot
            };
        }

        public static List<Bot> VersusNoob123(IAgent bot)
        {
            return new HeartsPlayerList
            {
                new Noob1AiExampleAgent(),
                new Noob2AiExampleAgent(),
                new Noob3AiExampleAgent(),
                bot
            };
        }
    }
}
