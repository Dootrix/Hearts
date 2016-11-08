using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hearts.Model
{
    public class GameTable
    {
        private int playerCount = 0;

        public GameTable(int playerCount)
        {
            this.playerCount = playerCount;
            this.ClearPiles();
        }

        public List<Card> CardsInPlay { get; private set; }

        public List<PlayedHand> PlayedHands { get; private set; }
        
        public Card Play(Player player, Card card)
        {
            this.CardsInPlay.Add(card);

            var lastPlayedHand = this.PlayedHands.LastOrDefault();

            if (lastPlayedHand == null || lastPlayedHand.Cards.Count == this.playerCount)
            {
                var playedHand = new PlayedHand();
                this.PlayedHands.Add(playedHand);
                lastPlayedHand = playedHand;
            }

            lastPlayedHand.Cards.Add(player.Guid, card);
            
            return card;
        }

        private void ClearPiles()
        {
            this.CardsInPlay = new List<Card>();
            this.PlayedHands = new List<PlayedHand>();
        }
    }
}