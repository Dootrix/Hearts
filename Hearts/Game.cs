using Hearts.Deal;
using Hearts.Factories;
using Hearts.Model;
using Hearts.Passing;
using Hearts.Rules;
using Hearts.Scoring;
using System;
using System.Collections.Generic;
using System.Linq;
using Hearts.Logging;

namespace Hearts
{
    public class Game
    {
        private PlayerCircle playerCircle;
        private GameTable gameTable;
        private Dealer dealer;

        public Game(IEnumerable<Player> players)
        {
            this.dealer = new Dealer(new StandardDeckFactory(), new EvenHandDealAlgorithm());
            this.playerCircle = new PlayerCircle();
            this.AddPlayers(players);
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
                return !this.gameTable.CurrentTrick.Any();
            }
        }

        public bool IsFollowTurn
        {
            get
            {
                return this.gameTable.CurrentTrick.Any();
            }
        }

        public bool IsFirstHand
        {
            get
            {
                return !this.gameTable.PlayedTricks.Any();
            }
        }

        public bool IsFirstLeadHand
        {
            get
            {
                return this.IsLeadTurn && this.IsFirstHand;
            }
        }

        public List<PlayedCard> CurrentTrick
        {
            get
            {
                return this.gameTable.CurrentTrick;
            }
        }

        public void AddPlayers(IEnumerable<Player> players)
        {
            foreach(var player in players)
            { 
                this.playerCircle.AddPlayer(player);
            }
        }

        public RoundResult Play(int roundNumber)
        {
            var players = this.playerCircle.AllPlayers;
            this.gameTable = new GameTable(players.Count);
            this.dealer.DealStartingHands(players);
            var startingHands = players.ToDictionary(i => i, i => i.RemainingCards.ToList());

            Log.StartingHands(startingHands);

            new PassService().HandlePassing(roundNumber, players, startingHands, this.playerCircle.FirstPlayer);

            Log.HandsAfterPass(players.ToDictionary(i => i, i => i.RemainingCards.ToList()));

            var handEvaluator = new HandWinEvaluator();
            var rulesEngine = new GameRulesEngine();
            var startingPlayer = this.playerCircle.GetStartingPlayer();
            
            while (players.Sum(i => i.RemainingCards.Count) > 0)
            {
                this.gameTable.BeginTrick();

                foreach (var player in this.playerCircle.GetOrderedPlayersStartingWith(startingPlayer))
                {
                    var legalCards = rulesEngine.LegalMoves(player.RemainingCards, this);
                    var card = player.Agent.ChooseCardToPlay(this, startingHands[player], player.RemainingCards, legalCards.ToList());
                    
                    if (!legalCards.Contains(card))
                    {
                        // TODO: Handle illegal move
                        Log.IllegalPlay(player, card);
                    }

                    player.Play(card);
                    this.gameTable.Play(player, card);
                }

                this.gameTable.EndTrick();
                var trick = this.gameTable.PlayedTricks.Last();
                var trickWinner = handEvaluator.EvaluateWinner(trick);
                trick.Winner = trickWinner;
                startingPlayer = trick.Winner;

                Log.TrickSummary(trick);
            }

            var scores = players.ToDictionary(i => i, i => new ScoreEvaluator().CalculateScore(this.gameTable.PlayedTricks.Where(j => j.Winner == i)));
            var tricks = players.ToDictionary(i => i, i => this.gameTable.PlayedTricks.Where(j => j.Winner == i).ToList());

            Log.PointsForRound(scores);

            return new RoundResult
            {
                Scores = scores,
                Tricks = tricks
            };
        }
    }
}
