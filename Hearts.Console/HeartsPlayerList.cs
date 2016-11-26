using System.Collections.Generic;
using Hearts.AI;
using Hearts.Model;

namespace Hearts.Console
{
    public class HeartsPlayerList : List<Bot>
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
            base.Add(Bot.Create(new Player((playerNamePrefix + " : " + agent.AgentName).PadRight(25, ' ')), agent));
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