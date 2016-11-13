using System.Collections.Generic;
using System.Linq;
using Hearts.Model;
using Hearts.Logging;

namespace Hearts.Console
{
    public class Simulator
    {
        public void SimulateGames(int simulationCount)
        {
            var players = Settings.Bots;
            var victories = players.ToDictionary(i => i, i => 0);

            for (int i = 0; i < simulationCount; i++)
            {
                var result = this.SimulateGame(players);
                int winningScore = result.Min(j => j.Value);

                foreach(var winner in result.Where(j => j.Value == result.Min(k => k.Value)))
                {
                    ++victories[winner.Key];
                }
            }

            Log.LogSimulationSummary(simulationCount, victories);
        }

        private Dictionary<Player, int> SimulateGame(IEnumerable<Player> players)
        {
            var game = new Game(players);
            var cumulativeScores = players.ToDictionary(i => i, i => 0);
            int roundNumber = 0;

            do
            {
                var result = game.Play(roundNumber);

                var moonShots = result.Scores.Where(i => i.Value == 26);

                if (moonShots.Any())
                {
                    const int MoonshotPoints = 26;
                    var shooter = moonShots.First().Key;
                    int shooterCumulativeScore = cumulativeScores[shooter];
                    var otherScores = cumulativeScores.Where(i => i.Key != shooter).ToList();
                    var otherScoresPlus26 = otherScores.Select(i => i.Value + 26).ToList();
                    bool hasShooterLostIfAddsScoreToOthers = 
                        otherScoresPlus26.Any(i => i >= 100) 
                        && shooterCumulativeScore > otherScoresPlus26.Min();

                    if (hasShooterLostIfAddsScoreToOthers)
                    {
                        cumulativeScores[shooter] -= MoonshotPoints;
                    }
                    else
                    {
                        foreach (var score in otherScores)
                        {
                            cumulativeScores[score.Key] += MoonshotPoints;
                        }
                    }
                }
                else
                {
                    foreach (var score in result.Scores)
                    {
                        cumulativeScores[score.Key] += score.Value;
                    }
                }

                ++roundNumber;

            } while (cumulativeScores.All(i => i.Value < 100));

            Log.LogFinalWinner(cumulativeScores);

            return cumulativeScores;
        }
    }
}
