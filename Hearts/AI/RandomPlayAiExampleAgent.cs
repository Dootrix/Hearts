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

        public IEnumerable<Card> ChooseCardsToPass(IEnumerable<Card> startingCards, Pass pass)
        {
            return startingCards.RandomSelection(3);
        }

        public Card ChooseCardToPlay(Round gameState, IEnumerable<Card> startingCards, IEnumerable<Card> availableCards, IEnumerable<Card> legalCards)
        {
            return legalCards.Random();
        }
    }
}