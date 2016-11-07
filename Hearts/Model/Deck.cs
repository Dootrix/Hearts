using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;

namespace Hearts.Model
{
    public struct Deck 
    {
        public Deck(List<Card> cards)
        {
            this.Cards = cards;
        }

        // An array for security - prevents values being changed
        public List<Card> Cards { get; private set; }

        public bool HasCardsRemaining { get { return this.Cards.Any(); } }

        public Card Deal(Card card)
        {
            this.Cards.Remove(card);

            return card;
        }

        public Card DealNextCard()
        {
            if (!this.HasCardsRemaining)
            {
                return new Card();
            }

            var lastCard = this.Cards.Last();
            this.Cards.Remove(lastCard);

            return lastCard;
        }

        public void Shuffle()
        {
            this.Cards.Shuffle();
        }
    }
}