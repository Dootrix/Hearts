using System;
using System.Collections.Generic;
using System.Linq;
using Hearts.Attributes;
using Hearts.Model;
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
                var cardsOfSuit = self.OfSuit(suit);

                foreach (var card in cardsOfSuit.Ascending())
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

        public static IEnumerable<Card> FromDebugString(string debugString)
        {
            var cards = debugString.Split(',');
            return cards.Where(_ => _.Length == 2)
                .Select(cardString => new Card(
                    cardString[0].GetAbbreviatedValue(Kind.Two),
                    cardString[1].GetAbbreviatedValue(Suit.Clubs)));
        }

    }
}