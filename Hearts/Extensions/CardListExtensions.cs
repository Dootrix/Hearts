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