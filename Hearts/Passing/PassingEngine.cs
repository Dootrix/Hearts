using Hearts.Logging;
using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Passing
{
    public class PassService
    {
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

        public Player GetPassRecipient(int roundNumber, int playerCount, Player fromPlayer)
        {
            var passFunction = this.PassFunctions[this.GetPass(roundNumber, playerCount)];

            return passFunction(fromPlayer);
        }

        private Pass GetPass(int roundNumber, int playerCount)
        {
            return this.PassSchedule[playerCount - 1][roundNumber % playerCount];
        }

        public void OrchestratePassing(int roundNumber, List<Player> players, Dictionary<Player, List<Card>> startingHands, Player playerFrom)
        {
            var passedCards = new List<List<Card>>();
            
            for (int i = 0; i < players.Count; i++)
            {
                var pass = playerFrom.Agent.ChooseCardsToPass(startingHands[playerFrom], this.GetPass(roundNumber, players.Count));

                if (!pass.All(j => startingHands[playerFrom].Contains(j)) || pass.Distinct().Count() != 3)
                {
                    // TODO: Handle illegal move
                    Log.IllegalPass(playerFrom, pass);
                    playerFrom.AgentHasMadeIllegalMove = true;
                }

                Log.Pass(playerFrom, pass);

                passedCards.Add(pass);
                playerFrom.Pass(pass);

                if (i < players.Count - 1)
                {
                    playerFrom = players[i + 1];
                }
            }

            for (int i = 0; i < players.Count; i++)
            {
                var receivingCards = passedCards[i];
                var playerTo = this.GetPassRecipient(roundNumber, players.Count, players[i]);
                playerTo.Receive(receivingCards);
            }
        }
    }
}
