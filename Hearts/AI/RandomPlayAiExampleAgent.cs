using Hearts.Factories;
using Hearts.Model;
using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;

namespace Hearts.AI
{
    public class RandomPlayAiExampleAgent : IAgent
    {
        public string AgentName { get { return "Random AI"; } }

        public Player Player { get; set; }

        public IEnumerable<Card> ChooseCardsToPass(Round round, IEnumerable<Card> startingCards)
        {
            return startingCards.RandomSelection(3);
        }

        public Card ChooseCardToPlay(Round round, PlayerCards playerCards)
        {
            return playerCards.Legal.Random();
        }
    }
}