using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.AI.Strategies
{
    public interface IPlayStrategy
    {
        Card ChooseCardToPlay(GameState gameState, IEnumerable<Card> availableCards, IEnumerable<Card> legalCards);
    }
}
