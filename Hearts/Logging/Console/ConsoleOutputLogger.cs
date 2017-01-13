using Hearts.Attributes;
using Hearts.Extensions;
using Hearts.Model;
using Hearts.Scoring;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Logging
{
    public class ConsoleOutputLogger : ILogger
    {
        public ILogDisplayOptions Options = new DefaultLogOptions();
        
        public void BeginLogging()
        {
        }

        public void StopLogging()
        {
            System.Console.ReadLine();
        }

        public void StartingHands(IEnumerable<CardHand> hands)
        {
            if (!Options.DisplayStartingHands) return;

            ToGrey();
            Console.WriteLine("Starting Hands:");

            foreach (var hand in hands)
            {
                Hand(hand, hand.Owner.Name);
            }

            NewLine();
        }

        public void TotalSimulationTime(long elapsedMilliseconds)
        {
            Console.WriteLine("Total simulation time: {0}s.", elapsedMilliseconds / 1000.0);
        }

        public void PassDirection(Pass pass)
        {
            if (!Options.DisplayPass) return;

            ToGrey();
            Console.WriteLine("Pass direction: " + Abbreviation.Get(pass));
        }

        public void HandsAfterPass(IEnumerable<CardHand> hands)
        {
            if (!Options.DisplayHandsAfterPass) return;

            NewLine();
            ToGrey();
            Console.WriteLine("Post-Pass Hands:");

            foreach (var hand in hands)
            {
                this.Hand(hand, hand.Owner.Name);
            }

            NewLine();
        }

        public void Pass(Player player, IEnumerable<Card> cards)
        {
            if (!Options.DisplayPass) return;

            ToBlue();
            Console.Write(" " + player.Name.PadLeft(Options.NamePad) + " ");
            ToGrey();
            Console.Write(" pass ");

            foreach(var card in cards)
            {
                Card(card);
                Console.Write(" ");
            }

            NewLine();
        }

        public void TrickSummary(PlayedTrick trick)
        {
            if (!Options.DisplayTrickSummary) return;

            foreach (var playedCard in trick.Cards)
            {
                ToBlue();
                Console.Write(" " + playedCard.Key.Name.PadLeft(Options.NamePad) + " ");
                ToGrey();
                Console.Write(" play ");
                Card(playedCard.Value);

                if (trick.Winner == playedCard.Key)
                {
                    ToBlue();
                    Console.Write(" Win");
                    int trickScore = trick.Cards.Select(i => i.Value).Score();

                    if (trickScore > 0)
                    {
                        Console.Write(" {0}pts", trickScore); 
                    }
                }

                NewLine();
            }

            NewLine();
        }

        public void IllegalPass(Player player, IEnumerable<Card> cards)
        {
            if (!Options.DisplayExceptions) return;

            ToBlue();
            Console.WriteLine(player.Name + " made an ILLEGAL PASS! (" + string.Join(", ", cards.Select(i => i.ToString())) + ") - " + player.DebuggerDisplay);
        }

        public void IllegalPlay(Player player, Card card)
        {
            if (!Options.DisplayExceptions) return;

            ToBlue();
            Console.WriteLine(player.Name + " played an ILLEGAL CARD! (" + card + ") - " + player.DebuggerDisplay);
        }

        public void OutOfCardsException()
        {
            if (!Options.DisplayExceptions) return;

            ToBlue();
            Console.WriteLine("*** OUT OF CARDS! ***");
        }

        public void PointsForRound(RoundResult roundResult)
        {
            if (!Options.DisplayPointsForRound) return;

            var scores = roundResult.Scores;

            foreach (var score in scores)
            {
                ToBlue();
                Console.Write(" " + score.Key.Name.PadLeft(Options.NamePad) + " ");
                ToGrey();
                Console.Write(" : ");
                ToBlue();
                Console.Write(score.Value.ToString().PadLeft(3));
                ToGrey();
                Console.Write(" pts");
                NewLine();
            }

            NewLine();
        }

        public void LogFinalWinner(GameResult gameResult)
        {
            if (!Options.DisplayLogFinalWinner) return;

            ToGrey();
            Console.WriteLine(string.Empty);
            Console.WriteLine("__________________________________________________________________");
            Console.WriteLine(string.Empty);

            Console.WriteLine("Moonshots:");

            foreach (var player in gameResult.Moonshots)
            {
                Console.WriteLine("{0} : {1}", player.Key.Name, player.Value);
            }

            Console.WriteLine("Rounds: W/L/P");

            foreach (var player in gameResult.RoundWins)
            {
                Console.WriteLine("{0} : {1}/{2}/{3}", player.Key.Name, player.Value, gameResult.RoundLosses[player.Key], gameResult.RoundsPlayed);
            }

            Console.WriteLine("Scores");

            foreach (var score in gameResult.Scores)
            {
                ToBlue();
                Console.Write(" " + score.Key.Name.PadLeft(Options.NamePad) + " ");
                ToGrey();
                Console.Write(" : ");
                ToBlue();
                Console.Write(score.Value.ToString().PadLeft(3));
                ToGrey();
                Console.Write(" pts ");

                if (score.Value == gameResult.Scores.Min(i => i.Value))
                {
                    ToBlue();
                    Console.Write("Wins");
                }

                NewLine();
            }

            ToGrey();
            Console.WriteLine(string.Empty);
            Console.WriteLine("__________________________________________________________________");
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
        }
        
        public void LogSimulationSummary(SimulationResult result)
        {
            if (!Options.DisplaySimulationSummary || !result.GameResults.Any()) return;

            var players = result.GameResults.First().Scores.Select(i => i.Key);

            ToGrey();
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
            Console.WriteLine("__________________________________________________________________");
            Console.WriteLine(string.Empty);
            Console.WriteLine("Simulation Stats");
            Console.WriteLine("Based on {0} game{1}", result.GameResults.Count, result.GameResults.Count > 1 ? "s" : string.Empty);

            Console.WriteLine(string.Empty);
            Console.WriteLine("Execution Times: Pass/Play");

            foreach (var player in players)
            {
                int moonshots = result.GameResults.SelectMany(i => i.Moonshots).Where(i => i.Key == player).Select(i => i.Value).Sum();
                var averagePassTiming = result.TimerService.GetAveragePassTiming(player);
                var averagePlayTiming = result.TimerService.GetAveragePlayTiming(player);
                Console.WriteLine("{0} : {1:0}ms / {2:0}ms", player.Name, averagePassTiming, averagePlayTiming);
            }
            Console.WriteLine(string.Empty);
            Console.WriteLine("Moonshots:");

            foreach (var player in players)
            {
                int moonshots = result.GameResults.SelectMany(i => i.Moonshots).Where(i => i.Key == player).Select(i => i.Value).Sum();
                var botPlayer = result.MoonshotAttempts.SingleOrDefault(i => i.Bot.Player == player);
                int attempts = botPlayer != null ? botPlayer.MoonshotAttempts : 0;
                Console.WriteLine("{0} : {1} / {2}", player.Name, moonshots, attempts);
            }

            Console.WriteLine(string.Empty);
            Console.WriteLine("Rounds: W/L/P");

            foreach (var player in players)
            {
                var playerName = player.Name;
                var roundsWon = result.GameResults.SelectMany(i => i.RoundWins).Where(i => i.Key == player).Select(i => i.Value).Sum();
                var roundsLost = result.GameResults.SelectMany(i => i.RoundLosses).Where(i => i.Key == player).Select(i => i.Value).Sum();
                int roundsPlayed = result.GameResults.Select(i => i.RoundsPlayed).Sum();
                Console.WriteLine("{0} : {1}/{2}/{3}", playerName, roundsWon, roundsLost, roundsPlayed);
            }

            Console.WriteLine(string.Empty);
            Console.WriteLine("Games: W/L/P");

            var totalWon = new Dictionary<Player, int>();
            int gamesPlayed = result.GameResults.Count();

            foreach (var player in players)
            {
                var playerName = player.Name;
                int gamesWon = result.GameResults.SelectMany(i => i.Winners).Where(i => i == player).Count();
                int gameLost = result.GameResults.SelectMany(i => i.Losers).Where(i => i == player).Count();
                Console.WriteLine("{0} : {1}/{2}/{3}", playerName, gamesWon, gameLost, gamesPlayed);
                totalWon.Add(player, gamesWon);
            }

            Console.WriteLine(string.Empty);
            Console.WriteLine("__________________________________________________________________");
            Console.WriteLine(string.Empty);

            foreach (var player in players)
            {
                ToBlue();
                Console.Write(" " + player.Name.PadLeft(Options.NamePad) + " ");
                ToGrey();
                Console.Write(" : ");
                ToBlue();
                float percent = (float)totalWon[player] / gamesPlayed;
                Console.Write(percent.ToString("0.00% ").PadLeft(8));
                ToGrey();

                if (totalWon[player] == totalWon.Max(i => i.Value))
                {
                    ToBlue();
                    Console.Write(" Best");
                }

                NewLine();
            }

            ToGrey();
            Console.WriteLine(string.Empty);
            Console.WriteLine("__________________________________________________________________");
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
        }

        public void LogAgentMoveNote(string note)
        {
            if (Options.DisplayAgentMoveNotes)
            {
                ToBlue();
                Console.WriteLine(note);
            }
        }
        public void LogAgentSummaryNote(string note)
        {
            if (Options.DisplayAgentSummaryNotes)
            {
                ToBlue();
                Console.WriteLine(note);
            }
        }

        public void LogRandomSeed(int randomSeed)
        {
            if (Options.DisplayRandomSeed)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("Simulation random seed: {0}", randomSeed);
                Console.WriteLine(string.Empty);
            }
        }

        public void Card(Card card)
        {
            if (card.Suit == Suit.Clubs || card.Suit == Suit.Spades)
            {
                ToBlack();
            }
            else
            {
                ToRed();
            }

            Console.Write(card);
        }

        private void Hand(IEnumerable<Card> cards, string name)
        {
            int padToLength = 38;
            ToBlue();
            Console.Write(" " + name.PadLeft(Logging.Log.Options.NamePad) + " ");

            foreach (var suit in new List<Suit> { Suit.Hearts, Suit.Spades, Suit.Diamonds, Suit.Clubs })
            {
                var cardsOfSuit = cards.OfSuit(suit);

                foreach (var card in cardsOfSuit.Ascending())
                {
                    Card(card);
                    Console.Write(" ");
                }

                Console.Write(new string(' ', padToLength - cardsOfSuit.Count() * 3));
            }

            NewLine();
        }

        #region Helpers
        private void ToBlue()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        private void ToGrey()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void ToRed()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
        }

        private void ToBlack()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void Reset()
        {
            Console.ResetColor();
        }

        private void NewLine()
        {
            Console.Write(Environment.NewLine);
        }
        #endregion /Helpers
    }
}
