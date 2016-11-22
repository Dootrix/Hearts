using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;

namespace Hearts.AI.Strategies
{
    public class FollowWithoutSuitStrategy : IPlayStrategy
    {
        public Card ChooseCardToPlay(Round round, IEnumerable<Card> availableCards, IEnumerable<Card> Legal)
        {
            Card cardToPlay = null;

            var idealCards = GetHighSpades().ToList();

            var hearts = Cards.Hearts.OrderByDescending(x => x.Kind);
            idealCards.AddRange(hearts);

            foreach (var idealCard in idealCards)
            {
                if (Legal.Contains(idealCard))
                {
                    cardToPlay = idealCard;
                    break;
                }
            }

            if (cardToPlay == null)
            {
                // assume legal cards are all cards in this situation, but this ain't always true!
                cardToPlay = this.GetSuitVoiders(Legal, 1).First();
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

        private IEnumerable<Card> GetSuitVoiders(IEnumerable<Card> cards, int number)
        {
            var suitGroupedCards = cards
                .GroupBy(x => x.Suit)
                .OrderBy(y => y.Count());

            return suitGroupedCards
                .SelectMany(x => x.AsEnumerable().OrderByDescending(y => y.Kind))
                .Take(number)
                .ToList();
        }
    }
}
