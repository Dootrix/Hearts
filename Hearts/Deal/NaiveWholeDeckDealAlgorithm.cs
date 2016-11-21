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
        public Dictionary<Player, IEnumerable<Card>> DealStartingHands(Deck deck, IEnumerable<Player> players)
        {
            // TODO: Check this still works since I made it return the result
            var result = new Dictionary<Player, IEnumerable<Card>>();

            foreach (var player in players)
            {
                result.Add(player, new List<Card>());
            }

            var receivingPlayers = players.ToList();
            int playerIndex = 0;
            int playerCount = players.Count();

            foreach (var card in deck.Cards.ToList())
            {
                result[receivingPlayers[playerIndex]] = result[receivingPlayers[playerIndex]].Union(new List<Card>{deck.Deal(card)});
                
                if (++playerIndex > playerCount - 1)
                {
                    playerIndex -= playerCount;
                }
            }

            return result;
        }
    }
}
