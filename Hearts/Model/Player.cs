using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hearts
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Player
    {
        private Guid guid;

        public Player()
        {
            this.Hand = new Card[0];
            this.guid = Guid.NewGuid();
        }

        public Guid Guid { get { return this.guid; } }

        public Card[] Hand { get; private set; }

        public string DebuggerDisplay { get { return string.Join(" ", this.Hand); } }

        public void Receive(Card card)
        {
            this.AddToHand(card);
        }

        public void Receive(List<Card> cards)
        {
            this.AddToHand(cards);
        }

        public Card Play(Card card)
        {
            this.RemoveFromHand(card);

            return card;
        }

        public IEnumerable<Card> Pass(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                this.RemoveFromHand(card);
            }

            return cards;
        }

        private void AddToHand(Card card)
        {
            var elements = this.Hand.ToList();
            elements.Add(card);
            this.Hand = elements.ToArray();
        }

        private void AddToHand(IEnumerable<Card> cards)
        {
            var elements = this.Hand.ToList();
            elements.AddRange(cards);
            this.Hand = elements.ToArray();
        }

        private void RemoveFromHand(Card card)
        {
            var elements = this.Hand.ToList();
            elements.Remove(card);
            this.Hand = elements.ToArray();
        }
    }
}