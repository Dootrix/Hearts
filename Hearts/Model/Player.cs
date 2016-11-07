using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hearts.Model
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Player
    {
        private Guid guid;

        public Player()
        {
            this.Hand = new List<Card>();
            this.guid = Guid.NewGuid();
        }

        public Guid Guid { get { return this.guid; } }

        public List<Card> Hand { get; private set; }

        public string DebuggerDisplay { get { return string.Join(" ", this.Hand); } }

        public void Receive(Card card)
        {
            this.Hand.Add(card);
        }

        public void Receive(List<Card> cards)
        {
            this.Hand.AddRange(cards);
        }

        public Card Play(Card card)
        {
            this.Hand.Remove(card);

            return card;
        }

        public IEnumerable<Card> Pass(IEnumerable<Card> cards)
        {
            this.Hand = this.Hand.Except(cards).ToList();

            return cards;
        }
    }
}