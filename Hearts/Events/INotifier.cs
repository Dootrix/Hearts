using System;

namespace Hearts.Events
{
    public interface INotifier
    {
        event EventHandler SimulationStarted;
        event EventHandler SimulationEnded;
        event EventHandler GameStarted;
        event EventHandler GameEnded;
        event EventHandler RoundStarted;
        event EventHandler RoundEnded;

        event EventHandler NoPass;
    }
}
