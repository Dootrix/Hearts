﻿using System;
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
            this.Reset();
        }
        
        public List<PlayedCard> CurrentTrick { get; private set; }

        public List<PlayedTrick> PlayedTricks { get; private set; }

        public void Reset()
        {

            this.CurrentTrick = new List<PlayedCard>();
            this.PlayedTricks = new List<PlayedTrick>();
        }

        public void BeginTrick()
        {
            this.CurrentTrick = new List<PlayedCard>();
        }

        public void EndTrick()
        {
            var playedTrick = new PlayedTrick();

            foreach (var playedCard in this.CurrentTrick)
            { 
                playedTrick.Cards.Add(playedCard.Player, playedCard.Card);
            }

            this.PlayedTricks.Add(playedTrick);
        }

        public Card Play(Player player, Card card)
        {
            this.CurrentTrick.Add(new PlayedCard(player, card));
            
            return card;
        }
    }
}