using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hearts.Deal;
using Hearts.Factories;
using Hearts.Model;
using Hearts.Passing;
using Hearts.Rules;
using Hearts.Scoring;
using Hearts.Extensions;
using Hearts.Logging;
using Hearts.AI;
using Hearts.Events;

namespace Hearts
{
    public class GameManager
    {
        private readonly Dictionary<Player, IAgent> playerAgentLookup = new Dictionary<Player, IAgent>();
        private readonly HandWinEvaluator handEvaluator;
        private readonly GameRulesEngine rulesEngine;
        private readonly EventNotifier notifier;
        private PlayerCircle playerCircle;
        private Dictionary<Player, PlayerState> playerCards;
        private Round round;
        private Dealer dealer;
        private Timing timing;

        public GameManager(IEnumerable<Bot> bots, Timing timing, EventNotifier notifier)
        {
            this.playerCircle = new PlayerCircle();
            this.handEvaluator = new HandWinEvaluator();
            this.rulesEngine = new GameRulesEngine();
            this.notifier = notifier;
            this.timing = timing;
            this.AddBots(bots);
            this.Reset();
        }

        public RoundResult Play(int roundNumber)
        {
            this.notifier.CallRoundStarted();
            this.Reset();
            var players = this.playerCircle.AllPlayers;

            var roundResult = new RoundResult(players, roundNumber);

            this.round = new Round(players.Count, roundNumber);
            var startingHands = this.dealer.DealStartingHands(players);

            foreach (var startingHand in startingHands)
            {
                var playerState = this.playerCards[startingHand.Owner];
                playerState.Starting = startingHand.ToList();
                playerState.Current = startingHand.ToList();
            }

            Log.StartingHands(startingHands);

            var postPassHands = this.GetPostPassHands(startingHands, this.timing.PassTimings);
            
            var startingPlayer = this.playerCircle.GetStartingPlayer(postPassHands);

            while (this.playerCards.Select(i => i.Value.Current.Count()).Sum() > 0)
            {
                startingPlayer = OrchestrateRound(startingPlayer);
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

            this.notifier.CallRoundEnded();

            return roundResult;
        }

        private void Reset()
        {
            this.dealer = new Dealer(new StandardDeckFactory(), new EvenHandDealAlgorithm());
            this.playerCards = this.playerCircle.AllPlayers.ToDictionary(i => i, i => new PlayerState());

            if (this.round != null)
            {
                this.round.Reset();
            }
        }

        private Player OrchestrateRound(Player startingPlayer)
        {
            this.round.BeginTrick();

            foreach (var player in this.playerCircle.GetOrderedPlayersStartingWith(startingPlayer))
            {
                this.OrchestratePlayForPlayer(player);
            }

            this.round.EndTrick();

            var trick = this.round.PlayedTricks.Last();
            var trickWinner = this.handEvaluator.EvaluateWinner(trick);
            trick.Winner = trickWinner;
            startingPlayer = trick.Winner;

            Log.TrickSummary(trick);

            return startingPlayer;
        }

        private void OrchestratePlayForPlayer(Player player)
        {
            var playerState = this.playerCards[player];

            var currentHand = playerState.Current;

            playerState.Legal = this.rulesEngine.GetPlayableCards(currentHand, this.round);

            var agent = this.playerAgentLookup[player];
            
            var gameState = new GameState(
                player, 
                new Game { Rounds = new List<Round> { this.round } }, 
                playerState);

            var stopwatch = Stopwatch.StartNew();

            var card = agent.ChooseCardToPlay(gameState);
            
            stopwatch.Stop();
            this.timing.PlayTimings[player].Add(Convert.ToInt32(stopwatch.ElapsedMilliseconds));

            if (!playerState.Legal.Contains(card))
            {
                // TODO: Handle illegal move
                Log.IllegalPlay(player, card);
                player.AgentHasMadeIllegalMove = true;
                card = playerState.Legal.First();
            }

            playerState.Current = currentHand.ExceptCards(card);

            this.round.Play(player, card);
        }

        private IEnumerable<CardHand> GetPostPassHands(
            IEnumerable<CardHand> startingHands, 
            Dictionary<Player, List<int>> passTimings)
        {
            int roundNumber = this.round.RoundNumber;

            var passService = new PassService(this.playerAgentLookup);
            var pass = passService.GetPass(roundNumber, this.round.NumberOfPlayers);

            Log.PassDirection(pass);

            IEnumerable<CardHand> postPassHands;

            if (pass != Pass.NoPass)
            {
                postPassHands = passService.OrchestratePassing(
                    this.playerCards,
                    this.playerCircle.FirstPlayer,
                    this.round,
                    passTimings);

                Log.HandsAfterPass(postPassHands);
            }
            else
            {
                postPassHands = startingHands;
            }

            foreach (var postPassHand in postPassHands)
            {
                var playerState = this.playerCards[postPassHand.Owner];
                playerState.PostPass = postPassHand.ToList();
                playerState.Current = postPassHand.ToList();
            }

            if (pass == Pass.NoPass)
            {
                this.notifier.CallNoPass();
            }

            return postPassHands;
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
