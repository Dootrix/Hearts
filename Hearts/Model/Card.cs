using Hearts.Extensions;

namespace Hearts.Model
{
    public class Card
    {   
        public Card(Kind kind, Suit suit)
        {
            this.Suit = suit;
            this.Kind = kind;
        }

        public Suit Suit { get; private set; }

        public Kind Kind { get; private set; }

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
            bool isANull = ReferenceEquals(null, a);
            bool isBNull = ReferenceEquals(null, b);

            if (isANull || isBNull)
                return isANull && isBNull;

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
