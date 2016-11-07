using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public struct Card
    {
        public Suit Suit;
        public Kind Kind;
        
        public Card(Kind kind, Suit suit)
        {
            this.Suit = suit;
            this.Kind = kind;
        }

        public string DebuggerDisplay { get { return this.ToString(); } }

        public override string ToString()
        {
            return this.Kind.ToAbbreviation() + this.Suit.ToAbbreviation();
        }
    }
}
