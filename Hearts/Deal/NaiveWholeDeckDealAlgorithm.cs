using Hearts.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Deal
{
    /// <summary>
    /// Deals every card, alternating player each time. Does not guarantee even distribution of cards if deck is not evenly distributable between players.
    /// </summary>
    internal class NaiveWholeDeckDealAlgorithm : IDealAlgorithm
    {
        public IEnumerable<CardHand> DealStartingHands(Deck deck, IEnumerable<Player> players)
        {
            var hands = new Dictionary<Player, IEnumerable<Card>>();

            foreach (var player in players)
            {
                hands.Add(player, new List<Card>());
            }

            var receivingPlayers = players.ToList();
            int playerIndex = 0;
            int playerCount = players.Count();

            foreach (var card in deck.Cards.ToList())
            {
                hands[receivingPlayers[playerIndex]] = hands[receivingPlayers[playerIndex]].Union(new List<Card>{deck.Deal(card)});
                
                if (++playerIndex > playerCount - 1)
                {
                    playerIndex -= playerCount;
                }
            }

            return hands
                .Select(x => new CardHand(x.Key, x.Value))
                .ToArray();
        }
    }
}
