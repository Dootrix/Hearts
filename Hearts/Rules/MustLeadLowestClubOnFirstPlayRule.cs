using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Rules
{
    public class MustLeadLowestClubOnFirstPlayRule : IGameRule
    {
        public IEnumerable<Card> FilterCards(IEnumerable<Card> cards, Game gameState)
        {
            // TODO - generalise incase there are not 4 players.
            // changed this as I think it needs to return a card only if the player has the lowest card in the deck 
            // (after pruning cards based on the number of players)
            return cards
                .Where(i => i == new Card(Kind.Two, Suit.Clubs))
                .ToArray();
        }

        public bool Applies(Game gameState)
        {
            return gameState.IsFirstLeadHand;
        }
    }
}
