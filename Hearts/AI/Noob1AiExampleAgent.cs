using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;
using Hearts.Factories;
using Hearts.Model;

namespace Hearts.AI
{
    public class Noob1AiExampleAgent : IAgent
    {
        public string AgentName { get { return "Noob 1 AI"; } }

        public Player Player { get; set; }

        public IEnumerable<Card> ChooseCardsToPass(IEnumerable<Card> startingCards, Pass pass)
        {
            // Basic "pass your highest cards" strategy
            var cards = startingCards.OrderByDescending(i => i.Kind).ThenBy(i => i.Suit).ToList();

            return new List<Card>
            {
                cards[0],
                cards[1],
                cards[2]
            };
        }

        public Card ChooseCardToPlay(Round gameState, PlayerHolding holding)
        {
            if (holding.LegalCards.Count() == 1)
            {
                return holding.LegalCards.First();
            }

            // Cut cards down to matching suit if appropriate
            if (!gameState.IsLeadTurn)
            {
                var constrainedSuit = gameState.CurrentTrick.First().Card.Suit;

                if (holding.RemainingCards.Any(i => i.Suit == constrainedSuit))
                {
                    return holding.LegalCards.Lowest();
                }
                else
                {
                    // Let's make our noob AI at least slightly viscious
                    // Queen someone at the first opportunity
                    if (holding.LegalCards.Any(i => i.Kind == Kind.Queen && i.Suit == Suit.Spades))
                    {
                        return holding.LegalCards.Single(i => i.Kind == Kind.Queen && i.Suit == Suit.Spades);
                    }
                }
            }

            // Return any low card
            // Terrible plan in long term for a game, but gives a half chance of dodging the queen against other noob AIs
            return holding.LegalCards.OrderBy(i => i.Kind).ThenByDescending(i => i.Suit).First();
        }
    }
}