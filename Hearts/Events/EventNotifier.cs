using Hearts.Events;
using Hearts.Model;
using System;
using System.Collections.Generic;

namespace Hearts.Events
{
    public class EventNotifier : INotifier
    {
        public event EventHandler SimulationStarted;
        public event EventHandler SimulationEnded;
        public event EventHandler<EventArg<IEnumerable<Bot>>> SimulationStartedForSeatingArrangement;
        public event EventHandler<EventArg<IEnumerable<Bot>>> SimulationEndedForSeatingArrangement;
        public event EventHandler GameStarted;
        public event EventHandler GameEnded;
        public event EventHandler RoundStarted;
        public event EventHandler RoundEnded;
        public event EventHandler NoPass;

        public void CallSimulationStarted()
        {
            if (SimulationStarted != null)
                this.SimulationStarted(this, new EventArgs());
        }

        public void CallSimulationEnded()
        {
            if (SimulationEnded != null)
                this.SimulationEnded(this, new EventArgs());
        }

        public void CallSimulationStartedForSeatingArrangement(IEnumerable<Bot> bots)
        {
            if(SimulationStartedForSeatingArrangement != null)
            {
                var args = new EventArg<IEnumerable<Bot>>(bots);
                this.SimulationStartedForSeatingArrangement(this, args);
            }
        }

        public void CallSimulationEndedForSeatingArrangement(IEnumerable<Bot> bots)
        {
            if (SimulationEndedForSeatingArrangement != null)
            {
                var args = new EventArg<IEnumerable<Bot>>(bots);
                this.SimulationEndedForSeatingArrangement(this, args);
            }
        }

        public void CallGameStarted()
        {
            if (GameStarted != null)
                this.GameStarted(this, new EventArgs());
        }

        public void CallGameEnded()
        {
            if (GameEnded != null)
                this.GameEnded(this, new EventArgs());
        }

        public void CallRoundStarted()
        {
            if (RoundStarted != null)
                this.RoundStarted(this, new EventArgs());
        }

        public void CallRoundEnded()
        {
            if (RoundEnded != null)
                this.RoundEnded(this, new EventArgs());
        }

        public void CallNoPass()
        {
            if (NoPass != null)
                this.NoPass(this, new EventArgs());
        }
    }
}
