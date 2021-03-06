﻿namespace Hearts.Logging
{
    public class DefaultLogOptions : ILogDisplayOptions
    {
        public int NamePad { get { return 12; } }
        public bool DisplayRandomSeed { get { return true; } }
        public bool DisplayStartingHands { get { return true; } }
        public bool DisplayHandsAfterPass { get { return true; } }
        public bool DisplayPass { get { return true; } }
        public bool DisplayTrickSummary { get { return true; } }
        public bool DisplayExceptions { get { return true; } }
        public bool DisplayPointsForRound { get { return true; } }
        public bool DisplayLogFinalWinner { get { return true; } }
        public bool DisplaySimulationSummary { get { return true; } }
        public bool DisplayAgentMoveNotes { get { return true; } }
        public bool DisplayAgentSummaryNotes { get { return true; } }
        public bool DisplayTotalSimulationTime { get { return true; } }
    }
}
