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
    public static class Log
    {
        public static ILogDisplayOptions Options = new DefaultLogOptions();
        
        public static void Gap()
        {
            Console.WriteLine(string.Empty);
        }

        public static void StartingHands(Dictionary<Player, List<Card>> hands)
        {
            if (!Options.DisplayStartingHands) return;

            ToGrey();
            Console.WriteLine("Starting Hands:");

            foreach (var hand in hands)
            {
                Player(hand.Key);
            }

            NewLine();
        }

        public static void HandsAfterPass(Dictionary<Player, List<Card>> hands)
        {
            if (!Options.DisplayHandsAfterPass) return;

            ToGrey();
            Console.WriteLine("Post-Pass Hands:");

            foreach (var hand in hands)
            {
                Player(hand.Key);
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
            Console.WriteLine(player.Name + " made an ILLEGAL PASS! (" + string.Join(", ", cards.Select(i => i.DebuggerDisplay)) + ") - " + player.DebuggerDisplay);
        }

        public static void IllegalPlay(Player player, Card card)
        {
            if (!Options.DisplayExceptions) return;

            ToBlue();
            Console.WriteLine(player.Name + " played an ILLEGAL CARD! (" + card.DebuggerDisplay + ") - " + player.DebuggerDisplay);
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
        
        public static void LogSimulationSummary(int gameCount, Dictionary<Player, int> victories)
        {
            if (!Options.DisplaySimulationSummary) return;

            ToGrey();
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
            Console.WriteLine("__________________________________________________________________");
            Console.WriteLine(string.Empty);
            Console.WriteLine("Simulation Stats");
            Console.WriteLine("Based on {0} game{1}", gameCount, gameCount > 1 ? "s" : string.Empty);
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

        public static void Player(Player player)
        {
            var cards = player.RemainingCards;
            int padToLength = 25;
            ToBlue();
            Console.Write(" " + player.Name.PadLeft(Options.NamePad) + " ");
            
            foreach(var suit in new List<Suit> { Suit.Hearts, Suit.Spades, Suit.Diamonds, Suit.Clubs })
            {
                var cardsOfSuit = cards.Where(i => i.Suit == suit);

                foreach (var card in cardsOfSuit.OrderBy(i => i.Kind))
                {
                    Card(card);
                    Console.Write(" ");
                }

                Console.Write(new string(' ', padToLength - cardsOfSuit.Count() * 3));
            }

            NewLine();
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

            Console.Write(card.DebuggerDisplay);
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
