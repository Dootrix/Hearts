using Hearts.Factories;
using Hearts.Model;
using System.Collections.Generic;

namespace Hearts.AI
{
    public interface IAgent
    {
        // An fixed (optionally arbitrary) name for your AI, that allows other AIs to adjust to it
        string AgentName { get; }

        // The owning player
        Player Player { get; set; }

        IEnumerable<Card> ChooseCardsToPass(Round round, PlayerCards cards); // TODO: Add player I'm passing to, Add player I'm receiving from, Add Game history, e.g. scores

        Card ChooseCardToPlay(Round round, PlayerCards cards); // TODO: Add player I passed to, Add player I received from, Add Game history, e.g. scores


        // IEnumerable<Card> ChooseCardsToPass(GameState gameState);
        // Card ChooseCardToPlay(GameState gameState);
    }
}