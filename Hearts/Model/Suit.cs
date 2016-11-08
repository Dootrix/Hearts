using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Model
{
    public enum Suit
    {
        [Abbreviation("♣")]
        Clubs,

        [Abbreviation("♦")]
        Diamonds,

        [Abbreviation("♠")]
        Spades,

        [Abbreviation("♥")]
        Hearts
    }
}
