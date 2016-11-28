using Hearts.Model;

namespace Hearts.AI
{
    public interface IPlayStrategy
    {
        Card ChooseCardToPlay(GameState gameState);
    }
}