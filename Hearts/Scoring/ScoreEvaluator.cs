using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;

namespace Hearts.Scoring
{
    public class ScoreEvaluator
    {
        public int CalculateScore(IEnumerable<PlayedTrick> playedHands)
        {
            return this.CalculateScore(playedHands.SelectMany(i => i.Cards).Select(i => i.Value));
        }

        public int CalculateScore(IEnumerable<Card> cards)
        {
            return cards.Where(i => i.Suit == Suit.Hearts).Count()
                + (cards.Any(i => i == new Card(Kind.Queen, Suit.Spades)) ? 13 : 0);
        }
    }
}
