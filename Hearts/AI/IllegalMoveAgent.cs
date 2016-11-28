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

        public IEnumerable<Card> ChooseCardsToPass(GameState gameState)
        {
            return gameState.StartingCards.Take(2);
        }

        public Card ChooseCardToPlay(GameState gameState)
        {
            var illegal = gameState.CurrentCards.Except(gameState.LegalCards).ToList();

            return illegal.Any() ? illegal.First() : gameState.LegalCards.First();
        }
    }
}