using Hearts.AI;

namespace Hearts.Model
{
    public class Bot
    {
        private readonly AgentFactory agentFactory;

        public Bot(Player player, AgentFactory agentFactory)
        {
            this.Player = player;
            this.Agent = agentFactory.Create();
            this.agentFactory = agentFactory;
        }

        public Bot Clone(Player player)
        {
            return new Bot(player, this.agentFactory);
        }

        public Player Player { get; private set; }

        public IAgent Agent { get; private set; }

        public void ReinstantiateAgent()
        {
            this.Agent = this.agentFactory.Create();
        }
    }
}