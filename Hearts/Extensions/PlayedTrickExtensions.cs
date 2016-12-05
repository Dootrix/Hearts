using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Extensions
{
    public static class PlayedTrickExtensions
    {
        public static IEnumerable<Card> SelectCards(this PlayedTrick self)
        {
            return self.Cards.Values;
        }

        public static IEnumerable<Card> SelectCards(this IEnumerable<PlayedTrick> self)
        {
            return self.SelectMany(i => i.Cards).Select(i => i.Value);
        }

        public static IEnumerable<Card> Hearts(this IEnumerable<PlayedTrick> self)
        {
            return self.SelectCards().Hearts();
        }

        public static IEnumerable<Card> Clubs(this IEnumerable<PlayedTrick> self)
        {
            return self.SelectCards().Clubs();
        }

        public static IEnumerable<Card> Diamonds(this IEnumerable<PlayedTrick> self)
        {
            return self.SelectCards().Diamonds();
        }
        public static IEnumerable<Card> Spades(this IEnumerable<PlayedTrick> self)
        {
            return self.SelectCards().Spades();
        }
    }
}