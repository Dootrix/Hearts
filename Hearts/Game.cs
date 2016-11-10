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
            var startingHands = players.ToDictionary(i => i, i => i.RemainingCards.ToList());

            // TODO - pass the cards.
            
            var currentPassingPlayer = this.playerCircle.FirstPlayer;
            var passedCards = new List<List<Card>> ();

            for (int i = 0; i < players.Count; i++)
            {
                passedCards.Add(currentPassingPlayer.Agent.ChooseCardsToPass(startingHands[currentPassingPlayer]));

                // TODO: Assuming passing left
                currentPassingPlayer = currentPassingPlayer.NextPlayer;
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