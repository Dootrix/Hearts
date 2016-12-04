using Hearts.Deal;
using Hearts.Events;
using Hearts.Extensions;
using Hearts.Factories;
using Hearts.Logging;
using Hearts.Model;
using Hearts.Passing;
using Hearts.Performance;
using Hearts.Randomisation;
using Hearts.Rules;
using Hearts.Scoring;
using System.Collections.Generic;
using System.Linq;

namespace Hearts
{
    public class GameManager
    {
        private readonly AgentLookup agentLookup;
        private readonly HandWinEvaluator handEvaluator;
        private readonly GameRulesEngine rulesEngine;
        private readonly EventNotifier notifier;
        private readonly PlayerCircle playerCircle;
        private readonly PlayerStateManager playerStateManager;
        private Round round;
        private Dealer dealer;
        private TimerService timerService;
        private IControlledRandom random;

        public GameManager(
            IEnumerable<Bot> bots,
            TimerService timerService,
            EventNotifier notifier, 
            IControlledRandom random)
        {
            this.playerCircle = new PlayerCircle();
            this.handEvaluator = new HandWinEvaluator();
            this.rulesEngine = new GameRulesEngine();
            this.playerStateManager = new PlayerStateManager();
            this.agentLookup = new AgentLookup();
            this.notifier = notifier;
            this.random = random;
            this.timerService = timerService;
            this.AddBots(bots);
            this.Reset();
        }

        public RoundResult Play(int roundNumber)
        {
            this.notifier.CallRoundStarted();
            this.Reset();
            var players = this.playerCircle.AllPlayers;

            this.round = new Round(players.Count, roundNumber);
            var startingHands = this.dealer.DealStartingHands(players, this.random);

            this.playerStateManager.SetStartingHands(startingHands);

            Log.StartingHands(startingHands);

            var postPassHands = this.GetPostPassHands(startingHands);
            
            var startingPlayer = this.playerCircle.GetStartingPlayer(postPassHands);

            while (this.playerStateManager.GetRemainingCardCount() > 0)
            {
                startingPlayer = OrchestrateRound(startingPlayer);
            }

            var roundResult = CreateRoundResult(players, roundNumber);

            // TODO: Raise event OnRoundComplete

            Log.PointsForRound(roundResult);

            this.notifier.CallRoundEnded();

            return roundResult;
        }

        private RoundResult CreateRoundResult(IEnumerable<Player> players, int roundNumber)
        {
            var roundResult = new RoundResult(players, roundNumber);
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

            return roundResult;
        }

        private void Reset()
        {
            this.dealer = new Dealer(new StandardDeckFactory(), new EvenHandDealAlgorithm(), this.random);

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
                var playerState = this.playerStateManager.GetPlayerState(player);
                this.OrchestratePlayForPlayer(player, playerState);
            }

            this.round.EndTrick();

            var trick = this.round.PlayedTricks.Last();
            var trickWinner = this.handEvaluator.EvaluateWinner(trick);
            trick.Winner = trickWinner;
            startingPlayer = trick.Winner;

            Log.TrickSummary(trick);

            return startingPlayer;
        }

        private void OrchestratePlayForPlayer(Player player, PlayerState playerState)
        {
            var currentHand = playerState.Current;

            playerState.Legal = this.rulesEngine.GetPlayableCards(currentHand, this.round);

            var gameState = new GameState(
                player, 
                new Game { Rounds = new List<Round> { this.round } }, 
                playerState);

            var timer = this.timerService.StartNewPlayTimer(player);

            var agent = this.agentLookup.GetAgent(player);
            var card = agent.ChooseCardToPlay(gameState);

            timer.Stop();

            if (!playerState.Legal.Contains(card))
            {
                // TODO: Handle illegal move
                Log.IllegalPlay(player, card);
                player.AgentHasMadeIllegalMove = true;
                card = playerState.Legal.First();
            }
          
            currentHand.Remove(card);

            this.round.Play(player, card);
        }

        private IEnumerable<CardHand> GetPostPassHands(IEnumerable<CardHand> startingHands)
        {
            int roundNumber = this.round.RoundNumber;

            var passService = new PassService(
                this.playerStateManager, 
                this.agentLookup, 
                this.timerService,
                this.notifier);

            return passService.OrchestratePassing(startingHands, this.round);
        }

        private void AddBots(IEnumerable<Bot> bots)
        {
            foreach (var bot in bots)
            {
                this.agentLookup.AssociateAgentWithPlayer(bot.Agent, bot.Player);
                this.playerCircle.AddPlayer(bot.Player);
            }
        }
    }
}
