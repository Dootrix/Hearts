using Hearts.Factories;
using Hearts.Model;
using System.Collections.Generic;

namespace Hearts.AI
{
    public interface IAgent
    {
        // An fixed (optionally arbitrary) name for your AI, that allows other AIs to adjust to it
        string AgentName { get; }

        IEnumerable<Card> ChooseCardsToPass(IEnumerable<Card> startingCards, Pass pass);

        Card ChooseCardToPlay(Round gameState, IEnumerable<Card> startingCards, IEnumerable<Card> availableCards, IEnumerable<Card> legalCards);
    }
}