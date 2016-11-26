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
            // TODO: Check this still works since I made it return the result
            var hands = new Dictionary<Player, IEnumerable<Card>>();

            foreach (var player in players)
            {
                hands.Add(player, new List<Card>());
            }

            int playerCount = players.Count();
            int cardsPerPlayer = (int)Math.Floor(deck.Cards.Count() / (double)playerCount);

            for (int i = 0; i < cardsPerPlayer; i++)
            {
                foreach (var player in players)
                {
                    hands[player] = hands[player].Union(new List<Card> { deck.DealNextCard() });
                }
            }

            return hands
                .Select(x => new CardHand(x.Key, x.Value))
                .ToArray();
        }
    }
}
