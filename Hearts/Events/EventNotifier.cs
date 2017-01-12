using Hearts.Events;
using Hearts.Model;
using Hearts.Scoring;
using System;
using System.Collections.Generic;

namespace Hearts.Events
{
    public class EventNotifier : INotifier
    {
        public delegate void SimulationStartedEventHandler(SimulationStartedEventArgs e);
        public delegate void SimulationEndedEventHandler(SimulationEndedEventArgs e);
        public delegate void GameStartedEventHandler(GameStartedEventArgs e);
        public delegate void GameEndedEventHandler(GameEndedEventArgs e);

        public event SimulationStartedEventHandler SimulationStarted;
        public event SimulationEndedEventHandler SimulationEnded;
        public event EventHandler<EventArg<IEnumerable<Bot>>> SimulationStartedForSeatingArrangement;
        public event EventHandler<EventArg<IEnumerable<Bot>>> SimulationEndedForSeatingArrangement;
        public event GameStartedEventHandler GameStarted;
        public event GameEndedEventHandler GameEnded;
        public event EventHandler RoundStarted;
        public event EventHandler RoundEnded;
        public event EventHandler NoPass;

        public void CallSimulationStarted(int randomSeed)
        {
            if (this.SimulationStarted != null)
            {
                this.SimulationStarted(new SimulationStartedEventArgs(randomSeed));
            }
        }

        public void CallSimulationEnded(SimulationResult result)
        {
            if (this.SimulationEnded != null)
            {
                this.SimulationEnded(new SimulationEndedEventArgs(result));
            }
        }

        public void CallSimulationStartedForSeatingArrangement(IEnumerable<Bot> bots)
        {
            if (this.SimulationStartedForSeatingArrangement != null)
            {
                var args = new EventArg<IEnumerable<Bot>>(bots);
                this.SimulationStartedForSeatingArrangement(this, args);
            }
        }

        public void CallSimulationEndedForSeatingArrangement(IEnumerable<Bot> bots)
        {
            if (this.SimulationEndedForSeatingArrangement != null)
            {
                var args = new EventArg<IEnumerable<Bot>>(bots);
                this.SimulationEndedForSeatingArrangement(this, args);
            }
        }

        public void CallGameStarted(int randomSeed)
        {
            if (this.GameStarted != null)
            {
                this.GameStarted(new GameStartedEventArgs(randomSeed));
            }
        }

        public void CallGameEnded(GameResult result)
        {
            if (this.GameEnded != null)
            {
                this.GameEnded(new GameEndedEventArgs(result));
            }
        }

        public void CallRoundStarted()
        {
            if (this.RoundStarted != null)
            {
                this.RoundStarted(this, new EventArgs());
            }
        }

        public void CallRoundEnded()
        {
            if (this.RoundEnded != null)
            {
                this.RoundEnded(this, new EventArgs());
            }
        }

        public void CallNoPass()
        {
            if (this.NoPass != null)
            {
                this.NoPass(this, new EventArgs());
            }
        }
    }
}
