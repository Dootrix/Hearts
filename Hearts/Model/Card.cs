using Hearts.Extensions;
using System;

namespace Hearts.Model
{
    public class Card
    {
        private int cachedHashCode;
        private Kind kind;
        private Suit suit;

        public Card(Kind kind, Suit suit)
        {
            this.Suit = suit;
            this.Kind = kind;
            this.cachedHashCode = this.RecalculateHashCode();
        }

        /// <summary>
        /// Creates a card from a number/hash (0–51)
        /// </summary>
        /// <param name="hash">An integer from 0–51</param>
        public Card(int hash)
        {
            int sVal = (int)Math.Floor(hash / 13.0);
            int kVal = hash % 13;
            this.Kind = (Kind)(kVal + 2);
            this.Suit = (Suit)sVal;
            this.RecalculateHashCode();
        }

        public Suit Suit
        {
            get { return this.suit; }
            set
            {
                this.suit = value; this.RecalculateHashCode();
            }
        }

        public Kind Kind
        {
            get { return this.kind; }
            set
            {
                this.kind = value; this.RecalculateHashCode();
            }
        }

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

        public static bool operator >(Card a, Kind b)
        {
            bool isANull = ReferenceEquals(null, a);

            return !isANull && a.Kind > b;
        }

        public static bool operator <(Card a, Kind b)
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

        /// <summary>
        /// Turns a card into a unique integer hash (from 0–51)
        /// </summary>
        /// <param name="card">The card</param>
        /// <returns>An integer from 0–51 unique to that card</returns>
        public override int GetHashCode()
        {
            return this.cachedHashCode;
        }

        private int RecalculateHashCode()
        {
            int kVal = (int)this.Kind - 2;
            int sVal = (int)this.Suit;
            int hashcode = kVal + sVal * 13;

            return hashcode;
        }
    }
}
