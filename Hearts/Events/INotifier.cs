using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
