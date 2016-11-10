using Hearts.Factories;
using Hearts.Model;
using System.Collections.Generic;

namespace Hearts.AI
{
    public interface IAgent
    {
        // An fixed (optionally arbitrary) name for your AI, that allows other AIs to adjust to it
        string AgentName { get; }
        List<Card> ChooseCardsToPass(List<Card> startingCards);
        Card ChooseCardToPlay(Game gameState, List<Card> startingCards, List<Card> availableCards, List<Card> legalCards);
    }
}