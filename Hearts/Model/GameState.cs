using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Model
{
    public class GameState
    {
        public GameState(Player player, Round round, PlayerState cards)
        {
            this.Round = round;
            //this.Cards = cards;
            this.StartingCards = cards.Starting;
            this.PostPassCards = cards.PostPass;
            this.CurrentCards = cards.Current;
            this.LegalCards = cards.Legal;
        }

        public Player Player { get; private set; }

        // TODO: Build hierarchy
        //public Game Game { get; set; }

        public Round Round { get; private set; } // Temporary, this will go inside Game

        //public PlayerState Cards { get; private set; }

        public IEnumerable<Card> StartingCards { get; private set; }
        public IEnumerable<Card> PassedCards { get { return this.StartingCards.Except(this.PostPassCards); } }
        public IEnumerable<Card> PostPassCards { get; private set; }
        public IEnumerable<Card> CurrentCards { get; private set; }
        public IEnumerable<Card> LegalCards { get; private set; }
    }
}
