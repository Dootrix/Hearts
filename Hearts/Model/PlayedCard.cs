using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;
using System;

namespace Hearts.Model
{
    public class PlayedCard
    {
        public Player Player { get; set; }
        public Card Card { get; set; }
    }
}