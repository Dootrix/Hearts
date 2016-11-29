using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Extensions
{
    public static class CardListRetrievalExtensions
    {
        public static Card Get(this IEnumerable<Card> self, Card card)
        {
            return self.FirstOrDefault(_ => _ == card);
        }

        public static Card Highest(this IEnumerable<Card> self)
        {
            return self.OrderByDescending(i => i.Kind).FirstOrDefault();
        }

        public static Card NextHighest(this IEnumerable<Card> self)
        {
            return self.Highest() + 1;
        }

        public static Card HighestButOne(this IEnumerable<Card> self)
        {
            return self.OrderByDescending(i => i.Kind).Skip(1).Take(1).SingleOrDefault();
        }

        public static Card HighestUnderCard(this IEnumerable<Card> self, Card card)
        {
            return self.Where(i => i.Suit == card.Suit && i.Kind < card.Kind).Highest();
        }

        public static bool HasCardUnder(this IEnumerable<Card> self, Card card)
        {
            return self.Any(i => i.Suit == card.Suit && i.Kind < card.Kind);
        }

        public static Card Lowest(this IEnumerable<Card> self)
        {
            return self.OrderBy(i => i.Kind).FirstOrDefault();
        }

        public static Card NextLowest(this IEnumerable<Card> self)
        {
            return self.Lowest() - 1;
        }

        public static Suit? FirstSuit(this IEnumerable<Card> self)
        {
            var firstCard = self.FirstOrDefault();
            if (firstCard == null)
            {
                return null;
            }

            return firstCard.Suit;
        }
    }
}