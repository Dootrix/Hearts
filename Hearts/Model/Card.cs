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

        public static Card operator +(Card a, int b)
        {
            if (ReferenceEquals(null, a))
            {
                return null;
            }

            var kind = a.Kind.Increment(b);
            if (!kind.HasValue)
            {
                return null;
            }

            return new Card(kind.Value, a.Suit);
        }

        public static Card operator -(Card a, int b)
        {
            if (ReferenceEquals(null, a))
            {
                return null;
            }

            var kind = a.Kind.Decrement(b);
            if (!kind.HasValue)
            {
                return null;
            }

            return new Card(kind.Value, a.Suit);
        }

        public static bool operator ==(Card a, Suit b)
        {
            bool isANull = ReferenceEquals(null, a);

            return !isANull && a.Suit == b;
        }

        public static bool operator !=(Card a, Suit b)
        {
            bool isANull = ReferenceEquals(null, a);

            return !isANull && a.Suit != b;
        }

        public static bool operator ==(Card a, Kind b)
        {
            bool isANull = ReferenceEquals(null, a);

            return !isANull && a.Kind == b;
        }

        public static bool operator !=(Card a, Kind b)
        {
            bool isANull = ReferenceEquals(null, a);

            return !isANull && a.Kind != b;
        }

        public static bool operator > (Card a, Kind b)
        {
            bool isANull = ReferenceEquals(null, a);

            return !isANull && a.Kind > b;
        }

        public static bool operator < (Card a, Kind b)
        {
            bool isANull = ReferenceEquals(null, a);

            return !isANull && a.Kind < b;
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
