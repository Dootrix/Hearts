﻿using System.Collections.Generic;
using System.Linq;
using Hearts.Model;
using Hearts.Logging;
using Hearts.Scoring;

namespace Hearts.Console
{
    public class Simulator
    {
        private const int MoonshotPoints = 26;
        private const int GameLosingPoints = 100;

        public void SimulateGames(IEnumerable<Bot> bots, int simulationCount)
        {
            var simulationResult = new SimulationResult();

            for (int i = 0; i < simulationCount; i++)
            {
                var gameResult = this.SimulateGame(bots, i + 1);
                simulationResult.GameResults.Add(gameResult);
            }

            Log.LogSimulationSummary(simulationResult);
        }

        private GameResult SimulateGame(IEnumerable<Bot> bots, int gameNumber)
        {
            var gameManager = new GameManager(bots);
            var gameResult = new GameResult(bots.Select(i => i.Player), gameNumber);
            int roundNumber = 1;
            bool gameHasEnded;

            do
            {
                var roundResult = gameManager.Play(roundNumber);

                foreach (var player in bots.Select(i => i.Player))
                {
                    int playerScore = roundResult.Scores[player];

                    if (playerScore < MoonshotPoints)
                    {
                        gameResult.Scores[player] += playerScore;
                    }
                    else
                    {
                        ++gameResult.Moonshots[player];
                        int shooterCumulativeScore = gameResult.Scores[player];
                        var otherScores = gameResult.Scores.Where(i => i.Key != player).ToList();
                        var otherScoresPlus26 = otherScores.Select(i => i.Value + MoonshotPoints).ToList();
                        bool hasShooterLostIfAddsScoreToOthers = otherScoresPlus26.Any(i => i >= GameLosingPoints) && shooterCumulativeScore > otherScoresPlus26.Min();

                        if (hasShooterLostIfAddsScoreToOthers)
                        {
                            gameResult.Scores[player] -= MoonshotPoints;
                        }
                        else
                        {
                            foreach (var otherPlayer in otherScores.Select(i => i.Key))
                            {
                                gameResult.Scores[otherPlayer] += MoonshotPoints;
                            }
                        }
                    }

                    gameResult.RoundWins[player] += roundResult.Winners.Count(i => i == player); // TODO: make sure what ever sets winners and losers understands to account for 26pts elsewhere and on self
                    gameResult.RoundLosses[player] += roundResult.Losers.Count(i => i == player); // TODO: make sure what ever sets winners and losers understands to account for 26pts elsewhere and on self
                }

                gameHasEnded = gameResult.Scores.Any(i => i.Value >= GameLosingPoints);

                if (gameHasEnded)
                {
                    foreach (var player in gameResult.Scores.Where(i => i.Value == gameResult.Scores.Min(j => j.Value)).Select(i => i.Key))
                    {
                        gameResult.Winners.Add(player);
                    }

                    foreach (var player in gameResult.Scores.Where(i => i.Value == gameResult.Scores.Max(j => j.Value)).Select(i => i.Key))
                    {
                        gameResult.Losers.Add(player);
                    }
                }

                ++gameResult.RoundsPlayed;
                ++roundNumber;
            } while (!gameHasEnded);

            Log.LogFinalWinner(gameResult);

            return gameResult;
        }
    }
}
