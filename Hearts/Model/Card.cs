using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Extensions;

namespace Hearts.Model
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Card
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

        public override bool Equals(object obj)
        {
            return obj is Card && this == (Card)obj;
        }

        public static bool operator ==(Card a, Card b)
        {
            return a.Kind == b.Kind && a.Suit == b.Suit;
        }

        public static bool operator !=(Card a, Card b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            //http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + Kind.GetHashCode();
                hash = hash * 23 + Suit.GetHashCode();
                return hash;
            }
        }
    }
}
