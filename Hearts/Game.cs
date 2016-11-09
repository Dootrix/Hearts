using Hearts.Deal;
using Hearts.Factories;
using Hearts.Model;
using Hearts.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts
{
    public class Game
    {
        private PlayerCircle playerCircle;
        private GameTable gameTable;
        private Dealer dealer;

        public Game()
        {
            this.dealer = new Dealer(new StandardDeckFactory(), new EvenHandDealAlgorithm());
            this.playerCircle = new PlayerCircle();
        }
        
        public bool IsHeartsBroken
        {
            get
            {
                return this.gameTable.PlayedHands.Any(i => i.Cards.Any(j => j.Value.Suit == Suit.Hearts));
            }
        }

        public bool IsLeadTurn
        {
            get
            {
                return this.gameTable.CurrentHand.Count() == 0;
            }
        }

        public bool IsFollowTurn
        {
            get
            {
                return this.gameTable.CurrentHand.Count() > 0;
            }
        }

        public bool IsFirstHand
        {
            get
            {
                return this.gameTable.PlayedHands.Count == 0;
            }
        }

        public bool IsFirstLeadHand
        {
            get
            {
                return this.IsLeadTurn && this.IsFirstHand;
            }
        }

        public List<Card> CurrentHand
        {
            get
            {
                return this.gameTable.CurrentHand;
            }
        }

        public void AddPlayer(Player player)
        {
            this.playerCircle.AddPlayer(player);
        }

        public void Play()
        {
            var players = this.playerCircle.AllPlayers;

            this.gameTable = new GameTable(players.Count);
            this.dealer.DealStartingHands(players);

            // TODO - pass the cards.

            var startingPlayer = this.GetStartingPlayer();

            while(players.Sum(i => i.RemainingCards.Count) > 0)
            {
                foreach (var player in this.playerCircle.GetOrderedPlayersStartingWith(startingPlayer))
                {
                    var card = player.Agent.ChooseCardToPlay(this, player.RemainingCards);
                    this.gameTable.Play(player, card);
                }

                //TODO - get the new starting player
            }
        }

        //// TODO - I think this logic can be inside GameTable.Play
        //public void PlayCard(Player player, Card card)
        //{
        //    this.CurrentHand.Cards.Add(player.Guid, card);

        //    if (this.CurrentHand.Cards.Count == this.playerCircle.AllPlayers.Count())
        //    {
        //        // TODO: Implement the following - but check my way of using CurrentHand doesn't break stuff:
        //        /*
        //        let winner = HandWinEvaluator().evaluateWinner(self.handInPlay);
        //        this.CurrentHand.Winner = winner;

        //        let playedHand = PlayedHand(playedCards: self.handInPlay, winner: winner)
        //        self.playedHands.append(playedHand)
        //        self.handInPlay.removeAll()
        //        self.eventQueue.handFinished(playedHand)
        //        */
        //    }
        //}

        private Player GetStartingPlayer()
        {
            // A complicated way of finding who has the two of clubs :) ...
            // but this avoids duplicating the game rule logic.
            var evaluator = new CardPlayEvaluator();

            foreach (var player in this.playerCircle.AllPlayers)
            {
                if(evaluator.GetPossibleCards(player.RemainingCards, this).Count() > 0)
                {
                    return player;
                }
            }

            return null;
        }
    }
}