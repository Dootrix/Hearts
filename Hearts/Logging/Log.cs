using Hearts.Attributes;
using Hearts.Extensions;
using Hearts.Model;
using Hearts.Scoring;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Logging
{
    public static class Log
    {
        public static ILogDisplayOptions Options = new DefaultLogOptions();
        public static ILogger Logger = new ConsoleOutputLogger();

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

        public static void IllegalPlay(Player player, Card card)
        {
            Logger.IllegalPlay(player, card);
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
