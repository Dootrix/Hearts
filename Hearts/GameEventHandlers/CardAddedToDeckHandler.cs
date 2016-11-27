using Hearts.Model;

namespace Hearts.GameEventHandlers
{
    public class CardAddedToDeckHandler : IGameEventHandler
    {
        public GameEventType GameEventType { get { return GameEventType.CardAddedToDeck; } }
    }
}
