using System.Linq;
using Hearts.Model;

namespace Hearts.Scoring
{
    public class HandWinEvaluator
    {
        public Player EvaluateWinner(PlayedTrick playedHand)
        {
            var leadSuit = playedHand.Cards.First().Value.Suit;
            var cardsOfLeadSuit = playedHand.Cards.Where(i => i.Value.Suit == leadSuit);
            var highestRanked = cardsOfLeadSuit.OrderByDescending(i => i.Value.Kind).First();

            return highestRanked.Key;
        }
    }
}
