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
            var ilLegal = cards.Current.Except(cards.Legal).ToList();

            return ilLegal.Any() ? ilLegal.Random() : cards.Legal.Random();
        }
    }
}