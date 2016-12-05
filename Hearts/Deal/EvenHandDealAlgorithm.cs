using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Deal
{
    /// <summary>
    /// Deals cards, alternating player each time. If there are insufficient cards to deal another card to each play, the deal is stopped there.
    /// </summary>
    internal class EvenHandDealAlgorithm : IDealAlgorithm
    {
        public IEnumerable<CardHand> DealStartingHands(Deck deck, IEnumerable<Player> players)
        {
            var hands = players
                .Select(x => new CardHand(x, new Card[] { }))
                .ToArray();

            int playerCount = players.Count();
            int cardsPerPlayer = (int)Math.Floor(deck.Cards.Count() / (double)playerCount);

            for (int i = 0; i < cardsPerPlayer; i++)
            {
                foreach (var hand in hands)
                {
                    hand.Add(deck.DealNextCard());
                }
            }

            return hands;                
        }
    }
}
