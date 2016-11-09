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

        // I think this should be just the cards for the current hand
        // instead of PlayedHand being created early before the hand is complete.
        // => it needs the player Guid (or bring over the PlayedCard class).
        // this would mean PlayedHand could be created from it.
        public List<Card> CurrentHand { get; private set; }

        public List<PlayedHand> PlayedHands { get; private set; }
        
        public Card Play(Player player, Card card)
        {
            this.CurrentHand.Add(card);

        //    if (self.handInPlay.count == players.count)
        //    {
        //        let winner = HandWinEvaluator().evaluateWinner(self.handInPlay)
        //    let playedHand = PlayedHand(playedCards: self.handInPlay, winner: winner)
        //    self.playedHands.append(playedHand)
        //    self.handInPlay.removeAll()
        //    self.eventQueue.handFinished(playedHand)
        //}


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
            this.CurrentHand = new List<Card>();
            this.PlayedHands = new List<PlayedHand>();
        }
    }
}