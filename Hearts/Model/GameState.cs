using System.Collections.Generic;
using System.Linq;

namespace Hearts.Model
{
    public class GameState
    {
        public GameState(int numberOfPlayers)
        {
            this.NumberOfPlayers = numberOfPlayers;
            this.Reset();
        }

        public int NumberOfPlayers { get; private set; }

        public List<PlayedCard> CurrentTrick { get; private set; }

        public List<PlayedTrick> PlayedTricks { get; private set; }

        public bool IsHeartsBroken
        {
            get
            {
                return this.PlayedTricks.Any(i => i.Cards.Any(j => j.Value.Suit == Suit.Hearts));
            }
        }

        public bool IsLeadTurn
        {
            get
            {
                return !this.CurrentTrick.Any();
            }
        }

        public bool IsFollowTurn
        {
            get
            {
                return this.CurrentTrick.Any();
            }
        }

        public bool IsFirstHand
        {
            get
            {
                return !this.PlayedTricks.Any();
            }
        }

        public bool IsFirstLeadHand
        {
            get
            {
                return this.IsLeadTurn && this.IsFirstHand;
            }
        }


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