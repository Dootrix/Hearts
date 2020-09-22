
using Hearts.Attributes;

namespace Hearts.Model
{
    public enum Suit
    {
        [Abbreviation("♣")]
        Clubs = 0,

        [Abbreviation("♦")]
        Diamonds = 1,

        [Abbreviation("♠")]
        Spades = 2,

        [Abbreviation("♥")]
        Hearts = 3
    }
}
