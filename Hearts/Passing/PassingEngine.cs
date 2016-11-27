using Hearts.AI;
using Hearts.Logging;
using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hearts.Passing
{
    public class PassService
    {
        private readonly IDictionary<Player, IAgent> playerAgentLookup;
        private readonly Timing timing;

        public List<List<Pass>> PassSchedule = new List<List<Pass>>
        {
            new List<Pass> { Pass.NoPass },
            new List<Pass> { Pass.OneToLeft, Pass.OneToRight },
            new List<Pass> { Pass.OneToLeft, Pass.OneToRight, Pass.NoPass },
            new List<Pass> { Pass.OneToLeft, Pass.OneToRight, Pass.TwoToLeft, Pass.NoPass },
            new List<Pass> { Pass.OneToLeft, Pass.OneToRight, Pass.TwoToLeft, Pass.TwoToRight, Pass.NoPass },
            new List<Pass> { Pass.NoPass },
        };

        public Dictionary<Pass, Func<Player, Player>> PassFunctions = new Dictionary<Pass, Func<Player, Player>>
        {
            { Pass.OneToLeft, (i) => { return i.NextPlayer; } },
            { Pass.OneToRight, (i) => { return i.PreviousPlayer; } },
            { Pass.NoPass, (i) => { return i; } },
            { Pass.TwoToLeft, (i) => { return i.NextPlayer.NextPlayer; } },
            { Pass.TwoToRight, (i) => { return i.PreviousPlayer.PreviousPlayer; }}
        };

        public PassService(
            IDictionary<Player, IAgent> playerAgentLookup,
            Timing timing)
        {
            this.playerAgentLookup = playerAgentLookup;
            this.timing = timing;
        }

        public Player GetPassRecipient(int roundNumber, int playerCount, Player fromPlayer)
        {
            var passFunction = this.PassFunctions[this.GetPass(roundNumber, playerCount)];

            return passFunction(fromPlayer);
        }

        public Pass GetPass(int roundNumber, int playerCount)
        {
            return this.PassSchedule[playerCount - 1][(roundNumber -1) % playerCount];
        }

        public IEnumerable<CardHand> OrchestratePassing(
            Dictionary<Player, PlayerState> playerCards, 
            Player playerFrom, 
            Round round)
        {
            var players = playerCards.Select(i => i.Key).ToList();
            var result = playerCards.ToDictionary(i => i.Key, i => playerCards[i.Key].Starting);

            var passedCards = new List<IEnumerable<Card>>();
            
            for (int i = 0; i < players.Count; i++)
            {
                round.Pass = this.GetPass(round.RoundNumber, players.Count);
                var agent = this.playerAgentLookup[playerFrom];
                var stopwatch = Stopwatch.StartNew();
                var playerState = playerCards[playerFrom];
                var cardsToPass = agent.ChooseCardsToPass(new GameState(playerFrom, new Game { Rounds = new List<Round> { round } }, playerState));
                stopwatch.Stop();
                timing.RecordPassTime(playerFrom, stopwatch.ElapsedMilliseconds);

                if (!cardsToPass.All(j => playerCards[playerFrom].Starting.Contains(j)) || cardsToPass.Count() != 3 || cardsToPass.Distinct().Count() != 3)
                {
                    // TODO: Handle illegal move
                    Log.IllegalPass(playerFrom, cardsToPass);
                    playerFrom.AgentHasMadeIllegalMove = true;
                }

                Log.Pass(playerFrom, cardsToPass);

                passedCards.Add(cardsToPass);
                result[playerFrom] = result[playerFrom].Except(cardsToPass).ToList();


                if (i < players.Count - 1)
                {
                    playerFrom = players[i + 1];
                }
            }

            for (int i = 0; i < players.Count; i++)
            {
                var receivingCards = passedCards[i];
                var playerTo = this.GetPassRecipient(round.RoundNumber, players.Count, players[i]);
                result[playerTo] = result[playerTo].Union(receivingCards);
            }

            return result
                .Select(x => new CardHand(x.Key, x.Value))
                .ToArray();
        }
    }
}
