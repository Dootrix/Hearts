using Hearts.AI;
using Hearts.Model;
using Hearts.Performance;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Scoring
{
    public class SimulationResult
    {
        public SimulationResult(
            List<GameResult> gameResults,
            IEnumerable<Bot> bots,
            TimerService timerService)
        {
            this.GameResults = gameResults;
            this.MoonshotAttempts = this.GetMoonshotAttempts(bots).ToList();
            this.TimerService = timerService;
        }

        public List<GameResult> GameResults { get; private set; }

        public List<MoonshotAttempt> MoonshotAttempts { get; private set; }

        public TimerService TimerService { get; private set; }

        private IEnumerable<MoonshotAttempt> GetMoonshotAttempts(IEnumerable<Bot> bots)
        {
            foreach (var bot in bots)
            {
                if (bot.Agent is IReportsShootAttempts)
                {
                    yield return new MoonshotAttempt { Bot = bot, MoonshotAttempts = ((IReportsShootAttempts)bot.Agent).GetShootAttempts() };
                }
            }
        }
    }
}
