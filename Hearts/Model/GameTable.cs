using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Model
{
    public class GameTable
    {
        public GameTable()
        {
            this.ClearPiles();
        }

        public List<Card> CardsInPlay { get; private set; }

        public List<Card> DiscardPile { get; private set; }

        public Card Discard(Card card)
        {
            if (this.CardsInPlay.Contains(card))
            {
                this.CardsInPlay.Remove(card);
            }

            this.DiscardPile.Add(card);

            return card;
        }

        public IEnumerable<Card> Discard(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                if (this.CardsInPlay.Contains(card))
                {
                    this.CardsInPlay.Remove(card);
                }
            }

            this.DiscardPile.AddRange(cards.ToList());

            return cards;
        }

        public Card Play(Card card)
        {
            this.CardsInPlay.Add(card);

            return card;
        }

        public IEnumerable<Card> Play(IEnumerable<Card> cards)
        {
            this.CardsInPlay.AddRange(cards);

            return cards;
        }

        public Card PickupCardInPlay(Player player, Card card)
        {
            if (!this.CardsInPlay.Contains(card))
            {
                throw new InvalidOperationException("Cannot pickup Card In Play. Card {0} is not in play.");
            }

            this.CardsInPlay.Remove(card);
            player.Receive(card);

            return card;
        }

        private void ClearPiles()
        {
            this.CardsInPlay = new List<Card>();
            this.DiscardPile = new List<Card>();
        }
    }
}