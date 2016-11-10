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
        
        public List<PlayedCard> CurrentTrick { get; private set; }

        public List<PlayedTrick> PlayedTricks { get; private set; }

        public void BeginTrick()
        {
            this.CurrentTrick = new List<PlayedCard>();
        }

        public void EndTrick()
        {
            var playedTrick = new PlayedTrick();

            foreach (var trick in this.CurrentTrick)
            { 
                playedTrick.Cards.Add(trick.Player, trick.Card);
            }

            this.PlayedTricks.Add(playedTrick);
        }

        public Card Play(Player player, Card card)
        {
            this.CurrentTrick.Add(new PlayedCard { Player = player, Card = card });
            
            return card;
        }

        private void ClearPiles()
        {
            this.CurrentTrick = new List<PlayedCard>();
            this.PlayedTricks = new List<PlayedTrick>();
        }
    }
}