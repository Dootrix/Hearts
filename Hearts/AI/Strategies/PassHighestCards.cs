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
        public List<Card> ChooseCardsToPass(List<Card> startingCards, Pass pass)
        {
            var cards = startingCards
                .OrderByDescending(i => i.Kind)
                .ThenBy(i => i.Suit).Take(3).ToList();

            return cards;
        }
    }
}
