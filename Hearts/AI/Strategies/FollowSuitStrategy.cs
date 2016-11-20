using Hearts.Extensions;
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
        public Card ChooseCardToPlay(GameState gameState, IEnumerable<Card> availableCards, IEnumerable<Card> legalCards)
        {
            Card cardToPlay = null;

            var currentCards = gameState.CurrentTrick.SelectCards().ToArray();
            var leadSuit = currentCards.First().Suit;

            var playedCards = gameState.PlayedTricks.SelectCards().ToArray();

            if (currentCards.Length == gameState.NumberOfPlayers - 1)
            {
                // we are last to play for this trick.
                int pointsToWin = gameState.CurrentTrick.Count(x => x.Card.Suit == Suit.Hearts)
                    + (currentCards.Any(x => x == Cards.QueenOfSpades) ? 13 : 0);

                // if there is nothing to win, then play high.
                if (pointsToWin == 0)
                {
                    cardToPlay = legalCards.OrderByDescending(x => x.Kind).FirstOrDefault();
                }
            }

            if (cardToPlay == null && playedCards.Count(x => x.Suit == leadSuit) == 0)
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
