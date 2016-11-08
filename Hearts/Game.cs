using Hearts.Deal;
using Hearts.Factories;
using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts
{
    public class Game
    {
        public GameTable GameTable;
        public Dealer Dealer;
        public Player[] Players;

        public Game()
        {
            this.Init();
        }
        
        public bool IsHeartsBroken
        {
            get
            {
                return this.GameTable.PlayedHands.Any(i => i.Cards.Any(j => j.Value.Suit == Suit.Hearts));
            }
        }

        public bool IsLeadTurn
        {
            get
            {
                return this.CurrentHand.Cards.Count() == 0;
            }
        }

        public bool IsFollowTurn
        {
            get
            {
                return this.CurrentHand.Cards.Count() > 0;
            }
        }

        public bool IsFirstHand
        {
            get
            {
                return this.GameTable.PlayedHands.Count == 0;
            }
        }

        public bool IsFirstLeadHand
        {
            get
            {
                return this.IsLeadTurn && this.IsFirstHand;
            }
        }

        public PlayedHand CurrentHand
        {
            get
            {
                var lastHand = this.GameTable.PlayedHands.LastOrDefault();

                if (lastHand == null && this.Players.First().RemainingCards.Count > 0)
                {
                    lastHand = new PlayedHand();
                    this.GameTable.PlayedHands.Add(lastHand);
                }

                return lastHand;
            }
        }

        public void PlayCard(Player player, Card card)
        {
            this.CurrentHand.Cards.Add(player.Guid, card);

            if (this.CurrentHand.Cards.Count == this.Players.Count())
            {
                // TODO: Implement the following - but check my way of using CurrentHand doesn't break stuff:
                /*
                let winner = HandWinEvaluator().evaluateWinner(self.handInPlay);
                this.CurrentHand.Winner = winner;

                let playedHand = PlayedHand(playedCards: self.handInPlay, winner: winner)
                self.playedHands.append(playedHand)
                self.handInPlay.removeAll()
                self.eventQueue.handFinished(playedHand)
                */
            }
        }

        private void Init()
        {
            this.Dealer = new Dealer(new StandardDeckFactory(), new EvenHandDealAlgorithm());
            this.Players = new List<Player> { new Player(), new Player(), new Player(), new Player() }.ToArray();
            this.GameTable = new GameTable(this.Players.Count());
            this.Dealer.DealStartingHands(this.Players);
        }
    }
}