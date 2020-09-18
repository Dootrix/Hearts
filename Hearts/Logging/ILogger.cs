using System.Collections.Generic;
using Hearts.Model;
using Hearts.Scoring;

namespace Hearts.Logging
{
    public interface ILogger
    {
        void BeginLogging();
        void Card(Card card);
        void HandsAfterPass(IEnumerable<CardHand> hands);
        void IllegalPass(Player player, IEnumerable<Card> cards);
        void IllegalPlay(Player player, Card card, IEnumerable<Card> legal);
        void LogAgentMoveNote(string note);
        void LogAgentSummaryNote(string note);
        void LogFinalWinner(GameResult gameResult);
        void LogRandomSeed(int randomSeed);
        void LogSimulationSummary(SimulationResult result);
        void OutOfCardsException();
        void Pass(Player player, IEnumerable<Card> cards);
        void PassDirection(Pass pass);
        void PointsForRound(RoundResult roundResult);
        void StartingHands(IEnumerable<CardHand> hands);
        void StopLogging();
        void TotalSimulationTime(long elapsedMilliseconds);
        void TrickSummary(PlayedTrick trick);
    }
}