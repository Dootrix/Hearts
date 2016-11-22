using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Extensions
{
    public static class CardListContainsExtensions
    {
        public static bool Has(this IEnumerable<Card> self, Card card)
        {
            return self.Get(card) != null;
        }

        public static bool HasQueenSpades(this IEnumerable<Card> self)
        {
            return self.Any(i => i.Suit == Suit.Spades && i.Kind == Kind.Queen);
        }

        public static bool HasKingSpades(this IEnumerable<Card> self)
        {
            return self.Any(i => i.Suit == Suit.Spades && i.Kind == Kind.King);
        }

        public static bool HasAceSpades(this IEnumerable<Card> self)
        {
            return self.Any(i => i.Suit == Suit.Spades && i.Kind == Kind.Ace);
        }

        public static bool HasHighSpade(this IEnumerable<Card> self)
        {
            return self.HasAceSpades() || self.HasKingSpades();
        }

        public static bool HasTwoClubs(this IEnumerable<Card> self)
        {
            return self.Any(i => i.Suit == Suit.Clubs && i.Kind == Kind.Two);
        }

    }
}