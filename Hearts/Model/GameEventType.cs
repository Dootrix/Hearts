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
