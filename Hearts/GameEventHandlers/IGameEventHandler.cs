using Hearts.Model;

namespace Hearts.GameEventHandlers
{
    public interface IGameEventHandler
    {
        GameEventType GameEventType { get; }
    }
}
