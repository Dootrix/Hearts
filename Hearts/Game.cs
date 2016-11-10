using Hearts.Deal;
using Hearts.Factories;
using Hearts.Model;
using Hearts.Rules;
using Hearts.Scoring;
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

            var handEvaluator = new HandWinEvaluator();

            var startingPlayer = this.GetStartingPlayer();

            while (players.Sum(i => i.RemainingCards.Count) > 0)
            {
                foreach (var player in this.playerCircle.GetOrderedPlayersStartingWith(startingPlayer))
                {
                    var card = player.Agent.ChooseCardToPlay(this, player.RemainingCards);
                    player.Play(card);
                    this.gameTable.Play(player, card);
                }

                var trick = this.gameTable.PlayedHands.Last();
                var trickWinnerId = handEvaluator.EvaluateWinnerId(trick);
                trick.Winner = players.Single(i => i.Guid == trickWinnerId);
                startingPlayer = trick.Winner;
            }
        }

        private Player GetStartingPlayer()
        {
            return this.playerCircle.AllPlayers
                .Where(i => i.RemainingCards
                    .Any(j => j.Suit == Suit.Clubs))
                .OrderBy(i => i.RemainingCards
                    .Where(j => j.Suit == Suit.Clubs)
                    .Min(k => k.Kind))
                .First();
        }

        private Player GetStartingPlayer2()
        {
            var lowestClub = this.playerCircle.AllPlayers
                .SelectMany(i => i.RemainingCards)
                .Where(j => j.Suit == Suit.Clubs)
                .Min(k => k.Kind);

            return this.playerCircle.AllPlayers
                .Single(i => i.RemainingCards.Any(j => j.Suit == Suit.Clubs && j.Kind == lowestClub));
        }
    }
}