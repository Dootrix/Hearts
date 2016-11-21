using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Extensions;

namespace Hearts.Model
{
    public enum GameEventType
    {
        CardAddedToDeck,
        PlayerAddedToGame,
        CardDealtToPlayer,
        CardPlayedByPlayer,
        HandFinished,
        HandsChanged,
        ShowHand
    }
}
