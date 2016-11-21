using Hearts.Model;
using System.Collections.Generic;

namespace Hearts.Deal
{
    public interface IDealAlgorithm
    {
        /// <summary>
        /// Deals a starting hand to each player
        /// </summary>
        /// <param name="deck">The pre-shuffled deck to remove cards from</param>
        /// <param name="players">The players to give cards to</param>
        Dictionary<Player, IEnumerable<Card>> DealStartingHands(Deck deck, IEnumerable<Player> players);
    }
}