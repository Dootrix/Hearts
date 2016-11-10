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
        private static Func<Player, Player> oneToLeft = (i) => { return i.NextPlayer; };
        private static Func<Player, Player> oneToRight = (i) => { return i.PreviousPlayer; };
        private static Func<Player, Player> twoToLeft = (i) => { return i.NextPlayer.NextPlayer; };
        private static Func<Player, Player> twoToRight = (i) => { return i.PreviousPlayer.PreviousPlayer; };
        private static Func<Player, Player> noPass = (i) => { return i; };
        
        private List<Func<Player, Player>> threePlayer = new List<Func<Player, Player>>
        {
            oneToLeft, oneToRight, noPass
        };

        private List<Func<Player, Player>> fourPlayer = new List<Func<Player, Player>>
        {
            oneToLeft, oneToRight, twoToLeft, noPass
        };

        private List<Func<Player, Player>> fivePlayer = new List<Func<Player, Player>>
        {
            oneToLeft, oneToRight, twoToLeft, twoToRight, noPass
        };

        public void HandlePassing(int roundNumber, List<Player> players, Dictionary<Player, List<Card>> startingHands, Player playerFrom)
        {
            var passedCards = new List<List<Card>>();

            for (int i = 0; i < players.Count; i++)
            {
                var pass = playerFrom.Agent.ChooseCardsToPass(startingHands[playerFrom]);

                // TODO: Validate passed cards

                passedCards.Add(pass);
                playerFrom.Pass(pass);
                playerFrom = this.GetPassRecipient(roundNumber, players.Count, playerFrom);
            }

            for (int i = 0; i < players.Count; i++)
            {
                var receivingCards = passedCards[i + 1 == players.Count ? 0 : i + 1];
                players[i].Receive(receivingCards);
            }
        }

        private Player GetPassRecipient(int roundNumber, int playerCount, Player fromPlayer)
        {
            int passIndex = roundNumber % playerCount;

            switch (playerCount)
            {
                case 3:
                    return this.threePlayer[passIndex](fromPlayer);
                case 4:
                    return this.fourPlayer[passIndex](fromPlayer);
                case 5:
                    return this.fivePlayer[passIndex](fromPlayer);
            }

            return fromPlayer;
        }
    }
}
