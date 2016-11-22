using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Model
{
    public class Game
    {
        public List<Round> Rounds { get; set; }

        public Round CurrentRound { get { return this.Rounds.LastOrDefault(); } }

        // TODO: We'll be adding score records etc. here
    }
}
