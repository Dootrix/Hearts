using Hearts.Model;
using System.Collections.Generic;
using System.Linq;
using Hearts.Collections;

namespace Hearts.Extensions
{
    public static class CardListOrderingExtensions
    {
        public static IEnumerable<Card> Ascending(this IEnumerable<Card> self)
        {
            return self.OrderBy(i => i.Kind);
        }

        public static IEnumerable<Card> Descending(this IEnumerable<Card> self)
        {
            return self.OrderByDescending(i => i.Kind);
        }

        public static IEnumerable<Card> TwoThenDescendingClubs(this IEnumerable<Card> self)
        {
            return self.Contains(Cards.TwoOfClubs) 
                ? new List<Card> { Cards.TwoOfClubs }.Concat(self.Where(i => i.Suit == Suit.Clubs && i.Kind != Kind.Two).Descending()) 
                : self.Clubs().Descending().ToList();
        }

        public static IEnumerable<Card> GroupBySuitAmountDescending(this IEnumerable<Card> self)
        {
           var groupedCards = self.GroupBy(_ => _.Suit);
           var orderedGroupedCards = groupedCards.OrderByDescending(_ => _.Count());

            return orderedGroupedCards.SelectMany(_ => _);
        }

        public static IEnumerable<Card> GroupBySuitAmountAscending(this IEnumerable<Card> self)
        {
            var groupedCards = self.GroupBy(_ => _.Suit);
            var orderedGroupedCards = groupedCards.OrderBy(_ => _.Count());

            return orderedGroupedCards.SelectMany(_ => _);
        }

        public static IEnumerable<Card> GroupBySuitDescending(this IEnumerable<Card> self, params Suit[] suitOrder)
        {
            var result = new UniqueList<Card>();
            foreach (var suit in suitOrder)
            {
                result.AddRange(self.OfSuit(suit).Descending());
            }

            // Add any suits not specified
            result.AddRange(self.OrderBy(i => i.Suit).ThenByDescending(_ => _.Kind));

            return result;
        }

        public static IEnumerable<Card> GroupBySuitAscending(this IEnumerable<Card> self, params Suit[] suitOrder)
        {
            var result = new UniqueList<Card>();
            foreach (var suit in suitOrder)
            {
                result.AddRange(self.OfSuit(suit).Ascending());
            }

            // Add any suits not specified
            result.AddRange(self.OrderBy(i => i.Suit).ThenBy(_ => _.Kind));

            return result;
        }
    }
}