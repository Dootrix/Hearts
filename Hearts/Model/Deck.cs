using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;
using Hearts.Randomisation;

namespace Hearts.Model
{
    public class Deck 
    {
        public Deck(List<Card> cards)
        {
            this.Cards = cards;
        }
        
        public List<Card> Cards { get; private set; }

        public bool HasCardsRemaining { get { return this.Cards.Any(); } }

        public Card Deal(Card card)
        {
            this.Cards.Remove(card);

            return card;
        }

        public Card DealNextCard()
        {
            var lastCard = this.Cards.Last();
            this.Cards.Remove(lastCard);

            return lastCard;
        }

        public void Shuffle(IControlledRandom random)
        {
            this.Cards.Shuffle(random);
        }
    }
}