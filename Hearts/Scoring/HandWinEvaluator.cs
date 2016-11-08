using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;

namespace Hearts.Scoring
{
    public class HandWinEvaluator
    {
        public Guid EvaluateWinner(PlayedHand playedHand)
        {
            var leadSuit = playedHand.Cards.First().Value.Suit;
            var cardsOfLeadSuit = playedHand.Cards.Where(i => i.Value.Suit == leadSuit);
            var highestRanked = cardsOfLeadSuit.OrderByDescending(i => i.Value.Kind).First();

            return highestRanked.Key;
        }
    }
}
