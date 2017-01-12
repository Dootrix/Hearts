using System;
using System.Collections.Generic;
using Hearts.Model;

namespace Hearts.Events
{
    public interface INotifier
    {
        event EventNotifier.SimulationStartedEventHandler SimulationStarted;
        event EventNotifier.SimulationEndedEventHandler SimulationEnded;
        event EventHandler<EventArg<IEnumerable<Bot>>> SimulationStartedForSeatingArrangement;
        event EventHandler<EventArg<IEnumerable<Bot>>> SimulationEndedForSeatingArrangement;
        event EventNotifier.GameStartedEventHandler GameStarted;
        event EventNotifier.GameEndedEventHandler GameEnded;
        event EventHandler RoundStarted;
        event EventHandler RoundEnded;
        event EventHandler NoPass;
    }
}
