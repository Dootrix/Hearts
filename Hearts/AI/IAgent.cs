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

        IEnumerable<Card> ChooseCardsToPass(GameState gameState);

        Card ChooseCardToPlay(GameState gameState);
    }
}
