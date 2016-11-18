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
        public IEnumerable<Card> FilterCards(IEnumerable<Card> cards, GameState gameState)
        {
            var leadSuit = gameState.CurrentTrick.First().Card.Suit;
            var sameSuitCards = cards.Where(i => i.Suit == leadSuit).ToList();

            return sameSuitCards.Any() ? sameSuitCards : cards;
        }

        public bool Applies(GameState gameState)
        {
            return !gameState.IsLeadTurn;
        }
    }
}
