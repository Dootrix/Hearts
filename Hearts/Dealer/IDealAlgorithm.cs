using System.Collections.Generic;

namespace Hearts
{
    public interface IDealAlgorithm
    {
        /// <summary>
        /// Deals a starting hand to each player
        /// </summary>
        /// <param name="deck">The pre-shuffled deck to remove cards from</param>
        /// <param name="players">The players to give cards to</param>
        void DealStartingHands(Deck deck, IEnumerable<Player> players);
    }
}