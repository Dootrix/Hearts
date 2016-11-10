using Hearts.Deal;
using Hearts.Factories;
using Hearts.Model;
using Hearts.Passing;
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
                return this.gameTable.PlayedTricks.Any(i => i.Cards.Any(j => j.Value.Suit == Suit.Hearts));
            }
        }

        public bool IsLeadTurn
        {
            get
            {
                return this.gameTable.CurrentTrick.Count() == 0;
            }
        }

        public bool IsFollowTurn
        {
            get
            {
                return this.gameTable.CurrentTrick.Count() > 0;
            }
        }

        public bool IsFirstHand
        {
            get
            {
                return this.gameTable.PlayedTricks.Count == 0;
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
                return this.gameTable.CurrentTrick;
            }
        }

        public void AddPlayer(Player player)
        {
            this.playerCircle.AddPlayer(player);
        }

        public void Play(int roundNumber)
        {
            var players = this.playerCircle.AllPlayers;
            this.gameTable = new GameTable(players.Count);
            this.dealer.DealStartingHands(players);
            var startingHands = players.ToDictionary(i => i, i => i.RemainingCards.ToList());            
            var currentPassingPlayer = this.playerCircle.FirstPlayer;
            var passedCards = new List<List<Card>> ();

            var passService = new PassService();

            for (int i = 0; i < players.Count; i++)
            {
                passedCards.Add(currentPassingPlayer.Agent.ChooseCardsToPass(startingHands[currentPassingPlayer]));
                currentPassingPlayer = passService.GetPassRecipient(roundNumber, players.Count, currentPassingPlayer);
            }

            for (int i = 0; i < players.Count; i++)
            {
                var receivingCards = passedCards[i + 1 == players.Count ? 0 : i + 1];
                players[i].Receive(receivingCards);
            }

            var handEvaluator = new HandWinEvaluator();
            var rulesEngine = new GameRulesEngine();
            var startingPlayer = this.GetStartingPlayer();

            while (players.Sum(i => i.RemainingCards.Count) > 0)
            {
                foreach (var player in this.playerCircle.GetOrderedPlayersStartingWith(startingPlayer))
                {
                    var legalCards = rulesEngine.LegalMoves(player.RemainingCards, this);
                    var card = player.Agent.ChooseCardToPlay(this, startingHands[player], player.RemainingCards, legalCards.ToList());
                    player.Play(card);
                    this.gameTable.Play(player, card);
                }

                var trick = this.gameTable.PlayedTricks.Last();
                var trickWinnerId = handEvaluator.EvaluateWinner(trick);
                trick.Winner = players.Single(i => i == trickWinnerId);
                startingPlayer = trick.Winner;
            }
        }

        private Player GetStartingPlayer()
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