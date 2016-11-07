using System;
using System.Linq;

namespace Hearts
{
    public class StandardDeckFactory : IFactory<Deck>
    {
        public Deck Create()
        {
            var clubs = Enum.GetValues(typeof(Kind)).Cast<Kind>().Select(k => new Card(k, Suit.Clubs));
            var diamonds = Enum.GetValues(typeof(Kind)).Cast<Kind>().Select(k => new Card(k, Suit.Diamonds));
            var hearts = Enum.GetValues(typeof(Kind)).Cast<Kind>().Select(k => new Card(k, Suit.Hearts));
            var spades = Enum.GetValues(typeof(Kind)).Cast<Kind>().Select(k => new Card(k, Suit.Spades));

            return new Deck(clubs.Union(diamonds).Union(hearts).Union(spades));
        }
    }
}
