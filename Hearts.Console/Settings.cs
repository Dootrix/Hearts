using System.Collections.Generic;
using Hearts.AI;
using Hearts.Collections;
using Hearts.Model;
using HeartsCrusher;

namespace Hearts.Console
{
    public class Settings
    {
        // Note: This simulates at about 300 games per second, without console outputs
        //      1 game  - 0s
        //  1,000 games - 3s
        // 10,000 games - 30s
        public static int GameSimulationCount = 250;

        public static bool ShowFullOutput = GameSimulationCount == 1;

        // Available bots:
        // (* = Doesn't currently build against current IAgent signature)
        //    • TerribleRandomAiAgent
        //    • Noob1AiExampleAgent
        //    • Noob2AiExampleAgent
        //    • Noob3AiExampleAgent
        //    • SavageBeast (Adam Hill)
        //    • NoobCrusher.Create(NoobCrusherVersion.v1) (Tony Beasley)
        //    • NoobCrusher.Create(NoobCrusherVersion.v2) (Tony Beasley)
        //    • Craghoul (Craig Rowe)

        public static List<Bot> Bots = new HeartsPlayerList
            {
                NoobCrusher.Create(NoobCrusherVersion.v2) ,
                new Deathstar(),
                new SavageBeast(true),
                new Craghoul()
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

        internal class HeartsPlayerList : List<Bot>
        {
            private char playerNamePrefix = 'A';

            public HeartsPlayerList()
            {
            }

            public HeartsPlayerList(IEnumerable<IAgent> agents)
            {
                this.AddRange(agents);
            }

            public new void Add(Bot bot)
            {
                base.Add(bot);
                this.playerNamePrefix++;
            }

            public void Add(IAgent agent)
            {
                this.Add(Bot.Create(new Player(playerNamePrefix + " (" + agent.AgentName + ")"), agent));
                playerNamePrefix++;
            }

            public void AddRange(IEnumerable<IAgent> agents)
            {
                foreach (var agent in agents)
                {
                    this.Add(agent);
                }
            }
        }
    }
}
