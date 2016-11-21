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
        public Dictionary<Player, IEnumerable<Card>> DealStartingHands(Deck deck, IEnumerable<Player> players)
        {
            // TODO: Check this still works since I made it return the result
            var result = new Dictionary<Player, IEnumerable<Card>>();

            foreach (var player in players)
            {
                result.Add(player, new List<Card>());
            }

            int playerCount = players.Count();
            int cardsPerPlayer = (int)Math.Floor(deck.Cards.Count() / (double)playerCount);

            for (int i = 0; i < cardsPerPlayer; i++)
            {
                foreach (var player in players)
                {
                    result[player] = result[player].Union(new List<Card> { deck.DealNextCard() });
                }
            }

            return result;
        }
    }
}
