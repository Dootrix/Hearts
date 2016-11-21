using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;
using System;

namespace Hearts.Model
{
    public class PlayedTrick
    {
        public PlayedTrick()
        {
            this.Cards = new Dictionary<Player, Card>();
        }

        public Dictionary<Player, Card> Cards { get; set; }

        public Player Winner { get; set; }
    }
}