using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Hearts.Extensions;

namespace Hearts.Model
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Player
    {
        private Guid guid;

        public Player(string name)
        {
            this.guid = Guid.NewGuid();
            this.Name = name;
        }

        public Guid Guid { get { return this.guid; } }

        public string Name { get; private set; }

        public Player NextPlayer { get; set; }

        public Player PreviousPlayer { get; set; }

        public bool AgentHasMadeIllegalMove { get; set; }

        public string DebuggerDisplay(IEnumerable<Card> cards)
        {
            int padToLength = 25;
            string hearts = "♥: " + string.Join(" ", cards.Where(i => i.Suit == Suit.Hearts).OrderBy(i => i.Kind).Select(i => i.Kind.ToAbbreviation())).PadRight(padToLength);
            string spades = "♠: " + string.Join(" ", cards.Where(i => i.Suit == Suit.Spades).OrderBy(i => i.Kind).Select(i => i.Kind.ToAbbreviation())).PadRight(padToLength);
            string diamonds = "♦: " + string.Join(" ", cards.Where(i => i.Suit == Suit.Diamonds).OrderBy(i => i.Kind).Select(i => i.Kind.ToAbbreviation())).PadRight(padToLength);
            string clubs = "♣: " + string.Join(" ", cards.Where(i => i.Suit == Suit.Clubs).OrderBy(i => i.Kind).Select(i => i.Kind.ToAbbreviation())).PadRight(padToLength);

            return this.Name + "     " + string.Join(" ", new List<string> { hearts, spades, diamonds, clubs });
        }
    }
}
