using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

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
                ? new List<Card> { Cards.TwoOfClubs }.Union(self.Where(i => i.Suit == Suit.Clubs && i.Kind != Kind.Two).Descending()) 
                : self.Clubs().Descending().ToList();
        }
    }
}