using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;
using Hearts.Factories;
using Hearts.Model;

namespace Hearts.AI
{
    public class Noob2AiExampleAgent : IAgent
    {
        public string AgentName { get { return "Noob 2 AI"; } }

        public Player Player { get; set; }

        public IEnumerable<Card> ChooseCardsToPass(GameState gameState)
        {
            // Basic "pass your highest cards" strategy
            var cards = gameState.StartingCards.OrderByDescending(i => i.Kind).ThenBy(i => i.Suit).ToList();

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
                    return gameState.LegalCards.Lowest();
                }
                else
                {
                    // Let's make our noob AI at least slightly viscious
                    // Queen someone at the first opportunity
                    if (gameState.LegalCards.Any(i => i.Kind == Kind.Queen && i.Suit == Suit.Spades))
                    {
                        return gameState.LegalCards.Single(i => i.Kind == Kind.Queen && i.Suit == Suit.Spades);
                    }
                    else
                    {
                        // Noob 2 is slightly improved in that it punishes people with Hearts at the first opportunity
                        if (gameState.LegalCards.Any(i => i.Suit == Suit.Hearts))
                        {
                            return gameState.LegalCards.Where(i => i.Suit == Suit.Hearts).OrderByDescending(i => i.Kind).First();
                        }
                    }
                }
            }

            // Return any low card
            // Terrible plan in long term for a game, but gives a half chance of dodging the queen against other noob AIs
            return gameState.LegalCards.OrderBy(i => i.Kind).ThenByDescending(i => i.Suit).First();
        }
    }
}