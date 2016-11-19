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
    public class Game
    {
        private PlayerCircle playerCircle;
        private GameState gameState;
        private Dealer dealer;

        public Game(IEnumerable<Player> players)
        {
            this.playerCircle = new PlayerCircle();
            this.AddPlayers(players);
            this.Reset();
        }

        public void Reset()
        {
            this.dealer = new Dealer(new StandardDeckFactory(), new EvenHandDealAlgorithm());
            this.playerCircle.Reset();

            if (this.gameState != null)
            {
                this.gameState.Reset();
            }
        }

        public int RoundIndex { get; private set; }

        public RoundResult Play(int roundIndex)
        {
            this.Reset();
            this.RoundIndex = roundIndex;
            var players = this.playerCircle.AllPlayers;
            this.gameState = new GameState();
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
                this.gameState.BeginTrick();

                foreach (var player in this.playerCircle.GetOrderedPlayersStartingWith(startingPlayer))
                {
                    var legalCards = rulesEngine.GetPlayableCards(player.RemainingCards, this.gameState);
                    var card = player.Agent.ChooseCardToPlay(this.gameState, startingHands[player], player.RemainingCards, legalCards.ToList());
                    
                    if (!legalCards.Contains(card))
                    {
                        // TODO: Handle illegal move
                        Log.IllegalPlay(player, card);
                        player.AgentHasMadeIllegalMove = true;
                    }

                    player.Play(card);
                    this.gameState.Play(player, card);
                }

                this.gameState.EndTrick();
                var trick = this.gameState.PlayedTricks.Last();
                var trickWinner = handEvaluator.EvaluateWinner(trick);
                trick.Winner = trickWinner;
                startingPlayer = trick.Winner;

                Log.TrickSummary(trick);
            }

            var scores = players.ToDictionary(i => i, i => new ScoreEvaluator().CalculateScore(this.gameState.PlayedTricks.Where(j => j.Winner == i)));
            var tricks = players.ToDictionary(i => i, i => this.gameState.PlayedTricks.Where(j => j.Winner == i).ToList());

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
