using Hearts.Model;

namespace Hearts.GameEventHandlers
{
    public class PlayerAddedToGameHandler : IGameEventHandler
    {
        public GameEventType GameEventType { get { return GameEventType.PlayerAddedToGame; } }
    }
}
