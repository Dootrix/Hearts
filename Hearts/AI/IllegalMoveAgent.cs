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

        public IEnumerable<Card> ChooseCardsToPass(GameState gameState)
        {
            return gameState.StartingCards.RandomSelection(2);
        }

        public Card ChooseCardToPlay(GameState gameState)
        {
            var ilLegal = gameState.CurrentCards.Except(gameState.LegalCards).ToList();

            return ilLegal.Any() ? ilLegal.Random() : gameState.LegalCards.Random();
        }
    }
}