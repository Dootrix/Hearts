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
            this.RemainingCards = new List<Card>();
            this.guid = Guid.NewGuid();
        }

        public Guid Guid { get { return this.guid; } }

        public List<Card> RemainingCards { get; private set; }

        public string DebuggerDisplay { get { return string.Join(" ", this.RemainingCards); } }

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