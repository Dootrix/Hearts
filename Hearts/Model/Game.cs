using System.Collections.Generic;
using System.Linq;

namespace Hearts.Model
{
    public class Game
    {
        public List<Round> Rounds { get; set; }

        public Round CurrentRound { get { return this.Rounds.LastOrDefault(); } }

        // TODO: We'll be adding score records etc. here
    }
}
