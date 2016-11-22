using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Extensions
{
    public static class PlayedCardExtensions
    {
        public static IEnumerable<Card> SelectCards(this IEnumerable<PlayedCard> self)
        {
            return self.Select(i => i.Card);
        }
    }
}