using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;

namespace Hearts.AI.Strategies
{
    class LeadCardStrategy : IPlayStrategy
    {
        public Card ChooseCardToPlay(Round gameState, IEnumerable<Card> availableCards, IEnumerable<Card> legalCards)
        {
            var playedCards = gameState
                .PlayedTricks
                .SelectMany(x => x.Cards.Select(y => y.Value));

            Card cardToPlay = null;

            // attempt to play a high-card if the suit hasn't yet been played.
            foreach (var suit in new Suit[] { Suit.Clubs, Suit.Diamonds, Suit.Spades })
            {
                if (!playedCards.Any(x => x.Suit == suit))
                {
                    cardToPlay = legalCards
                        .Where(x => x.Suit == suit)
                        .Except(GetHighSpades())
                        .OrderByDescending(y => y.Kind)
                        .FirstOrDefault();
                }

                if (cardToPlay != null)
                {
                    break;
                }
            }

            // otherwise ...
            if (cardToPlay == null)
            {
                // play low
                cardToPlay = legalCards.OrderBy(i => i.Kind).First();
            }

            return cardToPlay;
        }

        private IEnumerable<Card> GetHighSpades()
        {
            return new List<Card> {
                    new Card(Kind.Queen, Suit.Spades),
                    new Card(Kind.King, Suit.Spades),
                    new Card(Kind.Ace, Suit.Spades),
                };
        }
    }
}
