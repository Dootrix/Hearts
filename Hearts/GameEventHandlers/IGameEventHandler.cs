using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;
using Hearts.Extensions;

namespace Hearts.GameEventHandlers
{
    public interface IGameEventHandler
    {
        GameEventType GameEventType { get; }
    }
}
