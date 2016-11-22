using Hearts.AI;

namespace Hearts.Model
{
    public class Bot
    {
        public static Bot Create(Player player, IAgent agent)
        {
            return new Bot(player, agent);
        }

        public Bot(Player player, IAgent agent)
        {
            this.Player = player;
            this.Agent = agent;
        }

        public Player Player { get; private set; }

        public IAgent Agent { get; private set; }
    }
}
