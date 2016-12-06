using System.Collections.Generic;
using System.Linq;
using Hearts.Extensions;

namespace Hearts.Model
{
    public class Cards
    {
        public static Card AceOfHearts = new Card(Kind.Ace, Suit.Hearts);
        public static Card KingOfHearts = new Card(Kind.King, Suit.Hearts);
        public static Card QueenOfHearts = new Card(Kind.Queen, Suit.Hearts);
        public static Card JackOfHearts = new Card(Kind.Jack, Suit.Hearts);
        public static Card TenOfHearts = new Card(Kind.Ten, Suit.Hearts);
        public static Card NineOfHearts = new Card(Kind.Nine, Suit.Hearts);
        public static Card EightOfHearts = new Card(Kind.Eight, Suit.Hearts);
        public static Card SevenOfHearts = new Card(Kind.Seven, Suit.Hearts);
        public static Card SixOfHearts = new Card(Kind.Six, Suit.Hearts);
        public static Card FiveOfHearts = new Card(Kind.Five, Suit.Hearts);
        public static Card FourOfHearts = new Card(Kind.Four, Suit.Hearts);
        public static Card ThreeOfHearts = new Card(Kind.Three, Suit.Hearts);
        public static Card TwoOfHearts = new Card(Kind.Two, Suit.Hearts);

        public static Card AceOfSpades = new Card(Kind.Ace, Suit.Spades);
        public static Card KingOfSpades = new Card(Kind.King, Suit.Spades);
        public static Card QueenOfSpades = new Card(Kind.Queen, Suit.Spades);
        public static Card JackOfSpades = new Card(Kind.Jack, Suit.Spades);
        public static Card TenOfSpades = new Card(Kind.Ten, Suit.Spades);
        public static Card NineOfSpades = new Card(Kind.Nine, Suit.Spades);
        public static Card EightOfSpades = new Card(Kind.Eight, Suit.Spades);
        public static Card SevenOfSpades = new Card(Kind.Seven, Suit.Spades);
        public static Card SixOfSpades = new Card(Kind.Six, Suit.Spades);
        public static Card FiveOfSpades = new Card(Kind.Five, Suit.Spades);
        public static Card FourOfSpades = new Card(Kind.Four, Suit.Spades);
        public static Card ThreeOfSpades = new Card(Kind.Three, Suit.Spades);
        public static Card TwoOfSpades = new Card(Kind.Two, Suit.Spades);

        public static Card AceOfDiamonds = new Card(Kind.Ace, Suit.Diamonds);
        public static Card KingOfDiamonds = new Card(Kind.King, Suit.Diamonds);
        public static Card QueenOfDiamonds = new Card(Kind.Queen, Suit.Diamonds);
        public static Card JackOfDiamonds = new Card(Kind.Jack, Suit.Diamonds);
        public static Card TenOfDiamonds = new Card(Kind.Ten, Suit.Diamonds);
        public static Card NineOfDiamonds = new Card(Kind.Nine, Suit.Diamonds);
        public static Card EightOfDiamonds = new Card(Kind.Eight, Suit.Diamonds);
        public static Card SevenOfDiamonds = new Card(Kind.Seven, Suit.Diamonds);
        public static Card SixOfDiamonds = new Card(Kind.Six, Suit.Diamonds);
        public static Card FiveOfDiamonds = new Card(Kind.Five, Suit.Diamonds);
        public static Card FourOfDiamonds = new Card(Kind.Four, Suit.Diamonds);
        public static Card ThreeOfDiamonds = new Card(Kind.Three, Suit.Diamonds);
        public static Card TwoOfDiamonds = new Card(Kind.Two, Suit.Diamonds);

        public static Card AceOfClubs = new Card(Kind.Ace, Suit.Clubs);
        public static Card KingOfClubs = new Card(Kind.King, Suit.Clubs);
        public static Card QueenOfClubs = new Card(Kind.Queen, Suit.Clubs);
        public static Card JackOfClubs = new Card(Kind.Jack, Suit.Clubs);
        public static Card TenOfClubs = new Card(Kind.Ten, Suit.Clubs);
        public static Card NineOfClubs = new Card(Kind.Nine, Suit.Clubs);
        public static Card EightOfClubs = new Card(Kind.Eight, Suit.Clubs);
        public static Card SevenOfClubs = new Card(Kind.Seven, Suit.Clubs);
        public static Card SixOfClubs = new Card(Kind.Six, Suit.Clubs);
        public static Card FiveOfClubs = new Card(Kind.Five, Suit.Clubs);
        public static Card FourOfClubs = new Card(Kind.Four, Suit.Clubs);
        public static Card ThreeOfClubs = new Card(Kind.Three, Suit.Clubs);
        public static Card TwoOfClubs = new Card(Kind.Two, Suit.Clubs);

