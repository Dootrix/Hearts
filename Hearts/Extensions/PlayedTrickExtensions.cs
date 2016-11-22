using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Extensions
{
    public static class PlayedTrickExtensions
    {
        public static IEnumerable<Card> SelectCards(this IEnumerable<PlayedTrick> self)
        {
            return self.SelectMany(i => i.Cards).Select(i => i.Value);
        }
    }
}