using System.Collections.Generic;
using Hearts.AI;
using Hearts.Model;

namespace Hearts.Console
{
    public class HeartsPlayerList : List<Bot>
    {
        private const int NamePadLength = 25;
        private char playerNamePrefix = 'A';

        public HeartsPlayerList()
        {
        }

        public HeartsPlayerList(List<AgentFactory> agents)
        {
            foreach (var agent in agents)
            {
                this.Add(agent);
            }
        }

        public new void Add(Bot bot)
        {
            base.Add(bot);

            this.playerNamePrefix++;
        }

        public void Add(AgentFactory agentFactory)
        {
            base.Add(new Bot(new Player((this.playerNamePrefix + " : " + agentFactory.AgentName).PadRight(NamePadLength, ' ')), agentFactory));

            this.playerNamePrefix++;
        }

        public void AddRange(List<IAgent> agents)
        {
            foreach (var agent in agents)
            {
                this.Add(new AgentFactory(() => Agent.CreateAgent(agent.GetType())));
            }
        }
    }
}