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
        public RoundResult(IEnumerable<Player> players, int roundNumber)
        {
            this.RoundNumber = roundNumber;
            this.Scores = players.ToDictionary(i => i, i => 0);
            this.Winners = new List<Player>();
            this.Losers = new List<Player>();
            this.Shooters = new List<Player>();
            this.Tricks = new Dictionary<Player, List<PlayedTrick>>();
        }

        public int RoundNumber { get; set; }
        public Dictionary<Player, int> Scores { get; set; }
        public List<Player> Winners { get; set; }
        public List<Player> Losers { get; set; }
        public List<Player> Shooters { get; set; }
        public Dictionary<Player, List<PlayedTrick>> Tricks { get; set; }
    }
}
