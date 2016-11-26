using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Rules
{
    public class MustFollowSuitIfPossibleRule : IGameRule
    {
        public IEnumerable<Card> FilterCards(IEnumerable<Card> cards, Round round)
        {
            var leadSuit = round.CurrentTrick.First().Card.Suit;
            var sameSuitCards = cards.Where(i => i.Suit == leadSuit).ToList();

            return sameSuitCards.Any() ? sameSuitCards : cards;
        }

        public bool Applies(Round round)
        {
            return !round.IsLeadTurn;
        }
    }
}
