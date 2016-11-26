namespace Hearts.Logging
{
    public interface ILogDisplayOptions
    {
        int NamePad { get; }
        bool DisplayRandomSeed { get; }
        bool DisplayStartingHands { get; }
        bool DisplayHandsAfterPass { get; }
        bool DisplayPass { get; }
        bool DisplayTrickSummary { get; }
        bool DisplayExceptions { get; }
        bool DisplayPointsForRound { get; }
        bool DisplayLogFinalWinner { get; }
        bool DisplaySimulationSummary { get; }
        bool DisplayAgentMoveNotes { get; }
        bool DisplayAgentSummaryNotes { get; }
        bool DisplayTotalSimulationTime { get; }
    }
}
