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
using System.Diagnostics;

namespace Hearts
{
    public class GameManager
    {
        private PlayerCircle playerCircle;
        private Dictionary<Player, PlayerState> playerCards;
        private readonly Dictionary<Player, IAgent> playerAgentLookup = new Dictionary<Player, IAgent>();
        private Round round;
        private Dealer dealer;
        private HandWinEvaluator handEvaluator;
        private GameRulesEngine rulesEngine;

        public GameManager(IEnumerable<Bot> bots)
        {
            this.playerCircle = new PlayerCircle();
            this.handEvaluator = new HandWinEvaluator();
            this.rulesEngine = new GameRulesEngine();
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

        public RoundResult Play(int roundNumber, Dictionary<Player, List<int>> passTimings, Dictionary<Player, List<int>> playTimings)
        {
            this.Reset();
            var players = this.playerCircle.AllPlayers;

            var roundResult = new RoundResult(players, roundNumber);

            this.round = new Round(players.Count, roundNumber);
            var startingHands = this.dealer.DealStartingHands(players);

            foreach (var startingHand in startingHands)
            {
                this.playerCards[startingHand.Owner].Starting = startingHand.ToList();
                this.playerCards[startingHand.Owner].Current = startingHand.ToList();
            }

            Log.StartingHands(startingHands);

            this.PerformPass(startingHands, passTimings);
            
            var startingPlayer = this.playerCircle.GetStartingPlayer(this.playerCards);

            while (this.playerCards.Select(i => i.Value.Current.Count()).Sum() > 0)
            {
                this.round.BeginTrick();

                foreach (var player in this.playerCircle.GetOrderedPlayersStartingWith(startingPlayer))
                {
                    var playerRemaining = this.playerCards[player].Current;
                    this.playerCards[player].Legal = this.rulesEngine.GetPlayableCards(playerRemaining, this.round);
                    var agent = this.playerAgentLookup[player];
                    var stopwatch = Stopwatch.StartNew();
                    var card = agent.ChooseCardToPlay(new GameState(player, new Game { Rounds = new List<Round> { this.round } }, this.playerCards[player]));
                    stopwatch.Stop();
                    playTimings[player].Add(Convert.ToInt32(stopwatch.ElapsedMilliseconds));

                    if (!this.playerCards[player].Legal.Contains(card))
                    {
                        // TODO: Handle illegal move
                        Log.IllegalPlay(player, card);
                        player.AgentHasMadeIllegalMove = true;
                        card = this.playerCards[player].Legal.First();
                    }

                    this.playerCards[player].Current = playerRemaining.ExceptCards(card);

                    this.round.Play(player, card);
                }

                this.round.EndTrick();
                var trick = this.round.PlayedTricks.Last();
                var trickWinner = this.handEvaluator.EvaluateWinner(trick);
                trick.Winner = trickWinner;
                startingPlayer = trick.Winner;

                Log.TrickSummary(trick);
            }

            var scores = players.ToDictionary(i => i, i => this.round.PlayedTricks.Where(j => j.Winner == i).SelectCards().Score());
            var tricks = players.ToDictionary(i => i, i => this.round.PlayedTricks.Where(j => j.Winner == i).ToList()); // TODO: I think we can probably remove Tricks from roundResult
            roundResult.Scores = scores;
            roundResult.Tricks = tricks;

            foreach (var player in scores.Where(i => i.Value == 26).Select(i => i.Key))
            {
                roundResult.Shooters.Add(player);
            }

            foreach (var player in scores.Where(i => i.Value == roundResult.Scores.Min(j => j.Value)).Select(i => i.Key))
            {
                // +26 is actually a winning score, so account for moonshots swapping definition of winner / loser
                if (roundResult.Shooters.Any())
                {
                    roundResult.Losers.Add(player);
                }
                else
                {
                    roundResult.Winners.Add(player);
                }
                
            }

            foreach (var player in scores.Where(i => i.Value == roundResult.Scores.Max(j => j.Value)).Select(i => i.Key))
            {
                // +26 is actually a winning score, so account for moonshots swapping definition of winner / loser
                if (roundResult.Shooters.Any())
                {
                    roundResult.Winners.Add(player);
                }
                else
                {
                    roundResult.Losers.Add(player);
                }
            }

            // TODO: Raise event OnRoundComplete

            Log.PointsForRound(roundResult);


            return roundResult;
        }

        private void PerformPass(IEnumerable<CardHand> startingHands, Dictionary<Player, List<int>> passTimings)
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
                this.round,
                passTimings);

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
                    this.playerCards[startingHand.Owner].PostPass = startingHand.ToList();
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
