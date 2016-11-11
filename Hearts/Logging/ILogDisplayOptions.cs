using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Logging
{
    public interface ILogDisplayOptions
    {
        int NamePad { get; }
        bool DisplayStartingHands { get; }
        bool DisplayHandsAfterPass { get; }
        bool DisplayPass { get; }
        bool DisplayTrickSummary { get; }
        bool DisplayExceptions { get; }
        bool DisplayPointsForRound { get; }
        bool DisplayLogFinalWinner { get; }
    }
}
