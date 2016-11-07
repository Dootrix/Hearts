using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Deal
{
    /// <summary>
    /// Deals every card, alternating player each time. Does not guarantee even distribution of cards if deck is not evenly distributable between players.
    /// </summary>
    public class NaiveWholeDeckDealAlgorithm : IDealAlgorithm
    {
        public void DealStartingHands(Deck deck, IEnumerable<Player> players)
        {
            var receivingPlayers = players.ToList();
            int playerIndex = 0;
            int playerCount = players.Count();

            foreach (var card in deck.Cards.ToList())
            {
                receivingPlayers[playerIndex].Receive(deck.Deal(card));
                
                if (++playerIndex > playerCount - 1)
                {
                    playerIndex -= playerCount;
                }
            }
        }
    }
}
