using Hearts.Model;

namespace Hearts.GameEventHandlers
{
    public class HandsChangedHandler : IGameEventHandler
    {
        public GameEventType GameEventType { get { return GameEventType.HandsChanged; } }
    }
}
