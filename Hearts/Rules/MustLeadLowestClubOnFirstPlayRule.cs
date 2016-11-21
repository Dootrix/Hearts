using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Rules
{
    public class MustLeadLowestClubOnFirstPlayRule : IGameRule
    {
        public IEnumerable<Card> FilterCards(IEnumerable<Card> cards, Round gameState)
        {
            return cards.Any(i => i.Suit == Suit.Clubs) ? new List<Card> { cards.Where(i => i.Suit == Suit.Clubs).OrderBy(i => i.Kind).First() } : cards;
        }

        public bool Applies(Round gameState)
        {
            return gameState.IsFirstLeadHand;
        }
    }
}
