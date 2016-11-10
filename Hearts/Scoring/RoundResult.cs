using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;

namespace Hearts.Scoring
{
    public class RoundResult
    {
        public Dictionary<Player, int> Scores { get; set; }

        public Dictionary<Player, List<PlayedTrick>> Tricks { get; set; }
    }
}
