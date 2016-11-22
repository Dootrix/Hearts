using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Hearts.Scoring;

namespace Hearts.Extensions
{
    public static class CardListExtensions
    {
        public static void Log(this IEnumerable<Card> self, string name)
        {
            int padToLength = 38;
            Logging.Log.ToBlue();
            Console.Write(" " + name.PadLeft(Logging.Log.Options.NamePad) + " ");

            foreach (var suit in new List<Suit> { Suit.Hearts, Suit.Spades, Suit.Diamonds, Suit.Clubs })
            {
                var cardsOfSuit = self.Where(i => i.Suit == suit);

                foreach (var card in cardsOfSuit.OrderBy(i => i.Kind))
                {
                    Logging.Log.Card(card);
                    Console.Write(" ");
                }

                Console.Write(new string(' ', padToLength - cardsOfSuit.Count() * 3));
            }

            Logging.Log.NewLine();
        }

        public static int Score(this IEnumerable<Card> self)
        {
            return new ScoreEvaluator().CalculateScore(self);
        }

        public static string ToDebugString(this IEnumerable<Card> self)
        {
            return string.Join(",", self);
        }
    }
}