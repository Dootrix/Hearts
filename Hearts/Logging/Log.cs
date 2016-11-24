using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Extensions;
using Hearts.Model;
using Hearts.Scoring;
using Hearts.Attributes;

namespace Hearts.Logging
{
    public static class Log
    {
        public static ILogDisplayOptions Options = new DefaultLogOptions();
        
        public static void Gap()
        {
            Console.WriteLine(string.Empty);
        }

        public static void StartingHands(Dictionary<Player, IEnumerable<Card>> hands)
        {
            if (!Options.DisplayStartingHands) return;

            ToGrey();
            Console.WriteLine("Starting Hands:");

            foreach (var hand in hands)
            {
                hand.Value.Log(hand.Key.Name);
            }

            NewLine();
        }

        public static void PassDirection(Pass pass)
        {
            ToGrey();
            Console.WriteLine("Pass direction: " + Abbreviation.Get(pass));
            NewLine();
        }

        public static void HandsAfterPass(Dictionary<Player, IEnumerable<Card>> hands)
        {
            if (!Options.DisplayHandsAfterPass) return;

            ToGrey();
            Console.WriteLine("Post-Pass Hands:");

            foreach (var hand in hands)
            {
                hand.Value.Log(hand.Key.Name);
            }

            NewLine();
        }

        public static void Pass(Player player, IEnumerable<Card> cards)
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

        public static void TrickSummary(PlayedTrick trick)
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
                }

                NewLine();
            }

            NewLine();
        }

        public static void IllegalPass(Player player, IEnumerable<Card> cards)
        {
            if (!Options.DisplayExceptions) return;

            ToBlue();
            Console.WriteLine(player.Name + " made an ILLEGAL PASS! (" + string.Join(", ", cards.Select(i => i.ToString())) + ") - " + player.DebuggerDisplay);
        }

        public static void IllegalPlay(Player player, Card card)
        {
            if (!Options.DisplayExceptions) return;

            ToBlue();
            Console.WriteLine(player.Name + " played an ILLEGAL CARD! (" + card + ") - " + player.DebuggerDisplay);
        }

        public static void OutOfCardsException()
        {
            if (!Options.DisplayExceptions) return;

            ToBlue();
            Console.WriteLine("*** OUT OF CARDS! ***");
        }

        public static void PointsForRound(Dictionary<Player, int> scores)
        {
            if (!Options.DisplayPointsForRound) return;

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

        public static void LogFinalWinner(Dictionary<Player, int> results)
        {
            if (!Options.DisplayLogFinalWinner) return;

            ToGrey();
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
            Console.WriteLine("__________________________________________________________________");
            Console.WriteLine(string.Empty);

            foreach (var score in results)
            {
                ToBlue();
                Console.Write(" " + score.Key.Name.PadLeft(Options.NamePad) + " ");
                ToGrey();
                Console.Write(" : ");
                ToBlue();
                Console.Write(score.Value.ToString().PadLeft(3));
                ToGrey();
                Console.Write(" pts ");

                if (score.Value == results.Min(i => i.Value))
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
        
        public static void LogSimulationSummary(int gameCount, Dictionary<Player, int> victories, Dictionary<Bot, Tuple<int, int>> moonshots)
        {
            if (!Options.DisplaySimulationSummary) return;

            ToGrey();
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
            Console.WriteLine("__________________________________________________________________");
            Console.WriteLine(string.Empty);
            Console.WriteLine("Simulation Stats");
            Console.WriteLine("Based on {0} game{1}", gameCount, gameCount > 1 ? "s" : string.Empty);
            Console.WriteLine("Moonshots:");

            foreach (var moonshot in moonshots)
            {
                var player = moonshot.Key.Player;
                Console.WriteLine("{0} {1}/{2}", player.Name, moonshot.Value.Item1, moonshot.Value.Item2);
            }

            Console.WriteLine(string.Empty);

            foreach (var victory in victories)
            {
                ToBlue();
                Console.Write(" " + victory.Key.Name.PadLeft(Options.NamePad) + " ");
                ToGrey();
                Console.Write(" : ");
                ToBlue();
                float percent = (float)victory.Value / gameCount;
                Console.Write(percent.ToString("0.00% ").PadLeft(8));
                ToGrey();

                if (victory.Value == victories.Max(i => i.Value))
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

        public static void LogAgentMoveNote(string note)
        {
            if (Options.DisplayAgentMoveNotes)
            {
                ToBlue();
                Console.WriteLine(note);
            }
        }
        public static void LogAgentSummaryNote(string note)
        {
            if (Options.DisplayAgentSummaryNotes)
            {
                ToBlue();
                Console.WriteLine(note);
            }
        }

        public static void Card(Card card)
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

        #region Helpers
        public static void ToBlue()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public static void ToGrey()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void ToRed()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
        }

        public static void ToBlack()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void Reset()
        {
            Console.ResetColor();
        }

        public static void NewLine()
        {
            Console.Write(Environment.NewLine);
        }
        #endregion /Helpers
    }
}
