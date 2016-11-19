using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Scoring;

namespace Hearts.Extensions
{
    public static class CardListExtensions
    {
        public static int Score(this IEnumerable<Card> self)
        {
            return new ScoreEvaluator().CalculateScore(self);
        }

        public static Card Highest(this IEnumerable<Card> self)
        {
            return self.OrderByDescending(i => i.Kind).First();
        }

        public static Card HighestButOne(this IEnumerable<Card> self)
        {
            return self.OrderByDescending(i => i.Kind).Skip(1).Take(1).SingleOrDefault();
        }

        public static Card Lowest(this IEnumerable<Card> self)
        {
            return self.OrderBy(i => i.Kind).First();
        }

        public static IEnumerable<Card> Ascending(this IEnumerable<Card> self)
        {
            return self.OrderBy(i => i.Kind);
        }

        public static IEnumerable<Card> Descending(this IEnumerable<Card> self)
        {
            return self.OrderByDescending(i => i.Kind);
        }

        public static List<Card> TwoThenDescendingClubs(this IEnumerable<Card> self)
        {
            bool hasTwo = self.Contains(Cards.TwoOfClubs);
            var selfMinusTwo = self.Where(i => i.Suit == Suit.Clubs && i.Kind == Kind.Two).OrderByDescending(i => i.Kind);
            var twoThenRest = new List<Card> { Cards.TwoOfClubs };
            twoThenRest.AddRange(selfMinusTwo);

            return hasTwo ? twoThenRest : self.OrderByDescending(i => i.Kind).ToList();
        }

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

        public static IEnumerable<Card> Hearts(this IEnumerable<Card> self)
        {
            return self.Where(i => i.Suit == Suit.Hearts);
        }

        public static IEnumerable<Card> Spades(this IEnumerable<Card> self)
        {
            return self.Where(i => i.Suit == Suit.Spades);
        }

        public static IEnumerable<Card> Diamonds(this IEnumerable<Card> self)
        {
            return self.Where(i => i.Suit == Suit.Diamonds);
        }

        public static IEnumerable<Card> Clubs(this IEnumerable<Card> self)
        {
            return self.Where(i => i.Suit == Suit.Clubs);
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