        public static List<Card> Hearts = new List<Card> { TwoOfHearts, ThreeOfHearts, FourOfHearts, FiveOfHearts, SixOfHearts, SevenOfHearts, EightOfHearts, NineOfHearts, TenOfHearts, JackOfHearts, QueenOfHearts, KingOfHearts, AceOfHearts };
        public static List<Card> Spades = new List<Card> { TwoOfSpades, ThreeOfSpades, FourOfSpades, FiveOfSpades, SixOfSpades, SevenOfSpades, EightOfSpades, NineOfSpades, TenOfSpades, JackOfSpades, QueenOfSpades, KingOfSpades, AceOfSpades };
        public static List<Card> Diamonds = new List<Card> { TwoOfDiamonds, ThreeOfDiamonds, FourOfDiamonds, FiveOfDiamonds, SixOfDiamonds, SevenOfDiamonds, EightOfDiamonds, NineOfDiamonds, TenOfDiamonds, JackOfDiamonds, QueenOfDiamonds, KingOfDiamonds, AceOfDiamonds };
        public static List<Card> Clubs = new List<Card> { TwoOfClubs, ThreeOfClubs, FourOfClubs, FiveOfClubs, SixOfClubs, SevenOfClubs, EightOfClubs, NineOfClubs, TenOfClubs, JackOfClubs, QueenOfClubs, KingOfClubs, AceOfClubs };

        public static List<Card> AscendingHearts = new List<Card> { TwoOfHearts, ThreeOfHearts, FourOfHearts, FiveOfHearts, SixOfHearts, SevenOfHearts, EightOfHearts, NineOfHearts, TenOfHearts, JackOfHearts, QueenOfHearts, KingOfHearts, AceOfHearts };
        public static List<Card> AscendingSpades = new List<Card> { TwoOfSpades, ThreeOfSpades, FourOfSpades, FiveOfSpades, SixOfSpades, SevenOfSpades, EightOfSpades, NineOfSpades, TenOfSpades, JackOfSpades, QueenOfSpades, KingOfSpades, AceOfSpades };
        public static List<Card> AscendingDiamonds = new List<Card> { TwoOfDiamonds, ThreeOfDiamonds, FourOfDiamonds, FiveOfDiamonds, SixOfDiamonds, SevenOfDiamonds, EightOfDiamonds, NineOfDiamonds, TenOfDiamonds, JackOfDiamonds, QueenOfDiamonds, KingOfDiamonds, AceOfDiamonds };
        public static List<Card> AscendingClubs = new List<Card> { TwoOfClubs, ThreeOfClubs, FourOfClubs, FiveOfClubs, SixOfClubs, SevenOfClubs, EightOfClubs, NineOfClubs, TenOfClubs, JackOfClubs, QueenOfClubs, KingOfClubs, AceOfClubs };

        public static List<Card> DescendingHearts = new List<Card> { AceOfHearts, KingOfHearts, QueenOfHearts, JackOfHearts, TenOfHearts, NineOfHearts, EightOfHearts, SevenOfHearts, SixOfHearts, FiveOfHearts, FourOfHearts, ThreeOfHearts, TwoOfHearts };
        public static List<Card> DescendingSpades = new List<Card> { AceOfSpades, KingOfSpades, QueenOfSpades, JackOfSpades, TenOfSpades, NineOfSpades, EightOfSpades, SevenOfSpades, SixOfSpades, FiveOfSpades, FourOfSpades, ThreeOfSpades, TwoOfSpades };
        public static List<Card> DescendingDiamonds = new List<Card> { AceOfDiamonds, KingOfDiamonds, QueenOfDiamonds, JackOfDiamonds, TenOfDiamonds, NineOfDiamonds, EightOfDiamonds, SevenOfDiamonds, SixOfDiamonds, FiveOfDiamonds, FourOfDiamonds, ThreeOfDiamonds, TwoOfDiamonds };
        public static List<Card> DescendingClubs = new List<Card> { AceOfClubs, KingOfClubs, QueenOfClubs, JackOfClubs, TenOfClubs, NineOfClubs, EightOfClubs, SevenOfClubs, SixOfClubs, FiveOfClubs, FourOfClubs, ThreeOfClubs, TwoOfClubs };
        
        public static List<Card> Deck = Hearts.Union(Spades).Union(Diamonds).Union(Clubs).ToList();

        public static List<Card> DeckDescending = Hearts.Union(Spades).Union(Diamonds).Union(Clubs).Descending().ToList();
    }
}
