
using System.ComponentModel;
using Hearts.Attributes;

namespace Hearts.Model
{
    public enum Kind
    {
        [Abbreviation("2")]
        [Description("2")]
        Two = 2,

        [Abbreviation("3")]
        [Description("3")]
        Three = 3,

        [Abbreviation("4")]
        [Description("4")]
        Four = 4,

        [Abbreviation("5")]
        [Description("5")]
        Five = 5,

        [Abbreviation("6")]
        [Description("6")]
        Six = 6,

        [Abbreviation("7")]
        [Description("7")]
        Seven = 7,

        [Abbreviation("8")]
        [Description("8")]
        Eight = 8,

        [Abbreviation("9")]
        [Description("9")]
        Nine = 9,

        [Abbreviation("T")]
        [Description("T")]
        Ten = 10,

        [Abbreviation("J")]
        [Description("J")]
        Jack = 11,

        [Abbreviation("Q")]
        [Description("Q")]
        Queen = 12,

        [Abbreviation("K")]
        [Description("K")]
        King = 13,

        [Abbreviation("A")]
        [Description("A")]
        Ace = 14
    }
}
