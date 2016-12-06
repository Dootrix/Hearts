using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;
using Hearts.Model;

namespace Hearts.AI
{
    public class SuicideNoob1AiExampleAgent : IAgent
    {
        public string AgentName { get { return "Suicide Noob 1 AI"; } }

        public Player Player { get; set; }

        public IEnumerable<Card> ChooseCardsToPass(GameState gameState)
        {
            // Basic "pass your highest cards" strategy
            var cards = gameState.StartingCards.OrderBy(i => i.Kind).ThenBy(i => i.Suit).ToList();

            return new List<Card>
            {
                cards[0],
                cards[1],
                cards[2]
            };
        }

        public Card ChooseCardToPlay(GameState gameState)
        {
            var round = gameState.Game.CurrentRound;

            if (gameState.LegalCards.Count() == 1)
            {
                return gameState.LegalCards.First();
            }

            // Cut cards down to matching suit if appropriate
            if (!round.IsLeadTurn)
            {
                var constrainedSuit = round.CurrentTrick.First().Card.Suit;

                if (gameState.CurrentCards.Any(i => i.Suit == constrainedSuit))
                {
                    return gameState.LegalCards.Highest();
                }
            }

            // Return any low card
            // Terrible plan in long term for a game, but gives a half chance of dodging the queen against other noob AIs
            return gameState.LegalCards.OrderByDescending(i => i.Kind).ThenByDescending(i => i.Suit).First();
        }
    }
}