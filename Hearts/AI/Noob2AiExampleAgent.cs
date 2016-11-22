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

        public Card ChooseCardToPlay(Round round, PlayerCards cardss)
        {
            if (cards.LegalCards.Count() == 1)
            {
                return cards.LegalCards.First();
            }

            // Cut cards down to matching suit if appropriate
            if (!round.IsLeadTurn)
            {
                var constrainedSuit = round.CurrentTrick.First().Card.Suit;

                if (cards.RemainingCards.Any(i => i.Suit == constrainedSuit))
                {
                    return cards.LegalCards.Lowest();
                }
                else
                {
                    // Let's make our noob AI at least slightly viscious
                    // Queen someone at the first opportunity
                    if (cards.LegalCards.Any(i => i.Kind == Kind.Queen && i.Suit == Suit.Spades))
                    {
                        return cards.LegalCards.Single(i => i.Kind == Kind.Queen && i.Suit == Suit.Spades);
                    }
                    else
                    {
                        // Noob 2 is slightly improved in that it punishes people with Hearts at the first opportunity
                        if (cards.LegalCards.Any(i => i.Suit == Suit.Hearts))
                        {
                            return cards.LegalCards.Where(i => i.Suit == Suit.Hearts).OrderByDescending(i => i.Kind).First();
                        }
                    }
                }
            }

            // Return any low card
            // Terrible plan in long term for a game, but gives a half chance of dodging the queen against other noob AIs
            return cards.LegalCards.OrderBy(i => i.Kind).ThenByDescending(i => i.Suit).First();
        }
    }
}