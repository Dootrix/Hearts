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
            return playedHands
                .SelectMany(i => i.Cards)
                .Where(i => i.Value.Suit == Suit.Hearts)
                .Count() 
                + (playedHands
                    .SelectMany(i => i.Cards)
                    .Any(i => i.Value == new Card(Kind.Queen, Suit.Spades)) ? 13 : 0);
        }
    }
}
