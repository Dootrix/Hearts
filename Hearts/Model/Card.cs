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
            var other = obj as Card;
            
            return other != null && this.GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return (int)this.Kind * 10 + (int)this.Suit;
        }
    }
}
