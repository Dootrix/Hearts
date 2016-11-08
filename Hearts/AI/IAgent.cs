using Hearts.Factories;
using Hearts.Model;
using System.Collections.Generic;

namespace Hearts.AI
{
    public interface IAgent
    {
        List<Card> ChooseCardsToPass(List<Card> startingCards);
        Card ChooseCardToPlay(Game gameState, List<Card> availableCards);
    }
}