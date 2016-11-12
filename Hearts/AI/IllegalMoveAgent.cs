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
        
        public List<Card> ChooseCardsToPass(List<Card> startingCards, Pass pass)
        {
            return startingCards.RandomSelection(2);
        }

        public Card ChooseCardToPlay(Game gameState, List<Card> startingCards, List<Card> availableCards, List<Card> legalCards)
        {
            var illegalCards = availableCards.Except(legalCards).ToList();

            return illegalCards.Any() ? illegalCards.Random() : legalCards.Random();
        }
    }
}