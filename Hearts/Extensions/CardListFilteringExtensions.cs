using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Extensions
{
    public static class CardListFilteringExtensions
    {
        public static IEnumerable<Card> Missing(this IEnumerable<Card> self, Suit suit)
        {
            switch (suit)
            {
                case Suit.Hearts:
                    return Cards.Hearts.Except(self);
                case Suit.Spades:
                    return Cards.Spades.Except(self);
                case Suit.Diamonds:
                    return Cards.Diamonds.Except(self);
                case Suit.Clubs:
                default:
                    return Cards.Clubs.Except(self);
            }
        }

        public static IEnumerable<Card> Missing(this IEnumerable<Card> self)
        {
            return Cards.Deck.Except(self);
        }

        public static IEnumerable<Card> ExceptHighest(this IEnumerable<Card> self)
        {
            return self.OrderByDescending(i => i.Kind).Skip(1);
        }

        public static IEnumerable<Card> ExceptCard(this IEnumerable<Card> self, Card card)
        {
            return self.Except(new List<Card> { card });
        }

        public static IEnumerable<Card> ExceptCards(this IEnumerable<Card> self, Card cardA, Card cardB)
        {
            return self.Except(new List<Card> { cardA, cardB });
        }

        public static IEnumerable<Card> ExceptCards(this IEnumerable<Card> self, Card cardA, Card cardB, Card cardC)
        {
            return self.Except(new List<Card> { cardA, cardB, cardC });
        }

        public static IEnumerable<Card> ExceptHearts(this IEnumerable<Card> self)
        {
            return self.Except(Cards.Hearts);
        }
        public static IEnumerable<Card> ExceptSpades(this IEnumerable<Card> self)
        {
            return self.Except(Cards.Spades);
        }
        public static IEnumerable<Card> ExceptDiamonds(this IEnumerable<Card> self)
        {
            return self.Except(Cards.Diamonds);
        }
        public static IEnumerable<Card> ExceptClubs(this IEnumerable<Card> self)
        {
            return self.Except(Cards.Clubs);
        }

        public static IEnumerable<Card> OfSuit(this IEnumerable<Card> self, Suit suit)
        {
            return self.Where(i => i.Suit == suit);
        }

        public static IEnumerable<Card> Hearts(this IEnumerable<Card> self)
        {
            return self.OfSuit(Suit.Hearts);
        }

        public static IEnumerable<Card> Spades(this IEnumerable<Card> self)
        {
            return self.OfSuit(Suit.Spades);
        }

        public static IEnumerable<Card> Diamonds(this IEnumerable<Card> self)
        {
            return self.OfSuit(Suit.Diamonds);
        }

        public static IEnumerable<Card> Clubs(this IEnumerable<Card> self)
        {
            return self.OfSuit(Suit.Clubs);
        }
    }
}