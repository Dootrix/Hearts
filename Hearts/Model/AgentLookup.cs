using Hearts.AI;
using System.Collections.Generic;

namespace Hearts.Model
{
    public class AgentLookup
    {
        private readonly Dictionary<Player, IAgent> playerAgentLookup = new Dictionary<Player, IAgent>();

        public void AssociateAgentWithPlayer(IAgent agent, Player player)
        {
            this.playerAgentLookup[player] = agent;
        }

        public IAgent GetAgent(Player player)
        {
            return this.playerAgentLookup[player];
        }
    }
}
