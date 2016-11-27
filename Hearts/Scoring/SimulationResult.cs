using System.Collections.Generic;
using System.Linq;
using Hearts.Model;

namespace Hearts.Scoring
{
    public class SimulationResult
    {
        public SimulationResult(List<GameResult> gameResults, Timing timing)
        {
            this.GameResults = gameResults;
            this.Timing = timing;
        }

        public List<GameResult> GameResults { get; private set; }

        public Timing Timing { get; private set; }
    }
}
