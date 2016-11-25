using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;

namespace Hearts.Scoring
{
    public class GameResult
    {
        public GameResult(IEnumerable<Player> players, int gameNumber)
        {
            this.GameNumber = gameNumber;
            this.Scores = players.ToDictionary(i => i, i => 0);
            this.RoundWins = players.ToDictionary(i => i, i => 0);
            this.RoundLosses = players.ToDictionary(i => i, i => 0);
            this.Moonshots = players.ToDictionary(i => i, i => 0);
            this.Winners = new List<Player>();
            this.Losers = new List<Player>();
        }

        public int GameNumber { get; set; }
        public int RoundsPlayed { get; set; }
        public Dictionary<Player, int> Scores { get; set; }
        public Dictionary<Player, int> RoundWins { get; set; }
        public Dictionary<Player, int> RoundLosses { get; set; }
        public Dictionary<Player, int> Moonshots { get; set; }
        public List<Player> Winners { get; set; }
        public List<Player> Losers { get; set; }
    }
}
