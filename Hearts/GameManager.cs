using Hearts.Deal;
using Hearts.Factories;
using Hearts.Model;
using Hearts.Passing;
using Hearts.Rules;
using Hearts.Scoring;
using System;
using System.Collections.Generic;
using System.Linq;
using Hearts.Logging;

namespace Hearts
{
    public class GameManager
    {
        private PlayerCircle playerCircle;
        private Round round;
        private Dealer dealer;

        public GameManager(IEnumerable<Player> players)
        {
            this.playerCircle = new PlayerCircle();
            this.AddPlayers(players);
            this.Reset();
        }

        public void Reset()
        {
            this.dealer = new Dealer(new StandardDeckFactory(), new EvenHandDealAlgorithm());
            this.playerCircle.Reset();

            if (this.round != null)
            {
                this.round.Reset();
            }
        }

        public RoundResult Play(int roundIndex)
        {
            this.Reset();
            var players = this.playerCircle.AllPlayers;
            this.round = new Round(players.Count, roundIndex);
            this.dealer.DealStartingHands(players);
            var startingHands = players.ToDictionary(i => i, i => i.RemainingCards.ToList());
            
            Log.StartingHands(startingHands);

            new PassService().OrchestratePassing(roundIndex, players, startingHands, this.playerCircle.FirstPlayer);

            Log.HandsAfterPass(players.ToDictionary(i => i, i => i.RemainingCards.ToList()));

            var handEvaluator = new HandWinEvaluator();
            var rulesEngine = new GameRulesEngine();
            var startingPlayer = this.playerCircle.GetStartingPlayer();
            
            while (players.Sum(i => i.RemainingCards.Count) > 0)
            {
                this.round.BeginTrick();

                foreach (var player in this.playerCircle.GetOrderedPlayersStartingWith(startingPlayer))
                {
                    var legalCards = rulesEngine.GetPlayableCards(player.RemainingCards, this.round);
                    var card = player.Agent.ChooseCardToPlay(this.round, startingHands[player], player.RemainingCards, legalCards.ToList());
                    
                    if (!legalCards.Contains(card))
                    {
                        // TODO: Handle illegal move
                        Log.IllegalPlay(player, card);
                        player.AgentHasMadeIllegalMove = true;
                    }

                    player.Play(card);
                    this.round.Play(player, card);
                }

                this.round.EndTrick();
                var trick = this.round.PlayedTricks.Last();
                var trickWinner = handEvaluator.EvaluateWinner(trick);
                trick.Winner = trickWinner;
                startingPlayer = trick.Winner;

                Log.TrickSummary(trick);
            }

            var scores = players.ToDictionary(i => i, i => new ScoreEvaluator().CalculateScore(this.round.PlayedTricks.Where(j => j.Winner == i)));
            var tricks = players.ToDictionary(i => i, i => this.round.PlayedTricks.Where(j => j.Winner == i).ToList());

            Log.PointsForRound(scores);

            return new RoundResult
            {
                Scores = scores,
                Tricks = tricks
            };
        }

        private void AddPlayers(IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                this.playerCircle.AddPlayer(player);
            }
        }
    }
}
