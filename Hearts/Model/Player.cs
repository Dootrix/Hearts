using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hearts.Extensions;

namespace Hearts.Model
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Player
    {
        private Guid guid;

        public Player()
        {
            this.RemainingCards = new List<Card>();
            this.guid = Guid.NewGuid();
        }

        public Guid Guid { get { return this.guid; } }

        public Player NextPlayer { get; set; }

        public Player PreviousPlayer { get; set; }

        public List<Card> RemainingCards { get; private set; }

        public string DebuggerDisplay
        {
            get
            {
                int padToLength = this.RemainingCards.Count * 2;
                string hearts = "♥: " + string.Join(" ", this.RemainingCards.Where(i => i.Suit == Suit.Hearts).OrderBy(i => i.Kind).Select(i => i.Kind.ToAbbreviation())).PadRight(padToLength);
                string spades = "♠: " + string.Join(" ", this.RemainingCards.Where(i => i.Suit == Suit.Spades).OrderBy(i => i.Kind).Select(i => i.Kind.ToAbbreviation())).PadRight(padToLength);
                string diamonds = "♦: " + string.Join(" ", this.RemainingCards.Where(i => i.Suit == Suit.Diamonds).OrderBy(i => i.Kind).Select(i => i.Kind.ToAbbreviation())).PadRight(padToLength);
                string clubs = "♣: " + string.Join(" ", this.RemainingCards.Where(i => i.Suit == Suit.Clubs).OrderBy(i => i.Kind).Select(i => i.Kind.ToAbbreviation())).PadRight(padToLength);

                return string.Join("                  ", new List<string> { hearts, spades, diamonds, clubs });
            }
        }

        public void Receive(Card card)
        {
            this.RemainingCards.Add(card);
        }

        public void Receive(List<Card> cards)
        {
            this.RemainingCards.AddRange(cards);
        }

        public Card Play(Card card)
        {
            this.RemainingCards.Remove(card);

            return card;
        }

        public IEnumerable<Card> Pass(IEnumerable<Card> cards)
        {
            this.RemainingCards = this.RemainingCards.Except(cards).ToList();

            return cards;
        }
    }
}