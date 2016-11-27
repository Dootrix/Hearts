using Hearts.Model;

namespace Hearts.GameEventHandlers
{
    public class CardPlayedByPlayerHandler : IGameEventHandler
    {
        public GameEventType GameEventType { get { return GameEventType.CardPlayedByPlayer; } }
    }
}
