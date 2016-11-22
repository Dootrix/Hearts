using Hearts.Factories;
using Hearts.Model;
using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;

namespace Hearts.AI
{
    public class IllegalMoveAgent : IAgent
    {
        public string AgentName { get { return "Illegal AI"; } }

        public Player Player { get; set; }

        public IEnumerable<Card> ChooseCardsToPass(IEnumerable<Card> startingCards, Pass pass)
        {
            return startingCards.RandomSelection(2);
        }

        public Card ChooseCardToPlay(Round round, PlayerCards cards)
        {
            var illegalCards = cards.RemainingCards.Except(cards.LegalCards).ToList();

            return illegalCards.Any() ? illegalCards.Random() : cards.LegalCards.Random();
        }
    }
}