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
        private Dictionary<Player, IEnumerable<Card>> startingHands;
        private Dictionary<Player, IEnumerable<Card>> postPassHands;
        private Dictionary<Player, IEnumerable<Card>> remainingCards;
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
            this.startingHands = new Dictionary<Player, IEnumerable<Card>>();
            this.postPassHands = new Dictionary<Player, IEnumerable<Card>>();
            this.remainingCards = new Dictionary<Player, IEnumerable<Card>>();

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
            this.startingHands = this.dealer.DealStartingHands(players);
            this.remainingCards = this.startingHands.ToDictionary(i => i.Key, i => i.Value.ToList().AsEnumerable()); // Simple Clone

            Log.StartingHands(this.startingHands);

            this.postPassHands = new PassService().OrchestratePassing(roundIndex, players, startingHands, this.playerCircle.FirstPlayer);

            Log.HandsAfterPass(this.postPassHands);

            var handEvaluator = new HandWinEvaluator();
            var rulesEngine = new GameRulesEngine();
            var startingPlayer = this.playerCircle.GetStartingPlayer(this.startingHands);

            while (this.remainingCards.Select(i => i.Value.Count()).Sum() > 0)
            {
                this.round.BeginTrick();

                foreach (var player in this.playerCircle.GetOrderedPlayersStartingWith(startingPlayer))
                {
                    var legalCards = rulesEngine.GetPlayableCards(this.remainingCards[player], this.round);
                    var card = player.Agent.ChooseCardToPlay(this.round, this.startingHands[player], this.remainingCards[player], legalCards.ToList());

                    if (!legalCards.Contains(card))
                    {
                        // TODO: Handle illegal move
                        Log.IllegalPlay(player, card);
                        player.AgentHasMadeIllegalMove = true;
                        card = legalCards.First();
                    }

                    this.remainingCards[player] = this.remainingCards[player].Except(new List<Card> { card });

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
