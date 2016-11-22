using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Model
{
    public class GameState
    {
        public GameState(Round round, PlayerCards cards)
        {
            this.Round = round;
            this.Cards = cards;
        }

        // TODO: Build hierarchy
        //public Game Game { get; set; }

        public Round Round { get; private set; } // Temporary, this will go inside Game

        public PlayerCards Cards { get; private set; }
    }
}
