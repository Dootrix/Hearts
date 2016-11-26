using Hearts.Model;

namespace Hearts.GameEventHandlers
{
    public class ShowHandHandler : IGameEventHandler
    {
        public GameEventType GameEventType { get { return GameEventType.ShowHand; } }
    }
}
