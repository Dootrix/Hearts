using Hearts.Deal;
using Hearts.Factories;
using Hearts.Model;
using Hearts.Passing;
using Hearts.Rules;
using Hearts.Scoring;
using System;
using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;
using Hearts.Logging;

namespace Hearts
{
    public class GameManager
    {
        private PlayerCircle playerCircle;
        private Dictionary<Player, PlayerCards> playerCards;
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
            this.playerCards = this.playerCircle.AllPlayers.ToDictionary(i => i, i => new PlayerCards());

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
            var startingHands = this.dealer.DealStartingHands(players);

            foreach (var startingHand in startingHands)
            {
                this.playerCards[startingHand.Key].Starting = startingHand.Value;
                this.playerCards[startingHand.Key].Current = startingHand.Value.ToList();
            }

            // TODO: Logging - reinstate
            //Log.StartingHands(this.startingHands);

            foreach (var postPassHand in new PassService().OrchestratePassing(roundIndex, this.playerCards, this.playerCircle.FirstPlayer, this.round))
            {
                this.playerCards[postPassHand.Key].PostPass = postPassHand.Value;
            }

            // TODO: Logging - reinstate
            //Log.HandsAfterPass(this.postPassHands);

            var handEvaluator = new HandWinEvaluator();
            var rulesEngine = new GameRulesEngine();
            var startingPlayer = this.playerCircle.GetStartingPlayer(this.playerCards);

            while (this.playerCards.Select(i => i.Value.Current.Count()).Sum() > 0)
            {
                this.round.BeginTrick();

                foreach (var player in this.playerCircle.GetOrderedPlayersStartingWith(startingPlayer))
                {
                    var playerRemaining = this.playerCards[player].Current;
                    this.playerCards[player].Legal = rulesEngine.GetPlayableCards(playerRemaining, this.round);
                    var card = player.Agent.ChooseCardToPlay(new GameState(this.round, this.playerCards[player]));

                    if (!this.playerCards[player].Legal.Contains(card))
                    {
                        // TODO: Handle illegal move
                        Log.IllegalPlay(player, card);
                        player.AgentHasMadeIllegalMove = true;
                        card = this.playerCards[player].Legal.First();
                    }

                    this.playerCards[player].Current = playerRemaining.ExceptCard(card);

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
