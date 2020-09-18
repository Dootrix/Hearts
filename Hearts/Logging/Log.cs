using Hearts.Logging.Enums;
using Hearts.Model;
using Hearts.Scoring;
using System;
using System.Collections.Generic;

namespace Hearts.Logging
{
    public static class Log
    {
        public static ILogger Logger;

        public static void BeginLogging(LoggingLevel loggingLevel, LoggingOutput loggingOutput)
        {
            var options = loggingLevel == LoggingLevel.FullOutput
                ? new DefaultLogOptions() as ILogDisplayOptions
                : new SummaryOnlyLogOptions();

            var logger = loggingOutput == LoggingOutput.HtmlExport
                ? new HtmlExportLogger(options) as ILogger
                : new ConsoleOutputLogger(options);

            Log.Logger = logger;
            Logger.BeginLogging();
            Logger.LogAgentMoveNote($"Started on: {DateTime.Now}");
        }

        public static void StopLogging()
        {
            Logger.StopLogging();
        }

        internal static void StartingHands(IEnumerable<CardHand> hands)
        {
            Logger.StartingHands(hands);
        }

        public static void TotalSimulationTime(long elapsedMilliseconds)
        {
            Logger.TotalSimulationTime(elapsedMilliseconds);
        }

        public static void PassDirection(Pass pass)
        {
            Logger.PassDirection(pass);
        }

        public static void HandsAfterPass(IEnumerable<CardHand> hands)
        {
            Logger.HandsAfterPass(hands);
        }

        public static void Pass(Player player, IEnumerable<Card> cards)
        {
            Logger.Pass(player, cards);
        }

        public static void TrickSummary(PlayedTrick trick)
        {
            Logger.TrickSummary(trick);
        }

        public static void IllegalPass(Player player, IEnumerable<Card> cards)
        {
            Logger.IllegalPass(player, cards);
        }

        public static void IllegalPlay(Player player, Card card, IEnumerable<Card> legal)
        {
            Logger.IllegalPlay(player, card, legal);
        }

        public static void OutOfCardsException()
        {
            Logger.OutOfCardsException();
        }

        public static void PointsForRound(RoundResult roundResult)
        {
            Logger.PointsForRound(roundResult);
        }

        public static void LogFinalWinner(GameResult gameResult)
        {
            Logger.LogFinalWinner(gameResult);
        }
        
        public static void LogSimulationSummary(SimulationResult result)
        {
            Logger.LogSimulationSummary(result);
        }

        public static void LogAgentMoveNote(string note)
        {
            Logger.LogAgentMoveNote(note);
        }
        public static void LogAgentSummaryNote(string note)
        {
            Logger.LogAgentSummaryNote(note);
        }

        public static void LogRandomSeed(int randomSeed)
        {
            Logger.LogRandomSeed(randomSeed);
        }

        public static void Card(Card card)
        {
            Logger.Card(card);
        }
    }
}
