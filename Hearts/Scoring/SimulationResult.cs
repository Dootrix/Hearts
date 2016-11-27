using Hearts.Performance;
using System.Collections.Generic;

namespace Hearts.Scoring
{
    public class SimulationResult
    {
        public SimulationResult(
            List<GameResult> gameResults,
            TimerService timerService)
        {
            this.GameResults = gameResults;
            this.TimerService = timerService;          
        }

        public List<GameResult> GameResults { get; private set; }

        public TimerService TimerService { get; private set; }
    }
}
