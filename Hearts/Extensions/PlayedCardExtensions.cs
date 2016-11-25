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

        public static Suit? LeadSuit(this IEnumerable<PlayedCard> self)
        {
            var lead = self.FirstOrDefault();
            if (lead == null)
            {
                return null;
            }

            return lead.Card.Suit;
        }

        public static Card WinningCard(this IEnumerable<PlayedCard> self)
        {
            var leadSuit = self.LeadSuit();
            if (leadSuit.HasValue)
            {
                return self.SelectCards().OfSuit(leadSuit.Value).Descending().First();
            }

            return null;
        }
    }
}