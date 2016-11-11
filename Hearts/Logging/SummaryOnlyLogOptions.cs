using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Extensions;
using Hearts.Model;
using Hearts.Scoring;

namespace Hearts.Logging
{
    public class SummaryOnlyLogOptions : ILogDisplayOptions
    {
        public int NamePad { get { return 12; } }
        public bool DisplayStartingHands { get { return false; } }
        public bool DisplayHandsAfterPass { get { return false; } }
        public bool DisplayPass { get { return false; } }
        public bool DisplayTrickSummary { get { return false; } }
        public bool DisplayExceptions { get { return true; } }
        public bool DisplayPointsForRound { get { return false; } }
        public bool DisplayLogFinalWinner { get { return true; } }
    }
}
