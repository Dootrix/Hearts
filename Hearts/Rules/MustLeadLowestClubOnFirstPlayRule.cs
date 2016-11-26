using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Rules
{
    public class MustLeadLowestClubOnFirstPlayRule : IGameRule
    {
        public IEnumerable<Card> FilterCards(IEnumerable<Card> cards, Round round)
        {
            return cards.Any(i => i.Suit == Suit.Clubs) ? new List<Card> { cards.Where(i => i.Suit == Suit.Clubs).OrderBy(i => i.Kind).First() } : cards;
        }

        public bool Applies(Round round)
        {
            return round.IsFirstLeadHand;
        }
    }
}
