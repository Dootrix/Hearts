using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Model;

namespace Hearts.AI.Strategies
{
    public class PassHighestCards : IPassStrategy
    {
        public IEnumerable<Card> ChooseCardsToPass(IEnumerable<Card> startingCards, Pass pass)
        {
            var cards = startingCards
                .OrderByDescending(i => i.Kind)
                .ThenBy(i => i.Suit).Take(3);

            return cards;
        }
    }
}
