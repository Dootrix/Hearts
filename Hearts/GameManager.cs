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
using Hearts.AI;

namespace Hearts
{
    public class GameManager
    {
        private PlayerCircle playerCircle;
        private Dictionary<Player, PlayerState> playerCards;
        private readonly Dictionary<Player, IAgent> playerAgentLookup = new Dictionary<Player, IAgent>();
        private Round round;
        private Dealer dealer;

        public GameManager(IEnumerable<Bot> bots)
        {
            this.playerCircle = new PlayerCircle();
            this.AddBots(bots);
            this.Reset();
        }

        public void Reset()
        {
            this.dealer = new Dealer(new StandardDeckFactory(), new EvenHandDealAlgorithm());
            this.playerCards = this.playerCircle.AllPlayers.ToDictionary(i => i, i => new PlayerState());

            if (this.round != null)
            {
                this.round.Reset();
            }
        }

        public RoundResult Play(int roundNumber)
        {
            this.Reset();
            var players = this.playerCircle.AllPlayers;
            this.round = new Round(players.Count, roundNumber);
            var startingHands = this.dealer.DealStartingHands(players);

            foreach (var startingHand in startingHands)
            {
                this.playerCards[startingHand.Key].Starting = startingHand.Value.ToList();
                this.playerCards[startingHand.Key].Current = startingHand.Value.ToList();
            }

            Log.StartingHands(startingHands);

            this.PerformPass(startingHands);

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
                    var agent = this.playerAgentLookup[player];
                    var card = agent.ChooseCardToPlay(new GameState(player, new Game { Rounds = new List<Round> { this.round } }, this.playerCards[player]));

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

        private void PerformPass(Dictionary<Player, IEnumerable<Card>> startingHands)
        {
            int roundNumber = this.round.RoundNumber;

            var passService = new PassService(this.playerAgentLookup);
            var pass = passService.GetPass(roundNumber, this.round.NumberOfPlayers);

            Log.PassDirection(pass);

            if (pass != Pass.NoPass)
            {
                var postPassHands = passService.OrchestratePassing(
                roundNumber,
                this.playerCards,
                this.playerCircle.FirstPlayer,
                this.round);

                foreach (var postPassHand in postPassHands)
                {
                    this.playerCards[postPassHand.Key].PostPass = postPassHand.Value.ToList();
                    this.playerCards[postPassHand.Key].Current = postPassHand.Value.ToList();
                }

                Log.HandsAfterPass(this.playerCards.ToDictionary(i => i.Key, i => i.Value.PostPass));
            }
            else
            {
                foreach (var startingHand in startingHands)
                {
                    this.playerCards[startingHand.Key].PostPass = startingHand.Value.ToList();
                }
            }
        }

        private void AddBots(IEnumerable<Bot> bots)
        {
            foreach (var bot in bots)
            {
                this.playerAgentLookup[bot.Player] = bot.Agent;
                this.playerCircle.AddPlayer(bot.Player);
            }
        }
    }
}
