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

        public IEnumerable<Card> ChooseCardsToPass(GameState gameState)
        {
            // Basic "pass your highest cards" strategy
            var cards = gameState.Cards.Starting.OrderByDescending(i => i.Kind).ThenBy(i => i.Suit).ToList();

            return new List<Card>
            {
                cards[0],
                cards[1],
                cards[2]
            };
        }

        public Card ChooseCardToPlay(GameState gameState)
        {
            var round = gameState.Round;
            var cards = gameState.Cards;

            if (cards.Legal.Count() == 1)
            {
                return cards.Legal.First();
            }

            // Cut cards down to matching suit if appropriate
            if (!round.IsLeadTurn)
            {
                var constrainedSuit = round.CurrentTrick.First().Card.Suit;

                if (cards.Current.Any(i => i.Suit == constrainedSuit))
                {
                    return cards.Legal.Lowest();
                }
                else
                {
                    // Let's make our noob AI at least slightly viscious
                    // Queen someone at the first opportunity
                    if (cards.Legal.Any(i => i.Kind == Kind.Queen && i.Suit == Suit.Spades))
                    {
                        return cards.Legal.Single(i => i.Kind == Kind.Queen && i.Suit == Suit.Spades);
                    }
                }
            }

            // Return any low card
            // Terrible plan in long term for a game, but gives a half chance of dodging the queen against other noob AIs
            return cards.Legal.OrderBy(i => i.Kind).ThenByDescending(i => i.Suit).First();
        }
    }
}