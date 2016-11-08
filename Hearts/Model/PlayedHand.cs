using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;
using System;

namespace Hearts.Model
{
    public class PlayedHand
    {
        public PlayedHand()
        {
            this.Cards = new Dictionary<Guid, Card>();
        }

        public Dictionary<Guid, Card> Cards { get; set; }
    }
}