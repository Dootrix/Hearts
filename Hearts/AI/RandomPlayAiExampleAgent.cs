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

        public IEnumerable<Card> ChooseCardsToPass(GameState gameState)
        {
            return gameState.StartingCards.RandomSelection(3);
        }

        public Card ChooseCardToPlay(GameState gameState)
        {
            return gameState.LegalCards.Random();
        }
    }
}