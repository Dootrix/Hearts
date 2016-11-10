using Hearts.Factories;
using Hearts.Model;
using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;

namespace Hearts.AI
{
    public class TerribleRandomAiAgent : IAgent
    {
        public string AgentName { get { return "Random AI"; } }
        
        public List<Card> ChooseCardsToPass(List<Card> startingCards)
        {
            return startingCards.RandomSelection(3);
        }

        public Card ChooseCardToPlay(Game gameState, List<Card> startingCards, List<Card> availableCards, List<Card> legalCards)
        {
            return legalCards.Random();
        }
    }
}