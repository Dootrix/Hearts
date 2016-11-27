using System.Collections;
using System.Collections.Generic;

namespace Hearts.Model
{
    /// <summary>
    /// A collection of cards owned by a player.
    /// </summary>
    public class CardHand : IEnumerable<Card>
    {
        private readonly List<Card> cards;

        public CardHand(Player owner, IEnumerable<Card> cards)
        {
            this.Owner = owner;
            this.cards = new List<Card>(cards);
        }

        public Player Owner { get; private set; }

        public void AddRange(IEnumerable<Card> cardsToAdd)
        {
            this.cards.AddRange(cardsToAdd);
        }

        public void RemoveRange(IEnumerable<Card> cardsToRemove)
        {
            foreach (var cardToRemove in cardsToRemove)
            {
                cards.Remove(cardToRemove);
            }            
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return this.cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.cards.GetEnumerator();
        }
    }
}
