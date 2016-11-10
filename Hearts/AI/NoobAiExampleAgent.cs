using Hearts.Factories;
using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.AI
{
    public class NoobAiExampleAgent : IAgent
    {
        public string AgentName { get { return "Noob AI"; } }

        public List<Card> ChooseCardsToPass(List<Card> startingCards)
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

        public Card ChooseCardToPlay(Game gameState, List<Card> availableCards)
        {
            var remainingAvailableCards = availableCards.ToList();

            // Lead lowest club, assuming that a game manager intelligently selects the correct starting player
            if (gameState.IsFirstLeadHand)
            {
                return remainingAvailableCards.Where(i => i.Suit == Suit.Clubs).OrderBy(i => i.Kind).First();
            }

            // Cut cards down to matching suit if appropriate
            if (!gameState.IsLeadTurn)
            {
                var constrainedSuit = gameState.CurrentHand.First().Suit;

                if (remainingAvailableCards.Any(i => i.Suit == constrainedSuit))
                {
                    remainingAvailableCards = remainingAvailableCards.Where(i => i.Suit == constrainedSuit).ToList();
                }
                else
                {
                    // Let's make our noob AI at least slightly viscious
                    // Queen someone at the first opportunity
                    if (remainingAvailableCards.Any(i => i.Kind == Kind.Queen && i.Suit == Suit.Spades))
                    {
                        return remainingAvailableCards.Single(i => i.Kind == Kind.Queen && i.Suit == Suit.Spades);
                    }
                }
            }

            // Don't lead with a Heart if not broken
            if (!gameState.IsHeartsBroken && remainingAvailableCards.Any(i => i.Suit != Suit.Hearts))
            {
                remainingAvailableCards = remainingAvailableCards.Where(i => i.Suit != Suit.Hearts).ToList();
            }

            // Return any low card
            // Terrible plan in long term for a game, but gives a half chance of dodging the queen against other noob AIs
            return remainingAvailableCards.OrderBy(i => i.Kind).ThenByDescending(i => i.Suit).First();
        }
    }
}