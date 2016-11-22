using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Rules
{
    public class CannotPlayQueenSpadesOrHeartsOnFirstHandIfPossibleRule : IGameRule
    {
        public IEnumerable<Card> FilterCards(IEnumerable<Card> cards, Round round)
        {
            var queenOfSpades = new Card(Kind.Queen, Suit.Spades);
            bool hasQueen = cards.Contains(queenOfSpades);
            var nonQueenSpadesCards = cards.Where(i => i != queenOfSpades);
            var nonQueenAndNonHeartsCards = nonQueenSpadesCards.Where(i => i.Suit != Suit.Hearts);

            return nonQueenAndNonHeartsCards.Any() ? nonQueenAndNonHeartsCards
                : hasQueen ? new List<Card> { queenOfSpades }
                : nonQueenSpadesCards;
        }

        public bool Applies(Round round)
        {
            return round.IsFirstHand;
        }
    }
}
