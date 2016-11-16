using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.AI.Strategies
{
    public class FollowSuitStrategy : IPlayStrategy
    {
        public Card ChooseCardToPlay(Game gameState, List<Card> availableCards, List<Card> legalCards)
        {
            Card cardToPlay = null;

            var leadSuit = gameState.CurrentTrick.First().Card.Suit;

            // following suit
            var playedCards = gameState.PlayedTricks.SelectMany(x => x.Cards.Select(y => y.Value));

            if (playedCards.Count(x => x.Suit == leadSuit) == 0)
            {
                // play high if the suit hasn't already been led.
                cardToPlay = legalCards
                    .Except(GetHighSpades())
                    .OrderByDescending(x => x.Kind)
                    .FirstOrDefault();
            }

            if (cardToPlay == null)
            {
                var currentWinner = gameState.CurrentTrick.Select(x => x.Card).Max(y => y.Kind);

                // highest card below the current winner.
                cardToPlay = legalCards.Where(x => x.Kind < currentWinner).OrderByDescending(y => y.Kind).FirstOrDefault();

                // otherwise play the lowest...
                if (cardToPlay == null)
                    cardToPlay = legalCards.OrderBy(x => x.Kind).First();
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
