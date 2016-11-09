using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Rules
{
    public class MustFollowSuitIfPossibleRule : IGameRule
    {
        public IEnumerable<Card> FilterCards(IEnumerable<Card> cards, Game gameState)
        {
            var leadSuit = gameState.CurrentHand.First().Suit;
            var sameSuitCards = cards.Where(i => i.Suit == leadSuit);

            return sameSuitCards.Count() > 0 ? sameSuitCards : cards;
        }

        public bool Applies(Game gameState)
        {
            return gameState.IsFollowTurn;
        }
    }
}
