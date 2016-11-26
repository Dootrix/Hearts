namespace Hearts.Logging
{
    public class SummaryOnlyLogOptions : ILogDisplayOptions
    {
        public int NamePad { get { return 12; } }
        public bool DisplayRandomSeed { get { return true; } }
        public bool DisplayStartingHands { get { return false; } }
        public bool DisplayHandsAfterPass { get { return false; } }
        public bool DisplayPass { get { return false; } }
        public bool DisplayTrickSummary { get { return false; } }
        public bool DisplayExceptions { get { return true; } }
        public bool DisplayPointsForRound { get { return false; } }
        public bool DisplayLogFinalWinner { get { return false; } }
        public bool DisplaySimulationSummary { get { return true; } }
        public bool DisplayAgentMoveNotes { get { return false; } }
        public bool DisplayAgentSummaryNotes { get { return true; } }
        public bool DisplayTotalSimulationTime { get { return true; } }
    }
}
