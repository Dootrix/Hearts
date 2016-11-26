using Hearts.Model;

namespace Hearts.GameEventHandlers
{
    public class HandFinishedHandler : IGameEventHandler
    {
        public GameEventType GameEventType { get { return GameEventType.HandFinished; } }
    }
}
