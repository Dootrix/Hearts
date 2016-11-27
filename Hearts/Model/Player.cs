using System;
using System.Diagnostics;

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

        public string DebuggerDisplay
        {
            get
            {
                return "Player needs Debugger Display re-implementing";
                //int padToLength = 25;
                //string hearts = "♥: " + string.Join(" ", this.Remaining.Where(i => i.Suit == Suit.Hearts).OrderBy(i => i.Kind).Select(i => i.Kind.ToAbbreviation())).PadRight(padToLength);
                //string spades = "♠: " + string.Join(" ", this.Remaining.Where(i => i.Suit == Suit.Spades).OrderBy(i => i.Kind).Select(i => i.Kind.ToAbbreviation())).PadRight(padToLength);
                //string diamonds = "♦: " + string.Join(" ", this.Remaining.Where(i => i.Suit == Suit.Diamonds).OrderBy(i => i.Kind).Select(i => i.Kind.ToAbbreviation())).PadRight(padToLength);
                //string clubs = "♣: " + string.Join(" ", this.Remaining.Where(i => i.Suit == Suit.Clubs).OrderBy(i => i.Kind).Select(i => i.Kind.ToAbbreviation())).PadRight(padToLength);

                //return this.Name + "     " + string.Join(" ", new List<string> { hearts, spades, diamonds, clubs });
            }
        }
    }
}