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
        public IEnumerable<Card> FilterCards(IEnumerable<Card> cards, Game gameState)
        {
            return new List<Card> { cards.Where(i => i.Suit == Suit.Clubs).OrderBy(i => i.Kind).First() };
        }

        public bool Applies(Game gameState)
        {
            return gameState.IsFirstLeadHand;
        }
    }
}
