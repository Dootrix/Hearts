using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Deal
{
    /// <summary>
    /// Deals cards, alternating player each time. If there are insufficient cards to deal another card to each play, the deal is stopped there.
    /// </summary>
    public class EvenHandDealAlgorithm : IDealAlgorithm
    {
        public void DealStartingHands(Deck deck, IEnumerable<Player> players)
        {
            int playerCount = players.Count();
            int cardsPerPlayer = (int)Math.Floor(deck.Cards.Count() / (double)playerCount);

            for (int i = 0; i < cardsPerPlayer; i++)
            {
                foreach (var player in players)
                {
                    player.Receive(deck.DealNextCard());
                }
            }
        }
    }
}
