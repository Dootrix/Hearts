using System.Collections.Generic;
using System.Linq;
using Hearts.Model;

namespace Hearts.Scoring
{
    public class SimulationResult
    {
        public SimulationResult(IEnumerable<Bot> bots)
        {
            this.GameResults = new List<GameResult>();
            this.PassTimings = bots.ToDictionary(i => i.Player, i => new List<int>());
            this.PlayTimings = bots.ToDictionary(i => i.Player, i => new List<int>());
        }

        public List<GameResult> GameResults { get; set; }

        public Dictionary<Player, List<int>> PassTimings { get; set; }

        public Dictionary<Player, List<int>> PlayTimings { get; set; }
    }
}
