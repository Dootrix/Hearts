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
        
        public List<Card> CurrentTrick { get; private set; }

        public List<PlayedTrick> PlayedTricks { get; private set; }
        
        public Card Play(Player player, Card card)
        {
            this.CurrentTrick.Add(card);
            
            var lastPlayedTrick = this.PlayedTricks.LastOrDefault();

            if (lastPlayedTrick == null || lastPlayedTrick.Cards.Count == this.playerCount)
            {
                var playedHand = new PlayedTrick();
                this.PlayedTricks.Add(playedHand);
                lastPlayedTrick = playedHand;
            }

            lastPlayedTrick.Cards.Add(player, card);
            
            return card;
        }

        private void ClearPiles()
        {
            this.CurrentTrick = new List<Card>();
            this.PlayedTricks = new List<PlayedTrick>();
        }
    }
}