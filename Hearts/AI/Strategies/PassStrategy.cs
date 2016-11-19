using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;

namespace Hearts.AI.Strategies
{
    public class PassStrategy : IPassStrategy
    {
        public IEnumerable<Card> ChooseCardsToPass(IEnumerable<Card> startingCards, Pass pass)
        {
            var cardsToPass = new List<Card>();

            //return GetSuitVoiders(startingCards.Where(x => x.Suit != Suit.Spades), 3).ToList();

            var spades = startingCards.Where(x => x.Suit == Suit.Spades).ToArray();

            // pass Q/K/A spades if there are 3 or less spades.
            if (spades.Length <= 3)
            {
                cardsToPass.AddRange(spades.Intersect(GetHighSpades()));
            }

            //var hearts = startingCards.Where(x => x.Suit == Suit.Hearts).ToArray();

            //// pass Q/K/A spades if there are 3 or less spades.
            //if (hearts.Length <= 3)
            //{
            //    cardsToPass.AddRange(hearts.Where(x => x.Kind >= Kind.Queen));
            //}

            if (cardsToPass.Count < 3)
            {
                cardsToPass.AddRange(GetSuitVoiders(startingCards.Except(cardsToPass), 3 - cardsToPass.Count));
            }

            if(cardsToPass.Count < 3)
            {
                Console.WriteLine();
            }

            return cardsToPass.Take(3);
        }

        private IEnumerable<Card> GetSuitVoiders(IEnumerable<Card> cards, int number)
        {
            // Suit voiders apart from spades
            var suitGroupedCards = cards
                .Where(a => a.Suit != Suit.Spades)
                .GroupBy(x => x.Suit)
                .OrderBy(y => y.Count());

            return suitGroupedCards
                .SelectMany(x => x.AsEnumerable().OrderByDescending(y => y.Kind))
                .Take(number)
                .ToList();
        }

        private IEnumerable<Card> GetHighSpades()
        {
            return new List<Card> {
                    new Card(Kind.Queen, Suit.Spades),
                    new Card(Kind.King, Suit.Spades),
                    new Card(Kind.Ace, Suit.Spades),
                };
        }
    }
}
