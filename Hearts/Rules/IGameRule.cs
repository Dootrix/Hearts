using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Rules
{
    public interface IGameRule
    {
        IEnumerable<Card> FilterCards(IEnumerable<Card> cards, GameState gameState);

        bool Applies(GameState gameState);
    }
}
