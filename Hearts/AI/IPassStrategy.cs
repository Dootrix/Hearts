using System.Collections.Generic;
using Hearts.Model;

namespace Hearts.AI
{
    public interface IPassStrategy
    {
        IEnumerable<Card> ChooseCardsToPass(GameState gameState);
    }
}