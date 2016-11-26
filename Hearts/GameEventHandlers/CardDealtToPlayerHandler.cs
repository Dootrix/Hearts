using Hearts.Model;

namespace Hearts.GameEventHandlers
{
    public class CardDealtToPlayerHandler : IGameEventHandler
    {
        public GameEventType GameEventType { get { return GameEventType.CardDealtToPlayer; } }
    }
}
