using Hearts.Logging;
using Hearts.Model;
using Hearts.Performance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hearts.Passing
{
    public class PassService
    {
        private readonly PlayerStateManager playerStateManager;
        private readonly AgentLookup agentLookup;
        private readonly TimerService timerService;

        private List<List<Pass>> passSchedule = new List<List<Pass>>
        {
            new List<Pass> { Pass.NoPass },
            new List<Pass> { Pass.OneToLeft, Pass.OneToRight },
            new List<Pass> { Pass.OneToLeft, Pass.OneToRight, Pass.NoPass },
            new List<Pass> { Pass.OneToLeft, Pass.OneToRight, Pass.TwoToLeft, Pass.NoPass },
            new List<Pass> { Pass.OneToLeft, Pass.OneToRight, Pass.TwoToLeft, Pass.TwoToRight, Pass.NoPass },
            new List<Pass> { Pass.NoPass },
        };

        private Dictionary<Pass, Func<Player, Player>> passFunctions = new Dictionary<Pass, Func<Player, Player>>
        {
            { Pass.OneToLeft, (i) => { return i.NextPlayer; } },
            { Pass.OneToRight, (i) => { return i.PreviousPlayer; } },
            { Pass.NoPass, (i) => { return i; } },
            { Pass.TwoToLeft, (i) => { return i.NextPlayer.NextPlayer; } },
            { Pass.TwoToRight, (i) => { return i.PreviousPlayer.PreviousPlayer; }}
        };

        public PassService(
            PlayerStateManager playerStateManager,
            AgentLookup agentLookup,
            TimerService timerService)
        {
            this.playerStateManager = playerStateManager;
            this.agentLookup = agentLookup;
            this.timerService = timerService;
        }

        public Pass GetPass(int roundNumber, int playerCount)
        {
            return this.passSchedule[playerCount - 1][(roundNumber - 1) % playerCount];
        }

        public IEnumerable<CardHand> OrchestratePassing(
            IEnumerable<CardHand> cardHands,
            Round round)
        {
            round.Pass = this.GetPass(round.RoundNumber, cardHands.Count());

            var cardHandsToPass = this.GetCardHandsToPass(round, cardHands);

            var cardHandsAfterPass = this.PassCards(round, cardHands, cardHandsToPass);

            return cardHandsAfterPass;
        }

        private IEnumerable<CardHand> GetCardHandsToPass(Round round, IEnumerable<CardHand> cardHands)
        {
            var cardHandsToPass = new List<CardHand>();

            foreach (var cardHand in cardHands)
            {
                var playerFrom = cardHand.Owner;

                var gameState = this.CreateGameState(playerFrom, round);
                var agent = this.agentLookup.GetAgent(playerFrom);

                var timer = this.timerService.StartNewPassTimer(playerFrom);

                var cardsToPass = new CardHand(
                    playerFrom,
                    agent.ChooseCardsToPass(gameState));

                timer.Stop();

                if (!IsPassLegal(cardHand, cardsToPass))
                {
                    // TODO: Handle illegal move
                    Log.IllegalPass(playerFrom, cardsToPass);
                    playerFrom.AgentHasMadeIllegalMove = true;
                }

                Log.Pass(playerFrom, cardsToPass);

                cardHandsToPass.Add(cardsToPass);
            }

            return cardHandsToPass;
        }

        private IEnumerable<CardHand> PassCards(
            Round round,
            IEnumerable<CardHand> cardHands,
            IEnumerable<CardHand> cardHandsToPass)
        {
            int playerCount = cardHands.Count();

            var cardHandsLookup = cardHands.ToDictionary(x => x.Owner);

            foreach (var cardHandToPass in cardHandsToPass)
            {
                var playerFrom = cardHandToPass.Owner;
                var playerTo = this.GetPassRecipient(round.RoundNumber, playerCount, playerFrom);

                cardHandsLookup[playerFrom].RemoveRange(cardHandToPass);
                cardHandsLookup[playerTo].AddRange(cardHandToPass);
            }

            return cardHandsLookup.Values;
        }

        private Player GetPassRecipient(int roundNumber, int playerCount, Player fromPlayer)
        {
            var passFunction = this.passFunctions[this.GetPass(roundNumber, playerCount)];

            return passFunction(fromPlayer);
        }

        private GameState CreateGameState(Player player, Round round)
        {
            var playerState = this.playerStateManager.GetPlayerState(player);

            return new GameState(
                player, 
                new Game { Rounds = new List<Round> { round } }, 
                playerState);
        }

        private static bool IsPassLegal(CardHand startingHand, IEnumerable<Card> cardsToPass)
        {
            return cardsToPass.Count() == 3
                && cardsToPass.Distinct().Count() == 3
                && cardsToPass.All(x => startingHand.Contains(x));
        }
    }
}
