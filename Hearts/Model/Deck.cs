using System.Collections.Generic;
using System.Linq;

namespace Hearts
{
    public struct Deck 
    {
        public Deck(IEnumerable<Card> cards)
        {
            this.Cards = cards.ToArray();
        }

        // An array for security - prevents values being changed
        public Card[] Cards { get; private set; }

        public bool HasCardsRemaining { get { return this.Cards.Any(); } }

        public Card Deal(Card card)
        {
            this.Remove(card);

            return card;
        }

        public Card DealNextCard()
        {
            if (!this.HasCardsRemaining)
            {
                return new Card();
            }

            var lastCard = this.Cards.Last();
            this.Remove(lastCard);

            return lastCard;
        }

        public void Shuffle()
        {
            this.Cards.Shuffle();
        }

        private void Remove(Card card)
        {
            var elements = this.Cards.ToList();
            elements.Remove(card);
            this.Cards = elements.ToArray();
        }
    }
}