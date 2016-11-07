using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hearts
{
    public class GameTable
    {
        public GameTable()
        {
            this.ClearPiles();
        }

        public Card[] CardsInPlay { get; private set; }

        public Card[] DiscardPile { get; private set; }

        public Card Discard(Card card)
        {
            if (this.CardsInPlay.Contains(card))
            {
                this.RemoveFromCardsInPlay(card);
            }

            this.AddToDiscards(card);

            return card;
        }

        public IEnumerable<Card> Discard(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                if (this.CardsInPlay.Contains(card))
                {
                    this.RemoveFromCardsInPlay(card);
                }
            }

            this.AddToDiscards(cards);

            return cards;
        }

        public Card Play(Card card)
        {
            this.AddToCardsInPlay(card);

            return card;
        }

        public IEnumerable<Card> Play(IEnumerable<Card> cards)
        {
            this.AddToCardsInPlay(cards);

            return cards;
        }

        public Card PickupCardInPlay(Player player, Card card)
        {
            if (!this.CardsInPlay.Contains(card))
            {
                throw new InvalidOperationException("Cannot pickup Card In Play. Card {0} is not in play.");
            }

            this.RemoveFromCardsInPlay(card);
            player.Receive(card);

            return card;
        }

        private void ClearPiles()
        {
            this.CardsInPlay = new Card[0];
            this.DiscardPile = new Card[0];
        }

        private void AddToDiscards(Card card)
        {
            var elements = this.DiscardPile.ToList();
            elements.Add(card);
            this.DiscardPile = elements.ToArray();
        }

        private void AddToDiscards(IEnumerable<Card> cards)
        {
            var elements = this.DiscardPile.ToList();
            elements.AddRange(cards);
            this.DiscardPile = elements.ToArray();
        }

        private void AddToCardsInPlay(Card card)
        {
            var elements = this.CardsInPlay.ToList();
            elements.Add(card);
            this.CardsInPlay = elements.ToArray();
        }

        private void AddToCardsInPlay(IEnumerable<Card> cards)
        {
            var elements = this.CardsInPlay.ToList();
            elements.AddRange(cards);
            this.CardsInPlay = elements.ToArray();
        }

        private void RemoveFromCardsInPlay(Card card)
        {
            var elements = this.CardsInPlay.ToList();
            elements.Remove(card);
            this.CardsInPlay = elements.ToArray();
        }
    }
}